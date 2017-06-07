using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO; 
using System.Text; 

using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;


namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FCollectReject 的摘要说明。
	/// </summary>
	public class FCollectReject : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panelButton;
		private Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSet2;
		private Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSet1;
		private UserControl.UCLabelEdit txtMEMO;
		private UserControl.UCLabelEdit txtRunningCard;
		private UserControl.UCButton btnSave;
		private UserControl.UCLabelEdit txtNum;
		private UserControl.UCButton btnClear;
		private UserControl.UCButton btnOutPut;
		private System.Windows.Forms.ListBox lstMessages;
		private UserControl.UCButton btnExit;
		private UserControl.UCErrorCodeSelect errorCodeSelect;
		private ProductInfo product;	
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private UserControl.UCLabelCombox cbxOutLine;
		private UserControl.UCLabelEdit txtMO;
		private UserControl.UCLabelEdit txtItem;
		
		private IDomainDataProvider _domainDataProvider = null;
		public IDomainDataProvider DataProvider
		{
			get
			{
				if (_domainDataProvider == null)
				{
					_domainDataProvider =ApplicationService.Current().DataProvider;
				}

				return _domainDataProvider;
			}
		}

		public FCollectReject()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			//
			UserControl.UIStyleBuilder.FormUI(this);
			UserControl.UIStyleBuilder.UltraOptionSetUI(this.ultraOptionSet2);
			product = new ProductInfo();
		}

		/// <summary>
		/// 获得产品信息
		/// </summary>
		/// <returns></returns>
		private Messages GetProduct()
		{
			Messages productmessages=new Messages ();
			ActionOnLineHelper dataCollect = new ActionOnLineHelper(DataProvider);		
			try
			{
				productmessages.AddMessages( dataCollect.GetIDInfo(txtRunningCard.Value.Trim()));
				if (productmessages.IsSuccess() )
				{  
					product=(ProductInfo)productmessages.GetData().Values[0];		
					
				}			
			}
			catch (Exception e)
			{
				productmessages.Add(new UserControl.Message(e));
			}
			return productmessages;
		}

		private Messages CheckProduct()
		{
			Messages messages=new Messages ();
			try
			{
				messages.AddMessages(GetProduct());
				if (product.LastSimulation !=null )
				{
					txtItem.Value = product.LastSimulation.ItemCode;
					txtMO.Value = product.LastSimulation.MOCode;
					SetErrorCodeList();
                    errorCodeSelect.Focus();
					messages.Add(new UserControl.Message(MessageType.Success ,"$CS_CHECKSUCCESS"));
					btnSave.Enabled = true;
				}
				else
				{
					messages.Add(new UserControl.Message(MessageType.Error ,"$CS_MUSTGOMO"));
				}
			}
			catch (Exception e)
			{
				messages.Add(new UserControl.Message(e));

			}
			return messages;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCollectReject));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorCodeSelect = new UserControl.UCErrorCodeSelect();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMO = new UserControl.UCLabelEdit();
            this.txtItem = new UserControl.UCLabelEdit();
            this.cbxOutLine = new UserControl.UCLabelCombox();
            this.panelButton = new System.Windows.Forms.Panel();
            this.btnSave = new UserControl.UCButton();
            this.txtNum = new UserControl.UCLabelEdit();
            this.btnClear = new UserControl.UCButton();
            this.btnOutPut = new UserControl.UCButton();
            this.lstMessages = new System.Windows.Forms.ListBox();
            this.txtMEMO = new UserControl.UCLabelEdit();
            this.txtRunningCard = new UserControl.UCLabelEdit();
            this.btnExit = new UserControl.UCButton();
            this.ultraOptionSet1 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.ultraOptionSet2 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSet2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.errorCodeSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(832, 264);
            this.panel1.TabIndex = 163;
            // 
            // errorCodeSelect
            // 
            this.errorCodeSelect.AddButtonTop = 94;
            this.errorCodeSelect.BackColor = System.Drawing.Color.Gainsboro;
            this.errorCodeSelect.CanInput = true;
            this.errorCodeSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorCodeSelect.Location = new System.Drawing.Point(0, 0);
            this.errorCodeSelect.Name = "errorCodeSelect";
            this.errorCodeSelect.RemoveButtonTop = 175;
            this.errorCodeSelect.SelectedErrorCodeGroup = null;
            this.errorCodeSelect.Size = new System.Drawing.Size(832, 264);
            this.errorCodeSelect.TabIndex = 0;
            this.errorCodeSelect.EndErrorCodeInput += new System.EventHandler(this.errorCodeSelect_EndErrorCodeInput);
            this.errorCodeSelect.Resize += new System.EventHandler(this.errorCodeSelect_Resize);
            this.errorCodeSelect.SelectedIndexChanged += new System.EventHandler(this.errorCodeSelect_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMO);
            this.groupBox1.Controls.Add(this.txtItem);
            this.groupBox1.Controls.Add(this.cbxOutLine);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(832, 61);
            this.groupBox1.TabIndex = 162;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "产品信息";
            // 
            // txtMO
            // 
            this.txtMO.AllowEditOnlyChecked = true;
            this.txtMO.Caption = "工单";
            this.txtMO.Checked = false;
            this.txtMO.EditType = UserControl.EditTypes.String;
            this.txtMO.Location = new System.Drawing.Point(453, 22);
            this.txtMO.MaxLength = 40;
            this.txtMO.Multiline = false;
            this.txtMO.Name = "txtMO";
            this.txtMO.PasswordChar = '\0';
            this.txtMO.ReadOnly = true;
            this.txtMO.ShowCheckBox = false;
            this.txtMO.Size = new System.Drawing.Size(170, 24);
            this.txtMO.TabIndex = 14;
            this.txtMO.TabNext = true;
            this.txtMO.Value = "";
            this.txtMO.WidthType = UserControl.WidthTypes.Normal;
            this.txtMO.XAlign = 490;
            // 
            // txtItem
            // 
            this.txtItem.AllowEditOnlyChecked = true;
            this.txtItem.Caption = "产品";
            this.txtItem.Checked = false;
            this.txtItem.EditType = UserControl.EditTypes.String;
            this.txtItem.Location = new System.Drawing.Point(265, 22);
            this.txtItem.MaxLength = 40;
            this.txtItem.Multiline = false;
            this.txtItem.Name = "txtItem";
            this.txtItem.PasswordChar = '\0';
            this.txtItem.ReadOnly = true;
            this.txtItem.ShowCheckBox = false;
            this.txtItem.Size = new System.Drawing.Size(170, 24);
            this.txtItem.TabIndex = 13;
            this.txtItem.TabNext = true;
            this.txtItem.Value = "";
            this.txtItem.WidthType = UserControl.WidthTypes.Normal;
            this.txtItem.XAlign = 302;
            // 
            // cbxOutLine
            // 
            this.cbxOutLine.AllowEditOnlyChecked = true;
            this.cbxOutLine.Caption = "线外工序";
            this.cbxOutLine.Checked = false;
            this.cbxOutLine.Location = new System.Drawing.Point(33, 22);
            this.cbxOutLine.Name = "cbxOutLine";
            this.cbxOutLine.SelectedIndex = -1;
            this.cbxOutLine.ShowCheckBox = true;
            this.cbxOutLine.Size = new System.Drawing.Size(210, 24);
            this.cbxOutLine.TabIndex = 11;
            this.cbxOutLine.WidthType = UserControl.WidthTypes.Normal;
            this.cbxOutLine.XAlign = 110;
            // 
            // panelButton
            // 
            this.panelButton.Controls.Add(this.btnSave);
            this.panelButton.Controls.Add(this.txtNum);
            this.panelButton.Controls.Add(this.btnClear);
            this.panelButton.Controls.Add(this.btnOutPut);
            this.panelButton.Controls.Add(this.lstMessages);
            this.panelButton.Controls.Add(this.txtMEMO);
            this.panelButton.Controls.Add(this.txtRunningCard);
            this.panelButton.Controls.Add(this.btnExit);
            this.panelButton.Controls.Add(this.ultraOptionSet1);
            this.panelButton.Controls.Add(this.ultraOptionSet2);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(0, 325);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(832, 208);
            this.panelButton.TabIndex = 160;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.ButtonType = UserControl.ButtonTypes.Save;
            this.btnSave.Caption = "保存";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(500, 37);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 22);
            this.btnSave.TabIndex = 278;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNum
            // 
            this.txtNum.AllowEditOnlyChecked = true;
            this.txtNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNum.Caption = "批退个数";
            this.txtNum.Checked = false;
            this.txtNum.EditType = UserControl.EditTypes.String;
            this.txtNum.Location = new System.Drawing.Point(686, 178);
            this.txtNum.MaxLength = 40;
            this.txtNum.Multiline = false;
            this.txtNum.Name = "txtNum";
            this.txtNum.PasswordChar = '\0';
            this.txtNum.ReadOnly = false;
            this.txtNum.ShowCheckBox = false;
            this.txtNum.Size = new System.Drawing.Size(161, 24);
            this.txtNum.TabIndex = 277;
            this.txtNum.TabNext = true;
            this.txtNum.Value = "0";
            this.txtNum.WidthType = UserControl.WidthTypes.Small;
            this.txtNum.XAlign = 747;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.SystemColors.Control;
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.ButtonType = UserControl.ButtonTypes.None;
            this.btnClear.Caption = "清空";
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Location = new System.Drawing.Point(719, 136);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 22);
            this.btnClear.TabIndex = 276;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnOutPut
            // 
            this.btnOutPut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutPut.BackColor = System.Drawing.SystemColors.Control;
            this.btnOutPut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOutPut.BackgroundImage")));
            this.btnOutPut.ButtonType = UserControl.ButtonTypes.None;
            this.btnOutPut.Caption = "导出";
            this.btnOutPut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOutPut.Location = new System.Drawing.Point(719, 106);
            this.btnOutPut.Name = "btnOutPut";
            this.btnOutPut.Size = new System.Drawing.Size(88, 22);
            this.btnOutPut.TabIndex = 275;
            this.btnOutPut.Click += new System.EventHandler(this.btnOutPut_Click);
            // 
            // lstMessages
            // 
            this.lstMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMessages.Location = new System.Drawing.Point(7, 84);
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.Size = new System.Drawing.Size(685, 108);
            this.lstMessages.TabIndex = 273;
            // 
            // txtMEMO
            // 
            this.txtMEMO.AllowEditOnlyChecked = true;
            this.txtMEMO.Caption = "备注";
            this.txtMEMO.Checked = false;
            this.txtMEMO.EditType = UserControl.EditTypes.String;
            this.txtMEMO.Location = new System.Drawing.Point(7, 37);
            this.txtMEMO.MaxLength = 40;
            this.txtMEMO.Multiline = false;
            this.txtMEMO.Name = "txtMEMO";
            this.txtMEMO.PasswordChar = '\0';
            this.txtMEMO.ReadOnly = false;
            this.txtMEMO.ShowCheckBox = false;
            this.txtMEMO.Size = new System.Drawing.Size(237, 24);
            this.txtMEMO.TabIndex = 9;
            this.txtMEMO.TabNext = true;
            this.txtMEMO.Value = "";
            this.txtMEMO.WidthType = UserControl.WidthTypes.Long;
            this.txtMEMO.XAlign = 44;
            // 
            // txtRunningCard
            // 
            this.txtRunningCard.AllowEditOnlyChecked = true;
            this.txtRunningCard.Caption = "产品序列号";
            this.txtRunningCard.Checked = false;
            this.txtRunningCard.EditType = UserControl.EditTypes.String;
            this.txtRunningCard.Location = new System.Drawing.Point(265, 37);
            this.txtRunningCard.MaxLength = 40;
            this.txtRunningCard.Multiline = false;
            this.txtRunningCard.Name = "txtRunningCard";
            this.txtRunningCard.PasswordChar = '\0';
            this.txtRunningCard.ReadOnly = false;
            this.txtRunningCard.ShowCheckBox = false;
            this.txtRunningCard.Size = new System.Drawing.Size(206, 24);
            this.txtRunningCard.TabIndex = 7;
            this.txtRunningCard.TabNext = true;
            this.txtRunningCard.Value = "";
            this.txtRunningCard.WidthType = UserControl.WidthTypes.Normal;
            this.txtRunningCard.XAlign = 338;
            this.txtRunningCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRunningCard_TxtboxKeyPress);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(604, 37);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 4;
            // 
            // ultraOptionSet1
            // 
            appearance1.FontData.BoldAsString = "False";
            this.ultraOptionSet1.Appearance = appearance1;
            this.ultraOptionSet1.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.ultraOptionSet1.CheckedIndex = 0;
            valueListItem1.DataValue = "ValueListItem0";
            valueListItem1.DisplayText = "批退";
            valueListItem2.DataValue = "ValueListItem1";
            valueListItem2.DisplayText = "取消批退";
            this.ultraOptionSet1.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2});
            this.ultraOptionSet1.Location = new System.Drawing.Point(233, 8);
            this.ultraOptionSet1.Name = "ultraOptionSet1";
            this.ultraOptionSet1.Size = new System.Drawing.Size(100, 15);
            this.ultraOptionSet1.TabIndex = 274;
            this.ultraOptionSet1.Text = "批退";
            this.ultraOptionSet1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.ultraOptionSet1.Visible = false;
            // 
            // ultraOptionSet2
            // 
            appearance3.FontData.BoldAsString = "False";
            this.ultraOptionSet2.Appearance = appearance3;
            this.ultraOptionSet2.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.ultraOptionSet2.CheckedIndex = 1;
            valueListItem3.DataValue = "ValueListItem0";
            valueListItem3.DisplayText = "批号";
            valueListItem4.DataValue = "ValueListItem1";
            valueListItem4.DisplayText = "产品序列号";
            valueListItem5.DataValue = "ValueListItem2";
            valueListItem5.DisplayText = "Carton";
            valueListItem6.DataValue = "ValueListItem3";
            valueListItem6.DisplayText = "栈板";
            this.ultraOptionSet2.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem3,
            valueListItem4,
            valueListItem5,
            valueListItem6});
            this.ultraOptionSet2.Location = new System.Drawing.Point(13, 8);
            this.ultraOptionSet2.Name = "ultraOptionSet2";
            this.ultraOptionSet2.Size = new System.Drawing.Size(200, 15);
            this.ultraOptionSet2.TabIndex = 272;
            this.ultraOptionSet2.Text = "产品序列号";
            this.ultraOptionSet2.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.ultraOptionSet2.Visible = false;
            // 
            // FCollectReject
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(832, 533);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelButton);
            this.Name = "FCollectReject";
            this.Text = "批退采集";
            this.Closed += new System.EventHandler(this.FCollectReject_Closed);
            this.Activated += new System.EventHandler(this.FCollectReject_Activated);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panelButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSet2)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion



		private void txtRunningCard_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{	
				//Laws Lu,2005/08/12,修改 产品序列号不能为空值
				if (txtRunningCard.Value.Trim() == string.Empty)
				{
					ApplicationRun.GetInfoForm().Add("$CS_Please_Input_RunningCard");

					//将焦点移到产品序列号输入框
					txtRunningCard.TextFocus(false, true);
					System.Windows.Forms.SendKeys.Send("+{TAB}");
					
				}
				else
				{
					errorCodeSelect.ClearErrorGroup();
					errorCodeSelect.ClearSelectedErrorCode();
					errorCodeSelect.ClearSelectErrorCode();
					
				
					ApplicationRun.GetInfoForm().Add(CheckProduct());
					
					//Laws Lu,2006/06/21 set focus to error code
					errorCodeSelect.ErrorInpuTextBox.Text = String.Empty;
                    errorCodeSelect.ErrorInpuTextBox.Focus();

//					//Laws Lu,2005/08/12,新增设置焦点
//
//					txtRunningCard.TextFocus(false, true);
					SendKeys.Send("+{TAB}");
				}

			}
			else
			{
				btnSave.Enabled = false;
			}
		}

		private void SetErrorCodeList()
		{
			TSModelFacade tsFacade = new TSModelFacade(this.DataProvider);
			object[] errorCodeGroups = tsFacade.GetErrorCodeGroupByItemCode(txtItem.Value);
			if (errorCodeGroups!= null)
			{
				errorCodeSelect.ClearErrorGroup();
				errorCodeSelect.ClearSelectedErrorCode();
				errorCodeSelect.ClearSelectErrorCode();
				errorCodeSelect.AddErrorGroups(errorCodeGroups);
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{			
			Messages messages = new Messages();
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.OpenConnection();
			DataProvider.BeginTransaction();
			try
			{
				messages = CheckErrorCodes();
				if (messages.IsSuccess())
				{
					messages = RunReject();
				}
				if (messages.IsSuccess())
				{
					DataProvider.CommitTransaction();
					int i = int.Parse(txtNum.Value)+1;
					txtNum.Value = i.ToString();
					lstMessages.Items.Add("产品序列号:"+txtRunningCard.Value.Trim()+":判退");
				}
				else
				{
					DataProvider.RollbackTransaction();
				}
				ApplicationRun.GetInfoForm().Add(messages);
			}
			catch (Exception exp)
			{
				DataProvider.RollbackTransaction();
				messages.Add(new UserControl.Message(exp));
				ApplicationRun.GetInfoForm().Add(messages);
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
			}

			if (messages.IsSuccess()) 
			{
				ClearForm();
			}
		}

		private Messages CheckErrorCodes()
		{
			Messages megs = new  Messages();
			megs.Add(new UserControl.Message(MessageType.Debug, "$CS_Debug"+" CheckErrorCodes()"));
			if (errorCodeSelect.Count == 0 )
				megs.Add(new UserControl.Message(MessageType.Error, "$CS_Please_Select_ErrorCode"));
			return megs;
		}


		private void ClearForm()
		{
			txtRunningCard.Value = string.Empty;
			txtMEMO.Value = string.Empty;
			txtItem.Value = string.Empty;
			txtMO.Value = string.Empty;
			errorCodeSelect.ClearErrorGroup();
			errorCodeSelect.ClearSelectedErrorCode();
			errorCodeSelect.ClearSelectErrorCode();
			btnSave.Enabled = false;

			if(!cbxOutLine.Checked)
			{
				InitializeOutLineOP();
			}

			txtRunningCard.TextFocus(false, true);
            //Remove UCLabel.SelectAll;
		}

		/// <summary>
		/// Reject指令采集
		/// </summary>
		/// <returns></returns>
		private Messages RunReject()
		{
			Messages messages = new Messages ();
			try
			{
				object[] ErrorCodes = GetSelectedErrorCodes();//取不良代码组＋不良代码
				if(cbxOutLine.Checked == true)
				{
					if (CheckOutlineOPInRoute())
					{
						messages.Add(new UserControl.Message(MessageType.Error ,"$CS_OutLineOP_In_ThisRoute"));
						return messages;
					}

					//added by jessie lee, 2005/12/12, 判断是否是最后一道工序
					if( IsLastOP( product.LastSimulation.MOCode, product.LastSimulation.RouteCode, product.LastSimulation.OPCode ) )
					{
						messages.Add(new UserControl.Message(MessageType.Error ,"$CS_Op_IsLast_OutLineOP_Cannot_Collect"));
						return messages;
					}


					IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_OutLineReject);

					TSActionEventArgs actionArgs = new TSActionEventArgs(ActionType.DataCollectAction_OutLineReject,
						txtRunningCard.Value.Trim(),
						ApplicationService.Current().UserCode,
						ApplicationService.Current().ResourceCode,
						product,
						ErrorCodes, 
						null,
						txtMEMO.Value);
					actionArgs.OPCode = cbxOutLine.SelectedItemText;
					messages.AddMessages( dataCollectModule.Execute(actionArgs));
				}
				else
				{
					IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_Reject);
					messages.AddMessages( dataCollectModule.Execute(
						new TSActionEventArgs(ActionType.DataCollectAction_Reject,
						txtRunningCard.Value.Trim(),
						ApplicationService.Current().UserCode,
						ApplicationService.Current().ResourceCode,
						product,
						ErrorCodes, 
						null,
						txtMEMO.Value)));
				}
				if (messages.IsSuccess())
				{
					messages.Add(new UserControl.Message(MessageType.Success ,"$CS_RejectSUCCESS"));
				}
				return messages;
			}
			catch (Exception e)
			{
				messages.Add(new UserControl.Message(e));
				return messages;
			}
		}

		private object[] GetSelectedErrorCodes()
		{
			object[] result = errorCodeSelect.GetSelectedErrorCodes();
			return result;
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			lstMessages.Items.Clear();
			txtNum.Value = "0";
		}

		private void errorCodeSelect_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TSModelFacade tsFacade = new TSModelFacade(this.DataProvider);
			object[] errorCodes = tsFacade.GetSelectedErrorCodeByErrorCodeGroupCode(errorCodeSelect.SelectedErrorCodeGroup,string.Empty,1,int.MaxValue);
			if (errorCodes!= null)
			{
				errorCodeSelect.ClearSelectErrorCode();
				errorCodeSelect.AddErrorCodes(errorCodes);
			}
		}

		private void btnOutPut_Click(object sender, System.EventArgs e)
		{
			
			SaveFileDialog saveFileDialog=new SaveFileDialog(); 
			saveFileDialog.Filter="文本文件|*.txt|所有文件|*.*"; 
			saveFileDialog.FilterIndex=0; 
			saveFileDialog.RestoreDirectory=true; 
			if(saveFileDialog.ShowDialog()==DialogResult.OK) 
			{ 
				string fName=saveFileDialog.FileName; 
				File fSaveAs=new File(fName); 
				string str = string.Empty;
				for (int i=0;i<lstMessages.Items.Count;i++)
				{
					str = str+lstMessages.Items[i]+'\r'+'\n';
				}
				fSaveAs.WriteFile(str); 
			} 			
		}

		private void FCollectReject_Closed(object sender, System.EventArgs e)
		{
			if (_domainDataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();  
		}

		private void FCollectReject_Activated(object sender, System.EventArgs e)
		{
			InitializeOutLineOP();
			txtRunningCard.TextFocus(false, true);
		}

		private void InitializeOutLineOP()
		{
			//初始化线外工序下拉框。
			BaseModelFacade bsmodel = new BaseModelFacade(this.DataProvider);
			object[] oplist = bsmodel.GetAllOutLineOperationsByResource(ApplicationService.Current().ResourceCode);
			cbxOutLine.Clear();
			if (oplist != null)
			{
				for (int i=0;i<oplist.Length;i++)
				{
					cbxOutLine.AddItem(((Operation)oplist[i]).OPCode,"");
				}
			}
		}

		private bool IsLastOP(string moCode,string routeCode,string opCode)
		{
			if (routeCode==string.Empty)
				return false;
			DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);

			return dataCollectFacade.OPIsMORouteLastOP(moCode,routeCode,opCode);
		}

		private bool CheckOutlineOPInRoute()
		{
			BaseModelFacade bsmodel = new BaseModelFacade(this.DataProvider);
			return bsmodel.IsOperationInRoute(product.LastSimulation.RouteCode,cbxOutLine.SelectedItemText);
		}

		private void errorCodeSelect_EndErrorCodeInput(object sender, System.EventArgs e)
		{
			btnSave_Click(sender,e);

			//Laws Lu,2006/06/07	执行后初始化界面显示
			//ClearFormMessages();
				
			txtRunningCard.TextFocus(true, true);
		}

		private void errorCodeSelect_Resize(object sender, System.EventArgs e)
		{
			errorCodeSelect.AutoAdjustButtonLocation();
		}

	}

	/// 
	/// Summary description for File. 
	/// 
	public class File 
	{ 
		string fileName; 
		public File(string fileName) 
		{ 
			this.fileName=fileName; 
		} 

		public string ReadFile() 
		{ 
			try 
			{ 
				StreamReader sr=new StreamReader(fileName,Encoding.Default); 
				string result=sr.ReadToEnd(); 
				sr.Close(); 
				return result; 
			} 
			catch(Exception e){ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error,"$CS_System_Error "+e.Message));} 
			return null; 
		} 

		public void WriteFile(string str) 
		{ 
			try 
			{ 
				StreamWriter sw=new StreamWriter(fileName,false,Encoding.Default); 
				sw.Write(str); 
				sw.Close(); 
			} 
			catch {ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CS_Save_File_Failed"));} 
		} 
	} 


}
