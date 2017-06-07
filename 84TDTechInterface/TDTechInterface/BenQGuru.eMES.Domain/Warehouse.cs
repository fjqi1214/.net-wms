using System;
using System.Collections.Generic;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for Warehouse
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
/// ** 日 期:		2005-7-28 16:35:54
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.Warehouse
{
    #region Factory
    /// <summary>
    /// 工厂
    /// </summary>
    [Serializable, TableMap("TBLFACTORY", "FACCODE")]
    public class Factory : DomainObject
    {
        public Factory()
        {
        }

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

        /// <summary>
        /// 工厂代码
        /// </summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string FactoryCode;

        /// <summary>
        /// 工厂描述
        /// </summary>
        [FieldMapAttribute("FACDESC", typeof(string), 100, false)]
        public string FactoryDescription;

        /// <summary>
        /// 组织编号
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, true)]
        public int OrganizationID;
    }
    #endregion

    #region MOStock
    /// <summary>
    /// 工厂用料统计
    /// </summary>
    [Serializable, TableMap("TBLMOSTOCK", "ITEMCODE,MOCODE")]
    public class MOStock : DomainObject
    {
        public MOStock()
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
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 物料号
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 已消耗未过帐的数量
        /// </summary>
        [FieldMapAttribute("ISSUEQTY", typeof(decimal), 10, true)]
        public decimal IssueQty;

        /// <summary>
        /// 报废数量
        /// </summary>
        [FieldMapAttribute("SCRAPQTY", typeof(decimal), 10, true)]
        public decimal ScrapQty;

        /// <summary>
        /// 盈亏 (记录送离线维修数量)
        /// </summary>
        [FieldMapAttribute("GAINLOSE", typeof(decimal), 10, true)]
        public decimal GainLose;

        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 计划用量
        /// </summary>
        [FieldMapAttribute("RECQTY", typeof(decimal), 10, true)]
        public decimal ReceiptQty;

        /// <summary>
        /// 良品退料数量
        /// </summary>
        [FieldMapAttribute("RETURNQTY", typeof(decimal), 10, true)]
        public decimal ReturnQty;

        /// <summary>
        /// 不良品退料数量
        /// </summary>
        [FieldMapAttribute("RETURNSCRAPQTY", typeof(decimal), 10, true)]
        public decimal ReturnScrapQty;


        /// <summary>
        /// 工单上料采集该物料的总数
        /// </summary>
        public decimal MOLoadingQty;

        /// <summary>
        /// 维修此工单不良所换上该物料的总数
        /// </summary>
        public decimal TSLoadingQty;

        /// <summary>
        /// 该物料未修完数量
        /// </summary>
        public decimal TSUnCompletedQty;

        /// <summary>
        /// 工单状态 用于判断工单损耗率和报废率计算公式用
        /// </summary>
        public string MOStatus;

    }
    #endregion

    #region TransactionType
    /// <summary>
    /// 交易类型
    /// </summary>
    [Serializable, TableMap("TBLTRANSTYPE", "TRANSTYPECODE")]
    public class TransactionType : DomainObject
    {
        public TransactionType()
        {
        }

        public class TransactionTypeMoStock
        {
            /// <summary>
            /// 交易类型代码
            /// </summary>
            public string TransactionTypeCode;
            /// <summary>
            /// 累计到的attribute
            /// </summary>
            public string AttributeName;
            /// <summary>
            /// 操作类型(add, sub)
            /// </summary>
            public string Operation;
            /// <summary>
            /// 目标仓库 (用于计算送离线维修数量)
            /// </summary>
            public string ToWarehouse;
        }
        public static TransactionTypeMoStock[] TRANSACTIONTYPE_MOSTOCK = null;
        public static System.Collections.Hashtable TRANSACTION_MAPPING = null;

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
        /// 类型描述
        /// </summary>
        [FieldMapAttribute("TRANSTYPEESC", typeof(string), 100, false)]
        public string TransactionTypeDescription;

        /// <summary>
        /// 类型代码
        /// </summary>
        [FieldMapAttribute("TRANSTYPECODE", typeof(string), 40, true)]
        public string TransactionTypeCode;

        /// <summary>
        /// 类型名称
        /// </summary>
        [FieldMapAttribute("TRANSTYPENAME", typeof(string), 40, false)]
        public string TransactionTypeName;

        /// <summary>
        /// 是否工单管控
        /// </summary>
        [FieldMapAttribute("ISBYMO", typeof(string), 1, true)]
        public string IsByMOControl;

        /// <summary>
        /// 前缀
        /// </summary>
        [FieldMapAttribute("TRANSPREFIX", typeof(string), 40, false)]
        public string TransactionPrefix;

        /// <summary>
        /// 是否初始类型，初始类型不允许删除
        /// </summary>
        [FieldMapAttribute("ISINIT", typeof(string), 1, true)]
        public string IsInit;

    }
    #endregion

    #region Warehouse

    /// <summary>
    /// 仓库
    /// </summary>
    [Serializable, TableMap("TBLWAREHOURSE", "WHCODE,FACCODE")]
    public class Warehouse : DomainObject
    {
        public Warehouse()
        {
        }

        /// <summary>
        /// 仓库类型参数组名
        /// </summary>
        public static string WarehouseTypeGroup = "WAREHOUSETYPE";

        /// <summary>
        /// 初始状态
        /// </summary>
        public const string WarehouseStatus_Initialize = "WarehouseStatus_Initialize";
        /// <summary>
        /// 正常状态
        /// </summary>
        public const string WarehouseStatus_Normal = "WarehouseStatus_Normal";
        /// <summary>
        /// 盘点状态
        /// </summary>
        public const string WarehouseStatus_Cycle = "WarehouseStatus_Cycle";
        /// <summary>
        /// 关闭状态
        /// </summary>
        public const string WarehouseStatus_Closed = "WarehouseStatus_Closed";

        /// <summary>
        /// 仓库代码
        /// </summary>
        [FieldMapAttribute("WHCODE", typeof(string), 40, true)]
        public string WarehouseCode;

        /// <summary>
        /// 仓库描述
        /// </summary>
        [FieldMapAttribute("WHDESC", typeof(string), 100, false)]
        public string WarehouseDescription;

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
        /// 仓库类型
        /// 产线/良品物料区/不良物料区/半成品区
        /// </summary>
        [FieldMapAttribute("WHTYPE", typeof(string), 40, true)]
        public string WarehouseType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        //		/// <summary>
        //		/// 工段代码
        //		/// </summary>
        //		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        //		public string  SegmentCode;

        /// <summary>
        /// 备注
        /// </summary>
        [FieldMapAttribute("MEMO", typeof(string), 100, false)]
        public string MEMO;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string FactoryCode;

        /// <summary>
        /// 最后盘点日期
        /// </summary>
        [FieldMapAttribute("LCYCLEDATE", typeof(int), 8, true)]
        public int LastCycleCountDate;

        /// <summary>
        /// 最后盘点时间
        /// </summary>
        [FieldMapAttribute("LCYCLETIME", typeof(int), 6, true)]
        public int LastCycleCountTime;

        /// <summary>
        /// 库房状态 
        /// 初始化/正常/盘点/关闭
        /// </summary>
        [FieldMapAttribute("WHSTATUS", typeof(string), 40, true)]
        public string WarehouseStatus;

        /// <summary>
        /// 最后盘点代码
        /// </summary>
        [FieldMapAttribute("LCYCLECODE", typeof(string), 40, false)]
        public string LastCycleCountCode;

        /// <summary>
        /// 工单中使用次数
        /// </summary>
        [FieldMapAttribute("USECOUNT", typeof(decimal), 10, true)]
        public decimal UseCount;

        /// <summary>
        /// 是否管控
        /// </summary>
        [FieldMapAttribute("ISCTRL", typeof(string), 1, true)]
        public string IsControl;

    }
    #endregion

    #region Warehouse2StepSequence
    /// <summary>
    /// 产线与仓库对应
    /// </summary>
    //[Serializable, TableMap("TBLWH2SSCODE", "SSCODE,WHCODE,SEGCODE,FACCODE")]
    [Serializable, TableMap("TBLWH2SSCODE", "SSCODE,WHCODE,FACCODE")]
    public class Warehouse2StepSequence : DomainObject
    {
        public Warehouse2StepSequence()
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
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 产线代码
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string StepSequenceCode;

        /// <summary>
        /// 仓库代码
        /// </summary>
        [FieldMapAttribute("WHCODE", typeof(string), 40, true)]
        public string WarehouseCode;

        /*
        /// <summary>
        /// 工段代码
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string  SegmentCode;
        */

        /// <summary>
        /// 工厂代码
        /// </summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string FactoryCode;

    }
    #endregion

    #region WarehouseCycleCount
    /// <summary>
    /// 仓库盘点主档
    /// </summary>
    [Serializable, TableMap("TBLWHCYCLE", "CYCLECODE")]
    public class WarehouseCycleCount : DomainObject
    {
        public WarehouseCycleCount()
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
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 确认人
        /// </summary>
        [FieldMapAttribute("CFMUSER", typeof(string), 40, false)]
        public string ConfirmUser;

        /// <summary>
        /// 确认日期
        /// </summary>
        [FieldMapAttribute("CFMDATE", typeof(int), 8, false)]
        public int ConfirmDate;

        /// <summary>
        /// 确认时间
        /// </summary>
        [FieldMapAttribute("CFMTIME", typeof(int), 6, true)]
        public int ConfirmTime;

        /// <summary>
        /// 盘点代码
        /// </summary>
        [FieldMapAttribute("CYCLECODE", typeof(string), 40, true)]
        public string CycleCountCode;

        /// <summary>
        /// 盘点类型
        /// 日/周/月/换班盘点
        /// 
        /// </summary>
        [FieldMapAttribute("CYCLETYPE", typeof(string), 40, true)]
        public string CycleCountType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
        public string ShiftCode;

        /// <summary>
        /// 仓库代码
        /// </summary>
        [FieldMapAttribute("WHCODE", typeof(string), 40, true)]
        public string WarehouseCode;


        //		/// <summary>
        //		/// 工段代码
        //		/// </summary>
        //		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        //		public string  SegmentCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string FactoryCode;

    }
    #endregion

    #region WarehouseCycleCountDetail
    /// <summary>
    /// 盘点明细
    /// </summary>
    [Serializable, TableMap("TBLWHCYLCEDETAIL", "ITEMCODE,CYCLECODE")]
    public class WarehouseCycleCountDetail : DomainObject
    {
        public WarehouseCycleCountDetail()
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
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 物料代码
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 期初数 (库房离散数量)
        /// </summary>
        [FieldMapAttribute("QTY", typeof(decimal), 10, true)]
        public decimal Qty;

        /// <summary>
        /// 在制数量 
        /// </summary>
        [FieldMapAttribute("LINEQTY", typeof(decimal), 10, true)]
        public decimal LineQty;

        /// <summary>
        /// 账面数 = 库房离散数量 ＋ 在制数量
        /// </summary>
        [FieldMapAttribute("WAREHOUSE2LINEQTY", typeof(decimal), 10, true)]
        public decimal Warehouse2LineQty;

        /// <summary>
        /// 盘点数
        /// </summary>
        [FieldMapAttribute("PHQTY", typeof(decimal), 10, true)]
        public decimal PhysicalQty;

        /// <summary>
        /// 调整数
        /// </summary>
        [FieldMapAttribute("ADJQTY", typeof(decimal), 10, true)]
        public decimal AdjustQty;

        /// <summary>
        /// 调整人
        /// </summary>
        [FieldMapAttribute("ADJUSER", typeof(string), 40, false)]
        public string AdjustUser;

        /// <summary>
        /// 确认人
        /// </summary>
        [FieldMapAttribute("CFMUSER", typeof(string), 40, false)]
        public string ConfirmUser;

        /// <summary>
        /// 调整日期
        /// </summary>
        [FieldMapAttribute("ADJDATE", typeof(int), 8, false)]
        public int AdjustDate;

        /// <summary>
        /// 确认日期
        /// </summary>
        [FieldMapAttribute("CFMDATE", typeof(int), 8, false)]
        public int ConfirmDate;

        /// <summary>
        /// 确认时间
        /// </summary>
        [FieldMapAttribute("CFMTIME", typeof(int), 6, true)]
        public int ConfirmTime;

        /// <summary>
        /// 调整时间
        /// </summary>
        [FieldMapAttribute("ADJTIME", typeof(int), 6, true)]
        public int AdjustTime;

        /// <summary>
        /// 盘点代码
        /// </summary>
        [FieldMapAttribute("CYCLECODE", typeof(string), 40, true)]
        public string CycleCountCode;

        /// <summary>
        /// 仓库代码
        /// </summary>
        [FieldMapAttribute("WHCODE", typeof(string), 40, true)]
        public string WarehouseCode;

        //		/// <summary>
        //		/// 工段代码
        //		/// </summary>
        //		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        //		public string  SegmentCode;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string FactoryCode;
    }
    #endregion

    #region WarehouseItem
    /// <summary>
    /// 仓库物料主档
    /// </summary>
    [Serializable, TableMap("TBLWHITEM", "ITEMCODE")]
    public class WarehouseItem : DomainObject
    {
        public WarehouseItem()
        {
        }

        /// <summary>
        /// 计量单位参数组名
        /// </summary>
        public static string WarehouseItemUOMGroup = "WAREHOUSEITEMUOM";

        public static string WarehouseItemControlType_Lot = "WarehouseItemControlType_Lot";
        public static string WarehouseItemControlType_Single = "WarehouseItemControlType_Single";

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

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
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 料品名称[ItemName]
        /// </summary>
        [FieldMapAttribute("ITEMNAME", typeof(string), 100, false)]
        public string ItemName;

        /// <summary>
        /// 计量单位[ItemUOM]
        /// </summary>
        [FieldMapAttribute("ITEMUOM", typeof(string), 40, true)]
        public string ItemUOM;

        /// <summary>
        /// 管控方式[ItemControl]
        /// </summary>
        [FieldMapAttribute("ITEMCONTROL", typeof(string), 40, false)]
        public string ItemControlType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

    }
    #endregion

    #region WarehouseStock
    /// <summary>
    /// 仓库物料
    /// </summary>
    //[Serializable, TableMap("TBLWHSTOCK", "ITEMCODE,WHCODE,SEGCODE,FACCODE")]
    [Serializable, TableMap("TBLWHSTOCK", "ITEMCODE,WHCODE,FACCODE")]
    public class WarehouseStock : DomainObject
    {
        public WarehouseStock()
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
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 物料代码
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 期初数量
        /// </summary>
        [FieldMapAttribute("OPENQTY", typeof(decimal), 10, true)]
        public decimal OpenQty;

        /// <summary>
        /// 仓库代码
        /// </summary>
        [FieldMapAttribute("WHCODE", typeof(string), 40, true)]
        public string WarehouseCode;

        //		/// <summary>
        //		/// 工段代码
        //		/// </summary>
        //		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        //		public string  SegmentCode;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string FactoryCode;

    }
    #endregion

    #region WarehouseTicket
    /// <summary>
    /// 交易单主档
    /// </summary>
    [Serializable, TableMap("TBLWHTKT", "TKTNO")]
    public class WarehouseTicket : DomainObject
    {
        public WarehouseTicket()
        {
        }

        public enum TransactionStatusEnum
        {
            Pending,
            Transaction,
            Closed,
            Deleted
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
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 交易单号
        /// </summary>
        [FieldMapAttribute("TKTNO", typeof(string), 40, true)]
        public string TicketNo;

        /// <summary>
        /// 交易类型
        /// </summary>
        [FieldMapAttribute("TRANSTYPECODE", typeof(string), 40, true)]
        public string TransactionTypeCode;

        /// <summary>
        /// 状态
        /// Open/Close
        /// </summary>
        [FieldMapAttribute("TRANSSTATUS", typeof(string), 40, true)]
        public string TransactionStatus;

        /// <summary>
        /// 交易人
        /// </summary>
        [FieldMapAttribute("TRANSUSER", typeof(string), 40, false)]
        public string TransactionUser;

        /// <summary>
        /// 交易日期
        /// </summary>
        [FieldMapAttribute("TransactionDate", typeof(int), 8, false)]
        public int TransactionDate;

        /// <summary>
        /// 交易时间
        /// </summary>
        [FieldMapAttribute("TransactionTime", typeof(int), 6, false)]
        public int TransactionTime;

        /// <summary>
        /// 开单人
        /// </summary>
        [FieldMapAttribute("TicketUser", typeof(string), 40, true)]
        public string TicketUser;

        /// <summary>
        /// 开单日期
        /// </summary>
        [FieldMapAttribute("TicketDate", typeof(int), 8, true)]
        public int TicketDate;

        /// <summary>
        /// 开单时间
        /// </summary>
        [FieldMapAttribute("TicketTime", typeof(int), 6, true)]
        public int TicketTime;

        /// <summary>
        /// 来源单号
        /// </summary>
        [FieldMapAttribute("STCKNO", typeof(string), 40, false)]
        public string SourceTicketNo;

        /// <summary>
        /// 目标仓库
        /// </summary>
        [FieldMapAttribute("TOWHCODE", typeof(string), 40, true)]
        public string TOWarehouseCode;

        //		/// <summary>
        //		/// 目标工段
        //		/// </summary>
        //		[FieldMapAttribute("TOSEGCODE", typeof(string), 40, true)]
        //		public string  TOSegmentCode;

        /// <summary>
        /// 仓库代码
        /// </summary>
        [FieldMapAttribute("WHCODE", typeof(string), 40, false)]
        public string WarehouseCode;

        //		/// <summary>
        //		/// 工段代码
        //		/// </summary>
        //		[FieldMapAttribute("SEGCODE", typeof(string), 40, false)]
        //		public string  SegmentCode;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, false)]
        public string FactoryCode;

        /// <summary>
        /// 目标工厂
        /// </summary>
        [FieldMapAttribute("TOFACCODE", typeof(string), 40, true)]
        public string TOFactoryCode;

        /// <summary>
        /// 参考单号
        /// </summary>
        [FieldMapAttribute("REFCODE", typeof(string), 40, true)]
        public string ReferenceCode;

        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

    }
    #endregion

    #region WarehouseTicketSeq
    /// <summary>
    /// 交易序列号
    /// </summary>
    [Serializable, TableMap("TBLTICKETSEQ", "NEXTSEQ")]
    public class WarehouseTicketSeq : DomainObject
    {
        public WarehouseTicketSeq()
        {
        }
        /// <summary>
        /// 交易单号
        /// </summary>
        [FieldMapAttribute("NEXTSEQ", typeof(int), 10, true)]
        public int NextSeq;

    }
    #endregion

    #region WarehouseTicketDetail
    /// <summary>
    /// 交易单明细
    /// </summary>
    [Serializable, TableMap("TBLWHTKTDETAIL", "SEQ,TKTNO")]
    public class WarehouseTicketDetail : DomainObject
    {
        public WarehouseTicketDetail()
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
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 序列号
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

        /// <summary>
        /// 交易单号
        /// </summary>
        [FieldMapAttribute("TKTNO", typeof(string), 40, true)]
        public string TicketNo;

        /// <summary>
        /// 物料代码
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 数量
        /// </summary>
        [FieldMapAttribute("QTY", typeof(decimal), 15, false)]
        public decimal Qty;

        /// <summary>
        /// 实际数量
        /// </summary>
        [FieldMapAttribute("ACTQTY", typeof(decimal), 15, false)]
        public decimal ActualQty;

        /// <summary>
        /// 交易状态
        /// Open/Close
        /// </summary>
        [FieldMapAttribute("TRANSSTATUS", typeof(string), 40, false)]
        public string TransactionStatus;

        /// <summary>
        /// 交易人
        /// </summary>
        [FieldMapAttribute("TRANSUSER", typeof(string), 40, false)]
        public string TransactionUser;

        /// <summary>
        /// 交易日期
        /// </summary>
        [FieldMapAttribute("TransactionDate", typeof(int), 8, false)]
        public int TransactionDate;

        /// <summary>
        /// 交易时间
        /// </summary>
        [FieldMapAttribute("TransactionTime", typeof(int), 6, false)]
        public int TransactionTime;

        /// <summary>
        /// 开单人
        /// </summary>
        [FieldMapAttribute("TicketUser", typeof(string), 40, false)]
        public string TicketUser;

        /// <summary>
        /// 开单日期
        /// </summary>
        [FieldMapAttribute("TicketDate", typeof(int), 8, false)]
        public int TicketDate;

        /// <summary>
        /// 开单时间
        /// </summary>
        [FieldMapAttribute("TicketTime", typeof(int), 6, false)]
        public int TicketTime;

        /// <summary>
        /// 工单代码
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 物料名称
        /// </summary>
        [FieldMapAttribute("ITEMNANE", typeof(string), 40, false)]
        public string ItemName;

        /// <summary>
        /// 来源仓库结余数
        /// </summary>
        [FieldMapAttribute("FRMWHQTY", typeof(decimal), 15, false)]
        public decimal FromWarehouseQty;

        /// <summary>
        /// 目标仓库结余数
        /// </summary>
        [FieldMapAttribute("TOWHQTY", typeof(decimal), 15, false)]
        public decimal ToWarehouseQty;

    }
    #endregion

    #region WarehouseTicketQueryItem
    /// <summary>
    /// 交易单主档
    /// </summary>
    [Serializable, TableMap("TBLWHTKT", "TKTNO")]
    public class WarehouseTicketQueryItem : DomainObject
    {
        public WarehouseTicketQueryItem()
        {
        }

        /// <summary>
        /// 交易类型
        /// </summary>
        [FieldMapAttribute("TRANSTYPECODE", typeof(string), 40, true)]
        public string TransactionTypeCode;

        /// <summary>
        /// 目标仓库
        /// </summary>
        [FieldMapAttribute("TOWHCODE", typeof(string), 40, true)]
        public string TOWarehouseCode;

        //		/// <summary>
        //		/// 目标工段
        //		/// </summary>
        //		[FieldMapAttribute("TOSEGCODE", typeof(string), 40, true)]
        //		public string  TOSegmentCode;

        /// <summary>
        /// 仓库代码
        /// </summary>
        [FieldMapAttribute("WHCODE", typeof(string), 40, false)]
        public string WarehouseCode;

        //		/// <summary>
        //		/// 工段代码
        //		/// </summary>
        //		[FieldMapAttribute("SEGCODE", typeof(string), 40, false)]
        //		public string  SegmentCode;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, false)]
        public string FactoryCode;

        /// <summary>
        /// 目标工厂
        /// </summary>
        [FieldMapAttribute("TOFACCODE", typeof(string), 40, true)]
        public string TOFactoryCode;

        /// <summary>
        /// 物料编号
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 交易数量
        /// </summary>
        [FieldMapAttribute("QTY", typeof(decimal), 40, true)]
        public decimal Qty;

    }
    #endregion

    #region SAPStorageInfo

    [Serializable, TableMap("TBLSAPSTORAGEINFO", "ITEMCODE,ORGID,STORAGEID,ITEMGRADE")]
    public class SAPStorageInfo : DomainObject
    {
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        [FieldMapAttribute("DIVISION", typeof(string), 40, true)]
        public string Division;

        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrgID;

        [FieldMapAttribute("STORAGEID", typeof(string), 40, true)]
        public string StorageID;

        [FieldMapAttribute("STORAGENAME", typeof(string), 100, true)]
        public string StorageName;

        [FieldMapAttribute("ITEMGRADE", typeof(string), 40, false)]
        public string ItemGrade;

        [FieldMapAttribute("CLABSQty", typeof(decimal), 13, true)]
        public decimal CLABSQty;

        [FieldMapAttribute("CINSMQty", typeof(decimal), 13, true)]
        public decimal CINSMQty;

        [FieldMapAttribute("CSPEMQty", typeof(decimal), 13, true)]
        public decimal CSPEMQty;

        [FieldMapAttribute("CUMLQty", typeof(decimal), 13, true)]
        public decimal CUMLQty;

        [FieldMapAttribute("ITEMDESC", typeof(string), 100, true)]
        public string ItemDescription;

        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

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
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;
    }
    #endregion

    #region SAPStorageQuery

    /// <summary>
    ///	SAPStorageQuery
    /// </summary>
    [Serializable, TableMap("TBLSAPSTORAGEQUERY", "SERIAL")]
    public class SAPStorageQuery : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public SAPStorageQuery()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 8, false)]
        public int Serial;

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        ///<summary>
        ///OrganizationID
        ///</summary>	
        [FieldMapAttribute("ORGID", typeof(string), 500, false)]
        public string OrganizationID;

        ///<summary>
        ///StorageID
        ///</summary>	
        [FieldMapAttribute("STORAGEID", typeof(string), 500, false)]
        public string StorageID;

        ///<summary>
        ///Flag
        ///</summary>	
        [FieldMapAttribute("FLAG", typeof(string), 10, false)]
        public string Flag;

        ///<summary>
        ///TransactionCode
        ///</summary>	
        [FieldMapAttribute("TRANSACTIONCODE", typeof(string), 100, true)]
        public string TransactionCode;

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

    #region Storage-- 库别信息 add by jinger 2016-01-18
    /// <summary>
    /// TBLSTORAGE-- 库别信息 
    /// </summary>
    [Serializable, TableMap("TBLSTORAGE", "STORAGECODE,ORGID")]
    public class Storage : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Storage()
        {
        }
        ///<summary>
        ///组织ID
        ///</summary>
        [FieldMapAttribute("ORGID", typeof(int), 22, false)]
        public int OrgID;

        ///<summary>
        ///SAP库位代码
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        ///<summary>
        ///SAP库位名称
        ///</summary>
        [FieldMapAttribute("STORAGENAME", typeof(string), 100, true)]
        public string StorageName;

        ///<summary>
        ///预留字段3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 40, true)]
        public string Eattribute3;

        ///<summary>
        ///预留字段2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 40, true)]
        public string Eattribute2;

        ///<summary>
        ///库位来源(MES, SAP)
        ///</summary>
        [FieldMapAttribute("SOURCEFLAG", typeof(string), 3, false)]
        public string SourceFlag;

        ///<summary>
        ///虚拟库位标识(Y:虚拟库位; N:非虚拟库位)
        ///</summary>
        [FieldMapAttribute("VIRTUALFLAG", typeof(string), 1, false)]
        public string VirtualFlag;

        ///<summary>
        ///联系人4
        ///</summary>
        [FieldMapAttribute("CONTACTUSER4", typeof(string), 40, true)]
        public string ContactUser4;

        ///<summary>
        ///联系人3
        ///</summary>
        [FieldMapAttribute("CONTACTUSER3", typeof(string), 40, true)]
        public string ContactUser3;

        ///<summary>
        ///联系人2
        ///</summary>
        [FieldMapAttribute("CONTACTUSER2", typeof(string), 40, true)]
        public string ContactUser2;

        ///<summary>
        ///联系人1
        ///</summary>
        [FieldMapAttribute("CONTACTUSER1", typeof(string), 40, true)]
        public string ContactUser1;

        ///<summary>
        ///地址4
        ///</summary>
        [FieldMapAttribute("ADDRESS4", typeof(string), 200, true)]
        public string Address4;

        ///<summary>
        ///地址3
        ///</summary>
        [FieldMapAttribute("ADDRESS3", typeof(string), 200, true)]
        public string Address3;

        ///<summary>
        ///地址2
        ///</summary>
        [FieldMapAttribute("ADDRESS2", typeof(string), 200, true)]
        public string Address2;

        ///<summary>
        ///地址1
        ///</summary>
        [FieldMapAttribute("ADDRESS1", typeof(string), 200, true)]
        public string Address1;

        ///<summary>
        ///库存属性
        ///</summary>
        [FieldMapAttribute("SPROPERTY", typeof(string), 40, true)]
        public string SProperty;

        ///<summary>
        ///预留字段1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 100, true)]
        public string Eattribute1;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, true)]
        public int MaintainTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, true)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;
    }
    #endregion

    #region Location--货位信息 add by jinger 2016-01-18
    /// <summary>
    /// TBLLOCATION--货位信息
    /// </summary>
    [Serializable, TableMap("TBLLOCATION", "LOCATIONCODE,ORGID")]
    public class Location : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Location()
        {
        }

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///预留字段3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 40, true)]
        public string Eattribute3;

        ///<summary>
        ///预留字段2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 40, true)]
        public string Eattribute2;

        ///<summary>
        ///预留字段1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;

        ///<summary>
        ///组织ID
        ///</summary>
        [FieldMapAttribute("ORGID", typeof(int), 22, false)]
        public int OrgID;

        ///<summary>
        ///库位代码
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        ///<summary>
        ///货位名称
        ///</summary>
        [FieldMapAttribute("LOCATIONNAME", typeof(string), 100, true)]
        public string LocationName;

        ///<summary>
        ///货位代码
        ///</summary>
        [FieldMapAttribute("LOCATIONCODE", typeof(string), 40, false)]
        public string LocationCode;

    }

    [Serializable]
    public class LocationWithStorageName : Location
    {
        [FieldMapAttribute("STORAGENAME", typeof(string), 100, false)]
        public string StorageName;
    }
    #endregion

    #region SpecStorageInfo-- 特殊物料库存信息  add by jinger 2016-01-29
    /// <summary>
    /// TBLSPECSTORAGEINFO-- 特殊物料库存信息 
    /// </summary>
    [Serializable, TableMap("TBLSPECSTORAGEINFO", "STORAGECODE,MCODE,LOCATIONCODE")]
    public class SpecStorageInfo : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public SpecStorageInfo()
        {
        }

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///库存数量
        ///</summary>
        [FieldMapAttribute("STORAGEQTY", typeof(int), 22, false)]
        public int StorageQty;

        ///<summary>
        ///货位代码
        ///</summary>
        [FieldMapAttribute("LOCATIONCODE", typeof(string), 40, false)]
        public string LocationCode;

        ///<summary>
        ///库位
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        ///<summary>
        ///鼎桥物料编码
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 22, false)]
        public string DQMCode;

        ///<summary>
        ///SAP物料号
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

    }
    #endregion

    #region Storagedetail-- 库存明细信息 add by jinger 2016-01-18
    /// <summary>
    /// TBLSTORAGEDETAIL-- 库存明细信息 
    /// </summary>
    [Serializable, TableMap("TBLSTORAGEDETAIL", "CARTONNO")]
    public class StorageDetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public StorageDetail()
        {
        }

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///检测返工申请人
        ///</summary>
        [FieldMapAttribute("REWORKAPPLYUSER", typeof(string), 40, true)]
        public string ReworkApplyUser;

        ///<summary>
        ///工厂
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, false)]
        public string FacCode;

        ///<summary>
        ///最后一次入库时间
        ///</summary>
        [FieldMapAttribute("LASTSTORAGEAGEDATE", typeof(int), 22, true)]
        public int LastStorageAgeDate;

        ///<summary>
        ///第一次入库时间
        ///</summary>
        [FieldMapAttribute("STORAGEAGEDATE", typeof(int), 22, true)]
        public int StorageAgeDate;

        ///<summary>
        ///生产日期
        ///</summary>
        [FieldMapAttribute("PRODUCTION_DATE", typeof(int), 22, true)]
        public int ProductionDate;

        ///<summary>
        ///鼎桥批次号
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string Lotno;

        ///<summary>
        ///供应商批次号
        ///</summary>
        [FieldMapAttribute("SUPPLIER_LOTNO", typeof(string), 40, true)]
        public string SupplierLotNo;

        ///<summary>
        ///冻结数量
        ///</summary>
        [FieldMapAttribute("FREEZEQTY", typeof(int), 22, false)]
        public int FreezeQty;

        ///<summary>
        ///可用数量
        ///</summary>
        [FieldMapAttribute("AVAILABLEQTY", typeof(int), 22, false)]
        public int AvailableQty;

        ///<summary>
        ///库存数量
        ///</summary>
        [FieldMapAttribute("STORAGEQTY", typeof(int), 22, false)]
        public int StorageQty;

        ///<summary>
        ///单位
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///物料描述
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MDesc;

        ///<summary>
        ///鼎桥物料号
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DQMCode;

        ///<summary>
        ///箱号条码
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string CartonNo;

        ///<summary>
        ///货位条码
        ///</summary>
        [FieldMapAttribute("LOCATIONCODE", typeof(string), 40, false)]
        public string LocationCode;

        ///<summary>
        ///库位
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        ///<summary>
        ///SAP物料号
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

    }

    /// <summary>
    /// 库存明细扩展实体
    /// </summary>
    [Serializable]
    public class StorageDetailExt : StorageDetail
    {
        /// <summary>
        /// 库位名称
        /// </summary>
        [FieldMapAttribute("STORAGENAME", typeof(string), 100, false)]
        public string StorageName;

        ///<summary>
        ///货位名称
        ///</summary>
        [FieldMapAttribute("LOCATIONNAME", typeof(string), 100, true)]
        public string LocationName;

        ///<summary>
        ///SN条码
        ///</summary>
        [FieldMapAttribute("SN", typeof(string), 40, false)]
        public string SN;
    }
    #endregion

    #region Storagedetailsn-- 库存明细SN信息 add by jinger 2016-01-18
    /// <summary>
    /// TBLSTORAGEDETAILSN-- 库存明细SN信息 
    /// </summary>
    [Serializable, TableMap("TBLSTORAGEDETAILSN", "SN")]
    public class StorageDetailSN : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public StorageDetailSN()
        {
        }

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///拣货中
        ///</summary>
        [FieldMapAttribute("PICKBLOCK", typeof(string), 40, false)]
        public string PickBlock;

        ///<summary>
        ///SN条码
        ///</summary>
        [FieldMapAttribute("SN", typeof(string), 40, false)]
        public string SN;

        ///<summary>
        ///箱号条码
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string CartonNo;

    }
    #endregion

    #region Specinout-- 特殊物料出入库单 add by jinger 2016-01-22
    /// <summary>
    /// TBLSPECINOUT-- 特殊物料出入库单 
    /// </summary>
    [Serializable, TableMap("TBLSPECINOUT", "SERIAL")]
    public class SpecInOut : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public SpecInOut()
        {
        }

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///入库数量
        ///</summary>
        [FieldMapAttribute("QTY", typeof(int), 22, true)]
        public int Qty;

        ///<summary>
        ///货位条码
        ///</summary>
        [FieldMapAttribute("LOCATIONCODE", typeof(string), 40, false)]
        public string LocationCode;

        ///<summary>
        ///库位
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        ///<summary>
        ///物料编码
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///鼎桥物料编码
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DQMCode;

        ///<summary>
        ///I=入库，O=出库
        ///</summary>
        [FieldMapAttribute("MOVETYPE", typeof(string), 1, false)]
        public string MoveType;

        ///<summary>
        ///自增长
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

    }

    /// <summary>
    /// 出入单扩展实体
    /// </summary>
    [Serializable]
    public class SpecInOutWithMaterial : BenQGuru.eMES.Domain.MOModel.Material
    {
        /// <summary>
        /// 库位名称
        /// </summary>
        [FieldMapAttribute("STORAGENAME", typeof(string), 100, false)]
        public string StorageName;

        ///<summary>
        ///货位名称
        ///</summary>
        [FieldMapAttribute("LOCATIONNAME", typeof(string), 100, true)]
        public string LocationName;

        ///<summary>
        ///入库数量
        ///</summary>
        [FieldMapAttribute("QTY", typeof(int), 22, true)]
        public int Qty;
    }
    #endregion

    //#region Invoices-- SAP单据表  add by jinger 2016-01-25
    ///// <summary>
    ///// TBLINVOICES-- SAP单据表 
    ///// </summary>
    //[Serializable, TableMap("TBLINVOICES","INVNO")]
    //public class Invoices : BenQGuru.eMES.Common.Domain.DomainObject
    //{
    //     public Invoices()
    //     {
    //     }

    //     ///<summary>
    //     ///预留字段3
    //     ///</summary>
    //     [FieldMapAttribute("EATTRIBUTE3", typeof(string), 40, true)]
    //     public string Eattribute3;

    //     ///<summary>
    //     ///预留字段2
    //     ///</summary>
    //     [FieldMapAttribute("EATTRIBUTE2", typeof(string), 40, true)]
    //     public string Eattribute2;

    //     ///<summary>
    //     ///预留字段1
    //     ///</summary>
    //     [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
    //     public string Eattribute1;

    //     ///<summary>
    //     ///维护时间
    //     ///</summary>
    //     [FieldMapAttribute("MTIME", typeof(int), 22, false)]
    //     public int MaintainTime;

    //     ///<summary>
    //     ///维护日期
    //     ///</summary>
    //     [FieldMapAttribute("MDATE", typeof(int), 22, false)]
    //     public int MaintainDate;

    //     ///<summary>
    //     ///维护人
    //     ///</summary>
    //     [FieldMapAttribute("MUSER", typeof(string), 40, false)]
    //     public string MaintainUser;

    //     ///<summary>
    //     ///创建时间
    //     ///</summary>
    //     [FieldMapAttribute("CTIME", typeof(int), 22, false)]
    //     public int CTime;

    //     ///<summary>
    //     ///创建日期
    //     ///</summary>
    //     [FieldMapAttribute("CDATE", typeof(int), 22, false)]
    //     public int CDate;

    //     ///<summary>
    //     ///创建人
    //     ///</summary>
    //     [FieldMapAttribute("CUSER", typeof(string), 40, false)]
    //     public string CUser;

    //     ///<summary>
    //     ///盘点凭证号
    //     ///</summary>
    //     [FieldMapAttribute("INVENTORYNO", typeof(string), 16, true)]
    //     public string InventoryNo;

    //     ///<summary>
    //     ///凭证日期
    //     ///</summary>
    //     [FieldMapAttribute("VOUCHERDATE", typeof(int), 22, true)]
    //     public int VoucherDate;

    //     ///<summary>
    //     ///申请日期
    //     ///</summary>
    //     [FieldMapAttribute("APPLYDATE", typeof(int), 22, true)]
    //     public int ApplyDate;

    //     ///<summary>
    //     ///出/入标识
    //     ///</summary>
    //     [FieldMapAttribute("MOVETYPE", typeof(string), 3, false)]
    //     public string MoveType;

    //     ///<summary>
    //     ///成本中心
    //     ///</summary>
    //     [FieldMapAttribute("CC", typeof(string), 10, true)]
    //     public string Cc;

    //     ///<summary>
    //     ///检测返工申请人
    //     ///</summary>
    //     [FieldMapAttribute("REWORKAPPLYUSER", typeof(string), 12, true)]
    //     public string ReworkApplyUser;

    //     ///<summary>
    //     ///物流信息
    //     ///</summary>
    //     [FieldMapAttribute("LOGISTICS", typeof(string), 220, true)]
    //     public string Logistics;

    //     ///<summary>
    //     ///OA流水号
    //     ///</summary>
    //     [FieldMapAttribute("OANO", typeof(string), 12, true)]
    //     public string OANO;

    //     ///<summary>
    //     ///调拨类型（UB=库存调拨；ZC=转储；ZJCR=检测返工入；ZJCC=检测返工出；ZBLR=不良品入库；ZCAR=Claim入库）(UB)
    //     ///</summary>
    //     [FieldMapAttribute("UBTYPE", typeof(string), 4, true)]
    //     public string UbType;

    //     ///<summary>
    //     ///是否硬件光伏（光伏标识From SAP SO field）(Y，N)(DN)
    //     ///</summary>
    //     [FieldMapAttribute("GFFLAG", typeof(string), 40, true)]
    //     public string GfFlag;

    //     ///<summary>
    //     ///A=空pending，B=取消cancel，C=发布release(DN)
    //     ///</summary>
    //     [FieldMapAttribute("DNBATCHSTATUS", typeof(string), 1, true)]
    //     public string DNBatchStatus;

    //     ///<summary>
    //     ///交付类型 LF=发货、RE=退货(DN)
    //     ///</summary>
    //     [FieldMapAttribute("DNTYPE", typeof(string), 4, true)]
    //     public string DNType;

    //     ///<summary>
    //     ///退货标识(Return PO标识)(X=退货PO，空=正常PO)(PO)
    //     ///</summary>
    //     [FieldMapAttribute("RETURNFLAG", typeof(string), 1, true)]
    //     public string ReturnFlag;

    //     ///<summary>
    //     ///PO备注5--our reference(PO)
    //     ///</summary>
    //     [FieldMapAttribute("REMARK5", typeof(string), 12, true)]
    //     public string Remark5;

    //     ///<summary>
    //     ///PO备注4--your reference(PO)
    //     ///</summary>
    //     [FieldMapAttribute("REMARK4", typeof(string), 12, true)]
    //     public string Remark4;

    //     ///<summary>
    //     ///PO备注3--header remarks(PO)
    //     ///</summary>
    //     [FieldMapAttribute("REMARK3", typeof(string), 50, true)]
    //     public string Remark3;

    //     ///<summary>
    //     ///PO备注2--header note(PO)
    //     ///</summary>
    //     [FieldMapAttribute("REMARK2", typeof(string), 50, true)]
    //     public string Remark2;

    //     ///<summary>
    //     ///PO备注1--header text(PO)
    //     ///</summary>
    //     [FieldMapAttribute("REMARK1", typeof(string), 50, true)]
    //     public string Remark1;

    //     ///<summary>
    //     ///采购组描述（purchase group）(PO)
    //     ///</summary>
    //     [FieldMapAttribute("PURCHASEGROUP", typeof(string), 18, true)]
    //     public string PurchaseGroup;

    //     ///<summary>
    //     ///采购组(purchase group)(PO)
    //     ///</summary>
    //     [FieldMapAttribute("PURCHUGCODE", typeof(string), 3, true)]
    //     public string PurchugCode;

    //     ///<summary>
    //     ///采购组织（purchase org.）(PO)
    //     ///</summary>
    //     [FieldMapAttribute("PURCHORGCODE", typeof(string), 4, true)]
    //     public string PurchorgCode;

    //     ///<summary>
    //     ///跟单员电话(PO)
    //     ///</summary>
    //     [FieldMapAttribute("BUYERPHONE", typeof(string), 30, true)]
    //     public string BuyerPhone;

    //     ///<summary>
    //     ///订单更新时间（时分秒）(PO)
    //     ///</summary>
    //     [FieldMapAttribute("POUPDATETIME", typeof(int), 22, true)]
    //     public int PoupDateTime;

    //     ///<summary>
    //     ///订单更新日期（年月日）(PO)
    //     ///</summary>
    //     [FieldMapAttribute("POUPDATEDATE", typeof(int), 22, true)]
    //     public int PoupDateDate;

    //     ///<summary>
    //     ///订单创建人员(PO)
    //     ///</summary>
    //     [FieldMapAttribute("CREATEUSER", typeof(string), 12, true)]
    //     public string CreateUser;

    //     ///<summary>
    //     ///订单创建日期（年月日）(PO)
    //     ///</summary>
    //     [FieldMapAttribute("POCREATEDATE", typeof(int), 22, true)]
    //     public int PocreateDate;

    //     ///<summary>
    //     ///订单类型(NB)(PO)
    //     ///</summary>
    //     [FieldMapAttribute("ORDERTYPE", typeof(string), 4, true)]
    //     public string OrderType;

    //     ///<summary>
    //     ///供应商代码(PO)
    //     ///</summary>
    //     [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
    //     public string VendorCode;

    //     ///<summary>
    //     ///公司代码(取TD28)(PO)
    //     ///</summary>
    //     [FieldMapAttribute("COMPANYCODE", typeof(string), 4, true)]
    //     public string CompanyCode;

    //     ///<summary>
    //     ///订单状态(G=Release,非G=Pending)(PO)
    //     ///</summary>
    //     [FieldMapAttribute("ORDERSTATUS", typeof(string), 1, true)]
    //     public string OrderStatus;

    //     ///<summary>
    //     ///是否可创建入库指令(Y:可创建；N：不可创建)
    //     ///</summary>
    //     [FieldMapAttribute("ASNAVAILABLE", typeof(string), 40, true)]
    //     public string ASNAvailable;

    //     ///<summary>
    //     ///SAP单据完成状态(N:未完成；Y：完成)
    //     ///</summary>
    //     [FieldMapAttribute("FINISHFLAG", typeof(string), 40, true)]
    //     public string FinishFlag;

    //     ///<summary>
    //     ///SAP单据类型
    //     ///</summary>
    //     [FieldMapAttribute("INVTYPE", typeof(string), 40, false)]
    //     public string InvType;

    //     ///<summary>
    //     ///SAP单据状态
    //     ///</summary>
    //     [FieldMapAttribute("INVSTATUS", typeof(string), 1, true)]
    //     public string InvStatus;

    //     ///<summary>
    //     ///SAP单据号(PO)
    //     ///</summary>
    //     [FieldMapAttribute("INVNO", typeof(string), 40, false)]
    //     public string InvNo;

    // }

    ///// <summary>
    ///// SAP单据扩展实体
    ///// </summary>
    //[Serializable]
    //public class InvoicesExt : Invoices
    //{
    //    /// <summary>
    //    /// 供应商名称
    //    /// </summary>
    //    [FieldMapAttribute("VENDORNAME", typeof(string), 100, false)]
    //    public string VendorName;
    //}
    //#endregion

    //#region Invoicesdetail-- SAP单据明细表 add by jinger 2016-01-25
    ///// <summary>
    ///// TBLINVOICESDETAIL-- SAP单据明细表 add by jinger 2016-01-25
    ///// </summary>
    //[Serializable, TableMap("TBLINVOICESDETAIL", "INVNO,INVLINE")]
    //public class InvoicesDetail : BenQGuru.eMES.Common.Domain.DomainObject
    //{
    //    public InvoicesDetail()
    //    {
    //    }

    //    ///<summary>
    //    ///预留字段3
    //    ///</summary>
    //    [FieldMapAttribute("EATTRIBUTE3", typeof(string), 40, true)]
    //    public string Eattribute3;

    //    ///<summary>
    //    ///预留字段2
    //    ///</summary>
    //    [FieldMapAttribute("EATTRIBUTE2", typeof(string), 40, true)]
    //    public string Eattribute2;

    //    ///<summary>
    //    ///预留字段1
    //    ///</summary>
    //    [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
    //    public string Eattribute1;

    //    ///<summary>
    //    ///维护时间
    //    ///</summary>
    //    [FieldMapAttribute("MTIME", typeof(int), 22, false)]
    //    public int MaintainTime;

    //    ///<summary>
    //    ///维护日期
    //    ///</summary>
    //    [FieldMapAttribute("MDATE", typeof(int), 22, false)]
    //    public int MaintainDate;

    //    ///<summary>
    //    ///维护人
    //    ///</summary>
    //    [FieldMapAttribute("MUSER", typeof(string), 40, false)]
    //    public string MaintainUser;

    //    ///<summary>
    //    ///创建时间
    //    ///</summary>
    //    [FieldMapAttribute("CTIME", typeof(int), 22, false)]
    //    public int CTime;

    //    ///<summary>
    //    ///创建日期
    //    ///</summary>
    //    [FieldMapAttribute("CDATE", typeof(int), 22, false)]
    //    public int CDate;

    //    ///<summary>
    //    ///创建人
    //    ///</summary>
    //    [FieldMapAttribute("CUSER", typeof(string), 40, false)]
    //    public string CUser;

    //    ///<summary>
    //    ///盘亏/盘盈标识（702/701）(SC)
    //    ///</summary>
    //    [FieldMapAttribute("TYPE", typeof(string), 3, false)]
    //    public string Type;

    //    ///<summary>
    //    ///希望到货日期（需求日期）(RS)
    //    ///</summary>
    //    [FieldMapAttribute("NEEDDATE", typeof(int), 22, true)]
    //    public int NeedDate;

    //    ///<summary>
    //    ///要求到货时间(UB)
    //    ///</summary>
    //    [FieldMapAttribute("DEMANDARRIVALDATE", typeof(int), 22, true)]
    //    public int DemandArrivalDate;

    //    ///<summary>
    //    ///收货方物料号（Item text）(UB)
    //    ///</summary>
    //    [FieldMapAttribute("RECEIVEMCODE", typeof(string), 35, true)]
    //    public string ReceiveMCode;

    //    ///<summary>
    //    ///华为物料号（44118421）(UB,RS)
    //    ///</summary>
    //    [FieldMapAttribute("CUSTMCODE", typeof(string), 35, true)]
    //    public string CustmCode;

    //    ///<summary>
    //    ///收货联系人及联系方式(UB,RS)
    //    ///</summary>
    //    [FieldMapAttribute("RECEIVERUSER", typeof(string), 50, true)]
    //    public string ReceiverUser;

    //    ///<summary>
    //    ///入库库位地址,收货/发货地址(UB,RS)
    //    ///</summary>
    //    [FieldMapAttribute("RECEIVERADDR", typeof(string), 240, true)]
    //    public string ReceiverAddr;

    //    ///<summary>
    //    ///鼎桥S编码{物料号}（SAP 需要考虑取数逻辑）——加标识位区分光伏在SO(DN)
    //    ///</summary>
    //    [FieldMapAttribute("DQSMCODE", typeof(string), 40, true)]
    //    public string DQSMCode;

    //    ///<summary>
    //    ///包装箱方式取数 （SAP自建表取数，光伏特有）(DN)
    //    ///</summary>
    //    [FieldMapAttribute("PACKINGWAYNO", typeof(string), 40, true)]
    //    public string PackingWayNo;

    //    ///<summary>
    //    ///包装箱规格 （SAP自建表取数，光伏特有）(DN)
    //    ///</summary>
    //    [FieldMapAttribute("PACKINGSPEC", typeof(string), 40, true)]
    //    public string PackingSpec;

    //    ///<summary>
    //    ///包装箱编号 （SAP自建表取数，光伏特有）(DN)
    //    ///</summary>
    //    [FieldMapAttribute("PACKINGNO", typeof(string), 40, true)]
    //    public string PackingNo;

    //    ///<summary>
    //    ///包装方式 （SAP自建表取数，光伏特有）(DN)
    //    ///</summary>
    //    [FieldMapAttribute("PACKINGWAY", typeof(string), 40, true)]
    //    public string PackingWay;

    //    ///<summary>
    //    ///华为型号标签信息 （SAP自建表取数，光伏特有）(DN)
    //    ///</summary>
    //    [FieldMapAttribute("HWTYPEINFO", typeof(string), 40, true)]
    //    public string HWTypeInfo;

    //    ///<summary>
    //    ///华为编码数量单位(DN)
    //    ///</summary>
    //    [FieldMapAttribute("HWCODEUNIT", typeof(string), 40, true)]
    //    public string HWCodeUnit;

    //    ///<summary>
    //    ///华为编码数量 （SAP计算出来的，光伏特有）(DN)
    //    ///</summary>
    //    [FieldMapAttribute("HWCODEQTY", typeof(string), 40, true)]
    //    public string HWCodeQty;

    //    ///<summary>
    //    ///光伏华为描述(DN)
    //    ///</summary>
    //    [FieldMapAttribute("GFHWDESC", typeof(string), 40, true)]
    //    public string GFhwDesc;

    //    ///<summary>
    //    ///光伏包装序号 Purchase order item(SAP，光伏特有)(DN)
    //    ///</summary>
    //    [FieldMapAttribute("GFPACKINGSEQ", typeof(string), 6, true)]
    //    public string GFPackingSeq;

    //    ///<summary>
    //    ///光伏华为编码 item your reference (SAP)(DN)
    //    ///</summary>
    //    [FieldMapAttribute("GFHWMCODE", typeof(string), 12, true)]
    //    public string GFhwMCode;

    //    ///<summary>
    //    ///供应商物料编码_verdor material number (SAP，取数逻辑参考download报告) (DN)
    //    ///</summary>
    //    [FieldMapAttribute("VENDERMCODE", typeof(string), 40, true)]
    //    public string VenderMCode;

    //    ///<summary>
    //    ///客户物料描述（SAP) from SO(DN)
    //    ///</summary>
    //    [FieldMapAttribute("CUSITEMDESC", typeof(string), 40, true)]
    //    public string CusItemDesc;

    //    ///<summary>
    //    ///客户物料型号（SAP) from SO(DN)
    //    ///</summary>
    //    [FieldMapAttribute("CUSITEMSPEC", typeof(string), 40, true)]
    //    public string CusItemspec;

    //    ///<summary>
    //    ///客户物料编码（SAP) from SO(DN)
    //    ///</summary>
    //    [FieldMapAttribute("CUSMCODE", typeof(string), 40, true)]
    //    public string CusMCode;

    //    ///<summary>
    //    ///移动类型(DN)
    //    ///</summary>
    //    [FieldMapAttribute("MOVEMENTTYPE", typeof(string), 4, true)]
    //    public string MovementType;

    //    ///<summary>
    //    ///上层行项目号(Hign Level Item)(DN)
    //    ///</summary>
    //    [FieldMapAttribute("HIGNLEVELITEM", typeof(int), 22, true)]
    //    public int HignLevelItem;

    //    ///<summary>
    //    ///光伏合同号 Header your reference (SAP，光伏特有)(DN)
    //    ///</summary>
    //    [FieldMapAttribute("GFCONTRACTNO", typeof(string), 12, true)]
    //    public string GfContractNo;

    //    ///<summary>
    //    ///计划交货日期 Planed GI date (SAP)(DN)
    //    ///</summary>
    //    [FieldMapAttribute("PLANGIDATE", typeof(string), 40, true)]
    //    public string PlanGIDate;

    //    ///<summary>
    //    ///收货地址 shipping  location （SAP) (DN)
    //    ///</summary>
    //    [FieldMapAttribute("SHIPPINGLOCATION", typeof(string), 40, true)]
    //    public string ShippingLocation;

    //    ///<summary>
    //    ///客户批次号(SAP)\项目名称(DN)
    //    ///</summary>
    //    [FieldMapAttribute("CUSBATCHNO", typeof(string), 60, true)]
    //    public string CusBatchNo;

    //    ///<summary>
    //    ///订单类型（SO/PO)（SAP_SO订单类型）(DN)
    //    ///</summary>
    //    [FieldMapAttribute("CUSORDERNOTYPE", typeof(string), 2, true)]
    //    public string CusOrderNoType;

    //    ///<summary>
    //    ///订单号（SO/PO)（SAP SO号）(DN)
    //    ///</summary>
    //    [FieldMapAttribute("CUSORDERNO", typeof(string), 10, true)]
    //    public string CusOrderNo;

    //    ///<summary>
    //    ///合同号(SAP_SO PO号）(DN)
    //    ///</summary>
    //    [FieldMapAttribute("ORDERNO", typeof(string), 20, true)]
    //    public string OrderNo;

    //    ///<summary>
    //    ///拣料条件（SAP)(B B和S)(DN)
    //    ///</summary>
    //    [FieldMapAttribute("PICKCONDITION", typeof(string), 40, true)]
    //    public string PickCondition;

    //    ///<summary>
    //    ///交付方式 （SAP)(物料直发，软件纸件，软件电子件，空)(DN)
    //    ///</summary>
    //    [FieldMapAttribute("POSTWAY", typeof(string), 40, true)]
    //    public string PostWay;

    //    ///<summary>
    //    ///订单原因 Order reason (SAP) (DN)
    //    ///</summary>
    //    [FieldMapAttribute("ORDERREASON", typeof(string), 40, true)]
    //    public string OrderReason;

    //    ///<summary>
    //    ///送达方(DN)
    //    ///</summary>
    //    [FieldMapAttribute("SHIPTOPARTY", typeof(string), 10, true)]
    //    public string ShipToParty;

    //    ///<summary>
    //    ///行项目号(DN)
    //    ///</summary>
    //    [FieldMapAttribute("DNLINE", typeof(int), 22, true)]
    //    public int DNLine;

    //    ///<summary>
    //    ///交货单号(DN)
    //    ///</summary>
    //    [FieldMapAttribute("DNNO", typeof(string), 10, true)]
    //    public string DNNo;

    //    ///<summary>
    //    ///成本中心(CC号)(PO)
    //    ///</summary>
    //    [FieldMapAttribute("CCNO", typeof(string), 10, true)]
    //    public string CcNo;

    //    ///<summary>
    //    ///SO WBS号(PO)
    //    ///</summary>
    //    [FieldMapAttribute("SOWBSNO", typeof(string), 8, true)]
    //    public string SOWBSNo;

    //    ///<summary>
    //    ///销售订单行项目号(SO item号)(PO)
    //    ///</summary>
    //    [FieldMapAttribute("SOITEMNO", typeof(string), 6, true)]
    //    public string SOItemNo;

    //    ///<summary>
    //    ///销售订单号(SO号)(PO)
    //    ///</summary>
    //    [FieldMapAttribute("SO", typeof(string), 10, true)]
    //    public string SO;

    //    ///<summary>
    //    ///国际贸易条件描述(incoterms 2)(PO)
    //    ///</summary>
    //    [FieldMapAttribute("INCOTERMS2", typeof(string), 28, true)]
    //    public string Incoterms2;

    //    ///<summary>
    //    ///国际贸易条件(incoterms 1)(PO)
    //    ///</summary>
    //    [FieldMapAttribute("INCOTERMS1", typeof(string), 3, true)]
    //    public string Incoterms1;

    //    ///<summary>
    //    ///行项目类别（item category）(PO)
    //    ///</summary>
    //    [FieldMapAttribute("ITEMCATEGORY", typeof(string), 1, true)]
    //    public string ItemCategory;

    //    ///<summary>
    //    ///科目分配（account assignment）(PO)
    //    ///</summary>
    //    [FieldMapAttribute("ACCOUNTASSIGNMENT", typeof(string), 1, true)]
    //    public string AccountAssignment;

    //    ///<summary>
    //    ///退货标识(Return PO标识)(X=退货PO，空=正常PO)(PO)
    //    ///</summary>
    //    [FieldMapAttribute("RETURNFLAG", typeof(string), 1, true)]
    //    public string ReturnFlag;

    //    ///<summary>
    //    ///采购申请号(PR号)(PO,RS)
    //    ///</summary>
    //    [FieldMapAttribute("PRNO", typeof(string), 10, true)]
    //    public string PrNo;

    //    ///<summary>
    //    ///供应商物料编码(PO)
    //    ///</summary>
    //    [FieldMapAttribute("VENDORMCODE", typeof(string), 35, true)]
    //    public string VendorMCode;

    //    ///<summary>
    //    ///行项目备注（item text）(PO)
    //    ///</summary>
    //    [FieldMapAttribute("DETAILREMARK", typeof(string), 220, true)]
    //    public string DetailRemark;

    //    ///<summary>
    //    ///行项目状态(PO,UB,RS)
    //    ///</summary>
    //    [FieldMapAttribute("STATUS", typeof(string), 1, true)]
    //    public string Status;

    //    ///<summary>
    //    ///送货地址（行项目）(PO)
    //    ///</summary>
    //    [FieldMapAttribute("SHIPADDR", typeof(string), 240, true)]
    //    public string ShipAddr;

    //    ///<summary>
    //    ///物料单位(PO,DN)
    //    ///</summary>
    //    [FieldMapAttribute("UNIT", typeof(string), 3, true)]
    //    public string Unit;

    //    ///<summary>
    //    ///需求日期（年月日）(PO)
    //    ///</summary>
    //    [FieldMapAttribute("PLANDATE", typeof(int), 22, true)]
    //    public int PlanDate;
    

    //    ///<summary>
    //    ///已出库数量
    //    ///</summary>
    //    [FieldMapAttribute("OUTQTY", typeof(int), 22, true)]
    //    public int OutQty;

    //    ///<summary>
    //    ///已入库数量
    //    ///</summary>
    //    [FieldMapAttribute("ACTQTY", typeof(int), 22, true)]
    //    public int ActQty;

    //    ///<summary>
    //    ///需求数量(PO,DN)
    //    ///</summary>
    //    [FieldMapAttribute("PLANQTY", typeof(int), 22, false)]
    //    public int PlanQty;

    //    ///<summary>
    //    ///物料长描述(PO)
    //    ///</summary>
    //    [FieldMapAttribute("MLONGDESC", typeof(string), 220, true)]
    //    public string MLongDesc;

    //    ///<summary>
    //    ///物料短描述(PO)
    //    ///</summary>
    //    [FieldMapAttribute("MENSHORTDESC", typeof(string), 40, true)]
    //    public string MENShortDesc;

    //    ///<summary>
    //    ///鼎桥物料号
    //    ///</summary>
    //    [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
    //    public string DQMCode;

    //    ///<summary>
    //    ///物料编码(PO,DN)
    //    ///</summary>
    //    [FieldMapAttribute("MCODE", typeof(string), 18, false)]
    //    public string MCode;

    //    ///<summary>
    //    ///库位(PO,DN,UB)
    //    ///</summary>
    //    [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
    //    public string StorageCode;

    //    ///<summary>
    //    ///工厂代码(Plant)(PO,DN,UB)
    //    ///</summary>
    //    [FieldMapAttribute("FACCODE", typeof(string), 4, true)]
    //    public string FacCode;

    //    ///<summary>
    //    ///出库库位( item requisitioner)(UB)
    //    ///</summary>
    //    [FieldMapAttribute("FROMSTORAGECODE", typeof(string), 12, true)]
    //    public string FromStorageCode;

    //    ///<summary>
    //    ///供货工厂(10Y2)(UB)
    //    ///</summary>
    //    [FieldMapAttribute("FROMFACCODE", typeof(string), 10, true)]
    //    public string FromFacCode;

    //    ///<summary>
    //    ///SAP单据号行状态
    //    ///</summary>
    //    [FieldMapAttribute("INVLINESTATUS", typeof(string), 1, true)]
    //    public string InvLineStatus;

    //    ///<summary>
    //    ///SAP单据号行项目号
    //    ///</summary>
    //    [FieldMapAttribute("INVLINE", typeof(int), 22, false)]
    //    public int InvLine;

    //    ///<summary>
    //    ///SAP单据号
    //    ///</summary>
    //    [FieldMapAttribute("INVNO", typeof(string), 40, false)]
    //    public string InvNo;

    //}


    ///// <summary>
    ///// SAP单据明细扩展实体
    ///// </summary>
    //[Serializable]
    //public class InvoicesDetailExt : InvoicesDetail
    //{
    //    ///<summary>
    //    ///SAP单据类型
    //    ///</summary>
    //    [FieldMapAttribute("INVTYPE", typeof(string), 40, false)]
    //    public string InvType;
    //}
    //#endregion

    #region Pick-- 拣货任务令头 add by jinger 2016-01-27
    /// <summary>
    /// TBLPICK-- 拣货任务令头 
    /// </summary>
    [Serializable, TableMap("TBLPICK", "PICKNO")]
    public class Pick : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Pick()
        {
        }

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///备注
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;

        ///<summary>
        ///检测返工申请人
        ///</summary>
        [FieldMapAttribute("REWORKAPPLYUSER", typeof(string), 40, true)]
        public string ReworkapplyUser;

        ///<summary>
        ///收货地址
        ///</summary>
        [FieldMapAttribute("RECEIVERADDR", typeof(string), 100, true)]
        public string Receiveraddr;

        ///<summary>
        ///收货人
        ///</summary>
        [FieldMapAttribute("RECEIVERUSER", typeof(string), 40, true)]
        public string ReceiverUser;

        ///<summary>
        ///出库时间
        ///</summary>
        [FieldMapAttribute("DELIVERY_TIME", typeof(int), 22, true)]
        public int DeliveryTime;

        ///<summary>
        ///出库日期
        ///</summary>
        [FieldMapAttribute("DELIVERY_DATE", typeof(int), 22, true)]
        public int DeliveryDate;

        ///<summary>
        ///箱单完成时间
        ///</summary>
        [FieldMapAttribute("PACKING_LIST_TIME", typeof(int), 22, true)]
        public int PackingListTime;

        ///<summary>
        ///箱单完成日期
        ///</summary>
        [FieldMapAttribute("PACKING_LIST_DATE", typeof(int), 22, true)]
        public int PackingListDate;

        ///<summary>
        ///OQC完成时间
        ///</summary>
        [FieldMapAttribute("OQC_TIME", typeof(int), 22, true)]
        public int OQCTime;

        ///<summary>
        ///OQC完成日期
        ///</summary>
        [FieldMapAttribute("OQC_DATE", typeof(int), 22, true)]
        public int OQCDate;

        ///<summary>
        ///捡料完成时间
        ///</summary>
        [FieldMapAttribute("FINISH_TIME", typeof(int), 22, true)]
        public int FinishTime;

        ///<summary>
        ///捡料完成日期
        ///</summary>
        [FieldMapAttribute("FINISH_DATE", typeof(int), 22, true)]
        public int FinishDate;

        ///<summary>
        ///客户唛头完成时间
        ///</summary>
        [FieldMapAttribute("SHIPPING_MARK_TIME", typeof(int), 22, true)]
        public int ShippingMarkTime;

        ///<summary>
        ///客户唛头完成日期
        ///</summary>
        [FieldMapAttribute("SHIPPING_MARK_DATE", typeof(int), 22, true)]
        public int ShippingMarkDate;

        ///<summary>
        ///客户唛头完成人
        ///</summary>
        [FieldMapAttribute("SHIPPING_MARK_USER", typeof(string), 40, true)]
        public string ShippingMarkUser;

        ///<summary>
        ///下发时间
        ///</summary>
        [FieldMapAttribute("DOWN_TIME", typeof(int), 22, true)]
        public int DownTime;

        ///<summary>
        ///下发日期
        ///</summary>
        [FieldMapAttribute("DOWN_DATE", typeof(int), 22, true)]
        public int DownDate;

        ///<summary>
        ///下发人
        ///</summary>
        [FieldMapAttribute("DOWN_USER", typeof(string), 40, true)]
        public string DownUser;

        ///<summary>
        ///OA流水号（head Your Reference）
        ///</summary>
        [FieldMapAttribute("OANO", typeof(string), 40, true)]
        public string OANo;

        ///<summary>
        ///创建拣货任务令时间
        ///</summary>
        [FieldMapAttribute("CREATE_PICK_TIME", typeof(int), 22, true)]
        public int CreatePickTime;

        ///<summary>
        ///创建拣货任务令日期
        ///</summary>
        [FieldMapAttribute("CREATE_PICK_DATE", typeof(int), 22, true)]
        public int CreatePickDate;

        ///<summary>
        ///光伏标识（From SAP SO field）(DN)
        ///</summary>
        [FieldMapAttribute("GFFLAG", typeof(string), 40, true)]
        public string GFFlag;

        ///<summary>
        ///客户批次号（SAP 项目名称)(DN)
        ///</summary>
        [FieldMapAttribute("CUSBATCHNO", typeof(string), 40, true)]
        public string CusBatchNo;

        ///<summary>
        ///计划交货日期 Planed GI date (SAP)(DN)
        ///</summary>
        [FieldMapAttribute("PLANGIDATE", typeof(string), 40, true)]
        public string PlanGIDate;

        ///<summary>
        ///计划发货日期
        ///</summary>
        [FieldMapAttribute("PLAN_DATE", typeof(int), 22, true)]
        public int PlanDate;

        ///<summary>
        ///出库库位
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        ///<summary>
        ///出库工厂
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, false)]
        public string FacCode;

        ///<summary>
        ///入库库位
        ///</summary>
        [FieldMapAttribute("INSTORAGECODE", typeof(string), 40, true)]
        public string InStorageCode;

        ///<summary>
        ///入库工厂
        ///</summary>
        [FieldMapAttribute("INFACCODE", typeof(string), 40, true)]
        public string InFacCode;

        ///<summary>
        ///PO号
        ///</summary>
        [FieldMapAttribute("PONO", typeof(string), 40, true)]
        public string PONo;

        ///<summary>
        ///序号
        ///</summary>
        [FieldMapAttribute("SERIAL_NUMBER", typeof(int), 22, true)]
        public int SerialNumber;

        ///<summary>
        ///SAP单据号
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, true)]
        public string InvNo;

        ///<summary>
        ///状态:初始化，已下发，捡料完成，包装完成，OQC校验完成，箱单完成，已出库，取消
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///拣货任务令号类型(对应SAP单据类型)
        ///</summary>
        [FieldMapAttribute("PICKTYPE", typeof(string), 40, false)]
        public string PickType;

        ///<summary>
        ///拣货任务令号
        ///</summary>
        [FieldMapAttribute("PICKNO", typeof(string), 40, false)]
        public string PickNo;

    }
    #endregion

    #region ViewField--选择字段 add by jinger 2016-01-05
    /// <summary>
    /// ViewField--选择字段
    /// </summary>
    [Serializable, TableMap("TBLVIEWFILED", "USERCODE,SEQ,TABLENAME")]
    public class ViewField : DomainObject
    {
        public ViewField()
        {
        }

        /// <summary>
        /// 登录用户代码
        /// </summary>
        [FieldMapAttribute("USERCODE", typeof(string), 40, true)]
        public string UserCode;

        /// <summary>
        /// 顺序号
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

        /// <summary>
        /// 表名
        /// </summary>
        [FieldMapAttribute("TABLENAME", typeof(string), 40, true)]
        public string TableName;

        /// <summary>
        /// 字段名称
        /// </summary>
        [FieldMapAttribute("FIELDNAME", typeof(string), 40, false)]
        public string FieldName;

        /// <summary>
        /// 字段描述
        /// </summary>
        [FieldMapAttribute("DESCRIPTION", typeof(string), 200, false)]
        public string Description;

        /// <summary>
        /// 是否默认
        /// </summary>
        [FieldMapAttribute("ISDEFAULT", typeof(string), 40, false)]
        public string IsDefault;

    }
    #endregion

    #region Storageinfo-- 库存信息  add by jinger 2016-01-27
    ///// <summary>
    ///// TBLSTORAGEINFO-- 库存信息 
    ///// </summary>
    //[Serializable, TableMap("TBLSTORAGEINFO", "STORAGECODE,MCODE")]
    //public class StorageInfo : BenQGuru.eMES.Common.Domain.DomainObject
    //{
    //    public StorageInfo()
    //    {
    //    }

    //    ///<summary>
    //    ///维护时间
    //    ///</summary>
    //    [FieldMapAttribute("MTIME", typeof(int), 22, false)]
    //    public int MaintainTime;

    //    ///<summary>
    //    ///维护日期
    //    ///</summary>
    //    [FieldMapAttribute("MDATE", typeof(int), 22, false)]
    //    public int MaintainDate;

    //    ///<summary>
    //    ///维护人
    //    ///</summary>
    //    [FieldMapAttribute("MUSER", typeof(string), 40, false)]
    //    public string MaintainUser;

    //    ///<summary>
    //    ///工厂
    //    ///</summary>
    //    [FieldMapAttribute("FACCODE", typeof(string), 40, false)]
    //    public string FacCode;

    //    ///<summary>
    //    ///库存数量
    //    ///</summary>
    //    [FieldMapAttribute("STORAGEQTY", typeof(int), 22, false)]
    //    public int StorageQty;

    //    ///<summary>
    //    ///库位
    //    ///</summary>
    //    [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
    //    public string StorageCode;

    //    ///<summary>
    //    ///SAP物料号
    //    ///</summary>
    //    [FieldMapAttribute("MCODE", typeof(string), 40, false)]
    //    public string MCode;

    //}
    #endregion

    #region ASN-- ASN主表  add by jinger 2016-01-28
    /// <summary>
    /// TBLASN-- ASN主表 
    /// </summary>
    [Serializable, TableMap("TBLASN", "STNO")]
    public class ASN : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public ASN()
        {
        }

        ///<summary>
        ///初检让步接收箱数
        ///</summary>
        [FieldMapAttribute("INITGIVEINQTY", typeof(int), 22, true)]
        public int InitGiveInQty;

        ///<summary>
        ///初检接收箱数
        ///</summary>
        [FieldMapAttribute("INITRECEIVEQTY", typeof(int), 22, true)]
        public int InitReceiveQty;

        ///<summary>
        ///初检拒收原因
        ///</summary>
        [FieldMapAttribute("INITRECEIVEDESC", typeof(string), 200, true)]
        public string InitReceiveDesc;

        ///<summary>
        ///初检拒收箱数
        ///</summary>
        [FieldMapAttribute("INITREJECTQTY", typeof(int), 22, true)]
        public int InitRejectQty;

        ///<summary>
        ///备注
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///检测返工申请人
        ///</summary>
        [FieldMapAttribute("REWORKAPPLYUSER", typeof(string), 40, true)]
        public string ReWorkApplyUser;

        ///<summary>
        ///出具日期
        ///</summary>
        [FieldMapAttribute("PROVIDE_DATE", typeof(int), 22, true)]
        public int ProvideDate;

        ///<summary>
        ///装箱单号
        ///</summary>
        [FieldMapAttribute("PACKINGLISTNO", typeof(string), 40, true)]
        public string PackingListNo;

        ///<summary>
        ///预计到货日期
        ///</summary>
        [FieldMapAttribute("PREDICT_DATE", typeof(int), 22, true)]
        public int PreictDate;

        ///<summary>
        ///拣货任务令号
        ///</summary>
        [FieldMapAttribute("PICKNO", typeof(string), 40, true)]
        public string PickNo;

        ///<summary>
        ///生产退料入不良品库(Y:入不良品库)
        ///</summary>
        [FieldMapAttribute("REJECTS_FLAG", typeof(string), 1, true)]
        public string RejectsFlag;

        ///<summary>
        ///供应商直发(Y:直发)
        ///</summary>
        [FieldMapAttribute("DIRECT_FLAG", typeof(string), 1, true)]
        public string DirectFlag;

        ///<summary>
        ///紧急物料(Y:紧急)
        ///</summary>
        [FieldMapAttribute("EXIGENCY_FLAG", typeof(string), 1, true)]
        public string ExigencyFlag;

        ///<summary>
        ///体积
        ///</summary>
        [FieldMapAttribute("VOLUME", typeof(string), 40, true)]
        public string Volume;

        ///<summary>
        ///毛重
        ///</summary>
        [FieldMapAttribute("GROSS_WEIGHT", typeof(decimal), 22, true)]
        public decimal GrossWeight;

        ///<summary>
        ///出库库位
        ///</summary>
        [FieldMapAttribute("FROMSTORAGECODE", typeof(string), 40, true)]
        public string FromStorageCode;

        ///<summary>
        ///出库工厂
        ///</summary>
        [FieldMapAttribute("FROMFACCODE", typeof(string), 40, true)]
        public string FromFacCode;

        ///<summary>
        ///入库库位
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        ///<summary>
        ///入库工厂
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, false)]
        public string FacCode;

        ///<summary>
        ///OA申请单号
        ///</summary>
        [FieldMapAttribute("OANO", typeof(string), 40, true)]
        public string OANo;

        ///<summary>
        ///ASN状态:初始化，下发，接收，检验完成，入已库
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///供应商代码
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
        public string VendorCode;

        ///<summary>
        ///SAP单据号
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, true)]
        public string InvNo;

        ///<summary>
        ///ASN单号类型(对应SAP单据类型)
        ///</summary>
        [FieldMapAttribute("STTYPE", typeof(string), 40, false)]
        public string StType;

        ///<summary>
        ///ASN单号
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string StNo;

    }
    #endregion

    #region ASNDetail-- ASN明细表  add by jinger 2016-01-28
    /// <summary>
    /// TBLASNDETAIL-- ASN明细表 
    /// </summary>
    [Serializable, TableMap("TBLASNDETAIL", "STLINE,STNO")]
    public class ASNDetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public ASNDetail()
        {
        }

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///备注
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;

        ///<summary>
        ///初检接收说明(站步接收问题)
        ///</summary>
        [FieldMapAttribute("INITRECEIVEDESC", typeof(string), 200, true)]
        public string InitReceiveDesc;

        ///<summary>
        ///初检接收状态(Receive:接收；Reject:拒收；Givein:让步接收)
        ///</summary>
        [FieldMapAttribute("INITRECEIVESTATUS", typeof(string), 40, true)]
        public string InitReceiveStatus;

        ///<summary>
        ///第一次入库时间
        ///</summary>
        [FieldMapAttribute("STORAGEAGEDATE", typeof(int), 22, true)]
        public int StorageAgeDate;

        ///<summary>
        ///鼎桥批次号
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNo;

        ///<summary>
        ///供应商批次号
        ///</summary>
        [FieldMapAttribute("SUPPLIER_LOTNO", typeof(string), 40, true)]
        public string SupplierLotNo;

        ///<summary>
        ///生产日期
        ///</summary>
        [FieldMapAttribute("PRODUCTION_DATE", typeof(int), 22, true)]
        public int ProductionDate;

        ///<summary>
        ///单位
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///检验通过数量
        ///</summary>
        [FieldMapAttribute("QCPASSQTY", typeof(int), 22, true)]
        public int QcPassQty;

        ///<summary>
        ///入库数量
        ///</summary>
        [FieldMapAttribute("ACTQTY", typeof(int), 22, true)]
        public int ActQty;

        ///<summary>
        ///接收数量
        ///</summary>
        [FieldMapAttribute("RECEIVEQTY", typeof(int), 22, true)]
        public int ReceiveQty;

        ///<summary>
        ///需求数量
        ///</summary>
        [FieldMapAttribute("QTY", typeof(int), 22, false)]
        public int Qty;

        ///<summary>
        ///供应商物料编码描述
        ///</summary>
        [FieldMapAttribute("VENDORMCODEDESC", typeof(string), 200, true)]
        public string VendorMCodeDesc;

        ///<summary>
        ///供应商物料编码
        ///</summary>
        [FieldMapAttribute("VENDORMCODE", typeof(string), 35, true)]
        public string VendorMCode;

        ///<summary>
        ///收货方物料号
        ///</summary>
        [FieldMapAttribute("RECEIVEMCODE", typeof(string), 40, true)]
        public string ReceiveMCode;

        ///<summary>
        ///物料描述
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MDesc;

        ///<summary>
        ///鼎桥物料号
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DQMCode;

        ///<summary>
        ///SAP物料号
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///华为物料号
        ///</summary>
        [FieldMapAttribute("CUSTMCODE", typeof(string), 40, true)]
        public string CustMCode;

        ///<summary>
        ///箱号
        ///</summary>
        [FieldMapAttribute("CARTONSEQ", typeof(string), 40, true)]
        public string CartonSeq;

        ///<summary>
        ///大箱号
        ///</summary>
        [FieldMapAttribute("CARTONBIGSEQ", typeof(string), 40, true)]
        public string CartonBigSeq;

        ///<summary>
        ///箱号条码
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, true)]
        public string CartonNo;

        ///<summary>
        ///ASN行状态:Release:初始化；WaitReceive:待收货；ReceiveClose:初检完成；IQCClose:IQC完成；OnLocation:上架；Close:入库；Cancel:取消
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///ASN单行项目
        ///</summary>
        [FieldMapAttribute("STLINE", typeof(string), 40, false)]
        public string StLine;

        ///<summary>
        ///ASN单号
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string StNo;

    }
    #endregion

    #region InvDoc-- 单据文件表  add by jinger 2016-01-31
    /// <summary>
    /// TBLINVDOC-- 单据文件表 
    /// </summary>
    [Serializable, TableMap("TBLINVDOC", "DOCSERIAL")]
    public class InvDoc : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public InvDoc()
        {
        }

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///服务器文件名称
        ///</summary>
        [FieldMapAttribute("SERVERFILENAME", typeof(string), 200, false)]
        public string ServerFileName;

        ///<summary>
        ///上传日期
        ///</summary>
        [FieldMapAttribute("UPFILEDATE", typeof(int), 22, false)]
        public int UpfileDate;

        ///<summary>
        ///上传人
        ///</summary>
        [FieldMapAttribute("UPUSER", typeof(string), 40, false)]
        public string UpUser;

        ///<summary>
        ///备注
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 2000, true)]
        public string Remark1;

        ///<summary>
        ///文件大小
        ///</summary>
        [FieldMapAttribute("DOCSIZE", typeof(int), 22, true)]
        public int DocSize;

        ///<summary>
        ///文件类型(文件扩展名)
        ///</summary>
        [FieldMapAttribute("DOCTYPE", typeof(string), 20, true)]
        public string DocType;

        ///<summary>
        ///文件名称
        ///</summary>
        [FieldMapAttribute("DOCNAME", typeof(string), 200, false)]
        public string DocName;

        ///<summary>
        ///单据文件类型:DirectSign:供应商直发签收文件；InitReject:初检拒收文件；InitGivein:初检让步接收文件；
        ///</summary>
        [FieldMapAttribute("INVDOCTYPE", typeof(string), 40, false)]
        public string InvDocType;

        ///<summary>
        ///单据号
        ///</summary>
        [FieldMapAttribute("INVDOCNO", typeof(string), 40, false)]
        public string InvDocNo;

        ///<summary>
        ///文件序号
        ///</summary>
        [FieldMapAttribute("DOCSERIAL", typeof(int), 22, false)]
        public int DocSerial;

    }
    #endregion

    #region Stack

    [Serializable, TableMap("TBLSTACK", "STACKCODE")]
    public class SStack : DomainObject
    {
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrgID;

        [FieldMapAttribute("STACKCODE", typeof(string), 40, false)]
        public string StackCode;

        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        [FieldMapAttribute("STACKDESC", typeof(string), 100, false)]
        public string StackDesc;

        [FieldMapAttribute("CAPACITY", typeof(Int32), 10, false)]
        public Int32 Capacity;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        [FieldMapAttribute("ISONEITEM", typeof(string), 40, true)]
        public string IsOneItem;

    }

    [Serializable]
    public class SStackWithStorageName : SStack
    {
        [FieldMapAttribute("STORAGENAME", typeof(string), 100, false)]
        public string StorageName;
    }

    [Serializable, TableMap("TBLSTACK", "STACKCODE")]
    public class StackNew : DomainObject
    {
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrgID;

        [FieldMapAttribute("STACKCODE", typeof(string), 40, false)]
        public string StackCode;

        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        [FieldMapAttribute("STACKDESC", typeof(string), 100, false)]
        public string StackDesc;

        [FieldMapAttribute("CAPACITY", typeof(Int32), 10, false)]
        public Int32 Capacity;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

    }

    #endregion

    #region StackToRcard
    [Serializable, TableMap("TBLSTACK2RCARD", "SERIALNO,CARTONCODE")]
    public class StackToRcard : DomainObject
    {
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        [FieldMapAttribute("STACKCODE", typeof(string), 40, false)]
        public string StackCode;

        ///<summary>
        ///OQCLOT
        ///</summary>
        [FieldMapAttribute("OQCLOT", typeof(string), 40, true)]
        public string Oqclot;

        ///<summary>
        ///CARTONCODE
        ///</summary>
        [FieldMapAttribute("CARTONCODE", typeof(string), 100, false)]
        public string Cartoncode;

        [FieldMapAttribute("SERIALNO", typeof(string), 40, false)]
        public string SerialNo;

        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        [FieldMapAttribute("BUSINESSREASON", typeof(string), 40, false)]
        public string BusinessReason;

        [FieldMapAttribute("COMPANY", typeof(string), 100, false)]
        public string Company;

        //[FieldMapAttribute("ITEMGRADE", typeof(string), 40, false)]
        //public string ItemGrade;

        [FieldMapAttribute("INDATE", typeof(int), 8, false)]
        public int InDate;

        [FieldMapAttribute("INTIME", typeof(int), 6, false)]
        public int InTime;

        [FieldMapAttribute("INUSER", typeof(string), 40, false)]
        public string InUser;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        [FieldMapAttribute("TRANSINSERIAL", typeof(int), 40, false)]
        public int TransInSerial;

    }
    #endregion

    #region RcardToStackPallet

    [Serializable]
    public class RcardToStackPallet : StackToRcard
    {
        [FieldMapAttribute("PALLETCODE", typeof(string), 40, false)]
        public string PalletCode;

        [FieldMapAttribute("ITEMDESC", typeof(string), 100, false)]
        public string ItemDescription;

        //[FieldMapAttribute("CARTONCODE", typeof(string), 40, false)]
        //public string CartonCode;


        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;
    }

    #endregion

    #region InvBusiness
    [Serializable, TableMap("TBLINVBUSINESS", "BUSINESSCODE,ORGID")]
    public class InvBusiness : DomainObject
    {
        [FieldMapAttribute("BUSINESSCODE", typeof(string), 40, false)]
        public string BusinessCode;

        [FieldMapAttribute("BUSINESSDESC", typeof(string), 100, false)]
        public string BusinessDescription;

        [FieldMapAttribute("BUSINESSTYPE", typeof(string), 40, false)]
        public string BusinessType;

        [FieldMapAttribute("BUSINESSREASON", typeof(string), 40, false)]
        public string BusinessReason;

        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrgID;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 是否先进先出[ISFIFO]
        /// </summary>
        [FieldMapAttribute("ISFIFO", typeof(string), 40, false)]
        public string ISFIFO;

    }
    #endregion

    #region InvInTransaction
    [Serializable, TableMap("TBLINVINTRANSACTION", "serial")]
    public class InvInTransaction : DomainObject
    {
        [FieldMapAttribute("TRANSCODE", typeof(string), 40, true)]
        public string TransCode;

        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string Rcard;

        [FieldMapAttribute("CARTONCODE", typeof(string), 100, true)]
        public string CartonCode;

        [FieldMapAttribute("PALLETCODE", typeof(string), 40, true)]
        public string PalletCode;

        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        [FieldMapAttribute("BUSINESSCODE", typeof(string), 40, false)]
        public string BusinessCode;

        [FieldMapAttribute("STACKCODE", typeof(string), 40, false)]
        public string StackCode;

        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        //[FieldMapAttribute("ITEMGRADE", typeof(string), 40, false)]
        //public string ItemGrade;

        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string SSCode;

        [FieldMapAttribute("COMPANY", typeof(string), 100, false)]
        public string Company;

        [FieldMapAttribute("BUSINESSREASON", typeof(string), 40, false)]
        public string BusinessReason;

        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrgID;

        [FieldMapAttribute("SERIAL", typeof(int), 10, false)]
        public int Serial;

        [FieldMapAttribute("DELIVERUSER", typeof(string), 40, true)]
        public string DeliverUser;

        [FieldMapAttribute("MEMO", typeof(string), 100, true)]
        public string Memo;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        [FieldMapAttribute("relateddocument", typeof(string), 100, true)]
        public string RelatedDocument;

    }

    #endregion

    #region InvOutTransaction
    [Serializable, TableMap("TBLINVOUTTRANSACTION", "serial")]
    public class InvOutTransaction : DomainObject
    {
        [FieldMapAttribute("TRANSCODE", typeof(string), 40, true)]
        public string TransCode;

        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string Rcard;

        [FieldMapAttribute("CARTONCODE", typeof(string), 100, true)]
        public string CartonCode;

        [FieldMapAttribute("PALLETCODE", typeof(string), 40, true)]
        public string PalletCode;

        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        [FieldMapAttribute("BUSINESSCODE", typeof(string), 40, false)]
        public string BusinessCode;

        [FieldMapAttribute("STACKCODE", typeof(string), 40, false)]
        public string StackCode;

        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        //[FieldMapAttribute("ITEMGRADE", typeof(string), 40, false)]
        //public string ItemGrade;


        [FieldMapAttribute("COMPANY", typeof(string), 100, false)]
        public string Company;

        [FieldMapAttribute("BUSINESSREASON", typeof(string), 40, false)]
        public string BusinessReason;

        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrgID;

        [FieldMapAttribute("SERIAL", typeof(int), 10, false)]
        public int Serial;

        [FieldMapAttribute("MEMO", typeof(string), 100, true)]
        public string Memo;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        [FieldMapAttribute("DNLINE", typeof(string), 6, false)]
        public string DNLine;

        [FieldMapAttribute("TRANSINSERIAL", typeof(int), 40, false)]
        public int TransInSerial;
    }

    #endregion

    #region InvFormula

    [Serializable, TableMap("TBLINVFORMULA", "FORMULACODE")]
    public class InvFormula : DomainObject
    {

        [FieldMapAttribute("FORMULACODE", typeof(string), 40, false)]
        public string FormulaCode;

        [FieldMapAttribute("FORMULADESC", typeof(string), 100, true)]
        public string FormulaDesc;


        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;


        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

    }

    #endregion

    #region InvPeriod

    [Serializable, TableMap("TBLINVPERIOD", "INVPERIODCODE,PEIODGROUP")]
    public class InvPeriod : DomainObject
    {

        [FieldMapAttribute("INVPERIODCODE", typeof(string), 40, false)]
        public string InvPeriodCode;

        [FieldMapAttribute("PEIODGROUP", typeof(string), 40, false)]
        public string PeiodGroup;

        [FieldMapAttribute("DATEFROM", typeof(int), 8, true)]
        public int DateFrom;


        [FieldMapAttribute("DATETO", typeof(int), 8, true)]
        public int DateTo;

        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;


        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;
    }

    #endregion

    #region InventoryPeriodStandard

    /// <summary>
    ///	InventoryPeriodStandard
    /// </summary>
    [Serializable, TableMap("TBLINVPERIODSTD", "INVTYPE, PERIODGROUP, INVPERIODCODE")]
    public class InventoryPeriodStandard : DomainObject
    {
        public InventoryPeriodStandard()
        {
        }

        ///<summary>
        ///InventoryType
        ///</summary>	
        [FieldMapAttribute("INVTYPE", typeof(string), 40, false)]
        public string InventoryType;

        ///<summary>
        ///PeriodGROUP
        ///</summary>	
        [FieldMapAttribute("PERIODGROUP", typeof(string), 40, false)]
        public string PeriodGroup;

        ///<summary>
        ///InventoryPeriodCode
        ///</summary>	
        [FieldMapAttribute("INVPERIODCODE", typeof(string), 40, false)]
        public string InventoryPeriodCode;

        ///<summary>
        ///PercentageStandard
        ///</summary>	
        [FieldMapAttribute("PERCENTAGESTD", typeof(decimal), 8, false)]
        public decimal PercentageStandard;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

    }

    #endregion

    #region ProductInvPeriod

    /// <summary>
    ///	ProductInvPeriod
    /// </summary>
    [Serializable, TableMap("", "")]
    public class ProductInvPeriod : DomainObject
    {
        public ProductInvPeriod()
        {
        }

        [FieldMapAttribute("INVTYPE", typeof(string), 40, true)]
        public string InventoryType;

        [FieldMapAttribute("INVPERIODCODE", typeof(string), 40, true)]
        public string InventoryPeriodCode;

        [FieldMapAttribute("DATEFROM", typeof(int), 22, true)]
        public int DateFrom;

        [FieldMapAttribute("DATETO", typeof(int), 22, true)]
        public int DateTo;

        [FieldMapAttribute("PERCENTAGESTD", typeof(decimal), 8, true)]
        public decimal PercentageStandard;

        [FieldMapAttribute("PRODUCTCOUNT", typeof(int), 22, true)]
        public int ProductCount;

        public bool IsForTitle = false;
        public List<ProductInvPeriod> DataToGrid = new List<ProductInvPeriod>();
    }

    #endregion

    #region StorageAttribute

    [Serializable, TableMap(" (SELECT paramcode, paramdesc FROM tblsysparam WHERE paramgroupcode = 'STORAGEATTRIBUTE' ) storageattributeparam ", "")]
    public class StorageAttributeParam : DomainObject
    {
        public StorageAttributeParam()
        {
        }

        [FieldMapAttribute("PARAMCODE", typeof(string), 40, true)]
        public string ParamCode;

        [FieldMapAttribute("PARAMDESC", typeof(string), 40, true)]
        public string ParamDesc;
    }

    #endregion

    #region InvBusiness2Formula

    [Serializable, TableMap("TBLINVBUSINESS2FORMULA", "BUSINESSCODE,FORMULACODE,ORGID")]
    public class InvBusiness2Formula : DomainObject
    {

        [FieldMapAttribute("BUSINESSCODE", typeof(string), 40, false)]
        public string BusinessCode;


        [FieldMapAttribute("FORMULACODE", typeof(string), 40, false)]
        public string FormulaCode;


        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrgID;

        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;


        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

    }

    #endregion

    #region Pause

    [Serializable, TableMap("TBLPAUSE", "PAUSECODE")]
    public class Pause : DomainObject
    {

        [FieldMapAttribute("PAUSECODE", typeof(string), 40, false)]
        public string PauseCode;

        [FieldMapAttribute("PAUSEREASON", typeof(string), 200, true)]
        public string PauseReason;

        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        [FieldMapAttribute("CANCELREASON", typeof(string), 200, true)]
        public string CancelReason;

        [FieldMapAttribute("PDATE", typeof(int), 8, false)]
        public int PDate;


        [FieldMapAttribute("PTIME", typeof(int), 6, false)]
        public int PTime;

        [FieldMapAttribute("PUSER", typeof(string), 40, false)]
        public string PUser;

        [FieldMapAttribute("CDATE", typeof(int), 8, true)]
        public int CancelDate;


        [FieldMapAttribute("CTIME", typeof(int), 6, true)]
        public int CancelTime;

        [FieldMapAttribute("CUSER", typeof(string), 40, true)]
        public string CancelUser;

        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;


        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

    }

    #endregion

    #region DNTempOut

    [Serializable, TableMap("TBLDNTEMPOUT", "STACKCODE,ITEMCODE,DNNO,DNLINE")]
    public class DNTempOut : DomainObject
    {

        [FieldMapAttribute("STACKCODE", typeof(string), 40, false)]
        public string StackCode;

        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        [FieldMapAttribute("DNNO", typeof(string), 40, false)]
        public string DNNO;

        [FieldMapAttribute("DNLINE", typeof(string), 6, false)]
        public string DNLine;

        [FieldMapAttribute("TEMPQTY", typeof(int), 10, false)]
        public int TempQty;

        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;
    }

    #endregion

    #region Pause2Rcard

    [Serializable, TableMap("TBLPAUSE2RCARD", "SERIALNO,PAUSECODE")]
    public class Pause2Rcard : DomainObject
    {
        [FieldMapAttribute("PAUSECODE", typeof(string), 40, false)]
        public string PauseCode;

        [FieldMapAttribute("SERIALNO", typeof(string), 40, false)]
        public string SerialNo;

        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        [FieldMapAttribute("CANCELSEQ", typeof(string), 40, true)]
        public string CancelSeq;

        [FieldMapAttribute("BOM", typeof(string), 40, true)]
        public string BOM;

        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;


        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        [FieldMapAttribute("CANCELREASON", typeof(string), 200, true)]
        public string CancelReason;

        [FieldMapAttribute("PDATE", typeof(int), 8, false)]
        public int PDate;

        [FieldMapAttribute("PTIME", typeof(int), 6, false)]
        public int PTime;

        [FieldMapAttribute("PUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string PUser;

        [FieldMapAttribute("CDATE", typeof(int), 8, true)]
        public int CancelDate;


        [FieldMapAttribute("CTIME", typeof(int), 6, true)]
        public int CancelTime;

        [FieldMapAttribute("CUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string CancelUser;

        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;


        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

    }

    #endregion

    #region PauseQuery

    [Serializable]
    public class PauseQuery : Pause2Rcard
    {
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLSTORAGE", "STORAGECODE", "STORAGENAME")]
        public string StorageCode;

        [FieldMapAttribute("STACKCODE", typeof(string), 100, true)]
        public string StackCode;

        [FieldMapAttribute("PALLETCODE", typeof(string), 40, true)]
        public string PalletCode;

        [FieldMapAttribute("RCARDCOUNT", typeof(decimal), 8, true)]
        public decimal RcardCount;

        [FieldMapAttribute("MCODE", typeof(string), 40, true)]
        public string MCode;

        [FieldMapAttribute("MDESC", typeof(string), 40, true)]
        public string MDesc;

        [FieldMapAttribute("CARTONCODE", typeof(string), 40, true)]
        public string CartonCode;

        [FieldMapAttribute("PAUSEQTY", typeof(string), 40, true)]
        public string PauseQty;

        [FieldMapAttribute("CANCELQTY", typeof(string), 40, true)]
        public string CancelQty;

        [FieldMapAttribute("MMODELCODE", typeof(string), 40, true)]
        public string MModelCode;

        [FieldMapAttribute("PAUSEREASON", typeof(string), 200, true)]
        public string PauseReason;
    }

    #endregion

    #region PauseSetting

    [Serializable]
    public class PauseSetting : DomainObject
    {
        [FieldMapAttribute("itemcode", typeof(string), 40, false)]
        public string ItemCode;

        [FieldMapAttribute("mdesc", typeof(string), 100, true)]
        public string ItemDescription;

        [FieldMapAttribute("mobom", typeof(string), 40, true)]
        public string BOM;

        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, true)]
        public string BigSSCode;

        [FieldMapAttribute("qty", typeof(decimal), 0, true)]
        public decimal qty;

        [FieldMapAttribute("palletcode", typeof(string), 40, true)]
        public string PalletCode;

        [FieldMapAttribute("rcard", typeof(string), 40, true)]
        public string Rcard;

        [FieldMapAttribute("mocode", typeof(string), 40, true)]
        public string MOCode;


        [FieldMapAttribute("finisheddate", typeof(Int32), 8, true)]
        public Int32 FinishedDate;

        [FieldMapAttribute("indate", typeof(Int32), 8, true)]
        public Int32 InInvDate;
    }

    #endregion

    #region CancelPauseQuery

    [Serializable]
    public class CancelPauseQuery : DomainObject
    {
        [FieldMapAttribute("storagecode", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLSTORAGE", "STORAGECODE", "STORAGENAME")]
        public string StorageCode;

        [FieldMapAttribute("stackcode", typeof(string), 40, true)]
        public string StackCode;

        [FieldMapAttribute("itemcode", typeof(string), 40, false)]
        public string ItemCode;

        [FieldMapAttribute("mdesc", typeof(string), 100, true)]
        public string ItemDescription;

        [FieldMapAttribute("status", typeof(string), 40, false)]
        public string Status;

        [FieldMapAttribute("pausecode", typeof(string), 40, false)]
        public string PauseCode;

        [FieldMapAttribute("pauseqty", typeof(decimal), 0, false)]
        public decimal PauseQty;

        [FieldMapAttribute("cancelqty", typeof(decimal), 0, false)]
        public decimal CancelQty;
    }

    #endregion

    #region StackMessage

    [Serializable]
    public class StackMessage : DomainObject
    {
        [FieldMapAttribute("stackcode", typeof(string), 40, true)]
        public string StackCode;

        [FieldMapAttribute("STACKQTYMESSAGE", typeof(string), 40, true)]
        public string StackQtyMessage;
    }

    #endregion

    #region DNTempOutMessage

    [Serializable]
    public class DNTempOutMessage : StackToRcard
    {

        [FieldMapAttribute("STORAGENAME", typeof(string), 100, true)]
        public string StorageName;

        [FieldMapAttribute("MNAME", typeof(string), 100, true)]
        public string ItemDescription;

        [FieldMapAttribute("MMODELCODE", typeof(string), 40, true)]
        public string MModelCode;

        [FieldMapAttribute("INVQTY", typeof(int), 10, true)]
        public int INVQTY;

        [FieldMapAttribute("COMQTY", typeof(int), 10, true)]
        public int COMQTY;

        [FieldMapAttribute("SAPQTY", typeof(int), 10, true)]
        public int SAPQTY;

        [FieldMapAttribute("TEMPQTY", typeof(int), 10, true)]
        public int TEMPQTY;
    }

    #endregion

    #region MaterialBusiness(物料出入库类型)
    [Serializable, TableMap("TBLMATERIALBUSINESS", "BUSINESSCODE")]
    public class MaterialBusiness : DomainObject
    {
        /// <summary>
        /// 业务类型代码
        /// </summary>
        [FieldMapAttribute("BusinessCode", typeof(string), 40, false)]
        public string BusinessCode;

        /// <summary>
        /// 业务类型描述
        /// </summary>
        [FieldMapAttribute("BusinessDesc", typeof(string), 100, false)]
        public string BusinessDesc;

        /// <summary>
        /// 业务类型
        /// </summary>
        [FieldMapAttribute("BusinessType", typeof(string), 40, false)]
        public string BusinessType;

        /// <summary>
        /// SAP 业务代码
        /// </summary>
        [FieldMapAttribute("SAPCODE", typeof(string), 40, false)]
        public string SAPCODE;

        /// <summary>
        /// 组织ID
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrgID;

        /// <summary>
        /// FIFO检查
        /// </summary>
        [FieldMapAttribute("ISFIFO", typeof(string), 40, false)]
        public string ISFIFO;

        /// <summary>
        /// 维护日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 维护时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 维护人
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;
    }
    #endregion

    #region InvIntransSum
    [Serializable, TableMap("TBLINVINTRANSSUM", "")]
    public class InvIntransSum : DomainObject
    {
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        [FieldMapAttribute("STACKCODE", typeof(string), 40, false)]
        public string StackCode;

        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        //[FieldMapAttribute("ITEMGRADE", typeof(string), 40, false)]
        //public string ItemGrade;

        [FieldMapAttribute("INQTY", typeof(int), 22, false)]
        public int InQty;
    }

    #endregion

    #region MaterialFull 物料齐套查询类型
    public class MaterialFull : DomainObject
    {
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MoCode;
        [FieldMapAttribute("MCODE", typeof(string), 40, true)]
        public string MCode;
        [FieldMapAttribute("PLANTYPE", typeof(string), 8, true)]
        public string PlanType;
        [FieldMapAttribute("LOSTQTY", typeof(decimal), 22, true)]
        public decimal LostQty;
        [FieldMapAttribute("SUMQTY", typeof(decimal), 22, true)]
        public decimal SumQty;
        [FieldMapAttribute("QTY", typeof(decimal), 22, true)]
        public decimal Qty;
        [FieldMapAttribute("SHORTQTY", typeof(decimal), 22, true)]
        public decimal ShortQty;
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string Mdesc;
        [FieldMapAttribute("MODESC", typeof(string), 200, true)]
        public string Modesc;

        ///<summary>
        ///QUERYSEQ
        ///</summary>
        [FieldMapAttribute("QUERYSEQ", typeof(int), 22, false)]
        public int Queryseq;

        ///<summary>
        ///PLANDATE
        ///</summary>
        [FieldMapAttribute("PLANDATE", typeof(int), 22, false)]
        public int Plandate;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string Itemcode;

        ///<summary>
        ///ORGID
        ///</summary>
        [FieldMapAttribute("ORGID", typeof(int), 22, false)]
        public int Orgid;

        ///<summary>
        ///PLANQTY
        ///</summary>
        [FieldMapAttribute("PLANQTY", typeof(int), 22, false)]
        public int Planqty;

        ///<summary>
        ///MUSER
        ///</summary>
        ///
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;
    }
    #endregion

    #region TBLMSDLevel
    [Serializable, TableMap("TBLMSDLevel", "MHumidityLevel")]
    public class MSDLevel : DomainObject
    {
        /// <summary>
        /// 湿敏等级
        /// </summary>
        [FieldMapAttribute("MHUMIDITYLEVEL", typeof(string), 40, false)]
        public string MHumidityLevel;

        /// <summary>
        /// 湿敏等级描述
        /// </summary>
        [FieldMapAttribute("MHUMIDITYLEVELDESC", typeof(string), 100, true)]
        public string MHumidityLevelDesc;

        /// <summary>
        ///有效车间寿命
        /// </summary>
        [FieldMapAttribute("FLOORLIFE", typeof(int), 10, false)]
        public int FloorLife;

        /// <summary>
        ///干燥箱最小干燥时间
        /// </summary>
        [FieldMapAttribute("DRYINGTIME", typeof(int), 10, false)]
        public int DryingTime;

        /// <summary>
        ///暴露时间
        /// </summary>
        [FieldMapAttribute("INDRYINGTIME", typeof(int), 10, false)]
        public int INDryingTime;

        /// <summary>
        /// 维护日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 维护时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 维护人
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;
    }
    #endregion

    #region Customer
    /// <summary>
    /// Customer
    /// </summary>
    [Serializable, TableMap("TBLCUSTOMER", "CUSTOMERCODE")]
    public class Customer : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Customer()
        {
        }

        ///<summary>
        ///CUSTOMERCODE
        ///</summary>
        [FieldMapAttribute("CUSTOMERCODE", typeof(string), 40, false)]
        public string CustomerCode;

        ///<summary>
        ///CUSTOMERNAME
        ///</summary>
        [FieldMapAttribute("CUSTOMERNAME", typeof(string), 100, false)]
        public string CustomerName;

        ///<summary>
        ///EATTRIBUTE3
        ///</summary>
        [FieldMapAttribute("ADDRESS", typeof(string), 100, true)]
        public string ADDRESS;

        ///<summary>
        ///EATTRIBUTE3
        ///</summary>
        [FieldMapAttribute("TEL", typeof(string), 16, true)]
        public string TEL;

        ///<summary>
        ///EATTRIBUTE3
        ///</summary>
        [FieldMapAttribute("FLAG", typeof(string), 2, true)]
        public string FLAG;


        ///<summary>
        ///EATTRIBUTE1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;
        ///<summary>
        ///EATTRIBUTE2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 40, true)]
        public string Eattribute2;
        ///<summary>
        ///EATTRIBUTE3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 40, true)]
        public string Eattribute3;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string Muser;

    }
    #endregion

    #region TBLMaterialMSL
    [Serializable, TableMap("TBLMaterialMSL", "MCODE,ORGID")]
    public class MaterialMSL : DomainObject
    {
        /// <summary>
        /// 物料代码
        /// </summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        /// <summary>
        /// 组织ID
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrgID;

        /// <summary>
        /// 湿敏等级
        /// </summary>
        [FieldMapAttribute("MHUMIDITYLEVEL", typeof(string), 40, false)]
        public string MHumidityLevel;

        ///// <summary>
        /////干燥箱最小干燥时间
        ///// </summary>
        //[FieldMapAttribute("DRYINGTIME", typeof(int), 10, false)]
        //public int DryingTime;

        ///// <summary>
        /////拆封暴露在空气中未使用需进入干燥箱的时间(暴露时间)
        ///// </summary>
        //[FieldMapAttribute("INDRYINGTIME", typeof(int), 10, false)]
        //public int InDryingTime;

        /// <summary>
        /// 维护日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 维护时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 维护人
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;
    }
    #endregion

    #region STORAGEINFO jinger注释 20160127
    
    /// <summary>
    /// TBLSTORAGEINFO
    /// </summary>
    [Serializable, TableMap("TBLSTORAGEINFO", "STORAGEID,MCODE,STACKCODE")]
    public class StorageInfo : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public StorageInfo()
        {
        }

        ///<summary>
        ///MCODE
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string Mcode;

        ///<summary>
        ///STORAGEID
        ///</summary>
        [FieldMapAttribute("STORAGEID", typeof(string), 40, false)]
        public string Storageid;

        ///<summary>
        ///STACKCODE
        ///</summary>
        [FieldMapAttribute("STACKCODE", typeof(string), 40, false)]
        public string Stackcode;

        ///<summary>
        ///STORAGEQTY
        ///</summary>
        [FieldMapAttribute("STORAGEQTY", typeof(decimal), 22, false)]
        public decimal Storageqty;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string Muser;

        ///<summary>
        ///Organization ID
        ///</summary>	
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;

    }
    #endregion

    #region STORAGELOTINFO
    /// <summary>
    /// TBLSTORAGELOTINFO
    /// </summary>
    [Serializable, TableMap("TBLSTORAGELOTINFO", "LOTNO,STORAGEID,STACKCODE,MCODE")]
    public class StorageLotInfo : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public StorageLotInfo()
        {
        }

        ///<summary>
        ///LOTNO
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 100, false)]
        public string Lotno;

        ///<summary>
        ///STORAGEID
        ///</summary>
        [FieldMapAttribute("STORAGEID", typeof(string), 40, false)]
        public string Storageid;

        ///<summary>
        ///STACKCODE
        ///</summary>
        [FieldMapAttribute("STACKCODE", typeof(string), 40, false)]
        public string Stackcode;

        ///<summary>
        ///MCODE
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string Mcode;

        ///<summary>
        ///LOTQTY
        ///</summary>
        [FieldMapAttribute("LOTQTY", typeof(decimal), 28, false)]
        public decimal Lotqty;

        ///<summary>
        ///RECEIVEDATE
        ///</summary>
        [FieldMapAttribute("RECEIVEDATE", typeof(int), 22, false)]
        public int Receivedate;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string Muser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

    }
    #endregion

    #region ITEMTRANS
    /// <summary>
    /// TBLITEMTRANS
    /// </summary>
    [Serializable, TableMap("TBLITEMTRANS", "SERIAL")]
    public class ItemTrans : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public ItemTrans()
        {
        }

        ///<summary>
        ///SERIAL
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        ///TRANSNO
        ///</summary>
        [FieldMapAttribute("TRANSNO", typeof(string), 50, true)]
        public string Transno;

        ///<summary>
        ///TRANSLINE
        ///</summary>
        [FieldMapAttribute("TRANSLINE", typeof(int), 22, true)]
        public int Transline;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string Itemcode;

        ///<summary>
        ///FRMSTORAGEID
        ///</summary>
        [FieldMapAttribute("FRMSTORAGEID", typeof(string), 40, true)]
        public string Frmstorageid;

        ///<summary>
        ///FRMSTACKCODE
        ///</summary>
        [FieldMapAttribute("FRMSTACKCODE", typeof(string), 40, true)]
        public string Frmstackcode;

        ///<summary>
        ///TOSTORAGEID
        ///</summary>
        [FieldMapAttribute("TOSTORAGEID", typeof(string), 40, true)]
        public string Tostorageid;

        ///<summary>
        ///TOSTACKCODE
        ///</summary>
        [FieldMapAttribute("TOSTACKCODE", typeof(string), 40, true)]
        public string Tostackcode;

        ///<summary>
        ///TRANSQTY
        ///</summary>
        [FieldMapAttribute("TRANSQTY", typeof(decimal), 22, false)]
        public decimal Transqty;

        ///<summary>
        ///MEMO
        ///</summary>
        [FieldMapAttribute("MEMO", typeof(string), 1000, true)]
        public string Memo;

        ///<summary>
        ///TRANSTYPE
        ///</summary>
        [FieldMapAttribute("TRANSTYPE", typeof(string), 40, false)]
        public string Transtype;

        ///<summary>
        ///BUSINESSCODE
        ///</summary>
        [FieldMapAttribute("BUSINESSCODE", typeof(string), 40, false)]
        public string Businesscode;

        ///<summary>
        ///ORGID
        ///</summary>
        [FieldMapAttribute("ORGID", typeof(int), 22, false)]
        public int Orgid;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string Muser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

    }
    #endregion

    #region ITEMTRANSLOT
    /// <summary>
    /// TBLITEMTRANSLOT
    /// </summary>
    [Serializable, TableMap("TBLITEMTRANSLOT", "LOTNO,TBLITEMTRANS_SERIAL")]
    public class ItemTransLot : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public ItemTransLot()
        {
        }

        ///<summary>
        ///TBLITEMTRANS_SERIAL
        ///</summary>
        [FieldMapAttribute("TBLITEMTRANS_SERIAL", typeof(int), 22, false)]
        public int Tblitemtrans_serial;

        ///<summary>
        ///LOTNO
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 100, false)]
        public string Lotno;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string Itemcode;

        ///<summary>
        ///TRANSQTY
        ///</summary>
        [FieldMapAttribute("TRANSQTY", typeof(decimal), 28, false)]
        public decimal Transqty;

        ///<summary>
        ///MEMO
        ///</summary>
        [FieldMapAttribute("MEMO", typeof(string), 1000, true)]
        public string Memo;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string Muser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

    }
    #endregion

    #region ITEMTRANSLOTDETAIL
    /// <summary>
    /// TBLITEMTRANSLOTDETAIL
    /// </summary>
    [Serializable, TableMap("TBLITEMTRANSLOTDETAIL", "TBLITEMTRANS_SERIAL,LOTNO,SERIALNO")]
    public class ItemTransLotDetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public ItemTransLotDetail()
        {
        }

        ///<summary>
        ///TBLITEMTRANS_SERIAL
        ///</summary>
        [FieldMapAttribute("TBLITEMTRANS_SERIAL", typeof(int), 22, false)]
        public int Tblitemtrans_serial;

        ///<summary>
        ///LOTNO
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 100, false)]
        public string Lotno;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string Itemcode;

        ///<summary>
        ///SERIALNO
        ///</summary>
        [FieldMapAttribute("SERIALNO", typeof(string), 40, false)]
        public string Serialno;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string Muser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

    }
    #endregion

    #region ITEMTRANSLOTFORTRANS
    public class ItemTransLotForTrans : ItemTransLot
    {
        public ItemTransLotForTrans()
        {
        }
        ///<summary>
        ///FRMSTORAGEQTY
        ///</summary>
        [FieldMapAttribute("FRMSTORAGEQTY", typeof(decimal), 28, true)]
        public decimal FrmStorageQty;

        ///<summary>
        ///SERIAL
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        ///TRANSNO
        ///</summary>
        [FieldMapAttribute("TRANSNO", typeof(string), 40, true)]
        public string Transno;

        ///<summary>
        ///STACKCODE
        ///</summary>
        [FieldMapAttribute("STACKCODE", typeof(string), 40, false)]
        public string Stackcode;

        ///<summary>
        ///TRANSLINE
        ///</summary>
        [FieldMapAttribute("TRANSLINE", typeof(int), 22, true)]
        public int Transline;
    }
    #endregion

    #region ITEMTRANSLOTDETAILFORTRANS
    public class ItemTransLotDetailForTrans : ItemTransLotDetail
    {
        public ItemTransLotDetailForTrans()
        {
        }
        ///<summary>
        ///STACKCODE
        ///</summary>
        [FieldMapAttribute("STACKCODE", typeof(string), 40, false)]
        public string Stackcode;
    }
    #endregion

    [Serializable]
    public class MaterialMSLExc : MaterialMSL
    {
        [FieldMapAttribute("MaterialName", typeof(string), 40, true)]
        public string MaterialName;

        [FieldMapAttribute("MaterialDes", typeof(string), 40, true)]
        public string MaterialDes;

        [FieldMapAttribute("MHUMIDITYLEVELDESC", typeof(string), 40, true)]
        public string MHumidityLevelDesc;

        [FieldMapAttribute("FLOORLIFE", typeof(int), 10, false)]
        public int FloorLife;

        [FieldMapAttribute("DRYINGTIME", typeof(int), 10, false)]
        public int DryingTime;

        [FieldMapAttribute("INDRYINGTIME", typeof(int), 10, false)]
        public int InDryingTime;
    }


    #region TBLITEMLot
    [Serializable, TableMap("TBLITEMLot", "LotNO")]
    public class ITEMLot : DomainObject
    {
        /// <summary>
        /// 物料批号
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 100, false)]
        public string LotNO;

        /// <summary>
        /// 物料编号
        /// </summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        /// <summary>
        /// 组织ID
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrgID;

        /// <summary>
        /// 交易单号
        /// </summary>
        [FieldMapAttribute("TRANSNO", typeof(string), 50, true)]
        public string TransNO;

        /// <summary>
        /// 交易单行号
        /// </summary>
        [FieldMapAttribute("TRANSLINE", typeof(int), 8, true)]
        public int TransLine;

        /// <summary>
        /// 厂商物料代码
        /// </summary>
        [FieldMapAttribute("VENDORITEMCODE", typeof(string), 100, true)]
        public string VendorItemCode;

        /// <summary>
        /// 厂商代码
        /// </summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 100, true)]
        public string VendorCode;

        /// <summary>
        /// 供应商物料批号
        /// </summary>
        [FieldMapAttribute("VENDERLOTNO", typeof(string), 40, true)]
        public string VenderLotNO;

        /// <summary>
        /// 生产日期
        /// </summary>
        [FieldMapAttribute("DATECODE", typeof(int), 8, false)]
        public int DateCode;

        /// <summary>
        /// 原始批次数量
        /// </summary>
        [FieldMapAttribute("LOTQTY", typeof(int), 13, false)]
        public int LotQty;

        /// <summary>
        /// 是否有效
        /// </summary>
        [FieldMapAttribute("ACTIVE", typeof(string), 1, false)]
        public string Active;

        /// <summary>
        /// 失效日期
        /// </summary>
        [FieldMapAttribute("EXDATE", typeof(int), 8, false)]
        public int Exdate;

        /// <summary>
        /// 打印次数
        /// </summary>
        [FieldMapAttribute("PRINTTIMES", typeof(int), 6, false)]
        public int PrintTimes;

        /// <summary>
        /// 最后打印人
        /// </summary>
        [FieldMapAttribute("LASTPRINTUSER", typeof(string), 40, false)]
        public string lastPrintUSER;

        /// <summary>
        /// 最后打印日期
        /// </summary>
        [FieldMapAttribute("LASTPRINTDATE", typeof(int), 8, false)]
        public int lastPrintDate;

        /// <summary>
        /// 最后打印时间
        /// </summary>
        [FieldMapAttribute("LASTPRINTTIME", typeof(int), 6, false)]
        public int lastPrintTime;

        /// <summary>
        /// 维护日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 维护时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 维护人
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;
    }
    #endregion

    #region ITEMLOT
    /// <summary>
    /// TBLITEMLOT
    /// </summary>
    [Serializable, TableMap("TBLITEMLOT", "LOTNO,MCODE")]
    public class ItemLot : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public ItemLot()
        {
        }

        ///<summary>
        ///LOTNO
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 100, false)]
        public string Lotno;

        ///<summary>
        ///MCODE
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string Mcode;

        ///<summary>
        ///ORGID
        ///</summary>
        [FieldMapAttribute("ORGID", typeof(int), 22, false)]
        public int Orgid;

        ///<summary>
        ///TRANSNO
        ///</summary>
        [FieldMapAttribute("TRANSNO", typeof(string), 50, true)]
        public string Transno;

        ///<summary>
        ///TRANSLINE
        ///</summary>
        [FieldMapAttribute("TRANSLINE", typeof(int), 22, true)]
        public int Transline;

        ///<summary>
        ///VENDORITEMCODE
        ///</summary>
        [FieldMapAttribute("VENDORITEMCODE", typeof(string), 100, true)]
        public string Vendoritemcode;

        ///<summary>
        ///VENDORCODE
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 100, true)]
        public string Vendorcode;

        ///<summary>
        ///VENDERLOTNO
        ///</summary>
        [FieldMapAttribute("VENDERLOTNO", typeof(string), 40, true)]
        public string Venderlotno;

        ///<summary>
        ///DATECODE
        ///</summary>
        [FieldMapAttribute("DATECODE", typeof(int), 8, true)]
        public int Datecode;

        ///<summary>
        ///LOTQTY
        ///</summary>
        [FieldMapAttribute("LOTQTY", typeof(decimal), 28, false)]
        public decimal Lotqty;

        ///<summary>
        ///ACTIVE
        ///</summary>
        [FieldMapAttribute("ACTIVE", typeof(string), 1, false)]
        public string Active;

        ///<summary>
        ///EXDATE
        ///</summary>
        [FieldMapAttribute("EXDATE", typeof(int), 8, false)]
        public int Exdate;

        ///<summary>
        ///PRINTTIMES
        ///</summary>
        [FieldMapAttribute("PRINTTIMES", typeof(int), 6, false)]
        public int Printtimes;

        ///<summary>
        ///LASTPRINTUSER
        ///</summary>
        [FieldMapAttribute("LASTPRINTUSER", typeof(string), 40, false)]
        public string Lastprintuser;

        ///<summary>
        ///LASTPRINTDATE
        ///</summary>
        [FieldMapAttribute("LASTPRINTDATE", typeof(int), 8, false)]
        public int Lastprintdate;

        ///<summary>
        ///LASTPRINTTIME
        ///</summary>
        [FieldMapAttribute("LASTPRINTTIME", typeof(int), 6, false)]
        public int Lastprinttime;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
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

    #region ItemLotForQuery
    /// <summary>
    /// ItemLotForQuery
    /// </summary>

    public class ItemLotForQuery : ItemLot
    {
        public ItemLotForQuery()
        {
        }


        ///<summary>
        ///VENDORNAME
        ///</summary>
        [FieldMapAttribute("VENDORNAME", typeof(string), 100, false)]
        public string VendorName;


        ///<summary>
        ///Material Description
        ///</summary>	
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MaterialDescription;

        ///<summary>
        ///Material Name
        ///</summary>	
        [FieldMapAttribute("MNAME", typeof(string), 40, true)]
        public string MaterialName;

        ///<summary>
        ///StorageID
        ///</summary>	
        [FieldMapAttribute("STORAGEID", typeof(string), 40, false)]
        public string StorageID;


        ///<summary>
        ///STACKCODE
        ///</summary>	
        [FieldMapAttribute("STACKCODE", typeof(string), 40, false)]
        public string StackCode;

    }
    #endregion

    #region TBLITEMLOTFORTRANS
    /// <summary>
    /// TBLITEMLOTFORTRANS
    /// </summary> 
    public class ItemLotForTrans : ItemLot
    {
        public ItemLotForTrans()
        {
        }
        ///<summary>
        ///FRMSTORAGEQTY
        ///</summary>
        [FieldMapAttribute("FRMSTORAGEQTY", typeof(decimal), 28, true)]
        public decimal FrmStorageQty;

        ///<summary>
        ///STACKCODE
        ///</summary>
        [FieldMapAttribute("STACKCODE", typeof(string), 40, true)]
        public string Stackcode;

    }
    #endregion

    #region ITEMLOTDETAIL
    /// <summary>
    /// TBLITEMLOTDETAIL
    /// </summary>
    [Serializable, TableMap("TBLITEMLOTDETAIL", "SERIALNO,MCODE")]
    public class ItemLotDetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public ItemLotDetail()
        {
        }

        ///<summary>
        ///SERIALNO
        ///</summary>
        [FieldMapAttribute("SERIALNO", typeof(string), 40, false)]
        public string Serialno;

        ///<summary>
        ///LOTNO
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string Lotno;

        ///<summary>
        ///MCODE
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string Mcode;

        ///<summary>
        ///STORAGEID
        ///</summary>
        [FieldMapAttribute("STORAGEID", typeof(string), 40, true)]
        public string Storageid;

        ///<summary>
        ///STACKCODE
        ///</summary>
        [FieldMapAttribute("STACKCODE", typeof(string), 40, true)]
        public string Stackcode;

        ///<summary>
        ///SERIALSTATUS
        ///</summary>
        [FieldMapAttribute("SERIALSTATUS", typeof(string), 40, false)]
        public string Serialstatus;

        ///<summary>
        ///PRINTTIMES
        ///</summary>
        [FieldMapAttribute("PRINTTIMES", typeof(int), 22, false)]
        public int Printtimes;

        ///<summary>
        ///LASTPRINTUSER
        ///</summary>
        [FieldMapAttribute("LASTPRINTUSER", typeof(string), 40, false)]
        public string Lastprintuser;

        ///<summary>
        ///LASTPRINTDATE
        ///</summary>
        [FieldMapAttribute("LASTPRINTDATE", typeof(int), 8, false)]
        public int Lastprintdate;

        ///<summary>
        ///LASTPRINTTIME
        ///</summary>
        [FieldMapAttribute("LASTPRINTTIME", typeof(int), 6, false)]
        public int Lastprinttime;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
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

    #region TBLITEMLOTDETAILFORTRANS
    /// <summary>
    /// TBLITEMLOTDETAILFORTRANS
    /// </summary> 
    public class ItemLotDetailForTrans : ItemLotDetail
    {
        public ItemLotDetailForTrans()
        {
        }
        ///<summary>
        ///FRMSTORAGEQTY
        ///</summary>
        [FieldMapAttribute("FRMSTORAGEQTY", typeof(decimal), 28, true)]
        public decimal FrmStorageQty;

        ///<summary>
        ///LOTQTY
        ///</summary>
        [FieldMapAttribute("LOTQTY", typeof(decimal), 28, false)]
        public decimal Lotqty;
    }
    #endregion

    #region LOTCHANGELOG
    /// <summary>
    /// TBLLOTCHANGELOG
    /// </summary>
    [Serializable, TableMap("TBLLOTCHANGELOG", "SERIAL")]
    public class LotChangeLog : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public LotChangeLog()
        {
        }

        ///<summary>
        ///SERIAL
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///NEWLOTNO
        ///</summary>
        [FieldMapAttribute("NEWLOTNO", typeof(string), 100, false)]
        public string Newlotno;

        ///<summary>
        ///NEWLOTQTY
        ///</summary>
        [FieldMapAttribute("NEWLOTQTY", typeof(decimal), 22, false)]
        public decimal Newlotqty;

        ///<summary>
        ///OLDLOTNO
        ///</summary>
        [FieldMapAttribute("OLDLOTNO", typeof(string), 100, false)]
        public string Oldlotno;

        ///<summary>
        ///OLDLOTQTY
        ///</summary>
        [FieldMapAttribute("OLDLOTQTY", typeof(decimal), 22, false)]
        public decimal Oldlotqty;

        ///<summary>
        ///CHGTYPE
        ///</summary>
        [FieldMapAttribute("CHGTYPE", typeof(string), 40, false)]
        public string Chgtype;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string Muser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

    }
    #endregion

    #region TBLMSDLOT
    [Serializable, TableMap("TBLMSDLOT", "LotNo")]
    public class MSDLOT : DomainObject
    {
        /// <summary>
        /// 物料批号
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNo;

        /// <summary>
        /// MSD状态
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        /// <summary>
        ///有效车间寿命（小时）
        /// </summary>
        [FieldMapAttribute("FLOORlIFE", typeof(decimal), 15, false)]
        public decimal Floorlife;

        /// <summary>
        ///剩余车间寿命（小时）
        /// </summary>
        [FieldMapAttribute("OVERFLOORlIFE", typeof(decimal), 15, false)]
        public decimal OverFloorlife;

        /// <summary>
        /// 维护日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 维护时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 维护人
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;
    }
    #endregion

    #region TBLSERIALBOOK
    /// <summary>
    /// 工单关联的序列号
    /// </summary>
    [Serializable, TableMap("TBLSERIALBOOK", "SNPREFIX")]
    public class SERIALBOOK : DomainObject
    {
        public SERIALBOOK()
        {
        }

        /// <summary>
        /// 序列号前缀
        /// </summary>
        [FieldMapAttribute("SNPREFIX", typeof(string), 40, false)]
        public string SNPrefix;

        ///// <summary>
        ///// 日期
        ///// </summary>
        //[FieldMapAttribute("DATECODE", typeof(string), 40, false)]
        //public string DateCode;

        /// <summary>
        /// 序列号最大Serial号码
        /// </summary>
        [FieldMapAttribute("MAXSERIAL", typeof(string), 40, false)]
        public string MaxSerial;

        /// <summary>
        /// 维护人
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MUser;

        /// <summary>
        /// 维护日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MDate;

        /// <summary>
        /// 维护时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MTime;

        /// <summary>
        /// 进制
        /// </summary>
        [FieldMapAttribute("SERIALTYPE", typeof(string), 40, true)]
        public string SerialType;

    }
    #endregion


    #region InvTransfer
    /// <summary>
    /// TBLInvTransfer
    /// </summary>
    [Serializable, TableMap("TBLINVTRANSFER", "TRANSFERNO")]
    public class InvTransfer : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public InvTransfer()
        {
        }

        ///<summary>
        ///TRANSFERNO
        ///</summary>
        [FieldMapAttribute("TRANSFERNO", typeof(string), 40, false)]
        public string TransferNO;

        ///<summary>
        ///FRMSTORAGEID
        ///</summary>
        [FieldMapAttribute("FRMSTORAGEID", typeof(string), 40, false)]
        public string FromStorageID;

        ///<summary>
        ///TOSTORAGEID
        ///</summary>
        [FieldMapAttribute("TOSTORAGEID", typeof(string), 40, true)]
        public string ToStorageID;

        ///<summary>
        ///TRANSFERSTATUS
        ///</summary>
        [FieldMapAttribute("TRANSFERSTATUS", typeof(string), 40, false)]
        public string TransferStatus;

        ///<summary>
        ///RECTYPE
        ///</summary>
        [FieldMapAttribute("RECTYPE", typeof(string), 40, false)]
        public string Rectype;

        ///<summary>
        ///MEMO
        ///</summary>
        [FieldMapAttribute("MEMO", typeof(string), 2000, true)]
        public string Memo;


        ///<summary>
        ///CREATEDATE
        ///</summary>
        [FieldMapAttribute("CREATEDATE", typeof(int), 22, true)]
        public int CreateDate;

        ///<summary>
        ///CREATETIME
        ///</summary>
        [FieldMapAttribute("CREATETIME", typeof(int), 22, true)]
        public int CreateTime;

        ///<summary>
        ///CREATEUSER
        ///</summary>
        [FieldMapAttribute("CREATEUSER", typeof(string), 40, true)]

        public string CreateUser;

        ///<summary>
        ///ORGID
        ///</summary>
        [FieldMapAttribute("ORGID", typeof(int), 22, false)]
        public int OrgID;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string Muser;

    }
    #endregion

    #region InvTransferDetail
    /// <summary>
    /// TBLInvTransferDetail
    /// </summary>
    [Serializable, TableMap("TBLINVTRANSFERDETAIL", "TRANSFERNO,TRANSFERLINE")]
    public class InvTransferDetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public InvTransferDetail()
        {
        }

        ///<summary>
        ///TRANSFERNO
        ///</summary>
        [FieldMapAttribute("TRANSFERNO", typeof(string), 40, false)]
        public string TransferNO;

        ///<summary>
        ///TRANSFERLINE
        ///</summary>
        [FieldMapAttribute("TRANSFERLINE", typeof(int), 22, false)]
        public int TransferLine;

        ///<summary>
        ///ORDERNO
        ///</summary>
        [FieldMapAttribute("ORDERNO", typeof(string), 40, true)]
        public string OrderNO;

        ///<summary>
        ///ORDERLINE
        ///</summary>
        [FieldMapAttribute("ORDERLINE", typeof(int), 22, true)]
        public int OrderLine;

        ///<summary>
        ///TRANSFERSTATUS
        ///</summary>
        [FieldMapAttribute("TRANSFERSTATUS", typeof(string), 40, false)]
        public string TransferStatus;

        ///<summary>
        ///MEMO
        ///</summary>
        [FieldMapAttribute("MEMO", typeof(string), 2000, true)]
        public string Memo;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///MOCODE
        ///</summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        ///<summary>
        ///PLANQTY
        ///</summary>
        [FieldMapAttribute("PLANQTY", typeof(decimal), 28, false)]
        public decimal Planqty;

        ///<summary>
        ///ACTQTY
        ///</summary>
        [FieldMapAttribute("ACTQTY", typeof(decimal), 28, true)]
        public decimal Actqty;

        ///<summary>
        ///CUSTOMERCODE
        ///</summary>
        [FieldMapAttribute("CUSTOMERCODE", typeof(string), 40, true)]
        public string CustomerCode;

        ///<summary>
        ///CUSTOMERNAME
        ///</summary>
        [FieldMapAttribute("CUSTOMERNAME", typeof(string), 100, true)]
        public string CustomerName;

        ///<summary>
        ///TRANSFERDATE
        ///</summary>
        [FieldMapAttribute("TRANSFERDATE", typeof(int), 22, true)]
        public int TransferDate;

        ///<summary>
        ///TRANSFERTIME
        ///</summary>
        [FieldMapAttribute("TRANSFERTIME", typeof(int), 22, true)]
        public int TransferTime;

        ///<summary>
        ///TRANSFERUSER
        ///</summary>
        [FieldMapAttribute("TRANSFERUSER", typeof(string), 40, true)]
        public string TransferUser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string Muser;

    }
    #endregion

    #region INVTRANSFERMERGE
    /// <summary>
    /// TBLINVTRANSFERMERGE
    /// </summary>
    [Serializable, TableMap("TBLINVTRANSFERMERGE", "SERIAL")]
    public class InvTransferMerge : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public InvTransferMerge()
        {
        }

        ///<summary>
        ///SERIAL
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///TRANSFERNO
        ///</summary>
        [FieldMapAttribute("TRANSFERNO", typeof(string), 40, false)]
        public string Transferno;

        ///<summary>
        ///FRMTRANSFERNO
        ///</summary>
        [FieldMapAttribute("FRMTRANSFERNO", typeof(string), 40, false)]
        public string Frmtransferno;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string Muser;

    }
    #endregion

    #region InvTransferDetailForQuery
    public class InvTransferDetailForQuey : InvTransferDetail
    {
        public InvTransferDetailForQuey()
        {
        }
        ///<summary>
        ///Material Description
        ///</summary>	
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MaterialDescription;

        ///<summary>
        ///TransQTY
        ///</summary>	
        [FieldMapAttribute("TRANSQTY", typeof(decimal), 28, true)]
        public decimal TransQTY;

        ///<summary>
        ///TOSTORAGEID
        ///</summary>
        [FieldMapAttribute("TOSTORAGEID", typeof(string), 40, false)]
        public string ToStorageID;

        ///<summary>
        ///FRMSTORAGEID
        ///</summary>
        [FieldMapAttribute("FRMSTORAGEID", typeof(string), 40, false)]
        public string FromStorageID;

        ///<summary>
        ///STACKCODE
        ///</summary>
        [FieldMapAttribute("STACKCODE", typeof(string), 40, true)]
        public string StackCODE;

        ///<summary>
        ///TOSTORAGEQTY
        ///</summary>
        [FieldMapAttribute("TOSTORAGEQTY", typeof(decimal), 28, true)]
        public decimal ToStorageQty;

        ///<summary>
        ///FRMSTORAGEQTY
        ///</summary>
        [FieldMapAttribute("FRMSTORAGEQTY", typeof(decimal), 28, true)]
        public decimal FrmStorageQty;

        ///<summary>
        ///MCONTROLTYPE
        ///</summary>
        [FieldMap("MCONTROLTYPE", typeof(string), 40, false)]
        public string MaterialControlType;

        //edit by kathy @20140626 查询批次料库存数量-已备料数量
        ///<summary>
        ///TOSTORAGEQTY，批次料源库存数量-已备料数量
        ///</summary>
        [FieldMapAttribute("frmlotqty", typeof(decimal), 28, true)]
        public decimal FrmLotQty;

        ///<summary>
        ///FRMSTORAGEQTY，批次料目的库存数量-已备料数量
        ///</summary>
        [FieldMapAttribute("tolotqty", typeof(decimal), 28, true)]
        public decimal ToLotQty;
    }
    #endregion

    [Serializable]
    public class MSDLOTLExc : MSDLOT
    {
        [FieldMapAttribute("MNAME", typeof(string), 40, true)]
        public string MNAME;

        [FieldMapAttribute("MDESC", typeof(string), 40, true)]
        public string MDESC;

        [FieldMapAttribute("MCODE", typeof(string), 40, true)]
        public string MCODE;
    }

    #region MSDWIP
    [Serializable, TableMap("TBLMSDWIP", "SERIAL")]
    public class MSDWIP : DomainObject
    {
        /// <summary>
        ///自动增加（trigger）
        /// </summary>
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int serial;

        /// <summary>
        /// 物料批号
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNo;

        /// <summary>
        /// MSD状态
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        /// <summary>
        /// 维护日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 维护时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 维护人
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;
    }
    #endregion

    #region Sapcloseperiod
    /// <summary>
    /// TBLSAPCLOSEPERIOD
    /// </summary>
    [Serializable, TableMap("TBLSAPCLOSEPERIOD", "SERIAL")]
    public class Sapcloseperiod : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Sapcloseperiod()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///StartDate
        ///</summary>
        [FieldMapAttribute("STARTDATE", typeof(int), 22, false)]
        public int StartDate;

        ///<summary>
        ///StartTime
        ///</summary>
        [FieldMapAttribute("STARTTIME", typeof(int), 22, false)]
        public int StartTime;

        ///<summary>
        ///EndDate
        ///</summary>
        [FieldMapAttribute("ENDDATE", typeof(int), 22, false)]
        public int EndDate;

        ///<summary>
        ///EndTime
        ///</summary>
        [FieldMapAttribute("ENDTIME", typeof(int), 22, false)]
        public int EndTime;

        ///<summary>
        ///Orgid
        ///</summary>
        [FieldMapAttribute("ORGID", typeof(int), 22, false)]
        public int Orgid;

        ///<summary>
        ///Eattribute1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;

        ///<summary>
        ///Eattribute2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 40, true)]
        public string Eattribute2;

        ///<summary>
        ///Eattribute3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 40, true)]
        public string Eattribute3;

        ///<summary>
        ///CUser
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///CDate
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///CTime
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, true)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, true)]
        public int MaintainTime;

    }
    #endregion

  #region Asn  Amy Add
