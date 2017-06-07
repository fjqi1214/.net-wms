#region system
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System.Data;
#endregion

#region Project
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
#endregion


namespace BenQGuru.eMES.Client
{
	public class FGenLotIDMerge : Form
	{
		private int CurrentSNCount = 1;
		private int TotalSNCount = 1;
		//包装检查
		private string cartonPreffix = String.Empty;

		private int cartonCodeLen = 0;

		private int SNStartPosition = 0;
		private int SNEndPosition = 0;

		private ArrayList rcards = new ArrayList();
		private ArrayList rcardsBefore = new ArrayList();

		private UserControl.UCLabelEdit ucLabEdit2;
		private System.ComponentModel.IContainer components = null;
		private UserControl.UCLabelEdit ucLabelEdit1;


		private string itemCode= string.Empty;
		private string moCode = string.Empty;
		private decimal oqcMaxSize = 0;
		private DataTable dtIDList = new DataTable();
		private System.Windows.Forms.Panel panel1;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
		private System.Windows.Forms.GroupBox groupBox1;
		public UserControl.UCLabelEdit txtCarton;
		private UserControl.UCButton uBtnExit;
		private Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetOQCExameOpion;
		private Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetOQCType;
		public UserControl.UCLabelEdit ucLabEditMaxNumber;
		private System.Windows.Forms.TextBox txtLength;
		private System.Windows.Forms.CheckBox cbRemixMO;
		public UserControl.UCLabelEdit ucLabEditOQCLot;
		private System.Windows.Forms.CheckBox cbCheckLength;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private UserControl.UCButton ucButton3;
		private UserControl.UCButton ucButton2;
		private System.Windows.Forms.CheckBox chkCodex;
		private UserControl.UCButton btnEnable;
		public UserControl.UCLabelEdit txtCartonCapacity;
		private UserControl.UCLabelEdit txtLotActualQty;
		private System.Windows.Forms.Panel panel2;
		private UserControl.UCLabelEdit bCardTransLenULE;
		private UserControl.UCLabelEdit aCardTransLenULE;
		private UserControl.UCLabelEdit aCardTransLetterULE;
		private UserControl.UCLabelEdit bCardTransLetterULE;
		private System.Windows.Forms.CheckBox chkIDMerge;
		private UserControl.UCButton uBtnIDDelete;
		public UserControl.UCLabelEdit ucLabEditCurrentNubmer;
		private UserControl.UCLabelEdit chkSNSet;
		private UserControl.UCLabelEdit chkSNLastCHR;
		private UserControl.UCMessage ucMessage;
		private UserControl.UCButton btnAutoGenLot;
		private UserControl.UCLabelEdit txtCartonMemo;
		private UserControl.UCButton btnLockCheck;
		private UserControl.UCButton btnSendExam;
		private UserControl.UCLabelEdit chkCartonLen;
		private UserControl.UCLabelEdit txtMemo;
		public UserControl.UCLabelEdit ucLabEditInputID;
		private UserControl.UCLabelEdit txtItemName;



		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}
	

