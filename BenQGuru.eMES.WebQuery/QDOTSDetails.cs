using System;
using System.Collections;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Domain.OQC;

namespace BenQGuru.eMES.WebQuery
{
    /// <summary>
    /// QDOTSDetails 的摘要说明。
    /// </summary>
    public class QDOTSDetails : DomainObject
    {
        [FieldMapAttribute("tsid", typeof(string), 40, true)]
        public string TSID;

        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RCARD;

        [FieldMapAttribute("RCARDSEQ", typeof(int), 8, true)]
        public string RCARDSEQ;

        [FieldMapAttribute("TSMEMO", typeof(string), 40, true)]
        public string Memo;

        [FieldMapAttribute("TSUSER", typeof(string), 40, true)]
        public string TsOperator;

        [FieldMapAttribute("TSDATE", typeof(int), 8, true)]
        public int TsDate;

        [FieldMapAttribute("TSTIME", typeof(int), 6, true)]
        public int TsTime;

        [FieldMapAttribute("FrmMemo", typeof(string), 100, true)]
        public string FrmMemo;

        public ArrayList ErrorCodes = new ArrayList();

        public QDOTSRecord TSRecord;
    }

    public class QDOTSDetails1 : DomainObject
    {
        [FieldMapAttribute("TSID", typeof(string), 40, false)]
        public string TSID;

        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RunningCard;

        [FieldMapAttribute("TSMEMO", typeof(string), 40, true)]
        public string Memo;

