using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.Delivery
{
    #region DN

    [Serializable, TableMap("TBLDN", "DNNO,DNLINE")]
    public class DeliveryNote : DomainObject
    {
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

        [FieldMapAttribute("DNNO", typeof(string), 40, false)]
        public string DNCode;

        [FieldMapAttribute("SHIPTOPARTY", typeof(string), 40, false)]
        public string ShipTo;

        [FieldMapAttribute("DNLINE", typeof(string), 40, false)]
        public string DNLine;

        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        [FieldMapAttribute("ITEMDESC", typeof(string), 100, true)]
        public string ItemDescription;

        [FieldMapAttribute("ORGID", typeof(int), 8, true)]
        public int OrganizationID;

        [FieldMapAttribute("FRMSTORAGE", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLSTORAGE", "STORAGECODE", "STORAGENAME")]
        public string FromStorage;

        [FieldMapAttribute("DNQUANTITY", typeof(decimal), 13, true)]
        public decimal DNQuantity;

        [FieldMapAttribute("REALQUANTITY", typeof(decimal), 13, true)]
        public decimal RealQuantity;

        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        [FieldMapAttribute("MOVEMENTTYPE", typeof(string), 40, true)]
        public string MovementType;

        //[FieldMapAttribute("ITEMGRADE", typeof(string), 40, true)]
        //[FieldDisplay(FieldDisplayModifyType.Append, "(SELECT paramalias, paramdesc FROM tblsysparam WHERE paramgroupcode = 'PRODUCTLEVEL')", "PARAMALIAS", "PARAMDESC")]
        //public string ItemGrade;

        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        [FieldMapAttribute("ORDERNO", typeof(string), 40, true)]
        public string OrderNo;

        [FieldMapAttribute("CUSORDERNO", typeof(string), 40, true)]
        public string CustomerOrderNo;

        [FieldMapAttribute("CUSORDERNOTYPE", typeof(string), 40, true)]
        public string CustomerOrderNoType;

        [FieldMapAttribute("TOSTORAGE", typeof(string), 40, true)]        
        public string ToStorage;

        //[FieldMapAttribute("STATUS", typeof(string), 40, true)]
        //public string Status;

        [FieldMapAttribute("DNFROM", typeof(string), 40, true)]
        public string DNFrom;

        [FieldMapAttribute("DNSTATUS", typeof(string), 40, true)]
        public string DNStatus;

        [FieldMapAttribute("RELATEDDOCUMENT", typeof(string), 100, true)]
        public string RelatedDocument;

        [FieldMapAttribute("BUSINESSCODE", typeof(string), 40, true)]
        public string BusinessCode;

        [FieldMapAttribute("DEPT", typeof(string), 100, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "(SELECT paramcode, paramalias FROM tblsysparam WHERE paramgroupcode = 'DEPARTMENT')", "PARAMCODE", "PARAMALIAS")]
        public string Dept;

        [FieldMapAttribute("MEMO", typeof(string), 200, true)]
        public string Memo;

        [FieldMapAttribute("REWORKMOCODE", typeof(string), 40, true)]
        public string ReworkMOCode;

        [FieldMapAttribute("SAPSTORAGE", typeof(string), 40, true)]
        public string SAPStorage;

        [FieldMapAttribute("FLAG", typeof(string), 10, true)]
        public string Flag;

        [FieldMapAttribute("TRANSACTIONCODE", typeof(string), 100, true)]
        public string TransactionCode;

    }

    [Serializable]
    public class DNWithBusinessDesc : DeliveryNote
    {
        [FieldMapAttribute("BUSINESSDESC", typeof(string), 40, true)]
        public string BusinessDesc;
    }
    #endregion

    #region DN2SAP
    [Serializable, TableMap("TBLDN2SAP", "")]
    public class DN2SAP : DomainObject
    {
        [FieldMapAttribute("DNNO", typeof(string), 40, false)]
        public string DNCode;

        [FieldMapAttribute("FLAG", typeof(string), 40, false)]
        public string Flag;

        [FieldMapAttribute("ACTIVE", typeof(string), 40, true)]
        public string Active;

        [FieldMapAttribute("ERRORMESSAGE", typeof(string), 100, true)]
        public string ErrorMessage;

        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;
    }
    #endregion
}
