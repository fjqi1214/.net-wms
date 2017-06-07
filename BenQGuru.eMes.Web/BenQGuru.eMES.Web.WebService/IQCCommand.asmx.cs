using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Warehouse;
using System.Collections.Generic;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.IQC;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    /// <summary>
    /// IQCCommand 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class IQCCommand : System.Web.Services.WebService
    {

        public IQCCommand()
        {
            InitDbItems();
        }
        private IDomainDataProvider _domainDataProvider;
        private string m_DbName;
        private SystemSettingFacade _SystemSettingFacade = null;
        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider(m_DbName);
                }
                return _domainDataProvider;
            }
        }

        private void InitDbItems()
        {
            foreach (var item in BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.Settings)
            {
                if (item.Default)
                    m_DbName = item.Name;

            }
        }
        [WebMethod(EnableSession = true)]
        public DataTable GetCommand(string no, string status, string invNo, string user)
        {

            DataTable dt = new DataTable("ExampleDataTable");
            dt.Columns.Add(" ", typeof(string));
            dt.Columns.Add("ASN", typeof(string));
            dt.Columns.Add("STATUS", typeof(string));
            dt.Columns.Add("POSITION", typeof(string));
            dt.Columns.Add("SAPNO", typeof(string));
            dt.Columns.Add("ISEMERGENCY", typeof(string));
            dt.Columns.Add("DIRECTFLAG", typeof(string));

            WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser1(user);

            object[] objs = facade.QueryExecuteASN2(no, status, invNo, usergroupList);

            if (objs != null)
            {
                foreach (ASN a in objs)
                {
                    DataRow rd = dt.NewRow();
                    rd["STATUS"] = string.Empty;
                    if (a.Status == "Release")
                        rd["STATUS"] = "初始化";
                    if (a.Status == "WaitReceive")
                        rd["STATUS"] = "待收货";
                    if (a.Status == "Receive")
                        rd["STATUS"] = "初检";
                    if (a.Status == "ReceiveRejection")
                        rd["STATUS"] = "初检拒收";
                    if (a.Status == "IQC")
                        rd["STATUS"] = "IQC";
                    if (a.Status == "IQCRejection")
                        rd["STATUS"] = "IQC拒收";
                    if (a.Status == "OnLocation")
                        rd["STATUS"] = "上架";
                    if (a.Status == "Close")
                        rd["STATUS"] = "入库";
                    if (a.Status == "ReceiveClose")
                        rd["STATUS"] = "初检完成";

                    rd[" "] = string.Empty;
                    rd["ASN"] = a.StNo;

                    rd["POSITION"] = a.StorageCode;
                    rd["SAPNO"] = a.InvNo;
                    rd["ISEMERGENCY"] = a.ExigencyFlag;
                    rd["DIRECTFLAG"] = a.DirectFlag;
                    dt.Rows.Add(rd);
                }
            }
            return dt;
        }


        [WebMethod(EnableSession = true)]
        public string CancelDownCommand(string[] asns)
        {

            try
            {
                InventoryFacade facade = new InventoryFacade(DataProvider);
                WarehouseFacade _wa = new WarehouseFacade(DataProvider);

                foreach (string asn in asns)
                {
                    if (!facade.CheckASNDetailsStatus(asn))
                    {
                        return asn + "行项目已经全部初检完成不能取消下发";
                    }
                }
                DataProvider.BeginTransaction();

                _wa.UpdateASNForCancelDown(asns, "Release");
                foreach (string asn in asns)
                {
                    facade.UpdateASNDetail(asn, ASNHeadStatus.Release);
                    facade.UpdateASNDetailItem(asn);
                }
                DataProvider.CommitTransaction();
                return "取消下发成功！";
            }
            catch (Exception ex)
            {
                DataProvider.RollbackTransaction();
                throw ex;
            }

        }

        [WebMethod(EnableSession = true)]
        public bool FirstCheck(string[] asns, out string message)
        {
            try
            {
                WarehouseFacade facade = new WarehouseFacade(DataProvider);
                InventoryFacade facade1 = new InventoryFacade(DataProvider);
                message = string.Empty;
                Asn asn = (Asn)facade.GetAsn(asns[0]);

                if (asn.Direct_flag.ToUpper() == "Y")
                {
                    message = "该入库指令是供应商直发，不能做以下操作[取消下发][初检][申请IQC]";
                    return false;
                }

                if (asn.Status == ASNHeadStatus.WaitReceive)
                {
                    this.DataProvider.BeginTransaction();

                    string stNo = string.Format("'{0}'", asn.Stno);

                    facade1.UpdateASNStatusByStNo(ASNHeadStatus.Receive, stNo);
                    facade1.UpdateASNDetailStatusByStNo(ASNHeadStatus.Receive, stNo);
                    DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    WarehouseFacade _wa = new WarehouseFacade(DataProvider);
                    InvInOutTrans trans = _wa.CreateNewInvInOutTrans();
                    trans.CartonNO = string.Empty;
                    trans.DqMCode = " ";
                    trans.FacCode = asn.FacCode;
                    trans.FromFacCode = string.Empty;
                    trans.FromStorageCode = string.Empty;
                    trans.InvNO = asn.Invno;
                    trans.InvType = asn.StType;
                    trans.LotNo = string.Empty;
                    trans.MaintainDate = dbTime1.DBDate;
                    trans.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    trans.MaintainUser = string.Empty;
                    trans.MCode = " ";
                    trans.ProductionDate = 0;
                    trans.Qty = 0;
                    trans.Serial = 0;
                    trans.StorageAgeDate = 0;
                    trans.StorageCode = asn.StorageCode;
                    trans.SupplierLotNo = string.Empty;
                    trans.TransNO = asn.Stno;
                    trans.TransType = "IN";
                    trans.Unit = string.Empty;
                    trans.ProcessType = "RECEIVEBEGIN";
                    _wa.AddInvInOutTrans(trans);


                    this.DataProvider.CommitTransaction();
                    message = "初检成功";
                    return true;
                }
                else if (asn.Status == ASNHeadStatus.Receive)
                {
                    message = "初检成功";
                    return true;
                }
                else
                {
                    message = "状态必须是初见签收中或者待收货！";
                    return false;
                }

            }
            catch (Exception ex)
            {
                DataProvider.RollbackTransaction();
                BenQGuru.eMES.Common.Log.Error(ex.StackTrace);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public bool FirstCheckWithUser(string[] asns, string userCode, out string message)
        {
            try
            {
                WarehouseFacade facade = new WarehouseFacade(DataProvider);
                InventoryFacade facade1 = new InventoryFacade(DataProvider);
                message = string.Empty;
                Asn asn = (Asn)facade.GetAsn(asns[0]);

                if (asn.Direct_flag.ToUpper() == "Y")
                {
                    message = "该入库指令是供应商直发，不能做以下操作[取消下发][初检][申请IQC]";
                    return false;
                }

                if (asn.Status == ASNHeadStatus.WaitReceive)
                {
                    this.DataProvider.BeginTransaction();

                    string stNo = string.Format("'{0}'", asn.Stno);

                    facade1.UpdateASNStatusByStNo(ASNHeadStatus.Receive, stNo);
                    facade1.UpdateASNDetailStatusByStNo(ASNHeadStatus.Receive, stNo);
                    DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    WarehouseFacade _wa = new WarehouseFacade(DataProvider);
                    InvInOutTrans trans = _wa.CreateNewInvInOutTrans();
                    trans.CartonNO = string.Empty;
                    trans.DqMCode = " ";
                    trans.FacCode = asn.FacCode;
                    trans.FromFacCode = string.Empty;
                    trans.FromStorageCode = string.Empty;
                    trans.InvNO = asn.Invno;
                    trans.InvType = asn.StType;
                    trans.LotNo = string.Empty;
                    trans.MaintainDate = dbTime1.DBDate;
                    trans.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    trans.MaintainUser = userCode;
                    trans.MCode = " ";
                    trans.ProductionDate = 0;
                    trans.Qty = 0;
                    trans.Serial = 0;
                    trans.StorageAgeDate = 0;
                    trans.StorageCode = asn.StorageCode;
                    trans.SupplierLotNo = string.Empty;
                    trans.TransNO = asn.Stno;
                    trans.TransType = "IN";
                    trans.Unit = string.Empty;
                    trans.ProcessType = "RECEIVEBEGIN";
                    _wa.AddInvInOutTrans(trans);


                    this.DataProvider.CommitTransaction();
                    message = "初检成功";
                    return true;
                }
                else if (asn.Status == ASNHeadStatus.Receive)
                {
                    message = "初检成功";
                    return true;
                }
                else
                {
                    message = "状态必须是初见签收中或者待收货！";
                    return false;
                }

            }
            catch (Exception ex)
            {
                DataProvider.RollbackTransaction();
                BenQGuru.eMES.Common.Log.Error(ex.StackTrace);
                throw ex;
            }
        }


        [WebMethod(EnableSession = true)]
        public string[] ValidateASNStatusForIQC(string[] asns)
        {

            WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }


            string[] errors = facade.ValidateASNStatusForIQC(asns);
            return errors;
        }


        [WebMethod(EnableSession = true)]
        public string[] ValidateASNSTTypeForIQC(string[] asns)
        {

            WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }

            string[] errors = facade.ValidateASNSTTypeForIQC(asns);
            return errors;
        }

        private string CreateNewIqcNo(string stno, string userCode)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            WarehouseFacade _TransferFacade = new WarehouseFacade(this.DataProvider);

            string maxserial = _TransferFacade.GetMaxSerial("IQC" + stno);

            //如果已是最大值就返回为空
            if (maxserial == "99")
            {
                return "";
            }

            SERIALBOOK serialbook = new SERIALBOOK();

            if (maxserial == "")
            {
                serialbook.SNPrefix = "IQC" + stno;
                serialbook.MaxSerial = "1";
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = userCode;
                _TransferFacade.AddSerialBook(serialbook);
                return "IQC" + stno + string.Format("{0:00}", int.Parse(serialbook.MaxSerial));
            }
            else
            {
                serialbook.SNPrefix = "IQC" + stno;
                serialbook.MaxSerial = (int.Parse(maxserial) + 1).ToString();
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = userCode;
                _TransferFacade.UpdateSerialBook(serialbook);
                return "IQC" + stno + string.Format("{0:00}", int.Parse(serialbook.MaxSerial));
            }
        }


        private bool CheckAllASNDetailIsIQCClose(string iqcNo)
        {

            IQCFacade _IQCFacade = new IQCFacade(DataProvider);

            InventoryFacade _InventoryFacade = new InventoryFacade(DataProvider);

            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                object[] objAsnDetail = _InventoryFacade.GetASNDetailByStNo(asnIqc.StNo);
                if (objAsnDetail != null)
                {
                    foreach (ASNDetail asnDetail in objAsnDetail)
                    {
                        if (asnDetail.Status != ASNLineStatus.IQCClose)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //检查ASN明细所有行状态为OnLocation
        /// <summary>
        /// 检查ASN明细所有行状态为OnLocation
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <returns>全部是OnLocation：true;否则：false</returns>
        private bool CheckAllASNDetailIsOnLocation(string iqcNo)
        {

            IQCFacade _IQCFacade = new IQCFacade(DataProvider);

            InventoryFacade _InventoryFacade = new InventoryFacade(DataProvider);

            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                object[] objAsnDetail = _InventoryFacade.GetASNDetailByStNo(asnIqc.StNo);
                if (objAsnDetail != null)
                {
                    foreach (ASNDetail asnDetail in objAsnDetail)
                    {
                        if (asnDetail.Status != ASNLineStatus.OnLocation)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //检查ASN明细所有行状态为Close
        /// <summary>
        /// 检查ASN明细所有行状态为Close
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <returns>全部是Close：true;否则：false</returns>
        private bool CheckAllASNDetailIsClose(string iqcNo)
        {

            IQCFacade _IQCFacade = new IQCFacade(DataProvider);

            InventoryFacade _InventoryFacade = new InventoryFacade(DataProvider);

            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                object[] objAsnDetail = _InventoryFacade.GetASNDetailByStNo(asnIqc.StNo);
                if (objAsnDetail != null)
                {
                    foreach (ASNDetail asnDetail in objAsnDetail)
                    {
                        if (asnDetail.Status != ASNLineStatus.Close)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //检查ASN明细所有行状态为Cancel
        /// <summary>
        /// 检查ASN明细所有行状态为Cancel
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <returns>全部是Cancel：true;否则：false</returns>
        private bool CheckAllASNDetailIsCancel(string iqcNo)
        {

            IQCFacade _IQCFacade = new IQCFacade(DataProvider);

            InventoryFacade _InventoryFacade = new InventoryFacade(DataProvider);

            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                object[] objAsnDetail = _InventoryFacade.GetASNDetailByStNo(asnIqc.StNo);
                if (objAsnDetail != null)
                {
                    foreach (ASNDetail asnDetail in objAsnDetail)
                    {
                        if (asnDetail.Status != ASNLineStatus.Cancel)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        private void ToSTS(string iqcNo, string userCode)
        {

            IQCFacade _IQCFacade = new IQCFacade(DataProvider);

            InventoryFacade _InventoryFacade = new InventoryFacade(DataProvider);

            //1、更新送检单TBLASNIQC
            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                asnIqc.IqcType = "ExemptCheck";
                asnIqc.Status = IQCStatus.IQCStatus_IQCClose;
                asnIqc.QcStatus = "Y";
                _IQCFacade.UpdateAsnIQC(asnIqc);

                #region 在invinouttrans表中增加一条数据
                WarehouseFacade facade = new WarehouseFacade(DataProvider);

                //ASN asn = (ASN)domainObject;
                DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);
                InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                trans.CartonNO = string.Empty;
                trans.DqMCode = asnIqc.DQMCode;
                trans.FacCode = string.Empty;
                trans.FromFacCode = string.Empty;
                trans.FromStorageCode = string.Empty;
                trans.InvNO = asnIqc.InvNo;
                trans.InvType = asnIqc.IqcType;
                trans.LotNo = string.Empty;
                trans.MaintainDate = dbTime1.DBDate;
                trans.MaintainTime = dbTime1.DBTime;
                trans.MaintainUser = userCode;
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

            //7、以上表数据更新完成后检查ASN明细表(TBLASNDETAIL)所有行记录状态为：IQCClose:IQC完成 or OnLocation:上架 or Close:入库 or Cancel:取消时，
            //   更新ASN主表(TBLASN)状态(TBLASN.STATUS)为：OnLocation:上架
            bool isAllIQCClose = CheckAllASNDetailIsIQCClose(iqcNo);
            bool isAllOnLocation = CheckAllASNDetailIsOnLocation(iqcNo);
            bool isAllClose = CheckAllASNDetailIsClose(iqcNo);
            bool isAllCancel = CheckAllASNDetailIsCancel(iqcNo);

            if (isAllIQCClose || isAllOnLocation ||
                isAllClose || isAllCancel
                )
            {
                ASN asn = (ASN)_InventoryFacade.GetASN(asnIqc.StNo);
                if (asn != null)
                {
                    asn.Status = ASNHeadStatus.OnLocation;
                    _InventoryFacade.UpdateASN(asn);
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public string SaveIQCInfo(string[] asns, string usrCode)
        {

            WarehouseFacade wFacade = new WarehouseFacade(DataProvider);

            InventoryFacade facade = new InventoryFacade(DataProvider);

            BenQGuru.eMES.Web.Helper.DBDateTime dbDateTime = BenQGuru.eMES.Web.Helper.FormatHelper.GetNowDBDateTime(this.DataProvider);
            #region 6>	以下几种情况不可点击申请IQC：
            //1》 入库类型为：PD:盘点
            //2》 入库类型为：POR: PO入库 并且供应商直发标识为：Y
            //3》 入库类型为：SCTR:生产退料 并且生产退料入不良品库标识为：Y


            ASN asn = (ASN)facade.GetASN(asns[0]);

            if (asn.StType == InInvType.PD)
                return "入库类型为盘点，不可申请IQC";
            else if (asn.StType == InInvType.POR && asn.DirectFlag == "Y")
                return "入库类型为PO入库并且供应商直发标识为Y，不可申请IQC";

            else if (asn.StType == InInvType.SCTR && asn.RejectsFlag == "Y")
                return "入库类型为生产退料并且生产退料入不良品库标识为Y，不可申请IQC";


            bool hasDetail = facade.CheckASNHasDetail(asn.StNo, ASNLineStatus.ReceiveClose);
            if (!hasDetail)
            {
                bool hasReject = facade.CheckASNReceiveStatusHasDetail(asn.StNo, "Reject");
                if (hasReject)
                {
                    //将头数据改为拒收状态 IQCRejection:IQC拒收；
                    ASN oldAsn = (ASN)facade.GetASN(asn.StNo);
                    oldAsn.Status = ASNHeadStatus.IQCRejection;
                    facade.UpdateASN(oldAsn);
                    return "初检接收状态中全部为拒收状态";

                }
            }
            else
            {
                return "ASN单行项目状态必须为初检完成";

            }
            #endregion

            IQCFacade iqcFacade = new IQCFacade(DataProvider);
            object[] disdqMcodeList = facade.QueryAsnDetailForDqMcode(asn.StNo);
            if (disdqMcodeList == null)
                return "入库指令号对应在ASN明细表中不存在，不可申请IQC";


            //同一入库指令下，同一鼎桥物料编码，生成一个IQC检验单号。
            object[] dqMcodeList = facade.QueryAsnDetailForCreateIqc(asn.StNo);
            if (dqMcodeList == null)
                return "IQC检验单号已存在！";


            try
            {
                this.DataProvider.BeginTransaction();
                string iqcNo = string.Empty;
                foreach (ASNDetail dqMcode in dqMcodeList)
                {
                    //edit by sam 2016年3月21日 剔除拒收状态
                    object[] detailList = facade.QueryAsnDetailByStNo(asn.StNo, dqMcode.DQMCode, "Reject");
                    if (detailList != null)
                    {
                        #region AsnIQC
                        ASNDetail asnDetailobj = detailList[0] as ASNDetail;
                        string newIqcNo = this.CreateNewIqcNo(asnDetailobj.StNo, usrCode);
                        AsnIQC asnIqc = new AsnIQC();
                        asnIqc.IqcNo = newIqcNo;
                        iqcNo = newIqcNo;
                        asnIqc.IqcType = "";
                        asnIqc.StNo = asn.StNo;
                        asnIqc.InvNo = !string.IsNullOrEmpty(asn.InvNo) ? asn.InvNo : asn.StNo;


                        asnIqc.StType = asn.StType;	//	 STTYPE
                        asnIqc.Status = BenQGuru.eMES.Web.Helper.IQCStatus.IQCStatus_Release;	//	STATUS
                        asnIqc.AppDate = dbDateTime.DBDate;	//	MDATE
                        asnIqc.AppTime = dbDateTime.DBTime;	//	MTIME
                        asnIqc.InspDate = 0;	//	INSPDATE
                        asnIqc.InspTime = 0;	//	INSPTIME
                        asnIqc.CustmCode = asnDetailobj.CustMCode;	//	CUSTMCODE 华为物料号
                        asnIqc.MCode = asnDetailobj.MCode;	//	MCODE
                        asnIqc.DQMCode = asnDetailobj.DQMCode;	//	DQMCODE
                        asnIqc.MDesc = asnDetailobj.MDesc;	//	MDESC
                        // asnIqc.Qty = asnDetailobj.ReceiveQty;	//	QTY
                        asnIqc.QcStatus = "";	//	QCSTATUS IQC状态(Y:合格；N:不合格)
                        asnIqc.VendorCode = asn.VendorCode;	//	VENDORCODE
                        asnIqc.VendorMCode = asnDetailobj.VendorMCodeDesc;	//	VENDORMCODE
                        asnIqc.Remark1 = asn.Remark1;	//	REMARK1
                        asnIqc.CUser = usrCode;	//	CUSER
                        asnIqc.CDate = dbDateTime.DBDate;	//	CDATE
                        asnIqc.CTime = dbDateTime.DBTime;//	CTIME
                        asnIqc.MaintainDate = dbDateTime.DBDate;	//	MDATE
                        asnIqc.MaintainTime = dbDateTime.DBTime;	//	MTIME
                        asnIqc.MaintainUser = usrCode;		//	MUSER
                        foreach (ASNDetail asnDetail in detailList)
                        {
                            asnIqc.Qty += asnDetail.ReceiveQty;
                        }
                        iqcFacade.AddAsnIQC(asnIqc);
                        #endregion
                        foreach (ASNDetail asnDetail in detailList)
                        {
                            #region
                            AsnIQCDetail iqcDetail = new AsnIQCDetail();

                            #region  iqcDetail
                            iqcDetail.IqcNo = newIqcNo;	//	IQCNO	送检单号
                            iqcDetail.StNo = asnDetail.StNo;	//	STNO	ASN单号
                            iqcDetail.StLine = asnDetail.StLine;	//	STLINE	ASN单行项目
                            iqcDetail.CartonNo = asnDetail.CartonNo; 	//	CARTONNO	箱号条码
                            iqcDetail.Qty = asnDetail.ReceiveQty; 	//	QTY	送检数量
                            iqcDetail.QcPassQty = 0; 	//	QCPASSQTY	检验通过数量
                            iqcDetail.Unit = asnDetail.Unit; 	//	UNIT	单位
                            iqcDetail.NgQty = 0; 	//	NGQTY	缺陷品数
                            iqcDetail.ReturnQty = 0; 	//	ReturnQTY	退换货数量
                            iqcDetail.ReformQty = 0; 	//	ReformQTY	现场整改数量
                            iqcDetail.GiveQty = 0; 	//	GiveQTY	让步接收数量
                            iqcDetail.AcceptQty = 0; 	//	AcceptQTY	特采数量
                            iqcDetail.QcStatus = ""; 	//	QCSTATUS	 IQC状态(Y:合格；N:不合格)
                            iqcDetail.Remark1 = asnDetail.Remark1; 	//	REMARK1	备注
                            iqcDetail.CUser = usrCode;	//	CUSER
                            iqcDetail.CDate = dbDateTime.DBDate;	//	CDATE
                            iqcDetail.CTime = dbDateTime.DBTime;//	CTIME
                            iqcDetail.MaintainDate = dbDateTime.DBDate;	//	MDATE
                            iqcDetail.MaintainTime = dbDateTime.DBTime;	//	MTIME
                            iqcDetail.MaintainUser = usrCode;		//	MUSER
                            iqcFacade.AddAsnIQCDetail(iqcDetail);
                            #endregion

                            #region  AsnIqcDetailSN

                            object[] iqcDetailsnList = facade.GetSNbySTNo(asnDetail.StNo, asnDetail.StLine);
                            if (iqcDetailsnList != null)
                            {
                                foreach (Asndetailsn detailsn in iqcDetailsnList)
                                {
                                    AsnIqcDetailSN iqcDetailsn = new AsnIqcDetailSN();
                                    iqcDetailsn.IqcNo = newIqcNo; //	IQCNO	送检单号
                                    iqcDetailsn.StNo = asnDetail.StNo; //	STNO	ASN单号
                                    iqcDetailsn.StLine = asnDetail.StLine; //	STLINE	ASN单行项目
                                    iqcDetailsn.Sn = detailsn.Sn;
                                    iqcDetailsn.CartonNo = asnDetail.CartonNo; //	CARTONNO	箱号条码
                                    iqcDetailsn.StNo = asnDetail.StNo; //	SN	SN条码
                                    iqcDetailsn.QcStatus = ""; //	QCSTATUS	SN IQC状态(Y:合格；N:不合格)
                                    iqcDetailsn.Remark1 = asnDetail.Remark1; //	REMARK1	备注
                                    iqcDetailsn.CUser = usrCode; //	CUSER
                                    iqcDetailsn.CDate = dbDateTime.DBDate; //	CDATE
                                    iqcDetailsn.CTime = dbDateTime.DBTime; //	CTIME
                                    iqcDetailsn.MaintainDate = dbDateTime.DBDate; //	MDATE
                                    iqcDetailsn.MaintainTime = dbDateTime.DBTime; //	MTIME
                                    iqcDetailsn.MaintainUser = usrCode; //	MUSER
                                    iqcFacade.AddAsnIqcDetailSN(iqcDetailsn);
                                }
                            }
                            #endregion
                            #endregion
                        }
                    }
                    //判断是否是免检物料

                    BenQGuru.eMES.Domain.MOModel.Material mar = wFacade.GetMaterialFromDQMCode(dqMcode.DQMCode);
                    int count = wFacade.GetStockRecordCount(dbDateTime.DBDate, dbDateTime.DBTime, mar.MCode);
                    if (count > 0)
                    {
                        //是免检物料
                        try
                        {
                            ToSTS(iqcNo, usrCode);
                        }
                        catch (Exception ex)
                        {

                            this.DataProvider.RollbackTransaction();
                            throw ex;
                        }
                    }
                }
                // 3>	IQC检验单数据来源与ASN主表(TBLASN)、ASN明细表(TBLASNDETAIL)、ASN明细SN表(TBLASNDETAILSN)，
                //保存数据表有：送检单(TBLASNIQC)、送检单明细(TBLASNIQCDETAIL)、送检单明细SN表(TBLASNIQCDETAILSN)，
                //注：送检数量(TBLASNIQCDETAIL.QTY)为ASN明细表中的接收数量(TBLASNDETAIL.ReceiveQTY)
                //4>	IQC送检单号规则：IQC+入库指令号+两位流水号，如：IQCASN00000101

                //5>	更新ASN主表(TBLASN)状态为：IQC:IQC
                var oldasn = (ASN)facade.GetASN(asn.StNo);
                if (oldasn != null)
                {
                    if (!(oldasn.Status == ASN_STATUS.ASN_Close || oldasn.Status == ASN_STATUS.ASN_Cancel || oldasn.Status == ASN_STATUS.ASN_OnLocation || oldasn.Status == ASN_STATUS.ASN_IQC))
                    {
                        oldasn.Status = "IQC";
                        facade.UpdateASN(oldasn);
                    }
                }

                this.DataProvider.CommitTransaction();
                return "申请IQC成功";


            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }

        }
    }

}
