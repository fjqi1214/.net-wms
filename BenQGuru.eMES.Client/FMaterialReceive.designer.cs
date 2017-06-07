namespace BenQGuru.eMES.Client
{
    partial class FMaterialReceive
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMaterialReceive));
            this.ultraGridScrutiny = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.drpBusiness = new UserControl.UCLabelCombox();
            this.btnExit = new UserControl.UCButton();
            this.btnReceive = new UserControl.UCButton();
            this.btnClose = new UserControl.UCButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIsReceive = new System.Windows.Forms.CheckBox();
            this.txtBussCode = new UserControl.UCLabelEdit();
            this.txtReceiveCode = new UserControl.UCLabelEdit();
            this.txtstack = new System.Windows.Forms.TextBox();
            this.txtstroage = new System.Windows.Forms.TextBox();
            this.btnGetBussCode = new System.Windows.Forms.Button();
            this.ucEndDate = new UserControl.UCDatetTime();
            this.ucBegDate = new UserControl.UCDatetTime();
            this.btnQuery = new UserControl.UCButton();
            this.drpTypeCode = new UserControl.UCLabelCombox();
            this.uclMaterialCode = new UserControl.UCLabelEdit();
            this.btnMaterialCode = new System.Windows.Forms.Button();
            this.drpControlType = new UserControl.UCLabelCombox();
            this.drpStatus = new UserControl.UCLabelCombox();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridScrutiny)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraGridScrutiny
            // 
            this.ultraGridScrutiny.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridScrutiny.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridScrutiny.Location = new System.Drawing.Point(3, 17);
            this.ultraGridScrutiny.Name = "ultraGridScrutiny";
            this.ultraGridScrutiny.Size = new System.Drawing.Size(829, 233);
            this.ultraGridScrutiny.TabIndex = 1;
            this.ultraGridScrutiny.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ultraGridScrutiny_InitializeRow);
            this.ultraGridScrutiny.ClickCellButton += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridScrutiny_ClickCellButton);
            this.ultraGridScrutiny.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridScrutiny_InitializeLayout);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkAll);
            this.groupBox3.Controls.Add(this.drpBusiness);
            this.groupBox3.Controls.Add(this.btnExit);
            this.groupBox3.Controls.Add(this.btnReceive);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 372);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(835, 60);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(581, 22);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(48, 16);
            this.chkAll.TabIndex = 215;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.Visible = false;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // drpBusiness
            // 
            this.drpBusiness.AllowEditOnlyChecked = true;
            this.drpBusiness.Caption = "业务类型";
            this.drpBusiness.Checked = false;
            this.drpBusiness.Location = new System.Drawing.Point(22, 18);
            this.drpBusiness.Name = "drpBusiness";
            this.drpBusiness.SelectedIndex = -1;
            this.drpBusiness.ShowCheckBox = false;
            this.drpBusiness.Size = new System.Drawing.Size(194, 20);
            this.drpBusiness.TabIndex = 214;
            this.drpBusiness.WidthType = UserControl.WidthTypes.Normal;
            this.drpBusiness.XAlign = 83;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Confirm;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(465, 18);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 33;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReceive
            // 
            this.btnReceive.BackColor = System.Drawing.SystemColors.Control;
            this.btnReceive.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReceive.BackgroundImage")));
            this.btnReceive.ButtonType = UserControl.ButtonTypes.Confirm;
            this.btnReceive.Caption = "接受";
            this.btnReceive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReceive.Location = new System.Drawing.Point(257, 18);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(88, 22);
            this.btnReceive.TabIndex = 31;
            this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Control;
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.ButtonType = UserControl.ButtonTypes.Cancle;
            this.btnClose.Caption = "关闭";
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(361, 18);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 22);
            this.btnClose.TabIndex = 32;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ultraGridScrutiny);
            this.groupBox2.Location = new System.Drawing.Point(0, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(835, 253);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.drpStatus);
            this.groupBox1.Controls.Add(this.drpControlType);
            this.groupBox1.Controls.Add(this.uclMaterialCode);
            this.groupBox1.Controls.Add(this.btnMaterialCode);
            this.groupBox1.Controls.Add(this.chkIsReceive);
            this.groupBox1.Controls.Add(this.txtBussCode);
            this.groupBox1.Controls.Add(this.txtReceiveCode);
            this.groupBox1.Controls.Add(this.txtstack);
            this.groupBox1.Controls.Add(this.txtstroage);
            this.groupBox1.Controls.Add(this.btnGetBussCode);
            this.groupBox1.Controls.Add(this.ucEndDate);
            this.groupBox1.Controls.Add(this.ucBegDate);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.drpTypeCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(835, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chkIsReceive
            // 
            this.chkIsReceive.AutoSize = true;
            this.chkIsReceive.Checked = true;
            this.chkIsReceive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsReceive.Location = new System.Drawing.Point(540, 48);
            this.chkIsReceive.Name = "chkIsReceive";
            this.chkIsReceive.Size = new System.Drawing.Size(60, 16);
            this.chkIsReceive.TabIndex = 38;
            this.chkIsReceive.Text = "可入库";
            this.chkIsReceive.UseVisualStyleBackColor = true;
            this.chkIsReceive.Visible = false;
            // 
            // txtBussCode
            // 
            this.txtBussCode.AllowEditOnlyChecked = true;
            this.txtBussCode.AutoSelectAll = false;
            this.txtBussCode.AutoUpper = true;
            this.txtBussCode.Caption = "供应商代码";
            this.txtBussCode.Checked = false;
            this.txtBussCode.EditType = UserControl.EditTypes.String;
            this.txtBussCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtBussCode.Location = new System.Drawing.Point(20, 101);
            this.txtBussCode.MaxLength = 40;
            this.txtBussCode.Multiline = false;
            this.txtBussCode.Name = "txtBussCode";
            this.txtBussCode.PasswordChar = '\0';
            this.txtBussCode.ReadOnly = false;
            this.txtBussCode.ShowCheckBox = false;
            this.txtBussCode.Size = new System.Drawing.Size(206, 22);
            this.txtBussCode.TabIndex = 218;
            this.txtBussCode.TabNext = true;
            this.txtBussCode.Value = "";
            this.txtBussCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtBussCode.XAlign = 93;
            // 
            // txtReceiveCode
            // 
            this.txtReceiveCode.AllowEditOnlyChecked = true;
            this.txtReceiveCode.AutoSelectAll = false;
            this.txtReceiveCode.AutoUpper = true;
            this.txtReceiveCode.Caption = "入库单号  ";
            this.txtReceiveCode.Checked = false;
            this.txtReceiveCode.EditType = UserControl.EditTypes.String;
            this.txtReceiveCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtReceiveCode.Location = new System.Drawing.Point(22, 20);
            this.txtReceiveCode.MaxLength = 40;
            this.txtReceiveCode.Multiline = false;
            this.txtReceiveCode.Name = "txtReceiveCode";
            this.txtReceiveCode.PasswordChar = '\0';
            this.txtReceiveCode.ReadOnly = false;
            this.txtReceiveCode.ShowCheckBox = false;
            this.txtReceiveCode.Size = new System.Drawing.Size(273, 22);
            this.txtReceiveCode.TabIndex = 1;
            this.txtReceiveCode.TabNext = true;
            this.txtReceiveCode.Value = "";
            this.txtReceiveCode.WidthType = UserControl.WidthTypes.Long;
            this.txtReceiveCode.XAlign = 95;
            // 
            // txtstack
            // 
            this.txtstack.Location = new System.Drawing.Point(681, 21);
            this.txtstack.Name = "txtstack";
            this.txtstack.Size = new System.Drawing.Size(36, 21);
            this.txtstack.TabIndex = 215;
            this.txtstack.Visible = false;
            // 
            // txtstroage
            // 
            this.txtstroage.Location = new System.Drawing.Point(629, 21);
            this.txtstroage.Name = "txtstroage";
            this.txtstroage.Size = new System.Drawing.Size(34, 21);
            this.txtstroage.TabIndex = 214;
            this.txtstroage.Visible = false;
            // 
            // btnGetBussCode
            // 
            this.btnGetBussCode.Location = new System.Drawing.Point(232, 100);
            this.btnGetBussCode.Name = "btnGetBussCode";
            this.btnGetBussCode.Size = new System.Drawing.Size(34, 22);
            this.btnGetBussCode.TabIndex = 211;
            this.btnGetBussCode.Text = "...";
            this.btnGetBussCode.UseVisualStyleBackColor = true;
            this.btnGetBussCode.Click += new System.EventHandler(this.btnGetBussCode_Click);
            // 
            // ucEndDate
            // 
            this.ucEndDate.Caption = "到";
            this.ucEndDate.Location = new System.Drawing.Point(182, 46);
            this.ucEndDate.Name = "ucEndDate";
            this.ucEndDate.ShowType = UserControl.DateTimeTypes.Date;
            this.ucEndDate.Size = new System.Drawing.Size(113, 22);
            this.ucEndDate.TabIndex = 210;
            this.ucEndDate.Value = new System.DateTime(2010, 8, 25, 0, 0, 0, 0);
            this.ucEndDate.XAlign = 207;
            // 
            // ucBegDate
            // 
            this.ucBegDate.Caption = "创建日期从";
            this.ucBegDate.Location = new System.Drawing.Point(22, 46);
            this.ucBegDate.Name = "ucBegDate";
            this.ucBegDate.ShowType = UserControl.DateTimeTypes.Date;
            this.ucBegDate.Size = new System.Drawing.Size(161, 22);
            this.ucBegDate.TabIndex = 209;
            this.ucBegDate.Value = new System.DateTime(2010, 8, 25, 0, 0, 0, 0);
            this.ucBegDate.XAlign = 95;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuery.BackColor = System.Drawing.SystemColors.Control;
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.ButtonType = UserControl.ButtonTypes.None;
            this.btnQuery.Caption = "查询";
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.Location = new System.Drawing.Point(615, 75);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(88, 22);
            this.btnQuery.TabIndex = 208;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // drpTypeCode
            // 
            this.drpTypeCode.AllowEditOnlyChecked = true;
            this.drpTypeCode.Caption = "单据类型";
            this.drpTypeCode.Checked = false;
            this.drpTypeCode.Location = new System.Drawing.Point(336, 20);
            this.drpTypeCode.Name = "drpTypeCode";
            this.drpTypeCode.SelectedIndex = -1;
            this.drpTypeCode.ShowCheckBox = false;
            this.drpTypeCode.Size = new System.Drawing.Size(194, 22);
            this.drpTypeCode.TabIndex = 25;
            this.drpTypeCode.WidthType = UserControl.WidthTypes.Normal;
            this.drpTypeCode.XAlign = 397;
            // 
            // uclMaterialCode
            // 
            this.uclMaterialCode.AllowEditOnlyChecked = true;
            this.uclMaterialCode.AutoSelectAll = false;
            this.uclMaterialCode.AutoUpper = true;
            this.uclMaterialCode.Caption = "物料代码";
            this.uclMaterialCode.Checked = false;
            this.uclMaterialCode.EditType = UserControl.EditTypes.String;
            this.uclMaterialCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.uclMaterialCode.Location = new System.Drawing.Point(32, 73);
            this.uclMaterialCode.MaxLength = 40;
            this.uclMaterialCode.Multiline = false;
            this.uclMaterialCode.Name = "uclMaterialCode";
            this.uclMaterialCode.PasswordChar = '\0';
            this.uclMaterialCode.ReadOnly = false;
            this.uclMaterialCode.ShowCheckBox = false;
            this.uclMaterialCode.Size = new System.Drawing.Size(194, 22);
            this.uclMaterialCode.TabIndex = 220;
            this.uclMaterialCode.TabNext = true;
            this.uclMaterialCode.Value = "";
            this.uclMaterialCode.WidthType = UserControl.WidthTypes.Normal;
            this.uclMaterialCode.XAlign = 93;
            // 
            // btnMaterialCode
            // 
            this.btnMaterialCode.Location = new System.Drawing.Point(232, 72);
            this.btnMaterialCode.Name = "btnMaterialCode";
            this.btnMaterialCode.Size = new System.Drawing.Size(34, 22);
            this.btnMaterialCode.TabIndex = 219;
            this.btnMaterialCode.Text = "...";
            this.btnMaterialCode.UseVisualStyleBackColor = true;
            this.btnMaterialCode.Click += new System.EventHandler(this.btnMaterialCode_Click);
            // 
            // drpControlType
            // 
            this.drpControlType.AllowEditOnlyChecked = true;
            this.drpControlType.Caption = "管控类型";
            this.drpControlType.Checked = false;
            this.drpControlType.Location = new System.Drawing.Point(336, 46);
            this.drpControlType.Name = "drpControlType";
            this.drpControlType.SelectedIndex = -1;
            this.drpControlType.ShowCheckBox = false;
            this.drpControlType.Size = new System.Drawing.Size(194, 22);
            this.drpControlType.TabIndex = 221;
            this.drpControlType.WidthType = UserControl.WidthTypes.Normal;
            this.drpControlType.XAlign = 397;
            // 
            // drpStatus
            // 
            this.drpStatus.AllowEditOnlyChecked = true;
            this.drpStatus.Caption = "当前状态";
            this.drpStatus.Checked = false;
            this.drpStatus.Location = new System.Drawing.Point(336, 73);
            this.drpStatus.Name = "drpStatus";
            this.drpStatus.SelectedIndex = -1;
            this.drpStatus.ShowCheckBox = false;
            this.drpStatus.Size = new System.Drawing.Size(194, 22);
            this.drpStatus.TabIndex = 222;
            this.drpStatus.WidthType = UserControl.WidthTypes.Normal;
            this.drpStatus.XAlign = 397;
            // 
            // FMaterialReceive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(835, 432);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FMaterialReceive";
            this.Text = "物料收料入库";
            this.Load += new System.EventHandler(this.FMaterialReceive_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridScrutiny)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCButton btnExit;
        private UserControl.UCButton btnReceive;
        private UserControl.UCButton btnClose;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridScrutiny;
        private System.Windows.Forms.GroupBox groupBox3;
        private UserControl.UCLabelCombox drpBusiness;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControl.UCButton btnQuery;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCLabelCombox drpTypeCode;
        private UserControl.UCDatetTime ucEndDate;
        private UserControl.UCDatetTime ucBegDate;
        private System.Windows.Forms.Button btnGetBussCode;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.TextBox txtstack;
        private System.Windows.Forms.TextBox txtstroage;
        private UserControl.UCLabelEdit txtBussCode;
        private UserControl.UCLabelEdit txtReceiveCode;
        private System.Windows.Forms.CheckBox chkIsReceive;
        private UserControl.UCLabelEdit uclMaterialCode;
        private System.Windows.Forms.Button btnMaterialCode;
        private UserControl.UCLabelCombox drpControlType;
        private UserControl.UCLabelCombox drpStatus;
    }
}