namespace BenQGuru.eMES.Client
{
    partial class FPauseSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPauseSetting));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBigSSCode = new UserControl.UCLabelEdit();
            this.chkIsFinished = new System.Windows.Forms.CheckBox();
            this.btnQuiry = new UserControl.UCButton();
            this.txtRcardTo = new UserControl.UCLabelEdit();
            this.txtRcardFrom = new UserControl.UCLabelEdit();
            this.inINVDateTo = new UserControl.UCDatetTime();
            this.inINVdateFrom = new UserControl.UCDatetTime();
            this.txtItemDesc = new UserControl.UCLabelEdit();
            this.txtBOM = new UserControl.UCLabelEdit();
            this.txtModelCode = new UserControl.UCLabelEdit();
            this.pauseSetting = new System.Windows.Forms.GroupBox();
            this.txtPauseReason = new UserControl.UCLabelEdit();
            this.txtPauseCode = new UserControl.UCLabelEdit();
            this.gridInfo = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnSave = new UserControl.UCButton();
            this.btnCancel = new UserControl.UCButton();
            this.groupBox1.SuspendLayout();
            this.pauseSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBigSSCode);
            this.groupBox1.Controls.Add(this.chkIsFinished);
            this.groupBox1.Controls.Add(this.btnQuiry);
            this.groupBox1.Controls.Add(this.txtRcardTo);
            this.groupBox1.Controls.Add(this.txtRcardFrom);
            this.groupBox1.Controls.Add(this.inINVDateTo);
            this.groupBox1.Controls.Add(this.inINVdateFrom);
            this.groupBox1.Controls.Add(this.txtItemDesc);
            this.groupBox1.Controls.Add(this.txtBOM);
            this.groupBox1.Controls.Add(this.txtModelCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(792, 117);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtBigSSCode
            // 
            this.txtBigSSCode.AllowEditOnlyChecked = true;
            this.txtBigSSCode.AutoSelectAll = false;
            this.txtBigSSCode.AutoUpper = true;
            this.txtBigSSCode.Caption = "大线";
            this.txtBigSSCode.Checked = false;
            this.txtBigSSCode.EditType = UserControl.EditTypes.String;
            this.txtBigSSCode.Location = new System.Drawing.Point(499, 47);
            this.txtBigSSCode.MaxLength = 40;
            this.txtBigSSCode.Multiline = false;
            this.txtBigSSCode.Name = "txtBigSSCode";
            this.txtBigSSCode.PasswordChar = '\0';
            this.txtBigSSCode.ReadOnly = false;
            this.txtBigSSCode.ShowCheckBox = false;
            this.txtBigSSCode.Size = new System.Drawing.Size(170, 24);
            this.txtBigSSCode.TabIndex = 9;
            this.txtBigSSCode.TabNext = true;
            this.txtBigSSCode.Value = "";
            this.txtBigSSCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtBigSSCode.XAlign = 536;
            // 
            // chkIsFinished
            // 
            this.chkIsFinished.AutoSize = true;
            this.chkIsFinished.Location = new System.Drawing.Point(475, 82);
            this.chkIsFinished.Name = "chkIsFinished";
            this.chkIsFinished.Size = new System.Drawing.Size(60, 16);
            this.chkIsFinished.TabIndex = 8;
            this.chkIsFinished.Text = "已完工";
            this.chkIsFinished.UseVisualStyleBackColor = true;
            // 
            // btnQuiry
            // 
            this.btnQuiry.BackColor = System.Drawing.SystemColors.Control;
            this.btnQuiry.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuiry.BackgroundImage")));
            this.btnQuiry.ButtonType = UserControl.ButtonTypes.Query;
            this.btnQuiry.Caption = "查询";
            this.btnQuiry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuiry.Location = new System.Drawing.Point(581, 80);
            this.btnQuiry.Name = "btnQuiry";
            this.btnQuiry.Size = new System.Drawing.Size(88, 22);
            this.btnQuiry.TabIndex = 7;
            this.btnQuiry.Click += new System.EventHandler(this.btnQuiry_Click);
            // 
            // txtRcardTo
            // 
            this.txtRcardTo.AllowEditOnlyChecked = true;
            this.txtRcardTo.AutoSelectAll = false;
            this.txtRcardTo.AutoUpper = true;
            this.txtRcardTo.Caption = "截止序列号";
            this.txtRcardTo.Checked = false;
            this.txtRcardTo.EditType = UserControl.EditTypes.String;
            this.txtRcardTo.Location = new System.Drawing.Point(235, 80);
            this.txtRcardTo.MaxLength = 40;
            this.txtRcardTo.Multiline = false;
            this.txtRcardTo.Name = "txtRcardTo";
            this.txtRcardTo.PasswordChar = '\0';
            this.txtRcardTo.ReadOnly = false;
            this.txtRcardTo.ShowCheckBox = false;
            this.txtRcardTo.Size = new System.Drawing.Size(206, 24);
            this.txtRcardTo.TabIndex = 6;
            this.txtRcardTo.TabNext = true;
            this.txtRcardTo.Value = "";
            this.txtRcardTo.WidthType = UserControl.WidthTypes.Normal;
            this.txtRcardTo.XAlign = 308;
            // 
            // txtRcardFrom
            // 
            this.txtRcardFrom.AllowEditOnlyChecked = true;
            this.txtRcardFrom.AutoSelectAll = false;
            this.txtRcardFrom.AutoUpper = true;
            this.txtRcardFrom.Caption = "起至序列号";
            this.txtRcardFrom.Checked = false;
            this.txtRcardFrom.EditType = UserControl.EditTypes.String;
            this.txtRcardFrom.Location = new System.Drawing.Point(15, 80);
            this.txtRcardFrom.MaxLength = 40;
            this.txtRcardFrom.Multiline = false;
            this.txtRcardFrom.Name = "txtRcardFrom";
            this.txtRcardFrom.PasswordChar = '\0';
            this.txtRcardFrom.ReadOnly = false;
            this.txtRcardFrom.ShowCheckBox = false;
            this.txtRcardFrom.Size = new System.Drawing.Size(206, 24);
            this.txtRcardFrom.TabIndex = 5;
            this.txtRcardFrom.TabNext = true;
            this.txtRcardFrom.Value = "";
            this.txtRcardFrom.WidthType = UserControl.WidthTypes.Normal;
            this.txtRcardFrom.XAlign = 88;
            // 
            // inINVDateTo
            // 
            this.inINVDateTo.Caption = "入库截止日期";
            this.inINVDateTo.Location = new System.Drawing.Point(223, 50);
            this.inINVDateTo.Name = "inINVDateTo";
            this.inINVDateTo.ShowType = UserControl.DateTimeTypes.Date;
            this.inINVDateTo.Size = new System.Drawing.Size(173, 21);
            this.inINVDateTo.TabIndex = 4;
            this.inINVDateTo.Value = new System.DateTime(2008, 11, 19, 8, 30, 21, 0);
            this.inINVDateTo.XAlign = 308;
            // 
            // inINVdateFrom
            // 
            this.inINVdateFrom.Caption = "入库起至日期";
            this.inINVdateFrom.Location = new System.Drawing.Point(3, 50);
            this.inINVdateFrom.Name = "inINVdateFrom";
            this.inINVdateFrom.ShowType = UserControl.DateTimeTypes.Date;
            this.inINVdateFrom.Size = new System.Drawing.Size(173, 21);
            this.inINVdateFrom.TabIndex = 3;
            this.inINVdateFrom.Value = new System.DateTime(2008, 11, 19, 8, 30, 21, 0);
            this.inINVdateFrom.XAlign = 88;
            // 
            // txtItemDesc
            // 
            this.txtItemDesc.AllowEditOnlyChecked = true;
            this.txtItemDesc.AutoSelectAll = false;
            this.txtItemDesc.AutoUpper = true;
            this.txtItemDesc.Caption = "产品描述";
            this.txtItemDesc.Checked = false;
            this.txtItemDesc.EditType = UserControl.EditTypes.String;
            this.txtItemDesc.Location = new System.Drawing.Point(475, 20);
            this.txtItemDesc.MaxLength = 40;
            this.txtItemDesc.Multiline = false;
            this.txtItemDesc.Name = "txtItemDesc";
            this.txtItemDesc.PasswordChar = '\0';
            this.txtItemDesc.ReadOnly = false;
            this.txtItemDesc.ShowCheckBox = false;
            this.txtItemDesc.Size = new System.Drawing.Size(194, 24);
            this.txtItemDesc.TabIndex = 2;
            this.txtItemDesc.TabNext = true;
            this.txtItemDesc.Value = "";
            this.txtItemDesc.WidthType = UserControl.WidthTypes.Normal;
            this.txtItemDesc.XAlign = 536;
            // 
            // txtBOM
            // 
            this.txtBOM.AllowEditOnlyChecked = true;
            this.txtBOM.AutoSelectAll = false;
            this.txtBOM.AutoUpper = true;
            this.txtBOM.Caption = "BOM版本";
            this.txtBOM.Checked = false;
            this.txtBOM.EditType = UserControl.EditTypes.String;
            this.txtBOM.Location = new System.Drawing.Point(253, 20);
            this.txtBOM.MaxLength = 40;
            this.txtBOM.Multiline = false;
            this.txtBOM.Name = "txtBOM";
            this.txtBOM.PasswordChar = '\0';
            this.txtBOM.ReadOnly = false;
            this.txtBOM.ShowCheckBox = false;
            this.txtBOM.Size = new System.Drawing.Size(188, 24);
            this.txtBOM.TabIndex = 1;
            this.txtBOM.TabNext = true;
            this.txtBOM.Value = "";
            this.txtBOM.WidthType = UserControl.WidthTypes.Normal;
            this.txtBOM.XAlign = 308;
            // 
            // txtModelCode
            // 
            this.txtModelCode.AllowEditOnlyChecked = true;
            this.txtModelCode.AutoSelectAll = false;
            this.txtModelCode.AutoUpper = true;
            this.txtModelCode.Caption = "机型";
            this.txtModelCode.Checked = false;
            this.txtModelCode.EditType = UserControl.EditTypes.String;
            this.txtModelCode.Location = new System.Drawing.Point(51, 20);
            this.txtModelCode.MaxLength = 40;
            this.txtModelCode.Multiline = false;
            this.txtModelCode.Name = "txtModelCode";
            this.txtModelCode.PasswordChar = '\0';
            this.txtModelCode.ReadOnly = false;
            this.txtModelCode.ShowCheckBox = false;
            this.txtModelCode.Size = new System.Drawing.Size(170, 24);
            this.txtModelCode.TabIndex = 0;
            this.txtModelCode.TabNext = true;
            this.txtModelCode.Value = "";
            this.txtModelCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtModelCode.XAlign = 88;
            // 
            // pauseSetting
            // 
            this.pauseSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pauseSetting.Controls.Add(this.txtPauseReason);
            this.pauseSetting.Controls.Add(this.txtPauseCode);
            this.pauseSetting.Controls.Add(this.gridInfo);
            this.pauseSetting.Location = new System.Drawing.Point(0, 117);
            this.pauseSetting.Name = "pauseSetting";
            this.pauseSetting.Size = new System.Drawing.Size(792, 374);
            this.pauseSetting.TabIndex = 0;
            this.pauseSetting.TabStop = false;
            this.pauseSetting.Text = "停发设定";
            // 
            // txtPauseReason
            // 
            this.txtPauseReason.AllowEditOnlyChecked = true;
            this.txtPauseReason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPauseReason.AutoSelectAll = false;
            this.txtPauseReason.AutoUpper = true;
            this.txtPauseReason.Caption = "停发原因";
            this.txtPauseReason.Checked = false;
            this.txtPauseReason.EditType = UserControl.EditTypes.String;
            this.txtPauseReason.Location = new System.Drawing.Point(51, 305);
            this.txtPauseReason.MaxLength = 200;
            this.txtPauseReason.Multiline = true;
            this.txtPauseReason.Name = "txtPauseReason";
            this.txtPauseReason.PasswordChar = '\0';
            this.txtPauseReason.ReadOnly = false;
            this.txtPauseReason.ShowCheckBox = false;
            this.txtPauseReason.Size = new System.Drawing.Size(461, 63);
            this.txtPauseReason.TabIndex = 4;
            this.txtPauseReason.TabNext = true;
            this.txtPauseReason.Value = "";
            this.txtPauseReason.WidthType = UserControl.WidthTypes.TooLong;
            this.txtPauseReason.XAlign = 112;
            // 
            // txtPauseCode
            // 
            this.txtPauseCode.AllowEditOnlyChecked = true;
            this.txtPauseCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPauseCode.AutoSelectAll = false;
            this.txtPauseCode.AutoUpper = true;
            this.txtPauseCode.Caption = "停发通知单";
            this.txtPauseCode.Checked = false;
            this.txtPauseCode.EditType = UserControl.EditTypes.String;
            this.txtPauseCode.Location = new System.Drawing.Point(39, 275);
            this.txtPauseCode.MaxLength = 40;
            this.txtPauseCode.Multiline = false;
            this.txtPauseCode.Name = "txtPauseCode";
            this.txtPauseCode.PasswordChar = '\0';
            this.txtPauseCode.ReadOnly = false;
            this.txtPauseCode.ShowCheckBox = false;
            this.txtPauseCode.Size = new System.Drawing.Size(273, 24);
            this.txtPauseCode.TabIndex = 3;
            this.txtPauseCode.TabNext = true;
            this.txtPauseCode.Value = "";
            this.txtPauseCode.WidthType = UserControl.WidthTypes.Long;
            this.txtPauseCode.XAlign = 112;
            this.txtPauseCode.Leave += new System.EventHandler(this.txtPauseCode_Leave);
            // 
            // gridInfo
            // 
            this.gridInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridInfo.Location = new System.Drawing.Point(3, 17);
            this.gridInfo.Name = "gridInfo";
            this.gridInfo.Size = new System.Drawing.Size(786, 239);
            this.gridInfo.TabIndex = 2;
            this.gridInfo.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridInfo_InitializeLayout);
            this.gridInfo.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.gridInfo_CellChange);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.ButtonType = UserControl.ButtonTypes.Save;
            this.btnSave.Caption = "保存";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(297, 521);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 22);
            this.btnSave.TabIndex = 0;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.btnCancel.Caption = "取消";
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(430, 521);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 22);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FPauseSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pauseSetting);
            this.Controls.Add(this.groupBox1);
            this.Name = "FPauseSetting";
            this.Text = "停发设定";
            this.Load += new System.EventHandler(this.FPauseSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pauseSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox pauseSetting;
        private UserControl.UCButton btnSave;
        private UserControl.UCButton btnCancel;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridInfo;
        private UserControl.UCLabelEdit txtPauseReason;
        private UserControl.UCLabelEdit txtPauseCode;
        private UserControl.UCDatetTime inINVdateFrom;
        private UserControl.UCLabelEdit txtItemDesc;
        private UserControl.UCLabelEdit txtBOM;
        private UserControl.UCLabelEdit txtModelCode;
        private UserControl.UCLabelEdit txtRcardTo;
        private UserControl.UCLabelEdit txtRcardFrom;
        private UserControl.UCDatetTime inINVDateTo;
        private UserControl.UCButton btnQuiry;
        private System.Windows.Forms.CheckBox chkIsFinished;
        private UserControl.UCLabelEdit txtBigSSCode;
    }
}