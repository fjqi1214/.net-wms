using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.InterfaceDomain
{
    #region I_Sapvendor--供应商中间表
    /// <summary>
    /// I_SAPVENDOR--供应商中间表
    /// </summary>
    [Serializable, TableMap("I_SAPVENDOR", "ID")]
    public class I_Sapvendor : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public I_Sapvendor()
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
        ///移动电话
        ///</summary>
        [FieldMapAttribute("MOBILENO", typeof(string), 120, true)]
        public string Mobileno;

        ///<summary>
        ///传真
        ///</summary>
        [FieldMapAttribute("FAXNO", typeof(string), 124, true)]
        public string Faxno;

        ///<summary>
        ///供应商地址
        ///</summary>
        [FieldMapAttribute("VENDORADDR", typeof(string), 40, true)]
        public string Vendoraddr;

        ///<summary>
        ///供应商联系人（法人代表）
        ///</summary>
        [FieldMapAttribute("VENDORUSER", typeof(string), 140, true)]
        public string Vendoruser;

        ///<summary>
        ///供应商别名
        ///</summary>
        [FieldMapAttribute("ALIAS", typeof(string), 40, true)]
        public string Alias;

        ///<summary>
        ///供应商名称
        ///</summary>
        [FieldMapAttribute("VENDORNAME", typeof(string), 140, false)]
        public string Vendorname;

        ///<summary>
        ///供应商代码
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 10, false)]
        public string Vendorcode;

        ///<summary>
        ///Id
        ///</summary>
        [FieldMapAttribute("ID", typeof(int), 22, false)]
        public int Id;

    }
    #endregion

    #region Vendor--供应商列表
    /// <summary>
    /// TBLVENDOR--供应商列表
    /// </summary>
    [Serializable, TableMap("TBLVENDOR", "VENDORCODE")]
    public class Vendor : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Vendor()
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
        ///移动电话
        ///</summary>
        [FieldMapAttribute("MOBILENO", typeof(string), 120, true)]
        public string Mobileno;

        ///<summary>
        ///传真
        ///</summary>
        [FieldMapAttribute("FAXNO", typeof(string), 124, true)]
        public string Faxno;

        ///<summary>
        ///供应商地址
        ///</summary>
        [FieldMapAttribute("VENDORADDR", typeof(string), 40, true)]
        public string Vendoraddr;

        ///<summary>
        ///供应商联系人（法人代表）
        ///</summary>
        [FieldMapAttribute("VENDORUSER", typeof(string), 140, true)]
        public string Vendoruser;

        ///<summary>
        ///供应商别名
        ///</summary>
        [FieldMapAttribute("ALIAS", typeof(string), 40, true)]
        public string Alias;

        ///<summary>
        ///供应商名称
        ///</summary>
        [FieldMapAttribute("VENDORNAME", typeof(string), 140, false)]
        public string Vendorname;

        ///<summary>
        ///供应商代码
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 10, false)]
        public string Vendorcode;

    }
    #endregion


}
