using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Common.DCT.PC
{
    [Serializable, TableMap("TBLDCTMESSAGE", "SERIALNO")]
    public class DCTMessage : DomainObject
    {
        public DCTMessage()
        {
        }

        [FieldMapAttribute("SERIALNO", typeof(int), 8, false)]
        public int SerialNo;

        [FieldMapAttribute("FROMADDRESS", typeof(string), 40, false)]
        public string FromAddress;

        [FieldMapAttribute("FROMPORT", typeof(int), 8, false)]
        public int FromPort;

        [FieldMapAttribute("TOADDRESS", typeof(string), 40, false)]
        public string ToAddress;

        [FieldMapAttribute("TOPORT", typeof(int), 8, false)]
        public int ToPort;

        [FieldMapAttribute("MESSAGETYPE", typeof(string), 40, false)]
        public string MessageType;

        [FieldMapAttribute("DIRECTION", typeof(string), 40, false)]
        public string Direction;

        [FieldMapAttribute("MESSAGECONTENT", typeof(string), 1000, true)]
        public string MessageContent;

        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;
    }
}
