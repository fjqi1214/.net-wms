using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using System.Web.Services;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Web
{
    /// <summary>
    /// FStartPage 的摘要说明。
    /// </summary>
    public partial class FStartPage : BenQGuru.eMES.Web.Helper.BasePage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        //		public BenQGuru.eMES.Web.UserControl.PopNavigator popNavigator = new BenQGuru.eMES.Web.UserControl.PopNavigator();

        /// <summary>
        /// 记录当前页面为报表平台、或作业中心
        /// </summary>
        public bool IsReportCenter
        {
            get
            {
                if (this.ViewState["IsReportCenter"] == null)
                {
                    this.ViewState["IsReportCenter"] = false;
                }
                return Convert.ToBoolean(ViewState["IsReportCenter"]);
            }
            set { ViewState["IsReportCenter"] = value; }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.lblLogout.Text = this.languageComponent1.GetString("$PageControl_ConfirmLogOut");
                this.lnkButtonLogout.Text = this.languageComponent1.GetString("$PageControl_Logout");
                ChengePictureByLanguage();

                this.lblUserName.Text = this.GetUserName();
                //this.lblDepartmentName.Text = DepartmentName;

                //sammer kong 20050408 statisical for account of loggin user
                //				this.welcome.Text = WebStatisical.Instance()["User"].GetAllCount().ToString();

                MenuBuilder menubuilder = new MenuBuilder();
                menubuilder.currentPage = this;
                menubuilder.UserName = this.GetUserName();
                menubuilder.Build(mainMenu, this.languageComponent1, base.DataProvider);

                InitWorkCenterMenu();

                // Added By hi1/Venus.Feng on 20080627 for 
                this.LinkOrgList.InnerText = GlobalVariables.CurrentOrganizations.First().OrganizationDescription;
                // End Added

                lblLocation.Text = languageComponent1.GetString("$PageControl_WorkCenter");
                this.linkBtnPlatForm.Text = languageComponent1.GetString("$PageControl_ReportPlatfrom");
            }
            //else
            //{
            //    this.WebDataMenuSample.StyleSetName = this.DDList1.SelectedValue;
            //}
        }


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
            this.lnkButtonLogout.Click += new EventHandler(lnkButtonLogout_Click);
        }
        #endregion

        public void lnkButtonLogout_Click(object sender, EventArgs e)
        {
            SessionHelper sessionHelper = SessionHelper.Current(this.Session);
            sessionHelper.RemoveAll();

            this.Response.Redirect(this.MakeRedirectUrl(string.Format("{0}FLoginNew.aspx", this.VirtualHostRoot)), false);
        }



        //根据登录的语言选择加载不同的图片
        public void ChengePictureByLanguage()
        {
            if (this.languageComponent1.Language.Equals("CHS"))
            {
                this.ImageHead.Src = "Skin/Image/Index_eMES_banner.gif";
                this.ImageLogo.Src = "Skin/Image/Index_eMES_logo.gif";
            }

            if (this.languageComponent1.Language.Equals("CHT"))
            {
                this.ImageHead.Src = "Skin/Image/Index_eMES_banner_CHT.gif";
                this.ImageLogo.Src = "Skin/Image/Index_eMES_logo.gif";
            }

            if (this.languageComponent1.Language.Equals("ENU"))
            {
                this.ImageHead.Src = "Skin/Image/Index_eMES_banner_ENG.gif";
                this.ImageLogo.Src = "Skin/Image/Index_eMES_logo_ENG.gif";
            }


        }
        public void linkBtnPlatForm_OnClick(object sender, EventArgs e)
        {
            if (!IsReportCenter)
            {
                this.linkBtnPlatForm.Text = languageComponent1.GetString("$PageControl_WorkCenter");
                InitPlatFormMenu();
                IsReportCenter = true;
                ClientScript.RegisterStartupScript(this.GetType(), "frmWorkSrc", "$('#frmWorkSpace').attr('src', 'ReportView/FRptMaintainStartPageMP.aspx');", true);
                lblLocation.Text = languageComponent1.GetString("$PageControl_ReportPlatfrom");
            }
            else
            {
                this.linkBtnPlatForm.Text = languageComponent1.GetString("$PageControl_ReportPlatfrom");
                InitWorkCenterMenu();
                IsReportCenter = false;
                ClientScript.RegisterStartupScript(this.GetType(), "frmWorkSrc", "$('#frmWorkSpace').attr('src', 'FIframePage.aspx');", true);
                lblLocation.Text = languageComponent1.GetString("$PageControl_WorkCenter");
            }
        }

        private void InitWorkCenterMenu()
        {
            MenuBuilderNew menubuilderNew = new MenuBuilderNew();
            menubuilderNew.currentPage = this;
            menubuilderNew.UserName = this.GetUserName();
            menubuilderNew.Build(WebDataMenuSample, this.languageComponent1, base.DataProvider);
        }

        private void InitPlatFormMenu()
        {
            MenuBuilderNew menubuilderNew = new MenuBuilderNew();
            menubuilderNew.currentPage = this;
            menubuilderNew.UserName = this.GetUserName();
            menubuilderNew.BuildRPTNew(WebDataMenuSample, this.languageComponent1, base.DataProvider);
        }

        [WebMethod]
        public static void UploadError(string msg, string innserMsg)
        {
            SQLDomainDataProvider provider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider() as SQLDomainDataProvider;
            SystemSettingFacade facade = new SystemSettingFacade(provider);

            SystemError systemError = facade.CreateNewSystemError();

            systemError.SystemErrorCode = Guid.NewGuid().ToString();
            systemError.ErrorMessage = FormatHelper.CleanString(msg, 100);
            systemError.InnerErrorMessage = FormatHelper.CleanString(innserMsg, 100);
            systemError.TriggerModuleCode = SessionHelper.Current(HttpContext.Current.Session).ModuleCode;
            systemError.SendUser = SessionHelper.Current(HttpContext.Current.Session).UserCode;
            if (systemError.SendUser == null || systemError.SendUser == string.Empty)
            {
                systemError.SendUser = "System";
            }
            systemError.SendDate = FormatHelper.TODateInt(DateTime.Now);
            systemError.SendTime = FormatHelper.TOTimeInt(DateTime.Now);
            systemError.IsResolved = FormatHelper.BooleanToString(false);
            systemError.MaintainUser = SessionHelper.Current(HttpContext.Current.Session).UserCode;
            if (systemError.MaintainUser == null || systemError.MaintainUser == string.Empty)
            {
                systemError.MaintainUser = "System";
            }

            facade.AddSystemError(systemError);
            provider.PersistBroker.CloseConnection();
        }

    }
}
