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
	public class FSPProcess : System.Windows.Forms.Form
	{
		private DataTable dtSP = new DataTable();
		private DataTable dtSPAlert = new DataTable();

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label7;
		private UserControl.UCLabelEdit txtAlertPercent;
		private System.Windows.Forms.Label label6;
		private UserControl.UCLabelEdit txtRefreshRate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label red;
		private System.Windows.Forms.Label lblColorDesc;
		private UserControl.UCButton btnSearch;
		private UserControl.UCLabelEdit txtLineCode;
		private UserControl.UCLabelEdit txtItemCode;
		private UserControl.UCLabelEdit txtMocode;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ToolTip tip;
		private UserControl.UCButton btnExit;
		private UserControl.UCButton btnUnveil;
		private UserControl.UCButton btnAgitate;
		private System.Windows.Forms.GroupBox groupBox2;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdUsedUp;
		private System.ComponentModel.IContainer components;

		public FSPProcess()
		{
			//
			// Windows 窗体设计器支持所必需的
			//

			InitialForm();

			InitializeComponent();

			

			UserControl.UIStyleBuilder.FormUI(this);	
			//UserControl.UIStyleBuilder.GridUI(ultraGridMain);
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSPProcess));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAlertPercent = new UserControl.UCLabelEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRefreshRate = new UserControl.UCLabelEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.red = new System.Windows.Forms.Label();
            this.lblColorDesc = new System.Windows.Forms.Label();
            this.btnSearch = new UserControl.UCButton();
            this.txtLineCode = new UserControl.UCLabelEdit();
            this.txtItemCode = new UserControl.UCLabelEdit();
            this.txtMocode = new UserControl.UCLabelEdit();
            this.btnUnveil = new UserControl.UCButton();
            this.btnAgitate = new UserControl.UCButton();
            this.btnExit = new UserControl.UCButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdUsedUp = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsedUp)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtAlertPercent);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtRefreshRate);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.red);
            this.panel1.Controls.Add(this.lblColorDesc);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtLineCode);
            this.panel1.Controls.Add(this.txtItemCode);
            this.panel1.Controls.Add(this.txtMocode);
            this.panel1.Controls.Add(this.btnUnveil);
            this.panel1.Controls.Add(this.btnAgitate);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(832, 144);
            this.panel1.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(720, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 23);
            this.label7.TabIndex = 36;
            this.label7.Text = "%";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAlertPercent
            // 
            this.txtAlertPercent.AllowEditOnlyChecked = true;
            this.txtAlertPercent.Caption = "预警标准";
            this.txtAlertPercent.Checked = false;
            this.txtAlertPercent.EditType = UserControl.EditTypes.Integer;
            this.txtAlertPercent.Location = new System.Drawing.Point(609, 53);
            this.txtAlertPercent.MaxLength = 40;
            this.txtAlertPercent.Multiline = false;
            this.txtAlertPercent.Name = "txtAlertPercent";
            this.txtAlertPercent.PasswordChar = '\0';
            this.txtAlertPercent.ReadOnly = false;
            this.txtAlertPercent.ShowCheckBox = false;
            this.txtAlertPercent.Size = new System.Drawing.Size(111, 24);
            this.txtAlertPercent.TabIndex = 35;
            this.txtAlertPercent.TabNext = true;
            this.txtAlertPercent.TabStop = false;
            this.txtAlertPercent.Value = "60";
            this.txtAlertPercent.WidthType = UserControl.WidthTypes.Tiny;
            this.txtAlertPercent.XAlign = 670;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(552, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 23);
            this.label6.TabIndex = 34;
            this.label6.Text = "分钟";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRefreshRate
            // 
            this.txtRefreshRate.AllowEditOnlyChecked = true;
            this.txtRefreshRate.Caption = "刷新频率";
            this.txtRefreshRate.Checked = false;
            this.txtRefreshRate.EditType = UserControl.EditTypes.Integer;
            this.txtRefreshRate.Location = new System.Drawing.Point(441, 53);
            this.txtRefreshRate.MaxLength = 40;
            this.txtRefreshRate.Multiline = false;
            this.txtRefreshRate.Name = "txtRefreshRate";
            this.txtRefreshRate.PasswordChar = '\0';
            this.txtRefreshRate.ReadOnly = false;
            this.txtRefreshRate.ShowCheckBox = false;
            this.txtRefreshRate.Size = new System.Drawing.Size(111, 24);
            this.txtRefreshRate.TabIndex = 22;
            this.txtRefreshRate.TabNext = true;
            this.txtRefreshRate.TabStop = false;
            this.txtRefreshRate.Value = "5";
            this.txtRefreshRate.WidthType = UserControl.WidthTypes.Tiny;
            this.txtRefreshRate.XAlign = 502;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(176, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 23);
            this.label5.TabIndex = 33;
            this.label5.Text = "达到开封或未开封时长的预警条件";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(176, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 23);
            this.label4.TabIndex = 32;
            this.label4.Text = "达到或超过回温时长";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(176, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 23);
            this.label3.TabIndex = 31;
            this.label3.Text = "超出未开封或者开封时长";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Purple;
            this.label2.Location = new System.Drawing.Point(104, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 23);
            this.label2.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(104, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 23);
            this.label1.TabIndex = 29;
            // 
            // red
            // 
            this.red.BackColor = System.Drawing.Color.Red;
            this.red.Location = new System.Drawing.Point(104, 61);
            this.red.Name = "red";
            this.red.Size = new System.Drawing.Size(64, 23);
            this.red.TabIndex = 28;
            // 
            // lblColorDesc
            // 
            this.lblColorDesc.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblColorDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblColorDesc.Location = new System.Drawing.Point(40, 61);
            this.lblColorDesc.Name = "lblColorDesc";
            this.lblColorDesc.Size = new System.Drawing.Size(64, 23);
            this.lblColorDesc.TabIndex = 27;
            this.lblColorDesc.Text = "颜色说明";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.ButtonType = UserControl.ButtonTypes.Query;
            this.btnSearch.Caption = "查询";
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Location = new System.Drawing.Point(376, 96);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(88, 22);
            this.btnSearch.TabIndex = 26;
            this.btnSearch.TabStop = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtLineCode
            // 
            this.txtLineCode.AllowEditOnlyChecked = true;
            this.txtLineCode.Caption = "产线代码";
            this.txtLineCode.Checked = false;
            this.txtLineCode.EditType = UserControl.EditTypes.String;
            this.txtLineCode.Location = new System.Drawing.Point(553, 13);
            this.txtLineCode.MaxLength = 40;
            this.txtLineCode.Multiline = false;
            this.txtLineCode.Name = "txtLineCode";
            this.txtLineCode.PasswordChar = '\0';
            this.txtLineCode.ReadOnly = false;
            this.txtLineCode.ShowCheckBox = false;
            this.txtLineCode.Size = new System.Drawing.Size(194, 24);
            this.txtLineCode.TabIndex = 25;
            this.txtLineCode.TabNext = true;
            this.txtLineCode.Value = "";
            this.txtLineCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtLineCode.XAlign = 614;
            // 
            // txtItemCode
            // 
            this.txtItemCode.AllowEditOnlyChecked = true;
            this.txtItemCode.Caption = "产品代码";
            this.txtItemCode.Checked = false;
            this.txtItemCode.EditType = UserControl.EditTypes.String;
            this.txtItemCode.Enabled = false;
            this.txtItemCode.Location = new System.Drawing.Point(297, 13);
            this.txtItemCode.MaxLength = 40;
            this.txtItemCode.Multiline = false;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.PasswordChar = '\0';
            this.txtItemCode.ReadOnly = true;
            this.txtItemCode.ShowCheckBox = false;
            this.txtItemCode.Size = new System.Drawing.Size(194, 24);
            this.txtItemCode.TabIndex = 24;
            this.txtItemCode.TabNext = true;
            this.txtItemCode.Value = "";
            this.txtItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtItemCode.XAlign = 358;
            // 
            // txtMocode
            // 
            this.txtMocode.AllowEditOnlyChecked = true;
            this.txtMocode.Caption = "工单代码";
            this.txtMocode.Checked = false;
            this.txtMocode.EditType = UserControl.EditTypes.String;
            this.txtMocode.Location = new System.Drawing.Point(41, 13);
            this.txtMocode.MaxLength = 40;
            this.txtMocode.Multiline = false;
            this.txtMocode.Name = "txtMocode";
            this.txtMocode.PasswordChar = '\0';
            this.txtMocode.ReadOnly = false;
            this.txtMocode.ShowCheckBox = false;
            this.txtMocode.Size = new System.Drawing.Size(194, 24);
            this.txtMocode.TabIndex = 23;
            this.txtMocode.TabNext = true;
            this.txtMocode.Value = "";
            this.txtMocode.WidthType = UserControl.WidthTypes.Normal;
            this.txtMocode.XAlign = 102;
            this.txtMocode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMocode_TxtboxKeyPress);
            // 
            // btnUnveil
            // 
            this.btnUnveil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUnveil.BackColor = System.Drawing.SystemColors.Control;
            this.btnUnveil.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUnveil.BackgroundImage")));
            this.btnUnveil.ButtonType = UserControl.ButtonTypes.None;
            this.btnUnveil.Caption = "开封";
            this.btnUnveil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnveil.Location = new System.Drawing.Point(568, 96);
            this.btnUnveil.Name = "btnUnveil";
            this.btnUnveil.Size = new System.Drawing.Size(88, 22);
            this.btnUnveil.TabIndex = 31;
            this.btnUnveil.TabStop = false;
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
            this.btnAgitate.Location = new System.Drawing.Point(472, 96);
            this.btnAgitate.Name = "btnAgitate";
            this.btnAgitate.Size = new System.Drawing.Size(88, 22);
            this.btnAgitate.TabIndex = 30;
            this.btnAgitate.TabStop = false;
            this.btnAgitate.Click += new System.EventHandler(this.btnAgitate_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(664, 96);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 32;
            this.btnExit.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdUsedUp);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 264);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(832, 161);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用完锡膏列表";
            // 
            // grdUsedUp
            // 
            this.grdUsedUp.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdUsedUp.DisplayLayout.RowConnectorColor = System.Drawing.Color.Gainsboro;
            this.grdUsedUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUsedUp.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grdUsedUp.Location = new System.Drawing.Point(3, 17);
            this.grdUsedUp.Name = "grdUsedUp";
            this.grdUsedUp.Size = new System.Drawing.Size(826, 141);
            this.grdUsedUp.TabIndex = 30;
            this.grdUsedUp.TabStop = false;
            this.grdUsedUp.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.grdUsedUp_InitializeLayout);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ultraGridMain);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 144);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(832, 120);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "作业锡膏列表";
            // 
            // ultraGridMain
            // 
            this.ultraGridMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridMain.DisplayLayout.RowConnectorColor = System.Drawing.Color.Gainsboro;
            this.ultraGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMain.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridMain.Location = new System.Drawing.Point(3, 17);
            this.ultraGridMain.Name = "ultraGridMain";
            this.ultraGridMain.Size = new System.Drawing.Size(826, 100);
            this.ultraGridMain.TabIndex = 29;
            this.ultraGridMain.TabStop = false;
            // 
            // FSPProcess
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(832, 425);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "FSPProcess";
            this.Text = "锡膏作业";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUsedUp)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void InitialForm()
		{
			
			dtSP.Clear();

//			dtSP.Columns.Add("SPID",typeof(string)).ReadOnly = true;
//			dtSP.Columns.Add("SPTYPE",typeof(string)).ReadOnly = true;
//			dtSP.Columns.Add("SPITEMCODE",typeof(string)).ReadOnly = true;
//			dtSP.Columns.Add("STATUS",typeof(string)).ReadOnly = true;
//			dtSP.Columns.Add("RETURNTIME",typeof(string)).ReadOnly = true;
//			dtSP.Columns.Add("RETURNTIMESPAN",typeof(string)).ReadOnly = true;
//			dtSP.Columns.Add("RETURNCOUNTTIME",typeof(string)).ReadOnly = true;
//			dtSP.Columns.Add("VEILTIMESPAN",typeof(string)).ReadOnly = true;
//			dtSP.Columns.Add("VEILCOUNTTIME",typeof(string)).ReadOnly = true;
//			dtSP.Columns.Add("UNVEILTIME",typeof(string)).ReadOnly = true;
//			dtSP.Columns.Add("UNVEILTIMESPAN",typeof(string)).ReadOnly = true;
//			dtSP.Columns.Add("UNVEILCONTTIME",typeof(string)).ReadOnly = true;
//			dtSP.Columns.Add("AGITATEDATE",typeof(string)).ReadOnly = true;

			dtSP.Columns.Add("锡膏ID",typeof(string)).ReadOnly = true;
			dtSP.Columns.Add("锡膏类型",typeof(string)).ReadOnly = true;
			dtSP.Columns.Add("锡膏料号",typeof(string)).ReadOnly = true;
			dtSP.Columns.Add("状态",typeof(string));
			dtSP.Columns.Add("回温时间",typeof(string)).ReadOnly = true;
			dtSP.Columns.Add("回温时长",typeof(string)).ReadOnly = true;
			dtSP.Columns.Add("回温计时",typeof(string)).ReadOnly = true;
			dtSP.Columns.Add("未开封时长",typeof(string)).ReadOnly = true;
			dtSP.Columns.Add("未开封计时",typeof(string)).ReadOnly = true;
			dtSP.Columns.Add("开封时间",typeof(string)).ReadOnly = true;
			dtSP.Columns.Add("开封时长",typeof(string)).ReadOnly = true;
			dtSP.Columns.Add("开封计时",typeof(string)).ReadOnly = true;
			dtSP.Columns.Add("AGITATEDATE",typeof(string)).ReadOnly = true;
			dtSP.Columns.Add("MOCODE",typeof(string)).ReadOnly = true;
			dtSP.Columns.Add("LINECODE",typeof(string)).ReadOnly = true;
			dtSP.Columns.Add("MEMO",typeof(string)).ReadOnly = true;

			dtSPAlert.Columns.Add("锡膏ID",typeof(string)).ReadOnly = true;
			dtSPAlert.Columns.Add("锡膏类型",typeof(string)).ReadOnly = true;
			dtSPAlert.Columns.Add("锡膏料号",typeof(string)).ReadOnly = true;
			dtSPAlert.Columns.Add("状态",typeof(string)).ReadOnly = true;
			dtSPAlert.Columns.Add("备注",typeof(string)).ReadOnly = true;
			
		}

		private bool ShowItem(string moCode)
		{
			return ShowItem(moCode,false);
		}

		private bool ShowItem(string moCode,bool isDestination)
		{
			//Show Item
			bool bResult = true;

			MOFacade moFAC = null;
			//support 3-Tier architecture
			try
			{
				moFAC = (MOFacade)Activator.CreateInstance(typeof(MOFacade)
					,new object[]{DataProvider});
			}
			catch(Exception ex)
			{
				moFAC = new MOFacade(DataProvider);
			}
			
			object objMO = moFAC.GetMO(moCode);

			if(objMO != null )
			{
				if(isDestination)
				{
					txtItemCode.Value = (objMO as Domain.MOModel.MO).ItemCode;
				}
			}
			else
			{
				ApplicationRun.GetInfoForm().Add(
					new UserControl.Message(MessageType.Error,"$CS_MO_Not_Exist"));

				bResult = false;

				Application.DoEvents();

				if(isDestination)
				{
					txtMocode.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
				}
			}
			

			return bResult;
		}


		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			object[] objs = LoadDataSource();
			FillMainDataGrid(objs);

			FillAlertDataGrid(objs);
		}

		private object[] LoadDataSource()
		{
			SolderPasteFacade spFAC = null;
			//support 3-Tier architecture
			try
			{
				spFAC = (SolderPasteFacade)Activator.CreateInstance(typeof(SolderPasteFacade)
					,new object[]{DataProvider});
			}
			catch(Exception ex)
			{
				spFAC = new SolderPasteFacade(DataProvider);
			}

			object[] objs = spFAC.QueryOnWorkSPP(txtMocode.Value.Trim(),txtLineCode.Value.Trim());

			return objs;
		}

		private void FillMainDataGrid(object[] objs)
		{
			dtSP.Rows.Clear();

		
			if(objs != null && objs.Length > 0)
			{
				SolderPasteFacade spFAC = null;
				//support 3-Tier architecture
				try
				{
					spFAC = (SolderPasteFacade)Activator.CreateInstance(typeof(SolderPasteFacade)
						,new object[]{DataProvider});
				}
				catch(Exception ex)
				{
					spFAC = new SolderPasteFacade(DataProvider);
				}

				foreach(Domain.SolderPaste.SOLDERPASTEPRO spp in objs)
				{
					if(spp.STATUS != Web.Helper.SolderPasteStatus.Restrain)
					{
						string openDate = FormatHelper.TODateTimeString(spp.OPENDATE,spp.OPENTIME);

						string agitateDate = FormatHelper.TODateTimeString(spp.AGITATEDATE,spp.AGITATETIME);

						string strRCountTime = spp.RETURNCOUNTTIME.ToString();

						string strVeilCountTime = spp.VEILCOUNTTIME.ToString();

						string[] Rtimes = strRCountTime.Split('.');
						string[] Vtimes = strVeilCountTime.Split('.');

						int iRCountHour = 0,iRCountMinutes = 0;
						int iVCountHour = 0,iVCountMinutes = 0;

						
						iRCountHour	= int.Parse(Rtimes[0]);
						iVCountHour	= int.Parse(Vtimes[0]);

						if(Rtimes.Length > 1)
						{
							iRCountMinutes	= Convert.ToInt32(System.Math.Round(Convert.ToDouble(int.Parse(Rtimes[1]))));
						}
						if(Vtimes.Length > 1)
						{
							iVCountMinutes	= Convert.ToInt32(System.Math.Round(Convert.ToDouble(int.Parse(Vtimes[1]))));
						}

						TimeSpan tsOpenDate  = new TimeSpan(iRCountHour,iRCountMinutes,0);
						TimeSpan tsVeilDate  = new TimeSpan(iVCountHour,iVCountMinutes,0);

						if(spp.STATUS == Web.Helper.SolderPasteStatus.Return)
						{
							tsOpenDate = DateTime.Now - DateTime.Parse(openDate);
						}
						if(spp.STATUS == Web.Helper.SolderPasteStatus.Agitate
							|| spp.STATUS == Web.Helper.SolderPasteStatus.Return)
						{
							tsVeilDate = DateTime.Now - DateTime.Parse(openDate);
						}

						string unveilDate = FormatHelper.TODateTimeString(spp.UNVEILMDATE,spp.UNVEILTIME);

						TimeSpan tsUnveilDate = new TimeSpan(0);

						if(unveilDate.Trim() != "00:00:00")
						{
							tsUnveilDate = DateTime.Now - DateTime.Parse(unveilDate);
						}

						object objSP = spFAC.GetSolderPaste(spp.SOLDERPASTEID);

						Domain.SolderPaste.SolderPaste sp = null ;

						if(objSP != null)
						{
							sp = objSP as Domain.SolderPaste.SolderPaste;
						}
					
//						dtSP.Columns.Add("锡膏ID",typeof(string)).ReadOnly = true;
//						dtSP.Columns.Add("锡膏类型",typeof(string)).ReadOnly = true;
//						dtSP.Columns.Add("锡膏料号",typeof(string)).ReadOnly = true;
//						dtSP.Columns.Add("状态",typeof(string));
//						dtSP.Columns.Add("回温时间",typeof(string)).ReadOnly = true;
//						dtSP.Columns.Add("回温时长",typeof(string)).ReadOnly = true;
//						dtSP.Columns.Add("回温计时",typeof(string)).ReadOnly = true;
//						dtSP.Columns.Add("未开封时长",typeof(string)).ReadOnly = true;
//						dtSP.Columns.Add("未开封计时",typeof(string)).ReadOnly = true;
//						dtSP.Columns.Add("开封时间",typeof(string)).ReadOnly = true;
//						dtSP.Columns.Add("开封时长",typeof(string)).ReadOnly = true;
//						dtSP.Columns.Add("开封计时",typeof(string)).ReadOnly = true;
//						dtSP.Columns.Add("AGITATEDATE",typeof(string)).ReadOnly = true;
//						dtSP.Columns.Add("MOCODE",typeof(string)).ReadOnly = true;
//						dtSP.Columns.Add("LINECODE",typeof(string)).ReadOnly = true;
//						dtSP.Columns.Add("MEMO",typeof(string)).ReadOnly = true;

						dtSP.Rows.Add(new object[]{
													  spp.SOLDERPASTEID
													  ,UserControl.MutiLanguages.ParserString(spp.SPTYPE)
													  ,sp.PartNO
													  ,UserControl.MutiLanguages.ParserString(spp.STATUS)
													  ,openDate
													  ,spp.RETURNTIMESPAN
													  ,tsOpenDate.Hours.ToString() + "时" + tsOpenDate.Minutes + "分"
													  ,spp.VEILTIMESPAN
													  ,tsVeilDate.Hours.ToString() + "时" + tsVeilDate.Minutes + "分"
													  ,unveilDate
													  ,spp.UNVEILTIMESPAN
													  ,tsUnveilDate.Hours.ToString() + "时" + tsUnveilDate.Minutes + "分"
													  ,agitateDate
													  ,spp.MOCODE
													  ,spp.LINECODE
													  ,spp.MEMO
												  }
							);

						dtSP.AcceptChanges();
					}
				}
			}

			ultraGridMain.DataSource = dtSP;

			InitialGridColumnStatus();

			RefreshProcessGrid();
		}

		private void FillAlertDataGrid(object[] objs)
		{
			dtSPAlert.Rows.Clear();

		
			if(objs != null && objs.Length > 0)
			{
				foreach(Domain.SolderPaste.SOLDERPASTEPRO spp in objs)
				{
					if(spp.STATUS == Web.Helper.SolderPasteStatus.Restrain)
					{
						dtSPAlert.Rows.Add(new object[]{
													  spp.SOLDERPASTEID
													  ,UserControl.MutiLanguages.ParserString(spp.SPTYPE)
													  ,spp.LOTNO
													  ,UserControl.MutiLanguages.ParserString(spp.STATUS)
													  ,spp.MEMO
												  }
							);

						dtSPAlert.AcceptChanges();
					}
				}
			}

			grdUsedUp.DataSource = dtSPAlert;

			InitialGridColumnStatus();

		}


		private void RefreshProcessGrid()
		{
			if(this.ultraGridMain.Rows.Count > 0)
			{
				SolderPasteFacade _facade = null;
				try
				{
					_facade = (SolderPasteFacade)Activator.CreateInstance(typeof(SolderPasteFacade),new object[]{DataProvider});

				}
				catch(Exception ex)
				{
					Log.Error(ex.Message);
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));

					_facade = new SolderPasteFacade(DataProvider);

				}

				ArrayList arRows = new ArrayList();

				for(int iRow = 0 ; iRow < ultraGridMain.Rows.Count ; iRow ++)
				{
					Infragistics.Win.UltraWinGrid.UltraGridRow ugr =  ultraGridMain.Rows[iRow];
					#region 获得计算所需的基础数据

					string agitateDate = ugr.Cells["AGITATEDATE"].Text.ToString().Trim();//搅拌日期

					TimeSpan tsOpenDate = DateTime.Now - DateTime.Parse(ugr.Cells["回温时间"].Text);//回温计时 = 当前时间 - 回温时间

					TimeSpan tsUnveilDate = DateTime.Now - DateTime.Parse(ugr.Cells["开封时间"].Text);//开封计时 = 当前时间 - 开封时间

					TimeSpan tsVeilDate  = DateTime.Now - DateTime.Parse(ugr.Cells["回温时间"].Text);//未开封计时 = 当前时间 - 搅拌时间

					string unveilDate =  ugr.Cells["开封时间"].Value.ToString().Trim();//开封时间

					int iVeilSpanTime = int.Parse(ugr.Cells["未开封时长"].Text);//未开封时长
					int iAlertVeilSpanTime = Convert.ToInt32(System.Math.Floor(Convert.ToDouble(iVeilSpanTime * int.Parse(txtAlertPercent.Value.Trim()) / 100)));
					decimal iAlertVeilCountTime = Convert.ToDecimal(System.Math.Round(tsVeilDate.TotalHours,2));

					int iUnveilSpanTime = int.Parse(ugr.Cells["开封时长"].Text);//开封时长
					int iAlertUnveilSpanTime = Convert.ToInt32(System.Math.Floor(Convert.ToDouble(iUnveilSpanTime *  int.Parse(txtAlertPercent.Value.Trim()) / 100)));

					string status = ugr.Cells["状态"].Text;

					Domain.SolderPaste.SOLDERPASTEPRO spp = new BenQGuru.eMES.Domain.SolderPaste.SOLDERPASTEPRO();

					#endregion

					#region 更改行颜色、更新锡膏信息
					ugr.Appearance.BackColor = Color.White;
					ugr.Appearance.ForeColor = Color.Black;
					//对于处于回温状态的锡膏如果达到或超过回温时长则变更记录颜色为“蓝色”
					if(status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Return)
						&&  System.Math.Floor(tsOpenDate.TotalHours) >=  int.Parse(ugr.Cells["回温时长"].Text))
					{
						ugr.Appearance.BackColor =  Color.Blue;
						ugr.Appearance.ForeColor = Color.White;
					}
					////5.2.5.2	处于回温状态但已经超出回温时长的锡膏，
					//如果未开封计时达到未开封时长的预警百分比，则变更记录颜色为“紫色”
					if(status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Return)
						&&  System.Math.Floor(tsOpenDate.TotalHours) >=  int.Parse(ugr.Cells["回温时长"].Text)
						&& iAlertVeilCountTime >=  iAlertVeilSpanTime)
					{
						ugr.Appearance.BackColor =  Color.Purple;
						ugr.Appearance.ForeColor = Color.White;
					}

					//5.2.5.2	对于搅拌状态的锡膏，
					//如果未开封计时达到未开封时长的预警百分比，则变更记录颜色为“紫色”
					if(status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Agitate)
						&& iAlertVeilCountTime >=  iAlertVeilSpanTime)
					{
						ugr.Appearance.BackColor =  Color.Purple;
						ugr.Appearance.ForeColor = Color.White;
					}

					//对于搅拌状态的锡膏，超出未开封时长，则变更记录颜色为“红色”，
					//并将锡膏状态转换为“限制使用”状态，记录备注信息“超出未开封时长”，记录未开封计时
					if(status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Agitate)
						&&  iAlertVeilCountTime >=  iVeilSpanTime)
					{
						ugr.Appearance.BackColor =  Color.Red;
						ugr.Appearance.ForeColor = Color.White;

						ugr.Cells["状态"].Value = UserControl.MutiLanguages.ParserString(Web.Helper.SolderPasteStatus.Restrain);

						spp.STATUS = Web.Helper.SolderPasteStatus.Restrain;

						spp.VEILCOUNTTIME = iAlertVeilCountTime;

						spp.MEMO = "超出未开封时长";
					}

					//处于回温状态但已经超出回温时长的锡膏，超出未开封时长，则变更记录颜色为“红色”，
					//并将锡膏状态转换为“限制使用”状态，记录备注信息“超出未开封时长”，记录未开封计时
					if(status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Return)
						&&  System.Math.Floor(tsOpenDate.TotalHours) >=  int.Parse(ugr.Cells["回温时长"].Text)
						&&  iAlertVeilCountTime >=  iVeilSpanTime)
					{
						ugr.Appearance.BackColor =  Color.Red;
						ugr.Appearance.ForeColor = Color.White;

						ugr.Cells["状态"].Value = UserControl.MutiLanguages.ParserString(Web.Helper.SolderPasteStatus.Restrain);

						spp.STATUS = Web.Helper.SolderPasteStatus.Restrain;

						spp.VEILCOUNTTIME = iAlertVeilCountTime;

						spp.MEMO = "超出未开封时长";
					}

					//如果开封计时达到开封时长的预警百分比，变更记录颜色为“紫色”
					if(status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Unveil)
						&& System.Math.Floor(tsUnveilDate.TotalHours) >=  iAlertUnveilSpanTime)
					{
						ugr.Appearance.BackColor =  Color.Purple;
						ugr.Appearance.ForeColor = Color.White;
					}

					
					//如果锡膏超出了开封时长，则变更记录颜色为“红色”，
					//并变更锡膏状态未“限制使用”状态，记录备注信息“超出开封时长”，记录开封计时
					if(status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Unveil)
						&& System.Math.Floor(tsUnveilDate.TotalHours) >=  iUnveilSpanTime)
					{
						ugr.Appearance.BackColor =  Color.Red;
						ugr.Appearance.ForeColor = Color.White;

						ugr.Cells["状态"].Value = UserControl.MutiLanguages.ParserString(Web.Helper.SolderPasteStatus.Restrain);

						spp.STATUS = Web.Helper.SolderPasteStatus.Restrain;
						spp.UNVEILCOUNTTIME =  Convert.ToDecimal(System.Math.Round(tsUnveilDate.TotalHours,2));
						spp.MEMO = "超出开封时长";
					}

					//更新锡膏使用记录
					if(spp.STATUS != null && spp.STATUS != String.Empty )
					{
						object objSP =  _facade.GetSolderPaste(ugr.Cells["锡膏ID"].Text);

						object objSPP = _facade.GetSPP(ugr.Cells["锡膏ID"].Text
							,ugr.Cells["MOCODE"].Text
							,ugr.Cells["LINECODE"].Text);

						if(objSPP != null)
						{
							Domain.SolderPaste.SolderPaste sp = objSP as Domain.SolderPaste.SolderPaste;

							Domain.SolderPaste.SOLDERPASTEPRO sppNew = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;

							sp.MaintainUser = ApplicationService.Current().UserCode;
							sp.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
							sp.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);

							sppNew.MUSER = sp.MaintainUser;
							sppNew.MDATE = sp.MaintainDate;
							sppNew.MTIME = sp.MaintainTime;

							DataProvider.BeginTransaction();

							try
							{
								_facade.DeleteSOLDERPASTEPRO(sppNew);

								sppNew.STATUS = spp.STATUS;
								sppNew.MEMO = spp.MEMO;

								if(sppNew.MEMO == "超出开封时长")
								{
									
									sppNew.UNVEILCOUNTTIME =  Convert.ToDecimal(System.Math.Round(tsUnveilDate.TotalHours,2));
								}
								if(sppNew.MEMO == "超出未开封时长")
								{
									if(status == UserControl.MutiLanguages.ParserString(SolderPasteStatus.Return))
									{
										sppNew.RETURNCOUNTTIME =  Convert.ToDecimal(System.Math.Round(tsOpenDate.TotalHours,2));
									}
									sppNew.VEILCOUNTTIME = iAlertVeilCountTime;
								}

								_facade.AddSOLDERPASTEPRO(sppNew);

								sp.Status = spp.STATUS;

								_facade.UpdateSolderPaste(sp);

								DataProvider.CommitTransaction();

								arRows.Add(ugr);
							}
							catch(Exception ex)
							{
								Log.Error(ex.Message);
								DataProvider.RollbackTransaction();

								ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,ex.Message));
							}
							finally
							{
								((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
							}
						}
					}
					#endregion
					
				}
				foreach(Infragistics.Win.UltraWinGrid.UltraGridRow ugr in arRows)
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
//			ultraWinGridHelper.AddCommonColumn("MOCODE","工单代码");
//			ultraWinGridHelper.AddCommonColumn("LINECODE","产线代码");
//			ultraWinGridHelper.AddCommonColumn("MEMO","备注");
//
//			//ultraGridMain.DataSource = dtSP;
//
//			InitialGridColumnStatus();
		}

		private void grdUsedUp_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
		{
//			UltraWinGridHelper ultraWinGridHelper = new UltraWinGridHelper(this.grdUsedUp);
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
//			ultraWinGridHelper.AddCommonColumn("MOCODE","工单代码");
//			ultraWinGridHelper.AddCommonColumn("LINECODE","产线代码");
//			ultraWinGridHelper.AddCommonColumn("MEMO","备注");
//
//			InitialGridColumnStatus();
		}

		private void InitialGridColumnStatus()
		{
			if(this.ultraGridMain.DisplayLayout.Bands.Count > 0)
			{
				if(this.ultraGridMain.DisplayLayout.Bands[0].Columns.Count > 0)
				{
					this.ultraGridMain.DisplayLayout.Bands[0].Columns["AGITATEDATE"].Hidden = true;
					this.ultraGridMain.DisplayLayout.Bands[0].Columns["MOCODE"].Hidden = true;
					this.ultraGridMain.DisplayLayout.Bands[0].Columns["LINECODE"].Hidden = true;
					this.ultraGridMain.DisplayLayout.Bands[0].Columns["MEMO"].Hidden = true;
				}
			}

//			foreach(Infragistics.Win.UltraWinGrid.UltraGridRow ugr in grdUsedUp.Rows)
			for ( int iGridRowLoopIndex = 0; iGridRowLoopIndex < grdUsedUp.Rows.Count; iGridRowLoopIndex++)
			{
				Infragistics.Win.UltraWinGrid.UltraGridRow ugr = grdUsedUp.Rows[iGridRowLoopIndex];
				ugr.Appearance.ForeColor = Color.Red;
			}
		}

		private void txtMocode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				string moCode =  txtMocode.Value.ToUpper().Trim();
				if(moCode != String.Empty)
				{
					ShowItem(moCode,true);//show the item code by mocode 
				}
			}
		}

		private void btnUnveil_Click(object sender, System.EventArgs e)
		{
			SolderPasteFacade _facade = null;
			try
			{
				_facade = (SolderPasteFacade)Activator.CreateInstance(typeof(SolderPasteFacade),new object[]{DataProvider});

			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));

				_facade = new SolderPasteFacade(DataProvider);

			}

			#region Unveil Process

			Infragistics.Win.UltraWinGrid.UltraGridRow ugr = ultraGridMain.ActiveRow ;

			object objSPP = _facade.GetSPP(ugr.Cells["锡膏ID"].Text
				,ugr.Cells["MOCODE"].Text
				,ugr.Cells["LINECODE"].Text);

			if(objSPP != null)
			{
				Domain.SolderPaste.SOLDERPASTEPRO spp = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;

				if(spp.STATUS == Web.Helper.SolderPasteStatus.Agitate)
				{
					spp.UNVEILUSER = ApplicationService.Current().LoginInfo.UserCode;
					spp.UNVEILMDATE= FormatHelper.TODateInt(DateTime.Now);
					spp.UNVEILTIME= FormatHelper.TOTimeInt(DateTime.Now);

					TimeSpan tsVeilDate  = DateTime.Now - DateTime.Parse(ugr.Cells["回温时间"].Text);//未开封计时 = 当前时间 - 搅拌时间

					spp.VEILCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsVeilDate.TotalHours,2));

					spp.MUSER = ApplicationService.Current().LoginInfo.UserCode;
					spp.MDATE = FormatHelper.TODateInt(DateTime.Now);
					spp.MTIME = FormatHelper.TOTimeInt(DateTime.Now);

					DataProvider.BeginTransaction();

					try
					{
						_facade.DeleteSOLDERPASTEPRO(spp);

						spp.STATUS = Web.Helper.SolderPasteStatus.Unveil;

						_facade.AddSOLDERPASTEPRO(spp);

						DataProvider.CommitTransaction();

						btnSearch_Click(sender,e);
					}
					catch(Exception ex)
					{
						Log.Error(ex.Message);
						DataProvider.RollbackTransaction();
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,ex.Message));
					}
					finally
					{
						((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
					}


				}
			}

			#endregion
		}

		private void btnAgitate_Click(object sender, System.EventArgs e)
		{
			SolderPasteFacade _facade = null;
			try
			{
				_facade = (SolderPasteFacade)Activator.CreateInstance(typeof(SolderPasteFacade),new object[]{DataProvider});

			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));

				_facade = new SolderPasteFacade(DataProvider);

			}

			#region Agitate Process

			Infragistics.Win.UltraWinGrid.UltraGridRow ugr = ultraGridMain.ActiveRow ;

			if(ugr.Appearance.BackColor != Color.Blue && ugr.Appearance.BackColor != Color.Purple)
			{
				ApplicationRun.GetInfoForm().Add(
					new UserControl.Message(MessageType.Error,"$CS_STATUS_WRONG $Domain_SolderPaste_ID = " + ugr.Cells["锡膏ID"].Text
					+ " $Current_Status = " + ugr.Cells["状态"].Text));

				return ;
			}

			object objSPP = _facade.GetSPP(ugr.Cells["锡膏ID"].Text
				,ugr.Cells["MOCODE"].Text
				,ugr.Cells["LINECODE"].Text);

			if(objSPP != null)
			{
				Domain.SolderPaste.SOLDERPASTEPRO spp = objSPP as Domain.SolderPaste.SOLDERPASTEPRO;

				if(spp.STATUS == Web.Helper.SolderPasteStatus.Return)
				{
					

					spp.AGITAEUSER = ApplicationService.Current().LoginInfo.UserCode;
					spp.AGITATEDATE = FormatHelper.TODateInt(DateTime.Now);
					spp.AGITATETIME = FormatHelper.TOTimeInt(DateTime.Now);

					TimeSpan tsOpenDate = DateTime.Now - DateTime.Parse(ugr.Cells["回温时间"].Text);//回温计时 = 当前时间 - 回温时间

					spp.RETURNCOUNTTIME = Convert.ToDecimal(System.Math.Round(tsOpenDate.TotalHours,2));

					spp.MUSER = ApplicationService.Current().LoginInfo.UserCode;
					spp.MDATE = FormatHelper.TODateInt(DateTime.Now);
					spp.MTIME = FormatHelper.TOTimeInt(DateTime.Now);

					DataProvider.BeginTransaction();

					try
					{
						_facade.DeleteSOLDERPASTEPRO(spp);

						spp.STATUS = Web.Helper.SolderPasteStatus.Agitate;
						
						_facade.AddSOLDERPASTEPRO(spp);


						DataProvider.CommitTransaction();

						btnSearch_Click(sender,e);
					}
					catch(Exception ex)
					{
						Log.Error(ex.Message);
						DataProvider.RollbackTransaction();
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,ex.Message));
					}
					finally
					{
						((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
					}
				}
			}

			#endregion
		}

		private void ultraGridMain_MouseHover(object sender, System.EventArgs e)
		{
			//ultraGridMain.
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			timer1.Interval = int.Parse(txtRefreshRate.Value.Trim()) * 60 * 1000;

			btnSearch_Click(sender,e);
		}

		private void ultraGridMain_MouseEnterElement(object sender, Infragistics.Win.UIElementEventArgs e)
		{
			object oContext = null;
			if(e.Element.GetType() == typeof(Infragistics.Win.UltraWinGrid.CellUIElement))
			{
				oContext = e.Element.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridCell));

				if(oContext != null)
				{
					UltraGridCell oCell = (UltraGridCell)oContext;
					string memo = oCell.Row.Cells["MEMO"].Text;

					if(memo != String.Empty)
					{
						tip.SetToolTip(ultraGridMain,oCell.Row.Cells["MEMO"].Text);
					}
					//e.Element.ToolTipItem 
					//e.Element.ToolTipItem = new Infragistics.Win.ToolTip(this)
						//oCell.Row.Cells["MEMO"].Text;
				}
			}
		}

		private void ultraGridMain_MouseLeaveElement(object sender, Infragistics.Win.UIElementEventArgs e)
		{
		
		}

		


	}
}
