using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using ControlLibrary.Web.Language;

namespace BenQGuru.Web.ReportCenter.UserControls
{
    public partial class UCDisplayConditions : System.Web.UI.UserControl
    {

        #region 页面需要的公共部分

        protected LanguageComponent languageComponent1 = null;
        protected IDomainDataProvider dataProvider = null;

        public void InitUserControl(LanguageComponent languageComponent, IDomainDataProvider dataProvider)
        {
            this.languageComponent1 = languageComponent;
            this.dataProvider = dataProvider;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                rblReportDisplayType.Items.Clear();
                if (_DisplayList != null)
                {
                    foreach (ListItem item in _DisplayList)
                    {
                        rblReportDisplayType.Items.Add(item);
                    }
                    rblReportDisplayType.SelectedIndex = 0;
                }

                ReportPageHelper.SetControlValue(this, this.Request.Params);
            }

            RadioButtonListBuilder.FormatListControlStyle(this.rblReportDisplayType, 50);
        }

        public string GetDisplayType()
        {
            return this.rblReportDisplayType.SelectedValue;
        }

        private List<ListItem> _DisplayList = null;

        public List<ListItem> DisplayList
        {
            get { return _DisplayList; }
            set { _DisplayList = value; }
        }
    }
}