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

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Security;

namespace BenQGuru.eMES.Web
{
	/// <summary>
	/// FMain 的摘要说明。
	/// </summary>
	public partial class FLogin : BenQGuru.eMES.Web.Helper.BasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.Image imgDotNetLogo;

		private SecurityFacade _facade ;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!Page.IsPostBack)
			{
				this.InitPageLanguage(languageComponent1, false);
			}
//			this.InitUI();
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

		protected void cmdOK_ServerClick(object sender, System.EventArgs e)
		{
			SessionHelper sessionHelper = SessionHelper.Current(this.Session); 
			_facade = new SecurityFacade(base.DataProvider);
			try
			{
				// 未输入用户名
				if ( this.txtUserCode.Text.Trim() == string.Empty )
				{
					ExceptionManager.Raise(this.GetType(), "$Error_User_Code_Empty");	
				}
				//用户输入密码错误5次
				if(this.loguser.Value != this.txtUserCode.Text.Trim() && this.loguser.Value != string.Empty )
				{
				this.logintimes.Value = "0";//登陆用户与上一次的用户不同且不是第一次登陆,输入密码错误次数清零
				}
				if(this.loguser.Value == this.txtUserCode.Text.Trim() || this.loguser.Value == string.Empty )//登陆用户第一次或者和上次登陆的相同
				{
					this.loguser.Value = this.txtUserCode.Text.Trim();
					int logtimes = Convert.ToInt32(this.logintimes.Value);
					logtimes = logtimes + 1;
					this.logintimes.Value = logtimes.ToString();
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
					DataProvider.BeginTransaction();
					try
					{
						string login = "update tbluser set userstat ='L' where usercode ='"+txtUserCode.Text.Trim().ToUpper()+"'";
						if(logtimes > 5)
						{
							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.Execute(login);//密码错误5次将userstat改为L,锁定账户
							DataProvider.CommitTransaction();
							this.logintimes.Value = "0";
							return;
						}
					}
					catch 
					{
						DataProvider.RollbackTransaction();
					}
					finally
					{
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
					}
				}
				// 未输入密码
				if ( this.txtPassword.Text.Trim() == string.Empty )
				{
					ExceptionManager.Raise(this.GetType(), "$Error_Password_Empty");	
				}
				
					BenQGuru.eMES.Domain.BaseSetting.User user = this._facade.LoginCheck(FormatHelper.CleanString(this.txtUserCode.Text.ToUpper()),FormatHelper.CleanString(this.txtPassword.Text.ToUpper()));

				// 用户名不存在
				if ( user == null )
				{
					ExceptionManager.Raise(this.GetType(), "$Error_User_Not_Exist");	
				}
				//新用户必须更改密码,用户限制,锁定账户	
				string userstat="select userstat from tbluser where usercode ='"+txtUserCode.Text.Trim().ToUpper()+"'";
				DataSet ds=((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(userstat);
				if ( ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
				{
					if(ds.Tables[0].Rows[0][0].ToString() == "C")//用户限制
					{
						ExceptionManager.Raise(this.GetType(), "$Error_User_Confined");
						return;
					}
					else if (ds.Tables[0].Rows[0][0].ToString() == "L")//锁定账户
					{
						ExceptionManager.Raise(this.GetType(), "$Error_User_Locked");
						return;
					}
					else if (ds.Tables[0].Rows[0][0].ToString() == "N")//新用户必须更改密码
					{
						ExceptionManager.Raise(this.GetType(), "$Error_User_New");
						return;
					}
				}
				
				sessionHelper.IsBelongToAdminGroup = this._facade.IsBelongToAdminGroup( this.txtUserCode.Text.ToUpper() );
				sessionHelper.UserName = user.UserName;
				sessionHelper.UserCode = user.UserCode;
				sessionHelper.UserMail = user.UserEmail;
				sessionHelper.Language = this.drpLanguage.Value;

//				//sammer kong 20050408 statisical for account of loggin user
//				if( sessionHelper.UserCode != null )
//				{
//					WebStatisical.Instance()["user"].Add( (sessionHelper.UserCode ) );
//				}
					
				this.Response.Redirect(this.MakeRedirectUrl("./FStartPage.aspx"),false);
				
			}
			catch(Exception ex)
			{
				this.lblMessage.Text = MessageCenter.ParserMessage( ex.Message, this.languageComponent1 );
			}
		}

	}
}
