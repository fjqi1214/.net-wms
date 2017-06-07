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
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.Web.SelectQuery
{
    public partial class FSingleVendorSP : BaseSingleSelectorPageNew
    {
        private BenQGuru.eMES.SelectQuery.SPFacade facade;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // ≥ı ºªØ“≥√Ê”Ô—‘
                this.InitPageLanguage(this.languageComponent1, false);
            }
        }

        #region WebGrid
       
        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((Vendor)obj).VendorCode;
            row["Selector_UnSelectedDesc"] = ((Vendor)obj).VendorName;
            return row;
        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QueryUNSelectVendorCode(FormatHelper.CleanString(this.txtVendorCode.Text), FormatHelper.CleanString(this.txtVendorDescription.Text), new string[0], inclusive, exclusive);
        }


        protected override int GetUnSelectedRowCount()
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QueryUNSelectVendorCodeCount(this.txtVendorCode.Text, FormatHelper.CleanString(this.txtVendorDescription.Text), new string[0]);
        }

        #endregion
    }
}
