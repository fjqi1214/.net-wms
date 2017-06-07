using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncSAPJob
{
    public static class CommonConstants
    {
        public const string MaintainUser = "JOB";
        public const string InvoicesInitSatus = "Release";
        public const string InvoicesInitFinishFlag = "N";
        public const string PickInitSatus = "Release";
        public const string PickDetailInitSatus = "Release";
        public const string StorLocTransSatus = "Release";
        public const string StorLocTransDetailSatus = "Release";
    }
    public static class PickHeadStatus
    {
      public const string PickHeadStatus_Release = "Release";//初始化
        public const string PickHeadStatus_WaitPick = "WaitPick";//待拣料
        public const string PickHeadStatus_Pick = "Pick";//拣料
        public const string PickHeadStatus_MakePackingList = "MakePackingList";//制作箱单
        public const string PickHeadStatus_Pack = "Pack";//包装
        public const string PickHeadStatus_OQC = "OQC";//OQC检验
        public const string PickHeadStatus_ClosePackingList = "ClosePackingList";//箱单完成
        public const string PickHeadStatus_Close = "Close";//已出库
        public const string PickHeadStatus_Cancel = "Cancel";//取消
        public const string PickHeadStatus_Block = "Block";//冻结
   }

    public static class SAPRfcDefaultPara
    {
        public const string FacCode = "10Y2";
        public const string CompanyCode = "TD28";
        public const string PurchOrgCode = "61Y2";
        
    }

    public static class InvoicesTypes
    {
        public const string StockCheck = "PD";
    }

    public static class StorLocTransTypes
    {
        public const string ZC = "Transfer";
        public const string LocMove = "Move";
    }

    public static class StockCheckTypes
    {
        public const string Gain = "PYR";
        public const string Loss = "PKC";
    }

    public static class POHeadStatus
    {
        public const string Release = "Release";
        public const string Pending = "Pending";
        public const string Cancel = "Cancel";
    }
    public static class PODetailStatus
    {
        public const string Release = "Release";
        public const string Pending = "Pending";
        public const string Cancel = "Cancel";
    }

    public static class UBHeadStatus
    {
        public const string Release = "Release";
        public const string Pending = "Pending";
        public const string Cancel = "Cancel";
    }
    public static class UBDetailStatus
    {
        public const string Release = "Release";
        public const string Pending = "Pending";
        public const string Cancel = "Cancel";
    }

    public static class MiddleTableFlag
    {
        public const string Wait = "W";
        public const string Success = "S";
        public const string Fail = "F";
        /// <summary>
        /// 上次没有处理的记录
        /// </summary>
        public const string Last = "L";
    }

    public static class JobType
    {
        public const string Customer = "customer";
        public const string Vendor = "vendor";
        public const string Storage = "storage";
        public const string Material = "material";
        public const string PO = "po";
        public const string DN = "dn";
        public const string UB = "ub";
        public const string RS = "rs";
        public const string Stock = "stock";
        public const string StockCheck = "stockcheck";
        public const string SapLog = "saplog";
        public const string SendMail = "sendmail";
    }

    public static class SAPRfcFunctionName
    {
        public const string Customer = "ZCHN_SD_CUSTOMER_GET";
        public const string Vendor = "ZCHN_MM_VENDOR_GET";
        public const string Storage = "ZCHN_MM_LGORT_INFO";
        public const string Material = "ZCHN_MM_MATERNAL_GET";
        public const string PO = "ZCHN_MM_PO_GET";
        public const string DN = "ZCHN_SD_PCBH_GET";
        public const string UB = "ZCHN_MM_STO";
        public const string RS = "ZCHN_RESEARCH_PR_RESB";
        public const string Stock = "ZCHN_STOCK_DETAIL";
        public const string StockCheck = "ZCHN_STOCK_INVNU";
    }

    public static class SAPRfcOutTableName
    {
        public const string Customer = "IS_ITEM";
        public const string Vendor = "ET_VENDOR";
        public const string Storage = "ET_LGORT";
        public const string Material = "ET_MATERNAL";
        public const string PO = "ET_EKPO";
        public const string DN = "OT_PCBH";
        public const string UB = "ET_EKPO";
        public const string RS = "ET_RESB";
        public const string Stock = "ET_STOCK";
        public const string StockCheck = "ET_INVNU";
    }

    public static class SAPJOBTIMESTAMP
    {
        public const string PO = "POTIMESTAMP";
        public const string DN = "DNTIMESTAMP";
        public const string UB = "UBTIMESTAMP";
        public const string RS = "RSTIMESTAMP";
        public const string StockCheck = "STOCKCHECKTIMESTAMP";
    }

}
