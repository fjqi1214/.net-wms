using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenQGuru.eMES.SAPRFCService
{
    public static class MES2SAPRfcFunctionName
    {
        public const string PO = "ZCHN_MM_PO_GR_REC_ACP_REV";
        public const string PGI = "ZCHN_SD_PGI_POST";
        public const string RePGI = "ZCHN_SD_PGI_CANCEL";
        public const string UB = "ZCHN_MM_STO_INBOUND_OUTBOUND";
        public const string RS = "ZCHN_RESEARCH_PR_POST";
        /// <summary>
        /// 委外PO组件清单接口
        /// </summary>
        public const string WWPOComponent = "ZCHN_SUB_PO_COMPONT_LIST";
        /// <summary>
        /// 委外PO领料 退料接口
        /// </summary>
        public const string WWPO = "ZCHN_SUB_PO_REQ_RET";
        public const string StockScrap = "ZCHN_MM_STOCK_SCRAPPED";
    }

    /// <summary>
    /// SAP RFC接口输入参数名(Table)
    /// </summary>
    public static class MES2SAPRfcInTableName
    {
        public const string PO = "IT_MSEG";
        public const string DN = "IT_VBELN";
        public const string UB = "IT_MSEG";
        public const string RS = "IT_RESB";
        /// <summary>
        /// 委外PO组件清单接口
        /// </summary>
        public const string WWPOComponent = "IT_EKPO";
        /// <summary>
        /// 委外PO领料 退料接口
        /// </summary>
        public const string WWPO = "IT_EKPO";
        public const string StockScrap = "IT_MSEG";
    }

    /// <summary>
    /// SAP RFC接口输出参数名(Table)
    /// </summary>
    public static class MES2SAPRfcOutTableName
    {
        /// <summary>
        /// 委外PO组件清单接口
        /// </summary>
        public const string WWPOComponent = "ET_EKPO";
    }
}
