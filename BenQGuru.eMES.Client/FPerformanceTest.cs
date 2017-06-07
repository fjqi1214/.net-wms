using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.DataCollect ;
using BenQGuru.eMES.DataCollect.Action ;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FPerformanceTest 的摘要说明。
	/// </summary>
	public class FPerformanceTest : System.Windows.Forms.Form
	{
		private UserControl.UCLabelEdit txtRCardPrefix;
		private UserControl.UCLabelEdit txtRCardLength;
		private UserControl.UCLabelEdit txtRCardStart;
		public UserControl.UCButton btnStartCurrent;
		private UserControl.UCButton btnPauseCurrent;
		private UserControl.UCLabelEdit txtCostMax;
		private UserControl.UCLabelEdit txtCostMin;
		private UserControl.UCLabelEdit txtCostAvg;
		public UserControl.UCLabelEdit txtTimerInterval;
		private UserControl.UCLabelEdit txtRCardCurrent;
		private UserControl.UCLabelEdit txtSSCode;
		private UserControl.UCLabelEdit txtMOCode;
		private System.Windows.Forms.TextBox txtLog;
		private UserControl.UCLabelEdit txtRunCount;
		private UserControl.UCLabelCombox cbxProcType;
		private UserControl.UCLabelEdit txtINNO;
		private UserControl.UCLabelEdit txtSplitCount;
		private UserControl.UCLabelEdit txtRCardEnd;
		private System.Windows.Forms.TextBox txtFlow;
		private UserControl.UCButton btnApplyFlow;
		private UserControl.UCLabelEdit txtKeyPartPrefix;
		private UserControl.UCLabelEdit txtLotPrefix;
		private UserControl.UCLabelEdit txtLotStart;
		private UserControl.UCLabelEdit txtLotLen;
		private UserControl.UCLabelEdit txtLotSize;
		private UserControl.UCLabelEdit txtThreadCount;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FPerformanceTest()
		{
			InitializeComponent();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FPerformanceTest));
			this.txtTimerInterval = new UserControl.UCLabelEdit();
			this.txtCostAvg = new UserControl.UCLabelEdit();
			this.txtCostMin = new UserControl.UCLabelEdit();
			this.txtCostMax = new UserControl.UCLabelEdit();
			this.txtRCardCurrent = new UserControl.UCLabelEdit();
			this.btnPauseCurrent = new UserControl.UCButton();
			this.btnStartCurrent = new UserControl.UCButton();
			this.txtRCardStart = new UserControl.UCLabelEdit();
			this.txtRCardLength = new UserControl.UCLabelEdit();
			this.txtRCardPrefix = new UserControl.UCLabelEdit();
			this.txtSSCode = new UserControl.UCLabelEdit();
			this.txtMOCode = new UserControl.UCLabelEdit();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.txtRunCount = new UserControl.UCLabelEdit();
			this.cbxProcType = new UserControl.UCLabelCombox();
			this.txtINNO = new UserControl.UCLabelEdit();
			this.txtSplitCount = new UserControl.UCLabelEdit();
			this.txtRCardEnd = new UserControl.UCLabelEdit();
			this.txtFlow = new System.Windows.Forms.TextBox();
			this.btnApplyFlow = new UserControl.UCButton();
			this.txtKeyPartPrefix = new UserControl.UCLabelEdit();
			this.txtLotPrefix = new UserControl.UCLabelEdit();
			this.txtLotStart = new UserControl.UCLabelEdit();
			this.txtLotLen = new UserControl.UCLabelEdit();
			this.txtLotSize = new UserControl.UCLabelEdit();
			this.txtThreadCount = new UserControl.UCLabelEdit();
			this.SuspendLayout();
			// 
			// txtTimerInterval
			// 
			this.txtTimerInterval.AllowEditOnlyChecked = true;
			this.txtTimerInterval.Caption = "间隔时间";
			this.txtTimerInterval.Checked = false;
			this.txtTimerInterval.EditType = UserControl.EditTypes.String;
			this.txtTimerInterval.Location = new System.Drawing.Point(312, 152);
			this.txtTimerInterval.MaxLength = 40;
			this.txtTimerInterval.Multiline = false;
			this.txtTimerInterval.Name = "txtTimerInterval";
			this.txtTimerInterval.PasswordChar = '\0';
			this.txtTimerInterval.ReadOnly = false;
			this.txtTimerInterval.ShowCheckBox = false;
			this.txtTimerInterval.Size = new System.Drawing.Size(112, 24);
			this.txtTimerInterval.TabIndex = 19;
			this.txtTimerInterval.TabNext = true;
			this.txtTimerInterval.Value = "10";
			this.txtTimerInterval.WidthType = UserControl.WidthTypes.Tiny;
			this.txtTimerInterval.XAlign = 374;
			// 
			// txtCostAvg
			// 
			this.txtCostAvg.AllowEditOnlyChecked = true;
			this.txtCostAvg.Caption = "平均消耗";
			this.txtCostAvg.Checked = false;
			this.txtCostAvg.EditType = UserControl.EditTypes.String;
			this.txtCostAvg.Location = new System.Drawing.Point(456, 120);
			this.txtCostAvg.MaxLength = 40;
			this.txtCostAvg.Multiline = false;
			this.txtCostAvg.Name = "txtCostAvg";
			this.txtCostAvg.PasswordChar = '\0';
			this.txtCostAvg.ReadOnly = false;
			this.txtCostAvg.ShowCheckBox = false;
			this.txtCostAvg.Size = new System.Drawing.Size(195, 24);
			this.txtCostAvg.TabIndex = 18;
			this.txtCostAvg.TabNext = true;
			this.txtCostAvg.Value = "";
			this.txtCostAvg.WidthType = UserControl.WidthTypes.Normal;
			this.txtCostAvg.XAlign = 518;
			// 
			// txtCostMin
			// 
			this.txtCostMin.AllowEditOnlyChecked = true;
			this.txtCostMin.Caption = "最小消耗";
			this.txtCostMin.Checked = false;
			this.txtCostMin.EditType = UserControl.EditTypes.String;
			this.txtCostMin.Location = new System.Drawing.Point(240, 120);
			this.txtCostMin.MaxLength = 40;
			this.txtCostMin.Multiline = false;
			this.txtCostMin.Name = "txtCostMin";
			this.txtCostMin.PasswordChar = '\0';
			this.txtCostMin.ReadOnly = false;
			this.txtCostMin.ShowCheckBox = false;
			this.txtCostMin.Size = new System.Drawing.Size(195, 24);
			this.txtCostMin.TabIndex = 17;
			this.txtCostMin.TabNext = true;
			this.txtCostMin.Value = "";
			this.txtCostMin.WidthType = UserControl.WidthTypes.Normal;
			this.txtCostMin.XAlign = 302;
			// 
			// txtCostMax
			// 
			this.txtCostMax.AllowEditOnlyChecked = true;
			this.txtCostMax.Caption = "最大消耗";
			this.txtCostMax.Checked = false;
			this.txtCostMax.EditType = UserControl.EditTypes.String;
			this.txtCostMax.Location = new System.Drawing.Point(24, 120);
			this.txtCostMax.MaxLength = 40;
			this.txtCostMax.Multiline = false;
			this.txtCostMax.Name = "txtCostMax";
			this.txtCostMax.PasswordChar = '\0';
			this.txtCostMax.ReadOnly = false;
			this.txtCostMax.ShowCheckBox = false;
			this.txtCostMax.Size = new System.Drawing.Size(195, 24);
			this.txtCostMax.TabIndex = 16;
			this.txtCostMax.TabNext = true;
			this.txtCostMax.Value = "";
			this.txtCostMax.WidthType = UserControl.WidthTypes.Normal;
			this.txtCostMax.XAlign = 86;
			// 
			// txtRCardCurrent
			// 
			this.txtRCardCurrent.AllowEditOnlyChecked = true;
			this.txtRCardCurrent.Caption = "当前序列号";
			this.txtRCardCurrent.Checked = false;
			this.txtRCardCurrent.EditType = UserControl.EditTypes.String;
			this.txtRCardCurrent.Location = new System.Drawing.Point(228, 88);
			this.txtRCardCurrent.MaxLength = 40;
			this.txtRCardCurrent.Multiline = false;
			this.txtRCardCurrent.Name = "txtRCardCurrent";
			this.txtRCardCurrent.PasswordChar = '\0';
			this.txtRCardCurrent.ReadOnly = false;
			this.txtRCardCurrent.ShowCheckBox = false;
			this.txtRCardCurrent.Size = new System.Drawing.Size(207, 24);
			this.txtRCardCurrent.TabIndex = 15;
			this.txtRCardCurrent.TabNext = true;
			this.txtRCardCurrent.Value = "";
			this.txtRCardCurrent.WidthType = UserControl.WidthTypes.Normal;
			this.txtRCardCurrent.XAlign = 302;
			// 
			// btnPauseCurrent
			// 
			this.btnPauseCurrent.BackColor = System.Drawing.SystemColors.Control;
			this.btnPauseCurrent.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPauseCurrent.BackgroundImage")));
			this.btnPauseCurrent.ButtonType = UserControl.ButtonTypes.None;
			this.btnPauseCurrent.Caption = "暂停";
			this.btnPauseCurrent.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnPauseCurrent.Location = new System.Drawing.Point(552, 153);
			this.btnPauseCurrent.Name = "btnPauseCurrent";
			this.btnPauseCurrent.Size = new System.Drawing.Size(88, 22);
			this.btnPauseCurrent.TabIndex = 14;
			this.btnPauseCurrent.Click += new System.EventHandler(this.btnPauseCurrent_Click);
			// 
			// btnStartCurrent
			// 
			this.btnStartCurrent.BackColor = System.Drawing.SystemColors.Control;
			this.btnStartCurrent.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStartCurrent.BackgroundImage")));
			this.btnStartCurrent.ButtonType = UserControl.ButtonTypes.None;
			this.btnStartCurrent.Caption = "开始";
			this.btnStartCurrent.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnStartCurrent.Location = new System.Drawing.Point(448, 153);
			this.btnStartCurrent.Name = "btnStartCurrent";
			this.btnStartCurrent.Size = new System.Drawing.Size(88, 22);
			this.btnStartCurrent.TabIndex = 13;
			this.btnStartCurrent.Click += new System.EventHandler(this.btnStartCurrent_Click);
			// 
			// txtRCardStart
			// 
			this.txtRCardStart.AllowEditOnlyChecked = true;
			this.txtRCardStart.Caption = "起始序列号";
			this.txtRCardStart.Checked = false;
			this.txtRCardStart.EditType = UserControl.EditTypes.String;
			this.txtRCardStart.Location = new System.Drawing.Point(376, 56);
			this.txtRCardStart.MaxLength = 40;
			this.txtRCardStart.Multiline = false;
			this.txtRCardStart.Name = "txtRCardStart";
			this.txtRCardStart.PasswordChar = '\0';
			this.txtRCardStart.ReadOnly = false;
			this.txtRCardStart.ShowCheckBox = false;
			this.txtRCardStart.Size = new System.Drawing.Size(124, 24);
			this.txtRCardStart.TabIndex = 5;
			this.txtRCardStart.TabNext = true;
			this.txtRCardStart.Value = "";
			this.txtRCardStart.WidthType = UserControl.WidthTypes.Tiny;
			this.txtRCardStart.XAlign = 450;
			// 
			// txtRCardLength
			// 
			this.txtRCardLength.AllowEditOnlyChecked = true;
			this.txtRCardLength.Caption = "序列号长度";
			this.txtRCardLength.Checked = false;
			this.txtRCardLength.EditType = UserControl.EditTypes.String;
			this.txtRCardLength.Location = new System.Drawing.Point(228, 56);
			this.txtRCardLength.MaxLength = 40;
			this.txtRCardLength.Multiline = false;
			this.txtRCardLength.Name = "txtRCardLength";
			this.txtRCardLength.PasswordChar = '\0';
			this.txtRCardLength.ReadOnly = false;
			this.txtRCardLength.ShowCheckBox = false;
			this.txtRCardLength.Size = new System.Drawing.Size(124, 24);
			this.txtRCardLength.TabIndex = 4;
			this.txtRCardLength.TabNext = true;
			this.txtRCardLength.Value = "";
			this.txtRCardLength.WidthType = UserControl.WidthTypes.Tiny;
			this.txtRCardLength.XAlign = 302;
			// 
			// txtRCardPrefix
			// 
			this.txtRCardPrefix.AllowEditOnlyChecked = true;
			this.txtRCardPrefix.Caption = "序列号前缀";
			this.txtRCardPrefix.Checked = false;
			this.txtRCardPrefix.EditType = UserControl.EditTypes.String;
			this.txtRCardPrefix.Location = new System.Drawing.Point(12, 56);
			this.txtRCardPrefix.MaxLength = 40;
			this.txtRCardPrefix.Multiline = false;
			this.txtRCardPrefix.Name = "txtRCardPrefix";
			this.txtRCardPrefix.PasswordChar = '\0';
			this.txtRCardPrefix.ReadOnly = false;
			this.txtRCardPrefix.ShowCheckBox = false;
			this.txtRCardPrefix.Size = new System.Drawing.Size(207, 24);
			this.txtRCardPrefix.TabIndex = 3;
			this.txtRCardPrefix.TabNext = true;
			this.txtRCardPrefix.Value = "";
			this.txtRCardPrefix.WidthType = UserControl.WidthTypes.Normal;
			this.txtRCardPrefix.XAlign = 86;
			// 
			// txtSSCode
			// 
			this.txtSSCode.AllowEditOnlyChecked = true;
			this.txtSSCode.Caption = "产线代码";
			this.txtSSCode.Checked = false;
			this.txtSSCode.EditType = UserControl.EditTypes.String;
			this.txtSSCode.Location = new System.Drawing.Point(240, 24);
			this.txtSSCode.MaxLength = 40;
			this.txtSSCode.Multiline = false;
			this.txtSSCode.Name = "txtSSCode";
			this.txtSSCode.PasswordChar = '\0';
			this.txtSSCode.ReadOnly = false;
			this.txtSSCode.ShowCheckBox = false;
			this.txtSSCode.Size = new System.Drawing.Size(195, 24);
			this.txtSSCode.TabIndex = 2;
			this.txtSSCode.TabNext = true;
			this.txtSSCode.Value = "";
			this.txtSSCode.WidthType = UserControl.WidthTypes.Normal;
			this.txtSSCode.XAlign = 302;
			// 
			// txtMOCode
			// 
			this.txtMOCode.AllowEditOnlyChecked = true;
			this.txtMOCode.Caption = "工单代码";
			this.txtMOCode.Checked = false;
			this.txtMOCode.EditType = UserControl.EditTypes.String;
			this.txtMOCode.Location = new System.Drawing.Point(24, 24);
			this.txtMOCode.MaxLength = 40;
			this.txtMOCode.Multiline = false;
			this.txtMOCode.Name = "txtMOCode";
			this.txtMOCode.PasswordChar = '\0';
			this.txtMOCode.ReadOnly = false;
			this.txtMOCode.ShowCheckBox = false;
			this.txtMOCode.Size = new System.Drawing.Size(195, 24);
			this.txtMOCode.TabIndex = 1;
			this.txtMOCode.TabNext = true;
			this.txtMOCode.Value = "";
			this.txtMOCode.WidthType = UserControl.WidthTypes.Normal;
			this.txtMOCode.XAlign = 86;
			// 
			// txtLog
			// 
			this.txtLog.Location = new System.Drawing.Point(16, 456);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLog.Size = new System.Drawing.Size(648, 88);
			this.txtLog.TabIndex = 20;
			this.txtLog.Text = "";
			// 
			// txtRunCount
			// 
			this.txtRunCount.AllowEditOnlyChecked = true;
			this.txtRunCount.Caption = "执行次数";
			this.txtRunCount.Checked = false;
			this.txtRunCount.EditType = UserControl.EditTypes.String;
			this.txtRunCount.Location = new System.Drawing.Point(24, 88);
			this.txtRunCount.MaxLength = 40;
			this.txtRunCount.Multiline = false;
			this.txtRunCount.Name = "txtRunCount";
			this.txtRunCount.PasswordChar = '\0';
			this.txtRunCount.ReadOnly = false;
			this.txtRunCount.ShowCheckBox = false;
			this.txtRunCount.Size = new System.Drawing.Size(112, 24);
			this.txtRunCount.TabIndex = 21;
			this.txtRunCount.TabNext = true;
			this.txtRunCount.Value = "";
			this.txtRunCount.WidthType = UserControl.WidthTypes.Tiny;
			this.txtRunCount.XAlign = 86;
			// 
			// cbxProcType
			// 
			this.cbxProcType.AllowEditOnlyChecked = true;
			this.cbxProcType.Caption = "运行类型";
			this.cbxProcType.Checked = false;
			this.cbxProcType.Location = new System.Drawing.Point(24, 152);
			this.cbxProcType.Name = "cbxProcType";
			this.cbxProcType.SelectedIndex = -1;
			this.cbxProcType.ShowCheckBox = false;
			this.cbxProcType.Size = new System.Drawing.Size(262, 24);
			this.cbxProcType.TabIndex = 22;
			this.cbxProcType.WidthType = UserControl.WidthTypes.Long;
			this.cbxProcType.XAlign = 86;
			// 
			// txtINNO
			// 
			this.txtINNO.AllowEditOnlyChecked = true;
			this.txtINNO.Caption = "集成料号";
			this.txtINNO.Checked = false;
			this.txtINNO.EditType = UserControl.EditTypes.String;
			this.txtINNO.Location = new System.Drawing.Point(24, 194);
			this.txtINNO.MaxLength = 40;
			this.txtINNO.Multiline = false;
			this.txtINNO.Name = "txtINNO";
			this.txtINNO.PasswordChar = '\0';
			this.txtINNO.ReadOnly = false;
			this.txtINNO.ShowCheckBox = false;
			this.txtINNO.Size = new System.Drawing.Size(195, 24);
			this.txtINNO.TabIndex = 23;
			this.txtINNO.TabNext = true;
			this.txtINNO.Value = "";
			this.txtINNO.WidthType = UserControl.WidthTypes.Normal;
			this.txtINNO.XAlign = 86;
			// 
			// txtSplitCount
			// 
			this.txtSplitCount.AllowEditOnlyChecked = true;
			this.txtSplitCount.Caption = "分板比例";
			this.txtSplitCount.Checked = false;
			this.txtSplitCount.EditType = UserControl.EditTypes.Integer;
			this.txtSplitCount.Location = new System.Drawing.Point(240, 194);
			this.txtSplitCount.MaxLength = 40;
			this.txtSplitCount.Multiline = false;
			this.txtSplitCount.Name = "txtSplitCount";
			this.txtSplitCount.PasswordChar = '\0';
			this.txtSplitCount.ReadOnly = false;
			this.txtSplitCount.ShowCheckBox = false;
			this.txtSplitCount.Size = new System.Drawing.Size(195, 24);
			this.txtSplitCount.TabIndex = 24;
			this.txtSplitCount.TabNext = true;
			this.txtSplitCount.Value = "";
			this.txtSplitCount.WidthType = UserControl.WidthTypes.Normal;
			this.txtSplitCount.XAlign = 302;
			// 
			// txtRCardEnd
			// 
			this.txtRCardEnd.AllowEditOnlyChecked = true;
			this.txtRCardEnd.Caption = "结束序列号";
			this.txtRCardEnd.Checked = false;
			this.txtRCardEnd.EditType = UserControl.EditTypes.String;
			this.txtRCardEnd.Location = new System.Drawing.Point(528, 56);
			this.txtRCardEnd.MaxLength = 40;
			this.txtRCardEnd.Multiline = false;
			this.txtRCardEnd.Name = "txtRCardEnd";
			this.txtRCardEnd.PasswordChar = '\0';
			this.txtRCardEnd.ReadOnly = false;
			this.txtRCardEnd.ShowCheckBox = false;
			this.txtRCardEnd.Size = new System.Drawing.Size(124, 24);
			this.txtRCardEnd.TabIndex = 25;
			this.txtRCardEnd.TabNext = true;
			this.txtRCardEnd.Value = "";
			this.txtRCardEnd.WidthType = UserControl.WidthTypes.Tiny;
			this.txtRCardEnd.XAlign = 602;
			// 
			// txtFlow
			// 
			this.txtFlow.Location = new System.Drawing.Point(16, 336);
			this.txtFlow.Multiline = true;
			this.txtFlow.Name = "txtFlow";
			this.txtFlow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtFlow.Size = new System.Drawing.Size(504, 112);
			this.txtFlow.TabIndex = 26;
			this.txtFlow.Text = "资源代码,ProcType索引,工单代码,序列号前缀,集成料号,结束序列号";
			// 
			// btnApplyFlow
			// 
			this.btnApplyFlow.BackColor = System.Drawing.SystemColors.Control;
			this.btnApplyFlow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApplyFlow.BackgroundImage")));
			this.btnApplyFlow.ButtonType = UserControl.ButtonTypes.None;
			this.btnApplyFlow.Caption = "应用流程";
			this.btnApplyFlow.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnApplyFlow.Location = new System.Drawing.Point(536, 336);
			this.btnApplyFlow.Name = "btnApplyFlow";
			this.btnApplyFlow.Size = new System.Drawing.Size(88, 22);
			this.btnApplyFlow.TabIndex = 27;
			this.btnApplyFlow.Click += new System.EventHandler(this.btnApplyFlow_Click);
			// 
			// txtKeyPartPrefix
			// 
			this.txtKeyPartPrefix.AllowEditOnlyChecked = true;
			this.txtKeyPartPrefix.Caption = "KeyPart前缀";
			this.txtKeyPartPrefix.Checked = false;
			this.txtKeyPartPrefix.EditType = UserControl.EditTypes.String;
			this.txtKeyPartPrefix.Location = new System.Drawing.Point(438, 194);
			this.txtKeyPartPrefix.MaxLength = 40;
			this.txtKeyPartPrefix.Multiline = false;
			this.txtKeyPartPrefix.Name = "txtKeyPartPrefix";
			this.txtKeyPartPrefix.PasswordChar = '\0';
			this.txtKeyPartPrefix.ReadOnly = false;
			this.txtKeyPartPrefix.ShowCheckBox = false;
			this.txtKeyPartPrefix.Size = new System.Drawing.Size(213, 24);
			this.txtKeyPartPrefix.TabIndex = 28;
			this.txtKeyPartPrefix.TabNext = true;
			this.txtKeyPartPrefix.Value = "";
			this.txtKeyPartPrefix.WidthType = UserControl.WidthTypes.Normal;
			this.txtKeyPartPrefix.XAlign = 518;
			// 
			// txtLotPrefix
			// 
			this.txtLotPrefix.AllowEditOnlyChecked = true;
			this.txtLotPrefix.Caption = "批号前缀";
			this.txtLotPrefix.Checked = false;
			this.txtLotPrefix.EditType = UserControl.EditTypes.String;
			this.txtLotPrefix.Location = new System.Drawing.Point(24, 224);
			this.txtLotPrefix.MaxLength = 40;
			this.txtLotPrefix.Multiline = false;
			this.txtLotPrefix.Name = "txtLotPrefix";
			this.txtLotPrefix.PasswordChar = '\0';
			this.txtLotPrefix.ReadOnly = false;
			this.txtLotPrefix.ShowCheckBox = false;
			this.txtLotPrefix.Size = new System.Drawing.Size(195, 24);
			this.txtLotPrefix.TabIndex = 29;
			this.txtLotPrefix.TabNext = true;
			this.txtLotPrefix.Value = "";
			this.txtLotPrefix.WidthType = UserControl.WidthTypes.Normal;
			this.txtLotPrefix.XAlign = 86;
			// 
			// txtLotStart
			// 
			this.txtLotStart.AllowEditOnlyChecked = true;
			this.txtLotStart.Caption = "起始批号";
			this.txtLotStart.Checked = false;
			this.txtLotStart.EditType = UserControl.EditTypes.String;
			this.txtLotStart.Location = new System.Drawing.Point(388, 224);
			this.txtLotStart.MaxLength = 40;
			this.txtLotStart.Multiline = false;
			this.txtLotStart.Name = "txtLotStart";
			this.txtLotStart.PasswordChar = '\0';
			this.txtLotStart.ReadOnly = false;
			this.txtLotStart.ShowCheckBox = false;
			this.txtLotStart.Size = new System.Drawing.Size(112, 24);
			this.txtLotStart.TabIndex = 31;
			this.txtLotStart.TabNext = true;
			this.txtLotStart.Value = "";
			this.txtLotStart.WidthType = UserControl.WidthTypes.Tiny;
			this.txtLotStart.XAlign = 450;
			// 
			// txtLotLen
			// 
			this.txtLotLen.AllowEditOnlyChecked = true;
			this.txtLotLen.Caption = "批号长度";
			this.txtLotLen.Checked = false;
			this.txtLotLen.EditType = UserControl.EditTypes.String;
			this.txtLotLen.Location = new System.Drawing.Point(240, 224);
			this.txtLotLen.MaxLength = 40;
			this.txtLotLen.Multiline = false;
			this.txtLotLen.Name = "txtLotLen";
			this.txtLotLen.PasswordChar = '\0';
			this.txtLotLen.ReadOnly = false;
			this.txtLotLen.ShowCheckBox = false;
			this.txtLotLen.Size = new System.Drawing.Size(112, 24);
			this.txtLotLen.TabIndex = 30;
			this.txtLotLen.TabNext = true;
			this.txtLotLen.Value = "";
			this.txtLotLen.WidthType = UserControl.WidthTypes.Tiny;
			this.txtLotLen.XAlign = 302;
			// 
			// txtLotSize
			// 
			this.txtLotSize.AllowEditOnlyChecked = true;
			this.txtLotSize.Caption = "批量";
			this.txtLotSize.Checked = false;
			this.txtLotSize.EditType = UserControl.EditTypes.String;
			this.txtLotSize.Location = new System.Drawing.Point(561, 224);
			this.txtLotSize.MaxLength = 40;
			this.txtLotSize.Multiline = false;
			this.txtLotSize.Name = "txtLotSize";
			this.txtLotSize.PasswordChar = '\0';
			this.txtLotSize.ReadOnly = false;
			this.txtLotSize.ShowCheckBox = false;
			this.txtLotSize.Size = new System.Drawing.Size(87, 24);
			this.txtLotSize.TabIndex = 32;
			this.txtLotSize.TabNext = true;
			this.txtLotSize.Value = "";
			this.txtLotSize.WidthType = UserControl.WidthTypes.Tiny;
			this.txtLotSize.XAlign = 598;
			// 
			// txtThreadCount
			// 
			this.txtThreadCount.AllowEditOnlyChecked = true;
			this.txtThreadCount.Caption = "模拟线程数";
			this.txtThreadCount.Checked = false;
			this.txtThreadCount.EditType = UserControl.EditTypes.Integer;
			this.txtThreadCount.Location = new System.Drawing.Point(528, 24);
			this.txtThreadCount.MaxLength = 40;
			this.txtThreadCount.Multiline = false;
			this.txtThreadCount.Name = "txtThreadCount";
			this.txtThreadCount.PasswordChar = '\0';
			this.txtThreadCount.ReadOnly = false;
			this.txtThreadCount.ShowCheckBox = false;
			this.txtThreadCount.Size = new System.Drawing.Size(124, 24);
			this.txtThreadCount.TabIndex = 33;
			this.txtThreadCount.TabNext = true;
			this.txtThreadCount.Value = "1";
			this.txtThreadCount.WidthType = UserControl.WidthTypes.Tiny;
			this.txtThreadCount.XAlign = 602;
			// 
			// FPerformanceTest
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(680, 549);
			this.Controls.Add(this.txtThreadCount);
			this.Controls.Add(this.txtLotSize);
			this.Controls.Add(this.txtLotPrefix);
			this.Controls.Add(this.txtLotStart);
			this.Controls.Add(this.txtLotLen);
			this.Controls.Add(this.txtKeyPartPrefix);
			this.Controls.Add(this.btnApplyFlow);
			this.Controls.Add(this.txtFlow);
			this.Controls.Add(this.txtLog);
			this.Controls.Add(this.txtRCardEnd);
			this.Controls.Add(this.txtSplitCount);
			this.Controls.Add(this.txtINNO);
			this.Controls.Add(this.cbxProcType);
			this.Controls.Add(this.txtRunCount);
			this.Controls.Add(this.txtRCardPrefix);
			this.Controls.Add(this.btnStartCurrent);
			this.Controls.Add(this.txtRCardStart);
			this.Controls.Add(this.txtRCardLength);
			this.Controls.Add(this.txtTimerInterval);
			this.Controls.Add(this.txtCostAvg);
			this.Controls.Add(this.txtCostMin);
			this.Controls.Add(this.txtCostMax);
			this.Controls.Add(this.txtRCardCurrent);
			this.Controls.Add(this.txtSSCode);
			this.Controls.Add(this.txtMOCode);
			this.Controls.Add(this.btnPauseCurrent);
			this.Name = "FPerformanceTest";
			this.Text = "自动测试";
			this.Load += new System.EventHandler(this.FPerformanceTest_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private IDomainDataProvider _domainDataProvider = Service.ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		private ArrayList listFlow = null;	// 处理流程
		private int iListFlowIndex = -1;		// 流程索引
		private SMTFacade smtFacade = null;
		private MO mo = null;
		private void FPerformanceTest_Load(object sender, System.EventArgs e)
		{
			smtFacade = new SMTFacade(this.DataProvider);
			this.txtSSCode.Value = Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode;
			
			this.cbxProcType.Clear();
			this.cbxProcType.AddItem("0-SMT自动接料", "SMTCONTINUE");
			this.cbxProcType.AddItem("1-SMT序列号上料", "SMTRCARDLOAD");
			this.cbxProcType.AddItem("2-归属工单+GOOD采集", "GOMO_GOOD");
			this.cbxProcType.AddItem("3-GOOD采集", "GOOD");
			this.cbxProcType.AddItem("4-测试采集", "TEST");
			this.cbxProcType.AddItem("5-集成上料", "INNO");
			this.cbxProcType.AddItem("6-归属工单+集成上料", "GOMO_INNO");
			this.cbxProcType.AddItem("7-归属工单+集成上料+KeyPart上料", "GOMO_INNO_KEYPART");
			this.cbxProcType.AddItem("8-分板", "SPLIT");
			this.cbxProcType.AddItem("9-序号转换", "CHGSN");
			this.cbxProcType.AddItem("10-包装", "PACKING");
			this.cbxProcType.AddItem("", "");

//			listFlow = new ArrayList();
//			listFlow.Add(new string[]{"310_VI_01", "1", "VA89144MOTO001", "VA89144MOTO001", "", ""});
//			listFlow.Add(new string[]{"310_AOI_01", "2", "VA89144MOTO001", "VA89144MOTO001", "", ""});
//			listFlow.Add(new string[]{"310_SPLIT_01", "7", "VA89144MOTO001", "VA89144MOTO001", "", "2"});
//			listFlow.Add(new string[]{"310_FUNC_01", "2", "VA89144MOTO001", "VA89144MOTO001-1-", "", ""});
		}

		private void btnApplyFlow_Click(object sender, System.EventArgs e)
		{
			smtFacade = new SMTFacade(this.DataProvider);
			smtFacade.SensorCollect("SMTLINE11", "1", "1");
			return;
			// 资源代码，ProcType索引，工单代码，序列号前缀，集成料号，结束序列号
			listFlow = new ArrayList();
			if (this.txtFlow.Text == string.Empty)
				return;
			string[] strLine = this.txtFlow.Text.Replace("\n", "").Split('\r');
			for (int i = 1; i < strLine.Length; i++)
			{
				if (strLine[i].Trim() != string.Empty)
				{
					string[] strData = strLine[i].Split(',');
					if (strData.Length != 6)
					{
						MessageBox.Show("Error: " + strLine[i]);
						listFlow = null;
						return;
					}
					if ((strData[0] == "1" || strData[0] == "4" || strData[0] == "5" || strData[0] == "6") && 
						strData[4] == string.Empty)
					{
						MessageBox.Show("没有集成料号");
						listFlow = null;
						return;
					}
					if (strData[0] == "6" && this.txtKeyPartPrefix.Value == string.Empty)
					{
						MessageBox.Show("没有KeyPart");
						listFlow = null;
						return;
					}
					if (strData[0] == "9" && this.txtLotPrefix.Value == string.Empty)
					{
						MessageBox.Show("没有批号");
						listFlow = null;
						return;
					}
					if (strData[0] == "7" && this.txtSplitCount.Value == string.Empty)
					{
						MessageBox.Show("没有分板比例");
						listFlow = null;
						return;
					}
					listFlow.Add(strData);
				}
			}
			MessageBox.Show("OK");
		}

		private bool ChangeFlowNode()
		{
			iListFlowIndex++;
			if ((listFlow == null || listFlow.Count == 1) && iListFlowIndex == 0)
				return true;
			if (listFlow == null || iListFlowIndex >= listFlow.Count)
			{
				return false;
			}
			string[] strData = (string[])listFlow[iListFlowIndex];
			if (strData[0] != Service.ApplicationService.Current().ResourceCode)
			{
				BenQGuru.eMES.BaseSetting.BaseModelFacade baseFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
				BenQGuru.eMES.Domain.BaseSetting.Resource res = (BenQGuru.eMES.Domain.BaseSetting.Resource)baseFacade.GetResource(strData[0].Trim());
				Service.ApplicationService.Current().LoginInfo.Resource = res;
			}
			this.cbxProcType.SelectedIndex = int.Parse(strData[1].Trim());
			this.txtMOCode.Value = strData[2].Trim();
			this.txtRCardPrefix.Value = strData[3].Trim();
			this.txtINNO.Value = strData[4].Trim();
			this.txtRCardEnd.Value = strData[5].Trim();

			intCurrentRCard = 0;
			intRunCount = -1;
			lngRunCost = 0;
			intCostMax = 0;
			intCostMin = int.MaxValue;
			this.txtCostMax.Value = "";
			this.txtCostMin.Value = "";
			this.txtCostAvg.Value = "";
			this.txtRCardCurrent.Value = "";
			this.txtRunCount.Value = "";

			txtLog.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\tRES\t" + strData[0] + "\r\n";
			string strResource = string.Format("{0}/{1}/{2}", 
				Service.ApplicationService.Current().LoginInfo.Resource.ResourceCode,
				Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
				Service.ApplicationService.Current().LoginInfo.Resource.SegmentCode);
			Service.ApplicationService.Current().MainWindows.LoginResource = strResource;

			return true;
		}

		private Timer timer = null;
		private void btnStartCurrent_Click(object sender, System.EventArgs e)
		{
			string strReelNo = "06051504051\n\r06051";
			SMTFacade tmpF = new SMTFacade(this.DataProvider);
			object objTmp = tmpF.GetReel(strReelNo);
			
			BenQGuru.eMES.MOModel.MOFacade moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
			mo = (MO)moFacade.GetMO(txtMOCode.Value);
			if (mo == null)
			{
				txtLog.Text += "MO Not Exist: " + txtMOCode.Value + "\r\n";
				return;
			}
			if (timer != null && timer.Enabled == true)
				return;
			if (timer == null)
			{
				if (ChangeFlowNode() == false)
					return;
				timer = new Timer();
				timer.Interval = Convert.ToInt32(this.txtTimerInterval.Value) * 1000;
				timer.Tick += new EventHandler(timer_Tick);
				timer.Enabled = true;
			}
			else if (timer.Enabled == false)
			{
				timer.Interval = Convert.ToInt32(this.txtTimerInterval.Value) * 1000;
				timer.Enabled = true;
			}
			btnStartCurrent.Enabled = false;
			btnPauseCurrent.Enabled = true;
		}

		private void btnPauseCurrent_Click(object sender, System.EventArgs e)
		{
			if (timer != null)
				timer.Enabled = false;
			
			btnStartCurrent.Enabled = true;
			btnPauseCurrent.Enabled = false;
		}

		private int intCurrentRCard = 0;
		private int intRunCount = -1;
		private long lngRunCost = 0;
		private int intCostMax = 0;
		private int intCostMin = int.MaxValue;
		public void timer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (this.cbxProcType.SelectedIndex == 0)
				{
					AutoContinueReel();
					return;
				}
				intCurrentRCard++;
				int iRCardSeq = intCurrentRCard + Convert.ToInt32(this.txtRCardStart.Value);
				string rcard = this.txtRCardPrefix.Value + iRCardSeq.ToString().PadLeft(int.Parse(this.txtRCardLength.Value), '0');
				DateTime dtStart = DateTime.Now;
				Messages messages = new Messages();
				/*
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.OpenConnection();
				this.DataProvider.BeginTransaction();
				*/
				if (this.cbxProcType.SelectedItemValue.ToString() == "SMTRCARDLOAD")	// SMT序列号上料
				{
					messages.AddMessages(this.GoMo(rcard));
					if (messages.IsSuccess() == true)
						messages.AddMessages(this.Good(rcard));
					if (messages.IsSuccess() == true)
						messages.AddMessages(this.LoadMaterialForRCard(rcard));
				}
				else if (this.cbxProcType.SelectedItemValue.ToString() == "GOMO_GOOD")	//归属工单+GOOD采集
				{
					messages.AddMessages(this.GoMo(rcard));
					if (messages.IsSuccess() == true)
						messages.AddMessages(this.Good(rcard));
				}
				else if (this.cbxProcType.SelectedItemValue.ToString() == "GOOD")	//GOOD采集
				{
					messages.AddMessages(this.Good(rcard));
				}
				else if (this.cbxProcType.SelectedItemValue.ToString() == "TEST")	//测试采集
				{
					messages.AddMessages(this.Test(rcard));
				}
				else if (this.cbxProcType.SelectedItemValue.ToString() == "INNO")	//集成上料
				{
					messages.AddMessages(this.LoadINNO(rcard));
				}
				else if (this.cbxProcType.SelectedItemValue.ToString() == "GOMO_INNO")	//归属工单+集成上料
				{
					messages.AddMessages(this.GoMo(rcard));
					if (messages.IsSuccess() == true)
						messages.AddMessages(this.LoadINNO(rcard));
				}
				else if (this.cbxProcType.SelectedItemValue.ToString() == "GOMO_INNO_KEYPART")	//归属工单+集成上料+KeyPart上料
				{
					messages.AddMessages(this.GoMo(rcard));
					if (messages.IsSuccess() == true)
						messages.AddMessages(this.LoadINNO(rcard));
					if (messages.IsSuccess() == true)
						messages.AddMessages(this.LoadKeyPart(rcard));
				}
				else if (this.cbxProcType.SelectedItemValue.ToString() == "SPLIT")	//分板
				{
					messages.AddMessages(this.Split(rcard));
				}
				else if (this.cbxProcType.SelectedItemValue.ToString() == "CHGSN")	//序号转换
				{
					messages.AddMessages(this.ChangeSN(rcard));
				}
				else if (this.cbxProcType.SelectedItemValue.ToString() == "PACKING")	//包装
				{
					messages.AddMessages(this.Packing(rcard));
				}
				/*
				if (messages.IsSuccess() == true)
					this.DataProvider.CommitTransaction();
				else
					this.DataProvider.RollbackTransaction();
				*/
				if (messages.IsSuccess() == false)
				{
					txtLog.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + intCurrentRCard.ToString() + "\t" + messages.OutPut() + "\r\n";
				}
				if (intRunCount == -1)
				{
					intRunCount = 0;
					return;
				}
				intRunCount++;
				TimeSpan ts = DateTime.Now - dtStart;
				lngRunCost = lngRunCost + Convert.ToInt64(ts.TotalMilliseconds);
				if (ts.TotalMilliseconds > intCostMax)
					intCostMax = Convert.ToInt32(ts.TotalMilliseconds);
				if (ts.TotalMilliseconds < intCostMin)
					intCostMin = Convert.ToInt32(ts.TotalMilliseconds);
				this.txtCostMax.Value = intCostMax.ToString();
				this.txtCostMin.Value = intCostMin.ToString();
				this.txtCostAvg.Value = (lngRunCost / intRunCount).ToString();
				this.txtRCardCurrent.Value = rcard;
				this.txtRunCount.Value = intRunCount.ToString();

				if (this.txtRCardEnd.Value != string.Empty && iRCardSeq >= int.Parse(this.txtRCardEnd.Value))
				{
					if (ChangeFlowNode() == false)
					{
						txtLog.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\tEND\r\n";
						btnPauseCurrent_Click(sender, e);
					}
				}
			}
			catch (Exception ex)
			{
				txtLog.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + ex.Message + "\r\n";
			}
		}

		//归属工单采集
		private Messages GoMo(string rcard)
		{
			ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
			Messages messages =  onLine.GetIDInfo(rcard);
			ProductInfo product=(ProductInfo)messages.GetData().Values[0];
			IAction dataCollectMO = new ActionFactory(this._domainDataProvider).CreateAction(ActionType.DataCollectAction_GoMO);
			messages.AddMessages(((IActionWithStatus)dataCollectMO).Execute(
				new GoToMOActionEventArgs(ActionType.DataCollectAction_GoMO, 
				rcard,
				Service.ApplicationService.Current().UserCode,
				Service.ApplicationService.Current().ResourceCode,
				product,
				this.txtMOCode.Value)));
			if (messages.IsSuccess() == false)
			{
				this.txtLog.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + rcard + "\tGOMO\t" + messages.OutPut() + "\r\n";
			}
			return messages;
		}

		//GOOD采集
		private Messages Good(string rcard)
		{
			ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
			Messages messages =  onLine.GetIDInfo(rcard);
			ProductInfo product=(ProductInfo)messages.GetData().Values[0];
			BenQGuru.eMES.DataCollect.DataCollectFacade dcFacade = new BenQGuru.eMES.DataCollect.DataCollectFacade(this._domainDataProvider);
			dcFacade.ActionCollectGood(
				rcard,
				Service.ApplicationService.Current().UserCode,
				Service.ApplicationService.Current().ResourceCode ); 
			if (messages.IsSuccess() == false)
			{
				this.txtLog.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + rcard + "\tGOOD\t" + messages.OutPut() + "\r\n";
			}
			return messages;
		}

		//测试采集
		private Messages Test(string rcard)
		{
			int ngRate = 50;	//NG百分比
			System.Random rnd = new Random();
			int iRnd = rnd.Next(100);
			if (iRnd > ngRate)
			{
				return this.Good(rcard);
			}
			ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
			Messages messages =  onLine.GetIDInfo(rcard);
			ProductInfo product=(ProductInfo)messages.GetData().Values[0];
			BenQGuru.eMES.TSModel.TSModelFacade tsFacade = new BenQGuru.eMES.TSModel.TSModelFacade(this.DataProvider);
			object[] errorCodeGroups = tsFacade.GetErrorCodeGroupByItemCode(product.LastSimulation.ItemCode);
			int iCount = rnd.Next(4);
			if (iCount < 1)
				iCount = 1;
			BenQGuru.eMES.Domain.TSModel.ErrorCodeGroup2ErrorCode[] SelectedErrorCodes = new BenQGuru.eMES.Domain.TSModel.ErrorCodeGroup2ErrorCode[iCount];
			ArrayList listTmp = new ArrayList();
			for (int i = 0; i < iCount; i++)
			{
				while (true)
				{
					iRnd = rnd.Next(errorCodeGroups.Length);
					if (iRnd >= errorCodeGroups.Length)
						iRnd = errorCodeGroups.Length - 1;
					BenQGuru.eMES.Domain.TSModel.ErrorCodeGroupA errorGroup = ((BenQGuru.eMES.Domain.TSModel.ErrorCodeGroupA)errorCodeGroups[iRnd]);
					object[] errorCodes = tsFacade.GetSelectedErrorCodeByErrorCodeGroupCode(errorGroup.ErrorCodeGroup);
					iRnd = rnd.Next(errorCodes.Length);
					if (iRnd >= errorCodes.Length)
						iRnd = errorCodes.Length - 1;
					BenQGuru.eMES.Domain.TSModel.ErrorCodeA errorCode = (BenQGuru.eMES.Domain.TSModel.ErrorCodeA)errorCodes[iRnd];
					if (listTmp.Contains( errorGroup.ErrorCodeGroup + ":" + errorCode.ErrorCode) == false)
					{
						listTmp.Add(errorGroup.ErrorCodeGroup + ":" + errorCode.ErrorCode);
						BenQGuru.eMES.Domain.TSModel.ErrorCodeGroup2ErrorCode selectedErrorCode = new BenQGuru.eMES.Domain.TSModel.ErrorCodeGroup2ErrorCode();
						selectedErrorCode.ErrorCodeGroup = errorGroup.ErrorCodeGroup;
						selectedErrorCode.ErrorCode = errorCode.ErrorCode;
						SelectedErrorCodes[i] = selectedErrorCode;
						break;
					}
				}
			}
			IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_NG);
			messages.AddMessages( ((IAction)dataCollectModule).Execute(
				new TSActionEventArgs(ActionType.DataCollectAction_NG,
				rcard,
				Service.ApplicationService.Current().UserCode,
				Service.ApplicationService.Current().ResourceCode,
				product,
				SelectedErrorCodes, 
				null,
				string.Empty) ));
			if (messages.IsSuccess() == false)
			{
				this.txtLog.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + rcard + "\tNG\t" + messages.OutPut() + "\r\n";
			}
			return messages;
		}

		//SMT序列号上料
		private Messages LoadMaterialForRCard(string rcard)
		{
			Messages messages = smtFacade.LoadMaterialForRCard(rcard, Service.ApplicationService.Current().ResourceCode, Service.ApplicationService.Current().UserCode);
			return messages;
		}

		//集成上料
		private Messages LoadINNO(string rcard)
		{
			ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
			Messages messages =  onLine.GetIDInfo(rcard);
			ProductInfo product=(ProductInfo)messages.GetData().Values[0];
			BenQGuru.eMES.DataCollect.DataCollectFacade dcFacade = new BenQGuru.eMES.DataCollect.DataCollectFacade(this._domainDataProvider);
			messages.AddMessages(onLine.Action(new CINNOActionEventArgs(
				ActionType.DataCollectAction_CollectINNO,
				rcard,
				Service.ApplicationService.Current().UserCode,
				Service.ApplicationService.Current().ResourceCode,
				product,
				txtINNO.Value.Trim().ToUpper(),
				null
				)));
			if (messages.IsSuccess() == false)
			{
				this.txtLog.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + rcard + "\tINNO\t" + messages.OutPut() + "\r\n";
			}
			return messages;
		}

		// KeyPart上料
		private Messages LoadKeyPart(string rcard)
		{
			ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
			Messages messages =  onLine.GetIDInfo(rcard);
			ProductInfo product=(ProductInfo)messages.GetData().Values[0];
			BenQGuru.eMES.MOModel.OPBOMFacade opBOMFacade=new BenQGuru.eMES.MOModel.OPBOMFacade(this.DataProvider);
			object[] objBomDetail = opBOMFacade.GetOPBOMDetails(product.LastSimulation.MOCode,
				product.LastSimulation.RouteCode,product.LastSimulation.OPCode);
			OPBomKeyparts opBomKeyparts = new OPBomKeyparts(objBomDetail,Convert.ToInt32( mo.IDMergeRule),this.DataProvider);
			string strKeyPart = this.txtKeyPartPrefix.Value.Trim().ToUpper() + rcard.Substring(this.txtRCardPrefix.Value.Length);
			opBomKeyparts.AddKeyparts(strKeyPart, this.txtMOCode.Value);
			messages.AddMessages(onLine.Action(new CKeypartsActionEventArgs( 
				ActionType.DataCollectAction_CollectKeyParts,
				rcard,
				Service.ApplicationService.Current().UserCode,
				Service.ApplicationService.Current().ResourceCode,
				product,
				opBomKeyparts,
				null)));
			if (messages.IsSuccess() == false)
			{
				this.txtLog.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + rcard + "\tKeyPart\t" + messages.OutPut() + "\r\n";
			}
			return messages;
		}

		// 分板
		private Messages Split(string rcard)
		{
			ArrayList list = new ArrayList();
			for (int i = 1; i <= int.Parse(this.txtSplitCount.Value); i++)
			{
				list.Add(this.txtRCardPrefix.Value + "S0" + i.ToString() + rcard.Substring(this.txtRCardPrefix.Value.Length));
			}
			ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
			Messages messages =  onLine.GetIDInfo(rcard);
			ProductInfo product=(ProductInfo)messages.GetData().Values[0];
			SplitIDActionEventArgs args = new SplitIDActionEventArgs(
				ActionType.DataCollectAction_Split, 
				rcard, 
				Service.ApplicationService.Current().UserCode,
				Service.ApplicationService.Current().ResourceCode,
				product, 
				(object[])list.ToArray(),
				IDMergeType.IDMERGETYPE_ROUTER,
				true,
				Convert.ToInt32(product.LastSimulation.RunningCardSequence),
				true);
			messages.AddMessages(onLine.Action(args));
			if (messages.IsSuccess() == false)
			{
				this.txtLog.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + rcard + "\tSPLIT\t" + messages.OutPut() + "\r\n";
			}
			return messages;
		}

		// 序号转换
		private Messages ChangeSN(string rcard)
		{
			ArrayList list = new ArrayList();
			list.Add(this.txtRCardPrefix.Value + "-0-" + rcard.Substring(this.txtRCardPrefix.Value.Length));
			ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
			Messages messages =  onLine.GetIDInfo(rcard);
			ProductInfo product=(ProductInfo)messages.GetData().Values[0];
			SplitIDActionEventArgs args = new SplitIDActionEventArgs(
				ActionType.DataCollectAction_Split, 
				rcard, 
				Service.ApplicationService.Current().UserCode,
				Service.ApplicationService.Current().ResourceCode,
				product, 
				(object[])list.ToArray(),
				IDMergeType.IDMERGETYPE_IDMERGE,
				true,
				Convert.ToInt32(product.LastSimulation.RunningCardSequence),
				false);
			messages.AddMessages(onLine.Action(args));
			if (messages.IsSuccess() == false)
			{
				this.txtLog.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + rcard + "\tCHGSN\t" + messages.OutPut() + "\r\n";
			}
			return messages;
		}
		
		private int intCurrentLotNo = 0;
		private int intPackingCount = 0;
		// 包装
		private Messages Packing(string rcard)
		{
			if (intPackingCount % int.Parse(this.txtLotSize.Value) == 0)
			{
				intCurrentLotNo++;
				intPackingCount = 0;
			}
			int iLotNo = int.Parse(this.txtLotStart.Value) + intCurrentLotNo;
			string lotNo = this.txtLotPrefix.Value + iLotNo.ToString().PadLeft(int.Parse(this.txtLotLen.Value), '0');
			if (intPackingCount == 0)
			{
				this.CreateNewOQCLot(lotNo);
			}
			
			ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
			Messages messages =  onLine.GetIDInfo(rcard);
			ProductInfo product=(ProductInfo)messages.GetData().Values[0];
			OQCLotAddIDEventArgs args = new OQCLotAddIDEventArgs(
				ActionType.DataCollectAction_OQCLotAddID,
				rcard,
				Service.ApplicationService.Current().UserCode,
				Service.ApplicationService.Current().ResourceCode,
				lotNo,
				OQCLotType.OQCLotType_Normal,
				false,
				int.Parse(this.txtLotSize.Value),
				false,
				product);
			args.CartonNo = "CARTON" + lotNo;
			args.CartonMemo = string.Empty;
			args.CollectType = BenQGuru.eMES.OQC.OQCFacade.OQC_ExameObject_PCS;
			messages.AddMessages(onLine.Action(args));
			if (messages.IsSuccess() == false)
			{
				this.txtLog.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + rcard + "\tPACKING\t" + messages.OutPut() + "\r\n";
			}
			intPackingCount++;
			return messages;
		}
		private void CreateNewOQCLot(string lotNo)
		{
			BenQGuru.eMES.OQC.OQCFacade _oqcFacade = new BenQGuru.eMES.OQC.OQCFacade(DataProvider);
			object obj = _oqcFacade.GetOQCLot(lotNo, OQCHelper.Lot_Sequence_Default);
			if (obj != null)
				return;
			BenQGuru.eMES.Domain.OQC.OQCLot  newOQCLot = _oqcFacade.CreateNewOQCLot();

			newOQCLot.LOTNO = lotNo;
			newOQCLot.AcceptSize = 0;
			newOQCLot.AcceptSize1 = 0;
			newOQCLot.AcceptSize2 = 0;
			newOQCLot.AQL =0;
			newOQCLot.AQL1 =0;
			newOQCLot.AQL2 =0;
			newOQCLot.LotSequence = OQCHelper.Lot_Sequence_Default;
			newOQCLot.OQCLotType = OQCLotType.OQCLotType_Normal;
			newOQCLot.LotSize= 0;
			newOQCLot.LOTStatus = OQCLotStatus.OQCLotStatus_Initial;
			newOQCLot.LOTTimes =0;
			newOQCLot.MaintainUser = Service.ApplicationService.Current().UserCode;
			newOQCLot.RejectSize = 0;
			newOQCLot.RejectSize1 =0;
			newOQCLot.RejectSize2 =0;
			newOQCLot.SampleSize =0;

			_oqcFacade.AddOQCLot(newOQCLot);
		}

		/// <summary>
		/// 自动接料
		/// </summary>
		private void AutoContinueReel()
		{
			// 查询需要快用完的料卷
			string strSql = "SELECT * FROM tblReelQty WHERE UsedQty+UpdatedQty>=Qty*0.8 ";
			strSql += " AND ReelNo IN (SELECT ReelNo FROM tblMachineFeeder WHERE (NextReelNo='' OR NextReelNo IS NULL) AND Enabled='1' ) ";
			object[] objs = this.DataProvider.CustomQuery(typeof(ReelQty), new SQLCondition(strSql));
			if (objs == null || objs.Length == 0)
				return;
			for (int i = 0; i < objs.Length; i++)
			{
				ReelQty reelQty = (ReelQty)objs[i];
				string reelNo = string.Empty;
				// 查询未使用的料卷
				strSql = "SELECT * FROM tblReel WHERE UsedFlag='0' AND Qty*0.8>UsedQty AND PartNo='" + reelQty.MaterialCode + "' AND RowNum=1 ";
				object[] objsReel = this.DataProvider.CustomQuery(typeof(Reel), new SQLCondition(strSql));
				if (objsReel != null && objsReel.Length > 0)
				{
					reelNo = ((Reel)objsReel[0]).ReelNo;
					strSql = "UPDATE tblReel SET UsedFlag='1',MOCode='" + reelQty.MOCode + "',SSCode='" + reelQty.StepSequenceCode + "' WHERE ReelNo='" + reelNo + "' ";
					this.DataProvider.CustomExecute(new SQLCondition(strSql));
				}
				else
				{
					Reel reelNew = new Reel();
					reelNew.ReelNo = System.Guid.NewGuid().ToString();
					reelNew.PartNo = reelQty.MaterialCode;
					reelNew.IsSpecial = "0";
					reelNew.MaintainUser = "SYSTEM";
					reelNew.Qty = 5000;
					reelNew.UsedFlag = "1";
					reelNew.MOCode = reelQty.MOCode;
					reelNew.StepSequenceCode = reelQty.StepSequenceCode;
					reelNo = reelNew.ReelNo;
					smtFacade.AddReel(reelNew);
				}
				
				Feeder feeder = (Feeder)smtFacade.GetFeeder(reelQty.FeederCode);
				Reel reel = (Reel)smtFacade.GetReel(reelNo);
				BenQGuru.eMES.MOModel.MOFacade moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
				MO mo1 = (MO)moFacade.GetMO(reelQty.MOCode);
				strSql = "SELECT * FROM tblSMTFeederMaterial WHERE ProductCode='" + mo1.ItemCode + "' AND MachineStationCode='" + reelQty.MachineStationCode + "' AND SSCode='" + reelQty.StepSequenceCode + "' ";
				object[] objsTmp = this.DataProvider.CustomQuery(typeof(SMTFeederMaterial), new SQLCondition(strSql));
				SMTFeederMaterial feederMaterial = (SMTFeederMaterial)objsTmp[0];
				smtFacade.AddMachineFeederPass(mo1, feeder, reel, feederMaterial, Service.ApplicationService.Current().UserCode, null, SMTLoadFeederOperationType.Continue, Service.ApplicationService.Current().ResourceCode, reelQty.StepSequenceCode, reelQty.FeederCode, reelQty.ReelNo);
				//txtLog.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + reelQty.ReelNo + "\r\n";;
			}
		}

		// 自动备料
		private void AutoLoadFeeder()
		{
			string moCode = this.txtMOCode.Value;
			string ssCode = this.txtSSCode.Value;
			BenQGuru.eMES.MOModel.MOFacade moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
			MO mo = (MO)moFacade.GetMO(moCode);
			string strSql = "SELECT * FROM tblSMTFeederMaterial WHERE ProductCode='" + mo.ItemCode + "' AND SSCode='" + ssCode + "' ";
			object[] objsList = this.DataProvider.CustomQuery(typeof(SMTFeederMaterial), new SQLCondition(strSql));
			if (objsList == null)
			{
				return;
			}
			for (int i = 0; i < objsList.Length; i++)
			{
				SMTFeederMaterial feederMaterial = (SMTFeederMaterial)objsList[i];
				// 领用Feeder
				strSql = "SELECT * FROM tblFeeder WHERE FeederSpecCode='" + feederMaterial.FeederSpecCode + "' AND UseFlag='0' AND RowNum=1 ";
				object[] objsFeeder = this.DataProvider.CustomQuery(typeof(Feeder), new SQLCondition(strSql));
				Feeder feeder = null;
				if (objsFeeder == null || objsFeeder.Length == 0)
				{
					feeder = new Feeder();
					feeder.FeederCode = System.Guid.NewGuid().ToString().ToUpper();
					feeder.FeederSpecCode = feederMaterial.FeederSpecCode;
					feeder.MaintainUser = "SYSTEM";
					feeder.Status = "NORMAL";
					feeder.MaxCount = 50000;
					feeder.AlertCount = 49000;
					feeder.UseFlag = "1";
					feeder.MOCode = moCode;
					feeder.StepSequenceCode = ssCode;
					smtFacade.AddFeeder(feeder);
				}
				else
				{
					feeder = (Feeder)objsFeeder[0];
					feeder.UseFlag = "1";
					feeder.MOCode = moCode;
					feeder.StepSequenceCode = ssCode;
					smtFacade.UpdateFeeder(feeder);
				}
				// 领用Reel
				strSql = "SELECT * FROM tblReel WHERE PartNo='" + feederMaterial.MaterialCode + "' AND UsedFlag='0' AND RowNum=1 ";
				object[] objsReel = this.DataProvider.CustomQuery(typeof(Reel), new SQLCondition(strSql));
				Reel reel = null;
				if (objsReel != null && objsReel.Length > 0)
				{
					reel = (Reel)objsReel[0];
					reel.UsedFlag = "1";
					reel.MOCode = moCode;
					reel.StepSequenceCode = ssCode;
					smtFacade.UpdateReel(reel);
				}
				else
				{
					reel = new Reel();
					reel.ReelNo = System.Guid.NewGuid().ToString().ToUpper();
					reel.PartNo = feederMaterial.MaterialCode;
					reel.Qty = 5000;
					reel.IsSpecial = "0";
					reel.MaintainUser = "SYSTEM";
					reel.UsedFlag = "1";
					reel.MOCode = moCode;
					reel.StepSequenceCode = ssCode;
					smtFacade.AddReel(reel);
				}
				// 上料
				smtFacade.AddMachineFeederPass(mo, feeder, reel, feederMaterial, 
					Service.ApplicationService.Current().UserCode, null, SMTLoadFeederOperationType.Load, 
					Service.ApplicationService.Current().ResourceCode, 
					ssCode,
					string.Empty, string.Empty);
			}
			// 工单生效
			Messages msg = smtFacade.SMTEnabledMachineFeeder(moCode, ssCode, true, Service.ApplicationService.Current().UserCode);
			if (msg.IsSuccess() == true)
			{
				msg.Add(new UserControl.Message(MessageType.Success, "$MO_Enabled_Success"));
			}
			txtLog.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + msg.OutPut() + "\r\n";
		}

		private void GenerateInitINNO()
		{
			smtFacade.SMTEnabledMachineFeeder(this.txtMOCode.Value, this.txtSSCode.Value, true, "SYSTEM");
		}
		
		private void LoadThread()
		{
			for (int i = 0; i < int.Parse(this.txtThreadCount.Value); i++)
			{
				/*
				ThreadLoader threadLoader = new ThreadLoader();
				threadLoader.formInstance = this;
				System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(threadLoader.StartThreadEntry));
				thread.Name = "Thread" + i.ToString();
				thread.Start();
				System.Threading.Thread.Sleep(500);
				*/
			}
		}
	}

	public class ThreadLoader
	{
		public FPerformanceTest formInstance = null;
		public void StartThreadEntry()
		{
			while (formInstance.btnStartCurrent.Enabled == false)
			{
				formInstance.timer_Tick(null, null);
				System.Threading.Thread thread = System.Threading.Thread.CurrentThread;
				thread.Join(int.Parse(formInstance.txtTimerInterval.Value) * 1000);
			}
		}
	}
}
