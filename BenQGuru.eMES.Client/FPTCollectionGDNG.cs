using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
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
using BenQGuru.eMES.DeviceInterface;
using BenQGuru.eMES.Domain.DeviceInterface;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FCollectionGDNG 的摘要说明。
	/// Laws Lu,2005/08/10,调整页面逻辑
	/// Laws Lu,2005/08/16,修改	Lucky的需求
	/// 对于我来说，只存在两种情况，已经归属过工单的序列号被再次归属工单（无论输入的工单号码是否是正确的）；
	/// 其二，应该归属工单的序列号没有成功归属工单（无论是什么原因）。
	/// 在第一种情况下可以继续，在逻辑上没有问题；在第二种情况下是无法继续的，（它连工单都没有）
	///	细节的逻辑你推敲一下吧，如果是我说的第一种情况，目前的逻辑是完全满足的；
	///	如果是第二种情况，你只需要保证，工单归属不成功，后续的逻辑全部停止，
	///	此时只需要告诉用户：该产品序列号没有归属工单
	/// </summary>
	public class FPTCollectionGDNG : System.Windows.Forms.Form
	{
		private Infragistics.Win.Misc.UltraLabel ultraLabel6;
		private System.Windows.Forms.Panel panelButton;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panel1;
		private UserControl.UCLabelEdit txtRunningCard;
		private System.ComponentModel.IContainer components;
		//private ActionOnLineHelper dataCollect = null;
		private UserControl.UCButton btnSave;
		private UserControl.UCButton btnExit;
		private UserControl.UCLabelEdit txtMO;
		private UserControl.UCLabelEdit txtItem;
		private UserControl.UCLabelCombox cbxOutLine;
		private System.Windows.Forms.RadioButton rdoGood;
		private System.Windows.Forms.RadioButton rdoNG;
		private UserControl.UCLabelEdit txtMem;
		private UserControl.UCLabelEdit txtGOMO;
		private ProductInfo product;
		private UserControl.UCLabelEdit edtSoftName;
		private UserControl.UCLabelEdit edtSoftInfo;
		private System.Windows.Forms.CheckBox checkBox1;
		private UserControl.UCLabelEdit CollectedCount;
		private UserControl.UCLabelEdit bRCardLetterULE;
		private UserControl.UCLabelEdit bRCardLenULE;
		private UserControl.UCLabelEdit lblNotYield;	
		//Laws Lu,2005/08/16,新增	保存处理信息
		private Messages globeMSG = new Messages();
		private System.Windows.Forms.Label label1;
		private UserControl.UCLabelEdit txtElectric;
		private UserControl.UCButton btnGetElectricCurrent;
		private UserControl.UCLabelEdit txtMaxValue;
		private UserControl.UCLabelEdit txtMinValue;
		private AxMSCommLib.AxMSComm axMSComm1;
		private UserControl.UCButton btnReset;
		private UserControl.UCButton ucButton1;
		private UserControl.UCButton ucButton2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private UserControl.UCErrorCodeSelect errorCodeSelect;
		private System.Windows.Forms.Label lblResult;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private double iNG = 0;
		public FPTCollectionGDNG()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			UserControl.UIStyleBuilder.FormUI(this);	
			product = new ProductInfo();

			txtMem.AutoChange();

			PrepareMSComm();

			ResetMSComm();

			ResetRadioButton();

			#if DEBUG
			this.ucButton1.Visible = true;
			this.ucButton2.Visible = true;
			this.label2.Visible = true;
			this.label3.Visible = true;
			#endif
		}

		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FPTCollectionGDNG));
			this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
			this.panelButton = new System.Windows.Forms.Panel();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.ucButton2 = new UserControl.UCButton();
			this.ucButton1 = new UserControl.UCButton();
			this.btnReset = new UserControl.UCButton();
			this.txtMinValue = new UserControl.UCLabelEdit();
			this.txtMaxValue = new UserControl.UCLabelEdit();
			this.btnGetElectricCurrent = new UserControl.UCButton();
			this.txtElectric = new UserControl.UCLabelEdit();
			this.label1 = new System.Windows.Forms.Label();
			this.lblNotYield = new UserControl.UCLabelEdit();
			this.txtMem = new UserControl.UCLabelEdit();
			this.txtRunningCard = new UserControl.UCLabelEdit();
			this.rdoNG = new System.Windows.Forms.RadioButton();
			this.rdoGood = new System.Windows.Forms.RadioButton();
			this.btnExit = new UserControl.UCButton();
			this.btnSave = new UserControl.UCButton();
			this.CollectedCount = new UserControl.UCLabelEdit();
			this.txtGOMO = new UserControl.UCLabelEdit();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.bRCardLetterULE = new UserControl.UCLabelEdit();
			this.bRCardLenULE = new UserControl.UCLabelEdit();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.edtSoftName = new UserControl.UCLabelEdit();
			this.edtSoftInfo = new UserControl.UCLabelEdit();
			this.cbxOutLine = new UserControl.UCLabelCombox();
			this.axMSComm1 = new AxMSCommLib.AxMSComm();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtMO = new UserControl.UCLabelEdit();
			this.txtItem = new UserControl.UCLabelEdit();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.errorCodeSelect = new UserControl.UCErrorCodeSelect();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lblResult = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.panelButton.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axMSComm1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// ultraLabel6
			// 
			this.ultraLabel6.Location = new System.Drawing.Point(0, 0);
			this.ultraLabel6.Name = "ultraLabel6";
			this.ultraLabel6.TabIndex = 0;
			// 
			// panelButton
			// 
			this.panelButton.Controls.Add(this.textBox1);
			this.panelButton.Controls.Add(this.ucButton2);
			this.panelButton.Controls.Add(this.ucButton1);
			this.panelButton.Controls.Add(this.btnReset);
			this.panelButton.Controls.Add(this.txtMinValue);
			this.panelButton.Controls.Add(this.txtMaxValue);
			this.panelButton.Controls.Add(this.btnGetElectricCurrent);
			this.panelButton.Controls.Add(this.txtElectric);
			this.panelButton.Controls.Add(this.label1);
			this.panelButton.Controls.Add(this.lblNotYield);
			this.panelButton.Controls.Add(this.txtMem);
			this.panelButton.Controls.Add(this.txtRunningCard);
			this.panelButton.Controls.Add(this.rdoNG);
			this.panelButton.Controls.Add(this.rdoGood);
			this.panelButton.Controls.Add(this.btnExit);
			this.panelButton.Controls.Add(this.btnSave);
			this.panelButton.Controls.Add(this.CollectedCount);
			this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelButton.Location = new System.Drawing.Point(0, 461);
			this.panelButton.Name = "panelButton";
			this.panelButton.Size = new System.Drawing.Size(936, 104);
			this.panelButton.TabIndex = 155;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(504, 8);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(0, 21);
			this.textBox1.TabIndex = 13;
			this.textBox1.Text = "";
			this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
			// 
			// ucButton2
			// 
			this.ucButton2.BackColor = System.Drawing.SystemColors.Control;
			this.ucButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton2.BackgroundImage")));
			this.ucButton2.ButtonType = UserControl.ButtonTypes.None;
			this.ucButton2.Caption = "电流值(Debug2)";
			this.ucButton2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucButton2.Location = new System.Drawing.Point(728, 72);
			this.ucButton2.Name = "ucButton2";
			this.ucButton2.Size = new System.Drawing.Size(88, 22);
			this.ucButton2.TabIndex = 8;
			this.ucButton2.Visible = false;
			this.ucButton2.Click += new System.EventHandler(this.ucButton2_Click);
			// 
			// ucButton1
			// 
			this.ucButton1.BackColor = System.Drawing.SystemColors.Control;
			this.ucButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton1.BackgroundImage")));
			this.ucButton1.ButtonType = UserControl.ButtonTypes.None;
			this.ucButton1.Caption = "电流值(Debug1)";
			this.ucButton1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucButton1.Location = new System.Drawing.Point(632, 72);
			this.ucButton1.Name = "ucButton1";
			this.ucButton1.Size = new System.Drawing.Size(88, 22);
			this.ucButton1.TabIndex = 7;
			this.ucButton1.Visible = false;
			this.ucButton1.Click += new System.EventHandler(this.ucButton1_Click);
			// 
			// btnReset
			// 
			this.btnReset.BackColor = System.Drawing.SystemColors.Control;
			this.btnReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReset.BackgroundImage")));
			this.btnReset.ButtonType = UserControl.ButtonTypes.None;
			this.btnReset.Caption = "复位数据连线";
			this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnReset.Location = new System.Drawing.Point(320, 72);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(88, 22);
			this.btnReset.TabIndex = 4;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// txtMinValue
			// 
			this.txtMinValue.AllowEditOnlyChecked = true;
			this.txtMinValue.Caption = "标准最小电流值";
			this.txtMinValue.Checked = false;
			this.txtMinValue.EditType = UserControl.EditTypes.String;
			this.txtMinValue.Location = new System.Drawing.Point(408, 40);
			this.txtMinValue.MaxLength = 40;
			this.txtMinValue.Multiline = false;
			this.txtMinValue.Name = "txtMinValue";
			this.txtMinValue.PasswordChar = '\0';
			this.txtMinValue.ReadOnly = true;
			this.txtMinValue.ShowCheckBox = false;
			this.txtMinValue.Size = new System.Drawing.Size(199, 24);
			this.txtMinValue.TabIndex = 11;
			this.txtMinValue.TabNext = true;
			this.txtMinValue.Value = "";
			this.txtMinValue.WidthType = UserControl.WidthTypes.Small;
			this.txtMinValue.XAlign = 507;
			// 
			// txtMaxValue
			// 
			this.txtMaxValue.AllowEditOnlyChecked = true;
			this.txtMaxValue.Caption = "标准最大电流值";
			this.txtMaxValue.Checked = false;
			this.txtMaxValue.EditType = UserControl.EditTypes.String;
			this.txtMaxValue.Location = new System.Drawing.Point(200, 40);
			this.txtMaxValue.MaxLength = 40;
			this.txtMaxValue.Multiline = false;
			this.txtMaxValue.Name = "txtMaxValue";
			this.txtMaxValue.PasswordChar = '\0';
			this.txtMaxValue.ReadOnly = true;
			this.txtMaxValue.ShowCheckBox = false;
			this.txtMaxValue.Size = new System.Drawing.Size(199, 24);
			this.txtMaxValue.TabIndex = 10;
			this.txtMaxValue.TabNext = true;
			this.txtMaxValue.Value = "";
			this.txtMaxValue.WidthType = UserControl.WidthTypes.Small;
			this.txtMaxValue.XAlign = 299;
			// 
			// btnGetElectricCurrent
			// 
			this.btnGetElectricCurrent.BackColor = System.Drawing.SystemColors.Control;
			this.btnGetElectricCurrent.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGetElectricCurrent.BackgroundImage")));
			this.btnGetElectricCurrent.ButtonType = UserControl.ButtonTypes.None;
			this.btnGetElectricCurrent.Caption = "获取电流值";
			this.btnGetElectricCurrent.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnGetElectricCurrent.Enabled = false;
			this.btnGetElectricCurrent.Location = new System.Drawing.Point(216, 72);
			this.btnGetElectricCurrent.Name = "btnGetElectricCurrent";
			this.btnGetElectricCurrent.Size = new System.Drawing.Size(88, 22);
			this.btnGetElectricCurrent.TabIndex = 2;
			this.btnGetElectricCurrent.Click += new System.EventHandler(this.btnGetElectricCurrent_Click);
			// 
			// txtElectric
			// 
			this.txtElectric.AllowEditOnlyChecked = true;
			this.txtElectric.Caption = "电流值";
			this.txtElectric.Checked = false;
			this.txtElectric.EditType = UserControl.EditTypes.String;
			this.txtElectric.Location = new System.Drawing.Point(304, 8);
			this.txtElectric.MaxLength = 40;
			this.txtElectric.Multiline = false;
			this.txtElectric.Name = "txtElectric";
			this.txtElectric.PasswordChar = '\0';
			this.txtElectric.ReadOnly = true;
			this.txtElectric.ShowCheckBox = false;
			this.txtElectric.Size = new System.Drawing.Size(183, 24);
			this.txtElectric.TabIndex = 1;
			this.txtElectric.TabNext = false;
			this.txtElectric.Value = "";
			this.txtElectric.WidthType = UserControl.WidthTypes.Normal;
			this.txtElectric.XAlign = 354;
			this.txtElectric.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtElectric_TxtboxKeyPress);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(192, 72);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(11, 17);
			this.label1.TabIndex = 8;
			this.label1.Text = "%";
			// 
			// lblNotYield
			// 
			this.lblNotYield.AllowEditOnlyChecked = true;
			this.lblNotYield.Caption = "不良率";
			this.lblNotYield.Checked = false;
			this.lblNotYield.EditType = UserControl.EditTypes.Number;
			this.lblNotYield.Location = new System.Drawing.Point(40, 72);
			this.lblNotYield.MaxLength = 40;
			this.lblNotYield.Multiline = false;
			this.lblNotYield.Name = "lblNotYield";
			this.lblNotYield.PasswordChar = '\0';
			this.lblNotYield.ReadOnly = true;
			this.lblNotYield.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblNotYield.ShowCheckBox = false;
			this.lblNotYield.Size = new System.Drawing.Size(150, 24);
			this.lblNotYield.TabIndex = 12;
			this.lblNotYield.TabNext = false;
			this.lblNotYield.Value = "0";
			this.lblNotYield.WidthType = UserControl.WidthTypes.Small;
			this.lblNotYield.XAlign = 90;
			// 
			// txtMem
			// 
			this.txtMem.AllowEditOnlyChecked = true;
			this.txtMem.Caption = "备注";
			this.txtMem.Checked = false;
			this.txtMem.EditType = UserControl.EditTypes.String;
			this.txtMem.Location = new System.Drawing.Point(603, 8);
			this.txtMem.MaxLength = 80;
			this.txtMem.Multiline = true;
			this.txtMem.Name = "txtMem";
			this.txtMem.PasswordChar = '\0';
			this.txtMem.ReadOnly = false;
			this.txtMem.ShowCheckBox = false;
			this.txtMem.Size = new System.Drawing.Size(237, 56);
			this.txtMem.TabIndex = 5;
			this.txtMem.TabNext = true;
			this.txtMem.Value = "";
			this.txtMem.WidthType = UserControl.WidthTypes.Long;
			this.txtMem.XAlign = 640;
			// 
			// txtRunningCard
			// 
			this.txtRunningCard.AllowEditOnlyChecked = true;
			this.txtRunningCard.Caption = "产品序列号";
			this.txtRunningCard.Checked = false;
			this.txtRunningCard.EditType = UserControl.EditTypes.String;
			this.txtRunningCard.Location = new System.Drawing.Point(16, 8);
			this.txtRunningCard.MaxLength = 40;
			this.txtRunningCard.Multiline = false;
			this.txtRunningCard.Name = "txtRunningCard";
			this.txtRunningCard.PasswordChar = '\0';
			this.txtRunningCard.ReadOnly = false;
			this.txtRunningCard.ShowCheckBox = false;
			this.txtRunningCard.Size = new System.Drawing.Size(274, 24);
			this.txtRunningCard.TabIndex = 0;
			this.txtRunningCard.TabNext = true;
			this.txtRunningCard.Value = "";
			this.txtRunningCard.WidthType = UserControl.WidthTypes.Long;
			this.txtRunningCard.XAlign = 90;
			this.txtRunningCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRunningCard_TxtboxKeyPress);
			// 
			// rdoNG
			// 
			this.rdoNG.Location = new System.Drawing.Point(832, 80);
			this.rdoNG.Name = "rdoNG";
			this.rdoNG.Size = new System.Drawing.Size(64, 24);
			this.rdoNG.TabIndex = 6;
			this.rdoNG.Tag = "1";
			this.rdoNG.Text = "不良品";
			this.rdoNG.Visible = false;
			this.rdoNG.Click += new System.EventHandler(this.rdoNG_Click);
			this.rdoNG.CheckedChanged += new System.EventHandler(this.rdoNG_CheckedChanged);
			// 
			// rdoGood
			// 
			this.rdoGood.Checked = true;
			this.rdoGood.Location = new System.Drawing.Point(840, 64);
			this.rdoGood.Name = "rdoGood";
			this.rdoGood.Size = new System.Drawing.Size(56, 24);
			this.rdoGood.TabIndex = 5;
			this.rdoGood.TabStop = true;
			this.rdoGood.Tag = "1";
			this.rdoGood.Text = "良品";
			this.rdoGood.Visible = false;
			this.rdoGood.Click += new System.EventHandler(this.rdoGood_Click);
			this.rdoGood.CheckedChanged += new System.EventHandler(this.rdoGood_CheckedChanged);
			// 
			// btnExit
			// 
			this.btnExit.BackColor = System.Drawing.SystemColors.Control;
			this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
			this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
			this.btnExit.Caption = "退出";
			this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnExit.Location = new System.Drawing.Point(528, 72);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(88, 22);
			this.btnExit.TabIndex = 4;
			// 
			// btnSave
			// 
			this.btnSave.BackColor = System.Drawing.SystemColors.Control;
			this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
			this.btnSave.ButtonType = UserControl.ButtonTypes.Save;
			this.btnSave.Caption = "保存";
			this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnSave.Enabled = false;
			this.btnSave.Location = new System.Drawing.Point(424, 72);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(88, 22);
			this.btnSave.TabIndex = 3;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// CollectedCount
			// 
			this.CollectedCount.AllowEditOnlyChecked = true;
			this.CollectedCount.Caption = "已采集数量";
			this.CollectedCount.Checked = false;
			this.CollectedCount.EditType = UserControl.EditTypes.Integer;
			this.CollectedCount.Location = new System.Drawing.Point(16, 40);
			this.CollectedCount.MaxLength = 40;
			this.CollectedCount.Multiline = false;
			this.CollectedCount.Name = "CollectedCount";
			this.CollectedCount.PasswordChar = '\0';
			this.CollectedCount.ReadOnly = true;
			this.CollectedCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.CollectedCount.ShowCheckBox = false;
			this.CollectedCount.Size = new System.Drawing.Size(174, 24);
			this.CollectedCount.TabIndex = 9;
			this.CollectedCount.TabNext = false;
			this.CollectedCount.Value = "0";
			this.CollectedCount.WidthType = UserControl.WidthTypes.Small;
			this.CollectedCount.XAlign = 90;
			// 
			// txtGOMO
			// 
			this.txtGOMO.AllowEditOnlyChecked = true;
			this.txtGOMO.Caption = "设定归属工单";
			this.txtGOMO.Checked = false;
			this.txtGOMO.EditType = UserControl.EditTypes.String;
			this.txtGOMO.Location = new System.Drawing.Point(248, 16);
			this.txtGOMO.MaxLength = 40;
			this.txtGOMO.Multiline = false;
			this.txtGOMO.Name = "txtGOMO";
			this.txtGOMO.PasswordChar = '\0';
			this.txtGOMO.ReadOnly = false;
			this.txtGOMO.ShowCheckBox = true;
			this.txtGOMO.Size = new System.Drawing.Size(236, 24);
			this.txtGOMO.TabIndex = 1;
			this.txtGOMO.TabNext = true;
			this.txtGOMO.Value = "";
			this.txtGOMO.WidthType = UserControl.WidthTypes.Normal;
			this.txtGOMO.XAlign = 351;
			this.txtGOMO.CheckBoxCheckedChanged += new System.EventHandler(this.txtGOMO_CheckBoxCheckedChanged);
			this.txtGOMO.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGOMO_TxtboxKeyPress);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.bRCardLetterULE);
			this.groupBox2.Controls.Add(this.bRCardLenULE);
			this.groupBox2.Controls.Add(this.checkBox1);
			this.groupBox2.Controls.Add(this.edtSoftName);
			this.groupBox2.Controls.Add(this.edtSoftInfo);
			this.groupBox2.Controls.Add(this.cbxOutLine);
			this.groupBox2.Controls.Add(this.txtGOMO);
			this.groupBox2.Controls.Add(this.axMSComm1);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(936, 96);
			this.groupBox2.TabIndex = 157;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "流程外工位指定";
			this.groupBox2.Visible = false;
			// 
			// bRCardLetterULE
			// 
			this.bRCardLetterULE.AllowEditOnlyChecked = true;
			this.bRCardLetterULE.Caption = "产品序列号首字符";
			this.bRCardLetterULE.Checked = false;
			this.bRCardLetterULE.EditType = UserControl.EditTypes.String;
			this.bRCardLetterULE.Enabled = false;
			this.bRCardLetterULE.Location = new System.Drawing.Point(496, 40);
			this.bRCardLetterULE.MaxLength = 40;
			this.bRCardLetterULE.Multiline = false;
			this.bRCardLetterULE.Name = "bRCardLetterULE";
			this.bRCardLetterULE.PasswordChar = '\0';
			this.bRCardLetterULE.ReadOnly = false;
			this.bRCardLetterULE.ShowCheckBox = true;
			this.bRCardLetterULE.Size = new System.Drawing.Size(260, 24);
			this.bRCardLetterULE.TabIndex = 28;
			this.bRCardLetterULE.TabNext = false;
			this.bRCardLetterULE.Value = "";
			this.bRCardLetterULE.WidthType = UserControl.WidthTypes.Normal;
			this.bRCardLetterULE.XAlign = 623;
			// 
			// bRCardLenULE
			// 
			this.bRCardLenULE.AllowEditOnlyChecked = true;
			this.bRCardLenULE.Caption = "产品序列号长度";
			this.bRCardLenULE.Checked = false;
			this.bRCardLenULE.EditType = UserControl.EditTypes.Integer;
			this.bRCardLenULE.Enabled = false;
			this.bRCardLenULE.Location = new System.Drawing.Point(496, 16);
			this.bRCardLenULE.MaxLength = 40;
			this.bRCardLenULE.Multiline = false;
			this.bRCardLenULE.Name = "bRCardLenULE";
			this.bRCardLenULE.PasswordChar = '\0';
			this.bRCardLenULE.ReadOnly = false;
			this.bRCardLenULE.ShowCheckBox = true;
			this.bRCardLenULE.Size = new System.Drawing.Size(248, 24);
			this.bRCardLenULE.TabIndex = 27;
			this.bRCardLenULE.TabNext = false;
			this.bRCardLenULE.Value = "";
			this.bRCardLenULE.WidthType = UserControl.WidthTypes.Normal;
			this.bRCardLenULE.XAlign = 611;
			// 
			// checkBox1
			// 
			this.checkBox1.Enabled = false;
			this.checkBox1.Location = new System.Drawing.Point(8, 40);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(128, 24);
			this.checkBox1.TabIndex = 10;
			this.checkBox1.Text = "送检批关联检查";
			// 
			// edtSoftName
			// 
			this.edtSoftName.AllowEditOnlyChecked = true;
			this.edtSoftName.Caption = "采集软件名称";
			this.edtSoftName.Checked = false;
			this.edtSoftName.EditType = UserControl.EditTypes.String;
			this.edtSoftName.Enabled = false;
			this.edtSoftName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.edtSoftName.Location = new System.Drawing.Point(248, 64);
			this.edtSoftName.MaxLength = 40;
			this.edtSoftName.Multiline = false;
			this.edtSoftName.Name = "edtSoftName";
			this.edtSoftName.PasswordChar = '\0';
			this.edtSoftName.ReadOnly = false;
			this.edtSoftName.ShowCheckBox = true;
			this.edtSoftName.Size = new System.Drawing.Size(236, 24);
			this.edtSoftName.TabIndex = 9;
			this.edtSoftName.TabNext = true;
			this.edtSoftName.Value = "";
			this.edtSoftName.WidthType = UserControl.WidthTypes.Normal;
			this.edtSoftName.XAlign = 351;
			// 
			// edtSoftInfo
			// 
			this.edtSoftInfo.AllowEditOnlyChecked = true;
			this.edtSoftInfo.Caption = "采集软件版本";
			this.edtSoftInfo.Checked = false;
			this.edtSoftInfo.EditType = UserControl.EditTypes.String;
			this.edtSoftInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.edtSoftInfo.Location = new System.Drawing.Point(248, 40);
			this.edtSoftInfo.MaxLength = 40;
			this.edtSoftInfo.Multiline = false;
			this.edtSoftInfo.Name = "edtSoftInfo";
			this.edtSoftInfo.PasswordChar = '\0';
			this.edtSoftInfo.ReadOnly = false;
			this.edtSoftInfo.ShowCheckBox = true;
			this.edtSoftInfo.Size = new System.Drawing.Size(236, 24);
			this.edtSoftInfo.TabIndex = 8;
			this.edtSoftInfo.TabNext = true;
			this.edtSoftInfo.Value = "";
			this.edtSoftInfo.WidthType = UserControl.WidthTypes.Normal;
			this.edtSoftInfo.XAlign = 351;
			this.edtSoftInfo.CheckBoxCheckedChanged += new System.EventHandler(this.edtSoftInfo_CheckBoxCheckedChanged);
			this.edtSoftInfo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtSoftInfo_TxtboxKeyPress);
			// 
			// cbxOutLine
			// 
			this.cbxOutLine.AllowEditOnlyChecked = true;
			this.cbxOutLine.Caption = "线外工序";
			this.cbxOutLine.Checked = false;
			this.cbxOutLine.Location = new System.Drawing.Point(8, 16);
			this.cbxOutLine.Name = "cbxOutLine";
			this.cbxOutLine.SelectedIndex = -1;
			this.cbxOutLine.ShowCheckBox = true;
			this.cbxOutLine.Size = new System.Drawing.Size(211, 24);
			this.cbxOutLine.TabIndex = 0;
			this.cbxOutLine.WidthType = UserControl.WidthTypes.Normal;
			this.cbxOutLine.XAlign = 86;
			this.cbxOutLine.CheckBoxCheckedChanged += new System.EventHandler(this.cbxOutLine_CheckBoxCheckedChanged);
			// 
			// axMSComm1
			// 
			this.axMSComm1.ContainingControl = this;
			this.axMSComm1.Enabled = true;
			this.axMSComm1.Location = new System.Drawing.Point(800, 40);
			this.axMSComm1.Name = "axMSComm1";
			this.axMSComm1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMSComm1.OcxState")));
			this.axMSComm1.Size = new System.Drawing.Size(38, 38);
			this.axMSComm1.TabIndex = 14;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtMO);
			this.groupBox1.Controls.Add(this.txtItem);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox1.Location = new System.Drawing.Point(0, 96);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(936, 56);
			this.groupBox1.TabIndex = 158;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "产品信息";
			// 
			// txtMO
			// 
			this.txtMO.AllowEditOnlyChecked = true;
			this.txtMO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.txtMO.Caption = "工单";
			this.txtMO.Checked = false;
			this.txtMO.EditType = UserControl.EditTypes.String;
			this.txtMO.Location = new System.Drawing.Point(288, 24);
			this.txtMO.MaxLength = 40;
			this.txtMO.Multiline = false;
			this.txtMO.Name = "txtMO";
			this.txtMO.PasswordChar = '\0';
			this.txtMO.ReadOnly = true;
			this.txtMO.ShowCheckBox = false;
			this.txtMO.Size = new System.Drawing.Size(170, 24);
			this.txtMO.TabIndex = 3;
			this.txtMO.TabNext = true;
			this.txtMO.Value = "";
			this.txtMO.WidthType = UserControl.WidthTypes.Normal;
			this.txtMO.XAlign = 325;
			// 
			// txtItem
			// 
			this.txtItem.AllowEditOnlyChecked = true;
			this.txtItem.Caption = "产品";
			this.txtItem.Checked = false;
			this.txtItem.EditType = UserControl.EditTypes.String;
			this.txtItem.Location = new System.Drawing.Point(61, 24);
			this.txtItem.MaxLength = 40;
			this.txtItem.Multiline = false;
			this.txtItem.Name = "txtItem";
			this.txtItem.PasswordChar = '\0';
			this.txtItem.ReadOnly = true;
			this.txtItem.ShowCheckBox = false;
			this.txtItem.Size = new System.Drawing.Size(170, 24);
			this.txtItem.TabIndex = 0;
			this.txtItem.TabNext = true;
			this.txtItem.Value = "";
			this.txtItem.WidthType = UserControl.WidthTypes.Normal;
			this.txtItem.XAlign = 98;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 152);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(936, 309);
			this.panel1.TabIndex = 159;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.errorCodeSelect);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(720, 309);
			this.panel3.TabIndex = 2;
			// 
			// errorCodeSelect
			// 
			this.errorCodeSelect.AddButtonTop = 101;
			this.errorCodeSelect.BackColor = System.Drawing.Color.Gainsboro;
			this.errorCodeSelect.CanInput = true;
			this.errorCodeSelect.Dock = System.Windows.Forms.DockStyle.Fill;
			this.errorCodeSelect.Location = new System.Drawing.Point(0, 0);
			this.errorCodeSelect.Name = "errorCodeSelect";
			this.errorCodeSelect.RemoveButtonTop = 189;
			this.errorCodeSelect.SelectedErrorCodeGroup = null;
			this.errorCodeSelect.Size = new System.Drawing.Size(720, 309);
			this.errorCodeSelect.TabIndex = 1;
			this.errorCodeSelect.EndErrorCodeInput += new System.EventHandler(this.errorCodeSelect_EndErrorCodeInput);
			this.errorCodeSelect.SelectedIndexChanged += new System.EventHandler(this.errorCodeSelect_SelectedIndexChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.lblResult);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel2.Location = new System.Drawing.Point(720, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(216, 309);
			this.panel2.TabIndex = 1;
			// 
			// lblResult
			// 
			this.lblResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblResult.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblResult.ForeColor = System.Drawing.Color.Black;
			this.lblResult.Location = new System.Drawing.Point(0, 0);
			this.lblResult.Name = "lblResult";
			this.lblResult.Size = new System.Drawing.Size(216, 309);
			this.lblResult.TabIndex = 0;
			this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(488, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 23);
			this.label2.TabIndex = 14;
			this.label2.Text = "label2";
			this.label2.Visible = false;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(640, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(152, 23);
			this.label3.TabIndex = 15;
			this.label3.Text = "label3";
			this.label3.Visible = false;
			// 
			// FPTCollectionGDNG
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ClientSize = new System.Drawing.Size(936, 565);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.panelButton);
			this.KeyPreview = true;
			this.Name = "FPTCollectionGDNG";
			this.Text = "良品/不良品采集";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FCollectionGDNG_Load);
			this.Closed += new System.EventHandler(this.FCollectionGDNG_Closed);
			this.Activated += new System.EventHandler(this.FCollectionGDNG_Activated);
			this.panelButton.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.axMSComm1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// 获得产品信息
		/// Laws Lu,2005/08/02,修改
		/// </summary>
		/// <returns></returns>
		private Messages GetProduct()
		{
			
			Messages productmessages = new Messages();
			ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);	
			//Amoi,Laws Lu,2005/08/02,注释
			//			try
			//			{
			//EndAmoi
			productmessages.AddMessages( dataCollect.GetIDInfo(txtRunningCard.Value.Trim().ToUpper()));
			if (productmessages.IsSuccess() )
			{  
				product = (ProductInfo)productmessages.GetData().Values[0];					
			}
			else
			{
				product = new ProductInfo();
			}

			//Amoi,Laws Lu,2005/08/02,注释
			//			}
			//			catch (Exception e)
			//			{
			//				productmessages.Add(new UserControl.Message(e));
			//			}
			//EndAmoi
			dataCollect = null;
			return productmessages;
		}

		/// <summary>
		/// 根据产品信息，决定部分控件的状态
		/// </summary>
		/// <returns></returns>
		private Messages CheckProduct()
		{
			Messages messages = new Messages ();
			try
			{
				messages.AddMessages(GetProduct());

				if (product.LastSimulation !=null )
				{
					txtItem.Value = product.LastSimulation.ItemCode;
					txtMO.Value = product.LastSimulation.MOCode;
					
					//Karron Qiu,2005-5-30,显示产品的电流值
					GetItemStandardElectricCurrent(txtItem.Value);

					//Amoi,Laws Lu,2005/08/02,注释
					//					if (rdoGood.Checked)
					//					{	
					//						btnSave.TextFocus(false, true);
					//					}
					//					else
					//					{
					//						SetErrorCodeList();
					//						errorCodeSelect.TextFocus(false, true);
					//					}
					//EndAmoi

					btnSave.Enabled = true;
					//btnSave.Enabled = true;
					this.btnGetElectricCurrent.Enabled = true;

					//Amoi,Laws Lu,2005/08/02,注释
					//messages.Add(new UserControl.Message(MessageType.Normal ,"$CS_CHECKSUCCESS"));
					//EndAmoi
				}
//				else if (txtGOMO.Value == string.Empty)
//				{
//					txtItem.Value = String.Empty;
//					txtMO.Value = String.Empty;
//
//					txtRunningCard.Value = "";
//
//					//messages.Add(new UserControl.Message(MessageType.Error ,"$CS_MUSTGOMO"));
//				}
				else if (product.LastSimulation == null)
				{
					txtItem.Value = String.Empty;
					txtMO.Value = String.Empty;

					txtRunningCard.Value = "";

					messages.Add(new UserControl.Message(MessageType.Error ,"$NoSimulationInfo"));
				}
				//Amoi,Laws Lu,2005/08/02,注释
				//				else if (txtGOMO.Value != string.Empty) 
				//				{
				//					btnSave.Enabled = true;
				//				}
				//EndAmoi
			}
			catch (Exception e)
			{
				messages.Add(new UserControl.Message(e));

			}
			return messages;
		}

		private Hashtable listActionCheckStatus = new Hashtable();

		private void txtRunningCard_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//txtRunningCard.Value = txtRunningCard.Value.Trim();
			if (e.KeyChar == '\r')
			{
				if (txtRunningCard.Value.Trim() == string.Empty)
				{
					//Laws Lu,2005/08/10,新增	在没有输入产品序列号时清空工单和料号
					txtMO.Value = String.Empty;
					txtItem.Value = String.Empty;
					//End Laws Lu

					ApplicationRun.GetInfoForm().Add("$CS_Please_Input_RunningCard");

					//将焦点移到产品序列号输入框
					//Application.DoEvents();
					txtRunningCard.TextFocus(false, true);
//					System.Windows.Forms.SendKeys.Send("+{TAB}");

					return;
					
				}		
				else
				{
					//Laws Lu,2005/08/16,修改	把msg换成globeMSG
					globeMSG = CheckProduct();

					//Laws Lu,2005/10/19,新增	缓解性能问题
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();

					// Added by Icyer 2005/10/28
					if (Resource == null)
					{
						BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
						Resource = (Domain.BaseSetting.Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);
					}
					actionCheckStatus = new ActionCheckStatus();
					actionCheckStatus.ProductInfo = product;

					if(actionCheckStatus.ProductInfo != null)
					{
						actionCheckStatus.ProductInfo.Resource = Resource;
					}

					string strMoCode = String.Empty;
					if (product != null && product.LastSimulation != null)
					{
						strMoCode = product.LastSimulation.MOCode;
					}
					
					if(strMoCode != String.Empty)
					{
						if (listActionCheckStatus.ContainsKey(strMoCode))
						{
							actionCheckStatus = (ActionCheckStatus)listActionCheckStatus[strMoCode];
							actionCheckStatus.ProductInfo = product;
							actionCheckStatus.ActionList = new ArrayList();
						}
						else
						{
							listActionCheckStatus.Add(strMoCode, actionCheckStatus);
						}
					}
					//Amoi,Laws Lu,2005/08/02,修改
					
					if (txtGOMO.Checked == true)
					{
						globeMSG.AddMessages(RunGOMO(actionCheckStatus));

					}

					if ((edtSoftInfo.Checked)&&(edtSoftInfo.Value.Trim()==string.Empty))
					{
						ApplicationRun.GetInfoForm().Add("$CS_CMPleaseInputSoftInfo");
						edtSoftInfo.TextFocus(false, true);
						return;
					}
					if ((edtSoftName.Checked)&&(edtSoftName.Value.Trim()==string.Empty))
					{
						ApplicationRun.GetInfoForm().Add("$CS_CMPleaseInputSoftName");
						edtSoftName.TextFocus(false, true);
						return;
					}

					//EndAmoi

					

					//Karron Qiu,2006-5-30,不自动保存.
					
//					//Amoi,Laws Lu,2005/08/02,新增 如果良品RadioBox被选中则直接保存
//					//Laws Lu,2005/08/06,修改	Lucky提出工单归属失败也可以做下面的逻辑
//					if(/*msg.IsSuccess() && */rdoGood.Checked == true)
//					{
//						btnSave_Click(sender,e);
//
//						//将焦点移到产品序列号输入框
//						//						txtRunningCard.TextFocus(false, true);
//						//						System.Windows.Forms.SendKeys.Send("+{TAB}");
//						
//					}
//					else if (/*msg.IsSuccess() && */rdoNG.Checked == true)
//					{
//						//清除ErrorCode列表
//						
//
//						errorCodeSelect.ClearErrorGroup();
//						errorCodeSelect.ClearSelectErrorCode();
//						errorCodeSelect.ClearSelectedErrorCode();
//
//						if(SetErrorCodeList())
//						{
//							errorCodeSelect.TextFocus(false, true);
//
//							if(errorCodeSelect.ErrorGroupCount < 1)
//							{
//								globeMSG.Add(new UserControl.Message(MessageType.Error, "$CS_MUST_MAINTEN_ERRGROUP"));
//
//								txtRunningCard.Value = "";
//								
//							}
//
//							btnSave.Enabled = true;
//						}
//						
//					}

					

					//Laws Lu,2005/08/16,注释	逻辑变更,此处处理不再需要
					/*
					else if (!msg.IsSuccess())
					{
						//清除ErrorCode列表
						errorCodeSelect.ClearErrorGroup();
						errorCodeSelect.ClearSelectErrorCode();
						errorCodeSelect.ClearSelectedErrorCode();

						btnSave.Enabled = false;

						//将焦点移到产品序列号输入框
						txtRunningCard.TextFocus(false, true);
						System.Windows.Forms.SendKeys.Send("+{TAB}");
						
					}
					*/
					//EndAmoi
				}
				
				

				//将焦点移到产品序列号输入框
				ApplicationRun.GetInfoForm().Add(globeMSG);

				if(globeMSG.IsSuccess())
				{
					//this.textBox1.Enabled = true;
					//Application.DoEvents();
					this.txtElectric.TextFocus(false, true);
//					System.Windows.Forms.SendKeys.Send("+{TAB}");
				}
				else
				{
					//Application.DoEvents();
					txtRunningCard.TextFocus(false, true);
//					System.Windows.Forms.SendKeys.Send("+{TAB}");
				}

				globeMSG.ClearMessages();

				
				
				//e.Handled = true;
			}
			else
			{
				btnSave.Enabled = false;
				this.btnGetElectricCurrent.Enabled = false;
			}

			this.lblResult.Text = "";
		}

		private void InitErrorCodeList()
		{
			//errorCodeSelect.ClearErrorGroup();
			//errorCodeSelect.ClearSelectErrorCode();
			errorCodeSelect.ClearSelectedErrorCode();

			if(SetErrorCodeList())
			{
				//errorCodeSelect.TextFocus(false, true);
				errorCodeSelect.SetFocusErrorCode();

				if(errorCodeSelect.ErrorGroupCount < 1)
				{
					globeMSG.Add(new UserControl.Message(MessageType.Error, "$CS_MUST_MAINTEN_ERRGROUP"));

					txtRunningCard.Value = "";
					
				}

				btnSave.Enabled = true;
			}
		}

		//Laws Lu,2005/08/25,修改	保存失败清空RunningCard输入框
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			Messages messages = new Messages();

			ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
			
			if( cbxOutLine.Checked && this.checkBox1.Checked )
			{
				if(onLine.CheckBelongToLot( txtRunningCard.Value ))	
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$CS_RCrad_Belong_To_Lot"));

					//Application.DoEvents();
					txtRunningCard.TextFocus(false, true);
					DataProvider.RollbackTransaction();

					return;
				}
			}

			// Added end

			DataProvider.BeginTransaction();
			try
			{
				//Laws Lu,归属工单和线外工序只能二者选其一
				if (txtGOMO.Checked && cbxOutLine.Checked)
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$CS_Outline_Can_Not_GOMO"));
					//Application.DoEvents();
					txtRunningCard.TextFocus(false, true);

					DataProvider.RollbackTransaction();

					return;
				}

				if(this.rdoNG.Checked == false && this.rdoGood.Checked == false)
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$Please_Check_Electric_Current"));
					//Application.DoEvents();
					txtRunningCard.TextFocus(false, true);

					DataProvider.RollbackTransaction();

					return;
				}

				#region Laws Lu,保存按钮的主逻辑

			

				if (txtGOMO.Checked)
				{
					if (rdoGood.Checked)
					{
						//Amoi,Laws Lu,2005/08/03,注释
						//						messages = RunGOMO();
						//EndAmoi
						messages = GetProduct();


						if (messages.IsSuccess())
						{
							messages.AddMessages(RunGood(actionCheckStatus));

							if ((edtSoftInfo.Checked )||(edtSoftName.Checked) && product != null)
							{			
								//ProductInfo product=(ProductInfo)messages.GetData().Values[0];
								//Laws Lu,2005/10/11,新增	软件采集
								SoftwareActionEventArgs sArg = new SoftwareActionEventArgs(  ActionType.DataCollectAction_SoftINFO,txtRunningCard.Value.Trim().ToUpper(),
									ApplicationService.Current().UserCode,ApplicationService.Current().ResourceCode,
									product,edtSoftInfo.Value.Trim(),edtSoftName.Value.Trim());

								IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(sArg.ActionType);
							
								messages.AddMessages( ((IActionWithStatus)dataCollectModule).Execute(sArg,actionCheckStatus));

								if (messages.IsSuccess())
									messages.Add(new UserControl.Message(MessageType.Success,"$CS_Soft_CollectSuccess"));
							}

							
						}

					}
					else if (rdoNG.Checked)
					{
						messages = CheckErrorCodes();
 
						ApplicationRun.GetInfoForm().Add(messages);
						
						//Amoi,Laws Lu,2005/08/03,注释
						//						if (messages.IsSuccess())
						//						{
						//							messages = RunGOMO();
						//							ApplicationRun.GetInfoForm().Add(messages);
						//						}
						//EndAmoi

						if (messages.IsSuccess())
						{
							messages = GetProduct();
							ApplicationRun.GetInfoForm().Add(messages);
						}


						if (messages.IsSuccess())
						{
							messages.AddMessages(RunNG(actionCheckStatus));	
							//Laws Lu,2005/10/11,新增	软件采集
							if ((edtSoftInfo.Checked )||(edtSoftName.Checked) && product != null)
							{			
								//								ProductInfo product=(ProductInfo)messages.GetData().Values[0];

								SoftwareActionEventArgs sArg = new SoftwareActionEventArgs(  ActionType.DataCollectAction_SoftINFO,txtRunningCard.Value.Trim().ToUpper(),
									ApplicationService.Current().UserCode,ApplicationService.Current().ResourceCode,
									product,edtSoftInfo.Value.Trim(),edtSoftName.Value.Trim());

								IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(sArg.ActionType);
							
								messages.AddMessages( ((IActionWithStatus)dataCollectModule).Execute(sArg,actionCheckStatus));

								if (messages.IsSuccess())
									messages.Add(new UserControl.Message(MessageType.Success,"$CS_Soft_CollectSuccess"));
							}

							

						}
					}
				}
				else
				{
					if (rdoGood.Checked)
					{
						messages.AddMessages(RunGood(actionCheckStatus));
						//Laws Lu,2005/10/11,新增	软件采集
						if ((edtSoftInfo.Checked )||(edtSoftName.Checked) && product != null)
						{			
							//ProductInfo product=(ProductInfo)messages.GetData().Values[0];

							SoftwareActionEventArgs sArg = new SoftwareActionEventArgs(  ActionType.DataCollectAction_SoftINFO,txtRunningCard.Value.Trim().ToUpper(),
								ApplicationService.Current().UserCode,ApplicationService.Current().ResourceCode,
								product,edtSoftInfo.Value.Trim(),edtSoftName.Value.Trim());

							IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(sArg.ActionType);
							
							messages.AddMessages(((IActionWithStatus)dataCollectModule).Execute(sArg,actionCheckStatus));

							if (messages.IsSuccess())
								messages.Add(new UserControl.Message(MessageType.Success,"$CS_Soft_CollectSuccess"));
						}

					}
					else if (rdoNG.Checked)
					{
						messages = CheckErrorCodes(); 

						ApplicationRun.GetInfoForm().Add(messages);

						if (messages.IsSuccess())
						{
							
							messages.AddMessages(RunNG(actionCheckStatus));	
							//Laws Lu,2005/10/11,新增	软件采集
							if ((edtSoftInfo.Checked )||(edtSoftName.Checked) && product != null)
							{			
								//ProductInfo product=(ProductInfo)messages.GetData().Values[0];

								SoftwareActionEventArgs sArg = new SoftwareActionEventArgs(  ActionType.DataCollectAction_SoftINFO,txtRunningCard.Value.Trim().ToUpper(),
									ApplicationService.Current().UserCode,ApplicationService.Current().ResourceCode,
									product,edtSoftInfo.Value.Trim(),edtSoftName.Value.Trim());

								IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(sArg.ActionType);
							
								messages.AddMessages(((IActionWithStatus)dataCollectModule).Execute(sArg,actionCheckStatus));

								if (messages.IsSuccess())
									messages.Add(new UserControl.Message(MessageType.Success,"$CS_Soft_CollectSuccess"));
							}

							

						}
					}
					
				}
				#endregion

				#region 保存测试电流值 Karron Qiu,2006-5-30 (Removed by Icyer 2006-08-11)

				/*
				PreTestValue _PreTestValue = this.DeviceFacade.CreateNewPreTestValue();

				_PreTestValue.ID = Guid.NewGuid().ToString();
				_PreTestValue.MaxValue = decimal.Parse(this.txtMaxValue.Value);
				_PreTestValue.MinValue = decimal.Parse(this.txtMinValue.Value);
				_PreTestValue.MOCode = this.txtMO.Value;
				_PreTestValue.RCard = this.txtRunningCard.Value;
				_PreTestValue.Value = decimal.Parse(this.txtElectric.Value);

				//Added By Karron Qiu,2006-7-17
				_PreTestValue.ResCode = ApplicationService.Current().ResourceCode;

				if(ApplicationService.Current().LoginInfo != null && ApplicationService.Current().LoginInfo.Resource != null)
					_PreTestValue.SSCode = ApplicationService.Current().LoginInfo.Resource.StepSequenceCode;

				_PreTestValue.ItemCode = product.NowSimulation.ItemCode;
				_PreTestValue.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
				_PreTestValue.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
				_PreTestValue.MaintainUser = ApplicationService.Current().UserCode;
				_PreTestValue.TestResult = rdoGood.Checked ? "PASS":"FAIL";
				//End

				this.DeviceFacade.AddPreTestValue(_PreTestValue);
				*/

				#endregion

				#region 保存测试电流值 Icyer, 2006-08-11
				if (messages.IsSuccess())
				{
					BenQGuru.eMES.SPCDataCenter.DataEntry dataEntry = new BenQGuru.eMES.SPCDataCenter.DataEntry();
					Simulation sim =  product.NowSimulation;
					dataEntry.ModelCode = sim.ModelCode;
					dataEntry.ItemCode = sim.ItemCode;
					dataEntry.MOCode = sim.MOCode;
					dataEntry.RunningCard = sim.RunningCard;
					dataEntry.RunningCardSequence = sim.RunningCardSequence;
					dataEntry.SegmentCode = ApplicationService.Current().LoginInfo.Resource.SegmentCode;
					dataEntry.LineCode = ApplicationService.Current().LoginInfo.Resource.StepSequenceCode;
					dataEntry.ResourceCode = ApplicationService.Current().LoginInfo.Resource.ResourceCode;
					dataEntry.OPCode = sim.OPCode;
					if (sim.ProductStatus == ProductStatus.NG)
						dataEntry.TestResult = "F";
					else
						dataEntry.TestResult = "P";
					DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
					DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate,dbDateTime.DBTime);
					dataEntry.TestDate = FormatHelper.TODateInt(dtNow);
					dataEntry.TestTime = FormatHelper.TOTimeInt(dtNow);
					//dataEntry.TestResult = sim.ProductStatus;
					dataEntry.TestUser = ApplicationService.Current().UserCode;
					dataEntry.AddTestData(SPCObjectList.PT_ELECTRIC, decimal.Parse(this.txtElectric.Value));
					BenQGuru.eMES.SPCDataCenter.DataHandler handler = new BenQGuru.eMES.SPCDataCenter.DataHandler(this.DataProvider);
					//messages.AddMessages(handler.CollectData(dataEntry));
				}
				#endregion

				if (messages.IsSuccess())
				{
					DataProvider.CommitTransaction();
					
					this.AddCollectedCount();
				}
				else
				{
					txtRunningCard.Value = String.Empty;

					DataProvider.RollbackTransaction();
				}

			}
			catch (Exception exp)
			{
				DataProvider.RollbackTransaction();
				messages.Add(new UserControl.Message(exp));
				txtRunningCard.Value = String.Empty;
				
				globeMSG.AddMessages(messages);
			}
			finally
			{
				globeMSG.AddMessages(messages);

				ApplicationRun.GetInfoForm().Add(globeMSG);

				globeMSG.ClearMessages();
				//Laws Lu,2005/10/19,新增	缓解性能问题
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
			}

			//重新设置页面控件状态
			if(messages.IsSuccess())
			{
				ClearFormMessages();
				ResetRadioButton();
			}

			if(!messages.IsSuccess() && rdoGood.Checked == true)//Amoi,Laws Lu,2005/08/02,新增	失败时Save按钮状态为False
			{
				txtRunningCard.Value = String.Empty;

				//Application.DoEvents();
				txtRunningCard.TextFocus(false, true);

				btnSave.Enabled = false;
			}
			else if (!messages.IsSuccess() && rdoNG.Checked == true)//Amoi,Laws Lu,2005/08/10,新增	失败时Save按钮状态为True
			{
				txtRunningCard.Value = String.Empty;

				//Application.DoEvents();
				txtRunningCard.TextFocus(false, true);
//				txtRunningCard.TextFocus(false, true);

				btnSave.Enabled = true;
			}

			//重置MSCOMM
