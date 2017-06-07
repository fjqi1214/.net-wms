using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for Material
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
/// ** 日 期:		2005-05-18 9:29:18
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.Material
{

    #region ERPSRMX
    /// <summary>
    ///  ERP抛转资料表
    /// </summary>
    [Serializable, TableMap("SRMX", "SRMXNO,SONO")]
    public class ERPSRMX : DomainObject
    {
        public ERPSRMX()
        {
        }

        /// <summary>
        /// SR单号
        /// </summary>
        [FieldMapAttribute("SRMXNO", typeof(string), 40, true)]
        public string SRMXNO;

        /// <summary>
        /// 通过SR单号产生的出厂放行单号
        /// </summary>
        [FieldMapAttribute("SRFXD", typeof(string), 40, false)]
        public string SRFXD;

        /// <summary>
        /// 工单代码
        /// </summary>
        [FieldMapAttribute("SONO", typeof(int), true)]
        public int SONO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("UID", typeof(string), 40, true)]
        public string UID;

        /// <summary>
        /// 数量
        /// </summary>
        [FieldMapAttribute("FINQTY", typeof(decimal), 10, true)]
        public decimal FINQTY;

        /// <summary>
        /// 状态
        /// </summary>
        [FieldMapAttribute("STA", typeof(string), 40, true)]
        public string STA;

        /// <summary>
        /// 日期
        /// </summary>
        [FieldMapAttribute("ENDATE", typeof(int), 8, true)]
        public int ENDATE;

        /// <summary>
        /// 产品代码
        /// </summary>
        [FieldMapAttribute("SRPROD", typeof(string), 40, true)]
        public string SRPROD;

        /// <summary>
        /// 产品描述
        /// </summary>
        [FieldMapAttribute("SRDESC", typeof(string), 100, false)]
        public string SRDESC;

    }
    #endregion

    #region ERPINVInterface
    /// <summary>
    ///  ERP抛转资料表
    /// </summary>
    [Serializable, TableMap("TBLERPINVInterface", "RECNO,MOCODE,STATUS,SRNO")]
    public class ERPINVInterface : DomainObject
    {
        public ERPINVInterface()
        {
        }

        /// <summary>
        /// SR单号
        /// </summary>
        [FieldMapAttribute("SRNO", typeof(string), 40, false)]
        public string SRNO;

        /// <summary>
        /// 入库单号
        /// </summary>
        [FieldMapAttribute("RECNO", typeof(string), 40, true)]
        public string RECNO;

        /// <summary>
        /// 工单代码
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCODE;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ITEMCODE;

        /// <summary>
        /// 产品代码
        /// </summary>
        [FieldMapAttribute("QTY", typeof(decimal), 10, true)]
        public decimal QTY;

        /// <summary>
        /// 状态
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string STATUS;

        /// <summary>
        /// 日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MDATE;

        /// <summary>
        /// 时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MTIME;

        /// <summary>
        /// 用户
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MUSER;

        /// <summary>
        /// 上传日期
        /// </summary>
        [FieldMapAttribute("UPLOADDATE", typeof(int), 8, false)]
        public int UPLOADDATE;

        /// <summary>
        /// 上传时间
        /// </summary>
        [FieldMapAttribute("UPLOADTIME", typeof(int), 6, false)]
        public int UPLOADTIME;

        /// <summary>
        /// 上传用户
        /// </summary>
        [FieldMapAttribute("UPLOADUSER", typeof(string), 40, false)]
        public string UPLOADUSER;

        /// <summary>
        /// 关联SR单
        /// </summary>
        [FieldMapAttribute("LINKSRNO", typeof(string), 40, false)]
        public string LINKSRNO;

        /// <summary>
        /// 备注
        /// </summary>
        [FieldMapAttribute("EATTIBUTE1", typeof(string), 100, false)]
        public string EATTIBUTE1;

    }
    #endregion

    #region MaterialStockIn
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLMSTOCKIN", "OID,TKTNO,RCARD")]
    public class MaterialStockIn : DomainObject
    {
        public MaterialStockIn()
        {
        }

        /// <summary>
        /// 新增主键 
        /// </summary>
        //karron qiu ,2005/09/16, 新增主键
        [FieldMapAttribute("OID", typeof(string), 40, true)]
        public string OID = string.Empty;

        /// <summary>
        /// 入库单号
        /// </summary>
        [FieldMapAttribute("TKTNO", typeof(string), 40, true)]
        public string TicketNO;

        /// <summary>
        /// 未使用
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 未使用
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 源单号（未使用）
        /// </summary>
        [FieldMapAttribute("STKTNO", typeof(string), 40, false)]
        public string SourceTicketNO;

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
        /// 产品序列号
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 状态：
        /// INITIAL  初始化
        /// ALREADY 处理完毕
        /// DELETED 已删除
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string Status;

        /// <summary>
        /// 采集号：
        /// Carton号
        /// Pallet号
        /// 二维条码
        /// </summary>
        [FieldMapAttribute("CLTNO", typeof(string), 40, false)]
        public string CollectNo;

        /// <summary>
        /// 采集类型：
        /// 序列号采集
        /// Carton采集
        /// Pallet采集
        /// 二维条码采集
        /// 
        /// </summary>
        [FieldMapAttribute("CLTTYPE", typeof(string), 40, true)]
        public string CollectType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 删除信息
        /// </summary>
        [FieldMapAttribute("DelMemo", typeof(string), 100, false)]
        public string DelMemo;

        /// <summary>
        /// 删除用户
        /// </summary>
        [FieldMapAttribute("DelUser", typeof(string), 40, false)]
        public string DelUser;

        /// <summary>
        /// 删除日期
        /// </summary>
        [FieldMapAttribute("DelDate", typeof(int), 8, false)]
        public int DelDate;

        /// <summary>
        /// 删除时间
        /// </summary>
        [FieldMapAttribute("DelTime", typeof(int), 6, false)]
        public int DelTime;

        /// <summary>
        /// 备注
        /// </summary>
        [FieldMapAttribute("StockMemo", typeof(string), 100, false)]
        public string StockMemo;

    }
    #endregion

    #region MaterialStockOut
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLMSTOCKOUT", "OID,TKTNO,SEQ")]
    public class MaterialStockOut : DomainObject
    {
        public MaterialStockOut()
        {
        }

        /// <summary>
        /// 新增主键 
        /// </summary>
        //karron qiu ,2005/09/16, 新增主键
        [FieldMapAttribute("OID", typeof(string), 40, true)]
        public string OID = string.Empty;

        /// <summary>
        /// 出货单号
        /// </summary>
        [FieldMapAttribute("TKTNO", typeof(string), 40, true)]
        public string TicketNO;

        /// <summary>
        /// 源单号
        /// </summary>
        [FieldMapAttribute("STKTNO", typeof(string), 40, false)]
        public string SourceTicketNO;

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
        /// 状态:
        /// INITIAL  初始化
        /// ALREADY 处理完毕
        /// DELETED 已删除
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string Status;

        /// <summary>
        /// 机种
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 经销商
        /// </summary>
        [FieldMapAttribute("DEALER", typeof(string), 40, true)]
        public string Dealer;

        /// <summary>
        /// 发货日期
        /// 
        /// </summary>
        [FieldMapAttribute("OUTDATE", typeof(int), 8, true)]
        public int OutDate;

        /// <summary>
        /// 项次：代表着机种、经销商、发货日期这三个资料，因此只要其中任何一个栏位信息发生变化，就新增一个项次
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("QTY", typeof(decimal), 10, true)]
        public decimal Quantity;

        /// <summary>
        /// 删除信息
        /// </summary>
        [FieldMapAttribute("DelMemo", typeof(string), 100, false)]
        public string DelMemo;

        /// <summary>
        /// 执行删除的用户
        /// </summary>
        [FieldMapAttribute("DelUser", typeof(string), 40, false)]
        public string DelUser;

        /// <summary>
        /// 删除日期
        /// </summary>
        [FieldMapAttribute("DelDate", typeof(int), 8, false)]
        public int DelDate;

        /// <summary>
        /// 删除时间
        /// </summary>
        [FieldMapAttribute("DelTime", typeof(int), 6, false)]
        public int DelTime;

        /// <summary>
        /// 备注
        /// </summary>
        [FieldMapAttribute("StockMemo", typeof(string), 100, false)]
        public string StockMemo;

    }
    #endregion

    #region MaterialStockOutDetail
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLMSTOCKOUTDETAIL", "RCARD,TKTNO,SEQ")]
    public class MaterialStockOutDetail : DomainObject
    {
        public override string ToString()
        {
            return RunningCard;
        }

        public MaterialStockOutDetail()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 出货单号
        /// </summary>
        [FieldMapAttribute("TKTNO", typeof(string), 40, true)]
        public string TicketNO;

        /// <summary>
        /// 项次：代表着机种、经销商、发货日期这三个资料，因此只要其中任何一个栏位信息发生变化，就新增一个项次
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

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
        /// 
        /// </summary>
        [FieldMapAttribute("CLTNO", typeof(string), 40, false)]
        public string CollectNo;

        /// <summary>
        /// 采集类型：
        /// 序列号采集
        /// Carton采集
        /// Pallet采集
        /// 二维条码采集
        /// </summary>
        [FieldMapAttribute("CLTTYPE", typeof(string), 40, true)]
        public string CollectType;

    }
    #endregion

    #region MINNO
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLMINNO", "INNO,SEQ")]
    public class MINNO : DomainObject
    {
        public MINNO()
        {
        }

        /// <summary>
        /// 料品生产日期
        /// </summary>
        [FieldMapAttribute("DATECODE", typeof(string), 100, false)]
        public string DateCode;

        /// <summary>
        /// 厂商代号
        /// </summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 100, false)]
        public string VendorCode;

        /// <summary>
        /// 厂商料号
        /// </summary>
        [FieldMapAttribute("VENDORITEMCODE", typeof(string), 100, false)]
        public string VendorItemCode;

        /// <summary>
        /// 料品规格
        /// </summary>
        [FieldMapAttribute("VERSION", typeof(string), 100, false)]
        public string Version;

        /// <summary>
        /// BIOS版本
        /// </summary>
        [FieldMapAttribute("BIOS", typeof(string), 100, false)]
        public string BIOS;

        /// <summary>
        /// PCBA版本
        /// </summary>
        [FieldMapAttribute("PCBA", typeof(string), 100, false)]
        public string PCBA;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 集成上料ID
        /// </summary>
        [FieldMapAttribute("INNO", typeof(string), 40, true)]
        public string INNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LotNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 是否试流料: Y, N 
        /// </summary>
        [FieldMapAttribute("ISTRY", typeof(string), 40, true)]
        public string IsTry;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TryItemCode", typeof(string), 40, false)]
        public string TryItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOBSITEMCODE", typeof(string), 40, false)]
        public string MSourceItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("QTY", typeof(decimal), 10, true)]
        public decimal Qty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string RouteCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string ResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPBOMCODE", typeof(string), 40, true)]
        public string OPBOMCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPBOMVER", typeof(string), 40, true)]
        public string OPBOMVersion;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MITEMCODE", typeof(string), 40, false)]
        public string MItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ISLAST", typeof(string), 1, true)]
        public string IsLast;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MItemName", typeof(string), 80, false)]
        public string MItemName;

        /// <summary>
        /// OP BOM Source Item Packed No
        /// </summary>
        [FieldMapAttribute("MITEMPACKEDNO", typeof(string), 40, true)]
        public string MItemPackedNo;
    }
    #endregion

    #region MKeyPart
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLMKEYPART", "SEQ,MITEMCODE")]
    public class MKeyPart : DomainObject
    {
        public MKeyPart()
        {
        }

        /// <summary>
        /// 料品生产日期
        /// </summary>
        [FieldMapAttribute("DATECODE", typeof(string), 100, false)]
        public string DateCode;

        /// <summary>
        /// 厂商代号
        /// </summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 100, false)]
        public string VendorCode;

        /// <summary>
        /// 厂商料号
        /// </summary>
        [FieldMapAttribute("VENDORITEMCODE", typeof(string), 100, false)]
        public string VendorItemCode;

        /// <summary>
        /// 料品规格
        /// </summary>
        [FieldMapAttribute("VERSION", typeof(string), 100, false)]
        public string Version;

        /// <summary>
        /// BIOS版本
        /// </summary>
        [FieldMapAttribute("BIOS", typeof(string), 100, false)]
        public string BIOS;

        /// <summary>
        /// PCBA版本
        /// </summary>
        [FieldMapAttribute("PCBA", typeof(string), 100, false)]
        public string PCBA;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 序号
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(int), -1, true)]
        public int Sequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MITEMCODE", typeof(string), 40, true)]
        public string MItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSTART", typeof(string), 40, true)]
        public string RunningCardStart;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDEND", typeof(string), 40, true)]
        public string RunningCardEnd;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MoCode", typeof(string), 40, false)]
        public string MoCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MITEMNAME", typeof(string), 100, false)]
        public string MITEMNAME;

        /// <summary>
        /// 序列号前缀
        /// </summary>
        [FieldMapAttribute("RCARDPREFIX", typeof(string), 100, false)]
        public string RCardPrefix;

        /// <summary>
        /// 序列号进制
        /// </summary>
        [FieldMapAttribute("SNSCALE", typeof(string), 40, false)]
        public string SNScale;

        ///<summary>
        ///TemplateName
        ///</summary>	
        [FieldMapAttribute("TEMPLATENAME", typeof(string), 40, true)]
        public string TemplateName;

        /// <summary>
        /// Keypart号，与数据库无关
        /// Mark add 20050615
        /// </summary>
        public string Keypart;
    }
    #endregion

    #region MKeyPartDetail

    /// <summary>
    ///	MKeyPartDetail
    /// </summary>
    [Serializable, TableMap("TBLMKEYPARTDETAIL", "MITEMCODE,SERIALNO")]
    public class MKeyPartDetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public MKeyPartDetail()
        {
        }

        ///<summary>
        ///MItemCode
        ///</summary>	
        [FieldMapAttribute("MITEMCODE", typeof(string), 40, false)]
        public string MItemCode;

        ///<summary>
        ///Sequence
        ///</summary>	
        [FieldMapAttribute("SEQ", typeof(int), -1, false)]
        public int Sequence;

        ///<summary>
        ///SerialNo
        ///</summary>	
        [FieldMapAttribute("SERIALNO", typeof(string), 40, false)]
        public string SerialNo;

        ///<summary>
        ///PrintTimes
        ///</summary>	
        [FieldMapAttribute("PRINTTIMES", typeof(int), 10, false)]
        public int PrintTimes;

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

        ///<summary>
        ///EAttribute1
        ///</summary>	
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, false)]
        public string EAttribute1;
    }

    #endregion

    #region InventoryRCard
    [Serializable, TableMap("TBLINVRCARD", "INVID")]
    public class InventoryRCard : DomainObject
    {
        public InventoryRCard()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("INVID", typeof(int), 10, true)]
        public int InventoryID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECID", typeof(int), 10, true)]
        public int RecID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSTATUS", typeof(string), 40, true)]
        public string RCardStatus;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 40, false)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECSEQ", typeof(int), 10, true)]
        public int ReceiveSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPID", typeof(int), 10, true)]
        public int ShipID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPSEQ", typeof(int), 10, true)]
        public int ShipSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;

    }
    #endregion

    #region Receive
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLRECEIVE", "RECID")]
    public class Receive : DomainObject
    {
        public Receive()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECNO", typeof(string), 40, false)]
        public string ReceiveNo;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECSTATUS", typeof(string), 40, false)]
        public string ReceiveStatus;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("QTY", typeof(decimal), 10, true)]
        public decimal Quantity;

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
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECID", typeof(int), 10, true)]
        public int RecID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECDATE", typeof(int), 8, true)]
        public int ReceiveDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECTIME", typeof(int), 6, true)]
        public int ReceiveTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECUSER", typeof(string), 40, true)]
        public string ReceiveUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DELDATE", typeof(int), 8, true)]
        public int DeleteDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DELTIME", typeof(int), 6, true)]
        public int DeleteTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DELUSER", typeof(string), 40, true)]
        public string DeleteUser;

    }
    #endregion

    #region ReceiveDetail
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLRECEIVEDT", "RECSEQ,RECID")]
    public class ReceiveDetail : DomainObject
    {
        public ReceiveDetail()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECNO", typeof(string), 40, false)]
        public string ReceiveNo;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECSEQ", typeof(int), 10, true)]
        public int ReceiveSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMDESC", typeof(string), 100, false)]
        public string ItemDesc;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("QTY", typeof(decimal), 10, true)]
        public decimal Quantity;

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
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECID", typeof(int), 10, true)]
        public int RecID;

    }
    #endregion

    #region Ship
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLSHIP", "SHIPID")]
    public class Ship : DomainObject
    {
        public Ship()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPNO", typeof(string), 40, false)]
        public string ShipNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPSTATUS", typeof(string), 40, true)]
        public string ShipStatus;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("QTY", typeof(decimal), 10, true)]
        public decimal Quantity;

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
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPID", typeof(int), 10, true)]
        public int ShipID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPDATE", typeof(int), 8, true)]
        public int ShipDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPTIME", typeof(int), 6, true)]
        public int ShipTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPUSER", typeof(string), 40, true)]
        public string ShipUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DELDATE", typeof(int), 8, true)]
        public int DeleteDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DELTIME", typeof(int), 6, true)]
        public int DeleteTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DELUSER", typeof(string), 40, true)]
        public string DeleteUser;

    }
    #endregion

    #region ShipDetail
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLSHIPDT", "SHIPSEQ,SHIPID")]
    public class ShipDetail : DomainObject
    {
        public ShipDetail()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPNO", typeof(string), 40, false)]
        public string ShipNo;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPSEQ", typeof(int), 10, true)]
        public int ShipSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PARTNERNO", typeof(string), 40, true)]
        public string PartnerNo;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPDATE", typeof(int), 8, true)]
        public int ShipDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPDESC", typeof(string), 100, false)]
        public string Description;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PLANQTY", typeof(decimal), 10, true)]
        public decimal PlanQuantity;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ACTUALQTY", typeof(decimal), 10, true)]
        public decimal ActualQuantity;

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
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPID", typeof(int), 10, true)]
        public int ShipID;

    }
    #endregion

    #region InvReceive
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLINVREC", "RECNO,RECSEQ")]
    public class InvReceive : DomainObject
    {
        public InvReceive()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECNO", typeof(string), 40, true)]
        public string RecNo;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECTYPE", typeof(string), 40, true)]
        public string RecType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("INNERTYPE", typeof(string), 40, true)]
        public string InnerType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECSEQ", typeof(int), 10, true)]
        public int RecSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RecStatus", typeof(string), 40, true)]
        public string RecStatus;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECDESC", typeof(string), 100, false)]
        public string Description;

        /// <summary>
        /// 工单
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MoCode;
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMDESC", typeof(string), 100, false)]
        public string ItemDesc;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PLANQTY", typeof(int), 10, true)]
        public int PlanQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ACTQTY", typeof(int), 10, true)]
        public int ActQty;

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
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECDATE", typeof(int), 8, true)]
        public int ReceiveDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECTIME", typeof(int), 6, true)]
        public int ReceiveTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECUSER", typeof(string), 40, true)]
        public string ReceiveUser;

    }
    #endregion

    #region InvRCard
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLINVRCARD", "RCARD,RECNO")]
    public class InvRCard : DomainObject
    {
        public InvRCard()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECNO", typeof(string), 10, true)]
        public string RecNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSTATUS", typeof(string), 40, true)]
        public string RCardStatus;

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
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECSEQ", typeof(decimal), 10, false)]
        public decimal RecSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPNO", typeof(string), 40, true)]
        public string ShipNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPSEQ", typeof(decimal), 10, true)]
        public decimal ShipSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECDATE", typeof(int), 8, true)]
        public int ReceiveDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECTIME", typeof(int), 6, true)]
        public int ReceiveTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECUSER", typeof(string), 40, true)]
        public string ReceiveUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPDATE", typeof(int), 8, true)]
        public int ShipDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPTIME", typeof(int), 6, true)]
        public int ShipTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPUSER", typeof(string), 40, true)]
        public string ShipUser;

        //入库时使用的采集方式
        [FieldMapAttribute("RECCOLLECTTYPE", typeof(string), 40, true)]
        public string RecCollectType;

        //出货时使用的采集方式
        [FieldMapAttribute("SHIPCOLLECTTYPE", typeof(string), 40, true)]
        public string ShipCollectType;

        /// <summary>
        /// 订单
        /// </summary>
        [FieldMapAttribute("ORDERNUMBER", typeof(string), 40, false)]
        public string OrderNumber;

        [FieldMapAttribute("CartonCode", typeof(string), 40, true)]
        public string CartonCode = string.Empty;

        /// <summary>
        /// 正常库存品,异常库存品
        /// </summary>
        [FieldMapAttribute("INVRardType", typeof(string), 40, true)]
        public string INVRardType = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;
    }
    #endregion

    #region InvShip
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLINVSHIP", "SHIPNO,SHIPSEQ")]
    public class InvShip : DomainObject
    {
        public InvShip()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPNO", typeof(string), 40, true)]
        public string ShipNo;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPSEQ", typeof(int), 10, true)]
        public int ShipSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPINNERTYPE", typeof(string), 40, false)]
        public string ShipInnerType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPTYPE", typeof(string), 40, true)]
        public string ShipType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PARTNERCODE", typeof(string), 40, false)]
        public string PartnerCode;

        [FieldMapAttribute("PARTNERDESC", typeof(string), 100, false)]
        public string PartnerDesc;
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPDATE", typeof(int), 8, true)]
        public int ShipDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPTIME", typeof(int), 6, true)]
        public int ShipTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPUSER", typeof(string), 40, false)]
        public string ShipUser;

        /// <summary>
        /// 客户订单号
        /// </summary>
        [FieldMapAttribute("CustomerOrderNo", typeof(string), 40, false)]
        public string CustomerOrderNo;

        /// <summary>
        /// 运货方式
        /// </summary>
        [FieldMapAttribute("ShipMethod", typeof(string), 40, false)]
        public string ShipMethod;

        /// <summary>
        /// 返工工单
        /// </summary>
        [FieldMapAttribute("MoCode", typeof(string), 40, false)]
        public string MoCode;
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        [FieldMapAttribute("ITEMDESC", typeof(string), 100, true)]
        public string ItemDesc;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPSTATUS", typeof(string), 40, true)]
        public string ShipStatus;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PLANQTY", typeof(int), 10, true)]
        public int PlanQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ACTQTY", typeof(int), 10, true)]
        public int ActQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPDESC", typeof(string), 100, false)]
        public string ShipDesc;

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
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MainitainUser;

        [FieldMapAttribute("PRINTDATE", typeof(int), 8, true)]
        public int PrintDate;

    }
    #endregion

    #region ReceiveRCard
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("VRECRCARD", "RECNO,RECSEQ")]
    public class ReceiveRCard : DomainObject
    {
        public ReceiveRCard()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECNO", typeof(string), 40, true)]
        public string RecNo;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECTYPE", typeof(string), 40, true)]
        public string RecType;


        [FieldMapAttribute("RECSEQ", typeof(int), 10, true)]
        public int RecSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RecStatus", typeof(string), 40, true)]
        public string RecStatus;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RECDESC", typeof(string), 100, false)]
        public string Description;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMDESC", typeof(string), 100, false)]
        public string ItemDesc;

        [FieldMapAttribute("ACTQTY", typeof(int), 10, true)]
        public int ActQty;

        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        [FieldMapAttribute("RCARDSTATUS", typeof(string), 40, true)]
        public string RCardStatus;


        [FieldMapAttribute("RECDATE", typeof(int), 8, true)]
        public int ReceiveDate;


        [FieldMapAttribute("RECUSER", typeof(string), 40, true)]
        public string ReceiveUser;

        [FieldMapAttribute("CartonCode", typeof(string), 40, true)]
        public string CartonCode;
    }
    #endregion

    #region ShipRCard
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("VSHIPRCARD", "SHIPNO,SHIPSEQ")]
    public class ShipRCard : DomainObject
    {
        public ShipRCard()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPNO", typeof(string), 40, true)]
        public string ShipNo;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPSEQ", typeof(int), 10, true)]
        public int ShipSeq;


        [FieldMapAttribute("SHIPTYPE", typeof(string), 40, true)]
        public string ShipType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PARTNERCODE", typeof(string), 40, false)]
        public string PartnerCode;

        [FieldMapAttribute("PARTNERDESC", typeof(string), 40, false)]
        public string PartnerDesc;

        [FieldMapAttribute("SHIPDATE", typeof(int), 8, true)]
        public int ShipDate;


        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        [FieldMapAttribute("ITEMDESC", typeof(string), 100, true)]
        public string ItemDesc;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPSTATUS", typeof(string), 40, true)]
        public string ShipStatus;


        [FieldMapAttribute("ACTQTY", typeof(int), 10, true)]
        public int ActQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHIPDESC", typeof(string), 100, false)]
        public string ShipDesc;

        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;


        [FieldMapAttribute("RCARDSTATUS", typeof(string), 40, true)]
        public string RCardStatus;


        [FieldMapAttribute("RECNO", typeof(string), 10, true)]
        public string RecNO;


        [FieldMapAttribute("RCARDSHIPDATE", typeof(int), 8, true)]
        public int RCardShipDate;

        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSHIPUSER", typeof(string), 40, true)]
        public string RCardShipUser;

        [FieldMapAttribute("CartonCode", typeof(string), 40, true)]
        public string CartonCode;

    }
    #endregion

    #region RawReceive4SAP

    [Serializable, TableMap("TBLRAWRECEIVE2SAP", "PONO,POSTSEQ")]
    public class RawReceive4SAP : DomainObject
    {
        [FieldMapAttribute("PONO", typeof(string), 40, false)]
        public string PONo;

        [FieldMapAttribute("POSTSEQ", typeof(decimal), 10, false)]
        public decimal PostSequence;


        [FieldMapAttribute("FLAG", typeof(string), 10, false)]
        public string Flag;

        [FieldMapAttribute("ERRORMESSAGE", typeof(string), 2000, true)]
        public string ErrorMessage;

        [FieldMapAttribute("MATDOCUMENTYEAR", typeof(string), 10, true)]
        public string MaterialDocumentYear;

        [FieldMapAttribute("MATERIALDOCUMENT", typeof(string), 40, true)]
        public string MaterialDocument;

        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        [FieldMapAttribute("TRANSACTIONCODE", typeof(string), 100, true)]
        public string TransactionCode;
    }

    #endregion

    #region RawIssue4SAP

    [Serializable, TableMap("TBLRAWISSUE2SAP", "")]
    public class RawIssue4SAP : DomainObject
    {
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
        public string VendorCode;

        [FieldMapAttribute("FLAG", typeof(string), 40, false)]
        public string Flag;

        [FieldMapAttribute("ERRORMESSAGE", typeof(string), 100, true)]
        public string ErrorMessage;

        [FieldMapAttribute("MATDOCUMENTYEAR", typeof(string), 40, true)]
        public string MaterialDocumentYear;

        [FieldMapAttribute("MATERIALDOCUMENT", typeof(string), 40, true)]
        public string MaterialDocument;

        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        [FieldMapAttribute("TRANSACTIONCODE", typeof(string), 40, true)]
        public string TransactionCode;
    }

    #endregion

    #region MaterialLot
    [Serializable, TableMap("TBLMaterialLot", "MaterialLot")]
    public class MaterialLot : DomainObject
    {
        [FieldMapAttribute("MATERIALLOT", typeof(string), 40, false)]
        public string MaterialLotNo;

        [FieldMapAttribute("IQCNO", typeof(string), 50, true)]
        public string IQCNo;

        [FieldMapAttribute("STLINE", typeof(int), 6, true)]
        public int STLine;

        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        [FieldMapAttribute("VENDORCODE", typeof(string), 40, false)]
        public string VendorCode;

        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;

        [FieldMapAttribute("StorageID", typeof(string), 30, false)]
        public string StorageID;

        [FieldMapAttribute("UNIT", typeof(string), 40, false)]
        public string Unit;

        [FieldMapAttribute("CreateDate", typeof(int), 8, false)]
        public int CreateDate;

        [FieldMapAttribute("LOTINQTY", typeof(int), 13, false)]
        public int LotInQty;

        [FieldMapAttribute("LOTQTY", typeof(int), 13, false)]
        public int LotQty;

        [FieldMapAttribute("FIFOFlag", typeof(string), 1, false)]
        public string FIFOFlag;

        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;
    }
    #endregion

    #region MaterialTrans
    [Serializable, TableMap("TBLMaterialTrans", "SERIAL")]
    public class MaterialTrans : DomainObject
    {
        [FieldMapAttribute("SERIAL", typeof(int), 10, false)]
        public int Serial;

        [FieldMapAttribute("FRMaterialLot", typeof(string), 40, true)]
        public string FRMaterialLot;

        [FieldMapAttribute("FRMITEMCODE", typeof(string), 40, true)]
        public string FRMITEMCODE;

        [FieldMapAttribute("FRMStorageID", typeof(string), 30, true)]
        public string FRMStorageID;

        [FieldMapAttribute("TOMaterialLot", typeof(string), 40, true)]
        public string TOMaterialLot;

        [FieldMapAttribute("TOITEMCODE", typeof(string), 40, true)]
        public string TOITEMCODE;

        [FieldMapAttribute("TOStorageID", typeof(string), 30, true)]
        public string TOStorageID;

        [FieldMapAttribute("TransQTY", typeof(int), 22, false)]
        public int TransQTY;

        [FieldMapAttribute("Memo", typeof(string), 1000, true)]
        public string Memo;

        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string UNIT;

        [FieldMapAttribute("IssueType", typeof(string), 40, false)]
        public string IssueType;

        [FieldMapAttribute("TRANSACTIONCODE", typeof(string), 100, false)]
        public string TRANSACTIONCODE;

        [FieldMapAttribute("BusinessCode", typeof(string), 40, false)]
        public string BusinessCode;

        [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
        public string VendorCode;

        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;

        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;
    }
    #endregion

    #region SAPMaterialTrans
    [Serializable, TableMap("TBLSAPMaterialTrans", "MaterialLot,PostSeq")]
    public class SAPMaterialTrans : DomainObject
    {
        [FieldMapAttribute("MATERIALLOT", typeof(string), 40, false)]
        public string MaterialLotNo;

        [FieldMapAttribute("POSTSEQ", typeof(int), 8, false)]
        public int PostSeq;

        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;

        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        [FieldMapAttribute("ACCOUNTDATE", typeof(int), 8, true)]
        public int AccountDate;

        [FieldMapAttribute("VOUCHERDATE", typeof(int), 8, true)]
        public int VoucherDate;

        [FieldMapAttribute("FRMSTORAGEID", typeof(string), 4, false)]
        public string FRMStorageID;

        [FieldMapAttribute("TOSTORAGEID", typeof(string), 4, false)]
        public string TOStorageID;

        [FieldMapAttribute("TRANSQTY", typeof(int), 13, false)]
        public int TransQTY;

        [FieldMapAttribute("ReceiveMemo", typeof(string), 1000, true)]
        public string ReceiveMemo;

        [FieldMapAttribute("UNIT", typeof(string), 40, false)]
        public string Unit;

        [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
        public string VendorCode;

        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MoCode;

        [FieldMapAttribute("FLAG", typeof(string), 10, true)]
        public string Flag;

        [FieldMapAttribute("TRANSACTIONCODE", typeof(string), 100, false)]
        public string TransactionCode;

        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        [FieldMapAttribute("TOITEMCODE", typeof(string), 40, true)]
        public string ToItemCode;

        [FieldMapAttribute("SAPCODE", typeof(string), 40, false)]
        public string SAPCode;
    }

    public class SAPMaterialTransDesc : SAPMaterialTrans
    {
        [FieldMapAttribute("MDESC", typeof(string), 100, false)]
        public string MaterialDescription;

        [FieldMapAttribute("ERRORMESSAGE", typeof(string), 2000, false)]
        public string ErrorMessage;
    }
    #endregion

    #region MaterialReturn
    [Serializable, TableMap("TBLMaterialreturn", "MaterialLot,PostSeq")]
    public class MaterialReturn : DomainObject
    {
        [FieldMapAttribute("MATERIALLOT", typeof(string), 40, false)]
        public string MaterialLotNo;

        [FieldMapAttribute("POSTSEQ", typeof(int), 8, false)]
        public int PostSeq;

        [FieldMapAttribute("TRANSQTY", typeof(int), 13, true)]
        public int TransQty;

        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;
    }
    #endregion

    #region MaterialLotWithItemDesc

    public class MaterialLotWithItemDesc : MaterialLot
    {
        [FieldMapAttribute("ITEMDESC", typeof(string), 100, true)]
        public string ItemDesc;

        [FieldMapAttribute("VendorDesc", typeof(string), 100, true)]
        public string VendorDesc;
    }
    #endregion

    #region MaterialReqStd
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLMaterialReqStd", "ITEMCODE,ORGID")]
    public class MaterialReqStd : DomainObject
    {
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RequestQTY", typeof(int), 13, true)]
        public int RequestQTY;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        [FieldMapAttribute("EAttribute1", typeof(string), 100, true)]
        public string EAttribute1;

    }

    #endregion

    #region MaterialReqStdWithItemDesc

    public class MaterialReqStdWithItemDesc : MaterialReqStd
    {
        [FieldMapAttribute("ITEMDESC", typeof(string), 100, true)]
        public string ItemDesc;
    }
    #endregion

    #region ExpMaterialReqStd
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLMaterialReqStd", "ITEMCODE")]
    public class ExpMaterialReqStd : DomainObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        public ExpMaterialReqStd()
        {

        }
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(string), 40, false)]
        public string OrganizationID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REQUESTQTY", typeof(string), 40, true)]
        public string RequestQTY;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        [FieldMapAttribute("MDATE", typeof(string), 40, false)]
        public string MaintainDate;

        [FieldMapAttribute("MTIME", typeof(string), 40, false)]
        public string MaintainTime;

        [FieldMapAttribute("EAttribute1", typeof(string), 100, true)]
        public string EAttribute1;

    }

    #endregion

    #region WorkPlan
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLWORKPLAN", "BIGSSCODE,PLANDATE,MOCODE,MOSEQ")]
    public class WorkPlan : DomainObject
    {
        public WorkPlan()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, false)]
        public string BigSSCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PLANDATE", typeof(int), 8, false)]
        public int PlanDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MoCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, false)]
        public decimal MoSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PLANSEQ", typeof(decimal), 10, false)]
        public decimal PlanSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PLANQTY", typeof(decimal), 10, false)]
        public decimal PlanQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ACTQTY", typeof(decimal), 10, false)]
        public decimal ActQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MATERIALQTY", typeof(decimal), 10, false)]
        public decimal MaterialQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PLANSTARTTIME", typeof(int), 6, false)]
        public int PlanStartTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PLANENDTIME", typeof(int), 6, true)]
        public int PlanEndTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LASTRECEIVETIME", typeof(int), 6, true)]
        public int LastReceiveTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LASTREQTIME", typeof(int), 6, true)]
        public int LastReqTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PROMISETIME", typeof(int), 6, true)]
        public int PromiseTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ACTIONSTATUS", typeof(string), 40, false)]
        public string ActionStatus;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MATERIALSTATUS", typeof(string), 40, false)]
        public string MaterialStatus;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 8, false)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 100, true)]
        public string Eattribute1;

    }
    #endregion

    #region WorkPlanWithQty

    public class WorkPlanWithQty : WorkPlan
    {
        [FieldMapAttribute("LACKQTY", typeof(decimal), 13, true)] //缺料数量
        public decimal LackQTY;
        [FieldMapAttribute("REMAILQTY", typeof(decimal), 13, true)] //物料余量
        public decimal RemailQty;
        [FieldMapAttribute("TRANSQTY", typeof(decimal), 13, true)] //移转数量
        public decimal TransQty;
        [FieldMapAttribute("MMODELCODE", typeof(string), 40, true)]//机型
        public string MaterialModelCode;
    }

    #endregion

    #region  MaterialIssue
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap(" TBLMaterialIssue", "BIGSSCODE,PLANDATE,MOCODE,MOSEQ,ISSUESEQ")]
    public class MaterialIssue : DomainObject
    {
        public MaterialIssue()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, false)]
        public string BigSSCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PLANDATE", typeof(int), 8, false)]
        public int PlanDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MoCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, false)]
        public decimal MoSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ISSUESEQ", typeof(decimal), 10, false)]
        public decimal IssueSEQ;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ISSUEQTY", typeof(decimal), 10, false)]
        public decimal IssueQTY;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ISSUETYPE", typeof(string), 40, false)]
        public string IssueType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ISSUESTATUS", typeof(string), 40, false)]
        public string IssueStatus;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 8, false)]
        public int MaintainTime;

    }
    #endregion

    #region  MaterialIssuePlanSeq
    public class MaterialIssueAndPlanSeq : MaterialIssue
    {
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PLANSEQ", typeof(decimal), 10, false)]
        public decimal PlanSeq;
    }

    #endregion

    #region  MaterialReqInfo
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap(" TBLMaterialReqInfo", "BIGSSCODE,PLANDATE,MOCODE,MOSEQ,REQUESTSEQ")]
    public class MaterialReqInfo : DomainObject
    {
        public MaterialReqInfo()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, false)]
        public string BigSSCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PLANDATE", typeof(int), 8, false)]
        public int PlanDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MoCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, false)]
        public decimal MoSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REQUESTSEQ", typeof(decimal), 10, false)]
        public decimal RequestSEQ;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PLANSEQ", typeof(decimal), 10, false)]
        public decimal PlanSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLMATERIAL", "MCODE", "MCHSHORTDESC")]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REQUESTQTY", typeof(decimal), 10, false)]
        public decimal RequestQTY;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MAYBEQTY", typeof(decimal), 10, false)]
        public decimal MayBeQTY;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("REQTYPE", typeof(string), 40, false)]
        public string ReqType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 8, false)]
        public int MaintainTime;

    }
    #endregion

    #region MaterialReqInfoWithMessage

    public class MaterialReqInfoWithMessage : MaterialReqInfo
    {
        [FieldMapAttribute("MMODELCODE", typeof(string), 40, true)]//机型
        public string MaterialModelCode;
    }
    #endregion






}

