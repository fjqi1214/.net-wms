using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.WatchPanelNew
{   
    #region QDOTSInfo
    /// <summary>
    /// QDOTSInfo 的摘要说明。
    /// </summary>
    public class QDOTSInfo : DomainObject
    {
        [FieldMapAttribute("errorcodegroup", typeof(string), 40, true)]
        public string ErrorCodeGroup;

        [FieldMapAttribute("ecgdesc", typeof(string), 40, true)]
        public string ErrorCodeGroupDesc;

        [FieldMapAttribute("errorcode", typeof(string), 40, true)]
        public string ErrorCode;

        [FieldMapAttribute("ecdesc", typeof(string), 40, true)]
        public string ErrorCodeDesc;

        [FieldMapAttribute("errorcausegroup", typeof(string), 40, true)]
        public string ErrorCauseGroup;

        [FieldMapAttribute("ecsgdesc", typeof(string), 40, true)]
        public string ErrorCauseGroupDesc;

        [FieldMapAttribute("errorcause", typeof(string), 40, true)]
        public string ErrorCause;

        [FieldMapAttribute("ecsdesc", typeof(string), 40, true)]
        public string ErrorCauseDesc;

        [FieldMapAttribute("errorlocation", typeof(string), 40, true)]
        public string ErrorLocation;

        [FieldMapAttribute("duty", typeof(string), 40, true)]
        public string Duty;

        [FieldMapAttribute("ERRORCOMPONENT", typeof(string), 40, true)]
        public string ErrorComponent;

        [FieldMapAttribute("dutydesc", typeof(string), 40, true)]
        public string DutyDesc;

        [FieldMapAttribute("qty", typeof(int), 10, true)]
        public int Quantity;

        [FieldMapAttribute("allqty", typeof(int), 10, true)]
        public int AllQuantity;

        [FieldMapAttribute("percent", typeof(decimal), 10, true)]
        public decimal Percent;
    }

    #endregion

    #region NewReportDomainObject

    public class NewReportDomainObject : DomainObject
    {
        [FieldMapAttribute("TPCODE", typeof(string), 40, true)]
        public string PeriodCode;

        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        public string ShiftCode;

        [FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, true)]
        public string ShiftTypeCode;

        [FieldMapAttribute("TPSEQ", typeof(int), 22, true)]
        public int TPSeq;

        [FieldMapAttribute("SHIFTDAY", typeof(string), 40, true)]
        public string ShiftDay;

        [FieldMapAttribute("DWEEK", typeof(string), 40, true)]
        public string Week;

        [FieldMapAttribute("DMONTH", typeof(string), 40, true)]
        public string Month;

        [FieldMapAttribute("YEAR", typeof(string), 40, true)]
        public string Year;

        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, true)]
        public string BigSSCode;

        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string SegCode;

        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string SSCode;

        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string ResCode;

        [FieldMapAttribute("MTYPE", typeof(string), 40, true)]
        public string GoodSemiGood;

        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        [FieldMapAttribute("PROJECtCODE", typeof(string), 40, true)]
        public string ProjectCode;

        [FieldMapAttribute("MATERIALCODE", typeof(string), 40, true)]
        public string MaterialCode;

        [FieldMapAttribute("MMODELCODE", typeof(string), 40, true)]
        public string MaterialModelCode;

        [FieldMapAttribute("MMACHINETYPE", typeof(string), 40, true)]
        public string MaterialMachineType;

        [FieldMapAttribute("MEXPORTIMPORT", typeof(string), 40, true)]
        public string MaterialExportImport;

        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        [FieldMapAttribute("MOMEMO", typeof(string), 100, true)]
        public string MOMemo;

        [FieldMapAttribute("NEWMASS", typeof(string), 100, true)]
        public string NewMass;

        [FieldMapAttribute("CREWCODE", typeof(string), 40, true)]
        public string CrewCode;

        [FieldMapAttribute("FIRSTCLASS", typeof(string), 40, true)]
        public string FirstClass;

        [FieldMapAttribute("SECONDCLASS", typeof(string), 40, true)]
        public string SecondClass;

        [FieldMapAttribute("THIRDCLASS", typeof(string), 40, true)]
        public string ThirdClass;

        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MaterialDesc;

        [FieldMapAttribute("ORDERNO", typeof(string), 40, true)]
        public string OrderNo;

        [FieldMapAttribute("MOBOM", typeof(string), 40, true)]
        public string MOBOMVer;

        [FieldMapAttribute("TIMESTRING", typeof(string), 200, true)]
        public string TimeString;

        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string EAttribute1;

        [FieldMapAttribute("INPUT", typeof(decimal), 22, true)]
        public decimal Input;

        [FieldMapAttribute("OUTPUT", typeof(decimal), 22, true)]
        public decimal Output;

        [FieldMapAttribute("KEYPARTSINPUT", typeof(int), 22, true)]
        public int KeyPartsInput;

        [FieldMapAttribute("COMPELETEPUT", typeof(int), 22, true)]
        public int CompeletePut;

        [FieldMapAttribute("YR", typeof(float), 10, true)]
        public float YR;

        [FieldMapAttribute("ACHIEVEDRATE", typeof(float), 10, true)]
        public float AchievedRate;

        [FieldMapAttribute("TESTTIMES", typeof(int), 22, true)]
        public int TestTimes;

        [FieldMapAttribute("NGTIMES", typeof(int), 22, true)]
        public int NGTimes;

        [FieldMapAttribute("NGRATE", typeof(float), 10, true)]
        public float NGRate;

        [FieldMapAttribute("NGQTY", typeof(int), 22, true)]
        public int NGQTY;

        [FieldMapAttribute("SAMPLEQTY", typeof(int), 22, true)]
        public int SAMPLEQTY;

        [FieldMapAttribute("MAXQTY", typeof(int), 22, true)]
        public int MaxQty;

        [FieldMapAttribute("AVGQTY", typeof(float), 10, true)]
        public float AvgQty;

        [FieldMapAttribute("AGGCOUNT", typeof(int), 22, true)]
        public int AggCount;

        [FieldMapAttribute("PASSRCARDQTY", typeof(int), 22, true)]
        public int PassRcardQty;

        [FieldMapAttribute("PASSRCARDRATE", typeof(decimal), 10, true)]
        public decimal PassRcardRate;

        [FieldMapAttribute("FIRSTPASSYIELDBYECSG", typeof(float), 10, true)]
        public float FirstPassYieldByECSG;

        [FieldMapAttribute("DUTYCODE", typeof(string), 40, true)]
        public string DutyCode;

        [FieldMapAttribute("DUTYDESC", typeof(string), 40, true)]
        public string DutyDesc;

        [FieldMapAttribute("ECSGCODE", typeof(string), 40, false)]
        public string ErrorCauseGroupCode;

        [FieldMapAttribute("ERRORCOUNT", typeof(int), 22, true)]
        public int ErrorCount;

        [FieldMapAttribute("ERRORPERCENT", typeof(float), 10, true)]
        public float ErrorPercent;

        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string FacCode;

        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNo;

        [FieldMapAttribute("OQCLOTTYPE", typeof(string), 40, false)]
        public string OQCLotType;

        [FieldMapAttribute("PRODUCTIONTYPE", typeof(string), 40, true)]
        public string ProductionType;

        [FieldMapAttribute("OQCNGCOUNT", typeof(int), 22, true)]
        public int OQCNGCount;

        [FieldMapAttribute("OQCSSIZE", typeof(int), 22, true)]
        public int OQCSampleSize;

        [FieldMapAttribute("OQCSAMPLENGRATE", typeof(float), 10, true)]
        public float OQCSampleNGRate;

        [FieldMapAttribute("OQCSAMPLENGRATEPPM", typeof(float), 10, true)]
        public float OQCSampleNGRatePPM;

        [FieldMapAttribute("OQCPASSCOUNT", typeof(int), 22, true)]
        public int OQCPassCount;

        [FieldMapAttribute("OQCLOTCOUNT", typeof(int), 22, true)]
        public int OQCLotCount;

        [FieldMapAttribute("ALLCOUNT", typeof(int), 22, true)]
        public int AllCount;

        [FieldMapAttribute("SPOTCOUNT", typeof(int), 22, true)]
        public int SpotCount;

        [FieldMapAttribute("DPPM", typeof(float), 10, true)]
        public float DPPM;

        [FieldMapAttribute("OQCLOTPASSRATE", typeof(float), 10, true)]
        public float OQCLotPassRate;

        [FieldMapAttribute("TOTALGRADETIMES", typeof(float), 10, true)]
        public float TotalGradeTimes;

        [FieldMapAttribute("GRADETIMESPERCENT", typeof(float), 10, true)]
        public float GradeTimesPercent;

        [FieldMapAttribute("ZGRADETIMES", typeof(float), 10, true)]
        public float ZGradeTimes;

        [FieldMapAttribute("AGRADETIMES", typeof(float), 10, true)]
        public float AGradeTimes;

        [FieldMapAttribute("BGGRADETIMES", typeof(float), 10, true)]
        public float BGradeTimes;

        [FieldMapAttribute("CGRADETIMES", typeof(float), 10, true)]
        public float CGradeTimes;

        [FieldMapAttribute("OQCQUALITYLEVELVALUE", typeof(float), 10, true)]
        public float OQCQualityLevelValue;
                
        [FieldMapAttribute("WORKINGTIME", typeof(float), 10, true)]
        public float WorkingTime;

        [FieldMapAttribute("CYCLETIME", typeof(float), 10, true)]
        public float CycleTime;

        [FieldMapAttribute("INPUTDURATION", typeof(float), 10, true)]
        public float InputDuration;

        [FieldMapAttribute("MANHOURSUM", typeof(float), 10, true)]
        public float ManHourSum;

        [FieldMapAttribute("INVALIDMANHOUR", typeof(float), 10, true)]
        public float InvalidManHour;

        [FieldMapAttribute("LOSTMANHOUR1", typeof(float), 10, true)]
        public float LostManHour1;

        [FieldMapAttribute("LOSTMANHOUR2", typeof(float), 10, true)]
        public float LostManHour2;

        [FieldMapAttribute("UTILIZATION", typeof(float), 10, true)]
        public float Utilization;

        [FieldMapAttribute("INDIRECTMANHOUR", typeof(float), 10, true)]
        public float IndirectManHour;

        [FieldMapAttribute("PLANQTY", typeof(float), 10, true)]
        public float PlanQty;

        [FieldMapAttribute("REALQTY", typeof(float), 10, true)]
        public float RealQty;

        [FieldMapAttribute("LOSTQTY", typeof(float), 10, true)]
        public float LostQty;

        [FieldMapAttribute("STANDARDQTY", typeof(float), 10, true)]
        public float StandardQty;

        [FieldMapAttribute("UPPH", typeof(float), 10, true)]
        public float UPPH;

        [FieldMapAttribute("MANHOURPERPRODUCT", typeof(float), 10, true)]
        public float ManHourPerProduct;

        [FieldMapAttribute("PRODUCEEFFICIENCY", typeof(float), 10, true)]
        public float ProduceEfficiency;

        [FieldMapAttribute("ACQUIREDMANHOUR", typeof(float), 10, true)]
        public float AcquiredManHour;

        [FieldMapAttribute("ExceptionCode", typeof(string), 40, true)]
        public string ExceptionCode;

        [FieldMapAttribute("ExceptionDesc", typeof(string), 100, true)]
        public string ExceptionDesc;

        [FieldMapAttribute("LOSTMANHOUR", typeof(float), 10, true)]
        public float LostManHour;

        [FieldMapAttribute("LOSTMANHOURPERCENT", typeof(float), 10, true)]
        public float LostManHourPercent;

        [FieldMapAttribute("InspectorAndName", typeof(string), 100, true)]
        public string InspectorAndName;

        [FieldMapAttribute("QUALIFIEDQTY", typeof(float), 10, true)]
        public float QualifiedQty;

        [FieldMapAttribute("UNQUALIFIEDQTY", typeof(float), 10, true)]
        public float UNQualifiedQty;

        [FieldMapAttribute("RPTGOODQTY", typeof(float), 10, true)]
        public float RptGoodQty;

        [FieldMapAttribute("RPTNGQty", typeof(float), 10, true)]
        public float RptNGQty;

        [FieldMapAttribute("ALLQTY", typeof(float), 10, true)]
        public float AllQty;

        [FieldMapAttribute("ALLIQCQTY", typeof(float), 10, true)]
        public float AllIQCQty;

        [FieldMapAttribute("STSLOTQTY", typeof(float), 10, true)]
        public float STSLotQTY;

        [FieldMapAttribute("QUALIFIEDLOTQTY", typeof(float), 10, true)]
        public float QualifiedLotQTY;

        [FieldMapAttribute("UNQUALIFIEDRATEPPM", typeof(float), 10, true)]
        public float UnqualifiedRatePPM;

        [FieldMapAttribute("UNQUALIFIEDRATE", typeof(float), 10, true)]
        public float UnqualifiedRate;

        [FieldMapAttribute("QUALIFIEDRATE", typeof(float), 10, true)]
        public float QualifiedRate;

        [FieldMapAttribute("IQCLINEITEMTYPE", typeof(string), 40, true)]
        public string IQCLineItemType;

        [FieldMapAttribute("IQCITEMTYPE", typeof(string), 40, true)]
        public string IQCItemType;

        [FieldMapAttribute("CONCESSIONSTATUS", typeof(string), 40, true)]
        public string ConcessionStatus;

        [FieldMapAttribute("ROHS", typeof(string), 40, true)]
        public string Rohs;

        [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
        public string VendorCode;        
        [FieldMap("tsuser", typeof(String), 40, true)]
        public string TsOperator;
        [FieldMap("tsuserhidden", typeof(String), 40, true)]
        public string TsOperatorHidden;
        [FieldMap("tsqty", typeof(Int32), 10, true)]
        public int TsQuantity;
        [FieldMapAttribute("SSDESC", typeof(string), 100, false)]
        public string StepSequenceDescription;

        //以下存放临时数据
        [FieldMapAttribute("TEMPVALUE", typeof(string), 40, true)]
        public string TempValue;
    }

    #endregion

    #region StepSequence
    /// <summary>
    /// 工序组
    /// </summary>
    [Serializable, TableMap("TBLSS", "SSCODE")]
    public class StepSequence : DomainObject
    {
        public StepSequence()
        {
        }

        /// <summary>
        /// 工序组描述
        /// </summary>
        [FieldMapAttribute("SSDESC", typeof(string), 100, false)]
        public string StepSequenceDescription;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 工序组代码
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "", "", "SSDESC")]
        public string StepSequenceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 工段代码
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLSEG", "SEGCODE", "SEGDESC")]
        public string SegmentCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SSSEQ", typeof(decimal), 10, true)]
        public decimal StepSequenceOrder;

        /// <summary>
        /// 组织编号
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, true)]
        public int OrganizationID;

        /// <summary>
        /// 班制代码
        /// </summary>
        [FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLSHIFTTYPE", "SHIFTTYPECODE", "SHIFTTYPEDESC")]
        public string ShiftTypeCode;

        /// <summary>
        /// Step Sequence Type
        /// </summary>
        [FieldMapAttribute("SSTYPE", typeof(string), 40, false)]
        public string StepSequenceType;

        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, false)]
        public string BigStepSequenceCode;

        [FieldMapAttribute("SAVEINSTOCK", typeof(string), 40, false)]
        public string SaveInStock;
    }
    #endregion

    #region Segment
    /// <summary>
    /// 工段
    /// </summary>
    [Serializable, TableMap("TBLSEG", "SEGCODE")]
    public class Segment : DomainObject
    {
        public Segment()
        {
        }

        /// <summary>
        /// 工段描述
        /// </summary>
        [FieldMapAttribute("SEGDESC", typeof(string), 100, false)]
        public string SegmentDescription;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 工段代码
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string SegmentCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEGSEQ", typeof(decimal), 10, true)]
        public decimal SegmentSequence;

        /// <summary>
        /// 班制代码
        /// </summary>
        [FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLSHIFTTYPE", "SHIFTTYPECODE", "SHIFTTYPEDESC")]
        public string ShiftTypeCode;

        /// <summary>
        /// 组织编号
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, true)]
        public int OrganizationID;

        /// <summary>
        /// Factory Code
        /// </summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLFACTORY", "FACCODE", "FACDESC")]
        public string FactoryCode;
    }
    #endregion

    #region TimePeriod
    /// <summary>
    /// 时间段
    /// </summary>
    [Serializable, TableMap("TBLTP", "TPCODE")]
    public class TimePeriod : DomainObject
    {
        public TimePeriod()
        {
        }

        /// <summary>
        /// 时间段的开始时间
        /// </summary>
        [FieldMapAttribute("TPBTIME", typeof(int), 6, true)]
        public int TimePeriodBeginTime;

        /// <summary>
        /// 时间段的结束时间
        /// </summary>
        [FieldMapAttribute("TPETIME", typeof(int), 6, true)]
        public int TimePeriodEndTime;

        /// <summary>
        /// 最后系统用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 是否是跨日期
        /// 1:跨
        /// 0:不跨
        /// </summary>
        [FieldMapAttribute("ISOVERDATE", typeof(string), 1, true)]
        public string IsOverDate;

        /// <summary>
        /// 时间段序列
        /// </summary>
        [FieldMapAttribute("TPSEQ", typeof(int), 6, true)]
        public int TimePeriodSequence;

        /// <summary>
        /// 时间段代码
        /// </summary>
        [FieldMapAttribute("TPCODE", typeof(string), 40, true)]
        public string TimePeriodCode;

        /// <summary>
        /// 时间段描述
        /// </summary>
        [FieldMapAttribute("TPDESC", typeof(string), 100, false)]
        public string TimePeriodDescription;

        /// <summary>
        /// Normal /Exception
        /// </summary>
        [FieldMapAttribute("TPTYPE", typeof(string), 40, true)]
        public string TimePeriodType;

        /// <summary>
        /// 班次代码
        /// </summary>
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLSHIFT", "SHIFTCODE", "SHIFTDESC")]
        public string ShiftCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, true)]
        public string ShiftTypeCode;

    }
    #endregion

    #region Equipment
    [Serializable, TableMap("TBLEQUIPMENT", "EQPID")]
    public class Equipment : DomainObject
    {

        /// <summary>
        /// 设备ID
        /// </summary>
        [FieldMapAttribute("EQPID", typeof(string), 40, false)]
        public string EqpId;

        /// <summary>
        /// 品牌
        /// </summary>
        [FieldMapAttribute("Model", typeof(string), 40, true)]
        public string Model;

        /// <summary>
        /// 设备类型：系统参数维护
        /// </summary>
        [FieldMapAttribute("Type", typeof(string), 40, true)]
        public string Type;

        /// <summary>
        /// 设备描述
        /// </summary>
        [FieldMapAttribute("EQPDESC", typeof(string), 100, true)]
        public string EqpDesc;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;
    }
    #endregion

    #region EQPDATA
    /// <summary>
    /// TBLEQPDATA
    /// </summary>
    [Serializable, TableMap("TBLEQPDATA", "SERIAL")]
    public class EQPData : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public EQPData()
        {
        }

        ///<summary>
        ///SERIAL
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///EQPID
        ///</summary>
        [FieldMapAttribute("EQPID", typeof(string), 40, false)]
        public string EQPID;

        ///<summary>
        ///EVENTID
        ///</summary>
        [FieldMapAttribute("EVENTID", typeof(string), 100, true)]
        public string EventID;

        ///<summary>
        ///EVENTNAME
        ///</summary>
        [FieldMapAttribute("EVENTNAME", typeof(string), 40, true)]
        public string EventName;

        ///<summary>
        ///SETVALUEMAX
        ///</summary>
        [FieldMapAttribute("SETVALUEMAX", typeof(string), 40, true)]
        public string SetValueMax;

        ///<summary>
        ///SETVALUEMIN
        ///</summary>
        [FieldMapAttribute("SETVALUEMIN", typeof(string), 40, true)]
        public string SetValueMin;

        ///<summary>
        ///REALVALUE
        ///</summary>
        [FieldMapAttribute("REALVALUE", typeof(string), 40, true)]
        public string RealValue;

        ///<summary>
        ///UNIT
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MTime;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MDate;

    }
    #endregion

    #region Parameter 参数
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLSYSPARAM", "PARAMCODE,PARAMGROUPCODE")]
    public class Parameter : DomainObject
    {
        public Parameter()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PARAMCODE", typeof(string), 40, true)]
        public string ParameterCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PARAMDESC", typeof(string), 100, false)]
        public string ParameterDescription;

        /// <summary>
        /// 1 -  使用中
        /// 0 -  正常
        /// 
        /// </summary>
        [FieldMapAttribute("ISACTIVE", typeof(string), 1, true)]
        public string IsActive;

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
        /// 1  系统
        /// 0  用户
        /// </summary>
        [FieldMapAttribute("ISSYS", typeof(string), 1, true)]
        public string IsSystem;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PARAMALIAS", typeof(string), 40, false)]
        public string ParameterAlias;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PARAMVALUE", typeof(string), 100, false)]
        public string ParameterValue;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PARAMGROUPCODE", typeof(string), 40, true)]
        public string ParameterGroupCode;

        /// <summary>
        /// sequence 参数顺序
        /// </summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string ParameterSequence;

        /// <summary>
        /// 父级参数代码
        /// </summary>
        [FieldMapAttribute("PARENTPARAM", typeof(string), 40, false)]
        public string ParentParameterCode;

    }
    #endregion

    #region WatchPanelProductDate
    [Serializable]
    public class WatchPanelProductDate : DomainObject
    {
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;       
        [FieldMapAttribute("ITEMNAME", typeof(string), 40, true)]
        public string ItemName;       
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

    #region ExceptionEventWithDescription
    public class ExceptionEventWithDescription : ExceptionEvent
    {
        [FieldMapAttribute("EXCEPTIONDESC", typeof(string), 200, false)]
        public string Description;

        [FieldMapAttribute("SSDESC", typeof(string), 100, true)]
        public string SSDesc;
    }
    #endregion

    #region DBTimeDimension
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLTIMEDIMENSION", "DDATE")]
    public class DBTimeDimension : DomainObject
    {
        public DBTimeDimension()
        {
        }

        [FieldMapAttribute("DDATE", typeof(int), 8, false)]
        public int Date;

        [FieldMapAttribute("DWEEK", typeof(int), 8, false)]
        public int Week;

        [FieldMapAttribute("DMONTH", typeof(int), 8, false)]
        public int Month;

        [FieldMapAttribute("DQUARTER", typeof(int), 8, false)]
        public int Quarter;

        [FieldMapAttribute("YEAR", typeof(int), 8, false)]
        public int Year;

    }
    #endregion

    #region  PanelDetailsData
    [Serializable]
    public class PanelDetailsData : DomainObject
    {
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MoCode;

        [FieldMapAttribute("SHIFTDAY", typeof(string), 40, true)]
        public string ShiftDay;

        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string SSCode;

        [FieldMapAttribute("ORDERNO", typeof(string), 40, true)]
        public string OrderNo;

        [FieldMapAttribute("ITEMNAME", typeof(string), 40, true)]
        public string ItemName;

        [FieldMapAttribute("MOPLAYQTY", typeof(decimal), 22, true)]
        public decimal MoPlanQty;

        [FieldMapAttribute("MOINPUTQTY", typeof(decimal), 22, true)]
        public decimal MoInputOty;

        [FieldMapAttribute("PASSYIELD", typeof(decimal), 22, true)]
        public decimal PassYield;

        [FieldMapAttribute("MOOUTQTY", typeof(decimal), 22, true)]
        public decimal MoOutQty;

        [FieldMapAttribute("ACHIEVEMENTRATE", typeof(decimal), 22, true)]
        public decimal AchievementRate;
    }
    #endregion 
   
}
