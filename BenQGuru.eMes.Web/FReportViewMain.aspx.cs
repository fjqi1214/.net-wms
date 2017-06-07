using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web
{
	/// <summary>
	/// ReportMain 的摘要说明。
	/// </summary>
    public partial class FReportViewMain : BenQGuru.eMES.Web.Helper.BaseRQPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		protected System.Web.UI.HtmlControls.HtmlInputImage Image1;
		private System.ComponentModel.IContainer components;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面

			if(!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
                this.lblLogout.Text = this.languageComponent1.GetString("$PageControl_ConfirmLogOut");
                this.lnkButtonLogout.Text = this.languageComponent1.GetString("$PageControl_Logout");
                ChengePictureByLanguage();

				this.lblUserName.Text = this.GetUserName();
				//this.lblDepartmentName.Text = DepartmentName;
				imgLogout.Attributes["onclick"] = "return LogoutCheck();";

//				OutlookBarBuilder barbuilder = new OutlookBarBuilder();
//				barbuilder.currentPage = this;
//				barbuilder.UserName = this.GetUserName();
//				barbuilder.Build(webOutlookBar, this.languageComponent1,base.DataProvider);

                DisplayReportList();

                DisplayDefaultReport();
                // Added By GR14/Johnson.Shao on 20080831 for 
                this.LinkOrgList.InnerText = GlobalVariables.CurrentOrganizations.First().OrganizationDescription;
                // End Added
			}
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
			this.languageComponent1.Language = "CHT";
			this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";
			this.imgLogout.ServerClick += new ImageClickEventHandler(imgLogout_ServerClick);

		}
		#endregion

        private void DisplayReportList()
        {
            BenQGuru.eMES.ReportView.ReportViewFacade rptFacade = new BenQGuru.eMES.ReportView.ReportViewFacade(this.DataProvider);
            object[] objsEntry = rptFacade.GetAllRptViewEntry();
            if (objsEntry == null || objsEntry.Length == 0)
                return;
            BenQGuru.eMES.Domain.ReportView.RptViewEntry[] entryList = new BenQGuru.eMES.Domain.ReportView.RptViewEntry[objsEntry.Length];
            objsEntry.CopyTo(entryList, 0);
            for (int i = 0; i < entryList.Length; i++)
            {
                BenQGuru.eMES.Domain.ReportView.RptViewEntry entry = entryList[i];
                if (entry.ParentEntryCode == "" && FormatHelper.StringToBoolean(entry.Visible) == true)
                {
                    string strArrRptName = "";
                    string strArrRptId = "";
                    for (int n = 0; n < entryList.Length; n++)
                    {
                        if (entryList[n].ParentEntryCode == entry.EntryCode && 
                            entryList[n].EntryType == ReportEntryType.Report &&
                            FormatHelper.StringToBoolean(entryList[n].Visible) == true && 
                            entryList[n].ReportID != "")
                        {
                            strArrRptName += "\"" + entryList[n].EntryName + "\",";
                            strArrRptId += "\"" + entryList[n].ReportID + "\",";
                        }
                    }
                    if (strArrRptName.Length > 0)
                    {
                        strArrRptName = strArrRptName.Substring(0, strArrRptName.Length - 1);
                        strArrRptId = strArrRptId.Substring(0, strArrRptId.Length - 1);
                    }
                    string strRegName = "var arrRptName" + i.ToString() + "=new Array(" + strArrRptName + ");";
                    string strRegId = "var arrRptId" + i.ToString() + "=new Array(" + strArrRptId + ");";
                    string strScript = "<script language=javascript>\r\n" + strRegName + "\r\n" + strRegId + "\r\n" + "InitReportList(\"" + entry.EntryName + "\",arrRptName" + i.ToString() + ",arrRptId" + i.ToString() + ");\r\n</script>\r\n";
                    this.ClientScript.RegisterStartupScript(typeof(string), "Rpt" + i.ToString(), strScript);
                    
                }
            }
            
        }

        private void DisplayDefaultReport()
        {
            if (this.Session["PublishedReportId"] == null || this.Session["PublishedReportId"].ToString() == "")
            {
                BenQGuru.eMES.ReportView.ReportViewFacade rptFacade = new BenQGuru.eMES.ReportView.ReportViewFacade(this.DataProvider);
                BenQGuru.eMES.Domain.ReportView.RptViewUserDefault objDef = (BenQGuru.eMES.Domain.ReportView.RptViewUserDefault)rptFacade.GetRptViewUserDefault(this.GetUserCode());
                if (objDef != null && objDef.DefaultReportID != "")
                {
                    if (rptFacade.GetRptViewDesignMain(objDef.DefaultReportID) != null)
                    {
                        string strScript = "<script language=javascript>document.getElementById('content').src='ReportView/FRptViewMP.aspx?reportid=" + objDef.DefaultReportID + "';</script>";
                        this.ClientScript.RegisterStartupScript(typeof(string), "DefaultReport", strScript);
                    }
                }
            }
            else    // 显示刚发布的报表
            {
                string strScript = "<script language=javascript>document.getElementById('content').src='ReportView/FRptViewMP.aspx?reportid=" + this.Session["PublishedReportId"].ToString() + "';</script>";
                this.ClientScript.RegisterStartupScript(typeof(string), "DefaultReport", strScript);
                this.Session.Remove("PublishedReportId");
            }
        }

		private void imgLogout_ServerClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SessionHelper sessionHelper = SessionHelper.Current(this.Session); 						

			//sammer kong 20050408 statisical for account of loggin user						
			//			WebStatisical.Instance()["User"].Delete(sessionHelper.UserCode);	
		
			sessionHelper.RemoveAll();
			//

			this.Response.Redirect(this.MakeRedirectUrl(string.Format("{0}FLoginNew.aspx", this.VirtualHostRoot)),false);
		}

        public void lnkButtonLogout_Click(object sender, EventArgs e)
        {
            SessionHelper sessionHelper = SessionHelper.Current(this.Session);
            sessionHelper.RemoveAll();

            this.Response.Redirect(this.MakeRedirectUrl(string.Format("{0}FLoginNew.aspx", this.VirtualHostRoot)), false);
        }

        //根据登录的语言选择加载不同的图片
        private void ChengePictureByLanguage()
        {
            if (this.languageComponent1.Language.Equals("CHS"))
            {
                this.ImageHead.Src = "Skin/Image/Index_eMES_banner.gif";
                this.ImageLogo.Src = "Skin/Image/Index_eMES_logo.gif";

                this.imgHome.Src = "skin/image/ico_home.gif";
                this.imgLogout.Src = "skin/image/ico_exit.gif";
            }

            if (this.languageComponent1.Language.Equals("CHT"))
            {
                this.ImageHead.Src = "Skin/Image/Index_eMES_banner_CHT.gif";
                this.ImageLogo.Src = "Skin/Image/Index_eMES_logo.gif";

                this.imgHome.Src = "skin/image/ico_home_CHT.gif";
                this.imgLogout.Src = "skin/image/ico_exit.gif";
            }

            if (this.languageComponent1.Language.Equals("ENU"))
            {
                this.ImageHead.Src = "Skin/Image/Index_eMES_banner_ENG.gif";
                this.ImageLogo.Src = "Skin/Image/Index_eMES_logo_ENG.gif";

                this.imgHome.Src = "skin/image/ico_home_ENU.gif";
                this.imgLogout.Src = "skin/image/ico_exit_ENU.gif";
            }

        }
	}
}