        [FieldMapAttribute("TSUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string TSOperator;

        [FieldMapAttribute("TSDATE", typeof(int), 8, true)]
        public int TSDate;

        [FieldMapAttribute("TSTIME", typeof(int), 6, true)]
        public int TSTime;

        [FieldMapAttribute("ECGCODE", typeof(string), 40, true)]
        public string ErrorCodeGroup;

        [FieldMapAttribute("ECGDESC", typeof(string), 100, false)]
        public string ErrorCodeGroupDescription;

        [FieldMapAttribute("ECODE", typeof(string), 40, true)]
        public string ErrorCode;

        [FieldMapAttribute("ECDESC", typeof(string), 100, false)]
        public string ErrorCodeDescription;

        [FieldMapAttribute("ECSCODE", typeof(string), 40, true)]
        public string ErrorCauseCode;

        [FieldMapAttribute("ECSDESC", typeof(string), 100, false)]
        public string ErrorCauseDescription;

        [FieldMapAttribute("ELOC", typeof(string), 40, true)]
        public string ErrorLocation;

        [FieldMapAttribute("EPART", typeof(string), 40, true)]
        public string ErrorParts;

        [FieldMapAttribute("SOLMEMO", typeof(string), 40, true)]
        public string Solution;

        [FieldMapAttribute("DUTYCODE", typeof(string), 40, true)]
        public string Duty;

        [FieldMapAttribute("DUTYDESC", typeof(string), 100, false)]
        public string DutyDescription;

        [FieldMapAttribute("SOLDESC", typeof(string), 100, false)]
        public string SolutionDescription;

        [FieldMapAttribute("FrmMemo", typeof(string), 100, false)]
        public string FrmMemo;

        [FieldMapAttribute("ERRORCOMPONENT", typeof(string), 40, true)]
        public string ErrorComponent;

        [FieldMapAttribute("ECSGCODE", typeof(string), 40, true)]
        public string ErrorCauseGroupCode;

        [FieldMapAttribute("SOLCODE", typeof(string), 40, true)]
        public string SolutionCode;

        public static object[] ToArray(QDOTSDetails details)
        {
            ArrayList array = new ArrayList();

            foreach (QDOTSErrorCode errorCode in details.ErrorCodes)
            {
                if (errorCode.ErrorCauses == null || errorCode.ErrorCauses.Count < 1)
                {
                    QDOTSDetails1 obj = new QDOTSDetails1();
                    obj.TSID = errorCode.TSId;
                    obj.RunningCard = errorCode.RunningCard;
                    obj.ErrorCode = errorCode.ErrorCode;
                    obj.ErrorCodeDescription = errorCode.ErrorCodeDescription;

                    obj.ErrorCodeGroup = errorCode.ErrorCodeGroup;
                    obj.ErrorCodeGroupDescription = errorCode.ErrorCodeGroupDescription;

                    obj.TSOperator = details.TsOperator;
                    obj.TSDate = details.TsDate;
                    obj.TSTime = details.TsTime;
                    //Laws Lu,2006/06/13 add ,display tsmemo & frmmemo
                    obj.FrmMemo = details.FrmMemo;
                  
                    array.Add(obj);
                }
                else
                {
                    foreach (QDOTSErrorCause errorCause in errorCode.ErrorCauses)
                    {
                        QDOTSDetails1 obj = new QDOTSDetails1();
                        obj.TSID = errorCause.TSId;
                        obj.RunningCard = errorCode.RunningCard;
                        obj.Memo = errorCause.SolutionMEMO;
                        obj.SolutionCode = errorCause.SolutionCode;
                        obj.SolutionDescription = errorCause.SolutionDescription;

                        obj.Duty = errorCause.DutyCode;
                        obj.DutyDescription = errorCause.DutyDescription;

                        obj.TSOperator = errorCause.MaintainUser;
                        obj.TSDate = errorCause.MaintainDate;
                        obj.TSTime = errorCause.MaintainTime;

                        obj.ErrorCode = errorCode.ErrorCode;
                        obj.ErrorCodeDescription = errorCode.ErrorCodeDescription;

                        obj.ErrorCodeGroup = errorCode.ErrorCodeGroup;
                        obj.ErrorCodeGroupDescription = errorCode.ErrorCodeGroupDescription;

                        obj.ErrorCauseCode = errorCause.ErrorCauseCode;
                        obj.ErrorCauseDescription = errorCause.ErrorCauseDescription;

                        obj.Solution = errorCause.SolutionMEMO;
                        obj.SolutionCode = errorCause.SolutionCode;

                        //Laws Lu,2006/06/13 add ,display tsmemo & frmmemo
                        obj.FrmMemo = details.FrmMemo;
                        obj.ErrorComponent = errorCause.ErrorComponent;

                        obj.ErrorCauseGroupCode = errorCause.ErrorCauseGroupCode;
                        obj.ErrorComponent = errorCause.ErrorComponent;

                        //if (errorCause.ErrorPartsList != null)
                        //{
                        //    foreach (TSErrorCause2ErrorPart errorPart in errorCause.ErrorPartsList)
                        //    {
                        //        obj.ErrorParts += errorPart.ErrorPart + ",";
                        //    }
                        //    if (obj.ErrorParts != null &&
                        //        obj.ErrorParts.Length > 0)
                        //    {
                        //        obj.ErrorParts = obj.ErrorParts.Substring(0, obj.ErrorParts.Length - 1);
                        //    }
                        //}

                        if (errorCause.LocationList != null)
                        {
                            foreach (TSErrorCause2Location errorLocation in errorCause.LocationList)
                            {
                                obj.ErrorLocation += errorLocation.ErrorLocation +  ",";
                                obj.ErrorParts += errorLocation.ErrorPart + ",";
                            }
                            if (obj.ErrorLocation != null &&
                                obj.ErrorLocation.Length > 0)
                            {
                                obj.ErrorLocation = obj.ErrorLocation.Substring(0, obj.ErrorLocation.Length - 1);
                            }
                            if (obj.ErrorParts != null &&
                                obj.ErrorParts.Length > 0)
                            {
                                obj.ErrorParts = obj.ErrorParts.Substring(0, obj.ErrorParts.Length - 1);
                            }
                        }

                        array.Add(obj);
                    }
                }
            }

            return (QDOTSDetails1[])array.ToArray(typeof(QDOTSDetails1));
        }
    }

    /// <summary>
    /// 导出维修明细,包含主档信息
    /// </summary>
    public class ExportQDOTSDetails1 : QDOTSRecord
    {
        [FieldMapAttribute("TSMEMO", typeof(string), 40, true)]
        public string Memo;

        [FieldMapAttribute("TSOperator", typeof(string), 40, true)]
        public string TSOperator;

        [FieldMapAttribute("RealTSDATE", typeof(int), 8, true)]
        public int TSDate;

        [FieldMapAttribute("RealTSTIME", typeof(int), 6, true)]
        public int TSTime;

        [FieldMapAttribute("ECGCODE", typeof(string), 40, true)]
        public string ErrorCodeGroup;

        [FieldMapAttribute("ECGDESC", typeof(string), 100, false)]
        public string ErrorCodeGroupDescription;

        [FieldMapAttribute("ECODE", typeof(string), 40, true)]
        public string ErrorCode;

        [FieldMapAttribute("ECDESC", typeof(string), 100, false)]
        public string ErrorCodeDescription;

        [FieldMapAttribute("ECSCODE", typeof(string), 40, true)]
        public string ErrorCauseCode;

        [FieldMapAttribute("ECSDESC", typeof(string), 100, false)]
        public string ErrorCauseDescription;

        [FieldMapAttribute("ECSGCODE", typeof(string), 40, true)]
        public string ErrorCauseGroupCode;

        [FieldMapAttribute("ECSGDESC", typeof(string), 100, false)]
        public string ErrorCauseGroupDescription;

        [FieldMapAttribute("ELOC", typeof(string), 40, true)]
        public string ErrorLocation;

        [FieldMapAttribute("EPART", typeof(string), 40, true)]
        public string ErrorParts;

        [FieldMapAttribute("SOLMEMO", typeof(string), 40, true)]
        public string Solution;

        [FieldMapAttribute("SOLCODE", typeof(string), 40, true)]
        public string SolutionCode;

        [FieldMapAttribute("DUTYCODE", typeof(string), 40, true)]
        public string Duty;

        [FieldMapAttribute("DUTYDESC", typeof(string), 100, false)]
        public string DutyDescription;

        [FieldMapAttribute("SOLDESC", typeof(string), 100, false)]
        public string SolutionDescription;

        //[FieldMapAttribute("MODELTYPE", typeof(string), 40, true)]
        //public string ModelType;

        //[FieldMapAttribute("BOMVERSION", typeof(string), 40, true)]
        //public string BOMVersion;
    }

    public class QDOTSErrorCode : TSErrorCode
    {
        [FieldMapAttribute("ECGDESC", typeof(string), 100, false)]
        public string ErrorCodeGroupDescription;

        [FieldMapAttribute("ECDESC", typeof(string), 100, false)]
        public string ErrorCodeDescription;

        public ArrayList ErrorCauses = new ArrayList();
    }

    public class QDOTSErrorCause : TSErrorCause
    {
        [FieldMapAttribute("ECSDESC", typeof(string), 100, false)]
        public string ErrorCauseDescription;

        [FieldMapAttribute("DUTYDESC", typeof(string), 100, false)]
        public string DutyDescription;

        [FieldMapAttribute("SOLDESC", typeof(string), 100, false)]
        public string SolutionDescription;

        [FieldMapAttribute("ERRORCOMPONENT", typeof(string), 40, true)]
        public string ErrorComponent;


        public object[] ErrorPartsList = null;

        public object[] LocationList = null;
    }

    public class QueryOQCLot : OQCLot
    {
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("AGRADETIMES", typeof(decimal), 10, true)]
        public decimal AGradeTimes;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BGGRADETIMES", typeof(decimal), 10, true)]
        public decimal BGradeTimes;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CGRADETIMES", typeof(decimal), 10, true)]
        public decimal CGradeTimes;


        [FieldMapAttribute("ZGRADETIMES", typeof(decimal), 10, true)]
        public decimal ZGradeTimes;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RESULT", typeof(string), 40, true)]
        public string Result;

        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string ItemDesc;

        [FieldMapAttribute("MMODELCODE", typeof(string), 40, true)]
        public string MaterialModelCode;

        [FieldMapAttribute("MMACHINETYPE", typeof(string), 40, true)]
        public string MaterialMachineType;

        [FieldMapAttribute("MEXPORTIMPORT", typeof(string), 40, true)]
        public string MaterialExportImport;

        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, true)]
        public string BigStepSequenceCode;

