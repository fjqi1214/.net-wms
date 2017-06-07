using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace BenQGuru.eMES.SAPDataTransferService
{
    public class ScheduledServiceEntity
    {
        public ScheduledServiceEntity()
        {

        }
        private string m_Key;

        public string Key
        {
            get { return m_Key; }
            set { m_Key = value; }
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

        private int m_StartHour;

        public int StartHour
        {
            get { return m_StartHour; }
            set { m_StartHour = value; }
        }

        private int m_StartMinutes;

        public int StartMinutes
        {
            get { return m_StartMinutes; }
            set { m_StartMinutes = value; }
        }


        private bool m_WaitForRun = true;

        public bool WaitForRun
        {
            get { return m_WaitForRun; }
            set { m_WaitForRun = value; }
        }
        
        internal static List<ScheduledServiceEntity> ScheduledServiceEntities(XmlElement element)
        {
            List<ScheduledServiceEntity> list = new List<ScheduledServiceEntity>();
            XmlNodeList entityList = element.GetElementsByTagName("ScheduledServiceEntity");
            foreach (XmlNode node in entityList)
            {
                ScheduledServiceEntity entity = Node2ScheduledServiceEntity(node);
                if (list.Find(new Predicate<ScheduledServiceEntity>(delegate(ScheduledServiceEntity sse)
                {
                    if (string.Compare(sse.Key, entity.Key, true) == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                })) == null)
                {
                    list.Add(entity);
                }
            }
            return list;
        }

        private static ScheduledServiceEntity Node2ScheduledServiceEntity(XmlNode node)
        {
            ScheduledServiceEntity entity = new ScheduledServiceEntity();
            entity.m_AssemblyPath = node.Attributes["AssemblyPath"].Value;
            entity.m_Type = node.Attributes["Type"].Value;
            entity.m_Key = node.Attributes["Key"].Value;
            entity.m_StartHour = int.Parse(node.Attributes["StartHour"].Value);
            entity.m_StartMinutes = int.Parse(node.Attributes["StartMinutes"].Value);
            return entity;
        }
    }
}
