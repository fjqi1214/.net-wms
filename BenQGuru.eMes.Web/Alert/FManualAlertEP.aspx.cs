#region system
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
using Infragistics.WebUI.UltraWebGrid;
using System.Xml;
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Helper;
using  BenQGuru.eMES.Common.Config;
#endregion
[assembly: BenQGuru.eMES.Common.Config.DomainConfigurator(ConfigFile="Domain.xml", Watch=true)]
namespace BenQGuru.eMES.Web.Alert
{
	/// <summary>
	/// ItemMP 的摘要说明。
	/// </summary>
	public partial class FManualAlertEP : BasePage
	{
		#region 变量声明
		protected System.Web.UI.WebControls.Label lblItemTypeQuery;
		private System.ComponentModel.IContainer components;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		protected System.Web.UI.WebControls.Label lblItemNameQuery;
		protected System.Web.UI.WebControls.Label lblAlert;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox Textbox2;
		protected System.Web.UI.WebControls.Label Label3;

		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox Textbox3;
		protected System.Web.UI.WebControls.TextBox Textbox4;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox Textbox5;
		protected System.Web.UI.WebControls.Label Label7;

		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox Textbox6;
		protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
		protected System.Web.UI.HtmlControls.HtmlInputButton Submit2;

		protected System.Web.UI.WebControls.TextBox TextBox7;
		protected System.Web.UI.WebControls.TextBox TextBox8;
		protected System.Web.UI.WebControls.Label Lab;
		private BenQGuru.eMES.AlertModel.AlertFacade m_alertFacade;
		protected BenQGuru.eMES.BaseSetting.UserFacade m_userfacade;

		private AlertConst _alertConst;
		private string _actionType;
		private int _alertID;
		private BenQGuru.eMES.Web.Helper.ESmtpMail _mail;

		#endregion

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
			this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";

		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.cmdSave.Attributes["onclick"] = "this.disabled=true;if(document.getElementById('chbMailNotify').checked) document.getElementById('chbMailNotify').checked = confirm('" 
													+ BenQGuru.eMES.Web.Helper.MessageCenter.ParserMessage("$If_Mail_Notified" //确实要邮件通知相关人员吗?
																											,this.languageComponent1) 
													+ "');"
													+ "document.getElementById('btnSave').click();";	
			_actionType = Request.QueryString["action"];
			try
			{
				_alertID = int.Parse(Request.QueryString["alertid"]);
			}
			catch
			{
				_alertID = 0;
			}

			_alertConst = new AlertConst(this.languageComponent1);
			this.cmdAddUser.Attributes["onclick"] = "document.getElementById('stbUser:_ctl2').click();";

			if (!IsPostBack)
			{	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				AlertLevelBuilder.Build(this.drpAlertLevel.Items,this._alertConst);
				if(_actionType == AlertPageAction.Edit)
					LoadData();
			}
		}

		BenQGuru.eMES.BaseSetting.UserFacade _userfacade
		{
			get
			{
				if(m_userfacade == null)
					m_userfacade = new BenQGuru.eMES.BaseSetting.UserFacade(DataProvider);

				return m_userfacade;
			}
		}

		BenQGuru.eMES.AlertModel.AlertFacade _alertFacade
		{
			get
			{
				if(m_alertFacade == null)
					m_alertFacade = new BenQGuru.eMES.AlertModel.AlertFacade(DataProvider);

				return m_alertFacade;
			}
		}

