using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;	

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using UserControl;
using BenQGuru.eMES.OQC;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// Form2 的摘要说明。
	/// </summary>
	public class FStockOutput : System.Windows.Forms.Form
	{
		private System.Data.DataSet dsStockOut;
		private System.Data.DataTable dtStockOut;
		private System.Data.DataColumn dataColumn1;
		private System.Data.DataColumn dataColumn2;
		private System.Data.DataColumn dataColumn3;
		private System.Data.DataColumn dataColumn4;
		private System.Data.DataColumn dataColumn5;
		private System.Data.DataColumn dataColumn6;
		private System.Windows.Forms.GroupBox grpHandwork;
		private System.Windows.Forms.GroupBox grpFilePath;
		private System.Windows.Forms.ListBox lstRCardList;
		private Infragistics.Win.UltraWinTabControl.UltraTabControl tabCtrlCollect;
		private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabPageImport;
		private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabPageHandwork;
		private System.Windows.Forms.Panel pnlPager;
		private UserControl.UCButton ucBtnImport;
		private UserControl.UCButton ucBtnOpen;
		private UserControl.UCButton ucBtnRemove;
		private UserControl.UCButton ucBtnDelete;
		private UserControl.UCButton ucBtnExit;
		private UserControl.UCButton ucBtnSave;
		private UserControl.UCLabelEdit ucLEFilePath;
		private UserControl.UCLabelEdit ucLETicketNo;
		private UserControl.UCLabelEdit ucLEQty;
		private UserControl.UCLabelEdit ucLECurrQty;
		private UserControl.UCLabelEdit ucLEInput;
		private UserControl.UCLabelEdit ucLEDealer;
		private UserControl.PagerToolbar pagerToolbar;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridContent;
		private System.Windows.Forms.OpenFileDialog openFileDlg;
		private Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOsType;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
		private UserControl.UCDatetTime ucDtOutDate;
		private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabPageStockOut;
		private System.Windows.Forms.Panel panTop;
		private System.Windows.Forms.Panel panBottom;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraStockOut;
		private UserControl.UCButton btnSearch;
		private UserControl.UCButton btnStockOut;
		private UserControl.UCLabelEdit txtSumNum;
		private UserControl.UCLabelEdit txtStockNo;
		private UserControl.UCLabelCombox ucLCModel;


		//Laws Lu,2005/09/05,新增
		private DataTable dt = new DataTable();
		private UserControl.UCLabelEdit txtMEMO;
		private Infragistics.Win.UltraWinEditors.UltraOptionSet radImportType;
		public string DeleteInfor;

		public FStockOutput()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			this.pagerToolbar.OnPagerToolBarClick += new System.EventHandler(this.pagerToolbar_Click);
			//
			//			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			//			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			//			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();

			//			this.ultraOsType.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
			//
			//			ultraTab1.TabPage = this.tabPageImport;
			//			ultraTab1.Text = "文本导入";
			//			ultraTab2.TabPage = this.tabPageHandwork;
			//			ultraTab2.Text = "手工输入";
			//			ultraTab3.TabPage = this.tabPageStockOut;
			//			ultraTab3.Text = "出货";
			//			this.tabCtrlCollect.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
			//																									 ultraTab1,
			//																									 ultraTab2,
			//																									 ultraTab3});
			//
			//			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FStockOutput));
			//			this.btnStockOut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStockOut.BackgroundImage")));
			//			this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStockOut.BackgroundImage")));
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("dtStockOut", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("出货单号");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("产品别");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("经销商");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("出货日期");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("数量");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("产品序列号");
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FStockOutput));
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            this.tabPageImport = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ultraGridContent = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dtStockOut = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.pnlPager = new System.Windows.Forms.Panel();
            this.pagerToolbar = new UserControl.PagerToolbar();
            this.grpFilePath = new System.Windows.Forms.GroupBox();
            this.radImportType = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.ucBtnImport = new UserControl.UCButton();
            this.ucBtnOpen = new UserControl.UCButton();
            this.ucLEFilePath = new UserControl.UCLabelEdit();
            this.tabPageHandwork = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.grpHandwork = new System.Windows.Forms.GroupBox();
            this.txtMEMO = new UserControl.UCLabelEdit();
            this.ucLCModel = new UserControl.UCLabelCombox();
            this.ucDtOutDate = new UserControl.UCDatetTime();
            this.ucLEQty = new UserControl.UCLabelEdit();
            this.ucLECurrQty = new UserControl.UCLabelEdit();
            this.ucBtnRemove = new UserControl.UCButton();
            this.ucLEInput = new UserControl.UCLabelEdit();
            this.ucBtnDelete = new UserControl.UCButton();
            this.ucLEDealer = new UserControl.UCLabelEdit();
            this.ucLETicketNo = new UserControl.UCLabelEdit();
            this.ultraOsType = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.lstRCardList = new System.Windows.Forms.ListBox();
            this.tabPageStockOut = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ultraStockOut = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panBottom = new System.Windows.Forms.Panel();
            this.txtSumNum = new UserControl.UCLabelEdit();
            this.panTop = new System.Windows.Forms.Panel();
            this.btnStockOut = new UserControl.UCButton();
            this.btnSearch = new UserControl.UCButton();
            this.txtStockNo = new UserControl.UCLabelEdit();
            this.tabCtrlCollect = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.ucBtnExit = new UserControl.UCButton();
            this.ucBtnSave = new UserControl.UCButton();
            this.dsStockOut = new System.Data.DataSet();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.tabPageImport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStockOut)).BeginInit();
            this.pnlPager.SuspendLayout();
            this.grpFilePath.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radImportType)).BeginInit();
            this.tabPageHandwork.SuspendLayout();
            this.grpHandwork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOsType)).BeginInit();
            this.tabPageStockOut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraStockOut)).BeginInit();
            this.panBottom.SuspendLayout();
            this.panTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabCtrlCollect)).BeginInit();
            this.tabCtrlCollect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsStockOut)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageImport
            // 
            this.tabPageImport.Controls.Add(this.ultraGridContent);
            this.tabPageImport.Controls.Add(this.pnlPager);
            this.tabPageImport.Controls.Add(this.grpFilePath);
            this.tabPageImport.Location = new System.Drawing.Point(-10000, -10000);
            this.tabPageImport.Name = "tabPageImport";
            this.tabPageImport.Size = new System.Drawing.Size(858, 453);
            // 
            // ultraGridContent
            // 
            this.ultraGridContent.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridContent.DataSource = this.dtStockOut;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6});
            this.ultraGridContent.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGridContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridContent.Location = new System.Drawing.Point(0, 97);
            this.ultraGridContent.Name = "ultraGridContent";
            this.ultraGridContent.Size = new System.Drawing.Size(858, 334);
            this.ultraGridContent.TabIndex = 1;
            // 
            // dtStockOut
            // 
            this.dtStockOut.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn6,
            this.dataColumn5});
            this.dtStockOut.TableName = "dtStockOut";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "出货单号";
            this.dataColumn1.ColumnName = "出货单号";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "产品别";
            this.dataColumn2.ColumnName = "产品别";
            // 
            // dataColumn3
            // 
            this.dataColumn3.Caption = "经销商";
            this.dataColumn3.ColumnName = "经销商";
            // 
            // dataColumn4
            // 
            this.dataColumn4.Caption = "出货日期";
            this.dataColumn4.ColumnName = "出货日期";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "数量";
            // 
            // dataColumn5
            // 
            this.dataColumn5.Caption = "产品序列号";
            this.dataColumn5.ColumnName = "产品序列号";
            // 
            // pnlPager
            // 
            this.pnlPager.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlPager.Controls.Add(this.pagerToolbar);
            this.pnlPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPager.Location = new System.Drawing.Point(0, 431);
            this.pnlPager.Name = "pnlPager";
            this.pnlPager.Size = new System.Drawing.Size(858, 22);
            this.pnlPager.TabIndex = 2;
            // 
            // pagerToolbar
            // 
            this.pagerToolbar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pagerToolbar.Location = new System.Drawing.Point(478, 0);
            this.pagerToolbar.Name = "pagerToolbar";
            this.pagerToolbar.PageIndex = 1;
            this.pagerToolbar.PageSize = 20;
            this.pagerToolbar.RowCount = 0;
            this.pagerToolbar.Size = new System.Drawing.Size(380, 22);
            this.pagerToolbar.TabIndex = 0;
            // 
            // grpFilePath
            // 
            this.grpFilePath.BackColor = System.Drawing.Color.Gainsboro;
            this.grpFilePath.Controls.Add(this.radImportType);
            this.grpFilePath.Controls.Add(this.ucBtnImport);
            this.grpFilePath.Controls.Add(this.ucBtnOpen);
            this.grpFilePath.Controls.Add(this.ucLEFilePath);
            this.grpFilePath.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFilePath.Location = new System.Drawing.Point(0, 0);
            this.grpFilePath.Name = "grpFilePath";
            this.grpFilePath.Size = new System.Drawing.Size(858, 97);
            this.grpFilePath.TabIndex = 0;
            this.grpFilePath.TabStop = false;
            // 
            // radImportType
            // 
            this.radImportType.BackColor = System.Drawing.Color.Gainsboro;
            this.radImportType.BackColorInternal = System.Drawing.Color.Gainsboro;
            this.radImportType.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            valueListItem1.DisplayText = "二维条码";
            valueListItem2.DisplayText = "PCS";
            this.radImportType.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2});
            this.radImportType.Location = new System.Drawing.Point(20, 30);
            this.radImportType.Name = "radImportType";
            this.radImportType.Size = new System.Drawing.Size(207, 15);
            this.radImportType.TabIndex = 6;
            this.radImportType.Text = "二维条码";
            this.radImportType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // ucBtnImport
            // 
            this.ucBtnImport.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnImport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnImport.BackgroundImage")));
            this.ucBtnImport.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnImport.Caption = "导入";
            this.ucBtnImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnImport.Location = new System.Drawing.Point(625, 60);
            this.ucBtnImport.Name = "ucBtnImport";
            this.ucBtnImport.Size = new System.Drawing.Size(88, 22);
            this.ucBtnImport.TabIndex = 2;
            this.ucBtnImport.Click += new System.EventHandler(this.ucBtnImport_Click);
            // 
            // ucBtnOpen
            // 
            this.ucBtnOpen.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnOpen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnOpen.BackgroundImage")));
            this.ucBtnOpen.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnOpen.Caption = "选择";
            this.ucBtnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnOpen.Location = new System.Drawing.Point(510, 59);
            this.ucBtnOpen.Name = "ucBtnOpen";
            this.ucBtnOpen.Size = new System.Drawing.Size(88, 22);
            this.ucBtnOpen.TabIndex = 1;
            this.ucBtnOpen.Click += new System.EventHandler(this.ucBtnOpen_Click);
            // 
            // ucLEFilePath
            // 
            this.ucLEFilePath.AllowEditOnlyChecked = true;
            this.ucLEFilePath.Caption = "文件路径";
            this.ucLEFilePath.Checked = false;
            this.ucLEFilePath.EditType = UserControl.EditTypes.String;
            this.ucLEFilePath.Location = new System.Drawing.Point(14, 59);
            this.ucLEFilePath.MaxLength = 1500;
            this.ucLEFilePath.Multiline = false;
            this.ucLEFilePath.Name = "ucLEFilePath";
            this.ucLEFilePath.PasswordChar = '\0';
            this.ucLEFilePath.ReadOnly = false;
            this.ucLEFilePath.ShowCheckBox = false;
            this.ucLEFilePath.Size = new System.Drawing.Size(384, 23);
            this.ucLEFilePath.TabIndex = 0;
            this.ucLEFilePath.TabNext = false;
            this.ucLEFilePath.Value = "";
            this.ucLEFilePath.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLEFilePath.XAlign = 65;
            // 
            // tabPageHandwork
            // 
            this.tabPageHandwork.Controls.Add(this.grpHandwork);
            this.tabPageHandwork.Location = new System.Drawing.Point(2, 24);
            this.tabPageHandwork.Name = "tabPageHandwork";
            this.tabPageHandwork.Size = new System.Drawing.Size(858, 453);
            // 
            // grpHandwork
            // 
            this.grpHandwork.BackColor = System.Drawing.Color.Gainsboro;
            this.grpHandwork.Controls.Add(this.txtMEMO);
            this.grpHandwork.Controls.Add(this.ucLCModel);
            this.grpHandwork.Controls.Add(this.ucDtOutDate);
            this.grpHandwork.Controls.Add(this.ucLEQty);
            this.grpHandwork.Controls.Add(this.ucLECurrQty);
            this.grpHandwork.Controls.Add(this.ucBtnRemove);
            this.grpHandwork.Controls.Add(this.ucLEInput);
            this.grpHandwork.Controls.Add(this.ucBtnDelete);
            this.grpHandwork.Controls.Add(this.ucLEDealer);
            this.grpHandwork.Controls.Add(this.ucLETicketNo);
            this.grpHandwork.Controls.Add(this.ultraOsType);
            this.grpHandwork.Controls.Add(this.lstRCardList);
            this.grpHandwork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHandwork.Location = new System.Drawing.Point(0, 0);
            this.grpHandwork.Name = "grpHandwork";
            this.grpHandwork.Size = new System.Drawing.Size(858, 453);
            this.grpHandwork.TabIndex = 0;
            this.grpHandwork.TabStop = false;
            // 
            // txtMEMO
            // 
            this.txtMEMO.AllowEditOnlyChecked = true;
            this.txtMEMO.Caption = "备注";
            this.txtMEMO.Checked = false;
            this.txtMEMO.EditType = UserControl.EditTypes.String;
            this.txtMEMO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMEMO.Location = new System.Drawing.Point(33, 193);
            this.txtMEMO.MaxLength = 80;
            this.txtMEMO.Multiline = true;
            this.txtMEMO.Name = "txtMEMO";
            this.txtMEMO.PasswordChar = '\0';
            this.txtMEMO.ReadOnly = false;
            this.txtMEMO.ShowCheckBox = false;
            this.txtMEMO.Size = new System.Drawing.Size(198, 57);
            this.txtMEMO.TabIndex = 293;
            this.txtMEMO.TabNext = true;
            this.txtMEMO.Value = "";
            this.txtMEMO.WidthType = UserControl.WidthTypes.Long;
            this.txtMEMO.XAlign = 64;
            // 
            // ucLCModel
            // 
            this.ucLCModel.AllowEditOnlyChecked = true;
            this.ucLCModel.Caption = "产品别";
            this.ucLCModel.Checked = false;
            this.ucLCModel.Location = new System.Drawing.Point(22, 55);
            this.ucLCModel.Name = "ucLCModel";
            this.ucLCModel.SelectedIndex = -1;
            this.ucLCModel.ShowCheckBox = false;
            this.ucLCModel.Size = new System.Drawing.Size(210, 19);
            this.ucLCModel.TabIndex = 1;
            this.ucLCModel.WidthType = UserControl.WidthTypes.Long;
            this.ucLCModel.XAlign = 64;
            this.ucLCModel.Load += new System.EventHandler(this.ucLCModel_Load);
            // 
            // ucDtOutDate
            // 
            this.ucDtOutDate.Caption = "出货日期";
            this.ucDtOutDate.Location = new System.Drawing.Point(336, 55);
            this.ucDtOutDate.Name = "ucDtOutDate";
            this.ucDtOutDate.ShowType = UserControl.DateTimeTypes.Date;
            this.ucDtOutDate.Size = new System.Drawing.Size(153, 19);
            this.ucDtOutDate.TabIndex = 2;
            this.ucDtOutDate.Value = new System.DateTime(2005, 8, 10, 17, 17, 16, 0);
            this.ucDtOutDate.XAlign = 388;
            // 
            // ucLEQty
            // 
            this.ucLEQty.AllowEditOnlyChecked = true;
            this.ucLEQty.Caption = "数量";
            this.ucLEQty.Checked = false;
            this.ucLEQty.EditType = UserControl.EditTypes.String;
            this.ucLEQty.Location = new System.Drawing.Point(336, 82);
            this.ucLEQty.MaxLength = 40;
            this.ucLEQty.Multiline = false;
            this.ucLEQty.Name = "ucLEQty";
            this.ucLEQty.PasswordChar = '\0';
            this.ucLEQty.ReadOnly = false;
            this.ucLEQty.ShowCheckBox = false;
            this.ucLEQty.Size = new System.Drawing.Size(197, 22);
            this.ucLEQty.TabIndex = 4;
            this.ucLEQty.TabNext = true;
            this.ucLEQty.Value = "";
            this.ucLEQty.WidthType = UserControl.WidthTypes.Long;
            this.ucLEQty.XAlign = 367;
            // 
            // ucLECurrQty
            // 
            this.ucLECurrQty.AllowEditOnlyChecked = true;
            this.ucLECurrQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLECurrQty.Caption = "当前数量";
            this.ucLECurrQty.Checked = false;
            this.ucLECurrQty.EditType = UserControl.EditTypes.String;
            this.ucLECurrQty.Location = new System.Drawing.Point(717, 424);
            this.ucLECurrQty.MaxLength = 40;
            this.ucLECurrQty.Multiline = false;
            this.ucLECurrQty.Name = "ucLECurrQty";
            this.ucLECurrQty.PasswordChar = '\0';
            this.ucLECurrQty.ReadOnly = true;
            this.ucLECurrQty.ShowCheckBox = false;
            this.ucLECurrQty.Size = new System.Drawing.Size(134, 23);
            this.ucLECurrQty.TabIndex = 292;
            this.ucLECurrQty.TabNext = true;
            this.ucLECurrQty.TabStop = false;
            this.ucLECurrQty.Value = "";
            this.ucLECurrQty.WidthType = UserControl.WidthTypes.Small;
            this.ucLECurrQty.XAlign = 768;
            // 
            // ucBtnRemove
            // 
            this.ucBtnRemove.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnRemove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnRemove.BackgroundImage")));
            this.ucBtnRemove.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnRemove.Caption = "移除";
            this.ucBtnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnRemove.Location = new System.Drawing.Point(474, 149);
            this.ucBtnRemove.Name = "ucBtnRemove";
            this.ucBtnRemove.Size = new System.Drawing.Size(88, 22);
            this.ucBtnRemove.TabIndex = 291;
            this.ucBtnRemove.TabStop = false;
            this.ucBtnRemove.Click += new System.EventHandler(this.ucBtnRemove_Click);
            // 
            // ucLEInput
            // 
            this.ucLEInput.AllowEditOnlyChecked = true;
            this.ucLEInput.Caption = "输入框";
            this.ucLEInput.Checked = false;
            this.ucLEInput.EditType = UserControl.EditTypes.String;
            this.ucLEInput.Location = new System.Drawing.Point(23, 149);
            this.ucLEInput.MaxLength = 1000;
            this.ucLEInput.Multiline = false;
            this.ucLEInput.Name = "ucLEInput";
            this.ucLEInput.PasswordChar = '\0';
            this.ucLEInput.ReadOnly = false;
            this.ucLEInput.ShowCheckBox = false;
            this.ucLEInput.Size = new System.Drawing.Size(374, 22);
            this.ucLEInput.TabIndex = 6;
            this.ucLEInput.TabNext = false;
            this.ucLEInput.Value = "";
            this.ucLEInput.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLEInput.XAlign = 64;
            this.ucLEInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEInput_TxtboxKeyPress);
            // 
            // ucBtnDelete
            // 
            this.ucBtnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnDelete.BackgroundImage")));
            this.ucBtnDelete.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnDelete.Caption = "删除出货单";
            this.ucBtnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnDelete.Location = new System.Drawing.Point(336, 22);
            this.ucBtnDelete.Name = "ucBtnDelete";
            this.ucBtnDelete.Size = new System.Drawing.Size(88, 22);
            this.ucBtnDelete.TabIndex = 288;
            this.ucBtnDelete.TabStop = false;
            this.ucBtnDelete.Click += new System.EventHandler(this.ucBtnDelete_Click);
            // 
            // ucLEDealer
            // 
            this.ucLEDealer.AllowEditOnlyChecked = true;
            this.ucLEDealer.Caption = "经销商";
            this.ucLEDealer.Checked = false;
            this.ucLEDealer.EditType = UserControl.EditTypes.String;
            this.ucLEDealer.Location = new System.Drawing.Point(24, 82);
            this.ucLEDealer.MaxLength = 40;
            this.ucLEDealer.Multiline = false;
            this.ucLEDealer.Name = "ucLEDealer";
            this.ucLEDealer.PasswordChar = '\0';
            this.ucLEDealer.ReadOnly = false;
            this.ucLEDealer.ShowCheckBox = false;
            this.ucLEDealer.Size = new System.Drawing.Size(208, 22);
            this.ucLEDealer.TabIndex = 3;
            this.ucLEDealer.TabNext = true;
            this.ucLEDealer.Value = "";
            this.ucLEDealer.WidthType = UserControl.WidthTypes.Long;
            this.ucLEDealer.XAlign = 65;
            // 
            // ucLETicketNo
            // 
            this.ucLETicketNo.AllowEditOnlyChecked = true;
            this.ucLETicketNo.Caption = "出货单";
            this.ucLETicketNo.Checked = false;
            this.ucLETicketNo.EditType = UserControl.EditTypes.String;
            this.ucLETicketNo.Location = new System.Drawing.Point(24, 22);
            this.ucLETicketNo.MaxLength = 40;
            this.ucLETicketNo.Multiline = false;
            this.ucLETicketNo.Name = "ucLETicketNo";
            this.ucLETicketNo.PasswordChar = '\0';
            this.ucLETicketNo.ReadOnly = false;
            this.ucLETicketNo.ShowCheckBox = false;
            this.ucLETicketNo.Size = new System.Drawing.Size(208, 23);
            this.ucLETicketNo.TabIndex = 0;
            this.ucLETicketNo.TabNext = true;
            this.ucLETicketNo.Value = "";
            this.ucLETicketNo.WidthType = UserControl.WidthTypes.Long;
            this.ucLETicketNo.XAlign = 65;
            // 
            // ultraOsType
            // 
            this.ultraOsType.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraOsType.BackColorInternal = System.Drawing.Color.Gainsboro;
            this.ultraOsType.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            valueListItem3.DisplayText = "二维条码";
            valueListItem4.DisplayText = "PCS";
            this.ultraOsType.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem3,
            valueListItem4});
            this.ultraOsType.Location = new System.Drawing.Point(27, 115);
            this.ultraOsType.Name = "ultraOsType";
            this.ultraOsType.Size = new System.Drawing.Size(206, 15);
            this.ultraOsType.TabIndex = 5;
            this.ultraOsType.Text = "二维条码";
            this.ultraOsType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // lstRCardList
            // 
            this.lstRCardList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstRCardList.Location = new System.Drawing.Point(7, 267);
            this.lstRCardList.Name = "lstRCardList";
            this.lstRCardList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstRCardList.Size = new System.Drawing.Size(844, 134);
            this.lstRCardList.TabIndex = 6;
            // 
            // tabPageStockOut
            // 
            this.tabPageStockOut.Controls.Add(this.ultraStockOut);
            this.tabPageStockOut.Controls.Add(this.panBottom);
            this.tabPageStockOut.Controls.Add(this.panTop);
            this.tabPageStockOut.Location = new System.Drawing.Point(-10000, -10000);
            this.tabPageStockOut.Name = "tabPageStockOut";
            this.tabPageStockOut.Size = new System.Drawing.Size(858, 453);
            // 
            // ultraStockOut
            // 
            this.ultraStockOut.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraStockOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraStockOut.Location = new System.Drawing.Point(0, 52);
            this.ultraStockOut.Name = "ultraStockOut";
            this.ultraStockOut.Size = new System.Drawing.Size(858, 364);
            this.ultraStockOut.TabIndex = 2;
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.txtSumNum);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panBottom.Location = new System.Drawing.Point(0, 416);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(858, 37);
            this.panBottom.TabIndex = 1;
            // 
            // txtSumNum
            // 
            this.txtSumNum.AllowEditOnlyChecked = true;
            this.txtSumNum.Caption = "数量总计";
            this.txtSumNum.Checked = false;
            this.txtSumNum.EditType = UserControl.EditTypes.String;
            this.txtSumNum.Location = new System.Drawing.Point(441, 7);
            this.txtSumNum.MaxLength = 40;
            this.txtSumNum.Multiline = false;
            this.txtSumNum.Name = "txtSumNum";
            this.txtSumNum.PasswordChar = '\0';
            this.txtSumNum.ReadOnly = true;
            this.txtSumNum.ShowCheckBox = false;
            this.txtSumNum.Size = new System.Drawing.Size(134, 23);
            this.txtSumNum.TabIndex = 2;
            this.txtSumNum.TabNext = false;
            this.txtSumNum.Value = "0";
            this.txtSumNum.WidthType = UserControl.WidthTypes.Small;
            this.txtSumNum.XAlign = 492;
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.btnStockOut);
            this.panTop.Controls.Add(this.btnSearch);
            this.panTop.Controls.Add(this.txtStockNo);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(858, 52);
            this.panTop.TabIndex = 0;
            // 
            // btnStockOut
            // 
            this.btnStockOut.BackColor = System.Drawing.SystemColors.Control;
            this.btnStockOut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStockOut.BackgroundImage")));
            this.btnStockOut.ButtonType = UserControl.ButtonTypes.None;
            this.btnStockOut.Caption = "出货";
            this.btnStockOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStockOut.Location = new System.Drawing.Point(441, 22);
            this.btnStockOut.Name = "btnStockOut";
            this.btnStockOut.Size = new System.Drawing.Size(88, 22);
            this.btnStockOut.TabIndex = 290;
            this.btnStockOut.TabStop = false;
            this.btnStockOut.Click += new System.EventHandler(this.btnStockOut_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.ButtonType = UserControl.ButtonTypes.None;
            this.btnSearch.Caption = "查询";
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Location = new System.Drawing.Point(327, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(88, 22);
            this.btnSearch.TabIndex = 289;
            this.btnSearch.TabStop = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtStockNo
            // 
            this.txtStockNo.AllowEditOnlyChecked = true;
            this.txtStockNo.Caption = "出货单";
            this.txtStockNo.Checked = false;
            this.txtStockNo.EditType = UserControl.EditTypes.String;
            this.txtStockNo.Location = new System.Drawing.Point(24, 22);
            this.txtStockNo.MaxLength = 40;
            this.txtStockNo.Multiline = false;
            this.txtStockNo.Name = "txtStockNo";
            this.txtStockNo.PasswordChar = '\0';
            this.txtStockNo.ReadOnly = false;
            this.txtStockNo.ShowCheckBox = false;
            this.txtStockNo.Size = new System.Drawing.Size(208, 23);
            this.txtStockNo.TabIndex = 1;
            this.txtStockNo.TabNext = true;
            this.txtStockNo.Value = "";
            this.txtStockNo.WidthType = UserControl.WidthTypes.Long;
            this.txtStockNo.XAlign = 65;
            // 
            // tabCtrlCollect
            // 
            this.tabCtrlCollect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCtrlCollect.BackColorInternal = System.Drawing.Color.Gainsboro;
            this.tabCtrlCollect.Controls.Add(this.tabPageImport);
            this.tabCtrlCollect.Controls.Add(this.ultraTabSharedControlsPage1);
            this.tabCtrlCollect.Controls.Add(this.tabPageHandwork);
            this.tabCtrlCollect.Controls.Add(this.tabPageStockOut);
            this.tabCtrlCollect.Location = new System.Drawing.Point(0, 0);
            this.tabCtrlCollect.Name = "tabCtrlCollect";
            this.tabCtrlCollect.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.tabCtrlCollect.Size = new System.Drawing.Size(862, 479);
            this.tabCtrlCollect.TabIndex = 0;
            ultraTab1.TabPage = this.tabPageImport;
            ultraTab1.Text = "文本导入";
            ultraTab2.TabPage = this.tabPageHandwork;
            ultraTab2.Text = "手工输入";
            ultraTab3.TabPage = this.tabPageStockOut;
            ultraTab3.Text = "出货";
            this.tabCtrlCollect.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1,
            ultraTab2,
            ultraTab3});
            this.tabCtrlCollect.TabStop = false;
            this.tabCtrlCollect.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(this.tabCtrlCollect_SelectedTabChanged);
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(858, 453);
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(742, 487);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 1;
            // 
            // ucBtnSave
            // 
            this.ucBtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucBtnSave.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnSave.BackgroundImage")));
            this.ucBtnSave.ButtonType = UserControl.ButtonTypes.Save;
            this.ucBtnSave.Caption = "保存";
            this.ucBtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnSave.Location = new System.Drawing.Point(649, 487);
            this.ucBtnSave.Name = "ucBtnSave";
            this.ucBtnSave.Size = new System.Drawing.Size(88, 22);
            this.ucBtnSave.TabIndex = 0;
            this.ucBtnSave.Click += new System.EventHandler(this.ucBtnSave_Click);
            // 
            // dsStockOut
            // 
            this.dsStockOut.DataSetName = "dsStockOut";
            this.dsStockOut.Locale = new System.Globalization.CultureInfo("zh-CN");
            this.dsStockOut.Tables.AddRange(new System.Data.DataTable[] {
            this.dtStockOut});
            // 
            // openFileDlg
            // 
            this.openFileDlg.Filter = "*.txt|*.*";
            // 
            // FStockOutput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(862, 514);
            this.Controls.Add(this.tabCtrlCollect);
            this.Controls.Add(this.ucBtnSave);
            this.Controls.Add(this.ucBtnExit);
            this.Name = "FStockOutput";
            this.Text = "出货采集";
            this.Load += new System.EventHandler(this.FStockOutput_Load);
            this.Closed += new System.EventHandler(this.FStockOutput_Closed);
            this.tabPageImport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStockOut)).EndInit();
            this.pnlPager.ResumeLayout(false);
            this.grpFilePath.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radImportType)).EndInit();
            this.tabPageHandwork.ResumeLayout(false);
            this.grpHandwork.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraOsType)).EndInit();
            this.tabPageStockOut.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraStockOut)).EndInit();
            this.panBottom.ResumeLayout(false);
            this.panTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabCtrlCollect)).EndInit();
            this.tabCtrlCollect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dsStockOut)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		
		private MaterialStockFacade _facade = null;
		private BarCodeParse _barCodeFacade = null;
		private StockFileParser _parser = null;
		private ArrayList _stockContentList = new ArrayList();

		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}
		protected void ShowMessage(string message)
		{
			ApplicationRun.GetInfoForm().Add( message );
		}

		protected void ShowMessage(UserControl.Message message)
		{
			ApplicationRun.GetInfoForm().Add( message );
		}

		private enum CollectionType
		{
			Planate, PCS
		}

		private void FStockOutput_Load(object sender, System.EventArgs e)
		{
			UserControl.UIStyleBuilder.FormUI(this);	
			UserControl.UIStyleBuilder.GridUI(this.ultraGridContent);
			UserControl.UIStyleBuilder.GridUI(this.ultraStockOut);

			this._facade = new MaterialStockFacade( this.DataProvider );
			this._barCodeFacade = new BarCodeParse( this.DataProvider );
			//Laws Lu,2005/09/05	新增,出库信息列表列绑定
			InitialDataTable();
			FillStockOut();

			this.ucDtOutDate.Value = DateTime.Now;

			this.ucLECurrQty.Value = "0";

			//默认选择为二维条码
			ultraOsType.Items.Clear();
			ultraOsType.CheckedItem =  ultraOsType.Items.Add(OQCFacade.OQC_ExameObject_PlanarCode,"二维条码");
			//this.ultraOptionSetOQCExameOpion.Items.Add(OQCFacade.OQC_ExameObject_Carton,"Carton");
			ultraOsType.Items.Add(OQCFacade.OQC_ExameObject_PCS,"PCS");

			radImportType.Items.Clear();
			radImportType.CheckedItem =  radImportType.Items.Add(OQCFacade.OQC_ExameObject_PlanarCode,"二维条码");
			radImportType.Items.Add(OQCFacade.OQC_ExameObject_PCS,"PCS");

			this.ucLETicketNo.TextFocus(false, true);
		}

		private void FStockOutput_Closed(object sender, System.EventArgs e)
		{
			if (this.DataProvider!=null)
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();  
			}
		}

		private void ucBtnOpen_Click(object sender, System.EventArgs e)
		{
			try
			{
				if ( this.openFileDlg.ShowDialog(this) == DialogResult.OK )
				{
					this.ucLEFilePath.Value = this.openFileDlg.FileName;

					
				}
			}
			catch( Exception ex )
			{
				this.ShowMessage( ex.Message );
				this.ucLEFilePath.TextFocus(false, true);
			}
		}

		private void ucBtnImport_Click(object sender, System.EventArgs e)
		{
			try
			{
				//Laws Lu,2005/09/06,显示数据
				// 导入并显示数据
				this.requestData();
				
			}
			catch( Exception ex )
			{
				this.ShowMessage( ex.Message );
				this.ucLEFilePath.TextFocus(false, true);
			}
		}

		private void pagerToolbar_Click(object sender, System.EventArgs e)
		{
			// 分页显示
			this.gridBind( this.pagerToolbar.Inclusive, this.pagerToolbar.Exclusive );
		}

		private void ucBtnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				#region 文本导入
				if ( this.tabCtrlCollect.SelectedTab.Index == 0 )
				{
					if ( this._stockContentList.Count == 0 )
					{
						throw new Exception("$CS_RCard_List_Is_Empty");
					}

					this.saveImport();

					this.dtStockOut.Rows.Clear();

					this.pagerToolbar.PageIndex = 1;
					this.pagerToolbar.RowCount = 0;

					this.ucLEFilePath.Value = string.Empty;
					this.ucLEFilePath.TextFocus(false, true);
				}
				#endregion

				#region 手工输入
				if ( this.tabCtrlCollect.SelectedTab.Index == 1 )
				{					
					if(DialogResult.Cancel == MessageBox.Show("资料保存后不允许删除，是否确认保存？",this.Text,MessageBoxButtons.OKCancel))
					{
						return;
					}
					if ( !validateInput() )
					{
						return;
					}

					//karron qiu,2005/09/23，新增	与顾问要求不符，出货单出货后只能删除，不能再新增任何产品信息。
					if ( this._facade.IsStockOutTicketDeleteOrAlready(this.ucLETicketNo.Value.Trim().ToUpper()) )
					{
						this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_StockOut_Ticket_Deleted_OR_Already"));	// 入库单号已存在
						this.ucLETicketNo.TextFocus(false, true);
						return;
					}

					if ( this.lstRCardList.Items.Count == 0 )
					{
						throw new Exception("$CS_RCard_List_Is_Empty");
					}

					this.DataProvider.BeginTransaction();
					ArrayList ar = new ArrayList();
					try
					{
						// 获得界面输入的出库单的信息,并存库
						MaterialStockOut stockOut = this.getMaterialStockOut();
//						//Laws Lu,2005/09/20，新增	与顾问要求不符，出货单出货后只能删除，不能再新增任何产品信息。
						//注释,在前面控制
//						if(stockOut.Status == StockStatus.Already
//							||stockOut.Status == StockStatus.Deleted)
//						{
//							throw new Exception("$Error_CS_StockOut_Ticket_Deleted_OR_Already");
//						}
						
						// 获得RCard信息
						foreach ( MaterialStockOutDetail detail in this.lstRCardList.Items )
						{
							detail.TicketNO = stockOut.TicketNO;
							detail.Sequence = stockOut.Sequence;
							detail.MaintainUser = ApplicationService.Current().UserCode;
							detail.CollectNo = string.Empty;

							// 将RCard存入数据库
							if(!this._facade.RunningCardIsExist(detail.RunningCard,detail.TicketNO,detail.Sequence.ToString()))
							{
								this._facade.AddMaterialStockOutDetail(detail);
							}
							else
							{
								ar.Add(detail.RunningCard);
							}
						}

						//更新主档数量 Added By Karron Qiu at 2005-9-19
						stockOut.Quantity = this._facade.GetMaterialStockOutDetailCount(stockOut);

						this._facade.UpdateMaterialStockOut(stockOut);
						//=========================================

						this.DataProvider.CommitTransaction();
					}
					catch( Exception ex )
					{
						this.DataProvider.RollbackTransaction();
						throw ex;
					}
					foreach(object obj in ar)
					{
						this.ShowMessage("$CS_IDRepeatCollect $CS_Param_ID " + obj.ToString());
					}
					this.ShowMessage(new UserControl.Message(MessageType.Success,"$CS_Save_Success"));		// 保存成功

					this.lstRCardList.Items.Clear();
					this.ucLECurrQty.Value = "0";

					//this.ucLCModel.SelectedIndex = 0;
					//this.ucLEDealer.Value = string.Empty;
					this.ucLEQty.Value = string.Empty;
					//this.ucDtOutDate.Value = DateTime.Now;
					//Laws Lu,2005/09/06,	备注
					txtMEMO.Value = String.Empty;
					//设置默认为二维条码
					ultraOsType.Items.Clear();
					ultraOsType.CheckedItem =  ultraOsType.Items.Add(OQCFacade.OQC_ExameObject_PlanarCode,"二维条码");
					//this.ultraOptionSetOQCExameOpion.Items.Add(OQCFacade.OQC_ExameObject_Carton,"Carton");
					ultraOsType.Items.Add(OQCFacade.OQC_ExameObject_PCS,"PCS");


                    this.ucLCModel.Focus();
				}
				#endregion
			}
			catch( Exception ex )
			{
				this.ShowMessage(new UserControl.Message(MessageType.Error,ex.Message));
				return;
			}
		}

		#region 解析/显示文本文件
		private void requestData()
		{
			try
			{
				// 读入文本文件
				this.generateContent();
				
				// 统计数量,检查RCard是否重复
				this.checkRCard();
			}
			catch(Exception e)
			{
				// 出错清空
				this._stockContentList.Clear();
				throw e;
			}

			this.pagerToolbar.PageIndex = 1;
			this.pagerToolbar.RowCount = this._stockContentList.Count;
			this.gridBind( this.pagerToolbar.Inclusive, this.pagerToolbar.Exclusive);
		}

		private object[] importStockContent()
		{
			// 文本导入
			if ( this.ucLEFilePath.Value.Trim() == string.Empty )
			{
				throw new Exception("$Error_CS_Input_Import_File_Path");	// 请输入导入文件的路径
			}

			// 文件解析
			this._parser = new StockFileParser();
			this._parser.FormatName = "MaterialStockOutContent" ;
			this._parser.ConfigFile = Application.StartupPath + "\\DataFileParser.xml" ;

			object[] objs =  this._parser.Parse(this.ucLEFilePath.Value) ;
			//Laws Lu,2005/09/13,新增	关闭文件读取
			this._parser.CloseFile();

			return objs;
		}

		private void generateContent()
		{
			this._stockContentList.Clear();;

			// 获得文本解析的数组
			//Laws Lu,2005/09/14,修改	解析文件如果出现异常,则释放文件句柄
			object[] objs = null;
			try
			{
				objs = this.importStockContent();
			}
			catch(Exception E)
			{
				this._parser.CloseFile();

				this.ShowMessage(E.Message);
			}

			if(objs == null)
			{
				return;
			}

			foreach ( MaterialStockOutContent content in objs )
			{
				this.checkStockOutContent( content );
			}

			// 按出货单,产品别,经销商,发货日期 的顺序排序
			Array.Sort( objs, new StockOutContentComparer() );

			foreach ( MaterialStockOutContent content in objs )
			{
				string[] idList = null;
				if ( this.radImportType.CheckedIndex == (int)CollectionType.PCS )
				{
					idList = new string[]{content.PlanateCode.Trim()};
				}
				if( this.radImportType.CheckedIndex == (int)CollectionType.Planate )
				{
					// 解析二维条码
					idList = this._barCodeFacade.GetIDList( content.PlanateCode.Trim() );
				}
					
				if ( idList == null )
				{
					continue;
				}

				foreach ( string id in idList )
				{
					// 为每个RCard号建立一笔显示数据
					MaterialStockOutContent RCard = new MaterialStockOutContent();
					RCard.TicketNO	 = content.TicketNO.Trim();
					RCard.ModelCode	 = content.ModelCode.Trim();
					RCard.OutDate	 = content.OutDate;
					RCard.Dealer		 = content.Dealer.Trim();
					RCard.PlanateCode = id.Trim().ToUpper();

					this._stockContentList.Add( RCard );
				}
			}
		}

		private void checkStockOutContent( MaterialStockOutContent content )
		{
			if ( content.TicketNO == string.Empty || content.ModelCode == string.Empty ||
				content.Dealer == string.Empty || content.OutDate == 0 || content.PlanateCode == string.Empty )
			{
				throw new Exception("$Error_CS_File_Format_Error");
			}
		}

		private void checkRCard()
		{
			StockOutContentComparer comparer = new StockOutContentComparer();

			ArrayList array = new ArrayList();

			foreach ( MaterialStockOutContent content in this._stockContentList )
			{
				if ( array.Count == 0 )
				{
					array.Add( content );
					continue;
				}

				if ( comparer.Compare( array[0], content ) == 0 )
				{
					// 判断RCard号是否重复
					foreach ( MaterialStockOutContent equalContent in array )
					{
						if ( content.PlanateCode == equalContent.PlanateCode )
						{
							throw new Exception(string.Format("$Error_CS_RCard_Exist $StockOutTicket={0} $RCard={1}", content.TicketNO, content.PlanateCode));
						}
					}

					array.Add( content );
				}
				else
				{
					// 统计每一项次的出货数量
					foreach ( MaterialStockOutContent equalContent in array )
					{
						equalContent.Qty = array.Count;
					}

					array = new ArrayList();

					array.Add(content);
				}
			}

			foreach ( MaterialStockOutContent equalContent in array )
			{
				equalContent.Qty = array.Count;
			}
		}

		private void gridBind( int inclusive, int exclusive )
		{
			this.dtStockOut.Rows.Clear();

			MaterialStockOutContent lastContent = new MaterialStockOutContent();
			MaterialStockOutContent content = null;
			StockOutContentComparer comparer = new StockOutContentComparer();

			#region 显示数据
			for ( int i = inclusive; i <= Math.Min(exclusive,this._stockContentList.Count); i++ )
			{
				content = (MaterialStockOutContent)this._stockContentList[i-1];

				// 出货单与上一笔不同, 显示所有数据
				if ( lastContent.TicketNO != content.TicketNO )		
				{
					this.dtStockOut.Rows.Add( new object[] { content.TicketNO,
															   content.ModelCode,
															   content.Dealer,
															   FormatHelper.ToDateString(content.OutDate),
															   content.Qty,
															   content.PlanateCode} );
				}
					// 产品别,经销商,发货日期 不同, 不显示出货单
				else if ( comparer.Compare(lastContent, content) != 0 )		
				{
					this.dtStockOut.Rows.Add( new object[] { "",
															   content.ModelCode,
															   content.Dealer,
															   FormatHelper.ToDateString(content.OutDate),
															   content.Qty,
															   content.PlanateCode} );
				}
					// 只有RCard号不同,只显示RCard号
				else
				{
					this.dtStockOut.Rows.Add( new object[] { "",
															   "",
															   "",
															   "",
															   "",
															   content.PlanateCode} );
				}

				lastContent = content;
			}
			#endregion
		}
		#endregion

		#region 文本导入
		public void saveImport()
		{
			MaterialStockOutContent lastContent = new MaterialStockOutContent();
			StockOutContentComparer comparer = new StockOutContentComparer();

			MaterialStockOut stockOut = new MaterialStockOut();
			stockOut.MaintainUser = ApplicationService.Current().UserCode;

			MaterialStockOutDetail detail = new MaterialStockOutDetail();
			detail.MaintainUser = ApplicationService.Current().UserCode;
			if ( this.radImportType.CheckedIndex == (int)CollectionType.PCS )
			{
				detail.CollectType = StockCollectionType.PCS;
			}
			if ( this.radImportType.CheckedIndex == (int)CollectionType.Planate )
			{
				detail.CollectType = StockCollectionType.Planate;
			}
			

			//this._facade.IsStockOutTicketExist(

			this.DataProvider.BeginTransaction();

			try
			{
				foreach ( MaterialStockOutContent content in this._stockContentList )
				{
					// 存出货单信息
					if ( comparer.Compare(lastContent, content) != 0 )
					{
						stockOut.TicketNO	= content.TicketNO.ToUpper();
						stockOut.ModelCode	= content.ModelCode.ToUpper();
						stockOut.Dealer		= content.Dealer.ToUpper();
						stockOut.OutDate	= content.OutDate;
						stockOut.Quantity	= content.Qty;

						this._facade.AddMaterialStockOut(stockOut);
							
						detail.TicketNO = stockOut.TicketNO;
						detail.Sequence = stockOut.Sequence;

						lastContent = content;
					}
						
					detail.RunningCard = content.PlanateCode.ToUpper();

					// 将RCard存入数据库
					this._facade.AddMaterialStockOutDetail(detail);
				}

				this.DataProvider.CommitTransaction();
			}
			catch( Exception ex )
			{
				this.DataProvider.RollbackTransaction();
				throw ex;
			}

			this._stockContentList.Clear();
			//this.ShowMessage("$CS_Save_Success");	
		}
		#endregion

		#region 手工采集
		private MaterialStockOut getMaterialStockOut()
		{
			MaterialStockOut stockOut = this._facade.CreateNewMaterialStockOut();

			stockOut.TicketNO	= this.ucLETicketNo.Value.Trim().ToUpper();
			stockOut.ModelCode	= this.ucLCModel.SelectedItemValue.ToString().ToUpper();
			stockOut.Dealer		= this.ucLEDealer.Value.Trim().ToUpper();
			stockOut.OutDate	= FormatHelper.TODateInt( this.ucDtOutDate.Value );
			stockOut.Quantity	= this.lstRCardList.Items.Count;
			stockOut.MaintainUser = ApplicationService.Current().UserCode;
			stockOut.StockMemo = txtMEMO.Value.Trim();

			// 将出货单的产品别，经销商（发货地址），发货日期存入数据库,得出流水号
			return this._facade.AddMaterialStockOut(stockOut);

			//return stockOut;
		}

		private MaterialStockOutDetail findItemFromList( string runningCard )
		{
			foreach ( MaterialStockOutDetail item in this.lstRCardList.Items )
			{
				if ( item.RunningCard == runningCard )
				{
					return item;
				}
			}

			return null;
		}

		private void checkRCardExist( string runningCard )
		{
			if ( this.findItemFromList(runningCard) != null )
			{
				throw new Exception("$Error_CS_ID_Exist_In_StockOut_List $RCard=" + runningCard);	// 序列号已存在
			}
		}
		#endregion 

		private void ucBtnDelete_Click(object sender, System.EventArgs e)
		{
			#region 手工采集 删除出货单
			if ( this.ucLETicketNo.Value.Trim() == string.Empty )
			{
				throw new Exception("$Error_CS_Input_StockOut_TicketNo");	// 请输入出库单号
			}

			if ( !this._facade.IsStockOutTicketExist( this.ucLETicketNo.Value.Trim()) )
			{
				throw new Exception("$Error_CS_StockOut_Ticket_Not_Exist");
			}

			FStockDelInfo confirm = new FStockDelInfo(this);
			// 确认删除入库单
			if ( confirm.ShowDialog(this) == DialogResult.OK )
			{
				if(this.DeleteInfor != null && this.DeleteInfor != String.Empty)
				{
					

					this.DataProvider.BeginTransaction();

					try
					{
						//Laws Lu,2005/09/05,	修改出库单状态为已删除
						object[] objStockOuts = this._facade.QueryMaterialStockOut(this.ucLETicketNo.Value.Trim().ToUpper());

						bool result = false;

						if(objStockOuts != null && objStockOuts.Length > 0)
						{
							for(int i = 0;i< objStockOuts.Length ;i++ )
							{
								
								MaterialStockOut mso = (MaterialStockOut)objStockOuts[i];

								if(mso.Status != StockStatus.Deleted)
								{
									mso.DelUser = ApplicationService.Current().UserCode;
									mso.DelDate = BenQGuru.eMES.Web.Helper.FormatHelper.TODateInt(DateTime.Now);
									mso.DelTime = BenQGuru.eMES.Web.Helper.FormatHelper.TOTimeInt(DateTime.Now);
									mso.Status = BenQGuru.eMES.Material.StockStatus.Deleted;
									mso.DelMemo = this.DeleteInfor;

									this._facade.UpdateMaterialStockOut(mso);

									result = true;
								}
							}
						}
						
						//Laws Lu,2005/09/05,	修改出库单状态为已删除
						//this._facade.DeleteMaterialStockOut( this.ucLETicketNo.Value.Trim().ToUpper() );

						this.DataProvider.CommitTransaction();

						//入库和出货部分，删除出货单和删除入库单时，需要检测出货单或入库单是否已经被删除，如果已经处于删除状态则直接提示用户，出货单××或入库单××已经删除
						if(!result)
						{
							this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_Stock_Out_Ticket_Has_Been_Deleted"));	
							this.ucLETicketNo.TextFocus(false, true);
							return;
						}
					}
					catch( Exception ex )
					{
						this.DataProvider.RollbackTransaction();
						throw ex;
					}

					this.ShowMessage(new UserControl.Message(MessageType.Success,"$CS_Delete_Success "));
				}
				else
				{
					this.ShowMessage("$CS_PLEASE_INPUT_DEL_INFO");
				}
				
			}
			#endregion
		}

		private void ucBtnRemove_Click(object sender, System.EventArgs e)
		{
			try
			{
				#region 移除序列号
				if ( this.ultraOsType.CheckedIndex == (int)CollectionType.PCS )
				{
					if ( this.lstRCardList.SelectedItems.Count <= 0 )
					{
						throw new Exception("$Error_CS_Select_ID_To_Delete");	// 请选择至少一个序列号
					}	
					
					ArrayList idList = new ArrayList();
					ArrayList errList = new ArrayList();

					foreach ( MaterialStockOutDetail item in this.lstRCardList.SelectedItems )
					{	
						if ( item.CollectType != StockCollectionType.PCS )
						{
							errList.Add( item  );
						}
						else
						{
							idList.Add( item  );
						}
					}
					
					// 从List中删除
					foreach ( object obj in idList )
					{
						this.lstRCardList.Items.Remove( obj );
					}

					if ( errList.Count > 0 )
					{
						this.ShowMessage("$Error_CS_Should_Delete_by_Planate");	// 未移除的产品序列号是由二维条码方式添加,只能用二维条码方式移除
					}
					else
					{
						this.ShowMessage(new UserControl.Message(MessageType.Success,"$CS_Remove_Success"));
					}
				}
				#endregion

				#region 移除二维条码

				if ( this.ultraOsType.CheckedIndex == (int)CollectionType.Planate )
				{
					FStockRemove form = new FStockRemove();
					form.Text = this.Text;
				
					if ( form.ShowDialog() == DialogResult.OK )
					{
	
						// 解析二维条码
						string[] idList = this._barCodeFacade.GetIDList( form.Input );
					
						if ( idList == null )
						{
							return;
						}

						bool removed = false;

						foreach ( string id in idList )
						{
							MaterialStockOutDetail stockOut = this.findItemFromList(id);

							if ( stockOut == null )
							{
								continue;
							}

							if ( stockOut.CollectType == StockCollectionType.Planate )
							{
								this.lstRCardList.Items.Remove( stockOut );
							
								removed = true;
							}
						}

						if ( removed )
						{
							this.ShowMessage(new UserControl.Message(MessageType.Success,"$CS_Remove_Success"));
						}
						else
						{
							this.ShowMessage("$CS_BarCode_Not_Found");
						}
					}
				}

				#endregion
				
			}			
			catch( Exception ex )
			{
				this.ShowMessage( ex.Message );
			}
            
			this.ucLECurrQty.Value = this.lstRCardList.Items.Count.ToString();
		}
		
		private void ucLEInput_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar == '\r' )
			{
				if ( ucLEInput.Value.Trim() == string.Empty )
				{
					return;
				}	

				try
				{
					// 如果没有输入数量则报错.
					if(ucLEQty.Value == "")
					{
						this.ShowMessage("$CS_StockOutQty$Error_Input_Empty");
						return;
					}

					//Laws Lu,2005/09/20，新增	数量Check
					int cuNum = ucLEQty.Value == ""?0:int.Parse(ucLEQty.Value);
					if ( cuNum < (lstRCardList.Items.Count + 1) )
					{
						throw new Exception("$Error_CS_CRADCOUNT_MUST_LESS_OUT");
					}

					#region 二维条码采集
					if ( this.ultraOsType.CheckedIndex == (int)CollectionType.Planate )
					{					
						// 解析二维条码						
						string[] idList = this._barCodeFacade.GetIDList( this.ucLEInput.Value.Trim() );
					
						if ( idList == null )
						{
							return;
						}

						bool first = true;

						foreach ( string id in idList )
						{
							// 检查序列号是否已在List中存在
							this.checkRCardExist( id.ToUpper() );

							MaterialStockOutDetail detail = new MaterialStockOutDetail();
							detail.RunningCard = id.ToUpper();
							detail.CollectType = StockCollectionType.Planate;

							// 在解析出的第一个序列号中记下二维条码,即便保存时做产品别检查
							if ( first )	
							{
								detail.CollectNo = this.ucLEInput.Value.Trim();
								first = false;
							}

							this.lstRCardList.Items.Add( detail );
						}
					}
					#endregion

					#region 序列号采集
					if ( this.ultraOsType.CheckedIndex == (int)CollectionType.PCS )
					{
						string id = this.ucLEInput.Value.Trim().ToUpper();
						id = id.Substring(0, Math.Min(40,id.Length));
						
						// 检查序列号是否已在List中存在
						this.checkRCardExist( id );

						MaterialStockOutDetail detail = new MaterialStockOutDetail();
						detail.RunningCard = id;
						detail.CollectType = StockCollectionType.PCS;

						this.lstRCardList.Items.Add( detail );
					}
					#endregion
				}
				catch( Exception ex )
				{
					this.ShowMessage(new UserControl.Message(MessageType.Error,ex.Message));
					this.ucLEInput.TextFocus(false, true);

					return;
				}
				finally
				{
					this.ucLEInput.Value = "";
					this.ucLECurrQty.Value = this.lstRCardList.Items.Count.ToString();

					//Laws Lu,2005/08/27
					//注释 Karron Qiu ,2005-9-19
					//输入产品序列号无论是否成功都不清除数量栏位内容
					//ucLEQty.Value = String.Empty;
					this.ucLEInput.TextFocus(false, true);
				}
			}
		}

		private bool validateInput()
		{
			bool validate = true;

			#region 输入为空检查
			if ( this.ucLETicketNo.Value.Trim() == string.Empty )
			{
				this.ShowMessage("$CS_StockOutTicket$Error_Input_Empty");
				validate = false;
			}
			if ( this.ucLCModel.SelectedIndex == 0 )
			{
				this.ShowMessage("$CS_ModelCode$Error_Input_Empty");
				validate = false;
			}
			if ( this.ucLEDealer.Value.Trim() == string.Empty )
			{
				this.ShowMessage("$CS_Dealer$Error_Input_Empty");
				validate = false;
			}
			if ( this.ucLEQty.Value.Trim() == string.Empty )
			{
				this.ShowMessage("$CS_StockOutQty$Error_Input_Empty");
				validate = false;
			}
			if ( this.lstRCardList.Items.Count == 0 )
			{
				this.ShowMessage("$CS_RCard_List_Is_Empty");
				validate = false;
			}
			#endregion

			#region 出货数量检查
			if ( validate )
			{
				int qty = 0;
				try
				{
					qty = System.Int32.Parse( this.ucLEQty.Value.Trim() );
				}
				catch(FormatException)
				{					
					this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_Qty_Should_be_Integer"));		// 数量必须为整数
					return false;
				}
				catch(OverflowException)
				{	
					this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_Qty_Overflow"));		// 数量过大
					return false;
				}

				if ( qty <= 0 )
				{
					this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_Qty_Should_Over_Zero"));		// 数量必须大于零
					return false;
				}

				if ( this.lstRCardList.Items.Count != qty )
				{
					this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_StockOut_Qty_Not_Equal"));		// 出货数量与产品数量不一致
					return false;
				}
			}
			#endregion

			#region 产品别检查
			if ( validate )
			{
				ArrayList array = new ArrayList();

				foreach ( MaterialStockOutDetail detail in this.lstRCardList.Items )
				{
					if ( detail.CollectType == StockCollectionType.Planate && detail.CollectNo != null && detail.CollectNo != string.Empty )
					{
						if ( this._barCodeFacade.GetModelCode(detail.CollectNo) != this.ucLCModel.SelectedItemValue.ToString() )	
						{
							array.Add(detail.CollectNo);
						}
					}
				}

				if ( array.Count > 0 )
				{
					this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_Planate_Model_Code_Not_Match:"));
						
					foreach ( string code in array )
					{
						this.ShowMessage(code);
					}
					validate = false;
				}
			}
			#endregion 

			return validate;
		}

		private void ucLCModel_Load(object sender, System.EventArgs e)
		{
			#region 初始化产品别
			this.ucLCModel.Clear();
			this.ucLCModel.AddItem("", "");

			object[] objs = new ModelFacade( this.DataProvider ).GetAllModels();

			if ( objs == null )
			{
				return;
			}

			foreach ( Model model in objs )
			{
				this.ucLCModel.AddItem( model.ModelCode, model.ModelCode );
			}

			this.ucLCModel.SelectedIndex = 0;
			#endregion
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			dt.Clear();

			if(!this._facade.IsStockOutTicketExist(this.txtStockNo.Value.Trim().ToUpper()))
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$Error_CS_StockOut_Ticket_Not_Exist"));
				txtStockNo.TextFocus(false, true);
				return;
			}

			#region 获取出货单信息
			object[] objs = this._facade.QueryMaterialStockOut(this.txtStockNo.Value.Trim().ToUpper(),StockStatus.Initial);

			if(objs != null && objs.Length > 0)
			{
				decimal iSum = 0;
				for(int i = 0 ;i< objs.Length ;i++)
				{
					MaterialStockOut mso = (MaterialStockOut)objs[i];
					
					DataRow[] drs = dt.Select("DEALER='" + mso.Dealer + "' and MODEL='" + mso.ModelCode + "'");

					if(drs == null || (drs != null && drs.Length < 1))
					{
						dt.Rows.Add(new object[]
							{
								mso.Dealer
								,mso.ModelCode
								,mso.MaintainDate
								,mso.Quantity
								,mso.Dealer
								,mso.ModelCode
								,mso.Sequence
								,mso.OID
							});
						if(mso.Status == StockStatus.Already)
						{
							iSum = iSum - mso.Quantity;
						}
						else
						{
							iSum = iSum + mso.Quantity;
						}
					}
					else
					{
						dt.Rows.Add(new object[]
							{
								null
								,null
								,mso.MaintainDate
								,mso.Quantity
								,mso.Dealer
								,mso.ModelCode
								,mso.Sequence
								,mso.OID
							});
						if(mso.Status == StockStatus.Already)
						{
							iSum = iSum - mso.Quantity;
						}
						else
						{
							iSum = iSum + mso.Quantity;
						}
					}
					
					txtSumNum.Value= iSum.ToString();
					dt.AcceptChanges();
				}
			}
			else //Added By Karron Qiu ,2005-9-19,请在执行查询时根据用户输入的出货单的实际情况给出对应提示信息，比如出货单不存在、出货单已经被删除、出货单已经出货等等
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$Error_CS_StockOut_Ticket_Deleted_OR_Already"));
				txtStockNo.TextFocus(false, true);
				return;
			}

			ultraStockOut.DisplayLayout.Bands[0].Columns["DEALER"].Hidden = true;
			ultraStockOut.DisplayLayout.Bands[0].Columns["MODEL"].Hidden = true;
			ultraStockOut.DisplayLayout.Bands[0].Columns["Sequence"].Hidden = true;
			//添加OID ,Karron qiu 2005-9-19
			ultraStockOut.DisplayLayout.Bands[0].Columns["OID"].Hidden = true;
			#endregion
		}

		private void FillStockOut()
		{

			DataView dv = dt.DefaultView;

			dv.Sort = "DEALER,MODEL";

			this.ultraStockOut.DataSource = dv;

			ultraStockOut.DisplayLayout.Bands[0].Columns["DEALER"].Hidden = true;
			ultraStockOut.DisplayLayout.Bands[0].Columns["MODEL"].Hidden = true;
			ultraStockOut.DisplayLayout.Bands[0].Columns["Sequence"].Hidden = true;
			//添加OID ,Karron qiu 2005-9-19
			ultraStockOut.DisplayLayout.Bands[0].Columns["OID"].Hidden = true;
		}

		private void InitialDataTable()
		{
			dt.Columns.Add("经销商",typeof(string)).ReadOnly = true;
			dt.Columns.Add("产品别",typeof(string)).ReadOnly = true;
			dt.Columns.Add("出货日期",typeof(string)).ReadOnly = true;
			dt.Columns.Add("数量",typeof(decimal)).ReadOnly = true;
			dt.Columns.Add("DEALER",typeof(string)).ReadOnly = true;
			dt.Columns.Add("MODEL",typeof(string)).ReadOnly = true;			
			dt.Columns.Add("Sequence",typeof(decimal)).ReadOnly = true;
			dt.Columns.Add("OID",typeof(string)).ReadOnly = true;//添加OID ,Karron qiu 2005-9-19		
		}

		private void btnStockOut_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DialogResult.OK != MessageBox.Show("出货单资料出货后不允许作任何变更是否确认出货？",this.Text,MessageBoxButtons.OKCancel))
				{
					return;
				}

				try
				{
					this._domainDataProvider.BeginTransaction();
					//				if(ultraStockOut.Selected.Rows.Count > 0)
					//				{
					//Laws Lu,2005/09/09,修改
//					foreach(Infragistics.Win.UltraWinGrid.UltraGridRow ur in ultraStockOut.Rows)
					for ( int iGridRowLoopIndex = 0; iGridRowLoopIndex < ultraStockOut.Rows.Count; iGridRowLoopIndex++)
					{
						Infragistics.Win.UltraWinGrid.UltraGridRow ur = ultraStockOut.Rows[iGridRowLoopIndex];
						//						if(ur.Selected == true)
						//						{
						MaterialStockOut mso = (MaterialStockOut)this._facade.GetMaterialStockOut(txtStockNo.Value.Trim().ToUpper(),
							Decimal.Parse(ur.Cells["Sequence"].Text.Trim()),ur.Cells["OID"].Text.Trim());
						mso.Status = StockStatus.Already;
						this._facade.UpdateMaterialStockOut(mso);

						//
						//						}
					}
					this._domainDataProvider.CommitTransaction();
				}
				catch
				{
					this._domainDataProvider.RollbackTransaction();
					throw;
				}

				this.ShowMessage(new UserControl.Message(MessageType.Success,"$CS_STOCK_OUT_Success"));
				txtSumNum.Value = "0";

				
				//btnSearch_Click(sender,e);

				//				}
				//				else
				//				{
				//					this.ShowMessage("$CS_PLEASE_SELECT_OUTPUT");
				//				}
			}
			catch(Exception E)
			{
				this.ShowMessage(E.Message);
			}

		}

		private void tabCtrlCollect_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
		{
			if(this.tabCtrlCollect.SelectedTab.Text == "出货")
			{
				ucBtnSave.Visible = false;
			}
			else
			{
				ucBtnSave.Visible = true;
			}
		}
	}
}
