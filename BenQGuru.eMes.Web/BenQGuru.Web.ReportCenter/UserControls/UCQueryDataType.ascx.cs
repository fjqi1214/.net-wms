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

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using ControlLibrary.Web.Language;

namespace BenQGuru.Web.ReportCenter.UserControls
{
    public partial class UCQueryDataType : System.Web.UI.UserControl
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
            RadioButtonListBuilder builder = new RadioButtonListBuilder(new NewReportQueryDataType(), this.rblQueryDataType, this.languageComponent1);

            if (!this.IsPostBack)
            {
                builder.Build();

                ReportPageHelper.SetControlValue(this, this.Request.Params);
            }

            RadioButtonListBuilder.FormatListControlStyle(this.rblQueryDataType, 50);
        }

        public string GetQueryDataType()
        {
            return this.rblQueryDataType.SelectedValue;
        }
    }
}