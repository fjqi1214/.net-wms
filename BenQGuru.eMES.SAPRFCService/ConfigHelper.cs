using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace BenQGuru.eMES.SAPRFCService
{
    public static class ConfigHelper
    {
        public const string strDestinationName = "Q97";
        public static string[] LoadRFCConfig(string destinationName)
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

    }
}