//			ResetMSComm();

			
		}

		private void ClearCollectedCount()
		{
			this.CollectedCount.Text = "0";
		}

		//加一
		private void AddCollectedCount()
		{
			this.CollectedCount.Value = Convert.ToString(Convert.ToInt32(this.CollectedCount.Value) + 1);
			double total = int.Parse(CollectedCount.Value=="0"?"1":CollectedCount.Value);
			double notyield = iNG/total*100;
			this.lblNotYield.Value = Convert.ToString(System.Math.Round(notyield,2));
		}

		/// <summary>
		/// GOOD指令采集
		/// </summary>
		/// <returns></returns>
		private Messages RunGood(ActionCheckStatus actionCheckStatus)
		{
			Messages messages = new Messages ();
			//Amoi,Laws Lu,2005/08/02,注释
			//			try
			//			{
			//EndAmoi
			if (cbxOutLine.Checked)
			{
				if(product.LastSimulation == null)
				{
					messages.Add(new UserControl.Message(MessageType.Error ,"$NoSimulation"));
					return messages;
				}

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

				actionCheckStatus.ProductInfo = product;

				//actionCheckStatus.NeedUpdateSimulation = false;

				IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_OutLineGood);

				messages.AddMessages( (dataCollectModule).Execute(new OutLineActionEventArgs(ActionType.DataCollectAction_OutLineGood, 
					txtRunningCard.Value.Trim(),
					ApplicationService.Current().UserCode,
					ApplicationService.Current().ResourceCode,product,cbxOutLine.SelectedItemText)));


			}
			else
			{
				//actionCheckStatus.NeedUpdateSimulation = false;

				IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_GOOD);

				messages.AddMessages( ((IActionWithStatus)dataCollectModule).Execute(new ActionEventArgs(ActionType.DataCollectAction_GOOD, txtRunningCard.Value.Trim(),
					ApplicationService.Current().UserCode,
					ApplicationService.Current().ResourceCode,product),actionCheckStatus));

			}
			if (messages.IsSuccess())
			{
				#region Code not to use
				// Added by Icyer 2005/10/31
				// 更新Wip & Simulation
				//				ActionOnLineHelper onLine = new ActionOnLineHelper(DataProvider);
				//
				//				ActionEventArgs actionEventArgs;
				//				actionEventArgs = (ActionEventArgs)actionCheckStatus.ActionList[actionCheckStatus.ActionList.Count - 1];
				//				actionEventArgs.ProductInfo.LastSimulation = product.LastSimulation;
				//				onLine.Execute(actionEventArgs, actionCheckStatus, true, false);
				//					
				//				DataCollectFacade dataCollect = new DataCollect.DataCollectFacade(this.DataProvider);
				//				ReportHelper reportCollect= new ReportHelper(this.DataProvider);
				//				for (int i = 0; i < actionCheckStatus.ActionList.Count; i++)
				//				{
				//					actionEventArgs = (ActionEventArgs)actionCheckStatus.ActionList[i];
				//					//更新WIP
				//					if (actionEventArgs.OnWIP != null)
				//					{
				//						for (int iwip = 0; iwip < actionEventArgs.OnWIP.Count; iwip++)
				//						{
				//							if (actionEventArgs.OnWIP[iwip] is OnWIP)
				//							{
				//								dataCollect.AddOnWIP((OnWIP)actionEventArgs.OnWIP[iwip]);
				//							}
				//							else if (actionEventArgs.OnWIP[iwip] is OnWIPSoftVersion)
				//							{
				//								dataCollect.AddOnWIPSoftVersion((OnWIPSoftVersion)actionEventArgs.OnWIP[iwip]);
				//							}
				//						}
				//					}
				/*
						//根据Action类型更新Report
						if (actionCheckStatus.NeedFillReport == false)
						{
							if (actionEventArgs.ActionType == ActionType.DataCollectAction_GoMO)
							{
								reportCollect.ReportLineQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo, actionCheckStatus);
							}
							else if (actionEventArgs.ActionType == ActionType.DataCollectAction_CollectINNO || actionEventArgs.ActionType == ActionType.DataCollectAction_CollectKeyParts)
							{
								reportCollect.ReportResQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo, actionCheckStatus);
							}
							else if (actionEventArgs.ActionType == ActionType.DataCollectAction_GOOD)
							{
								reportCollect.ReportLineQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo, actionCheckStatus);
								reportCollect.ReportResQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo, actionCheckStatus);
							}
						}
						*/
			
				// Added end
				#endregion

				messages.Add(new UserControl.Message(MessageType.Success ,string.Format("$CS_GOODSUCCESS,$CS_Param_ID: {0}",txtRunningCard.Value.Trim())));
			}
			return messages;
		}
		//Amoi,Laws Lu,2005/08/02,注释
		//			}
		//			catch (Exception e)
		//			{
		//				messages.Add(new UserControl.Message(e));
		//				return messages;
		//			}
		//EndAmoi
	
		/// <summary>
		/// NG指令采集
		/// </summary>
		/// <returns></returns>
		private Messages RunNG(ActionCheckStatus actionCheckStatus)
		{
			Messages messages = new Messages ();
			//Amoi,Laws Lu,2005/08/02,注释
			//			try
			//			{
			//EndAmoi

			object[] ErrorCodes = GetSelectedErrorCodes();//取不良代码组＋不良代码
			

			if (cbxOutLine.Checked)
			{

				if(product.LastSimulation == null)
				{
					messages.Add(new UserControl.Message(MessageType.Error ,"$NoSimulation"));
					return messages;
				}

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

				actionCheckStatus.ProductInfo = product;

				//actionCheckStatus.NeedUpdateSimulation = false;

				IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_OutLineNG);
				messages.AddMessages((dataCollectModule).Execute(new OutLineActionEventArgs(ActionType.DataCollectAction_OutLineNG, 
					txtRunningCard.Value.Trim(),
					ApplicationService.Current().UserCode,
					ApplicationService.Current().ResourceCode,product,
					cbxOutLine.SelectedItemText,
					ErrorCodes,txtMem.Value)));
			}
			else
			{
				//actionCheckStatus.NeedUpdateSimulation = false;

				IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_NG);

				messages.AddMessages( ((IActionWithStatus)dataCollectModule).Execute(
					new TSActionEventArgs(ActionType.DataCollectAction_NG,
					txtRunningCard.Value.Trim(),
					ApplicationService.Current().UserCode,
					ApplicationService.Current().ResourceCode,
					product,
					ErrorCodes, 
					null,
					txtMem.Value),actionCheckStatus));
			}
			if (messages.IsSuccess())
			{
				iNG = iNG  + 1;
				
				messages.Add(new UserControl.Message(MessageType.Success ,string.Format("$CS_NGSUCCESS,$CS_Param_ID: {0}",txtRunningCard.Value.Trim())));
			}
			return messages;
			//Amoi,Laws Lu,2005/08/02,注释
			//			}
			//			catch (Exception e)
			//			{
			//				messages.Add(new UserControl.Message(e));
			//				return messages;
			//			}
			//EndAmoi
		}
		private ActionCheckStatus actionCheckStatus = new ActionCheckStatus();
		private Domain.BaseSetting.Resource Resource;

		/// <summary>
		/// 工单归属采集
		/// </summary>
		/// <returns></returns>
		private Messages RunGOMO(ActionCheckStatus actionCheckStatus)
		{
			Messages messages = new Messages ();
			
			//Amoi,Laws Lu,2005/08/02,注释
			//			try
			//			{
			//EndAmoi
			
			//Laws Lu,新建数据采集Action
			ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
			//IAction dataCollectModule = (new ActionFactory(this.DataProvider)).CreateAction(ActionType.DataCollectAction_GoMO);

			//Laws Lu,2005/09/14,新增	工单不能为空
			if(txtGOMO.Checked == true && txtGOMO.Value.Trim() != String.Empty)
			{

				actionCheckStatus.ProductInfo = product;

				//检查产品序列号格式
				bool lenCheckBool = true;
				//产品序列号长度检查
				if(bRCardLenULE.Checked && bRCardLenULE.Value.Trim() != string.Empty)
				{

					int len = 0;
					try
					{
						len = int.Parse(bRCardLenULE.Value.Trim());
                        if (txtRunningCard.Value.Trim().Length != len)
						{
							lenCheckBool = false;
							messages.Add(new UserControl.Message(MessageType.Error,"$CS_Before_Card_Length_FLetter_NotCompare")); 
						}
					}
					catch
					{
						messages.Add(new UserControl.Message(MessageType.Error,"$CS_Before_Card_Length_FLetter_NotCompare")); 
					}
				}

				//产品序列号首字符检查
				if(bRCardLetterULE.Checked && bRCardLetterULE.Value.Trim() != string.Empty)
				{
					int index = txtRunningCard.Value.IndexOf( bRCardLetterULE.Value.Substring(0,1) );
					if( index != 0 )
					{
						lenCheckBool = false;
						messages.Add(new UserControl.Message(MessageType.Error,"$CS_Before_Card_FLetter_NotCompare")); 
					}
				}
				//Laws Lu,参数定义
				GoToMOActionEventArgs args = new GoToMOActionEventArgs(ActionType.DataCollectAction_GoMO,
					txtRunningCard.Value.Trim(),
					ApplicationService.Current().UserCode,
					ApplicationService.Current().ResourceCode,product,txtGOMO.Value);

				//actionCheckStatus.NeedUpdateSimulation = false;

				//Laws Lu,执行工单采集并收集返回信息
				if(messages.IsSuccess())
				{
					messages.AddMessages(onLine.Action(args,actionCheckStatus));
				}
			}
				
			if (messages.IsSuccess())
			{
				messages.Add(new UserControl.Message(MessageType.Success ,"$CS_GOMOSUCCESS"));
				txtRunningCard.TextFocus(false, true);
			}

			return messages;
			//Amoi,Laws Lu,2005/08/02,注释
			//			}
			//			catch (Exception e)
			//			{
			//				messages.Add(new UserControl.Message(e));
			//				return messages;
			//			}
			//EndAmoi
		}

		private void rdoGood_Click(object sender, System.EventArgs e)
		{
			errorCodeSelect.Enabled = false;
		}

		private void rdoNG_Click(object sender, System.EventArgs e)
		{
			errorCodeSelect.Enabled = true;
			
		}
		
		/// <summary>
		/// 保存成功后清除窗体数据并初始化控件状态
		/// Amoi,Laws Lu,2005/08/02
		/// </summary>
		private void ClearFormMessages()
		{
			
			txtMO.Value = string.Empty;
			txtItem.Value = String.Empty;
			txtMem.Value =string.Empty;
			this.txtMaxValue.Value = "";
			this.txtMinValue.Value = "";
//			this.lblResult.Text = "";
			txtElectric.Value = "";
			
//			errorCodeSelect.ClearErrorGroup();
			errorCodeSelect.ClearSelectedErrorCode();
//			errorCodeSelect.ClearSelectErrorCode();

			btnSave.Enabled = false;
			
			InitialRunningCard();

			//如果线外工序被选中,则不需要重新初始化
			if(!cbxOutLine.Checked)
			{
				InitializeOutLineOP();
			}

			//Application.DoEvents();
			txtRunningCard.TextFocus(false, true);
		}
		
		/// <summary>
		/// 初始化RunningCard的状态
		/// Amoi,Laws Lu,2005/08/02,新增
		/// </summary>
		private void InitialRunningCard()
		{
			txtRunningCard.Value = String.Empty;
			txtRunningCard.TextFocus(false, true);
		}

		private void FCollectionGDNG_Load(object sender, System.EventArgs e)
		{
			InitialRunningCard();

			//Amoi,Laws Lu,2005/08/02,注释
			//txtRunningCard.TextFocus(false, true);
			//EndAmoi
		}
		
		/// <summary>
		/// 清除以前设置，并重新设置不良代码列表的值
		/// Amoi,Laws Lu,2005/08/02,新增
		/// </summary>
		private bool SetErrorCodeList()
		{
			TSModelFacade tsFacade = new TSModelFacade(this.DataProvider);
			
			//Amoi,Laws Lu,2005/08/06,修改
			string strItem = String.Empty;
			
//			//Laws Lu,2005/08/16
//			Messages productmessages = new Messages();
//			ProductInfo productInfo = new ProductInfo();
//			
//			ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
//
//			productmessages.AddMessages(dataCollect.GetIDInfo(txtRunningCard.Value.Trim().ToUpper()));
//			if (productmessages.IsSuccess() )
//			{  
//				productInfo = (ProductInfo)productmessages.GetData().Values[0];					
//			}

			GetProduct();

			if (product.LastSimulation !=null )
			{
				strItem = product.LastSimulation.ItemCode;

				txtItem.Value = product.LastSimulation.ItemCode;
				txtMO.Value = product.LastSimulation.MOCode;
			}
			else
			{
				globeMSG.Add(new UserControl.Message("$NoSimulation"));

				txtItem.Value = String.Empty;
				txtMO.Value = String.Empty;
				//Laws Lu,2005/08/16
				return false;
			}
			
			object[] errorCodeGroups = tsFacade.GetErrorCodeGroupByItemCode(strItem);
			if (errorCodeGroups!= null)
			{
				string currentGroup = errorCodeSelect.SelectedErrorCodeGroup;
				errorCodeSelect.ClearErrorGroup();
				errorCodeSelect.ClearSelectedErrorCode();
				errorCodeSelect.ClearSelectErrorCode();
				errorCodeSelect.AddErrorGroups(errorCodeGroups);

				if(currentGroup != null)
				{
					errorCodeSelect.SelectedErrorCodeGroup = currentGroup;
				}
			}
			//Laws Lu,2005/08/16
			return true;
			//EndAmoi
		}
		
		/// <summary>
		/// 设置不良代码列表的值
		/// Amoi,Laws Lu,2005/08/02,新增
		/// </summary>
		private void errorCodeSelect_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TSModelFacade tsFacade = new TSModelFacade(this.DataProvider);
			object[] errorCodes = tsFacade.GetSelectedErrorCodeByErrorCodeGroupCode(errorCodeSelect.SelectedErrorCodeGroup);
			if (errorCodes!= null)
			{
				errorCodeSelect.ClearSelectErrorCode();
				errorCodeSelect.AddErrorCodes(errorCodes);
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

		private object[] GetSelectedErrorCodes()
		{
			object[] result = errorCodeSelect.GetSelectedErrorCodes();
			return result;
		}

		private bool CheckOutlineOPInRoute()
		{
			BaseModelFacade bsmodel = new BaseModelFacade(this.DataProvider);
			return bsmodel.IsOperationInRoute(product.LastSimulation.RouteCode,cbxOutLine.SelectedItemText);
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

		private void FCollectionGDNG_Activated(object sender, System.EventArgs e)
		{
			InitializeOutLineOP();
			txtRunningCard.TextFocus(false, true);
		}

		//Amoi,Laws Lu,2005/08/02,注释
		#region Laws Lu 注释，根据页面归属工单的值获取工单信息
		/*
		/// <summary>
		/// 根据归属工单取工单信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtGOMO_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
			if (e.KeyChar == '\r')
			{
				if (txtGOMO.Checked && txtGOMO.Value.Trim() != string.Empty) 
				{
					MOFacade moFacade = new MOFacade( this.DataProvider );
					object obj = moFacade.GetMO( this.txtGOMO.Value.Trim().ToUpper() );
					if (obj == null)
					{
						e.Handled = true;
						ApplicationRun.GetInfoForm().Add("$CS_MO_NOT_EXIST");
						txtGOMO.TextFocus(false, true);
					}
					else
					{
						txtMO.Value = ((MO)obj).MOCode;
						txtItem.Value = ((MO)obj).ItemCode;
						if (rdoNG.Checked)
							SetErrorCodeList();
					}				
				}
			}
			
		}
		*/
		#endregion
		//EndAmoi

		private void FCollectionGDNG_Closed(object sender, System.EventArgs e)
		{
			if (_domainDataProvider!=null)
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();  
			}
		}

		private void rdoGood_CheckedChanged(object sender, System.EventArgs e)
		{
			txtRunningCard.TextFocus(false, true);
		}

		private void rdoNG_CheckedChanged(object sender, System.EventArgs e)
		{
			txtRunningCard.TextFocus(false, true);
		}

		private void errorCodeSelect_Resize(object sender, System.EventArgs e)
		{
			errorCodeSelect.AutoAdjustButtonLocation();
		}
		//Laws Lu,2005/10/12,新增	允许软件采集
		private void edtSoftInfo_CheckBoxCheckedChanged(object sender, System.EventArgs e)
		{
			if (edtSoftInfo.Checked )
				edtSoftName.Enabled =true;
			else
			{
				edtSoftName.Checked =false;
				edtSoftName.Enabled =false;
			}
		}

		private void edtSoftInfo_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar =='\r')
			{
				txtRunningCard.TextFocus(false, true);
				//SendKeys.Send("+{TAB}");
                //Remove UCLabel.SelectAll;
			}
		}


		private bool IsNumber(object obj)
		{
			try
			{
				int.Parse(obj.ToString());
				return true;
			}
			catch
			{
				return false;
			}
		}


		private void CollectedCount_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData != Keys.D0 && e.KeyData != Keys.D1 && e.KeyData != Keys.D2 && e.KeyData != Keys.D3 &&
				e.KeyData != Keys.D4 && e.KeyData != Keys.D5 && e.KeyData != Keys.D6 && e.KeyData != Keys.D7 &&
				e.KeyData != Keys.D8 && e.KeyData != Keys.D9 )
			{
				CollectedCount.Text = "0";
			}
		}

		/// <summary>
		/// added by jessie lee,判断是否是最后一道工序
		/// </summary>
		/// <param name="moCode"></param>
		/// <param name="routeCode"></param>
		/// <param name="opCode"></param>
		/// <returns></returns>
		private bool IsLastOP(string moCode,string routeCode,string opCode)
		{
			if (routeCode==string.Empty)
				return false;
			DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);

			return dataCollectFacade.OPIsMORouteLastOP(moCode,routeCode,opCode);
		}

		private void cbxOutLine_CheckBoxCheckedChanged(object sender, EventArgs e)
		{
			checkBox1.Enabled = cbxOutLine.Checked;
			checkBox1.Checked = cbxOutLine.Checked;
		}

		private void txtGOMO_CheckBoxCheckedChanged(object sender, System.EventArgs e)
		{
			if(txtGOMO.Checked == false)
			{
				this.bRCardLenULE.Value = String.Empty;
				this.bRCardLetterULE.Value = String.Empty;

				this.bRCardLenULE.Enabled = false;
				this.bRCardLetterULE.Enabled = false;
			}
			if(txtGOMO.Checked == true)
			{
				this.bRCardLenULE.Enabled = true;
				this.bRCardLetterULE.Enabled = true;
			}
		}

		private void txtGOMO_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				txtRunningCard.TextFocus(false, true);
				//SendKeys.Send("+{TAB}");
                //Remove UCLabel.SelectAll;
			}
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

				axMSComm1.CDTimeout = 10;
				axMSComm1.CTSTimeout = 10;
				axMSComm1.DSRTimeout = 10;
			}
			catch(Exception ex)
			{
				ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$MSCOMM_Init_Error"));//COM设备初始化失败
			}
		}

		private void ResetMSComm()
		{
			try
			{
				axMSComm1.Output = "rems\r";
				axMSComm1.Output = "*RST\r";
				axMSComm1.Output = "*CLS\r";
				axMSComm1.Output = "ADC\r";
			}
			catch(Exception ex)
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
             
			int i = 0;
			while(axMSComm1.InBufferCount<20)//一定要等待到InBufferCount<22这个长度才行
			{
				i++;
				if(i > 1200000)
				{
					break;
				}
					
			}

			

			string input = axMSComm1.Input.ToString().Trim();
			this.label2.Text = input;

			//将缓冲区清空
			axMSComm1.InBufferCount = 0;
			axMSComm1.OutBufferCount = 0;

			return input;
		}

		private string GetElectricCurrent(string input)
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
					string result = input.Substring(index+1,input.Length - index - 1 - 2);

					this.label3.Text = result;
					return Convert.ToDecimal(double.Parse(result)).ToString();
				}
				else
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$Cannot_Get_Electric_Current"));//无法获取电流值

					return "";
				}

