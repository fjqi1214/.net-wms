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
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.Web.MOModel
{
    /// <summary>
    /// FMO2SAPLogQP 的摘要说明。
    /// </summary>
    public partial class FMO2SAPLogQP : BaseMPage
    {
        protected System.Web.UI.WebControls.Label lblOperationSelectTitle;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private MOFacade facade = null;

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //			
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        #region Stable
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //this.pagerSizeSelector.Readonly = true;

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.txtMoCodeQuery.Text = this.GetRequestParam("MOCode");

                this.txtPostSeqQuery.Text = this.GetRequestParam("PostSeq");

                ViewState["PageName"] = this.GetRequestParam("PageName");

            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #endregion

        #region NotStable
        protected override void InitWebGrid()
        {
            this.gridHelper.AddColumn("MoCode", "工单号", null);
            this.gridHelper.AddColumn("PostSeq", "报工批次", null);
            this.gridHelper.AddColumn("Seq", "批次", null);
            this.gridHelper.AddColumn("ErrorMessage", "错误信息", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);


            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            base.InitWebGrid();

            this.gridHelper.RequestData();
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new MOFacade(this.DataProvider);
            }

            return this.facade.GetMO2SAPLogListCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoCodeQuery.Text)), decimal.Parse(this.txtPostSeqQuery.Text));
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{
							((MO2SAPLog)obj).MOCode.ToString(),
                            ((MO2SAPLog)obj).PostSequence.ToString(),
                            ((MO2SAPLog)obj).Sequence.ToString(),
                            ((MO2SAPLog)obj).ErrorMessage.ToString(),
							FormatHelper.ToDateString(((MO2SAPLog)obj).MaintainDate),							
							FormatHelper.ToTimeString(((MO2SAPLog)obj).MaintainTime)
							});
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new MOFacade(this.DataProvider);
            }
            return facade.GetMO2SAPLogList(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoCodeQuery.Text)), decimal.Parse(this.txtPostSeqQuery.Text),
                inclusive, exclusive);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl(ViewState["PageName"].ToString(), new string[] { "MOCode", "OrgID" }, new string[] { this.txtMoCodeQuery.Text, GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString() }));
        }
        #endregion
    }
}
