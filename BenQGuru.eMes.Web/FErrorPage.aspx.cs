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

using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web
{
	/// <summary>
	/// FErrorPage 的摘要说明。
	/// </summary>
	public partial class FErrorPage : BenQGuru.eMES.Web.Helper.BasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

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

		public string InnerMessage
		{
			get
			{
				return FormatHelper.CleanString( MessageCenter.ParserMessage( this.Request["innermsg"], this.languageComponent1 ) ).Replace("@", Environment.NewLine);
			}
		}
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.lblError.Text = MessageCenter.ParserMessage( this.Request["msg"], this.languageComponent1 ).Replace("@", Environment.NewLine);
                this.cmdDetail.Value = this.languageComponent1.GetString("$PageControl_Detials");
				if ( this.lblError.Text == string.Empty )
				{
					this.lblError.Text = MessageCenter.ParserMessage("$Error_System_Error", this.languageComponent1);
					this.cmdUploadError.Disabled = true;
				}

                //Add by Jarvis 20121212
                if (this.Request["msg"] == "$License_Will_Expire")
                {
                    this.cmdReturn.Visible = false;
                    this.cmdUploadError.Visible = false;
                    this.SplitSpace.Visible = false;
                    this.cmdSure.Visible = true;
                }
			}
		}

		protected void cmdUploadError_ServerClick(object sender, System.EventArgs e)
		{
			SystemSettingFacade facade = new SystemSettingFacade(base.DataProvider);
	
			SystemError systemError = facade.CreateNewSystemError();

			systemError.SystemErrorCode = Guid.NewGuid().ToString();
			systemError.ErrorMessage = FormatHelper.CleanString(this.GetRequestParam("msg"), 100);
			systemError.InnerErrorMessage = FormatHelper.CleanString(this.GetRequestParam("innermsg"), 100);
			systemError.TriggerModuleCode = SessionHelper.Current(this.Session).ModuleCode;
			systemError.SendUser = this.GetUserCode();
            if (systemError.SendUser ==null || systemError.SendUser == string.Empty)
            {
                systemError.SendUser = "System";
            }
			systemError.SendDate = FormatHelper.TODateInt(DateTime.Now);
			systemError.SendTime = FormatHelper.TOTimeInt(DateTime.Now);
			systemError.IsResolved = FormatHelper.BooleanToString(false);
			systemError.MaintainUser = this.GetUserCode();
            if (systemError.MaintainUser == null || systemError.MaintainUser == string.Empty)
            {
                systemError.MaintainUser = "System";
            }

			facade.AddSystemError( systemError );

            string scriptString = "<script language=JavaScript>window.name+='[back]';window.parent.history.back(-1);</script>";

			if( !this.IsClientScriptBlockRegistered("clientScript") )
			{
				this.RegisterClientScriptBlock("clientScript", scriptString);
			}
		}

        //Add by Jarvis 20121212
        protected void cmdSure_ServerClick(object sender, System.EventArgs e)
        {
            LicenseContinue.LicenseGo = "Continue";
            Server.Transfer(this.MakeRedirectUrl(string.Format("{0}FStartPage.aspx", this.VirtualHostRoot)));
        }
	}
}
