using System;
using System.Text;
using System.Security.Cryptography;
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
using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.Web
{
	/// <summary>
	/// FUserPassWordModifyMP 的摘要说明。
	/// </summary>
	public partial class FUserPassWordModifyMP : BasePage
	{
		protected System.Web.UI.WebControls.Label lblAdmin;
		protected System.Web.UI.WebControls.Button cmdOk;
		protected System.Web.UI.WebControls.Label lblUser;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdCancel;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdSave;

		//private BenQGuru.eMES.BaseSetting.UserFacade _userFacade = new BenQGuru.eMES.BaseSetting.UserFacade();
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		private BenQGuru.eMES.Security.SecurityFacade _securityFacade ;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.tblAdmin.Visible =false;
				this.chbForgetPassword.Checked = false;
			}

			// 在此处放置用户代码以初始化页面
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
	
		private bool PageValidate()
		{
			if(txtUserCode.Text.Trim()=="")
			{
				WebInfoPublish.Publish(this,"$Error_User_Code_Empty",this.languageComponent1 );
                return false;
			}

			if(!chbForgetPassword.Checked)
			{
				if(txtOriginPassword.Value=="")
				{
					WebInfoPublish.Publish(this,"$Error_User_Origin_PassWord_Empty",this.languageComponent1 );
                    return false;
				}
			}

			if(txtNewPassword.Value=="")
			{
				WebInfoPublish.Publish(this,"$Error_User_New_PassWord_Empty",this.languageComponent1 );
                return false;
			}

			if((txtConfrimPassword.Value!= txtNewPassword.Value)|| (txtConfrimPassword.Value == ""))
			{
				WebInfoPublish.Publish(this,"$Error_User_ConfirmPassWord_Error",this.languageComponent1 );
                return false;
			}
            return true;
		}

		protected void cmdConfirm_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				_securityFacade = new BenQGuru.eMES.Security.SecurityFacade(base.DataProvider);
                //this.PageValidate();

				
                /*
                // Removed by Icyer 2007/08/27  取出密码复杂度检查
				int alive_Upper = 0;
				int alive_Lower = 0;
				int alive_Number = 0;
				int alive_Other = 0;

				foreach (char letter in this.txtNewPassword.Value.ToCharArray())
				{
					if(letter >= 'A' && letter <= 'Z')
					{
						alive_Upper = 1;
					}
					else if(letter >= 'a' && letter <= 'z')
					{
						alive_Lower = 1;
					}
					else if(letter >= '0' && letter <= '9')
					{
						alive_Number = 1;
					}
						//if(letter < '0' && letter > '9' && letter < 'a' && letter > 'b' && letter < 'A' && letter > 'Z')
					else
					{
						alive_Other = 1;
					}
				}
                */
				//密码复杂度管控 joe
                if (this.PageValidate()/*this.txtNewPassword.Value.Length >= 7 && alive_Upper == 1 && alive_Lower == 1 && alive_Number == 1 && alive_Other == 1*/)
				{
					if(!chbForgetPassword.Checked)
					{					
						this._securityFacade.ModifyPassword(this.txtUserCode.Text.ToUpper(),this.txtOriginPassword.Value,this.txtNewPassword.Value);
						//joe 新用户更改密码以后正常登陆
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
						DataProvider.BeginTransaction();
						try
						{
							string  pwmodify= "update tbluser set userstat ='O' where usercode ='"+txtUserCode.Text.Trim().ToUpper()+"'";
							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.Execute(pwmodify);
							DataProvider.CommitTransaction();			
						}
						catch
						{
							DataProvider.RollbackTransaction();
							return;
						}
						finally
						{
							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
						}
					}

					else
					{
						this._securityFacade.AdminModifyPassword(this.txtAdminUserCode.Text.ToUpper(),this.txtAdminPassword.Value,this.txtUserCode.Text.ToUpper(),this.txtNewPassword.Value.ToUpper());
					}
					WebInfoPublish.Publish(this,"$Message_Modify_Password_Successful",this.languageComponent1);				
				}
				else
				{
					WebInfoPublish.Publish(this,"$Error_PassWord_Not_Complex",this.languageComponent1 );
					return;
				}
			}
			
			catch(Exception ex)
			{
				string msg = this.languageComponent1.GetString( ex.Message );
				if( msg == "" || msg == null )
				{
					msg = ex.Message;
				}
				WebInfoPublish.Publish(this,msg,this.languageComponent1);

				return;
			}
		}

		protected void chkForgetPassword_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chbForgetPassword.Checked)
			{
				txtOriginPassword.Disabled = true;
				tblAdmin.Visible = true;
			}
			else
			{
				txtOriginPassword.Disabled = false ;
				tblAdmin.Visible = false;
			}
		}
	}
}
