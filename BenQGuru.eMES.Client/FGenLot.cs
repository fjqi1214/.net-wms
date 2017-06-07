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
	public class FGenLot : Form
	{
		private int CurrentSNCount = 1;
		private int TotalSNCount = 1;

		private ArrayList rcards = new ArrayList();

		private UserControl.UCLabelEdit ucLabEdit2;
		private System.ComponentModel.IContainer components = null;
		private UserControl.UCLabelEdit ucLabelEdit1;


		private string itemCode= string.Empty;
		private string moCode = string.Empty;
		private decimal oqcMaxSize = 0;
		private DataTable dtIDList = new DataTable();
		private System.Windows.Forms.Panel panel1;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
		private UserControl.UCMessage ucMessage;
		private System.Windows.Forms.GroupBox groupBox1;
		private UserControl.UCLabelEdit txtCarton;
		private UserControl.UCLabelEdit chkSNLastCHR;
		private UserControl.UCLabelEdit chkSNSet;
		private UserControl.UCLabelEdit ucLabEditInputID;
		private UserControl.UCButton uBtnExit;
		private Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetOQCExameOpion;
		private Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetOQCType;
		private UserControl.UCLabelEdit ucLabEditMaxNumber;
		private System.Windows.Forms.TextBox txtLength;
		private System.Windows.Forms.CheckBox cbRemixMO;
		private UserControl.UCLabelEdit ucLabEditOQCLot;
		private System.Windows.Forms.CheckBox cbCheckLength;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private UserControl.UCButton uBtnIDDelete;
		private UserControl.UCLabelEdit ucLabEditCurrentNubmer;
		private UserControl.UCButton ucButton3;
		private UserControl.UCButton ucButton2;
		private System.Windows.Forms.CheckBox chkCodex;
		private UserControl.UCButton btnEnable;
		private UserControl.UCLabelEdit txtCartonCapacity;



		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}
	

		public FGenLot()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FGenLot));
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
			this.uBtnIDDelete = new UserControl.UCButton();
			this.ucLabEditCurrentNubmer = new UserControl.UCLabelEdit();
			this.ucMessage = new UserControl.UCMessage();
			this.chkSNSet = new UserControl.UCLabelEdit();
			this.chkSNLastCHR = new UserControl.UCLabelEdit();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnEnable = new UserControl.UCButton();
			this.chkCodex = new System.Windows.Forms.CheckBox();
			this.ultraOptionSetOQCType = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
			this.ucLabEditMaxNumber = new UserControl.UCLabelEdit();
			this.ucLabEditOQCLot = new UserControl.UCLabelEdit();
			this.cbRemixMO = new System.Windows.Forms.CheckBox();
			this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.ucButton3 = new UserControl.UCButton();
			this.ucButton2 = new UserControl.UCButton();
			this.txtLength = new System.Windows.Forms.TextBox();
			this.cbCheckLength = new System.Windows.Forms.CheckBox();
			this.ultraOptionSetOQCExameOpion = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
			this.txtCarton = new UserControl.UCLabelEdit();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtCartonCapacity = new UserControl.UCLabelEdit();
			this.ucLabEditInputID = new UserControl.UCLabelEdit();
			this.uBtnExit = new UserControl.UCButton();
			this.panel1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetOQCType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetOQCExameOpion)).BeginInit();
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
			this.panel1.Size = new System.Drawing.Size(824, 493);
			this.panel1.TabIndex = 173;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.uBtnIDDelete);
			this.groupBox3.Controls.Add(this.ucLabEditCurrentNubmer);
			this.groupBox3.Controls.Add(this.ucMessage);
			this.groupBox3.Controls.Add(this.chkSNSet);
			this.groupBox3.Controls.Add(this.chkSNLastCHR);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Location = new System.Drawing.Point(0, 280);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(824, 213);
			this.groupBox3.TabIndex = 288;
			this.groupBox3.TabStop = false;
			// 
			// uBtnIDDelete
			// 
			this.uBtnIDDelete.BackColor = System.Drawing.SystemColors.Control;
			this.uBtnIDDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uBtnIDDelete.BackgroundImage")));
			this.uBtnIDDelete.ButtonType = UserControl.ButtonTypes.Delete;
			this.uBtnIDDelete.Caption = "删除";
			this.uBtnIDDelete.Cursor = System.Windows.Forms.Cursors.Hand;
			this.uBtnIDDelete.Location = new System.Drawing.Point(696, 16);
			this.uBtnIDDelete.Name = "uBtnIDDelete";
			this.uBtnIDDelete.Size = new System.Drawing.Size(88, 22);
			this.uBtnIDDelete.TabIndex = 14;
			this.uBtnIDDelete.Click += new System.EventHandler(this.uBtnIDDelete_Click);
			// 
			// ucLabEditCurrentNubmer
			// 
			this.ucLabEditCurrentNubmer.AllowEditOnlyChecked = true;
			this.ucLabEditCurrentNubmer.BackColor = System.Drawing.Color.Gainsboro;
			this.ucLabEditCurrentNubmer.Caption = "采集数量";
			this.ucLabEditCurrentNubmer.Checked = false;
			this.ucLabEditCurrentNubmer.EditType = UserControl.EditTypes.Integer;
			this.ucLabEditCurrentNubmer.Location = new System.Drawing.Point(2, 16);
			this.ucLabEditCurrentNubmer.MaxLength = 40;
			this.ucLabEditCurrentNubmer.Multiline = false;
			this.ucLabEditCurrentNubmer.Name = "ucLabEditCurrentNubmer";
			this.ucLabEditCurrentNubmer.PasswordChar = '\0';
			this.ucLabEditCurrentNubmer.ReadOnly = true;
			this.ucLabEditCurrentNubmer.ShowCheckBox = false;
			this.ucLabEditCurrentNubmer.Size = new System.Drawing.Size(162, 24);
			this.ucLabEditCurrentNubmer.TabIndex = 13;
			this.ucLabEditCurrentNubmer.TabNext = true;
			this.ucLabEditCurrentNubmer.Value = "";
			this.ucLabEditCurrentNubmer.WidthType = UserControl.WidthTypes.Small;
			this.ucLabEditCurrentNubmer.XAlign = 64;
			// 
			// ucMessage
			// 
			this.ucMessage.AutoScroll = true;
			this.ucMessage.BackColor = System.Drawing.Color.Gainsboro;
			this.ucMessage.ButtonVisible = false;
			this.ucMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ucMessage.Location = new System.Drawing.Point(3, 42);
			this.ucMessage.Name = "ucMessage";
			this.ucMessage.Size = new System.Drawing.Size(818, 168);
			this.ucMessage.TabIndex = 174;
			// 
			// chkSNSet
			// 
			this.chkSNSet.AllowEditOnlyChecked = true;
			this.chkSNSet.BackColor = System.Drawing.Color.Gainsboro;
			this.chkSNSet.Caption = "成套归属(SN数)";
			this.chkSNSet.Checked = false;
			this.chkSNSet.EditType = UserControl.EditTypes.Integer;
			this.chkSNSet.Location = new System.Drawing.Point(176, 16);
			this.chkSNSet.MaxLength = 40;
			this.chkSNSet.Multiline = false;
			this.chkSNSet.Name = "chkSNSet";
			this.chkSNSet.PasswordChar = '\0';
			this.chkSNSet.ReadOnly = false;
			this.chkSNSet.ShowCheckBox = true;
			this.chkSNSet.Size = new System.Drawing.Size(165, 24);
			this.chkSNSet.TabIndex = 11;
			this.chkSNSet.TabNext = true;
			this.chkSNSet.Value = "";
			this.chkSNSet.WidthType = UserControl.WidthTypes.Tiny;
			this.chkSNSet.XAlign = 291;
			this.chkSNSet.CheckBoxCheckedChanged += new System.EventHandler(this.chkSNSet_CheckBoxCheckedChanged);
			this.chkSNSet.InnerTextChanged += new System.EventHandler(this.chkSNSet_InnerTextChanged);
			// 
			// chkSNLastCHR
			// 
			this.chkSNLastCHR.AllowEditOnlyChecked = true;
			this.chkSNLastCHR.BackColor = System.Drawing.Color.Gainsboro;
			this.chkSNLastCHR.Caption = "成套SN末位相同位数";
			this.chkSNLastCHR.Checked = false;
			this.chkSNLastCHR.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.chkSNLastCHR.EditType = UserControl.EditTypes.Integer;
			this.chkSNLastCHR.Enabled = false;
			this.chkSNLastCHR.Location = new System.Drawing.Point(352, 16);
			this.chkSNLastCHR.MaxLength = 40;
			this.chkSNLastCHR.Multiline = false;
			this.chkSNLastCHR.Name = "chkSNLastCHR";
			this.chkSNLastCHR.PasswordChar = '\0';
			this.chkSNLastCHR.ReadOnly = false;
			this.chkSNLastCHR.ShowCheckBox = true;
			this.chkSNLastCHR.Size = new System.Drawing.Size(190, 24);
			this.chkSNLastCHR.TabIndex = 12;
			this.chkSNLastCHR.TabNext = true;
			this.chkSNLastCHR.Value = "";
			this.chkSNLastCHR.WidthType = UserControl.WidthTypes.Tiny;
			this.chkSNLastCHR.XAlign = 492;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnEnable);
			this.groupBox2.Controls.Add(this.chkCodex);
			this.groupBox2.Controls.Add(this.ultraOptionSetOQCType);
			this.groupBox2.Controls.Add(this.ucLabEditMaxNumber);
			this.groupBox2.Controls.Add(this.ucLabEditOQCLot);
			this.groupBox2.Controls.Add(this.cbRemixMO);
			this.groupBox2.Controls.Add(this.ultraGridMain);
			this.groupBox2.Controls.Add(this.ucButton3);
			this.groupBox2.Controls.Add(this.ucButton2);
			this.groupBox2.Controls.Add(this.txtLength);
			this.groupBox2.Controls.Add(this.cbCheckLength);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(824, 280);
			this.groupBox2.TabIndex = 287;
			this.groupBox2.TabStop = false;
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
			// chkCodex
			// 
			this.chkCodex.Location = new System.Drawing.Point(440, 24);
			this.chkCodex.Name = "chkCodex";
			this.chkCodex.Size = new System.Drawing.Size(168, 24);
			this.chkCodex.TabIndex = 285;
			this.chkCodex.Text = "检查Carton 与序列号算法";
			// 
			// ultraOptionSetOQCType
			// 
			this.ultraOptionSetOQCType.BackColor = System.Drawing.Color.Gainsboro;
			this.ultraOptionSetOQCType.FlatMode = true;
			this.ultraOptionSetOQCType.ItemAppearance = appearance1;
			valueListItem1.DisplayText = "正常批";
			valueListItem2.DisplayText = "再次检验批";
			this.ultraOptionSetOQCType.Items.Add(valueListItem1);
			this.ultraOptionSetOQCType.Items.Add(valueListItem2);
			this.ultraOptionSetOQCType.Location = new System.Drawing.Point(8, 64);
			this.ultraOptionSetOQCType.Name = "ultraOptionSetOQCType";
			this.ultraOptionSetOQCType.Size = new System.Drawing.Size(184, 16);
			this.ultraOptionSetOQCType.TabIndex = 0;
			// 
			// ucLabEditMaxNumber
			// 
			this.ucLabEditMaxNumber.AllowEditOnlyChecked = true;
			this.ucLabEditMaxNumber.BackColor = System.Drawing.Color.Gainsboro;
			this.ucLabEditMaxNumber.Caption = "批数量";
			this.ucLabEditMaxNumber.Checked = false;
			this.ucLabEditMaxNumber.EditType = UserControl.EditTypes.Integer;
			this.ucLabEditMaxNumber.Location = new System.Drawing.Point(308, 24);
			this.ucLabEditMaxNumber.MaxLength = 40;
			this.ucLabEditMaxNumber.Multiline = false;
			this.ucLabEditMaxNumber.Name = "ucLabEditMaxNumber";
			this.ucLabEditMaxNumber.PasswordChar = '\0';
			this.ucLabEditMaxNumber.ReadOnly = false;
			this.ucLabEditMaxNumber.ShowCheckBox = true;
			this.ucLabEditMaxNumber.Size = new System.Drawing.Size(116, 24);
			this.ucLabEditMaxNumber.TabIndex = 284;
			this.ucLabEditMaxNumber.TabNext = true;
			this.ucLabEditMaxNumber.Value = "";
			this.ucLabEditMaxNumber.WidthType = UserControl.WidthTypes.Tiny;
			this.ucLabEditMaxNumber.XAlign = 374;
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
			// cbRemixMO
			// 
			this.cbRemixMO.BackColor = System.Drawing.Color.Gainsboro;
			this.cbRemixMO.Checked = true;
			this.cbRemixMO.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbRemixMO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cbRemixMO.Location = new System.Drawing.Point(440, 48);
			this.cbRemixMO.Name = "cbRemixMO";
			this.cbRemixMO.Size = new System.Drawing.Size(128, 24);
			this.cbRemixMO.TabIndex = 279;
			this.cbRemixMO.Text = "允许不同工单采集";
			this.cbRemixMO.CheckedChanged += new System.EventHandler(this.cbRemixMO_CheckedChanged);
			// 
			// ultraGridMain
			// 
			this.ultraGridMain.Cursor = System.Windows.Forms.Cursors.Default;
			this.ultraGridMain.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ultraGridMain.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.ultraGridMain.Location = new System.Drawing.Point(3, 93);
			this.ultraGridMain.Name = "ultraGridMain";
			this.ultraGridMain.Size = new System.Drawing.Size(818, 184);
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
			this.ucButton3.Location = new System.Drawing.Point(664, 24);
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
			this.ucButton2.Location = new System.Drawing.Point(664, 64);
			this.ucButton2.Name = "ucButton2";
			this.ucButton2.Size = new System.Drawing.Size(88, 22);
			this.ucButton2.TabIndex = 16;
			this.ucButton2.Visible = false;
			// 
			// txtLength
			// 
			this.txtLength.Enabled = false;
			this.txtLength.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtLength.Location = new System.Drawing.Point(376, 48);
			this.txtLength.Name = "txtLength";
			this.txtLength.Size = new System.Drawing.Size(48, 21);
			this.txtLength.TabIndex = 282;
			this.txtLength.Text = "";
			// 
			// cbCheckLength
			// 
			this.cbCheckLength.BackColor = System.Drawing.Color.Gainsboro;
			this.cbCheckLength.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cbCheckLength.Location = new System.Drawing.Point(308, 48);
			this.cbCheckLength.Name = "cbCheckLength";
			this.cbCheckLength.Size = new System.Drawing.Size(112, 24);
			this.cbCheckLength.TabIndex = 281;
			this.cbCheckLength.Text = "批号长度";
			this.cbCheckLength.CheckedChanged += new System.EventHandler(this.cbCheckLength_CheckedChanged);
			// 
			// ultraOptionSetOQCExameOpion
			// 
			this.ultraOptionSetOQCExameOpion.BackColor = System.Drawing.Color.Gainsboro;
			this.ultraOptionSetOQCExameOpion.FlatMode = true;
			this.ultraOptionSetOQCExameOpion.ItemAppearance = appearance2;
			valueListItem3.DisplayText = "二维条码";
			valueListItem4.DisplayText = "PCS";
			this.ultraOptionSetOQCExameOpion.Items.Add(valueListItem3);
			this.ultraOptionSetOQCExameOpion.Items.Add(valueListItem4);
			this.ultraOptionSetOQCExameOpion.Location = new System.Drawing.Point(40, 48);
			this.ultraOptionSetOQCExameOpion.Name = "ultraOptionSetOQCExameOpion";
			this.ultraOptionSetOQCExameOpion.Size = new System.Drawing.Size(184, 16);
			this.ultraOptionSetOQCExameOpion.TabIndex = 0;
			this.ultraOptionSetOQCExameOpion.Visible = false;
			// 
			// txtCarton
			// 
			this.txtCarton.AllowEditOnlyChecked = true;
			this.txtCarton.BackColor = System.Drawing.Color.Gainsboro;
			this.txtCarton.Caption = "归属Carton";
			this.txtCarton.Checked = false;
			this.txtCarton.EditType = UserControl.EditTypes.String;
			this.txtCarton.Location = new System.Drawing.Point(272, 16);
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
			this.txtCarton.XAlign = 362;
			this.txtCarton.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCarton_TxtboxKeyPress);
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.Gainsboro;
			this.groupBox1.Controls.Add(this.txtCartonCapacity);
			this.groupBox1.Controls.Add(this.ucLabEditInputID);
			this.groupBox1.Controls.Add(this.uBtnExit);
			this.groupBox1.Controls.Add(this.ultraOptionSetOQCExameOpion);
			this.groupBox1.Controls.Add(this.txtCarton);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 493);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(824, 56);
			this.groupBox1.TabIndex = 171;
			this.groupBox1.TabStop = false;
			// 
			// txtCartonCapacity
			// 
			this.txtCartonCapacity.AllowEditOnlyChecked = true;
			this.txtCartonCapacity.BackColor = System.Drawing.Color.Gainsboro;
			this.txtCartonCapacity.Caption = "Carton中数量";
			this.txtCartonCapacity.Checked = false;
			this.txtCartonCapacity.EditType = UserControl.EditTypes.Integer;
			this.txtCartonCapacity.Location = new System.Drawing.Point(568, 16);
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
			this.txtCartonCapacity.XAlign = 655;
			// 
			// ucLabEditInputID
			// 
			this.ucLabEditInputID.AllowEditOnlyChecked = true;
			this.ucLabEditInputID.BackColor = System.Drawing.Color.Gainsboro;
			this.ucLabEditInputID.Caption = "输入框";
			this.ucLabEditInputID.Checked = false;
			this.ucLabEditInputID.EditType = UserControl.EditTypes.String;
			this.ucLabEditInputID.Location = new System.Drawing.Point(14, 16);
			this.ucLabEditInputID.MaxLength = 4000;
			this.ucLabEditInputID.Multiline = false;
			this.ucLabEditInputID.Name = "ucLabEditInputID";
			this.ucLabEditInputID.PasswordChar = '\0';
			this.ucLabEditInputID.ReadOnly = false;
			this.ucLabEditInputID.ShowCheckBox = false;
			this.ucLabEditInputID.Size = new System.Drawing.Size(250, 24);
			this.ucLabEditInputID.TabIndex = 10;
			this.ucLabEditInputID.TabNext = false;
			this.ucLabEditInputID.Value = "";
			this.ucLabEditInputID.WidthType = UserControl.WidthTypes.Long;
			this.ucLabEditInputID.XAlign = 64;
			this.ucLabEditInputID.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabEditInputID_TxtboxKeyPress);
			// 
			// uBtnExit
			// 
			this.uBtnExit.BackColor = System.Drawing.SystemColors.Control;
			this.uBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uBtnExit.BackgroundImage")));
			this.uBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
			this.uBtnExit.Caption = "退出";
			this.uBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
			this.uBtnExit.Location = new System.Drawing.Point(704, 16);
			this.uBtnExit.Name = "uBtnExit";
			this.uBtnExit.Size = new System.Drawing.Size(88, 22);
			this.uBtnExit.TabIndex = 9;
			// 
			// FGenLot
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(824, 549);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox1);
			this.Name = "FGenLot";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "产生送检批";
			this.Resize += new System.EventHandler(this.FGenLot_Resize);
			this.Load += new System.EventHandler(this.FGenLot_Load);
			this.Closed += new System.EventHandler(this.FGenLot_Closed);
			this.Activated += new System.EventHandler(this.FGenLot_Activated);
			this.panel1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetOQCType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetOQCExameOpion)).EndInit();
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
			#endregion

			ClearUI();


			//UserControl.UIStyleBuilder.GridUI(this.ultraGridMain);
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
					object obj = _oqcFacade.GetOQCLot( FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.ucLabEditOQCLot.Value)),OQCFacade.Lot_Sequence_Default);
					if(obj == null)
					{
						//Laws Lu,2005/10/19,新增	缓解性能问题
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
						DataProvider.BeginTransaction();
						try
						{
						
							//产生送检验批号
							_oqcFacade.AddOQCLot(CreateNewOQCLot());
							this.DataProvider.CommitTransaction();
							btnEnable_Click(sender,e);
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

						InitMessage( FormatHelper.CleanString(this.ucLabEditOQCLot.Value),ucLabEditMaxNumber.Checked,oqcMaxSize);
					}
					else
					{
						InitPostView(_oqcFacade);
						btnEnable_Click(sender,e);

						SetcbRemixMOStatus();
					}
					//Laws Lu,2005/08/10,新增 将焦点移到产品序列号输入框
					SendKeys.Send("{TAB}");

					Application.DoEvents();
					this.ucLabEditInputID.TextFocus(false, true);	// Added by Icyer 2006/06/06
				}
			}
			else
			{
				ucLabEditInputID.SelectAll();
				rcards.Clear();

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

		private void ucLabEditInputID_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
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

				//Karron Qiu,2005-10-26,在采集界面上增加一个检查批号长度的勾选框和一个输入框，
				//如果用户勾选该项，输入框有效，用户必须输入批号长度，当用户输入送检批号时，
				//如果用户已经勾选了检查批号长度项，则用户输入的送检批号长度必须与用户设置的长度值一致，
				//如果没有勾选则不检查
				if(cbCheckLength.Checked)
				{
					Messages messages  = new Messages();
					if(!IsNumberic(txtLength.Text))
					{
						messages.Add(new UserControl.Message("$CS_FQCLOTLENGTH_NOT_NULL"));//批号长度不能为空
						ApplicationRun.GetInfoForm().Add(messages);

                        txtLength.Focus();
						return ;
					}
					else if(ucLabEditOQCLot.Value.Length != Convert.ToInt32(txtLength.Text))
					{
						messages.Add(new UserControl.Message("$CS_FQCLOTLENGTH_NOT_ENOUGH"));//批号长度不够
						ApplicationRun.GetInfoForm().Add(messages);

						ucLabEditOQCLot.TextFocus(false, true);
						return ;
					}
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

		//Laws Lu,2006/05/26,Support multi-rcard collect
		private void DoAction(ArrayList rcards)
		{

			Messages messages = new Messages();
			if( InvalidData())
			{
				ActionOnLineHelper actionOnLineHelper = new ActionOnLineHelper(this.DataProvider);
				ActionFactory actionFactory = new ActionFactory(this.DataProvider);
				IAction actionOQCLotAddID = actionFactory.CreateAction(ActionType.DataCollectAction_OQCLotAddID);

				if(FormatHelper.CleanString(this.ucLabEditOQCLot.Value) == string.Empty)
				{
					messages.Add(new UserControl.Message("$CS_FQCLOT_NOT_NULL"));
					ApplicationRun.GetInfoForm().Add(messages);

					//Laws Lu,2005/08/11,新增焦点设置
					ucLabEditOQCLot.TextFocus(false, true);
					return ;
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
						foreach(string rcard in rcards)
						{
							messages.AddMessages(AddSingleIDIntoOQCLot( rcard ,FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabEditOQCLot.Value)),messages,actionOQCLotAddID,actionOnLineHelper));
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
						ucMessage.Add(new UserControl.Message(MessageType.Normal,"$Please_Input_Lot_No"));
						ucLabEditOQCLot.Value = string.Empty;
						if (ucLabEditOQCLot.Enabled == false)
						{
							this.btnEnable_Click(null, null);
						}
						Application.DoEvents();
						ucLabEditOQCLot.TextFocus(false, true);
					}
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
			this.ucLabEditInputID.Value = string.Empty;

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
				this.ultraGridMain.Selected.Rows.Clear();
				this.ultraGridMain.Rows[this.ultraGridMain.Rows.Count - 1].Selected = true;
			}
			// Added end

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
					iLength = System.Int32.Parse(this.chkSNLastCHR.Value);
				}
				catch
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CSError_PackingQuantity_Must_Be_Decimail(必须输入配套尾数位数！)"));

					TotalSNCount = 1;
					valid = false;

				}

				if(rcards.Count > 0)
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
				oqcLotAddIDEventArgs.CollectType = this.ultraOptionSetOQCExameOpion.CheckedItem.DataValue.ToString();

				newMessages.AddMessages(action.Execute(oqcLotAddIDEventArgs));	
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
//					if(ugr.Cells
//					if(this.ultraGridMain.Rows[i].Cells != null)
//					{
//						if(this.ultraGridMain.Rows[i].Cells[0] != null)
//						{
							if(ugr.Selected == true)
							{
								IDList.Add(ugr.Cells["runningcard"].Text,ugr.Cells["collecttype"].Text);
							}
//						}
//					}
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
			newOQCLot.LOTNO = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabEditOQCLot.Value));
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

					long currentCTNNO = long.Parse(txtCarton.Value.Trim());

					if(lctnNO > currentCTNNO + 10 || lctnNO < currentCTNNO)
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
				this.ucMessage.Add(">>$CS_Please_Input_RunningCard 批个数:1/"+OQCMaxSize.ToString()
					+ " 套数:" + CurrentSNCount.ToString() + "/" + TotalSNCount.ToString());
			}
			else
			{
				this.ucMessage.Add(">>$CS_Please_Input_RunningCard 套数:" + CurrentSNCount.ToString() + "/" + TotalSNCount.ToString());
			}
		}

		private void txtCarton_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				if (txtCarton.Value.Trim() != string.Empty)
				{
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
				chkSNLastCHR.Checked =false;
				chkSNLastCHR.Enabled =false;

				TotalSNCount = 1;
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
				DataRow[] drs = dtIDList.Select("mocode ='" + dr[2].ToString().Trim() + "'");
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
					this.ucMessage.Add(">>$CS_Please_Input_RunningCard 批个数:1/"+OQCMaxSize.ToString()
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

	}
}

