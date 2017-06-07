using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using BenQGuru.eMES.PDAClient.Service;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.PDAClient
{
    /// <summary>
    /// FSMTLoadFeeder 的摘要说明。
    /// </summary>
    public class FSMTLoadFeeder : Form
    {
        private System.Windows.Forms.Panel panel1;
        private UserControl.UCLabelEdit txtMOCode;
        private UserControl.UCLabelEdit txtMachineCode;
        private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
        private System.Windows.Forms.TextBox txtMachineLoadedCount;
        private System.Windows.Forms.Label lblLoadedCount;
        private UserControl.UCLabelCombox cboStationTable;
        private UCLabelEdit txtInput;
        private UCButton btnOK;
        private UCButton ucBtnExit;
        private UCButton btnUnloadAll;
        private UCMessage ucMessageInfo;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridList;
        private Panel panel2;
        private IContainer components;
        private string currentOperationType;
        private ArrayList lstReelAcceptFeeder = new ArrayList();
        private ArrayList lstReelAcceptStationCode = new ArrayList();


        public FSMTLoadFeeder()
        {
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
            this.cboStationTable = new UserControl.UCLabelCombox();
            this.txtMachineCode = new UserControl.UCLabelEdit();
            this.txtMOCode = new UserControl.UCLabelEdit();
            this.lblLoadedCount = new System.Windows.Forms.Label();
            this.txtMachineLoadedCount = new System.Windows.Forms.TextBox();
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.txtInput = new UserControl.UCLabelEdit();
            this.btnOK = new UserControl.UCButton();
            this.ucBtnExit = new UserControl.UCButton();
            this.btnUnloadAll = new UserControl.UCButton();
            this.ucMessageInfo = new UserControl.UCMessage();
            this.gridList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboStationTable);
            this.panel1.Controls.Add(this.txtMachineCode);
            this.panel1.Controls.Add(this.txtMOCode);
            this.panel1.Controls.Add(this.lblLoadedCount);
            this.panel1.Controls.Add(this.txtMachineLoadedCount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 61);
            this.panel1.TabIndex = 0;
            // 
            // cboStationTable
            // 
            this.cboStationTable.AllowEditOnlyChecked = true;
            this.cboStationTable.Caption = "Table";
            this.cboStationTable.Checked = false;
            this.cboStationTable.Location = new System.Drawing.Point(19, 29);
            this.cboStationTable.Name = "cboStationTable";
            this.cboStationTable.SelectedIndex = -1;
            this.cboStationTable.ShowCheckBox = false;
            this.cboStationTable.Size = new System.Drawing.Size(176, 24);
            this.cboStationTable.TabIndex = 23;
            this.cboStationTable.WidthType = UserControl.WidthTypes.Normal;
            this.cboStationTable.XAlign = 62;
            this.cboStationTable.SelectedIndexChanged += new System.EventHandler(this.cboStationTable_SelectedIndexChanged);
            // 
            // txtMachineCode
            // 
            this.txtMachineCode.AllowEditOnlyChecked = true;
            this.txtMachineCode.AutoSelectAll = false;
            this.txtMachineCode.AutoUpper = true;
            this.txtMachineCode.Caption = "机台编号";
            this.txtMachineCode.Checked = false;
            this.txtMachineCode.EditType = UserControl.EditTypes.String;
            this.txtMachineCode.Location = new System.Drawing.Point(200, 4);
            this.txtMachineCode.MaxLength = 40;
            this.txtMachineCode.Multiline = false;
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.PasswordChar = '\0';
            this.txtMachineCode.ReadOnly = false;
            this.txtMachineCode.ShowCheckBox = false;
            this.txtMachineCode.Size = new System.Drawing.Size(111, 24);
            this.txtMachineCode.TabIndex = 10;
            this.txtMachineCode.TabNext = false;
            this.txtMachineCode.Value = "";
            this.txtMachineCode.WidthType = UserControl.WidthTypes.Tiny;
            this.txtMachineCode.XAlign = 261;
            this.txtMachineCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMachineCode_TxtboxKeyPress);
            // 
            // txtMOCode
            // 
            this.txtMOCode.AllowEditOnlyChecked = true;
            this.txtMOCode.AutoSelectAll = false;
            this.txtMOCode.AutoUpper = true;
            this.txtMOCode.Caption = "工单代码";
            this.txtMOCode.Checked = false;
            this.txtMOCode.EditType = UserControl.EditTypes.String;
            this.txtMOCode.Location = new System.Drawing.Point(2, 4);
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
            this.txtMOCode.XAlign = 63;
            this.txtMOCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMOCode_TxtboxKeyPress);
            // 
            // lblLoadedCount
            // 
            this.lblLoadedCount.Location = new System.Drawing.Point(200, 33);
            this.lblLoadedCount.Name = "lblLoadedCount";
            this.lblLoadedCount.Size = new System.Drawing.Size(83, 24);
            this.lblLoadedCount.TabIndex = 158;
            this.lblLoadedCount.Text = "机台缺料总数";
            // 
            // txtMachineLoadedCount
            // 
            this.txtMachineLoadedCount.Location = new System.Drawing.Point(283, 28);
            this.txtMachineLoadedCount.Name = "txtMachineLoadedCount";
            this.txtMachineLoadedCount.ReadOnly = true;
            this.txtMachineLoadedCount.Size = new System.Drawing.Size(27, 21);
            this.txtMachineLoadedCount.TabIndex = 159;
            this.txtMachineLoadedCount.Text = "0";
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
            this.txtInput.Location = new System.Drawing.Point(39, 155);
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
            this.txtInput.XAlign = 88;
            this.txtInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_TxtboxKeyPress);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.ButtonType = UserControl.ButtonTypes.None;
            this.btnOK.Caption = "确定";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Location = new System.Drawing.Point(22, 183);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 22);
            this.btnOK.TabIndex = 16;
            this.btnOK.TabStop = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(210, 183);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 17;
            // 
            // btnUnloadAll
            // 
            this.btnUnloadAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUnloadAll.BackColor = System.Drawing.SystemColors.Control;
            this.btnUnloadAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUnloadAll.BackgroundImage")));
            this.btnUnloadAll.ButtonType = UserControl.ButtonTypes.None;
            this.btnUnloadAll.Caption = "全部下料";
            this.btnUnloadAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnloadAll.Location = new System.Drawing.Point(116, 183);
            this.btnUnloadAll.Name = "btnUnloadAll";
            this.btnUnloadAll.Size = new System.Drawing.Size(88, 22);
            this.btnUnloadAll.TabIndex = 162;
            this.btnUnloadAll.TabStop = false;
            this.btnUnloadAll.Click += new System.EventHandler(this.btnUnloadAll_Click);
            // 
            // ucMessageInfo
            // 
            this.ucMessageInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessageInfo.ButtonVisible = false;
            this.ucMessageInfo.Location = new System.Drawing.Point(3, 88);
            this.ucMessageInfo.Name = "ucMessageInfo";
            this.ucMessageInfo.Size = new System.Drawing.Size(319, 60);
            this.ucMessageInfo.TabIndex = 164;
            this.ucMessageInfo.TabStop = false;
            // 
            // gridList
            // 
            this.gridList.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridList.DataSource = this.ultraDataSource1;
            this.gridList.Location = new System.Drawing.Point(3, 0);
            this.gridList.Name = "gridList";
            this.gridList.Size = new System.Drawing.Size(319, 86);
            this.gridList.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridList);
            this.panel2.Controls.Add(this.ucMessageInfo);
            this.panel2.Controls.Add(this.btnUnloadAll);
            this.panel2.Controls.Add(this.ucBtnExit);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.txtInput);
            this.panel2.Location = new System.Drawing.Point(0, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(322, 214);
            this.panel2.TabIndex = 1;
            // 
            // FSMTLoadFeeder
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(322, 266);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FSMTLoadFeeder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SMT上料";
            this.Load += new System.EventHandler(this.FSMTLoadFeeder_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            this.panel2.ResumeLayout(false);
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
            htLoad.Add(SMTLoadFeederInputType.ReelNo, new string[] { SMTLoadFeederInputType.FeederCode, "$Please_Input_FeederCode" });
            htLoad.Add(SMTLoadFeederInputType.FeederCode, new string[] { SMTLoadFeederInputType.StationCode, "$Please_Input_StationCode" });
            inputFlow.Add(SMTLoadFeederOperationType.Load, htLoad);

            Hashtable htEx = new Hashtable();
            htEx.Add(SMTLoadFeederInputType.Init, new string[] { SMTLoadFeederInputType.StationCode, "$Please_Input_StationCode" });
            htEx.Add(SMTLoadFeederInputType.StationCode, new string[] { SMTLoadFeederInputType.ReelNoOld, "$Please_Input_ReelNoOld" });
            htEx.Add(SMTLoadFeederInputType.ReelNoOld, new string[] { SMTLoadFeederInputType.ReelNo, "$Please_Input_ReelNo" });
            htEx.Add(SMTLoadFeederInputType.ReelNo, new string[] { SMTLoadFeederInputType.FeederCodeOld, "$Please_Input_FeederCodeOld" });
            htEx.Add(SMTLoadFeederInputType.FeederCodeOld, new string[] { SMTLoadFeederInputType.FeederCode, "$Please_Input_FeederCode" });
            inputFlow.Add(SMTLoadFeederOperationType.Exchange, htEx);

            Hashtable htContinue = new Hashtable();
            htContinue.Add(SMTLoadFeederInputType.Init, new string[] { SMTLoadFeederInputType.StationCode, "$Please_Input_StationCode" });
            htContinue.Add(SMTLoadFeederInputType.StationCode, new string[] { SMTLoadFeederInputType.ReelNoOld, "$Please_Input_ReelNoOld" });
            htContinue.Add(SMTLoadFeederInputType.ReelNoOld, new string[] { SMTLoadFeederInputType.ReelNo, "$Please_Input_ReelNo" });
            inputFlow.Add(SMTLoadFeederOperationType.Continue, htContinue);

            Hashtable htUnLoad = new Hashtable();
            htUnLoad.Add(SMTLoadFeederInputType.Init, new string[] { SMTLoadFeederInputType.MachineCode, "$Please_Input_MachineCode" });
            htUnLoad.Add(SMTLoadFeederInputType.MachineCode, new string[] { SMTLoadFeederInputType.StationCode, "$Please_Input_StationCode" });
            htUnLoad.Add(SMTLoadFeederInputType.StationCode, new string[] { SMTLoadFeederInputType.ReelNo, "$Please_Input_ReelNo" });
            htUnLoad.Add(SMTLoadFeederInputType.ReelNo, new string[] { SMTLoadFeederInputType.FeederCode, "$Please_Input_FeederCode" });
            inputFlow.Add(SMTLoadFeederOperationType.UnLoadSingle, htUnLoad);

            Hashtable htChk = new Hashtable();
            htChk.Add(SMTLoadFeederInputType.Init, new string[] { SMTLoadFeederInputType.MachineCode, "$Please_Input_MachineCode" });
            htChk.Add(SMTLoadFeederInputType.MachineCode, new string[] { SMTLoadFeederInputType.StationCode, "$Please_Input_StationCode" });
            htChk.Add(SMTLoadFeederInputType.StationCode, new string[] { SMTLoadFeederInputType.FeederCode, "$Please_Input_FeederCode" });
            htChk.Add(SMTLoadFeederInputType.FeederCode, new string[] { SMTLoadFeederInputType.ReelNo, "$Please_Input_ReelNo" });
            inputFlow.Add(SMTLoadFeederOperationType.LoadCheck, htChk);

            //Hashtable eff = new Hashtable();
            //htChk.Add(SMTLoadFeederInputType.Init, new string[] { SMTLoadFeederInputType.MachineCode, "$Please_Input_MachineCode" });
            //htChk.Add(SMTLoadFeederInputType.MachineCode, new string[] { SMTLoadFeederInputType.StationCode, "$Please_Input_StationCode" });
            //htChk.Add(SMTLoadFeederInputType.StationCode, new string[] { SMTLoadFeederInputType.FeederCode, "$Please_Input_FeederCode" });
            //htChk.Add(SMTLoadFeederInputType.FeederCode, new string[] { SMTLoadFeederInputType.ReelNo, "$Please_Input_ReelNo" });
            //inputFlow.Add(SMTLoadFeederOperationType.LoadCheck, htChk);

            //Hashtable inv = new Hashtable();
            //htChk.Add(SMTLoadFeederInputType.Init, new string[] { SMTLoadFeederInputType.MachineCode, "$Please_Input_MachineCode" });
            //htChk.Add(SMTLoadFeederInputType.MachineCode, new string[] { SMTLoadFeederInputType.StationCode, "$Please_Input_StationCode" });
            //htChk.Add(SMTLoadFeederInputType.StationCode, new string[] { SMTLoadFeederInputType.FeederCode, "$Please_Input_FeederCode" });
            //htChk.Add(SMTLoadFeederInputType.FeederCode, new string[] { SMTLoadFeederInputType.ReelNo, "$Please_Input_ReelNo" });
            //inputFlow.Add(SMTLoadFeederOperationType.LoadCheck, htChk);


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
            smtFacade = new SMTFacade(this.DataProvider);
            this.txtInput.TextFocus(false, true);

            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(gridList);
            gridList.DisplayLayout.Bands[0].Columns["FeederLeftCount"].Format = "#,#";
            gridList.DisplayLayout.Bands[0].Columns["FeederLeftCount"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            gridList.DisplayLayout.Bands[0].Columns["ReelLeftQty"].Format = "#,#";
            gridList.DisplayLayout.Bands[0].Columns["ReelLeftQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

            currentOperationType = SMTLoadFeederOperationType.Load;    
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
                ucMessageInfo.Add("$Please_Input_MachineCode");
                this.txtMachineCode.TextFocus(false, true);
                InitInput();
                return;
            }
            else if (strInput == SMTLoadFeederCommand.Load)
            {
                InitInput();
                bIsCmd = true;
                currentOperationType = SMTLoadFeederOperationType.Load;
                cmdChanged();
                return;

            }
            else if (strInput == SMTLoadFeederCommand.Exchange)
            {
                InitInput();
                bIsCmd = true;
                currentOperationType = SMTLoadFeederOperationType.Exchange;
                cmdChanged();
                return;
            }
            else if (strInput == SMTLoadFeederCommand.Continue)
            {
                InitInput();
                bIsCmd = true;
                currentOperationType = SMTLoadFeederOperationType.Continue;
                cmdChanged();
                return;
            }
            else if (strInput == SMTLoadFeederCommand.UnLoadSingle)
            {
                InitInput();
                bIsCmd = true;
                currentOperationType = SMTLoadFeederOperationType.UnLoadSingle;
                cmdChanged();
                return;
            }
            else if (strInput == SMTLoadFeederCommand.LoadCheck)
            {
                InitInput();
                bIsCmd = true;
                currentOperationType = SMTLoadFeederOperationType.LoadCheck;
                cmdChanged();
                return;
            }
            else if (strInput == SMTLoadFeederCommand.Effective)
            {
                InitInput();
                bIsCmd = true;
                currentOperationType = SMTLoadFeederOperationType.Effective;
                btnEnable_Click(null, null);
                this.txtInput.TextFocus(true, true);
                return;
            }
            else if (strInput == SMTLoadFeederCommand.Invalidate)
            {
                InitInput();
                bIsCmd = true;
                currentOperationType = SMTLoadFeederOperationType.Invalidate;
                btnDisable_Click(null, null);
                this.txtInput.TextFocus(true, true);
                return;
            }
            // 如果不是命令，则保存当前输入
            if (bIsCmd == false)
            {
                Hashtable ht = null;
                if (currentOperationType == SMTLoadFeederOperationType.Load)
                    ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.Load];

                else if (currentOperationType == SMTLoadFeederOperationType.Exchange)
                    ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.Exchange];

                else if (currentOperationType == SMTLoadFeederOperationType.Continue)
                    ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.Continue];

                else if (currentOperationType == SMTLoadFeederOperationType.UnLoadSingle)
                    ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.UnLoadSingle];

                else if (currentOperationType == SMTLoadFeederOperationType.LoadCheck)
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
                            this.lstReelAcceptFeeder.Clear();
                            this.lstReelAcceptStationCode.Clear();
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
            if (currentOperationType == SMTLoadFeederOperationType.Load)
                ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.Load];

            else if (currentOperationType == SMTLoadFeederOperationType.Exchange)
                ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.Exchange];

            else if (currentOperationType == SMTLoadFeederOperationType.Continue)
                ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.Continue];

            else if (currentOperationType == SMTLoadFeederOperationType.UnLoadSingle)
                ht = (Hashtable)inputFlow[SMTLoadFeederOperationType.UnLoadSingle];

            else if (currentOperationType == SMTLoadFeederOperationType.LoadCheck)
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
            if (currentOperationType == SMTLoadFeederOperationType.Load)
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

                    lstReelAcceptFeeder.Clear();
                    lstReelAcceptStationCode.Clear();
                    lstReelAcceptFeeder.AddRange(listFeeder.ToArray());
                    lstReelAcceptStationCode.AddRange(listStation.ToArray());

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
                    for (int i = 0; i < lstReelAcceptFeeder.Count; i++)
                    {
                        if (lstReelAcceptFeeder[i].ToString() == feeder.FeederSpecCode)
                        {
                            return true;
                        }
                    }
                    ucMessageInfo.Add(new UserControl.Message(MessageType.Error, "$Feeder_Isnt_In_Accept_List"));
                    return false;
                }
                else if (inputType == SMTLoadFeederInputType.StationCode)
                {
                    for (int i = 0; i < lstReelAcceptStationCode.Count; i++)
                    {
                        if (lstReelAcceptStationCode[i].ToString() == this.txtInput.Value)
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
            if (ApplicationService.Current().UserCode == string.Empty)
            {
                this.ucMessageInfo.Add(new UserControl.Message(MessageType.Error," $Error_Input_Empty"));
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
                if (currentOperationType == SMTLoadFeederOperationType.Load)
                {
                    operationType = SMTLoadFeederOperationType.Load;
                    messages = smtFacade.SMTLoadFeeder(
                        mo, txtMachineCode.Value,
                        listInput[SMTLoadFeederInputType.StationCode].ToString(),
                        listInput[SMTLoadFeederInputType.ReelNo].ToString(),
                        listInput[SMTLoadFeederInputType.FeederCode].ToString(),
                        ApplicationService.Current().UserCode,
                        tableSource,
                        Service.ApplicationService.Current().ResourceCode,
                        Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                        this.stationTableGroupActive,
                        this.cboStationTable.ComboBoxData.Text);
                    if (messages.IsSuccess())
                        messages.Add(new UserControl.Message(MessageType.Success, "$SMT_LoadItem_Success"));
                }
                else if (currentOperationType == SMTLoadFeederOperationType.Exchange)
                {
                    operationType = SMTLoadFeederOperationType.Exchange;
                    messages = smtFacade.SMTExchangeFeeder(
                        operationType, mo, txtMachineCode.Value,
                        listInput[SMTLoadFeederInputType.StationCode].ToString(),
                        listInput[SMTLoadFeederInputType.ReelNoOld].ToString(),
                        listInput[SMTLoadFeederInputType.ReelNo].ToString(),
                        listInput[SMTLoadFeederInputType.FeederCodeOld].ToString(),
                        listInput[SMTLoadFeederInputType.FeederCode].ToString(),
                        ApplicationService.Current().UserCode,
                        tableSource,
                        Service.ApplicationService.Current().ResourceCode,
                        Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                        this.cboStationTable.ComboBoxData.Text);
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
                else if (currentOperationType == SMTLoadFeederOperationType.Continue)
                {
                    operationType = SMTLoadFeederOperationType.Continue;
                    messages = smtFacade.SMTContinueFeeder(
                        operationType, mo, txtMachineCode.Value,
                        listInput[SMTLoadFeederInputType.StationCode].ToString(),
                        listInput[SMTLoadFeederInputType.ReelNoOld].ToString(),
                        listInput[SMTLoadFeederInputType.ReelNo].ToString(),
                        ApplicationService.Current().UserCode,
                        tableSource,
                        Service.ApplicationService.Current().ResourceCode,
                        Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                        this.cboStationTable.ComboBoxData.Text);
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
                else if (currentOperationType == SMTLoadFeederOperationType.UnLoadSingle)
                {
                    messages = smtFacade.SMTUnLoadFeederSingle(
                        mo,
                        listInput[SMTLoadFeederInputType.MachineCode].ToString(),
                        listInput[SMTLoadFeederInputType.StationCode].ToString(),
                        listInput[SMTLoadFeederInputType.FeederCode].ToString(),
                        listInput[SMTLoadFeederInputType.ReelNo].ToString(),
                        ApplicationService.Current().UserCode,
                        tableSource,
                        Service.ApplicationService.Current().ResourceCode,
                        Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                        this.cboStationTable.ComboBoxData.Text);
                    if (messages.IsSuccess())
                        messages.Add(new UserControl.Message(MessageType.Success, "$SMT_UnLoadSingleItem_Success"));
                }
                else if (currentOperationType == SMTLoadFeederOperationType.LoadCheck)
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
                if (currentOperationType == SMTLoadFeederOperationType.LoadCheck)		// 上料检查时不用更改Grid的内容
                {
                    DataRow[] rows = tableSource.Select("MachineStationCode='" + listInput[SMTLoadFeederInputType.StationCode].ToString() + "'");
                    for (int i = 0; i < tableSource.Rows.Count; i++)
                    {
                        if (tableSource.Rows[i]["MachineStationCode"].ToString() == listInput[SMTLoadFeederInputType.StationCode].ToString())
                        {
                            if (messages.IsSuccess() == false)
                            {
                                if ((currentOperationType == SMTLoadFeederOperationType.LoadCheck) && tableSource.Rows[i]["ReelNo"].ToString() == string.Empty)
                                    gridList.Rows[i].Appearance.ForeColor = Color.Red;
                            }
                            else if ((currentOperationType == SMTLoadFeederOperationType.UnLoadSingle) && tableSource.Rows[i]["ReelNo"].ToString() == string.Empty)
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
                    //PlayAlertMusic(operationType);
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


        private void GetMOEnabled(string moCode)
        {
            object[] objstmp = smtFacade.QueryMachineFeeder(moCode, string.Empty, string.Empty, 1, 1);
            bool bEnabled = false;
            if (objstmp != null && objstmp.Length > 0 && FormatHelper.StringToBoolean(((MachineFeeder)objstmp[0]).Enabled) == true)
                bEnabled = true;
            if (bEnabled == true)
            {
                //this.txtMOStatus.Value = UserControl.MutiLanguages.ParserString("$SMTMOEnabledStatus_Enabled");

            }
            else
            {
                //this.txtMOStatus.Value = UserControl.MutiLanguages.ParserString("$SMTMOEnabledStatus_Disabled");
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
                                    tableSource.Rows[n]["MachineStationCode"].ToString() == mf.MachineStationCode &&
                                    tableSource.Rows[n]["FeederSpecCode"].ToString() == mf.FeederSpecCode &&
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
                                    row["ReelLeftQty"] = reelQty.Qty - reelQty.UsedQty;
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
                                    row["NextReelLeftQty"] = nextReelQty.Qty - nextReelQty.UsedQty;
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
            //this.txtTableStatus.Value = UserControl.MutiLanguages.ParserString("$StationTableGroupStatus_Inactive");
            stationTableGroupActive = false;

            string strGrp = smtFacade.GetActiveStationTable(mo.MOCode, Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode, this.txtMachineCode.Value);
            for (int i = 0; i < this.cboStationTable.ComboBoxData.Items.Count; i++)
            {
                if (this.cboStationTable.ComboBoxData.Items[i].ToString() == strGrp)
                {
                    this.cboStationTable.SelectedIndex = i;
                    //this.txtTableStatus.Value = UserControl.MutiLanguages.ParserString("$StationTableGroupStatus_Active");
                    stationTableGroupActive = true;

                    break;
                }
            }
            if (this.cboStationTable.ComboBoxData.Items.Count <= 1)
            {
                this.cboStationTable.Enabled = false;

                //this.txtTableStatus.Value = UserControl.MutiLanguages.ParserString("$StationTableGroupStatus_Active");
                stationTableGroupActive = true;

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

        private void cmdChanged()
        {
            if (this.txtMOCode.Value != string.Empty && this.txtMachineCode.Value != string.Empty)
            {
                InitInput();
                ShowCurrentMessage();
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
                    ApplicationService.Current().UserCode,
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
            if (msg.IsSuccess() == true)
            {
                GetMachineLoadedFeeder();
            }
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
                    ApplicationService.Current().UserCode));
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
                //this.txtTableStatus.Value = UserControl.MutiLanguages.ParserString("$StationTableGroupStatus_Active");
                stationTableGroupActive = true;
            }
            else
            {
                //this.txtTableStatus.Value = UserControl.MutiLanguages.ParserString("$StationTableGroupStatus_Inactive");
                stationTableGroupActive = false;

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
                    //this.txtTableStatus.Value = UserControl.MutiLanguages.ParserString("$StationTableGroupStatus_Active");

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
                    //this.txtTableStatus.Value = UserControl.MutiLanguages.ParserString("$StationTableGroupStatus_Inactive");
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

        private void txtMachineCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.GetMachineLoadedFeeder();
                this.txtInput.TextFocus(true, true);
            }
            
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

        /// <summary>
        /// 工单生效
        /// </summary>
        public static string Effective = "CMD07";

        /// <summary>
        /// 工单失效
        /// </summary>
        public static string Invalidate = "CMD08";
    }

}
