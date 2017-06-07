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

using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Web.SelectQuery
{
    public partial class FsingleShiftcodeBySSCodeSP : BaseSingleSelectorPageNew
    {
        private BenQGuru.eMES.SelectQuery.SPFacade facade;
        public BenQGuru.eMES.Web.UserControl.eMESDate DateQuery;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // ≥ı ºªØ“≥√Ê”Ô—‘
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitControls();
            }
        }

        protected void InitControls()
        {
            if (this.GetRequestParam("SSCode") != null)
            {
                this.txtSSQuery.Text = this.GetRequestParam("SSCode").ToString();
            }
        }

        #region WebGrid
        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((Shift)obj).ShiftCode;
            row["Selector_UnSelectedDesc"] = ((Shift)obj).ShiftDescription;
            return row;
        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QueryUnSelectedShiftCodeByStepsequence(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftCodeQuery.Text)),
                                                                      FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSQuery.Text)),
                                                                      new string[0],
                                                                      inclusive, exclusive);
        }


        protected override int GetUnSelectedRowCount()
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QueryUnSelectedShiftCodeByStepsequenceCount(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftCodeQuery.Text)),
                                                                           FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSQuery.Text)),
                                                                           new string[0]);
        }

        #endregion
    }
}
