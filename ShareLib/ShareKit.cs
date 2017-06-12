using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.SAPRFCService.Domain;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using System.IO;
using System.Net.Mail;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.SAPRFCService;
using BenQGuru.eMES.IQC;

namespace ShareLib
{
    public class ShareKit
    {

        public static bool IsReBackSupport = true;


        public string Receive(string stno, List<Asndetail> asnDetails, string rejectReason, string usr, string webFilePath, InventoryFacade _InventoryFacade, WarehouseFacade _WarehouseFacade, IDomainDataProvider DataProvider)
        {
            ASN asn = (ASN)_InventoryFacade.GetASN(stno);
            if (asn == null)
                return stno + "不存在";
            List<ASNDetail> ads = new List<ASNDetail>();
            foreach (Asndetail asnDetail in asnDetails)
            {
                ASNDetail ad = (ASNDetail)_InventoryFacade.GetASNDetail(int.Parse(asnDetail.Stline), asnDetail.Stno);
                ads.Add(ad);
                if (string.IsNullOrEmpty(ad.CartonNo) && asn.InitRejectQty == 0)
                    return ad.StNo + ":" + ad.StLine + "未绑定箱号," + "请填写拒收数量";
            }
            if (ads.Count <= 0)
                return "没有对应的入库条目！";

            List<PoLog> logs = new List<PoLog>();
            try
            {

                if (asn.Status != ASN_STATUS.ASN_ReceiveRejection && asn.Status != ASN_STATUS.ASN_Receive)
                    return "入库指令状态必须是初检阶段！";

                int noCartons = _InventoryFacade.NotHavCatonnoCount(asn.StNo);
                if (noCartons != asn.InitRejectQty)
                    return "拒收数量为" + asn.InitRejectQty + " 未绑定箱号数量为" + noCartons + " 两者必须一致！";

                foreach (ASNDetail ad in ads)
                {

                    if (!string.IsNullOrEmpty(ad.CartonNo) && !_WarehouseFacade.CheckAlterIncludeEQ(asn.InvNo, ad.DQMCode))
                        return asn.InvNo + ":" + ad.DQMCode + "数量已超出SAP计划数量！";
                }
                DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(DataProvider);
                DataProvider.BeginTransaction();


                foreach (ASNDetail ad in ads)
                {
                    if (!string.IsNullOrEmpty(ad.CartonNo))
                    {
                        ad.Status = ASNDetail_STATUS.ASNDetail_ReceiveClose;
                        if (ad.InitReceiveStatus != SAP_LineStatus.SAP_LINE_GIVEIN)
                            ad.InitReceiveStatus = SAP_LineStatus.SAP_LINE_RECEIVE;
                        ad.ReceiveQty = ad.Qty;
                        ad.MaintainTime = dbTime1.DBTime;
                        ad.MaintainDate = dbTime1.DBDate;
                        _InventoryFacade.UpdateASNDetail(ad);
                        asn.InitReceiveQty += 1;
                    }
                    else
                    {

                        ad.Status = ASNDetail_STATUS.ASNDetail_ReceiveClose;
                        ad.InitReceiveStatus = SAP_LineStatus.SAP_LINE_REJECT;
                        ad.InitReceiveDesc = rejectReason;
                        ad.MaintainTime = dbTime1.DBTime;
                        ad.MaintainDate = dbTime1.DBDate;
                        _InventoryFacade.UpdateASNDetail(ad);
                        decimal rejectCount = (decimal)ad.Qty;
                        InvoicesDetail[] invs = _WarehouseFacade.GetInvoiceDetailsDescByPlanDate(stno, ad.DQMCode);

                        if (invs == null)
                            throw new Exception(ad.StNo + ":" + ad.DQMCode + " SAP单据不存在！");


                        foreach (InvoicesDetail inv in invs)
                        {
                            if (rejectCount == 0)
                                break;
                            Asndetailitem[] items = _WarehouseFacade.GetAsnItems(ad.StNo, inv.InvNo, inv.InvLine.ToString());
                            foreach (Asndetailitem item in items)
                            {
                                Log.Error("RejectCount ======------------------------------------------------- -----" + rejectCount);
                                if (rejectCount <= 0)
                                    break;
                                if (item.Qty >= rejectCount)
                                {
                                    item.Qty -= rejectCount;
                                    item.ReceiveQty -= rejectCount;
                                    item.ActQty -= rejectCount;
                                    item.QcpassQty -= rejectCount;
                                    rejectCount = 0;
                                }
                                else
                                {
                                    rejectCount -= item.Qty;
                                    item.Qty = 0;
                                    item.ReceiveQty = 0;
                                    item.ActQty = 0;
                                    item.QcpassQty = 0;

                                }

                                _WarehouseFacade.UpdateAsndetailitem(item);
                            }
                        }
                    }
                }

                #region 加trans
                bool hasDetail = _InventoryFacade.CheckASNHasDetail(asn.StNo, ASNLineStatus.ReceiveClose);

                if (hasDetail)
                    throw new Exception("初检完成失败，某些剩余行项目不能确定状态！");

                if (!hasDetail)
                {
                    //接收完成写trans
                    WarehouseFacade facade = new WarehouseFacade(DataProvider);
                    #region 在invinouttrans表中增加一条数据
                    //ASN asn = (ASN)domainObject;

                    InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                    trans.CartonNO = string.Empty;
                    trans.DqMCode = string.Empty;
                    trans.FacCode = asn.FacCode;
                    trans.FromFacCode = asn.FromFacCode;
                    trans.FromStorageCode = asn.FromStorageCode;
                    trans.InvNO = asn.InvNo;
                    trans.InvType = asn.StType;
                    trans.LotNo = string.Empty;
                    trans.MaintainDate = dbTime1.DBDate;
                    trans.MaintainTime = dbTime1.DBTime;
                    trans.MaintainUser = usr;
                    trans.MCode = string.Empty;
                    trans.ProductionDate = 0;
                    trans.Qty = 0;
                    trans.Serial = 0;
                    trans.StorageAgeDate = 0;
                    trans.StorageCode = asn.StorageCode;
                    trans.SupplierLotNo = string.Empty;
                    trans.TransNO = asn.StNo;
                    trans.TransType = "IN";
                    trans.Unit = string.Empty;
                    trans.ProcessType = "Receive";

                    facade.AddInvInOutTrans(trans);

                    #endregion

                }
                #endregion

                bool allReject = true;

                foreach (ASNDetail ad in ads)
                {
                    if (ad.InitReceiveStatus != SAP_LineStatus.SAP_LINE_REJECT)
                        allReject = false;
                }
                if (allReject)
                {
                    asn.MaintainDate = dbTime1.DBDate;
                    asn.MaintainTime = dbTime1.DBTime;
                    asn.Status = ASN_STATUS.ASN_ReceiveRejection;
                }

                else
                {
                    asn.MaintainDate = dbTime1.DBDate;
                    asn.MaintainTime = dbTime1.DBTime;
                    asn.Status = ASNDetail_STATUS.ASNDetail_ReceiveClose;
                }
                _InventoryFacade.UpdateASN(asn);


                if (!allReject)
                    PostToSap(usr, DataProvider, asn, out logs, hasDetail);

                SendMail mail = ReceiveRejectThenGenerMail(ads, usr, asn, _WarehouseFacade);
                if (mail != null)
                    _WarehouseFacade.AddSendMail(mail);

                DataProvider.CommitTransaction();
                try
                {
                    InvDoc doc = _WarehouseFacade.GetCartonnoDoc(asn.StNo);
                    if (doc != null)
                    {
                        string sourceFullPath = webFilePath + doc.ServerFileName;
                        string destFullPath = @"D:/mesinpacking/new/" + doc.ServerFileName;
                        File.Copy(sourceFullPath, destFullPath);
                    }

                }
                catch (Exception ex)
                {

                    Log.Error(ex.Message);
                }

                foreach (PoLog log in logs)
                    _InventoryFacade.AddPoLog(log);
                return "接收成功！";

            }
            catch (Exception ex)
            {

                DataProvider.RollbackTransaction();
                BenQGuru.eMES.Common.Log.Error(ex.Message + "---" + ex.StackTrace);
                foreach (PoLog log in logs)
                    _InventoryFacade.AddPoLog(log);
                throw ex;
            }

        }


