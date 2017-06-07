using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class SAPWebServiceEntity
    {
        public SAPWebServiceEntity()
        {

        }

        private string m_Url;

        public string Url
        {
            get { return m_Url; }
            set { m_Url = value; }
        }

        private string m_UserName;

        public string UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }

        private string m_Password;

        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }

        internal static SAPWebServiceEntity Parse(XmlElement element)
        {
            XmlNodeList entityList = element.GetElementsByTagName("WebService");
            XmlNode node = entityList[0];
            return Node2WebServiceEntity(node);   
        }

        private static SAPWebServiceEntity Node2WebServiceEntity(XmlNode node)
        {
            SAPWebServiceEntity entity = new SAPWebServiceEntity();
            entity.m_Url = node.Attributes["Url"].Value;
            entity.m_UserName = node.Attributes["UserName"].Value;
            entity.m_Password = node.Attributes["Password"].Value;
            return entity;
        }
    }
}
