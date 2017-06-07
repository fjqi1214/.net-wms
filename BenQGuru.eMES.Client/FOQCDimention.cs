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
	/// FOQCDimention 的摘要说明。
	/// </summary>
	public class FOQCDimention : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel3;
		private UserControl.UCLabelEdit txtInput;
		private UserControl.UCButton btnLockInput;
		private UserControl.UCButton btnExit;
		private UserControl.UCButton btnTest;
		private System.Windows.Forms.Panel palTop;
		private System.Windows.Forms.Panel pnlMiddle;
		private UserControl.UCLabelEdit ucLabelEdit20;
		private UserControl.UCLabelEdit ucLabelEdit5;
		private UserControl.UCLabelEdit ucLabelEdit6;
		private UserControl.UCLabelEdit ucLabelEdit7;
		private UserControl.UCLabelEdit ucLabelEdit8;
		private UserControl.UCLabelEdit ucLabelEdit9;
		private UserControl.UCLabelEdit ucLabelEdit10;
		private UserControl.UCLabelEdit ucLabelEdit11;
		private UserControl.UCLabelEdit ucLabelEdit12;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox grbResult;
		private UserControl.UCLabelEdit txtLotNo;
		private UserControl.UCLabelEdit txtMoCode;
		private UserControl.UCLabelEdit txtItemCode;
		private UserControl.UCLabelEdit txtUnit;
		private UserControl.UCLabelEdit txtWidthValue;
		private UserControl.UCLabelEdit txtBoardHeightValue;
		private UserControl.UCLabelEdit txtHeightValue;
		private UserControl.UCLabelEdit txtAllHeightValue;
		private UserControl.UCLabelEdit txtLeft2RightValue;
		private UserControl.UCLabelEdit txtLeft2MiddleValue;
		private UserControl.UCLabelEdit txtRight2MiddleValue;
		private UserControl.UCLabelEdit txtLengthValue;
		private UserControl.UCLabelEdit txtLengthMax;
		private UserControl.UCLabelEdit txtWidthMax;
		private UserControl.UCLabelEdit txtBoardHeightMax;
		private UserControl.UCLabelEdit txtHeightMax;
		private UserControl.UCLabelEdit txtLeft2RightMax;
		private UserControl.UCLabelEdit txtLeft2MiddleMax;
		private UserControl.UCLabelEdit txtRight2MiddleMax;
		private UserControl.UCLabelEdit txtAllHeightMax;
		private UserControl.UCLabelEdit txtWidthMin;
		private UserControl.UCLabelEdit txtLeft2RightMin;
		private UserControl.UCLabelEdit txtBoardHeightMin;
		private UserControl.UCLabelEdit txtHeightMin;
		private UserControl.UCLabelEdit txtAllHeightMin;
		private UserControl.UCLabelEdit txtLeft2MiddleMin;
		private UserControl.UCLabelEdit txtRight2MiddleMin;
		private UserControl.UCLabelEdit txtLengthMin;
		private UserControl.UCButton btnSave;
		private System.Windows.Forms.Panel pnlMiddle2;

		private FInfoForm _infoForm;
		private System.Windows.Forms.Label lblResult;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FOQCDimention()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FOQCDimention));
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSave = new UserControl.UCButton();
            this.btnTest = new UserControl.UCButton();
            this.btnExit = new UserControl.UCButton();
            this.btnLockInput = new UserControl.UCButton();
            this.txtInput = new UserControl.UCLabelEdit();
            this.palTop = new System.Windows.Forms.Panel();
            this.txtLotNo = new UserControl.UCLabelEdit();
            this.txtMoCode = new UserControl.UCLabelEdit();
            this.txtItemCode = new UserControl.UCLabelEdit();
            this.txtUnit = new UserControl.UCLabelEdit();
            this.pnlMiddle = new System.Windows.Forms.Panel();
            this.pnlMiddle2 = new System.Windows.Forms.Panel();
            this.grbResult = new System.Windows.Forms.GroupBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtWidthValue = new UserControl.UCLabelEdit();
            this.txtBoardHeightValue = new UserControl.UCLabelEdit();
            this.txtHeightValue = new UserControl.UCLabelEdit();
            this.txtAllHeightValue = new UserControl.UCLabelEdit();
            this.txtLeft2RightValue = new UserControl.UCLabelEdit();
            this.txtLeft2MiddleValue = new UserControl.UCLabelEdit();
            this.txtRight2MiddleValue = new UserControl.UCLabelEdit();
            this.txtLengthValue = new UserControl.UCLabelEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLengthMax = new UserControl.UCLabelEdit();
            this.txtWidthMax = new UserControl.UCLabelEdit();
            this.txtBoardHeightMax = new UserControl.UCLabelEdit();
            this.txtHeightMax = new UserControl.UCLabelEdit();
            this.txtLeft2RightMax = new UserControl.UCLabelEdit();
            this.txtLeft2MiddleMax = new UserControl.UCLabelEdit();
            this.txtRight2MiddleMax = new UserControl.UCLabelEdit();
            this.txtAllHeightMax = new UserControl.UCLabelEdit();
            this.txtWidthMin = new UserControl.UCLabelEdit();
            this.txtLeft2RightMin = new UserControl.UCLabelEdit();
            this.txtBoardHeightMin = new UserControl.UCLabelEdit();
            this.txtHeightMin = new UserControl.UCLabelEdit();
            this.txtAllHeightMin = new UserControl.UCLabelEdit();
            this.txtLeft2MiddleMin = new UserControl.UCLabelEdit();
            this.txtRight2MiddleMin = new UserControl.UCLabelEdit();
            this.txtLengthMin = new UserControl.UCLabelEdit();
            this.ucLabelEdit12 = new UserControl.UCLabelEdit();
            this.ucLabelEdit11 = new UserControl.UCLabelEdit();
            this.ucLabelEdit10 = new UserControl.UCLabelEdit();
            this.ucLabelEdit9 = new UserControl.UCLabelEdit();
            this.ucLabelEdit8 = new UserControl.UCLabelEdit();
            this.ucLabelEdit7 = new UserControl.UCLabelEdit();
            this.ucLabelEdit6 = new UserControl.UCLabelEdit();
            this.ucLabelEdit5 = new UserControl.UCLabelEdit();
            this.ucLabelEdit20 = new UserControl.UCLabelEdit();
            this.panel3.SuspendLayout();
            this.palTop.SuspendLayout();
            this.pnlMiddle.SuspendLayout();
            this.pnlMiddle2.SuspendLayout();
            this.grbResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnTest);
            this.panel3.Controls.Add(this.btnExit);
            this.panel3.Controls.Add(this.btnLockInput);
            this.panel3.Controls.Add(this.txtInput);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 502);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(894, 60);
            this.panel3.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.ButtonType = UserControl.ButtonTypes.None;
            this.btnSave.Caption = "保存(F5)";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(699, 22);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 22);
            this.btnSave.TabIndex = 291;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.SystemColors.Control;
            this.btnTest.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTest.BackgroundImage")));
            this.btnTest.ButtonType = UserControl.ButtonTypes.None;
            this.btnTest.Caption = "测试";
            this.btnTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTest.Location = new System.Drawing.Point(368, 22);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(88, 22);
            this.btnTest.TabIndex = 290;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(793, 22);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 289;
            // 
            // btnLockInput
            // 
            this.btnLockInput.BackColor = System.Drawing.SystemColors.Control;
            this.btnLockInput.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLockInput.BackgroundImage")));
            this.btnLockInput.ButtonType = UserControl.ButtonTypes.None;
            this.btnLockInput.Caption = "锁定";
            this.btnLockInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLockInput.Location = new System.Drawing.Point(274, 22);
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
            this.txtInput.Location = new System.Drawing.Point(6, 22);
            this.txtInput.MaxLength = 4000;
            this.txtInput.Multiline = false;
            this.txtInput.Name = "txtInput";
            this.txtInput.PasswordChar = '\0';
            this.txtInput.ReadOnly = false;
            this.txtInput.ShowCheckBox = false;
            this.txtInput.Size = new System.Drawing.Size(249, 23);
            this.txtInput.TabIndex = 0;
            this.txtInput.TabNext = false;
            this.txtInput.Value = "";
            this.txtInput.WidthType = UserControl.WidthTypes.Long;
            this.txtInput.XAlign = 55;
            this.txtInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_TxtboxKeyPress);
            // 
            // palTop
            // 
            this.palTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.palTop.Controls.Add(this.txtLotNo);
            this.palTop.Controls.Add(this.txtMoCode);
            this.palTop.Controls.Add(this.txtItemCode);
            this.palTop.Controls.Add(this.txtUnit);
            this.palTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.palTop.Location = new System.Drawing.Point(0, 0);
            this.palTop.Name = "palTop";
            this.palTop.Size = new System.Drawing.Size(894, 45);
            this.palTop.TabIndex = 1;
            // 
            // txtLotNo
            // 
            this.txtLotNo.AllowEditOnlyChecked = true;
            this.txtLotNo.BackColor = System.Drawing.Color.Gainsboro;
            this.txtLotNo.Caption = "送检批号";
            this.txtLotNo.Checked = false;
            this.txtLotNo.EditType = UserControl.EditTypes.String;
            this.txtLotNo.Location = new System.Drawing.Point(13, 15);
            this.txtLotNo.MaxLength = 4000;
            this.txtLotNo.Multiline = false;
            this.txtLotNo.Name = "txtLotNo";
            this.txtLotNo.PasswordChar = '\0';
            this.txtLotNo.ReadOnly = true;
            this.txtLotNo.ShowCheckBox = false;
            this.txtLotNo.Size = new System.Drawing.Size(194, 22);
            this.txtLotNo.TabIndex = 4;
            this.txtLotNo.TabNext = false;
            this.txtLotNo.Value = "";
            this.txtLotNo.WidthType = UserControl.WidthTypes.Normal;
            this.txtLotNo.XAlign = 74;
            // 
            // txtMoCode
            // 
            this.txtMoCode.AllowEditOnlyChecked = true;
            this.txtMoCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtMoCode.Caption = "工单代码";
            this.txtMoCode.Checked = false;
            this.txtMoCode.EditType = UserControl.EditTypes.String;
            this.txtMoCode.Location = new System.Drawing.Point(223, 15);
            this.txtMoCode.MaxLength = 4000;
            this.txtMoCode.Multiline = false;
            this.txtMoCode.Name = "txtMoCode";
            this.txtMoCode.PasswordChar = '\0';
            this.txtMoCode.ReadOnly = true;
            this.txtMoCode.ShowCheckBox = false;
            this.txtMoCode.Size = new System.Drawing.Size(194, 22);
            this.txtMoCode.TabIndex = 3;
            this.txtMoCode.TabNext = false;
            this.txtMoCode.Value = "";
            this.txtMoCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtMoCode.XAlign = 284;
            // 
            // txtItemCode
            // 
            this.txtItemCode.AllowEditOnlyChecked = true;
            this.txtItemCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtItemCode.Caption = "产品代码";
            this.txtItemCode.Checked = false;
            this.txtItemCode.EditType = UserControl.EditTypes.String;
            this.txtItemCode.Location = new System.Drawing.Point(441, 15);
            this.txtItemCode.MaxLength = 4000;
            this.txtItemCode.Multiline = false;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.PasswordChar = '\0';
            this.txtItemCode.ReadOnly = true;
            this.txtItemCode.ShowCheckBox = false;
            this.txtItemCode.Size = new System.Drawing.Size(194, 22);
            this.txtItemCode.TabIndex = 2;
            this.txtItemCode.TabNext = false;
            this.txtItemCode.Value = "";
            this.txtItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtItemCode.XAlign = 502;
            // 
            // txtUnit
            // 
            this.txtUnit.AllowEditOnlyChecked = true;
            this.txtUnit.BackColor = System.Drawing.Color.Gainsboro;
            this.txtUnit.Caption = "单位";
            this.txtUnit.Checked = false;
            this.txtUnit.EditType = UserControl.EditTypes.String;
            this.txtUnit.Location = new System.Drawing.Point(658, 15);
            this.txtUnit.MaxLength = 4000;
            this.txtUnit.Multiline = false;
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.PasswordChar = '\0';
            this.txtUnit.ReadOnly = true;
            this.txtUnit.ShowCheckBox = false;
            this.txtUnit.Size = new System.Drawing.Size(170, 22);
            this.txtUnit.TabIndex = 1;
            this.txtUnit.TabNext = false;
            this.txtUnit.Value = "MM";
            this.txtUnit.WidthType = UserControl.WidthTypes.Normal;
            this.txtUnit.XAlign = 695;
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Controls.Add(this.pnlMiddle2);
            this.pnlMiddle.Controls.Add(this.ucLabelEdit12);
            this.pnlMiddle.Controls.Add(this.ucLabelEdit11);
            this.pnlMiddle.Controls.Add(this.ucLabelEdit10);
            this.pnlMiddle.Controls.Add(this.ucLabelEdit9);
            this.pnlMiddle.Controls.Add(this.ucLabelEdit8);
            this.pnlMiddle.Controls.Add(this.ucLabelEdit7);
            this.pnlMiddle.Controls.Add(this.ucLabelEdit6);
            this.pnlMiddle.Controls.Add(this.ucLabelEdit5);
            this.pnlMiddle.Controls.Add(this.ucLabelEdit20);
            this.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddle.Location = new System.Drawing.Point(0, 45);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.Size = new System.Drawing.Size(894, 457);
            this.pnlMiddle.TabIndex = 2;
            // 
            // pnlMiddle2
            // 
            this.pnlMiddle2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMiddle2.Controls.Add(this.grbResult);
            this.pnlMiddle2.Controls.Add(this.txtWidthValue);
            this.pnlMiddle2.Controls.Add(this.txtBoardHeightValue);
            this.pnlMiddle2.Controls.Add(this.txtHeightValue);
            this.pnlMiddle2.Controls.Add(this.txtAllHeightValue);
            this.pnlMiddle2.Controls.Add(this.txtLeft2RightValue);
            this.pnlMiddle2.Controls.Add(this.txtLeft2MiddleValue);
            this.pnlMiddle2.Controls.Add(this.txtRight2MiddleValue);
            this.pnlMiddle2.Controls.Add(this.txtLengthValue);
            this.pnlMiddle2.Controls.Add(this.label8);
            this.pnlMiddle2.Controls.Add(this.label7);
            this.pnlMiddle2.Controls.Add(this.label6);
            this.pnlMiddle2.Controls.Add(this.label5);
            this.pnlMiddle2.Controls.Add(this.label4);
            this.pnlMiddle2.Controls.Add(this.label3);
            this.pnlMiddle2.Controls.Add(this.label2);
            this.pnlMiddle2.Controls.Add(this.label1);
            this.pnlMiddle2.Controls.Add(this.txtLengthMax);
            this.pnlMiddle2.Controls.Add(this.txtWidthMax);
            this.pnlMiddle2.Controls.Add(this.txtBoardHeightMax);
            this.pnlMiddle2.Controls.Add(this.txtHeightMax);
            this.pnlMiddle2.Controls.Add(this.txtLeft2RightMax);
            this.pnlMiddle2.Controls.Add(this.txtLeft2MiddleMax);
            this.pnlMiddle2.Controls.Add(this.txtRight2MiddleMax);
            this.pnlMiddle2.Controls.Add(this.txtAllHeightMax);
            this.pnlMiddle2.Controls.Add(this.txtWidthMin);
            this.pnlMiddle2.Controls.Add(this.txtLeft2RightMin);
            this.pnlMiddle2.Controls.Add(this.txtBoardHeightMin);
            this.pnlMiddle2.Controls.Add(this.txtHeightMin);
            this.pnlMiddle2.Controls.Add(this.txtAllHeightMin);
            this.pnlMiddle2.Controls.Add(this.txtLeft2MiddleMin);
            this.pnlMiddle2.Controls.Add(this.txtRight2MiddleMin);
            this.pnlMiddle2.Controls.Add(this.txtLengthMin);
            this.pnlMiddle2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddle2.Location = new System.Drawing.Point(0, 0);
            this.pnlMiddle2.Name = "pnlMiddle2";
            this.pnlMiddle2.Size = new System.Drawing.Size(894, 457);
            this.pnlMiddle2.TabIndex = 0;
            // 
            // grbResult
            // 
            this.grbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grbResult.Controls.Add(this.lblResult);
            this.grbResult.Location = new System.Drawing.Point(3, 330);
            this.grbResult.Name = "grbResult";
            this.grbResult.Size = new System.Drawing.Size(227, 122);
            this.grbResult.TabIndex = 53;
            this.grbResult.TabStop = false;
            this.grbResult.Text = "测试结果";
            // 
            // lblResult
            // 
            this.lblResult.Font = new System.Drawing.Font("宋体", 72F);
            this.lblResult.Location = new System.Drawing.Point(13, 22);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(204, 89);
            this.lblResult.TabIndex = 53;
            // 
            // txtWidthValue
            // 
            this.txtWidthValue.AllowEditOnlyChecked = true;
            this.txtWidthValue.BackColor = System.Drawing.Color.Gainsboro;
            this.txtWidthValue.Caption = "实际值";
            this.txtWidthValue.Checked = false;
            this.txtWidthValue.EditType = UserControl.EditTypes.Number;
            this.txtWidthValue.Location = new System.Drawing.Point(471, 36);
            this.txtWidthValue.MaxLength = 4000;
            this.txtWidthValue.Multiline = false;
            this.txtWidthValue.Name = "txtWidthValue";
            this.txtWidthValue.PasswordChar = '\0';
            this.txtWidthValue.ReadOnly = false;
            this.txtWidthValue.ShowCheckBox = false;
            this.txtWidthValue.Size = new System.Drawing.Size(182, 22);
            this.txtWidthValue.TabIndex = 1;
            this.txtWidthValue.TabNext = false;
            this.txtWidthValue.Tag = "88";
            this.txtWidthValue.Value = "";
            this.txtWidthValue.WidthType = UserControl.WidthTypes.Normal;
            this.txtWidthValue.XAlign = 520;
            this.txtWidthValue.Leave += new System.EventHandler(this.txtLengthValue_Leave);
            // 
            // txtBoardHeightValue
            // 
            this.txtBoardHeightValue.AllowEditOnlyChecked = true;
            this.txtBoardHeightValue.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBoardHeightValue.Caption = "实际值";
            this.txtBoardHeightValue.Checked = false;
            this.txtBoardHeightValue.EditType = UserControl.EditTypes.Number;
            this.txtBoardHeightValue.Location = new System.Drawing.Point(471, 67);
            this.txtBoardHeightValue.MaxLength = 4000;
            this.txtBoardHeightValue.Multiline = false;
            this.txtBoardHeightValue.Name = "txtBoardHeightValue";
            this.txtBoardHeightValue.PasswordChar = '\0';
            this.txtBoardHeightValue.ReadOnly = false;
            this.txtBoardHeightValue.ShowCheckBox = false;
            this.txtBoardHeightValue.Size = new System.Drawing.Size(182, 22);
            this.txtBoardHeightValue.TabIndex = 2;
            this.txtBoardHeightValue.TabNext = false;
            this.txtBoardHeightValue.Tag = "88";
            this.txtBoardHeightValue.Value = "";
            this.txtBoardHeightValue.WidthType = UserControl.WidthTypes.Normal;
            this.txtBoardHeightValue.XAlign = 520;
            this.txtBoardHeightValue.Leave += new System.EventHandler(this.txtLengthValue_Leave);
            // 
            // txtHeightValue
            // 
            this.txtHeightValue.AllowEditOnlyChecked = true;
            this.txtHeightValue.BackColor = System.Drawing.Color.Gainsboro;
            this.txtHeightValue.Caption = "实际值";
            this.txtHeightValue.Checked = false;
            this.txtHeightValue.EditType = UserControl.EditTypes.Number;
            this.txtHeightValue.Location = new System.Drawing.Point(471, 97);
            this.txtHeightValue.MaxLength = 4000;
            this.txtHeightValue.Multiline = false;
            this.txtHeightValue.Name = "txtHeightValue";
            this.txtHeightValue.PasswordChar = '\0';
            this.txtHeightValue.ReadOnly = false;
            this.txtHeightValue.ShowCheckBox = false;
            this.txtHeightValue.Size = new System.Drawing.Size(182, 22);
            this.txtHeightValue.TabIndex = 3;
            this.txtHeightValue.TabNext = false;
            this.txtHeightValue.Tag = "88";
            this.txtHeightValue.Value = "";
            this.txtHeightValue.WidthType = UserControl.WidthTypes.Normal;
            this.txtHeightValue.XAlign = 520;
            this.txtHeightValue.Leave += new System.EventHandler(this.txtLengthValue_Leave);
            // 
            // txtAllHeightValue
            // 
            this.txtAllHeightValue.AllowEditOnlyChecked = true;
            this.txtAllHeightValue.BackColor = System.Drawing.Color.Gainsboro;
            this.txtAllHeightValue.Caption = "实际值";
            this.txtAllHeightValue.Checked = false;
            this.txtAllHeightValue.EditType = UserControl.EditTypes.Number;
            this.txtAllHeightValue.Location = new System.Drawing.Point(471, 126);
            this.txtAllHeightValue.MaxLength = 4000;
            this.txtAllHeightValue.Multiline = false;
            this.txtAllHeightValue.Name = "txtAllHeightValue";
            this.txtAllHeightValue.PasswordChar = '\0';
            this.txtAllHeightValue.ReadOnly = false;
            this.txtAllHeightValue.ShowCheckBox = false;
            this.txtAllHeightValue.Size = new System.Drawing.Size(182, 23);
            this.txtAllHeightValue.TabIndex = 4;
            this.txtAllHeightValue.TabNext = false;
            this.txtAllHeightValue.Tag = "88";
            this.txtAllHeightValue.Value = "";
            this.txtAllHeightValue.WidthType = UserControl.WidthTypes.Normal;
            this.txtAllHeightValue.XAlign = 520;
            this.txtAllHeightValue.Leave += new System.EventHandler(this.txtLengthValue_Leave);
            // 
            // txtLeft2RightValue
            // 
            this.txtLeft2RightValue.AllowEditOnlyChecked = true;
            this.txtLeft2RightValue.BackColor = System.Drawing.Color.Gainsboro;
            this.txtLeft2RightValue.Caption = "实际值";
            this.txtLeft2RightValue.Checked = false;
            this.txtLeft2RightValue.EditType = UserControl.EditTypes.Number;
            this.txtLeft2RightValue.Location = new System.Drawing.Point(471, 156);
            this.txtLeft2RightValue.MaxLength = 4000;
            this.txtLeft2RightValue.Multiline = false;
            this.txtLeft2RightValue.Name = "txtLeft2RightValue";
            this.txtLeft2RightValue.PasswordChar = '\0';
            this.txtLeft2RightValue.ReadOnly = false;
            this.txtLeft2RightValue.ShowCheckBox = false;
            this.txtLeft2RightValue.Size = new System.Drawing.Size(182, 22);
            this.txtLeft2RightValue.TabIndex = 5;
            this.txtLeft2RightValue.TabNext = false;
            this.txtLeft2RightValue.Tag = "88";
            this.txtLeft2RightValue.Value = "";
            this.txtLeft2RightValue.WidthType = UserControl.WidthTypes.Normal;
            this.txtLeft2RightValue.XAlign = 520;
            this.txtLeft2RightValue.Leave += new System.EventHandler(this.txtLengthValue_Leave);
            // 
            // txtLeft2MiddleValue
            // 
            this.txtLeft2MiddleValue.AllowEditOnlyChecked = true;
            this.txtLeft2MiddleValue.BackColor = System.Drawing.Color.Gainsboro;
            this.txtLeft2MiddleValue.Caption = "实际值";
            this.txtLeft2MiddleValue.Checked = false;
            this.txtLeft2MiddleValue.EditType = UserControl.EditTypes.Number;
            this.txtLeft2MiddleValue.Location = new System.Drawing.Point(471, 187);
            this.txtLeft2MiddleValue.MaxLength = 4000;
            this.txtLeft2MiddleValue.Multiline = false;
            this.txtLeft2MiddleValue.Name = "txtLeft2MiddleValue";
            this.txtLeft2MiddleValue.PasswordChar = '\0';
            this.txtLeft2MiddleValue.ReadOnly = false;
            this.txtLeft2MiddleValue.ShowCheckBox = false;
            this.txtLeft2MiddleValue.Size = new System.Drawing.Size(182, 22);
            this.txtLeft2MiddleValue.TabIndex = 6;
            this.txtLeft2MiddleValue.TabNext = false;
            this.txtLeft2MiddleValue.Tag = "88";
            this.txtLeft2MiddleValue.Value = "";
            this.txtLeft2MiddleValue.WidthType = UserControl.WidthTypes.Normal;
            this.txtLeft2MiddleValue.XAlign = 520;
            this.txtLeft2MiddleValue.Leave += new System.EventHandler(this.txtLengthValue_Leave);
            // 
            // txtRight2MiddleValue
            // 
            this.txtRight2MiddleValue.AllowEditOnlyChecked = true;
            this.txtRight2MiddleValue.BackColor = System.Drawing.Color.Gainsboro;
            this.txtRight2MiddleValue.Caption = "实际值";
            this.txtRight2MiddleValue.Checked = false;
            this.txtRight2MiddleValue.EditType = UserControl.EditTypes.Number;
            this.txtRight2MiddleValue.Location = new System.Drawing.Point(471, 215);
            this.txtRight2MiddleValue.MaxLength = 4000;
            this.txtRight2MiddleValue.Multiline = false;
            this.txtRight2MiddleValue.Name = "txtRight2MiddleValue";
            this.txtRight2MiddleValue.PasswordChar = '\0';
            this.txtRight2MiddleValue.ReadOnly = false;
            this.txtRight2MiddleValue.ShowCheckBox = false;
            this.txtRight2MiddleValue.Size = new System.Drawing.Size(182, 23);
            this.txtRight2MiddleValue.TabIndex = 7;
            this.txtRight2MiddleValue.TabNext = false;
            this.txtRight2MiddleValue.Tag = "88";
            this.txtRight2MiddleValue.Value = "";
            this.txtRight2MiddleValue.WidthType = UserControl.WidthTypes.Normal;
            this.txtRight2MiddleValue.XAlign = 520;
            this.txtRight2MiddleValue.Leave += new System.EventHandler(this.txtLengthValue_Leave);
            // 
            // txtLengthValue
            // 
            this.txtLengthValue.AllowEditOnlyChecked = true;
            this.txtLengthValue.BackColor = System.Drawing.Color.Gainsboro;
            this.txtLengthValue.Caption = "实际值";
            this.txtLengthValue.Checked = false;
            this.txtLengthValue.EditType = UserControl.EditTypes.Number;
            this.txtLengthValue.Location = new System.Drawing.Point(471, 7);
            this.txtLengthValue.MaxLength = 4000;
            this.txtLengthValue.Multiline = false;
            this.txtLengthValue.Name = "txtLengthValue";
            this.txtLengthValue.PasswordChar = '\0';
            this.txtLengthValue.ReadOnly = false;
            this.txtLengthValue.ShowCheckBox = false;
            this.txtLengthValue.Size = new System.Drawing.Size(182, 23);
            this.txtLengthValue.TabIndex = 0;
            this.txtLengthValue.TabNext = false;
            this.txtLengthValue.Tag = "88";
            this.txtLengthValue.Value = "";
            this.txtLengthValue.WidthType = UserControl.WidthTypes.Normal;
            this.txtLengthValue.XAlign = 520;
            this.txtLengthValue.Leave += new System.EventHandler(this.txtLengthValue_Leave);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(10, 219);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 21);
            this.label8.TabIndex = 43;
            this.label8.Text = "右孔到中间孔";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(10, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 22);
            this.label7.TabIndex = 42;
            this.label7.Text = "左孔到中间孔";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 21);
            this.label6.TabIndex = 41;
            this.label6.Text = "左孔到右孔";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 21);
            this.label5.TabIndex = 40;
            this.label5.Text = "总高";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 21);
            this.label4.TabIndex = 39;
            this.label4.Text = "厚度";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 22);
            this.label3.TabIndex = 38;
            this.label3.Text = "板上高";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 21);
            this.label2.TabIndex = 37;
            this.label2.Text = "宽度";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 21);
            this.label1.TabIndex = 36;
            this.label1.Text = "长度";
            // 
            // txtLengthMax
            // 
            this.txtLengthMax.AllowEditOnlyChecked = true;
            this.txtLengthMax.BackColor = System.Drawing.Color.Gainsboro;
            this.txtLengthMax.Caption = "MAX";
            this.txtLengthMax.Checked = false;
            this.txtLengthMax.EditType = UserControl.EditTypes.String;
            this.txtLengthMax.Location = new System.Drawing.Point(291, 7);
            this.txtLengthMax.MaxLength = 4000;
            this.txtLengthMax.Multiline = false;
            this.txtLengthMax.Name = "txtLengthMax";
            this.txtLengthMax.PasswordChar = '\0';
            this.txtLengthMax.ReadOnly = true;
            this.txtLengthMax.ShowCheckBox = false;
            this.txtLengthMax.Size = new System.Drawing.Size(164, 23);
            this.txtLengthMax.TabIndex = 35;
            this.txtLengthMax.TabNext = false;
            this.txtLengthMax.Tag = "88";
            this.txtLengthMax.Value = "";
            this.txtLengthMax.WidthType = UserControl.WidthTypes.Normal;
            this.txtLengthMax.XAlign = 322;
            // 
            // txtWidthMax
            // 
            this.txtWidthMax.AllowEditOnlyChecked = true;
            this.txtWidthMax.BackColor = System.Drawing.Color.Gainsboro;
            this.txtWidthMax.Caption = "MAX";
            this.txtWidthMax.Checked = false;
            this.txtWidthMax.EditType = UserControl.EditTypes.String;
            this.txtWidthMax.Location = new System.Drawing.Point(291, 36);
            this.txtWidthMax.MaxLength = 4000;
            this.txtWidthMax.Multiline = false;
            this.txtWidthMax.Name = "txtWidthMax";
            this.txtWidthMax.PasswordChar = '\0';
            this.txtWidthMax.ReadOnly = true;
            this.txtWidthMax.ShowCheckBox = false;
            this.txtWidthMax.Size = new System.Drawing.Size(164, 22);
            this.txtWidthMax.TabIndex = 34;
            this.txtWidthMax.TabNext = false;
            this.txtWidthMax.Tag = "88";
            this.txtWidthMax.Value = "";
            this.txtWidthMax.WidthType = UserControl.WidthTypes.Normal;
            this.txtWidthMax.XAlign = 322;
            // 
            // txtBoardHeightMax
            // 
            this.txtBoardHeightMax.AllowEditOnlyChecked = true;
            this.txtBoardHeightMax.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBoardHeightMax.Caption = "MAX";
            this.txtBoardHeightMax.Checked = false;
            this.txtBoardHeightMax.EditType = UserControl.EditTypes.String;
            this.txtBoardHeightMax.Location = new System.Drawing.Point(291, 67);
            this.txtBoardHeightMax.MaxLength = 4000;
            this.txtBoardHeightMax.Multiline = false;
            this.txtBoardHeightMax.Name = "txtBoardHeightMax";
            this.txtBoardHeightMax.PasswordChar = '\0';
            this.txtBoardHeightMax.ReadOnly = true;
            this.txtBoardHeightMax.ShowCheckBox = false;
            this.txtBoardHeightMax.Size = new System.Drawing.Size(164, 22);
            this.txtBoardHeightMax.TabIndex = 33;
            this.txtBoardHeightMax.TabNext = false;
            this.txtBoardHeightMax.Tag = "88";
            this.txtBoardHeightMax.Value = "";
            this.txtBoardHeightMax.WidthType = UserControl.WidthTypes.Normal;
            this.txtBoardHeightMax.XAlign = 322;
            // 
            // txtHeightMax
            // 
            this.txtHeightMax.AllowEditOnlyChecked = true;
            this.txtHeightMax.BackColor = System.Drawing.Color.Gainsboro;
            this.txtHeightMax.Caption = "MAX";
            this.txtHeightMax.Checked = false;
            this.txtHeightMax.EditType = UserControl.EditTypes.String;
            this.txtHeightMax.Location = new System.Drawing.Point(291, 97);
            this.txtHeightMax.MaxLength = 4000;
            this.txtHeightMax.Multiline = false;
            this.txtHeightMax.Name = "txtHeightMax";
            this.txtHeightMax.PasswordChar = '\0';
            this.txtHeightMax.ReadOnly = true;
            this.txtHeightMax.ShowCheckBox = false;
            this.txtHeightMax.Size = new System.Drawing.Size(164, 22);
            this.txtHeightMax.TabIndex = 32;
            this.txtHeightMax.TabNext = false;
            this.txtHeightMax.Tag = "88";
            this.txtHeightMax.Value = "";
            this.txtHeightMax.WidthType = UserControl.WidthTypes.Normal;
            this.txtHeightMax.XAlign = 322;
            // 
            // txtLeft2RightMax
            // 
            this.txtLeft2RightMax.AllowEditOnlyChecked = true;
            this.txtLeft2RightMax.BackColor = System.Drawing.Color.Gainsboro;
            this.txtLeft2RightMax.Caption = "MAX";
            this.txtLeft2RightMax.Checked = false;
            this.txtLeft2RightMax.EditType = UserControl.EditTypes.String;
            this.txtLeft2RightMax.Location = new System.Drawing.Point(291, 156);
            this.txtLeft2RightMax.MaxLength = 4000;
            this.txtLeft2RightMax.Multiline = false;
            this.txtLeft2RightMax.Name = "txtLeft2RightMax";
            this.txtLeft2RightMax.PasswordChar = '\0';
            this.txtLeft2RightMax.ReadOnly = true;
            this.txtLeft2RightMax.ShowCheckBox = false;
            this.txtLeft2RightMax.Size = new System.Drawing.Size(164, 22);
            this.txtLeft2RightMax.TabIndex = 31;
            this.txtLeft2RightMax.TabNext = false;
            this.txtLeft2RightMax.Tag = "88";
            this.txtLeft2RightMax.Value = "";
            this.txtLeft2RightMax.WidthType = UserControl.WidthTypes.Normal;
            this.txtLeft2RightMax.XAlign = 322;
            // 
            // txtLeft2MiddleMax
            // 
            this.txtLeft2MiddleMax.AllowEditOnlyChecked = true;
            this.txtLeft2MiddleMax.BackColor = System.Drawing.Color.Gainsboro;
            this.txtLeft2MiddleMax.Caption = "MAX";
            this.txtLeft2MiddleMax.Checked = false;
            this.txtLeft2MiddleMax.EditType = UserControl.EditTypes.String;
            this.txtLeft2MiddleMax.Location = new System.Drawing.Point(291, 187);
            this.txtLeft2MiddleMax.MaxLength = 4000;
            this.txtLeft2MiddleMax.Multiline = false;
            this.txtLeft2MiddleMax.Name = "txtLeft2MiddleMax";
            this.txtLeft2MiddleMax.PasswordChar = '\0';
            this.txtLeft2MiddleMax.ReadOnly = true;
            this.txtLeft2MiddleMax.ShowCheckBox = false;
            this.txtLeft2MiddleMax.Size = new System.Drawing.Size(164, 22);
            this.txtLeft2MiddleMax.TabIndex = 30;
            this.txtLeft2MiddleMax.TabNext = false;
            this.txtLeft2MiddleMax.Tag = "88";
            this.txtLeft2MiddleMax.Value = "";
            this.txtLeft2MiddleMax.WidthType = UserControl.WidthTypes.Normal;
            this.txtLeft2MiddleMax.XAlign = 322;
            // 
            // txtRight2MiddleMax
            // 
            this.txtRight2MiddleMax.AllowEditOnlyChecked = true;
            this.txtRight2MiddleMax.BackColor = System.Drawing.Color.Gainsboro;
            this.txtRight2MiddleMax.Caption = "MAX";
            this.txtRight2MiddleMax.Checked = false;
            this.txtRight2MiddleMax.EditType = UserControl.EditTypes.String;
            this.txtRight2MiddleMax.Location = new System.Drawing.Point(291, 215);
            this.txtRight2MiddleMax.MaxLength = 4000;
            this.txtRight2MiddleMax.Multiline = false;
            this.txtRight2MiddleMax.Name = "txtRight2MiddleMax";
            this.txtRight2MiddleMax.PasswordChar = '\0';
            this.txtRight2MiddleMax.ReadOnly = true;
            this.txtRight2MiddleMax.ShowCheckBox = false;
            this.txtRight2MiddleMax.Size = new System.Drawing.Size(164, 23);
            this.txtRight2MiddleMax.TabIndex = 29;
            this.txtRight2MiddleMax.TabNext = false;
            this.txtRight2MiddleMax.Tag = "88";
            this.txtRight2MiddleMax.Value = "";
            this.txtRight2MiddleMax.WidthType = UserControl.WidthTypes.Normal;
            this.txtRight2MiddleMax.XAlign = 322;
            // 
            // txtAllHeightMax
            // 
            this.txtAllHeightMax.AllowEditOnlyChecked = true;
            this.txtAllHeightMax.BackColor = System.Drawing.Color.Gainsboro;
            this.txtAllHeightMax.Caption = "MAX";
            this.txtAllHeightMax.Checked = false;
            this.txtAllHeightMax.EditType = UserControl.EditTypes.String;
            this.txtAllHeightMax.Location = new System.Drawing.Point(291, 126);
            this.txtAllHeightMax.MaxLength = 4000;
            this.txtAllHeightMax.Multiline = false;
            this.txtAllHeightMax.Name = "txtAllHeightMax";
            this.txtAllHeightMax.PasswordChar = '\0';
            this.txtAllHeightMax.ReadOnly = true;
            this.txtAllHeightMax.ShowCheckBox = false;
            this.txtAllHeightMax.Size = new System.Drawing.Size(164, 23);
            this.txtAllHeightMax.TabIndex = 28;
            this.txtAllHeightMax.TabNext = false;
            this.txtAllHeightMax.Tag = "88";
            this.txtAllHeightMax.Value = "";
            this.txtAllHeightMax.WidthType = UserControl.WidthTypes.Normal;
            this.txtAllHeightMax.XAlign = 322;
            // 
            // txtWidthMin
            // 
            this.txtWidthMin.AllowEditOnlyChecked = true;
            this.txtWidthMin.BackColor = System.Drawing.Color.Gainsboro;
            this.txtWidthMin.Caption = "MIN";
            this.txtWidthMin.Checked = false;
            this.txtWidthMin.EditType = UserControl.EditTypes.String;
            this.txtWidthMin.Location = new System.Drawing.Point(107, 36);
            this.txtWidthMin.MaxLength = 4000;
            this.txtWidthMin.Multiline = false;
            this.txtWidthMin.Name = "txtWidthMin";
            this.txtWidthMin.PasswordChar = '\0';
            this.txtWidthMin.ReadOnly = true;
            this.txtWidthMin.ShowCheckBox = false;
            this.txtWidthMin.Size = new System.Drawing.Size(164, 22);
            this.txtWidthMin.TabIndex = 27;
            this.txtWidthMin.TabNext = false;
            this.txtWidthMin.Tag = "88";
            this.txtWidthMin.Value = "";
            this.txtWidthMin.WidthType = UserControl.WidthTypes.Normal;
            this.txtWidthMin.XAlign = 138;
            // 
            // txtLeft2RightMin
            // 
            this.txtLeft2RightMin.AllowEditOnlyChecked = true;
            this.txtLeft2RightMin.BackColor = System.Drawing.Color.Gainsboro;
            this.txtLeft2RightMin.Caption = "MIN";
            this.txtLeft2RightMin.Checked = false;
            this.txtLeft2RightMin.EditType = UserControl.EditTypes.String;
            this.txtLeft2RightMin.Location = new System.Drawing.Point(107, 156);
            this.txtLeft2RightMin.MaxLength = 4000;
            this.txtLeft2RightMin.Multiline = false;
            this.txtLeft2RightMin.Name = "txtLeft2RightMin";
            this.txtLeft2RightMin.PasswordChar = '\0';
            this.txtLeft2RightMin.ReadOnly = true;
            this.txtLeft2RightMin.ShowCheckBox = false;
            this.txtLeft2RightMin.Size = new System.Drawing.Size(164, 22);
            this.txtLeft2RightMin.TabIndex = 26;
            this.txtLeft2RightMin.TabNext = false;
            this.txtLeft2RightMin.Tag = "88";
            this.txtLeft2RightMin.Value = "";
            this.txtLeft2RightMin.WidthType = UserControl.WidthTypes.Normal;
            this.txtLeft2RightMin.XAlign = 138;
            // 
            // txtBoardHeightMin
            // 
            this.txtBoardHeightMin.AllowEditOnlyChecked = true;
            this.txtBoardHeightMin.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBoardHeightMin.Caption = "MIN";
            this.txtBoardHeightMin.Checked = false;
            this.txtBoardHeightMin.EditType = UserControl.EditTypes.String;
            this.txtBoardHeightMin.Location = new System.Drawing.Point(107, 67);
            this.txtBoardHeightMin.MaxLength = 4000;
            this.txtBoardHeightMin.Multiline = false;
            this.txtBoardHeightMin.Name = "txtBoardHeightMin";
            this.txtBoardHeightMin.PasswordChar = '\0';
            this.txtBoardHeightMin.ReadOnly = true;
            this.txtBoardHeightMin.ShowCheckBox = false;
            this.txtBoardHeightMin.Size = new System.Drawing.Size(164, 22);
            this.txtBoardHeightMin.TabIndex = 25;
            this.txtBoardHeightMin.TabNext = false;
            this.txtBoardHeightMin.Tag = "88";
            this.txtBoardHeightMin.Value = "";
            this.txtBoardHeightMin.WidthType = UserControl.WidthTypes.Normal;
            this.txtBoardHeightMin.XAlign = 138;
            // 
            // txtHeightMin
            // 
            this.txtHeightMin.AllowEditOnlyChecked = true;
            this.txtHeightMin.BackColor = System.Drawing.Color.Gainsboro;
            this.txtHeightMin.Caption = "MIN";
            this.txtHeightMin.Checked = false;
            this.txtHeightMin.EditType = UserControl.EditTypes.String;
            this.txtHeightMin.Location = new System.Drawing.Point(107, 97);
            this.txtHeightMin.MaxLength = 4000;
            this.txtHeightMin.Multiline = false;
            this.txtHeightMin.Name = "txtHeightMin";
            this.txtHeightMin.PasswordChar = '\0';
            this.txtHeightMin.ReadOnly = true;
            this.txtHeightMin.ShowCheckBox = false;
            this.txtHeightMin.Size = new System.Drawing.Size(164, 22);
            this.txtHeightMin.TabIndex = 24;
            this.txtHeightMin.TabNext = false;
            this.txtHeightMin.Tag = "88";
            this.txtHeightMin.Value = "";
            this.txtHeightMin.WidthType = UserControl.WidthTypes.Normal;
            this.txtHeightMin.XAlign = 138;
            // 
            // txtAllHeightMin
            // 
            this.txtAllHeightMin.AllowEditOnlyChecked = true;
            this.txtAllHeightMin.BackColor = System.Drawing.Color.Gainsboro;
            this.txtAllHeightMin.Caption = "MIN";
            this.txtAllHeightMin.Checked = false;
            this.txtAllHeightMin.EditType = UserControl.EditTypes.String;
            this.txtAllHeightMin.Location = new System.Drawing.Point(107, 126);
            this.txtAllHeightMin.MaxLength = 4000;
            this.txtAllHeightMin.Multiline = false;
            this.txtAllHeightMin.Name = "txtAllHeightMin";
            this.txtAllHeightMin.PasswordChar = '\0';
            this.txtAllHeightMin.ReadOnly = true;
            this.txtAllHeightMin.ShowCheckBox = false;
            this.txtAllHeightMin.Size = new System.Drawing.Size(164, 23);
            this.txtAllHeightMin.TabIndex = 23;
            this.txtAllHeightMin.TabNext = false;
            this.txtAllHeightMin.Tag = "88";
            this.txtAllHeightMin.Value = "";
            this.txtAllHeightMin.WidthType = UserControl.WidthTypes.Normal;
            this.txtAllHeightMin.XAlign = 138;
            // 
            // txtLeft2MiddleMin
            // 
            this.txtLeft2MiddleMin.AllowEditOnlyChecked = true;
            this.txtLeft2MiddleMin.BackColor = System.Drawing.Color.Gainsboro;
            this.txtLeft2MiddleMin.Caption = "MIN";
            this.txtLeft2MiddleMin.Checked = false;
            this.txtLeft2MiddleMin.EditType = UserControl.EditTypes.String;
            this.txtLeft2MiddleMin.Location = new System.Drawing.Point(107, 187);
            this.txtLeft2MiddleMin.MaxLength = 4000;
            this.txtLeft2MiddleMin.Multiline = false;
            this.txtLeft2MiddleMin.Name = "txtLeft2MiddleMin";
            this.txtLeft2MiddleMin.PasswordChar = '\0';
            this.txtLeft2MiddleMin.ReadOnly = true;
            this.txtLeft2MiddleMin.ShowCheckBox = false;
            this.txtLeft2MiddleMin.Size = new System.Drawing.Size(164, 22);
            this.txtLeft2MiddleMin.TabIndex = 22;
            this.txtLeft2MiddleMin.TabNext = false;
            this.txtLeft2MiddleMin.Tag = "88";
            this.txtLeft2MiddleMin.Value = "";
            this.txtLeft2MiddleMin.WidthType = UserControl.WidthTypes.Normal;
            this.txtLeft2MiddleMin.XAlign = 138;
            // 
            // txtRight2MiddleMin
            // 
            this.txtRight2MiddleMin.AllowEditOnlyChecked = true;
            this.txtRight2MiddleMin.BackColor = System.Drawing.Color.Gainsboro;
            this.txtRight2MiddleMin.Caption = "MIN";
            this.txtRight2MiddleMin.Checked = false;
            this.txtRight2MiddleMin.EditType = UserControl.EditTypes.String;
            this.txtRight2MiddleMin.Location = new System.Drawing.Point(107, 215);
            this.txtRight2MiddleMin.MaxLength = 4000;
            this.txtRight2MiddleMin.Multiline = false;
            this.txtRight2MiddleMin.Name = "txtRight2MiddleMin";
            this.txtRight2MiddleMin.PasswordChar = '\0';
            this.txtRight2MiddleMin.ReadOnly = true;
            this.txtRight2MiddleMin.ShowCheckBox = false;
            this.txtRight2MiddleMin.Size = new System.Drawing.Size(164, 23);
            this.txtRight2MiddleMin.TabIndex = 21;
            this.txtRight2MiddleMin.TabNext = false;
            this.txtRight2MiddleMin.Tag = "88";
            this.txtRight2MiddleMin.Value = "";
            this.txtRight2MiddleMin.WidthType = UserControl.WidthTypes.Normal;
            this.txtRight2MiddleMin.XAlign = 138;
            // 
            // txtLengthMin
            // 
            this.txtLengthMin.AllowEditOnlyChecked = true;
            this.txtLengthMin.BackColor = System.Drawing.Color.Gainsboro;
            this.txtLengthMin.Caption = "MIN";
            this.txtLengthMin.Checked = false;
            this.txtLengthMin.EditType = UserControl.EditTypes.String;
            this.txtLengthMin.Location = new System.Drawing.Point(107, 7);
            this.txtLengthMin.MaxLength = 4000;
            this.txtLengthMin.Multiline = false;
            this.txtLengthMin.Name = "txtLengthMin";
            this.txtLengthMin.PasswordChar = '\0';
            this.txtLengthMin.ReadOnly = true;
            this.txtLengthMin.ShowCheckBox = false;
            this.txtLengthMin.Size = new System.Drawing.Size(164, 23);
            this.txtLengthMin.TabIndex = 20;
            this.txtLengthMin.TabNext = false;
            this.txtLengthMin.Tag = "88";
            this.txtLengthMin.Value = "";
            this.txtLengthMin.WidthType = UserControl.WidthTypes.Normal;
            this.txtLengthMin.XAlign = 138;
            // 
            // ucLabelEdit12
            // 
            this.ucLabelEdit12.AllowEditOnlyChecked = true;
            this.ucLabelEdit12.BackColor = System.Drawing.Color.Gainsboro;
            this.ucLabelEdit12.Caption = "MAX";
            this.ucLabelEdit12.Checked = false;
            this.ucLabelEdit12.EditType = UserControl.EditTypes.String;
            this.ucLabelEdit12.Location = new System.Drawing.Point(362, 45);
            this.ucLabelEdit12.MaxLength = 4000;
            this.ucLabelEdit12.Multiline = false;
            this.ucLabelEdit12.Name = "ucLabelEdit12";
            this.ucLabelEdit12.PasswordChar = '\0';
            this.ucLabelEdit12.ReadOnly = true;
            this.ucLabelEdit12.ShowCheckBox = false;
            this.ucLabelEdit12.Size = new System.Drawing.Size(164, 22);
            this.ucLabelEdit12.TabIndex = 28;
            this.ucLabelEdit12.TabNext = false;
            this.ucLabelEdit12.Value = "";
            this.ucLabelEdit12.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEdit12.XAlign = 393;
            // 
            // ucLabelEdit11
            // 
            this.ucLabelEdit11.AllowEditOnlyChecked = true;
            this.ucLabelEdit11.BackColor = System.Drawing.Color.Gainsboro;
            this.ucLabelEdit11.Caption = "MIN";
            this.ucLabelEdit11.Checked = false;
            this.ucLabelEdit11.EditType = UserControl.EditTypes.String;
            this.ucLabelEdit11.Location = new System.Drawing.Point(142, 253);
            this.ucLabelEdit11.MaxLength = 4000;
            this.ucLabelEdit11.Multiline = false;
            this.ucLabelEdit11.Name = "ucLabelEdit11";
            this.ucLabelEdit11.PasswordChar = '\0';
            this.ucLabelEdit11.ReadOnly = true;
            this.ucLabelEdit11.ShowCheckBox = false;
            this.ucLabelEdit11.Size = new System.Drawing.Size(164, 22);
            this.ucLabelEdit11.TabIndex = 27;
            this.ucLabelEdit11.TabNext = false;
            this.ucLabelEdit11.Value = "";
            this.ucLabelEdit11.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEdit11.XAlign = 173;
            // 
            // ucLabelEdit10
            // 
            this.ucLabelEdit10.AllowEditOnlyChecked = true;
            this.ucLabelEdit10.BackColor = System.Drawing.Color.Gainsboro;
            this.ucLabelEdit10.Caption = "MIN";
            this.ucLabelEdit10.Checked = false;
            this.ucLabelEdit10.EditType = UserControl.EditTypes.String;
            this.ucLabelEdit10.Location = new System.Drawing.Point(135, 245);
            this.ucLabelEdit10.MaxLength = 4000;
            this.ucLabelEdit10.Multiline = false;
            this.ucLabelEdit10.Name = "ucLabelEdit10";
            this.ucLabelEdit10.PasswordChar = '\0';
            this.ucLabelEdit10.ReadOnly = true;
            this.ucLabelEdit10.ShowCheckBox = false;
            this.ucLabelEdit10.Size = new System.Drawing.Size(164, 22);
            this.ucLabelEdit10.TabIndex = 26;
            this.ucLabelEdit10.TabNext = false;
            this.ucLabelEdit10.Value = "";
            this.ucLabelEdit10.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEdit10.XAlign = 166;
            // 
            // ucLabelEdit9
            // 
            this.ucLabelEdit9.AllowEditOnlyChecked = true;
            this.ucLabelEdit9.BackColor = System.Drawing.Color.Gainsboro;
            this.ucLabelEdit9.Caption = "MIN";
            this.ucLabelEdit9.Checked = false;
            this.ucLabelEdit9.EditType = UserControl.EditTypes.String;
            this.ucLabelEdit9.Location = new System.Drawing.Point(128, 238);
            this.ucLabelEdit9.MaxLength = 4000;
            this.ucLabelEdit9.Multiline = false;
            this.ucLabelEdit9.Name = "ucLabelEdit9";
            this.ucLabelEdit9.PasswordChar = '\0';
            this.ucLabelEdit9.ReadOnly = true;
            this.ucLabelEdit9.ShowCheckBox = false;
            this.ucLabelEdit9.Size = new System.Drawing.Size(164, 22);
            this.ucLabelEdit9.TabIndex = 25;
            this.ucLabelEdit9.TabNext = false;
            this.ucLabelEdit9.Value = "";
            this.ucLabelEdit9.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEdit9.XAlign = 159;
            // 
            // ucLabelEdit8
            // 
            this.ucLabelEdit8.AllowEditOnlyChecked = true;
            this.ucLabelEdit8.BackColor = System.Drawing.Color.Gainsboro;
            this.ucLabelEdit8.Caption = "MIN";
            this.ucLabelEdit8.Checked = false;
            this.ucLabelEdit8.EditType = UserControl.EditTypes.String;
            this.ucLabelEdit8.Location = new System.Drawing.Point(122, 230);
            this.ucLabelEdit8.MaxLength = 4000;
            this.ucLabelEdit8.Multiline = false;
            this.ucLabelEdit8.Name = "ucLabelEdit8";
            this.ucLabelEdit8.PasswordChar = '\0';
            this.ucLabelEdit8.ReadOnly = true;
            this.ucLabelEdit8.ShowCheckBox = false;
            this.ucLabelEdit8.Size = new System.Drawing.Size(164, 23);
            this.ucLabelEdit8.TabIndex = 24;
            this.ucLabelEdit8.TabNext = false;
            this.ucLabelEdit8.Value = "";
            this.ucLabelEdit8.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEdit8.XAlign = 153;
            // 
            // ucLabelEdit7
            // 
            this.ucLabelEdit7.AllowEditOnlyChecked = true;
            this.ucLabelEdit7.BackColor = System.Drawing.Color.Gainsboro;
            this.ucLabelEdit7.Caption = "MIN";
            this.ucLabelEdit7.Checked = false;
            this.ucLabelEdit7.EditType = UserControl.EditTypes.String;
            this.ucLabelEdit7.Location = new System.Drawing.Point(115, 223);
            this.ucLabelEdit7.MaxLength = 4000;
            this.ucLabelEdit7.Multiline = false;
            this.ucLabelEdit7.Name = "ucLabelEdit7";
            this.ucLabelEdit7.PasswordChar = '\0';
            this.ucLabelEdit7.ReadOnly = true;
            this.ucLabelEdit7.ShowCheckBox = false;
            this.ucLabelEdit7.Size = new System.Drawing.Size(164, 22);
            this.ucLabelEdit7.TabIndex = 23;
            this.ucLabelEdit7.TabNext = false;
            this.ucLabelEdit7.Value = "";
            this.ucLabelEdit7.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEdit7.XAlign = 146;
            // 
            // ucLabelEdit6
            // 
            this.ucLabelEdit6.AllowEditOnlyChecked = true;
            this.ucLabelEdit6.BackColor = System.Drawing.Color.Gainsboro;
            this.ucLabelEdit6.Caption = "MIN";
            this.ucLabelEdit6.Checked = false;
            this.ucLabelEdit6.EditType = UserControl.EditTypes.String;
            this.ucLabelEdit6.Location = new System.Drawing.Point(108, 215);
            this.ucLabelEdit6.MaxLength = 4000;
            this.ucLabelEdit6.Multiline = false;
            this.ucLabelEdit6.Name = "ucLabelEdit6";
            this.ucLabelEdit6.PasswordChar = '\0';
            this.ucLabelEdit6.ReadOnly = true;
            this.ucLabelEdit6.ShowCheckBox = false;
            this.ucLabelEdit6.Size = new System.Drawing.Size(164, 23);
            this.ucLabelEdit6.TabIndex = 22;
            this.ucLabelEdit6.TabNext = false;
            this.ucLabelEdit6.Value = "";
            this.ucLabelEdit6.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEdit6.XAlign = 139;
            // 
            // ucLabelEdit5
            // 
            this.ucLabelEdit5.AllowEditOnlyChecked = true;
            this.ucLabelEdit5.BackColor = System.Drawing.Color.Gainsboro;
            this.ucLabelEdit5.Caption = "MIN";
            this.ucLabelEdit5.Checked = false;
            this.ucLabelEdit5.EditType = UserControl.EditTypes.String;
            this.ucLabelEdit5.Location = new System.Drawing.Point(102, 208);
            this.ucLabelEdit5.MaxLength = 4000;
            this.ucLabelEdit5.Multiline = false;
            this.ucLabelEdit5.Name = "ucLabelEdit5";
            this.ucLabelEdit5.PasswordChar = '\0';
            this.ucLabelEdit5.ReadOnly = true;
            this.ucLabelEdit5.ShowCheckBox = false;
            this.ucLabelEdit5.Size = new System.Drawing.Size(164, 22);
            this.ucLabelEdit5.TabIndex = 21;
            this.ucLabelEdit5.TabNext = false;
            this.ucLabelEdit5.Value = "";
            this.ucLabelEdit5.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEdit5.XAlign = 133;
            // 
            // ucLabelEdit20
            // 
            this.ucLabelEdit20.AllowEditOnlyChecked = true;
            this.ucLabelEdit20.BackColor = System.Drawing.Color.Gainsboro;
            this.ucLabelEdit20.Caption = "MIN";
            this.ucLabelEdit20.Checked = false;
            this.ucLabelEdit20.EditType = UserControl.EditTypes.String;
            this.ucLabelEdit20.Location = new System.Drawing.Point(61, 104);
            this.ucLabelEdit20.MaxLength = 4000;
            this.ucLabelEdit20.Multiline = false;
            this.ucLabelEdit20.Name = "ucLabelEdit20";
            this.ucLabelEdit20.PasswordChar = '\0';
            this.ucLabelEdit20.ReadOnly = true;
            this.ucLabelEdit20.ShowCheckBox = false;
            this.ucLabelEdit20.Size = new System.Drawing.Size(164, 22);
            this.ucLabelEdit20.TabIndex = 20;
            this.ucLabelEdit20.TabNext = false;
            this.ucLabelEdit20.Value = "";
            this.ucLabelEdit20.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEdit20.XAlign = 92;
            // 
            // FOQCDimention
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(894, 562);
            this.Controls.Add(this.pnlMiddle);
            this.Controls.Add(this.palTop);
            this.Controls.Add(this.panel3);
            this.KeyPreview = true;
            this.Name = "FOQCDimention";
            this.Text = "尺寸数据采集";
            this.Load += new System.EventHandler(this.FOQCDimention_Load);
            this.Closed += new System.EventHandler(this.FOQCDimention_Closed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FOQCDimention_KeyUp);
            this.panel3.ResumeLayout(false);
            this.palTop.ResumeLayout(false);
            this.pnlMiddle.ResumeLayout(false);
            this.pnlMiddle2.ResumeLayout(false);
            this.grbResult.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region 系统服务部分
		protected void SucessMessage(string msg)
		{
			_infoForm.Add(new UserControl.Message(UserControl.MessageType.Success,msg));
		}

		protected void ErrorMessage(string msg)
		{			
			_infoForm.Add(new UserControl.Message(UserControl.MessageType.Error,msg));
			//BenQGuru.eMES.Web.Helper.SoundPlayer.PlayErrorMusic();
		}

		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}
		private void FOQCDimention_Closed(object sender, System.EventArgs e)
		{
			if (this.DataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();  
		}
		#endregion

		private System.Collections.Hashtable _htEdits = new Hashtable();
		private void FOQCDimention_Load(object sender, System.EventArgs e)
		{
			_infoForm = ApplicationRun.GetInfoForm();
			UserControl.UIStyleBuilder.FormUI(this);
			this.txtInput.TextFocus(false, true);

			foreach(Control con in pnlMiddle2.Controls)
			{
				if(Convert.ToInt32(con.Tag) == 88)
				{
					_htEdits.Add(con.Name,con);
				}
			}
		}
		

		private ProductInfo product = null;
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

		private OQCLot2Card GetLastLotCard(object[] objLotCard)
		{
			if(objLotCard.Length <= 0)
				return null;

			OQCLot2Card a = objLotCard[0] as OQCLot2Card;

			for(int i = 1 ; i < objLotCard.Length ; i++)
			{
				OQCLot2Card b = objLotCard[i] as OQCLot2Card;

				if((a.MaintainDate*1000000.0 + a.MaintainTime) < (b.MaintainDate * 1000000.0 + b.MaintainTime ))
					a = b;
			}

			return a;
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
						ActionType.DataCollectAction_OQCPass,  
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
						lotCard = GetLastLotCard(objLotCard);//(OQCLot2Card)objLotCard[0];
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
					if (messages.IsSuccess())
					{
						this.txtLotNo.Value = oqcLot.LOTNO;
						this.txtItemCode.Value = product.LastSimulation.ItemCode;
						this.txtMoCode.Value = product.LastSimulation.MOCode;
					}
				}
			}
			catch (Exception e)
			{
				messages.Add(new UserControl.Message(e));
			}
			return messages;
		}

		//显示测试信息
		private void DisplayTestInfo()
		{
			//LOAD 产品中设定的尺寸信息
			LoadItemDimention();

			//load 之前做的测试信息
			LoadTestDimention();

			this.CheckResult();
		}
		private void LoadItemDimention()
		{
			BenQGuru.eMES.MOModel.ItemFacade facade = new ItemFacade(this.DataProvider);
			object[] objs = facade.QueryItem2Dimention(this.txtItemCode.Value);
			if(objs != null)
			{
				foreach(BenQGuru.eMES.Domain.MOModel.Item2Dimention dim in objs)
				{
					if(dim != null)
					{
						UserControl.UCLabelEdit edit = _htEdits["txt"+dim.ParamName] as UserControl.UCLabelEdit;
						if(edit!= null)
							edit.Value = dim.ParamValue.ToString();
					}
				}
			}
		}

		private void LoadTestDimention()
		{
			BenQGuru.eMES.OQC.OQCFacade facade = new OQCFacade(this.DataProvider);
			object[] objs = facade.QueryOQCDimentionValue(this.txtLotNo.Value,OQCFacade.Lot_Sequence_Default,this.product.LastSimulation.RunningCard,this.product.LastSimulation.RunningCardSequence,this.txtMoCode.Text,0,int.MaxValue);
			if(objs != null)
			{
				foreach(BenQGuru.eMES.Domain.OQC.OQCDimentionValue dim in objs)
				{
					if(dim != null)
					{
						UserControl.UCLabelEdit edit = _htEdits["txt"+dim.ParamName+"Value"] as UserControl.UCLabelEdit;
						if(edit != null)
							edit.Value = dim.ActualValue.ToString();
					}
				}
			}
		}

		private void txtInput_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
			if (e.KeyChar == '\r' && txtInput.Value.Trim() != string.Empty)
			{
				LoadRCard();
			}
		}

		private void CheckResult()
		{
			bool pass = true;
			bool hasone = false;
			foreach(string key in this._htEdits.Keys)
			{
				if(key.IndexOf("Value") >=0)
				{
					string param = key.Substring(3,key.Length - 3 - 5);
					UserControl.UCLabelEdit editValue = _htEdits[key] as UserControl.UCLabelEdit;
					UserControl.UCLabelEdit editMin = _htEdits["txt"+param+"Min"] as UserControl.UCLabelEdit;
					UserControl.UCLabelEdit editMax = _htEdits["txt"+param+"Max"] as UserControl.UCLabelEdit;
					if(editValue != null && editMin!= null && editMax != null 
					  && editValue.Value != string.Empty && editMin.Value != string.Empty && editMax.Value != string.Empty)
					{
						hasone = true;
						if(decimal.Parse(editValue.Value) < decimal.Parse(editMin.Value) 
							||
							decimal.Parse(editValue.Value) > decimal.Parse(editMax.Value))
						{
							pass = false;
							break;
						}
					}
				}
			}

			if(hasone)
			{
				if(pass)
				{
					this.lblResult.ForeColor = System.Drawing.Color.Blue;
					this.lblResult.Text = "Pass";
				}
				else
				{
					this.lblResult.ForeColor = System.Drawing.Color.Red;
					this.lblResult.Text = "Fail";
				}
			}
			else
			{
				this.lblResult.Text = string.Empty;
			}
		}

		private bool CheckInput()
		{
			foreach(string key in this._htEdits.Keys)
			{
				if(key.IndexOf("Value") >=0)
				{
					string param = key.Substring(3,key.Length - 3 - 5);
					UserControl.UCLabelEdit editValue = _htEdits[key] as UserControl.UCLabelEdit;
					UserControl.UCLabelEdit editMin = _htEdits["txt"+param+"Min"] as UserControl.UCLabelEdit;
					UserControl.UCLabelEdit editMax = _htEdits["txt"+param+"Max"] as UserControl.UCLabelEdit;
					if(editValue != null && editMin!= null && editMax != null 
						&& editValue.Value != string.Empty && editMin.Value != string.Empty && editMax.Value != string.Empty)
					{
						return true;
					}
				}
			}

			return false;
		}

		private void ClearControlValues()
		{
			foreach(string key in this._htEdits.Keys)
			{
				if(key.IndexOf("Value") >=0)
				{
					UserControl.UCLabelEdit editValue = _htEdits[key] as UserControl.UCLabelEdit;

					editValue.Value = "";
				}
			}

			lblResult.Text = "";
		}


		private void LoadRCard()
		{
			ClearControlValues();

			Messages msg = CheckProduct();
			if (msg.IsSuccess())
			{
				DisplayTestInfo();
					
				LockInput(true);
				this.txtLengthValue.TextFocus(false, true);
			}
			if (!msg.IsSuccess())
			{
				ApplicationRun.GetInfoForm().Add(msg);
                //Remove UCLabel.SelectAll;
				txtInput.TextFocus(false, true);
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
			LoadRCard();
		}

		private void txtLengthValue_Leave(object sender, System.EventArgs e)
		{
			this.CheckResult();
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			#region 检查用户输入

			//至少输入一个有效值
			if(!this.CheckInput())
				this.ErrorMessage("$OQC_Atleast_One_Dimention");
			
			#endregion

			try
			{
				this.DataProvider.BeginTransaction();
				#region 保存序列号部分
			
				OQCFacade facade = new OQCFacade(this.DataProvider);
				OQCDimention dim = facade.GetOQCDimention(this.product.LastSimulation.RunningCard,
														this.product.LastSimulation.RunningCardSequence,
														this.txtMoCode.Value,
														this.txtLotNo.Value,
														OQCFacade.Lot_Sequence_Default)
														as
														OQCDimention;

				DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
				bool bNeedDeleteExist = false;
				int iExistTestDate = 0;
				if(dim != null)
				{
					bNeedDeleteExist = true;
					iExistTestDate = dim.TestDate;
					dim.TestResult = lblResult.Text;
				
					dim.TestDate = dbDateTime.DBDate;
					dim.TestTime = dbDateTime.DBTime;
					dim.TestUser = ApplicationService.Current().UserCode;
					dim.MaintainUser = dim.TestUser;

					facade.UpdateOQCDimention(dim);
				}
				else
				{
					dim = new OQCDimention();
					dim.ItemCode = this.txtItemCode.Value;
					dim.LOTNO = this.txtLotNo.Value;
					dim.LotSequence = OQCFacade.Lot_Sequence_Default;
					dim.MOCode = this.txtMoCode.Value;
					dim.ModelCode = this.product.LastSimulation.ModelCode;
					dim.OPCode = this.product.LastSimulation.OPCode;
					dim.ResourceCode = this.product.LastSimulation.ResourceCode;
					dim.RouteCode = this.product.LastSimulation.RouteCode;
					dim.RunningCard = this.product.LastSimulation.RunningCard;
					dim.RunningCardSequence = this.product.LastSimulation.RunningCardSequence;

					if(ApplicationService.Current().LoginInfo != null && ApplicationService.Current().LoginInfo.Resource != null)
					{
						dim.SegmnetCode = ApplicationService.Current().LoginInfo.Resource.SegmentCode;//" ";
						dim.StepSequenceCode = ApplicationService.Current().LoginInfo.Resource.StepSequenceCode;//" ";
					}

					dim.TestResult = lblResult.Text;
					dim.TestDate = dbDateTime.DBDate;
					dim.TestTime = dbDateTime.DBTime;
					dim.TestUser = ApplicationService.Current().UserCode;
					dim.MaintainUser = dim.TestUser;

					facade.AddOQCDimention(dim);
				}

				#endregion

				#region 保存测试值部分
				// Added by Icyer 2006/08/14
				BenQGuru.eMES.SPCDataCenter.DataEntry dataEntry = new BenQGuru.eMES.SPCDataCenter.DataEntry();
				BenQGuru.eMES.Domain.DataCollect.Simulation sim = this.product.LastSimulation;
				dataEntry.ModelCode = sim.ModelCode;
				dataEntry.ItemCode = sim.ItemCode;
				dataEntry.MOCode = sim.MOCode;
				dataEntry.RunningCard = sim.RunningCard;
				dataEntry.RunningCardSequence = sim.RunningCardSequence;
				dataEntry.SegmentCode = ApplicationService.Current().LoginInfo.Resource.SegmentCode;
				dataEntry.LineCode = ApplicationService.Current().LoginInfo.Resource.StepSequenceCode;
				dataEntry.ResourceCode = ApplicationService.Current().LoginInfo.Resource.ResourceCode;
				dataEntry.OPCode = sim.OPCode;
				dataEntry.LotNo = sim.LOTNO;
				dataEntry.TestDate = dim.TestDate;
				dataEntry.TestTime = dim.TestTime;
				if (lblResult.Text.ToUpper() == "FAIL")
					dataEntry.TestResult = "F";
				else
					dataEntry.TestResult = "P";
				//dataEntry.TestResult = lblResult.Text;
				dataEntry.TestUser = ApplicationService.Current().UserCode;
				// Added end
				foreach(string key in this._htEdits.Keys)
				{
					if(key.IndexOf("Value") >=0)
					{
						string param = key.Substring(3,key.Length - 3 - 5);
						UserControl.UCLabelEdit editValue = _htEdits[key] as UserControl.UCLabelEdit;
						UserControl.UCLabelEdit editMin = _htEdits["txt"+param+"Min"] as UserControl.UCLabelEdit;
						UserControl.UCLabelEdit editMax = _htEdits["txt"+param+"Max"] as UserControl.UCLabelEdit;

						// Added by Icyer 2006/08/14
						if(editValue != null && editValue.Value != string.Empty)
						{
							string strObjCode = "OQC_DIM_" + param.ToUpper();
							dataEntry.AddTestData(strObjCode, decimal.Parse(editValue.Value));
						}
						// Added end
						/* Removed by Icyer 2006/08/14
						OQCDimentionValue dv = facade.GetOQCDimentionVALUE(this.txtLotNo.Value,OQCFacade.Lot_Sequence_Default,
																		this.product.LastSimulation.RunningCard,this.product.LastSimulation.RunningCardSequence,
																		this.txtMoCode.Value,param)
																		as
																		OQCDimentionValue;

						//如果用户输入不为空,则增加或者update
						if(editValue != null && editMin!= null && editMax != null 
							&& editValue.Value != string.Empty && editMin.Value != string.Empty && editMax.Value != string.Empty)
						{
							
							if(dv != null)
							{
								dv.MaxValue = decimal.Parse(editMax.Value);
								dv.MinValue = decimal.Parse(editMin.Value);
								dv.ActualValue = decimal.Parse(editValue.Value);
								dv.MaintainUser = ApplicationService.Current().UserCode;
								facade.UpdateOQCDimentionVALUE(dv);
							}
							else
							{
								dv = new OQCDimentionValue();
								dv.LOTNO = dim.LOTNO;
								dv.LotSequence = dim.LotSequence;
								dv.MOCode = dim.MOCode;
								dv.ParamName = param;
								dv.RunningCard = dim.RunningCard;
								dv.RunningCardSequence = dim.RunningCardSequence;
								
								dv.MaxValue = decimal.Parse(editMax.Value);
								dv.MinValue = decimal.Parse(editMin.Value);
								dv.ActualValue = decimal.Parse(editValue.Value);

								dv.MaintainUser = ApplicationService.Current().UserCode;

								facade.AddOQCDimentionVALUE(dv);
							}
						}
						else //如果用户输入为空,如果数据库中以前存在,则删除
						{
							if(dv != null)
								facade.DeleteOQCDimentionVALUE(dv);

						}
						*/
					}
				}
				// Added by Icyer 2006/08/14
				BenQGuru.eMES.SPCDataCenter.DataHandler handler = new BenQGuru.eMES.SPCDataCenter.DataHandler(this.DataProvider);
				if (bNeedDeleteExist == true && dataEntry.ListTestData.Count > 0)
				{
					BenQGuru.eMES.SPCDataCenter.DataEntryTestData testData0 = (BenQGuru.eMES.SPCDataCenter.DataEntryTestData)dataEntry.ListTestData[0];
					//handler.DeleteData(testData0.ObjectCode, dataEntry.ItemCode, dataEntry.MOCode, dataEntry.RunningCard, dataEntry.RunningCardSequence, iExistTestDate);
				}
				//handler.CollectData(dataEntry);
				// Added end
				#endregion

				this.SucessMessage("$CS_Add_Success");
				this.DataProvider.CommitTransaction();
			}
			catch(System.Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				this.ErrorMessage(ex.Message);
			}
		}

		private void FOQCDimention_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyData == Keys.F5)
			{
				this.btnSave_Click(null, null);
			}
		}

	}
}
