using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.InterfaceDomain
{
    #region I_Sapstorageinfo--SAP库存信息中间表
    /// <summary>
    /// I_SAPSTORAGEINFO--SAP库存信息中间表
    /// </summary>
    [Serializable, TableMap("I_SAPSTORAGEINFO", "ID")]
    public class I_Sapstorageinfo : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public I_Sapstorageinfo()
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
        ///单位
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 3, true)]
        public string Unit;

        ///<summary>
        ///冻结退货数量-BLOCKED STOCK RETURNS
        ///</summary>
        [FieldMapAttribute("FREEZERETURNQTY", typeof(decimal), 22, true)]
        public decimal Freezereturnqty;

        ///<summary>
        ///在途数量-STOCK IN TRANSFER
        ///</summary>
        [FieldMapAttribute("TRANSITQTY", typeof(decimal), 22, true)]
        public decimal Transitqty;

        ///<summary>
        ///冻结数量-BLOCK STOCK
        ///</summary>
        [FieldMapAttribute("FREEZEQTY", typeof(decimal), 22, true)]
        public decimal Freezeqty;

        ///<summary>
        ///质检数量-STOCK IN QUALITY INSPECTION
        ///</summary>
        [FieldMapAttribute("QUALITYQTY", typeof(decimal), 22, true)]
        public decimal Qualityqty;

        ///<summary>
        ///可用数量-UNRESTRICTED-USE STOCK
        ///</summary>
        [FieldMapAttribute("AVAILABLEQTY", typeof(decimal), 22, true)]
        public decimal Availableqty;

        ///<summary>
        ///特殊库存编号
        ///</summary>
        [FieldMapAttribute("TYPENO", typeof(string), 24, true)]
        public string Typeno;

        ///<summary>
        ///特殊库存标示，空=库位库存；E=销售订单库存；O=委外供应商库存
        ///</summary>
        [FieldMapAttribute("TYPE", typeof(string), 1, true)]
        public string Type;

        ///<summary>
        ///工厂
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 4, false)]
        public string Faccode;

        ///<summary>
        ///库位代码
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 4, true)]
        public string Storagecode;

        ///<summary>
        ///物料编号
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 18, false)]
        public string Mcode;

        ///<summary>
        ///Id
        ///</summary>
        [FieldMapAttribute("ID", typeof(int), 22, false)]
        public int Id;

    }
    #endregion

    #region Sapstorageinfo--SAP库存信息表
    /// <summary>
    /// TBLSAPSTORAGEINFO--SAP库存信息表
    /// </summary>
    [Serializable, TableMap("TBLSAPSTORAGEINFO", "")]
    public class Sapstorageinfo : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Sapstorageinfo()
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
        ///单位
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 1, true)]
        public string Unit;

        ///<summary>
        ///冻结退货数量-Blocked Stock Returns
        ///</summary>
        [FieldMapAttribute("FREEZERETURNQTY", typeof(decimal), 22, true)]
        public decimal Freezereturnqty;

        ///<summary>
        ///在途数量-stock in transfer
        ///</summary>
        [FieldMapAttribute("TRANSITQTY", typeof(decimal), 22, true)]
        public decimal Transitqty;

        ///<summary>
        ///冻结数量-block stock
        ///</summary>
        [FieldMapAttribute("FREEZEQTY", typeof(decimal), 22, true)]
        public decimal Freezeqty;

        ///<summary>
        ///质检数量-stock in quality inspection
        ///</summary>
        [FieldMapAttribute("QUALITYQTY", typeof(decimal), 22, true)]
        public decimal Qualityqty;

        ///<summary>
        ///可用数量-unrestricted-use stock
        ///</summary>
        [FieldMapAttribute("AVAILABLEQTY", typeof(decimal), 22, true)]
        public decimal Availableqty;

        ///<summary>
        ///特殊库存编号
        ///</summary>
        [FieldMapAttribute("TYPENO", typeof(string), 24, true)]
        public string Typeno;

        ///<summary>
        ///特殊库存标示，空=库位库存；E=销售订单库存；O=委外供应商库存
        ///</summary>
        [FieldMapAttribute("TYPE", typeof(string), 1, true)]
        public string Type;

        ///<summary>
        ///工厂
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 4, false)]
        public string Faccode;

        ///<summary>
        ///库位代码
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 4, false)]
        public string Storagecode;

        ///<summary>
        ///物料编号
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 18, false)]
        public string Mcode;

    }
    #endregion
}