/// <summary>
/// TBLASN
/// </summary>

[Serializable, TableMap("TBLASN","STNO")]
public class Asn : BenQGuru.eMES.Common.Domain.DomainObject
{
     public Asn()
     {
     }

     ///<summary>
     ///InitrejectQty
     ///</summary>
     [FieldMapAttribute("INITREJECTQTY", typeof(int), 22, true)]
     public int InitrejectQty;

     ///<summary>
     ///InitreceiveDesc
     ///</summary>
     [FieldMapAttribute("INITRECEIVEDESC", typeof(string), 200, true)]
     public string InitreceiveDesc;

     ///<summary>
     ///InitreceiveQty
     ///</summary>
     [FieldMapAttribute("INITRECEIVEQTY", typeof(int), 22, true)]
     public int InitreceiveQty;

     ///<summary>
     ///InitgiveinQty
     ///</summary>
     [FieldMapAttribute("INITGIVEINQTY", typeof(int), 22, true)]
     public int InitgiveinQty;

     ///<summary>
     ///Stno
     ///</summary>
     [FieldMapAttribute("STNO", typeof(string), 40, false)]
     public string Stno;

     ///<summary>
     ///StType
     ///</summary>
     [FieldMapAttribute("STTYPE", typeof(string), 40, false)]
     public string StType;

