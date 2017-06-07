using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.SessionState;

using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.MOModel
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FItem2SNCheck : IHttpHandler, IRequiresSessionState
    {
        private ItemFacade _facade = null;//
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
            string itemCode = string.Empty;
            string snPrefix = context.Request.QueryString["snPrefix"].Trim();
            string snLength = context.Request.QueryString["snLength"].Trim();
            string actionType = context.Request.QueryString["Action"].Trim();
            string itemCodePara = context.Request.QueryString["ItemCode"].Trim();            

            try
            {
                _facade = new ItemFacade(this.DataProvider);
                object[] obj = _facade.GetSN(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(snPrefix, 40)), snLength);
                if (obj != null && obj.Length > 0)
                {
                    if (string.Compare(actionType, "ADD", true) == 0)
                    {
                        foreach (Item2SNCheckMP item2SNCheckMP in obj)
                        {
                            itemCode += item2SNCheckMP.ItemCode + ",";
                        }

                        if (itemCode.Length > 0)
                        {
                            itemCode = itemCode.Substring(0, itemCode.Length - 1);
                        }
                    }
                    else
                    {
                        foreach (Item2SNCheckMP item2SNCheckMP in obj)
                        {
                            if (string.Compare(itemCodePara, item2SNCheckMP.ItemCode, true) != 0)
                            {
                                itemCode += item2SNCheckMP.ItemCode + ",";
                            }
                        }

                        if (itemCode.Length > 0)
                        {
                            itemCode = itemCode.Substring(0, itemCode.Length - 1);
                        }
                    }
                }
                context.Response.Write(itemCode);
            }
            catch (Exception ex)
            {
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
