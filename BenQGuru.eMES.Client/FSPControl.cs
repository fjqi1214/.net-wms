using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Client.Service;

using Infragistics.Win.UltraWinGrid;

namespace BenQGuru.eMES.Client
{
    /// <summary>
    /// FSPProcess 的摘要说明。
    /// </summary>
    public class FSPControl : BaseForm
    {
        private DataTable dtSP = new DataTable();
        private DataTable dtSPAlert = new DataTable();

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private UserControl.UCButton btnAgitate;
        private System.Windows.Forms.GroupBox solderPastNowData;
        private UserControl.UCButton btnUnveil;
        private UserControl.UCLabelEdit txtMemo;
        private UserControl.UCLabelEdit txtStatus;
        private UserControl.UCLabelEdit txtSolderPasteID;
        private UserControl.UCButton btnConfrim;
        private UserControl.UCLabelEdit txtDestinationLine;
        private UserControl.UCLabelEdit txtDestinationMo;
        private UserControl.UCLabelEdit txtOPUser;
        private System.Windows.Forms.GroupBox currentOperation;
        private System.Windows.Forms.RadioButton radOpen;
        private System.Windows.Forms.RadioButton radReflow;
        private System.Windows.Forms.RadioButton radTransferMo;
        private System.Windows.Forms.RadioButton radReturn;
        private System.Windows.Forms.RadioButton radUsedUp;
        private System.Windows.Forms.RadioButton radUnavial;
        private System.Windows.Forms.RadioButton radUnveil;
        private System.Windows.Forms.RadioButton radAgitate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelOverBothTime;
        private UserControl.UCLabelEdit txtItemCode;
        private System.Windows.Forms.Label label2;
        private UserControl.UCLabelEdit txtMocode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label red;
        private System.Windows.Forms.Label lblColorDesc;
        private UserControl.UCButton btnExit;
        private UserControl.UCButton btnSearch;
        private System.Windows.Forms.Label labelPercent;
        private UserControl.UCLabelEdit txtLineCode;
        private UserControl.UCLabelEdit txtAlertPercent;
        private System.Windows.Forms.Label labelMinute;
        private UserControl.UCLabelEdit txtRefreshRate;
        private System.Windows.Forms.Label labelAlarmCondition;
        private System.Windows.Forms.Label labelBackTime;
        private System.Windows.Forms.Panel panel3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
        private System.ComponentModel.IContainer components;
        private UserControl.UCLabelEdit txtDestinationItem;
        private UserControl.UCLabelEdit txtSPCode;


        SolderPasteFacade _facade = null;


        public FSPControl()
        {
            //
            // Windows 窗体设计器支持所必需的
            //

            InitialForm();

            InitializeComponent();



            UserControl.UIStyleBuilder.FormUI(this);
            //UserControl.UIStyleBuilder.GridUI(ultraGridMain);

            try
            {
                _facade = (SolderPasteFacade)Activator.CreateInstance(typeof(SolderPasteFacade), new object[] { DataProvider });
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));

