using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Infragistics.Win.UltraWinGrid;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.CodeSoftPrint;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Domain.ATE;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class FPrintLabel : BaseForm
    {
        private System.ComponentModel.IContainer components;
        private Panel panel1;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetPrintBy;
        private UCButton ucButtonQuery;
        private Label labelItemDesc;
        private UCLabelEdit ucLabelEditItemCode;
        private UCLabelEdit ucLabelEditRCard;
        private Panel panelMain;
        private UCLabelEdit ucLabelEditQtyPrinted;
        private UCLabelEdit ucLabelEditQtyToPrint;
        private UCLabelEdit ucLabelEditTotal;
        private UCButton btnCancel;
        private UCButton ucButtonExit;
        private UCButton ucButtonPrint;
        private UCLabelCombox ucLabelComboxPrinter;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridPrintTemplate;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridRCardList;
        private ListBox listBoxRCardRange;
        private Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetScale;
        private System.Data.DataTable dataTableMKeypartDetial;
        private System.Data.DataTable dataTablePrintTemplate;
        private System.Data.DataColumn Check1;
        private System.Data.DataColumn RunningCard;
        private System.Data.DataColumn PrintTimes;
        private System.Data.DataColumn Check2;
        private System.Data.DataColumn TemplateName;
        private System.Data.DataColumn TemplateDesc;
        private System.Data.DataSet dataSetMKeypartDetial;
        private Label lblPrintTemplateList;
        private Label lblRCardList;
        private Label lblRCardRange;
        private CheckBox checkBoxSelectAll;
        private UCLabelEdit ucLabelEditReserve6;
        private UCLabelEdit ucLabelEditReserve2;
        private UCLabelEdit ucLabelEditReserve5;
        private UCLabelEdit ucLabelEditReserve1;
        private UCLabelEdit ucLabelEditReserve8;
        private UCLabelEdit ucLabelEditReserve4;
        private UCLabelEdit ucLabelEditReserve7;
        private UCLabelEdit ucLabelEditReserve3;
        private System.Data.DataSet dataSetPrintTemplate;

        public FPrintLabel()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
            UserControl.UIStyleBuilder.FormUI(this);
        }

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
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
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPrintLabel));
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("dataTablePrintTemplate", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Check");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateDesc");
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("dataTableMKeypartDetial", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Check");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SerialNo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PrintTimes");
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.ultraOptionSetScale = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ultraOptionSetPrintBy = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.ucButtonQuery = new UserControl.UCButton();
            this.labelItemDesc = new System.Windows.Forms.Label();
            this.ucLabelEditItemCode = new UserControl.UCLabelEdit();
            this.ucLabelEditRCard = new UserControl.UCLabelEdit();
            this.panelMain = new System.Windows.Forms.Panel();
            this.ucLabelEditReserve8 = new UserControl.UCLabelEdit();
            this.ucLabelEditReserve4 = new UserControl.UCLabelEdit();
            this.ucLabelEditReserve7 = new UserControl.UCLabelEdit();
            this.ucLabelEditReserve3 = new UserControl.UCLabelEdit();
            this.ucLabelEditReserve6 = new UserControl.UCLabelEdit();
            this.ucLabelEditReserve2 = new UserControl.UCLabelEdit();
            this.ucLabelEditReserve5 = new UserControl.UCLabelEdit();
            this.ucLabelEditReserve1 = new UserControl.UCLabelEdit();
            this.checkBoxSelectAll = new System.Windows.Forms.CheckBox();
            this.lblPrintTemplateList = new System.Windows.Forms.Label();
            this.lblRCardList = new System.Windows.Forms.Label();
            this.lblRCardRange = new System.Windows.Forms.Label();
            this.listBoxRCardRange = new System.Windows.Forms.ListBox();
            this.ultraGridPrintTemplate = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dataTablePrintTemplate = new System.Data.DataTable();
            this.Check2 = new System.Data.DataColumn();
            this.TemplateName = new System.Data.DataColumn();
            this.TemplateDesc = new System.Data.DataColumn();
            this.ultraGridRCardList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dataTableMKeypartDetial = new System.Data.DataTable();
            this.Check1 = new System.Data.DataColumn();
            this.RunningCard = new System.Data.DataColumn();
            this.PrintTimes = new System.Data.DataColumn();
            this.ucLabelEditQtyPrinted = new UserControl.UCLabelEdit();
            this.ucLabelEditQtyToPrint = new UserControl.UCLabelEdit();
            this.ucLabelEditTotal = new UserControl.UCLabelEdit();
            this.btnCancel = new UserControl.UCButton();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonPrint = new UserControl.UCButton();
            this.ucLabelComboxPrinter = new UserControl.UCLabelCombox();
            this.dataSetMKeypartDetial = new System.Data.DataSet();
            this.dataSetPrintTemplate = new System.Data.DataSet();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetScale)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetPrintBy)).BeginInit();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridPrintTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTablePrintTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridRCardList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableMKeypartDetial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetMKeypartDetial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetPrintTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraOptionSetScale
            // 
            this.ultraOptionSetScale.Location = new System.Drawing.Point(0, 0);
            this.ultraOptionSetScale.Name = "ultraOptionSetScale";
            this.ultraOptionSetScale.Size = new System.Drawing.Size(96, 32);
            this.ultraOptionSetScale.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ultraOptionSetPrintBy);
            this.panel1.Controls.Add(this.ucButtonQuery);
            this.panel1.Controls.Add(this.labelItemDesc);
            this.panel1.Controls.Add(this.ucLabelEditItemCode);
            this.panel1.Controls.Add(this.ucLabelEditRCard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 88);
            this.panel1.TabIndex = 200;
            // 
            // ultraOptionSetPrintBy
            // 
            appearance2.FontData.BoldAsString = "False";
            this.ultraOptionSetPrintBy.Appearance = appearance2;
            this.ultraOptionSetPrintBy.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.ultraOptionSetPrintBy.CausesValidation = false;
            this.ultraOptionSetPrintBy.CheckedIndex = 0;
            valueListItem1.DataValue = "0";
            valueListItem1.DisplayText = "按照单个序列号打印";
            valueListItem2.DataValue = "1";
            valueListItem2.DisplayText = "按照产品打印";
            this.ultraOptionSetPrintBy.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2});
            this.ultraOptionSetPrintBy.ItemSpacingVertical = 12;
            this.ultraOptionSetPrintBy.Location = new System.Drawing.Point(14, 11);
            this.ultraOptionSetPrintBy.Name = "ultraOptionSetPrintBy";
            this.ultraOptionSetPrintBy.Size = new System.Drawing.Size(135, 48);
            this.ultraOptionSetPrintBy.TabIndex = 100;
            this.ultraOptionSetPrintBy.Text = "按照单个序列号打印";
            this.ultraOptionSetPrintBy.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.ultraOptionSetPrintBy.ValueChanged += new System.EventHandler(this.ultraOptionSetPrintBy_ValueChanged);
            // 
            // ucButtonQuery
            // 
            this.ucButtonQuery.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonQuery.BackgroundImage")));
            this.ucButtonQuery.ButtonType = UserControl.ButtonTypes.Query;
            this.ucButtonQuery.Caption = "查询";
            this.ucButtonQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonQuery.Location = new System.Drawing.Point(507, 11);
            this.ucButtonQuery.Name = "ucButtonQuery";
            this.ucButtonQuery.Size = new System.Drawing.Size(88, 22);
            this.ucButtonQuery.TabIndex = 3;
            this.ucButtonQuery.Click += new System.EventHandler(this.ucButtonQuery_Click);
            // 
            // labelItemDesc
            // 
            this.labelItemDesc.Location = new System.Drawing.Point(203, 66);
            this.labelItemDesc.Name = "labelItemDesc";
            this.labelItemDesc.Size = new System.Drawing.Size(392, 18);
            this.labelItemDesc.TabIndex = 45;
            this.labelItemDesc.Text = "产品描述";
            // 
            // ucLabelEditItemCode
            // 
            this.ucLabelEditItemCode.AllowEditOnlyChecked = true;
            this.ucLabelEditItemCode.AutoSelectAll = false;
            this.ucLabelEditItemCode.AutoSize = true;
            this.ucLabelEditItemCode.AutoUpper = true;
            this.ucLabelEditItemCode.Caption = "产品名称";
            this.ucLabelEditItemCode.Checked = false;
            this.ucLabelEditItemCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditItemCode.Location = new System.Drawing.Point(163, 41);
            this.ucLabelEditItemCode.MaxLength = 40;
            this.ucLabelEditItemCode.Multiline = false;
            this.ucLabelEditItemCode.Name = "ucLabelEditItemCode";
            this.ucLabelEditItemCode.PasswordChar = '\0';
            this.ucLabelEditItemCode.ReadOnly = false;
            this.ucLabelEditItemCode.ShowCheckBox = false;
            this.ucLabelEditItemCode.Size = new System.Drawing.Size(261, 22);
            this.ucLabelEditItemCode.TabIndex = 2;
            this.ucLabelEditItemCode.TabNext = false;
            this.ucLabelEditItemCode.Value = "";
            this.ucLabelEditItemCode.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditItemCode.XAlign = 224;
            this.ucLabelEditItemCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditItemCode_TxtboxKeyPress);
            // 
            // ucLabelEditRCard
            // 
            this.ucLabelEditRCard.AllowEditOnlyChecked = true;
            this.ucLabelEditRCard.AutoSelectAll = false;
            this.ucLabelEditRCard.AutoUpper = true;
            this.ucLabelEditRCard.Caption = "序列号";
            this.ucLabelEditRCard.Checked = false;
            this.ucLabelEditRCard.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRCard.Location = new System.Drawing.Point(175, 13);
            this.ucLabelEditRCard.MaxLength = 40;
            this.ucLabelEditRCard.Multiline = false;
            this.ucLabelEditRCard.Name = "ucLabelEditRCard";
            this.ucLabelEditRCard.PasswordChar = '\0';
            this.ucLabelEditRCard.ReadOnly = false;
            this.ucLabelEditRCard.ShowCheckBox = false;
            this.ucLabelEditRCard.Size = new System.Drawing.Size(249, 22);
            this.ucLabelEditRCard.TabIndex = 1;
            this.ucLabelEditRCard.TabNext = false;
            this.ucLabelEditRCard.Value = "";
            this.ucLabelEditRCard.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditRCard.XAlign = 224;
            this.ucLabelEditRCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditRCard_TxtboxKeyPress);
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.Controls.Add(this.ucLabelEditReserve8);
            this.panelMain.Controls.Add(this.ucLabelEditReserve4);
            this.panelMain.Controls.Add(this.ucLabelEditReserve7);
            this.panelMain.Controls.Add(this.ucLabelEditReserve3);
            this.panelMain.Controls.Add(this.ucLabelEditReserve6);
            this.panelMain.Controls.Add(this.ucLabelEditReserve2);
            this.panelMain.Controls.Add(this.ucLabelEditReserve5);
            this.panelMain.Controls.Add(this.ucLabelEditReserve1);
            this.panelMain.Controls.Add(this.checkBoxSelectAll);
            this.panelMain.Controls.Add(this.lblPrintTemplateList);
            this.panelMain.Controls.Add(this.lblRCardList);
            this.panelMain.Controls.Add(this.lblRCardRange);
            this.panelMain.Controls.Add(this.listBoxRCardRange);
            this.panelMain.Controls.Add(this.ultraGridPrintTemplate);
            this.panelMain.Controls.Add(this.ultraGridRCardList);
            this.panelMain.Controls.Add(this.ucLabelEditQtyPrinted);
            this.panelMain.Controls.Add(this.ucLabelEditQtyToPrint);
            this.panelMain.Controls.Add(this.ucLabelEditTotal);
            this.panelMain.Controls.Add(this.btnCancel);
            this.panelMain.Controls.Add(this.ucButtonExit);
            this.panelMain.Controls.Add(this.ucButtonPrint);
            this.panelMain.Controls.Add(this.ucLabelComboxPrinter);
            this.panelMain.Location = new System.Drawing.Point(0, 88);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(824, 476);
            this.panelMain.TabIndex = 202;
            // 
            // ucLabelEditReserve8
            // 
            this.ucLabelEditReserve8.AllowEditOnlyChecked = true;
            this.ucLabelEditReserve8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditReserve8.AutoSelectAll = false;
            this.ucLabelEditReserve8.AutoUpper = true;
            this.ucLabelEditReserve8.Caption = "保留8";
            this.ucLabelEditReserve8.Checked = false;
            this.ucLabelEditReserve8.EditType = UserControl.EditTypes.String;
            this.ucLabelEditReserve8.Location = new System.Drawing.Point(608, 394);
            this.ucLabelEditReserve8.MaxLength = 40;
            this.ucLabelEditReserve8.Multiline = false;
            this.ucLabelEditReserve8.Name = "ucLabelEditReserve8";
            this.ucLabelEditReserve8.PasswordChar = '\0';
            this.ucLabelEditReserve8.ReadOnly = false;
            this.ucLabelEditReserve8.ShowCheckBox = false;
            this.ucLabelEditReserve8.Size = new System.Drawing.Size(143, 22);
            this.ucLabelEditReserve8.TabIndex = 21;
            this.ucLabelEditReserve8.TabNext = true;
            this.ucLabelEditReserve8.TabStop = false;
            this.ucLabelEditReserve8.Value = "";
            this.ucLabelEditReserve8.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditReserve8.XAlign = 651;
            // 
            // ucLabelEditReserve4
            // 
            this.ucLabelEditReserve4.AllowEditOnlyChecked = true;
            this.ucLabelEditReserve4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditReserve4.AutoSelectAll = false;
            this.ucLabelEditReserve4.AutoUpper = true;
            this.ucLabelEditReserve4.Caption = "保留4";
            this.ucLabelEditReserve4.Checked = false;
            this.ucLabelEditReserve4.EditType = UserControl.EditTypes.String;
            this.ucLabelEditReserve4.Location = new System.Drawing.Point(608, 366);
            this.ucLabelEditReserve4.MaxLength = 40;
            this.ucLabelEditReserve4.Multiline = false;
            this.ucLabelEditReserve4.Name = "ucLabelEditReserve4";
            this.ucLabelEditReserve4.PasswordChar = '\0';
            this.ucLabelEditReserve4.ReadOnly = false;
            this.ucLabelEditReserve4.ShowCheckBox = false;
            this.ucLabelEditReserve4.Size = new System.Drawing.Size(143, 22);
            this.ucLabelEditReserve4.TabIndex = 17;
            this.ucLabelEditReserve4.TabNext = true;
            this.ucLabelEditReserve4.TabStop = false;
            this.ucLabelEditReserve4.Value = "";
            this.ucLabelEditReserve4.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditReserve4.XAlign = 651;
            this.ucLabelEditReserve4.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditReserve4_TxtboxKeyPress);
            // 
            // ucLabelEditReserve7
            // 
            this.ucLabelEditReserve7.AllowEditOnlyChecked = true;
            this.ucLabelEditReserve7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditReserve7.AutoSelectAll = false;
            this.ucLabelEditReserve7.AutoUpper = true;
            this.ucLabelEditReserve7.Caption = "保留7";
            this.ucLabelEditReserve7.Checked = false;
            this.ucLabelEditReserve7.EditType = UserControl.EditTypes.String;
            this.ucLabelEditReserve7.Location = new System.Drawing.Point(424, 394);
            this.ucLabelEditReserve7.MaxLength = 40;
            this.ucLabelEditReserve7.Multiline = false;
            this.ucLabelEditReserve7.Name = "ucLabelEditReserve7";
            this.ucLabelEditReserve7.PasswordChar = '\0';
            this.ucLabelEditReserve7.ReadOnly = false;
            this.ucLabelEditReserve7.ShowCheckBox = false;
            this.ucLabelEditReserve7.Size = new System.Drawing.Size(143, 22);
            this.ucLabelEditReserve7.TabIndex = 20;
            this.ucLabelEditReserve7.TabNext = true;
            this.ucLabelEditReserve7.TabStop = false;
            this.ucLabelEditReserve7.Value = "";
            this.ucLabelEditReserve7.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditReserve7.XAlign = 467;
            this.ucLabelEditReserve7.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditReserve7_TxtboxKeyPress);
            // 
            // ucLabelEditReserve3
            // 
            this.ucLabelEditReserve3.AllowEditOnlyChecked = true;
            this.ucLabelEditReserve3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditReserve3.AutoSelectAll = false;
            this.ucLabelEditReserve3.AutoUpper = true;
            this.ucLabelEditReserve3.Caption = "保留3";
            this.ucLabelEditReserve3.Checked = false;
            this.ucLabelEditReserve3.EditType = UserControl.EditTypes.String;
            this.ucLabelEditReserve3.Location = new System.Drawing.Point(424, 366);
            this.ucLabelEditReserve3.MaxLength = 40;
            this.ucLabelEditReserve3.Multiline = false;
            this.ucLabelEditReserve3.Name = "ucLabelEditReserve3";
            this.ucLabelEditReserve3.PasswordChar = '\0';
            this.ucLabelEditReserve3.ReadOnly = false;
            this.ucLabelEditReserve3.ShowCheckBox = false;
            this.ucLabelEditReserve3.Size = new System.Drawing.Size(143, 22);
            this.ucLabelEditReserve3.TabIndex = 16;
            this.ucLabelEditReserve3.TabNext = true;
            this.ucLabelEditReserve3.TabStop = false;
            this.ucLabelEditReserve3.Value = "";
            this.ucLabelEditReserve3.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditReserve3.XAlign = 467;
            this.ucLabelEditReserve3.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditReserve3_TxtboxKeyPress);
            // 
            // ucLabelEditReserve6
            // 
            this.ucLabelEditReserve6.AllowEditOnlyChecked = true;
            this.ucLabelEditReserve6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditReserve6.AutoSelectAll = false;
            this.ucLabelEditReserve6.AutoUpper = true;
            this.ucLabelEditReserve6.Caption = "保留6";
            this.ucLabelEditReserve6.Checked = false;
            this.ucLabelEditReserve6.EditType = UserControl.EditTypes.String;
            this.ucLabelEditReserve6.Location = new System.Drawing.Point(238, 394);
            this.ucLabelEditReserve6.MaxLength = 40;
            this.ucLabelEditReserve6.Multiline = false;
            this.ucLabelEditReserve6.Name = "ucLabelEditReserve6";
            this.ucLabelEditReserve6.PasswordChar = '\0';
            this.ucLabelEditReserve6.ReadOnly = false;
            this.ucLabelEditReserve6.ShowCheckBox = false;
            this.ucLabelEditReserve6.Size = new System.Drawing.Size(143, 22);
            this.ucLabelEditReserve6.TabIndex = 19;
            this.ucLabelEditReserve6.TabNext = true;
            this.ucLabelEditReserve6.TabStop = false;
            this.ucLabelEditReserve6.Value = "";
            this.ucLabelEditReserve6.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditReserve6.XAlign = 281;
            this.ucLabelEditReserve6.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditReserve6_TxtboxKeyPress);
            // 
            // ucLabelEditReserve2
            // 
            this.ucLabelEditReserve2.AllowEditOnlyChecked = true;
            this.ucLabelEditReserve2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditReserve2.AutoSelectAll = false;
            this.ucLabelEditReserve2.AutoUpper = true;
            this.ucLabelEditReserve2.Caption = "保留2";
            this.ucLabelEditReserve2.Checked = false;
            this.ucLabelEditReserve2.EditType = UserControl.EditTypes.String;
            this.ucLabelEditReserve2.Location = new System.Drawing.Point(238, 366);
            this.ucLabelEditReserve2.MaxLength = 40;
            this.ucLabelEditReserve2.Multiline = false;
            this.ucLabelEditReserve2.Name = "ucLabelEditReserve2";
            this.ucLabelEditReserve2.PasswordChar = '\0';
            this.ucLabelEditReserve2.ReadOnly = false;
            this.ucLabelEditReserve2.ShowCheckBox = false;
            this.ucLabelEditReserve2.Size = new System.Drawing.Size(143, 22);
            this.ucLabelEditReserve2.TabIndex = 15;
            this.ucLabelEditReserve2.TabNext = true;
            this.ucLabelEditReserve2.TabStop = false;
            this.ucLabelEditReserve2.Value = "";
            this.ucLabelEditReserve2.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditReserve2.XAlign = 281;
            this.ucLabelEditReserve2.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditReserve2_TxtboxKeyPress);
            // 
            // ucLabelEditReserve5
            // 
            this.ucLabelEditReserve5.AllowEditOnlyChecked = true;
            this.ucLabelEditReserve5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditReserve5.AutoSelectAll = false;
            this.ucLabelEditReserve5.AutoUpper = true;
            this.ucLabelEditReserve5.Caption = "保留5";
            this.ucLabelEditReserve5.Checked = false;
            this.ucLabelEditReserve5.EditType = UserControl.EditTypes.String;
            this.ucLabelEditReserve5.Location = new System.Drawing.Point(64, 394);
            this.ucLabelEditReserve5.MaxLength = 40;
            this.ucLabelEditReserve5.Multiline = false;
            this.ucLabelEditReserve5.Name = "ucLabelEditReserve5";
            this.ucLabelEditReserve5.PasswordChar = '\0';
            this.ucLabelEditReserve5.ReadOnly = false;
            this.ucLabelEditReserve5.ShowCheckBox = false;
            this.ucLabelEditReserve5.Size = new System.Drawing.Size(143, 22);
            this.ucLabelEditReserve5.TabIndex = 18;
            this.ucLabelEditReserve5.TabNext = true;
            this.ucLabelEditReserve5.TabStop = false;
            this.ucLabelEditReserve5.Value = "";
            this.ucLabelEditReserve5.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditReserve5.XAlign = 107;
            this.ucLabelEditReserve5.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditReserve5_TxtboxKeyPress);
            // 
            // ucLabelEditReserve1
            // 
            this.ucLabelEditReserve1.AllowEditOnlyChecked = true;
            this.ucLabelEditReserve1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditReserve1.AutoSelectAll = false;
            this.ucLabelEditReserve1.AutoUpper = true;
            this.ucLabelEditReserve1.Caption = "保留1";
            this.ucLabelEditReserve1.Checked = false;
            this.ucLabelEditReserve1.EditType = UserControl.EditTypes.String;
            this.ucLabelEditReserve1.Location = new System.Drawing.Point(64, 366);
            this.ucLabelEditReserve1.MaxLength = 40;
            this.ucLabelEditReserve1.Multiline = false;
            this.ucLabelEditReserve1.Name = "ucLabelEditReserve1";
            this.ucLabelEditReserve1.PasswordChar = '\0';
            this.ucLabelEditReserve1.ReadOnly = false;
            this.ucLabelEditReserve1.ShowCheckBox = false;
            this.ucLabelEditReserve1.Size = new System.Drawing.Size(143, 22);
            this.ucLabelEditReserve1.TabIndex = 14;
            this.ucLabelEditReserve1.TabNext = true;
            this.ucLabelEditReserve1.TabStop = false;
            this.ucLabelEditReserve1.Value = "";
            this.ucLabelEditReserve1.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditReserve1.XAlign = 107;
            this.ucLabelEditReserve1.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditReserve1_TxtboxKeyPress);
            // 
            // checkBoxSelectAll
            // 
            this.checkBoxSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxSelectAll.Location = new System.Drawing.Point(242, 238);
            this.checkBoxSelectAll.Name = "checkBoxSelectAll";
            this.checkBoxSelectAll.Size = new System.Drawing.Size(81, 24);
            this.checkBoxSelectAll.TabIndex = 49;
            this.checkBoxSelectAll.Text = "全选";
            this.checkBoxSelectAll.CheckedChanged += new System.EventHandler(this.checkBoxSelectAll_CheckedChanged);
            // 
            // lblPrintTemplateList
            // 
            this.lblPrintTemplateList.Location = new System.Drawing.Point(537, 12);
            this.lblPrintTemplateList.Name = "lblPrintTemplateList";
            this.lblPrintTemplateList.Size = new System.Drawing.Size(195, 18);
            this.lblPrintTemplateList.TabIndex = 48;
            this.lblPrintTemplateList.Text = "打印模板列表";
            // 
            // lblRCardList
            // 
            this.lblRCardList.Location = new System.Drawing.Point(238, 12);
            this.lblRCardList.Name = "lblRCardList";
            this.lblRCardList.Size = new System.Drawing.Size(195, 18);
            this.lblRCardList.TabIndex = 47;
            this.lblRCardList.Text = "序列号列表";
            // 
            // lblRCardRange
            // 
            this.lblRCardRange.Location = new System.Drawing.Point(12, 12);
            this.lblRCardRange.Name = "lblRCardRange";
            this.lblRCardRange.Size = new System.Drawing.Size(195, 18);
            this.lblRCardRange.TabIndex = 46;
            this.lblRCardRange.Text = "序列号范围";
            // 
            // listBoxRCardRange
            // 
            this.listBoxRCardRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxRCardRange.FormattingEnabled = true;
            this.listBoxRCardRange.ItemHeight = 12;
            this.listBoxRCardRange.Location = new System.Drawing.Point(14, 33);
            this.listBoxRCardRange.Name = "listBoxRCardRange";
            this.listBoxRCardRange.Size = new System.Drawing.Size(193, 256);
            this.listBoxRCardRange.TabIndex = 4;
            this.listBoxRCardRange.SelectedIndexChanged += new System.EventHandler(this.listBoxRCardRange_SelectedIndexChanged);
            // 
            // ultraGridPrintTemplate
            // 
            this.ultraGridPrintTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ultraGridPrintTemplate.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridPrintTemplate.DataSource = this.dataTablePrintTemplate;
            appearance4.BackColor = System.Drawing.Color.White;
            this.ultraGridPrintTemplate.DisplayLayout.Appearance = appearance4;
            ultraGridBand1.AddButtonCaption = "Table1";
            ultraGridColumn1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn1.Header.Caption = "";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            ultraGridColumn1.Width = 20;
            ultraGridColumn2.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn2.Header.Caption = "模板名称";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 100;
            ultraGridColumn3.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn3.Header.Caption = "模板描述";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 100;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3});
            this.ultraGridPrintTemplate.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance5.FontData.BoldAsString = "True";
            appearance5.TextHAlignAsString = "Left";
            this.ultraGridPrintTemplate.DisplayLayout.CaptionAppearance = appearance5;
            this.ultraGridPrintTemplate.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.ultraGridPrintTemplate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridPrintTemplate.Location = new System.Drawing.Point(540, 33);
            this.ultraGridPrintTemplate.Name = "ultraGridPrintTemplate";
            this.ultraGridPrintTemplate.Size = new System.Drawing.Size(264, 256);
            this.ultraGridPrintTemplate.TabIndex = 6;
            this.ultraGridPrintTemplate.AfterCellActivate += new System.EventHandler(this.ultraGridPrintTemplate_AfterCellActivate);
            this.ultraGridPrintTemplate.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridPrintTemplate_CellChange);
            // 
            // dataTablePrintTemplate
            // 
            this.dataTablePrintTemplate.Columns.AddRange(new System.Data.DataColumn[] {
            this.Check2,
            this.TemplateName,
            this.TemplateDesc});
            this.dataTablePrintTemplate.TableName = "dataTablePrintTemplate";
            // 
            // Check2
            // 
            this.Check2.ColumnName = "Check";
            // 
            // TemplateName
            // 
            this.TemplateName.ColumnName = "TemplateName";
            // 
            // TemplateDesc
            // 
            this.TemplateDesc.ColumnName = "TemplateDesc";
            // 
            // ultraGridRCardList
            // 
            this.ultraGridRCardList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ultraGridRCardList.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridRCardList.DataSource = this.dataTableMKeypartDetial;
            appearance6.BackColor = System.Drawing.Color.White;
            this.ultraGridRCardList.DisplayLayout.Appearance = appearance6;
            ultraGridBand2.AddButtonCaption = "Table1";
            ultraGridColumn4.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn4.Header.Caption = "";
            ultraGridColumn4.Header.VisiblePosition = 0;
            ultraGridColumn4.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            ultraGridColumn4.Width = 20;
            ultraGridColumn5.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn5.Header.Caption = "序列号";
            ultraGridColumn5.Header.VisiblePosition = 1;
            ultraGridColumn5.Width = 100;
            ultraGridColumn6.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn6.Header.Caption = "打印次数";
            ultraGridColumn6.Header.VisiblePosition = 2;
            ultraGridColumn6.Width = 100;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6});
            this.ultraGridRCardList.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance7.FontData.BoldAsString = "True";
            appearance7.TextHAlignAsString = "Left";
            this.ultraGridRCardList.DisplayLayout.CaptionAppearance = appearance7;
            this.ultraGridRCardList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
            this.ultraGridRCardList.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridRCardList.Location = new System.Drawing.Point(241, 33);
            this.ultraGridRCardList.Name = "ultraGridRCardList";
            this.ultraGridRCardList.Size = new System.Drawing.Size(264, 203);
            this.ultraGridRCardList.TabIndex = 5;
            this.ultraGridRCardList.AfterCellActivate += new System.EventHandler(this.ultraGridRCardList_AfterCellActivate);
            this.ultraGridRCardList.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridRCardList_CellChange);
            // 
            // dataTableMKeypartDetial
            // 
            this.dataTableMKeypartDetial.Columns.AddRange(new System.Data.DataColumn[] {
            this.Check1,
            this.RunningCard,
            this.PrintTimes});
            this.dataTableMKeypartDetial.TableName = "dataTableMKeypartDetial";
            // 
            // Check1
            // 
            this.Check1.ColumnName = "Check";
            // 
            // RunningCard
            // 
            this.RunningCard.ColumnName = "SerialNo";
            // 
            // PrintTimes
            // 
            this.PrintTimes.ColumnName = "PrintTimes";
            // 
            // ucLabelEditQtyPrinted
            // 
            this.ucLabelEditQtyPrinted.AllowEditOnlyChecked = true;
            this.ucLabelEditQtyPrinted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditQtyPrinted.AutoSelectAll = false;
            this.ucLabelEditQtyPrinted.AutoUpper = true;
            this.ucLabelEditQtyPrinted.Caption = "已打印个数";
            this.ucLabelEditQtyPrinted.Checked = false;
            this.ucLabelEditQtyPrinted.EditType = UserControl.EditTypes.String;
            this.ucLabelEditQtyPrinted.Location = new System.Drawing.Point(332, 290);
            this.ucLabelEditQtyPrinted.MaxLength = 40;
            this.ucLabelEditQtyPrinted.Multiline = false;
            this.ucLabelEditQtyPrinted.Name = "ucLabelEditQtyPrinted";
            this.ucLabelEditQtyPrinted.PasswordChar = '\0';
            this.ucLabelEditQtyPrinted.ReadOnly = true;
            this.ucLabelEditQtyPrinted.ShowCheckBox = false;
            this.ucLabelEditQtyPrinted.Size = new System.Drawing.Size(173, 22);
            this.ucLabelEditQtyPrinted.TabIndex = 13;
            this.ucLabelEditQtyPrinted.TabNext = true;
            this.ucLabelEditQtyPrinted.TabStop = false;
            this.ucLabelEditQtyPrinted.Value = "";
            this.ucLabelEditQtyPrinted.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditQtyPrinted.XAlign = 405;
            // 
            // ucLabelEditQtyToPrint
            // 
            this.ucLabelEditQtyToPrint.AllowEditOnlyChecked = true;
            this.ucLabelEditQtyToPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditQtyToPrint.AutoSelectAll = false;
            this.ucLabelEditQtyToPrint.AutoUpper = true;
            this.ucLabelEditQtyToPrint.Caption = "待打印个数";
            this.ucLabelEditQtyToPrint.Checked = false;
            this.ucLabelEditQtyToPrint.EditType = UserControl.EditTypes.String;
            this.ucLabelEditQtyToPrint.Location = new System.Drawing.Point(332, 267);
            this.ucLabelEditQtyToPrint.MaxLength = 40;
            this.ucLabelEditQtyToPrint.Multiline = false;
            this.ucLabelEditQtyToPrint.Name = "ucLabelEditQtyToPrint";
            this.ucLabelEditQtyToPrint.PasswordChar = '\0';
            this.ucLabelEditQtyToPrint.ReadOnly = true;
            this.ucLabelEditQtyToPrint.ShowCheckBox = false;
            this.ucLabelEditQtyToPrint.Size = new System.Drawing.Size(173, 22);
            this.ucLabelEditQtyToPrint.TabIndex = 12;
            this.ucLabelEditQtyToPrint.TabNext = true;
            this.ucLabelEditQtyToPrint.TabStop = false;
            this.ucLabelEditQtyToPrint.Value = "";
            this.ucLabelEditQtyToPrint.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditQtyToPrint.XAlign = 405;
            // 
            // ucLabelEditTotal
            // 
            this.ucLabelEditTotal.AllowEditOnlyChecked = true;
            this.ucLabelEditTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditTotal.AutoSelectAll = false;
            this.ucLabelEditTotal.AutoUpper = true;
            this.ucLabelEditTotal.Caption = "总个数";
            this.ucLabelEditTotal.Checked = false;
            this.ucLabelEditTotal.EditType = UserControl.EditTypes.String;
            this.ucLabelEditTotal.Location = new System.Drawing.Point(356, 244);
            this.ucLabelEditTotal.MaxLength = 40;
            this.ucLabelEditTotal.Multiline = false;
            this.ucLabelEditTotal.Name = "ucLabelEditTotal";
            this.ucLabelEditTotal.PasswordChar = '\0';
            this.ucLabelEditTotal.ReadOnly = true;
            this.ucLabelEditTotal.ShowCheckBox = false;
            this.ucLabelEditTotal.Size = new System.Drawing.Size(149, 22);
            this.ucLabelEditTotal.TabIndex = 11;
            this.ucLabelEditTotal.TabNext = true;
            this.ucLabelEditTotal.TabStop = false;
            this.ucLabelEditTotal.Value = "";
            this.ucLabelEditTotal.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditTotal.XAlign = 405;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.btnCancel.Caption = "取消";
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(392, 432);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 22);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Click += new System.EventHandler(this.ucButtonCancel_Click);
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(531, 433);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 10;
            // 
            // ucButtonPrint
            // 
            this.ucButtonPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButtonPrint.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonPrint.BackgroundImage")));
            this.ucButtonPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucButtonPrint.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonPrint.Caption = "打印";
            this.ucButtonPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonPrint.Location = new System.Drawing.Point(250, 433);
            this.ucButtonPrint.Name = "ucButtonPrint";
            this.ucButtonPrint.Size = new System.Drawing.Size(88, 22);
            this.ucButtonPrint.TabIndex = 8;
            this.ucButtonPrint.Click += new System.EventHandler(this.ucButtonPrint_Click);
            // 
            // ucLabelComboxPrinter
            // 
            this.ucLabelComboxPrinter.AllowEditOnlyChecked = true;
            this.ucLabelComboxPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelComboxPrinter.Caption = "打印机";
            this.ucLabelComboxPrinter.Checked = false;
            this.ucLabelComboxPrinter.Location = new System.Drawing.Point(355, 326);
            this.ucLabelComboxPrinter.Name = "ucLabelComboxPrinter";
            this.ucLabelComboxPrinter.SelectedIndex = -1;
            this.ucLabelComboxPrinter.ShowCheckBox = false;
            this.ucLabelComboxPrinter.Size = new System.Drawing.Size(449, 22);
            this.ucLabelComboxPrinter.TabIndex = 7;
            this.ucLabelComboxPrinter.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelComboxPrinter.XAlign = 404;
            // 
            // dataSetMKeypartDetial
            // 
            this.dataSetMKeypartDetial.DataSetName = "dataSetMKeypartDetial";
            this.dataSetMKeypartDetial.Locale = new System.Globalization.CultureInfo("zh-CN");
            this.dataSetMKeypartDetial.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTableMKeypartDetial});
            // 
            // dataSetPrintTemplate
            // 
            this.dataSetPrintTemplate.DataSetName = "dataSetPrintTemplate";
            this.dataSetPrintTemplate.Locale = new System.Globalization.CultureInfo("zh-CN");
            this.dataSetPrintTemplate.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTablePrintTemplate});
            // 
            // FPrintLabel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(824, 564);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FPrintLabel";
            this.Text = "产品序列号打印";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FPrintLabel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetScale)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetPrintBy)).EndInit();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridPrintTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTablePrintTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridRCardList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableMKeypartDetial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetMKeypartDetial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetPrintTemplate)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion


        #region User Events

        private void FPrintLabel_Load(object sender, EventArgs e)
        {
            this.ClearForm();
            this.ucLabelEditQtyPrinted.Value = "0";
            this.ucLabelEditItemCode.Enabled = false;

            this.LoadPrinter();
            this.LoadTemplateList();

            this.ucLabelEditRCard.TextFocus(false, true);

            //this.InitGridLanguage(ultraGridPrintTemplate);
            //this.InitGridLanguage(ultraGridRCardList);
            //this.InitPageLanguage();
        }

        private void ultraOptionSetPrintBy_ValueChanged(object sender, EventArgs e)
        {
            switch (ultraOptionSetPrintBy.CheckedIndex)
            {
                case 0:
                    this.ucLabelEditRCard.Value = string.Empty;
                    this.ucLabelEditRCard.Enabled = true;
                    this.ucLabelEditItemCode.Value = string.Empty;
                    this.ucLabelEditItemCode.Enabled = false;

                    this.ucLabelEditRCard.TextFocus(false, true);
                    break;

                case 1:
                    this.ucLabelEditRCard.Value = string.Empty;
                    this.ucLabelEditRCard.Enabled = false;
                    this.ucLabelEditItemCode.Value = string.Empty;
                    this.ucLabelEditItemCode.Enabled = true;

                    this.ucLabelEditItemCode.TextFocus(false, true);
                    break;

                default:
                    this.ucLabelEditRCard.Value = string.Empty;
                    this.ucLabelEditRCard.Enabled = true;
                    this.ucLabelEditItemCode.Value = string.Empty;
                    this.ucLabelEditItemCode.Enabled = false;

                    this.ucLabelEditRCard.TextFocus(false, true);
                    break;
            }
        }

        private void ucLabelEditRCard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                if (e.KeyChar == '\r')
                {
                    this.RequestDataByRCard(ucLabelEditRCard.Value.Trim().ToUpper());
                }
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
        }

        private void ucLabelEditItemCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                if (e.KeyChar == '\r')
                {
                    this.RequestDataByItemCode(ucLabelEditItemCode.Value.Trim().ToUpper());
                }
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
        }

        private void ucButtonQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                switch (ultraOptionSetPrintBy.CheckedIndex)
                {
                    case 0:
                        this.RequestDataByRCard(ucLabelEditRCard.Value.Trim().ToUpper());
                        break;

                    case 1:
                        this.RequestDataByItemCode(ucLabelEditItemCode.Value.Trim().ToUpper());
                        break;

                    default:
                        this.RequestDataByRCard(ucLabelEditRCard.Value.Trim().ToUpper());
                        break;
                }
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
        }

        private void listBoxRCardRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                MKeyPart mKeyPart = this._MKeyPartList[listBoxRCardRange.SelectedIndex];

                if (mKeyPart != null)
                {
                    string defaulRCard = string.Empty;

                    if (ultraOptionSetPrintBy.CheckedIndex == 0)
                    {
                        defaulRCard = ucLabelEditRCard.Value.Trim().ToUpper();
                    }

                    this.LoadMKeyPartDetailList(mKeyPart.MItemCode.Trim().ToUpper(), mKeyPart.Sequence, defaulRCard);
                }
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
        }

        private void ultraGridPrintTemplate_AfterCellActivate(object sender, EventArgs e)
        {
            if (this.ultraGridPrintTemplate.ActiveCell.Column.Index != 0)
            {
                this.ultraGridPrintTemplate.ActiveCell.Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            }
        }

        private void ultraGridRCardList_AfterCellActivate(object sender, EventArgs e)
        {
            if (this.ultraGridRCardList.ActiveCell.Column.Index != 0)
            {
                this.ultraGridRCardList.ActiveCell.Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            }
        }

        private void ultraGridPrintTemplate_CellChange(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Index == 0)
            {
                if (!bool.Parse(e.Cell.Value.ToString()))
                {
                    for (int i = 0; i < ultraGridPrintTemplate.Rows.Count; i++)
                    {
                        if (ultraGridPrintTemplate.Rows[i].Index != e.Cell.Row.Index && bool.Parse(ultraGridPrintTemplate.Rows[i].Cells[0].Value.ToString()))
                        {
                            ultraGridPrintTemplate.Rows[i].Cells[0].Value = false;
                        }
                    }
                }
                ultraGridPrintTemplate.UpdateData();
            }
        }

        private void ultraGridRCardList_CellChange(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Index == 0)
            {
                if (!bool.Parse(e.Cell.Value.ToString()))
                {
                    AddCount(ucLabelEditQtyToPrint, 1);
                }
                else
                {
                    AddCount(ucLabelEditQtyToPrint, -1);
                }
                ultraGridRCardList.UpdateData();
            }
        }

        private bool ValidateInput(string printer, PrintTemplate printTemplate, MKeyPart mKeyPart, List<MKeyPartDetail> mKeyPartDetailList)
        {
            //序列号
            if (mKeyPart == null || mKeyPartDetailList == null || mKeyPartDetailList.Count <= 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_RCard_Empty"));
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

        private void ucButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // Added By Hi1/Venus.Feng on 20081127 for Hisense : Check Printers
                if (this.ucLabelComboxPrinter.ComboBoxData.Items.Count == 0)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_PleaseInstallPrinter"));
                    return;
                }
                // ENd Added

                SetPrintButtonStatus(false);

                CodeSoftPrintFacade codeSoftPrintFacade = new CodeSoftPrintFacade(this.DataProvider);
                PrintTemplateFacade printTemplateFacade = new PrintTemplateFacade(this.DataProvider);
                MaterialFacade materialFacade = new MaterialFacade(this.DataProvider);

                string printer = this.ucLabelComboxPrinter.SelectedItemText;

                PrintTemplate printTemplate = null;
                for (int i = 0; i < ultraGridPrintTemplate.Rows.Count; i++)
                {
                    if (ultraGridPrintTemplate.Rows[i].Cells[0].Value.ToString().Trim().ToUpper() == "TRUE")
                    {
                        printTemplate = (PrintTemplate)this._PrintTemplateList[i];
                        printTemplate = (PrintTemplate)printTemplateFacade.GetPrintTemplate(printTemplate.TemplateName);
                        break;
                    }
                }

                MKeyPart mKeyPart = null;
                if (this.listBoxRCardRange.SelectedIndex >= 0)
                {
                    mKeyPart = (MKeyPart)this._MKeyPartList[this.listBoxRCardRange.SelectedIndex];
                    mKeyPart = (MKeyPart)materialFacade.GetMKeyPart(mKeyPart.Sequence, mKeyPart.MItemCode);
                }

                List<MKeyPartDetail> mKeyPartDetailList = new List<MKeyPartDetail>();
                for (int i = 0; i < ultraGridRCardList.Rows.Count; i++)
                {
                    if (ultraGridRCardList.Rows[i].Cells[0].Value.ToString().Trim().ToUpper() == "TRUE")
                    {
                        mKeyPartDetailList.Add((MKeyPartDetail)this._MKeyPartDetailList[i]);
                    }
                }

                if (!ValidateInput(printer, printTemplate, mKeyPart, mKeyPartDetailList))
                    return;

                //打印操作
                List<string> reserveInfo = new List<string>();
                reserveInfo.Add(this.ucLabelEditReserve1.Value.Trim());
                reserveInfo.Add(this.ucLabelEditReserve2.Value.Trim());
                reserveInfo.Add(this.ucLabelEditReserve3.Value.Trim());
                reserveInfo.Add(this.ucLabelEditReserve4.Value.Trim());
                reserveInfo.Add(this.ucLabelEditReserve5.Value.Trim());
                reserveInfo.Add(this.ucLabelEditReserve6.Value.Trim());
                reserveInfo.Add(this.ucLabelEditReserve7.Value.Trim());
                reserveInfo.Add(this.ucLabelEditReserve8.Value.Trim());

                Messages msg = codeSoftPrintFacade.Print(printer, printTemplate.TemplatePath, mKeyPart, mKeyPartDetailList, reserveInfo);

                if (msg.IsSuccess())
                {
                    //打印后的数据处理
                    try
                    {
                        string userCode = ApplicationService.Current().UserCode;
                        DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                        this.DataProvider.BeginTransaction();

                        mKeyPart.TemplateName = printTemplate.TemplateName;
                        mKeyPart.MaintainUser = userCode;
                        mKeyPart.MaintainDate = dbDateTime.DBDate;
                        mKeyPart.MaintainTime = dbDateTime.DBTime;

                        materialFacade.UpdateMKeyPart(mKeyPart);

                        foreach (MKeyPartDetail detial in mKeyPartDetailList)
                        {
                            detial.PrintTimes++;
                            detial.MaintainUser = userCode;
                            detial.MaintainDate = dbDateTime.DBDate;
                            detial.MaintainTime = dbDateTime.DBTime;

                            materialFacade.UpdateMKeyPartDetail(detial);
                        }

                        this.DataProvider.CommitTransaction();
                    }
                    catch (System.Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                        return;
                    }

                    AddCount(ucLabelEditQtyPrinted, mKeyPartDetailList.Count);

                    listBoxRCardRange_SelectedIndexChanged(null, null);
                }

                this.ShowMessage(msg);
            }
            finally
            {
                SetPrintButtonStatus(true);
            }
        }

        private void AddCount(UCLabelEdit ucLabelEdit, int add)
        {
            int count = int.Parse(ucLabelEdit.Value);
            count += add;
            ucLabelEdit.Value = count.ToString();
        }

        private void ucButtonCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            this.ucLabelEditRCard.Value = string.Empty;
            this.ucLabelEditItemCode.Value = string.Empty;

            this.ucLabelEditTotal.Value = "0";
            this.ucLabelEditQtyToPrint.Value = "0";
            this.labelItemDesc.Text = string.Empty;

            this.listBoxRCardRange.Items.Clear();
            this.dataTableMKeypartDetial.Clear();

            this.ucLabelEditReserve1.Value = string.Empty;
            this.ucLabelEditReserve2.Value = string.Empty;
            this.ucLabelEditReserve3.Value = string.Empty;
            this.ucLabelEditReserve4.Value = string.Empty;
            this.ucLabelEditReserve5.Value = string.Empty;
            this.ucLabelEditReserve6.Value = string.Empty;
            this.ucLabelEditReserve7.Value = string.Empty;
            this.ucLabelEditReserve8.Value = string.Empty;
        }

        private void checkBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                for (int i = 0; i < ultraGridRCardList.Rows.Count; i++)
                {
                    ultraGridRCardList.Rows[i].Cells[0].Value = checkBoxSelectAll.Checked;
                }

                ultraGridRCardList.UpdateData();

                if (checkBoxSelectAll.Checked)
                {
                    this.ucLabelEditQtyToPrint.Value = ultraGridRCardList.Rows.Count.ToString();
                }
                else
                {
                    this.ucLabelEditQtyToPrint.Value = "0";
                }
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
        }

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

        private void ucLabelEditReserve1_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.ucLabelEditReserve2.TextFocus(false, true);
            }
        }

        private void ucLabelEditReserve2_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.ucLabelEditReserve3.TextFocus(false, true);
            }
        }

        private void ucLabelEditReserve3_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.ucLabelEditReserve4.TextFocus(false, true);
            }
        }

        private void ucLabelEditReserve4_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.ucLabelEditReserve5.TextFocus(false, true);
            }
        }

        private void ucLabelEditReserve5_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.ucLabelEditReserve6.TextFocus(false, true);
            }
        }

        private void ucLabelEditReserve6_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.ucLabelEditReserve7.TextFocus(false, true);
            }
        }

        private void ucLabelEditReserve7_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.ucLabelEditReserve8.TextFocus(false, true);
            }
        }

        #endregion

        #region RequestData

        private void RequestDataByRCard(string rCard)
        {
            if (rCard.Trim().Length <= 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_RCARD"+"$Error_Input_Empty"));
                ucLabelEditRCard.TextFocus(false, true);
                return;
            }

            this.labelItemDesc.Text = string.Empty;
            _MKeyPartList = null;
            this.listBoxRCardRange.Items.Clear();

            MaterialFacade materialFacade = new MaterialFacade(this.DataProvider);
            ItemFacade itemFacade = new ItemFacade(this.DataProvider);

            //根据当前序列号获取产品的原始序列号
            string sourceRCard = (new DataCollectFacade(DataProvider)).GetSourceCard(rCard, string.Empty);

            object[] mKeyPartList = materialFacade.QueryMKeyPart(string.Empty, sourceRCard, int.MinValue, int.MaxValue);

            if (mKeyPartList == null || mKeyPartList.Length <= 0)
            {
                LoadMKeyPartDetailList(Guid.NewGuid().ToString(), -1, string.Empty);
                this.ucLabelEditRCard.TextFocus(false, true);
                return;
            }

            if (mKeyPartList != null)
            {
                _MKeyPartList = new MKeyPart[mKeyPartList.Length];

                for (int i = 0; i < mKeyPartList.Length; i++)
                {
                    _MKeyPartList[i] = (MKeyPart)mKeyPartList[i];

                    string show = _MKeyPartList[i].RCardPrefix +
                        _MKeyPartList[i].RunningCardStart +
                        " ~ " +
                        _MKeyPartList[i].RCardPrefix +
                        _MKeyPartList[i].RunningCardEnd;

                    this.listBoxRCardRange.Items.Add(show);
                }

                this.listBoxRCardRange.SelectedIndex = 0;

                Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterial(this._MKeyPartList[listBoxRCardRange.SelectedIndex].MItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);

                if (material != null)
                {
                    this.labelItemDesc.Text = material.MaterialDescription;
                }
            }
        }

        private void RequestDataByItemCode(string itemCode)
        {
            if (itemCode.Trim().Length <= 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_ItemCode" + "$Error_Input_Empty"));
                ucLabelEditItemCode.TextFocus(false, true);
                return;
            }

            this.labelItemDesc.Text = string.Empty;
            _MKeyPartList = null;
            this.listBoxRCardRange.Items.Clear();

            MaterialFacade materialFacade = new MaterialFacade(this.DataProvider);
            ItemFacade itemFacade = new ItemFacade(this.DataProvider);

            object[] mKeyPartList = materialFacade.QueryMKeyPart(itemCode, string.Empty, int.MinValue, int.MaxValue);

            if (mKeyPartList == null || mKeyPartList.Length <= 0)
            {
                LoadMKeyPartDetailList(Guid.NewGuid().ToString(), -1, string.Empty);
                this.ucLabelEditItemCode.TextFocus(false, true);
                return;
            }

            if (mKeyPartList != null)
            {
                _MKeyPartList = new MKeyPart[mKeyPartList.Length];

                for (int i = 0; i < mKeyPartList.Length; i++)
                {
                    _MKeyPartList[i] = (MKeyPart)mKeyPartList[i];

                    string show = _MKeyPartList[i].RCardPrefix +
                        _MKeyPartList[i].RunningCardStart +
                        " ~ " +
                        _MKeyPartList[i].RCardPrefix +
                        _MKeyPartList[i].RunningCardEnd;

                    this.listBoxRCardRange.Items.Add(show);
                }

                this.listBoxRCardRange.SelectedIndex = 0;

                Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterial(itemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);

                if (material != null)
                {
                    this.labelItemDesc.Text = material.MaterialDescription;
                }
            }
        }

        private void LoadMKeyPartDetailList(string itemCode, int seq, string defaulRCard)
        {
            _MKeyPartDetailList = null;
            dataTableMKeypartDetial.Rows.Clear();
            this.checkBoxSelectAll.Checked = false;
            this.ucLabelEditQtyToPrint.Value = "0";
            this.ucLabelEditTotal.Value = "0";

            object[] objs = this.LoadMKeyPartDetailListDataSource(itemCode, seq);
            if (objs == null || objs.Length <= 0)
            {
                this.ShowMessage("$CS_No_Data_To_Display");
                return;
            }

            _MKeyPartDetailList = new MKeyPartDetail[objs.Length];

            for (int i = 0; i < objs.Length; i++)
            {
                _MKeyPartDetailList[i] = (MKeyPartDetail)objs[i];

                bool checkedRow = false;
                if (defaulRCard.Trim().Length > 0)
                {
                    if (defaulRCard.Trim().ToUpper() == _MKeyPartDetailList[i].SerialNo.Trim().ToUpper())
                    {
                        checkedRow = true;
                        AddCount(ucLabelEditQtyToPrint, 1);
                    }
                }

                dataTableMKeypartDetial.Rows.Add(new object[] { checkedRow, _MKeyPartDetailList[i].SerialNo, _MKeyPartDetailList[i].PrintTimes });

            }

            this.ucLabelEditTotal.Value = ultraGridRCardList.Rows.Count.ToString();

            if (defaulRCard.Trim().Length <= 0)
            {
                this.checkBoxSelectAll.Checked = true;
            }

            dataTableMKeypartDetial.DefaultView.Sort = "SerialNo";

            this.SetDefaultTemplate(itemCode);
        }

        private void LoadTemplateList()
        {
            _PrintTemplateList = null;
            dataTablePrintTemplate.Rows.Clear();

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

                bool rowChecked = false;

                dataTablePrintTemplate.Rows.Add(new object[] { rowChecked, _PrintTemplateList[i].TemplateName, _PrintTemplateList[i].TemplateDesc });
            }

            dataTablePrintTemplate.DefaultView.Sort = "TemplateName";
        }

        private void SetDefaultTemplate(string itemCodeForDefaultTemplate)
        {
            MaterialFacade materialFacade = new MaterialFacade(this.DataProvider);
            MKeyPart mKeyPart = (MKeyPart)materialFacade.GetLatestMKeyPart(itemCodeForDefaultTemplate);

            if (mKeyPart != null && mKeyPart.TemplateName != null && mKeyPart.TemplateName.Trim().Length > 0)
            {
                for (int i = 0; i < ultraGridPrintTemplate.Rows.Count; i++)
                {
                    if (ultraGridPrintTemplate.Rows[i].Cells[1].Value.ToString().Trim().ToUpper() == mKeyPart.TemplateName.Trim().ToUpper())
                    {
                        ultraGridPrintTemplate.Rows[i].Cells[0].Value = true;
                    }
                    else
                    {
                        ultraGridPrintTemplate.Rows[i].Cells[0].Value = false;
                    }
                }
                ultraGridPrintTemplate.UpdateData();
            }
        }

        private object[] LoadMKeyPartDetailListDataSource(string itemCode, int seq)
        {
            try
            {
                MaterialFacade materialFacade = new MaterialFacade(this.DataProvider);
                return materialFacade.QueryMKeyPartDetail(itemCode, seq, string.Empty, string.Empty);
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }

            return null;
        }

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

        private void LoadPrinter()
        {
            this.ucLabelComboxPrinter.Clear();

            // Added By hi1/Venus.Feng on 20081127 for Hisense Version : Check Printers
            if (System.Drawing.Printing.PrinterSettings.InstalledPrinters == null ||
                System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count == 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_PleaseInstallPrinter"));
                return;
            }
            // End Added

            int defaultprinter = 0;
            for (int i = 0; i < System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count; i++)
            {
                this.ucLabelComboxPrinter.AddItem(System.Drawing.Printing.PrinterSettings.InstalledPrinters[i], System.Drawing.Printing.PrinterSettings.InstalledPrinters[i]);
                System.Drawing.Printing.PrinterSettings pts = new System.Drawing.Printing.PrinterSettings();
                pts.PrinterName = System.Drawing.Printing.PrinterSettings.InstalledPrinters[i];
                if (pts.IsDefaultPrinter)
                {
                    defaultprinter = i;
                }
            }
            this.ucLabelComboxPrinter.SelectedIndex = defaultprinter;
        }

        #endregion

        #region Base Functyion

        protected void ShowMessage(string message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        protected void ShowMessage(Exception e)
        {
            ApplicationRun.GetInfoForm().Add(new UserControl.Message(e));
        }

        protected void ShowMessage(UserControl.Message message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        protected void ShowMessage(Messages messages)
        {
            ApplicationRun.GetInfoForm().Add(messages);
        }

        #endregion

        private MKeyPart[] _MKeyPartList = null;
        private MKeyPartDetail[] _MKeyPartDetailList = null;
        private PrintTemplate[] _PrintTemplateList = null;
    }
}
