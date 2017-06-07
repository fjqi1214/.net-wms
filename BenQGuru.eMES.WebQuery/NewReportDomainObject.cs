using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.DataCollect;

namespace BenQGuru.eMES.WebQuery
{
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

        [FieldMapAttribute("MATERIALCODE", typeof(string), 40, true)]
        public string MaterialCode;        

        [FieldMapAttribute("MMODELCODE", typeof(string), 40, true)]
        public string MaterialModelCode;

        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

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

        [FieldMapAttribute("INPUT", typeof(int), 22, true)]
        public int Input;

        [FieldMapAttribute("OUTPUT", typeof(int), 22, true)]
        public int Output;

        [FieldMapAttribute("INPUTQTY", typeof(int), 22, true)]
        public int InputQty;

        [FieldMapAttribute("OUTPUTQTY", typeof(int), 22, true)]
        public int OutputQty;

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

        [FieldMapAttribute("MAXQTY", typeof(int), 22, true)]
        public int MaxQty;

        [FieldMapAttribute("AVGQTY", typeof(float), 10, true)]
        public float AvgQty;

        [FieldMapAttribute("AGGCOUNT", typeof(int), 22, true)]
        public int AggCount;

        [FieldMapAttribute("PASSRCARDQTY", typeof(int), 22, true)]
        public int PassRcardQty;

        [FieldMapAttribute("PASSRCARDRATE", typeof(float), 10, true)]
        public float PassRcardRate;

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

        [FieldMapAttribute("ERRORCAUSE", typeof(string), 40, true)]
        public string ErrorCause;

        [FieldMapAttribute("ERRORLOCATION", typeof(string), 40, true)]
        public string ErrorLocation;

        [FieldMapAttribute("QTY", typeof(int), 10, true)]
        public int Quantity;


        [FieldMapAttribute("CUSTOMERCODE", typeof(string), 40, true)]
        public string CustomerCode;


        [FieldMapAttribute("DAY", typeof(int), 8, true)]
        public int NatureDate;
                
        [FieldMapAttribute("AllTimes", typeof(int), 10, true)]
        public long AllTimes;

        [FieldMapAttribute("YieldPercent", typeof(decimal), 10, true)]
        public decimal YieldPercent;

        [FieldMapAttribute("ALLGOODQTY", typeof(int), 10, true)]
        public long AllGoodQuantity;

        [FieldMapAttribute("AllGoodYieldPercent", typeof(decimal), 10, true)]
        public decimal AllGoodYieldPercent;

        [FieldMapAttribute("NotYieldPercent", typeof(decimal), 10, true)]
        public decimal NotYieldPercent;

        [FieldMapAttribute("NotYieldLocation", typeof(int), 10, true)]
        public long NotYieldLocation;

        [FieldMapAttribute("TotalLocation", typeof(int), 10, true)]
        public long TotalLocation;

        [FieldMapAttribute("PPM", typeof(int), 10, true)]
        public long PPM;

        //以下为OQC报表的相关部分

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


        //以下为绩效报表使用
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

        //以下存放临时数据
        [FieldMapAttribute("TEMPVALUE", typeof(string), 40, true)]
        public string TempValue;

        [FieldMapAttribute("PROJECtCODE", typeof(string), 40, true)]
        public string ProjectCode;
    }

    public class SAPMESStorageCompare : DomainObject
    {
        [FieldMapAttribute("ORGID", typeof(int), 8, true)]
        public int OrganizationID;

        [FieldMapAttribute("STORAGEID", typeof(string), 40, true)]
        public string StorageID;

        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        [FieldMapAttribute("ITEMGRADE", typeof(string), 40, true)]
        public string ItemGrade;

        [FieldMapAttribute("SAPQTY", typeof(int), 8, true)]
        public int SAPQTY;

        [FieldMapAttribute("SAPMQTY", typeof(int), 8, true)]
        public int SAPMQTY;

        [FieldMapAttribute("SAPOKQTY", typeof(int), 8, true)]
        public int SAPOKQTY;

        [FieldMapAttribute("MESQTY", typeof(int), 8, true)]
        public int MESQTY;

        [FieldMapAttribute("MNAME", typeof(string), 40, true)]
        public string MaterialName;


    }
}
