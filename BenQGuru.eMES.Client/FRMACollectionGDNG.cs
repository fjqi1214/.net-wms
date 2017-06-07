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
using BenQGuru.eMES.Domain.ATE;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;

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
	public class FRMACollectionGDNG : System.Windows.Forms.Form
	{
		private const string ng_collect = ActionType.DataCollectAction_NG;
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
		private UserControl.UCErrorCodeSelect errorCodeSelect;
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
		private System.Windows.Forms.CheckBox chkAutoGetData;
		private UserControl.UCLabelEdit txtCarton;
		private System.Windows.Forms.CheckBox ckLockErrorCode;
		private double iNG = 0;
		public FRMACollectionGDNG()
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
			//errorCodeSelect.EndErrorCodeInput += new EventHandler(errorCodeSelect_EndErrorCodeInput);

			txtMem.AutoChange();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMACollectionGDNG));
            this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
            this.panelButton = new System.Windows.Forms.Panel();
            this.txtCarton = new UserControl.UCLabelEdit();
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
            this.chkAutoGetData = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckLockErrorCode = new System.Windows.Forms.CheckBox();
            this.txtMO = new UserControl.UCLabelEdit();
            this.txtItem = new UserControl.UCLabelEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorCodeSelect = new UserControl.UCErrorCodeSelect();
            this.panelButton.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraLabel6
            // 
            this.ultraLabel6.Location = new System.Drawing.Point(0, 0);
            this.ultraLabel6.Name = "ultraLabel6";
            this.ultraLabel6.Size = new System.Drawing.Size(100, 23);
            this.ultraLabel6.TabIndex = 0;
            // 
            // panelButton
            // 
            this.panelButton.Controls.Add(this.txtCarton);
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
            this.panelButton.Location = new System.Drawing.Point(0, 468);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(904, 97);
            this.panelButton.TabIndex = 155;
            // 
            // txtCarton
            // 
            this.txtCarton.AllowEditOnlyChecked = true;
            this.txtCarton.Caption = "Carton箱号";
            this.txtCarton.Checked = false;
            this.txtCarton.EditType = UserControl.EditTypes.String;
            this.txtCarton.Location = new System.Drawing.Point(209, 37);
            this.txtCarton.MaxLength = 40;
            this.txtCarton.Multiline = false;
            this.txtCarton.Name = "txtCarton";
            this.txtCarton.PasswordChar = '\0';
            this.txtCarton.ReadOnly = false;
            this.txtCarton.ShowCheckBox = false;
            this.txtCarton.Size = new System.Drawing.Size(273, 24);
            this.txtCarton.TabIndex = 9;
            this.txtCarton.TabNext = true;
            this.txtCarton.Value = "";
            this.txtCarton.WidthType = UserControl.WidthTypes.Long;
            this.txtCarton.XAlign = 282;
            this.txtCarton.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCarton_TxtboxKeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(181, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "%";
            // 
            // lblNotYield
            // 
            this.lblNotYield.AllowEditOnlyChecked = true;
            this.lblNotYield.Caption = "不良率";
            this.lblNotYield.Checked = false;
            this.lblNotYield.EditType = UserControl.EditTypes.Number;
            this.lblNotYield.Location = new System.Drawing.Point(26, 67);
            this.lblNotYield.MaxLength = 40;
            this.lblNotYield.Multiline = false;
            this.lblNotYield.Name = "lblNotYield";
            this.lblNotYield.PasswordChar = '\0';
            this.lblNotYield.ReadOnly = true;
            this.lblNotYield.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblNotYield.ShowCheckBox = false;
            this.lblNotYield.Size = new System.Drawing.Size(149, 24);
            this.lblNotYield.TabIndex = 7;
            this.lblNotYield.TabNext = false;
            this.lblNotYield.Value = "0";
            this.lblNotYield.WidthType = UserControl.WidthTypes.Small;
            this.lblNotYield.XAlign = 75;
            // 
            // txtMem
            // 
            this.txtMem.AllowEditOnlyChecked = true;
            this.txtMem.Caption = "备注";
            this.txtMem.Checked = false;
            this.txtMem.EditType = UserControl.EditTypes.String;
            this.txtMem.Location = new System.Drawing.Point(520, 6);
            this.txtMem.MaxLength = 80;
            this.txtMem.Multiline = true;
            this.txtMem.Name = "txtMem";
            this.txtMem.PasswordChar = '\0';
            this.txtMem.ReadOnly = false;
            this.txtMem.ShowCheckBox = false;
            this.txtMem.Size = new System.Drawing.Size(237, 67);
            this.txtMem.TabIndex = 3;
            this.txtMem.TabNext = true;
            this.txtMem.Value = "";
            this.txtMem.WidthType = UserControl.WidthTypes.Long;
            this.txtMem.XAlign = 557;
            // 
            // txtRunningCard
            // 
            this.txtRunningCard.AllowEditOnlyChecked = true;
            this.txtRunningCard.Caption = "产品序列号";
            this.txtRunningCard.Checked = false;
            this.txtRunningCard.EditType = UserControl.EditTypes.String;
            this.txtRunningCard.Location = new System.Drawing.Point(209, 7);
            this.txtRunningCard.MaxLength = 40;
            this.txtRunningCard.Multiline = false;
            this.txtRunningCard.Name = "txtRunningCard";
            this.txtRunningCard.PasswordChar = '\0';
            this.txtRunningCard.ReadOnly = false;
            this.txtRunningCard.ShowCheckBox = false;
            this.txtRunningCard.Size = new System.Drawing.Size(273, 24);
            this.txtRunningCard.TabIndex = 2;
            this.txtRunningCard.TabNext = true;
            this.txtRunningCard.Value = "";
            this.txtRunningCard.WidthType = UserControl.WidthTypes.Long;
            this.txtRunningCard.XAlign = 282;
            this.txtRunningCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRunningCard_TxtboxKeyPress);
            // 
            // rdoNG
            // 
            this.rdoNG.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoNG.ForeColor = System.Drawing.Color.Red;
            this.rdoNG.Location = new System.Drawing.Point(87, 7);
            this.rdoNG.Name = "rdoNG";
            this.rdoNG.Size = new System.Drawing.Size(80, 23);
            this.rdoNG.TabIndex = 6;
            this.rdoNG.Tag = "1";
            this.rdoNG.Text = "不良品";
            this.rdoNG.Click += new System.EventHandler(this.rdoNG_Click);
            this.rdoNG.CheckedChanged += new System.EventHandler(this.rdoNG_CheckedChanged);
            // 
            // rdoGood
            // 
            this.rdoGood.Checked = true;
            this.rdoGood.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoGood.ForeColor = System.Drawing.Color.Blue;
            this.rdoGood.Location = new System.Drawing.Point(7, 7);
            this.rdoGood.Name = "rdoGood";
            this.rdoGood.Size = new System.Drawing.Size(66, 23);
            this.rdoGood.TabIndex = 5;
            this.rdoGood.TabStop = true;
            this.rdoGood.Tag = "1";
            this.rdoGood.Text = "良品";
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
            this.btnExit.Location = new System.Drawing.Point(385, 67);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.ButtonType = UserControl.ButtonTypes.Save;
            this.btnSave.Caption = "保存";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(280, 67);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 22);
            this.btnSave.TabIndex = 4;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // CollectedCount
            // 
            this.CollectedCount.AllowEditOnlyChecked = true;
            this.CollectedCount.Caption = "已采集数量";
            this.CollectedCount.Checked = false;
            this.CollectedCount.EditType = UserControl.EditTypes.Integer;
            this.CollectedCount.Location = new System.Drawing.Point(2, 37);
            this.CollectedCount.MaxLength = 40;
            this.CollectedCount.Multiline = false;
            this.CollectedCount.Name = "CollectedCount";
            this.CollectedCount.PasswordChar = '\0';
            this.CollectedCount.ReadOnly = true;
            this.CollectedCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CollectedCount.ShowCheckBox = false;
            this.CollectedCount.Size = new System.Drawing.Size(173, 24);
            this.CollectedCount.TabIndex = 0;
            this.CollectedCount.TabNext = false;
            this.CollectedCount.Value = "0";
            this.CollectedCount.WidthType = UserControl.WidthTypes.Small;
            this.CollectedCount.XAlign = 75;
            // 
            // txtGOMO
            // 
            this.txtGOMO.AllowEditOnlyChecked = true;
            this.txtGOMO.Caption = "设定归属工单";
            this.txtGOMO.Checked = false;
            this.txtGOMO.EditType = UserControl.EditTypes.String;
            this.txtGOMO.Location = new System.Drawing.Point(263, 15);
            this.txtGOMO.MaxLength = 40;
            this.txtGOMO.Multiline = false;
            this.txtGOMO.Name = "txtGOMO";
            this.txtGOMO.PasswordChar = '\0';
            this.txtGOMO.ReadOnly = false;
            this.txtGOMO.ShowCheckBox = true;
            this.txtGOMO.Size = new System.Drawing.Size(234, 24);
            this.txtGOMO.TabIndex = 1;
            this.txtGOMO.TabNext = true;
            this.txtGOMO.Value = "";
            this.txtGOMO.WidthType = UserControl.WidthTypes.Normal;
            this.txtGOMO.XAlign = 364;
            this.txtGOMO.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGOMO_TxtboxKeyPress);
            this.txtGOMO.CheckBoxCheckedChanged += new System.EventHandler(this.txtGOMO_CheckBoxCheckedChanged);
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
            this.groupBox2.Controls.Add(this.chkAutoGetData);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(904, 100);
            this.groupBox2.TabIndex = 157;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "流程外工位指定";
            // 
            // bRCardLetterULE
            // 
            this.bRCardLetterULE.AllowEditOnlyChecked = true;
            this.bRCardLetterULE.Caption = "产品序列号首字符串";
            this.bRCardLetterULE.Checked = false;
            this.bRCardLetterULE.EditType = UserControl.EditTypes.String;
            this.bRCardLetterULE.Enabled = false;
            this.bRCardLetterULE.Location = new System.Drawing.Point(529, 45);
            this.bRCardLetterULE.MaxLength = 40;
            this.bRCardLetterULE.Multiline = false;
            this.bRCardLetterULE.Name = "bRCardLetterULE";
            this.bRCardLetterULE.PasswordChar = '\0';
            this.bRCardLetterULE.ReadOnly = false;
            this.bRCardLetterULE.ShowCheckBox = true;
            this.bRCardLetterULE.Size = new System.Drawing.Size(270, 24);
            this.bRCardLetterULE.TabIndex = 28;
            this.bRCardLetterULE.TabNext = false;
            this.bRCardLetterULE.Value = "";
            this.bRCardLetterULE.WidthType = UserControl.WidthTypes.Normal;
            this.bRCardLetterULE.XAlign = 666;
            // 
            // bRCardLenULE
            // 
            this.bRCardLenULE.AllowEditOnlyChecked = true;
            this.bRCardLenULE.Caption = "产品序列号长度    ";
            this.bRCardLenULE.Checked = false;
            this.bRCardLenULE.EditType = UserControl.EditTypes.Integer;
            this.bRCardLenULE.Enabled = false;
            this.bRCardLenULE.Location = new System.Drawing.Point(529, 15);
            this.bRCardLenULE.MaxLength = 40;
            this.bRCardLenULE.Multiline = false;
            this.bRCardLenULE.Name = "bRCardLenULE";
            this.bRCardLenULE.PasswordChar = '\0';
            this.bRCardLenULE.ReadOnly = false;
            this.bRCardLenULE.ShowCheckBox = true;
            this.bRCardLenULE.Size = new System.Drawing.Size(270, 24);
            this.bRCardLenULE.TabIndex = 27;
            this.bRCardLenULE.TabNext = false;
            this.bRCardLenULE.Value = "";
            this.bRCardLenULE.WidthType = UserControl.WidthTypes.Normal;
            this.bRCardLenULE.XAlign = 666;
            // 
            // checkBox1
            // 
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(26, 45);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(121, 24);
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
            this.edtSoftName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edtSoftName.Location = new System.Drawing.Point(263, 73);
            this.edtSoftName.MaxLength = 40;
            this.edtSoftName.Multiline = false;
            this.edtSoftName.Name = "edtSoftName";
            this.edtSoftName.PasswordChar = '\0';
            this.edtSoftName.ReadOnly = false;
            this.edtSoftName.ShowCheckBox = true;
            this.edtSoftName.Size = new System.Drawing.Size(234, 24);
            this.edtSoftName.TabIndex = 9;
            this.edtSoftName.TabNext = true;
            this.edtSoftName.Value = "";
            this.edtSoftName.WidthType = UserControl.WidthTypes.Normal;
            this.edtSoftName.XAlign = 364;
            // 
            // edtSoftInfo
            // 
            this.edtSoftInfo.AllowEditOnlyChecked = true;
            this.edtSoftInfo.Caption = "采集软件版本";
            this.edtSoftInfo.Checked = false;
            this.edtSoftInfo.EditType = UserControl.EditTypes.String;
            this.edtSoftInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edtSoftInfo.Location = new System.Drawing.Point(263, 45);
            this.edtSoftInfo.MaxLength = 40;
            this.edtSoftInfo.Multiline = false;
            this.edtSoftInfo.Name = "edtSoftInfo";
            this.edtSoftInfo.PasswordChar = '\0';
            this.edtSoftInfo.ReadOnly = false;
            this.edtSoftInfo.ShowCheckBox = true;
            this.edtSoftInfo.Size = new System.Drawing.Size(234, 24);
            this.edtSoftInfo.TabIndex = 8;
            this.edtSoftInfo.TabNext = true;
            this.edtSoftInfo.Value = "";
            this.edtSoftInfo.WidthType = UserControl.WidthTypes.Normal;
            this.edtSoftInfo.XAlign = 364;
            this.edtSoftInfo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtSoftInfo_TxtboxKeyPress);
            this.edtSoftInfo.CheckBoxCheckedChanged += new System.EventHandler(this.edtSoftInfo_CheckBoxCheckedChanged);
            // 
            // cbxOutLine
            // 
            this.cbxOutLine.AllowEditOnlyChecked = true;
            this.cbxOutLine.Caption = "线外工序";
            this.cbxOutLine.Checked = false;
            this.cbxOutLine.Location = new System.Drawing.Point(26, 15);
            this.cbxOutLine.Name = "cbxOutLine";
            this.cbxOutLine.SelectedIndex = -1;
            this.cbxOutLine.ShowCheckBox = true;
            this.cbxOutLine.Size = new System.Drawing.Size(212, 24);
            this.cbxOutLine.TabIndex = 0;
            this.cbxOutLine.WidthType = UserControl.WidthTypes.Normal;
            this.cbxOutLine.XAlign = 105;
            this.cbxOutLine.CheckBoxCheckedChanged += new System.EventHandler(this.cbxOutLine_CheckBoxCheckedChanged);
            // 
            // chkAutoGetData
            // 
            this.chkAutoGetData.Location = new System.Drawing.Point(529, 73);
            this.chkAutoGetData.Name = "chkAutoGetData";
            this.chkAutoGetData.Size = new System.Drawing.Size(141, 24);
            this.chkAutoGetData.TabIndex = 18;
            this.chkAutoGetData.Text = "自动从设备获取数据";
            this.chkAutoGetData.CheckedChanged += new System.EventHandler(this.chkAutoGetData_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckLockErrorCode);
            this.groupBox1.Controls.Add(this.txtMO);
            this.groupBox1.Controls.Add(this.txtItem);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(904, 52);
            this.groupBox1.TabIndex = 158;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "产品信息";
            // 
            // ckLockErrorCode
            // 
            this.ckLockErrorCode.Location = new System.Drawing.Point(463, 22);
            this.ckLockErrorCode.Name = "ckLockErrorCode";
            this.ckLockErrorCode.Size = new System.Drawing.Size(103, 24);
            this.ckLockErrorCode.TabIndex = 4;
            this.ckLockErrorCode.Text = "锁定不良代码";
            // 
            // txtMO
            // 
            this.txtMO.AllowEditOnlyChecked = true;
            this.txtMO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMO.Caption = "工单";
            this.txtMO.Checked = false;
            this.txtMO.EditType = UserControl.EditTypes.String;
            this.txtMO.Location = new System.Drawing.Point(263, 22);
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
            this.txtMO.XAlign = 300;
            // 
            // txtItem
            // 
            this.txtItem.AllowEditOnlyChecked = true;
            this.txtItem.Caption = "产品";
            this.txtItem.Checked = false;
            this.txtItem.EditType = UserControl.EditTypes.String;
            this.txtItem.Location = new System.Drawing.Point(45, 22);
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
            this.txtItem.XAlign = 82;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.errorCodeSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 152);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(904, 316);
            this.panel1.TabIndex = 159;
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
            this.errorCodeSelect.Size = new System.Drawing.Size(904, 316);
            this.errorCodeSelect.TabIndex = 0;
            this.errorCodeSelect.EndErrorCodeInput += new System.EventHandler(this.errorCodeSelect_EndErrorCodeInput);
            this.errorCodeSelect.ErrorCodeKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.errorCodeSelect_ErrorCodeKeyPress);
            this.errorCodeSelect.Resize += new System.EventHandler(this.errorCodeSelect_Resize);
            this.errorCodeSelect.SelectedIndexChanged += new System.EventHandler(this.errorCodeSelect_SelectedIndexChanged);
            // 
            // FRMACollectionGDNG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(904, 565);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelButton);
            this.KeyPreview = true;
            this.Name = "FRMACollectionGDNG";
            this.Text = "良品/不良品采集";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FCollectionGDNG_Load);
            this.Closed += new System.EventHandler(this.FCollectionGDNG_Closed);
            this.Activated += new System.EventHandler(this.FCollectionGDNG_Activated);
            this.panelButton.ResumeLayout(false);
            this.panelButton.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
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
					//Amoi,Laws Lu,2005/08/02,注释
					//messages.Add(new UserControl.Message(MessageType.Normal ,"$CS_CHECKSUCCESS"));
					//EndAmoi
				}
				else if (txtGOMO.Value == string.Empty)
				{
					txtItem.Value = String.Empty;
					txtMO.Value = String.Empty;

					txtRunningCard.Value = "";
					//messages.Add(new UserControl.Message(MessageType.Error ,"$CS_MUSTGOMO"));
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
					txtRunningCard.TextFocus(false, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");
                    //Remove UCLabel.SelectAll;
					return;
					
				}		
				else
				{
					if( txtRunningCard.Value.Trim().ToUpper() == ng_collect)
					{
						rdoNG.Checked = true;
						errorCodeSelect.Enabled = true;

						txtRunningCard.TextFocus(true, true);
						//System.Windows.Forms.SendKeys.Send("+{TAB}");
                        //Remove UCLabel.SelectAll;

						return;
					}
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

					#region ATE数据连线
					/* Added by jessie lee, 2006-6-6, ATE数据连线 */
					if( chkAutoGetData.Checked )
					{
						ATEFacade ateFacade = new ATEFacade( this.DataProvider );

						if(product.LastSimulation == null)
						{
							ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error ,"$NoSimulation"));
							txtRunningCard.Enabled = true;
							txtRunningCard.TextFocus(true, true);
							//System.Windows.Forms.SendKeys.Send("+{TAB}");
                            //Remove UCLabel.SelectAll;
							return;
						}

						object[] atedatas = ateFacade.GetATETestInfoByRCard( product.LastSimulation.RunningCard, Resource.ResourceCode );

						if( atedatas==null || atedatas.Length==0)
						{
							ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, "$CS_ATE_DONNOT_HAVE_DATA"));
							txtRunningCard.TextFocus(false, true);
							//System.Windows.Forms.SendKeys.Send("+{TAB}");
                            //Remove UCLabel.SelectAll;
							return;
						}
						else if( atedatas.Length > 1 )
						{
							ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, ""));
							txtRunningCard.TextFocus(false, true);
							//System.Windows.Forms.SendKeys.Send("+{TAB}");
                            //Remove UCLabel.SelectAll;
							return;
						}

						ATETestInfo ateTestInfo = atedatas[0] as ATETestInfo;

						//资源信息从设备中获取
						//						BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
						//						Resource = (Domain.BaseSetting.Resource)dataModel.GetResource( ateTestInfo.ResCode );
						//						actionCheckStatus.ProductInfo.Resource = Resource;

						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
						DataProvider.BeginTransaction();

						try
						{
							if( string.Compare( ateTestInfo.TestResult, "OK", true)==0 )
							{
								globeMSG = RunGood( actionCheckStatus, ateTestInfo );
								ApplicationRun.GetInfoForm().Add(globeMSG);
							}
							else if( string.Compare( ateTestInfo.TestResult, "NG", true)==0 )
							{
								globeMSG = RunNG( actionCheckStatus, ateTestInfo );
								ApplicationRun.GetInfoForm().Add(globeMSG);
							}
							else
							{
								ApplicationRun.GetInfoForm().Add( new UserControl.Message(MessageType.Error, ""));
							}

							if (globeMSG.IsSuccess())
							{
								ateFacade.DeleteATETestInfo( ateTestInfo );

								DataProvider.CommitTransaction();
								AddCollectedCount();
							}
							else
							{
								txtRunningCard.Enabled = true;
								DataProvider.RollbackTransaction();
							}
						}
						catch( Exception ex )
						{
							DataProvider.RollbackTransaction();
							globeMSG.Add(new UserControl.Message(ex));
							ApplicationRun.GetInfoForm().Add(globeMSG);
						}
						finally
						{
							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
						}

						txtRunningCard.Enabled = true;
						txtRunningCard.TextFocus(true, true);
						//System.Windows.Forms.SendKeys.Send("+{TAB}");
                        //Remove UCLabel.SelectAll;

						return;
					}
					#endregion

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

					//Amoi,Laws Lu,2005/08/02,新增 如果良品RadioBox被选中则直接保存
					//Laws Lu,2005/08/06,修改	Lucky提出工单归属失败也可以做下面的逻辑
					if(/*msg.IsSuccess() && */rdoGood.Checked == true)
					{
						btnSave_Click(sender,e);

						//将焦点移到产品序列号输入框
						//						txtRunningCard.TextFocus(false, true);
						//						System.Windows.Forms.SendKeys.Send("+{TAB}");
						
					}
					else if (/*msg.IsSuccess() && */rdoNG.Checked == true)
					{
						//清除ErrorCode列表
						if(!this.ckLockErrorCode.Checked || (this.ckLockErrorCode.Checked && errorCodeSelect.ErrorGroupCount == 0))
						{

							//errorCodeSelect.ClearErrorGroup();
							//errorCodeSelect.ClearSelectErrorCode();
							errorCodeSelect.ClearSelectedErrorCode();

							if(SetErrorCodeList())
							{
                                errorCodeSelect.Focus();

								if(errorCodeSelect.ErrorGroupCount < 1)
								{
									globeMSG.Add(new UserControl.Message(MessageType.Error, "$CS_MUST_MAINTEN_ERRGROUP"));

									txtRunningCard.Value = "";
								
								}

								btnSave.Enabled = true;
							}
						}
						else// if(this.ckLockErrorCode.Checked)
						{
							GetProduct();

							if (product.LastSimulation ==null )
							{
							
								globeMSG.Add(new UserControl.Message(MessageType.Error,"$NoSimulation"));

								txtItem.Value = String.Empty;
								txtMO.Value = String.Empty;
								//Laws Lu,2005/08/16
								//return false;
							}

                            errorCodeSelect.Focus();
							
						}
						
					}
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

				//Application.DoEvents();
				txtRunningCard.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
				//System.Windows.Forms.SendKeys.Send("+{TAB}");

				if (rdoNG.Checked == true && globeMSG.IsSuccess())
				{
					errorCodeSelect.ErrorInpuTextBox.Text = String.Empty;
                    errorCodeSelect.ErrorInpuTextBox.Focus();
				}

				globeMSG.ClearMessages();

				

				
				//e.Handled = true;
			}
			else
			{
				btnSave.Enabled = false;
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
			}

			if(!messages.IsSuccess() && rdoGood.Checked == true)//Amoi,Laws Lu,2005/08/02,新增	失败时Save按钮状态为False
			{
				txtRunningCard.Value = String.Empty;
				btnSave.Enabled = false;
			}
			else if (!messages.IsSuccess() && rdoNG.Checked == true)//Amoi,Laws Lu,2005/08/10,新增	失败时Save按钮状态为True
			{
				txtRunningCard.Value = String.Empty;
				txtRunningCard.TextFocus(false, true);

				btnSave.Enabled = true;
			}
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

		private void errorCodeSelect_ErrorCodeKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//errorCodeSelect.
			//Laws Lu,2006/07/04 modify support get error group by input error code
			if(e != null && e.KeyChar == '\r')
			{
				TSModelFacade tsModelFac = new TSModelFacade(DataProvider);
				object objECG = tsModelFac.
					GetErrorCodeGroup2ErrorCodeByecCode(errorCodeSelect.ErrorInpuTextBox.Text.ToUpper().Trim());
				if(objECG != null)
				{
					string ecgCode = errorCodeSelect.SelectedErrorCodeGroup;
					string newECGCode = (objECG as eMES.Domain.TSModel.ErrorCodeGroup2ErrorCode).ErrorCodeGroup;
					string eCode = (objECG as eMES.Domain.TSModel.ErrorCodeGroup2ErrorCode).ErrorCode;
					
					if(newECGCode != ecgCode)
					{
						errorCodeSelect.SelectedErrorCodeGroup = (objECG as eMES.Domain.TSModel.ErrorCodeGroup2ErrorCode).ErrorCodeGroup;

						ErrorCodeGroup2ErrorCode ecg2ec = new ErrorCodeGroup2ErrorCode();
						ecg2ec.ErrorCodeGroup = newECGCode;

						object objEc = tsModelFac.GetErrorCode(eCode);

						if(objEc != null)
						{

							ErrorCodeA ecA =  objEc as ErrorCodeA;
//							string[] errCode = errorCodeSelect.ErrorCodeListControl.SelectedItem.ToString().Split(new char[]{' '});
//
//							string ec = String.Empty;
//							string en = String.Empty;
//
//							if(errCode.Length > 1)
//							{
//								ec = errCode[0];
//								en = errCode[1];
//							}
//							if(errCode.Length == 1)
//							{
//								ec = errCode[0];
//							}

							ecg2ec.ErrorCode = ecA.ErrorCode + " " + ecA.ErrorDescription;
						}
						else
						{

							ecg2ec.ErrorCode = eCode;
						}

						errorCodeSelect.AddSelectedErrorCodes(new object[]
						{
							ecg2ec
						});
					}
				}
			}
		}
		//2006/07/03 add by Laws Lu,support good/ng collect by carton
		private void txtCarton_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);	
				object[] objs = (new DataCollectFacade(DataProvider)).GetSimulationFromCarton(txtCarton.Value.ToUpper().Trim());

				if(objs != null && objs.Length > 0)
				{
					Messages msg = new Messages();
					DataProvider.BeginTransaction();
					try
					{
						#region 业务数据
						if(rdoGood.Checked)
						{
							foreach(object obj in objs)
							{
								Simulation sim = obj as Simulation;
								msg.AddMessages( dataCollect.GetIDInfo(sim.RunningCard));
								if (msg.IsSuccess() )
								{  
									product = (ProductInfo)msg.GetData().Values[0];					
								}
								else
								{
									msg.Add(new UserControl.Message(MessageType.Error,"$NoSimulation  $CS_Param_ID :" + sim.RunningCard));
								}
								//Run Good Action Collect
								if(msg.IsSuccess())
								{
									IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_GOOD);

									msg.AddMessages( dataCollectModule.Execute(new ActionEventArgs(ActionType.DataCollectAction_GOOD, sim.RunningCard,
										ApplicationService.Current().UserCode,
										ApplicationService.Current().ResourceCode,product)));

								}
								if(msg.IsSuccess())
								{
									msg.Add(new UserControl.Message(MessageType.Success ,string.Format("$CS_GOODSUCCESS,$CS_Param_ID:{0}",sim.RunningCard)));
								}//Laws Lu,2006/07/07 add ,if error than break loop
								else
								{
									break;
								}
							}
						}
						else if(rdoNG.Checked)
						{
//							foreach(object obj in objs)
//							{
//								object[] ErrorCodes = GetSelectedErrorCodes();//取不良代码组＋不良代码
//
//								Simulation sim = obj as Simulation;
//								msg.AddMessages( dataCollect.GetIDInfo(sim.RunningCard));
//								if (msg.IsSuccess() )
//								{  
//									product = (ProductInfo)msg.GetData().Values[0];					
//								}
//								else
//								{
//									msg.Add(new UserControl.Message(MessageType.Error,"$NoSimulation  $CS_Param_ID :" + sim.RunningCard));
//								}
//								//Run NG Action Collect
//								if(msg.IsSuccess())
//								{
//									IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_NG);
//
//									msg.AddMessages( dataCollectModule.Execute(
//										new TSActionEventArgs(ActionType.DataCollectAction_NG,
//										sim.RunningCard,
//										ApplicationService.Current().UserCode,
//										ApplicationService.Current().ResourceCode,
//										product,
//										ErrorCodes, 
//										null,
//										txtMem.Value)));
//								}
//
//								if(msg.IsSuccess())
//								{
//									msg.Add(new UserControl.Message(MessageType.Success ,string.Format("$CS_NGSUCCESS,$CS_Param_ID:{0}",sim.RunningCard)));
//								}
//								else//Laws Lu,2006/07/07 add ,if error than break loop
//								{
//									break;
//								}
//							}

						}
						#endregion

					}
					catch(Exception ex)
					{
						msg.Add(new UserControl.Message(ex));
					}
					finally
					{
						if(msg.IsSuccess())
						{
							DataProvider.CommitTransaction();
						}
						else
						{
							DataProvider.RollbackTransaction();
						}
						((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();

						ApplicationRun.GetInfoForm().Add(msg);
						//将焦点移到产品序列号输入框
						//Application.DoEvents();
						txtCarton.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;
						//System.Windows.Forms.SendKeys.Send("+{TAB}");

					}
					
				}
				else
				{
					Application.DoEvents();
					txtCarton.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
				}
				
			}
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
						if(txtRunningCard.Value.Trim().Length != len)
						{
							lenCheckBool = false;
							messages.Add(new UserControl.Message(MessageType.Error,"$CS_Before_Card_Length_FLetter_NotCompare $CS_Param_ID: " + txtRunningCard.Value.Trim())); 
						}
					}
					catch
					{
						messages.Add(new UserControl.Message(MessageType.Error,"$CS_Before_Card_Length_FLetter_NotCompare $CS_Param_ID: " + txtRunningCard.Value.Trim())); 
					}
				}

				//产品序列号首字符串检查
				if(bRCardLetterULE.Checked && bRCardLetterULE.Value.Trim() != string.Empty)
				{
					int index = -1;
					if(bRCardLetterULE.Value.Trim().Length <= txtRunningCard.Value.Trim().Length)
					{
						index = txtRunningCard.Value.IndexOf( bRCardLetterULE.Value.Trim());
					}
					if( index == -1 )
					{
						lenCheckBool = false;
						messages.Add(new UserControl.Message(MessageType.Error,"$CS_Before_Card_FLetter_NotCompare $CS_Param_ID: " + txtRunningCard.Value.Trim())); 
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
				messages.Add(new UserControl.Message(MessageType.Success ,"$CS_GOMOSUCCESS $CS_Param_ID: " + txtRunningCard.Value.Trim()));
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
			
			if(!this.ckLockErrorCode.Checked)
			{
				//errorCodeSelect.ClearErrorGroup();
				errorCodeSelect.ClearSelectedErrorCode();
				//errorCodeSelect.ClearSelectErrorCode();
			}

			btnSave.Enabled = false;
			
			InitialRunningCard();

			//如果线外工序被选中,则不需要重新初始化
			if(!cbxOutLine.Checked)
			{
				InitializeOutLineOP();
			}
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
				globeMSG.Add(new UserControl.Message(MessageType.Error,"$NoSimulation"));

				txtItem.Value = String.Empty;
				txtMO.Value = String.Empty;
				//Laws Lu,2005/08/16
				return false;
			}
			
			object[] errorCodeGroups = tsFacade.GetErrorCodeGroupByItemCode(strItem);
			if (errorCodeGroups!= null)
			{
				//if(errorCodeSelect.e
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

		private void errorCodeSelect_EndErrorCodeInput(object sender, EventArgs e)
		{
			btnSave_Click(sender,e);

			//Laws Lu,2006/06/07	执行后初始化界面显示
			ClearFormMessages();
			
			if(errorCodeSelect.ErrorInpuTextBox.Text.ToUpper() == UCErrorCodeSelect.ClipOK)
			{
				rdoGood.Checked = true;
				rdoGood_Click(sender,e);
				errorCodeSelect.ErrorInpuTextBox.Text = String.Empty;
			}
			else
			{
				

				rdoNG.Checked = true;
				rdoNG_Click(sender,e);
				errorCodeSelect.ErrorInpuTextBox.Text = String.Empty;
			}
			txtRunningCard.TextFocus(true, true);
		}

		private void chkAutoGetData_CheckedChanged(object sender, EventArgs e)
		{
			if(chkAutoGetData.Checked)
			{
				bool used = false;
				cbxOutLine.Checked = used ;
				cbxOutLine.Enabled = used ;

				checkBox1.Checked = used ;
				checkBox1.Enabled = used ;

				txtGOMO.Checked =  used ;
				txtGOMO.Enabled = used ;

				edtSoftInfo.Checked =  used ;
				edtSoftInfo.Enabled =  used ;

				edtSoftName.Checked =  used ;
				edtSoftName.Enabled =  used ;

				bRCardLenULE.Checked =  used ;
				bRCardLenULE.Enabled =  used ;

				bRCardLetterULE.Checked =  used ;
				bRCardLetterULE.Enabled =  used ;
			}
			else
			{
				bool used = true;
				cbxOutLine.Enabled = used;
				txtGOMO.Enabled = used ;
				edtSoftInfo.Enabled =  used ;
			}
		}

		/// <summary>
		/// GOOD指令采集
		/// </summary>
		/// <returns></returns>
		private Messages RunGood(ActionCheckStatus actionCheckStatus, ATETestInfo atetestinfo)
		{
			Messages messages = new Messages ();
			try
			{
				if(product.LastSimulation == null)
				{
					messages.Add(new UserControl.Message(MessageType.Error ,"$NoSimulation"));
					return messages;
				}

				IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_GOOD);

				actionCheckStatus.ProductInfo = product;

				messages.AddMessages( ((IActionWithStatus)dataCollectModule).Execute(
					new ActionEventArgs(
					ActionType.DataCollectAction_GOOD, 
					txtRunningCard.Value.Trim(),
					atetestinfo.MaintainUser,
					atetestinfo.ResCode,
					product),
					actionCheckStatus));

				if (messages.IsSuccess())
				{
					messages.Add(new UserControl.Message(MessageType.Success ,string.Format("$CS_GOODSUCCESS,$CS_Param_ID:{0}",txtRunningCard.Value.Trim())));
				}

				return messages;
			}
			catch (Exception e)
			{
				messages.Add(new UserControl.Message(e));
				return messages;
			}
		}

		private Messages RunNG(ActionCheckStatus actionCheckStatus, ATETestInfo atetestinfo)
		{
			Messages messages=new Messages ();
			try
			{
				if(product.LastSimulation == null)
				{
					messages.Add(new UserControl.Message(MessageType.Error ,"$NoSimulation"));
					return messages;
				}

				ATEFacade ateFacade = new ATEFacade( this.DataProvider );
				object[] errorinfor =  ateFacade.GetErrorInfo( atetestinfo, actionCheckStatus.ProductInfo.LastSimulation.ModelCode );

				actionCheckStatus.ProductInfo = product;

				IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_SMTNG);
				messages.AddMessages( ((IActionWithStatus)dataCollectModule).Execute(
					new TSActionEventArgs(ActionType.DataCollectAction_SMTNG,
					txtRunningCard.Value.Trim(),
					atetestinfo.MaintainUser,
					atetestinfo.ResCode,
					product,
					errorinfor, 
					txtMem.Value),actionCheckStatus));
				
				if (messages.IsSuccess())
				{
					messages.Add(new UserControl.Message(MessageType.Success ,string.Format("$CS_NGSUCCESS,$CS_Param_ID:{0}",txtRunningCard.Value.Trim())));
				}
				return messages;
			}
			catch (Exception e)
			{
				messages.Add(new UserControl.Message(e));
				return messages;
			}
		}



	}
}
