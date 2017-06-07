using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mail;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;

using BenQGuru.Palau.Common;

namespace BenQGuru.eMES.MailUtility
{
    public class MailFacade : MarshalByRefObject
    {
        private FacadeHelper _helper = null;
        private IDomainDataProvider _domainDataProvider = null;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                }

                return _domainDataProvider;
            }
        }

        public MailFacade()
        {
        }

        public MailFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        #region Mail

        public Mail CreateNewMail()
        {
            return new Mail();
        }

        public void AddMail(Mail mail)
        {
            this._helper.AddDomainObject(mail);
        }

        public void DeleteMail(Mail mail)
        {
            this._helper.DeleteDomainObject(mail);
        }

        public void UpdateMail(Mail mail)
        {
            this._helper.UpdateDomainObject(mail);
        }

        public object GetMail(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(Mail), new object[] { serial });
        }

        public static void SendMail(string currentUserMail, object[] users, MailMessage mailMessage)
        {
            BenQGuru.eMES.Web.Helper.ESmtpMail smtp = new BenQGuru.eMES.Web.Helper.ESmtpMail();
            for (int i = 0; i < users.Length; i++)
            {
                string eMail = (users[i] as User).UserEmail;
                if (eMail.Trim().Length == 0) continue;

                smtp.AddRecipient(eMail);
            }
            smtp.MailDomain = System.Configuration.ConfigurationSettings.AppSettings["MailDomain"].ToString();
            smtp.MailServerUserName = System.Configuration.ConfigurationSettings.AppSettings["MailServerUserName"].ToString();
            smtp.MailServerPassWord = CommonFunction.DESDeCrypt(System.Configuration.ConfigurationSettings.AppSettings["MailServerPassWord"].ToString());
            smtp.Subject = mailMessage.Subject;
            smtp.Body = mailMessage.Body;
            smtp.From = currentUserMail;
            smtp.FromName = "MESϵͳ";
            smtp.Html = true;
            smtp.Send();
        }

        #endregion
    }
}
