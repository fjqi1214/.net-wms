using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using BenQGuru.eMES.SAPDataTransfer;

namespace BenQGuru.eMES.SAPWebService
{
    /// <summary>
    /// Summary description for MESInterface
    /// </summary>
    [WebService(Namespace = "http://BenQGuru.eMES.SAPWebService/DNReceive/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class DeliveryNoteInterface : System.Web.Services.WebService
    {
        log4net.ILog m_log = log4net.LogManager.GetLogger(typeof(DeliveryNoteInterface));

        [WebMethod]
        public void Receive(DNRecive_REQ request)
        {
            if (request == null)
            {
                return;
            }
            
            m_log.Info("Called");

            DNTransfer dnTransfer = new DNTransfer();
            try
            {
                dnTransfer.Receive(request);

                m_log.Info("Success");
            }
            catch (Exception ex)
            {
                m_log.Error(ex);
            }
        }
    }
}
