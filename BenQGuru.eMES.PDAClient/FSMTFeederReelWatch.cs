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

namespace BenQGuru.eMES.PDAClient
{
    /// <summary>
    /// FSMTFeederReelWatch 的摘要说明。
    /// </summary>
    public class FSMTFeederReelWatch : Form
    {
        private UserControl.UCLabelEdit txtLineCode;
        private UserControl.UCButton btnRefresh;
        private System.Windows.Forms.GroupBox grpAlertMusic;
        private System.Windows.Forms.GroupBox grpAlertLog;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;
        private UserControl.UCMessage ucMessage;
        private System.Windows.Forms.ListBox lstAlertMusic;
        private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
        private System.Windows.Forms.Timer tmrRefresh;
        private UserControl.UCLabelEdit txtReelAlert;
        private UserControl.UCLabelEdit txtRefreshInteval;
        private UserControl.UCLabelEdit txtReelStopLine;
        //private AxMSCommLib.AxMSComm axMSComm1;
        private System.Windows.Forms.Panel panelLineStopMsg;
        private System.Windows.Forms.Panel panelMiddle;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridList;
        private System.Windows.Forms.Label lblLineStatusTip;
        private System.Windows.Forms.Label lblLineStatus;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridLineError;
        private System.ComponentModel.IContainer components;

