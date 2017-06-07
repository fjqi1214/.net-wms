using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for TS
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
/// ** 日 期:		2005-05-31 15:31:31
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.TS
{

    #region TS
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLTS", "TSID")]
    public class TS : DomainObject
    {
        public TS()
        {
        }

        /// <summary>
        /// ManufactoryID
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FRMROUTECODE", typeof(string), 40, false)]
        public string FromRouteCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FRMSEGCODE", typeof(string), 40, false)]
        public string FromSegmentCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FRMSSCODE", typeof(string), 40, false)]
        public string FromStepSequenceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FRMOPCODE", typeof(string), 40, false)]
        public string FromOPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FRMRESCODE", typeof(string), 40, false)]
        public string FromResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSRESCODE", typeof(string), 40, false)]
        public string TSResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TCARD", typeof(string), 40, false)]
        public string TranslateCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SCARD", typeof(string), 40, false)]
        public string SourceCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSTIMES", typeof(decimal), 10, true)]
        public decimal TSTimes;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSID", typeof(string), 40, true)]
        public string TSId;

        /// <summary>
        /// 维修状态
        /// New/Confirm/TS/Split/Scrap/Reflow/Complete
        /// </summary>
        [FieldMapAttribute("TSSTATUS", typeof(string), 40, true)]
        public string TSStatus;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSDATE", typeof(int), 8, true)]
        public int TSDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSUSER", typeof(string), 40, true)]
        public string TSUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSTIME", typeof(int), 6, true)]
        public int TSTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CONFIRMUSER", typeof(string), 40, false)]
        public string ConfirmUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CONFIRMDATE", typeof(int), 8, false)]
        public int ConfirmDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CONFIRMTIME", typeof(int), 6, false)]
        public int ConfirmTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSMEMO", typeof(string), 100, false)]
        public string TSMEMO;

        /// <summary>
        /// 维修来源 OnWIP, TS
        /// </summary>
        [FieldMapAttribute("FRMINPUTTYPE", typeof(string), 40, true)]
        public string FromInputType;

        /// <summary>
        /// 成品还是物料
        /// </summary>
        [FieldMapAttribute("CARDTYPE", typeof(string), 40, true)]
        public string CardType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RRCARD", typeof(string), 40, false)]
        public string ReplacedRunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CRESCODE", typeof(string), 40, true)]
        public string ConfirmResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("COPCODE", typeof(string), 40, true)]
        public string ConfirmOPCode;

        /// <summary>
        /// ?
        /// </summary>
        [FieldMapAttribute("TRANSSTATUS", typeof(string), 40, true)]
        public string TransactionStatus;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, true)]
        public int FromShiftDay;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REFMOCODE", typeof(string), 40, false)]
        public string ReflowMOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REFROUTECODE", typeof(string), 40, false)]
        public string ReflowRouteCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REFOPCODE", typeof(string), 40, false)]
        public string ReflowOPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REFRESCODE", typeof(string), 40, false)]
        public string ReflowResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TFFULLPATH", typeof(string), 100, false)]
        public string TestFileFullPath;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TCARDSEQ", typeof(decimal), 10, true)]
        public decimal TranslateCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SCARDSEQ", typeof(decimal), 10, true)]
        public decimal SourceCardSequence;

        /// <summary>
        /// 时间段代码
        /// </summary>
        [FieldMapAttribute("TPCODE", typeof(string), 40, true)]
        public string FromTimePeriodCode;

        /// <summary>
        /// 班次代码
        /// </summary>
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        public string FromShiftCode;

        /// <summary>
        /// 班制代码
        /// </summary>
        [FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, true)]
        public string FromShiftTypeCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FRMUSER", typeof(string), 40, true)]
        public string FromUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FRMMEMO", typeof(string), 100, false)]
        public string FromMemo;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FRMDATE", typeof(int), 8, true)]
        public int FromDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FRMTIME", typeof(int), 6, true)]
        public int FormTime;

        /// <summary>
        /// 报废原因
        /// </summary>
        [FieldMapAttribute("SCRAPCAUSE", typeof(string), 200, true)]
        public string ScrapCause;

        /// <summary>
        /// 日期的月
        /// </summary>
        [FieldMapAttribute("FRMMonth", typeof(decimal), 10, true)]
        public decimal Month;

        /// <summary>
        /// 日期的周
        /// </summary>
        [FieldMapAttribute("FRMWEEK", typeof(decimal), 10, true)]
        public decimal Week;

        /// <summary>
        /// 正常的途程和线外的From途程
        /// </summary>
        [FieldMapAttribute("FRMOUTROUTECODE", typeof(string), 10, true)]
        public string FromOutLineRouteCode;

        /// <summary>
        /// RMA单号
        /// </summary>
        [FieldMapAttribute("RMABILLCODE", typeof(string), 40, false)]
        public string RMABillCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;

        /// <summary>
        /// 维修类型
        /// ts_normal(null)/ts_misjudge
        /// </summary>
        [FieldMapAttribute("TSTYPE", typeof(string), 40, true)]
        public string TSType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSREPAIRMDATE", typeof(int), 8, false)]
        public int TSRepairDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSREPAIRUSER", typeof(string), 40, false)]
        public string TSRepairUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSREPAIRMTIME", typeof(int), 6, false)]
        public int TSRepairTime;


    }
    #endregion

    #region TSErrorCause
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLTSERRORCAUSE", "ECSCODE,ECSGCODE,ECODE,ECGCODE,TSID")]
    public class TSErrorCause : DomainObject
    {
        public TSErrorCause()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SOLMEMO", typeof(string), 100, false)]
        public string SolutionMEMO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RRESCODE", typeof(string), 40, true)]
        public string RepairResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECSCODE", typeof(string), 40, true)]
        public string ErrorCauseCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECSGCODE", typeof(string), 40, true)]
        public string ErrorCauseGroupCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SOLCODE", typeof(string), 40, false)]
        public string SolutionCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DUTYCODE", typeof(string), 40, true)]
        public string DutyCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECODE", typeof(string), 40, true)]
        public string ErrorCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ROPCODE", typeof(string), 40, true)]
        public string RepairOPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECGCODE", typeof(string), 40, true)]
        public string ErrorCodeGroup;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSID", typeof(string), 40, true)]
        public string TSId;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, true)]
        public int TSShiftDay;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;

    }
    #endregion

    #region TSErrorCause2ErrorPart
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLTSERRORCAUSE2EPART", "EPART,ECODE,ECSCODE,ECSGCODE,ECGCODE,TSID")]
    public class TSErrorCause2ErrorPart : DomainObject
    {
        public TSErrorCause2ErrorPart()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, false)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EPART", typeof(string), 40, true)]
        public string ErrorPart;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECODE", typeof(string), 40, true)]
        public string ErrorCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECSCODE", typeof(string), 40, true)]
        public string ErrorCauseCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECSGCODE", typeof(string), 40, true)]
        public string ErrorCauseGroupCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECGCODE", typeof(string), 40, true)]
        public string ErrorCodeGroup;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RRESCODE", typeof(string), 40, true)]
        public string RepairResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ROPCODE", typeof(string), 40, true)]
        public string RepairOPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSID", typeof(string), 40, true)]
        public string TSId;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;
    }
    #endregion

    #region TSErrorCause2Location
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLTSERRORCAUSE2LOC", "ECSCODE,ECSGCODE,ECODE,ECGCODE,ELOC,AB,TSID")]
    public class TSErrorCause2Location : DomainObject
    {
        public TSErrorCause2Location()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, false)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECSCODE", typeof(string), 40, true)]
        public string ErrorCauseCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECSGCODE", typeof(string), 40, true)]
        public string ErrorCauseGroupCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECODE", typeof(string), 40, true)]
        public string ErrorCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECGCODE", typeof(string), 40, true)]
        public string ErrorCodeGroup;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RRESCODE", typeof(string), 40, true)]
        public string RepairResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ROPCODE", typeof(string), 40, true)]
        public string RepairOPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ELOC", typeof(string), 40, true)]
        public string ErrorLocation;

        /// <summary>
        /// A
        /// B
        /// AB
        /// </summary>
        [FieldMapAttribute("AB", typeof(string), 40, true)]
        public string AB;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SUBELOC", typeof(string), 40, true)]
        public string SubErrorLocation;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSID", typeof(string), 40, true)]
        public string TSId;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EPART", typeof(string), 40, true)]
        public string ErrorPart;
    }
    #endregion

    #region TSErrorCode
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLTSERRORCODE", "ECGCODE,ECODE,TSID")]
    public class TSErrorCode : DomainObject
    {
        public TSErrorCode()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECGCODE", typeof(string), 40, true)]
        public string ErrorCodeGroup;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECODE", typeof(string), 40, true)]
        public string ErrorCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, false)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSID", typeof(string), 40, true)]
        public string TSId;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;

        /// <summary>
        /// 不良数量
        /// </summary>
        [FieldMapAttribute("ERRORQTY", typeof(decimal), 10, true)]
        public decimal ErrorQty;
    }
    #endregion

    #region TSErrorCode2Location
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLTSERRORCODE2LOC", "ECODE,ELOC,ECGCODE,AB,TSID")]
    public class TSErrorCode2Location : DomainObject
    {
        public TSErrorCode2Location()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, true)]
        public int ShiftDay = 0;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;


        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, false)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECODE", typeof(string), 40, true)]
        public string ErrorCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ELOC", typeof(string), 40, true)]
        public string ErrorLocation;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECGCODE", typeof(string), 40, true)]
        public string ErrorCodeGroup;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SUBELOC", typeof(string), 40, true)]
        public string SubErrorLocation;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MEMO", typeof(string), 100, false)]
        public string MEMO;

        /// <summary>
        /// A
        /// B
        /// AB
        /// </summary>
        [FieldMapAttribute("AB", typeof(string), 40, true)]
        public string AB;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSID", typeof(string), 40, true)]
        public string TSId;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;
    }
    #endregion

    #region TSItem
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLTSITEM", "ITEMSEQ,TSID")]
    public class TSItem : DomainObject
    {
        public TSItem()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MEMO", typeof(string), 100, false)]
        public string MEMO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, false)]
        public string VendorCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("VENDORITEMCODE", typeof(string), 40, false)]
        public string VendorItemCode;

        /// <summary>
        /// 规格
        /// </summary>
        [FieldMapAttribute("REVERSION", typeof(string), 40, false)]
        public string Reversion;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BIOS", typeof(string), 40, false)]
        public string BIOS;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PCBA", typeof(string), 40, false)]
        public string PCBA;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DATECODE", typeof(string), 40, false)]
        public string DateCode;

        /// <summary>
        /// 替换的物料批号
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNO;

        /// <summary>
        /// 新物料料号
        /// </summary>
        [FieldMapAttribute("MITEMCODE", typeof(string), 40, true)]
        public string MItemCode;

        /// <summary>
        /// 新物料序列号
        /// </summary>
        [FieldMapAttribute("MCARD", typeof(string), 40, false)]
        public string MCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOCPOINT", typeof(decimal), 10, true)]
        public decimal LocationPoint;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 原物料料号
        /// </summary>
        [FieldMapAttribute("SITEMCODE", typeof(string), 40, false)]
        public string SourceItemCode;

        /// <summary>
        /// 原物料序列号
        /// </summary>
        [FieldMapAttribute("MSCARD", typeof(string), 40, false)]
        public string MSourceCard;


        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;


        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RRESCODE", typeof(string), 40, true)]
        public string RepairResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ROPCODE", typeof(string), 40, true)]
        public string RepairOPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MCARDTYPE", typeof(string), 40, true)]
        public string MCardType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, false)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMSEQ", typeof(decimal), 10, true)]
        public decimal ItemSequence;



        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSID", typeof(string), 40, true)]
        public string TSId;

        /// <summary>
        /// 是否再送维修
        /// </summary>
        [FieldMapAttribute("ISRETS", typeof(string), 40, true)]
        public string IsReTS;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOC", typeof(string), 40, false)]
        public string Location;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("QTY", typeof(decimal), 10, true)]
        public decimal Qty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;

        public string MSourceCardType;

    }
    #endregion

    #region TSSMTItem
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLTSSMTITEM", "TSID,ITEMSEQ")]
    public class TSSMTItem : DomainObject
    {
        public TSSMTItem()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DATECODE", typeof(string), 40, false)]
        public string DateCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MITEMCODE", typeof(string), 40, true)]
        public string MItemCode;

        /// <summary>
        /// 替换的物料批号
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNO;

        /// <summary>
        /// 替换的物料序号
        /// </summary>
        [FieldMapAttribute("MCARD", typeof(string), 40, false)]
        public string MCard;

        /// <summary>
        /// 是否再送维修
        /// </summary>
        [FieldMapAttribute("ISRETS", typeof(string), 40, true)]
        public string IsReTS;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOC", typeof(string), 40, false)]
        public string Location;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("QTY", typeof(decimal), 10, true)]
        public decimal Qty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSID", typeof(string), 40, true)]
        public string TSId;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOCPOINT", typeof(decimal), 10, true)]
        public decimal LocationPoint;

        /// <summary>
        /// 替换零件代码
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MSCARD", typeof(string), 40, false)]
        public string MSourceCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SITEMCODE", typeof(string), 40, false)]
        public string SourceItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RRESCODE", typeof(string), 40, true)]
        public string RepairResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ROPCODE", typeof(string), 40, true)]
        public string RepairOPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MCARDTYPE", typeof(string), 40, true)]
        public string MCardType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MEMO", typeof(string), 100, false)]
        public string MEMO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, false)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, false)]
        public string VendorCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("VENDORITEMCODE", typeof(string), 40, false)]
        public string VendorItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 规格
        /// </summary>
        [FieldMapAttribute("REVERSION", typeof(string), 40, false)]
        public string Reversion;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BIOS", typeof(string), 40, false)]
        public string BIOS;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMSEQ", typeof(decimal), 10, true)]
        public decimal ItemSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PCBA", typeof(string), 40, false)]
        public string PCBA;

    }
    #endregion

    #region TSSplitItem
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLTSSPLITITEM", "TSID,ITEMSEQ")]
    public class TSSplitItem : DomainObject
    {
        public TSSplitItem()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MEMO", typeof(string), 100, false)]
        public string MEMO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, false)]
        public string VendorCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("VENDORITEMCODE", typeof(string), 40, false)]
        public string VendorItemCode;

        /// <summary>
        /// 规格
        /// </summary>
        [FieldMapAttribute("REVERSION", typeof(string), 40, false)]
        public string Reversion;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BIOS", typeof(string), 40, false)]
        public string BIOS;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PCBA", typeof(string), 40, false)]
        public string PCBA;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DATECODE", typeof(string), 40, false)]
        public string DateCode;

        /// <summary>
        /// 替换的物料批号
        /// </summary>
        [FieldMapAttribute("MCARDTYPE", typeof(string), 40, true)]
        public string MCardType;

        /// <summary>
        /// 替换的物料序号
        /// </summary>
        [FieldMapAttribute("MCARD", typeof(string), 40, false)]
        public string MCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOCPOINT", typeof(decimal), 10, true)]
        public decimal LocationPoint;

        /// <summary>
        /// 替换零件代码
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RRESCODE", typeof(string), 40, true)]
        public string RepairResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ROPCODE", typeof(string), 40, true)]
        public string RepairOPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("QTY", typeof(decimal), 10, true)]
        public decimal Qty;

        /// <summary>
        /// 所有的数量
        /// </summary>
        [FieldMapAttribute("OPENQTY", typeof(decimal), 10, true)]
        public decimal OpenQty;

        /// <summary>
        /// 报废数量
        /// </summary>
        [FieldMapAttribute("SCRAPQTY", typeof(decimal), 10, true)]
        public decimal ScrapQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSID", typeof(string), 40, true)]
        public string TSId;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MITEMCODE", typeof(string), 40, true)]
        public string MItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMSEQ", typeof(decimal), 10, true)]
        public decimal ItemSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;

    }
    #endregion

    #region TSErrorCause2Com
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLTSERRORCAUSE2COM", "TSID,ECSCODE,ECGCODE,ECODE,ECSGCODE,ERRORCOMPONENT")]
    public class TSErrorCause2Com : DomainObject
    {
        public TSErrorCause2Com()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECSCODE", typeof(string), 40, false)]
        public string ErrorCauseCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECGCODE", typeof(string), 40, false)]
        public string ErrorCodeGroupCode;

        /// <summary>
        ///
        /// </summary>
        [FieldMapAttribute("ECODE", typeof(string), 40, false)]
        public string ErrorCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TSID", typeof(string), 40, false)]
        public string TSId;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECSGCODE", typeof(string), 40, false)]
        public string ErrorCauseGroupCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ERRORCOMPONENT", typeof(string), 40, false)]
        public string ErrorComponent;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, false)]
        public string ModelCode;

        /// <summary>
        /// 替换零件代码
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RRESCODE", typeof(string), 40, false)]
        public string RepairResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ROPCODE", typeof(string), 40, false)]
        public string RepairOPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSequence;

    }
    #endregion

    #region TSAndItemDesc
    [Serializable]
    public class TSAndItemName : TS
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        [FieldMapAttribute("ItemName", typeof(string), 40, true)]
        public string ItemName;
    }
    #endregion
}

