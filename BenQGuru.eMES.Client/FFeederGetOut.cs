using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FFeederGetOut 的摘要说明。
	/// </summary>
	public class FFeederGetOut : BaseForm
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.RadioButton rdoGetOut;
		private System.Windows.Forms.RadioButton rdoExchange;
		private UserControl.UCButton btnOK;
		private UserControl.UCButton ucBtnExit;
		private UserControl.UCLabelEdit txtFeederNoGetOut;
		private UserControl.UCLabelEdit txtFeederNoExchange;
		private UserControl.UCLabelEdit txtFeederNoExchangeNew;
		private UserControl.UCLabelEdit txtExchangeReason;
		private System.Windows.Forms.CheckBox chkScrapExchange;
		private UserControl.UCLabelEdit txtFeederNoReturn;
		private System.Windows.Forms.RadioButton rdoReturnR;
		private System.Windows.Forms.CheckBox chkScrapReturn;
		private UserControl.UCButton btnOKMOCode;
		private Infragistics.Win.UltraWinGrid.UltraGrid gridList;
		private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
		private UserControl.UCLabelEdit txtMOCode;
		private UserControl.UCLabelEdit txtProductCode;
		private UserControl.UCButton btnReturnAll;
		private System.Windows.Forms.CheckBox chkAutoExchangeFeeder;
		private UserControl.UCLabelEdit txtSSCode;
		private UserControl.UCLabelEdit txtUserCodeGetOut;
        private UserControl.UCLabelEdit txtUserCodeReturn;
        private IContainer components;

		public FFeederGetOut()
		{
			InitializeComponent();
			InitGridColumn();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FFeederGetOut));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSSCode = new UserControl.UCLabelEdit();
            this.txtProductCode = new UserControl.UCLabelEdit();
            this.btnOKMOCode = new UserControl.UCButton();
            this.txtMOCode = new UserControl.UCLabelEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtUserCodeReturn = new UserControl.UCLabelEdit();
            this.txtUserCodeGetOut = new UserControl.UCLabelEdit();
            this.chkAutoExchangeFeeder = new System.Windows.Forms.CheckBox();
            this.btnReturnAll = new UserControl.UCButton();
            this.ucBtnExit = new UserControl.UCButton();
            this.btnOK = new UserControl.UCButton();
            this.chkScrapReturn = new System.Windows.Forms.CheckBox();
            this.txtFeederNoReturn = new UserControl.UCLabelEdit();
            this.rdoReturnR = new System.Windows.Forms.RadioButton();
            this.chkScrapExchange = new System.Windows.Forms.CheckBox();
            this.txtExchangeReason = new UserControl.UCLabelEdit();
            this.txtFeederNoExchangeNew = new UserControl.UCLabelEdit();
            this.txtFeederNoExchange = new UserControl.UCLabelEdit();
            this.rdoExchange = new System.Windows.Forms.RadioButton();
            this.txtFeederNoGetOut = new UserControl.UCLabelEdit();
            this.rdoGetOut = new System.Windows.Forms.RadioButton();
            this.gridList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSSCode);
            this.panel1.Controls.Add(this.txtProductCode);
            this.panel1.Controls.Add(this.btnOKMOCode);
            this.panel1.Controls.Add(this.txtMOCode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(907, 45);
            this.panel1.TabIndex = 0;
            // 
            // txtSSCode
            // 
            this.txtSSCode.AllowEditOnlyChecked = true;
            this.txtSSCode.AutoSelectAll = false;
            this.txtSSCode.AutoUpper = true;
            this.txtSSCode.Caption = "产线代码";
            this.txtSSCode.Checked = false;
            this.txtSSCode.EditType = UserControl.EditTypes.String;
            this.txtSSCode.Location = new System.Drawing.Point(442, 15);
            this.txtSSCode.MaxLength = 40;
            this.txtSSCode.Multiline = false;
            this.txtSSCode.Name = "txtSSCode";
            this.txtSSCode.PasswordChar = '\0';
            this.txtSSCode.ReadOnly = false;
            this.txtSSCode.ShowCheckBox = false;
            this.txtSSCode.Size = new System.Drawing.Size(194, 24);
            this.txtSSCode.TabIndex = 15;
            this.txtSSCode.TabNext = false;
            this.txtSSCode.Value = "";
            this.txtSSCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtSSCode.XAlign = 503;
            this.txtSSCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMOCode_TxtboxKeyPress);
            // 
            // txtProductCode
            // 
            this.txtProductCode.AllowEditOnlyChecked = true;
            this.txtProductCode.AutoSelectAll = false;
            this.txtProductCode.AutoUpper = true;
            this.txtProductCode.Caption = "产品代码";
            this.txtProductCode.Checked = false;
            this.txtProductCode.EditType = UserControl.EditTypes.String;
            this.txtProductCode.Location = new System.Drawing.Point(226, 15);
            this.txtProductCode.MaxLength = 40;
            this.txtProductCode.Multiline = false;
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.PasswordChar = '\0';
            this.txtProductCode.ReadOnly = true;
            this.txtProductCode.ShowCheckBox = false;
            this.txtProductCode.Size = new System.Drawing.Size(194, 24);
            this.txtProductCode.TabIndex = 14;
            this.txtProductCode.TabNext = false;
            this.txtProductCode.Value = "";
            this.txtProductCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtProductCode.XAlign = 287;
            // 
            // btnOKMOCode
            // 
            this.btnOKMOCode.BackColor = System.Drawing.SystemColors.Control;
            this.btnOKMOCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMOCode.BackgroundImage")));
            this.btnOKMOCode.ButtonType = UserControl.ButtonTypes.None;
            this.btnOKMOCode.Caption = "确定";
            this.btnOKMOCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOKMOCode.Location = new System.Drawing.Point(660, 15);
            this.btnOKMOCode.Name = "btnOKMOCode";
            this.btnOKMOCode.Size = new System.Drawing.Size(88, 22);
            this.btnOKMOCode.TabIndex = 13;
            this.btnOKMOCode.TabStop = false;
            this.btnOKMOCode.Click += new System.EventHandler(this.btnOKMOCode_Click);
            // 
            // txtMOCode
            // 
            this.txtMOCode.AllowEditOnlyChecked = true;
            this.txtMOCode.AutoSelectAll = false;
            this.txtMOCode.AutoUpper = true;
            this.txtMOCode.Caption = "工单代码";
            this.txtMOCode.Checked = false;
            this.txtMOCode.EditType = UserControl.EditTypes.String;
            this.txtMOCode.Location = new System.Drawing.Point(12, 15);
            this.txtMOCode.MaxLength = 40;
            this.txtMOCode.Multiline = false;
            this.txtMOCode.Name = "txtMOCode";
            this.txtMOCode.PasswordChar = '\0';
            this.txtMOCode.ReadOnly = false;
            this.txtMOCode.ShowCheckBox = false;
            this.txtMOCode.Size = new System.Drawing.Size(194, 24);
            this.txtMOCode.TabIndex = 2;
            this.txtMOCode.TabNext = false;
            this.txtMOCode.Value = "";
            this.txtMOCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtMOCode.XAlign = 73;
            this.txtMOCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMOCode_TxtboxKeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtUserCodeReturn);
            this.panel2.Controls.Add(this.txtUserCodeGetOut);
            this.panel2.Controls.Add(this.chkAutoExchangeFeeder);
            this.panel2.Controls.Add(this.btnReturnAll);
            this.panel2.Controls.Add(this.ucBtnExit);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.chkScrapReturn);
            this.panel2.Controls.Add(this.txtFeederNoReturn);
            this.panel2.Controls.Add(this.rdoReturnR);
            this.panel2.Controls.Add(this.chkScrapExchange);
            this.panel2.Controls.Add(this.txtExchangeReason);
            this.panel2.Controls.Add(this.txtFeederNoExchangeNew);
            this.panel2.Controls.Add(this.txtFeederNoExchange);
            this.panel2.Controls.Add(this.rdoExchange);
            this.panel2.Controls.Add(this.txtFeederNoGetOut);
            this.panel2.Controls.Add(this.rdoGetOut);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 409);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(907, 149);
            this.panel2.TabIndex = 1;
            // 
            // txtUserCodeReturn
            // 
            this.txtUserCodeReturn.AllowEditOnlyChecked = true;
            this.txtUserCodeReturn.AutoSelectAll = false;
            this.txtUserCodeReturn.AutoUpper = true;
            this.txtUserCodeReturn.Caption = "退回用户";
            this.txtUserCodeReturn.Checked = false;
            this.txtUserCodeReturn.EditType = UserControl.EditTypes.String;
            this.txtUserCodeReturn.Location = new System.Drawing.Point(86, 45);
            this.txtUserCodeReturn.MaxLength = 40;
            this.txtUserCodeReturn.Multiline = false;
            this.txtUserCodeReturn.Name = "txtUserCodeReturn";
            this.txtUserCodeReturn.PasswordChar = '\0';
            this.txtUserCodeReturn.ReadOnly = false;
            this.txtUserCodeReturn.ShowCheckBox = false;
            this.txtUserCodeReturn.Size = new System.Drawing.Size(194, 24);
            this.txtUserCodeReturn.TabIndex = 17;
            this.txtUserCodeReturn.TabNext = false;
            this.txtUserCodeReturn.Value = "";
            this.txtUserCodeReturn.WidthType = UserControl.WidthTypes.Normal;
            this.txtUserCodeReturn.XAlign = 147;
            this.txtUserCodeReturn.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFeederNoReturn_TxtboxKeyPress);
            // 
            // txtUserCodeGetOut
            // 
            this.txtUserCodeGetOut.AllowEditOnlyChecked = true;
            this.txtUserCodeGetOut.AutoSelectAll = false;
            this.txtUserCodeGetOut.AutoUpper = true;
            this.txtUserCodeGetOut.Caption = "领用用户";
            this.txtUserCodeGetOut.Checked = false;
            this.txtUserCodeGetOut.EditType = UserControl.EditTypes.String;
            this.txtUserCodeGetOut.Location = new System.Drawing.Point(86, 15);
            this.txtUserCodeGetOut.MaxLength = 40;
            this.txtUserCodeGetOut.Multiline = false;
            this.txtUserCodeGetOut.Name = "txtUserCodeGetOut";
            this.txtUserCodeGetOut.PasswordChar = '\0';
            this.txtUserCodeGetOut.ReadOnly = false;
            this.txtUserCodeGetOut.ShowCheckBox = false;
            this.txtUserCodeGetOut.Size = new System.Drawing.Size(194, 24);
            this.txtUserCodeGetOut.TabIndex = 16;
            this.txtUserCodeGetOut.TabNext = false;
            this.txtUserCodeGetOut.Value = "";
            this.txtUserCodeGetOut.WidthType = UserControl.WidthTypes.Normal;
            this.txtUserCodeGetOut.XAlign = 147;
            this.txtUserCodeGetOut.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFeederNoGetOut_TxtboxKeyPress);
            // 
            // chkAutoExchangeFeeder
            // 
            this.chkAutoExchangeFeeder.Location = new System.Drawing.Point(585, 14);
            this.chkAutoExchangeFeeder.Name = "chkAutoExchangeFeeder";
            this.chkAutoExchangeFeeder.Size = new System.Drawing.Size(51, 23);
            this.chkAutoExchangeFeeder.TabIndex = 15;
            this.chkAutoExchangeFeeder.Text = "自动";
            this.chkAutoExchangeFeeder.Visible = false;
            this.chkAutoExchangeFeeder.CheckedChanged += new System.EventHandler(this.chkAutoExchangeFeeder_CheckedChanged);
            // 
            // btnReturnAll
            // 
            this.btnReturnAll.BackColor = System.Drawing.SystemColors.Control;
            this.btnReturnAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReturnAll.BackgroundImage")));
            this.btnReturnAll.ButtonType = UserControl.ButtonTypes.None;
            this.btnReturnAll.Caption = "全部退回";
            this.btnReturnAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturnAll.Location = new System.Drawing.Point(509, 44);
            this.btnReturnAll.Name = "btnReturnAll";
            this.btnReturnAll.Size = new System.Drawing.Size(88, 22);
            this.btnReturnAll.TabIndex = 14;
            this.btnReturnAll.TabStop = false;
            this.btnReturnAll.Click += new System.EventHandler(this.btnReturnAll_Click);
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(347, 111);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 13;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.ButtonType = UserControl.ButtonTypes.None;
            this.btnOK.Caption = "确定";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Location = new System.Drawing.Point(207, 111);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 22);
            this.btnOK.TabIndex = 12;
            this.btnOK.TabStop = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkScrapReturn
            // 
            this.chkScrapReturn.Location = new System.Drawing.Point(286, 75);
            this.chkScrapReturn.Name = "chkScrapReturn";
            this.chkScrapReturn.Size = new System.Drawing.Size(86, 23);
            this.chkScrapReturn.TabIndex = 11;
            this.chkScrapReturn.Text = "是否停用";
            // 
            // txtFeederNoReturn
            // 
            this.txtFeederNoReturn.AllowEditOnlyChecked = true;
            this.txtFeederNoReturn.AutoSelectAll = false;
            this.txtFeederNoReturn.AutoUpper = true;
            this.txtFeederNoReturn.Caption = "Feeder编号";
            this.txtFeederNoReturn.Checked = false;
            this.txtFeederNoReturn.EditType = UserControl.EditTypes.String;
            this.txtFeederNoReturn.Location = new System.Drawing.Point(286, 45);
            this.txtFeederNoReturn.MaxLength = 40;
            this.txtFeederNoReturn.Multiline = false;
            this.txtFeederNoReturn.Name = "txtFeederNoReturn";
            this.txtFeederNoReturn.PasswordChar = '\0';
            this.txtFeederNoReturn.ReadOnly = false;
            this.txtFeederNoReturn.ShowCheckBox = false;
            this.txtFeederNoReturn.Size = new System.Drawing.Size(206, 24);
            this.txtFeederNoReturn.TabIndex = 10;
            this.txtFeederNoReturn.TabNext = false;
            this.txtFeederNoReturn.Value = "";
            this.txtFeederNoReturn.WidthType = UserControl.WidthTypes.Normal;
            this.txtFeederNoReturn.XAlign = 359;
            this.txtFeederNoReturn.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFeederNoReturn_TxtboxKeyPress);
            // 
            // rdoReturnR
            // 
            this.rdoReturnR.Location = new System.Drawing.Point(20, 45);
            this.rdoReturnR.Name = "rdoReturnR";
            this.rdoReturnR.Size = new System.Drawing.Size(53, 23);
            this.rdoReturnR.TabIndex = 9;
            this.rdoReturnR.Text = "退回";
            this.rdoReturnR.CheckedChanged += new System.EventHandler(this.rdoGetOut_CheckedChanged);
            // 
            // chkScrapExchange
            // 
            this.chkScrapExchange.Checked = true;
            this.chkScrapExchange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkScrapExchange.Location = new System.Drawing.Point(642, 14);
            this.chkScrapExchange.Name = "chkScrapExchange";
            this.chkScrapExchange.Size = new System.Drawing.Size(86, 23);
            this.chkScrapExchange.TabIndex = 8;
            this.chkScrapExchange.Text = "是否停用";
            this.chkScrapExchange.Visible = false;
            // 
            // txtExchangeReason
            // 
            this.txtExchangeReason.AllowEditOnlyChecked = true;
            this.txtExchangeReason.AutoSelectAll = false;
            this.txtExchangeReason.AutoUpper = true;
            this.txtExchangeReason.Caption = "更换原因";
            this.txtExchangeReason.Checked = false;
            this.txtExchangeReason.EditType = UserControl.EditTypes.String;
            this.txtExchangeReason.Location = new System.Drawing.Point(86, 75);
            this.txtExchangeReason.MaxLength = 40;
            this.txtExchangeReason.Multiline = false;
            this.txtExchangeReason.Name = "txtExchangeReason";
            this.txtExchangeReason.PasswordChar = '\0';
            this.txtExchangeReason.ReadOnly = false;
            this.txtExchangeReason.ShowCheckBox = false;
            this.txtExchangeReason.Size = new System.Drawing.Size(194, 24);
            this.txtExchangeReason.TabIndex = 7;
            this.txtExchangeReason.TabNext = false;
            this.txtExchangeReason.Value = "";
            this.txtExchangeReason.WidthType = UserControl.WidthTypes.Normal;
            this.txtExchangeReason.XAlign = 147;
            this.txtExchangeReason.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtExchangeReason_TxtboxKeyPress);
            // 
            // txtFeederNoExchangeNew
            // 
            this.txtFeederNoExchangeNew.AllowEditOnlyChecked = true;
            this.txtFeederNoExchangeNew.AutoSelectAll = false;
            this.txtFeederNoExchangeNew.AutoUpper = true;
            this.txtFeederNoExchangeNew.Caption = "新Feeder编号";
            this.txtFeederNoExchangeNew.Checked = false;
            this.txtFeederNoExchangeNew.EditType = UserControl.EditTypes.String;
            this.txtFeederNoExchangeNew.Location = new System.Drawing.Point(634, 88);
            this.txtFeederNoExchangeNew.MaxLength = 40;
            this.txtFeederNoExchangeNew.Multiline = false;
            this.txtFeederNoExchangeNew.Name = "txtFeederNoExchangeNew";
            this.txtFeederNoExchangeNew.PasswordChar = '\0';
            this.txtFeederNoExchangeNew.ReadOnly = false;
            this.txtFeederNoExchangeNew.ShowCheckBox = false;
            this.txtFeederNoExchangeNew.Size = new System.Drawing.Size(218, 24);
            this.txtFeederNoExchangeNew.TabIndex = 6;
            this.txtFeederNoExchangeNew.TabNext = false;
            this.txtFeederNoExchangeNew.Value = "";
            this.txtFeederNoExchangeNew.Visible = false;
            this.txtFeederNoExchangeNew.WidthType = UserControl.WidthTypes.Normal;
            this.txtFeederNoExchangeNew.XAlign = 719;
            this.txtFeederNoExchangeNew.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFeederNoExchangeNew_TxtboxKeyPress);
            // 
            // txtFeederNoExchange
            // 
            this.txtFeederNoExchange.AllowEditOnlyChecked = true;
            this.txtFeederNoExchange.AutoSelectAll = false;
            this.txtFeederNoExchange.AutoUpper = true;
            this.txtFeederNoExchange.Caption = "Feeder编号";
            this.txtFeederNoExchange.Checked = false;
            this.txtFeederNoExchange.EditType = UserControl.EditTypes.String;
            this.txtFeederNoExchange.Location = new System.Drawing.Point(634, 44);
            this.txtFeederNoExchange.MaxLength = 40;
            this.txtFeederNoExchange.Multiline = false;
            this.txtFeederNoExchange.Name = "txtFeederNoExchange";
            this.txtFeederNoExchange.PasswordChar = '\0';
            this.txtFeederNoExchange.ReadOnly = false;
            this.txtFeederNoExchange.ShowCheckBox = false;
            this.txtFeederNoExchange.Size = new System.Drawing.Size(206, 24);
            this.txtFeederNoExchange.TabIndex = 5;
            this.txtFeederNoExchange.TabNext = false;
            this.txtFeederNoExchange.Value = "";
            this.txtFeederNoExchange.Visible = false;
            this.txtFeederNoExchange.WidthType = UserControl.WidthTypes.Normal;
            this.txtFeederNoExchange.XAlign = 707;
            this.txtFeederNoExchange.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFeederNoExchange_TxtboxKeyPress);
            // 
            // rdoExchange
            // 
            this.rdoExchange.Location = new System.Drawing.Point(509, 13);
            this.rdoExchange.Name = "rdoExchange";
            this.rdoExchange.Size = new System.Drawing.Size(53, 23);
            this.rdoExchange.TabIndex = 4;
            this.rdoExchange.Text = "更换";
            this.rdoExchange.Visible = false;
            this.rdoExchange.CheckedChanged += new System.EventHandler(this.rdoGetOut_CheckedChanged);
            // 
            // txtFeederNoGetOut
            // 
            this.txtFeederNoGetOut.AllowEditOnlyChecked = true;
            this.txtFeederNoGetOut.AutoSelectAll = false;
            this.txtFeederNoGetOut.AutoUpper = true;
            this.txtFeederNoGetOut.Caption = "Feeder编号";
            this.txtFeederNoGetOut.Checked = false;
            this.txtFeederNoGetOut.EditType = UserControl.EditTypes.String;
            this.txtFeederNoGetOut.Location = new System.Drawing.Point(286, 15);
            this.txtFeederNoGetOut.MaxLength = 40;
            this.txtFeederNoGetOut.Multiline = false;
            this.txtFeederNoGetOut.Name = "txtFeederNoGetOut";
            this.txtFeederNoGetOut.PasswordChar = '\0';
            this.txtFeederNoGetOut.ReadOnly = false;
            this.txtFeederNoGetOut.ShowCheckBox = false;
            this.txtFeederNoGetOut.Size = new System.Drawing.Size(206, 24);
            this.txtFeederNoGetOut.TabIndex = 3;
            this.txtFeederNoGetOut.TabNext = false;
            this.txtFeederNoGetOut.Value = "";
            this.txtFeederNoGetOut.WidthType = UserControl.WidthTypes.Normal;
            this.txtFeederNoGetOut.XAlign = 359;
            this.txtFeederNoGetOut.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFeederNoGetOut_TxtboxKeyPress);
            // 
            // rdoGetOut
            // 
            this.rdoGetOut.Location = new System.Drawing.Point(20, 15);
            this.rdoGetOut.Name = "rdoGetOut";
            this.rdoGetOut.Size = new System.Drawing.Size(53, 23);
            this.rdoGetOut.TabIndex = 0;
            this.rdoGetOut.Text = "领用";
            this.rdoGetOut.CheckedChanged += new System.EventHandler(this.rdoGetOut_CheckedChanged);
            // 
            // gridList
            // 
            this.gridList.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridList.DataSource = this.ultraDataSource1;
            this.gridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridList.Location = new System.Drawing.Point(0, 45);
            this.gridList.Name = "gridList";
            this.gridList.Size = new System.Drawing.Size(907, 364);
            this.gridList.TabIndex = 2;
            // 
            // FFeederGetOut
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(907, 558);
            this.Controls.Add(this.gridList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FFeederGetOut";
            this.Text = "Feeder领用/退换";
            this.Load += new System.EventHandler(this.FFeederGetOut_Load);
            this.Closed += new System.EventHandler(this.FFeederGetOut_Closed);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void InitGridColumn()
		{
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeederSpecCode");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Qty");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("GetOutQty");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeederCode");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxCount");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UsedCount");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn1 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("FeederSpecCode");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Qty");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn3 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("GetOutQty");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn4 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("FeederCode");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn5 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("MaxCount");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn6 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("UsedCount");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn7 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Status");
			
			ultraGridColumn1.AutoEdit = false;
			ultraGridColumn1.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn1.Header.Caption = "Feeder规格编号";
			ultraGridColumn2.AutoEdit = false;
			ultraGridColumn2.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn2.Header.Caption = "需求数量";
			ultraGridColumn2.Width = 67;
			ultraGridColumn3.AutoEdit = false;
			ultraGridColumn3.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn3.Header.Caption = "已领用数量";
			ultraGridColumn3.Width = 79;
			ultraGridColumn4.AutoEdit = false;
			ultraGridColumn4.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn4.Header.Caption = "Feeder编号";
			ultraGridColumn5.AutoEdit = false;
			ultraGridColumn5.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn5.Header.Caption = "最大使用次数";
			ultraGridColumn5.Width = 91;
			ultraGridColumn6.AutoEdit = false;
			ultraGridColumn6.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn6.Header.Caption = "已使用次数";
			ultraGridColumn6.Width = 82;
			ultraGridColumn7.AutoEdit = false;
			ultraGridColumn7.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn7.Header.Caption = "当前状态";
			ultraGridColumn7.Width = 94;
			ultraGridBand1.Columns.Add(ultraGridColumn1);
			ultraGridBand1.Columns.Add(ultraGridColumn2);
			ultraGridBand1.Columns.Add(ultraGridColumn3);
			ultraGridBand1.Columns.Add(ultraGridColumn4);
			ultraGridBand1.Columns.Add(ultraGridColumn5);
			ultraGridBand1.Columns.Add(ultraGridColumn6);
			ultraGridBand1.Columns.Add(ultraGridColumn7);
			this.gridList.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			// 
			// ultraDataSource1
			// 
			this.ultraDataSource1.Band.Columns.AddRange(new object[] {
																		 ultraDataColumn1,
																		 ultraDataColumn2,
																		 ultraDataColumn3,
																		 ultraDataColumn4,
																		 ultraDataColumn5,
																		 ultraDataColumn6,
																		 ultraDataColumn7});
		}

		private string checkedMOCode = string.Empty;
		private string checkedSSCode = string.Empty;
		private SMTFacade smtFacade = null;
		private IDomainDataProvider _domainDataProvider = Service.ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		private void CloseConnection()
		{
			if (this.DataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection(); 
		}

		private DataTable tableSource = null;
		private void FFeederGetOut_Load(object sender, System.EventArgs e)
		{
			if (tableSource == null)
			{
				tableSource = new DataTable();
				tableSource.Columns.Add("FeederSpecCode");
				tableSource.Columns.Add("Qty");
				tableSource.Columns.Add("GetOutQty");
				tableSource.Columns.Add("FeederCode");
				tableSource.Columns.Add("MaxCount", typeof(int));
				tableSource.Columns.Add("UsedCount", typeof(int));
				tableSource.Columns.Add("Status");
				tableSource.Columns.Add("StatusValue");
			}
			tableSource.DefaultView.Sort = "FeederSpecCode,FeederCode";
			gridList.DataSource = tableSource.DefaultView;
			gridList.DisplayLayout.Bands[0].Columns["StatusValue"].Hidden = true;
			smtFacade = new SMTFacade(this.DataProvider);
			SetControlEnable();
			txtMOCode.TextFocus(false, true);
			
			UserControl.UIStyleBuilder.FormUI(this);  
			UserControl.UIStyleBuilder.GridUI(gridList);

			gridList.DisplayLayout.Bands[0].Columns["MaxCount"].Format = "#,#";
			gridList.DisplayLayout.Bands[0].Columns["MaxCount"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
			gridList.DisplayLayout.Bands[0].Columns["UsedCount"].Format = "#,#";
			gridList.DisplayLayout.Bands[0].Columns["UsedCount"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            //this.InitPageLanguage();
            //this.InitGridLanguage(gridList);
		}

		private void FFeederGetOut_Closed(object sender, System.EventArgs e)
		{
			this.CloseConnection();
		}

		private void SetControlEnable()
		{
			this.txtFeederNoGetOut.Enabled = false;
			this.txtFeederNoExchange.Enabled = false;
			this.txtFeederNoExchangeNew.Enabled = false;
			this.chkAutoExchangeFeeder.Enabled = false;
			this.txtExchangeReason.Enabled = false;
			this.chkScrapExchange.Enabled = false;
			this.txtFeederNoReturn.Enabled = false;
			this.chkScrapReturn.Enabled = false;
			this.btnReturnAll.Enabled = false;
			this.txtUserCodeGetOut.Enabled = false;
			this.txtUserCodeReturn.Enabled = false;
			if (this.rdoGetOut.Checked == true)
			{
				this.txtFeederNoGetOut.Enabled = true;
				this.txtUserCodeGetOut.Enabled = true;
				this.txtUserCodeGetOut.TextFocus(false, true);
			}
			else if (this.rdoExchange.Checked == true)
			{
				this.txtFeederNoExchange.Enabled = true;
				this.txtFeederNoExchangeNew.Enabled = true;
				this.chkAutoExchangeFeeder.Enabled = true;
				this.txtExchangeReason.Enabled = true;
				this.chkScrapExchange.Enabled = true;
				this.txtFeederNoExchange.TextFocus(false, true);
			}
			else if (this.rdoReturnR.Checked == true)
			{
				this.txtFeederNoReturn.Enabled = true;
				this.chkScrapReturn.Enabled = true;
				this.txtExchangeReason.Enabled = true;
				this.txtUserCodeReturn.Enabled = true;
				this.btnReturnAll.Enabled = true;
				this.txtUserCodeReturn.TextFocus(false, true);
			}
		}

		private void btnOKMOCode_Click(object sender, System.EventArgs e)
		{
			txtMOCode.Value = txtMOCode.Value.Trim().ToUpper();
			if (txtMOCode.Value.Trim() == string.Empty)
			{
				//Application.DoEvents();
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, "$CS_CMPleaseInputMO"));
				txtMOCode.TextFocus(true, true);
				return;
			}
			BenQGuru.eMES.MOModel.MOFacade moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
			BenQGuru.eMES.Domain.MOModel.MO mo = (BenQGuru.eMES.Domain.MOModel.MO)moFacade.GetMO(txtMOCode.Value);
			if (mo == null)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
				return;
			}
			this.txtProductCode.Value = mo.ItemCode;
			if (txtSSCode.Value.Trim() == string.Empty)
			{
				//Application.DoEvents();
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, "$CS_Please_Input_SSCode"));
				txtSSCode.TextFocus(true, true);
				return;
			}
			txtSSCode.Value = txtSSCode.Value.Trim().ToUpper();
			
			tableSource.Rows.Clear();
			Feeder[] feeders = null;
			Messages messages = smtFacade.GetFeederByMOCode(txtMOCode.Value, txtSSCode.Value, ref feeders);
			if (messages != null && messages.IsSuccess() == false)
			{
				ApplicationRun.GetInfoForm().Add(messages);
				txtMOCode.Value = string.Empty;
				txtMOCode.TextFocus(false, true);
				return;
			}
			if (feeders == null)
				return;
			
			Hashtable htStatus = new Hashtable();
			for (int i = 0; i < feeders.Length; i++)
			{
				DataRow row = tableSource.NewRow();
				row["FeederSpecCode"] = feeders[i].FeederSpecCode;
				string[] qty = feeders[i].EAttribute1.Split(',');
				row["Qty"] = qty[0];
				row["GetOutQty"] = qty[1];
				if (feeders[i].FeederCode != null && feeders[i].FeederCode != string.Empty)
				{
					row["FeederCode"] = feeders[i].FeederCode;
					row["MaxCount"] = feeders[i].MaxCount;
					row["UsedCount"] = feeders[i].UsedCount;
					if (htStatus.ContainsKey(feeders[i].Status) == false)
					{
						BenQGuru.eMES.BaseSetting.SystemSettingFacade sysSetting = new BenQGuru.eMES.BaseSetting.SystemSettingFacade(this.DataProvider);
						BenQGuru.eMES.Domain.BaseSetting.Parameter param = (BenQGuru.eMES.Domain.BaseSetting.Parameter)sysSetting.GetParameter(feeders[i].Status.ToUpper(), "FEEDERSTATUS");
						htStatus.Add(feeders[i].Status, param.ParameterAlias);
					}
					row["StatusValue"] = feeders[i].Status;
					row["Status"] = htStatus[feeders[i].Status];
				}
				tableSource.Rows.Add(row);
			}
			checkedMOCode = txtMOCode.Value;
			checkedSSCode = txtSSCode.Value;

			if (sender != null)
			{
				this.rdoGetOut.Checked = true;
				Application.DoEvents();
				this.txtUserCodeGetOut.TextFocus(false, true);
			}
		}

		private void rdoGetOut_CheckedChanged(object sender, System.EventArgs e)
		{
			this.SetControlEnable();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			//Icyer,2007/01/15 修改	减少Open/Close的次数
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			try
			{
				if (checkedMOCode != txtMOCode.Value)
				{
					btnOKMOCode_Click(null, null);
				}
				if (checkedSSCode != txtSSCode.Value)
				{
					btnOKMOCode_Click(null, null);
				}
				if (tableSource == null || tableSource.Rows.Count == 0)
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$SMT_Feeder_GetOut_No_SMTMaterial"));
					return;
				}
			
				if (this.rdoGetOut.Checked == true)
					this.FeederGetOut();
				else if (this.rdoExchange.Checked == true)
					this.FeederExchange();
				else if (this.rdoReturnR.Checked == true)
					this.FeederReturn();
			}
			catch {}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
			}
		}

		private void btnReturnAll_Click(object sender, System.EventArgs e)
		{
			if (checkedMOCode != txtMOCode.Value)
			{
				btnOKMOCode_Click(null, null);
			}
			if (checkedSSCode != txtSSCode.Value)
			{
				btnOKMOCode_Click(null, null);
			}
			if (tableSource == null || tableSource.Rows.Count == 0)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$SMT_Feeder_GetOut_No_SMTMaterial"));
				return;
			}
			if (MessageBox.Show(UserControl.MutiLanguages.ParserMessage("$Confirm_Return_All_Feeder_In_MOCode"), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;
			
			this.ReturnAll();
		}

		private void FeederGetOut()
		{
			this.txtFeederNoGetOut.Value = this.txtFeederNoGetOut.Value.Trim().ToUpper();
			if (this.txtFeederNoGetOut.Value == string.Empty)
			{
				//ApplicationRun.GetInfoForm().Add("$Please_Input_FeederCode");
				Application.DoEvents();
				this.txtFeederNoGetOut.TextFocus(false, true);
				return;
			}
			if (this.txtUserCodeGetOut.Value.Trim() == string.Empty)
			{
				Application.DoEvents();
				this.txtUserCodeGetOut.TextFocus(true, true);
				return;
			}
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.BeginTransaction();
			try
			{
				Messages messages = smtFacade.FeederGetOut(txtMOCode.Value, txtSSCode.Value, txtFeederNoGetOut.Value, this.txtUserCodeGetOut.Value.Trim().ToUpper(), this.tableSource, Service.ApplicationService.Current().UserCode);
				if (messages.IsSuccess())
				{
					messages.Add(new UserControl.Message(MessageType.Success, "$Feeder_GetOut_Success"));
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CommitTransaction();
					SelectFeeder(this.txtFeederNoGetOut.Value);
				}
				else
				{
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
				}
				ApplicationRun.GetInfoForm().Add(messages);
			}
			catch (Exception e)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(e));
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
			}
			this.txtFeederNoGetOut.Value = string.Empty;
			this.txtFeederNoGetOut.TextFocus(false, true);
		}

		private void FeederExchange()
		{
			this.txtFeederNoExchange.Value = this.txtFeederNoExchange.Value.Trim().ToUpper();
			this.txtFeederNoExchangeNew.Value = this.txtFeederNoExchangeNew.Value.Trim().ToUpper();
			if ((this.txtFeederNoExchange.Value == string.Empty && this.chkAutoExchangeFeeder.Checked == false) || this.txtFeederNoExchangeNew.Value == string.Empty)
			{
				ApplicationRun.GetInfoForm().Add("$Please_Input_FeederCode");
				return;
			}
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.BeginTransaction();
			try
			{
				Messages messages = smtFacade.FeederExchange(txtMOCode.Value, txtFeederNoExchangeNew.Value, this.txtFeederNoExchange.Value, this.txtExchangeReason.Value, this.chkScrapExchange.Checked, this.chkAutoExchangeFeeder.Checked, this.tableSource, Service.ApplicationService.Current().UserCode);
				if (messages.IsSuccess())
				{
					messages.Add(new UserControl.Message(MessageType.Success, "$Feeder_Exchange_Success"));
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CommitTransaction();
					SelectFeeder(this.txtFeederNoExchange.Value);
				}
				else
				{
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
				}
				ApplicationRun.GetInfoForm().Add(messages);
			}
			catch (Exception e)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(e));
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
			}
			this.txtFeederNoExchange.Value = string.Empty;
			this.txtFeederNoExchangeNew.Value = string.Empty;
			this.txtExchangeReason.Value = string.Empty;
			this.txtFeederNoExchange.TextFocus(false, true);
		}

		private void FeederReturn()
		{
			this.txtFeederNoReturn.Value = this.txtFeederNoReturn.Value.Trim().ToUpper();
			if (this.txtFeederNoReturn.Value == string.Empty)
			{
				//ApplicationRun.GetInfoForm().Add("$Please_Input_FeederCode");
				Application.DoEvents();
				this.txtFeederNoReturn.TextFocus(false, true);
				return;
			}
			if (this.txtUserCodeReturn.Value.Trim() == string.Empty)
			{
				Application.DoEvents();
				this.txtUserCodeReturn.TextFocus(true, true);
				return;
			}
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.BeginTransaction();
			try
			{
				Messages messages = smtFacade.FeederReturn(txtMOCode.Value, txtSSCode.Value, txtFeederNoReturn.Value, this.txtExchangeReason.Value, this.chkScrapReturn.Checked, this.txtUserCodeReturn.Value.Trim().ToUpper(), this.tableSource, Service.ApplicationService.Current().UserCode);
				if (messages.IsSuccess())
				{
					messages.Add(new UserControl.Message(MessageType.Success, "$Feeder_Return_Success"));
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CommitTransaction();
				}
				else
				{
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
				}
				ApplicationRun.GetInfoForm().Add(messages);
			}
			catch (Exception e)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(e));
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
			}
			this.txtFeederNoReturn.Value = string.Empty;
			this.txtFeederNoReturn.TextFocus(false, true);
		}

		private void ReturnAll()
		{
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.BeginTransaction();
			try
			{
				Messages messages = smtFacade.FeederReturnAll(txtMOCode.Value, txtSSCode.Value, this.txtUserCodeReturn.Value.Trim().ToUpper(), this.tableSource, Service.ApplicationService.Current().UserCode);
				if (messages.IsSuccess())
				{
					messages.Add(new UserControl.Message(MessageType.Success, "$Feeder_Return_Success"));
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CommitTransaction();
				}
				else
				{
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
					ApplicationRun.GetInfoForm().Add(messages);
					return;
				}
				ApplicationRun.GetInfoForm().Add(messages);
			}
			catch (Exception e)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(e));
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
				return;
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
			}
			Application.DoEvents();
            //Remove UCLabel.SelectAll;
			this.txtMOCode.TextFocus(false, true);
		}

		private void SelectFeeder(string feederCode)
		{
			for (int i = 0; i < this.gridList.Rows.Count; i++)
			{
				if (this.gridList.Rows[i].Cells["FeederCode"].Text == feederCode)
				{
					this.gridList.Selected.Rows.Clear();
					this.gridList.Rows[i].Activate();
					Application.DoEvents();
					this.gridList.Rows[i].Selected = true;
					break;
				}
			}
		}

		private void ucBtnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void txtMOCode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
				this.btnOKMOCode_Click(sender, null);
		}

		private void txtFeederNoGetOut_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
				this.btnOK_Click(null, null);
		}

		private void txtFeederNoExchange_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
				this.txtFeederNoExchangeNew.TextFocus(false, true);
		}

		private void txtFeederNoExchangeNew_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
				this.txtExchangeReason.TextFocus(false, true);
		}

		private void txtExchangeReason_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
				this.btnOK_Click(null, null);
		}

		private void txtFeederNoReturn_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.btnOK_Click(null, null);
			}
		}

		private void chkAutoExchangeFeeder_CheckedChanged(object sender, System.EventArgs e)
		{
			if (this.chkAutoExchangeFeeder.Checked == true)
			{
				this.chkScrapExchange.Checked = false;
				Application.DoEvents();
				this.txtFeederNoExchangeNew.TextFocus(false, true);
			}
		}
		
	}
}
