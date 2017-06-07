using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.InterfaceDomain
{
    #region I_Sapstorage--库位中间表
    /// <summary>
    /// I_SAPSTORAGE--库位中间表
    /// </summary>
    [Serializable, TableMap("I_SAPSTORAGE", "ID")]
    public class I_Sapstorage : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public I_Sapstorage()
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
        ///联系人4
        ///</summary>
        [FieldMapAttribute("CONTACTUSER4", typeof(string), 400, true)]
        public string Contactuser4;

        ///<summary>
        ///联系人3
        ///</summary>
        [FieldMapAttribute("CONTACTUSER3", typeof(string), 400, true)]
        public string Contactuser3;

        ///<summary>
        ///联系人2
        ///</summary>
        [FieldMapAttribute("CONTACTUSER2", typeof(string), 400, true)]
        public string Contactuser2;

        ///<summary>
        ///联系人1
        ///</summary>
        [FieldMapAttribute("CONTACTUSER1", typeof(string), 400, true)]
        public string Contactuser1;

        ///<summary>
        ///地址4
        ///</summary>
        [FieldMapAttribute("ADDRESS4", typeof(string), 640, true)]
        public string Address4;

        ///<summary>
        ///地址3
        ///</summary>
        [FieldMapAttribute("ADDRESS3", typeof(string), 640, true)]
        public string Address3;

        ///<summary>
        ///地址2
        ///</summary>
        [FieldMapAttribute("ADDRESS2", typeof(string), 640, true)]
        public string Address2;

        ///<summary>
        ///地址1
        ///</summary>
        [FieldMapAttribute("ADDRESS1", typeof(string), 640, true)]
        public string Address1;

        ///<summary>
        ///SAP库位名称
        ///</summary>
        [FieldMapAttribute("STORAGENAME", typeof(string), 64, true)]
        public string Storagename;

        ///<summary>
        ///SAP库位代码
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 4, false)]
        public string Storagecode;

        ///<summary>
        ///自增列
        ///</summary>
        [FieldMapAttribute("ID", typeof(int), 22, false)]
        public int Id;

    }
    #endregion

    #region Storage--库位表
    /// <summary>
    /// TBLSTORAGE--库位表
    /// </summary>
    [Serializable, TableMap("TBLSTORAGE", "STORAGECODE,ORGID")]
    public class Storage : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Storage()
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
        [FieldMapAttribute("MTIME", typeof(int), 22, true)]
        public int MaintainTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, true)]
        public int MaintainDate;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        ///<summary>
        ///库位来源(MES, SAP)
        ///</summary>
        [FieldMapAttribute("SOURCEFLAG", typeof(string), 3, false)]
        public string Sourceflag;

        ///<summary>
        ///虚拟库位标识(Y:虚拟库位; N:非虚拟库位)
        ///</summary>
        [FieldMapAttribute("VIRTUALFLAG", typeof(string), 1, false)]
        public string Virtualflag;

        ///<summary>
        ///联系人4
        ///</summary>
        [FieldMapAttribute("CONTACTUSER4", typeof(string), 400, true)]
        public string Contactuser4;

        ///<summary>
        ///联系人3
        ///</summary>
        [FieldMapAttribute("CONTACTUSER3", typeof(string), 400, true)]
        public string Contactuser3;

        ///<summary>
        ///联系人2
        ///</summary>
        [FieldMapAttribute("CONTACTUSER2", typeof(string), 400, true)]
        public string Contactuser2;

        ///<summary>
        ///联系人1
        ///</summary>
        [FieldMapAttribute("CONTACTUSER1", typeof(string), 400, true)]
        public string Contactuser1;

        ///<summary>
        ///地址4
        ///</summary>
        [FieldMapAttribute("ADDRESS4", typeof(string), 640, true)]
        public string Address4;

        ///<summary>
        ///地址3
        ///</summary>
        [FieldMapAttribute("ADDRESS3", typeof(string), 640, true)]
        public string Address3;

        ///<summary>
        ///地址2
        ///</summary>
        [FieldMapAttribute("ADDRESS2", typeof(string), 640, true)]
        public string Address2;

        ///<summary>
        ///地址1
        ///</summary>
        [FieldMapAttribute("ADDRESS1", typeof(string), 640, true)]
        public string Address1;

        ///<summary>
        ///库存属性
        ///</summary>
        [FieldMapAttribute("SPROPERTY", typeof(string), 160, true)]
        public string Sproperty;

        ///<summary>
        ///SAP库位名称
        ///</summary>
        [FieldMapAttribute("STORAGENAME", typeof(string), 64, true)]
        public string Storagename;

        ///<summary>
        ///SAP库位代码
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 4, false)]
        public string Storagecode;

        ///<summary>
        ///组织ID
        ///</summary>
        [FieldMapAttribute("ORGID", typeof(int), 22, false)]
        public int Orgid;

    }
    #endregion


}
