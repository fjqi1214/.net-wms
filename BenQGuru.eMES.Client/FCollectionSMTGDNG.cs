using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using UserControl;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.ATE;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.TS;


namespace BenQGuru.eMES.Client
{
    /// <summary>
    /// FCollectionSMTGDNG 的摘要说明。
    /// Laws Lu,2005/08/10,调整页面逻辑
    /// </summary>
    public class FCollectionSMTGDNG : BaseForm
    {
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCLabelEdit txtRunningCard;
        private UserControl.UCLabelCombox cbxErrorGroup;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdError;
        private UserControl.UCLabelEdit txtErrorCode;
        private UserControl.UCLabelEdit txtLocation;
        private UserControl.UCLabelCombox cbxAB;
        private UserControl.UCButton btnSave;
        private UserControl.UCButton btnDelete;
        private UserControl.UCButton btnExit;
        private UserControl.UCButton btnAdd;
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        private ProductInfo product;
        private UserControl.UCLabelEdit txtMEMO;
        private System.Windows.Forms.RadioButton rdoNG;
        private System.Windows.Forms.RadioButton rdoGOOD;
        private System.Data.DataSet dataSet1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private UserControl.UCLabelEdit txtGOMO;
        private System.Windows.Forms.TextBox CollectedCount;
        private System.Windows.Forms.Label lblCollectedQty;
        private System.Windows.Forms.CheckBox chkAutoGetData;
        private System.Data.DataTable dtSMT;

        private string _FunctionName = string.Empty;

