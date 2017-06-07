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

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FSMTTransferMO 的摘要说明。
	/// </summary>
	public class FSMTTransferMO : BaseForm
	{
		private System.Windows.Forms.Panel panelMOFrom;
		private UserControl.UCLabelEdit txtMOCodeFrom;
		private UserControl.UCLabelEdit txtProductCodeFrom;
		private System.Windows.Forms.Panel panelMaterialFrom;
		private UserControl.UCLabelEdit txtProductCodeTo;
		private UserControl.UCLabelEdit txtMOCodeTo;
		private UserControl.UCButton btnTransfer;
		private System.Windows.Forms.Panel panelMOCodeTo;
		private System.Windows.Forms.Panel panelBottom;
		private UserControl.UCButton btnOK;
		private UserControl.UCButton ucBtnExit;
		private UserControl.UCLabelEdit txtDiffCount;
		private System.Windows.Forms.Panel panelMaterialTo;
		private Infragistics.Win.UltraWinGrid.UltraGrid gridFrom;
		private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
		private Infragistics.Win.UltraWinGrid.UltraGrid gridTo;
		private UserControl.UCLabelEdit txtSSCodeFrom;
        private UserControl.UCLabelEdit txtSSCodeTo;
        private IContainer components;

		public FSMTTransferMO()
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MachineCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MachineStationCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeederCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeederSpecCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeederLeftCount");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReelNo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaterialCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReelLeftQty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Memo");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn1 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("MachineCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("MachineStationCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn3 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("FeederCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn4 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("FeederSpecCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn5 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("FeederLeftCount");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn6 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("ReelNo");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn7 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("MaterialCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn8 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("ReelLeftQty");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn9 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Memo");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSMTTransferMO));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MachineCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MachineStationCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeederCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeederSpecCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeederLeftCount");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReelNo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaterialCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReelLeftQty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Memo");
            this.panelMOFrom = new System.Windows.Forms.Panel();
            this.txtSSCodeFrom = new UserControl.UCLabelEdit();
            this.txtProductCodeFrom = new UserControl.UCLabelEdit();
            this.txtMOCodeFrom = new UserControl.UCLabelEdit();
            this.panelMaterialFrom = new System.Windows.Forms.Panel();
            this.gridFrom = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.panelMOCodeTo = new System.Windows.Forms.Panel();
            this.txtSSCodeTo = new UserControl.UCLabelEdit();
            this.btnTransfer = new UserControl.UCButton();
            this.txtProductCodeTo = new UserControl.UCLabelEdit();
            this.txtMOCodeTo = new UserControl.UCLabelEdit();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.txtDiffCount = new UserControl.UCLabelEdit();
            this.ucBtnExit = new UserControl.UCButton();
            this.btnOK = new UserControl.UCButton();
            this.panelMaterialTo = new System.Windows.Forms.Panel();
            this.gridTo = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panelMOFrom.SuspendLayout();
            this.panelMaterialFrom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            this.panelMOCodeTo.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelMaterialTo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMOFrom
            // 
            this.panelMOFrom.Controls.Add(this.txtSSCodeFrom);
            this.panelMOFrom.Controls.Add(this.txtProductCodeFrom);
            this.panelMOFrom.Controls.Add(this.txtMOCodeFrom);
            this.panelMOFrom.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMOFrom.Location = new System.Drawing.Point(0, 0);
            this.panelMOFrom.Name = "panelMOFrom";
            this.panelMOFrom.Size = new System.Drawing.Size(768, 45);
            this.panelMOFrom.TabIndex = 0;
            // 
            // txtSSCodeFrom
            // 
            this.txtSSCodeFrom.AllowEditOnlyChecked = true;
            this.txtSSCodeFrom.Caption = "产线代码";
            this.txtSSCodeFrom.Checked = false;
            this.txtSSCodeFrom.EditType = UserControl.EditTypes.String;
            this.txtSSCodeFrom.Location = new System.Drawing.Point(451, 15);
            this.txtSSCodeFrom.MaxLength = 40;
            this.txtSSCodeFrom.Multiline = false;
            this.txtSSCodeFrom.Name = "txtSSCodeFrom";
            this.txtSSCodeFrom.PasswordChar = '\0';
            this.txtSSCodeFrom.ReadOnly = false;
            this.txtSSCodeFrom.ShowCheckBox = false;
            this.txtSSCodeFrom.Size = new System.Drawing.Size(162, 24);
            this.txtSSCodeFrom.TabIndex = 3;
            this.txtSSCodeFrom.TabNext = true;
            this.txtSSCodeFrom.Value = "";
            this.txtSSCodeFrom.WidthType = UserControl.WidthTypes.Normal;
            this.txtSSCodeFrom.XAlign = 502;
            this.txtSSCodeFrom.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSSCodeFrom_TxtboxKeyPress);
            // 
            // txtProductCodeFrom
            // 
            this.txtProductCodeFrom.AllowEditOnlyChecked = true;
            this.txtProductCodeFrom.Caption = "产品代码";
            this.txtProductCodeFrom.Checked = false;
            this.txtProductCodeFrom.EditType = UserControl.EditTypes.String;
            this.txtProductCodeFrom.Location = new System.Drawing.Point(236, 15);
            this.txtProductCodeFrom.MaxLength = 40;
            this.txtProductCodeFrom.Multiline = false;
            this.txtProductCodeFrom.Name = "txtProductCodeFrom";
            this.txtProductCodeFrom.PasswordChar = '\0';
            this.txtProductCodeFrom.ReadOnly = true;
            this.txtProductCodeFrom.ShowCheckBox = false;
            this.txtProductCodeFrom.Size = new System.Drawing.Size(162, 24);
            this.txtProductCodeFrom.TabIndex = 2;
            this.txtProductCodeFrom.TabNext = true;
            this.txtProductCodeFrom.Value = "";
            this.txtProductCodeFrom.WidthType = UserControl.WidthTypes.Normal;
            this.txtProductCodeFrom.XAlign = 287;
            // 
            // txtMOCodeFrom
            // 
            this.txtMOCodeFrom.AllowEditOnlyChecked = true;
            this.txtMOCodeFrom.Caption = "工单代码";
            this.txtMOCodeFrom.Checked = false;
            this.txtMOCodeFrom.EditType = UserControl.EditTypes.String;
            this.txtMOCodeFrom.Location = new System.Drawing.Point(21, 15);
            this.txtMOCodeFrom.MaxLength = 40;
            this.txtMOCodeFrom.Multiline = false;
            this.txtMOCodeFrom.Name = "txtMOCodeFrom";
            this.txtMOCodeFrom.PasswordChar = '\0';
            this.txtMOCodeFrom.ReadOnly = false;
            this.txtMOCodeFrom.ShowCheckBox = false;
            this.txtMOCodeFrom.Size = new System.Drawing.Size(161, 24);
            this.txtMOCodeFrom.TabIndex = 1;
            this.txtMOCodeFrom.TabNext = true;
            this.txtMOCodeFrom.Value = "";
            this.txtMOCodeFrom.WidthType = UserControl.WidthTypes.Normal;
            this.txtMOCodeFrom.XAlign = 72;
            this.txtMOCodeFrom.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMOCodeFrom_TxtboxKeyPress);
            // 
            // panelMaterialFrom
            // 
            this.panelMaterialFrom.Controls.Add(this.gridFrom);
            this.panelMaterialFrom.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMaterialFrom.Location = new System.Drawing.Point(0, 45);
            this.panelMaterialFrom.Name = "panelMaterialFrom";
            this.panelMaterialFrom.Size = new System.Drawing.Size(768, 156);
            this.panelMaterialFrom.TabIndex = 1;
            // 
            // gridFrom
            // 
            this.gridFrom.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridFrom.DataSource = this.ultraDataSource1;
            ultraGridColumn1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn1.Header.Caption = "机台代码";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 74;
            ultraGridColumn2.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn2.Header.Caption = "站位";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 74;
            ultraGridColumn3.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn3.Header.Caption = "Feeder代码";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn4.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn4.Header.Caption = "Feeder规格";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn5.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn5.Header.Caption = "Feeder剩余次数";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 95;
            ultraGridColumn6.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn6.Header.Caption = "料卷编号";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn7.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn7.Header.Caption = "物料代码";
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn8.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn8.Header.Caption = "料卷剩余数量";
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Width = 83;
            ultraGridColumn9.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn9.Header.Caption = "备注";
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Width = 83;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9});
            this.gridFrom.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.gridFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFrom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridFrom.Location = new System.Drawing.Point(0, 0);
            this.gridFrom.Name = "gridFrom";
            this.gridFrom.Size = new System.Drawing.Size(768, 156);
            this.gridFrom.TabIndex = 0;
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
            ultraDataColumn9});
            // 
            // panelMOCodeTo
            // 
            this.panelMOCodeTo.Controls.Add(this.txtSSCodeTo);
            this.panelMOCodeTo.Controls.Add(this.btnTransfer);
            this.panelMOCodeTo.Controls.Add(this.txtProductCodeTo);
            this.panelMOCodeTo.Controls.Add(this.txtMOCodeTo);
            this.panelMOCodeTo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMOCodeTo.Location = new System.Drawing.Point(0, 201);
            this.panelMOCodeTo.Name = "panelMOCodeTo";
            this.panelMOCodeTo.Size = new System.Drawing.Size(768, 44);
            this.panelMOCodeTo.TabIndex = 2;
            // 
            // txtSSCodeTo
            // 
            this.txtSSCodeTo.AllowEditOnlyChecked = true;
            this.txtSSCodeTo.Caption = "产线代码";
            this.txtSSCodeTo.Checked = false;
            this.txtSSCodeTo.EditType = UserControl.EditTypes.String;
            this.txtSSCodeTo.Location = new System.Drawing.Point(451, 15);
            this.txtSSCodeTo.MaxLength = 40;
            this.txtSSCodeTo.Multiline = false;
            this.txtSSCodeTo.Name = "txtSSCodeTo";
            this.txtSSCodeTo.PasswordChar = '\0';
            this.txtSSCodeTo.ReadOnly = true;
            this.txtSSCodeTo.ShowCheckBox = false;
            this.txtSSCodeTo.Size = new System.Drawing.Size(162, 24);
            this.txtSSCodeTo.TabIndex = 5;
            this.txtSSCodeTo.TabNext = true;
            this.txtSSCodeTo.Value = "";
            this.txtSSCodeTo.WidthType = UserControl.WidthTypes.Normal;
            this.txtSSCodeTo.XAlign = 502;
            // 
            // btnTransfer
            // 
            this.btnTransfer.BackColor = System.Drawing.SystemColors.Control;
            this.btnTransfer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTransfer.BackgroundImage")));
            this.btnTransfer.ButtonType = UserControl.ButtonTypes.None;
            this.btnTransfer.Caption = "转移";
            this.btnTransfer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTransfer.Location = new System.Drawing.Point(654, 15);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(88, 22);
            this.btnTransfer.TabIndex = 4;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // txtProductCodeTo
            // 
            this.txtProductCodeTo.AllowEditOnlyChecked = true;
            this.txtProductCodeTo.Caption = "产品代码";
            this.txtProductCodeTo.Checked = false;
            this.txtProductCodeTo.EditType = UserControl.EditTypes.String;
            this.txtProductCodeTo.Location = new System.Drawing.Point(236, 15);
            this.txtProductCodeTo.MaxLength = 40;
            this.txtProductCodeTo.Multiline = false;
            this.txtProductCodeTo.Name = "txtProductCodeTo";
            this.txtProductCodeTo.PasswordChar = '\0';
            this.txtProductCodeTo.ReadOnly = true;
            this.txtProductCodeTo.ShowCheckBox = false;
            this.txtProductCodeTo.Size = new System.Drawing.Size(162, 24);
            this.txtProductCodeTo.TabIndex = 2;
            this.txtProductCodeTo.TabNext = true;
            this.txtProductCodeTo.Value = "";
            this.txtProductCodeTo.WidthType = UserControl.WidthTypes.Normal;
            this.txtProductCodeTo.XAlign = 287;
            // 
            // txtMOCodeTo
            // 
            this.txtMOCodeTo.AllowEditOnlyChecked = true;
            this.txtMOCodeTo.Caption = "目标工单";
            this.txtMOCodeTo.Checked = false;
            this.txtMOCodeTo.EditType = UserControl.EditTypes.String;
            this.txtMOCodeTo.Location = new System.Drawing.Point(21, 15);
            this.txtMOCodeTo.MaxLength = 40;
            this.txtMOCodeTo.Multiline = false;
            this.txtMOCodeTo.Name = "txtMOCodeTo";
            this.txtMOCodeTo.PasswordChar = '\0';
            this.txtMOCodeTo.ReadOnly = false;
            this.txtMOCodeTo.ShowCheckBox = false;
            this.txtMOCodeTo.Size = new System.Drawing.Size(161, 24);
            this.txtMOCodeTo.TabIndex = 1;
            this.txtMOCodeTo.TabNext = true;
            this.txtMOCodeTo.Value = "";
            this.txtMOCodeTo.WidthType = UserControl.WidthTypes.Normal;
            this.txtMOCodeTo.XAlign = 72;
            this.txtMOCodeTo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMOCodeTo_TxtboxKeyPress);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.txtDiffCount);
            this.panelBottom.Controls.Add(this.ucBtnExit);
            this.panelBottom.Controls.Add(this.btnOK);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 497);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(768, 44);
            this.panelBottom.TabIndex = 3;
            // 
            // txtDiffCount
            // 
            this.txtDiffCount.AllowEditOnlyChecked = true;
            this.txtDiffCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiffCount.Caption = "差异数量";
            this.txtDiffCount.Checked = false;
            this.txtDiffCount.EditType = UserControl.EditTypes.String;
            this.txtDiffCount.Location = new System.Drawing.Point(654, 15);
            this.txtDiffCount.MaxLength = 40;
            this.txtDiffCount.Multiline = false;
            this.txtDiffCount.Name = "txtDiffCount";
            this.txtDiffCount.PasswordChar = '\0';
            this.txtDiffCount.ReadOnly = true;
            this.txtDiffCount.ShowCheckBox = false;
            this.txtDiffCount.Size = new System.Drawing.Size(93, 24);
            this.txtDiffCount.TabIndex = 7;
            this.txtDiffCount.TabNext = true;
            this.txtDiffCount.Value = "";
            this.txtDiffCount.WidthType = UserControl.WidthTypes.Tiny;
            this.txtDiffCount.XAlign = 705;
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(300, 15);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 6;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.ButtonType = UserControl.ButtonTypes.None;
            this.btnOK.Caption = "确定";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Location = new System.Drawing.Point(180, 15);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 22);
            this.btnOK.TabIndex = 5;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelMaterialTo
            // 
            this.panelMaterialTo.Controls.Add(this.gridTo);
            this.panelMaterialTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaterialTo.Location = new System.Drawing.Point(0, 245);
            this.panelMaterialTo.Name = "panelMaterialTo";
            this.panelMaterialTo.Size = new System.Drawing.Size(768, 252);
            this.panelMaterialTo.TabIndex = 4;
            // 
            // gridTo
            // 
            this.gridTo.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridTo.DataSource = this.ultraDataSource1;
            ultraGridColumn10.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn10.Header.Caption = "机台代码";
            ultraGridColumn10.Header.VisiblePosition = 0;
            ultraGridColumn10.Width = 74;
            ultraGridColumn11.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn11.Header.Caption = "站位";
            ultraGridColumn11.Header.VisiblePosition = 1;
            ultraGridColumn11.Width = 74;
            ultraGridColumn12.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn12.Header.Caption = "Feeder代码";
            ultraGridColumn12.Header.VisiblePosition = 2;
            ultraGridColumn13.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn13.Header.Caption = "Feeder规格";
            ultraGridColumn13.Header.VisiblePosition = 3;
            ultraGridColumn14.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn14.Header.Caption = "Feeder剩余次数";
            ultraGridColumn14.Header.VisiblePosition = 4;
            ultraGridColumn14.Width = 95;
            ultraGridColumn15.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn15.Header.Caption = "料卷编号";
            ultraGridColumn15.Header.VisiblePosition = 5;
            ultraGridColumn16.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn16.Header.Caption = "物料代码";
            ultraGridColumn16.Header.VisiblePosition = 6;
            ultraGridColumn17.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn17.Header.Caption = "料卷剩余数量";
            ultraGridColumn17.Header.VisiblePosition = 7;
            ultraGridColumn17.Width = 83;
            ultraGridColumn18.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn18.Header.Caption = "备注";
            ultraGridColumn18.Header.VisiblePosition = 8;
            ultraGridColumn18.Width = 83;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18});
            this.gridTo.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.gridTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridTo.Location = new System.Drawing.Point(0, 0);
            this.gridTo.Name = "gridTo";
            this.gridTo.Size = new System.Drawing.Size(768, 252);
            this.gridTo.TabIndex = 1;
            // 
            // FSMTTransferMO
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(768, 541);
            this.Controls.Add(this.panelMaterialTo);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelMOCodeTo);
            this.Controls.Add(this.panelMaterialFrom);
            this.Controls.Add(this.panelMOFrom);
            this.Name = "FSMTTransferMO";
            this.Text = "上料转移";
            this.Load += new System.EventHandler(this.FSMTTransferMO_Load);
            this.Closed += new System.EventHandler(this.FSMTTransferMO_Closed);
            this.panelMOFrom.ResumeLayout(false);
            this.panelMaterialFrom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            this.panelMOCodeTo.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelMaterialTo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTo)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		
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
			if (this.DataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection(); 
		}

		private void FSMTTransferMO_Closed(object sender, System.EventArgs e)
		{
			this.CloseConnection();
		}

		private DataTable tableFrom = null;
		private DataTable tableTo = null;
		private void FSMTTransferMO_Load(object sender, System.EventArgs e)
		{
			smtFacade = new SMTFacade(this.DataProvider);
			tableFrom = new DataTable();
			tableFrom.Columns.AddRange(new DataColumn[]{
														   new DataColumn("MachineCode"),
														   new DataColumn("MachineStationCode"),
														   new DataColumn("FeederCode"),
														   new DataColumn("FeederSpecCode"),
														   new DataColumn("FeederLeftCount", typeof(decimal)),
														   new DataColumn("ReelNo"),
														   new DataColumn("MaterialCode"),
														   new DataColumn("ReelLeftQty", typeof(decimal)),
														   new DataColumn("Memo")
													   });
			tableTo = new DataTable();
			tableTo = tableFrom.Clone();
			this.gridFrom.DataSource = tableFrom;
			this.gridTo.DataSource = tableTo;
			this.gridTo.DisplayLayout.Bands[0].Columns["Memo"].Hidden = true;

			UserControl.UIStyleBuilder.FormUI(this);
			UserControl.UIStyleBuilder.GridUI(this.gridFrom);
			UserControl.UIStyleBuilder.GridUI(this.gridTo);
			gridFrom.DisplayLayout.Bands[0].Columns["FeederLeftCount"].Format = "#,#";
			gridFrom.DisplayLayout.Bands[0].Columns["FeederLeftCount"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
			gridFrom.DisplayLayout.Bands[0].Columns["ReelLeftQty"].Format = "#,#";
			gridFrom.DisplayLayout.Bands[0].Columns["ReelLeftQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
			gridTo.DisplayLayout.Bands[0].Columns["FeederLeftCount"].Format = "#,#";
			gridTo.DisplayLayout.Bands[0].Columns["FeederLeftCount"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
			gridTo.DisplayLayout.Bands[0].Columns["ReelLeftQty"].Format = "#,#";
			gridTo.DisplayLayout.Bands[0].Columns["ReelLeftQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

            //this.InitPageLanguage();
            //this.InitGridLanguage(gridFrom);
            //this.InitGridLanguage(gridTo);
		}

		private void txtMOCodeFrom_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r' && txtMOCodeFrom.Value.Trim() != string.Empty)
			{
				/*
				this.txtSSCodeFrom.Value = string.Empty;
				txtMOCodeFrom.Value = txtMOCodeFrom.Value.Trim().ToUpper();
				MachineFeeder[] machineFeeder = this.LoadMaterial(this.txtMOCodeFrom.Value, tableFrom);
				if (machineFeeder != null && machineFeeder.Length > 0)
				{
					this.txtProductCodeFrom.Value = machineFeeder[0].ProductCode;
					checkedMOCodeFrom = txtMOCodeFrom.Value;
					this.txtSSCodeFrom.Value = machineFeeder[0].StepSequenceCode;
				}
				else
				{
					BenQGuru.eMES.MOModel.MOFacade moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
					MO mo = (MO)moFacade.GetMO(txtMOCodeFrom.Value);
					if (mo == null)
					{
						txtProductCodeFrom.Value = string.Empty;
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
						Application.DoEvents();
						txtMOCodeFrom.TextFocus(false, true);
					}
					else
					{
						txtProductCodeFrom.Value = mo.ItemCode;
						checkedMOCodeFrom = txtMOCodeFrom.Value;
					}
				}
				*/
				txtMOCodeFrom.Value = txtMOCodeFrom.Value.Trim().ToUpper();
				BenQGuru.eMES.MOModel.MOFacade moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
				MO mo = (MO)moFacade.GetMO(txtMOCodeFrom.Value);
				if (mo == null)
				{
					txtProductCodeFrom.Value = string.Empty;
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
					Application.DoEvents();
					txtMOCodeFrom.TextFocus(false, true);
				}
				else
				{
					txtProductCodeFrom.Value = mo.ItemCode;
					checkedMOCodeFrom = txtMOCodeFrom.Value;
					if (this.txtSSCodeFrom.Value != string.Empty)
					{
						LoadFromMOMaterial();
					}
					else
					{
						Application.DoEvents();
						this.txtSSCodeFrom.TextFocus(false, true);
					}
				}
			}
			else
			{
				checkedMOCodeTo = string.Empty;
				transferedMO = false;
			}
		}

		private void txtSSCodeFrom_TxtboxKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r' && txtSSCodeFrom.Value.Trim() != string.Empty)
			{
				if (this.txtMOCodeFrom.Value != string.Empty)
				{
					LoadFromMOMaterial();
				}
				else
				{
					Application.DoEvents();
					this.txtMOCodeFrom.TextFocus(false, true);
				}
			}
		}

		private void LoadFromMOMaterial()
		{
			txtMOCodeFrom.Value = txtMOCodeFrom.Value.Trim().ToUpper();
			txtSSCodeFrom.Value = txtSSCodeFrom.Value.Trim().ToUpper();
			if (txtMOCodeFrom.Value == string.Empty)
			{
				Application.DoEvents();
				txtMOCodeFrom.TextFocus(false, true);
				return;
			}
			if (txtSSCodeFrom.Value == string.Empty)
			{
				Application.DoEvents();
				txtSSCodeFrom.TextFocus(false, true);
				return;
			}
			if (checkedMOCodeFrom != txtMOCodeFrom.Value)
			{
				BenQGuru.eMES.MOModel.MOFacade moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
				MO mo = (MO)moFacade.GetMO(txtMOCodeFrom.Value);
				if (mo == null)
				{
					txtProductCodeFrom.Value = string.Empty;
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
					Application.DoEvents();
					txtMOCodeFrom.TextFocus(false, true);
				}
				else
				{
					txtProductCodeFrom.Value = mo.ItemCode;
					checkedMOCodeFrom = txtMOCodeFrom.Value;
				}
			}
			checkedSSCodeFrom = txtSSCodeFrom.Value;
			MachineFeeder[] machineFeeder = this.LoadMaterial(this.txtMOCodeFrom.Value, txtSSCodeFrom.Value, tableFrom);
		}

		private void txtMOCodeTo_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r' && txtMOCodeTo.Value.Trim() != string.Empty)
			{
				txtMOCodeTo.Value = txtMOCodeTo.Value.Trim().ToUpper();
				BenQGuru.eMES.MOModel.MOFacade moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
				MO mo = (MO)moFacade.GetMO(txtMOCodeTo.Value);
				if (mo == null)
				{
					txtProductCodeTo.Value = string.Empty;
					this.txtSSCodeTo.Value = string.Empty;
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
					Application.DoEvents();
					txtMOCodeTo.TextFocus(false, true);
				}
				else
				{
					txtProductCodeTo.Value = mo.ItemCode;
					GetMachineLoadedFeeder(mo);
					checkedMOCodeTo = txtMOCodeTo.Value;
					moCodeToExistMaterial = (tableTo.Rows.Count > int.Parse(this.txtDiffCount.Value));
					this.txtSSCodeTo.Value = Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode;
				}
			}
			else
			{
				checkedMOCodeTo = string.Empty;
				transferedMO = false;
				moCodeToExistMaterial = true;
			}
		}

		private void GetMachineLoadedFeeder(MO mo)
		{
			// 显示需要上料的记录
			tableTo.Rows.Clear();
			object[] objsSmt = smtFacade.QuerySMTFeederMaterialByProductCode(mo.ItemCode, Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode);
			if (objsSmt != null)
			{
				for (int i = 0; i < objsSmt.Length; i++)
				{
					SMTFeederMaterial item = (SMTFeederMaterial)objsSmt[i];
					DataRow row = tableTo.NewRow();
					row["MachineCode"] = item.MachineCode;
					row["MachineStationCode"] = item.MachineStationCode;
					row["FeederCode"] = string.Empty;
					row["FeederSpecCode"] = item.FeederSpecCode;
					row["FeederLeftCount"] = "0";
					row["ReelNo"] = string.Empty;
					row["ReelLeftQty"] = "0";
					row["MaterialCode"] = item.MaterialCode;
					tableTo.Rows.Add(row);
				}
			}
			// 显示已上料记录
			object[] objs = smtFacade.GetMachineLoadedFeeder(mo.MOCode);
			if (objs != null)
			{
				for (int i = 0; i < objs.Length; i++)
				{
					MachineFeeder mf = (MachineFeeder)objs[i];
					int iRowIdx = -1;
					for (int n = 0; n < tableTo.Rows.Count; n++)
					{
						if (tableTo.Rows[n]["MachineCode"].ToString() == mf.MachineCode && 
							tableTo.Rows[n]["MachineStationCode"].ToString() == mf.MachineStationCode && 
							tableTo.Rows[n]["FeederSpecCode"].ToString() == mf.FeederSpecCode)
						{
							iRowIdx = n;
							break;
						}
					}
					if (iRowIdx >= 0)
					{
						DataRow row = tableTo.Rows[iRowIdx];
						row["MachineCode"] = mf.MachineCode;
						row["MachineStationCode"] = mf.MachineStationCode;
						row["FeederCode"] = mf.FeederCode;
						row["FeederSpecCode"] = mf.FeederSpecCode;
						row["FeederLeftCount"] = "0";
						if (mf.NextReelNo == string.Empty)
							row["ReelNo"] = mf.ReelNo;
						else
							row["ReelNo"] = mf.NextReelNo;
						row["ReelLeftQty"] = "0";
						row["MaterialCode"] = mf.MaterialCode;
					}
				}
			}
			for (int i = 0; i < tableTo.Rows.Count; i++)
			{
				if (tableTo.Rows[i]["FeederCode"].ToString() == string.Empty)
				{
					gridTo.Rows[i].Appearance.ForeColor = Color.Red;
				}
			}
			this.txtDiffCount.Value = tableTo.Rows.Count.ToString();
			if (tableTo.Rows.Count > 0)
			{
				DataRow[] rows = tableTo.Select("FeederCode<>''");
				if (rows != null && rows.Length > 0)
					txtDiffCount.Value = (tableTo.Rows.Count - rows.Length).ToString();
			}
		}

		private string checkedMOCodeFrom = string.Empty;
		private string checkedMOCodeTo = string.Empty;
		private string checkedSSCodeFrom = string.Empty;
		private bool moCodeToExistMaterial = true;
		private bool transferedMO = false;
		private MachineFeeder[] transferedMachineFeederList = null;
		private void btnTransfer_Click(object sender, System.EventArgs e)
		{
			txtMOCodeFrom.Value = txtMOCodeFrom.Value.Trim().ToUpper();
			if (txtMOCodeFrom.Value == string.Empty)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
				Application.DoEvents();
				txtMOCodeFrom.TextFocus(false, true);
				return;
			}
			txtMOCodeTo.Value = txtMOCodeTo.Value.Trim().ToUpper();
			if (txtMOCodeTo.Value == string.Empty)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
				Application.DoEvents();
				txtMOCodeTo.TextFocus(false, true);
				return;
			}
			//Icyer,2007/01/15 修改	减少Open/Close的次数
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			try
			{
				if (txtMOCodeFrom.Value != checkedMOCodeFrom)
				{
					txtMOCodeFrom_TxtboxKeyPress(null, new KeyPressEventArgs('\r'));
				}
				if (txtMOCodeTo.Value != checkedMOCodeTo)
				{
					txtMOCodeTo_TxtboxKeyPress(null, new KeyPressEventArgs('\r'));
				}
				if (moCodeToExistMaterial == true)
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$MO_LoadedMaterial_Already"));
					Application.DoEvents();
					txtMOCodeTo.TextFocus(false, true);
					return;
				}
				Application.DoEvents();
				Messages messages = smtFacade.TransferMaterialByMO(txtMOCodeFrom.Value, txtSSCodeFrom.Value, txtMOCodeTo.Value, Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode);
				if (messages.IsSuccess() == false)
				{
					ApplicationRun.GetInfoForm().Add(messages);
				}
				else
				{
					transferedMO = true;
					transferedMachineFeederList = (MachineFeeder[])messages.GetData().Values;
					this.UpdateGridListAfterTransfer(transferedMachineFeederList);
				}
			}
            catch (Exception ex)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));
            }
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
			}
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			txtMOCodeFrom.Value = txtMOCodeFrom.Value.Trim().ToUpper();
			if (txtMOCodeFrom.Value == string.Empty)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
				Application.DoEvents();
				txtMOCodeFrom.TextFocus(false, true);
				return;
			}
			txtMOCodeTo.Value = txtMOCodeTo.Value.Trim().ToUpper();
			if (txtMOCodeTo.Value == string.Empty)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
				Application.DoEvents();
				txtMOCodeTo.TextFocus(false, true);
				return;
			}
			//Icyer,2007/01/15 修改	减少Open/Close的次数
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			try
			{
				if (txtMOCodeFrom.Value != checkedMOCodeFrom)
				{
					txtMOCodeFrom_TxtboxKeyPress(null, new KeyPressEventArgs('\r'));
				}
				if (txtSSCodeFrom.Value != checkedSSCodeFrom)
				{
					txtMOCodeFrom_TxtboxKeyPress(null, new KeyPressEventArgs('\r'));
				}
				if (txtMOCodeTo.Value != checkedMOCodeTo)
				{
					txtMOCodeTo_TxtboxKeyPress(null, new KeyPressEventArgs('\r'));
				}
				if (transferedMO == false || transferedMachineFeederList == null)
				{
					btnTransfer_Click(null, null);
				}
				if (transferedMO == false || transferedMachineFeederList == null)
					return;
				if (moCodeToExistMaterial == true)
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$MO_LoadedMaterial_Already"));
					Application.DoEvents();
					txtMOCodeTo.TextFocus(false, true);
					return;
				}
				//((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.BeginTransaction();
				try
				{
					Messages messages = smtFacade.TransferMaterialByMOUpdate(
						transferedMachineFeederList,
						txtMOCodeFrom.Value,
						txtSSCodeFrom.Value,
						txtMOCodeTo.Value,
						Service.ApplicationService.Current().UserCode,
						Service.ApplicationService.Current().LoginInfo.Resource.ResourceCode,
						Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode
						);
					ApplicationRun.GetInfoForm().Add(messages);
					if (messages.IsSuccess() == true)
					{
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CommitTransaction();
						txtMOCodeFrom_TxtboxKeyPress(null, new KeyPressEventArgs('\r'));
						txtMOCodeTo_TxtboxKeyPress(null, new KeyPressEventArgs('\r'));
						// 停线
						if (messages.GetData() != null && messages.GetData().Values != null && messages.GetData().Values.Length > 0)
						{
							SMTLineControlLog line = (SMTLineControlLog)messages.GetData().Values[0];
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
					{
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
					}
				}
				catch (Exception ex)
				{
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));
				}
				finally
				{
					//((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				}
			}
			catch {}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
			}
		}

		private MachineFeeder[] LoadMaterial(string moCode, string ssCode, DataTable table)
		{
			table.Rows.Clear();
			MachineFeeder[] machineFeeder = smtFacade.GetLoadedMaterialByMO(moCode, ssCode);
			if (machineFeeder != null)
			{
				for (int i = 0; i < machineFeeder.Length; i++)
				{
					DataRow row = table.NewRow();
					row["MachineCode"] = machineFeeder[i].MachineCode;
					row["MachineStationCode"] = machineFeeder[i].MachineStationCode;
					row["FeederCode"] = machineFeeder[i].FeederCode;
					row["FeederSpecCode"] = machineFeeder[i].FeederSpecCode;
					row["ReelNo"] = machineFeeder[i].ReelNo;
					row["MaterialCode"] = machineFeeder[i].MaterialCode;
					string[] strTmp = machineFeeder[i].EAttribute1.Split(':');
					if (strTmp[0] == string.Empty)
						strTmp[0] = "0";
					row["FeederLeftCount"] = strTmp[0];
					if (strTmp.Length > 1 && strTmp[1] != string.Empty)
						row["ReelLeftQty"] = strTmp[1];
					else
						row["ReelLeftQty"] = "0";
					table.Rows.Add(row);
				}
			}
			return machineFeeder;
		}

		private void UpdateGridListAfterTransfer(MachineFeeder[] machineFeeder)
		{
			if (machineFeeder == null)
				return;
			tableFrom.Rows.Clear();
			for (int i = 0; i < machineFeeder.Length; i++)
			{
				DataRow row = null;
				if (machineFeeder[i].FailReason == string.Empty)
				{
					DataRow[] rows = tableTo.Select("MachineCode='" + machineFeeder[i].MachineCode + "' AND MachineStationCode='" + machineFeeder[i].MachineStationCode + "'");
					if (rows != null && rows.Length > 0)
						row = rows[0];
					else
						continue;
				}
				else
					row = tableFrom.NewRow();
				row["MachineCode"] = machineFeeder[i].MachineCode;
				row["MachineStationCode"] = machineFeeder[i].MachineStationCode;
				row["FeederCode"] = machineFeeder[i].FeederCode;
				row["FeederSpecCode"] = machineFeeder[i].FeederSpecCode;
				row["ReelNo"] = machineFeeder[i].ReelNo;
				row["MaterialCode"] = machineFeeder[i].MaterialCode;
				string[] strTmp = machineFeeder[i].EAttribute1.Split(':');
				row["FeederLeftCount"] = strTmp[0];
				if (strTmp.Length > 1)
					row["ReelLeftQty"] = strTmp[1];
				if (machineFeeder[i].FailReason != string.Empty)
				{
					row["Memo"] = UserControl.MutiLanguages.ParserString(machineFeeder[i].FailReason);
					tableFrom.Rows.Add(row);
				}
			}
			for (int i = 0; i < tableTo.Rows.Count; i++)
			{
				if (tableTo.Rows[i]["FeederCode"].ToString() == string.Empty)
				{
					gridTo.Rows[i].Appearance.ForeColor = Color.Red;
				}
				else
				{
					gridTo.Rows[i].Appearance.ForeColor = Color.Black;
				}
			}
			this.txtDiffCount.Value = tableTo.Rows.Count.ToString();
			if (tableTo.Rows.Count > 0)
			{
				DataRow[] rows = tableTo.Select("FeederCode<>''");
				if (rows != null && rows.Length > 0)
					txtDiffCount.Value = (tableTo.Rows.Count - rows.Length).ToString();
			}
		}
	}
}
