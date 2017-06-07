using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.InterfaceDomain
{
    #region I_Sapmaterial--物料表
    /// <summary>
    /// I_SAPMATERIAL--物料表
    /// </summary>
    [Serializable, TableMap("I_SAPMATERIAL", "ID")]
    public class I_Sapmaterial : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public I_Sapmaterial()
        {
        }

        ///<summary>
        ///预留字段3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 160, true)]
        public string Eattribute3;

        ///<summary>
        ///预留字段2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 160, true)]
        public string Eattribute2;

        ///<summary>
        ///预留字段1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 160, true)]
        public string Eattribute1;

        ///<summary>
        ///处理时间
        ///</summary>
        [FieldMapAttribute("PTIME", typeof(int), 22, false)]
        public int Ptime;

        ///<summary>
        ///处理日期
        ///</summary>
        [FieldMapAttribute("PDATE", typeof(int), 22, false)]
        public int Pdate;

        ///<summary>
        ///同步时间
        ///</summary>
        [FieldMapAttribute("STIME", typeof(int), 22, false)]
        public int Stime;

        ///<summary>
        ///同步日期
        ///</summary>
        [FieldMapAttribute("SDATE", typeof(int), 22, false)]
        public int Sdate;

        ///<summary>
        ///MES处理标识
        ///</summary>
        [FieldMapAttribute("MESFLAG", typeof(string), 1, true)]
        public string Mesflag;

        ///<summary>
        ///散装物料
        ///</summary>
        [FieldMapAttribute("BULKMATERIAL", typeof(string), 4, true)]
        public string Bulkmaterial;

        ///<summary>
        ///安全库存
        ///</summary>
        [FieldMapAttribute("SAFETYSTOCK", typeof(decimal), 22, true)]
        public decimal Safetystock;

        ///<summary>
        ///特殊采购类
        ///</summary>
        [FieldMapAttribute("SPECIALPROCYREMENT", typeof(string), 8, true)]
        public string Specialprocyrement;

        ///<summary>
        ///舍入值
        ///</summary>
        [FieldMapAttribute("ROUNDINGVALUE", typeof(decimal), 22, true)]
        public decimal Roundingvalue;

        ///<summary>
        ///最大批量大小
        ///</summary>
        [FieldMapAttribute("MINIMUMLOTSIZE", typeof(decimal), 22, true)]
        public decimal Minimumlotsize;

        ///<summary>
        ///MRP控制者
        ///</summary>
        [FieldMapAttribute("MRPCONTORLLER", typeof(string), 12, true)]
        public string Mrpcontorller;

        ///<summary>
        ///再订货点
        ///</summary>
        [FieldMapAttribute("REORDERPOINT", typeof(decimal), 22, true)]
        public decimal Reorderpoint;

        ///<summary>
        ///MRP类型
        ///</summary>
        [FieldMapAttribute("MRPTYPE", typeof(string), 8, true)]
        public string Mrptype;

        ///<summary>
        ///ABC标识ABC Indicator
        ///</summary>
        [FieldMapAttribute("ABCINDICATOR", typeof(string), 4, true)]
        public string Abcindicator;

        ///<summary>
        ///采购组Purchasing Group
        ///</summary>
        [FieldMapAttribute("PURCHASINGGROUP", typeof(string), 12, true)]
        public string Purchasinggroup;

        ///<summary>
        ///CD用途
        ///</summary>
        [FieldMapAttribute("CDFOR", typeof(string), 16, true)]
        public string Cdfor;

        ///<summary>
        ///CD数量
        ///</summary>
        [FieldMapAttribute("CDQTY", typeof(string), 528, true)]
        public string Cdqty;

        ///<summary>
        ///项目类别组ItemCatGroup
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE8", typeof(string), 16, true)]
        public string Eattribute8;

        ///<summary>
        ///销售有效开始日期,valid from 销售状态
        ///</summary>
        [FieldMapAttribute("VALIDFROM", typeof(int), 22, true)]
        public int Validfrom;

        ///<summary>
        ///物料状态字段3,跨分销链物料状态
        ///</summary>
        [FieldMapAttribute("MSTATE3", typeof(string), 8, true)]
        public string Mstate3;

        ///<summary>
        ///物料状态字段2,跨工厂物料状态
        ///</summary>
        [FieldMapAttribute("MSTATE2", typeof(string), 8, true)]
        public string Mstate2;

        ///<summary>
        ///物料状态字段1,在客户级标记要删除的物料,X=删除, 空=未删除
        ///</summary>
        [FieldMapAttribute("MSTATE1", typeof(string), 4, true)]
        public string Mstate1;

        ///<summary>
        ///物料类型SAP MATERIAL TYPE
        ///</summary>
        [FieldMapAttribute("MATERIALTYPE", typeof(string), 16, true)]
        public string Materialtype;

        ///<summary>
        ///物料组(品类)
        ///</summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 36, true)]
        public string Modelcode;

        ///<summary>
        ///物料中文长描述
        ///</summary>
        [FieldMapAttribute("MCHLONGDESC", typeof(string), 400, true)]
        public string Mchlongdesc;

        ///<summary>
        ///物料英文长描述
        ///</summary>
        [FieldMapAttribute("MENLONGDESC", typeof(string), 400, true)]
        public string Menlongdesc;

        ///<summary>
        ///物料英文短描述
        ///</summary>
        [FieldMapAttribute("MENSHORTDESC", typeof(string), 160, true)]
        public string Menshortdesc;

        ///<summary>
        ///物料中文短描述
        ///</summary>
        [FieldMapAttribute("MCHSHORTDESC", typeof(string), 160, true)]
        public string Mchshortdesc;

        ///<summary>
        ///环保标志(Y, N)
        ///</summary>
        [FieldMapAttribute("ROHS", typeof(string), 1, true)]
        public string Rohs;

        ///<summary>
        ///单位
        ///</summary>
        [FieldMapAttribute("MUOM", typeof(string), 3, true)]
        public string Muom;

        ///<summary>
        ///鼎桥物料编码
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 88, true)]
        public string Dqmcode;

        ///<summary>
        ///物料编码
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 18, false)]
        public string Mcode;

        ///<summary>
        ///自增列
        ///</summary>
        [FieldMapAttribute("ID", typeof(int), 22, false)]
        public int Id;

    }
    #endregion

    #region Material--物料表
    /// <summary>
    /// TBLMATERIAL--物料表
    /// </summary>
    [Serializable, TableMap("TBLMATERIAL", "MCODE")]
    public class Material : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Material()
        {
        }

        ///<summary>
        ///预留字段3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 160, true)]
        public string Eattribute3;

        ///<summary>
        ///预留字段2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 160, true)]
        public string Eattribute2;

        ///<summary>
        ///预留字段1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 160, true)]
        public string Eattribute1;

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
        public int Ctime;

        ///<summary>
        ///创建日期 
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int Cdate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string Cuser;

        ///<summary>
        ///散装物料
        ///</summary>
        [FieldMapAttribute("BULKMATERIAL", typeof(string), 4, true)]
        public string Bulkmaterial;

        ///<summary>
        ///安全库存
        ///</summary>
        [FieldMapAttribute("SAFETYSTOCK", typeof(decimal), 22, true)]
        public decimal Safetystock;

        ///<summary>
        ///特殊采购类
        ///</summary>
        [FieldMapAttribute("SPECIALPROCYREMENT", typeof(string), 8, true)]
        public string Specialprocyrement;

        ///<summary>
        ///舍入值
        ///</summary>
        [FieldMapAttribute("ROUNDINGVALUE", typeof(decimal), 22, true)]
        public decimal Roundingvalue;

        ///<summary>
        ///最大批量大小
        ///</summary>
        [FieldMapAttribute("MINIMUMLOTSIZE", typeof(decimal), 22, true)]
        public decimal Minimumlotsize;

        ///<summary>
        ///MRP控制者
        ///</summary>
        [FieldMapAttribute("MRPCONTORLLER", typeof(string), 12, true)]
        public string Mrpcontorller;

        ///<summary>
        ///再订货点
        ///</summary>
        [FieldMapAttribute("REORDERPOINT", typeof(decimal), 22, true)]
        public decimal Reorderpoint;

        ///<summary>
        ///MRP类型
        ///</summary>
        [FieldMapAttribute("MRPTYPE", typeof(string), 8, true)]
        public string Mrptype;

        ///<summary>
        ///ABC标识ABC Indicator
        ///</summary>
        [FieldMapAttribute("ABCINDICATOR", typeof(string), 4, true)]
        public string Abcindicator;

        ///<summary>
        ///采购组Purchasing Group
        ///</summary>
        [FieldMapAttribute("PURCHASINGGROUP", typeof(string), 12, true)]
        public string Purchasinggroup;

        ///<summary>
        ///CD用途
        ///</summary>
        [FieldMapAttribute("CDFOR", typeof(string), 16, true)]
        public string Cdfor;

        ///<summary>
        ///CD数量
        ///</summary>
        [FieldMapAttribute("CDQTY", typeof(string), 528, true)]
        public string Cdqty;

        ///<summary>
        ///项目类别组ItemCatGroup
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE8", typeof(string), 16, true)]
        public string Eattribute8;

        ///<summary>
        ///销售有效开始日期,valid from 销售状态
        ///</summary>
        [FieldMapAttribute("VALIDFROM", typeof(int), 22, true)]
        public int Validfrom;

        ///<summary>
        ///物料状态字段3,跨分销链物料状态
        ///</summary>
        [FieldMapAttribute("MSTATE3", typeof(string), 8, true)]
        public string Mstate3;

        ///<summary>
        ///物料状态字段2,跨工厂物料状态
        ///</summary>
        [FieldMapAttribute("MSTATE2", typeof(string), 8, true)]
        public string Mstate2;

        ///<summary>
        ///物料状态字段1,在客户级标记要删除的物料,X=删除, 空=未删除
        ///</summary>
        [FieldMapAttribute("MSTATE1", typeof(string), 4, true)]
        public string Mstate1;

        ///<summary>
        ///物料类型SAP MATERIAL TYPE
        ///</summary>
        [FieldMapAttribute("MATERIALTYPE", typeof(string), 16, true)]
        public string Materialtype;

        ///<summary>
        ///有效期起算时间
        ///</summary>
        [FieldMapAttribute("VALIDITY", typeof(int), 22, true)]
        public int Validity;

        ///<summary>
        ///物料来源MES-->MES特殊物料管理入库单增加的特殊物料SAP-->SAP同步过来的
        ///</summary>
        [FieldMapAttribute("SOURCEFLAG", typeof(string), 3, false)]
        public string Sourceflag;

        ///<summary>
        ///物料状态0，有效；1，失效；
        ///</summary>
        [FieldMapAttribute("MSTATE", typeof(int), 22, true)]
        public int Mstate;

        ///<summary>
        ///物料组(品类)
        ///</summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 36, true)]
        public string Modelcode;

        ///<summary>
        ///特殊物料描述
        ///</summary>
        [FieldMapAttribute("MSPECIALDESC", typeof(string), 800, true)]
        public string Mspecialdesc;

        ///<summary>
        ///物料中文长描述
        ///</summary>
        [FieldMapAttribute("MCHLONGDESC", typeof(string), 400, true)]
        public string Mchlongdesc;

        ///<summary>
        ///物料英文长描述
        ///</summary>
        [FieldMapAttribute("MENLONGDESC", typeof(string), 400, true)]
        public string Menlongdesc;

        ///<summary>
        ///物料英文短描述
        ///</summary>
        [FieldMapAttribute("MENSHORTDESC", typeof(string), 160, true)]
        public string Menshortdesc;

        ///<summary>
        ///物料中文短描述
        ///</summary>
        [FieldMapAttribute("MCHSHORTDESC", typeof(string), 160, true)]
        public string Mchshortdesc;

        ///<summary>
        ///环保标志(Y, N)
        ///</summary>
        [FieldMapAttribute("ROHS", typeof(string), 1, true)]
        public string Rohs;

        ///<summary>
        ///控管类型NULL-默认,item_control_keyparts, item_control_lot,item_control_nocontrol
        ///</summary>
        [FieldMapAttribute("MCONTROLTYPE", typeof(string), 40, true)]
        public string Mcontroltype;

        ///<summary>
        ///物料类型itemtype_finishedproduct-产成品itemtype_semimanufacture-半成品itemtype_rawmaterial-原材料
        ///</summary>
        [FieldMapAttribute("MTYPE", typeof(string), 40, true)]
        public string Mtype;

        ///<summary>
        ///单位
        ///</summary>
        [FieldMapAttribute("MUOM", typeof(string), 3, true)]
        public string Muom;

        ///<summary>
        ///鼎桥物料编码
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 88, true)]
        public string Dqmcode;

        ///<summary>
        ///物料编码
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 18, false)]
        public string Mcode;

    }
    #endregion

}
