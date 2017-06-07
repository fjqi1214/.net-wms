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

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;

namespace BenQuru.eMES.Web
{
    public partial class FIframePage : BasePage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);

            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
        }

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPageLanguage(this.languageComponent1, false);
            }

            try
            {
                SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
                string parameterValue = string.Empty;

                //获取是否可用
                bool enabled = false;
                parameterValue = systemSettingFacade.GetParameterAlias("BSHOMEREPORT", "ENABLED");
                enabled = string.Compare(parameterValue, "Y", true) == 0;

                if (enabled)
                {
                    //获取时间间隔
                    int seconds = 0;
                    parameterValue = systemSettingFacade.GetParameterAlias("BSHOMEREPORT", "REFRESHSECONDS");
                    int.TryParse(parameterValue, out seconds);
                    this.RefreshController1.Interval = seconds * 1000;
                    this.AutoRefresh = (seconds > 0);

                    iframeExpection.Attributes.Add("src", "FException.aspx");
                    iframeExpection.Attributes.Add("style", "width:100%;height:100%;border-bottom-style:inset;");

                    iframeAlertNotice.Attributes.Add("src", "FAlertNotice.aspx");
                    iframeAlertNotice.Attributes.Add("style", "width:100%;height:100%;border-bottom-style:inset;");

                    string module = string.Empty;
                    string url = string.Empty;

                    systemSettingFacade.GetBSHomeReportURL(1, out module, out url);
                    module = FormatHelper.GetModuleTitle(this.languageComponent1, module);
                    this.Iframe.SetIframeA(module, url);

                    systemSettingFacade.GetBSHomeReportURL(2, out module, out url);
                    module = FormatHelper.GetModuleTitle(this.languageComponent1, module);
                    this.Iframe.SetIframeB(module, url);

                    systemSettingFacade.GetBSHomeReportURL(3, out module, out url);
                    module = FormatHelper.GetModuleTitle(this.languageComponent1, module);
                    this.Iframe.SetIframeC(module, url);

                    systemSettingFacade.GetBSHomeReportURL(4, out module, out url);
                    module = FormatHelper.GetModuleTitle(this.languageComponent1, module);
                    this.Iframe.SetIframeD(module, url);
                }
                else
                {
                    //string imageUrl=string.Empty;
                    //if (this.languageComponent1.Language == "CHS")
                    //{
                    //    imageUrl="'url(Skin/Image/BackgroundImage.png)'";
                    //}
                    //else if (this.languageComponent1.Language == "CHT")
                    //{
                    //    imageUrl = "'url(Skin/Image/BackgroundImage_CHT.png)'";
                    //}
                    //else if (this.languageComponent1.Language == "ENU")
                    //{
                    //    imageUrl = "'url(Skin/Image/BackgroundImage_ENU.png)'";
                    //}

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "bodyBack", "<script language='javascript'>document.body.style.backgroundImage ='url(Skin/Image/BackgroundImage_" + this.languageComponent1.Language + ".png)';</script>");

                    this.RefreshController1.Interval = int.MaxValue;
                    this.AutoRefresh = false;

                    lblException.Text = string.Empty;
                    lblAlertNotice.Text = string.Empty;

                    this.Iframe.Visible = false;
                    this.iframeAlertNotice.Visible = false;
                    this.iframeExpection.Visible = false;

                    ClientScript.RegisterStartupScript(this.GetType(), "hideScroll", @"<script language='javascript'>document.body.scroll='no';</script>");
                }
            }
            catch { }

            //string url1 = @"BenQGuru.Web.ReportCenter/FNewReportQuantityQP.aspx";
            //url1 += @"?__Page.IsForBSHome=true";
            //url1 += @"&UCWhereConditions1.datStartDateWhere=20070101";
            //url1 += @"&UCWhereConditions1.ddlInputOututWhere=output";
            //url1 += @"&UCGroupConditions1.rblByTimeTypeGroup=newreportbytimetype_month";
            //url1 += @"&UCDisplayConditions1.rblReportDisplayType=newreportdisplaytype_histogramchart";
            //string url4 = @"BenQGuru.Web.ReportCenter/FNewReportPerformanceUPPHQP.aspx";
            //url4 += @"?__Page.IsForBSHome=true";
            //url4 += @"&UCWhereConditions1.datStartDateWhere=20070101";
            //url4 += @"&UCDisplayConditions1.rblReportDisplayType=newreportdisplaytype_linechart";
            //this.Iframe.SetIframeA("A", url1);
            //this.Iframe.SetIframeB("A", url1);
            //this.Iframe.SetIframeC("A", url1);
            //this.Iframe.SetIframeD("A", url4);
        }

        public bool AutoRefresh
        {
            get
            {
                if (this.ViewState["AutoRefresh"] != null)
                {
                    try
                    {
                        return bool.Parse(this.ViewState["AutoRefresh"].ToString());
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            set
            {
                this.ViewState["AutoRefresh"] = value.ToString();

                if (value)
                {
                    this.RefreshController1.Start();
                }
                else
                {
                    this.RefreshController1.Stop();
                }
            }
        }
    }
}
