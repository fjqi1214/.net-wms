using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using BenQGuru.eMES.Domain.ArmorPlate;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FArmorPlate 的摘要说明。
	/// </summary>
	public class FArmorPlate : BaseForm
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private UserControl.UCLabelEdit txtMOEdit;
		private UserControl.UCLabelEdit txtItemEdit;
		private UserControl.UCLabelEdit txtSSCodeEdit;
		private UserControl.UCLabelEdit txtAlertEdit;
		private System.Windows.Forms.Label labelTenThousand;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
		private System.Windows.Forms.RadioButton rdoReceive;
		private System.Windows.Forms.RadioButton rdoReturnR;
		private UserControl.UCLabelEdit txtUserEdit;
		private UserControl.UCLabelEdit txtAPIDEdit;
		private System.Windows.Forms.Label labelTensionTestResult;
		private UserControl.UCLabelEdit txtTenAEdit;
		private UserControl.UCLabelEdit txtTenBEdit;
		private UserControl.UCLabelEdit txtTenCEdit;
		private UserControl.UCLabelEdit txtTenDEdit;
		private UserControl.UCLabelEdit txtTenEEdit;
		private UserControl.UCButton btnReturn;
		private UserControl.UCButton btnUse;
		private UserControl.UCLabelEdit txtReturnUserEdit;
		private UserControl.UCLabelEdit txtAPID2Edit;
		private UserControl.UCLabelEdit txtCountEdit;
		private UserControl.UCButton btnOK;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private object _mo = null;
		private object _ss = null;
		private object _user = null;
		private object _armorPlate = null;
		private object _armorPlateInUse = null;
		private System.Data.DataTable dtSource = new System.Data.DataTable();
		private ArmorPlateFacade _apFacade = null;
        private UserControl.UCLabelEdit txtUnitPrintEdit;

		private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		public FArmorPlate()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			UserControl.UIStyleBuilder.GridUI(this.ultraGridMain);
			UserControl.UIStyleBuilder.FormUI(this);
			InitializeUltraGrid();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FArmorPlate));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ManufactureRSN");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BPCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UsedTimes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Version");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LBRate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TensionA");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TensionB");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TensionC");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TensionD");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TensionE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UnitPrint");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UsedDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UsedTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UsedUser");
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTenThousand = new System.Windows.Forms.Label();
            this.btnOK = new UserControl.UCButton();
            this.txtAlertEdit = new UserControl.UCLabelEdit();
            this.txtSSCodeEdit = new UserControl.UCLabelEdit();
            this.txtItemEdit = new UserControl.UCLabelEdit();
            this.txtMOEdit = new UserControl.UCLabelEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtUnitPrintEdit = new UserControl.UCLabelEdit();
            this.btnUse = new UserControl.UCButton();
            this.txtTenEEdit = new UserControl.UCLabelEdit();
            this.txtTenDEdit = new UserControl.UCLabelEdit();
            this.txtTenCEdit = new UserControl.UCLabelEdit();
            this.txtTenBEdit = new UserControl.UCLabelEdit();
            this.txtTenAEdit = new UserControl.UCLabelEdit();
            this.labelTensionTestResult = new System.Windows.Forms.Label();
            this.txtAPIDEdit = new UserControl.UCLabelEdit();
            this.txtUserEdit = new UserControl.UCLabelEdit();
            this.rdoReceive = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnReturn = new UserControl.UCButton();
            this.txtAPID2Edit = new UserControl.UCLabelEdit();
            this.txtReturnUserEdit = new UserControl.UCLabelEdit();
            this.rdoReturnR = new System.Windows.Forms.RadioButton();
            this.txtCountEdit = new UserControl.UCLabelEdit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelTenThousand);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.txtAlertEdit);
            this.panel1.Controls.Add(this.txtSSCodeEdit);
            this.panel1.Controls.Add(this.txtItemEdit);
            this.panel1.Controls.Add(this.txtMOEdit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(894, 67);
            this.panel1.TabIndex = 0;
            // 
            // labelTenThousand
            // 
            this.labelTenThousand.Location = new System.Drawing.Point(153, 37);
            this.labelTenThousand.Name = "labelTenThousand";
            this.labelTenThousand.Size = new System.Drawing.Size(83, 21);
            this.labelTenThousand.TabIndex = 10;
            this.labelTenThousand.Text = "万次";
            this.labelTenThousand.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.ButtonType = UserControl.ButtonTypes.Confirm;
            this.btnOK.Caption = "确定";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Location = new System.Drawing.Point(643, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 22);
            this.btnOK.TabIndex = 2;
            this.btnOK.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtAlertEdit
            // 
            this.txtAlertEdit.AllowEditOnlyChecked = true;
            this.txtAlertEdit.AutoSelectAll = false;
            this.txtAlertEdit.AutoUpper = true;
            this.txtAlertEdit.Caption = "预警使用次数";
            this.txtAlertEdit.Checked = false;
            this.txtAlertEdit.EditType = UserControl.EditTypes.Integer;
            this.txtAlertEdit.Location = new System.Drawing.Point(12, 37);
            this.txtAlertEdit.MaxLength = 40;
            this.txtAlertEdit.Multiline = false;
            this.txtAlertEdit.Name = "txtAlertEdit";
            this.txtAlertEdit.PasswordChar = '\0';
            this.txtAlertEdit.ReadOnly = false;
            this.txtAlertEdit.ShowCheckBox = false;
            this.txtAlertEdit.Size = new System.Drawing.Size(135, 22);
            this.txtAlertEdit.TabIndex = 3;
            this.txtAlertEdit.TabNext = true;
            this.txtAlertEdit.Value = "27";
            this.txtAlertEdit.WidthType = UserControl.WidthTypes.Tiny;
            this.txtAlertEdit.XAlign = 97;
            // 
            // txtSSCodeEdit
            // 
            this.txtSSCodeEdit.AllowEditOnlyChecked = true;
            this.txtSSCodeEdit.AutoSelectAll = false;
            this.txtSSCodeEdit.AutoUpper = true;
            this.txtSSCodeEdit.Caption = "产线编号";
            this.txtSSCodeEdit.Checked = false;
            this.txtSSCodeEdit.EditType = UserControl.EditTypes.String;
            this.txtSSCodeEdit.Location = new System.Drawing.Point(429, 7);
            this.txtSSCodeEdit.MaxLength = 40;
            this.txtSSCodeEdit.Multiline = false;
            this.txtSSCodeEdit.Name = "txtSSCodeEdit";
            this.txtSSCodeEdit.PasswordChar = '\0';
            this.txtSSCodeEdit.ReadOnly = false;
            this.txtSSCodeEdit.ShowCheckBox = false;
            this.txtSSCodeEdit.Size = new System.Drawing.Size(194, 24);
            this.txtSSCodeEdit.TabIndex = 1;
            this.txtSSCodeEdit.TabNext = true;
            this.txtSSCodeEdit.Value = "";
            this.txtSSCodeEdit.WidthType = UserControl.WidthTypes.Normal;
            this.txtSSCodeEdit.XAlign = 490;
            // 
            // txtItemEdit
            // 
            this.txtItemEdit.AllowEditOnlyChecked = true;
            this.txtItemEdit.AutoSelectAll = false;
            this.txtItemEdit.AutoUpper = true;
            this.txtItemEdit.Caption = "产品代码";
            this.txtItemEdit.Checked = false;
            this.txtItemEdit.EditType = UserControl.EditTypes.String;
            this.txtItemEdit.Location = new System.Drawing.Point(218, 7);
            this.txtItemEdit.MaxLength = 40;
            this.txtItemEdit.Multiline = false;
            this.txtItemEdit.Name = "txtItemEdit";
            this.txtItemEdit.PasswordChar = '\0';
            this.txtItemEdit.ReadOnly = true;
            this.txtItemEdit.ShowCheckBox = false;
            this.txtItemEdit.Size = new System.Drawing.Size(194, 24);
            this.txtItemEdit.TabIndex = 10;
            this.txtItemEdit.TabNext = true;
            this.txtItemEdit.Value = "";
            this.txtItemEdit.WidthType = UserControl.WidthTypes.Normal;
            this.txtItemEdit.XAlign = 279;
            // 
            // txtMOEdit
            // 
            this.txtMOEdit.AllowEditOnlyChecked = true;
            this.txtMOEdit.AutoSelectAll = false;
            this.txtMOEdit.AutoUpper = true;
            this.txtMOEdit.Caption = "工单号码";
            this.txtMOEdit.Checked = false;
            this.txtMOEdit.EditType = UserControl.EditTypes.String;
            this.txtMOEdit.Location = new System.Drawing.Point(12, 7);
            this.txtMOEdit.MaxLength = 40;
            this.txtMOEdit.Multiline = false;
            this.txtMOEdit.Name = "txtMOEdit";
            this.txtMOEdit.PasswordChar = '\0';
            this.txtMOEdit.ReadOnly = false;
            this.txtMOEdit.ShowCheckBox = false;
            this.txtMOEdit.Size = new System.Drawing.Size(194, 24);
            this.txtMOEdit.TabIndex = 0;
            this.txtMOEdit.TabNext = false;
            this.txtMOEdit.Value = "";
            this.txtMOEdit.WidthType = UserControl.WidthTypes.Normal;
            this.txtMOEdit.XAlign = 73;
            this.txtMOEdit.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMOEdit_TxtboxKeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ultraGridMain);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 67);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(894, 530);
            this.panel2.TabIndex = 1;
            // 
            // ultraGridMain
            // 
            this.ultraGridMain.Cursor = System.Windows.Forms.Cursors.Default;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 72;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 72;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 91;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 77;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 74;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 66;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Width = 82;
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Width = 71;
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Width = 76;
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn10.Width = 68;
            ultraGridColumn11.Header.VisiblePosition = 10;
            ultraGridColumn11.Width = 68;
            ultraGridColumn12.Header.VisiblePosition = 11;
            ultraGridColumn12.Width = 67;
            ultraGridColumn13.Header.VisiblePosition = 12;
            ultraGridColumn13.Width = 70;
            ultraGridColumn14.Header.VisiblePosition = 13;
            ultraGridColumn14.Width = 76;
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
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14});
            this.ultraGridMain.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMain.Location = new System.Drawing.Point(0, 0);
            this.ultraGridMain.Name = "ultraGridMain";
            this.ultraGridMain.Size = new System.Drawing.Size(894, 356);
            this.ultraGridMain.TabIndex = 171;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtUnitPrintEdit);
            this.panel4.Controls.Add(this.btnUse);
            this.panel4.Controls.Add(this.txtTenEEdit);
            this.panel4.Controls.Add(this.txtTenDEdit);
            this.panel4.Controls.Add(this.txtTenCEdit);
            this.panel4.Controls.Add(this.txtTenBEdit);
            this.panel4.Controls.Add(this.txtTenAEdit);
            this.panel4.Controls.Add(this.labelTensionTestResult);
            this.panel4.Controls.Add(this.txtAPIDEdit);
            this.panel4.Controls.Add(this.txtUserEdit);
            this.panel4.Controls.Add(this.rdoReceive);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 356);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(894, 100);
            this.panel4.TabIndex = 1;
            // 
            // txtUnitPrintEdit
            // 
            this.txtUnitPrintEdit.AllowEditOnlyChecked = true;
            this.txtUnitPrintEdit.AutoSelectAll = false;
            this.txtUnitPrintEdit.AutoUpper = true;
            this.txtUnitPrintEdit.Caption = "单片印刷次数";
            this.txtUnitPrintEdit.Checked = false;
            this.txtUnitPrintEdit.EditType = UserControl.EditTypes.String;
            this.txtUnitPrintEdit.Location = new System.Drawing.Point(488, 6);
            this.txtUnitPrintEdit.MaxLength = 40;
            this.txtUnitPrintEdit.Multiline = false;
            this.txtUnitPrintEdit.Name = "txtUnitPrintEdit";
            this.txtUnitPrintEdit.PasswordChar = '\0';
            this.txtUnitPrintEdit.ReadOnly = false;
            this.txtUnitPrintEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUnitPrintEdit.ShowCheckBox = false;
            this.txtUnitPrintEdit.Size = new System.Drawing.Size(135, 24);
            this.txtUnitPrintEdit.TabIndex = 18;
            this.txtUnitPrintEdit.TabNext = true;
            this.txtUnitPrintEdit.Value = "1";
            this.txtUnitPrintEdit.WidthType = UserControl.WidthTypes.Tiny;
            this.txtUnitPrintEdit.XAlign = 573;
            // 
            // btnUse
            // 
            this.btnUse.BackColor = System.Drawing.SystemColors.Control;
            this.btnUse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUse.BackgroundImage")));
            this.btnUse.ButtonType = UserControl.ButtonTypes.Confirm;
            this.btnUse.Caption = "领用";
            this.btnUse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUse.Location = new System.Drawing.Point(488, 67);
            this.btnUse.Name = "btnUse";
            this.btnUse.Size = new System.Drawing.Size(88, 22);
            this.btnUse.TabIndex = 17;
            this.btnUse.Click += new System.EventHandler(this.btnUse_Click);
            // 
            // txtTenEEdit
            // 
            this.txtTenEEdit.AllowEditOnlyChecked = true;
            this.txtTenEEdit.AutoSelectAll = false;
            this.txtTenEEdit.AutoUpper = true;
            this.txtTenEEdit.Caption = "E";
            this.txtTenEEdit.Checked = false;
            this.txtTenEEdit.EditType = UserControl.EditTypes.Number;
            this.txtTenEEdit.Location = new System.Drawing.Point(293, 66);
            this.txtTenEEdit.MaxLength = 40;
            this.txtTenEEdit.Multiline = false;
            this.txtTenEEdit.Name = "txtTenEEdit";
            this.txtTenEEdit.PasswordChar = '\0';
            this.txtTenEEdit.ReadOnly = false;
            this.txtTenEEdit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTenEEdit.ShowCheckBox = false;
            this.txtTenEEdit.Size = new System.Drawing.Size(119, 23);
            this.txtTenEEdit.TabIndex = 7;
            this.txtTenEEdit.TabNext = true;
            this.txtTenEEdit.Value = "";
            this.txtTenEEdit.WidthType = UserControl.WidthTypes.Small;
            this.txtTenEEdit.XAlign = 312;
            // 
            // txtTenDEdit
            // 
            this.txtTenDEdit.AllowEditOnlyChecked = true;
            this.txtTenDEdit.AutoSelectAll = false;
            this.txtTenDEdit.AutoUpper = true;
            this.txtTenDEdit.Caption = "D";
            this.txtTenDEdit.Checked = false;
            this.txtTenDEdit.EditType = UserControl.EditTypes.Number;
            this.txtTenDEdit.Location = new System.Drawing.Point(141, 66);
            this.txtTenDEdit.MaxLength = 40;
            this.txtTenDEdit.Multiline = false;
            this.txtTenDEdit.Name = "txtTenDEdit";
            this.txtTenDEdit.PasswordChar = '\0';
            this.txtTenDEdit.ReadOnly = false;
            this.txtTenDEdit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTenDEdit.ShowCheckBox = false;
            this.txtTenDEdit.Size = new System.Drawing.Size(119, 23);
            this.txtTenDEdit.TabIndex = 6;
            this.txtTenDEdit.TabNext = true;
            this.txtTenDEdit.Value = "";
            this.txtTenDEdit.WidthType = UserControl.WidthTypes.Small;
            this.txtTenDEdit.XAlign = 160;
            // 
            // txtTenCEdit
            // 
            this.txtTenCEdit.AllowEditOnlyChecked = true;
            this.txtTenCEdit.AutoSelectAll = false;
            this.txtTenCEdit.AutoUpper = true;
            this.txtTenCEdit.Caption = "C";
            this.txtTenCEdit.Checked = false;
            this.txtTenCEdit.EditType = UserControl.EditTypes.Number;
            this.txtTenCEdit.Location = new System.Drawing.Point(456, 37);
            this.txtTenCEdit.MaxLength = 40;
            this.txtTenCEdit.Multiline = false;
            this.txtTenCEdit.Name = "txtTenCEdit";
            this.txtTenCEdit.PasswordChar = '\0';
            this.txtTenCEdit.ReadOnly = false;
            this.txtTenCEdit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTenCEdit.ShowCheckBox = false;
            this.txtTenCEdit.Size = new System.Drawing.Size(119, 23);
            this.txtTenCEdit.TabIndex = 5;
            this.txtTenCEdit.TabNext = true;
            this.txtTenCEdit.Value = "";
            this.txtTenCEdit.WidthType = UserControl.WidthTypes.Small;
            this.txtTenCEdit.XAlign = 475;
            // 
            // txtTenBEdit
            // 
            this.txtTenBEdit.AllowEditOnlyChecked = true;
            this.txtTenBEdit.AutoSelectAll = false;
            this.txtTenBEdit.AutoUpper = true;
            this.txtTenBEdit.Caption = "B";
            this.txtTenBEdit.Checked = false;
            this.txtTenBEdit.EditType = UserControl.EditTypes.Number;
            this.txtTenBEdit.Location = new System.Drawing.Point(293, 37);
            this.txtTenBEdit.MaxLength = 40;
            this.txtTenBEdit.Multiline = false;
            this.txtTenBEdit.Name = "txtTenBEdit";
            this.txtTenBEdit.PasswordChar = '\0';
            this.txtTenBEdit.ReadOnly = false;
            this.txtTenBEdit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTenBEdit.ShowCheckBox = false;
            this.txtTenBEdit.Size = new System.Drawing.Size(119, 23);
            this.txtTenBEdit.TabIndex = 4;
            this.txtTenBEdit.TabNext = true;
            this.txtTenBEdit.Value = "";
            this.txtTenBEdit.WidthType = UserControl.WidthTypes.Small;
            this.txtTenBEdit.XAlign = 312;
            // 
            // txtTenAEdit
            // 
            this.txtTenAEdit.AllowEditOnlyChecked = true;
            this.txtTenAEdit.AutoSelectAll = false;
            this.txtTenAEdit.AutoUpper = true;
            this.txtTenAEdit.Caption = "A";
            this.txtTenAEdit.Checked = false;
            this.txtTenAEdit.EditType = UserControl.EditTypes.Number;
            this.txtTenAEdit.Location = new System.Drawing.Point(141, 37);
            this.txtTenAEdit.MaxLength = 40;
            this.txtTenAEdit.Multiline = false;
            this.txtTenAEdit.Name = "txtTenAEdit";
            this.txtTenAEdit.PasswordChar = '\0';
            this.txtTenAEdit.ReadOnly = false;
            this.txtTenAEdit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTenAEdit.ShowCheckBox = false;
            this.txtTenAEdit.Size = new System.Drawing.Size(119, 23);
            this.txtTenAEdit.TabIndex = 3;
            this.txtTenAEdit.TabNext = true;
            this.txtTenAEdit.Value = "";
            this.txtTenAEdit.WidthType = UserControl.WidthTypes.Small;
            this.txtTenAEdit.XAlign = 160;
            // 
            // labelTensionTestResult
            // 
            this.labelTensionTestResult.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelTensionTestResult.Location = new System.Drawing.Point(63, 37);
            this.labelTensionTestResult.Name = "labelTensionTestResult";
            this.labelTensionTestResult.Size = new System.Drawing.Size(84, 21);
            this.labelTensionTestResult.TabIndex = 11;
            this.labelTensionTestResult.Text = "张力测试结果";
            this.labelTensionTestResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAPIDEdit
            // 
            this.txtAPIDEdit.AllowEditOnlyChecked = true;
            this.txtAPIDEdit.AutoSelectAll = false;
            this.txtAPIDEdit.AutoUpper = true;
            this.txtAPIDEdit.Caption = "厂内编号";
            this.txtAPIDEdit.Checked = false;
            this.txtAPIDEdit.EditType = UserControl.EditTypes.String;
            this.txtAPIDEdit.Location = new System.Drawing.Point(278, 7);
            this.txtAPIDEdit.MaxLength = 40;
            this.txtAPIDEdit.Multiline = false;
            this.txtAPIDEdit.Name = "txtAPIDEdit";
            this.txtAPIDEdit.PasswordChar = '\0';
            this.txtAPIDEdit.ReadOnly = false;
            this.txtAPIDEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtAPIDEdit.ShowCheckBox = false;
            this.txtAPIDEdit.Size = new System.Drawing.Size(194, 24);
            this.txtAPIDEdit.TabIndex = 2;
            this.txtAPIDEdit.TabNext = true;
            this.txtAPIDEdit.Value = "";
            this.txtAPIDEdit.WidthType = UserControl.WidthTypes.Normal;
            this.txtAPIDEdit.XAlign = 339;
            // 
            // txtUserEdit
            // 
            this.txtUserEdit.AllowEditOnlyChecked = true;
            this.txtUserEdit.AutoSelectAll = false;
            this.txtUserEdit.AutoUpper = true;
            this.txtUserEdit.Caption = "领用人员";
            this.txtUserEdit.Checked = false;
            this.txtUserEdit.EditType = UserControl.EditTypes.String;
            this.txtUserEdit.Location = new System.Drawing.Point(66, 7);
            this.txtUserEdit.MaxLength = 40;
            this.txtUserEdit.Multiline = false;
            this.txtUserEdit.Name = "txtUserEdit";
            this.txtUserEdit.PasswordChar = '\0';
            this.txtUserEdit.ReadOnly = false;
            this.txtUserEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUserEdit.ShowCheckBox = false;
            this.txtUserEdit.Size = new System.Drawing.Size(194, 24);
            this.txtUserEdit.TabIndex = 1;
            this.txtUserEdit.TabNext = true;
            this.txtUserEdit.Value = "";
            this.txtUserEdit.WidthType = UserControl.WidthTypes.Normal;
            this.txtUserEdit.XAlign = 127;
            // 
            // rdoReceive
            // 
            this.rdoReceive.Checked = true;
            this.rdoReceive.Location = new System.Drawing.Point(12, 7);
            this.rdoReceive.Name = "rdoReceive";
            this.rdoReceive.Size = new System.Drawing.Size(53, 23);
            this.rdoReceive.TabIndex = 0;
            this.rdoReceive.TabStop = true;
            this.rdoReceive.Text = "领用";
            this.rdoReceive.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnReturn);
            this.panel3.Controls.Add(this.txtAPID2Edit);
            this.panel3.Controls.Add(this.txtReturnUserEdit);
            this.panel3.Controls.Add(this.rdoReturnR);
            this.panel3.Controls.Add(this.txtCountEdit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 456);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(894, 74);
            this.panel3.TabIndex = 0;
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.SystemColors.Control;
            this.btnReturn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReturn.BackgroundImage")));
            this.btnReturn.ButtonType = UserControl.ButtonTypes.Confirm;
            this.btnReturn.Caption = "退回";
            this.btnReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturn.Enabled = false;
            this.btnReturn.Location = new System.Drawing.Point(487, 37);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(88, 22);
            this.btnReturn.TabIndex = 18;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // txtAPID2Edit
            // 
            this.txtAPID2Edit.AllowEditOnlyChecked = true;
            this.txtAPID2Edit.AutoSelectAll = false;
            this.txtAPID2Edit.AutoUpper = true;
            this.txtAPID2Edit.Caption = "厂内编号";
            this.txtAPID2Edit.Checked = false;
            this.txtAPID2Edit.EditType = UserControl.EditTypes.String;
            this.txtAPID2Edit.Enabled = false;
            this.txtAPID2Edit.Location = new System.Drawing.Point(278, 3);
            this.txtAPID2Edit.MaxLength = 40;
            this.txtAPID2Edit.Multiline = false;
            this.txtAPID2Edit.Name = "txtAPID2Edit";
            this.txtAPID2Edit.PasswordChar = '\0';
            this.txtAPID2Edit.ReadOnly = false;
            this.txtAPID2Edit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtAPID2Edit.ShowCheckBox = false;
            this.txtAPID2Edit.Size = new System.Drawing.Size(194, 24);
            this.txtAPID2Edit.TabIndex = 2;
            this.txtAPID2Edit.TabNext = true;
            this.txtAPID2Edit.Value = "";
            this.txtAPID2Edit.WidthType = UserControl.WidthTypes.Normal;
            this.txtAPID2Edit.XAlign = 339;
            // 
            // txtReturnUserEdit
            // 
            this.txtReturnUserEdit.AllowEditOnlyChecked = true;
            this.txtReturnUserEdit.AutoSelectAll = false;
            this.txtReturnUserEdit.AutoUpper = true;
            this.txtReturnUserEdit.Caption = "退回人员";
            this.txtReturnUserEdit.Checked = false;
            this.txtReturnUserEdit.EditType = UserControl.EditTypes.String;
            this.txtReturnUserEdit.Enabled = false;
            this.txtReturnUserEdit.Location = new System.Drawing.Point(66, 7);
            this.txtReturnUserEdit.MaxLength = 40;
            this.txtReturnUserEdit.Multiline = false;
            this.txtReturnUserEdit.Name = "txtReturnUserEdit";
            this.txtReturnUserEdit.PasswordChar = '\0';
            this.txtReturnUserEdit.ReadOnly = false;
            this.txtReturnUserEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtReturnUserEdit.ShowCheckBox = false;
            this.txtReturnUserEdit.Size = new System.Drawing.Size(194, 24);
            this.txtReturnUserEdit.TabIndex = 1;
            this.txtReturnUserEdit.TabNext = true;
            this.txtReturnUserEdit.Value = "";
            this.txtReturnUserEdit.WidthType = UserControl.WidthTypes.Normal;
            this.txtReturnUserEdit.XAlign = 127;
            // 
            // rdoReturnR
            // 
            this.rdoReturnR.Location = new System.Drawing.Point(12, 7);
            this.rdoReturnR.Name = "rdoReturnR";
            this.rdoReturnR.Size = new System.Drawing.Size(53, 23);
            this.rdoReturnR.TabIndex = 0;
            this.rdoReturnR.Text = "退回";
            this.rdoReturnR.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // txtCountEdit
            // 
            this.txtCountEdit.AllowEditOnlyChecked = true;
            this.txtCountEdit.AutoSelectAll = false;
            this.txtCountEdit.AutoUpper = true;
            this.txtCountEdit.Caption = "加工小板数量";
            this.txtCountEdit.Checked = false;
            this.txtCountEdit.EditType = UserControl.EditTypes.Integer;
            this.txtCountEdit.Enabled = false;
            this.txtCountEdit.Location = new System.Drawing.Point(75, 36);
            this.txtCountEdit.MaxLength = 40;
            this.txtCountEdit.Multiline = false;
            this.txtCountEdit.Name = "txtCountEdit";
            this.txtCountEdit.PasswordChar = '\0';
            this.txtCountEdit.ReadOnly = false;
            this.txtCountEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCountEdit.ShowCheckBox = false;
            this.txtCountEdit.Size = new System.Drawing.Size(185, 23);
            this.txtCountEdit.TabIndex = 3;
            this.txtCountEdit.TabNext = true;
            this.txtCountEdit.Value = "";
            this.txtCountEdit.WidthType = UserControl.WidthTypes.Small;
            this.txtCountEdit.XAlign = 160;
            // 
            // FArmorPlate
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(894, 597);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FArmorPlate";
            this.Text = "钢板使用管理";
            this.Load += new System.EventHandler(this.FArmorPlate_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			bool checkedrb1 = !this.rdoReturnR.Checked;
			this.rdoReceive.Checked = checkedrb1;

            RadioButtonCheckedChanged( checkedrb1 );	
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			bool checkedrb1 = this.rdoReceive.Checked;
			this.rdoReturnR.Checked = !checkedrb1;

			RadioButtonCheckedChanged( checkedrb1 );			
		}

		private void RadioButtonCheckedChanged( bool checkUse )
		{
			bool checkedrb1 = checkUse;

			this.txtUserEdit.Enabled = checkedrb1;
			this.txtAPIDEdit.Enabled = checkedrb1;
            this.txtUnitPrintEdit.Enabled = checkedrb1;
			this.txtTenAEdit.Enabled = checkedrb1;
			this.txtTenBEdit.Enabled = checkedrb1;
			this.txtTenCEdit.Enabled = checkedrb1;
			this.txtTenDEdit.Enabled = checkedrb1;
			this.txtTenEEdit.Enabled = checkedrb1;

			this.txtUserEdit.Value = String.Empty;
			this.txtAPIDEdit.Value = String.Empty;
            this.txtUnitPrintEdit.Value = "1";
			this.txtTenAEdit.Value = String.Empty;
			this.txtTenBEdit.Value = String.Empty;
			this.txtTenCEdit.Value = String.Empty;
			this.txtTenDEdit.Value = String.Empty;
			this.txtTenEEdit.Value = String.Empty;

			this.btnUse.Enabled = checkedrb1;

			this.txtReturnUserEdit.Enabled = !checkedrb1;
			this.txtAPID2Edit.Enabled = !checkedrb1;
			this.txtCountEdit.Enabled = !checkedrb1;

			this.txtReturnUserEdit.Value = String.Empty;
			this.txtAPID2Edit.Value = String.Empty;
			this.txtCountEdit.Value = String.Empty;

			this.btnReturn.Enabled = !checkedrb1;
		}

		private void btnQuery_Click(object sender, EventArgs e)
		{
			string moCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtMOEdit.Value) );
			string ssCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtSSCodeEdit.Value) );
			if( CheckMO( moCode ) 
				&& CheckStepSequnce( ssCode ))
			{
				_apFacade = new ArmorPlateFacade( this.DataProvider );
				FillUltraGridMain( _apFacade.QueryArmorPlateContol( moCode, ssCode ) );
			}

		}

		private void txtMOEdit_TxtboxKeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				string moCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtMOEdit.Value) );
				if( CheckMO( moCode ) )
				{
					this.txtSSCodeEdit.TextFocus(true, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");
                    //Remove UCLabel.SelectAll;
				}
			}
		}

		private void InitializeUltraGrid()
		{
			dtSource.Columns.Clear();
			
			dtSource.Columns.Add("ManufactureRSN",typeof(string)).ReadOnly = true;
			dtSource.Columns.Add("BPCode",typeof(string)).ReadOnly = true;
			dtSource.Columns.Add("UsedTimes",typeof(string)).ReadOnly = true;
			dtSource.Columns.Add("Version",typeof(string)).ReadOnly = true;
			dtSource.Columns.Add("LBRate",typeof(string)).ReadOnly = true;
            dtSource.Columns.Add("TensionA", typeof(string)).ReadOnly = true;
            dtSource.Columns.Add("TensionB", typeof(string)).ReadOnly = true;
            dtSource.Columns.Add("TensionC", typeof(string)).ReadOnly = true;
            dtSource.Columns.Add("TensionD", typeof(string)).ReadOnly = true;
            dtSource.Columns.Add("TensionE", typeof(string)).ReadOnly = true;
            dtSource.Columns.Add("UnitPrint", typeof(string)).ReadOnly = true;
            dtSource.Columns.Add("UsedDate", typeof(string)).ReadOnly = true;
            dtSource.Columns.Add("UsedTime", typeof(string)).ReadOnly = true;
            dtSource.Columns.Add("UsedUser", typeof(string)).ReadOnly = true;

			this.ultraGridMain.DataSource = dtSource;

		}

		private void FillUltraGridMain(object[] objs)
		{
			this.dtSource.Rows.Clear();
			if(objs == null)
			{
				return;
			}
			else
			{
				foreach(ArmorPlateContol obj in objs)
				{
					this.dtSource.Rows.Add( 
						new object[] {
										 obj.ArmorPlateID.ToString(),
										 obj.BasePlateCode.ToString(),
										 obj.UsedTimes.ToString("##.##"),
										 obj.Version.ToString(),
										 obj.LBRate.ToString("##.##"),

										 obj.TensionA.ToString(),
										 obj.TensionB.ToString(),
										 obj.TensionC.ToString(),
										 obj.TensionD.ToString(),
										 obj.TensionE.ToString(),
                                         obj.UnitPrint.ToString(),
										 obj.UsedUser.ToString(),
										 FormatHelper.ToDateString( obj.UsedDate ),
										 FormatHelper.ToTimeString( obj.UsedTime )
									 });
				}
			}
		}

		private void btnUse_Click(object sender, EventArgs e)
		{
			string moCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtMOEdit.Value) );
			string ssCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtSSCodeEdit.Value) );
			string apid = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtAPIDEdit.Value) );
			string userCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtUserEdit.Value) );
			if( CheckMO( moCode ) 
				&& CheckStepSequnce( ssCode ))
			{
				if( !CheckUser( userCode ))
				{
					this.txtUserEdit.TextFocus(true, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");
					return;
				}

				if( !CheckArmorPlate( apid, moCode, ssCode, true ) )
				{
					this.txtAPIDEdit.TextFocus(true, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");
					return;
				}

                if (!CheckUnitPrint(FormatHelper.CleanString(this.txtUnitPrintEdit.Value)))
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_UnitPrint_Not_In_Range"));
                    this.txtUnitPrintEdit.TextFocus(true, true);
                    return;
                }
                int unitPrint = Convert.ToInt32(FormatHelper.CleanString(this.txtUnitPrintEdit.Value));

				#region Check Test Tension
				bool tenABool = CheckTension( FormatHelper.CleanString( this.txtTenAEdit.Value) );
				if( !tenABool )
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$TestTension_MustBe_Digital" ) );
					this.txtTenAEdit.TextFocus(true, true);
					return;
				}

				bool tenBBool = CheckTension( FormatHelper.CleanString( this.txtTenBEdit.Value) );
				if( !tenBBool )
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$TestTension_MustBe_Digital" ) );
					this.txtTenBEdit.TextFocus(true, true);
					return;
				}

				bool tenCBool = CheckTension( FormatHelper.CleanString( this.txtTenCEdit.Value) );
				if( !tenCBool )
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$TestTension_MustBe_Digital" ) );
					this.txtTenCEdit.TextFocus(true, true);
					return;
				}

				bool tenDBool = CheckTension( FormatHelper.CleanString( this.txtTenDEdit.Value) );
				if( !tenDBool )
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$TestTension_MustBe_Digital" ) );
					this.txtTenDEdit.TextFocus(true, true);
					return;
				}

				bool tenEBool = CheckTension( FormatHelper.CleanString( this.txtTenEEdit.Value) );
				if( !tenEBool )
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$TestTension_MustBe_Digital" ) );
					this.txtTenEEdit.TextFocus(true, true);
					return;
				}

				decimal tenA = Convert.ToDecimal( FormatHelper.CleanString( this.txtTenAEdit.Value) ) ;
				decimal tenB = Convert.ToDecimal( FormatHelper.CleanString( this.txtTenBEdit.Value) ) ;
				decimal tenC = Convert.ToDecimal( FormatHelper.CleanString( this.txtTenCEdit.Value) ) ;
				decimal tenD = Convert.ToDecimal( FormatHelper.CleanString( this.txtTenDEdit.Value) ) ;
				decimal tenE = Convert.ToDecimal( FormatHelper.CleanString( this.txtTenEEdit.Value) ) ;

				string msg = string.Empty;
				if( (_armorPlate as ArmorPlate).TensionA > tenA )
				{
					msg += string.Format( "$Tension_A, $StandardValue {0}, $ActualValue {1},\r\n", (_armorPlate as ArmorPlate).TensionA, tenA );
				}

				if( (_armorPlate as ArmorPlate).TensionB > tenB )
				{
					msg += string.Format( "$Tension_B, $StandardValue {0}, $ActualValue {1},\r\n", (_armorPlate as ArmorPlate).TensionB, tenB );
				}

				if( (_armorPlate as ArmorPlate).TensionC > tenC )
				{
					msg += string.Format( "$Tension_C, $StandardValue {0}, $ActualValue {1},\r\n", (_armorPlate as ArmorPlate).TensionC, tenC );
				}

				if( (_armorPlate as ArmorPlate).TensionD > tenD )
				{
					msg += string.Format( "$Tension_D, $StandardValue {0}, $ActualValue {1},\r\n", (_armorPlate as ArmorPlate).TensionD, tenD );
				}

				if( (_armorPlate as ArmorPlate).TensionE > tenE )
				{
					msg += string.Format( "$Tension_E, $StandardValue {0}, $ActualValue {1},\r\n", (_armorPlate as ArmorPlate).TensionE, tenE );
				}

				if( msg.Length>0 )
				{
					msg += "$Error_Can_Not_Use";
					ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Normal, msg ) );

                    DialogResult dr = MessageBox.Show(UserControl.MutiLanguages.ParserString("$CS_ArmorPlate") + " " + apid + " " + UserControl.MutiLanguages.ParserString("$CS_ArmorPlate_Tension_Not_Standard"), UserControl.MutiLanguages.ParserString("$ShowMessage"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

					if( dr == DialogResult.No )
					{
						this.txtTenAEdit.Value = string.Empty;
						this.txtTenBEdit.Value = string.Empty;
						this.txtTenCEdit.Value = string.Empty;
						this.txtTenDEdit.Value = string.Empty;
						this.txtTenEEdit.Value = string.Empty;

						this.txtAPIDEdit.TextFocus(true, true);
						//System.Windows.Forms.SendKeys.Send("+{TAB}");
						return;
					}
				}
				#endregion

				int alertCount = 0;
				try
				{
					alertCount = int.Parse( FormatHelper.CleanString( this.txtAlertEdit.Value)  ) * 10000;
				}
				catch
				{
					alertCount = -1;
				}

				if( alertCount<0 )
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$Error_AlertTimes_Should_MoreThan_Zero" ) );
					this.txtAlertEdit.TextFocus(true, true);
					return;
				}

				if( (_armorPlate as ArmorPlate).UsedTimes > alertCount )
				{
                    DialogResult dr = MessageBox.Show(UserControl.MutiLanguages.ParserString("$CS_ArmorPlate") + " " + apid + " " + UserControl.MutiLanguages.ParserString("$CS_ArmorPlate_Over_AlarmTimes"), UserControl.MutiLanguages.ParserString("$ShowMessage"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

					if( dr == DialogResult.No )
					{
						return;
					}
				}

				if(_apFacade == null){_apFacade=new ArmorPlateFacade(this.DataProvider) ;}
				ArmorPlateContol obj = _apFacade.CreateNewArmorPlateContol();

				obj.OID = Guid.NewGuid().ToString();
                obj.ArmorPlateID = (_armorPlate as ArmorPlate).ArmorPlateID;
				obj.BasePlateCode = (_armorPlate as ArmorPlate).BasePlateCode;
				obj.LBRate = (_armorPlate as ArmorPlate).LBRate;
				obj.ManufacturerSN = (_armorPlate as ArmorPlate).ManufacturerSN;
				obj.Status = FormatHelper.TRUE_STRING;
				obj.TensionA = tenA;
				obj.TensionB = tenB;
				obj.TensionC = tenC;
				obj.TensionD = tenD;
				obj.TensionE = tenE;
                obj.UnitPrint = unitPrint;
				obj.Thickness = (_armorPlate as ArmorPlate).Thickness;
				obj.UsedTimes = (_armorPlate as ArmorPlate).UsedTimes;
				obj.Version = (_armorPlate as ArmorPlate).Version;

				obj.UsedMOCode = moCode;
				obj.UsedSSCode = ssCode;
				obj.UsedUser = userCode;
				obj.UsedDate = FormatHelper.TODateInt( DateTime.Now );
				obj.UsedTime = FormatHelper.TOTimeInt( DateTime.Now );

				obj.MaintainUser = ApplicationService.Current().UserCode ;

				_apFacade.AddArmorPlateContol( obj );

				this.rdoReceive.Checked = true;
				RadioButtonCheckedChanged( true );
				FillUltraGridMain( _apFacade.QueryArmorPlateContol( moCode, ssCode ) );

			}
		}


		private void btnReturn_Click(object sender, EventArgs e)
		{
			string moCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtMOEdit.Value) );
			string ssCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtSSCodeEdit.Value) );
			string apid = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtAPID2Edit.Value) );
			string userCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtReturnUserEdit.Value) );
			if( CheckMO( moCode ) 
				&& CheckStepSequnce( ssCode ))
			{
				if( !CheckUser( userCode ))
				{
					this.txtReturnUserEdit.TextFocus(true, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");
					return;
				}

				if( !CheckArmorPlate( apid, moCode, ssCode, false ) )
				{
					this.txtAPID2Edit.TextFocus(true, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");
					return;
				}

				int countInMO = 0;
				try
				{
					countInMO = Convert.ToInt32( FormatHelper.CleanString( this.txtCountEdit.Value) );
				}
				catch
				{
					countInMO = -1;
				}
				if( countInMO < 1 )
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$Error_Count_LessThan_One" ) );

					this.txtCountEdit.TextFocus(true, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");
					return ;
				}

				ArmorPlateContol obj = _armorPlateInUse as ArmorPlateContol;
				obj.Status = FormatHelper.FALSE_STRING;
				obj.UsedTimesInMO = countInMO*int.Parse(obj.UnitPrint.ToString())/int.Parse( obj.LBRate.ToString() );
				obj.UsedTimes += obj.UsedTimesInMO;
				obj.ReturnUser = userCode;
				obj.ReturnDate = FormatHelper.TODateInt( DateTime.Now );
				obj.ReturnTime = FormatHelper.TOTimeInt( DateTime.Now );
				obj.MaintainUser = ApplicationService.Current().UserCode ;

				(_armorPlate as ArmorPlate).UsedTimes += obj.UsedTimesInMO;
				if(_apFacade == null){_apFacade=new ArmorPlateFacade(this.DataProvider) ;}
				_apFacade.UpdateArmorPlateContol( obj );
				_apFacade.UpdateArmorPlate( _armorPlate as ArmorPlate );
				
				this.rdoReceive.Checked = true;
				RadioButtonCheckedChanged( true );
				FillUltraGridMain( _apFacade.QueryArmorPlateContol( moCode, ssCode ) );
			}
		}

		private bool CheckMO( string modCode )
		{
			if( _mo == null || string.Compare(( _mo as MO).MOCode, modCode, true)!=0 )
			{
				if( modCode.Length>0 )
				{
					MOFacade moFacade = new MOFacade(this.DataProvider);
					_mo = moFacade.GetMO( modCode );
					if(_mo == null)
					{
						ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$CS_MO_Not_Exist" ) );

						this.txtItemEdit.Value = string.Empty;
						this.txtMOEdit.TextFocus(true, true);
						//System.Windows.Forms.SendKeys.Send("+{TAB}");
                        //Remove UCLabel.SelectAll;
						return false;
					}
					else
					{
						this.txtItemEdit.Value = ( _mo as MO).ItemCode ;
						return true;
					}
				}
				else
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$CS_CMPleaseInputMO" ) );

					this.txtItemEdit.Value = string.Empty;
					this.txtMOEdit.TextFocus(true, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");
                    //Remove UCLabel.SelectAll;
					return false;
				}
			}

			return true;
		}

		private bool CheckStepSequnce( string ssCode )
		{
			if( _ss == null || string.Compare(( _ss as StepSequence).StepSequenceCode, ssCode, true)!=0 )
			{
				if( ssCode.Length>0 )
				{
					BaseModelFacade bmFacade = new BaseModelFacade( this.DataProvider );
					_ss = bmFacade.GetStepSequence( FormatHelper.CleanString( this.txtSSCodeEdit.Value) );
					if(_ss == null)
					{
						ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$CS_STEPSEQUENCE_NOT_EXIST" ) );
					
						this.txtSSCodeEdit.TextFocus(true, true);
						//System.Windows.Forms.SendKeys.Send("+{TAB}");
						return false;
					}
				}
				else
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$CS_Please_Input_SSCode" ) );

					this.txtSSCodeEdit.TextFocus(true, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");

					return false;
				}
			}

			return true;
		}

		private bool CheckUser( string userCode )
		{
			bool pass = true;
			if( _user == null || string.Compare(( _user as User).UserCode, userCode, true)!=0 )
			{
				if( userCode.Length>0 )
				{
					UserFacade userFacade = new UserFacade( this.DataProvider );
					_user = userFacade.GetUser( userCode );
					if(_user == null)
					{
						ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$Error_User_Not_Exist" ) );
						pass = false;
					}
				}
				else
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$Error_User_Code_Empty" ) );
					pass = false;
				}
			}

			return pass;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="apid"></param>
		/// <param name="isUse">true:检查领用；false:检查退回</param>
		/// <returns></returns>
		private bool CheckArmorPlate( string apid, string mocode, string sscode, bool isUse )
		{
			if(apid.Length>0)
			{
				_apFacade = new ArmorPlateFacade( this.DataProvider );
				_armorPlate = _apFacade.GetArmorPlate( apid );
				if( _armorPlate == null )
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$Error_ArmorPlate_Not_Exist" ) );
					return false;
				}
				else if( string.Compare( ArmorPlateStatus.EndUsing, ( _armorPlate as ArmorPlate ).Status, true)==0 )
				{
					ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$Error_ArmorPlate_EndUsing" ) );
					return false;
				}

				_armorPlateInUse = _apFacade.GetArmorPlateInUseContol( apid );

				if(isUse)
				{
					if(_armorPlateInUse!=null)
					{
						ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$Error_ArmorPlate_HasBeen_Used" ) );
						return false;
					}
					
					if( _apFacade.GetArmorPlate2Item( (_mo as MO).ItemCode, apid ) == null )
					{
						ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$Error_ArmorPlate_ItemCode_NotMatch" ) );
						return false;
					}

					return true;
				}
				else
				{
					if( _armorPlateInUse !=null)
					{
						if( string.Compare( ( _armorPlateInUse as ArmorPlateContol ).UsedMOCode, mocode, true)==0
							&& string.Compare( ( _armorPlateInUse as ArmorPlateContol ).UsedSSCode, sscode, true)==0)
						{
							return true;
						}
						else if( string.Compare( ( _armorPlateInUse as ArmorPlateContol ).UsedMOCode, mocode, true)!=0 )
						{
							ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$Error_ArmorPlate_UsedBy_OtherMO" ) );
							return false;
						}
						else if( string.Compare( ( _armorPlateInUse as ArmorPlateContol ).UsedSSCode, sscode, true)!=0 )
						{
							ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$Error_ArmorPlate_UsedBy_OtherStepSequnce" ) );
							return false;
						}

						return false;
					}
					else
					{
						ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$Error_ArmorPlate_HasNotBeen_Used" ) );
						return false;
					}
				}
			}
			else
			{
				_armorPlateInUse = null;
				_armorPlate = null ;
				ApplicationRun.GetInfoForm().Add( new UserControl.Message( UserControl.MessageType.Error, "$Error_Please_Input_ArmorPlateId" ) );
				return false;
			}
		}

		private bool CheckTension( string tension ) 
		{
			decimal ten = decimal.Zero;
			try
			{
				ten = Convert.ToDecimal( tension );
			}
			catch
			{
				ten = -1;
			}
			
            return ten>0 ? true:false;
		}

        private bool CheckUnitPrint(string unitPrint)
        {
            int times = 0;
            try
            {
                times = Convert.ToInt32(unitPrint);
            }
            catch
            {
                times = -1;
            }

            return (times >= 1 && times <= 10) ? true : false;
        }

        private void FArmorPlate_Load(object sender, EventArgs e)
        {
            //this.InitPageLanguage();
            //this.InitGridLanguage(ultraGridMain);
        }
	}
}
