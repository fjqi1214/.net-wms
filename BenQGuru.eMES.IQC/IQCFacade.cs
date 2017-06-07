using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using BenQGuru.eMES.AlertModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Domain.OQC;
using System.Data;

namespace BenQGuru.eMES.IQC
{
    public class IQCFacade : MarshalByRefObject
    {
        #region Common

        private FacadeHelper _helper = null;
        private IDomainDataProvider _domainDataProvider = null;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                }

                return _domainDataProvider;
            }
        }

        public IQCFacade()
        {
        }

        public IQCFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        #endregion

        //#region ASN

        //public ASN CreateNewASN()
        //{
        //    return new ASN();
        //}

        //public void AddASN(ASN asn)
        //{
        //    this._helper.AddDomainObject(asn);
        //}

        //public void DeleteASN(ASN asn)
        //{
        //    this._helper.DeleteDomainObject(asn);
        //}

        //public void UpdateASN(ASN asn)
        //{
        //    this._helper.UpdateDomainObject(asn);
        //}

        //public object GetASN(string stNo)
        //{
        //    return this.DataProvider.CustomSearch(typeof(ASN), new object[] { stNo });
        //}

        //public bool CreateIQCFromSRM(string ticketType, string asnPO, string materialList, string userCode)
        //{
        //    bool returnValue = false;

        //    ticketType = ticketType.Trim().ToUpper();

        //    string srmDBLink = GetSRMDBLink();
        //    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

        //    string sqlInner = string.Empty;

        //    //查询SRM基本数据的SQL    
        //    if (ticketType == "ASN")
        //    {
        //        sqlInner = string.Empty;
        //        sqlInner += "SELECT DISTINCT sn.stno AS asnpo, sn.ststatus, sn.plantcode, sn.vendorcode, od.shipinv, snd.STLINE,snd.ORDERNO,snd.ORDERLINE,snd.ITEMCODE,snd.ITEMNAME,snd.SHIPDATE,snd.PLANQTY,snd.SHIPQTY,snd.STDSTATUS, ";
        //        sqlInner += "snd.CREATEDATE,snd.CREATETIME,snd.MEMO,snd.ADDITION1,snd.ADDITION2,snd.STNO,snd.SSDATE as plandate,snd.SSLINE,snd.LOGDATE,snd.LOGTIME,snd.LOGUSER, ";
        //        sqlInner += "snd.SSQTY,snd.CREATEUSER,snd.RECEIVEQTY,snd.CHECKSTATUS,snd.CHECKQTY,snd.UNIT, i.invuser, snd.shipqty AS realreceiveqty, snd.ssqty AS realplanqty  ";
        //        sqlInner += "FROM asndetail{0} snd ";
        //        sqlInner += "INNER JOIN asn{0} sn ON sn.stno = snd.stno ";
        //        sqlInner += "INNER JOIN orderdetail{0} od ON od.orderno = snd.orderno ";
        //        sqlInner += "    AND od.orderline = snd.orderline ";
        //        sqlInner += "LEFT JOIN inv{0} i ON i.plantcode = sn.plantcode ";
        //        sqlInner += "    AND i.companycode = sn.companycode ";
        //        sqlInner += "    AND i.itemcode = snd.itemcode ";
        //        sqlInner += "    AND od.shipinv = i.wh ";
        //        sqlInner += "WHERE 1 = 1 ";
        //        sqlInner += "AND sn.stno = '{1}' ";
        //        sqlInner += "AND sn.ststatus IN ('" + IQCStatus.IQCStatus_Release + "') ";
        //        sqlInner += "AND snd.stdstatus IN ('" + IQCStatus.IQCStatus_Release + "') ";
        //        sqlInner = string.Format(sqlInner, srmDBLink, asnPO);
        //    }
        //    else    //PO
        //    {
        //        sqlInner = string.Empty;
        //        sqlInner += "SELECT DISTINCT 'PO' || pod.orderno AS asnpo, pod.palantcode AS plantcode, i.invuser, ";
        //        sqlInner += "    po.vendorcode, pod.orderline AS stline, '" + IQCStatus.IQCStatus_WaitCheck + "' AS ststatus, 0 AS plandate, 0 AS planqty, pod.planqty AS receiveqty, pod.planqty AS realreceiveqty, 0 AS realplanqty, ";
        //        sqlInner += "    pod.shipinv, pod.itemcode, pod.orderno, pod.orderline, pod.unit ";
        //        sqlInner += "FROM purchorder{0} po ";
        //        sqlInner += "INNER JOIN orderdetail{0} pod ON po.orderno = pod.orderno ";
        //        sqlInner += "LEFT JOIN inv{0} i ON i.plantcode = pod.palantcode ";
        //        sqlInner += "    AND i.companycode = po.companycode ";
        //        sqlInner += "    AND i.itemcode = pod.itemcode ";
        //        sqlInner += "    AND pod.shipinv = i.wh ";
        //        sqlInner += "WHERE 1 = 1 ";
        //        sqlInner += "AND po.orderno = '{1}' ";
        //        sqlInner += "AND pod.itemcode IN ({2}) ";

        //        string[] list = materialList.Split(',');
        //        for (int i = 0; i < list.Length; i++)
        //        {
        //            list[i] = "'" + list[i].Trim() + "'";
        //        }
        //        materialList = string.Join(",", list);

        //        sqlInner = string.Format(sqlInner, srmDBLink, asnPO, materialList);
        //    }

        //    //查询出其他相关数据的SQL
        //    string sql = string.Empty;
        //    sql += "SELECT tblitemclass.secondclass, srm.invuser, DECODE(tblshiptostock.active, NULL, 'N', 'Y') AS shiptostock, tblmaterial.rohs, srm.asnpo AS stno, srm.stline, ";
        //    sql += "srm.plantcode AS orgid, srm.shipinv AS storageid, srm.vendorcode, srm.ststatus, srm.itemcode, srm.orderno, srm.orderline, srm.plandate, srm.planqty, srm.receiveqty, srm.realreceiveqty, srm.realplanqty, srm.unit ";
        //    sql += "FROM (" + sqlInner + ") srm ";
        //    sql += "LEFT JOIN tblmaterial ON srm.itemcode = tblmaterial.mcode ";
        //    sql += "LEFT JOIN tblitemclass ON tblmaterial.mgroup = tblitemclass.itemgroup ";
        //    sql += "LEFT JOIN tblshiptostock ON srm.itemcode = tblshiptostock.itemcode ";
        //    sql += "    AND srm.vendorcode = tblshiptostock.vendorcode ";
        //    sql += "    AND tblshiptostock.active = 'Y' ";
        //    sql += "    AND " + dbDateTime.DBDate.ToString() + " BETWEEN tblshiptostock.effdate AND tblshiptostock.ivldate ";
        //    sql += "ORDER BY tblitemclass.secondclass, srm.invuser, tblshiptostock.active, tblmaterial.rohs, srm.asnpo, srm.stline ";

        //    object[] srmASNDetailList = this.DataProvider.CustomQuery(typeof(SRMASNDetail), new SQLCondition(sql));

        //    if (srmASNDetailList == null || srmASNDetailList.Length <= 0)
        //    {
        //        return returnValue;
        //    }

        //    string oldIQCString = string.Empty;
        //    string newIQCString = string.Empty;
        //    int stNoSeq = 0;

        //    try
        //    {
        //        this.DataProvider.BeginTransaction();

        //        for (int i = 0; i < srmASNDetailList.Length; i++)
        //        {
        //            SRMASNDetail detail = (SRMASNDetail)srmASNDetailList[i];

        //            newIQCString = (detail.SecondClass == null ? string.Empty : detail.SecondClass.Trim().ToUpper()) + "\t";
        //            newIQCString += (detail.InventoryUser == null ? string.Empty : detail.InventoryUser.Trim().ToUpper()) + "\t";
        //            newIQCString += (detail.ShipToStock == null ? "N" : detail.ShipToStock.Trim().ToUpper()) + "\t";
        //            newIQCString += (detail.ROHS == null ? "N" : detail.ROHS.Trim().ToUpper()) + "\t";

        //            if (oldIQCString != newIQCString)
        //            {
        //                ASN asn = (ASN)GetASN(detail.STNo);

        //                if (asn == null)
        //                {
        //                    //新增ASN
        //                    asn = CreateNewASN();

        //                    asn.STNo = detail.STNo;
        //                    asn.OrganizationID = detail.OrganizationID;
        //                    asn.VendorCode = detail.VendorCode;
        //                    asn.STStatus = detail.STStatus;
        //                    asn.SyncStatus = IQCSyncStatus.IQCSyncStatus_New;
        //                    asn.Flag = ticketType;
        //                    asn.MaintainUser = userCode;
        //                    asn.MaintainDate = dbDateTime.DBDate;
        //                    asn.MaintainTime = dbDateTime.DBTime;

        //                    AddASN(asn);
        //                }

        //                //新增IQCHead
        //                stNoSeq = GetNextSTNoSeq(detail.STNo);
        //                IQCHead iqcHead = CreateNewIQCHead();

        //                iqcHead.IQCNo = detail.STNo + "-" + stNoSeq.ToString();
        //                iqcHead.STNo = detail.STNo;
        //                iqcHead.STNoSeq = stNoSeq;
        //                iqcHead.Status = IQCStatus.IQCStatus_New;
        //                iqcHead.InventoryUser = detail.InventoryUser;
        //                iqcHead.ROHS = (detail.ROHS != null && detail.ROHS.Trim().ToUpper() == "Y") ? "Y" : "N";
        //                iqcHead.STS = detail.ShipToStock;
        //                iqcHead.Attribute = IQCHeadAttribute.IQCHeadAttribute_Normal;
        //                iqcHead.MaintainUser = userCode;
        //                iqcHead.MaintainDate = dbDateTime.DBDate;
        //                iqcHead.MaintainTime = dbDateTime.DBTime;

        //                AddIQCHead(iqcHead);

        //                oldIQCString = newIQCString;
        //            }

        //            //新增IQCDetail
        //            IQCDetail iqcDetail = CreateNewIQCDetail();

        //            iqcDetail.IQCNo = detail.STNo + "-" + stNoSeq.ToString();
        //            iqcDetail.STNo = detail.STNo;
        //            iqcDetail.STLine = detail.STLine;
        //            iqcDetail.ItemCode = detail.ItemCode;
        //            iqcDetail.OrderNo = detail.OrderNo;
        //            iqcDetail.OrderLine = detail.OrderLine;
        //            iqcDetail.STDStatus = IQCStatus.IQCStatus_New;
        //            iqcDetail.ReceiveQty = detail.RealReceiveQTY;
        //            iqcDetail.CheckStatus = null;
        //            iqcDetail.Unit = detail.Unit;
        //            iqcDetail.Type = IQCReceiveType.IQCReceiveType_Normal;
        //            iqcDetail.Attribute = IQCDetailAttribute.IQCDetailAttribute_Normal;
        //            iqcDetail.MaintainUser = userCode;
        //            iqcDetail.MaintainDate = dbDateTime.DBDate;
        //            iqcDetail.MaintainTime = dbDateTime.DBTime;

        //            iqcDetail.OrganizationID = detail.OrganizationID;
        //            iqcDetail.StorageID = detail.StorageID;
        //            iqcDetail.ConcessionStatus = "N";

        //            AddIQCDetail(iqcDetail);
        //        }

        //        this.DataProvider.CommitTransaction();

        //        returnValue = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.DataProvider.RollbackTransaction();
        //        throw ex;
        //    }

        //    return returnValue;
        //}

        //public bool CreateIQC(string ticketType, string asnPO, string materialList, string userCode)
        //{
        //    bool returnValue = false;

        //    ticketType = ticketType.Trim().ToUpper();
        //    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

        //    string sqlInner = string.Empty;

        //    //查询SRM基本数据的SQL    
        //    if (ticketType == "PO")
        //    {
        //        sqlInner = string.Empty;
        //        sqlInner += "SELECT DISTINCT TBLINVReceiptDetail.ReceiptNO AS asnpo, TBLINVReceiptDetail.IQCSTATUS AS ststatus, TBLINVReceipt.vendorcode, TBLINVReceiptDetail.invuser,TBLINVReceiptdetail.receiptline AS stline, ";
        //        sqlInner += "TBLINVReceiptdetail.orderno,TBLINVReceiptdetail.orderline, TBLINVReceiptdetail.itemcode,tblinvreceiptdetail.planqty, TBLINVReceipt.createdate, TBLINVReceipt.createtime, ";
        //        sqlInner += "TBLINVReceiptDetail.MEMO, TBLINVReceipt.createuser, TBLINVReceipt.orgid,TBLINVReceipt.storageid FROM TBLINVReceiptdetail, TBLINVReceipt WHERE TBLINVReceiptdetail.receiptno = TBLINVReceipt.receiptno ";
        //        sqlInner += " AND TBLINVReceipt. RECSTATUS='NEW'  AND TBLINVReceiptdetail.receiptno = '{0}' ";
        //        sqlInner = string.Format(sqlInner, asnPO);
        //    }

        //    //查询出其他相关数据的SQL
        //    string sql = string.Empty;
        //    sql += "SELECT DECODE(tblshiptostock.active, NULL, 'N', 'Y') AS shiptostock, tblmaterial.rohs, srm.asnpo AS stno, srm.stline, ";
        //    sql += "srm.orgid, srm.storageid, srm.vendorcode,srm.invuser ,srm.ststatus, srm.itemcode, srm.orderno, srm.orderline, srm.planqty as ReceiveQty, srm.memo AS PurchaseMEMO  ";
        //    sql += "FROM (" + sqlInner + ") srm ";
        //    sql += "LEFT JOIN tblmaterial ON srm.itemcode = tblmaterial.mcode AND SRM.orgid=TBLMATERIAL.Orgid ";
        //    //sql += "LEFT JOIN tblitemclass ON tblmaterial.mgroup = tblitemclass.itemgroup ";
        //    sql += "LEFT JOIN tblshiptostock ON srm.itemcode = tblshiptostock.itemcode ";
        //    sql += "    AND srm.vendorcode = tblshiptostock.vendorcode ";
        //    sql += "    AND tblshiptostock.active = 'Y' ";
        //    sql += "    AND " + dbDateTime.DBDate.ToString() + " BETWEEN tblshiptostock.effdate AND tblshiptostock.ivldate ";
        //    sql += "ORDER BY  tblshiptostock.active, tblmaterial.rohs, srm.asnpo, srm.stline ";

        //    object[] srmASNDetailList = this.DataProvider.CustomQuery(typeof(SRMASNDetail), new SQLCondition(sql));

        //    if (srmASNDetailList == null || srmASNDetailList.Length <= 0)
        //    {
        //        return returnValue;
        //    }

        //    string oldIQCString = string.Empty;
        //    string newIQCString = string.Empty;
        //    int stNoSeq = 0;

        //    try
        //    {
        //        this.DataProvider.BeginTransaction();

        //        for (int i = 0; i < srmASNDetailList.Length; i++)
        //        {
        //            SRMASNDetail detail = (SRMASNDetail)srmASNDetailList[i];

        //            //newIQCString = (detail.SecondClass == null ? string.Empty : detail.SecondClass.Trim().ToUpper()) + "\t";
        //            newIQCString += (detail.InventoryUser == null ? string.Empty : detail.InventoryUser.Trim().ToUpper()) + "\t";
        //            newIQCString += (detail.ShipToStock == null ? "N" : detail.ShipToStock.Trim().ToUpper()) + "\t";
        //            newIQCString += (detail.ROHS == null ? "N" : detail.ROHS.Trim().ToUpper()) + "\t";

        //            if (oldIQCString != newIQCString)
        //            {
        //                ASN asn = (ASN)GetASN(detail.STNo);

        //                if (asn == null)
        //                {
        //                    //新增ASN
        //                    asn = CreateNewASN();

        //                    asn.STNo = detail.STNo;
        //                    asn.OrganizationID = detail.OrganizationID;
        //                    if (detail.VendorCode != "")
        //                    {
        //                        asn.VendorCode = detail.VendorCode;
        //                    }
        //                    else
        //                    {
        //                        asn.VendorCode = " ";
        //                    }
        //                    asn.STStatus = detail.STStatus;
        //                    asn.SyncStatus = IQCSyncStatus.IQCSyncStatus_New;
        //                    asn.Flag = ticketType;
        //                    asn.MaintainUser = userCode;
        //                    asn.MaintainDate = dbDateTime.DBDate;
        //                    asn.MaintainTime = dbDateTime.DBTime;

        //                    AddASN(asn);
        //                }

        //                //新增IQCHead
        //                stNoSeq = GetNextSTNoSeq(detail.STNo);
        //                IQCHead iqcHead = CreateNewIQCHead();

        //                iqcHead.IQCNo = detail.STNo + "-" + stNoSeq.ToString();
        //                iqcHead.STNo = detail.STNo;
        //                iqcHead.STNoSeq = stNoSeq;
        //                iqcHead.Status = IQCStatus.IQCStatus_New;
        //                iqcHead.InventoryUser = detail.InventoryUser;
        //                iqcHead.ROHS = (detail.ROHS != null && detail.ROHS.Trim().ToUpper() == "Y") ? "Y" : "N";
        //                iqcHead.STS = detail.ShipToStock;
        //                iqcHead.Attribute = IQCHeadAttribute.IQCHeadAttribute_Normal;
        //                iqcHead.MaintainUser = userCode;
        //                iqcHead.MaintainDate = dbDateTime.DBDate;
        //                iqcHead.MaintainTime = dbDateTime.DBTime;


        //                AddIQCHead(iqcHead);

        //                oldIQCString = newIQCString;
        //            }

        //            //新增IQCDetail
        //            IQCDetail iqcDetail = CreateNewIQCDetail();

        //            iqcDetail.IQCNo = detail.STNo + "-" + stNoSeq.ToString();
        //            iqcDetail.STNo = detail.STNo;
        //            iqcDetail.STLine = detail.STLine;
        //            iqcDetail.ItemCode = detail.ItemCode;
        //            iqcDetail.OrderNo = detail.OrderNo;
        //            iqcDetail.OrderLine = detail.OrderLine;
        //            iqcDetail.STDStatus = IQCStatus.IQCStatus_New;
        //            iqcDetail.ReceiveQty = detail.ReceiveQty;
        //            iqcDetail.CheckStatus = null;
        //            iqcDetail.Unit = detail.Unit;
        //            iqcDetail.Type = IQCReceiveType.IQCReceiveType_Normal;
        //            iqcDetail.Attribute = IQCDetailAttribute.IQCDetailAttribute_Normal;
        //            iqcDetail.MaintainUser = userCode;
        //            iqcDetail.MaintainDate = dbDateTime.DBDate;
        //            iqcDetail.MaintainTime = dbDateTime.DBTime;

        //            iqcDetail.OrganizationID = detail.OrganizationID;
        //            iqcDetail.StorageID = detail.StorageID;
        //            iqcDetail.ConcessionStatus = "N";
        //            iqcDetail.PurchaseMEMO = detail.PurchaseMEMO;
        //            AddIQCDetail(iqcDetail);
        //        }

        //        this.DataProvider.CommitTransaction();

        //        returnValue = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.DataProvider.RollbackTransaction();
        //        throw ex;
        //    }

        //    return returnValue;
        //}

        //public bool IsFromASN(string stNo)
        //{
        //    bool returnValue = false;

        //    ASN asn = (ASN)GetASN(stNo);
        //    returnValue = asn != null && asn.Flag == IQCTicketType.IQCTicketType_ASN;

        //    return returnValue;
        //}

        //private int GetNextSTNoSeq(string stNo)
        //{
        //    string sql = "";
        //    sql += "SELECT NVL(MAX(stno_seq), 0) + 1 AS stno_seq ";
        //    sql += "FROM tblasniqc ";
        //    sql += "WHERE stno = '" + stNo.Trim().ToUpper() + "' ";

        //    object[] list = this.DataProvider.CustomQuery(typeof(IQCHead), new SQLCondition(sql));
        //    return (list[0] as IQCHead).STNoSeq;
        //}

        //private void LockASN(string stNo)
        //{
        //    string sql = "SELECT * FROM tblasn WHERE stno = '{0}' FOR UPDATE ";
        //    sql = string.Format(sql, stNo.Trim().ToUpper());
        //    this.DataProvider.CustomQuery(typeof(ASN), new SQLCondition(sql));
        //}

        //#endregion

        #region IQCHead

        public IQCHead CreateNewIQCHead()
        {
            return new IQCHead();
        }

        public void AddIQCHead(IQCHead iqcHead)
        {
            this._helper.AddDomainObject(iqcHead);
        }

        public void DeleteIQCHead(IQCHead iqcHead)
        {
            this._helper.DeleteDomainObject(iqcHead);
        }

        public void UpdateIQCHead(IQCHead iqcHead)
        {
            this._helper.UpdateDomainObject(iqcHead);
        }

        public object GetIQCHead(string iqcNo)
        {
            return this.DataProvider.CustomSearch(typeof(IQCHead), new object[] { iqcNo });
        }

        public object GetIQCHeadWithVendor(string iqcNo)
        {
            object[] iqcHeadList = QueryIQCHeadWithVendor(iqcNo, string.Empty, string.Empty, string.Empty, string.Empty, -1, -1,
                string.Empty, string.Empty, string.Empty, int.MinValue, int.MaxValue);

            if (iqcHeadList != null && iqcHeadList.Length > 0)
            {
                return iqcHeadList[0];
            }
            else
            {
                return null;
            }
        }

        public object[] QueryIQCHeadWithVendor(string stNo)
        {
            string sql = string.Empty;
            sql += "SELECT " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(IQCHead)) + ", tblasn.vendorcode, tblvendor.vendorname, ";
            sql += "usera.username as applicantusername, userb.username as inspectorusername, userc.username as invusername ";
            sql += "FROM tblasniqc ";
            sql += "INNER JOIN tblasn ON tblasniqc.stno = tblasn.stno ";
            sql += "LEFT OUTER JOIN tblvendor ON tblasn.vendorcode = tblvendor.vendorcode ";
            sql += "LEFT OUTER JOIN tbluser usera ON tblasniqc.applicant = usera.usercode ";
            sql += "LEFT OUTER JOIN tbluser userb ON tblasniqc.inspector = userb.usercode ";
            sql += "LEFT OUTER JOIN tbluser userc ON tblasniqc.invuser = userc.usercode ";
            sql += "LEFT OUTER JOIN tblinvreceipt ON tblasniqc.stno = tblinvreceipt.receiptno ";
            sql += "LEFT OUTER JOIN tblstorage ON tblstorage.storagecode = tblinvreceipt.storageid ";

            sql += "WHERE 1 = 1 ";

            if (stNo.Trim().Length > 0)
            {
                sql += "AND tblasniqc.stno = '" + stNo.Trim().ToUpper() + "' ";
            }

            return this.DataProvider.CustomQuery(typeof(IQCHeadWithVendor), new SQLCondition(sql));
        }

        public object[] QueryIQCHeadWithVendor(string iqcNo, string stNo, string status, string rohs, string vendorCode,
            int appStartDate, int appEndDate, string sts, string result, string materialCodeList, int inclusive, int exclusive)
        {
            string sql = string.Empty;
            sql += "SELECT " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(IQCHead)) + ", tblasn.vendorcode, tblvendor.vendorname, ";
            sql += "usera.username as applicantusername, userb.username as inspectorusername, userc.username as invusername,";
            sql += " DECODE((SELECT COUNT(*) ";
            sql += "     FROM TBLIQCDETAIL a";
            sql += "    INNER JOIN TBLINVReceiptDetail b";
            sql += "       ON a.Stno = b.Receiptno";
            sql += "      AND a.Stline = b.Receiptline";
            sql += "    WHERE a.Iqcno = tblasniqc.Iqcno";
            sql += "      AND b.Isinstorage <> 'Y'), 0, 'Y', 'N') AS IsAllInStorage ";
            sql += "FROM tblasniqc ";
            sql += "INNER JOIN tblasn ON tblasniqc.stno = tblasn.stno ";
            sql += "LEFT OUTER JOIN tblvendor ON tblasn.vendorcode = tblvendor.vendorcode ";
            sql += "LEFT OUTER JOIN tbluser usera ON tblasniqc.applicant = usera.usercode ";
            sql += "LEFT OUTER JOIN tbluser userb ON tblasniqc.inspector = userb.usercode ";
            sql += "LEFT OUTER JOIN tbluser userc ON tblasniqc.invuser = userc.usercode ";
            sql += "LEFT OUTER JOIN tblinvreceipt ON tblasniqc.stno = tblinvreceipt.receiptno ";
            sql += "LEFT OUTER JOIN tblstorage ON tblstorage.storagecode = tblinvreceipt.storageid ";

            sql += GetQueryIQCHeadWithVendorWhereSql(iqcNo, stNo, status, rohs, vendorCode, appStartDate, appEndDate, sts, result, materialCodeList);
            //sql += "ORDER BY tblasniqc.iqcno  ";
            return this.DataProvider.CustomQuery(typeof(IQCHeadWithVendor), new PagerCondition(sql, "iqcno ", inclusive, exclusive));
        }

        public int QueryIQCHeadWithVendorCount(string iqcNo, string stNo, string status, string rohs, string vendorCode,
            int appStartDate, int appEndDate, string sts, string result, string materialCodeList)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) ";
            sql += "FROM tblasniqc ";
            sql += "INNER JOIN tblasn ON tblasniqc.stno = tblasn.stno ";
            sql += "LEFT OUTER JOIN tblvendor ON tblasn.vendorcode = tblvendor.vendorcode ";
            sql += GetQueryIQCHeadWithVendorWhereSql(iqcNo, stNo, status, rohs, vendorCode, appStartDate, appEndDate, sts, result, materialCodeList);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private string GetQueryIQCHeadWithVendorWhereSql(string iqcNo, string stNo, string statusList, string rohs, string vendorCode,
            int appStartDate, int appEndDate, string sts, string result, string materialCodeList)
        {
            string sql = "WHERE 1 = 1 ";

            if (iqcNo.Trim().Length > 0)
            {
                sql += "AND tblasniqc.iqcno LIKE '%" + iqcNo.Trim().ToUpper() + "%' ";
            }

            if (stNo.Trim().Length > 0)
            {
                sql += "AND tblasniqc.stno LIKE '%" + stNo.Trim().ToUpper() + "%' ";
            }

            if (statusList.Trim().Length > 0)
            {
                if (statusList.IndexOf(",") >= 0)
                {
                    string[] list = statusList.Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i].Trim() + "'";
                    }
                    statusList = string.Join(",", list);

                    sql += "AND tblasniqc.status IN (" + statusList.Trim() + ") ";
                }
                else
                {
                    sql += "AND tblasniqc.status = '" + statusList.Trim() + "' ";
                }
            }

            if (rohs.Trim().Length > 0)
            {
                sql += "AND tblasniqc.rohs = '" + rohs.Trim().ToUpper() + "' ";
            }

            if (vendorCode.Trim().Length > 0)
            {
                sql += "AND tblasn.vendorcode in (" + FormatHelper.ProcessQueryValues(vendorCode.Trim().ToUpper()) + " ) ";
            }

            if (appStartDate > 0)
            {
                sql += "AND tblasniqc.appdate >= " + appStartDate + " ";
            }

            if (appEndDate > 0)
            {
                sql += "AND tblasniqc.appdate <=" + appEndDate + " ";
            }

            if (sts.Trim().Length > 0)
            {
                sql += "AND tblasniqc.sts = '" + sts + "' ";
            }

            if (result.Trim().Length > 0)
            {
                sql += "AND tblasniqc.iqcno IN (SELECT iqcno FROM tbliqcdetail WHERE checkstatus = '" + result.Trim() + "')";
            }

            if (materialCodeList.Trim().Length > 0)
            {
                if (materialCodeList.IndexOf(",") >= 0)
                {
                    string[] list = materialCodeList.Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i].Trim() + "'";
                    }
                    materialCodeList = string.Join(",", list);

                    sql += "AND tblasniqc.iqcno IN (SELECT iqcno FROM tbliqcdetail WHERE itemcode IN (" + materialCodeList.Trim().ToUpper() + "))";
                }
                else
                {
                    sql += "AND tblasniqc.iqcno IN (SELECT iqcno FROM tbliqcdetail WHERE itemcode LIKE '%" + materialCodeList.Trim().ToUpper() + "%')";
                }
            }

            return sql;
        }


        //public void DeleteIQCHeadAll(IQCHead[] iqcHeadList)
        //{
        //    try
        //    {
        //        this.DataProvider.BeginTransaction();

        //        for (int i = 0; i < iqcHeadList.Length; i++)
        //        {
        //            object[] iqcDetailList = QueryIQCDetail(iqcHeadList[i].IQCNo, string.Empty);
        //            if (iqcDetailList != null)
        //            {
        //                for (int j = 0; j < iqcDetailList.Length; j++)
        //                {
        //                    DeleteIQCDetail((IQCDetail)iqcDetailList[j]);
        //                }
        //            }

        //            DeleteIQCHead(iqcHeadList[i]);

        //            object[] iqcHeadListBySTNo = QueryIQCHeadWithVendor(iqcHeadList[i].STNo);

        //            if (iqcHeadListBySTNo == null || iqcHeadListBySTNo.Length <= 0)
        //            {
        //                object asn = GetASN(iqcHeadList[i].STNo);

        //                if (asn != null)
        //                {
        //                    DeleteASN((ASN)asn);
        //                }
        //            }
        //        }

        //        this.DataProvider.CommitTransaction();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.DataProvider.RollbackTransaction();
        //        throw ex;
        //    }
        //}

        #endregion

        #region IQCDetail

        public IQCDetail CreateNewIQCDetail()
        {
            return new IQCDetail();
        }

        public IQCDetailWithMaterial CreateNewIQCDetailWithMaterial()
        {
            return new IQCDetailWithMaterial();
        }

        public void AddIQCDetail(IQCDetail iqcDetail)
        {
            this._helper.AddDomainObject(iqcDetail);
        }

        public void DeleteIQCDetail(IQCDetail iqcDetail)
        {
            this._helper.DeleteDomainObject(iqcDetail);
        }

        public void DeleteIQCDetail(IQCDetail[] IQCDetail)
        {
            try
            {
                this.DataProvider.BeginTransaction();

                for (int i = 0; i < IQCDetail.Length; i++)
                {
                    DeleteIQCDetail(IQCDetail[i]);
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_DeleteIQCDetail", ex);
            }
        }

        public void UpdateIQCDetail(IQCDetail iqcDetail)
        {
            this._helper.UpdateDomainObject(iqcDetail);
        }

        public object GetIQCDetail(string iqcNo, int stLine)
        {
            return this.DataProvider.CustomSearch(typeof(IQCDetail), new object[] { iqcNo, stLine });
        }

        public object[] QueryIQCDetail(string iqcNo, string stdstatus)
        {
            string sql = string.Empty;
            sql += "SELECT * FROM tbliqcdetail WHERE iqcno = '" + iqcNo + "' ";
            if (stdstatus.Trim().Length > 0)
            {
                sql += "AND checkstatus = '" + stdstatus + "' ";
            }
            return this.DataProvider.CustomQuery(typeof(IQCDetail), new SQLCondition(sql));
        }

        public object[] QueryIQCDetail(string stNo)
        {
            string sql = string.Empty;
            sql += "SELECT * FROM tbliqcdetail WHERE stno = '" + stNo + "' ";
            return this.DataProvider.CustomQuery(typeof(IQCDetail), new SQLCondition(sql));
        }

        public object[] QueryIQCDetail(string stNo, int stLine)
        {
            string sql = string.Empty;
            sql += "SELECT * FROM tbliqcdetail WHERE stno = '" + stNo + "' and STLINE=" + stLine;
            return this.DataProvider.CustomQuery(typeof(IQCDetail), new SQLCondition(sql));
        }

        public object[] QueryIQCDetailByIQCNO(string iqcNo)
        {
            string sql = string.Empty;
            sql += "SELECT * FROM tbliqcdetail WHERE iqcno = '" + iqcNo + "' ";
            return this.DataProvider.CustomQuery(typeof(IQCDetail), new SQLCondition(sql));
        }

        public object[] QueryIQCDetailWithMaterial(string iqcNo, int inclusive, int exclusive)
        {
            return QueryIQCDetailWithMaterial(iqcNo, string.Empty, string.Empty, inclusive, exclusive);
        }

        public object[] QueryIQCDetailWithMaterial(string iqcNo, string includedStatus, string excludedStatus, int inclusive, int exclusive)
        {
            string sql = string.Empty;
            sql += " SELECT tbliqcdetail.*,tblmaterial.mdesc,tblinvreceiptdetail.memo as invreceiptdetailmemo,tblinvreceiptdetail.venderlotno,tblinvreceiptdetail.IsInStorage FROM tbliqcdetail ";
            sql += " LEFT outer JOIN tblmaterial ON  tbliqcdetail.itemcode=tblmaterial.mcode ";
            sql += " AND tbliqcdetail.orgid=tblmaterial.orgid ";

            sql += " LEFT outer JOIN tblinvreceiptdetail ON  tbliqcdetail.stno=tblinvreceiptdetail.receiptno ";
            sql += " AND tbliqcdetail.stline=tblinvreceiptdetail.receiptline ";

            sql += " WHERE  1=1 ";

            if (iqcNo.Trim().Length > 0)
            {
                sql += " AND tbliqcdetail.iqcno= '" + iqcNo.Trim().ToUpper() + "' ";
            }

            if (excludedStatus.Trim().Length > 0)
            {
                sql += " AND tbliqcdetail.stdstatus <> '" + excludedStatus.Trim() + "' ";
            }

            sql += "ORDER BY tbliqcdetail.iqcno, tbliqcdetail.stline ,tbliqcdetail.stno";

            return this.DataProvider.CustomQuery(typeof(IQCDetailWithMaterial), new PagerCondition(sql, inclusive, exclusive));

        }

        public int QueryIQCDetailWithMaterialCount(string iqcNo)
        {
            return QueryIQCDetailWithMaterialCount(iqcNo, string.Empty, string.Empty);
        }

        public int QueryIQCDetailWithMaterialCount(string iqcNo, string includedStatus, string excludedStatus)
        {
            string sql = string.Empty;
            sql += " SELECT count(*) FROM tbliqcdetail ";
            sql += " WHERE 1=1 ";

            if (iqcNo.Trim().Length > 0)
            {
                sql += " AND iqcno='" + iqcNo + "' ";
            }

            if (excludedStatus.Trim().Length > 0)
            {
                sql += " AND stdstatus <> '" + excludedStatus.Trim() + "' ";
            }

            sql += " ORDER by stline";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int QueryIQCDetailConcessionCount(string stNo, string itemCode, string concessionstatus)
        {
            string sql = string.Empty;
            sql += "SELECT count(*) FROM tbliqcdetail a, tblasn b ";
            sql += " WHERE a.stno = b.stno ";
            sql += " AND a.itemcode = '" + itemCode.Trim() + "' ";
            sql += " AND a.concessionstatus='" + concessionstatus + "'";
            sql += " AND b.vendorcode = (SELECT vendorcode FROM tblasn WHERE  stno = '" + stNo.Trim() + "') ";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryIQCDetailForReceive(string iqcNo, string stNo, string vendorCode, string rohs, string shipToStock, int appDateFrom, int appDateTo, int inclusive, int exclusive)
        {
            string sql = string.Empty;
            sql += "SELECT TBLIQCDETAIL.*";
            sql += " ,tblasn.vendorcode,tblvendor.vendorname,tblmaterial.mdesc,tblasniqc.rohs,tblasniqc.sts,tblasniqc.inspector,tblasniqc.attribute AS iqcheadattribute,tblasniqc.inspdate,tblasniqc.invuser ";
            sql += " , usera.username AS inspectorusername, userb.username AS invusername ";
            sql += "FROM tbliqcdetail ";
            sql += "LEFT OUTER JOIN tblasniqc ON tbliqcdetail.iqcno = tblasniqc.iqcno ";
            sql += "LEFT OUTER JOIN tblasn ON tblasniqc.stno = tblasn.stno ";
            sql += "LEFT OUTER JOIN tblvendor ON tblasn.vendorcode = tblvendor.vendorcode ";
            sql += "LEFT OUTER JOIN tblmaterial ON tbliqcdetail.itemcode = tblmaterial.mcode AND tbliqcdetail.orgid = tblmaterial.orgid ";
            sql += "LEFT OUTER JOIN tbluser usera ON tblasniqc.inspector = usera.usercode ";
            sql += "LEFT OUTER JOIN tbluser userb ON tblasniqc.invuser = userb.usercode ";
            sql += GetQueryIQCDetailForReceiveWhereSql(iqcNo, stNo, vendorCode, rohs, shipToStock, appDateFrom, appDateTo);
            sql += "ORDER BY tbliqcdetail.iqcno, tbliqcdetail.stline ";

            return this.DataProvider.CustomQuery(typeof(IQCDetailForReceive), new PagerCondition(sql, inclusive, exclusive));
        }

        public int QueryIQCDetailForReceiveCount(string iqcNo, string stNo, string vendorCode, string rohs, string shipToStock, int appDateFrom, int appDateTo)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) ";
            sql += "FROM tbliqcdetail ";
            sql += "LEFT OUTER JOIN tblasniqc ON tbliqcdetail.iqcno = tblasniqc.iqcno ";
            sql += "LEFT OUTER JOIN tblasn ON tblasniqc.stno = tblasn.stno ";
            sql += "LEFT OUTER JOIN tblvendor ON tblasn.vendorcode = tblvendor.vendorcode ";
            sql += "LEFT OUTER JOIN tblmaterial ON tbliqcdetail.itemcode = tblmaterial.mcode AND tbliqcdetail.orgid = tblmaterial.orgid ";
            sql += GetQueryIQCDetailForReceiveWhereSql(iqcNo, stNo, vendorCode, rohs, shipToStock, appDateFrom, appDateTo);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private string GetQueryIQCDetailForReceiveWhereSql(string iqcNo, string stNo, string vendorCode, string rohs, string shipToStock, int appDateFrom, int appDateTo)
        {
            string sql = "WHERE 1 = 1 ";
            sql += "AND (tbliqcdetail.checkstatus = '" + IQCCheckStatus.IQCCheckStatus_Qualified + "' ";
            sql += "    OR (tbliqcdetail.checkstatus = '" + IQCCheckStatus.IQCCheckStatus_UnQualified + "' ";
            sql += "        AND tbliqcdetail.concessionstatus = 'Y')) ";
            sql += "AND (tbliqcdetail.iqcno, tbliqcdetail.stline) NOT IN (SELECT iqcno, stline FROM tblmaterialreceive) ";

            if (iqcNo.Trim().Length > 0)
            {
                sql += "AND tbliqcdetail.iqcno LIKE '%" + iqcNo.Trim().ToUpper() + "%' ";
            }

            if (stNo.Trim().Length > 0)
            {
                sql += "AND tbliqcdetail.stno LIKE '%" + stNo.Trim().ToUpper() + "%' ";
            }

            if (vendorCode.Trim().Length > 0)
            {
                sql += "AND tblasn.vendorcode LIKE '%" + vendorCode.Trim().ToUpper() + "%' ";
            }

            if (rohs.Trim().Length > 0)
            {
                sql += "AND tblasniqc.rohs = '" + rohs.Trim().ToUpper() + "' ";
            }

            if (shipToStock.Trim().Length > 0)
            {
                sql += "AND tblasniqc.sts = '" + shipToStock + "' ";
            }

            if (appDateFrom > 0)
            {
                sql += "AND tblasniqc.appdate >= " + appDateFrom + " ";
            }

            if (appDateTo > 0)
            {
                sql += "AND tblasniqc.appdate <=" + appDateTo + " ";
            }

            return sql;
        }

        public bool CheckAllIQCDetailIsClose(string stno)
        {
            string sql = "SELECT Count(*) FROM TBLIQCDETAIL WHERE stdstatus <> 'Close' AND stno = '" + stno + "'";
            if (this.DataProvider.GetCount(new SQLCondition(sql)) > 0)
            {
                return false;
            }
            return true;
        }

        public bool CheckAllIQCDetailInASNIQCIsClose(string iqcNo)
        {
            string sql = "SELECT Count(*) FROM TBLIQCDETAIL WHERE stdstatus <> 'Close' AND iqcno = '" + iqcNo + "'";
            if (this.DataProvider.GetCount(new SQLCondition(sql)) > 0)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region ShipToStock

        public ShipToStock CreateNewShipToStock()
        {
            return new ShipToStock();
        }

        public void AddShipToStock(ShipToStock shipToStock)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            string sql = string.Empty;
            sql += "INSERT INTO tblshiptostock(itemcode, orgid, vendorcode, active, effdate, ivldate, muser, mdate, mtime,DQMCODE) ";
            sql += "VALUES(";
            sql += "'" + shipToStock.MaterialCode.Trim().ToUpper() + "', ";
            sql += shipToStock.OrganizationID.ToString() + ", ";
            sql += "'" + shipToStock.VendorCode.Trim().ToUpper() + "', ";
            sql += "'" + shipToStock.Active.Trim().ToUpper() + "', ";
            sql += shipToStock.EffectDate.ToString() + ", ";
            sql += shipToStock.InvalidDate.ToString() + ", ";
            sql += "'" + shipToStock.MaintainUser.Trim().ToUpper() + "', ";
            sql += dbDateTime.DBDate.ToString() + ", ";
            sql += dbDateTime.DBTime.ToString() + " ,'";
            sql += shipToStock.DQMCode + "') ";

            try
            {
                this.DataProvider.BeginTransaction();

                this.DataProvider.CustomExecute(new SQLCondition(sql));

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        private void DeleteShipToStock(ShipToStock shipToStock)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            string sql = string.Empty;
            sql += "UPDATE tblshiptostock ";
            sql += "SET active = 'N' ";
            sql += ", ivldate = " + dbDateTime.DBDate.ToString() + " ";
            sql += "WHERE dqmcode = '" + shipToStock.DQMCode.Trim().ToUpper() + "' ";
            sql += "AND orgid = " + shipToStock.OrganizationID.ToString() + " ";
            sql += "AND vendorcode = '" + shipToStock.VendorCode.Trim().ToUpper() + "' ";
            sql += "AND active = 'Y' ";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void DeleteShipToStock(ShipToStock[] shipToStock)
        {
            try
            {
                this.DataProvider.BeginTransaction();

                for (int i = 0; i < shipToStock.Length; i++)
                {
                    DeleteShipToStock(shipToStock[i]);
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        public void UpdateShipToStock(ShipToStock shipToStock)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            string sql = string.Empty;
            sql += "UPDATE tblshiptostock ";
            sql += "SET active = '" + shipToStock.Active.Trim().ToUpper() + "' ";
            sql += ", effdate = " + shipToStock.EffectDate.ToString() + " ";
            sql += ", ivldate = " + shipToStock.InvalidDate.ToString() + " ";
            sql += ", muser = '" + shipToStock.MaintainUser.Trim().ToUpper() + "' ";
            sql += ", mdate = " + dbDateTime.DBDate.ToString() + " ";
            sql += ", mtime = " + dbDateTime.DBTime.ToString() + " ";
            sql += "WHERE itemcode = '" + shipToStock.MaterialCode.Trim().ToUpper() + "' ";
            sql += "AND orgid = " + shipToStock.OrganizationID.ToString() + " ";
            sql += "AND vendorcode = '" + shipToStock.VendorCode.Trim().ToUpper() + "' ";
            sql += "AND active = 'Y' ";

            try
            {
                this.DataProvider.BeginTransaction();

                this.DataProvider.CustomExecute(new SQLCondition(sql));

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        public object GetShipToStock(string itemCode, int organizationID, string vendorCode)
        {
            object[] shipToStockList = null;

            string sql = string.Empty;
            sql += "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ShipToStock)) + " FROM tblshiptostock ";
            sql += "WHERE 1 = 1 ";
            sql += "AND dqmcode = '" + itemCode.Trim().ToUpper() + "' ";
            sql += "AND orgid = " + organizationID.ToString() + " ";
            sql += "AND vendorcode = '" + vendorCode.Trim().ToUpper() + "' ";
            sql += "AND active = 'Y' ";

            shipToStockList = this.DataProvider.CustomQuery(typeof(ShipToStock), new SQLCondition(sql));

            if (shipToStockList == null)
            {
                return null;
            }
            else
            {
                return shipToStockList[0];
            }
        }

        public object[] QueryShipToStock(string itemCode, int organizationID, string vendorCode, string active)
        {
            string sql = string.Empty;
            sql += "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ShipToStock)) + " FROM tblshiptostock ";
            sql += GetQueryShipToStockWhereSql(itemCode, organizationID, vendorCode, active);

            return this.DataProvider.CustomQuery(typeof(ShipToStock), new SQLCondition(sql));
        }

        public object[] QueryShipToStockEx(string itemCode, int organizationID, string vendorCode, string active, int inclusive, int exclusive)
        {
            //string sql = string.Empty;
            //sql += "SELECT " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(ShipToStock)) + ", tblmaterial.mdesc, tblorg.orgdesc, tblvendor.vendorname ";
            //sql += "FROM tblshiptostock ";
            //sql += "LEFT OUTER JOIN tblmaterial ON tblshiptostock.itemcode = tblmaterial.mcode AND tblshiptostock.orgid = tblmaterial.orgid ";
            //sql += "LEFT OUTER JOIN tblorg ON tblshiptostock.orgid = tblorg.orgid ";
            //sql += "LEFT OUTER JOIN tblvendor ON tblshiptostock.vendorcode = tblvendor.vendorcode ";
            //sql += GetQueryShipToStockWhereSql(itemCode, organizationID, vendorCode, active);

            string sql = string.Empty;
            sql += "SELECT " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(ShipToStock)) + ", tblmaterial.mchlongdesc, tblorg.orgdesc, tblvendor.vendorname ";
            sql += "FROM tblshiptostock ";
            sql += "LEFT OUTER JOIN tblmaterial ON tblshiptostock.itemcode = tblmaterial.mcode ";
            sql += "LEFT OUTER JOIN tblorg ON tblshiptostock.orgid = tblorg.orgid ";
            sql += "LEFT OUTER JOIN tblvendor ON tblshiptostock.vendorcode = tblvendor.vendorcode ";
            sql += GetQueryShipToStockWhereSql(itemCode, organizationID, vendorCode, active);
            sql += " order by tblshiptostock.mdate desc,tblshiptostock.mtime desc ";

            return this.DataProvider.CustomQuery(typeof(ShipToStockEx), new PagerCondition(sql, "itemcode, tblshiptostock.orgid, tblshiptostock.vendorcode", inclusive, exclusive));
        }

        public int QueryShipToStockCount(string itemCode, int organizationID, string vendorCode, string active)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) FROM tblshiptostock ";
            sql += GetQueryShipToStockWhereSql(itemCode, organizationID, vendorCode, active);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private string GetQueryShipToStockWhereSql(string itemCode, int organizationID, string vendorCode, string active)
        {
            string sql = "WHERE 1 = 1 ";

            if (itemCode.Trim().Length > 0)
            {
                sql += "AND tblshiptostock.dqmcode LIKE '%" + itemCode.Trim().ToUpper() + "%' ";
            }

            if (organizationID >= 0)
            {
                sql += "AND tblshiptostock.orgid = " + organizationID.ToString() + " ";
            }

            if (vendorCode.Trim().Length > 0)
            {
                if (vendorCode.IndexOf(",") < 0)
                {
                    sql += "AND tblshiptostock.vendorcode LIKE '%" + vendorCode.Trim().ToUpper() + "%' ";
                }
                else
                {
                    sql += "AND tblshiptostock.vendorcode IN (" + FormatHelper.ProcessQueryValues(vendorCode.Trim().ToUpper()) + ") ";
                }
            }

            if (active.Trim().Length > 0)
            {
                sql += "AND tblshiptostock.active = '" + active.Trim().ToUpper() + "' ";
            }

            return sql;
        }

        #endregion

        #region MaterialReceive

        public MaterialReceive CreateNewMaterialReceive()
        {
            return new MaterialReceive();
        }

        public void AddMaterialReceive(MaterialReceive materialReceive)
        {
            this._helper.AddDomainObject(materialReceive);
        }

        public void DeleteMaterialReceive(MaterialReceive materialReceive)
        {
            this._helper.DeleteDomainObject(materialReceive);
        }

        public void UpdateMaterialReceive(MaterialReceive materialReceive)
        {
            this._helper.UpdateDomainObject(materialReceive);
        }

        public object GetMaterialReceive(string iqcNo, int stLine)
        {
            return this.DataProvider.CustomSearch(typeof(MaterialReceive), new object[] { iqcNo, stLine });
        }

        public void AddOrUpdateMaterialReceive(MaterialReceive materialReceive)
        {
            if (GetMaterialReceive(materialReceive.IQCNo, materialReceive.STLine) == null)
            {
                AddMaterialReceive(materialReceive);
            }
            else
            {
                UpdateMaterialReceive(materialReceive);
            }
        }

        public void UpdateMaterialReceiveFlag(string orderNo, string transactionCode, string oldFlag, string newFlag)
        {
            string sql = "UPDATE tblmaterialreceive SET flag = '" + newFlag + "' WHERE orderno = '" + orderNo + "' AND transactioncode = '" + transactionCode + "'  ";

            if (oldFlag.Trim().Length > 0)
            {
                sql += "AND flag = '" + oldFlag + "' ";
            }

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public object[] QueryMaterialReceive(string iqcNo, string orderNo, int orgId, string flag, string srmFlag,
            int beginDate, int endDate, string storageId, string itemCodeList,
            string vendorCode, string keepMan, int inclusive, int exclusive)
        {
            string strSql = this.GenerateSQL4QueryMaterialReceive(iqcNo, orderNo, orgId, flag, srmFlag,
                beginDate, endDate, storageId, itemCodeList, vendorCode, keepMan);

            return this.DataProvider.CustomQuery(typeof(MaterialReceiveExtend), new PagerCondition(strSql, "tblmaterialreceive.iqcno,tblmaterialreceive.stline", inclusive, exclusive));
        }

        public int QueryMaterialReceiveCount(string iqcNo, string orderNo, int orgId, string flag, string srmFlag,
            int beginDate, int endDate, string storageId, string itemCodeList,
            string vendorCode, string keepMan)
        {
            string strSql = this.GenerateSQL4QueryMaterialReceive(iqcNo, orderNo, orgId, flag, srmFlag,
                beginDate, endDate, storageId, itemCodeList, vendorCode, keepMan);
            strSql = "SELECT COUNT(*) FROM (" + strSql + ")";

            return this.DataProvider.GetCount(new SQLCondition(strSql));
        }

        private string GenerateSQL4QueryMaterialReceive(string iqcNo, string orderNo, int orgId, string flag, string srmFlag,
            int beginDate, int endDate, string storageId, string itemCodeList,
            string vendorCode, string keepMan)
        {
            string strSql = "";
            strSql += "SELECT {0}, ";
            strSql += "       tblmaterial.mdesc, tblrawreceive2sap.errormessage, tbliqcdetail.srmflag, tbliqcdetail.srmerrormsg ";
            strSql += "  FROM tblmaterialreceive, tblmaterial, tblrawreceive2sap, tbliqcdetail";
            strSql += " WHERE tblmaterialreceive.itemcode = tblmaterial.mcode(+)";
            strSql += "   AND tblmaterialreceive.transactioncode = tblrawreceive2sap.transactioncode(+)";
            strSql += "   AND tblmaterialreceive.orderno = tblrawreceive2sap.pono(+)";
            strSql += "   AND tblmaterialreceive.iqcno = tbliqcdetail.iqcno(+)";
            strSql += "   AND tblmaterialreceive.stline = tbliqcdetail.stline(+)";
            strSql = string.Format(strSql, DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(MaterialReceive)));

            if (!string.IsNullOrEmpty(iqcNo))
            {
                strSql += "   AND tblmaterialreceive.iqcno LIKE '%" + iqcNo + "%'";
            }
            if (!string.IsNullOrEmpty(orderNo))
            {
                strSql += "   AND tblmaterialreceive.orderno LIKE '%" + orderNo + "%'";
            }
            if (orgId != 0)
            {
                strSql += "   AND tblmaterialreceive.orgid = " + orgId;
            }
            if (!string.IsNullOrEmpty(flag))
            {
                strSql += "   AND tblmaterialreceive.flag = '" + flag + "'";
            }
            if (!string.IsNullOrEmpty(srmFlag))
            {
                strSql += "   AND tbliqcdetail.srmflag = '" + srmFlag + "'";
            }
            strSql += FormatHelper.GetDateRangeSql("tblmaterialreceive.voucherdate", beginDate, endDate);

            if (!string.IsNullOrEmpty(storageId))
            {
                strSql += "   AND tblmaterialreceive.storageid LIKE '%" + storageId + "%'";
            }
            if (!string.IsNullOrEmpty(itemCodeList))
            {
                string[] list = itemCodeList.Split(',');
                string valueList = "";
                foreach (string v in list)
                {
                    if (!string.IsNullOrEmpty(v))
                    {
                        valueList += v.Trim() + ",";
                    }
                }

                if (!string.IsNullOrEmpty(valueList))
                {
                    valueList = valueList.Substring(0, valueList.Length - 1);
                    valueList = valueList.Replace(",", "','");
                    strSql += "   AND tblmaterialreceive.itemcode IN ('" + valueList + "')";
                }
            }
            if (!string.IsNullOrEmpty(keepMan))
            {
                strSql += "   AND tblmaterialreceive.iqcno IN (SELECT DISTINCT tblasniqc.iqcno";
                strSql += "                              FROM tblasniqc";
                strSql += "                             WHERE tblasniqc.invuser LIKE '%" + keepMan + "%')";
            }
            if (!string.IsNullOrEmpty(vendorCode))
            {
                strSql += "   AND tblmaterialreceive.stno IN (SELECT DISTINCT tblasn.stno";
                strSql += "                             FROM tblasn";
                strSql += "                            WHERE vendorcode LIKE '%" + vendorCode + "%')";
            }

            return strSql;
        }

        //public bool IQCReceiveMaterial(MaterialReceive materialReceive)
        //{
        //    bool returnValue = false;

        //    try
        //    {
        //        this.DataProvider.BeginTransaction();

        //        //插入tblmaterialreceive
        //        AddOrUpdateMaterialReceive(materialReceive);

        //        //更新IQCDetaild的两个字段
        //        IQCDetail iqcDetail = (IQCDetail)GetIQCDetail(materialReceive.IQCNo, materialReceive.STLine);
        //        if (iqcDetail != null && iqcDetail.CheckStatus == IQCCheckStatus.IQCCheckStatus_Qualified)
        //        {
        //            //iqcDetail.SRMFlag = FlagStatus.FlagStatus_MES;
        //            iqcDetail.SRMErrorMessage = string.Empty;
        //            iqcDetail.MaintainUser = materialReceive.MaintainUser;
        //            UpdateIQCDetail(iqcDetail);
        //        }

        //        //汇总tblmaterialstorageinfo
        //        MaterialStorageInfo materialStorageInfo = (MaterialStorageInfo)GetMaterialStorageInfo(materialReceive.ItemCode, materialReceive.OrganizationID, materialReceive.StorageID);
        //        if (materialStorageInfo == null)
        //        {
        //            materialStorageInfo = CreateNewMaterialStorageInfo();

        //            materialStorageInfo.ItemCode = materialReceive.ItemCode;
        //            materialStorageInfo.OrganizationID = materialReceive.OrganizationID;
        //            materialStorageInfo.StorageID = materialReceive.StorageID;
        //            materialStorageInfo.CLABSQty = materialReceive.RealReceiveQty;
        //            materialStorageInfo.MaintainUser = materialReceive.MaintainUser;

        //            AddMaterialStorageInfo(materialStorageInfo);
        //        }
        //        else
        //        {
        //            materialStorageInfo.CLABSQty += materialReceive.RealReceiveQty;

        //            UpdateMaterialStorageInfo(materialStorageInfo);
        //        }

        //        //产生物料批产生物料批
        //        ASN asn = (ASN)GetASN(materialReceive.STNo);
        //        if (asn == null)
        //        {
        //            this.DataProvider.RollbackTransaction();
        //            returnValue = false;
        //            return returnValue;
        //        }
        //        else
        //        {
        //            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
        //            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

        //            //materialLot.StorageID
        //            //materialReceive.StorageID=tblstorage.SAPSTORAGE
        //            string storageId = " ";
        //            object[] storageList = inventoryFacade.QueryStorageByOrgId(materialReceive.OrganizationID, materialReceive.StorageID);
        //            if (storageList != null)
        //            {
        //                storageId = ((Storage)storageList[0]).StorageCode;
        //            }
        //            else
        //            {
        //                this.DataProvider.RollbackTransaction();
        //                return false;
        //            }

        //            MaterialLot materialLot = inventoryFacade.CreateNewMaterialLot();
        //            materialLot.OrganizationID = materialReceive.OrganizationID;
        //            materialLot.StorageID = storageId;
        //            materialLot.ItemCode = materialReceive.ItemCode;
        //            materialLot.LotInQty = materialReceive.RealReceiveQty;
        //            materialLot.LotQty = materialReceive.RealReceiveQty;
        //            materialLot.Unit = materialReceive.Unit;
        //            materialLot.FIFOFlag = FIFOFlag.FIFOFlag_Y;
        //            materialLot.IQCNo = materialReceive.IQCNo;
        //            materialLot.STLine = materialReceive.STLine;
        //            materialLot.MaintainUser = materialReceive.MaintainUser;
        //            materialLot.CreateDate = dbDateTime.DBDate;
        //            materialLot.VendorCode = asn.VendorCode;

        //            string dateCode = GetMaterialLotDateCode(materialLot.CreateDate);

        //            string runningNumber = inventoryFacade.GetNewMaterialLotRunningNumber(materialLot.VendorCode, materialLot.ItemCode, materialLot.CreateDate);
        //            materialLot.MaterialLotNo = materialLot.VendorCode + "-" + materialLot.ItemCode + "-" + dateCode + "-" + runningNumber;
        //            inventoryFacade.AddMaterialLot(materialLot);

        //            //入库 MaterialTrans
        //            MaterialTrans materialTrans = inventoryFacade.CreateNewMaterialTrans();
        //            materialTrans.Serial = 0;
        //            materialTrans.FRMaterialLot = " ";
        //            materialTrans.FRMITEMCODE = " ";
        //            materialTrans.FRMStorageID = " ";
        //            materialTrans.TOMaterialLot = materialLot.MaterialLotNo;
        //            materialTrans.TOITEMCODE = materialLot.ItemCode;
        //            materialTrans.TOStorageID = materialLot.StorageID;
        //            materialTrans.TransQTY = materialLot.LotInQty;
        //            materialTrans.Memo = materialReceive.ReceiveMemo;
        //            materialTrans.UNIT = materialLot.Unit;
        //            materialTrans.VendorCode = materialLot.VendorCode;
        //            materialTrans.IssueType = IssueType.IssueType_Receive;
        //            materialTrans.TRANSACTIONCODE = materialLot.IQCNo;
        //            materialTrans.BusinessCode = "101";
        //            materialTrans.OrganizationID = materialLot.OrganizationID;
        //            materialTrans.MaintainUser = materialLot.MaintainUser;
        //            materialTrans.MaintainDate = dbDateTime.DBDate;
        //            materialTrans.MaintainTime = dbDateTime.DBTime;
        //            inventoryFacade.AddMaterialTrans(materialTrans);
        //        }

        //        this.DataProvider.CommitTransaction();
        //        returnValue = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.DataProvider.RollbackTransaction();
        //        throw ex;
        //    }

        //    return returnValue;
        //}

        public object[] QueryMaterialReceiveNotConfirmed(int maxCount)
        {
            string sql = "SELECT {0} FROM tblmaterialreceive WHERE flag = '{1}' ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(MaterialReceive)), FlagStatus.FlagStatus_MES);
            return this.DataProvider.CustomQuery(typeof(MaterialReceive), new PagerCondition(sql, 1, maxCount));
        }

        public object[] QueryMaterialReceive(string orderNo, string transactionCode)
        {
            string sql = "SELECT {0} FROM tblmaterialreceive WHERE orderno = '{1}' AND transactioncode = '{2}' ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(MaterialReceive)), orderNo, transactionCode);
            return this.DataProvider.CustomQuery(typeof(MaterialReceive), new SQLCondition(sql));
        }

        private string GetMaterialLotDateCode(int date)
        {
            string returnValue = string.Empty;
            string dateString = date.ToString("00000000");

            returnValue += dateString.Substring(2, 2);
            returnValue += NumberScaleHelper.ChangeNumber(dateString.Substring(4, 2), NumberScale.Scale10, NumberScale.Scale16);
            returnValue += NumberScaleHelper.ChangeNumber(dateString.Substring(6, 2), NumberScale.Scale10, NumberScale.Scale36);

            return returnValue;
        }

        #endregion

        #region MaterialStorageInfo

        public MaterialStorageInfo CreateNewMaterialStorageInfo()
        {
            return new MaterialStorageInfo();
        }

        public void AddMaterialStorageInfo(MaterialStorageInfo materialStorageInfo)
        {
            this._helper.AddDomainObject(materialStorageInfo);
        }

        public void DeleteMaterialStorageInfo(MaterialStorageInfo materialStorageInfo)
        {
            this._helper.DeleteDomainObject(materialStorageInfo);
        }

        public void UpdateMaterialStorageInfo(MaterialStorageInfo materialStorageInfo)
        {
            this._helper.UpdateDomainObject(materialStorageInfo);
        }

        public object GetMaterialStorageInfo(string itemCode, int organizationID, string storageID)
        {
            return this.DataProvider.CustomSearch(typeof(MaterialStorageInfo), new object[] { itemCode, organizationID, storageID });
        }

        #endregion

        #region SRM Interface

        //private SRMWebService.PurchOrderService _GetSRMPurchOrderService = null;
        //private SRMWebService.PurchOrderService GetSRMPurchOrderService()
        //{
        //    if (_GetSRMPurchOrderService == null)
        //    {
        //        _GetSRMPurchOrderService = new SRMWebService.PurchOrderService();

        //        if (System.Configuration.ConfigurationManager.AppSettings["SRMWebServiceUrl"] != null)
        //        {
        //            _GetSRMPurchOrderService.Url = System.Configuration.ConfigurationManager.AppSettings["SRMWebServiceUrl"].ToString().Trim();
        //        }

        //        return _GetSRMPurchOrderService;
        //    }
        //    else
        //    {
        //        return _GetSRMPurchOrderService;
        //    }
        //}

        private string _SRMDBLink = string.Empty;
        private string GetSRMDBLink()
        {
            if (_SRMDBLink.Trim().Length <= 0 || _SRMDBLink.IndexOf("@") < 0)
            {
                Parameter srmDBLink = (Parameter)(new SystemSettingFacade(this.DataProvider)).GetParameter("SRMDBLINK", "SRMINTERFACE");

                if (srmDBLink == null)
                {
                    _SRMDBLink = "@SRM";
                }
                else
                {
                    _SRMDBLink = srmDBLink.ParameterAlias.Trim().ToUpper();
                    if (_SRMDBLink.IndexOf("@") != 0)
                    {
                        _SRMDBLink = "@" + _SRMDBLink;
                    }
                }
            }

            return _SRMDBLink;
        }

        //由于调用的下面三个SRM函数，其中已经包含了Trans，所以一定要放在我们的Trans的末尾去调用！！！！！！

        public void SRMWaitCheckASN(string stNo)
        {
            //SRMWebService.PurchOrderService service = GetSRMPurchOrderService();
            //service.WaitCheckASN(stNo);
        }

        public void SRMCancelASNDetail(string stNo, int stLine)
        {
            //SRMWebService.PurchOrderService service = GetSRMPurchOrderService();
            //service.CancelASNDetail(stNo, stLine.ToString());
        }

        public void SRMCheckASNDetail(object[] iqcDetailList)
        {
            //SRMWebService.PurchOrderService service = GetSRMPurchOrderService();

            //string stNo = string.Empty;
            //List<SRMWebService.ASNDetail> srmASNDetailList = new List<SRMWebService.ASNDetail>();

            //for (int i = 0; i < iqcDetailList.Length; i++)
            //{
            //    IQCDetail iqcDetail = (IQCDetail)iqcDetailList[i];

            //    //由于service.CheckASNDetail只能处理相同STNo的ASNDetail数据
            //    if (stNo != iqcDetail.STNo && srmASNDetailList.Count > 0)
            //    {
            //        service.CheckASNDetail(stNo, srmASNDetailList.ToArray());
            //        srmASNDetailList.Clear();
            //    }

            //    //更新stNo为当前iqcDetail的
            //    stNo = iqcDetail.STNo;

            //    //通过主健获取SRM中的ASNDetail
            //    SRMWebService.ExpressionParamValue stNoParam = new SRMWebService.ExpressionParamValue();
            //    stNoParam.Name = "STNo";
            //    stNoParam.Value = iqcDetail.STNo;

            //    SRMWebService.ExpressionParamValue stLineParam = new SRMWebService.ExpressionParamValue();
            //    stLineParam.Name = "STLine";
            //    stLineParam.Value = iqcDetail.STLine;

            //    SRMWebService.ASNDetail[] asnDetailList = service.FindASNDetails(null, new SRMWebService.ExpressionParamValue[] { stNoParam, stLineParam });
            //    if (asnDetailList != null && asnDetailList.Length > 0)
            //    {
            //        asnDetailList[0].CheckStatus = iqcDetail.CheckStatus;
            //        asnDetailList[0].CheckQty = iqcDetail.ReceiveQty;
            //        srmASNDetailList.Add(asnDetailList[0]);
            //    }
            //}

            ////针对最后一笔IQCDetail数据
            //if (srmASNDetailList.Count > 0)
            //{
            //    service.CheckASNDetail(stNo, srmASNDetailList.ToArray());
            //    srmASNDetailList.Clear();
            //}
        }

        public void SRMForceCloseASN(string stNo)
        {
            //SRMWebService.PurchOrderService service = GetSRMPurchOrderService();
            //service.CheckASN(stNo);
        }

        #endregion

        #region 状态相关函数

        //public void SendCheckIQCHead(IQCHead[] iqcHeadList, string userCode)
        //{
        //    if (iqcHeadList == null)
        //    {
        //        return;
        //    }

        //    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

        //    foreach (IQCHead iqcHead in iqcHeadList)
        //    {
        //        try
        //        {
        //            //记录下是否来自ASN
        //            bool isFromASN = IsFromASN(iqcHead.STNo);

        //            //即将更新到的状态
        //            string iqcStatus = IQCStatus.IQCStatus_WaitCheck;
        //            string chekcStatus = IQCCheckStatus.IQCCheckStatus_WaitCheck;
        //            if (iqcHead.STS == "Y")  //免检
        //            {
        //                chekcStatus = IQCCheckStatus.IQCCheckStatus_Qualified;
        //            }

        //            this.DataProvider.BeginTransaction();

        //            //ASN的状态变为WaitCheck
        //            ASN asn = (ASN)GetASN(iqcHead.STNo);
        //            asn.STStatus = iqcStatus;
        //            asn.MaintainDate = dbDateTime.DBDate;
        //            asn.MaintainTime = dbDateTime.DBTime;
        //            asn.MaintainUser = userCode;
        //            UpdateASN(asn);

        //            //IQC的状态变为WaitCheck
        //            iqcHead.Status = iqcStatus;
        //            iqcHead.Applicant = userCode;
        //            iqcHead.AppDate = dbDateTime.DBDate;
        //            iqcHead.AppTime = dbDateTime.DBTime;
        //            iqcHead.MaintainUser = userCode;
        //            iqcHead.MaintainDate = dbDateTime.DBDate;
        //            iqcHead.MaintainTime = dbDateTime.DBTime;
        //            UpdateIQCHead(iqcHead);





        //            //IQC的行项目变为WaitCheck，检验结果变为WaitCheck
        //            object[] iqcDetailList = QueryIQCDetail(iqcHead.IQCNo, string.Empty);
        //            if (iqcDetailList != null)
        //            {


        //                foreach (IQCDetail iqcDetail in iqcDetailList)
        //                {
        //                    if (iqcDetail.STDStatus == IQCStatus.IQCStatus_New)
        //                    {
        //                        iqcDetail.STDStatus = iqcStatus;
        //                        iqcDetail.CheckStatus = chekcStatus;
        //                        iqcDetail.MaintainUser = userCode;
        //                        iqcDetail.MaintainDate = dbDateTime.DBDate;
        //                        iqcDetail.MaintainTime = dbDateTime.DBTime;
        //                        UpdateIQCDetail(iqcDetail);
        //                    }

        //                    object invReceiptDetail = GetInvReceiptDetailForUpdate(iqcHead.STNo, iqcDetail.STLine);

        //                    //modified by vivian.sun 2011-7-6 IQC检验状态为WaitCheck/Qualified(免检)
        //                    //invReceiptDetail.Iqcstatus = iqcStatus;
        //                    if (invReceiptDetail != null)
        //                    {

        //                        (invReceiptDetail as InvReceiptDetail).Iqcstatus = chekcStatus;

        //                        if (chekcStatus == IQCCheckStatus.IQCCheckStatus_Qualified)
        //                        {
        //                            (invReceiptDetail as InvReceiptDetail).Qualifyqty = (invReceiptDetail as InvReceiptDetail).Planqty;
        //                        }
        //                        //end modify                           

        //                        (invReceiptDetail as InvReceiptDetail).Recstatus = iqcStatus;
        //                        (invReceiptDetail as InvReceiptDetail).Muser = userCode;
        //                        (invReceiptDetail as InvReceiptDetail).Mdate = dbDateTime.DBDate;
        //                        (invReceiptDetail as InvReceiptDetail).Mtime = dbDateTime.DBTime;
        //                        UpdateInvReceiptDetail((invReceiptDetail as InvReceiptDetail));
        //                    }

        //                }
        //            }

        //            //InvReceipt的状态变为WaitCheck
        //            InvReceipt invReceipt = (InvReceipt)GetINVRecepitForUpdate(iqcHead.STNo);
        //            if (invReceipt != null)
        //            {
        //                invReceipt.Recstatus = iqcStatus;
        //                invReceipt.Muser = userCode;
        //                invReceipt.Mdate = dbDateTime.DBDate;
        //                invReceipt.Mtime = dbDateTime.DBTime;
        //                UpdateInvReceipt(invReceipt);
        //            }

        //            //InvReceiptDetail的状态变为WaitCheck



        //            TryToUpdateStatusCausedByIQCDetail(new string[] { iqcHead.IQCNo }, userCode);

        //            this.DataProvider.CommitTransaction();
        //        }
        //        catch (Exception ex)
        //        {
        //            this.DataProvider.RollbackTransaction();
        //            throw ex;
        //        }
        //    }
        //}

        //public void CancelIQCDetail(IQCDetail[] iqcDetailList, string userCode)
        //{
        //    if (iqcDetailList == null)
        //    {
        //        return;
        //    }

        //    foreach (IQCDetail iqcDetail in iqcDetailList)
        //    {
        //        try
        //        {
        //            //记录下是否来自ASN
        //            bool isFromASN = IsFromASN(iqcDetail.STNo);

        //            //即将更新到的状态
        //            string iqcStatus = IQCStatus.IQCStatus_Cancel;

        //            this.DataProvider.BeginTransaction();

        //            iqcDetail.STDStatus = iqcStatus;
        //            iqcDetail.CheckStatus = string.Empty;

        //            //SRMFlag == SRM的意义已经改变为：表示当前IQCDetail彻底处理完成
        //            //iqcDetail.SRMFlag = FlagStatus.FlagStatus_SRM;
        //            iqcDetail.SRMErrorMessage = string.Empty;
        //            iqcDetail.MaintainUser = userCode;
        //            UpdateIQCDetail(iqcDetail);

        //            TryToUpdateStatusCausedByIQCDetail(new string[] { iqcDetail.IQCNo }, userCode);

        //            if (isFromASN)
        //            {
        //                SRMCancelASNDetail(iqcDetail.STNo, iqcDetail.STLine);

        //                //针对SRM中Cancel ASNDetail的一个问题
        //                //当ASN的最后处理的ASNDetail通过Cancel方式结束时，并不会在CheckASNDetail中关闭ASN，
        //                //所以，需要我们强制关闭
        //                ASN asn = (ASN)GetASN(iqcDetail.STNo);
        //                if (asn != null
        //                    && asn.Flag == IQCTicketType.IQCTicketType_ASN
        //                    && asn.STStatus == IQCStatus.IQCStatus_Close)
        //                {
        //                    try
        //                    {
        //                        SRMForceCloseASN(asn.STNo);
        //                    }
        //                    catch { }
        //                }
        //            }

        //            this.DataProvider.CommitTransaction();
        //        }
        //        catch (Exception ex)
        //        {
        //            this.DataProvider.RollbackTransaction();
        //            throw ex;
        //        }
        //    }
        //}

        //private void AlertIQCDetailUnQualified(IQCDetail iqcDetail)
        //{
        //    if (iqcDetail != null && iqcDetail.CheckStatus == IQCCheckStatus.IQCCheckStatus_UnQualified)
        //    {
        //        string vendor = string.Empty;
        //        object[] iqcHeadWithVendorArray = QueryIQCHeadWithVendor(iqcDetail.STNo);
        //        if (iqcHeadWithVendorArray != null && iqcHeadWithVendorArray.Length > 0)
        //        {
        //            IQCHeadWithVendor iqcHeadWithVendor = (IQCHeadWithVendor)iqcHeadWithVendorArray[0];
        //            vendor = iqcHeadWithVendor.VendorCode + " - " + iqcHeadWithVendor.VendorName;
        //        }

        //        AlertFacade alertFacade = new AlertFacade(this.DataProvider);
        //        alertFacade.AlertIQCNG(vendor, iqcDetail.OrganizationID, iqcDetail.ItemCode, iqcDetail.IQCNo, iqcDetail.STLine, iqcDetail.MemoEx);
        //    }
        //}

        //public void CheckIQCDetail(IQCDetail[] iqcDetailList, string userCode, string checkStatus)
        //{
        //    if (iqcDetailList == null)
        //    {
        //        return;
        //    }

        //    List<IQCDetail> iqcDetailToSRMList = new List<IQCDetail>();

        //    foreach (IQCDetail iqcDetail in iqcDetailList)
        //    {
        //        try
        //        {
        //            //记录下是否来自ASN
        //            bool isFromASN = IsFromASN(iqcDetail.STNo);

        //            this.DataProvider.BeginTransaction();

        //            iqcDetail.CheckStatus = checkStatus;
        //            if (checkStatus == IQCCheckStatus.IQCCheckStatus_UnQualified)
        //            {
        //                //SRMFlag == SRM的意义已经改变为：表示当前IQCDetail彻底处理完成
        //                //iqcDetail.SRMFlag = FlagStatus.FlagStatus_SRM;
        //                iqcDetail.SRMErrorMessage = string.Empty;
        //            }

        //            //Added By Nettie Chen on 2009/09/22
        //            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
        //            iqcDetail.DInspector = userCode;
        //            iqcDetail.DInspectDate = dbDateTime.DBDate;
        //            iqcDetail.DInspectTime = dbDateTime.DBTime;
        //            //End Added

        //            iqcDetail.MaintainUser = userCode;
        //            UpdateIQCDetail(iqcDetail);

        //            //IQC不合格预警
        //            AlertIQCDetailUnQualified(iqcDetail);

        //            IQCHead iqcHead = (IQCHead)GetIQCHead(iqcDetail.IQCNo);

        //            //更新栏位
        //            if (iqcHead != null)
        //            {
        //                //Modified By Nettie Chen on 2009/09/22
        //                //this.TryToUpdateIQCHead(iqcHead, userCode);
        //                this.TryToUpdateIQCHead(iqcHead, userCode, dbDateTime);
        //                //End Modified
        //            }
        //            //end

        //            if (checkStatus == IQCCheckStatus.IQCCheckStatus_UnQualified ||
        //                checkStatus == IQCCheckStatus.IQCCheckStatus_Qualified)
        //            {

        //                if (iqcHead != null)
        //                {
        //                    TryToCloseIQCHead(new IQCHead[] { iqcHead }, userCode);
        //                }

        //                if (checkStatus == IQCCheckStatus.IQCCheckStatus_UnQualified)
        //                {
        //                    iqcDetailToSRMList.Add(iqcDetail);
        //                }
        //            }

        //            if (isFromASN && iqcDetailToSRMList.Count > 0)
        //            {
        //                try
        //                {
        //                    SRMCheckASNDetail(iqcDetailToSRMList.ToArray());
        //                }
        //                catch (Exception ex)
        //                {
        //                    this.DataProvider.RollbackTransaction();
        //                    this.DataProvider.BeginTransaction();

        //                    foreach (IQCDetail iqcDetailToSRM in iqcDetailToSRMList)
        //                    {
        //                        //iqcDetailToSRM.SRMFlag = FlagStatus.FlagStatus_MES;
        //                        iqcDetailToSRM.SRMErrorMessage = ex.Message;
        //                        iqcDetailToSRM.MaintainUser = userCode;
        //                        UpdateIQCDetail(iqcDetailToSRM);

        //                        //IQC不合格预警
        //                        AlertIQCDetailUnQualified(iqcDetailToSRM);
        //                    }
        //                }
        //            }

        //            this.DataProvider.CommitTransaction();
        //        }
        //        catch (Exception ex)
        //        {
        //            this.DataProvider.RollbackTransaction();
        //            throw ex;
        //        }
        //    }
        //}

        //public void ConfirmMaterialReceive(MaterialReceive[] materialReceiveList, string userCode)
        //{
        //    if (materialReceiveList == null)
        //    {
        //        return;
        //    }

        //    List<IQCDetail> iqcDetailToSRM = new List<IQCDetail>();

        //    foreach (MaterialReceive materialReceive in materialReceiveList)
        //    {
        //        IQCDetail iqcDetail = (IQCDetail)GetIQCDetail(materialReceive.IQCNo, materialReceive.STLine);

        //        try
        //        {
        //            //更新Flag为SAP
        //            materialReceive.Flag = FlagStatus.FlagStatus_SAP;
        //            materialReceive.MaintainUser = userCode;
        //            UpdateMaterialReceive(materialReceive);

        //            this.DataProvider.BeginTransaction();

        //            LockASN(materialReceive.STNo);

        //            if (iqcDetail != null && iqcDetail.CheckStatus == IQCCheckStatus.IQCCheckStatus_Qualified)
        //            {
        //                //记录下是否来自ASN
        //                bool isFromASN = IsFromASN(iqcDetail.STNo);

        //                //SRMFlag == SRM的意义已经改变为：表示当前IQCDetail彻底处理完成
        //                //iqcDetail.SRMFlag = FlagStatus.FlagStatus_SRM;
        //                iqcDetail.SRMErrorMessage = string.Empty;
        //                iqcDetail.MaintainUser = userCode;
        //                UpdateIQCDetail(iqcDetail);

        //                IQCHead iqcHead = (IQCHead)GetIQCHead(materialReceive.IQCNo);
        //                if (iqcHead != null)
        //                {
        //                    TryToCloseIQCHead(new IQCHead[] { iqcHead }, userCode);
        //                }
        //                iqcDetailToSRM.Add(iqcDetail);

        //                if (isFromASN && iqcDetailToSRM.Count > 0)
        //                {
        //                    try
        //                    {
        //                        SRMCheckASNDetail(iqcDetailToSRM.ToArray());
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        this.DataProvider.RollbackTransaction();
        //                        this.DataProvider.BeginTransaction();

        //                        //iqcDetail.SRMFlag = FlagStatus.FlagStatus_MES;
        //                        iqcDetail.SRMErrorMessage = ex.Message;
        //                        iqcDetail.MaintainUser = userCode;
        //                        UpdateIQCDetail(iqcDetail);
        //                    }
        //                }
        //            }

        //            this.DataProvider.CommitTransaction();
        //        }
        //        catch (Exception ex)
        //        {
        //            this.DataProvider.RollbackTransaction();
        //            throw ex;
        //        }
        //    }
        //}

        //public void ManualSyncMaterialReceiveFlag(string iqcNo, int stLine, string userCode)
        //{
        //    try
        //    {
        //        MaterialReceive materialReceive = (MaterialReceive)GetMaterialReceive(iqcNo, stLine);

        //        if (materialReceive != null)
        //        {
        //            ConfirmMaterialReceive(new MaterialReceive[] { materialReceive }, userCode);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void ManualSyncIQCDetailSRMFlag(string iqcNo, int stLine, string userCode)
        //{
        //    IQCDetail iqcDetail = (IQCDetail)GetIQCDetail(iqcNo, stLine);
        //    MaterialReceive materialReceive = (MaterialReceive)GetMaterialReceive(iqcNo, stLine);

        //    if (iqcDetail != null
        //        && iqcDetail.CheckStatus == IQCCheckStatus.IQCCheckStatus_Qualified
        //        && IsFromASN(iqcDetail.STNo)
        //        && materialReceive != null
        //        && materialReceive.Flag == FlagStatus.FlagStatus_SAP)
        //    {
        //        //iqcDetail.SRMFlag = FlagStatus.FlagStatus_SRM;
        //        iqcDetail.SRMErrorMessage = string.Empty;
        //        iqcDetail.MaintainUser = userCode;
        //        UpdateIQCDetail(iqcDetail);

        //        try
        //        {
        //            this.DataProvider.BeginTransaction();

        //            IQCHead iqcHead = (IQCHead)GetIQCHead(iqcDetail.IQCNo);
        //            if (iqcHead != null)
        //            {
        //                TryToCloseIQCHead(new IQCHead[] { iqcHead }, userCode);
        //            }

        //            this.DataProvider.CommitTransaction();

        //        }
        //        catch (Exception ex)
        //        {
        //            this.DataProvider.RollbackTransaction();
        //            throw ex;
        //        }
        //    }
        //}
        //Modified By Nettie Chen 2009/09/22 for add dbDateTime
        //private void TryToUpdateIQCHead(IQCHead iqcHead, string userCode)
        private void TryToUpdateIQCHead(IQCHead iqcHead, string userCode, DBDateTime dbDateTime)
        {
            if (iqcHead == null)
            {
                return;
            }
            //DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            if (iqcHead.InspectDate == 0)
            {
                iqcHead.Inspector = userCode;
                iqcHead.InspectDate = dbDateTime.DBDate;
                iqcHead.Standard = "指导书";
                iqcHead.Method = " 抽检";

                UpdateIQCHead(iqcHead);
            }

        }
        //End Modified

        //private void TryToCloseIQCHead(IQCHead[] iqcHeadList, string userCode)
        //{
        //    if (iqcHeadList == null)
        //    {
        //        return;
        //    }

        //    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

        //    foreach (IQCHead iqcHead in iqcHeadList)
        //    {
        //        if (IsIQCHeadCanClose(iqcHead.IQCNo))
        //        {
        //            iqcHead.Status = IQCStatus.IQCStatus_Close;
        //            iqcHead.MaintainUser = userCode;
        //            UpdateIQCHead(iqcHead);

        //            //Close下面除Cancel的所有的IQCDetail
        //            object[] iqcDetailList = QueryIQCDetail(iqcHead.IQCNo, string.Empty);
        //            if (iqcDetailList != null)
        //            {
        //                foreach (IQCDetail iqcDetail in iqcDetailList)
        //                {
        //                    if (iqcDetail.STDStatus != IQCStatus.IQCStatus_Cancel)
        //                    {
        //                        iqcDetail.STDStatus = IQCStatus.IQCStatus_Close;
        //                        iqcDetail.MaintainUser = userCode;
        //                        UpdateIQCDetail(iqcDetail);
        //                    }
        //                }
        //            }

        //            TryToUpdateStatusCausedByIQCDetail(new string[] { iqcHead.IQCNo }, userCode);
        //        }
        //    }
        //}

        private bool IsIQCHeadCanClose(string iqcNo)
        {
            bool returnValue = true;

            object[] iqcDetailList = QueryIQCDetail(iqcNo, string.Empty);

            if (iqcDetailList != null)
            {
                foreach (IQCDetail iqcDetail in iqcDetailList)
                {
                    bool validStatus = false;
                    if (iqcDetail.STDStatus == IQCStatus.IQCStatus_Cancel)
                    {
                        validStatus = true;
                    }
                    else if (iqcDetail.STDStatus == IQCStatus.IQCStatus_WaitCheck)
                    {
                        if (iqcDetail.CheckStatus == IQCCheckStatus.IQCCheckStatus_UnQualified)
                        {
                            validStatus = true;
                        }
                        else if (iqcDetail.CheckStatus == IQCCheckStatus.IQCCheckStatus_Qualified)
                        {
                            //MaterialReceive materialReceive = (MaterialReceive)GetMaterialReceive(iqcDetail.IQCNo, iqcDetail.STLine);
                            //if (materialReceive != null && materialReceive.Flag == FlagStatus.FlagStatus_SAP)
                            //if (iqcDetail.SRMFlag == FlagStatus.FlagStatus_SRM)
                            //{
                            validStatus = true;
                            //}
                        }
                    }

                    if (!validStatus)
                    {
                        returnValue = false;
                        break;
                    }
                }
            }

            return returnValue;
        }

        private int GetIQCDetailCount(string iqcNo, string[] excludedStatus)
        {
            string sql = "SELECT COUNT(*) FROM tbliqcdetail WHERE iqcno = '{0}' {1} ";

            string addCondition = string.Empty;
            if (excludedStatus != null && excludedStatus.Length > 0)
            {
                addCondition = "AND stdstatus NOT IN (" + FormatHelper.ProcessQueryValues(excludedStatus) + ")";
            }

            sql = string.Format(sql, iqcNo, addCondition);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private int GetIQCHeadCount(string stNo, string[] excludedStatus)
        {
            string sql = "SELECT COUNT(*) FROM tblasniqc WHERE stno = '{0}' {1} ";

            string addCondition = string.Empty;
            if (excludedStatus != null && excludedStatus.Length > 0)
            {
                addCondition = "AND status NOT IN (" + FormatHelper.ProcessQueryValues(excludedStatus) + ")";
            }

            sql = string.Format(sql, stNo, addCondition);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //private void TryToUpdateStatusCausedByIQCDetail(string[] iqcNoList, string userCode)
        //{
        //    if (iqcNoList == null)
        //    {
        //        return;
        //    }

        //    List<ASN> asnToSRMWaitCheck = new List<ASN>();
        //    List<ASN> asnToSRMClose = new List<ASN>();

        //    foreach (string iqcNo in iqcNoList)
        //    {
        //        IQCHead iqcHead = (IQCHead)GetIQCHead(iqcNo);

        //        if (iqcHead == null)
        //        {
        //            continue;
        //        }

        //        //规则一：IQCHead(New)全部IQCDetail为Cancel，则IQCHead(New)-->IQCHead(Close)
        //        if (iqcHead.Status == IQCStatus.IQCStatus_New)
        //        {
        //            if (GetIQCDetailCount(iqcHead.IQCNo, new string[] { IQCStatus.IQCStatus_Cancel }) == 0)
        //            {
        //                iqcHead.Status = IQCStatus.IQCStatus_Close;
        //                iqcHead.MaintainUser = userCode;
        //                UpdateIQCHead(iqcHead);
        //            }
        //        }

        //        //规则二：IQCHead(New)全部IQCDetail为WaitCheck和Cancel，则IQCHead(New)-->IQCHead(WaitCheck)
        //        if (iqcHead.Status == IQCStatus.IQCStatus_New)
        //        {
        //            if (GetIQCDetailCount(iqcHead.IQCNo, new string[] { IQCStatus.IQCStatus_WaitCheck, IQCStatus.IQCStatus_Cancel }) == 0)
        //            {
        //                iqcHead.Status = IQCStatus.IQCStatus_WaitCheck;
        //                iqcHead.MaintainUser = userCode;
        //                UpdateIQCHead(iqcHead);
        //            }
        //        }

        //        //规则三：IQCHead(WaitCheck)全部IQCDetail为不合格、合格且接收、Cancel，则IQCHead(WaitCheck)-->IQCHead(Close)
        //        if (iqcHead.Status == IQCStatus.IQCStatus_WaitCheck)
        //        {
        //            if (IsIQCHeadCanClose(iqcHead.IQCNo))
        //            {
        //                iqcHead.Status = IQCStatus.IQCStatus_Close;
        //                iqcHead.MaintainUser = userCode;
        //                UpdateIQCHead(iqcHead);
        //            }
        //        }

        //        ASN asn = (ASN)GetASN(iqcHead.STNo);

        //        if (asn != null)
        //        {
        //            //规则四：ASN(Release)全部IQCHead为WaitCheck和Close，则ASN(Release)-->ASN(WaitCheck)
        //            if (asn.STStatus == IQCStatus.IQCStatus_Release)
        //            {
        //                if (GetIQCHeadCount(asn.STNo, new string[] { IQCStatus.IQCStatus_WaitCheck, IQCStatus.IQCStatus_Close }) == 0)
        //                {
        //                    asn.STStatus = IQCStatus.IQCStatus_WaitCheck;
        //                    asn.MaintainUser = userCode;
        //                    UpdateASN(asn);
        //                    asnToSRMWaitCheck.Add(asn);
        //                }
        //            }

        //            //规则五：ASN(WaitCheck)全部IQCHead为Close，则ASN(WaitCheck)-->ASN(Close)
        //            if (asn.STStatus == IQCStatus.IQCStatus_WaitCheck)
        //            {
        //                if (GetIQCHeadCount(asn.STNo, new string[] { IQCStatus.IQCStatus_Close }) == 0)
        //                {
        //                    asn.STStatus = IQCStatus.IQCStatus_Close;
        //                    UpdateASN(asn);
        //                    asn.MaintainUser = userCode;
        //                    asnToSRMClose.Add(asn);
        //                }
        //            }
        //        }
        //    }

        //    //规则六：更新SRM中的ASN
        //    foreach (ASN asn in asnToSRMWaitCheck)
        //    {
        //        if (asn.Flag == IQCTicketType.IQCTicketType_ASN && asn.STStatus == IQCStatus.IQCStatus_WaitCheck)
        //        {
        //            SRMWaitCheckASN(asn.STNo);
        //        }
        //    }
        //}

        #endregion


        #region INVReceipt
        public object[] GetAllINVReceipt()
        {
            return this.DataProvider.CustomQuery(typeof(InvReceipt), new SQLCondition(string.Format("select {0} from TBLINVReceipt where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by ReceiptNO", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceipt)))));
        }

        public object[] QueryINVReceipt(string receiptNO, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(InvReceipt), new PagerCondition(string.Format("select {0} from TBLINVReceipt where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and ReceiptNO like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceipt)), receiptNO), "receiptNO", inclusive, exclusive));
        }


        public object[] QueryInvReceiptDetail(string receiptNO)
        {
            string sql = "SELECT receiptdetail.RECEIPTNO,receiptdetail.RECEIPTLINE FROM TBLINVRECEIPT  rectipt left JOIN TBLINVRECEIPTDETAIL receiptdetail  ON rectipt.RECEIPTNO = receiptdetail.RECEIPTNO WHERE rectipt.RECEIPTNO = '" + receiptNO + "' ";//AND  rectipt.ORGID ="+GlobalVariables.CurrentOrganizations.First().OrganizationID;
            return this.DataProvider.CustomQuery(typeof(InvReceiptDetail), new SQLCondition(sql));

        }

        public void UpdateINVReceiptDetail(InvReceiptDetail iNVReceiptDetail)
        {
            this._helper.UpdateDomainObject(iNVReceiptDetail);
        }

        public object[] QueryInvReceiptDetail(string receiptNo, int receitpLine)
        {
            string sql = "SELECT * FROM TBLINVRECEIPTDETAIL WHERE 1=1 ";
            if (receiptNo != string.Empty)
            {
                sql += " AND RECEIPTNO = '" + receiptNo + "'";
            }
            if (receitpLine > -1)
            {
                sql += " AND RECEIPTLINE = '" + receitpLine + "'";
            }
            return this.DataProvider.CustomQuery(typeof(InvReceiptDetail), new SQLCondition(sql));
        }


        public InvReceipt CreateNewInvReceipt()
        {
            return new InvReceipt();
        }
        public void AddInvReceipt(InvReceipt invreceipt)
        {
            this._helper.AddDomainObject(invreceipt);
        }

        public void DeleteInvReceipt(InvReceipt invreceipt)
        {
            this._helper.DeleteDomainObject(invreceipt);
        }

        public void DeleteInvReceipt(InvReceipt[] invreceipts)
        {
            foreach (InvReceipt invreceipt in invreceipts)
            {
                this._helper.DeleteDomainObject(invreceipt);
            }

        }
        public void UpdateInvReceipt(InvReceipt invreceipt)
        {
            this._helper.UpdateDomainObject(invreceipt);
        }

        public object[] GetINVReceipt(string ReceiptNo, string ItemCode, string TicketType, string VendorCode, int CreateDateStart, int CreateDateEnd, string TicketStatus, string StorageId, int OrgId, int inclusive, int exclusive)
        {
            string sql = "SELECT A.createtime,A.muser,A.vendorcode,A.createuser,A.mdate,A.mtime,A.memo, ";
            sql += " A.orgid,A.recstatus, A.storageid,C.STORAGENAME ,B.vendorname, A.rectype,A.receiptno,A.createdate,a.IsAllInStorage FROM TBLINVRECEIPT A";
            sql += " LEFT JOIN TBLVENDOR B ON A.VENDORCODE=B.VENDORCODE ";
            sql += string.Format("  LEFT JOIN TBLSTORAGE C ON A.STORAGEID = C.STORAGECODE  WHERE A.ORGID={0}", OrgId);
            if (ReceiptNo.Length > 0)
            {
                sql += string.Format(" AND UPPER(A.RECEIPTNO) like '%{0}%'", ReceiptNo.ToUpper());
            }
            if (ItemCode.Length > 0)
            {
                sql += string.Format(" AND A.RECEIPTNO IN (SELECT RECEIPTNO FROM TBLINVRECEIPTDETAIL WHERE ITEMCODE IN ({0}))", FormatHelper.ProcessQueryValues(ItemCode));
            }
            if (TicketType.Length > 0)
            {
                sql += string.Format(" AND A.RECTYPE ='{0}'", TicketType);
            }
            if (VendorCode.Length > 0)
            {
                sql += string.Format(" AND A.VENDORCODE IN ({0}) ", FormatHelper.ProcessQueryValues(VendorCode));
            }
            if (CreateDateStart > 0)
            {
                sql += string.Format(" AND A.CREATEDATE >= {0}", CreateDateStart);
            }
            if (CreateDateEnd > 0)
            {
                sql += string.Format(" AND A.CREATEDATE <= {0} ", CreateDateEnd);
            }
            if (TicketStatus.Length > 0)
            {
                sql += string.Format(" AND A.RECSTATUS ='{0}'", TicketStatus);
            }
            if (StorageId.Length > 0)
            {
                sql += string.Format(" AND A.STORAGEID IN  ({0})", FormatHelper.ProcessQueryValues(StorageId)
                    );
            }
            //sql += " ORDER BY RECEIPTNO ";
            return this.DataProvider.CustomQuery(typeof(InvReceiptForQuery), new PagerCondition(sql, "RECEIPTNO", inclusive, exclusive));
        }

        public int GetINVReceiptRowCount(string ReceiptNo, string ItemCode, string TicketType, string VendorCode, int CreateDateStart, int CreateDateEnd, string TicketStatus, string StorageId, int OrgId)
        {
            string sql = string.Format("SELECT COUNT(*) FROM TBLINVRECEIPT WHERE  ORGID={0}", OrgId);
            if (ReceiptNo.Length > 0)
            {
                sql += string.Format(" AND RECEIPTNO like '%{0}%'", ReceiptNo.ToUpper());
            }
            if (ItemCode.Length > 0)
            {
                sql += string.Format(" AND RECEIPTNO IN (SELECT RECEIPTNO FROM TBLINVRECEIPTDETAIL WHERE ITEMCODE IN ({0}))", FormatHelper.ProcessQueryValues(ItemCode));
            }
            if (TicketType.Length > 0)
            {
                sql += string.Format(" AND RECTYPE ='{0}'", TicketType);
            }

            if (VendorCode.Length > 0)
            {
                sql += string.Format(" AND VENDORCODE IN ({0}) ", FormatHelper.ProcessQueryValues(VendorCode));
            }
            if (CreateDateStart > 0 && CreateDateEnd > 0)
            {
                sql += string.Format(" AND CREATEDATE BETWEEN {0} AND {1} ", CreateDateStart, CreateDateEnd);
            }
            if (TicketStatus.Length > 0)
            {
                sql += string.Format(" AND RECSTATUS ='{0}'", TicketStatus);
            }
            if (StorageId.Length > 0)
            {
                sql += string.Format(" AND STORAGEID IN ({0}) ", FormatHelper.ProcessQueryValues(StorageId));
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object GetINVRecepitForUpdate(string ReceiptNo, int OrgId)
        {
            string sql = string.Format("SELECT {0} FROM TBLINVRECEIPT WHERE RECEIPTNO='{1}' and ORGID={2}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceipt)), ReceiptNo, OrgId);
            object[] obj = this.DataProvider.CustomQuery(typeof(InvReceipt), new SQLCondition(sql));
            return ((InvReceipt)obj[0]);

        }
        public object GetINVRecepitForUpdate(string ReceiptNo)
        {
            string sql = string.Format("SELECT * FROM TBLINVRECEIPT WHERE RECEIPTNO='{0}' ", ReceiptNo);
            object[] obj = this.DataProvider.CustomQuery(typeof(InvReceipt), new SQLCondition(sql));
            if (obj != null)
            {
                return obj[0];
            }
            return null;
        }

        public int GetINVReceiptRepeateCount(string ReceiptNo, int OrgId)
        {
            string sql = string.Format("SELECT COUNT(*) FROM TBLINVRECEIPT WHERE RECEIPTNO='{0}' AND ORGID={1}", ReceiptNo, OrgId);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryINVReceiptDetail2ReceiptNo(string ReceiptNo)
        {
            string sql = string.Format("SELECT {0} FROM TBLINVRECEIPTDETAIL  ", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(InvReceiptDetail)));
            sql += string.Format("WHERE RECEIPTNO='{0}' ", ReceiptNo);
            return this.DataProvider.CustomQuery(typeof(InvReceiptDetail), new SQLCondition(sql));
        }

        public object GetInvReceipt(string receiptno)
        {
            return this.DataProvider.CustomSearch(typeof(InvReceipt), new object[] { receiptno });
        }

        #endregion

        #region InvReceiptDetail
        public InvReceiptDetail CreateNewINVReceiptDetail()
        {
            return new InvReceiptDetail();
        }
        public void AddInvReceiptDetail(InvReceiptDetail invreceiptdetail)
        {
            this._helper.AddDomainObject(invreceiptdetail);
        }
        public object GetInvReceiptDetail(string receiptno, int receiptline)
        {
            return this.DataProvider.CustomSearch(typeof(InvReceiptDetail), new object[] { receiptno, receiptline });
        }
        public void DeleteInvReceiptDetail(InvReceiptDetail invreceiptdetail)
        {
            this._helper.DeleteDomainObject(invreceiptdetail);
        }

        public void DeleteInvReceiptDetail(InvReceiptDetail[] invreceiptdetails)
        {
            foreach (InvReceiptDetail invreceiptdetail in invreceiptdetails)
            {
                this._helper.DeleteDomainObject(invreceiptdetail);
            }

        }

        public void UpdateInvReceiptDetail(InvReceiptDetail invreceiptdetail)
        {
            this._helper.UpdateDomainObject(invreceiptdetail);
        }

        public object[] GetInvReceiptDetailForQuery(string ReceiptNo, int OrgId, int inclusive, int exclusive)
        {
            string sql = "SELECT T3.MMACHINETYPE,T1.Venderlotno,T1.RECEIPTNO,T1.RECEIPTLINE,T1.ORDERNO,";
            sql += " T1.ORDERLINE,T1.ITEMCODE,T1.MEMO,T1.INVUSER,T1.PLANQTY, T3.MCODE,T3.MDESC,T1.IsInStorage";
            sql += " FROM TBLINVRECEIPTDETAIL T1 ";
            sql += " LEFT JOIN TBLINVRECEIPT T2 ON T1.RECEIPTNO = T2.RECEIPTNO ";
            sql += " LEFT JOIN TBLMATERIAL T3";
            sql += " ON T2.ORGID = T3.ORGID AND T1.ITEMCODE=T3.MCODE";
            sql += string.Format(" WHERE T2.ORGID={0} AND T1.RECEIPTNO='{1}' ", OrgId, ReceiptNo);
            return this.DataProvider.CustomQuery(typeof(InvReceiptDetailForQuery), new PagerCondition(sql, inclusive, exclusive));
        }

        public int GetInvReceiptDetailCount(string ReceiptNo, int OrgId)
        {
            string sql = "SELECT COUNT(*) FROM TBLINVRECEIPTDETAIL T1 ";
            sql += "LEFT JOIN TBLINVRECEIPT T2 ON T1.RECEIPTNO=T2.RECEIPTNO ";
            sql += " LEFT JOIN TBLMATERIAL T3";
            sql += " ON T2.ORGID = T3.ORGID AND T1.ITEMCODE=T3.MCODE";
            sql += string.Format(" WHERE T2.ORGID={0} AND T1.RECEIPTNO='{1}' ", OrgId, ReceiptNo);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object GetInvReceiptDetailForUpdate(string ReceiptNo, int ReceiptLine)
        {
            string sql = string.Format("SELECT {0} FROM TBLINVRECEIPTDETAIL  ", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(InvReceiptDetail)));
            sql += string.Format("WHERE RECEIPTNO='{0}' AND RECEIPTLINE={1}", ReceiptNo, ReceiptLine);
            object[] obj = this.DataProvider.CustomQuery(typeof(InvReceiptDetail), new SQLCondition(sql));
            if (obj != null)
            {
                return obj[0];
            }
            return null;

        }

        public object[] GetInvReceiptDetailForUpdate(string ReceiptNo)
        {
            string sql = string.Format("SELECT * FROM TBLINVRECEIPTDETAIL WHERE RECEIPTNO='{0}'  ", ReceiptNo);
            object[] obj = this.DataProvider.CustomQuery(typeof(InvReceiptDetail), new SQLCondition(sql));
            if (obj != null)
            {
                return obj;
            }
            return null;

        }



        public int GetInvReceiptDetailRepeatCount(string ReceiptNo, int ReceiptLine)
        {
            string sql = "SELECT COUNT(*) FROM TBLINVRECEIPTDETAIL ";
            sql += string.Format("WHERE RECEIPTNO='{0}' AND RECEIPTLINE={1}", ReceiptNo, ReceiptLine);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public bool CheckAllInvReceiptDetailIsClose(string stno)
        {
            string sql = "SELECT Count(*) FROM TBLINVReceiptDetail WHERE RECSTATUS <> 'Close' AND receiptno = '" + stno + "'";
            if (this.DataProvider.GetCount(new SQLCondition(sql)) > 0)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region IQCTestData
        public IQCTestData CreateNewIQCTestData()
        {
            return new IQCTestData();
        }
        public void AddIQCTestData(IQCTestData iqcTestData)
        {
            this._helper.AddDomainObject(iqcTestData);
        }

        public void DeleteIQCTestData(IQCTestData iqcTestData)
        {
            this._helper.DeleteDomainObject(iqcTestData);
        }

        public void DeleteIQCTestData(IQCTestData[] iqcTestDatas)
        {
            foreach (IQCTestData iqcTestData in iqcTestDatas)
            {
                this._helper.DeleteDomainObject(iqcTestData);
            }

        }
        public object GetIQCTestData(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(IQCTestData), new object[] { serial });
        }
        public void UpdateIQCTestData(IQCTestData iqcTestData)
        {
            this._helper.UpdateDomainObject(iqcTestData);
        }


        #endregion

        #region AsnIQC-- 送检单  add by jinger 2016-02-18
        /// <summary>
        /// TBLASNIQC-- 送检单 
        /// </summary>
        public AsnIQC CreateNewAsnIQC()
        {
            return new AsnIQC();
        }

        public void AddAsnIQC(AsnIQC asniqc)
        {
            this._helper.AddDomainObject(asniqc);
        }

        public void DeleteAsnIQC(AsnIQC asniqc)
        {
            this._helper.DeleteDomainObject(asniqc);
        }

        public void UpdateAsnIQC(AsnIQC asniqc)
        {
            this._helper.UpdateDomainObject(asniqc);
        }

        public object GetAsnIQC(string Iqcno)
        {
            return this.DataProvider.CustomSearch(typeof(AsnIQC), new object[] { Iqcno });
        }

        public object[] GetASNIQCByStatus(string status)
        {
            string sql = string.Format("SELECT {0} FROM TBLASNIQC WHERE STATUS='{1}' ORDER BY MDATE DESC,MTIME DESC", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQC)), status);
            return this.DataProvider.CustomQuery(typeof(AsnIQC), new SQLCondition(sql));
        }

        //查询送检单
        /// <summary>
        ///查询送检单
        /// </summary>
        /// <param name="stNo">入库指令号</param>
        /// <param name="stType">入库类型</param>
        /// <param name="iQCNo">IQC检验单号</param>
        /// <param name="status">IQC检验单状态</param>
        /// <param name="bAppDate">送检日期开始</param>
        /// <param name="eAppDate">送检日期结束</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        public object[] QueryAsnIQC(string stNo, string invno, string stType, string iQCNo, string status, int bAppDate, int eAppDate, string cartonno, string sn,
           string DQMCode, string CusMCode, int inclusive, int exclusive)
        {
            string sql = string.Empty;


            if (!string.IsNullOrEmpty(sn) && string.IsNullOrEmpty(cartonno))
            {
                sql = string.Format(@" SELECT {0} FROM TBLASNIQC A
                                                    INNER JOIN (SELECT IQCNO IQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY FROM TBLASNIQCDETAIL where iqcNo 
                                       in (select distinct iqcno from TBLASNIQCDETAILSN where sn='" + sn + "' ) GROUP BY IQCNO) B ON A.IQCNO=B.IQCCODE WHERE 1=1 ",
                                                             DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCExt)));

            }
            if (!string.IsNullOrEmpty(sn) && !string.IsNullOrEmpty(cartonno))
            {
                sql = string.Format(@" SELECT {0} FROM TBLASNIQC A
                                                    INNER JOIN (SELECT IQCNO IQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY FROM TBLASNIQCDETAIL where iqcNo in
                   (select distinct iqcno from TBLASNIQCDETAILSN where sn='" + sn + "' ) and iqcNo in (select distinct iqcno from TBLASNIQCDETAIL where cartonno='" + cartonno + "' ) " +
                                    "GROUP BY IQCNO) B ON A.IQCNO=B.IQCCODE WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCExt)));

            }
            if (!string.IsNullOrEmpty(cartonno) && string.IsNullOrEmpty(sn))
            {
                sql = string.Format(@" SELECT {0} FROM TBLASNIQC A
                                                    INNER JOIN (SELECT IQCNO IQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY FROM TBLASNIQCDETAIL where iqcNo in 
                                         (select distinct iqcno from TBLASNIQCDETAIL where cartonno='" + cartonno + "' ) GROUP BY IQCNO) B ON A.IQCNO=B.IQCCODE WHERE 1=1 ",
                                                              DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCExt)));

            }
            if (string.IsNullOrEmpty(sql))
                sql = string.Format(@" SELECT {0} FROM TBLASNIQC A
                                                    left JOIN (SELECT IQCNO IQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY FROM TBLASNIQCDETAIL GROUP BY IQCNO) 
                                        B ON A.IQCNO=B.IQCCODE WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCExt)));
            #region add by sam 2016年8月4日
            if (!string.IsNullOrEmpty(DQMCode))
            {
                sql += string.Format(" AND A.DQMCode ='{0}'", DQMCode);
            }
            if (!string.IsNullOrEmpty(CusMCode))
            {
                sql += string.Format(" AND A.CustMCode ='{0}'", CusMCode);
            }
            #endregion
            if (!string.IsNullOrEmpty(invno))
            {
                sql += string.Format(" AND A.invno ='{0}'", invno);
            }

            if (!string.IsNullOrEmpty(stNo))
            {
                sql += string.Format(" AND A.STNO ='{0}'", stNo);
            }

            if (!string.IsNullOrEmpty(stType) && stType.IndexOf(",") > 0)
            {
                sql += string.Format(@" AND A.STTYPE IN ({0})", stType);
            }
            else if (!string.IsNullOrEmpty(stType))
            {
                sql += string.Format(@" AND A.STTYPE IN ('{0}')", stType);
            }

            if (!string.IsNullOrEmpty(iQCNo))
            {
                sql += string.Format(@" AND A.IQCNO = '{0}'", iQCNo);
            }

            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(@" AND A.STATUS = '{0}'", status);
            }
            if (bAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE >= {0}", bAppDate);
            }
            if (eAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE <= {0}", eAppDate);
            }
            return this.DataProvider.CustomQuery(typeof(AsnIQCExt), new PagerCondition(sql, "A.IQCNO DESC", inclusive, exclusive));
        }


        public object[] QueryAsnIQC1(string[] userGroup, string stNo, string invno, string stType, string iQCNo, string status, int bAppDate, int eAppDate, string cartonno, string sn,
           string DQMCode, string CusMCode, string storageCode, int inclusive, int exclusive)
        {
            string sql = @"SELECT A.*,B.APPQTY,B.NGQTY,B.RETURNQTY,B.REFORMQTY ,k.storagecode FROM TBLASNIQC A inner join tblasn k on a.stno=k.stno LEFT JOIN 
(SELECT SUM(NGQTY) NGQTY,SUM(RETURNQTY) RETURNQTY,SUM(REFORMQTY) REFORMQTY,SUM(QTY) APPQTY ,IQCNO FROM TBLASNIQCDETAIL GROUP BY IQCNO ) B ON A.IQCNO=B.IQCNO
WHERE A.STNO IN(
 SELECT DISTINCT STNO FROM
 (SELECT STNO FROM TBLASN WHERE STORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in (" + SqlFormat(userGroup) + @")) UNION 
 SELECT STNO FROM TBLASN WHERE FROMSTORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE IN(" + SqlFormat(userGroup) + @"))) T 
 ) ";

            if (!string.IsNullOrEmpty(sn))
            {
                sql += @" AND A.IQCNO IN (select distinct iqcno from TBLASNIQCDETAILSN where sn='" + sn + "') ";


            }
            if (!string.IsNullOrEmpty(cartonno))
            {
                sql += @" AND A.IQCNO in (select distinct iqcno from TBLASNIQCDETAIL where cartonno='" + cartonno + "' )";


            }

            #region add by sam 2016年8月4日
            if (!string.IsNullOrEmpty(DQMCode))
            {
                sql += string.Format(" AND A.DQMCode ='{0}'", DQMCode);
            }
            if (!string.IsNullOrEmpty(CusMCode))
            {
                sql += string.Format(" AND A.vendormcode ='{0}' or a.custmCode='{0}'", CusMCode);
            }
            #endregion
            if (!string.IsNullOrEmpty(invno))
            {
                sql += string.Format(" AND A.invno ='{0}'", invno);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND k.storagecode ='{0}'", storageCode);
            }
            if (!string.IsNullOrEmpty(stNo))
            {
                sql += string.Format(" AND A.STNO ='{0}'", stNo);
            }

            if (!string.IsNullOrEmpty(stType) && stType.IndexOf(",") > 0)
            {
                sql += string.Format(@" AND A.STTYPE IN ({0})", stType);
            }
            else if (!string.IsNullOrEmpty(stType))
            {
                sql += string.Format(@" AND A.STTYPE IN ('{0}')", stType);
            }

            if (!string.IsNullOrEmpty(iQCNo))
            {
                sql += string.Format(@" AND A.IQCNO = '{0}'", iQCNo);
            }

            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(@" AND A.STATUS = '{0}'", status);
            }
            if (bAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE >= {0}", bAppDate);
            }
            if (eAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE <= {0}", eAppDate);
            }
            return this.DataProvider.CustomQuery(typeof(AsnIQCExt), new PagerCondition(sql, "A.IQCNO DESC", inclusive, exclusive));
        }

        public int QueryAsnIQC1Count(string[] userGroup, string stNo, string invno, string stType, string iQCNo, string status, int bAppDate, int eAppDate, string cartonno, string sn,
           string DQMCode, string CusMCode, string storageCode)
        {
            string sql = @"SELECT count(*) FROM TBLASNIQC A inner join tblasn k on a.stno=k.stno LEFT JOIN   
(SELECT SUM(NGQTY) NGQTY,SUM(RETURNQTY) RETURNQTY,SUM(REFORMQTY) REFORMQTY,SUM(QTY) APPQTY ,IQCNO FROM TBLASNIQCDETAIL GROUP BY IQCNO ) B ON A.IQCNO=B.IQCNO
WHERE A.STNO IN(
 SELECT DISTINCT STNO FROM
 (SELECT STNO FROM TBLASN WHERE STORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in (" + SqlFormat(userGroup) + @")) UNION 
 SELECT STNO FROM TBLASN WHERE FROMSTORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE IN(" + SqlFormat(userGroup) + @"))) T 
 ) ";

            if (!string.IsNullOrEmpty(sn))
            {
                sql += @" AND A.IQCNO IN (select distinct iqcno from TBLASNIQCDETAILSN where sn='" + sn + "') ";


            }
            if (!string.IsNullOrEmpty(cartonno))
            {
                sql += @" AND A.IQCNO in (select distinct iqcno from TBLASNIQCDETAIL where cartonno='" + cartonno + "' )";


            }

            #region add by sam 2016年8月4日
            if (!string.IsNullOrEmpty(DQMCode))
            {
                sql += string.Format(" AND A.DQMCode ='{0}'", DQMCode);
            }
            if (!string.IsNullOrEmpty(CusMCode))
            {
                sql += string.Format(" AND A.vendormcode ='{0}' or a.custmCode='{0}'", CusMCode);
            }
            #endregion
            if (!string.IsNullOrEmpty(invno))
            {
                sql += string.Format(" AND A.invno ='{0}'", invno);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND k.storagecode ='{0}'", storageCode);
            }
            if (!string.IsNullOrEmpty(stNo))
            {
                sql += string.Format(" AND A.STNO ='{0}'", stNo);
            }

            if (!string.IsNullOrEmpty(stType) && stType.IndexOf(",") > 0)
            {
                sql += string.Format(@" AND A.STTYPE IN ({0})", stType);
            }
            else if (!string.IsNullOrEmpty(stType))
            {
                sql += string.Format(@" AND A.STTYPE IN ('{0}')", stType);
            }

            if (!string.IsNullOrEmpty(iQCNo))
            {
                sql += string.Format(@" AND A.IQCNO = '{0}'", iQCNo);
            }

            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(@" AND A.STATUS = '{0}'", status);
            }
            if (bAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE >= {0}", bAppDate);
            }
            if (eAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE <= {0}", eAppDate);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        private string SqlFormat(string[] strs)
        {
            if (strs.Length == 0)
                return "''";
            System.Text.StringBuilder sb = new System.Text.StringBuilder(200);

            foreach (string str in strs)
            {
                sb.Append("'");
                sb.Append(str);
                sb.Append("',");

            }
            return sb.ToString().TrimEnd(',');
        }

        //送检单总行数
        /// <summary>
        ///送检单总行数
        /// </summary>
        /// <param name="stNo">入库指令号</param>
        /// <param name="stType">入库类型</param>
        /// <param name="iQCNo">IQC检验单号</param>
        /// <param name="status">IQC检验单状态</param>
        /// <param name="bAppDate">送检日期开始</param>
        /// <param name="eAppDate">送检日期结束</param>
        /// <returns></returns>
        public int QueryAsnIQCCount(string stNo, string invno, string stType, string iQCNo, string status, int bAppDate, int eAppDate, string cartonno,
            string sn, string DQMCode, string CusMCode)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(sn) && string.IsNullOrEmpty(cartonno))
            {
                sql = @" SELECT COUNT(1) FROM TBLASNIQC A
                                                    INNER JOIN (SELECT IQCNO IQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY FROM TBLASNIQCDETAIL where iqcNo in (select distinct iqcno from TBLASNIQCDETAILSN where sn='" + sn + "' ) GROUP BY IQCNO) B ON A.IQCNO=B.IQCCODE WHERE 1=1 ";

            }
            if (!string.IsNullOrEmpty(sn) && !string.IsNullOrEmpty(cartonno))
            {
                sql = @" SELECT COUNT(1) FROM TBLASNIQC A
                                                    INNER JOIN (SELECT IQCNO IQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY FROM TBLASNIQCDETAIL where iqcNo in (select distinct iqcno from TBLASNIQCDETAILSN where sn='" + sn + "' ) and iqcNo in (select distinct iqcno from TBLASNIQCDETAIL where cartonno='" + cartonno + "' ) GROUP BY IQCNO) B ON A.IQCNO=B.IQCCODE WHERE 1=1 ";

            }
            if (!string.IsNullOrEmpty(cartonno) && string.IsNullOrEmpty(sn))
            {
                sql = @" SELECT COUNT(1) FROM TBLASNIQC A
                                                    INNER JOIN (SELECT IQCNO IQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY FROM TBLASNIQCDETAIL where iqcNo in (select distinct iqcno from TBLASNIQCDETAIL where cartonno='" + cartonno + "' ) GROUP BY IQCNO) B ON A.IQCNO=B.IQCCODE WHERE 1=1 ";

            }
            if (string.IsNullOrEmpty(sql))
                sql = @" SELECT COUNT(1) FROM TBLASNIQC A
                                                    left JOIN (SELECT IQCNO IQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY FROM TBLASNIQCDETAIL GROUP BY IQCNO) B ON A.IQCNO=B.IQCCODE WHERE 1=1 ";

            #region add by sam 2016年8月4日
            if (!string.IsNullOrEmpty(DQMCode))
            {
                sql += string.Format(" AND A.DQMCode ='{0}'", DQMCode);
            }
            if (!string.IsNullOrEmpty(CusMCode))
            {
                sql += string.Format(" AND A.CustMCode ='{0}'", CusMCode);
            }
            #endregion
            if (!string.IsNullOrEmpty(stNo))
            {
                sql += string.Format(" AND A.STNO ='{0}'", stNo);
            }
            if (!string.IsNullOrEmpty(invno))
            {
                sql += string.Format(" AND A.invno ='{0}'", invno);
            }
            if (!string.IsNullOrEmpty(stType) && stType.IndexOf(",") > 0)
            {
                sql += string.Format(@" AND A.STTYPE IN ({0})", stType);
            }
            else if (!string.IsNullOrEmpty(stType))
            {
                sql += string.Format(@" AND A.STTYPE IN ('{0}')", stType);
            }

            if (!string.IsNullOrEmpty(iQCNo))
            {
                sql += string.Format(@" AND A.IQCNO = '{0}'", iQCNo);
            }

            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(@" AND A.STATUS = '{0}'", status);
            }
            if (bAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE >= {0}", bAppDate);
            }
            if (eAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE <= {0}", eAppDate);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region AsnIQCDetail-- 送检单明细  add by jinger 2016-02-18
        /// <summary>
        /// TBLASNIQCDETAIL-- 送检单明细 
        /// </summary>
        public AsnIQCDetail CreateNewAsnIQCDetail()
        {
            return new AsnIQCDetail();
        }

        public void AddAsnIQCDetail(AsnIQCDetail asniqcdetail)
        {
            this._helper.AddDomainObject(asniqcdetail);
        }

        public void DeleteAsnIQCDetail(AsnIQCDetail asniqcdetail)
        {
            this._helper.DeleteDomainObject(asniqcdetail);
        }

        public void UpdateAsnIQCDetail(AsnIQCDetail asniqcdetail)
        {
            this._helper.UpdateDomainObject(asniqcdetail);
        }

        public object GetAsnIQCDetail(int Stline, string Iqcno, string Stno)
        {
            return this.DataProvider.CustomSearch(typeof(AsnIQCDetail), new object[] { Stline, Iqcno, Stno });
        }

        public void UpdateAsnIQCDetailByIqcno(string Iqcno, string status)
        {
            string sql = string.Format("update TBLASNIQCDETAIL set QcStatus='{0}',mdate=sys_date,mtime=sys_time where IqcNo='{1}'", status, Iqcno);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        //根据IQC检验单号获取送检单明细信息
        /// <summary>
        /// 根据IQC检验单号获取送检单明细信息
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <returns></returns>
        public object[] GetAsnIQCDetailByIqcNo(string iqcNo)
        {
            return this.DataProvider.CustomQuery(typeof(AsnIQCDetail), new SQLCondition(string.Format("SELECT {0} FROM TBLASNIQCDETAIL WHERE IQCNO='{1}' ",
                                                                                                            DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetail)),
                                                                                                            iqcNo))
                                               );
        }

        //查询送检单明细
        /// <summary>
        ///查询送检单明细
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <param name="cartonNo">箱号</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        public object[] QueryAsnIQCDetail(string iqcNo, string cartonNo, int inclusive, int exclusive)
        {
            string sql = @"SELECT A.*,
                                    B.DQMCODE,
                                    B.VENDORMCODE,
                                    B.STATUS,
                                    B.IQCTYPE,
                                    M.MCONTROLTYPE 
                                    FROM TBLASNIQCDETAIL A
                                    LEFT JOIN TBLASNIQC B ON A.IQCNO = B.IQCNO
                                    LEFT JOIN TBLMATERIAL M ON B.DQMCODE = M.DQMCODE
                                    WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(iqcNo))
            {
                sql += string.Format(" AND A.IQCNO = '{0}'", iqcNo);
            }

            if (!string.IsNullOrEmpty(cartonNo))
            {
                sql += string.Format(" AND A.CARTONNO ='{0}'", cartonNo);
            }


            return this.DataProvider.CustomQuery(typeof(AsnIQCDetailExt), new PagerCondition(sql, "A.MDATE DESC,A.MTIME DESC", inclusive, exclusive));
        }

        //送检单明细总行数
        /// <summary>
        ///送检单明细总行数
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <param name="cartonNo">箱号</param>
        /// <returns></returns>
        public int QueryAsnIQCDetailCount(string iqcNo, string cartonNo)
        {
            string sql = @" SELECT COUNT(1) 
                                FROM TBLASNIQCDETAIL A
                                LEFT JOIN TBLASNIQC B ON A.IQCNO = B.IQCNO
                                LEFT JOIN TBLMATERIAL M ON B.DQMCODE = M.DQMCODE
                                WHERE 1 = 1 ";
            if (!string.IsNullOrEmpty(iqcNo))
            {
                sql += string.Format(" AND A.IQCNO = '{0}'", iqcNo);
            }
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sql += string.Format(" AND A.CARTONNO ='{0}'", cartonNo);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //根据IQC检验单号获取送检单明细中缺陷品数总数量
        /// <summary>
        /// 根据IQC检验单号获取送检单明细中缺陷品数总数量
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <returns></returns>
        public int GetSumNgQtyFromAsnIQCDetail(string iqcNo)
        {
            string sql = string.Format("SELECT NVL(SUM(NGQTY),0) NGQTY FROM TBLASNIQCDETAIL WHERE IQCNO='{0}'", iqcNo);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int GetReformAndReturnNgQtyFromIQCNO(string iqcNo)
        {
            string sql = string.Format("SELECT NVL(SUM(NGQTY),0) NGQTY,SQESTATUS FROM TBLASNIQCDETAILEC WHERE IQCNO='{0}' GROUP BY SQESTATUS", iqcNo);

            object[] objs = this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql)); ;
            if (objs != null && objs.Length > 0)
            {
                int sum = 0;
                foreach (AsnIQCDetailEc ec in objs)
                {
                    if (ec.SqeStatus == "Return")
                        sum = sum + ec.NgQty;
                    if (ec.SqeStatus == "Reform")
                        sum = sum + ec.NgQty;
                }
                return sum;
            }
            return 0;


        }

        #endregion

        #region AsnIQCDetailEc-- 送检单明细对应缺陷明细表  add by jinger 2016-02-19
        /// <summary>
        /// TBLASNIQCDETAILEC-- 送检单明细对应缺陷明细表 
        /// </summary>
        public AsnIQCDetailEc CreateNewAsnIQCDetailEc()
        {
            return new AsnIQCDetailEc();
        }

        public void AddAsnIQCDetailEc(AsnIQCDetailEc asniqcdetailec)
        {
            this._helper.AddDomainObject(asniqcdetailec);
        }

        public void DeleteAsnIQCDetailEc(AsnIQCDetailEc asniqcdetailec)
        {
            this._helper.DeleteDomainObject(asniqcdetailec);
        }
        public void DeleteAsnIQCDetailEc(AsnIQCDetailEc[] asniqcdetailecs)
        {
            //this._helper.DeleteDomainObject(asniqcdetailecs);   
            foreach (AsnIQCDetailEc ec in asniqcdetailecs)
            {
                DeleteAsnIQCDetailEc(ec);
            }
        }

        public void UpdateAsnIQCDetailEc(AsnIQCDetailEc asniqcdetailec)
        {
            this._helper.UpdateDomainObject(asniqcdetailec);
        }

        public object GetAsnIQCDetailEc(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(AsnIQCDetailEc), new object[] { Serial });
        }

        //获取送检单明细对应缺陷明细
        /// <summary>
        /// 获取送检单明细对应缺陷明细
        /// </summary>
        /// <param name="ECode">缺陷品数</param>
        /// <param name="Stline">ASN单行项目</param>
        /// <param name="Iqcno">IQC送检单号</param>
        /// <param name="Stno">ASN单号</param>
        /// <param name="Sn">SN条码</param>
        /// <returns></returns>
        public object[] GetAsnIQCDetailEc(string ECode, string Stline, string Iqcno, string Stno, string Sn)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNIQCDETAILEC 
WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetailEc)));
            if (!string.IsNullOrEmpty(ECode))
            {
                sql += string.Format(" AND ECODE='{0}'", ECode);
            }
            if (!string.IsNullOrEmpty(Stline))
            {
                sql += string.Format(" AND STLINE='{0}'", Stline);
            }
            if (!string.IsNullOrEmpty(Iqcno))
            {
                sql += string.Format(" AND IQCNO='{0}'", Iqcno);
            }
            if (!string.IsNullOrEmpty(Stno))
            {
                sql += string.Format(" AND STNO='{0}'", Stno);
            }
            if (!string.IsNullOrEmpty(Sn))
            {
                sql += string.Format(" AND SN='{0}'", Sn);
            }

            return this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
        }
        //获取送检单明细对应缺陷明细
        /// <summary>
        /// 获取送检单明细对应缺陷明细
        /// </summary>
        /// <param name="ECode">缺陷品数</param>
        /// <param name="Stline">ASN单行项目</param>
        /// <param name="Iqcno">IQC送检单号</param>
        /// <param name="Stno">ASN单号</param>
        /// <param name="Sn">SN条码</param>
        /// <returns></returns>
        public object[] GetAsnIQCDetailEc(string ECGCode, string ECode, string Stline, string Iqcno, string Stno, string Sn)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNIQCDETAILEC 
WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetailEc)));
            if (!string.IsNullOrEmpty(ECGCode))
            {
                sql += string.Format(" AND ECGCode='{0}'", ECGCode);
            }
            if (!string.IsNullOrEmpty(ECode))
            {
                sql += string.Format(" AND ECODE='{0}'", ECode);
            }
            if (!string.IsNullOrEmpty(Stline))
            {
                sql += string.Format(" AND STLINE='{0}'", Stline);
            }
            if (!string.IsNullOrEmpty(Iqcno))
            {
                sql += string.Format(" AND IQCNO='{0}'", Iqcno);
            }
            if (!string.IsNullOrEmpty(Stno))
            {
                sql += string.Format(" AND STNO='{0}'", Stno);
            }
            if (!string.IsNullOrEmpty(Sn))
            {
                sql += string.Format(" AND SN='{0}'", Sn);
            }

            return this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
        }
        //add by sam 2016年3月21日
        public object[] GetAsnIQCDetailEcByStatus(string ECode, string Stline, string Iqcno, string Stno, string sqeStatus)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNIQCDETAILEC 
              WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetailEc)));
            if (!string.IsNullOrEmpty(ECode))
            {
                sql += string.Format(" AND ECODE='{0}'", ECode);
            }
            if (!string.IsNullOrEmpty(Stline))
            {
                sql += string.Format(" AND STLINE='{0}'", Stline);
            }
            if (!string.IsNullOrEmpty(Iqcno))
            {
                sql += string.Format(" AND IQCNO='{0}'", Iqcno);
            }
            if (!string.IsNullOrEmpty(Stno))
            {
                sql += string.Format(" AND STNO='{0}'", Stno);
            }
            if (!string.IsNullOrEmpty(sqeStatus))
            {
                sql += string.Format(" AND sqeStatus in '{0}'", sqeStatus);
            }
            return this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
        }

        //add by sam 2016年3月22日
        public int GetAsnIQCDetailEcQtyByStatus(string ECode, string Stline, string Iqcno, string Stno, string sqeStatus)
        {
            string sql = string.Format(@"SELECT sum (NGQTY ) FROM TBLASNIQCDETAILEC WHERE 1=1 ");
            if (!string.IsNullOrEmpty(ECode))
            {
                sql += string.Format(" AND ECODE='{0}'", ECode);
            }
            if (!string.IsNullOrEmpty(Stline))
            {
                sql += string.Format(" AND STLINE='{0}'", Stline);
            }
            if (!string.IsNullOrEmpty(Iqcno))
            {
                sql += string.Format(" AND IQCNO='{0}'", Iqcno);
            }
            if (!string.IsNullOrEmpty(Stno))
            {
                sql += string.Format(" AND STNO='{0}'", Stno);
            }
            if (!string.IsNullOrEmpty(sqeStatus))
            {
                sql += string.Format(" AND sqeStatus in '{0}'", sqeStatus);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #region Amy Add @20160405
        public object[] GetUpLoadFilesByInvDocNo(string InvDocNo, string InvDocType)
        {
            string sql = string.Format("select {0} from TBLINVDOC where InvDocNo='{1}' and InvDocType='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvDoc)), InvDocNo, InvDocType);
            return this.DataProvider.CustomQuery(typeof(InvDoc), new SQLCondition(sql));
        }
        public DataSet GetIQCandMaterialInfoByIQCNo(string iqcno)
        {
            string sql = string.Format("select  c.dqmcode,c.custmcode,c.cdate,c.invno,t.mchlongdesc,r.vendorname,c.qty from tblasniqc c left join tblmaterial t on c.dqmcode=t.dqmcode left join tblvendor r on c.vendorcode=r.vendorcode  where iqcno='{0}'", iqcno);
            return ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
        }

        public int GetAsnIqcDetailSum(string IQCNO)
        {
            string sql = "select sum(qty) qty from TBLASNIQCDETAIL  where IQCNO= '" + IQCNO + "'";
            object[] ooo = this.DataProvider.CustomQuery(typeof(AsnIQCDetail), new SQLCondition(sql));

            if (ooo != null && ooo.Length > 0)
                return ((AsnIQCDetail)ooo[0]).Qty;
            else
                return 0;
        }
        public object[] GetAsnIQCDetailEcByIqcNo(string iqcNo, string NGFlag)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNIQCDETAILEC A WHERE A.IQCNO = '{1}' AND NGFLAG='{2}' ORDER BY STLINE",
                                      DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetailEc)), iqcNo, NGFlag);
            return this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
        }
        public object[] GetAsnIQCDetailEcByIqcNo(string iqcNo, string NGFlag, string cartonNo)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNIQCDETAILEC A WHERE A.IQCNO = '{1}' AND NGFLAG='{2}' and cartonno='{3}' ORDER BY STLINE",
                                      DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetailEc)), iqcNo, NGFlag, cartonNo);
            return this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
        }
        public object GetParaValueByText(string Text)
        {
            string sql = "select * from tblsysparam m where m.paramgroupcode='SQESTATUS' and m.paramdesc='" + Text + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
                return objs[0];
            return null;
        }

        public BenQGuru.eMES.Domain.MOModel.Vendor GetVendor(string vendorCode)
        {
            return (BenQGuru.eMES.Domain.MOModel.Vendor)this.DataProvider.CustomSearch(typeof(BenQGuru.eMES.Domain.MOModel.Vendor), new object[] { vendorCode });
        }
        public object[] GetAsnIqcDetailSNByIqcNoAndCartonNo(string Iqcno, string CartonNo)
        {
            return this.DataProvider.CustomQuery(typeof(AsnIqcDetailSN), new SQLCondition(string.Format(@"SELECT {0} FROM TBLASNIQCDETAILSN WHERE IQCNO='{1}' AND CARTONNO='{2}'  ORDER BY STLINE",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIqcDetailSN)), Iqcno, CartonNo)));
        }
        public bool GetAsnIQCDetailEc(string iqcNo, string cartonNo, string NGFlag)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNIQCDETAILEC A WHERE A.IQCNO = '{1}' AND A.CARTONNO ='{2}' AND NGFLAG='Y' AND SQESTATUS IS NOT NULL ORDER BY STLINE",
                                     DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetailEc)), iqcNo, cartonNo);
            object[] objs = this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
            if (objs != null)
            {
                return true;
            }
            else
            {
                //没有已经维护的整箱不良
                if (NGFlag == "Y")
                    return true;
                else
                {
                    //是否有需要维护的整箱不良
                    sql = string.Format(@"SELECT {0} FROM TBLASNIQCDETAILEC A WHERE A.IQCNO = '{1}' AND A.CARTONNO ='{2}' AND NGFLAG='Y' ORDER BY STLINE",
                                     DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetailEc)), iqcNo, cartonNo);
                    object[] objs1 = this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
                    if (objs1 == null)
                        return true;
                    else
                        return false;
                }
            }
        }
        public bool CheckALLNGStatus(string iqcNo, string cartonNo, string NGFlag)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNIQCDETAILEC A WHERE A.IQCNO = '{1}' AND A.CARTONNO ='{2}' AND NGFLAG='Y' AND SQESTATUS IS NOT NULL ORDER BY STLINE",
                                     DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetailEc)), iqcNo, cartonNo);
            object[] objs = this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
            if (objs != null)
            {
                AsnIQCDetailEc ec = objs[0] as AsnIQCDetailEc;
                if (ec.SqeStatus == "Return" || ec.SqeStatus == "Reform")
                    return true;
                else
                    return false;
            }
            return false;
        }
        public object[] GetAsnIQCDetailEcByIQCNoAndCartonNo(string iqcNo, string cartonNo)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNIQCDETAILEC A WHERE A.IQCNO = '{1}' AND A.CARTONNO ='{2}' ORDER BY STLINE",
                                      DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetailEc)), iqcNo, cartonNo);
            return this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
        }

        public object[] GetAsnIQCDetailEcByIQCNoAndCartonNo1(string iqcNo, string cartonNo)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNIQCDETAILEC A WHERE A.IQCNO = '{1}' AND A.CARTONNO ='{2}'  AND NGFLAG='Y' AND SQESTATUS IS NOT NULL ORDER BY STLINE",
                                      DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetailEc)), iqcNo, cartonNo);
            return this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
        }
        #endregion
        //获取送检单明细对应缺陷明细
        /// <summary>
        /// 获取送检单明细对应缺陷明细
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <param name="cartonNo">箱号</param>
        /// <returns></returns>
        public object[] GetAsnIQCDetailEc(string iqcNo, string cartonNo)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNIQCDETAILEC A WHERE A.IQCNO = '{1}' AND A.CARTONNO ='{2}' AND SQESTATUS IS NOT NULL ORDER BY STLINE",
                                      DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetailEc)), iqcNo, cartonNo);
            return this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
        }

        //根据IQC检验单获取送检单明细对应缺陷明细
        /// <summary>
        /// 根据IQC检验单获取送检单明细对应缺陷明细
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <returns></returns>
        public object[] GetAsnIQCDetailEcByIqcNo(string iqcNo)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNIQCDETAILEC A WHERE A.IQCNO = '{1}' ORDER BY STLINE",
                                      DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetailEc)), iqcNo);
            return this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
        }

        //查询送检单明细对应缺陷明细
        /// <summary>
        /// 查询送检单明细对应缺陷明细
        /// </summary>
        /// <param name="iqcNo">IQC检验单</param>
        /// <param name="stNo">ASN单号</param>
        /// <param name="stLine">ASN单行项目</param>
        /// <param name="cartonNo">箱号</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        public object[] QueryAsnIQCDetailEc(string iqcNo, string stNo, string stLine, string cartonNo, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNIQCDETAILEC A WHERE 1=1",
                                     DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetailEc)));

            if (!string.IsNullOrEmpty(iqcNo))
            {
                sql += string.Format(" AND A.IQCNO = '{0}'", iqcNo);
            }
            if (!string.IsNullOrEmpty(stNo))
            {
                sql += string.Format(" AND A.STNO = '{0}'", stNo);
            }
            //if (!string.IsNullOrEmpty(stLine))
            //{
            //    sql += string.Format(" AND A.STLINE = '{0}'", stLine);
            //}
            //if (!string.IsNullOrEmpty(cartonNo))
            //{
            //    sql += string.Format(" AND A.CARTONNO ='{0}'", cartonNo);
            //}
            return this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new PagerCondition(sql, "A.MDATE DESC,A.MTIME DESC", inclusive, exclusive));
        }
        #region   Amy Add @20160401
        public object GetSampleQTYByIqcQTY(int QTY)
        {
            string sql = string.Format("SELECT {0} FROM TBLAQL WHERE {1} BETWEEN LOTSIZEMIN AND LOTSIZEMAX ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AQL)), QTY);
            object[] objs = this.DataProvider.CustomQuery(typeof(AQL), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
                return objs[0];
            return null;
        }

        public object[] GetSampleQTYByIqcQTY1(int QTY)
        {
            string sql = string.Format("SELECT {0} FROM TBLAQL WHERE {1} BETWEEN LOTSIZEMIN AND LOTSIZEMAX ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AQL)), QTY);
            object[] objs = this.DataProvider.CustomQuery(typeof(AQL), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
                return objs;
            return null;
        }

        public AQL GetAQL(int aqlSeq, string aqlLevel)
        {
            string sql = "SELECT * FROM TBLAQL WHERE AQLSEQ=" + aqlSeq + "and AQLLEVEL='" + aqlLevel + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(AQL), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
                return (AQL)objs[0];
            return null;
        }

        public object GetASNIQCDetailByIQCNoAndCartonNo(string IQCNo, string CartonNo)
        {
            string sql = string.Format("SELECT {0} FROM TBLASNIQCDETAIL WHERE IQCNO='{1}' AND CARTONNO='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetail)), IQCNo, CartonNo);
            object[] objs = this.DataProvider.CustomQuery(typeof(AsnIQCDetail), new SQLCondition(sql));
            if (objs != null)
                return objs[0];
            return null;
        }
        #endregion

        //送检单明细对应缺陷明细总行数
        /// <summary>
        /// 送检单明细对应缺陷明细总行数
        /// </summary>
        /// <param name="iqcNo">IQC检验单</param>
        /// <param name="stNo">ASN单号</param>
        /// <param name="stLine">ASN单行项目</param>
        /// <param name="cartonNo">箱号</param>
        /// <returns></returns>
        public int QueryAsnIQCDetailEcCount(string iqcNo, string stNo, string stLine, string cartonNo)
        {
            string sql = @" SELECT COUNT(1) 
                                FROM TBLASNIQCDETAILEC A WHERE 1 = 1 ";
            if (!string.IsNullOrEmpty(iqcNo))
            {
                sql += string.Format(" AND A.IQCNO = '{0}'", iqcNo);
            }
            if (!string.IsNullOrEmpty(stNo))
            {
                sql += string.Format(" AND A.STNO = '{0}'", stNo);
            }
            if (!string.IsNullOrEmpty(stLine))
            {
                sql += string.Format(" AND A.STLINE = '{0}'", stLine);
            }
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sql += string.Format(" AND A.CARTONNO ='{0}'", cartonNo);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        //查询送检单明细对应缺陷明细
        /// <summary>
        /// 查询送检单明细对应缺陷明细
        /// </summary>
        /// <param name="iqcNo">IQC检验单</param>
        /// <param name="sn">SN</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        public object[] QueryAsnIQCDetailEc(string iqcNo, string sn, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNIQCDETAILEC A WHERE 1=1 ",
                                     DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetailEc)));
            if (!string.IsNullOrEmpty(iqcNo))
            {
                sql += string.Format(" AND A.IQCNO = '{0}'", iqcNo);
            }

            if (!string.IsNullOrEmpty(sn))
            {
                sql += string.Format(" AND A.SN = '{0}'", sn);
            }
            return this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new PagerCondition(sql, "A.MDATE DESC,A.MTIME DESC", inclusive, exclusive));
        }

        public object[] QueryAsnIQCDetailEc1(string iqcNo, string sn, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNIQCDETAILEC A,TBLASNIQC B WHERE A.IQCNO=B.IQCNO  ",
                                     DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQCDetailEc)));
            if (!string.IsNullOrEmpty(iqcNo))
            {
                sql += string.Format(" AND A.IQCNO = '{0}'", iqcNo);
            }

            if (!string.IsNullOrEmpty(sn))
            {
                sql += string.Format(" AND A.SN = '{0}'", sn);
            }
            return this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new PagerCondition(sql, "A.MDATE DESC,A.MTIME DESC", inclusive, exclusive));
        }

        public int QueryAsnIQCDetailEcCount1(string iqcNo, string sn)
        {
            string sql = @" SELECT COUNT(1) 
                                FROM TBLASNIQCDETAILEC A WHERE 1 = 1 ";
            if (!string.IsNullOrEmpty(iqcNo))
            {
                sql += string.Format(" AND A.IQCNO = '{0}'", iqcNo);
            }

            if (!string.IsNullOrEmpty(sn))
            {
                sql += string.Format(" AND A.SN = '{0}'", sn);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        //送检单明细对应缺陷明细总行数
        /// <summary>
        /// 送检单明细对应缺陷明细总行数
        /// </summary>
        /// <param name="iqcNo">IQC检验单</param>
        /// <param name="sn">SN</param>
        /// <returns></returns>
        public int QueryAsnIQCDetailEcCount(string iqcNo, string sn)
        {
            string sql = @" SELECT COUNT(1) 
                                FROM TBLASNIQCDETAILEC A WHERE 1 = 1 ";
            if (!string.IsNullOrEmpty(iqcNo))
            {
                sql += string.Format(" AND A.IQCNO = '{0}'", iqcNo);
            }

            if (!string.IsNullOrEmpty(sn))
            {
                sql += string.Format(" AND A.SN = '{0}'", sn);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //获取送检单明细对应缺陷明细中缺陷品数总数量
        /// <summary>
        ///获取送检单明细对应缺陷明细中缺陷品数总数量
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <param name="ngFlag">缺陷种类</param>
        /// <returns></returns>
        public int GetSumNgQtyFromAsnIQCDetailEc(string iqcNo, string ngFlag)
        {
            string sql = string.Format("SELECT NVL(SUM(NGQTY),0) NGQTY FROM TBLASNIQCDETAILEC WHERE IQCNO='{0}'", iqcNo);
            if (!string.IsNullOrEmpty(ngFlag))
            {
                sql += string.Format(@" AND NGFLAG = '{0}'", ngFlag);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        public int GetSumNgQtyFromAsnIQCDetailEc1(string iqcNo, string CartonNo, string ngFlag)
        {
            string sql = string.Format("SELECT NVL(SUM(NGQTY),0) NGQTY FROM TBLASNIQCDETAILEC WHERE IQCNO='{0}'", iqcNo);
            if (!string.IsNullOrEmpty(ngFlag))
            {
                sql += string.Format(@" AND NGFLAG = '{0}'", ngFlag);
            }
            if (!string.IsNullOrEmpty(CartonNo))
            {
                sql += string.Format(@" AND CartonNo = '{0}'", CartonNo);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        //获取送检单明细对应缺陷明细中缺陷品数总数量
        /// <summary>
        ///获取送检单明细对应缺陷明细中缺陷品数总数量
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <param name="cartonNo">箱号</param>
        /// <param name="ngFlag">缺陷种类</param>
        /// <returns></returns>
        public int GetSumNgQtyFromAsnIQCDetailEc(string iqcNo, string cartonNo, string ngFlag)
        {
            string sql = string.Format("SELECT NVL(SUM(NGQTY),0) NGQTY FROM TBLASNIQCDETAILEC WHERE IQCNO = '{0}' AND CARTONNO='{1}' AND NGFLAG='{2}'", iqcNo, cartonNo, ngFlag);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //根据条件获取送检单明细对应缺陷明细总行数
        /// <summary>
        /// 根据条件获取送检单明细对应缺陷明细总行数
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <param name="cartonNo">箱号</param>
        /// <param name="ngFlag">缺陷种类</param>
        /// <returns></returns>
        public int GetAsnIQCDetailEcCount(string iqcNo, string cartonNo, string ngFlag)
        {
            string sql = string.Format(@" SELECT COUNT(1) 
                                FROM TBLASNIQCDETAILEC A WHERE 1 = 1 AND A.IQCNO = '{0}' AND A.CARTONNO='{1}' AND A.NGFLAG='{2}'", iqcNo, cartonNo, ngFlag);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //根据条件获取送检单明细对应缺陷明细总行数
        /// <summary>
        /// 根据条件获取送检单明细对应缺陷明细总行数
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <param name="sn">SN</param>
        /// <param name="ngFlag">缺陷种类</param>
        /// <returns></returns>
        public int GetAsnIQCDetailECCount(string iqcNo, string sn, string ngFlag)
        {
            string sql = string.Format(@" SELECT COUNT(1) 
                                FROM TBLASNIQCDETAILEC A WHERE 1 = 1 AND A.IQCNO = '{0}' AND A.SN='{1}' AND A.NGFLAG='{2}'", iqcNo, sn, ngFlag);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        #endregion

        #region AsnIqcDetailSN add by sam 2016年2月25日
        /// <summary>
        /// TBLASNIQCDETAILSN
        /// </summary>
        public AsnIqcDetailSN CreateNewAsnIqcDetailSN()
        {
            return new AsnIqcDetailSN();
        }

        public void AddAsnIqcDetailSN(AsnIqcDetailSN asniqcdetailsn)
        {
            this.DataProvider.Insert(asniqcdetailsn);
        }

        public void DeleteAsnIqcDetailSN(AsnIqcDetailSN asniqcdetailsn)
        {
            this.DataProvider.Delete(asniqcdetailsn);
        }

        public void UpdateAsnIqcDetailSN(AsnIqcDetailSN asniqcdetailsn)
        {
            this.DataProvider.Update(asniqcdetailsn);
        }

        public object GetAsnIqcDetailSN(int Stline, string Iqcno, string Sn, string Stno)
        {
            return this.DataProvider.CustomSearch(typeof(AsnIqcDetailSN), new object[] { Stline, Iqcno, Sn, Stno });
        }

        //根据IQC送检单号获取送检单明细SN信息 add by jinger 20160226
        /// <summary>
        ///根据IQC送检单号获取送检单明细SN信息
        /// </summary>
        /// <param name="Iqcno">IQC送检单号</param>
        /// <returns></returns>
        public object[] GetAsnIqcDetailSNByIqcNo(string Iqcno)
        {
            return this.DataProvider.CustomQuery(typeof(AsnIqcDetailSN), new SQLCondition(string.Format(@"SELECT {0} FROM TBLASNIQCDETAILSN WHERE IQCNO='{1}' ORDER BY STLINE",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIqcDetailSN)), Iqcno)));
        }

        #endregion


        public int ReturnQtyTotalWithIQCNO(string IQCNO)
        {
            string sql = "SELECT nvl(sum(RETURNQTY),0) FROM TBLASNIQCDETAIL WHERE IQCNO='" + IQCNO + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        public int ReformQtyTotalWithIQCNO(string IQCNO)
        {
            string sql = "SELECT nvl(sum(REFORMQTY),0) FROM TBLASNIQCDETAIL WHERE IQCNO='" + IQCNO + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        public int GiveQtyTotalWithIQCNO(string IQCNO)
        {
            string sql = "SELECT nvl(sum(GIVEQTY),0) FROM TBLASNIQCDETAIL WHERE IQCNO='" + IQCNO + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        public int AcceptQtyTotalWithIQCNO(string IQCNO)
        {
            string sql = "SELECT nvl(sum(ACCEPTQTY),0) FROM TBLASNIQCDETAIL WHERE IQCNO='" + IQCNO + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        public int ReturnQtyTotalWithStNoLine(string sTNo, string stLine)
        {
            string sql = "SELECT nvl(sum(RETURNQTY),0) FROM TBLASNIQCDETAIL WHERE sTNo='" + sTNo + "' and stline ='" + stLine + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }


        public int ReformQtyTotalWithStNoLine(string sTNo, string stLine)
        {
            string sql = "SELECT nvl(sum(REFORMQTY),0) FROM TBLASNIQCDETAIL WHERE stno='" + sTNo + "' and stLine='" + stLine + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }


        public int ReturnQtyTotalWithStNo(string sTNo)
        {
            string sql = "SELECT nvl(sum(RETURNQTY),0) FROM TBLASNIQCDETAIL WHERE sTNo='" + sTNo + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }


        public int ReformQtyTotalWithStNo(string sTNo)
        {
            string sql = "SELECT nvl(sum(REFORMQTY),0) FROM TBLASNIQCDETAIL WHERE stno='" + sTNo + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }




        public bool BeIQCReject(string stno)
        {

            string sql = "select count(*) from tblasndetailitem where stno='" + stno + "' and qcpassqty>0";
            int total = this.DataProvider.GetCount(new SQLCondition(sql));
            return total <= 0;

        }

        public bool CanToOnlocationStaus(string stno)
        {
            List<AsnIQC> l = new List<AsnIQC>();
            string sqlTotal = "SELECT count(*) FROM TBLASNIQC WHERE STNO='" + stno + "' and status<>'Cancel'";
            int total = this.DataProvider.GetCount(new SQLCondition(sqlTotal));

            string iqcCloseTotalSql = "SELECT COUNT(*) FROM TBLASNIQC WHERE STNO='" + stno + "' AND (STATUS='" + ASNDetail_STATUS.ASNDetail_IQCClose + "' or STATUS='" + ASNDetail_STATUS.ASNDetail_IQCRejection + "')";
            int iqcCloseCount = this.DataProvider.GetCount(new SQLCondition(iqcCloseTotalSql));


            if (total == iqcCloseCount)
                return true;
            else if (total > iqcCloseCount)
                return false;

            else
                throw new Exception("IQC单总数为" + total + "已完成数为" + iqcCloseCount);
        }

        public void UpdateAsnDetailSn(List<string> cartonnos, string stno)
        {
            string sql = "update TBLASNDETAILSN set QCSTATUS='N' WHERE STNO='" + stno + "' and CARTONNO in(" + SqlFormat(cartonnos) + ")";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdateAsnDetailSnQcStatus(string stno)
        {
            string sql = "update TBLASNDETAILSN set QCSTATUS='Y' WHERE STNO='" + stno + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));

            sql = "update TBLASNDETAILSN set QCSTATUS='N' WHERE STNO='" + stno + "' AND SN IN(SELECT SN FROM TBLASNIQCDETAILEC WHERE STNO='" + stno + "')";
            this.DataProvider.CustomExecute(new SQLCondition(sql));

        }

        public bool HaveOneRetunOrReformEc(string iqcNo)
        {
            string sql = "select count(*) from TBLASNIQCDETAILEC where IQCNO='" + iqcNo + "' and SQESTATUS in ('Return','Reform')";
            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            return count > 0;

        }


        public AsnIQCDetailEc[] GetRetunOrReformEcs(string iqcNo)
        {
            string sql = "select * from TBLASNIQCDETAILEC  where IQCNO='" + iqcNo + "' and SQESTATUS in ('Return','Reform')";

            object[] objs = this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));

            List<AsnIQCDetailEc> ecs = new List<AsnIQCDetailEc>();
            if (objs != null && objs.Length > 0)
            {
                foreach (AsnIQCDetailEc ec in objs)
                {
                    ecs.Add(ec);
                }

            }
            return ecs.ToArray();

        }

        public AsnIqcDetailSN GetIQCSN(string iqcNo, string SN)
        {
            string sql = "select * from TBLASNIQCDETAILSN  where IQCNO='" + iqcNo + "' and SN='" + SN + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(AsnIqcDetailSN), new SQLCondition(sql));


            if (objs != null && objs.Length > 0)

                return (AsnIqcDetailSN)objs[0];
            return null;

        }


        public Asndetailsn GetASNDetailSN(string stno, string SN)
        {
            string sql = "select * from TBLASNDETAILSN  where stno='" + stno + "' and SN='" + SN + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(Asndetailsn), new SQLCondition(sql));


            if (objs != null && objs.Length > 0)

                return (Asndetailsn)objs[0];
            return null;

        }

        public AsnIQCDetail[] GetAsnIQCDetails(string stno, string iqcNo)
        {

            string sql = "SELECT * FROM TBLASNIQCDETAIL WHERE IQCNO='" + iqcNo + "' AND STNO='" + stno + "'";
            List<AsnIQCDetail> l = new List<AsnIQCDetail>();
            object[] os = this.DataProvider.CustomQuery(typeof(AsnIQCDetail), new SQLCondition(sql));
            if (os != null && os.Length > 0)
            {
                foreach (AsnIQCDetail o in os)
                    l.Add(o);
            }
            return l.ToArray();
        }

        public AsnIQCDetail[] GetAsnIQCDetails(string iqcNo)
        {

            string sql = "SELECT * FROM TBLASNIQCDETAIL WHERE IQCNO='" + iqcNo + "'";
            List<AsnIQCDetail> l = new List<AsnIQCDetail>();
            object[] os = this.DataProvider.CustomQuery(typeof(AsnIQCDetail), new SQLCondition(sql));
            if (os != null && os.Length > 0)
            {
                foreach (AsnIQCDetail o in os)
                    l.Add(o);
            }
            return l.ToArray();
        }


        public OQCDetail[] GetOQCDetails(string oqcNo)
        {

            string sql = "SELECT * FROM TBLOQCDETAIL WHERE OQCNO='" + oqcNo + "'";
            List<OQCDetail> l = new List<OQCDetail>();
            object[] os = this.DataProvider.CustomQuery(typeof(OQCDetail), new SQLCondition(sql));
            if (os != null && os.Length > 0)
            {
                foreach (OQCDetail o in os)
                    l.Add(o);
            }
            return l.ToArray();
        }

        private string SqlFormat(List<string> strs)
        {
            if (strs.Count == 0)
                return "''";
            System.Text.StringBuilder sb = new System.Text.StringBuilder(200);

            foreach (string str in strs)
            {
                sb.Append("'");
                sb.Append(str);
                sb.Append("',");

            }
            return sb.ToString().TrimEnd(',');
        }


        public AsnIQCDetailEc GetAsnIQCDetailEC(string stno, string iqcNo)
        {

            string sql = "SELECT * FROM TBLASNIQCDETAILEC WHERE IQCNO='" + iqcNo + "' AND STNO='" + stno + "' order by mdate,mtime desc";
            List<AsnIQCDetailEc> l = new List<AsnIQCDetailEc>();
            object[] os = this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
            if (os != null && os.Length > 0)
            {
                foreach (AsnIQCDetailEc o in os)
                    l.Add(o);
            }
            if (l.Count > 0)
                return l[0];
            else
                return null;
        }

        public AsnIQCDetailEc[] GetAsnIQCDetailECs(string iqcNo)
        {

            string sql = "SELECT * FROM TBLASNIQCDETAILEC WHERE IQCNO='" + iqcNo + "'";
            List<AsnIQCDetailEc> l = new List<AsnIQCDetailEc>();
            object[] os = this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
            if (os != null && os.Length > 0)
            {
                foreach (AsnIQCDetailEc o in os)
                    l.Add(o);
            }
            return l.ToArray();
        }

        public AsnIQCDetailEc[] GetAsnIQCDetailReturnOrReformECs(string iqcNo)
        {

            string sql = "SELECT * FROM TBLASNIQCDETAILEC WHERE (IQCNO='" + iqcNo + "' AND SQESTATUS='Reform') OR (IQCNO='" + iqcNo + "' AND SQESTATUS='Return')";
            List<AsnIQCDetailEc> l = new List<AsnIQCDetailEc>();
            object[] os = this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
            if (os != null && os.Length > 0)
            {
                foreach (AsnIQCDetailEc o in os)
                    l.Add(o);
            }
            return l.ToArray();
        }

        public OQCDetailEC[] GetOQCDetailECs(string oqcNo)
        {

            string sql = "SELECT * FROM TBLOQCDETAILEC WHERE OQCNO='" + oqcNo + "'";
            List<OQCDetailEC> l = new List<OQCDetailEC>();
            object[] os = this.DataProvider.CustomQuery(typeof(OQCDetailEC), new SQLCondition(sql));
            if (os != null && os.Length > 0)
            {
                foreach (OQCDetailEC o in os)
                    l.Add(o);
            }
            return l.ToArray();
        }

        public OQCDetailEC[] GetOQCDetailReturnECs(string oqcNo)
        {

            string sql = "SELECT * FROM TBLOQCDETAILEC WHERE OQCNO='" + oqcNo + "' AND SQESTATUS='Return'";
            List<OQCDetailEC> l = new List<OQCDetailEC>();
            object[] os = this.DataProvider.CustomQuery(typeof(OQCDetailEC), new SQLCondition(sql));
            if (os != null && os.Length > 0)
            {
                foreach (OQCDetailEC o in os)
                    l.Add(o);
            }
            return l.ToArray();
        }

        public int GetOQCDetailQty(string oqcNo)
        {

            string sql = "SELECT nvl(sum(qty),0) qty FROM TBLOQCDETAIL WHERE oqcNo='" + oqcNo + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        public int GetAsnIQCDetailQty(string iqcNo)
        {

            string sql = "SELECT nvl(sum(qty),0) qty FROM tblasniqcdetail WHERE IQCNO='" + iqcNo + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        public int GetNGTypesNum(string iqcNo,string cartonno)
        {

            string sql = "SELECT nvl(sum(qty),0) qty FROM tblasniqcdetail WHERE IQCNO='" + iqcNo + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }



    }

}
