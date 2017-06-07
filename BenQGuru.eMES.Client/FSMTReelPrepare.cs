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
	/// FSMTReelPrepare 的摘要说明。
	/// </summary>
	public class FSMTReelPrepare : BaseForm
	{
		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.Panel panelBottom;
		private UserControl.UCLabelEdit txtMOCodeQuery;
		private UserControl.UCLabelEdit txtSSCodeQuery;
		private UserControl.UCLabelEdit txtReelNoQuery;
		private UserControl.UCButton btnQuery;
		private System.Windows.Forms.CheckBox chkMOPrepare;
		private UserControl.UCLabelEdit txtMOCode;
		private UserControl.UCLabelEdit txtSSCode;
		private UserControl.UCLabelEdit txtMaterialCodeQuery;
		private UserControl.UCLabelEdit txtReelNo;
		private UserControl.UCLabelEdit txtMaterialCode;
		private UserControl.UCLabelEdit txtQty;
		private UserControl.UCLabelEdit txtLotNo;
		private System.Windows.Forms.CheckBox chkReelValidity;
		private UserControl.UCLabelEdit txtDateCode;
		private System.Windows.Forms.CheckBox chkSpecial;
		private UserControl.UCLabelEdit txtMemo;
		private UserControl.UCButton btnAdd;
		private UserControl.UCButton btnDelete;
		private UserControl.UCButton btnExit;
		private System.Windows.Forms.Panel panelMiddle;
		private Infragistics.Win.UltraWinGrid.UltraGrid gridList;
		private UserControl.UCLabelEdit txtPCSUnit;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FSMTReelPrepare()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSMTReelPrepare));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MOCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StepSequenceCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaterialCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NeedQty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CollectedQty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReelNo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReelQty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LotNo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DateCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsSpecial");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Memo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaintainUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaintainDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaintainTime");
            this.panelTop = new System.Windows.Forms.Panel();
            this.txtPCSUnit = new UserControl.UCLabelEdit();
            this.btnQuery = new UserControl.UCButton();
            this.txtMaterialCodeQuery = new UserControl.UCLabelEdit();
            this.txtReelNoQuery = new UserControl.UCLabelEdit();
            this.txtSSCodeQuery = new UserControl.UCLabelEdit();
            this.txtMOCodeQuery = new UserControl.UCLabelEdit();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnExit = new UserControl.UCButton();
            this.btnDelete = new UserControl.UCButton();
            this.btnAdd = new UserControl.UCButton();
            this.txtMemo = new UserControl.UCLabelEdit();
            this.chkSpecial = new System.Windows.Forms.CheckBox();
            this.txtDateCode = new UserControl.UCLabelEdit();
            this.chkReelValidity = new System.Windows.Forms.CheckBox();
            this.txtLotNo = new UserControl.UCLabelEdit();
            this.txtQty = new UserControl.UCLabelEdit();
            this.txtMaterialCode = new UserControl.UCLabelEdit();
            this.txtReelNo = new UserControl.UCLabelEdit();
            this.txtSSCode = new UserControl.UCLabelEdit();
            this.txtMOCode = new UserControl.UCLabelEdit();
            this.chkMOPrepare = new System.Windows.Forms.CheckBox();
            this.panelMiddle = new System.Windows.Forms.Panel();
            this.gridList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.txtPCSUnit);
            this.panelTop.Controls.Add(this.btnQuery);
            this.panelTop.Controls.Add(this.txtMaterialCodeQuery);
            this.panelTop.Controls.Add(this.txtReelNoQuery);
            this.panelTop.Controls.Add(this.txtSSCodeQuery);
            this.panelTop.Controls.Add(this.txtMOCodeQuery);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(901, 67);
            this.panelTop.TabIndex = 0;
            // 
            // txtPCSUnit
            // 
            this.txtPCSUnit.AllowEditOnlyChecked = true;
            this.txtPCSUnit.Caption = "联板比例";
            this.txtPCSUnit.Checked = false;
            this.txtPCSUnit.EditType = UserControl.EditTypes.Integer;
            this.txtPCSUnit.Location = new System.Drawing.Point(474, 7);
            this.txtPCSUnit.MaxLength = 40;
            this.txtPCSUnit.Multiline = false;
            this.txtPCSUnit.Name = "txtPCSUnit";
            this.txtPCSUnit.PasswordChar = '\0';
            this.txtPCSUnit.ReadOnly = false;
            this.txtPCSUnit.ShowCheckBox = false;
            this.txtPCSUnit.Size = new System.Drawing.Size(93, 24);
            this.txtPCSUnit.TabIndex = 15;
            this.txtPCSUnit.TabNext = true;
            this.txtPCSUnit.Value = "1";
            this.txtPCSUnit.WidthType = UserControl.WidthTypes.Tiny;
            this.txtPCSUnit.XAlign = 525;
            this.txtPCSUnit.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPCSUnit_TxtboxKeyPress);
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.SystemColors.Control;
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.ButtonType = UserControl.ButtonTypes.None;
            this.btnQuery.Caption = "查询";
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.Location = new System.Drawing.Point(479, 37);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(88, 22);
            this.btnQuery.TabIndex = 14;
            this.btnQuery.TabStop = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtMaterialCodeQuery
            // 
            this.txtMaterialCodeQuery.AllowEditOnlyChecked = true;
            this.txtMaterialCodeQuery.Caption = "物料代码";
            this.txtMaterialCodeQuery.Checked = false;
            this.txtMaterialCodeQuery.EditType = UserControl.EditTypes.String;
            this.txtMaterialCodeQuery.Location = new System.Drawing.Point(254, 37);
            this.txtMaterialCodeQuery.MaxLength = 40;
            this.txtMaterialCodeQuery.Multiline = false;
            this.txtMaterialCodeQuery.Name = "txtMaterialCodeQuery";
            this.txtMaterialCodeQuery.PasswordChar = '\0';
            this.txtMaterialCodeQuery.ReadOnly = false;
            this.txtMaterialCodeQuery.ShowCheckBox = false;
            this.txtMaterialCodeQuery.Size = new System.Drawing.Size(162, 24);
            this.txtMaterialCodeQuery.TabIndex = 6;
            this.txtMaterialCodeQuery.TabNext = true;
            this.txtMaterialCodeQuery.Value = "";
            this.txtMaterialCodeQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtMaterialCodeQuery.XAlign = 305;
            this.txtMaterialCodeQuery.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaterialCodeQuery_TxtboxKeyPress);
            // 
            // txtReelNoQuery
            // 
            this.txtReelNoQuery.AllowEditOnlyChecked = true;
            this.txtReelNoQuery.Caption = "料卷编号";
            this.txtReelNoQuery.Checked = false;
            this.txtReelNoQuery.EditType = UserControl.EditTypes.String;
            this.txtReelNoQuery.Location = new System.Drawing.Point(25, 37);
            this.txtReelNoQuery.MaxLength = 40;
            this.txtReelNoQuery.Multiline = false;
            this.txtReelNoQuery.Name = "txtReelNoQuery";
            this.txtReelNoQuery.PasswordChar = '\0';
            this.txtReelNoQuery.ReadOnly = false;
            this.txtReelNoQuery.ShowCheckBox = false;
            this.txtReelNoQuery.Size = new System.Drawing.Size(162, 24);
            this.txtReelNoQuery.TabIndex = 5;
            this.txtReelNoQuery.TabNext = true;
            this.txtReelNoQuery.Value = "";
            this.txtReelNoQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtReelNoQuery.XAlign = 76;
            // 
            // txtSSCodeQuery
            // 
            this.txtSSCodeQuery.AllowEditOnlyChecked = true;
            this.txtSSCodeQuery.Caption = "产线代码";
            this.txtSSCodeQuery.Checked = false;
            this.txtSSCodeQuery.EditType = UserControl.EditTypes.String;
            this.txtSSCodeQuery.Location = new System.Drawing.Point(254, 7);
            this.txtSSCodeQuery.MaxLength = 40;
            this.txtSSCodeQuery.Multiline = false;
            this.txtSSCodeQuery.Name = "txtSSCodeQuery";
            this.txtSSCodeQuery.PasswordChar = '\0';
            this.txtSSCodeQuery.ReadOnly = false;
            this.txtSSCodeQuery.ShowCheckBox = false;
            this.txtSSCodeQuery.Size = new System.Drawing.Size(162, 24);
            this.txtSSCodeQuery.TabIndex = 4;
            this.txtSSCodeQuery.TabNext = true;
            this.txtSSCodeQuery.Value = "";
            this.txtSSCodeQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtSSCodeQuery.XAlign = 305;
            // 
            // txtMOCodeQuery
            // 
            this.txtMOCodeQuery.AllowEditOnlyChecked = true;
            this.txtMOCodeQuery.Caption = "工单代码";
            this.txtMOCodeQuery.Checked = false;
            this.txtMOCodeQuery.EditType = UserControl.EditTypes.String;
            this.txtMOCodeQuery.Location = new System.Drawing.Point(25, 7);
            this.txtMOCodeQuery.MaxLength = 40;
            this.txtMOCodeQuery.Multiline = false;
            this.txtMOCodeQuery.Name = "txtMOCodeQuery";
            this.txtMOCodeQuery.PasswordChar = '\0';
            this.txtMOCodeQuery.ReadOnly = false;
            this.txtMOCodeQuery.ShowCheckBox = false;
            this.txtMOCodeQuery.Size = new System.Drawing.Size(162, 24);
            this.txtMOCodeQuery.TabIndex = 3;
            this.txtMOCodeQuery.TabNext = true;
            this.txtMOCodeQuery.Value = "";
            this.txtMOCodeQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtMOCodeQuery.XAlign = 76;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnExit);
            this.panelBottom.Controls.Add(this.btnDelete);
            this.panelBottom.Controls.Add(this.btnAdd);
            this.panelBottom.Controls.Add(this.txtMemo);
            this.panelBottom.Controls.Add(this.chkSpecial);
            this.panelBottom.Controls.Add(this.txtDateCode);
            this.panelBottom.Controls.Add(this.chkReelValidity);
            this.panelBottom.Controls.Add(this.txtLotNo);
            this.panelBottom.Controls.Add(this.txtQty);
            this.panelBottom.Controls.Add(this.txtMaterialCode);
            this.panelBottom.Controls.Add(this.txtReelNo);
            this.panelBottom.Controls.Add(this.txtSSCode);
            this.panelBottom.Controls.Add(this.txtMOCode);
            this.panelBottom.Controls.Add(this.chkMOPrepare);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 541);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(901, 134);
            this.panelBottom.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(367, 104);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 17;
            this.btnExit.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.ButtonType = UserControl.ButtonTypes.None;
            this.btnDelete.Caption = "删除";
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Location = new System.Drawing.Point(253, 104);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(88, 22);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.TabStop = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.Control;
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.ButtonType = UserControl.ButtonTypes.None;
            this.btnAdd.Caption = "新增";
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Location = new System.Drawing.Point(140, 104);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 22);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.TabStop = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtMemo
            // 
            this.txtMemo.AllowEditOnlyChecked = true;
            this.txtMemo.Caption = "备注";
            this.txtMemo.Checked = false;
            this.txtMemo.EditType = UserControl.EditTypes.String;
            this.txtMemo.Location = new System.Drawing.Point(550, 74);
            this.txtMemo.MaxLength = 40;
            this.txtMemo.Multiline = false;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.PasswordChar = '\0';
            this.txtMemo.ReadOnly = false;
            this.txtMemo.ShowCheckBox = false;
            this.txtMemo.Size = new System.Drawing.Size(115, 24);
            this.txtMemo.TabIndex = 13;
            this.txtMemo.TabNext = false;
            this.txtMemo.Value = "";
            this.txtMemo.WidthType = UserControl.WidthTypes.Small;
            this.txtMemo.XAlign = 581;
            // 
            // chkSpecial
            // 
            this.chkSpecial.Location = new System.Drawing.Point(418, 74);
            this.chkSpecial.Name = "chkSpecial";
            this.chkSpecial.Size = new System.Drawing.Size(63, 24);
            this.chkSpecial.TabIndex = 12;
            this.chkSpecial.Text = "特采";
            // 
            // txtDateCode
            // 
            this.txtDateCode.AllowEditOnlyChecked = true;
            this.txtDateCode.Caption = "生产日期";
            this.txtDateCode.Checked = false;
            this.txtDateCode.EditType = UserControl.EditTypes.String;
            this.txtDateCode.Location = new System.Drawing.Point(201, 74);
            this.txtDateCode.MaxLength = 40;
            this.txtDateCode.Multiline = false;
            this.txtDateCode.Name = "txtDateCode";
            this.txtDateCode.PasswordChar = '\0';
            this.txtDateCode.ReadOnly = false;
            this.txtDateCode.ShowCheckBox = false;
            this.txtDateCode.Size = new System.Drawing.Size(134, 24);
            this.txtDateCode.TabIndex = 11;
            this.txtDateCode.TabNext = false;
            this.txtDateCode.Value = "";
            this.txtDateCode.WidthType = UserControl.WidthTypes.Small;
            this.txtDateCode.XAlign = 252;
            this.txtDateCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaterialCode_TxtboxKeyPress);
            // 
            // chkReelValidity
            // 
            this.chkReelValidity.Checked = true;
            this.chkReelValidity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReelValidity.Location = new System.Drawing.Point(78, 74);
            this.chkReelValidity.Name = "chkReelValidity";
            this.chkReelValidity.Size = new System.Drawing.Size(87, 24);
            this.chkReelValidity.TabIndex = 10;
            this.chkReelValidity.Text = "有效期检查";
            // 
            // txtLotNo
            // 
            this.txtLotNo.AllowEditOnlyChecked = true;
            this.txtLotNo.Caption = "生产批次";
            this.txtLotNo.Checked = false;
            this.txtLotNo.EditType = UserControl.EditTypes.String;
            this.txtLotNo.Location = new System.Drawing.Point(530, 44);
            this.txtLotNo.MaxLength = 40;
            this.txtLotNo.Multiline = false;
            this.txtLotNo.Name = "txtLotNo";
            this.txtLotNo.PasswordChar = '\0';
            this.txtLotNo.ReadOnly = false;
            this.txtLotNo.ShowCheckBox = false;
            this.txtLotNo.Size = new System.Drawing.Size(135, 24);
            this.txtLotNo.TabIndex = 9;
            this.txtLotNo.TabNext = false;
            this.txtLotNo.Value = "";
            this.txtLotNo.WidthType = UserControl.WidthTypes.Small;
            this.txtLotNo.XAlign = 581;
            this.txtLotNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaterialCode_TxtboxKeyPress);
            // 
            // txtQty
            // 
            this.txtQty.AllowEditOnlyChecked = true;
            this.txtQty.Caption = "数量";
            this.txtQty.Checked = false;
            this.txtQty.EditType = UserControl.EditTypes.String;
            this.txtQty.Location = new System.Drawing.Point(387, 45);
            this.txtQty.MaxLength = 40;
            this.txtQty.Multiline = false;
            this.txtQty.Name = "txtQty";
            this.txtQty.PasswordChar = '\0';
            this.txtQty.ReadOnly = false;
            this.txtQty.ShowCheckBox = false;
            this.txtQty.Size = new System.Drawing.Size(114, 24);
            this.txtQty.TabIndex = 8;
            this.txtQty.TabNext = false;
            this.txtQty.Value = "";
            this.txtQty.WidthType = UserControl.WidthTypes.Small;
            this.txtQty.XAlign = 418;
            this.txtQty.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaterialCode_TxtboxKeyPress);
            // 
            // txtMaterialCode
            // 
            this.txtMaterialCode.AllowEditOnlyChecked = true;
            this.txtMaterialCode.Caption = "物料代码";
            this.txtMaterialCode.Checked = false;
            this.txtMaterialCode.EditType = UserControl.EditTypes.String;
            this.txtMaterialCode.Location = new System.Drawing.Point(201, 44);
            this.txtMaterialCode.MaxLength = 40;
            this.txtMaterialCode.Multiline = false;
            this.txtMaterialCode.Name = "txtMaterialCode";
            this.txtMaterialCode.PasswordChar = '\0';
            this.txtMaterialCode.ReadOnly = false;
            this.txtMaterialCode.ShowCheckBox = false;
            this.txtMaterialCode.Size = new System.Drawing.Size(134, 24);
            this.txtMaterialCode.TabIndex = 7;
            this.txtMaterialCode.TabNext = false;
            this.txtMaterialCode.Value = "";
            this.txtMaterialCode.WidthType = UserControl.WidthTypes.Small;
            this.txtMaterialCode.XAlign = 252;
            this.txtMaterialCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaterialCode_TxtboxKeyPress);
            // 
            // txtReelNo
            // 
            this.txtReelNo.AllowEditOnlyChecked = true;
            this.txtReelNo.Caption = "料卷编号";
            this.txtReelNo.Checked = false;
            this.txtReelNo.EditType = UserControl.EditTypes.String;
            this.txtReelNo.Location = new System.Drawing.Point(25, 44);
            this.txtReelNo.MaxLength = 40;
            this.txtReelNo.Multiline = false;
            this.txtReelNo.Name = "txtReelNo";
            this.txtReelNo.PasswordChar = '\0';
            this.txtReelNo.ReadOnly = false;
            this.txtReelNo.ShowCheckBox = false;
            this.txtReelNo.Size = new System.Drawing.Size(135, 24);
            this.txtReelNo.TabIndex = 6;
            this.txtReelNo.TabNext = false;
            this.txtReelNo.Value = "";
            this.txtReelNo.WidthType = UserControl.WidthTypes.Small;
            this.txtReelNo.XAlign = 76;
            this.txtReelNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReelNo_TxtboxKeyPress);
            // 
            // txtSSCode
            // 
            this.txtSSCode.AllowEditOnlyChecked = true;
            this.txtSSCode.Caption = "产线代码";
            this.txtSSCode.Checked = false;
            this.txtSSCode.EditType = UserControl.EditTypes.String;
            this.txtSSCode.Location = new System.Drawing.Point(367, 15);
            this.txtSSCode.MaxLength = 40;
            this.txtSSCode.Multiline = false;
            this.txtSSCode.Name = "txtSSCode";
            this.txtSSCode.PasswordChar = '\0';
            this.txtSSCode.ReadOnly = true;
            this.txtSSCode.ShowCheckBox = false;
            this.txtSSCode.Size = new System.Drawing.Size(134, 24);
            this.txtSSCode.TabIndex = 5;
            this.txtSSCode.TabNext = true;
            this.txtSSCode.Value = "";
            this.txtSSCode.WidthType = UserControl.WidthTypes.Small;
            this.txtSSCode.XAlign = 418;
            // 
            // txtMOCode
            // 
            this.txtMOCode.AllowEditOnlyChecked = true;
            this.txtMOCode.Caption = "工单代码";
            this.txtMOCode.Checked = false;
            this.txtMOCode.EditType = UserControl.EditTypes.String;
            this.txtMOCode.Location = new System.Drawing.Point(201, 15);
            this.txtMOCode.MaxLength = 40;
            this.txtMOCode.Multiline = false;
            this.txtMOCode.Name = "txtMOCode";
            this.txtMOCode.PasswordChar = '\0';
            this.txtMOCode.ReadOnly = true;
            this.txtMOCode.ShowCheckBox = false;
            this.txtMOCode.Size = new System.Drawing.Size(134, 24);
            this.txtMOCode.TabIndex = 4;
            this.txtMOCode.TabNext = true;
            this.txtMOCode.Value = "";
            this.txtMOCode.WidthType = UserControl.WidthTypes.Small;
            this.txtMOCode.XAlign = 252;
            // 
            // chkMOPrepare
            // 
            this.chkMOPrepare.Checked = true;
            this.chkMOPrepare.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMOPrepare.Location = new System.Drawing.Point(78, 14);
            this.chkMOPrepare.Name = "chkMOPrepare";
            this.chkMOPrepare.Size = new System.Drawing.Size(82, 24);
            this.chkMOPrepare.TabIndex = 0;
            this.chkMOPrepare.Text = "工单备料";
            this.chkMOPrepare.CheckedChanged += new System.EventHandler(this.chkMOPrepare_CheckedChanged);
            // 
            // panelMiddle
            // 
            this.panelMiddle.Controls.Add(this.gridList);
            this.panelMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMiddle.Location = new System.Drawing.Point(0, 67);
            this.panelMiddle.Name = "panelMiddle";
            this.panelMiddle.Size = new System.Drawing.Size(901, 474);
            this.panelMiddle.TabIndex = 2;
            // 
            // gridList
            // 
            this.gridList.Cursor = System.Windows.Forms.Cursors.Default;
            ultraGridColumn1.Header.Caption = "工单代码";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 70;
            ultraGridColumn2.Header.Caption = "产线代码";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 63;
            ultraGridColumn3.Header.Caption = "物料代码";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 100;
            ultraGridColumn4.Header.Caption = "需求数量";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 92;
            ultraGridColumn5.Header.Caption = "领用数量";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 89;
            ultraGridColumn6.Header.Caption = "料卷编号";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 94;
            ultraGridColumn7.Header.Caption = "料卷数量";
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Width = 60;
            ultraGridColumn8.Header.Caption = "生产批号";
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Width = 88;
            ultraGridColumn9.Header.Caption = "生产日期";
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Width = 65;
            ultraGridColumn10.Header.Caption = "是否特采";
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn10.Width = 60;
            ultraGridColumn11.Header.Caption = "备注";
            ultraGridColumn11.Header.VisiblePosition = 10;
            ultraGridColumn11.Width = 79;
            ultraGridColumn12.Header.Caption = "维护人员";
            ultraGridColumn12.Header.VisiblePosition = 11;
            ultraGridColumn12.Width = 59;
            ultraGridColumn13.Header.Caption = "维护日期";
            ultraGridColumn13.Header.VisiblePosition = 12;
            ultraGridColumn13.Width = 58;
            ultraGridColumn14.Header.Caption = "维护时间";
            ultraGridColumn14.Header.VisiblePosition = 13;
            ultraGridColumn14.Width = 57;
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
            this.gridList.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.gridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridList.Location = new System.Drawing.Point(0, 0);
            this.gridList.Name = "gridList";
            this.gridList.Size = new System.Drawing.Size(901, 474);
            this.gridList.TabIndex = 0;
            // 
            // FSMTReelPrepare
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(901, 675);
            this.Controls.Add(this.panelMiddle);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "FSMTReelPrepare";
            this.Text = "工单备料";
            this.Load += new System.EventHandler(this.FSMTReelPrepare_Load);
            this.Closed += new System.EventHandler(this.FSMTReelPrepare_Closed);
            this.panelTop.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelMiddle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
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
		
		private DataTable tableSource = null;
		private void FSMTReelPrepare_Load(object sender, System.EventArgs e)
		{
			smtFacade = new SMTFacade(this.DataProvider);
			tableSource = new DataTable();
			tableSource.Columns.Add("MOCode");
			tableSource.Columns.Add("StepSequenceCode");
			tableSource.Columns.Add("MaterialCode");
			tableSource.Columns.Add("NeedQty", typeof(long));
			tableSource.Columns.Add("CollectedQty", typeof(long));
			tableSource.Columns.Add("ReelNo");
			tableSource.Columns.Add("ReelQty", typeof(long));
			tableSource.Columns.Add("LotNo");
			tableSource.Columns.Add("DateCode");
			tableSource.Columns.Add("IsSpecial");
			tableSource.Columns.Add("Memo");
			tableSource.Columns.Add("MaintainUser");
			tableSource.Columns.Add("MaintainDate");
			tableSource.Columns.Add("MaintainTime");
			
			tableSource.DefaultView.Sort = "MaterialCode,ReelNo";
			gridList.DataSource = tableSource.DefaultView;
			gridList.DisplayLayout.Bands[0].Columns["NeedQty"].Format = "#,0";
			gridList.DisplayLayout.Bands[0].Columns["NeedQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
			gridList.DisplayLayout.Bands[0].Columns["CollectedQty"].Format = "#,0";
			gridList.DisplayLayout.Bands[0].Columns["CollectedQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
			gridList.DisplayLayout.Bands[0].Columns["ReelQty"].Format = "#,#";
			gridList.DisplayLayout.Bands[0].Columns["ReelQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
			
			UserControl.UIStyleBuilder.FormUI(this);
			UserControl.UIStyleBuilder.GridUI(gridList);

            //this.InitPageLanguage();
            //this.InitGridLanguage(gridList);
		}

		private void FSMTReelPrepare_Closed(object sender, System.EventArgs e)
		{
			this.CloseConnection();
		}

		private BenQGuru.eMES.Domain.MOModel.MO checkedMOCode = null;
		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			if (this.txtMOCodeQuery.Value.Trim() == string.Empty)
			{
				Application.DoEvents();
				this.txtMOCodeQuery.TextFocus(false, true);
				return;
			}
			//Icyer,2007/01/15 修改	减少Open/Close的次数
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			try
			{
				this.txtMOCodeQuery.Value = this.txtMOCodeQuery.Value.Trim().ToUpper();
				this.txtSSCodeQuery.Value = this.txtSSCodeQuery.Value.Trim().ToUpper();
				// 查询工单代码
				BenQGuru.eMES.MOModel.MOFacade moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
				MO mo = (MO)moFacade.GetMO(this.txtMOCodeQuery.Value.Trim().ToUpper());
				if (mo == null)
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
					Application.DoEvents();
					this.txtMOCodeQuery.TextFocus(true, true);
					return;
				}
				checkedMOCode = mo;
				if (this.txtSSCodeQuery.Value.Trim() == string.Empty)
				{
					Application.DoEvents();
					this.txtSSCodeQuery.TextFocus(false, true);
					return;
				}
				try
				{
					this.txtPCSUnit.Value = int.Parse(this.txtPCSUnit.Value).ToString();
				}
				catch
				{
					this.txtPCSUnit.Value = "1";
				}
				// 查询料站表资料
				object[] objsSmt = smtFacade.QuerySMTFeederMaterialGroupByMaterial(mo.ItemCode, this.txtSSCodeQuery.Value);
				Hashtable htMaterialQty = new Hashtable();
				if (objsSmt != null)
				{
					for (int i = 0; i < objsSmt.Length; i++)
					{
						SMTFeederMaterial smtMaterial = (SMTFeederMaterial)objsSmt[i];
						htMaterialQty.Add(smtMaterial.MaterialCode, smtMaterial.Qty * mo.MOPlanQty / int.Parse(this.txtPCSUnit.Value));
					}
				}
				// 查询料卷信息
				object[] objsReel = smtFacade.QueryReelByMO(this.txtMOCodeQuery.Value, this.txtSSCodeQuery.Value, this.txtReelNoQuery.Value.Trim().ToUpper(), this.txtMaterialCodeQuery.Value.Trim().ToUpper());
				// 显示
				tableSource.Rows.Clear();
				ArrayList listExistMaterial = new ArrayList();
				ArrayList listExtendReel = new ArrayList();
				if (objsReel != null)
				{
					for (int i = 0; i < objsReel.Length; i++)
					{
						Reel reel = (Reel)objsReel[i];
						if (htMaterialQty.ContainsKey(reel.PartNo) == true)
						{
							AddToSourceFromReel(reel, Convert.ToInt64(htMaterialQty[reel.PartNo]));
							listExistMaterial.Add(reel.PartNo);
						}
						else
						{
							listExtendReel.Add(reel);
						}
					}
				}
				// 显示未领用的料卷
				foreach (object materialCode in htMaterialQty.Keys)
				{
					if (listExistMaterial.Contains(materialCode) == false)
					{
						Reel reel = new Reel();
						reel.MOCode = this.txtMOCodeQuery.Value;
						reel.StepSequenceCode = this.txtSSCodeQuery.Value;
						reel.PartNo = materialCode.ToString();
						AddToSourceFromReel(reel, Convert.ToInt64(htMaterialQty[materialCode]));
					}
				}
				// 显示多余领用的料卷
				foreach (Reel reel in listExtendReel)
				{
					AddToSourceFromReel(reel, 0);
				}
				this.txtMOCode.Value = this.txtMOCodeQuery.Value;
				this.txtSSCode.Value = this.txtSSCodeQuery.Value;
				Application.DoEvents();
				this.txtReelNo.TextFocus(false, true);
			}
			catch {}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
			}
		}
		private void AddToSourceFromReel(Reel reel, long needQty)
		{
			DataRow[] rows = tableSource.Select("MaterialCode='" + reel.PartNo + "'");
			decimal iCount = 0;
			bool bFirst = false;
			if (rows.Length == 0)
			{
				iCount = reel.Qty - reel.UsedQty;
				if (reel.ReelNo == null || reel.ReelNo == string.Empty)
					iCount = 0;
			}
			else
			{
				if (rows[0]["ReelNo"].ToString() == string.Empty)
				{
					bFirst = true;
					iCount = reel.Qty - reel.UsedQty;
				}
				else
				{
					iCount = Convert.ToInt64(rows[0]["CollectedQty"]) + (reel.Qty - reel.UsedQty);
					for (int i = 0; i < rows.Length; i++)
					{
						rows[i]["CollectedQty"] = iCount;
					}
				}
			}
			DataRow row = null;
			if (bFirst == true)
				row = rows[0];
			else
				row = tableSource.NewRow();
			row["MOCode"] = this.txtMOCodeQuery.Value;
			row["StepSequenceCode"] = this.txtSSCodeQuery.Value;
			if (reel.MOCode == string.Empty && reel.StepSequenceCode == string.Empty)
			{
				row["MOCode"] = string.Empty;
				row["StepSequenceCode"] = string.Empty;
			}
			row["MaterialCode"] = reel.PartNo;
			if (needQty != int.MinValue)
			{
				row["NeedQty"] = needQty;
			}
			else
			{
				if (rows.Length > 0)
					row["NeedQty"] = rows[0]["NeedQty"];
				else
					row["NeedQty"] = 0;
			}
			row["CollectedQty"] = iCount;
			row["ReelNo"] = reel.ReelNo;
			row["ReelQty"] = reel.Qty - reel.UsedQty;
			row["LotNo"] = reel.LotNo;
			row["DateCode"] = reel.DateCode;
			row["IsSpecial"] = FormatHelper.StringToBoolean(reel.IsSpecial).ToString();
			row["Memo"] = reel.Memo;
			row["MaintainUser"] = reel.MaintainUser;
			row["MaintainDate"] = FormatHelper.ToDateString(reel.MaintainDate);
			row["MaintainTime"] = FormatHelper.ToTimeString(reel.MaintainTime);
			if (bFirst == false)
				tableSource.Rows.Add(row);
		}

		private void txtMaterialCodeQuery_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				//if (((UCLabelEdit)sender).Name == "txtMaterialCodeQuery")
					this.btnQuery_Click(null, null);
			}
		}

		private string existReelNo = string.Empty;
		private bool checkedDateCodeReelNo = true;
		private string checkedReelNo = string.Empty;
		private void txtReelNo_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				if (this.txtReelNo.Value.Trim() == string.Empty)
				{
					Application.DoEvents();
					this.txtReelNo.TextFocus(true, true);
					return;
				}
				this.txtReelNo.Value = this.txtReelNo.Value.Trim().ToUpper();
				Reel reel = (Reel)smtFacade.GetReel(this.txtReelNo.Value);
				if (reel != null)
				{
					// 有效期检查
					bool bCheck = true;
					if (this.chkReelValidity.Checked == true)
					{
						Messages msgTmp = smtFacade.CheckReelValidity(reel.PartNo, reel.DateCode);
						bCheck = msgTmp.IsSuccess();
						checkedDateCodeReelNo = bCheck;
						checkedReelNo = reel.ReelNo;
						if (bCheck == false && this.chkSpecial.Checked == false)
						{
							string strMsg = msgTmp.OutPut();
							strMsg = UserControl.MutiLanguages.ParserString(strMsg);
							strMsg = string.Format(strMsg, reel.ReelNo, reel.DateCode);
							ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, strMsg));
							Application.DoEvents();
							if (reel.DateCode != string.Empty)
							{
								this.txtReelNo.TextFocus(true, true);
								reel = new Reel();
							}
							else
							{
								this.txtDateCode.TextFocus(true, true);
							}
						}
					}
					existReelNo = reel.ReelNo;
					this.txtMaterialCode.Value = reel.PartNo;
					this.txtQty.Value = reel.Qty.ToString();
					this.txtLotNo.Value = reel.LotNo;
					this.txtDateCode.Value = reel.DateCode;
					if (bCheck == true)
					{
						this.btnAdd_Click(null, null);
						return;
					}
				}
				else
				{
					reel = new Reel();
				}
				this.txtMaterialCode.Value = reel.PartNo;
				this.txtQty.Value = reel.Qty.ToString();
				this.txtLotNo.Value = reel.LotNo;
				this.txtDateCode.Value = reel.DateCode;
				// 设置焦点
				Application.DoEvents();
				if (this.txtReelNo.Value == string.Empty)
					this.txtReelNo.TextFocus(false, true);
				else if (this.txtMaterialCode.Value == string.Empty)
					this.txtMaterialCode.TextFocus(false, true);
				else if (this.txtLotNo.Value == string.Empty)
					this.txtLotNo.TextFocus(false, true);
				else if (this.txtDateCode.Value == string.Empty)
					this.txtDateCode.TextFocus(false, true);
				else
				{
					// 输入内容完整，则保存
					btnAdd_Click(null, null);
				}
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if (this.chkMOPrepare.Checked == true && (this.txtMOCode.Value == string.Empty || this.txtSSCode.Value == string.Empty))
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$SMT_Prepare_Reel_Please_Input_MO_SSCode"));
				Application.DoEvents();
				if (this.txtMOCode.Value == string.Empty)
					this.txtMOCodeQuery.TextFocus(false, true);
				else if (this.txtSSCode.Value == string.Empty)
					this.txtSSCodeQuery.TextFocus(false, true);
				return;
			}
			if (this.chkMOPrepare.Checked == true)
			{
				if (this.checkedMOCode == null || this.checkedMOCode.MOCode != this.txtMOCode.Value)
				{
					// 查询工单代码
					BenQGuru.eMES.MOModel.MOFacade moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
					MO mo = (MO)moFacade.GetMO(this.txtMOCode.Value);
					if (mo == null)
					{
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
						Application.DoEvents();
						this.txtMOCodeQuery.TextFocus(true, true);
						return;
					}
					checkedMOCode = mo;
				}
			}
			if (this.txtReelNo.Value.Trim() == string.Empty)
			{
				Application.DoEvents();
				this.txtReelNo.TextFocus(true, true);
				return;
			}
			//Icyer,2007/01/15 修改	减少Open/Close的次数
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			try
			{
				this.txtReelNo.Value = this.txtReelNo.Value.Trim().ToUpper();
				if (this.txtReelNo.Value != this.existReelNo)
				{
					Reel reel = (Reel)smtFacade.GetReel(this.txtReelNo.Value);
					if (reel != null)
					{
						// 有效期检查
						if (this.chkReelValidity.Checked == true)
						{
							if (reel.DateCode == string.Empty && this.txtDateCode.Value.Trim() != string.Empty)
								reel.DateCode = this.txtDateCode.Value.Trim().ToUpper();
							Messages msgTmp = smtFacade.CheckReelValidity(reel.PartNo, reel.DateCode);
							bool bCheck = msgTmp.IsSuccess();
							checkedDateCodeReelNo = bCheck;
							checkedReelNo = reel.ReelNo;
							if (bCheck == false && this.chkSpecial.Checked == false)
							{
								string strMsg = UserControl.MutiLanguages.ParserString(msgTmp.OutPut());
								strMsg = string.Format(strMsg, reel.ReelNo, reel.DateCode);
								ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, strMsg));
								Application.DoEvents();
								if (reel.DateCode != string.Empty)
								{
									this.txtMaterialCode.Value = string.Empty;
									this.txtQty.Value = string.Empty;
									this.txtLotNo.Value = string.Empty;
									this.txtDateCode.Value = string.Empty;
									this.txtReelNo.TextFocus(true, true);
								}
								else
								{
									this.txtDateCode.TextFocus(true, true);
								}
								return;
							}
						}
						existReelNo = reel.ReelNo;
						this.txtMaterialCode.Value = reel.PartNo;
						this.txtQty.Value = reel.Qty.ToString();
						this.txtLotNo.Value = reel.LotNo;
						this.txtDateCode.Value = reel.DateCode;
					}
				}
				if ((this.txtReelNo.Value != this.checkedReelNo && this.chkReelValidity.Checked == true) || 
					(this.txtReelNo.Value == this.checkedReelNo && checkedDateCodeReelNo == false && this.chkReelValidity.Checked == true))
				{
					Messages msgTmp = smtFacade.CheckReelValidity(this.txtMaterialCode.Value, this.txtDateCode.Value);
					bool bCheck = msgTmp.IsSuccess();
					checkedDateCodeReelNo = bCheck;
					checkedReelNo = this.txtReelNo.Value;
					if (bCheck == false && this.chkSpecial.Checked == false)
					{
						string strMsg = UserControl.MutiLanguages.ParserString(msgTmp.OutPut());
						strMsg = string.Format(strMsg, this.txtReelNo.Value, this.txtDateCode.Value);
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, strMsg));
						Application.DoEvents();
						if (this.txtDateCode.Value != string.Empty)
						{
							this.txtMaterialCode.Value = string.Empty;
							this.txtQty.Value = string.Empty;
							this.txtLotNo.Value = string.Empty;
							this.txtDateCode.Value = string.Empty;
							this.txtReelNo.TextFocus(true, true);
						}
						else
						{
							this.txtDateCode.TextFocus(true, true);
						}
						return;
					}
				}
				if (this.chkReelValidity.Checked == true && this.txtDateCode.Value == string.Empty)
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$SMT_Prepare_Reel_Please_Input_Date"));
					Application.DoEvents();
					this.txtDateCode.TextFocus(false, true);
					return;
				}
				if (this.checkedDateCodeReelNo == false && this.chkSpecial.Checked == true &&
					this.chkSpecial.Checked == true && this.txtMemo.Value == string.Empty)
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$SMT_Prepare_Reel_Please_Input_SpecialMemo"));
					Application.DoEvents();
					this.txtMemo.TextFocus(false, true);
					return;
				}
				// 添加
				Reel reelObj = new Reel();
				reelObj = (Reel)smtFacade.GetReel(this.txtReelNo.Value);
				if (reelObj != null)
				{
					if (this.chkMOPrepare.Checked == false)
					{
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$SMT_Prepare_Reel_Exist_Already"));
						this.txtMaterialCode.Value = string.Empty;
						this.txtQty.Value = string.Empty;
						this.txtLotNo.Value = string.Empty;
						this.txtDateCode.Value = string.Empty;
						Application.DoEvents();
						this.txtReelNo.TextFocus(true, true);
						return;
					}
					if (this.chkReelValidity.Checked == true && reelObj.DateCode == string.Empty && this.txtDateCode.Value.Trim() != string.Empty)
						reelObj.DateCode = this.txtDateCode.Value.Trim().ToUpper();
				}
				else
				{
					reelObj = new Reel();
					if (this.txtReelNo.Value.Trim() == string.Empty)
					{
						this.txtReelNo.TextFocus(true, true);
						return;
					}
					if (this.txtMaterialCode.Value.Trim() == string.Empty)
					{
						this.txtMaterialCode.TextFocus(true, true);
						return;
					}
					reelObj.ReelNo = this.txtReelNo.Value;
					reelObj.PartNo = this.txtMaterialCode.Value;
					try
					{
						reelObj.Qty = decimal.Parse(this.txtQty.Value);
					}
					catch
					{
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$SMT_Prepare_Reel_Qty_FormatError"));
						Application.DoEvents();
						this.txtQty.TextFocus(true, true);
						return;
					}
					reelObj.LotNo = this.txtLotNo.Value;
					reelObj.DateCode = this.txtDateCode.Value;
				}
				reelObj.IsSpecial = FormatHelper.BooleanToString(false);
				if (this.chkReelValidity.Checked == true)
				{
					if (checkedDateCodeReelNo == false && this.chkSpecial.Checked == true)
					{
						reelObj.IsSpecial = FormatHelper.BooleanToString(true);
						reelObj.Memo = this.txtMemo.Value;
					}
				}
				if (this.chkMOPrepare.Checked == true)
				{
					reelObj.MOCode = this.txtMOCode.Value;
					reelObj.StepSequenceCode = this.txtSSCode.Value;
					reelObj.UsedFlag = FormatHelper.BooleanToString(true);
				}
				reelObj.MaintainUser = Service.ApplicationService.Current().UserCode;

				//Laws Lu,2006/11/13 uniform system collect date
				DBDateTime dbDateTime;
			
				dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
				

				reelObj.MaintainDate = dbDateTime.DBDate;
				reelObj.MaintainTime = dbDateTime.DBTime;
				// 检查物料是否匹配
				DataRow[] rowsTmp = tableSource.Select("MaterialCode='" + reelObj.PartNo + "'");
				if (rowsTmp == null || rowsTmp.Length == 0)
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$SMT_Prepare_Reel_Material_Not_Match"));
					Application.DoEvents();
					this.txtReelNo.Value = string.Empty;
					this.txtMaterialCode.Value = string.Empty;
					this.txtQty.Value = string.Empty;
					this.txtLotNo.Value = string.Empty;
					this.txtDateCode.Value = string.Empty;
					this.txtReelNo.TextFocus(false, true);
					return;
				}
				// 料卷领用到工单
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
				this.DataProvider.BeginTransaction();
				Messages messages = new Messages();
				try
				{
					messages.AddMessages(smtFacade.UpdateReelToMO(reelObj, this.txtReelNo.Value != this.existReelNo));
					if (messages.IsSuccess() == true)
					{
						this.DataProvider.CommitTransaction();
						messages.Add(new UserControl.Message(MessageType.Success, "$SMT_Prepare_Reel_Success"));
					}
					else
					{
						this.DataProvider.RollbackTransaction();
					}
				}
				catch (Exception ex)
				{
					messages.Add(new UserControl.Message(ex));
					this.DataProvider.RollbackTransaction();
				
					try
					{
						string strErrorInfo = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "\t" + ex.Message + "\t" + ex.Source;
						strErrorInfo += "\t" + ex.StackTrace;
						UserControl.FileLog.FileLogOut("Client.log", strErrorInfo);
					}
					catch
					{}
				}
				finally
				{
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				}
				ApplicationRun.GetInfoForm().Add(messages);
			
				// 添加到Grid
				if (messages.IsSuccess() == true && this.chkMOPrepare.Checked == true)
				{
					AddToSourceFromReel(reelObj, int.MinValue);
				}

				this.txtReelNo.Value = string.Empty;
				this.txtMaterialCode.Value = string.Empty;
				this.txtQty.Value = string.Empty;
				this.txtLotNo.Value = string.Empty;
				this.txtDateCode.Value = string.Empty;
				this.checkedDateCodeReelNo = false;
				this.checkedReelNo = string.Empty;
				this.existReelNo = string.Empty;
				Application.DoEvents();
				this.txtReelNo.TextFocus(false, true);
			}
			catch {}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			/*
			string strReelNo = gridList.Selected.Rows[0].Cells["ReelNo"].Text;
			if (strReelNo == string.Empty)
				return;
			string strMsg = UserControl.MutiLanguages.ParserString("$SMT_Prepare_Reel_Return_Confirm");
			if (MessageBox.Show(strMsg + " [" + strReelNo + "]", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;
			*/
			string strMsg = UserControl.MutiLanguages.ParserString("$Please_Input_ReelNo");
			FDialogInput finput = new FDialogInput(strMsg);
			DialogResult dialogResult = finput.ShowDialog();
			if (dialogResult != DialogResult.OK)
			{
				return;
			}
			string strReelNo = finput.InputText.Trim().ToUpper();
			finput.Close();
			DataRow[] rowsTmp = tableSource.Select("ReelNo='" + strReelNo + "'");
			if (rowsTmp == null || rowsTmp.Length == 0)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$SMT_CheckReel_Not_In_List"));
				Application.DoEvents();
				this.txtReelNo.TextFocus(false, true);
				return;
			}
			string strMaterialCode = rowsTmp[0]["MaterialCode"].ToString();
				
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			this.DataProvider.BeginTransaction();
			Messages messages = new Messages();
			try
			{
				messages.AddMessages(smtFacade.DeleteReelFromMO(strReelNo, Service.ApplicationService.Current().UserCode));
				if (messages.IsSuccess() == true)
				{
					this.DataProvider.CommitTransaction();
					messages.Add(new UserControl.Message(MessageType.Success, "$SMT_Prepare_Reel_Return_Success"));
				}
				else
				{
					this.DataProvider.RollbackTransaction();
				}
			}
			catch (Exception ex)
			{
				messages.Add(new UserControl.Message(ex));
				this.DataProvider.RollbackTransaction();
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
			}
			ApplicationRun.GetInfoForm().Add(messages);

			if (messages.IsSuccess() == true)
			{
				DataRow[] rows = tableSource.Select("MaterialCode='" + strMaterialCode + "'");
				if (rows.Length == 1)
				{
					if (Convert.ToInt64(rows[0]["NeedQty"]) > 0)
					{
						rows[0]["CollectedQty"] = 0;
						rows[0]["ReelNo"] = string.Empty;
						rows[0]["ReelQty"] = 0;
						rows[0]["LotNo"] = string.Empty;
						rows[0]["DateCode"] = string.Empty;
						rows[0]["IsSpecial"] = string.Empty;
						rows[0]["Memo"] = string.Empty;
						rows[0]["MaintainUser"] = Service.ApplicationService.Current().UserCode;

						//Laws Lu,2006/11/13 uniform system collect date
						DBDateTime dbDateTime;
						
						dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
						

						rows[0]["MaintainDate"] = FormatHelper.ToDateString(dbDateTime.DBDate);
						rows[0]["MaintainTime"] = FormatHelper.ToTimeString(dbDateTime.DBTime);
					}
					else
					{
						rows[0].Delete();
					}
				}
				else
				{
					DataRow[] rowsReel = tableSource.Select("ReelNo='" + strReelNo + "'");
					int iReelLeftQty = 0;
					if (rowsReel.Length == 1)
					{
						iReelLeftQty = Convert.ToInt32(rowsReel[0]["ReelQty"]);
					}
					for (int i = 0; i < rows.Length; i++)
					{
						rows[i]["CollectedQty"] = Convert.ToInt64(rows[i]["CollectedQty"]) - iReelLeftQty;
					}
					if (rowsReel.Length == 1)
					{
						rowsReel[0].Delete();
					}
				}
			}
			Application.DoEvents();
			this.txtReelNo.TextFocus(false, true);
		}

		private void txtMaterialCode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				if (((Control)sender).Parent == this.txtMaterialCode)
				{
					if (this.txtMaterialCode.Value.Length > 13)
					{
						string strTmp = this.txtMaterialCode.Value.Substring(13);
						strTmp = strTmp.TrimStart('0');
						if (strTmp.Length > 0)
						{
							try
							{
								this.txtQty.Value = int.Parse(strTmp).ToString();
							}
							catch{}
						}
						this.txtMaterialCode.Value = this.txtMaterialCode.Value.Substring(0, 12);
					}
				}
				if (this.txtMaterialCode.Value.Trim() == string.Empty)
					this.txtMaterialCode.TextFocus(false, true);
				else if (this.txtQty.Value == string.Empty || this.txtQty.Value == "0")
					this.txtQty.TextFocus(false, true);
				else if (this.txtLotNo.Value == string.Empty)
					this.txtLotNo.TextFocus(false, true);
				else if (this.txtDateCode.Value == string.Empty)
					this.txtDateCode.TextFocus(false, true);
				else
				{
					this.btnAdd_Click(null, null);
				}
			}
		}

		private void chkMOPrepare_CheckedChanged(object sender, System.EventArgs e)
		{
			if (this.chkMOPrepare.Checked == true)
			{
				this.txtMOCode.Value = this.txtMOCodeQuery.Value.Trim().ToUpper();
				this.txtSSCode.Value = this.txtSSCodeQuery.Value.Trim().ToUpper();
				this.chkReelValidity.Enabled = true;
				this.chkSpecial.Enabled = true;
				this.txtMemo.Enabled = true;
			}
			else
			{
				this.txtMOCode.Value = string.Empty;
				this.txtSSCode.Value = string.Empty;
				this.chkReelValidity.Checked = false;
				this.chkReelValidity.Enabled = false;
				this.chkSpecial.Checked = false;
				this.chkSpecial.Enabled = false;
				this.txtMemo.Value = string.Empty;
				this.txtMemo.Enabled = false;
			}
		}

		private int iPrevPCSUnit = 1;
		private void txtPCSUnit_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				int iPCSUnit = 1;
				try
				{
					iPCSUnit = int.Parse(this.txtPCSUnit.Value);
				}
				catch
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Format_Error"));
					Application.DoEvents();
					this.txtPCSUnit.TextFocus(true, true);
					return;
				}
				if (iPCSUnit != iPrevPCSUnit)
				{
					if (tableSource.Rows.Count > 0)
					{
						for (int i = 0; i < tableSource.Rows.Count; i++)
						{
							tableSource.Rows[i]["NeedQty"] = Convert.ToInt64(tableSource.Rows[i]["NeedQty"]) * iPrevPCSUnit / iPCSUnit;
						}
					}
				}
				iPrevPCSUnit = iPCSUnit;
			}
		}
	}
}
