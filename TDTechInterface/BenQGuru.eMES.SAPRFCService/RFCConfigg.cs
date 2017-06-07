using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using SAP.Middleware.Connector;

namespace BenQGuru.eMES.SAPRFCService
{
    public class RFCConfigg : IDestinationConfiguration
    {
        public RfcConfigParameters GetParameters(string destinationName)
        {
            if ("Q97".Equals(destinationName))
            {
                RfcConfigParameters parms = new RfcConfigParameters();
                string[] rfcArray = LoadRFCConfig(destinationName);
                parms.Add(RfcConfigParameters.Name, rfcArray[0]);
                parms.Add(RfcConfigParameters.MaxPoolSize, rfcArray[1]);
                parms.Add(RfcConfigParameters.IdleTimeout, rfcArray[2]);
                parms.Add(RfcConfigParameters.User, rfcArray[3]);
                parms.Add(RfcConfigParameters.Password, rfcArray[4]);
                parms.Add(RfcConfigParameters.Client, rfcArray[5]);
                parms.Add(RfcConfigParameters.Language, rfcArray[6]);
                parms.Add(RfcConfigParameters.AppServerHost, rfcArray[7]);
                parms.Add(RfcConfigParameters.SystemNumber, rfcArray[8]);
                parms.Add(RfcConfigParameters.SAPRouter, rfcArray[9]);
                return parms;
            }
            else return null;
        }

        public bool ChangeEventsSupported()
        {

            return false;

        }

        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

        #region
        private string[] LoadRFCConfig(string destinationName)
        {
            string rfcConfigInfermation = string.Empty;
            XmlDocument oXmlDoc = new XmlDocument();
            XmlElement oRoot, oNode;
            XmlNodeList nodelist;

            string sPath = AppDomain.CurrentDomain.BaseDirectory;
            if (!sPath.EndsWith("\\"))
            {
                sPath = sPath + "\\";
            }
            if (!File.Exists(sPath + "RFCConfig.xml"))
            {
                throw new Exception("{\"" + sPath + "RFCConfig.xml\" not found or no rights to access}");
            }
            try
            {
                oXmlDoc.Load(sPath + "RFCConfig.xml");
            }
            catch (Exception errConfigFile)
            {
                throw new Exception("{error in \"" + sPath + "RFCConfig.xml" + "\"}" + errConfigFile.Message);
            }
            oRoot = (XmlElement)oXmlDoc.SelectSingleNode("//configuration");
            nodelist = oRoot.GetElementsByTagName("add");

            string name = "";
            string maxPoolSize = "";
            string idleTimeOut = "";
            string user = "";
            string passsd = "";
            string client = "";
            string lang = "";
            string ashost = "";
            string sysnr = "";
            string saprouter = "";

            for (int i = 0; i < nodelist.Count; i++)
            {
                oNode = (XmlElement)nodelist[i];

                name = oNode.GetAttribute("NAME").Trim();

                if (name == destinationName)
                {
                    maxPoolSize = oNode.GetAttribute("MAX_POOL_SIZE").Trim();
                    idleTimeOut = oNode.GetAttribute("IDLE_TIMEOUT").Trim();
                    user = oNode.GetAttribute("USER").Trim();
                    passsd = oNode.GetAttribute("PASSWD").Trim();
                    client = oNode.GetAttribute("CLIENT").Trim();
                    lang = oNode.GetAttribute("LANG").Trim();
                    ashost = oNode.GetAttribute("ASHOST").Trim();
                    sysnr = oNode.GetAttribute("SYSNR").Trim();
                    saprouter = oNode.GetAttribute("SAPROUTER").Trim();
                    rfcConfigInfermation = name + "|" + maxPoolSize + "|" + idleTimeOut + "|" + user + "|" + passsd + "|" + client + "|" + lang + "|" + ashost + "|" + sysnr + "|" + saprouter;
                    string[] rfcConfig = rfcConfigInfermation.Split('|');
                    return rfcConfig;
                }
                else
                {
                    throw new Exception("{\"" + name + "\" not found}");
                }
            }
            return null;
        }
        #endregion
    }
}