        public FSMTFeederReelWatch()
        {
            InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSMTFeederReelWatch));
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn1 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("LineCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("MachineCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn3 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("MachineStationCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn4 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("FeederCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn5 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("FeederLeftCount");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn6 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("FeederAlterDay");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn7 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("ReelNo");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn8 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("ReelLeftQty");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LineCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MOCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ErrorType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MachineCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MachineStationCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReelNo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReelLeftQty");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LineCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MachineCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MachineStationCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeederCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeederLeftCount");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeederAlterDay");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReelNo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReelLeftQty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NextReelNo", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NextReelLeftQty", 1);
            this.panelTop = new System.Windows.Forms.Panel();
            this.txtReelStopLine = new UserControl.UCLabelEdit();
            this.txtRefreshInteval = new UserControl.UCLabelEdit();
            this.txtReelAlert = new UserControl.UCLabelEdit();
            this.btnRefresh = new UserControl.UCButton();
            this.txtLineCode = new UserControl.UCLabelEdit();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.grpAlertLog = new System.Windows.Forms.GroupBox();
            this.ucMessage = new UserControl.UCMessage();
            this.grpAlertMusic = new System.Windows.Forms.GroupBox();
            this.lstAlertMusic = new System.Windows.Forms.ListBox();
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.panelLineStopMsg = new System.Windows.Forms.Panel();
            this.gridLineError = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.lblLineStatus = new System.Windows.Forms.Label();
            this.lblLineStatusTip = new System.Windows.Forms.Label();
            this.panelMiddle = new System.Windows.Forms.Panel();
            this.gridList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.grpAlertLog.SuspendLayout();
            this.grpAlertMusic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            this.panelLineStopMsg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLineError)).BeginInit();
            this.panelMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.txtReelStopLine);
            this.panelTop.Controls.Add(this.txtRefreshInteval);
            this.panelTop.Controls.Add(this.txtReelAlert);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.txtLineCode);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(835, 52);
            this.panelTop.TabIndex = 0;
            // 
            // txtReelStopLine
            // 
            this.txtReelStopLine.AllowEditOnlyChecked = true;
            this.txtReelStopLine.AutoUpper = true;
            this.txtReelStopLine.Caption = "料卷停线标准(%)";
            this.txtReelStopLine.Checked = false;
            this.txtReelStopLine.EditType = UserControl.EditTypes.Number;
            this.txtReelStopLine.Location = new System.Drawing.Point(627, 15);
            this.txtReelStopLine.MaxLength = 40;
            this.txtReelStopLine.Multiline = false;
            this.txtReelStopLine.Name = "txtReelStopLine";
            this.txtReelStopLine.PasswordChar = '\0';
            this.txtReelStopLine.ReadOnly = false;
            this.txtReelStopLine.ShowCheckBox = false;
            this.txtReelStopLine.Size = new System.Drawing.Size(153, 24);
            this.txtReelStopLine.TabIndex = 8;
            this.txtReelStopLine.TabNext = true;
            this.txtReelStopLine.Value = "98";
            this.txtReelStopLine.WidthType = UserControl.WidthTypes.Tiny;
            this.txtReelStopLine.XAlign = 730;
            // 
            // txtRefreshInteval
            // 
            this.txtRefreshInteval.AllowEditOnlyChecked = true;
            this.txtRefreshInteval.AutoUpper = true;
            this.txtRefreshInteval.Caption = "刷新频率(分)";
            this.txtRefreshInteval.Checked = false;
            this.txtRefreshInteval.EditType = UserControl.EditTypes.Integer;
            this.txtRefreshInteval.Location = new System.Drawing.Point(473, 15);
            this.txtRefreshInteval.MaxLength = 40;
            this.txtRefreshInteval.Multiline = false;
            this.txtRefreshInteval.Name = "txtRefreshInteval";
            this.txtRefreshInteval.PasswordChar = '\0';
            this.txtRefreshInteval.ReadOnly = false;
            this.txtRefreshInteval.ShowCheckBox = false;
            this.txtRefreshInteval.Size = new System.Drawing.Size(135, 24);
            this.txtRefreshInteval.TabIndex = 7;
            this.txtRefreshInteval.TabNext = true;
            this.txtRefreshInteval.Value = "2";
            this.txtRefreshInteval.WidthType = UserControl.WidthTypes.Tiny;
            this.txtRefreshInteval.XAlign = 558;
            this.txtRefreshInteval.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRefreshInteval_TxtboxKeyPress);
            // 
            // txtReelAlert
            // 
            this.txtReelAlert.AllowEditOnlyChecked = true;
            this.txtReelAlert.AutoUpper = true;
            this.txtReelAlert.Caption = "料卷预警标准(%)";
            this.txtReelAlert.Checked = false;
            this.txtReelAlert.EditType = UserControl.EditTypes.Number;
            this.txtReelAlert.Location = new System.Drawing.Point(301, 15);
            this.txtReelAlert.MaxLength = 40;
            this.txtReelAlert.Multiline = false;
            this.txtReelAlert.Name = "txtReelAlert";
            this.txtReelAlert.PasswordChar = '\0';
            this.txtReelAlert.ReadOnly = false;
            this.txtReelAlert.ShowCheckBox = false;
            this.txtReelAlert.Size = new System.Drawing.Size(153, 24);
            this.txtReelAlert.TabIndex = 6;
            this.txtReelAlert.TabNext = true;
            this.txtReelAlert.Value = "90";
            this.txtReelAlert.WidthType = UserControl.WidthTypes.Tiny;
            this.txtReelAlert.XAlign = 404;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.SystemColors.Control;
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.BackgroundImage")));
            this.btnRefresh.ButtonType = UserControl.ButtonTypes.None;
            this.btnRefresh.Caption = "更新";
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Location = new System.Drawing.Point(194, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(88, 22);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtLineCode
            // 
            this.txtLineCode.AllowEditOnlyChecked = true;
            this.txtLineCode.AutoUpper = true;
            this.txtLineCode.Caption = "产线编号";
            this.txtLineCode.Checked = false;
            this.txtLineCode.EditType = UserControl.EditTypes.String;
            this.txtLineCode.Location = new System.Drawing.Point(15, 15);
            this.txtLineCode.MaxLength = 40;
            this.txtLineCode.Multiline = false;
            this.txtLineCode.Name = "txtLineCode";
            this.txtLineCode.PasswordChar = '\0';
            this.txtLineCode.ReadOnly = true;
            this.txtLineCode.ShowCheckBox = false;
            this.txtLineCode.Size = new System.Drawing.Size(161, 24);
            this.txtLineCode.TabIndex = 1;
            this.txtLineCode.TabNext = true;
            this.txtLineCode.Value = "";
            this.txtLineCode.WidthType = UserControl.WidthTypes.Small;
            this.txtLineCode.XAlign = 76;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.grpAlertLog);
            this.panelBottom.Controls.Add(this.grpAlertMusic);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 301);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(835, 119);
            this.panelBottom.TabIndex = 1;
            // 
            // grpAlertLog
            // 
            this.grpAlertLog.Controls.Add(this.ucMessage);
            this.grpAlertLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAlertLog.Location = new System.Drawing.Point(0, 0);
            this.grpAlertLog.Name = "grpAlertLog";
            this.grpAlertLog.Size = new System.Drawing.Size(655, 119);
            this.grpAlertLog.TabIndex = 1;
            this.grpAlertLog.TabStop = false;
            this.grpAlertLog.Text = "预警记录";
            // 
            // ucMessage
            // 
            this.ucMessage.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessage.ButtonVisible = false;
            this.ucMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessage.Location = new System.Drawing.Point(3, 17);
            this.ucMessage.Name = "ucMessage";
            this.ucMessage.Size = new System.Drawing.Size(649, 99);
            this.ucMessage.TabIndex = 1;
            // 
            // grpAlertMusic
            // 
            this.grpAlertMusic.Controls.Add(this.lstAlertMusic);
            this.grpAlertMusic.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpAlertMusic.Location = new System.Drawing.Point(655, 0);
            this.grpAlertMusic.Name = "grpAlertMusic";
            this.grpAlertMusic.Size = new System.Drawing.Size(180, 119);
            this.grpAlertMusic.TabIndex = 0;
            this.grpAlertMusic.TabStop = false;
            this.grpAlertMusic.Text = "预警提示音";
            // 
            // lstAlertMusic
            // 
            this.lstAlertMusic.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstAlertMusic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAlertMusic.ItemHeight = 12;
            this.lstAlertMusic.Location = new System.Drawing.Point(3, 17);
            this.lstAlertMusic.Name = "lstAlertMusic";
            this.lstAlertMusic.Size = new System.Drawing.Size(174, 96);
            this.lstAlertMusic.TabIndex = 0;
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
            ultraDataColumn8});
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 5000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // panelLineStopMsg
            // 
            this.panelLineStopMsg.Controls.Add(this.gridLineError);
            this.panelLineStopMsg.Controls.Add(this.lblLineStatus);
            this.panelLineStopMsg.Controls.Add(this.lblLineStatusTip);
            this.panelLineStopMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLineStopMsg.ForeColor = System.Drawing.Color.Green;
            this.panelLineStopMsg.Location = new System.Drawing.Point(0, 420);
            this.panelLineStopMsg.Name = "panelLineStopMsg";
            this.panelLineStopMsg.Padding = new System.Windows.Forms.Padding(80, 0, 0, 0);
            this.panelLineStopMsg.Size = new System.Drawing.Size(835, 89);
            this.panelLineStopMsg.TabIndex = 2;
            // 
            // gridLineError
            // 
            this.gridLineError.Cursor = System.Windows.Forms.Cursors.Default;
            ultraGridColumn1.Header.Caption = "产线代码";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 82;
            ultraGridColumn2.Header.Caption = "工单代码";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 80;
            ultraGridColumn3.Header.Caption = "停线描述";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 180;
            ultraGridColumn4.Header.Caption = "机台";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 44;
            ultraGridColumn5.Header.Caption = "站位";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 69;
            ultraGridColumn6.Header.Caption = "料卷编号";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 93;
            ultraGridColumn7.Header.Caption = "料卷剩余数量";
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7});
            this.gridLineError.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.gridLineError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLineError.Location = new System.Drawing.Point(80, 0);
            this.gridLineError.Name = "gridLineError";
            this.gridLineError.Size = new System.Drawing.Size(755, 89);
            this.gridLineError.TabIndex = 2;
            // 
            // lblLineStatus
            // 
            this.lblLineStatus.AutoSize = true;
            this.lblLineStatus.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLineStatus.ForeColor = System.Drawing.Color.Green;
            this.lblLineStatus.Location = new System.Drawing.Point(7, 30);
            this.lblLineStatus.Name = "lblLineStatus";
            this.lblLineStatus.Size = new System.Drawing.Size(56, 22);
            this.lblLineStatus.TabIndex = 1;
            this.lblLineStatus.Text = "正常";
            // 
            // lblLineStatusTip
            // 
            this.lblLineStatusTip.AutoSize = true;
            this.lblLineStatusTip.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLineStatusTip.Location = new System.Drawing.Point(7, 7);
            this.lblLineStatusTip.Name = "lblLineStatusTip";
            this.lblLineStatusTip.Size = new System.Drawing.Size(65, 12);
            this.lblLineStatusTip.TabIndex = 0;
            this.lblLineStatusTip.Text = "产线状态：";
            // 
            // panelMiddle
            // 
            this.panelMiddle.Controls.Add(this.gridList);
            this.panelMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMiddle.Location = new System.Drawing.Point(0, 52);
            this.panelMiddle.Name = "panelMiddle";
            this.panelMiddle.Size = new System.Drawing.Size(835, 249);
            this.panelMiddle.TabIndex = 3;
            // 
            // gridList
            // 
            this.gridList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gridList.DataSource = this.ultraDataSource1;
            ultraGridColumn8.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn8.Header.Caption = "产线编号";
            ultraGridColumn8.Header.VisiblePosition = 0;
            ultraGridColumn8.Width = 41;
            ultraGridColumn9.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn9.Header.Caption = "机台代码";
            ultraGridColumn9.Header.VisiblePosition = 1;
            ultraGridColumn9.Width = 86;
            ultraGridColumn10.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn10.Header.Caption = "站位";
            ultraGridColumn10.Header.VisiblePosition = 2;
            ultraGridColumn10.Width = 70;
            ultraGridColumn11.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn11.Header.Caption = "Feeder代码";
            ultraGridColumn11.Header.VisiblePosition = 3;
            ultraGridColumn11.Width = 128;
            ultraGridColumn12.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn12.Header.Caption = "Feeder剩余次数";
            ultraGridColumn12.Header.VisiblePosition = 4;
            ultraGridColumn12.Width = 105;
            ultraGridColumn13.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn13.Header.Caption = "保养剩余天数";
            ultraGridColumn13.Header.VisiblePosition = 5;
            ultraGridColumn14.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn14.Header.Caption = "料卷编号";
            ultraGridColumn14.Header.VisiblePosition = 6;
            ultraGridColumn14.Width = 125;
            ultraGridColumn15.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn15.Header.Caption = "料卷剩余数量";
            ultraGridColumn15.Header.VisiblePosition = 7;
            ultraGridColumn15.Width = 92;
            ultraGridColumn16.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn16.Header.Caption = "下一料卷编号";
            ultraGridColumn16.Header.VisiblePosition = 8;
            ultraGridColumn17.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn17.Header.Caption = "下一料卷剩余数量";
            ultraGridColumn17.Header.VisiblePosition = 9;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17});
            this.gridList.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.gridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridList.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridList.Location = new System.Drawing.Point(0, 0);
            this.gridList.Name = "gridList";
            this.gridList.Size = new System.Drawing.Size(835, 249);
            this.gridList.TabIndex = 0;
            // 
            // FSMTFeederReelWatch
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(835, 509);
            this.Controls.Add(this.panelMiddle);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelLineStopMsg);
            this.Controls.Add(this.panelTop);
            this.Name = "FSMTFeederReelWatch";
            this.Text = "SMT机台监控";
            this.Load += new System.EventHandler(this.FSMTFeederReelWatch_Load);
            this.Closed += new System.EventHandler(this.FSMTFeederReelWatch_Closed);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FSMTFeederReelWatch_Closing);
            this.panelTop.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.grpAlertLog.ResumeLayout(false);
            this.grpAlertMusic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            this.panelLineStopMsg.ResumeLayout(false);
            this.panelLineStopMsg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLineError)).EndInit();
            this.panelMiddle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        public static FSMTFeederReelWatch Current = null;

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

        private decimal ReelAlert
        {
            get
            {
                try
                {
                    return decimal.Parse(this.txtReelAlert.Value) / 100;
                }
                catch
                {
                    return Convert.ToDecimal(0.9);
                }
            }
        }

        private decimal ReelStopLine
        {
            get
            {
                try
                {
                    return decimal.Parse(this.txtReelStopLine.Value) / 100;
                }
                catch
                {
                    return Convert.ToDecimal(0.98);
                }
            }
        }

        private DataTable tableSource = null;
        private DataTable tableLineError = null;
        private void FSMTFeederReelWatch_Load(object sender, System.EventArgs e)
        {
            smtFacade = new SMTFacade(this.DataProvider);

            tableSource = new DataTable();
            tableSource.Columns.AddRange(new DataColumn[]{
															 new DataColumn("LineCode"),
															 new DataColumn("MachineCode"),
															 new DataColumn("MachineStationCode"),
															 new DataColumn("FeederCode"),
															 new DataColumn("FeederLeftCount", typeof(int)),
                                                             new DataColumn("FeederAlterDay", typeof(int)),
															 new DataColumn("ReelNo"),
															 new DataColumn("ReelLeftQty", typeof(int)),
															 new DataColumn("IsAlert"),
															 new DataColumn("NextReelNo"),
                                                             new DataColumn("NextReelLeftQty", typeof(int)),
                                                             new DataColumn("MoCode")});
            tableSource.DefaultView.Sort = "LineCode,MachineCode,MachineStationCode";
            this.gridList.DataSource = tableSource.DefaultView;
            this.gridList.DisplayLayout.Bands[0].Columns["IsAlert"].Hidden = true;
            this.gridList.DisplayLayout.Bands[0].Columns["MoCode"].Hidden = true;
            //this.gridList.DisplayLayout.Bands[0].Columns["NextReelNo"].Hidden = true;

            tableLineError = new DataTable();
            tableLineError.Columns.AddRange(new DataColumn[]{
																new DataColumn("LineCode"),
																new DataColumn("MOCode"),
																new DataColumn("ErrorType"),
																new DataColumn("MachineCode"),
																new DataColumn("MachineStationCode"),
																new DataColumn("ReelNo"),
																new DataColumn("ReelLeftQty", typeof(int))
															});
            tableLineError.DefaultView.Sort = "LineCode,MOCode,MachineCode,MachineStationCode";
            this.gridLineError.DataSource = tableLineError.DefaultView;

            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(gridList);
            UserControl.UIStyleBuilder.GridUI(gridLineError);
            gridList.DisplayLayout.Bands[0].Columns["FeederLeftCount"].Format = "#,#";
            gridList.DisplayLayout.Bands[0].Columns["FeederLeftCount"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            gridList.DisplayLayout.Bands[0].Columns["FeederAlterDay"].Format = "#,#";
            gridList.DisplayLayout.Bands[0].Columns["FeederAlterDay"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            gridList.DisplayLayout.Bands[0].Columns["ReelLeftQty"].Format = "#,#";
            gridList.DisplayLayout.Bands[0].Columns["ReelLeftQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            gridList.DisplayLayout.Bands[0].Columns["NextReelLeftQty"].Format = "#,#";
            gridList.DisplayLayout.Bands[0].Columns["NextReelLeftQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            gridLineError.DisplayLayout.Bands[0].Columns["ReelLeftQty"].Format = "#,#";
            gridLineError.DisplayLayout.Bands[0].Columns["ReelLeftQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

            this.lblLineStatus.Text = string.Empty;
            this.txtLineCode.Value = Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode;
            this.btnRefresh_Click(null, null);

            Current = this;
        }

        private void FSMTFeederReelWatch_Closed(object sender, System.EventArgs e)
        {
            this.CloseConnection();
        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            this.txtLineCode.Value = this.txtLineCode.Value.Trim().ToUpper();
            RefreshData();
            if (tmrRefresh.Enabled == false && tableSource.Rows.Count > 0)
            {
                try
                {
                    tmrRefresh.Interval = int.Parse(this.txtRefreshInteval.Value) * 60 * 1000;
                }
                catch
                { }
                tmrRefresh.Enabled = true;
            }
            else if (tableSource.Rows.Count == 0 && tmrRefresh.Enabled == true)
                tmrRefresh.Enabled = false;
        }

        private ArrayList htMachineFeederList = null;
        private MachineFeeder[] machineFeederList = null;
        private bool bInRefresh = false;
        private void RefreshData()
        {
            if (bInRefresh == true)
                return;
            bInRefresh = true;
            //Icyer,2007/01/15 修改	减少Open/Close的次数
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            try
            {
                tableLineError.Rows.Clear();
                // 查询上料记录
                object[] machineFeederListTmp = smtFacade.LoadMachineFeederByLineMachine(this.txtLineCode.Value, string.Empty);
                if (machineFeederListTmp == null || machineFeederListTmp.Length == 0)
                {
                    //ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Cannot_Find_MachineFeeder_In_Watch"));
                    tableSource.Rows.Clear();
                    // 更新产线状态
                    RefreshLineStatusForMO();
                    // 控制产线
                    ControlLine();
                    return;
                }
                machineFeederList = new MachineFeeder[machineFeederListTmp.Length];
                machineFeederListTmp.CopyTo(machineFeederList, 0);
                // 是否需要更新上料记录
                bool bNeedUpdateSource = false;
                if (machineFeederList.Length != tableSource.Rows.Count)	// 有单独上料或下料时
                    bNeedUpdateSource = true;
                else
                {
                    if (htMachineFeederList == null || htMachineFeederList.Count != machineFeederList.Length)
                        bNeedUpdateSource = true;
                    else
                    {
                        for (int i = 0; i < machineFeederList.Length; i++)
                        {
                            string strKey = machineFeederList[i].MachineCode + "'" + machineFeederList[i].MachineStationCode + "'" + machineFeederList[i].FeederCode + "'" + machineFeederList[i].ReelNo + "'" + machineFeederList[i].NextReelNo;
                            if (htMachineFeederList.Contains(strKey) == false)
                            {
                                bNeedUpdateSource = true;
                            }
                        }
                    }
                }
                if (bNeedUpdateSource == true)
                {
                    htMachineFeederList = new ArrayList();
                    for (int i = 0; i < machineFeederList.Length; i++)
                    {
                        string strKey = machineFeederList[i].MachineCode + "'" + machineFeederList[i].MachineStationCode + "'" + machineFeederList[i].FeederCode + "'" + machineFeederList[i].ReelNo + "'" + machineFeederList[i].NextReelNo;
                        if (htMachineFeederList.Contains(strKey) == false)
                        {
                            htMachineFeederList.Add(strKey);
                        }
                    }
                }
                // 更新上料记录
                if (bNeedUpdateSource == true)
                {
                    tableSource.Rows.Clear();
                    for (int i = 0; i < machineFeederList.Length; i++)
                    {
                        DataRow row = tableSource.NewRow();
                        row["LineCode"] = machineFeederList[i].StepSequenceCode;
                        row["MachineCode"] = machineFeederList[i].MachineCode;
                        row["MachineStationCode"] = machineFeederList[i].MachineStationCode;
                        row["FeederCode"] = machineFeederList[i].FeederCode;
                        row["ReelNo"] = machineFeederList[i].ReelNo;
                        row["IsAlert"] = false;
                        row["NextReelNo"] = machineFeederList[i].NextReelNo;
                        row["MoCode"] = machineFeederList[i].MOCode;
                        tableSource.Rows.Add(row);
                    }
                }
                ArrayList listAlertFeeder = new ArrayList();
                ArrayList listAlertReel = new ArrayList();
                // 更新Feeder次数和预警日期
                object[] objsFeeder = smtFacade.LoadFeederByLineMachine(this.txtLineCode.Value, string.Empty);
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                if (objsFeeder != null)
                {
                    for (int i = 0; i < objsFeeder.Length; i++)
                    {
                        Feeder feeder = (Feeder)objsFeeder[i];
                        DataRow[] rows = tableSource.Select("FeederCode='" + feeder.FeederCode + "'");
                        if (rows != null && rows.Length > 0)
                        {
                            DateTime dateNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);
                            DateTime mDate = FormatHelper.ToDateTime(Convert.ToInt32(feeder.TheMaintainDate), 000001);
                            int alterDate = dateNow.Subtract(mDate).Days; //算法是now减去保养日期

                            rows[0]["FeederLeftCount"] = feeder.MaxCount - feeder.UsedCount;
                            rows[0]["FeederAlterDay"] = alterDate == 0 ? feeder.MaxMDay : feeder.MaxMDay - alterDate;
                            if (feeder.UsedCount >= feeder.AlertCount || alterDate > feeder.AlterMDay)
                            {
                                listAlertFeeder.Add(feeder);
                                rows[0]["IsAlert"] = true;
                            }
                        }
                    }
                }
                // 更新料卷用量
                object[] objsReel = smtFacade.LoadReelQtyByLineMachine(this.txtLineCode.Value, string.Empty);
                if (objsReel != null)
                {
                    for (int i = 0; i < objsReel.Length; i++)
                    {
                        ReelQty reel = (ReelQty)objsReel[i];
                        DataRow[] rows = tableSource.Select("ReelNo='" + reel.ReelNo + "'");
                        if (rows != null && rows.Length > 0)
                        {
                            rows[0]["ReelLeftQty"] = reel.Qty - reel.UpdatedQty - reel.UsedQty;
                            if (reel.UpdatedQty + reel.UsedQty >= reel.Qty * this.ReelAlert &&
                                rows[0]["NextReelNo"].ToString() == string.Empty)
                            {
                                listAlertReel.Add(reel);
                                rows[0]["IsAlert"] = true;
                            }
                            if (reel.UpdatedQty + reel.UsedQty >= reel.Qty * this.ReelStopLine &&
                                rows[0]["NextReelNo"].ToString() == string.Empty)
                            {
                                this.AddLineError(reel, rows[0]["ReelLeftQty"].ToString());
                            }

                            ReelQty nextReelQty = (ReelQty)smtFacade.GetReelQty(rows[0]["NextReelNo"].ToString(), rows[0]["MoCode"].ToString());
                            if (nextReelQty!=null)
                            {
                                rows[0]["NextReelLeftQty"] = nextReelQty.Qty - nextReelQty.UpdatedQty - nextReelQty.UsedQty;
                            }

                        }
                    }
                }
                // 更新预警消息
                //Application.DoEvents();
                for (int i = 0; i < this.gridList.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(this.gridList.Rows[i].Cells["IsAlert"].Value) == true)
                    {
                        this.gridList.Rows[i].Appearance.ForeColor = Color.Red;
                    }
                }


                UpdateAlert(listAlertFeeder, listAlertReel);

                // 更新产线状态
                RefreshLineStatusForMO();
                // 控制产线
                ControlLine();
            }
            catch (Exception ex)
            {
                string strErrorInfo = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "\t" + ex.Message + "\t" + ex.Source;
                strErrorInfo += "\r\n" + ex.StackTrace;
                UserControl.FileLog.FileLogOut("Client.log", strErrorInfo);
            }
            finally
            {
                bInRefresh = false;
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
            }
        }

        /// <summary>
        /// 添加停线信息
        /// </summary>
        private void AddLineError(ReelQty reel, string reelLeftQty)
        {
            DataRow[] rows = tableLineError.Select("LineCode='" + reel.StepSequenceCode + "' AND MachineCode='" + reel.MachineCode + "' AND MachineStationCode='" + reel.MachineStationCode + "'");
            DataRow row = tableLineError.NewRow();
            if (rows != null && rows.Length > 0)
                row = rows[0];
            row["LineCode"] = reel.StepSequenceCode;
            row["MOCode"] = reel.MOCode;
            row["ErrorType"] = UserControl.MutiLanguages.ParserString("$SMT_LineError_Reel_Exhaust");
            row["MachineCode"] = reel.MachineCode;
            row["MachineStationCode"] = reel.MachineStationCode;
            row["ReelNo"] = reel.ReelNo;
            row["ReelLeftQty"] = reelLeftQty;
            if (rows == null || rows.Length == 0)
            {
                tableLineError.Rows.Add(row);
                // Removed by Icye r2006/12/30	由于Loading比较大，暂时不记录Log
                //smtFacade.AddStopLineLog(reel, SMTLoadFeederOperationType.ReelExhaust, Service.ApplicationService.Current().UserCode);
            }
        }
        private void AddLineError(string errorMessage)
        {
            DataRow row = tableLineError.NewRow();
            row["LineCode"] = this.txtLineCode.Value;
            row["ErrorType"] = UserControl.MutiLanguages.ParserString(errorMessage);
            tableLineError.Rows.Add(row);
        }

        private ArrayList listAlertFeederCode = new ArrayList();
        private ArrayList listAlertReelNo = new ArrayList();
        /// <summary>
        /// 添加预警信息
        /// </summary>
        private void UpdateAlert(ArrayList listFeeder, ArrayList listReel)
        {
            Hashtable listFeederCode = new Hashtable();
            for (int i = 0; i < listFeeder.Count; i++)
            {
                smtFacade.UpdateFeederStatusInWatch((Feeder)listFeeder[i]);
                if (listAlertFeederCode.Contains(((Feeder)listFeeder[i]).FeederCode) == false)
                {
                    listFeederCode.Add(((Feeder)listFeeder[i]).FeederCode, listFeeder[i]);
                    listAlertFeederCode.Add(((Feeder)listFeeder[i]).FeederCode);
                }
            }
            Hashtable listReelNo = new Hashtable();
            for (int i = 0; i < listReel.Count; i++)
            {
                if (listAlertReelNo.Contains(((ReelQty)listReel[i]).ReelNo) == false)
                {
                    listReelNo.Add(((ReelQty)listReel[i]).ReelNo, listReel[i]);
                    listAlertReelNo.Add(((ReelQty)listReel[i]).ReelNo);
                }
            }
            if (listFeederCode.Count > 0 || listReelNo.Count > 0)
            {
                for (int i = 0; i < this.machineFeederList.Length; i++)
                {
                    if (listFeederCode.ContainsKey(this.machineFeederList[i].FeederCode) == true)
                    {
                        ShowAlertMessage(this.machineFeederList[i], listFeederCode[this.machineFeederList[i].FeederCode]);
                        smtFacade.AddSMTAlertInWatch(this.machineFeederList[i], listFeederCode[this.machineFeederList[i].FeederCode]);
                    }
                    if (listReelNo.ContainsKey(this.machineFeederList[i].ReelNo) == true)
                    {
                        ShowAlertMessage(this.machineFeederList[i], listReelNo[this.machineFeederList[i].ReelNo]);
                        smtFacade.AddSMTAlertInWatch(this.machineFeederList[i], listReelNo[this.machineFeederList[i].ReelNo]);
                    }
                }
            }
        }

        /// <summary>
        /// 显示预警信息
        /// </summary>
        private void ShowAlertMessage(MachineFeeder machineFeeder, object alertObj)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            string strMsg = ">> $MachineCode = " + machineFeeder.MachineCode + " $MachineStationCode = " + machineFeeder.MachineStationCode;
            if (alertObj is Feeder)
            {
                Feeder feeder = (Feeder)alertObj;
                DateTime dateNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);
                DateTime mDate = FormatHelper.ToDateTime(Convert.ToInt32(feeder.TheMaintainDate), 000001);
                int alterDate = dateNow.Subtract(mDate).Days; //算法是now减去保养日期

                strMsg += " $FeederCode = " + feeder.FeederCode + " $FeederMaxCount = " + feeder.MaxCount.ToString() + " $FeederAlertCount = " + feeder.AlertCount.ToString() + " $FeederUsedCount = " + feeder.UsedCount.ToString() +
                    " $MaxMaintainDate = " + feeder.MaxMDay + " $AlterDate = " + feeder.AlterMDay + " $UsedDate = " + alterDate;
            }
            else
            {
                ReelQty reel = (ReelQty)alertObj;
                strMsg += " $ReelNo = " + reel.ReelNo + " $ReelQty = " + reel.Qty.ToString() + " $ReelUsedQty = " + (reel.UsedQty + reel.UpdatedQty).ToString();
            }
            this.ucMessage.Add(new UserControl.Message(MessageType.Error, strMsg));
        }

        /// <summary>
        /// 检查工单导致的产线状态
        /// </summary>
        private void RefreshLineStatusForMO()
        {
            object[] objsmf = smtFacade.QueryEnabledMOInLine(this.txtLineCode.Value);
            if (objsmf == null || objsmf.Length == 0)
            {
                AddLineError("$SMT_No_Enabled_MO_In_Line");
            }
        }

        private void tmrRefresh_Tick(object sender, System.EventArgs e)
        {
            this.RefreshData();
        }

        private void ucBtnExit_Click(object sender, EventArgs e)
        {
            this.tmrRefresh.Enabled = false;
            while (this.bInRefresh == true)
            {
                Application.DoEvents();
            }
            this.Close();
        }

        private bool bInitComm = false;
        /// <summary>
        /// 控制产线
        /// </summary>
        private void ControlLine()
        {
            InitLineComm();
            if (this.tableLineError.Rows.Count > 0)
            {
                this.lblLineStatus.Text = UserControl.MutiLanguages.ParserString("$SMT_LineStatus_Stop");
                this.lblLineStatus.ForeColor = Color.Red;
                StopLine();
            }
            else
            {
                this.lblLineStatus.Text = UserControl.MutiLanguages.ParserString("$SMT_LineStatus_Normal");
                this.lblLineStatus.ForeColor = Color.Green;
                StartLine();
            }
        }
        private void InitLineComm()
        {
            if (bInitComm == false)
            {
                //axMSComm1.CommPort = 1;
                //axMSComm1.Settings = "9600,n,8,1";
                //axMSComm1.InBufferCount = 0;
                //axMSComm1.OutBufferCount = 0;
                //axMSComm1.InputLen = 0;
                bInitComm = true;
            }
        }
        public void StartLine()
        {
            InitLineComm();
            //if (this.axMSComm1.PortOpen == false)
            //    return;
            //this.axMSComm1.PortOpen = false;
        }
        public void StopLine()
        {
            InitLineComm();
            //if (this.axMSComm1.PortOpen == true)
            //    return;
            //this.axMSComm1.PortOpen = true;
        }

        /// <summary>
        /// 强制关闭
        /// </summary>
        public void CloseForce()
        {
            bCloseForce = true;
            this.Close();
        }

        private bool bCloseForce = false;
        private void FSMTFeederReelWatch_Closing(object sender, CancelEventArgs e)
        {
            if (bCloseForce == true)
                return;
            this.WindowState = FormWindowState.Minimized;
            e.Cancel = true;
        }

        private void txtRefreshInteval_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                {
                    this.tmrRefresh.Interval = int.Parse(this.txtRefreshInteval.Value) * 60 * 1000;
                }
            }
            catch
            { }
        }
    }
}
