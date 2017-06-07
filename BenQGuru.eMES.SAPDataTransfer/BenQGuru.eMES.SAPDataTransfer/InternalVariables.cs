using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BenQGuru.eMES.SAPDataTransfer
{
    internal static class InternalVariables
    {       
        /// <summary>
        /// Organization ID
        /// </summary>
        internal static int MS_OrganizationID = System.Configuration.ConfigurationManager.AppSettings["OrgID"] == null ? 0 : int.Parse(System.Configuration.ConfigurationManager.AppSettings["OrgID"]);

        /// <summary>
        /// Max Days of Period
        /// </summary>
        internal static int MS_MaxDaysOfPeriod = System.Configuration.ConfigurationManager.AppSettings["MaxDaysOfPeriod"] == null ? 1 : int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxDaysOfPeriod"]);

        /// <summary>
        /// Max Mo Count of per request
        /// </summary>
        internal static int MS_MaxMOPerRequest = System.Configuration.ConfigurationManager.AppSettings["MaxMoPerRequest"] == null ? 1 : int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxMoPerRequest"]);

        /// <summary>
        /// day offset for begin & end date
        /// </summary>
        internal static int MS_DateOffSet = System.Configuration.ConfigurationManager.AppSettings["DateOffSet"] == null ? 0 : int.Parse(System.Configuration.ConfigurationManager.AppSettings["DateOffSet"]);

        /// <summary>
        /// WebService TimeOut
        /// </summary>
        internal static int MS_TimeOut = System.Configuration.ConfigurationManager.AppSettings["WebServiceTimeOut"] == null ? 120 : int.Parse(System.Configuration.ConfigurationManager.AppSettings["WebServiceTimeOut"]);

        /// <summary>
        /// XML File Path
        /// </summary>
        internal static string MS_XMLFilePath = System.Configuration.ConfigurationManager.AppSettings["XMLPath"];

        /// <summary>
        /// ReTry Times when timeout
        /// </summary>
        internal static int MS_ReTryTimes = System.Configuration.ConfigurationManager.AppSettings["ReTryTimes"] == null ? 3 : int.Parse(System.Configuration.ConfigurationManager.AppSettings["ReTryTimes"]);

        /// <summary>
        /// Interval between every two retry events
        /// </summary>
        internal static int MS_ReTryInterval = System.Configuration.ConfigurationManager.AppSettings["ReTryInterval"] == null ? 10 : int.Parse(System.Configuration.ConfigurationManager.AppSettings["ReTryInterval"]);

        internal static string MatchItemCode(string itemCode)
        {
            return Regex.Replace(itemCode, "^0*", "");
        }

        internal static string SubStringX(string input)
        {
            if (input.Trim().Length == 0)
            {
                return input;
            }

            string lastChar = input.Substring(input.Length - 1, 1);
            Encoding encode = Encoding.GetEncoding("gb18030");

            int byteLength = encode.GetByteCount(lastChar);

            if (byteLength == 1)
            {
                // English or Number or semi Chinese
                byte[] temp = encode.GetBytes(lastChar);
                if (temp[0] < 128)
                {
                    return input;
                }
                else
                {
                    return input.Substring(0, input.Length - 1);
                }
            }
            else if (byteLength == 4)
            {
                byte[] temp = encode.GetBytes(lastChar);
                if (temp[0] < 128)
                {
                    return input;
                }
                else
                {
                    return input.Substring(0, input.Length - 1);
                }
            }
            else
            {
                return input;
            }
        }
    }
}
