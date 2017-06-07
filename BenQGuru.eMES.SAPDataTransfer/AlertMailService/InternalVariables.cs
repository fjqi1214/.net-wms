using System;
using System.Collections.Generic;
using System.Text;

namespace BenQGuru.eMES.AlertMailService
{
    internal static class InternalVariables
    {
        internal static string MailServerAddress = System.Configuration.ConfigurationManager.AppSettings["MailServerAddress"];
        internal static string MailServerPort = System.Configuration.ConfigurationManager.AppSettings["MailServerPort"];
        internal static string MailSendUserName = System.Configuration.ConfigurationManager.AppSettings["MailSendUserName"];
        internal static string MailSendUserPassword = System.Configuration.ConfigurationManager.AppSettings["MailSendUserPassword"];
        internal static string MailSendFrom = System.Configuration.ConfigurationManager.AppSettings["MailSendFrom"];
        internal static string MailSignature = System.Configuration.ConfigurationManager.AppSettings["MailSignature"];
    }
}
