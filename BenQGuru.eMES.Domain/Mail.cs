using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain
{
    #region Mail

    /// <summary>
    ///	Mail
    /// </summary>
    [Serializable, TableMap("TBLMAIL", "SERIAL")]
    public class Mail : DomainObject
    {
        public Mail()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        ///MailSubject
        ///</summary>	
        [FieldMapAttribute("MAILSUBJECT", typeof(string), 150, false)]
        public string MailSubject;

        ///<summary>
        ///Recipients
        ///</summary>	
        [FieldMapAttribute("RECIPIENTS", typeof(string), 4000, false)]
        public string Recipients;

        ///<summary>
        ///MailContent
        ///</summary>	
        [FieldMapAttribute("MAILCONTENT", typeof(string), 4000, false)]
        public string MailContent;

        ///<summary>
        ///IsSend
        ///</summary>	
        [FieldMapAttribute("ISSEND", typeof(string), 1, false)]
        public string IsSend;

        ///<summary>
        ///SendTimes
        ///</summary>	
        [FieldMapAttribute("SENDTIMES", typeof(int), 38, false)]
        public int SendTimes;

        ///<summary>
        ///SendResult
        ///</summary>	
        [FieldMapAttribute("SENDRESULT", typeof(string), 40, false)]
        public string SendResult;

        ///<summary>
        ///ErrorMessage
        ///</summary>	
        [FieldMapAttribute("ERRORMESSAGE", typeof(string), 2000, true)]
        public string ErrorMessage;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        ///<summary>
        ///EAttribute1
        ///</summary>	
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 100, true)]
        public string EAttribute1;

        ///<summary>
        ///EAttribute2
        ///</summary>	
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 100, true)]
        public string EAttribute2;

        ///<summary>
        ///EAttribute3
        ///</summary>	
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 100, true)]
        public string EAttribute3;

        ///<summary>
        ///IsSendToPhone
        ///</summary>	
        [FieldMapAttribute("ISSENDTOPHONE", typeof(string), 1, false)]
        public string IsSendToPhone;

        ///<summary>
        ///PHONESENDRESULT
        ///</summary>	
        [FieldMapAttribute("PHONESENDRESULT", typeof(string), 40, false)]
        public string PhoneSendResult;

    }

    #endregion
}
