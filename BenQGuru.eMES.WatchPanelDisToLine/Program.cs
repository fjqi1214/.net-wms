using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Xml;

namespace BenQGuru.eMES.WatchPanelDisToLine
{
    static class Program
    {
        public static string DBName = string.Empty;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "Domain.xml";
            XmlDocument document = new XmlDocument();
            document.Load(xmlPath);
            XmlNodeList nodes = document.SelectNodes("/DomainSetting/PersistBrokers/PersistBroker");
            foreach (XmlNode item in nodes)
            {
                if (item.Attributes["Default"].Value.Trim().ToUpper() == "TRUE")
                {
                    DBName = item.Attributes["Name"].Value.Trim();
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FWatchPanelDisToLine());
        }
    }
}