using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

#region Project
using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.DataCollect;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Domain.TS;
#endregion

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FTSInputComplete 的摘要说明。
	/// </summary>
	public class FTSScrap : BaseForm
	{
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox;
		private UserControl.UCButton btnConfirm;
		private UserControl.UCButton btnExit;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox2;
		/// <summary>
		/// 报废
		/// </summary>
		private Infragistics.Win.UltraWinEditors.UltraCheckEditor ultraCheckEditorScrap;
		private System.Windows.Forms.GroupBox groupBox1;
		/// <summary>
		/// 工序
		/// </summary>
		private UserControl.UCLabelCombox ucLabComboxOPCode;
		/// <summary>
		/// 途程
		/// </summary>
		private UserControl.UCLabelEdit ucLabEditRoute;
		/// <summary>
		/// 产品代码
		/// </summary>
		private UserControl.UCLabelEdit ucLabEditItemCode;
		/// <summary>
		/// 工单
		/// </summary>
		private UserControl.UCLabelEdit ucLabEditMOCode;
		/// <summary>
		/// 回流
		/// </summary>
		private Infragistics.Win.UltraWinEditors.UltraCheckEditor ultraCheckEditor1;

		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
        private System.Windows.Forms.Label txtRunningCard;
		private Infragistics.Win.UltraWinEditors.UltraTextEditor rCardEditor;
		private UserControl.UCLabelEdit txtAgentUser;
		private System.Windows.Forms.Label scrapLabel;
		private System.Windows.Forms.TextBox txtScrapCause;
	
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FTSScrap()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTSScrap));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtScrapCause = new System.Windows.Forms.TextBox();
            this.scrapLabel = new System.Windows.Forms.Label();
            this.ultraCheckEditorScrap = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucLabComboxOPCode = new UserControl.UCLabelCombox();
            this.ucLabEditRoute = new UserControl.UCLabelEdit();
            this.ucLabEditItemCode = new UserControl.UCLabelEdit();
            this.ucLabEditMOCode = new UserControl.UCLabelEdit();
            this.ultraCheckEditor1 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtAgentUser = new UserControl.UCLabelEdit();
            this.rCardEditor = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtRunningCard = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.btnConfirm = new UserControl.UCButton();
            this.btnExit = new UserControl.UCButton();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCheckEditorScrap)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCheckEditor1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rCardEditor)).BeginInit();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(526, 229);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtScrapCause);
            this.groupBox2.Controls.Add(this.scrapLabel);
            this.groupBox2.Controls.Add(this.ultraCheckEditorScrap);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(520, 161);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // txtScrapCause
            // 
            this.txtScrapCause.Location = new System.Drawing.Point(86, 25);
            this.txtScrapCause.MaxLength = 100;
            this.txtScrapCause.Multiline = true;
            this.txtScrapCause.Name = "txtScrapCause";
            this.txtScrapCause.Size = new System.Drawing.Size(328, 72);
            this.txtScrapCause.TabIndex = 10;
            this.txtScrapCause.Leave += new System.EventHandler(this.txtScrapCause_Leave);
            this.txtScrapCause.Enter += new System.EventHandler(this.txtScrapCause_Enter);
            // 
            // scrapLabel
            // 
            this.scrapLabel.Location = new System.Drawing.Point(7, 25);
            this.scrapLabel.Name = "scrapLabel";
            this.scrapLabel.Size = new System.Drawing.Size(72, 18);
            this.scrapLabel.TabIndex = 9;
            this.scrapLabel.Text = "报废原因";
            // 
            // ultraCheckEditorScrap
            // 
            this.ultraCheckEditorScrap.Checked = true;
            this.ultraCheckEditorScrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ultraCheckEditorScrap.Location = new System.Drawing.Point(8, 0);
            this.ultraCheckEditorScrap.Name = "ultraCheckEditorScrap";
            this.ultraCheckEditorScrap.Size = new System.Drawing.Size(72, 16);
            this.ultraCheckEditorScrap.TabIndex = 8;
            this.ultraCheckEditorScrap.Text = "报废";
            this.ultraCheckEditorScrap.Visible = false;
            this.ultraCheckEditorScrap.CheckedValueChanged += new System.EventHandler(this.ultraCheckEditor2_CheckedValueChanged);
            this.ultraCheckEditorScrap.CheckedChanged += new System.EventHandler(this.ultraCheckEditor2_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucLabComboxOPCode);
            this.groupBox1.Controls.Add(this.ucLabEditRoute);
            this.groupBox1.Controls.Add(this.ucLabEditItemCode);
            this.groupBox1.Controls.Add(this.ucLabEditMOCode);
            this.groupBox1.Controls.Add(this.ultraCheckEditor1);
            this.groupBox1.Location = new System.Drawing.Point(2, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 79);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // ucLabComboxOPCode
            // 
            this.ucLabComboxOPCode.AllowEditOnlyChecked = true;
            this.ucLabComboxOPCode.Caption = "工位";
            this.ucLabComboxOPCode.Checked = false;
            this.ucLabComboxOPCode.Location = new System.Drawing.Point(239, 45);
            this.ucLabComboxOPCode.Name = "ucLabComboxOPCode";
            this.ucLabComboxOPCode.SelectedIndex = -1;
            this.ucLabComboxOPCode.ShowCheckBox = false;
            this.ucLabComboxOPCode.Size = new System.Drawing.Size(170, 24);
            this.ucLabComboxOPCode.TabIndex = 57;
            this.ucLabComboxOPCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabComboxOPCode.XAlign = 276;
            // 
            // ucLabEditRoute
            // 
            this.ucLabEditRoute.AllowEditOnlyChecked = true;
            this.ucLabEditRoute.AutoSelectAll = false;
            this.ucLabEditRoute.AutoUpper = true;
            this.ucLabEditRoute.Caption = "途程";
            this.ucLabEditRoute.Checked = false;
            this.ucLabEditRoute.EditType = UserControl.EditTypes.String;
            this.ucLabEditRoute.Location = new System.Drawing.Point(24, 45);
            this.ucLabEditRoute.MaxLength = 40;
            this.ucLabEditRoute.Multiline = false;
            this.ucLabEditRoute.Name = "ucLabEditRoute";
            this.ucLabEditRoute.PasswordChar = '\0';
            this.ucLabEditRoute.ReadOnly = true;
            this.ucLabEditRoute.ShowCheckBox = false;
            this.ucLabEditRoute.Size = new System.Drawing.Size(170, 24);
            this.ucLabEditRoute.TabIndex = 55;
            this.ucLabEditRoute.TabNext = true;
            this.ucLabEditRoute.Value = "";
            this.ucLabEditRoute.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEditRoute.XAlign = 61;
            // 
            // ucLabEditItemCode
            // 
            this.ucLabEditItemCode.AllowEditOnlyChecked = true;
            this.ucLabEditItemCode.AutoSelectAll = false;
            this.ucLabEditItemCode.AutoUpper = true;
            this.ucLabEditItemCode.Caption = "产品代码";
            this.ucLabEditItemCode.Checked = false;
            this.ucLabEditItemCode.EditType = UserControl.EditTypes.String;
            this.ucLabEditItemCode.Location = new System.Drawing.Point(217, 16);
            this.ucLabEditItemCode.MaxLength = 40;
            this.ucLabEditItemCode.Multiline = false;
            this.ucLabEditItemCode.Name = "ucLabEditItemCode";
            this.ucLabEditItemCode.PasswordChar = '\0';
            this.ucLabEditItemCode.ReadOnly = true;
            this.ucLabEditItemCode.ShowCheckBox = false;
            this.ucLabEditItemCode.Size = new System.Drawing.Size(194, 24);
            this.ucLabEditItemCode.TabIndex = 54;
            this.ucLabEditItemCode.TabNext = true;
            this.ucLabEditItemCode.Value = "";
            this.ucLabEditItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEditItemCode.XAlign = 278;
            // 
            // ucLabEditMOCode
            // 
            this.ucLabEditMOCode.AllowEditOnlyChecked = true;
            this.ucLabEditMOCode.AutoSelectAll = false;
            this.ucLabEditMOCode.AutoUpper = true;
            this.ucLabEditMOCode.Caption = "工单";
            this.ucLabEditMOCode.Checked = false;
            this.ucLabEditMOCode.EditType = UserControl.EditTypes.String;
            this.ucLabEditMOCode.Location = new System.Drawing.Point(24, 16);
            this.ucLabEditMOCode.MaxLength = 40;
            this.ucLabEditMOCode.Multiline = false;
            this.ucLabEditMOCode.Name = "ucLabEditMOCode";
            this.ucLabEditMOCode.PasswordChar = '\0';
            this.ucLabEditMOCode.ReadOnly = true;
            this.ucLabEditMOCode.ShowCheckBox = false;
            this.ucLabEditMOCode.Size = new System.Drawing.Size(170, 24);
            this.ucLabEditMOCode.TabIndex = 53;
            this.ucLabEditMOCode.TabNext = true;
            this.ucLabEditMOCode.Value = "";
            this.ucLabEditMOCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEditMOCode.XAlign = 61;
            // 
            // ultraCheckEditor1
            // 
            this.ultraCheckEditor1.Location = new System.Drawing.Point(8, 0);
            this.ultraCheckEditor1.Name = "ultraCheckEditor1";
            this.ultraCheckEditor1.Size = new System.Drawing.Size(48, 16);
            this.ultraCheckEditor1.TabIndex = 7;
            this.ultraCheckEditor1.Text = "回流";
            this.ultraCheckEditor1.CheckedValueChanged += new System.EventHandler(this.ultraCheckEditor1_CheckedValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtAgentUser);
            this.panel1.Controls.Add(this.rCardEditor);
            this.panel1.Controls.Add(this.txtRunningCard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(520, 48);
            this.panel1.TabIndex = 0;
            // 
            // txtAgentUser
            // 
            this.txtAgentUser.AllowEditOnlyChecked = true;
            this.txtAgentUser.AutoSelectAll = false;
            this.txtAgentUser.AutoUpper = true;
            this.txtAgentUser.Caption = "代录维修人员";
            this.txtAgentUser.Checked = false;
            this.txtAgentUser.EditType = UserControl.EditTypes.String;
            this.txtAgentUser.Location = new System.Drawing.Point(250, 16);
            this.txtAgentUser.MaxLength = 20;
            this.txtAgentUser.Multiline = false;
            this.txtAgentUser.Name = "txtAgentUser";
            this.txtAgentUser.PasswordChar = '\0';
            this.txtAgentUser.ReadOnly = false;
            this.txtAgentUser.ShowCheckBox = true;
            this.txtAgentUser.Size = new System.Drawing.Size(234, 24);
            this.txtAgentUser.TabIndex = 194;
            this.txtAgentUser.TabNext = false;
            this.txtAgentUser.Value = "";
            this.txtAgentUser.WidthType = UserControl.WidthTypes.Normal;
            this.txtAgentUser.XAlign = 351;
            // 
            // rCardEditor
            // 
            this.rCardEditor.ImageTransparentColor = System.Drawing.Color.Teal;
            this.rCardEditor.Location = new System.Drawing.Point(88, 16);
            this.rCardEditor.MaxLength = 100;
            this.rCardEditor.Name = "rCardEditor";
            this.rCardEditor.Size = new System.Drawing.Size(136, 21);
            this.rCardEditor.TabIndex = 1;
            this.rCardEditor.WordWrap = false;
            this.rCardEditor.TextChanged += new System.EventHandler(this.rCardEditor_TextChanged);
            this.rCardEditor.Leave += new System.EventHandler(this.rCardEditor_Leave);
            this.rCardEditor.Enter += new System.EventHandler(this.rCardEditor_Enter);
            this.rCardEditor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rCardEditor_KeyPress);
            // 
            // txtRunningCard
            // 
            this.txtRunningCard.Location = new System.Drawing.Point(8, 16);
            this.txtRunningCard.Name = "txtRunningCard";
            this.txtRunningCard.Size = new System.Drawing.Size(100, 16);
            this.txtRunningCard.TabIndex = 0;
            this.txtRunningCard.Text = "产品序列号";
            this.txtRunningCard.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.btnConfirm);
            this.groupBox.Controls.Add(this.btnExit);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox.Location = new System.Drawing.Point(0, 173);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(526, 56);
            this.groupBox.TabIndex = 170;
            this.groupBox.TabStop = false;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.SystemColors.Control;
            this.btnConfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConfirm.BackgroundImage")));
            this.btnConfirm.ButtonType = UserControl.ButtonTypes.Confirm;
            this.btnConfirm.Caption = "确认";
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.Location = new System.Drawing.Point(120, 16);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(88, 22);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Click += new System.EventHandler(this.ucButton2_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(264, 16);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 0;
            // 
            // FTSScrap
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(526, 229);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.groupBox3);
            this.Name = "FTSScrap";
            this.Text = "维修报废";
            this.Load += new System.EventHandler(this.FTSScrap_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCheckEditorScrap)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraCheckEditor1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rCardEditor)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region event

		private void ultraCheckEditor1_CheckedValueChanged(object sender, EventArgs e)
		{
			if(ultraCheckEditor1.Checked == true)
			{
				if(ultraCheckEditorScrap.Checked == true)
				{
					ultraCheckEditorScrap.Checked = false ;
				}

				Messages messages = new Messages();
				if(rCardEditor.Text.Trim() == String.Empty)
				{
					messages.Add(new UserControl.Message(MessageType.Error,"$CSError_RCard_CanNot_Empty"));
					this.ultraCheckEditor1.Checked = false ;
					ApplicationRun.GetInfoForm().Add(messages);
					return;
				}

				//Laws Lu,2005/09/16,修改	逻辑调整P4.8
				TSFacade tsFacade = new TSFacade(this.DataProvider);

				object obj = tsFacade.GetCardLastTSRecord(rCardEditor.Value.ToString().Trim().ToUpper());

				if(obj == null)
				{
					messages.Add(new UserControl.Message(MessageType.Error,"$CSError_Card_Not_In_TS"));

					ApplicationRun.GetInfoForm().Add(messages);
					return;
				}
				else
				{
					Domain.TS.TS ts = (Domain.TS.TS)obj;

					if(ts.TSStatus == TSStatus.TSStatus_Scrap 
						|| ts.TSStatus == TSStatus.TSStatus_Split 
						|| ts.TSStatus == TSStatus.TSStatus_Reflow
						|| ts.TSStatus == TSStatus.TSStatus_Confirm)
					{
						messages.Add(new UserControl.Message(MessageType.Error,"$CSError_Card_TSStatus_IsNot_TS $Current_Status $"+ts.TSStatus));

						ApplicationRun.GetInfoForm().Add(messages);
						return;
					}
					if(ts.TSStatus != TSStatus.TSStatus_TS)
					{
						messages.Add(new UserControl.Message(MessageType.Error,"$CSError_Card_TSStatus_IsNot_TS $Current_Status $"+ts.TSStatus));
						this.ultraCheckEditor1.Checked = false ;
						ApplicationRun.GetInfoForm().Add(messages);
						return;
					}
					else
					{
						SetRaflowPanel(obj);
					}
				}
//				object obj = tsFacade.GetCardLastTSRecordInTSStatus(rCardEditor.Text.ToUpper().Trim());
//				if(obj == null)
//				{
//					messages.Add(new UserControl. Message(MessageType.Error,"$CSError_Card_TSStatus_IsNot_TS"));
//					this.ultraCheckEditor1.Checked = false ;
//					ApplicationRun.GetInfoForm().Add(messages);
//					return;
//				}
//				else
//				{
//					SetRaflowPanel(obj);
//				}

			}
			else
			{
				ClearReflowPanel();
			}
		}

		private void ultraCheckEditor2_CheckedValueChanged(object sender, EventArgs e)
		{
			if(ultraCheckEditorScrap.Checked == true)
			{
				if(ultraCheckEditor1.Checked == true )
				{
					ultraCheckEditor1.Checked = false ;
				}

				ClearReflowPanel();

				Messages messages = new Messages();
				if(rCardEditor.Text.Trim() == String.Empty)
				{
					messages.Add(new UserControl.Message(MessageType.Error,"$CSError_RCard_CanNot_Empty"));
					this.ultraCheckEditorScrap.Checked = false ;
					ApplicationRun.GetInfoForm().Add(messages);
					return;
				}	
			}
		}

		private void ucButton2_Click(object sender, EventArgs e)
		{
			Messages messages = new Messages();
			if(rCardEditor.Text.Trim() == String.Empty)
			{
				messages.Add(new UserControl.Message(MessageType.Error,"$CSError_RCard_CanNot_Empty"));
				ApplicationRun.GetInfoForm().Add(messages);
				return;
			}

            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(rCardEditor.Text.Trim().ToUpper(), string.Empty);

			string tsStatus;
			//Laws Lu,2005/11/22,新增	添加代理录入人员
			//modified by jessie lee, 2005/11/24
			string userCode = ApplicationService.Current().UserCode;
			if(txtAgentUser.Checked == true && txtAgentUser.Value.Trim().Length == 0)
			{
				messages.Add(new UserControl.Message(MessageType.Error,"$CS_Error_Vicegerent_CanNot_Empty"));
				ApplicationRun.GetInfoForm().Add(messages);
				return ;
			}
			else if(txtAgentUser.Checked == true && txtAgentUser.Value != String.Empty)
			{
				if((new BenQGuru.eMES.Security.SecurityFacade(this.DataProvider)).CheckResourceRight(txtAgentUser.Value.Trim().ToUpper()
					,ApplicationService.Current().ResourceCode))
				{
					userCode = txtAgentUser.Value;
				}
				else
				{
					messages.Add(new UserControl.Message(MessageType.Error,"$CS_Error_Vicegerent_Is_Wrong"));
					ApplicationRun.GetInfoForm().Add(messages);
					return ;
				}
			}

			if(ultraCheckEditor1.Checked == true)
			{
				tsStatus = TSStatus.TSStatus_Reflow;
				
				//勾选回流，然后不工位栏位未选资料也可允许通过。在业务上回流是一定有确定的工位的。
				//系统要检查此时保存，工位是否为空。
				if(ucLabComboxOPCode.SelectedItemText == string.Empty)
				{
					messages.Add(new UserControl.Message(MessageType.Error,"$CS_Error_ReflowOPCode_CanNot_Empty"));
					ApplicationRun.GetInfoForm().Add(messages);
                    ucLabComboxOPCode.Focus();
					return;
				}
			}
			else if(ultraCheckEditorScrap.Checked == true )
			{
				tsStatus = TSStatus.TSStatus_Scrap ; 
			}
			else
			{
				
				tsStatus = TSStatus.TSStatus_Complete ;
//				//修改,Karron Qiu 2005-9-23
//				//线上采集的不良品，修复后则“回流”；修不好则“报废”。而不选“回流”或“报废”代表该产品要入了良品库，
//				//线上的不良品修好后是直接回产线回流，不需要入良品库的
//				messages.Add(new UserControl.Message(MessageType.Error,"$CSError_Please_Select_Reflow_OR_Scrap"));//请选择回流或者报废
//				ApplicationRun.GetInfoForm().Add(messages);
//				return;
			}

			ActionFactory actionFactory = new ActionFactory(this.DataProvider);
			IAction actionTSComplete = actionFactory.CreateAction(ActionType.DataCollectAction_TSComplete); 
			TSActionEventArgs actionEventArgs = new TSActionEventArgs(
				ActionType.DataCollectAction_TSComplete,
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(sourceRCard)),
				userCode,
				ApplicationService.Current().ResourceCode,
				tsStatus,
				this.ucLabEditMOCode.Value,
				this.ucLabEditItemCode.Value,
				this.ucLabEditRoute.Value,
				this.ucLabComboxOPCode.SelectedItemText,
				ApplicationService.Current().UserCode,
				FormatHelper.CleanString( this.txtScrapCause.Text,100 ) );

			//修改 Karron Qiu 2005-9-26
			//在做维修完成处理时，依然按照之前的检查逻辑
			//（不良品是否“已选不良零件”或“已选不良位置”有信息），如果没有，则弹出提示信息，
			//比如：“该不良品无“不良零件”或“不良位置”信息，是否要维修完成”，
			//点击“确认”即维修完成，点击“取消”则维修完成失败

			TSFacade tsFacade = new TSFacade(this.DataProvider);
			object obj = tsFacade.GetCardLastTSRecord(actionEventArgs.RunningCard);

			if(obj == null)
			{
				messages.Add(new UserControl.Message(MessageType.Error,"$CSError_Card_Not_In_TS"));
				ApplicationRun.GetInfoForm().Add(messages);
				return;
			}
			else
			{
				Domain.TS.TS ts = (Domain.TS.TS)obj;
				if(tsFacade.CheckErrorCodeCountAndErrorSolutionForTSComplete(actionEventArgs.RunningCard))
				{
					if(!tsFacade.CheckErrorPartAndErrorLocationForTSComplete(actionEventArgs.RunningCard))
					{
                        if (System.Windows.Forms.MessageBox.Show(null, MutiLanguages.ParserString("$CS_No_ErrorPartAndLoc_Is_Com"), "", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
						{
							return;
						}
					}
				}
				else
				{
                    messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_TSStatus_IsNot_TS $Current_Status $" + ts.TSStatus));
					ApplicationRun.GetInfoForm().Add(messages);
					return;
				}
			}
			//karron qiu ,2005/9/16 ,增加try catch,在catch中添加rollback操作
			DataProvider.BeginTransaction();
			try
			{
				
				messages.AddMessages(actionTSComplete.Execute(actionEventArgs));
				if(!messages.IsSuccess())
				{
					this.DataProvider.RollbackTransaction();
					ApplicationRun.GetInfoForm().Add(messages);
				}
				else
				{
					this.DataProvider.CommitTransaction();
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success,"$CS_TSScrap_SUCCESS"));
				}
			}
			catch
			{
				this.DataProvider.RollbackTransaction();
				throw;
			}
			finally
			{
				(DataProvider as SQLDomainDataProvider).PersistBroker.CloseConnection();
			}
		}

		private void rCardEditor_TextChanged(object sender, EventArgs e)
		{
			ClearReflowPanel();
			//this.ultraCheckEditor1.Checked = false ;
			//this.ultraCheckEditorScrap.Checked = false ;
		}
		private void rCardEditor_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar =='\r')
			{
				Messages messages = new Messages();
				if(rCardEditor.Text.Trim() == String.Empty)
				{
					messages.Add(new UserControl.Message(MessageType.Error,"$CSError_RCard_CanNot_Empty"));
					ApplicationRun.GetInfoForm().Add(messages);
					return;
				}

                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                string sourceRCard = dataCollectFacade.GetSourceCard(rCardEditor.Text.Trim().ToUpper(), string.Empty);

				//Laws Lu,2005/08/25,新增	检查当前资源是否为TS站
				ActionOnLineHelper dataCollect = new ActionOnLineHelper(this._domainDataProvider);
				ActionEventArgs actionEventArgs = new ActionEventArgs(
					ActionType.DataCollectAction_TSConfirm,
					FormatHelper.PKCapitalFormat(FormatHelper.CleanString(sourceRCard)),
					ApplicationService.Current().UserCode,
					ApplicationService.Current().ResourceCode);

				messages.AddMessages(dataCollect.CheckResourceInOperationTS(actionEventArgs));
				//End Laws LU
				if(!messages.IsSuccess())
				{
					ApplicationRun.GetInfoForm().Add(messages);
					return;
				}

//				TSFacade tsFacade = new TSFacade(this.DataProvider);
//				if(!tsFacade.IsCardInTS(rCardEditor.Text.ToUpper().Trim()))
//				{
//					messages.Add(new UserControl.Message (MessageType.Error,"$CSError_Card_Not_In_TS"));
//					ApplicationRun.GetInfoForm().Add(messages);
//					return;
//				}
//
//				object obj = tsFacade.GetCardLastTSRecordInTSStatus(rCardEditor.Text.ToUpper().Trim());
//				if(obj == null)
//				{
//					messages.Add(new UserControl. Message(MessageType.Error,"$CSError_Card_TSStatus_IsNot_TS $Current_Status $"+ts.TSStatus));
//					ApplicationRun.GetInfoForm().Add(messages);
//					return;
//				}
//				BenQGuru.eMES.Domain.TS.TS ts = (BenQGuru.eMES.Domain.TS.TS)obj;
				TSFacade tsFacade = new TSFacade(this.DataProvider);
			
				//Laws Lu,2005/09/16,修改	逻辑调整P4.8
				object obj = tsFacade.GetCardLastTSRecord(actionEventArgs.RunningCard);

				Domain.TS.TS ts = null;
				if(obj == null)
				{
					messages.Add(new UserControl.Message(MessageType.Error,"$CSError_Card_Not_In_TS"));
					ApplicationRun.GetInfoForm().Add(messages);
					return;
				}
				else
				{
					ts = (Domain.TS.TS)obj;

					if(ts.TSStatus == TSStatus.TSStatus_Scrap 
						|| ts.TSStatus == TSStatus.TSStatus_Split 
						|| ts.TSStatus == TSStatus.TSStatus_Reflow
						|| ts.TSStatus == TSStatus.TSStatus_Confirm)
					{
						messages.Add(new UserControl.Message(MessageType.Error,"$CSError_Card_TSStatus_IsNot_TS $Current_Status $"+ts.TSStatus));

						ApplicationRun.GetInfoForm().Add(messages);
						return;
					}
					if(ts.TSStatus != TSStatus.TSStatus_TS)
					{
						messages.Add(new UserControl. Message(MessageType.Error,"$CSError_Card_TSStatus_IsNot_TS $Current_Status $"+ts.TSStatus));
						ApplicationRun.GetInfoForm().Add(messages);
						return;
					}
				}

				if (ts.FromInputType ==TSFacade.TSSource_TS)
				{
					ultraCheckEditor1.Enabled =false;
				}
				else
					ultraCheckEditor1.Enabled=true;
			}
		}
		#endregion

		private void ultraCheckEditor2_CheckedChanged(object sender, System.EventArgs e)
		{
			if( ultraCheckEditorScrap.Checked )
			{
				this.txtScrapCause.ReadOnly = false; 
				this.txtScrapCause.Text = string.Empty;
			}
			else
			{
				this.txtScrapCause.ReadOnly = true; 
				this.txtScrapCause.Text = string.Empty;
			}
		}

		#region Private function 

		private void SetRaflowPanel(object obj)
		{
			ClearReflowPanel();
			this.ucLabEditMOCode.Value = ((BenQGuru.eMES.Domain.TS.TS)obj).MOCode ;
			this.ucLabEditItemCode.Value = ((BenQGuru.eMES.Domain.TS.TS)obj).ItemCode ;
			if(((BenQGuru.eMES.Domain.TS.TS)obj).FromRouteCode != string.Empty)
			{
				this.ucLabEditRoute.Value = ((BenQGuru.eMES.Domain.TS.TS)obj).FromRouteCode;
			}
			else
			{
				TSFacade tsFacade = new TSFacade(this.DataProvider);
				BenQGuru.eMES.Domain.DataCollect.Simulation  simulation = tsFacade.GetSimulation(((BenQGuru.eMES.Domain.TS.TS)obj).RunningCard , ((BenQGuru.eMES.Domain.TS.TS)obj).MOCode );
				if(simulation == null)
				{
					Messages message = new Messages();
					message.Add(new UserControl.Message(MessageType.Error,"$CSError_Card_HasNot_RouteCode"));
					ApplicationRun.GetInfoForm().Add(message);
					ClearReflowPanel();
					this.ultraCheckEditor1.Checked = false ;
					return;
				}
				else
				{
					this.ucLabEditRoute.Value = simulation.FromRoute ;
				}
			}

			BenQGuru.eMES.MOModel.ItemFacade  itemFacade = new BenQGuru.eMES.MOModel.ItemFacade(this.DataProvider);
   			object[] item2Op = itemFacade.QueryItem2Operation(this.ucLabEditItemCode.Value, this.ucLabEditRoute.Value);
			if(item2Op == null)
			{
				return;
			}
			else
			{
				for(int i=0 ; i <item2Op.Length ; i++)
				{
					this.ucLabComboxOPCode.AddItem( ((BenQGuru.eMES.Domain.MOModel.ItemRoute2OP)item2Op[i]).OPCode,((BenQGuru.eMES.Domain.MOModel.ItemRoute2OP)item2Op[i]).OPCode);
				}
			}
		}

		private void ClearReflowPanel()
		{
			this.ucLabEditMOCode.Value = String.Empty ;
			this.ucLabEditItemCode.Value = String.Empty ;
			this.ucLabEditRoute.Value = String.Empty ;
			this.ucLabComboxOPCode.Clear();
		}

		#endregion

		private void txtScrapCause_Enter(object sender, System.EventArgs e)
		{
			txtScrapCause.BackColor = Color.GreenYellow;
		}

		private void txtScrapCause_Leave(object sender, System.EventArgs e)
		{
			txtScrapCause.BackColor = Color.White;
		}

		private void rCardEditor_Enter(object sender, System.EventArgs e)
		{
			rCardEditor.BackColor = Color.GreenYellow;
		}

		private void rCardEditor_Leave(object sender, System.EventArgs e)
		{
			rCardEditor.BackColor = Color.White;
		}

        private void FTSScrap_Load(object sender, EventArgs e)
        {
            //this.InitPageLanguage();
        }
	}
}
