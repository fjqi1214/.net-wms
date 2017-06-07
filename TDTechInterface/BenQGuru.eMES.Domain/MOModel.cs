using System;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.MOModel
{

    #region Item2CartonCFG
    /// <summary>
    /// POWER包装站防错检查需求,本需求只适用于POWER 厂，其它工厂可以通过设置屏蔽该功能。
    /// </summary>
    [Serializable, TableMap("TBLItem2CartonCFG", "ItemName")]
    public class Item2CartonCFG : DomainObject
    {
        public Item2CartonCFG()
        {
        }

        /// <summary>
        /// "1"：双面
        /// "0"：单面
        /// </summary>
        [FieldMapAttribute("PCSTYPE", typeof(string), 1, true)]
        public string PCSTYPE;

        /// <summary>
        /// 产品名称
        /// </summary>
        [FieldMapAttribute("ItemName", typeof(string), 40, true)]
        public string ItemName;

        /// <summary>
        /// M板
        /// </summary>
        [FieldMapAttribute("MPlate", typeof(string), 40, true)]
        public string MPlate;

        /// <summary>
        /// S板
        /// </summary>
        [FieldMapAttribute("SPlate", typeof(string), 40, false)]
        public string SPlate;

        /// <summary>
        /// 大箱料号
        /// </summary>
        [FieldMapAttribute("CartonItemNo", typeof(string), 40, true)]
        public string CartonItemNo;

        /// <summary>
        /// 大箱标签位数
        /// </summary>
        [FieldMapAttribute("CartonLabelLen", typeof(decimal), 10, true)]
        public decimal CartonLabelLen;

        /// <summary>
        /// 起始位数
        /// </summary>
        [FieldMapAttribute("StartPosition", typeof(decimal), 10, true)]
        public decimal StartPosition;

        /// <summary>
        /// 结束位数
        /// </summary>
        [FieldMapAttribute("EndPosition", typeof(decimal), 10, true)]
        public decimal EndPosition;

        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDate", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTime", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
        public string EAttribute1;

    }
    #endregion

    #region DefaultItem2Route
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TblDefaultItem2Route", "ItemCode")]
    public class DefaultItem2Route : DomainObject
    {
        public DefaultItem2Route()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ItemCode", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RouteCode", typeof(string), 40, true)]
        public string RouteCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDate", typeof(int), 8, true)]
        public int MDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTime", typeof(int), 6, true)]
        public int MTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
        public string EAttribute1;

    }
    #endregion

    //#region Material

    ///// <summary>
    /////	Material
    ///// </summary>
    //[Serializable, TableMap("TBLMATERIAL", "MCODE,ORGID")]
    //public class Material : BenQGuru.eMES.Common.Domain.DomainObject
    //{
    //    public Material()
    //    {
    //    }

    //    ///<summary>
    //    ///Material Code
    //    ///</summary>	
    //    [FieldMapAttribute("MCODE", typeof(string), 40, false)]
    //    public string MaterialCode;

    //    ///<summary>
    //    ///Material Name
    //    ///</summary>	
    //    [FieldMapAttribute("MNAME", typeof(string), 200, true)]
    //    public string MaterialName;

    //    ///<summary>
    //    ///Material Description
    //    ///</summary>	
    //    [FieldMapAttribute("MDESC", typeof(string), 200, true)]
    //    public string MaterialDescription;

    //    ///<summary>
    //    ///Material UOM
    //    ///</summary>	
    //    [FieldMapAttribute("MUOM", typeof(string), 40, true)]
    //    public string MaterialUOM;

    //    ///<summary>
    //    ///MaterialType
    //    ///</summary>	
    //    [FieldMapAttribute("MTYPE", typeof(string), 40, false)]
    //    public string MaterialType;

    //    ///<summary>
    //    ///MaterialMachineType
    //    ///</summary>	
    //    [FieldMapAttribute("MMACHINETYPE", typeof(string), 40, true)]
    //    public string MaterialMachineType;

    //    ///<summary>
    //    ///Material Volume
    //    ///</summary>	
    //    [FieldMapAttribute("MVOLUME", typeof(string), 40, true)]
    //    public string MaterialVolume;

    //    ///<summary>
    //    ///Material Model Code
    //    ///</summary>	
    //    [FieldMapAttribute("MMODELCODE", typeof(string), 40, true)]
    //    public string MaterialModelCode;

    //    ///<summary>
    //    ///Material Export Import
    //    ///</summary>	
    //    [FieldMapAttribute("MEXPORTIMPORT", typeof(string), 40, false)]
    //    public string MaterialExportImport;

    //    ///<summary>
    //    ///Material Model Group
    //    ///</summary>	
    //    [FieldMapAttribute("MMODELGROUP", typeof(string), 40, true)]
    //    public string MaterialModelGroup;

    //    ///<summary>
    //    ///Material Group
    //    ///</summary>	
    //    [FieldMapAttribute("MGROUP", typeof(string), 40, true)]
    //    public string MaterialGroup;

    //    ///<summary>
    //    ///Material Group Description
    //    ///</summary>	
    //    [FieldMapAttribute("MGROUPDESC", typeof(string), 40, true)]
    //    public string MaterialGroupDescription;

    //    ///<summary>
    //    ///Material Control Type
    //    ///</summary>	
    //    [FieldMapAttribute("MCONTROLTYPE", typeof(string), 40, false)]
    //    public string MaterialControlType;

    //    ///<summary>
    //    ///Maintain User
    //    ///</summary>	
    //    [FieldMapAttribute("MUSER", typeof(string), 40, false)]
    //    public string MaintainUser;

    //    ///<summary>
    //    ///Maintain Date
    //    ///</summary>	
    //    [FieldMapAttribute("MDATE", typeof(int), 8, false)]
    //    public int MaintainDate;

    //    ///<summary>
    //    ///Maintain Time
    //    ///</summary>	
    //    [FieldMapAttribute("MTIME", typeof(int), 6, false)]
    //    public int MaintainTime;

    //    ///<summary>
    //    ///EAttribute1
    //    ///</summary>	
    //    [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, false)]
    //    public string EAttribute1;

    //    ///<summary>
    //    ///Organization ID
    //    ///</summary>	
    //    [FieldMapAttribute("ORGID", typeof(int), 8, false)]
    //    public int OrganizationID;

    //    /// <summary>
    //    /// Parse Type : " ", "parse_barcode", "parse_prepare", "parse_product"
    //    /// </summary>
    //    [FieldMapAttribute("MPARSETYPE", typeof(string), 100, true)]
    //    public string MaterialParseType;

    //    ///<summary>
    //    ///Check Status
    //    ///</summary>	
    //    [FieldMapAttribute("CHECKSTATUS", typeof(string), 40, true)]
    //    public string CheckStatus;

    //    /// <summary>
    //    /// Check Type : " ", "check_linkbarcode", "check_compareitem"
    //    /// </summary>
    //    [FieldMapAttribute("MCHECKTYPE", typeof(string), 40, true)]
    //    public string MaterialCheckType;

    //    ///<summary>
    //    ///SerialNoLength for Check
    //    ///</summary>	
    //    [FieldMapAttribute("SNLENGTH", typeof(int), 8, true)]
    //    public int SerialNoLength;

    //    [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
    //    [FieldDisplay(FieldDisplayModifyType.Append, "TBLVENDOR", "VENDORCODE", "VENDORNAME")]
    //    public string VendorCode;

    //    [FieldMapAttribute("ROHS", typeof(string), 40, true)]
    //    public string ROHS;

    //    [FieldMapAttribute("NEEDVENDOR", typeof(string), 40, true)]
    //    public string NeedVendor;

    //    [FieldMapAttribute("MSHELFLIFE", typeof(int), 8, true)]
    //    public int MShelfLife;

    //    [FieldMapAttribute("ISSMT", typeof(string), 40, false)]
    //    public string IsSMT;

    //}

    //[Serializable]
    //public class StockAge : Material
    //{
    //    [FieldMapAttribute("COMPANY", typeof(string), 40, true)]
    //    public string Company;

    //    [FieldMapAttribute("FIRSTCLASS", typeof(string), 100, true)]
    //    public string Firstclass;

    //    [FieldMapAttribute("CUSORDERNO", typeof(string), 40, true)]
    //    public string Cusorderno;

    //    [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
    //    public string StorageCode;

    //    [FieldMapAttribute("ITEMGRADE", typeof(string), 40, true)]
    //    public string Itemgrade;

    //    [FieldMapAttribute("INVPERIODCODE", typeof(string), 40, true)]
    //    public string Invperiodcode;

    //    [FieldMapAttribute("DATEFROM", typeof(int), 8, true)]
    //    public int Datefrom;

    //    [FieldMapAttribute("DATETO", typeof(int), 8, true)]
    //    public int dateto;

    //    [FieldMapAttribute("CNT", typeof(int), 8, true)]
    //    public int CNT;

    //    [FieldMapAttribute("CUSITEMCODE", typeof(string), 40, true)]
    //    public string CustomerItemCode;


    //}

    //#endregion
    #region Material
    /// <summary>
    /// TBLMATERIAL
    /// </summary>
    [Serializable, TableMap("TBLMATERIAL", "MCODE")]
    public class Material : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Material()
        {
        }

        ///<summary>
        ///MchshortDesc
        ///</summary>
        [FieldMapAttribute("MCHSHORTDESC", typeof(string), 500, true)]
        public string MchshortDesc;

        ///<summary>
        ///MenshortDesc
        ///</summary>
        [FieldMapAttribute("MENSHORTDESC", typeof(string), 100, true)]
        public string MenshortDesc;

        ///<summary>
        ///MenlongDesc
        ///</summary>
        [FieldMapAttribute("MENLONGDESC", typeof(string), 2000, true)]
        public string MenlongDesc;

        ///<summary>
        ///MchlongDesc
        ///</summary>
        [FieldMapAttribute("MCHLONGDESC", typeof(string), 2000, true)]
        public string MchlongDesc;

        ///<summary>
        ///MspecialDesc
        ///</summary>
        [FieldMapAttribute("MSPECIALDESC", typeof(string), 200, true)]
        public string MspecialDesc;

        ///<summary>
        ///ModelCode
        ///</summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        ///<summary>
        ///Mstate
        ///</summary>
        [FieldMapAttribute("MSTATE", typeof(int), 22, true)]
        public int Mstate;

        ///<summary>
        ///Sourceflag
        ///</summary>
        [FieldMapAttribute("SOURCEFLAG", typeof(string), 3, false)]
        public string Sourceflag;

        ///<summary>
        ///Validity
        ///</summary>
        [FieldMapAttribute("VALIDITY", typeof(int), 22, true)]
        public int Validity;

        ///<summary>
        ///MaterialType
        ///</summary>
        [FieldMapAttribute("MATERIALTYPE", typeof(string), 40, true)]
        public string MaterialType;

        ///<summary>
        ///Mstate1
        ///</summary>
        [FieldMapAttribute("MSTATE1", typeof(string), 40, true)]
        public string Mstate1;

        ///<summary>
        ///Mstate2
        ///</summary>
        [FieldMapAttribute("MSTATE2", typeof(string), 40, true)]
        public string Mstate2;

        ///<summary>
        ///Mstate3
        ///</summary>
        [FieldMapAttribute("MSTATE3", typeof(string), 40, true)]
        public string Mstate3;

        ///<summary>
        ///Validfrom
        ///</summary>
        [FieldMapAttribute("VALIDFROM", typeof(string), 40, true)]
        public string Validfrom;

        ///<summary>
        ///Eattribute8
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE8", typeof(string), 40, true)]
        public string Eattribute8;

        ///<summary>
        ///CdQty
        ///</summary>
        [FieldMapAttribute("CDQTY", typeof(string), 40, true)]
        public string CdQty;

        ///<summary>
        ///Cdfor
        ///</summary>
        [FieldMapAttribute("CDFOR", typeof(string), 40, true)]
        public string Cdfor;

        ///<summary>
        ///Purchasinggroup
        ///</summary>
        [FieldMapAttribute("PURCHASINGGROUP", typeof(string), 40, true)]
        public string Purchasinggroup;

        ///<summary>
        ///Abcindicator
        ///</summary>
        [FieldMapAttribute("ABCINDICATOR", typeof(string), 40, true)]
        public string Abcindicator;

        ///<summary>
        ///MrpType
        ///</summary>
        [FieldMapAttribute("MRPTYPE", typeof(string), 40, true)]
        public string MrpType;

        ///<summary>
        ///Reorderpoint
        ///</summary>
        [FieldMapAttribute("REORDERPOINT", typeof(string), 40, true)]
        public string Reorderpoint;

        ///<summary>
        ///Mrpcontorller
        ///</summary>
        [FieldMapAttribute("MRPCONTORLLER", typeof(string), 40, true)]
        public string Mrpcontorller;

        ///<summary>
        ///Minimumlotsize
        ///</summary>
        [FieldMapAttribute("MINIMUMLOTSIZE", typeof(string), 40, true)]
        public string Minimumlotsize;

        ///<summary>
        ///Roundingvalue
        ///</summary>
        [FieldMapAttribute("ROUNDINGVALUE", typeof(string), 40, true)]
        public string Roundingvalue;

        ///<summary>
        ///Specialprocyrement
        ///</summary>
        [FieldMapAttribute("SPECIALPROCYREMENT", typeof(string), 40, true)]
        public string Specialprocyrement;

        ///<summary>
        ///Safetystock
        ///</summary>
        [FieldMapAttribute("SAFETYSTOCK", typeof(string), 40, true)]
        public string Safetystock;

        ///<summary>
        ///Bulkmaterial
        ///</summary>
        [FieldMapAttribute("BULKMATERIAL", typeof(string), 40, true)]
        public string Bulkmaterial;

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
        ///MCode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///DqmCode
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 200, true)]
        public string DqmCode;

        ///<summary>
        ///Muom
        ///</summary>
        [FieldMapAttribute("MUOM", typeof(string), 40, true)]
        public string Muom;

        ///<summary>
        ///MType
        ///</summary>
        [FieldMapAttribute("MTYPE", typeof(string), 40, true)]
        public string MType;

        ///<summary>
        ///MmodelCode
        ///</summary>
        [FieldMapAttribute("MCONTROLTYPE", typeof(string), 40, true)]
        public string MCONTROLTYPE;

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

        ///<summary>
        ///Eattribute1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;

        /////<summary>
        /////Orgid
        /////</summary>
        //[FieldMapAttribute("ORGID", typeof(int), 22, false)]
        //public int Orgid;

        ///<summary>
        ///Rohs
        ///</summary>
        [FieldMapAttribute("ROHS", typeof(string), 40, true)]
        public string Rohs;

    }
    #endregion

    #region ItemClass

    [Serializable, TableMap("TBLITEMCLASS", "ITEMGROUP")]
    public class ItemClass : DomainObject
    {
        public ItemClass()
        {
        }

        /// <summary>
        /// ItemGroup
        /// </summary>
        [FieldMapAttribute("ITEMGROUP", typeof(string), 40, false)]
        public string ItemGroup;

        /// <summary>
        /// FirstClass
        /// </summary>
        [FieldMapAttribute("FIRSTCLASS", typeof(string), 40, false)]
        public string FirstClass;

        /// <summary>
        /// SecondClass
        /// </summary>
        [FieldMapAttribute("SECONDCLASS", typeof(string), 40, true)]
        public string SecondClass;

        /// <summary>
        /// ThirdClass
        /// </summary>
        [FieldMapAttribute("THIRDCLASS", typeof(string), 40, true)]
        public string ThirdClass;

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

    #region Item
    /// <summary>
    /// 料号
    /// </summary>
    [Serializable, TableMap("TBLITEM", "ITEMCODE,ORGID")]
    public class Item : DomainObject
    {
        public Item()
        {
        }

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "ITEMDESC")]
        public string ItemCode;

        /// <summary>
        /// 料品描述[ItemDesc]
        /// </summary>
        [FieldMapAttribute("ITEMDESC", typeof(string), 100, true)]
        public string ItemDescription;

        /// <summary>
        /// 料品类别[ItemType]
        /// </summary>
        [FieldMapAttribute("ITEMTYPE", typeof(string), 40, false)]
        public string ItemType;

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
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 料品名称[ItemName]
        /// </summary>
        [FieldMapAttribute("ITEMNAME", typeof(string), 100, true)]
        public string ItemName;

        /// <summary>
        /// 物料规格[ItemVersion]
        /// </summary>
        [FieldMapAttribute("ITEMVER", typeof(string), 40, true)]
        public string ItemVersion;

        /// <summary>
        /// 计量单位[ItemUOM]
        /// </summary>
        [FieldMapAttribute("ITEMUOM", typeof(string), 40, true)]
        public string ItemUOM;

        /// <summary>
        /// 开立日期[ItemDownDate]
        /// </summary>
        [FieldMapAttribute("ITEMDATE", typeof(int), 8, false)]
        public int ItemDate;

        /// <summary>
        /// 开立人员[ItemUser]
        /// </summary>
        [FieldMapAttribute("ITEMUSER", typeof(string), 40, false)]
        public string ItemUser;

        /// <summary>
        /// 管控方式[ItemControl]
        /// </summary>
        [FieldMapAttribute("ITEMCONTROL", typeof(string), 40, true)]
        public string ItemControlType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 配置码
        /// </summary>
        [FieldMapAttribute("ITEMCONFIG", typeof(string), 40, false)]
        public string ItemConfigration;

        /// <summary>
        /// 配置码
        /// </summary>
        [FieldMapAttribute("ITEMBURNINQTY", typeof(int), 10, false)]
        public int ItemBurnInQty;

        /// <summary>
        /// 装箱数量
        /// </summary>
        [FieldMapAttribute("ITEMCARTONQTY", typeof(int), 10, false)]
        public int ItemCartonQty;

        /// <summary>
        /// 最小电流值
        /// </summary>
        [FieldMapAttribute("ElectricCurrentMinValue", typeof(decimal), 15, true)]
        public decimal ElectricCurrentMinValue;

        /// <summary>
        /// 最大电流值
        /// </summary>
        [FieldMapAttribute("ElectricCurrentMaxValue", typeof(decimal), 15, true)]
        public decimal ElectricCurrentMaxValue;

        /// <summary>
        /// 组织编号
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;

        [FieldMapAttribute("CHKITEMOP", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLOP", "OPCODE", "OPDESC")]
        public string CheckItemOP;

        [FieldMapAttribute("LOTSIZE", typeof(int), 8, true)]
        public int LotSize;

        [FieldMapAttribute("PRODUCTCODE", typeof(string), 100, true)]
        public string ItemProductCode;

        [FieldMapAttribute("NEEDCHKCARTON", typeof(string), 40, true)]
        public string NeedCheckCarton;

        [FieldMapAttribute("NEEDCHKACCESSORY", typeof(string), 40, true)]
        public string NeedCheckAccessory;

        //Add by terry 2010-10-28
        [FieldMapAttribute("PCBACOUNT", typeof(int), 22, false)]
        public int PcbaCount;

        //Add by sandy 2014-05-28
        [FieldMapAttribute("BURNUSEMINUTES", typeof(int), 10, false)]
        public int BurnUseMinutes;
    }
    #endregion

    #region ItemForQuery
    public class ItemForQuery : Item
    {
        public ItemForQuery()
        {

        }

        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;
    }
    #endregion

    #region ITEM2BOM

    [Serializable, TableMap("TBLITEM", "ITEMCODE,ORGID")]
    public class ITEM2BOM : Item
    {
        public ITEM2BOM()
        {
        }

        /// <summary>
        /// SBOM标准BOM最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string SBOMMaintainUser = string.Empty;

        /// <summary>
        /// SBOM标准BOM最后维护日期[LastMaintainDate]
        /// </summary>
        public int SBOMMaintainDate = 0;

        /// <summary>
        /// OPBOM产品工序BOM最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string OPBOMMaintainUser = string.Empty;

        /// <summary>
        /// OPBOM 产品工序BOM最后维护日期[LastMaintainDate]
        /// </summary>
        public int OPBOMMaintainDate = 0;

    }

    #endregion

    #region Item2Config
    /// <summary>
    /// 料号
    /// </summary>
    [Serializable, TableMap("TBLITEM2CONFIG", "ITEMCODE,ITEMCONFIG,PARENTCODE,CONFIGCODE,ORGID")]
    public class Item2Config : DomainObject
    {
        public Item2Config()
        {
        }

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 配置码
        /// </summary>
        [FieldMapAttribute("ITEMCONFIG", typeof(string), 40, true)]
        public string ItemConfigration;

        /// <summary>
        /// 属性
        /// </summary>
        [FieldMapAttribute("CONFIGCODE", typeof(string), 40, true)]
        public string ConfigCode;

        /// <summary>
        /// 属性名称
        /// </summary>
        [FieldMapAttribute("CONFIGNAME", typeof(string), 40, true)]
        public string ConfigName;

        /// <summary>
        /// 标准值
        /// </summary>
        [FieldMapAttribute("CONFIGVALUE", typeof(string), 40, true)]
        public string ConfigValue;

        /// <summary>
        /// 配置大类
        /// </summary>
        [FieldMapAttribute("PARENTCODE", typeof(string), 40, true)]
        public string ParentCode;

        /// <summary>
        /// 配置大类名称
        /// </summary>
        [FieldMapAttribute("PARENTNAME", typeof(string), 40, true)]
        public string ParentName;
        /// <summary>
        /// 是否需要比对
        /// </summary>
        [FieldMapAttribute("NEEDCHECK", typeof(int), 1, true)]
        public string NeedCheck;

        /// <summary>
        /// 层级，从1开始
        /// </summary>
        [FieldMapAttribute("LEVELCODE", typeof(int), 10, true)]
        public int Level;

        /// <summary>
        /// 是否leaf
        /// </summary>
        [FieldMapAttribute("ISLEAF", typeof(int), 1, true)]
        public string IsLeaf;

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
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 组织编号
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, true)]
        public int OrganizationID;

    }
    #endregion

    #region ItemLocation
    [Serializable, TableMap("TBLITEMLOCATION", "ITEMCODE,AB,ORGID")]
    public class ItemLocation : DomainObject
    {
        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// AB[AB]
        /// </summary>
        [FieldMapAttribute("AB", typeof(string), 40, true)]
        public string AB;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("Qty", typeof(decimal), 10, true)]
        public decimal Qty;


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
        /// 工段代码
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string SegmentCode;

        /// <summary>
        /// 组织编号
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, true)]
        public int OrganizationID;
    }
    #endregion

    #region Item2Route
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLITEM2ROUTE", "ITEMCODE,ROUTECODE,ORGID")]
    public class Item2Route : DomainObject
    {
        public Item2Route()
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
        /// 
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string RouteCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ISREF", typeof(string), 1, true)]
        public string IsReference;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// Org ID
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;
    }
    #endregion

    #region Item2SPCTable
    /// <summary>
    /// SPC 产品数据收集表 Joe Song 20080822
    /// </summary>
    [Serializable, TableMap("TBLITEM2SPCTBL", "OID")]
    public class Item2SPCTable : DomainObject
    {
        public Item2SPCTable()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OID", typeof(string), 40, true)]
        public string OID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SPCTBLNAME", typeof(string), 40, true)]
        public string SPCTableName;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("STARTDATE", typeof(int), 8, true)]
        public int StartDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ENDDATE", typeof(int), 8, true)]
        public int EndDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SPCDESC", typeof(string), 100, false)]
        public string Description;

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

    }
    #endregion

    #region ITEM2SPCTest
    /// <summary>
    /// SPC产品测试项
    /// </summary>
    [Serializable, TableMap("TBLITEM2SPCTEST", "OID")]
    public class Item2SPCTest : DomainObject
    {
        public Item2SPCTest()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OID", typeof(string), 40, true)]
        public string OID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TESTNAME", typeof(string), 40, true)]
        public string TestName;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(int), 10, true)]
        public int Seq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("USL", typeof(decimal), 10, false)]
        public decimal USL;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LSL", typeof(decimal), 10, false)]
        public decimal LSL;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("UCL", typeof(decimal), 10, false)]
        public decimal UCL;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LCL", typeof(decimal), 10, false)]
        public decimal LCL;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TESTDESC", typeof(string), 100, false)]
        public string Description;

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
        [FieldMapAttribute("AUTOCL", typeof(string), 1, false)]
        public string AutoCL;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOWONLY", typeof(string), 1, false)]
        public string LowOnly;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("UPONLY", typeof(string), 1, false)]
        public string UpOnly;
    }
    #endregion

    #region ItemRoute2OP
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLITEMROUTE2OP", "OPID,ORGID")]
    public class ItemRoute2OP : DomainObject
    {
        public ItemRoute2OP()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLOP", "OPCODE", "OPDESC")]
        public string OPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPID", typeof(string), 100, true)]
        public string OPID;

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
        [FieldMapAttribute("OPCONTROL", typeof(string), 40, true)]
        public string OPControl;

        /// <summary>
        /// IDMerge/ IDSplit/ IDTranslate
        /// </summary>
        [FieldMapAttribute("IDMERGETYPE", typeof(string), 40, true)]
        public string IDMergeType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("IDMERGERULE", typeof(decimal), 10, false)]
        public decimal IDMergeRule;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPSEQ", typeof(decimal), 10, true)]
        public decimal OPSequence;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string RouteCode;

        [FieldMapAttribute("OPTIONALOP", typeof(string), 40, true)]
        public string OptionalOP;

        /// <summary>
        /// Org ID
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;
    }
    #endregion

    #region Item2Dimention
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLITEM2DIM", "ITEMCODE,PARAMNAME,ORGID")]
    public class Item2Dimention : DomainObject
    {
        public Item2Dimention()
        {
        }

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
        [FieldMapAttribute("ItemCode", typeof(string), 40, true)]
        public string ItemCode;

        [FieldMapAttribute("ParamName", typeof(string), 40, true)]
        public string ParamName;

        [FieldMapAttribute("PARAMVALUE", typeof(decimal), 15, true)]
        public decimal ParamValue;

        /// <summary>
        /// 组织编号
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, true)]
        public int OrganizationID;


        /// <summary>
        /// 
        /// </summary>
        //		[FieldMapAttribute("Unit", typeof(string), 40, true)]
        //		public string  Unit="MM";
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("LengthMin", typeof(string), 100, false)]
        //		public string  LengthMin;
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("LengthMax", typeof(string), 100, false)]
        //		public string  LengthMax;
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("WidthMin", typeof(string), 100, false)]
        //		public string  WidthMin;
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("WidthMax", typeof(string), 100, false)]
        //		public string  WidthMax;
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("BoardHeightMin", typeof(string), 100, false)]
        //		public string  BoardHeightMin;
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("BoardHeightMax", typeof(string), 100, false)]
        //		public string  BoardHeightMax;
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("HeightMin", typeof(string), 100, false)]
        //		public string  HeightMin;
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("HeightMax", typeof(string), 100, false)]
        //		public string  HeightMax;
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("AllHeightMin", typeof(string), 100, false)]
        //		public string  AllHeightMin;
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("AllHeightMax", typeof(string), 100, false)]
        //		public string  AllHeightMax;
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("Left2RightMin", typeof(string), 100, false)]
        //		public string  Left2RightMin;
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("Left2RightMax", typeof(string), 100, false)]
        //		public string  Left2RightMax;
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("Left2MiddleMin", typeof(string), 100, false)]
        //		public string  Left2MiddleMin;
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("Left2MiddleMax", typeof(string), 100, false)]
        //		public string  Left2MiddleMax;
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("Right2MiddleMin", typeof(string), 100, false)]
        //		public string  Right2MiddleMin;
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("Right2MiddleMax", typeof(string), 100, false)]
        //		public string  Right2MiddleMax;

    }

    public class DimentionParam
    {
        public static string Length = "Length";
        public static string Width = "Width";
        public static string Right2Middle = "Right2Middle";
        public static string Left2Middle = "Left2Middle";
        public static string Left2Right = "Left2Right";
        public static string AllHeight = "AllHeight";
        public static string BoardHeight = "BoardHeight";
        public static string Height = "Height";
    }
    #endregion



    #region MO
    /// <summary>
    /// 工单
    /// </summary>
    [Serializable, TableMap("TBLMO", "MOCODE")]
    public class MO : DomainObject
    {
        public MO()
        {
        }

        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 工单说明
        /// </summary>
        [FieldMapAttribute("MORemark", typeof(string), 500, false)]
        public string MORemark;

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

        /// <summary>
        /// 工单说明
        /// </summary>
        [FieldMapAttribute("MODESC", typeof(string), 100, false)]
        public string MODescription;

        /// <summary>
        /// 工单类型
        /// 来自系统参数的定义:
        /// 1. 系统参数类型: MOTYPE
        /// </summary>
        [FieldMapAttribute("MOTYPE", typeof(string), 40, true)]
        public string MOType;

        /// <summary>
        /// 预计开工日期
        /// </summary>
        [FieldMapAttribute("MOPLANSTARTDATE", typeof(int), 8, true)]
        public int MOPlanStartDate;

        /// <summary>
        /// 预计完工日期
        /// </summary>
        [FieldMapAttribute("MOPLANENDDATE", typeof(int), 8, true)]
        public int MOPlanEndDate;


        /// <summary>
        /// 投入量
        /// </summary>
        [FieldMapAttribute("MOINPUTQTY", typeof(decimal), 10, true)]
        public decimal MOInputQty;


        /// <summary>
        /// 预计产出量
        /// </summary>
        [FieldMapAttribute("MOPLANQTY", typeof(decimal), 10, true)]
        public decimal MOPlanQty;



        /// <summary>
        /// 投入比例
        /// </summary>
        [FieldMapAttribute("IDMERGERULE", typeof(decimal), 10, true)]
        public decimal IDMergeRule;

        /// <summary>
        /// PCBA版本
        /// </summary>
        [FieldMapAttribute("MOPCBAVER", typeof(string), 40, false)]
        public string MOPCBAVersion;

        /// <summary>
        /// BIOS版本
        /// </summary>
        [FieldMapAttribute("MOBIOSVER", typeof(string), 40, false)]
        public string MOBIOSVersion;

        /// <summary>
        /// 订单单号
        /// </summary>
        [FieldMapAttribute("ORDERNO", typeof(string), 40, false)]
        public string OrderNO;

        /// <summary>
        /// 订单序号
        /// </summary>
        [FieldMapAttribute("ORDERSEQ", typeof(decimal), 10, true)]
        public decimal OrderSequence;

        /// <summary>
        /// 客户单号
        /// </summary>
        [FieldMapAttribute("CUSORDERNO", typeof(string), 40, false)]
        public string CustomerOrderNO;

        /// <summary>
        /// 客户代号
        /// </summary>
        [FieldMapAttribute("CUSCODE", typeof(string), 40, false)]
        public string CustomerCode;

        /// <summary>
        /// 客户料品
        /// </summary>
        [FieldMapAttribute("CUSITEMCODE", typeof(string), 40, false)]
        public string CustomerItemCode;

        /// <summary>
        /// 开立日期
        /// </summary>
        [FieldMapAttribute("MODOWNDATE", typeof(int), -1, true)]
        public int MODownloadDate;

        /// <summary>
        /// 开立人员
        /// </summary>
        [FieldMapAttribute("MOUSER", typeof(string), 40, false)]
        public string MOUser;

        /// <summary>
        /// 生产状态
        /// </summary>
        [FieldMapAttribute("MOSTATUS", typeof(string), 10, true)]
        public string MOStatus;

        /// <summary>
        /// 实际开工日期
        /// </summary>
        [FieldMapAttribute("MOACTSTARTDATE", typeof(int), 8, true)]
        public int MOActualStartDate;

        /// <summary>
        /// 实际完工日期
        /// </summary>
        [FieldMapAttribute("MOACTENDDATE", typeof(int), 8, true)]
        public int MOActualEndDate;

        /// <summary>
        /// 实际产出量
        /// </summary>
        [FieldMapAttribute("MOACTQTY", typeof(decimal), 10, true)]
        public decimal MOActualQty;

        /// <summary>
        /// 实际报废量
        /// </summary>
        [FieldMapAttribute("MOSCRAPQTY", typeof(decimal), 10, true)]
        public decimal MOScrapQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CUSNAME", typeof(string), 100, false)]
        public string CustomerName;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOMEMO", typeof(string), 100, false)]
        public string MOMemo;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOVER", typeof(string), 40, true)]
        public string MOVersion;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FACTORY", typeof(string), 40, false)]
        public string Factory;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ISCONINPUT", typeof(string), 1, true)]
        public string IsControlInput;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ISBOMPASS", typeof(string), 1, true)]
        public string IsBOMPass;

        /// <summary>
        /// 下发日期
        /// </summary>
        [FieldMapAttribute("MORELEASEDATE", typeof(int), 8, true)]
        public int MOReleaseDate;

        /// <summary>
        /// 下发时间
        /// </summary>
        [FieldMapAttribute("MORELEASETIME", typeof(int), 6, true)]
        public int MOReleaseTime;

        /// <summary>
        /// 暂停原因
        /// </summary>
        [FieldMapAttribute("MOPENDINGCAUSE", typeof(string), 50, true)]
        public string MOPendingCause;

        /// <summary>
        /// 导入日期
        /// </summary>
        [FieldMapAttribute("MOIMPORTDATE", typeof(int), 8, true)]
        public int MOImportDate;

        /// <summary>
        /// 导入时间
        /// </summary>
        [FieldMapAttribute("MOIMPORTTIME", typeof(int), 6, true)]
        public int MOImportTime;

        /// <summary>
        /// 脱离工单数量
        /// </summary>
        [FieldMapAttribute("OffMoQty", typeof(decimal), 10, false)]
        public decimal MOOffQty = 0;

        /// <summary>
        /// 是否比对软件版本
        /// 1 表示比对 , 0 表示不比对
        /// </summary>
        [FieldMapAttribute("ISCOMPARESOFT", typeof(int), 1, true)]
        public int IsCompareSoft = 0;

        /// <summary>
        /// RMA单号
        /// </summary>
        [FieldMapAttribute("RMABILLCODE", typeof(string), 40, true)]
        public string RMABillCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;

        //Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization
        /// <summary>
        /// Organization ID
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;

        /// <summary>
        /// BOM Version
        /// </summary>
        [FieldMapAttribute("MOBOM", typeof(string), 40, true)]
        public string BOMVersion;
        //End

        [FieldMapAttribute("MOOP", typeof(string), 40, true)]
        public string MOOP;

        [FieldMapAttribute("ITEMDESC", typeof(string), 200, true)]
        public string MaterialDescription;

        /// <summary>
        /// 计划开工时间
        /// </summary>
        [FieldMapAttribute("MOPLANSTARTTIME", typeof(int), 22, true)]
        public int MOPlanStartTime;

        /// <summary>
        /// 计划完工时间
        /// </summary>
        [FieldMapAttribute("MOPLANENDTIME", typeof(int), 22, true)]
        public int MOPlanEndTime;

        /// <summary>
        /// 计划生产产线
        /// </summary>
        [FieldMapAttribute("MOPLANLINE", typeof(string), 40, true)]
        public string MOPlanLine;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute2", typeof(string), 100, false)]
        public string EAttribute2;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute3", typeof(string), 100, false)]
        public string EAttribute3;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute4", typeof(string), 100, false)]
        public string EAttribute4;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute5", typeof(string), 100, false)]
        public string EAttribute5;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute6", typeof(string), 100, false)]
        public string EAttribute6;
    }

    public class MOWithItem : MO
    {
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ItemName", typeof(string), 100, false)]
        public string ItemName;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ItemDescription", typeof(string), 100, false)]
        public string ItemDescription;
    }
    #endregion

    #region TBLMO2RCARDLink
    /// <summary>
    /// 工单关联的序列号
    /// </summary>
    [Serializable, TableMap("TBLMO2RCARDLINK", "RCARD")]
    public class MO2RCARDLINK : DomainObject
    {
        public MO2RCARDLINK()
        {
        }

        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 产品当前序列号
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RCard;


        /// <summary>
        /// 打印次数
        /// </summary>
        [FieldMapAttribute("PRINTTIMES", typeof(int), 6, false)]
        public int PrintTimes;


        /// <summary>
        /// 最后打印人
        /// </summary>
        [FieldMapAttribute("LASTPRINTUSER", typeof(string), 40, true)]
        public string LastPrintUSER;

        /// <summary>
        /// 最后打印日期
        /// </summary>
        [FieldMapAttribute("LASTPRINTDate", typeof(int), 8, true)]
        public int LastPrintDate;

        /// <summary>
        /// 最后打印时间
        /// </summary>
        [FieldMapAttribute("LASTPRINTTime", typeof(int), 6, true)]
        public int LastPrintTime;

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
    }
    #endregion

    #region MO2SAP

    [Serializable, TableMap("TBLMO2SAP", "MOCODE, POSTSEQ")]
    public class MO2SAP : DomainObject
    {
        public MO2SAP()
        {

        }

        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        [FieldMapAttribute("POSTSEQ", typeof(decimal), 10, false)]
        public decimal PostSequence;

        [FieldMapAttribute("MOPRODUCED", typeof(decimal), 13, true)]
        public decimal MOProduced;

        [FieldMapAttribute("MOSCRAP", typeof(decimal), 13, true)]
        public decimal MOScrap;

        [FieldMapAttribute("MOCONFIRM", typeof(string), 10, true)]
        public string MOConfirm;

        [FieldMapAttribute("MOMANHOUR", typeof(string), 13, true)]
        public string MOManHour;

        [FieldMapAttribute("MOMACHINEHOUR", typeof(string), 13, true)]
        public string MOMachineHour;

        [FieldMapAttribute("MOCLOSEDATE", typeof(int), 8, true)]
        public int MOCloseDate;

        [FieldMapAttribute("MOLOCATION", typeof(string), 100, true)]
        public string MOLocation;

        [FieldMapAttribute("MOGRADE", typeof(string), 10, true)]
        public string MOGrade;

        [FieldMapAttribute("MOOP", typeof(string), 40, true)]
        public string MOOP;

        [FieldMapAttribute("FLAG", typeof(string), 10, true)]
        public string Flag;

        [FieldMapAttribute("ERRORMESSAGE", typeof(string), 2000, true)]
        public string ErrorMessage;

        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string EAttribute1;

        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;
    }
    #endregion

    #region MO2SAPDetail
    [Serializable, TableMap("TBLMO2SAPDETAIL", "MOCODE, RCARD")]
    public class MO2SAPDetail : DomainObject
    {
        public MO2SAPDetail()
        {

        }

        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        [FieldMapAttribute("POSTSEQ", typeof(decimal), 10, false)]
        public decimal PostSequence;

        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RuningCrad;

        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

    }
    #endregion

    #region MO2SAPLog
    [Serializable, TableMap("TBLMO2SAPLOG", "MOCODE, POSTSEQ,SEQ")]
    public class MO2SAPLog : DomainObject
    {
        public MO2SAPLog()
        {

        }

        [FieldMapAttribute("SEQ", typeof(int), 8, false)]
        public int Sequence;

        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        [FieldMapAttribute("POSTSEQ", typeof(decimal), 10, false)]
        public decimal PostSequence;

        [FieldMapAttribute("ERRORMESSAGE", typeof(string), 2000, true)]
        public string ErrorMessage;

        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        [FieldMapAttribute("ACTIVE", typeof(string), 2, true)]
        public string Active;

        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;
    }
    #endregion

    #region MOViewField
    /// <summary>
    /// 工单
    /// </summary>
    [Serializable, TableMap("TBLMOVIEWFIELD", "USERCODE,SEQ")]
    public class MOViewField : DomainObject
    {
        public MOViewField()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("USERCODE", typeof(string), 40, true)]
        public string UserCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FIELDNAME", typeof(string), 40, false)]
        public string FieldName;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DESCRIPTION", typeof(string), 200, false)]
        public string Description;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ISDEFAULT", typeof(string), 40, false)]
        public string IsDefault;

    }
    #endregion

    #region OffMoCard
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLOffMoCard", "PK")]
    public class OffMoCard : DomainObject
    {
        public OffMoCard()
        {
        }

        /// <summary>
        /// 工单
        /// </summary>
        [FieldMapAttribute("MoCode", typeof(string), 40, true)]
        public string MoCode;

        /// <summary>
        /// 工单类型
        /// </summary>
        [FieldMapAttribute("MoType", typeof(string), 40, true)]
        public string MoType;

        /// <summary>
        /// 主键
        /// </summary>
        [FieldMapAttribute("PK", typeof(string), 40, true)]
        public string PK;

        /// <summary>
        /// 产品序列号
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RCARD;

        /// <summary>
        /// 维护日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MDATE;

        /// <summary>
        /// 维护用户
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MUSER;

        /// <summary>
        /// 维护时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 8, true)]
        public int MTIME;

        /// <summary>
        /// 备注
        /// </summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string EATTRIBUTE1;

    }
    #endregion

    #region MO2Route
    /// <summary>
    /// 工单生产途程
    /// </summary>
    [Serializable, TableMap("TBLMO2ROUTE", "ROUTECODE,MOCODE")]
    public class MO2Route : DomainObject
    {
        public MO2Route()
        {
        }

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

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
        /// 
        /// </summary>
        [FieldMapAttribute("ROUTETYPE", typeof(string), 40, false)]
        public string RouteType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string RouteCode;

        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

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
        [FieldMapAttribute("ISMROUTE", typeof(string), 1, true)]
        public string IsMainRoute;

    }
    #endregion

    #region MOBOM
    /// <summary>
    /// [MOBOM]
    /// </summary>
    [Serializable, TableMap("TBLMOBOM", "MOCODE,ITEMCODE,MOBITEMCODE,SEQ")]
    public class MOBOM : DomainObject
    {
        public MOBOM()
        {
        }

        /// <summary>
        /// [ECNNo]
        /// </summary>
        [FieldMapAttribute("MOBITEMECN", typeof(string), 40, false)]
        public string MOBOMItemECN;

        /// <summary>
        /// [CItemCode]
        /// </summary>
        [FieldMapAttribute("MOBITEMCODE", typeof(string), 40, false)]
        public string MOBOMItemCode;

        /// <summary>
        /// [CItemName]
        /// </summary>
        [FieldMapAttribute("MOBITEMNAME", typeof(string), 100, false)]
        public string MOBOMItemName;

        /// <summary>
        /// [EffectiveDate]
        /// </summary>
        [FieldMapAttribute("MOBITEMEFFDATE", typeof(int), 8, true)]
        public int MOBOMItemEffectiveDate;

        /// <summary>
        /// [IneffectiveDate]
        /// </summary>
        [FieldMapAttribute("MOBITEMINVDATE", typeof(int), 8, true)]
        public int MOBOMItemInvalidDate;

        /// <summary>
        /// [Location]
        /// </summary>
        [FieldMapAttribute("MOBITEMLOCATION", typeof(string), 100, false)]
        public string MOBOMItemLocation;

        /// <summary>
        /// 1 -  使用中
        /// 0 -  正常
        /// -1 - 已删除
        /// [Status]
        /// </summary>
        [FieldMapAttribute("MOBITEMSTATUS", typeof(string), 1, true)]
        public string MOBOMItemStatus;

        /// <summary>
        /// [LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// [LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// [LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOBITEMINVTIME", typeof(int), 6, true)]
        public int MOBOMItemInvalidTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOBITEMEFFTIME", typeof(int), 6, true)]
        public int MOBOMItemEffectiveTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOBITEMQTY", typeof(decimal), 15, true)]
        public decimal MOBOMItemQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOBSITEMCODE", typeof(string), 40, false)]
        public string MOBOMSourceItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOBITEMDESC", typeof(string), 100, false)]
        public string MOBOMItemDescription;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOBITEMVER", typeof(string), 40, false)]
        public string MOBOMItemVersion;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOBITEMCONTYPE", typeof(string), 40, true)]
        public string MOBOMItemControlType;


        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		[FieldMapAttribute("MOBOMEXCEPTION", typeof(string), 100, true)]
        public string MOBOMException;

        //opbom 单机用量,用于比对opbom的记录
        public decimal OPBOMItemQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOBOMITEMUOM", typeof(string), 100, true)]
        public string MOBOMItemUOM;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 工序Code	(从SAP中导入)
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        [FieldMapAttribute("MOBOM", typeof(string), 40, true)]
        public string MoBOM;

        [FieldMapAttribute("MOBOMLINE", typeof(string), 40, true)]
        public string MOBOMLine;

        [FieldMapAttribute("MOFAC", typeof(string), 40, true)]
        public string MOFactory;

        [FieldMapAttribute("MORESOURCE", typeof(string), 40, true)]
        public string MOResource;
    }

    #endregion

    #region Model
    /// <summary>
    /// 料号
    /// </summary>
    [Serializable, TableMap("TBLMODEL", "MODELCODE,ORGID")]
    public class Model : DomainObject
    {
        public Model()
        {
        }

        /// <summary>
        /// 料品描述[ItemDesc]
        /// </summary>
        [FieldMapAttribute("MODELDESC", typeof(string), 100, false)]
        public string ModelDescription;

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
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 机种代码[Model]
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 是否可入库
        /// </summary>
        [FieldMapAttribute("ISINV", typeof(string), 1, true)]
        public string IsInventory;

        /// <summary>
        /// 是否使用回流
        /// </summary>
        [FieldMap("ISReflow", typeof(string), 1, true)]
        public string IsReflow;

        /// <summary>
        /// 是否检查设备连线数量
        /// </summary>
        [FieldMap("IsCheckDataLink", typeof(string), 1, true)]
        public string IsCheckDataLink;

        /// <summary>
        /// 设备连线样本数量
        /// </summary>
        [FieldMap("DataLinkQty", typeof(int), 1, true)]
        public int DataLinkQty;

        /// <summary>
        /// 是否检查尺寸量测数量
        /// </summary>
        [FieldMap("IsDim", typeof(string), 1, true)]
        public string IsDim;

        /// <summary>
        /// 尺寸量测数量 
        /// </summary>
        [FieldMap("DimQty", typeof(int), 1, true)]
        public int DimQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 组织编号
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, true)]
        public int OrganizationID;

    }
    #endregion

    #region ModelBarcodeRule

    [Serializable, TableMap("TBLBARCODERULE", "MODELCODE,AMODELCODE")]

    public class BarcodeRule : DomainObject
    {
        public BarcodeRule()
        {
        }

        /// <summary>
        /// 机种代码[Model]
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("AMODELCODE", typeof(string), 40, true)]
        public string AModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ADESC", typeof(string), 100, false)]
        public string Description;


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
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

    }

    #endregion

    #region Model2Item
    /// <summary>
    /// 料号
    /// </summary>
    [Serializable, TableMap("TBLMODEL2ITEM", "MODELCODE,ITEMCODE,ORGID")]
    public class Model2Item : DomainObject
    {
        public Model2Item()
        {
        }

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
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 机种代码[Model]
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// ORGID
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;
    }
    #endregion

    #region Model2OP
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLMODEL2OP", "OPID,ORGID")]
    public class Model2OP : DomainObject
    {
        public Model2OP()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, false)]
        public string RouteCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPID", typeof(string), 100, true)]
        public string OPID;

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
        [FieldMapAttribute("OPCONTROL", typeof(string), 40, true)]
        public string OPControl;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("IDMERGETYPE", typeof(string), 40, true)]
        public string IDMergeType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("IDMERGERULE", typeof(decimal), 10, false)]
        public decimal IDMergeRule;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPSEQ", typeof(decimal), 10, true)]
        public decimal OPSequence;

        /// <summary>
        /// 机种代码[Model]
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, false)]
        public string ModelCode;

        /// <summary>
        /// ORGID
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;
    }
    #endregion

    #region Model2Route
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLMODEL2ROUTE", "ROUTECODE,MODELCODE,ORGID")]
    public class Model2Route : DomainObject
    {
        public Model2Route()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string RouteCode;

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
        /// 机种代码[Model]
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// ORGID
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;
    }
    #endregion

    #region MORunningCard
    /// <summary>
    /// 工单流程卡号
    /// </summary>
    [Serializable, TableMap("TBLMORCARD", "MOCODE,SEQ")]
    public class MORunningCard : DomainObject
    {
        public MORunningCard()
        {
        }

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

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
        /// 备注
        /// </summary>
        [FieldMapAttribute("MORCARDMEMO", typeof(string), 100, false)]
        public string MORunningCardMemo;

        /// <summary>
        /// 流程卡起始号
        /// </summary>
        [FieldMapAttribute("MORCARDEND", typeof(string), 30, false)]
        public string MORunningCardEnd;

        /// <summary>
        /// 流程卡截止号
        /// </summary>
        [FieldMapAttribute("MORCARDSTART", typeof(string), 40, false)]
        public string MORunningCardStart;

        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;

    }
    #endregion

    #region MORunningCardRange
    /// <summary>
    /// 工单流程卡号
    /// </summary>
    [Serializable, TableMap("TBLMORCARDRANGE", "SEQ,MOCODE")]
    public class MORunningCardRange : DomainObject
    {
        public MORunningCardRange()
        {
        }

        /// <summary>
        /// 备注
        /// </summary>
        [FieldMapAttribute("MORCARDMEMO", typeof(string), 100, false)]
        public string MORunningCardMemo;

        /// <summary>
        /// 流程卡起始号
        /// </summary>
        [FieldMapAttribute("MORCARDEND", typeof(string), 40, false)]
        public string MORunningCardEnd;

        /// <summary>
        /// 流程卡截止号
        /// </summary>
        [FieldMapAttribute("MORCARDSTART", typeof(string), 40, false)]
        public string MORunningCardStart;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

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
        /// 流程卡类型：转换前、转换后
        /// </summary>
        [FieldMapAttribute("RCARDTYPE", typeof(string), 40, false)]
        public string RunningCardType;

    }
    #endregion

    #region OPBOM
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLOPBOM", "OBCODE,ITEMCODE,OPBOMVER,ORGID")]
    public class OPBOM : DomainObject
    {
        public OPBOM()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OBCODE", typeof(string), 40, true)]
        public string OPBOMCode;

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
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPDESC", typeof(string), 100, false)]
        public string OPBOMDescription;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OBROUTE", typeof(string), 40, false)]
        public string OPBOMRoute;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPBOMVER", typeof(string), 40, true)]
        public string OPBOMVersion;

        /// <summary>
        /// 1，生效
        /// 0，失效
        /// </summary>
        [FieldMapAttribute("Avialable", typeof(int), 10, true)]
        public int Avialable;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 10, true)]
        public string EAttribute1;

        /// <summary>
        /// Org ID
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;
    }
    #endregion

    #region OPBOMDetail
    /// <summary>
    /// [SBOM]
    /// </summary>
    [Serializable, TableMap("TBLOPBOMDETAIL", "OBITEMCODE,ITEMCODE,OBCODE,OPBOMVER,OPID,ActionType,ORGID")]
    public class OPBOMDetail : DomainObject
    {
        public OPBOMDetail()
        {
        }

        /// <summary>
        /// [ECNNo]
        /// </summary>
        [FieldMapAttribute("OBITEMECN", typeof(string), 40, false)]
        public string OPBOMItemECN;

        /// <summary>
        /// [CItemCode]
        /// </summary>
        [FieldMapAttribute("OBITEMCODE", typeof(string), 40, true)]
        public string OPBOMItemCode;

        /// <summary>
        /// [CItemName]
        /// </summary>
        [FieldMapAttribute("OBITEMNAME", typeof(string), 100, false)]
        public string OPBOMItemName;

        /// <summary>
        /// [LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// [LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// [LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OBITEMQTY", typeof(decimal), 10, true)]
        public decimal OPBOMItemQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OBSITEMCODE", typeof(string), 40, false)]
        public string OPBOMSourceItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OBITEMVER", typeof(string), 40, false)]
        public string OPBOMItemVersion;

        /// <summary>
        /// Lot/PIC
        /// </summary>
        [FieldMapAttribute("OBITEMCONTYPE", typeof(string), 40, true)]
        public string OPBOMItemControlType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 是否要做物料控制
        /// </summary>
        [FieldMapAttribute("ISITEMCHECK", typeof(string), 1, true)]
        public string IsItemCheck;

        /// <summary>
        /// 物料控制选项
        /// </summary>
        [FieldMapAttribute("ITEMCHECKVALUE", typeof(string), 40, false)]
        public string ItemCheckValue;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OBCODE", typeof(string), 40, true)]
        public string OPBOMCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, false)]
        public string OPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPBOMVER", typeof(string), 40, true)]
        public string OPBOMVersion;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OBITEMUOM", typeof(string), 40, true)]
        public string OPBOMItemUOM;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPID", typeof(string), 40, true)]
        public string OPID;

        /// <summary>
        /// 成品/半成品
        /// </summary>
        [FieldMapAttribute("OBITEMTYPE", typeof(string), 40, true)]
        public string OPBOMItemType;


        /// <summary>
        /// [EffectiveDate]
        /// </summary>
        [FieldMapAttribute("OBITEMEFFDATE", typeof(int), 8, true)]
        public int OPBOMItemEffectiveDate;

        /// <summary>
        /// [IneffectiveDate]
        /// </summary>
        [FieldMapAttribute("OBITEMINVDATE", typeof(int), 8, true)]
        public int OPBOMItemInvalidDate;


        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OBITEMEFFTIME", typeof(int), 6, true)]
        public int OPBOMItemEffectiveTime;



        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OBITEMINVTIME", typeof(int), 6, true)]
        public int OPBOMItemInvalidTime;

        /// <summary>
        /// 0,上料
        /// 1,下料
        /// </summary>
        [FieldMapAttribute("ActionType", typeof(int), 10, false)]
        public int ActionType;

        /// <summary>
        /// Org ID
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;

        /// <summary>
        /// OPBOM Item Seq
        /// </summary>
        [FieldMapAttribute("OBITEMSEQ", typeof(int), 8, true)]
        public int OPBOMItemSeq;

        /// <summary>
        /// Parse Type : " ", "parse_barcode", "parse_prepare", "parse_product"
        /// </summary>
        [FieldMapAttribute("OBPARSETYPE", typeof(string), 100, true)]
        public string OPBOMParseType;

        ///<summary>
        ///Check Status
        ///</summary>	
        [FieldMapAttribute("CHECKSTATUS", typeof(string), 40, false)]
        public string CheckStatus;

        /// <summary>
        /// Check Type : " ", "check_linkbarcode", "check_compareitem"
        /// </summary>
        [FieldMapAttribute("OBCHECKTYPE", typeof(string), 40, true)]
        public string OPBOMCheckType;

        /// <summary>
        /// OPBOM Is Valid (1 -- true)
        /// </summary>
        [FieldMapAttribute("OBVALID", typeof(int), 8, true)]
        public int OPBOMValid;

        ///<summary>
        ///SerialNoLength for Check
        ///</summary>	
        [FieldMapAttribute("SNLENGTH", typeof(int), 8, true)]
        public int SerialNoLength;

        ///<summary>
        ///NeedVendor
        ///</summary>
        [FieldMapAttribute("NEEDVENDOR", typeof(string), 40, true)]
        public string NeedVendor;

    }
    #endregion

    #region OPBOMDetailAndMINNO
    /// <summary>
    /// [SBOM]
    /// </summary>
    [Serializable]
    public class OPBOMDetailAndMINNO : OPBOMDetail
    {
        public OPBOMDetailAndMINNO()
        {
        }

        /// <summary>
        /// 批号
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LotNO;

        /// <summary>
        /// 最小包装条码（批号）
        /// </summary>
        [FieldMapAttribute("MITEMPACKEDNO", typeof(string), 40, true)]
        public string MItemPackedNo;

        /// <summary>
        /// 序号
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Seq;

        /// <summary>
        /// 原始数量
        /// </summary>
        [FieldMapAttribute("LOTQTY", typeof(decimal), 13, true)]
        public decimal LotQty;

        /// <summary>
        /// 实际数量
        /// </summary>
        [FieldMapAttribute("LOTACTQTY", typeof(decimal), 28, true)]
        public decimal LotActQty;

    }
    #endregion

    #region OPItemControl
    /// <summary>
    /// 工序物料控制
    /// </summary>
    [Serializable, TableMap("TBLOPITEMCONTROL", "SEQ,ITEMCODE,OBCODE,OPBOMVER,OPID,OBITEMCODE")]
    public class OPItemControl : DomainObject
    {
        public OPItemControl()
        {
        }

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

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
        /// 序号
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

        /// <summary>
        /// 料品产品序列号范围起始
        /// </summary>
        [FieldMapAttribute("CARDSTART", typeof(string), 40, false)]
        public string CardStart;

        /// <summary>
        /// 料品产品序列号范围结束
        /// </summary>
        [FieldMapAttribute("CARDEND", typeof(string), 50, true)]
        public string CardEnd;

        /// <summary>
        /// PCBA 版本 多个用; 分开
        /// </summary>
        [FieldMapAttribute("PCBAVER", typeof(string), 100, false)]
        public string PCBAVersion;

        /// <summary>
        /// BOIS 版本 多个用; 分开
        /// </summary>
        [FieldMapAttribute("BIOSVER", typeof(string), 100, false)]
        public string BIOSVersion;

        /// <summary>
        /// 料品规格
        /// </summary>
        [FieldMapAttribute("ITEMVER", typeof(string), 100, false)]
        public string ItemVersion;

        /// <summary>
        /// 厂商代号
        /// </summary>
        [FieldMapAttribute("VCODE", typeof(string), 100, false)]
        public string VendorCode;

        /// <summary>
        /// 料品生产日期 多个用; 分开
        /// </summary>
        [FieldMapAttribute("DCSTART", typeof(string), 40, false)]
        public string DateCodeStart;

        /// <summary>
        /// 厂商料号
        /// </summary>
        [FieldMapAttribute("VITEMCODE", typeof(string), 100, false)]
        public string VendorItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DCEND", typeof(string), 40, false)]
        public string DateCodeEnd;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MEMO", typeof(string), 100, false)]
        public string MEMO;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OBCODE", typeof(string), 40, true)]
        public string OPBOMCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPBOMVER", typeof(string), 40, true)]
        public string OPBOMVersion;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPID", typeof(string), 40, true)]
        public string OPID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// [CItemCode]
        /// </summary>
        [FieldMapAttribute("OBITEMCODE", typeof(string), 40, true)]
        public string OPBOMItemCode;

    }
    #endregion

    #region Resource2MO
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLRES2MO", "SEQ")]
    public class Resource2MO : DomainObject
    {
        public Resource2MO()
        {
        }

        /// <summary>
        /// 序号
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

        /// <summary>
        /// 资源代码
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, false)]
        public string ResourceCode;

        /// <summary>
        /// 开始日期
        /// </summary>
        [FieldMapAttribute("STARTDATE", typeof(int), 8, true)]
        public int StartDate;

        /// <summary>
        /// 开始时间
        /// </summary>
        [FieldMapAttribute("STARTTIME", typeof(int), 6, true)]
        public int StartTime;

        /// <summary>
        /// 结束日期
        /// </summary>
        [FieldMapAttribute("ENDDATE", typeof(int), 8, true)]
        public int EndDate;

        /// <summary>
        /// 结束时间
        /// </summary>
        [FieldMapAttribute("ENDTIME", typeof(int), 6, true)]
        public int EndTime;

        /// <summary>
        /// 工单获取方式：Static/FromRCard
        /// </summary>
        [FieldMapAttribute("MOGETTYPE", typeof(string), 40, false)]
        public string MOGetType;

        /// <summary>
        /// 固定工单代码
        /// </summary>
        [FieldMapAttribute("STATICMOCODE", typeof(string), 40, false)]
        public string StaticMOCode;

        /// <summary>
        /// 工单截取序列号起始位置
        /// </summary>
        [FieldMapAttribute("MOCODERCARDSTARTIDX", typeof(decimal), 10, true)]
        public decimal MOCodeRunningCardStartIndex;

        /// <summary>
        /// 工单长度
        /// </summary>
        [FieldMapAttribute("MOCODELEN", typeof(decimal), 10, true)]
        public decimal MOCodeLength;

        /// <summary>
        /// 工单前缀
        /// </summary>
        [FieldMapAttribute("MOCODEPREFIX", typeof(string), 40, false)]
        public string MOCodePrefix;

        /// <summary>
        /// 工单后缀
        /// </summary>
        [FieldMapAttribute("MOCODEPOSTFIX", typeof(string), 40, false)]
        public string MOCodePostfix;

        /// <summary>
        /// 是否做序列号防呆
        /// </summary>
        [FieldMapAttribute("CHKRCARDFORMAT", typeof(string), 1, true)]
        public string CheckRunningCardFormat;

        /// <summary>
        /// 序列号前缀
        /// </summary>
        [FieldMapAttribute("RCARDPREFIX", typeof(string), 40, false)]
        public string RunningCardPrefix;

        /// <summary>
        /// 序列号长度
        /// </summary>
        [FieldMapAttribute("RCARDLEN", typeof(decimal), 10, true)]
        public decimal RunningCardLength;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
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
        [FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
        public string EAttribute1;

    }
    #endregion

    #region SBOM
    /// <summary>
    /// [SBOM]
    /// </summary>
    [Serializable, TableMap("TBLSBOM", "ITEMCODE,SBITEMCODE,SBOMVER,SBITEMPROJECT,SBITEMSEQ,ORGID")]
    public class SBOM : DomainObject
    {
        public SBOM()
        {
        }

        /// <summary>
        /// [ECNNo]
        /// </summary>
        [FieldMapAttribute("SBITEMECN", typeof(string), 40, false)]
        public string SBOMItemECN;

        /// <summary>
        /// [CItemCode]
        /// </summary>
        [FieldMapAttribute("SBITEMCODE", typeof(string), 40, false)]
        public string SBOMItemCode;

        /// <summary>
        /// [CItemName]
        /// </summary>
        [FieldMapAttribute("SBITEMNAME", typeof(string), 100, false)]
        public string SBOMItemName;

        /// <summary>
        /// [EffectiveDate]
        /// </summary>
        [FieldMapAttribute("SBITEMEFFDATE", typeof(int), 8, true)]
        public int SBOMItemEffectiveDate;

        /// <summary>
        /// [IneffectiveDate]
        /// </summary>
        [FieldMapAttribute("SBITEMINVDATE", typeof(int), 8, true)]
        public int SBOMItemInvalidDate;

        /// <summary>
        /// [Location]
        /// </summary>
        [FieldMapAttribute("SBITEMLOCATION", typeof(string), 100, false)]
        public string SBOMItemLocation;

        /// <summary>
        /// 1 -  使用中
        /// 0 -  正常
        /// -1 - 已删除
        /// [Status]
        /// </summary>
        [FieldMapAttribute("SBITEMSTATUS", typeof(string), 1, true)]
        public string SBOMItemStatus;

        /// <summary>
        /// [LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// [LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// [LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SBITEMINVTIME", typeof(int), 6, true)]
        public int SBOMItemInvalidTime;


        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SBITEMUOM", typeof(string), 40, true)]
        public string SBOMItemUOM;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SBITEMEFFTIME", typeof(int), 6, true)]
        public int SBOMItemEffectiveTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SBITEMQTY", typeof(decimal), 16, true)]
        public decimal SBOMItemQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SBSITEMCODE", typeof(string), 40, false)]
        public string SBOMSourceItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SBITEMDESC", typeof(string), 100, false)]
        public string SBOMItemDescription;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SBITEMVER", typeof(string), 40, false)]
        public string SBOMItemVersion;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SBITEMCONTYPE", typeof(string), 40, true)]
        public string SBOMItemControlType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

        /// <summary>
        /// 替代项目组
        /// </summary>
        [FieldMapAttribute("ALPGR", typeof(string), 10, true)]
        public string ALPGR = string.Empty;

        /// <summary>
        /// 库别
        /// </summary>
        [FieldMapAttribute("SBWH", typeof(string), 40, true)]
        public string SBOMWH;

        /// <summary>
        /// 物料父阶料号
        /// </summary>
        [FieldMapAttribute("SBPITEMCODE", typeof(string), 40, true)]
        public string SBOMParentItemCode;

        /// <summary>
        /// 组织编号
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, true)]
        public int OrganizationID;

        /// <summary>
        /// SBOM Version
        /// </summary>
        [FieldMapAttribute("SBOMVER", typeof(string), 40, true)]
        public string SBOMVersion;

        [FieldMapAttribute("ITEMDESC", typeof(string), 40, true)]
        public string ItemDescription;

        [FieldMapAttribute("SBFACTORY", typeof(string), 40, false)]
        public string SBOMFactory;

        [FieldMapAttribute("SBUSAGE", typeof(string), 40, true)]
        public string SBOMUsage;

        [FieldMapAttribute("SBITEMPROJECT", typeof(string), 40, false)]
        public string SBOMItemProject;

        [FieldMapAttribute("SBITEMSEQ", typeof(string), 40, false)]
        public string SBOMItemSequence;

        [FieldMapAttribute("LOCATION", typeof(string), 400, true)]
        public string Location;
    }
    #endregion

    #region Order
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLORDER", "OrderNumber")]
    public class Order : DomainObject
    {
        public Order()
        {
        }

        /// <summary>
        /// 订单号
        /// </summary>
        [FieldMapAttribute("OrderNumber", typeof(string), 40, true)]
        public string OrderNumber;

        /// <summary>
        /// 计划出货日期
        /// </summary>
        [FieldMapAttribute("PlanShipDate", typeof(int), 8, true)]
        public int PlanShipDate;

        /// <summary>
        /// 实际出货日期
        /// </summary>
        [FieldMapAttribute("ActShipDate", typeof(int), 8, false)]
        public int ActShipDate;

        /// <summary>
        /// 维护人员
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

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
        /// 备注
        /// </summary>
        [FieldMapAttribute("eAttribute1", typeof(string), 40, false)]
        public string eAttribute1;

        /// <summary>
        /// 实际出货周
        /// </summary>
        [FieldMapAttribute("ActShipWeek", typeof(decimal), 10, false)]
        public decimal ActShipWeek;

        /// <summary>
        /// 实际出货月
        /// </summary>
        [FieldMapAttribute("ActShipMonth", typeof(decimal), 10, false)]
        public decimal ActShipMonth;

        /// <summary>
        /// 订单状态
        /// </summary>
        [FieldMapAttribute("OrderStatus", typeof(string), 40, true)]
        public string OrderStatus;

    }
    #endregion

    #region OrderDetail
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLORDERDETAIL", "PartnerCode,ItemCode,OrderNumber")]
    public class OrderDetail : DomainObject
    {
        public OrderDetail()
        {
        }

        /// <summary>
        /// 经销商代码
        /// </summary>
        [FieldMapAttribute("PartnerCode", typeof(string), 40, true)]
        public string PartnerCode;

        /// <summary>
        /// 经销商描述
        /// </summary>
        [FieldMapAttribute("PartnerDesc", typeof(string), 100, false)]
        public string PartnerDesc;

        /// <summary>
        /// 产品代码
        /// </summary>
        [FieldMapAttribute("ItemCode", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 计划数量
        /// </summary>
        [FieldMapAttribute("PlanQTY", typeof(decimal), 10, true)]
        public decimal PlanQTY;

        /// <summary>
        /// 产品名称
        /// </summary>
        [FieldMapAttribute("ItemName", typeof(string), 40, true)]
        public string ItemName;

        /// <summary>
        /// 完成数量
        /// </summary>
        [FieldMapAttribute("ActQTY", typeof(decimal), 10, false)]
        public decimal ActQTY;

        /// <summary>
        /// 实际完成日期
        /// </summary>
        [FieldMapAttribute("ActDate", typeof(int), 8, false)]
        public int ActDate;

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
        [FieldMapAttribute("eAttribute1", typeof(string), 40, false)]
        public string eAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PlanDate", typeof(int), 8, true)]
        public int PlanDate;

        /// <summary>
        /// 订单号
        /// </summary>
        [FieldMapAttribute("OrderNumber", typeof(string), 40, true)]
        public string OrderNumber;

    }
    #endregion

    #region Shelf
    /// <summary>
    /// Added by jessie lee, 2006-5-24,车位
    /// </summary>
    [Serializable, TableMap("TBLSHELF", "SHELFNO")]
    public class Shelf : DomainObject
    {
        public Shelf()
        {
        }

        /// <summary>
        /// 车号
        /// </summary>
        [FieldMapAttribute("SHELFNO", typeof(string), 40, true)]
        public string ShelfNO;

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
        [FieldMapAttribute("eAttribute1", typeof(string), 40, false)]
        public string eAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string Status;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("Memo", typeof(string), 100, false)]
        public string Memo;

        /// <summary>
        /// 产品代码
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ITEMCODE;

    }
    #endregion

    #region BurnInOutVolumn
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLBURNVOLUMN", "PKID")]
    public class BurnInOutVolumn : DomainObject
    {
        public BurnInOutVolumn()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PKID", typeof(string), 40, true)]
        public string PKID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("USED", typeof(decimal), 10, true)]
        public decimal Used;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("Total", typeof(decimal), 10, true)]
        public decimal Total;

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
        [FieldMapAttribute("eAttribute1", typeof(string), 40, false)]
        public string eAttribute1;

    }
    #endregion

    #region ShelfActionList
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLSHELFACTIONLIST", "PKID")]
    public class ShelfActionList : DomainObject
    {
        public ShelfActionList()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PKID", typeof(string), 40, true)]
        public string PKID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHELFNO", typeof(string), 40, true)]
        public string ShelfNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string Status;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BIDATE", typeof(int), 8, true)]
        public int BurnInDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BITP", typeof(string), 40, true)]
        public string BurnInTimePeriod;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BITIME", typeof(int), 6, true)]
        public int BurnInTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BODATE", typeof(int), 8, false)]
        public int BurnOutDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BOTIME", typeof(int), 6, true)]
        public int BurnOutTIme;

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
        [FieldMapAttribute("eAttribute1", typeof(string), 40, false)]
        public string eAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BIUSER", typeof(string), 40, true)]
        public string BurnInUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BOUSER", typeof(string), 40, false)]
        public string BurnOutUser;

    }
    #endregion

    #region ShelfActionListForQuery
    public class ShelfActionListForQuery : ShelfActionList
    {
        /// <summary>
        /// 资源代码
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, false)]
        public string ResourceCode;

        /// <summary>
        /// 工序组代码
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string StepSequenceCode;
    }
    #endregion

    #region ERPBOM
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLERPBOM", "SEQUENCE")]
    public class ERPBOM : DomainObject
    {
        public ERPBOM()
        {
        }

        /// <summary>
        /// 物料号
        /// </summary>
        [FieldMapAttribute("BITEMCODE", typeof(string), 40, true)]
        public string BITEMCODE;

        /// <summary>
        /// 发料数量
        /// </summary>
        [FieldMapAttribute("BQTY", typeof(decimal), 10, true)]
        public decimal BQTY;

        /// <summary>
        /// 批号
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LOTNO;

        /// <summary>
        /// 工单
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCODE;

        /// <summary>
        /// 序号
        /// </summary>
        [FieldMapAttribute("SEQUENCE", typeof(decimal), 10, true)]
        public decimal SEQUENCE;

        /// <summary>
        /// [LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// [LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// [LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

    }
    #endregion

    #region Item2SNCheck
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLITEM2SNCHECK", "ITEMCODE,TYPE")]
    public class Item2SNCheck : DomainObject
    {
        public Item2SNCheck()
        {
        }

        /// <summary>
        /// 产品代码
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        //检查类型
        [FieldMapAttribute("TYPE", typeof(string), 40, true)]
        public string Type;

        /// <summary>
        /// 序列号前缀
        /// </summary>
        [FieldMapAttribute("SNPREFIX", typeof(string), 40, true)]
        public string SNPrefix;

        /// <summary>
        /// 序列号长度
        /// </summary>
        [FieldMapAttribute("SNLENGTH", typeof(int), 6, true)]
        public int SNLength;

        /// <summary>
        /// [LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// [LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// [LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        [FieldMapAttribute("SNCONTENTCHECK", typeof(string), 40, true)]
        public string SNContentCheck;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

    }
    #endregion

    #region MOForExport
    /// <summary>
    /// 工单
    /// </summary>
    [Serializable, TableMap("TBLMO", "MOCODE")]
    public class MOForExport : DomainObject
    {
        public MOForExport()
        {
        }

        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 预计开工日期
        /// </summary>
        [FieldMapAttribute("MOPLANSTARTDATE", typeof(string), 40, true)]
        public string MOPlanStartDate1;

        /// <summary>
        /// 预计完工日期
        /// </summary>
        [FieldMapAttribute("MOPLANENDDATE", typeof(string), 40, true)]
        public string MOPlanEndDate1;


        /// <summary>
        /// 预计产出量
        /// </summary>
        [FieldMapAttribute("MOPLANQTY", typeof(string), 40, true)]
        public string MOPlanQty1;


        /// <summary>
        /// 订单单号
        /// </summary>
        [FieldMapAttribute("ORDERNO", typeof(string), 40, false)]
        public string OrderNO;

        /// <summary>
        /// 订单序号
        /// </summary>
        [FieldMapAttribute("ORDERSEQ", typeof(string), 40, true)]
        public string OrderSequence1;

        /// <summary>
        /// 客户单号
        /// </summary>
        [FieldMapAttribute("CUSORDERNO", typeof(string), 40, false)]
        public string CustomerOrderNO1;

        /// <summary>
        /// 客户代号
        /// </summary>
        [FieldMapAttribute("CUSCODE", typeof(string), 40, false)]
        public string CustomerCode;

        /// <summary>
        /// 客户料品
        /// </summary>
        [FieldMapAttribute("CUSITEMCODE", typeof(string), 40, false)]
        public string CustomerItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CUSNAME", typeof(string), 100, false)]
        public string CustomerName;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOMEMO", typeof(string), 100, false)]
        public string MOMemo1;

        /// <summary>
        /// BOM Version
        /// </summary>
        [FieldMapAttribute("MOBOM", typeof(string), 40, true)]
        public string BOMVersion1;
        //End

        /// <summary>
        /// 计划开工时间
        /// </summary>
        [FieldMapAttribute("MOPLANSTARTTIME", typeof(string), 40, true)]
        public string MOPlanStartTime;

        /// <summary>
        /// 计划完工时间
        /// </summary>
        [FieldMapAttribute("MOPLANENDTIME", typeof(string), 40, true)]
        public string MOPlanEndTime;

        /// <summary>
        /// 计划生产产线
        /// </summary>
        [FieldMapAttribute("MOPLANLINE", typeof(string), 40, true)]
        public string MOPlanLine;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute2", typeof(string), 100, false)]
        public string EAttribute2;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute3", typeof(string), 100, false)]
        public string EAttribute3;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute4", typeof(string), 100, false)]
        public string EAttribute4;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute5", typeof(string), 100, false)]
        public string EAttribute5;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute6", typeof(string), 100, false)]
        public string EAttribute6;

        public string ValidateResult;
    }
    #endregion

    #region SoftWareVersionForImport
    /// <summary>
    /// SoftWareVersionForImport
    /// </summary>
    [Serializable, TableMap("TBLSOFTVER", "VERSIONCODE")]
    public class SoftWareVersionForImport : DomainObject
    {
        public SoftWareVersionForImport()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("VERSIONCODE", typeof(string), 40, false)]
        public string VersionCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string Status;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EFFDATE", typeof(string), 40, false)]
        public string EffectiveDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("INVDATE", typeof(string), 40, false)]
        public string InvalidDate;

    }
    #endregion

    #region vendor
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLVENDOR", "VENDORCODE")]
    public class Vendor : DomainObject
    {
        public Vendor()
        {
        }

        /// <summary>
        /// 供应商代码
        /// </summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, false)]
        public string VendorCode;

        /// <summary>
        /// 供应商名称
        /// </summary>
        [FieldMapAttribute("VENDORNAME", typeof(string), 100, false)]
        public string VendorName;
        /// <summary>
        /// 供应商别名
        /// </summary>
        [FieldMapAttribute("ALIAS", typeof(string), 100, true)]
        public string ALIAS;
        /// <summary>
        /// 供应商联系人
        /// </summary>
        [FieldMapAttribute("VENDORUSER", typeof(string), 100, true)]
        public string VENDORUSER;
        /// <summary>
        /// 供应商地址
        /// </summary>
        [FieldMapAttribute("VENDORADDR", typeof(string), 500, true)]
        public string VENDORADDR;

        /// <summary>
        /// 传真
        /// </summary>
        [FieldMapAttribute("FAXNO", typeof(string), 40, true)]
        public string FAXNO;
        /// <summary>
        /// 移动电话
        /// </summary>
        [FieldMapAttribute("MOBILENO", typeof(string), 40, true)]
        public string MOBILENO;

        /// <summary>
        /// [LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// [LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// [LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, true)]
        public string EAttribute1;

        /// </summary>
        [FieldMapAttribute("EAttribute2", typeof(string), 40, true)]
        public string EAttribute2;
        /// </summary>
        [FieldMapAttribute("EAttribute3", typeof(string), 40, true)]
        public string EAttribute3;

    }
    #endregion

    #region Try

    /// <summary>
    ///	Try
    /// </summary>
    [Serializable, TableMap("TBLTRY", "TRYCODE")]
    public class Try : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Try()
        {
        }

        ///<summary>
        ///TryCode
        ///</summary>	
        [FieldMapAttribute("TRYCODE", typeof(string), 40, false)]
        public string TryCode;

        ///<summary>
        ///Status
        ///</summary>	
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        ///<summary>
        ///PlanQty
        ///</summary>	
        [FieldMapAttribute("PLANQTY", typeof(int), 22, true)]
        public int PlanQty;

        ///<summary>
        ///ActualQty
        ///</summary>	
        [FieldMapAttribute("ACTUALQTY", typeof(int), 22, true)]
        public int ActualQty;

        ///<summary>
        ///TryDocument
        ///</summary>	
        [FieldMapAttribute("TRYDOCUMENT", typeof(string), 500, true)]
        public string TryDocument;

        ///<summary>
        ///Department
        ///</summary>	
        [FieldMapAttribute("DEPT", typeof(string), 100, true)]
        public string Department;

        ///<summary>
        ///VendorName
        ///</summary>	
        [FieldMapAttribute("VENDORNAME", typeof(string), 100, true)]
        public string VendorName;

        ///<summary>
        ///ResourceULT
        ///</summary>	
        [FieldMapAttribute("RESULT", typeof(string), 200, true)]
        public string Result;

        ///<summary>
        ///Memo
        ///</summary>	
        [FieldMapAttribute("MEMO", typeof(string), 1000, true)]
        public string Memo;

        ///<summary>
        ///LinkLot
        ///</summary>	
        [FieldMapAttribute("LINKLOT", typeof(string), 40, true)]
        public string LinkLot;

        ///<summary>
        ///CreateUser
        ///</summary>	
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string CreateUser;

        ///<summary>
        ///CreateDate
        ///</summary>	
        [FieldMapAttribute("CDATE", typeof(int), 8, false)]
        public int CreateDate;

        ///<summary>
        ///CreateTime
        ///</summary>	
        [FieldMapAttribute("CTIME", typeof(int), 6, false)]
        public int CreateTime;

        ///<summary>
        ///ReleaseUser
        ///</summary>	
        [FieldMapAttribute("RUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string ReleaseUser;

        ///<summary>
        ///ReleaseDate
        ///</summary>	
        [FieldMapAttribute("RDATE", typeof(int), 8, false)]
        public int ReleaseDate;

        ///<summary>
        ///ReleaseTime
        ///</summary>	
        [FieldMapAttribute("RTIME", typeof(int), 6, false)]
        public int ReleaseTime;

        ///<summary>
        ///FinishUser
        ///</summary>	
        [FieldMapAttribute("FUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string FinishUser;

        ///<summary>
        ///FinishDate
        ///</summary>	
        [FieldMapAttribute("FDATE", typeof(int), 8, false)]
        public int FinishDate;

        ///<summary>
        ///FinishTime
        ///</summary>	
        [FieldMapAttribute("FTIME", typeof(int), 6, false)]
        public int FinishTime;

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
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string EAttribute1;

        ///<summary>
        ///TryType
        ///</summary>	
        [FieldMapAttribute("TRYTYPE", typeof(string), 40, false)]
        public string TryType;

        ///<summary>
        ///TryReason
        ///</summary>	
        [FieldMapAttribute("TRYREASON", typeof(string), 1000, true)]
        public string TryReason;

        ///<summary>
        ///SoftVersion
        ///</summary>	
        [FieldMapAttribute("SOFTVERSION", typeof(string), 40, true)]
        public string SoftVersion;

        ///<summary>
        ///WaitTry
        ///</summary>	
        [FieldMapAttribute("WAITTRY", typeof(string), 40, true)]
        public string WaitTry;

        ///<summary>
        ///Change
        ///</summary>	
        [FieldMapAttribute("CHANGE", typeof(string), 40, true)]
        public string Change;

        ///<summary>
        ///BurnINDuration
        ///</summary>	
        [FieldMapAttribute("BURNINDURATION", typeof(string), 40, true)]
        public string BurnINDuration;
    }

    [Serializable]
    public class TryWithDesc : Try
    {
        [FieldMapAttribute("ItemDesc", typeof(string), 100, true)]
        public string ItemDesc;
    }

    #endregion

    #region Try2RCard

    /// <summary>
    ///	Try2RCard
    /// </summary>
    [Serializable, TableMap("TBLTRY2RCARD", "TRYCODE,RCARD,ITEMCODE")]
    public class Try2RCard : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Try2RCard()
        {
        }

        ///<summary>
        ///TryCode
        ///</summary>	
        [FieldMapAttribute("TRYCODE", typeof(string), 40, false)]
        public string TryCode;

        ///<summary>
        ///RCard
        ///</summary>	
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RCard;

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///OPCode
        ///</summary>	
        [FieldMapAttribute("OPCODE", typeof(string), 40, false)]
        public string OPCode;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
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
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string EAttribute1;

    }

    [Serializable]
    public class Try2RcardNew : Try2RCard
    {
        [FieldMapAttribute("CARTONCODE", typeof(string), 40, true)]
        public string CartonCode;

        [FieldMapAttribute("PALLETCODE", typeof(string), 40, true)]
        public string PalletCode;

        [FieldMapAttribute("MDESC", typeof(string), 100, true)]
        public string ItemDescription;
    }

    #endregion

    #region Try2Lot

    /// <summary>
    ///	Try2Lot
    /// </summary>
    [Serializable, TableMap("TBLTRY2LOT", "TRYCODE,LOTNO")]
    public class Try2Lot : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Try2Lot()
        {
        }

        ///<summary>
        ///TryCode
        ///</summary>	
        [FieldMapAttribute("TRYCODE", typeof(string), 40, false)]
        public string TryCode;

        ///<summary>
        ///LotNo
        ///</summary>	
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNo;

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
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string EAttribute1;

    }

    #endregion

    #region TryAndItemDesc

    public class TryAndItemDesc : Try
    {
        [FieldMapAttribute("ITEMDESC", typeof(string), 100, true)]
        public string ItemDescription;

        [FieldMapAttribute("USERNAME", typeof(string), 100, true)]
        public string UserName;
    }


    #endregion

    #region TestData
    /// <summary>
    /// TBLTESTDATA
    /// </summary>
    [Serializable, TableMap("TBLTESTDATA", "SERIAL")]
    public class TestData : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public TestData()
        {
        }

        ///<summary>
        ///SERIAL
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///RCARD
        ///</summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RCard;

        ///<summary>
        ///MOCODE
        ///</summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///SHIFTDAY
        ///</summary>
        [FieldMapAttribute("SHIFTDAY", typeof(int), 22, false)]
        public int ShiftDay;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///TBLMESENTITYLIST_SERIAL
        ///</summary>
        [FieldMapAttribute("TBLMESENTITYLIST_SERIAL", typeof(int), 22, false)]
        public int Tblmesentitylist_Serial;

        ///<summary>
        ///DEVICENO
        ///</summary>
        [FieldMapAttribute("DEVICENO", typeof(string), 40, true)]
        public string DeviceNO;

        ///<summary>
        ///CKGROUP
        ///</summary>
        [FieldMapAttribute("CKGROUP", typeof(string), 400, true)]
        public string CheckGroup;

        ///<summary>
        ///CKITEMCODE
        ///</summary>
        [FieldMapAttribute("CKITEMCODE", typeof(string), 400, false)]
        public string CheckItemCode;

        ///<summary>
        ///PARAM
        ///</summary>
        [FieldMapAttribute("PARAM", typeof(string), 200, true)]
        public string Param;

        ///<summary>
        ///USL
        ///</summary>
        [FieldMapAttribute("USL", typeof(decimal), 22, true)]
        public decimal USL;

        ///<summary>
        ///LSL
        ///</summary>
        [FieldMapAttribute("LSL", typeof(decimal), 22, true)]
        public decimal LSL;

        ///<summary>
        ///TESTINGVALUE
        ///</summary>
        [FieldMapAttribute("TESTINGVALUE", typeof(string), 200, true)]
        public string TestingValue;

        ///<summary>
        ///TESTINGRESULT
        ///</summary>
        [FieldMapAttribute("TESTINGRESULT", typeof(string), 40, true)]
        public string TestingResult;

        ///<summary>
        ///TESTINGDATE
        ///</summary>
        [FieldMapAttribute("TESTINGDATE", typeof(int), 22, false)]
        public int TestingDate;

        ///<summary>
        ///TESTINGTIME
        ///</summary>
        [FieldMapAttribute("TESTINGTIME", typeof(int), 22, false)]
        public int TestingTime;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
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

    #region TestDataIM
    /// <summary>
    /// TBLTESTDATA
    /// </summary>
    /// 
    [Serializable, TableMap("TBLTESTDATAIM", "RESCODE,MOCODE,ITEMCODE")]
    public class TestDataIM : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public TestDataIM()
        {
        }

        ///<summary>
        ///RESCODE
        ///</summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, false)]
        public string ResCode;


        ///<summary>
        ///RCARD
        ///</summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RCard;

        ///<summary>
        ///MOCODE
        ///</summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;


        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;



        ///<summary>
        ///DEVICENO
        ///</summary>
        [FieldMapAttribute("DEVICENO", typeof(string), 40, true)]
        public string DeviceNO;

        ///<summary>
        ///CKGROUP
        ///</summary>
        [FieldMapAttribute("CKGROUP", typeof(string), 400, true)]
        public string CheckGroup;

        ///<summary>
        ///CKITEMCODE
        ///</summary>
        [FieldMapAttribute("CKITEMCODE", typeof(string), 400, false)]
        public string CheckItemCode;

        ///<summary>
        ///PARAM
        ///</summary>
        [FieldMapAttribute("PARAM", typeof(string), 200, true)]
        public string Param;

        ///<summary>
        ///USL
        ///</summary>
        [FieldMapAttribute("USL", typeof(decimal), 22, true)]
        public decimal USL;

        ///<summary>
        ///LSL
        ///</summary>
        [FieldMapAttribute("LSL", typeof(decimal), 22, true)]
        public decimal LSL;

        ///<summary>
        ///TESTINGVALUE
        ///</summary>
        [FieldMapAttribute("TESTINGVALUE", typeof(string), 200, true)]
        public string TestingValue;

        ///<summary>
        ///TESTINGRESULT
        ///</summary>
        [FieldMapAttribute("TESTINGRESULT", typeof(string), 40, true)]
        public string TestingResult;

        ///<summary>
        ///TESTINGDATE
        ///</summary>
        [FieldMapAttribute("TESTINGDATE", typeof(int), 22, false)]
        public int TestingDate;

        ///<summary>
        ///TESTINGTIME
        ///</summary>
        [FieldMapAttribute("TESTINGTIME", typeof(int), 22, false)]
        public int TestingTime;


    }
    #endregion

    #region FIRSTCHECKBYMO
    /// <summary>
    /// FIRSTCHECKBYMO
    /// </summary>
    [Serializable, TableMap("TBLFIRSTCHECKBYMO", "MOCODE,CHECKDATE")]
    public class FirstCheckByMO : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public FirstCheckByMO()
        {
        }

        ///<summary>
        ///MOCODE
        ///</summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///CHECKDATE
        ///</summary>
        [FieldMapAttribute("CHECKDATE", typeof(int), 22, false)]
        public int CheckDate;

        ///<summary>
        ///CHECKRESULT
        ///</summary>
        [FieldMapAttribute("CHECKRESULT", typeof(string), 40, false)]
        public string CheckResult;

        ///<summary>
        ///MEMO
        ///</summary>
        [FieldMapAttribute("MEMO", typeof(string), 2000, true)]
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

    #region FirstCheckByMOForQuery
    public class FirstCheckByMOForQuery : FirstCheckByMO
    {
        /// <summary>
        /// 产品代码
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 产品名称
        /// </summary>
        [FieldMapAttribute("ITEMNAME", typeof(string), 40, false)]
        public string ItemName;
    }
    #endregion

    #region MO2RCARDLINKForQuery
    [Serializable]
    public class MO2RCARDLINKForQuery : MO2RCARDLINK
    {
        //MoPlanQty
        [FieldMapAttribute("MOPLANQTY", typeof(decimal), true)]
        public decimal MoPlanQty;

        //LotNo
        [FieldMapAttribute("LOTNO", typeof(string), true)]
        public string LotNo;

        //Item
        [FieldMapAttribute("ITEMCODE", typeof(string), true)]
        public string Item;

        //ItemDesc
        [FieldMapAttribute("ItemDesc", typeof(string), true)]
        public string ItemDesc;

    }
    #endregion

}

