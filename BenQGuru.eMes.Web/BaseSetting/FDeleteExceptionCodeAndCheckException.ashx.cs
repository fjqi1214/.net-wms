using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.SessionState;

using BenQGuru.eMES.Performance;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Performance;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FDeleteExceptionCodeAndCheckException : IHttpHandler, IRequiresSessionState
    {
        private PerformanceFacade _facade = null;//
        private IDomainDataProvider _domainDataProvider = null;
        protected IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                }
                return _domainDataProvider;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            string exceptionCode = context.Request.QueryString["exceptionCode"].Trim().ToUpper();

            _facade = new PerformanceFacade(this.DataProvider);

            string returnValue = "false";
            this.DataProvider.BeginTransaction();
            try
            {
                ExceptionCode exceptionCodeObject = (ExceptionCode)_facade.GetExceptionCode(exceptionCode);
                if (exceptionCodeObject != null)
                {
                    _facade.DeleteExceptionCode(exceptionCodeObject);
                }

                object[] exceptionList = _facade.QueryExceptionEvent(string.Empty, 0, string.Empty, string.Empty, exceptionCode);

                if (exceptionList != null)
                {
                    returnValue = "true";
                }

                this.DataProvider.CommitTransaction();
                context.Response.Write(returnValue);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                context.Response.Write(ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
}
