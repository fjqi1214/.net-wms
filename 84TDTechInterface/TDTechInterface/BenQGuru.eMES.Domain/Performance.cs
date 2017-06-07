using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.Performance
{
    #region PlanWorkTime

    /// <summary>
    ///	PlanWorkTime
    /// </summary>
    [Serializable, TableMap("TBLPLANWORKTIME", "ITEMCODE, SSCODE")]
    public class PlanWorkTime : DomainObject
    {
        public PlanWorkTime()
        {
        }

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///SSCode
        ///</summary>	
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string SSCode;

        ///<summary>
        ///CycleTime
        ///</summary>	
        [FieldMapAttribute("CYCLETIME", typeof(int), 38, false)]
        public int CycleTime;

        ///<summary>
        ///WorkingTime
        ///</summary>	
        [FieldMapAttribute("WORKINGTIME", typeof(int), 38, false)]
        public int WorkingTime;

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

    #region Line2Crew

    /// <summary>
    ///	Line2Crew
    /// </summary>
    [Serializable, TableMap("TBLLINE2CREW", "SHIFTDATE, SSCODE, SHIFTCODE")]
    public class Line2Crew : DomainObject
    {
        public Line2Crew()
        {
        }

        ///<summary>
        ///ShiftDate
        ///</summary>	
        [FieldMapAttribute("SHIFTDATE", typeof(int), 8, false)]
        public int ShiftDate;

        ///<summary>
        ///SSCode
        ///</summary>	
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string SSCode;

        ///<summary>
        ///ShiftCode
        ///</summary>	
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
        public string ShiftCode;

        ///<summary>
        ///CrewCode
        ///</summary>	
        [FieldMapAttribute("CREWCODE", typeof(string), 40, false)]
        public string CrewCode;

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

    #region Line2ManDetail

    /// <summary>
    ///	Line2ManDetail
    /// </summary>
    [Serializable, TableMap("TBLLINE2MANDETAIL", "SERIAL")]
    public class Line2ManDetail : DomainObject
    {
        public Line2ManDetail()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        ///UserCode
        ///</summary>	
        [FieldMapAttribute("USERCODE", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string UserCode;

        ///<summary>
        ///SSCode
        ///</summary>	
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLSS", "SSCODE", "SSDESC")]
        public string SSCode;

        ///<summary>
        ///OPCode
        ///</summary>	
        [FieldMapAttribute("OPCODE", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLOP", "OPCODE", "OPDESC")]
        public string OPCode;

        ///<summary>
        ///ResourceCode
        ///</summary>	
        [FieldMapAttribute("RESCODE", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLRES", "RESCODE", "RESDESC")]
        public string ResourceCode;

        ///<summary>
        ///Status
        ///</summary>	
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///ShiftDate
        ///</summary>	
        [FieldMapAttribute("SHIFTDATE", typeof(int), 8, false)]
        public int ShiftDate;

        ///<summary>
        ///ShiftCode
        ///</summary>	
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLSHIFT", "SHIFTCODE", "SHIFTDESC")]
        public string ShiftCode;

        ///<summary>
        ///OnDate
        ///</summary>	
        [FieldMapAttribute("ONDATE", typeof(int), 8, false)]
        public int OnDate;

        ///<summary>
        ///OnTime
        ///</summary>	
        [FieldMapAttribute("ONTIME", typeof(int), 6, false)]
        public int OnTime;

        ///<summary>
        ///OffDate
        ///</summary>	
        [FieldMapAttribute("OFFDATE", typeof(int), 8, false)]
        public int OffDate;

        ///<summary>
        ///OffTime
        ///</summary>	
        [FieldMapAttribute("OFFTIME", typeof(int), 6, false)]
        public int OffTime;

        ///<summary>
        ///MOCode
        ///</summary>	
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///Duration
        ///</summary>	
        [FieldMapAttribute("DURATION", typeof(int), 10, false)]
        public int Duration;

        ///<summary>
        ///ManActQty
        ///</summary>	
        [FieldMapAttribute("MANACTQTY", typeof(decimal), 10, false)]
        public decimal ManActQty;

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

    public class Line2ManDetailEx : Line2ManDetail
    {
        [FieldMapAttribute("USERNAME", typeof(string), 40, true)]
        public string UserName;

        [FieldMapAttribute("OPDESC", typeof(string), 100, true)]
        public string OPDesc;

        [FieldMapAttribute("RESDESC", typeof(string), 100, true)]
        public string ResDesc;

        [FieldMapAttribute("SHIFTDESC", typeof(string), 100, true)]
        public string ShiftDesc;

    }

    #endregion

    #region IndirectManCount

    /// <summary>
    ///	IndirectManCount
    /// </summary>
    [Serializable, TableMap("TBLINDIRECTMANCOUNT", "SHIFTDATE,SHIFTCODE,CREWCODE,FACCODE,FIRSTCLASS")]
    public class IndirectManCount : DomainObject
    {
        public IndirectManCount()
        {
        }

        ///<summary>
        ///ShiftDate
        ///</summary>	
        [FieldMapAttribute("SHIFTDATE", typeof(int), 8, false)]
        public int ShiftDate;

        ///<summary>
        ///ShiftCode
        ///</summary>	
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
        public string ShiftCode;

        ///<summary>
        ///CrewCode
        ///</summary>	
        [FieldMapAttribute("CREWCODE", typeof(string), 40, false)]
        public string CrewCode;

        ///<summary>
        ///FactoryCode
        ///</summary>	
        [FieldMapAttribute("FACCODE", typeof(string), 40, false)]
        public string FactoryCode;

        ///<summary>
        ///FirstClass
        ///</summary>	
        [FieldMapAttribute("FIRSTCLASS", typeof(string), 40, false)]
        public string FirstClass;

        ///<summary>
        ///ManCount
        ///</summary>	
        [FieldMapAttribute("MANCOUNT", typeof(int), 10, false)]
        public int ManCount;

        ///<summary>
        ///Duration
        ///</summary>	
        [FieldMapAttribute("DURATION", typeof(int), 10, false)]
        public int Duration;

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

    #region ProduceDetail

    /// <summary>
    ///	ProduceDetail
    /// </summary>
    [Serializable, TableMap("TBLPRODDETAIL", "SERIAL")]
    public class ProduceDetail : DomainObject
    {
        public ProduceDetail()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        ///SSCode
        ///</summary>	
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string SSCode;

        ///<summary>
        ///ShiftDate
        ///</summary>	
        [FieldMapAttribute("SHIFTDATE", typeof(int), 8, false)]
        public int ShiftDate;

        ///<summary>
        ///ShiftCode
        ///</summary>	
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
        public string ShiftCode;

        ///<summary>
        ///MOCode
        ///</summary>	
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///ManCount
        ///</summary>	
        [FieldMapAttribute("MANCOUNT", typeof(int), 10, false)]
        public int ManCount;

        ///<summary>
        ///BeginDate
        ///</summary>	
        [FieldMapAttribute("BEGINDATE", typeof(int), 8, false)]
        public int BeginDate;

        ///<summary>
        ///BeginTime
        ///</summary>	
        [FieldMapAttribute("BEGINTIME", typeof(int), 6, false)]
        public int BeginTime;

        ///<summary>
        ///EndDate
        ///</summary>	
        [FieldMapAttribute("ENDDATE", typeof(int), 8, false)]
        public int EndDate;

        ///<summary>
        ///EndTime
        ///</summary>	
        [FieldMapAttribute("ENDTIME", typeof(int), 6, false)]
        public int EndTime;

        ///<summary>
        ///Duration
        ///</summary>	
        [FieldMapAttribute("DURATION", typeof(int), 10, false)]
        public int Duration;

        ///<summary>
        ///Status
        ///</summary>	
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

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

    #region LinePause

    /// <summary>
    ///	LinePause
    /// </summary>
    [Serializable, TableMap("TBLLINEPAUSE", "SERIAL")]
    public class LinePause : DomainObject
    {
        public LinePause()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        ///SSCode
        ///</summary>	
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string SSCode;

        ///<summary>
        ///ManCount
        ///</summary>	
        [FieldMapAttribute("MANCOUNT", typeof(int), 10, false)]
        public int ManCount;

        ///<summary>
        ///BeginDate
        ///</summary>	
        [FieldMapAttribute("BEGINDATE", typeof(int), 8, false)]
        public int BeginDate;

        ///<summary>
        ///BeginTime
        ///</summary>	
        [FieldMapAttribute("BEGINTIME", typeof(int), 6, false)]
        public int BeginTime;

        ///<summary>
        ///EndDate
        ///</summary>	
        [FieldMapAttribute("ENDDATE", typeof(int), 8, false)]
        public int EndDate;

        ///<summary>
        ///EndTime
        ///</summary>	
        [FieldMapAttribute("ENDTIME", typeof(int), 6, false)]
        public int EndTime;

        ///<summary>
        ///Duration
        ///</summary>	
        [FieldMapAttribute("DURATION", typeof(int), 10, false)]
        public int Duration;

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

    #region ExceptionCode

    /// <summary>
    ///	ExceptionCode
    /// </summary>
    [Serializable, TableMap("TBLEXCEPTIONCODE", "EXCEPTIONCODE")]
    public class ExceptionCode : DomainObject
    {
        public ExceptionCode()
        {
        }

        ///<summary>
        ///ExceptionCode
        ///</summary>	
        [FieldMapAttribute("EXCEPTIONCODE", typeof(string), 40, false)]
        public string Code;

        ///<summary>
        ///ExceptionName
        ///</summary>	
        [FieldMapAttribute("EXCEPTIONNAME", typeof(string), 40, false)]
        public string Name;

        ///<summary>
        ///ExceptionDescription
        ///</summary>	
        [FieldMapAttribute("EXCEPTIONDESC", typeof(string), 200, false)]
        public string Description;

        ///<summary>
        ///ExceptionType
        ///</summary>	
        [FieldMapAttribute("EXCEPTIONTYPE", typeof(string), 40, false)]
        public string Type;

        ///<summary>
        ///ExceptionFlag
        ///</summary>	
        [FieldMapAttribute("EXCEPTIONFLAG", typeof(string), 40, false)]
        public string Flag;

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

    #region ExceptionEvent

    /// <summary>
    ///	ExceptionEvent
    /// </summary>
    [Serializable, TableMap("TBLEXCEPTION", "SERIAL")]
    public class ExceptionEvent : DomainObject
    {
        public ExceptionEvent()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        ///SSCode
        ///</summary>	
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string SSCode;

        ///<summary>
        ///ShiftDate
        ///</summary>	
        [FieldMapAttribute("SHIFTDATE", typeof(int), 8, false)]
        public int ShiftDate;

        ///<summary>
        ///ShiftCode
        ///</summary>	
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
        public string ShiftCode;

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///BeginTime
        ///</summary>	
        [FieldMapAttribute("BEGINTIME", typeof(int), 6, false)]
        public int BeginTime;

        ///<summary>
        ///EndTime
        ///</summary>	
        [FieldMapAttribute("ENDTIME", typeof(int), 6, false)]
        public int EndTime;

        ///<summary>
        ///ExceptionCode
        ///</summary>	
        [FieldMapAttribute("EXCEPTIONCODE", typeof(string), 40, false)]
        public string ExceptionCode;

        ///<summary>
        ///Memo
        ///</summary>	
        [FieldMapAttribute("MEMO", typeof(string), 500, true)]
        public string Memo;

        ///<summary>
        ///ComfirmMemo
        ///</summary>	
        [FieldMapAttribute("COMFIRMMEMO", typeof(string), 500, true)]
        public string ComfirmMemo;

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

    #region LostManHourHead

    /// <summary>
    ///	LostManHourHead
    /// </summary>
    [Serializable, TableMap("TBLLOSTMANHOUR", "SSCODE, SHIFTDATE, SHIFTCODE, ITEMCODE")]
    public class LostManHourHead : DomainObject
    {
        public LostManHourHead()
        {
        }

        ///<summary>
        ///SSCode
        ///</summary>	
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string SSCode;

        ///<summary>
        ///ShiftDate
        ///</summary>	
        [FieldMapAttribute("SHIFTDATE", typeof(int), 8, false)]
        public int ShiftDate;

        ///<summary>
        ///ShiftCode
        ///</summary>	
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
        public string ShiftCode;

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///ActManHour
        ///</summary>	
        [FieldMapAttribute("ACTMANHOUR", typeof(int), 10, false)]
        public int ActManHour;

        ///<summary>
        ///ActOutput
        ///</summary>	
        [FieldMapAttribute("ACTOUTPUT", typeof(int), 10, false)]
        public int ActOutput;

        ///<summary>
        ///AcquireManHour
        ///</summary>	
        [FieldMapAttribute("ACQUIREMANHOUR", typeof(int), 10, false)]
        public int AcquireManHour;

        ///<summary>
        ///LostManHour
        ///</summary>	
        [FieldMapAttribute("LOSTMANHOUR", typeof(int), 10, false)]
        public int LostManHour;

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

    #region LostManHourDetail

    /// <summary>
    ///	LostManHourDetail
    /// </summary>
    [Serializable, TableMap("TBLLOSTMANHOURDETAIL", "SSCODE, SHIFTDATE, SHIFTCODE, ITEMCODE, SEQ")]
    public class LostManHourDetail : DomainObject
    {
        public LostManHourDetail()
        {
        }

        ///<summary>
        ///SSCode
        ///</summary>	
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string SSCode;

        ///<summary>
        ///ShiftDate
        ///</summary>	
        [FieldMapAttribute("SHIFTDATE", typeof(int), 8, false)]
        public int ShiftDate;

        ///<summary>
        ///ShiftCode
        ///</summary>	
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
        public string ShiftCode;

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///Seq
        ///</summary>	
        [FieldMapAttribute("SEQ", typeof(int), 10, false)]
        public int Seq;

        ///<summary>
        ///LostManHour
        ///</summary>	
        [FieldMapAttribute("LOSTMANHOUR", typeof(int), 10, false)]
        public int LostManHour;

        ///<summary>
        ///ExceptionCode
        ///</summary>	
        [FieldMapAttribute("EXCEPTIONCODE", typeof(string), 40, false)]
        public string ExceptionCode;

        ///<summary>
        ///ExceptionSerial
        ///</summary>	
        [FieldMapAttribute("EXCEPTIONSERIAL", typeof(int), 38, false)]
        public int ExceptionSerial;

        ///<summary>
        ///DutyCode
        ///</summary>	
        [FieldMapAttribute("DUTYCODE", typeof(string), 40, false)]
        public string DutyCode;

        ///<summary>
        ///Memo
        ///</summary>	
        [FieldMapAttribute("MEMO", typeof(string), 500, true)]
        public string Memo;

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

    public class PlanWorkTimeWithMessage : PlanWorkTime
    {
        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, true)]
        public string BigSSCode;

        [FieldMapAttribute("ITEMDESC", typeof(string), 100, true)]
        public string ItemDescription;

        [FieldMapAttribute("SSDESC", typeof(string), 100, false)]
        public string StepSequenceDescription;
    }

    public class Line2CrewWithMessage : Line2Crew
    {
        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, true)]
        public string BigSSCode;

        [FieldMapAttribute("SSDESC", typeof(string), 100, false)]
        public string StepSequenceDescription;

        [FieldMapAttribute("SHIFTDESC", typeof(string), 100, false)]
        public string ShiftDescription;

        [FieldMapAttribute("CREWDESC", typeof(string), 100, true)]
        public string CrewDesc;
    }

    public class IndirectManCountWithMessage : IndirectManCount
    {
        [FieldMapAttribute("SHIFTDESC", typeof(string), 100, false)]
        public string ShiftDescription;

        [FieldMapAttribute("CREWDESC", typeof(string), 100, true)]
        public string CrewDesc;

        [FieldMapAttribute("FACDESC", typeof(string), 100, false)]
        public string FacDesc;
    }

    public class LostManHourHeadWithMessage : LostManHourHead
    {
        [FieldMapAttribute("SSDESC", typeof(string), 100, false)]
        public string StepSequenceDescription;

        [FieldMapAttribute("SHIFTDESC", typeof(string), 100, false)]
        public string ShiftDescription;

        [FieldMapAttribute("ITEMDESC", typeof(string), 100, true)]
        public string ItemDescription;
    }

    public class LostManHourDetailWithMessage : LostManHourDetail
    {
        [FieldMapAttribute("BEGINTIME", typeof(int), 6, false)]
        public int BeginTime;

        [FieldMapAttribute("ENDTIME", typeof(int), 6, false)]
        public int EndTime;

        [FieldMapAttribute("ITEMDESC", typeof(string), 100, true)]
        public string ItemDescription;

        [FieldMapAttribute("EXCEPTIONDESC", typeof(string), 200, false)]
        public string ExceptionDescription;

        [FieldMapAttribute("EXCEPTIONMEMO", typeof(string), 500, true)]
        public string ExceptionMemo;
    }

    public class ExceptionEventWithDescription : ExceptionEvent
    {
        [FieldMapAttribute("EXCEPTIONDESC", typeof(string), 200, false)]
        public string Description;

        [FieldMapAttribute("SSDESC", typeof(string), 100, true)]
        public string SSDesc;
    }

    [Serializable]
    public class watchPanelProductDate : DomainObject
    {
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;
        //Added By Nettie Chen 2009/09/23
        [FieldMapAttribute("ITEMNAME", typeof(string), 40, true)]
        public string ItemName;
        //End Added
        [FieldMapAttribute("MATERIALMODELCODE", typeof(string), 40, true)]
        public string MaterialModelCode;

        [FieldMapAttribute("DAYPLANQTY", typeof(int), 22, true)]
        public int DayPlanQty;

        [FieldMapAttribute("PERTIMEOUTPUTQTY", typeof(int), 22, true)]
        public int PerTimeOutPutQty;

        [FieldMapAttribute("PASSRATE", typeof(decimal), 22, true)]
        public decimal PassRate;

        [FieldMapAttribute("ONENEEDTIME", typeof(decimal), 22, true)]
        public decimal OneNeedTime;

        [FieldMapAttribute("UPPH", typeof(decimal), 22, true)]
        public decimal UPPH;

        [FieldMapAttribute("TPCODE", typeof(string), 40, true)]
        public string TimePeriodCode;

        [FieldMapAttribute("MANCOUNT", typeof(int), 22, true)]
        public int ManCount;

        [FieldMapAttribute("MONTHPRODUCTQTY", typeof(int), 22, true)]
        public int MonthProductQty;

        [FieldMapAttribute("mType", typeof(string), 40, true)]
        public string Mtype;

        [FieldMapAttribute("QualifiedRate", typeof(decimal), 22, true)]
        public decimal QualifiedRate;

        [FieldMapAttribute("SSCode", typeof(string), 40, true)]
        public string SSCode;

        [FieldMapAttribute("ERRORCAUSE", typeof(string), 40, true)]
        public string ErrorCause;

        [FieldMapAttribute("ECSDESC", typeof(string), 40, true)]
        public string ErroeCauseDesc;

        [FieldMapAttribute("ERRORCAUSERATE", typeof(decimal), 22, true)]
        public decimal ErrorCauseRate;

        [FieldMapAttribute("OQCLOTPASSRATE", typeof(decimal), 22, true)]
        public decimal OQCLotPassRate;

        [FieldMapAttribute("INTPUT", typeof(int), 22, true)]
        public int IntPut;

        //因为下面栏位是根据大线下的班次决定,所以暂时默认是个班次产出
        [FieldMapAttribute("SHIFTLOUT1", typeof(int), 22, true)]
        public int ShiftLineOutPut1;

        [FieldMapAttribute("SHIFTLOUT2", typeof(int), 22, true)]
        public int ShiftLineOutPut2;

        [FieldMapAttribute("SHIFTLOUT3", typeof(int), 22, true)]
        public int ShiftLineOutPut3;

        [FieldMapAttribute("SHIFTLOUT4", typeof(int), 22, true)]
        public int ShiftLineOutPut4;

        [FieldMapAttribute("SHIFTLOUT5", typeof(int), 22, true)]
        public int ShiftLineOutPut5;

        [FieldMapAttribute("SHIFTLOUT6", typeof(int), 22, true)]
        public int ShiftLineOutPut6;

        [FieldMapAttribute("SHIFTLOUT7", typeof(int), 22, true)]
        public int ShiftLineOutPut7;

        [FieldMapAttribute("SHIFTLOUT8", typeof(int), 22, true)]
        public int ShiftLineOutPut8;

        [FieldMapAttribute("SHIFTLOUT9", typeof(int), 22, true)]
        public int ShiftLineOutPut9;

        [FieldMapAttribute("SHIFTLOUT10", typeof(int), 22, true)]
        public int ShiftLineOutPut10;

    }

}
