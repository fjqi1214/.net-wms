using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.DataCollect;

using UserControl;
using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.Client
{
    public class FMetrialLotParts : BaseForm
    {

        #region 设计器生成的代码

        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox grpQuery;
        private System.Windows.Forms.GroupBox groupBox1;

        private UserControl.UCLabelEdit ucLabelEditMOCode;
        private UserControl.UCLabelEdit ucLabelEditSSCode;
        private UserControl.UCLabelEdit ucLabelEditOPCode;

        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridLotParts;

        private UserControl.UCLabelEdit ucLabelEditItemSeq;
        private UserControl.UCLabelEdit ucLabelEditBarcode;

        private UserControl.UCButton ucBtnExit;
        private UserControl.UCButton ucButtonSave;

        private System.Data.DataTable dataTableLotMaterials;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataColumn dataColumn8;
        private System.Data.DataColumn dataColumn9;
        private System.Data.DataColumn dataColumn10;//add by Jarvis 2012-03-09
        private System.Data.DataColumn dataColumn11;//add by Jarvis 2012-03-09
        private System.Data.DataColumn dataColumn12;//add by Jarvis 2012-03-09
        private UserControl.UCLabelCombox ucLabComboxRoute;
        private UserControl.UCButton bntLock;
        private CheckBox chkShowZero;
        private UCButton ucButtonRefresh;
        private System.Data.DataSet dataSet1;

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FMetrialLotParts()
        {
            // 该调用是 Windows 窗体设计器所必需的。
            InitializeComponent();

            // TODO: 在 InitializeComponent 调用后添加任何初始化			
            UserControl.UIStyleBuilder.GridUI(ultraGridLotParts);
            UserControl.UIStyleBuilder.FormUI(this);

            this._MaterialFacade = new MaterialFacade(this.DataProvider);
            this._MoFacade = new MOFacade(this.DataProvider);
            this._OPBOMFacade = new OPBOMFacade(this.DataProvider);
            this._DataCollectFacade = new DataCollectFacade(this.DataProvider);
            this._ModelFacade = new ModelFacade(this.DataProvider);
            this._ShiftModelFacade = new ShiftModelFacade(this.DataProvider);
            this._InventoryFacade = new InventoryFacade(this.DataProvider);//add by Jarvis 20120314
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

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMetrialLotParts));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Table1", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StepSeq");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SBSITEMCODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SBITEMCODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MLotNo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MDESC");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ParseType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CheckStatus");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CheckType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SNLength");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PrimaryQty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StorageQty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Seq");
            this.dataTableLotMaterials = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn10 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataColumn12 = new System.Data.DataColumn();
            this.dataSet1 = new System.Data.DataSet();
            this.grpQuery = new System.Windows.Forms.GroupBox();
            this.ucButtonRefresh = new UserControl.UCButton();
            this.chkShowZero = new System.Windows.Forms.CheckBox();
            this.bntLock = new UserControl.UCButton();
            this.ucLabComboxRoute = new UserControl.UCLabelCombox();
            this.ucLabelEditSSCode = new UserControl.UCLabelEdit();
            this.ucLabelEditOPCode = new UserControl.UCLabelEdit();
            this.ucLabelEditMOCode = new UserControl.UCLabelEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucBtnExit = new UserControl.UCButton();
            this.ucButtonSave = new UserControl.UCButton();
            this.ucLabelEditBarcode = new UserControl.UCLabelEdit();
            this.ucLabelEditItemSeq = new UserControl.UCLabelEdit();
            this.ultraGridLotParts = new Infragistics.Win.UltraWinGrid.UltraGrid();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableLotMaterials)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.grpQuery.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridLotParts)).BeginInit();
            this.SuspendLayout();
            // 
            // dataTableLotMaterials
            // 
            this.dataTableLotMaterials.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn11,
            this.dataColumn12});
            this.dataTableLotMaterials.TableName = "Table1";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "StepSeq";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "SBSITEMCODE";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "SBITEMCODE";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "MLotNo";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "MDESC";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "ParseType";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "CheckStatus";
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "CheckType";
            // 
            // dataColumn9
            // 
            this.dataColumn9.ColumnName = "SNLength";
            // 
            // dataColumn10
            // 
            this.dataColumn10.ColumnName = "PrimaryQty";
            // 
            // dataColumn11
            // 
            this.dataColumn11.ColumnName = "StorageQty";
            // 
            // dataColumn12
            // 
            this.dataColumn12.ColumnName = "Seq";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Locale = new System.Globalization.CultureInfo("zh-CN");
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTableLotMaterials});
            // 
            // grpQuery
            // 
            this.grpQuery.BackColor = System.Drawing.Color.Gainsboro;
            this.grpQuery.Controls.Add(this.ucButtonRefresh);
            this.grpQuery.Controls.Add(this.chkShowZero);
            this.grpQuery.Controls.Add(this.bntLock);
            this.grpQuery.Controls.Add(this.ucLabComboxRoute);
            this.grpQuery.Controls.Add(this.ucLabelEditSSCode);
            this.grpQuery.Controls.Add(this.ucLabelEditOPCode);
            this.grpQuery.Controls.Add(this.ucLabelEditMOCode);
            this.grpQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpQuery.Location = new System.Drawing.Point(0, 0);
            this.grpQuery.Name = "grpQuery";
            this.grpQuery.Size = new System.Drawing.Size(760, 74);
            this.grpQuery.TabIndex = 0;
            this.grpQuery.TabStop = false;
            // 
            // ucButtonRefresh
            // 
            this.ucButtonRefresh.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonRefresh.BackgroundImage")));
            this.ucButtonRefresh.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonRefresh.Caption = "刷新";
            this.ucButtonRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonRefresh.Location = new System.Drawing.Point(604, 44);
            this.ucButtonRefresh.Name = "ucButtonRefresh";
            this.ucButtonRefresh.Size = new System.Drawing.Size(88, 22);
            this.ucButtonRefresh.TabIndex = 40;
            this.ucButtonRefresh.TabStop = false;
            this.ucButtonRefresh.Click += new System.EventHandler(this.ucButtonRefresh_Click);
            // 
            // chkShowZero
            // 
            this.chkShowZero.AutoSize = true;
            this.chkShowZero.Location = new System.Drawing.Point(604, 19);
            this.chkShowZero.Name = "chkShowZero";
            this.chkShowZero.Size = new System.Drawing.Size(90, 16);
            this.chkShowZero.TabIndex = 39;
            this.chkShowZero.Text = "显示数量为0";
            this.chkShowZero.UseVisualStyleBackColor = true;
            // 
            // bntLock
            // 
            this.bntLock.BackColor = System.Drawing.SystemColors.Control;
            this.bntLock.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bntLock.BackgroundImage")));
            this.bntLock.ButtonType = UserControl.ButtonTypes.None;
            this.bntLock.Caption = "锁定";
            this.bntLock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntLock.Location = new System.Drawing.Point(194, 16);
            this.bntLock.Name = "bntLock";
            this.bntLock.Size = new System.Drawing.Size(88, 22);
            this.bntLock.TabIndex = 13;
            this.bntLock.TabStop = false;
            this.bntLock.Click += new System.EventHandler(this.bntLock_Click);
            // 
            // ucLabComboxRoute
            // 
            this.ucLabComboxRoute.AllowEditOnlyChecked = true;
            this.ucLabComboxRoute.Caption = "途程";
            this.ucLabComboxRoute.Checked = false;
            this.ucLabComboxRoute.Location = new System.Drawing.Point(324, 16);
            this.ucLabComboxRoute.Name = "ucLabComboxRoute";
            this.ucLabComboxRoute.SelectedIndex = -1;
            this.ucLabComboxRoute.ShowCheckBox = false;
            this.ucLabComboxRoute.Size = new System.Drawing.Size(170, 24);
            this.ucLabComboxRoute.TabIndex = 1;
            this.ucLabComboxRoute.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabComboxRoute.XAlign = 361;
            this.ucLabComboxRoute.ComboBoxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabComboxRoute_ComboBoxKeyPress);
            this.ucLabComboxRoute.SelectedIndexChanged += new System.EventHandler(this.ucLabComboxRoute_SelectedIndexChanged);
            // 
            // ucLabelEditSSCode
            // 
            this.ucLabelEditSSCode.AllowEditOnlyChecked = true;
            this.ucLabelEditSSCode.AutoUpper = true;
            this.ucLabelEditSSCode.Caption = "线别";
            this.ucLabelEditSSCode.Checked = false;
            this.ucLabelEditSSCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditSSCode.Enabled = false;
            this.ucLabelEditSSCode.Location = new System.Drawing.Point(324, 46);
            this.ucLabelEditSSCode.MaxLength = 40;
            this.ucLabelEditSSCode.Multiline = false;
            this.ucLabelEditSSCode.Name = "ucLabelEditSSCode";
            this.ucLabelEditSSCode.PasswordChar = '\0';
            this.ucLabelEditSSCode.ReadOnly = false;
            this.ucLabelEditSSCode.ShowCheckBox = false;
            this.ucLabelEditSSCode.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditSSCode.TabIndex = 3;
            this.ucLabelEditSSCode.TabNext = true;
            this.ucLabelEditSSCode.Value = "";
            this.ucLabelEditSSCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditSSCode.XAlign = 361;
            // 
            // ucLabelEditOPCode
            // 
            this.ucLabelEditOPCode.AllowEditOnlyChecked = true;
            this.ucLabelEditOPCode.AutoUpper = true;
            this.ucLabelEditOPCode.Caption = "工序";
            this.ucLabelEditOPCode.Checked = false;
            this.ucLabelEditOPCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditOPCode.Enabled = false;
            this.ucLabelEditOPCode.Location = new System.Drawing.Point(18, 46);
            this.ucLabelEditOPCode.MaxLength = 40;
            this.ucLabelEditOPCode.Multiline = false;
            this.ucLabelEditOPCode.Name = "ucLabelEditOPCode";
            this.ucLabelEditOPCode.PasswordChar = '\0';
            this.ucLabelEditOPCode.ReadOnly = false;
            this.ucLabelEditOPCode.ShowCheckBox = false;
            this.ucLabelEditOPCode.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditOPCode.TabIndex = 2;
            this.ucLabelEditOPCode.TabNext = true;
            this.ucLabelEditOPCode.Value = "";
            this.ucLabelEditOPCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditOPCode.XAlign = 55;
            // 
            // ucLabelEditMOCode
            // 
            this.ucLabelEditMOCode.AllowEditOnlyChecked = true;
            this.ucLabelEditMOCode.AutoUpper = true;
            this.ucLabelEditMOCode.Caption = "工单";
            this.ucLabelEditMOCode.Checked = false;
            this.ucLabelEditMOCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditMOCode.Location = new System.Drawing.Point(18, 16);
            this.ucLabelEditMOCode.MaxLength = 40;
            this.ucLabelEditMOCode.Multiline = false;
            this.ucLabelEditMOCode.Name = "ucLabelEditMOCode";
            this.ucLabelEditMOCode.PasswordChar = '\0';
            this.ucLabelEditMOCode.ReadOnly = false;
            this.ucLabelEditMOCode.ShowCheckBox = false;
            this.ucLabelEditMOCode.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditMOCode.TabIndex = 0;
            this.ucLabelEditMOCode.TabNext = true;
            this.ucLabelEditMOCode.Value = "";
            this.ucLabelEditMOCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditMOCode.XAlign = 55;
            this.ucLabelEditMOCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditMOCode_TxtboxKeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucBtnExit);
            this.groupBox1.Controls.Add(this.ucButtonSave);
            this.groupBox1.Controls.Add(this.ucLabelEditBarcode);
            this.groupBox1.Controls.Add(this.ucLabelEditItemSeq);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 421);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 80);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(406, 50);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 7;
            // 
            // ucButtonSave
            // 
            this.ucButtonSave.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonSave.BackgroundImage")));
            this.ucButtonSave.ButtonType = UserControl.ButtonTypes.Save;
            this.ucButtonSave.Caption = "保存";
            this.ucButtonSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonSave.Location = new System.Drawing.Point(281, 50);
            this.ucButtonSave.Name = "ucButtonSave";
            this.ucButtonSave.Size = new System.Drawing.Size(88, 22);
            this.ucButtonSave.TabIndex = 6;
            this.ucButtonSave.TabStop = false;
            this.ucButtonSave.Visible = false;
            this.ucButtonSave.Click += new System.EventHandler(this.ucButtonSave_Click);
            // 
            // ucLabelEditBarcode
            // 
            this.ucLabelEditBarcode.AllowEditOnlyChecked = true;
            this.ucLabelEditBarcode.AutoUpper = true;
            this.ucLabelEditBarcode.Caption = "物料批号";
            this.ucLabelEditBarcode.Checked = false;
            this.ucLabelEditBarcode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditBarcode.Location = new System.Drawing.Point(42, 16);
            this.ucLabelEditBarcode.MaxLength = 40;
            this.ucLabelEditBarcode.Multiline = false;
            this.ucLabelEditBarcode.Name = "ucLabelEditBarcode";
            this.ucLabelEditBarcode.PasswordChar = '\0';
            this.ucLabelEditBarcode.ReadOnly = false;
            this.ucLabelEditBarcode.ShowCheckBox = false;
            this.ucLabelEditBarcode.Size = new System.Drawing.Size(461, 24);
            this.ucLabelEditBarcode.TabIndex = 5;
            this.ucLabelEditBarcode.TabNext = true;
            this.ucLabelEditBarcode.Value = "";
            this.ucLabelEditBarcode.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditBarcode.XAlign = 103;
            this.ucLabelEditBarcode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditBarcode_TxtboxKeyPress);
            // 
            // ucLabelEditItemSeq
            // 
            this.ucLabelEditItemSeq.AllowEditOnlyChecked = true;
            this.ucLabelEditItemSeq.AutoUpper = true;
            this.ucLabelEditItemSeq.Caption = "顺序号";
            this.ucLabelEditItemSeq.Checked = false;
            this.ucLabelEditItemSeq.EditType = UserControl.EditTypes.String;
            this.ucLabelEditItemSeq.Location = new System.Drawing.Point(52, 47);
            this.ucLabelEditItemSeq.MaxLength = 40;
            this.ucLabelEditItemSeq.Multiline = false;
            this.ucLabelEditItemSeq.Name = "ucLabelEditItemSeq";
            this.ucLabelEditItemSeq.PasswordChar = '\0';
            this.ucLabelEditItemSeq.ReadOnly = false;
            this.ucLabelEditItemSeq.ShowCheckBox = false;
            this.ucLabelEditItemSeq.Size = new System.Drawing.Size(182, 24);
            this.ucLabelEditItemSeq.TabIndex = 4;
            this.ucLabelEditItemSeq.TabNext = true;
            this.ucLabelEditItemSeq.Value = "";
            this.ucLabelEditItemSeq.Visible = false;
            this.ucLabelEditItemSeq.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditItemSeq.XAlign = 101;
            this.ucLabelEditItemSeq.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditItemSeq_TxtboxKeyPress);
            // 
            // ultraGridLotParts
            // 
            this.ultraGridLotParts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ultraGridLotParts.DataSource = this.dataTableLotMaterials;
            ultraGridColumn1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn2.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn3.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn4.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn5.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn6.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn7.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn8.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn9.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn10.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn11.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn11.Header.VisiblePosition = 10;
            ultraGridColumn12.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn12.Header.VisiblePosition = 11;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12});
            this.ultraGridLotParts.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGridLotParts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridLotParts.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridLotParts.Location = new System.Drawing.Point(0, 74);
            this.ultraGridLotParts.Name = "ultraGridLotParts";
            this.ultraGridLotParts.Size = new System.Drawing.Size(760, 347);
            this.ultraGridLotParts.TabIndex = 8;
            this.ultraGridLotParts.Text = "批管控料";
            // 
            // FMetrialLotParts
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(760, 501);
            this.Controls.Add(this.ultraGridLotParts);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpQuery);
            this.Name = "FMetrialLotParts";
            this.Text = "岗位用料列表维护";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FMetrialLotParts_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FMetrialLotParts_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataTableLotMaterials)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.grpQuery.ResumeLayout(false);
            this.grpQuery.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridLotParts)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region Form Basis

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private MaterialFacade _MaterialFacade = null;
        private MOFacade _MoFacade = null;
        private OPBOMFacade _OPBOMFacade = null;
        private DataCollectFacade _DataCollectFacade = null;
        private ModelFacade _ModelFacade = null;
        private ShiftModelFacade _ShiftModelFacade = null;
        private InventoryFacade _InventoryFacade = null;//add by Jarvis 20120314

        private string _currResCode = string.Empty;
        private string _currOPCode = string.Empty;
        private string _currSSCode = string.Empty;
        private string _currSegCode = string.Empty;
        private string _currShiftTypeCode = string.Empty;
        private string _moCode = string.Empty;
        private decimal _moSeq = 0;
        private string _moBOMCode = string.Empty;
        private string _moBOMVersion = string.Empty;
        private string _moItemCode = string.Empty;
        private string _moModelCode = string.Empty;
        private string _selectedRouteCode = string.Empty;

        private ArrayList _minnoToSave = new ArrayList();
        private ArrayList _onwipItemToSave = new ArrayList();
        private Hashtable _opBOMDetailList = new Hashtable();

        private bool _notSaved = false;

        protected void ShowMessage(string message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        protected void ShowMessage(UserControl.Messages messages)
        {
            ApplicationRun.GetInfoForm().Add(messages);
        }

        protected void ShowMessage(UserControl.Message message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        private bool confirmChangeQuery()
        {
            if (!this._notSaved)
            {
                return true;
            }

            if (MessageBox.Show(this, "改变查询条件将丢失所有未保存的操作,是否确定?", this.Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this._notSaved = false;
                return true;
            }

            return false;
        }

        #endregion

        #region Form Events

        private void FMetrialLotParts_Load(object sender, System.EventArgs e)
        {
            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
            _currResCode = ApplicationService.Current().ResourceCode;

            //Load OPCode
            Operation2Resource op2Res = (Operation2Resource)baseModelFacade.GetOperationByResource(_currResCode);
            if (op2Res != null)
            {
                _currOPCode = op2Res.OPCode;

                Operation op = (Operation)baseModelFacade.GetOperation(op2Res.OPCode);
                if (op != null)
                    ucLabelEditOPCode.Value = op.OPCode;
            }

            //Load SSCode
            Resource res = (Resource)baseModelFacade.GetResource(_currResCode);
            if (res != null)
            {
                _currSSCode = res.StepSequenceCode;
                _currSegCode = res.SegmentCode;
                _currShiftTypeCode = res.ShiftTypeCode;

                StepSequence ss = (StepSequence)baseModelFacade.GetStepSequence(res.StepSequenceCode);
                if (ss != null)
                    ucLabelEditSSCode.Value = ss.StepSequenceCode + "(" + ss.StepSequenceDescription + ")";
            }

            //Set ultraGridLotParts's column
            ultraGridLotParts.DisplayLayout.Bands[0].Columns[0].Hidden = true;//隐藏顺序号
            ultraGridLotParts.DisplayLayout.Bands[0].Columns[5].Hidden = true;
            ultraGridLotParts.DisplayLayout.Bands[0].Columns[6].Hidden = true;
            ultraGridLotParts.DisplayLayout.Bands[0].Columns[7].Hidden = true;
            ultraGridLotParts.DisplayLayout.Bands[0].Columns[8].Hidden = true;
            ultraGridLotParts.DisplayLayout.Bands[0].Columns[11].Hidden = true;//add by Jarvis 隐藏序号

            //this.InitGridLanguage(ultraGridLotParts);
            //this.InitPageLanguage();
        }

        private void FMetrialLotParts_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DataProvider != null)
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
        }

        #endregion

        #region User Events

        private void ucLabelEditMOCode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
                return;

            LockMOInput();
            //Application.DoEvents();

            if (!this.confirmChangeQuery())
            {
                UnLockMOInput();
                this.ucLabelEditMOCode.Value = this._moCode;
                this.ucLabelEditMOCode.TextFocus(false, true);
                return;
            }

            if (this.ucLabelEditMOCode.Value.Trim() == string.Empty)
            {
                UnLockMOInput();
                this.ucLabelEditMOCode.TextFocus(false, true);
                return;
            }

            //Get MO			
            MO mo = (MO)_MoFacade.GetMO(this.ucLabelEditMOCode.Value.Trim().ToUpper());
            if (mo == null)
            {
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_CS_MO_Not_Exist"));
                UnLockMOInput();
                this.ucLabelEditMOCode.TextFocus(false, true);
                return;
            }
            else
            {
                this._moCode = mo.MOCode;
                this._moSeq = mo.MOSeq;
                this._moItemCode = mo.ItemCode;
                this._moBOMVersion = mo.BOMVersion;

                object[] model = _ModelFacade.GetModel2ItemByItemCode(_moItemCode);
                if (model == null)
                {
                    this._moModelCode = "";
                }
                else
                {
                    this._moModelCode = ((Model2Item)model[0]).ModelCode;
                }

                this._minnoToSave.Clear();
                this._onwipItemToSave.Clear();
            }

            //Check MO status
            if (mo.MOStatus != BenQGuru.eMES.DataCollect.MOManufactureStatus.MOSTATUS_RELEASE && mo.MOStatus != BenQGuru.eMES.DataCollect.MOManufactureStatus.MOSTATUS_OPEN)
            {
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_CS_MO_Should_be_Release_or_Open"));
                UnLockMOInput();
                this.ucLabelEditMOCode.TextFocus(false, true);
                return;
            }

            //Get opbom's routes and MO's main route
            object[] opBOMs = _OPBOMFacade.QueryDistinctOPBOMRoute(_moItemCode, _moBOMVersion, GlobalVariables.CurrentOrganizations.First().OrganizationID);
            this.ucLabComboxRoute.Clear();
            if (opBOMs != null && opBOMs.Length > 0)
            {
                foreach (OPBOM opBOM in opBOMs)
                {
                    ucLabComboxRoute.AddItem(opBOM.OPBOMRoute, opBOM.OPBOMRoute);
                }
            }
            else
            {
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_OPBOM_NotFound"));
                UnLockMOInput();
                this.ucLabelEditMOCode.TextFocus(false, true);
                return;
            }

            MO2Route mo2Route = (MO2Route)_MoFacade.GetMONormalRouteByMOCode(_moCode);
            if (mo2Route == null)
            {
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_CS_MO_Not_Exist"));
                UnLockMOInput();
                this.ucLabelEditMOCode.TextFocus(false, true);
                return;
            }
            else
            {
                try
                {
                    ucLabComboxRoute.SetSelectItemText(mo2Route.RouteCode);
                }
                catch
                {
                    ucLabComboxRoute.SelectedIndex = 0;
                }
            }
        }

        private void ucLabComboxRoute_ComboBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
                return;

            //Load Grid
            this.RequestData();
            //this.ucLabelEditItemSeq.TextFocus(false, true);

            //Get first empty row
            //FindFirsEmptyRow();

        }

        private void ucLabComboxRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedRouteCode = ucLabComboxRoute.SelectedItemText;

            //Load Grid
            this.RequestData();
            //this.ucLabelEditItemSeq.TextFocus(false, true);
            _minnoToSave.Clear();
        }

        private void ucLabelEditItemSeq_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
                return;

            if (this.ucLabelEditItemSeq.Value.Trim() == string.Empty)
            {
                this.ucLabelEditItemSeq.TextFocus(false, true);
                return;
            }

            //if (FindFirsRowByItemSeq(ucLabelEditItemSeq.Value.Trim()) < 0)
            //{
            //    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_ItemSeqNotFound"));
            //    this.ucLabelEditItemSeq.TextFocus(false, true);
            //    return;
            //}
        }

        //Added By Bernard 2010/11
        private string GetItemCodeFromItemLot(string barcode)
        {
            string sql = "select " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(ItemLot)) + " ";
            sql += "from tblitemlot ";
            sql += "where lotno = '" + barcode.Trim().ToUpper() + "' ";

            object[] itemLot = this.DataProvider.CustomQuery(typeof(ItemLot), new SQLCondition(sql));
            if (itemLot != null && itemLot.Length > 0)
            {
                return ((ItemLot)itemLot[0]).Mcode;
            }
            else
            {
                return "";
            }
        }
        //End Added

        private void ucLabelEditBarcode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
                return;

            //Modified By Bernard @ 2011-09
            //if (!ValidateInput())
            //    return;

            //Cache new minno
            //string inputItemSeq = ucLabelEditItemSeq.Value.Trim();
            string inputBarCode = Web.Helper.FormatHelper.CleanString(ucLabelEditBarcode.Value.Trim());
            //int rowNo = FindFirsRowByItemSeq(inputItemSeq);
            int rowNo = 0;
            string itemCode = string.Empty;
            string parseTypeSetting = string.Empty;
            bool findRowNum = false;

            itemCode = GetItemCodeFromItemLot(inputBarCode);
            if (itemCode == "")
            {
                //Error;
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_CannotFindItemCode"));
                this.ucLabelEditBarcode.TextFocus(false, true);
                return;
            }
            for (int i = 0; i < ultraGridLotParts.Rows.Count; i++)
            {
                parseTypeSetting = "," + ultraGridLotParts.Rows[i].Cells[5].Value.ToString() + ",";
                if (ultraGridLotParts.Rows[i].Cells[2].Value.ToString() == itemCode
                   && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PREPARE.ToLower() + ",") >= 0)
                {
                    findRowNum = true;
                    rowNo = i;
                    break;
                }
            }
            
            if (findRowNum == false)//找不到行
            {
                //Error;
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_CannotFindItemCode"));
                this.ucLabelEditBarcode.TextFocus(false, true);
                return;
            }
            //End Modified

            //add By Jarvis 20120314,检查该批的库存数量是否大于0
            if (_InventoryFacade.QueryStorageLotInfoLotQty(inputBarCode.Trim(), itemCode) <= 0)
            {
                //Error;
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_LotQtyIsZeroCannotUse"));
                this.ucLabelEditBarcode.TextFocus(false, true);
                return;
            }

            OPBOMDetail opBOMDetail = (OPBOMDetail)_opBOMDetailList[rowNo];

            MINNO newMINNO = _MaterialFacade.CreateNewMINNO();

            newMINNO.INNO = Guid.NewGuid().ToString();
            //newMINNO.Sequence = 0;//修改Seq
            newMINNO.MOCode = this._moCode;
            newMINNO.RouteCode = this._selectedRouteCode;
            newMINNO.OPCode = this._currOPCode;
            newMINNO.ResourceCode = this._currResCode;
            newMINNO.OPBOMVersion = this._moBOMVersion;
            newMINNO.OPBOMCode = this._moBOMCode;

            newMINNO.ItemCode = opBOMDetail.ItemCode;
            newMINNO.MItemCode = opBOMDetail.OPBOMItemCode;
            newMINNO.MItemName = opBOMDetail.OPBOMItemName;
            newMINNO.MSourceItemCode = opBOMDetail.OPBOMSourceItemCode;
            newMINNO.Qty = opBOMDetail.OPBOMItemQty;
            newMINNO.MItemPackedNo = inputBarCode.Trim();
            newMINNO.LotNO = inputBarCode.Trim();//add By Jarvis

            newMINNO.IsTry = "N";
            newMINNO.IsLast = "Y";
            newMINNO.MaintainUser = ApplicationService.Current().UserCode;

            //add by Jarvis 20120314,Seq,修改为按同一首选料产生seq
            newMINNO.Sequence = _MaterialFacade.GetUniqueMINNOSequence(this._moCode, this._selectedRouteCode, this._currOPCode, this._currResCode, this._moBOMVersion, opBOMDetail.OPBOMSourceItemCode);

            //Modified By Bernard @2011-09
            //if (inputBarCode.Trim() == string.Empty)

            //修改删除逻辑，Jarvis 20120314
            for (int row= 0; row < ultraGridLotParts.Rows.Count; row++)
            {
                if (inputBarCode.Trim() == ultraGridLotParts.Rows[row].Cells[3].Value.ToString())
                {
                    if (MessageBox.Show(MutiLanguages.ParserMessage("$Already_Prepared_Material"), MutiLanguages.ParserMessage("$ShowMessage"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        Application.DoEvents();
                        this.ucLabelEditBarcode.TextFocus(false, true);
                        return;
                    }
                    //End Modified

                    //去掉,Jarvis 20120314
                    //if (_minnoToSave.Count > 0)
                    //{
                    //    object[] ObjCheck = new object[_minnoToSave.Count];
                    //    _minnoToSave.CopyTo(ObjCheck);
                    //    for (int i = 0; i < ObjCheck.Length; i++)
                    //    {
                    //        if (((MINNO)ObjCheck[i]).MSourceItemCode == newMINNO.MSourceItemCode)
                    //        {
                    //            _minnoToSave.Remove((MINNO)ObjCheck[i]);
                    //        }
                    //    }
                    //}
                    //this._minnoToSave.Add(newMINNO);
                    //this._notSaved = true;

                    //add by Jarvis for delete,从保存的方法中移过来，保存按钮只保留添加
                    //因可以备同一物料的多个批,故添加批号(最小包装条码列)
                    //begin
                    object[] minnos = _MaterialFacade.QueryMINNO(this._moCode, this._selectedRouteCode, this._currOPCode,
                                                                    this._currResCode, this._moBOMVersion, newMINNO.MItemCode, newMINNO.MSourceItemCode, newMINNO.MItemPackedNo);

                    if (minnos != null && minnos.Length > 0)
                    {
                        for (int j = 0; j < minnos.Length; j++)
                        {
                            this._MaterialFacade.DeleteMINNO((MINNO)minnos[j]);
                        }
                    }
                    //end

                    //Modified By Bernard @ 2011-09
                    //this.ultraGridLotParts.Rows[rowNo].Cells[3].Value = inputBarCode;
                    //this.ultraGridLotParts.Rows[rowNo].Cells[3].Value = string.Empty;//去掉,Jarvis 20120314
                    this.RequestData();//刷新数据，add by Jarvis 20120314
                    Application.DoEvents();
                    this.ucLabelEditBarcode.TextFocus(true, true);
                    //FindFirsEmptyRow();
                    //End modified
                    return;
                }
            }
            

            if (ParseMoreInfo(rowNo, newMINNO))
            {
                //去掉,Jarvis 20120314
                //add by hiro 08/10/13
                //for (int i = 0; i < _minnoToSave.Count; i++)
                //{
                //    if (((MINNO)_minnoToSave[i]).MSourceItemCode == newMINNO.MSourceItemCode && 
                //        ((MINNO)_minnoToSave[i]).MItemPackedNo.ToString().Trim()!=string.Empty &&
                //        ((MINNO)_minnoToSave[i]).MItemCode.ToString().Trim() != newMINNO.MItemCode)
                //    { 
                //        this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, newMINNO.MSourceItemCode + "$CS_Error_Itemcollection"));
                //        this.ucLabelEditItemSeq.Value = string.Empty;
                //        this.ucLabelEditBarcode.TextFocus(true, true);
                //        return;
                //    }

                //    if (((MINNO)_minnoToSave[i]).MSourceItemCode == newMINNO.MSourceItemCode &&
                //        ((MINNO)_minnoToSave[i]).MItemPackedNo.ToString().Trim() != string.Empty &&
                //        ((MINNO)_minnoToSave[i]).MItemCode.ToString().Trim() == newMINNO.MItemCode)
                //    {
                //        this._minnoToSave.Remove((MINNO)_minnoToSave[i]);
                //        this._minnoToSave.Add(newMINNO);
                //        this._notSaved = true;
                //        this.ultraGridLotParts.Rows[rowNo].Cells[3].Value = inputBarCode;
                //        //FindFirsEmptyRow();
                //        return;
                //    }
                //}

                //去除同一首选料的子阶料只能备一种物料，Jarvis remove 20120314
                //for (int j = 0; j < this.ultraGridLotParts.Rows.Count; j++)
                //{
                //    if (this.ultraGridLotParts.Rows[j].Cells[1].Text.Trim()== newMINNO.MSourceItemCode.Trim()
                //        && this.ultraGridLotParts.Rows[j].Cells[3].Text.Trim() != string.Empty
                //        && this.ultraGridLotParts.Rows[j].Cells[2].Text.Trim() != newMINNO.MItemCode.Trim())
                //    {
                //        this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, newMINNO.MSourceItemCode + "$CS_Error_Itemcollection"));
                //        this.ucLabelEditItemSeq.Value = string.Empty;
                //        this.ucLabelEditBarcode.TextFocus(false, true);
                //        return;
                //    }
                //}
                //end by hiro

                this._minnoToSave.Add(newMINNO);
                this._notSaved = true;
                //this.ultraGridLotParts.Rows[rowNo].Cells[3].Value = inputBarCode;//去掉Jarvis 20120314
                //FindFirsEmptyRow();
                Application.DoEvents();
                this.ucLabelEditBarcode.TextFocus(false, true);
                this.ucButtonSave_Click(null, null);
            }

            //扫完批次条码检查通过后，自动保存 Jarvis
            //this.ucButtonSave_Click(null, null);
        }

        private void ucButtonSave_Click(object sender, System.EventArgs e)
        {
            //备料时不需要全部完成
            //if (!ValidateComplete())
            //    return;

            try
            {
                this.DataProvider.BeginTransaction();

                for (int i = 0; i < this._minnoToSave.Count; i++)
                {
                    if (((MINNO)_minnoToSave[i]).MItemPackedNo == string.Empty)
                    {
                        //注释掉，Jarvis
                        //object[] minnos = _MaterialFacade.QueryMINNO(this._moCode, this._selectedRouteCode, this._currOPCode,
                        //                                        this._currResCode, this._moBOMVersion, ((MINNO)_minnoToSave[i]).MItemCode, ((MINNO)_minnoToSave[i]).MSourceItemCode);

                        //if (minnos != null && minnos.Length > 0)
                        //{
                        //    for (int j = 0; j < minnos.Length; j++)
                        //    {
                        //        this._MaterialFacade.DeleteMINNO((MINNO)minnos[j]);
                        //    }
                        //}
                    }
                    else
                    {
                        this._MaterialFacade.AddMINNO((MINNO)_minnoToSave[i]);//去掉Update操作,Jarvis 20120314
                    }
                }

                for (int i = 0; i < this._onwipItemToSave.Count; i++)
                {
                    this._DataCollectFacade.AddOnWIPItem((OnWIPItem)_onwipItemToSave[i]);
                }

                this.DataProvider.CommitTransaction();

                this._minnoToSave.Clear();
                this._onwipItemToSave.Clear();
                this._notSaved = false;

                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Success, "$CS_Save_Success"));
                //UnLockMOInput();//Jarvis 20120314
                this.ucLabelEditItemSeq.Value = string.Empty;
                ucLabelEditBarcode.Value = string.Empty;
                this.ucLabelEditBarcode.TextFocus(false, true);//modified By Jarvis 20120314

                this.RequestData();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
            }
        }

        private void ucButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucButtonRefresh_Click(object sender, EventArgs e)
        {
            this.RequestData();
        }

        private void ClearInput()
        {
            //this.ucLabelEditItemSeq.Value = string.Empty;
            this.ucLabelEditBarcode.Value = string.Empty;
            //this.ucLabelEditItemSeq.TextFocus(false, true);
            this.ucLabelEditBarcode.TextFocus(false, true);
        }

        /* 不使用顺序号，此处验证取消  Cancel By Bernard
        private bool ValidateInput()
        {
            if (ucLabelEditItemSeq.Value.Trim().Length <= 0)
            {
                //Application.DoEvents();
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_ItemSeqEmpty"));
                ucLabelEditItemSeq.TextFocus(false, true);
                return false;
            }

            if (FindFirsRowByItemSeq(ucLabelEditItemSeq.Value.Trim()) < 0)
            {
                //Application.DoEvents();
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_ItemSeqNotFound"));
                this.ucLabelEditItemSeq.TextFocus(false, true);
                return false;
            }

            //if (ucLabelEditBarcode.Value.Trim().Length <= 0)
            //{


            //    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_BarcodeEmpty"));
            //    //Application.DoEvents();
            //    ucLabelEditBarcode.TextFocus(false, true);
            //    return false;
            //}

            return true;
        }
        

        private bool ValidateComplete()
        {
            bool complete = true;

            for (int i = 0; i < ultraGridLotParts.Rows.Count; i++)
            {
                if (ultraGridLotParts.Rows[i].Cells[2].Text.Trim().Length <= 0)
                {
                    complete = false;
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_LotControlMaterial_NotComplete"));
                    this.ucLabelEditItemSeq.TextFocus(false, true);
                    break;
                }
            }

            return complete;
        }

        private void FindFirsEmptyRow()
        {
            for (int i = 0; i < this.ultraGridLotParts.Rows.Count; i++)
            {
                if (this.ultraGridLotParts.Rows[i].Cells[2].Text.Trim().Length <= 0)
                {
                    //this.ucLabelEditItemSeq.Value = this.ultraGridLotParts.Rows[i].Cells[0].Text.Trim();
                    this.ucLabelEditBarcode.Value = string.Empty;
                    this.ucLabelEditBarcode.TextFocus(false, true);
                    break;
                }

                ClearInput();
            }
        }

        private int FindFirsRowByItemSeq(string itemSeq)
        {
            for (int i = 0; i < this.ultraGridLotParts.Rows.Count; i++)
            {
                if (this.ultraGridLotParts.Rows[i].Cells[0].Text.Trim() == itemSeq.Trim())
                    return i;
            }

            return -1;
        }
         * */

        private bool ParseMoreInfo(int rowNo, MINNO newMINNO)
        {
            MINNO minno = new MINNO();
            Messages msg = _DataCollectFacade.GetMINNOByBarcode((OPBOMDetail)_opBOMDetailList[rowNo], newMINNO.MItemPackedNo, newMINNO.MOCode, null, false,false, out minno);

            //Modified By Bernard @ 2011-09
            //if (msg.IsSuccess())
            if (msg.IsSuccess() && minno != null)
            {
                newMINNO.VendorCode = minno.VendorCode;
                newMINNO.VendorItemCode = minno.VendorItemCode;
                newMINNO.DateCode = minno.DateCode;
                newMINNO.LotNO = minno.LotNO;
                newMINNO.BIOS = minno.BIOS;
                newMINNO.PCBA = minno.PCBA;
                newMINNO.Version = minno.Version;
            }
            else
            {
                //Add By Bernard @ 2011-09
                if (msg.Count() == 0)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$Error_CannotFindItemCode"));
                }
                //End Add
                this.ShowMessage(msg);
                this.ucLabelEditBarcode.TextFocus(false, true);
                return false;
            }
            return true;
        }

        private bool CompareItem(string opBOMDetailItemCode, string inputMaterialCode)
        {
            return opBOMDetailItemCode == inputMaterialCode;
        }

        #endregion

        #region DataSource

        private void RequestData()
        {
            if (this._moCode == string.Empty
                || this._currResCode == string.Empty
                || this._currOPCode == string.Empty
                || this._selectedRouteCode == string.Empty)
            {
                this.dataTableLotMaterials.Rows.Clear();
            }
            else
            {
                this.GridBind();
            }

            this._notSaved = false;
        }

        private void GridBind()
        {
            this.dataTableLotMaterials.Rows.Clear();
            this._opBOMDetailList.Clear();

            object[] objs = this.LoadOPBOMDetailSource();
            int rowNo = 0;            
            if (objs != null)
            {
                int seqNum = 1;
                foreach (OPBOMDetailAndMINNO obj in objs)
                {
                    if (obj.OPBOMValid.ToString() == BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING
                        && obj.OPBOMItemControlType.ToLower() == BOMItemControlType.ITEM_CONTROL_LOT.ToLower())
                    {
                        this.dataTableLotMaterials.Rows.Add(this.GetRow(obj));
                        seqNum += 1;
                        this._moBOMCode = obj.OPBOMCode;
                        _opBOMDetailList.Add(rowNo, obj);
                        rowNo++;
                    }
                }
            }

            if (rowNo <= 0)
            {
                this.ShowMessage("$CS_NoLotMaterialFound");

                if (ucLabelEditMOCode.Enabled)
                    this.ucLabelEditMOCode.TextFocus(false, true);
                else
                    this.ucLabComboxRoute.Focus();
            }
        }

        private object[] LoadOPBOMDetailSource()
        {
            if (this.chkShowZero.Checked)
            {
                return this._OPBOMFacade.QueryOPBOMDetail_New(_moItemCode, string.Empty, string.Empty,
                _moBOMVersion, _selectedRouteCode, _currOPCode, -1, int.MinValue, int.MaxValue,
                GlobalVariables.CurrentOrganizations.First().OrganizationID, true, _moCode, _currResCode, true);//获取BOM备料信息，Jarvis
            }
            else
            {
                return this._OPBOMFacade.QueryOPBOMDetail_New(_moItemCode, string.Empty, string.Empty,
                _moBOMVersion, _selectedRouteCode, _currOPCode, -1, int.MinValue, int.MaxValue,
                GlobalVariables.CurrentOrganizations.First().OrganizationID, true, _moCode, _currResCode, false);//获取BOM备料信息，Jarvis
            }
            
        }

        private object[] GetRow(object obj)
        {
            if (obj == null)
            {
                return new object[] { "", "", "", "", "", "", "", "" };
            }

            OPBOMDetailAndMINNO opBOMDetailAndMINNO = (OPBOMDetailAndMINNO)obj;
            //string packedNo = string.Empty;

            //object[] minnos = _MaterialFacade.QueryMINNO(this._moCode, this._selectedRouteCode, this._currOPCode, this._currResCode, this._moBOMVersion, opBOMDetail.OPBOMItemCode, opBOMDetail.OPBOMSourceItemCode);

            //if (minnos != null)
                //packedNo = ((MINNO)minnos[0]).MItemPackedNo;

            return new object[] {
				opBOMDetailAndMINNO.OPBOMItemSeq,
                opBOMDetailAndMINNO.OPBOMSourceItemCode,
				opBOMDetailAndMINNO.OPBOMItemCode,
				opBOMDetailAndMINNO.MItemPackedNo,
				opBOMDetailAndMINNO.EAttribute1,
                opBOMDetailAndMINNO.OPBOMParseType,
                opBOMDetailAndMINNO.CheckStatus,
                opBOMDetailAndMINNO.OPBOMCheckType,
                opBOMDetailAndMINNO.SerialNoLength,
                opBOMDetailAndMINNO.LotQty,
                opBOMDetailAndMINNO.LotActQty,
                opBOMDetailAndMINNO.Seq
								};
        }

        #endregion

        private void bntLock_Click(object sender, EventArgs e)
        {
            if (bntLock.Caption == MutiLanguages.ParserString("Lock"))
            {
                LockMOInput();
            }
            else
            {
                UnLockMOInput();
            }
        }

        private void LockMOInput()
        {
            bntLock.Caption = MutiLanguages.ParserString("UnLock");
            ucLabelEditMOCode.Enabled = false;
        }

        private void UnLockMOInput()
        {
            this.dataTableLotMaterials.Rows.Clear();
            bntLock.Caption = MutiLanguages.ParserString("Lock");
            ucLabelEditMOCode.Enabled = true;
            ucLabelEditMOCode.TextFocus(false, true);
        }
        
    }
}

