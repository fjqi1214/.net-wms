using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain
{  

    #region Esoppicsng
    /// <summary>
    /// TBLESOPPICSNG
    /// </summary>
    [Serializable, TableMap("TBLESOPPICSNG", "SERIAL")]
    public class Esoppicsng : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Esoppicsng()
        {
        }

        ///<summary>
        ///SERIAL
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(string), 40, false)]
        public string Serial;

        ///<summary>
        ///RCARD
        ///</summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string Rcard;

        ///<summary>
        ///TSID
        ///</summary>
        [FieldMapAttribute("TSID", typeof(string), 40, false)]
        public string Tsid;

        ///<summary>
        ///PICSNAME
        ///</summary>
        [FieldMapAttribute("PICSNAME", typeof(string), 200, false)]
        public string Picsname;

        ///<summary>
        ///NGPICMEMO
        ///</summary>
        [FieldMapAttribute("NGPICMEMO", typeof(string), 1000, true)]
        public string Ngpicmemo;

        ///<summary>
        ///EATTRIBUTE1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;

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
}
