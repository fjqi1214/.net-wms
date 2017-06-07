using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;

namespace BenQGuru.eMES.SAPDataTransferService
{
    public class ScheduledServiceEntitySectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            List<ScheduledServiceEntity> scheduledServiceEntityes = new List<ScheduledServiceEntity>();
            try
            {
                XmlElement element = null;
                element = (XmlElement)section;
                scheduledServiceEntityes = ScheduledServiceEntity.ScheduledServiceEntities(element);
            }
            catch
            {
                throw new System.Configuration.ConfigurationErrorsException("Invalid scheduled service entity configuration");
            }
            return scheduledServiceEntityes;
        }

        #endregion
    }
}