		private void LoadData()
		{
			BenQGuru.eMES.Domain.Alert.Alert alert = (BenQGuru.eMES.Domain.Alert.Alert)this._alertFacade.GetAlert(this._alertID);
			if(alert != null)
			{
				this.drpAlertLevel.SelectedValue = alert.AlertLevel;
				this.txtAlertMsg.Text = alert.AlertMsg;
				this.txtDesc.Text = alert.Description;
				this.chbMailNotify.Checked=(alert.MailNotify=="Y");

//				//load 用户
//				if(this.chbMailNotify.Checked)
//				{
					object[] objs = _alertFacade.QueryAlertManualNotifier(alert.AlertID);
					if(objs != null)
					{
						foreach(object obj in objs)
						{
							BenQGuru.eMES.Domain.Alert.AlertManualNotifier notifier  = obj as BenQGuru.eMES.Domain.Alert.AlertManualNotifier;
							if(notifier == null )
								continue;

							BenQGuru.eMES.Domain.BaseSetting.User user = _userfacade.GetUser(notifier.UserCode) as BenQGuru.eMES.Domain.BaseSetting.User;
							if(user!= null && user.UserEmail != null)
							{
								this.lstUser.Items.Add(new ListItem(user.UserCode + "(" + user.UserEmail + ")",user.UserCode));
							}
						}
					}
				//}
			}	
			
		}
		protected ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}

		protected void cmdSave_ServerClick(object sender, System.EventArgs e)
		{
			if(this.ValidateInput())
			{
				SaveData();
				this.cmdReturn_ServerClick(null,null);
			}
		}
		
		private void SaveData()
		{
			try
			{
				DataProvider.BeginTransaction();

				BenQGuru.eMES.Domain.Alert.Alert alert = null;
				if(_actionType == "add")
				{
					alert = _alertFacade.CreateNewAlert();
					//alert.AlertID = _alertFacade.GetNextAlertID();
					alert.ItemCode =string.Empty;
					alert.AlertItem = string.Empty;
					alert.AlertStatus = BenQGuru.eMES.AlertModel.AlertStatus_Old.Unhandled;
					alert.AlertType = BenQGuru.eMES.AlertModel.AlertType_Old.Manual;
				}
				else
				{
					alert = (BenQGuru.eMES.Domain.Alert.Alert)_alertFacade.GetAlert(this._alertID);
				}
	
				alert.MaintainDate = BenQGuru.eMES.Web.Helper.FormatHelper.TODateInt(DateTime.Now);
				alert.MaintainUser = this.GetUserCode();
				alert.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
				alert.SendUser = alert.MaintainUser;
				alert.AlertDate= alert.MaintainDate;
				alert.AlertTime = alert.MaintainTime;
				
				alert.AlertLevel = this.drpAlertLevel.SelectedValue;
				alert.AlertMsg  = this.txtAlertMsg.Text;
				alert.Description = this.txtDesc.Text;	
				alert.MailNotify=this.chbMailNotify.Checked?"Y":"N";
				alert.AlertValue = 0;
				
				if(this._actionType == "add")
					_alertFacade.AddAlert(alert);
				else
					_alertFacade.UpdateAlert(alert);

				//增加接收人
				//if(alert != null)
				//{
					object[] objs = _alertFacade.QueryAlertManualNotifier(alert.AlertID);
					if(objs != null)
					{
						foreach(object obj in objs)
						{
							BenQGuru.eMES.Domain.Alert.AlertManualNotifier an = obj as BenQGuru.eMES.Domain.Alert.AlertManualNotifier;
							if(an != null)
								_alertFacade.DeleteAlertManualNotifier(an);
						}
					}
				//}

				//if(this.chbMailNotify.Checked)
				//{
					for(int i=0;i<this.lstUser.Items.Count;i++)
					{
						if(this.lstUser.Items[i].Value != string.Empty)
						{
							BenQGuru.eMES.Domain.Alert.AlertManualNotifier an = new BenQGuru.eMES.Domain.Alert.AlertManualNotifier();
							an.AlertID = Convert.ToInt32(alert.AlertID);
							an.UserCode = this.lstUser.Items[i].Value;
							_alertFacade.AddAlertManualNotifier(an);
						}
					}
				//}

				if(this.chbMailNotify.Checked)
				{
					this.SendMail(alert);	
				}

				DataProvider.CommitTransaction();
			}
			catch(System.Exception ex)
			{
				DataProvider.RollbackTransaction();
				throw ex;
			}
		}
		protected bool ValidateInput()
		{
			string ie = BenQGuru.eMES.Web.Helper.MessageCenter.ParserMessage("$Error_Input_Empty",this.languageComponent1);
			
			if(this.chbMailNotify.Checked)
			{
				if(this.lstUser.Items.Count == 0)
				{
					WebInfoPublish.Publish(this,this.lblReceiver.Text + " " + ie,this.languageComponent1);
					return false;
				}
			}

			PageCheckManager manager = new PageCheckManager();
			
			manager.Add(new LengthCheck(this.lblAlertMsg,this.txtAlertMsg,1000,true));
			
			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,this.languageComponent1);
				return false;
			}

			return true;
		}

		protected void cmdAddUser_Click(object sender, System.EventArgs e)
		{
			BenQGuru.eMES.BaseSetting.UserFacade  userFacade =null;
			string[] users = this.stbUser.Text.Trim().Split(',');
			if(users.Length > 0)
				userFacade = new BenQGuru.eMES.BaseSetting.UserFacade(DataProvider);

			for(int i=0;i<users.Length;i++)
			{
				if(this.lstUser.Items.FindByValue(users[i]) == null)
				{
					BenQGuru.eMES.Domain.BaseSetting.User user = (BenQGuru.eMES.Domain.BaseSetting.User)userFacade.GetUser(users[i]);
					if(user != null)
						lstUser.Items.Add(new ListItem(user.UserCode + "(" + user.UserEmail + ")",user.UserCode));
				}
					
			}
		}

		protected void cmdDeleteUser_Click(object sender, System.EventArgs e)
		{
			this.lstUser.Items.Remove(this.lstUser.SelectedItem);
			this.stbUser.Text = string.Empty;
			string[] users = new string[this.lstUser.Items.Count];
			for(int i = 0;i<this.lstUser.Items.Count;i++)
			{
				users[i] = this.lstUser.Items[i].Value;
			}

			this.stbUser.Text = String.Join(",",users);
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(MakeRedirectUrl("FManualAlertMP.aspx"));
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("FAlertSampleSP.aspx");
		}

		private void SendMail(BenQGuru.eMES.Domain.Alert.Alert alert)
		{
			
			BenQGuru.eMES.Web.Helper.ESmtpMail mail = this.GetNewMail();
			
			//收件人
			BenQGuru.eMES.BaseSetting.UserFacade userfacade = new BenQGuru.eMES.BaseSetting.UserFacade(DataProvider);

			int recipientCount = 0;
			foreach(ListItem li in this.lstUser.Items)
			{
				if(li.Value != null && li.Value != string.Empty)
				{
					BenQGuru.eMES.Domain.BaseSetting.User user = userfacade.GetUser(li.Value) as BenQGuru.eMES.Domain.BaseSetting.User;
					if(user != null && user.UserEmail != null && user.UserEmail != string.Empty && user.UserEmail.IndexOf("@")!=-1)
					{
						mail.AddRecipient(user.UserName, user.UserEmail);
						recipientCount ++;
					}
				}
			}
			
			if(recipientCount > 0)
			{
				mail.Body = alert.AlertMsg;

				if(!mail.Send())
					BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType(),mail.ErrorMessage);
			}
		}

		private ESmtpMail GetNewMail()
		{
			if(this._mail == null)
			{
				_mail = new ESmtpMail();

				string fileFullPath = BenQGuru.eMES.Common.Config.ConfigSection.Current.ConfigFileFullPath();
				XmlDocument doc = new System.Xml.XmlDocument();
				doc.Load(fileFullPath);

				XmlNode node = doc.SelectSingleNode("//EmailConfiguration/SMTPServer");
				if(node != null)
					_mail.MailDomain = node.InnerText;

				node = doc.SelectSingleNode("//EmailConfiguration/UserName");
				if(node != null)
					_mail.MailServerUserName = node.InnerText;

				node = doc.SelectSingleNode("//EmailConfiguration/Password");
				if(node != null)
					_mail.MailServerPassWord = node.InnerText;

				node = doc.SelectSingleNode("//EmailConfiguration/FromEmail");
				if(node != null)
					_mail.From = node.InnerText;

				node = doc.SelectSingleNode("//EmailConfiguration/FromName");
				if(node != null)
					_mail.FromName = node.InnerText;

				node = doc.SelectSingleNode("//EmailConfiguration/Subject");
				if(node != null)
					_mail.Subject = node.InnerText;

				_mail.Html = false;
			}

			return this._mail;
		}

	}
}
