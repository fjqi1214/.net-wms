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
using BenQGuru.eMES.BaseSetting;


namespace BenQGuru.eMES.Client
{
    /// <summary>
    /// FSMTLoadFeeder 的摘要说明。
    /// </summary>
    public class FSMTLoadFeeder : BaseForm
    {
        [System.Runtime.InteropServices.DllImport("winmm.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        public static extern bool PlaySound(string szSound, int hMod, int flags);
        private int SND_FILENAME = 0x00020000;

        private System.Windows.Forms.Panel panel1;
        private UserControl.UCLabelEdit txtMOCode;
        private UserControl.UCLabelEdit txtMachineCode;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdoLoad;
        private System.Windows.Forms.RadioButton rdoExchanges;
        private UserControl.UCButton ucBtnExit;
        private UserControl.UCButton btnOK;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridList;
        private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
        private UserControl.UCButton btnOKMachine;
        private UserControl.UCLabelEdit txtInput;
        private UserControl.UCLabelEdit txtItem;
        private System.Windows.Forms.TextBox txtMachineLoadedCount;
        private System.Windows.Forms.Label lblLoadedCount;
        private System.Windows.Forms.RadioButton rdoContinue;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ListBox lstReelAcceptStationCode;
        private System.Windows.Forms.Label lblReelAcceptStationCode;
        private System.Windows.Forms.ListBox lstReelAcceptFeeder;
        private System.Windows.Forms.Label lblReelAcceptFeeder;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListBox lstAlertMusic;
        private System.Windows.Forms.Label lblAlertMusic;
        private UserControl.UCMessage ucMessageInfo;
        private UserControl.UCButton btnUnloadAll;
        private System.Windows.Forms.RadioButton rdoReturn;
        private UserControl.UCButton btnDisable;
        private UserControl.UCButton btnEnable;
        private UserControl.UCLabelEdit txtUserCode;
        private UserControl.UCLabelEdit txtMOStatus;
        private System.Windows.Forms.RadioButton rdoCheck;
        private UserControl.UCLabelCombox cboStationTable;
        private UserControl.UCLabelEdit txtTableStatus;
        private UserControl.UCButton btnActive;
        private UserControl.UCButton btnInactive;
        private IContainer components;

        /// <summary>
        /// 是否管控feeder,在参数中控制
        /// </summary>
        protected bool needFeeder = true;
        public FSMTLoadFeeder()
        {
            //GetParameterAlias
            SystemSettingFacade ssfacade = new SystemSettingFacade(this.DataProvider);
            string strNeedFeeder = ssfacade.GetParameterAlias("", "");
            if (strNeedFeeder.ToUpper() == "N")
            {
                needFeeder = false;
            }
            InitializeComponent();
            InitGridColumn();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSMTLoadFeeder));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnInactive = new UserControl.UCButton();
            this.btnActive = new UserControl.UCButton();
            this.txtTableStatus = new UserControl.UCLabelEdit();
            this.cboStationTable = new UserControl.UCLabelCombox();
            this.txtMOStatus = new UserControl.UCLabelEdit();
            this.txtItem = new UserControl.UCLabelEdit();
            this.txtMachineCode = new UserControl.UCLabelEdit();
            this.txtMOCode = new UserControl.UCLabelEdit();
            this.btnOKMachine = new UserControl.UCButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdoCheck = new System.Windows.Forms.RadioButton();
            this.txtUserCode = new UserControl.UCLabelEdit();
            this.btnDisable = new UserControl.UCButton();
            this.btnEnable = new UserControl.UCButton();
            this.rdoReturn = new System.Windows.Forms.RadioButton();
            this.btnUnloadAll = new UserControl.UCButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ucMessageInfo = new UserControl.UCMessage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lstReelAcceptStationCode = new System.Windows.Forms.ListBox();
            this.lblReelAcceptStationCode = new System.Windows.Forms.Label();
            this.lstReelAcceptFeeder = new System.Windows.Forms.ListBox();
            this.lblReelAcceptFeeder = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lstAlertMusic = new System.Windows.Forms.ListBox();
            this.lblAlertMusic = new System.Windows.Forms.Label();
            this.rdoContinue = new System.Windows.Forms.RadioButton();
            this.txtMachineLoadedCount = new System.Windows.Forms.TextBox();
            this.lblLoadedCount = new System.Windows.Forms.Label();
            this.ucBtnExit = new UserControl.UCButton();
            this.btnOK = new UserControl.UCButton();
            this.rdoExchanges = new System.Windows.Forms.RadioButton();
            this.rdoLoad = new System.Windows.Forms.RadioButton();
            this.txtInput = new UserControl.UCLabelEdit();
            this.gridList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnInactive);
            this.panel1.Controls.Add(this.btnActive);
            this.panel1.Controls.Add(this.txtTableStatus);
            this.panel1.Controls.Add(this.cboStationTable);
            this.panel1.Controls.Add(this.txtMOStatus);
            this.panel1.Controls.Add(this.txtItem);
            this.panel1.Controls.Add(this.txtMachineCode);
            this.panel1.Controls.Add(this.txtMOCode);
            this.panel1.Controls.Add(this.btnOKMachine);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(895, 87);
            this.panel1.TabIndex = 0;
            // 
            // btnInactive
            // 
            this.btnInactive.BackColor = System.Drawing.SystemColors.Control;
            this.btnInactive.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInactive.BackgroundImage")));
            this.btnInactive.ButtonType = UserControl.ButtonTypes.None;
            this.btnInactive.Caption = "停用";
            this.btnInactive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInactive.Location = new System.Drawing.Point(545, 45);
            this.btnInactive.Name = "btnInactive";
            this.btnInactive.Size = new System.Drawing.Size(88, 22);
            this.btnInactive.TabIndex = 26;
            this.btnInactive.TabStop = false;
            this.btnInactive.Click += new System.EventHandler(this.btnInactive_Click);
            // 
            // btnActive
            // 
            this.btnActive.BackColor = System.Drawing.SystemColors.Control;
            this.btnActive.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnActive.BackgroundImage")));
            this.btnActive.ButtonType = UserControl.ButtonTypes.None;
            this.btnActive.Caption = "使用";
            this.btnActive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActive.Location = new System.Drawing.Point(417, 45);
            this.btnActive.Name = "btnActive";
            this.btnActive.Size = new System.Drawing.Size(88, 22);
            this.btnActive.TabIndex = 25;
            this.btnActive.TabStop = false;
            this.btnActive.Load += new System.EventHandler(this.btnActive_Load);
            this.btnActive.Click += new System.EventHandler(this.btnActive_Click);
            // 
            // txtTableStatus
            // 
            this.txtTableStatus.AllowEditOnlyChecked = true;
            this.txtTableStatus.AutoSelectAll = false;
            this.txtTableStatus.AutoUpper = true;
            this.txtTableStatus.Caption = "状态";
            this.txtTableStatus.Checked = false;
            this.txtTableStatus.EditType = UserControl.EditTypes.String;
            this.txtTableStatus.Location = new System.Drawing.Point(221, 43);
            this.txtTableStatus.MaxLength = 40;
            this.txtTableStatus.Multiline = false;
            this.txtTableStatus.Name = "txtTableStatus";
            this.txtTableStatus.PasswordChar = '\0';
            this.txtTableStatus.ReadOnly = true;
            this.txtTableStatus.ShowCheckBox = false;
            this.txtTableStatus.Size = new System.Drawing.Size(170, 24);
            this.txtTableStatus.TabIndex = 24;
            this.txtTableStatus.TabNext = false;
            this.txtTableStatus.Value = "";
            this.txtTableStatus.WidthType = UserControl.WidthTypes.Normal;
            this.txtTableStatus.XAlign = 258;
            // 
            // cboStationTable
            // 
            this.cboStationTable.AllowEditOnlyChecked = true;
            this.cboStationTable.Caption = "Table";
            this.cboStationTable.Checked = false;
            this.cboStationTable.Location = new System.Drawing.Point(28, 45);
            this.cboStationTable.Name = "cboStationTable";
            this.cboStationTable.SelectedIndex = -1;
            this.cboStationTable.ShowCheckBox = false;
            this.cboStationTable.Size = new System.Drawing.Size(176, 24);
            this.cboStationTable.TabIndex = 23;
            this.cboStationTable.WidthType = UserControl.WidthTypes.Normal;
            this.cboStationTable.XAlign = 71;
            this.cboStationTable.SelectedIndexChanged += new System.EventHandler(this.cboStationTable_SelectedIndexChanged);
            // 
            // txtMOStatus
            // 
            this.txtMOStatus.AllowEditOnlyChecked = true;
            this.txtMOStatus.AutoSelectAll = false;
            this.txtMOStatus.AutoUpper = true;
            this.txtMOStatus.Caption = "工单状态";
            this.txtMOStatus.Checked = false;
            this.txtMOStatus.EditType = UserControl.EditTypes.String;
            this.txtMOStatus.Location = new System.Drawing.Point(402, 15);
            this.txtMOStatus.MaxLength = 40;
            this.txtMOStatus.Multiline = false;
            this.txtMOStatus.Name = "txtMOStatus";
            this.txtMOStatus.PasswordChar = '\0';
            this.txtMOStatus.ReadOnly = true;
            this.txtMOStatus.ShowCheckBox = false;
            this.txtMOStatus.Size = new System.Drawing.Size(111, 24);
            this.txtMOStatus.TabIndex = 19;
            this.txtMOStatus.TabNext = false;
            this.txtMOStatus.Value = "";
            this.txtMOStatus.WidthType = UserControl.WidthTypes.Tiny;
            this.txtMOStatus.XAlign = 463;
            // 
            // txtItem
            // 
            this.txtItem.AllowEditOnlyChecked = true;
            this.txtItem.AutoSelectAll = false;
            this.txtItem.AutoUpper = true;
            this.txtItem.Caption = "产品";
            this.txtItem.Checked = false;
            this.txtItem.EditType = UserControl.EditTypes.String;
            this.txtItem.Location = new System.Drawing.Point(221, 15);
            this.txtItem.MaxLength = 40;
            this.txtItem.Multiline = false;
            this.txtItem.Name = "txtItem";
            this.txtItem.PasswordChar = '\0';
            this.txtItem.ReadOnly = true;
            this.txtItem.ShowCheckBox = false;
            this.txtItem.Size = new System.Drawing.Size(170, 24);
            this.txtItem.TabIndex = 18;
            this.txtItem.TabNext = false;
            this.txtItem.Value = "";
            this.txtItem.WidthType = UserControl.WidthTypes.Normal;
            this.txtItem.XAlign = 258;
            // 
            // txtMachineCode
            // 
            this.txtMachineCode.AllowEditOnlyChecked = true;
            this.txtMachineCode.AutoSelectAll = false;
            this.txtMachineCode.AutoUpper = true;
            this.txtMachineCode.Caption = "机台编号";
            this.txtMachineCode.Checked = false;
            this.txtMachineCode.EditType = UserControl.EditTypes.String;
            this.txtMachineCode.Location = new System.Drawing.Point(530, 15);
            this.txtMachineCode.MaxLength = 40;
            this.txtMachineCode.Multiline = false;
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.PasswordChar = '\0';
            this.txtMachineCode.ReadOnly = false;
            this.txtMachineCode.ShowCheckBox = false;
            this.txtMachineCode.Size = new System.Drawing.Size(194, 24);
            this.txtMachineCode.TabIndex = 10;
            this.txtMachineCode.TabNext = false;
            this.txtMachineCode.Value = "";
            this.txtMachineCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtMachineCode.XAlign = 591;
            this.txtMachineCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMOCode_TxtboxKeyPress);
            // 
            // txtMOCode
            // 
            this.txtMOCode.AllowEditOnlyChecked = true;
            this.txtMOCode.AutoSelectAll = false;
            this.txtMOCode.AutoUpper = true;
            this.txtMOCode.Caption = "工单代码";
            this.txtMOCode.Checked = false;
            this.txtMOCode.EditType = UserControl.EditTypes.String;
            this.txtMOCode.Location = new System.Drawing.Point(11, 15);
            this.txtMOCode.MaxLength = 40;
            this.txtMOCode.Multiline = false;
            this.txtMOCode.Name = "txtMOCode";
            this.txtMOCode.PasswordChar = '\0';
            this.txtMOCode.ReadOnly = false;
            this.txtMOCode.ShowCheckBox = false;
            this.txtMOCode.Size = new System.Drawing.Size(194, 24);
            this.txtMOCode.TabIndex = 9;
            this.txtMOCode.TabNext = false;
            this.txtMOCode.Value = "";
            this.txtMOCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtMOCode.XAlign = 72;
            this.txtMOCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMOCode_TxtboxKeyPress);
            // 
            // btnOKMachine
            // 
            this.btnOKMachine.BackColor = System.Drawing.SystemColors.Control;
            this.btnOKMachine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMachine.BackgroundImage")));
            this.btnOKMachine.ButtonType = UserControl.ButtonTypes.None;
            this.btnOKMachine.Caption = "确定";
            this.btnOKMachine.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOKMachine.Location = new System.Drawing.Point(730, 17);
            this.btnOKMachine.Name = "btnOKMachine";
            this.btnOKMachine.Size = new System.Drawing.Size(88, 22);
            this.btnOKMachine.TabIndex = 17;
            this.btnOKMachine.TabStop = false;
            this.btnOKMachine.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdoCheck);
            this.panel2.Controls.Add(this.txtUserCode);
            this.panel2.Controls.Add(this.btnDisable);
            this.panel2.Controls.Add(this.btnEnable);
            this.panel2.Controls.Add(this.rdoReturn);
            this.panel2.Controls.Add(this.btnUnloadAll);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.rdoContinue);
            this.panel2.Controls.Add(this.txtMachineLoadedCount);
            this.panel2.Controls.Add(this.lblLoadedCount);
            this.panel2.Controls.Add(this.ucBtnExit);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.rdoExchanges);
            this.panel2.Controls.Add(this.rdoLoad);
            this.panel2.Controls.Add(this.txtInput);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 299);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(895, 267);
            this.panel2.TabIndex = 1;
            // 
            // rdoCheck
            // 
            this.rdoCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoCheck.Location = new System.Drawing.Point(287, 180);
            this.rdoCheck.Name = "rdoCheck";
            this.rdoCheck.Size = new System.Drawing.Size(81, 23);
            this.rdoCheck.TabIndex = 167;
            this.rdoCheck.Text = "上料抽检";
            this.rdoCheck.CheckedChanged += new System.EventHandler(this.rdoLoad_CheckedChanged);
            // 
            // txtUserCode
            // 
            this.txtUserCode.AllowEditOnlyChecked = true;
            this.txtUserCode.AutoSelectAll = false;
            this.txtUserCode.AutoUpper = true;
            this.txtUserCode.Caption = "操作人员";
            this.txtUserCode.Checked = false;
            this.txtUserCode.EditType = UserControl.EditTypes.String;
            this.txtUserCode.Location = new System.Drawing.Point(11, 219);
            this.txtUserCode.MaxLength = 40;
            this.txtUserCode.Multiline = false;
            this.txtUserCode.Name = "txtUserCode";
            this.txtUserCode.PasswordChar = '\0';
            this.txtUserCode.ReadOnly = false;
            this.txtUserCode.ShowCheckBox = false;
            this.txtUserCode.Size = new System.Drawing.Size(161, 24);
            this.txtUserCode.TabIndex = 166;
            this.txtUserCode.TabNext = false;
            this.txtUserCode.Value = "";
            this.txtUserCode.WidthType = UserControl.WidthTypes.Small;
            this.txtUserCode.XAlign = 72;
            // 
            // btnDisable
            // 
            this.btnDisable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDisable.BackColor = System.Drawing.SystemColors.Control;
            this.btnDisable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDisable.BackgroundImage")));
            this.btnDisable.ButtonType = UserControl.ButtonTypes.None;
            this.btnDisable.Caption = "工单失效";
            this.btnDisable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDisable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDisable.Location = new System.Drawing.Point(462, 219);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(88, 22);
            this.btnDisable.TabIndex = 165;
            this.btnDisable.TabStop = false;
            this.btnDisable.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // btnEnable
            // 
            this.btnEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnable.BackColor = System.Drawing.SystemColors.Control;
            this.btnEnable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEnable.BackgroundImage")));
            this.btnEnable.ButtonType = UserControl.ButtonTypes.None;
            this.btnEnable.Caption = "工单生效";
            this.btnEnable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnEnable.Location = new System.Drawing.Point(368, 219);
            this.btnEnable.Name = "btnEnable";
            this.btnEnable.Size = new System.Drawing.Size(88, 22);
            this.btnEnable.TabIndex = 164;
            this.btnEnable.TabStop = false;
            this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // rdoReturn
            // 
            this.rdoReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoReturn.Location = new System.Drawing.Point(235, 180);
            this.rdoReturn.Name = "rdoReturn";
            this.rdoReturn.Size = new System.Drawing.Size(55, 23);
            this.rdoReturn.TabIndex = 163;
            this.rdoReturn.Text = "退料";
            this.rdoReturn.CheckedChanged += new System.EventHandler(this.rdoLoad_CheckedChanged);
            // 
            // btnUnloadAll
            // 
            this.btnUnloadAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUnloadAll.BackColor = System.Drawing.SystemColors.Control;
            this.btnUnloadAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUnloadAll.BackgroundImage")));
            this.btnUnloadAll.ButtonType = UserControl.ButtonTypes.None;
            this.btnUnloadAll.Caption = "全部下料";
            this.btnUnloadAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnloadAll.Location = new System.Drawing.Point(274, 219);
            this.btnUnloadAll.Name = "btnUnloadAll";
            this.btnUnloadAll.Size = new System.Drawing.Size(88, 22);
            this.btnUnloadAll.TabIndex = 162;
            this.btnUnloadAll.TabStop = false;
            this.btnUnloadAll.Click += new System.EventHandler(this.btnUnloadAll_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ucMessageInfo);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(895, 163);
            this.panel3.TabIndex = 161;
            // 
            // ucMessageInfo
            // 
            this.ucMessageInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessageInfo.ButtonVisible = false;
            this.ucMessageInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessageInfo.Location = new System.Drawing.Point(0, 0);
            this.ucMessageInfo.Name = "ucMessageInfo";
            this.ucMessageInfo.Size = new System.Drawing.Size(615, 163);
            this.ucMessageInfo.TabIndex = 164;
            this.ucMessageInfo.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lstReelAcceptStationCode);
            this.panel5.Controls.Add(this.lblReelAcceptStationCode);
            this.panel5.Controls.Add(this.lstReelAcceptFeeder);
            this.panel5.Controls.Add(this.lblReelAcceptFeeder);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(615, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(140, 163);
            this.panel5.TabIndex = 163;
            // 
            // lstReelAcceptStationCode
            // 
            this.lstReelAcceptStationCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstReelAcceptStationCode.ItemHeight = 12;
            this.lstReelAcceptStationCode.Location = new System.Drawing.Point(0, 83);
            this.lstReelAcceptStationCode.Name = "lstReelAcceptStationCode";
            this.lstReelAcceptStationCode.Size = new System.Drawing.Size(140, 80);
            this.lstReelAcceptStationCode.TabIndex = 167;
            // 
            // lblReelAcceptStationCode
            // 
            this.lblReelAcceptStationCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReelAcceptStationCode.Location = new System.Drawing.Point(0, 61);
            this.lblReelAcceptStationCode.Name = "lblReelAcceptStationCode";
            this.lblReelAcceptStationCode.Size = new System.Drawing.Size(140, 22);
            this.lblReelAcceptStationCode.TabIndex = 166;
            this.lblReelAcceptStationCode.Text = "对应站位列表";
            this.lblReelAcceptStationCode.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lstReelAcceptFeeder
            // 
            this.lstReelAcceptFeeder.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstReelAcceptFeeder.ItemHeight = 12;
            this.lstReelAcceptFeeder.Location = new System.Drawing.Point(0, 21);
            this.lstReelAcceptFeeder.Name = "lstReelAcceptFeeder";
            this.lstReelAcceptFeeder.Size = new System.Drawing.Size(140, 40);
            this.lstReelAcceptFeeder.TabIndex = 165;
            // 
            // lblReelAcceptFeeder
            // 
            this.lblReelAcceptFeeder.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReelAcceptFeeder.Location = new System.Drawing.Point(0, 0);
            this.lblReelAcceptFeeder.Name = "lblReelAcceptFeeder";
            this.lblReelAcceptFeeder.Size = new System.Drawing.Size(140, 21);
            this.lblReelAcceptFeeder.TabIndex = 164;
            this.lblReelAcceptFeeder.Text = "对应Feeder规格列表";
            this.lblReelAcceptFeeder.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lstAlertMusic);
            this.panel4.Controls.Add(this.lblAlertMusic);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(755, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(140, 163);
            this.panel4.TabIndex = 162;
            // 
            // lstAlertMusic
            // 
            this.lstAlertMusic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAlertMusic.ItemHeight = 12;
            this.lstAlertMusic.Location = new System.Drawing.Point(0, 21);
            this.lstAlertMusic.Name = "lstAlertMusic";
            this.lstAlertMusic.Size = new System.Drawing.Size(140, 142);
            this.lstAlertMusic.TabIndex = 164;
            // 
            // lblAlertMusic
            // 
            this.lblAlertMusic.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAlertMusic.Location = new System.Drawing.Point(0, 0);
            this.lblAlertMusic.Name = "lblAlertMusic";
            this.lblAlertMusic.Size = new System.Drawing.Size(140, 21);
            this.lblAlertMusic.TabIndex = 163;
            this.lblAlertMusic.Text = "警告提示音";
            this.lblAlertMusic.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // rdoContinue
            // 
            this.rdoContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoContinue.Location = new System.Drawing.Point(180, 180);
            this.rdoContinue.Name = "rdoContinue";
            this.rdoContinue.Size = new System.Drawing.Size(54, 23);
            this.rdoContinue.TabIndex = 160;
            this.rdoContinue.Text = "接料";
            this.rdoContinue.CheckedChanged += new System.EventHandler(this.rdoLoad_CheckedChanged);
            // 
            // txtMachineLoadedCount
            // 
            this.txtMachineLoadedCount.Location = new System.Drawing.Point(715, 182);
            this.txtMachineLoadedCount.Name = "txtMachineLoadedCount";
            this.txtMachineLoadedCount.ReadOnly = true;
            this.txtMachineLoadedCount.Size = new System.Drawing.Size(27, 21);
            this.txtMachineLoadedCount.TabIndex = 159;
            this.txtMachineLoadedCount.Text = "0";
            // 
            // lblLoadedCount
            // 
            this.lblLoadedCount.Location = new System.Drawing.Point(636, 185);
            this.lblLoadedCount.Name = "lblLoadedCount";
            this.lblLoadedCount.Size = new System.Drawing.Size(83, 24);
            this.lblLoadedCount.TabIndex = 158;
            this.lblLoadedCount.Text = "机台缺料总数";
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(556, 219);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 17;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.ButtonType = UserControl.ButtonTypes.None;
            this.btnOK.Caption = "确定";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Location = new System.Drawing.Point(180, 219);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 22);
            this.btnOK.TabIndex = 16;
            this.btnOK.TabStop = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // rdoExchanges
            // 
            this.rdoExchanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoExchanges.AutoSize = true;
            this.rdoExchanges.Location = new System.Drawing.Point(73, 186);
            this.rdoExchanges.Name = "rdoExchanges";
            this.rdoExchanges.Size = new System.Drawing.Size(101, 16);
            this.rdoExchanges.TabIndex = 11;
            this.rdoExchanges.Text = "换料/换Feeder";
            this.rdoExchanges.CheckedChanged += new System.EventHandler(this.rdoLoad_CheckedChanged);
            // 
            // rdoLoad
            // 
            this.rdoLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoLoad.Checked = true;
            this.rdoLoad.Location = new System.Drawing.Point(21, 180);
            this.rdoLoad.Name = "rdoLoad";
            this.rdoLoad.Size = new System.Drawing.Size(54, 23);
            this.rdoLoad.TabIndex = 10;
            this.rdoLoad.TabStop = true;
            this.rdoLoad.Text = "上料";
            this.rdoLoad.CheckedChanged += new System.EventHandler(this.rdoLoad_CheckedChanged);
            // 
            // txtInput
            // 
            this.txtInput.AllowEditOnlyChecked = true;
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInput.AutoSelectAll = false;
            this.txtInput.AutoUpper = true;
            this.txtInput.Caption = "输入框";
            this.txtInput.Checked = false;
            this.txtInput.EditType = UserControl.EditTypes.String;
            this.txtInput.Location = new System.Drawing.Point(375, 180);
            this.txtInput.MaxLength = 40;
            this.txtInput.Multiline = false;
            this.txtInput.Name = "txtInput";
            this.txtInput.PasswordChar = '\0';
            this.txtInput.ReadOnly = false;
            this.txtInput.ShowCheckBox = false;
            this.txtInput.Size = new System.Drawing.Size(249, 24);
            this.txtInput.TabIndex = 9;
            this.txtInput.TabNext = false;
            this.txtInput.Value = "";
            this.txtInput.WidthType = UserControl.WidthTypes.Long;
            this.txtInput.XAlign = 424;
            this.txtInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_TxtboxKeyPress);
            // 
            // gridList
            // 
            this.gridList.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridList.DataSource = this.ultraDataSource1;
            this.gridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridList.Location = new System.Drawing.Point(0, 87);
            this.gridList.Name = "gridList";
            this.gridList.Size = new System.Drawing.Size(895, 212);
            this.gridList.TabIndex = 3;
            // 
            // FSMTLoadFeeder
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(895, 566);
            this.Controls.Add(this.gridList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FSMTLoadFeeder";
            this.Text = "SMT上料";
            this.Load += new System.EventHandler(this.FSMTLoadFeeder_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void InitGridColumn()
        {
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MachineCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MachineStationCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeederCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeederSpecCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeederLeftCount");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReelNo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReelLeftQty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaterialCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LoadUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LoadDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LoadTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NextReelNo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NextReelLeftQty");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn1 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("MachineCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("MachineStationCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn3 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("FeederCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn4 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("FeederSpecCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn5 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("FeederLeftCount");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn6 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("ReelNo");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn7 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("ReelLeftQty");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn8 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("MaterialCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn9 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("LoadUser");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn10 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("LoadDate");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn11 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("LoadTime");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn12 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("NextReelNo");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn13 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("NextReelLeftQty");

            ultraGridColumn1.AutoEdit = false;
            ultraGridColumn1.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn1.Header.Caption = "机台编号";
            ultraGridColumn1.Width = 79;
            ultraGridColumn2.AutoEdit = false;
            ultraGridColumn2.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn2.Header.Caption = "站位";
            ultraGridColumn2.Width = 45;
            ultraGridColumn3.AutoEdit = false;
            ultraGridColumn3.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn3.Header.Caption = "Feeder代码";
            ultraGridColumn4.AutoEdit = false;
            ultraGridColumn4.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn4.Header.Caption = "Feeder规格";
            ultraGridColumn4.Width = 95;
            ultraGridColumn5.AutoEdit = false;
            ultraGridColumn5.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn5.Header.Caption = "Feeder剩余次数";
            ultraGridColumn5.Width = 95;
            ultraGridColumn6.AutoEdit = false;
            ultraGridColumn6.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn6.Header.Caption = "料卷编号";
            ultraGridColumn6.Width = 96;
            ultraGridColumn7.AutoEdit = false;
            ultraGridColumn7.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn7.Header.Caption = "料卷剩余数量";
            ultraGridColumn7.Width = 81;
            ultraGridColumn8.AutoEdit = false;
            ultraGridColumn8.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn8.Header.Caption = "物料代码";
            ultraGridColumn9.AutoEdit = false;
            ultraGridColumn9.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn9.Header.Caption = "上料人员";
            ultraGridColumn9.Width = 56;
            ultraGridColumn10.AutoEdit = false;
            ultraGridColumn10.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn10.Header.Caption = "上料日期";
            ultraGridColumn10.Width = 57;
            ultraGridColumn11.AutoEdit = false;
            ultraGridColumn11.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn11.Header.Caption = "上料时间";
            ultraGridColumn11.Width = 59;
            ultraGridColumn12.AutoEdit = false;
            ultraGridColumn12.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn12.Header.Caption = "下一料卷编号";
            ultraGridColumn12.Width = 96;
            ultraGridColumn13.AutoEdit = false;
            ultraGridColumn13.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn13.Header.Caption = "下一料卷剩余数量";
            ultraGridColumn13.Width = 81;
            ultraGridBand1.Columns.Add(ultraGridColumn1);
            ultraGridBand1.Columns.Add(ultraGridColumn2);
            ultraGridBand1.Columns.Add(ultraGridColumn3);
            ultraGridBand1.Columns.Add(ultraGridColumn4);
            ultraGridBand1.Columns.Add(ultraGridColumn5);
            ultraGridBand1.Columns.Add(ultraGridColumn6);
            ultraGridBand1.Columns.Add(ultraGridColumn7);
            ultraGridBand1.Columns.Add(ultraGridColumn8);
            ultraGridBand1.Columns.Add(ultraGridColumn9);
            ultraGridBand1.Columns.Add(ultraGridColumn10);
            ultraGridBand1.Columns.Add(ultraGridColumn11);
            ultraGridBand1.Columns.Add(ultraGridColumn12);
            ultraGridBand1.Columns.Add(ultraGridColumn13);
            this.gridList.DisplayLayout.BandsSerializer.Add(ultraGridBand1);

            // 
            // ultraDataSource1
            // 
            this.ultraDataSource1.Band.Columns.AddRange(new object[] {
																		 ultraDataColumn1,
																		 ultraDataColumn2,
																		 ultraDataColumn3,
																		 ultraDataColumn4,
																		 ultraDataColumn5,
																		 ultraDataColumn6,
																		 ultraDataColumn7,
																		 ultraDataColumn8,
																		 ultraDataColumn9,
																		 ultraDataColumn10,
																		 ultraDataColumn11,
                                                                         ultraDataColumn12,
																		 ultraDataColumn13});
        }

        private Hashtable inputFlow = null;
        private Hashtable listInput = null;
        private void InitFlowData()
        {
            inputFlow = new Hashtable();
            Hashtable htLoad = new Hashtable();
            htLoad.Add(SMTLoadFeederInputType.Init, new string[] { SMTLoadFeederInputType.ReelNo, "$Please_Input_ReelNo" });
            if (needFeeder)
            {
                htLoad.Add(SMTLoadFeederInputType.ReelNo, new string[] { SMTLoadFeederInputType.FeederCode, "$Please_Input_FeederCode" });
                htLoad.Add(SMTLoadFeederInputType.FeederCode, new string[] { SMTLoadFeederInputType.StationCode, "$Please_Input_StationCode" });

            }
            else
            {
                htLoad.Add(SMTLoadFeederInputType.ReelNo, new string[] { SMTLoadFeederInputType.StationCode, "$Please_Input_StationCode" });
            }

            inputFlow.Add(SMTLoadFeederOperationType.Load, htLoad);

            Hashtable htEx = new Hashtable();
            htEx.Add(SMTLoadFeederInputType.Init, new string[] { SMTLoadFeederInputType.StationCode, "$Please_Input_StationCode" });
            htEx.Add(SMTLoadFeederInputType.StationCode, new string[] { SMTLoadFeederInputType.ReelNoOld, "$Please_Input_ReelNoOld" });
            htEx.Add(SMTLoadFeederInputType.ReelNoOld, new string[] { SMTLoadFeederInputType.ReelNo, "$Please_Input_ReelNo" });

            if (needFeeder)
            {
                htEx.Add(SMTLoadFeederInputType.ReelNo, new string[] { SMTLoadFeederInputType.FeederCodeOld, "$Please_Input_FeederCodeOld" });
                htEx.Add(SMTLoadFeederInputType.FeederCodeOld, new string[] { SMTLoadFeederInputType.FeederCode, "$Please_Input_FeederCode" });
            }
            else
            {
                inputFlow.Add(SMTLoadFeederOperationType.Exchange, htEx);
            }

            Hashtable htContinue = new Hashtable();
            htContinue.Add(SMTLoadFeederInputType.Init, new string[] { SMTLoadFeederInputType.StationCode, "$Please_Input_StationCode" });
            htContinue.Add(SMTLoadFeederInputType.StationCode, new string[] { SMTLoadFeederInputType.ReelNoOld, "$Please_Input_ReelNoOld" });
            htContinue.Add(SMTLoadFeederInputType.ReelNoOld, new string[] { SMTLoadFeederInputType.ReelNo, "$Please_Input_ReelNo" });
            inputFlow.Add(SMTLoadFeederOperationType.Continue, htContinue);

            Hashtable htUnLoad = new Hashtable();
            htUnLoad.Add(SMTLoadFeederInputType.Init, new string[] { SMTLoadFeederInputType.MachineCode, "$Please_Input_MachineCode" });
            htUnLoad.Add(SMTLoadFeederInputType.MachineCode, new string[] { SMTLoadFeederInputType.StationCode, "$Please_Input_StationCode" });
            htUnLoad.Add(SMTLoadFeederInputType.StationCode, new string[] { SMTLoadFeederInputType.ReelNo, "$Please_Input_ReelNo" });
            if (needFeeder)
            {
                htUnLoad.Add(SMTLoadFeederInputType.ReelNo, new string[] { SMTLoadFeederInputType.FeederCode, "$Please_Input_FeederCode" });
            }
            inputFlow.Add(SMTLoadFeederOperationType.UnLoadSingle, htUnLoad);

            Hashtable htChk = new Hashtable();
            htChk.Add(SMTLoadFeederInputType.Init, new string[] { SMTLoadFeederInputType.MachineCode, "$Please_Input_MachineCode" });
            htChk.Add(SMTLoadFeederInputType.MachineCode, new string[] { SMTLoadFeederInputType.StationCode, "$Please_Input_StationCode" });

            if (needFeeder)
            {
                htChk.Add(SMTLoadFeederInputType.StationCode, new string[] { SMTLoadFeederInputType.FeederCode, "$Please_Input_FeederCode" });
                htChk.Add(SMTLoadFeederInputType.FeederCode, new string[] { SMTLoadFeederInputType.ReelNo, "$Please_Input_ReelNo" });
            }
            else
            {
                htChk.Add(SMTLoadFeederInputType.StationCode, new string[] { SMTLoadFeederInputType.ReelNo, "$Please_Input_ReelNo" });
            }

            inputFlow.Add(SMTLoadFeederOperationType.LoadCheck, htChk);

            listInput = new Hashtable();
        }

        private SMTFacade smtFacade = null;
        private IDomainDataProvider _domainDataProvider = Service.ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        private void CloseConnection()
        {
            if (this.DataProvider != null)
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
        }

        private DataTable tableSource = null;
        private MO mo = null;
        private string checkedMachineCode = string.Empty;
        private void FSMTLoadFeeder_Load(object sender, System.EventArgs e)
        {
            InitFlowData();
            if (tableSource == null)
            {
                tableSource = new DataTable();
                tableSource.Columns.Add("MachineCode");
                tableSource.Columns.Add("MachineStationCode");
                tableSource.Columns.Add("FeederCode");
                tableSource.Columns.Add("FeederSpecCode");
                tableSource.Columns.Add("FeederLeftCount", typeof(int));
                tableSource.Columns.Add("ReelNo");
                tableSource.Columns.Add("ReelLeftQty", typeof(int));
                tableSource.Columns.Add("MaterialCode");
                tableSource.Columns.Add("LoadUser");
                tableSource.Columns.Add("LoadDate");
                tableSource.Columns.Add("LoadTime");
                tableSource.Columns.Add("CheckResult");
                tableSource.Columns.Add("FailReason");
                tableSource.Columns.Add("NextReelNo");
                tableSource.Columns.Add("NextReelLeftQty", typeof(int));
            }
            gridList.DataSource = tableSource;
            gridList.DisplayLayout.Bands[0].Columns["LoadUser"].Hidden = true;
            gridList.DisplayLayout.Bands[0].Columns["LoadDate"].Hidden = true;
            gridList.DisplayLayout.Bands[0].Columns["CheckResult"].Hidden = true;
            gridList.DisplayLayout.Bands[0].Columns["FailReason"].Hidden = true;
            gridList.DisplayLayout.Bands[0].Columns["FeederCode"].Hidden = true;
            gridList.DisplayLayout.Bands[0].Columns["FeederSpecCode"].Hidden = true;
            gridList.DisplayLayout.Bands[0].Columns["FeederLeftCount"].Hidden = true;

            smtFacade = new SMTFacade(this.DataProvider);
            this.txtInput.TextFocus(false, true);

            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(gridList);
            gridList.DisplayLayout.Bands[0].Columns["FeederLeftCount"].Format = "#,#";
            gridList.DisplayLayout.Bands[0].Columns["FeederLeftCount"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            gridList.DisplayLayout.Bands[0].Columns["ReelLeftQty"].Format = "#,#";
            gridList.DisplayLayout.Bands[0].Columns["ReelLeftQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

            this.gridList.DisplayLayout.Bands[0].Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;

            // 排序
            this.gridList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            //显示行号
            this.gridList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.gridList.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.VisibleIndex;
            this.gridList.DisplayLayout.Override.RowSelectorWidth = 30;

            string strPath = Application.StartupPath;
            if (strPath.EndsWith("\\") == false)
                strPath += "\\";
            strPath += "Music";
            if (System.IO.Directory.Exists(strPath))
            {
                string[] files = System.IO.Directory.GetFiles(strPath, "*.wav");
                for (int i = 0; i < files.Length; i++)
                {
                    lstAlertMusic.Items.Add(files[i].Substring(files[i].LastIndexOf("\\") + 1));
                }
            }
            else
            {
                System.IO.Directory.CreateDirectory(strPath);
            }
            this.txtUserCode.Value = Service.ApplicationService.Current().UserCode;
            this.btnDisable.Enabled = false;
            this.btnEnable.Enabled = false;
            this.btnActive.Enabled = false;
            this.btnInactive.Enabled = false;

            //this.InitPageLanguage();
            //this.InitGridLanguage(gridList);
        }

        private void FFeederGetOut_Closed(object sender, System.EventArgs e)
        {
            this.CloseConnection();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            txtInput.Value = txtInput.Value.Trim().ToUpper();
            if (txtInput.Value == string.Empty)
                return;
            ProcessInput(txtInput.Value);
            txtInput.Value = string.Empty;
        }

        private string currInput = SMTLoadFeederInputType.Init;
        /// <summary>
        /// 处理输入数据
        /// </summary>
        private void ProcessInput(string strInput)
        {
            ucMessageInfo.Add(txtInput.Value);
            bool bIsCmd = false;
            // 固定命令处理
            if (strInput == SMTLoadFeederCommand.Machine)
            {
                //Remove UCLabel.SelectAll;
                this.txtMachineCode.TextFocus(false, true);
                InitInput();
                return;
            }
            else if (strInput == SMTLoadFeederCommand.Load)
            {
                InitInput();
                bIsCmd = true;
                if (this.rdoLoad.Checked == false)
                {
                    this.rdoLoad.Checked = true;
                    return;
                }
            }
            else if (strInput == SMTLoadFeederCommand.Exchange)
            {
                InitInput();
                bIsCmd = true;
                if (this.rdoExchanges.Checked == false)
                {
                    this.rdoExchanges.Checked = true;
                    return;
                }
            }
            else if (strInput == SMTLoadFeederCommand.Continue)
            {
                InitInput();
                bIsCmd = true;
                if (this.rdoContinue.Checked == false)
                {
                    this.rdoContinue.Checked = true;
                    return;
                }
            }
            else if (strInput == SMTLoadFeederCommand.UnLoadSingle)
            {
                InitInput();
                bIsCmd = true;
                if (this.rdoReturn.Checked == false)
                {
                    this.rdoReturn.Checked = true;
                    return;
                }
            }
            else if (strInput == SMTLoadFeederCommand.LoadCheck)
            {
                InitInput();
                bIsCmd = true;
                if (this.rdoCheck.Checked == false)
                {
                    this.rdoCheck.Checked = true;
                    return;
                }
            }
            // 如果不是命令，则保存当前输入
            if (bIsCmd == false)
            {
                Hashtable ht = null;
                if (this.rdoLoad.Checked == true)
                    ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.Load];
                else if (this.rdoExchanges.Checked == true)
                    ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.Exchange];
                else if (this.rdoContinue.Checked == true)
                    ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.Continue];
                else if (this.rdoReturn.Checked == true)
                    ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.UnLoadSingle];
                else if (this.rdoCheck.Checked == true)
                    ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.LoadCheck];
                if (ht.ContainsKey(currInput) == true)
                {
                    //Icyer,2007/01/15 修改	减少Open/Close的次数
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                    try
                    {
                        string[] str = (string[])ht[currInput];
                        string thisInput = str[0];
                        if (CheckInput(thisInput) == true)
                        {
                            listInput.Add(thisInput, txtInput.Value);
                            currInput = thisInput;
                        }
                        // 是否输入结束
                        if (ht.ContainsKey(currInput) == false)
                        {
                            Messages messages = ExecuteInput();
                            Application.DoEvents();
                            listInput.Clear();
                            currInput = SMTLoadFeederInputType.Init;
                            this.lstReelAcceptFeeder.Items.Clear();
                            this.lstReelAcceptStationCode.Items.Clear();
                            if (messages == null)
                            {
                                return;
                            }
                            else
                            {
                                Application.DoEvents();
                                ucMessageInfo.Add(messages);
                                Application.DoEvents();
                            }
                            GetMachineLoadedFeeder();
                        }
                    }
                    catch { }
                    finally
                    {
                        ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                        ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
                    }
                }
            }
            ShowCurrentMessage();
            this.txtInput.TextFocus(false, true);
        }

        private void ShowCurrentMessage()
        {
            Hashtable ht = null;
            if (this.rdoLoad.Checked == true)
                ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.Load];
            else if (this.rdoExchanges.Checked == true)
                ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.Exchange];
            else if (this.rdoContinue.Checked == true)
                ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.Continue];
            else if (this.rdoReturn.Checked == true)
                ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.UnLoadSingle];
            else if (this.rdoCheck.Checked == true)
                ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.LoadCheck];
            if (ht.ContainsKey(currInput) == true)
            {
                string[] str = (string[])ht[currInput];
                string strMsg = ">>" + str[1];
                ucMessageInfo.AddBoldText(strMsg);
            }
        }

        /// <summary>
        /// 在输入时检查数据是否正确
        /// </summary>
        private bool CheckInput(string inputType)
        {
            bool bResult = true;
            // 检查上料输入
            if (this.rdoLoad.Checked == true)
            {
                if (inputType == SMTLoadFeederInputType.ReelNo)
                {
                    Reel reel = (Reel)smtFacade.GetReel(this.txtInput.Value);
                    if (reel == null)
                    {
                        ucMessageInfo.Add(new UserControl.Message(MessageType.Error, "$Reel_Not_Exist"));
                        return false;
                    }
                    ArrayList listFeeder = new ArrayList();
                    ArrayList listStation = new ArrayList();
                    for (int i = 0; i < tableSource.Rows.Count; i++)
                    {
                        if (tableSource.Rows[i]["MaterialCode"].ToString() == reel.PartNo &&
                            tableSource.Rows[i]["FeederCode"].ToString() == string.Empty)
                        {
                            if (listFeeder.Contains(tableSource.Rows[i]["FeederSpecCode"].ToString()) == false)
                            {
                                listFeeder.Add(tableSource.Rows[i]["FeederSpecCode"].ToString());
                            }
                            if (listStation.Contains(tableSource.Rows[i]["MachineStationCode"].ToString()) == false)
                            {
                                listStation.Add(tableSource.Rows[i]["MachineStationCode"].ToString());
                            }
                        }
                    }
                    lstReelAcceptFeeder.Items.Clear();
                    lstReelAcceptStationCode.Items.Clear();
                    lstReelAcceptFeeder.Items.AddRange(listFeeder.ToArray());
                    lstReelAcceptStationCode.Items.AddRange(listStation.ToArray());
                    if (listFeeder.Count == 0)
                    {
                        ucMessageInfo.Add(new UserControl.Message(MessageType.Error, "$SMTLoad_Reel_Not_Match_FeederSpec"));
                        return false;
                    }
                    if (listStation.Count == 0)
                    {
                        ucMessageInfo.Add(new UserControl.Message(MessageType.Error, "$SMTLoad_Reel_Not_Match_MachineStationCode"));
                        return false;
                    }
                }
                else if (inputType == SMTLoadFeederInputType.FeederCode)
                {
                    Feeder feeder = (Feeder)smtFacade.GetFeeder(this.txtInput.Value);
                    if (feeder == null)
                    {
                        ucMessageInfo.Add(new UserControl.Message(MessageType.Error, "$Feeder_Not_Exist"));
                        return false;
                    }
                    for (int i = 0; i < lstReelAcceptFeeder.Items.Count; i++)
                    {
                        if (lstReelAcceptFeeder.Items[i].ToString() == feeder.FeederSpecCode)
                        {
                            return true;
                        }
                    }
                    ucMessageInfo.Add(new UserControl.Message(MessageType.Error, "$Feeder_Isnt_In_Accept_List"));
                    return false;
                }
                else if (inputType == SMTLoadFeederInputType.StationCode)
                {
                    for (int i = 0; i < lstReelAcceptStationCode.Items.Count; i++)
                    {
                        if (lstReelAcceptStationCode.Items[i].ToString() == this.txtInput.Value)
                        {
                            return true;
                        }
                    }
                    ucMessageInfo.Add(new UserControl.Message(MessageType.Error, "$StationCode_Isnt_In_Accept_List"));
                    return false;
                }
            }
            return bResult;
        }

        private void InitInput()
        {
            currInput = SMTLoadFeederInputType.Init;
            listInput.Clear();
        }

        private Messages ExecuteInput()
        {
            if (txtUserCode.Value.Trim() == string.Empty)
            {
                this.ucMessageInfo.Add(new UserControl.Message(MessageType.Error, txtUserCode.Caption + " $Error_Input_Empty"));
                return null;
            }
            this.txtMOCode.Value = this.txtMOCode.Value.Trim().ToUpper();
            this.txtMachineCode.Value = this.txtMachineCode.Value.Trim().ToUpper();
            if (this.txtMOCode.Value == string.Empty)
            {
                ucMessageInfo.Add("$CS_Please_Input_MOCode");
                this.txtMOCode.TextFocus(false, true);
                return null;
            }
            if (mo == null || mo.MOCode != txtMOCode.Value)
            {
                BenQGuru.eMES.MOModel.MOFacade moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
                object objMO = moFacade.GetMO(txtMOCode.Value);
                if (objMO == null)
                {
                    ucMessageInfo.Add("$CS_MO_Not_Exist");
                    txtMOCode.TextFocus(false, true);
                    return null;
                }
                mo = (MO)objMO;
                txtItem.Value = mo.ItemCode;
                GetMOEnabled(mo.MOCode);
            }
            if (this.txtMachineCode.Value == string.Empty)
            {
                ucMessageInfo.Add("$Please_Input_MachineCode");
                this.txtMachineCode.TextFocus(false, true);
                return null;
            }
            if (checkedMachineCode != this.txtMachineCode.Value)
            {
                this.GetMachineLoadedFeeder();
            }

            //((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.BeginTransaction();
            Messages messages = new Messages();
            string operationType = string.Empty;
            try
            {
                if (this.rdoLoad.Checked == true)
                {
                    operationType = SMTLoadFeederOperationType.Load;
                    if (needFeeder)
                    {
                        messages = smtFacade.SMTLoadFeeder(
                            mo, txtMachineCode.Value,
                            listInput[SMTLoadFeederInputType.StationCode].ToString(),
                            listInput[SMTLoadFeederInputType.ReelNo].ToString(),
                            listInput[SMTLoadFeederInputType.FeederCode].ToString(),
                            this.txtUserCode.Value.Trim().ToUpper(),
                            tableSource,
                            Service.ApplicationService.Current().ResourceCode,
                            Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                            this.stationTableGroupActive,
                            this.cboStationTable.ComboBoxData.Text);
                    }
                    else
                    {
                        messages = smtFacade.SMTLoadFeederNoFeeder(
                       mo, txtMachineCode.Value,
                       listInput[SMTLoadFeederInputType.StationCode].ToString(),
                       listInput[SMTLoadFeederInputType.ReelNo].ToString(),
                       "DEFAULTFEEDER",//listInput[SMTLoadFeederInputType.FeederCode].ToString(),
                       this.txtUserCode.Value.Trim().ToUpper(),
                       tableSource,
                       Service.ApplicationService.Current().ResourceCode,
                       Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                       this.stationTableGroupActive,
                       this.cboStationTable.ComboBoxData.Text);
                    }
                    if (messages.IsSuccess())
                        messages.Add(new UserControl.Message(MessageType.Success, "$SMT_LoadItem_Success"));
                }
                else if (this.rdoExchanges.Checked == true)
                {
                    operationType = SMTLoadFeederOperationType.Exchange;
                    if (needFeeder)
                    {
                        messages = smtFacade.SMTExchangeFeeder(
                            operationType, mo, txtMachineCode.Value,
                            listInput[SMTLoadFeederInputType.StationCode].ToString(),
                            listInput[SMTLoadFeederInputType.ReelNoOld].ToString(),
                            listInput[SMTLoadFeederInputType.ReelNo].ToString(),
                            listInput[SMTLoadFeederInputType.FeederCodeOld].ToString(),
                            listInput[SMTLoadFeederInputType.FeederCode].ToString(),
                            this.txtUserCode.Value.Trim().ToUpper(),
                            tableSource,
                            Service.ApplicationService.Current().ResourceCode,
                            Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                            this.cboStationTable.ComboBoxData.Text);
                    }
                    else
                    {
                        messages = smtFacade.SMTExchangeFeederNoFeeder(
                              operationType, mo, txtMachineCode.Value,
                              listInput[SMTLoadFeederInputType.StationCode].ToString(),
                              listInput[SMTLoadFeederInputType.ReelNoOld].ToString(),
                              listInput[SMTLoadFeederInputType.ReelNo].ToString(),
                              "DEFAULTFEEDER",//listInput[SMTLoadFeederInputType.FeederCodeOld].ToString(),
                              "DEFAULTFEEDER",//listInput[SMTLoadFeederInputType.FeederCode].ToString(),
                              this.txtUserCode.Value.Trim().ToUpper(),
                              tableSource,
                              Service.ApplicationService.Current().ResourceCode,
                              Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                              this.cboStationTable.ComboBoxData.Text);
                    }
                    if (messages.IsSuccess())
                    {
                        messages.Add(new UserControl.Message(MessageType.Success, "$SMT_ExchangeItem_Success"));
                        // 开启产线
                        if (messages.GetData() != null && messages.GetData().Values != null && messages.GetData().Values.Length > 0)
                        {
                            SMTLineControlLog line = (SMTLineControlLog)messages.GetData().Values[0];
                            if (FormatHelper.StringToBoolean(line.LineStatus) == true)
                            {
                                if (FSMTFeederReelWatch.Current != null)
                                {
                                    FSMTFeederReelWatch.Current.StartLine();
                                }
                            }
                        }
                    }
                }
                else if (this.rdoContinue.Checked == true)
                {
                    operationType = SMTLoadFeederOperationType.Continue;
                    if (needFeeder)
                    {
                        messages = smtFacade.SMTContinueFeeder(
                            operationType, mo, txtMachineCode.Value,
                            listInput[SMTLoadFeederInputType.StationCode].ToString(),
                            listInput[SMTLoadFeederInputType.ReelNoOld].ToString(),
                            listInput[SMTLoadFeederInputType.ReelNo].ToString(),
                            this.txtUserCode.Value.Trim().ToUpper(),
                            tableSource,
                            Service.ApplicationService.Current().ResourceCode,
                            Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                            this.cboStationTable.ComboBoxData.Text);
                    }
                    else
                    {
                        messages = smtFacade.SMTContinueNoFeeder(
                                  operationType, mo, txtMachineCode.Value,
                                  listInput[SMTLoadFeederInputType.StationCode].ToString(),
                                  listInput[SMTLoadFeederInputType.ReelNoOld].ToString(),
                                  listInput[SMTLoadFeederInputType.ReelNo].ToString(),
                                  this.txtUserCode.Value.Trim().ToUpper(),
                                  tableSource,
                                  Service.ApplicationService.Current().ResourceCode,
                                  Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                                  this.cboStationTable.ComboBoxData.Text);
                    }

                    if (messages.IsSuccess())
                    {
                        messages.Add(new UserControl.Message(MessageType.Success, "$SMT_ContinueItem_Success"));
                        // 开启产线
                        if (messages.GetData() != null && messages.GetData().Values != null && messages.GetData().Values.Length > 0)
                        {
                            SMTLineControlLog line = (SMTLineControlLog)messages.GetData().Values[0];
                            if (FormatHelper.StringToBoolean(line.LineStatus) == true)
                            {
                                if (FSMTFeederReelWatch.Current != null)
                                {
                                    FSMTFeederReelWatch.Current.StartLine();
                                }
                            }
                        }
                    }
                }
                else if (this.rdoReturn.Checked == true)
                {
                    if (needFeeder)
                    {
                        messages = smtFacade.SMTUnLoadFeederSingle(
                            mo,
                            listInput[SMTLoadFeederInputType.MachineCode].ToString(),
                            listInput[SMTLoadFeederInputType.StationCode].ToString(),
                            listInput[SMTLoadFeederInputType.FeederCode].ToString(),
                            listInput[SMTLoadFeederInputType.ReelNo].ToString(),
                            this.txtUserCode.Value.Trim().ToUpper(),
                            tableSource,
                            Service.ApplicationService.Current().ResourceCode,
                            Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                            this.cboStationTable.ComboBoxData.Text);
                    }
                    else
                    {
                        messages = smtFacade.SMTUnLoadFeederSingleNoFeeder(
                            mo,
                            listInput[SMTLoadFeederInputType.MachineCode].ToString(),
                            listInput[SMTLoadFeederInputType.StationCode].ToString(),
                            "DEFAULTFEEDER",//listInput[SMTLoadFeederInputType.FeederCode].ToString(),
                            listInput[SMTLoadFeederInputType.ReelNo].ToString(),
                            this.txtUserCode.Value.Trim().ToUpper(),
                            tableSource,
                            Service.ApplicationService.Current().ResourceCode,
                            Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                            this.cboStationTable.ComboBoxData.Text);
                    }

                    if (messages.IsSuccess())
                        messages.Add(new UserControl.Message(MessageType.Success, "$SMT_UnLoadSingleItem_Success"));
                }
                else if (this.rdoCheck.Checked == true)
                {
                    if (needFeeder)
                    {
                        messages = smtFacade.SMTLoadCheck(
                            mo.MOCode,
                            Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                            listInput[SMTLoadFeederInputType.MachineCode].ToString(),
                            listInput[SMTLoadFeederInputType.StationCode].ToString(),
                            listInput[SMTLoadFeederInputType.ReelNo].ToString(),
                            listInput[SMTLoadFeederInputType.FeederCode].ToString()
                            );
                    }
                    else
                    {
                        messages = smtFacade.SMTLoadCheck(
                        mo.MOCode,
                        Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                        listInput[SMTLoadFeederInputType.MachineCode].ToString(),
                        listInput[SMTLoadFeederInputType.StationCode].ToString(),
                        listInput[SMTLoadFeederInputType.ReelNo].ToString(),
                        "DEFAULTFEEDER"//listInput[SMTLoadFeederInputType.FeederCode].ToString()
                        );
                    }
                }
                if (this.rdoCheck.Checked == false)		// 上料检查时不用更改Grid的内容
                {
                    DataRow[] rows = tableSource.Select("MachineStationCode='" + listInput[SMTLoadFeederInputType.StationCode].ToString() + "'");
                    for (int i = 0; i < tableSource.Rows.Count; i++)
                    {
                        if (tableSource.Rows[i]["MachineStationCode"].ToString() == listInput[SMTLoadFeederInputType.StationCode].ToString())
                        {
                            if (messages.IsSuccess() == false)
                            {
                                if (this.rdoLoad.Checked == true && tableSource.Rows[i]["ReelNo"].ToString() == string.Empty)
                                    gridList.Rows[i].Appearance.ForeColor = Color.Red;
                            }
                            else if (this.rdoReturn.Checked == true && tableSource.Rows[i]["ReelNo"].ToString() == string.Empty)
                            {
                                gridList.Rows[i].Appearance.ForeColor = Color.Red;
                            }
                            else
                            {
                                if (tableSource.Rows[i]["ReelNo"].ToString() != string.Empty)
                                    gridList.Rows[i].Appearance.ForeColor = Color.Black;
                            }
                        }
                    }
                }
                if (messages.IsSuccess() == false)
                {
                    PlayAlertMusic(operationType);
                }
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CommitTransaction();
            }
            catch (Exception e)
            {
                messages.Add(new UserControl.Message(e));
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
                try
                {
                    string strErrorInfo = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "\t" + e.Message + "\t" + e.Source;
                    strErrorInfo += "\r\n" + e.StackTrace;
                    UserControl.FileLog.FileLogOut("Client.log", strErrorInfo);
                }
                catch
                { }
            }
            finally
            {
                //((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            }
            txtMachineLoadedCount.Text = tableSource.Rows.Count.ToString();
            if (tableSource.Rows.Count > 0)
            {
                DataRow[] rows = tableSource.Select("CheckResult='1'");
                if (rows != null)
                    txtMachineLoadedCount.Text = (tableSource.Rows.Count - rows.Length).ToString();
            }
            return messages;
        }

        private void PlayAlertMusic(string operationType)
        {
            string strPath = Application.StartupPath;
            if (strPath.EndsWith("\\") == false)
                strPath += "\\";
            strPath += "Music";
            if (lstAlertMusic.Items.Count > 0)
            {
                if (lstAlertMusic.SelectedItem == null)
                {
                    lstAlertMusic.SelectedIndex = 0;
                }
                strPath += "\\" + lstAlertMusic.SelectedItem.ToString();
                PlaySound(strPath, 0, SND_FILENAME);
            }
        }

        private void GetMOEnabled(string moCode)
        {
            //object[] objstmp = smtFacade.QueryMachineFeeder(moCode, string.Empty, string.Empty, 1, 1);
            object[] objstmp = smtFacade.QueryMachineFeeder(moCode, Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode);
            bool bEnabled = false;
            if (objstmp != null && objstmp.Length > 0 && FormatHelper.StringToBoolean(((MachineFeeder)objstmp[0]).Enabled) == true)
                bEnabled = true;
            if (bEnabled == true)
            {
                this.txtMOStatus.Value = UserControl.MutiLanguages.ParserString("$SMTMOEnabledStatus_Enabled");
                this.txtMOStatus.InnerTextBox.ForeColor = Color.Green;
                this.btnDisable.Enabled = true;
                this.btnEnable.Enabled = false;
            }
            else
            {
                this.txtMOStatus.Value = UserControl.MutiLanguages.ParserString("$SMTMOEnabledStatus_Disabled");
                this.txtMOStatus.InnerTextBox.ForeColor = Color.Red;
                this.btnDisable.Enabled = false;
                this.btnEnable.Enabled = true;
            }
        }

        private void GetMachineLoadedFeeder()
        {
            GetMachineLoadedFeeder(false);
        }
        private void GetMachineLoadedFeeder(bool fromTableGroup)
        {
            int iCount = 0;
            while (iCount < 3)
            {
                try
                {
                    this.txtMOCode.Value = this.txtMOCode.Value.Trim().ToUpper();
                    this.txtMachineCode.Value = this.txtMachineCode.Value.Trim().ToUpper();
                    if (this.txtMOCode.Value == string.Empty)
                    {
                        ucMessageInfo.Add("$CS_Please_Input_MOCode");
                        this.txtMOCode.TextFocus(false, true);
                        return;
                    }
                    if (mo == null || mo.MOCode != txtMOCode.Value)
                    {
                        BenQGuru.eMES.MOModel.MOFacade moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
                        object objMO = moFacade.GetMO(txtMOCode.Value);
                        if (objMO == null)
                        {
                            ucMessageInfo.Add("$CS_MO_Not_Exist");
                            txtMOCode.TextFocus(false, true);
                            return;
                        }
                        mo = (MO)objMO;
                        txtItem.Value = mo.ItemCode;
                        this.GetMOEnabled(mo.MOCode);
                        fromTableGroup = false;
                    }
                    if (this.txtMachineCode.Value == string.Empty)
                    {
                        ucMessageInfo.Add("$Please_Input_MachineCode");
                        this.txtMachineCode.TextFocus(false, true);
                        return;
                    }

                    if (fromTableGroup == false)
                    {
                        InitTableGroup();
                    }

                    // 显示需要上料的记录
                    tableSource.Rows.Clear();
                    object[] objsSmt = smtFacade.QuerySMTFeederMaterialByProductCode(mo.ItemCode, Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode, txtMachineCode.Value, this.cboStationTable.SelectedItemValue.ToString());
                    if (objsSmt != null)
                    {
                        for (int i = 0; i < objsSmt.Length; i++)
                        {
                            SMTFeederMaterial item = (SMTFeederMaterial)objsSmt[i];
                            DataRow row = tableSource.NewRow();
                            row["MachineCode"] = item.MachineCode;
                            row["MachineStationCode"] = item.MachineStationCode;
                            row["FeederCode"] = string.Empty;
                            row["FeederSpecCode"] = item.FeederSpecCode;
                            row["FeederLeftCount"] = "0";
                            row["ReelNo"] = string.Empty;
                            row["ReelLeftQty"] = "0";
                            row["MaterialCode"] = item.MaterialCode;
                            row["LoadUser"] = string.Empty;
                            row["LoadDate"] = string.Empty;
                            row["LoadTime"] = string.Empty;
                            row["NextReelNo"] = string.Empty;
                            row["NextReelLeftQty"] = "0";
                            tableSource.Rows.Add(row);
                        }
                    }
                    // 显示已上料记录
                    object[] objs = smtFacade.GetMachineLoadedFeeder(txtMOCode.Value, Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode, txtMachineCode.Value, this.cboStationTable.SelectedItemValue.ToString());
                    if (objs != null)
                    {
                        for (int i = 0; i < objs.Length; i++)
                        {
                            MachineFeeder mf = (MachineFeeder)objs[i];
                            int iRowIdx = -1;
                            for (int n = 0; n < tableSource.Rows.Count; n++)
                            {
                                if (tableSource.Rows[n]["MachineCode"].ToString() == mf.MachineCode &&
                                    tableSource.Rows[n]["MachineStationCode"].ToString() == mf.MachineStationCode && (needFeeder ? (tableSource.Rows[n]["FeederSpecCode"].ToString() == mf.FeederSpecCode):true) &&
                                    tableSource.Rows[n]["MaterialCode"].ToString() == mf.MaterialCode)
                                {
                                    iRowIdx = n;
                                    break;
                                }
                            }
                            if (iRowIdx >= 0)
                            {
                                DataRow row = tableSource.Rows[iRowIdx];
                                row["MachineCode"] = mf.MachineCode;
                                row["MachineStationCode"] = mf.MachineStationCode;
                                row["FeederCode"] = mf.FeederCode;
                                row["FeederSpecCode"] = mf.FeederSpecCode;
                                Feeder feeder = (Feeder)smtFacade.GetFeeder(mf.FeederCode);
                                row["FeederLeftCount"] = "0";
                                if (feeder != null)
                                {
                                    row["FeederLeftCount"] = feeder.MaxCount - feeder.UsedCount;
                                }
                                //当前料卷信息
                                if (mf.ReelNo != string.Empty)
                                {
                                    row["ReelNo"] = mf.ReelNo;
                                }
                                ReelQty reelQty = (ReelQty)smtFacade.GetReelQty(row["ReelNo"].ToString(), this.txtMOCode.Value.Trim().ToUpper());
                                row["ReelLeftQty"] = "0";
                                if (reelQty != null)
                                {
                                    row["ReelLeftQty"] = reelQty.Qty - reelQty.UsedQty - reelQty.UpdatedQty;
                                }
                                //下一料卷信息
                                if (mf.NextReelNo != string.Empty)
                                {
                                    row["NextReelNo"] = mf.NextReelNo;
                                }

                                ReelQty nextReelQty = (ReelQty)smtFacade.GetReelQty(row["NextReelNo"].ToString(), this.txtMOCode.Value.Trim().ToUpper());

                                row["NextReelLeftQty"] = "0";
                                if (nextReelQty != null)
                                {
                                    row["NextReelLeftQty"] = nextReelQty.Qty - nextReelQty.UsedQty - nextReelQty.UpdatedQty;
                                }

                                row["MaterialCode"] = mf.MaterialCode;
                                row["LoadUser"] = mf.LoadUser;
                                row["LoadDate"] = FormatHelper.ToDateString(mf.LoadDate);
                                row["LoadTime"] = FormatHelper.ToTimeString(mf.LoadTime);
                                row["CheckResult"] = mf.CheckResult;
                                row["FailReason"] = mf.FailReason;
                                if (FormatHelper.StringToBoolean(mf.CheckResult) == false)
                                {
                                    gridList.Rows[i].Appearance.ForeColor = Color.Red;
                                }
                            }
                        }
                    }
                    for (int i = 0; i < tableSource.Rows.Count; i++)
                    {
                        if (tableSource.Rows[i]["FeederCode"].ToString() == string.Empty)
                        {
                            gridList.Rows[i].Appearance.ForeColor = Color.Red;
                        }
                    }
                    txtMachineLoadedCount.Text = tableSource.Rows.Count.ToString();
                    if (tableSource.Rows.Count > 0)
                    {
                        DataRow[] rows = tableSource.Select("CheckResult='1'");
                        if (rows != null)
                            txtMachineLoadedCount.Text = (tableSource.Rows.Count - rows.Length).ToString();
                    }
                    break;
                }
                catch (Exception ex)
                {
                    try
                    {
                        string strErrorInfo = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "\t" + ex.Message + "\t" + ex.Source;
                        strErrorInfo += "\r\n" + ex.StackTrace;
                        UserControl.FileLog.FileLogOut("Client.log", strErrorInfo);
                    }
                    catch { }
                    finally
                    {
                        iCount++;
                        if (iCount > 2)
                        {
                            throw ex;
                        }
                    }
                }
            }
            //ucMessageInfo.AddBoldText(">>$Please_Input_StationCode");
            ShowCurrentMessage();
            InitInput();
            txtInput.TextFocus(false, true);
            checkedMachineCode = txtMachineCode.Value;
        }
        private bool stationTableGroupActive = false;
        private void InitTableGroup()
        {
            // 查询Table组次
            string[] strTableGrp = smtFacade.GetSMTFeederMatrialTableGroup(mo.ItemCode, Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode, txtMachineCode.Value);
            bool bUpdateTableGrp = true;
            if (strTableGrp != null && strTableGrp.Length == this.cboStationTable.ComboBoxData.Items.Count)
            {
                bUpdateTableGrp = false;
                for (int i = 0; i < strTableGrp.Length; i++)
                {
                    if (strTableGrp[i] != this.cboStationTable.ComboBoxData.Items[i].ToString())
                    {
                        bUpdateTableGrp = true;
                        break;
                    }
                }
            }

            if (bUpdateTableGrp == true)
            {
                this.cboStationTable.Clear();
                if (strTableGrp != null && strTableGrp.Length > 0)
                {
                    for (int i = 0; i < strTableGrp.Length; i++)
                    {
                        this.cboStationTable.AddItem(strTableGrp[i], strTableGrp[i]);
                    }
                }
                else
                {
                    this.cboStationTable.AddItem(string.Empty, string.Empty);
                }
            }
            changeTableInCode = true;
            this.cboStationTable.SelectedIndex = 0;
            this.cboStationTable.Enabled = true;
            this.txtTableStatus.Value = UserControl.MutiLanguages.ParserString("$StationTableGroupStatus_Inactive");
            stationTableGroupActive = false;
            this.btnActive.Enabled = true;
            this.btnInactive.Enabled = false;
            this.txtTableStatus.InnerTextBox.ForeColor = Color.Red;
            string strGrp = smtFacade.GetActiveStationTable(mo.MOCode, Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode, this.txtMachineCode.Value);
            for (int i = 0; i < this.cboStationTable.ComboBoxData.Items.Count; i++)
            {
                if (this.cboStationTable.ComboBoxData.Items[i].ToString() == strGrp)
                {
                    this.cboStationTable.SelectedIndex = i;
                    this.txtTableStatus.Value = UserControl.MutiLanguages.ParserString("$StationTableGroupStatus_Active");
                    stationTableGroupActive = true;
                    this.btnActive.Enabled = false;
                    this.btnInactive.Enabled = true;
                    this.txtTableStatus.InnerTextBox.ForeColor = Color.Green;
                    break;
                }
            }
            if (this.cboStationTable.ComboBoxData.Items.Count <= 1)
            {
                this.cboStationTable.Enabled = false;
                this.btnActive.Enabled = false;
                this.btnInactive.Enabled = false;
                this.txtTableStatus.Value = UserControl.MutiLanguages.ParserString("$StationTableGroupStatus_Active");
                stationTableGroupActive = true;
                this.txtTableStatus.InnerTextBox.ForeColor = Color.Green;
            }
            changeTableInCode = false;
        }

        private void txtInput_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                this.btnOK_Click(null, null);
        }

        private void txtMOCode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                this.GetMachineLoadedFeeder();
        }

        private void rdoLoad_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.txtMOCode.Value != string.Empty && this.txtMachineCode.Value != string.Empty &&
                ((RadioButton)sender).Checked == true)
            {
                InitInput();
                ShowCurrentMessage();
                Application.DoEvents();
                this.txtInput.TextFocus(false, true);
            }
        }

        private void btnUnloadAll_Click(object sender, System.EventArgs e)
        {
            if (this.txtMOCode.Value == string.Empty)
                return;
            this.txtMOCode.Value = this.txtMOCode.Value.Trim().ToUpper();
            if (MessageBox.Show(UserControl.MutiLanguages.ParserMessage("$Confirm_UnLoad_All_MachineFeeder_In_MOCode"), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            // 输入用户名、密码确认
            string strMsg = UserControl.MutiLanguages.ParserString("$SMT_UnLoadAll_Confirm_UserCode");
            FDialogInput finput = new FDialogInput(strMsg);
            strMsg = UserControl.MutiLanguages.ParserString("$SMT_UnLoadAll_Confirm_Title");
            finput.Text = strMsg;
            DialogResult dialogResult = finput.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            string strUserCode = finput.InputText.Trim().ToUpper();
            strMsg = UserControl.MutiLanguages.ParserString("$Please_Input_Password");
            finput = new FDialogInput(strMsg);
            finput.InputPasswordChar = '*';
            dialogResult = finput.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            string strPassword = finput.InputText.Trim().ToUpper();
            finput.Close();
            BenQGuru.eMES.Security.SecurityFacade security = new BenQGuru.eMES.Security.SecurityFacade(this.DataProvider);
            try
            {
                object objSec = security.PasswordCheck(strUserCode, strPassword);
            }
            catch (Exception ex)
            {
                Messages msgErr = new Messages();
                msgErr.Add(new UserControl.Message(ex));
                this.ucMessageInfo.Add(msgErr);
                Application.DoEvents();
                this.txtInput.TextFocus(false, true);
                return;
            }

            Messages msg = new Messages();
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.BeginTransaction();
            try
            {
                msg.AddMessages(smtFacade.SMTUnLoadByMOCode(
                    this.txtMOCode.Value.Trim().ToUpper(),
                    this.txtUserCode.Value.Trim().ToUpper(),
                    Service.ApplicationService.Current().LoginInfo.Resource.ResourceCode,
                    Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode));
                if (msg.IsSuccess() == true)
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CommitTransaction();
                    msg.Add(new UserControl.Message(MessageType.Success, "$SMT_UnLoad_Success"));
                    // 停线
                    if (msg.GetData() != null && msg.GetData().Values != null && msg.GetData().Values.Length > 0)
                    {
                        SMTLineControlLog line = (SMTLineControlLog)msg.GetData().Values[0];
                        if (FormatHelper.StringToBoolean(line.LineStatus) == false)
                        {
                            if (FSMTFeederReelWatch.Current != null)
                            {
                                FSMTFeederReelWatch.Current.StopLine();
                            }
                        }
                    }
                }
                else
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            }
            this.ucMessageInfo.Add(msg);
            this.tableSource.Rows.Clear();
            this.currInput = SMTLoadFeederInputType.Init;
            //if (msg.IsSuccess() == true)
            //{
            GetMachineLoadedFeeder();
            //}
        }

        private void btnEnable_Click(object sender, System.EventArgs e)
        {
            SetMOEnabled(sender, true);
        }

        private void btnDisable_Click(object sender, System.EventArgs e)
        {
            SetMOEnabled(sender, false);
        }

        private void SetMOEnabled(object sender, bool enabled)
        {
            if (this.txtMOCode.Value.Trim() == string.Empty)
            {
                Application.DoEvents();
                this.txtMOCode.TextFocus(true, true);
                return;
            }
            this.txtMOCode.Value = this.txtMOCode.Value.Trim().ToUpper();
            if (mo == null || mo.MOCode != txtMOCode.Value)
            {
                BenQGuru.eMES.MOModel.MOFacade moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
                object objMO = moFacade.GetMO(txtMOCode.Value);
                if (objMO == null)
                {
                    ucMessageInfo.Add("$CS_MO_Not_Exist");
                    txtMOCode.TextFocus(false, true);
                    return;
                }
                mo = (MO)objMO;
                txtItem.Value = mo.ItemCode;
                GetMOEnabled(mo.MOCode);
                if (((UCButton)sender).Enabled == false)
                    return;
            }
            string strConfirm = string.Empty;
            if (enabled == true)
            {
                strConfirm = UserControl.MutiLanguages.ParserString("$SMTEnabledMO_Confirm_Enabled") + " [" + this.txtMOCode.Value + "]";
            }
            else
            {
                strConfirm = UserControl.MutiLanguages.ParserString("$SMTEnabledMO_Confirm_Disabled") + " [" + this.txtMOCode.Value + "]";
            }

            if (MessageBox.Show(strConfirm, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            Messages msg = new Messages();
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.BeginTransaction();
            try
            {
                msg.AddMessages(smtFacade.SMTEnabledMachineFeeder(
                    this.txtMOCode.Value.Trim().ToUpper(),
                    Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                    enabled,
                    this.txtUserCode.Value.Trim().ToUpper()));
                if (msg.IsSuccess() == true)
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CommitTransaction();
                    if (enabled == true)
                        msg.Add(new UserControl.Message(MessageType.Success, "$SMT_Enabled_Success"));
                    else
                        msg.Add(new UserControl.Message(MessageType.Success, "$SMT_Disabled_Success"));
                    this.GetMOEnabled(this.txtMOCode.Value);
                    // 停线/开启产线
                    if (msg.GetData() != null && msg.GetData().Values != null && msg.GetData().Values.Length > 0)
                    {
                        SMTLineControlLog line = (SMTLineControlLog)msg.GetData().Values[0];
                        // 失效则停线
                        if (enabled == false)
                        {
                            if (FormatHelper.StringToBoolean(line.LineStatus) == false)
                            {
                                if (FSMTFeederReelWatch.Current != null)
                                {
                                    FSMTFeederReelWatch.Current.StopLine();
                                }
                            }
                        }
                        else	// 生效则开启产线
                        {
                            if (FormatHelper.StringToBoolean(line.LineStatus) == true)
                            {
                                if (FSMTFeederReelWatch.Current != null)
                                {
                                    FSMTFeederReelWatch.Current.StartLine();
                                }
                            }
                        }
                    }
                }
                else
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            }
            this.ucMessageInfo.Add(msg);
        }

        private bool changeTableInCode = false;
        private void cboStationTable_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (changeTableInCode == true)
                return;
            this.GetMachineLoadedFeeder(true);
            string strGrp = smtFacade.GetActiveStationTable(mo.MOCode, Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode, this.txtMachineCode.Value);
            if (this.cboStationTable.SelectedItemValue.ToString() == strGrp)
            {
                this.txtTableStatus.Value = UserControl.MutiLanguages.ParserString("$StationTableGroupStatus_Active");
                stationTableGroupActive = true;
                this.btnActive.Enabled = false;
                this.btnInactive.Enabled = true;
                this.txtTableStatus.InnerTextBox.ForeColor = Color.Green;
            }
            else
            {
                this.txtTableStatus.Value = UserControl.MutiLanguages.ParserString("$StationTableGroupStatus_Inactive");
                stationTableGroupActive = false;
                this.btnActive.Enabled = true;
                this.btnInactive.Enabled = false;
                this.txtTableStatus.InnerTextBox.ForeColor = Color.Red;
            }
        }

        private void btnActive_Click(object sender, System.EventArgs e)
        {
            Messages msg = new Messages();
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.BeginTransaction();
            try
            {
                msg.AddMessages(smtFacade.SetTableGroupEnabled(mo,
                    Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                    txtMachineCode.Value, this.cboStationTable.SelectedItemValue.ToString(),
                    Service.ApplicationService.Current().ResourceCode,
                    Service.ApplicationService.Current().UserCode));
                if (msg.IsSuccess() == true)
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CommitTransaction();
                    this.txtTableStatus.Value = UserControl.MutiLanguages.ParserString("$StationTableGroupStatus_Active");
                    this.txtTableStatus.InnerTextBox.ForeColor = Color.Green;
                    this.btnActive.Enabled = false;
                    this.btnInactive.Enabled = true;
                }
                else
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            }
            this.ucMessageInfo.Add(msg);
            Application.DoEvents();
            this.txtInput.TextFocus(false, true);
        }

        private void btnInactive_Click(object sender, System.EventArgs e)
        {
            Messages msg = new Messages();
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.BeginTransaction();
            try
            {
                msg.AddMessages(smtFacade.SetTableGroupDisabled(mo,
                    Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                    txtMachineCode.Value, this.cboStationTable.SelectedItemValue.ToString(),
                    Service.ApplicationService.Current().ResourceCode,
                    Service.ApplicationService.Current().UserCode));
                if (msg.IsSuccess() == true)
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CommitTransaction();
                    this.GetMOEnabled(this.txtMOCode.Value);
                    // 停线
                    if (msg.GetData() != null && msg.GetData().Values != null && msg.GetData().Values.Length > 0)
                    {
                        SMTLineControlLog line = (SMTLineControlLog)msg.GetData().Values[0];
                        // 失效则停线
                        if (FormatHelper.StringToBoolean(line.LineStatus) == false)
                        {
                            if (FSMTFeederReelWatch.Current != null)
                            {
                                FSMTFeederReelWatch.Current.StopLine();
                            }
                        }
                    }
                    this.txtTableStatus.Value = UserControl.MutiLanguages.ParserString("$StationTableGroupStatus_Inactive");
                    this.txtTableStatus.InnerTextBox.ForeColor = Color.Red;
                    this.btnActive.Enabled = true;
                    this.btnInactive.Enabled = false;
                }
                else
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            }
            this.ucMessageInfo.Add(msg);
            Application.DoEvents();
            this.txtInput.TextFocus(false, true);
        }

        private void btnActive_Load(object sender, EventArgs e)
        {

        }

    }

    public class SMTLoadFeederCommand
    {
        /// <summary>
        /// 换产品别
        /// </summary>
        public static string Machine = "CMD01";
        /// <summary>
        /// 上料
        /// </summary>
        public static string Load = "CMD02";
        /// <summary>
        /// 换料
        /// </summary>
        public static string Exchange = "CMD03";
        /// <summary>
        /// 接料
        /// </summary>
        public static string Continue = "CMD04";
        /// <summary>
        /// 下料
        /// </summary>
        public static string UnLoadSingle = "CMD05";

        /// <summary>
        /// 上料抽检
        /// </summary>
        public static string LoadCheck = "CMD06";
    }

}
