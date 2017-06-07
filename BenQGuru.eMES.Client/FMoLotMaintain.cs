using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.DataCollect;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Warehouse;
using Infragistics.Win.UltraWinGrid;
using BenQGuru.eMES.CodeSoftPrint;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.LotDataCollect;
using BenQGuru.eMES.Domain.TS;
using System.Collections.Generic;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.LotDataCollect;
using Infragistics.Win;


namespace BenQGuru.eMES.Client
{
    /// <summary>
    /// FMoLotMaintain 的摘要说明。
    /// </summary>
    public class FMoLotMaintain : BaseForm
    {
        private string type = "";
        private string savedLotNo = "";
        private string savedNewLotNo = "";
        private decimal lotQty = 0;
        private decimal ngQty = 0;
        private decimal goodQty = 0;
        private bool saveOldLotInfo = false;
        private bool isNG = false;
        private bool _IsBatchPrint = true;
        private string _DataDescFileName = "Label.dsc";
        public PrintTemplate[] _PrintTemplateList = null;
        private Hashtable ht = new Hashtable();
        private DataSet m_CheckHeadList = null;
        private DataSet m_CheckFooderList = null;
        private DataTable m_LotHead = null;
        private DataTable m_LotFooder = null;
        private MOLotFacade _MOLotFacade = null;
        private BenQGuru.eMES.LotDataCollect.DataCollectFacade _DataCollectFacade = null;
        private ItemFacade _ItemFacade = null;
        private MOFacade _MOFacade = null;
        private TSFacade _TSFacade = null;

        private string itemcode = string.Empty;
        public string ItemCode
        {
            get
            {
                return itemcode;
            }
            set
            {
                itemcode = value;
            }
        }

        private string firstletter = string.Empty;
        public string FirstLetter
        {
            get
            {
                return firstletter;
            }
            set
            {
                firstletter = value;
            }
        }

        public UserControl.UCLabelEdit uclMoCode;
        public UserControl.UCLabelEdit uclItemCode;
        private UserControl.UCButton ucbCancel;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private UserControl.UCLabelEdit uclLotNo;
        private Button button_MaterialCode;
        private Panel panel3;
        private UCButton ucButton_OpenLot;
        private UCButton ucButton_MerageLot;
        private UCButton btnSave;
        public UCLabelCombox ucLabelComboxPrintTemplete;
        public UCLabelCombox ucLabelComboxPrintList;
        private UCLabelEdit ucLabelEditPrintCount;
        public UCButton ucButtonPrint;
        private UCButton ucButtonQuery;
        private UltraGrid ultraGridFooder;
        private UltraGrid ultraGridHead;
        public UCLabelEdit ucLSaveOld;
        private UCButton ucButtonExist;
        private CheckBox checkBoxOnlyValid;
        private IContainer components;

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FMoLotMaintain()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            UserControl.UIStyleBuilder.FormUI(this);

            //this.ultraGridHead.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White;;
            //this.ultraGridHead.DisplayLayout.CaptionAppearance.BackColor =Color.FromName("WhiteSmoke");
            //this.ultraGridHead.DisplayLayout.Appearance.BackColor=Color.FromArgb(255, 255, 255);
            //this.ultraGridHead.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridHead.DisplayLayout.Override.RowAppearance.BackColor =Color.FromArgb(230, 234, 245);
            this.ultraGridHead.DisplayLayout.Override.RowAlternateAppearance.BackColor=Color.FromArgb(255, 255, 255);
            //this.ultraGridHead.DisplayLayout.Override.RowSelectors =Infragistics.Win.DefaultableBoolean.False;
            //this.ultraGridHead.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            //this.ultraGridHead.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            //this.ultraGridHead.DisplayLayout.ScrollBarLook.Appearance.BackColor =Color.FromName("LightGray");

            InitializeMainGrid();
        }