                _facade = new SolderPasteFacade(DataProvider);

            }

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }


        private IDomainDataProvider _domainDataProvider = Service.ApplicationService.Current().DataProvider;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSPControl));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDestinationItem = new UserControl.UCLabelEdit();
            this.currentOperation = new System.Windows.Forms.GroupBox();
            this.radAgitate = new System.Windows.Forms.RadioButton();
            this.radUnveil = new System.Windows.Forms.RadioButton();
            this.radUnavial = new System.Windows.Forms.RadioButton();
            this.radUsedUp = new System.Windows.Forms.RadioButton();
            this.radReturn = new System.Windows.Forms.RadioButton();
            this.radTransferMo = new System.Windows.Forms.RadioButton();
            this.radReflow = new System.Windows.Forms.RadioButton();
            this.radOpen = new System.Windows.Forms.RadioButton();
            this.btnUnveil = new UserControl.UCButton();
            this.btnAgitate = new UserControl.UCButton();
            this.txtOPUser = new UserControl.UCLabelEdit();
            this.txtDestinationLine = new UserControl.UCLabelEdit();
            this.txtDestinationMo = new UserControl.UCLabelEdit();
            this.btnConfrim = new UserControl.UCButton();
            this.txtMemo = new UserControl.UCLabelEdit();
            this.txtStatus = new UserControl.UCLabelEdit();
            this.txtSolderPasteID = new UserControl.UCLabelEdit();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.solderPastNowData = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtSPCode = new UserControl.UCLabelEdit();
            this.labelOverBothTime = new System.Windows.Forms.Label();
            this.txtItemCode = new UserControl.UCLabelEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMocode = new UserControl.UCLabelEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.red = new System.Windows.Forms.Label();
            this.lblColorDesc = new System.Windows.Forms.Label();
            this.btnExit = new UserControl.UCButton();
            this.btnSearch = new UserControl.UCButton();
            this.labelPercent = new System.Windows.Forms.Label();
            this.txtLineCode = new UserControl.UCLabelEdit();
            this.txtAlertPercent = new UserControl.UCLabelEdit();
            this.labelMinute = new System.Windows.Forms.Label();
            this.txtRefreshRate = new UserControl.UCLabelEdit();
            this.labelAlarmCondition = new System.Windows.Forms.Label();
            this.labelBackTime = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.currentOperation.SuspendLayout();
            this.solderPastNowData.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtDestinationItem);
            this.panel1.Controls.Add(this.currentOperation);
            this.panel1.Controls.Add(this.txtOPUser);
            this.panel1.Controls.Add(this.txtDestinationLine);
            this.panel1.Controls.Add(this.txtDestinationMo);
            this.panel1.Controls.Add(this.btnConfrim);
            this.panel1.Controls.Add(this.txtMemo);
            this.panel1.Controls.Add(this.txtStatus);
            this.panel1.Controls.Add(this.txtSolderPasteID);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(925, 149);
            this.panel1.TabIndex = 26;
            // 
            // txtDestinationItem
            // 
            this.txtDestinationItem.AllowEditOnlyChecked = true;
            this.txtDestinationItem.AutoSelectAll = false;
            this.txtDestinationItem.AutoUpper = true;
            this.txtDestinationItem.Caption = "产品代码";
            this.txtDestinationItem.Checked = false;
            this.txtDestinationItem.EditType = UserControl.EditTypes.String;
            this.txtDestinationItem.Enabled = false;
            this.txtDestinationItem.Location = new System.Drawing.Point(12, 125);
            this.txtDestinationItem.MaxLength = 40;
            this.txtDestinationItem.Multiline = false;
            this.txtDestinationItem.Name = "txtDestinationItem";
            this.txtDestinationItem.PasswordChar = '\0';
            this.txtDestinationItem.ReadOnly = true;
            this.txtDestinationItem.ShowCheckBox = false;
            this.txtDestinationItem.Size = new System.Drawing.Size(194, 24);
            this.txtDestinationItem.TabIndex = 40;
            this.txtDestinationItem.TabNext = true;
            this.txtDestinationItem.TabStop = false;
            this.txtDestinationItem.Value = "";
            this.txtDestinationItem.Visible = false;
            this.txtDestinationItem.WidthType = UserControl.WidthTypes.Normal;
            this.txtDestinationItem.XAlign = 73;
            // 
            // currentOperation
            // 
            this.currentOperation.Controls.Add(this.radAgitate);
            this.currentOperation.Controls.Add(this.radUnveil);
            this.currentOperation.Controls.Add(this.radUnavial);
            this.currentOperation.Controls.Add(this.radUsedUp);
            this.currentOperation.Controls.Add(this.radReturn);
            this.currentOperation.Controls.Add(this.radTransferMo);
            this.currentOperation.Controls.Add(this.radReflow);
            this.currentOperation.Controls.Add(this.radOpen);
            this.currentOperation.Controls.Add(this.btnUnveil);
            this.currentOperation.Controls.Add(this.btnAgitate);
            this.currentOperation.Dock = System.Windows.Forms.DockStyle.Top;
            this.currentOperation.Location = new System.Drawing.Point(0, 0);
            this.currentOperation.Name = "currentOperation";
            this.currentOperation.Size = new System.Drawing.Size(925, 59);
            this.currentOperation.TabIndex = 39;
            this.currentOperation.TabStop = false;
            this.currentOperation.Text = "当前操作";
            // 
            // radAgitate
            // 
            this.radAgitate.Location = new System.Drawing.Point(171, 22);
            this.radAgitate.Name = "radAgitate";
            this.radAgitate.Size = new System.Drawing.Size(73, 24);
            this.radAgitate.TabIndex = 0;
            this.radAgitate.Tag = "ProcessType";
            this.radAgitate.Text = "搅拌";
            this.radAgitate.CheckedChanged += new System.EventHandler(this.radGroup_Click);
            // 
            // radUnveil
            // 
            this.radUnveil.Location = new System.Drawing.Point(250, 22);
            this.radUnveil.Name = "radUnveil";
            this.radUnveil.Size = new System.Drawing.Size(74, 24);
            this.radUnveil.TabIndex = 0;
            this.radUnveil.Tag = "ProcessType";
            this.radUnveil.Text = "开封";
            this.radUnveil.CheckedChanged += new System.EventHandler(this.radGroup_Click);
            // 
            // radUnavial
            // 
            this.radUnavial.Location = new System.Drawing.Point(490, 22);
            this.radUnavial.Name = "radUnavial";
            this.radUnavial.Size = new System.Drawing.Size(74, 24);
            this.radUnavial.TabIndex = 0;
            this.radUnavial.Tag = "ProcessType";
            this.radUnavial.Text = "报废";
            this.radUnavial.CheckedChanged += new System.EventHandler(this.radGroup_Click);
            // 
            // radUsedUp
            // 
            this.radUsedUp.Location = new System.Drawing.Point(409, 22);
            this.radUsedUp.Name = "radUsedUp";
            this.radUsedUp.Size = new System.Drawing.Size(73, 24);
            this.radUsedUp.TabIndex = 0;
            this.radUsedUp.Tag = "ProcessType";
            this.radUsedUp.Text = "用完";
            this.radUsedUp.CheckedChanged += new System.EventHandler(this.radGroup_Click);
            // 
            // radReturn
            // 
            this.radReturn.Checked = true;
            this.radReturn.Location = new System.Drawing.Point(12, 22);
            this.radReturn.Name = "radReturn";
            this.radReturn.Size = new System.Drawing.Size(74, 24);
            this.radReturn.TabIndex = 0;
            this.radReturn.TabStop = true;
            this.radReturn.Tag = "ProcessType";
            this.radReturn.Text = "回温";
            this.radReturn.CheckedChanged += new System.EventHandler(this.radGroup_Click);
            // 
            // radTransferMo
            // 
            this.radTransferMo.Location = new System.Drawing.Point(570, 22);
            this.radTransferMo.Name = "radTransferMo";
            this.radTransferMo.Size = new System.Drawing.Size(74, 24);
            this.radTransferMo.TabIndex = 0;
            this.radTransferMo.Tag = "ProcessType";
            this.radTransferMo.Text = "转换工单";
            this.radTransferMo.CheckedChanged += new System.EventHandler(this.radGroup_Click);
            // 
            // radReflow
            // 
            this.radReflow.Location = new System.Drawing.Point(330, 22);
            this.radReflow.Name = "radReflow";
            this.radReflow.Size = new System.Drawing.Size(73, 24);
            this.radReflow.TabIndex = 0;
            this.radReflow.Tag = "ProcessType";
            this.radReflow.Text = "回存";
            this.radReflow.CheckedChanged += new System.EventHandler(this.radGroup_Click);
            // 
            // radOpen
            // 
            this.radOpen.Location = new System.Drawing.Point(92, 22);
            this.radOpen.Name = "radOpen";
            this.radOpen.Size = new System.Drawing.Size(73, 24);
            this.radOpen.TabIndex = 0;
            this.radOpen.Tag = "ProcessType";
            this.radOpen.Text = "领用";
            this.radOpen.CheckedChanged += new System.EventHandler(this.radGroup_Click);
            // 
            // btnUnveil
            // 
            this.btnUnveil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUnveil.BackColor = System.Drawing.SystemColors.Control;
            this.btnUnveil.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUnveil.BackgroundImage")));
            this.btnUnveil.ButtonType = UserControl.ButtonTypes.None;
            this.btnUnveil.Caption = "开封";
            this.btnUnveil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnveil.Location = new System.Drawing.Point(676, 22);
            this.btnUnveil.Name = "btnUnveil";
            this.btnUnveil.Size = new System.Drawing.Size(88, 22);
            this.btnUnveil.TabIndex = 31;
            this.btnUnveil.TabStop = false;
            this.btnUnveil.Visible = false;
            this.btnUnveil.Click += new System.EventHandler(this.btnUnveil_Click);
            // 
            // btnAgitate
            // 
            this.btnAgitate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgitate.BackColor = System.Drawing.SystemColors.Control;
            this.btnAgitate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAgitate.BackgroundImage")));
            this.btnAgitate.ButtonType = UserControl.ButtonTypes.None;
            this.btnAgitate.Caption = "搅拌";
            this.btnAgitate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgitate.Location = new System.Drawing.Point(770, 22);
            this.btnAgitate.Name = "btnAgitate";
            this.btnAgitate.Size = new System.Drawing.Size(88, 22);
            this.btnAgitate.TabIndex = 30;
            this.btnAgitate.TabStop = false;
            this.btnAgitate.Visible = false;
            this.btnAgitate.Click += new System.EventHandler(this.btnAgitate_Click);
            // 
            // txtOPUser
            // 
            this.txtOPUser.AllowEditOnlyChecked = true;
            this.txtOPUser.AutoSelectAll = false;
            this.txtOPUser.AutoUpper = true;
            this.txtOPUser.Caption = "操作人员";
            this.txtOPUser.Checked = false;
            this.txtOPUser.EditType = UserControl.EditTypes.String;
            this.txtOPUser.Location = new System.Drawing.Point(12, 67);
            this.txtOPUser.MaxLength = 40;
            this.txtOPUser.Multiline = false;
            this.txtOPUser.Name = "txtOPUser";
            this.txtOPUser.PasswordChar = '\0';
            this.txtOPUser.ReadOnly = false;
            this.txtOPUser.ShowCheckBox = false;
            this.txtOPUser.Size = new System.Drawing.Size(194, 24);
            this.txtOPUser.TabIndex = 1;
            this.txtOPUser.TabNext = true;
            this.txtOPUser.Value = "";
            this.txtOPUser.WidthType = UserControl.WidthTypes.Normal;
            this.txtOPUser.XAlign = 73;
            this.txtOPUser.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOPUser_TxtboxKeyPress);
            // 
            // txtDestinationLine
            // 
            this.txtDestinationLine.AllowEditOnlyChecked = true;
            this.txtDestinationLine.AutoSelectAll = false;
            this.txtDestinationLine.AutoUpper = true;
            this.txtDestinationLine.Caption = "产线代码";
            this.txtDestinationLine.Checked = false;
            this.txtDestinationLine.EditType = UserControl.EditTypes.String;
            this.txtDestinationLine.Location = new System.Drawing.Point(430, 65);
            this.txtDestinationLine.MaxLength = 40;
            this.txtDestinationLine.Multiline = false;
            this.txtDestinationLine.Name = "txtDestinationLine";
            this.txtDestinationLine.PasswordChar = '\0';
            this.txtDestinationLine.ReadOnly = false;
            this.txtDestinationLine.ShowCheckBox = false;
            this.txtDestinationLine.Size = new System.Drawing.Size(194, 24);
            this.txtDestinationLine.TabIndex = 5;
            this.txtDestinationLine.TabNext = false;
            this.txtDestinationLine.Value = "";
            this.txtDestinationLine.WidthType = UserControl.WidthTypes.Normal;
            this.txtDestinationLine.XAlign = 491;
            this.txtDestinationLine.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDestinationLine_TxtboxKeyPress);
            // 
            // txtDestinationMo
            // 
            this.txtDestinationMo.AllowEditOnlyChecked = true;
            this.txtDestinationMo.AutoSelectAll = false;
            this.txtDestinationMo.AutoUpper = true;
            this.txtDestinationMo.Caption = "工单代码";
            this.txtDestinationMo.Checked = false;
            this.txtDestinationMo.EditType = UserControl.EditTypes.String;
            this.txtDestinationMo.Location = new System.Drawing.Point(221, 65);
            this.txtDestinationMo.MaxLength = 40;
            this.txtDestinationMo.Multiline = false;
            this.txtDestinationMo.Name = "txtDestinationMo";
            this.txtDestinationMo.PasswordChar = '\0';
            this.txtDestinationMo.ReadOnly = false;
            this.txtDestinationMo.ShowCheckBox = false;
            this.txtDestinationMo.Size = new System.Drawing.Size(194, 24);
            this.txtDestinationMo.TabIndex = 3;
            this.txtDestinationMo.TabNext = false;
            this.txtDestinationMo.Value = "";
            this.txtDestinationMo.WidthType = UserControl.WidthTypes.Normal;
            this.txtDestinationMo.XAlign = 282;
            this.txtDestinationMo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDestinationMo_TxtboxKeyPress);
            // 
            // btnConfrim
            // 
            this.btnConfrim.BackColor = System.Drawing.SystemColors.Control;
            this.btnConfrim.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConfrim.BackgroundImage")));
            this.btnConfrim.ButtonType = UserControl.ButtonTypes.None;
            this.btnConfrim.Caption = "确定";
            this.btnConfrim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfrim.Location = new System.Drawing.Point(637, 97);
            this.btnConfrim.Name = "btnConfrim";
            this.btnConfrim.Size = new System.Drawing.Size(88, 22);
            this.btnConfrim.TabIndex = 35;
            this.btnConfrim.TabStop = false;
            this.btnConfrim.Click += new System.EventHandler(this.btnConfrim_Click);
            // 
            // txtMemo
            // 
            this.txtMemo.AllowEditOnlyChecked = true;
            this.txtMemo.AutoSelectAll = false;
            this.txtMemo.AutoUpper = true;
            this.txtMemo.Caption = "备注";
            this.txtMemo.Checked = false;
            this.txtMemo.EditType = UserControl.EditTypes.String;
            this.txtMemo.Location = new System.Drawing.Point(454, 97);
            this.txtMemo.MaxLength = 40;
            this.txtMemo.Multiline = false;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.PasswordChar = '\0';
            this.txtMemo.ReadOnly = true;
            this.txtMemo.ShowCheckBox = false;
            this.txtMemo.Size = new System.Drawing.Size(170, 24);
            this.txtMemo.TabIndex = 11;
            this.txtMemo.TabNext = false;
            this.txtMemo.TabStop = false;
            this.txtMemo.Value = "";
            this.txtMemo.WidthType = UserControl.WidthTypes.Normal;
            this.txtMemo.XAlign = 491;
            // 
            // txtStatus
            // 
            this.txtStatus.AllowEditOnlyChecked = true;
            this.txtStatus.AutoSelectAll = false;
            this.txtStatus.AutoUpper = true;
            this.txtStatus.Caption = "状态";
            this.txtStatus.Checked = false;
            this.txtStatus.EditType = UserControl.EditTypes.String;
            this.txtStatus.Location = new System.Drawing.Point(245, 97);
            this.txtStatus.MaxLength = 40;
            this.txtStatus.Multiline = false;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.PasswordChar = '\0';
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ShowCheckBox = false;
            this.txtStatus.Size = new System.Drawing.Size(170, 24);
            this.txtStatus.TabIndex = 9;
            this.txtStatus.TabNext = false;
            this.txtStatus.TabStop = false;
            this.txtStatus.Value = "";
            this.txtStatus.WidthType = UserControl.WidthTypes.Normal;
            this.txtStatus.XAlign = 282;
            // 
            // txtSolderPasteID
            // 
            this.txtSolderPasteID.AllowEditOnlyChecked = true;
            this.txtSolderPasteID.AutoSelectAll = false;
            this.txtSolderPasteID.AutoUpper = true;
            this.txtSolderPasteID.Caption = "锡膏ID";
            this.txtSolderPasteID.Checked = false;
            this.txtSolderPasteID.EditType = UserControl.EditTypes.String;
            this.txtSolderPasteID.Location = new System.Drawing.Point(24, 97);
            this.txtSolderPasteID.MaxLength = 40;
            this.txtSolderPasteID.Multiline = false;
            this.txtSolderPasteID.Name = "txtSolderPasteID";
            this.txtSolderPasteID.PasswordChar = '\0';
            this.txtSolderPasteID.ReadOnly = false;
            this.txtSolderPasteID.ShowCheckBox = false;
            this.txtSolderPasteID.Size = new System.Drawing.Size(182, 24);
            this.txtSolderPasteID.TabIndex = 7;
            this.txtSolderPasteID.TabNext = true;
            this.txtSolderPasteID.Value = "";
            this.txtSolderPasteID.WidthType = UserControl.WidthTypes.Normal;
            this.txtSolderPasteID.XAlign = 73;
            this.txtSolderPasteID.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSolderPasteID_TxtboxKeyPress);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // solderPastNowData
            // 
            this.solderPastNowData.Controls.Add(this.panel3);
            this.solderPastNowData.Controls.Add(this.panel2);
            this.solderPastNowData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.solderPastNowData.Location = new System.Drawing.Point(0, 149);
            this.solderPastNowData.Name = "solderPastNowData";
            this.solderPastNowData.Size = new System.Drawing.Size(925, 411);
            this.solderPastNowData.TabIndex = 30;
            this.solderPastNowData.TabStop = false;
            this.solderPastNowData.Text = "锡膏实时资料";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ultraGridMain);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 110);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(919, 298);
            this.panel3.TabIndex = 31;
            // 
            // ultraGridMain
            // 
            this.ultraGridMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridMain.DisplayLayout.RowConnectorColor = System.Drawing.Color.Gainsboro;
            this.ultraGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMain.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridMain.Location = new System.Drawing.Point(0, 0);
            this.ultraGridMain.Name = "ultraGridMain";
            this.ultraGridMain.Size = new System.Drawing.Size(919, 298);
            this.ultraGridMain.TabIndex = 30;
            this.ultraGridMain.TabStop = false;
            this.ultraGridMain.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMain_InitializeLayout);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtSPCode);
            this.panel2.Controls.Add(this.labelOverBothTime);
            this.panel2.Controls.Add(this.txtItemCode);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtMocode);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.red);
            this.panel2.Controls.Add(this.lblColorDesc);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.labelPercent);
            this.panel2.Controls.Add(this.txtLineCode);
            this.panel2.Controls.Add(this.txtAlertPercent);
            this.panel2.Controls.Add(this.labelMinute);
            this.panel2.Controls.Add(this.txtRefreshRate);
            this.panel2.Controls.Add(this.labelAlarmCondition);
            this.panel2.Controls.Add(this.labelBackTime);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(919, 93);
            this.panel2.TabIndex = 30;
            // 
            // txtSPCode
            // 
            this.txtSPCode.AllowEditOnlyChecked = true;
            this.txtSPCode.AutoSelectAll = false;
            this.txtSPCode.AutoUpper = true;
            this.txtSPCode.Caption = "锡膏编号";
            this.txtSPCode.Checked = false;
            this.txtSPCode.EditType = UserControl.EditTypes.String;
            this.txtSPCode.Location = new System.Drawing.Point(427, 31);
            this.txtSPCode.MaxLength = 40;
            this.txtSPCode.Multiline = false;
            this.txtSPCode.Name = "txtSPCode";
            this.txtSPCode.PasswordChar = '\0';
            this.txtSPCode.ReadOnly = false;
            this.txtSPCode.ShowCheckBox = false;
            this.txtSPCode.Size = new System.Drawing.Size(194, 24);
            this.txtSPCode.TabIndex = 53;
            this.txtSPCode.TabNext = true;
            this.txtSPCode.TabStop = false;
            this.txtSPCode.Value = "";
            this.txtSPCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtSPCode.XAlign = 488;
            // 
            // labelOverBothTime
            // 
            this.labelOverBothTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOverBothTime.ForeColor = System.Drawing.Color.Red;
            this.labelOverBothTime.Location = new System.Drawing.Point(145, 63);
            this.labelOverBothTime.Name = "labelOverBothTime";
            this.labelOverBothTime.Size = new System.Drawing.Size(155, 22);
            this.labelOverBothTime.TabIndex = 46;
            this.labelOverBothTime.Text = "超出未开封或者开封时长";
            this.labelOverBothTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtItemCode
            // 
            this.txtItemCode.AllowEditOnlyChecked = true;
            this.txtItemCode.AutoSelectAll = false;
            this.txtItemCode.AutoUpper = true;
            this.txtItemCode.Caption = "产品代码";
            this.txtItemCode.Checked = false;
            this.txtItemCode.EditType = UserControl.EditTypes.String;
            this.txtItemCode.Enabled = false;
            this.txtItemCode.Location = new System.Drawing.Point(218, 3);
            this.txtItemCode.MaxLength = 40;
            this.txtItemCode.Multiline = false;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.PasswordChar = '\0';
            this.txtItemCode.ReadOnly = true;
            this.txtItemCode.ShowCheckBox = false;
            this.txtItemCode.Size = new System.Drawing.Size(194, 24);
            this.txtItemCode.TabIndex = 39;
            this.txtItemCode.TabNext = true;
            this.txtItemCode.TabStop = false;
            this.txtItemCode.Value = "";
            this.txtItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtItemCode.XAlign = 279;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Purple;
            this.label2.Location = new System.Drawing.Point(524, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 22);
            this.label2.TabIndex = 45;
            // 
            // txtMocode
            // 
            this.txtMocode.AllowEditOnlyChecked = true;
            this.txtMocode.AutoSelectAll = false;
            this.txtMocode.AutoUpper = true;
            this.txtMocode.Caption = "工单代码";
            this.txtMocode.Checked = false;
            this.txtMocode.EditType = UserControl.EditTypes.String;
            this.txtMocode.Location = new System.Drawing.Point(9, 2);
            this.txtMocode.MaxLength = 40;
            this.txtMocode.Multiline = false;
            this.txtMocode.Name = "txtMocode";
            this.txtMocode.PasswordChar = '\0';
            this.txtMocode.ReadOnly = false;
            this.txtMocode.ShowCheckBox = false;
            this.txtMocode.Size = new System.Drawing.Size(194, 24);
            this.txtMocode.TabIndex = 38;
            this.txtMocode.TabNext = true;
            this.txtMocode.TabStop = false;
            this.txtMocode.Value = "";
            this.txtMocode.WidthType = UserControl.WidthTypes.Normal;
            this.txtMocode.XAlign = 70;
            this.txtMocode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMocode_TxtboxKeyPress);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(319, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 22);
            this.label1.TabIndex = 44;
            // 
            // red
            // 
            this.red.BackColor = System.Drawing.Color.Red;
            this.red.Location = new System.Drawing.Point(85, 63);
            this.red.Name = "red";
            this.red.Size = new System.Drawing.Size(54, 22);
            this.red.TabIndex = 43;
            // 
            // lblColorDesc
            // 
            this.lblColorDesc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblColorDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblColorDesc.Location = new System.Drawing.Point(7, 68);
            this.lblColorDesc.Name = "lblColorDesc";
            this.lblColorDesc.Size = new System.Drawing.Size(72, 22);
            this.lblColorDesc.TabIndex = 42;
            this.lblColorDesc.Text = "颜色说明";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(634, 32);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 48;
            this.btnExit.TabStop = false;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.ButtonType = UserControl.ButtonTypes.Query;
            this.btnSearch.Caption = "查询";
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Location = new System.Drawing.Point(634, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(88, 22);
            this.btnSearch.TabIndex = 41;
            this.btnSearch.TabStop = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labelPercent
            // 
            this.labelPercent.Location = new System.Drawing.Point(335, 30);
            this.labelPercent.Name = "labelPercent";
            this.labelPercent.Size = new System.Drawing.Size(20, 22);
            this.labelPercent.TabIndex = 52;
            this.labelPercent.Text = "%";
            this.labelPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLineCode
            // 
            this.txtLineCode.AllowEditOnlyChecked = true;
            this.txtLineCode.AutoSelectAll = false;
            this.txtLineCode.AutoUpper = true;
            this.txtLineCode.Caption = "产线代码";
            this.txtLineCode.Checked = false;
            this.txtLineCode.EditType = UserControl.EditTypes.String;
            this.txtLineCode.Location = new System.Drawing.Point(427, 3);
            this.txtLineCode.MaxLength = 40;
            this.txtLineCode.Multiline = false;
            this.txtLineCode.Name = "txtLineCode";
            this.txtLineCode.PasswordChar = '\0';
            this.txtLineCode.ReadOnly = false;
            this.txtLineCode.ShowCheckBox = false;
            this.txtLineCode.Size = new System.Drawing.Size(194, 24);
            this.txtLineCode.TabIndex = 40;
            this.txtLineCode.TabNext = true;
            this.txtLineCode.TabStop = false;
            this.txtLineCode.Value = "";
            this.txtLineCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtLineCode.XAlign = 488;
            // 
            // txtAlertPercent
            // 
            this.txtAlertPercent.AllowEditOnlyChecked = true;
            this.txtAlertPercent.AutoSelectAll = false;
            this.txtAlertPercent.AutoUpper = true;
            this.txtAlertPercent.Caption = "预警标准";
            this.txtAlertPercent.Checked = false;
            this.txtAlertPercent.EditType = UserControl.EditTypes.Integer;
            this.txtAlertPercent.Location = new System.Drawing.Point(218, 32);
            this.txtAlertPercent.MaxLength = 40;
            this.txtAlertPercent.Multiline = false;
            this.txtAlertPercent.Name = "txtAlertPercent";
            this.txtAlertPercent.PasswordChar = '\0';
            this.txtAlertPercent.ReadOnly = false;
            this.txtAlertPercent.ShowCheckBox = false;
            this.txtAlertPercent.Size = new System.Drawing.Size(111, 24);
            this.txtAlertPercent.TabIndex = 51;
            this.txtAlertPercent.TabNext = true;
            this.txtAlertPercent.TabStop = false;
            this.txtAlertPercent.Value = "60";
            this.txtAlertPercent.WidthType = UserControl.WidthTypes.Tiny;
            this.txtAlertPercent.XAlign = 279;
            // 
            // labelMinute
            // 
            this.labelMinute.Location = new System.Drawing.Point(124, 32);
            this.labelMinute.Name = "labelMinute";
            this.labelMinute.Size = new System.Drawing.Size(40, 22);
            this.labelMinute.TabIndex = 50;
            this.labelMinute.Text = "分钟";
            this.labelMinute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRefreshRate
            // 
            this.txtRefreshRate.AllowEditOnlyChecked = true;
            this.txtRefreshRate.AutoSelectAll = false;
            this.txtRefreshRate.AutoUpper = true;
            this.txtRefreshRate.Caption = "刷新频率";
            this.txtRefreshRate.Checked = false;
            this.txtRefreshRate.EditType = UserControl.EditTypes.Integer;
            this.txtRefreshRate.Location = new System.Drawing.Point(7, 32);
            this.txtRefreshRate.MaxLength = 40;
            this.txtRefreshRate.Multiline = false;
            this.txtRefreshRate.Name = "txtRefreshRate";
            this.txtRefreshRate.PasswordChar = '\0';
            this.txtRefreshRate.ReadOnly = false;
            this.txtRefreshRate.ShowCheckBox = false;
            this.txtRefreshRate.Size = new System.Drawing.Size(111, 24);
            this.txtRefreshRate.TabIndex = 37;
            this.txtRefreshRate.TabNext = true;
            this.txtRefreshRate.TabStop = false;
            this.txtRefreshRate.Value = "30";
            this.txtRefreshRate.WidthType = UserControl.WidthTypes.Tiny;
            this.txtRefreshRate.XAlign = 68;
            // 
            // labelAlarmCondition
            // 
            this.labelAlarmCondition.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelAlarmCondition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.labelAlarmCondition.Location = new System.Drawing.Point(583, 63);
            this.labelAlarmCondition.Name = "labelAlarmCondition";
            this.labelAlarmCondition.Size = new System.Drawing.Size(243, 22);
            this.labelAlarmCondition.TabIndex = 49;
            this.labelAlarmCondition.Text = "达到开封或未开封时长的预警条件";
            this.labelAlarmCondition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelBackTime
            // 
            this.labelBackTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelBackTime.ForeColor = System.Drawing.Color.Blue;
            this.labelBackTime.Location = new System.Drawing.Point(376, 63);
            this.labelBackTime.Name = "labelBackTime";
            this.labelBackTime.Size = new System.Drawing.Size(125, 22);
            this.labelBackTime.TabIndex = 47;
            this.labelBackTime.Text = "达到或超过回温时长";
            this.labelBackTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FSPControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(925, 560);
            this.Controls.Add(this.solderPastNowData);
            this.Controls.Add(this.panel1);
            this.Name = "FSPControl";
            this.Text = "锡膏作业";
            this.Load += new System.EventHandler(this.FSPControl_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FSPControl_Closing);
            this.panel1.ResumeLayout(false);
            this.currentOperation.ResumeLayout(false);
            this.solderPastNowData.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private void InitialForm()
        {

            dtSP.Clear();

            #region Initial Grid Column

            dtSP.Columns.Add("SPID", typeof(string)).ReadOnly = true;
            dtSP.Columns.Add("MO", typeof(string)).ReadOnly = true;
            dtSP.Columns.Add("SSCODE", typeof(string)).ReadOnly = true;
            dtSP.Columns.Add("SPTYPE", typeof(string)).ReadOnly = true;
            dtSP.Columns.Add("SPITEMCODE", typeof(string)).ReadOnly = true;
            dtSP.Columns.Add("STATUS", typeof(string));
            dtSP.Columns.Add("RETURNTIME", typeof(string)).ReadOnly = true;
            dtSP.Columns.Add("RETURNTIMESPAN", typeof(string)).ReadOnly = true;
            dtSP.Columns.Add("RETURNCOUNTTIME", typeof(string)).ReadOnly = true;
            dtSP.Columns.Add("VEILTIMESPAN", typeof(string)).ReadOnly = true;
            dtSP.Columns.Add("VEILCOUNTTIME", typeof(string)).ReadOnly = true;
            dtSP.Columns.Add("UNVEILTIME", typeof(string)).ReadOnly = true;
            dtSP.Columns.Add("UNVEILTIMESPAN", typeof(string)).ReadOnly = true;
            dtSP.Columns.Add("UNVEILCONTTIME", typeof(string)).ReadOnly = true;
            dtSP.Columns.Add("AGITATEDATE", typeof(string)).ReadOnly = true;

            dtSP.Columns.Add("MEMO", typeof(string)).ReadOnly = true;
            dtSPAlert.Columns.Add("SPID", typeof(string)).ReadOnly = true;
            dtSPAlert.Columns.Add("SPTYPE", typeof(string)).ReadOnly = true;
            dtSPAlert.Columns.Add("SPITEMCODE", typeof(string)).ReadOnly = true;
            dtSPAlert.Columns.Add("STATUS", typeof(string)).ReadOnly = true;
            dtSPAlert.Columns.Add("备注", typeof(string)).ReadOnly = true;

            #endregion

        }

        private bool ShowItem(string moCode)
        {
            return ShowItem(moCode, false);
        }

        private bool ShowItem(string moCode, bool bNotDestination)
        {
            bool bResult = true;
            //Show Item
            if ((ir != null && ir.IsCompleted) || ir == null)
            {


                MOFacade moFAC = null;
                //support 3-Tier architecture
                try
                {
                    moFAC = (MOFacade)Activator.CreateInstance(typeof(MOFacade)
                        , new object[] { DataProvider });
                }
                catch (Exception ex)
                {
                    moFAC = new MOFacade(DataProvider);
                }

                object objMO = moFAC.GetMO(moCode);

                if (objMO != null)
                {
                    if (bNotDestination)
                    {
                        txtItemCode.Value = (objMO as Domain.MOModel.MO).ItemCode;
                    }
                    else
                    {
                        txtDestinationItem.Value = (objMO as Domain.MOModel.MO).ItemCode;

                    }
                }
                else
                {
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));

                    bResult = false;

                    Application.DoEvents();

                    if (!bNotDestination)
                    {
                        txtDestinationMo.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;
                    }
                }
            }

            return bResult;
        }



        private void FillDataSource()
        {
            object[] objs = LoadDataSource();
            FillMainDataGrid(objs);
        }

        public delegate void LoadDataSourceHander();

        IAsyncResult ir = null;
        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            //marked by hiro 20101110
            //if (txtMocode.Value.Trim() == String.Empty &&
            //    txtLineCode.Value.Trim() == String.Empty &&
            //    txtSPCode.Value.Trim() == String.Empty)
            //{
            //    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_NOTIFY_PLEASE_INPUT_LINE_MO_SID"));

            //    Application.DoEvents();
            //    txtMocode.TextFocus(false, true);
            //    return;
            //}

            LoadDataSourceHander dataHander = new LoadDataSourceHander(FillDataSource);
            object obj = new object();
            lock (obj)
            {
                ir = dataHander.BeginInvoke(null, null);
            }

            //			object[] objs = LoadDataSource();
            //			FillMainDataGrid(objs);

            //FillAlertDataGrid(objs);

        }

        private object[] LoadDataSource()
        {
            //support 3-Tier architecture

            object[] objs = _facade.QueryOnWorkSPP(txtMocode.Value.Trim(), txtLineCode.Value.Trim(), txtSPCode.Value.Trim());

            return objs;
        }

        private void FillMainDataGrid(object[] objs)
        {
            dtSP.Rows.Clear();

            #region Display solderpaste in grid

            if (objs != null && objs.Length > 0)
            {
                //support 3-Tier architecture
                try
                {
                    _facade = (SolderPasteFacade)Activator.CreateInstance(typeof(SolderPasteFacade)
                        , new object[] { DataProvider });
                }
                catch (Exception ex)
                {
                    _facade = new SolderPasteFacade(DataProvider);
                }

                string uiSPCode = txtSPCode.Value.Trim();
                foreach (Domain.SolderPaste.SOLDERPASTEPRO spp in objs)
                {

                    if (txtSPCode.Value.Trim() != String.Empty && uiSPCode != spp.SOLDERPASTEID.Substring(0, uiSPCode.Length))
                    {
                        continue;
                    }
                    //					if(spp.STATUS != Web.Helper.SolderPasteStatus.Restrain)
                    //					{
                    string returnDate = FormatHelper.TODateTimeString(spp.RETURNDATE, spp.RETURNTIME);

                    string agitateDate = FormatHelper.TODateTimeString(spp.AGITATEDATE, spp.AGITATETIME);

                    string strRCountTime = spp.RETURNCOUNTTIME.ToString().TrimEnd(new char[] { '0' });

                    string strVeilCountTime = spp.VEILCOUNTTIME.ToString().TrimEnd(new char[] { '0' });

                    string[] Rtimes = strRCountTime.Split('.');
                    string[] Vtimes = strVeilCountTime.Split('.');

                    int iRCountHour = 0, iRCountMinutes = 0;
                    int iVCountHour = 0, iVCountMinutes = 0;


                    try
                    {
                        iRCountHour = int.Parse(Rtimes[0]);
                    }
                    catch
                    { }
                    try
                    {
                        iVCountHour = int.Parse(Vtimes[0]);
                    }
                    catch { }

                    if (Rtimes.Length > 1)
                    {
                        try
                        {
                            iRCountMinutes = Convert.ToInt32(System.Math.Round(Convert.ToDouble(int.Parse(Rtimes[1]))) / 100 * 60);
                        }
                        catch { }
                    }
                    if (Vtimes.Length > 1)
                    {
                        try
                        {
                            iVCountMinutes = Convert.ToInt32(System.Math.Round(Convert.ToDouble(int.Parse(Vtimes[1]))) / 100 * 60);
                        }
                        catch { }
                    }

                    TimeSpan tsOpenDate = new TimeSpan(iRCountHour, iRCountMinutes, 0);
                    TimeSpan tsVeilDate = new TimeSpan(iVCountHour, iVCountMinutes, 0);

                    //Laws Lu,2006/11/13 uniform system collect date
                    DBDateTime dbDateTime;

                    dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                    DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

                    if (spp.STATUS == Web.Helper.SolderPasteStatus.Return)
                    {
                        tsOpenDate = workDateTime - DateTime.Parse(returnDate);
                    }
                    if (spp.STATUS == Web.Helper.SolderPasteStatus.Agitate
                        || spp.STATUS == Web.Helper.SolderPasteStatus.Return)
                    {
                        tsVeilDate = workDateTime - DateTime.Parse(returnDate);
                    }

                    string unveilDate = FormatHelper.TODateTimeString(spp.UNVEILMDATE, spp.UNVEILTIME);

                    string unveilTimeCount = spp.UNVEILCOUNTTIME.ToString().TrimEnd(new char[] { '0' });

                    TimeSpan tsUnveilDate = new TimeSpan(0);

                    if (unveilDate.Trim() != "00:00:00")
                    {
                        int iUnveilCountHour = 0, iUnveilCountMinutes = 0;

                        string[] Unveiltimes = unveilTimeCount.Split('.');

                        try
                        {
                            iUnveilCountHour = int.Parse(Unveiltimes[0]);
                        }
                        catch { }

                        try
                        {
                            iUnveilCountMinutes = int.Parse(Unveiltimes[1]);
                        }
                        catch { }

                        if (Rtimes.Length > 1)
                        {
                            try
                            {
                                iUnveilCountMinutes = Convert.ToInt32(System.Math.Round(Convert.ToDouble(int.Parse(Rtimes[1]))) / 100 * 60);
                            }
                            catch { }
                        }

                        if (spp.STATUS == Web.Helper.SolderPasteStatus.Restrain)
                        {
                            tsUnveilDate = new TimeSpan(iUnveilCountHour, iUnveilCountMinutes, 0);
                        }
                        else
                        {
                            if (unveilDate.Trim() != string.Empty)
                            {
                                tsUnveilDate = workDateTime - DateTime.Parse(unveilDate);
                            }
                        }
                    }


                    object objSP = _facade.GetSolderPaste(spp.SOLDERPASTEID);

                    Domain.SolderPaste.SolderPaste sp = null;

                    if (objSP != null)
                    {
                        sp = objSP as Domain.SolderPaste.SolderPaste;
                    }

                    dtSP.Rows.Add(new object[]{
												  spp.SOLDERPASTEID
												  ,spp.MOCODE
												  ,spp.LINECODE
												  ,UserControl.MutiLanguages.ParserString(spp.SPTYPE)
												  ,sp.PartNO
												  ,UserControl.MutiLanguages.ParserString(spp.STATUS)
												  ,returnDate
												  ,spp.RETURNTIMESPAN
												  ,System.Math.Floor(tsOpenDate.TotalHours).ToString() + "时" + tsOpenDate.Minutes + "分"
												  ,spp.VEILTIMESPAN
												  ,System.Math.Floor(tsVeilDate.TotalHours).ToString() + "时" + tsVeilDate.Minutes + "分"
												  ,unveilDate
												  ,spp.UNVEILTIMESPAN
												  ,System.Math.Floor(tsUnveilDate.TotalHours).ToString() + "时" + tsUnveilDate.Minutes + "分"
												  ,agitateDate
												  ,spp.MEMO
											  }
                        );

                    dtSP.AcceptChanges();
                }
            }

            #endregion

            ultraGridMain.DataSource = dtSP;

            InitialGridColumnStatus();

            RefreshProcessGrid();
        }


        private void RefreshProcessGrid()
        {
            if (this.ultraGridMain.Rows.Count > 0)
            {

                ArrayList arRows = new ArrayList();

                for (int iRow = 0; iRow < ultraGridMain.Rows.Count; iRow++)
                {
                    Infragistics.Win.UltraWinGrid.UltraGridRow ugr = ultraGridMain.Rows[iRow];
                    #region 获得计算所需的基础数据

                    //Laws Lu,2006/11/13 uniform system collect date
                    DBDateTime dbDateTime;

                    dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                    DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

                    string agitateDate = ugr.Cells["AGITATEDATE"].Text.ToString().Trim();//搅拌日期

                    TimeSpan tsReturnDate = workDateTime - DateTime.Parse(ugr.Cells["RETURNTIME"].Text);//回温计时 = 当前时间 - 回温时间

                    TimeSpan tsUnveilDate = new TimeSpan();

                    if (ugr.Cells["UNVEILTIME"].Text != string.Empty)
                    {
                        tsUnveilDate = workDateTime - DateTime.Parse(ugr.Cells["UNVEILTIME"].Text);//开封计时 = 当前时间 - 开封时间
                    }


                    TimeSpan tsVeilDate = workDateTime - DateTime.Parse(ugr.Cells["RETURNTIME"].Text);//未开封计时 = 当前时间 - 搅拌时间

                    string unveilDate = ugr.Cells["UNVEILTIME"].Value.ToString().Trim();//开封时间

                    decimal iVeilSpanTime = Convert.ToDecimal(ugr.Cells["VEILTIMESPAN"].Text);//未开封时长
                    decimal iAlertVeilSpanTime = Convert.ToDecimal(iVeilSpanTime * int.Parse(txtAlertPercent.Value.Trim()) / 100);
                    decimal iAlertVeilCountTime = Convert.ToDecimal(System.Math.Round(tsVeilDate.TotalHours, 2));

                    decimal iUnveilSpanTime = Convert.ToDecimal(ugr.Cells["UNVEILTIMESPAN"].Text);//开封时长
                    decimal iAlertUnveilSpanTime = Convert.ToDecimal(iUnveilSpanTime * int.Parse(txtAlertPercent.Value.Trim()) / 100);

                    string status = ugr.Cells["STATUS"].Text;

                    Domain.SolderPaste.SOLDERPASTEPRO spp = new BenQGuru.eMES.Domain.SolderPaste.SOLDERPASTEPRO();

                    #endregion

                    #region 更改行颜色、更新锡膏信息
                    ugr.Appearance.BackColor = Color.White;
                    ugr.Appearance.ForeColor = Color.Black;
                    //对于处于回温状态的锡膏如果达到或超过回温时长则变更记录颜色为“蓝色”
                    if (status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Return)
                        && tsReturnDate.TotalHours >= Convert.ToDouble(ugr.Cells["RETURNTIMESPAN"].Text))
                    {
                        ugr.Appearance.BackColor = Color.Blue;
                        ugr.Appearance.ForeColor = Color.White;
                    }
                    ////5.2.5.2	处于回温状态但已经超出回温时长的锡膏，
                    //如果未开封计时达到未开封时长的预警百分比，则变更记录颜色为“紫色”
                    if (status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Return)
                        && tsReturnDate.TotalHours >= Convert.ToDouble(ugr.Cells["RETURNTIMESPAN"].Text)
                        && iAlertVeilCountTime >= iAlertVeilSpanTime)
                    {
                        ugr.Appearance.BackColor = Color.Purple;
                        ugr.Appearance.ForeColor = Color.White;
                    }

                    //5.2.5.2	对于搅拌状态的锡膏，
                    //如果未开封计时达到未开封时长的预警百分比，则变更记录颜色为“紫色”
                    if (status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Agitate)
                        && iAlertVeilCountTime >= iAlertVeilSpanTime)
                    {
                        ugr.Appearance.BackColor = Color.Purple;
                        ugr.Appearance.ForeColor = Color.White;
                    }

                    //对于搅拌状态的锡膏，超出未开封时长，则变更记录颜色为“红色”，
                    //并将锡膏状态转换为“限制使用”状态，记录备注信息“超出未开封时长”，记录未开封计时
                    if (status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Agitate)
                        && iAlertVeilCountTime >= iVeilSpanTime)
                    {
                        ugr.Appearance.BackColor = Color.Red;
                        ugr.Appearance.ForeColor = Color.White;

                        ugr.Cells["STATUS"].Value = UserControl.MutiLanguages.ParserString(Web.Helper.SolderPasteStatus.Restrain);

                        spp.STATUS = Web.Helper.SolderPasteStatus.Restrain;

                        spp.VEILCOUNTTIME = iAlertVeilCountTime;

                        spp.MEMO = MutiLanguages.ParserString("$CS_OverVeilTime");//"超出未开封时长";
                    }

                    //处于回温状态但已经超出回温时长的锡膏，超出未开封时长，则变更记录颜色为“红色”，
                    //并将锡膏状态转换为“限制使用”状态，记录备注信息“超出未开封时长”，记录未开封计时
                    if (status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Return)
                        && tsReturnDate.TotalHours >= Convert.ToDouble(ugr.Cells["RETURNTIMESPAN"].Text)
                        && iAlertVeilCountTime >= iVeilSpanTime)
                    {
                        ugr.Appearance.BackColor = Color.Red;
                        ugr.Appearance.ForeColor = Color.White;

                        ugr.Cells["STATUS"].Value = UserControl.MutiLanguages.ParserString(Web.Helper.SolderPasteStatus.Restrain);

                        spp.STATUS = Web.Helper.SolderPasteStatus.Restrain;

                        spp.VEILCOUNTTIME = iAlertVeilCountTime;

                        spp.MEMO = MutiLanguages.ParserString("$CS_OverVeilTime");//"超出未开封时长";
                    }

                    //如果开封计时达到开封时长的预警百分比，变更记录颜色为“紫色”
                    if (status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Unveil)
                        && tsUnveilDate.TotalHours >= Convert.ToDouble(iAlertUnveilSpanTime.ToString()))
                    {
                        ugr.Appearance.BackColor = Color.Purple;
                        ugr.Appearance.ForeColor = Color.White;
                    }


                    //如果锡膏超出了开封时长，则变更记录颜色为“红色”，
                    //并变更锡膏状态未“限制使用”状态，记录备注信息“超出开封时长”，记录开封计时
                    if (status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Unveil)
                        && tsUnveilDate.TotalHours >= Convert.ToDouble(iUnveilSpanTime.ToString()))
                    {
                        ugr.Appearance.BackColor = Color.Red;
                        ugr.Appearance.ForeColor = Color.White;

                        ugr.Cells["STATUS"].Value = UserControl.MutiLanguages.ParserString(Web.Helper.SolderPasteStatus.Restrain);

                        spp.STATUS = Web.Helper.SolderPasteStatus.Restrain;
                        spp.UNVEILCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsUnveilDate.TotalHours, 2));
                        spp.MEMO = MutiLanguages.ParserString("$CS_OverUnveilTime");//"超出开封时长";
                    }

                    //如果锡膏状态为限制使用，则变更记录颜色为“红色”，
                    if (status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Restrain))
                    {
                        ugr.Appearance.BackColor = Color.Red;
                        ugr.Appearance.ForeColor = Color.White;
                    }

                    //更新锡膏使用记录
                    if (spp.STATUS != null && spp.STATUS != String.Empty)
                    {
                        object objSP = _facade.GetSolderPaste(ugr.Cells["SPID"].Text);

                        object objSPP = _facade.GetSOLDERPASTEPROBySPPID(ugr.Cells["SPID"].Text.Trim());

                        if (objSPP != null && (objSPP as Domain.SolderPaste.SOLDERPASTEPRO).STATUS != Web.Helper.SolderPasteStatus.Restrain)
                        {
                            Domain.SolderPaste.SolderPaste sp = objSP as Domain.SolderPaste.SolderPaste;

                            Domain.SolderPaste.SOLDERPASTEPRO sppNew = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;

                            sp.MaintainUser = ApplicationService.Current().UserCode;
                            sp.MaintainDate = FormatHelper.TODateInt(workDateTime);
                            sp.MaintainTime = FormatHelper.TOTimeInt(workDateTime);

                            sppNew.MUSER = sp.MaintainUser;
                            sppNew.MDATE = dbDateTime.DBDate;
                            sppNew.MTIME = dbDateTime.DBTime;

                            DataProvider.BeginTransaction();

                            try
                            {
                                _facade.DeleteSOLDERPASTEPRO(sppNew);

                                sppNew.STATUS = spp.STATUS;
                                sppNew.MEMO = spp.MEMO;

                                if (sppNew.MEMO == MutiLanguages.ParserString("$CS_OverUnveilTime"))//"超出开封时长")
                                {

                                    sppNew.UNVEILCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsUnveilDate.TotalHours, 2));
                                }
                                if (sppNew.MEMO == MutiLanguages.ParserString("$CS_OverVeilTime"))//"超出未开封时长")
                                {
                                    if (status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Return))
                                    {
                                        sppNew.RETURNCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsReturnDate.TotalHours, 2));
                                    }
                                    sppNew.VEILCOUNTTIME = iAlertVeilCountTime;
                                }

                                _facade.AddSOLDERPASTEPRO(sppNew);

                                sp.Status = spp.STATUS;

                                _facade.UpdateSolderPaste(sp);

                                DataProvider.CommitTransaction();

                                arRows.Add(ugr);
                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex.Message);
                                DataProvider.RollbackTransaction();

                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));
                            }
                            finally
                            {
                                //								((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                            }
                        }
                    }
                    #endregion

                }
                foreach (Infragistics.Win.UltraWinGrid.UltraGridRow ugr in arRows)
                {
                    ugr.Selected = true;
                    ultraGridMain.DeleteSelectedRows(false);
                }
                this.ultraGridMain.Refresh();
            }


        }

        private void ultraGridMain_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            //			UltraWinGridHelper ultraWinGridHelper = new UltraWinGridHelper(this.ultraGridMain);
            //
            //			//dtSP.Columns.Add("",typeof(string));
            //			//ultraWinGridHelper.AddCheckColumn("checkbox","*");
            //
            ////			Infragistics.Win.ValueList vl = new Infragistics.Win.ValueList();
            ////			SolderPasteStatus sps = new SolderPasteStatus();
            ////			foreach(string status in sps.Items)
            ////			{
            ////				vl.ValueListItems.Add(status,UserControl.MutiLanguages.ParserString(status));
            ////			}
            //
            //			ultraWinGridHelper.AddCommonColumn("SPID","锡膏ID");
            //			ultraWinGridHelper.AddCommonColumn("SPTYPE","锡膏类型");
            //			ultraWinGridHelper.AddCommonColumn("SPITEMCODE","锡膏物料号");
            //			ultraWinGridHelper.AddCommonColumn("STATUS","使用状态");
            //			ultraWinGridHelper.AddCommonColumn("RETURNTIME","回温时间");
            //			ultraWinGridHelper.AddCommonColumn("RETURNTIMESPAN","回温时长");
            //			ultraWinGridHelper.AddCommonColumn("RETURNCOUNTTIME","回温计时");
            //			ultraWinGridHelper.AddCommonColumn("VEILTIMESPAN","未开封时长");
            //			ultraWinGridHelper.AddCommonColumn("VEILCOUNTTIME","未开封计时");
            //			ultraWinGridHelper.AddCommonColumn("UNVEILTIME","开封时间");
            //			ultraWinGridHelper.AddCommonColumn("UNVEILTIMESPAN","开封时长");
            //			ultraWinGridHelper.AddCommonColumn("UNVEILCONTTIME","开封计时");
            //
            //			ultraWinGridHelper.AddCommonColumn("AGITATEDATE","搅拌日期");
            //			ultraWinGridHelper.AddCommonColumn("工单","工单代码");
            //			ultraWinGridHelper.AddCommonColumn("产线","产线代码");
            //			ultraWinGridHelper.AddCommonColumn("MEMO","备注");
            //
            //			//ultraGridMain.DataSource = dtSP;
            //
            //			InitialGridColumnStatus();

            this.InitGridLanguage(ultraGridMain);
        }

        private void InitialGridColumnStatus()
        {
            if (this.ultraGridMain.DisplayLayout.Bands.Count > 0)
            {
                if (this.ultraGridMain.DisplayLayout.Bands[0].Columns.Count > 0)
                {
                    this.ultraGridMain.DisplayLayout.Bands[0].Columns["AGITATEDATE"].Hidden = true;
                    //this.ultraGridMain.DisplayLayout.Bands[0].Columns["工单"].Hidden = true;
                    //this.ultraGridMain.DisplayLayout.Bands[0].Columns["产线"].Hidden = true;
                    this.ultraGridMain.DisplayLayout.Bands[0].Columns["MEMO"].Hidden = true;
                }
            }

        }

        private void txtMocode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string moCode = txtMocode.Value.ToUpper().Trim();
                if (moCode != String.Empty)
                {
                    ShowItem(moCode, true);//show the item code by mocode 
                }
            }
        }

        private void btnUnveil_Click(object sender, System.EventArgs e)
        {
            #region Unveil Process

            Infragistics.Win.UltraWinGrid.UltraGridRow ugr = ultraGridMain.ActiveRow;

            object objSPP = _facade.GetSPP(ugr.Cells["SPID"].Text
                , ugr.Cells["MO"].Text
                , ugr.Cells["SSCODE"].Text);

            if (objSPP != null)
            {
                Domain.SolderPaste.SOLDERPASTEPRO spp = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;

                if (spp.STATUS == Web.Helper.SolderPasteStatus.Agitate)
                {
                    //Laws Lu,2006/11/13 uniform system collect date
                    DBDateTime dbDateTime;

                    dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                    DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

                    spp.UNVEILUSER = ApplicationService.Current().LoginInfo.UserCode;
                    spp.UNVEILMDATE = FormatHelper.TODateInt(workDateTime);
                    spp.UNVEILTIME = FormatHelper.TOTimeInt(workDateTime);

                    TimeSpan tsVeilDate = workDateTime - DateTime.Parse(ugr.Cells["RETURNTIME"].Text);//未开封计时 = 当前时间 - 搅拌时间

                    spp.VEILCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsVeilDate.TotalHours, 2));

                    spp.MUSER = ApplicationService.Current().LoginInfo.UserCode;
                    spp.MDATE = FormatHelper.TODateInt(workDateTime);
                    spp.MTIME = FormatHelper.TOTimeInt(workDateTime);

                    DataProvider.BeginTransaction();

                    try
                    {
                        _facade.DeleteSOLDERPASTEPRO(spp);

                        spp.STATUS = Web.Helper.SolderPasteStatus.Unveil;

                        _facade.AddSOLDERPASTEPRO(spp);

                        DataProvider.CommitTransaction();

                        //btnSearch_Click(sender,e);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                        DataProvider.RollbackTransaction();
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));
                    }
                    finally
                    {
                        //						((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    }
                }
            }

            #endregion
        }

        private void btnAgitate_Click(object sender, System.EventArgs e)
        {

            #region Agitate Process

            Infragistics.Win.UltraWinGrid.UltraGridRow ugr = ultraGridMain.ActiveRow;

            if (ugr.Appearance.BackColor != Color.Blue && ugr.Appearance.BackColor != Color.Purple)
            {
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_STATUS_WRONG $Domain_SolderPaste_ID = " + ugr.Cells["SPID"].Text
                    + " $Current_Status = " + ugr.Cells["STATUS"].Text));

                return;
            }

            object objSPP = _facade.GetSPP(ugr.Cells["SPID"].Text
                , ugr.Cells["MO"].Text
                , ugr.Cells["SSCODE"].Text);

            if (objSPP != null)
            {
                Domain.SolderPaste.SOLDERPASTEPRO spp = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;

                if (spp.STATUS == Web.Helper.SolderPasteStatus.Return)
                {
                    //Laws Lu,2006/11/13 uniform system collect date
                    DBDateTime dbDateTime;

                    dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                    DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

                    spp.AGITAEUSER = ApplicationService.Current().LoginInfo.UserCode;
                    spp.AGITATEDATE = FormatHelper.TODateInt(workDateTime);
                    spp.AGITATETIME = FormatHelper.TOTimeInt(workDateTime);

                    TimeSpan tsOpenDate = workDateTime - DateTime.Parse(ugr.Cells["RETURNTIME"].Text);//回温计时 = 当前时间 - 回温时间

                    spp.RETURNCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsOpenDate.TotalHours, 2));

                    spp.MUSER = ApplicationService.Current().LoginInfo.UserCode;
                    spp.MDATE = FormatHelper.TODateInt(workDateTime);
                    spp.MTIME = FormatHelper.TOTimeInt(workDateTime);

                    DataProvider.BeginTransaction();

                    try
                    {
                        _facade.DeleteSOLDERPASTEPRO(spp);

                        spp.STATUS = Web.Helper.SolderPasteStatus.Agitate;

                        _facade.AddSOLDERPASTEPRO(spp);


                        DataProvider.CommitTransaction();

                        //btnSearch_Click(sender,e);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                        DataProvider.RollbackTransaction();
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));
                    }
                    finally
                    {
                        //						((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    }
                }
            }

            #endregion
        }

        private void ultraGridMain_MouseHover(object sender, System.EventArgs e)
        {
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            timer1.Interval = int.Parse(txtRefreshRate.Value.Trim()) * 60 * 1000;

            //if(dataHander.
            if ((ir != null && ir.IsCompleted) || ir == null)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void ultraGridMain_MouseEnterElement(object sender, Infragistics.Win.UIElementEventArgs e)
        {

        }

        private void ultraGridMain_MouseLeaveElement(object sender, Infragistics.Win.UIElementEventArgs e)
        {

        }

        private void btnConfrim_Click(object sender, System.EventArgs e)
        {
            if (txtOPUser.Value.Trim() == String.Empty)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_PLEASE_INPUT_OP_USER"));

                Application.DoEvents();
                txtOPUser.TextFocus(false, true);
                //Remove UCLabel.SelectAll;

                return;
            }
            if (txtSolderPasteID.Value.Trim() != String.Empty)
            {
                if ((ir != null && ir.IsCompleted) || ir == null)
                {
                    //Laws Lu,2007/01/05,新增	缓解性能问题
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                    if (radReturn.Checked)
                    {
                        ReturnSolderPaste();
                    }
                    else if (radOpen.Checked)
                    {
                        OpenSolderPaste();
                    }
                    else if (radReflow.Checked)
                    {
                        ReflowSolderPaste();
                    }
                    else if (radUsedUp.Checked)
                    {
                        UsedUpSolderPaste();
                    }
                    else if (radUnavial.Checked)
                    {
                        UnavialSolderPaste();
                    }
                    else if (radUnveil.Checked)
                    {
                        UnveilSolderPaste();
                    }
                    else if (radAgitate.Checked)
                    {
                        AgitateSolderPaste();
                    }
                    else if (radTransferMo.Checked)
                    {
                        TransferMo();
                    }

                    //Laws Lu,2007/01/05,新增	缓解性能问题
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
                }
            }
        }
        /// <summary>
        /// 锡膏领用
        /// </summary>
        private void OpenSolderPaste()
        {
            //4.3.4.1	必须输入了工单和产线资料
            if (txtDestinationMo.Value.Trim() == String.Empty)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_CMPleaseInputMO"));

                Application.DoEvents();
                txtDestinationMo.TextFocus(false, true);
                //Remove UCLabel.SelectAll;

                return;
            }

            if (txtDestinationLine.Value.Trim() == String.Empty)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_SSCode_NotExist"));

                Application.DoEvents();
                txtDestinationLine.TextFocus(false, true);
                //Remove UCLabel.SelectAll;

                return;
            }
            //Laws Lu，2007/03/09	检查工单
            if (!ShowItem(txtDestinationMo.Value.Trim()))
            {
                return;
            }

            object objSP = _facade.GetSolderPaste(txtSolderPasteID.Value.Trim());

            Messages msg = new Messages();

            if (objSP != null)
            {
                Domain.SolderPaste.SolderPaste sp = objSP as Domain.SolderPaste.SolderPaste;

                //Laws Lu,2006/11/13 uniform system collect date
                DBDateTime dbDateTime;

                dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);


                #region 锡膏领用

                //4.3.4.2	该锡膏必须是没有被领用到其他工单的正常状态或回存的锡膏,
                //检查使用状态
                object objExistSPP = null;
                if (sp.Status != Web.Helper.SolderPasteStatus.Reflow && sp.Status != Web.Helper.SolderPasteStatus.Normal)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_SP_STATUS_MUST_BE_NORMAIL_OR_REFLOW"));
                }
                else if (sp.Status == Web.Helper.SolderPasteStatus.Normal)
                {
                    objExistSPP = _facade.GetSOLDERPASTEPROBySPPID(txtSolderPasteID.Value.Trim());

                    if (objExistSPP != null)
                    {
                        Domain.SolderPaste.SOLDERPASTEPRO existSpp = objExistSPP as Domain.SolderPaste.SOLDERPASTEPRO;

                        if (existSpp.STATUS != Web.Helper.SolderPasteStatus.Reflow
                            && existSpp.STATUS != Web.Helper.SolderPasteStatus.Restrain
                            && existSpp.STATUS != Web.Helper.SolderPasteStatus.scrap
                            && existSpp.STATUS != Web.Helper.SolderPasteStatus.UsedUp)
                        {
                            if (existSpp.MOCODE != String.Empty)
                            {
                                msg.Add(new UserControl.Message(MessageType.Error, "$CS_SOLDERPASTE_ALREADY_USING $Domain_SolderPaste_ID = " + sp.SolderPasteID));
                            }
                            //回温状态允许领用
                            if (existSpp.STATUS != Web.Helper.SolderPasteStatus.Return)
                            //							{
                            ////								if(existSpp.OPENUSER != String.Empty)
                            ////								{
                            ////									msg.Add(new UserControl.Message(MessageType.Error,"$CS_SOLDERPASTE_ALREADY_USING $Domain_SolderPaste_ID = " + sp.SolderPasteID));
                            ////								}
                            //							}
                            //							else
                            {
                                msg.Add(new UserControl.Message(MessageType.Error, "$CS_SOLDERPASTE_ALREADY_USING $Domain_SolderPaste_ID = " + sp.SolderPasteID));
                            }
                        }
                    }
                }
                //4.3.4.3	检查失效日期，如果达到失效日期则不能领用，并变更其状态为限制使用，并记录备注信息为“超出有效期”
                if (msg.IsSuccess())
                {

                    if (sp.ExpiringDate <= FormatHelper.TODateInt(workDateTime))//检查失效日期
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_SOLDERPASTE_ALREADY_EXPIRED"));

                        #region 4.3.4.1	检查失效日期，如果达到失效日期则不能领用，并变更其状态为限制使用，并记录备注信息为“超出有效期”
                        sp.Status = Web.Helper.SolderPasteStatus.Restrain;
                        sp.eAttribute1 = "超出有效期";
                        DataProvider.BeginTransaction();
                        try
                        {
                            _facade.UpdateSolderPaste(sp);

                            DataProvider.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            msg.Add(new UserControl.Message(ex));
                            DataProvider.RollbackTransaction();
                        }
                        finally
                        {
                            //							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
                            //								.PersistBroker.CloseConnection();
                        }
                        #endregion
                    }
                }

                Domain.SolderPaste.SOLDERPASTEPRO spp = _facade.CreateNewSOLDERPASTEPRO();

                //4.3.4.4	检查该盒锡膏对应的锡膏类型和当前工单对应的产品代码是否匹配
                //获取锡膏类型和产品对应关系实体
                object objSP2Item = null;
                if (msg.IsSuccess())
                {
                    ShowItem(txtDestinationMo.Value.Trim());
                    objSP2Item = _facade.GetSolderPaste2Item(txtDestinationItem.Value.Trim());
                    //同步产品
                    Domain.SolderPaste.SolderPaste2Item sp2item = new BenQGuru.eMES.Domain.SolderPaste.SolderPaste2Item();


                    if (objSP2Item == null)
                    {
                        sp2item.ItemCode = txtDestinationItem.Value.Trim();
                        sp2item.MaintainUser = ApplicationService.Current().UserCode;

                        if (txtDestinationItem.Value.Trim().Length > 1 && char.IsLetter(txtDestinationItem.Value.Trim(), 1))
                        {
                            sp2item.SolderPasteType = SolderPasteType.Pb_Free;

                            #region 新增产品和锡膏类型对应

                            DataProvider.BeginTransaction();
                            try
                            {
                                _facade.AddSolderPaste2Item(sp2item);

                                DataProvider.CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                msg.Add(new UserControl.Message(ex));
                                DataProvider.RollbackTransaction();
                            }
                            finally
                            {
                                //								((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
                                //									.PersistBroker.CloseConnection();
                            }

                            #endregion

                            //objSP2Item = sp2item;
                        }
                        else if (txtDestinationItem.Value.Trim().Length > 1 && char.IsDigit(txtDestinationItem.Value.Trim(), 1))
                        {
                            sp2item.SolderPasteType = SolderPasteType.Pb;

                            #region 新增产品和锡膏类型对应

                            DataProvider.BeginTransaction();
                            try
                            {
                                _facade.AddSolderPaste2Item(sp2item);

                                DataProvider.CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                msg.Add(new UserControl.Message(ex));
                                DataProvider.RollbackTransaction();
                            }
                            finally
                            {
                                //								((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
                                //									.PersistBroker.CloseConnection();
                            }

                            #endregion

                            //objSP2Item = sp2item;
                        }
                        else
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$CS_SP_ITEM_NOT_MATCH $Domain_SolderPaste_ID = " + sp.SolderPasteID));
                        }
                    }
                    else
                    {
                        sp2item = objSP2Item as Domain.SolderPaste.SolderPaste2Item;
                    }

                    object objSPCTR = _facade.GetSolderPasteControl(sp.PartNO);

                    //获取锡膏类型
                    if (objSPCTR != null)
                    {
                        Domain.SolderPaste.SolderPasteControl spCTR = objSPCTR as Domain.SolderPaste.SolderPasteControl;

                        spp.RETURNTIMESPAN = spCTR.ReturnTimeSpan;
                        spp.UNVEILTIMESPAN = spCTR.OpenTimeSpan;
                        spp.VEILTIMESPAN = spCTR.UnOpenTimeSpan;

                        spp.SPTYPE = spCTR.Type;
                    }

                    if (msg.IsSuccess())
                    {
                        //比较锡膏类型和产品是否匹配
                        if (sp2item.SolderPasteType != spp.SPTYPE)
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$CS_SP_ITEM_NOT_MATCH $Domain_SolderPaste_ID = " + sp.SolderPasteID));
                        }
                    }
                }

                if (msg.IsSuccess())
                {

                    object[] objReflowSPs = _facade.GetReflowInSPPByItem(txtDestinationItem.Value.Trim());
                    bool bExist = false;
                    if (objReflowSPs != null)
                    {
                        foreach (Domain.SolderPaste.SolderPaste tmp in objReflowSPs)
                        {
                            if (tmp.SolderPasteID == sp.SolderPasteID)
                            {
                                bExist = true;
                                break;
                            }
                        }
                        //4.3.4.5	检查适用于当前产品的锡膏中是否有回存的锡膏，如果有回存的锡膏再判断目前领用的锡膏是否是回存的锡膏，如果不是则不能领用
                        if (objReflowSPs != null && objReflowSPs.Length > 0 && !bExist)
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$CS_SP_EXIST_RESAVE_MUST_BE_USED $Domain_SolderPaste_ID = "
                                + (objReflowSPs[0] as Domain.SolderPaste.SolderPaste).SolderPasteID));
                        }
                        else if (!bExist)
                        {
                            //4.3.4.6	先进先出的检查：在适用于当前产品的锡膏没有回存锡膏的情况下，
                            //检查领用的锡膏是否是所有适于当前产品的锡膏中生产日期最早的锡膏，如果不是则列出最早生产日期的锡膏并给出提示信息，
                            //由用户自行决定是领用该锡膏，还是从列表中选择一个最早生产日期的锡膏
                            object objFirstInSP = _facade.GetFirstInSPPByItem(txtDestinationItem.Value.Trim());//,sp.ProductionDate.ToString(),sp.SolderPasteID);

                            if (objFirstInSP != null && (objFirstInSP as Domain.SolderPaste.SolderPaste).ProductionDate != sp.ProductionDate)
                            {
                                string message = UserControl.MutiLanguages.ParserMessage("$CS_ALREADY_EXIST_SOLDERPASTE $Domain_SolderPaste_ID = " + (objFirstInSP as Domain.SolderPaste.SolderPaste).SolderPasteID);
                                if (DialogResult.Yes == MessageBox.Show(this, message, MutiLanguages.ParserString("$ShowConfirm"), MessageBoxButtons.YesNo))
                                {
                                    txtSolderPasteID.TextFocus(false, true);
                                    //Remove UCLabel.SelectAll;
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        //4.3.4.6	先进先出的检查：在适用于当前产品的锡膏没有回存锡膏的情况下，
                        //检查领用的锡膏是否是所有适于当前产品的锡膏中生产日期最早的锡膏，如果不是则列出最早生产日期的锡膏并给出提示信息，
                        //由用户自行决定是领用该锡膏，还是从列表中选择一个最早生产日期的锡膏
                        object objFirstInSP = _facade.GetFirstInSPPByItem(txtDestinationItem.Value.Trim());//,sp.ProductionDate.ToString(),sp.SolderPasteID);

                        if (objFirstInSP != null && (objFirstInSP as Domain.SolderPaste.SolderPaste).ProductionDate != sp.ProductionDate)
                        {
                            string message = UserControl.MutiLanguages.ParserMessage("$CS_ALREADY_EXIST_SOLDERPASTE $Domain_SolderPaste_ID = " + (objFirstInSP as Domain.SolderPaste.SolderPaste).SolderPasteID);
                            if (DialogResult.Yes == MessageBox.Show(this, message, MutiLanguages.ParserString("$ShowConfirm"), MessageBoxButtons.YesNo))
                            {
                                txtSolderPasteID.TextFocus(false, true);
                                //Remove UCLabel.SelectAll;
                                return;
                            }
                        }
                    }
                }

                if (msg.IsSuccess())
                {
                    #region 更新SolderPaste Process

                    if (msg.IsSuccess())
                    {
                        //4.3.4.7	上述检查通过后判断该锡膏是否已经在回温，
                        //如果不在回温则添加使用记录并开始回温计时和未开封计时；
                        //否则只要记录领用当前锡膏的工单、产线到最新的使用记录上
                        spp.SEQUENCE = 1;

                        Domain.SolderPaste.SOLDERPASTEPRO sppExist = null;
                        if (objExistSPP != null)
                        {
                            sppExist = objExistSPP as Domain.SolderPaste.SOLDERPASTEPRO;
                            if (sppExist.STATUS == Web.Helper.SolderPasteStatus.Return)
                            {
                                spp = sppExist;

                            }
                        }

                        spp.LOTNO = sp.LotNO;
                        sp.Status = Web.Helper.SolderPasteStatus.Normal;//change status normal
                        sp.MaintainUser = ApplicationService.Current().UserCode;
                        sp.MaintainDate = FormatHelper.TODateInt(workDateTime);
                        sp.MaintainTime = FormatHelper.TOTimeInt(workDateTime);

                        spp.SOLDERPASTEID = sp.SolderPasteID;

                        spp.STATUS = Web.Helper.SolderPasteStatus.Return;

                        //

                        spp.MUSER = sp.MaintainUser;
                        spp.MDATE = dbDateTime.DBDate;
                        spp.MTIME = dbDateTime.DBTime;

                        spp.OPENUSER = txtOPUser.Value.Trim();
                        spp.OPENDATE = FormatHelper.TODateInt(workDateTime);
                        spp.OPENTIME = FormatHelper.TOTimeInt(workDateTime);

                        //						spp.MOCODE = txtMocode.Value.Trim();
                        //						spp.LINECODE = txtLineCode.Value.Trim();
                        //						spp.EXPIREDDATE = sp.ExpiringDate;

                        DataProvider.BeginTransaction();
                        try
                        {
                            _facade.UpdateSolderPaste(sp);

                            if (sppExist != null && sppExist.STATUS == Web.Helper.SolderPasteStatus.Return)
                            {
                                spp.MOCODE = txtDestinationMo.Value.Trim();
                                spp.LINECODE = txtDestinationLine.Value.Trim();

                                spp.SEQUENCE = spp.SEQUENCE + 1;

                                _facade.UpdateSOLDERPASTEPRO(spp);
                            }
                            else
                            {
                                spp.SPPKID = System.Guid.NewGuid().ToString();

                                spp.MOCODE = txtDestinationMo.Value.Trim();
                                spp.LINECODE = txtDestinationLine.Value.Trim();
                                spp.EXPIREDDATE = sp.ExpiringDate;


                                spp.RETRUNUSER = txtOPUser.Value.Trim();
                                spp.RETURNDATE = FormatHelper.TODateInt(workDateTime);
                                spp.RETURNTIME = FormatHelper.TOTimeInt(workDateTime);

                                _facade.AddSOLDERPASTEPRO(spp);
                            }
                            DataProvider.CommitTransaction();

                            msg.Add(
                                new UserControl.Message(MessageType.Success
                                , "$CS_SP_OPEN_SUCCESS $Domain_SolderPaste_ID = " + sp.SolderPasteID));

                            ClearInputControl();
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
                            msg.Add(new UserControl.Message(MessageType.Error, ex.Message));
                            DataProvider.RollbackTransaction();
                        }
                        finally
                        {
                            //							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
                            //								.PersistBroker.CloseConnection();
                        }
                    }
                    #endregion
                }

                #endregion
            }
            else
            {
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error
                    , "$CS_SOLDERPASTE_NOT_EXIST $Domain_SolderPaste_ID = " + txtSolderPasteID.Value.Trim()));

            }

            ApplicationRun.GetInfoForm().Add(msg);
            txtSolderPasteID.TextFocus(false, true);
            //Remove UCLabel.SelectAll;

        }
        /// <summary>
        /// 回温
        /// </summary>
        private void ReturnSolderPaste()
        {
            object objSP = _facade.GetSolderPaste(txtSolderPasteID.Value.Trim());
            Domain.SolderPaste.SolderPaste sp = null;

            Messages msg = new Messages();
            if (objSP != null)
            {
                sp = objSP as Domain.SolderPaste.SolderPaste;


                object objExistSPP = null;
                //4.3.3.2	因为锡膏领用后会自动开始回温，因此只有锡膏主档中标识为正常状态和回存状态且尚未被领用的锡膏才可以回温
                //检查使用状态

                if (sp.Status == Web.Helper.SolderPasteStatus.Normal || sp.Status == Web.Helper.SolderPasteStatus.Reflow)
                {
                    objExistSPP = _facade.GetSOLDERPASTEPROBySPPID(txtSolderPasteID.Value.Trim());

                    if (objExistSPP != null
                        && (objExistSPP as Domain.SolderPaste.SOLDERPASTEPRO).OPENUSER != String.Empty
                        && sp.Status != Web.Helper.SolderPasteStatus.Reflow)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_SOLDERPASTE_ALREADY_USING $Domain_SolderPaste_ID = " + sp.SolderPasteID));
                    }

                    if (objExistSPP != null
                        && (objExistSPP as Domain.SolderPaste.SOLDERPASTEPRO).STATUS == Web.Helper.SolderPasteStatus.Return
                        && (objExistSPP as Domain.SolderPaste.SOLDERPASTEPRO).OPENUSER == String.Empty)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_SOLDERPASTE_ALREADY_USING $Domain_SolderPaste_ID = " + sp.SolderPasteID));
                    }
                }
                else
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_SP_STATUS_MUST_BE_NORMAIL_OR_REFLOW"));
                }

                if (msg.IsSuccess())
                {
                    //Laws Lu,2006/11/13 uniform system collect date
                    DBDateTime dbDateTime;

                    dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                    DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);


                    if (sp.ExpiringDate <= FormatHelper.TODateInt(workDateTime))//检查失效日期
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_SOLDERPASTE_ALREADY_EXPIRED"));

                        #region 4.3.3.1	检查有效期，超出有效期的锡膏不能再回温，并自动转为限制使用状态
                        sp.Status = Web.Helper.SolderPasteStatus.Restrain;
                        sp.eAttribute1 = "超出有效期";
                        DataProvider.BeginTransaction();
                        try
                        {
                            _facade.UpdateSolderPaste(sp);

                            DataProvider.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            msg.Add(new UserControl.Message(ex));
                            DataProvider.RollbackTransaction();
                        }
                        finally
                        {
                            //							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
                            //								.PersistBroker.CloseConnection();
                        }
                        #endregion
                    }
                }

                Domain.SolderPaste.SOLDERPASTEPRO spp = _facade.CreateNewSOLDERPASTEPRO();

                #region 回温

                if (msg.IsSuccess())
                {
                    object objSPCTR = _facade.GetSolderPasteControl(sp.PartNO);
                    //获取锡膏类型
                    if (objSPCTR != null)
                    {
                        Domain.SolderPaste.SolderPasteControl spCTR = objSPCTR as Domain.SolderPaste.SolderPasteControl;

                        spp.RETURNTIMESPAN = spCTR.ReturnTimeSpan;
                        spp.UNVEILTIMESPAN = spCTR.OpenTimeSpan;
                        spp.VEILTIMESPAN = spCTR.UnOpenTimeSpan;

                        spp.SPTYPE = spCTR.Type;
                    }

                    spp.LOTNO = sp.LotNO;

                    //Laws Lu,2006/11/13 uniform system collect date
                    DBDateTime dbDateTime;

                    dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                    DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);


                    sp.Status = Web.Helper.SolderPasteStatus.Normal;//change status normal
                    sp.MaintainUser = ApplicationService.Current().UserCode;
                    sp.MaintainDate = FormatHelper.TODateInt(workDateTime);
                    sp.MaintainTime = FormatHelper.TOTimeInt(workDateTime);

                    spp.SOLDERPASTEID = sp.SolderPasteID;

                    spp.STATUS = Web.Helper.SolderPasteStatus.Return;

                    spp.SEQUENCE = spp.SEQUENCE + 1;

                    spp.MUSER = sp.MaintainUser;
                    spp.MDATE = dbDateTime.DBDate;
                    spp.MTIME = dbDateTime.DBTime;

                    //					spp.OPENUSER = txtOPUser.Value.Trim();
                    //					spp.OPENDATE = FormatHelper.TODateInt(DateTime.Now);
                    //					spp.OPENTIME = FormatHelper.TOTimeInt(DateTime.Now);

                    spp.SPPKID = System.Guid.NewGuid().ToString();

                    //					spp.MOCODE = txtMocode.Value.Trim();
                    //					spp.LINECODE = txtLineCode.Value.Trim();
                    spp.EXPIREDDATE = sp.ExpiringDate;

                    spp.RETRUNUSER = txtOPUser.Value.Trim();
                    spp.RETURNDATE = FormatHelper.TODateInt(workDateTime);
                    spp.RETURNTIME = FormatHelper.TOTimeInt(workDateTime);

                    DataProvider.BeginTransaction();
                    try
                    {
                        _facade.UpdateSolderPaste(sp);

                        _facade.AddSOLDERPASTEPRO(spp);
                        DataProvider.CommitTransaction();

                        msg.Add(
                            new UserControl.Message(MessageType.Success
                            , "$CS_SP_RETURN_SUCCESS $Domain_SolderPaste_ID = " + sp.SolderPasteID));


                        ClearInputControl();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                        msg.Add(new UserControl.Message(MessageType.Error, ex.Message));
                        DataProvider.RollbackTransaction();
                    }
                    finally
                    {

                        //						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
                        //							.PersistBroker.CloseConnection();
                    }
                }
                #endregion
            }
            else
            {
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error
                    , "$CS_SOLDERPASTE_NOT_EXIST $Domain_SolderPaste_ID = " + txtSolderPasteID.Value.Trim()));

            }

            ApplicationRun.GetInfoForm().Add(msg);
            txtSolderPasteID.TextFocus(false, true);
            //Remove UCLabel.SelectAll;
        }
        /// <summary>
        /// 回存
        /// </summary>
        private void ReflowSolderPaste()
        {
            object objNoramlSP = _facade.GetSolderPaste(txtSolderPasteID.Value.Trim());
            object objSPP = _facade.GetSOLDERPASTEPROBySPPID(txtSolderPasteID.Value.Trim());

            if (objSPP != null && objNoramlSP != null)
            {
                Domain.SolderPaste.SolderPaste spNormal = objNoramlSP as Domain.SolderPaste.SolderPaste;
                Domain.SolderPaste.SOLDERPASTEPRO spp = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;
                //4.3.5.1	锡膏在最新的使用记录中处于回温、搅拌、开封这三种状态中的任何一种。
                if (spp.STATUS == Web.Helper.SolderPasteStatus.Return
                    || spp.STATUS == Web.Helper.SolderPasteStatus.Unveil
                    || spp.STATUS == Web.Helper.SolderPasteStatus.Agitate)
                {
                    #region 回存

                    //Laws Lu,2006/11/13 uniform system collect date
                    DBDateTime dbDateTime;

                    dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                    DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);


                    spNormal.Status = Web.Helper.SolderPasteStatus.Reflow;
                    spNormal.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;
                    spNormal.MaintainDate = FormatHelper.TODateInt(workDateTime);
                    spNormal.MaintainTime = FormatHelper.TOTimeInt(workDateTime);

                    spp.RESAVEUSER = txtOPUser.Value.Trim();
                    spp.RESAVEDATE = FormatHelper.TODateInt(workDateTime);
                    spp.RESAVETIME = FormatHelper.TOTimeInt(workDateTime);

                    spp.MUSER = spNormal.MaintainUser;
                    spp.MDATE = dbDateTime.DBDate;
                    spp.MTIME = dbDateTime.DBTime;

                    if (spp.STATUS == Web.Helper.SolderPasteStatus.Unveil)
                    {
                        string unveilTime = FormatHelper.TODateTimeString(spp.UNVEILMDATE, spp.UNVEILTIME);
                        TimeSpan tsUnveilDate = workDateTime - DateTime.Parse(unveilTime);//开封计时 = 当前时间 - 开封时间

                        spp.UNVEILCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsUnveilDate.TotalHours, 2));
                    }

                    if (spp.STATUS == Web.Helper.SolderPasteStatus.Return
                        || spp.STATUS == Web.Helper.SolderPasteStatus.Agitate)
                    {
                        string openTime = FormatHelper.TODateTimeString(spp.RETURNDATE, spp.RETURNTIME);
                        TimeSpan tsVeilDate = workDateTime - DateTime.Parse(openTime);//未开封计时 = 当前时间 - 回温时间
                        //Laws Lu,2006/08/23 修改
                        //一个锡膏ID回存后，再重新领用，然后搅拌，然后在回存，系统记录的回温计时错误；
                        if (spp.STATUS == Web.Helper.SolderPasteStatus.Return)
                        {
                            spp.RETURNCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsVeilDate.TotalHours, 2));
                        }

                        spp.VEILCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsVeilDate.TotalHours, 2));
                    }


                    DataProvider.BeginTransaction();
                    try
                    {
                        _facade.UpdateSolderPaste(spNormal);


                        spp.STATUS = spNormal.Status;

                        _facade.UpdateSOLDERPASTEPRO(spp);

                        DataProvider.CommitTransaction();

                        ApplicationRun.GetInfoForm().Add(
                            new UserControl.Message(MessageType.Success
                            , "$CS_SP_RESAVE_SUCCESS $Domain_SolderPaste_ID = " + spp.SOLDERPASTEID));

                        ClearInputControl();
                    }
                    catch (Exception ex)
                    {
                        DataProvider.RollbackTransaction();

                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));
                    }
                    finally
                    {
                        //						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
                        //							.PersistBroker.CloseConnection();
                    }

                    #endregion
                }
                else
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error
                        , "$CS_STATUS_WRONG $Current_Status = " + UserControl.MutiLanguages.ParserString(spp.STATUS)));
                }
            }
            else
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SOLDERPASTE_NOT_EXIST $Domain_SolderPaste_ID = " + txtSolderPasteID.Value.Trim()));

            }

            txtSolderPasteID.TextFocus(false, true);
            //Remove UCLabel.SelectAll;
        }
        /// <summary>
        /// 用完
        /// </summary>
        private void UsedUpSolderPaste()
        {

            object objSP = _facade.GetSolderPaste(txtSolderPasteID.Value.Trim());

            if (objSP != null)
            {
                Domain.SolderPaste.SolderPaste sp = (objSP as Domain.SolderPaste.SolderPaste);

                object objSPP = _facade.GetSOLDERPASTEPROBySPPID(sp.SolderPasteID);

                #region 用完
                if (objSPP != null)
                {
                    Domain.SolderPaste.SOLDERPASTEPRO spp = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;
                    //4.3.8.1	锡膏在使用记录中必须处于开封状态
                    if (spp.STATUS == Web.Helper.SolderPasteStatus.Unveil)
                    {
                        //Laws Lu,2006/11/13 uniform system collect date
                        DBDateTime dbDateTime;

                        dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                        DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);


                        sp.MaintainUser = txtOPUser.Value.Trim();
                        sp.MaintainDate = FormatHelper.TODateInt(workDateTime);
                        sp.MaintainTime = FormatHelper.TOTimeInt(workDateTime);
                        sp.Status = Web.Helper.SolderPasteStatus.UsedUp;

                        spp.MUSER = sp.MaintainUser;
                        spp.MDATE = dbDateTime.DBDate;
                        spp.MTIME = dbDateTime.DBTime;

                        string unveilTime = FormatHelper.TODateTimeString(spp.UNVEILMDATE, spp.UNVEILTIME);
                        TimeSpan tsUnveilDate = workDateTime - DateTime.Parse(unveilTime);//开封计时 = 当前时间 - 开封时间

                        spp.UNVEILCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsUnveilDate.TotalHours, 2));

                        DataProvider.BeginTransaction();
                        try
                        {
                            spp.STATUS = sp.Status;

                            _facade.UpdateSOLDERPASTEPRO(spp);

                            _facade.UpdateSolderPaste(sp);

                            DataProvider.CommitTransaction();

                            ApplicationRun.GetInfoForm().Add(
                                new UserControl.Message(MessageType.Success
                                , "$CS_SP_RUNOUT_SUCCESS  $Domain_SolderPaste_ID = " + spp.SOLDERPASTEID));

                            ClearInputControl();
                        }
                        catch (Exception ex)
                        {
                            DataProvider.RollbackTransaction();
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));
                        }
                        finally
                        {
                            //							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
                            //								.PersistBroker.CloseConnection();
                        }
                    }
                    else
                    {
                        ApplicationRun.GetInfoForm().Add(
                            new UserControl.Message(MessageType.Error
                            , "$CS_STATUS_WRONG  $Domain_SolderPaste_ID = " + spp.SOLDERPASTEID
                            + " $Current_Status = " + UserControl.MutiLanguages.ParserString(spp.STATUS)));
                    }
                }
                else
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SOLDERPASTE_NOT_EXIST"));

                }
                #endregion
            }
            else
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SOLDERPASTE_NOT_EXIST"));
            }

            txtSolderPasteID.TextFocus(false, true);
            //Remove UCLabel.SelectAll;

        }

        /// <summary>
        /// 作废
        /// </summary>
        private void UnavialSolderPaste()
        {

            object objSP = _facade.GetSolderPaste(txtSolderPasteID.Value.Trim());

            if (objSP != null)
            {
                if (DialogResult.No == MessageBox.Show(this, UserControl.MutiLanguages.ParserMessage("$CS_CONFRIM_UNAVIAL"), MutiLanguages.ParserString("$ShowConfirm"), MessageBoxButtons.YesNo))
                {
                    return;
                }

                Domain.SolderPaste.SolderPaste sp = (objSP as Domain.SolderPaste.SolderPaste);
                object objSPP = _facade.GetSOLDERPASTEPROBySPPID(txtSolderPasteID.Value.Trim());

                #region 报废
                if (objSPP != null)
                {
                    Domain.SolderPaste.SOLDERPASTEPRO spp = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;

                    //4.3.11.4	除用完、报废状态的锡膏外都可以执行报废
                    if (spp.STATUS != Web.Helper.SolderPasteStatus.UsedUp
                        && spp.STATUS != Web.Helper.SolderPasteStatus.scrap)
                    {
                        //Laws Lu,2006/11/13 uniform system collect date
                        DBDateTime dbDateTime;

                        dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                        DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);


                        sp.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;
                        sp.MaintainDate = FormatHelper.TODateInt(workDateTime);
                        sp.MaintainTime = FormatHelper.TOTimeInt(workDateTime);
                        sp.Status = Web.Helper.SolderPasteStatus.scrap;

                        spp.UNAVIALUSER = txtOPUser.Value.Trim();
                        spp.UNAVIALDATE = FormatHelper.TODateInt(workDateTime);
                        spp.UNAVIALTIME = FormatHelper.TOTimeInt(workDateTime);

                        //4.3.7.4	对于需要更新的使用记录，变更其状态为报废，
                        //记录报废人员、日期、时间（可以使用MUser、Mdate、Mtime栏位，不需要单独记录）
                        spp.MUSER = sp.MaintainUser;
                        spp.MDATE = dbDateTime.DBDate;
                        spp.MTIME = dbDateTime.DBTime;

                        //如果锡膏处于回温状态则停止回温计时和未开封计时，
                        //并记录回温计时和未开封计时；如果锡膏处于搅拌状态则停止未开封计时，
                        //并记录未开封计时；如果锡膏处于开封状态则停止开封计时，记录开封计时
                        if (spp.STATUS == Web.Helper.SolderPasteStatus.Unveil)
                        {
                            string unveilTime = FormatHelper.TODateTimeString(spp.UNVEILMDATE, spp.UNVEILTIME);
                            TimeSpan tsUnveilDate = workDateTime - DateTime.Parse(unveilTime);//开封计时 = 当前时间 - 开封时间

                            spp.UNVEILCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsUnveilDate.TotalHours, 2));
                        }

                        if (spp.STATUS == Web.Helper.SolderPasteStatus.Return
                            || spp.STATUS == Web.Helper.SolderPasteStatus.Agitate)
                        {
                            string openTime = FormatHelper.TODateTimeString(spp.RETURNDATE, spp.RETURNTIME);
                            TimeSpan tsVeilDate = workDateTime - DateTime.Parse(openTime);//未开封计时 = 当前时间 - 回温时间
                            //Laws Lu,2006/08/23 修改
                            //一个锡膏ID当前处于回温状态，报废的时候，BS查询出来的回温计时为空，
                            if (spp.STATUS == Web.Helper.SolderPasteStatus.Return)
                            {
                                spp.RETURNCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsVeilDate.TotalHours, 2));
                            }
                            spp.VEILCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsVeilDate.TotalHours, 2));
                        }


                        DataProvider.BeginTransaction();
                        try
                        {

                            spp.STATUS = sp.Status;

                            _facade.UpdateSOLDERPASTEPRO(spp);

                            _facade.UpdateSolderPaste(sp);

                            DataProvider.CommitTransaction();

                            ApplicationRun.GetInfoForm().Add(
                                new UserControl.Message(MessageType.Success
                                , "$CS_SP_UNAVIAL_SUCCESS  $Domain_SolderPaste_ID = " + spp.SOLDERPASTEID));

                            ClearInputControl();
                        }
                        catch (Exception ex)
                        {
                            DataProvider.RollbackTransaction();

                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));
                        }
                        finally
                        {
                            //							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
                            //								.PersistBroker.CloseConnection();
                        }
                    }
                    else
                    {
                        ApplicationRun.GetInfoForm().Add(
                            new UserControl.Message(MessageType.Error
                            , "$CS_STATUS_WRONG  $Domain_SolderPaste_ID = " + spp.SOLDERPASTEID
                            + "$Current_Status = " + UserControl.MutiLanguages.ParserString(spp.STATUS)));
                    }
                }
                else
                {
                    //4.3.7.4	对于需要更新的使用记录，变更其状态为报废，
                    //记录报废人员、日期、时间（可以使用MUser、Mdate、Mtime栏位，不需要单独记录）
                    if (sp.Status != Web.Helper.SolderPasteStatus.UsedUp
                        && sp.Status != Web.Helper.SolderPasteStatus.scrap)
                    {
                        //Laws Lu,2006/11/13 uniform system collect date
                        DBDateTime dbDateTime;

                        dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                        DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);


                        sp.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;
                        sp.MaintainDate = FormatHelper.TODateInt(workDateTime);
                        sp.MaintainTime = FormatHelper.TOTimeInt(workDateTime);
                        sp.Status = Web.Helper.SolderPasteStatus.scrap;


                        _facade.UpdateSolderPaste(sp);

                        ApplicationRun.GetInfoForm().Add(
                            new UserControl.Message(MessageType.Success
                            , "$CS_SP_UNAVIAL_SUCCESS  $Domain_SolderPaste_ID = " + sp.SolderPasteID));
                    }
                    //ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CS_SOLDERPASTE_NOT_EXIST"));
                }
                #endregion
            }
            else
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SOLDERPASTE_NOT_EXIST"));
            }

            txtSolderPasteID.TextFocus(false, true);
            //Remove UCLabel.SelectAll;

        }
        /// <summary>
        /// 开封
        /// </summary>
        private void UnveilSolderPaste()
        {
            #region Unveil Process

            object objSPP = _facade.GetSOLDERPASTEPROBySPPID(txtSolderPasteID.Value.Trim());

            if (objSPP != null)
            {
                Domain.SolderPaste.SOLDERPASTEPRO spp = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;

                if (spp.STATUS == Web.Helper.SolderPasteStatus.Agitate)
                {
                    //Laws Lu,2006/11/13 uniform system collect date
                    DBDateTime dbDateTime;

                    dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);



                    DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);


                    //Laws Lu,2006/08/23 modify
                    //系统记录的搅拌人员和开封人员不正确；
                    spp.UNVEILUSER = txtOPUser.Value.Trim();
                    spp.UNVEILMDATE = FormatHelper.TODateInt(workDateTime);
                    spp.UNVEILTIME = FormatHelper.TOTimeInt(workDateTime);

                    TimeSpan tsVeilDate = workDateTime - DateTime.Parse(FormatHelper.TODateTimeString(spp.RETURNDATE, spp.RETURNTIME));//未开封计时 = 当前时间 - 搅拌时间

                    spp.VEILCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsVeilDate.TotalHours, 2));

                    spp.MUSER = ApplicationService.Current().LoginInfo.UserCode;
                    spp.MDATE = FormatHelper.TODateInt(workDateTime);
                    spp.MTIME = FormatHelper.TOTimeInt(workDateTime);

                    DataProvider.BeginTransaction();

                    try
                    {
                        spp.STATUS = Web.Helper.SolderPasteStatus.Unveil;

                        _facade.UpdateSOLDERPASTEPRO(spp);

                        DataProvider.CommitTransaction();

                        //btnSearch_Click(null,null);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                        DataProvider.RollbackTransaction();
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));
                    }
                    finally
                    {
                        //						((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    }

                }
                else
                {
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error
                        , "$CS_STATUS_WRONG  $Domain_SolderPaste_ID = " + spp.SOLDERPASTEID
                        + "$Current_Status = " + UserControl.MutiLanguages.ParserString(spp.STATUS)));
                }
            }
            else
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SOLDERPASTE_NOT_EXIST"));
            }


            txtSolderPasteID.TextFocus(false, true);
            //Remove UCLabel.SelectAll;


            #endregion
        }
        /// <summary>
        /// 搅拌
        /// </summary>
        private void AgitateSolderPaste()
        {
            #region Agitate Process

            object objSPP = _facade.GetSOLDERPASTEPROBySPPID(txtSolderPasteID.Value.Trim());

            if (objSPP != null)
            {
                Domain.SolderPaste.SOLDERPASTEPRO spp = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;

                if (spp.STATUS == Web.Helper.SolderPasteStatus.Return && spp.OPENUSER != String.Empty)
                {
                    //Laws Lu,2006/11/13 uniform system collect date
                    DBDateTime dbDateTime;

                    dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                    DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

                    //Laws Lu,2006/08/23 modify
                    //系统记录的搅拌人员和开封人员不正确；
                    spp.AGITAEUSER = txtOPUser.Value.Trim();
                    spp.AGITATEDATE = FormatHelper.TODateInt(workDateTime);
                    spp.AGITATETIME = FormatHelper.TOTimeInt(workDateTime);

                    TimeSpan tsOpenDate = workDateTime - DateTime.Parse(FormatHelper.TODateTimeString(spp.RETURNDATE, spp.RETURNTIME));//回温计时 = 当前时间 - 回温时间

                    spp.RETURNCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsOpenDate.TotalHours, 2));

                    if (spp.RETURNTIMESPAN > spp.RETURNCOUNTTIME)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_NOT_REACH_RETURNTIME"));
                        txtSolderPasteID.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;

                        return;
                    }

                    spp.MUSER = ApplicationService.Current().LoginInfo.UserCode;
                    spp.MDATE = FormatHelper.TODateInt(workDateTime);
                    spp.MTIME = FormatHelper.TOTimeInt(workDateTime);

                    DataProvider.BeginTransaction();

                    try
                    {
                        spp.STATUS = Web.Helper.SolderPasteStatus.Agitate;

                        _facade.UpdateSOLDERPASTEPRO(spp);


                        DataProvider.CommitTransaction();

                        //btnSearch_Click(null,null);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                        DataProvider.RollbackTransaction();
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));
                    }
                    finally
                    {
                        //						((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    }
                }
                else
                {
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error
                        , "$CS_STATUS_WRONG  $Domain_SolderPaste_ID = " + spp.SOLDERPASTEID
                        + "$Current_Status = " + UserControl.MutiLanguages.ParserString(spp.STATUS)));
                }
            }
            else
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SOLDERPASTE_NOT_EXIST"));
            }

            txtSolderPasteID.TextFocus(false, true);
            //Remove UCLabel.SelectAll;

            #endregion
        }
        /// <summary>
        /// 转换工单
        /// </summary>
        private void TransferMo()
        {
            //4.3.6.1	必须输入工单和产线
            if (txtDestinationMo.Value.Trim() == String.Empty)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_CMPleaseInputMO"));

                Application.DoEvents();
                txtDestinationMo.TextFocus(false, true);
                //Remove UCLabel.SelectAll;

                return;
            }

            if (txtDestinationLine.Value.Trim() == String.Empty)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_SSCode_NotExist"));

                Application.DoEvents();
                txtDestinationLine.TextFocus(false, true);
                //Remove UCLabel.SelectAll;

                return;
            }
            //Laws Lu，2007/03/09	检查工单
            if (!ShowItem(txtDestinationMo.Value.Trim()))
            {
                return;
            }
            #region 转换工单

            //4.3.6.2	锡膏必须已经被领用到某张工单、产线下
            object objSPP = _facade.GetSOLDERPASTEPROBySPPID(txtSolderPasteID.Value.Trim());

            if (objSPP != null)
            {
                Domain.SolderPaste.SOLDERPASTEPRO spp = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;

                //4.3.6.2	锡膏必须已经被领用到某张工单、产线下
                if (spp.MOCODE == String.Empty && String.Empty == spp.LINECODE)
                {
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error
                        , "$CS_SP_NOT_OPEN $Domain_SolderPaste_ID = " + txtSolderPasteID.Value.Trim()));

                    txtSolderPasteID.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;

                    return;

                }

                //4.3.6.3	新旧产线和新旧工单编号不能同时一致，旧工单和旧产线是指锡膏的最新使用记录中的工单和产线
                if (txtDestinationMo.Value.Trim() == spp.MOCODE)
                {
                    //工单相同，新旧产线编号不能一致
                    if (txtDestinationLine.Value.Trim() == spp.LINECODE)
                    {
                        ApplicationRun.GetInfoForm().Add(
                            new UserControl.Message(MessageType.Error
                            , "$CS_SP_TRANSMO_LINE_REPEAT $Domain_SolderPaste_ID = " + txtSolderPasteID.Value.Trim()));

                        //ClearInputControl();

                        txtDestinationLine.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;

                        return;
                    }
                }
                //4.3.6.3	新旧产线和新旧工单编号不能同时一致，旧工单和旧产线是指锡膏的最新使用记录中的工单和产线
                if (txtDestinationLine.Value.Trim() == spp.LINECODE)
                {
                    //产线相同，新旧工单编号不能一致
                    if (txtDestinationMo.Value.Trim() == spp.MOCODE)
                    {
                        ApplicationRun.GetInfoForm().Add(
                            new UserControl.Message(MessageType.Error
                            , "$CS_SP_TRANSMO_MO_REPEAT $Domain_SolderPaste_ID = " + txtSolderPasteID.Value.Trim()));

                        //ClearInputControl();

                        txtMocode.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;

                        return;
                    }


                }

                //4.3.6.4	锡膏的最新使用记录中不能是回存、限制使用、报废、用完这四种状态中的任何一种
                if (spp.STATUS == Web.Helper.SolderPasteStatus.Reflow
                    || spp.STATUS == Web.Helper.SolderPasteStatus.Restrain
                    || spp.STATUS == Web.Helper.SolderPasteStatus.scrap
                    || spp.STATUS == Web.Helper.SolderPasteStatus.UsedUp)
                {
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error
                        , "$CS_STATUS_WRONG $Domain_SolderPaste_ID = " + spp.SOLDERPASTEID
                    + " $Current_Status = " + MutiLanguages.ParserString(spp.STATUS)));

                }
                else
                {
                    //Laws Lu,2006/11/13 uniform system collect date
                    DBDateTime dbDateTime;

                    dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                    DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);


                    //更新锡膏使用记录

                    spp.MUSER = ApplicationService.Current().LoginInfo.UserCode;
                    spp.MDATE = FormatHelper.TODateInt(workDateTime);
                    spp.MTIME = FormatHelper.TOTimeInt(workDateTime);

                    DataProvider.BeginTransaction();
                    try
                    {
                        //4.3.6.5	通过检查后，复制该锡膏编号的最新使用记录，
                        //并更新其中的工单和产线信息为转换后的工单和产线，
                        //记录备注信息为“工单转换”，其他信息不变。
                        //回温计时或未开封计时或开封计时在新的使用记录中持续累计
                        string originalID = spp.SPPKID;
                        string originalMO = spp.MOCODE;
                        string originalLine = spp.LINECODE;

                        spp.SPPKID = System.Guid.NewGuid().ToString();
                        spp.MOCODE = txtDestinationMo.Value.Trim(); ;
                        spp.LINECODE = txtDestinationLine.Value.Trim();

                        spp.RESAVEUSER = String.Empty;
                        spp.RESAVEDATE = 0;
                        spp.RESAVETIME = 0;
                        spp.SEQUENCE = spp.SEQUENCE + 1;

                        spp.MEMO = MutiLanguages.ParserString("$CS_MOSwitch");//"工单转换";

                        _facade.AddSOLDERPASTEPRO(spp);//添加新的使用记录

                        spp.SPPKID = originalID;

                        spp.SEQUENCE = spp.SEQUENCE - 1;

                        spp.MOCODE = originalMO;
                        spp.LINECODE = originalLine;

                        spp.RESAVEUSER = spp.MUSER;
                        spp.RESAVEDATE = spp.MDATE;
                        spp.RESAVETIME = spp.MTIME;

                        spp.STATUS = Web.Helper.SolderPasteStatus.Reflow;



                        _facade.UpdateSOLDERPASTEPRO(spp);//更新原有使用记录


                        DataProvider.CommitTransaction();

                        ApplicationRun.GetInfoForm().Add(
                            new UserControl.Message(MessageType.Success
                            , "$CS_SP_TRANSMO_SUCCESS $Domain_SolderPaste_ID = " + spp.SOLDERPASTEID
                            + " $Domain_MO = " + txtDestinationMo.Value.Trim()));

                        ClearInputControl();
                    }
                    catch (Exception ex)
                    {
                        DataProvider.RollbackTransaction();
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));
                    }
                    finally
                    {
                        //						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider)
                        //							.PersistBroker.CloseConnection();
                    }
                }

            }
            else
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SOLDERPASTE_NOT_EXIST $Domain_SolderPaste_ID = " + txtSolderPasteID.Value.Trim()));

            }

            #endregion

            txtSolderPasteID.TextFocus(false, true);
            //Remove UCLabel.SelectAll;
        }


        private void radGroup_Click(object sender, System.EventArgs e)
        {
            txtOPUser.TextFocus(false, true);
            //Remove UCLabel.SelectAll;
        }

        private void FSPControl_Load(object sender, System.EventArgs e)
        {
            txtOPUser.Text = ApplicationService.Current().UserCode;

            btnSearch_Click(sender, e);
            //this.InitPageLanguage();
        }

        private void ClearInputControl()
        {
            txtSolderPasteID.Value = String.Empty;
            txtStatus.Value = String.Empty;
            txtMemo.Value = String.Empty;

            txtDestinationLine.Value = String.Empty;
            txtDestinationMo.Value = String.Empty;
        }

        private void txtSolderPasteID_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if ((ir != null && ir.IsCompleted) || ir == null)
                {
                    //Laws Lu,2007/01/05,新增	缓解性能问题
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                    object objSPP = _facade.GetSOLDERPASTEPROBySPPID(txtSolderPasteID.Value.Trim());

                    if (objSPP != null)
                    {
                        Domain.SolderPaste.SOLDERPASTEPRO spp = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;

                        txtStatus.Value = UserControl.MutiLanguages.ParserString(spp.STATUS);
                        txtMemo.Value = spp.MEMO;
                    }
                    else
                    {
                        object objSP = _facade.GetSolderPaste(txtSolderPasteID.Value.Trim());

                        if (objSP != null)
                        {
                            Domain.SolderPaste.SolderPaste sp = objSP as Domain.SolderPaste.SolderPaste;

                            txtStatus.Value = UserControl.MutiLanguages.ParserString(sp.Status);

                            txtMemo.Value = sp.eAttribute1;
                        }
                    }

                    //Laws Lu,2007/01/05,新增	缓解性能问题
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;

                    Application.DoEvents();
                    txtSolderPasteID.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                }
            }
        }

        private void txtOPUser_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Application.DoEvents();
                if (radOpen.Checked || radTransferMo.Checked)
                {
                    txtDestinationMo.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                }
                else
                {
                    txtSolderPasteID.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                }
            }
        }

        private void txtDestinationLine_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Application.DoEvents();

                txtSolderPasteID.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
            }
        }

        private void txtDestinationMo_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Application.DoEvents();

                txtDestinationLine.TextFocus(false, true);
                //Remove UCLabel.SelectAll;

                ShowItem(txtDestinationMo.Value.Trim());
            }

        }

        private void txtMocode_InnerTextChanged(object sender, System.EventArgs e)
        {
            string moCode = txtMocode.Value.ToUpper().Trim();
            if (moCode != String.Empty)
            {
                ShowItem(moCode, true);//show the item code by mocode 
            }
            else
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_CMPleaseInputMO"));

                Application.DoEvents();
                txtMocode.TextFocus(false, true);
            }
        }

        private void txtDestinationMo_InnerTextChanged(object sender, System.EventArgs e)
        {
            Application.DoEvents();

            txtDestinationLine.TextFocus(false, true);
            //Remove UCLabel.SelectAll;

            ShowItem(txtDestinationMo.Value.Trim());
        }

        private void FSPControl_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Laws Lu,2007/01/05,新增	缓解性能问题
            try
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
            }
            catch
            { }
        }

    }

}
