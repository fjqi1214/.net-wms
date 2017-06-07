#region system
using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
#endregion


#region Project
using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.DataCollect;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using Infragistics.Win.UltraWinTree;
#endregion

namespace BenQGuru.eMES.Client
{
    /// <summary>
    /// Form1 的摘要说明。
    /// </summary>
    public class FRMATSInputEdit : System.Windows.Forms.Form
    {
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl2;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage2;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl4;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl5;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private UserControl.UCDatetTime ucDateTimeEnd;
        private UserControl.UCDatetTime ucDateTimeStart;
        private UserControl.UCLabelEdit ucLEOperation;
        private UserControl.UCLabelEdit ucLERunningCard;
        private UserControl.UCLabelEdit ucLEMOCode;
        private UserControl.UCButton ucButtonQuery;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridRepairItem;
        private System.Windows.Forms.Panel panelKeparts;
        private System.Windows.Forms.CheckBox checkBoxTS;
        private System.Windows.Forms.ListView lstSelectedErrorLocation;
        private System.Windows.Forms.ListView lstSelectErrorLocation;
        private UserControl.UCLabelCombox ucLCSolution;
        private UserControl.UCLabelCombox ucLCDuty;
        private UserControl.UCLabelCombox ucLCErrorCause;
        private UserControl.UCLabelEdit ucLEErrorCodeDescription;
        private UserControl.UCLabelEdit ucLEErrorCodeGroupDescription;
        private UserControl.UCButton ucBtnViewTest;
        private UserControl.UCLabelEdit ucLEMemo;
        private UserControl.UCButton ucBtnTSErrorEdit;
        private UserControl.UCLabelEdit ucLEMNID;
        private System.Windows.Forms.Label lblSelectedErrorLocation;
        private UserControl.UCLabelEdit ucLEErrorLocation;
        private UserControl.UCButton ucBtnDeleteErrorLocation;
        private UserControl.UCButton ucBtnAddErrorLocation;
        private UserControl.UCButton ucBtnDelete;
        private UserControl.UCButton ucBtnExit;
        private UserControl.UCButton ucBtnSave;
        private UserControl.UCButton ucBtnCancel;
        private Infragistics.Win.UltraWinTree.UltraTree ultraTreeRunningCard;
        private UserControl.UCButton ucBtnItemDelete;
        private UserControl.UCButton ucBtnItemExit;
        private UserControl.UCButton ucBtnItemSave;
        private UserControl.UCButton ucBtnItemCancel;
        private UserControl.UCButton ucBtnItemUpdate;
        private UserControl.UCButton ucBtnItemAdd;
        private UserControl.UCLabelEdit ucLEItemDescription;
        private UserControl.UCLabelEdit ucLESolutionMemo;
        private UserControl.UCButton ucBtnDeleteErrorPart;
        private System.Windows.Forms.ListView lstSelectedErrorPart;
        private System.Windows.Forms.ListView lstSelectErrorPart;
        private UserControl.UCLabelEdit ucLEErrorPart;
        private UserControl.UCButton ucBtnQueryErrorPart;
        private System.Windows.Forms.Label lblSelectedErrorPart;
        private UserControl.UCButton ucBtnAdd;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTabCtrlTS;
        private UserControl.UCLabelEdit ucLERC;
        private UserControl.UCLabelCombox ucLbSourceItemCode;
        private UserControl.UCLabelEdit ucLEItemSourceRCID;
        private UserControl.UCLabelEdit ucLEItemLocation;
        private UserControl.UCLabelEdit ucLEItemLot;
        private UserControl.UCLabelEdit ucLEItemVendorItem;
        private UserControl.UCLabelEdit ucLEItemDateCode;
        private UserControl.UCLabelEdit ucLEItemVendor;
        private UserControl.UCLabelEdit ucLEItemVersion;
        private UserControl.UCLabelEdit ucLEItemBIOS;
        private UserControl.UCLabelEdit ucLEItemPCBA;
        private UserControl.UCLabelEdit ucLEItemMCard;
        private UserControl.UCLabelCombox ucLCItemMItem;
        private UserControl.UCLabelEdit ucLEResource;
        private UserControl.UCLabelEdit ucLEStepsequence;
        private UserControl.UCLabelEdit ucLEItemCode;
        private UserControl.UCLabelCombox ucLCTSStatus;
        private System.Windows.Forms.TextBox txtItemSequence;
        private UserControl.UCLabelCombox ucLCSplitTSStatus;
        private System.Windows.Forms.GroupBox grpSplitQuery;
        private System.Windows.Forms.Panel panelSplitButton;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridSplitItem;
        private UserControl.UCButton ucBtnSplitExit;
        private UserControl.UCButton ucBtnSplit;
        private System.Windows.Forms.Panel pnlBtn;
        private UserControl.UCButton ucBtnQueryExit;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridTSQuery;
        private UserControl.UCLabelEdit txtAgentUser;
        private UserControl.UCLabelEdit txtAgentSpliter;
        private UserControl.UCLabelCombox ucLCErrorCauseGroup;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTabTSInput;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl6;
        private System.Windows.Forms.GroupBox groupBox3;
        private UserControl.UCLabelCombox ucLabCombox2;
        private UserControl.UCLabelEdit ucLabEdit3;
        private UserControl.UCLabelEdit ucLabEdit2;
        private UserControl.UCLabelEdit ucLabEdit1;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor ultraCheckEditor1;
        private UserControl.UCButton ucButton2;
        private UserControl.UCLabelEdit ucLabelEdit1;
        private UserControl.UCLabelCombox cmbRoute;
        private System.Windows.Forms.CheckBox cbCopyTSInfo;



        private System.ComponentModel.IContainer components = null;


        public FRMATSInputEdit()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(this.ultraGridTSQuery);
            UserControl.UIStyleBuilder.GridUI(this.ultraGridRepairItem);
            UserControl.UIStyleBuilder.GridUI(this.ultraGridSplitItem);

            this._tsFacade = new TSFacade(this.DataProvider);
            this._tsModelFacade = new TSModelFacade(this.DataProvider);

            ucLEMNID.InnerTextBox.MouseDown += new MouseEventHandler(ucLEMNID_MouseDown);
            ucLERC.InnerTextBox.MouseDown += new MouseEventHandler(ucLERC_MouseDown);
        }

        private TSModelFacade _tsModelFacade = null;
        private TSFacade _tsFacade = null;

