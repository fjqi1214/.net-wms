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
	/// FPageNavigator 的摘要说明。
	/// </summary>
	public partial class FPageNavigator : BasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;		
		protected BenQGuru.eMES.Web.UserControl.PageNavigator pageNavigator;
		//private BenQGuru.eMES.Security.SecurityFacade _facade;
		public const string FRAME_WORKSPACE = "frmWorkSpace";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				this.pageNavigator.TargetFrame = FRAME_WORKSPACE;

                // changed by icyer 2007/05/08
                // 从URL读取模块代码
                // 功能：在IE点"后退"时，能更新导航栏
                /*
				PageNavigatorBuilder.Build(
					pageNavigator,  
					SessionHelper.Current(this.Session).ModuleCode,
					SessionHelper.Current(this.Session).Url,
					SessionHelper.Current(this.Session).Urls,
					this.languageComponent1,
					base.DataProvider,this);
                */
                string moduleCode = this.GetRequestParam("modulecode");
                if (moduleCode == string.Empty)
                {
                    moduleCode = SessionHelper.Current(this.Session).ModuleCode;
                }
                string sUrl = Server.UrlDecode(this.GetRequestParam("currentmoduleurl"));
                if (sUrl == string.Empty)
                {
                    sUrl = SessionHelper.Current(this.Session).Url;
                }
                this.txtModuleCode.Value = moduleCode;
                PageNavigatorBuilder.Build(
                    pageNavigator,
                    moduleCode,
                    sUrl,
                    SessionHelper.Current(this.Session).Urls,
                    this.languageComponent1,
                    base.DataProvider, this);
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
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";

		}
		#endregion
	}
}
