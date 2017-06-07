using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for Report
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
/// ** 日 期:		2005-6-13 10:53:20
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.Report
{

    #region ReportHistoryOPQty
    /// <summary>
    ///  实时报表- 工序产量统计
    /// </summary>
    [Serializable, TableMap("TBLRPTHISOPQTY", "MODELCODE,SHIFTDAY,MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,OPCODE,RESCODE,QTYFLAG")]
    public class ReportHistoryOPQty : DomainObject
    {
        public ReportHistoryOPQty()
        {
        }

        /// <summary>
        /// 机种代码
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 产出总数量
        /// </summary>
        [FieldMapAttribute("OUTPUTQTY", typeof(decimal), 10, true)]
        public decimal OuputQty;

        /// <summary>
        /// 工厂日
        /// </summary>
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, true)]
        public int ShiftDay;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(decimal), 10, true)]
        public decimal EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute2", typeof(decimal), 10, true)]
        public decimal EAttribute2;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute3", typeof(decimal), 10, true)]
        public decimal EAttribute3;

        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 时间段代码
        /// </summary>
        [FieldMapAttribute("TPCODE", typeof(string), 40, true)]
        public string TimePeriodCode;

        /// <summary>
        /// 产线代码
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string StepSequenceCode;

        /// <summary>
        /// 工段代码
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string SegmnetCode;

        /// <summary>
        /// 料号
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 班次代码
        /// </summary>
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        public string ShiftCode;

        /// <summary>
        /// 工序代码
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        /// <summary>
        /// 资源代码
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string ResourceCode;

        /// <summary>
        /// 直通数量
        /// </summary>
        [FieldMapAttribute("ALLGOODQTY", typeof(decimal), 10, true)]
        public decimal AllGoodQty;

        /// <summary>
        /// 不良次数
        /// </summary>
        [FieldMapAttribute("NGTIMES", typeof(decimal), 10, true)]
        public decimal NGTimes;

        /// <summary>
        /// 日期的天
        /// </summary>
        [FieldMapAttribute("DAY", typeof(int), 8, true)]
        public int Day;

        /// <summary>
        /// 日期的周
        /// </summary>
        [FieldMapAttribute("WEEK", typeof(decimal), 10, true)]
        public decimal Week;

        /// <summary>
        /// 区分是工单产量
        /// </summary>
        [FieldMapAttribute("QtyFlag", typeof(string), 1, true)]
        public string QtyFlag;

        /// <summary>
        /// 日期的月
        /// </summary>
        [FieldMapAttribute("Month", typeof(decimal), 10, true)]
        public decimal Month;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LastUpdateTime", typeof(int), 6, false)]
        public int LastUpdateTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECG2EC", typeof(string), 2000, false)]
        public string ErrorGroup2Err;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPSEQ", typeof(int), 8, true)]
        public int OPSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPCONTROL", typeof(string), 40, false)]
        public string OPControl;

    }
    #endregion

    #region ReportRealtimeLineErrorCauseQty
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLRPTREALLINEECEQTY", "MODELCODE,SHIFTDAY,MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,ECCODE")]
    public class ReportRealtimeLineErrorCauseQty : DomainObject
    {
        public ReportRealtimeLineErrorCauseQty()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECTIMES", typeof(decimal), 10, true)]
        public decimal ErrorCauseTimes;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, true)]
        public int ShiftDay;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(decimal), 10, true)]
        public decimal EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute2", typeof(decimal), 10, true)]
        public decimal EAttribute2;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute3", typeof(decimal), 10, true)]
        public decimal EAttribute3;

        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 时间段代码
        /// </summary>
        [FieldMapAttribute("TPCODE", typeof(string), 40, true)]
        public string TimePeriodCode;

        /// <summary>
        /// 工序组代码
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string StepSequenceCode;

        /// <summary>
        /// 工段代码
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string SegmnetCode;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 班次代码
        /// </summary>
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        public string ShiftCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECCODE", typeof(string), 40, true)]
        public string ErrorCause;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DAY", typeof(int), 8, true)]
        public int Day;

    }
    #endregion

    #region ReportRealtimeLineErrorCodeQty
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLRPTREALLINEECQTY", "MODELCODE,SHIFTDAY,MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,ECODE,ECGCODE")]
    public class ReportRealtimeLineErrorCodeQty : DomainObject
    {
        public ReportRealtimeLineErrorCodeQty()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECTIMES", typeof(decimal), 10, true)]
        public decimal ErrorCodeTimes;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, true)]
        public int ShiftDay;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(decimal), 10, true)]
        public decimal EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute2", typeof(decimal), 10, true)]
        public decimal EAttribute2;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute3", typeof(decimal), 10, true)]
        public decimal EAttribute3;

        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 时间段代码
        /// </summary>
        [FieldMapAttribute("TPCODE", typeof(string), 40, true)]
        public string TimePeriodCode;

        /// <summary>
        /// 工序组代码
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string StepSequenceCode;

        /// <summary>
        /// 工段代码
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string SegmnetCode;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 班次代码
        /// </summary>
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        public string ShiftCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECODE", typeof(string), 40, true)]
        public string ErrorCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECGCODE", typeof(string), 10, true)]
        public string ErrorCodeGroup;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DAY", typeof(int), 8, true)]
        public int Day;

    }
    #endregion

    #region ReportRealtimeLineErrorLocationQty
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLRPTREALLINEELQTY", "MODELCODE,SHIFTDAY,MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,ELOC")]
    public class ReportRealtimeLineErrorLocationQty : DomainObject
    {
        public ReportRealtimeLineErrorLocationQty()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ELOCTIMES", typeof(string), 1, true)]
        public string ErrorLoactionTimes;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, true)]
        public int ShiftDay;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(decimal), 10, true)]
        public decimal EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute2", typeof(decimal), 10, true)]
        public decimal EAttribute2;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute3", typeof(decimal), 10, true)]
        public decimal EAttribute3;

        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 时间段代码
        /// </summary>
        [FieldMapAttribute("TPCODE", typeof(string), 40, true)]
        public string TimePeriodCode;

        /// <summary>
        /// 工序组代码
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string StepSequenceCode;

        /// <summary>
        /// 工段代码
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string SegmnetCode;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 班次代码
        /// </summary>
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        public string ShiftCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ELOC", typeof(string), 40, true)]
        public string ErrorLoaction;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DAY", typeof(int), 8, true)]
        public int Day;

    }
    #endregion

    #region ReportRealtimeLineErrorPartQty
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLRPTREALLINEEPQTY", "MODELCODE,SHIFTDAY,MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,EPART")]
    public class ReportRealtimeLineErrorPartQty : DomainObject
    {
        public ReportRealtimeLineErrorPartQty()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EPTIMES", typeof(decimal), 10, true)]
        public decimal ErrorPartTimes;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, true)]
        public int ShiftDay;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(decimal), 10, true)]
        public decimal EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute2", typeof(decimal), 10, true)]
        public decimal EAttribute2;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute3", typeof(decimal), 10, true)]
        public decimal EAttribute3;

        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 时间段代码
        /// </summary>
        [FieldMapAttribute("TPCODE", typeof(string), 40, true)]
        public string TimePeriodCode;

        /// <summary>
        /// 工序组代码
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string StepSequenceCode;

        /// <summary>
        /// 工段代码
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string SegmnetCode;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 班次代码
        /// </summary>
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        public string ShiftCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EPART", typeof(string), 40, true)]
        public string ErrorPart;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DAY", typeof(int), 8, true)]
        public int Day;

    }
    #endregion

    #region ReportRealtimeLineQty
    /// <summary>
    ///  实时报表- 产线产量统计
    /// </summary>
    [Serializable, TableMap("TBLRPTREALLINEQTY", "MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,MODELCODE,SHIFTDAY,QtyFlag")]
    public class ReportRealtimeLineQty : DomainObject
    {
        public ReportRealtimeLineQty()
        {
        }

        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 时间段代码
        /// </summary>
        [FieldMapAttribute("TPCODE", typeof(string), 40, true)]
        public string TimePeriodCode;

        /// <summary>
        /// 产线代码
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string StepSequenceCode;

        /// <summary>
        /// 工段代码
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string SegmentCode;

        /// <summary>
        /// 料号
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 班次代码
        /// </summary>
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        public string ShiftCode;

        /// <summary>
        /// 机种代码
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 产出总数量
        /// </summary>
        [FieldMapAttribute("OUTPUTQTY", typeof(decimal), 10, true)]
        public decimal OuputQty;

        /// <summary>
        /// 直通数量
        /// </summary>
        [FieldMapAttribute("ALLGOODQTY", typeof(decimal), 10, true)]
        public decimal AllGoodQty;

        /// <summary>
        /// 工厂日
        /// </summary>
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, true)]
        public int ShiftDay;

        /// <summary>
        /// 工单投入量
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(decimal), 10, true)]
        public decimal EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute2", typeof(decimal), 10, true)]
        public decimal EAttribute2;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute3", typeof(decimal), 10, true)]
        public decimal EAttribute3;

        /// <summary>
        /// 不良次数
        /// </summary>
        [FieldMapAttribute("NGTIMES", typeof(decimal), 10, true)]
        public decimal NGTimes;

        /// <summary>
        /// 报废数量
        /// </summary>
        [FieldMapAttribute("SCRAPQTY", typeof(decimal), 10, true)]
        public decimal ScrapQty;

        /// <summary>
        /// 中间投入数量
        /// </summary>
        [FieldMapAttribute("INPUTQTY", typeof(decimal), 10, true)]
        public decimal InputQty;

        /// <summary>
        /// 时间段开始时间
        /// </summary>
        [FieldMapAttribute("TPBTIME", typeof(int), 6, true)]
        public int TimePeriodBeginTime;

        /// <summary>
        /// 时间段结束时间
        /// </summary>
        [FieldMapAttribute("TPETIME", typeof(int), 6, true)]
        public int TimePeriodEndTime;

        /// <summary>
        /// 日期的天
        /// </summary>
        [FieldMapAttribute("DAY", typeof(int), 8, true)]
        public int Day;

        /// <summary>
        /// 日期的周
        /// </summary>
        [FieldMapAttribute("WEEK", typeof(decimal), 10, true)]
        public decimal Week;

        /// <summary>
        /// 区分是否最后工序产量
        /// 
        /// Y：最后工序
        /// N：非最后工序
        /// </summary>
        [FieldMapAttribute("QtyFlag", typeof(string), 1, true)]
        public string QtyFlag;

        /// <summary>
        /// 日期的月
        /// </summary>
        [FieldMapAttribute("Month", typeof(decimal), 10, true)]
        public decimal Month;

        /// <summary>
        /// 工单的直通台数
        /// </summary>
        [FieldMapAttribute("MoAllGoodQty", typeof(decimal), 10, true)]
        public decimal MoAllGoodQty;

    }
    #endregion

    #region USERGROUP2ITEM
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLUSERGROUP2ITEM", "USERGROUPCODE,ITEMCODE")]
    public class USERGROUP2ITEM : DomainObject
    {
        public USERGROUP2ITEM()
        {
        }

        /// <summary>
        /// 用户组代码
        /// </summary>
        [FieldMapAttribute("USERGROUPCODE", typeof(string), 40, true)]
        public string USERGROUPCODE;

        /// <summary>
        /// 产品代码
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ITEMCODE;

        /// <summary>
        /// 维护人员
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MUSER;

        /// <summary>
        /// 维护日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MDATE;

        /// <summary>
        /// 维护时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MTIME;

        /// <summary>
        /// 是否有效
        /// </summary>
        [FieldMapAttribute("ISAVAILABLE", typeof(decimal), 10, true)]
        public decimal ISAVAILABLE;

        /// <summary>
        /// 备注
        /// </summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 100, false)]
        public string EATTRIBUTE1;

    }
    #endregion

    #region ReportErrorCode2Resource
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLRPTRESECG", "ModelCode,ItemCode,MOCode,ShiftDay,ShiftCode,TPCode,OPCode,RESCODE,SegCode,SSCode,ECGCode,ECCode")]
    public class ReportErrorCode2Resource : DomainObject
    {
        public ReportErrorCode2Resource()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ModelCode", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ItemCode", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCode", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ShiftDay", typeof(int), 8, true)]
        public int ShiftDay;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ShiftCode", typeof(string), 40, true)]
        public string ShiftCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TPCode", typeof(string), 40, true)]
        public string TPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TPBTime", typeof(int), 6, true)]
        public int TPBTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TPETime", typeof(int), 6, true)]
        public int TPETime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPCode", typeof(string), 40, true)]
        public string OPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string ResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SegCode", typeof(string), 40, true)]
        public string SegCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SSCode", typeof(string), 40, true)]
        public string SSCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECGCode", typeof(string), 40, true)]
        public string ErrorCodeGroup;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECCode", typeof(string), 40, true)]
        public string ErrorCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("Month", typeof(decimal), 10, true)]
        public decimal Month;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("Day", typeof(int), 8, true)]
        public int Day;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("Week", typeof(decimal), 10, true)]
        public decimal Week;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("NGTimes", typeof(decimal), 10, true)]
        public decimal NGTimes;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(decimal), 10, true)]
        public decimal EATTRIBUTE1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(decimal), 10, true)]
        public decimal EATTRIBUTE2;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(decimal), 10, true)]
        public decimal EATTRIBUTE3;

    }
    #endregion

    #region FactoryWeekCheck
    /// <summary>
    /// Added by melo zheng, 2006-12-12,外包厂检验周报
    /// </summary>
    [Serializable, TableMap("TBLFACTORYWEEKCHECK", "CHECKID")]
    public class FactoryWeekCheck : DomainObject
    {
        public FactoryWeekCheck()
        {
        }

        /// <summary>
        /// 检验ID
        /// </summary>
        [FieldMapAttribute("CHECKID", typeof(string), 40, true)]
        public string CheckID;

        /// <summary>
        /// 厂商ID
        /// </summary>
        [FieldMapAttribute("FACTORYID", typeof(string), 40, true)]
        public string FactoryID;

        /// <summary>
        /// 周别
        /// </summary>
        [FieldMapAttribute("WEEKNO", typeof(string), 40, true)]
        public string WeekNo;

        /// <summary>
        /// 总产量
        /// </summary>
        [FieldMapAttribute("TOTAL", typeof(decimal), 10, true)]
        public decimal Total;

        /// <summary>
        /// LRR
        /// </summary>
        [FieldMapAttribute("LRR", typeof(decimal), 10, true)]
        public decimal LRR;

    }
    #endregion

    #region TimeDimension
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLTIMEDIMENSION", "DDATE")]
    public class TimeDimension : DomainObject
    {
        public TimeDimension()
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

    #region ReportSOQty

    /// <summary>
    ///	ReportSOQty
    /// </summary>
    [Serializable, TableMap("TBLRPTSOQTY", "MOCODE,SHIFTDAY,ITEMCODE,TBLMESENTITYLIST_SERIAL")]
    public class ReportSOQty : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public ReportSOQty()
        {
        }

        ///<summary>
        ///MOCode
        ///</summary>	
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///ShiftDay
        ///</summary>	
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, false)]
        public int ShiftDay;

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///MESEntityListSerial
        ///</summary>	
        [FieldMapAttribute("TBLMESENTITYLIST_SERIAL", typeof(int), 38, false)]
        public int MESEntityListSerial;

        ///<summary>
        ///MOInputCount
        ///</summary>	
        [FieldMapAttribute("MOINPUTCOUNT", typeof(int), 22, false)]
        public int MOInputCount;

        ///<summary>
        ///MOOutputCount
        ///</summary>	
        [FieldMapAttribute("MOOUTPUTCOUNT", typeof(int), 22, false)]
        public int MOOutputCount;

        ///<summary>
        ///MOLineOutputCount
        ///</summary>	
        [FieldMapAttribute("MOLINEOUTPUTCOUNT", typeof(int), 22, false)]
        public int MOLineOutputCount;

        ///<summary>
        ///MOWhiteCardCount
        ///</summary>	
        [FieldMapAttribute("MOWHITECARDCOUNT", typeof(int), 22, false)]
        public int MOWhiteCardCount;

        ///<summary>
        ///MOOutputWhiteCardCount
        ///</summary>	
        [FieldMapAttribute("MOOUTPUTWHITECARDCOUNT", typeof(int), 22, false)]
        public int MOOutputWhiteCardCount;

        ///<summary>
        ///LineInputCount
        ///</summary>	
        [FieldMapAttribute("LINEINPUTCOUNT", typeof(int), 22, false)]
        public int LineInputCount;

        ///<summary>
        ///LineOutputCount
        ///</summary>	
        [FieldMapAttribute("LINEOUTPUTCOUNT", typeof(int), 22, false)]
        public int LineOutputCount;

        ///<summary>
        ///OPCount
        ///</summary>	
        [FieldMapAttribute("OPCOUNT", typeof(int), 22, false)]
        public int OPCount;

        ///<summary>
        ///OPWhiteCardCount
        ///</summary>	
        [FieldMapAttribute("OPWHITECARDCOUNT", typeof(int), 22, false)]
        public int OPWhiteCardCount;

        ///<summary>
        ///EAttribute1
        ///</summary>	
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string EAttribute1;

    }

    #endregion

    #region ReportOPQty

    /// <summary>
    ///	ReportOPQty
    /// </summary>
    [Serializable, TableMap("TBLRPTOPQTY", "MOCODE,SHIFTDAY,ITEMCODE,TBLMESENTITYLIST_SERIAL")]
    public class ReportOPQty : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public ReportOPQty()
        {
        }

        ///<summary>
        ///MOCode
        ///</summary>	
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///ShiftDay
        ///</summary>	
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, false)]
        public int ShiftDay;

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///MESEntityListSerial
        ///</summary>	
        [FieldMapAttribute("TBLMESENTITYLIST_SERIAL", typeof(int), 38, false)]
        public int MESEntityListSerial;

        ///<summary>
        ///InputTimeS
        ///</summary>	
        [FieldMapAttribute("INPUTTIMES", typeof(int), 22, false)]
        public int InputTimes;

        ///<summary>
        ///OutputTimeS
        ///</summary>	
        [FieldMapAttribute("OUTPUTTIMES", typeof(int), 22, false)]
        public int OutputTimes;

        ///<summary>
        ///NGTimeS
        ///</summary>	
        [FieldMapAttribute("NGTIMES", typeof(int), 22, false)]
        public int NGTimes;

        ///<summary>
        ///EAttribute1
        ///</summary>	
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string EAttribute1;

    }

    #endregion

    #region ReportLineQty

    /// <summary>
    ///	ReportLineQty
    /// </summary>
    [Serializable, TableMap("TBLRPTLINEQTY", "MOCODE,SHIFTDAY,ITEMCODE,TBLMESENTITYLIST_SERIAL")]
    public class ReportLineQty : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public ReportLineQty()
        {
        }

        ///<summary>
        ///MOCode
        ///</summary>	
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///ShiftDay
        ///</summary>	
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, false)]
        public int ShiftDay;

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///MESEntityListSerial
        ///</summary>	
        [FieldMapAttribute("TBLMESENTITYLIST_SERIAL", typeof(int), 38, false)]
        public int MESEntityListSerial;

        ///<summary>
        ///LineWhiteCardCount
        ///</summary>	
        [FieldMapAttribute("LINEWHITECARDCOUNT", typeof(int), 22, false)]
        public int LineWhiteCardCount;

        ///<summary>
        ///ResourceWhiteCardCount
        ///</summary>	
        [FieldMapAttribute("RESWHITECARDCOUNT", typeof(int), 22, false)]
        public int ResourceWhiteCardCount;

        ///<summary>
        ///EAttribute1
        ///</summary>	
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string EAttribute1;

    }

    #endregion

    #region MESEntityList

    /// <summary>
    ///	MESEntityList
    /// </summary>
    [Serializable, TableMap("TBLMESENTITYLIST", "SERIAL")]
    public class MESEntityList : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public MESEntityList()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        ///BigSSCode
        ///</summary>	
        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, false)]
        public string BigSSCode;

        ///<summary>
        ///ModelCode
        ///</summary>	
        [FieldMapAttribute("MODELCODE", typeof(string), 40, false)]
        public string ModelCode;

        ///<summary>
        ///OPCode
        ///</summary>	
        [FieldMapAttribute("OPCODE", typeof(string), 40, false)]
        public string OPCode;

        ///<summary>
        ///SegmentCode
        ///</summary>	
        [FieldMapAttribute("SEGCODE", typeof(string), 40, false)]
        public string SegmentCode;

        ///<summary>
        ///StepSequenceCode
        ///</summary>	
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string StepSequenceCode;

        ///<summary>
        ///ResourceCode
        ///</summary>	
        [FieldMapAttribute("RESCODE", typeof(string), 40, false)]
        public string ResourceCode;

        ///<summary>
        ///ShiftTypeCode
        ///</summary>	
        [FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, false)]
        public string ShiftTypeCode;

        ///<summary>
        ///ShiftCode
        ///</summary>	
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
        public string ShiftCode;

        ///<summary>
        ///TPCode
        ///</summary>	
        [FieldMapAttribute("TPCODE", typeof(string), 40, false)]
        public string TPCode;

        ///<summary>
        ///FactoryCode
        ///</summary>	
        [FieldMapAttribute("FACCODE", typeof(string), 40, false)]
        public string FactoryCode;

        ///<summary>
        ///OrganizationID
        ///</summary>	
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;

        ///<summary>
        ///EAttribute1
        ///</summary>	
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string EAttribute1;

    }

    #endregion
}