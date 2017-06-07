using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for Rework
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
/// ** 日 期:		2005-04-29 10:36:44
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.Rework
{

    #region Reject
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLREJECT", "RCARD,RCARDSEQ,mocode")]
    public class Reject : DomainObject
    {
        public Reject()
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
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string RouteCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string SegmentCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string StepSequenceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string ResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPID", typeof(string), 40, false)]
        public string OPId;

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
        [FieldMapAttribute("TCARD", typeof(string), 40, true)]
        public string TranslateCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SCARD", typeof(string), 40, true)]
        public string SourceCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RUSER", typeof(string), 40, false)]
        public string RejectUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RDATE", typeof(int), 8, true)]
        public int RejectDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RTIME", typeof(int), 6, true)]
        public int RejectTime;

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
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LOTNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REWORKCODE", typeof(string), 40, false)]
        public string ReworkCode;


        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REJSTATUS", typeof(string), 40, true)]
        public string RejectStatus;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;
    }
    #endregion

    #region Reject2ErrorCode
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLREJECT2ERRORCODE", "ECODE,RCARD,RCARDSEQ,ErrorCodeGroup,MOCODE")]
    public class Reject2ErrorCode : DomainObject
    {
        public Reject2ErrorCode()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECODE", typeof(string), 40, true)]
        public string ErrorCode;

        /// <summary>
        /// ManufactoryID
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
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

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
        [FieldMapAttribute("ErrorCodeGroup", typeof(string), 40, true)]
        public string ErrorCodeGroup;

        /// <summary>
        /// LOTNO
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNo;

        /// <summary>
        /// LOTSEQ
        /// </summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, false)]
        public decimal LotSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;
    }
    #endregion

    #region ReworkCause
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLREWORKCAUSE", "REWORKCCODE")]
    public class ReworkCause : DomainObject
    {
        public ReworkCause()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REWORKCCODE", typeof(string), 40, true)]
        public string ReworkCauseCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REWORKCDESC", typeof(string), 100, false)]
        public string Description;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
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

    }
    #endregion

    #region ReworkPass
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLREWORKPASS", "SEQ,ReworkCode")]
    public class ReworkPass : DomainObject
    {
        public ReworkPass()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ReworkCode", typeof(string), 40, true)]
        public string ReworkCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PSEQ", typeof(decimal), 10, true)]
        public decimal PassSequence;

        /// <summary>
        /// 0:未签核
        /// 1：已经签核
        /// </summary>
        [FieldMapAttribute("ISPASS", typeof(decimal), 10, true)]
        public decimal IsPass;

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
        [FieldMapAttribute("PCONTENT", typeof(string), 100, false)]
        public string PassContent;

        /// <summary>
        /// 0:等待签核      Waiting
        /// 1：签核通过     Passed
        /// 2：签核不通过   NoPassed
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(decimal), 10, true)]
        public decimal Status;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string PassUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 用户代码
        /// </summary>
        [FieldMapAttribute("USERCODE", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string UserCode;

    }
    #endregion

    #region ReworkRange
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLREWORKRANGE", "ReworkCode,RCARD,RCARDSEQ")]
    public class ReworkRange : DomainObject
    {
        public ReworkRange()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ReworkCode", typeof(string), 40, true)]
        public string ReworkCode;

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

    }
    #endregion

    #region ReworkSheet
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLREWORKSHEET", "ReworkCode")]
    public class ReworkSheet : DomainObject
    {
        public ReworkSheet()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REWORKSCODE", typeof(string), 40, true)]
        public string ReworkSourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ReworkCode", typeof(string), 40, true)]
        public string ReworkCode;

        /// <summary>
        /// OnLine:线上重工
        /// ReMo:重开工单重工
        /// </summary>
        [FieldMapAttribute("REWORKTYPE", typeof(string), 40, true)]
        public string ReworkType;

        /// <summary>
        /// New
        /// Waiting
        /// Open
        /// Complete
        /// Close
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string Status;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REWORKHC", typeof(decimal), 10, true)]
        public decimal ReworkHC;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DEPARTMENT", typeof(string), 40, false)]
        public string Department;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CONTENT", typeof(string), 100, false)]
        public string ReworkContent;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REWORKDATE", typeof(int), 8, false)]
        public int ReworkDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REWORKTIME", typeof(int), 6, false)]
        public int ReworkTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REWORKQTY", typeof(decimal), 10, true)]
        public decimal ReworkQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REWORKMAXQTY", typeof(decimal), 10, true)]
        public decimal ReworkMaxQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REWORKREALQTY", typeof(decimal), 10, true)]
        public decimal ReworkRealQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
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
        [FieldMapAttribute("CUSER", typeof(string), 40, true)]
        public string CreateUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CDATE", typeof(int), 8, true)]
        public int CreateDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CTIME", typeof(int), 6, true)]
        public int CreateTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("NEWMOCODE", typeof(string), 40, false)]
        public string NewMOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("NEWROUTECODE", typeof(string), 40, false)]
        public string NewRouteCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("NEWOPBOMCODE", typeof(string), 40, false)]
        public string NewOPBOMCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("NEWOPBOMVER", typeof(string), 40, false)]
        public string NewOPBOMVersion;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("NEWOPCODE", typeof(string), 40, false)]
        public string NewOPCode;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REWORKREASON", typeof(string), 1000, true)]
        public string ReworkReason;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REASONANALYSE", typeof(string), 1000, true)]
        public string ReasonAnalyse;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SOLUTION", typeof(string), 1000, true)]
        public string Soluation;

        [FieldMapAttribute("NeedCheck", typeof(string), 1, false)]
        public string NeedCheck;

        [FieldMapAttribute("LOTLIST", typeof(string), 1000, true)]
        public string LotList;

        [FieldMapAttribute("AUTOLOTNO", typeof(string), 40, true)]
        public string AutoLot;

        /// <summary>
        /// DUTYCODE
        /// </summary>
        [FieldMapAttribute("DUTYCODE", typeof(string), 40, true)]
        public string DutyCode;
    }
    #endregion

    #region ReworkSheet2Cause
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLREWORKSHEET2CAUSE", "ReworkCode,REWORKCCODE")]
    public class ReworkSheet2Cause : DomainObject
    {
        public ReworkSheet2Cause()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ReworkCode", typeof(string), 40, true)]
        public string ReworkCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REWORKCCODE", typeof(string), 40, true)]
        public string ReworkCauseCode;

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

    }
    #endregion

    #region ReworkSource
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLREWORKSOURCE", "REWORKSCODE")]
    public class ReworkSource : DomainObject
    {
        public ReworkSource()
        {
        }

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
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REWORKSCODE", typeof(string), 40, true)]
        public string ReworkSourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REWORKSDESC", typeof(string), 100, false)]
        public string Description;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

    }
    #endregion

    #region REWORKLOTNO
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLTEMPREWORKLOTNO", "LOTNO")]
    public class ReworkLotNo : DomainObject
    {
        public ReworkLotNo()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, true)]
        public string EAttribute1;

    }
    #endregion

    #region REWORKRCARD
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLTEMPREWORKRCARD", "LOTNO,RCARD")]
    public class ReworkRcard : DomainObject
    {
        public ReworkRcard()
        {
        }

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
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PALLETCODE", typeof(string), 40, false)]
        public string PalletCode;


        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, true)]
        public string EAttribute1;

    }
    #endregion

    #region TempRework
    [Serializable]
    public class TempRework : DomainObject
    {
        public TempRework()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDESC", typeof(string), 40, false)]
        public string MaterialDescription;

        [FieldMapAttribute("TOTALCOUNT", typeof(int), 8, false)]
        public int TotalCount;

        [FieldMapAttribute("UNCONFIRMEDCOUNT", typeof(int), 8, false)]
        public int UnConfirmedCount;
    }
    #endregion

    #region ReworkSheetQuery

    [Serializable]
    public class ReworkSheetQuery : ReworkSheet
    {
        [FieldMapAttribute("MMODELCODE", typeof(string), 40, true)]
        public string MaterialModelCode;

        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, true)]
        public string BigStepSequenceCode;

        [FieldMapAttribute("DDATE", typeof(int), 8, true)]
        public int DDATE;

    }

    #endregion
}