        [FieldMapAttribute("REWORKCODE", typeof(string), 40, true)]
        public string ReworkCode;

        [FieldMapAttribute("CREWCODE", typeof(string), 40, true)]
        public string CrewCode;

        //样本数
        [FieldMapAttribute("SAMPLECOUNT", typeof(decimal), 10, true)]
        public decimal SampleCount;

        //样本不良数
        [FieldMapAttribute("SAMPLENGCOUNT", typeof(decimal), 10, true)]
        public decimal SampleNGCount;

        //一交合格数
        [FieldMapAttribute("FIRSTGOODCOUNT", typeof(decimal), 10, true)]
        public decimal FirstGoodCount;


    }
    //批信息
    public class QueryOQCLotInfo : OQCLot2Card
    {

    }

    //送检批一交合格数
    public class OQCLotFirstHandingYield : DomainObject
    {

        /// <summary>
        /// 批号
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LOTNO;


        /// <summary>
        /// 样本数
        /// </summary>
        [FieldMapAttribute("samplecount", typeof(decimal), 10, true)]
        public decimal SampleCount;

        /// <summary>
        /// 样本不良数
        /// </summary>
        [FieldMapAttribute("ngcount", typeof(decimal), 10, true)]
        public decimal SampleNGCount;

    }

    //送检批导出 产品明细
    public class ExportQueryOQCLotDetails : QueryOQCLot
    {
        /// <summary>
        /// MNID
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 序号
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, false)]
        public decimal RunningCardSequence;



        /// <summary>
        /// 时间段代码
        /// </summary>
        [FieldMapAttribute("TPCODE", typeof(string), 40, true)]
        public string TimePeriodCode;

        /// <summary>
        /// 班次代码
        /// </summary>
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        public string ShiftCode;

        /// <summary>
        /// 班制代码
        /// </summary>
        [FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, true)]
        public string ShiftTypeCode;

        /// <summary>
        /// 资源代码
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string ResourceCode;



        /// <summary>
        /// 生产途程代码
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string RouteCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;


        /// <summary>
        /// PASS
        /// REJECT
        /// REWORK
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string Status;


        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CollectType", typeof(string), 20, false)]
        public string CollectType;
    }

    //送检批导出 样本不良明细
    public class ExportQueryOQCLotSampleDetails : QueryOQCLot
    {
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RunningCard;

        [FieldMapAttribute("TSMEMO", typeof(string), 40, true)]
        public string Memo;

        [FieldMapAttribute("TSUSER", typeof(string), 40, true)]
        public string TSOperator;

        [FieldMapAttribute("TSDATE", typeof(int), 8, true)]
        public int TSDate;

        [FieldMapAttribute("TSTIME", typeof(int), 6, true)]
        public int TSTime;

        [FieldMapAttribute("ECGCODE", typeof(string), 40, true)]
        public string ErrorCodeGroup;

        [FieldMapAttribute("ECGDESC", typeof(string), 100, false)]
        public string ErrorCodeGroupDescription;

        [FieldMapAttribute("ECODE", typeof(string), 40, true)]
        public string ErrorCode;

        [FieldMapAttribute("ECDESC", typeof(string), 100, false)]
        public string ErrorCodeDescription;

        [FieldMapAttribute("ECSCODE", typeof(string), 40, true)]
        public string ErrorCauseCode;

        [FieldMapAttribute("ECSDESC", typeof(string), 100, false)]
        public string ErrorCauseDescription;

        [FieldMapAttribute("ELOC", typeof(string), 40, true)]
        public string ErrorLocation;

        [FieldMapAttribute("EPART", typeof(string), 40, true)]
        public string ErrorParts;

        [FieldMapAttribute("SOLMEMO", typeof(string), 40, true)]
        public string Solution;

        [FieldMapAttribute("DUTYCODE", typeof(string), 40, true)]
        public string Duty;

        [FieldMapAttribute("DUTYDESC", typeof(string), 100, false)]
        public string DutyDescription;

        [FieldMapAttribute("SOLDESC", typeof(string), 100, false)]
        public string SolutionDescription;

        [FieldMapAttribute("FrmMemo", typeof(string), 100, false)]
        public string FrmMemo;
    }
}
