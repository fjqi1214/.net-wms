using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.AlertModel;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FManualAlert 的摘要说明。
	/// </summary>
	public class FManualAlert : System.Windows.Forms.Form
	{

		private System.ComponentModel.Container components = null;
		private string _moCode = string.Empty;
		
		private UserControl.UCLabelEdit txtAlertMsg;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox lbxSample;
		private UserControl.UCButton btnSelect;
		private UserControl.UCLabelCombox cbAlertLevel;
		private System.Windows.Forms.Panel panel1;
		private UserControl.UCButton btnConfirm;

		private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
		private UserControl.UCButton btnRefresh;
		private UserControl.UCLabelEdit txtMOCode;
		private System.Collections.Specialized.StringDictionary _levelDict;

		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		public FManualAlert()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			UserControl.UIStyleBuilder.FormUI(this);
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FManualAlert));
            this.txtAlertMsg = new UserControl.UCLabelEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.lbxSample = new System.Windows.Forms.ListBox();
            this.btnSelect = new UserControl.UCButton();
            this.cbAlertLevel = new UserControl.UCLabelCombox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMOCode = new UserControl.UCLabelEdit();
            this.btnRefresh = new UserControl.UCButton();
            this.btnConfirm = new UserControl.UCButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtAlertMsg
            // 
            this.txtAlertMsg.AllowEditOnlyChecked = true;
            this.txtAlertMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAlertMsg.Caption = "事件描述";
            this.txtAlertMsg.Checked = false;
            this.txtAlertMsg.EditType = UserControl.EditTypes.String;
            this.txtAlertMsg.Location = new System.Drawing.Point(12, 55);
            this.txtAlertMsg.MaxLength = 900;
            this.txtAlertMsg.Multiline = true;
            this.txtAlertMsg.Name = "txtAlertMsg";
            this.txtAlertMsg.PasswordChar = '\0';
            this.txtAlertMsg.ReadOnly = false;
            this.txtAlertMsg.ShowCheckBox = false;
            this.txtAlertMsg.Size = new System.Drawing.Size(220, 394);
            this.txtAlertMsg.TabIndex = 1;
            this.txtAlertMsg.TabNext = true;
            this.txtAlertMsg.Value = "";
            this.txtAlertMsg.WidthType = UserControl.WidthTypes.Long;
            this.txtAlertMsg.XAlign = 65;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(370, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "预警样例";
            // 
            // lbxSample
            // 
            this.lbxSample.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxSample.HorizontalScrollbar = true;
            this.lbxSample.Location = new System.Drawing.Point(400, 55);
            this.lbxSample.Name = "lbxSample";
            this.lbxSample.Size = new System.Drawing.Size(463, 394);
            this.lbxSample.TabIndex = 3;
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.SystemColors.Control;
            this.btnSelect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelect.BackgroundImage")));
            this.btnSelect.ButtonType = UserControl.ButtonTypes.None;
            this.btnSelect.Caption = "<<";
            this.btnSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelect.Location = new System.Drawing.Point(283, 103);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(88, 22);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // cbAlertLevel
            // 
            this.cbAlertLevel.AllowEditOnlyChecked = true;
            this.cbAlertLevel.Caption = "预警级别";
            this.cbAlertLevel.Checked = false;
            this.cbAlertLevel.Location = new System.Drawing.Point(24, 12);
            this.cbAlertLevel.Name = "cbAlertLevel";
            this.cbAlertLevel.SelectedIndex = -1;
            this.cbAlertLevel.ShowCheckBox = false;
            this.cbAlertLevel.Size = new System.Drawing.Size(176, 24);
            this.cbAlertLevel.TabIndex = 0;
            this.cbAlertLevel.WidthType = UserControl.WidthTypes.Normal;
            this.cbAlertLevel.XAlign = 89;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtMOCode);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnConfirm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 481);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(875, 37);
            this.panel1.TabIndex = 7;
            // 
            // txtMOCode
            // 
            this.txtMOCode.AllowEditOnlyChecked = true;
            this.txtMOCode.Caption = "工单号码";
            this.txtMOCode.Checked = false;
            this.txtMOCode.EditType = UserControl.EditTypes.String;
            this.txtMOCode.Location = new System.Drawing.Point(24, 10);
            this.txtMOCode.MaxLength = 40;
            this.txtMOCode.Multiline = false;
            this.txtMOCode.Name = "txtMOCode";
            this.txtMOCode.PasswordChar = '\0';
            this.txtMOCode.ReadOnly = false;
            this.txtMOCode.ShowCheckBox = false;
            this.txtMOCode.Size = new System.Drawing.Size(162, 24);
            this.txtMOCode.TabIndex = 9;
            this.txtMOCode.TabNext = true;
            this.txtMOCode.Value = "";
            this.txtMOCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtMOCode.XAlign = 75;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.SystemColors.Control;
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.BackgroundImage")));
            this.btnRefresh.ButtonType = UserControl.ButtonTypes.None;
            this.btnRefresh.Caption = "刷新样例列表";
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Location = new System.Drawing.Point(775, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(88, 22);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.SystemColors.Control;
            this.btnConfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConfirm.BackgroundImage")));
            this.btnConfirm.ButtonType = UserControl.ButtonTypes.None;
            this.btnConfirm.Caption = "添加";
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.Location = new System.Drawing.Point(233, 9);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(88, 22);
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // FManualAlert
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(875, 518);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lbxSample);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAlertMsg);
            this.Controls.Add(this.cbAlertLevel);
            this.Name = "FManualAlert";
            this.Text = "手动预警";
            this.Load += new System.EventHandler(this.FManualAlert_Load);
            this.Closed += new System.EventHandler(this.FFirstOnline_Closed);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void FFirstOnline_Closed(object sender, System.EventArgs e)
		{
			if (this.DataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();  
		}

		private void CloseConnection()
		{
			if (this.DataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection(); 
		}

		private void FManualAlert_Load(object sender, System.EventArgs e)
		{
			_levelDict = new System.Collections.Specialized.StringDictionary();
            _levelDict.Add(UserControl.MutiLanguages.ParserMessage("$ALERT_" + AlertLevel_Old.Severity), AlertLevel_Old.Severity);
            _levelDict.Add(UserControl.MutiLanguages.ParserMessage("$ALERT_" + AlertLevel_Old.Important), AlertLevel_Old.Important);
            _levelDict.Add(UserControl.MutiLanguages.ParserMessage("$ALERT_" + AlertLevel_Old.Primary), AlertLevel_Old.Primary);

			//预警级别
			this.cbAlertLevel.ComboBoxData.Items.Clear();
			foreach(System.Collections.DictionaryEntry de in _levelDict)
			{
				this.cbAlertLevel.ComboBoxData.Items.Add(de.Key);
			}
			this.cbAlertLevel.SelectedIndex = 0;

			RefreshSample();	
		}

		private void RefreshSample()
		{
			//预警样例
			string currsample = null;
			if(this.lbxSample.SelectedItem != null)
				currsample = this.lbxSample.SelectedItem.ToString();

			this.lbxSample.Items.Clear();
			BenQGuru.eMES.AlertModel.AlertFacade facade = new BenQGuru.eMES.AlertModel.AlertFacade(DataProvider);
			object[] objs = facade.QueryAlertSample();
			if(objs == null) return;
			foreach(object obj in objs)
			{
				BenQGuru.eMES.Domain.Alert.AlertSample sample = obj as BenQGuru.eMES.Domain.Alert.AlertSample;
				if(sample != null)
				{
					this.lbxSample.Items.Add(sample.SampleDesc);	
				}
			}
			
			if(currsample != null && currsample != string.Empty)
				this.lbxSample.SelectedItem = currsample;
		}

		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			if(this.lbxSample.SelectedItem != null)
				this.txtAlertMsg.InnerTextBox.Text += this.lbxSample.SelectedItem.ToString();
		}

		private void btnConfirm_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

				Messages msg = new Messages();
				if(this.txtAlertMsg.InnerTextBox.Text.Trim() == string.Empty)
				{
					msg.Add(new UserControl.Message(MessageType.Error,this.txtAlertMsg.Caption + " $Error_Input_Empty"));
					ApplicationRun.GetInfoForm().Add(msg);
					return;
				}

				if(this.txtMOCode.InnerTextBox.Text.Trim() == string.Empty)
				{
					msg.Add(new UserControl.Message(MessageType.Error,this.txtMOCode.Caption + " $Error_Input_Empty"));
					ApplicationRun.GetInfoForm().Add(msg);
					this.txtMOCode.TextFocus(false, true);
					return;
				}
				
				//根据工单得到产品代码
				BenQGuru.eMES.MOModel.MOFacade moFacade = new MOFacade(this.DataProvider);
				BenQGuru.eMES.Domain.MOModel.MO mo = moFacade.GetMO(this.txtMOCode.InnerTextBox.Text.Trim()) as BenQGuru.eMES.Domain.MOModel.MO;
				string productcode;
				if(mo != null)
				{
					productcode = mo.ItemCode;
				}
				else
				{
					msg.Add(new UserControl.Message(MessageType.Error,"$ManualAlert_MOCode_Error")); //工单号码输入错误
					ApplicationRun.GetInfoForm().Add(msg);
					this.txtMOCode.TextFocus(false, true);
					return;
				}
				try
				{
					DataProvider.BeginTransaction();
				
					BenQGuru.eMES.AlertModel.AlertFacade facade = new BenQGuru.eMES.AlertModel.AlertFacade(DataProvider);
					BenQGuru.eMES.Domain.Alert.Alert alert = facade.CreateNewAlert();
				
					alert.ItemCode =string.Empty;
					alert.AlertItem = string.Empty;
					alert.AlertStatus = BenQGuru.eMES.AlertModel.AlertStatus_Old.Unhandled;
					alert.AlertType = BenQGuru.eMES.AlertModel.AlertType_Old.Manual;
				
					//Laws Lu,2006/11/13 uniform system collect date
					DBDateTime dbDateTime;
					
					dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
					

					alert.MaintainDate = dbDateTime.DBDate;
					alert.MaintainUser = ApplicationService.Current().UserCode;
					alert.MaintainTime = dbDateTime.DBTime;
				
					alert.AlertLevel = this._levelDict[this.cbAlertLevel.ComboBoxData.SelectedItem.ToString()];
					alert.AlertMsg  = ApplicationService.Current().ResourceCode + ":" + this.txtAlertMsg.InnerTextBox.Text;
					alert.Description = string.Empty;
					alert.MailNotify= "N";
					alert.AlertValue = 0;
				
					alert.ProductCode = productcode;
					//单得到产线代码
					BenQGuru.eMES.BaseSetting.BaseModelFacade baseFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
					Resource res = baseFacade.GetResource(ApplicationService.Current().ResourceCode) as Resource;
					if(res != null)
					{
						alert.SSCode = res.StepSequenceCode;
					}

					alert.AlertMsg  = this.txtAlertMsg.InnerTextBox.Text  + "," +
						"产品: " +
						alert.ProductCode + 
						", 资源: " + ApplicationService.Current().ResourceCode;
					facade.AddAlert(alert);
				
					DataProvider.CommitTransaction();

					msg.Add(new UserControl.Message(MessageType.Success,"$ManualAlert_Add_Success"));
					ApplicationRun.GetInfoForm().Add(msg);
					this.txtAlertMsg.InnerTextBox.Text = string.Empty;

					//刷新Sample
					//RefreshSample();
				}
				catch(System.Exception ex)
				{
					DataProvider.RollbackTransaction();
					msg.Add(new UserControl.Message(MessageType.Error,ex.Message));
					ApplicationRun.GetInfoForm().Add(msg);
				}
			}
			finally
			{
				this.Cursor = System.Windows.Forms.Cursors.Default;
				this.CloseConnection();
			}
		}
		
		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
				RefreshSample();
			}
			finally
			{
				this.Cursor = System.Windows.Forms.Cursors.Default;
			}
		}
	}
}
