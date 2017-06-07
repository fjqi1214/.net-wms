using System;
using System.Collections;

namespace BenQGuru.eMES.Web.Helper
{

    /// 修改人:crystal chu 
    /// 修改日期:2005/04/21
    ///          2005/04/22
    /// 描 述: crystal chu 2005/04/21 增加对IDMergeType的系统变量
    ///        crystal chu 2005/04/22 修改定义常量满足多原因的需求
    /// 版 本:
    /// 


    /// <summary>
    /// 盘点状态
    /// </summary>
    public enum CycleStatus
    {
        /// <summary>
        /// 产线暂停
        /// </summary>
        Hold = -1,
        /// <summary>
        /// 正常采集
        /// </summary>
        Pass = 0
    }

    /// <summary>
    /// 板面类型
    /// </summary>
    public enum PCSType
    {
        /// <summary>
        /// 单面板
        /// </summary>
        SingleSide = 0,
        /// <summary>
        /// 双面板
        /// </summary>
        DoubleSide = 1

    }

    public enum MaterialType
    {
        CollectMaterial = 0,//上料采集
        DropMaterial = 1//下料采集
    }

    public interface IInternalSystemVariable
    {
        string Group
        {
            get;
        }

        ArrayList Items
        {
            get;
        }
    }

    public class InternalSystemVariable
    {
        public static Hashtable s_registry = new Hashtable();

        public static void Register(IInternalSystemVariable sv)
        {
            if (!InternalSystemVariable.s_registry.ContainsKey(sv.Group.ToUpper()))
            {
                InternalSystemVariable.s_registry.Add(sv.Group.ToUpper(), sv);
            }
        }

        public static IInternalSystemVariable Lookup(string group)
        {
            if (InternalSystemVariable.s_registry.ContainsKey(group.ToUpper()))
            {
                return InternalSystemVariable.s_registry[group.ToUpper()] as IInternalSystemVariable;
            }
            return null;
        }
    }

    #region EquipmentTSLogStatus

    public class EquipmentTSLogStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public EquipmentTSLogStatus()
        {
            this._list.Add(EquipmentTSLogStatus_New);
            this._list.Add(EquipmentTSLogStatus_Closed);
        }

        public const string EquipmentTSLogStatus_New = "EquipmentTSLogStatus_New";
        public const string EquipmentTSLogStatus_Closed = "EquipmentTSLogStatus_Closed";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "EquipmentTSLogStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region MOManufactureStatus

    public class MOManufactureStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public MOManufactureStatus()
        {
            this._list.Add(MOManufactureStatus.MOSTATUS_INITIAL);
            this._list.Add(MOManufactureStatus.MOSTATUS_RELEASE);
            this._list.Add(MOManufactureStatus.MOSTATUS_OPEN);
            this._list.Add(MOManufactureStatus.MOSTATUS_CLOSE);
            this._list.Add(MOManufactureStatus.MOSTATUS_PENDING);
        }

        public const string MOSTATUS_INITIAL = "mostatus_initial";		//初始
        public const string MOSTATUS_RELEASE = "mostatus_release";		//下发
        public const string MOSTATUS_OPEN = "mostatus_open";			//生产中
        public const string MOSTATUS_CLOSE = "mostatus_close";			//关单
        public const string MOSTATUS_PENDING = "mostatus_pending";		//暂停

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "MOManufactureStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region TryStatus

    public class TryStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public TryStatus()
        {
            this._list.Add(TryStatus.STATUS_INITIAL);
            this._list.Add(TryStatus.STATUS_RELEASE);
            this._list.Add(TryStatus.STATUS_PRODUCE);
            this._list.Add(TryStatus.STATUS_FINISH);
            this._list.Add(TryStatus.STATUS_PAUSE);
        }

        public const string STATUS_INITIAL = "trystatus_initial";		//初始
        public const string STATUS_RELEASE = "trystatus_release";		//下发
        public const string STATUS_PRODUCE = "trystatus_produce";			//生产中
        public const string STATUS_FINISH = "trystatus_finish";			//关单
        public const string STATUS_PAUSE = "trystatus_pause";		//暂停

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "TryStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region TryType

    public class TryType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public TryType()
        {
            this._list.Add(TryType.TYPE_TRYPART);
            this._list.Add(TryType.TYPE_IMPROTEPRODUCT);
        }

        public const string TYPE_TRYPART = "type_trypart";		//1、器件试流
        public const string TYPE_IMPROTEPRODUCT = "trytype_improteproduct";		//2、产品改进试流

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "TryType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region OPBOMCheckOption

    public class OPBOMCheckOption
    {
        public OPBOMCheckOption()
        {
        }

        public const string ISITEMCHECK_DEFAULT = "0";
        public const string ITEMCHECKVALUE_DEFAULT = "0000000";
        public const string ISOPBOMCHANGED_CHECK = "1";
    }

    #endregion

    #region SNContentCheckStatus

    public class SNContentCheckStatus
    {
        public SNContentCheckStatus()
        {
        }

        public const string SNContentCheckStatus_Need = "Y";
        public const string SNContentCheckStatus_NONeed = "N";
    }

    #endregion

    #region BussinessType

    public class BussinessType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public BussinessType()
        {
            this._list.Add(BussinessType.type_in);
            this._list.Add(BussinessType.type_out);
        }

        public const string type_in = "type_in";//入库
        public const string type_out = "type_out";//出库

        #region IInternalSystemVariable Members

        public string Group
        {
            get 
            {
                return "BussinessType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region BussinessReason

    public class BussinessReason : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public BussinessReason()
        {
            this._list.Add(BussinessReason.type_produce);
            this._list.Add(BussinessReason.type_noneproduce);
        }

        public const string type_produce = "type_produce";//生产性入库
        public const string type_noneproduce = "type_noneproduce";//非生产性入库

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "BussinessReason";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region PauseStatus

    public class PauseStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public PauseStatus()
        {
            this._list.Add(BussinessReason.type_produce);
            this._list.Add(BussinessReason.type_noneproduce);
        }

        public const string status_pause = "status_pause";//停发
        public const string status_cancel = "status_cancel";//取消停发

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "PauseStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region IDMergeType

    public class IDMergeType
    {
        private ArrayList _list = new ArrayList();
        public IDMergeType()
        {
            this._list.Add(IDMERGETYPE_IDMERGE);
            this._list.Add(IDMERGETYPE_ROUTER);
        }
        public const string IDMERGETYPE_IDMERGE = "idmergetype_idmerge";
        public const string IDMERGETYPE_ROUTER = "idmergetype_router";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "IDMergeType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region BOMItemControlType

    public class BOMItemControlType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public BOMItemControlType()
        {
            this._list.Add(BOMItemControlType.ITEM_CONTROL_LOT);
            this._list.Add(BOMItemControlType.ITEM_CONTROL_KEYPARTS);
            this._list.Add(BOMItemControlType.ITEM_CONTROL_NOCONTROL);
        }
        public const string ITEM_CONTROL_LOT = "item_control_lot";
        public const string ITEM_CONTROL_KEYPARTS = "item_control_keyparts";
        public const string ITEM_CONTROL_NOCONTROL = "item_control_nocontrol";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "BOMItemControlType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region OPBOMDetailParseType

    public class OPBOMDetailParseType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public OPBOMDetailParseType()
        {
            this._list.Add(OPBOMDetailParseType.PARSE_BARCODE);
            this._list.Add(OPBOMDetailParseType.PARSE_PREPARE);
            this._list.Add(OPBOMDetailParseType.PARSE_PRODUCT);
        }
        public const string PARSE_BARCODE = "parse_barcode";
        public const string PARSE_PREPARE = "parse_prepare";
        public const string PARSE_PRODUCT = "parse_product";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "OPBOMDetailParseType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region OPBOMDetailCheckType

    public class OPBOMDetailCheckType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public OPBOMDetailCheckType()
        {
            this._list.Add(OPBOMDetailCheckType.CHECK_LINKBARCODE);
            this._list.Add(OPBOMDetailCheckType.CHECK_COMPAREITEM);
        }
        public const string CHECK_LINKBARCODE = "check_linkbarcode";
        public const string CHECK_COMPAREITEM = "check_compareitem";


        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "OPBOMDetailCheckType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region TextColor

    public class TextColor : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public TextColor()
        {
            this._list.Add(TextColor.NormalColor);
            this._list.Add(TextColor.SuccessedColor);
            this._list.Add(TextColor.FailedColor);
            this._list.Add(TextColor.NoticeColor);
        }

        public static System.Drawing.Color NormalColor = System.Drawing.Color.Black;
        public static System.Drawing.Color SuccessedColor = System.Drawing.Color.Blue;
        public static System.Drawing.Color FailedColor = System.Drawing.Color.Red;
        public static System.Drawing.Color NoticeColor = System.Drawing.Color.Orange;

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "TextColor";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region BurnInTP

    public class BurnInTP : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "BURNINTP";

        public BurnInTP()
        {
            this._list.Add(BurnInTP.A);
            this._list.Add(BurnInTP.B);
            this._list.Add(BurnInTP.C);
        }
        public const string A = "1";
        public const string B = "2";
        public const string C = "3";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "BurnInTP";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region MOType

    public class MOType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "MOTYPE";

        public MOType()
        {
            this._list.Add(MOType.MOTYPE_NORMALMOTYPE);
            this._list.Add(MOType.MOTYPE_REWORKMOTYPE);
            this._list.Add(MOType.MOTYPE_MONTHREWORKMOTYPE);
            this._list.Add(MOType.MOTYPE_RMAREWORKMOTYPE);
        }
        public static readonly string MOTYPE_NORMALMOTYPE = "NORMAL";				//正常	
        public static readonly string MOTYPE_REWORKMOTYPE = "REWORK";				//返工
        public static readonly string MOTYPE_MONTHREWORKMOTYPE = "MONTHREWORK";		//每月大返工 ( 处理方式与返工不同 )
        public static readonly string MOTYPE_RMAREWORKMOTYPE = "RMAREWORK";		//RMA返工 ( 处理方式与返工不同 )

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "MOType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region INVERPType

    public class INVERPType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "INVERPType";

        public INVERPType()
        {
            this._list.Add(INVERPType.INVERPTYPE_PROCESSED);
            this._list.Add(INVERPType.INVERPTYPE_NEW);
        }
        public static readonly string INVERPTYPE_PROCESSED = "inverptype_processed";				//已经抛转	
        public static readonly string INVERPTYPE_NEW = "inverptype_new";				//尚未抛转

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "INVERPType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region ItemControlType

    public class ItemControlType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public ItemControlType()
        {
            this._list.Add(ItemControlType.ITEMCONTROLTYPE_LOT);
            this._list.Add(ItemControlType.ITEMCONTROLTYPE_PICS);
        }
        public const string ITEMCONTROLTYPE_LOT = "itemcontroltype_lot";
        public const string ITEMCONTROLTYPE_PICS = "itemcontroltype_pics";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ItemControlType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region ItemType

    public class ItemType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public ItemType()
        {
            this._list.Add(ItemType.ITEMTYPE_FINISHEDPRODUCT);
            this._list.Add(ItemType.ITEMTYPE_SEMIMANUFACTURE);
            this._list.Add(ItemType.ITEMTYPE_RAWMATERIAL);
        }
        public const string ITEMTYPE_FINISHEDPRODUCT = "itemtype_finishedproduct";		//成品
        public const string ITEMTYPE_SEMIMANUFACTURE = "itemtype_semimanufacture";		//半成品
        public const string ITEMTYPE_RAWMATERIAL = "itemtype_rawmaterial";				//原物料

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ItemType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region RecordStatus

    public class RecordStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public RecordStatus()
        {
            this._list.Add(RecordStatus_NEW);//新建
            this._list.Add(RecordStatus_USING);//使用中
            this._list.Add(RecordStatus_CLOSE);//完成

        }

        public const string RecordStatus_NEW = "RecordStatus_NEW";
        public const string RecordStatus_USING = "RecordStatus_USING";
        public const string RecordStatus_CLOSE = "RecordStatus_CLOSE";



        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "RecordStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region FrozenStatus

    public class FrozenStatus
    {
        private ArrayList _list = new ArrayList();
        public FrozenStatus()
        {
            this._list.Add(STATUS_FRONZEN);
            this._list.Add(STATUS_UNFRONZEN);
        }
        public const string STATUS_FRONZEN = "status_frozen";
        public const string STATUS_UNFRONZEN = "status_unfrozen";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "FrozenStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region RecordType

    public class RecordType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public RecordType()
        {
            this._list.Add(RecordType_MO);//工单
            this._list.Add(RecordType_DB);//调拨
            this._list.Add(RecordType_WX);//外协
            this._list.Add(RecordType_SO);//销售
            this._list.Add(RecordType_OT);//其他
            this._list.Add(RecordType_QT);//其他发料

        }

        public const string RecordType_MO = "RecordType_MO";
        public const string RecordType_DB = "RecordType_DB";
        public const string RecordType_WX = "RecordType_WX";
        public const string RecordType_SO = "RecordType_SO";
        public const string RecordType_OT = "RecordType_OT";
        public const string RecordType_QT = "RecordType_QT";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "RecordType"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region MOProductType

    public class MOProductType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "MO_PRODUCT_TYPE";

        public MOProductType()
        {
            this._list.Add(MOProductType.MO_Product_Type_New);
            this._list.Add(MOProductType.MO_Product_Type_Mass);
        }

        public static readonly string MO_Product_Type_New = "mo_product_type_new"; //新品
        public static readonly string MO_Product_Type_Mass = "mo_product_type_mass"; //量产品

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "MO_Product_Type"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region Rework
    /// <summary>
    /// 修改人:vizo fan
    /// 修改日期:2005/04/21
    /// 描 述: 重工需求单状态
    /// 版 本:
    /// </summary>
    public class ReworkStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public ReworkStatus()
        {
            this._list.Add(ReworkStatus.REWORKSTATUS_NEW);
            this._list.Add(ReworkStatus.REWORKSTATUS_WAITING);
            this._list.Add(ReworkStatus.REWORKSTATUS_OPEN);
            //			this._list.Add(ReworkStatus.REWORKSTATUS_COMPLETE);
            this._list.Add(ReworkStatus.REWORKSTATUS_CLOSE);
            this._list.Add(ReworkStatus.REWORKSTATUS_RELEASE);
        }

        /// <summary>
        /// 修改人:vizo fan
        /// 修改日期:2005/04/21
        /// 描 述: 新增的重工需求单
        /// 版 本:
        /// </summary>
        public const string REWORKSTATUS_NEW = "reworkstatus_new";

        /// <summary>
        /// 修改人:vizo fan
        /// 修改日期:2005/04/21
        /// 描 述: 等待签核
        /// 版 本:
        /// </summary>
        public const string REWORKSTATUS_WAITING = "reworkstatus_waiting";

        /// <summary>
        /// 修改人:vizo fan
        /// 修改日期:2005/04/21
        /// 描 述: 开立
        /// 版 本:
        /// </summary>
        public const string REWORKSTATUS_OPEN = "reworkstatus_open";


        //        /// <summary>
        //        /// 修改人:vizo fan
        //        /// 修改日期:2005/04/21
        //        /// 描 述: 完工
        //        /// 版 本:
        //        /// </summary>
        //        public const string REWORKSTATUS_COMPLETE = "reworkstatus_complete";
        //
        /// <summary>
        /// 修改人:vizo fan
        /// 修改日期:2005/04/21
        /// 描 述: 关闭
        /// 版 本:
        /// </summary>
        public const string REWORKSTATUS_CLOSE = "reworkstatus_close";

        /// <summary>
        /// 修改人:vizo fan
        /// 修改日期:2005/04/21
        /// 描 述: Release
        /// 版 本:
        /// </summary>
        public const string REWORKSTATUS_RELEASE = "reworkstatus_release";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ReworkStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }



    /// <summary>
    /// <summary>
    /// 修改人:vizo fan
    /// 修改日期:2005/04/21
    /// 描 述: 重工类型
    /// 版 本:
    /// </summary>

    /// </summary>
    public class ReworkType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public ReworkType()
        {
            this._list.Add(ReworkType.REWORKTYPE_ONLINE);
            this._list.Add(ReworkType.REWORKTYPE_REMO);
        }

        /// <summary>
        /// 修改人:vizo fan
        /// 修改日期:2005/04/21
        /// 描 述: 在线重工
        /// 版 本:
        /// </summary>
        public const string REWORKTYPE_ONLINE = "reworktype_online";


        /// <summary>
        /// 修改人:vizo fan
        /// 修改日期:2005/04/21
        /// 描 述: 离线重工
        /// 版 本:
        /// </summary>
        public const string REWORKTYPE_REMO = "reworktype_remo";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ReworkType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region Approve

    /// <summary>
    /// 修改人:vizo fan
    /// 修改日期:2005/04/21
    /// 描 述: 重工需求单签核状态
    /// 版 本:
    /// </summary>
    public class ApproveStatus
    {
        public ApproveStatus()
        {
        }

        /// <summary>
        /// 修改人:vizo fan
        /// 修改日期:2005/04/21
        /// 描 述: 等待签核
        /// 版 本:
        /// </summary>
        public const int APPROVESTATUS_WAITING = 0;
        public const string APPROVESTATUS_WAITING_STRING = "ApproveStatus_Waiting";

        /// <summary>
        /// 修改人:vizo fan
        /// 修改日期:2005/04/21
        /// 描 述: 签核通过
        /// 版 本:
        /// </summary>
        public const int APPROVESTATUS_PASSED = 1;
        public const string APPROVESTATUS_PASSED_STRING = "ApproveStatus_Passed";

        /// <summary>
        /// 修改人:vizo fan
        /// 修改日期:2005/04/21
        /// 描 述: 签核未通过
        /// 版 本:
        /// </summary>
        public const int APPROVESTATUS_NOPASSED = 2;
        public const string APPROVESTATUS_NOPASSED_STRING = "ApproveStatus_NoPassed";

        /// <summary>
        /// 修改人:vizo fan
        /// 修改日期:2005/04/21
        /// 描 述: 等待其他人签核,当前签核层级不是用户的层级,并且签核未完成时,为该签核状态
        /// 版 本:
        /// </summary>
        public const int APPROVESTATUS_WAITING_OTHERS = 3;
        public const string APPROVESTATUS_WAITING_OTHERS_STRING = "ApproveStatus_Waiting_Others";
    }
    /// <summary>
    /// 修改人:vizo fan
    /// 修改日期:2005/04/21
    /// 描 述: 重工需求单签核动作
    /// 版 本:
    /// </summary>
    public class IsPass
    {
        public IsPass()
        {
        }

        /// <summary>
        /// 修改人:vizo fan
        /// 修改日期:2005/04/21
        /// 描 述: 通过
        /// 版 本:
        /// </summary>
        public const int ISPASS_NOPASS = 0;

        /// <summary>
        /// 修改人:vizo fan
        /// 修改日期:2005/04/21
        /// 描 述: 不通过
        /// 版 本:
        /// </summary>
        public const int ISPASS_PASS = 1;

        /// <summary>
        /// 修改人:vizo fan
        /// 修改日期:2005/05/20
        /// 描 述: 等待其他人
        /// 版 本:
        /// </summary>
        public const int ISPASS_NOTACTION = -1;
    }

    #endregion

    #region(Module)
    /// <summary>
    /// 修改人: Angel  Zhu
    /// 修改日期:2005/04/27
    /// 描 述: Module部分的状态及类型设定
    /// 版 本:
    /// </summary>
    public class ModuleStatus : IInternalSystemVariable
    {
        private ArrayList _ModuleStatuslist = new ArrayList();

        public ModuleStatus()
        {
            this._ModuleStatuslist.Add(Module_Alpha);
            this._ModuleStatuslist.Add(Module_Beta);
            this._ModuleStatuslist.Add(Module_Release);
        }

        public const string Module_Alpha = "Alpha";
        public const string Module_Beta = "Beta";
        public const string Module_Release = "Release";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ModuleStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._ModuleStatuslist;
            }
        }

        #endregion
    }

    public class ModuleType : IInternalSystemVariable
    {
        private ArrayList _ModuleTypelist = new ArrayList();

        public ModuleType()
        {
            this._ModuleTypelist.Add(ModuleType_BS);
            this._ModuleTypelist.Add(ModuleType_CS);
            this._ModuleTypelist.Add(ModuleType_PDA);
        }

        public const string ModuleType_BS = "B/S";
        public const string ModuleType_CS = "C/S";
        public const string ModuleType_PDA = "PDA";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ModuleType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._ModuleTypelist;
            }
        }

