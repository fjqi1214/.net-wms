using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ControlLibrary.Web.Language;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.MOModel
{
    public class MOModelPublic
    {
        public static string TranslateParseType(string parseType, LanguageComponent languageComponent1)
        {
            string returnValue = string.Empty;

            string[] parseTypeList = parseType.Trim().ToLower().Split(new char[] { ',' });

            if (parseTypeList != null)
            {
                foreach (string part in parseTypeList)
                {
                    returnValue += "," + languageComponent1.GetString(part.Trim().ToLower());

                    //if (part == OPBOMDetailParseType.PARSE_PRODUCT && checkStatus)
                    //{
                    //    returnValue += "(" + languageComponent1.GetString("parse_product_checkstatus") + ")";
                    //}
                }
            }
            if (returnValue.Length > 0)
                returnValue = returnValue.Substring(1);

            return returnValue;
        }

        public static string TranslateCheckType(string checkType, LanguageComponent languageComponent1)
        {
            string returnValue = string.Empty;

            string[] checkTypeList = checkType.Trim().ToLower().Split(new char[] { ',' });

            if (checkTypeList != null)
            {
                foreach (string part in checkTypeList)
                {
                    returnValue += "," + languageComponent1.GetString(part.Trim().ToLower());
                }
            }
            if (returnValue.Length > 0)
                returnValue = returnValue.Substring(1);

            return returnValue;
        }
    }
}
