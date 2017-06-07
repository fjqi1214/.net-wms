namespace BenQGuru.eMES.Client
{
    partial class FOQCLotOperate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FOQCLotOperate));
            this.ucLabelEditLotNo = new UserControl.UCLabelEdit();
            this.ucLabelEditRcard = new UserControl.UCLabelEdit();
            this.ucLabelEditSizeAndCapacity = new UserControl.UCLabelEdit();
            this.ucButtonGetLot = new UserControl.UCButton();
            this.ucLabelEditStatusMemo = new UserControl.UCLabelEdit();
            this.ucButtonLotForceReject = new UserControl.UCButton();
            this.ucButtonLotReject = new UserControl.UCButton();
            this.ucButtonLotForcePass = new UserControl.UCButton();
            this.ucButtonLotPass = new UserControl.UCButton();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.checkBoxAutoLot = new System.Windows.Forms.CheckBox();
            this.chkBoxAutoGenerate = new System.Windows.Forms.CheckBox();
            this.ucLabelEditItemCode = new UserControl.UCLabelEdit();
            this.labelItemDescription = new System.Windows.Forms.Label();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucLabelEditCartonCode = new UserControl.UCLabelEdit();
            this.ucLabelEditSampleNgSize = new UserControl.UCLabelEdit();
            this.ucLabelEditSampleGoodSize = new UserControl.UCLabelEdit();
            this.ucLabelEditSampleSize = new UserControl.UCLabelEdit();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucLabelEditLotNo
            // 
            this.ucLabelEditLotNo.AllowEditOnlyChecked = true;
            this.ucLabelEditLotNo.AutoSelectAll = false;
            this.ucLabelEditLotNo.AutoUpper = true;
            this.ucLabelEditLotNo.Caption = "批号";
            this.ucLabelEditLotNo.Checked = false;
            this.ucLabelEditLotNo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditLotNo.Location = new System.Drawing.Point(52, 23);
            this.ucLabelEditLotNo.MaxLength = 40;
            this.ucLabelEditLotNo.Multiline = false;
            this.ucLabelEditLotNo.Name = "ucLabelEditLotNo";
            this.ucLabelEditLotNo.PasswordChar = '\0';
            this.ucLabelEditLotNo.ReadOnly = false;
            this.ucLabelEditLotNo.ShowCheckBox = false;
            this.ucLabelEditLotNo.Size = new System.Drawing.Size(437, 24);
            this.ucLabelEditLotNo.TabIndex = 1;
            this.ucLabelEditLotNo.TabNext = true;
            this.ucLabelEditLotNo.Value = "";
            this.ucLabelEditLotNo.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditLotNo.XAlign = 89;
            this.ucLabelEditLotNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditLotNo_TxtboxKeyPress);
            // 
            // ucLabelEditRcard
            // 
            this.ucLabelEditRcard.AllowEditOnlyChecked = true;
            this.ucLabelEditRcard.AutoSelectAll = false;
            this.ucLabelEditRcard.AutoUpper = true;
            this.ucLabelEditRcard.Caption = "产品序列号";
            this.ucLabelEditRcard.Checked = false;
            this.ucLabelEditRcard.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRcard.Location = new System.Drawing.Point(16, 81);
            this.ucLabelEditRcard.MaxLength = 40;
            this.ucLabelEditRcard.Multiline = false;
            this.ucLabelEditRcard.Name = "ucLabelEditRcard";
            this.ucLabelEditRcard.PasswordChar = '\0';
            this.ucLabelEditRcard.ReadOnly = false;
            this.ucLabelEditRcard.ShowCheckBox = false;
            this.ucLabelEditRcard.Size = new System.Drawing.Size(473, 24);
            this.ucLabelEditRcard.TabIndex = 2;
            this.ucLabelEditRcard.TabNext = true;
            this.ucLabelEditRcard.Value = "";
            this.ucLabelEditRcard.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditRcard.XAlign = 89;
            this.ucLabelEditRcard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditRcard_TxtboxKeyPress);
            // 
            // ucLabelEditSizeAndCapacity
            // 
            this.ucLabelEditSizeAndCapacity.AllowEditOnlyChecked = true;
            this.ucLabelEditSizeAndCapacity.AutoSelectAll = false;
            this.ucLabelEditSizeAndCapacity.AutoUpper = true;
            this.ucLabelEditSizeAndCapacity.Caption = "实际批量";
            this.ucLabelEditSizeAndCapacity.Checked = false;
            this.ucLabelEditSizeAndCapacity.EditType = UserControl.EditTypes.String;
            this.ucLabelEditSizeAndCapacity.Enabled = false;
            this.ucLabelEditSizeAndCapacity.Location = new System.Drawing.Point(515, 140);
            this.ucLabelEditSizeAndCapacity.MaxLength = 40;
            this.ucLabelEditSizeAndCapacity.Multiline = false;
            this.ucLabelEditSizeAndCapacity.Name = "ucLabelEditSizeAndCapacity";
            this.ucLabelEditSizeAndCapacity.PasswordChar = '\0';
            this.ucLabelEditSizeAndCapacity.ReadOnly = true;
            this.ucLabelEditSizeAndCapacity.ShowCheckBox = false;
            this.ucLabelEditSizeAndCapacity.Size = new System.Drawing.Size(161, 24);
            this.ucLabelEditSizeAndCapacity.TabIndex = 0;
            this.ucLabelEditSizeAndCapacity.TabNext = true;
            this.ucLabelEditSizeAndCapacity.Value = "";
            this.ucLabelEditSizeAndCapacity.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditSizeAndCapacity.XAlign = 576;
            // 
            // ucButtonGetLot
            // 
            this.ucButtonGetLot.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonGetLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonGetLot.BackgroundImage")));
            this.ucButtonGetLot.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonGetLot.Caption = "获取批";
            this.ucButtonGetLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonGetLot.Location = new System.Drawing.Point(495, 81);
            this.ucButtonGetLot.Name = "ucButtonGetLot";
            this.ucButtonGetLot.Size = new System.Drawing.Size(88, 22);
            this.ucButtonGetLot.TabIndex = 12;
            this.ucButtonGetLot.Click += new System.EventHandler(this.ucButtonGetLot_Click);
            // 
            // ucLabelEditStatusMemo
            // 
            this.ucLabelEditStatusMemo.AllowEditOnlyChecked = true;
            this.ucLabelEditStatusMemo.AutoSelectAll = false;
            this.ucLabelEditStatusMemo.AutoUpper = true;
            this.ucLabelEditStatusMemo.Caption = "状况说明";
            this.ucLabelEditStatusMemo.Checked = false;
            this.ucLabelEditStatusMemo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditStatusMemo.Location = new System.Drawing.Point(28, 140);
            this.ucLabelEditStatusMemo.MaxLength = 100;
            this.ucLabelEditStatusMemo.Multiline = true;
            this.ucLabelEditStatusMemo.Name = "ucLabelEditStatusMemo";
            this.ucLabelEditStatusMemo.PasswordChar = '\0';
            this.ucLabelEditStatusMemo.ReadOnly = false;
            this.ucLabelEditStatusMemo.ShowCheckBox = false;
            this.ucLabelEditStatusMemo.Size = new System.Drawing.Size(461, 263);
            this.ucLabelEditStatusMemo.TabIndex = 5;
            this.ucLabelEditStatusMemo.TabNext = true;
            this.ucLabelEditStatusMemo.Value = "";
            this.ucLabelEditStatusMemo.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditStatusMemo.XAlign = 89;
            // 
            // ucButtonLotForceReject
            // 
            this.ucButtonLotForceReject.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonLotForceReject.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonLotForceReject.BackgroundImage")));
            this.ucButtonLotForceReject.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonLotForceReject.Caption = "批强制批退";
            this.ucButtonLotForceReject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonLotForceReject.Location = new System.Drawing.Point(124, 20);
            this.ucButtonLotForceReject.Name = "ucButtonLotForceReject";
            this.ucButtonLotForceReject.Size = new System.Drawing.Size(88, 22);
            this.ucButtonLotForceReject.TabIndex = 17;
            this.ucButtonLotForceReject.Click += new System.EventHandler(this.ucButtonLotForceReject_Click);
            // 
            // ucButtonLotReject
            // 
            this.ucButtonLotReject.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonLotReject.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonLotReject.BackgroundImage")));
            this.ucButtonLotReject.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonLotReject.Caption = "批退";
            this.ucButtonLotReject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonLotReject.Location = new System.Drawing.Point(15, 19);
            this.ucButtonLotReject.Name = "ucButtonLotReject";
            this.ucButtonLotReject.Size = new System.Drawing.Size(88, 22);
            this.ucButtonLotReject.TabIndex = 16;
            this.ucButtonLotReject.Click += new System.EventHandler(this.ucButtonLotReject_Click);
            // 
            // ucButtonLotForcePass
            // 
            this.ucButtonLotForcePass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButtonLotForcePass.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonLotForcePass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonLotForcePass.BackgroundImage")));
            this.ucButtonLotForcePass.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonLotForcePass.Caption = "批强制通过";
            this.ucButtonLotForcePass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonLotForcePass.Location = new System.Drawing.Point(210, 408);
            this.ucButtonLotForcePass.Name = "ucButtonLotForcePass";
            this.ucButtonLotForcePass.Size = new System.Drawing.Size(88, 22);
            this.ucButtonLotForcePass.TabIndex = 15;
            this.ucButtonLotForcePass.Click += new System.EventHandler(this.ucButtonLotForcePass_Click);
            // 
            // ucButtonLotPass
            // 
            this.ucButtonLotPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButtonLotPass.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonLotPass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonLotPass.BackgroundImage")));
            this.ucButtonLotPass.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonLotPass.Caption = "批通过";
            this.ucButtonLotPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonLotPass.Location = new System.Drawing.Point(101, 408);
            this.ucButtonLotPass.Name = "ucButtonLotPass";
            this.ucButtonLotPass.Size = new System.Drawing.Size(88, 22);
            this.ucButtonLotPass.TabIndex = 14;
            this.ucButtonLotPass.Click += new System.EventHandler(this.ucButtonLotPass_Click);
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox.Controls.Add(this.checkBoxAutoLot);
            this.groupBox.Controls.Add(this.chkBoxAutoGenerate);
            this.groupBox.Controls.Add(this.ucButtonLotReject);
            this.groupBox.Controls.Add(this.ucButtonLotForceReject);
            this.groupBox.Location = new System.Drawing.Point(86, 436);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(527, 55);
            this.groupBox.TabIndex = 18;
            this.groupBox.TabStop = false;
            // 
            // checkBoxAutoLot
            // 
            this.checkBoxAutoLot.AutoSize = true;
            this.checkBoxAutoLot.Enabled = false;
            this.checkBoxAutoLot.Location = new System.Drawing.Point(357, 25);
            this.checkBoxAutoLot.Name = "checkBoxAutoLot";
            this.checkBoxAutoLot.Size = new System.Drawing.Size(138, 16);
            this.checkBoxAutoLot.TabIndex = 19;
            this.checkBoxAutoLot.Text = "返工投入时,产生批号";
            this.checkBoxAutoLot.UseVisualStyleBackColor = true;
            this.checkBoxAutoLot.Visible = false;
            // 
            // chkBoxAutoGenerate
            // 
            this.chkBoxAutoGenerate.AutoSize = true;
            this.chkBoxAutoGenerate.Checked = true;
            this.chkBoxAutoGenerate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxAutoGenerate.Enabled = false;
            this.chkBoxAutoGenerate.Location = new System.Drawing.Point(218, 25);
            this.chkBoxAutoGenerate.Name = "chkBoxAutoGenerate";
            this.chkBoxAutoGenerate.Size = new System.Drawing.Size(132, 16);
            this.chkBoxAutoGenerate.TabIndex = 18;
            this.chkBoxAutoGenerate.Text = "自动产生返工需求单";
            this.chkBoxAutoGenerate.UseVisualStyleBackColor = true;
            this.chkBoxAutoGenerate.CheckedChanged += new System.EventHandler(this.chkBoxAutoGenerate_CheckedChanged);
            // 
            // ucLabelEditItemCode
            // 
            this.ucLabelEditItemCode.AllowEditOnlyChecked = true;
            this.ucLabelEditItemCode.AutoSelectAll = false;
            this.ucLabelEditItemCode.AutoUpper = true;
            this.ucLabelEditItemCode.Caption = "产品";
            this.ucLabelEditItemCode.Checked = false;
            this.ucLabelEditItemCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditItemCode.Location = new System.Drawing.Point(52, 111);
            this.ucLabelEditItemCode.MaxLength = 40;
            this.ucLabelEditItemCode.Multiline = false;
            this.ucLabelEditItemCode.Name = "ucLabelEditItemCode";
            this.ucLabelEditItemCode.PasswordChar = '\0';
            this.ucLabelEditItemCode.ReadOnly = true;
            this.ucLabelEditItemCode.ShowCheckBox = false;
            this.ucLabelEditItemCode.Size = new System.Drawing.Size(237, 24);
            this.ucLabelEditItemCode.TabIndex = 3;
            this.ucLabelEditItemCode.TabNext = true;
            this.ucLabelEditItemCode.Value = "";
            this.ucLabelEditItemCode.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditItemCode.XAlign = 89;
            // 
            // labelItemDescription
            // 
            this.labelItemDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemDescription.Location = new System.Drawing.Point(293, 108);
            this.labelItemDescription.Name = "labelItemDescription";
            this.labelItemDescription.Size = new System.Drawing.Size(414, 28);
            this.labelItemDescription.TabIndex = 19;
            this.labelItemDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(619, 456);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 20;
            // 
            // ucLabelEditCartonCode
            // 
            this.ucLabelEditCartonCode.AllowEditOnlyChecked = true;
            this.ucLabelEditCartonCode.AutoSelectAll = false;
            this.ucLabelEditCartonCode.AutoUpper = true;
            this.ucLabelEditCartonCode.Caption = "箱号";
            this.ucLabelEditCartonCode.Checked = false;
            this.ucLabelEditCartonCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditCartonCode.Location = new System.Drawing.Point(52, 53);
            this.ucLabelEditCartonCode.MaxLength = 40;
            this.ucLabelEditCartonCode.Multiline = false;
            this.ucLabelEditCartonCode.Name = "ucLabelEditCartonCode";
            this.ucLabelEditCartonCode.PasswordChar = '\0';
            this.ucLabelEditCartonCode.ReadOnly = false;
            this.ucLabelEditCartonCode.ShowCheckBox = false;
            this.ucLabelEditCartonCode.Size = new System.Drawing.Size(437, 23);
            this.ucLabelEditCartonCode.TabIndex = 194;
            this.ucLabelEditCartonCode.TabNext = true;
            this.ucLabelEditCartonCode.Value = "";
            this.ucLabelEditCartonCode.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditCartonCode.XAlign = 89;
            this.ucLabelEditCartonCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditCartonCode_TxtboxKeyPress);
            // 
            // ucLabelEditSampleNgSize
            // 
            this.ucLabelEditSampleNgSize.AllowEditOnlyChecked = true;
            this.ucLabelEditSampleNgSize.AutoSelectAll = false;
            this.ucLabelEditSampleNgSize.AutoUpper = true;
            this.ucLabelEditSampleNgSize.Caption = "不合格样本";
            this.ucLabelEditSampleNgSize.Checked = false;
            this.ucLabelEditSampleNgSize.EditType = UserControl.EditTypes.String;
            this.ucLabelEditSampleNgSize.Enabled = false;
            this.ucLabelEditSampleNgSize.Location = new System.Drawing.Point(503, 236);
            this.ucLabelEditSampleNgSize.MaxLength = 40;
            this.ucLabelEditSampleNgSize.Multiline = false;
            this.ucLabelEditSampleNgSize.Name = "ucLabelEditSampleNgSize";
            this.ucLabelEditSampleNgSize.PasswordChar = '\0';
            this.ucLabelEditSampleNgSize.ReadOnly = false;
            this.ucLabelEditSampleNgSize.ShowCheckBox = false;
            this.ucLabelEditSampleNgSize.Size = new System.Drawing.Size(173, 22);
            this.ucLabelEditSampleNgSize.TabIndex = 197;
            this.ucLabelEditSampleNgSize.TabNext = true;
            this.ucLabelEditSampleNgSize.Value = "";
            this.ucLabelEditSampleNgSize.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditSampleNgSize.XAlign = 576;
            // 
            // ucLabelEditSampleGoodSize
            // 
            this.ucLabelEditSampleGoodSize.AllowEditOnlyChecked = true;
            this.ucLabelEditSampleGoodSize.AutoSelectAll = false;
            this.ucLabelEditSampleGoodSize.AutoUpper = true;
            this.ucLabelEditSampleGoodSize.Caption = "合格样本";
            this.ucLabelEditSampleGoodSize.Checked = false;
            this.ucLabelEditSampleGoodSize.EditType = UserControl.EditTypes.String;
            this.ucLabelEditSampleGoodSize.Enabled = false;
            this.ucLabelEditSampleGoodSize.Location = new System.Drawing.Point(515, 205);
            this.ucLabelEditSampleGoodSize.MaxLength = 40;
            this.ucLabelEditSampleGoodSize.Multiline = false;
            this.ucLabelEditSampleGoodSize.Name = "ucLabelEditSampleGoodSize";
            this.ucLabelEditSampleGoodSize.PasswordChar = '\0';
            this.ucLabelEditSampleGoodSize.ReadOnly = false;
            this.ucLabelEditSampleGoodSize.ShowCheckBox = false;
            this.ucLabelEditSampleGoodSize.Size = new System.Drawing.Size(161, 22);
            this.ucLabelEditSampleGoodSize.TabIndex = 196;
            this.ucLabelEditSampleGoodSize.TabNext = true;
            this.ucLabelEditSampleGoodSize.Value = "";
            this.ucLabelEditSampleGoodSize.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditSampleGoodSize.XAlign = 576;
            // 
            // ucLabelEditSampleSize
            // 
            this.ucLabelEditSampleSize.AllowEditOnlyChecked = true;
            this.ucLabelEditSampleSize.AutoSelectAll = false;
            this.ucLabelEditSampleSize.AutoUpper = true;
            this.ucLabelEditSampleSize.Caption = "实际样本";
            this.ucLabelEditSampleSize.Checked = false;
            this.ucLabelEditSampleSize.EditType = UserControl.EditTypes.String;
            this.ucLabelEditSampleSize.Enabled = false;
            this.ucLabelEditSampleSize.Location = new System.Drawing.Point(515, 174);
            this.ucLabelEditSampleSize.MaxLength = 40;
            this.ucLabelEditSampleSize.Multiline = false;
            this.ucLabelEditSampleSize.Name = "ucLabelEditSampleSize";
            this.ucLabelEditSampleSize.PasswordChar = '\0';
            this.ucLabelEditSampleSize.ReadOnly = false;
            this.ucLabelEditSampleSize.ShowCheckBox = false;
            this.ucLabelEditSampleSize.Size = new System.Drawing.Size(161, 22);
            this.ucLabelEditSampleSize.TabIndex = 195;
            this.ucLabelEditSampleSize.TabNext = true;
            this.ucLabelEditSampleSize.Value = "";
            this.ucLabelEditSampleSize.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditSampleSize.XAlign = 576;
            // 
            // FOQCLotOperate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 503);
            this.Controls.Add(this.ucLabelEditSampleNgSize);
            this.Controls.Add(this.ucLabelEditSampleGoodSize);
            this.Controls.Add(this.ucLabelEditSampleSize);
            this.Controls.Add(this.ucLabelEditCartonCode);
            this.Controls.Add(this.ucButtonExit);
            this.Controls.Add(this.labelItemDescription);
            this.Controls.Add(this.ucLabelEditItemCode);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.ucButtonLotForcePass);
            this.Controls.Add(this.ucButtonLotPass);
            this.Controls.Add(this.ucLabelEditStatusMemo);
            this.Controls.Add(this.ucButtonGetLot);
            this.Controls.Add(this.ucLabelEditSizeAndCapacity);
            this.Controls.Add(this.ucLabelEditRcard);
            this.Controls.Add(this.ucLabelEditLotNo);
            this.Name = "FOQCLotOperate";
            this.Text = "OQC - 批判定";
            this.Load += new System.EventHandler(this.FOQCLotOperate_Load);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCLabelEdit ucLabelEditLotNo;
        private UserControl.UCLabelEdit ucLabelEditRcard;
        private UserControl.UCLabelEdit ucLabelEditSizeAndCapacity;
        private UserControl.UCButton ucButtonGetLot;
        private UserControl.UCLabelEdit ucLabelEditStatusMemo;
        private UserControl.UCButton ucButtonLotForceReject;
        private UserControl.UCButton ucButtonLotReject;
        private UserControl.UCButton ucButtonLotForcePass;
        private UserControl.UCButton ucButtonLotPass;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.CheckBox chkBoxAutoGenerate;
        private UserControl.UCLabelEdit ucLabelEditItemCode;
        private System.Windows.Forms.Label labelItemDescription;
        private UserControl.UCButton ucButtonExit;
        private System.Windows.Forms.CheckBox checkBoxAutoLot;
        private UserControl.UCLabelEdit ucLabelEditCartonCode;
        private UserControl.UCLabelEdit ucLabelEditSampleNgSize;
        private UserControl.UCLabelEdit ucLabelEditSampleGoodSize;
        private UserControl.UCLabelEdit ucLabelEditSampleSize;
    }
}