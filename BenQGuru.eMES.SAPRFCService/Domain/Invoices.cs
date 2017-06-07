using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenQGuru.eMES.SAPRFCService.Domain
{
    public class DN
    {
        public string BatchNO { get; set; }
        public string DNNO { get; set; }
        public int DNLine { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
    }

    public class PO
    {
        public string PONO { get; set; }
        public int POLine { get; set; }
        public string FacCode { get; set; }
        public string SerialNO { get; set; }
        public string MCode { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public string Status { get; set; }
        public string SAPMaterialInvoice { get; set; }
        public string Operator { get; set; }
        public string VendorInvoice { get; set; }
        public string StorageCode { get; set; }
        public string Remark { get; set; }
        public int InvoiceDate { get; set; }
        public string ZNUMBER { get; set; }
    }

    public class UB
    {
        /// <summary>
        /// 调拨单号
        /// </summary>
        public string UBNO { get; set; }
        /// <summary>
        /// 行项目号
        /// </summary>
        public int UBLine { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string FacCode { get; set; }
        /// <summary>
        /// 出/入标识 351=出；101=入
        /// </summary>
        public string InOutFlag { get; set; }
        /// <summary>
        /// 物料编号
        /// </summary>
        public string MCode { get; set; }
        /// <summary>
        /// 库位
        /// </summary>
        public string StorageCode { get; set; }
        /// <summary>
        /// 调拨数量
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 凭证日期
        /// </summary>
        public int DocumentDate { get; set; }
        /// <summary>
        /// MES交易号
        /// </summary>
        public string MesTransNO { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactUser { get; set; }
    }

    public class RS
    {
        /// <summary>
        /// 预留单号
        /// </summary>
        public string RSNO { get; set; }
        /// <summary>
        /// 预留单行号
        /// </summary>
        public int RSLine { get; set; }
        /// <summary>
        /// 出/入标识 201=出库记入CC,202=从CC入库，241=出库记入固定资产
        /// </summary>
        public string InOutFlag { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string FacCode { get; set; }
        /// <summary>
        /// 物料编号
        /// </summary>
        public string MCode { get; set; }
        /// <summary>
        /// 库位
        /// </summary>
        public string StorageCode { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// MES交易号
        /// </summary>
        public string MesTransNO { get; set; }
        /// <summary>
        /// 凭证日期
        /// </summary>
        public int DocumentDate { get; set; }
    }

    public class WWPOComponentPara
    {
        /// <summary>
        /// PO号
        /// </summary>
        public string PONO { get; set; }
        /// <summary>
        /// 行项目号
        /// </summary>
        public int POLine { get; set; }
    }
    public class WWPOComponent
    {
        /// <summary>
        /// PO号
        /// </summary>
        public string PONO { get; set; }
        /// <summary>
        /// 行项目号
        /// </summary>
        public int POLine { get; set; }
        /// <summary>
        /// 子件行项目号
        /// </summary>
        public int SubLine { get; set; }
        /// <summary>
        /// 物料号
        /// </summary>
        public string MCode { get; set; }
        /// <summary>
        /// 华为物料号
        /// </summary>
        public string HWMCode { get; set; }
        /// <summary>
        /// 数量（子件）
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
    }
    public class WWPOComponentResult
    {
        public List<WWPOComponent> WWPOComponentList { get; set; }
        public SAPRfcReturn RfcResult { get; set; }
    }
    public class WWPO
    {
        /// <summary>
        /// PO号
        /// </summary>
        public string PONO { get; set; }
        /// <summary>
        /// 行项目号
        /// </summary>
        public int POLine { get; set; }
        /// <summary>
        /// 供应商代码
        /// </summary>
        public string VendorCode { get; set; }
        /// <summary>
        /// 物料号
        /// </summary>
        public string MCode { get; set; }
        /// <summary>
        /// 出/入标识 541=领料出库，542=退料入库
        /// </summary>
        public string InOutFlag { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 工厂 默认值10Y2
        /// </summary>
        public string FacCode { get; set; }
        /// <summary>
        /// 库位
        /// </summary>
        public string StorageCode { get; set; }
        /// <summary>
        /// MES领料/退料单号
        /// </summary>
        public string MesTransNO { get; set; }
        /// <summary>
        /// MES领料/退料日期
        /// </summary>
        public int MesTransDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    public class StockScrap
    {
        public string MESScrapNO { get; set; }
        public int LineNO { get; set; }
        public string MCode { get; set; }
        /// <summary>
        /// 551=报废
        /// </summary>
        public string ScrapCode { get; set; }
        public string FacCode { get; set; }
        public string StorageCode { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public string CC { get; set; }
        public string Remark { get; set; }
        public int DocumentDate { get; set; }
        public string Operator { get; set; }
    }

}
