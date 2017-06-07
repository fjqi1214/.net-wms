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
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.AlertModel;
#endregion

namespace BenQGuru.eMES.Web.Alert
{
	/// <summary>
	/// ItemMP 的摘要说明。
	/// </summary>
	public partial class FAlertEP : BasePage
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
		private int _alertID;
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
			try
			{
				_alertID = int.Parse(Request.QueryString["alertid"]);
			}
			catch
			{
				_alertID = 0;
			}

			_alertConst = new AlertConst(this.languageComponent1);
			
			if (!IsPostBack)
			{	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				AlertLevelBuilder.Build(this.drpAlertLevel.Items,this._alertConst);
				AlertStatusBuilder.Build(this.drpAlertStatus.Items,this._alertConst);
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
			get{
				if(m_alertFacade == null)
					m_alertFacade = new BenQGuru.eMES.AlertModel.AlertFacade(DataProvider);

			return m_alertFacade;
			}
		}
		private void LoadData()
		{
			BenQGuru.eMES.Domain.Alert.Alert alert = this._alertFacade.GetAlert(this._alertID) as BenQGuru.eMES.Domain.Alert.Alert;
			if(alert != null)
			{
				this.txtAlertType.Text = _alertConst.GetName(alert.AlertType);
				this.txtAlertItem.Text = _alertConst.GetName(alert.AlertItem);
				if(this.txtAlertItem.Text != null && this.txtAlertItem.Text != string.Empty)
					this.lblAlertEdit.Text = this.lblAlertEdit.Text + txtAlertItem.Text;
				else
					this.lblAlertEdit.Text = this.lblAlertEdit.Text + "项值";

				this.txtAlertItemValue.Text = alert.ItemCode;
				this.txtAlertDate.Text = FormatHelper.ToDateString(alert.AlertDate);
				this.txtAlertTime.Text = FormatHelper.ToTimeString(alert.AlertTime);
				this.txtAlertValue.Text = alert.AlertValue.ToString();
				
				this.txtSendUser.Text = alert.SendUser;
				if(alert.AlertStatus != null && alert.AlertStatus != string.Empty)
					this.drpAlertStatus.SelectedValue = alert.AlertStatus;
				
				if(alert.AlertLevel != null && alert.AlertLevel != string.Empty)
					this.drpAlertLevel.SelectedValue = alert.AlertLevel;

				this.txtAlertMsg.Text = alert.AlertMsg;
				this.txtDesc.Text = alert.Description;
				
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
				this.Response.Redirect("FAlertMP.aspx");
			}
		}
		
		private void SaveData()
		{
			try
			{
				DataProvider.BeginTransaction();

				BenQGuru.eMES.Domain.Alert.Alert alert = this._alertFacade.GetAlert(this._alertID) as BenQGuru.eMES.Domain.Alert.Alert;
				if(alert != null)
				{
					alert.AlertLevel = this.drpAlertLevel.SelectedValue;
					alert.AlertStatus = this.drpAlertStatus.SelectedValue;
					alert.Description = this.txtDesc.Text;
					alert.MaintainUser = this.GetUserCode();
					alert.MaintainDate = FormatHelper.TODateInt(DateTime.Now.ToShortDateString());
					alert.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
					_alertFacade.UpdateAlert(alert);

					//写处理记录表
					BenQGuru.eMES.Domain.Alert.AlertHandleLog log = this._alertFacade.CreateNewAlertHandleLog();
					log.AlertID = alert.AlertID;
					log.AlertLevel = alert.AlertLevel;
					log.AlertStatus = alert.AlertStatus;
					log.HandleUser  = alert.MaintainUser;
					log.HandleDate = alert.MaintainDate;
					log.HandleTime = alert.MaintainTime;
					log.HandleMsg = alert.Description;
					log.HandleSeq = _alertFacade.GetNextHandleSeq(alert.AlertID);
					
					BenQGuru.eMES.BaseSetting.UserFacade userfacade = new BenQGuru.eMES.BaseSetting.UserFacade(DataProvider);
					BenQGuru.eMES.Domain.BaseSetting.User user = userfacade.GetUser(log.HandleUser) as BenQGuru.eMES.Domain.BaseSetting.User;
					if(user != null)
						log.UserEmail = user.UserEmail;
					
					_alertFacade.AddAlertHandleLog(log);
				}

				DataProvider.CommitTransaction();
			}
			catch(System.Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				throw ex;
			}
		}
		protected bool ValidateInput()
		{
			string ie = BenQGuru.eMES.Web.Helper.MessageCenter.ParserMessage("$Error_Input_Empty",this.languageComponent1);
			
			PageCheckManager manager = new PageCheckManager();
			
			manager.Add(new LengthCheck(this.lblAlertMsg,this.txtAlertMsg,1000,true));
			
			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,this.languageComponent1);
				return false;
			}

			return true;
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect("FAlertMP.aspx");
		}

	}
}
