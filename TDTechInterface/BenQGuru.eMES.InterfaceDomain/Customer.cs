using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.InterfaceDomain
{
    #region I_Sapcustomer--客户中间表
    /// <summary>
    /// I_SAPCUSTOMER--客户中间表
    /// </summary>
    [Serializable, TableMap("I_SAPCUSTOMER", "ID")]
    public class I_Sapcustomer : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public I_Sapcustomer()
        {
        }

        ///<summary>
        ///子增列
        ///</summary>
        [FieldMapAttribute("ID", typeof(int), 22, false)]
        public int Id;

        ///<summary>
        ///客户编号
        ///</summary>
        [FieldMapAttribute("CUSTOMERCODE", typeof(string), 10, false)]
        public string Customercode;

        ///<summary>
        ///客户名称
        ///</summary>
        [FieldMapAttribute("CUSTOMERNAME", typeof(string), 280, true)]
        public string Customername;

        ///<summary>
        ///客户地址
        ///</summary>
        [FieldMapAttribute("ADDRESS", typeof(string), 140, true)]
        public string Address;

        ///<summary>
        ///客户电话
        ///</summary>
        [FieldMapAttribute("TEL", typeof(string), 64, true)]
        public string Tel;

        ///<summary>
        ///封存标识
        ///</summary>
        [FieldMapAttribute("FLAG", typeof(string), 8, true)]
        public string Flag;

        ///<summary>
        ///MES处理标识
        ///</summary>
        [FieldMapAttribute("MESFLAG", typeof(string), 1, true)]
        public string Mesflag;

        ///<summary>
        ///同步日期
        ///</summary>
        [FieldMapAttribute("SDATE", typeof(int), 22, false)]
        public int Sdate;

        ///<summary>
        ///同步时间
        ///</summary>
        [FieldMapAttribute("STIME", typeof(int), 22, false)]
        public int Stime;

        ///<summary>
        ///处理日期
        ///</summary>
        [FieldMapAttribute("PDATE", typeof(int), 22, false)]
        public int Pdate;

        ///<summary>
        ///处理时间
        ///</summary>
        [FieldMapAttribute("PTIME", typeof(int), 22, false)]
        public int Ptime;

        ///<summary>
        ///预留字段1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 160, true)]
        public string Eattribute1;

        ///<summary>
        ///预留字段2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 160, true)]
        public string Eattribute2;

        ///<summary>
        ///预留字段3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 160, true)]
        public string Eattribute3;

    }
    #endregion

    #region Customer--客户中间表
    /// <summary>
    /// TBLCUSTOMER--客户中间表
    /// </summary>
    [Serializable, TableMap("TBLCUSTOMER", "CUSTOMERCODE")]
    public class Customer : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Customer()
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
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///封存标识
        ///</summary>
        [FieldMapAttribute("FLAG", typeof(string), 8, true)]
        public string Flag;

        ///<summary>
        ///客户电话
        ///</summary>
        [FieldMapAttribute("TEL", typeof(string), 64, true)]
        public string Tel;

        ///<summary>
        ///客户地址
        ///</summary>
        [FieldMapAttribute("ADDRESS", typeof(string), 140, true)]
        public string Address;

        ///<summary>
        ///客户名称
        ///</summary>
        [FieldMapAttribute("CUSTOMERNAME", typeof(string), 280, true)]
        public string Customername;

        ///<summary>
        ///客户编号
        ///</summary>
        [FieldMapAttribute("CUSTOMERCODE", typeof(string), 10, false)]
        public string Customercode;

    }
    #endregion

}
