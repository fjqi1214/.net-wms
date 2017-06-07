using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.SelectQuery
{
    public partial class FSingleOPSP : BaseSingleSelectorPageNew
    {
        private BenQGuru.eMES.SelectQuery.SPFacade facade;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // ≥ı ºªØ“≥√Ê”Ô—‘
                this.InitPageLanguage(this.languageComponent1, false);
                if (Request.QueryString["WithRoute"] == "Y")
                {
                    this.txtRouteCodeQuery.Text = Request.QueryString["RouteCode"];
                    this.tdRouteLabel.Attributes.Add("style", "display:block;");
                    this.tdRouteEdit.Attributes.Add("style", "display:block;");
                }
                else
                {
                    this.txtRouteCodeQuery.Text = "";
                    this.tdRouteLabel.Attributes.Add("style", "display:none;");
                    this.tdRouteEdit.Attributes.Add("style", "display:none;");
                }
            }
        }

        #region WebGrid
       
        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((BenQGuru.eMES.Domain.BaseSetting.Operation)obj).OPCode;
            row["Selector_UnSelectedDesc"] = ((BenQGuru.eMES.Domain.BaseSetting.Operation)obj).OPDescription;
            return row;
        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QueryUnSelectedOperation(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRouteCodeQuery.Text)), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOPCodeQuery.Text)), new string[0], inclusive, exclusive);
        }


        protected override int GetUnSelectedRowCount()
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QueryUnSelectedOperationCount(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRouteCodeQuery.Text)), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOPCodeQuery.Text)), new string[0]);
        }

        #endregion
    }
}
