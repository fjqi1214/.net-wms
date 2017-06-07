using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Client.Service;


namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FSPProcess 的摘要说明。
	/// </summary>
	public class FSPManage : System.Windows.Forms.Form
	{
		private UserControl.UCLabelEdit txtMocode;
		private UserControl.UCLabelEdit txtItemCode;
		private UserControl.UCLabelEdit txtLineCode;
		private UserControl.UCButton btnConfrim;
		private UserControl.UCButton btnUnavial;
		private UserControl.UCButton btnRunOut;
		private UserControl.UCButton btnQuit;
		private UserControl.UCButton btnLock;
		private System.Windows.Forms.GroupBox grpAction;
		private UserControl.UCLabelEdit txtMemo;
		private UserControl.UCLabelEdit txtStatus;
		private UserControl.UCLabelEdit txtSolderPasteID;
		private UserControl.UCLabelEdit txtDestinationLine;
		private UserControl.UCLabelEdit txtDestinationMo;
		private UserControl.UCLabelEdit txtReturnUser;
		private UserControl.UCLabelEdit txtOpenUser;
		private System.Windows.Forms.RadioButton radOther;
		private System.Windows.Forms.RadioButton radTransferMo;
		private System.Windows.Forms.RadioButton radReturn;
		private System.Windows.Forms.RadioButton radOpen;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FSPManage()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			UserControl.UIStyleBuilder.FormUI(this);	

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
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

		private IDomainDataProvider _domainDataProvider = Service.ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FSPManage));
			this.txtMocode = new UserControl.UCLabelEdit();
			this.txtItemCode = new UserControl.UCLabelEdit();
			this.txtLineCode = new UserControl.UCLabelEdit();
			this.btnLock = new UserControl.UCButton();
			this.btnConfrim = new UserControl.UCButton();
			this.btnUnavial = new UserControl.UCButton();
			this.btnRunOut = new UserControl.UCButton();
			this.btnQuit = new UserControl.UCButton();
			this.grpAction = new System.Windows.Forms.GroupBox();
			this.txtDestinationLine = new UserControl.UCLabelEdit();
			this.txtDestinationMo = new UserControl.UCLabelEdit();
			this.txtReturnUser = new UserControl.UCLabelEdit();
			this.txtOpenUser = new UserControl.UCLabelEdit();
			this.radOther = new System.Windows.Forms.RadioButton();
			this.radTransferMo = new System.Windows.Forms.RadioButton();
			this.radReturn = new System.Windows.Forms.RadioButton();
			this.radOpen = new System.Windows.Forms.RadioButton();
			this.txtMemo = new UserControl.UCLabelEdit();
			this.txtStatus = new UserControl.UCLabelEdit();
			this.txtSolderPasteID = new UserControl.UCLabelEdit();
			this.grpAction.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtMocode
			// 
			this.txtMocode.AllowEditOnlyChecked = true;
			this.txtMocode.Caption = "工单代码";
			this.txtMocode.Checked = false;
			this.txtMocode.EditType = UserControl.EditTypes.String;
			this.txtMocode.Location = new System.Drawing.Point(8, 16);
			this.txtMocode.MaxLength = 40;
			this.txtMocode.Multiline = false;
			this.txtMocode.Name = "txtMocode";
			this.txtMocode.PasswordChar = '\0';
			this.txtMocode.ReadOnly = false;
			this.txtMocode.ShowCheckBox = false;
			this.txtMocode.Size = new System.Drawing.Size(195, 24);
			this.txtMocode.TabIndex = 1;
			this.txtMocode.TabNext = true;
			this.txtMocode.Value = "";
			this.txtMocode.WidthType = UserControl.WidthTypes.Normal;
			this.txtMocode.XAlign = 70;
			this.txtMocode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMocode_TxtboxKeyPress);
			// 
			// txtItemCode
			// 
			this.txtItemCode.AllowEditOnlyChecked = true;
			this.txtItemCode.Caption = "产品代码";
			this.txtItemCode.Checked = false;
			this.txtItemCode.EditType = UserControl.EditTypes.String;
			this.txtItemCode.Location = new System.Drawing.Point(208, 16);
			this.txtItemCode.MaxLength = 40;
			this.txtItemCode.Multiline = false;
			this.txtItemCode.Name = "txtItemCode";
			this.txtItemCode.PasswordChar = '\0';
			this.txtItemCode.ReadOnly = true;
			this.txtItemCode.ShowCheckBox = false;
			this.txtItemCode.Size = new System.Drawing.Size(195, 24);
			this.txtItemCode.TabIndex = 2;
			this.txtItemCode.TabNext = true;
			this.txtItemCode.TabStop = false;
			this.txtItemCode.Value = "";
			this.txtItemCode.WidthType = UserControl.WidthTypes.Normal;
			this.txtItemCode.XAlign = 270;
			// 
			// txtLineCode
			// 
			this.txtLineCode.AllowEditOnlyChecked = true;
			this.txtLineCode.Caption = "产线代码";
			this.txtLineCode.Checked = false;
			this.txtLineCode.EditType = UserControl.EditTypes.String;
			this.txtLineCode.Location = new System.Drawing.Point(408, 16);
			this.txtLineCode.MaxLength = 40;
			this.txtLineCode.Multiline = false;
			this.txtLineCode.Name = "txtLineCode";
			this.txtLineCode.PasswordChar = '\0';
			this.txtLineCode.ReadOnly = false;
			this.txtLineCode.ShowCheckBox = false;
			this.txtLineCode.Size = new System.Drawing.Size(195, 24);
			this.txtLineCode.TabIndex = 3;
			this.txtLineCode.TabNext = true;
			this.txtLineCode.Value = "";
			this.txtLineCode.WidthType = UserControl.WidthTypes.Normal;
			this.txtLineCode.XAlign = 470;
			this.txtLineCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLineCode_TxtboxKeyPress);
			// 
			// btnLock
			// 
			this.btnLock.BackColor = System.Drawing.SystemColors.Control;
			this.btnLock.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLock.BackgroundImage")));
			this.btnLock.ButtonType = UserControl.ButtonTypes.None;
			this.btnLock.Caption = "锁定";
			this.btnLock.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnLock.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnLock.Location = new System.Drawing.Point(608, 16);
			this.btnLock.Name = "btnLock";
			this.btnLock.Size = new System.Drawing.Size(88, 22);
			this.btnLock.TabIndex = 0;
			this.btnLock.TabStop = false;
			this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
			// 
			// btnConfrim
			// 
			this.btnConfrim.BackColor = System.Drawing.SystemColors.Control;
			this.btnConfrim.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConfrim.BackgroundImage")));
			this.btnConfrim.ButtonType = UserControl.ButtonTypes.None;
			this.btnConfrim.Caption = "确定";
			this.btnConfrim.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnConfrim.Location = new System.Drawing.Point(120, 320);
			this.btnConfrim.Name = "btnConfrim";
			this.btnConfrim.Size = new System.Drawing.Size(88, 22);
			this.btnConfrim.TabIndex = 11;
			this.btnConfrim.TabStop = false;
			this.btnConfrim.Click += new System.EventHandler(this.btnConfrim_Click);
			// 
			// btnUnavial
			// 
			this.btnUnavial.BackColor = System.Drawing.SystemColors.Control;
			this.btnUnavial.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUnavial.BackgroundImage")));
			this.btnUnavial.ButtonType = UserControl.ButtonTypes.None;
			this.btnUnavial.Caption = "报废";
			this.btnUnavial.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnUnavial.Enabled = false;
			this.btnUnavial.Location = new System.Drawing.Point(232, 320);
			this.btnUnavial.Name = "btnUnavial";
			this.btnUnavial.Size = new System.Drawing.Size(88, 22);
			this.btnUnavial.TabIndex = 12;
			this.btnUnavial.TabStop = false;
			this.btnUnavial.Click += new System.EventHandler(this.btnUnavial_Click);
			// 
			// btnRunOut
			// 
			this.btnRunOut.BackColor = System.Drawing.SystemColors.Control;
			this.btnRunOut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRunOut.BackgroundImage")));
			this.btnRunOut.ButtonType = UserControl.ButtonTypes.None;
			this.btnRunOut.Caption = "用完";
			this.btnRunOut.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnRunOut.Enabled = false;
			this.btnRunOut.Location = new System.Drawing.Point(344, 320);
			this.btnRunOut.Name = "btnRunOut";
			this.btnRunOut.Size = new System.Drawing.Size(88, 22);
			this.btnRunOut.TabIndex = 13;
			this.btnRunOut.TabStop = false;
			this.btnRunOut.Click += new System.EventHandler(this.btnRunOut_Click);
			// 
			// btnQuit
			// 
			this.btnQuit.BackColor = System.Drawing.SystemColors.Control;
			this.btnQuit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuit.BackgroundImage")));
			this.btnQuit.ButtonType = UserControl.ButtonTypes.Exit;
			this.btnQuit.Caption = "退出";
			this.btnQuit.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnQuit.Location = new System.Drawing.Point(456, 320);
			this.btnQuit.Name = "btnQuit";
			this.btnQuit.Size = new System.Drawing.Size(88, 22);
			this.btnQuit.TabIndex = 14;
			this.btnQuit.TabStop = false;
			// 
			// grpAction
			// 
			this.grpAction.Controls.Add(this.txtDestinationLine);
			this.grpAction.Controls.Add(this.txtDestinationMo);
			this.grpAction.Controls.Add(this.txtReturnUser);
			this.grpAction.Controls.Add(this.txtOpenUser);
			this.grpAction.Controls.Add(this.radOther);
			this.grpAction.Controls.Add(this.radTransferMo);
			this.grpAction.Controls.Add(this.radReturn);
			this.grpAction.Controls.Add(this.radOpen);
			this.grpAction.Location = new System.Drawing.Point(0, 56);
			this.grpAction.Name = "grpAction";
			this.grpAction.Size = new System.Drawing.Size(704, 200);
			this.grpAction.TabIndex = 4;
			this.grpAction.TabStop = false;
			// 
			// txtDestinationLine
			// 
			this.txtDestinationLine.AllowEditOnlyChecked = true;
			this.txtDestinationLine.Caption = "产线代码";
			this.txtDestinationLine.Checked = false;
			this.txtDestinationLine.EditType = UserControl.EditTypes.String;
			this.txtDestinationLine.Enabled = false;
			this.txtDestinationLine.Location = new System.Drawing.Point(355, 104);
			this.txtDestinationLine.MaxLength = 40;
			this.txtDestinationLine.Multiline = false;
			this.txtDestinationLine.Name = "txtDestinationLine";
			this.txtDestinationLine.PasswordChar = '\0';
			this.txtDestinationLine.ReadOnly = false;
			this.txtDestinationLine.ShowCheckBox = false;
			this.txtDestinationLine.Size = new System.Drawing.Size(195, 24);
			this.txtDestinationLine.TabIndex = 7;
			this.txtDestinationLine.TabNext = true;
			this.txtDestinationLine.Value = "";
			this.txtDestinationLine.WidthType = UserControl.WidthTypes.Normal;
			this.txtDestinationLine.XAlign = 417;
			this.txtDestinationLine.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDestinationLine_TxtboxKeyPress);
			// 
			// txtDestinationMo
			// 
			this.txtDestinationMo.AllowEditOnlyChecked = true;
			this.txtDestinationMo.Caption = "工单代码";
			this.txtDestinationMo.Checked = false;
			this.txtDestinationMo.EditType = UserControl.EditTypes.String;
			this.txtDestinationMo.Enabled = false;
			this.txtDestinationMo.Location = new System.Drawing.Point(147, 104);
			this.txtDestinationMo.MaxLength = 40;
			this.txtDestinationMo.Multiline = false;
			this.txtDestinationMo.Name = "txtDestinationMo";
			this.txtDestinationMo.PasswordChar = '\0';
			this.txtDestinationMo.ReadOnly = false;
			this.txtDestinationMo.ShowCheckBox = false;
			this.txtDestinationMo.Size = new System.Drawing.Size(195, 24);
			this.txtDestinationMo.TabIndex = 5;
			this.txtDestinationMo.TabNext = true;
			this.txtDestinationMo.Value = "";
			this.txtDestinationMo.WidthType = UserControl.WidthTypes.Normal;
			this.txtDestinationMo.XAlign = 209;
			this.txtDestinationMo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDestinationMo_TxtboxKeyPress);
			// 
			// txtReturnUser
			// 
			this.txtReturnUser.AllowEditOnlyChecked = true;
			this.txtReturnUser.Caption = "回存人员";
			this.txtReturnUser.Checked = false;
			this.txtReturnUser.EditType = UserControl.EditTypes.String;
			this.txtReturnUser.Enabled = false;
			this.txtReturnUser.Location = new System.Drawing.Point(147, 64);
			this.txtReturnUser.MaxLength = 40;
			this.txtReturnUser.Multiline = false;
			this.txtReturnUser.Name = "txtReturnUser";
			this.txtReturnUser.PasswordChar = '\0';
			this.txtReturnUser.ReadOnly = false;
			this.txtReturnUser.ShowCheckBox = false;
			this.txtReturnUser.Size = new System.Drawing.Size(195, 24);
			this.txtReturnUser.TabIndex = 5;
			this.txtReturnUser.TabNext = true;
			this.txtReturnUser.Value = "";
			this.txtReturnUser.WidthType = UserControl.WidthTypes.Normal;
			this.txtReturnUser.XAlign = 209;
			// 
			// txtOpenUser
			// 
			this.txtOpenUser.AllowEditOnlyChecked = true;
			this.txtOpenUser.Caption = "领用人员";
			this.txtOpenUser.Checked = false;
			this.txtOpenUser.EditType = UserControl.EditTypes.String;
			this.txtOpenUser.Location = new System.Drawing.Point(147, 24);
			this.txtOpenUser.MaxLength = 40;
			this.txtOpenUser.Multiline = false;
			this.txtOpenUser.Name = "txtOpenUser";
			this.txtOpenUser.PasswordChar = '\0';
			this.txtOpenUser.ReadOnly = false;
			this.txtOpenUser.ShowCheckBox = false;
			this.txtOpenUser.Size = new System.Drawing.Size(195, 24);
			this.txtOpenUser.TabIndex = 5;
			this.txtOpenUser.TabNext = true;
			this.txtOpenUser.Value = "";
			this.txtOpenUser.WidthType = UserControl.WidthTypes.Normal;
			this.txtOpenUser.XAlign = 209;
			// 
			// radOther
			// 
			this.radOther.Location = new System.Drawing.Point(59, 144);
			this.radOther.Name = "radOther";
			this.radOther.Size = new System.Drawing.Size(64, 24);
			this.radOther.TabIndex = 0;
			this.radOther.Tag = "ProcessType";
			this.radOther.Text = "其他";
			this.radOther.CheckedChanged += new System.EventHandler(this.radOther_CheckedChanged);
			// 
			// radTransferMo
			// 
			this.radTransferMo.Location = new System.Drawing.Point(59, 104);
			this.radTransferMo.Name = "radTransferMo";
			this.radTransferMo.TabIndex = 0;
			this.radTransferMo.Tag = "ProcessType";
			this.radTransferMo.Text = "转换工单";
			this.radTransferMo.CheckedChanged += new System.EventHandler(this.radTransferMo_CheckedChanged);
			// 
			// radReturn
			// 
			this.radReturn.Location = new System.Drawing.Point(59, 64);
			this.radReturn.Name = "radReturn";
			this.radReturn.Size = new System.Drawing.Size(64, 24);
			this.radReturn.TabIndex = 0;
			this.radReturn.Tag = "ProcessType";
			this.radReturn.Text = "回存";
			this.radReturn.CheckedChanged += new System.EventHandler(this.radReturn_CheckedChanged);
			// 
			// radOpen
			// 
			this.radOpen.Checked = true;
			this.radOpen.Location = new System.Drawing.Point(59, 24);
			this.radOpen.Name = "radOpen";
			this.radOpen.Size = new System.Drawing.Size(64, 24);
			this.radOpen.TabIndex = 0;
			this.radOpen.Tag = "ProcessType";
			this.radOpen.Text = "领用";
			this.radOpen.CheckedChanged += new System.EventHandler(this.radOpen_CheckedChanged);
			// 
			// txtMemo
			// 
			this.txtMemo.AllowEditOnlyChecked = true;
			this.txtMemo.Caption = "备注";
			this.txtMemo.Checked = false;
			this.txtMemo.EditType = UserControl.EditTypes.String;
			this.txtMemo.Location = new System.Drawing.Point(456, 280);
			this.txtMemo.MaxLength = 40;
			this.txtMemo.Multiline = false;
			this.txtMemo.Name = "txtMemo";
			this.txtMemo.PasswordChar = '\0';
			this.txtMemo.ReadOnly = false;
			this.txtMemo.ShowCheckBox = false;
			this.txtMemo.Size = new System.Drawing.Size(170, 24);
			this.txtMemo.TabIndex = 22;
			this.txtMemo.TabNext = true;
			this.txtMemo.Value = "";
			this.txtMemo.WidthType = UserControl.WidthTypes.Normal;
			this.txtMemo.XAlign = 493;
			// 
			// txtStatus
			// 
			this.txtStatus.AllowEditOnlyChecked = true;
			this.txtStatus.Caption = "状态";
			this.txtStatus.Checked = false;
			this.txtStatus.EditType = UserControl.EditTypes.String;
			this.txtStatus.Location = new System.Drawing.Point(256, 280);
			this.txtStatus.MaxLength = 40;
			this.txtStatus.Multiline = false;
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.PasswordChar = '\0';
			this.txtStatus.ReadOnly = true;
			this.txtStatus.ShowCheckBox = false;
			this.txtStatus.Size = new System.Drawing.Size(170, 24);
			this.txtStatus.TabIndex = 20;
			this.txtStatus.TabNext = true;
			this.txtStatus.Value = "";
			this.txtStatus.WidthType = UserControl.WidthTypes.Normal;
			this.txtStatus.XAlign = 293;
			// 
			// txtSolderPasteID
			// 
			this.txtSolderPasteID.AllowEditOnlyChecked = true;
			this.txtSolderPasteID.Caption = "锡膏ID";
			this.txtSolderPasteID.Checked = false;
			this.txtSolderPasteID.EditType = UserControl.EditTypes.String;
			this.txtSolderPasteID.Location = new System.Drawing.Point(40, 280);
			this.txtSolderPasteID.MaxLength = 40;
			this.txtSolderPasteID.Multiline = false;
			this.txtSolderPasteID.Name = "txtSolderPasteID";
			this.txtSolderPasteID.PasswordChar = '\0';
			this.txtSolderPasteID.ReadOnly = false;
			this.txtSolderPasteID.ShowCheckBox = false;
			this.txtSolderPasteID.Size = new System.Drawing.Size(183, 24);
			this.txtSolderPasteID.TabIndex = 17;
			this.txtSolderPasteID.TabNext = true;
			this.txtSolderPasteID.Value = "";
			this.txtSolderPasteID.WidthType = UserControl.WidthTypes.Normal;
			this.txtSolderPasteID.XAlign = 90;
			this.txtSolderPasteID.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSolderPasteID_TxtboxKeyPress);
			// 
			// FSPManage
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(704, 365);
			this.Controls.Add(this.grpAction);
			this.Controls.Add(this.btnQuit);
			this.Controls.Add(this.btnRunOut);
			this.Controls.Add(this.btnUnavial);
			this.Controls.Add(this.btnConfrim);
			this.Controls.Add(this.btnLock);
			this.Controls.Add(this.txtLineCode);
			this.Controls.Add(this.txtItemCode);
			this.Controls.Add(this.txtMocode);
			this.Controls.Add(this.txtMemo);
			this.Controls.Add(this.txtStatus);
			this.Controls.Add(this.txtSolderPasteID);
			this.Name = "FSPManage";
			this.Text = "锡膏管理";
			this.grpAction.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private bool ShowItem(string moCode)
		{
			return ShowItem(moCode,false);
		}

		private bool ShowItem(string moCode,bool isDestination)
		{
			//Show Item
			bool bResult = true;

			MOFacade moFAC = null;
			//support 3-Tier architecture
			try
			{
				moFAC = (MOFacade)Activator.CreateInstance(typeof(MOFacade)
					,new object[]{DataProvider});
			}
			catch(Exception ex)
			{
				moFAC = new MOFacade(DataProvider);
			}
			
			object objMO = moFAC.GetMO(moCode);

			if(objMO != null )
			{
				if(isDestination)
				{
					txtItemCode.Value = (objMO as Domain.MOModel.MO).ItemCode;
				}
			}
			else
			{
				ApplicationRun.GetInfoForm().Add(
					new UserControl.Message(MessageType.Error,"$CS_MO_Not_Exist"));

				bResult = false;

				Application.DoEvents();

				if(isDestination)
				{
					txtMocode.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
				}
			}
			

			return bResult;
		}

		private bool CheckLine(string lineCode)
		{
			return CheckLine(lineCode,false);
		}

		private bool CheckLine(string lineCode,bool isDestination)
		{
			//Check Line
			bool bResult = true;
			
			BaseSetting.BaseModelFacade basemodelFacade = null;
			//support 3-Tier architecture
			try
			{
				basemodelFacade = (BaseSetting.BaseModelFacade)
					Activator.CreateInstance(typeof(BaseSetting.BaseModelFacade)
					,new object[]{DataProvider});
			}
			catch(Exception ex)
			{
				basemodelFacade = new BaseSetting.BaseModelFacade(DataProvider);
			}

			object objLine = basemodelFacade.GetStepSequence(lineCode);

			if(objLine == null)
			{
				ApplicationRun.GetInfoForm().Add(
					new UserControl.Message(MessageType.Error,"$CS_STEPSEQUENCE_NOT_EXIST"));

				bResult = false;

				Application.DoEvents();

				if(isDestination)
				{
					txtLineCode.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
				}
			}

			return bResult;
		}

		private void LockMoInput()
		{
			txtMocode.Enabled = false;
			txtLineCode.Enabled = false;
		}

		private void UnLockMoInput()
		{
			txtMocode.Enabled = true;
			txtLineCode.Enabled = true;
		}

		private void btnLock_Click(object sender, System.EventArgs e)
		{
			if(btnLock.Caption == "锁定")
			{
				LockMoInput();
				btnLock.Caption = "解除锁定";
			}
			else
			{
				txtMocode.Enabled = true;
				txtLineCode.Enabled = true;
				btnLock.Caption = "锁定";
			}
		}

		private void radOther_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radOther.Checked == true)
			{
				LockMoInput();
				btnConfrim.Enabled = false;
				btnUnavial.Enabled = true;
				btnRunOut.Enabled = true;

				txtSolderPasteID.TextFocus(false, true);
			}
			else
			{
				btnConfrim.Enabled = true;
				btnUnavial.Enabled = false;
				btnRunOut.Enabled = false;
			}
		}

		private void radOpen_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radOpen.Checked == true)
			{
				btnConfrim.Enabled = true;

				if(btnLock.Caption == "解除锁定")
				{
					LockMoInput();
				}
				else
				{
					UnLockMoInput();
				}

				txtOpenUser.Enabled = true;
				txtOpenUser.TextFocus(false, true);
			}
			else
			{
				txtOpenUser.Enabled = false;
			}
		}

		private void radReturn_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radReturn.Checked == true)
			{
				btnConfrim.Enabled = true;

				LockMoInput();
				txtReturnUser.Enabled = true;
				txtReturnUser.TextFocus(false, true);
			}
			else
			{
				txtReturnUser.Enabled = false;
			}
		}

		private void radTransferMo_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radTransferMo.Checked == true)
			{
				btnConfrim.Enabled = true;

				LockMoInput();
				txtDestinationMo.Enabled = true;
				txtDestinationLine.Enabled =true;
				txtDestinationMo.TextFocus(false, true);
			}
			else
			{
				txtDestinationMo.Enabled = false;
				txtDestinationLine.Enabled = false;
			}
		}

		private void txtDestinationMo_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				string moCode =  txtDestinationMo.Value.Trim();
				if(moCode != String.Empty)
				{
					ShowItem(moCode);//show the item code by mocode 
				}
			}
		}

		private void txtDestinationLine_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				string lineCode =  txtDestinationLine.Value.Trim();
				if(lineCode != String.Empty)
				{
					CheckLine(lineCode,true);//Check line 
				}
			}
		}

		private void txtMocode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				string moCode =  txtMocode.Value.ToUpper().Trim();
				if(moCode != String.Empty)
				{
					ShowItem(moCode,true);//show the item code by mocode 
				}
				else
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CS_CMPleaseInputMO"));

					Application.DoEvents();
					txtMocode.TextFocus(false, true);
				}
			}
		}

		private void txtLineCode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				string lineCode =  txtLineCode.Value.ToUpper().Trim();
				if(lineCode != String.Empty)
				{
					if(CheckLine(lineCode))//Check line 
					{
						if(radOpen.Checked)
						{
							Application.DoEvents();
							txtOpenUser.TextFocus(false, true);
							//SendKeys.Send("+{TAB}");
						}
//						if(radReturn.Checked)
//						{
//							Application.DoEvents();
//							txtReturnUser.TextFocus(false, true);
//						}
//						if(radTransferMo.Checked)
//						{
//							Application.DoEvents();
//							txtDestinationMo.TextFocus(false, true);
//						}
					}
					else
					{
						Application.DoEvents();
						txtLineCode.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;
					}

				}
				else
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$Error_SSCode_NotExist"));

					Application.DoEvents();
					txtLineCode.TextFocus(false, true);
				}
			}
		}
		
		private void ClearInputControl()
		{
			txtSolderPasteID.Value = String.Empty;
			txtStatus.Value = String.Empty;
			txtMemo.Value = String.Empty;
		}

		private void btnConfrim_Click(object sender, System.EventArgs e)
		{
			SolderPasteFacade _facade = null;
			try
			{
				_facade = (SolderPasteFacade)Activator.CreateInstance(typeof(SolderPasteFacade),new object[]{DataProvider});

			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));

				_facade = new SolderPasteFacade(DataProvider);

			}

			if(txtSolderPasteID.Value != String.Empty)
			{
				object objSP = _facade.GetUnUseSolderPaste(txtSolderPasteID.Value.Trim());

				if(txtMocode.Value.Trim() == String.Empty)
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CS_CMPleaseInputMO"));

					Application.DoEvents();
					txtMocode.TextFocus(false, true);

					return;
				}

				if(txtLineCode.Value.Trim() == String.Empty)
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$Error_SSCode_NotExist"));

					Application.DoEvents();
					txtLineCode.TextFocus(false, true);

					return;
				}

				if(radOpen.Checked == true)//领用
				{
					if(objSP != null)
					{
						Domain.SolderPaste.SolderPaste sp = objSP as Domain.SolderPaste.SolderPaste;
						Messages msg = new Messages();

					
						#region 锡膏领用
					
						if(sp.ExpiringDate <= FormatHelper.TODateInt(DateTime.Now))//检查实效日期
						{
							msg.Add(new UserControl.Message(MessageType.Error,"$CS_SOLDERPASTE_ALREADY_EXPIRED"));

							#region 4.3.4.1	检查失效日期，如果达到失效日期则不能领用，并变更其状态为限制使用，并记录备注信息为“超出有效期”
							sp.Status = Web.Helper.SolderPasteStatus.Restrain;
							sp.eAttribute1 = "超出有效期";
							DataProvider.BeginTransaction();
							try
							{
								_facade.UpdateSolderPaste(sp);

								DataProvider.CommitTransaction();
							}
							catch
							{
								DataProvider.RollbackTransaction();
							}
							finally
							{
								((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
									.PersistBroker.CloseConnection();
							}
							#endregion
						}
						
						if(sp.Status != Web.Helper.SolderPasteStatus.Reflow && sp.Status != Web.Helper.SolderPasteStatus.Normal)
						{
							msg.Add(new UserControl.Message(MessageType.Error,"$CS_SP_STATUS_MUST_BE_NORMAIL_OR_REFLOW"));
						}

						//获取锡膏类型和产品对应关系实体
						object objSP2Item = null;
						if(msg.IsSuccess())
						{
							objSP2Item = _facade.GetSolderPaste2Item(txtItemCode.Value.Trim());
							//同步产品
							Domain.SolderPaste.SolderPaste2Item sp2item = new BenQGuru.eMES.Domain.SolderPaste.SolderPaste2Item();
							sp2item.ItemCode = txtItemCode.Value.Trim();
							sp2item.MaintainUser = ApplicationService.Current().UserCode;

							if(objSP2Item == null)
							{
								if(txtItemCode.Value.Trim().Length > 1 && char.IsLetter(txtItemCode.Value.Trim(),1))
								{
									sp2item.SolderPasteType = SolderPasteType.Pb_Free;

									_facade.AddSolderPaste2Item(sp2item);

									objSP2Item = sp2item;
								}
								else if(txtItemCode.Value.Trim().Length > 1 && char.IsDigit(txtItemCode.Value.Trim(),1))
								{
									sp2item.SolderPasteType = SolderPasteType.Pb;

									_facade.AddSolderPaste2Item(sp2item);

									objSP2Item = sp2item;
								}
								else
								{
									msg.Add(new UserControl.Message(MessageType.Error,"$CS_SP_ITEM_NOT_MATCH $Domain_SolderPaste_ID = " + sp.SolderPasteID));
								}
							}
						}
						if(msg.IsSuccess())
						{
							#region 4.3.4.2	检查生产日期，适用于当前工单的某类型所有未被使用的正常状态或回存状态的锡膏，当前领用的锡膏是否生产日期最早（或是日期最早的锡膏中的一个）， 如果不是则提示“尚有更早生产日期的锡膏未被领用，是否重新领用？”如果用户确认重新领用则等待用户输入新的锡膏ID

							object[] objFirstInSPs = _facade .GetFirstInSPPByItem(txtItemCode.Value.Trim(),sp.ProductionDate.ToString(),sp.SolderPasteID);

//							bool isInclude = true;
							bool bResave = false;

							string reservedSPID = String.Empty;
							if(objFirstInSPs != null)
							{

								foreach(Domain.SolderPaste.SolderPaste tmp in objFirstInSPs)
								{
//									if(sp.ProductionDate > tmp.ProductionDate)
//									{
//										isInclude = false;
//									}
										
									if(tmp.Status == Web.Helper.SolderPasteStatus.Reflow)//存在状态为回存的锡膏
									{
										reservedSPID = tmp.SolderPasteID;
										bResave = true;
									}
								}

//								if(!isInclude)
//								{	
									if(bResave)
									{
										msg.Add(new UserControl.Message(MessageType.Error,"$CS_SP_EXIST_RESAVE_MUST_BE_USED $Domain_SolderPaste_ID = " + reservedSPID));
									}
									else if(msg.IsSuccess())
									{
										string message = UserControl.MutiLanguages.ParserMessage("$CS_ALREADY_EXIST_SOLDERPASTE");

										if(DialogResult.Yes == MessageBox.Show(this,message,"Confirm",MessageBoxButtons.YesNo))
										{
//											Application.DoEvents();
//											txtSolderPasteID.TextFocus(false, true);
//
//											txtSolderPasteID.SelectAll(1,2,3);

											return;
										}
									}
//								}
							}

							
							#endregion
						}

						if(msg.IsSuccess())
						{
							#region 更新SolderPaste Process
							
							Domain.SolderPaste.SOLDERPASTEPRO spp = _facade.CreateNewSOLDERPASTEPRO();

							object objExistSPP = _facade.GetSOLDERPASTEPROBySPPID(txtSolderPasteID.Value.Trim());
							//是否存在锡膏ID被使用
							if(objExistSPP != null)
							{
								Domain.SolderPaste.SOLDERPASTEPRO sppExist = objExistSPP as Domain.SolderPaste.SOLDERPASTEPRO;
								if(sppExist.STATUS != Web.Helper.SolderPasteStatus.UsedUp 
									&& sppExist.STATUS != Web.Helper.SolderPasteStatus.scrap 
									&& sppExist.STATUS != Web.Helper.SolderPasteStatus.Restrain
									&& sppExist.STATUS != Web.Helper.SolderPasteStatus.Reflow)
								{
									msg.Add(new UserControl.Message(MessageType.Error,"$CS_SOLDERPASTE_ALREADY_USING $Domain_SolderPaste_ID = " + sp.SolderPasteID));
								}
							}

							if(msg.IsSuccess())
							{
								Domain.SolderPaste.SolderPaste2Item sp2item = objSP2Item as Domain.SolderPaste.SolderPaste2Item;
								
								object objSPCTR = _facade.GetSolderPasteControl(sp.PartNO);
								//获取锡膏类型
								if(objSPCTR != null)
								{
									Domain.SolderPaste.SolderPasteControl spCTR = objSPCTR as Domain.SolderPaste.SolderPasteControl;

									spp.RETURNTIMESPAN  = spCTR.ReturnTimeSpan;
									spp.UNVEILTIMESPAN  = spCTR.OpenTimeSpan;
									spp.VEILTIMESPAN  = spCTR.UnOpenTimeSpan;

									spp.SPTYPE = spCTR.Type;
								}
								//比较锡膏类型和产品是否匹配
								if(sp2item.SolderPasteType != spp.SPTYPE)
								{
									msg.Add(new UserControl.Message(MessageType.Error,"$CS_SP_ITEM_NOT_MATCH $Domain_SolderPaste_ID = " + sp.SolderPasteID));
								}

								if(msg.IsSuccess())
								{
									spp.LOTNO = sp.LotNO;
									//sp.Used = "1";//mark sp used
									sp.Status = Web.Helper.SolderPasteStatus.Normal;//change status normal
									sp.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;
									sp.MaintainDate =  FormatHelper.TODateInt(DateTime.Now);
									sp.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);

									//spp.SPRPKID = System.Guid.NewGuid().ToString();
									spp.SOLDERPASTEID = sp.SolderPasteID;
									spp.STATUS = Web.Helper.SolderPasteStatus.Return;

									spp.MUSER = sp.MaintainUser;
									spp.MDATE =  sp.MaintainDate;
									spp.MTIME = sp.MaintainTime;

									spp.OPENUSER = txtOpenUser.Value.Trim();
									spp.OPENDATE = FormatHelper.TODateInt(DateTime.Now);
									spp.OPENTIME = FormatHelper.TOTimeInt(DateTime.Now);

									spp.MOCODE = txtMocode.Value.Trim();
									spp.LINECODE = txtLineCode.Value.Trim();
									spp.EXPIREDDATE = sp.ExpiringDate;

									DataProvider.BeginTransaction();
									try
									{
										_facade.UpdateSolderPaste(sp);
										_facade.AddSOLDERPASTEPRO(spp);
										DataProvider.CommitTransaction();

										ApplicationRun.GetInfoForm().Add(
											new UserControl.Message(MessageType.Success
											,"$CS_SP_OPEN_SUCCESS $Domain_SolderPaste_ID = " + sp.SolderPasteID));
									}
									catch(Exception ex)
									{
										Log.Error(ex.Message);
										ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,ex.Message));
										DataProvider.RollbackTransaction();
									}
									finally
									{
										((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
											.PersistBroker.CloseConnection();
									}
								}
							}
							#endregion
						}

						ApplicationRun.GetInfoForm().Add(msg);
//						txtSolderPasteID.TextFocus(false, true);
//						txtSolderPasteID.SelectAll(1,2,3);
					}
					else
					{
						ApplicationRun.GetInfoForm().Add(
							new UserControl.Message(MessageType.Error
							,"$CS_SOLDERPASTE_NOT_EXIST $Domain_SolderPaste_ID = " + txtSolderPasteID.Value.Trim()));
//						txtSolderPasteID.TextFocus(false, true);
//						txtSolderPasteID.SelectAll(1,2,3);

					}
					#endregion
				}

				#region 回存
				if(radReturn.Checked == true)//回存
				{
					object objNoramlSP = _facade.GetSolderPaste(txtSolderPasteID.Value.Trim());
					object objSPP = _facade.GetSPP(txtSolderPasteID.Value.Trim(),txtMocode.Value.Trim(),txtLineCode.Value.Trim());

					if(objSPP != null && objNoramlSP != null)
					{
						Domain.SolderPaste.SolderPaste spNormal = objNoramlSP as Domain.SolderPaste.SolderPaste;
						Domain.SolderPaste.SOLDERPASTEPRO spp = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;
							
						spNormal.Status = Web.Helper.SolderPasteStatus.Reflow;
						spNormal.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;
						spNormal.MaintainDate =  FormatHelper.TODateInt(DateTime.Now);
						spNormal.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
						//spNormal.Used = "0";

						spp.RESAVEUSER = txtReturnUser.Value.Trim();
						spp.RESAVEDATE = FormatHelper.TODateInt(DateTime.Now);
						spp.RESAVETIME = FormatHelper.TOTimeInt(DateTime.Now);

						spp.MUSER = spNormal.MaintainUser;
						spp.MDATE =  spNormal.MaintainDate;
						spp.MTIME = spNormal.MaintainTime;
						
						if(spp.STATUS == Web.Helper.SolderPasteStatus.Unveil)
						{
							string unveilTime = FormatHelper.TODateTimeString(spp.UNVEILMDATE,spp.UNVEILTIME);
							TimeSpan tsUnveilDate  = DateTime.Now - DateTime.Parse(unveilTime);//开封计时 = 当前时间 - 开封时间

							spp.UNVEILCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsUnveilDate.TotalHours,2));
						}

						if(spp.STATUS == Web.Helper.SolderPasteStatus.Return
							|| spp.STATUS == Web.Helper.SolderPasteStatus.Agitate)
						{
							string openTime = FormatHelper.TODateTimeString(spp.OPENDATE,spp.OPENTIME);
							TimeSpan tsVeilDate  = DateTime.Now - DateTime.Parse(openTime);//未开封计时 = 当前时间 - 回温时间

							spp.VEILCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsVeilDate.TotalHours,2));
						}
						

						DataProvider.BeginTransaction();
						try
						{
							_facade.UpdateSolderPaste(spNormal);


							_facade.DeleteSOLDERPASTEPRO(spp);

							spp.STATUS = spNormal.Status;

							_facade.AddSOLDERPASTEPRO(spp);

							DataProvider.CommitTransaction();

							ApplicationRun.GetInfoForm().Add(
								new UserControl.Message(MessageType.Success
								,"$CS_SP_RESAVE_SUCCESS $Domain_SolderPaste_ID = " + spp.SOLDERPASTEID));
						}
						catch(Exception ex)
						{
							DataProvider.RollbackTransaction();

							ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,ex.Message));
						}
						finally
						{
							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
								.PersistBroker.CloseConnection();
						}
					}
					else
					{
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CS_SOLDERPASTE_NOT_EXIST $Domain_SolderPaste_ID = " + txtSolderPasteID.Value.Trim()));
//						txtSolderPasteID.TextFocus(false, true);
//						txtSolderPasteID.SelectAll(1,2,3);
					}
					
				}
				#endregion
					
				#region 转换工单
				if(radTransferMo.Checked == true)//转换工单
				{
					//object objNoramlSP = _facade.GetSolderPaste(txtSolderPasteID.Value.Trim());
					object objSPP = _facade.GetSPP(txtSolderPasteID.Value.Trim(),txtMocode.Value.Trim(),txtLineCode.Value.Trim());

					if(objSPP != null)
					{
						Domain.SolderPaste.SOLDERPASTEPRO spp = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;

						if(txtDestinationMo.Value.Trim() == spp.MOCODE)
						{
							//允许工单号码相同，但是如果用户输入相同工单提示用户确认。
							string showText = UserControl.MutiLanguages.ParserMessage("$CS_SP_TRANSMO_IS_CURRENT");
							if(DialogResult.No == MessageBox.Show(this,showText,"Notification",MessageBoxButtons.YesNo))
							{
//								txtSolderPasteID.TextFocus(false, true);
//								txtSolderPasteID.SelectAll(1,2,3);
								ClearInputControl();

								return;
							}
						}
					
						//4.3.8.1	新旧产线编号不能一致
						if(txtDestinationLine.Value.Trim() == spp.LINECODE)
						{
							ApplicationRun.GetInfoForm().Add(
								new UserControl.Message(MessageType.Error
								,"$CS_SP_TRANSMO_LINE_REPEAT $Domain_SolderPaste_ID = " + txtSolderPasteID.Value.Trim()));

//							txtSolderPasteID.TextFocus(false, true);
//							txtSolderPasteID.SelectAll(1,2,3);
							ClearInputControl();

							return;
						}
					
						//4.3.8.2	当前锡膏必须是已经被领用到原工单下，且处于开封状态
						if(spp.STATUS != Web.Helper.SolderPasteStatus.Unveil)
						{
							ApplicationRun.GetInfoForm().Add(
								new UserControl.Message(MessageType.Error
								,"$CS_SP_MUST_BE_UNVEIL $Domain_SolderPaste_ID = " + txtSolderPasteID.Value.Trim()));
						}
						else
						{
							//更新锡膏领用记录

							spp.MUSER = ApplicationService.Current().LoginInfo.UserCode;
							spp.MDATE =  FormatHelper.TODateInt(DateTime.Now);
							spp.MTIME = FormatHelper.TOTimeInt(DateTime.Now);

							

							DataProvider.BeginTransaction();
							try
							{
								_facade.DeleteSOLDERPASTEPRO(spp);

								spp.RESAVEUSER = spp.MUSER;
								spp.RESAVEDATE = spp.MDATE;
								spp.RESAVETIME = spp.MTIME;

								spp.STATUS = Web.Helper.SolderPasteStatus.Reflow;

								spp.MEMO = "工单转换";

								_facade.AddSOLDERPASTEPRO(spp);//更新原有使用记录

								spp.MOCODE = txtDestinationMo.Value.Trim();;
								spp.LINECODE = txtDestinationLine.Value.Trim();

								spp.RESAVEUSER = String.Empty;
								spp.RESAVEDATE = 0;
								spp.RESAVETIME = 0;

								spp.STATUS = Web.Helper.SolderPasteStatus.Unveil;

								//spp.MEMO = "工单转换";

								_facade.AddSOLDERPASTEPRO(spp);//添加新的使用记录

								DataProvider.CommitTransaction();

								ApplicationRun.GetInfoForm().Add(
									new UserControl.Message(MessageType.Success
									,"$CS_SP_TRANSMO_SUCCESS $Domain_SolderPaste_ID = " + spp.SOLDERPASTEID
									+ " $Domain_MO = " + txtDestinationMo.Value.Trim()));
							}
							catch(Exception ex)
							{
								DataProvider.RollbackTransaction();
								ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,ex.Message));
							}
							finally
							{
								((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
									.PersistBroker.CloseConnection();
							}
						}
						//							}
					}
					else
					{
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CS_SOLDERPASTE_NOT_EXIST $Domain_SolderPaste_ID = " + txtSolderPasteID.Value.Trim()));
//						txtSolderPasteID.TextFocus(false, true);
//						txtSolderPasteID.SelectAll(1,2,3);
					}
				}
				#endregion

				#region 其他
				if(radOther.Checked == true)
				{
				}
				#endregion
				
				Application.DoEvents();

				txtSolderPasteID.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
				ClearInputControl();
			}
			
		}

		private void btnUnavial_Click(object sender, System.EventArgs e)
		{
			SolderPasteFacade _facade = null;
			try
			{
				_facade = (SolderPasteFacade)Activator.CreateInstance(typeof(SolderPasteFacade),new object[]{DataProvider});

			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));

				_facade = new SolderPasteFacade(DataProvider);

			}


			if(txtSolderPasteID.Value != String.Empty)
			{
				object objSP = _facade.GetSolderPaste(txtSolderPasteID.Value.Trim());

				if(objSP != null)
				{
					Domain.SolderPaste.SolderPaste sp = (objSP as Domain.SolderPaste.SolderPaste);
					object objSPP = _facade.GetSPPForUnavial(txtSolderPasteID.Value.Trim(),txtMocode.Value.Trim(),txtLineCode.Value.Trim());

					#region 报废
					if(objSPP != null)
					{
						Domain.SolderPaste.SOLDERPASTEPRO spp = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;

						//4.3.11.4	除用完、报废状态的锡膏外都可以执行报废
						if(spp.STATUS != Web.Helper.SolderPasteStatus.UsedUp
							&& spp.STATUS != Web.Helper.SolderPasteStatus.scrap)
						{
							sp.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;
							sp.MaintainDate =  FormatHelper.TODateInt(DateTime.Now);
							sp.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
							sp.Status = Web.Helper.SolderPasteStatus.scrap;

							spp.UNAVIALUSER = ApplicationService.Current().LoginInfo.UserCode;
							spp.UNAVIALDATE = FormatHelper.TODateInt(DateTime.Now);
							spp.UNAVIALTIME = FormatHelper.TOTimeInt(DateTime.Now);

							spp.MUSER = sp.MaintainUser;
							spp.MDATE = sp.MaintainDate;
							spp.MTIME = sp.MaintainTime;

							
							DataProvider.BeginTransaction();
							try
							{
								_facade.DeleteSOLDERPASTEPRO(spp);

								spp.STATUS = sp.Status;

								_facade.AddSOLDERPASTEPRO(spp);

								_facade.UpdateSolderPaste(sp);

								DataProvider.CommitTransaction();

								ApplicationRun.GetInfoForm().Add(
									new UserControl.Message(MessageType.Success
									,"$CS_SP_UNAVIAL_SUCCESS  $Domain_SolderPaste_ID = " + spp.SOLDERPASTEID));
							}
							catch(Exception ex)
							{
								DataProvider.RollbackTransaction();

								ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,ex.Message));
							}
							finally
							{
								((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
									.PersistBroker.CloseConnection();
							}
						}
						else
						{
							ApplicationRun.GetInfoForm().Add(
								new UserControl.Message(MessageType.Error
								,"$CS_STATUS_WRONG  $Domain_SolderPaste_ID = " + spp.SOLDERPASTEID
								+ "$Current_Status = " + spp.STATUS));
						}
					}
					else
					{
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CS_SOLDERPASTE_NOT_EXIST"));
//						txtSolderPasteID.TextFocus(false, true);
//						txtSolderPasteID.SelectAll(1,2,3);
					}
					#endregion
				}
				else
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CS_SOLDERPASTE_NOT_EXIST"));
//					txtSolderPasteID.TextFocus(false, true);
//					txtSolderPasteID.SelectAll(1,2,3);
				}
			}

			txtSolderPasteID.TextFocus(false, true);
            //Remove UCLabel.SelectAll;
			ClearInputControl();
		}

		private void btnRunOut_Click(object sender, System.EventArgs e)
		{	
			SolderPasteFacade _facade = null;
			try
			{
				_facade = (SolderPasteFacade)Activator.CreateInstance(typeof(SolderPasteFacade),new object[]{DataProvider});

			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));

				_facade = new SolderPasteFacade(DataProvider);

			}

			if(txtSolderPasteID.Value != String.Empty)
			{
				object objSP = _facade.GetSolderPaste(txtSolderPasteID.Value.Trim());

				if(objSP != null)
				{
					Domain.SolderPaste.SolderPaste sp = (objSP as Domain.SolderPaste.SolderPaste);

					object objSPP = _facade.GetSPP(sp.SolderPasteID,txtMocode.Value.Trim(),txtLineCode.Value.Trim());

					#region 用完
					if(objSPP != null)
					{
						Domain.SolderPaste.SOLDERPASTEPRO spp = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;
						//4.3.12.3	只有领用到当前工单下且处于开封状态的锡膏才可以执行用完操作
						if(spp.STATUS == Web.Helper.SolderPasteStatus.Unveil)
						{
							sp.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;
							sp.MaintainDate =  FormatHelper.TODateInt(DateTime.Now);
							sp.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
							sp.Status = Web.Helper.SolderPasteStatus.UsedUp;

							spp.MUSER = sp.MaintainUser;
							spp.MDATE =  sp.MaintainDate;
							spp.MTIME = sp.MaintainTime;

							string unveilTime = FormatHelper.TODateTimeString(spp.UNVEILMDATE,spp.UNVEILTIME);
							TimeSpan tsUnveilDate  = DateTime.Now - DateTime.Parse(unveilTime);//开封计时 = 当前时间 - 开封时间

							spp.UNVEILCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsUnveilDate.TotalHours,2));

							DataProvider.BeginTransaction();
							try
							{
								_facade.DeleteSOLDERPASTEPRO(spp);

								spp.STATUS = sp.Status;

								_facade.AddSOLDERPASTEPRO(spp);

								_facade.UpdateSolderPaste(sp);

								DataProvider.CommitTransaction();

								ApplicationRun.GetInfoForm().Add(
									new UserControl.Message(MessageType.Success
									,"$CS_SP_RUNOUT_SUCCESS  $Domain_SolderPaste_ID = " + spp.SOLDERPASTEID));
							}
							catch(Exception ex )
							{
								DataProvider.RollbackTransaction();
								ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,ex.Message));
							}
							finally
							{
								((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
									.PersistBroker.CloseConnection();
							}
						}
						else
						{
							ApplicationRun.GetInfoForm().Add(
								new UserControl.Message(MessageType.Error
								,"$CS_STATUS_WRONG  $Domain_SolderPaste_ID = " + spp.SOLDERPASTEID
								+ " $Current_Status = " + UserControl.MutiLanguages.ParserString(spp.STATUS)));
						}
					}
					else
					{
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CS_SOLDERPASTE_NOT_EXIST"));
//						txtSolderPasteID.TextFocus(false, true);
//						txtSolderPasteID.SelectAll(1,2,3);
					}
					#endregion
				}
				else
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CS_SOLDERPASTE_NOT_EXIST"));
//					txtSolderPasteID.TextFocus(false, true);
//					txtSolderPasteID.SelectAll(1,2,3);
				}

			}
			txtSolderPasteID.TextFocus(false, true);
            //Remove UCLabel.SelectAll;
			ClearInputControl();
		}

		private void txtSolderPasteID_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				SolderPasteFacade _facade = null;
				try
				{
					_facade = (SolderPasteFacade)Activator.CreateInstance(typeof(SolderPasteFacade),new object[]{DataProvider});

				}
				catch(Exception ex)
				{
					Log.Error(ex.Message);
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));

					_facade = new SolderPasteFacade(DataProvider);

				}

				if(txtSolderPasteID.Value != String.Empty)
				{
					object objSP = _facade.GetSolderPaste(txtSolderPasteID.Value.Trim());

					if(objSP != null)
					{
						//显示sp信息
						Domain.SolderPaste.SolderPaste sp = objSP as Domain.SolderPaste.SolderPaste;
						txtStatus.Value = UserControl.MutiLanguages.ParserString(sp.Status);
						txtMemo.Value = sp.eAttribute1;

						if(radOpen.Checked == true)
						{
							btnConfrim_Click(sender,e);
						}
						if(radReturn.Checked == true)
						{
							btnConfrim_Click(sender,e);
						}
						if(radTransferMo.Checked == true)
						{
							btnConfrim_Click(sender,e);
						}
					}
					else
					{
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error
							,"$CS_SOLDERPASTE_NOT_EXIST  $Domain_SolderPaste_ID = " + txtSolderPasteID.Value.Trim()));

						Application.DoEvents();
//						txtSolderPasteID.TextFocus(false, true);
//						txtSolderPasteID.SelectAll(1,2,3);
					}
				}
				else
				{
					Application.DoEvents();

//					txtSolderPasteID.TextFocus(false, true);
//					txtSolderPasteID.SelectAll(1,2,3);
				}

				
				if(radOther.Checked != true)
				{
					ClearInputControl();
				}

				Application.DoEvents();
				txtSolderPasteID.TextFocus(false, true);
                //Remove UCLabel.SelectAll;

			
			}
		}
	}
}
