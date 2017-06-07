using System;
using System.Collections;
using BenQGuru.eMES.Domain.LotDataCollect;

namespace BenQGuru.eMES.LotDataCollect.Action
{
    /// <summary>
    /// ActionEventArgs 的摘要说明。
    /// </summary>
    /// 
    [Serializable]
    public class ActionEventArgs : System.EventArgs
    {
        public ActionEventArgs()
        {
        }

        /// <summary>
        /// 当前工单
        /// </summary>
        public Domain.MOModel.MO CurrentMO;

        /// <summary>
        /// 当前工单途程
        /// </summary>
        public Domain.MOModel.ItemRoute2OP CurrentItemRoute2OP;

        public ActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode)
        {
            this.UserCode = userCode;
            this.ResourceCode = resourceCode;
            this.LotCode = lotCode;
            this.ActionType = actionType;
        }

        public ActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, ProductInfo productInfo)
            : this(actionType, lotCode, userCode, resourceCode)
        {
            this.ProductInfo = productInfo;
        }

        public ActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, object[] param)
            : this(actionType, lotCode, userCode, resourceCode)
        {
            this.Params = param;
        }

        public ActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, ProductInfo productInfo, object[] param)
            : this(actionType, lotCode, userCode, resourceCode)
        {
            this.Params = param;
            this.ProductInfo = productInfo;
        }

        //added by jessie lee, 2005/11/24
        public ActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, string maintainUser)
            : this(actionType, lotCode, userCode, resourceCode)
        {
            this.MaintainUser = maintainUser;
        }

        public ActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, string shelfNO, ProductInfo productInfo)
            : this(actionType, lotCode, userCode, resourceCode)
        {
            this.ProductInfo = productInfo;
            this.ShelfNO = shelfNO;
        }

        public string ActionType = string.Empty;
        public string LotCode = string.Empty;
        public string UserCode = string.Empty;
        public string ResourceCode = string.Empty;
        //Laws Lu,2005/08/30	Add
        public string CollectType = string.Empty;

        //Laws Lu,2006/04/18	Add
        public string Passwod = String.Empty;

        public object[] UserGroup;

        public ProductInfo ProductInfo = null;
        public object[] Params = null;

        // Added by Icyer 2005/10/31
        public IList OnWIP = new ArrayList();

        //added by jessie lee, 2005/11/24
        public string MaintainUser = string.Empty;

        //added by jessie lee, 2006/05/29
        public string ShelfNO = string.Empty;

        // Added by Icyer 2006/06/05
        public bool NeedUpdateReport = true;

        //Laws Lu,2006/07/06 是否RMA重工
        public bool IsRMA = false;
    }
    [Serializable]
    public class GoToMOActionEventArgs : ActionEventArgs
    {
        public GoToMOActionEventArgs()
        {
        }

        public GoToMOActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, string moCode)
            : base(actionType, lotCode, userCode, resourceCode)
        {
            this.MOCode = moCode.Trim().ToUpper();
        }

        public GoToMOActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, ProductInfo productInfo, string moCode)
            : base(actionType, lotCode, userCode, resourceCode)
        {
            this.MOCode = moCode.Trim().ToUpper();
            this.ProductInfo = productInfo;
        }

        public string Memo = String.Empty;
        public string MOCode = string.Empty;
        public bool PassCheck = true;
    }
    [Serializable]
    public class CINNOActionEventArgs : ActionEventArgs
    {
        public CINNOActionEventArgs()
        {
        }

        public CINNOActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, ProductInfo productInfo, string iNNO, BenQGuru.eMES.Material.WarehouseFacade warehouse
            )
            : base(actionType, lotCode, userCode, resourceCode)
        {
            this.INNO = iNNO.ToUpper();
            this.ProductInfo = productInfo;
            this.Warehouse = warehouse;
        }

        public CINNOActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, ProductInfo productInfo
            )
            : base(actionType, lotCode, userCode, resourceCode)
        {
            this.ProductInfo = productInfo;
        }

        public string INNO = string.Empty;
        public BenQGuru.eMES.Material.WarehouseFacade Warehouse = null;
    }

    [Serializable]
    public class TSActionEventArgs : OutLineActionEventArgs
    {
        public TSActionEventArgs()
        {
        }

        public TSActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, ProductInfo productInfo, object[] errorCodes, object[] errorLocations, string memo)
        {
            this.UserCode = userCode;
            this.ResourceCode = resourceCode;
            this.LotCode = lotCode;
            this.ActionType = actionType;

            this.ErrorCodes = errorCodes;
            this.ErrorLocations = errorLocations;
            this.Memo = memo;
            this.ProductInfo = productInfo;
        }

        public TSActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, object[] errorCodes, object[] errorLocations, string memo)
        {
            this.UserCode = userCode;
            this.ResourceCode = resourceCode;
            this.LotCode = lotCode;
            this.ActionType = actionType;

            this.ErrorCodes = errorCodes;
            this.ErrorLocations = errorLocations;
            this.Memo = memo;
        }

        public TSActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, ProductInfo productInfo, object[] errorCodes, string memo)
        {
            this.UserCode = userCode;
            this.ResourceCode = resourceCode;
            this.LotCode = lotCode;
            this.ActionType = actionType;

            this.ErrorCodes = errorCodes;
            this.Memo = memo;
            this.ProductInfo = productInfo;
        }

        ////Nanjing 2005/08/10  Jessie  
        public TSActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, string tsStatus, string moCode, string itemCode, string routeCode, string opCode)
        {
            this.UserCode = userCode;
            this.ResourceCode = resourceCode;
            this.LotCode = lotCode;
            this.ActionType = actionType;

            this.TSStatus = tsStatus;
            this.MOCode = moCode;
            this.ItemCode = itemCode;
            this.OPCode = opCode;
            this.RouteCode = routeCode;
        }

        ///Nanjing 2005/08/10  Jessie  
        public TSActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, string tsStatus, string moCode,
            string itemCode, string routeCode, string opCode, string maintenUser)
        {
            this.UserCode = userCode;
            this.ResourceCode = resourceCode;
            this.LotCode = lotCode;
            this.ActionType = actionType;

            this.TSStatus = tsStatus;
            this.MOCode = moCode;
            this.ItemCode = itemCode;
            this.OPCode = opCode;
            this.RouteCode = routeCode;
            this.MaintainUser = maintenUser;
        }

        ///modified by jessie lee, 2005/11/24,增加代录人，报废原因
        public TSActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, string tsStatus, string moCode,
            string itemCode, string routeCode, string opCode, string maintainUser, string scrapCause)
        {
            this.UserCode = userCode;
            this.ResourceCode = resourceCode;
            this.LotCode = lotCode;
            this.ActionType = actionType;

            this.TSStatus = tsStatus;
            this.MOCode = moCode;
            this.ItemCode = itemCode;
            this.OPCode = opCode;
            this.RouteCode = routeCode;
            this.MaintainUser = maintainUser;
            this.ScrapCause = scrapCause;
        }

        //		public object[] ErrorCodes = null;
        public object[] ErrorInfor = null;
        public object[] ErrorLocations = null;
        //		public string Memo = null;

        public string TSStatus = String.Empty;
        public string MOCode = String.Empty;
        public string ItemCode = String.Empty;
        public string RouteCode = String.Empty;
        //		public string OPCode = String.Empty;

        public string MaiternUser = String.Empty;

        ///modified by jessie lee, 2005/11/24, 报废原因
        public string ScrapCause = String.Empty;
        //		public string MaintainUser = String.Empty;

        // Added By Hi1/Venus.Feng on 20080711 for Hisense Version : 忽略检查Resource是否在TS的工序中
        public bool IgnoreResourceInOPTS = false;
    }

    [Serializable]
    public class OutLineActionEventArgs : ActionEventArgs
    {
        public OutLineActionEventArgs()
        {
        }

        public OutLineActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, ProductInfo productInfo, string opCode)
            : base(actionType, lotCode, userCode, resourceCode)
        {
            this.OPCode = opCode;
            this.ProductInfo = productInfo;
        }

        public OutLineActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, ProductInfo productInfo, string opCode, object[] errorCodes, string memo)
            : base(actionType, lotCode, userCode, resourceCode)
        {
            this.ErrorCodes = errorCodes;
            this.Memo = memo;
            this.ProductInfo = productInfo;
            this.OPCode = opCode;
        }

        public string OPCode = string.Empty;
        public object[] ErrorCodes = null;
        public string Memo = null;
    }

    [Serializable]
    public class SplitIDActionEventArgs : ActionEventArgs
    {
        public SplitIDActionEventArgs()
        {
        }

        public SplitIDActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, object[] splitedIDs, string idMergeType)
            : base(actionType, lotCode, userCode, resourceCode)
        {
            this.SplitedIDs = splitedIDs;
            this.IDMergeType = idMergeType;
        }

        public SplitIDActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, ProductInfo productInfro, object[] splitedIDs, string idMergeType)
            : base(actionType, lotCode, userCode, resourceCode, productInfro)
        {
            this.SplitedIDs = splitedIDs;
            this.IDMergeType = idMergeType;
        }

        public SplitIDActionEventArgs(string actionType, string lotCode, string userCode, string resourceCode, ProductInfo productInfro, object[] splitedIDs, string idMergeType, bool isSameMO, int existIMEISeq, bool updateSimulation)
            : base(actionType, lotCode, userCode, resourceCode, productInfro)
        {
            this.SplitedIDs = splitedIDs;
            this.IDMergeType = idMergeType;
            this.IsSameMO = isSameMO;
            this.ExistIMEISeq = existIMEISeq;
            this.UpdateSimulation = updateSimulation;
        }

        public object[] SplitedIDs = null;
        public string IDMergeType = string.Empty;
        public bool IsSameMO = false;
        public int ExistIMEISeq = 0;
        public bool UpdateSimulation = false;

        public bool IsUndo = false;		// Added by Icyer 2006/11/08, 是否在做Undo

    }


    //试流处理的参数
    [Serializable]
    public class TryEventArgs : ActionEventArgs
    {
        public string OPCode = string.Empty;
        public string LotCode = string.Empty;
        public string MItemCode = string.Empty;
        public string ItemCode = string.Empty;
        public string TryCode = string.Empty;
        public bool ForCollect = true;
        public bool ForLinkLot = false;

        public TryEventArgs(string actionType, string userCode, string opCode, string resourceCode,
            string itemCode, string runningCard, string mItemCode, string lotCode, string tryCode, bool forCollect, bool forLinkLot)
            : base(actionType, runningCard, userCode, resourceCode)
        {
            this.OPCode = opCode;
            this.LotCode = lotCode;
            this.MItemCode = mItemCode;
            this.ItemCode = itemCode;
            this.TryCode = tryCode;
            this.ForCollect = forCollect;
            this.ForLinkLot = forLinkLot;
        }
    }


    /// <summary>
    /// 产品信息
    /// </summary>
    /// 
    [Serializable]
    public class ProductInfo
    {
        public Web.Helper.DBDateTime WorkDateTime;
        public Domain.TS.TS LastTS;
        //Laws Lu,2005/12/19,新增	资源不良数 预警
        public string ECG2ErrCodes;
        public ExtendSimulation LastSimulation;
        public LotSimulation NowSimulation;
        public LotSimulationReport NowSimulationReport;
        // Added by Icyer 2005/10/18
        public Domain.BaseSetting.Resource Resource;
        public Domain.BaseSetting.TimePeriod TimePeriod;
        // Added end
        /// <summary>
        /// 当前工单途程
        /// </summary>
        public Domain.MOModel.ItemRoute2OP CurrentItemRoute2OP;

        /// <summary>
        /// 当前工单
        /// </summary>
        public Domain.MOModel.MO CurrentMO;

        public ProductInfo()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public override string ToString()
        {
            string s = "";
            if (LastSimulation != null)
            {
                s = LastSimulation.ToString();
            }

            s = string.Format("{0}&", s);

            if (NowSimulation != null)
            {
                s = string.Format("{0}{1}", s, NowSimulation.ToString());
            }
            return s;
        }
    }
    [Serializable]
    public class ExtendSimulation : LotSimulation
    {
        public ExtendSimulation(LotSimulation simulation)
        {
            if (simulation == null)
            {
                return;
            }
            this.ActionList = simulation.ActionList;
            this.CartonCode = simulation.CartonCode;
            this.EAttribute1 = simulation.EAttribute1;
            this.EAttribute2 = simulation.EAttribute2;
            this.FromOP = simulation.FromOP;
            this.FromRoute = simulation.FromRoute;
            this.IsComplete = simulation.IsComplete;
            this.ItemCode = simulation.ItemCode;
            this.LastAction = simulation.LastAction;
            this.LotNo = simulation.LotNo;
            this.BeginDate = simulation.BeginDate;
            this.BeginTime = simulation.BeginTime;
            this.MaintainUser = simulation.MaintainUser;
            this.MOCode = simulation.MOCode;
            this.ModelCode = simulation.ModelCode;
            this.NGTimes = simulation.NGTimes;

            this.OPCode = simulation.OPCode;
            this.PalletCode = simulation.PalletCode;
            this.ProductStatus = simulation.ProductStatus;
            this.ResCode = simulation.ResCode;
            this.RouteCode = simulation.RouteCode;
            this.LotQty = simulation.LotQty;
            this.GoodQty = simulation.GoodQty;
            this.NGQty = simulation.NGQty;
            this.CollectStatus = simulation.CollectStatus;
            this.LotStatus = simulation.LotStatus;

            this.LotCode = simulation.LotCode;
            this.LotSeq = simulation.LotSeq;
            this.RMABillCode = simulation.RMABillCode;
            this.IsHold = simulation.IsHold;
            this.MOSeq = simulation.MOSeq;

        }
        public string NextOPCode = string.Empty;
        public string NextRouteCode = string.Empty;
        public string AdjustProductStatus = string.Empty;
        public string NextAction = string.Empty;
        public string NextActionList = string.Empty;
        public int IsHold = (int)Web.Helper.CycleStatus.Pass;
    }

    // Added by Icyer 2005/10/28
    /// <summary>
    /// 标识Action过程中对Check点的执行情况
    /// 在Action执行时，先检查相应状态是否被执行过，如果被执行过，则不用再次Check
    /// </summary>
    /// 
    [Serializable]
    public class ActionCheckStatus
    {
        /// <summary>
        /// 是否做过CheckMO操作 (检查MO状态)
        /// </summary>
        public bool CheckedMO = false;

        /// <summary>
        /// 是否做过CheckOP操作 (检查途程、OP)
        /// </summary>
        public bool CheckedOP = false;

        /// <summary>
        /// 是否做过CheckID操作
        /// </summary>
        public bool CheckedID = false;

        /// <summary>
        /// 是否检查过NextOP，用在CheckOnlineOP函数中
        /// </summary>
        public bool CheckedNextOP = false;
        /// <summary>
        /// 在检查NextOP时，原来的NextOPCode，用在CheckOnlineOP函数中
        /// </summary>
        public string CheckedNextOPCode = string.Empty;
        /// <summary>
        /// 在检查NextOP时，原来的NextRouteCode，用在CheckOnlineOP函数中
        /// </summary>
        public string CheckedNextRouteCode = string.Empty;

        /// <summary>
        /// 是否需要更新LotSimulation
        /// </summary>
        public bool NeedUpdateSimulation = true;

        /// <summary>
        /// 是否需要写报表
        /// </summary>
        public bool NeedFillReport = true;

        /// <summary>
        /// 依次执行过的Action列表
        /// </summary>
        public IList ActionList = new ArrayList();

        /// <summary>
        /// MO
        /// </summary>
        public Domain.MOModel.MO MO = null;

        /// <summary>
        /// MOType的Parameter值
        /// </summary>
        public string MOTypeParamValue = string.Empty;

        /// <summary>
        /// Route
        /// </summary>
        public Domain.MOModel.MO2Route Route = null;

        /// <summary>
        /// OP
        /// </summary>
        public Domain.MOModel.ItemRoute2OP OP = null;

        /// <summary>
        /// Model
        /// </summary>
        public Domain.MOModel.Model Model = null;

        /// <summary>
        /// ProductInfo
        /// </summary>
        public ProductInfo ProductInfo = null;

        /// <summary>
        /// 是否是最后一个工序，N表示否，Y表示yes，string.Empty表示未赋值
        /// </summary>
        public string IsLastOP = string.Empty;

        /// <summary>
        /// 是否是中间工序, "No"表示否，"Yes"表示是，""表示未检查
        /// </summary>
        public string IsMidOutputOP = string.Empty;

        /// <summary>
        /// 是否是中间投入工序, "No"表示否，"Yes"表示是，""表示未检查
        /// </summary>
        public string IsMidInputOP = string.Empty;

        /// <summary>
        /// 是否执行过updateItem2Route
        /// </summary>
        public bool IsUpdateRefItem2Route = false;

        /// <summary>
        /// OP BOM类型
        /// </summary>
        public string opBOMType = string.Empty;
        /// <summary>
        /// Keypart数量
        /// </summary>
        public int keypartTimes = 0;
        /// <summary>
        /// 集成上料数量
        /// </summary>
        public int innoTimes = 0;

        public decimal currentGoodQty = 0;

    }
    // Added end

}
