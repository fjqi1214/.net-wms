using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class TransferServiceHandler : System.Configuration.IConfigurationSectionHandler
    {

        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            SAPWebServiceEntity webServiceEntity = new SAPWebServiceEntity();

            try
            {
                XmlElement element = null;
                element = (XmlElement)section;
                webServiceEntity = SAPWebServiceEntity.Parse(element);
            }
            catch
            {
                throw new System.Configuration.ConfigurationErrorsException("Invalid service entity configuration");
            }
            return webServiceEntity;
        }

        #endregion
    }
}