        #endregion
    }
    #endregion

    #region(Menu)
    /// <summary>
    /// 修改人: Angel  Zhu
    /// 修改日期:2005/04/27
    /// 描 述: Menu部分的类型设定
    /// 版 本:
    /// </summary>
    public class MenuType : IInternalSystemVariable
    {
        private ArrayList _menuTypeList = new ArrayList();

        public MenuType()
        {
            this._menuTypeList.Add(MenuType_BS);
            this._menuTypeList.Add(MenuType_CS);
            this._menuTypeList.Add(MenuType_RPT);
            this._menuTypeList.Add(MenuType_PDA);
        }

        public const string MenuType_BS = "B/S";
        public const string MenuType_CS = "C/S";
        public const string MenuType_RPT = "B/S RPT";
        public const string MenuType_PDA = "PDA";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "MenuType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._menuTypeList;
            }
        }

        #endregion
    }

    #endregion

    #region Group Type
    public class UserGroupType : IInternalSystemVariable
    {
        private ArrayList _userGroupTypeList = new ArrayList();

        public const string UserGroupTypeName = "UserGroupType";

        public UserGroupType()
        {
            this._userGroupTypeList.Add(Administrator);
            this._userGroupTypeList.Add(Guest);
        }

        public const string Administrator = "UserGroupType_Administrator";
        public const string Guest = "UserGroupType_Guest";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "UserGroupType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._userGroupTypeList;
            }
        }

        #endregion
    }

    #endregion

    #region Resource Type
    public class ResourceType : IInternalSystemVariable
    {
        private ArrayList _resourceTypeList = new ArrayList();

        public const string ResourceTypeName = "ResourceType";

        public ResourceType()
        {
            this._resourceTypeList.Add(Human);
            this._resourceTypeList.Add(Machine);
        }

        public const string Human = "ResourceType_Human";
        public const string Machine = "ResourceType_Machine";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ResourceType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._resourceTypeList;
            }
        }

        #endregion
    }
    #endregion

    #region Reject
    /// <summary>
    /// 修改人: Angel  Zhu
    /// 修改日期:2005/04/27
    /// 描 述: Module部分的状态及类型设定
    /// 版 本:
    /// </summary>
    public class RejectStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public RejectStatus()
        {
            this._list.Add(Reject);
            this._list.Add(UnReject);
            this._list.Add(Confirm);
            this._list.Add(Handle);
        }

        public const string Reject = "rejectstatus_reject";
        public const string UnReject = "rejectstatus_unreject";
        public const string Confirm = "rejectstatus_confirm";
        public const string Handle = "rejectstatus_handle";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "RejectStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region Route Type
    /// <summary>
    /// 修改人: sammer kong
    /// 修改日期:2005/05/19
    /// 版 本:
    /// </summary>
    public class RouteType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public RouteType()
        {
            this._list.Add(Normal);
            this._list.Add(Rework);
        }

        public const string Normal = "routetype_normal";
        public const string Rework = "routetype_rework";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "RouteType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region OPType
    public class OPType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "OPType";

        public OPType()
        {
            this._list.Add(OPType.COMPLOADING);
            this._list.Add(OPType.TESTING);
            this._list.Add(OPType.SN);
            this._list.Add(OPType.PACKING);
            this._list.Add(OPType.TS);
        }

        public const string COMPLOADING = "optype_comploading";
        public const string TESTING = "optype_testing";
        public const string SN = "optype_sn";
        public const string PACKING = "optype_packing";
        public const string TS = "optype_ts";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "OPType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region DataCollect
    public class ActionType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        //根据action获取对应的模块 add by Simone
        public static string GetOperationResultModule(string Action)
        {
            string returnStr = Action;
            if (Action == ActionType.DataCollectAction_CollectKeyParts || Action == ActionType.DataCollectAction_CollectINNO)
            {
                returnStr = "ComponentLoading";
            }
            else if (Action == ActionType.DataCollectAction_GOOD || Action == ActionType.DataCollectAction_NG || Action == ActionType.DataCollectAction_SMTGOOD || Action == ActionType.DataCollectAction_SMTNG || Action == ActionType.DataCollectAction_OutLineGood || Action == ActionType.DataCollectAction_OutLineNG)
            {
                returnStr = "Testing";
            }
            else if (Action == ActionType.DataCollectAction_IDTran || Action == ActionType.DataCollectAction_Split)
            {
                returnStr = "IDTranslation";
            }
            else if (Action == ActionType.DataCollectAction_Convert)
            {
                returnStr = "IDTranslation";
            }
            else if (Action == ActionType.DataCollectAction_Carton || Action == ActionType.DataCollectAction_Pallet || Action == ActionType.DataCollectAction_OQCLotAddID /* || Action == ActionType.DataCollectAction_OQCLotRemoveID*/ || Action == ActionType.DataCollectAction_LOT)
            {
                returnStr = "Packing";
            }
            else if (Action == ActionType.DataCollectAction_OQCLotRemoveID)
            {
                returnStr = "UnPacking";
            }
            else if (Action == ActionType.DataCollectAction_OQCGood || Action == ActionType.DataCollectAction_OQCNG || Action == ActionType.DataCollectAction_OQCPass || Action == ActionType.DataCollectAction_OQCReject)
            {
                returnStr = "OQC";
            }
            else if (Action == ActionType.DataCollectAction_TSConfirm || Action == ActionType.DataCollectAction_TSComplete)
            {
                returnStr = "TS";
            }
            else if (Action == ActionType.DataCollectAction_Reject)
            {
                returnStr = "Reject";
            }
            else if (Action == ActionType.DataCollectAction_GoMO)
            {
                returnStr = "GoMO";
            }
            else if (Action == ActionType.DataCollectAction_ECN)
            {
                returnStr = "ECN";
            }
            else if (Action == ActionType.DataCollectAction_SoftINFO)
            {
                returnStr = "SoftINFO";
            }
            else if (Action == ActionType.DataCollectAction_TRY)
            {
                returnStr = "TRY";
            }
            else if (Action == ActionType.DataCollectAction_TryNew)
            {
                returnStr = "TryNew";
            }//Laws Lu,2005/12/19,新增	脱离工单
            else if (Action == ActionType.DataCollectAction_OffMo)
            {
                returnStr = "OffMo";
            }//Laws Lu,2005/12/19,新增	下料
            else if (Action == ActionType.DataCollectAction_DropMaterial)
            {
                returnStr = "DropMaterial";
            }
            else if (Action == ActionType.DataCollectAction_BurnIn)//added by jessie lee, 2006-5-29
            {
                returnStr = "BurnIn";
            }
            else if (Action == ActionType.DataCollectAction_BurnOutGood)//added by jessie lee, 2006-5-29
            {
                returnStr = "BurnOut";
            }
            else if (Action == ActionType.DataCollectAction_BurnOutNG)   //老化出的良品和不良品对应的都是BurnOut工序，added by sandy, 2014-5-27
            {
                returnStr = "BurnOut";
            }
            else if (Action == ActionType.DataCollectAction_OQCFuncTest)	// Added by Icyer 2006/06/09
            {
                returnStr = "OQC";
            }


            return returnStr;

        }

        public ActionType()
        {
            this._list.Add(ActionType.DataCollectAction_GOOD);
            this._list.Add(ActionType.DataCollectAction_NG);
            this._list.Add(ActionType.DataCollectAction_SMTGOOD);
            this._list.Add(ActionType.DataCollectAction_SMTNG);
            this._list.Add(ActionType.DataCollectAction_Split);
            this._list.Add(ActionType.DataCollectAction_Carton);
            this._list.Add(ActionType.DataCollectAction_Pallet);
            this._list.Add(ActionType.DataCollectAction_Reject);
            this._list.Add(ActionType.DataCollectAction_CollectKeyParts);
            this._list.Add(ActionType.DataCollectAction_CollectINNO);
            this._list.Add(ActionType.DataCollectAction_GoMO);
            this._list.Add(ActionType.DataCollectAction_OutLineGood);
            this._list.Add(ActionType.DataCollectAction_ECN);
            this._list.Add(ActionType.DataCollectAction_OutLineNG);
            this._list.Add(ActionType.DataCollectAction_SoftINFO);
            this._list.Add(ActionType.DataCollectAction_TRY);
            this._list.Add(ActionType.DataCollectAction_TryNew);
            this._list.Add(ActionType.DataCollectAction_OQCLotAddID);
            this._list.Add(ActionType.DataCollectAction_OQCLotRemoveID);
            this._list.Add(ActionType.DataCollectAction_OQCGood);
            this._list.Add(ActionType.DataCollectAction_OQCNG);
            this._list.Add(ActionType.DataCollectAction_OQCPass);
            this._list.Add(ActionType.DataCollectAction_OQCReject);
            this._list.Add(ActionType.DataCollectAction_IDTran);
            this._list.Add(ActionType.DataCollectAction_TSConfirm);
            this._list.Add(ActionType.DataCollectAction_TSComplete);
            this._list.Add(ActionType.DataCollectAction_OffMo);
            this._list.Add(ActionType.DataCollectAction_DropMaterial);
            this._list.Add(ActionType.DataCollectAction_BurnIn);
            this._list.Add(ActionType.DataCollectAction_BurnOutGood);
            this._list.Add(ActionType.DataCollectAction_BurnOutNG);   //Add by sandy on 20140527
            this._list.Add(ActionType.DataCollectAction_OQCFuncTest);
            //Laws Lu,2006/06/19	Add out line reject action type
            this._list.Add(ActionType.DataCollectAction_OutLineReject);
            this._list.Add(ActionType.DataCollectAction_Split_OutLine);    //ian: add outline split
            this._list.Add(ActionType.DataCollectAction_CompareAppendix);
            this._list.Add(ActionType.DataCollectAction_Mix);
            this._list.Add(ActionType.DataCollectAction_KBatch);
            this._list.Add(ActionType.DataCollectAction_CompareProductCode);
            this._list.Add(ActionType.DataCollectAction_CompareTwo);
            this._list.Add(ActionType.DataCollectAction_AutoNG);
            this._list.Add(ActionType.DataCollectAction_FGPacking);
            this._list.Add(ActionType.DataCollectAction_MACALL);
            this._list.Add(ActionType.DataCollectAction_MACID);
            this._list.Add(ActionType.DataCollectAction_ONPost);
            this._list.Add(ActionType.DataCollectAction_OffPost);

            //Add By Bernard @ 2010-10-29
            this._list.Add(ActionType.DataCollectAction_Convert);
        }

        public const string DataCollectAction_OffMo = "OFFMO";
        public const string DataCollectAction_GOOD = "GOOD";
        public const string DataCollectAction_NG = "NG";
        public const string DataCollectAction_SMTGOOD = "SMTGOOD";
        public const string DataCollectAction_SMTNG = "SMTNG";
        public const string DataCollectAction_Split = "SPLIT";
        public const string DataCollectAction_Split_OutLine = "OUTLINESPLIT";    //ian: outline split
        public const string DataCollectAction_Carton = "CARTON";
        public const string DataCollectAction_Pallet = "PALLECT";
        public const string DataCollectAction_Reject = "REJECT";
        public const string DataCollectAction_CollectKeyParts = "CKEPARTS";
        public const string DataCollectAction_CollectINNO = "CINNO";
        public const string DataCollectAction_GoMO = "GOMO";
        public const string DataCollectAction_OutLineGood = "OUTLINEGOOD";
        public const string DataCollectAction_OutLineNG = "OUTLINENG";
        public const string DataCollectAction_ECN = "ECN";
        public const string DataCollectAction_SoftINFO = "SOFTINFO";
        public const string DataCollectAction_TRY = "TRY";
        public const string DataCollectAction_TryNew = "TryNew";
        public const string DataCollectAction_LOT = "LOT";
        public const string DataCollectAction_OQCLotAddID = "OQCLOTADDID";
        public const string DataCollectAction_OQCLotRemoveID = "OQCLOTREMOVEID";
        public const string DataCollectAction_OQCGood = "OQCGOOD";
        public const string DataCollectAction_OQCNG = "OQCNG";
        public const string DataCollectAction_OQCPass = "OQCPASS";
        public const string DataCollectAction_OQCReject = "OQCREJECT";
        public const string DataCollectAction_IDTran = "IDTRAN";
        public const string DataCollectAction_TSConfirm = "TSCONFIRM";
        public const string DataCollectAction_TSComplete = "TSCOMPLETE";
        public const string DataCollectAction_DropMaterial = "DROPMATERIAL";
        public const string DataCollectAction_BurnIn = "BURNIN";
        public const string DataCollectAction_BurnOutGood = "BURNOUTGOOD";
        public const string DataCollectAction_OQCFuncTest = "OQCFUNCTEST";
        //Laws Lu,2006/06/19	Add out line reject action type
        public const string DataCollectAction_OutLineReject = "OUTLINEREJECT";
        public const string DataCollectAction_CompareAppendix = "COMPAPP";
        public const string DataCollectAction_Mix = "MIX";
        public const string DataCollectAction_KBatch = "KBATCH";
        public const string DataCollectAction_CompareProductCode = "COMPPROCODE";
        public const string DataCollectAction_CompareTwo = "COMPTWO";
        public const string DataCollectAction_AutoNG = "AUTONG";
        public const string DataCollectAction_FGPacking = "FGPACKING";
        public const string DataCollectAction_MACID = "MACID";
        public const string DataCollectAction_MACALL = "MACALL";
        public const string DataCollectAction_ONPost = "ONPOST";
        public const string DataCollectAction_OffPost = "OFFPOST";

        //Add By Bernard @ 2010-10-29
        public const string DataCollectAction_Convert = "CONVERT";
        //Add By Sandy @ 2014-05-27
        public const string DataCollectAction_BurnOutNG = "BURNOUTNG";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ActionType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    public class ProductStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public ProductStatus()
        {
            this._list.Add(GOOD);
            this._list.Add(NG);
            this._list.Add(Reject);
            this._list.Add(OutLine);
            this._list.Add(OffLine);
            this._list.Add(Scrap);
            this._list.Add(OffMo);
        }

        public const string GOOD = "GOOD";
        public const string NG = "NG";
        public const string Reject = "REJECT";
        public const string OutLine = "OUTLINE";
        public const string OffLine = "OFFLINE";
        public const string Scrap = "SCRAP";
        public const string OffMo = "OFFMO";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ProductStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    public class MCardType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public MCardType()
        {
            this._list.Add(MCardType_Keyparts);
            this._list.Add(MCardType_INNO);
        }

        public const string MCardType_Keyparts = "0";
        public const string MCardType_INNO = "1";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "MCardType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    public class TransactionStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public TransactionStatus()
        {
            this._list.Add(TransactionStatus_NO);
            this._list.Add(TransactionStatus_YES);
        }

        public const string TransactionStatus_NO = "NO";
        public const string TransactionStatus_YES = "YES";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "TransactionStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    public class ProductComplete : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public ProductComplete()
        {
            this._list.Add(Complete);
            this._list.Add(NoComplete);
        }

        public const string Complete = "1";
        public const string NoComplete = "0";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ProductComplete";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region Pick--拣货任务令头 add by jinger 20160303

    public class PickHeadStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public PickHeadStatus()
        {
            this._list.Add(PickHeadStatus_Release);
            this._list.Add(PickHeadStatus_WaitPick);
            this._list.Add(PickHeadStatus_Pick);
            this._list.Add(PickHeadStatus_MakePackingList);
            this._list.Add(PickHeadStatus_Pack);
            this._list.Add(PickHeadStatus_OQC);
            this._list.Add(PickHeadStatus_ClosePackingList);
            this._list.Add(PickHeadStatus_Close);
            this._list.Add(PickHeadStatus_Cancel);
            this._list.Add(PickHeadStatus_Block);
        }
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
        public const string PickHeadStatus_PackingListing = "PackingListing";//制作箱单中



        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "PickHeadStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }


    #endregion

    #region Pick--拣货任务令头 add by sam 20160303

    public class PickType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public PickType()
        {
            this._list.Add(PickType_POC);
            this._list.Add(PickType_DNC);
            this._list.Add(PickType_UB);
            this._list.Add(PickType_JCC);
            this._list.Add(PickType_BLC);
            this._list.Add(PickType_PRC);
            this._list.Add(PickType_PD);
            this._list.Add(PickType_DNZC);
            this._list.Add(PickType_WWPOC);
            this._list.Add(PickType_BFC);
        }
        public const string PickType_POC = "POC";//Return PO
        public const string PickType_DNC = "DNC";//DN出库
        public const string PickType_UB = "UB";//调拨
        public const string PickType_JCC = "JCC";//检测返工出库
        public const string PickType_BLC = "BLC";//不良品出库
        public const string PickType_PRC = "PRC";//PR领料
        public const string PickType_PD = "PD";//盘点
        public const string PickType_DNZC = "DNZC";//DN直发出库
        public const string PickType_WWPOC = "WWPOC";//生产领料
        public const string PickType_BFC = "BFC";//报废出库

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "PickType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }


    #endregion

    #region OQC

    //OQC单据类型 add by jinger 20160303
    public class OQCStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public OQCStatus()
        {
            this._list.Add(OQCStatus_Release);
            this._list.Add(OQCStatus_WaitCheck);
            this._list.Add(OQCStatus_SQEJudge);
            this._list.Add(OQCStatus_OQCClose);
            this._list.Add(OQCStatus_SQEFail);
            this._list.Add(OQCStatus_Cancel);
        }
        public const string OQCStatus_Release = "Release";//初始化
        public const string OQCStatus_WaitCheck = "WaitCheck";//待检验
        public const string OQCStatus_SQEJudge = "SQEJudge";//SQE判定
        public const string OQCStatus_OQCClose = "OQCClose";//OQC检验完成
        public const string OQCStatus_SQEFail = "SQEFail";//SQE检验FAIL
        public const string OQCStatus_Cancel = "Cancel";//取消

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "OQCStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    //OQC检验方式 add by jinger 20160303
    public class OQCType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public OQCType()
        {
            this._list.Add(OQCType_SpotCheck);
            this._list.Add(OQCType_FullCheck);
            this._list.Add(OQCType_ExemptCheck);
        }
        public static readonly string OQCType_SpotCheck = "SpotCheck";//抽检
        public static readonly string OQCType_FullCheck = "FullCheck";//全检
        public static readonly string OQCType_ExemptCheck = "ExemptCheck";//免检

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "OQCType"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    public class OQCLotStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public OQCLotStatus()
        {
            this._list.Add(OQCLotStatus_Initial);
            this._list.Add(OQCLotStatus_SendExame);
            this._list.Add(OQCLotStatus_NoExame);
            this._list.Add(OQCLotStatus_Examing);
            this._list.Add(OQCLotStatus_Pass);
            this._list.Add(OQCLotStatus_Reject);
            this._list.Add(OQCLotStatus_Passing);
            this._list.Add(OQCLotStatus_Rejecting);
        }
        public const string OQCLotStatus_Initial = "oqclotstatus_initial";
        public const string OQCLotStatus_SendExame = "oqclotstatus_sendexame";
        public const string OQCLotStatus_NoExame = "oqclotstatus_noexame";
        public const string OQCLotStatus_Examing = "oqclotstatus_examing";
        public const string OQCLotStatus_Pass = "oqclotstatus_pass";
        public const string OQCLotStatus_Reject = "oqclotstatus_reject";
        public const string OQCLotStatus_Passing = "oqclotstatus_passing";
        public const string OQCLotStatus_Rejecting = "oqclotstatus_rejecting";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "OQCLotStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    public class IQCCheckType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public IQCCheckType()
        {
            this._list.Add(IQCCheckType_All);
            this._list.Add(IQCCheckType_Sample);
        }
        public static readonly string IQCCheckType_All = "CheckAll";
        public static readonly string IQCCheckType_Sample = "CheckSample";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "IQCCheckType"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    public class OQCLotType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public OQCLotType()
        {
            this._list.Add(OQCLotType_Normal);
            this._list.Add(OQCLotType_ReDO);
            this._list.Add(OQCLotType_Split);
        }
        public const string OQCLotType_Normal = "oqclottype_normal";
        public const string OQCLotType_ReDO = "oqclottype_redo";
        public const string OQCLotType_Split = "oqclottype_split";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "OQCLotType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    public class ProductionType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public ProductionType()
        {
            this._list.Add(ProductionType_Mass);
            this._list.Add(ProductionType_Try);
            this._list.Add(ProductionType_New);
            this._list.Add(ProductionType_Claim);
        }
        public const string ProductionType_Mass = "productiontype_mass";
        public const string ProductionType_Try = "productiontype_try";
        public const string ProductionType_New = "productiontype_new";
        public const string ProductionType_Claim = "productiontype_claim";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ProductionType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region TSStatus  维修状态		Note By Simone Xu
    public class TSStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public TSStatus()
        {
            this._list.Add(TSStatus_New);
            this._list.Add(TSStatus_Confirm);
            this._list.Add(TSStatus_TS);
            this._list.Add(TSStatus_Split);
            this._list.Add(TSStatus_Scrap);
            this._list.Add(TSStatus_Complete);
            this._list.Add(TSStatus_Reflow);
            this._list.Add(TSStatus_OffMo);
            this._list.Add(TSStatus_RepeatNG);
        }
        public const string TSStatus_New = "tsstatus_new";				//送修
        public const string TSStatus_Confirm = "tsstatus_confirm";		//待修
        public const string TSStatus_TS = "tsstatus_ts";					//维修中
        public const string TSStatus_Split = "tsstatus_split";			//拆解
        public const string TSStatus_Scrap = "tsstatus_scrap";			//报废
        public const string TSStatus_Complete = "tsstatus_complete";		//维修完成
        public const string TSStatus_Reflow = "tsstatus_reflow";			//回流
        public const string TSStatus_OffMo = "tsstatus_offmo";			//脱离工单
        public const string TSStatus_RepeatNG = "tsstatus_repeatng";	//不良品重复测试

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "TSStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region TSType  维修类型
    public class TSType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public TSType()
        {
            this._list.Add(TS_Normal);
            this._list.Add(TS_Misjudge);

        }
        public const string TS_Normal = "ts_normal";				//正常
        public const string TS_Misjudge = "ts_misjudge";		    //误判


        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "TSType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region TSSource  在线,离线维修枚举		Note By Simone Xu  注意:此枚举变量的值在TSFacade中存在 为 TSFacade.TSSource_OnWIP 在线维修	TSFacade.TSSource_TS 离线维修
    public class TSSource : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public TSSource()
        {
            this._list.Add(TSSource_OnWIP);
            this._list.Add(TSSource_TS);
            this._list.Add(TSSource_RMA);
        }
        public const string TSSource_OnWIP = "tssource_onwip";		//在线维修	Note by Simone Xu
        public const string TSSource_TS = "tssource_ts";		    //离线维修  Note by Simone Xu
        public const string TSSource_RMA = "tssource_rma";		    //RMA维修  Note by Simone Xu

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "TSSource";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region Item Loacation
    public class ItemLocationSide : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public ItemLocationSide()
        {
            this._list.Add(ItemLocationSide_A);
            this._list.Add(ItemLocationSide_B);
            this._list.Add(ItemLocationSide_AB);
        }
        public const string ItemLocationSide_A = "A";
        public const string ItemLocationSide_B = "B";
        public const string ItemLocationSide_AB = "AB";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ItemLocationSide";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ItemRoute2OP

    //工序在途程中的位置 第一道,中间,最后一道
    public class Route2OPLocation : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public Route2OPLocation()
        {
            this._list.Add(OPLocation_FIRST);
            this._list.Add(OPLocation_MIDDLE);
            this._list.Add(OPLocation_LAST);
        }
        public const string OPLocation_FIRST = "FIRST";	//第一道
        public const string OPLocation_MIDDLE = "MIDDLE"; //中间
        public const string OPLocation_LAST = "LAST";	//最后一道

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ItemLocationSide";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }


    #endregion

    #region CardType
    public class CardType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public CardType()
        {
            this._list.Add(CardType_Product);
            this._list.Add(CardType_Part);
        }
        public const string CardType_Product = "cardtype_product";
        public const string CardType_Part = "cardtype_part";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "CardType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region StockCollectionType
    public class StockCollectionType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public StockCollectionType()
        {
            this._list.Add(Pallet);
            this._list.Add(Carton);
            this._list.Add(Planate);
            this._list.Add(PCS);
        }

        public const string Pallet = "Pallet";
        public const string Carton = "Carton";
        public const string Planate = "Planate";
        public const string PCS = "PCS";


        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "StockCollectionType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region OWCChartCombinationType		OWC 多图组合的绘图方式
    public class OWCChartCombinationType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public OWCChartCombinationType()
        {
            this._list.Add(OWCCombinationNormal);
            this._list.Add(OWCCombinationPareto);
        }
        public const string OWCCombinationNormal = "owccombinationtype_normal";		//默认绘图方式
        public const string OWCCombinationPareto = "owccombinationtype_pareto";		//Pareto绘图方式	柏拉图

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "OWCChartCombinationType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region 默认时间 01/01/01

    public class DefaultDateTime
    {
        public static DateTime Default
        {
            get
            {
                return new System.DateTime(1, 1, 1);
            }
        }
        public static int DefaultToInt
        {
            get
            {
                return 10101;
            }
        }
    }


    #endregion

    #region SoftCompareStatus  软件比对状态		Note By Simone Xu
    public class SoftCompareStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public SoftCompareStatus()
        {
            this._list.Add(Success);
            this._list.Add(Failed);
        }
        public const string Success = "SoftCompareStatus_Success";				//比对成功 (正常)
        public const string Failed = "SoftCompareStatus_Failed";					//比对失败 (失败)

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "SoftCompareStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region OrderStatus
    public class OrderStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "ORDERSTATUS";

        public OrderStatus()
        {
            this._list.Add(OrderStatus.InProcess);
            this._list.Add(OrderStatus.Completed);
        }
        public const string InProcess = "OrderStatus_InProcess";
        public const string Completed = "OrderStatus_Completed";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "OrderStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ShelfStatus
    public class ShelfStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "SHELFSTATUS";

        public ShelfStatus()
        {
            this._list.Add(ShelfStatus.BurnIn);
            this._list.Add(ShelfStatus.BurnOut);
        }
        public const string BurnIn = "ShelfStatus_BurnIn";
        public const string BurnOut = "ShelfStatus_BurnOut";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ShelfStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region FeederStatus
    public class FeederStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "FEEDERSTATUS";

        public FeederStatus()
        {
            this._list.Add(FeederStatus.Normal);
            this._list.Add(FeederStatus.WaitCheck);
            this._list.Add(FeederStatus.Disabled);
            this._list.Add(FeederStatus.Scrap);
        }
        public const string Normal = "NORMAL";
        public const string WaitCheck = "WAITCHECK";
        public const string Disabled = "DISABLED";
        public const string Scrap = "SCRAP";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "FeederStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region FeederType
    public class FeederType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "FEEDERTYPE";

        public FeederType()
        {
        }

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "FeederType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region FeederOperationType
    public class FeederOperationType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "FEEDEROPERATIONTYPE";

        public FeederOperationType()
        {
            this._list.Add(FeederOperationType.GetOut);
            this._list.Add(FeederOperationType.Exchange);
            this._list.Add(FeederOperationType.Return);
            this._list.Add(FeederOperationType.Maintain);
            this._list.Add(FeederOperationType.Adjust);
            this._list.Add(FeederOperationType.Analyse);
        }
        public const string GetOut = "GetOut";
        public const string Exchange = "Exchange";
        public const string Return = "Return";
        public const string Maintain = "Maintain";
        public const string Adjust = "Adjust";
        public const string Analyse = "Analyse";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "FeederOperationType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region SMTLoadFeederInputType
    public class SMTLoadFeederInputType
    {
        public const string Init = "Init";
        public const string MachineCode = "MachineCode";
        public const string StationCode = "StationCode";
        public const string FeederCode = "FeederCode";
        public const string FeederCodeOld = "FeederCodeOld";
        public const string ReelNo = "ReelNo";
        public const string ReelNoOld = "ReelNoOld";
    }
    #endregion

    #region SMTLoadFeederOperationType
    public class SMTLoadFeederOperationType
    {
        public const string Load = "Load";
        public const string Exchange = "Exchange";
        public const string Continue = "Continue";
        public const string UnLoad = "UnLoad";
        public const string TransferMO = "TransferMO";
        public const string UnLoadSingle = "UnLoadSingle";
        public const string MOEnabled = "MOEnabled";
        public const string MODisabled = "MODisabled";
        public const string ReelExhaust = "ReelExhaust";
        public const string LoadCheck = "LoadCheck";
        public const string Effective = "Effective";
        public const string Invalidate = "Invalidate";
    }
    #endregion

    #region FeederAnalyseReason
    public class FeederAnalyseReason : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "FEEDERANALYSEREASON";

        public FeederAnalyseReason()
        {
        }

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "FeederAnalyseReason";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region FeederSolution
    public class FeederSolution : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "FEEDERSOLUTION";

        public FeederSolution()
        {
        }

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "FeederSolution";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region SoundType
    public class SoundType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "SOUNDTYPE";

        public SoundType()
        {
            this._list.Add(SoundType.Success);
            this._list.Add(SoundType.Error);
        }

        public const string Success = "Success";
        public const string Error = "Error";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "SoundType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region SMTAlertStatus
    public class SMTAlertStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "SMTALERTSTATUS";

        public SMTAlertStatus()
        {
            this._list.Add(SMTAlertStatus.Initial);
            this._list.Add(SMTAlertStatus.Closed);
        }
        public const string Initial = "INITIAL";
        public const string Closed = "CLOSED";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "SMTAlertStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region SMTAlertType
    public class SMTAlertType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "SMTALERTTYPE";

        public SMTAlertType()
        {
            this._list.Add(SMTAlertType.Feeder);
            this._list.Add(SMTAlertType.Reel);
        }
        public const string Feeder = "FEEDER";
        public const string Reel = "REEL";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "SMTAlertType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region SMTCheckMaterialDetailType
    public class SMTCheckMaterialDetailType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "SMTCHECKMATERIALDETAILTYPE";

        public SMTCheckMaterialDetailType()
        {
            this._list.Add(SMTCheckMaterialDetailType.SMT);
            this._list.Add(SMTCheckMaterialDetailType.MOBOM);
        }
        public const string SMT = "SMTCheckMaterialDetailType_SMT";
        public const string MOBOM = "SMTCheckMaterialDetailType_MOBOM";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "SMTCheckMaterialDetailType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region SMTCheckMaterialResultDescription
    public class SMTCheckMaterialResultDescription : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "SMTCHECKMATERIALRESULTDESCRIPTION";

        public SMTCheckMaterialResultDescription()
        {
            this._list.Add(SMTCheckMaterialResultDescription.Matchable);
            this._list.Add(SMTCheckMaterialResultDescription.MOBOM_Exception);
            this._list.Add(SMTCheckMaterialResultDescription.Qty_Exception);
            this._list.Add(SMTCheckMaterialResultDescription.MOBOM_Only);
        }
        public const string Matchable = "SMTCheckMaterialResultDescription_Matchable";
        public const string MOBOM_Exception = "SMTCheckMaterialResultDescription_MOBOM_Exception";
        public const string Qty_Exception = "SMTCheckMaterialResultDescription_Qty_Exception";
        public const string MOBOM_Only = "SMTCheckMaterialResultDescription_MOBOM_Only";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "SMTCheckMaterialResultDescription";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region SMTCheckMaterialResult
    public class SMTCheckMaterialResult : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "SMTCHECKMATERIALRESULT";

        public SMTCheckMaterialResult()
        {
            this._list.Add(SMTCheckMaterialResult.Matchable);
            this._list.Add(SMTCheckMaterialResult.Accept_Exception);
            this._list.Add(SMTCheckMaterialResult.Not_Confirm);
        }
        public const string Matchable = "SMTCheckMaterialResult_Matchable";
        public const string Accept_Exception = "SMTCheckMaterialResult_Accept_Exception";
        public const string Not_Confirm = "SMTCheckMaterialResult_Not_Confirm";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "SMTCheckMaterialResult";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region RMA added by jessie lee

    #region RMAItemIssue
    public class RMAItemIssue : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "RMAITEMISSUE";

        public RMAItemIssue()
        {
            this._list.Add(RMAItemIssue.QualityIssue);
            this._list.Add(RMAItemIssue.NonQualityIssue);
        }
        public const string QualityIssue = "RMAItemIssue_QualityIssue";
        public const string NonQualityIssue = "RMAItemIssue_NonQualityIssue";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "RMAItemIssue";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region RMAIssueKide
    public class RMAIssueKide : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "RMAISSUEKIDE";

        public RMAIssueKide()
        {
            this._list.Add(RMAIssueKide.FunctionError);
            this._list.Add(RMAIssueKide.AppearanceError);
            this._list.Add(RMAIssueKide.OperationIssue);
            this._list.Add(RMAIssueKide.AgainstSystemProcedure);
        }
        public const string FunctionError = "RMAIssueKide_FunctionError";
        public const string AppearanceError = "RMAIssueKide_AppearanceError";
        public const string OperationIssue = "RMAIssueKide_OperationIssue";
        public const string AgainstSystemProcedure = "RMAIssueKide_AgainstSystemProcedure";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "RMAIssueKide";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region RMAHandleWay
    public class RMAHandleWay : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "RMAHANDLEWAY";

        public RMAHandleWay()
        {
            this._list.Add(RMAHandleWay.TSCenter);
            this._list.Add(RMAHandleWay.Rework);
            this._list.Add(RMAHandleWay.Scrap);
        }
        public const string TSCenter = "RMAHandleWay_TSCenter";
        public const string Rework = "RMAHandleWay_Rework";
        public const string Scrap = "RMAHandleWay_Scrap";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "RMAHandleWay";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region RMABillStatus
    public class RMABillStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "RMABILLSTATUS";

        public RMABillStatus()
        {
            this._list.Add(RMABillStatus.Initial);
            this._list.Add(RMABillStatus.Release);
            this._list.Add(RMABillStatus.Closed);
            this._list.Add(RMABillStatus.Opened);
        }
        public const string Initial = "RMABillStatus_Initial";
        public const string Release = "RMABillStatus_Release";
        public const string Closed = "RMABillStatus_Closed";
        public const string Opened = "RMABillStatus_Opened";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "RMABillStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region RMATimeKide
    public class RMATimeKide : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "RMATIMEKIDE";

        public RMATimeKide()
        {
            this._list.Add(RMATimeKide.Day);
            this._list.Add(RMATimeKide.Week);
            this._list.Add(RMATimeKide.Month);
        }
        public const string Day = "RMATimeKide_Day";
        public const string Week = "RMATimeKide_Week";
        public const string Month = "RMATimeKide_Month";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "RMATimeKide";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #endregion

    #region SolderPasteType
    public class SolderPasteType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "SOLDERPASTETYPE";

        public SolderPasteType()
        {
            this._list.Add(SolderPasteType.Pb_Free);
            this._list.Add(SolderPasteType.Pb);
            this._list.Add(SolderPasteType.Tin_Antimony);
            this._list.Add(SolderPasteType.HighTemperature);
        }
        public const string Pb_Free = "SolderPasteType_PbFree";
        public const string Pb = "SolderPasteType_Pb";
        public const string Tin_Antimony = "SolderPasteType_TinAntimony";
        public const string HighTemperature = "SolderPasteType_HighTemperature";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "SolderPasteType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region SolderPasteStatus
    public class SolderPasteStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "SOLDERPASTESTATUS";

        public SolderPasteStatus()
        {
            this._list.Add(SolderPasteStatus.Normal);
            this._list.Add(SolderPasteStatus.Return);
            this._list.Add(SolderPasteStatus.Restrain);
            this._list.Add(SolderPasteStatus.Agitate);
            this._list.Add(SolderPasteStatus.Unveil);
            this._list.Add(SolderPasteStatus.scrap);
            this._list.Add(SolderPasteStatus.UsedUp);
            this._list.Add(SolderPasteStatus.Reflow);
        }
        public const string Normal = "SolderPasteStatus_Normal";	//Noraml
        public const string Return = "SolderPasteStatus_Return";	//回温
        public const string Restrain = "SolderPasteStatus_Restrain";//停用
        public const string Agitate = "SolderPasteStatus_Agitate";//搅拌
        public const string Unveil = "SolderPasteStatus_Unveil";//开封
        public const string scrap = "SolderPasteStatus_scrap";//报废
        public const string UsedUp = "SolderPasteStatus_UsedUp";//用完
        public const string Reflow = "SolderPasteStatus_Reflow";//回存			

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "SolderPasteStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ArmorPlateStatus
    public class ArmorPlateStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "ARMORPLATESTATUS";

        public ArmorPlateStatus()
        {
            this._list.Add(ArmorPlateStatus.StartUsing);
            this._list.Add(ArmorPlateStatus.EndUsing);
        }
        public const string StartUsing = "ArmorPlateStatus_StartUsing";
        public const string EndUsing = "ArmorPlateStatus_EndUsing";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ArmorPlateStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region SPCChartType
    public class SPCChartType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "SPCCHARTTYPE";

        public SPCChartType()
        {
            this._list.Add(SPCChartType.XBar_R_Chart);
            this._list.Add(SPCChartType.NormalDistributionDiagram);
            this._list.Add(SPCChartType.OQC_NormalDistributionDiagram);
        }
        public const string XBar_R_Chart = "SPCChartType_XBar_R_Chart";	//X Bar-R Chart		
        public const string NormalDistributionDiagram = "SPCChartType_NDD";	//正态分布图
        public const string OQC_NormalDistributionDiagram = "SPCChartType_OQC_NDD";  //OQC正态分布图

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "SPCChartType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region INVRardType
    public class INVRardType
    {
        public const string Normal = "NORMAL";
        public const string Unnormal = "UNNORMAL";
    }

    #endregion

    #region SPC Object Code
    public class SPCObjectList
    {
        public const string FT_DUTY_RATO = "FT_DUTY_RATO";   //Added by Anco 2006/09/28
        public const string FT_BURST_MD = "FT_BURST_MD";     //Added by Anco 2006/09/28

        public const string PT_ELECTRIC = "PT_ELECTRIC";
        public const string FT_FREQUENCY = "FT_FREQUENCY";
        public const string FT_ELECTRIC = "FT_ELECTRIC";
        public const string OQC_DUTY_RATO = "OQC_DUTY_RATO";
        public const string OQC_BURST_MD = "OQC_BURST_MD";
        public const string OQC_FT_FREQUENCY = "OQC_FT_FREQUENCY";
        public const string OQC_FT_ELECTRIC = "OQC_FT_ELECTRIC";
        public const string OQC_DIM_LENGTH = "OQC_DIM_LENGTH";
        public const string OQC_DIM_WIDTH = "OQC_DIM_WIDTH";
        public const string OQC_DIM_BOARDHEIGHT = "OQC_DIM_BOARDHEIGHT";
        public const string OQC_DIM_HEIGHT = "OQC_DIM_HEIGHT";
        public const string OQC_DIM_ALLHEIGHT = "OQC_DIM_ALLHEIGHT";
        public const string OQC_DIM_LEFT2RIGHT = "OQC_DIM_LEFT2RIGHT";
        public const string OQC_DIM_LEFT2MIDDLE = "OQC_DIM_LEFT2MIDDLE";
        public const string OQC_DIM_RIGHT2MIDDLE = "OQC_DIM_RIGHT2MIDDLE";
    }
    #endregion

    #region DateRange
    public class DateRange : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "DateRange";

        public DateRange()
        {
            this._list.Add(DateRange.Single);
            this._list.Add(DateRange.Range);
        }
        public const string Single = "SINGLE";
        public const string Range = "RANGE";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "DateRange";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region MORunningCardType
    public class MORunningCardType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "MORunningCardType";

        public MORunningCardType()
        {
            this._list.Add(MORunningCardType.BeforeConvert);
            this._list.Add(MORunningCardType.AfterConvert);
        }
        public const string BeforeConvert = "morcardrangetype_before_convert";
        public const string AfterConvert = "morcardrangetype_after_convert";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "MORunningCardType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region Resource2MOGetType
    public class Resource2MOGetType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "Resource2MOGetType";

        public Resource2MOGetType()
        {
            this._list.Add(Resource2MOGetType.Static);
            this._list.Add(Resource2MOGetType.GetFromRCard);
        }
        public const string Static = "resource2mo_gettype_static";
        public const string GetFromRCard = "resource2mo_gettype_getfromrcard";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "Resource2MOGetType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region DatabaseType
    public class DatabaseType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "DatabaseType";

        public DatabaseType()
        {
            this._list.Add(DatabaseType.Oracle);
            this._list.Add(DatabaseType.SQLServer);
        }
        public const string Oracle = "oracle";
        public const string SQLServer = "sqlserver";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "DatabaseType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region DataSourceType
    public class DataSourceType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "DataSourceType";

        public DataSourceType()
        {
            this._list.Add(DataSourceType.SQL);
            this._list.Add(DataSourceType.DLL);
        }
        public const string SQL = "sql";
        public const string DLL = "dll";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "DataSourceType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ReportDataType
    public class ReportDataType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "ReportDataType";

        public ReportDataType()
        {
            this._list.Add(ReportDataType.String);
            this._list.Add(ReportDataType.Numeric);
            this._list.Add(ReportDataType.Date);
        }
        public const string String = "string";
        public const string Numeric = "numeric";
        public const string Date = "date";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ReportDataType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ReportTotalType
    public class ReportTotalType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "ReportTotalType";

        public ReportTotalType()
        {
            this._list.Add(ReportTotalType.Empty);
            this._list.Add(ReportTotalType.Sum);
            this._list.Add(ReportTotalType.Avg);
            this._list.Add(ReportTotalType.Count);
            this._list.Add(ReportTotalType.Max);
        }
        public const string Empty = "reporttotaltype_empty";
        public const string Sum = "reporttotaltype_sum";
        public const string Avg = "reporttotaltype_avg";
        public const string Count = "reporttotaltype_count";
        public const string Max = "reporttotaltype_max";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ReportTotalType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ReportFilterType
    public class ReportFilterType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "ReportFilterType";

        public ReportFilterType()
        {
            this._list.Add(ReportFilterType.Equal);
            this._list.Add(ReportFilterType.Greater);
            this._list.Add(ReportFilterType.GreaterEqual);
            this._list.Add(ReportFilterType.Lesser);
            this._list.Add(ReportFilterType.LesserEqual);
            this._list.Add(ReportFilterType.LeftMatch);
            this._list.Add(ReportFilterType.RightMatch);
        }
        public const string Equal = "reportfiltertype_equal";
        public const string Greater = "reportfiltertype_greater";
        public const string GreaterEqual = "reportfiltertype_greaterequal";
        public const string Lesser = "reportfiltertype_lesser";
        public const string LesserEqual = "reportfiltertype_lesserequal";
        public const string LeftMatch = "reportfiltertype_leftmatch";
        public const string RightMatch = "reportfiltertype_rightmatch";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ReportFilterType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ReportStyleType
    public class ReportStyleType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "ReportStyleType";

        public ReportStyleType()
        {
            this._list.Add(ReportStyleType.Header);
            this._list.Add(ReportStyleType.Item);
            this._list.Add(ReportStyleType.SubTotal);
            this._list.Add(ReportStyleType.SubTotalGroupField);
            this._list.Add(ReportStyleType.SubTotalCalField);
            this._list.Add(ReportStyleType.SubTotalNonCalField);
        }
        public const string Header = "reportstyletype_header";
        public const string Item = "reportstyletype_item";
        public const string SubTotal = "reportstyletype_subtotal";
        public const string SubTotalGroupField = "reportstyletype_subtotal_groupfield";
        public const string SubTotalCalField = "reportstyletype_subtotal_calfield";
        public const string SubTotalNonCalField = "reportstyletype_subtotal_noncalfield";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ReportStyleType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region TextAlign
    public class TextAlign : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "TextAlign";

        public TextAlign()
        {
            this._list.Add(TextAlign.Left);
            this._list.Add(TextAlign.Right);
            this._list.Add(TextAlign.Center);
        }
        public const string Left = "textalign_left";
        public const string Right = "textalign_right";
        public const string Center = "textalign_center";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "TextAlign";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region VerticalAlign
    public class VerticalAlign : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "VerticalAlign";

        public VerticalAlign()
        {
            this._list.Add(VerticalAlign.Top);
            this._list.Add(VerticalAlign.Bottom);
            this._list.Add(VerticalAlign.Middle);
        }
        public const string Top = "verticalalign_top";
        public const string Bottom = "verticalalign_bottom";
        public const string Middle = "verticalalign_middle";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "VerticalAlign";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ReportDisplayType
    public class ReportDisplayType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "ReportDisplayType";

        public ReportDisplayType()
        {
            this._list.Add(ReportDisplayType.Grid);
            this._list.Add(ReportDisplayType.Chart);
            this._list.Add(ReportDisplayType.GridChart);
        }
        public const string Grid = "reportdisplaytype_grid";
        public const string Chart = "reportdisplaytype_chart";
        public const string GridChart = "reportdisplaytype_gridchart";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ReportDisplayType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ReportBuilder
    public class ReportBuilder : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "ReportBuilder";

        public ReportBuilder()
        {
            this._list.Add(ReportBuilder.OnLine);
            this._list.Add(ReportBuilder.OffLine);
        }
        public const string OnLine = "reportbuilder_online";
        public const string OffLine = "reportbuilder_offline";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ReportBuilder";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ReportDesignStatus
    public class ReportDesignStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "ReportDesignStatus";

        public ReportDesignStatus()
        {
            this._list.Add(ReportDesignStatus.Initial);
            this._list.Add(ReportDesignStatus.Publish);
            this._list.Add(ReportDesignStatus.ReDesign);
        }
        public const string Initial = "reportdesignstatus_initial";
        public const string Publish = "reportdesignstatus_publish";
        public const string ReDesign = "reportdesignstatus_redesign";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ReportDesignStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ReportChartType
    public class ReportChartType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "ReportChartType";

        public ReportChartType()
        {
            this._list.Add(ReportChartType.Column);
            this._list.Add(ReportChartType.Bar);
            this._list.Add(ReportChartType.Line);
            this._list.Add(ReportChartType.Pie);
        }
        public const string Column = "Column";
        public const string Bar = "Bar";
        public const string Line = "Line";
        public const string Pie = "Pie";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ReportChartType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ReportChartSubType
    public class ReportChartSubType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "ReportChartSubType";

        public ReportChartSubType()
        {
            this._list.Add(ReportChartSubType.Plain);
            this._list.Add(ReportChartSubType.Stacked);
            this._list.Add(ReportChartSubType.PercentStacked);
            this._list.Add(ReportChartSubType.Smooth);
            this._list.Add(ReportChartSubType.Exploded);
        }
        public const string Plain = "Plain";
        public const string Stacked = "Stacked";
        public const string PercentStacked = "PercentStacked";
        public const string Smooth = "Smooth";
        public const string Exploded = "Exploded";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ReportChartSubType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ReportMarkerType
    public class ReportMarkerType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "ReportMarkerType";

        public ReportMarkerType()
        {
            this._list.Add(ReportMarkerType.Cross);
            this._list.Add(ReportMarkerType.Square);
            this._list.Add(ReportMarkerType.Diamond);
            this._list.Add(ReportMarkerType.Circle);
            this._list.Add(ReportMarkerType.Triangle);
        }
        public const string Cross = "Cross";
        public const string Square = "Square";
        public const string Diamond = "Diamond";
        public const string Circle = "Circle";
        public const string Triangle = "Triangle";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ReportMarkerType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ReportViewerInputType
    public class ReportViewerInputType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "ReportViewerInputType";

        public ReportViewerInputType()
        {
            this._list.Add(ReportViewerInputType.SqlFilter);
            this._list.Add(ReportViewerInputType.DllParameter);
            this._list.Add(ReportViewerInputType.FileParameter);
        }
        public const string SqlFilter = "reportviewerinputtype_sqlfilter";
        public const string DllParameter = "reportviewerinputtype_dllparameter";
        public const string FileParameter = "reportviewerinputtype_fileparameter";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ReportViewerInputType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ReportEntryType
    public class ReportEntryType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "ReportEntryType";

        public ReportEntryType()
        {
            this._list.Add(ReportEntryType.Folder);
            this._list.Add(ReportEntryType.Report);
        }
        public const string Folder = "reportentrytype_folder";
        public const string Report = "reportentrytype_report";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ReportEntryType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region ReportFilterUIType
    public class ReportFilterUIType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "ReportFilterUIType";

        public ReportFilterUIType()
        {
            this._list.Add(ReportFilterUIType.InputText);
            this._list.Add(ReportFilterUIType.Date);
            this._list.Add(ReportFilterUIType.CheckBox);
            this._list.Add(ReportFilterUIType.SelectQuery);
            this._list.Add(ReportFilterUIType.DropDownList);
            this._list.Add(ReportFilterUIType.SelectComplex);
        }
        public const string InputText = "reportfilteruitype_inputtext";
        public const string Date = "reportfilteruitype_date";
        public const string CheckBox = "reportfilteruitype_checkbox";
        public const string SelectQuery = "reportfilteruitype_selectquery";
        public const string DropDownList = "reportfilteruitype_dropdownlist";
        public const string SelectComplex = "reportfilteuitype_selectcomplex"; //复选框


        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ReportFilterUIType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region LabelType
    public class LabelType
    {
        public const string LABELTYPE_RCARD = "labeltype_rcard";
        public const string LABELTYPE_CUSTSN = "labeltype_custsn";
        public const string LABELTYPE_CARTONNO = "labeltype_cartonno";
        public const string LABELTYPE_SEND = "labeltype_send";
        public const string LABELTYPE_SHIP = "labeltype_ship";
        public const string LABELTYPE_VA_SN = "labeltype_va_sn";
        public const string LABELTYPE_DB_SN = "labeltype_db_sn";


        public const string GroupType = "LabelType";
        private ArrayList _list = new ArrayList();
        public LabelType()
        {
            this._list.Add(LabelType.LABELTYPE_RCARD);
            this._list.Add(LabelType.LABELTYPE_CUSTSN);
            this._list.Add(LabelType.LABELTYPE_CARTONNO);
            this._list.Add(LabelType.LABELTYPE_SEND);
            this._list.Add(LabelType.LABELTYPE_SHIP);
            this._list.Add(LabelType.LABELTYPE_VA_SN);
            this._list.Add(LabelType.LABELTYPE_DB_SN);
        }

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return GroupType;
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region WorkingErrorFunctionType

    public class WorkingErrorFunctionType
    {
        public const string DCT = "DCT";
        public const string CS = "CS";

        public const string GroupType = "WorkingErrorFunctionType";
        private ArrayList _list = new ArrayList();
        public WorkingErrorFunctionType()
        {
            this._list.Add(WorkingErrorFunctionType.DCT);
            this._list.Add(WorkingErrorFunctionType.CS);
        }

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return GroupType;
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region WorkingErrorStatus

    public class WorkingErrorStatus
    {
        public const string NEW = "NEW";
        public const string DEAL = "DEAL";

        public const string GroupType = "WorkingErrorStatus";
        private ArrayList _list = new ArrayList();
        public WorkingErrorStatus()
        {
            this._list.Add(WorkingErrorStatus.NEW);
            this._list.Add(WorkingErrorStatus.DEAL);
        }

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return GroupType;
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region NewReportDisplayType

    public class NewReportDisplayType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "NewReportDisplayType";

        public NewReportDisplayType()
        {
            this._list.Add(NewReportDisplayType.Grid);
            this._list.Add(NewReportDisplayType.LineChart);
            this._list.Add(NewReportDisplayType.HistogramChart);
            this._list.Add(NewReportDisplayType.PieChart);
        }
        public const string Grid = "newreportdisplaytype_grid";
        public const string LineChart = "newreportdisplaytype_linechart";
        public const string HistogramChart = "newreportdisplaytype_histogramchart";
        public const string PieChart = "newreportdisplaytype_piechart";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "NewReportDisplayType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region NewReportQueryDataType

    public class NewReportQueryDataType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "NewReportQueryDataType";

        public NewReportQueryDataType()
        {
            this._list.Add(NewReportQueryDataType.DataNumber);
            this._list.Add(NewReportQueryDataType.DataRate);
        }
        public const string DataNumber = "querydatatype_number";
        public const string DataRate = "querydatatype_rate";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "NewReportQueryDataType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region NewReportByTimeType

    public class NewReportByTimeType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "NewReportByTimeType";

        public NewReportByTimeType()
        {
            this._list.Add(NewReportByTimeType.Period);
            this._list.Add(NewReportByTimeType.Shift);
            this._list.Add(NewReportByTimeType.ShiftDay);
            this._list.Add(NewReportByTimeType.Week);
            this._list.Add(NewReportByTimeType.Month);
            this._list.Add(NewReportByTimeType.Year);
        }
        public const string Period = "newreportbytimetype_period";
        public const string Shift = "newreportbytimetype_shift";
        public const string ShiftDay = "newreportbytimetype_shiftday";
        public const string Week = "newreportbytimetype_week";
        public const string Month = "newreportbytimetype_month";
        public const string Year = "newreportbytimetype_year";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "NewReportByTimeType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region NewReportCompareType

    public class NewReportCompareType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "NewReportCompareType";

        public NewReportCompareType()
        {
            this._list.Add(NewReportCompareType.Previous);
            this._list.Add(NewReportCompareType.LastYear);
        }
        public const string Previous = "newreportcomparetype_previous";
        public const string LastYear = "newreportcomparetype_lastyear";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "NewReportCompareType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region NewReportCompleteType

    public class NewReportCompleteType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "NewReportCompleteType";

        public NewReportCompleteType()
        {
            this._list.Add(NewReportCompleteType.Offline);
            this._list.Add(NewReportCompleteType.Complete);
        }
        public const string Offline = "newreportcompletetype_offline";
        public const string Complete = "newreportcompletetype_complete";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "NewReportCompleteType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region NewReportExceptionOrDuty

    public class NewReportExceptionOrDuty : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "NewReportExceptionOrDuty";

        public NewReportExceptionOrDuty()
        {
            this._list.Add(NewReportExceptionOrDuty.Exception);
            this._list.Add(NewReportExceptionOrDuty.Duty);
        }
        public const string Exception = "newreportexceptionorduty_exception";
        public const string Duty = "newreportexceptionorduty_duty";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "NewReportExceptionOrDuty";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region MaterialExportImportType

    public class MaterialExportImportType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public const string GroupType = "MaterialExportImportType";

        public MaterialExportImportType()
        {
            this._list.Add(MaterialExportImportType.Export);
            this._list.Add(MaterialExportImportType.Import);
        }
        public const string Export = "materialexportimport_export";
        public const string Import = "materialexportimport_import";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "MaterialExportImportType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region SoftWareVersionStatus
    public class SoftWareVersionStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public SoftWareVersionStatus()
        {
            this._list.Add(SoftWareVersionStatus.Valid);
            this._list.Add(SoftWareVersionStatus.InValid);
        }

        public const string Valid = "type_valid";
        public const string InValid = "type_invalid";

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "SoftWareVersionStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region DNStatus
    public class DNStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public DNStatus()
        {
            this._list.Add(SoftWareVersionStatus.Valid);
            this._list.Add(SoftWareVersionStatus.InValid);
        }

        public const string StatusInit = "status_init";
        public const string StatusUsing = "status_using";
        public const string StatusClose = "status_close";


        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "DNStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region DNFrom
    public class DNFrom : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public DNFrom()
        {
            this._list.Add(SoftWareVersionStatus.Valid);
            this._list.Add(SoftWareVersionStatus.InValid);
        }

        public const string MES = "MES";
        public const string ERP = "ERP";


        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "DNFrom";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region FlagStatus

    public class FlagStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public FlagStatus()
        {
            this._list.Add(FlagStatus.FlagStatus_MES);
            this._list.Add(FlagStatus.FlagStatus_SAP);
            this._list.Add(FlagStatus.FlagStatus_SRM);
            this._list.Add(FlagStatus.FlagStatus_POST);
        }

        public const string FlagStatus_MES = "MES";
        public const string FlagStatus_SAP = "SAP";
        public const string FlagStatus_SRM = "SRM";
        public const string FlagStatus_POST = "POST";


        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "FlagStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region OutInvRuleCheck
    public class OutInvRuleCheck : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public OutInvRuleCheck()
        {
            this._list.Add(OutInvRuleCheck.ProductIsFinished);
            this._list.Add(OutInvRuleCheck.ProductIsNotFinished);
            this._list.Add(OutInvRuleCheck.ProductIsFrozen);
            this._list.Add(OutInvRuleCheck.ProductIsDown);
            this._list.Add(OutInvRuleCheck.ProductIsFrozen);
            this._list.Add(OutInvRuleCheck.RelatedInvOutDoc);
            this._list.Add(OutInvRuleCheck.InSAPNotCompeleted);
        }

        public const string ProductIsFinished = "PRODUCTISFINISHED";
        public const string ProductIsNotFinished = "PRODUCTISNOTFINISHED";
        public const string ProductIsFrozen = "PRODUCTISFROZEN";

        public const string ProductIsDown = "PRODUCTISDOWN";
        public const string ProductIsPause = "PRODUCTISPAUSE";
        public const string RelatedInvOutDoc = "RELATEDINVOUTDOC";
        public const string DNConfirmSAP = "DNCONFIRM";

        public const string InSAPNotCompeleted = "INSAPNOTCOMPELETED";


        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "OutInvRuleCheck";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region DownStatus

    public class DownStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public DownStatus()
        {
            this._list.Add(DownStatus_Down);
            this._list.Add(DownStatus_Up);
        }
        public static readonly string DownStatus_Down = "status_down";
        public static readonly string DownStatus_Up = "status_up";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "DownStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region IQC 

    //Modify by jinger 2016-02-18
    public class IQCStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public IQCStatus()
        {
            this._list.Add(IQCStatus_Release);
            this._list.Add(IQCStatus_WaitCheck);
            this._list.Add(IQCStatus_SQEJudge);
            this._list.Add(IQCStatus_IQCClose);
            this._list.Add(IQCStatus_IQCRejection);
            this._list.Add(IQCStatus_Cancel);
        }
        //public static readonly string IQCStatus_New = "New";
        //public static readonly string IQCStatus_Release = "Release";
        //public static readonly string IQCStatus_WaitCheck = "WaitCheck";
        //public static readonly string IQCStatus_UnReceipt = "UnReceipt";
        //public static readonly string IQCStatus_Close = "Close";
        //public static readonly string IQCStatus_Cancel = "Cancel";

        public static readonly string IQCStatus_Release = "Release";
        public static readonly string IQCStatus_WaitCheck = "WaitCheck";
        public static readonly string IQCStatus_SQEJudge = "SQEJudge";
        public static readonly string IQCStatus_IQCClose = "IQCClose";
        public static readonly string IQCStatus_IQCRejection = "IQCRejection";
        public static readonly string IQCStatus_Cancel = "Cancel";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "IQCStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    //IQC检验方式 add by jinger 20160226
    public class IQCType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public IQCType()
        {
            this._list.Add(IQCType_SpotCheck);
            this._list.Add(IQCType_FullCheck);
            this._list.Add(IQCType_ExemptCheck);
        }

        public static readonly string IQCType_SpotCheck = "SpotCheck";//抽检
        public static readonly string IQCType_FullCheck = "FullCheck";//全检
        public static readonly string IQCType_ExemptCheck = "ExemptCheck";//免检

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "IQCType"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    //SQE判定状态 add by jinger 20160227
    public class SQEStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public SQEStatus()
        {
            this._list.Add(SQEStatus_Return);
            this._list.Add(SQEStatus_Reform);
            this._list.Add(SQEStatus_Give);
            this._list.Add(SQEStatus_Accept);
        }

        public static readonly string SQEStatus_Return = "Return";//退换货
        public static readonly string SQEStatus_Reform = "Reform";//现场整改
        public static readonly string SQEStatus_Give = "Give";//让步接收
        public static readonly string SQEStatus_Accept = "Accept";//特采

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "SQEStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }


    public class IQCSyncStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public IQCSyncStatus()
        {
            this._list.Add(IQCSyncStatus_New);
            this._list.Add(IQCSyncStatus_Closed);
        }
        public static readonly string IQCSyncStatus_New = "New";
        public static readonly string IQCSyncStatus_Closed = "Closed";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "IQCSyncStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    public class IQCTicketType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public IQCTicketType()
        {
            this._list.Add(IQCTicketType_ASN);
            this._list.Add(IQCTicketType_PO);
        }
        public static readonly string IQCTicketType_ASN = "ASN";
        public static readonly string IQCTicketType_PO = "PO";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "IQCTicketType"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    public class IQCHeadAttribute : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public IQCHeadAttribute()
        {
            this._list.Add(IQCHeadAttribute_Normal);
            this._list.Add(IQCHeadAttribute_RePO);
            this._list.Add(IQCHeadAttribute_Present);
            this._list.Add(IQCHeadAttribute_BranchReturn);
        }
        public static readonly string IQCHeadAttribute_Normal = "iqcheadattribute_normal";                      //正常
        public static readonly string IQCHeadAttribute_RePO = "iqcheadattribute_repo";                          //回购
        public static readonly string IQCHeadAttribute_Present = "iqcheadattribute_present";                    //赠送
        public static readonly string IQCHeadAttribute_BranchReturn = "iqcheadattribute_branchreturn";          //分公司退料


        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "IQCHeadAttribute"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    public class IQCDetailAttribute : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public IQCDetailAttribute()
        {
            this._list.Add(IQCDetailAttribute_Normal);
            this._list.Add(IQCDetailAttribute_Claim);
            this._list.Add(IQCDetailAttribute_Try);
            this._list.Add(IQCDetailAttribute_TS_Market);
        }
        public static readonly string IQCDetailAttribute_Normal = "iqcdetailattribute_normal";                      //正常
        public static readonly string IQCDetailAttribute_Claim = "iqcdetailattribute_claim";                        //理赔
        public static readonly string IQCDetailAttribute_Try = "iqcdetailattribute_try";                            //试流
        public static readonly string IQCDetailAttribute_TS_Market = "iqcdetailattribute_ts_market";                //维修（市场）


        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "IQCDetailAttribute"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    public class IQCCheckStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public IQCCheckStatus()
        {
            this._list.Add(IQCCheckStatus_WaitCheck);
            this._list.Add(IQCCheckStatus_Qualified);
            this._list.Add(IQCCheckStatus_UnQualified);
        }
        public static readonly string IQCCheckStatus_WaitCheck = "WaitCheck";
        public static readonly string IQCCheckStatus_Qualified = "Qualified";
        public static readonly string IQCCheckStatus_UnQualified = "UnQualified";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "IQCCheckStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    public class IQCReceiveType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public IQCReceiveType()
        {
            this._list.Add(IQCReceiveType_Normal);
            this._list.Add(IQCReceiveType_SpecialPurchase);
            this._list.Add(IQCReceiveType_Instead);
        }
        public static readonly string IQCReceiveType_Normal = "iqcreceivetype_normal";                      //正常
        public static readonly string IQCReceiveType_SpecialPurchase = "iqcreceivetype_specialpurchase";    //特采
        public static readonly string IQCReceiveType_Instead = "iqcreceivetype_instead";                    //代用

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "IQCReceiveType"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }


    public class IQCConcessionStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public IQCConcessionStatus()
        {
            this._list.Add(IQCConcessionStatus_Y);
            this._list.Add(IQCConcessionStatus_N);
        }
        public static readonly string IQCConcessionStatus_Y = "Y";                      //让步接受
        public static readonly string IQCConcessionStatus_N = "N";                      //没有让步接受

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "IQCConcessionStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    public class MaterialIsSMT : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public MaterialIsSMT()
        {
            this._list.Add(MaterialIsSMT_Y);
            this._list.Add(MaterialIsSMT_N);
        }
        public static readonly string MaterialIsSMT_Y = "Y";                      //是SMT物料
        public static readonly string MaterialIsSMT_N = "N";                      //不是SMT物料

        #region MaterialIsSMT Members

        public string Group
        {
            get { return "MaterialIsSMT"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    public class NeedVendor : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public NeedVendor()
        {
            this._list.Add(NeedVendor_Y);
            this._list.Add(NeedVendor_N);
        }
        public static readonly string NeedVendor_Y = "1";                      //必须检查供应商
        public static readonly string NeedVendor_N = "0";                      //不检查供应商

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "NeedVendor"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region MaterialLot

    public class FIFOFlag : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public FIFOFlag()
        {
            this._list.Add(FIFOFlag_Y);
            this._list.Add(FIFOFlag_N);
        }
        public static readonly string FIFOFlag_Y = "Y";
        public static readonly string FIFOFlag_N = "N";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "FIFOFlag"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region ItemCheckType

    public class ItemCheckType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public ItemCheckType()
        {
            this._list.Add(ItemCheckType_SERIAL);
            this._list.Add(ItemCheckType_ID1);
            this._list.Add(ItemCheckType_ID2);
            this._list.Add(ItemCheckType_ID3);
        }
        public static readonly string ItemCheckType_SERIAL = "SERIAL";
        public static readonly string ItemCheckType_ID1 = "ID1";
        public static readonly string ItemCheckType_ID2 = "ID2";
        public static readonly string ItemCheckType_ID3 = "ID3";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "ItemCheckType"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region WorkPlanActionStatus

    public class WorkPlanActionStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public WorkPlanActionStatus()
        {
            this._list.Add(WorkPlanActionStatus_Init);
            this._list.Add(WorkPlanActionStatus_Ready);
            this._list.Add(WorkPlanActionStatus_Open);
            this._list.Add(WorkPlanActionStatus_Close);
        }

        public static readonly string WorkPlanActionStatus_Init = "workplanactionstatus_init";      //待齐套确认
        public static readonly string WorkPlanActionStatus_Ready = "workplanactionstatus_ready";    //待投产
        public static readonly string WorkPlanActionStatus_Open = "workplanactionstatus_open";      //生产中
        public static readonly string WorkPlanActionStatus_Close = "workplanactionstatus_close";    //结束

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "WorkPlanActionStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region MaterialWarningStatus

    public class MaterialWarningStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public MaterialWarningStatus()
        {
            this._list.Add(MaterialWarningStatus_No);
            this._list.Add(MaterialWarningStatus_Delivery);
            this._list.Add(MaterialWarningStatus_Responsed);
            this._list.Add(MaterialWarningStatus_Lack);
        }

        public static readonly string MaterialWarningStatus_No = "materialwarningstatus_no";                //正常
        public static readonly string MaterialWarningStatus_Delivery = "materialwarningstatus_delivery";    //待配料
        public static readonly string MaterialWarningStatus_Responsed = "materialwarningstatus_responsed";  //已响应
        public static readonly string MaterialWarningStatus_Lack = "materialwarningstatus_lack";            //缺料

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "MaterialWarningStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region MaterialIssueType

    public class MaterialIssueType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public MaterialIssueType()
        {
            this._list.Add(MaterialIssueType_Issue);
            this._list.Add(MaterialIssueType_Receive);
            this._list.Add(MaterialIssueType_LineTransferOut);
            this._list.Add(MaterialIssueType_LineTransferIn);
        }

        public static readonly string MaterialIssueType_Issue = "materialissuetype_issue";                      //齐套区发料
        public static readonly string MaterialIssueType_Receive = "materialissuetype_receive";                  //产线收料
        public static readonly string MaterialIssueType_LineTransferOut = "materialissuetype_linetransferout";  //产线物料移出
        public static readonly string MaterialIssueType_LineTransferIn = "materialissuetype_linetransferin";    //产线物料转入

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "MaterialIssueType"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region IssueType
    public class IssueType : IInternalSystemVariable
    {

        private ArrayList _list = new ArrayList();
        public IssueType()
        {
            this._list.Add(IssueType_Issue);
            this._list.Add(IssueType_Receive);
        }

        public static readonly string IssueType_Issue = "issue";             //发料
        public static readonly string IssueType_Receive = "receive";      //收料

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "IssueType"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }
    #endregion

    #region MaterialIssueStatus

    public class MaterialIssueStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public MaterialIssueStatus()
        {
            this._list.Add(MaterialIssueStatus_Delivered);
            this._list.Add(MaterialIssueStatus_Close);
        }

        public static readonly string MaterialIssueStatus_Delivered = "materialissuestatus_delivered";          //配送
        public static readonly string MaterialIssueStatus_Close = "materialissuestatus_close";                  //完成

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "MaterialIssueStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region MaterialReqStatus

    public class MaterialReqStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public MaterialReqStatus()
        {
            this._list.Add(MaterialReqStatus_Requesting);
            this._list.Add(MaterialReqStatus_Responsed);
        }

        public static readonly string MaterialReqStatus_Requesting = "materialreqstatus_requesting";          //请求中
        public static readonly string MaterialReqStatus_Responsed = "materialreqstatus_responsed";            //已处理

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "MaterialReqStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region MaterialReqType

    public class MaterialReqType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public MaterialReqType()
        {
            this._list.Add(MaterialReqType_Delivery);
            this._list.Add(MaterialReqType_Lack);
        }

        public static readonly string MaterialReqType_Delivery = "materialreqtype_delivery";          //配料
        public static readonly string MaterialReqType_Lack = "materialreqtype_lack";                  //缺料

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "MaterialReqType"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region AlertType

    public class AlertType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public AlertType()
        {
            this._list.Add(AlertType_Error);
            this._list.Add(AlertType_ErrorCode);
            this._list.Add(AlertType_DirectPass);
            this._list.Add(AlertType_OQCNG);
            this._list.Add(AlertType_LinePause);
            this._list.Add(AlertType_OQCReject);
            this._list.Add(AlertType_IQCNG);
        }

        public const string AlertType_Error = "alerttype_error";
        public const string AlertType_ErrorCode = "alerttype_errorcode";
        public const string AlertType_DirectPass = "alerttype_directpass";
        public const string AlertType_OQCNG = "alerttype_oqcng";
        public const string AlertType_LinePause = "alerttype_linepause";
        public const string AlertType_OQCReject = "alerttype_oqcreject";
        public const string AlertType_IQCNG = "alerttype_iqcng";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "AlertType"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region AlertTimeDimension

    public class AlertTimeDimension : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public AlertTimeDimension()
        {
            this._list.Add(AlertTimeDimension_ByShiftCode);
            this._list.Add(AlertTimeDimension_ByShiftDay);
        }

        public static readonly string AlertTimeDimension_ByShiftCode = "byshiftcode";
        public static readonly string AlertTimeDimension_ByShiftDay = "byshiftday";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "AlertTimeDimension"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region AlertNoticeStatus

    public class AlertNoticeStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public AlertNoticeStatus()
        {
            this._list.Add(AlertNoticeStatus_HasDeal);
            this._list.Add(AlertNoticeStatus_NotDeal);
        }

        public static readonly string AlertNoticeStatus_HasDeal = "dealstatus_hasdeal";
        public static readonly string AlertNoticeStatus_NotDeal = "dealstatus_notdeal";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "AlertNoticeStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region MailSendResult

    public class MailSendResult : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public MailSendResult()
        {
            this._list.Add(MailSendResult_NotSend);
            this._list.Add(MailSendResult_OK);
            this._list.Add(MailSendResult_Fail);
        }

        public static readonly string MailSendResult_NotSend = " ";
        public static readonly string MailSendResult_OK = "ok";
        public static readonly string MailSendResult_Fail = "fail";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "MailSendResult"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region Line2ManDetailStatus

    public class Line2ManDetailStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public Line2ManDetailStatus()
        {
            this._list.Add(Line2ManDetailStatus_On);
            this._list.Add(Line2ManDetailStatus_AutoOn);
            this._list.Add(Line2ManDetailStatus_Pause);
            this._list.Add(Line2ManDetailStatus_Off);
            this._list.Add(Line2ManDetailStatus_AutoOff);
        }

        public static readonly string Line2ManDetailStatus_On = "on";           //上岗
        public static readonly string Line2ManDetailStatus_AutoOn = "autoon";   //自动上岗
        public static readonly string Line2ManDetailStatus_Pause = "pause";     //暂停
        public static readonly string Line2ManDetailStatus_Off = "off";         //离岗
        public static readonly string Line2ManDetailStatus_AutoOff = "autooff"; //自动离岗

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "Line2ManDetailStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region ProduceDetailStatus

    public class ProduceDetailStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public ProduceDetailStatus()
        {
            this._list.Add(ProduceDetailStatus_Open);
            this._list.Add(ProduceDetailStatus_Close);
        }

        public static readonly string ProduceDetailStatus_Open = "open";
        public static readonly string ProduceDetailStatus_Close = "close";  

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "ProduceDetailStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region ExceptionFlag

    public class ExceptionFlag : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public ExceptionFlag()
        {
            this._list.Add(ExceptionFlag_Y);
            this._list.Add(ExceptionFlag_N);
        }

        public static readonly string ExceptionFlag_Y = "Y";   //非生产性损失
        public static readonly string ExceptionFlag_N = "N";   //生产性损失

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "ExceptionFlag"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region AlertJob

    public class AlertJob : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public AlertJob()
        {
            this._list.Add(alertJob);
        }

        public static readonly string alertJob = "AlertJob";   //AlertJob

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "AlertJob"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region InputOutputType

    public class InputOutputType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public InputOutputType()
        {
            this._list.Add(InputOutputType_Input);
            this._list.Add(InputOutputType_Output);
        }

        public const string InputOutputType_Input = "input";
        public const string InputOutputType_Output = "output";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "InputOutputType"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    #endregion

    #region MaintenanceType
    public class MaintenanceType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public MaintenanceType()
        {
            this._list.Add(MaintenanceType.MaintenanceType_UsingType);
            this._list.Add(MaintenanceType.MaintenanceType_DayType);
        }
        public const string MaintenanceType_UsingType = "USINGTYPE";//使用保养
        public const string MaintenanceType_DayType = "DAYTYPE";//日常保养

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "MaintenanceType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion


    #region MSDStatus
    public class MSDStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public MSDStatus()
        {
            this._list.Add(MSDStatus.MSDStatus_MSD_PACKAGE);
            this._list.Add(MSDStatus.MSDStatus_MSD_OPENED);
            //this._list.Add(MSDStatus.MSDStatus_MSD_USING);
            this._list.Add(MSDStatus.MSDStatus_MSD_DRYING);
            this._list.Add(MSDStatus.MSDStatus_MSD_BAKING);
            this._list.Add(MSDStatus.MSDStatus_MSD_OVERTIME);
            this._list.Add(MSDStatus.MSDStatus_MSD_ALLUSED);

        }
            public const string MSDStatus_MSD_PACKAGE = "MSD_PACKAGE";		//封装
            public const string MSDStatus_MSD_OPENED = "MSD_OPENED";		//拆封
            //public const string MSDStatus_MSD_USING = "MSD_USING";			//使用
            public const string MSDStatus_MSD_DRYING = "MSD_DRYING";		//干燥箱干燥
            public const string MSDStatus_MSD_BAKING = "MSD_BAKING";		//烘烤箱烘烤
            public const string MSDStatus_MSD_OVERTIME = "MSD_OVERTIME";	//超时
            public const string MSDStatus_MSD_ALLUSED = "MSD_ALLUSED";		//全部使用

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "MSDStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region INVReceiptType
    public class INVReceiptType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public INVReceiptType()
        {
            this._list.Add(INVReceiptType.INVReceiptType_P);
            this._list.Add(INVReceiptType.INVReceiptType_WX);
            this._list.Add(INVReceiptType.INVReceiptType_O);
        }
        public const string INVReceiptType_P = "P";//外购
        public const string INVReceiptType_WX = "WX";//外协
        public const string INVReceiptType_O = "O";//其他

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "INVReceiptType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    //add by Jarvis
    #region CreateType

    public class CreateType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public CreateType()
        {
            this._list.Add(CreateType.CREATETYPE_DECIMAL);
            this._list.Add(CreateType.CREATETYPE_HEXADECIMAL);
            this._list.Add(CreateType.CREATETYPE_THIRTYFOUR);
        }
        public const string CREATETYPE_DECIMAL = "10";		    //10进制
        public const string CREATETYPE_HEXADECIMAL = "16";		//16进制
        public const string CREATETYPE_THIRTYFOUR = "34";      //34进制

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "CreateType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    #region PicType

    public class PicType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public PicType()
        {
            this._list.Add(PicType.Operating_Instructions);
            this._list.Add(PicType.Maintenance_instructions);            
        }
        public const string Operating_Instructions = "Operating_Instructions";		    //操作说明  
        public const string Maintenance_instructions = "Maintenance_instructions";		//维护说明   

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "PicType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    //add by Jarvis
    #region LotStatusForMO2LotLink

    public class LotStatusForMO2LotLink : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public LotStatusForMO2LotLink()
        {
            this._list.Add(LotStatusForMO2LotLink.LOTSTATUS_NEW);
            this._list.Add(LotStatusForMO2LotLink.LOTSTATUS_USE);
            this._list.Add(LotStatusForMO2LotLink.LOTSTATUS_STOP);
        }
        public const string LOTSTATUS_NEW = "NEW";    //NEW
        public const string LOTSTATUS_USE = "USE";		//USE
        public const string LOTSTATUS_STOP = "STOP";  //STOP

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "LotStatusForMO2LotLink";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    #endregion

    //add by Jarvis
    #region CollectStatus
    public class CollectStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public CollectStatus()
        {
            this._list.Add(CollectStatus.CollectStatus_BEGIN);
            this._list.Add(CollectStatus.CollectStatus_END);
        }
        public const string CollectStatus_BEGIN = "COLLECT_BEGIN";//开始采集
        public const string CollectStatus_END = "COLLECT_END";//结束采集

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "CollectStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    //jack 2012-03-31 add 
    #region ProductionStatus
    public class ProductionStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public ProductionStatus()
        {
            this._list.Add(ProductionStatus.ProductionStatus_InProduction);
            this._list.Add(ProductionStatus.ProductionStatus_CloseProduction);
            this._list.Add(ProductionStatus.ProductionStatus_NoProduction);
        }
        public const string ProductionStatus_InProduction = "INPRODUCTION";//
        public const string ProductionStatus_NoProduction = "NOPRODUCTION";//
        public const string ProductionStatus_CloseProduction = "CLOSEPRODUCTION";//

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ProductionStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    //add by Jarvis
    #region CollectStatus
    public class OperationType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public OperationType()
        {
            this._list.Add(OperationType.OPERATIONTYPE_MERGE);
            this._list.Add(OperationType.OPERATIONTYPE_SPLIT);
        }
        public const string OPERATIONTYPE_MERGE = "MERGE";//开始采集
        public const string OPERATIONTYPE_SPLIT = "SPLIT";//结束采集

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "OperationType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion


    //add by Klaus
    #region EqpMaintType
    public class EqpMaintType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public EqpMaintType()
        {
            this._list.Add(EqpMaintType.EqpMaintType_USINGTYPE);
            this._list.Add(EqpMaintType.EqpMaintType_DAYTYPE);
        }
        public const string EqpMaintType_USINGTYPE = "USINGTYPE";//使用保养
        public const string EqpMaintType_DAYTYPE = "DAYTYPE";//日常保养

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "EqpMaintType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    public class InvReceiptDetailStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        public InvReceiptDetailStatus()
        {
            this._list.Add(InvReceiptDetailStatus_WaitCheck);
            this._list.Add(InvReceiptDetailStatus_AllowStorage);
            this._list.Add(InvReceiptDetailStatus_StorageOK);
        }
        public static readonly string InvReceiptDetailStatus_WaitCheck = "WaitCheck";
        public static readonly string InvReceiptDetailStatus_AllowStorage = "AllowStorage";
        public static readonly string InvReceiptDetailStatus_StorageOK = "StorageOK";

        #region IInternalSystemVariable Members

        public string Group
        {
            get { return "InvReceiptDetailStatus"; }
        }

        public ArrayList Items
        {
            get { return this._list; }
        }

        #endregion
    }

    //add by Sandy on20140529
    #region BurnType
    public class BurnType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public BurnType()
        {
            this._list.Add(BurnType.BurnIn);
            this._list.Add(BurnType.BurnOut);
        }
        public const string BurnIn = "BurnIn";//老化进
        public const string BurnOut = "BurnOut";//老化出

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "BurnType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region DisToLineStatus Add By Berney @20140827
    public class DisToLineStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public DisToLineStatus()
        {
            this._list.Add(DisToLineStatus.NormalDis);
            this._list.Add(DisToLineStatus.WaitDis);
            this._list.Add(DisToLineStatus.ERDis);
            this._list.Add(DisToLineStatus.ShortDis);
            this._list.Add(DisToLineStatus.Finished);
        }
        public const string NormalDis = "Normal";//正常配送
        public const string WaitDis = "WaitDis";//待配送
        public const string ERDis = "ERDis";//紧急配送
        public const string ShortDis = "ShortDis";//缺料中
        public const string Finished = "Finish";//配送完成

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "DisToLineStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    public class DisHeadStatus : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public DisHeadStatus()
        {
            this._list.Add(DisHeadStatus.Dis_Initial);
            this._list.Add(DisHeadStatus.Dis_Distributing);
            this._list.Add(DisHeadStatus.Dis_Pending);
            this._list.Add(DisHeadStatus.Dis_Finish);
        }
        public const string Dis_Initial = "Initial";//待配送
        public const string Dis_Distributing = "Distributing";//配送中
        public const string Dis_Pending = "Pending";//暂停
        public const string Dis_Finish = "Finish";//配送完成

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "DisHeadStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #region WareHouse
    public class SAP_ImportType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public SAP_ImportType()
        {
            this._list.Add(SAP_PGIR);  
            this._list.Add(SAP_PGIR);  
            this._list.Add(SAP_POR); 
            this._list.Add(SAP_YFR); 
            this._list.Add(SAP_PD);  
            this._list.Add(SAP_DNR);  
            this._list.Add(SAP_UB);  
            this._list.Add(SAP_JCR);  
            this._list.Add(SAP_BLR);  
            this._list.Add(SAP_CAR);  
            
        }
        public const string SAP_PGIR = "PGIR";  //PGI退料
        public const string SAP_SCTR = "SCTR";  //生产退料
        public const string SAP_POR = "POR";     //PO入库
        public const string SAP_YFR = "YFR";     //研发入库
        public const string SAP_PD = "PD";       //盘点
        public const string SAP_DNR = "DNR";   //退货入库
        public const string SAP_UB = "UB";       //调拨
        public const string SAP_JCR = "JCR";    //检测返工入库
        public const string SAP_BLR = "BLR";    //不良品入库
        public const string SAP_CAR = "CAR";  //CLAIM入库

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "SAP_ImportType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    /// <summary>
    /// 拣货任务令ASN头状态
    /// </summary>
    public class ASN_STATUS : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        //ASN状态:Release:初始化；WaitReceive:待收货；Receive:初检；ReceiveRejection:初检拒收；IQC:IQC；
        //IQCRejection:IQC拒收；OnLocation:上架；Close:入库；CutOff关单；
        public ASN_STATUS()
        {
            this._list.Add(ASN_Release);
            this._list.Add(ASN_WaitReceive);
            this._list.Add(ASN_Receive);
            this._list.Add(ASN_ReceiveRejection);
            this._list.Add(ASN_IQC);
            this._list.Add(ASN_IQCRejection);
            this._list.Add(ASN_OnLocation);
            this._list.Add(ASN_Close);
            this._list.Add(ASN_Cancel);
            this._list.Add(ASN_CutOff);

        }
        public const string ASN_Release = "Release";   //初始化
        public const string ASN_WaitReceive = "WaitReceive";   //待收货
        public const string ASN_Receive = "Receive";   //初检完成
        public const string ASN_ReceiveRejection = "ReceiveRejection";   //初检完成
        public const string ASN_IQC = "IQC";   //IQC完成
        public const string ASN_IQCRejection = "IQCRejection";   //IQC完成
        public const string ASN_OnLocation = "OnLocation";  //上架
        public const string ASN_Close = "Close";   //入库
        public const string ASN_Cancel = "Cancel";  //取消
        public const string ASN_CutOff = "CutOff";  //取消
        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ASN_STATUS";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    /// <summary>
    /// 拣货任务令ASN行状态
    /// </summary>
    public class ASNDetail_STATUS : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();
        //ASN行状态:Release:初始化；WaitReceive:待收货；ReceiveClose:初检完成；
        //IQCClose:IQC完成；OnLocation:上架；Close:入库；Cancel:取消
        public ASNDetail_STATUS()
        {
            this._list.Add(ASNDetail_Release);
            this._list.Add(ASNDetail_WaitReceive);
            this._list.Add(ASNDetail_ReceiveClose);
            this._list.Add(ASNDetail_IQCClose);
            this._list.Add(ASNDetail_IQCRejection);
            this._list.Add(ASNDetail_OnLocation);
            this._list.Add(ASNDetail_Close);
            this._list.Add(ASNDetail_Cancel);
            //this._list.Add(SAP_ReceiveRejection); 

        }
        public const string ASNDetail_Release = "Release";   //初始化
        public const string ASNDetail_WaitReceive = "WaitReceive";   //待收货
        public const string ASNDetail_ReceiveClose = "ReceiveClose";   //初检完成
        public const string ASNDetail_IQCClose = "IQCClose";   //IQC完成
        public const string ASNDetail_IQCRejection = "IQCRejection";   //IQC完成
        public const string ASNDetail_OnLocation = "OnLocation";  //上架
        public const string ASNDetail_Close = "Close";   //入库
        public const string ASNDetail_Cancel = "Cancel";  //取消
        //public const string SAP_ReceiveRejection = "ReceiveRejection";  //初检拒收
        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "ASNDetail_STATUS";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    /// <summary>
    /// 管控类型
    /// </summary>
    public class SAP_CONTROLTYPE : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public SAP_CONTROLTYPE()
        {
            this._list.Add(SAP_ITEM_CONTROL_KEYPARTS);   //单件管控
            this._list.Add(SAP_ITEM_CONTROL_LOT);  //批管控
            this._list.Add(SAP_ITEM_CONTROL_NOCONTROL);  //不管控
         

        }
        public const string SAP_ITEM_CONTROL_KEYPARTS = "item_control_keyparts";
        public const string SAP_ITEM_CONTROL_LOT = "item_control_lot";
        public const string SAP_ITEM_CONTROL_NOCONTROL = "item_control_nocontrol";
      

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "SAP_CONTROLTYPE";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    /// <summary>
    /// SAP单据号的状态
    /// </summary>
    public class SAP_STATUS : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public SAP_STATUS()
        {
            this._list.Add(SAP_Release);  
            this._list.Add(SAP_Pedding); 
            this._list.Add(SAP_Cancel); 


        }
        public const string SAP_Release = "Release";
        public const string SAP_Pedding = "Pedding";
        public const string SAP_Cancel = "Cancel";


        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "SAP_STATUS";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    public class SAP_LineStatus: IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public SAP_LineStatus()
        {
            this._list.Add(SAP_LINE_RECEIVE);   //接收
            this._list.Add(SAP_LINE_REJECT);  //拒收
            this._list.Add(SAP_LINE_GIVEIN);  //让步接收


        }
        public const string SAP_LINE_RECEIVE = "Receive";
        public const string SAP_LINE_REJECT = "Reject";
        public const string SAP_LINE_GIVEIN = "Givein";


        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "SAP_LineStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    public class StorageTrans_STATUS : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public StorageTrans_STATUS()
        {
            this._list.Add(Trans_Release);
            this._list.Add(Trans_Pick);
            this._list.Add(Trans_Close);

        }
        public const string Trans_Release = "Release";   //初始化
        public const string Trans_Pick = "Pick";   //拣料
        public const string Trans_Close = "Close";   //完成



        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "StorageTrans_STATUS";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    /// <summary>
    /// 拣货任务令行状态
    /// </summary>
    public class PickDetail_STATUS : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public PickDetail_STATUS()
        {
            //状态:Release:初始化，WaitPick:待拣料，Pick:拣料中，ClosePick:拣料完成，
            //Pack:包装中，ClosePack:包装完成，Cancel:取消；Owe:欠料
            this._list.Add(Status_Release);
            this._list.Add(Status_WaitPick);
            this._list.Add(Status_Pick);
            this._list.Add(Status_ClosePick);
            this._list.Add(Status_Pack);
            this._list.Add(Status_ClosePack);
            this._list.Add(Status_Cancel);
            this._list.Add(Status_Owe);

        }
        public const string Status_Release = "Release";   //初始化
        public const string Status_WaitPick = "WaitPick";   //待拣料
        public const string Status_Pick = "Pick";   //拣料中
        public const string Status_ClosePick = "ClosePick";   //拣料完成
        public const string Status_Pack = "Pack";   //包装中
        public const string Status_ClosePack = "ClosePack";   //包装完成
        public const string Status_Cancel = "Cancel";   //取消
        public const string Status_Owe = "Owe";   //欠料

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "PickDetail_STATUS";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    /// <summary>
    /// 拣货任务令头状态
    /// </summary>
    public class Pick_STATUS : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public Pick_STATUS()
        {
            //状态:Release:初始化，WaitPick:待拣料，Pick:拣料，MakePackingList:制作箱单，
            //Pack:包装，OQC:OQC检验，ClosePackingList:箱单完成，Close:已出库，
            //Cancel:取消，Block:冻结

            this._list.Add(Status_Release);
            this._list.Add(Status_WaitPick);
            this._list.Add(Status_Pick);
            this._list.Add(Status_MakePackingList);
            this._list.Add(Status_Pack);
            this._list.Add(Status_OQC);
            this._list.Add(Status_ClosePackingList);
            this._list.Add(Status_Close);
            this._list.Add(Status_Cancel);
            this._list.Add(Status_Block);

        }
        public const string Status_Release = "Release"; 
        public const string Status_WaitPick = "WaitPick";  
        public const string Status_Pick = "Pick";
        public const string Status_MakePackingList = "MakePackingList";  
        public const string Status_Pack = "Pack";
        public const string Status_OQC = "OQC";
        public const string Status_ClosePackingList = "ClosePackingList";
        public const string Status_Close = "Close";
        public const string Status_Cancel = "Cancel";
        public const string Status_Block = "Block";  

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "Pick_STATUS";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    /// <summary>
    /// 箱单状态
    /// </summary>
    public class CartonInvoices_STATUS : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public CartonInvoices_STATUS()
        {
            //状态:Release:初始化，Pack:包装中，ClosePack:包装完成，
            //OQCClose:OQC检验完成，ClosePackingList:箱单完成，Close:已出库，Cancel:取消，Block:冻结
            this._list.Add(Status_Release);
            //this._list.Add(Status_WaitPick);
            this._list.Add(Status_Close);
            this._list.Add(Status_Block);
            this._list.Add(Status_Pack);
            this._list.Add(Status_ClosePack);
            this._list.Add(Status_Cancel);
            this._list.Add(Status_OQCClose);
            this._list.Add(Status_ClosePackingList);

        }
        public const string Status_Release = "Release";   //初始化
        //public const string Status_WaitPick = "WaitPick";   //待拣料
        public const string Status_Close = "Close";   
        public const string Status_Block = "Block";  
        public const string Status_Pack = "Pack";   
        public const string Status_ClosePack = "ClosePack";  
        public const string Status_Cancel = "Cancel";  
        public const string Status_ClosePackingList = "ClosePackingList";  
        public const string Status_OQCClose = "OQCClose";  

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "CartonInvoices_STATUS";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }

    //add by sam
    #region TransType
    public class TransType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public TransType()
        {
            this._list.Add(TransType.TransType_Transfer);
            this._list.Add(TransType.TransType_Move);
        }
        public const string TransType_Transfer = "Transfer";//转储
        public const string TransType_Move = "Move";//货位移动

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "TransType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    //add by sam
    #region CartonType
    public class CartonType : IInternalSystemVariable
    {
        private ArrayList _list = new ArrayList();

        public CartonType()
        {
            this._list.Add(CartonType.CartonType_SplitCarton);
            this._list.Add(CartonType.CartonType_AllCarton);
        }
        public const string CartonType_SplitCarton = "SplitCarton";//转储
        public const string CartonType_AllCarton = "AllCarton";//货位移动

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "CartonType";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }
    #endregion

    #endregion
}