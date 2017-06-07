using System;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.MOModel
{

    //ADD BY Jarvis
    #region ITEM2LOTCHECK
    /// <summary>
    /// TBLITEM2LOTCHECK
    /// </summary>
    [Serializable, TableMap("TBLITEM2LOTCHECK", "ITEMCODE")]
    public class Item2LotCheck : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Item2LotCheck()
        {
        }

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///SNPREFIX
        ///</summary>
        [FieldMapAttribute("SNPREFIX", typeof(string), 40, false)]
        public string SNPrefix;

        ///<summary>
        ///SNLENGTH
        ///</summary>
        [FieldMapAttribute("SNLENGTH", typeof(int), 22, false)]
        public int SNLength;

        ///<summary>
        ///SNCONTENTCHECK
        ///</summary>
        [FieldMapAttribute("SNCONTENTCHECK", typeof(string), 40, false)]
        public string SNContentCheck;

        ///<summary>
        ///CREATETYPE
        ///</summary>
        [FieldMapAttribute("CREATETYPE", typeof(string), 40, false)]
        public string CreateType;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MUser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///EATTRIBUTE1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;
    }
    #endregion

    //ADD BY Jarvis
    #region ITEM2LOTCHECKMP
    /// <summary>
    /// ITEM2LOTCHECKMP
    /// </summary>
    [Serializable]
    public class Item2LotCheckMP : Item2LotCheck
    {
        public Item2LotCheckMP()
        {
        }

        [FieldMapAttribute("ItemDesc", typeof(string), 40, true)]
        public string ItemDesc;
    }
    #endregion

    //ADD BY Jarvis
    #region MO2LOTLINK
    /// <summary>
    /// TBLMO2LOTLINK
    /// </summary>
    [Serializable, TableMap("TBLMO2LOTLINK", "LOTNO,MOCODE")]
    public class MO2LotLink : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public MO2LotLink()
        {
        }

        ///<summary>
        ///MOCODE
        ///</summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///LOTNO
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNo;

        ///<summary>
        ///LOTQTY
        ///</summary>
        [FieldMapAttribute("LOTQTY", typeof(decimal), 15, false)]
        public decimal LotQty;

        ///<summary>
        ///LOTSTATUS
        ///</summary>
        [FieldMapAttribute("LOTSTATUS", typeof(string), 40, false)]
        public string LotStatus;

        ///<summary>
        ///PRINTTIMES
        ///</summary>
        [FieldMapAttribute("PRINTTIMES", typeof(int), 6, false)]
        public int PrintTimes;

        ///<summary>
        ///LASTPRINTUSER
        ///</summary>
        [FieldMapAttribute("LASTPRINTUSER", typeof(string), 40, true)]
        public string LastPrintUser;

        ///<summary>
        ///LASTPRINTDATE
        ///</summary>
        [FieldMapAttribute("LASTPRINTDATE", typeof(int), 8, true)]
        public int LastPrintDate;

        ///<summary>
        ///LASTPRINTTIME
        ///</summary>
        [FieldMapAttribute("LASTPRINTTIME", typeof(int), 6, true)]
        public int LastPrintTime;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MUser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;
    }
    #endregion


}

