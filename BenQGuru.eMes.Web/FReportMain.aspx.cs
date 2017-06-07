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

namespace BenQGuru.eMES.Web
{
	/// <summary>
	/// ReportMain 的摘要说明。
	/// </summary>
	public partial class FReportMain : BenQGuru.eMES.Web.Helper.BaseRQPage
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
				
				this.lblUserName.Text = this.GetUserName();
				this.lblDepartmentName.Text = DepartmentName;
				imgLogout.Attributes["onclick"] = "return LogoutCheck();";

//				OutlookBarBuilder barbuilder = new OutlookBarBuilder();
//				barbuilder.currentPage = this;
//				barbuilder.UserName = this.GetUserName();
//				barbuilder.Build(webOutlookBar, this.languageComponent1,base.DataProvider);
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

		private void imgLogout_ServerClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SessionHelper sessionHelper = SessionHelper.Current(this.Session); 						

			//sammer kong 20050408 statisical for account of loggin user						
			//			WebStatisical.Instance()["User"].Delete(sessionHelper.UserCode);	
		
			sessionHelper.RemoveAll();
			//

			this.Response.Redirect(this.MakeRedirectUrl(string.Format("{0}FLoginNew.aspx", this.VirtualHostRoot)),false);
		}
	}
}
