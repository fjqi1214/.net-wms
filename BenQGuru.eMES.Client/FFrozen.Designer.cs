namespace BenQGuru.eMES.Client
{
    partial class FFrozen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FFrozen));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelItemDescription = new System.Windows.Forms.Label();
            this.ucLabelEditItemCode = new UserControl.UCLabelEdit();
            this.ucButtonGetLot = new UserControl.UCButton();
            this.ucLSizeAndCapacityMore = new UserControl.UCLabelEdit();
            this.ucLabelEditRcard = new UserControl.UCLabelEdit();
            this.ucLabelEditLotNo = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSave = new UserControl.UCButton();
            this.ucButtonExit = new UserControl.UCButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkBoxForbid = new System.Windows.Forms.CheckBox();
            this.ucLESeparateMemo = new UserControl.UCLabelEdit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelItemDescription);
            this.groupBox1.Controls.Add(this.ucLabelEditItemCode);
            this.groupBox1.Controls.Add(this.ucButtonGetLot);
            this.groupBox1.Controls.Add(this.ucLSizeAndCapacityMore);
            this.groupBox1.Controls.Add(this.ucLabelEditRcard);
            this.groupBox1.Controls.Add(this.ucLabelEditLotNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(712, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // labelItemDescription
            // 
            this.labelItemDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemDescription.Location = new System.Drawing.Point(283, 78);
            this.labelItemDescription.Name = "labelItemDescription";
            this.labelItemDescription.Size = new System.Drawing.Size(340, 28);
            this.labelItemDescription.TabIndex = 25;
            this.labelItemDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucLabelEditItemCode
            // 
            this.ucLabelEditItemCode.AllowEditOnlyChecked = true;
            this.ucLabelEditItemCode.AutoSelectAll = false;
            this.ucLabelEditItemCode.AutoUpper = true;
            this.ucLabelEditItemCode.Caption = "产品";
            this.ucLabelEditItemCode.Checked = false;
            this.ucLabelEditItemCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditItemCode.Location = new System.Drawing.Point(42, 81);
            this.ucLabelEditItemCode.MaxLength = 40;
            this.ucLabelEditItemCode.Multiline = false;
            this.ucLabelEditItemCode.Name = "ucLabelEditItemCode";
            this.ucLabelEditItemCode.PasswordChar = '\0';
            this.ucLabelEditItemCode.ReadOnly = true;
            this.ucLabelEditItemCode.ShowCheckBox = false;
            this.ucLabelEditItemCode.Size = new System.Drawing.Size(237, 24);
            this.ucLabelEditItemCode.TabIndex = 23;
            this.ucLabelEditItemCode.TabNext = true;
            this.ucLabelEditItemCode.Value = "";
            this.ucLabelEditItemCode.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditItemCode.XAlign = 79;
            // 
            // ucButtonGetLot
            // 
            this.ucButtonGetLot.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonGetLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonGetLot.BackgroundImage")));
            this.ucButtonGetLot.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonGetLot.Caption = "获取批";
            this.ucButtonGetLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonGetLot.Location = new System.Drawing.Point(485, 52);
            this.ucButtonGetLot.Name = "ucButtonGetLot";
            this.ucButtonGetLot.Size = new System.Drawing.Size(88, 22);
            this.ucButtonGetLot.TabIndex = 24;
            this.ucButtonGetLot.Click += new System.EventHandler(this.ucButtonGetLot_Click);
            // 
            // ucLSizeAndCapacityMore
            // 
            this.ucLSizeAndCapacityMore.AllowEditOnlyChecked = true;
            this.ucLSizeAndCapacityMore.AutoSelectAll = false;
            this.ucLSizeAndCapacityMore.AutoUpper = true;
            this.ucLSizeAndCapacityMore.Caption = "实际批量/标准批量";
            this.ucLSizeAndCapacityMore.Checked = false;
            this.ucLSizeAndCapacityMore.EditType = UserControl.EditTypes.String;
            this.ucLSizeAndCapacityMore.Enabled = false;
            this.ucLSizeAndCapacityMore.Location = new System.Drawing.Point(485, 22);
            this.ucLSizeAndCapacityMore.MaxLength = 40;
            this.ucLSizeAndCapacityMore.Multiline = false;
            this.ucLSizeAndCapacityMore.Name = "ucLSizeAndCapacityMore";
            this.ucLSizeAndCapacityMore.PasswordChar = '\0';
            this.ucLSizeAndCapacityMore.ReadOnly = true;
            this.ucLSizeAndCapacityMore.ShowCheckBox = false;
            this.ucLSizeAndCapacityMore.Size = new System.Drawing.Size(215, 24);
            this.ucLSizeAndCapacityMore.TabIndex = 20;
            this.ucLSizeAndCapacityMore.TabNext = true;
            this.ucLSizeAndCapacityMore.Value = "";
            this.ucLSizeAndCapacityMore.WidthType = UserControl.WidthTypes.Small;
            this.ucLSizeAndCapacityMore.XAlign = 600;
            // 
            // ucLabelEditRcard
            // 
            this.ucLabelEditRcard.AllowEditOnlyChecked = true;
            this.ucLabelEditRcard.AutoSelectAll = false;
            this.ucLabelEditRcard.AutoUpper = true;
            this.ucLabelEditRcard.Caption = "产品序列号";
            this.ucLabelEditRcard.Checked = false;
            this.ucLabelEditRcard.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRcard.Location = new System.Drawing.Point(6, 52);
            this.ucLabelEditRcard.MaxLength = 40;
            this.ucLabelEditRcard.Multiline = false;
            this.ucLabelEditRcard.Name = "ucLabelEditRcard";
            this.ucLabelEditRcard.PasswordChar = '\0';
            this.ucLabelEditRcard.ReadOnly = false;
            this.ucLabelEditRcard.ShowCheckBox = false;
            this.ucLabelEditRcard.Size = new System.Drawing.Size(473, 24);
            this.ucLabelEditRcard.TabIndex = 22;
            this.ucLabelEditRcard.TabNext = true;
            this.ucLabelEditRcard.Value = "";
            this.ucLabelEditRcard.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditRcard.XAlign = 79;
            this.ucLabelEditRcard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditRcard_TxtboxKeyPress);
            // 
            // ucLabelEditLotNo
            // 
            this.ucLabelEditLotNo.AllowEditOnlyChecked = true;
            this.ucLabelEditLotNo.AutoSelectAll = false;
            this.ucLabelEditLotNo.AutoUpper = true;
            this.ucLabelEditLotNo.Caption = "批号";
            this.ucLabelEditLotNo.Checked = false;
            this.ucLabelEditLotNo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditLotNo.Location = new System.Drawing.Point(42, 22);
            this.ucLabelEditLotNo.MaxLength = 40;
            this.ucLabelEditLotNo.Multiline = false;
            this.ucLabelEditLotNo.Name = "ucLabelEditLotNo";
            this.ucLabelEditLotNo.PasswordChar = '\0';
            this.ucLabelEditLotNo.ReadOnly = false;
            this.ucLabelEditLotNo.ShowCheckBox = false;
            this.ucLabelEditLotNo.Size = new System.Drawing.Size(437, 24);
            this.ucLabelEditLotNo.TabIndex = 21;
            this.ucLabelEditLotNo.TabNext = true;
            this.ucLabelEditLotNo.Value = "";
            this.ucLabelEditLotNo.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditLotNo.XAlign = 79;
            this.ucLabelEditLotNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditLotNo_TxtboxKeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.ucButtonExit);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 451);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(712, 82);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.ButtonType = UserControl.ButtonTypes.Confirm;
            this.btnSave.Caption = "保存";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(160, 35);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 22);
            this.btnSave.TabIndex = 187;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(344, 35);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 185;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkBoxForbid);
            this.groupBox3.Controls.Add(this.ucLESeparateMemo);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 119);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(712, 332);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // chkBoxForbid
            // 
            this.chkBoxForbid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkBoxForbid.AutoSize = true;
            this.chkBoxForbid.Checked = true;
            this.chkBoxForbid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxForbid.Location = new System.Drawing.Point(77, 307);
            this.chkBoxForbid.Name = "chkBoxForbid";
            this.chkBoxForbid.Size = new System.Drawing.Size(120, 16);
            this.chkBoxForbid.TabIndex = 19;
            this.chkBoxForbid.Text = "禁止新序列号进入";
            this.chkBoxForbid.UseVisualStyleBackColor = true;
            // 
            // ucLESeparateMemo
            // 
            this.ucLESeparateMemo.AllowEditOnlyChecked = true;
            this.ucLESeparateMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLESeparateMemo.AutoSelectAll = false;
            this.ucLESeparateMemo.AutoUpper = true;
            this.ucLESeparateMemo.Caption = "隔离原因";
            this.ucLESeparateMemo.Checked = false;
            this.ucLESeparateMemo.EditType = UserControl.EditTypes.String;
            this.ucLESeparateMemo.Location = new System.Drawing.Point(18, 30);
            this.ucLESeparateMemo.MaxLength = 100;
            this.ucLESeparateMemo.Multiline = true;
            this.ucLESeparateMemo.Name = "ucLESeparateMemo";
            this.ucLESeparateMemo.PasswordChar = '\0';
            this.ucLESeparateMemo.ReadOnly = false;
            this.ucLESeparateMemo.ShowCheckBox = false;
            this.ucLESeparateMemo.Size = new System.Drawing.Size(461, 272);
            this.ucLESeparateMemo.TabIndex = 6;
            this.ucLESeparateMemo.TabNext = true;
            this.ucLESeparateMemo.Value = "";
            this.ucLESeparateMemo.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLESeparateMemo.XAlign = 79;
            // 
            // FFrozen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 533);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FFrozen";
            this.Text = "隔离";
            this.Load += new System.EventHandler(this.FFrozen_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private UserControl.UCLabelEdit ucLESeparateMemo;
        private System.Windows.Forms.CheckBox chkBoxForbid;
        private UserControl.UCButton ucButtonExit;
        private System.Windows.Forms.Label labelItemDescription;
        private UserControl.UCLabelEdit ucLabelEditItemCode;
        private UserControl.UCButton ucButtonGetLot;
        private UserControl.UCLabelEdit ucLSizeAndCapacityMore;
        private UserControl.UCLabelEdit ucLabelEditRcard;
        private UserControl.UCLabelEdit ucLabelEditLotNo;
        private UserControl.UCButton btnSave;
    }
}