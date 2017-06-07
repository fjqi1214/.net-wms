using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.SopPicture
{
    #region Esoppics
    /// <summary>
    /// TBLESOPPICS
    /// </summary>
    [Serializable, TableMap("TBLESOPPICS", "SERIAL")]
    public class Esoppics : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Esoppics()
        {
        }

        ///<summary>
        ///PICTYPE
        ///</summary>
        [FieldMapAttribute("PICTYPE", typeof(string), 40, false)]
        public string Pictype;

        ///<summary>
        ///SERIAL
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(string), 40, false)]
        public string Serial;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string Itemcode;

        ///<summary>
        ///OPCODE
        ///</summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, false)]
        public string Opcode;

        ///<summary>
        ///PICFULLNAME
        ///</summary>
        [FieldMapAttribute("PICFULLNAME", typeof(string), 200, false)]
        public string Picfullname;

        ///<summary>
        ///PICSEQ
        ///</summary>
        [FieldMapAttribute("PICSEQ", typeof(int), 22, false)]
        public int Picseq;

        ///<summary>
        ///PICTITLE
        ///</summary>
        [FieldMapAttribute("PICTITLE", typeof(string), 200, true)]
        public string Pictitle;

        ///<summary>
        ///PICMEMO
        ///</summary>
        [FieldMapAttribute("PICMEMO", typeof(string), 1000, true)]
        public string Picmemo;

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