		public FGenLotIDMerge()
		{
			// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化
			//			ucMessage.Add(">>请输入产品序列号  1/240");
			//			ucMessage.Add("<<PC001");
			//			ucMessage.Add(">>请输入产品序列号  2/240");
			//			ucMessage.Add("<<PC002");
			//			ucMessage.Add(">>送检批LOT01已经被判退，无法采集PC002");
			//			UserControl.UIStyleBuilder.FormUI(this);	
			//			UserControl.UIStyleBuilder.UltraOptionSetUI(this.ultraOptionSet1);
			//			UserControl.UIStyleBuilder.UltraOptionSetUI(this.ultraOptionSet2);
			//			UserControl.UIStyleBuilder.UltraOptionSetUI(this.ultraOptionSet3);
			//			this.ultraTabPageControlOQCLot.CanFocus = true;


			UserControl.UIStyleBuilder.FormUI(this);	
			//UserControl.UIStyleBuilder.UltraOptionSetUI(this.ultraOptionSetOQC);
			UserControl.UIStyleBuilder.UltraOptionSetUI(this.ultraOptionSetOQCExameOpion );
			UserControl.UIStyleBuilder.UltraOptionSetUI(this.ultraOptionSetOQCType);
			UserControl.UIStyleBuilder.GridUI(ultraGridMain);

			InitializeGrid();


			this.ultraGridMain.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White;;
			this.ultraGridMain.DisplayLayout.CaptionAppearance.BackColor =Color.FromName("WhiteSmoke");
			this.ultraGridMain.DisplayLayout.Appearance.BackColor=Color.FromArgb(255, 255, 255);
			this.ultraGridMain.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
			this.ultraGridMain.DisplayLayout.Override.RowAppearance.BackColor =Color.FromArgb(230, 234, 245);
			this.ultraGridMain.DisplayLayout.Override.RowAlternateAppearance.BackColor=Color.FromArgb(255, 255, 255);
			this.ultraGridMain.DisplayLayout.Override.RowSelectors =Infragistics.Win.DefaultableBoolean.False;
			this.ultraGridMain.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
			this.ultraGridMain.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
			this.ultraGridMain.DisplayLayout.ScrollBarLook.Appearance.BackColor =Color.FromName("LightGray");

			//			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			//			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			//			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			//			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			//			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			//			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			//			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			//			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			//			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			//			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			//			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			//
			//			appearance4.BackColor = System.Drawing.Color.Gainsboro;
			//			this.ultraTabControlMain.ActiveTabAppearance = appearance4;
			//			appearance5.BackColor = System.Drawing.Color.Gainsboro;
			//			appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
			//			this.ultraTabControlMain.Appearance = appearance5;
			//			this.ultraTabControlMain.BackColor = System.Drawing.Color.Gainsboro;
			//			appearance6.BackColor = System.Drawing.Color.Gainsboro;
			//			this.ultraTabControlMain.ClientAreaAppearance = appearance6;
			//			this.ultraTabControlMain.Controls.Add(this.ultraTabSharedControlsPage1);
			//			this.ultraTabControlMain.Controls.Add(this.ultraTabPageControlMain);
			//			this.ultraTabControlMain.Controls.Add(this.ultraTabPageControl2);
			//			this.ultraTabControlMain.Controls.Add(this.ultraTabPageControlOQCLot);
			//			this.ultraTabControlMain.Dock = System.Windows.Forms.DockStyle.Top;
			//			appearance7.BackColor = System.Drawing.Color.Gainsboro;
			//			this.ultraTabControlMain.HotTrackAppearance = appearance7;
			//			this.ultraTabControlMain.Location = new System.Drawing.Point(0, 0);
			//			this.ultraTabControlMain.Name = "ultraTabControlMain";
			//			appearance8.BackColor = System.Drawing.Color.Gainsboro;
			//			this.ultraTabControlMain.ScrollButtonAppearance = appearance8;
			//			appearance9.BackColor = System.Drawing.Color.Gainsboro;
			//			this.ultraTabControlMain.ScrollTrackAppearance = appearance9;
			//			appearance10.BackColor = System.Drawing.Color.Gainsboro;
			//			this.ultraTabControlMain.SelectedTabAppearance = appearance10;
			//			this.ultraTabControlMain.SharedControlsPage = this.ultraTabSharedControlsPage1;
			//			this.ultraTabControlMain.Size = new System.Drawing.Size(632, 72);
			//			this.ultraTabControlMain.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio;
			//			appearance11.BackColor = System.Drawing.Color.Gainsboro;
			//			this.ultraTabControlMain.TabHeaderAreaAppearance = appearance11;
			//			this.ultraTabControlMain.TabIndex = 165;
			//			ultraTab1.TabPage = this.ultraTabPageControlMain;
			//			ultraTab1.Text = "栈板包装";
			//			ultraTab2.TabPage = this.ultraTabPageControl2;
			//			ultraTab2.Text = "Carton包装";
			//			ultraTab3.TabPage = this.ultraTabPageControlOQCLot;
			//			ultraTab3.Text = "产生送检批";
			//
			//			//this.ultraTabControlMain.Tabs.Clear();
			//			this.ultraTabControlMain.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
			//																										  ultraTab1,
			//																										  ultraTab2,
			//																										  ultraTab3});
		   
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FGenLotIDMerge));
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
			this.ucLabEdit2 = new UserControl.UCLabelEdit();
			this.ucLabelEdit1 = new UserControl.UCLabelEdit();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.ucMessage = new UserControl.UCMessage();
			this.panel2 = new System.Windows.Forms.Panel();
			this.txtItemName = new UserControl.UCLabelEdit();
			this.btnLockCheck = new UserControl.UCButton();
			this.bCardTransLenULE = new UserControl.UCLabelEdit();
			this.aCardTransLenULE = new UserControl.UCLabelEdit();
			this.aCardTransLetterULE = new UserControl.UCLabelEdit();
			this.bCardTransLetterULE = new UserControl.UCLabelEdit();
			this.uBtnIDDelete = new UserControl.UCButton();
			this.ucLabEditCurrentNubmer = new UserControl.UCLabelEdit();
			this.chkSNSet = new UserControl.UCLabelEdit();
			this.chkSNLastCHR = new UserControl.UCLabelEdit();
			this.txtLotActualQty = new UserControl.UCLabelEdit();
			this.ultraOptionSetOQCExameOpion = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtMemo = new UserControl.UCLabelEdit();
			this.btnSendExam = new UserControl.UCButton();
			this.btnAutoGenLot = new UserControl.UCButton();
			this.btnEnable = new UserControl.UCButton();
			this.ultraOptionSetOQCType = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
			this.ucLabEditMaxNumber = new UserControl.UCLabelEdit();
			this.ucLabEditOQCLot = new UserControl.UCLabelEdit();
			this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.ucButton3 = new UserControl.UCButton();
			this.ucButton2 = new UserControl.UCButton();
			this.txtLength = new System.Windows.Forms.TextBox();
			this.cbCheckLength = new System.Windows.Forms.CheckBox();
			this.chkIDMerge = new System.Windows.Forms.CheckBox();
			this.cbRemixMO = new System.Windows.Forms.CheckBox();
			this.chkCodex = new System.Windows.Forms.CheckBox();
			this.txtCarton = new UserControl.UCLabelEdit();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkCartonLen = new UserControl.UCLabelEdit();
			this.txtCartonMemo = new UserControl.UCLabelEdit();
			this.txtCartonCapacity = new UserControl.UCLabelEdit();
			this.ucLabEditInputID = new UserControl.UCLabelEdit();
			this.uBtnExit = new UserControl.UCButton();
			this.panel1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetOQCExameOpion)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetOQCType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ucLabEdit2
			// 
			this.ucLabEdit2.AllowEditOnlyChecked = true;
			this.ucLabEdit2.Caption = "包装数量";
			this.ucLabEdit2.Checked = false;
			this.ucLabEdit2.EditType = UserControl.EditTypes.String;
			this.ucLabEdit2.Location = new System.Drawing.Point(160, -16);
			this.ucLabEdit2.MaxLength = 40;
			this.ucLabEdit2.Multiline = false;
			this.ucLabEdit2.Name = "ucLabEdit2";
			this.ucLabEdit2.PasswordChar = '\0';
			this.ucLabEdit2.ReadOnly = false;
			this.ucLabEdit2.ShowCheckBox = false;
			this.ucLabEdit2.Size = new System.Drawing.Size(195, 56);
			this.ucLabEdit2.TabIndex = 16;
			this.ucLabEdit2.TabNext = true;
			this.ucLabEdit2.Value = "";
			this.ucLabEdit2.WidthType = UserControl.WidthTypes.Normal;
			this.ucLabEdit2.XAlign = 222;
			// 
			// ucLabelEdit1
			// 
			this.ucLabelEdit1.AllowEditOnlyChecked = true;
			this.ucLabelEdit1.Caption = "输入标示";
			this.ucLabelEdit1.Checked = false;
			this.ucLabelEdit1.EditType = UserControl.EditTypes.String;
			this.ucLabelEdit1.Location = new System.Drawing.Point(357, 16);
			this.ucLabelEdit1.MaxLength = 40;
			this.ucLabelEdit1.Multiline = false;
			this.ucLabelEdit1.Name = "ucLabelEdit1";
			this.ucLabelEdit1.PasswordChar = '\0';
			this.ucLabelEdit1.ReadOnly = false;
			this.ucLabelEdit1.ShowCheckBox = false;
			this.ucLabelEdit1.Size = new System.Drawing.Size(195, 24);
			this.ucLabelEdit1.TabIndex = 1;
			this.ucLabelEdit1.TabNext = true;
			this.ucLabelEdit1.Value = "";
			this.ucLabelEdit1.WidthType = UserControl.WidthTypes.Normal;
			this.ucLabelEdit1.XAlign = 419;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.Controls.Add(this.groupBox3);
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(824, 541);
			this.panel1.TabIndex = 173;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.ucMessage);
			this.groupBox3.Controls.Add(this.panel2);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Location = new System.Drawing.Point(0, 280);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(824, 261);
			this.groupBox3.TabIndex = 288;
			this.groupBox3.TabStop = false;
			// 
			// ucMessage
			// 
			this.ucMessage.AutoScroll = true;
			this.ucMessage.BackColor = System.Drawing.Color.Gainsboro;
			this.ucMessage.ButtonVisible = false;
			this.ucMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucMessage.Location = new System.Drawing.Point(3, 104);
			this.ucMessage.Name = "ucMessage";
			this.ucMessage.Size = new System.Drawing.Size(818, 154);
			this.ucMessage.TabIndex = 176;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.txtItemName);
			this.panel2.Controls.Add(this.btnLockCheck);
			this.panel2.Controls.Add(this.bCardTransLenULE);
			this.panel2.Controls.Add(this.aCardTransLenULE);
			this.panel2.Controls.Add(this.aCardTransLetterULE);
			this.panel2.Controls.Add(this.bCardTransLetterULE);
			this.panel2.Controls.Add(this.uBtnIDDelete);
			this.panel2.Controls.Add(this.ucLabEditCurrentNubmer);
			this.panel2.Controls.Add(this.chkSNSet);
			this.panel2.Controls.Add(this.chkSNLastCHR);
			this.panel2.Controls.Add(this.txtLotActualQty);
			this.panel2.Controls.Add(this.ultraOptionSetOQCExameOpion);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(3, 17);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(818, 87);
			this.panel2.TabIndex = 175;
			// 
			// txtItemName
			// 
			this.txtItemName.AllowEditOnlyChecked = true;
			this.txtItemName.BackColor = System.Drawing.Color.Gainsboro;
			this.txtItemName.Caption = "产品名称";
			this.txtItemName.Checked = false;
			this.txtItemName.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtItemName.EditType = UserControl.EditTypes.String;
			this.txtItemName.Location = new System.Drawing.Point(24, 8);
			this.txtItemName.MaxLength = 40;
			this.txtItemName.Multiline = false;
			this.txtItemName.Name = "txtItemName";
			this.txtItemName.PasswordChar = '\0';
			this.txtItemName.ReadOnly = false;
			this.txtItemName.ShowCheckBox = true;
			this.txtItemName.Size = new System.Drawing.Size(178, 24);
			this.txtItemName.TabIndex = 295;
			this.txtItemName.TabNext = true;
			this.txtItemName.Value = "";
			this.txtItemName.WidthType = UserControl.WidthTypes.Small;
			this.txtItemName.XAlign = 102;
			this.txtItemName.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtItemName_TxtboxKeyPress);
			// 
			// btnLockCheck
			// 
			this.btnLockCheck.BackColor = System.Drawing.SystemColors.Control;
			this.btnLockCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLockCheck.BackgroundImage")));
			this.btnLockCheck.ButtonType = UserControl.ButtonTypes.None;
			this.btnLockCheck.Caption = "锁定";
			this.btnLockCheck.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnLockCheck.Location = new System.Drawing.Point(520, 32);
			this.btnLockCheck.Name = "btnLockCheck";
			this.btnLockCheck.Size = new System.Drawing.Size(88, 22);
			this.btnLockCheck.TabIndex = 294;
			this.btnLockCheck.Click += new System.EventHandler(this.btnLockCheck_Click);
			// 
			// bCardTransLenULE
			// 
			this.bCardTransLenULE.AllowEditOnlyChecked = true;
			this.bCardTransLenULE.BackColor = System.Drawing.Color.Gainsboro;
			this.bCardTransLenULE.Caption = "转换前序列号长度";
			this.bCardTransLenULE.Checked = false;
			this.bCardTransLenULE.EditType = UserControl.EditTypes.Integer;
			this.bCardTransLenULE.Location = new System.Drawing.Point(24, 32);
			this.bCardTransLenULE.MaxLength = 40;
			this.bCardTransLenULE.Multiline = false;
			this.bCardTransLenULE.Name = "bCardTransLenULE";
			this.bCardTransLenULE.PasswordChar = '\0';
			this.bCardTransLenULE.ReadOnly = false;
			this.bCardTransLenULE.ShowCheckBox = true;
			this.bCardTransLenULE.Size = new System.Drawing.Size(177, 24);
			this.bCardTransLenULE.TabIndex = 293;
			this.bCardTransLenULE.TabNext = false;
			this.bCardTransLenULE.Value = "";
			this.bCardTransLenULE.WidthType = UserControl.WidthTypes.Tiny;
			this.bCardTransLenULE.XAlign = 151;
			// 
			// aCardTransLenULE
			// 
			this.aCardTransLenULE.AllowEditOnlyChecked = true;
			this.aCardTransLenULE.BackColor = System.Drawing.Color.Gainsboro;
			this.aCardTransLenULE.Caption = "转换后序列号长度";
			this.aCardTransLenULE.Checked = false;
			this.aCardTransLenULE.EditType = UserControl.EditTypes.Integer;
			this.aCardTransLenULE.Location = new System.Drawing.Point(24, 56);
			this.aCardTransLenULE.MaxLength = 40;
			this.aCardTransLenULE.Multiline = false;
			this.aCardTransLenULE.Name = "aCardTransLenULE";
			this.aCardTransLenULE.PasswordChar = '\0';
			this.aCardTransLenULE.ReadOnly = false;
			this.aCardTransLenULE.ShowCheckBox = true;
			this.aCardTransLenULE.Size = new System.Drawing.Size(177, 24);
			this.aCardTransLenULE.TabIndex = 292;
			this.aCardTransLenULE.TabNext = false;
			this.aCardTransLenULE.Value = "";
			this.aCardTransLenULE.WidthType = UserControl.WidthTypes.Tiny;
			this.aCardTransLenULE.XAlign = 151;
			// 
			// aCardTransLetterULE
			// 
			this.aCardTransLetterULE.AllowEditOnlyChecked = true;
			this.aCardTransLetterULE.BackColor = System.Drawing.Color.Gainsboro;
			this.aCardTransLetterULE.Caption = "转换后序列号首字符串";
			this.aCardTransLetterULE.Checked = false;
			this.aCardTransLetterULE.EditType = UserControl.EditTypes.String;
			this.aCardTransLetterULE.Location = new System.Drawing.Point(216, 56);
			this.aCardTransLetterULE.MaxLength = 40;
			this.aCardTransLetterULE.Multiline = false;
			this.aCardTransLetterULE.Name = "aCardTransLetterULE";
			this.aCardTransLetterULE.PasswordChar = '\0';
			this.aCardTransLetterULE.ReadOnly = false;
			this.aCardTransLetterULE.ShowCheckBox = true;
			this.aCardTransLetterULE.Size = new System.Drawing.Size(285, 24);
			this.aCardTransLetterULE.TabIndex = 291;
			this.aCardTransLetterULE.TabNext = false;
			this.aCardTransLetterULE.Value = "";
			this.aCardTransLetterULE.WidthType = UserControl.WidthTypes.Normal;
			this.aCardTransLetterULE.XAlign = 368;
			// 
			// bCardTransLetterULE
			// 
			this.bCardTransLetterULE.AllowEditOnlyChecked = true;
			this.bCardTransLetterULE.BackColor = System.Drawing.Color.Gainsboro;
			this.bCardTransLetterULE.Caption = "转换前序列号首字符串";
			this.bCardTransLetterULE.Checked = false;
			this.bCardTransLetterULE.EditType = UserControl.EditTypes.String;
			this.bCardTransLetterULE.Location = new System.Drawing.Point(216, 32);
			this.bCardTransLetterULE.MaxLength = 40;
			this.bCardTransLetterULE.Multiline = false;
			this.bCardTransLetterULE.Name = "bCardTransLetterULE";
			this.bCardTransLetterULE.PasswordChar = '\0';
			this.bCardTransLetterULE.ReadOnly = false;
			this.bCardTransLetterULE.ShowCheckBox = true;
			this.bCardTransLetterULE.Size = new System.Drawing.Size(285, 24);
			this.bCardTransLetterULE.TabIndex = 290;
			this.bCardTransLetterULE.TabNext = false;
			this.bCardTransLetterULE.Value = "";
			this.bCardTransLetterULE.WidthType = UserControl.WidthTypes.Normal;
			this.bCardTransLetterULE.XAlign = 368;
			// 
			// uBtnIDDelete
			// 
			this.uBtnIDDelete.BackColor = System.Drawing.SystemColors.Control;
			this.uBtnIDDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uBtnIDDelete.BackgroundImage")));
			this.uBtnIDDelete.ButtonType = UserControl.ButtonTypes.Delete;
			this.uBtnIDDelete.Caption = "删除";
			this.uBtnIDDelete.Cursor = System.Windows.Forms.Cursors.Hand;
			this.uBtnIDDelete.Location = new System.Drawing.Point(704, 8);
			this.uBtnIDDelete.Name = "uBtnIDDelete";
			this.uBtnIDDelete.Size = new System.Drawing.Size(88, 22);
			this.uBtnIDDelete.TabIndex = 288;
			this.uBtnIDDelete.Click += new System.EventHandler(this.uBtnIDDelete_Click);
			// 
			// ucLabEditCurrentNubmer
			// 
			this.ucLabEditCurrentNubmer.AllowEditOnlyChecked = true;
			this.ucLabEditCurrentNubmer.BackColor = System.Drawing.Color.Gainsboro;
			this.ucLabEditCurrentNubmer.Caption = "采集数量";
			this.ucLabEditCurrentNubmer.Checked = false;
			this.ucLabEditCurrentNubmer.EditType = UserControl.EditTypes.Integer;
			this.ucLabEditCurrentNubmer.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.ucLabEditCurrentNubmer.Location = new System.Drawing.Point(536, 8);
			this.ucLabEditCurrentNubmer.MaxLength = 40;
			this.ucLabEditCurrentNubmer.Multiline = false;
			this.ucLabEditCurrentNubmer.Name = "ucLabEditCurrentNubmer";
			this.ucLabEditCurrentNubmer.PasswordChar = '\0';
			this.ucLabEditCurrentNubmer.ReadOnly = true;
			this.ucLabEditCurrentNubmer.ShowCheckBox = false;
			this.ucLabEditCurrentNubmer.Size = new System.Drawing.Size(162, 24);
			this.ucLabEditCurrentNubmer.TabIndex = 287;
			this.ucLabEditCurrentNubmer.TabNext = true;
			this.ucLabEditCurrentNubmer.Value = "";
			this.ucLabEditCurrentNubmer.WidthType = UserControl.WidthTypes.Small;
			this.ucLabEditCurrentNubmer.XAlign = 598;
			// 
			// chkSNSet
			// 
			this.chkSNSet.AllowEditOnlyChecked = true;
			this.chkSNSet.BackColor = System.Drawing.Color.Gainsboro;
			this.chkSNSet.Caption = "成套归属(SN数)";
			this.chkSNSet.Checked = false;
			this.chkSNSet.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.chkSNSet.EditType = UserControl.EditTypes.Integer;
			this.chkSNSet.Location = new System.Drawing.Point(252, 8);
			this.chkSNSet.MaxLength = 40;
			this.chkSNSet.Multiline = false;
			this.chkSNSet.Name = "chkSNSet";
			this.chkSNSet.PasswordChar = '\0';
			this.chkSNSet.ReadOnly = false;
			this.chkSNSet.ShowCheckBox = true;
			this.chkSNSet.Size = new System.Drawing.Size(165, 24);
			this.chkSNSet.TabIndex = 285;
			this.chkSNSet.TabNext = true;
			this.chkSNSet.Value = "";
			this.chkSNSet.WidthType = UserControl.WidthTypes.Tiny;
			this.chkSNSet.XAlign = 367;
			this.chkSNSet.CheckBoxCheckedChanged += new System.EventHandler(this.chkSNSet_CheckBoxCheckedChanged);
			this.chkSNSet.InnerTextChanged += new System.EventHandler(this.chkSNSet_InnerTextChanged);
			// 
			// chkSNLastCHR
			// 
			this.chkSNLastCHR.AllowEditOnlyChecked = true;
			this.chkSNLastCHR.BackColor = System.Drawing.Color.Gainsboro;
			this.chkSNLastCHR.Caption = "成套SN末位相同位数  ";
			this.chkSNLastCHR.Checked = false;
			this.chkSNLastCHR.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.chkSNLastCHR.EditType = UserControl.EditTypes.Integer;
			this.chkSNLastCHR.Enabled = false;
			this.chkSNLastCHR.Location = new System.Drawing.Point(504, 0);
			this.chkSNLastCHR.MaxLength = 40;
			this.chkSNLastCHR.Multiline = false;
			this.chkSNLastCHR.Name = "chkSNLastCHR";
			this.chkSNLastCHR.PasswordChar = '\0';
			this.chkSNLastCHR.ReadOnly = false;
			this.chkSNLastCHR.ShowCheckBox = true;
			this.chkSNLastCHR.Size = new System.Drawing.Size(202, 24);
			this.chkSNLastCHR.TabIndex = 286;
			this.chkSNLastCHR.TabNext = true;
			this.chkSNLastCHR.Value = "";
			this.chkSNLastCHR.Visible = false;
			this.chkSNLastCHR.WidthType = UserControl.WidthTypes.Tiny;
			this.chkSNLastCHR.XAlign = 656;
			// 
			// txtLotActualQty
			// 
			this.txtLotActualQty.AllowEditOnlyChecked = true;
			this.txtLotActualQty.BackColor = System.Drawing.Color.Gainsboro;
			this.txtLotActualQty.Caption = "批中数量";
			this.txtLotActualQty.Checked = false;
			this.txtLotActualQty.EditType = UserControl.EditTypes.String;
			this.txtLotActualQty.Enabled = false;
			this.txtLotActualQty.Location = new System.Drawing.Point(624, 64);
			this.txtLotActualQty.MaxLength = 4000;
			this.txtLotActualQty.Multiline = false;
			this.txtLotActualQty.Name = "txtLotActualQty";
			this.txtLotActualQty.PasswordChar = '\0';
			this.txtLotActualQty.ReadOnly = false;
			this.txtLotActualQty.ShowCheckBox = false;
			this.txtLotActualQty.Size = new System.Drawing.Size(112, 24);
			this.txtLotActualQty.TabIndex = 287;
			this.txtLotActualQty.TabNext = false;
			this.txtLotActualQty.Value = "";
			this.txtLotActualQty.Visible = false;
			this.txtLotActualQty.WidthType = UserControl.WidthTypes.Tiny;
			this.txtLotActualQty.XAlign = 686;
			// 
			// ultraOptionSetOQCExameOpion
			// 
			this.ultraOptionSetOQCExameOpion.BackColor = System.Drawing.Color.Gainsboro;
			this.ultraOptionSetOQCExameOpion.FlatMode = true;
			this.ultraOptionSetOQCExameOpion.ItemAppearance = appearance1;
			valueListItem1.DisplayText = "二维条码";
			valueListItem2.DisplayText = "PCS";
			this.ultraOptionSetOQCExameOpion.Items.Add(valueListItem1);
			this.ultraOptionSetOQCExameOpion.Items.Add(valueListItem2);
			this.ultraOptionSetOQCExameOpion.Location = new System.Drawing.Point(624, 40);
			this.ultraOptionSetOQCExameOpion.Name = "ultraOptionSetOQCExameOpion";
			this.ultraOptionSetOQCExameOpion.Size = new System.Drawing.Size(184, 16);
			this.ultraOptionSetOQCExameOpion.TabIndex = 0;
			this.ultraOptionSetOQCExameOpion.Visible = false;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtMemo);
			this.groupBox2.Controls.Add(this.btnSendExam);
			this.groupBox2.Controls.Add(this.btnAutoGenLot);
			this.groupBox2.Controls.Add(this.btnEnable);
			this.groupBox2.Controls.Add(this.ultraOptionSetOQCType);
			this.groupBox2.Controls.Add(this.ucLabEditMaxNumber);
			this.groupBox2.Controls.Add(this.ucLabEditOQCLot);
			this.groupBox2.Controls.Add(this.ultraGridMain);
			this.groupBox2.Controls.Add(this.ucButton3);
			this.groupBox2.Controls.Add(this.ucButton2);
			this.groupBox2.Controls.Add(this.txtLength);
			this.groupBox2.Controls.Add(this.cbCheckLength);
			this.groupBox2.Controls.Add(this.chkIDMerge);
			this.groupBox2.Controls.Add(this.cbRemixMO);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(824, 280);
			this.groupBox2.TabIndex = 287;
			this.groupBox2.TabStop = false;
			// 
			// txtMemo
			// 
			this.txtMemo.AllowEditOnlyChecked = true;
			this.txtMemo.BackColor = System.Drawing.Color.Gainsboro;
			this.txtMemo.Caption = "备注";
			this.txtMemo.Checked = false;
			this.txtMemo.EditType = UserControl.EditTypes.String;
			this.txtMemo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtMemo.Location = new System.Drawing.Point(216, 48);
			this.txtMemo.MaxLength = 40;
			this.txtMemo.Multiline = false;
			this.txtMemo.Name = "txtMemo";
			this.txtMemo.PasswordChar = '\0';
			this.txtMemo.ReadOnly = false;
			this.txtMemo.ShowCheckBox = false;
			this.txtMemo.Size = new System.Drawing.Size(170, 24);
			this.txtMemo.TabIndex = 292;
			this.txtMemo.TabNext = true;
			this.txtMemo.Value = "";
			this.txtMemo.WidthType = UserControl.WidthTypes.Normal;
			this.txtMemo.XAlign = 253;
			// 
			// btnSendExam
			// 
			this.btnSendExam.BackColor = System.Drawing.SystemColors.Control;
			this.btnSendExam.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSendExam.BackgroundImage")));
			this.btnSendExam.ButtonType = UserControl.ButtonTypes.None;
			this.btnSendExam.Caption = "送检";
			this.btnSendExam.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnSendExam.Location = new System.Drawing.Point(392, 24);
			this.btnSendExam.Name = "btnSendExam";
			this.btnSendExam.Size = new System.Drawing.Size(88, 22);
			this.btnSendExam.TabIndex = 291;
			this.btnSendExam.Click += new System.EventHandler(this.btnSendExam_Click);
			// 
			// btnAutoGenLot
			// 
			this.btnAutoGenLot.BackColor = System.Drawing.SystemColors.Control;
			this.btnAutoGenLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAutoGenLot.BackgroundImage")));
			this.btnAutoGenLot.ButtonType = UserControl.ButtonTypes.None;
			this.btnAutoGenLot.Caption = "自动产生批";
			this.btnAutoGenLot.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnAutoGenLot.Location = new System.Drawing.Point(296, 24);
			this.btnAutoGenLot.Name = "btnAutoGenLot";
			this.btnAutoGenLot.Size = new System.Drawing.Size(88, 22);
			this.btnAutoGenLot.TabIndex = 290;
			this.btnAutoGenLot.Click += new System.EventHandler(this.btnAutoGenLot_Click);
			// 
			// btnEnable
			// 
			this.btnEnable.BackColor = System.Drawing.SystemColors.Control;
			this.btnEnable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEnable.BackgroundImage")));
			this.btnEnable.ButtonType = UserControl.ButtonTypes.None;
			this.btnEnable.Caption = "锁定";
			this.btnEnable.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnEnable.Location = new System.Drawing.Point(200, 24);
			this.btnEnable.Name = "btnEnable";
			this.btnEnable.Size = new System.Drawing.Size(88, 22);
			this.btnEnable.TabIndex = 286;
			this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
			// 
			// ultraOptionSetOQCType
			// 
			this.ultraOptionSetOQCType.BackColor = System.Drawing.Color.Gainsboro;
			this.ultraOptionSetOQCType.FlatMode = true;
			this.ultraOptionSetOQCType.ItemAppearance = appearance2;
			valueListItem3.DisplayText = "正常批";
			valueListItem4.DisplayText = "再次检验批";
			this.ultraOptionSetOQCType.Items.Add(valueListItem3);
			this.ultraOptionSetOQCType.Items.Add(valueListItem4);
			this.ultraOptionSetOQCType.Location = new System.Drawing.Point(24, 56);
			this.ultraOptionSetOQCType.Name = "ultraOptionSetOQCType";
			this.ultraOptionSetOQCType.Size = new System.Drawing.Size(184, 16);
			this.ultraOptionSetOQCType.TabIndex = 100;
			// 
			// ucLabEditMaxNumber
			// 
			this.ucLabEditMaxNumber.AllowEditOnlyChecked = true;
			this.ucLabEditMaxNumber.BackColor = System.Drawing.Color.Gainsboro;
			this.ucLabEditMaxNumber.Caption = "批数量 ";
			this.ucLabEditMaxNumber.Checked = false;
			this.ucLabEditMaxNumber.EditType = UserControl.EditTypes.Integer;
			this.ucLabEditMaxNumber.Location = new System.Drawing.Point(496, 24);
			this.ucLabEditMaxNumber.MaxLength = 40;
			this.ucLabEditMaxNumber.Multiline = false;
			this.ucLabEditMaxNumber.Name = "ucLabEditMaxNumber";
			this.ucLabEditMaxNumber.PasswordChar = '\0';
			this.ucLabEditMaxNumber.ReadOnly = false;
			this.ucLabEditMaxNumber.ShowCheckBox = true;
			this.ucLabEditMaxNumber.Size = new System.Drawing.Size(122, 24);
			this.ucLabEditMaxNumber.TabIndex = 284;
			this.ucLabEditMaxNumber.TabNext = true;
			this.ucLabEditMaxNumber.Value = "";
			this.ucLabEditMaxNumber.WidthType = UserControl.WidthTypes.Tiny;
			this.ucLabEditMaxNumber.XAlign = 568;
			// 
			// ucLabEditOQCLot
			// 
			this.ucLabEditOQCLot.AllowEditOnlyChecked = true;
			this.ucLabEditOQCLot.BackColor = System.Drawing.Color.Gainsboro;
			this.ucLabEditOQCLot.Caption = "批号";
			this.ucLabEditOQCLot.Checked = false;
			this.ucLabEditOQCLot.EditType = UserControl.EditTypes.String;
			this.ucLabEditOQCLot.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.ucLabEditOQCLot.Location = new System.Drawing.Point(27, 24);
			this.ucLabEditOQCLot.MaxLength = 40;
			this.ucLabEditOQCLot.Multiline = false;
			this.ucLabEditOQCLot.Name = "ucLabEditOQCLot";
			this.ucLabEditOQCLot.PasswordChar = '\0';
			this.ucLabEditOQCLot.ReadOnly = false;
			this.ucLabEditOQCLot.ShowCheckBox = false;
			this.ucLabEditOQCLot.Size = new System.Drawing.Size(170, 24);
			this.ucLabEditOQCLot.TabIndex = 1;
			this.ucLabEditOQCLot.TabNext = true;
			this.ucLabEditOQCLot.Value = "";
			this.ucLabEditOQCLot.WidthType = UserControl.WidthTypes.Normal;
			this.ucLabEditOQCLot.XAlign = 64;
			this.ucLabEditOQCLot.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabEditOQCLot_TxtboxKeyPress);
			// 
			// ultraGridMain
			// 
			this.ultraGridMain.Cursor = System.Windows.Forms.Cursors.Default;
			this.ultraGridMain.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ultraGridMain.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.ultraGridMain.Location = new System.Drawing.Point(3, 77);
			this.ultraGridMain.Name = "ultraGridMain";
			this.ultraGridMain.Size = new System.Drawing.Size(818, 200);
			this.ultraGridMain.TabIndex = 7;
			this.ultraGridMain.Text = "送检批中产品列表";
			this.ultraGridMain.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMain_InitializeLayout);
			// 
			// ucButton3
			// 
			this.ucButton3.BackColor = System.Drawing.SystemColors.Control;
			this.ucButton3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton3.BackgroundImage")));
			this.ucButton3.ButtonType = UserControl.ButtonTypes.Confirm;
			this.ucButton3.Caption = "确认";
			this.ucButton3.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucButton3.Location = new System.Drawing.Point(392, 48);
			this.ucButton3.Name = "ucButton3";
			this.ucButton3.Size = new System.Drawing.Size(88, 22);
			this.ucButton3.TabIndex = 15;
			this.ucButton3.Visible = false;
			// 
			// ucButton2
			// 
			this.ucButton2.BackColor = System.Drawing.SystemColors.Control;
			this.ucButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton2.BackgroundImage")));
			this.ucButton2.ButtonType = UserControl.ButtonTypes.Delete;
			this.ucButton2.Caption = "删除";
			this.ucButton2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucButton2.Location = new System.Drawing.Point(728, 24);
			this.ucButton2.Name = "ucButton2";
			this.ucButton2.Size = new System.Drawing.Size(88, 22);
			this.ucButton2.TabIndex = 16;
			this.ucButton2.Visible = false;
			// 
			// txtLength
			// 
			this.txtLength.Enabled = false;
			this.txtLength.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtLength.Location = new System.Drawing.Point(568, 48);
			this.txtLength.Name = "txtLength";
			this.txtLength.Size = new System.Drawing.Size(50, 21);
			this.txtLength.TabIndex = 282;
			this.txtLength.Text = "";
			this.txtLength.Visible = false;
			// 
			// cbCheckLength
			// 
			this.cbCheckLength.BackColor = System.Drawing.Color.Gainsboro;
			this.cbCheckLength.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cbCheckLength.Location = new System.Drawing.Point(496, 48);
			this.cbCheckLength.Name = "cbCheckLength";
			this.cbCheckLength.Size = new System.Drawing.Size(112, 24);
			this.cbCheckLength.TabIndex = 281;
			this.cbCheckLength.Text = "批号长度";
			this.cbCheckLength.Visible = false;
			this.cbCheckLength.CheckedChanged += new System.EventHandler(this.cbCheckLength_CheckedChanged);
			// 
			// chkIDMerge
			// 
			this.chkIDMerge.BackColor = System.Drawing.Color.Gainsboro;
			this.chkIDMerge.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chkIDMerge.Location = new System.Drawing.Point(624, 24);
			this.chkIDMerge.Name = "chkIDMerge";
			this.chkIDMerge.Size = new System.Drawing.Size(80, 24);
			this.chkIDMerge.TabIndex = 289;
			this.chkIDMerge.Text = "序号转换";
			this.chkIDMerge.CheckedChanged += new System.EventHandler(this.chkIDMerge_CheckedChanged);
			// 
			// cbRemixMO
			// 
			this.cbRemixMO.BackColor = System.Drawing.Color.Gainsboro;
			this.cbRemixMO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cbRemixMO.Location = new System.Drawing.Point(624, 48);
			this.cbRemixMO.Name = "cbRemixMO";
			this.cbRemixMO.Size = new System.Drawing.Size(128, 24);
			this.cbRemixMO.TabIndex = 279;
			this.cbRemixMO.Text = "允许不同工单采集";
			this.cbRemixMO.CheckedChanged += new System.EventHandler(this.cbRemixMO_CheckedChanged);
			// 
			// chkCodex
			// 
			this.chkCodex.Location = new System.Drawing.Point(328, 48);
			this.chkCodex.Name = "chkCodex";
			this.chkCodex.Size = new System.Drawing.Size(168, 24);
			this.chkCodex.TabIndex = 285;
			this.chkCodex.Text = "检查Carton 与序列号算法";
			// 
			// txtCarton
			// 
			this.txtCarton.AllowEditOnlyChecked = true;
			this.txtCarton.BackColor = System.Drawing.Color.Gainsboro;
			this.txtCarton.Caption = "归属Carton";
			this.txtCarton.Checked = false;
			this.txtCarton.EditType = UserControl.EditTypes.String;
			this.txtCarton.Location = new System.Drawing.Point(24, 16);
			this.txtCarton.MaxLength = 40;
			this.txtCarton.Multiline = false;
			this.txtCarton.Name = "txtCarton";
			this.txtCarton.PasswordChar = '\0';
			this.txtCarton.ReadOnly = false;
			this.txtCarton.ShowCheckBox = true;
			this.txtCarton.Size = new System.Drawing.Size(290, 24);
			this.txtCarton.TabIndex = 10;
			this.txtCarton.TabNext = false;
			this.txtCarton.Value = "";
			this.txtCarton.WidthType = UserControl.WidthTypes.Long;
			this.txtCarton.XAlign = 114;
			this.txtCarton.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCarton_TxtboxKeyPress);
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.Gainsboro;
			this.groupBox1.Controls.Add(this.chkCartonLen);
			this.groupBox1.Controls.Add(this.txtCartonMemo);
			this.groupBox1.Controls.Add(this.txtCartonCapacity);
			this.groupBox1.Controls.Add(this.ucLabEditInputID);
			this.groupBox1.Controls.Add(this.uBtnExit);
			this.groupBox1.Controls.Add(this.txtCarton);
			this.groupBox1.Controls.Add(this.chkCodex);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 541);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(824, 80);
			this.groupBox1.TabIndex = 171;
			this.groupBox1.TabStop = false;
			// 
			// chkCartonLen
			// 
			this.chkCartonLen.AllowEditOnlyChecked = true;
			this.chkCartonLen.BackColor = System.Drawing.Color.Gainsboro;
			this.chkCartonLen.Caption = "Carton箱号长度";
			this.chkCartonLen.Checked = false;
			this.chkCartonLen.EditType = UserControl.EditTypes.Integer;
			this.chkCartonLen.Location = new System.Drawing.Point(328, 16);
			this.chkCartonLen.MaxLength = 40;
			this.chkCartonLen.Multiline = false;
			this.chkCartonLen.Name = "chkCartonLen";
			this.chkCartonLen.PasswordChar = '\0';
			this.chkCartonLen.ReadOnly = false;
			this.chkCartonLen.ShowCheckBox = true;
			this.chkCartonLen.Size = new System.Drawing.Size(165, 24);
			this.chkCartonLen.TabIndex = 287;
			this.chkCartonLen.TabNext = false;
			this.chkCartonLen.Value = "";
			this.chkCartonLen.WidthType = UserControl.WidthTypes.Tiny;
			this.chkCartonLen.XAlign = 443;
			// 
			// txtCartonMemo
			// 
			this.txtCartonMemo.AllowEditOnlyChecked = true;
			this.txtCartonMemo.BackColor = System.Drawing.Color.Gainsboro;
			this.txtCartonMemo.Caption = "Carton备注";
			this.txtCartonMemo.Checked = false;
			this.txtCartonMemo.EditType = UserControl.EditTypes.String;
			this.txtCartonMemo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtCartonMemo.Location = new System.Drawing.Point(512, 16);
			this.txtCartonMemo.MaxLength = 40;
			this.txtCartonMemo.Multiline = false;
			this.txtCartonMemo.Name = "txtCartonMemo";
			this.txtCartonMemo.PasswordChar = '\0';
			this.txtCartonMemo.ReadOnly = false;
			this.txtCartonMemo.ShowCheckBox = false;
			this.txtCartonMemo.Size = new System.Drawing.Size(274, 24);
			this.txtCartonMemo.TabIndex = 286;
			this.txtCartonMemo.TabNext = true;
			this.txtCartonMemo.Value = "";
			this.txtCartonMemo.WidthType = UserControl.WidthTypes.Long;
			this.txtCartonMemo.XAlign = 586;
			// 
			// txtCartonCapacity
			// 
			this.txtCartonCapacity.AllowEditOnlyChecked = true;
			this.txtCartonCapacity.BackColor = System.Drawing.Color.Gainsboro;
			this.txtCartonCapacity.Caption = "Carton中数量";
			this.txtCartonCapacity.Checked = false;
			this.txtCartonCapacity.EditType = UserControl.EditTypes.Integer;
			this.txtCartonCapacity.Location = new System.Drawing.Point(499, 48);
			this.txtCartonCapacity.MaxLength = 10;
			this.txtCartonCapacity.Multiline = false;
			this.txtCartonCapacity.Name = "txtCartonCapacity";
			this.txtCartonCapacity.PasswordChar = '\0';
			this.txtCartonCapacity.ReadOnly = true;
			this.txtCartonCapacity.ShowCheckBox = false;
			this.txtCartonCapacity.Size = new System.Drawing.Size(137, 24);
			this.txtCartonCapacity.TabIndex = 285;
			this.txtCartonCapacity.TabNext = true;
			this.txtCartonCapacity.Value = "";
			this.txtCartonCapacity.WidthType = UserControl.WidthTypes.Tiny;
			this.txtCartonCapacity.XAlign = 586;
			// 
			// ucLabEditInputID
			// 
			this.ucLabEditInputID.AllowEditOnlyChecked = true;
			this.ucLabEditInputID.BackColor = System.Drawing.Color.Gainsboro;
			this.ucLabEditInputID.Caption = "产品序列号输入框";
			this.ucLabEditInputID.Checked = false;
			this.ucLabEditInputID.EditType = UserControl.EditTypes.String;
			this.ucLabEditInputID.Location = new System.Drawing.Point(3, 48);
			this.ucLabEditInputID.MaxLength = 4000;
			this.ucLabEditInputID.Multiline = false;
			this.ucLabEditInputID.Name = "ucLabEditInputID";
			this.ucLabEditInputID.PasswordChar = '\0';
			this.ucLabEditInputID.ReadOnly = false;
			this.ucLabEditInputID.ShowCheckBox = false;
			this.ucLabEditInputID.Size = new System.Drawing.Size(311, 24);
			this.ucLabEditInputID.TabIndex = 2;
			this.ucLabEditInputID.TabNext = false;
			this.ucLabEditInputID.Value = "";
			this.ucLabEditInputID.WidthType = UserControl.WidthTypes.Long;
			this.ucLabEditInputID.XAlign = 114;
			this.ucLabEditInputID.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabEditInputID_TxtboxKeyPress);
			// 
			// uBtnExit
			// 
			this.uBtnExit.BackColor = System.Drawing.SystemColors.Control;
			this.uBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uBtnExit.BackgroundImage")));
			this.uBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
			this.uBtnExit.Caption = "退出";
			this.uBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
			this.uBtnExit.Location = new System.Drawing.Point(696, 48);
			this.uBtnExit.Name = "uBtnExit";
			this.uBtnExit.Size = new System.Drawing.Size(88, 22);
			this.uBtnExit.TabIndex = 9;
			// 
			// FGenLotIDMerge
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(824, 621);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox1);
			this.Name = "FGenLotIDMerge";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "产生送检批";
			this.Resize += new System.EventHandler(this.FGenLot_Resize);
			this.Load += new System.EventHandler(this.FGenLot_Load);
			this.Closed += new System.EventHandler(this.FGenLot_Closed);
			this.Activated += new System.EventHandler(this.FGenLot_Activated);
			this.panel1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetOQCExameOpion)).EndInit();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetOQCType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		#region form的初始化
		private void InitForm()
		{
			//			ultraTabControlMain.Tabs[2].Selected = true;
			//			ultraTabControlMain.Tabs[2].Active = true;
			
			#region 检验对象的类型
			//默认选择为二维条码
			this.ultraOptionSetOQCExameOpion.Items.Clear();
			//this.ultraOptionSetOQCExameOpion.CheckedItem =  this.ultraOptionSetOQCExameOpion.Items.Add(OQCFacade.OQC_ExameObject_PlanarCode,"二维条码");
			//this.ultraOptionSetOQCExameOpion.Items.Add(OQCFacade.OQC_ExameObject_Carton,"Carton");
			//this.ultraOptionSetOQCExameOpion.Items.Add(OQCFacade.OQC_ExameObject_PCS,"PCS");
			this.ultraOptionSetOQCExameOpion.CheckedItem =  this.ultraOptionSetOQCExameOpion.Items.Add(OQCFacade.OQC_ExameObject_PCS,"PCS");
			#endregion

			#region 检验批类型
			this.ultraOptionSetOQCType.Items.Clear();
			this.ultraOptionSetOQCType.CheckedItem =  this.ultraOptionSetOQCType.Items.Add(OQCLotType.OQCLotType_Normal,"正常批");
			this.ultraOptionSetOQCType.Items.Add(OQCLotType.OQCLotType_ReDO,"再次检验批");
			#endregion

			#region 设置默认值
			this.ucLabEditMaxNumber.Checked = false;
			this.cbRemixMO.Checked = OQCHelper.IsRemixMO;
			this.ucLabEditOQCLot.Value = string.Empty;
			this.ucLabEditCurrentNubmer.Value = "0";
			this.ucMessage.Clear();
			this.ucLabEditInputID.Value = string.Empty;
			//Laws Lu，2005/08/11，注释
			//this.uBtnIDDelete.Enabled = false;

			/* added by jessie lee, 2006-6-16,
			 * power0060 */
			this.aCardTransLenULE.Enabled		= this.chkIDMerge.Checked;
			this.aCardTransLetterULE.Enabled	= this.chkIDMerge.Checked;
			this.bCardTransLenULE.Enabled		= this.chkIDMerge.Checked;
			this.bCardTransLetterULE.Enabled	= this.chkIDMerge.Checked;
			#endregion

			ClearUI();

			
			//UserControl.UIStyleBuilder.GridUI(this.ultraGridMain);
			//			//Laws Lu,2006/06/21	auto generate lot no under the rule four digit year + month + day + line code + sequence number
			//			
			//
			//			if(objLot == null)
			//			{
			//				OQCLot oqcLot = CreateNewOQCLot();
			//				oqcLot.LOTNO = 
			//			}

			/* added by jessie lee, 2006/6/16
			 * power0058:包装自动产生送检批 */
			//			string lotno = DateTime.Now.ToString("yyyyMMdd-hhmmss-")+DateTime.Now.Millisecond.ToString().PadLeft(3,'0');
			

		}

		#endregion


		#region 页面事件

		private void FGenLot_Load(object sender, System.EventArgs e)
		{
			InitForm();
			
		}

		private void ultraGridMain_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
		{
			UltraWinGridHelper ultraWinGridHelper = new UltraWinGridHelper(this.ultraGridMain);
			ultraWinGridHelper.AddCheckColumn("checkbox","*");
			ultraWinGridHelper.AddCommonColumn("runningcard","产品序列号");
			ultraWinGridHelper.AddCommonColumn("itemcode","产品");
			ultraWinGridHelper.AddCommonColumn("mocode","工单");
			ultraWinGridHelper.AddCommonColumn("stepsequence","生产线");
			ultraWinGridHelper.AddCommonColumn("cartonno","Carton号");
			ultraWinGridHelper.AddCommonColumn("collecttype","采集类型");

			//			foreach(UltraGridBand ub in ultraGridMain.co
		}

		private void InitializeGrid()
		{
			dtIDList.Columns.Clear();
			dtIDList.Columns.Add("checkbox",typeof(System.Boolean));
			dtIDList.Columns.Add("runningcard",typeof(string)).ReadOnly = true;
			dtIDList.Columns.Add("itemcode",typeof(string)).ReadOnly = true;
			dtIDList.Columns.Add("mocode",typeof(string)).ReadOnly = true;
			dtIDList.Columns.Add("stepsequence",typeof(string)).ReadOnly = true;
			dtIDList.Columns.Add("cartonno",typeof(string)).ReadOnly = true;
			dtIDList.Columns.Add("collecttype",typeof(string)).ReadOnly = true;

			this.ultraGridMain.DataSource = dtIDList;

			if(this.ultraGridMain.DisplayLayout.Bands.Count > 0)
			{
				if(this.ultraGridMain.DisplayLayout.Bands[0].Columns.Count > 0)
				{
					this.ultraGridMain.DisplayLayout.Bands[0].Columns["collecttype"].Hidden = true;
				}
			}
		}

		//Laws Lu,2005/08/11,注释
		//		private void ultraOptionSetOQCExameOpion_ValueChanged(object sender, System.EventArgs e)
		//		{
		////			if(this.ultraOptionSetOQCExameOpion.CheckedItem.DataValue.ToString() == OQCFacade.OQC_ExameObject_PCS)
		////			{
		////				this.uBtnIDDelete.Enabled = true;
		////			}
		////			else
		////			{
		////				this.uBtnIDDelete.Enabled = false;
		////			}
		//		}


		
		private void ucLabEditOQCLot_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar =='\r')
			{
				ClearUI();
				OQCFacade _oqcFacade = new OQCFacade(this.DataProvider);
				//批号不能重复
				if(FormatHelper.CleanString(this.ucLabEditOQCLot.Value) == string.Empty)
				{
					ucLabEditOQCLot.TextFocus(false, true);
					
					System.Windows.Forms.SendKeys.Send("+{TAB}");
					return ;
				}
				if(InvalidData())
				{
					//Laws Lu,2006/12/27 减少Open/Close次数
					((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
					((SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();

					object obj = _oqcFacade.GetOQCLot( FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.ucLabEditOQCLot.Value)),OQCFacade.Lot_Sequence_Default);
					if(obj == null)
					{
						this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_LOT_NOT_EXIST")); 

						ucLabEditOQCLot.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;
					
						System.Windows.Forms.SendKeys.Send("+{TAB}");

						//Laws Lu,2006/12/27 减少Open/Close次数
						
						((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
						((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
						return;

						//						//Laws Lu,2005/10/19,新增	缓解性能问题
						//						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
						//						DataProvider.BeginTransaction();
						//						try
						//						{
						//						
						//							//产生送检验批号
						//							_oqcFacade.AddOQCLot(CreateNewOQCLot());
						//							this.DataProvider.CommitTransaction();
						//							btnEnable_Click(sender,e);
						//						}
						//						catch(Exception ex)
						//						{
						//							this.DataProvider.RollbackTransaction();
						//							throw ex;
						//						}
						//						finally
						//						{
						//							//Laws Lu,2005/10/19,新增	缓解性能问题
						//							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
						//						}
						//
						//InitMessage( FormatHelper.CleanString(this.ucLabEditOQCLot.Value),ucLabEditMaxNumber.Checked,oqcMaxSize);
					}
					else
					{
						InitPostView(_oqcFacade);
						btnEnable_Click(sender,e);

						SetcbRemixMOStatus();
					}
					//Laws Lu,2005/08/10,新增 将焦点移到产品序列号输入框
					//SendKeys.Send("{TAB}");

					//Laws Lu,2006/12/27 减少Open/Close次数
						
					((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
					((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;

					Application.DoEvents();
					this.ucLabEditInputID.TextFocus(false, true);	// Added by Icyer 2006/06/06
				}
			}
			else
			{
				ucLabEditInputID.SelectAll();
				rcards.Clear();
				rcardsBefore.Clear();	// Added by Icyer 2006/06/06

				CurrentSNCount = 1;
			}
		}


		private bool IsNumberic(string num)
		{
			try
			{
				Convert.ToInt32(num);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public void ucLabEditInputID_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar =='\r')
			{
				if( FormatHelper.CleanString(this.ucLabEditInputID.Value) == string.Empty)
				{
					//Laws Lu,2005/08/11,新增焦点设置
					ucLabEditInputID.TextFocus(false, true);
					//SendKeys.Send("+{TAB}");

					return;
				}

				if( chkCartonLen.Checked == true && chkCartonLen.Value.Trim() != string.Empty)
				{
					if(txtCarton.Checked == true && txtCarton.Value.Trim().Length !=  Convert.ToInt32(chkCartonLen.Value))
					{
						//messages.Add(new UserControl.Message(MessageType.Error,"$CS_CARTON_NO_LEN_CHECK_FAIL"));

						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CS_CARTON_NO_LEN_CHECK_FAIL"));//Carton箱号长度不正确

						txtCarton.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;
						return;
					}
				}

				//Karron Qiu,2005-10-26,在采集界面上增加一个检查批号长度的勾选框和一个输入框，
				//如果用户勾选该项，输入框有效，用户必须输入批号长度，当用户输入送检批号时，
				//如果用户已经勾选了检查批号长度项，则用户输入的送检批号长度必须与用户设置的长度值一致，
				//如果没有勾选则不检查
				if(cbCheckLength.Checked)
				{
					Messages messages  = new Messages();
					if(!IsNumberic(txtLength.Text))
					{
						messages.Add(new UserControl.Message(MessageType.Error,"$CS_FQCLOTLENGTH_NOT_NULL"));//批号长度不能为空
						ApplicationRun.GetInfoForm().Add(messages);

                        txtLength.Focus();
						return ;
					}
					else if(ucLabEditOQCLot.Value.Length != Convert.ToInt32(txtLength.Text))
					{
						messages.Add(new UserControl.Message(MessageType.Error,"$CS_FQCLOTLENGTH_NOT_ENOUGH"));//批号长度不够
						ApplicationRun.GetInfoForm().Add(messages);

						ucLabEditOQCLot.TextFocus(false, true);
						return ;
					}
				}

				// Added by Icyer 2006/06/05
				if (this.chkIDMerge.Checked == true && this.rcards.Count == this.rcardsBefore.Count)
				{
					//added by jessie lee, 2006/6/16
					#region 判断转换前序列号是否符合条件
					//长度检查
					if( bCardTransLenULE.Checked )
					{
						if( bCardTransLenULE.Value.Trim().Length == 0 )
						{
							this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_Before_Card_Transfer_Empty")); 
							ucLabEditInputID.TextFocus(true, true);
							return ;
						}

						int len = 0;
						try
						{
							len = int.Parse(bCardTransLenULE.Value.Trim());
						}
						catch
						{
							this.ucMessage.Add( new UserControl.Message(MessageType.Error,"$Error_BeforeCardTransLen_Should_be_Integer"));
							ucLabEditInputID.TextFocus(true, true);
							return ;
						}

						if( len != this.ucLabEditInputID.Value.Trim().Length )
						{
							this.ucMessage.Add( new UserControl.Message(MessageType.Error,"$Error_BeforeCardTransLen_Not_Correct"));
							ucLabEditInputID.TextFocus(true, true);
							return ;
						}
					}

					//首字符串检查
					if(bCardTransLetterULE.Checked)
					{
						if( bCardTransLetterULE.Value.Trim().Length == 0 )
						{
							this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_Before_Card_Transfer_FLetter_Empty")); 
							ucLabEditInputID.TextFocus(true, true);
							return ;
						}

						int index = ucLabEditInputID.Value.Trim().IndexOf( bCardTransLetterULE.Value.Trim() );
						if( index != 0 )
						{
							this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_Before_Card_Transfer_FLetter_NotCompare")); 
							ucLabEditInputID.TextFocus(true, true);
							return ;
						}
					}
					#endregion

					//Added By Karron Qiu,2006-7-4

					Messages messages  = new Messages();
					messages = CheckID(ucLabEditInputID.Value);
					if(!messages.IsSuccess())
					{
						ApplicationRun.GetInfoForm().Add(messages);

						rcards.Clear();
						rcardsBefore.Clear();
						CurrentSNCount = 1;

						this.ucLabEditInputID.Value = string.Empty;
						ucLabEditInputID.TextFocus(false, true);
						return ;
					}
					//end


					this.rcardsBefore.Add(FormatHelper.CleanString(this.ucLabEditInputID.Value) );
					ucMessage.Add(new UserControl.Message("<<" + FormatHelper.CleanString(this.ucLabEditInputID.Value)));
					ucMessage.AddBoldText(">>$CS_Please_Input_Merge_ID ");
					this.ucLabEditInputID.Value = string.Empty;
					this.ucLabEditInputID.TextFocus(false, true);
					return;
				}
				// Added end

				if( this.chkIDMerge.Checked )
				{
					//added by jessie lee, 2006/6/16
					#region 判断转换后序列号是否符合条件
					//长度检查
					if( aCardTransLenULE.Checked )
					{
						if( aCardTransLenULE.Value.Trim().Length == 0 )
						{
							this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_After_Card_Transfer_Empty")); 
							ucLabEditInputID.TextFocus(true, true);
							return ;
						}

						int len = 0;
						try
						{
							len = int.Parse(aCardTransLenULE.Value.Trim());
						}
						catch
						{
							this.ucMessage.Add( new UserControl.Message(MessageType.Error,"$Error_AfterCardTransLen_Should_be_Integer"));
							ucLabEditInputID.TextFocus(true, true);
							return ;
						}

						if( len != this.ucLabEditInputID.Value.Trim().Length )
						{
							this.ucMessage.Add( new UserControl.Message(MessageType.Error,"$Error_AfterCardTransLen_Not_Correct"));
							ucLabEditInputID.TextFocus(true, true);
							return ;
						}
					}

					//首字符串检查
					if(aCardTransLetterULE.Checked)
					{
						if( aCardTransLetterULE.Value.Trim().Length == 0 )
						{
							this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_After_Card_Transfer_FLetter_Empty")); 
							ucLabEditInputID.TextFocus(true, true);
							return ;
						}

						int index = ucLabEditInputID.Value.Trim().IndexOf( aCardTransLetterULE.Value.Trim() );
						if( index != 0 )
						{
							this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_After_Card_Transfer_FLetter_NotCompare")); 
							ucLabEditInputID.TextFocus(true, true);
							return ;
						}
					}
					#endregion

					//Added By Karron Qiu,2006-7-4

					Messages messages  = new Messages();
					//Laws Lu,2006/07/05 不需要Check
					//messages = CheckID(ucLabEditInputID.Value);
					if(!messages.IsSuccess())
					{
						ApplicationRun.GetInfoForm().Add(messages);

						rcards.Clear();
						rcardsBefore.Clear();
						CurrentSNCount = 1;

						this.ucLabEditInputID.Value = string.Empty;
						ucLabEditInputID.TextFocus(false, true);
						return ;
					}
					//end
				}

				rcards.Add( FormatHelper.CleanString(this.ucLabEditInputID.Value) );

				if(CurrentSNCount == TotalSNCount)
				{
					if(rcards != null && rcards.Count > 0)
					{
						bool bCheck = true;

						if(chkCodex.Checked == true && txtCarton.Checked == true)
						{
							bCheck = ValidateRcards((string[])rcards.ToArray(typeof(string)));
						}

						if(bCheck)
						{
							DoAction(rcards);
						}
						else
						{
							ucMessage.Add(new UserControl.Message(MessageType.Error
								,"$CS_RCARD_NOT_BELONG_CARTON $CS_Param_ID =" + ucLabEditInputID.Value.Trim().ToUpper()
								+ " $CS_CARTON_NO =" + txtCarton.Value.Trim()));

							rcards.Clear();
							ucLabEditInputID.TextFocus(true, true);

						}
					}
				}
				else
				{
					ucMessage.Add(new UserControl.Message("<<" + FormatHelper.CleanString(this.ucLabEditInputID.Value)));
					ucMessage.Add(new UserControl.Message("$CS_Please_Input_RunningCard " + CurrentSNCount.ToString() + "/" + TotalSNCount.ToString()));

					CurrentSNCount ++;
					ucLabEditInputID.TextFocus(true, true);
				}
			}
		}

		private Messages CheckID(string RCard)
		{
			DataCollectFacade facade = new DataCollectFacade(this.DataProvider);
			ActionOnLineHelper actionOnLineHelper = new ActionOnLineHelper(this.DataProvider);

			//Laws Lu,2006/07/05 修正未将对象引用设定到
			ProductInfo productinInfo = null;
			Messages msg = new Messages();
			msg.AddMessages(actionOnLineHelper.GetIDInfo(
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(RCard))));

			if(msg.IsSuccess())
			{
				productinInfo = msg.GetData().Values[0] as ProductInfo;
			}
						
			if(productinInfo == null || (productinInfo != null && productinInfo.LastSimulation == null))
			{
				msg.Add(new UserControl.Message(MessageType.Error,"$NoSimulation"));
				return msg;
			}

			return facade.CheckID(RCard,ActionType.DataCollectAction_IDTran,Service.ApplicationService.Current().ResourceCode,
				Service.ApplicationService.Current().UserCode,productinInfo);
		}

		//Laws Lu,2006/05/26,Support multi-rcard collect
		private void DoAction(ArrayList rcards)
		{

			Messages messages = new Messages();
			if( InvalidData())
			{
				ActionOnLineHelper actionOnLineHelper = new ActionOnLineHelper(this.DataProvider);
				ActionFactory actionFactory = new ActionFactory(this.DataProvider);
				IAction actionOQCLotAddID = actionFactory.CreateAction(ActionType.DataCollectAction_OQCLotAddID);
				// Added by Icyer 2006/06/05
				IAction actionIDMerge = actionFactory.CreateAction(ActionType.DataCollectAction_Split);
				// Added end

				if(FormatHelper.CleanString(this.ucLabEditOQCLot.Value) == string.Empty)
				{
					messages.Add(new UserControl.Message("$CS_FQCLOT_NOT_NULL"));
					ApplicationRun.GetInfoForm().Add(messages);

					if (ucLabEditOQCLot.Enabled == false)
					{
						this.btnEnable_Click(null, null);
					}

					//Laws Lu,2005/08/11,新增焦点设置
					ucLabEditOQCLot.TextFocus(false, true);

					rcards.Clear();
					this.rcardsBefore.Clear();

					CurrentSNCount = 1;


					return ;
				}

				// Added by Icyer 2006/06/06
				// Laws Lu,bug修复 lot is full,but be able to add pcs yet
				if (ucLabEditMaxNumber.Value != string.Empty)
				{
					if (IsNumberic(ucLabEditMaxNumber.Value) == true)
					{
						int iQty = Convert.ToInt32(ucLabEditMaxNumber.Value);
						int iCurrentQty = ((this.ucLabEditCurrentNubmer.Value != String.Empty)?(Convert.ToInt32(this.ucLabEditCurrentNubmer.Value)):0);
						if (iCurrentQty >= iQty)
						{
							ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CS_LOT_ALREADY_FULL"));
							ucLabEditOQCLot.Value = string.Empty;
							if (ucLabEditOQCLot.Enabled == false)
							{
								this.btnEnable_Click(null, null);
							}
							Application.DoEvents();
							ucLabEditOQCLot.TextFocus(false, true);

							ClearUI();
							rcards.Clear();
							this.rcardsBefore.Clear();

							CurrentSNCount = 1;

							return;
						}
					}
				}

				

				//判断数量

				//判断是哪中产生批的来源

				#region 二维条码
				if( ultraOptionSetOQCExameOpion.CheckedItem.DataValue.ToString() == OQCFacade.OQC_ExameObject_PlanarCode)
				{
					BarCodeParse barCodeParse = new BarCodeParse(this.DataProvider);
					ArrayList tmpIDList = null;
					try
					{
						foreach(string rcard in rcards)
						{
							/*tmpIDList = */
							string[] tmp= barCodeParse.GetIDList(rcard);

							if(tmp == null || (tmp != null && tmp.Length < 1))
							{
								//Laws Lu,2005/08/11,新增焦点设置
								ucLabEditInputID.TextFocus(false, true);
								//SendKeys.Send("+{TAB}");

								return;
							}
							tmpIDList.AddRange(tmp);
						}
					
					}
					catch (Exception ex)
					{
						messages.Add(new UserControl.Message(ex));
						ApplicationRun.GetInfoForm().Add(messages);

						//Laws Lu,2005/08/11,新增焦点设置
						ucLabEditInputID.TextFocus(true, true);
						//SendKeys.Send("{TAB}");
						return ;
					}
					
					ProductInfo productinInfo = (ProductInfo) actionOnLineHelper.GetIDInfo(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(tmpIDList[0].ToString()))).GetData().Values[0];
					//Laws Lu,2005/10/19,新增	缓解性能问题
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
					DataProvider.BeginTransaction();
					try
					{
						for(int i=0;i<tmpIDList.Count;i++)
						{
							messages.AddMessages(AddSingleIDIntoOQCLot(tmpIDList[i].ToString(),FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabEditOQCLot.Value)),messages,actionOQCLotAddID,actionOnLineHelper));
						}
						
						if(!messages.IsSuccess())
						{
							this.DataProvider.RollbackTransaction();
						}
						else
						{
							this.DataProvider.CommitTransaction();

						}
					}
					catch(Exception ex)
					{
						this.DataProvider.RollbackTransaction();
						throw ex;
					}
					finally
					{
						//Laws Lu,2005/10/19,新增	缓解性能问题
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
					}
				}
				#endregion

				//carton
				if(ultraOptionSetOQCExameOpion.CheckedItem.DataValue.ToString() == OQCFacade.OQC_ExameObject_Carton)
				{
					this.ClearUI();
				}
				//pics
				if(ultraOptionSetOQCExameOpion.CheckedItem.DataValue.ToString() == OQCFacade.OQC_ExameObject_PCS)
				{
					//Laws Lu,2005/10/19,新增	缓解性能问题
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
					DataProvider.BeginTransaction();
					try
					{
						//Laws Lu,2006/05/26,循环加入批
						//foreach(string rcard in rcards)
						for (int i = 0; i < rcards.Count; i++)
						{
							string rcard = rcards[i].ToString();
							// Added by Icyer 2006/06/05
							if (this.chkIDMerge.Checked == true && this.rcardsBefore.Count > i)
							{
								messages.AddMessages(SingleIDMerge(this.rcardsBefore[i].ToString(), rcard, messages, actionIDMerge, actionOnLineHelper));
								if (messages.IsSuccess() == false)
								{
									break;
								}
							}
							// Added end
							messages.AddMessages(AddSingleIDIntoOQCLot( rcard ,FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabEditOQCLot.Value)),messages,actionOQCLotAddID,actionOnLineHelper));
							if (messages.IsSuccess() == false)
							{
								break;
							}
						}
						if(!messages.IsSuccess())
						{
							this.DataProvider.RollbackTransaction();
							//循环显示信息
							foreach(string rcard in rcards)
							{
								InitMessage(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabEditOQCLot.Value)),ucLabEditMaxNumber.Checked,oqcMaxSize,rcard,false);
							}
							rcards.Clear();
							this.rcardsBefore.Clear();

							CurrentSNCount = 1;
						}
						else
						{
							this.DataProvider.CommitTransaction();
							//循环显示信息
							foreach(string rcard in rcards)
							{
								InitMessage(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabEditOQCLot.Value)),ucLabEditMaxNumber.Checked,oqcMaxSize,rcard,true);
							}

							rcards.Clear();
							this.rcardsBefore.Clear();
							CurrentSNCount = 1;

							if(txtCarton.Checked == true & txtCarton.Value != String.Empty)
							{
								RefreshCartonNumber();
							}
						}
					}
					catch
					{
						this.DataProvider.RollbackTransaction();
						throw;
					}
					finally
					{
						//Laws Lu,2005/10/19,新增	缓解性能问题
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
					}
				}

				//initView
				InitPostView(new OQCFacade(this.DataProvider));

				//if(messages.
				ApplicationRun.GetInfoForm().Add(messages);

			}

			//Laws Lu,2005/08/11,新增焦点设置
			ucLabEditInputID.TextFocus(true, true);
			//SendKeys.Send("+{TAB}");
			
			// Added by Icyer 2006/06/06
			if (ucLabEditMaxNumber.Value != string.Empty)
			{
				if (IsNumberic(ucLabEditMaxNumber.Value) == true)
				{
					int iQty = Convert.ToInt32(ucLabEditMaxNumber.Value);
					if (Convert.ToInt32(this.ucLabEditCurrentNubmer.Value) >= iQty)
					{
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal,"$Please_Input_Lot_No"));
						ucLabEditOQCLot.Value = string.Empty;
						if (ucLabEditOQCLot.Enabled == false)
						{
							this.btnEnable_Click(null, null);
						}
						Application.DoEvents();
						ucLabEditOQCLot.TextFocus(false, true);

						ClearUI();
						rcards.Clear();
						this.rcardsBefore.Clear();

						CurrentSNCount = 1;

					}
				}
			}

			for(int i = 0 ;i < messages.Count() ; i ++)
			{
				UserControl.Message msg = messages.Objects(i);
				if(msg.Body == "$CARTON_ALREADY_FILL_OUT" || 
					msg.Body == "$CARTON_ALREADY_FULL_PlEASE_CHANGE" )
				{
					Application.DoEvents();
					txtCarton.TextFocus(true, true);
					break;
				}
			}
			// Added end
		}

		private void InitPostView(OQCFacade oqcFacade)
		{
			this.dtIDList.Rows.Clear();
			this.ultraGridMain.DataSource = null;
			//			this.ultraGridMain.Refresh();
			object[] objs = oqcFacade.ExactQueryOQCLot2Card(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabEditOQCLot.Value)),OQCFacade.Lot_Sequence_Default);
			//取出其中包含的IDlist
			if(objs == null)
			{
				InitMessage( FormatHelper.CleanString(this.ucLabEditOQCLot.Value),ucLabEditMaxNumber.Checked,oqcMaxSize);
				ucLabEditCurrentNubmer.Value = "0";
			}
			else
			{
				
				FillUltraWinGrid(this.ultraGridMain,objs);
				InitMessage( FormatHelper.CleanString(this.ucLabEditOQCLot.Value),ucLabEditMaxNumber.Checked,oqcMaxSize,objs);
				ucLabEditCurrentNubmer.Value = objs.Length.ToString();
			}

			
			this.ultraGridMain.DataSource =this.dtIDList;
			

			if(this.ultraGridMain.DisplayLayout.Bands.Count > 0)
			{
				if(this.ultraGridMain.DisplayLayout.Bands[0].Columns.Count > 0)
				{
					this.ultraGridMain.DisplayLayout.Bands[0].Columns["runningcard"].Width = 150;
					this.ultraGridMain.DisplayLayout.Bands[0].Columns["collecttype"].Hidden = true;

					//this.ultraGridMain.DisplayLayout.Bands[0].Columns["runningcard"].Width = 150;
					this.ultraGridMain.DisplayLayout.Bands[0].Columns["checkbox"].Hidden = true;
				}
			}

			// Added by Icyer 2006/06/06
			if (this.ultraGridMain.Rows.Count > 0)
			{
				//				this.ultraGridMain.Selected.Rows.Clear();
				//				this.ultraGridMain.Rows[this.ultraGridMain.Rows.Count - 1].Selected = true;
				//
				//				// Added by karron 2006-6-27
				//				this.ultraGridMain.ActiveRow = this.ultraGridMain.Rows[this.ultraGridMain.Rows.Count - 1];

//				foreach(UltraGridRow row in this.ultraGridMain.Rows)
				for ( int iGridRowLoopIndex = 0; iGridRowLoopIndex < this.ultraGridMain.Rows.Count; iGridRowLoopIndex++)
				{
					UltraGridRow row = this.ultraGridMain.Rows[iGridRowLoopIndex];
					if(row.Cells["runningcard"].Value.ToString() == this.ucLabEditInputID.Value)
					{
						this.ultraGridMain.ActiveRow = row;
						row.Selected = true;

						break;
					}
				}

				//end
			}
			// Added end

			this.ucLabEditInputID.Value = string.Empty;

			//SetcbRemixMOStatus();
		}

		private bool InvalidData()
		{
			bool valid = true;
			if(ucLabEditMaxNumber.Checked)
			{
				try
				{
					oqcMaxSize = System.Decimal.Parse(this.ucLabEditMaxNumber.Value);
				}
				catch
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CSError_PackingQuantity_Must_Be_Decimail(包装数量必须输入数据类型！)"));
					valid = false;
				}
			}

			if(chkSNSet.Checked)
			{
				int iLength = 0;
				try
				{
					TotalSNCount = System.Int32.Parse(chkSNSet.Value);
					if(!txtItemName.Checked)
					{
						iLength = System.Int32.Parse(this.chkSNLastCHR.Value);
					}
				}
				catch
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CSError_PackingQuantity_Must_Be_Decimail(必须输入配套尾数位数！)"));

					TotalSNCount = 1;
					valid = false;

				}

				if(rcards.Count > 0)
				{
					//Laws Lu,2007./01/09
					if(txtItemName.Checked == true && SNStartPosition != SNEndPosition)
					{
						string match = rcards[0].ToString();
						//Laws Lu,2006/06/05	modify 
						match = match.Substring(SNStartPosition,SNEndPosition - SNStartPosition + 1);
						foreach(string rcard in rcards)
						{
							if(rcard.Length > iLength)
							{
								string subRCard = rcard.Substring(SNStartPosition,SNEndPosition - SNStartPosition + 1);
							
								if(subRCard != match)
								{
									ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$SN_NOT_BE_SET"));

									rcards.Remove(rcard);
									valid = false;
									break;
								}
							}
						}
					}
					else
					{
						string match = rcards[0].ToString();
						//Laws Lu,2006/06/05	modify 
						match = match.Substring(match.Length - iLength,iLength);
						foreach(string rcard in rcards)
						{
							if(rcard.Length > iLength)
							{
								string subRCard = rcard.Substring(rcard.Length - iLength,iLength);
							
								if(subRCard != match)
								{
									ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$SN_NOT_BE_SET"));

									rcards.Remove(rcard);
									valid = false;
									break;
								}
							}
						}
					}
				}
				
			}

			return valid;
		}

		//对单个的ID进行对立
		private Messages AddSingleIDIntoOQCLot(string ID,string OQCLOtNO,Messages messages,IAction action,ActionOnLineHelper actionOnLineHelper)
		{
			Messages newMessages = actionOnLineHelper.GetIDInfo(ID);
			string cartonno = FormatHelper.CleanString(this.txtCarton.Value);


			if ( newMessages.IsSuccess() )
			{
				ProductInfo product= (ProductInfo)newMessages.GetData().Values[0];
				if(product.LastSimulation == null)
				{
					newMessages.Add(new UserControl.Message( new Exception("$Error_LastSimulation_IsNull!"))); 
					return newMessages;
				}
				OQCLotAddIDEventArgs oqcLotAddIDEventArgs = new OQCLotAddIDEventArgs(
					ActionType.DataCollectAction_OQCLotAddID,ID
					,ApplicationService.Current().
					UserCode
					,ApplicationService.Current().ResourceCode
					,FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.ucLabEditOQCLot.Value))
					,FormatHelper.CleanString(this.ultraOptionSetOQCType.CheckedItem.DataValue.ToString())
					,this.ucLabEditMaxNumber.Checked
					,oqcMaxSize,
					cbRemixMO.Checked
					,product);
	
				//Laws Lu,2006/05/27	CartonNo
				oqcLotAddIDEventArgs.CartonNo = cartonno;
				oqcLotAddIDEventArgs.CartonMemo = FormatHelper.CleanString(this.txtCartonMemo.Value);
				oqcLotAddIDEventArgs.CollectType = this.ultraOptionSetOQCExameOpion.CheckedItem.DataValue.ToString();

				newMessages.AddMessages(action.Execute(oqcLotAddIDEventArgs));	
			}
			return newMessages;
		}

		/// <summary>
		/// 单个ID做序号转换
		/// </summary>
		private Messages SingleIDMerge(string ID,string newID,Messages messages,IAction action,ActionOnLineHelper actionOnLineHelper)
		{
			Messages newMessages = actionOnLineHelper.GetIDInfo(ID);

			if ( newMessages.IsSuccess() )
			{
				ProductInfo product= (ProductInfo)newMessages.GetData().Values[0];
				if(product.LastSimulation == null)
				{
					newMessages.Add(new UserControl.Message( new Exception("$Error_LastSimulation_IsNull!"))); 
					return newMessages;
				}

				/* modified by jessie lee, 2005/12/9, CS188-3
					 * 1,包装段的产品被拆解或报废后，可能再次投入到包装段，但是由于EMP平台的手机无法使用新的IMEI号，
					 * 就存在着重用IMEI号的问题，而且该IMEI号码有可能投入到原来的工单。顾问给客户的解决方案是不良拆
					 * 解或报废后的手机再次投入包装时粘贴新的M条码（IMEI序号转换前的号码），该号码可以归属到原来的工
					 * 单或新工单，序号转换时系统允许将M条码转换成曾经使用过的IMEI号
					 * 2,RMA等客诉返工的EMP平台的产品返工生产时同样可能存在重复使用IMEI号的问题。顾问给出的第一种解
					 * 决方式是开立返工类型的工单，以IMEI号投入新的返工工单，开始生产活动，但是新的生产流程不需要包
					 * 涵序号转换工序；第二种方式是粘贴新的M条码投入到正常工单中，序号转换时，如果出现IMEI号码重复使
					 * 用则必须保证原来使用IMEI号的工单已经关单。客户要求采用第二种方法。 
					 * 
					 * 2中客户要求采用第二种方法，那么采集进来的rcard是没有simulation记录的，所以不会进入这里check
					 * */
				/* 不是序列号转换工序，保持原来的逻辑 */

				/* 是序列号转换工序 
					 * 转换前的rcard 和 转换后的rcard  不相同
					 * 不同， check IMEI 重复使用
					 * */
				if( string.Compare(newID,ID,true) != 0 )  
				{
					messages.AddMessages(actionOnLineHelper.GetIDInfo(newID));
					if(messages.IsSuccess() && ((ProductInfo)messages.GetData().Values[0]).LastSimulation != null)
					{
						string bMoCode = ((ProductInfo)messages.GetData().Values[0]).LastSimulation.MOCode;
						string aMoCode = product.LastSimulation.MOCode;
						
						/* 判断这个IMEI号是否报废或者拆解 */
						bool isSpliteOrScrape = CheckIMEISpliteOrScrape(
							((ProductInfo)messages.GetData().Values[0]).LastSimulation.RunningCard,
							((ProductInfo)messages.GetData().Values[0]).LastSimulation.RunningCardSequence,
							aMoCode) ;
						if( !isSpliteOrScrape )
						{
							/* rcard 完工，工单未关 */
							if(((ProductInfo)messages.GetData().Values[0]).LastSimulation.IsComplete != "1"
								&& (((ProductInfo)messages.GetData().Values[0]).LastSimulation.ProductStatus != ProductStatus.OffMo))
							{
								newMessages.Add(new UserControl.Message(MessageType.Error,"$Error_CS_ID_Already_Exist"));//序列号已存在
							
								return newMessages;
							}
						}
					}
				}
				

				SplitIDActionEventArgs args = new SplitIDActionEventArgs(
					ActionType.DataCollectAction_Split, 
					product.LastSimulation.RunningCard, 
					ApplicationService.Current().UserCode,
					ApplicationService.Current().ResourceCode,
					product, 
					new string[]{newID},
					IDMergeType.IDMERGETYPE_IDMERGE,
					false,
					Convert.ToInt32(product.LastSimulation.RunningCardSequence),
					true);
				args.NeedUpdateReport = true;

				newMessages.AddMessages(action.Execute(args));	
			}
			return newMessages;
		}

		private Messages DeletSingleIDFromOQCLot(string ID,string OQCLotNO,Messages messages,IAction action,ActionOnLineHelper actionOnLineHelper)
		{
			Messages newMessages = actionOnLineHelper.GetIDInfo(ID);
			if ( newMessages.IsSuccess() )
			{
				ProductInfo product= (ProductInfo)newMessages.GetData().Values[0];

				OQCLotRemoveIDEventArgs oqcLotRemoveIDEventArgs = new OQCLotRemoveIDEventArgs(ActionType.DataCollectAction_OQCLotRemoveID,ID,ApplicationService.Current().
					UserCode,ApplicationService.Current().ResourceCode,FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.ucLabEditOQCLot.Value)),product);

				newMessages.AddMessages(action.Execute(oqcLotRemoveIDEventArgs));	
			}
			return newMessages;
		}

		private void uBtnIDDelete_Click(object sender, System.EventArgs e)
		{
			System.Collections.Specialized.NameValueCollection IDList = new System.Collections.Specialized.NameValueCollection();
			Messages messages = new Messages();
			OQCFacade fac = new OQCFacade(this._domainDataProvider);
			
			//列表中没有数据
			if(ultraGridMain.Rows.Count < 1)
			{
				object objOQC = fac.GetOQCLot(
					ucLabEditOQCLot.Value.ToUpper().Trim(),
					OQCFacade.Lot_Sequence_Default);
				if (objOQC != null)
				{
					OQCLot lot = (OQCLot)objOQC;

                    if (DialogResult.OK == MessageBox.Show(MutiLanguages.ParserMessage("$ConformDelete"), this.Text, MessageBoxButtons.OKCancel))
					{
						object[] objs = fac.ExactQueryOQCLot2Card(lot.LOTNO,lot.LotSequence);

						if(objs == null)
						{
							try
							{
								this.DataProvider.BeginTransaction();
								fac.DeleteOQCLot(lot);
								this.DataProvider.CommitTransaction();
							}
							catch
							{
								this.DataProvider.RollbackTransaction();
								throw;
							}

							messages.Add(new UserControl.Message(MessageType.Success,"$CS_Delete_Success"));

							ucLabEditOQCLot.Value = String.Empty;
						}
						else
						{
							messages.Add(new UserControl.Message(MessageType.Error,"$CS_LOT_NOT_EXIST $CS_OR $CS_NOT_CONFIRM"));
						}
					}
				}
				else
				{
					messages.Add(new UserControl.Message("$CS_LOT_NOT_EXIST"));
				}
			}
			else
			{
				//获取选中的ID列表
//				foreach(UltraGridRow ugr in ultraGridMain.Rows)
				for ( int iGridRowLoopIndex = 0; iGridRowLoopIndex < ultraGridMain.Rows.Count; iGridRowLoopIndex++)
				{
					UltraGridRow ugr = ultraGridMain.Rows[iGridRowLoopIndex];
					if(ugr.Selected == true)
					{
						IDList.Add(ugr.Cells["runningcard"].Text,ugr.Cells["collecttype"].Text);
					}

				}
				if(IDList.Count < 1)
				{
					messages.Add(new UserControl.Message(MessageType.Error,"$Error_CS_Select_ID_To_Delete"));
					
				}
				else
				{
					this.DataProvider.BeginTransaction();

					try
					{
						ArrayList alreadyDeleted = new ArrayList();
						foreach(object obj in IDList)
						{
							string id = obj.ToString().ToUpper().Trim();
							if(IDList[id].Trim() ==  OQCFacade.OQC_ExameObject_PCS)
							{
								messages.AddMessages(DeletePCS(id));
							}
							if(IDList[id].Trim() ==  OQCFacade.OQC_ExameObject_PlanarCode)
							{
								FStockRemove form = new FStockRemove(this);
								form.Text = this.Text;

								if ( form.ShowDialog() == DialogResult.OK )
								{
									BarCodeParse barParser = new BarCodeParse(this._domainDataProvider);

									string[] parseList = barParser.GetIDList( PlanateNum );
									foreach(string str in parseList)
									{
										if(!alreadyDeleted.Contains(str))
										{
											messages.AddMessages(DeletePlanate(str));

											alreadyDeleted.Add(str);
										}
									}
								
								}
							}
					
						}
						this.DataProvider.CommitTransaction();

						this.dtIDList.Rows.Clear();
						this.ultraGridMain.DataSource = null;
						//			this.ultraGridMain.Refresh();
						object[] objs = fac.ExactQueryOQCLot2Card(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabEditOQCLot.Value)),OQCFacade.Lot_Sequence_Default);
						//取出其中包含的IDlist
						if(objs != null)
						{
				
							FillUltraWinGrid(this.ultraGridMain,objs);
							ucLabEditCurrentNubmer.Value = objs.Length.ToString();
						}

			
						this.ultraGridMain.DataSource =this.dtIDList;
						this.ucLabEditInputID.Value = string.Empty;

						if(this.ultraGridMain.DisplayLayout.Bands.Count > 0)
						{
							if(this.ultraGridMain.DisplayLayout.Bands[0].Columns.Count > 0)
							{
								this.ultraGridMain.DisplayLayout.Bands[0].Columns["checkbox"].Hidden = true;
								this.ultraGridMain.DisplayLayout.Bands[0].Columns["collecttype"].Hidden = true;
							}
						}
					}
					catch(Exception E)
					{
						this.DataProvider.RollbackTransaction();

						messages.Add(new UserControl.Message(E));
					}
					finally
					{
						(DataProvider as SQLDomainDataProvider).PersistBroker.CloseConnection();
					}
				}
			}

			if(this.ultraGridMain.Rows.Count == 0)
			{
				ucLabEditOQCLot.Value = String.Empty;
			}

			RefreshCartonNumber();

			ApplicationRun.GetInfoForm().Add(messages);

			ucLabEditOQCLot.TextFocus(false, true);
		}

		//Laws Lu,2005/08/30,保存二维条码
		public string PlanateNum;

		private Messages DeletePlanate(string id)
		{
			#region 移除二维条码
			Messages msg = new Messages();
			
			// 检查序列号是否已在List中存在
			ActionOnLineHelper actionOnLineHelper = new ActionOnLineHelper(this.DataProvider);
			
			ActionFactory actionFactory = new ActionFactory(this.DataProvider);
			IAction action = actionFactory.CreateAction(ActionType.DataCollectAction_OQCLotRemoveID);

			try
			{

				//this.DataProvider.BeginTransaction();
				msg.AddMessages( DeletSingleIDFromOQCLot(
					FormatHelper.PKCapitalFormat(FormatHelper.CleanString(id.ToString())),
					FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabEditOQCLot.Value)),msg,action,actionOnLineHelper));

				//this.DataProvider.CommitTransaction();
				return msg;
			}
			catch
			{
				//this.DataProvider.RollbackTransaction();
				throw ;
			}
			#endregion
		}

		private Messages DeletePCS(string id)
		{
			#region PCS删除
			Messages messages = new Messages();

            if (MessageBox.Show(MutiLanguages.ParserMessage("$ConformDelete"), this.Text, MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return messages;
			}

			ActionOnLineHelper actionOnLineHelper = new ActionOnLineHelper(this.DataProvider);
			
			ActionFactory actionFactory = new ActionFactory(this.DataProvider);
			IAction action = actionFactory.CreateAction(ActionType.DataCollectAction_OQCLotRemoveID);

			try
			{
				//this.DataProvider.BeginTransaction();
				messages.AddMessages( DeletSingleIDFromOQCLot(
					FormatHelper.PKCapitalFormat(FormatHelper.CleanString(id)),
					FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabEditOQCLot.Value)),messages,action,actionOnLineHelper));

				//this.DataProvider.CommitTransaction();
				return messages;
			}
			catch
			{
				//this.DataProvider.RollbackTransaction();
				throw;
			}
			#endregion
		}

		#endregion


		#region object<->UI
		private void ClearUI()
		{
			this.dtIDList.Rows.Clear();
			//			this.ultraGridMain.Refresh();
			this.ucMessage.Clear();
			this.ucLabEditCurrentNubmer.Value = string.Empty;
		}

		public OQCLot CreateNewOQCLot()
		{
			OQCFacade _oqcFacade = new OQCFacade(DataProvider);
			OQCLot  newOQCLot = _oqcFacade.CreateNewOQCLot();

			//			Domain.BaseSetting.StepSequence ss = new BenQGuru.eMES.Domain.BaseSetting.StepSequence();
			//
			//			object objRes =  (new BaseSetting.BaseModelFacade(DataProvider)).GetResource(ApplicationService.Current().ResourceCode);

			//object objLine = (new BaseSetting.BaseModelFacade(DataProvider)).GetStepSequence(

			//Laws Lu,2006/06/21,新增	get ShiftDay
			BaseSetting.BaseModelFacade dataModel = new BaseSetting.BaseModelFacade(this.DataProvider);
			Domain.BaseSetting.Resource res = (Domain.BaseSetting.Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);
			int shiftDay = 0;
			
			BaseSetting.ShiftModelFacade shiftModel	= new BaseSetting.ShiftModelFacade(this.DataProvider);
			Domain.BaseSetting.TimePeriod  period	= (Domain.BaseSetting.TimePeriod)shiftModel.GetTimePeriod(res.ShiftTypeCode,Web.Helper.FormatHelper.TOTimeInt(DateTime.Now));		
			if (period==null)
			{
				throw new Exception("$OutOfPerid");
			}
				
			if ( period.IsOverDate == Web.Helper.FormatHelper.TRUE_STRING )
			{
				if ( period.TimePeriodBeginTime < period.TimePeriodEndTime )
				{
					shiftDay =	FormatHelper.TODateInt(DateTime.Now.AddDays(-1)) ;
				}
				else if ( Web.Helper.FormatHelper.TOTimeInt(DateTime.Now) < period.TimePeriodBeginTime)
				{
					shiftDay =	FormatHelper.TODateInt(DateTime.Now.AddDays(-1)) ;
				}
				else
				{
					shiftDay = FormatHelper.TODateInt(DateTime.Now) ;
				}
			}
			else
			{
				shiftDay = FormatHelper.TODateInt(DateTime.Now) ;
			}
			//get lot entity
			object objLot = (new OQCFacade(DataProvider)).GetNewLotNO(res.StepSequenceCode,shiftDay.ToString());

			if(objLot == null)
			{
				newOQCLot.LOTNO = shiftDay.ToString() + res.StepSequenceCode + "-001";
			}
			else
			{
				string oldLotNO = (objLot as OQCLot).LOTNO;
				string newSequence = Convert.ToString((int.Parse(oldLotNO.Substring(oldLotNO.Length - 3 ,3)) + 1));
				newOQCLot.LOTNO = oldLotNO.Substring(0,oldLotNO.Length - 3) + newSequence.PadLeft(3,'0');
			}
			newOQCLot.AcceptSize = 0;
			newOQCLot.AcceptSize1 = 0;
			newOQCLot.AcceptSize2 = 0;
			newOQCLot.AQL =0;
			newOQCLot.AQL1 =0;
			newOQCLot.AQL2 =0;
			newOQCLot.LotSequence = OQCHelper.Lot_Sequence_Default;
			newOQCLot.OQCLotType = FormatHelper.CleanString(this.ultraOptionSetOQCType.CheckedItem.DataValue.ToString());
			newOQCLot.LotSize= 0;
			newOQCLot.LOTStatus = OQCLotStatus.OQCLotStatus_Initial;
			newOQCLot.LOTTimes =0;
			newOQCLot.MaintainUser = ApplicationService.Current().UserCode;
			newOQCLot.RejectSize = 0;
			newOQCLot.RejectSize1 =0;
			newOQCLot.RejectSize2 =0;
			newOQCLot.SampleSize =0;

			if(ultraOptionSetOQCType.CheckedItem.DataValue.ToString() == OQCLotType.OQCLotType_ReDO) //"再次检验批")
			{
				if(this.txtMemo.Value.Length == 0)
					throw new Exception("$Please_Input_Memo");

				newOQCLot.OldLotNo = this.txtMemo.Value;
			}

			return newOQCLot;
		}

		#endregion

		private bool ValidateRcards(string[] rcards)
		{
			bool bResult = true;
			//bool bTmp = true;

			foreach(string rcard in rcards)
			{
				if(rcard != String.Empty)
				{
					long lctnNO = FormatHelper.DecFrom36(rcard.Substring(rcard.Length - 4,4));

					string cartonNO = txtCarton.Value.Trim();

					long currentCTNNO = long.Parse(cartonNO.Substring(15,6));

					int currentLen = int.Parse(cartonNO.Substring(cartonNO.Length - 2,2));

					if(lctnNO >  currentCTNNO * currentLen - 1 || lctnNO < (currentCTNNO - 1) * currentLen )
					{
						bResult = false;
						break;
					}

				}
			}

			//bResult = bTmp;

			return bResult;
		}

		private void RefreshCartonNumber()
		{
			object obj = (new Package.PackageFacade(DataProvider)).GetCARTONINFO(txtCarton.Value.Trim().ToUpper());

			if(obj != null)
			{
				txtCartonCapacity.Value = (obj as Domain.Package.CARTONINFO).COLLECTED.ToString();
			}
			else
			{
				txtCartonCapacity.Value = "0";
			}
		}

		#region file parse
		private string[] GetIDsByOQCExameObject(string idStrings,string exameObject)
		{
			string[] returnStringIDs = null;
			if(exameObject ==OQCFacade.OQC_ExameObject_PlanarCode)
			{
				BarCodeParse barCodeParse = new BarCodeParse(_domainDataProvider);
				returnStringIDs = barCodeParse.GetIDList(idStrings);
			}
			if(exameObject == OQCFacade.OQC_ExameObject_PCS)
			{
				returnStringIDs = null;
			}
			if(exameObject== OQCFacade.OQC_ExameObject_PCS)
			{
				returnStringIDs[0] =  idStrings;
			}
			return returnStringIDs;
		}
		#endregion

		#region 公共的函数
		private void FillUltraWinGrid(Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid,object[] objs)
		{
			this.dtIDList.Rows.Clear();
			for(int i=0;i<objs.Length;i++)
			{
				this.dtIDList.Rows.Add( new object[] {
														 false
														 ,((OQCLot2Card)objs[i]).RunningCard
														 ,((OQCLot2Card)objs[i]).ItemCode
														 ,((OQCLot2Card)objs[i]).MOCode
														 ,((OQCLot2Card)objs[i]).StepSequenceCode
														 ,((OQCLot2Card)objs[i]).EAttribute1
														 ,((OQCLot2Card)objs[i]).CollectType
													 });

				dtIDList.AcceptChanges();
			}
			//			ultraGrid.Refresh();
		}
		#endregion

		#region 显示的信息
		private void InitMessage(string lotNO,bool isCheckMaxSize,decimal OQCMaxSize)
		{
			this.ucMessage.Clear();
			this.ucMessage.Add("抽检批号: "+lotNO);
			InvalidData();
			if(isCheckMaxSize)
			{
				this.ucMessage.Add(">>$CS_Please_Input_RunningCard "//批个数:1/"+OQCMaxSize.ToString()
					+ " 套数:" + CurrentSNCount.ToString() + "/" + TotalSNCount.ToString());
			}
			else
			{
				this.ucMessage.Add(">>$CS_Please_Input_RunningCard 套数:" + CurrentSNCount.ToString() + "/" + TotalSNCount.ToString());
			}
		}

		public void txtCarton_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				if (txtCarton.Value.Trim() != string.Empty)
				{
					if(txtItemName.Checked == true && cartonCodeLen != 0)
					{
						if(txtCarton.Value.Length != cartonCodeLen)
						{
							this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_CARTON_CODE_LEN_ERROR " + cartonCodeLen.ToString()));
							SendKeys.Send("{TAB}");
							//Application.DoEvents();
							this.txtCarton.TextFocus(false, true);
                            //Remove UCLabel.SelectAll;
							
							return;
						}
					}

					if( txtItemName.Checked == true && cartonPreffix != String.Empty)
					{
						if(txtCarton.Value.Trim().Length < cartonPreffix.Length)
						{
							this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_CARTON_CODE_PREFFIX " + cartonPreffix));
							SendKeys.Send("{TAB}");
							//Application.DoEvents();
							this.txtCarton.TextFocus(false, true);
                            //Remove UCLabel.SelectAll;
							
							return;
						}
						if(txtCarton.Value.Trim().Substring(0,cartonPreffix.Length) != cartonPreffix)
						{
							this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_CARTON_CODE_PREFFIX " + cartonPreffix));
							SendKeys.Send("{TAB}");
							//Application.DoEvents();
							this.txtCarton.TextFocus(false, true);
                            //Remove UCLabel.SelectAll;
							
							return;
						}
					}
					
						
					string countlot="select count(distinct(lotno)) from tblsimulation where cartoncode='"+txtCarton.Value.Trim()+"'";
					string carton2lot="select distinct(lotno) from tblsimulation where cartoncode='"+txtCarton.Value.Trim()+"'";
					DataSet ds=((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(countlot);
					
					if ( ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
					{
						if (int.Parse(ds.Tables[0].Rows[0][0].ToString()) > 1)
						{
							this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_LOT_DUPLICATE"));
							SendKeys.Send("{TAB}");
							//Application.DoEvents();
							this.txtCarton.TextFocus(false, true);
                            //Remove UCLabel.SelectAll;
							
							return;
						}
						else if (int.Parse(ds.Tables[0].Rows[0][0].ToString()) ==1)
						{

							DataSet dt=((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(carton2lot);
							if ( dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
							{
								if (dt.Tables[0].Rows[0][0].ToString() != ucLabEditOQCLot.Value.Trim())
								{
									this.ucMessage.Add(new UserControl.Message(MessageType.Error,"该箱已归属于:"+dt.Tables[0].Rows[0][0].ToString()));
									SendKeys.Send("{TAB}");
									//Application.DoEvents();
									this.txtCarton.TextFocus(false, true);
                                    //Remove UCLabel.SelectAll;

									return;
								}
							}
						}
					}
					
					RefreshCartonNumber();
					Application.DoEvents();
					this.ucLabEditInputID.TextFocus(false, true);
					
				}
			}
		}

		private void chkSNSet_InnerTextChanged(object sender, System.EventArgs e)
		{
			if(chkSNSet.Value.Trim() != String.Empty)
			{
				TotalSNCount = int.Parse(chkSNSet.Value.Trim());
				Application.DoEvents();
				this.txtCarton.Checked = true;
				this.txtCarton.TextFocus(false, true);
			}
		}

		private void FGenLot_Resize(object sender, System.EventArgs e)
		{
			ucMessage.Height = Convert.ToInt32(groupBox3.Height * .82);
		}

		private void chkSNSet_CheckBoxCheckedChanged(object sender, System.EventArgs e)
		{
			if (chkSNSet.Checked )
			{
				chkSNLastCHR.Enabled =true;
				
			}
			else
			{
				//Laws Lu,2006/06/22
				if(CurrentSNCount == TotalSNCount || 
					(CurrentSNCount == 1))
				{
					chkSNLastCHR.Checked =false;
					chkSNLastCHR.Enabled =false;

					TotalSNCount = 1;
				}
				//				CurrentSNCount = 1;
				//				TotalSNCount = 1;
			}
		}

		private void cbCheckLength_CheckedChanged(object sender, System.EventArgs e)
		{
			if(cbCheckLength.Checked)
			{
				txtLength.Enabled = true;
			}
			else
			{
				txtLength.Enabled = false;
			}
		}

		private void btnEnable_Click(object sender, System.EventArgs e)
		{
			if(ucLabEditOQCLot.Enabled)
			{
				ucLabEditOQCLot.Enabled = false;
				btnEnable.Caption = "解除锁定";
			}
			else
			{
				ucLabEditOQCLot.Enabled = true;
				ucLabEditOQCLot.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
				btnEnable.Caption = "锁定";
			}
		}
		//Laws Lu,2005/08/12,新增设置焦点
		private void FGenLot_Activated(object sender, System.EventArgs e)
		{
			ucLabEditOQCLot.TextFocus(false, true);
		}
		

		//设置允许不同工单采集状态为不可选
		private void SetcbRemixMOStatus()
		{
			if(dtIDList.Rows.Count > 0)
			{
				if(CheckDiffMO())
				{
					cbRemixMO.Checked = true;
				}
				else
				{
					cbRemixMO.Checked = false;
				}
			}
		}
		//Laws Lu,2005/08/11,检查当前选中的产品是否存在于不同工单
		private bool CheckDiffMO()
		{
			foreach(DataRow dr in dtIDList.Rows)
			{
				DataRow[] drs = dtIDList.Select("mocode ='" + dr["mocode"].ToString().Trim() + "'");
				if(drs.Length 
					== dtIDList.Rows.Count)
				{
					return false;
				}
			}

			return true;
		}
		//Laws Lu,2005/08/11,Lucky 提出 CS0025,产生送检批资料过程中增加对“允许不同工单采集”勾选项的检查，
		//具体逻辑如下：取消勾选“允许混单采集”时系统需要先判断是否已经采集了不同工单的产品序列号，
		//如果已经采集了则提示用户不能取消该勾选项；
		//输入送检批时检查该送检批中是否已经存在不同工单的产品序列号，
		//如果已经存在则将“允许混单采集”勾选项变更为已勾选状态，并且不允许User修改
		private void cbRemixMO_CheckedChanged(object sender, System.EventArgs e)
		{
			if(dtIDList.Rows.Count > 0)
			{
				if(cbRemixMO.Checked == false)
				{
					if(CheckDiffMO())
					{
						cbRemixMO.Checked = true;
					}
				}
			}
		}

		private void FGenLot_Closed(object sender, System.EventArgs e)
		{
			if (this.DataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();  
		}

	
		//Laws Lu,2005/08/11,注释
		//		private void ultraOptionSetOQCExameOpion_ValueChanged_1(object sender, System.EventArgs e)
		//		{
		//			if(this.ultraOptionSetOQCExameOpion.CheckedItem.DataValue.ToString() ==OQCFacade.OQC_ExameObject_PCS)
		//			{
		//				this.uBtnIDDelete.Enabled = true;
		//			}
		//			else
		//			{
		//				this.uBtnIDDelete.Enabled = false;
		//			}
		//		
		//		}

		

		

		private void InitMessage(string lotNo,bool isCheckMaxSize,decimal OQCMaxSize,object[] objs)
		{
			InvalidData();
			if(isCheckMaxSize)
			{
				if(OQCMaxSize == objs.Length)
				{
					this.ucMessage.Add(">>该批输入完成");
				}
				else
				{
					this.ucMessage.Add(">>$CS_Please_Input_RunningCard "//批个数:1/"+OQCMaxSize.ToString()
						+ " 套数:" + CurrentSNCount.ToString() + "/" + TotalSNCount.ToString());
				}
			}
			else
			{
				this.ucMessage.Add(">>$CS_Please_Input_RunningCard 套数:" + CurrentSNCount.ToString() + "/" + TotalSNCount.ToString());
			}

			//			if(isCheckMaxSize)
			//			{
			//				if(OQCMaxSize == objs.Length)
			//				{
			//					this.ucMessage.Add(">>该批输入完成");
			//				}
			//				else
			//				{
			//					this.ucMessage.Add(">>$CS_Please_Input_RunningCard  "+(objs.Length+1)+"/"+OQCMaxSize.ToString());
			//				}
			//			}
			//			else
			//			{
			//				this.ucMessage.Add(">>$CS_Please_Input_RunningCard  ");
			//			}
		}

		private void InitMessage(string lotNo,bool isCheckMaxSize,decimal OQCMaxSize,string runningCard,bool result)
		{
			InvalidData();
			this.ucMessage.Add("<<"+runningCard);
			if(result)
			{
				this.ucMessage.Add(new UserControl.Message(MessageType.Success,"成功添加" + runningCard));
			}
			else
			{
				this.ucMessage.Add(new UserControl.Message(MessageType.Error,"添加"  +runningCard + "失败"));
			}
		}
		#endregion

		private void chkIDMerge_CheckedChanged(object sender, EventArgs e)
		{
			if( !this.chkIDMerge.Checked )
			{
				this.aCardTransLenULE.Checked = this.chkIDMerge.Checked;
				this.aCardTransLenULE.Checked = this.chkIDMerge.Checked;
				this.aCardTransLenULE.Checked = this.chkIDMerge.Checked;
				this.aCardTransLenULE.Checked = this.chkIDMerge.Checked;
			}

			this.aCardTransLenULE.Enabled		= this.chkIDMerge.Checked;
			this.aCardTransLetterULE.Enabled	= this.chkIDMerge.Checked;
			this.bCardTransLenULE.Enabled		= this.chkIDMerge.Checked;
			this.bCardTransLetterULE.Enabled	= this.chkIDMerge.Checked;
		}

		public void btnAutoGenLot_Click(object sender, System.EventArgs e)
		{
			
			//Laws Lu,2006/06/21
			
			OQCFacade oqcFacade = new OQCFacade(DataProvider);
			OQCLot lot = null;//CreateNewOQCLot();

			//Laws Lu,2006/12/28 减少连接数
			((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
			((SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			try
			{
				lot = CreateNewOQCLot();
			}
			catch(Exception ex)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));
				this.txtMemo.TextFocus(false, true);

				return;
			}


			DataProvider.BeginTransaction();
			try
			{
				//产生送检验批号
				oqcFacade.AddOQCLot(lot);

				DataProvider.CommitTransaction();
			}
			catch(Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				this.ucMessage.Add(new UserControl.Message(ex));
			}
			finally
			{
				//Laws Lu,2005/10/19,新增	缓解性能问题
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
			}

			btnEnable_Click(sender,e);
			this.ucLabEditOQCLot.Value = lot.LOTNO;
			this.ucLabEditOQCLot.TextFocus(false, true);
			System.Windows.Forms.SendKeys.Send("\r");
		}

		private void btnLockCheck_Click(object sender, System.EventArgs e)
		{
			if(btnLockCheck.Caption == "解除锁定")
			{
				bCardTransLenULE.InnerTextBox.ReadOnly = false;
				bCardTransLenULE.InnerCheckBox.Enabled = true;
				
				bCardTransLetterULE.InnerTextBox.ReadOnly = false;
				bCardTransLetterULE.InnerCheckBox.Enabled = true;
				
				aCardTransLenULE.InnerTextBox.ReadOnly = false;
				aCardTransLenULE.InnerCheckBox.Enabled = true;
				
				aCardTransLetterULE.InnerTextBox.ReadOnly = false;
				aCardTransLetterULE.InnerCheckBox.Enabled = true;
				

				//				ucLabEditOQCLot.TextFocus(false, true);
				//				ucLabEditOQCLot.SelectAll(1,2,3);
				btnLockCheck.Caption = "锁定";
			}
			else
			{
				btnLockCheck.Caption = "解除锁定";
				

				bCardTransLenULE.InnerTextBox.ReadOnly = true;
				bCardTransLenULE.InnerCheckBox.Enabled = false;
				
				bCardTransLetterULE.InnerTextBox.ReadOnly = true;
				bCardTransLetterULE.InnerCheckBox.Enabled = false;

				aCardTransLenULE.InnerTextBox.ReadOnly = true;
				aCardTransLenULE.InnerCheckBox.Enabled = false;
				
				aCardTransLetterULE.InnerTextBox.ReadOnly = true;
				aCardTransLetterULE.InnerCheckBox.Enabled = false;
			}
		}

		private void btnSendExam_Click(object sender, System.EventArgs e)
		{
			if(ucLabEditOQCLot.Value.Trim() == string.Empty) //(this.ucLabEditLotNO.Value.Trim() == string.Empty)
			{
				return;
			}

			OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
			object objLot = oqcFacade.GetOQCLot(FormatHelper.CleanString(ucLabEditOQCLot.Value.Trim()),OQCHelper.Lot_Sequence_Default);
			if(objLot != null)
			{
				OQCLot oqcLot = objLot as OQCLot;

				if(oqcLot.LOTStatus == Web.Helper.OQCLotStatus.OQCLotStatus_Initial)
				{
					oqcLot.LOTStatus = Web.Helper.OQCLotStatus.OQCLotStatus_SendExame;

					DataProvider.BeginTransaction();
					try
					{
						oqcFacade.UpdateOQCLotStatus(oqcLot);//update lot status

						this.DataProvider.CommitTransaction();

						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_LOT_SEND_EXAM_SUCCESS"));
					}
					catch(Exception ex)
					{
						this.DataProvider.RollbackTransaction();
						throw ex;
					}
					finally
					{
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
					}
				}
			}

		}

		/// <summary>
		/// 检查当前rcard是否拆解或则报废
		/// </summary>
		/// <param name="rcard"></param>
		/// <param name="rcardseq"></param>
		/// <param name="mocode"></param>
		/// <returns></returns>
		private bool CheckIMEISpliteOrScrape( string rcard,decimal rcardseq,string mocode )
		{
			string sql = string.Format(" select count(*) from tblts where rcard='{0}' and rcardseq={1} and mocode='{2}' and tsstatus in ('{3}','{4}')",
				rcard,rcardseq,mocode,TSStatus.TSStatus_Scrap,TSStatus.TSStatus_Split);
			int count = this.DataProvider.GetCount( new SQLCondition(sql) );
			if( count>0 )
			{
				return true;
			}
			return false;
		}

		private void txtItemName_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
//			//Laws Lu,2006/12/28 减少连接数
//			((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
//			((SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();

			if(txtItemName.Value.Trim() != String.Empty)
			{
				ItemFacade itemFac = new ItemFacade(DataProvider);

				object objItem2CartonCFG = itemFac.GetItem2CartonCFG(txtItemName.Value.Trim().ToUpper());
				if(objItem2CartonCFG != null)
				{
					Domain.MOModel.Item2CartonCFG entity = objItem2CartonCFG as Domain.MOModel.Item2CartonCFG;
				
					chkSNSet.Checked  = true;
					chkSNSet.Value = Convert.ToString(Convert.ToInt32(entity.PCSTYPE) + 1);

					cartonPreffix = entity.CartonItemNo;
					cartonCodeLen = Convert.ToInt32(entity.CartonLabelLen);
					SNStartPosition = Convert.ToInt32(entity.StartPosition - 1);
					SNEndPosition = Convert.ToInt32(entity.EndPosition - 1 );
				}
			}
		}
		

	}
}