     ///<summary>
     ///Invno
     ///</summary>
     [FieldMapAttribute("INVNO", typeof(string), 40, true)]
     public string Invno;

     ///<summary>
     ///VEndorCode
     ///</summary>
     [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
     public string VEndorCode;

     ///<summary>
     ///Status
     ///</summary>
     [FieldMapAttribute("STATUS", typeof(string), 40, false)]
     public string Status;

     ///<summary>
     ///Oano
     ///</summary>
     [FieldMapAttribute("OANO", typeof(string), 40, true)]
     public string Oano;

     ///<summary>
     ///FacCode
     ///</summary>
     [FieldMapAttribute("FACCODE", typeof(string), 40, false)]
     public string FacCode;

     ///<summary>
     ///StorageCode
     ///</summary>
     [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
     public string StorageCode;

     ///<summary>
     ///FromfacCode
     ///</summary>
     [FieldMapAttribute("FROMFACCODE", typeof(string), 40, true)]
     public string FromfacCode;

     ///<summary>
     ///FromstorageCode
     ///</summary>
     [FieldMapAttribute("FROMSTORAGECODE", typeof(string), 40, true)]
     public string FromstorageCode;

     ///<summary>
     ///Gross_weight
     ///</summary>
     [FieldMapAttribute("GROSS_WEIGHT", typeof(decimal), 22, true)]
     public decimal Gross_weight;

     ///<summary>
     ///Volume
     ///</summary>
     [FieldMapAttribute("VOLUME", typeof(string), 40, true)]
     public string Volume;

     ///<summary>
     ///Exigency_flag
     ///</summary>
     [FieldMapAttribute("EXIGENCY_FLAG", typeof(string), 1, true)]
     public string  Exigency_flag;

     ///<summary>
     ///Direct_flag
     ///</summary>
     [FieldMapAttribute("DIRECT_FLAG", typeof(string), 1, true)]
     public string Direct_flag;

     ///<summary>
     ///Rejects_flag
     ///</summary>
     [FieldMapAttribute("REJECTS_FLAG", typeof(string), 1, true)]
     public string Rejects_flag;

     ///<summary>
     ///Pickno
     ///</summary>
     [FieldMapAttribute("PICKNO", typeof(string), 40, true)]
     public string Pickno;

     ///<summary>
     ///Predict_Date
     ///</summary>
     [FieldMapAttribute("PREDICT_DATE", typeof(int), 22, true)]
     public int Predict_Date;

     ///<summary>
     ///Packinglistno
     ///</summary>
     [FieldMapAttribute("PACKINGLISTNO", typeof(string), 40, true)]
     public string Packinglistno;

     ///<summary>
     ///Provide_Date
     ///</summary>
     [FieldMapAttribute("PROVIDE_DATE", typeof(int), 22, true)]
     public int Provide_Date;

     ///<summary>
     ///ReworkapplyUser
     ///</summary>
     [FieldMapAttribute("REWORKAPPLYUSER", typeof(string), 40, true)]
     public string ReworkapplyUser;

     ///<summary>
     ///CUser
     ///</summary>
     [FieldMapAttribute("CUSER", typeof(string), 40, false)]
     public string CUser;

     ///<summary>
     ///CDate
     ///</summary>
     [FieldMapAttribute("CDATE", typeof(int), 22, false)]
     public int CDate;

     ///<summary>
     ///CTime
     ///</summary>
     [FieldMapAttribute("CTIME", typeof(int), 22, false)]
     public int CTime;

     ///<summary>
     ///MaintainDate
     ///</summary>
     [FieldMapAttribute("MDATE", typeof(int), 22, false)]
     public int MaintainDate;

     ///<summary>
     ///MaintainTime
     ///</summary>
     [FieldMapAttribute("MTIME", typeof(int), 22, false)]
     public int MaintainTime;

     ///<summary>
     ///MaintainUser
     ///</summary>
     [FieldMapAttribute("MUSER", typeof(string), 40, false)]
     public string MaintainUser;

     ///<summary>
     ///Remark1
     ///</summary>
     [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
     public string Remark1;

 }


 #endregion


#region Asndetail   Amy add
/// <summary>
/// TBLASNDETAIL
/// </summary>
[Serializable, TableMap("TBLASNDETAIL", "STLINE,STNO")]
public class Asndetail : BenQGuru.eMES.Common.Domain.DomainObject
{
    public Asndetail()
    {
    }

    ///<summary>
    ///StorageageDate
    ///</summary>
    [FieldMapAttribute("STORAGEAGEDATE", typeof(string), 22, true)]
    public string StorageageDate;

    ///<summary>
    ///InitreceiveStatus
    ///</summary>
    [FieldMapAttribute("INITRECEIVESTATUS", typeof(string), 40, true)]
    public string InitreceiveStatus;

    ///<summary>
    ///InitreceiveDesc
    ///</summary>
    [FieldMapAttribute("INITRECEIVEDESC", typeof(string), 200, true)]
    public string InitreceiveDesc;

    ///<summary>
    ///VEndormCodeDesc
    ///</summary>
    [FieldMapAttribute("VENDORMCODEDESC", typeof(string), 100, true)]
    public string VEndormCodeDesc;

    ///<summary>
    ///VEndormCode
    ///</summary>
    [FieldMapAttribute("VENDORMCODE", typeof(string), 40, true)]
    public string VEndormCode;

    ///<summary>
    ///Stno
    ///</summary>
    [FieldMapAttribute("STNO", typeof(string), 40, false)]
    public string Stno;

    ///<summary>
    ///Stline
    ///</summary>
    [FieldMapAttribute("STLINE", typeof(string), 40, false)]
    public string Stline;

    ///<summary>
    ///Status
    ///</summary>
    [FieldMapAttribute("STATUS", typeof(string), 40, false)]
    public string Status;

    ///<summary>
    ///Cartonno
    ///</summary>
    [FieldMapAttribute("CARTONNO", typeof(string), 40, true)]
    public string Cartonno;

    ///<summary>
    ///Cartonbigseq
    ///</summary>
    [FieldMapAttribute("CARTONBIGSEQ", typeof(string), 40, true)]
    public string Cartonbigseq;

    ///<summary>
    ///Cartonseq
    ///</summary>
    [FieldMapAttribute("CARTONSEQ", typeof(string), 40, true)]
    public string Cartonseq;

    ///<summary>
    ///CustmCode
    ///</summary>
    [FieldMapAttribute("CUSTMCODE", typeof(string), 40, true)]
    public string CustmCode;

    ///<summary>
    ///MCode
    ///</summary>
    [FieldMapAttribute("MCODE", typeof(string), 40, false)]
    public string MCode;

    ///<summary>
    ///DqmCode
    ///</summary>
    [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
    public string DqmCode;

    ///<summary>
    ///MDesc
    ///</summary>
    [FieldMapAttribute("MDESC", typeof(string), 200, true)]
    public string MDesc;

    ///<summary>
    ///ReceivemCode
    ///</summary>
    [FieldMapAttribute("RECEIVEMCODE", typeof(string), 40, true)]
    public string ReceivemCode;

    ///<summary>
    ///Qty
    ///</summary>
    [FieldMapAttribute("QTY", typeof(int), 22, false)]
    public int Qty;

    ///<summary>
    ///ReceiveQty
    ///</summary>
    [FieldMapAttribute("RECEIVEQTY", typeof(int), 22, true)]
    public int ReceiveQty;

    ///<summary>
    ///ActQty
    ///</summary>
    [FieldMapAttribute("ACTQTY", typeof(int), 22, true)]
    public int ActQty;

    ///<summary>
    ///QcpassQty
    ///</summary>
    [FieldMapAttribute("QCPASSQTY", typeof(int), 22, true)]
    public int QcpassQty;

    ///<summary>
    ///Unit
    ///</summary>
    [FieldMapAttribute("UNIT", typeof(string), 40, true)]
    public string Unit;

    ///<summary>
    ///Production_Date
    ///</summary>
    [FieldMapAttribute("PRODUCTION_DATE", typeof(int), 22, true)]
    public int Production_Date;

    ///<summary>
    ///Supplier_lotno
    ///</summary>
    [FieldMapAttribute("SUPPLIER_LOTNO", typeof(string), 40, true)]
    public string Supplier_lotno;

    ///<summary>
    ///Lotno
    ///</summary>
    [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
    public string Lotno;

    ///<summary>
    ///Remark1
    ///</summary>
    [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
    public string Remark1;

    ///<summary>
    ///CUser
    ///</summary>
    [FieldMapAttribute("CUSER", typeof(string), 40, false)]
    public string CUser;

    ///<summary>
    ///CDate
    ///</summary>
    [FieldMapAttribute("CDATE", typeof(int), 22, false)]
    public int CDate;

    ///<summary>
    ///CTime
    ///</summary>
    [FieldMapAttribute("CTIME", typeof(int), 22, false)]
    public int CTime;

    ///<summary>
    ///MaintainDate
    ///</summary>
    [FieldMapAttribute("MDATE", typeof(int), 22, false)]
    public int MaintainDate;

    ///<summary>
    ///MaintainTime
    ///</summary>
    [FieldMapAttribute("MTIME", typeof(int), 22, false)]
    public int MaintainTime;

    ///<summary>
    ///MaintainUser
    ///</summary>
    [FieldMapAttribute("MUSER", typeof(string), 40, false)]
    public string MaintainUser;

}
#endregion
#region  Amy add


/// <summary>
    /// TBLASNDETAILEX
    /// </summary>
    public class AsndetailEX : Asndetail
    {
        public AsndetailEX()
        {
        }

        ///<summary>
        ///入库类型
        ///</summary>
        [FieldMapAttribute("MControlType", typeof(string), 40, true)]
        public string MControlType;


        //add by bela 20160222
        ///<summary>
        ///StType
        ///</summary>
        [FieldMapAttribute("STTYPE", typeof(string), 40, false)]
        public string StType;

        ///<summary>
        ///Invno
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, true)]
        public string Invno;
    }

    #endregion
    #region  Amy add

    //public class invoicedetailEX : InvoicesDetail
    //{
    //    public invoicedetailEX()
    //    {
    //    }

    //    ///<summary>
    //    ///stno
    //    ///</summary>
    //    [FieldMapAttribute("stno", typeof(string), 40, true)]
    //    public string stno;

    //    ///<summary>
    //    ///stno
    //    ///</summary>
    //    [FieldMapAttribute("stline", typeof(string), 40, true)]
    //    public string stline;

    //    ///<summary>
    //    ///stno
    //    ///</summary>
    //    [FieldMapAttribute("EQTY", typeof(Decimal), 40, true)]
    //    public Decimal EQTY;

    //}

    #endregion

    #region Asndetailitem  Amy add
    /// <summary>
    /// TBLASNDETAILITEM
    /// </summary>
    [Serializable, TableMap("TBLASNDETAILITEM", "INVNO,STLINE,INVLINE,STNO")]
    public class Asndetailitem : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Asndetailitem()
        {
        }

        ///<summary>
        ///Stno
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string Stno;

        ///<summary>
        ///Stline
        ///</summary>
        [FieldMapAttribute("STLINE", typeof(string), 40, false)]
        public string Stline;

        ///<summary>
        ///Invno
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, false)]
        public string Invno;

        ///<summary>
        ///Invline
        ///</summary>
        [FieldMapAttribute("INVLINE", typeof(string), 40, false)]
        public string Invline;

        ///<summary>
        ///MCode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///DqmCode
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DqmCode;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, false)]
        public decimal Qty;

        ///<summary>
        ///ReceiveQty
        ///</summary>
        [FieldMapAttribute("RECEIVEQTY", typeof(decimal), 22, true)]
        public decimal ReceiveQty;

        ///<summary>
        ///ActQty
        ///</summary>
        [FieldMapAttribute("ACTQTY", typeof(decimal), 22, true)]
        public decimal ActQty;

        ///<summary>
        ///QcpassQty
        ///</summary>
        [FieldMapAttribute("QCPASSQTY", typeof(decimal), 22, true)]
        public decimal QcpassQty;

        ///<summary>
        ///CUser
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///CDate
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///CTime
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

    }
    #endregion

    #region Asndetailsn  Amy add
    /// <summary>
    /// TBLASNDETAILSN
    /// </summary>
    [Serializable, TableMap("TBLASNDETAILSN", "SN,STNO")]
    public class Asndetailsn : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Asndetailsn()
        {
        }

        ///<summary>
        ///Stno
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string Stno;

        ///<summary>
        ///Stline
        ///</summary>
        [FieldMapAttribute("STLINE", typeof(string), 40, false)]
        public string Stline;

        ///<summary>
        ///Cartonno
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, true)]
        public string Cartonno;

        ///<summary>
        ///Sn
        ///</summary>
        [FieldMapAttribute("SN", typeof(string), 40, false)]
        public string Sn;

        ///<summary>
        ///SN IQC状态(Y:合格；N:不合格)
        ///</summary>
        [FieldMapAttribute("QCSTATUS", typeof(string), 40, false)]
        public string QcStatus;

        ///<summary>
        ///CUser
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///CDate
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///CTime
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

    }
    #endregion

    #region Pickdetailmaterialsn   Amy add
    /// <summary>
    /// TBLPICKDETAILMATERIALSN
    /// </summary>
    [Serializable, TableMap("TBLPICKDETAILMATERIALSN", "PICKNO,SN")]
    public class Pickdetailmaterialsn : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Pickdetailmaterialsn()
        {
        }

        ///<summary>
        ///Pickno
        ///</summary>
        [FieldMapAttribute("PICKNO", typeof(string), 40, false)]
        public string Pickno;

        ///<summary>
        ///Pickline
        ///</summary>
        [FieldMapAttribute("PICKLINE", typeof(string), 6, false)]
        public string Pickline;

        ///<summary>
        ///Cartonno
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string Cartonno;

        ///<summary>
        ///Sn
        ///</summary>
        [FieldMapAttribute("SN", typeof(string), 40, false)]
        public string Sn;

        ///<summary>
        ///QcStatus
        ///</summary>
        [FieldMapAttribute("QCSTATUS", typeof(string), 40, true)]
        public string QcStatus;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

    }
    #endregion

    #region Pickdetailmaterial  Amy add
    /// <summary>
    /// TBLPICKDETAILMATERIAL
    /// </summary>
    [Serializable, TableMap("TBLPICKDETAILMATERIAL", "PICKNO,CARTONNO")]
    public class Pickdetailmaterial : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Pickdetailmaterial()
        {
        }

        ///<summary>
        ///Pickno
        ///</summary>
        [FieldMapAttribute("PICKNO", typeof(string), 40, false)]
        public string Pickno;

        ///<summary>
        ///Pickline
        ///</summary>
        [FieldMapAttribute("PICKLINE", typeof(string), 6, false)]
        public string Pickline;

        ///<summary>
        ///Status
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///CustmCode
        ///</summary>
        [FieldMapAttribute("CUSTMCODE", typeof(string), 40, true)]
        public string CustmCode;

        ///<summary>
        ///MCode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///DqmCode
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DqmCode;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, false)]
        public decimal Qty;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///LocationCode
        ///</summary>
        [FieldMapAttribute("LOCATIONCODE", typeof(string), 40, false)]
        public string LocationCode;

        ///<summary>
        ///Cartonno
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string Cartonno;

        ///<summary>
        ///Production_Date
        ///</summary>
        [FieldMapAttribute("PRODUCTION_DATE", typeof(int), 22, true)]
        public int Production_Date;

        ///<summary>
        ///Supplier_lotno
        ///</summary>
        [FieldMapAttribute("SUPPLIER_LOTNO", typeof(string), 40, true)]
        public string Supplier_lotno;

        ///<summary>
        ///Lotno
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string Lotno;

        ///<summary>
        ///QcStatus
        ///</summary>
        [FieldMapAttribute("QCSTATUS", typeof(string), 40, true)]
        public string QcStatus;

        ///<summary>
        ///StorageageDate
        ///</summary>
        [FieldMapAttribute("STORAGEAGEDATE", typeof(int), 22, true)]
        public int StorageageDate;

        ///<summary>
        ///CUser
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///CDate
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///CTime
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

    }
    #endregion   amy add
    #region

    public class Asndetailexp : Asndetail
    {
        public Asndetailexp()
        {
        }

        ///<summary>
        ///LocationCode
        ///</summary>
        [FieldMapAttribute("LocationCode", typeof(string), 40, false)]
        public string LocationCode;
        ///<summary>
        ///ReLocationCode   --推荐货位
        ///</summary>
        [FieldMapAttribute("ReLocationCode", typeof(string), 40, false)]
        public string ReLocationCode;
        ///<summary>
        ///mcontroltype   --管控类型
        ///</summary>
        [FieldMapAttribute("MControlType", typeof(string), 40, false)]
        public string MControlType;
    }


    #endregion
    #region Invinouttrans
    /// <summary>
    /// TBLINVINOUTTRANS
    /// </summary>
    [Serializable, TableMap("TBLINVINOUTTRANS", "SERIAL")]
    public class InvInOutTrans : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public InvInOutTrans()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///Transno
        ///</summary>
        [FieldMapAttribute("TRANSNO", typeof(string), 40, false)]
        public string TransNO;

        ///<summary>
        ///TransType
        ///</summary>
        [FieldMapAttribute("TRANSTYPE", typeof(string), 40, false)]
        public string TransType;

        ///<summary>
        ///InvType
        ///</summary>
        [FieldMapAttribute("INVTYPE", typeof(string), 40, false)]
        public string InvType;

        ///<summary>
        ///Invno
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, true)]
        public string InvNO;

        ///<summary>
        ///Cartonno
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string CartonNO;

        ///<summary>
        ///MCode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///DqmCode
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DqMCode;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, false)]
        public decimal Qty;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///FacCode
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string FacCode;

        ///<summary>
        ///StorageCode
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
        public string StorageCode;

        ///<summary>
        ///FromfacCode
        ///</summary>
        [FieldMapAttribute("FROMFACCODE", typeof(string), 40, true)]
        public string FromFacCode;

        ///<summary>
        ///FromstorageCode
        ///</summary>
        [FieldMapAttribute("FROMSTORAGECODE", typeof(string), 40, true)]
        public string FromStorageCode;

        ///<summary>
        ///Production_Date
        ///</summary>
        [FieldMapAttribute("PRODUCTION_DATE", typeof(int), 22, true)]
        public int ProductionDate;

        ///<summary>
        ///Supplier_lotno
        ///</summary>
        [FieldMapAttribute("SUPPLIER_LOTNO", typeof(string), 40, true)]
        public string SupplierLotNo;

        ///<summary>
        ///Lotno
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LotNo;

        ///<summary>
        ///StorageageDate
        ///</summary>
        [FieldMapAttribute("STORAGEAGEDATE", typeof(int), 22, true)]
        public int StorageAgeDate;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

    }
    #endregion

    #region Pickdetail  Amy Add by @20160224
    /// <summary>
    /// TBLPICKDETAIL
    /// </summary>
    [Serializable, TableMap("TBLPICKDETAIL", "PICKNO,PICKLINE")]
    public class PickDetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public PickDetail()
        {
        }

        ///<summary>
        ///Pickno
        ///</summary>
        [FieldMapAttribute("PICKNO", typeof(string), 40, false)]
        public string PickNo;

        ///<summary>
        ///Pickline
        ///</summary>
        [FieldMapAttribute("PICKLINE", typeof(string), 6, false)]
        public string PickLine;

        ///<summary>
        ///Status
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///CustmCode
        ///</summary>
        [FieldMapAttribute("CUSTMCODE", typeof(string), 40, true)]
        public string CustMCode;

        ///<summary>
        ///MCode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///DqmCode
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DQMCode;

        ///<summary>
        ///MDesc
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MDesc;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, false)]
        public decimal QTY;

        ///<summary>
        ///OweQty
        ///</summary>
        [FieldMapAttribute("OWEQTY", typeof(decimal), 22, true)]
        public decimal OweQTY;

        ///<summary>
        ///SQty
        ///</summary>
        [FieldMapAttribute("SQTY", typeof(decimal), 22, true)]
        public decimal SQTY;

        ///<summary>
        ///PQty
        ///</summary>
        [FieldMapAttribute("PQTY", typeof(decimal), 22, true)]
        public decimal PQTY;

        ///<summary>
        ///OutQty
        ///</summary>
        [FieldMapAttribute("OUTQTY", typeof(decimal), 22, true)]
        public decimal OutQTY;

        ///<summary>
        ///QcpassQty
        ///</summary>
        [FieldMapAttribute("QCPASSQTY", typeof(decimal), 22, true)]
        public decimal QCPassQTY;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///Cusorderno
        ///</summary>
        [FieldMapAttribute("CUSORDERNO", typeof(string), 10, true)]
        public string CusOrderNo;

        ///<summary>
        ///CusordernoType
        ///</summary>
        [FieldMapAttribute("CUSORDERNOTYPE", typeof(string), 2, true)]
        public string CusOrderNoType;

        ///<summary>
        ///CusitemCode
        ///</summary>
        [FieldMapAttribute("CUSITEMCODE", typeof(string), 40, true)]
        public string CusItemCode;

        ///<summary>
        ///Cusitemspec
        ///</summary>
        [FieldMapAttribute("CUSITEMSPEC", typeof(string), 40, true)]
        public string CusItemSpec;

        ///<summary>
        ///CusitemDesc
        ///</summary>
        [FieldMapAttribute("CUSITEMDESC", typeof(string), 40, true)]
        public string CusItemDesc;

        ///<summary>
        ///VEnderitemCode
        ///</summary>
        [FieldMapAttribute("VENDERITEMCODE", typeof(string), 40, true)]
        public string VEnderItemCode;

        ///<summary>
        ///Gfcontractno
        ///</summary>
        [FieldMapAttribute("GFCONTRACTNO", typeof(string), 40, true)]
        public string GFContractNo;

        ///<summary>
        ///GfhwitemCode
        ///</summary>
        [FieldMapAttribute("GFHWITEMCODE", typeof(string), 40, true)]
        public string GFHWItemCode;

        ///<summary>
        ///Gfpackingseq
        ///</summary>
        [FieldMapAttribute("GFPACKINGSEQ", typeof(string), 40, true)]
        public string GFPackingSeq;

        ///<summary>
        ///Postway
        ///</summary>
        [FieldMapAttribute("POSTWAY", typeof(string), 40, true)]
        public string PostWay;

        ///<summary>
        ///Pickcondition
        ///</summary>
        [FieldMapAttribute("PICKCONDITION", typeof(string), 40, true)]
        public string PickCondition;

        ///<summary>
        ///HwCodeQty
        ///</summary>
        [FieldMapAttribute("HWCODEQTY", typeof(string), 40, true)]
        public string HWCodeQTY;

        ///<summary>
        ///HwTypeinfo
        ///</summary>
        [FieldMapAttribute("HWTYPEINFO", typeof(string), 40, true)]
        public string HWTypeInfo;

        ///<summary>
        ///Packingway
        ///</summary>
        [FieldMapAttribute("PACKINGWAY", typeof(string), 40, true)]
        public string PackingWay;

        ///<summary>
        ///Packingno
        ///</summary>
        [FieldMapAttribute("PACKINGNO", typeof(string), 40, true)]
        public string PackingNo;

        ///<summary>
        ///Packingspec
        ///</summary>
        [FieldMapAttribute("PACKINGSPEC", typeof(string), 40, true)]
        public string PackingSpec;

        ///<summary>
        ///Packingwayno
        ///</summary>
        [FieldMapAttribute("PACKINGWAYNO", typeof(string), 40, true)]
        public string PackingWayNo;

        ///<summary>
        ///DqsitemCode
        ///</summary>
        [FieldMapAttribute("DQSITEMCODE", typeof(string), 40, true)]
        public string DQSItemCode;

        ///<summary>
        ///CUser
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///CDate
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///CTime
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

    }
    #endregion

    #region Serialbook
    /// <summary>
    /// amy x lv  OQC生成送检批
    /// </summary>
    [Serializable, TableMap("TBLSERIALBOOK", "SNPREFIX")]
    public class Serialbook : DomainObject
    {
        public Serialbook()
        {
        }

        [FieldMapAttribute("SNPREFIX", typeof(string), 40, false)]
        public string SNprefix;

        [FieldMapAttribute("MAXSERIAL", typeof(string), 40, false)]
        public string MAXSerial;

        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MUser;

        [FieldMapAttribute("MDATE", typeof(int), 10, false)]
        public int MDate;

        [FieldMapAttribute("MTIME", typeof(int), 10, false)]
        public int MTime;

        [FieldMapAttribute("SERIALTYPE", typeof(string), 40, false)]
        public int SerialType;
    }
    #endregion

    #region Barcode add by sam 2016年2月26日
    /// <summary>
    /// TBLBARCODE
    /// </summary>
    [Serializable, TableMap("TBLBARCODE", "BARCODE")]
    public class BarCode : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public BarCode()
        {
        }

        ///<summary>
        ///Barcode
        ///</summary>
        [FieldMapAttribute("BARCODE", typeof(string), 50, false)]
        public string BarCodeNo;

        ///<summary>
        ///Type
        ///</summary>
        [FieldMapAttribute("TYPE", typeof(string), 40, false)]
        public string Type;

        ///<summary>
        ///Mcode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, true)]
        public string MCode;

        ///<summary>
        ///Encode
        ///</summary>
        [FieldMapAttribute("ENCODE", typeof(string), 40, true)]
        public string EnCode;

        ///<summary>
        ///Spanyear
        ///</summary>
        [FieldMapAttribute("SPANYEAR", typeof(string), 22, false)]
        public string SpanYear;

        ///<summary>
        ///Spandate
        ///</summary>
        [FieldMapAttribute("SPANDATE", typeof(int), 22, false)]
        public int SpanDate;

        ///<summary>
        ///Serialno
        ///</summary>
        [FieldMapAttribute("SERIALNO", typeof(int), 22, false)]
        public int SerialNo;

        ///<summary>
        ///Printtimes
        ///</summary>
        [FieldMapAttribute("PRINTTIMES", typeof(int), 22, true)]
        public int PrintTimes;

        ///<summary>
        ///Cuser
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///Cdate
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///Ctime
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

    }
    #endregion

    #region Storloctrans
    /// <summary>
    /// TBLSTORLOCTRANS
    /// </summary>
    [Serializable, TableMap("TBLSTORLOCTRANS", "TRANSNO")]
    public class Storloctrans : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Storloctrans()
        {
        }

        ///<summary>
        ///Transno
        ///</summary>
        [FieldMapAttribute("TRANSNO", typeof(string), 40, false)]
        public string Transno;

        ///<summary>
        ///TransType
        ///</summary>
        [FieldMapAttribute("TRANSTYPE", typeof(string), 40, false)]
        public string TransType;

        ///<summary>
        ///Status
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///Invno
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, false)]
        public string Invno;

        ///<summary>
        ///StorageCode
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
        public string StorageCode;

        ///<summary>
        ///FromstorageCode
        ///</summary>
        [FieldMapAttribute("FROMSTORAGECODE", typeof(string), 40, true)]
        public string FromstorageCode;

        ///<summary>
        ///CUser
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///CDate
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///CTime
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

    }
    #endregion

    #region StorloctransDetail
    /// <summary>
    /// TBLSTORLOCTRANSDETAIL
    /// </summary>
    [Serializable, TableMap("TBLSTORLOCTRANSDETAIL", "TRANSNO,MCODE")]
    public class StorloctransDetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public StorloctransDetail()
        {
        }

        ///<summary>
        ///Transno
        ///</summary>
        [FieldMapAttribute("TRANSNO", typeof(string), 40, false)]
        public string Transno;

        ///<summary>
        ///Status
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///CustmCode
        ///</summary>
        [FieldMapAttribute("CUSTMCODE", typeof(string), 40, true)]
        public string CustmCode;

        ///<summary>
        ///MCode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///DqmCode
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DqmCode;

        ///<summary>
        ///MDesc
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MDesc;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, false)]
        public decimal Qty;

        ///<summary>
        ///CUser
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///CDate
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///CTime
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

    }

    public class StorloctransDetailEX : StorloctransDetail
    {
        ///<summary>
        ///TRANSQTY
        ///</summary>
        [FieldMapAttribute("TRANSQTY", typeof(decimal), 22, false)]
        public decimal TransQty;

        ///<summary>
        ///Sn
        ///</summary>
        [FieldMapAttribute("SN", typeof(string), 40, false)]
        public string Sn;
    }
    #endregion

    #region StorloctransDetailCarton
    /// <summary>
    /// TBLSTORLOCTRANSDETAILCARTON
    /// </summary>
    [Serializable, TableMap("TBLSTORLOCTRANSDETAILCARTON", "TRANSNO,FROMCARTONNO,CARTONNO")]
    public class StorloctransDetailCarton : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public StorloctransDetailCarton()
        {
        }

        ///<summary>
        ///Transno
        ///</summary>
        [FieldMapAttribute("TRANSNO", typeof(string), 40, false)]
        public string Transno;

        ///<summary>
        ///MCode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///DqmCode
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DqmCode;

        ///<summary>
        ///MDesc
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MDesc;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///Supplier_lotno
        ///</summary>
        [FieldMapAttribute("SUPPLIER_LOTNO", typeof(string), 40, true)]
        public string Supplier_lotno;

        ///<summary>
        ///Lotno
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string Lotno;

        ///<summary>
        ///Production_Date
        ///</summary>
        [FieldMapAttribute("PRODUCTION_DATE", typeof(int), 22, true)]
        public int Production_Date;

        ///<summary>
        ///StorageageDate
        ///</summary>
        [FieldMapAttribute("STORAGEAGEDATE", typeof(int), 22, true)]
        public int StorageageDate;

        ///<summary>
        ///LaststorageageDate
        ///</summary>
        [FieldMapAttribute("LASTSTORAGEAGEDATE", typeof(int), 22, true)]
        public int LaststorageageDate;

        ///<summary>
        ///ValidStartDate
        ///</summary>
        [FieldMapAttribute("VALIDSTARTDATE", typeof(int), 22, true)]
        public int ValidStartDate;

        ///<summary>
        ///FacCode
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, false)]
        public string FacCode;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, false)]
        public decimal Qty;

        ///<summary>
        ///LocationCode
        ///</summary>
        [FieldMapAttribute("LOCATIONCODE", typeof(string), 40, false)]
        public string LocationCode;

        ///<summary>
        ///Cartonno
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string Cartonno;

        ///<summary>
        ///FromlocationCode
        ///</summary>
        [FieldMapAttribute("FROMLOCATIONCODE", typeof(string), 40, false)]
        public string FromlocationCode;

        ///<summary>
        ///Fromcartonno
        ///</summary>
        [FieldMapAttribute("FROMCARTONNO", typeof(string), 40, false)]
        public string Fromcartonno;

        ///<summary>
        ///CUser
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///CDate
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///CTime
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

    }
    #endregion

    #region StorloctransDetailSN
    /// <summary>
    /// TBLSTORLOCTRANSDETAILSN
    /// </summary>
    [Serializable, TableMap("TBLSTORLOCTRANSDETAILSN", "TRANSNO,SN")]
    public class StorloctransDetailSN : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public StorloctransDetailSN()
        {
        }

        ///<summary>
        ///Transno
        ///</summary>
        [FieldMapAttribute("TRANSNO", typeof(string), 40, false)]
        public string Transno;

        ///<summary>
        ///Cartonno
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string Cartonno;

        ///<summary>
        ///Fromcartonno
        ///</summary>
        [FieldMapAttribute("FROMCARTONNO", typeof(string), 40, false)]
        public string Fromcartonno;

        ///<summary>
        ///Sn
        ///</summary>
        [FieldMapAttribute("SN", typeof(string), 40, false)]
        public string Sn;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

    }
    #endregion



    [Serializable]
    public class MSDWIPExc : MSDWIP
    {
        [FieldMapAttribute("MNAME", typeof(string), 40, true)]
        public string MNAME;

        [FieldMapAttribute("MDESC", typeof(string), 40, true)]
        public string MDESC;

        [FieldMapAttribute("MCODE", typeof(string), 40, true)]
        public string MCODE;
    }



    [Serializable, TableMap("TBLSENDMAIL", "SERIAL")]
    public class SendMail : DomainObject
    {
        public SendMail()
        {
        }


        [FieldMapAttribute("SERIAL", typeof(int), 40, false)]
        public int SERIAL;


        [FieldMapAttribute("MAILTYPE", typeof(string), 40, true)]
        public string MAILTYPE;

        ///<summary>
        ///ItemFirstClass
        ///</summary>	
        [FieldMapAttribute("SENDFLAG", typeof(string), 40, true)]
        public string SENDFLAG;

        ///<summary>
        ///ItemSecondClass
        ///</summary>	
        [FieldMapAttribute("MAILCONTENT", typeof(string), 40, true)]
        public string MAILCONTENT;



        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUSER;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("CDATE", typeof(int), 8, false)]
        public int CDATE;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("CTIME", typeof(int), 6, false)]
        public int CTIME;

        [FieldMapAttribute("Recipients", typeof(string), 6, false)]
        public string Recipients;

    }


    

}

