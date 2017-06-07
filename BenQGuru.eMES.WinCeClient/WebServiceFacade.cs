using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

namespace BenQGuru.eMES.WinCeClient
{
    public class WebServiceFacade
    {
        OpenService.Login loginFacade = new BenQGuru.eMES.WinCeClient.OpenService.Login();

        public WebServiceFacade()
        {
            SetWebService("OpenService.asmx");
        }

        //配置Web Service
        private void SetWebService(string asmxName)
        {
            string webServiceURL = string.Empty;
            webServiceURL = GetWebServiceURL();

            if (string.IsNullOrEmpty(webServiceURL)==false)
            {
                loginFacade.Url = webServiceURL+asmxName;
            }
        }

        //获取Web Service的URL
        public static string GetWebServiceURL()
        {
            //kelvin
            string cePath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            cePath = cePath.Substring(0, cePath.LastIndexOf(@"\"));
            string path = cePath + "\\WinCeClientConfig.xml";

            //string path = @".\WinCeClientConfig.xml";

            string returnString = string.Empty;
            //string path = this.GetType().Assembly.ManifestModule.FullyQualifiedName;
            //path = path.Replace("BenQGuru.eMES.WinCeClient.exe", "BenQGuru.eMES.WinCeClient.config");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            XmlNode selectNode = xmlDoc.SelectSingleNode("//WebServiceConfig");

            XmlNodeList nodeList = selectNode.ChildNodes;

            foreach (XmlNode node in nodeList)
            {
                XmlElement emt = (XmlElement)node;
                if (emt.Name == "URL")
                {
                    returnString = emt.InnerText;
                }
            }
            return returnString;
        }

        //登陆
        public string Login(string userCode, string passWord, string resCode)
        {
            return loginFacade.UserLogin(userCode, passWord, resCode);
        }

        //获取菜单
        public DataTable GetAllMenuWithUrl(string userCode)
        {
            return loginFacade.GetAllMenuWithUrlWithTypePermission(userCode);
        }
    }
    #region SMTLoadFeederOperationType
    public class SMTLoadFeederOperationType
    {
        public const string Load = "Load";
        public const string Exchange = "Exchange";
        public const string Continue = "Continue";
        public const string UnLoad = "UnLoad";
        public const string TransferMO = "TransferMO";
        public const string UnLoadSingle = "UnLoadSingle";
        public const string MOEnabled = "MOEnabled";
        public const string MODisabled = "MODisabled";
        public const string ReelExhaust = "ReelExhaust";
        public const string LoadCheck = "LoadCheck";
        public const string Effective = "Effective";
        public const string Invalidate = "Invalidate";
    }
    #endregion

    #region SMTLoadFeederInputType
    public class SMTLoadFeederInputType
    {
        public const string Init = "Init";
        public const string MachineCode = "MachineCode";
        public const string StationCode = "StationCode";
        public const string FeederCode = "FeederCode";
        public const string FeederCodeOld = "FeederCodeOld";
        public const string ReelNo = "ReelNo";
        public const string ReelNoOld = "ReelNoOld";
    }
    #endregion
}
