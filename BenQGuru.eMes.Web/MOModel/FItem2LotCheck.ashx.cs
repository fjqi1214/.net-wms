using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.SessionState;

using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
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
    public class FItem2LotCheck : IHttpHandler, IRequiresSessionState
    {
        private ItemLotFacade _facade = null;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent = null;
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
            languageComponent = new ControlLibrary.Web.Language.LanguageComponent();

            try
            {
                _facade = new ItemLotFacade(this.DataProvider);
                object[] obj = _facade.GetItemCodeForLotCheck(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(snPrefix, 40)), snLength);
                if (obj != null && obj.Length > 0)
                {
                    if (string.Compare(actionType, "ADD", true) == 0)
                    {
                        foreach (Item2LotCheck item2LotCheck in obj)
                        {
                            itemCode += item2LotCheck.ItemCode + ",";
                        }

                        if (itemCode.Length > 0)
                        {
                            itemCode = itemCode.Substring(0, itemCode.Length - 1);
                        }
                    }
                    else
                    {
                        foreach (Item2LotCheck item2LotCheck in obj)
                        {
                            if (string.Compare(itemCodePara, item2LotCheck.ItemCode, true) != 0)
                            {
                                itemCode += item2LotCheck.ItemCode + ",";
                            }
                        }

                        if (itemCode.Length > 0)
                        {
                            itemCode = itemCode.Substring(0, itemCode.Length - 1);
                        }
                    }
                }
                if (itemCode.Length > 0)
                {
                    context.Response.Write(this.languageComponent.GetString("$Error_Item2LotCheck_1") + itemCode + this.languageComponent.GetString("$Error_Item2LotCheck_2"));
                }
                else
                {
                    context.Response.Write(itemCode);
                }                
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