        /// <summary>
        /// 获得产品信息
        /// </summary>
        /// <returns></returns>
        private Messages GetProduct()
        {
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(txtRunningCard.Value.Trim().ToUpper(), string.Empty);

            Messages productmessages = new Messages();
            ActionOnLineHelper dataCollect = new ActionOnLineHelper(DataProvider);
            try
            {
                productmessages.AddMessages(dataCollect.GetIDInfo(sourceRCard.Trim()));
                if (productmessages.IsSuccess())
                {
                    product = (ProductInfo)productmessages.GetData().Values[0];
                }
                else
                {
                    txtRunningCard.Value = String.Empty;
                }
            }
            catch (Exception e)
            {
                productmessages.Add(new UserControl.Message(e));

            }
            return productmessages;
        }

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FCollectionSMTGDNG()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(this.grdError);
            cbxAB.AddItem("", "");
            cbxAB.AddItem(MutiLanguages.ParserString(Web.Helper.ItemLocationSide.ItemLocationSide_A), Web.Helper.ItemLocationSide.ItemLocationSide_A);
            cbxAB.AddItem(MutiLanguages.ParserString(Web.Helper.ItemLocationSide.ItemLocationSide_B), Web.Helper.ItemLocationSide.ItemLocationSide_B);
            cbxAB.SelectedIndex = 0;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCollectionSMTGDNG));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Table1", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RCard");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ErrorGroup");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ErrorCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ErrorLocation");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ABFace");
            this.panelButton = new System.Windows.Forms.Panel();
            this.chkAutoGetData = new System.Windows.Forms.CheckBox();
            this.CollectedCount = new System.Windows.Forms.TextBox();
            this.lblCollectedQty = new System.Windows.Forms.Label();
            this.txtMEMO = new UserControl.UCLabelEdit();
            this.cbxAB = new UserControl.UCLabelCombox();
            this.btnSave = new UserControl.UCButton();
            this.btnDelete = new UserControl.UCButton();
            this.txtErrorCode = new UserControl.UCLabelEdit();
            this.cbxErrorGroup = new UserControl.UCLabelCombox();
            this.txtGOMO = new UserControl.UCLabelEdit();
            this.txtLocation = new UserControl.UCLabelEdit();
            this.txtRunningCard = new UserControl.UCLabelEdit();
            this.rdoNG = new System.Windows.Forms.RadioButton();
            this.rdoGOOD = new System.Windows.Forms.RadioButton();
            this.btnExit = new UserControl.UCButton();
            this.btnAdd = new UserControl.UCButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdError = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dtSMT = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataSet1 = new System.Data.DataSet();
            this.panelButton.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSMT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelButton
            // 
            this.panelButton.Controls.Add(this.chkAutoGetData);
            this.panelButton.Controls.Add(this.CollectedCount);
            this.panelButton.Controls.Add(this.lblCollectedQty);
            this.panelButton.Controls.Add(this.txtMEMO);
            this.panelButton.Controls.Add(this.cbxAB);
            this.panelButton.Controls.Add(this.btnSave);
            this.panelButton.Controls.Add(this.btnDelete);
            this.panelButton.Controls.Add(this.txtErrorCode);
            this.panelButton.Controls.Add(this.cbxErrorGroup);
            this.panelButton.Controls.Add(this.txtGOMO);
            this.panelButton.Controls.Add(this.txtLocation);
            this.panelButton.Controls.Add(this.txtRunningCard);
            this.panelButton.Controls.Add(this.rdoNG);
            this.panelButton.Controls.Add(this.rdoGOOD);
            this.panelButton.Controls.Add(this.btnExit);
            this.panelButton.Controls.Add(this.btnAdd);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(0, 310);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(656, 200);
            this.panelButton.TabIndex = 1;
            // 
            // chkAutoGetData
            // 
            this.chkAutoGetData.Location = new System.Drawing.Point(336, 91);
            this.chkAutoGetData.Name = "chkAutoGetData";
            this.chkAutoGetData.Size = new System.Drawing.Size(298, 24);
            this.chkAutoGetData.TabIndex = 12;
            this.chkAutoGetData.Text = "自动从设备获取数据";
            // 
            // CollectedCount
            // 
            this.CollectedCount.Location = new System.Drawing.Point(504, 128);
            this.CollectedCount.Name = "CollectedCount";
            this.CollectedCount.ReadOnly = true;
            this.CollectedCount.Size = new System.Drawing.Size(128, 21);
            this.CollectedCount.TabIndex = 16;
            this.CollectedCount.Text = "0";
            // 
            // lblCollectedQty
            // 
            this.lblCollectedQty.Location = new System.Drawing.Point(424, 136);
            this.lblCollectedQty.Name = "lblCollectedQty";
            this.lblCollectedQty.Size = new System.Drawing.Size(79, 16);
            this.lblCollectedQty.TabIndex = 15;
            this.lblCollectedQty.Text = "已采集数量";
            // 
            // txtMEMO
            // 
            this.txtMEMO.AllowEditOnlyChecked = true;
            this.txtMEMO.AutoSelectAll = false;
            this.txtMEMO.AutoUpper = true;
            this.txtMEMO.Caption = "备注";
            this.txtMEMO.Checked = false;
            this.txtMEMO.EditType = UserControl.EditTypes.String;
            this.txtMEMO.Location = new System.Drawing.Point(81, 91);
            this.txtMEMO.MaxLength = 80;
            this.txtMEMO.Multiline = true;
            this.txtMEMO.Name = "txtMEMO";
            this.txtMEMO.PasswordChar = '\0';
            this.txtMEMO.ReadOnly = false;
            this.txtMEMO.ShowCheckBox = false;
            this.txtMEMO.Size = new System.Drawing.Size(237, 61);
            this.txtMEMO.TabIndex = 11;
            this.txtMEMO.TabNext = true;
            this.txtMEMO.Value = "";
            this.txtMEMO.WidthType = UserControl.WidthTypes.Long;
            this.txtMEMO.XAlign = 118;
            // 
            // cbxAB
            // 
            this.cbxAB.AllowEditOnlyChecked = true;
            this.cbxAB.Caption = "";
            this.cbxAB.Checked = false;
            this.cbxAB.Location = new System.Drawing.Point(493, 64);
            this.cbxAB.Name = "cbxAB";
            this.cbxAB.SelectedIndex = -1;
            this.cbxAB.ShowCheckBox = false;
            this.cbxAB.Size = new System.Drawing.Size(141, 24);
            this.cbxAB.TabIndex = 7;
            this.cbxAB.WidthType = UserControl.WidthTypes.Normal;
            this.cbxAB.XAlign = 501;
            this.cbxAB.ComboBoxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxAB_ComboBoxKeyPress);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.ButtonType = UserControl.ButtonTypes.Save;
            this.btnSave.Caption = "保存";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(352, 160);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 22);
            this.btnSave.TabIndex = 9;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.ButtonType = UserControl.ButtonTypes.Delete;
            this.btnDelete.Caption = "删除";
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Location = new System.Drawing.Point(216, 160);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(88, 22);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtErrorCode
            // 
            this.txtErrorCode.AllowEditOnlyChecked = true;
            this.txtErrorCode.AutoSelectAll = false;
            this.txtErrorCode.AutoUpper = true;
            this.txtErrorCode.Caption = "不良代码";
            this.txtErrorCode.Checked = false;
            this.txtErrorCode.EditType = UserControl.EditTypes.String;
            this.txtErrorCode.Location = new System.Drawing.Point(57, 64);
            this.txtErrorCode.MaxLength = 40;
            this.txtErrorCode.Multiline = false;
            this.txtErrorCode.Name = "txtErrorCode";
            this.txtErrorCode.PasswordChar = '\0';
            this.txtErrorCode.ReadOnly = false;
            this.txtErrorCode.ShowCheckBox = false;
            this.txtErrorCode.Size = new System.Drawing.Size(194, 24);
            this.txtErrorCode.TabIndex = 5;
            this.txtErrorCode.TabNext = true;
            this.txtErrorCode.Value = "";
            this.txtErrorCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtErrorCode.XAlign = 118;
            this.txtErrorCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtErrorCode_TxtboxKeyPress);
            // 
            // cbxErrorGroup
            // 
            this.cbxErrorGroup.AllowEditOnlyChecked = true;
            this.cbxErrorGroup.Caption = "不良代码组";
            this.cbxErrorGroup.Checked = false;
            this.cbxErrorGroup.Location = new System.Drawing.Point(285, 37);
            this.cbxErrorGroup.Name = "cbxErrorGroup";
            this.cbxErrorGroup.SelectedIndex = -1;
            this.cbxErrorGroup.ShowCheckBox = false;
            this.cbxErrorGroup.Size = new System.Drawing.Size(206, 24);
            this.cbxErrorGroup.TabIndex = 2;
            this.cbxErrorGroup.Visible = false;
            this.cbxErrorGroup.WidthType = UserControl.WidthTypes.Normal;
            this.cbxErrorGroup.XAlign = 358;
            // 
            // txtGOMO
            // 
            this.txtGOMO.AllowEditOnlyChecked = true;
            this.txtGOMO.AutoSelectAll = false;
            this.txtGOMO.AutoUpper = true;
            this.txtGOMO.Caption = "设定归属工单";
            this.txtGOMO.Checked = false;
            this.txtGOMO.EditType = UserControl.EditTypes.String;
            this.txtGOMO.Location = new System.Drawing.Point(257, 8);
            this.txtGOMO.MaxLength = 40;
            this.txtGOMO.Multiline = false;
            this.txtGOMO.Name = "txtGOMO";
            this.txtGOMO.PasswordChar = '\0';
            this.txtGOMO.ReadOnly = false;
            this.txtGOMO.ShowCheckBox = true;
            this.txtGOMO.Size = new System.Drawing.Size(234, 24);
            this.txtGOMO.TabIndex = 3;
            this.txtGOMO.TabNext = true;
            this.txtGOMO.Value = "";
            this.txtGOMO.WidthType = UserControl.WidthTypes.Normal;
            this.txtGOMO.XAlign = 358;
            this.txtGOMO.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGOMO_TxtboxKeyPress);
            this.txtGOMO.CheckBoxCheckedChanged += new System.EventHandler(this.txtGOMO_CheckBoxCheckedChanged);
            // 
            // txtLocation
            // 
            this.txtLocation.AllowEditOnlyChecked = true;
            this.txtLocation.AutoSelectAll = false;
            this.txtLocation.AutoUpper = true;
            this.txtLocation.Caption = "不良位置";
            this.txtLocation.Checked = false;
            this.txtLocation.EditType = UserControl.EditTypes.String;
            this.txtLocation.Location = new System.Drawing.Point(297, 64);
            this.txtLocation.MaxLength = 40;
            this.txtLocation.Multiline = false;
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.PasswordChar = '\0';
            this.txtLocation.ReadOnly = false;
            this.txtLocation.ShowCheckBox = false;
            this.txtLocation.Size = new System.Drawing.Size(194, 24);
            this.txtLocation.TabIndex = 6;
            this.txtLocation.TabNext = true;
            this.txtLocation.Value = "";
            this.txtLocation.WidthType = UserControl.WidthTypes.Normal;
            this.txtLocation.XAlign = 358;
            this.txtLocation.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLocation_TxtboxKeyPress);
            // 
            // txtRunningCard
            // 
            this.txtRunningCard.AllowEditOnlyChecked = true;
            this.txtRunningCard.AutoSelectAll = false;
            this.txtRunningCard.AutoUpper = true;
            this.txtRunningCard.Caption = "产品序列号";
            this.txtRunningCard.Checked = false;
            this.txtRunningCard.EditType = UserControl.EditTypes.String;
            this.txtRunningCard.Location = new System.Drawing.Point(45, 37);
            this.txtRunningCard.MaxLength = 40;
            this.txtRunningCard.Multiline = false;
            this.txtRunningCard.Name = "txtRunningCard";
            this.txtRunningCard.PasswordChar = '\0';
            this.txtRunningCard.ReadOnly = false;
            this.txtRunningCard.ShowCheckBox = false;
            this.txtRunningCard.Size = new System.Drawing.Size(206, 24);
            this.txtRunningCard.TabIndex = 4;
            this.txtRunningCard.TabNext = true;
            this.txtRunningCard.Value = "";
            this.txtRunningCard.WidthType = UserControl.WidthTypes.Normal;
            this.txtRunningCard.XAlign = 118;
            this.txtRunningCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRunningCard_TxtboxKeyPress);
            // 
            // rdoNG
            // 
            this.rdoNG.Location = new System.Drawing.Point(104, 8);
            this.rdoNG.Name = "rdoNG";
            this.rdoNG.Size = new System.Drawing.Size(64, 24);
            this.rdoNG.TabIndex = 2;
            this.rdoNG.Tag = "1";
            this.rdoNG.Text = "不良品";
            this.rdoNG.CheckedChanged += new System.EventHandler(this.rdoNG_CheckedChanged);
            // 
            // rdoGOOD
            // 
            this.rdoGOOD.Checked = true;
            this.rdoGOOD.Location = new System.Drawing.Point(48, 8);
            this.rdoGOOD.Name = "rdoGOOD";
            this.rdoGOOD.Size = new System.Drawing.Size(56, 24);
            this.rdoGOOD.TabIndex = 1;
            this.rdoGOOD.TabStop = true;
            this.rdoGOOD.Tag = "1";
            this.rdoGOOD.Text = "良品";
            this.rdoGOOD.CheckedChanged += new System.EventHandler(this.rdoGOOD_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(488, 160);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 10;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.Control;
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.ButtonType = UserControl.ButtonTypes.Add;
            this.btnAdd.Caption = "新增";
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Location = new System.Drawing.Point(80, 160);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 22);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdError);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(656, 310);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "不良信息";
            // 
            // grdError
            // 
            this.grdError.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdError.DataSource = this.dtSMT;
            ultraGridColumn1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn1.Header.Caption = "产品序列号";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn2.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn2.Header.Caption = "不良代码组";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn3.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn3.Header.Caption = "不良代码";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn4.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn4.Header.Caption = "不良位置";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn5.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn5.Header.Caption = "A/B面";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5});
            this.grdError.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdError.Location = new System.Drawing.Point(3, 17);
            this.grdError.Name = "grdError";
            this.grdError.Size = new System.Drawing.Size(650, 290);
            this.grdError.TabIndex = 14;
            // 
            // dtSMT
            // 
            this.dtSMT.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5});
            this.dtSMT.TableName = "Table1";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "RCard";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "ErrorGroup";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "ErrorCode";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "ErrorLocation";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "ABFace";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Locale = new System.Globalization.CultureInfo("zh-CN");
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dtSMT});
            // 
            // FCollectionSMTGDNG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(656, 510);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelButton);
            this.KeyPreview = true;
            this.Name = "FCollectionSMTGDNG";
            this.Text = "SMT良品/不良品采集";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FCollectionSMTGDNG_Load);
            this.Closed += new System.EventHandler(this.FCollectionSMTGDNG_Closed);
            this.Activated += new System.EventHandler(this.FCollectionSMTGDNG_Activated);
            this.panelButton.ResumeLayout(false);
            this.panelButton.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSMT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            //获取产品的初始时的序列号
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(txtRunningCard.Value.Trim().ToUpper(), string.Empty);

            if (CheckRunningCard()
                && CheckErrors()
                && CheckErrorGroup()
                && CheckErrorCode()
                && CheckErrorLocation()
                && CheckErrorLocationAndAB())
            {
                dtSMT.Rows.Add(new object[]{
											   sourceRCard.Trim().ToUpper(),cbxErrorGroup.SelectedItemValue,
											   txtErrorCode.Value.Trim().ToUpper(),txtLocation.Value.Trim().ToUpper(),cbxAB.SelectedItemValue
										   });

                dtSMT.AcceptChanges();
                ClearAfterAdd();
            }

        }


        #region Amoi,Laws Lu,修改Add Check逻辑

        private bool CheckRunningCard()
        {
            if (txtRunningCard.Value.Trim() == string.Empty)
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), false);
                txtRunningCard.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
                //System.Windows.Forms.SendKeys.Send("+{TAB}");
                return false;
            }
            //Amoi,Laws Lu,2005/08/02,注释
            //			if ((dtSMT.Rows.Count > 0) && 
            //				(txtRunningCard.Value.Trim().ToUpper() != dtSMT.Rows[0][0].ToString()))
            //			{
            //				txtRunningCard.Enabled = false;
            //				
            //				ApplicationRun.GetInfoForm().AddEx("$CS_Please_Input_Same_RunningCard");
            //				txtRunningCard.TextFocus(false, true);
            //              //System.Windows.Forms.SendKeys.Send("+{TAB}");
            //				return false;
            //				
            //			}
            //EndAmoi
            return true;
        }

        private bool CheckErrorGroup()
        {
            if (cbxErrorGroup.SelectedIndex == -1)
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_ErrorInfo"), false);
                txtErrorCode.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
                //System.Windows.Forms.SendKeys.Send("+{TAB}");
                return false;
            }
            return true;
        }

        private bool CheckErrorCode()
        {
            if (cbxErrorGroup.SelectedIndex == -1)
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_ErrorInfo"), false);
                txtErrorCode.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
                //System.Windows.Forms.SendKeys.Send("+{TAB}");
                return false;
            }
            TSModelFacade tSModelFacade = new TSModelFacade(DataProvider);
            try
            {
                if (!tSModelFacade.IsErrorCodeInGroup(txtErrorCode.Value.Trim().ToUpper(),
                    cbxErrorGroup.SelectedItemValue.ToString()))
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, txtErrorCode.Caption + ": " + txtErrorCode.Value, new UserControl.Message(MessageType.Error, "$CS_ErrorCode_NoGroup"), false);

                    txtErrorCode.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    //System.Windows.Forms.SendKeys.Send("+{TAB}");
                    return false;
                }
            }
            catch (Exception exp)
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtErrorCode.Caption + ": " + this.txtErrorCode.Value, new UserControl.Message(exp), false);
                txtErrorCode.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
                //System.Windows.Forms.SendKeys.Send("+{TAB}");
                return false;
            }

            if (txtLocation.Value.Trim() == string.Empty)
            {
                for (int i = 0; i < dtSMT.Rows.Count; i++)
                {
                    if (dtSMT.Rows[i][3].ToString() == string.Empty
                        && dtSMT.Rows[i][1].ToString() == cbxErrorGroup.SelectedItemText.Trim().ToUpper()
                        && dtSMT.Rows[i][2].ToString() == txtErrorCode.Value.Trim().ToUpper())
                    {
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtErrorCode.Caption + ": " + this.txtErrorCode.Value, new UserControl.Message(UserControl.MessageType.Error, "$CS_Repert_ErrorCode"), false);

                        txtLocation.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;
                        //System.Windows.Forms.SendKeys.Send("+{TAB}");
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CheckErrorLocationAndAB()
        {
            if (cbxAB.SelectedIndex == 0 && txtLocation.Value.Trim() != "")
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_AB"), false);
                cbxAB.Focus();
                return false;
            }
            return true;
        }

        private bool CheckErrorLocation()
        {
            if (cbxAB.SelectedIndex != 0 && txtLocation.Value.Trim() == string.Empty)
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_Location"), false);
                txtLocation.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
                return false;
            }
            if (txtLocation.Value.Trim() != string.Empty)
            {
                for (int i = 0; i < dtSMT.Rows.Count; i++)
                {
                    if (dtSMT.Rows[i][3].ToString() == txtLocation.Value.Trim().ToUpper())
                    {
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, txtLocation.Caption + ": " + txtLocation.Value, new UserControl.Message(MessageType.Error, "$CS_Repert_Location"), false);
                        txtLocation.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;
                        return false;
                    }
                }
            }
            return true;
        }
        //Amoi,Laws Lu,2005/08/05,新增	检查不良
        //1,不良位置重复检查
        //2,不良代码组、不良代码、位置都相同
        private bool CheckErrors()
        {
            for (int i = 0; i < dtSMT.Rows.Count; i++)
            {
                string errGroup = dtSMT.Rows[i][1].ToString().Trim();
                string errCode = dtSMT.Rows[i][2].ToString().Trim();
                string errLocation = dtSMT.Rows[i][3].ToString().Trim();

                //1,不良位置重复检查
                if (errLocation != String.Empty
                    && errLocation == txtLocation.Value.Trim().ToUpper())
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, txtLocation.Caption + ": " + txtLocation.Value, new UserControl.Message(MessageType.Error, "$CS_Repert_Location"), false);
                    txtLocation.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    return false;
                }

                //2,不良代码组、不良代码、位置都相同
                if (errLocation == txtLocation.Value.Trim().ToUpper()
                    && errGroup == cbxErrorGroup.SelectedItemValue.ToString().Trim().ToUpper()
                    && errCode == txtErrorCode.Value.Trim().ToUpper())
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, txtErrorCode.Caption + ": " + txtErrorCode.Value, new UserControl.Message(MessageType.Error, "$CS_Repert_ErrorCode"), false);
                    txtErrorCode.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    return false;
                }
            }

            return true;
        }
        #endregion

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Messages messages = new Messages();

            //Laws Lu,2005/10/19,新增	缓解性能问题
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            DataProvider.BeginTransaction();
            try
            {
                if (txtGOMO.Checked)
                {
                    if (rdoGOOD.Checked)
                    {
                        //Amoi,Laws Lu,2005/08/03,注释
                        //messages = RunGOMO();
                        //EndAmoi

                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, messages, true);

                        if (messages.IsSuccess())
                        {
                            messages = GetProduct();
                            ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, messages, true);
                        }

                        if (messages.IsSuccess())
                        {
                            messages = RunGood(actionCheckStatus);
                            ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, messages, true);
                        }

                    }
                    else if (rdoNG.Checked)
                    {
                        messages = CheckErrorCodes();
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, messages, true);
                        //Amoi,Laws Lu,2005/08/03,注释
                        //						if (messages.IsSuccess())
                        //						{
                        //							messages = RunGOMO();
                        //							ApplicationRun.GetInfoForm().AddEx(messages);
                        //						}
                        //EndAmoi
                        if (messages.IsSuccess())
                        {
                            messages = GetProduct();
                            ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, messages, true);
                        }
                        if (messages.IsSuccess())
                        {
                            ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, messages, true);
                            if (messages.IsSuccess())
                            {
                                messages = RunNG(actionCheckStatus);
                                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, messages, true);
                            }

                        }
                    }
                }
                else
                {
                    if (rdoGOOD.Checked)
                    {
                        messages = RunGood(actionCheckStatus);
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, messages, true);
                    }
                    else if (rdoNG.Checked)
                    {
                        messages = CheckErrorCodes();
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtErrorCode.Caption + ": " + this.txtErrorCode.Value, messages, false);

                        if (messages.IsSuccess())
                        {
                            messages = RunNG(actionCheckStatus);
                            ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, messages, true);
                        }
                    }

                }
                if (messages.IsSuccess())
                {
                    DataProvider.CommitTransaction();
                    ClearAfterSave();

                    /* Added by jessie lee, 2006/1/11, CS219 
                     * SMT良品/不良品采集：提供采集数量的计数和显示功能，类似于良品/不良品采集 */
                    int count = int.Parse(this.CollectedCount.Text) + 1;
                    this.CollectedCount.Text = count.ToString();
                }
                else
                {
                    txtRunningCard.Enabled = true;
                    ClearAfterSave();
                    DataProvider.RollbackTransaction();
                }
            }
            catch (Exception exp)
            {
                DataProvider.RollbackTransaction();
                messages.Add(new UserControl.Message(exp));
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, messages, true);
                //Amoi,Laws Lu,2005/08/06,新增	处理不良品采集没有成功时直接返回,无需设置产品序列号输入框的状态
                if (rdoNG.Checked == true)
                {
                    return;
                }

                if (rdoGOOD.Checked == true)
                {
                    txtRunningCard.Value = String.Empty;
                }
                //EndAmoi
            }
            finally
            {
                //Laws Lu,2005/10/19,新增	缓解性能问题
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            }

            //Amoi,Laws Lu,2005/08/06,新增	处理不良品采集没有成功时直接返回,无需设置产品序列号输入框的状态
            if (rdoNG.Checked == true && !messages.IsSuccess())
            {
                return;
            }
            //EndAmoi

            //Amoi,Laws Lu,2005/08/06,新增	处理不良品采集没有成功时直接返回,无需设置产品序列号输入框的状态
            if (rdoGOOD.Checked == true && !messages.IsSuccess())
            {
                txtRunningCard.Value = String.Empty;
            }
            //EndAmoi

            txtRunningCard.Enabled = true;
            txtRunningCard.TextFocus(false, true);
            //Remove UCLabel.SelectAll;
            //System.Windows.Forms.SendKeys.Send("+{TAB}");
        }

        /// <summary>
        /// GOOD指令采集
        /// </summary>
        /// <returns></returns>
        private Messages RunGood(ActionCheckStatus actionCheckStatus)
        {
            Messages messages = new Messages();
            try
            {
                IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_SMTGOOD);

                //获取产品的原始序列号
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                string sourceRCard = dataCollectFacade.GetSourceCard(this.txtRunningCard.Value.Trim().ToUpper(), string.Empty);

                actionCheckStatus.ProductInfo = product;

                messages.AddMessages(((IActionWithStatus)dataCollectModule).Execute(new ActionEventArgs(ActionType.DataCollectAction_SMTGOOD, sourceRCard.Trim(),
                    ApplicationService.Current().UserCode,
                    ApplicationService.Current().ResourceCode, product), actionCheckStatus));

                if (messages.IsSuccess())
                {
                    messages.Add(new UserControl.Message(MessageType.Success, string.Format("$CS_SMTGOODSUCCESS,$CS_Param_ID:{0}", txtRunningCard.Value.Trim())));
                }
                return messages;
            }
            catch (Exception e)
            {
                messages.Add(new UserControl.Message(e));
                return messages;
            }
        }

        /// <summary>
        /// GOOD指令采集
        /// </summary>
        /// <returns></returns>
        private Messages RunGood(ActionCheckStatus actionCheckStatus, ATETestInfo atetestinfo)
        {
            Messages messages = new Messages();
            try
            {
                IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_SMTGOOD);

                //获取产品的原始序列号
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                string sourceRCard = dataCollectFacade.GetSourceCard(this.txtRunningCard.Value.Trim().ToUpper(), string.Empty);

                actionCheckStatus.ProductInfo = product;

                messages.AddMessages(((IActionWithStatus)dataCollectModule).Execute(
                    new ActionEventArgs(
                    ActionType.DataCollectAction_SMTGOOD,
                    sourceRCard.Trim(),
                    atetestinfo.MaintainUser,
                    atetestinfo.ResCode,
                    product),
                    actionCheckStatus));

                if (messages.IsSuccess())
                {
                    messages.Add(new UserControl.Message(MessageType.Success, string.Format("$CS_SMTGOODSUCCESS,$CS_Param_ID:{0}", txtRunningCard.Value.Trim())));
                }

                return messages;
            }
            catch (Exception e)
            {
                messages.Add(new UserControl.Message(e));
                return messages;
            }
        }

        /// <summary>
        /// NG指令采集
        /// </summary>
        /// <returns></returns>
        private Messages RunNG(ActionCheckStatus actionCheckStatus)
        {
            Messages messages = new Messages();

            //获取产品的原始序列号
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(this.txtRunningCard.Value.Trim().ToUpper(), string.Empty);

            try
            {
                //取不良信息
                object[] errorinfor = GetErrorInfor();

                actionCheckStatus.ProductInfo = product;

                IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_SMTNG);
                messages.AddMessages(((IActionWithStatus)dataCollectModule).Execute(
                    new TSActionEventArgs(ActionType.DataCollectAction_SMTNG,
                    sourceRCard.Trim(),
                    ApplicationService.Current().UserCode,
                    ApplicationService.Current().ResourceCode,
                    product,
                    errorinfor,
                    txtMEMO.Value), actionCheckStatus));

                if (messages.IsSuccess())
                {
                    messages.Add(new UserControl.Message(MessageType.Success, string.Format("$CS_SMTNGSUCCESS,$CS_Param_ID:{0}", txtRunningCard.Value.Trim())));
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
            //获取产品的原始序列号
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(this.txtRunningCard.Value.Trim().ToUpper(), string.Empty);

            Messages messages = new Messages();
            try
            {
                ATEFacade ateFacade = new ATEFacade(this.DataProvider);
                object[] errorinfor = ateFacade.GetErrorInfo(atetestinfo, actionCheckStatus.ProductInfo.LastSimulation.ModelCode);

                actionCheckStatus.ProductInfo = product;

                IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_SMTNG);
                messages.AddMessages(((IActionWithStatus)dataCollectModule).Execute(
                    new TSActionEventArgs(ActionType.DataCollectAction_SMTNG,
                    sourceRCard.Trim(),
                    atetestinfo.MaintainUser,
                    atetestinfo.ResCode,
                    product,
                    errorinfor,
                    txtMEMO.Value), actionCheckStatus));

                if (messages.IsSuccess())
                {
                    messages.Add(new UserControl.Message(MessageType.Success, string.Format("$CS_SMTNGSUCCESS,$CS_Param_ID:{0}", txtRunningCard.Value.Trim())));
                }
                return messages;
            }
            catch (Exception e)
            {
                messages.Add(new UserControl.Message(e));
                return messages;
            }
        }

        /// <summary>
        /// 工单归属采集
        /// </summary>
        /// <returns></returns>
        private Messages RunGOMO(ActionCheckStatus actionCheckStatus)
        {
            //获取产品的最原始的序列号
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(this.txtRunningCard.Value.Trim().ToUpper(), string.Empty);

            Messages messages = new Messages();
            try
            {
                //Laws Lu,2005/09/14,新增	工单不能为空
                if (txtGOMO.Checked == true && txtGOMO.Value.Trim() != String.Empty)
                {
                    actionCheckStatus.ProductInfo = product;

                    //Laws Lu,新建数据采集Action
                    ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);

                    IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_GoMO);
                    messages.AddMessages(onLine.Action(new GoToMOActionEventArgs(ActionType.DataCollectAction_GoMO, sourceRCard.Trim(),
                        ApplicationService.Current().UserCode,
                        ApplicationService.Current().ResourceCode, product, txtGOMO.Value), actionCheckStatus));
                }
                if (messages.IsSuccess())
                {
                    messages.Add(new UserControl.Message(MessageType.Success, "$CS_GOMOSUCCESS"));
                }
                return messages;
            }
            catch (Exception e)
            {
                messages.Add(new UserControl.Message(e));
                return messages;
            }
        }
        private Hashtable listActionCheckStatus = new Hashtable();
        private ActionCheckStatus actionCheckStatus = new ActionCheckStatus();
        private Domain.BaseSetting.Resource Resource;

        private void txtRunningCard_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //txtRunningCard.Value = txtRunningCard.Value.Trim();
            //Amoi,Laws Lu,2005/08/03,修改	页面逻辑调整
            if (e.KeyChar == '\r')
            {
                UserControl.Messages msg = new UserControl.Messages();

                if (txtRunningCard.Value.Trim() == string.Empty)
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), false);
                    //将焦点移到产品序列号输入框
                    txtRunningCard.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    //System.Windows.Forms.SendKeys.Send("+{TAB}");
                }
                else
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, GetProduct(), true);

                    // Added by Icyer 2005/10/28
                    if (Resource == null)
                    {
                        BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
                        Resource = (Domain.BaseSetting.Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);
                    }

                    actionCheckStatus = new ActionCheckStatus();
                    actionCheckStatus.ProductInfo = product;
                    if (actionCheckStatus.ProductInfo != null)
                    {
                        actionCheckStatus.ProductInfo.Resource = Resource;
                    }

                    string strMoCode = String.Empty;
                    if (product != null && product.LastSimulation != null)
                    {
                        strMoCode = product.LastSimulation.MOCode;
                    }

                    if (strMoCode != String.Empty)
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

                    if (txtGOMO.Checked == true && txtGOMO.Value != String.Empty)
                    {
                        msg = RunGOMO(actionCheckStatus);
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, msg, true);
                    }

                    if (!msg.IsSuccess())
                    {
                        e.Handled = true;
                        txtRunningCard.TextFocus(false, true);
                        return;
                    }

                    if (!txtGOMO.Checked && !CheckGOMO())
                    {
                        e.Handled = true;

                        //将焦点移到产品序列号输入框
                        txtRunningCard.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;
                        //System.Windows.Forms.SendKeys.Send("+{TAB}");

                        return;
                    }
                    else if (chkAutoGetData.Checked)
                    {
                        ATEFacade ateFacade = new ATEFacade(this.DataProvider);
                        object[] atedatas = ateFacade.GetATETestInfoByRCard(product.LastSimulation.RunningCard, Resource.ResourceCode);

                        if (atedatas == null || atedatas.Length == 0)
                        {
                            ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, new UserControl.Message(MessageType.Error, "$CS_ATE_DONNOT_HAVE_DATA"), true);
                            txtRunningCard.TextFocus(false, true);
                            //Remove UCLabel.SelectAll;
                            //System.Windows.Forms.SendKeys.Send("+{TAB}");
                            return;
                        }
                        else if (atedatas.Length > 1)
                        {
                            ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, new UserControl.Message(MessageType.Error, ""), true);
                            txtRunningCard.TextFocus(false, true);
                            //Remove UCLabel.SelectAll;
                            //System.Windows.Forms.SendKeys.Send("+{TAB}");
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
                            if (string.Compare(ateTestInfo.TestResult, "OK", true) == 0)
                            {
                                msg = RunGood(actionCheckStatus, ateTestInfo);
                                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, msg, true);
                            }
                            else if (string.Compare(ateTestInfo.TestResult, "NG", true) == 0)
                            {
                                msg = RunNG(actionCheckStatus, ateTestInfo);
                                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, msg, true);
                            }
                            else
                            {
                                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, new UserControl.Message(MessageType.Error, ""), true);
                            }

                            if (msg.IsSuccess())
                            {
                                ateFacade.DeleteATETestInfo(ateTestInfo);
                                DataProvider.CommitTransaction();
                                ClearAfterSave();

                                int count = int.Parse(this.CollectedCount.Text) + 1;
                                this.CollectedCount.Text = count.ToString();
                            }
                            else
                            {
                                txtRunningCard.Enabled = true;
                                ClearAfterSave();
                                DataProvider.RollbackTransaction();
                            }
                        }
                        catch (Exception ex)
                        {
                            DataProvider.RollbackTransaction();
                            msg.Add(new UserControl.Message(ex));
                            ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, msg, true);
                        }
                        finally
                        {
                            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                        }

                        txtRunningCard.Enabled = true;
                        txtRunningCard.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;
                        //System.Windows.Forms.SendKeys.Send("+{TAB}");

                    }
                    else if (rdoGOOD.Checked)
                    {
                        btnSave_Click(sender, e);

                        //将焦点移到产品序列号输入框
                        txtRunningCard.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;
                        //System.Windows.Forms.SendKeys.Send("+{TAB}");

                    }
                    else if (rdoNG.Checked && product.LastSimulation != null)
                    {
                        SetErrorCodeGroupList(product.LastSimulation.ItemCode);

                        if (msg.IsSuccess())
                        {
                            txtErrorCode.TextFocus(false, true);
                            //Remove UCLabel.SelectAll;
                        }
                        else
                        {
                            txtRunningCard.TextFocus(false, true);
                            //Remove UCLabel.SelectAll
                        }
                    }
                    else if (rdoNG.Checked == true && msg.IsSuccess())
                    {
                        ActionOnLineHelper dataCollect = new ActionOnLineHelper(DataProvider);

                        //获取产品的最原始的序列号
                        DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                        string sourceRCard = dataCollectFacade.GetSourceCard(txtRunningCard.Value.Trim().ToUpper(), string.Empty);

                        if (dataCollect.GetIDInfo(sourceRCard.Trim()).IsSuccess())
                        {
                            product = (ProductInfo)dataCollect.GetIDInfo(sourceRCard.Trim()).GetData().Values[0];
                        }

                        SetErrorCodeGroupList(product.LastSimulation.ItemCode);

                        txtErrorCode.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;
                        //System.Windows.Forms.SendKeys.Send("+{TAB}");
                    }

                    //Amoi,Laws Lu,2005/08/02,注释
                    //				if (txtGOMO.Checked && txtGOMO.Value.Trim()!=string.Empty)
                    //				{
                    //					btnSave.Enabled = true;
                    //					//ApplicationRun.GetInfoForm().AddEx("$CS_CHECKSUCCESS");
                    //				}
                    //EndAmoi

                }

            }
            else
            {
                btnSave.Enabled = false;
            }

            //EndAmoi
        }

        private bool CheckGOMO()
        {
            Messages messages = new Messages();
            try
            {
                messages.AddMessages(GetProduct());
                if (messages.IsSuccess() && product.LastSimulation != null)
                {
                    btnSave.Enabled = true;
                    //ApplicationRun.GetInfoForm().AddEx("$CS_CHECKSUCCESS");
                    return true;
                }
                else
                {
                    if (messages.IsSuccess() && product.LastSimulation == null)
                    {
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, txtRunningCard.Caption + ": " + txtRunningCard.Value, new UserControl.Message(MessageType.Error, "$NoSimulation"), true);
                        txtRunningCard.Value = String.Empty;
                    }

                    return false;
                }
            }
            catch (Exception exp)
            {

                messages.Add(new UserControl.Message(exp));
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, messages, true);
                txtRunningCard.Value = String.Empty;

                return false;
            }
        }

        private void rdoGOOD_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rdoGOOD.Checked)
            {
                //Amoi,Laws Lu,2005/08/05,新增
                //Laws Lu,2005/08/15,修改	新增按钮和删除按钮在选择良品时应该为不可用
                ClearAfterSave();
                txtRunningCard.Enabled = true;

                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                //EndAmoi
            }
        }

        private void rdoNG_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rdoNG.Checked)
            {
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;

                //Amoi,Laws Lu,2005/08/05,新增
                ClearAfterSave();
                //EndAmoi
            }
        }

        //Amoi,Laws Lu,2005/08/03,注释
        //		private void txtGOMO_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        //		{
        //			if (e.KeyChar == '\r')
        //			{
        //				if (txtGOMO.Checked && txtGOMO.Value.Trim() != string.Empty) 
        //				{
        //					MOFacade moFacade = new MOFacade( this.DataProvider );
        //					object obj = moFacade.GetMO( this.txtGOMO.Value.Trim().ToUpper() );
        //					if (obj == null)
        //					{
        //						e.Handled = true;
        //						ApplicationRun.GetInfoForm().AddEx("$CS_MO_NOT_EXIST");
        //						txtGOMO.TextFocus(false, true);
        //                      //System.Windows.Forms.SendKeys.Send("+{TAB}");
        //					}
        //					else if (rdoNG.Checked)
        //					{
        //						SetErrorCodeGroupList(((MO)obj).ItemCode);
        //					}				
        //				}
        //			}
        //		}
        //EndAmoi

        private void SetErrorCodeGroupList(string item)
        {
            TSModelFacade tsFacade = new TSModelFacade(this.DataProvider);
            object[] errorCodeGroups = tsFacade.GetErrorCodeGroupByItemCode(item);
            cbxErrorGroup.Clear();
            if (errorCodeGroups != null)
            {
                for (int i = 0; i < errorCodeGroups.Length; i++)
                {
                    ErrorCodeGroupA errGroup = (ErrorCodeGroupA)errorCodeGroups[i];
                    cbxErrorGroup.AddItem(errGroup.ErrorCodeGroup, errGroup.ErrorCodeGroup);
                }
                //Amio,Laws Lu,2005/08/02,新增 默认选择第一条不良代码组
                if (errorCodeGroups.Length > 0)
                {
                    cbxErrorGroup.SelectedIndex = 0;
                }
                //EndAmoi				
            }
        }

        private object[] GetErrorInfor()
        {
            object[] obj = new object[grdError.Rows.Count];
            for (int i = 0; i < grdError.Rows.Count; i++)
            {
                TSErrorCode2Location tsinfo = new TSErrorCode2Location();
                tsinfo.ErrorCode = dtSMT.Rows[i][2].ToString();
                tsinfo.ErrorCodeGroup = dtSMT.Rows[i][1].ToString();
                tsinfo.ErrorLocation = dtSMT.Rows[i][3].ToString();
                tsinfo.AB = dtSMT.Rows[i][4].ToString();
                obj[i] = tsinfo;
            }
            return obj;
        }

        private void ClearAfterAdd()
        {
            txtErrorCode.Value = string.Empty;
            txtLocation.Value = string.Empty;

            if (rdoNG.Checked == true)
            {
                txtRunningCard.Enabled = false;

                cbxAB.SelectedIndex = 0;
            }

            txtErrorCode.TextFocus(false, true);
            //Remove UCLabel.SelectAll;
            //System.Windows.Forms.SendKeys.Send("+{TAB}");
            btnSave.Enabled = true;
        }

        private void ClearAfterSave()
        {
            if (txtGOMO.Checked != true)
            {
                txtGOMO.Checked = false;
            }
            txtRunningCard.Value = string.Empty;
            dtSMT.Clear();
            cbxErrorGroup.Clear();
            txtErrorCode.Value = string.Empty;
            txtLocation.Value = string.Empty;
            cbxAB.SelectedIndex = 0;
            txtMEMO.Value = string.Empty;
            btnSave.Enabled = false;
            txtRunningCard.TextFocus(false, true);
            //Remove UCLabel.SelectAll;
            //System.Windows.Forms.SendKeys.Send("+{TAB}");
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (dtSMT.Rows.Count < 1 || grdError.Selected.Rows.Count < 1)
            {
                UserControl.Messages msg = new UserControl.Messages();
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_PLEASE_SELECT_ERRORINFO"));
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtErrorCode.Caption + ": " + this.txtErrorCode.Value, msg, false);
            }
            else
            {
                grdError.DeleteSelectedRows(false);

                dtSMT.AcceptChanges();
            }

            if (grdError.Rows.Count < 1)
            {
                //Laws Lu,2005/08/25,新增	处理最后一条不良信息被删除，应该允许用户修改产品序列号
                ClearAfterSave();
                txtRunningCard.Enabled = true;
                txtRunningCard.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
                //System.Windows.Forms.SendKeys.Send("+{TAB}");
            }
        }

        private Messages CheckErrorCodes()
        {
            Messages megs = new Messages();
            megs.Add(new UserControl.Message(MessageType.Debug, "$CS_Debug" + " CheckErrorCodes()"));
            if (grdError.Rows.Count == 0)
                megs.Add(new UserControl.Message(MessageType.Error, "$CS_Please_Input_ErrorInfo"));
            return megs;
        }

        private void txtGOMO_CheckBoxCheckedChanged(object sender, System.EventArgs e)
        {
            if (!txtGOMO.Checked)
            {
                cbxErrorGroup.Clear();
            }
        }

        private void FCollectionSMTGDNG_Closed(object sender, System.EventArgs e)
        {
            if (_domainDataProvider != null)
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();
        }

        private void FCollectionSMTGDNG_Activated(object sender, System.EventArgs e)
        {
            //2005/08/16,新增	新增和删除按钮在默认为良品的情况下状态为Enable=false
            if (rdoGOOD.Checked == true)
            {
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
            }

            txtRunningCard.TextFocus(false, true);
            //Remove UCLabel.SelectAll;
            //System.Windows.Forms.SendKeys.Send("+{TAB}");

            this._FunctionName = this.Text;
        }

        private void txtErrorCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                UserControl.Messages msg = new UserControl.Messages();

                if (txtErrorCode.Value.Trim() == string.Empty)
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_ErrorInfo"), false);
                    txtErrorCode.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    //System.Windows.Forms.SendKeys.Send("+{TAB}");
                    return;
                }

                TSModelFacade tsModelFacade = new TSModelFacade(this.DataProvider);
                object errorCodeGroup2ErrorCode = tsModelFacade.GetErrorCodeGroup2ErrorCodeByecCode(txtErrorCode.Value.Trim().ToUpper());
                if (errorCodeGroup2ErrorCode == null)
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, txtErrorCode.Caption + ": " + txtErrorCode.Value, new UserControl.Message(MessageType.Error, "$CS_ErrorCode_NoGroup"), false);
                    txtErrorCode.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    //System.Windows.Forms.SendKeys.Send("+{TAB}");
                    return;
                }

                string errorCodeGroupCode = ((ErrorCodeGroup2ErrorCode)errorCodeGroup2ErrorCode).ErrorCodeGroup;
                cbxErrorGroup.SetSelectItemText(errorCodeGroupCode);

                if (cbxErrorGroup.SelectedIndex < 0
                    || !tsModelFacade.IsErrorCodeInGroup(txtErrorCode.Value.Trim().ToUpper(), cbxErrorGroup.SelectedItemValue.ToString()))
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, txtErrorCode.Caption + ": " + txtErrorCode.Value, new UserControl.Message(MessageType.Error, "$CS_ErrorCode_NoGroup"), false);
                    txtErrorCode.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    //System.Windows.Forms.SendKeys.Send("+{TAB}");
                    return;
                }

                cbxAB.SelectedIndex = 1;

                txtLocation.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
                //System.Windows.Forms.SendKeys.Send("+{TAB}");
            }
        }

        private void txtGOMO_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                txtRunningCard.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
                //System.Windows.Forms.SendKeys.Send("+{TAB}");
            }
        }

        private void txtLocation_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                cbxAB.Focus();
            }
        }

        private void cbxAB_ComboBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.btnAdd_Click(this.btnAdd, null);
            }
        }

        private void FCollectionSMTGDNG_Load(object sender, EventArgs e)
        {
            //this.InitPageLanguage();
            //this.InitGridLanguage(this.grdError);
        }
    }
}
