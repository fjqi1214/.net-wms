using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.IQC
{
    //#region ASN

    ///// <summary>
    /////	ASN
    ///// </summary>
    //[Serializable, TableMap("TBLASN", "STNO")]
    //public class ASN : BenQGuru.eMES.Common.Domain.DomainObject
    //{
    //    public ASN()
    //    {
    //    }

    //    ///<summary>
    //    ///STNo
    //    ///</summary>	
    //    [FieldMapAttribute("STNO", typeof(string), 40, false)]
    //    public string STNo;

    //    ///<summary>
    //    ///OrganizationID
    //    ///</summary>	
    //    [FieldMapAttribute("ORGID", typeof(int), 8, false)]
    //    public int OrganizationID;

    //    ///<summary>
    //    ///VendorCode
    //    ///</summary>	
    //    [FieldMapAttribute("VENDORCODE", typeof(string), 40, false)]
    //    public string VendorCode;

    //    ///<summary>
    //    ///STStatus
    //    ///</summary>	
    //    [FieldMapAttribute("STSTATUS", typeof(string), 40, false)]
    //    public string STStatus;

    //    ///<summary>
    //    ///MaintainDate
    //    ///</summary>	
    //    [FieldMapAttribute("MDATE", typeof(int), 8, false)]
    //    public int MaintainDate;

    //    ///<summary>
    //    ///MaintainTime
    //    ///</summary>	
    //    [FieldMapAttribute("MTIME", typeof(int), 6, false)]
    //    public int MaintainTime;

    //    ///<summary>
    //    ///MaintainUser
    //    ///</summary>	
    //    [FieldMapAttribute("MUSER", typeof(string), 40, false)]
    //    public string MaintainUser;

    //    ///<summary>
    //    ///SyncStatus
    //    ///</summary>	
    //    [FieldMapAttribute("SYNCSTATUS", typeof(string), 40, true)]
    //    public string SyncStatus;

    //    ///<summary>
    //    ///Flag
    //    ///</summary>	
    //    [FieldMapAttribute("FLAG", typeof(string), 40, false)]
    //    public string Flag;

    //}

    //#endregion

    #region IQCHead

    /// <summary>
    ///	IQCHead
    /// </summary>
    [Serializable, TableMap("TBLASNIQC", "IQCNO")]
    public class IQCHead : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public IQCHead()
        {
        }

        ///<summary>
        ///IQCNo
        ///</summary>	
        [FieldMapAttribute("IQCNO", typeof(string), 50, false)]
        public string IQCNo;

        ///<summary>
        ///STNo
        ///</summary>	
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string STNo;

        ///<summary>
        ///STNoSeq
        ///</summary>	
        [FieldMapAttribute("STNO_SEQ", typeof(int), 8, false)]
        public int STNoSeq;

        ///<summary>
        ///Status
        ///</summary>	
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///InventoryUser
        ///</summary>	
        [FieldMapAttribute("INVUSER", typeof(string), 100, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string InventoryUser;

        ///<summary>
        ///Applicant
        ///</summary>	
        [FieldMapAttribute("APPLICANT", typeof(string), 100, true)]
        public string Applicant;

        ///<summary>
        ///AppDate
        ///</summary>	
        [FieldMapAttribute("APPDATE", typeof(int), 8, false)]
        public int AppDate;

        ///<summary>
        ///AppTime
        ///</summary>	
        [FieldMapAttribute("APPTIME", typeof(int), 6, false)]
        public int AppTime;

        ///<summary>
        ///LotNo
        ///</summary>	
        [FieldMapAttribute("LOTNO", typeof(string), 100, true)]
        public string LotNo;

        ///<summary>
        ///InspectDate
        ///</summary>	
        [FieldMapAttribute("INSPDATE", typeof(int), 8, true)]
        public int InspectDate;

        ///<summary>
        ///ProduceDate
        ///</summary>	
        [FieldMapAttribute("PRODDATE", typeof(int), 8, true)]
        public int ProduceDate;

        ///<summary>
        ///Standard
        ///</summary>	
        [FieldMapAttribute("STANDARD", typeof(string), 100, true)]
        public string Standard;

        ///<summary>
        ///Method
        ///</summary>	
        [FieldMapAttribute("METHOD", typeof(string), 100, true)]
        public string Method;

        ///<summary>
        ///Result
        ///</summary>	
        [FieldMapAttribute("RESULT", typeof(string), 100, true)]
        public string Result;

        ///<summary>
        ///ReceiveDate
        ///</summary>	
        [FieldMapAttribute("RECEIVEDATE", typeof(int), 8, true)]
        public int ReceiveDate;

        ///<summary>
        ///PIC
        ///</summary>	
        [FieldMapAttribute("PIC", typeof(string), 100, true)]
        public string PIC;

        ///<summary>
        ///Inspector
        ///</summary>	
        [FieldMapAttribute("INSPECTOR", typeof(string), 100, true)]
        public string Inspector;

        ///<summary>
        ///ROHS
        ///</summary>	
        [FieldMapAttribute("ROHS", typeof(string), 15, true)]
        public string ROHS;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///Attribute
        ///</summary>	
        [FieldMapAttribute("ATTRIBUTE", typeof(string), 40, true)]
        public string Attribute;

        ///<summary>
        ///STS
        ///</summary>	
        [FieldMapAttribute("STS", typeof(string), 15, true)]
        public string STS;

    }

    public class IQCHeadWithVendor : IQCHead
    {
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, false)]
        public string VendorCode;

        [FieldMapAttribute("VENDORNAME", typeof(string), 100, false)]
        public string VendorName;

        [FieldMapAttribute("APPLICANTUSERNAME", typeof(string), 40, false)]
        public string ApplicantUserName;

        [FieldMapAttribute("INSPECTORUSERNAME", typeof(string), 40, false)]
        public string InspectorUserName;

        [FieldMapAttribute("INVUSERNAME", typeof(string), 100, false)]
        public string InventoryUserName;

        [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
        public string StorageCode;

        [FieldMapAttribute("STORAGENAME", typeof(string), 100, true)]
        public string StorageName;

        ///<summary>
        ///IsAllInStorage
        ///</summary>
        [FieldMapAttribute("IsAllInStorage", typeof(string), 1, true)]
        public string IsAllInStorage;
    }

    #endregion

    #region IQCDetail

    /// <summary>
    ///	IQCDetail
    /// </summary>
    [Serializable, TableMap("TBLIQCDETAIL", "IQCNO, STLINE")]
    public class IQCDetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public IQCDetail()
        {
        }

        ///<summary>
        ///IQCNo
        ///</summary>	
        [FieldMapAttribute("IQCNO", typeof(string), 50, false)]
        public string IQCNo;

        ///<summary>
        ///STNo
        ///</summary>	
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string STNo;

        ///<summary>
        ///STLine
        ///</summary>	
        [FieldMapAttribute("STLINE", typeof(int), 6, false)]
        public int STLine;

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///OrderNo
        ///</summary>	
        [FieldMapAttribute("ORDERNO", typeof(string), 40, true)]
        public string OrderNo;

        ///<summary>
        ///OrderLine
        ///</summary>	
        [FieldMapAttribute("ORDERLINE", typeof(int), 22, true)]
        public int OrderLine;

        /////<summary>
        /////PlanDate
        /////</summary>	
        //[FieldMapAttribute("PLANDATE", typeof(int), 8, false)]
        //public int PlanDate;

        /////<summary>
        /////PlanQty
        /////</summary>	
        //[FieldMapAttribute("PLANQTY", typeof(decimal), 18, false)]
        //public decimal PlanQty;

        ///<summary>
        ///STDStatus
        ///</summary>	
        [FieldMapAttribute("STDSTATUS", typeof(string), 40, false)]
        public string STDStatus;

        ///<summary>
        ///ReceiveQty
        ///</summary>	
        [FieldMapAttribute("RECEIVEQTY", typeof(decimal), 18, true)]
        public decimal ReceiveQty;

        ///<summary>
        ///CheckStatus
        ///</summary>	
        [FieldMapAttribute("CHECKSTATUS", typeof(string), 40, true)]
        public string CheckStatus;

        ///<summary>
        ///Unit
        ///</summary>	
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///Memo
        ///</summary>	
        [FieldMapAttribute("MEMO", typeof(string), 2000, true)]
        public string Memo;

        ///<summary>
        ///MemoEx
        ///</summary>	
        [FieldMapAttribute("MEMOEX", typeof(string), 1000, true)]
        public string MemoEx;

        ///<summary>
        ///SampleQty
        ///</summary>	
        [FieldMapAttribute("SAMPLEQTY", typeof(decimal), 18, true)]
        public decimal SampleQty;

        ///<summary>
        ///NGQty
        ///</summary>	
        [FieldMapAttribute("NGQTY", typeof(decimal), 18, true)]
        public decimal NGQty;

        ///<summary>
        ///PIC
        ///</summary>	
        [FieldMapAttribute("PIC", typeof(string), 100, true)]
        public string PIC;

        ///<summary>
        ///Action
        ///</summary>	
        [FieldMapAttribute("ACTION", typeof(string), 1000, true)]
        public string Action;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///Attribute
        ///</summary>	
        [FieldMapAttribute("ATTRIBUTE", typeof(string), 40, true)]
        public string Attribute;

        ///<summary>
        ///Type
        ///</summary>	
        [FieldMapAttribute("TYPE", typeof(string), 40, true)]
        public string Type;

        ///<summary>
        ///OrganizationID
        ///</summary>	
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;

        ///<summary>
        ///StorageID
        ///</summary>	
        [FieldMapAttribute("STORAGEID", typeof(string), 4, false)]
        public string StorageID;

        ///<summary>
        ///ConcessionStatus
        ///</summary>	
        [FieldMapAttribute("CONCESSIONSTATUS", typeof(string), 15, false)]
        public string ConcessionStatus;

        ///<summary>
        ///ConcessionQty
        ///</summary>	
        [FieldMapAttribute("CONCESSIONQTY", typeof(decimal), 18, true)]
        public decimal ConcessionQty;

        ///<summary>
        ///ConcessionNo
        ///</summary>	
        [FieldMapAttribute("CONCESSIONNO", typeof(string), 100, true)]
        public string ConcessionNo;

        ///<summary>
        ///ConcessionMemo
        ///</summary>	
        [FieldMapAttribute("CONCESSIONMEMO", typeof(string), 1000, true)]
        public string ConcessionMemo;

        /////<summary>
        /////SRMFlag
        /////</summary>	
        //[FieldMapAttribute("SRMFLAG", typeof(string), 10, true)]
        //public string SRMFlag;

        ///<summary>
        ///SRMErrorMessage
        ///</summary>	
        [FieldMapAttribute("SRMERRORMSG", typeof(string), 2000, true)]
        public string SRMErrorMessage;

        //Added By Nettie Chen on 2009/09/22
        ///<summary>
        ///DInspector
        ///</summary>	
        [FieldMapAttribute("DINSPECTOR", typeof(string), 100, true)]
        public string DInspector;

        ///<summary>
        ///InspectDate
        ///</summary>	
        [FieldMapAttribute("DINSPDATE", typeof(int), 8, true)]
        public int DInspectDate;

        ///<summary>
        ///InspectTime
        ///</summary>	
        [FieldMapAttribute("DINSPTIME", typeof(int), 8, true)]
        public int DInspectTime;

        ///<summary>
        ///INSType
        ///</summary>	
        [FieldMapAttribute("INSTYPE", typeof(string), 40, true)]
        public string INSType;

        ///<summary>
        ///PurchaseMEMO
        ///</summary>	
        [FieldMapAttribute("PURCHASEMEMO", typeof(string), 2000, true)]
        public string PurchaseMEMO;

        //End Added
    }

    public class IQCDetailWithMaterial : IQCDetail
    {
        [FieldMapAttribute("MDESC", typeof(string), 100, true)]
        public string MaterialDescription;

        [FieldMapAttribute("INVRECEIPTDETAILMEMO", typeof(string), 2000, false)]
        public string InvreceiptDetailMemo;
        
        [FieldMapAttribute("VENDERLOTNO", typeof(string), 40, false)]
        public string VenderLotNo;

        ///<summary>
        ///IsAllInStorage
        ///</summary>
        [FieldMapAttribute("IsInStorage", typeof(string), 1, true)]
        public string IsInStorage;
    }

    public class IQCDetailForReceive : IQCDetail
    {
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, false)]
        public string VendorCode;

        [FieldMapAttribute("VENDORNAME", typeof(string), 100, false)]
        public string VendorName;

        [FieldMapAttribute("MDESC", typeof(string), 100, true)]
        public string MaterialDescription;

        [FieldMapAttribute("ROHS", typeof(string), 15, true)]
        public string ROHS;

        [FieldMapAttribute("STS", typeof(string), 15, true)]
        public string ShipToStock;

        [FieldMapAttribute("INSPECTOR", typeof(string), 100, true)]
        public string Inspector;

        [FieldMapAttribute("INSPDATE", typeof(int), 8, true)]
        public int InspectDate;

        [FieldMapAttribute("INVUSER", typeof(string), 100, false)]
        public string InventoryUser;

        [FieldMapAttribute("IQCHEADATTRIBUTE", typeof(string), 40, true)]
        public string IQCHeadAttribute;

        [FieldMapAttribute("INSPECTORUSERNAME", typeof(string), 40, false)]
        public string InspectorUserName;

        [FieldMapAttribute("INVUSERNAME", typeof(string), 100, false)]
        public string InventoryUserName;
    }

    #endregion

    #region MaterialReceive

    /// <summary>
    ///	MaterialReceive
    /// </summary>
    [Serializable, TableMap("TBLMATERIALRECEIVE", "IQCNO, STLINE")]
    public class MaterialReceive : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public MaterialReceive()
        {
        }

        ///<summary>
        ///IQCNo
        ///</summary>	
        [FieldMapAttribute("IQCNO", typeof(string), 50, false)]
        public string IQCNo;

        ///<summary>
        ///STNo
        ///</summary>	
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string STNo;

        ///<summary>
        ///STLine
        ///</summary>	
        [FieldMapAttribute("STLINE", typeof(int), 6, false)]
        public int STLine;

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///RealReceiveQty
        ///</summary>	
        [FieldMapAttribute("REALRECEIVEQTY", typeof(int), 13, true)]
        public int RealReceiveQty;

        ///<summary>
        ///ReceiveMemo
        ///</summary>	
        [FieldMapAttribute("RECEIVEMEMO", typeof(string), 1000, true)]
        public string ReceiveMemo;

        ///<summary>
        ///AccountDate
        ///</summary>	
        [FieldMapAttribute("ACCOUNTDATE", typeof(int), 8, false)]
        public int AccountDate;

        ///<summary>
        ///VoucherDate
        ///</summary>	
        [FieldMapAttribute("VOUCHERDATE", typeof(int), 8, false)]
        public int VoucherDate;

        ///<summary>
        ///OrderNo
        ///</summary>	
        [FieldMapAttribute("ORDERNO", typeof(string), 40, true)]
        public string OrderNo;

        ///<summary>
        ///OrderLine
        ///</summary>	
        [FieldMapAttribute("ORDERLINE", typeof(int), 22, true)]
        public int OrderLine;

        ///<summary>
        ///OrganizationID
        ///</summary>	
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;

        ///<summary>
        ///StorageID
        ///</summary>	
        [FieldMapAttribute("STORAGEID", typeof(string), 4, false)]
        public string StorageID;

        ///<summary>
        ///Unit
        ///</summary>	
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///Flag
        ///</summary>	
        [FieldMapAttribute("FLAG", typeof(string), 10, true)]
        public string Flag;

        ///<summary>
        ///TransactionCode
        ///</summary>	
        [FieldMapAttribute("TRANSACTIONCODE", typeof(string), 100, false)]
        public string TransactionCode;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }

    public class MaterialReceiveExtend : MaterialReceive
    {
        [FieldMapAttribute("MDESC", typeof(string), 200, false)]
        public string MaterialDescription;

        [FieldMapAttribute("ERRORMESSAGE", typeof(string), 2000, false)]
        public string ErrorMessage;

        [FieldMapAttribute("SRMFLAG", typeof(string), 10, true)]
        public string SRMFlag;

        [FieldMapAttribute("SRMERRORMSG", typeof(string), 2000, true)]
        public string SRMErrorMessage;
    }

    #endregion

    #region MaterialStorageInfo

    /// <summary>
    ///	MaterialStorageInfo
    /// </summary>
    [Serializable, TableMap("TBLMATERIALSTORAGEINFO", "ITEMCODE, ORGID, STORAGEID")]
    public class MaterialStorageInfo : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public MaterialStorageInfo()
        {
        }

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///OrganizationID
        ///</summary>	
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;

        ///<summary>
        ///StorageID
        ///</summary>	
        [FieldMapAttribute("STORAGEID", typeof(string), 4, false)]
        public string StorageID;

        ///<summary>
        ///CLABSQty
        ///</summary>	
        [FieldMapAttribute("CLABSQTY", typeof(int), 13, false)]
        public int CLABSQty;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }

    #endregion

    #region ShipToStock

    /// <summary>
    ///	ShipToStock
    /// </summary>
    [Serializable, TableMap("TBLSHIPTOSTOCK", "")]
    public class ShipToStock : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public ShipToStock()
        {
        }

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string MaterialCode;

        ///<summary>
        ///OrganizationID
        ///</summary>	
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;

        ///<summary>
        ///VendorCode
        ///</summary>	
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, false)]
        public string VendorCode;

        ///<summary>
        ///EffectDate
        ///</summary>	
        [FieldMapAttribute("EFFDATE", typeof(int), 8, false)]
        public int EffectDate;

        ///<summary>
        ///InvalidDate
        ///</summary>	
        [FieldMapAttribute("IVLDATE", typeof(int), 8, false)]
        public int InvalidDate;

        ///<summary>
        ///Active
        ///</summary>	
        [FieldMapAttribute("ACTIVE", typeof(string), 2, false)]
        public string Active;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

    }

    public class ShipToStockEx : ShipToStock
    {
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MaterialDesc;

        [FieldMapAttribute("ORGDESC", typeof(string), 40, false)]
        public string OrganizationDesc;

        [FieldMapAttribute("VENDORNAME", typeof(string), 100, false)]
        public string VendorName;
    }

    #endregion

    #region SRM ASN/PO

    public class SRMASNDetail : IQCDetail
    {
        ///<summary>
        ///SecondClass
        ///</summary>	
        [FieldMapAttribute("SECONDCLASS", typeof(string), 40, true)]
        public string SecondClass;

        ///<summary>
        ///ShipToStock
        ///</summary>	
        [FieldMapAttribute("SHIPTOSTOCK", typeof(string), 40, true)]
        public string ShipToStock;

        ///<summary>
        ///ROHS
        ///</summary>	
        [FieldMapAttribute("ROHS", typeof(string), 40, true)]
        public string ROHS;

        ///<summary>
        ///VendorCode
        ///</summary>	
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, false)]
        public string VendorCode;

        ///<summary>
        ///STStatus
        ///</summary>	
        [FieldMapAttribute("STSTATUS", typeof(string), 40, false)]
        public string STStatus;

        ///<summary>
        ///InventoryUser
        ///</summary>	
        [FieldMapAttribute("INVUSER", typeof(string), 100, false)]
        public string InventoryUser;

        ///<summary>
        ///SSQty
        ///</summary>	
        [FieldMapAttribute("SSQTY", typeof(decimal), 18, false)]
        public decimal SSQty;

        ///<summary>
        ///RealReceiveQTY
        ///</summary>	
        [FieldMapAttribute("REALRECEIVEQTY", typeof(decimal), 18, false)]
        public decimal RealReceiveQTY;

        ///<summary>
        ///RealPlanQTY
        ///</summary>	
        [FieldMapAttribute("REALPLANQTY", typeof(decimal), 18, false)]
        public decimal RealPlanQTY;

        ///<summary>
        ///SSDate
        ///</summary>	
        [FieldMapAttribute("SSDATE", typeof(int), 8, false)]
        public int SSDate;
    }

    #endregion

    public class IQCHeadWithInspectorName : IQCHead
    {
        [FieldMapAttribute("USERNAME", typeof(string), 40, true)]
        public string UserName;
    }

    #region INVRECEIPT
    /// <summary>
    /// TBLINVRECEIPT
    /// </summary>
    [Serializable, TableMap("TBLINVRECEIPT", "RECEIPTNO")]
    public class InvReceipt : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public InvReceipt()
        {
        }

        ///<summary>
        ///RECEIPTNO
        ///</summary>
        [FieldMapAttribute("RECEIPTNO", typeof(string), 40, false)]
        public string Receiptno;

        ///<summary>
        ///STORAGEID
        ///</summary>
        [FieldMapAttribute("STORAGEID", typeof(string), 40, false)]
        public string Storageid;

        ///<summary>
        ///RECSTATUS
        ///</summary>
        [FieldMapAttribute("RECSTATUS", typeof(string), 40, false)]
        public string Recstatus;

        ///<summary>
        ///VENDORCODE
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
        public string Vendorcode;

        ///<summary>
        ///RECTYPE
        ///</summary>
        [FieldMapAttribute("RECTYPE", typeof(string), 40, false)]
        public string Rectype;

        ///<summary>
        ///MEMO
        ///</summary>
        [FieldMapAttribute("MEMO", typeof(string), 2000, true)]
        public string Memo;

        ///<summary>
        ///CREATEDATE
        ///</summary>
        [FieldMapAttribute("CREATEDATE", typeof(int), 22, true)]
        public int Createdate;

        ///<summary>
        ///CREATETIME
        ///</summary>
        [FieldMapAttribute("CREATETIME", typeof(int), 22, true)]
        public int Createtime;

        ///<summary>
        ///CREATEUSER
        ///</summary>
        [FieldMapAttribute("CREATEUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string Createuser;

        ///<summary>
        ///ORGID
        ///</summary>
        [FieldMapAttribute("ORGID", typeof(int), 22, false)]
        public int Orgid;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string Muser;

        ///<summary>
        ///IsAllInStorage
        ///</summary>
        [FieldMapAttribute("IsAllInStorage", typeof(string), 1, true)]
        public string IsAllInStorage;

    }
    #endregion

    #region INVRECEIPTDETAIL
    /// <summary>
    /// TBLINVRECEIPTDETAIL
    /// </summary>
    [Serializable, TableMap("TBLINVRECEIPTDETAIL", "RECEIPTNO,RECEIPTLINE")]
    public class InvReceiptDetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public InvReceiptDetail()
        {
        }

        ///<summary>
        ///RECEIPTNO
        ///</summary>
        [FieldMapAttribute("RECEIPTNO", typeof(string), 40, false)]
        public string Receiptno;

        ///<summary>
        ///RECEIPTLINE
        ///</summary>
        [FieldMapAttribute("RECEIPTLINE", typeof(int), 22, false)]
        public int Receiptline;

        ///<summary>
        ///ORDERNO
        ///</summary>
        [FieldMapAttribute("ORDERNO", typeof(string), 40, true)]
        public string Orderno;

        ///<summary>
        ///ORDERLINE
        ///</summary>
        [FieldMapAttribute("ORDERLINE", typeof(int), 22, true)]
        public int Orderline;


        ///<summary>
        ///VENDERLOTNO
        ///</summary>
        [FieldMapAttribute("VENDERLOTNO", typeof(string), 40, true)]
        public string VenderLotNO;

        ///<summary>
        ///RECSTATUS
        ///</summary>
        [FieldMapAttribute("RECSTATUS", typeof(string), 40, false)]
        public string Recstatus;

        ///<summary>
        ///IQCSTATUS
        ///</summary>
        [FieldMapAttribute("IQCSTATUS", typeof(string), 40, false)]
        public string Iqcstatus;

        ///<summary>
        ///MEMO
        ///</summary>
        [FieldMapAttribute("MEMO", typeof(string), 2000, true)]
        public string Memo;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string Itemcode;

        ///<summary>
        ///PLANQTY
        ///</summary>
        [FieldMapAttribute("PLANQTY", typeof(decimal), 28, false)]
        public decimal Planqty;

        ///<summary>
        ///QUALIFYQTY
        ///</summary>
        [FieldMapAttribute("QUALIFYQTY", typeof(decimal), 28, true)]
        public decimal Qualifyqty;

        ///<summary>
        ///ACTQTY
        ///</summary>
        [FieldMapAttribute("ACTQTY", typeof(decimal), 28, true)]
        public decimal Actqty;

        ///<summary>
        ///RECDATE
        ///</summary>
        [FieldMapAttribute("RECDATE", typeof(int), 22, true)]
        public int Recdate;

        ///<summary>
        ///RECTIME
        ///</summary>
        [FieldMapAttribute("RECTIME", typeof(int), 22, true)]
        public int Rectime;

        ///<summary>
        ///RECUSER
        ///</summary>
        [FieldMapAttribute("RECUSER", typeof(string), 40, true)]
        public string Recuser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string Muser;

        ///<summary>
        ///IsAllInStorage
        ///</summary>
        [FieldMapAttribute("IsInStorage", typeof(string), 1, true)]
        public string IsInStorage;

        ///<summary>
        ///InvUser
        ///</summary>
        [FieldMapAttribute("INVUSER", typeof(string), 100, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string InvUser;

    }
    #endregion

    #region INVRECEIPTFORQUERY
    public class InvReceiptForQuery : InvReceipt
    {
        public InvReceiptForQuery()
        {
        }

        [FieldMapAttribute("VENDORNAME", typeof(string), 100, false)]
        public string VendorName;
        [FieldMapAttribute("STORAGENAME", typeof(string), 100, true)]
        public string StorageName;
    }
    #endregion

    #region InvReceiptDetailForQuery
    public class InvReceiptDetailForQuery : InvReceiptDetail
    {
        public InvReceiptDetailForQuery()
        {
        }
        ///<summary>
        ///Material Code
        ///</summary>	
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MaterialCode;
        ///<summary>
        ///Material Description
        ///</summary>	
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MaterialDescription;
        ///<summary>
        ///MaterialMachineType
        ///</summary>	
        [FieldMapAttribute("MMACHINETYPE", typeof(string), 100, true)]
        public string MaterialMachineType;
    }
    #endregion 

    #region INVReceipt

    public class INVReceipt : InvReceiptDetail
    {
        [FieldMapAttribute("STLINE", typeof(int), 22, true)]
        public string StLine;

        [FieldMapAttribute("ATTRIBUTE", typeof(string), 40, true)]
        public string Attribute;

        [FieldMapAttribute("CHECKSTATUS", typeof(string), 40, true)]
        public string CheckStatus;

        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string Mdesc;

        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        [FieldMapAttribute("RECEIVEQTY", typeof(int), 22, true)]
        public string ReceiveQty;

        [FieldMapAttribute("MSTORAGE", typeof(string), 40, true)]
        public string Mstorage;

        [FieldMapAttribute("MSTACK", typeof(string), 40, true)]
        public string Mstack;

        [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
        public string VendorCode;

        [FieldMapAttribute("VENDORNAME", typeof(string), 100, true)]
        public string VendorName;

        [FieldMapAttribute("ROHS", typeof(string), 15, true)]
        public string Rohs;

        [FieldMapAttribute("MCONTROLTYPE", typeof(string), 40, true)]
        public string McontrolType;

        [FieldMapAttribute("RECTYPE", typeof(string), 40, true)]
        public string Rectype;

        [FieldMapAttribute("MHUMIDITYLEVEL", typeof(string), 40, true)]
        public string MHumidityLevel;
    }
    #endregion

    #region  IQCTestData
    /// <summary>
    /// </summary>
    [Serializable, TableMap("TBLIQCTestData", "SERIAL")]
    public class IQCTestData : DomainObject
    {
        public IQCTestData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("IQCNO", typeof(string), 50, false)]
        public string IQCNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string STNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("STLINE", typeof(int), 22, false)]
        public int STLine;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CKGROUP", typeof(string), 400, true)]
        public string CheckGroup;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CKITEMCODE", typeof(string), 400, false)]
        public string CKItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("USL", typeof(float), 22, true)]
        public float USL;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LSL", typeof(float), 22, true)]
        public float LSL;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TESTINGVALUE", typeof(string), 200, true)]
        public string TestingValue;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TESTINGRESULT", typeof(string), 40, true)]
        public string TestingResult;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TESTINGDATE", typeof(int), 8, false)]
        public int TestingDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TESTINGTIME", typeof(int), 6, false)]
        public int TestingTime;

    }
    #endregion

    #region AsnIQC-- 送检单  add by jinger 2016-02-18
    /// <summary>
    /// TBLASNIQC-- 送检单 
    /// </summary>
    [Serializable, TableMap("TBLASNIQC", "IQCNO")]
    public class AsnIQC : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public AsnIQC()
        {
        }

        ///<summary>
        ///送检单号
        ///</summary>
        [FieldMapAttribute("IQCNO", typeof(string), 50, false)]
        public string IqcNo;

        ///<summary>
        ///检验方式
        ///</summary>
        [FieldMapAttribute("IQCTYPE", typeof(string), 40, true)]
        public string IqcType;

        ///<summary>
        ///ASN单号
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string StNo;

        ///<summary>
        ///SAP单据号
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, false)]
        public string InvNo;

        ///<summary>
        ///ASN单号类型
        ///</summary>
        [FieldMapAttribute("STTYPE", typeof(string), 40, false)]
        public string StType;

        ///<summary>
        ///单据状态:Release:初始化；WaitCheck:待检验；SQEJudge:SQE判定；IQCClose:IQC检验完成；Cancel:取消
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///送检日期
        ///</summary>
        [FieldMapAttribute("APPDATE", typeof(int), 22, false)]
        public int AppDate;

        ///<summary>
        ///送检时间
        ///</summary>
        [FieldMapAttribute("APPTIME", typeof(int), 22, false)]
        public int AppTime;

        ///<summary>
        ///检验完成日期
        ///</summary>
        [FieldMapAttribute("INSPDATE", typeof(int), 22, true)]
        public int InspDate;

        ///<summary>
        ///检验完成时间
        ///</summary>
        [FieldMapAttribute("INSPTIME", typeof(int), 22, true)]
        public int InspTime;

        ///<summary>
        ///华为物料号
        ///</summary>
        [FieldMapAttribute("CUSTMCODE", typeof(string), 40, true)]
        public string CustmCode;

        ///<summary>
        ///SAP物料号
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///鼎桥物料号
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DQMCode;

        ///<summary>
        ///物料描述
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MDesc;

        ///<summary>
        ///送检数量
        ///</summary>
        [FieldMapAttribute("QTY", typeof(int), 22, false)]
        public int Qty;

        ///<summary>
        ///IQC状态(Y:合格；N:不合格)
        ///</summary>
        [FieldMapAttribute("QCSTATUS", typeof(string), 40, true)]
        public string QcStatus;

        ///<summary>
        ///供应商代码
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
        public string VendorCode;

        ///<summary>
        ///供应商物料编码
        ///</summary>
        [FieldMapAttribute("VENDORMCODE", typeof(string), 35, true)]
        public string VendorMCode;

        ///<summary>
        ///备注
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

    }

    /// <summary>
    /// AsnIQCExc-- 送检单扩展实体
    /// </summary>
    [Serializable]
    public class AsnIQCExt : AsnIQC
    {
        public AsnIQCExt()
        {
        }

        ///<summary>
        ///送检数量
        ///</summary>
        [FieldMapAttribute("APPQTY", typeof(int), 22, false)]
        public int AppQty;

        ///<summary>
        ///缺陷品数
        ///</summary>
        [FieldMapAttribute("NGQTY", typeof(int), 22, false)]
        public int NgQty;

    }


    #endregion

    #region AsnIQCDetail-- 送检单明细  add by jinger 2016-02-18
    /// <summary>
    /// TBLASNIQCDETAIL-- 送检单明细 
    /// </summary>
    [Serializable, TableMap("TBLASNIQCDETAIL", "STLINE,IQCNO,STNO")]
    public class AsnIQCDetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public AsnIQCDetail()
        {
        }

        ///<summary>
        ///送检单号
        ///</summary>
        [FieldMapAttribute("IQCNO", typeof(string), 50, false)]
        public string IqcNo;

        ///<summary>
        ///ASN单号
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string StNo;

        ///<summary>
        ///ASN单行项目
        ///</summary>
        [FieldMapAttribute("STLINE", typeof(string), 40, false)]
        public string StLine;

        ///<summary>
        ///箱号条码
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string CartonNo;

        ///<summary>
        ///送检数量
        ///</summary>
        [FieldMapAttribute("QTY", typeof(int), 22, false)]
        public int Qty;

        ///<summary>
        ///检验通过数量
        ///</summary>
        [FieldMapAttribute("QCPASSQTY", typeof(int), 22, true)]
        public int QcPassQty;

        ///<summary>
        ///单位
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///缺陷品数
        ///</summary>
        [FieldMapAttribute("NGQTY", typeof(int), 22, true)]
        public int NgQty;

        ///<summary>
        ///退换货数量
        ///</summary>
        [FieldMapAttribute("RETURNQTY", typeof(int), 22, true)]
        public int ReturnQty;

        ///<summary>
        ///现场整改数量
        ///</summary>
        [FieldMapAttribute("REFORMQTY", typeof(int), 22, true)]
        public int ReformQty;

        ///<summary>
        ///让步接收数量
        ///</summary>
        [FieldMapAttribute("GIVEQTY", typeof(int), 22, true)]
        public int GiveQty;

        ///<summary>
        ///特采数量
        ///</summary>
        [FieldMapAttribute("ACCEPTQTY", typeof(int), 22, true)]
        public int AcceptQty;

        ///<summary>
        ///IQC状态(Y:合格；N:不合格)
        ///</summary>
        [FieldMapAttribute("QCSTATUS", typeof(string), 40, true)]
        public string QcStatus;

        ///<summary>
        ///备注
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

    }


    /// <summary>
    /// AsnIQCDetailExt--送检单明细扩展实体
    /// </summary>
    [Serializable]
    public class AsnIQCDetailExt : AsnIQCDetail
    {
        public AsnIQCDetailExt()
        {
        }

        ///<summary>
        ///鼎桥物料号
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DQMCode;

        ///<summary>
        ///供应商物料编码
        ///</summary>
        [FieldMapAttribute("VENDORMCODE", typeof(string), 35, true)]
        public string VendorMCode;

        ///<summary>
        ///单据状态:Release:初始化；WaitCheck:待检验；SQEJudge:SQE判定；IQCClose:IQC检验完成；Cancel:取消
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///检验方式
        ///</summary>
        [FieldMapAttribute("IQCTYPE", typeof(string), 40, true)]
        public string IqcType;

        ///<summary>
        ///控管类型
        ///</summary>
        [FieldMapAttribute("MCONTROLTYPE", typeof(string), 40, true)]
        public string MControlType;
    }
    #endregion

    #region Asniqcdetailec-- 送检单明细对应缺陷明细表  add by jinger 2016-02-19
    /// <summary>
    /// TBLASNIQCDETAILEC-- 送检单明细对应缺陷明细表 
    /// </summary>
    [Serializable, TableMap("TBLASNIQCDETAILEC", "ECODE,STLINE,IQCNO,SN,STNO")]
    public class AsnIQCDetailEc : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public AsnIQCDetailEc()
        {
        }

        ///<summary>
        ///送检单号
        ///</summary>
        [FieldMapAttribute("IQCNO", typeof(string), 50, false)]
        public string IqcNo;

        ///<summary>
        ///ASN单号
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string StNo;

        ///<summary>
        ///ASN单行项目
        ///</summary>
        [FieldMapAttribute("STLINE", typeof(string), 40, false)]
        public string StLine;

        ///<summary>
        ///箱号条码
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string CartonNo;

        ///<summary>
        ///缺陷类型
        ///</summary>
        [FieldMapAttribute("ECGCODE", typeof(string), 40, false)]
        public string EcgCode;

        ///<summary>
        ///缺陷代码
        ///</summary>
        [FieldMapAttribute("ECODE", typeof(string), 40, false)]
        public string ECode;

        ///<summary>
        ///缺陷品数
        ///</summary>
        [FieldMapAttribute("NGQTY", typeof(int), 22, true)]
        public int NgQty;

        ///<summary>
        ///SN条码
        ///</summary>
        [FieldMapAttribute("SN", typeof(string), 40, false)]
        public string SN;

        ///<summary>
        ///SQE判定状态(Return:退换货、Reform:现场整改、Give:让步接收、Accept:特采)
        ///</summary>
        [FieldMapAttribute("SQESTATUS", typeof(string), 40, true)]
        public string SqeStatus;

        ///<summary>
        ///备注
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;

        ///<summary>
        ///缺陷种类(Y:整箱缺陷；N:非整箱缺陷)
        ///</summary>
        [FieldMapAttribute("NGFLAG", typeof(string), 10, true)]
        public string NgFlag;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

    }
    #endregion


    #region AsniqcDetailSn add by sam 2016年2月25日
    /// <summary>
    /// TBLASNIQCDETAILSN
    /// </summary>
    [Serializable, TableMap("TBLASNIQCDETAILSN", "STLINE,IQCNO,SN,STNO")]
    public class AsnIqcDetailSN : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public AsnIqcDetailSN()
        {
        }

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///Iqcno
        ///</summary>
        [FieldMapAttribute("IQCNO", typeof(string), 50, false)]
        public string IqcNo;

        ///<summary>
        ///Stno
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string StNo;

        ///<summary>
        ///Stline
        ///</summary>
        [FieldMapAttribute("STLINE", typeof(string), 40, false)]
        public string StLine;

        ///<summary>
        ///Cartonno
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, true)]
        public string CartonNo;

        ///<summary>
        ///Sn
        ///</summary>
        [FieldMapAttribute("SN", typeof(string), 40, false)]
        public string Sn;

        ///<summary>
        ///Qcstatus
        ///</summary>
        [FieldMapAttribute("QCSTATUS", typeof(string), 40, true)]
        public string QcStatus;

        ///<summary>
        ///Remark1
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;

        ///<summary>
        ///Cuser
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///Cdate
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///Ctime
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

    }
    #endregion



}