        private void FMoLotMaintain_Load(object sender, EventArgs e)
        {
            LoadPrinter();
            LoadTemplateList();
            _MOLotFacade = new MOLotFacade(this.DataProvider);
            _MOFacade = new MOFacade(this.DataProvider);
            _ItemFacade = new ItemFacade(this.DataProvider);
            _DataCollectFacade = new BenQGuru.eMES.LotDataCollect.DataCollectFacade(this.DataProvider);
            _TSFacade = new TSFacade(this.DataProvider);

            //this.InitPageLanguage();
            //this.InitGridLanguage(this.ultraGridHead);
            //this.InitGridLanguage(this.ultraGridFooder);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMoLotMaintain));
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
            this.uclMoCode = new UserControl.UCLabelEdit();
            this.uclItemCode = new UserControl.UCLabelEdit();
            this.ucbCancel = new UserControl.UCButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxOnlyValid = new System.Windows.Forms.CheckBox();
            this.ucButtonQuery = new UserControl.UCButton();
            this.button_MaterialCode = new System.Windows.Forms.Button();
            this.uclLotNo = new UserControl.UCLabelEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucButtonExist = new UserControl.UCButton();
            this.ucButtonPrint = new UserControl.UCButton();
            this.ucLabelComboxPrintTemplete = new UserControl.UCLabelCombox();
            this.ucLabelComboxPrintList = new UserControl.UCLabelCombox();
            this.ucLabelEditPrintCount = new UserControl.UCLabelEdit();
            this.btnSave = new UserControl.UCButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ucLSaveOld = new UserControl.UCLabelEdit();
            this.ultraGridFooder = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGridHead = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ucButton_OpenLot = new UserControl.UCButton();
            this.ucButton_MerageLot = new UserControl.UCButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridFooder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridHead)).BeginInit();
            this.SuspendLayout();
            // 
            // uclMoCode
            // 
            this.uclMoCode.AllowEditOnlyChecked = true;
            this.uclMoCode.AutoSelectAll = false;
            this.uclMoCode.AutoUpper = true;
            this.uclMoCode.Caption = "工单代码";
            this.uclMoCode.Checked = false;
            this.uclMoCode.EditType = UserControl.EditTypes.String;
            this.uclMoCode.Location = new System.Drawing.Point(8, 7);
            this.uclMoCode.MaxLength = 40;
            this.uclMoCode.Multiline = false;
            this.uclMoCode.Name = "uclMoCode";
            this.uclMoCode.PasswordChar = '\0';
            this.uclMoCode.ReadOnly = false;
            this.uclMoCode.ShowCheckBox = false;
            this.uclMoCode.Size = new System.Drawing.Size(194, 23);
            this.uclMoCode.TabIndex = 0;
            this.uclMoCode.TabNext = true;
            this.uclMoCode.Value = "";
            this.uclMoCode.WidthType = UserControl.WidthTypes.Normal;
            this.uclMoCode.XAlign = 69;
            // 
            // uclItemCode
            // 
            this.uclItemCode.AllowEditOnlyChecked = true;
            this.uclItemCode.AutoSelectAll = false;
            this.uclItemCode.AutoUpper = true;
            this.uclItemCode.Caption = "产品代码";
            this.uclItemCode.Checked = false;
            this.uclItemCode.EditType = UserControl.EditTypes.String;
            this.uclItemCode.Location = new System.Drawing.Point(207, 7);
            this.uclItemCode.MaxLength = 60;
            this.uclItemCode.Multiline = false;
            this.uclItemCode.Name = "uclItemCode";
            this.uclItemCode.PasswordChar = '\0';
            this.uclItemCode.ReadOnly = false;
            this.uclItemCode.ShowCheckBox = false;
            this.uclItemCode.Size = new System.Drawing.Size(194, 23);
            this.uclItemCode.TabIndex = 1;
            this.uclItemCode.TabNext = true;
            this.uclItemCode.Value = "";
            this.uclItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.uclItemCode.XAlign = 268;
            // 
            // ucbCancel
            // 
            this.ucbCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucbCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucbCancel.BackgroundImage")));
            this.ucbCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.ucbCancel.Caption = "取消";
            this.ucbCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucbCancel.Location = new System.Drawing.Point(467, 60);
            this.ucbCancel.Name = "ucbCancel";
            this.ucbCancel.Size = new System.Drawing.Size(88, 22);
            this.ucbCancel.TabIndex = 13;
            this.ucbCancel.Click += new System.EventHandler(this.ucbCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxOnlyValid);
            this.panel1.Controls.Add(this.ucButtonQuery);
            this.panel1.Controls.Add(this.button_MaterialCode);
            this.panel1.Controls.Add(this.uclLotNo);
            this.panel1.Controls.Add(this.uclMoCode);
            this.panel1.Controls.Add(this.uclItemCode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1023, 39);
            this.panel1.TabIndex = 20;
            // 
            // checkBoxOnlyValid
            // 
            this.checkBoxOnlyValid.AutoSize = true;
            this.checkBoxOnlyValid.Checked = true;
            this.checkBoxOnlyValid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOnlyValid.Location = new System.Drawing.Point(641, 9);
            this.checkBoxOnlyValid.Name = "checkBoxOnlyValid";
            this.checkBoxOnlyValid.Size = new System.Drawing.Size(84, 16);
            this.checkBoxOnlyValid.TabIndex = 9;
            this.checkBoxOnlyValid.Text = "只显示有效";
            this.checkBoxOnlyValid.UseVisualStyleBackColor = true;
            // 
            // ucButtonQuery
            // 
            this.ucButtonQuery.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonQuery.BackgroundImage")));
            this.ucButtonQuery.ButtonType = UserControl.ButtonTypes.Query;
            this.ucButtonQuery.Caption = "查询";
            this.ucButtonQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonQuery.Location = new System.Drawing.Point(724, 7);
            this.ucButtonQuery.Name = "ucButtonQuery";
            this.ucButtonQuery.Size = new System.Drawing.Size(88, 22);
            this.ucButtonQuery.TabIndex = 8;
            this.ucButtonQuery.Click += new System.EventHandler(this.ucButtonQuery_Click);
            // 
            // button_MaterialCode
            // 
            this.button_MaterialCode.Location = new System.Drawing.Point(405, 7);
            this.button_MaterialCode.Name = "button_MaterialCode";
            this.button_MaterialCode.Size = new System.Drawing.Size(35, 23);
            this.button_MaterialCode.TabIndex = 4;
            this.button_MaterialCode.Text = "...";
            this.button_MaterialCode.UseVisualStyleBackColor = true;
            this.button_MaterialCode.Click += new System.EventHandler(this.button_ItemCode_Click);
            // 
            // uclLotNo
            // 
            this.uclLotNo.AllowEditOnlyChecked = true;
            this.uclLotNo.AutoSelectAll = false;
            this.uclLotNo.AutoUpper = true;
            this.uclLotNo.Caption = "批次条码";
            this.uclLotNo.Checked = false;
            this.uclLotNo.EditType = UserControl.EditTypes.String;
            this.uclLotNo.Location = new System.Drawing.Point(444, 7);
            this.uclLotNo.MaxLength = 60;
            this.uclLotNo.Multiline = false;
            this.uclLotNo.Name = "uclLotNo";
            this.uclLotNo.PasswordChar = '\0';
            this.uclLotNo.ReadOnly = false;
            this.uclLotNo.ShowCheckBox = false;
            this.uclLotNo.Size = new System.Drawing.Size(194, 23);
            this.uclLotNo.TabIndex = 2;
            this.uclLotNo.TabNext = true;
            this.uclLotNo.Value = "";
            this.uclLotNo.WidthType = UserControl.WidthTypes.Normal;
            this.uclLotNo.XAlign = 505;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucButtonExist);
            this.panel2.Controls.Add(this.ucButtonPrint);
            this.panel2.Controls.Add(this.ucLabelComboxPrintTemplete);
            this.panel2.Controls.Add(this.ucLabelComboxPrintList);
            this.panel2.Controls.Add(this.ucLabelEditPrintCount);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.ucbCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 477);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1023, 94);
            this.panel2.TabIndex = 21;
            // 
            // ucButtonExist
            // 
            this.ucButtonExist.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExist.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExist.BackgroundImage")));
            this.ucButtonExist.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExist.Caption = "退出";
            this.ucButtonExist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExist.Location = new System.Drawing.Point(572, 60);
            this.ucButtonExist.Name = "ucButtonExist";
            this.ucButtonExist.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExist.TabIndex = 55;
            // 
            // ucButtonPrint
            // 
            this.ucButtonPrint.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonPrint.BackgroundImage")));
            this.ucButtonPrint.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonPrint.Caption = "打印";
            this.ucButtonPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonPrint.Location = new System.Drawing.Point(722, 14);
            this.ucButtonPrint.Name = "ucButtonPrint";
            this.ucButtonPrint.Size = new System.Drawing.Size(88, 22);
            this.ucButtonPrint.TabIndex = 54;
            this.ucButtonPrint.Click += new System.EventHandler(this.ucButtonPrint_Click);
            // 
            // ucLabelComboxPrintTemplete
            // 
            this.ucLabelComboxPrintTemplete.AllowEditOnlyChecked = true;
            this.ucLabelComboxPrintTemplete.Caption = "打印模板";
            this.ucLabelComboxPrintTemplete.Checked = false;
            this.ucLabelComboxPrintTemplete.Location = new System.Drawing.Point(509, 16);
            this.ucLabelComboxPrintTemplete.Name = "ucLabelComboxPrintTemplete";
            this.ucLabelComboxPrintTemplete.SelectedIndex = -1;
            this.ucLabelComboxPrintTemplete.ShowCheckBox = false;
            this.ucLabelComboxPrintTemplete.Size = new System.Drawing.Size(194, 20);
            this.ucLabelComboxPrintTemplete.TabIndex = 52;
            this.ucLabelComboxPrintTemplete.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxPrintTemplete.XAlign = 570;
            // 
            // ucLabelComboxPrintList
            // 
            this.ucLabelComboxPrintList.AllowEditOnlyChecked = true;
            this.ucLabelComboxPrintList.Caption = "打印机列表";
            this.ucLabelComboxPrintList.Checked = false;
            this.ucLabelComboxPrintList.Location = new System.Drawing.Point(299, 16);
            this.ucLabelComboxPrintList.Name = "ucLabelComboxPrintList";
            this.ucLabelComboxPrintList.SelectedIndex = -1;
            this.ucLabelComboxPrintList.ShowCheckBox = false;
            this.ucLabelComboxPrintList.Size = new System.Drawing.Size(206, 20);
            this.ucLabelComboxPrintList.TabIndex = 53;
            this.ucLabelComboxPrintList.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxPrintList.XAlign = 372;
            // 
            // ucLabelEditPrintCount
            // 
            this.ucLabelEditPrintCount.AllowEditOnlyChecked = true;
            this.ucLabelEditPrintCount.AutoSelectAll = false;
            this.ucLabelEditPrintCount.AutoUpper = true;
            this.ucLabelEditPrintCount.Caption = "打印数量";
            this.ucLabelEditPrintCount.Checked = false;
            this.ucLabelEditPrintCount.EditType = UserControl.EditTypes.Integer;
            this.ucLabelEditPrintCount.Location = new System.Drawing.Point(133, 16);
            this.ucLabelEditPrintCount.MaxLength = 8;
            this.ucLabelEditPrintCount.Multiline = false;
            this.ucLabelEditPrintCount.Name = "ucLabelEditPrintCount";
            this.ucLabelEditPrintCount.PasswordChar = '\0';
            this.ucLabelEditPrintCount.ReadOnly = false;
            this.ucLabelEditPrintCount.ShowCheckBox = false;
            this.ucLabelEditPrintCount.Size = new System.Drawing.Size(161, 24);
            this.ucLabelEditPrintCount.TabIndex = 51;
            this.ucLabelEditPrintCount.TabNext = false;
            this.ucLabelEditPrintCount.Tag = "";
            this.ucLabelEditPrintCount.Value = "1";
            this.ucLabelEditPrintCount.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditPrintCount.XAlign = 194;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.ButtonType = UserControl.ButtonTypes.Save;
            this.btnSave.Caption = "保存";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(362, 60);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 22);
            this.btnSave.TabIndex = 21;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.ucLSaveOld);
            this.panel3.Controls.Add(this.ultraGridFooder);
            this.panel3.Controls.Add(this.ultraGridHead);
            this.panel3.Controls.Add(this.ucButton_OpenLot);
            this.panel3.Controls.Add(this.ucButton_MerageLot);
            this.panel3.Location = new System.Drawing.Point(0, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1023, 440);
            this.panel3.TabIndex = 22;
            // 
            // ucLSaveOld
            // 
            this.ucLSaveOld.AllowEditOnlyChecked = true;
            this.ucLSaveOld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLSaveOld.AutoSelectAll = false;
            this.ucLSaveOld.AutoUpper = true;
            this.ucLSaveOld.Caption = "保留旧批";
            this.ucLSaveOld.Checked = false;
            this.ucLSaveOld.EditType = UserControl.EditTypes.String;
            this.ucLSaveOld.Location = new System.Drawing.Point(54, 203);
            this.ucLSaveOld.MaxLength = 40;
            this.ucLSaveOld.Multiline = false;
            this.ucLSaveOld.Name = "ucLSaveOld";
            this.ucLSaveOld.PasswordChar = '\0';
            this.ucLSaveOld.ReadOnly = false;
            this.ucLSaveOld.ShowCheckBox = true;
            this.ucLSaveOld.Size = new System.Drawing.Size(210, 23);
            this.ucLSaveOld.TabIndex = 27;
            this.ucLSaveOld.TabNext = true;
            this.ucLSaveOld.Value = "";
            this.ucLSaveOld.WidthType = UserControl.WidthTypes.Normal;
            this.ucLSaveOld.XAlign = 131;
            // 
            // ultraGridFooder
            // 
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGridFooder.DisplayLayout.Appearance = appearance4;
            this.ultraGridFooder.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridFooder.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance1.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridFooder.DisplayLayout.GroupByBox.Appearance = appearance1;
            appearance2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridFooder.DisplayLayout.GroupByBox.BandLabelAppearance = appearance2;
            this.ultraGridFooder.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackColor2 = System.Drawing.SystemColors.Control;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridFooder.DisplayLayout.GroupByBox.PromptAppearance = appearance3;
            this.ultraGridFooder.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGridFooder.DisplayLayout.MaxRowScrollRegions = 1;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGridFooder.DisplayLayout.Override.ActiveCellAppearance = appearance12;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGridFooder.DisplayLayout.Override.ActiveRowAppearance = appearance7;
            this.ultraGridFooder.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGridFooder.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGridFooder.DisplayLayout.Override.CardAreaAppearance = appearance6;
            appearance5.BorderColor = System.Drawing.Color.Silver;
            appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGridFooder.DisplayLayout.Override.CellAppearance = appearance5;
            this.ultraGridFooder.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGridFooder.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridFooder.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance11.TextHAlignAsString = "Left";
            this.ultraGridFooder.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.ultraGridFooder.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGridFooder.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            this.ultraGridFooder.DisplayLayout.Override.RowAppearance = appearance10;
            this.ultraGridFooder.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGridFooder.DisplayLayout.Override.TemplateAddRowAppearance = appearance8;
            this.ultraGridFooder.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGridFooder.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGridFooder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ultraGridFooder.Location = new System.Drawing.Point(0, 230);
            this.ultraGridFooder.Name = "ultraGridFooder";
            this.ultraGridFooder.Size = new System.Drawing.Size(1023, 210);
            this.ultraGridFooder.TabIndex = 26;
            this.ultraGridFooder.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ultraGridFooder_InitializeRow);
            this.ultraGridFooder.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridFooder_InitializeLayout);
            // 
            // ultraGridHead
            // 
            this.ultraGridHead.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance27.BackColor = System.Drawing.SystemColors.Window;
            appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGridHead.DisplayLayout.Appearance = appearance27;
            this.ultraGridHead.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridHead.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance28.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridHead.DisplayLayout.GroupByBox.Appearance = appearance28;
            appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridHead.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
            this.ultraGridHead.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance30.BackColor2 = System.Drawing.SystemColors.Control;
            appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridHead.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
            this.ultraGridHead.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGridHead.DisplayLayout.MaxRowScrollRegions = 1;
            appearance31.BackColor = System.Drawing.SystemColors.Window;
            appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGridHead.DisplayLayout.Override.ActiveCellAppearance = appearance31;
            appearance32.BackColor = System.Drawing.SystemColors.Highlight;
            appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGridHead.DisplayLayout.Override.ActiveRowAppearance = appearance32;
            this.ultraGridHead.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGridHead.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance33.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGridHead.DisplayLayout.Override.CardAreaAppearance = appearance33;
            appearance34.BorderColor = System.Drawing.Color.Silver;
            appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGridHead.DisplayLayout.Override.CellAppearance = appearance34;
            this.ultraGridHead.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGridHead.DisplayLayout.Override.CellPadding = 0;
            appearance35.BackColor = System.Drawing.SystemColors.Control;
            appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance35.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridHead.DisplayLayout.Override.GroupByRowAppearance = appearance35;
            appearance36.TextHAlignAsString = "Left";
            this.ultraGridHead.DisplayLayout.Override.HeaderAppearance = appearance36;
            this.ultraGridHead.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGridHead.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance37.BackColor = System.Drawing.SystemColors.Window;
            appearance37.BorderColor = System.Drawing.Color.Silver;
            this.ultraGridHead.DisplayLayout.Override.RowAppearance = appearance37;
            this.ultraGridHead.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGridHead.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
            this.ultraGridHead.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGridHead.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGridHead.Location = new System.Drawing.Point(0, 0);
            this.ultraGridHead.Name = "ultraGridHead";
            this.ultraGridHead.Size = new System.Drawing.Size(1023, 196);
            this.ultraGridHead.TabIndex = 25;
            this.ultraGridHead.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridHead_InitializeLayout);
            this.ultraGridHead.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridHead_CellChange);
            // 
            // ucButton_OpenLot
            // 
            this.ucButton_OpenLot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButton_OpenLot.BackColor = System.Drawing.SystemColors.Control;
            this.ucButton_OpenLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton_OpenLot.BackgroundImage")));
            this.ucButton_OpenLot.ButtonType = UserControl.ButtonTypes.None;
            this.ucButton_OpenLot.Caption = "分批";
            this.ucButton_OpenLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButton_OpenLot.Location = new System.Drawing.Point(396, 202);
            this.ucButton_OpenLot.Name = "ucButton_OpenLot";
            this.ucButton_OpenLot.Size = new System.Drawing.Size(88, 22);
            this.ucButton_OpenLot.TabIndex = 22;
            this.ucButton_OpenLot.Click += new System.EventHandler(this.ucButton_OpenLot_Click);
            // 
            // ucButton_MerageLot
            // 
            this.ucButton_MerageLot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButton_MerageLot.BackColor = System.Drawing.SystemColors.Control;
            this.ucButton_MerageLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton_MerageLot.BackgroundImage")));
            this.ucButton_MerageLot.ButtonType = UserControl.ButtonTypes.None;
            this.ucButton_MerageLot.Caption = "合批";
            this.ucButton_MerageLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButton_MerageLot.Location = new System.Drawing.Point(539, 202);
            this.ucButton_MerageLot.Name = "ucButton_MerageLot";
            this.ucButton_MerageLot.Size = new System.Drawing.Size(88, 22);
            this.ucButton_MerageLot.TabIndex = 21;
            this.ucButton_MerageLot.Click += new System.EventHandler(this.ucButton_MerageLot_Click);
            // 
            // FMoLotMaintain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1023, 571);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FMoLotMaintain";
            this.Text = "工单分批合批";
            this.Load += new System.EventHandler(this.FMoLotMaintain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridFooder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridHead)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region 初始化Grid样式以及清空Grid
        private void ultraGridHead_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            //e.Layout.MaxBandDepth = 1;
            // 自动判断是否显示前面的+、-号
            e.Layout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            e.Layout.Override.MaxSelectedRows = 2;
            // 冻结列
            e.Layout.UseFixedHeaders = true;
            e.Layout.Override.FixedHeaderAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedHeaderAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedCellAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedCellAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;

            // 排序
            //e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "LotNO";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["MOCode"].Header.Caption = "工单代码";
            e.Layout.Bands[0].Columns["ItemCode"].Header.Caption = "产品代码";
            e.Layout.Bands[0].Columns["LotNo"].Header.Caption = "批次条码";
            e.Layout.Bands[0].Columns["LotQty"].Header.Caption = "批次数量";
            e.Layout.Bands[0].Columns["GoodQty"].Header.Caption = "良品数量";
            e.Layout.Bands[0].Columns["NGQty"].Header.Caption = "不良数量";
            e.Layout.Bands[0].Columns["LotStatus"].Header.Caption = "批次状态";
            e.Layout.Bands[0].Columns["LotStatusHidden"].Header.Caption = "批次状态";
            e.Layout.Bands[0].Columns["ProductStatus"].Header.Caption = "生产状态";
            e.Layout.Bands[0].Columns["RouteCode"].Header.Caption = "途程代码";
            e.Layout.Bands[0].Columns["OPCode"].Header.Caption = "工序代码";
            e.Layout.Bands[0].Columns["CollectStatus"].Header.Caption = "是否采集中";
            e.Layout.Bands[0].Columns["CollectStatusHidden"].Header.Caption = "是否采集中";
            e.Layout.Bands[0].Columns["IsCom"].Header.Caption = "是否完工";
            e.Layout.Bands[0].Columns["IsComHidden"].Header.Caption = "是否完工";
            e.Layout.Bands[0].Columns["MaintainDate"].Header.Caption = "维护日期";
            e.Layout.Bands[0].Columns["MaintainTime"].Header.Caption = "维护时间";

            e.Layout.Bands[0].Columns["Checked"].Width = 40;
            e.Layout.Bands[0].Columns["MOCode"].Width = 100;
            e.Layout.Bands[0].Columns["ItemCode"].Width = 60;
            e.Layout.Bands[0].Columns["LotNo"].Width = 100;
            e.Layout.Bands[0].Columns["LotQty"].Width = 60;
            e.Layout.Bands[0].Columns["GoodQty"].Width = 60;
            e.Layout.Bands[0].Columns["NGQty"].Width = 60;
            e.Layout.Bands[0].Columns["LotStatus"].Width = 60;
            e.Layout.Bands[0].Columns["ProductStatus"].Width = 60;
            e.Layout.Bands[0].Columns["RouteCode"].Width = 70;
            e.Layout.Bands[0].Columns["OPCode"].Width = 70;
            e.Layout.Bands[0].Columns["CollectStatus"].Width = 60;
            e.Layout.Bands[0].Columns["IsCom"].Width = 60;
            e.Layout.Bands[0].Columns["MaintainDate"].Width = 90;
            e.Layout.Bands[0].Columns["MaintainTime"].Width = 90;

            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["LotNo"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["MOCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["ItemCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["LotQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["GoodQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["NGQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["LotStatus"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["ProductStatus"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["RouteCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["OPCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["CollectStatus"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["IsCom"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MaintainDate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MaintainTime"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            //设置可编辑列的颜色
            //e.Layout.Bands[0].Columns["DATECODE"].CellAppearance.BackColor = Color.LawnGreen;
            //e.Layout.Bands[0].Columns["VenderLotNO"].CellAppearance.BackColor = Color.LawnGreen ;
            //e.Layout.Bands[0].Columns["VenderITEMCODE"].CellAppearance.BackColor = Color.LawnGreen;

            //设置隐藏列
            e.Layout.Bands[0].Columns["LotStatusHidden"].Hidden = true;
            e.Layout.Bands[0].Columns["CollectStatusHidden"].Hidden = true;
            e.Layout.Bands[0].Columns["IsComHidden"].Hidden = true;

            // 允许筛选
            //e.Layout.Bands[0].Columns["LotNo"].AllowRowFiltering = DefaultableBoolean.True;
            //e.Layout.Bands[0].Columns["LotNo"].SortIndicator = SortIndicator.Ascending;
        }

        private void ultraGridFooder_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            //e.Layout.MaxBandDepth = 1;
            // 自动判断是否显示前面的+、-号
            e.Layout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 冻结列
            e.Layout.UseFixedHeaders = true;
            e.Layout.Override.FixedHeaderAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedHeaderAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedCellAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedCellAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;

            // 排序
            //e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "LotNO";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["MOCode"].Header.Caption = "工单代码";
            e.Layout.Bands[0].Columns["ItemCode"].Header.Caption = "产品代码";
            e.Layout.Bands[0].Columns["LotNo"].Header.Caption = "批次条码";
            e.Layout.Bands[0].Columns["LotQty"].Header.Caption = "批次数量";
            e.Layout.Bands[0].Columns["GoodQty"].Header.Caption = "良品数量";
            e.Layout.Bands[0].Columns["NGQty"].Header.Caption = "不良数量";
            e.Layout.Bands[0].Columns["LotStatus"].Header.Caption = "批次状态";
            e.Layout.Bands[0].Columns["LotStatusHidden"].Header.Caption = "批次状态";
            e.Layout.Bands[0].Columns["ProductStatus"].Header.Caption = "生产状态";
            e.Layout.Bands[0].Columns["ProductStatusChecked"].Header.Caption = "批次不良";
            e.Layout.Bands[0].Columns["RouteCode"].Header.Caption = "途程代码";
            e.Layout.Bands[0].Columns["OPCode"].Header.Caption = "工序代码";
            e.Layout.Bands[0].Columns["CollectStatus"].Header.Caption = "是否采集中";
            e.Layout.Bands[0].Columns["CollectStatusHidden"].Header.Caption = "是否采集中";
            e.Layout.Bands[0].Columns["IsCom"].Header.Caption = "是否完工";
            e.Layout.Bands[0].Columns["IsComHidden"].Header.Caption = "是否完工";
            e.Layout.Bands[0].Columns["MaintainDate"].Header.Caption = "维护日期";
            e.Layout.Bands[0].Columns["MaintainTime"].Header.Caption = "维护时间";

            e.Layout.Bands[0].Columns["Checked"].Width = 40;
            e.Layout.Bands[0].Columns["MOCode"].Width = 100;
            e.Layout.Bands[0].Columns["ItemCode"].Width = 60;
            e.Layout.Bands[0].Columns["LotNo"].Width = 100;
            e.Layout.Bands[0].Columns["LotQty"].Width = 60;
            e.Layout.Bands[0].Columns["GoodQty"].Width = 60;
            e.Layout.Bands[0].Columns["NGQty"].Width = 60;
            e.Layout.Bands[0].Columns["LotStatus"].Width = 60;
            e.Layout.Bands[0].Columns["ProductStatus"].Width = 60;
            e.Layout.Bands[0].Columns["ProductStatusChecked"].Width = 60;
            e.Layout.Bands[0].Columns["RouteCode"].Width = 70;
            e.Layout.Bands[0].Columns["OPCode"].Width = 70;
            e.Layout.Bands[0].Columns["CollectStatus"].Width = 60;
            e.Layout.Bands[0].Columns["IsCom"].Width = 60;
            e.Layout.Bands[0].Columns["MaintainDate"].Width = 90;
            e.Layout.Bands[0].Columns["MaintainTime"].Width = 90;

            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["LotNo"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["MOCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["ItemCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["LotQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["GoodQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["NGQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["LotStatus"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["ProductStatus"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["ProductStatusChecked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["RouteCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["OPCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["CollectStatus"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["IsCom"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MaintainDate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MaintainTime"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            //设置可编辑列的颜色
            //e.Layout.Bands[0].Columns["DATECODE"].CellAppearance.BackColor = Color.LawnGreen;
            //e.Layout.Bands[0].Columns["VenderLotNO"].CellAppearance.BackColor = Color.LawnGreen ;
            //e.Layout.Bands[0].Columns["VenderITEMCODE"].CellAppearance.BackColor = Color.LawnGreen;

            //设置隐藏列
            e.Layout.Bands[0].Columns["LotStatusHidden"].Hidden = true;
            e.Layout.Bands[0].Columns["CollectStatusHidden"].Hidden = true;
            e.Layout.Bands[0].Columns["IsComHidden"].Hidden = true;
            e.Layout.Bands[0].Columns["ProductStatusChecked"].Hidden = true;

            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;

            // 允许筛选
            //e.Layout.Bands[0].Columns["LotNo"].AllowRowFiltering = DefaultableBoolean.True;
            //e.Layout.Bands[0].Columns["LotNo"].SortIndicator = SortIndicator.Ascending;
        }

        private void InitializeMainGrid()
        {
            this.m_CheckHeadList = new DataSet();
            this.m_CheckFooderList = new DataSet();

            this.m_LotHead = new DataTable("LotHead");
            this.m_LotFooder = new DataTable("LotFooder");

            this.m_LotHead.Columns.Add("Checked", typeof(bool));
            this.m_LotHead.Columns.Add("MOCode", typeof(string));
            this.m_LotHead.Columns.Add("ItemCode", typeof(string));
            this.m_LotHead.Columns.Add("LotNo", typeof(string));
            this.m_LotHead.Columns.Add("LotQty", typeof(string));
            this.m_LotHead.Columns.Add("GoodQty", typeof(string));
            this.m_LotHead.Columns.Add("NGQty", typeof(string));
            this.m_LotHead.Columns.Add("LotStatus", typeof(string));
            this.m_LotHead.Columns.Add("LotStatusHidden", typeof(string));
            this.m_LotHead.Columns.Add("ProductStatus", typeof(string));
            this.m_LotHead.Columns.Add("RouteCode", typeof(string));
            this.m_LotHead.Columns.Add("OPCode", typeof(string));
            this.m_LotHead.Columns.Add("CollectStatus", typeof(string));
            this.m_LotHead.Columns.Add("CollectStatusHidden", typeof(string));
            this.m_LotHead.Columns.Add("IsCom", typeof(string));
            this.m_LotHead.Columns.Add("IsComHidden", typeof(string));
            this.m_LotHead.Columns.Add("MaintainDate", typeof(string));
            this.m_LotHead.Columns.Add("MaintainTime", typeof(string));

            this.m_LotFooder.Columns.Add("Checked", typeof(bool));
            this.m_LotFooder.Columns.Add("MOCode", typeof(string));
            this.m_LotFooder.Columns.Add("ItemCode", typeof(string));
            this.m_LotFooder.Columns.Add("LotNo", typeof(string));
            this.m_LotFooder.Columns.Add("LotQty", typeof(string));
            this.m_LotFooder.Columns.Add("GoodQty", typeof(string));
            this.m_LotFooder.Columns.Add("NGQty", typeof(string));
            this.m_LotFooder.Columns.Add("LotStatus", typeof(string));
            this.m_LotFooder.Columns.Add("LotStatusHidden", typeof(string));
            this.m_LotFooder.Columns.Add("ProductStatus", typeof(string));
            this.m_LotFooder.Columns.Add("ProductStatusChecked", typeof(bool));
            this.m_LotFooder.Columns.Add("RouteCode", typeof(string));
            this.m_LotFooder.Columns.Add("OPCode", typeof(string));
            this.m_LotFooder.Columns.Add("CollectStatus", typeof(string));
            this.m_LotFooder.Columns.Add("CollectStatusHidden", typeof(string));
            this.m_LotFooder.Columns.Add("IsCom", typeof(string));
            this.m_LotFooder.Columns.Add("IsComHidden", typeof(string));
            this.m_LotFooder.Columns.Add("MaintainDate", typeof(string));
            this.m_LotFooder.Columns.Add("MaintainTime", typeof(string));

            this.m_CheckHeadList.Tables.Add(this.m_LotHead);
            this.m_CheckFooderList.Tables.Add(this.m_LotFooder);

            this.m_CheckHeadList.AcceptChanges();
            this.m_CheckFooderList.AcceptChanges();

            this.ultraGridHead.DataSource = this.m_CheckHeadList;
            this.ultraGridFooder.DataSource = this.m_CheckFooderList;
        }

        private void ultraGridFooder_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (type == "Split")
            {
                if (e.Row.Band.Index == 0)
                {
                    e.Row.Cells["LotQty"].Column.CellActivation = Activation.AllowEdit;
                    e.Row.Cells["LotQty"].Column.CellAppearance.BackColor = Color.LawnGreen;
                    e.Row.Cells["NGQty"].Column.CellActivation = Activation.AllowEdit;
                    e.Row.Cells["NGQty"].Column.CellAppearance.BackColor = Color.LawnGreen;
                    e.Row.Cells["GoodQty"].Column.CellActivation = Activation.AllowEdit;
                    e.Row.Cells["GoodQty"].Column.CellAppearance.BackColor = Color.LawnGreen;
                }
            }
            else
            {
                e.Row.Cells["LotQty"].Column.CellActivation = Activation.NoEdit;
                e.Row.Cells["LotQty"].Column.CellAppearance.BackColor = Color.White;
                e.Row.Cells["NGQty"].Column.CellActivation = Activation.NoEdit;
                e.Row.Cells["NGQty"].Column.CellAppearance.BackColor = Color.White;
                e.Row.Cells["GoodQty"].Column.CellActivation = Activation.NoEdit;
                e.Row.Cells["GoodQty"].Column.CellAppearance.BackColor = Color.White;
            }
        }

        private void ultraGridHead_CellChange(object sender, CellEventArgs e)
        {
            this.ultraGridHead.UpdateData();
            if (e.Cell.Column.Key == "Checked")
            {
                if (e.Cell.Row.Band.Index == 0) //Parent
                {

                }

                if (e.Cell.Row.Band.Index == 1) // Child
                {
                    if (Convert.ToBoolean(e.Cell.Value) == true)
                    {

                    }
                }
            }
            this.ultraGridHead.UpdateData();
        }

        //清空Grid
        private void ClearGrid()
        {
            if (this.m_CheckHeadList == null)
            {
                return;
            }
            if (this.m_CheckFooderList == null)
            {
                return;
            }
            this.m_CheckHeadList.Tables["LotHead"].Rows.Clear();
            this.m_CheckHeadList.Tables["LotHead"].AcceptChanges();
            this.m_CheckFooderList.Tables["LotFooder"].Rows.Clear();
            this.m_CheckFooderList.Tables["LotFooder"].AcceptChanges();

            this.m_CheckHeadList.AcceptChanges();
            this.m_CheckFooderList.AcceptChanges();

            this.ultraGridHead.DataSource = this.m_CheckHeadList;
            this.ultraGridFooder.DataSource = this.m_CheckFooderList;
        }

        #endregion

        #region Button Events
        //取消
        private void ucbCancel_Click(object sender, EventArgs e)
        {
            //取消:清空下面的Grid
            ClearGrid();

            //重新调用查询
            ucButtonQuery_Click(null, null);

            type = "";
            this.ucButton_OpenLot.Enabled = true;
            this.ucButton_MerageLot.Enabled = true;

        }

        //保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            string moCode = string.Empty;
            string maintainUser = ApplicationService.Current().LoginInfo.UserCode;
            this.ultraGridFooder.UpdateData();

            //检查下面的Grid中必须有值。
            if (this.ultraGridFooder.Rows.Count < 1)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_SELECTED_COUNT_THEN_ONE"));
                return;
            }

            if (type == "")
            {
                return;
            }

            try
            {
                this.DataProvider.BeginTransaction();
                if (type == "Split")//拆批处理
                {
                    //如果批次已经投入生产,则新增批次的在制品信息和旧批的在制品信息一样,把插入新批的在制品信息到TblLotSimulation，TblLotSimulationReport中，记录批次拆解的信息到TBLONWIPLotTrans中，新增批次信息插入到TBLMO2LOT 中，新批的状态和旧批保持一致。
                    //如果批次没有投入生产,则新批的信息只需插入TBLMO2LOT中即可., 新批的状态和旧批保持一致。

                    //如果是拆批，检查数量之和必须等于原批数量。
                    decimal countLotQty = 0;
                    decimal countNGQty = 0;
                    decimal countGoodQty = 0;
                    decimal count = 0;
                    bool checkPass = false;
                    bool updateOldTS = false;
                    foreach (UltraGridRow row in this.ultraGridFooder.Rows)
                    {
                        moCode = row.Cells["MOCode"].Value.ToString().Trim();
                        countNGQty += decimal.Parse(row.Cells["NGQty"].Value.ToString().Trim());
                        count = decimal.Parse(row.Cells["NGQty"].Value.ToString().Trim());
                        countGoodQty += decimal.Parse(row.Cells["GoodQty"].Value.ToString().Trim());
                        count += decimal.Parse(row.Cells["GoodQty"].Value.ToString().Trim());
                        countLotQty += decimal.Parse(row.Cells["LotQty"].Value.ToString().Trim());

                        //良品和不良品数量之和是否超过该批的批次数量
                        if (count > decimal.Parse(row.Cells["LotQty"].Value.ToString().Trim()))
                        {
                            this.DataProvider.RollbackTransaction();
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_NG_GOOD_OVERFLOW"));
                            return;
                        }

                        //是否至少有一笔生产状态与拆解批次相同
                        if (Convert.ToBoolean(row.Cells["ProductStatusChecked"].Value) == isNG)
                        {
                            checkPass = true;
                        }

                        //如果批次数量为0，记录不符合要求
                        if (decimal.Parse(row.Cells["LotQty"].Value.ToString().Trim()) < 1)
                        {
                            this.DataProvider.RollbackTransaction();
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ERROR_QTY_ZERO"));
                            return;
                        }
                    }
                    if ((lotQty != countLotQty) || (ngQty != countNGQty) || (goodQty != countGoodQty))
                    {
                        this.DataProvider.RollbackTransaction();
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ERROR_QTY_DIF"));
                        return;
                    }
                    if (!checkPass)
                    {
                        //批次生产状态需至少一笔与原生产状态相同
                        this.DataProvider.RollbackTransaction();
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ERROR_PRODUCTSTATUS_DIF"));
                        return;
                    }

                    object mo2LotLink = _MOLotFacade.GetMO2LotLink(savedLotNo, moCode);
                    object mo2LotLinkNew = _MOLotFacade.GetMO2LotLink(savedLotNo, moCode);

                    //获取在制品信息
                    object lotSimulation = _DataCollectFacade.GetLotSimulation(moCode, savedLotNo);
                    object lotSimulationReport = _DataCollectFacade.GetLotSimulationReport(moCode, savedLotNo);
                    object lotSimulationNew = _DataCollectFacade.GetLotSimulation(moCode, savedLotNo);
                    object lotSimulationReportNew = _DataCollectFacade.GetLotSimulationReport(moCode, savedLotNo);
                    bool unProductive = false;

                    string lotCode = string.Empty;
                    if ((lotSimulation == null) && (lotSimulationReport == null))
                    {
                        unProductive = true;
                    }

                    foreach (UltraGridRow row in ultraGridFooder.Rows)
                    {
                        countNGQty = decimal.Parse(row.Cells["NGQty"].Value.ToString().Trim());
                        countGoodQty = decimal.Parse(row.Cells["GoodQty"].Value.ToString().Trim());
                        countLotQty = decimal.Parse(row.Cells["LotQty"].Value.ToString().Trim());
                        lotCode = row.Cells["LotNo"].Value.ToString().Trim();

                        if (!unProductive)//已生产需要更新的信息
                        {
                            //更新LotSimulation和LotSimulationReport 
                            if (lotSimulation != null)//未完工时需要更新
                            {
                                ((LotSimulation)lotSimulation).LotQty = countLotQty;
                                ((LotSimulation)lotSimulation).GoodQty = countGoodQty;
                                ((LotSimulation)lotSimulation).NGQty = countNGQty;
                                ((LotSimulation)lotSimulation).MaintainUser = maintainUser;

                                ((LotSimulation)lotSimulation).ProductStatus = ((LotSimulation)lotSimulationNew).ProductStatus;
                                ((LotSimulation)lotSimulation).LastAction = ((LotSimulation)lotSimulationNew).LastAction;
                                ((LotSimulation)lotSimulation).ActionList = ((LotSimulation)lotSimulationNew).ActionList;
                                if (Convert.ToBoolean(row.Cells["ProductStatusChecked"].Value))
                                {
                                    //如果原批状态是NG则保留原来状态信息，否则批改为不良
                                    if (!isNG)
                                    {
                                        ((LotSimulation)lotSimulation).ProductStatus = ProductStatus.NG;
                                        ((LotSimulation)lotSimulation).LastAction = ActionType.DataCollectAction_NG;
                                        ((LotSimulation)lotSimulation).ActionList = ";" + ActionType.DataCollectAction_NG + ";";                                        
                                    }
                                }
                                else
                                {
                                    //如果当前状态为良，保留原来信息，否则设为良
                                    if (isNG)
                                    {
                                        ((LotSimulation)lotSimulation).ProductStatus = ProductStatus.GOOD;
                                        ((LotSimulation)lotSimulation).LastAction = ActionType.DataCollectAction_GOOD;
                                        ((LotSimulation)lotSimulation).ActionList = ";" + ActionType.DataCollectAction_GOOD + ";";
                                    }
                                }
                            }

                            ((LotSimulationReport)lotSimulationReport).LotQty = countLotQty;
                            ((LotSimulationReport)lotSimulationReport).GoodQty = countGoodQty;
                            ((LotSimulationReport)lotSimulationReport).NGQty = countNGQty;
                            ((LotSimulationReport)lotSimulationReport).MaintainUser = maintainUser;

                            ((LotSimulationReport)lotSimulationReport).Status = ((LotSimulationReport)lotSimulationReportNew).Status;
                            ((LotSimulationReport)lotSimulationReport).LastAction = ((LotSimulationReport)lotSimulationReportNew).LastAction;
                            if (Convert.ToBoolean(row.Cells["ProductStatusChecked"].Value))
                            {
                                //如果原批状态是NG则保留原来状态信息，否则批改为不良
                                if (!isNG)
                                {
                                    ((LotSimulationReport)lotSimulationReport).Status = ProductStatus.NG;
                                    ((LotSimulationReport)lotSimulationReport).LastAction = ActionType.DataCollectAction_NG;                                    
                                }
                            }
                            else
                            {
                                //如果当前状态为良，保留原来信息，否则设为良
                                if (isNG)
                                {
                                    ((LotSimulationReport)lotSimulationReport).Status = ProductStatus.GOOD;
                                    ((LotSimulationReport)lotSimulationReport).LastAction = ActionType.DataCollectAction_GOOD;
                                }
                                
                            }
                        }

                        if (savedLotNo.Equals(row.Cells["LotNo"].Value.ToString().Trim()))//如果是旧批
                        {
                            //更新MO2LotLink
                            if (mo2LotLink != null)
                            {
                                ((MO2LotLink)mo2LotLink).LotQty = countLotQty;
                                ((MO2LotLink)mo2LotLink).MUser = maintainUser;
                                _MOLotFacade.UpdateMO2LotLink(mo2LotLink as MO2LotLink);
                            }

                            if (!unProductive)//已生产需要更新的信息
                            {
                                //更新旧批的LotSimulation和LotSimulationReport 
                                if (lotSimulation != null)//未完工时需要更新
                                {
                                    _DataCollectFacade.UpdateLotSimulation(lotSimulation as LotSimulation);
                                }

                                _DataCollectFacade.UpdateLotSimulationReport(lotSimulationReport as LotSimulationReport);

                                //如果原是不良批现已转为良批，需要更新维修状态
                                if (isNG && !Convert.ToBoolean(row.Cells["ProductStatusChecked"].Value))
                                {
                                    updateOldTS = true;
                                }
                                //OnWipLotTrans

                            }
                        }
                        else
                        {
                            //新增MO2LotLink
                            if (mo2LotLinkNew != null)
                            {
                                ((MO2LotLink)mo2LotLinkNew).LotNo = lotCode;
                                ((MO2LotLink)mo2LotLinkNew).LotQty = countLotQty;
                                ((MO2LotLink)mo2LotLinkNew).PrintTimes = 0;
                                ((MO2LotLink)mo2LotLinkNew).LastPrintUser = "";
                                ((MO2LotLink)mo2LotLinkNew).LastPrintDate = 0;
                                ((MO2LotLink)mo2LotLinkNew).LastPrintTime = 0;
                                ((MO2LotLink)mo2LotLinkNew).MUser = maintainUser;
                                _MOLotFacade.AddMO2LotLink(mo2LotLinkNew as MO2LotLink);
                            }

                            if (!unProductive)//已生产需要新增的信息
                            {
                                //新增LotSimulation和LotSimulationReport 
                                if (lotSimulation != null)//未完工时需要新增
                                {
                                    ((LotSimulation)lotSimulation).LotCode = lotCode;
                                    _DataCollectFacade.AddLotSimulation(lotSimulation as LotSimulation);
                                }

                                ((LotSimulationReport)lotSimulationReport).LotCode = lotCode;
                                _DataCollectFacade.AddLotSimulationReport(lotSimulationReport as LotSimulationReport);

                                //新增OnWipLotTrans
                            }
                        }

                        if (!unProductive)
                        {
                            //新增OnWipLotTrans
                            OnWipLotTrans onWIPLotTrans = _DataCollectFacade.CreateNewOnWIPLotTransfer();
                            onWIPLotTrans.OperationType = OperationType.OPERATIONTYPE_SPLIT;
                            onWIPLotTrans.LotCode = lotCode;
                            onWIPLotTrans.MOCode = moCode;
                            onWIPLotTrans.Qty = countLotQty;
                            onWIPLotTrans.LotSeq = ((LotSimulationReport)lotSimulationReportNew).LotSeq;
                            onWIPLotTrans.TLotCode = savedLotNo;
                            onWIPLotTrans.TLotSeq = ((LotSimulationReport)lotSimulationReportNew).LotSeq;
                            onWIPLotTrans.TLotQty = ((LotSimulationReport)lotSimulationReportNew).LotQty;
                            onWIPLotTrans.RouteCode = ((LotSimulationReport)lotSimulationReportNew).RouteCode;
                            onWIPLotTrans.OPCode = ((LotSimulationReport)lotSimulationReportNew).OPCode;
                            onWIPLotTrans.MaintainUser = maintainUser;
                            _DataCollectFacade.AddOnWIPLotTransfer(onWIPLotTrans);

                            if (Convert.ToBoolean(row.Cells["ProductStatusChecked"].Value))
                            {
                                GenerateTSInfo(moCode, lotCode);
                            }
                        }

                    }

                    if (!saveOldLotInfo)//不保留旧批
                    {
                        //如果原批不保留，则要将原批的状态设为STOP。
                        StopLot(moCode, savedLotNo);

                        //旧批如果不良，停用时需要更新维修状态
                        if (isNG)
                        {
                            updateOldTS = true;
                        }
                    }

                    //处理旧批维修信息
                    if (updateOldTS)
                    {
                        BenQGuru.eMES.Domain.TS.TS ts = _TSFacade.QueryLastTSByRunningCard(savedLotNo) as BenQGuru.eMES.Domain.TS.TS;
                        if (ts != null)
                        {
                            ts.TSStatus = TSStatus.TSStatus_RepeatNG;
                            _TSFacade.UpdateTS_New(ts);
                        }                        
                    }

                }
                else if (type == "Merge")//合批处理
                {
                    if (!saveOldLotInfo)//不保留旧批
                    {
                        #region 旧批1
                        UltraGridRow ultraGridRowOld1 = ultraGridFooder.Rows[0];//旧批1
                        object mo2LotOld1 = _MOLotFacade.GetMO2LotLink(ultraGridRowOld1.Cells["LotNo"].Value.ToString().Trim(), ultraGridRowOld1.Cells["MOCode"].Value.ToString().Trim());                        
                        #endregion

                        #region 旧批2
                        UltraGridRow ultraGridRowOld2 = ultraGridFooder.Rows[1];//旧批2
                        object mo2LotOld2 = _MOLotFacade.GetMO2LotLink(ultraGridRowOld2.Cells["LotNo"].Value.ToString().Trim(), ultraGridRowOld2.Cells["MOCode"].Value.ToString().Trim());
                        #endregion

                        #region 产生新批信息
                        UltraGridRow ultraGridRowNew = ultraGridFooder.Rows[2];//新批
                        object mo2LotNew = mo2LotOld1;
                        string lotCode = ultraGridRowNew.Cells["LotNo"].Value.ToString().Trim();
                        moCode = ultraGridRowOld2.Cells["MOCode"].Value.ToString().Trim();
                        decimal newNGQty = decimal.Parse(ultraGridRowNew.Cells["NGQty"].Value.ToString().Trim());
                        decimal newGoodQty = decimal.Parse(ultraGridRowNew.Cells["GoodQty"].Value.ToString().Trim());
                        decimal newLotQty = decimal.Parse(ultraGridRowNew.Cells["LotQty"].Value.ToString().Trim());
                        //新增MO2LotLink
                        if (mo2LotNew != null)
                        {
                            ((MO2LotLink)mo2LotNew).LotNo = lotCode;
                            ((MO2LotLink)mo2LotNew).LotQty = newLotQty;
                            ((MO2LotLink)mo2LotNew).PrintTimes = 0;
                            ((MO2LotLink)mo2LotNew).LastPrintUser = "";
                            ((MO2LotLink)mo2LotNew).LastPrintDate = 0;
                            ((MO2LotLink)mo2LotNew).LastPrintTime = 0;
                            ((MO2LotLink)mo2LotNew).MUser = maintainUser;
                            _MOLotFacade.AddMO2LotLink(mo2LotNew as MO2LotLink);
                        }

                        //获取旧批1的在制品信息
                        object lotSimulationOld1 = _DataCollectFacade.GetLotSimulation(ultraGridRowOld1.Cells["MOCode"].Value.ToString().Trim(), ultraGridRowOld1.Cells["LotNo"].Value.ToString().Trim());
                        object lotSimulationReportOld1 = _DataCollectFacade.GetLotSimulationReport(ultraGridRowOld1.Cells["MOCode"].Value.ToString().Trim(), ultraGridRowOld1.Cells["LotNo"].Value.ToString().Trim());

                        //获取旧批2的在制品信息
                        object lotSimulationOld2 = _DataCollectFacade.GetLotSimulation(ultraGridRowOld2.Cells["MOCode"].Value.ToString().Trim(), ultraGridRowOld2.Cells["LotNo"].Value.ToString().Trim());
                        object lotSimulationReportOld2 = _DataCollectFacade.GetLotSimulationReport(ultraGridRowOld2.Cells["MOCode"].Value.ToString().Trim(), ultraGridRowOld2.Cells["LotNo"].Value.ToString().Trim());

                        bool unProductive = true;
                        if (lotSimulationReportOld1 != null)
                        {
                            unProductive = false;                            

                            //比较采集时间，获取合批后的采集时间
                            string beginDateTimeOld1 = ((LotSimulationReport)lotSimulationReportOld1).BeginDate.ToString().PadLeft(8, '0') + ((LotSimulationReport)lotSimulationReportOld1).BeginTime.ToString().PadLeft(6, '0');
                            string beginDateTimeOld2 = ((LotSimulationReport)lotSimulationReportOld2).BeginDate.ToString().PadLeft(8, '0') + ((LotSimulationReport)lotSimulationReportOld2).BeginTime.ToString().PadLeft(6, '0');
                            if (decimal.Parse(beginDateTimeOld1) > decimal.Parse(beginDateTimeOld2))
                            {
                                //取采集开始时间最早的
                                ((LotSimulationReport)lotSimulationReportOld1).BeginDate = ((LotSimulationReport)lotSimulationReportOld2).BeginDate;
                                ((LotSimulationReport)lotSimulationReportOld1).BeginTime = ((LotSimulationReport)lotSimulationReportOld2).BeginTime;

                                if (lotSimulationOld1 != null)
                                {
                                    ((LotSimulation)lotSimulationOld1).BeginDate = ((LotSimulation)lotSimulationOld2).BeginDate;
                                    ((LotSimulation)lotSimulationOld1).BeginTime = ((LotSimulation)lotSimulationOld2).BeginTime;
                                }

                            }

                            string endDateTimeOld1 = ((LotSimulationReport)lotSimulationReportOld1).EndDate.ToString().PadLeft(8, '0') + ((LotSimulationReport)lotSimulationReportOld1).EndTime.ToString().PadLeft(6, '0');
                            string endDateTimeOld2 = ((LotSimulationReport)lotSimulationReportOld2).EndDate.ToString().PadLeft(8, '0') + ((LotSimulationReport)lotSimulationReportOld2).EndTime.ToString().PadLeft(6, '0');
                            if (decimal.Parse(endDateTimeOld1) < decimal.Parse(endDateTimeOld2))
                            {
                                //取采集结束时间最晚的
                                ((LotSimulationReport)lotSimulationReportOld1).EndDate = ((LotSimulationReport)lotSimulationReportOld2).EndDate;
                                ((LotSimulationReport)lotSimulationReportOld1).EndTime = ((LotSimulationReport)lotSimulationReportOld2).EndTime;

                                if (lotSimulationOld1 != null)
                                {
                                    ((LotSimulation)lotSimulationOld1).EndDate = ((LotSimulation)lotSimulationOld2).EndDate;
                                    ((LotSimulation)lotSimulationOld1).EndTime = ((LotSimulation)lotSimulationOld2).EndTime;
                                }

                            }
                        }

                        if (!unProductive)//已生产需要新增的信息
                        {
                            //新增OnWipLotTrans两笔
                            OnWipLotTrans onWIPLotTrans = _DataCollectFacade.CreateNewOnWIPLotTransfer();
                            onWIPLotTrans.OperationType = OperationType.OPERATIONTYPE_MERGE;
                            onWIPLotTrans.LotCode = lotCode;
                            onWIPLotTrans.MOCode = moCode;
                            onWIPLotTrans.Qty = newLotQty;
                            onWIPLotTrans.LotSeq = ((LotSimulationReport)lotSimulationReportOld1).LotSeq;
                            onWIPLotTrans.TLotCode = ((LotSimulationReport)lotSimulationReportOld1).LotCode;
                            onWIPLotTrans.TLotSeq = ((LotSimulationReport)lotSimulationReportOld1).LotSeq;
                            onWIPLotTrans.TLotQty = ((LotSimulationReport)lotSimulationReportOld1).LotQty;
                            onWIPLotTrans.RouteCode = ((LotSimulationReport)lotSimulationReportOld1).RouteCode;
                            onWIPLotTrans.OPCode = ((LotSimulationReport)lotSimulationReportOld1).OPCode;
                            onWIPLotTrans.MaintainUser = maintainUser;
                            _DataCollectFacade.AddOnWIPLotTransfer(onWIPLotTrans);

                            onWIPLotTrans.LotSeq = ((LotSimulationReport)lotSimulationReportOld2).LotSeq;
                            onWIPLotTrans.TLotCode = ((LotSimulationReport)lotSimulationReportOld2).LotCode;
                            onWIPLotTrans.TLotSeq = ((LotSimulationReport)lotSimulationReportOld2).LotSeq;
                            onWIPLotTrans.TLotQty = ((LotSimulationReport)lotSimulationReportOld2).LotQty;
                            onWIPLotTrans.RouteCode = ((LotSimulationReport)lotSimulationReportOld2).RouteCode;
                            onWIPLotTrans.OPCode = ((LotSimulationReport)lotSimulationReportOld2).OPCode;
                            _DataCollectFacade.AddOnWIPLotTransfer(onWIPLotTrans);

                            //新增LotSimulation和LotSimulationReport 
                            if (lotSimulationOld1 != null)//未完工时需要新增
                            {
                                ((LotSimulation)lotSimulationOld1).LotCode = lotCode;
                                ((LotSimulation)lotSimulationOld1).LotQty = newLotQty;
                                ((LotSimulation)lotSimulationOld1).GoodQty = newGoodQty;
                                ((LotSimulation)lotSimulationOld1).NGQty = newNGQty;
                                ((LotSimulation)lotSimulationOld1).MaintainUser = maintainUser;
                                _DataCollectFacade.AddLotSimulation(lotSimulationOld1 as LotSimulation);
                            }

                            ((LotSimulationReport)lotSimulationReportOld1).LotCode = lotCode;
                            ((LotSimulationReport)lotSimulationReportOld1).LotQty = newLotQty;
                            ((LotSimulationReport)lotSimulationReportOld1).GoodQty = newGoodQty;
                            ((LotSimulationReport)lotSimulationReportOld1).NGQty = newNGQty;
                            ((LotSimulationReport)lotSimulationReportOld1).MaintainUser = maintainUser;
                            _DataCollectFacade.AddLotSimulationReport(lotSimulationReportOld1 as LotSimulationReport);                            
                        }
                        #endregion

                        #region 停用旧批1
                        StopLot(ultraGridRowOld1.Cells["MOCode"].Value.ToString().Trim(), ultraGridRowOld1.Cells["LotNo"].Value.ToString().Trim());
                        #endregion

                        #region 停用旧批2
                        StopLot(ultraGridRowOld2.Cells["MOCode"].Value.ToString().Trim(), ultraGridRowOld2.Cells["LotNo"].Value.ToString().Trim());
                        #endregion
                    }
                    else
                    {
                        foreach (UltraGridRow row in this.ultraGridFooder.Rows)
                        {
                            if (savedLotNo.Equals(row.Cells["LotNo"].Value.ToString().Trim()))//如果是保留的旧批
                            {
                                object mo2LotLink = _MOLotFacade.GetMO2LotLink(row.Cells["LotNo"].Value.ToString().Trim(), row.Cells["MOCode"].Value.ToString().Trim());

                                //获取在制品信息
                                object lotSimulation = _DataCollectFacade.GetLotSimulation(row.Cells["MOCode"].Value.ToString().Trim(), savedLotNo);
                                object lotSimulationReport = _DataCollectFacade.GetLotSimulationReport(row.Cells["MOCode"].Value.ToString().Trim(), savedLotNo);
                                bool unProductive = false;
                                decimal newNGQty = decimal.Parse(row.Cells["NGQty"].Value.ToString().Trim());
                                decimal newGoodQty = decimal.Parse(row.Cells["GoodQty"].Value.ToString().Trim());
                                decimal newLotQty = decimal.Parse(row.Cells["LotQty"].Value.ToString().Trim());

                                if ((lotSimulation == null) && (lotSimulationReport == null))
                                {
                                    unProductive = true;
                                }

                                //更新MO2LotLink
                                if (mo2LotLink != null)
                                {
                                    ((MO2LotLink)mo2LotLink).LotQty = newLotQty;
                                    ((MO2LotLink)mo2LotLink).MUser = maintainUser;
                                    _MOLotFacade.UpdateMO2LotLink(mo2LotLink as MO2LotLink);
                                }

                                if (!unProductive)//已生产需要更新的信息
                                {
                                    //OnWipLotTrans
                                    OnWipLotTrans onWIPLotTrans = _DataCollectFacade.CreateNewOnWIPLotTransfer();
                                    onWIPLotTrans.OperationType = OperationType.OPERATIONTYPE_MERGE;
                                    onWIPLotTrans.LotCode = row.Cells["LotNo"].Value.ToString().Trim();
                                    onWIPLotTrans.MOCode = row.Cells["MOCode"].Value.ToString().Trim();
                                    onWIPLotTrans.Qty = newLotQty;
                                    onWIPLotTrans.LotSeq = ((LotSimulationReport)lotSimulationReport).LotSeq;
                                    onWIPLotTrans.TLotCode = ((LotSimulationReport)lotSimulationReport).LotCode;
                                    onWIPLotTrans.TLotSeq = ((LotSimulationReport)lotSimulationReport).LotSeq;
                                    onWIPLotTrans.TLotQty = ((LotSimulationReport)lotSimulationReport).LotQty;
                                    onWIPLotTrans.RouteCode = ((LotSimulationReport)lotSimulationReport).RouteCode;
                                    onWIPLotTrans.OPCode = ((LotSimulationReport)lotSimulationReport).OPCode;
                                    onWIPLotTrans.MaintainUser = maintainUser;
                                    _DataCollectFacade.AddOnWIPLotTransfer(onWIPLotTrans);

                                    //更新LotSimulation和LotSimulationReport 
                                    if (lotSimulation != null)//未完工时需要更新
                                    {
                                        ((LotSimulation)lotSimulation).LotQty = newLotQty;
                                        ((LotSimulation)lotSimulation).GoodQty = newGoodQty;
                                        ((LotSimulation)lotSimulation).NGQty = newNGQty;
                                        ((LotSimulation)lotSimulation).MaintainUser = maintainUser;
                                        _DataCollectFacade.UpdateLotSimulation(lotSimulation as LotSimulation);
                                    }

                                    ((LotSimulationReport)lotSimulationReport).LotQty = newLotQty;
                                    ((LotSimulationReport)lotSimulationReport).GoodQty = newGoodQty;
                                    ((LotSimulationReport)lotSimulationReport).NGQty = newNGQty;
                                    ((LotSimulationReport)lotSimulationReport).MaintainUser = maintainUser;
                                    _DataCollectFacade.UpdateLotSimulationReport(lotSimulationReport as LotSimulationReport);                                    

                                }

                            }
                            else
                            {                              

                                //将原批的状态设为STOP。
                                StopLot(row.Cells["MOCode"].Value.ToString().Trim(), row.Cells["LotNo"].Value.ToString().Trim());

                            }

                        }
                    }

                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                if (!ex.Message.ToString().Equals("未将对象引用设置到对象的实例。"))
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));
                }                
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Save_Lot_Error"));
                return;

            }

            //提示保存成功。
            if (type == "Merge")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_Merage_Lot_SUCCESS"));
            }
            if (type == "Split")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_Split_Lot_SUCCESS"));
            }
            //清空下面的Grid，重新调用查询。
            this.ucbCancel_Click(null, null);

        }

        private void ItemCodeSelector_ItemCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.uclItemCode.Value = e.CustomObject;
        }

        //选择产品代码
        private void button_ItemCode_Click(object sender, EventArgs e)
        {
            //FMCodeQuery fMCodeQuery = new FMCodeQuery();
            FItemCodeQuery fItemCodeQuery = new FItemCodeQuery();
            fItemCodeQuery.Owner = this;
            fItemCodeQuery.StartPosition = FormStartPosition.CenterScreen;
            fItemCodeQuery.ItemCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(ItemCodeSelector_ItemCodeSelectedEvent);
            fItemCodeQuery.ShowDialog();
            fItemCodeQuery = null;
        }

        //查询
        private void ucButtonQuery_Click(object sender, EventArgs e)
        {
            //取消:清空下面的Grid
            ClearGrid();
            type = "";
            this.ucButton_OpenLot.Enabled = true;
            this.ucButton_MerageLot.Enabled = true;

            string moCode = this.uclMoCode.Value.Trim();
            string itemCode = this.uclItemCode.Value.Trim();
            string lotNo = this.uclLotNo.Value.Trim();

            if (moCode == "" && itemCode == "" && lotNo == "")
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$At_Least_One_Conditon_Not_NULL"));
                return;
            }

            object[] objs = _MOLotFacade.QueryMO2LotForQuery(moCode, itemCode, lotNo, checkBoxOnlyValid.Checked);

            if (objs != null)
            {
                foreach (LotSimulationForQuery lotInfo in objs)
                {
                    DataRow dr = this.m_LotHead.NewRow();
                    dr["Checked"] = false;
                    dr["MOCode"] = lotInfo.MOCode;
                    dr["ItemCode"] = lotInfo.ItemCode;
                    dr["LotNo"] = lotInfo.LotCode;
                    dr["LotQty"] = lotInfo.LotQty;
                    dr["GoodQty"] = lotInfo.GoodQty;
                    dr["NGQty"] = lotInfo.NGQty;
                    dr["LotStatus"] = MutiLanguages.ParserString(lotInfo.LotStatus);
                    dr["LotStatusHidden"] = lotInfo.LotStatus;
                    dr["RouteCode"] = lotInfo.RouteCode;
                    dr["OPCode"] = lotInfo.OPCode;
                    dr["CollectStatus"] = MutiLanguages.ParserString(lotInfo.CollectStatus);
                    dr["CollectStatusHidden"] = lotInfo.CollectStatus;
                    dr["ProductStatus"] = MutiLanguages.ParserString(lotInfo.ProductStatus);
                    dr["IsCom"] = MutiLanguages.ParserString(lotInfo.IsComplete);
                    dr["IsComHidden"] = lotInfo.IsComplete;
                    dr["MaintainDate"] = FormatHelper.ToDateString(lotInfo.MaintainDate);
                    dr["MaintainTime"] = FormatHelper.ToTimeString(lotInfo.MaintainTime);
                    this.m_LotHead.Rows.Add(dr);
                }
                this.m_CheckHeadList.AcceptChanges();
                this.ultraGridHead.DataSource = this.m_CheckHeadList;
            }
        }

        //合批
        private void ucButton_MerageLot_Click(object sender, EventArgs e)
        {
            //检查下面的Grid必须没有数据，否则提示：请将前一笔处理保存或取消。
            if (ultraGridFooder.Rows.Count > 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_Please_Save_Pre_Recard"));
                return;
            }

            //判断是否选择了两笔数据
            if (!CheckSelectTwoLot())
            {
                return;
            }

            //检查上面的Grid选择的两笔数据的工单代码，途程和工序必须一致。
            if (!IsLotRowsSame())
            {
                return;
            }

            //获取勾选的行
            List<UltraGridRow> list = this.GetLotNoRows();
            //设置Type =Merge
            type = "Merge";
            if (!CheckLotSimulation(list))//检查批次生产状态
            {
                return;
            }

            //如果保留旧批判断是否输入了选中两笔批次之一的批号
            if (this.ucLSaveOld.Checked && (!IsOneOfSelectTwoLot()))
            {
                return;
            }

            #region 合并逻辑            

            saveOldLotInfo = this.ucLSaveOld.Checked;
            savedLotNo = this.ucLSaveOld.Value.ToString().Trim();
            Copy(list, this.ucLSaveOld.Checked);

            #endregion

            this.ucButton_OpenLot.Enabled = false;
        }

        //拆批
        private void ucButton_OpenLot_Click(object sender, EventArgs e)
        {
            //先判断下面Grid中有没有数据
            if (ultraGridFooder.Rows.Count > 0)//请将前一笔处理保存或取消
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_Please_Save_Pre_Recard"));
                return;
            }

            //上面Grid中只能选择一笔数据
            if (!CheckSelectOnlyOneLot())
            {
                return;
            }

            //获取勾选的行            
            UltraGridRow row = this.GetLotNoRow();
            //设置Type =Split
            type = "Split";
            if (!CheckLotSimulation(row))//检查批次生产状态
            {
                return;
            }

            #region 拆解逻辑            

            //先进行获取新的批号
            string newLotNo = _MOLotFacade.GenerateNewLotNo(row.Cells["MOCode"].Value.ToString().Trim(), row.Cells["ItemCode"].Value.ToString().Trim(), 1);
            if (newLotNo != null)
            {
                if (newLotNo.IndexOf("False$") >= 0)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, newLotNo.Substring(newLotNo.IndexOf("$"))));
                    return;
                }
            }

            //设置数量
            if (row.Cells["LotQty"].ToString().Trim() != "")
            {
                lotQty = decimal.Parse(row.Cells["LotQty"].Value.ToString().Trim());
                ngQty = decimal.Parse(row.Cells["NGQty"].Value.ToString().Trim());
                goodQty = decimal.Parse(row.Cells["GoodQty"].Value.ToString().Trim());
            }

            //1:原批号原信息复制一笔在下面的Grid中。
            //如果勾选保留原批，则新增一笔记录到Grid中，新批的信息等同于原批，数量为0。
            //如果不勾选保留原批，则新增两笔记录到Grid中，新批的信息等同于原批，数量为0。
            //2:拆批时：下面Grid中的批次数量可以被修改,新批和原批的数量相加等于拆批前的数量。
            saveOldLotInfo = this.ucLSaveOld.Checked;
            savedLotNo = row.Cells["LotNo"].Value.ToString().Trim();
            Copy(newLotNo, row, this.ucLSaveOld.Checked);

            #endregion

            this.ucButton_MerageLot.Enabled = false;
            //this.ucLSaveOld.Checked = false;
            if (saveOldLotInfo)
            {
                this.ucLSaveOld.Value = savedLotNo;
            }            
        }

        //原批号原信息复制一或两笔在下面的Grid中。
        private void Copy(string lotNo, Infragistics.Win.UltraWinGrid.UltraGridRow row, bool saveOld)
        {
            if (this.m_LotFooder != null)
            {
                this.m_LotFooder.Rows.Clear();
            }

            if (ht.Count > 0)
            {
                ht.Clear();
            }

            DataRow dr;
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            if (saveOld)
            {
                dr = this.m_LotFooder.NewRow();
                dr["Checked"] = false;
                dr["MOCode"] = row.Cells["MOCode"].Value.ToString();
                dr["ItemCode"] = row.Cells["ItemCode"].Value.ToString();
                dr["LotNo"] = row.Cells["LotNo"].Value.ToString();
                dr["LotQty"] = row.Cells["GoodQty"].Value.ToString();
                dr["GoodQty"] = row.Cells["GoodQty"].Value.ToString();
                dr["NGQty"] = 0 + "";
                dr["LotStatus"] = row.Cells["LotStatus"].Value.ToString();
                dr["LotStatusHidden"] = row.Cells["LotStatusHidden"].Value.ToString();
                dr["RouteCode"] = row.Cells["RouteCode"].Value.ToString();
                dr["OPCode"] = row.Cells["OPCode"].Value.ToString();
                dr["CollectStatus"] = row.Cells["CollectStatus"].Value.ToString();
                dr["CollectStatusHidden"] = row.Cells["CollectStatusHidden"].Value.ToString();
                dr["ProductStatus"] = row.Cells["ProductStatus"].Value.ToString();
                if (isNG)
                {
                    dr["ProductStatusChecked"] = true;
                }
                else
                {
                    dr["ProductStatusChecked"] = false;
                }                
                dr["IsCom"] = row.Cells["IsCom"].Value.ToString();
                dr["IsComHidden"] = row.Cells["IsComHidden"].Value.ToString();
                dr["MaintainDate"] = row.Cells["MaintainDate"].Value.ToString();
                dr["MaintainTime"] = row.Cells["MaintainTime"].Value.ToString();
                this.m_LotFooder.Rows.Add(dr);
                ht.Add("old", dr);
            }

            dr = this.m_LotFooder.NewRow();
            dr["Checked"] = false;
            dr["MOCode"] = row.Cells["MOCode"].Value.ToString();
            dr["ItemCode"] = row.Cells["ItemCode"].Value.ToString();
            dr["LotNo"] = lotNo;            
            if (saveOld)
            {
                dr["LotQty"] = row.Cells["NGQty"].Value.ToString();
                dr["GoodQty"] = 0 + "";
                dr["NGQty"] = row.Cells["NGQty"].Value.ToString();
                if (decimal.Parse(row.Cells["NGQty"].Value.ToString()) < 1)
                {
                    dr["ProductStatusChecked"] = false;
                }
                else
                {
                    dr["ProductStatusChecked"] = true;
                }
            }
            else
            {
                dr["LotQty"] = row.Cells["GoodQty"].Value.ToString();
                dr["GoodQty"] = row.Cells["GoodQty"].Value.ToString();
                dr["NGQty"] = 0 + "";
                dr["ProductStatusChecked"] = false;
            }
            dr["LotStatus"] = row.Cells["LotStatus"].Value.ToString();
            dr["LotStatusHidden"] = row.Cells["LotStatusHidden"].Value.ToString();
            dr["RouteCode"] = row.Cells["RouteCode"].Value.ToString();
            dr["OPCode"] = row.Cells["OPCode"].Value.ToString();
            dr["CollectStatus"] = row.Cells["CollectStatus"].Value.ToString();
            dr["CollectStatusHidden"] = row.Cells["CollectStatusHidden"].Value.ToString();
            dr["ProductStatus"] = row.Cells["ProductStatus"].Value.ToString();
            dr["IsCom"] = row.Cells["IsCom"].Value.ToString();
            dr["IsComHidden"] = row.Cells["IsComHidden"].Value.ToString();
            dr["MaintainDate"] = FormatHelper.ToDateString(dbDateTime.DBDate);
            dr["MaintainTime"] = FormatHelper.ToTimeString(dbDateTime.DBTime);
            this.m_LotFooder.Rows.Add(dr);
            ht.Add("new", dr);

            if (!saveOld)
            {
                string newLotNo = _MOLotFacade.GenerateNewLotNo(row.Cells["MOCode"].Value.ToString().Trim(), row.Cells["ItemCode"].Value.ToString().Trim(), 2);
                if (newLotNo != null)
                {
                    if (newLotNo.IndexOf("False$") >= 0)
                    {
                        this.ShowMessage(new UserControl.Message(MessageType.Error, newLotNo.Substring(newLotNo.IndexOf("$"))));
                        this.m_CheckFooderList.Tables["LotFooder"].Rows.Clear();
                        this.m_CheckFooderList.Tables["LotFooder"].AcceptChanges();
                        this.ultraGridFooder.DataSource = this.m_CheckFooderList;
                        return;
                    }
                }

                dr = this.m_LotFooder.NewRow();
                dr["Checked"] = false;
                dr["MOCode"] = row.Cells["MOCode"].Value.ToString();
                dr["ItemCode"] = row.Cells["ItemCode"].Value.ToString();
                dr["LotNo"] = newLotNo;
                dr["LotQty"] = row.Cells["NGQty"].Value.ToString();
                dr["GoodQty"] = 0 + "";
                dr["NGQty"] = row.Cells["NGQty"].Value.ToString();
                dr["LotStatus"] = row.Cells["LotStatus"].Value.ToString();
                dr["LotStatusHidden"] = row.Cells["LotStatusHidden"].Value.ToString();
                dr["RouteCode"] = row.Cells["RouteCode"].Value.ToString();
                dr["OPCode"] = row.Cells["OPCode"].Value.ToString();
                dr["CollectStatus"] = row.Cells["CollectStatus"].Value.ToString();
                dr["CollectStatusHidden"] = row.Cells["CollectStatusHidden"].Value.ToString();
                dr["ProductStatus"] = row.Cells["ProductStatus"].Value.ToString();
                if (decimal.Parse(row.Cells["NGQty"].Value.ToString()) < 1)
                {
                    dr["ProductStatusChecked"] = false;
                }
                else
                {
                    dr["ProductStatusChecked"] = true;
                }
                dr["IsCom"] = row.Cells["IsCom"].Value.ToString();
                dr["IsComHidden"] = row.Cells["IsComHidden"].Value.ToString();
                dr["MaintainDate"] = FormatHelper.ToDateString(dbDateTime.DBDate);
                dr["MaintainTime"] = FormatHelper.ToTimeString(dbDateTime.DBTime);
                this.m_LotFooder.Rows.Add(dr);
                ht.Add("newRecord", dr);
            }

            this.m_CheckFooderList.AcceptChanges();
            this.ultraGridFooder.DataSource = this.m_CheckFooderList;

            //设置显示和隐藏的列
            if (row.Cells["ProductStatus"].Value.ToString() != string.Empty)
            {
                this.ultraGridFooder.DisplayLayout.Bands[0].Columns["ProductStatus"].Hidden = true;
                this.ultraGridFooder.DisplayLayout.Bands[0].Columns["ProductStatusChecked"].Hidden = false;
            }
            else
            {
                this.ultraGridFooder.DisplayLayout.Bands[0].Columns["ProductStatus"].Hidden = false;
                this.ultraGridFooder.DisplayLayout.Bands[0].Columns["ProductStatusChecked"].Hidden = true;
            }
            
        }

        //原批号原信息复制两或三笔在下面的Grid中。
        private void Copy(List<UltraGridRow> list, bool saveOld)
        {
            if (this.m_LotFooder != null)
            {
                this.m_LotFooder.Rows.Clear();
            }

            if (ht.Count > 0)
            {
                ht.Clear();
            }

            DataRow dr;
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            lotQty = 0;
            ngQty = 0;
            goodQty = 0;
            for (int i = 0; i < list.Count; i++)
            {
                dr = this.m_LotFooder.NewRow();
                dr["Checked"] = false;
                dr["MOCode"] = list[i].Cells["MOCode"].Value.ToString();
                dr["ItemCode"] = list[i].Cells["ItemCode"].Value.ToString();
                dr["LotNo"] = list[i].Cells["LotNo"].Value.ToString();
                dr["LotQty"] = list[i].Cells["LotQty"].Value.ToString();
                dr["GoodQty"] = list[i].Cells["GoodQty"].Value.ToString();
                dr["NGQty"] = list[i].Cells["NGQty"].Value.ToString();
                dr["LotStatus"] = list[i].Cells["LotStatus"].Value.ToString();
                dr["LotStatusHidden"] = list[i].Cells["LotStatusHidden"].Value.ToString();
                dr["RouteCode"] = list[i].Cells["RouteCode"].Value.ToString();
                dr["OPCode"] = list[i].Cells["OPCode"].Value.ToString();
                dr["CollectStatus"] = list[i].Cells["CollectStatus"].Value.ToString();
                dr["CollectStatusHidden"] = list[i].Cells["CollectStatusHidden"].Value.ToString();
                dr["ProductStatus"] = list[i].Cells["ProductStatus"].Value.ToString();
                dr["IsCom"] = list[i].Cells["IsCom"].Value.ToString();
                dr["IsComHidden"] = list[i].Cells["IsComHidden"].Value.ToString();
                dr["MaintainDate"] = list[i].Cells["MaintainDate"].Value.ToString();
                dr["MaintainTime"] = list[i].Cells["MaintainTime"].Value.ToString();
                this.m_LotFooder.Rows.Add(dr);
                ht.Add("old" + i, dr);
                try
                {
                    lotQty += decimal.Parse(list[i].Cells["LotQty"].Value.ToString().Trim());
                }
                catch
                {
                    lotQty += 0;
                }
                try
                {
                    ngQty += decimal.Parse(list[i].Cells["NGQty"].Value.ToString().Trim());
                }
                catch
                {
                    ngQty += 0;
                }
                try
                {
                    goodQty += decimal.Parse(list[i].Cells["GoodQty"].Value.ToString().Trim());
                }
                catch
                {
                    goodQty += 0;
                }
            }

            if (saveOld)
            {
                foreach (UltraGridRow row in ultraGridFooder.Rows)
                {
                    if (row.Cells["LotNo"].Value.ToString().Trim() == this.ucLSaveOld.Value.ToString().Trim())
                    {
                        row.Cells["LotQty"].Value = lotQty;
                        row.Cells["NGQty"].Value = ngQty;
                        row.Cells["GoodQty"].Value = goodQty;
                        row.Cells["MaintainDate"].Value = FormatHelper.ToDateString(dbDateTime.DBDate);
                        row.Cells["MaintainTime"].Value = FormatHelper.ToTimeString(dbDateTime.DBTime);
                    }
                }
            }
            else
            {
                savedNewLotNo = _MOLotFacade.GenerateNewLotNo(list[0].Cells["MOCode"].Value.ToString().Trim(), list[0].Cells["ItemCode"].Value.ToString().Trim(), 1);
                if (savedNewLotNo != null)
                {
                    if (savedNewLotNo.IndexOf("False$") >= 0)
                    {
                        this.ShowMessage(new UserControl.Message(MessageType.Error, savedNewLotNo.Substring(savedNewLotNo.IndexOf("$"))));
                        this.m_CheckFooderList.Tables["LotFooder"].Rows.Clear();
                        this.m_CheckFooderList.Tables["LotFooder"].AcceptChanges();
                        this.ultraGridFooder.DataSource = this.m_CheckFooderList;
                        return;
                    }
                }

                dr = this.m_LotFooder.NewRow();
                dr["Checked"] = false;
                dr["MOCode"] = list[0].Cells["MOCode"].Value.ToString();
                dr["ItemCode"] = list[0].Cells["ItemCode"].Value.ToString();
                dr["LotNo"] = savedNewLotNo;
                dr["LotQty"] = lotQty;
                dr["GoodQty"] = goodQty;
                dr["NGQty"] = ngQty;
                dr["LotStatus"] = list[0].Cells["LotStatus"].Value.ToString();
                dr["LotStatusHidden"] = list[0].Cells["LotStatusHidden"].Value.ToString();
                dr["RouteCode"] = list[0].Cells["RouteCode"].Value.ToString();
                dr["OPCode"] = list[0].Cells["OPCode"].Value.ToString();
                dr["CollectStatus"] = list[0].Cells["CollectStatus"].Value.ToString();
                dr["CollectStatusHidden"] = list[0].Cells["CollectStatusHidden"].Value.ToString();
                dr["ProductStatus"] = list[0].Cells["ProductStatus"].Value.ToString();
                dr["IsCom"] = list[0].Cells["IsCom"].Value.ToString();
                dr["IsComHidden"] = list[0].Cells["IsComHidden"].Value.ToString();
                dr["MaintainDate"] = FormatHelper.ToDateString(dbDateTime.DBDate);
                dr["MaintainTime"] = FormatHelper.ToTimeString(dbDateTime.DBTime);
                this.m_LotFooder.Rows.Add(dr);
                ht.Add("new", dr);
            }

            this.m_CheckFooderList.AcceptChanges();
            this.ultraGridFooder.DataSource = this.m_CheckFooderList;

            //设置显示和隐藏的列
            this.ultraGridFooder.DisplayLayout.Bands[0].Columns["ProductStatus"].Hidden = false;
            this.ultraGridFooder.DisplayLayout.Bands[0].Columns["ProductStatusChecked"].Hidden = true;
        }

        //获取Grid上选中的某个批号
        private UltraGridRow GetLotNoRow()
        {
            for (int i = 0; i < this.ultraGridHead.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridHead.Rows[i];
                if (row.Band.Index == 0)
                {
                    if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                    {
                        return row;
                    }
                }
            }
            return null;
        }

        //获取Grid上选中的两个批号
        private List<UltraGridRow> GetLotNoRows()
        {
            List<UltraGridRow> list = new List<UltraGridRow>();
            for (int i = 0; i < this.ultraGridHead.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridHead.Rows[i];
                if (row.Band.Index == 0)
                {
                    if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                    {
                        list.Add(row);
                    }
                }
            }
            return list;
        }

        //检查Grid上是否唯一勾选了某个批次
        private bool CheckSelectOnlyOneLot()
        {
            int tempCount = 0;
            for (int i = 0; i < this.ultraGridHead.Rows.Count; i++)
            {
                UltraGridRow row = ultraGridHead.Rows[i];
                if (row.Band.Index == 0)
                {
                    if (Convert.ToBoolean(row.Cells["Checked"].Value))
                    {
                        object mo2LotLink = _MOLotFacade.GetMO2LotLink(row.Cells["LotNo"].Value.ToString().Trim(), row.Cells["MoCode"].Value.ToString().Trim());
                        if ((mo2LotLink == null) || (((MO2LotLink)mo2LotLink).LotStatus.Equals(LotStatusForMO2LotLink.LOTSTATUS_STOP)))
                        {
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_STOP"));//已停止使用的批不可拆批和合批
                            return false;
                        }
                        tempCount++;
                    }
                }
            }
            if (tempCount > 1)//选中的批大于一个
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_SELECTED_COUNT_THEN_ONE"));
                return false;
            }

            if (tempCount < 1)//没有选中的批
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_SELECTED"));
                return false;
            }
            return true;
        }

        //检查Grid上是否唯一勾选了两个批次
        private bool CheckSelectTwoLot()
        {
            int tempCount = 0;
            for (int i = 0; i < this.ultraGridHead.Rows.Count; i++)
            {
                UltraGridRow row = ultraGridHead.Rows[i];
                if (row.Band.Index == 0)
                {
                    if (Convert.ToBoolean(row.Cells["Checked"].Value))
                    {
                        object mo2LotLink = _MOLotFacade.GetMO2LotLink(row.Cells["LotNo"].Value.ToString().Trim(), row.Cells["MoCode"].Value.ToString().Trim());
                        if ((mo2LotLink == null) || (((MO2LotLink)mo2LotLink).LotStatus.Equals(LotStatusForMO2LotLink.LOTSTATUS_STOP)))
                        {
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_STOP"));//已停止使用的批不可拆批和合批
                            return false;
                        }
                        tempCount++;
                    }
                }
            }
            if (tempCount > 2)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_SELECTED_COUNT_THEN_TWO"));
                return false;
            }

            if (tempCount < 2)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_SELECTED_COUNT_LESS_TWO"));
                return false;
            }
            return true;
        }

        //检查合批时输入的保留批次是否为选中的二者之一
        private bool IsOneOfSelectTwoLot()
        {
            bool flag = false;
            for (int i = 0; i < this.ultraGridHead.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridHead.Rows[i];
                if (row.Band.Index == 0)
                {
                    if (Convert.ToBoolean(row.Cells["Checked"].Value) && this.ucLSaveOld.Value.ToString().Trim().Equals(row.Cells["LotNo"].Value.ToString().Trim()))
                    {
                        flag = true;
                    }
                }
            }
            if (!flag)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SAVE_OLD_LOT"));
                return false;
            }

            return true;
        }

        //两笔数据的工单代码，途程和工序必须一致
        private bool IsLotRowsSame()
        {
            List<UltraGridRow> list = this.GetLotNoRows();

            Infragistics.Win.UltraWinGrid.UltraGridRow row1 = list[0];
            Infragistics.Win.UltraWinGrid.UltraGridRow row2 = list[1];

            if (row1.Cells["MOCode"].Value.ToString() != row2.Cells["MOCode"].Value.ToString())//工单代码必须一致
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MOCode_MUST_BE_SAME"));
                return false;
            }
            if (row1.Cells["RouteCode"].Value.ToString() != row2.Cells["RouteCode"].Value.ToString())//途程必须一致
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RouteCode_MUST_BE_SAME"));
                return false;
            }
            if (row1.Cells["OPCode"].Value.ToString() != row2.Cells["OPCode"].Value.ToString())//工序必须一致
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_OPCode_MUST_BE_SAME"));
                return false;
            }

            return true;

        }

        //检查是否已投入生产，已经投入生产需要判断完工，采集状态，不良和包装
        private bool CheckLotSimulation(UltraGridRow ultraGridRow)
        {
            object lotSimulation = _DataCollectFacade.GetLotSimulation(ultraGridRow.Cells["MOCode"].Value.ToString().Trim(), ultraGridRow.Cells["LotNo"].Value.ToString().Trim());
            object lotSimulationReport = _DataCollectFacade.GetLotSimulationReport(ultraGridRow.Cells["MOCode"].Value.ToString().Trim(), ultraGridRow.Cells["LotNo"].Value.ToString().Trim());

            //检查是否已经投入生产，未投入生产的不需要进行后续检查
            if ((lotSimulation == null) && (lotSimulationReport == null))
            {
                return true;
            }

            //记录当前批的生产状态
            isNG = ((LotSimulationReport)lotSimulationReport).Status.Equals(ProductStatus.NG);

            //判断是否已经完工，1: 完工, 0: 未完工
            if (((LotSimulationReport)lotSimulationReport).IsComplete.Equals("1"))
            {
                return true;
            }

            //是否已完成当前工序采集
            if (((LotSimulation)lotSimulation).CollectStatus.Equals(CollectStatus.CollectStatus_BEGIN))
            {
                //批次在 站作业，无法进行分批合批操作
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_IN_PROCESSING " + ((LotSimulation)lotSimulation).OPCode));
                return false;
            }

            //批次当前状态是否不良，不良批次不能合批
            if ((type == "Merge") && (((LotSimulation)lotSimulation).ProductStatus.Equals(ProductStatus.NG)))
            {
                //不良批次不能进行分批合批操作
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_IS_NG"));
                return false;
            }

            //批次当前状态为GOOD和NG之外的状态不可分批合批
            if (!((((LotSimulation)lotSimulation).ProductStatus.Equals(ProductStatus.GOOD)) || (((LotSimulation)lotSimulation).ProductStatus.Equals(ProductStatus.NG))))
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_IS_ILLEGAL"));
                return false;
            }

            //判断批次是否已包装 批次已经包装，不能进行分批合批，判断暂不处理


            return true;
        }

        //检查是否已投入生产，已经投入生产需要判断完工，采集状态，不良和包装
        private bool CheckLotSimulation(List<UltraGridRow> ultraGridRows)
        {
            foreach (UltraGridRow ultraGridRow in ultraGridRows)
            {
                if (!CheckLotSimulation(ultraGridRow))
                {
                    return false;
                }
            }
            return true;
        }

        //停用批
        private bool StopLot(string moCode, string lotNo)
        {
            string maintainUser = ApplicationService.Current().LoginInfo.UserCode;
            object mo2LotLink = _MOLotFacade.GetMO2LotLink(lotNo, moCode);
            object lotSimulation = _DataCollectFacade.GetLotSimulation(moCode, lotNo);
            object lotSimulationReport = _DataCollectFacade.GetLotSimulationReport(moCode, lotNo);
            if (mo2LotLink != null)
            {
                ((MO2LotLink)mo2LotLink).LotStatus = LotStatusForMO2LotLink.LOTSTATUS_STOP;
                ((MO2LotLink)mo2LotLink).MUser = maintainUser;
                _MOLotFacade.UpdateMO2LotLink(mo2LotLink as MO2LotLink);
            }
            if (lotSimulation != null)
            {
                ((LotSimulation)lotSimulation).LotStatus = LotStatusForMO2LotLink.LOTSTATUS_STOP;
                ((LotSimulation)lotSimulation).MaintainUser = maintainUser;
                _DataCollectFacade.UpdateLotSimulation(lotSimulation as LotSimulation);
            }
            if (lotSimulationReport != null)
            {
                ((LotSimulationReport)lotSimulationReport).LotStatus = LotStatusForMO2LotLink.LOTSTATUS_STOP;
                ((LotSimulationReport)lotSimulationReport).MaintainUser = maintainUser;
                _DataCollectFacade.UpdateLotSimulationReport(lotSimulationReport as LotSimulationReport);
            }
            return true;
        }

        //产生维修记录
        private bool GenerateTSInfo(string moCode, string lotNo) 
        {
            if (isNG)
            {
                //如果批是保留的旧批，已有送修不需要产生送修
                if (lotNo.Equals(savedLotNo))
                {
                    return true;
                }

                BenQGuru.eMES.Domain.TS.TS ts = _TSFacade.QueryLastTSByRunningCard(savedLotNo) as BenQGuru.eMES.Domain.TS.TS;
                if (ts != null)
                {
                    string tsid = FormatHelper.GetUniqueID(ts.MOCode, lotNo, ts.RunningCardSequence.ToString());
                    string oldTSId = ts.TSId;

                    //TBLTS
                    ts.TSId = tsid;
                    ts.RunningCard = lotNo;
                    _TSFacade.AddTS_New(ts);

                    //TBLTSERRORCODE
                    object[] tsErrorCodes = _TSFacade.ExtraQueryTSErrorCode(oldTSId);
                    if (tsErrorCodes != null)
                    {
                        foreach (TSErrorCode tsErrorCode in tsErrorCodes)
                        {
                            tsErrorCode.TSId = tsid;
                            tsErrorCode.RunningCard = lotNo;
                            _TSFacade.AddTSErrorCode_New(tsErrorCode);
                        }
                    }                    

                    //TBLTSERRORCODE2LOC
                    object[] tsErrorCode2Locations = _TSFacade.ExtraQueryTSErrorCode2Location(oldTSId);
                    if (tsErrorCode2Locations != null)
                    {
                        foreach (TSErrorCode2Location tsErrorCode2Location in tsErrorCode2Locations)
                        {
                            tsErrorCode2Location.TSId = tsid;
                            tsErrorCode2Location.RunningCard = lotNo;
                            _TSFacade.AddTSErrorCode2Location_New(tsErrorCode2Location);
                        }
                    }                    

                    //TBLTSERRORCAUSE
                    object[] tsErrorCauses = _TSFacade.ExtraQueryTSErrorCause(oldTSId);
                    if (tsErrorCauses != null)
                    {
                        foreach (TSErrorCause tsErrorCause in tsErrorCauses)
                        {
                            tsErrorCause.TSId = tsid;
                            tsErrorCause.RunningCard = lotNo;
                            _TSFacade.AddTSErrorCause_New(tsErrorCause);
                        }
                    }                    

                    //TBLTSERRORCAUSE2LOC
                    object[] tsErrorCause2Locations = _TSFacade.ExtraQueryTSErrorCause2Location(oldTSId);
                    if (tsErrorCause2Locations != null)
                    {
                        foreach (TSErrorCause2Location tsErrorCause2Location in tsErrorCause2Locations)
                        {
                            tsErrorCause2Location.TSId = tsid;
                            tsErrorCause2Location.RunningCard = lotNo;
                            _TSFacade.AddTSErrorCause2Location(tsErrorCause2Location);
                        }
                    }                    

                    //TBLTSERRORCAUSE2EPART
                    object[] tsErrorCause2ErrorParts = _TSFacade.ExtraQueryTSErrorCause2ErrorPart(oldTSId);
                    if (tsErrorCause2ErrorParts != null)
                    {
                        foreach (TSErrorCause2ErrorPart tsErrorCause2ErrorPart in tsErrorCause2ErrorParts)
                        {
                            tsErrorCause2ErrorPart.TSId = tsid;
                            tsErrorCause2ErrorPart.RunningCard = lotNo;
                            _TSFacade.AddTSErrorCause2ErrorPart(tsErrorCause2ErrorPart);
                        }
                    }                    

                    //TBLTSERRORCAUSE2COM
                    object[] tsErrorCause2Coms = _TSFacade.ExtraQueryTSErrorCause2Com(oldTSId);
                    if (tsErrorCause2Coms != null)
                    {
                        foreach (TSErrorCause2Com tsErrorCause2Com in tsErrorCause2Coms)
                        {
                            tsErrorCause2Com.TSId = tsid;
                            tsErrorCause2Com.RunningCard = lotNo;
                            _TSFacade.AddTSErrorCause2Com(tsErrorCause2Com);
                        }
                    }                    

                }
            }
            else
            {
                //产生新的送修信息
                _DataCollectFacade.AutoCollectErrorInfo(this.DataProvider, moCode, lotNo, type.ToUpper());
            }
            
            return true;
        }

        //判断是否选择了要打印的数据
        private bool CheckISSelectRow()
        {
            bool flag = false;

            for (int i = 0; i < this.ultraGridFooder.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridFooder.Rows[i];
                if (Convert.ToBoolean(row.Cells["Checked"].Value))
                {
                    flag = true;
                }
            }

            for (int i = 0; i < this.ultraGridHead.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridHead.Rows[i];
                if (Convert.ToBoolean(row.Cells["Checked"].Value))
                {
                    flag = true;
                }
            }
            return flag;
        }

        //打印
        private void ucButtonPrint_Click(object sender, EventArgs e)
        {
            if (!CheckISSelectRow())
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_NO_ROW_SELECTED"));
                return;
            }

            //获取打印模板的路径
            if (this.ucLabelComboxPrintTemplete.SelectedIndex < 0)
            {
                this.ShowMessage("$Error_NO_TempLeteSelect");
                return;
            }
            string filePath = ((PrintTemplate)this.ucLabelComboxPrintTemplete.SelectedItemValue).TemplatePath.ToString();
            if (!filePath.ToUpper().Contains(".LAB"))
            {
                this.ShowMessage("$Error_LAB_File_Select!");
                return;
            }

            PrintLotList();
        }

        #endregion

        #region 打印

        //打印机列表
        private void LoadPrinter()
        {
            this.ucLabelComboxPrintList.Clear();

            if (System.Drawing.Printing.PrinterSettings.InstalledPrinters == null ||
                System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count == 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_PleaseInstallPrinter"));

                return;
            }

            int defaultprinter = 0;
            for (int i = 0; i < System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count; i++)
            {
                this.ucLabelComboxPrintList.AddItem(System.Drawing.Printing.PrinterSettings.InstalledPrinters[i], System.Drawing.Printing.PrinterSettings.InstalledPrinters[i]);
                System.Drawing.Printing.PrinterSettings pts = new System.Drawing.Printing.PrinterSettings();
                pts.PrinterName = System.Drawing.Printing.PrinterSettings.InstalledPrinters[i];
                if (pts.IsDefaultPrinter)
                {
                    defaultprinter = i;
                }
            }
            this.ucLabelComboxPrintList.SelectedIndex = defaultprinter;
        }

        //打印模板
        private void LoadTemplateList()
        {

            this.ucLabelComboxPrintTemplete.Clear();
            object[] objs = this.LoadTemplateListDataSource();
            if (objs == null)
            {
                this.ShowMessage("$CS_No_Data_To_Display");
                return;
            }
            _PrintTemplateList = new PrintTemplate[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                _PrintTemplateList[i] = (PrintTemplate)objs[i];
                ucLabelComboxPrintTemplete.AddItem(_PrintTemplateList[i].TemplateName, _PrintTemplateList[i]);
            }
        }

        //打印模板数据源
        private object[] LoadTemplateListDataSource()
        {
            try
            {
                PrintTemplateFacade printTemplateFacade = new PrintTemplateFacade(this.DataProvider);
                return printTemplateFacade.QueryPrintTemplate(string.Empty, string.Empty, int.MinValue, int.MaxValue);
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }

            return null;
        }

        //打印前检查数据
        private bool ValidateInput(string printer, PrintTemplate printTemplate)
        {
            if (this.ucLabelEditPrintCount.Value.Trim() == "")
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_Print_Count_Empty"));
                return false;
            }

            //模板
            if (printTemplate == null)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_PrintTemplate_Empty"));
                return false;
            }

            //打印机
            if (printer == null || printer.Trim().Length <= 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_Printer_Empty"));
                return false;
            }

            return true;
        }

        //条码打印方法
        private void PrintLotList()
        {
            try
            {
                if (this.ucLabelComboxPrintList.ComboBoxData.Items.Count == 0)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_PleaseInstallPrinter"));
                    return;
                }

                SetPrintButtonStatus(false);
                PrintTemplateFacade printTemplateFacade = new PrintTemplateFacade(this.DataProvider);
                MOFacade moFacade = new MOFacade(this.DataProvider);
                string printer = this.ucLabelComboxPrintList.SelectedItemText;
                PrintTemplate printTemplate = (PrintTemplate)this.ucLabelComboxPrintTemplete.SelectedItemValue;
                printTemplate = (PrintTemplate)printTemplateFacade.GetPrintTemplate(printTemplate.TemplateName);

                if (!System.IO.Path.IsPathRooted(printTemplate.TemplatePath))
                {
                    string ExePath = Application.StartupPath;
                    string SimplyPath = printTemplate.TemplatePath;
                    int PathIndex = SimplyPath.IndexOf("\\");
                    SimplyPath = SimplyPath.Substring(PathIndex);
                    printTemplate.TemplatePath = ExePath + SimplyPath;
                }

                //获得需要打印的数据
                List<LotSimulationForQuery> lotLinkList = new List<LotSimulationForQuery>();
                LotSimulationForQuery lotSimulationForQuery = null;
                for (int i = 0; i < ultraGridFooder.Rows.Count; i++)
                {
                    UltraGridRow row = ultraGridFooder.Rows[i];

                    if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                    {
                        lotSimulationForQuery = new LotSimulationForQuery();
                        lotSimulationForQuery.MOCode = row.Cells["MOCode"].Value.ToString();
                        lotSimulationForQuery.ItemCode = row.Cells["ItemCode"].Value.ToString();
                        lotSimulationForQuery.LotCode = row.Cells["LotNo"].Value.ToString();
                        lotLinkList.Add(lotSimulationForQuery as LotSimulationForQuery);
                    }

                }
                for (int i = 0; i < ultraGridHead.Rows.Count; i++)
                {
                    UltraGridRow row = ultraGridHead.Rows[i];

                    if (row.Band.Index == 0)
                    {
                        if (Convert.ToBoolean(row.Cells["Checked"].Value))
                        {
                            lotSimulationForQuery = new LotSimulationForQuery();
                            lotSimulationForQuery.MOCode = row.Cells["MOCode"].Value.ToString();
                            lotSimulationForQuery.ItemCode = row.Cells["ItemCode"].Value.ToString();
                            lotSimulationForQuery.LotCode = row.Cells["LotNo"].Value.ToString();
                            lotLinkList.Add(lotSimulationForQuery as LotSimulationForQuery);
                        }
                    }
                }
                if (lotLinkList.Count == 0)
                {
                    return;
                }

                //打印前打印数据检查
                if (!ValidateInput(printer, printTemplate))
                {
                    return;
                }

                for (int i = 0; i < int.Parse(this.ucLabelEditPrintCount.Value.Trim()); i++)
                {
                    Messages msg = this.Print(printer, printTemplate.TemplatePath, lotLinkList);
                    if (msg.IsSuccess())
                    {
                        //打印后的数据处理
                        try
                        {
                            string userCode = ApplicationService.Current().UserCode;
                            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                            this.DataProvider.BeginTransaction();
                            foreach (LotSimulationForQuery lotLink in lotLinkList)
                            {
                                object obj = _MOLotFacade.GetMO2LotLink(lotLink.LotCode, lotSimulationForQuery.MOCode);
                                if (obj != null)
                                {
                                    ((MO2LotLink)obj).PrintTimes++;
                                    ((MO2LotLink)obj).LastPrintUser = userCode;
                                    ((MO2LotLink)obj).LastPrintDate = dbDateTime.DBDate;
                                    ((MO2LotLink)obj).LastPrintTime = dbDateTime.DBTime;

                                    _MOLotFacade.UpdateMO2LotLink(obj as MO2LotLink);
                                }
                                
                            }
                            this.DataProvider.CommitTransaction();
                        }
                        catch (System.Exception ex)
                        {
                            this.DataProvider.RollbackTransaction();

                            this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                            return;
                        }
                    }
                    this.ShowMessage(msg);
                }
            }
            finally
            {
                SetPrintButtonStatus(true);
            }
        }

        //打印
        public UserControl.Messages Print(string printer, string templatePath, List<LotSimulationForQuery> lotLinkList)
        {
            UserControl.Messages messages = new UserControl.Messages();
            //CodeSoftPrintData _CodeSoftPrintData = new CodeSoftPrintData();
            CodeSoftFacade _CodeSoftFacade = new CodeSoftFacade();
            CodeSoftPrintFacade _CodeSoftPrintFacade = new CodeSoftPrintFacade(this.DataProvider);
            try
            {
                try
                {
                    _CodeSoftPrintFacade.PrePrint();
                    _CodeSoftFacade.OpenTemplate(printer, templatePath);
                }
                catch (System.Exception ex)
                {
                    messages.Add(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                    return messages;
                }

                //批量打印前生成文本文件
                string strBatchDataFile = string.Empty;
                if (_IsBatchPrint)
                {
                    strBatchDataFile = _CodeSoftPrintFacade.CreateFile();
                }

                //填充打印的变量值                
                for (int i = 0; i < lotLinkList.Count; i++)
                {
                    LotSimulationForQuery lotLink = (LotSimulationForQuery)lotLinkList[i];

                    if (!ht.ContainsKey(lotLink.MOCode))
                    {
                        MO mo = _MOFacade.GetMO(lotLink.MOCode) as MO;
                        Item item = _ItemFacade.GetItem(mo.ItemCode, mo.OrganizationID) as Item;
                        ht.Add(lotLink.MOCode, item);
                    }

                    LabelPrintVars labelPrintVars = new LabelPrintVars();
                    string[] vars = new string[0];
                    if (messages.IsSuccess())
                    {
                        try
                        {
                            //要传给Codesoft的数组，字段顺序不能修改
                            vars = _CodeSoftPrintFacade.GetPrintVars(lotLink.LotCode, (ht[lotLink.MOCode] as Item).ItemCode, (ht[lotLink.MOCode] as Item).ItemName, lotLink.MOCode, "", "");

                            //批量打印前的写文件
                            if (_IsBatchPrint)
                            {
                                string[] printVars = _CodeSoftPrintFacade.ProcessVars(vars, labelPrintVars);
                                _CodeSoftPrintFacade.WriteFile(strBatchDataFile, printVars);
                            }
                            //直接打印
                            else
                            {
                                _CodeSoftFacade.LabelPrintVars = labelPrintVars;
                                _CodeSoftFacade.Print(vars);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            messages.Add(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                            return messages;
                        }
                    }
                }
                //批量打印
                if (_IsBatchPrint)
                {
                    try
                    {
                        _CodeSoftFacade.Print(strBatchDataFile, _CodeSoftPrintFacade.GetDataDescPath(_DataDescFileName));
                    }
                    catch (System.Exception ex)
                    {
                        messages.Add(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                        return messages;
                    }
                }

                messages.Add(new UserControl.Message(UserControl.MessageType.Success, "$Success_Print_Label"));
            }
            finally
            {
                _CodeSoftFacade.ReleaseCom();
            }

            return messages;
        }

        //控制打印按钮状态，防止多次点击
        private void SetPrintButtonStatus(bool enabled)
        {
            this.ucButtonPrint.Enabled = enabled;
            if (enabled)
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
            else
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            }
        }

        #endregion

        #region Base Messages
        protected void ShowMessage(string message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        protected void ShowMessage(Exception e)
        {
            ApplicationRun.GetInfoForm().Add(new UserControl.Message(e));
        }

        protected void ShowMessage(Messages messages)
        {
            ApplicationRun.GetInfoForm().Add(messages);
        }

        protected void ShowMessage(UserControl.Message message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }
        #endregion

    }
}
