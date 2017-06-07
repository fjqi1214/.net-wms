using System;
using System.Windows.Forms;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Domain.BaseSetting;
using Infragistics.Win.UltraWinGrid;
using System.Drawing;
using System.Data;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.DataCollect;
using UserControl;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Domain.DataCollect;

namespace BenQGuru.eMES.Client
{
    public partial class FDisToLine : BaseForm
    {
        #region designer

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FDisToLine));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnQuery = new UserControl.UCButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFinished = new System.Windows.Forms.CheckBox();
            this.cbShortDis = new System.Windows.Forms.CheckBox();
            this.cbERDis = new System.Windows.Forms.CheckBox();
            this.cbWaitDis = new System.Windows.Forms.CheckBox();
            this.cbNormal = new System.Windows.Forms.CheckBox();
            this.cmbSSCodeQuery = new UserControl.UCLabelCombox();
            this.cmbSegCodeQuery = new UserControl.UCLabelCombox();
            this.txtRefreshTime = new UserControl.UCLabelEdit();
            this.txtMCodeQuery = new UserControl.UCLabelEdit();
            this.txtMoQuery = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReDis = new UserControl.UCButton();
            this.btnCancel = new UserControl.UCButton();
            this.btnDis = new UserControl.UCButton();
            this.lblDIsStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDisQty = new UserControl.UCLabelEdit();
            this.txtLastDisQty = new UserControl.UCLabelEdit();
            this.txtMNameEdit = new UserControl.UCLabelEdit();
            this.txtProductionTime = new UserControl.UCLabelEdit();
            this.txtMssLeftQty = new UserControl.UCLabelEdit();
            this.txtMssDisQty = new UserControl.UCLabelEdit();
            this.txtMCodeEdit = new UserControl.UCLabelEdit();
            this.txtMoEdit = new UserControl.UCLabelEdit();
            this.cmbSSCodeEdit = new UserControl.UCLabelCombox();
            this.cmbSegCodeEdit = new UserControl.UCLabelCombox();
            this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbFinished);
            this.groupBox1.Controls.Add(this.cbShortDis);
            this.groupBox1.Controls.Add(this.cbERDis);
            this.groupBox1.Controls.Add(this.cbWaitDis);
            this.groupBox1.Controls.Add(this.cbNormal);
            this.groupBox1.Controls.Add(this.cmbSSCodeQuery);
            this.groupBox1.Controls.Add(this.cmbSegCodeQuery);
            this.groupBox1.Controls.Add(this.txtRefreshTime);
            this.groupBox1.Controls.Add(this.txtMCodeQuery);
            this.groupBox1.Controls.Add(this.txtMoQuery);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(951, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.SystemColors.Control;
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.ButtonType = UserControl.ButtonTypes.None;
            this.btnQuery.Caption = "查询";
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.Location = new System.Drawing.Point(758, 48);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(88, 22);
            this.btnQuery.TabIndex = 11;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "配送预警";
            // 
            // cbFinished
            // 
            this.cbFinished.AutoSize = true;
            this.cbFinished.Location = new System.Drawing.Point(342, 51);
            this.cbFinished.Name = "cbFinished";
            this.cbFinished.Size = new System.Drawing.Size(72, 16);
            this.cbFinished.TabIndex = 9;
            this.cbFinished.Text = "配送完成";
            this.cbFinished.UseVisualStyleBackColor = true;
            // 
            // cbShortDis
            // 
            this.cbShortDis.AutoSize = true;
            this.cbShortDis.ForeColor = System.Drawing.Color.Red;
            this.cbShortDis.Location = new System.Drawing.Point(272, 51);
            this.cbShortDis.Name = "cbShortDis";
            this.cbShortDis.Size = new System.Drawing.Size(60, 16);
            this.cbShortDis.TabIndex = 8;
            this.cbShortDis.Text = "缺料中";
            this.cbShortDis.UseVisualStyleBackColor = true;
            // 
            // cbERDis
            // 
            this.cbERDis.AutoSize = true;
            this.cbERDis.ForeColor = System.Drawing.Color.DarkOrange;
            this.cbERDis.Location = new System.Drawing.Point(193, 51);
            this.cbERDis.Name = "cbERDis";
            this.cbERDis.Size = new System.Drawing.Size(72, 16);
            this.cbERDis.TabIndex = 7;
            this.cbERDis.Text = "紧急配送";
            this.cbERDis.UseVisualStyleBackColor = true;
            // 
            // cbWaitDis
            // 
            this.cbWaitDis.AutoSize = true;
            this.cbWaitDis.ForeColor = System.Drawing.Color.Blue;
            this.cbWaitDis.Location = new System.Drawing.Point(126, 51);
            this.cbWaitDis.Name = "cbWaitDis";
            this.cbWaitDis.Size = new System.Drawing.Size(60, 16);
            this.cbWaitDis.TabIndex = 6;
            this.cbWaitDis.Text = "待配送";
            this.cbWaitDis.UseVisualStyleBackColor = true;
            // 
            // cbNormal
            // 
            this.cbNormal.AutoSize = true;
            this.cbNormal.ForeColor = System.Drawing.Color.Green;
            this.cbNormal.Location = new System.Drawing.Point(71, 51);
            this.cbNormal.Name = "cbNormal";
            this.cbNormal.Size = new System.Drawing.Size(48, 16);
            this.cbNormal.TabIndex = 5;
            this.cbNormal.Text = "正常";
            this.cbNormal.UseVisualStyleBackColor = true;
            // 
            // cmbSSCodeQuery
            // 
            this.cmbSSCodeQuery.AllowEditOnlyChecked = true;
            this.cmbSSCodeQuery.Caption = "产线代码";
            this.cmbSSCodeQuery.Checked = false;
            this.cmbSSCodeQuery.Location = new System.Drawing.Point(654, 17);
            this.cmbSSCodeQuery.Name = "cmbSSCodeQuery";
            this.cmbSSCodeQuery.SelectedIndex = -1;
            this.cmbSSCodeQuery.ShowCheckBox = false;
            this.cmbSSCodeQuery.Size = new System.Drawing.Size(194, 20);
            this.cmbSSCodeQuery.TabIndex = 4;
            this.cmbSSCodeQuery.WidthType = UserControl.WidthTypes.Normal;
            this.cmbSSCodeQuery.XAlign = 715;
            // 
            // cmbSegCodeQuery
            // 
            this.cmbSegCodeQuery.AllowEditOnlyChecked = true;
            this.cmbSegCodeQuery.Caption = "车间代码";
            this.cmbSegCodeQuery.Checked = false;
            this.cmbSegCodeQuery.Location = new System.Drawing.Point(439, 17);
            this.cmbSegCodeQuery.Name = "cmbSegCodeQuery";
            this.cmbSegCodeQuery.SelectedIndex = -1;
            this.cmbSegCodeQuery.ShowCheckBox = false;
            this.cmbSegCodeQuery.Size = new System.Drawing.Size(194, 20);
            this.cmbSegCodeQuery.TabIndex = 3;
            this.cmbSegCodeQuery.WidthType = UserControl.WidthTypes.Normal;
            this.cmbSegCodeQuery.XAlign = 500;
            this.cmbSegCodeQuery.SelectedIndexChanged += new System.EventHandler(this.cmbSegCodeQuery_SelectedIndexChanged);
            // 
            // txtRefreshTime
            // 
            this.txtRefreshTime.AllowEditOnlyChecked = true;
            this.txtRefreshTime.AutoSelectAll = false;
            this.txtRefreshTime.AutoUpper = true;
            this.txtRefreshTime.Caption = "刷新频率(分钟)";
            this.txtRefreshTime.Checked = false;
            this.txtRefreshTime.EditType = UserControl.EditTypes.Integer;
            this.txtRefreshTime.Location = new System.Drawing.Point(437, 47);
            this.txtRefreshTime.MaxLength = 40;
            this.txtRefreshTime.Multiline = false;
            this.txtRefreshTime.Name = "txtRefreshTime";
            this.txtRefreshTime.PasswordChar = '\0';
            this.txtRefreshTime.ReadOnly = false;
            this.txtRefreshTime.ShowCheckBox = false;
            this.txtRefreshTime.Size = new System.Drawing.Size(197, 24);
            this.txtRefreshTime.TabIndex = 2;
            this.txtRefreshTime.TabNext = true;
            this.txtRefreshTime.Value = "";
            this.txtRefreshTime.WidthType = UserControl.WidthTypes.Small;
            this.txtRefreshTime.XAlign = 534;
            // 
            // txtMCodeQuery
            // 
            this.txtMCodeQuery.AllowEditOnlyChecked = true;
            this.txtMCodeQuery.AutoSelectAll = false;
            this.txtMCodeQuery.AutoUpper = true;
            this.txtMCodeQuery.Caption = "物料代码";
            this.txtMCodeQuery.Checked = false;
            this.txtMCodeQuery.EditType = UserControl.EditTypes.String;
            this.txtMCodeQuery.Location = new System.Drawing.Point(224, 15);
            this.txtMCodeQuery.MaxLength = 40;
            this.txtMCodeQuery.Multiline = false;
            this.txtMCodeQuery.Name = "txtMCodeQuery";
            this.txtMCodeQuery.PasswordChar = '\0';
            this.txtMCodeQuery.ReadOnly = false;
            this.txtMCodeQuery.ShowCheckBox = false;
            this.txtMCodeQuery.Size = new System.Drawing.Size(194, 24);
            this.txtMCodeQuery.TabIndex = 1;
            this.txtMCodeQuery.TabNext = true;
            this.txtMCodeQuery.Value = "";
            this.txtMCodeQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtMCodeQuery.XAlign = 285;
            // 
            // txtMoQuery
            // 
            this.txtMoQuery.AllowEditOnlyChecked = true;
            this.txtMoQuery.AutoSelectAll = false;
            this.txtMoQuery.AutoUpper = true;
            this.txtMoQuery.Caption = "工单代码";
            this.txtMoQuery.Checked = false;
            this.txtMoQuery.EditType = UserControl.EditTypes.String;
            this.txtMoQuery.Location = new System.Drawing.Point(9, 15);
            this.txtMoQuery.MaxLength = 40;
            this.txtMoQuery.Multiline = false;
            this.txtMoQuery.Name = "txtMoQuery";
            this.txtMoQuery.PasswordChar = '\0';
            this.txtMoQuery.ReadOnly = false;
            this.txtMoQuery.ShowCheckBox = false;
            this.txtMoQuery.Size = new System.Drawing.Size(194, 24);
            this.txtMoQuery.TabIndex = 0;
            this.txtMoQuery.TabNext = true;
            this.txtMoQuery.Value = "";
            this.txtMoQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtMoQuery.XAlign = 70;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnReDis);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnDis);
            this.groupBox2.Controls.Add(this.lblDIsStatus);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtDisQty);
            this.groupBox2.Controls.Add(this.txtLastDisQty);
            this.groupBox2.Controls.Add(this.txtMNameEdit);
            this.groupBox2.Controls.Add(this.txtProductionTime);
            this.groupBox2.Controls.Add(this.txtMssLeftQty);
            this.groupBox2.Controls.Add(this.txtMssDisQty);
            this.groupBox2.Controls.Add(this.txtMCodeEdit);
            this.groupBox2.Controls.Add(this.txtMoEdit);
            this.groupBox2.Controls.Add(this.cmbSSCodeEdit);
            this.groupBox2.Controls.Add(this.cmbSegCodeEdit);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 345);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(951, 135);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(611, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "注：替代最后一次配送数量";
            // 
            // btnReDis
            // 
            this.btnReDis.BackColor = System.Drawing.SystemColors.Control;
            this.btnReDis.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReDis.BackgroundImage")));
            this.btnReDis.ButtonType = UserControl.ButtonTypes.None;
            this.btnReDis.Caption = "重新配送";
            this.btnReDis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReDis.Location = new System.Drawing.Point(515, 107);
            this.btnReDis.Name = "btnReDis";
            this.btnReDis.Size = new System.Drawing.Size(88, 22);
            this.btnReDis.TabIndex = 19;
            this.btnReDis.Click += new System.EventHandler(this.btnReDis_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.ButtonType = UserControl.ButtonTypes.None;
            this.btnCancel.Caption = "取消";
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(386, 107);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 22);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDis
            // 
            this.btnDis.BackColor = System.Drawing.SystemColors.Control;
            this.btnDis.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDis.BackgroundImage")));
            this.btnDis.ButtonType = UserControl.ButtonTypes.None;
            this.btnDis.Caption = "配送";
            this.btnDis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDis.Location = new System.Drawing.Point(258, 107);
            this.btnDis.Name = "btnDis";
            this.btnDis.Size = new System.Drawing.Size(88, 22);
            this.btnDis.TabIndex = 17;
            this.btnDis.Click += new System.EventHandler(this.btnDis_Click);
            // 
            // lblDIsStatus
            // 
            this.lblDIsStatus.AutoSize = true;
            this.lblDIsStatus.Location = new System.Drawing.Point(570, 80);
            this.lblDIsStatus.Name = "lblDIsStatus";
            this.lblDIsStatus.Size = new System.Drawing.Size(0, 12);
            this.lblDIsStatus.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(508, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "预警状态";
            // 
            // txtDisQty
            // 
            this.txtDisQty.AllowEditOnlyChecked = true;
            this.txtDisQty.AutoSelectAll = false;
            this.txtDisQty.AutoUpper = true;
            this.txtDisQty.Caption = "本次发料数量";
            this.txtDisQty.Checked = false;
            this.txtDisQty.EditType = UserControl.EditTypes.Integer;
            this.txtDisQty.Location = new System.Drawing.Point(243, 74);
            this.txtDisQty.MaxLength = 40;
            this.txtDisQty.Multiline = false;
            this.txtDisQty.Name = "txtDisQty";
            this.txtDisQty.PasswordChar = '\0';
            this.txtDisQty.ReadOnly = false;
            this.txtDisQty.ShowCheckBox = false;
            this.txtDisQty.Size = new System.Drawing.Size(218, 24);
            this.txtDisQty.TabIndex = 14;
            this.txtDisQty.TabNext = true;
            this.txtDisQty.Value = "";
            this.txtDisQty.WidthType = UserControl.WidthTypes.Normal;
            this.txtDisQty.XAlign = 328;
            // 
            // txtLastDisQty
            // 
            this.txtLastDisQty.AllowEditOnlyChecked = true;
            this.txtLastDisQty.AutoSelectAll = false;
            this.txtLastDisQty.AutoUpper = true;
            this.txtLastDisQty.Caption = "上次发料数量";
            this.txtLastDisQty.Checked = false;
            this.txtLastDisQty.EditType = UserControl.EditTypes.String;
            this.txtLastDisQty.Location = new System.Drawing.Point(5, 74);
            this.txtLastDisQty.MaxLength = 40;
            this.txtLastDisQty.Multiline = false;
            this.txtLastDisQty.Name = "txtLastDisQty";
            this.txtLastDisQty.PasswordChar = '\0';
            this.txtLastDisQty.ReadOnly = false;
            this.txtLastDisQty.ShowCheckBox = false;
            this.txtLastDisQty.Size = new System.Drawing.Size(218, 24);
            this.txtLastDisQty.TabIndex = 13;
            this.txtLastDisQty.TabNext = true;
            this.txtLastDisQty.Value = "";
            this.txtLastDisQty.WidthType = UserControl.WidthTypes.Normal;
            this.txtLastDisQty.XAlign = 90;
            // 
            // txtMNameEdit
            // 
            this.txtMNameEdit.AllowEditOnlyChecked = true;
            this.txtMNameEdit.AutoSelectAll = false;
            this.txtMNameEdit.AutoUpper = true;
            this.txtMNameEdit.Caption = "物料名称";
            this.txtMNameEdit.Checked = false;
            this.txtMNameEdit.EditType = UserControl.EditTypes.String;
            this.txtMNameEdit.Location = new System.Drawing.Point(723, 46);
            this.txtMNameEdit.MaxLength = 40;
            this.txtMNameEdit.Multiline = false;
            this.txtMNameEdit.Name = "txtMNameEdit";
            this.txtMNameEdit.PasswordChar = '\0';
            this.txtMNameEdit.ReadOnly = false;
            this.txtMNameEdit.ShowCheckBox = false;
            this.txtMNameEdit.Size = new System.Drawing.Size(194, 24);
            this.txtMNameEdit.TabIndex = 12;
            this.txtMNameEdit.TabNext = true;
            this.txtMNameEdit.Value = "";
            this.txtMNameEdit.WidthType = UserControl.WidthTypes.Normal;
            this.txtMNameEdit.XAlign = 784;
            // 
            // txtProductionTime
            // 
            this.txtProductionTime.AllowEditOnlyChecked = true;
            this.txtProductionTime.AutoSelectAll = false;
            this.txtProductionTime.AutoUpper = true;
            this.txtProductionTime.Caption = "产线生产时间";
            this.txtProductionTime.Checked = false;
            this.txtProductionTime.EditType = UserControl.EditTypes.String;
            this.txtProductionTime.Location = new System.Drawing.Point(485, 46);
            this.txtProductionTime.MaxLength = 40;
            this.txtProductionTime.Multiline = false;
            this.txtProductionTime.Name = "txtProductionTime";
            this.txtProductionTime.PasswordChar = '\0';
            this.txtProductionTime.ReadOnly = false;
            this.txtProductionTime.ShowCheckBox = false;
            this.txtProductionTime.Size = new System.Drawing.Size(218, 24);
            this.txtProductionTime.TabIndex = 11;
            this.txtProductionTime.TabNext = true;
            this.txtProductionTime.Value = "";
            this.txtProductionTime.WidthType = UserControl.WidthTypes.Normal;
            this.txtProductionTime.XAlign = 570;
            // 
            // txtMssLeftQty
            // 
            this.txtMssLeftQty.AllowEditOnlyChecked = true;
            this.txtMssLeftQty.AutoSelectAll = false;
            this.txtMssLeftQty.AutoUpper = true;
            this.txtMssLeftQty.Caption = "产线剩余数量";
            this.txtMssLeftQty.Checked = false;
            this.txtMssLeftQty.EditType = UserControl.EditTypes.String;
            this.txtMssLeftQty.Location = new System.Drawing.Point(243, 46);
            this.txtMssLeftQty.MaxLength = 40;
            this.txtMssLeftQty.Multiline = false;
            this.txtMssLeftQty.Name = "txtMssLeftQty";
            this.txtMssLeftQty.PasswordChar = '\0';
            this.txtMssLeftQty.ReadOnly = false;
            this.txtMssLeftQty.ShowCheckBox = false;
            this.txtMssLeftQty.Size = new System.Drawing.Size(218, 24);
            this.txtMssLeftQty.TabIndex = 10;
            this.txtMssLeftQty.TabNext = true;
            this.txtMssLeftQty.Value = "";
            this.txtMssLeftQty.WidthType = UserControl.WidthTypes.Normal;
            this.txtMssLeftQty.XAlign = 328;
            // 
            // txtMssDisQty
            // 
            this.txtMssDisQty.AllowEditOnlyChecked = true;
            this.txtMssDisQty.AutoSelectAll = false;
            this.txtMssDisQty.AutoUpper = true;
            this.txtMssDisQty.Caption = "产线已发数量";
            this.txtMssDisQty.Checked = false;
            this.txtMssDisQty.EditType = UserControl.EditTypes.String;
            this.txtMssDisQty.Location = new System.Drawing.Point(5, 46);
            this.txtMssDisQty.MaxLength = 40;
            this.txtMssDisQty.Multiline = false;
            this.txtMssDisQty.Name = "txtMssDisQty";
            this.txtMssDisQty.PasswordChar = '\0';
            this.txtMssDisQty.ReadOnly = false;
            this.txtMssDisQty.ShowCheckBox = false;
            this.txtMssDisQty.Size = new System.Drawing.Size(218, 24);
            this.txtMssDisQty.TabIndex = 9;
            this.txtMssDisQty.TabNext = true;
            this.txtMssDisQty.Value = "";
            this.txtMssDisQty.WidthType = UserControl.WidthTypes.Normal;
            this.txtMssDisQty.XAlign = 90;
            // 
            // txtMCodeEdit
            // 
            this.txtMCodeEdit.AllowEditOnlyChecked = true;
            this.txtMCodeEdit.AutoSelectAll = false;
            this.txtMCodeEdit.AutoUpper = true;
            this.txtMCodeEdit.Caption = "物料代码";
            this.txtMCodeEdit.Checked = false;
            this.txtMCodeEdit.EditType = UserControl.EditTypes.String;
            this.txtMCodeEdit.Location = new System.Drawing.Point(723, 14);
            this.txtMCodeEdit.MaxLength = 40;
            this.txtMCodeEdit.Multiline = false;
            this.txtMCodeEdit.Name = "txtMCodeEdit";
            this.txtMCodeEdit.PasswordChar = '\0';
            this.txtMCodeEdit.ReadOnly = false;
            this.txtMCodeEdit.ShowCheckBox = false;
            this.txtMCodeEdit.Size = new System.Drawing.Size(194, 24);
            this.txtMCodeEdit.TabIndex = 8;
            this.txtMCodeEdit.TabNext = true;
            this.txtMCodeEdit.Value = "";
            this.txtMCodeEdit.WidthType = UserControl.WidthTypes.Normal;
            this.txtMCodeEdit.XAlign = 784;
            // 
            // txtMoEdit
            // 
            this.txtMoEdit.AllowEditOnlyChecked = true;
            this.txtMoEdit.AutoSelectAll = false;
            this.txtMoEdit.AutoUpper = true;
            this.txtMoEdit.Caption = "工单代码";
            this.txtMoEdit.Checked = false;
            this.txtMoEdit.EditType = UserControl.EditTypes.String;
            this.txtMoEdit.Location = new System.Drawing.Point(509, 14);
            this.txtMoEdit.MaxLength = 40;
            this.txtMoEdit.Multiline = false;
            this.txtMoEdit.Name = "txtMoEdit";
            this.txtMoEdit.PasswordChar = '\0';
            this.txtMoEdit.ReadOnly = false;
            this.txtMoEdit.ShowCheckBox = false;
            this.txtMoEdit.Size = new System.Drawing.Size(194, 24);
            this.txtMoEdit.TabIndex = 7;
            this.txtMoEdit.TabNext = true;
            this.txtMoEdit.Value = "";
            this.txtMoEdit.WidthType = UserControl.WidthTypes.Normal;
            this.txtMoEdit.XAlign = 570;
            // 
            // cmbSSCodeEdit
            // 
            this.cmbSSCodeEdit.AllowEditOnlyChecked = true;
            this.cmbSSCodeEdit.Caption = "产线代码";
            this.cmbSSCodeEdit.Checked = false;
            this.cmbSSCodeEdit.Location = new System.Drawing.Point(267, 16);
            this.cmbSSCodeEdit.Name = "cmbSSCodeEdit";
            this.cmbSSCodeEdit.SelectedIndex = -1;
            this.cmbSSCodeEdit.ShowCheckBox = false;
            this.cmbSSCodeEdit.Size = new System.Drawing.Size(194, 20);
            this.cmbSSCodeEdit.TabIndex = 6;
            this.cmbSSCodeEdit.WidthType = UserControl.WidthTypes.Normal;
            this.cmbSSCodeEdit.XAlign = 328;
            // 
            // cmbSegCodeEdit
            // 
            this.cmbSegCodeEdit.AllowEditOnlyChecked = true;
            this.cmbSegCodeEdit.Caption = "车间代码";
            this.cmbSegCodeEdit.Checked = false;
            this.cmbSegCodeEdit.Location = new System.Drawing.Point(29, 16);
            this.cmbSegCodeEdit.Name = "cmbSegCodeEdit";
            this.cmbSegCodeEdit.SelectedIndex = -1;
            this.cmbSegCodeEdit.ShowCheckBox = false;
            this.cmbSegCodeEdit.Size = new System.Drawing.Size(194, 20);
            this.cmbSegCodeEdit.TabIndex = 5;
            this.cmbSegCodeEdit.WidthType = UserControl.WidthTypes.Normal;
            this.cmbSegCodeEdit.XAlign = 90;
            this.cmbSegCodeEdit.SelectedIndexChanged += new System.EventHandler(this.cmbSegCodeEdit_SelectedIndexChanged);
            // 
            // ultraGridMain
            // 
            this.ultraGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMain.Location = new System.Drawing.Point(0, 82);
            this.ultraGridMain.Name = "ultraGridMain";
            this.ultraGridMain.Size = new System.Drawing.Size(951, 263);
            this.ultraGridMain.TabIndex = 2;
            this.ultraGridMain.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMain_InitializeLayout);
            this.ultraGridMain.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ultraGridMain_InitializeRow);
            this.ultraGridMain.ClickCellButton += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridMain_ClickCellButton);
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 1000;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // FDisToLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 480);
            this.Controls.Add(this.ultraGridMain);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FDisToLine";
            this.Text = "产线配料";
            this.Load += new System.EventHandler(this.FDisToLine_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbFinished;
        private System.Windows.Forms.CheckBox cbShortDis;
        private System.Windows.Forms.CheckBox cbERDis;
        private System.Windows.Forms.CheckBox cbWaitDis;
        private System.Windows.Forms.CheckBox cbNormal;
        private UserControl.UCLabelCombox cmbSSCodeQuery;
        private UserControl.UCLabelCombox cmbSegCodeQuery;
        private UserControl.UCLabelEdit txtRefreshTime;
        private UserControl.UCLabelEdit txtMCodeQuery;
        private UserControl.UCButton btnQuery;
        private Label label3;
        private UserControl.UCButton btnReDis;
        private UserControl.UCButton btnCancel;
        private UserControl.UCButton btnDis;
        private Label lblDIsStatus;
        private Label label2;
        private UserControl.UCLabelEdit txtDisQty;
        private UserControl.UCLabelEdit txtLastDisQty;
        private UserControl.UCLabelEdit txtMNameEdit;
        private UserControl.UCLabelEdit txtProductionTime;
        private UserControl.UCLabelEdit txtMssLeftQty;
        private UserControl.UCLabelEdit txtMssDisQty;
        private UserControl.UCLabelEdit txtMCodeEdit;
        private UserControl.UCLabelEdit txtMoEdit;
        private UserControl.UCLabelCombox cmbSSCodeEdit;
        private UserControl.UCLabelCombox cmbSegCodeEdit;
        private UserControl.UCLabelEdit txtMoQuery;

        #endregion

        #region 变量、属性
        private UltraWinGridHelper GridHelper = null;
        private DataTable DataLoadDetail = new DataTable();
        private BaseModelFacade baseModelFacade = null;
        private DataCollectFacade dataCollectFacade = null;
        private Timer timerRefresh;

        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get { return _DomainDataProvider; }
        }
        #endregion

        #region Form Load
        public FDisToLine()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
            InitializeUltraGrid();
        }

        private void FDisToLine_Load(object sender, EventArgs e)
        {
            InitSegCode();
            InitStepSequenceQuery("");
            InitQueryControls();
            InitEditControls();
        }
        #endregion

        #region 初始化赋值
        //车间信息
        private void InitSegCode()
        {
            if (baseModelFacade == null)
                baseModelFacade = new BaseModelFacade(this.DataProvider);

            object[] objSeg = baseModelFacade.GetAllSegment();
            if (objSeg != null)
            {
                cmbSegCodeQuery.Clear();
                cmbSegCodeEdit.Clear();
                cmbSegCodeQuery.AddItem("ALL", "");
                cmbSegCodeEdit.AddItem("", "");
                foreach (Segment seg in objSeg)
                {
                    cmbSegCodeQuery.AddItem(seg.SegmentCode, seg.SegmentCode);
                    cmbSegCodeEdit.AddItem(seg.SegmentCode, seg.SegmentCode);
                }
                this.cmbSegCodeQuery.SelectedIndex = 0;
                this.cmbSegCodeEdit.SelectedIndex = 0;
            }
        }

        //产线信息
        private void InitStepSequenceQuery(string segCode)
        {
            if (baseModelFacade == null)
                baseModelFacade = new BaseModelFacade(this.DataProvider);

            object[] objSS = null;
            cmbSSCodeQuery.Clear();
            cmbSSCodeQuery.AddItem("ALL", "");
            cmbSSCodeEdit.AddItem("", "");
            if (string.IsNullOrEmpty(segCode))
            {
                objSS = baseModelFacade.GetAllStepSequence();
            }
            else
            {
                objSS = baseModelFacade.GetStepSequenceBySegmentCode(segCode);
            }

            if (objSS != null)
            {
                foreach (StepSequence ss in objSS)
                {
                    cmbSSCodeQuery.AddItem(ss.StepSequenceCode, ss.StepSequenceCode);
                    cmbSSCodeEdit.AddItem(ss.StepSequenceCode, ss.StepSequenceCode);
                }

                this.cmbSSCodeQuery.SelectedIndex = 0;
                this.cmbSSCodeEdit.SelectedIndex = 0;
            }
        }

        //初始化查询区域
        private void InitQueryControls()
        {
            this.txtMoQuery.Value = string.Empty;
            this.txtMCodeQuery.Value = string.Empty;

            this.cbNormal.Checked = true;
            this.cbWaitDis.Checked = true;
            this.cbERDis.Checked = true;
            this.cbShortDis.Checked = true;
            this.cbFinished.Checked = false;
        }

        //初始化编辑控件
        private void InitEditControls()
        {
            this.txtMoEdit.Value = string.Empty;
            this.txtMCodeEdit.Value = string.Empty;
            this.txtDisQty.Value = string.Empty;
            this.txtMNameEdit.Value = string.Empty;
            this.txtMssDisQty.Value = string.Empty;
            this.txtMssLeftQty.Value = string.Empty;
            this.txtProductionTime.Value = string.Empty;
            this.txtLastDisQty.Value = string.Empty;
            this.lblDIsStatus.Text = " ";

            this.txtMNameEdit.Enabled = false;
            this.txtMssDisQty.Enabled = false;
            this.txtMssLeftQty.Enabled = false;
            this.txtProductionTime.Enabled = false;
            this.txtLastDisQty.Enabled = false;
        }

        //初始化Grid区域
        private void InitUltraGridUI(UltraGrid ultraGrid)
        {
            ultraGrid.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGrid.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGrid.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGrid.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("LightBlue");
            ultraGrid.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGrid.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGrid.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGrid.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGrid.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }
        private void InitializeUltraGrid()
        {
            InitUltraGridUI(this.ultraGridMain);

            DataLoadDetail.Columns.Add("SeqNo", typeof(int));
            DataLoadDetail.Columns.Add("SendMaterial", typeof(string));//发料按钮
            DataLoadDetail.Columns.Add("Status", typeof(string));//产线预警状态
            DataLoadDetail.Columns.Add("StatusCN", typeof(string));//产线预警状态
            DataLoadDetail.Columns.Add("SegCode", typeof(string));
            //DataLoadDetail.Columns.Add("SegDesc", typeof(string));
            DataLoadDetail.Columns.Add("SSCode", typeof(string));
            //DataLoadDetail.Columns.Add("SSDesc", typeof(string));
            DataLoadDetail.Columns.Add("MSSDisQty", typeof(decimal));
            DataLoadDetail.Columns.Add("MSSLeftQty", typeof(decimal));
            DataLoadDetail.Columns.Add("MSSLeftTime", typeof(string));
            DataLoadDetail.Columns.Add("MQty", typeof(decimal));//上次发料数量
            DataLoadDetail.Columns.Add("MOCode", typeof(string));
            DataLoadDetail.Columns.Add("MCode", typeof(string));
            DataLoadDetail.Columns.Add("MName", typeof(string));
            DataLoadDetail.Columns.Add("MPlanQty", typeof(decimal));
            DataLoadDetail.Columns.Add("MDisQty", typeof(decimal));
            DataLoadDetail.Columns.Add("HStatus", typeof(string));
            DataLoadDetail.Columns.Add("DisList", typeof(string));

            DataLoadDetail.Columns["SeqNo"].ReadOnly = true;
            DataLoadDetail.Columns["SendMaterial"].ReadOnly = true;
            DataLoadDetail.Columns["Status"].ReadOnly = true; 
            DataLoadDetail.Columns["StatusCN"].ReadOnly = true;
            DataLoadDetail.Columns["SegCode"].ReadOnly = true;
            //DataLoadDetail.Columns["SegDesc"].ReadOnly = true;
            DataLoadDetail.Columns["SSCode"].ReadOnly = true;
            //DataLoadDetail.Columns["SSDesc"].ReadOnly = true;
            DataLoadDetail.Columns["MSSDisQty"].ReadOnly = true;
            DataLoadDetail.Columns["MSSLeftQty"].ReadOnly = true;
            DataLoadDetail.Columns["MSSLeftTime"].ReadOnly = true;
            DataLoadDetail.Columns["MQty"].ReadOnly = true;
            DataLoadDetail.Columns["MOCode"].ReadOnly = true;
            DataLoadDetail.Columns["MCode"].ReadOnly = true;
            DataLoadDetail.Columns["MName"].ReadOnly = true;
            DataLoadDetail.Columns["MPlanQty"].ReadOnly = true;
            DataLoadDetail.Columns["MDisQty"].ReadOnly = true;
            DataLoadDetail.Columns["HStatus"].ReadOnly = true;
            DataLoadDetail.Columns["DisList"].ReadOnly = true;

            this.ultraGridMain.DataSource = this.DataLoadDetail;
            DataLoadDetail.Clear();

            ultraGridMain.DisplayLayout.Bands[0].Columns["SeqNo"].Width = 28;
            ultraGridMain.DisplayLayout.Bands[0].Columns["SendMaterial"].Width = 42;
            ultraGridMain.DisplayLayout.Bands[0].Columns["SendMaterial"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            ultraGridMain.DisplayLayout.Bands[0].Columns["SendMaterial"].ButtonDisplayStyle = ButtonDisplayStyle.Always;
            ultraGridMain.DisplayLayout.Bands[0].Columns["SendMaterial"].CellButtonAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            ultraGridMain.DisplayLayout.Bands[0].Columns["Status"].Width = 70;
            ultraGridMain.DisplayLayout.Bands[0].Columns["Status"].Hidden = true;
            ultraGridMain.DisplayLayout.Bands[0].Columns["StatusCN"].Width = 70;
            ultraGridMain.DisplayLayout.Bands[0].Columns["SegCode"].Width = 70;
            //ultraGridMain.DisplayLayout.Bands[0].Columns["SegCode"].Hidden = true;
            //ultraGridMain.DisplayLayout.Bands[0].Columns["SegDesc"].Width = 80;
            ultraGridMain.DisplayLayout.Bands[0].Columns["SSCode"].Width = 70;
            //ultraGridMain.DisplayLayout.Bands[0].Columns["SSCode"].Hidden = true;
            //ultraGridMain.DisplayLayout.Bands[0].Columns["SSDesc"].Width = 70;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MSSDisQty"].Width = 90;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MSSLeftQty"].Width = 90;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MSSLeftTime"].Width = 90;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MQty"].Width = 90;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MOCode"].Width = 90;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MCode"].Width = 90;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MName"].Width = 90;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MPlanQty"].Width = 90;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MDisQty"].Width = 90;
            ultraGridMain.DisplayLayout.Bands[0].Columns["HStatus"].Width = 70;
            ultraGridMain.DisplayLayout.Bands[0].Columns["DisList"].Width = 60;
            ultraGridMain.DisplayLayout.Bands[0].Columns["DisList"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            ultraGridMain.DisplayLayout.Bands[0].Columns["DisList"].ButtonDisplayStyle = ButtonDisplayStyle.Always;
            ultraGridMain.DisplayLayout.Bands[0].Columns["DisList"].CellButtonAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            ultraGridMain.DisplayLayout.Bands[0].Columns["SeqNo"].CellActivation = Activation.NoEdit;
            ultraGridMain.DisplayLayout.Bands[0].Columns["SendMaterial"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["Status"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["StatusCN"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["SegCode"].CellActivation = Activation.ActivateOnly;
            //ultraGridMain.DisplayLayout.Bands[0].Columns["SegDesc"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["SSCode"].CellActivation = Activation.ActivateOnly;
            //ultraGridMain.DisplayLayout.Bands[0].Columns["SSDesc"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MSSDisQty"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MSSLeftQty"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MSSLeftTime"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MQty"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MOCode"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MCode"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MName"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MPlanQty"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MDisQty"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["HStatus"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["DisList"].CellActivation = Activation.ActivateOnly;
           
        }
        #endregion

        #region 方法
        //动态获取产线信息
        private void GetStepSequenceEdit(string segCode)
        {
            if (baseModelFacade == null)
                baseModelFacade = new BaseModelFacade(this.DataProvider);

            object[] objSS = baseModelFacade.GetStepSequenceBySegmentCode(segCode);
            cmbSSCodeEdit.Clear();
            if (objSS != null)
            {
                foreach (StepSequence ss in objSS)
                {
                    cmbSSCodeEdit.AddItem(ss.StepSequenceCode, ss.StepSequenceCode);
                }

                this.cmbSSCodeEdit.SelectedIndex = 0;
            }
        }

        //获取剩余生产时间
        private decimal GetLeftTime(decimal mssLeftQty, decimal mobItemQty, decimal cycleTime, out string leftMinDesc)
        {
            decimal leftTime = 0m;
            //计算剩余生产时间
            if (mobItemQty == 0 || cycleTime == 0)
            {
                leftMinDesc = "0秒";
            }
            else
            {
                leftTime = mssLeftQty / mobItemQty * cycleTime;
                if (leftTime <= 59)
                    leftMinDesc = Math.Ceiling(leftTime) + "秒";
                else
                    leftMinDesc = (Math.Ceiling(Convert.ToDecimal(leftTime / 60))).ToString() + "分" + Math.Ceiling(leftTime % 60) + "秒";
            }
            return leftTime;
        }

        //获取配送实时状态
        private string GetDisToLineStatus(decimal leftTime,decimal cycleTime)
        {
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(DataProvider);
            string strNormal = systemSettingFacade.GetParameterAlias("ALERTMATERIALDISGROUP", "ALERTDISNORMAL");
            int normal = 0;
            if (!string.IsNullOrEmpty(strNormal))
            {
                normal = Convert.ToInt32(strNormal);
            }
            string strDisER = systemSettingFacade.GetParameterAlias("ALERTMATERIALDISGROUP", "ALERTDISER");
            int disER = 0;
            if (!string.IsNullOrEmpty(strDisER))
            {
                disER = Convert.ToInt32(strDisER);
            }
            //状态根据剩余生产时间换算（小于cycleTime为缺料中、小于紧急预警时间为紧急配料、小于正常预警时间为待配送）
            if (leftTime < cycleTime)
            {
                return DisToLineStatus.ShortDis;
            }
            if (leftTime <= disER)
            {
                return DisToLineStatus.ERDis;
            }
            if (leftTime <= normal)
            {
                return DisToLineStatus.WaitDis;
            }
            return DisToLineStatus.NormalDis; ;
        }

        //查询操作
        private void QueryResult(string moCode, string mCode, string segCode, string ssCode, string statusList)
        {
            if (dataCollectFacade == null)
                dataCollectFacade = new DataCollectFacade(this.DataProvider);

            try
            {
                object[] objDis = dataCollectFacade.QueryDistolinedetailForDis(moCode, mCode, segCode, ssCode, statusList);

                DataLoadDetail.Clear();
                if (objDis == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_No_Data_To_Display"));
                    return;
                }

                int cnt = 0;
                foreach (DisToLineForDistribute dis in objDis)
                {
                    cnt++;
                    decimal leftTime = 0m;
                    string leftMin = string.Empty;
                    //计算剩余生产时间
                    leftTime = GetLeftTime(dis.MssleftQty, dis.MOBItemQty, dis.CycleTime, out leftMin);

                    string detailStatus = DisToLineStatus.Finished;
                    if (dis.Status != DisToLineStatus.Finished)
                    {
                        detailStatus = GetDisToLineStatus(leftTime, dis.CycleTime);
                    }

                    DataLoadDetail.Rows.Add(new object[] {
                        cnt,
                        "发料",
                        detailStatus,
                        MutiLanguages.ParserString("$CS_DisLine_" + detailStatus),
                        dis.SegCode,
                        dis.SsCode,
                        dis.MssdisQty,
                        dis.MssleftQty,
                        leftMin,
                        dis.MQty,
                        dis.MoCode,
                        dis.MCode,
                        dis.MName,
                        dis.MPlanQty,
                        dis.MDisQty,
                        MutiLanguages.ParserString("$CS_DisHead_" + dis.HStatus),
                        "发料记录"
                    });
                }
            }
            catch (Exception ex)
            {
                ApplicationRun.GetInfoForm().Add(ex.Message);
            }
        }

        //获取编辑区域信息
        private void SetEditObject(string segCode, string ssCode, string moCode, string mCode)
        {
            if (dataCollectFacade == null)
                dataCollectFacade = new DataCollectFacade(this.DataProvider);

            object[] objs = dataCollectFacade.QueryDistolinedetailForEdit(moCode, mCode, segCode, ssCode);
            if (objs == null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_No_Data_To_Display"));//没有可赋值的数据
                return;
            }

            DisToLineForDistribute dis = objs[0] as DisToLineForDistribute;
            #region 获取leftTime和实时Status
            decimal leftTime = 0m;
            string leftMin = string.Empty;
            //计算剩余生产时间
            leftTime = GetLeftTime(dis.MssleftQty, dis.MOBItemQty, dis.CycleTime, out leftMin);

            string detailStatus = DisToLineStatus.Finished;
            if (dis.Status != DisToLineStatus.Finished)
            {
                detailStatus = GetDisToLineStatus(leftTime, dis.CycleTime);
            }
            #endregion

            this.cmbSegCodeEdit.SetSelectItem(segCode);
            this.cmbSSCodeEdit.SetSelectItem(ssCode);
            this.txtMoEdit.Value = moCode;
            this.txtMCodeEdit.Value = mCode;
            this.txtMssDisQty.Value = dis.MssdisQty.ToString();
            this.txtMssLeftQty.Value = dis.MssleftQty.ToString(); ;
            this.txtMNameEdit.Value = dis.MName;
            this.txtLastDisQty.Value = dis.MQty.ToString();
            this.txtDisQty.Value = Math.Round(dis.MQty, 0).ToString();
            this.txtProductionTime.Value = leftMin;
            this.lblDIsStatus.Text = MutiLanguages.ParserString("$CS_DisLine_" + detailStatus);

            if (detailStatus == DisToLineStatus.NormalDis)
                lblDIsStatus.ForeColor = Color.Green;
            else if (detailStatus == DisToLineStatus.WaitDis)
                lblDIsStatus.ForeColor = Color.Blue;
            else if (detailStatus == DisToLineStatus.ERDis)
                lblDIsStatus.ForeColor = Color.Purple;
            else if (detailStatus == DisToLineStatus.ShortDis)
                lblDIsStatus.ForeColor = Color.Red;
        }

        //配送逻辑
        private bool SaveDisInput(bool isReDis)
        {
            #region 常规检查
            if (this.cmbSegCodeEdit.SelectedItemValue == null || string.IsNullOrEmpty(this.cmbSegCodeEdit.SelectedItemValue.ToString().Trim()))
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CS_Please_InputSegCode"));
                return false;
            }

            if (this.cmbSSCodeEdit.SelectedItemValue == null || string.IsNullOrEmpty(this.cmbSSCodeEdit.SelectedItemValue.ToString().Trim()))
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Please_InputSSCode"));
                return false;
            }

            if (string.IsNullOrEmpty(this.txtMoEdit.Value.Trim()))
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Please_Input_MOCode"));
                return false;
            }

            if (string.IsNullOrEmpty(this.txtMCodeEdit.Value.Trim()))
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Please_Input_MaterialCode"));
                return false;
            }

            int qty = 0;
            if (!Int32.TryParse(this.txtDisQty.Value, out qty) || qty <= 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Error_DisQty_Must_Over_Zero"));
                return false;
            }
            #endregion

            if (dataCollectFacade == null)
                dataCollectFacade = new DataCollectFacade(this.DataProvider);

            //检查工单信息
            MOFacade mofacade = new MOFacade(this.DataProvider);
            object objMO = mofacade.GetMO(this.txtMoEdit.Value.Trim().ToUpper());
            if (objMO == null)
            {
                //工单不存在
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
                return false;
            }
            else
            {
                MO mo = objMO as MO;
                if (mo.MOStatus == MOStatus.Initial || mo.MOStatus == MOStatus.Close)
                {
                    //未下发及已关闭的工单不再配料
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Dis_For_Init_Close"));
                    return false;
                }
            }

            //检查库存信息
            InventoryFacade invFacade = new  InventoryFacade(this.DataProvider);
            object[] objstorage = invFacade.QueryStorageInfoByIDAndMCode(this.txtMCodeEdit.Value.Trim().ToUpper());
            if (objstorage == null||objstorage.Length<=0)
            {
                //物料不存在或没有库存
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Material_Not_Exist_Or_NoStorage"));
                return false;
            }
            else
            {
                decimal nowQty = 0;//当前库存数量
                foreach (StorageInfo si in objstorage)
                {
                    nowQty += si.Storageqty;
                }
                if (qty > nowQty)//超出库存
                {
                    //发料数量超出库存数量
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_DisQTy_More_Than_Storage" + nowQty.ToString()));
                    return false;
                }
            }

            //检查配料主档信息
            object objHead = dataCollectFacade.GetDisToLineHead(this.txtMoEdit.Value.Trim().ToUpper(), this.txtMCodeEdit.Value.Trim().ToUpper());
            if (objHead == null)
            {
                //没有可配送的工单物料信息
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_NO_MO_Material_For_Dis"));
                return false;
            }
            
            //检查配料明细信息
            object[] objdetail = dataCollectFacade.QueryDistolinedetailForEdit(this.txtMoEdit.Value.Trim().ToUpper(),this.txtMCodeEdit.Value.Trim().ToUpper(),
                this.cmbSegCodeEdit.SelectedItemValue.ToString(),this.cmbSSCodeEdit.SelectedItemValue.ToString());

            if (!isReDis)
            {
                #region 正常配送
                try
                {
                    DisToLineHead head = objHead as DisToLineHead;
                    DisToLineDetail detail = null;
                    DisToLineList list = null;
                    if (objdetail == null || objdetail.Length <= 0)//未发过料
                    {
                        if (head.Status == DisHeadStatus.Dis_Finish)
                        {
                            string msg = MutiLanguages.ParserString("$CS_MO_Material_Dis_Finish_Continue");//当前工单下该物料已配送完成，是否继续配送？
                            DialogResult dr = MessageBox.Show(msg, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dr == DialogResult.No)
                            {
                                return false;
                            }
                        }
                    }
                    else if ((objdetail[0] as DisToLineForDistribute).Status == DisToLineStatus.Finished)
                    {
                        string msg = MutiLanguages.ParserString("$CS_MO_Line_Material_Dis_Finish_Continue");//工单在产线下该物料已配送完成，是否继续配送？
                        DialogResult dr = MessageBox.Show(msg, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.No)
                        {
                            return false;
                        }
                    }

                    this.DataProvider.BeginTransaction();
                    DBDateTime dbDatetime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    #region 更新配送主档信息
                    if (head.Status != DisHeadStatus.Dis_Finish)
                    {
                        if ((head.MplanQty - head.MdisQty) > qty)
                        {
                            head.Status = DisHeadStatus.Dis_Distributing;
                        }
                        else
                        {
                            head.Status = DisHeadStatus.Dis_Finish;
                        }
                    }
                    head.MdisQty += qty;
                    head.MaintainDate = dbDatetime.DBDate;
                    head.MaintainTime = dbDatetime.DBTime;
                    head.MaintainUser = ApplicationService.Current().UserCode;

                    dataCollectFacade.UpdateDisToLineHead(head);
                    #endregion

                    #region 更新配送明细信息
                    if (objdetail == null || objdetail.Length <= 0)//未发过料
                    {
                        //Insert配送明细信息
                        detail = new DisToLineDetail();
                        list = new DisToLineList();
                        object[] objMobCycle = dataCollectFacade.GetDisMobItemQtyAndCycleTime(head.MoCode, head.MCode, this.cmbSSCodeEdit.SelectedItemValue.ToString());
                        if (objMobCycle == null || objMobCycle.Length <= 0)
                        {
                            this.DataProvider.RollbackTransaction();
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "未维护产品生产节拍"));
                            return false;
                        }

                        DisToLineForDistribute dis = objMobCycle[0] as DisToLineForDistribute;

                        decimal leftTime = 0m;
                        string leftMin = string.Empty;
                        leftTime = GetLeftTime(qty, dis.MOBItemQty, dis.CycleTime, out leftMin);

                        detail.MssleftTime = leftTime;
                        detail.Status = (head.Status == DisHeadStatus.Dis_Finish ? DisToLineStatus.Finished : GetDisToLineStatus(leftTime, dis.CycleTime));
                        detail.MoCode = head.MoCode;
                        detail.MCode = head.MCode;
                        detail.SegCode = this.cmbSegCodeEdit.SelectedItemValue.ToString();
                        detail.OpCode = "1";
                        detail.SsCode = this.cmbSSCodeEdit.SelectedItemValue.ToString();
                        detail.MssdisQty = qty;
                        detail.MssleftQty = qty;
                        detail.MQty = qty;
                        detail.MaintainUser = ApplicationService.Current().UserCode;
                        detail.MaintainDate = dbDatetime.DBDate;
                        detail.MaintainTime = dbDatetime.DBTime;

                        dataCollectFacade.AddDistolinedetail(detail);

                        list.MssleftTime = leftTime;
                        list.Status = detail.Status;
                        list.MoCode = head.MoCode;
                        list.MCode = head.MCode;
                        list.SegCode = this.cmbSegCodeEdit.SelectedItemValue.ToString();
                        list.OpCode = "1";
                        list.SsCode = this.cmbSSCodeEdit.SelectedItemValue.ToString();
                        list.MssdisQty = qty;
                        list.MssleftQty = qty;
                        list.MQty = qty;
                        list.Delflag = "N";
                        list.MaintainUser = ApplicationService.Current().UserCode;
                        list.MaintainDate = dbDatetime.DBDate;
                        list.MaintainTime = dbDatetime.DBTime;
                        dataCollectFacade.AddDistolinelist(list);
                    }
                    else
                    {
                        //Update配送明细信息
                        DisToLineForDistribute dis = objdetail[0] as DisToLineForDistribute;
                        detail = new DisToLineDetail();
                        list = new DisToLineList();

                        decimal leftTime = 0m;
                        string leftMin = string.Empty;
                        leftTime = GetLeftTime(qty + dis.MssleftQty, dis.MOBItemQty, dis.CycleTime, out leftMin);

                        detail.MssleftTime = leftTime;
                        detail.Status = (head.Status == DisHeadStatus.Dis_Finish ? DisToLineStatus.Finished : GetDisToLineStatus(leftTime, dis.CycleTime));
                        detail.MoCode = head.MoCode;
                        detail.MCode = head.MCode;
                        detail.SegCode = this.cmbSegCodeEdit.SelectedItemValue.ToString();
                        detail.OpCode = "1";
                        detail.SsCode = this.cmbSSCodeEdit.SelectedItemValue.ToString();
                        detail.MssdisQty = dis.MssdisQty + qty;
                        detail.MssleftQty = dis.MssleftQty + qty;
                        detail.MQty = qty;
                        detail.MaintainUser = ApplicationService.Current().UserCode;
                        detail.MaintainDate = dbDatetime.DBDate;
                        detail.MaintainTime = dbDatetime.DBTime;

                        dataCollectFacade.UpdateDistolinedetail(detail);

                        list.MssleftTime = leftTime;
                        list.Status = detail.Status;
                        list.MoCode = head.MoCode;
                        list.MCode = head.MCode;
                        list.SegCode = this.cmbSegCodeEdit.SelectedItemValue.ToString();
                        list.OpCode = "1";
                        list.SsCode = this.cmbSSCodeEdit.SelectedItemValue.ToString();
                        list.MssdisQty = dis.MssdisQty + qty;
                        list.MssleftQty = dis.MssleftQty + qty;
                        list.MQty = qty;
                        list.Delflag = "N";
                        list.MaintainUser = ApplicationService.Current().UserCode;
                        list.MaintainDate = dbDatetime.DBDate;
                        list.MaintainTime = dbDatetime.DBTime;
                        dataCollectFacade.AddDistolinelist(list);
                    }
                    #endregion
                    this.DataProvider.CommitTransaction();
                    return true;
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    ApplicationRun.GetInfoForm().Add(ex.Message);
                    return false;
                }
                #endregion
            }
            else  //重新配送
            {
                #region 重新配送
                try
                {
                    //确认重新配送
                    DialogResult drReDis = MessageBox.Show(MutiLanguages.ParserString("$$CS_Confirm_ReDis"), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (drReDis == DialogResult.No)
                    {
                        return false;
                    }

                    if (objdetail == null || objdetail.Length <= 0)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_No_Material_For_ReDis"));
                        return false;
                    }

                    //获取最后一笔配送信息
                    DisToLineForDistribute dis = objdetail[0] as DisToLineForDistribute;
                    //检查产线剩余数量小于最后一次配送数量
                    if (dis.MssleftQty < dis.MQty)
                    {
                        //上次配送物料已经使用，无法重新配送
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Last_Material_Has_Used"));
                        return false;
                    }

                    if (qty > dis.MQty)  //本次配送数量大于上次配送数量
                    {
                        //本次配送数量大于上次配送数量，无法重新配送
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Dis_Qty_More_Than_Last_Qty"));
                        return false;
                    }

                    DisToLineHead head = objHead as DisToLineHead;
                    DisToLineDetail detail = null;
                    DisToLineList list = null;
                    if (dis.Status == DisToLineStatus.Finished)
                    {
                        string msg = MutiLanguages.ParserString("$CS_MO_Line_Material_Dis_Finish_Continue");//工单在产线下该物料已配送完成，是否继续配送？
                        DialogResult dr = MessageBox.Show(msg, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.No)
                        {
                            return false;
                        }
                    }

                    this.DataProvider.BeginTransaction();
                    DBDateTime dbDatetime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    #region 更新配送主档信息
                    if ((head.MplanQty - head.MdisQty) > (dis.MQty - qty))
                    {
                        head.Status = DisHeadStatus.Dis_Distributing;
                    }
                    else
                    {
                        head.Status = DisHeadStatus.Dis_Finish;
                    }
                    head.MdisQty += (qty - dis.MQty);
                    head.MaintainDate = dbDatetime.DBDate;
                    head.MaintainTime = dbDatetime.DBTime;
                    head.MaintainUser = ApplicationService.Current().UserCode;

                    dataCollectFacade.UpdateDisToLineHead(head);
                    #endregion

                    #region 更新配送明细信息
                    //Update配送明细信息
                    detail = new DisToLineDetail();
                    list = new DisToLineList();

                    decimal leftTime = 0m;
                    string leftMin = string.Empty;
                    leftTime = GetLeftTime(qty + dis.MssleftQty - dis.MQty, dis.MOBItemQty, dis.CycleTime, out leftMin);

                    detail.MssleftTime = leftTime;
                    detail.Status = (head.Status == DisHeadStatus.Dis_Finish ? DisToLineStatus.Finished : GetDisToLineStatus(leftTime, dis.CycleTime));
                    detail.MoCode = head.MoCode;
                    detail.MCode = head.MCode;
                    detail.SegCode = this.cmbSegCodeEdit.SelectedItemValue.ToString();
                    detail.OpCode = "1";
                    detail.SsCode = this.cmbSSCodeEdit.SelectedItemValue.ToString();
                    detail.MssdisQty = dis.MssdisQty + qty - dis.MQty;
                    detail.MssleftQty = dis.MssleftQty + qty - dis.MQty;
                    detail.MQty = qty;
                    detail.MaintainUser = ApplicationService.Current().UserCode;
                    detail.MaintainDate = dbDatetime.DBDate;
                    detail.MaintainTime = dbDatetime.DBTime;

                    dataCollectFacade.UpdateDistolinedetail(detail);

                    list.MssleftTime = leftTime;
                    list.Status = detail.Status;
                    list.MoCode = head.MoCode;
                    list.MCode = head.MCode;
                    list.SegCode = this.cmbSegCodeEdit.SelectedItemValue.ToString();
                    list.OpCode = "1";
                    list.SsCode = this.cmbSSCodeEdit.SelectedItemValue.ToString();
                    list.MssdisQty = dis.MssdisQty + qty - dis.MQty;
                    list.MssleftQty = dis.MssleftQty + qty - dis.MQty;
                    list.MQty = qty;
                    list.Delflag = "Y";
                    list.MaintainUser = ApplicationService.Current().UserCode;
                    list.MaintainDate = dbDatetime.DBDate;
                    list.MaintainTime = dbDatetime.DBTime;
                    dataCollectFacade.AddDistolinelist(list);

                    #endregion
                    this.DataProvider.CommitTransaction();
                    return true;
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    ApplicationRun.GetInfoForm().Add(ex.Message);
                    return false;
                }
                #endregion
            }
        }
        #endregion

        #region 事件
        //车间改变，同步获取产线信息
        private void cmbSegCodeQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitStepSequenceQuery(this.cmbSegCodeQuery.SelectedItemValue.ToString());
        }

        private void cmbSegCodeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbSegCodeEdit.SelectedItemValue == null)
            {
                GetStepSequenceEdit(string.Empty);
            }
            else
            {
                GetStepSequenceEdit(this.cmbSegCodeEdit.SelectedItemValue.ToString());
            }
        }

        //Grid事件
        private void ultraGridMain_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            GridHelper = new UltraWinGridHelper(this.ultraGridMain);

            GridHelper.AddReadOnlyColumn("SeqNo", "No.");
            GridHelper.AddReadOnlyColumn("SendMaterial", "");
            GridHelper.AddReadOnlyColumn("Status", "预警");
            GridHelper.AddReadOnlyColumn("StatusCN", "配送预警");
            GridHelper.AddReadOnlyColumn("SegCode", "车间代码");
            //GridHelper.AddReadOnlyColumn("SegDesc", "车间");
            GridHelper.AddReadOnlyColumn("SSCode", "产线代码");
            //GridHelper.AddReadOnlyColumn("SSDesc", "产线");
            GridHelper.AddReadOnlyColumn("MSSDisQty", "产线已发数量");
            GridHelper.AddReadOnlyColumn("MSSLeftQty", "产线剩余数量");
            GridHelper.AddReadOnlyColumn("MSSLeftTime", "剩余生产时间");
            GridHelper.AddReadOnlyColumn("MQty", "本次发料数量");
            GridHelper.AddReadOnlyColumn("MOCode", "工单");
            GridHelper.AddReadOnlyColumn("MCode", "物料");
            GridHelper.AddReadOnlyColumn("MName", "物料名称");
            GridHelper.AddReadOnlyColumn("MPlanQty", "工单计划数量");
            GridHelper.AddReadOnlyColumn("MDisQty", "工单已发数量");
            GridHelper.AddReadOnlyColumn("HStatus", "配料状态");
            GridHelper.AddReadOnlyColumn("DisList", "");
            
        }

        private void ultraGridMain_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            UltraGridCell gridCell = e.Row.Cells["Status"];

            if (gridCell.Value.Equals(DisToLineStatus.ShortDis))
            {
                gridCell.Row.Appearance.BackColor = Color.Red;
            }
            else if (gridCell.Value.Equals(DisToLineStatus.ERDis))
            {
                gridCell.Row.Appearance.BackColor = Color.Yellow;
            }
            else if (gridCell.Value.Equals(DisToLineStatus.WaitDis))
            {
                gridCell.Row.Appearance.BackColor = Color.LightBlue;
            }
            else if (gridCell.Value.Equals(DisToLineStatus.NormalDis))
            {
                gridCell.Row.Appearance.BackColor = Color.LightGreen;
            }
        }

        private void ultraGridMain_ClickCellButton(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Key == "SendMaterial")//点击发料按钮
            {
                UltraGridRow row = e.Cell.Row;
                SetEditObject(row.Cells["SegCode"].Value.ToString(), row.Cells["SSCode"].Value.ToString(),
                    row.Cells["MOCode"].Value.ToString(), row.Cells["MCode"].Value.ToString());
            }
            else if (e.Cell.Column.Key == "DisList")//查看发料记录
            {
                UltraGridRow row = e.Cell.Row;
                FDisTOLineList disList = new FDisTOLineList(row.Cells["MOCode"].Value.ToString(),
                    row.Cells["MCode"].Value.ToString(),
                    row.Cells["SegCode"].Value.ToString(),
                    row.Cells["SSCode"].Value.ToString(), "1");
                disList.ShowDialog(this);
            }
        }

        //查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            string moCode = this.txtMoQuery.Value.Trim().ToUpper();
            string mCode = this.txtMCodeQuery.Value.Trim().ToUpper();
            string segCode = this.cmbSegCodeQuery.SelectedItemValue.ToString();
            string ssCode = this.cmbSSCodeQuery.SelectedItemValue.ToString();

            string statusList = string.Empty;
            if (cbNormal.Checked)
                statusList += "'" + DisToLineStatus.NormalDis + "',";
            if(cbWaitDis.Checked)
                statusList += "'" + DisToLineStatus.WaitDis + "',";
            if(cbERDis.Checked)
                statusList += "'" + DisToLineStatus.ERDis + "',";
            if(cbShortDis.Checked)
                statusList += "'" + DisToLineStatus.ShortDis + "',";
            if(cbFinished.Checked)
                statusList += "'" + DisToLineStatus.Finished + "',";
            if (statusList.Length > 0)
                statusList = statusList.Substring(0, statusList.Length - 1);

            int refreshTime = 0;
            if (Int32.TryParse(this.txtRefreshTime.Value.Trim(), out refreshTime) && refreshTime > 0)
            {
                //开启定时刷新
                this.timerRefresh.Interval = refreshTime * 60000;
                this.timerRefresh.Enabled = true;
                this.timerRefresh.Start();

            }
            else
            {
                this.timerRefresh.Enabled = false;
                this.timerRefresh.Stop();
            }
            //查询操作
            QueryResult(moCode, mCode, segCode, ssCode, statusList);
            
        }

        //定时刷新数据
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            string moCode = this.txtMoQuery.Value.Trim().ToUpper();
            string mCode = this.txtMCodeQuery.Value.Trim().ToUpper();
            string segCode = this.cmbSegCodeQuery.SelectedItemValue.ToString();
            string ssCode = this.cmbSSCodeQuery.SelectedItemValue.ToString();

            string statusList = string.Empty;
            if (cbNormal.Checked)
                statusList += "'" + DisToLineStatus.NormalDis + "',";
            if (cbWaitDis.Checked)
                statusList += "'" + DisToLineStatus.WaitDis + "',";
            if (cbERDis.Checked)
                statusList += "'" + DisToLineStatus.ERDis + "',";
            if (cbShortDis.Checked)
                statusList += "'" + DisToLineStatus.ShortDis + "',";
            if (cbFinished.Checked)
                statusList += "'" + DisToLineStatus.Finished + "',";
            if (statusList.Length > 0)
                statusList = statusList.Substring(0, statusList.Length - 1);

            //查询操作
            QueryResult(moCode, mCode, segCode, ssCode, statusList);
        }

        //物料配送
        private void btnDis_Click(object sender, EventArgs e)
        {
            if (SaveDisInput(false))
            {
                this.InitEditControls();
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_Dis_Success"));//物料配送成功
                btnQuery_Click(sender, e);
            }
        }

        //取消操作
        private void btnCancel_Click(object sender, EventArgs e)
        {
            InitEditControls();
            this.txtMoEdit.TextFocus(true, true);
        }

        //重新配送
        private void btnReDis_Click(object sender, EventArgs e)
        {
            if (SaveDisInput(true))
            {
                this.InitEditControls();
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_ReDis_Success"));//物料重新配送成功
                btnQuery_Click(sender, e);
            }
        }

        #endregion

    }
}
