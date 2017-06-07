using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;

namespace BenQGuru.eMES.SAPDataTransferConsole
{
    public class ServiceEntity
    {
        public ServiceEntity()
        {

        }

        private string m_Key;

        public string Key
        {
            get { return m_Key; }
            set { m_Key = value; }
        }

        private string m_Description;

        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        private string m_AssemblyPath;

        public string AssemblyPath
        {
            get { return m_AssemblyPath; }
            set { m_AssemblyPath = value; }
        }

        private string m_Type;

        public string Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        internal static List<ServiceEntity> ServiceEntities(XmlElement element)
        {
            List<ServiceEntity> list = new List<ServiceEntity>();
            XmlNodeList entityList = element.GetElementsByTagName("ServiceEntity");
            foreach (XmlNode node in entityList)
            {
                ServiceEntity entity = Node2ServiceEntity(node);
                if (list.Find(new Predicate<ServiceEntity>(delegate(ServiceEntity se){
                    if (string.Compare(se.Key, entity.Key, true) == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }})) == null)
                {
                    list.Add(entity);
                }
            }
            return list;
        }

        private static ServiceEntity Node2ServiceEntity(XmlNode node)
        {
            ServiceEntity entity = new ServiceEntity();
            entity.m_AssemblyPath = node.Attributes["AssemblyPath"].Value;
            entity.m_Type = node.Attributes["Type"].Value;
            entity.m_Key = node.Attributes["Key"].Value;
            entity.m_Description = node.Attributes["Description"].Value;
            return entity;
        }
    }
}
