using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using BenQGuru.eMES.SAPDataTransfer;

namespace BenQGuru.eMES.SAPWebService
{
    /// <summary>
    /// Summary description for MaterialIssueInterface
    /// </summary>
    [WebService(Namespace = "http://BenQGuru.eMES.SAPWebService/MaterialIssueInterface")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class MaterialIssueInterface : System.Web.Services.WebService
    {

        log4net.ILog m_log = log4net.LogManager.GetLogger(typeof(MaterialIssueInterface));
        [WebMethod]
        public void Receive(MATISSUE_REQ request)
        {
            if (request == null)
            {
                return;
            }
            m_log.Info("Called");

            MaterialIssueTransfer materialIssueTransfer = new MaterialIssueTransfer();

            try
            {
                materialIssueTransfer.Receive(request);

                m_log.Info("Success");
            }
            catch (Exception ex)
            {
                m_log.Error(ex);
            }
        }
    }
}
