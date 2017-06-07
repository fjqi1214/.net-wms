using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.DataCollect;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.MOModel;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FOQCFuncTest 的摘要说明。
	/// </summary>
	public class FOQCFuncTest : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private UserControl.UCLabelEdit txtModelCode;
		private UserControl.UCLabelEdit txtProductCode;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.GroupBox grpMinDutyRato;
		private UserControl.UCLabelEdit txtMinDutyRatoMax;
		private UserControl.UCLabelEdit txtMinDutyRatoMin;
		private UserControl.UCLabelEdit txtMinDutyRatoValue;
		private System.Windows.Forms.GroupBox grpBurstMdFre;
		private UserControl.UCLabelEdit txtBurstMdFreValue;
		private UserControl.UCLabelEdit txtBurstMdFreMax;
		private UserControl.UCLabelEdit txtBurstMdFreMin;
		private System.Windows.Forms.Panel panel3;
		private UserControl.UCLabelEdit txtInput;
		private UserControl.UCButton btnLockInput;
		private UserControl.UCButton btnSave;
		private UserControl.UCButton btnExit;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.TabControl tabGroup;
		private System.Windows.Forms.GroupBox grpFre;
		private UserControl.UCLabelEdit txtFreValue;
		private UserControl.UCLabelEdit txtFreMax;
		private UserControl.UCLabelEdit txtFreMin;
		private UserControl.UCLabelEdit txtElectricMax;
		private UserControl.UCLabelEdit txtElectricMin;
		private UserControl.UCButton btnTest;
		private System.Windows.Forms.TabPage tabGroup_1;
		private System.Windows.Forms.GroupBox grpElectric;
		private AxMSCommLib.AxMSComm axMSComm1;
		private System.Windows.Forms.Label lblResult;
		private System.Windows.Forms.CheckBox chkManualInput;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FOQCFuncTest()
		{
			InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FOQCFuncTest));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkManualInput = new System.Windows.Forms.CheckBox();
            this.txtProductCode = new UserControl.UCLabelEdit();
            this.txtModelCode = new UserControl.UCLabelEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grpBurstMdFre = new System.Windows.Forms.GroupBox();
            this.txtBurstMdFreValue = new UserControl.UCLabelEdit();
            this.txtBurstMdFreMax = new UserControl.UCLabelEdit();
            this.txtBurstMdFreMin = new UserControl.UCLabelEdit();
            this.grpMinDutyRato = new System.Windows.Forms.GroupBox();
            this.txtMinDutyRatoValue = new UserControl.UCLabelEdit();
            this.txtMinDutyRatoMax = new UserControl.UCLabelEdit();
            this.txtMinDutyRatoMin = new UserControl.UCLabelEdit();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.axMSComm1 = new AxMSCommLib.AxMSComm();
            this.btnTest = new UserControl.UCButton();
            this.btnSave = new UserControl.UCButton();
            this.btnExit = new UserControl.UCButton();
            this.btnLockInput = new UserControl.UCButton();
            this.txtInput = new UserControl.UCLabelEdit();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tabGroup = new System.Windows.Forms.TabControl();
            this.tabGroup_1 = new System.Windows.Forms.TabPage();
            this.grpElectric = new System.Windows.Forms.GroupBox();
            this.txtElectricMax = new UserControl.UCLabelEdit();
            this.txtElectricMin = new UserControl.UCLabelEdit();
            this.grpFre = new System.Windows.Forms.GroupBox();
            this.txtFreValue = new UserControl.UCLabelEdit();
            this.txtFreMax = new UserControl.UCLabelEdit();
            this.txtFreMin = new UserControl.UCLabelEdit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpBurstMdFre.SuspendLayout();
            this.grpMinDutyRato.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMSComm1)).BeginInit();
            this.panel4.SuspendLayout();
            this.tabGroup.SuspendLayout();
            this.tabGroup_1.SuspendLayout();
            this.grpElectric.SuspendLayout();
            this.grpFre.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkManualInput);
            this.panel1.Controls.Add(this.txtProductCode);
            this.panel1.Controls.Add(this.txtModelCode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(752, 45);
            this.panel1.TabIndex = 0;
            // 
            // chkManualInput
            // 
            this.chkManualInput.Location = new System.Drawing.Point(407, 15);
            this.chkManualInput.Name = "chkManualInput";
            this.chkManualInput.Size = new System.Drawing.Size(166, 22);
            this.chkManualInput.TabIndex = 4;
            this.chkManualInput.Text = "手动输入（输入后回车）";
            // 
            // txtProductCode
            // 
            this.txtProductCode.AllowEditOnlyChecked = true;
            this.txtProductCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtProductCode.Caption = "产品代码";
            this.txtProductCode.Checked = false;
            this.txtProductCode.EditType = UserControl.EditTypes.String;
            this.txtProductCode.Enabled = false;
            this.txtProductCode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProductCode.Location = new System.Drawing.Point(207, 15);
            this.txtProductCode.MaxLength = 40;
            this.txtProductCode.Multiline = false;
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.PasswordChar = '\0';
            this.txtProductCode.ReadOnly = false;
            this.txtProductCode.ShowCheckBox = false;
            this.txtProductCode.Size = new System.Drawing.Size(161, 22);
            this.txtProductCode.TabIndex = 3;
            this.txtProductCode.TabNext = true;
            this.txtProductCode.Value = "";
            this.txtProductCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtProductCode.XAlign = 258;
            // 
            // txtModelCode
            // 
            this.txtModelCode.AllowEditOnlyChecked = true;
            this.txtModelCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtModelCode.Caption = "机种代码";
            this.txtModelCode.Checked = false;
            this.txtModelCode.EditType = UserControl.EditTypes.String;
            this.txtModelCode.Enabled = false;
            this.txtModelCode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtModelCode.Location = new System.Drawing.Point(21, 15);
            this.txtModelCode.MaxLength = 40;
            this.txtModelCode.Multiline = false;
            this.txtModelCode.Name = "txtModelCode";
            this.txtModelCode.PasswordChar = '\0';
            this.txtModelCode.ReadOnly = false;
            this.txtModelCode.ShowCheckBox = false;
            this.txtModelCode.Size = new System.Drawing.Size(161, 22);
            this.txtModelCode.TabIndex = 2;
            this.txtModelCode.TabNext = true;
            this.txtModelCode.Value = "";
            this.txtModelCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtModelCode.XAlign = 72;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grpBurstMdFre);
            this.panel2.Controls.Add(this.grpMinDutyRato);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(752, 66);
            this.panel2.TabIndex = 1;
            // 
            // grpBurstMdFre
            // 
            this.grpBurstMdFre.Controls.Add(this.txtBurstMdFreValue);
            this.grpBurstMdFre.Controls.Add(this.txtBurstMdFreMax);
            this.grpBurstMdFre.Controls.Add(this.txtBurstMdFreMin);
            this.grpBurstMdFre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBurstMdFre.Location = new System.Drawing.Point(313, 0);
            this.grpBurstMdFre.Name = "grpBurstMdFre";
            this.grpBurstMdFre.Size = new System.Drawing.Size(439, 66);
            this.grpBurstMdFre.TabIndex = 3;
            this.grpBurstMdFre.TabStop = false;
            this.grpBurstMdFre.Text = "Burst md 频率";
            // 
            // txtBurstMdFreValue
            // 
            this.txtBurstMdFreValue.AllowEditOnlyChecked = true;
            this.txtBurstMdFreValue.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBurstMdFreValue.Caption = "实际值";
            this.txtBurstMdFreValue.Checked = false;
            this.txtBurstMdFreValue.EditType = UserControl.EditTypes.String;
            this.txtBurstMdFreValue.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBurstMdFreValue.Location = new System.Drawing.Point(214, 30);
            this.txtBurstMdFreValue.MaxLength = 40;
            this.txtBurstMdFreValue.Multiline = false;
            this.txtBurstMdFreValue.Name = "txtBurstMdFreValue";
            this.txtBurstMdFreValue.PasswordChar = '\0';
            this.txtBurstMdFreValue.ReadOnly = false;
            this.txtBurstMdFreValue.ShowCheckBox = false;
            this.txtBurstMdFreValue.Size = new System.Drawing.Size(83, 22);
            this.txtBurstMdFreValue.TabIndex = 5;
            this.txtBurstMdFreValue.TabNext = true;
            this.txtBurstMdFreValue.Value = "";
            this.txtBurstMdFreValue.WidthType = UserControl.WidthTypes.Tiny;
            this.txtBurstMdFreValue.XAlign = 255;
            this.txtBurstMdFreValue.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBurstMdFreValue_TxtboxKeyPress);
            // 
            // txtBurstMdFreMax
            // 
            this.txtBurstMdFreMax.AllowEditOnlyChecked = true;
            this.txtBurstMdFreMax.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBurstMdFreMax.Caption = "最大值";
            this.txtBurstMdFreMax.Checked = false;
            this.txtBurstMdFreMax.EditType = UserControl.EditTypes.String;
            this.txtBurstMdFreMax.Enabled = false;
            this.txtBurstMdFreMax.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBurstMdFreMax.Location = new System.Drawing.Point(107, 30);
            this.txtBurstMdFreMax.MaxLength = 40;
            this.txtBurstMdFreMax.Multiline = false;
            this.txtBurstMdFreMax.Name = "txtBurstMdFreMax";
            this.txtBurstMdFreMax.PasswordChar = '\0';
            this.txtBurstMdFreMax.ReadOnly = false;
            this.txtBurstMdFreMax.ShowCheckBox = false;
            this.txtBurstMdFreMax.Size = new System.Drawing.Size(83, 22);
            this.txtBurstMdFreMax.TabIndex = 4;
            this.txtBurstMdFreMax.TabNext = true;
            this.txtBurstMdFreMax.Value = "";
            this.txtBurstMdFreMax.WidthType = UserControl.WidthTypes.Tiny;
            this.txtBurstMdFreMax.XAlign = 148;
            // 
            // txtBurstMdFreMin
            // 
            this.txtBurstMdFreMin.AllowEditOnlyChecked = true;
            this.txtBurstMdFreMin.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBurstMdFreMin.Caption = "最小值";
            this.txtBurstMdFreMin.Checked = false;
            this.txtBurstMdFreMin.EditType = UserControl.EditTypes.String;
            this.txtBurstMdFreMin.Enabled = false;
            this.txtBurstMdFreMin.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBurstMdFreMin.Location = new System.Drawing.Point(14, 30);
            this.txtBurstMdFreMin.MaxLength = 40;
            this.txtBurstMdFreMin.Multiline = false;
            this.txtBurstMdFreMin.Name = "txtBurstMdFreMin";
            this.txtBurstMdFreMin.PasswordChar = '\0';
            this.txtBurstMdFreMin.ReadOnly = false;
            this.txtBurstMdFreMin.ShowCheckBox = false;
            this.txtBurstMdFreMin.Size = new System.Drawing.Size(83, 22);
            this.txtBurstMdFreMin.TabIndex = 3;
            this.txtBurstMdFreMin.TabNext = true;
            this.txtBurstMdFreMin.Value = "";
            this.txtBurstMdFreMin.WidthType = UserControl.WidthTypes.Tiny;
            this.txtBurstMdFreMin.XAlign = 55;
            // 
            // grpMinDutyRato
            // 
            this.grpMinDutyRato.Controls.Add(this.txtMinDutyRatoValue);
            this.grpMinDutyRato.Controls.Add(this.txtMinDutyRatoMax);
            this.grpMinDutyRato.Controls.Add(this.txtMinDutyRatoMin);
            this.grpMinDutyRato.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpMinDutyRato.Location = new System.Drawing.Point(0, 0);
            this.grpMinDutyRato.Name = "grpMinDutyRato";
            this.grpMinDutyRato.Size = new System.Drawing.Size(313, 66);
            this.grpMinDutyRato.TabIndex = 2;
            this.grpMinDutyRato.TabStop = false;
            this.grpMinDutyRato.Text = "Min Duty Rato";
            // 
            // txtMinDutyRatoValue
            // 
            this.txtMinDutyRatoValue.AllowEditOnlyChecked = true;
            this.txtMinDutyRatoValue.BackColor = System.Drawing.Color.Gainsboro;
            this.txtMinDutyRatoValue.Caption = "实际值";
            this.txtMinDutyRatoValue.Checked = false;
            this.txtMinDutyRatoValue.EditType = UserControl.EditTypes.String;
            this.txtMinDutyRatoValue.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMinDutyRatoValue.Location = new System.Drawing.Point(214, 30);
            this.txtMinDutyRatoValue.MaxLength = 40;
            this.txtMinDutyRatoValue.Multiline = false;
            this.txtMinDutyRatoValue.Name = "txtMinDutyRatoValue";
            this.txtMinDutyRatoValue.PasswordChar = '\0';
            this.txtMinDutyRatoValue.ReadOnly = false;
            this.txtMinDutyRatoValue.ShowCheckBox = false;
            this.txtMinDutyRatoValue.Size = new System.Drawing.Size(83, 22);
            this.txtMinDutyRatoValue.TabIndex = 5;
            this.txtMinDutyRatoValue.TabNext = true;
            this.txtMinDutyRatoValue.Value = "";
            this.txtMinDutyRatoValue.WidthType = UserControl.WidthTypes.Tiny;
            this.txtMinDutyRatoValue.XAlign = 255;
            this.txtMinDutyRatoValue.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMinDutyRatoValue_TxtboxKeyPress);
            // 
            // txtMinDutyRatoMax
            // 
            this.txtMinDutyRatoMax.AllowEditOnlyChecked = true;
            this.txtMinDutyRatoMax.BackColor = System.Drawing.Color.Gainsboro;
            this.txtMinDutyRatoMax.Caption = "最大值";
            this.txtMinDutyRatoMax.Checked = false;
            this.txtMinDutyRatoMax.EditType = UserControl.EditTypes.String;
            this.txtMinDutyRatoMax.Enabled = false;
            this.txtMinDutyRatoMax.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMinDutyRatoMax.Location = new System.Drawing.Point(107, 30);
            this.txtMinDutyRatoMax.MaxLength = 40;
            this.txtMinDutyRatoMax.Multiline = false;
            this.txtMinDutyRatoMax.Name = "txtMinDutyRatoMax";
            this.txtMinDutyRatoMax.PasswordChar = '\0';
            this.txtMinDutyRatoMax.ReadOnly = false;
            this.txtMinDutyRatoMax.ShowCheckBox = false;
            this.txtMinDutyRatoMax.Size = new System.Drawing.Size(83, 22);
            this.txtMinDutyRatoMax.TabIndex = 4;
            this.txtMinDutyRatoMax.TabNext = true;
            this.txtMinDutyRatoMax.Value = "";
            this.txtMinDutyRatoMax.WidthType = UserControl.WidthTypes.Tiny;
            this.txtMinDutyRatoMax.XAlign = 148;
            // 
            // txtMinDutyRatoMin
            // 
            this.txtMinDutyRatoMin.AllowEditOnlyChecked = true;
            this.txtMinDutyRatoMin.BackColor = System.Drawing.Color.Gainsboro;
            this.txtMinDutyRatoMin.Caption = "最小值";
            this.txtMinDutyRatoMin.Checked = false;
            this.txtMinDutyRatoMin.EditType = UserControl.EditTypes.String;
            this.txtMinDutyRatoMin.Enabled = false;
            this.txtMinDutyRatoMin.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMinDutyRatoMin.Location = new System.Drawing.Point(14, 30);
            this.txtMinDutyRatoMin.MaxLength = 40;
            this.txtMinDutyRatoMin.Multiline = false;
            this.txtMinDutyRatoMin.Name = "txtMinDutyRatoMin";
            this.txtMinDutyRatoMin.PasswordChar = '\0';
            this.txtMinDutyRatoMin.ReadOnly = false;
            this.txtMinDutyRatoMin.ShowCheckBox = false;
            this.txtMinDutyRatoMin.Size = new System.Drawing.Size(83, 22);
            this.txtMinDutyRatoMin.TabIndex = 3;
            this.txtMinDutyRatoMin.TabNext = true;
            this.txtMinDutyRatoMin.Value = "";
            this.txtMinDutyRatoMin.WidthType = UserControl.WidthTypes.Tiny;
            this.txtMinDutyRatoMin.XAlign = 55;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblResult);
            this.panel3.Controls.Add(this.btnTest);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnExit);
            this.panel3.Controls.Add(this.btnLockInput);
            this.panel3.Controls.Add(this.txtInput);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 473);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(752, 60);
            this.panel3.TabIndex = 0;
            // 
            // lblResult
            // 
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.Location = new System.Drawing.Point(459, 14);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(73, 37);
            this.lblResult.TabIndex = 294;
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // axMSComm1
            // 
            this.axMSComm1.Enabled = true;
            this.axMSComm1.Location = new System.Drawing.Point(0, 0);
            this.axMSComm1.Name = "axMSComm1";
            this.axMSComm1.TabIndex = 0;
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.SystemColors.Control;
            this.btnTest.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTest.BackgroundImage")));
            this.btnTest.ButtonType = UserControl.ButtonTypes.None;
            this.btnTest.Caption = "测试";
            this.btnTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTest.Location = new System.Drawing.Point(365, 22);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(88, 22);
            this.btnTest.TabIndex = 290;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.ButtonType = UserControl.ButtonTypes.None;
            this.btnSave.Caption = "保存(F5)";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(557, 22);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 22);
            this.btnSave.TabIndex = 288;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(651, 22);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 289;
            this.btnExit.TabStop = false;
            // 
            // btnLockInput
            // 
            this.btnLockInput.BackColor = System.Drawing.SystemColors.Control;
            this.btnLockInput.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLockInput.BackgroundImage")));
            this.btnLockInput.ButtonType = UserControl.ButtonTypes.None;
            this.btnLockInput.Caption = "锁定";
            this.btnLockInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLockInput.Location = new System.Drawing.Point(271, 22);
            this.btnLockInput.Name = "btnLockInput";
            this.btnLockInput.Size = new System.Drawing.Size(88, 22);
            this.btnLockInput.TabIndex = 287;
            this.btnLockInput.Click += new System.EventHandler(this.btnLockInput_Click);
            // 
            // txtInput
            // 
            this.txtInput.AllowEditOnlyChecked = true;
            this.txtInput.BackColor = System.Drawing.Color.Gainsboro;
            this.txtInput.Caption = "输入框";
            this.txtInput.Checked = false;
            this.txtInput.EditType = UserControl.EditTypes.String;
            this.txtInput.Location = new System.Drawing.Point(14, 22);
            this.txtInput.MaxLength = 4000;
            this.txtInput.Multiline = false;
            this.txtInput.Name = "txtInput";
            this.txtInput.PasswordChar = '\0';
            this.txtInput.ReadOnly = false;
            this.txtInput.ShowCheckBox = false;
            this.txtInput.Size = new System.Drawing.Size(208, 23);
            this.txtInput.TabIndex = 0;
            this.txtInput.TabNext = false;
            this.txtInput.Value = "";
            this.txtInput.WidthType = UserControl.WidthTypes.Long;
            this.txtInput.XAlign = 55;
            this.txtInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_TxtboxKeyPress);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tabGroup);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 111);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(752, 362);
            this.panel4.TabIndex = 3;
            // 
            // tabGroup
            // 
            this.tabGroup.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabGroup.Controls.Add(this.tabGroup_1);
            this.tabGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabGroup.Location = new System.Drawing.Point(0, 0);
            this.tabGroup.Name = "tabGroup";
            this.tabGroup.SelectedIndex = 0;
            this.tabGroup.Size = new System.Drawing.Size(752, 362);
            this.tabGroup.TabIndex = 4;
            this.tabGroup.TabStop = false;
            // 
            // tabGroup_1
            // 
            this.tabGroup_1.BackColor = System.Drawing.Color.Gainsboro;
            this.tabGroup_1.Controls.Add(this.grpElectric);
            this.tabGroup_1.Controls.Add(this.grpFre);
            this.tabGroup_1.Location = new System.Drawing.Point(4, 25);
            this.tabGroup_1.Name = "tabGroup_1";
            this.tabGroup_1.Size = new System.Drawing.Size(744, 333);
            this.tabGroup_1.TabIndex = 0;
            this.tabGroup_1.Text = "Group 1#";
            // 
            // grpElectric
            // 
            this.grpElectric.Controls.Add(this.txtElectricMax);
            this.grpElectric.Controls.Add(this.txtElectricMin);
            this.grpElectric.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpElectric.Location = new System.Drawing.Point(0, 67);
            this.grpElectric.Name = "grpElectric";
            this.grpElectric.Size = new System.Drawing.Size(744, 266);
            this.grpElectric.TabIndex = 4;
            this.grpElectric.TabStop = false;
            this.grpElectric.Text = "电流数据";
            // 
            // txtElectricMax
            // 
            this.txtElectricMax.AllowEditOnlyChecked = true;
            this.txtElectricMax.BackColor = System.Drawing.Color.Gainsboro;
            this.txtElectricMax.Caption = "最大值";
            this.txtElectricMax.Checked = false;
            this.txtElectricMax.EditType = UserControl.EditTypes.String;
            this.txtElectricMax.Enabled = false;
            this.txtElectricMax.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtElectricMax.Location = new System.Drawing.Point(107, 22);
            this.txtElectricMax.MaxLength = 40;
            this.txtElectricMax.Multiline = false;
            this.txtElectricMax.Name = "txtElectricMax";
            this.txtElectricMax.PasswordChar = '\0';
            this.txtElectricMax.ReadOnly = false;
            this.txtElectricMax.ShowCheckBox = false;
            this.txtElectricMax.Size = new System.Drawing.Size(83, 23);
            this.txtElectricMax.TabIndex = 6;
            this.txtElectricMax.TabNext = true;
            this.txtElectricMax.Value = "";
            this.txtElectricMax.WidthType = UserControl.WidthTypes.Tiny;
            this.txtElectricMax.XAlign = 148;
            // 
            // txtElectricMin
            // 
            this.txtElectricMin.AllowEditOnlyChecked = true;
            this.txtElectricMin.BackColor = System.Drawing.Color.Gainsboro;
            this.txtElectricMin.Caption = "最小值";
            this.txtElectricMin.Checked = false;
            this.txtElectricMin.EditType = UserControl.EditTypes.String;
            this.txtElectricMin.Enabled = false;
            this.txtElectricMin.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtElectricMin.Location = new System.Drawing.Point(14, 22);
            this.txtElectricMin.MaxLength = 40;
            this.txtElectricMin.Multiline = false;
            this.txtElectricMin.Name = "txtElectricMin";
            this.txtElectricMin.PasswordChar = '\0';
            this.txtElectricMin.ReadOnly = false;
            this.txtElectricMin.ShowCheckBox = false;
            this.txtElectricMin.Size = new System.Drawing.Size(83, 23);
            this.txtElectricMin.TabIndex = 5;
            this.txtElectricMin.TabNext = true;
            this.txtElectricMin.Value = "";
            this.txtElectricMin.WidthType = UserControl.WidthTypes.Tiny;
            this.txtElectricMin.XAlign = 55;
            // 
            // grpFre
            // 
            this.grpFre.Controls.Add(this.txtFreValue);
            this.grpFre.Controls.Add(this.txtFreMax);
            this.grpFre.Controls.Add(this.txtFreMin);
            this.grpFre.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFre.Location = new System.Drawing.Point(0, 0);
            this.grpFre.Name = "grpFre";
            this.grpFre.Size = new System.Drawing.Size(744, 67);
            this.grpFre.TabIndex = 3;
            this.grpFre.TabStop = false;
            this.grpFre.Text = "频率数据";
            // 
            // txtFreValue
            // 
            this.txtFreValue.AllowEditOnlyChecked = true;
            this.txtFreValue.BackColor = System.Drawing.Color.Gainsboro;
            this.txtFreValue.Caption = "实际值";
            this.txtFreValue.Checked = false;
            this.txtFreValue.EditType = UserControl.EditTypes.String;
            this.txtFreValue.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFreValue.Location = new System.Drawing.Point(214, 30);
            this.txtFreValue.MaxLength = 40;
            this.txtFreValue.Multiline = false;
            this.txtFreValue.Name = "txtFreValue";
            this.txtFreValue.PasswordChar = '\0';
            this.txtFreValue.ReadOnly = false;
            this.txtFreValue.ShowCheckBox = false;
            this.txtFreValue.Size = new System.Drawing.Size(83, 22);
            this.txtFreValue.TabIndex = 5;
            this.txtFreValue.TabNext = false;
            this.txtFreValue.Value = "";
            this.txtFreValue.WidthType = UserControl.WidthTypes.Tiny;
            this.txtFreValue.XAlign = 255;
            this.txtFreValue.TxtboxGotFocus += new System.EventHandler(this.ValueInputTxt_GotFocus);
            this.txtFreValue.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValueInputTxt_KeyPress);
            // 
            // txtFreMax
            // 
            this.txtFreMax.AllowEditOnlyChecked = true;
            this.txtFreMax.BackColor = System.Drawing.Color.Gainsboro;
            this.txtFreMax.Caption = "最大值";
            this.txtFreMax.Checked = false;
            this.txtFreMax.EditType = UserControl.EditTypes.String;
            this.txtFreMax.Enabled = false;
            this.txtFreMax.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFreMax.Location = new System.Drawing.Point(107, 30);
            this.txtFreMax.MaxLength = 40;
            this.txtFreMax.Multiline = false;
            this.txtFreMax.Name = "txtFreMax";
            this.txtFreMax.PasswordChar = '\0';
            this.txtFreMax.ReadOnly = false;
            this.txtFreMax.ShowCheckBox = false;
            this.txtFreMax.Size = new System.Drawing.Size(83, 22);
            this.txtFreMax.TabIndex = 4;
            this.txtFreMax.TabNext = true;
            this.txtFreMax.Value = "";
            this.txtFreMax.WidthType = UserControl.WidthTypes.Tiny;
            this.txtFreMax.XAlign = 148;
            // 
            // txtFreMin
            // 
            this.txtFreMin.AllowEditOnlyChecked = true;
            this.txtFreMin.BackColor = System.Drawing.Color.Gainsboro;
            this.txtFreMin.Caption = "最小值";
            this.txtFreMin.Checked = false;
            this.txtFreMin.EditType = UserControl.EditTypes.String;
            this.txtFreMin.Enabled = false;
            this.txtFreMin.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFreMin.Location = new System.Drawing.Point(14, 30);
            this.txtFreMin.MaxLength = 40;
            this.txtFreMin.Multiline = false;
            this.txtFreMin.Name = "txtFreMin";
            this.txtFreMin.PasswordChar = '\0';
            this.txtFreMin.ReadOnly = false;
            this.txtFreMin.ShowCheckBox = false;
            this.txtFreMin.Size = new System.Drawing.Size(83, 22);
            this.txtFreMin.TabIndex = 3;
            this.txtFreMin.TabNext = true;
            this.txtFreMin.Value = "";
            this.txtFreMin.WidthType = UserControl.WidthTypes.Tiny;
            this.txtFreMin.XAlign = 55;
            // 
            // FOQCFuncTest
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(752, 533);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FOQCFuncTest";
            this.Text = "FQC功能测试";
            this.Load += new System.EventHandler(this.FOQCFuncTest_Load);
            this.Closed += new System.EventHandler(this.FOQCFuncTest_Closed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FOQCFuncTest_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.grpBurstMdFre.ResumeLayout(false);
            this.grpMinDutyRato.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMSComm1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.tabGroup.ResumeLayout(false);
            this.tabGroup_1.ResumeLayout(false);
            this.grpElectric.ResumeLayout(false);
            this.grpFre.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		private ProductInfo product = null;
		private int iElectricCount = -1;
		private UCLabelEdit currentInputValue = null;	// 当前输入的输入框
		private Hashtable listTestValueFre = new Hashtable();			// 频率测试实际值
		private Hashtable listTestValueEle = new Hashtable();			// 电流测试实际值
		private OQCFuncTest oqcFuncTest = null;
		private OQCFuncTestSpec[] oqcFuncTestSpecList = null;
		private void FOQCFuncTest_Load(object sender, System.EventArgs e)
		{
			UserControl.UIStyleBuilder.FormUI(this);
			this.PrepareMSComm();
		}
		private void FOQCFuncTest_Closed(object sender, System.EventArgs e)
		{
			if (this.DataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();  
			this.CloseMSComm();
		}

		private void txtInput_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r' && txtInput.Value.Trim() != string.Empty)
			{
				Messages msg = CheckProduct();
				if (msg.IsSuccess())
				{
					msg = DisplayTestGroup();
					if (msg.IsSuccess())
					{
						LockInput(true);
						this.txtMinDutyRatoValue.TextFocus(false, true);
					}
				}
				if (!msg.IsSuccess())
				{
					ApplicationRun.GetInfoForm().Add(msg);
                    //Remove UCLabel.SelectAll;
					txtInput.TextFocus(false, true);
				}
			}
		}

		private void LockInput(bool locked)
		{
			if (locked == true)
			{
				txtInput.Enabled = false;
				btnLockInput.Caption = "解除锁定";
			}
			else
			{
				txtInput.Enabled = true;
				btnLockInput.Caption = "锁定";
			}
		}

		private Messages DisplayTestGroup()
		{
			Messages msg = new Messages();
			OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
			OQCFuncTest test = (OQCFuncTest)oqcFacade.GetOQCFuncTest(this.txtProductCode.Value);
			if (test == null)
			{
				msg.Add(new UserControl.Message(MessageType.Error, "$OQCFuncTest_Not_Exist"));
				return msg;
			}
			this.txtMinDutyRatoMin.Value = test.MinDutyRatoMin.ToString();
			this.txtMinDutyRatoMax.Value = test.MinDutyRatoMax.ToString();
			this.txtBurstMdFreMin.Value = test.BurstMdFreMin.ToString();
			this.txtBurstMdFreMax.Value = test.BurstMdFreMax.ToString();

			int iGroupCount = Convert.ToInt32(test.FuncTestGroupCount);
			iElectricCount = Convert.ToInt32(test.ElectricTestCount);

			object[] specList = oqcFacade.QueryOQCFuncTestSpec(this.txtProductCode.Value);
			if (specList == null || specList.Length < iGroupCount)
			{
				msg.Add(new UserControl.Message(MessageType.Error, "$OQCFuncTestSpec_Not_Exist"));
				return msg;
			}

			for (int i = tabGroup.TabPages.Count; i > specList.Length; i--)
			{
				tabGroup.TabPages.RemoveAt(i);
			}
			for (int i = tabGroup.TabPages.Count; i < specList.Length; i++)
			{
				TabPage tab = new TabPage();
				tab.Name = "tabGroup_" + (i + 1).ToString();
				tab.Text = "Group " + (i + 1).ToString() + "#";
				tab.BackColor = tabGroup_1.BackColor;
				tabGroup.TabPages.Add(tab);
			}

			for (int i = 0; i < specList.Length; i++)
			{
				InitGroupControl(i, (OQCFuncTestSpec)specList[i]);
			}
			this.Refresh();
			if (msg.IsSuccess())
			{
				listTestValueFre = new Hashtable();
				listTestValueEle = new Hashtable();
				this.oqcFuncTest = test;
				oqcFuncTestSpecList = new OQCFuncTestSpec[specList.Length];
				specList.CopyTo(oqcFuncTestSpecList, 0);
			}
			return msg;
		}

		private void InitGroupControl(int tabIndex, OQCFuncTestSpec spec)
		{
			TabPage tab = tabGroup.TabPages[tabIndex];
			GroupBox grpEle0 = null;
			if (tab.Controls.Count == 0)
			{
				GroupBox grpFre1 = new GroupBox();
				tab.Controls.Add(grpFre1);
				grpFre1.Height = grpFre.Height;
				grpFre1.Dock = DockStyle.Top;
				grpFre1.Text = grpFre.Text;
				grpFre1.Name = "grpFre";
				UserControl.UCLabelEdit txtFreMin1 = new UCLabelEdit();
				txtFreMin1.Name = "txtFreMin";
				grpFre1.Controls.Add(txtFreMin1);
				txtFreMin1.Enabled = false;
				txtFreMin1.Caption = txtFreMin.Caption;
				txtFreMin1.WidthType = txtFreMin.WidthType;
				txtFreMin1.Location = txtFreMin.Location;
				txtFreMin1.Value = spec.FreMin.ToString();
				txtFreMin1.Font = txtFreMin.Font;
				UserControl.UCLabelEdit txtFreMax1 = new UCLabelEdit();
				txtFreMax1.Name = "txtFreMax";
				grpFre1.Controls.Add(txtFreMax1);
				txtFreMax1.Enabled = false;
				txtFreMax1.Caption = txtFreMax.Caption;
				txtFreMax1.WidthType = txtFreMax.WidthType;
				txtFreMax1.Location = txtFreMax.Location;
				txtFreMax1.Value = spec.FreMax.ToString();
				txtFreMax1.Font = txtFreMax.Font;
				UserControl.UCLabelEdit txtFreValue1 = new UCLabelEdit();
				txtFreValue1.Name = "txtFreValue";
				grpFre1.Controls.Add(txtFreValue1);
				txtFreValue1.Caption = txtFreValue.Caption;
				txtFreValue1.WidthType = txtFreValue.WidthType;
				txtFreValue1.Location = txtFreValue.Location;
				txtFreValue1.TxtboxGotFocus += new EventHandler(ValueInputTxt_GotFocus);
				txtFreValue1.TxtboxKeyPress += new KeyPressEventHandler(ValueInputTxt_KeyPress);
				tab.Refresh();
				
				GroupBox grpElectric1 = new GroupBox();
				tab.Controls.Add(grpElectric1);
				grpElectric1.Name = "grpElectric";
				grpElectric1.Height = grpElectric.Height;
				//grpElectric1.Dock = DockStyle.Fill;
				grpElectric1.Height = tab.Height - grpFre1.Height;
				grpElectric1.Dock = DockStyle.Bottom;
				grpElectric1.Text = grpElectric.Text;
				UserControl.UCLabelEdit txtElectricMin1 = new UCLabelEdit();
				txtElectricMin1.Name = "txtElectricMin";
				grpElectric1.Controls.Add(txtElectricMin1);
				txtElectricMin1.Enabled = false;
				txtElectricMin1.Caption = txtElectricMin.Caption;
				txtElectricMin1.WidthType = txtElectricMin.WidthType;
				txtElectricMin1.Location = txtElectricMin.Location;
				txtElectricMin1.Value = spec.ElectricMin.ToString();
				txtElectricMin1.Font = txtElectricMin.Font;
				UserControl.UCLabelEdit txtElectricMax1 = new UCLabelEdit();
				txtElectricMax1.Name = "txtElectricMax";
				grpElectric1.Controls.Add(txtElectricMax1);
				txtElectricMax1.Enabled = false;
				txtElectricMax1.Caption = txtElectricMax.Caption;
				txtElectricMax1.WidthType = txtElectricMax.WidthType;
				txtElectricMax1.Location = txtElectricMax.Location;
				txtElectricMax1.Value = spec.ElectricMax.ToString();
				txtElectricMax1.Font = txtElectricMax.Font;

				grpEle0 = grpElectric1;
			}
			else
			{
				GroupBox grpFre1 = (GroupBox)FindControl(tab, "grpFre");
				UCLabelEdit txtFreMin1 = (UCLabelEdit)FindControl(grpFre1, "txtFreMin");
				txtFreMin1.Value = spec.FreMin.ToString();
				UCLabelEdit txtFreMax1 = (UCLabelEdit)FindControl(grpFre1, "txtFreMax");
				txtFreMax1.Value = spec.FreMax.ToString();
				UCLabelEdit txtFreValue1 = (UCLabelEdit)FindControl(grpFre1, "txtFreValue");
				txtFreValue1.Value = string.Empty;
				GroupBox grpElectric1 = (GroupBox)FindControl(tab, "grpElectric");
				UCLabelEdit txtElectricMin1 = (UCLabelEdit)FindControl(grpElectric1, "txtElectricMin");
				txtElectricMin1.Value = spec.ElectricMin.ToString();
				UCLabelEdit txtElectricMax1 = (UCLabelEdit)FindControl(grpElectric1, "txtElectricMax");
				txtElectricMax1.Value = spec.ElectricMax.ToString();

				grpEle0 = grpElectric1;
			}

			int iTmp = iElectricCount + 1;
			while (true)
			{
				UCLabelEdit txtEleValueTmp = (UCLabelEdit)FindControl(grpEle0, "txtElectricValue_" + iTmp.ToString());
				if (txtEleValueTmp != null)
					grpEle0.Controls.Remove(txtEleValueTmp);
				else
					break;
				iTmp++;
			}
			iTmp = 1;
			int iExistControl = 0;
			UCLabelEdit lastControl = null;
			while (true)
			{
				UCLabelEdit txtEleValueTmp = (UCLabelEdit)FindControl(grpEle0, "txtElectricValue_" + iTmp.ToString());
				if (txtEleValueTmp != null)
				{
					txtEleValueTmp.Value = string.Empty;
					txtEleValueTmp.InnerTextBox.ForeColor = Color.Black;
					lastControl = txtEleValueTmp;
					txtEleValueTmp.Refresh();
				}
				else
				{
					iExistControl = iTmp - 1;
					break;
				}
				iTmp++;
			}
			for (int i = iExistControl + 1; i <= iElectricCount; i++)
			{
				UCLabelEdit txtElectricValue0 = new UCLabelEdit();
				grpEle0.Controls.Add(txtElectricValue0);
				txtElectricValue0.Name = "txtElectricValue_" + i.ToString();
				txtElectricValue0.Caption = "1#";
				txtElectricValue0.WidthType = WidthTypes.Tiny;
				txtElectricValue0.TabNext = false;
				if (lastControl == null)
				{
					txtElectricValue0.Left = 41;
					txtElectricValue0.Top = 64;
				}
				else
				{
					txtElectricValue0.Left = lastControl.Left + lastControl.Width + 30;
					if (txtElectricValue0.Left + txtElectricValue0.Width > grpEle0.Width)
					{
						txtElectricValue0.Left = 41;
						txtElectricValue0.Top = lastControl.Top + lastControl.Height + 10;
					}
					else
					{
						txtElectricValue0.Top = lastControl.Top;
					}
				}
				txtElectricValue0.Caption = i.ToString() + "#";
				txtElectricValue0.TxtboxGotFocus += new EventHandler(ValueInputTxt_GotFocus);
				txtElectricValue0.TxtboxKeyPress += new KeyPressEventHandler(ValueInputTxt_KeyPress);
				lastControl = txtElectricValue0;
			}
			tab.Refresh();
		}

		/// <summary>
		/// 根据产品信息，决定部分控件的状态
		/// </summary>
		/// <returns></returns>
		private Messages CheckProduct()
		{
			Messages messages = new Messages();
			try
			{
				messages.AddMessages(GetProduct());

				if (product.LastSimulation ==null )
				{
					messages.Add(new UserControl.Message(MessageType.Error, "$NoSimulation"));
				}
				//检查是否完工
				else if(product.LastSimulation.IsComplete == "1")
				{
					messages.Add(new UserControl.Message(MessageType.Error,"$CS_PRODUCT_ALREADY_COMPLETE $CS_Param_OPCode =" + product.LastSimulation.OPCode));
				}
				else
				{
					// 检查途程
					/* annotated by jessie lee, 2006/8/9, for Power0163 
					messages.AddMessages(new DataCollectFacade( this.DataProvider).CheckID(
						product.LastSimulation.RunningCard,
						ActionType.DataCollectAction_OQCGood,  
						ApplicationService.Current().ResourceCode,
						ApplicationService.Current().UserCode,
						product));
					if (messages.IsSuccess() == false)
						return messages;
					*/
					OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
					// 是否在送检批中
					object[] objLotCard = oqcFacade.QueryOQCLot2Card(product.LastSimulation.RunningCard, string.Empty, string.Empty, OQCFacade.Lot_Sequence_Default, 0, int.MaxValue);
					OQCLot2Card lotCard = null;
					if (objLotCard != null)
					{
						lotCard = (OQCLot2Card)objLotCard[0];
						for (int i = 1; i < objLotCard.Length; i++)
						{
							if (((OQCLot2Card)objLotCard[i]).RunningCardSequence > lotCard.RunningCardSequence)
								lotCard = (OQCLot2Card)objLotCard[i];
						}
					}
					if (lotCard == null)
					{
						messages.Add(new UserControl.Message(MessageType.Error, "$RCard_Not_In_Lot"));
						return messages;
					}
					// 检查送检批状态
					OQCLot oqcLot = (OQCLot)oqcFacade.GetOQCLot(lotCard.LOTNO, lotCard.LotSequence);
					if (oqcLot == null)
					{
						messages.Add(new UserControl.Message(MessageType.Error, "$OQCLot_Not_Exist"));
						return messages;
					}
					if (oqcLot.LOTStatus != OQCLotStatus.OQCLotStatus_Examing && 
						oqcLot.LOTStatus != OQCLotStatus.OQCLotStatus_NoExame)
					{
						messages.Add(new UserControl.Message(MessageType.Error, "$OQCLot_Must_Be_Examing"));
					}
					// 是否已被测试过
					/* annotated by jessie lee, 2006/8/9, for Power0163 
					object objTmp = oqcFacade.GetOQCFuncTestValue(product.LastSimulation.RunningCard, product.LastSimulation.RunningCardSequence);
					if (objTmp != null)
					{
						messages.Add(new UserControl.Message(MessageType.Error, "$RCard_FuncTest_Completed"));
					}
					*/
					if (messages.IsSuccess())
					{
						txtModelCode.Value = product.LastSimulation.ModelCode;
						txtProductCode.Value = product.LastSimulation.ItemCode;
					}
				}
			}
			catch (Exception e)
			{
				messages.Add(new UserControl.Message(e));
			}
			return messages;
		}
		private Messages GetProduct()
		{
            DataCollect.DataCollectFacade dcf = new BenQGuru.eMES.DataCollect.DataCollectFacade(DataProvider);
            //根据产品的当前序列号获取原始序列号
            string sourceRCard = dcf.GetSourceCard(txtInput.Value.Trim().ToUpper(), string.Empty);

			Messages productmessages = new Messages();
			ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
            productmessages.AddMessages(dataCollect.GetIDInfo(sourceRCard));
			if (productmessages.IsSuccess() )
			{  
				product = (ProductInfo)productmessages.GetData().Values[0];					
			}
			else
			{
				product = new ProductInfo();
			}

			dataCollect = null;
			return productmessages;
		}

		private void ClearUI(Control ctl)
		{
			for (int i = 0; i < ctl.Controls.Count; i++)
			{
				if (ctl.Controls[i] is UserControl.UCLabelEdit)
				{
					((UCLabelEdit)ctl.Controls[i]).Value = string.Empty;
				}
				else if (ctl.Controls.Count > 0)
				{
					ClearUI(ctl.Controls[i]);
				}
			}
		}

		private Control FindControl(Control parent, string childName)
		{
			for (int i = 0; i < parent.Controls.Count; i++)
			{
				if (parent.Controls[i].Name == childName)
				{
					return parent.Controls[i];
				}
			}
			return null;
		}


		private void PrepareMSComm()
		{
			try
			{
				if (axMSComm1.PortOpen)
					axMSComm1.PortOpen = false;

				axMSComm1.CommPort = 1;
				axMSComm1.PortOpen = true;
				axMSComm1.Settings = "9600,n,8,1";
				axMSComm1.InBufferCount = 0;
				axMSComm1.OutBufferCount = 0;
				axMSComm1.InputLen = 0;
			}
			catch(Exception ex)
			{
				ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$MSCOMM_Init_Error"));//COM设备初始化失败
			}
		}
		private string prevResetType = string.Empty;
		private void ResetMSComm(string resetType)
		{
			if (prevResetType == resetType)
				return;
			try
			{
				//axMSComm1.Output = "rems\r";	// Removed by Icyer 2006/06/10 @Darfon
				axMSComm1.Output = "*RST\r";
				axMSComm1.Output = "*CLS\r";
				if (resetType == "Fre")
					axMSComm1.Output = "FREQ\r";
				else
					axMSComm1.Output = "VAC\r";	// Changed by Icyer 2006/06/10 @Darfon
					//axMSComm1.Output = "VAC\r";
				prevResetType = resetType;
				// 由于设备反应速度，需要延迟一段时间，暂定一秒，测试在500毫秒下无法读取，在800毫秒下可以读取
				// 由于设备初始化后，数据不稳定，所以延缓两秒
				this.btnTest.Enabled = false;
				System.Threading.Thread.Sleep(4000);
				this.btnTest.Enabled = true;
			}
			catch
			{
				ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$MSCOMM_Reset_Error"));//COM口复位失败
			}
		}
		private void CloseMSComm()
		{
			try
			{
				if(axMSComm1.PortOpen == true)
					axMSComm1.PortOpen = false;

				axMSComm1.Dispose();
			}
			catch{}
		}

		private string GetElectricCurrentFromCOMM()
		{
			//在读取数据之前先将输入的缓冲区清空
			axMSComm1.InBufferCount=0;
			axMSComm1.Output = "meas?\r";//发送获取电流值的命令
             
			bool bExistInput = false;
			DateTime dtStart = DateTime.Now;
			while(axMSComm1.InBufferCount<20)//一定要等待到InBufferCount<22这个长度才行
			{
				if (axMSComm1.InBufferCount > 0)
					bExistInput = true;
				// 3秒之内无输出则退出
				TimeSpan ts = DateTime.Now - dtStart;
				if (ts.TotalSeconds >= 10 && bExistInput == false)
					return string.Empty;
			}
			string input = axMSComm1.Input.ToString().Trim();

			//将缓冲区清空
			axMSComm1.InBufferCount = 0;
			axMSComm1.OutBufferCount = 0;

			return input;
		}

		private string ParseMSCommInput(string input)
		{
			try
			{
				//				axMSComm1.Output = "meas?\r";
				//
				//				string input = axMSComm1.Input.ToString().Trim();

				//string s = "MEAS?+0.000E+0=>";"MEAS?-0.000E+0=>";获取的字符类似这样
				
				int index = input.LastIndexOf("?");
				if(index > -1)
				{
					// Changed by Icyer 2006/06/10 @Darfon
					//string result = input.Substring(index+1,input.Length - index - 1 - 2);
					string result = input.Substring(index + 1, input.LastIndexOf("E") - index - 1);
					// Changed end
					return double.Parse(result).ToString();
				}
				else
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$Cannot_Get_Electric_Current"));//无法获取电流值
					return "";
				}
			}
			catch(Exception ex)
			{
				ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$Cannot_Get_Electric_Current"));//无法获取电流值
			}

			return "";
		}
		private bool bInMSCommProcess = false;
		private void GetValueFromMSComm(string type)
		{
			bInMSCommProcess = true;
			ResetMSComm(type);
			string strValue = GetElectricCurrentFromCOMM();
			strValue = ParseMSCommInput(strValue);
			currentInputValue.Value = strValue;
			bInMSCommProcess = false;
		}
		private void GetFreValue()
		{
			GetValueFromMSComm("Fre");
			SaveFreValue();
			SetResult();
		}
		private void SaveFreValue()
		{
			TabPage tab = tabGroup.TabPages[tabGroup.SelectedIndex];
			decimal deMin = this.oqcFuncTestSpecList[tabGroup.SelectedIndex].FreMin;
			decimal deMax = this.oqcFuncTestSpecList[tabGroup.SelectedIndex].FreMax;
			try
			{
			if (Convert.ToDecimal(currentInputValue.Value) < deMin ||  
				Convert.ToDecimal(currentInputValue.Value) > deMax)
			{
				currentInputValue.InnerTextBox.ForeColor = Color.Red;
			}
			else
			{
				currentInputValue.InnerTextBox.ForeColor = Color.Black;
			}
			}
			catch
			{
				currentInputValue.InnerTextBox.ForeColor = Color.Red;
				currentInputValue.Value = string.Empty;
			}
			if (currentInputValue.Value != string.Empty)
			{
				if (listTestValueFre.ContainsKey(tabGroup.SelectedIndex) == false)
					this.listTestValueFre.Add(tabGroup.SelectedIndex, currentInputValue.Value);
				else
					this.listTestValueFre[tabGroup.SelectedIndex] = currentInputValue.Value;
			}
			else
			{
				if (listTestValueFre.ContainsKey(tabGroup.SelectedIndex) == true)
					listTestValueFre.Remove(tabGroup.SelectedIndex);
			}

			GroupBox grp = (GroupBox)FindControl(tabGroup.TabPages[tabGroup.SelectedIndex], "grpElectric");
			UCLabelEdit txtElectricValue0 = (UCLabelEdit)FindControl(grp, "txtElectricValue_1");
			if (txtElectricValue0 != null)
			{
				//Application.DoEvents();
				txtElectricValue0.TextFocus(false, true);
			}
		}
		private void GetElectricValue()
		{
			GetValueFromMSComm("Electric");
			SaveElectricValue();
			SetResult();
		}
		private void SaveElectricValue()
		{
			TabPage tab = tabGroup.TabPages[tabGroup.SelectedIndex];
			decimal deMin = this.oqcFuncTestSpecList[tabGroup.SelectedIndex].ElectricMin;
			decimal deMax = this.oqcFuncTestSpecList[tabGroup.SelectedIndex].ElectricMax;
			try
			{
				if (Convert.ToDecimal(currentInputValue.Value) < deMin ||  
					Convert.ToDecimal(currentInputValue.Value) > deMax)
				{
					currentInputValue.InnerTextBox.ForeColor = Color.Red;
				}
				else
				{
					currentInputValue.InnerTextBox.ForeColor = Color.Black;
				}
			}
			catch
			{
				currentInputValue.InnerTextBox.ForeColor = Color.Red;
				currentInputValue.Value = string.Empty;
			}
			Hashtable ht = (Hashtable)this.listTestValueEle[tabGroup.SelectedIndex];
			if (ht == null)
			{
				ht = new Hashtable();
				this.listTestValueEle.Add(tabGroup.SelectedIndex, ht);
			}
			string strInputIdx = currentInputValue.Name.Substring(currentInputValue.Name.LastIndexOf("_") + 1);
			if (currentInputValue.Value != string.Empty)
			{
				if (ht.ContainsKey(strInputIdx) == false)
					ht.Add(strInputIdx, currentInputValue.Value);
				else
					ht[strInputIdx] = currentInputValue.Value;
			}
			else
			{
				if (ht.ContainsKey(strInputIdx) == true)
					ht.Remove(strInputIdx);
			}

			//Application.DoEvents();
			GroupBox grp = (GroupBox)currentInputValue.Parent;
			int iIdx = Convert.ToInt32(currentInputValue.Name.Substring(currentInputValue.Name.IndexOf("_") + 1));
			if (iIdx < this.oqcFuncTest.ElectricTestCount)
			{
				iIdx++;
				UCLabelEdit txtElectricValue0 = (UCLabelEdit)FindControl(grp, "txtElectricValue_" + iIdx.ToString());
				if (txtElectricValue0 != null)
				{
					txtElectricValue0.TextFocus(false, true);
				}
			}
			else if (tabGroup.SelectedIndex < tabGroup.TabPages.Count - 1)
			{
				tabGroup.SelectedIndex++;
				grp = (GroupBox)FindControl(tabGroup.TabPages[0], "grpFre");
				UCLabelEdit txtFreValue0 = (UCLabelEdit)FindControl(grp, "txtFreValue");
				//Application.DoEvents();
				SendKeys.Send("{TAB}");
			}
		}

		
		private bool SetResult()
		{
			bool bResult = true;
			if (this.txtMinDutyRatoValue.Value != string.Empty && 
				(Convert.ToDecimal(this.txtMinDutyRatoValue.Value) < Convert.ToDecimal(this.txtMinDutyRatoMin.Value) ||
				Convert.ToDecimal(this.txtMinDutyRatoValue.Value) > Convert.ToDecimal(this.txtMinDutyRatoMax.Value)))
			{
				bResult = false;
			}
			if (this.txtBurstMdFreValue.Value != string.Empty &&
				(Convert.ToDecimal(this.txtBurstMdFreValue.Value) < Convert.ToDecimal(this.txtBurstMdFreMin.Value) ||
				Convert.ToDecimal(this.txtBurstMdFreValue.Value) > Convert.ToDecimal(this.txtBurstMdFreMax.Value)))
			{
				bResult = false;
			}
			if (bResult == true)
			{
				for (int i = 0; i < tabGroup.TabPages.Count; i++)
				{
					if (listTestValueFre.ContainsKey(i) == true)
					{
						if (Convert.ToDecimal(this.listTestValueFre[i]) < this.oqcFuncTestSpecList[i].FreMin || 
							Convert.ToDecimal(this.listTestValueFre[i]) > this.oqcFuncTestSpecList[i].FreMax)
						{
							bResult = false;
							break;
						}
					}
					Hashtable ht = (Hashtable)this.listTestValueEle[i];
					if (ht != null)
					{
						for (int n = 0; n < this.iElectricCount; n++)
						{
							if (ht.ContainsKey((n + 1).ToString()) == true)
							{
								if (Convert.ToDecimal(ht[(n + 1).ToString()]) < this.oqcFuncTestSpecList[i].ElectricMin || 
									Convert.ToDecimal(ht[(n + 1).ToString()]) > this.oqcFuncTestSpecList[i].ElectricMax)
								{
									bResult = false;
									break;
								}
							}
						}
					}
					if (bResult == false)
						break;
				}
			}
			if (bResult == false)
			{
				lblResult.Text = "NG";
				lblResult.ForeColor = Color.Red;
			}
			else
			{
				lblResult.Text = string.Empty;
				lblResult.ForeColor = Color.Green;
			}
			return bResult;
		}

		private bool CheckDataComplete()
		{
			if (this.txtMinDutyRatoValue.Value == string.Empty)
			{
				this.currentInputValue = this.txtMinDutyRatoValue;
				return false;
			}
			if (this.txtBurstMdFreValue.Value == string.Empty)
			{
				this.currentInputValue = this.txtBurstMdFreValue;
				return false;
			}
			for (int i = 0; i < tabGroup.TabPages.Count; i++)
			{
				if (this.listTestValueFre.ContainsKey(i) == false)
				{
					tabGroup.TabIndex = i;
					GroupBox grpFre1 = (GroupBox)FindControl(tabGroup.TabPages[i], "grpFre");
					UCLabelEdit txtFreValue1 = (UCLabelEdit)FindControl(grpFre1, "txtFreValue");
					this.currentInputValue = txtFreValue1;
					return false;
				}
				Hashtable ht = (Hashtable)this.listTestValueEle[i];
				if (ht == null)
				{
					tabGroup.TabIndex = i;
					GroupBox grpElectric1 = (GroupBox)FindControl(tabGroup.TabPages[i], "grpElectric");
					UCLabelEdit txtEleValueTmp = (UCLabelEdit)FindControl(grpElectric1, "txtElectricValue_1");
					this.currentInputValue = txtEleValueTmp;
					return false;
				}
				for (int n = 1; n <= this.oqcFuncTest.ElectricTestCount; n++)
				{
					if (ht.ContainsKey(n.ToString()) == false)
					{
						tabGroup.TabIndex = i;
						GroupBox grpElectric1 = (GroupBox)FindControl(tabGroup.TabPages[i], "grpElectric");
						UCLabelEdit txtEleValueTmp = (UCLabelEdit)FindControl(grpElectric1, "txtElectricValue_" + n.ToString());
						this.currentInputValue = txtEleValueTmp;
						return false;
					}
				}
			}
			return true;
		}

		private void btnLockInput_Click(object sender, System.EventArgs e)
		{
			if (this.txtInput.Enabled == true)
			{
				this.LockInput(true);
			}
			else
			{
				this.LockInput(false);
			}
		}

		private void btnTest_Click(object sender, System.EventArgs e)
		{
			if (bInMSCommProcess == true)
				return;
			if (currentInputValue == null)
				return;
			try
			{
				if (currentInputValue.Name.StartsWith("txtFreValue") == true)
				{
					this.GetFreValue();
				}
				else if (currentInputValue.Name.StartsWith("txtElectricValue_") == true)
				{
					this.GetElectricValue();
				}
			}
			catch {}
		}

		private void ValueInputTxt_GotFocus(object sender, EventArgs e)
		{
			if (bInMSCommProcess == true)
				return;
			currentInputValue = (UCLabelEdit)sender;
		}

		private void ValueInputTxt_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				if (bInMSCommProcess == true)
					return;
				if (currentInputValue == null)
					return;
				try
				{
					if (currentInputValue.Name.StartsWith("txtFreValue") == true)
					{
						if (this.chkManualInput.Checked == true)
						{
							SaveFreValue();
							SetResult();
						}
						else
						{
							this.GetFreValue();		// 按Enter读取设备数据
						}
					}
					else if (currentInputValue.Name.StartsWith("txtElectricValue_") == true)
					{
						if (this.chkManualInput.Checked == true)
						{
							SaveElectricValue();
							SetResult();
						}
						else
						{
							this.GetElectricValue();	// 按Enter读取设备数据
						}
					}
				}
				catch {}
			}
		}

		private void txtMinDutyRatoValue_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				SetResult();
				if (Convert.ToDecimal(this.txtMinDutyRatoValue.Value) < this.oqcFuncTest.MinDutyRatoMin || 
					Convert.ToDecimal(this.txtMinDutyRatoValue.Value) > this.oqcFuncTest.MinDutyRatoMax)
				{
					this.txtMinDutyRatoValue.InnerTextBox.ForeColor = Color.Red;
				}
				else
				{
					this.txtMinDutyRatoValue.InnerTextBox.ForeColor = Color.Black;
				}
				//Application.DoEvents();
				txtBurstMdFreValue.TextFocus(false, true);
			}
		}

		private void txtBurstMdFreValue_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				SetResult();
				if (Convert.ToDecimal(this.txtBurstMdFreValue.Value) < this.oqcFuncTest.BurstMdFreMin || 
					Convert.ToDecimal(this.txtBurstMdFreValue.Value) > this.oqcFuncTest.BurstMdFreMax)
				{
					this.txtBurstMdFreValue.InnerTextBox.ForeColor = Color.Red;
				}
				else
				{
					this.txtBurstMdFreValue.InnerTextBox.ForeColor = Color.Black;
				}
				//Application.DoEvents();
				tabGroup.SelectedIndex = 0;
				GroupBox grp = (GroupBox)FindControl(tabGroup.TabPages[0], "grpFre");
				UCLabelEdit txtFreValue0 = (UCLabelEdit)FindControl(grp, "txtFreValue");
				txtFreValue0.TextFocus(false, true);
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if (CheckDataComplete() == false)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message("$DataCollect_Not_Complete"));
				//Application.DoEvents();
				if (this.currentInputValue != null)
					this.currentInputValue.TextFocus(false, true);
				return;
			}
			bool bResult = this.SetResult();
			if (bResult == true)
			{
				lblResult.Text = "PASS";
				lblResult.ForeColor = Color.Green;
			}
			if (MessageBox.Show(UserControl.MutiLanguages.ParserMessage("$Confirm_Submit_OQCFuncTest ") + lblResult.Text, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;
			ActionFactory actionFactory = new ActionFactory(this.DataProvider);
			IAction action = actionFactory.CreateAction(ActionType.DataCollectAction_OQCFuncTest);
			OQCFuncTestActionEventArgs args = new OQCFuncTestActionEventArgs();
			args.RunningCard = product.LastSimulation.RunningCard;
			args.ActionType = ActionType.DataCollectAction_OQCPass;	// 以OQCPass作为Action检查
			args.ResourceCode = ApplicationService.Current().ResourceCode;
			args.UserCode = ApplicationService.Current().UserCode;
			args.ProductInfo = product;
			args.oqcFuncTest = this.oqcFuncTest;
			args.oqcFuncTestSpec = this.oqcFuncTestSpecList;
			args.minDutyRatoValue = Convert.ToDecimal(this.txtMinDutyRatoValue.Value);
			args.burstMdFreValue = Convert.ToDecimal(this.txtBurstMdFreValue.Value);
			args.listTestValueFre = this.listTestValueFre;
			args.listTestValueEle = this.listTestValueEle;
			args.Result = bResult;

			Messages messages = new Messages();

			messages.AddMessages(action.Execute(args));	
			if (messages.IsSuccess())
			{
				this.ClearUI(this);
				tabGroup.SelectedIndex = 0;
				this.LockInput(false);
				//Application.DoEvents();
				this.txtInput.TextFocus(false, true);
			}
			else
			{
				ApplicationRun.GetInfoForm().Add(messages);
			}
		}

		private void FOQCFuncTest_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F5)
			{
				this.btnSave_Click(null, null);
			}
		}
	}
}