        private System.Data.DataTable dtTSInformation = new DataTable();
        private System.Data.DataTable dtRepairItem = new DataTable();
        private System.Data.DataTable dtSplitItem = new DataTable();

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        protected void ShowMessage(string message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        protected void ShowMessage(UserControl.Message message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        protected void ShowMessage(Exception e)
        {
            this.ShowMessage(new UserControl.Message(e));
        }


        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMATSInputEdit));
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            this.ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblSelectedErrorLocation = new System.Windows.Forms.Label();
            this.ucBtnAddErrorLocation = new UserControl.UCButton();
            this.ucLEErrorLocation = new UserControl.UCLabelEdit();
            this.ucBtnDeleteErrorLocation = new UserControl.UCButton();
            this.ucBtnDeleteErrorPart = new UserControl.UCButton();
            this.lstSelectedErrorLocation = new System.Windows.Forms.ListView();
            this.lstSelectedErrorPart = new System.Windows.Forms.ListView();
            this.lstSelectErrorLocation = new System.Windows.Forms.ListView();
            this.lstSelectErrorPart = new System.Windows.Forms.ListView();
            this.ucLEErrorPart = new UserControl.UCLabelEdit();
            this.ucBtnQueryErrorPart = new UserControl.UCButton();
            this.lblSelectedErrorPart = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ucLCErrorCauseGroup = new UserControl.UCLabelCombox();
            this.ucLESolutionMemo = new UserControl.UCLabelEdit();
            this.ucLCSolution = new UserControl.UCLabelCombox();
            this.ucLCDuty = new UserControl.UCLabelCombox();
            this.ucLCErrorCause = new UserControl.UCLabelCombox();
            this.ucLEErrorCodeDescription = new UserControl.UCLabelEdit();
            this.ucLEErrorCodeGroupDescription = new UserControl.UCLabelEdit();
            this.panelButton = new System.Windows.Forms.Panel();
            this.ucBtnDelete = new UserControl.UCButton();
            this.ucBtnExit = new UserControl.UCButton();
            this.ucBtnSave = new UserControl.UCButton();
            this.ucBtnCancel = new UserControl.UCButton();
            this.ucBtnAdd = new UserControl.UCButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.ultraTreeRunningCard = new Infragistics.Win.UltraWinTree.UltraTree();
            this.ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ultraGridRepairItem = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panelKeparts = new System.Windows.Forms.Panel();
            this.ucLbSourceItemCode = new UserControl.UCLabelCombox();
            this.ucLEItemSourceRCID = new UserControl.UCLabelEdit();
            this.checkBoxTS = new System.Windows.Forms.CheckBox();
            this.ucLEItemLocation = new UserControl.UCLabelEdit();
            this.ucLEItemMCard = new UserControl.UCLabelEdit();
            this.ucLEItemLot = new UserControl.UCLabelEdit();
            this.ucBtnItemDelete = new UserControl.UCButton();
            this.ucBtnItemExit = new UserControl.UCButton();
            this.ucBtnItemSave = new UserControl.UCButton();
            this.ucBtnItemCancel = new UserControl.UCButton();
            this.ucBtnItemUpdate = new UserControl.UCButton();
            this.ucBtnItemAdd = new UserControl.UCButton();
            this.ucLEItemBIOS = new UserControl.UCLabelEdit();
            this.ucLEItemVendorItem = new UserControl.UCLabelEdit();
            this.ucLEItemPCBA = new UserControl.UCLabelEdit();
            this.ucLEItemDateCode = new UserControl.UCLabelEdit();
            this.ucLEItemDescription = new UserControl.UCLabelEdit();
            this.ucLEItemVendor = new UserControl.UCLabelEdit();
            this.ucLEItemVersion = new UserControl.UCLabelEdit();
            this.ucLCItemMItem = new UserControl.UCLabelCombox();
            this.txtItemSequence = new System.Windows.Forms.TextBox();
            this.ultraTabPageControl6 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ucLabelEdit1 = new UserControl.UCLabelEdit();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbRoute = new UserControl.UCLabelCombox();
            this.ucLabCombox2 = new UserControl.UCLabelCombox();
            this.ultraCheckEditor1 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.ucLabEdit2 = new UserControl.UCLabelEdit();
            this.ucLabEdit1 = new UserControl.UCLabelEdit();
            this.ucButton2 = new UserControl.UCButton();
            this.ucLabEdit3 = new UserControl.UCLabelEdit();
            this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ultraTabTSInput = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbCopyTSInfo = new System.Windows.Forms.CheckBox();
            this.txtAgentUser = new UserControl.UCLabelEdit();
            this.ucBtnViewTest = new UserControl.UCButton();
            this.ucLEMemo = new UserControl.UCLabelEdit();
            this.ucBtnTSErrorEdit = new UserControl.UCButton();
            this.ucLEMNID = new UserControl.UCLabelEdit();
            this.ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ultraGridSplitItem = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panelSplitButton = new System.Windows.Forms.Panel();
            this.ucBtnSplitExit = new UserControl.UCButton();
            this.ucBtnSplit = new UserControl.UCButton();
            this.grpSplitQuery = new System.Windows.Forms.GroupBox();
            this.txtAgentSpliter = new UserControl.UCLabelEdit();
            this.ucLCSplitTSStatus = new UserControl.UCLabelCombox();
            this.ucLERC = new UserControl.UCLabelEdit();
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ultraGridTSQuery = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.pnlBtn = new System.Windows.Forms.Panel();
            this.ucBtnQueryExit = new UserControl.UCButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeEnd = new UserControl.UCDatetTime();
            this.ucDateTimeStart = new UserControl.UCDatetTime();
            this.ucLEResource = new UserControl.UCLabelEdit();
            this.ucLEStepsequence = new UserControl.UCLabelEdit();
            this.ucLEItemCode = new UserControl.UCLabelEdit();
            this.ucLEOperation = new UserControl.UCLabelEdit();
            this.ucLCTSStatus = new UserControl.UCLabelCombox();
            this.ucLERunningCard = new UserControl.UCLabelEdit();
            this.ucLEMOCode = new UserControl.UCLabelEdit();
            this.ucButtonQuery = new UserControl.UCButton();
            this.ultraTabCtrlTS = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.ultraTabPageControl4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTreeRunningCard)).BeginInit();
            this.ultraTabPageControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridRepairItem)).BeginInit();
            this.panel5.SuspendLayout();
            this.panelKeparts.SuspendLayout();
            this.ultraTabPageControl6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.ultraTabPageControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabTSInput)).BeginInit();
            this.ultraTabTSInput.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.ultraTabPageControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridSplitItem)).BeginInit();
            this.panelSplitButton.SuspendLayout();
            this.grpSplitQuery.SuspendLayout();
            this.ultraTabPageControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridTSQuery)).BeginInit();
            this.pnlBtn.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabCtrlTS)).BeginInit();
            this.ultraTabCtrlTS.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraTabPageControl4
            // 
            this.ultraTabPageControl4.Controls.Add(this.panel1);
            this.ultraTabPageControl4.Controls.Add(this.splitter1);
            this.ultraTabPageControl4.Controls.Add(this.ultraTreeRunningCard);
            this.ultraTabPageControl4.Location = new System.Drawing.Point(1, 23);
            this.ultraTabPageControl4.Name = "ultraTabPageControl4";
            this.ultraTabPageControl4.Size = new System.Drawing.Size(856, 529);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panelButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(155, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(701, 529);
            this.panel1.TabIndex = 277;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblSelectedErrorLocation);
            this.panel2.Controls.Add(this.ucBtnAddErrorLocation);
            this.panel2.Controls.Add(this.ucLEErrorLocation);
            this.panel2.Controls.Add(this.ucBtnDeleteErrorLocation);
            this.panel2.Controls.Add(this.ucBtnDeleteErrorPart);
            this.panel2.Controls.Add(this.lstSelectedErrorLocation);
            this.panel2.Controls.Add(this.lstSelectedErrorPart);
            this.panel2.Controls.Add(this.lstSelectErrorLocation);
            this.panel2.Controls.Add(this.lstSelectErrorPart);
            this.panel2.Controls.Add(this.ucLEErrorPart);
            this.panel2.Controls.Add(this.ucBtnQueryErrorPart);
            this.panel2.Controls.Add(this.lblSelectedErrorPart);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 128);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(701, 353);
            this.panel2.TabIndex = 1;
            // 
            // lblSelectedErrorLocation
            // 
            this.lblSelectedErrorLocation.Location = new System.Drawing.Point(8, 8);
            this.lblSelectedErrorLocation.Name = "lblSelectedErrorLocation";
            this.lblSelectedErrorLocation.Size = new System.Drawing.Size(80, 23);
            this.lblSelectedErrorLocation.TabIndex = 302;
            this.lblSelectedErrorLocation.Text = "已选不良位置";
            this.lblSelectedErrorLocation.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // ucBtnAddErrorLocation
            // 
            this.ucBtnAddErrorLocation.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnAddErrorLocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnAddErrorLocation.BackgroundImage")));
            this.ucBtnAddErrorLocation.ButtonType = UserControl.ButtonTypes.Add;
            this.ucBtnAddErrorLocation.Caption = "添加";
            this.ucBtnAddErrorLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnAddErrorLocation.Location = new System.Drawing.Point(528, 8);
            this.ucBtnAddErrorLocation.Name = "ucBtnAddErrorLocation";
            this.ucBtnAddErrorLocation.Size = new System.Drawing.Size(88, 22);
            this.ucBtnAddErrorLocation.TabIndex = 4;
            this.ucBtnAddErrorLocation.Click += new System.EventHandler(this.ucBtnAddErrorLocation_Click);
            // 
            // ucLEErrorLocation
            // 
            this.ucLEErrorLocation.AllowEditOnlyChecked = true;
            this.ucLEErrorLocation.Caption = "不良位置";
            this.ucLEErrorLocation.Checked = false;
            this.ucLEErrorLocation.EditType = UserControl.EditTypes.String;
            this.ucLEErrorLocation.Location = new System.Drawing.Point(313, 8);
            this.ucLEErrorLocation.MaxLength = 40;
            this.ucLEErrorLocation.Multiline = false;
            this.ucLEErrorLocation.Name = "ucLEErrorLocation";
            this.ucLEErrorLocation.PasswordChar = '\0';
            this.ucLEErrorLocation.ReadOnly = false;
            this.ucLEErrorLocation.ShowCheckBox = false;
            this.ucLEErrorLocation.Size = new System.Drawing.Size(161, 24);
            this.ucLEErrorLocation.TabIndex = 3;
            this.ucLEErrorLocation.TabNext = false;
            this.ucLEErrorLocation.Value = "";
            this.ucLEErrorLocation.WidthType = UserControl.WidthTypes.Small;
            this.ucLEErrorLocation.XAlign = 374;
            this.ucLEErrorLocation.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEErrorLocation_TxtboxKeyPress);
            // 
            // ucBtnDeleteErrorLocation
            // 
            this.ucBtnDeleteErrorLocation.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnDeleteErrorLocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnDeleteErrorLocation.BackgroundImage")));
            this.ucBtnDeleteErrorLocation.ButtonType = UserControl.ButtonTypes.Delete;
            this.ucBtnDeleteErrorLocation.Caption = "删除";
            this.ucBtnDeleteErrorLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnDeleteErrorLocation.Location = new System.Drawing.Point(216, 8);
            this.ucBtnDeleteErrorLocation.Name = "ucBtnDeleteErrorLocation";
            this.ucBtnDeleteErrorLocation.Size = new System.Drawing.Size(88, 22);
            this.ucBtnDeleteErrorLocation.TabIndex = 1;
            this.ucBtnDeleteErrorLocation.Click += new System.EventHandler(this.ucBtnDeleteErrorLocation_Click);
            // 
            // ucBtnDeleteErrorPart
            // 
            this.ucBtnDeleteErrorPart.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnDeleteErrorPart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnDeleteErrorPart.BackgroundImage")));
            this.ucBtnDeleteErrorPart.ButtonType = UserControl.ButtonTypes.Delete;
            this.ucBtnDeleteErrorPart.Caption = "删除";
            this.ucBtnDeleteErrorPart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnDeleteErrorPart.Location = new System.Drawing.Point(216, 200);
            this.ucBtnDeleteErrorPart.Name = "ucBtnDeleteErrorPart";
            this.ucBtnDeleteErrorPart.Size = new System.Drawing.Size(88, 22);
            this.ucBtnDeleteErrorPart.TabIndex = 6;
            this.ucBtnDeleteErrorPart.Click += new System.EventHandler(this.ucBtnDeleteErrorPart_Click);
            // 
            // lstSelectedErrorLocation
            // 
            this.lstSelectedErrorLocation.LabelWrap = false;
            this.lstSelectedErrorLocation.Location = new System.Drawing.Point(8, 32);
            this.lstSelectedErrorLocation.Name = "lstSelectedErrorLocation";
            this.lstSelectedErrorLocation.Size = new System.Drawing.Size(296, 152);
            this.lstSelectedErrorLocation.TabIndex = 0;
            this.lstSelectedErrorLocation.UseCompatibleStateImageBehavior = false;
            this.lstSelectedErrorLocation.View = System.Windows.Forms.View.List;
            this.lstSelectedErrorLocation.DoubleClick += new System.EventHandler(this.lstSelectedErrorLocation_DoubleClick);
            // 
            // lstSelectedErrorPart
            // 
            this.lstSelectedErrorPart.LabelWrap = false;
            this.lstSelectedErrorPart.Location = new System.Drawing.Point(8, 224);
            this.lstSelectedErrorPart.Name = "lstSelectedErrorPart";
            this.lstSelectedErrorPart.Size = new System.Drawing.Size(296, 120);
            this.lstSelectedErrorPart.TabIndex = 5;
            this.lstSelectedErrorPart.UseCompatibleStateImageBehavior = false;
            this.lstSelectedErrorPart.View = System.Windows.Forms.View.List;
            this.lstSelectedErrorPart.DoubleClick += new System.EventHandler(this.lstSelectedErrorPart_DoubleClick);
            // 
            // lstSelectErrorLocation
            // 
            this.lstSelectErrorLocation.LabelWrap = false;
            this.lstSelectErrorLocation.Location = new System.Drawing.Point(312, 32);
            this.lstSelectErrorLocation.Name = "lstSelectErrorLocation";
            this.lstSelectErrorLocation.Size = new System.Drawing.Size(304, 152);
            this.lstSelectErrorLocation.TabIndex = 2;
            this.lstSelectErrorLocation.UseCompatibleStateImageBehavior = false;
            this.lstSelectErrorLocation.View = System.Windows.Forms.View.List;
            this.lstSelectErrorLocation.DoubleClick += new System.EventHandler(this.lstSelectErrorLocation_DoubleClick);
            // 
            // lstSelectErrorPart
            // 
            this.lstSelectErrorPart.LabelWrap = false;
            this.lstSelectErrorPart.Location = new System.Drawing.Point(312, 224);
            this.lstSelectErrorPart.Name = "lstSelectErrorPart";
            this.lstSelectErrorPart.Size = new System.Drawing.Size(304, 120);
            this.lstSelectErrorPart.TabIndex = 7;
            this.lstSelectErrorPart.UseCompatibleStateImageBehavior = false;
            this.lstSelectErrorPart.View = System.Windows.Forms.View.List;
            this.lstSelectErrorPart.DoubleClick += new System.EventHandler(this.lstSelectErrorPart_DoubleClick);
            // 
            // ucLEErrorPart
            // 
            this.ucLEErrorPart.AllowEditOnlyChecked = true;
            this.ucLEErrorPart.Caption = "不良零件";
            this.ucLEErrorPart.Checked = false;
            this.ucLEErrorPart.EditType = UserControl.EditTypes.String;
            this.ucLEErrorPart.Location = new System.Drawing.Point(313, 200);
            this.ucLEErrorPart.MaxLength = 40;
            this.ucLEErrorPart.Multiline = false;
            this.ucLEErrorPart.Name = "ucLEErrorPart";
            this.ucLEErrorPart.PasswordChar = '\0';
            this.ucLEErrorPart.ReadOnly = false;
            this.ucLEErrorPart.ShowCheckBox = false;
            this.ucLEErrorPart.Size = new System.Drawing.Size(161, 24);
            this.ucLEErrorPart.TabIndex = 8;
            this.ucLEErrorPart.TabNext = false;
            this.ucLEErrorPart.Value = "";
            this.ucLEErrorPart.WidthType = UserControl.WidthTypes.Small;
            this.ucLEErrorPart.XAlign = 374;
            this.ucLEErrorPart.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEErrorPart_TxtboxKeyPress);
            // 
            // ucBtnQueryErrorPart
            // 
            this.ucBtnQueryErrorPart.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnQueryErrorPart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnQueryErrorPart.BackgroundImage")));
            this.ucBtnQueryErrorPart.ButtonType = UserControl.ButtonTypes.Query;
            this.ucBtnQueryErrorPart.Caption = "查询";
            this.ucBtnQueryErrorPart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnQueryErrorPart.Location = new System.Drawing.Point(528, 200);
            this.ucBtnQueryErrorPart.Name = "ucBtnQueryErrorPart";
            this.ucBtnQueryErrorPart.Size = new System.Drawing.Size(88, 22);
            this.ucBtnQueryErrorPart.TabIndex = 9;
            this.ucBtnQueryErrorPart.Click += new System.EventHandler(this.ucBtnQueryErrorPart_Click);
            // 
            // lblSelectedErrorPart
            // 
            this.lblSelectedErrorPart.Location = new System.Drawing.Point(8, 200);
            this.lblSelectedErrorPart.Name = "lblSelectedErrorPart";
            this.lblSelectedErrorPart.Size = new System.Drawing.Size(100, 23);
            this.lblSelectedErrorPart.TabIndex = 302;
            this.lblSelectedErrorPart.Text = "已选不良零件";
            this.lblSelectedErrorPart.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ucLCErrorCauseGroup);
            this.panel4.Controls.Add(this.ucLESolutionMemo);
            this.panel4.Controls.Add(this.ucLCSolution);
            this.panel4.Controls.Add(this.ucLCDuty);
            this.panel4.Controls.Add(this.ucLCErrorCause);
            this.panel4.Controls.Add(this.ucLEErrorCodeDescription);
            this.panel4.Controls.Add(this.ucLEErrorCodeGroupDescription);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(701, 128);
            this.panel4.TabIndex = 0;
            // 
            // ucLCErrorCauseGroup
            // 
            this.ucLCErrorCauseGroup.AllowEditOnlyChecked = true;
            this.ucLCErrorCauseGroup.Caption = "不良原因组";
            this.ucLCErrorCauseGroup.Checked = false;
            this.ucLCErrorCauseGroup.Location = new System.Drawing.Point(29, 40);
            this.ucLCErrorCauseGroup.Name = "ucLCErrorCauseGroup";
            this.ucLCErrorCauseGroup.SelectedIndex = -1;
            this.ucLCErrorCauseGroup.ShowCheckBox = false;
            this.ucLCErrorCauseGroup.Size = new System.Drawing.Size(273, 24);
            this.ucLCErrorCauseGroup.TabIndex = 6;
            this.ucLCErrorCauseGroup.WidthType = UserControl.WidthTypes.Long;
            this.ucLCErrorCauseGroup.XAlign = 102;
            this.ucLCErrorCauseGroup.Load += new System.EventHandler(this.ucLCErrorCauseGroup_Load);
            this.ucLCErrorCauseGroup.SelectedIndexChanged += new System.EventHandler(this.ucLCErrorCauseGroup_SelectedIndexChanged);
            // 
            // ucLESolutionMemo
            // 
            this.ucLESolutionMemo.AllowEditOnlyChecked = true;
            this.ucLESolutionMemo.Caption = "补充说明";
            this.ucLESolutionMemo.Checked = false;
            this.ucLESolutionMemo.EditType = UserControl.EditTypes.String;
            this.ucLESolutionMemo.Location = new System.Drawing.Point(353, 96);
            this.ucLESolutionMemo.MaxLength = 100;
            this.ucLESolutionMemo.Multiline = false;
            this.ucLESolutionMemo.Name = "ucLESolutionMemo";
            this.ucLESolutionMemo.PasswordChar = '\0';
            this.ucLESolutionMemo.ReadOnly = false;
            this.ucLESolutionMemo.ShowCheckBox = false;
            this.ucLESolutionMemo.Size = new System.Drawing.Size(261, 24);
            this.ucLESolutionMemo.TabIndex = 5;
            this.ucLESolutionMemo.TabNext = true;
            this.ucLESolutionMemo.Value = "";
            this.ucLESolutionMemo.WidthType = UserControl.WidthTypes.Long;
            this.ucLESolutionMemo.XAlign = 414;
            // 
            // ucLCSolution
            // 
            this.ucLCSolution.AllowEditOnlyChecked = true;
            this.ucLCSolution.Caption = "解决方案";
            this.ucLCSolution.Checked = false;
            this.ucLCSolution.Location = new System.Drawing.Point(41, 96);
            this.ucLCSolution.Name = "ucLCSolution";
            this.ucLCSolution.SelectedIndex = -1;
            this.ucLCSolution.ShowCheckBox = false;
            this.ucLCSolution.Size = new System.Drawing.Size(261, 24);
            this.ucLCSolution.TabIndex = 4;
            this.ucLCSolution.WidthType = UserControl.WidthTypes.Long;
            this.ucLCSolution.XAlign = 102;
            this.ucLCSolution.Load += new System.EventHandler(this.ucLCSolution_Load);
            // 
            // ucLCDuty
            // 
            this.ucLCDuty.AllowEditOnlyChecked = true;
            this.ucLCDuty.Caption = "责任别";
            this.ucLCDuty.Checked = false;
            this.ucLCDuty.Location = new System.Drawing.Point(53, 64);
            this.ucLCDuty.Name = "ucLCDuty";
            this.ucLCDuty.SelectedIndex = -1;
            this.ucLCDuty.ShowCheckBox = false;
            this.ucLCDuty.Size = new System.Drawing.Size(249, 24);
            this.ucLCDuty.TabIndex = 3;
            this.ucLCDuty.WidthType = UserControl.WidthTypes.Long;
            this.ucLCDuty.XAlign = 102;
            this.ucLCDuty.Load += new System.EventHandler(this.ucLCDuty_Load);
            // 
            // ucLCErrorCause
            // 
            this.ucLCErrorCause.AllowEditOnlyChecked = true;
            this.ucLCErrorCause.Caption = "不良原因";
            this.ucLCErrorCause.Checked = false;
            this.ucLCErrorCause.Location = new System.Drawing.Point(353, 40);
            this.ucLCErrorCause.Name = "ucLCErrorCause";
            this.ucLCErrorCause.SelectedIndex = -1;
            this.ucLCErrorCause.ShowCheckBox = false;
            this.ucLCErrorCause.Size = new System.Drawing.Size(261, 24);
            this.ucLCErrorCause.TabIndex = 2;
            this.ucLCErrorCause.WidthType = UserControl.WidthTypes.Long;
            this.ucLCErrorCause.XAlign = 414;
            this.ucLCErrorCause.Load += new System.EventHandler(this.ucLCErrorCause_Load);
            // 
            // ucLEErrorCodeDescription
            // 
            this.ucLEErrorCodeDescription.AllowEditOnlyChecked = true;
            this.ucLEErrorCodeDescription.Caption = "不良代码描述";
            this.ucLEErrorCodeDescription.Checked = false;
            this.ucLEErrorCodeDescription.EditType = UserControl.EditTypes.String;
            this.ucLEErrorCodeDescription.Location = new System.Drawing.Point(329, 10);
            this.ucLEErrorCodeDescription.MaxLength = 100;
            this.ucLEErrorCodeDescription.Multiline = false;
            this.ucLEErrorCodeDescription.Name = "ucLEErrorCodeDescription";
            this.ucLEErrorCodeDescription.PasswordChar = '\0';
            this.ucLEErrorCodeDescription.ReadOnly = true;
            this.ucLEErrorCodeDescription.ShowCheckBox = false;
            this.ucLEErrorCodeDescription.Size = new System.Drawing.Size(285, 24);
            this.ucLEErrorCodeDescription.TabIndex = 1;
            this.ucLEErrorCodeDescription.TabNext = true;
            this.ucLEErrorCodeDescription.Value = "";
            this.ucLEErrorCodeDescription.WidthType = UserControl.WidthTypes.Long;
            this.ucLEErrorCodeDescription.XAlign = 414;
            // 
            // ucLEErrorCodeGroupDescription
            // 
            this.ucLEErrorCodeGroupDescription.AllowEditOnlyChecked = true;
            this.ucLEErrorCodeGroupDescription.Caption = "不良代码组描述";
            this.ucLEErrorCodeGroupDescription.Checked = false;
            this.ucLEErrorCodeGroupDescription.EditType = UserControl.EditTypes.String;
            this.ucLEErrorCodeGroupDescription.Location = new System.Drawing.Point(5, 10);
            this.ucLEErrorCodeGroupDescription.MaxLength = 100;
            this.ucLEErrorCodeGroupDescription.Multiline = false;
            this.ucLEErrorCodeGroupDescription.Name = "ucLEErrorCodeGroupDescription";
            this.ucLEErrorCodeGroupDescription.PasswordChar = '\0';
            this.ucLEErrorCodeGroupDescription.ReadOnly = true;
            this.ucLEErrorCodeGroupDescription.ShowCheckBox = false;
            this.ucLEErrorCodeGroupDescription.Size = new System.Drawing.Size(297, 24);
            this.ucLEErrorCodeGroupDescription.TabIndex = 0;
            this.ucLEErrorCodeGroupDescription.TabNext = true;
            this.ucLEErrorCodeGroupDescription.Value = "";
            this.ucLEErrorCodeGroupDescription.WidthType = UserControl.WidthTypes.Long;
            this.ucLEErrorCodeGroupDescription.XAlign = 102;
            // 
            // panelButton
            // 
            this.panelButton.Controls.Add(this.ucBtnDelete);
            this.panelButton.Controls.Add(this.ucBtnExit);
            this.panelButton.Controls.Add(this.ucBtnSave);
            this.panelButton.Controls.Add(this.ucBtnCancel);
            this.panelButton.Controls.Add(this.ucBtnAdd);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(0, 481);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(701, 48);
            this.panelButton.TabIndex = 2;
            // 
            // ucBtnDelete
            // 
            this.ucBtnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnDelete.BackgroundImage")));
            this.ucBtnDelete.ButtonType = UserControl.ButtonTypes.Delete;
            this.ucBtnDelete.Caption = "删除";
            this.ucBtnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnDelete.Location = new System.Drawing.Point(154, 8);
            this.ucBtnDelete.Name = "ucBtnDelete";
            this.ucBtnDelete.Size = new System.Drawing.Size(88, 22);
            this.ucBtnDelete.TabIndex = 2;
            this.ucBtnDelete.Click += new System.EventHandler(this.ucBtnDelete_Click);
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(466, 8);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 5;
            // 
            // ucBtnSave
            // 
            this.ucBtnSave.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnSave.BackgroundImage")));
            this.ucBtnSave.ButtonType = UserControl.ButtonTypes.Save;
            this.ucBtnSave.Caption = "保存";
            this.ucBtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnSave.Location = new System.Drawing.Point(258, 8);
            this.ucBtnSave.Name = "ucBtnSave";
            this.ucBtnSave.Size = new System.Drawing.Size(88, 22);
            this.ucBtnSave.TabIndex = 3;
            this.ucBtnSave.Click += new System.EventHandler(this.ucBtnSave_Click);
            // 
            // ucBtnCancel
            // 
            this.ucBtnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnCancel.BackgroundImage")));
            this.ucBtnCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.ucBtnCancel.Caption = "取消";
            this.ucBtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnCancel.Location = new System.Drawing.Point(362, 8);
            this.ucBtnCancel.Name = "ucBtnCancel";
            this.ucBtnCancel.Size = new System.Drawing.Size(88, 22);
            this.ucBtnCancel.TabIndex = 4;
            this.ucBtnCancel.Click += new System.EventHandler(this.ucBtnCancel_Click);
            // 
            // ucBtnAdd
            // 
            this.ucBtnAdd.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnAdd.BackgroundImage")));
            this.ucBtnAdd.ButtonType = UserControl.ButtonTypes.Add;
            this.ucBtnAdd.Caption = "添加";
            this.ucBtnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnAdd.Location = new System.Drawing.Point(50, 8);
            this.ucBtnAdd.Name = "ucBtnAdd";
            this.ucBtnAdd.Size = new System.Drawing.Size(88, 22);
            this.ucBtnAdd.TabIndex = 0;
            this.ucBtnAdd.Click += new System.EventHandler(this.ucBtnAdd_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(152, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 529);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // ultraTreeRunningCard
            // 
            this.ultraTreeRunningCard.Dock = System.Windows.Forms.DockStyle.Left;
            this.ultraTreeRunningCard.HideSelection = false;
            this.ultraTreeRunningCard.Location = new System.Drawing.Point(0, 0);
            this.ultraTreeRunningCard.Name = "ultraTreeRunningCard";
            this.ultraTreeRunningCard.Size = new System.Drawing.Size(152, 529);
            this.ultraTreeRunningCard.TabIndex = 0;
            this.ultraTreeRunningCard.AfterActivate += new Infragistics.Win.UltraWinTree.AfterNodeChangedEventHandler(this.ultraTreeRunningCard_AfterActivate);
            // 
            // ultraTabPageControl5
            // 
            this.ultraTabPageControl5.Controls.Add(this.ultraGridRepairItem);
            this.ultraTabPageControl5.Controls.Add(this.panel5);
            this.ultraTabPageControl5.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl5.Name = "ultraTabPageControl5";
            this.ultraTabPageControl5.Size = new System.Drawing.Size(856, 529);
            // 
            // ultraGridRepairItem
            // 
            this.ultraGridRepairItem.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridRepairItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridRepairItem.Location = new System.Drawing.Point(0, 0);
            this.ultraGridRepairItem.Name = "ultraGridRepairItem";
            this.ultraGridRepairItem.Size = new System.Drawing.Size(856, 313);
            this.ultraGridRepairItem.TabIndex = 0;
            this.ultraGridRepairItem.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridRepairItem_InitializeLayout);
            this.ultraGridRepairItem.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridRepairItem_CellChange);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panelKeparts);
            this.panel5.Controls.Add(this.ucLEItemLocation);
            this.panel5.Controls.Add(this.ucLEItemMCard);
            this.panel5.Controls.Add(this.ucLEItemLot);
            this.panel5.Controls.Add(this.ucBtnItemDelete);
            this.panel5.Controls.Add(this.ucBtnItemExit);
            this.panel5.Controls.Add(this.ucBtnItemSave);
            this.panel5.Controls.Add(this.ucBtnItemCancel);
            this.panel5.Controls.Add(this.ucBtnItemUpdate);
            this.panel5.Controls.Add(this.ucBtnItemAdd);
            this.panel5.Controls.Add(this.ucLEItemBIOS);
            this.panel5.Controls.Add(this.ucLEItemVendorItem);
            this.panel5.Controls.Add(this.ucLEItemPCBA);
            this.panel5.Controls.Add(this.ucLEItemDateCode);
            this.panel5.Controls.Add(this.ucLEItemDescription);
            this.panel5.Controls.Add(this.ucLEItemVendor);
            this.panel5.Controls.Add(this.ucLEItemVersion);
            this.panel5.Controls.Add(this.ucLCItemMItem);
            this.panel5.Controls.Add(this.txtItemSequence);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 313);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(856, 216);
            this.panel5.TabIndex = 0;
            // 
            // panelKeparts
            // 
            this.panelKeparts.Controls.Add(this.ucLbSourceItemCode);
            this.panelKeparts.Controls.Add(this.ucLEItemSourceRCID);
            this.panelKeparts.Controls.Add(this.checkBoxTS);
            this.panelKeparts.Location = new System.Drawing.Point(26, 40);
            this.panelKeparts.Name = "panelKeparts";
            this.panelKeparts.Size = new System.Drawing.Size(718, 32);
            this.panelKeparts.TabIndex = 329;
            // 
            // ucLbSourceItemCode
            // 
            this.ucLbSourceItemCode.AllowEditOnlyChecked = true;
            this.ucLbSourceItemCode.Caption = "原物料号";
            this.ucLbSourceItemCode.Checked = false;
            this.ucLbSourceItemCode.Location = new System.Drawing.Point(9, 8);
            this.ucLbSourceItemCode.Name = "ucLbSourceItemCode";
            this.ucLbSourceItemCode.SelectedIndex = -1;
            this.ucLbSourceItemCode.ShowCheckBox = false;
            this.ucLbSourceItemCode.Size = new System.Drawing.Size(194, 24);
            this.ucLbSourceItemCode.TabIndex = 1;
            this.ucLbSourceItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLbSourceItemCode.XAlign = 70;
            // 
            // ucLEItemSourceRCID
            // 
            this.ucLEItemSourceRCID.AllowEditOnlyChecked = true;
            this.ucLEItemSourceRCID.Caption = "原物料序列号";
            this.ucLEItemSourceRCID.Checked = false;
            this.ucLEItemSourceRCID.EditType = UserControl.EditTypes.String;
            this.ucLEItemSourceRCID.Location = new System.Drawing.Point(250, 4);
            this.ucLEItemSourceRCID.MaxLength = 40;
            this.ucLEItemSourceRCID.Multiline = false;
            this.ucLEItemSourceRCID.Name = "ucLEItemSourceRCID";
            this.ucLEItemSourceRCID.PasswordChar = '\0';
            this.ucLEItemSourceRCID.ReadOnly = false;
            this.ucLEItemSourceRCID.ShowCheckBox = false;
            this.ucLEItemSourceRCID.Size = new System.Drawing.Size(218, 24);
            this.ucLEItemSourceRCID.TabIndex = 2;
            this.ucLEItemSourceRCID.TabNext = true;
            this.ucLEItemSourceRCID.Value = "";
            this.ucLEItemSourceRCID.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemSourceRCID.XAlign = 335;
            // 
            // checkBoxTS
            // 
            this.checkBoxTS.Location = new System.Drawing.Point(528, 0);
            this.checkBoxTS.Name = "checkBoxTS";
            this.checkBoxTS.Size = new System.Drawing.Size(104, 24);
            this.checkBoxTS.TabIndex = 0;
            this.checkBoxTS.Text = "是否再维修";
            this.checkBoxTS.Click += new System.EventHandler(this.checkBoxTS_Click);
            // 
            // ucLEItemLocation
            // 
            this.ucLEItemLocation.AllowEditOnlyChecked = true;
            this.ucLEItemLocation.Caption = "零件位置";
            this.ucLEItemLocation.Checked = false;
            this.ucLEItemLocation.EditType = UserControl.EditTypes.String;
            this.ucLEItemLocation.Location = new System.Drawing.Point(36, 80);
            this.ucLEItemLocation.MaxLength = 40;
            this.ucLEItemLocation.Multiline = false;
            this.ucLEItemLocation.Name = "ucLEItemLocation";
            this.ucLEItemLocation.PasswordChar = '\0';
            this.ucLEItemLocation.ReadOnly = false;
            this.ucLEItemLocation.ShowCheckBox = false;
            this.ucLEItemLocation.Size = new System.Drawing.Size(194, 24);
            this.ucLEItemLocation.TabIndex = 3;
            this.ucLEItemLocation.TabNext = true;
            this.ucLEItemLocation.Value = "";
            this.ucLEItemLocation.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemLocation.XAlign = 97;
            // 
            // ucLEItemMCard
            // 
            this.ucLEItemMCard.AllowEditOnlyChecked = true;
            this.ucLEItemMCard.Caption = "物料序列号";
            this.ucLEItemMCard.Checked = false;
            this.ucLEItemMCard.EditType = UserControl.EditTypes.String;
            this.ucLEItemMCard.Location = new System.Drawing.Point(289, 8);
            this.ucLEItemMCard.MaxLength = 40;
            this.ucLEItemMCard.Multiline = false;
            this.ucLEItemMCard.Name = "ucLEItemMCard";
            this.ucLEItemMCard.PasswordChar = '\0';
            this.ucLEItemMCard.ReadOnly = false;
            this.ucLEItemMCard.ShowCheckBox = false;
            this.ucLEItemMCard.Size = new System.Drawing.Size(206, 24);
            this.ucLEItemMCard.TabIndex = 1;
            this.ucLEItemMCard.TabNext = true;
            this.ucLEItemMCard.Value = "";
            this.ucLEItemMCard.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemMCard.XAlign = 362;
            // 
            // ucLEItemLot
            // 
            this.ucLEItemLot.AllowEditOnlyChecked = true;
            this.ucLEItemLot.Caption = "批号";
            this.ucLEItemLot.Checked = false;
            this.ucLEItemLot.EditType = UserControl.EditTypes.String;
            this.ucLEItemLot.Location = new System.Drawing.Point(575, 8);
            this.ucLEItemLot.MaxLength = 40;
            this.ucLEItemLot.Multiline = false;
            this.ucLEItemLot.Name = "ucLEItemLot";
            this.ucLEItemLot.PasswordChar = '\0';
            this.ucLEItemLot.ReadOnly = false;
            this.ucLEItemLot.ShowCheckBox = false;
            this.ucLEItemLot.Size = new System.Drawing.Size(170, 24);
            this.ucLEItemLot.TabIndex = 2;
            this.ucLEItemLot.TabNext = true;
            this.ucLEItemLot.Value = "";
            this.ucLEItemLot.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemLot.XAlign = 612;
            // 
            // ucBtnItemDelete
            // 
            this.ucBtnItemDelete.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnItemDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnItemDelete.BackgroundImage")));
            this.ucBtnItemDelete.ButtonType = UserControl.ButtonTypes.Delete;
            this.ucBtnItemDelete.Caption = "删除";
            this.ucBtnItemDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnItemDelete.Location = new System.Drawing.Point(300, 176);
            this.ucBtnItemDelete.Name = "ucBtnItemDelete";
            this.ucBtnItemDelete.Size = new System.Drawing.Size(88, 22);
            this.ucBtnItemDelete.TabIndex = 13;
            this.ucBtnItemDelete.Click += new System.EventHandler(this.ucBtnItemDelete_Click);
            // 
            // ucBtnItemExit
            // 
            this.ucBtnItemExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnItemExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnItemExit.BackgroundImage")));
            this.ucBtnItemExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnItemExit.Caption = "退出";
            this.ucBtnItemExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnItemExit.Location = new System.Drawing.Point(612, 176);
            this.ucBtnItemExit.Name = "ucBtnItemExit";
            this.ucBtnItemExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnItemExit.TabIndex = 16;
            // 
            // ucBtnItemSave
            // 
            this.ucBtnItemSave.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnItemSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnItemSave.BackgroundImage")));
            this.ucBtnItemSave.ButtonType = UserControl.ButtonTypes.Save;
            this.ucBtnItemSave.Caption = "保存";
            this.ucBtnItemSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnItemSave.Location = new System.Drawing.Point(404, 176);
            this.ucBtnItemSave.Name = "ucBtnItemSave";
            this.ucBtnItemSave.Size = new System.Drawing.Size(88, 22);
            this.ucBtnItemSave.TabIndex = 14;
            this.ucBtnItemSave.Click += new System.EventHandler(this.ucBtnItemSave_Click);
            // 
            // ucBtnItemCancel
            // 
            this.ucBtnItemCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnItemCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnItemCancel.BackgroundImage")));
            this.ucBtnItemCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.ucBtnItemCancel.Caption = "取消";
            this.ucBtnItemCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnItemCancel.Location = new System.Drawing.Point(508, 176);
            this.ucBtnItemCancel.Name = "ucBtnItemCancel";
            this.ucBtnItemCancel.Size = new System.Drawing.Size(88, 22);
            this.ucBtnItemCancel.TabIndex = 15;
            this.ucBtnItemCancel.Click += new System.EventHandler(this.ucBtnItemCancel_Click);
            // 
            // ucBtnItemUpdate
            // 
            this.ucBtnItemUpdate.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnItemUpdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnItemUpdate.BackgroundImage")));
            this.ucBtnItemUpdate.ButtonType = UserControl.ButtonTypes.Edit;
            this.ucBtnItemUpdate.Caption = "编辑";
            this.ucBtnItemUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnItemUpdate.Location = new System.Drawing.Point(196, 176);
            this.ucBtnItemUpdate.Name = "ucBtnItemUpdate";
            this.ucBtnItemUpdate.Size = new System.Drawing.Size(88, 22);
            this.ucBtnItemUpdate.TabIndex = 12;
            this.ucBtnItemUpdate.Click += new System.EventHandler(this.ucBtnItemUpdate_Click);
            // 
            // ucBtnItemAdd
            // 
            this.ucBtnItemAdd.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnItemAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnItemAdd.BackgroundImage")));
            this.ucBtnItemAdd.ButtonType = UserControl.ButtonTypes.Add;
            this.ucBtnItemAdd.Caption = "添加";
            this.ucBtnItemAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnItemAdd.Location = new System.Drawing.Point(84, 176);
            this.ucBtnItemAdd.Name = "ucBtnItemAdd";
            this.ucBtnItemAdd.Size = new System.Drawing.Size(88, 22);
            this.ucBtnItemAdd.TabIndex = 11;
            this.ucBtnItemAdd.Click += new System.EventHandler(this.ucButtonItemAdd_Click);
            // 
            // ucLEItemBIOS
            // 
            this.ucLEItemBIOS.AllowEditOnlyChecked = true;
            this.ucLEItemBIOS.Caption = "BIOS版本";
            this.ucLEItemBIOS.Checked = false;
            this.ucLEItemBIOS.EditType = UserControl.EditTypes.String;
            this.ucLEItemBIOS.Location = new System.Drawing.Point(301, 144);
            this.ucLEItemBIOS.MaxLength = 40;
            this.ucLEItemBIOS.Multiline = false;
            this.ucLEItemBIOS.Name = "ucLEItemBIOS";
            this.ucLEItemBIOS.PasswordChar = '\0';
            this.ucLEItemBIOS.ReadOnly = false;
            this.ucLEItemBIOS.ShowCheckBox = false;
            this.ucLEItemBIOS.Size = new System.Drawing.Size(194, 24);
            this.ucLEItemBIOS.TabIndex = 10;
            this.ucLEItemBIOS.TabNext = true;
            this.ucLEItemBIOS.Value = "";
            this.ucLEItemBIOS.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemBIOS.XAlign = 362;
            // 
            // ucLEItemVendorItem
            // 
            this.ucLEItemVendorItem.AllowEditOnlyChecked = true;
            this.ucLEItemVendorItem.Caption = "厂商料号";
            this.ucLEItemVendorItem.Checked = false;
            this.ucLEItemVendorItem.EditType = UserControl.EditTypes.String;
            this.ucLEItemVendorItem.Location = new System.Drawing.Point(301, 112);
            this.ucLEItemVendorItem.MaxLength = 40;
            this.ucLEItemVendorItem.Multiline = false;
            this.ucLEItemVendorItem.Name = "ucLEItemVendorItem";
            this.ucLEItemVendorItem.PasswordChar = '\0';
            this.ucLEItemVendorItem.ReadOnly = false;
            this.ucLEItemVendorItem.ShowCheckBox = false;
            this.ucLEItemVendorItem.Size = new System.Drawing.Size(194, 24);
            this.ucLEItemVendorItem.TabIndex = 7;
            this.ucLEItemVendorItem.TabNext = true;
            this.ucLEItemVendorItem.Value = "";
            this.ucLEItemVendorItem.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemVendorItem.XAlign = 362;
            // 
            // ucLEItemPCBA
            // 
            this.ucLEItemPCBA.AllowEditOnlyChecked = true;
            this.ucLEItemPCBA.Caption = "PCBA版本";
            this.ucLEItemPCBA.Checked = false;
            this.ucLEItemPCBA.EditType = UserControl.EditTypes.String;
            this.ucLEItemPCBA.Location = new System.Drawing.Point(36, 144);
            this.ucLEItemPCBA.MaxLength = 40;
            this.ucLEItemPCBA.Multiline = false;
            this.ucLEItemPCBA.Name = "ucLEItemPCBA";
            this.ucLEItemPCBA.PasswordChar = '\0';
            this.ucLEItemPCBA.ReadOnly = false;
            this.ucLEItemPCBA.ShowCheckBox = false;
            this.ucLEItemPCBA.Size = new System.Drawing.Size(194, 24);
            this.ucLEItemPCBA.TabIndex = 9;
            this.ucLEItemPCBA.TabNext = true;
            this.ucLEItemPCBA.Value = "";
            this.ucLEItemPCBA.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemPCBA.XAlign = 97;
            // 
            // ucLEItemDateCode
            // 
            this.ucLEItemDateCode.AllowEditOnlyChecked = true;
            this.ucLEItemDateCode.Caption = "生产日期";
            this.ucLEItemDateCode.Checked = false;
            this.ucLEItemDateCode.EditType = UserControl.EditTypes.String;
            this.ucLEItemDateCode.Location = new System.Drawing.Point(301, 80);
            this.ucLEItemDateCode.MaxLength = 40;
            this.ucLEItemDateCode.Multiline = false;
            this.ucLEItemDateCode.Name = "ucLEItemDateCode";
            this.ucLEItemDateCode.PasswordChar = '\0';
            this.ucLEItemDateCode.ReadOnly = false;
            this.ucLEItemDateCode.ShowCheckBox = false;
            this.ucLEItemDateCode.Size = new System.Drawing.Size(194, 24);
            this.ucLEItemDateCode.TabIndex = 4;
            this.ucLEItemDateCode.TabNext = true;
            this.ucLEItemDateCode.Value = "";
            this.ucLEItemDateCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemDateCode.XAlign = 362;
            // 
            // ucLEItemDescription
            // 
            this.ucLEItemDescription.AllowEditOnlyChecked = true;
            this.ucLEItemDescription.Caption = "补充说明";
            this.ucLEItemDescription.Checked = false;
            this.ucLEItemDescription.EditType = UserControl.EditTypes.String;
            this.ucLEItemDescription.Location = new System.Drawing.Point(551, 112);
            this.ucLEItemDescription.MaxLength = 40;
            this.ucLEItemDescription.Multiline = false;
            this.ucLEItemDescription.Name = "ucLEItemDescription";
            this.ucLEItemDescription.PasswordChar = '\0';
            this.ucLEItemDescription.ReadOnly = false;
            this.ucLEItemDescription.ShowCheckBox = false;
            this.ucLEItemDescription.Size = new System.Drawing.Size(194, 24);
            this.ucLEItemDescription.TabIndex = 8;
            this.ucLEItemDescription.TabNext = true;
            this.ucLEItemDescription.Value = "";
            this.ucLEItemDescription.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemDescription.XAlign = 612;
            // 
            // ucLEItemVendor
            // 
            this.ucLEItemVendor.AllowEditOnlyChecked = true;
            this.ucLEItemVendor.Caption = "厂商代码";
            this.ucLEItemVendor.Checked = false;
            this.ucLEItemVendor.EditType = UserControl.EditTypes.String;
            this.ucLEItemVendor.Location = new System.Drawing.Point(36, 112);
            this.ucLEItemVendor.MaxLength = 40;
            this.ucLEItemVendor.Multiline = false;
            this.ucLEItemVendor.Name = "ucLEItemVendor";
            this.ucLEItemVendor.PasswordChar = '\0';
            this.ucLEItemVendor.ReadOnly = false;
            this.ucLEItemVendor.ShowCheckBox = false;
            this.ucLEItemVendor.Size = new System.Drawing.Size(194, 24);
            this.ucLEItemVendor.TabIndex = 6;
            this.ucLEItemVendor.TabNext = true;
            this.ucLEItemVendor.Value = "";
            this.ucLEItemVendor.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemVendor.XAlign = 97;
            // 
            // ucLEItemVersion
            // 
            this.ucLEItemVersion.AllowEditOnlyChecked = true;
            this.ucLEItemVersion.Caption = "料品版本";
            this.ucLEItemVersion.Checked = false;
            this.ucLEItemVersion.EditType = UserControl.EditTypes.String;
            this.ucLEItemVersion.Location = new System.Drawing.Point(551, 80);
            this.ucLEItemVersion.MaxLength = 40;
            this.ucLEItemVersion.Multiline = false;
            this.ucLEItemVersion.Name = "ucLEItemVersion";
            this.ucLEItemVersion.PasswordChar = '\0';
            this.ucLEItemVersion.ReadOnly = false;
            this.ucLEItemVersion.ShowCheckBox = false;
            this.ucLEItemVersion.Size = new System.Drawing.Size(194, 24);
            this.ucLEItemVersion.TabIndex = 5;
            this.ucLEItemVersion.TabNext = true;
            this.ucLEItemVersion.Value = "";
            this.ucLEItemVersion.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemVersion.XAlign = 612;
            // 
            // ucLCItemMItem
            // 
            this.ucLCItemMItem.AllowEditOnlyChecked = true;
            this.ucLCItemMItem.Caption = "物料号";
            this.ucLCItemMItem.Checked = false;
            this.ucLCItemMItem.Location = new System.Drawing.Point(48, 8);
            this.ucLCItemMItem.Name = "ucLCItemMItem";
            this.ucLCItemMItem.SelectedIndex = -1;
            this.ucLCItemMItem.ShowCheckBox = false;
            this.ucLCItemMItem.Size = new System.Drawing.Size(182, 24);
            this.ucLCItemMItem.TabIndex = 0;
            this.ucLCItemMItem.WidthType = UserControl.WidthTypes.Normal;
            this.ucLCItemMItem.XAlign = 97;
            // 
            // txtItemSequence
            // 
            this.txtItemSequence.Location = new System.Drawing.Point(617, 144);
            this.txtItemSequence.Name = "txtItemSequence";
            this.txtItemSequence.Size = new System.Drawing.Size(128, 21);
            this.txtItemSequence.TabIndex = 330;
            this.txtItemSequence.Visible = false;
            // 
            // ultraTabPageControl6
            // 
            this.ultraTabPageControl6.Controls.Add(this.ucLabelEdit1);
            this.ultraTabPageControl6.Controls.Add(this.groupBox3);
            this.ultraTabPageControl6.Controls.Add(this.ucButton2);
            this.ultraTabPageControl6.Controls.Add(this.ucLabEdit3);
            this.ultraTabPageControl6.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl6.Name = "ultraTabPageControl6";
            this.ultraTabPageControl6.Size = new System.Drawing.Size(856, 529);
            // 
            // ucLabelEdit1
            // 
            this.ucLabelEdit1.AllowEditOnlyChecked = true;
            this.ucLabelEdit1.Caption = "代录维修人员";
            this.ucLabelEdit1.Checked = false;
            this.ucLabelEdit1.EditType = UserControl.EditTypes.String;
            this.ucLabelEdit1.Location = new System.Drawing.Point(10, 144);
            this.ucLabelEdit1.MaxLength = 20;
            this.ucLabelEdit1.Multiline = false;
            this.ucLabelEdit1.Name = "ucLabelEdit1";
            this.ucLabelEdit1.PasswordChar = '\0';
            this.ucLabelEdit1.ReadOnly = false;
            this.ucLabelEdit1.ShowCheckBox = true;
            this.ucLabelEdit1.Size = new System.Drawing.Size(234, 24);
            this.ucLabelEdit1.TabIndex = 195;
            this.ucLabelEdit1.TabNext = false;
            this.ucLabelEdit1.Value = "";
            this.ucLabelEdit1.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEdit1.XAlign = 111;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbRoute);
            this.groupBox3.Controls.Add(this.ucLabCombox2);
            this.groupBox3.Controls.Add(this.ultraCheckEditor1);
            this.groupBox3.Controls.Add(this.ucLabEdit2);
            this.groupBox3.Controls.Add(this.ucLabEdit1);
            this.groupBox3.Location = new System.Drawing.Point(8, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(416, 120);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // cmbRoute
            // 
            this.cmbRoute.AllowEditOnlyChecked = true;
            this.cmbRoute.Caption = "途程";
            this.cmbRoute.Checked = false;
            this.cmbRoute.Location = new System.Drawing.Point(16, 40);
            this.cmbRoute.Name = "cmbRoute";
            this.cmbRoute.SelectedIndex = -1;
            this.cmbRoute.ShowCheckBox = true;
            this.cmbRoute.Size = new System.Drawing.Size(186, 24);
            this.cmbRoute.TabIndex = 58;
            this.cmbRoute.WidthType = UserControl.WidthTypes.Normal;
            this.cmbRoute.XAlign = 69;
            this.cmbRoute.CheckBoxCheckedChanged += new System.EventHandler(this.cmbRoute_CheckBoxCheckedChanged);
            // 
            // ucLabCombox2
            // 
            this.ucLabCombox2.AllowEditOnlyChecked = true;
            this.ucLabCombox2.Caption = "工位";
            this.ucLabCombox2.Checked = false;
            this.ucLabCombox2.Location = new System.Drawing.Point(216, 40);
            this.ucLabCombox2.Name = "ucLabCombox2";
            this.ucLabCombox2.SelectedIndex = -1;
            this.ucLabCombox2.ShowCheckBox = false;
            this.ucLabCombox2.Size = new System.Drawing.Size(170, 24);
            this.ucLabCombox2.TabIndex = 57;
            this.ucLabCombox2.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabCombox2.XAlign = 253;
            // 
            // ultraCheckEditor1
            // 
            this.ultraCheckEditor1.Location = new System.Drawing.Point(8, 0);
            this.ultraCheckEditor1.Name = "ultraCheckEditor1";
            this.ultraCheckEditor1.Size = new System.Drawing.Size(48, 16);
            this.ultraCheckEditor1.TabIndex = 7;
            this.ultraCheckEditor1.Text = "回流";
            this.ultraCheckEditor1.CheckedValueChanged += new System.EventHandler(this.ultraCheckEditor1_CheckedValueChanged);
            // 
            // ucLabEdit2
            // 
            this.ucLabEdit2.AllowEditOnlyChecked = true;
            this.ucLabEdit2.Caption = "产品代码";
            this.ucLabEdit2.Checked = false;
            this.ucLabEdit2.EditType = UserControl.EditTypes.String;
            this.ucLabEdit2.Location = new System.Drawing.Point(9, 72);
            this.ucLabEdit2.MaxLength = 40;
            this.ucLabEdit2.Multiline = false;
            this.ucLabEdit2.Name = "ucLabEdit2";
            this.ucLabEdit2.PasswordChar = '\0';
            this.ucLabEdit2.ReadOnly = true;
            this.ucLabEdit2.ShowCheckBox = false;
            this.ucLabEdit2.Size = new System.Drawing.Size(194, 24);
            this.ucLabEdit2.TabIndex = 54;
            this.ucLabEdit2.TabNext = true;
            this.ucLabEdit2.Value = "";
            this.ucLabEdit2.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEdit2.XAlign = 70;
            // 
            // ucLabEdit1
            // 
            this.ucLabEdit1.AllowEditOnlyChecked = true;
            this.ucLabEdit1.Caption = "工单";
            this.ucLabEdit1.Checked = false;
            this.ucLabEdit1.EditType = UserControl.EditTypes.String;
            this.ucLabEdit1.Location = new System.Drawing.Point(216, 72);
            this.ucLabEdit1.MaxLength = 40;
            this.ucLabEdit1.Multiline = false;
            this.ucLabEdit1.Name = "ucLabEdit1";
            this.ucLabEdit1.PasswordChar = '\0';
            this.ucLabEdit1.ReadOnly = true;
            this.ucLabEdit1.ShowCheckBox = false;
            this.ucLabEdit1.Size = new System.Drawing.Size(170, 24);
            this.ucLabEdit1.TabIndex = 53;
            this.ucLabEdit1.TabNext = true;
            this.ucLabEdit1.Value = "";
            this.ucLabEdit1.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEdit1.XAlign = 253;
            // 
            // ucButton2
            // 
            this.ucButton2.BackColor = System.Drawing.SystemColors.Control;
            this.ucButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton2.BackgroundImage")));
            this.ucButton2.ButtonType = UserControl.ButtonTypes.Confirm;
            this.ucButton2.Caption = "确认";
            this.ucButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButton2.Location = new System.Drawing.Point(336, 144);
            this.ucButton2.Name = "ucButton2";
            this.ucButton2.Size = new System.Drawing.Size(88, 22);
            this.ucButton2.TabIndex = 9;
            this.ucButton2.Click += new System.EventHandler(this.ucButton2_Click);
            // 
            // ucLabEdit3
            // 
            this.ucLabEdit3.AllowEditOnlyChecked = true;
            this.ucLabEdit3.Caption = "途程";
            this.ucLabEdit3.Checked = false;
            this.ucLabEdit3.EditType = UserControl.EditTypes.String;
            this.ucLabEdit3.Location = new System.Drawing.Point(8, 232);
            this.ucLabEdit3.MaxLength = 40;
            this.ucLabEdit3.Multiline = false;
            this.ucLabEdit3.Name = "ucLabEdit3";
            this.ucLabEdit3.PasswordChar = '\0';
            this.ucLabEdit3.ReadOnly = true;
            this.ucLabEdit3.ShowCheckBox = false;
            this.ucLabEdit3.Size = new System.Drawing.Size(170, 24);
            this.ucLabEdit3.TabIndex = 55;
            this.ucLabEdit3.TabNext = true;
            this.ucLabEdit3.Value = "";
            this.ucLabEdit3.Visible = false;
            this.ucLabEdit3.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEdit3.XAlign = 45;
            // 
            // ultraTabPageControl2
            // 
            this.ultraTabPageControl2.Controls.Add(this.ultraTabTSInput);
            this.ultraTabPageControl2.Controls.Add(this.groupBox2);
            this.ultraTabPageControl2.Location = new System.Drawing.Point(1, 23);
            this.ultraTabPageControl2.Name = "ultraTabPageControl2";
            this.ultraTabPageControl2.Size = new System.Drawing.Size(860, 603);
            // 
            // ultraTabTSInput
            // 
            this.ultraTabTSInput.Controls.Add(this.ultraTabSharedControlsPage2);
            this.ultraTabTSInput.Controls.Add(this.ultraTabPageControl4);
            this.ultraTabTSInput.Controls.Add(this.ultraTabPageControl5);
            this.ultraTabTSInput.Controls.Add(this.ultraTabPageControl6);
            this.ultraTabTSInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraTabTSInput.Location = new System.Drawing.Point(0, 48);
            this.ultraTabTSInput.Name = "ultraTabTSInput";
            this.ultraTabTSInput.SharedControlsPage = this.ultraTabSharedControlsPage2;
            this.ultraTabTSInput.Size = new System.Drawing.Size(860, 555);
            this.ultraTabTSInput.TabIndex = 1;
            ultraTab7.TabPage = this.ultraTabPageControl4;
            ultraTab7.Text = "维修信息";
            ultraTab8.TabPage = this.ultraTabPageControl5;
            ultraTab8.Text = "物料信息";
            ultraTab9.TabPage = this.ultraTabPageControl6;
            ultraTab9.Text = "维修完成";
            this.ultraTabTSInput.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab7,
            ultraTab8,
            ultraTab9});
            // 
            // ultraTabSharedControlsPage2
            // 
            this.ultraTabSharedControlsPage2.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
            this.ultraTabSharedControlsPage2.Size = new System.Drawing.Size(856, 529);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbCopyTSInfo);
            this.groupBox2.Controls.Add(this.txtAgentUser);
            this.groupBox2.Controls.Add(this.ucBtnViewTest);
            this.groupBox2.Controls.Add(this.ucLEMemo);
            this.groupBox2.Controls.Add(this.ucBtnTSErrorEdit);
            this.groupBox2.Controls.Add(this.ucLEMNID);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(860, 48);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // cbCopyTSInfo
            // 
            this.cbCopyTSInfo.Location = new System.Drawing.Point(400, 16);
            this.cbCopyTSInfo.Name = "cbCopyTSInfo";
            this.cbCopyTSInfo.Size = new System.Drawing.Size(104, 24);
            this.cbCopyTSInfo.TabIndex = 5;
            this.cbCopyTSInfo.Text = "复制维修信息";
            // 
            // txtAgentUser
            // 
            this.txtAgentUser.AllowEditOnlyChecked = true;
            this.txtAgentUser.Caption = "代录维修人员";
            this.txtAgentUser.Checked = false;
            this.txtAgentUser.EditType = UserControl.EditTypes.String;
            this.txtAgentUser.Location = new System.Drawing.Point(506, 16);
            this.txtAgentUser.MaxLength = 20;
            this.txtAgentUser.Multiline = false;
            this.txtAgentUser.Name = "txtAgentUser";
            this.txtAgentUser.PasswordChar = '\0';
            this.txtAgentUser.ReadOnly = false;
            this.txtAgentUser.ShowCheckBox = true;
            this.txtAgentUser.Size = new System.Drawing.Size(201, 24);
            this.txtAgentUser.TabIndex = 4;
            this.txtAgentUser.TabNext = false;
            this.txtAgentUser.Value = "";
            this.txtAgentUser.WidthType = UserControl.WidthTypes.Small;
            this.txtAgentUser.XAlign = 607;
            // 
            // ucBtnViewTest
            // 
            this.ucBtnViewTest.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnViewTest.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnViewTest.BackgroundImage")));
            this.ucBtnViewTest.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnViewTest.Caption = "查看测试文件";
            this.ucBtnViewTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnViewTest.Location = new System.Drawing.Point(592, 0);
            this.ucBtnViewTest.Name = "ucBtnViewTest";
            this.ucBtnViewTest.Size = new System.Drawing.Size(88, 22);
            this.ucBtnViewTest.TabIndex = 3;
            this.ucBtnViewTest.Visible = false;
            // 
            // ucLEMemo
            // 
            this.ucLEMemo.AllowEditOnlyChecked = true;
            this.ucLEMemo.Caption = "备注";
            this.ucLEMemo.Checked = false;
            this.ucLEMemo.EditType = UserControl.EditTypes.String;
            this.ucLEMemo.Location = new System.Drawing.Point(224, 16);
            this.ucLEMemo.MaxLength = 40;
            this.ucLEMemo.Multiline = false;
            this.ucLEMemo.Name = "ucLEMemo";
            this.ucLEMemo.PasswordChar = '\0';
            this.ucLEMemo.ReadOnly = true;
            this.ucLEMemo.ShowCheckBox = false;
            this.ucLEMemo.Size = new System.Drawing.Size(170, 24);
            this.ucLEMemo.TabIndex = 3;
            this.ucLEMemo.TabNext = false;
            this.ucLEMemo.Value = "";
            this.ucLEMemo.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEMemo.XAlign = 261;
            // 
            // ucBtnTSErrorEdit
            // 
            this.ucBtnTSErrorEdit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnTSErrorEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnTSErrorEdit.BackgroundImage")));
            this.ucBtnTSErrorEdit.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnTSErrorEdit.Caption = "维护不良信息";
            this.ucBtnTSErrorEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnTSErrorEdit.Location = new System.Drawing.Point(712, 16);
            this.ucBtnTSErrorEdit.Name = "ucBtnTSErrorEdit";
            this.ucBtnTSErrorEdit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnTSErrorEdit.TabIndex = 2;
            this.ucBtnTSErrorEdit.Click += new System.EventHandler(this.ucBtnTSErrorEdit_Click);
            // 
            // ucLEMNID
            // 
            this.ucLEMNID.AllowEditOnlyChecked = true;
            this.ucLEMNID.Caption = "产品序列号";
            this.ucLEMNID.Checked = false;
            this.ucLEMNID.EditType = UserControl.EditTypes.String;
            this.ucLEMNID.Location = new System.Drawing.Point(9, 16);
            this.ucLEMNID.MaxLength = 40;
            this.ucLEMNID.Multiline = false;
            this.ucLEMNID.Name = "ucLEMNID";
            this.ucLEMNID.PasswordChar = '\0';
            this.ucLEMNID.ReadOnly = false;
            this.ucLEMNID.ShowCheckBox = false;
            this.ucLEMNID.Size = new System.Drawing.Size(206, 24);
            this.ucLEMNID.TabIndex = 1;
            this.ucLEMNID.TabNext = false;
            this.ucLEMNID.Value = "";
            this.ucLEMNID.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEMNID.XAlign = 82;
            this.ucLEMNID.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEMNID_TxtboxKeyPress);
            // 
            // ultraTabPageControl3
            // 
            this.ultraTabPageControl3.Controls.Add(this.ultraGridSplitItem);
            this.ultraTabPageControl3.Controls.Add(this.panelSplitButton);
            this.ultraTabPageControl3.Controls.Add(this.grpSplitQuery);
            this.ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl3.Name = "ultraTabPageControl3";
            this.ultraTabPageControl3.Size = new System.Drawing.Size(860, 603);
            // 
            // ultraGridSplitItem
            // 
            this.ultraGridSplitItem.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridSplitItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridSplitItem.Location = new System.Drawing.Point(0, 48);
            this.ultraGridSplitItem.Name = "ultraGridSplitItem";
            this.ultraGridSplitItem.Size = new System.Drawing.Size(860, 499);
            this.ultraGridSplitItem.TabIndex = 293;
            this.ultraGridSplitItem.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridSplitItem_InitializeLayout);
            this.ultraGridSplitItem.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridSplitItem_CellChange);
            // 
            // panelSplitButton
            // 
            this.panelSplitButton.Controls.Add(this.ucBtnSplitExit);
            this.panelSplitButton.Controls.Add(this.ucBtnSplit);
            this.panelSplitButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSplitButton.Location = new System.Drawing.Point(0, 547);
            this.panelSplitButton.Name = "panelSplitButton";
            this.panelSplitButton.Size = new System.Drawing.Size(860, 56);
            this.panelSplitButton.TabIndex = 292;
            // 
            // ucBtnSplitExit
            // 
            this.ucBtnSplitExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnSplitExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnSplitExit.BackgroundImage")));
            this.ucBtnSplitExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnSplitExit.Caption = "退出";
            this.ucBtnSplitExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnSplitExit.Location = new System.Drawing.Point(422, 16);
            this.ucBtnSplitExit.Name = "ucBtnSplitExit";
            this.ucBtnSplitExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnSplitExit.TabIndex = 4;
            // 
            // ucBtnSplit
            // 
            this.ucBtnSplit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnSplit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnSplit.BackgroundImage")));
            this.ucBtnSplit.ButtonType = UserControl.ButtonTypes.Save;
            this.ucBtnSplit.Caption = "保存";
            this.ucBtnSplit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnSplit.Location = new System.Drawing.Point(278, 16);
            this.ucBtnSplit.Name = "ucBtnSplit";
            this.ucBtnSplit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnSplit.TabIndex = 3;
            this.ucBtnSplit.Click += new System.EventHandler(this.ucBtnSplit_Click);
            // 
            // grpSplitQuery
            // 
            this.grpSplitQuery.Controls.Add(this.txtAgentSpliter);
            this.grpSplitQuery.Controls.Add(this.ucLCSplitTSStatus);
            this.grpSplitQuery.Controls.Add(this.ucLERC);
            this.grpSplitQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSplitQuery.Location = new System.Drawing.Point(0, 0);
            this.grpSplitQuery.Name = "grpSplitQuery";
            this.grpSplitQuery.Size = new System.Drawing.Size(860, 48);
            this.grpSplitQuery.TabIndex = 288;
            this.grpSplitQuery.TabStop = false;
            // 
            // txtAgentSpliter
            // 
            this.txtAgentSpliter.AllowEditOnlyChecked = true;
            this.txtAgentSpliter.Caption = "代录维修人员";
            this.txtAgentSpliter.Checked = false;
            this.txtAgentSpliter.EditType = UserControl.EditTypes.String;
            this.txtAgentSpliter.Location = new System.Drawing.Point(478, 16);
            this.txtAgentSpliter.MaxLength = 20;
            this.txtAgentSpliter.Multiline = false;
            this.txtAgentSpliter.Name = "txtAgentSpliter";
            this.txtAgentSpliter.PasswordChar = '\0';
            this.txtAgentSpliter.ReadOnly = false;
            this.txtAgentSpliter.ShowCheckBox = true;
            this.txtAgentSpliter.Size = new System.Drawing.Size(201, 24);
            this.txtAgentSpliter.TabIndex = 5;
            this.txtAgentSpliter.TabNext = false;
            this.txtAgentSpliter.Value = "";
            this.txtAgentSpliter.WidthType = UserControl.WidthTypes.Small;
            this.txtAgentSpliter.XAlign = 579;
            // 
            // ucLCSplitTSStatus
            // 
            this.ucLCSplitTSStatus.AllowEditOnlyChecked = true;
            this.ucLCSplitTSStatus.Caption = "维修状态";
            this.ucLCSplitTSStatus.Checked = false;
            this.ucLCSplitTSStatus.Enabled = false;
            this.ucLCSplitTSStatus.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ucLCSplitTSStatus.Location = new System.Drawing.Point(241, 16);
            this.ucLCSplitTSStatus.Name = "ucLCSplitTSStatus";
            this.ucLCSplitTSStatus.SelectedIndex = -1;
            this.ucLCSplitTSStatus.ShowCheckBox = false;
            this.ucLCSplitTSStatus.Size = new System.Drawing.Size(194, 24);
            this.ucLCSplitTSStatus.TabIndex = 1;
            this.ucLCSplitTSStatus.WidthType = UserControl.WidthTypes.Normal;
            this.ucLCSplitTSStatus.XAlign = 302;
            // 
            // ucLERC
            // 
            this.ucLERC.AllowEditOnlyChecked = true;
            this.ucLERC.Caption = "产品序列号";
            this.ucLERC.Checked = false;
            this.ucLERC.EditType = UserControl.EditTypes.String;
            this.ucLERC.Location = new System.Drawing.Point(24, 16);
            this.ucLERC.MaxLength = 40;
            this.ucLERC.Multiline = false;
            this.ucLERC.Name = "ucLERC";
            this.ucLERC.PasswordChar = '\0';
            this.ucLERC.ReadOnly = false;
            this.ucLERC.ShowCheckBox = false;
            this.ucLERC.Size = new System.Drawing.Size(206, 24);
            this.ucLERC.TabIndex = 0;
            this.ucLERC.TabNext = true;
            this.ucLERC.Value = "";
            this.ucLERC.WidthType = UserControl.WidthTypes.Normal;
            this.ucLERC.XAlign = 97;
            this.ucLERC.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLERC_TxtboxKeyPress);
            // 
            // ultraTabPageControl1
            // 
            this.ultraTabPageControl1.Controls.Add(this.ultraGridTSQuery);
            this.ultraTabPageControl1.Controls.Add(this.pnlBtn);
            this.ultraTabPageControl1.Controls.Add(this.groupBox1);
            this.ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            this.ultraTabPageControl1.Size = new System.Drawing.Size(860, 603);
            // 
            // ultraGridTSQuery
            // 
            this.ultraGridTSQuery.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridTSQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridTSQuery.Location = new System.Drawing.Point(0, 128);
            this.ultraGridTSQuery.Name = "ultraGridTSQuery";
            this.ultraGridTSQuery.Size = new System.Drawing.Size(860, 435);
            this.ultraGridTSQuery.TabIndex = 1;
            // 
            // pnlBtn
            // 
            this.pnlBtn.Controls.Add(this.ucBtnQueryExit);
            this.pnlBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBtn.Location = new System.Drawing.Point(0, 563);
            this.pnlBtn.Name = "pnlBtn";
            this.pnlBtn.Size = new System.Drawing.Size(860, 40);
            this.pnlBtn.TabIndex = 2;
            // 
            // ucBtnQueryExit
            // 
            this.ucBtnQueryExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnQueryExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnQueryExit.BackgroundImage")));
            this.ucBtnQueryExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnQueryExit.Caption = "退出";
            this.ucBtnQueryExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnQueryExit.Location = new System.Drawing.Point(350, 8);
            this.ucBtnQueryExit.Name = "ucBtnQueryExit";
            this.ucBtnQueryExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnQueryExit.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucDateTimeEnd);
            this.groupBox1.Controls.Add(this.ucDateTimeStart);
            this.groupBox1.Controls.Add(this.ucLEResource);
            this.groupBox1.Controls.Add(this.ucLEStepsequence);
            this.groupBox1.Controls.Add(this.ucLEItemCode);
            this.groupBox1.Controls.Add(this.ucLEOperation);
            this.groupBox1.Controls.Add(this.ucLCTSStatus);
            this.groupBox1.Controls.Add(this.ucLERunningCard);
            this.groupBox1.Controls.Add(this.ucLEMOCode);
            this.groupBox1.Controls.Add(this.ucButtonQuery);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(860, 128);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // ucDateTimeEnd
            // 
            this.ucDateTimeEnd.Caption = "结束时间";
            this.ucDateTimeEnd.Location = new System.Drawing.Point(273, 72);
            this.ucDateTimeEnd.Name = "ucDateTimeEnd";
            this.ucDateTimeEnd.ShowType = UserControl.DateTimeTypes.DateTime;
            this.ucDateTimeEnd.Size = new System.Drawing.Size(222, 21);
            this.ucDateTimeEnd.TabIndex = 7;
            this.ucDateTimeEnd.Value = new System.DateTime(2005, 7, 26, 15, 13, 23, 0);
            this.ucDateTimeEnd.XAlign = 334;
            // 
            // ucDateTimeStart
            // 
            this.ucDateTimeStart.Caption = "开始时间";
            this.ucDateTimeStart.Location = new System.Drawing.Point(35, 72);
            this.ucDateTimeStart.Name = "ucDateTimeStart";
            this.ucDateTimeStart.ShowType = UserControl.DateTimeTypes.DateTime;
            this.ucDateTimeStart.Size = new System.Drawing.Size(222, 21);
            this.ucDateTimeStart.TabIndex = 6;
            this.ucDateTimeStart.Value = new System.DateTime(2005, 7, 26, 15, 13, 23, 0);
            this.ucDateTimeStart.XAlign = 96;
            // 
            // ucLEResource
            // 
            this.ucLEResource.AllowEditOnlyChecked = true;
            this.ucLEResource.Caption = "资源";
            this.ucLEResource.Checked = false;
            this.ucLEResource.EditType = UserControl.EditTypes.String;
            this.ucLEResource.Location = new System.Drawing.Point(536, 44);
            this.ucLEResource.MaxLength = 40;
            this.ucLEResource.Multiline = false;
            this.ucLEResource.Name = "ucLEResource";
            this.ucLEResource.PasswordChar = '\0';
            this.ucLEResource.ReadOnly = false;
            this.ucLEResource.ShowCheckBox = false;
            this.ucLEResource.Size = new System.Drawing.Size(170, 24);
            this.ucLEResource.TabIndex = 5;
            this.ucLEResource.TabNext = true;
            this.ucLEResource.Value = "";
            this.ucLEResource.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEResource.XAlign = 573;
            // 
            // ucLEStepsequence
            // 
            this.ucLEStepsequence.AllowEditOnlyChecked = true;
            this.ucLEStepsequence.Caption = "产线";
            this.ucLEStepsequence.Checked = false;
            this.ucLEStepsequence.EditType = UserControl.EditTypes.String;
            this.ucLEStepsequence.Location = new System.Drawing.Point(296, 44);
            this.ucLEStepsequence.MaxLength = 40;
            this.ucLEStepsequence.Multiline = false;
            this.ucLEStepsequence.Name = "ucLEStepsequence";
            this.ucLEStepsequence.PasswordChar = '\0';
            this.ucLEStepsequence.ReadOnly = false;
            this.ucLEStepsequence.ShowCheckBox = false;
            this.ucLEStepsequence.Size = new System.Drawing.Size(170, 24);
            this.ucLEStepsequence.TabIndex = 4;
            this.ucLEStepsequence.TabNext = true;
            this.ucLEStepsequence.Value = "";
            this.ucLEStepsequence.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEStepsequence.XAlign = 333;
            // 
            // ucLEItemCode
            // 
            this.ucLEItemCode.AllowEditOnlyChecked = true;
            this.ucLEItemCode.Caption = "产品代码";
            this.ucLEItemCode.Checked = false;
            this.ucLEItemCode.EditType = UserControl.EditTypes.String;
            this.ucLEItemCode.Location = new System.Drawing.Point(272, 16);
            this.ucLEItemCode.MaxLength = 40;
            this.ucLEItemCode.Multiline = false;
            this.ucLEItemCode.Name = "ucLEItemCode";
            this.ucLEItemCode.PasswordChar = '\0';
            this.ucLEItemCode.ReadOnly = false;
            this.ucLEItemCode.ShowCheckBox = false;
            this.ucLEItemCode.Size = new System.Drawing.Size(194, 24);
            this.ucLEItemCode.TabIndex = 1;
            this.ucLEItemCode.TabNext = true;
            this.ucLEItemCode.Value = "";
            this.ucLEItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemCode.XAlign = 333;
            // 
            // ucLEOperation
            // 
            this.ucLEOperation.AllowEditOnlyChecked = true;
            this.ucLEOperation.Caption = "工序";
            this.ucLEOperation.Checked = false;
            this.ucLEOperation.EditType = UserControl.EditTypes.String;
            this.ucLEOperation.Location = new System.Drawing.Point(60, 44);
            this.ucLEOperation.MaxLength = 40;
            this.ucLEOperation.Multiline = false;
            this.ucLEOperation.Name = "ucLEOperation";
            this.ucLEOperation.PasswordChar = '\0';
            this.ucLEOperation.ReadOnly = false;
            this.ucLEOperation.ShowCheckBox = false;
            this.ucLEOperation.Size = new System.Drawing.Size(170, 24);
            this.ucLEOperation.TabIndex = 3;
            this.ucLEOperation.TabNext = true;
            this.ucLEOperation.Value = "";
            this.ucLEOperation.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEOperation.XAlign = 97;
            // 
            // ucLCTSStatus
            // 
            this.ucLCTSStatus.AllowEditOnlyChecked = true;
            this.ucLCTSStatus.Caption = "维修状态";
            this.ucLCTSStatus.Checked = false;
            this.ucLCTSStatus.Location = new System.Drawing.Point(36, 100);
            this.ucLCTSStatus.Name = "ucLCTSStatus";
            this.ucLCTSStatus.SelectedIndex = -1;
            this.ucLCTSStatus.ShowCheckBox = false;
            this.ucLCTSStatus.Size = new System.Drawing.Size(194, 24);
            this.ucLCTSStatus.TabIndex = 8;
            this.ucLCTSStatus.WidthType = UserControl.WidthTypes.Normal;
            this.ucLCTSStatus.XAlign = 97;
            // 
            // ucLERunningCard
            // 
            this.ucLERunningCard.AllowEditOnlyChecked = true;
            this.ucLERunningCard.Caption = "产品序列号";
            this.ucLERunningCard.Checked = false;
            this.ucLERunningCard.EditType = UserControl.EditTypes.String;
            this.ucLERunningCard.Location = new System.Drawing.Point(24, 16);
            this.ucLERunningCard.MaxLength = 40;
            this.ucLERunningCard.Multiline = false;
            this.ucLERunningCard.Name = "ucLERunningCard";
            this.ucLERunningCard.PasswordChar = '\0';
            this.ucLERunningCard.ReadOnly = false;
            this.ucLERunningCard.ShowCheckBox = false;
            this.ucLERunningCard.Size = new System.Drawing.Size(206, 24);
            this.ucLERunningCard.TabIndex = 0;
            this.ucLERunningCard.TabNext = true;
            this.ucLERunningCard.Value = "";
            this.ucLERunningCard.WidthType = UserControl.WidthTypes.Normal;
            this.ucLERunningCard.XAlign = 97;
            // 
            // ucLEMOCode
            // 
            this.ucLEMOCode.AllowEditOnlyChecked = true;
            this.ucLEMOCode.Caption = "工单";
            this.ucLEMOCode.Checked = false;
            this.ucLEMOCode.EditType = UserControl.EditTypes.String;
            this.ucLEMOCode.Location = new System.Drawing.Point(536, 16);
            this.ucLEMOCode.MaxLength = 40;
            this.ucLEMOCode.Multiline = false;
            this.ucLEMOCode.Name = "ucLEMOCode";
            this.ucLEMOCode.PasswordChar = '\0';
            this.ucLEMOCode.ReadOnly = false;
            this.ucLEMOCode.ShowCheckBox = false;
            this.ucLEMOCode.Size = new System.Drawing.Size(170, 24);
            this.ucLEMOCode.TabIndex = 2;
            this.ucLEMOCode.TabNext = true;
            this.ucLEMOCode.Value = "";
            this.ucLEMOCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEMOCode.XAlign = 573;
            // 
            // ucButtonQuery
            // 
            this.ucButtonQuery.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonQuery.BackgroundImage")));
            this.ucButtonQuery.ButtonType = UserControl.ButtonTypes.Query;
            this.ucButtonQuery.Caption = "查询";
            this.ucButtonQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonQuery.Location = new System.Drawing.Point(616, 96);
            this.ucButtonQuery.Name = "ucButtonQuery";
            this.ucButtonQuery.Size = new System.Drawing.Size(88, 22);
            this.ucButtonQuery.TabIndex = 9;
            this.ucButtonQuery.Click += new System.EventHandler(this.ucButtonQuery_Click);
            // 
            // ultraTabCtrlTS
            // 
            this.ultraTabCtrlTS.Controls.Add(this.ultraTabSharedControlsPage1);
            this.ultraTabCtrlTS.Controls.Add(this.ultraTabPageControl1);
            this.ultraTabCtrlTS.Controls.Add(this.ultraTabPageControl2);
            this.ultraTabCtrlTS.Controls.Add(this.ultraTabPageControl3);
            this.ultraTabCtrlTS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraTabCtrlTS.Location = new System.Drawing.Point(0, 0);
            this.ultraTabCtrlTS.Name = "ultraTabCtrlTS";
            this.ultraTabCtrlTS.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.ultraTabCtrlTS.Size = new System.Drawing.Size(864, 629);
            this.ultraTabCtrlTS.TabIndex = 0;
            ultraTab1.TabPage = this.ultraTabPageControl2;
            ultraTab1.Text = "维修";
            ultraTab2.TabPage = this.ultraTabPageControl3;
            ultraTab2.Text = "拆解";
            ultraTab3.TabPage = this.ultraTabPageControl1;
            ultraTab3.Text = "不良查询";
            this.ultraTabCtrlTS.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1,
            ultraTab2,
            ultraTab3});
            this.ultraTabCtrlTS.TabStop = false;
            this.ultraTabCtrlTS.Click += new System.EventHandler(this.ultraTabCtrlTS_Click);
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(860, 603);
            // 
            // FRMATSInputEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(864, 629);
            this.Controls.Add(this.ultraTabCtrlTS);
            this.Name = "FRMATSInputEdit";
            this.Text = "维修处理";
            this.Load += new System.EventHandler(this.FTSInputEdit_Load);
            this.Closed += new System.EventHandler(this.FTSInputEdit_Closed);
            this.Activated += new System.EventHandler(this.FTSInputEdit_Activated);
            this.ultraTabPageControl4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panelButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraTreeRunningCard)).EndInit();
            this.ultraTabPageControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridRepairItem)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panelKeparts.ResumeLayout(false);
            this.ultraTabPageControl6.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ultraTabPageControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabTSInput)).EndInit();
            this.ultraTabTSInput.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ultraTabPageControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridSplitItem)).EndInit();
            this.panelSplitButton.ResumeLayout(false);
            this.grpSplitQuery.ResumeLayout(false);
            this.ultraTabPageControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridTSQuery)).EndInit();
            this.pnlBtn.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabCtrlTS)).EndInit();
            this.ultraTabCtrlTS.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region 初始化

        private void InitializeForm()
        {
            this.InitializePageTSInformation();
            this.InitializeUltraGridRepairItem();
            this.InitializeUltraGridSplitItem();

            this.panelKeparts.Visible = true;

            this.TSEditStatus = ErrorInfoEditStatus.DoNothing;
            this.TSItemEditStatus = FormStatus.NoReady;

            //Laws Lu,2005/08/23,新增	
            DateTime dt = new DateTime(DateTime.Now.Year
                , DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
            ucDateTimeStart.Value = dt;
            //End Laws Lu
        }

        private void InitializePageTSInformation()
        {
            this.InitializeTSInformationQueryPanel();
            this.InitializeTSInformationStatus();
            this.InitializeTSInformationUltraGrid();
        }

        private void InitializeTSInformationQueryPanel()
        {
            this.ucDateTimeStart.Value = DateTime.Now;
            this.ucDateTimeEnd.Value = DateTime.Now;
        }

        private void FTSInputEdit_Load(object sender, System.EventArgs e)
        {
            InitializeForm();

            this.ucLERunningCard.TextFocus(false, true);
        }

        private void FTSInputEdit_Closed(object sender, System.EventArgs e)
        {
            if (this.DataProvider != null)
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
            }
        }

        #endregion

        #region 不良查询
        private void InitializeTSInformationStatus()
        {
            //init TSStatus
            this.ucLCTSStatus.Clear();
            this.ucLCSplitTSStatus.Clear();

            TSStatus tsStatus = new TSStatus();

            this.ucLCTSStatus.AddItem(MutiLanguages.ParserString("listItemAll"), "");
            this.ucLCSplitTSStatus.AddItem(MutiLanguages.ParserString(""), "");

            foreach (string status in tsStatus.Items)
            {
                this.ucLCTSStatus.AddItem(MutiLanguages.ParserString(status), status);
                this.ucLCSplitTSStatus.AddItem(MutiLanguages.ParserString(status), status);
            }

            this.ucLCTSStatus.SelectedIndex = 0;
            this.ucLCSplitTSStatus.SelectedIndex = 0;
        }

        private void ucButtonQuery_Click(object sender, System.EventArgs e)
        {
            try
            {
                UserControl.Messages messages = new UserControl.Messages();
                //检查开始时间必须小于结束时间
                this.checkValidDateTimeInput(this.ucDateTimeStart.Value, this.ucDateTimeEnd.Value);

                //Laws Lu,2005/08/25,新增	检查当前资源是否为TS站
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this._domainDataProvider);
                ActionEventArgs actionEventArgs = new ActionEventArgs(
                    ActionType.DataCollectAction_TSConfirm,
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ucLERunningCard.Value)),
                    ApplicationService.Current().UserCode,
                    ApplicationService.Current().ResourceCode);

                messages.AddMessages(dataCollect.CheckResourceInOperationTS(actionEventArgs));
                //End Laws LU

                if (messages.IsSuccess())
                {
                    this.fillUltraTSInformation(this._tsFacade.IllegibilityQueryTS(
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLERunningCard.Value)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLEItemCode.Value)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLEMOCode.Value)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLEOperation.Value)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLEStepsequence.Value)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLEResource.Value)),
                        FormatHelper.TODateInt(this.ucDateTimeStart.Value),
                        FormatHelper.TOTimeInt(this.ucDateTimeStart.Value),
                        FormatHelper.TODateInt(this.ucDateTimeEnd.Value),
                        FormatHelper.TOTimeInt(this.ucDateTimeEnd.Value),
                        this.ucLCTSStatus.SelectedItemValue.ToString()));
                }
                else
                {
                    ApplicationRun.GetInfoForm().Add(messages);
                }

            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }
        }

        private void InitializeTSInformationUltraGrid()
        {
            dtTSInformation.Columns.Clear();
            //for 多语言
            dtTSInformation.Columns.Add(new DataColumn("序列号", typeof(string)));
            dtTSInformation.Columns.Add(new DataColumn("产品别", typeof(string)));
            dtTSInformation.Columns.Add(new DataColumn("产品代码", typeof(string)));
            dtTSInformation.Columns.Add(new DataColumn("产品名称", typeof(string)));
            dtTSInformation.Columns.Add(new DataColumn("二次不良", typeof(string)));
            dtTSInformation.Columns.Add(new DataColumn("工单", typeof(string)));
            dtTSInformation.Columns.Add(new DataColumn("工序", typeof(string)));
            dtTSInformation.Columns.Add(new DataColumn("产线", typeof(string)));
            dtTSInformation.Columns.Add(new DataColumn("资源", typeof(string)));
            dtTSInformation.Columns.Add(new DataColumn("维修状态", typeof(string)));
            dtTSInformation.Columns.Add(new DataColumn("发现人员", typeof(string)));
            dtTSInformation.Columns.Add(new DataColumn("发现时间", typeof(string)));
            //dtTSInformation.Columns.Add(new DataColumn("维修人员",typeof(string)));
            //dtTSInformation.Columns.Add(new DataColumn("维修时间",typeof(int)));

            dtTSInformation.DefaultView.Sort = "序列号,产品代码,工单,产线,资源,维修状态";
            this.ultraGridTSQuery.DataSource = dtTSInformation;
        }

        private void fillUltraTSInformation(object[] objs)
        {
            this.dtTSInformation.Rows.Clear();

            if (objs == null)
            {
                return;
            }
            ItemFacade itemFAC = new ItemFacade(this._domainDataProvider);
            object[] objItems = itemFAC.GetAllItem();//.GetItem(((BenQGuru.eMES.Domain.TS.TS)obj).ItemCode);
            foreach (object obj in objs)
            {
                string tsStatus = ((BenQGuru.eMES.Domain.TS.TS)obj).TSStatus;
                if (tsStatus == TSStatus.TSStatus_Reflow)
                    tsStatus = TSStatus.TSStatus_Complete;
                {

                    string itemName = String.Empty;
                    foreach (object objItem in objItems)
                    {
                        if (((Item)objItem).ItemCode.Trim() == ((BenQGuru.eMES.Domain.TS.TS)obj).ItemCode.Trim())
                        {
                            itemName = ((Item)objItem).ItemName;
                        }
                    }

                    this.dtTSInformation.Rows.Add(new object[] {((BenQGuru.eMES.Domain.TS.TS)obj).RunningCard,
																((BenQGuru.eMES.Domain.TS.TS)obj).ModelCode, 
																((BenQGuru.eMES.Domain.TS.TS)obj).ItemCode,
																itemName,
																((BenQGuru.eMES.Domain.TS.TS)obj).TSTimes == 0?1:((BenQGuru.eMES.Domain.TS.TS)obj).TSTimes,
																((BenQGuru.eMES.Domain.TS.TS)obj).MOCode,
																((BenQGuru.eMES.Domain.TS.TS)obj).FromOPCode,	
																((BenQGuru.eMES.Domain.TS.TS)obj).FromStepSequenceCode,
																((BenQGuru.eMES.Domain.TS.TS)obj).FromResourceCode,															
																MutiLanguages.ParserString(tsStatus),
																//发现人员，发现日期
																//Laws Lu,2005/09/29,修改	显示时间
																((BenQGuru.eMES.Domain.TS.TS)obj).FromUser,
																/*FormatHelper.ToDateString(*/((BenQGuru.eMES.Domain.TS.TS)obj).FromDate.ToString() + ((BenQGuru.eMES.Domain.TS.TS)obj).FormTime.ToString()
																//																((BenQGuru.eMES.Domain.TS.TS)obj).ConfirmUser,
																//																((BenQGuru.eMES.Domain.TS.TS)obj).ConfirmDate
															});
                }
            }
        }

        private void checkValidDateTimeInput(DateTime startDateTime, DateTime endDateTime)
        {
            if (startDateTime.CompareTo(endDateTime) > 0)
            {
                throw new Exception("$CSError_StartDate_LessThan_EndDate");
            }
        }
        #endregion


        #region 换料

        #region Grid
        private void InitializeUltraGridRepairItem()
        {
            this.ultraGridRepairItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;

            this.dtRepairItem.Columns.Clear();
            //for 多语言
            this.dtRepairItem.Columns.Add(new DataColumn("FLAG", typeof(bool)));
            this.dtRepairItem.Columns.Add(new DataColumn("ITEMSEQ", typeof(decimal)));
            this.dtRepairItem.Columns.Add(new DataColumn("MITEMCODE", typeof(string)));
            this.dtRepairItem.Columns.Add(new DataColumn("MCARD", typeof(string)));
            this.dtRepairItem.Columns.Add(new DataColumn("SOURCEMITEMCODE", typeof(string)));
            this.dtRepairItem.Columns.Add(new DataColumn("SOURCEMCARD", typeof(string)));
            this.dtRepairItem.Columns.Add(new DataColumn("IsReTS", typeof(bool)));
            this.dtRepairItem.Columns.Add(new DataColumn("LOC", typeof(string)));
            this.dtRepairItem.Columns.Add(new DataColumn("LOTNO", typeof(string)));
            this.dtRepairItem.Columns.Add(new DataColumn("VENDORCODE", typeof(string)));
            this.dtRepairItem.Columns.Add(new DataColumn("VENDORITEMCODE", typeof(string)));
            this.dtRepairItem.Columns.Add(new DataColumn("DATECODE", typeof(string)));
            this.dtRepairItem.Columns.Add(new DataColumn("REVERSION", typeof(string)));
            this.dtRepairItem.Columns.Add(new DataColumn("BIOS", typeof(string)));
            this.dtRepairItem.Columns.Add(new DataColumn("PCBA", typeof(string)));
            this.dtRepairItem.Columns.Add(new DataColumn("DESC", typeof(string)));
            this.dtRepairItem.Columns.Add(new DataColumn("MCardType", typeof(string)));
            this.dtRepairItem.Columns.Add(new DataColumn("MSourceCardType", typeof(string)));

            this.ultraGridRepairItem.DataSource = dtRepairItem;
        }

        private void ultraGridRepairItem_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraWinGridHelper ultraWinGridHelper = new UltraWinGridHelper(this.ultraGridRepairItem);

            ultraWinGridHelper.AddCheckColumn("FLAG", "*");
            ultraWinGridHelper.AddCommonColumn("ITEMSEQ", "ITEMSEQ");
            ultraWinGridHelper.AddCommonColumn("MITEMCODE", "物料号");
            ultraWinGridHelper.AddCommonColumn("MCARD", "物料序列号");
            ultraWinGridHelper.AddCommonColumn("SOURCEMITEMCODE", "原物料号");
            ultraWinGridHelper.AddCommonColumn("SOURCEMCARD", "原物料序列号");
            ultraWinGridHelper.AddCheckColumn("IsReTS", "是否再维修");
            ultraWinGridHelper.AddCommonColumn("LOC", "零件位置");
            ultraWinGridHelper.AddCommonColumn("LOTNO", "批号");
            ultraWinGridHelper.AddCommonColumn("VENDORCODE", "厂商代码");
            ultraWinGridHelper.AddCommonColumn("VENDORITEMCODE", "厂商料号");
            ultraWinGridHelper.AddCommonColumn("DATECODE", "生产日期");
            ultraWinGridHelper.AddCommonColumn("REVERSION", "料品版本");
            ultraWinGridHelper.AddCommonColumn("BIOS", "BIOS版本");
            ultraWinGridHelper.AddCommonColumn("PCBA", "PCBA版本");
            ultraWinGridHelper.AddCommonColumn("DESC", "补充说明");
            ultraWinGridHelper.AddCommonColumn("MCardType", "类型");
            ultraWinGridHelper.AddCommonColumn("MSourceCardType", "源类型");

            this.ultraGridRepairItem.DisplayLayout.Bands[0].Columns["IsReTS"].Width = 100;
            this.ultraGridRepairItem.DisplayLayout.Bands[0].Columns["ITEMSEQ"].Hidden = true;
            this.ultraGridRepairItem.DisplayLayout.Bands[0].Columns["DESC"].Hidden = true;
            this.ultraGridRepairItem.DisplayLayout.Bands[0].Columns["MCardType"].Hidden = true;
            this.ultraGridRepairItem.DisplayLayout.Bands[0].Columns["MSourceCardType"].Hidden = true;
        }

        private void ultraGridRepairItem_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            if (e.Cell.Column.Key != "FLAG")
            {
                e.Cell.CancelUpdate();
            }
        }
        #endregion

        #region Data
        private void InitializeMaterialItemcode()
        {
            this.ucLCItemMItem.Clear();

            ucLbSourceItemCode.Clear();

            if (this._currentTS == null)
            {
                return;
            }

            object[] objs = new SBOMFacade(this.DataProvider).GetSBOM(this._currentTS.ItemCode, 1, int.MaxValue);
            //Laws Lu,2006/01/09	注释
            //			object[] objOPBOMs = new OPBOMFacade(this.DataProvider).GetOPBOMDetails(this._currentTS.MOCode,this._currentTS.FromRouteCode,this._currentTS.FromOPCode);
            object[] objWipItems = (new DataCollectFacade(DataProvider)).ExtraQuery(_currentTS.RunningCard);
            if (objs != null)
            {
                foreach (object obj in objs)
                {
                    bool isExist = false;
                    foreach (object objItem in this.ucLCItemMItem.ComboBoxData.Items)
                    {
                        if (objItem.ToString() == ((SBOM)obj).SBOMItemCode)
                        {
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        this.ucLCItemMItem.AddItem(((SBOM)obj).SBOMItemCode, obj);

                        //this.ucLbSourceItemCode.AddItem( ((OPBOMDetail)obj).OPBOMItemCode, obj );
                    }
                }
            }
            //Laws Lu,2006/01/09	注释
            //			if(objOPBOMs != null)
            //			{
            //				foreach(object obj in objOPBOMs)
            //				{
            //					bool isExist = false;
            //					foreach(object objItem in this.ucLbSourceItemCode.ComboBoxData.Items)
            //					{
            //						if(objItem.ToString() ==((OPBOMDetail)obj).OPBOMItemCode)
            //						{
            //							isExist = true;
            //							break;
            //						}
            //					}
            //					if(!isExist)
            //					{
            //						this.ucLbSourceItemCode.AddItem( ((OPBOMDetail)obj).OPBOMItemCode, obj );
            //					}
            //				}
            //			}
            if (objWipItems != null)
            {
                foreach (object obj in objWipItems)
                {
                    bool isExist = false;
                    foreach (object objItem in this.ucLbSourceItemCode.ComboBoxData.Items)
                    {
                        if (objItem.ToString() == ((OnWIPItem)obj).MItemCode)
                        {
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        this.ucLbSourceItemCode.AddItem(((OnWIPItem)obj).MItemCode, obj);
                    }
                }
            }
        }

        private void fillUltraGridRepairItem()
        {
            this.ultraGridRepairItem.DataSource = null;

            this.dtRepairItem.Rows.Clear();

            if (this._currentTS != null)
            {
                object[] objs = this._tsFacade.ExtraQueryTSItem(this._currentTS.TSId);

                if (objs != null)
                {
                    foreach (object obj in objs)
                    {
                        this.dtRepairItem.Rows.Add(this.buildGridRowValues((TSItem)obj));
                    }
                }
            }

            this.ultraGridRepairItem.DataSource = this.dtRepairItem;
        }

        public object[] buildGridRowValues(TSItem tsItem)
        {
            return new object[] { false, 
									tsItem.ItemSequence,
									tsItem.MItemCode,
									tsItem.MCard,
									tsItem.SourceItemCode,
									tsItem.MSourceCard,
									FormatHelper.StringToBoolean(tsItem.IsReTS),
									tsItem.Location,
									tsItem.LotNO,
									tsItem.VendorCode,
									tsItem.VendorItemCode,
									tsItem.DateCode,
									//发现人员，发现日期
									tsItem.Reversion,
									tsItem.BIOS,
									tsItem.PCBA,				
									tsItem.MEMO,
									tsItem.MCardType,
									tsItem.MSourceCardType
								};
        }

        private void CheckEditData()
        {
            if (this.ucLCItemMItem.SelectedIndex == -1)
            {
                throw new Exception("请选择物料号!");
            }

            if (((OnWIPItem)this.ucLbSourceItemCode.SelectedItemValue).MCardType == MCardType.MCardType_Keyparts)
            {
                if (this.ucLEItemMCard.Value.Trim().Length == 0)
                {
                    throw new Exception("请填写物料序列号!");
                }
            }
            else
            {
                if (this.ucLEItemLot.Value.Trim().Length == 0)
                {
                    throw new Exception("请填写批号");
                }
            }
        }

        public TSItem getEditTSItem()
        {
            TSItem tsItem = this._tsFacade.CreateTSItem();

            tsItem.RunningCard = this._currentTS.RunningCard;
            tsItem.RunningCardSequence = this._currentTS.RunningCardSequence;
            tsItem.TSId = this._currentTS.TSId;
            tsItem.ModelCode = this._currentTS.ModelCode;
            tsItem.ItemCode = this._currentTS.ItemCode;
            tsItem.MOCode = this._currentTS.MOCode;
            tsItem.MItemCode = this.ucLCItemMItem.SelectedItemText;
            tsItem.MCard = this.ucLEItemMCard.Value;
            tsItem.SourceItemCode = ucLbSourceItemCode.SelectedItemText;//this.ucLCItemMItem.SelectedItemText;
            tsItem.MSourceCard = ucLEItemSourceRCID.Value;//this.ucLEItemMCard.Value;
            tsItem.LotNO = this.ucLEItemLot.Value;
            tsItem.Location = this.ucLEItemLocation.Value;
            tsItem.DateCode = this.ucLEItemDateCode.Value;
            tsItem.VendorCode = this.ucLEItemVendor.Value;
            tsItem.VendorItemCode = this.ucLEItemVendorItem.Value;
            tsItem.Reversion = this.ucLEItemVersion.Value;
            tsItem.BIOS = this.ucLEItemBIOS.Value;
            tsItem.PCBA = this.ucLEItemPCBA.Value;
            tsItem.MEMO = this.ucLEItemDescription.Value;
            tsItem.LocationPoint = 0;
            tsItem.MaintainUser = ApplicationService.Current().UserCode;
            tsItem.RepairResourceCode = ApplicationService.Current().ResourceCode;
            tsItem.RepairOPCode = OPType.TS;
            tsItem.IsReTS = FormatHelper.BooleanToString(checkBoxTS.Checked);//FormatHelper.FALSE_STRING;
            tsItem.Qty = 1;
            tsItem.MOSeq = this._currentTS.MOSeq;   // Added by Icyer 2007/07/03

            if ((this.ucLbSourceItemCode.SelectedItemValue as OnWIPItem).MCardType == MCardType.MCardType_Keyparts)
            {
                tsItem.MCardType = MCardType.MCardType_Keyparts;
            }
            else
            {
                tsItem.MCardType = MCardType.MCardType_INNO;
            }

            if ((this.ucLbSourceItemCode.SelectedItemValue as OnWIPItem).MCardType == MCardType.MCardType_Keyparts)
            {
                tsItem.MSourceCardType = MCardType.MCardType_Keyparts;
            }
            else
            {
                tsItem.MSourceCardType = MCardType.MCardType_INNO;
            }

            return tsItem;
        }

        //MOModel.ModelFacade mf			  = new MOModel.ModelFacade( ApplicationService.Current().DataProvider );	
        //Karron Qiu,2005-10-26,
        private BenQGuru.eMES.Domain.TS.TS GetReTS(TSItem item)
        {
            BenQGuru.eMES.Domain.TS.TS ts = this._currentTS;

            BenQGuru.eMES.Domain.TS.TS itemTs = null;
            if (FormatHelper.StringToBoolean(item.IsReTS) /*&& item.MCardType == MCardType.MCardType_Keyparts */)
            {
                itemTs = new BenQGuru.eMES.Domain.TS.TS();
                //Laws Lu,2005/09/01,修改	工单号码
                itemTs.MOCode = ts.MOCode;
                itemTs.RunningCard = item.MSourceCard;//MCard;
                itemTs.RunningCardSequence = this._tsFacade.GetUniqueTSRunningCardSequence(item.MSourceCard);
                //itemTs.TSId					 = ts.RunningCard + "-" + ts.RunningCardSequence;
                itemTs.TSId = FormatHelper.GetUniqueID(ts.MOCode, itemTs.RunningCard, itemTs.RunningCardSequence.ToString());
                //itemTs.EAttribute1			 = item.MItemCode;
                itemTs.TranslateCard = ts.RunningCard;
                itemTs.TranslateCardSequence = ts.RunningCardSequence;
                itemTs.CardType = CardType.CardType_Part;
                //之前拆解后的不良的状态为“待修”，现在改为拆解后的“需要再维修”的不良状态为“送修”。
                //经过离线维修送修确认后，状态就从“送修”转为“待修”
                itemTs.TSStatus = TSStatus.TSStatus_New;//TSStatus.TSStatus_Confirm;
                //itemTs.ConfirmResourceCode	 = item.RepairResourceCode;
                //itemTs.ConfirmOPCode		 = "TS";
                //				itemTs.ConfirmUser			 = item.MaintainUser;
                //				itemTs.ConfirmDate			 = FormatHelper.TODateInt( DateTime.Now );
                //				itemTs.ConfirmTime			 = FormatHelper.TOTimeInt( DateTime.Now );
                itemTs.MaintainUser = item.MaintainUser;
                itemTs.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                itemTs.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                //modified by jessie lee ,when the status is new ,there is not tsuser
                //itemTs.TSUser = item.MaintainUser;
                itemTs.TSDate = itemTs.ConfirmDate;
                itemTs.TSTime = itemTs.ConfirmTime;
                itemTs.FromInputType = TSFacade.TSSource_TS;
                itemTs.FromUser = itemTs.MaintainUser;
                itemTs.FromDate = itemTs.MaintainDate;
                itemTs.FormTime = itemTs.MaintainTime;
                itemTs.FromOPCode = "TS";
                itemTs.FromResourceCode = item.RepairResourceCode;


                #region 时间和资源的处理
                //Laws Lu,2005/09/16,修改	不允许~号的出现
                itemTs.FromRouteCode = ts.FromRouteCode;//"~";
                itemTs.FromSegmentCode = ts.FromSegmentCode;//"~";
                itemTs.FromShiftCode = ts.FromShiftCode;//"~";
                itemTs.FromShiftDay = ts.FromShiftDay;
                itemTs.FromShiftTypeCode = ts.FromShiftTypeCode;//"~";
                itemTs.FromStepSequenceCode = ts.FromStepSequenceCode;//"~";
                itemTs.FromTimePeriodCode = ts.FromTimePeriodCode;//"~";						
                #endregion
                //Laws Lu,2005/09/01,注释 允许记录来源TS的工单号
                //                            itemTs.MOCode ="";
                //Laws LU,2005/09/01,新增	保存来源工段
                itemTs.FromSegmentCode = ts.FromSegmentCode;
                itemTs.ItemCode = item.SourceItemCode;


                //				Model model=mf.GetModelByItemCode( _cui.SourceItemCode);//MItemCode);
                //				if (model==null)
                //				{
                //					throw new Exception("$CS_Model_Lost $CS_Param_Keyparts="
                //						+item.MSourceCard /*.MCard*/+" $CS_Param_ItemCode="+item.SourceItemCode /*.MItemCode*/);
                //				}
                itemTs.ModelCode = ts.ModelCode;

                itemTs.TransactionStatus = TransactionStatus.TransactionStatus_NO;
                itemTs.TSDate = 0;
                itemTs.TSTime = 0;
                itemTs.MOSeq = ts.MOSeq;    // Added by Icyer 2007/07/03
            }

            return itemTs;
        }
        #endregion

        #region 新增上料记录
        //Laws Lu,2006/01/06,允许记录Lot料的明细记录
        public void InsertINNOOnWipItem(TSItem item, DataCollectFacade dcf)
        {
            OnWIPItem wipItem = new OnWIPItem();

            wipItem = FillOnwipItem(item, dcf);

            wipItem.MCardType = MCardType.MCardType_INNO;

            dcf.AddOnWIPItem(wipItem);
        }
        //填充OnWipItem
        private OnWIPItem FillOnwipItem(TSItem item, DataCollectFacade dcf)
        {
            OnWIPItem wipItem = new OnWIPItem();

            BenQGuru.eMES.Material.CastDownHelper castHelper = new BenQGuru.eMES.Material.CastDownHelper(DataProvider);
            ArrayList arRcard = new ArrayList();
            castHelper.GetAllRCard(ref arRcard, item.RunningCard);
            arRcard.Add(item.RunningCard);
            string runningCards = "'" + String.Join("','", (string[])arRcard.ToArray(typeof(string))) + "'";

            object[] LastWip = dcf.GetLastOnWIPItem(runningCards, _currentTS.MOCode);

            if (item.MCardType == MCardType.MCardType_INNO)
            {
                wipItem.MCARD = (LastWip[0] as OnWIPItem).MCARD;
            }
            else
            {
                wipItem.MCARD = item.MCard;
            }

            //wipItem.MCARD = String.Empty;//iNNO;	
            wipItem.BIOS = item.BIOS;
            wipItem.DateCode = item.DateCode;
            wipItem.LotNO = item.LotNO;/*ActionOnLineHelper.StringNull;*/
            wipItem.MItemCode = item.MItemCode;/*ActionOnLineHelper.StringNull;*/
            wipItem.PCBA = item.PCBA;
            wipItem.VendorCode = item.VendorCode;
            wipItem.VendorItemCode = item.VendorItemCode;
            wipItem.Version = String.Empty;//item.;


            wipItem.ItemCode = _currentTS.ItemCode;
            wipItem.ResourceCode = ApplicationService.Current().ResourceCode;
            wipItem.RouteCode = _currentTS.FromRouteCode;
            wipItem.RunningCard = _currentTS.RunningCard;
            wipItem.RunningCardSequence = _currentTS.RunningCardSequence;
            BaseSetting.BaseModelFacade bas = new BaseSetting.BaseModelFacade(DataProvider);
            Domain.BaseSetting.Resource res = (Domain.BaseSetting.Resource)bas.GetResource(ApplicationService.Current().ResourceCode);

            wipItem.SegmentCode = res.SegmentCode;
            wipItem.ShiftTypeCode = res.ShiftTypeCode;
            wipItem.StepSequenceCode = res.StepSequenceCode;
            wipItem.MOCode = _currentTS.MOCode;
            wipItem.ModelCode = _currentTS.ModelCode;
            wipItem.OPCode = "TS";

            wipItem.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
            wipItem.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
            wipItem.MaintainUser = ApplicationService.Current().UserCode;

            BaseSetting.ShiftModelFacade smf = new BenQGuru.eMES.BaseSetting.ShiftModelFacade(DataProvider);

            TimePeriod period = (TimePeriod)smf.GetTimePeriod(res.ShiftTypeCode, wipItem.MaintainTime);
            if (period == null)
            {
                throw new Exception("$OutOfPerid");
            }

            wipItem.ShiftCode = period.ShiftCode;
            wipItem.TimePeriodCode = period.TimePeriodCode;

            wipItem.Qty = item.Qty;
            wipItem.TransactionStatus = TransactionStatus.TransactionStatus_YES;
            //TODO:序号需要从OnWipItem中取最大的MSequence
            wipItem.MSequence = (LastWip[0] as OnWIPItem).MSequence + 1;
            //Laws Lu,2005/12/20,新增	采集类型
            wipItem.ActionType = (int)MaterialType.CollectMaterial;
            wipItem.EAttribute1 = Material.WarehouseFacade.ReplaceMaterial;//维修换料
            wipItem.MOSeq = _currentTS.MOSeq;   // Added by Icyer 2007/07/02

            return wipItem;
        }
        //Laws Lu,2005/12/23,新增KeyParts上料记录
        public void InsertKeyPartOnWipItem(TSItem item, DataCollectFacade dcf)
        {
            OnWIPItem wipItem = new OnWIPItem();

            wipItem = FillOnwipItem(item, dcf);

            wipItem.MCardType = MCardType.MCardType_Keyparts;

            dcf.AddOnWIPItem(wipItem);
        }

        #endregion

        #region Action
        private void ucButtonItemAdd_Click(object sender, System.EventArgs e)
        {
            if (this._currentTS == null)
            {
                return;
            }

            this.DataProvider.BeginTransaction();
            try
            {
                this.CheckEditData();
                TSItem tsItem = this.getEditTSItem();

                //(new BaseSetting.BaseModelFacade(DataProvider)).get
                //Laws Lu,2005/12/22,新增	维修换料库房操作
                if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                {
                    Domain.BaseSetting.Resource res = (new BaseSetting.BaseModelFacade(DataProvider)).GetResource(ApplicationService.Current().ResourceCode) as Domain.BaseSetting.Resource;
                    (new Material.WarehouseFacade(DataProvider)).ReplaceMaterialStock(
                        this._currentTS.RunningCard
                        , this._currentTS.MOCode
                        , res.StepSequenceCode
                        , new object[] { tsItem }
                        , ApplicationService.Current().UserCode
                        , "TS");

                    DataCollectFacade dcf = new DataCollectFacade(DataProvider);
                    if (tsItem.MCardType == MCardType.MCardType_INNO)
                    {

                        InsertINNOOnWipItem(tsItem, dcf);
                    }
                    else
                    {
                        InsertKeyPartOnWipItem(tsItem, dcf);
                    }

                }
                this._tsFacade.AddTSItem(tsItem);

                #region Karron Qiu,2006-6-10-26,增加重修功能
                BenQGuru.eMES.Domain.TS.TS rets = GetReTS(tsItem);
                if (rets != null)
                {
                    this._tsFacade.AddTS(rets);

                    for (int i = 0; i < m_SelectedErrorCode.Length; i++)
                    {
                        TSErrorCode tsErrorCode = new TSErrorCode();
                        tsErrorCode.TSId = rets.TSId;
                        tsErrorCode.RunningCard = rets.RunningCard;
                        tsErrorCode.RunningCardSequence = rets.RunningCardSequence;
                        tsErrorCode.ModelCode = rets.ModelCode;
                        tsErrorCode.ItemCode = rets.ItemCode;
                        tsErrorCode.MOCode = rets.MOCode;
                        tsErrorCode.MaintainDate = rets.MaintainDate;
                        tsErrorCode.MaintainTime = rets.MaintainTime;
                        tsErrorCode.MaintainUser = rets.MaintainUser;
                        tsErrorCode.ErrorCode = ((ErrorCodeGroup2ErrorCode)m_SelectedErrorCode[i]).ErrorCode;
                        tsErrorCode.ErrorCodeGroup = ((ErrorCodeGroup2ErrorCode)m_SelectedErrorCode[i]).ErrorCodeGroup;
                        tsErrorCode.MOSeq = rets.MOSeq;     // Added by Icyer 2007/07/03

                        this._tsFacade.AddTSErrorCode(tsErrorCode);
                    }
                }
                #endregion
                //Laws Lu，2006/02/15，新增 刷新物料选择框
                InitializeMaterialItemcode();

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();

                this.ShowMessage(ex);

                return;
            }

            this.fillUltraGridRepairItem();
            this.TSItemEditStatus = FormStatus.Ready;
        }

        private void ucBtnItemUpdate_Click(object sender, System.EventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraGridRow selectedRow = null;

            //			foreach ( Infragistics.Win.UltraWinGrid.UltraGridRow row in this.ultraGridRepairItem.Rows )
            for (int iGridRowLoopIndex = 0; iGridRowLoopIndex < this.ultraGridRepairItem.Rows.Count; iGridRowLoopIndex++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridRepairItem.Rows[iGridRowLoopIndex];
                if (row.Cells[0].Text.ToUpper() == "TRUE")
                {
                    selectedRow = row;
                    break;
                }
            }

            if (selectedRow == null)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_Select_One_Row_To_Update"));	//请勾选要删除的记录

                return;
            }

            this.txtItemSequence.Text = selectedRow.Cells["ITEMSEQ"].Text;
            this.ucLCItemMItem.SetSelectItemText(selectedRow.Cells["MITEMCODE"].Text);
            this.ucLEItemMCard.Value = selectedRow.Cells["MCARD"].Text;
            this.ucLbSourceItemCode.SetSelectItemText(selectedRow.Cells["SOURCEMITEMCODE"].Text);
            this.ucLEItemSourceRCID.Value = selectedRow.Cells["SOURCEMCARD"].Text;
            checkBoxTS.Checked = bool.Parse(selectedRow.Cells["IsReTS"].Text);
            this.ucLEItemLot.Value = selectedRow.Cells["LOTNO"].Text;
            this.ucLEItemLocation.Value = selectedRow.Cells["LOC"].Text;
            this.ucLEItemDateCode.Value = selectedRow.Cells["DATECODE"].Text;
            this.ucLEItemVendor.Value = selectedRow.Cells["VENDORCODE"].Text;
            this.ucLEItemVendorItem.Value = selectedRow.Cells["VENDORITEMCODE"].Text;
            this.ucLEItemVersion.Value = selectedRow.Cells["REVERSION"].Text;
            this.ucLEItemBIOS.Value = selectedRow.Cells["BIOS"].Text;
            this.ucLEItemPCBA.Value = selectedRow.Cells["PCBA"].Text;
            this.ucLEItemDescription.Value = selectedRow.Cells["DESC"].Text;



            this.TSItemEditStatus = FormStatus.Update;
        }

        private void ucBtnItemDelete_Click(object sender, System.EventArgs e)
        {

            if (this._currentTS == null)
            {
                return;
            }

            if (this._currentTS.TSStatus != TSStatus.TSStatus_TS && this._currentTS.TSStatus != TSStatus.TSStatus_Confirm)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CSError_TSStatus_Should_be: $tsstatus_ts $tsstatus_confirm"));
                return;
            }

            ArrayList array = new ArrayList();
            ArrayList arTS = new ArrayList();

            //			foreach ( Infragistics.Win.UltraWinGrid.UltraGridRow row in this.ultraGridRepairItem.Rows )
            for (int iGridRowLoopIndex = 0; iGridRowLoopIndex < this.ultraGridRepairItem.Rows.Count; iGridRowLoopIndex++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridRepairItem.Rows[iGridRowLoopIndex];
                if (row.Cells[0].Text.ToUpper() == "TRUE")
                {
                    array.Add(row.Cells[1].Text);
                    arTS.Add(row.Cells["SOURCEMCARD"].Text);
                }
            }

            if (array.Count == 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_Select_One_Row_To_Delete"));	//请勾选要删除的记录

                return;
            }

            this.DataProvider.BeginTransaction();
            try
            {
                //TODO:Laws Lu，2006/01/06，需要检查是否送修
                this._tsFacade.DeleteTSItem(this._currentTS.TSId, (string[])array.ToArray(typeof(string)), (string[])arTS.ToArray(typeof(string)));


                //Laws Lu,2005/12/22,新增	维修换料库房操作
                if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                {
                    //考虑删除换料操作对库房的影响,TODO：逻辑还是有问题
                    ArrayList arRep = new ArrayList();
                    //					foreach ( Infragistics.Win.UltraWinGrid.UltraGridRow row in this.ultraGridRepairItem.Rows )
                    for (int iGridRowLoopIndex = 0; iGridRowLoopIndex < this.ultraGridRepairItem.Rows.Count; iGridRowLoopIndex++)
                    {
                        Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridRepairItem.Rows[iGridRowLoopIndex];
                        if (row.Cells[0].Text.ToUpper() == "TRUE")
                        {
                            TSItem it = new TSItem();
                            it.SourceItemCode = row.Cells["SOURCEMITEMCODE"].Text.Trim();
                            it.MItemCode = row.Cells["MITEMCODE"].Text.Trim();
                            it.MOCode = _currentTS.MOCode;

                            it.MSourceCard = row.Cells["SOURCEMCARD"].Text.Trim();
                            it.MCard = row.Cells["MCARD"].Text.Trim();

                            it.IsReTS = row.Cells["IsReTS"].Text;
                            it.TSId = _currentTS.TSId;
                            it.MCardType = row.Cells["MCardType"].Text.Trim();
                            it.MSourceCardType = row.Cells["MCardType"].Text.Trim();
                            it.Qty = 1;

                            arRep.Add(it);
                        }
                    }
                    Domain.BaseSetting.Resource res = (new BaseSetting.BaseModelFacade(DataProvider)).GetResource(ApplicationService.Current().ResourceCode) as Domain.BaseSetting.Resource;
                    (new Material.WarehouseFacade(DataProvider)).UnDoReplaceMaterialStock(
                        this._currentTS.RunningCard
                        , this._currentTS.MOCode
                        , res.StepSequenceCode
                        , arRep.ToArray()
                        , ApplicationService.Current().UserCode
                        , "TS");
                }

                //Laws Lu，2006/02/15，新增 刷新物料选择框
                InitializeMaterialItemcode();
                this.DataProvider.CommitTransaction();

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();

                this.ShowMessage(ex);

                return;
            }
            finally
            {
                //Laws Lu,2005/12/22,新增	关闭连接
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
            }

            this.fillUltraGridRepairItem();
            this.TSItemEditStatus = FormStatus.Ready;
        }

        private void ucBtnItemSave_Click(object sender, System.EventArgs e)
        {
            if (this._currentTS == null)
            {
                return;
            }

            try
            {
                this.CheckEditData();

                TSItem item = this.getEditTSItem();
                item.ItemSequence = System.Decimal.Parse(this.txtItemSequence.Text);

                this.DataProvider.BeginTransaction();

                //Laws Lu,2005/12/22,新增	维修换料库房操作
                if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                {
                    Domain.BaseSetting.Resource res = (new BaseSetting.BaseModelFacade(DataProvider)).GetResource(ApplicationService.Current().ResourceCode) as Domain.BaseSetting.Resource;
                    (new Material.WarehouseFacade(DataProvider)).ReplaceMaterialStock(
                        this._currentTS.RunningCard
                        , this._currentTS.MOCode
                        , res.StepSequenceCode
                        , new object[] { item }
                        , ApplicationService.Current().UserCode
                        , "TS");

                    ActionItem act = new ActionItem(DataProvider);
                    DataCollectFacade dcf = new DataCollectFacade(DataProvider);

                    if (item.MCardType == MCardType.MCardType_INNO)
                    {

                        InsertINNOOnWipItem(item, dcf);
                    }
                    else
                    {
                        InsertKeyPartOnWipItem(item, dcf);
                    }
                }

                this._tsFacade.UpdateTSItem(item);

                #region Karron Qiu,2006-6-10-26,增加重修功能
                BenQGuru.eMES.Domain.TS.TS rets = GetReTS(item);
                if (rets != null)
                {
                    object objTS = (new TSFacade(DataProvider)).GetCardLastTSRecord(rets.RunningCard);
                    Domain.TS.TS ts = objTS as Domain.TS.TS;
                    if ((ts != null && ts.FromInputType != TSFacade.TSSource_TS)
                        || ts == null)
                    {
                        this._tsFacade.AddTS(rets);

                        for (int i = 0; i < m_SelectedErrorCode.Length; i++)
                        {
                            TSErrorCode tsErrorCode = new TSErrorCode();
                            tsErrorCode.TSId = rets.TSId;
                            tsErrorCode.RunningCard = rets.RunningCard;
                            tsErrorCode.RunningCardSequence = rets.RunningCardSequence;
                            tsErrorCode.ModelCode = rets.ModelCode;
                            tsErrorCode.ItemCode = rets.ItemCode;
                            tsErrorCode.MOCode = rets.MOCode;
                            tsErrorCode.MaintainDate = rets.MaintainDate;
                            tsErrorCode.MaintainTime = rets.MaintainTime;
                            tsErrorCode.MaintainUser = rets.MaintainUser;
                            tsErrorCode.ErrorCode = ((ErrorCodeGroup2ErrorCode)m_SelectedErrorCode[i]).ErrorCode;
                            tsErrorCode.ErrorCodeGroup = ((ErrorCodeGroup2ErrorCode)m_SelectedErrorCode[i]).ErrorCodeGroup;
                            tsErrorCode.MOSeq = rets.MOSeq;     // Added by Icyer 2007/07/03

                            this._tsFacade.AddTSErrorCode(tsErrorCode);
                        }
                    }
                }
                #endregion

                //Laws Lu，2006/02/15，新增 刷新物料选择框
                InitializeMaterialItemcode();

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();

                this.ShowMessage(ex);

                return;
            }
            finally
            {
                //Laws Lu,2005/12/14,新增	关闭连接
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
            }

            this.fillUltraGridRepairItem();
            this.TSItemEditStatus = FormStatus.Ready;
        }

        private void ucBtnItemCancel_Click(object sender, System.EventArgs e)
        {
            this.TSItemEditStatus = FormStatus.Ready;
        }

        private void clearTSItemEdit()
        {
            this.txtItemSequence.Text = string.Empty;
            this.ucLCItemMItem.SelectedIndex = -1;
            this.ucLEItemMCard.Value = string.Empty;
            this.ucLbSourceItemCode.SelectedIndex = -1;
            this.ucLEItemSourceRCID.Value = string.Empty;
            this.checkBoxTS.Checked = false;
            this.ucLEItemLot.Value = string.Empty;
            this.ucLEItemLocation.Value = string.Empty;
            this.ucLEItemDateCode.Value = string.Empty;
            this.ucLEItemVendor.Value = string.Empty;
            this.ucLEItemVendorItem.Value = string.Empty;
            this.ucLEItemVersion.Value = string.Empty;
            this.ucLEItemBIOS.Value = string.Empty;
            this.ucLEItemPCBA.Value = string.Empty;
            this.ucLEItemDescription.Value = string.Empty;
        }
        #endregion

        #region Form Control
        public class FormStatus
        {
            public static string Update = "Update";
            public static string Ready = "Ready";
            public static string NoReady = "NoReady";
        }

        private string _tsItemEditStatus;
        private string TSItemEditStatus
        {
            get
            {
                return this._tsItemEditStatus;
            }
            set
            {
                this._tsItemEditStatus = value;

                if (this._tsItemEditStatus == FormStatus.NoReady)
                {
                    this.clearTSItemEdit();

                    this.ucBtnItemAdd.Enabled = false;
                    this.ucBtnItemUpdate.Enabled = false;
                    this.ucBtnItemCancel.Enabled = false;
                    this.ucBtnItemSave.Enabled = false;
                    this.ucBtnItemDelete.Enabled = false;
                }

                if (this._tsItemEditStatus == FormStatus.Ready)
                {
                    this.clearTSItemEdit();

                    this.ucBtnItemAdd.Enabled = true;
                    this.ucBtnItemUpdate.Enabled = true;
                    this.ucBtnItemCancel.Enabled = true;
                    this.ucBtnItemSave.Enabled = false;
                    this.ucBtnItemDelete.Enabled = true;
                }

                if (this._tsItemEditStatus == FormStatus.Update)
                {
                    this.ucBtnItemAdd.Enabled = false;
                    this.ucBtnItemUpdate.Enabled = false;
                    this.ucBtnItemCancel.Enabled = true;
                    this.ucBtnItemSave.Enabled = true;
                    this.ucBtnItemDelete.Enabled = false;
                }
            }
        }
        #endregion

        #endregion

        #region 维修

        private ListChangeHelper<string> _partHelper = new ListChangeHelper<string>();
        private ListChangeHelper<string> _locationHelper = new ListChangeHelper<string>();
        private BenQGuru.eMES.Domain.TS.TS _currentTS = null;

        #region 维修信息
        private void ucLEMNID_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!this.confirmChangeStatus())
                {
                    return;
                }

                // 显示维修信息
                this.displayTSInfo();
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                string sourceRCard = dataCollectFacade.GetSourceCard(this.ucLEMNID.Value.Trim().ToUpper(), string.Empty);
                InitialItem2Routes(sourceRCard);

                if (this.cbCopyTSInfo.Checked)
                {
                    GetTSInfoOfSameMO();
                }
            }
        }



        private BenQGuru.eMES.Domain.TS.TS getTSByRunningCard(string rcard, bool checkStatus)
        {
            if (rcard.Length == 0)
            {
                this.ShowMessage("$CS_Please_Input_RunningCard"); //产品序列号号不能为空
                return null;
            }

            // 获得序列号的TS信息

            BenQGuru.eMES.Domain.TS.TS ts = (BenQGuru.eMES.Domain.TS.TS)this._tsFacade.GetCardLastTSRecord(rcard);

            if (ts == null)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CSError_Card_Not_In_TS"));
                return null;
            }

            //Laws Lu,2005/09/19,修改	不良品处于＂待修＂状态的才可以进行拆解
            if (!checkStatus && ts.TSStatus != TSStatus.TSStatus_Confirm)
            {
                if (ts.TSStatus != TSStatus.TSStatus_Split)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, string.Format("$CSError_TSStatus_Should_be: ${0}/${1}", TSStatus.TSStatus_Confirm, TSStatus.TSStatus_TS)));
                    return null;
                }
            }

            if (checkStatus && ts.TSStatus != TSStatus.TSStatus_Confirm && ts.TSStatus != TSStatus.TSStatus_TS)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, string.Format("$CSError_TSStatus_Should_be: ${0}/${1}", TSStatus.TSStatus_Confirm, TSStatus.TSStatus_TS)));
                return null;
            }

            //			if ( checkStatus && ts.TSStatus != TSStatus.TSStatus_Confirm && ts.TSStatus != TSStatus.TSStatus_TS )
            //			{
            //				this.ShowMessage(new UserControl.Message(MessageType.Error,string.Format("$CSError_TSStatus_Should_be: ${0}/${1}", TSStatus.TSStatus_Confirm, TSStatus.TSStatus_TS)));
            //				return null;
            //			}
            //			if ( ts.FromInputType ==TSFacade.TSSource_TS )
            //			{
            //				this.ShowMessage("$CSError_KeypartsFromTsNotOnLine");
            //				return null;
            //			}

            return ts;
        }

        private void displayTSInfo()
        {
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(this.ucLEMNID.Value.Trim().ToUpper(), string.Empty);
            this._currentTS = this.getTSByRunningCard(sourceRCard, true);

            if (this._currentTS == null)
            {
                this.ultraTreeRunningCard.Nodes.Clear();
                this.dtRepairItem.Rows.Clear();
                this.ucLCItemMItem.Clear();

                this.TSEditStatus = ErrorInfoEditStatus.DoNothing;
                this.TSItemEditStatus = FormStatus.NoReady;

                return;
            }

            this.ucLEMemo.Value = this._currentTS.FromMemo;

            // 显示维修信息树
            this.buildTSInfoTree();
            this.TSEditStatus = ErrorInfoEditStatus.DoNothing;
            this.TSItemEditStatus = FormStatus.Ready;

            // 填写物料信息表
            this.fillUltraGridRepairItem();
            this.InitializeMaterialItemcode();

            //取得产品别下的所有不良原因组
            LoadErrorCauseGroup(this._currentTS.ModelCode);
        }


        private void ucBtnTSErrorEdit_Click(object sender, System.EventArgs e)
        {
            if (!this.confirmChangeStatus())
            {
                return;
            }
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(this.ucLEMNID.Value.Trim().ToUpper(), string.Empty);
            BenQGuru.eMES.Domain.TS.TS ts = this.getTSByRunningCard(sourceRCard, true);

            if (ts == null)
            {
                return;
            }

            FTSErrorEdit form = new FTSErrorEdit();
            form.RunningCardTitle = "Item";
            form.AddTSErrorCodeWhenSave = true;
            form.CurrentTS = ts;
            form.ShowDialog();

            this.displayTSInfo();
        }
        #endregion

        #region 维修信息树
        private object[] owninfos = null;
        private void buildTSInfoTree()
        {
            this.ultraTreeRunningCard.Nodes.Clear();

            Infragistics.Win.UltraWinTree.UltraTreeNode topNode = new Infragistics.Win.UltraWinTree.UltraTreeNode(this._currentTS.RunningCard);
            this.ultraTreeRunningCard.Nodes.Add(topNode);

            // 获得TS下所有的ErrorCode和ErrorCause
            object[] infos = this._tsFacade.GetTSInfoByTS(this._currentTS);
            this.owninfos = infos;

            if (infos != null)
            {
                foreach (TSErrorInfo info in infos)
                {
                    Infragistics.Win.UltraWinTree.UltraTreeNode node = new Infragistics.Win.UltraWinTree.UltraTreeNode(string.Format("{0}:{1}", info.ErrorCodeGroup, info.ErrorCode));

                    info.MaintainUser = ApplicationService.Current().UserCode;
                    node.Tag = info;
                    topNode.Nodes.Add(node);

                    if (info.ErrorCauseList == null)
                    {
                        continue;
                    }

                    foreach (TSErrorCause errCause in info.ErrorCauseList)
                    {
                        UltraTreeNode errCauseNode = new UltraTreeNode(node.Text + ":" + errCause.ErrorCauseGroupCode + ":" + errCause.ErrorCauseCode, errCause.ErrorCauseGroupCode + ":" + errCause.ErrorCauseCode);

                        errCause.MaintainUser = ApplicationService.Current().UserCode;
                        errCauseNode.Tag = errCause;

                        node.Nodes.Add(errCauseNode);
                    }

                }
            }
        }

        //增加不良位置的显示
        private void ShowIDErrorLocation(string tsid, string errorCodeGroup, string errorCode)
        {
            lstSelectErrorLocation.Clear();
            object[] errorCode2Locations = this._tsFacade.GetTSErrorCode2Location(tsid, errorCodeGroup, errorCode);
            if (errorCode2Locations != null)
            {
                for (int i = 0; i < errorCode2Locations.Length; i++)
                {
                    //AddErrorLocation(((TSErrorCode2Location)errorCode2Locations[i]).ErrorLocation);
                    lstSelectErrorLocation.Items.Add(((TSErrorCode2Location)errorCode2Locations[i]).ErrorLocation);
                }
            }
        }
        private void ultraTreeRunningCard_AfterActivate(object sender, Infragistics.Win.UltraWinTree.NodeEventArgs e)
        {
            if (!this.confirmChangeStatus())
            {
                return;
            }

            if (e.TreeNode.Tag is TSErrorCode)		// 点击不良代码组：不良代码
            {
                // 进入编辑ErrorCode状态
                this.TSEditStatus = ErrorInfoEditStatus.UpdateErrorCode;

                // 显示ErrorCode信息
                this.showErrorDescription(((TSErrorCode)e.TreeNode.Tag).ErrorCodeGroup, ((TSErrorCode)e.TreeNode.Tag).ErrorCode);
                this.showErrorCodeInfo((TSErrorCode)e.TreeNode.Tag);

                //this.ShowIDErrorLocation(((TSErrorCode)e.TreeNode.Tag).TSId,((TSErrorCode)e.TreeNode.Tag).ErrorCodeGroup,((TSErrorCode)e.TreeNode.Tag).ErrorCode);

                //				if(this.cbCopyTSInfo.Checked && (e.TreeNode.Nodes == null || e.TreeNode.Nodes.Count == 0))
                //				{
                //					ucBtnAdd_Click(null,null);
                //					TSErrorCause cause = GetSameErrorCodeTSErrorCause((TSErrorCode)e.TreeNode.Tag);
                //					this.showErrorCauseInfo(cause);
                //				}
            }
            else if (e.TreeNode.Tag is TSErrorCause)		// 点击不良原因
            {
                // 进入编辑ErrorCause状态
                this.TSEditStatus = ErrorInfoEditStatus.UpdateErrorCause;

                // 显示ErrorCause信息
                this.showErrorDescription(((TSErrorCause)e.TreeNode.Tag).ErrorCodeGroup, ((TSErrorCause)e.TreeNode.Tag).ErrorCode);
                this.showErrorCauseInfo((TSErrorCause)e.TreeNode.Tag);

                //this.ShowIDErrorLocation(((TSErrorCause)e.TreeNode.Tag).TSId,((TSErrorCause)e.TreeNode.Tag).ErrorCodeGroup,((TSErrorCause)e.TreeNode.Tag).ErrorCode);
            }
            else
            {
                this.TSEditStatus = ErrorInfoEditStatus.DoNothing;
            }
        }
        #endregion

        #region 不良信息
        private void showErrorDescription(string errorCodeGroup, string errorCode)
        {
            // ＂不良代码组描述＂栏位显示信息
            ErrorCodeGroupA errorCodeGroupA = (ErrorCodeGroupA)this._tsModelFacade.GetErrorCodeGroup(errorCodeGroup);

            if (errorCodeGroupA != null)
            {
                this.ucLEErrorCodeGroupDescription.Value = errorCodeGroupA.ErrorCodeGroupDescription;
            }
            else
            {
                this.ucLEErrorCodeGroupDescription.Value = string.Empty;
            }

            // ＂不良代码描述＂栏位显示信息
            ErrorCodeA errorCodeA = (ErrorCodeA)this._tsModelFacade.GetErrorCode(errorCode);

            if (errorCodeA != null)
            {
                this.ucLEErrorCodeDescription.Value = errorCodeA.ErrorDescription;
            }
            else
            {
                this.ucLEErrorCodeDescription.Value = string.Empty;
            }
        }

        private void showErrorCodeInfo(TSErrorCode errorCode)
        {
            if (errorCode != null)
            {
                // 在右侧的＂不良位置＂列表框中显示该不良品在产线的工序上采集不良时该不良代码下的不良位置．
                object[] objs = this._tsFacade.GetTSErrorCode2Location(this._currentTS.TSId, errorCode.ErrorCodeGroup, errorCode.ErrorCode);

                if (objs != null)
                {
                    foreach (TSErrorCode2Location location in objs)
                    {
                        this.lstSelectErrorLocation.Items.Add(location.ErrorLocation);
                    }
                }

                // 未选不良位置
                //				objs = this._tsFacade.GetUnselectedTSErrorCode2Location( this._currentTS.TSId, errorCode.ErrorCodeGroup, errorCode.ErrorCode );
                //
                //				if ( objs != null )
                //				{
                //					foreach ( ErrorLocation location in objs )
                //					{
                //						this.lstSelectErrorLocation.Items.Add( location.LocationCode );
                //					}
                //				}
            }
        }

        /// <summary>
        /// 在界面右侧如下栏位显示在维修时，挂在不良原因下的维修信息：
        /// 不良代码组描述/不良代码描述/不良原因/责任别/解决方案/补充说明/已选不良位置/已选不良零件
        /// </summary>
        /// <param name="errorCause"></param>
        private void showErrorCauseInfo(TSErrorCause errCause)
        {
            if (errCause != null)
            {
                //不良原因//不良原因/责任别/解决方案/补充说明
                this.ucLCErrorCauseGroup.SetSelectItem(errCause.ErrorCauseGroupCode);
                this.ucLCErrorCause.SetSelectItem(errCause.ErrorCauseCode);
                this.ucLCDuty.SetSelectItem(errCause.DutyCode);
                this.ucLCSolution.SetSelectItem(errCause.SolutionCode);
                this.ucLESolutionMemo.Value = errCause.SolutionMEMO;

                // 已选不良位置
                object[] objs = this._tsFacade.GetTSErrorCause2Location(this._currentTS.TSId, errCause.ErrorCodeGroup, errCause.ErrorCode, errCause.ErrorCauseCode);

                if (objs != null)
                {
                    foreach (TSErrorCause2Location location in objs)
                    {
                        this.lstSelectedErrorLocation.Items.Add(location.ErrorLocation);
                    }
                }

                // 未选不良位置
                //				objs = this._tsFacade.GetUnselectedTSErrorCause2Location( this._currentTS.TSId, errCause.ErrorCodeGroup, errCause.ErrorCode, errCause.ErrorCauseCode );
                //
                //				if ( objs != null )
                //				{
                //					foreach ( ErrorLocation location in objs )
                //					{
                //						this.lstSelectErrorLocation.Items.Add( location.LocationCode );
                //					}
                //				}
                ShowIDErrorLocation(this._currentTS.TSId, errCause.ErrorCodeGroup, errCause.ErrorCode);

                // 已选不良零件
                objs = this._tsFacade.GetTSErrorCause2ErrorPart(this._currentTS.TSId, errCause.ErrorCodeGroup, errCause.ErrorCode, errCause.ErrorCauseCode);

                if (objs != null)
                {
                    foreach (TSErrorCause2ErrorPart part in objs)
                    {
                        this.lstSelectedErrorPart.Items.Add(part.ErrorPart);
                    }
                }
            }
            else
            {
                this.clearErrorInfo();
            }
            //this.clearErrorInfo();

            //				// 未选不良位置
            //				object[] objs = this._tsFacade.GetUnselectedTSErrorCause2Location("", "", "", "");
            //
            //				if ( objs != null )
            //				{
            //					foreach ( ErrorLocation location in objs )
            //					{
            //						this.lstSelectErrorLocation.Items.Add( location.LocationCode );
            //					}
            //				}

            //				// 未选不良零件
            //				objs = this._tsFacade.GetUnselectedTSErrorCause2ErrorPart("", "", "", "");
            //
            //				if ( objs != null )
            //				{
            //					foreach ( ErrorPart part in objs )
            //					{
            //						this.lstSelectErrorPart.Items.Add( part.PartCode );
            //					}
            //				}

        }
        #endregion

        #region 下拉框数据
        private void ucLCErrorCause_Load(object sender, System.EventArgs e)
        {
            //			this.ucLCErrorCause.Clear();
            //			this.ucLCErrorCause.AddItem("", "");
            //
            //			object[] objs = this._tsModelFacade.GetAllErrorCause();
            //
            //			if ( objs == null )
            //			{
            //				return;
            //			}
            //
            //			foreach ( ErrorCause errCause in objs )
            //			{
            //				this.ucLCErrorCause.AddItem( errCause.ErrorCauseDescription, errCause.ErrorCauseCode );
            //			}
        }

        private void ucLCDuty_Load(object sender, System.EventArgs e)
        {
            this.ucLCDuty.Clear();
            this.ucLCDuty.AddItem("", "");

            object[] objs = this._tsModelFacade.GetAllDuty();

            if (objs == null)
            {
                return;
            }

            foreach (Duty duty in objs)
            {
                this.ucLCDuty.AddItem(duty.DutyDescription, duty.DutyCode);
            }
        }


        private void ucLCSolution_Load(object sender, System.EventArgs e)
        {
            this.ucLCSolution.Clear();
            this.ucLCSolution.AddItem("", "");

            object[] objs = this._tsModelFacade.GetAllSolution();

            if (objs == null)
            {
                return;
            }

            foreach (Solution solution in objs)
            {
                this.ucLCSolution.AddItem(solution.SolutionDescription, solution.SolutionCode);
            }
        }
        #endregion

        #region 不良原因维护

        private void ucBtnAdd_Click(object sender, System.EventArgs e)
        {
            if (!this.confirmChangeStatus())
            {
                return;
            }

            // 新增ErrorCause
            if (this.currentErrorCode != null)
            {
                this.TSEditStatus = ErrorInfoEditStatus.AddErrorCause;

                //Laws Lu,2005/09/05,修改
                this.showErrorCodeInfo(currentErrorCode);
            }
        }

        private void ucBtnDelete_Click(object sender, System.EventArgs e)
        {
            // 删除ErrorCause
            if (this.currentErrorCause != null)
            {
                // 确认删除不良原因
                if (MessageBox.Show("确认删除不良原因？", this.Text, MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    this._tsFacade.DeleteTSErrorCause(this.currentErrorCause);
                }
                catch (Exception ex)
                {
                    this.ShowMessage(ex);
                    return;
                }

                this.ultraTreeRunningCard.ActiveNode.Remove();

                this.TSEditStatus = ErrorInfoEditStatus.DoNothing;
            }
        }

        private void ucBtnSave_Click(object sender, System.EventArgs e)
        {
            #region 编辑ErrorCode
            if (this.TSEditStatus == ErrorInfoEditStatus.UpdateErrorCode)
            {
                if (!this._locationHelper.IsDirty)
                {
                    return;
                }

                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                this.DataProvider.BeginTransaction();
                try
                {
                    if (this._locationHelper.AddList.Length > 0)
                    {
                        this._tsFacade.AddTSErrorCode2Location(this.currentErrorCode, this._locationHelper.AddList);
                    }
                    if (this._locationHelper.DeleteList.Length > 0)
                    {
                        this._tsFacade.DeleteTSErrorCode2Location(this.currentErrorCode, this._locationHelper.DeleteList);
                    }

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();

                    this.ShowMessage(ex);
                    return;
                }
                finally
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                }
            }
            #endregion

            #region 新增ErrorCause
            else if (this.TSEditStatus == ErrorInfoEditStatus.AddErrorCause)
            {
                foreach (Infragistics.Win.UltraWinTree.UltraTreeNode node in this.ultraTreeRunningCard.ActiveNode.Nodes)
                {
                    if (node.Text.ToUpper() == this.ucLCErrorCauseGroup.SelectedItemValue.ToString() + ":" + this.ucLCErrorCause.SelectedItemValue.ToString())
                    {
                        this.ShowMessage("$Error_CS_ErrorCause_is_Exist");
                        return;
                    }
                }

                TSErrorCause errCause = this.getEditTSErrorCause();

                if (errCause == null)
                {
                    return;
                }

                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                this.DataProvider.BeginTransaction();
                try
                {
                    //Laws Lu,2005/11/09,新增	记录ShiftDay
                    BaseSetting.BaseModelFacade dataModel = new BaseSetting.BaseModelFacade(this.DataProvider);
                    Domain.BaseSetting.Resource res = (Domain.BaseSetting.Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);
                    //onwip.SegmentCode				= productInfo.NowSimulationReport.SegmentCode;

                    BaseSetting.ShiftModelFacade shiftModel = new BaseSetting.ShiftModelFacade(this.DataProvider);
                    Domain.BaseSetting.TimePeriod period = (Domain.BaseSetting.TimePeriod)shiftModel.GetTimePeriod(res.ShiftTypeCode, Web.Helper.FormatHelper.TOTimeInt(DateTime.Now));
                    if (period == null)
                    {
                        throw new Exception("$OutOfPerid");
                    }

                    if (period.IsOverDate == Web.Helper.FormatHelper.TRUE_STRING)
                    {
                        if (period.TimePeriodBeginTime < period.TimePeriodEndTime)
                        {
                            errCause.TSShiftDay = FormatHelper.TODateInt(DateTime.Now.AddDays(-1));
                        }
                        else if (Web.Helper.FormatHelper.TOTimeInt(DateTime.Now) < period.TimePeriodBeginTime)
                        {
                            errCause.TSShiftDay = FormatHelper.TODateInt(DateTime.Now.AddDays(-1));
                        }
                        else
                        {
                            errCause.TSShiftDay = FormatHelper.TODateInt(DateTime.Now);
                        }
                    }
                    else
                    {
                        errCause.TSShiftDay = FormatHelper.TODateInt(DateTime.Now);
                    }

                    this._tsFacade.AddTSErrorCause(ApplicationService.Current().UserCode, errCause);

                    if (this._locationHelper.AddList.Length > 0)
                    {
                        this._tsFacade.AddTSErrorCause2Location(errCause, this._locationHelper.AddList);
                    }
                    if (this._locationHelper.DeleteList.Length > 0)
                    {
                        this._tsFacade.DeleteTSErrorCause2Location(errCause, this._locationHelper.DeleteList);
                    }

                    if (this._partHelper.AddList.Length > 0)
                    {
                        this._tsFacade.AddTSErrorCause2ErrorPart(errCause, this._partHelper.AddList);
                    }
                    if (this._partHelper.DeleteList.Length > 0)
                    {
                        this._tsFacade.DeleteTSErrorCause2ErrorPart(errCause, this._partHelper.DeleteList);
                    }

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();

                    this.ShowMessage(ex);
                    return;
                }
                finally
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                }

                // 添加进树
                this.TSEditStatus = ErrorInfoEditStatus.DoNothing;
                Infragistics.Win.UltraWinTree.UltraTreeNode errCauseNode = new Infragistics.Win.UltraWinTree.UltraTreeNode(this.ultraTreeRunningCard.ActiveNode.Text + ":" + errCause.ErrorCauseGroupCode + ":" + errCause.ErrorCauseCode, errCause.ErrorCauseGroupCode + ":" + errCause.ErrorCauseCode);
                errCauseNode.Tag = errCause;
                this.ultraTreeRunningCard.ActiveNode.ExpandAll();
                this.ultraTreeRunningCard.ActiveNode.Nodes.Add(errCauseNode);
                this.ultraTreeRunningCard_AfterActivate(this, new Infragistics.Win.UltraWinTree.NodeEventArgs(this.ultraTreeRunningCard.ActiveNode));
                //this.ucBtnCancel_Click(this, null);
            }
            #endregion

            #region 编辑ErrorCause
            else if (this.TSEditStatus == ErrorInfoEditStatus.UpdateErrorCause)
            {
                TSErrorCause errCause = this.getEditTSErrorCause();

                if (errCause == null)
                {
                    return;
                }

                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                this.DataProvider.BeginTransaction();

                try
                {
                    this._tsFacade.UpdateTSErrorCause(errCause);

                    if (this._locationHelper.AddList.Length > 0)
                    {
                        this._tsFacade.AddTSErrorCause2Location(errCause, this._locationHelper.AddList);
                    }
                    if (this._locationHelper.DeleteList.Length > 0)
                    {
                        this._tsFacade.DeleteTSErrorCause2Location(errCause, this._locationHelper.DeleteList);
                    }

                    if (this._partHelper.AddList.Length > 0)
                    {
                        this._tsFacade.AddTSErrorCause2ErrorPart(errCause, this._partHelper.AddList);
                    }
                    if (this._partHelper.DeleteList.Length > 0)
                    {
                        this._tsFacade.DeleteTSErrorCause2ErrorPart(errCause, this._partHelper.DeleteList);
                    }

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();

                    this.ShowMessage(ex);
                    return;
                }
                finally
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                }
            }
            #endregion

            // 保存成功 清空缓存
            this._locationHelper.Clear();
            this._partHelper.Clear();

            this.ShowMessage(new UserControl.Message(MessageType.Success, "$CS_Save_Success"));
            ucLEMNID.TextFocus(false, true);
            //Remove UCLabel.SelectAll;
        }

        private void ucBtnCancel_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("是否取消所做的修改？", this.Text, MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            this._locationHelper.Clear();
            this._partHelper.Clear();

            this.ultraTreeRunningCard_AfterActivate(this, new Infragistics.Win.UltraWinTree.NodeEventArgs(this.ultraTreeRunningCard.ActiveNode));
        }

        private TSErrorCause getEditTSErrorCause()
        {
            if (!this.validateErrorCauseInput())
            {
                return null;
            }

            TSErrorCause tsErrorCause = null;

            if (this.currentErrorCause != null)
            {
                tsErrorCause = this.currentErrorCause;
            }
            else
            {
                tsErrorCause = this._tsFacade.CreateNewTSErrorCause();
                TSErrorCode tsErrorCode = this.currentErrorCode;

                tsErrorCause.TSId = tsErrorCode.TSId;
                tsErrorCause.ErrorCodeGroup = tsErrorCode.ErrorCodeGroup;
                tsErrorCause.ErrorCode = tsErrorCode.ErrorCode;
                tsErrorCause.RunningCard = tsErrorCode.RunningCard;
                tsErrorCause.RunningCardSequence = tsErrorCode.RunningCardSequence;
                tsErrorCause.ModelCode = tsErrorCode.ModelCode;
                tsErrorCause.ItemCode = tsErrorCode.ItemCode;
                tsErrorCause.MOCode = tsErrorCode.MOCode;
                tsErrorCause.MOSeq = tsErrorCode.MOSeq;     // Added by Icyer 2007/07/03
            }

            tsErrorCause.RepairResourceCode = ApplicationService.Current().ResourceCode;
            tsErrorCause.RepairOPCode = OPType.TS;

            tsErrorCause.ErrorCauseGroupCode = this.ucLCErrorCauseGroup.SelectedItemValue.ToString();
            tsErrorCause.ErrorCauseCode = this.ucLCErrorCause.SelectedItemValue.ToString();
            tsErrorCause.DutyCode = this.ucLCDuty.SelectedItemValue.ToString();
            tsErrorCause.SolutionCode = this.ucLCSolution.SelectedItemValue.ToString();
            tsErrorCause.SolutionMEMO = this.ucLESolutionMemo.Value.Trim();

            //Laws Lu,2005/11/22,新增	添加代理录入人员
            //modified by jessie lee, 2005/11/24
            UserControl.Messages messages = new Messages();
            string userCode = ApplicationService.Current().UserCode;
            if (txtAgentUser.Checked == true && txtAgentUser.Value.Trim().Length == 0)
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Error_Vicegerent_CanNot_Empty"));
                ApplicationRun.GetInfoForm().Add(messages);
                return null;
            }
            else if (txtAgentUser.Checked == true && txtAgentUser.Value != String.Empty)
            {
                if ((new BenQGuru.eMES.Security.SecurityFacade(this.DataProvider)).CheckResourceRight(txtAgentUser.Value.Trim().ToUpper()
                    , ApplicationService.Current().ResourceCode))
                {
                    userCode = txtAgentUser.Value;
                }
                else
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_Error_Vicegerent_Is_Wrong"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return null;
                }
            }

            tsErrorCause.MaintainUser = userCode;
            tsErrorCause.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
            tsErrorCause.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);

            return tsErrorCause;
        }

        private bool validateErrorCauseInput()
        {
            bool validate = true;

            if (this.ucLCErrorCauseGroup.SelectedIndex < 1)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_Please_Select_ErrorCauseGroup"));
                validate = false;
            }

            if (this.ucLCErrorCause.SelectedIndex < 1)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_Please_Select_ErrorCause"));
                validate = false;
            }
            if (this.ucLCDuty.SelectedIndex < 1)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_Please_Select_Duty"));
                validate = false;
            }


            if (this.ucLCSolution.SelectedIndex < 1)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_Please_Select_Solution"));
                validate = false;
            }


            return validate;
        }
        #endregion

        #region 不良位置维护
        private void ucBtnDeleteErrorLocation_Click(object sender, System.EventArgs e)
        {
            // 从已维护不良位置列表中删除
            foreach (ListViewItem item in this.lstSelectedErrorLocation.SelectedItems)
            {
                this.lstSelectErrorLocation.Items.Add(item.Text);
                this._locationHelper.Delete(item.Text);
                item.Remove();
            }
        }

        private void lstSelectedErrorLocation_DoubleClick(object sender, System.EventArgs e)
        {
            this.ucBtnDeleteErrorLocation_Click(sender, e);
        }

        private void AddErrorLocation(string errorLocation)
        {
            // 手动添加不良位置
            if (errorLocation == string.Empty)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_Input_Error_Location"));
                return;
            }

            foreach (ListViewItem item in this.lstSelectedErrorLocation.Items)
            {
                if (item.Text.ToUpper() == errorLocation.ToUpper())
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_Error_Location_is_Exist"));
                    return;
                }
            }

            foreach (ListViewItem item in this.lstSelectErrorLocation.Items)
            {
                if (item.Text.ToUpper() == errorLocation.ToUpper())
                {
                    item.Remove();
                    break;
                }
            }

            this.lstSelectedErrorLocation.Items.Add(errorLocation);
            this._locationHelper.Add(errorLocation);
        }
        private void ucBtnAddErrorLocation_Click(object sender, System.EventArgs e)
        {
            AddErrorLocation(this.ucLEErrorLocation.Value.Trim());
        }

        private void ucLEErrorLocation_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.ucBtnAddErrorLocation_Click(this, null);
            }
        }

        private void lstSelectErrorLocation_DoubleClick(object sender, System.EventArgs e)
        {
            // 修改 by Mark Lee 20050823
            foreach (ListViewItem item in this.lstSelectErrorLocation.SelectedItems)
            {
                AddErrorLocation(item.Text);
            }

            /*
            // 双击未选列表中的项,添加入已选列表
            foreach ( ListViewItem item in this.lstSelectErrorLocation.SelectedItems )
            {
                foreach ( ListViewItem i in this.lstSelectedErrorLocation.Items )
                {
                    if ( i.Text.ToUpper() == item.Text.ToUpper() )
                    {
                        this.ShowMessage("$Error_CS_Error_Location_is_Exist");
                        return;
                    }
                }

                item.Remove();

                this.lstSelectedErrorLocation.Items.Add( item.Text );
                this._locationHelper.Add( item.Text );
            }
            */
            //  修改  by Mark Lee 20050823 END
        }
        #endregion

        #region 不良零件维护

        private void ucBtnDeleteErrorPart_Click(object sender, System.EventArgs e)
        {
            foreach (ListViewItem item in this.lstSelectedErrorPart.SelectedItems)
            {
                this.lstSelectErrorPart.Items.Add(item.Text);
                this._partHelper.Delete(item.Text);
                item.Remove();
            }
        }

        private void ucBtnQueryErrorPart_Click(object sender, System.EventArgs e)
        {
            this.lstSelectErrorPart.Clear();

            object[] objs = null;

            //			if ( this.TSEditStatus == ErrorInfoEditStatus.AddErrorCause )
            //			{
            //				objs = this._tsFacade.GetUnselectedTSErrorCause2ErrorPart( 
            //					this._currentTS.TSId);
            //			}

            //			if ( this.TSEditStatus == ErrorInfoEditStatus.UpdateErrorCause )
            //			{
            // 查询未选不良零件
            //Laws Lu,2005/08/25,修改	根据标准BOM获取零件列表
            //Laws Lu,2005/09/13,修改	支持不良零件的模糊查询
            TSErrorCause errCause = this.currentErrorCause;
            MO mo = new MOFacade(this.DataProvider).GetMO(this._currentTS.MOCode) as MO;
            objs = this._tsFacade.GetUnselectedTSErrorCause2ErrorPart(
                this._currentTS.TSId, mo.BOMVersion);
            //			}


            if (objs != null)
            {
                foreach (SBOM part in objs)
                {
                    bool isExist = false;
                    foreach (ListViewItem item in lstSelectErrorPart.Items)
                    {
                        if (item.Text.Trim() == part.SBOMItemCode.Trim())
                        {
                            isExist = true;
                            break;
                        }
                    }

                    int iTxtLen = ucLEErrorPart.Value.Length;
                    int iBomLen = part.SBOMItemCode.Trim().Length;
                    if (iBomLen >= iTxtLen)
                    {
                        if (!isExist && ucLEErrorPart.Value.Trim() != String.Empty
                            && part.SBOMItemCode.Trim().Substring(0, ucLEErrorPart.Value.Length)
                            == ucLEErrorPart.Value.Trim().ToUpper())
                        {
                            this.lstSelectErrorPart.Items.Add(part.SBOMItemCode);
                        }
                        else if (!isExist && ucLEErrorPart.Value.Trim() == String.Empty)
                        {
                            this.lstSelectErrorPart.Items.Add(part.SBOMItemCode);
                        }
                    }
                }
            }
        }

        private void ucLEErrorPart_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.ucBtnQueryErrorPart_Click(this, null);
            }
        }

        private void lstSelectErrorPart_DoubleClick(object sender, System.EventArgs e)
        {
            // 双击未选列表中的项,添加入已选列表
            foreach (ListViewItem item in this.lstSelectErrorPart.SelectedItems)
            {
                foreach (ListViewItem i in this.lstSelectedErrorLocation.Items)
                {
                    if (i.Text.ToUpper() == item.Text.ToUpper())
                    {
                        this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_Error_Part_is_Exist"));
                        return;
                    }
                }

                item.Remove();

                this.lstSelectedErrorPart.Items.Add(item.Text);
                this._partHelper.Add(item.Text);
            }
        }

        private void lstSelectedErrorPart_DoubleClick(object sender, System.EventArgs e)
        {
            this.ucBtnDeleteErrorPart_Click(sender, e);
        }
        #endregion

        #region 不良信息编辑状态
        private TSErrorCode currentErrorCode
        {
            get
            {
                if (this.ultraTreeRunningCard.ActiveNode == null)
                {
                    return null;
                }

                if (this.ultraTreeRunningCard.ActiveNode.Tag is TSErrorCode)
                {
                    return (TSErrorCode)this.ultraTreeRunningCard.ActiveNode.Tag;
                }

                return null;
            }
        }

        private TSErrorCause currentErrorCause
        {
            get
            {
                if (this.ultraTreeRunningCard.ActiveNode == null)
                {
                    return null;
                }

                if (this.ultraTreeRunningCard.ActiveNode.Tag is TSErrorCause)
                {
                    return (TSErrorCause)this.ultraTreeRunningCard.ActiveNode.Tag;
                }

                return null;
            }
        }

        private class ErrorInfoEditStatus
        {
            public static string DoNothing = "DoNothing";
            public static string UpdateErrorCode = "UpdateErrorCode";
            public static string AddErrorCause = "AddErrorCause";
            public static string UpdateErrorCause = "UpdateErrorCause";
        }

        private string _errInfoStatus = string.Empty;

        private string TSEditStatus
        {
            get
            {
                return this._errInfoStatus;
            }
            set
            {
                this._errInfoStatus = value;
                this.clearErrorInfo();

                if (this._errInfoStatus == ErrorInfoEditStatus.DoNothing)
                {
                    this.ucLEErrorCodeGroupDescription.Value = string.Empty;
                    this.ucLEErrorCodeDescription.Value = string.Empty;

                    this.ucLCErrorCauseGroup.Enabled = false;
                    this.ucLCErrorCause.Enabled = false;
                    this.ucLCDuty.Enabled = false;
                    this.ucLCSolution.Enabled = false;
                    this.ucLESolutionMemo.Enabled = false;

                    this.enableEditLocation(false);
                    this.enableEditPart(false);

                    this.ucBtnAdd.Enabled = false;
                    this.ucBtnDelete.Enabled = false;
                    this.ucBtnSave.Enabled = false;
                    this.ucBtnCancel.Enabled = false;
                }

                if (this._errInfoStatus == ErrorInfoEditStatus.UpdateErrorCode)
                {
                    this.ucLCErrorCauseGroup.Enabled = false;
                    this.ucLCErrorCause.Enabled = false;
                    this.ucLCDuty.Enabled = false;
                    this.ucLCSolution.Enabled = false;
                    this.ucLESolutionMemo.Enabled = false;

                    this.enableEditLocation(false);
                    this.enableEditPart(false);

                    this.ucBtnAdd.Enabled = true;
                    this.ucBtnDelete.Enabled = false;
                    this.ucBtnSave.Enabled = true;
                    this.ucBtnCancel.Enabled = true;
                }

                if (this._errInfoStatus == ErrorInfoEditStatus.AddErrorCause)
                {
                    this.ucLCErrorCauseGroup.Enabled = true;
                    this.ucLCErrorCause.Enabled = true;
                    this.ucLCDuty.Enabled = true;
                    this.ucLCSolution.Enabled = true;
                    this.ucLESolutionMemo.Enabled = true;

                    this.enableEditLocation(true);
                    this.enableEditPart(true);

                    this.ucBtnAdd.Enabled = false;
                    this.ucBtnDelete.Enabled = false;
                    this.ucBtnSave.Enabled = true;
                    this.ucBtnCancel.Enabled = true;
                }

                if (this._errInfoStatus == ErrorInfoEditStatus.UpdateErrorCause)
                {
                    this.ucLCErrorCauseGroup.Enabled = false;
                    this.ucLCErrorCause.Enabled = false;
                    this.ucLCDuty.Enabled = true;
                    this.ucLCSolution.Enabled = true;
                    this.ucLESolutionMemo.Enabled = true;

                    this.enableEditLocation(true);
                    this.enableEditPart(true);

                    this.ucBtnAdd.Enabled = false;
                    this.ucBtnDelete.Enabled = true;
                    this.ucBtnSave.Enabled = true;
                    this.ucBtnCancel.Enabled = true;
                }
            }
        }

        private void clearErrorInfo()
        {
            if (this.ucLCErrorCauseGroup.ComboBoxData.Items.Count > 0)
                this.ucLCErrorCauseGroup.SelectedIndex = 0;
            if (this.ucLCErrorCause.ComboBoxData.Items.Count > 0)
                this.ucLCErrorCause.SelectedIndex = 0;
            this.ucLCDuty.SelectedIndex = 0;
            this.ucLCSolution.SelectedIndex = 0;
            this.ucLESolutionMemo.Value = string.Empty;

            this.lstSelectedErrorLocation.Items.Clear();
            this.lstSelectedErrorPart.Items.Clear();
            this.lstSelectErrorLocation.Items.Clear();
            this.lstSelectErrorPart.Items.Clear();

            this._locationHelper.Clear();
            this._partHelper.Clear();
        }

        private void enableEditLocation(bool enable)
        {
            this.ucBtnDeleteErrorLocation.Enabled = enable;
            this.ucBtnAddErrorLocation.Enabled = enable;
            this.ucLEErrorLocation.Enabled = enable;

            this.lstSelectedErrorLocation.Enabled = enable;
            this.lstSelectErrorLocation.Enabled = enable;
        }

        private void enableEditPart(bool enable)
        {
            this.ucBtnDeleteErrorPart.Enabled = enable;
            this.ucBtnQueryErrorPart.Enabled = enable;
            this.ucLEErrorPart.Enabled = enable;

            this.lstSelectedErrorPart.Enabled = enable;
            this.lstSelectErrorPart.Enabled = enable;
        }

        private bool confirmChangeStatus()
        {
            if (this._locationHelper.IsDirty || this._partHelper.IsDirty)
            {
                if (MessageBox.Show("是否保存所做的修改?", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.ucBtnSave_Click(this, null);

                    return true;
                }

                this._locationHelper.Clear();
                this._partHelper.Clear();

                return false;
            }

            return true;
        }
        #endregion

        #endregion

        #region 拆解
        private ArrayList _itemList = new ArrayList();
        private BenQGuru.eMES.Domain.TS.TS _splitTS = null;

        public void InitializeUltraGridSplitItem()
        {
            // 使单元格可编辑
            this.ultraGridSplitItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;

            this.dtSplitItem.Columns.Clear();
            //for 多语言
            this.dtSplitItem.Columns.Add(new DataColumn("NEEDTS", typeof(bool)));
            this.dtSplitItem.Columns.Add(new DataColumn("SCRAPQTY", typeof(decimal)));
            this.dtSplitItem.Columns.Add(new DataColumn("MITEMCODE", typeof(string)));
            this.dtSplitItem.Columns.Add(new DataColumn("MCARD", typeof(string)));
            this.dtSplitItem.Columns.Add(new DataColumn("QTY", typeof(decimal)));
            this.dtSplitItem.Columns.Add(new DataColumn("MCARDTYPE", typeof(string)));
            this.dtSplitItem.Columns.Add(new DataColumn("SPLITUSER", typeof(string)));
            this.dtSplitItem.Columns.Add(new DataColumn("SPLITDATE", typeof(string)));

            this.ultraGridSplitItem.DataSource = this.dtSplitItem;
        }

        private void ultraGridSplitItem_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraWinGridHelper ultraWinGridHelper = new UltraWinGridHelper(this.ultraGridSplitItem);

            ultraWinGridHelper.AddCheckColumn("NEEDTS", "是否再维修");
            ultraWinGridHelper.AddCommonColumn("SCRAPQTY", "报废数量");
            ultraWinGridHelper.AddCommonColumn("MITEMCODE", "料号");
            ultraWinGridHelper.AddCommonColumn("MCARD", "物料序列号");
            ultraWinGridHelper.AddCommonColumn("QTY", "数量");
            ultraWinGridHelper.AddCommonColumn("MCARDTYPE", "类别");
            ultraWinGridHelper.AddCommonColumn("SPLITUSER", "拆解人员");
            ultraWinGridHelper.AddCommonColumn("SPLITDATE", "拆解时间");

            this.ultraGridSplitItem.DisplayLayout.Bands[0].Columns["NEEDTS"].Width = 100;
            this.ultraGridSplitItem.DisplayLayout.Bands[0].Columns["MCARDTYPE"].Hidden = true;
        }
        //Laws Lu,2006/08/14 加载产品途程列表
        private void InitialItem2Routes(string rcard)
        {
            cmbRoute.Clear();

            if (rcard != null && rcard != String.Empty)
            {
                object sim = (new DataCollectFacade(DataProvider)).GetSimulation(rcard);
                if (sim != null)
                {
                    object[] objItem2Routes = (new ItemFacade(DataProvider)).QueryItem2Route((sim as Simulation).ItemCode, String.Empty, GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString());

                    foreach (Item2Route i2r in objItem2Routes)
                    {
                        if (!cmbRoute.ComboBoxData.Items.Contains(i2r.RouteCode))
                        {
                            cmbRoute.ComboBoxData.Items.Add(i2r.RouteCode);
                        }
                    }
                }
                for (int i = 0; i < cmbRoute.ComboBoxData.Items.Count; i++)
                {
                    if (cmbRoute.ComboBoxData.Items[i].ToString() == ((sim as Simulation).RouteCode))
                    {
                        cmbRoute.ComboBoxData.SelectedIndex = i;
                        break;
                    }
                }

            }
        }

        private void ucLERC_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this._splitTS = this.getTSByRunningCard(this.ucLERC.Value.Trim(), false);

                if (this._splitTS == null)
                {
                    this.ucLCSplitTSStatus.SelectedIndex = 0;
                    this.fillUltraTSSplitItem(null);

                    return;
                }


                //				if ( this._splitTS.FromInputType ==TSFacade.TSSource_TS )
                //				{
                //					this.ShowMessage(new UserControl.Message(MessageType.Error,"$CSError_KeypartsFromTsNotOnLine"));
                //					this.ucLCSplitTSStatus.SelectedIndex = 0;
                //					this.fillUltraTSSplitItem(null);
                //					return;
                //				}

                //Karron Qiu,2005-10-14 ,如果状态为已拆解,则将明细的再修的栏位disable掉
                if (_splitTS.TSStatus == TSStatus.TSStatus_Split)
                {
                    this.ultraGridSplitItem.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                }
                else
                {
                    this.ultraGridSplitItem.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
                }

                this.ucLCSplitTSStatus.SetSelectItem(this._splitTS.TSStatus);

                object[] objItems = this._tsFacade.GetItemsOfRunningCard(
                        this._splitTS.MOCode,
                        this._splitTS.RunningCard,
                        this._splitTS.RunningCardSequence);

                object[] objSplites = this._tsFacade.GetSpliteItems(
                    this._splitTS.RunningCard,
                    this._splitTS.RunningCardSequence,
                    this._splitTS.MOCode);

                if (null != objSplites)
                {
                    foreach (TSSplitItem splite in objSplites)
                    {
                        foreach (ItemOfRunningCard item in objItems)
                        {
                            if (splite.MCard == item.MCARD)
                            {
                                item.ScraptQty = splite.ScrapQty;
                            }
                        }
                    }
                }

                this.fillUltraTSSplitItem(objItems);
            }
        }

        private void ultraGridSplitItem_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            if (e.Cell.Column.Key != "NEEDTS" && e.Cell.Column.Key != "SCRAPQTY")
            {
                e.Cell.CancelUpdate();
            }

            try
            {
                if (e.Cell.Column.Key == "NEEDTS")
                {
                    this.checkNeedTS(e.Cell.Row);
                }

                if (e.Cell.Column.Key == "SCRAPQTY")
                {
                    this.checkScrapQty(e.Cell.Row);
                }
            }
            catch (Exception ex)
            {
                e.Cell.CancelUpdate();

                this.ShowMessage(ex);
            }
        }

        private System.Collections.Hashtable _SelectedErrorCode = new Hashtable();

        private void checkNeedTS(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            if (row.Cells["MCARDTYPE"].Text != MCardType.MCardType_Keyparts)
            {
                throw new Exception("$Error_CS_Only_Keyparts_Can_TS");
            }

            //Karron Qiu dm81606_24
            if (System.Boolean.Parse(row.Cells["NEEDTS"].Text))
            {

                FTSErrorCodeSelect _FTSErrorCodeSelect = new FTSErrorCodeSelect();
                if (_SelectedErrorCode.ContainsKey(row.Cells["MCARD"].Text))
                {
                    _FTSErrorCodeSelect.SelectedErrorCode = (object[])_SelectedErrorCode[row.Cells["MCARD"].Text];
                }

                _FTSErrorCodeSelect.ItemCode = row.Cells["MITEMCODE"].Text;

                _FTSErrorCodeSelect.ShowDialog();

                if (_FTSErrorCodeSelect.SelectedErrorCode != null && _FTSErrorCodeSelect.SelectedErrorCode.Length > 0)
                {
                    if (_SelectedErrorCode.ContainsKey(row.Cells["MCARD"].Text))
                    {
                        _SelectedErrorCode[row.Cells["MCARD"].Text] = _FTSErrorCodeSelect.SelectedErrorCode;
                    }
                    else
                    {
                        _SelectedErrorCode.Add(row.Cells["MCARD"].Text, _FTSErrorCodeSelect.SelectedErrorCode);
                    }
                }
                else
                {
                    row.Cells["NEEDTS"].Value = "False";
                }
            }
            else
            {
                //移除
                if (_SelectedErrorCode.ContainsKey(row.Cells["MCARD"].Text))
                {
                    _SelectedErrorCode.Remove(row.Cells["MCARD"].Text);
                }
            }
        }

        private void checkScrapQty(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            decimal qty = 0;

            try
            {
                qty = System.Int32.Parse(row.Cells["SCRAPQTY"].Text);
            }
            catch (FormatException)
            {
                throw new Exception("$Error_CS_Scrap_Qty_Should_be_Integer");
            }
            catch (OverflowException)
            {
                throw new Exception("$Error_CS_Scrap_Qty_Overflow");
            }

            if (qty < 0)
            {
                throw new Exception("$Error_CS_Scrap_Qty_Should_Over_Zero");
            }

            if (qty > System.Int32.Parse(row.Cells["QTY"].Text))
            {
                throw new Exception("$Error_CS_Scrap_Qty_Should_Below_Item_Qty");
            }
        }

        private void fillUltraTSSplitItem(object[] objs)
        {
            this.ultraGridSplitItem.DataSource = null;

            DataTable dt = new DataTable();
            dt = this.dtSplitItem.Clone();

            this._itemList.Clear();
            dt.Rows.Clear();

            if (objs != null)
            {


                foreach (object obj in objs)
                {
                    object objTS = _tsFacade.GetCardLastTSRecord(((ItemOfRunningCard)obj).MCARD);
                    //_tsFacade.q
                    if (objTS != null && ((Domain.TS.TS)objTS).CardType == CardType.CardType_Part)
                    {
                        ((ItemOfRunningCard)obj).NeedTS = true;
                    }
                    dt.Rows.Add(new object[] { ((ItemOfRunningCard)obj).NeedTS, ((ItemOfRunningCard)obj).ScraptQty, ((ItemOfRunningCard)obj).MItemCode,
																((ItemOfRunningCard)obj).MCARD,
																((ItemOfRunningCard)obj).Qty,
																((ItemOfRunningCard)obj).MCardType,
																ApplicationService.Current().UserCode,
																FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Now))
															});


                    this._itemList.Add(obj);
                    dt.AcceptChanges();
                }

            }

            this.ultraGridSplitItem.DataSource = dt;
        }

        private void ucBtnSplit_Click(object sender, System.EventArgs e)
        {
            if (this._splitTS == null)
            {
                return;
            }

            if (this._itemList.Count == 0)
            {
                return;
            }

            try
            {
                bool checkNeedTS = false;
                //				foreach ( Infragistics.Win.UltraWinGrid.UltraGridRow row in this.ultraGridSplitItem.Rows )
                for (int iGridRowLoopIndex = 0; iGridRowLoopIndex < this.ultraGridSplitItem.Rows.Count; iGridRowLoopIndex++)
                {
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridSplitItem.Rows[iGridRowLoopIndex];
                    checkNeedTS = System.Boolean.Parse(row.Cells["NEEDTS"].Text);
                    if (checkNeedTS) break;
                }
                if (!checkNeedTS)
                {
                    DialogResult result = MessageBox.Show("是否良品拆解不送修", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
                //				foreach ( Infragistics.Win.UltraWinGrid.UltraGridRow row in this.ultraGridSplitItem.Rows )
                for (int iGridRowLoopIndex = 0; iGridRowLoopIndex < this.ultraGridSplitItem.Rows.Count; iGridRowLoopIndex++)
                {
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridSplitItem.Rows[iGridRowLoopIndex];
                    ((ItemOfRunningCard)this._itemList[row.Index]).NeedTS = System.Boolean.Parse(row.Cells["NEEDTS"].Text);
                    ((ItemOfRunningCard)this._itemList[row.Index]).ScraptQty = System.Decimal.Parse(row.Cells["SCRAPQTY"].Text);
                }

                //Laws Lu,2005/11/22,新增	添加代理录入人员
                //modified by jessie lee, 2005/11/24
                UserControl.Messages messages = new Messages();
                string userCode = ApplicationService.Current().UserCode;
                if (txtAgentSpliter.Checked == true && txtAgentSpliter.Value.Trim().Length == 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_Error_Vicegerent_CanNot_Empty"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
                else if (txtAgentSpliter.Checked == true && txtAgentSpliter.Value != String.Empty)
                {
                    if ((new BenQGuru.eMES.Security.SecurityFacade(this.DataProvider)).CheckResourceRight(txtAgentSpliter.Value.Trim().ToUpper()
                        , ApplicationService.Current().ResourceCode))
                    {
                        userCode = txtAgentSpliter.Value;
                    }
                    else
                    {
                        messages.Add(new UserControl.Message(MessageType.Error, "$CS_Error_Vicegerent_Is_Wrong"));
                        ApplicationRun.GetInfoForm().Add(messages);
                        return;
                    }
                }

                this._tsFacade.SplitItem(
                    this._splitTS.TSId,
                    (ItemOfRunningCard[])this._itemList.ToArray(typeof(ItemOfRunningCard)),
                    userCode, ApplicationService.Current().UserCode,
                    ApplicationService.Current().ResourceCode, _SelectedErrorCode);

                this.ShowMessage(new UserControl.Message(MessageType.Success, "$CS_Save_Success"));
                this.ucLCSplitTSStatus.SetSelectItem(TSStatus.TSStatus_Split);
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }
        }
        #endregion

        private void ucLEMNID_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ucLEMNID.SelectAll();
        }

        private void ucLERC_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ucLERC.SelectAll();
        }

        //private Hashtable m_SelectedErrorCode = new Hashtable();
        private object[] m_SelectedErrorCode = null;

        private void checkBoxTS_Click(object sender, System.EventArgs e)
        {
            if (this.TSItemEditStatus == FormStatus.Update &&
                !checkBoxTS.Checked)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_Cannot_Change_NG_To_Good"));//不能从不良品转为良品
                checkBoxTS.Checked = true;
                return;
            }

            if (checkBoxTS.Checked)
            {
                FTSErrorCodeSelect _FTSErrorCodeSelect = new FTSErrorCodeSelect();
                //				if(m_SelectedErrorCode != null )
                //				{
                //					_FTSErrorCodeSelect.SelectedErrorCode = m_SelectedErrorCode;
                //				}

                _FTSErrorCodeSelect.ItemCode = ucLbSourceItemCode.SelectedItemText;//ucLCItemMItem.SelectedItemText;

                _FTSErrorCodeSelect.ShowDialog();

                if (_FTSErrorCodeSelect.SelectedErrorCode != null && _FTSErrorCodeSelect.SelectedErrorCode.Length > 0)
                {
                    m_SelectedErrorCode = _FTSErrorCodeSelect.SelectedErrorCode;
                }
                else
                {
                    checkBoxTS.Checked = false;
                    m_SelectedErrorCode = null;
                }
            }
        }

        private void LoadErrorCauseGroup(string model)
        {
            this.ucLCErrorCauseGroup.Clear();
            this.ucLCErrorCauseGroup.AddItem("", "");

            object[] objs = this._tsModelFacade.QueryModelErrorCauseGroup(model, 0, int.MaxValue);

            if (objs == null)
            {
                return;
            }

            foreach (ErrorCauseGroup errCause in objs)
            {
                this.ucLCErrorCauseGroup.AddItem(errCause.ErrorCauseGroupDescription, errCause.ErrorCauseGroupCode);
            }
        }
        //不良原因组列表
        private void ucLCErrorCauseGroup_Load(object sender, System.EventArgs e)
        {

        }

        //根据不良原因组load这个组下所有的不良原因
        private void ucLCErrorCauseGroup_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.ucLCErrorCause.Clear();
            this.ucLCErrorCause.AddItem("", "");
            string group = null;
            if (this.ucLCErrorCauseGroup.SelectedItemValue != null)
                group = this.ucLCErrorCauseGroup.SelectedItemValue.ToString();

            BenQGuru.eMES.TSModel.TSModelFacade facade = new TSModelFacade(_tsFacade.DataProvider);

            object[] objs = facade.GetSelectedErrorCauseByErrorCauseGroupCode(group);
            if (objs != null)
            {
                foreach (ErrorCause errCause in objs)
                {
                    if (errCause != null)
                    {
                        this.ucLCErrorCause.AddItem(errCause.ErrorCauseDescription, errCause.ErrorCauseCode);
                    }
                }
            }
        }

        //维修完成后界面信息清空
        private void ClearInfoWhenTSSuccess()
        {
            this.ucLEMNID.Text = string.Empty;
            //this.txtAgentUser.Text = string.Empty;
            this.ucLabEdit1.Text = string.Empty;
            this.ucLabEdit2.Text = string.Empty;
            this.ucLabEdit3.Text = string.Empty;

            //this.txtAgentUser.Checked = false;
            this.ultraCheckEditor1.Checked = false;

            this.ucLabCombox2.Clear();
            this.ucLEMNID.TextFocus(false, true);
        }

        private void ucButton2_Click(object sender, System.EventArgs e)
        {
            Messages messages = new Messages();
            if (ucLEMNID.Value.ToUpper().Trim() == String.Empty)
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CSError_RCard_CanNot_Empty"));
                ApplicationRun.GetInfoForm().Add(messages);
                return;
            }

            if (ultraCheckEditor1.Checked)
            {
                if (!(new BaseSetting.BaseModelFacade(DataProvider))
                    .IsOperationInRoute(cmbRoute.ComboBoxData.Text.Trim().ToUpper()
                    , ucLabCombox2.SelectedItemText.Trim().ToUpper()))
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$Error_RouteHasNoOperations"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
            }

            string tsStatus;
            //Laws Lu,2005/11/22,新增	添加代理录入人员
            //modified by jessie lee, 2005/11/24
            string userCode = ApplicationService.Current().UserCode;
            if (txtAgentUser.Checked == true && txtAgentUser.Value.Trim().Length == 0)
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Error_Vicegerent_CanNot_Empty"));
                ApplicationRun.GetInfoForm().Add(messages);
                return;
            }
            else if (txtAgentUser.Checked == true && txtAgentUser.Value != String.Empty)
            {
                if ((new BenQGuru.eMES.Security.SecurityFacade(this.DataProvider)).CheckResourceRight(txtAgentUser.Value.Trim().ToUpper()
                    , ApplicationService.Current().ResourceCode))
                {
                    userCode = txtAgentUser.Value;
                }
                else
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_Error_Vicegerent_Is_Wrong"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
            }

            if (ultraCheckEditor1.Checked == true)
            {
                tsStatus = TSStatus.TSStatus_Reflow;

                //勾选回流，然后不工位栏位未选资料也可允许通过。在业务上回流是一定有确定的工位的。
                //系统要检查此时保存，工位是否为空。
                if (ucLabCombox2.SelectedItemText == string.Empty)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_Error_ReflowOPCode_CanNot_Empty"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    ucLabCombox2.Focus();
                    return;
                }
            }
            else
            {

                tsStatus = TSStatus.TSStatus_Complete;
                //				//修改,Karron Qiu 2005-9-23
                //				//线上采集的不良品，修复后则“回流”；修不好则“报废”。而不选“回流”或“报废”代表该产品要入了良品库，
                //				//线上的不良品修好后是直接回产线回流，不需要入良品库的
                //				messages.Add(new UserControl.Message(MessageType.Error,"$CSError_Please_Select_Reflow_OR_Scrap"));//请选择回流或者报废
                //				ApplicationRun.GetInfoForm().Add(messages);
                //				return;
            }

            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(this.ucLEMNID.Value.Trim().ToUpper(), string.Empty);

            ActionFactory actionFactory = new ActionFactory(this.DataProvider);
            IAction actionTSComplete = actionFactory.CreateAction(ActionType.DataCollectAction_TSComplete);
            TSActionEventArgs actionEventArgs = new TSActionEventArgs(
                ActionType.DataCollectAction_TSComplete,
                FormatHelper.PKCapitalFormat(sourceRCard),
                userCode,
                ApplicationService.Current().ResourceCode,
                tsStatus,
                this.ucLabEdit1.Value,
                this.ucLabEdit2.Value,
                this.ucLabEdit3.Value,
                this.ucLabCombox2.SelectedItemText,
                ApplicationService.Current().UserCode,
                null);

            //Laws Lu,2006/08/14 维修允许选择途程
            actionEventArgs.RouteCode = cmbRoute.ComboBoxData.Text.ToUpper().Trim();

            //修改 Karron Qiu 2005-9-26
            //在做维修完成处理时，依然按照之前的检查逻辑
            //（不良品是否“已选不良零件”或“已选不良位置”有信息），如果没有，则弹出提示信息，
            //比如：“该不良品无“不良零件”或“不良位置”信息，是否要维修完成”，
            //点击“确认”即维修完成，点击“取消”则维修完成失败

            TSFacade tsFacade = new TSFacade(this.DataProvider);
            object obj = tsFacade.GetCardLastTSRecord(actionEventArgs.RunningCard);

            if (obj == null)
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_Not_In_TS"));
                ApplicationRun.GetInfoForm().Add(messages);
                return;
            }
            else
            {
                Domain.TS.TS ts = (Domain.TS.TS)obj;
                if (tsFacade.CheckErrorCodeCountAndErrorSolutionForTSComplete(actionEventArgs.RunningCard))
                {
                    if (!tsFacade.CheckErrorPartAndErrorLocationForTSComplete(actionEventArgs.RunningCard))
                    {
                        if (System.Windows.Forms.MessageBox.Show(null, "该不良品无“不良零件”或“不良位置”信息，是否要维修完成?", "", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_TS_CanNot_Complete $Current_Status $" + ts.TSStatus));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
            }
            //karron qiu ,2005/9/16 ,增加try catch,在catch中添加rollback操作
            DataProvider.BeginTransaction();
            try
            {
                messages.AddMessages(actionTSComplete.Execute(actionEventArgs));
                if (!messages.IsSuccess())
                {
                    this.DataProvider.RollbackTransaction();
                    ApplicationRun.GetInfoForm().Add(messages);
                }
                else
                {
                    this.DataProvider.CommitTransaction();
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_TSComplete_SUCCESS"));
                    this.ClearInfoWhenTSSuccess();
                }
            }
            catch
            {
                this.DataProvider.RollbackTransaction();
                throw;
            }
            finally
            {
                (DataProvider as SQLDomainDataProvider).PersistBroker.CloseConnection();
            }
        }

        private void ultraCheckEditor1_CheckedValueChanged(object sender, System.EventArgs e)
        {
            if (ultraCheckEditor1.Checked == true)
            {
                //				if(ultraCheckEditor2.Checked == true)
                //				{
                //					ultraCheckEditor2.Checked = false ;
                //				}

                Messages messages = new Messages();
                if (ucLEMNID.Value.Trim() == String.Empty)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CSError_RCard_CanNot_Empty"));
                    this.ultraCheckEditor1.Checked = false;
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }

                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                string sourceRCard = dataCollectFacade.GetSourceCard(this.ucLEMNID.Value.Trim().ToUpper(), string.Empty);

                //Laws Lu,2005/09/16,修改	逻辑调整P4.8
                TSFacade tsFacade = new TSFacade(this.DataProvider);
                object obj = tsFacade.GetCardLastTSRecord(sourceRCard);

                if (obj == null)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_Not_In_TS"));

                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
                else
                {
                    Domain.TS.TS ts = (Domain.TS.TS)obj;

                    if (ts.TSStatus == TSStatus.TSStatus_Scrap
                        || ts.TSStatus == TSStatus.TSStatus_Split
                        || ts.TSStatus == TSStatus.TSStatus_Reflow
                        || ts.TSStatus == TSStatus.TSStatus_Confirm)
                    {
                        messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_TSStatus_IsNot_TS $Current_Status $" + ts.TSStatus));

                        ApplicationRun.GetInfoForm().Add(messages);
                        return;
                    }
                    if (ts.TSStatus != TSStatus.TSStatus_TS)
                    {
                        messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_TSStatus_IsNot_TS $Current_Status $" + ts.TSStatus));
                        this.ultraCheckEditor1.Checked = false;
                        ApplicationRun.GetInfoForm().Add(messages);
                        return;
                    }
                    else
                    {
                        SetRaflowPanel(obj);
                    }
                }
                //				object obj = tsFacade.GetCardLastTSRecordInTSStatus(rCardEditor.Text.ToUpper().Trim());
                //				if(obj == null)
                //				{
                //					messages.Add(new UserControl. Message(MessageType.Error,"$CSError_Card_TSStatus_IsNot_TS"));
                //					this.ultraCheckEditor1.Checked = false ;
                //					ApplicationRun.GetInfoForm().Add(messages);
                //					return;
                //				}
                //				else
                //				{
                //					SetRaflowPanel(obj);
                //				}

            }
            else
            {
                ClearReflowPanel();
            }
        }

        private void SetRaflowPanel(object obj)
        {
            ClearReflowPanel();
            this.ucLabEdit1.Value = ((BenQGuru.eMES.Domain.TS.TS)obj).MOCode;
            this.ucLabEdit2.Value = ((BenQGuru.eMES.Domain.TS.TS)obj).ItemCode;
            if (((BenQGuru.eMES.Domain.TS.TS)obj).FromRouteCode != string.Empty)
            {
                this.ucLabEdit3.Value = ((BenQGuru.eMES.Domain.TS.TS)obj).FromRouteCode;
            }
            else
            {
                TSFacade tsFacade = new TSFacade(this.DataProvider);
                BenQGuru.eMES.Domain.DataCollect.Simulation simulation = tsFacade.GetSimulation(((BenQGuru.eMES.Domain.TS.TS)obj).RunningCard, ((BenQGuru.eMES.Domain.TS.TS)obj).MOCode);
                if (simulation == null)
                {
                    Messages message = new Messages();
                    message.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_HasNot_RouteCode"));
                    ApplicationRun.GetInfoForm().Add(message);
                    ClearReflowPanel();
                    this.ultraCheckEditor1.Checked = false;
                    return;
                }
                else
                {
                    this.ucLabEdit3.Value = simulation.FromRoute;
                }
            }

            BenQGuru.eMES.MOModel.ItemFacade itemFacade = new BenQGuru.eMES.MOModel.ItemFacade(this.DataProvider);


            BenQGuru.eMES.Domain.MOModel.Model model = new BenQGuru.eMES.MOModel.ModelFacade(this.DataProvider).GetModelByItemCode(this.ucLabEdit2.Value);
            //如果产品别设定了使用回流，则根据不良原因组找到回流工序，如果没设，则把route下的所有的工序列出来
            if (model == null || model.IsReflow != BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING)
            {
                object[] item2Op = itemFacade.QueryItem2Operation(this.ucLabEdit2.Value, this.ucLabEdit3.Value);
                if (item2Op == null)
                {
                    return;
                }
                else
                {
                    for (int i = 0; i < item2Op.Length; i++)
                    {
                        this.ucLabCombox2.AddItem(((BenQGuru.eMES.Domain.MOModel.ItemRoute2OP)item2Op[i]).OPCode, ((BenQGuru.eMES.Domain.MOModel.ItemRoute2OP)item2Op[i]).OPCode);
                    }
                }
            }
            else
            {
                this.ucLabCombox2.Clear();
                //根据产品，途程，和不良代码组取得回流工序
                string item = this.ucLabEdit2.Value;
                string route = this.ucLabEdit3.Value;

                //找到维修中所有的不良原因组
                TSFacade facade = new TSFacade(this.DataProvider);
                object[] objs = facade.QueryTSErrorCause(((BenQGuru.eMES.Domain.TS.TS)obj).TSId, 1, int.MaxValue);
                if (objs == null)
                {
                    Messages message = new Messages();
                    message.Add(new UserControl.Message(MessageType.Error, "$CSError_CauseGroup_No"));
                    ApplicationRun.GetInfoForm().Add(message);
                    return;
                }

                //把重复的不良原因组去年
                TSModelFacade modelFacade = new TSModelFacade(this.DataProvider);
                ArrayList egList = new ArrayList();
                if (objs.Length > 0)
                {
                    foreach (TSErrorCause errorCase in objs)
                    {
                        if (!egList.Contains(errorCase.ErrorCauseGroupCode))
                            egList.Add(errorCase.ErrorCauseGroupCode);
                    }
                }

                //如果有多个工序可用，则取序号最小的工序
                BenQGuru.eMES.MOModel.ItemFacade opfacade = new BenQGuru.eMES.MOModel.ItemFacade(this.DataProvider);
                string opstr = null;
                int minOpSeq = int.MaxValue;
                for (int i = 0; i < egList.Count; i++)
                {
                    string eg = egList[i] as String;

                    object[] objs2 = modelFacade.QueryItemRouteOp2ErrorCauseGroup(item, route, eg, 1, int.MaxValue);
                    if (objs2 != null && objs2.Length > 0)
                    {
                        BenQGuru.eMES.Domain.TSModel.ItemRouteOp2ErrorCauseGroup ig = objs2[0] as BenQGuru.eMES.Domain.TSModel.ItemRouteOp2ErrorCauseGroup;
                        string opid = ig.OpID;

                        BenQGuru.eMES.Domain.MOModel.ItemRoute2OP op = opfacade.GetItemRoute2Op(opid, GlobalVariables.CurrentOrganizations.First().OrganizationID) as BenQGuru.eMES.Domain.MOModel.ItemRoute2OP;
                        //找到序号最小的ID
                        if (op != null && op.OPSequence < minOpSeq)
                        {
                            minOpSeq = (int)op.OPSequence;
                            opstr = op.OPCode;
                        }
                    }
                }

                if (opstr != null)
                {
                    this.ucLabCombox2.AddItem(opstr, opstr);
                    this.ucLabCombox2.SelectedIndex = 0;
                }

                if (this.ucLabCombox2.ComboBoxData.Items.Count == 0) //如果没有回流工序，则提示用户 1.可能是维修时不良原因组选择错误。2.没有设定产品途程的不良原因的回流工序
                {
                    Messages message = new Messages();
                    message.Add(new UserControl.Message(MessageType.Error, "$CSError_CauseGroup_No"));
                    ApplicationRun.GetInfoForm().Add(message);
                    return;
                }
            }
        }

        private void ClearReflowPanel()
        {
            this.ucLabEdit1.Value = String.Empty;
            this.ucLabEdit2.Value = String.Empty;
            this.ucLabEdit3.Value = String.Empty;
            this.ucLabCombox2.Clear();
        }

        private void ultraTabCtrlTS_Click(object sender, System.EventArgs e)
        {
            if (ultraTabCtrlTS.SelectedTab == ultraTabCtrlTS.Tabs[0])
            {
                ultraTabTSInput.SelectedTab = ultraTabTSInput.Tabs[0];
            }
        }

        private void FTSInputEdit_Activated(object sender, System.EventArgs e)
        {
            ultraTabCtrlTS.SelectedTab = ultraTabCtrlTS.Tabs[0];

            ultraTabTSInput.SelectedTab = ultraTabTSInput.Tabs[0];
            ucLEMNID.TextFocus(false, true);
            //Remove UCLabel.SelectAll;
        }

        private void cmbRoute_CheckBoxCheckedChanged(object sender, System.EventArgs e)
        {
            if (cmbRoute.Checked == true)
            {

            }
        }


        private TSErrorInfo GetSameErrorCodeTSErrorInfo(object[] infos, TSErrorInfo info)
        {
            foreach (TSErrorInfo _info in infos)
            {
                if (_info.ErrorCodeGroup == info.ErrorCodeGroup && _info.ErrorCode == info.ErrorCode)
                {
                    return _info;
                }
            }

            return null;
        }

        private object[] TSInfoOfSameMO;

        private TSErrorCause GetSameErrorCodeTSErrorCause(TSErrorCode ownInfo)
        {
            if (TSInfoOfSameMO == null || TSInfoOfSameMO.Length == 0)
                return null;

            foreach (TSErrorInfo info in TSInfoOfSameMO)
            {
                if (info.ErrorCodeGroup == ownInfo.ErrorCodeGroup && info.ErrorCode == ownInfo.ErrorCode &&
                    info.ErrorCauseList != null && info.ErrorCauseList.Length > 0)
                {
                    return (TSErrorCause)info.ErrorCauseList[0];
                }
            }

            return null;
        }

        private object[] GetSameErrorCodeTSErrorCauses(TSErrorCode ownInfo)
        {
            if (TSInfoOfSameMO == null || TSInfoOfSameMO.Length == 0)
                return new object[] { };

            foreach (TSErrorInfo info in TSInfoOfSameMO)
            {
                if (info.ErrorCodeGroup == ownInfo.ErrorCodeGroup && info.ErrorCode == ownInfo.ErrorCode &&
                    info.ErrorCauseList != null && info.ErrorCauseList.Length > 0)
                {
                    return info.ErrorCauseList;
                }
            }

            return new object[] { };
        }

        private TSErrorCause CopyTSErrorCause(TSErrorCause oldCause, TSErrorCode tsErrorCode)
        {
            TSErrorCause tsErrorCause = null;


            tsErrorCause = this._tsFacade.CreateNewTSErrorCause();
            //TSErrorCode tsErrorCode   = this.currentErrorCode;

            tsErrorCause.TSId = tsErrorCode.TSId;
            tsErrorCause.ErrorCodeGroup = tsErrorCode.ErrorCodeGroup;
            tsErrorCause.ErrorCode = tsErrorCode.ErrorCode;
            tsErrorCause.RunningCard = tsErrorCode.RunningCard;
            tsErrorCause.RunningCardSequence = tsErrorCode.RunningCardSequence;
            tsErrorCause.ModelCode = tsErrorCode.ModelCode;
            tsErrorCause.ItemCode = tsErrorCode.ItemCode;
            tsErrorCause.MOCode = tsErrorCode.MOCode;
            tsErrorCause.MOSeq = tsErrorCode.MOSeq;     // Added by Icyer 2007/07/03


            tsErrorCause.RepairResourceCode = ApplicationService.Current().ResourceCode;
            tsErrorCause.RepairOPCode = OPType.TS;

            tsErrorCause.ErrorCauseGroupCode = oldCause.ErrorCauseGroupCode;//this.ucLCErrorCauseGroup.SelectedItemValue.ToString();
            tsErrorCause.ErrorCauseCode = oldCause.ErrorCauseCode;//this.ucLCErrorCause.SelectedItemValue.ToString();
            tsErrorCause.DutyCode = oldCause.DutyCode;//this.ucLCDuty.SelectedItemValue.ToString();
            tsErrorCause.SolutionCode = oldCause.SolutionCode;//this.ucLCSolution.SelectedItemValue.ToString();
            tsErrorCause.SolutionMEMO = oldCause.SolutionMEMO;//this.ucLESolutionMemo.Value.Trim();

            //Laws Lu,2005/11/22,新增	添加代理录入人员
            //modified by jessie lee, 2005/11/24
            UserControl.Messages messages = new Messages();
            string userCode = ApplicationService.Current().UserCode;
            if (txtAgentUser.Checked == true && txtAgentUser.Value.Trim().Length == 0)
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Error_Vicegerent_CanNot_Empty"));
                ApplicationRun.GetInfoForm().Add(messages);
                return null;
            }
            else if (txtAgentUser.Checked == true && txtAgentUser.Value != String.Empty)
            {
                if ((new BenQGuru.eMES.Security.SecurityFacade(this.DataProvider)).CheckResourceRight(txtAgentUser.Value.Trim().ToUpper()
                    , ApplicationService.Current().ResourceCode))
                {
                    userCode = txtAgentUser.Value;
                }
                else
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_Error_Vicegerent_Is_Wrong"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return null;
                }
            }

            tsErrorCause.MaintainUser = userCode;
            tsErrorCause.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
            tsErrorCause.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);

            return tsErrorCause;
        }

        private void GetTSInfoOfSameMO()
        {
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(this.ucLEMNID.Value.Trim().ToUpper(), string.Empty);
            BenQGuru.eMES.Domain.TS.TS otherTS = (BenQGuru.eMES.Domain.TS.TS)this._tsFacade.GetTSInfoOfSameMO(
                sourceRCard, this._currentTS, this.owninfos);

            if (otherTS == null)
                return;

            // 获得TS下所有的ErrorCode和ErrorCause
            object[] infos = this._tsFacade.GetTSInfoByTS(otherTS);

            TSInfoOfSameMO = infos;

            if (this.ultraTreeRunningCard.Nodes.Count == 0)
                return;

            //复制同一工单中已经维修完成的维修信息,并直接保存.

            //Laws Lu,2005/11/09,新增	记录ShiftDay
            BaseSetting.BaseModelFacade dataModel = new BaseSetting.BaseModelFacade(this.DataProvider);
            Domain.BaseSetting.Resource res = (Domain.BaseSetting.Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);
            //onwip.SegmentCode				= productInfo.NowSimulationReport.SegmentCode;

            BaseSetting.ShiftModelFacade shiftModel = new BaseSetting.ShiftModelFacade(this.DataProvider);
            Domain.BaseSetting.TimePeriod period = (Domain.BaseSetting.TimePeriod)shiftModel.GetTimePeriod(res.ShiftTypeCode, Web.Helper.FormatHelper.TOTimeInt(DateTime.Now));
            if (period == null)
            {
                throw new Exception("$OutOfPerid");
            }

            Infragistics.Win.UltraWinTree.UltraTreeNode topNode = this.ultraTreeRunningCard.Nodes[0];

            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            this.DataProvider.BeginTransaction();

            try
            {

                #region DO
                foreach (Infragistics.Win.UltraWinTree.UltraTreeNode node in topNode.Nodes)
                {
                    TSErrorCode ownInfo = node.Tag as TSErrorCode;

                    //如果已经有解决方案则不复制.
                    if (node.Nodes.Count != 0)
                        continue;

                    object[] ErrorCauseLists = GetSameErrorCodeTSErrorCauses(ownInfo);

                    if (ErrorCauseLists == null)
                        continue;

                    foreach (TSErrorCause errCause in ErrorCauseLists)
                    {
                        UltraTreeNode errCauseNode = new UltraTreeNode(node.Text + ":" + errCause.ErrorCauseGroupCode + ":" + errCause.ErrorCauseCode, errCause.ErrorCauseGroupCode + ":" + errCause.ErrorCauseCode);

                        errCause.MaintainUser = ApplicationService.Current().UserCode;
                        errCauseNode.Tag = errCause;

                        node.Nodes.Add(errCauseNode);

                        #region 保存TSErrorCause

                        TSErrorCause newErrCause = CopyTSErrorCause(errCause, ownInfo);

                        if (newErrCause == null)
                        {
                            continue;
                        }


                        #region 记录ShiftDay
                        if (period.IsOverDate == Web.Helper.FormatHelper.TRUE_STRING)
                        {
                            if (period.TimePeriodBeginTime < period.TimePeriodEndTime)
                            {
                                newErrCause.TSShiftDay = FormatHelper.TODateInt(DateTime.Now.AddDays(-1));
                            }
                            else if (Web.Helper.FormatHelper.TOTimeInt(DateTime.Now) < period.TimePeriodBeginTime)
                            {
                                newErrCause.TSShiftDay = FormatHelper.TODateInt(DateTime.Now.AddDays(-1));
                            }
                            else
                            {
                                newErrCause.TSShiftDay = FormatHelper.TODateInt(DateTime.Now);
                            }
                        }
                        else
                        {
                            newErrCause.TSShiftDay = FormatHelper.TODateInt(DateTime.Now);
                        }
                        #endregion

                        try
                        {
                            this._tsFacade.AddTSErrorCause(ApplicationService.Current().UserCode, newErrCause);

                            #region 保存不良位置

                            // 已选不良位置,不良零件
                            object[] objs = this._tsFacade.GetTSErrorCause2Location(otherTS.TSId, errCause.ErrorCodeGroup, errCause.ErrorCode, errCause.ErrorCauseCode);

                            this._locationHelper.Clear();

                            if (objs != null)
                            {
                                foreach (TSErrorCause2Location location in objs)
                                {
                                    this._locationHelper.Add(location.ErrorLocation);

                                }
                            }

                            // 已选不良零件
                            objs = this._tsFacade.GetTSErrorCause2ErrorPart(this._currentTS.TSId, errCause.ErrorCodeGroup, errCause.ErrorCode, errCause.ErrorCauseCode);

                            this._partHelper.Clear();
                            if (objs != null)
                            {
                                foreach (TSErrorCause2ErrorPart part in objs)
                                {
                                    //this.lstSelectedErrorPart.Items.Add( part.ErrorPart );
                                    this._partHelper.Add(part.ErrorPart);
                                }
                            }

                            if (this._locationHelper.AddList.Length > 0)
                            {
                                this._tsFacade.AddTSErrorCause2Location(newErrCause, this._locationHelper.AddList);
                            }

                            if (this._partHelper.AddList.Length > 0)
                            {
                                this._tsFacade.AddTSErrorCause2ErrorPart(newErrCause, this._partHelper.AddList);
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message != "")//如果是重复添加则隐藏异常.
                                throw;
                        }
                        #endregion

                    }
                }
                #endregion


                this.DataProvider.CommitTransaction();

                this._locationHelper.Clear();
                this._partHelper.Clear();
                this.ShowMessage(new UserControl.Message(MessageType.Success, "$CS_Save_Success"));
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();

                this.ShowMessage(ex);
                return;
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            }
        }
    }



}
