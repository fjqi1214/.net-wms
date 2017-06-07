using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

using BenQGuru.eMES.SAPDataTransferInterface;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Alert;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.AlertModel;

using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Collections;

namespace BenQGuru.eMES.AlertMailService
{
    public class AlertMail : ICommand
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

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

        public AlertMail()
        {
            this._helper = new FacadeHelper(DataProvider);
        }


        private ServiceResult RunSendMail()
        {
            try
            {
                object[] mails = GetSendMails(10);

                if (mails == null || mails.Length == 0)
                {
                    return new ServiceResult(true, "", "");
                }
                foreach (Mail mail in mails)
                {
                    SendMail(mail);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message, "");
            }

            return new ServiceResult(true, "", "");
        }

        private object[] GetSendMails(int maxSendTimes)
        {
            string sql = "SELECT {0} FROM tblmail WHERE (issendtophone='N' or issend = 'N') ";
            if (maxSendTimes > 0)
            {
                sql +="AND sendtimes < {1} ";
            }
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Mail)), maxSendTimes.ToString());

            return this.DataProvider.CustomQuery(typeof(Mail), new SQLCondition(sql));
        }

        private void SaveMail(Mail mail)
        {
            this._helper.UpdateDomainObject(mail);
        }

        private bool SendMail(Mail mail)
        {
            ESmtpMail smtp = new ESmtpMail();

            //modified by leon.li @20130308
            ArrayList arrMails = new ArrayList();
            AlertFacade alertFacade = new AlertFacade(this.DataProvider);

            object[] obj = alertFacade.QueryUserMailByCodes(mail.Recipients);

            if (obj != null && obj.Length > 0)
            {
                foreach (User user in obj)
                {
                    if (arrMails.Contains(user.UserEmail))
                    {
                        continue;
                    }
                    arrMails.Add(user.UserEmail);
                }
            }

            foreach (string mailAddress in arrMails)
            {
                smtp.AddRecipient(mailAddress);
            }
            smtp.MailDomain = InternalVariables.MailServerAddress;
            smtp.MailDomainPort = int.Parse(InternalVariables.MailServerPort);
            smtp.MailServerUserName = InternalVariables.MailSendUserName;
            smtp.MailServerPassWord = InternalVariables.MailSendUserPassword;
            smtp.Subject = mail.MailSubject;
            smtp.Body = mail.MailContent + "<br /><br /><br />" + InternalVariables.MailSignature;
            smtp.From = InternalVariables.MailSendFrom;
            smtp.FromName = "生辉Mes预警";
            smtp.Html = true;

            if (mail.IsSend.Equals("N"))
            {
                if (smtp.Send())
                {
                    //更新邮件信息
                    mail.IsSend = "Y";
                    mail.SendResult = MailSendResult.MailSendResult_OK;
                    mail.ErrorMessage = string.Empty;
                }
                else
                {
                    //更新邮件信息
                    mail.IsSend = "N";
                    mail.SendResult = MailSendResult.MailSendResult_Fail;
                    mail.ErrorMessage = smtp.ErrorMessage;
                }
                mail.SendTimes++;
                SaveMail(mail);
            }

            #region  发短信 added by leon.li @20130308
            if (mail.IsSendToPhone.Equals("N"))
            {
                //短信号码
                ArrayList arrtelNums = new ArrayList();

                obj = alertFacade.QueryUserTelByCodes(mail.Recipients);
                if (obj != null && obj.Length > 0)
                {
                    foreach (User user in obj)
                    {
                        if (arrtelNums.Contains(user.UserTelephone))
                        {
                            continue;
                        }
                        arrtelNums.Add(user.UserTelephone);
                    }
                }

                string usertelNums = string.Join(",", (string[])arrtelNums.ToArray(typeof(string)));
                mail.PhoneSendResult = sendToPhone(usertelNums, mail.MailContent);
                string result = string.Empty;
                try
                {
                    result = mail.PhoneSendResult.Split('&')[0].Substring(7);
                }
                catch
                {
                    result = "20";//异常
                }

                if (result == "0")
                {
                    //更新短信发送情况
                    mail.IsSendToPhone = "Y";
                }
                else
                {
                    mail.IsSendToPhone = "N";
                }
                mail.SendTimes++;
                SaveMail(mail);
            }
            #endregion
            
            return true;
        }

        //added by leon.li @20130308 调用短信的webservice
        private string sendToPhone(string telnums, string message)
        {
            try
            {
                string result = string.Empty; //此处调用发短信的WebService
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString(); ;
            }
        }

        #region ICommand Members

        public ServiceResult Run(RunMethod runMethod)
        {
            return RunSendMail();
        }

        public bool ArgumentValid(ref string returnMessage)
        {
            return true;
        }

        public object GetArguments()
        {
            return null;
        }

        public object NewTransactionCode()
        {
            return null;
        }

        public void SetArguments(object arguments)
        {
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        	//解决多个oracle session问题。Added by Gawain@20140320
        	((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();
        }

        #endregion
    }
}