        public bool IQCSubmit(string _IQCNo, IDomainDataProvider DataProvider, string user, string checkType, string aql, string rejectionNum, out string message)
        {


            IQCFacade _IQCFacade = new IQCFacade(DataProvider);

            InventoryFacade _InventoryFacade = new InventoryFacade(DataProvider);
            WarehouseFacade _WarehouseFacade = new WarehouseFacade(DataProvider);

            AsnIQC iqc = _IQCFacade.GetAsnIQC(_IQCNo) as AsnIQC;
            message = string.Empty;
            if (iqc == null)
            {
                message = _IQCNo + "检验单不存在！";
                return false;

            }
            if (iqc.Status == IQCStatus.IQCStatus_SQEJudge)
            {
                message = _IQCNo + " 检验单的状态已经是SQE判定不能再提交";
                return false;
            }
            if (iqc.Status != IQCStatus.IQCStatus_WaitCheck && iqc.Status != IQCStatus.IQCStatus_Release)
            {

                message = _IQCNo + "必须是待检验或者是初始化才能提交！";
                return false;
            }
            if (iqc.Status == IQCStatus.IQCStatus_IQCClose)
            {
                message = _IQCNo + "已经关闭，不能再提交";
                return false;
            }
            try
            {
                DataProvider.BeginTransaction();

                int ngQty = _IQCFacade.GetSumNgQtyFromAsnIQCDetail(_IQCNo);
                if (ngQty == 0)
                {
                    this.ToSTS(_IQCNo, _IQCFacade, _InventoryFacade, _WarehouseFacade, user);

                    if (checkType == OQCType.OQCType_SpotCheck)
                        iqc.IqcType = OQCType.OQCType_SpotCheck;
                    else if (checkType == OQCType.OQCType_FullCheck)
                        iqc.IqcType = OQCType.OQCType_FullCheck;
                    iqc.AQLLevel = aql;
                    iqc.Status = "IQCClose";
                    iqc.QcStatus = "Y";
                    _IQCFacade.UpdateAsnIQC(iqc);

                }
                else if (checkType == OQCType.OQCType_FullCheck && ngQty > 0)
                {
                    #region 更新表 TBLASNIQC,TBLASNIQCDETAIL,TBLASNIQCDETAILSN


                    iqc.IqcType = IQCType.IQCType_FullCheck;
                    iqc.Status = IQCStatus.IQCStatus_SQEJudge;
                    iqc.AQLLevel = aql;
                    iqc.QcStatus = "N";

                    _IQCFacade.UpdateAsnIQC(iqc);
                    ToSTS1(_IQCNo, _IQCFacade, _WarehouseFacade, user);
                    object[] objAsnIqcDetail = _IQCFacade.GetAsnIQCDetailByIqcNo(_IQCNo);
                    if (objAsnIqcDetail != null)
                    {
                        foreach (AsnIQCDetail asnIQCDetail in objAsnIqcDetail)
                        {

                            asnIQCDetail.QcStatus = "Y";
                            asnIQCDetail.QcPassQty = asnIQCDetail.Qty;

                            _IQCFacade.UpdateAsnIQCDetail(asnIQCDetail);

                        }
                    }

                    object[] objAsnIqcDetailSN = _IQCFacade.GetAsnIqcDetailSNByIqcNo(_IQCNo);
                    if (objAsnIqcDetailSN != null)
                    {
                        foreach (AsnIqcDetailSN asnIqcDetailSN in objAsnIqcDetailSN)
                        {

                            asnIqcDetailSN.QcStatus = "Y";
                            _IQCFacade.UpdateAsnIqcDetailSN(asnIqcDetailSN);


                            Asndetailsn asnDetailSn = (Asndetailsn)_InventoryFacade.GetAsndetailsn(asnIqcDetailSN.Sn, asnIqcDetailSN.StNo, Convert.ToInt32(asnIqcDetailSN.StLine));

                            if (asnDetailSn != null)
                            {
                                asnDetailSn.QcStatus = "Y";
                                _InventoryFacade.UpdateAsndetailsn(asnDetailSn);
                            }
                        }
                    }

                    ToSTS1(_IQCNo, _IQCFacade, _WarehouseFacade, user);
                    #endregion
                }
                else if (checkType == OQCType.OQCType_SpotCheck && ngQty > 0)
                {
                    if (string.IsNullOrEmpty(rejectionNum))
                    {
                        message = "请选择AQL标准的样本数";
                        return false;
                    }
                    int rejectSize = Convert.ToInt32(rejectionNum);//页面拒收数量
                    if (ngQty < rejectSize)
                    {
                        #region 更新表 TBLASNIQC,TBLASNIQCDETAIL,TBLASNIQCDETAILSN,TBLASNDETAILITEM,TBLASNDETAIL

                        //1》通过IQC检验单号更新送检单表(TBLASNIQC)数据

                        if (iqc != null)
                        {
                            iqc.IqcType = IQCType.IQCType_SpotCheck;
                            iqc.Status = IQCStatus.IQCStatus_SQEJudge;//IQCStatus.IQCStatus_IQCClose;
                            iqc.QcStatus = "Y";


                            //2》	通过IQC检验单号更新检单明细表(TBLASNIQCDETAIL)
                            object[] objAsnIqcDetail = _IQCFacade.GetAsnIQCDetailByIqcNo(_IQCNo);
                            if (objAsnIqcDetail != null)
                            {
                                foreach (AsnIQCDetail asnIQCDetail in objAsnIqcDetail)
                                {

                                    asnIQCDetail.QcStatus = "Y";
                                    asnIQCDetail.QcPassQty = asnIQCDetail.Qty;
                                    _IQCFacade.UpdateAsnIQCDetail(asnIQCDetail);

                                    //更新ASN明细表(TBLASNDETAIL)
                                    ASNDetail asnDetail = (ASNDetail)_InventoryFacade.GetASNDetail(Convert.ToInt32(asnIQCDetail.StLine), asnIQCDetail.StNo);
                                    if (asnDetail != null)
                                    {

                                        asnDetail.QcPassQty = asnDetail.ReceiveQty;
                                        asnDetail.Status = ASNLineStatus.IQCClose;

                                        _InventoryFacade.UpdateASNDetail(asnDetail);
                                    }

                                    //更新ASN明细对应单据行明细表(TBLASNDETAILITEM)
                                    //object[] objAsnDetailItem = _InventoryFacade.GetAsnDetailItem(asnIQCDetail.StNo, Convert.ToInt32(asnIQCDetail.StLine));
                                    //if (objAsnDetailItem != null)
                                    //{
                                    //    foreach (Asndetailitem asnDetailItem in objAsnDetailItem)
                                    //    {
                                    //        asnDetailItem.QcpassQty = asnDetailItem.ReceiveQty;
                                    //        asnDetailItem.ActQty = asnDetailItem.QcpassQty;
                                    //        _InventoryFacade.UpdateAsndetailitem(asnDetailItem);
                                    //    }
                                    //}
                                }
                            }

                            //3》通过IQC检验单号更新检单明细SN表
                            object[] objAsnIqcDetailSN = _IQCFacade.GetAsnIqcDetailSNByIqcNo(_IQCNo);
                            if (objAsnIqcDetailSN != null)
                            {
                                foreach (AsnIqcDetailSN asnIqcDetailSN in objAsnIqcDetailSN)
                                {

                                    asnIqcDetailSN.QcStatus = "Y";
                                    _IQCFacade.UpdateAsnIqcDetailSN(asnIqcDetailSN);


                                    Asndetailsn asnDetailSn = (Asndetailsn)_InventoryFacade.GetAsndetailsn(asnIqcDetailSN.Sn, asnIqcDetailSN.StNo, Convert.ToInt32(asnIqcDetailSN.StLine));
                                    if (asnDetailSn != null)
                                    {
                                        asnDetailSn.QcStatus = "Y";
                                        _InventoryFacade.UpdateAsndetailsn(asnDetailSn);
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        #region 更新表 TBLASNIQC,TBLASNIQCDETAIL,TBLASNIQCDETAILSN

                        if (iqc != null)
                        {
                            iqc.IqcType = IQCType.IQCType_SpotCheck;
                            iqc.Status = IQCStatus.IQCStatus_SQEJudge;
                            iqc.QcStatus = "N";

                            object[] objAsnIqcDetail = _IQCFacade.GetAsnIQCDetailByIqcNo(_IQCNo);
                            if (objAsnIqcDetail != null)
                            {
                                foreach (AsnIQCDetail asnIQCDetail in objAsnIqcDetail)
                                {

                                    asnIQCDetail.QcStatus = "N";
                                    asnIQCDetail.QcPassQty = 0;
                                    _IQCFacade.UpdateAsnIQCDetail(asnIQCDetail);

                                }
                            }

                            object[] objAsnIqcDetailSN = _IQCFacade.GetAsnIqcDetailSNByIqcNo(_IQCNo);
                            if (objAsnIqcDetailSN != null)
                            {
                                foreach (AsnIqcDetailSN sn in objAsnIqcDetailSN)
                                {
                                    sn.QcStatus = "N";
                                    _IQCFacade.UpdateAsnIqcDetailSN(sn);
                                    Asndetailsn asnDetailSn = (Asndetailsn)_InventoryFacade.GetAsndetailsn(sn.Sn, sn.StNo, Convert.ToInt32(sn.StLine));

                                    if (asnDetailSn != null)
                                    {
                                        asnDetailSn.QcStatus = "N";
                                        _InventoryFacade.UpdateAsndetailsn(asnDetailSn);
                                    }
                                }
                            }

                        }
                        #endregion
                    }
                    iqc.AQLLevel = aql;
                    _IQCFacade.UpdateAsnIQC(iqc);
                    ToSTS1(_IQCNo, _IQCFacade, _WarehouseFacade, user);
                }

                DataProvider.CommitTransaction();
                message = "提交成功";
                return true;

            }
            catch (Exception ex)
            {
                DataProvider.RollbackTransaction();
                message = "提交失败：" + ex.Message;
                return false;

            }

        }

        private void ToSTS1(string iqcNo, IQCFacade _IQCFacade, WarehouseFacade facade, string user)
        {



            //1、更新送检单TBLASNIQC
            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            Log.Error("----------------------------------------------------------------------------------------------");
            if (asnIqc != null)
            {

                #region 在invinouttrans表中增加一条数据

                //ASN asn = (ASN)domainObject;

                InvInOutTrans trans = facade.CreateNewInvInOutTrans();

                trans.CartonNO = string.Empty;
                trans.DqMCode = asnIqc.DQMCode;
                trans.FacCode = string.Empty;
                trans.FromFacCode = string.Empty;
                trans.FromStorageCode = string.Empty;
                trans.InvNO = asnIqc.InvNo;
                trans.InvType = asnIqc.IqcType;
                trans.LotNo = string.Empty;
                trans.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                trans.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                trans.MaintainUser = user;
                trans.MCode = asnIqc.MCode;
                trans.ProductionDate = 0;
                trans.Qty = asnIqc.Qty;
                trans.Serial = 0;
                trans.StorageAgeDate = 0;
                trans.StorageCode = string.Empty;
                trans.SupplierLotNo = string.Empty;
                trans.TransNO = asnIqc.IqcNo;
                trans.TransType = "IN";
                trans.Unit = string.Empty;
                trans.ProcessType = "IQC";
                facade.AddInvInOutTrans(trans);
                #endregion
            }

        }



        private void ToSTS(string iqcNo, IQCFacade _IQCFacade, InventoryFacade _InventoryFacade, WarehouseFacade facade, string user)
        {


            //1、更新送检单TBLASNIQC
            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                asnIqc.IqcType = IQCType.IQCType_ExemptCheck;//this.rblType.SelectedItem.Text;
                asnIqc.Status = IQCStatus.IQCStatus_IQCClose;
                asnIqc.QcStatus = "Y";
                _IQCFacade.UpdateAsnIQC(asnIqc);
                #region 在invinouttrans表中增加一条数据

                //ASN asn = (ASN)domainObject;

                InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                trans.CartonNO = string.Empty;
                trans.DqMCode = asnIqc.DQMCode;
                trans.FacCode = string.Empty;
                trans.FromFacCode = string.Empty;
                trans.FromStorageCode = string.Empty;
                trans.InvNO = asnIqc.InvNo;
                trans.InvType = asnIqc.IqcType;
                trans.LotNo = string.Empty;
                trans.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                trans.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                trans.MaintainUser = user;
                trans.MCode = asnIqc.MCode;
                trans.ProductionDate = 0;
                trans.Qty = asnIqc.Qty;
                trans.Serial = 0;
                trans.StorageAgeDate = 0;
                trans.StorageCode = string.Empty;
                trans.SupplierLotNo = string.Empty;
                trans.TransNO = asnIqc.IqcNo;
                trans.TransType = "IN";
                trans.Unit = string.Empty;
                trans.ProcessType = "IQC";
                facade.AddInvInOutTrans(trans);
                #endregion
            }

            object[] objAsnIqcDetail = _IQCFacade.GetAsnIQCDetailByIqcNo(iqcNo);
            if (objAsnIqcDetail != null)
            {
                foreach (AsnIQCDetail asnIqcDetail in objAsnIqcDetail)
                {
                    //2、更新送检单明细TBLASNIQCDETAIL
                    asnIqcDetail.QcPassQty = asnIqcDetail.Qty;
                    asnIqcDetail.QcStatus = "Y";
                    _IQCFacade.UpdateAsnIQCDetail(asnIqcDetail);


                    //4、更新ASN明细TBLASNDETAIL
                    ASNDetail asnDetail = (ASNDetail)_InventoryFacade.GetASNDetail(Convert.ToInt32(asnIqcDetail.StLine), asnIqcDetail.StNo);
                    if (asnDetail != null)
                    {
                        asnDetail.QcPassQty = asnDetail.ReceiveQty;
                        asnDetail.Status = IQCStatus.IQCStatus_IQCClose;
                        _InventoryFacade.UpdateASNDetail(asnDetail);
                    }

                    //5、更新ASN明细对应单据行明细TBLASNDETAILITEM
                    object[] objAsnDetaileItem = _InventoryFacade.GetAsnDetailItem(asnIqcDetail.StNo, Convert.ToInt32(asnIqcDetail.StLine));
                    if (objAsnDetaileItem != null)
                    {
                        foreach (Asndetailitem asnDetaileItem in objAsnDetaileItem)
                        {
                            asnDetaileItem.QcpassQty = asnDetaileItem.ReceiveQty;
                            asnDetaileItem.ActQty = asnDetaileItem.QcpassQty;
                            _InventoryFacade.UpdateAsndetailitem(asnDetaileItem);
                        }
                    }

                }
            }

            object[] objAsnIqcDetailSN = _IQCFacade.GetAsnIqcDetailSNByIqcNo(iqcNo);
            if (objAsnIqcDetailSN != null)
            {
                foreach (AsnIqcDetailSN asnIqcDetailSN in objAsnIqcDetailSN)
                {
                    //3、更新送检单明细SNTBLASNIQCDETAILSN
                    asnIqcDetailSN.QcStatus = "Y";
                    _IQCFacade.UpdateAsnIqcDetailSN(asnIqcDetailSN);

                    //6、更新ASN明细SN TBLASNDETAILSN
                    Asndetailsn asnDetailSn = (Asndetailsn)_InventoryFacade.GetAsndetailsn(asnIqcDetailSN.Sn, asnIqcDetailSN.StNo, Convert.ToInt32(asnIqcDetailSN.StLine));
                    if (asnDetailSn != null)
                    {
                        asnDetailSn.QcStatus = "Y";
                        _InventoryFacade.UpdateAsndetailsn(asnDetailSn);
                    }
                }
            }

            if (_IQCFacade.CanToOnlocationStaus(asnIqc.StNo))
            {

                ASN asn = (ASN)_InventoryFacade.GetASN(asnIqc.StNo);
                asn.Status = ASNHeadStatus.OnLocation;
                _InventoryFacade.UpdateASN(asn);
            }
        }


        public static SendMail IQCRejectThenGenerMail(AsnIQC iqc, ASN asn, string user, WarehouseFacade _WarehouseFacade, IQCFacade iqcFacade)
        {

            AsnIQCDetail[] details = iqcFacade.GetAsnIQCDetails(iqc.IqcNo);
            AsnIQCDetailEc[] ecs = iqcFacade.GetAsnIQCDetailReturnOrReformECs(iqc.IqcNo);
            if (details.Length == 0)
                return null;
            string stno = asn.StNo;
            string iqcNo = iqc.IqcNo;
            string invno = asn.InvNo;
            string cDate = FormatHelper.TODateTimeString(iqc.CDate, iqc.CTime);
            int iqcQty = 0;
            int ngQty = 0;
            int returnQty = 0;
            int reformQty = 0;
            int giveQty = 0;
            int acceptQty = 0;
            foreach (AsnIQCDetail iqcDetail in details)
            {
                ngQty += iqcDetail.NgQty;
                iqcQty += iqcDetail.Qty;
                returnQty += iqcDetail.ReturnQty;
                reformQty += iqcDetail.ReformQty;
                giveQty += iqcDetail.GiveQty;
                acceptQty += iqcDetail.AcceptQty;

            }
            List<string> ngDescs = new List<string>();
            foreach (AsnIQCDetailEc ec in ecs)
            {
                string ngDesc = ec.EcgCode + "-" + ec.ECode;
                if (!ngDescs.Contains(ngDesc))
                    ngDescs.Add(ngDesc);
            }

            if ((returnQty > 0 || reformQty > 0) || (iqc.Status == IQCStatus.IQCStatus_IQCRejection))
            {
                SendMail mail = new SendMail();
                mail.CDATE = FormatHelper.TODateInt(DateTime.Now);
                mail.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                mail.CUSER = user;
                mail.MAILTYPE = MailName.IQCSQERejectMail;
                mail.Recipients = _WarehouseFacade.Recipients(mail.MAILTYPE);
                mail.SENDFLAG = "N";
                string mdesc = string.Empty;
                BenQGuru.eMES.Domain.MOModel.Material m = _WarehouseFacade.GetMaterialFromDQMCode(iqc.DQMCode);
                if (m != null)
                    mdesc = m.MchshortDesc;
                string content = string.Format("IQC检验单号:{0}，ASN单号:{1}，SAP单号:{2}，供应商代码{3}，供应商名称{4}，鼎桥编码:{5}，鼎桥物料编码描述{6}，送检日期:{7}，送检数量:{8}，缺陷品数:{9}，缺陷描述:{10}，退换货数量:{11}，现场整改数量:{12}，让步接收数量:{13}，特采数量:{14}。",
                                                iqcNo,
                                               stno,
                                                invno,
                                                asn.VendorCode,
                                                _WarehouseFacade.GetVendorName(asn.VendorCode),
                                                iqc.DQMCode,
                                                mdesc,
                                                cDate,
                                                iqcQty,
                                                ngQty,
                                                string.Join(",", ngDescs.ToArray()),
                                                returnQty,
                                                reformQty, giveQty, acceptQty
                                                );
                mail.MAILCONTENT = content;
                return mail;
            }
            return null;
        }

        public static SendMail IQCExceptionThenGenerMail(AsnIQC iqc, ASN asn, string user, IQCFacade facade, WarehouseFacade _WarehouseFacade)
        {
            AsnIQCDetailEc[] ecs = facade.GetAsnIQCDetailECs(iqc.IqcNo);
            if (ecs.Length == 0)
                return null;
            int qty = facade.GetAsnIQCDetailQty(iqc.IqcNo);
            string stno = iqc.StNo;
            string iqcNo = iqc.IqcNo;
            string invno = iqc.InvNo;
            string cDate = FormatHelper.TODateTimeString(iqc.CDate, iqc.CTime);

            int ngQty = 0;
            List<string> ecDescs = new List<string>();
            foreach (AsnIQCDetailEc ec in ecs)
            {
                ngQty += ec.NgQty;
                if (!ecDescs.Contains(ec.EcgCode))
                    ecDescs.Add(ec.EcgCode);
            }

            if (ngQty > 0)
            {
                SendMail mail = new SendMail();
                mail.CDATE = FormatHelper.TODateInt(DateTime.Now);
                mail.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                mail.CUSER = user;
                mail.MAILTYPE = MailName.IQCExceptionMail;
                mail.Recipients = _WarehouseFacade.Recipients(mail.MAILTYPE);
                mail.SENDFLAG = "N";
                string mdesc = string.Empty;
                BenQGuru.eMES.Domain.MOModel.Material m = _WarehouseFacade.GetMaterialFromDQMCode(iqc.DQMCode);
                if (m != null)
                    mdesc = m.MchshortDesc;
                string content = string.Format("IQC检验单号:{0}，ASN单号:{1}，SAP单号:{2}，供应商编码:{3},供应商名称:{4},库位:{5},鼎桥物料编码:{6},鼎桥物料描述:{7},状态:{8}，送检数量:{9}，缺陷品数:{10}，缺陷描述:{11}，送检日期:{12}。",
                                                iqcNo,
                                               stno,
                                                invno,
                                                asn.VendorCode,
                                                _WarehouseFacade.GetVendorName(asn.VendorCode),
                                                asn.StorageCode,
                                                iqc.DQMCode,
                                                mdesc,
                                                iqc.Status,
                                                qty,
                                                ngQty,
                                                string.Join(",", ecDescs.ToArray()),
                                                cDate
                                                );
                mail.MAILCONTENT = content;
                return mail;
            }
            return null;
        }


        public static SendMail OQCExceptionThenGenerMail(OQC oqc, string user, IQCFacade facade, WarehouseFacade _WarehouseFacade)
        {
            OQCDetailEC[] ecs = facade.GetOQCDetailECs(oqc.OqcNo);
            if (ecs.Length == 0)
                return null;
            int qty = facade.GetOQCDetailQty(oqc.OqcNo);
            string pickNo = oqc.PickNo;
            string oqcNo = oqc.OqcNo;

            Pick pick = (Pick)_WarehouseFacade.GetPick(pickNo);
            string invno = string.Empty;
            if (pick != null)
                invno = pick.InvNo;

            string cDate = FormatHelper.TODateTimeString(oqc.CDate, oqc.CTime);

            int ngQty = 0;
            List<string> ecDescs = new List<string>();
            List<string> dqmcodes = new List<string>();
            List<string> mdescs = new List<string>();
            foreach (OQCDetailEC ec in ecs)
            {
                ngQty += ec.NgQty;
                if (!ecDescs.Contains(ec.EcgCode))
                    ecDescs.Add(ec.EcgCode);
                if (!dqmcodes.Contains(ec.DQMCode))
                    dqmcodes.Add(ec.DQMCode);

                BenQGuru.eMES.Domain.MOModel.Material m = _WarehouseFacade.GetMaterialFromDQMCode(ec.DQMCode);
                if (!mdescs.Contains(m.MchshortDesc))
                    mdescs.Add(m.MchshortDesc);
            }

            if (ngQty > 0)
            {
                SendMail mail = new SendMail();
                mail.CDATE = FormatHelper.TODateInt(DateTime.Now);
                mail.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                mail.CUSER = user;
                mail.MAILTYPE = MailName.OQCExceptionMail;
                mail.Recipients = _WarehouseFacade.Recipients(mail.MAILTYPE);
                mail.SENDFLAG = "N";

                string content = string.Format("OQC检验单号:{0}，拣货任务令号:{1}，SAP单号:{2}，库位:{3},状态:{4}，鼎桥物料编码:{5},鼎桥物料描述:{6},送检数量:{7}，缺陷品数:{8}，缺陷描述:{9}，送检日期:{10}。",
                                                oqcNo,
                                               pickNo,
                                                invno,
                                                pick.StorageCode,

                                                oqc.Status,
                                                string.Join("#", dqmcodes.ToArray()),
                                                string.Join("#", mdescs.ToArray()),
                                                qty,
                                                ngQty,
                                                string.Join(",", ecDescs.ToArray()),
                                                cDate
                                                );
                mail.MAILCONTENT = content;
                return mail;
            }
            return null;
        }

        public static SendMail OQCRejectThenGenerMail(OQC oqc, Pick pick, string user, WarehouseFacade _WarehouseFacade, IQCFacade iqcFacade)
        {

            OQCDetail[] details = iqcFacade.GetOQCDetails(oqc.OqcNo);

            if (details.Length == 0)
                return null;
            string pickNo = oqc.PickNo;
            string oqcNo = oqc.OqcNo;
            string invno = pick.InvNo;
            string cDate = FormatHelper.TODateTimeString(oqc.CDate, oqc.CTime);
            int oqcQty = 0;
            int ngQty = 0;
            int returnQty = 0;
            foreach (OQCDetail oqcDetail in details)
            {
                ngQty += oqcDetail.NgQty;
                oqcQty += oqcDetail.Qty;
                returnQty += oqcDetail.ReturnQty;
            }

            OQCDetailEC[] ecs = iqcFacade.GetOQCDetailReturnECs(oqc.OqcNo);
            List<string> ngDescs = new List<string>();
            foreach (OQCDetailEC ec in ecs)
            {
                string desc = ec.EcgCode + "-" + ec.ECode;
                if (!ngDescs.Contains(desc))
                    ngDescs.Add(desc);

            }

            if (returnQty > 0)
            {

                SendMail mail = new SendMail();
                mail.CDATE = FormatHelper.TODateInt(DateTime.Now);
                mail.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                mail.CUSER = user;
                mail.MAILTYPE = MailName.OQCSQERejectMail;
                mail.Recipients = _WarehouseFacade.Recipients(mail.MAILTYPE);
                mail.SENDFLAG = "N";
                string content = "OQC检验单号:{0}，拣货任务令号:{1}，SAP单号:{2}，状态:{3}，送检数量:{4}，缺陷品数:{5}，退换货数量:{6},缺陷描述:{7}，送检日期:{8}。";
                content = string.Format(content, oqcNo, pickNo, invno, "OQC检验中", oqcQty, ngQty, returnQty, string.Join(",", ngDescs.ToArray()), cDate);
                mail.MAILCONTENT = content;
                return mail;

            }
            return null;
        }

        private static SendMail ReceiveRejectThenGenerMail(List<ASNDetail> ads, string user, ASN asn, WarehouseFacade _WarehouseFacade)
        {
            Dictionary<string, MailData> mailDatas = new Dictionary<string, MailData>();
            foreach (ASNDetail ad in ads)
            {
                if (ad.InitReceiveStatus == SAP_LineStatus.SAP_LINE_REJECT)
                {
                    if (mailDatas.ContainsKey(ad.DQMCode))
                        mailDatas[ad.DQMCode].RejectCountByDQMCode += ad.Qty;
                    else
                    {
                        mailDatas.Add(ad.DQMCode, new MailData());
                        mailDatas[ad.DQMCode].DQMCode = ad.DQMCode;
                        mailDatas[ad.DQMCode].RejectCountByDQMCode += ad.Qty;

                    }
                    mailDatas[ad.DQMCode].RejectBoxs++;
                }
            }
            if (mailDatas.Keys.Count == 0)
                return null;
            int boxes = 0;
            foreach (string key in mailDatas.Keys)
                boxes += mailDatas[key].RejectBoxs;



            string mailBody = MailBody(ads, asn, mailDatas, boxes, _WarehouseFacade);
            SendMail mail = new SendMail();
            mail.CDATE = FormatHelper.TODateInt(DateTime.Now);
            mail.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
            mail.CUSER = user;
            mail.MAILCONTENT = mailBody;
            mail.MAILTYPE = MailName.ReceiveRejectMail;
            mail.Recipients = _WarehouseFacade.Recipients(mail.MAILTYPE);
            mail.SENDFLAG = "N";

            return mail;

        }


        private static List<SendMail> ToMailBlocks(StringBuilder sb, string mailName, WarehouseFacade _WarehouseFacade)
        {
            string mailBody = sb.ToString();
            List<string> mailBodyPartys = new List<string>();

            if (mailBody.Length > 0)
            {
                while (mailBody.Length > 1000)
                {
                    mailBodyPartys.Add(mailBody.Substring(0, 1000));
                    mailBody = mailBody.Substring(1000);
                }

                if (mailBody.Length > 0 && mailBody.Length <= 1000)
                    mailBodyPartys.Add(mailBody);

            }
            string blockName = DateTime.Now.ToString("yyMMddHHmmss");
            List<SendMail> mails = new List<SendMail>();
            if (mailBodyPartys.Count > 0)
            {
                int index = 1;
                foreach (string mailBodyParty in mailBodyPartys)
                {
                    SendMail mail = new SendMail();
                    mail.BLOCKNAME = blockName;
                    mail.BLOCKINX = index;
                    mail.MAILCONTENT = mailBody;
                    mail.CDATE = FormatHelper.TODateInt(DateTime.Now);
                    mail.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                    mail.CUSER = "JOB";
                    mail.MAILCONTENT = mailBodyParty;
                    mail.MAILTYPE = mailName;
                    mail.Recipients = _WarehouseFacade.Recipients(mail.MAILTYPE);
                    mail.SENDFLAG = "N";
                    index++;
                    mails.Add(mail);
                }
            }
            return mails;
        }

        public static List<SendMail> IQCSQEFinishThenGenerMail(MailIQCEc[] mailecs, ASN asn, string user, string iqcCUser, SystemSettingFacade _SystemSettingFacade, WarehouseFacade _WarehouseFacade)
        {
            string temple = @"IQC检验单号:{0}、箱号:{1}、供应商代码{2}、供应商名称{3}、库位{4}、鼎桥编码:{5}、鼎桥物料编码描述{6}、供应商物料编码:{7}、鼎桥SN:{8}、缺陷品数量:{9}、缺陷类型:{10}、缺陷描述:{11}、检验提交人:{12}、SQE判定人:{13}、判定结果:{14}、备注:{15}。\r\n";
            StringBuilder sb = new StringBuilder(2000);
            foreach (MailIQCEc ec in mailecs)
            {
                string mdesc = string.Empty;
                BenQGuru.eMES.Domain.MOModel.Material m = _WarehouseFacade.GetMaterialFromDQMCode(ec.DQMCODE);
                if (m != null)
                    mdesc = m.MchshortDesc;
                sb.Append(string.Format(temple, ec.IqcNo, ec.CartonNo, asn.VendorCode, _WarehouseFacade.GetVendorName(asn.VendorCode), asn.StorageCode, ec.DQMCODE, mdesc, ec.VENDORMCODE, ec.SN, ec.NgQty, ec.EcgCode, ec.ECode, iqcCUser, user, GetSQEStatusName(ec.SqeStatus, _SystemSettingFacade), ec.Remark1));
            }


            List<SendMail> mails = ToMailBlocks(sb, MailName.IQCSQEMail, _WarehouseFacade);

            return mails;




        }

        public static SendMail OQCSQEFinishThenGenerMail(MailOQCEc[] mailecs, Pick pick, string user, string oqcCUser, SystemSettingFacade _SystemSettingFacade, WarehouseFacade _WarehouseFacade)
        {
            string temple = "箱号:{0}、库位{1}、鼎桥编码:{1}、鼎桥物料描述{2}、供应商物料编码:{2}、鼎桥SN:{3}、缺陷品数量:{4}、缺陷类型:{5}、缺陷描述:{6}、检验提交人:{7}、SQE判定人:{8}、判定结果:{9}、备注:{10}。\r\n";
            StringBuilder sb = new StringBuilder(2000);
            foreach (MailOQCEc ec in mailecs)
            {
                string mdesc = string.Empty;
                BenQGuru.eMES.Domain.MOModel.Material m = _WarehouseFacade.GetMaterialFromDQMCode(ec.DQMCode);
                if (m != null)
                    mdesc = m.MchshortDesc;
                sb.Append(string.Format(temple, ec.CartonNo, pick.StorageCode, ec.DQMCode, mdesc, ec.VENDERITEMCODE, ec.SN, ec.NgQty, ec.EcgCode, ec.ECode, oqcCUser, user, GetSQEStatusName(ec.SqeStatus, _SystemSettingFacade), ec.Remark1));
            }

            string content = sb.ToString();
            if (!string.IsNullOrEmpty(content))
            {
                SendMail mail = new SendMail();
                mail.CDATE = FormatHelper.TODateInt(DateTime.Now);
                mail.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                mail.CUSER = user;
                mail.MAILCONTENT = content;
                mail.MAILTYPE = MailName.OQCSQEMail;
                mail.Recipients = _WarehouseFacade.Recipients(mail.MAILTYPE);
                mail.SENDFLAG = "N";
                return mail;
            }
            return null;
        }

        public static string GetSQEStatusName(string parameterCode, SystemSettingFacade _SystemSettingFacade)
        {
            BenQGuru.eMES.Domain.BaseSetting.Parameter parameter = (BenQGuru.eMES.Domain.BaseSetting.Parameter)_SystemSettingFacade.GetParameter(parameterCode.ToUpper(), "SQESTATUS");
            if (parameter != null)
            {
                return parameter.ParameterDescription;
            }
            return parameterCode;
        }

        private static string MailBody(List<ASNDetail> ads, ASN asn, Dictionary<string, MailData> mailDatas, int boxes, WarehouseFacade _WarehouseFacade)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append("ASN单号:");
            sb.Append(asn.StNo);
            sb.Append(",");
            sb.Append("SAP单号:");
            sb.Append(asn.InvNo);
            sb.Append(",");
            sb.Append("供应商代码:");
            sb.Append(asn.VendorCode);
            sb.Append(",");
            sb.Append("供应商名称:");
            sb.Append(_WarehouseFacade.GetVendorName(asn.VendorCode));
            sb.Append(",");
            sb.Append("库位:");
            sb.Append(asn.StorageCode);
            sb.Append(",");
            sb.Append("状态:");
            sb.Append("到货初检中");
            sb.Append(",");

            sb.Append("入库指令创建时间:");
            sb.Append(FormatHelper.TODateTimeString(asn.CDate, asn.CTime));
            sb.Append(",");

            sb.Append("初检拒收箱数:");
            sb.Append(boxes);
            sb.Append(",");

            List<string> descs = new List<string>();


            foreach (ASNDetail dd in ads)
            {
                string desc = _WarehouseFacade.GetRejectDESC(dd.InitReceiveDesc);
                if (!string.IsNullOrEmpty(desc))
                {
                    if (!descs.Contains(desc))
                        descs.Add(desc);
                }
            }

            sb.Append("初检拒收原因:");
            sb.Append(string.Join(",", descs.ToArray()));
            sb.Append(@"\r\n");
            foreach (string dqmCode in mailDatas.Keys)
            {


                sb.Append("初检拒收物料编码:");
                sb.Append(dqmCode);
                sb.Append(",");
                sb.Append("初检拒收数量:");
                sb.Append(mailDatas[dqmCode].RejectCountByDQMCode);
                sb.Append(@"\r\n");


            }
            return sb.ToString();
        }

        private void PostToSap(string usr, IDomainDataProvider DataProvider, ASN asn, out List<PoLog> logs, bool hasDetail)
        {
            try
            {
                logs = new List<PoLog>();
                if (!hasDetail && asn.StType == SAP_ImportType.SAP_POR)
                {
                    int poNum = 0;
                    string message = string.Empty;





                    bool result = PoToSap(DataProvider, new string[] { asn.StNo }, usr, out poNum, out message, out logs);

                    if (!result)
                    {

                        string errorMsg = "回写失败-" + message;


                        throw new Exception(errorMsg);
                    }


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string ReceiveCancelCartonno(string stno, List<ASNDetail> ads, WarehouseFacade facade, InventoryFacade _InventoryFacade, IDomainDataProvider DataProvider)
        {
            ASN asn = (ASN)_InventoryFacade.GetASN(stno);
            if (asn.Status != ASNHeadStatus.Receive)
                return "状态必须是到货初检中！";
            if (ads == null || ads.Count == 0)
                return stno + "项为空！";
            try
            {
                DataProvider.BeginTransaction();
                foreach (ASNDetail ad in ads)
                {
                    ASNDetail d = (ASNDetail)_InventoryFacade.GetASNDetail(int.Parse(ad.StLine), stno);
                    d.CartonNo = string.Empty;
                    _InventoryFacade.UpdateASNDetail(d);
                    object[] objs = facade.GetASNDetailSNbyStnoandStline(d.StNo, d.StLine);
                    if (objs != null)
                    {
                        for (int i = 0; i < objs.Length; i++)
                        {
                            Asndetailsn a_sn = objs[i] as Asndetailsn;
                            a_sn.Cartonno = string.Empty;
                            facade.UpdateAsndetailsn(a_sn);
                        }
                    }
                }
                DataProvider.CommitTransaction();
                return "清除箱号成功！";
            }
            catch (Exception ex)
            {
                DataProvider.RollbackTransaction();
                throw ex;
            }
        }


        private bool PoToSap(IDomainDataProvider DataProvider, string[] asnDetailList, string usr, out  int poNum, out string message, out List<PoLog> logs)
        {

            InventoryFacade _InventoryFacade = new InventoryFacade(DataProvider);

            WarehouseFacade _WarehouseFacade = new WarehouseFacade(DataProvider);
            poNum = 0;
            message = string.Empty;
            logs = new List<PoLog>();
            List<PO> list = new List<PO>();
            string stno = string.Empty;
            if (asnDetailList != null)
            {
                stno = asnDetailList[0];
            }
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(DataProvider);
            int count = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime);
            ASN asn = (ASN)_InventoryFacade.GetASN(stno);
            #region check
            bool hasDetail = _InventoryFacade.CheckASNHasDetail(asn.StNo, ASNLineStatus.ReceiveClose);
            if (hasDetail)
            {
                message = "检查STNO下所有的行的状态必须是全部是初检完成";

                return false;
            }
            #endregion
            #region 初检103
            object[] invoicesDetaillist = _InventoryFacade.GetInvoicesDetailByInvNoAndStno1(asn.InvNo, stno);//GetInvoicesDetailByInvNo

            #region tblpolog
            foreach (InvoicesDetail detail in invoicesDetaillist)
            {

                //i.	查找每个项目行的就收数量（select sum（ReceiveQTY） from tblasndetailitem where stno=‘***’and invno=‘***’and invline=‘***’） 
                int receiveQty = _InventoryFacade.GetReceiveQtyInAsn(stno, detail.InvNo, detail.InvLine);
                BenQGuru.eMES.Domain.MOModel.Material material = (BenQGuru.eMES.Domain.MOModel.Material)_InventoryFacade.GetMaterialByDQMCode(detail.DQMCode);
                //_WarehouseFacade.GetSTLineInPOIntridution(detail.InvNo, detail.InvLine.ToString(), stno);
                #region 回传
                PoLog poLog = new PoLog();
                int serial = _InventoryFacade.GetMaxSerialInPoLog() + 1;
                poLog.Serial = serial;
                poLog.ZNUMBER = poLog.Serial.ToString();
                poLog.SerialNO = asn.StNo;
                poLog.PONO = detail.InvNo;
                int invLine = detail.InvLine;
                poLog.PoLine = invLine.ToString();
                poLog.FacCode = asn.FacCode;
                poLog.MCode = detail.MCode;//SAPMcode
                poLog.Qty = receiveQty;//初检 接收数量
                if (material != null)
                {
                    poLog.Unit = material.Muom;//asndetailObj.Unit;
                }
                poLog.Status = "103";//接收
                poLog.SAPMaterialInvoice = "";
                poLog.Operator = usr;// asndetail.;
                poLog.VendorInvoice = asn.InvNo;
                poLog.StorageCode = asn.StorageCode;
                poLog.Remark = asn.Remark1;
                poLog.InvoiceDate = asn.MaintainDate;
                poLog.SapDateStamp = dbTime.DBDate;
                poLog.SapTimeStamp = dbTime.DBTime;
                if (count > 0)//P回传
                {
                    poLog.IsPBack = "";
                }
                else
                {
                    poLog.IsPBack = "Actual";
                }
                poNum += receiveQty;

                logs.Add(poLog);


                PO po = new PO();
                po.PONO = poLog.PONO;
                po.POLine = invLine;
                po.FacCode = poLog.FacCode;
                po.SerialNO = poLog.SerialNO;// asndetail.s;
                po.MCode = poLog.MCode;//SAPMcode
                po.Qty = receiveQty;//初检 接收数量
                po.Unit = poLog.Unit;//asndetailObj.Unit;
                po.Status = poLog.Status;//接收
                po.SAPMaterialInvoice = "";
                po.Operator = poLog.Operator;// asndetail.;
                po.VendorInvoice = poLog.VendorInvoice;
                po.StorageCode = poLog.StorageCode;
                po.Remark = poLog.Remark;
                po.InvoiceDate = poLog.InvoiceDate;
                po.ZNUMBER = poLog.ZNUMBER;

                list.Add(po);

                #endregion
            }
            #endregion

            #region POToSAP
            BenQGuru.eMES.SAPRFCService.POToSAP poToSAP = new BenQGuru.eMES.SAPRFCService.POToSAP(DataProvider);


            #endregion

            #region add by sam
            bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;

            #endregion

            if (is2Sap)
            {
                LogPO2Sap(dbTime, list, _InventoryFacade);
            }
            else
            {
                #region 回传

                try
                {
                    SAPRfcReturn msg = poToSAP.POReceiveToSAP(list);
                    //Log.Error(msg.Result);
                    foreach (PoLog log in logs)
                    {

                        if (count > 0) //P回传
                        {
                            log.SAPMaterialInvoice = ""; //初检时放，P就为空
                            log.SapReturn = "";
                        }
                        else
                        {
                            log.SAPMaterialInvoice = msg.MaterialDocument; //
                            log.SapReturn = msg.Result; // msg.Result;//(S表示成功，E表示失败)
                            log.Message = msg.Message;
                        }
                        _InventoryFacade.AddPoLog(log);
                    }

                    if (msg.Result.ToUpper() == "E")
                    {
                        string Mess = msg.Message;


                        message = Mess;


                        return false;
                    }


                }
                catch (Exception ex)
                {
                    //throw ex;
                    throw ex;
                }

                #endregion
            }

            PoToSupport(list, false);

            #endregion
            return true;
        }


        private void LogPO2Sap(DBDateTime dbTime, List<BenQGuru.eMES.SAPRFCService.Domain.PO> dns, InventoryFacade _InventoryFacade)
        {


            foreach (BenQGuru.eMES.SAPRFCService.Domain.PO po in dns)
            {

                Po2Sap poLog = new Po2Sap();
                poLog.PONO = po.PONO;
                poLog.POLine = po.POLine;
                poLog.SerialNO = po.SerialNO;
                poLog.Qty = po.Qty; // 
                poLog.Unit = po.Unit;
                poLog.FacCode = po.FacCode;
                poLog.InvoiceDate = po.InvoiceDate; //  
                poLog.MCode = po.MCode;//SAPMcode
                poLog.SAPMaterialInvoice = po.SAPMaterialInvoice;
                poLog.Operator = po.Operator;
                poLog.Status = po.Status;
                poLog.VendorInvoice = po.VendorInvoice;
                poLog.StorageCode = po.StorageCode;
                poLog.Remark = po.Remark;
                poLog.SapDateStamp = dbTime.DBDate;
                poLog.SapTimeStamp = dbTime.DBTime;
                poLog.SapReturn = string.Empty;
                poLog.STNO = po.SerialNO;
                poLog.ZNUMBER = po.ZNUMBER;
                //poLog.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                //poLog.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                //poLog.MaintainUser = GetUserCode();
                //poLog.r = "empty";
                //poLog.Message = "empty";
                _InventoryFacade.AddPo2Sap(poLog);
            }
        }


        public bool OnShelf(string cartonNo, string locationNo, List<Asndetail> details, IDomainDataProvider DataProvider, out string message, string usr)
        {
            message = string.Empty;
            if (details.Count == 0)
            {
                message = "上架条目为空！";
                return false;
            }

            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(DataProvider);

            bool isReceive = false;
            //List<Asndetail> asnDetailList = new List<Asndetail>();//add by sam
            foreach (Asndetail d in details)
            {
                if (!CheckDataStatus(d.Cartonno, locationNo, out message, DataProvider))
                    return false;
            }

            WarehouseFacade facade = new WarehouseFacade(DataProvider);
            InventoryFacade _Invenfacade = new InventoryFacade(DataProvider);
            try
            {
                foreach (Asndetail d in details)
                {
                    if (d.Status == ASNDetail_STATUS.ASNDetail_Close)
                    {
                        message = "不能重复上架";
                        return false;
                    }
                    d.LocationCode1 = locationNo;
                }
                bool canSend = true;
                string StNo = details[0].Stno;


                object asnobj1 = facade.GetAsn(StNo);

                Asn asn = (Asn)asnobj1;


                if (asn == null)
                    throw new Exception(StNo + "不存在！");

                if ((asn.StType == InInvType.PD) || (asn.StType == InInvType.POR && asn.Direct_flag == "Y") || (asn.StType == InInvType.SCTR && asn.Rejects_flag == "Y"))
                {
                    foreach (Asndetail asndetail in details)
                    {
                        if (asndetail.Status != "ReceiveClose")
                        {
                            message = asndetail.Cartonno + "状态必须是初检完成！";
                            return false;
                        }
                    }
                }
                else
                {
                    foreach (Asndetail asndetail in details)
                    {
                        AsnIQC iqc = _Invenfacade.QueryAsnIqc(asndetail.DqmCode, asndetail.Stno);

                        if (iqc == null)
                        {
                            message = asndetail.DqmCode + "没有对应的IQC检验数据！";
                            return false;
                        }
                        if (iqc.Status != "IQCClose")
                        {
                            message = asndetail.Cartonno + "状态必须是IQC完成！";
                            return false;
                        }


                    }

                }

                //if (asn.Status != ASN_STATUS.ASN_OnLocation)
                //{
                //    message = asn.Stno + "状态必须是上架中！";
                //    return false;

                //}
                foreach (Asndetail asndetail in details)
                {
                    if (asn.StType == SAP_ImportType.SAP_PD || asn.Rejects_flag.ToUpper() == "Y")
                    {
                        asndetail.ActQty = asndetail.ReceiveQty;
                    }
                    else
                    {
                        asndetail.ActQty = asndetail.QcpassQty;
                    }

                }

                isReceive = true;
                if (isReceive && asn.StType == SAP_ImportType.SAP_POR)
                {
                    bool result = PoToSap(details.ToArray(), _Invenfacade, facade, dbTime, DataProvider, out message, usr);//asnDetailList.Distinct<string>().ToArray());
                    if (!result)
                        return false;


                }
                if (isReceive && asn.StType == SAP_ImportType.SAP_YFR) //研发入库 
                {
                    bool result = RSToSAP(details.ToArray(), _Invenfacade, facade, dbTime, DataProvider, usr, out message);
                    if (!result)
                        return false;

                }
                if (isReceive && asn.StType == SAP_ImportType.SAP_DNR)  //退货入库
                {
                    Asndetail[] asndetails = facade.GetAsnDetails(((Asndetail)details[0]).Stno);
                    foreach (Asndetail asnDetail in asndetails)
                    {
                        if (string.IsNullOrEmpty(asnDetail.LocationCode1))
                        {
                            foreach (Asndetail asndd in details)
                            {
                                if (asndd.Stline == asnDetail.Stline && asndd.Stno == asnDetail.Stno)
                                {
                                    asnDetail.LocationCode1 = asndd.LocationCode1;
                                }

                            }
                        }
                    }

                    foreach (Asndetail asnDetail in asndetails)
                    {
                        Log.Error("2------------------------------------" + asnDetail.Stno + ":" + asnDetail.Stline + ":" + asnDetail.LocationCode1);
                        if (string.IsNullOrEmpty(asnDetail.LocationCode1) && asnDetail.Status == "IQCClose")
                            canSend = false;
                    }

                    if (canSend)
                    {
                        bool result = DNToSAP(details.ToArray(), _Invenfacade, facade, dbTime, DataProvider, usr, out message);//asnDetailList.Distinct<string>().ToArray());
                        if (!result)
                            return false;

                    }

                    foreach (Asndetail ad in asndetails)
                    {
                        facade.UpdateAsndetail(ad);
                    }


                }
                if (isReceive && asn.StType == SAP_ImportType.SAP_PGIR)  //PGI退回入库
                {

                    Asndetail[] asndetails = facade.GetAsnDetails(((Asndetail)details[0]).Stno);

                    foreach (Asndetail asnDetail in asndetails)
                    {
                        if (string.IsNullOrEmpty(asnDetail.LocationCode1))
                        {
                            foreach (Asndetail asndd in details)
                            {
                                if (asndd.Stline == asnDetail.Stline && asndd.Stno == asnDetail.Stno)
                                {
                                    asnDetail.LocationCode1 = asndd.LocationCode1;
                                }
                            }
                        }
                    }
                    foreach (Asndetail asnDetail in asndetails)
                    {
                        if (string.IsNullOrEmpty(asnDetail.LocationCode1))
                            canSend = false;
                    }
                    if (canSend)
                    {

                        bool result = DNRePGIToSAP(asndetails, asn, _Invenfacade, facade, dbTime, DataProvider, out message, usr);
                        if (!result)
                            return false;

                    }

                    foreach (Asndetail ad in asndetails)
                    {
                        facade.UpdateAsndetail(ad);
                    }

                }
                if (isReceive && asn.StType == SAP_ImportType.SAP_SCTR)  //生产退料入库 
                {
                    bool result = WWPO2SAP(details.ToArray(), _Invenfacade, facade, dbTime, DataProvider, out message, usr);
                    if (!result)
                        return false;

                }
                if (isReceive && (asn.StType == SAP_ImportType.SAP_UB || asn.StType == SAP_ImportType.SAP_JCR || asn.StType == SAP_ImportType.SAP_CAR || asn.StType == SAP_ImportType.SAP_BLR))
                {
                    //PO入库         POR          POToSAP
                    // 研发入库        YFR           RSToSAP 
                    // 调拨入库        UB           UBToSAP
                    // 检测返工入库    JCR          UBToSAP*2
                    // Claim入库      CAR          UBToSAP*2
                    // 不良品入库      BLR		  UBToSAP*2
                    // 退货入库        DNR          DNToSAP
                    // 生产退料入库                  WWPO2SAP
                    // PGI退回入库    PGIR	       DNRePGIToSAP
                    if (asn.StType == SAP_ImportType.SAP_JCR || asn.StType == SAP_ImportType.SAP_CAR || asn.StType == SAP_ImportType.SAP_BLR)
                    {
                        bool result1 = UBToSAP(details.ToArray(), false, "351", _Invenfacade, facade, dbTime, DataProvider, out message, usr);
                        if (!result1)
                            return false;


                    }

                    bool result = UBToSAP(details.ToArray(), true, "101", _Invenfacade, facade, dbTime, DataProvider, out message, usr);//asnDetailList.Distinct<string>().ToArray());
                    if (!result)
                        return false;

                }

                if (asn.StType != SAP_ImportType.SAP_PGIR && asn.StType != SAP_ImportType.SAP_DNR)
                {

                    if (OnShelves(details.ToArray(), dbTime, _Invenfacade, facade, DataProvider, out message, usr, locationNo))
                    {
                        message = "上架成功！";
                        return true;
                    }
                }
                else
                {
                    if (canSend)
                    {
                        Asndetail[] asndetails = facade.GetIQCCloseAsnDetails(((Asndetail)details[0]).Stno);
                        if (OnShelves(asndetails, dbTime, _Invenfacade, facade, DataProvider, out message, usr, locationNo))
                        {
                            message = "上架成功！";
                            return true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //DataProvider.RollbackTransaction();
                Log.Error(ex.StackTrace);
                message = "上架出错:" + ex.Message;
                return false;

            }
            message = "上架失败！ " + message;
            return false;


        }


        private bool OnShelves(Asndetail[] asnds, DBDateTime dbTime, InventoryFacade _InventoryFacade, WarehouseFacade facade, IDomainDataProvider DataProvider, out string message, string usr, string locationNo)
        {

            object asnobj1 = facade.GetAsn(asnds[0].Stno);
            message = string.Empty;
            if (asnobj1 == null)
            {
                message = "入库指令号不存在！";
                return false;
            }

            Asn asn = asnobj1 as Asn;

            #region add by sam



            #endregion

            try
            {
                DataProvider.BeginTransaction();
                foreach (Asndetail asndetail in asnds)
                {
                    if (asn.StType == SAP_ImportType.SAP_PD || asn.Rejects_flag.ToUpper() == "Y")
                    {
                        asndetail.ActQty = asndetail.ReceiveQty;
                    }
                    else
                    {
                        asndetail.ActQty = asndetail.QcpassQty;
                    }
                    asndetail.Status = ASNDetail_STATUS.ASNDetail_Close;
                    asndetail.MaintainDate = dbTime.DBDate;
                    asndetail.MaintainTime = dbTime.DBTime;
                    asndetail.MaintainUser = usr;
                    //if (asn.StType == SAP_ImportType.SAP_POR || asn.StType == SAP_ImportType.SAP_YFR || asn.StType == SAP_ImportType.SAP_PD)
                    //{
                    //    //asndetail.StorageageDate = dbTime.DBDate.ToString();
                    //    asndetail.StorageageDate = notUBStorageAgeDate;

                    //}
                    facade.UpdateAsndetail(asndetail);

                    #region 更新asndetailitem表 更新 actqty 和更新invoicedetail表，更新actqty
                    object[] itemobjs = facade.GetASNDetailItembyStnoAndStline(asndetail.Stno, asndetail.Stline.ToString());
                    if (itemobjs != null)
                    {
                        for (int i = 0; i < itemobjs.Length; i++)
                        {
                            Asndetailitem asnitem = itemobjs[i] as Asndetailitem;
                            if (asn.StType == SAP_ImportType.SAP_PD || asn.Rejects_flag.ToUpper() == "Y")
                            {
                                asnitem.ActQty = (int)asnitem.ReceiveQty;
                            }
                            else
                            {
                                asnitem.ActQty = (int)asnitem.QcpassQty;
                            }
                            //asnitem.ActQty = asnitem.QcpassQty;
                            asnitem.MaintainDate = dbTime.DBDate;
                            asnitem.MaintainTime = dbTime.DBTime;
                            asnitem.MaintainUser = usr;
                            facade.UpdateAsndetailitem(asnitem);
                            if (asn.StType != SAP_ImportType.SAP_PGIR && asn.StType != SAP_ImportType.SAP_SCTR)
                            {
                                object invoiobj = _InventoryFacade.GetInvoicesDetail(asnitem.Invno, int.Parse(asnitem.Invline));
                                InvoicesDetail inv = invoiobj as InvoicesDetail;
                                if (asn.StType == SAP_ImportType.SAP_PD || asn.Rejects_flag.ToUpper() == "Y")
                                {
                                    inv.ActQty += (int)asnitem.ReceiveQty;
                                }
                                else
                                {
                                    inv.ActQty += (int)asnitem.QcpassQty;
                                }
                                //inv.ActQty +=Convert.ToInt32(asnitem.QcpassQty);
                                inv.MaintainDate = dbTime.DBDate;
                                inv.MaintainTime = dbTime.DBTime;
                                inv.MaintainUser = usr;

                                _InventoryFacade.UpdateInvoicesDetail(inv);
                            }
                        }
                    }
                    #endregion

                    //新增数据tblstoragedetail
                    object asnobj = facade.GetAsn(asndetail.Stno);


                    asn = asnobj as Asn;
                    #region 对于不良品入库，先删除不良品库的库存
                    if (asn.StType == SAP_ImportType.SAP_JCR || asn.StType == SAP_ImportType.SAP_CAR || asn.StType == SAP_ImportType.SAP_BLR)
                    {
                        object objs_item = _InventoryFacade.GetACTQTYFromASNDetailItem(asndetail.Stno, asndetail.Stline);
                        if (objs_item == null)
                        {
                            DataProvider.RollbackTransaction();
                            message = "asnDetailItem中没有数据";
                            return false;
                        }
                        Asndetailitem item = objs_item as Asndetailitem;
                        //库存有sn就删除

                        InvoicesDetail invD = (InvoicesDetail)_InventoryFacade.GetInvoicesDetail(asn.Invno);
                        object[] objs_sto = facade.GetStorageDetailByDQMCodeAndStorageCode(asndetail.DqmCode, invD.FromStorageCode);
                        List<string> cartonnos = new List<string>();
                        if (objs_sto == null)
                        {
                            DataProvider.RollbackTransaction();
                            message = asn.FromstorageCode + "库位没有库存";
                            return false;
                        }
                        else
                        {
                            decimal needQty = item.ActQty;
                            int sub = 0;

                            foreach (StorageDetail sto in objs_sto)
                            {
                                cartonnos.Add(sto.CartonNo);

                                sub = sto.AvailableQty;
                                if (sub > (int)needQty)
                                {
                                    sub = sto.AvailableQty - (int)needQty;
                                    if (sub > 0)
                                    {

                                        sto.AvailableQty = sub;
                                        sto.StorageQty = sto.StorageQty - (int)needQty;
                                        _InventoryFacade.UpdateStorageDetail(sto);
                                    }
                                    else
                                    {
                                        _InventoryFacade.DeleteStorageDetail(sto);
                                    }

                                    break;
                                }
                                else
                                {
                                    needQty = needQty - sub;
                                    _InventoryFacade.DeleteStorageDetail(sto);
                                }
                            }
                        }
                        BenQGuru.eMES.Domain.MOModel.Material mmm = facade.GetMaterialFromDQMCode(asndetail.DqmCode);
                        if (mmm.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                        {
                            Asndetailsn[] snss = facade.GetAsnDetailSNs(asndetail.Stno, asndetail.Stline);
                            List<Asndetailsn> newSns = new List<Asndetailsn>();
                            foreach (Asndetailsn snn in snss)
                            {
                                object ooo = _InventoryFacade.GetStorageDetailSN(snn.Sn);
                                if (ooo != null)
                                {
                                    StorageDetailSN sdn = (StorageDetailSN)ooo;
                                    sdn.CartonNo = asndetail.Cartonno;
                                    sdn.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                                    sdn.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                                    sdn.MaintainUser = usr;
                                    _InventoryFacade.UpdateStorageDetailSN(sdn);
                                }
                                else
                                {
                                    newSns.Add(snn);
                                }

                            }

                            foreach (Asndetailsn sn in newSns)
                            {
                                //if (!RandDeteleStorageDetailSn(cartonnos, _InventoryFacade, facade))
                                //{
                                //    DataProvider.RollbackTransaction();
                                //    message = "箱" + string.Join(",", cartonnos.ToArray()) + " 不存在SN";
                                //    return false;
                                //}

                                RandDeteleStorageDetailSn(cartonnos, _InventoryFacade, facade);
                                StorageDetailSN storDetailSN = _InventoryFacade.CreateNewStorageDetailSN();
                                storDetailSN.CartonNo = sn.Cartonno;
                                storDetailSN.CDate = dbTime.DBDate;
                                storDetailSN.CTime = dbTime.DBTime;
                                storDetailSN.CUser = usr;
                                storDetailSN.MaintainDate = dbTime.DBDate;
                                storDetailSN.MaintainTime = dbTime.DBTime;
                                storDetailSN.MaintainUser = usr;
                                storDetailSN.PickBlock = "N";
                                storDetailSN.SN = sn.Sn;
                                _InventoryFacade.AddStorageDetailSN(storDetailSN);
                            }
                        }
                    }
                    #endregion
                    #region 在storagedetail表中增加一条数据
                    StorageDetail stordetail = _InventoryFacade.CreateNewStorageDetail();
                    stordetail.AvailableQty = asndetail.ActQty;
                    stordetail.CartonNo = asndetail.Cartonno;
                    stordetail.CDate = dbTime.DBDate;
                    stordetail.CTime = dbTime.DBTime;
                    stordetail.CUser = usr;
                    stordetail.DQMCode = asndetail.DqmCode;
                    stordetail.FacCode = (asnobj as Asn).FacCode;
                    stordetail.FreezeQty = 0;
                    stordetail.LastStorageAgeDate = dbTime.DBDate;

                    stordetail.LocationCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(locationNo));
                    stordetail.Lotno = asndetail.Lotno;
                    stordetail.MaintainDate = dbTime.DBDate;
                    stordetail.MaintainTime = dbTime.DBTime;
                    stordetail.MaintainUser = usr;
                    stordetail.MCode = asndetail.MCode;
                    stordetail.MDesc = asndetail.MDesc;

                    stordetail.ProductionDate = asndetail.Production_Date;

                    stordetail.ReworkApplyUser = (asnobj as Asn).ReworkapplyUser;

                    if (asn.StType == SAP_ImportType.SAP_POR || asn.StType == SAP_ImportType.SAP_JCR)
                    {
                        stordetail.ValidStartDate = asndetail.Production_Date != 0 ? asndetail.Production_Date : dbTime.DBDate;
                    }




                    if (asn.StType != "UB")
                        stordetail.StorageAgeDate = asndetail.StorageageDate;
                    else
                        stordetail.StorageAgeDate = facade.GetEarliestInStorageDate(asn.Invno, "OUT", asndetail.DqmCode);
                    //add by sam


                    stordetail.StorageCode = (asnobj as Asn).StorageCode;
                    stordetail.StorageQty = asndetail.ActQty;
                    stordetail.SupplierLotNo = asndetail.Supplier_lotno;
                    stordetail.Unit = asndetail.Unit;

                    _InventoryFacade.AddStorageDetail(stordetail);
                    #endregion
                    #region 在StorageDetailSN表中增加数据
                    //新增数据tblStorageDetailSN



                    if (asn.StType != SAP_ImportType.SAP_JCR && asn.StType != SAP_ImportType.SAP_CAR && asn.StType != SAP_ImportType.SAP_BLR)
                    {
                        object[] snobj = null;
                        if (asn.StType != SAP_ImportType.SAP_SCTR)
                            snobj = facade.GetASNDetailSNbyStnoandStline1(asndetail.Stno, asndetail.Stline.ToString());
                        else
                            snobj = facade.GetASNDetailSNbyStnoandStline(asndetail.Stno, asndetail.Stline.ToString());
                        if (snobj != null)
                        {
                            //this.DataProvider.RollbackTransaction();
                            //WebInfoPublish.Publish(this, "$Error_ASNDetail_NO_DATA", this.languageComponent1);

                            //return;

                            for (int i = 0; i < snobj.Length; i++)
                            {
                                StorageDetailSN storDetailSN = _InventoryFacade.CreateNewStorageDetailSN();
                                Asndetailsn detailSN = snobj[i] as Asndetailsn;
                                storDetailSN.CartonNo = detailSN.Cartonno;
                                storDetailSN.CDate = dbTime.DBDate;
                                storDetailSN.CTime = dbTime.DBTime;
                                storDetailSN.CUser = usr;
                                storDetailSN.MaintainDate = dbTime.DBDate;
                                storDetailSN.MaintainTime = dbTime.DBTime;
                                storDetailSN.MaintainUser = usr;
                                storDetailSN.PickBlock = "N";
                                storDetailSN.SN = detailSN.Sn;

                                _InventoryFacade.AddStorageDetailSN(storDetailSN);

                            }
                        }
                    }
                    #endregion
                    #region 在invinouttrans表中增加一条数据
                    InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                    trans.CartonNO = asndetail.Cartonno;
                    trans.DqMCode = asndetail.DqmCode;
                    trans.FacCode = asn.FacCode;
                    trans.FromFacCode = asn.FromfacCode;
                    trans.FromStorageCode = asn.FromstorageCode;
                    trans.InvNO = asn.Invno;
                    trans.InvType = asn.StType;
                    trans.LotNo = asndetail.Lotno;
                    trans.MaintainDate = dbTime.DBDate;
                    trans.MaintainTime = dbTime.DBTime;
                    trans.MaintainUser = usr;
                    trans.MCode = asndetail.MCode;
                    trans.ProductionDate = asndetail.Production_Date;
                    trans.Qty = asndetail.ActQty;
                    trans.Serial = 0;
                    int stline = 0;
                    if (!int.TryParse(asndetail.Stline, out stline))
                    {

                        throw new Exception(asndetail.Stno + ":" + asndetail.Stline + "入库指令明细行号必须是数字！");
                    }

                    Asndetail asndet = (Asndetail)facade.GetAsndetail(stline, asndetail.Stno);

                    //_Invenfacade.GetASNDetail(asndetail.Stline, asndetail.Stno);


                    trans.StorageAgeDate = asndet.StorageageDate;
                    trans.StorageCode = asn.StorageCode;
                    trans.SupplierLotNo = asndetail.Supplier_lotno;
                    trans.TransNO = asn.Stno;
                    trans.TransType = "IN";
                    trans.ProcessType = "INSTORAEE";
                    trans.Unit = asndetail.Unit;
                    facade.AddInvInOutTrans(trans);
                    #endregion

                    #region  更新tblstorageinfo
                    string sumQty = facade.GetStorageQtyByMcodeAndStorageCode(stordetail.MCode, stordetail.StorageCode);
                    object stoinfo_obj = facade.GetStorageinfo(stordetail.MCode, stordetail.StorageCode);
                    if (stoinfo_obj == null)
                    {
                        StorageInfo sto_info = facade.CreateNewStorageinfo();
                        sto_info.Mcode = stordetail.MCode;
                        sto_info.Mdate = dbTime.DBDate;
                        sto_info.Mtime = dbTime.DBTime;
                        sto_info.Muser = usr;
                        sto_info.StorageCode = stordetail.StorageCode;
                        sto_info.Storageqty = Int32.Parse(sumQty);
                        facade.AddStorageinfo(sto_info);
                    }
                    else
                    {
                        StorageInfo sto_info = stoinfo_obj as StorageInfo;
                        sto_info.Storageqty = Int32.Parse(sumQty);
                        sto_info.Mdate = dbTime.DBDate;
                        sto_info.Mtime = dbTime.DBTime;
                        sto_info.Muser = usr;
                        facade.UpdateStorageinfo(sto_info);
                    }
                    #endregion

                }

                #region  上架完成后，检查这个stno在asndetail中的状态是否都是close，cancel，如果是将asn表的状态更新为close，cancel


                if (facade.IsAsnFinish(asn.Stno))
                {


                    asn.Status = ASN_STATUS.ASN_Close;
                    asn.MaintainDate = dbTime.DBDate;
                    asn.MaintainTime = dbTime.DBTime;
                    asn.MaintainUser = usr;

                    facade.UpdateAsn(asn);
                }

                #endregion
                #region  通过入库指令号，查找invno，检查actqty是否等于planqty，如果等于将finishflag=Y


                if (asn.StType != SAP_ImportType.SAP_PGIR && asn.StType != SAP_ImportType.SAP_SCTR)
                {
                    if (facade.JudgeInvoiceDetailStatus(asn.Invno))
                    {
                        object invobj = _InventoryFacade.GetInvoices(asn.Invno);
                        if (invobj == null)
                        {
                            DataProvider.RollbackTransaction();
                            message = asn.Invno + "Sap单据不存在";
                            return false;
                        }
                        Invoices inv = invobj as Invoices;
                        inv.FinishFlag = "Y";
                        inv.MaintainDate = dbTime.DBDate;
                        inv.MaintainTime = dbTime.DBTime;
                        inv.MaintainUser = usr;

                        _InventoryFacade.UpdateInvoices(inv);
                    }
                }
                #endregion

                DataProvider.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }

        }

        private bool RandDeteleStorageDetailSn(List<string> cartonnos, InventoryFacade _InventoryFacade, WarehouseFacade _WarehouseFacade)
        {
            StorageDetailSN storSN = _WarehouseFacade.GetStorageDetailSnFromCartonnos(cartonnos);
            if (storSN != null)
            {
                _InventoryFacade.DeleteStorageDetailSN(storSN);
                return true;
            }
            return false;
        }

        private bool UBToSAP(object[] asnDetailList, bool FF, string code, InventoryFacade _InventoryFacade, WarehouseFacade _WarehouseFacade, DBDateTime dbTime, IDomainDataProvider DataProvider, out string message, string usr)
        {

            bool isSuccess = true;
            message = string.Empty;
            List<string> stlines = new List<string>();
            foreach (Asndetail asndetail in asnDetailList)
            {
                stlines.Add(asndetail.Stline);
            }


            List<UB> list = new List<UB>();

            List<Ublog> logList = new List<Ublog>();

            string stno = ((Asndetail)asnDetailList[0]).Stno;
            ASN asn = (ASN)_InventoryFacade.GetASN(stno);
            object[] obj_item = _InventoryFacade.GetQcRejectQtyFromASNDetailItem1(stno, stlines);
            int count = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime);
            foreach (Asndetailitem itemNew in obj_item)
            {
                object invoicesDetaillist = _InventoryFacade.GetInvoicesDetail(asn.InvNo, int.Parse(itemNew.Invline));
                InvoicesDetail invdetail = invoicesDetaillist as InvoicesDetail;
                decimal actQTY = itemNew.ActQty;
                if (actQTY <= 0)
                    continue;


                Ublog ublog = new Ublog();
                ublog.Serial = 0;
                ublog.ContactUser = invdetail.ReceiverUser;
                ublog.FacCode = invdetail.FacCode;
                ublog.Inoutflag = code;
                ublog.MCode = invdetail.MCode;
                ublog.DocumentDate = dbTime.DBDate.ToString();
                ublog.Mestransno = asn.StNo;
                ublog.Ubline = invdetail.InvLine;
                ublog.Ubno = invdetail.InvNo;
                ublog.Qty = actQTY;
                if (FF)
                {

                    ublog.StorageCode = asn.StorageCode;
                }
                else
                {
                    ublog.StorageCode = invdetail.FromStorageCode;
                }
                ublog.Unit = invdetail.Unit;
                ublog.MaintainDate = dbTime.DBDate;
                ublog.MaintainTime = dbTime.DBTime;
                ublog.MaintainUser = usr;

                if (count > 0) //P回传
                {
                    ublog.Sapmaterialinvoice = "";
                    ublog.Ispback = " ";
                    ublog.Sapreturn = "";
                }
                else
                {
                    ublog.Ispback = "Actual";
                }
                logList.Add(ublog);

                #region 回传接口
                UB ub = new UB();
                ub.ContactUser = invdetail.ReceiverUser;
                ub.FacCode = invdetail.FacCode;
                ub.InOutFlag = code;
                ub.MCode = invdetail.MCode;
                ub.DocumentDate = dbTime.DBDate;
                ub.MesTransNO = asn.StNo;
                ub.UBLine = invdetail.InvLine;
                ub.UBNO = invdetail.InvNo;
                ub.Qty = actQTY;
                ub.StorageCode = ublog.StorageCode;

                ub.Unit = invdetail.Unit;
                list.Add(ub);
                #endregion


            }

            bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;


            if (is2Sap)
            {
                LogUB2Sap(list, usr, _InventoryFacade);
            }
            else
            {
                #region SAP


                BenQGuru.eMES.SAPRFCService.UBToSAP ubToSAP = new UBToSAP(DataProvider);
                SAPRfcReturn msg = ubToSAP.PostUBToSAP(list);

                #region 如果错了返回false

                if (msg.Result.Trim().ToUpper() == "E")
                {
                    isSuccess = false;

                    message = "SAP回写错误" + msg.Message;
                }

                foreach (Ublog ubLog in logList)
                {

                    if (count > 0) //P回传
                    {
                        ubLog.Sapmaterialinvoice = ""; //初检时放，P就为空
                        ubLog.Sapreturn = "";
                    }
                    else
                    {
                        ubLog.Sapmaterialinvoice = Getstring(msg.MaterialDocument); //
                        ubLog.Sapreturn = Getstring(msg.Result); //(S表示成功，E表示失败)
                        ubLog.Message = Getstring(msg.Message);
                    }
                    _InventoryFacade.AddUblog(ubLog);

                }

                #endregion

                if (!isSuccess)
                {
                    return false;
                }

            }

            return true;
        }

        private void LogUB2Sap(List<BenQGuru.eMES.SAPRFCService.Domain.UB> dns, string usr, InventoryFacade inventoryFacade)
        {
            foreach (BenQGuru.eMES.SAPRFCService.Domain.UB ub in dns)
            {

                Ub2Sap ubLog = new Ub2Sap();
                ubLog.Qty = ub.Qty;
                ubLog.UBNO = ub.UBNO;
                ubLog.Unit = ub.Unit;
                ubLog.UBLine = ub.UBLine;
                ubLog.InOutFlag = ub.InOutFlag;
                ubLog.MCode = ub.MCode;
                ubLog.ContactUser = ub.ContactUser;
                ubLog.DocumentDate = ub.DocumentDate;
                ubLog.FacCode = ub.FacCode;
                ubLog.MesTransNO = ub.MesTransNO;
                ubLog.StorageCode = ub.StorageCode;
                ubLog.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                ubLog.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                ubLog.MaintainUser = usr;
                inventoryFacade.AddUb2Sap(ubLog);
            }
        }


        private bool WWPO2SAP(Asndetail[] asnDetailList, InventoryFacade _InventoryFacade, WarehouseFacade _WarehouseFacade, DBDateTime dbTime, IDomainDataProvider DataProvider, out string message, string usr)
        {
            string stno = "";
            message = string.Empty;
            bool isSuccess = true;
            List<string> stlines = new List<string>();
            foreach (Asndetail asndetail in asnDetailList)
            {
                stlines.Add(asndetail.Stline);
            }
            List<WWPO> list = new List<WWPO>();

            List<Wwpolog> logList = new List<Wwpolog>();

            stno = ((Asndetail)asnDetailList[0]).Stno;
            ASN asn = (ASN)_InventoryFacade.GetASN(stno);
            object[] obj_item = _InventoryFacade.GetQcRejectQtyFromASNDetailItem1(stno, stlines);
            int count = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime);
            foreach (Asndetailitem itemNew in obj_item)
            {


                ASNDetail detail = (ASNDetail)_InventoryFacade.GetASNDetail(int.Parse(itemNew.Invline), stno);
                //object invoicesDetaillist = _InventoryFacade.GetInvoicesDetail(asn.InvNo, int.Parse(itemNew.Invline));
                //InvoicesDetail invdetail = invoicesDetaillist as InvoicesDetail;
                decimal actQTY = itemNew.ActQty;
                if (actQTY <= 0)
                    continue;



                Wwpolog wwlog = new Wwpolog();
                int serial = 0;
                wwlog.Serial = serial;
                wwlog.FacCode = "10Y2";
                wwlog.Inoutflag = "542";
                wwlog.MCode = detail.MCode;
                wwlog.MestransDate = dbTime.DBDate;
                wwlog.Mestransno = asn.StNo;
                wwlog.Poline = 0;
                wwlog.Pono = " ";
                wwlog.Qty = actQTY; // 
                wwlog.Remark = asn.Remark1;
                wwlog.Unit = detail.Unit; //asndetailObj.Unit;
                wwlog.VEndorCode = asn.VendorCode;
                wwlog.MaintainDate = dbTime.DBDate;
                wwlog.MaintainTime = dbTime.DBTime;
                wwlog.MaintainUser = usr;
                wwlog.StorageCode = asn.StorageCode;

                if (count > 0) //P回传
                {
                    wwlog.Sapmaterialinvoice = "";
                    wwlog.Ispback = " ";
                    wwlog.Sapreturn = "";
                }
                else
                {
                    wwlog.Ispback = "Actual";
                }
                logList.Add(wwlog);


                #region 回传接口

                WWPO wwpo = new WWPO();
                wwpo.FacCode = wwlog.FacCode;
                wwpo.InOutFlag = "542";
                wwpo.MCode = wwlog.MCode;
                wwpo.MesTransDate = dbTime.DBDate;
                wwpo.MesTransNO = asn.StNo;
                wwpo.POLine = wwlog.Poline;
                wwpo.PONO = wwlog.Pono;
                wwpo.Qty = actQTY;
                wwpo.Remark = asn.Remark1;
                wwpo.StorageCode = wwlog.StorageCode;
                wwpo.Unit = wwlog.Unit;
                wwpo.VendorCode = asn.VendorCode;
                list.Add(wwpo);

            }

            if (list.Count <= 0)
            {
                message = "回写的数据为空！"; ;
                return false;
            }

            bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;

            if (is2Sap)
            {
                LogWWPoSap(list, usr, _InventoryFacade);
            }
            else
            {
                #region SAP
                BenQGuru.eMES.SAPRFCService.WWPO2SAP wwPO2SAP = new WWPO2SAP(DataProvider);
                SAPRfcReturn msg = wwPO2SAP.PostWWPOToSAP(list);

                #region 如果错了返回false

                if (msg.Result.Trim().ToUpper() == "E")
                {
                    isSuccess = false;

                    message = "SAP回写错误" + msg.Message;
                }

                #endregion

                #region 写log

                foreach (Wwpolog dnLog in logList)
                {

                    if (count > 0) //P回传
                    {
                        dnLog.Sapmaterialinvoice = "";//初检时放，P就为空
                        dnLog.Sapreturn = "";
                    }
                    else
                    {
                        dnLog.Sapmaterialinvoice = Getstring(msg.MaterialDocument); //
                        dnLog.Sapreturn = Getstring(msg.Result);//(S表示成功，E表示失败)
                        dnLog.Message = Getstring(msg.Message);
                    }
                    _InventoryFacade.AddWwpolog(dnLog);

                }

                #endregion

                if (!isSuccess)
                {
                    return false;
                }
                #endregion
            }

            return true;

        }

        private void LogWWPoSap(List<BenQGuru.eMES.SAPRFCService.Domain.WWPO> dns, string usr, InventoryFacade inventoryFacade)
        {
            foreach (BenQGuru.eMES.SAPRFCService.Domain.WWPO wwPo in dns)
            {

                Wwpo2Sap wwpoLog = new Wwpo2Sap();
                wwpoLog.Qty = wwPo.Qty;
                wwpoLog.PONO = wwPo.PONO;
                wwpoLog.POLine = wwPo.POLine;
                wwpoLog.Unit = wwPo.Unit;
                wwpoLog.InOutFlag = wwPo.InOutFlag;
                wwpoLog.FacCode = wwPo.FacCode;
                wwpoLog.MCode = wwPo.MCode;
                wwpoLog.MesTransNO = wwPo.MesTransNO;
                wwpoLog.MesTransDate = wwPo.MesTransDate;
                wwpoLog.StorageCode = wwPo.StorageCode;// inv.FromStorageCode;//tblpick
                wwpoLog.VendorCode = wwPo.VendorCode;//541 为空
                wwpoLog.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                wwpoLog.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                wwpoLog.MaintainUser = usr;
                inventoryFacade.AddWwpo2Sap(wwpoLog);
            }
        }


        private bool DNRePGIToSAP(Asndetail[] asnDetailList, Asn asn, InventoryFacade _InventoryFacade, WarehouseFacade _WarehouseFacade, DBDateTime dbTime, IDomainDataProvider DataProvider, out string message, string usr)
        {

            bool isSuccess = true;
            message = string.Empty;
            List<string> dQMCodes = new List<string>();
            foreach (Asndetail asndetail in asnDetailList)
            {
                if (!dQMCodes.Contains(asndetail.DqmCode))
                {
                    dQMCodes.Add(asndetail.DqmCode);
                }
            }

            List<DN> dns = new List<DN>();
            List<Dnlog_in> logList = new List<Dnlog_in>();

            InvoicesDetailDN[] ins = _WarehouseFacade.GetDQMCodeForDN(asn.Pickno, dQMCodes);
            if (ins.Length <= 0)
            {
                throw new Exception(asn.Pickno + "拣货任务令号" + string.Join(",", dQMCodes.ToArray()) + "物料号，没有对应的出库SAP单据");
            }
            List<string> Dns = new List<string>();


            foreach (BenQGuru.eMES.Material.InvoicesDetailDN inv in ins)
            {


                BenQGuru.eMES.SAPRFCService.Domain.DN dn = new BenQGuru.eMES.SAPRFCService.Domain.DN();

                dn.DNNO = inv.InvNo;
                dn.DNLine = 0;
                dn.Unit = " ";
                dn.BatchNO = " ";

                Dnlog_in log = new Dnlog_in();
                log.Batchno = " ";
                log.DNline = 0;
                log.DNno = dn.DNNO;
                log.Ispback = " ";
                log.MaintainDate = dbTime.DBDate;
                log.MaintainTime = dbTime.DBTime;
                log.MaintainUser = usr;
                log.Qty = dn.Qty;
                log.Serial = 0;
                log.Unit = dn.Unit;
                logList.Add(log);
                dns.Add(dn);
            }

            bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;
            if (is2Sap)
            {
                LogDNRePGIT2Sap(dns, usr, _InventoryFacade);
            }
            else
            {
                #region SAP
                BenQGuru.eMES.SAPRFCService.DNToSAP dnToSAP = new DNToSAP(DataProvider);
                for (int i = 0; i < dns.Count; i++)
                {

                    SAPRfcReturn msg = dnToSAP.DNRePGIToSAP(dns[i]);

                    #region 如果错了返回false

                    if (msg.Result.Trim().ToUpper() == "E")
                    {
                        logList[i].Sapmaterialinvoice = Getstring(msg.MaterialDocument);
                        logList[i].Sapreturn = "E";
                        logList[i].Message = Getstring(msg.Message);

                        isSuccess = false;

                        message = "SAP回写错误" + msg.Message;
                        break;
                    }
                    else
                    {
                        logList[i].Sapmaterialinvoice = Getstring(msg.MaterialDocument);
                        logList[i].Sapreturn = Getstring(msg.Result);
                        logList[i].Message = Getstring(msg.Message);
                    }

                    #endregion
                }
                foreach (Dnlog_in dnLog in logList)
                {
                    _InventoryFacade.AddDnlog_in(dnLog);
                }
                #endregion
            }

            return isSuccess;

        }


        private void LogDNRePGIT2Sap(List<BenQGuru.eMES.SAPRFCService.Domain.DN> dns, string usr, InventoryFacade inventoryFacade)
        {
            foreach (BenQGuru.eMES.SAPRFCService.Domain.DN dn in dns)
            {

                Dn_in2Sap log = new Dn_in2Sap();
                dn.DNNO = dn.DNNO;
                log.DNLine = dn.DNLine;
                log.Unit = dn.DNNO;
                log.BatchNO = dn.BatchNO;



                log.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                log.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                log.MaintainUser = usr;
                log.Sapreturn = "empty";
                log.Message = "empty";
                #region 注释
                //if (ret == null)
                //else
                //    log.RESULT = ret.Result;
                //log.MESSAGE = ret != null ? ret.Message : "null";
                //if (ret != null && string.IsNullOrEmpty(ret.Message))
                //{
                //} 
                #endregion
                inventoryFacade.AddDn_in2Sap(log);
            }
        }



        private bool PoToSap(Asndetail[] asnDetailList, InventoryFacade _InventoryFacade, WarehouseFacade _WarehouseFacade, DBDateTime dbTime, IDomainDataProvider DataProvider, out string message, string usr)
        {

            string stno = "";
            message = string.Empty;
            bool isSuccess = true;
            List<string> stlines = new List<string>();
            foreach (Asndetail asndetail in asnDetailList)
            {
                stlines.Add(asndetail.Stline);
            }
            List<PO> list = new List<PO>();
            List<PoLog> logList = new List<PoLog>();

            stno = ((Asndetail)asnDetailList[0]).Stno;
            ASN asn = (ASN)_InventoryFacade.GetASN(stno);
            object[] obj_item = _InventoryFacade.GetQcRejectQtyFromASNDetailItem1(stno, stlines);
            int count = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime);
            foreach (Asndetailitem itemNew in obj_item)
            {

                object invoicesDetaillist = _InventoryFacade.GetInvoicesDetail(asn.InvNo, int.Parse(itemNew.Invline));
                InvoicesDetail invdetail = invoicesDetaillist as InvoicesDetail;
                decimal actQTY = itemNew.ActQty;
                if (actQTY <= 0)
                    continue;



                PoLog poLog = new PoLog();
                int serial = _InventoryFacade.GetMaxSerialInPoLog() + 1;
                poLog.Serial = serial;
                poLog.ZNUMBER = serial.ToString();
                poLog.PONO = asn.InvNo;
                poLog.PoLine = itemNew.Invline.ToString();
                poLog.FacCode = asn.FacCode;
                poLog.SerialNO = asn.StNo; // asndetail.s;
                poLog.MCode = invdetail.MCode;
                poLog.Qty = actQTY; // 
                poLog.Unit = invdetail.Unit; //asndetailObj.Unit;
                poLog.Status = "105"; // 
                poLog.Operator = usr; // asndetail.;
                poLog.VendorInvoice = asn.InvNo;
                poLog.StorageCode = asn.StorageCode;
                poLog.Remark = asn.Remark1;
                poLog.InvoiceDate = asn.MaintainDate;
                poLog.SapDateStamp = dbTime.DBDate;
                poLog.SapTimeStamp = dbTime.DBTime;
                if (count > 0) //P回传
                {
                    poLog.SAPMaterialInvoice = "";
                    poLog.IsPBack = "";
                    poLog.SapReturn = "";
                }
                else
                {
                    poLog.IsPBack = "Actual";
                }
                logList.Add(poLog);


                #region 回传接口

                PO po = new PO();
                po.PONO = asn.InvNo;
                po.POLine = int.Parse(itemNew.Invline);
                po.FacCode = asn.FacCode;
                po.SerialNO = asn.StNo; // asndetail.s;
                po.ZNUMBER = serial.ToString();
                po.MCode = invdetail.MCode;//SAPMcode
                po.Qty = actQTY; //初检 接收数量
                po.Unit = invdetail.Unit; //asndetailObj.Unit;
                po.Status = "105"; //接收
                string invoice103 = string.Empty;
                PoLog oldPoLogs =
                    (PoLog)
                    _InventoryFacade.GetPoLog(po.PONO, po.POLine.ToString(), po.SerialNO, "103");
                if (oldPoLogs != null)
                    invoice103 = oldPoLogs.SAPMaterialInvoice;
                else
                    invoice103 = _InventoryFacade.GetPo103Invoices(po.PONO, po.POLine.ToString(), po.SerialNO);


                po.SAPMaterialInvoice = invoice103;

                po.Operator = usr;
                po.VendorInvoice = asn.InvNo;
                po.StorageCode = asn.StorageCode;
                po.Remark = asn.Remark1;
                po.InvoiceDate = asn.MaintainDate;
                list.Add(po);

            }

            if (list.Count <= 0)
            {
                message = "回写的数据为空！";
                return false;
            }

            bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;




            if (is2Sap)
            {
                LogPO2Sap(dbTime, list, _InventoryFacade);
            }
            else
            {
                #region SAP
                BenQGuru.eMES.SAPRFCService.POToSAP poToSAP = new POToSAP(DataProvider);
                SAPRfcReturn msg = poToSAP.POReceiveToSAP(list);

                #region 如果错了返回false

                if (msg.Result.Trim().ToUpper() == "E")
                {
                    isSuccess = false;
                    message = "SAP回写错误" + msg.Message;
                }

                #endregion

                #region 写log

                foreach (PoLog poLog in logList)
                {

                    if (count > 0) //P回传
                    {
                        poLog.SAPMaterialInvoice = ""; //初检时放，P就为空
                        poLog.SapReturn = "";
                    }
                    else
                    {
                        poLog.SAPMaterialInvoice = Getstring(msg.MaterialDocument); //
                        poLog.SapReturn = Getstring(msg.Result); // msg.Result;//(S表示成功，E表示失败)
                        poLog.Message = Getstring(msg.Message);
                    }
                    _InventoryFacade.AddPoLog(poLog);

                }

                #endregion

                if (!isSuccess)
                {
                    return false;
                }
                #endregion
            }
            PoToSupport(list, false);
            return true;

        }


        private bool DNToSAP(Asndetail[] asnDetailList, InventoryFacade _InventoryFacade, WarehouseFacade _WarehouseFacade, DBDateTime dbTime, IDomainDataProvider DataProvider, string usr, out string message)
        {

            message = string.Empty;
            Dictionary<string, SumItemsQTYDTO> sumItemGroupbyInvNo = new Dictionary<string, SumItemsQTYDTO>();



            foreach (Asndetail asndetail in asnDetailList)
            {

                ASN asn = (ASN)_InventoryFacade.GetASN(asndetail.Stno);

                if (sumItemGroupbyInvNo.ContainsKey(asn.InvNo))
                    continue;
                List<SumItemsQTY> sums = _InventoryFacade.SumItemsWithInvNoAdnLineBy(asn.InvNo, asn.StNo);

                sumItemGroupbyInvNo.Add(asn.InvNo, new SumItemsQTYDTO { Sums = sums, Unit = asndetail.Unit });

            }
            int count = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime);

            foreach (string invo in sumItemGroupbyInvNo.Keys)
            {
                List<DN> dns = new List<DN>();
                List<Dnlog_in> logList = new List<Dnlog_in>();
                SumItemsQTYDTO dto = sumItemGroupbyInvNo[invo];

                foreach (SumItemsQTY sum in dto.Sums)
                {
                    DN dn = new DN();
                    dn.BatchNO = sum.DNBATCHNO;
                    dn.Qty = sum.ACTQTY;
                    dn.DNLine = int.Parse(sum.InvLine);
                    dn.DNNO = sum.InvNo;
                    dn.Unit = dto.Unit;
                    dns.Add(dn);

                    Dnlog_in dnlog = new Dnlog_in();
                    dnlog.Serial = 0;

                    dnlog.Batchno = sum.DNBATCHNO;
                    dnlog.MaintainDate = dbTime.DBDate;
                    dnlog.MaintainTime = dbTime.DBTime;
                    dnlog.MaintainUser = usr;
                    dnlog.Qty = sum.ACTQTY;
                    dnlog.DNline = int.Parse(sum.InvLine);
                    dnlog.DNno = sum.InvNo;
                    dnlog.Unit = dto.Unit;
                    if (count > 0) //P回传
                    {
                        dnlog.Sapmaterialinvoice = "";
                        dnlog.Ispback = " ";
                        dnlog.Sapreturn = "";
                    }
                    else
                    {
                        dnlog.Ispback = "Actual";
                    }
                    logList.Add(dnlog);
                }
                bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;
                if (is2Sap)
                {
                    LogDN2Sap(dns, "Y", usr, _InventoryFacade);
                }
                else
                {
                    #region SAP回写
                    BenQGuru.eMES.SAPRFCService.DNToSAP dnToSAP = new DNToSAP(DataProvider);
                    SAPRfcReturn msg = null;
                    try
                    {
                        msg = dnToSAP.DNPGIToSAP(dns, false);
                    }
                    catch (Exception ex)
                    {

                    }
                    foreach (Dnlog_in dnLog in logList)
                    {

                        if (count > 0) //P回传
                        {
                            dnLog.Sapmaterialinvoice = ""; //初检时放，P就为空
                            dnLog.Sapreturn = "";
                        }
                        else
                        {
                            dnLog.Sapmaterialinvoice = msg != null ? msg.MaterialDocument : string.Empty;
                            dnLog.Sapreturn = msg != null ? msg.Result : string.Empty;

                            dnLog.Message = msg != null ? msg.Message : string.Empty;
                        }
                        _InventoryFacade.AddDnlog_in(dnLog);
                    }

                    Log.Error("-------------------------------------------" + msg.Message + ":" + msg.Result +
                              "-------------------------------------------------------");
                    if (msg == null)
                    {

                        message = "SAP回写错误 返回结果为空！";
                        return false;
                    }
                    if (msg.Result.Trim().ToUpper() == "E")
                    {


                        message = "SAP回写错误" + msg.Message;
                        return false;

                    }
                    #endregion
                }
            }

            return true;
        }

        private void LogDN2Sap(List<BenQGuru.eMES.SAPRFCService.Domain.DN> dns, string isAll, string usr, InventoryFacade _InventoryFacade)
        {
            foreach (BenQGuru.eMES.SAPRFCService.Domain.DN dn in dns)
            {

                DN2Sap log = new DN2Sap();
                log.DNLine = dn.DNLine;
                log.DNNO = dn.DNNO;
                log.IsAll = isAll;



                log.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                log.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                log.MaintainUser = usr;
                log.Result = "empty";
                log.Message = "empty";
                log.Qty = dn.Qty;
                log.Unit = dn.Unit;
                #region 注释
                //if (ret == null)
                //else
                //    log.RESULT = ret.Result;
                //log.MESSAGE = ret != null ? ret.Message : "null";
                //if (ret != null && string.IsNullOrEmpty(ret.Message))
                //{
                //} 
                #endregion
                log.BatchNO = dn.BatchNO;
                _InventoryFacade.AddDN2Sap(log);
            }
        }


        private bool RSToSAP(Asndetail[] asnDetailList, InventoryFacade _InventoryFacade, WarehouseFacade _WarehouseFacade, DBDateTime dbTime, IDomainDataProvider DataProvider, string usr, out string message)
        {
            string stno = "";
            message = string.Empty;
            bool isSuccess = true;
            List<string> stlines = new List<string>();
            foreach (Asndetail asndetail in asnDetailList)
            {
                stlines.Add(asndetail.Stline);
            }
            List<RS> list = new List<RS>();
            List<Rslog> logList = new List<Rslog>();

            stno = ((Asndetail)asnDetailList[0]).Stno;
            ASN asn = (ASN)_InventoryFacade.GetASN(stno);
            object[] obj_item = _InventoryFacade.GetQcRejectQtyFromASNDetailItem1(stno, stlines);
            int count = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime);
            foreach (Asndetailitem itemNew in obj_item)
            {

                object invoicesDetaillist = _InventoryFacade.GetInvoicesDetail(asn.InvNo, int.Parse(itemNew.Invline));
                InvoicesDetail invdetail = invoicesDetaillist as InvoicesDetail;
                decimal actQTY = itemNew.ActQty;
                if (actQTY <= 0)
                    continue;



                Rslog rsLog = new Rslog();
                int serial = _InventoryFacade.GetMaxSerialInPoLog() + 1;
                rsLog.Serial = serial;
                rsLog.DocumentDate = dbTime.DBDate;
                rsLog.FacCode = invdetail.FacCode;
                rsLog.Inoutflag = "202";
                rsLog.MCode = invdetail.MCode; // asndetail.s;
                rsLog.Mestransno = stno;
                rsLog.Qty = actQTY; // 
                rsLog.Unit = invdetail.Unit; //asndetailObj.Unit;
                rsLog.Qty = actQTY;
                rsLog.Rsline = invdetail.InvLine;
                rsLog.Rsno = invdetail.InvNo;
                rsLog.StorageCode = asn.StorageCode;
                rsLog.MaintainUser = usr;
                rsLog.MaintainDate = dbTime.DBDate;
                rsLog.MaintainTime = dbTime.DBTime;


                if (count > 0) //P回传
                {
                    rsLog.Sapmaterialinvoice = "";
                    rsLog.IsPBack = "";
                    rsLog.Sapreturn = "";
                }
                else
                {
                    rsLog.IsPBack = "Actual";
                }
                logList.Add(rsLog);


                #region 回传接口
                RS rs = new RS();
                rs.DocumentDate = dbTime.DBDate;
                rs.FacCode = invdetail.FacCode;
                rs.InOutFlag = "202";
                rs.MCode = invdetail.MCode;
                rs.MesTransNO = stno;
                rs.Qty = actQTY;
                rs.RSLine = invdetail.InvLine;
                rs.RSNO = invdetail.InvNo;
                rs.StorageCode = asn.StorageCode;// invdetail.StorageCode;
                rs.Unit = invdetail.Unit;
                list.Add(rs);

            }

            if (list.Count <= 0)
            {
                message = "回写的数据为空！";
                return false;
            }

            bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;




            if (is2Sap)
            {
                LogRS2Sap(list, _InventoryFacade, usr);
            }
            else
            {
                #region SAP
                BenQGuru.eMES.SAPRFCService.RSToSAP rsToSAP = new RSToSAP(DataProvider);
                SAPRfcReturn msg = rsToSAP.PostRSToSAP(list);
                #region 如果错了返回false

                if (msg.Result.Trim().ToUpper() == "E")
                {
                    isSuccess = false;

                    message = "SAP回写错误" + msg.Message;
                }

                #endregion

                #region 写log

                foreach (Rslog rsLog in logList)
                {

                    if (count > 0) //P回传
                    {
                        rsLog.Sapmaterialinvoice = "";//初检时放，P就为空
                        rsLog.Sapreturn = "";
                    }
                    else
                    {
                        rsLog.Sapmaterialinvoice = Getstring(msg.MaterialDocument); //
                        rsLog.Sapreturn = Getstring(msg.Result);  // msg.Result;//(S表示成功，E表示失败)
                        rsLog.Message = Getstring(msg.Message);
                    }
                    _InventoryFacade.AddRslog(rsLog);

                }

                #endregion

                if (!isSuccess)
                {
                    return false;
                }
                #endregion
            }

            return true;
        }

        private void LogRS2Sap(List<BenQGuru.eMES.SAPRFCService.Domain.RS> dns, InventoryFacade _InventoryFacade, string usr)
        {
            foreach (BenQGuru.eMES.SAPRFCService.Domain.RS rs in dns)
            {

                Rs2Sap rsLog = new Rs2Sap();
                rsLog.Qty = rs.Qty;
                rsLog.RSNO = rs.RSNO;
                rsLog.RSLine = rs.RSLine;
                rsLog.Unit = rs.Unit;
                rsLog.DocumentDate = rs.DocumentDate;
                rsLog.FacCode = rs.FacCode;
                rsLog.InOutFlag = rs.InOutFlag;
                rsLog.MCode = rs.MCode;
                rsLog.MesTransNO = rs.MesTransNO;
                rsLog.StorageCode = rs.StorageCode;
                rsLog.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                rsLog.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                rsLog.MaintainUser = usr;
                _InventoryFacade.AddRs2Sap(rsLog);
            }
        }


        protected bool CheckDataStatus(string cartonCode, string locationNo, out string message, IDomainDataProvider DataProvider)
        {
            message = string.Empty;
            if (string.IsNullOrEmpty(cartonCode))
            {
                message = "箱号不能为空！";
                return false;
            }
            if (string.IsNullOrEmpty(locationNo))
            {
                message = "货位号不能为空！";
                return false;
            }

            WarehouseFacade facade = new WarehouseFacade(DataProvider);
            int result = facade.CheckDatabyCartonNo(cartonCode);
            if (result == 0)
            {
                message = cartonCode + "箱号不存在！";
                return false;
            }
            result = facade.CheckLocationInfobyLocationNo(locationNo, cartonCode);
            if (result == 1)
            {
                message = locationNo + "货位号不存在！";
                return false;
            }
            else if (result == 2)
            {
                message = "货位号对应的库位与入库指令号对应的货位不同";
                return false;
            }
            return true;
        }

        private string Getstring(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                return message;
            }
            return "";
        }

        //public bool SaveIQCNG(string _IQCNo, IDomainDataProvider DataProvider, string stno, string stLine, string ECGCode,string EGCode, string cartonno, bool isAdd, string user, out string message)
        //{

        //    IQCFacade _IQCFacade = new IQCFacade(DataProvider);
        //    WarehouseFacade _WarehouseFacade = new WarehouseFacade(DataProvider);
        //    if (string.IsNullOrEmpty(_IQCNo))
        //    {
        //        message = "IQC号不能为空！";
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(_IQCNo))
        //    {
        //        message = "IQC号不能为空！";
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(ECGCode))
        //    {
        //        message = "缺陷类型不能为空！";
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(EGCode))
        //    {
        //        message = "缺陷描述不能为空！";
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(cartonno))
        //    {
        //        message = "箱号不能为空！";
        //        return false;
        //    }

        //    AsnIQCDetailEc asnIqcDetailEc = new AsnIQCDetailEc();
        //    asnIqcDetailEc.IqcNo = _IQCNo;
        //    asnIqcDetailEc.StNo = stno;
        //    asnIqcDetailEc.StLine = stLine;
        //    asnIqcDetailEc.CartonNo = cartonno.ToUpper();
        //    asnIqcDetailEc.EcgCode = ECGCode.ToUpper();

        //    asnIqcDetailEc.ECode = FormatHelper.CleanString(this.drpNGDescEdit.SelectedValue, 40);
        //    if (string.IsNullOrEmpty(this.txtSNEdit.Text) && string.IsNullOrEmpty(this.txtNGQtyEdit.Text.Trim()))
        //    {
        //        asnIqcDetailEc.NgFlag = "Y";
        //    }
        //    else
        //    {
        //        asnIqcDetailEc.NgFlag = "N";
        //    }

        //    asnIqcDetailEc.CUser = user;
        //    asnIqcDetailEc.CDate = FormatHelper.GetNowDBDateTime(DataProvider).DBDate;
        //    asnIqcDetailEc.CTime = FormatHelper.GetNowDBDateTime(DataProvider).DBTime;

        //    asnIqcDetailEc.MaintainUser = this.GetUserName();
        //    asnIqcDetailEc.SN = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSNEdit.Text, 40));
        //    asnIqcDetailEc.NgQty = string.IsNullOrEmpty(this.txtNGQtyEdit.Text.Trim()) ? 1 : Convert.ToInt32(this.txtNGQtyEdit.Text.Trim());
        //    asnIqcDetailEc.Remark1 = FormatHelper.CleanString(this.txtMemoEdit.Text, 200);



        //    message = string.Empty;
        //    AsnIQC asnIQC = _IQCFacade.GetAsnIQC(_IQCNo) as AsnIQC;
        //    if (asnIQC.Status == IQCStatus.IQCStatus_SQEJudge)
        //    {
        //        message = "SQE判定状态不能再修改";
        //        return false;
        //    }
        //    if (asnIQC.Status == IQCStatus.IQCStatus_IQCClose)
        //    {
        //        message = _IQCNo + "已经关闭，不能再修改"; ;
        //        return false;
        //    }
        //    try
        //    {
        //        DataProvider.BeginTransaction();
        //        AsnIQCDetailEc asnIQCDetailEc = (AsnIQCDetailEc)domainObject;
        //        if (isAdd)
        //        {
        //            #region  判断录入的SN是否在包装内
        //            if (!string.IsNullOrEmpty(this.txtSNEdit.Text.Trim()))
        //            {

        //                object[] cartoninvmar_obj = _WarehouseFacade.GetIqcdetailsn(asnIQCDetailEc.IqcNo, FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSNEdit.Text)), asnIQCDetailEc.CartonNo);
        //                if (cartoninvmar_obj == null)
        //                {
        //                    this.DataProvider.RollbackTransaction();
        //                    WebInfoPublish.Publish(this, "SN不在包装内", this.languageComponent1);
        //                    return;
        //                }
        //            }
        //            if (!string.IsNullOrEmpty(this.txtNGQtyEdit.Text))
        //            {
        //                object objs_carqty = _IQCFacade.GetASNIQCDetailByIQCNoAndCartonNo(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo);
        //                if (objs_carqty != null)
        //                {
        //                    AsnIQCDetail carqty = objs_carqty as AsnIQCDetail;
        //                    int num = 0;
        //                    try
        //                    {
        //                        num = int.Parse(this.txtNGQtyEdit.Text);
        //                    }
        //                    catch
        //                    {
        //                        this.DataProvider.RollbackTransaction();
        //                        WebInfoPublish.Publish(this, "不良数必须是数字格式", this.languageComponent1);
        //                        return;
        //                    }
        //                    if (num > carqty.Qty)
        //                    {
        //                        this.DataProvider.RollbackTransaction();
        //                        WebInfoPublish.Publish(this, "不良数量不能大于送检数量", this.languageComponent1);
        //                        return;
        //                    }
        //                }
        //            }
        //            #endregion

        //            #region CheckData
        //            //1》	同一箱号NGFLAG=Y的记录只能存在一笔
        //            //2》	单件管控：SN记录NGFLAG=N的记录只能存在一笔
        //            //3》	批管控：同一箱号NGFLAG=N记录的SUM(NGQTY)不能大于箱号送检数量
        //            int asnIQCDetailEcCount = _IQCFacade.GetAsnIQCDetailEcCount(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo, "Y");
        //            if (asnIQCDetailEcCount > 0)
        //            {
        //                if (asnIQCDetailEc.NgFlag == "Y")
        //                {
        //                    this.DataProvider.RollbackTransaction();
        //                    WebInfoPublish.PublishInfo(this, "同一箱号NGFLAG=Y的记录只能存在一笔", this.languageComponent1);
        //                    return;
        //                }
        //            }
        //            if (MControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
        //            {
        //                int asnIQCDetailECCount = _IQCFacade.GetAsnIQCDetailECCount(asnIQCDetailEc.IqcNo, asnIQCDetailEc.SN, "N");
        //                if (asnIQCDetailECCount > 0)
        //                {
        //                    if (asnIQCDetailEc.NgFlag == "N")
        //                    {
        //                        this.DataProvider.RollbackTransaction();
        //                        WebInfoPublish.PublishInfo(this, "SN记录NGFLAG=N的记录只能存在一笔", this.languageComponent1);
        //                        return;
        //                    }
        //                }

        //            }
        //            if (MControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_LOT)
        //            {
        //                AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(_IQCNo);
        //                if (asnIqc != null)
        //                {
        //                    int ngQtyCount = _IQCFacade.GetSumNgQtyFromAsnIQCDetailEc(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo, "N");
        //                    if (ngQtyCount > asnIqc.Qty)
        //                    {
        //                        this.DataProvider.RollbackTransaction();
        //                        WebInfoPublish.PublishInfo(this, "大于箱号送检数量", this.languageComponent1);
        //                        return;
        //                    }
        //                }
        //            }

        //            #endregion


        //            this.ViewState["IsAdd"] = false;//新增状态更改为false

        //            //object[] objAsnIqcDetaileEc = _IQCFacade.GetAsnIQCDetailEc(asnIQCDetailEc.ECode, asnIQCDetailEc.StLine,
        //            //asnIQCDetailEc.IqcNo, asnIQCDetailEc.StNo,
        //            //asnIQCDetailEc.SN);
        //            object[] objAsnIqcDetaileEc = _IQCFacade.GetAsnIQCDetailEc(asnIQCDetailEc.EcgCode, asnIQCDetailEc.ECode, asnIQCDetailEc.StLine,
        //                                                                 asnIQCDetailEc.IqcNo, asnIQCDetailEc.StNo,
        //                                                                 asnIQCDetailEc.SN);
        //            if (objAsnIqcDetaileEc != null)
        //            {
        //                this.DataProvider.RollbackTransaction();
        //                WebInfoPublish.PublishInfo(this, "重复添加", this.languageComponent1);
        //                return;
        //            }
        //            this._IQCFacade.AddAsnIQCDetailEc(asnIQCDetailEc);

        //            AsnIQCDetail asnIqcDetail = (AsnIQCDetail)_IQCFacade.GetAsnIQCDetail(Convert.ToInt32(asnIQCDetailEc.StLine),
        //                                                                                asnIQCDetailEc.IqcNo,
        //                                                                                asnIQCDetailEc.StNo);
        //            #region UpdateTable
        //            if (asnIqcDetail != null)
        //            {
        //                //asnIqcDetail.NgQty = _IQCFacade.GetSumNgQtyFromAsnIQCDetailEc(asnIQCDetailEc.IqcNo, "");
        //                asnIqcDetail.NgQty = _IQCFacade.GetSumNgQtyFromAsnIQCDetailEc1(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo, "");
        //                asnIqcDetail.QcStatus = "N";
        //                _IQCFacade.UpdateAsnIQCDetail(asnIqcDetail);
        //            }
        //            AsnIqcDetailSN asnIqcDetailSN = (AsnIqcDetailSN)_IQCFacade.GetAsnIqcDetailSN(Convert.ToInt32(asnIQCDetailEc.StLine),
        //                                                                                            asnIQCDetailEc.IqcNo,
        //                                                                                            asnIQCDetailEc.SN,
        //                                                                                            asnIQCDetailEc.StNo);
        //            if (asnIqcDetailSN != null)
        //            {
        //                asnIqcDetailSN.QcStatus = "N";
        //                _IQCFacade.UpdateAsnIqcDetailSN(asnIqcDetailSN);
        //            }
        //            if (string.IsNullOrEmpty(txtSNEdit.Text) && string.IsNullOrEmpty(txtNGQtyEdit.Text))
        //            {
        //                object[] objs_asnIqcDetailSN1 = _IQCFacade.GetAsnIqcDetailSNByIqcNoAndCartonNo(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo);
        //                if (objs_asnIqcDetailSN1 != null && objs_asnIqcDetailSN1.Length > 0)
        //                {
        //                    foreach (AsnIqcDetailSN asnIqcDetailSN1 in objs_asnIqcDetailSN1)
        //                    {
        //                        asnIqcDetailSN1.QcStatus = "N";
        //                        _IQCFacade.UpdateAsnIqcDetailSN(asnIqcDetailSN1);
        //                    }
        //                }

        //            }
        //            #endregion

        //            #region add by sam
        //            if (this.rblType.Items[0].Selected)//抽检
        //            {
        //                AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(_IQCNo);
        //                if (asnIqc != null)
        //                {
        //                    _IQCFacade.UpdateAsnIQCDetailByIqcno(_IQCNo, asnIqc.QcStatus);
        //                }
        //            }
        //            #endregion
        //            this.DataProvider.CommitTransaction();
        //        }
        //        else
        //        {
        //            this._IQCFacade.UpdateAsnIQCDetailEc(asnIQCDetailEc);
        //            this.DataProvider.CommitTransaction();
        //            WebInfoPublish.Publish(this, "保存成功！", this.languageComponent1);
        //        }
        //        this.ViewState["iqcNo"] = asnIQCDetailEc.IqcNo;
        //        this.ViewState["stNo"] = asnIQCDetailEc.StNo;
        //        this.ViewState["stLine"] = asnIQCDetailEc.StLine;
        //        this.ViewState["cartonNo"] = asnIQCDetailEc.CartonNo;
        //        this.gridHelper.RequestData();
        //        this.gridHelper2.RequestData();
        //    }
        //    catch (Exception ex)
        //    {

        //        WebInfoPublish.Publish(this, "保存失败：" + ex.Message, this.languageComponent1);
        //        this.DataProvider.RollbackTransaction();
        //    }

        //}

        public bool SubmitPortionCheck(string checkNo, string cartonno, string checkQtyStr, string locationCode, string diffDesc, string dqmCode, IDomainDataProvider DataProvider, string usr, out string message)
        {

            WarehouseFacade _WarehouseFacade = new WarehouseFacade(DataProvider);

            decimal checkQty = 0;
            message = string.Empty;
            if (!decimal.TryParse(checkQtyStr, out checkQty))
            {
                message = "盘点数量必须是数字！";
                return false;
            }

            if (string.IsNullOrEmpty(locationCode))
            {
                message = "实际货位不能为空！";
                return false;
            }

            if (string.IsNullOrEmpty(checkNo))
            {
                message = "盘点单不能为空！";
                return false;
            }
            if (string.IsNullOrEmpty(cartonno))
            {
                message = "箱号不能为空！";
                return false;
            }

            checkNo = checkNo.ToUpper();
            cartonno = cartonno.ToUpper();
            dqmCode = dqmCode.ToUpper();
            locationCode = locationCode.ToUpper();
            StockCheckDetailCarton oo = (StockCheckDetailCarton)_WarehouseFacade.GetStockCheckDetailCarton1(checkNo.ToUpper(), cartonno.ToUpper());
            try
            {
                DataProvider.BeginTransaction();
                if (oo != null)
                {
                    StorageDetail storageDetail = (StorageDetail)_WarehouseFacade.GetStorageDetail(oo.SCARTONNO);
                    decimal storageQty = 0;
                    if (storageDetail != null)
                        storageQty = storageDetail.StorageQty;
                    oo.CheckQty = checkQty;
                    oo.STORAGEQTY = storageQty;
                    oo.CheckNo = checkNo;
                    oo.CARTONNO = cartonno;
                    oo.LocationCode = locationCode;
                    oo.MDATE = FormatHelper.TODateInt(DateTime.Now);
                    oo.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                    oo.DiffDesc = diffDesc;
                    oo.MUSER = usr;
                    _WarehouseFacade.UpdateStockCheckDetailCartonImprov(oo);
                }
                else
                {
                    #region dqcmocde


                    #endregion

                    StockCheck stockCheck = (StockCheck)_WarehouseFacade.GetStockCheck(checkNo);
                    if (stockCheck == null)
                    {
                        message = "盘点单号不存在"; return false;
                    }

                    #region storageQty
                    string carton = FormatHelper.CleanString(cartonno);
                    StorageDetail storageDetail = (StorageDetail)_WarehouseFacade.GetStorageDetail(carton);



                    decimal storageQty = 0;
                    if (storageDetail != null)
                    {
                        storageQty = storageDetail.StorageQty;
                        dqmCode = storageDetail.DQMCode;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(dqmCode))
                        {
                            message = "鼎桥物料编码不能为空！";
                            return false;
                        }
                        //object[] materials = _WarehouseFacade.GetMaterialInfoByQDMCode(dqmCode);
                        //if (materials == null)
                        //{
                        //    message = "系统无此物料，请输入鼎桥物料编码后提交"; return false;
                        //}
                    }

                    StockCheckDetail sss = _WarehouseFacade.GetStockCheckDetail1(checkNo, cartonno);
                    BenQGuru.eMES.Domain.Warehouse.StockCheckDetailCarton s = new BenQGuru.eMES.Domain.Warehouse.StockCheckDetailCarton();
                    s.CheckNo = checkNo;
                    s.CheckQty = checkQty;
                    s.DQMCODE = dqmCode;
                    s.StorageCode = stockCheck.StorageCode;// txtStorageCodeEdit.Text;
                    s.STORAGEQTY = storageQty;
                    s.CARTONNO = cartonno;
                    s.LocationCode = locationCode;
                    s.DiffDesc = diffDesc;
                    s.CDATE = FormatHelper.TODateInt(DateTime.Now);
                    s.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                    s.CUSER = usr;
                    s.MDATE = FormatHelper.TODateInt(DateTime.Now);
                    s.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                    s.MUSER = usr;
                    s.SCARTONNO = sss != null ? sss.CARTONNO : " ";
                    s.SLocationCode = sss != null ? sss.LocationCode : " ";
                    _WarehouseFacade.InsertStockCheckDetailCarton(s);
                    if (sss == null)
                    {
                        StockCheckDetail empty = _WarehouseFacade.GetStockCheckDetail1(checkNo, " ");
                        if (empty == null)
                        {
                            StockCheckDetail sd = new StockCheckDetail();
                            sd.CDATE = FormatHelper.TODateInt(DateTime.Now);
                            sd.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                            sd.CUSER = usr;
                            sd.MDATE = FormatHelper.TODateInt(DateTime.Now);
                            sd.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                            sd.MUSER = usr;
                            sd.CARTONNO = " ";

                            sd.CheckNo = checkNo;
                            sd.CheckQty = 0;
                            sd.DQMCODE = " ";
                            sd.LocationCode = " ";
                            sd.StorageCode = " ";
                            sd.STORAGEQTY = 0;
                            sd.UNIT = " ";
                            _WarehouseFacade.AddStockCheckDetails(sd);
                        }
                    }
                }
                DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                DataProvider.RollbackTransaction();
                throw ex;
            }
            message = "保存成功！";
            return true;

        }

        public static string FormatDateTimeIntStr(string dateStr)
        {
            if (!string.IsNullOrEmpty(dateStr))
            {
                string[] strs = dateStr.Split(' ');
                if (strs.Length == 1)
                    return FormatHelper.ToDateString(int.Parse(strs[0]));
                else if (strs.Length == 2)
                    return FormatHelper.TODateTimeString(int.Parse(strs[0]), int.Parse(strs[1]));

            }
            return string.Empty;
        }
        public static bool PoToSupport(List<PO> pos, bool isReturnPo)
        {
            if (!IsReBackSupport)
                return false;
            if (pos.Count == 0)
                return false;
            string supportMsg = string.Empty;
            try
            {
                StringBuilder sb = new StringBuilder(300);
                sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sb.Append("<inputs>");
                sb.Append("<rows>");
                string dateNowStr = DateTime.Now.ToShortDateString();

                foreach (PO po in pos)
                {

                    sb.Append(string.Format("<EBELN>{0}</EBELN><EBELP>{1}</EBELP><ZNUMBER>{2}</ZNUMBER><MATNR>{3}</MATNR><MENGE>{4}</MENGE><MEINS>{5}</MEINS><BWART>{6}</BWART><RETPO>{7}</RETPO><SGTXT>{8}</SGTXT><LFSNR>{9}</LFSNR><LGORT>{10}</LGORT><BKTXT>{11}</BKTXT><BUDAT>{12}</BUDAT>",
                        po.PONO,
                        po.POLine,
                        po.ZNUMBER,
                        po.MCode,
                        po.Qty,
                        po.Unit,
                        po.Status,
                        isReturnPo ? "X" : string.Empty,
                        po.Operator,
                        string.Empty,
                        po.StorageCode,
                        string.Empty,
                        dateNowStr
                        ));

                }
                sb.Append("</rows>");
                sb.Append("<userid>test</userid>");
                sb.Append("<userkey>DBCA6E1E-8477-4188-A429-C042B395CC64</userkey>");
                sb.Append("</inputs>");
                ryan rrrrr = new ryan();
                string uri = System.Web.Configuration.WebConfigurationManager.AppSettings["SupportUri"];
                if (!string.IsNullOrEmpty(uri))
                {
                    rrrrr.Url = uri;
                }
                supportMsg = sb.ToString();
                Log.Error("support uri is :" + rrrrr.Url);
                rrrrr.ZCHN_MM_PO_GR_REC_ACP_REV(supportMsg);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ":" + supportMsg);
                return false;
            }
            return true;
        }

        public bool PostToSupport(Dictionary<string, decimal> ddd, string batchCode, InvoicesDetailExt[] ins, string pickNo, WarehouseFacade _WarehouseFacade, IDomainDataProvider DataProvider, out string message)
        {
            message = string.Empty;
            foreach (InvoicesDetailExt inv in ins)
            {
                string LFIMG = "";
                if (ddd.ContainsKey(inv.DQMCode))
                    LFIMG = ddd[inv.DQMCode].ToString();
                else
                    LFIMG = "0";


                //if (ddd.ContainsKey(inv.DQMCode))
                //{
                //    decimal remainCount = ddd[inv.DQMCode];
                //    remainCount -= inv.PlanQty;
                //    if (remainCount >= 0)
                //    {
                //        //po.Qty = inv.PlanQty;
                //        LFIMG = inv.PlanQty.ToString();
                //        ddd[inv.DQMCode] -= inv.PlanQty;
                //    }
                //    else
                //    {
                //        //po.Qty = ddd[inv.DQMCode];
                //        LFIMG = ddd[inv.DQMCode].ToString();
                //        ddd[inv.DQMCode] = 0;
                //    }
                //}
                //else
                //{
                //    LFIMG = inv.PlanQty.ToString();
                //}
                PickDetail pikd = (PickDetail)_WarehouseFacade.GetPickLineByDQMcode(pickNo, inv.DQMCode);
                if (pikd == null)
                {
                    DataProvider.RollbackTransaction();

                    message = "拣货任务令中没有鼎桥物料号：" + inv.DQMCode;
                    return false;
                }
                #region
                string PCBH = batchCode;	//	发货批次包号
                string VBTYP = "J";	//	交付类型
                string VBELN = inv.InvNo;// //	交货单号
                string POSNR = inv.InvLine.ToString("G0");	//	行项目号
                string MATNR = inv.DQMCode;	//	物料号
                //string LFIMG = "1";	//	交货数量
                string SERIAL = "";	//	序列号
                object[] list = _WarehouseFacade.QueryPickdetailmaterialsnByPickLine(pikd.PickNo, pikd.PickLine);
                if (list != null)
                {
                    foreach (Pickdetailmaterialsn sn in list)
                    {
                        SERIAL += sn.Sn + ";";
                    }
                    SERIAL = SERIAL.Remove(SERIAL.LastIndexOf(';'));
                }
                string VRKME = pikd.Unit;//	销售单位
                string BSTKD = inv.ORDERNO;		//	合同号
                string VBELV = inv.CUSORDERNO;	//	订单号
                string AUART = inv.CUSORDERNOTYPE;	//	订单类型
                string KHPCH = inv.CUSBATCHNO;	//	客户批次号\项目名称
                string EMPST = inv.CusMCode;//	客户物料编码
                string CMTART = inv.CusitemSpec;	//	客户物料型号
                string CMAKTX = inv.CusitemDesc;//	客户物料描述
                string SHIPLOC = inv.SHIPPINGLOCATION;//	收货地址
                string IHREZ_PV = inv.GFCONTRACTNO;	//	光伏合同号 
                string AUGRU = inv.ORDERREASON;	//	订单原因 
                string JFFS = inv.POSTWAY;	//	交付方式
                string JLTJ = inv.PICKCONDITION;	//	拣料条件 
                string DN_YJGF = inv.GFFLAG;	//	是否硬件光伏
                string WBSTK = inv.INVSTATUS;	//	状态

                #endregion

                #region xml

                //string xmlStr = "<?xml version='1.0' encoding='utf-8'?> ";
                string xmlStr = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                xmlStr += string.Format(@"   <inputs>
                                  <rows>
                                 <PCBH>" + PCBH + @" </PCBH>
                                <VBTYP>" + VBTYP + @" </VBTYP>
                                <VBELN>" + VBELN + @" </VBELN>
                                <POSNR>" + POSNR + @" </POSNR>
                                <MATNR>" + MATNR + @" </MATNR>
                                <LFIMG>" + LFIMG + @" </LFIMG>
                                <SERIAL>" + SERIAL + @" </SERIAL>
                                <VRKME>" + VRKME + @" </VRKME>
                                <BSTKD>" + BSTKD + @" </BSTKD>
                                <VBELV>" + VBELV + @" </VBELV>
                                <AUART>" + AUART + @" </AUART>
                                <KHPCH>" + KHPCH + @" </KHPCH>
                                <EMPST>" + EMPST + @" </EMPST>
                                <CMTART>" + CMTART + @" </CMTART>
                                <CMAKTX>" + CMAKTX + @" </CMAKTX>
                                <SHIPLOC>" + SHIPLOC + @" </SHIPLOC>
                                <IHREZ_PV>" + IHREZ_PV + @" </IHREZ_PV>
                                <AUGRU>" + AUGRU + @" </AUGRU>
                                <JFFS>" + JFFS + @" </JFFS>
                                <DN_YJGF>" + DN_YJGF + @" </DN_YJGF>
                                <WBSTK>" + WBSTK + @" </WBSTK>
                                      </rows>
                                    <userid>test</userid><userkey>DBCA6E1E-8477-4188-A429-C042B395CC64</userkey></inputs>");
                #endregion
                //<JLTJ>" + JLTJ + @" </JLTJ>
                try
                {
                    string uri = System.Web.Configuration.WebConfigurationManager.AppSettings["SupportUri"];

                    ryan ryan = new ryan();

                    if (!string.IsNullOrEmpty(uri))
                    {
                        ryan.Url = uri;
                    }
                    Log.Error("support uri is :" + ryan.Url);
                    string returnstr = ryan.ZCHN_SD_PGI_DETAIL_POST(xmlStr);
                    BenQGuru.eMES.Common.Log.Error(returnstr + " ======================================== " + xmlStr);
                }
                catch (Exception ex)
                {

                    BenQGuru.eMES.Common.Log.Error(ex.Message + "================================" + xmlStr);
                    return false;

                }
            }
            return true;
        }


        public class MailName
        {
            public const string ReceiveExpiredMail = "超期未到货预警";
            public const string ReceiveRejectMail = "到货初检拒收通知";
            public const string IQCSQEMail = "iqc_sqe判定结果通知";
            public const string OQCSQEMail = "oqc_sqe判定结果通知";
            public const string IQCExceptionMail = "iqc异常通知";
            public const string OQCExceptionMail = "oqc异常通知";
            public const string PickingExceptionMail = "生产领料提醒";
            public const string MaterialExpiredMail = "物料存储超期提醒";
            public const string OQCExpiredMail = "oqc_qc长期未处理流程预警";
            public const string IQCExpiredMail = "iqc_qc长期未处理流程预警";
            public const string IQC_SQEExpiredMail = "iqc_sqe长期未处理流程预警";
            public const string OQC_SQEExpiredMail = "oqc_sqe长期未处理流程预警";
            public const string YFRExpiredMail = "研发入库超期未完成入库预警";
            public const string Sap2MESDiversityMail = "sap库存与mes库存差异通知";
            public const string SapClosingErrorMail = "mes批量回传sap接口错误通知";
            public const string IQCSQERejectMail = "iqc_sqe拒收通知";
            public const string OQCSQERejectMail = "oqc_sqe拒收通知";

        }


        public class MailData
        {
            public int RejectCountByDQMCode { get; set; }
            public int RejectBoxs { get; set; }
            public string DQMCode { get; set; }




        }






    }







}
                    #endregion
                #endregion
                #endregion
                #endregion
                #endregion