//				return "";
			}
			catch(Exception ex)
			{
				ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$Cannot_Get_Electric_Current"));//无法获取电流值
			}

			return "";
		}

		private DeviceInterfaceFacade _DeviceInterfaceFacade = null;

		private DeviceInterfaceFacade DeviceFacade
		{
			get
			{
				if(_DeviceInterfaceFacade == null)
					_DeviceInterfaceFacade = new DeviceInterfaceFacade(DataProvider);

				return _DeviceInterfaceFacade;
			}
		}

		private void btnGetElectricCurrent_Click(object sender, System.EventArgs e)
		{
			ResetRadioButton();

			try
			{
				string input = GetElectricCurrentFromCOMM();

				this.txtElectric.Value = GetElectricCurrent(input);
			}
			catch(Exception ex)
			{
				ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$Cannot_Get_Electric_Current"));//无法获取电流值
			}

			if(this.txtElectric.Value == "")
			{
				//Application.DoEvents();
				txtElectric.TextFocus(false, true);
				return;
			}

			try
			{
				JudgePassOrFail();
			}
			catch
			{
				ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$Electric_Must_Be_Numberic"));//电流值必须是数值

				//Application.DoEvents();
				txtElectric.TextFocus(false, true);
				return;
			}

			//如果是Good就直接保存
			if(this.rdoGood.Checked)
			{
				this.btnSave_Click(sender,e);
			}
		}

		private void JudgePassOrFail()
		{
			double electric = double.Parse(this.txtElectric.Value);
			double maxValue = double.Parse(this.txtMaxValue.Value);
			double minValue = double.Parse(this.txtMinValue.Value);

			if(electric >= minValue && electric <= maxValue)
			{
				this.lblResult.Text = "PASS";
				this.lblResult.ForeColor = System.Drawing.Color.Green;
				this.rdoGood.Checked = true;
				this.rdoNG.Checked = false;
			}
			else
			{
				this.lblResult.Text = "FAIL";
				this.lblResult.ForeColor = System.Drawing.Color.Red;
				this.rdoNG.Checked = true;
				this.rdoGood.Checked = false;
				InitErrorCodeList();

			}
			
		}

		/// <summary>
		/// 显示标准电流值
		/// </summary>
		/// <param name="ItemCode"></param>
		private void GetItemStandardElectricCurrent(string ItemCode)
		{
			ItemFacade facade = new ItemFacade(this.DataProvider);

            Item item = (Item)facade.GetItem(ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
			this.txtMaxValue.Value = item.ElectricCurrentMaxValue.ToString();
			this.txtMinValue.Value = item.ElectricCurrentMinValue.ToString();
		}

		private void ResetRadioButton()
		{
			this.rdoNG.Checked = false ;
			this.rdoGood.Checked = false;
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			this.PrepareMSComm();
			this.ResetMSComm();
		}

		private void ucButton1_Click(object sender, System.EventArgs e)
		{
			ResetRadioButton();

			try
			{
				//string input = GetElectricCurrentFromCOMM();

				string input = "MEAS?+0.002E+3=>";

				this.txtElectric.Value = GetElectricCurrent(input);
			}
			catch(Exception ex)
			{
				ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$Cannot_Get_Electric_Current"));//无法获取电流值
			}

			if(this.txtElectric.Value == "")
				return;

			try
			{
//				double electric = double.Parse(this.txtElectric.Value);
//				double maxValue = double.Parse(this.txtMaxValue.Value);
//				double minValue = double.Parse(this.txtMinValue.Value);

				JudgePassOrFail();

//				if(electric >= minValue && electric <= maxValue)
//				{
//					this.txtResult.Value = "PASS";
//					this.rdoGood.Checked = true;
//					this.rdoNG.Checked = false;
//				}
//				else
//				{
//					this.txtResult.Value = "FAILED";
//					this.rdoNG.Checked = true;
//					this.rdoGood.Checked = false;
//				}
			}
			catch
			{
				ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$Electric_Must_Be_Numberic"));//电流值必须是数值

				return;
			}
		}

		private void ucButton2_Click(object sender, System.EventArgs e)
		{
			ResetRadioButton();

			try
			{
				//string input = GetElectricCurrentFromCOMM();

				string input = "MEAS?+0.000E+3=>";

				this.txtElectric.Value = GetElectricCurrent(input);
			}
			catch(Exception ex)
			{
				ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$Cannot_Get_Electric_Current"));//无法获取电流值
			}

			if(this.txtElectric.Value == "")
				return;

			try
			{
//				double electric = double.Parse(this.txtElectric.Value);
//				double maxValue = double.Parse(this.txtMaxValue.Value);
//				double minValue = double.Parse(this.txtMinValue.Value);

				JudgePassOrFail();

//				if(electric >= minValue && electric <= maxValue)
//				{
//					this.txtResult.Value = "PASS";
//					this.rdoGood.Checked = true;
//					this.rdoNG.Checked = false;
//				}
//				else
//				{
//					this.txtResult.Value = "FAILED";
//					this.rdoNG.Checked = true;
//					this.rdoGood.Checked = false;
//				}
			}
			catch
			{
				ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$Electric_Must_Be_Numberic"));//电流值必须是数值

				return;
			}
		}



		private void txtElectric_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				this.btnGetElectricCurrent_Click(null,null);
			}
		}

		private void textBox1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				this.btnGetElectricCurrent_Click(null,null);
			}
		}

		private void txtElectric_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				this.btnGetElectricCurrent_Click(null,null);
			}
		}

		private void errorCodeSelect_EndErrorCodeInput(object sender, System.EventArgs e)
		{
			btnSave_Click(sender,e);

			//Laws Lu,2006/06/07	执行后初始化界面显示
			ClearFormMessages();
//			rdoGood.Checked = true;
			errorCodeSelect.ErrorInpuTextBox.Text = String.Empty;
			txtRunningCard.TextFocus(true, true);
//			rdoGood_Click(sender,e);
		}


	}
}
