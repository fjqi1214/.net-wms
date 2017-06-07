namespace BenQGuru.eMES.Client
{
    partial class FOQCOffLot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FOQCOffLot));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelItemDescription = new System.Windows.Forms.Label();
            this.ucLabelEditItemCode = new UserControl.UCLabelEdit();
            this.ucButtonGetLot = new UserControl.UCButton();
            this.ucLabelEditRcard = new UserControl.UCLabelEdit();
            this.ucButtonQuery = new UserControl.UCButton();
            this.ucLabelEditOldLotNo = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucButtonSaveLot = new UserControl.UCButton();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonSave = new UserControl.UCButton();
            this.ucLabelEditUnfrozenReason = new UserControl.UCLabelEdit();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rBRCard = new System.Windows.Forms.RadioButton();
            this.radioButtonCartonCode = new System.Windows.Forms.RadioButton();
            this.checkBoxFrozen = new System.Windows.Forms.CheckBox();
            this.ucLabelEditInput = new UserControl.UCLabelEdit();
            this.ucLabEditLotQty = new UserControl.UCLabelEdit();
            this.ucLabEditSelectedCount = new UserControl.UCLabelEdit();
            this.ucLabelEditNewLotNo = new UserControl.UCLabelEdit();
            this.ultraGridRCardList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridRCardList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelItemDescription);
            this.groupBox1.Controls.Add(this.ucLabelEditItemCode);
            this.groupBox1.Controls.Add(this.ucButtonGetLot);
            this.groupBox1.Controls.Add(this.ucLabelEditRcard);
            this.groupBox1.Controls.Add(this.ucButtonQuery);
            this.groupBox1.Controls.Add(this.ucLabelEditOldLotNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(723, 107);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // labelItemDescription
            // 
            this.labelItemDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemDescription.Location = new System.Drawing.Point(287, 75);
            this.labelItemDescription.Name = "labelItemDescription";
            this.labelItemDescription.Size = new System.Drawing.Size(418, 28);
            this.labelItemDescription.TabIndex = 28;
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
            this.ucLabelEditItemCode.Location = new System.Drawing.Point(44, 79);
            this.ucLabelEditItemCode.MaxLength = 40;
            this.ucLabelEditItemCode.Multiline = false;
            this.ucLabelEditItemCode.Name = "ucLabelEditItemCode";
            this.ucLabelEditItemCode.PasswordChar = '\0';
            this.ucLabelEditItemCode.ReadOnly = true;
            this.ucLabelEditItemCode.ShowCheckBox = false;
            this.ucLabelEditItemCode.Size = new System.Drawing.Size(237, 24);
            this.ucLabelEditItemCode.TabIndex = 26;
            this.ucLabelEditItemCode.TabNext = true;
            this.ucLabelEditItemCode.Value = "";
            this.ucLabelEditItemCode.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditItemCode.XAlign = 81;
            // 
            // ucButtonGetLot
            // 
            this.ucButtonGetLot.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonGetLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonGetLot.BackgroundImage")));
            this.ucButtonGetLot.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonGetLot.Caption = "获取批";
            this.ucButtonGetLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonGetLot.Location = new System.Drawing.Point(487, 50);
            this.ucButtonGetLot.Name = "ucButtonGetLot";
            this.ucButtonGetLot.Size = new System.Drawing.Size(88, 22);
            this.ucButtonGetLot.TabIndex = 27;
            this.ucButtonGetLot.Click += new System.EventHandler(this.ucButtonGetLot_Click);
            // 
            // ucLabelEditRcard
            // 
            this.ucLabelEditRcard.AllowEditOnlyChecked = true;
            this.ucLabelEditRcard.AutoSelectAll = false;
            this.ucLabelEditRcard.AutoUpper = true;
            this.ucLabelEditRcard.Caption = "产品序列号";
            this.ucLabelEditRcard.Checked = false;
            this.ucLabelEditRcard.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRcard.Location = new System.Drawing.Point(8, 50);
            this.ucLabelEditRcard.MaxLength = 40;
            this.ucLabelEditRcard.Multiline = false;
            this.ucLabelEditRcard.Name = "ucLabelEditRcard";
            this.ucLabelEditRcard.PasswordChar = '\0';
            this.ucLabelEditRcard.ReadOnly = false;
            this.ucLabelEditRcard.ShowCheckBox = false;
            this.ucLabelEditRcard.Size = new System.Drawing.Size(473, 24);
            this.ucLabelEditRcard.TabIndex = 25;
            this.ucLabelEditRcard.TabNext = true;
            this.ucLabelEditRcard.Value = "";
            this.ucLabelEditRcard.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditRcard.XAlign = 81;
            this.ucLabelEditRcard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditRcard_TxtboxKeyPress);
            // 
            // ucButtonQuery
            // 
            this.ucButtonQuery.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonQuery.BackgroundImage")));
            this.ucButtonQuery.ButtonType = UserControl.ButtonTypes.Query;
            this.ucButtonQuery.Caption = "查询";
            this.ucButtonQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonQuery.Enabled = false;
            this.ucButtonQuery.Location = new System.Drawing.Point(487, 20);
            this.ucButtonQuery.Name = "ucButtonQuery";
            this.ucButtonQuery.Size = new System.Drawing.Size(88, 22);
            this.ucButtonQuery.TabIndex = 7;
            this.ucButtonQuery.Visible = false;
            this.ucButtonQuery.Click += new System.EventHandler(this.ucButtonQuery_Click);
            // 
            // ucLabelEditOldLotNo
            // 
            this.ucLabelEditOldLotNo.AllowEditOnlyChecked = true;
            this.ucLabelEditOldLotNo.AutoSelectAll = false;
            this.ucLabelEditOldLotNo.AutoUpper = true;
            this.ucLabelEditOldLotNo.Caption = "原批号";
            this.ucLabelEditOldLotNo.Checked = false;
            this.ucLabelEditOldLotNo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditOldLotNo.Location = new System.Drawing.Point(32, 20);
            this.ucLabelEditOldLotNo.MaxLength = 40;
            this.ucLabelEditOldLotNo.Multiline = false;
            this.ucLabelEditOldLotNo.Name = "ucLabelEditOldLotNo";
            this.ucLabelEditOldLotNo.PasswordChar = '\0';
            this.ucLabelEditOldLotNo.ReadOnly = false;
            this.ucLabelEditOldLotNo.ShowCheckBox = false;
            this.ucLabelEditOldLotNo.Size = new System.Drawing.Size(449, 24);
            this.ucLabelEditOldLotNo.TabIndex = 0;
            this.ucLabelEditOldLotNo.TabNext = false;
            this.ucLabelEditOldLotNo.Value = "";
            this.ucLabelEditOldLotNo.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditOldLotNo.XAlign = 81;
            this.ucLabelEditOldLotNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditOldLotNo_TxtboxKeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucButtonSaveLot);
            this.groupBox2.Controls.Add(this.ucButtonExit);
            this.groupBox2.Controls.Add(this.ucButtonSave);
            this.groupBox2.Controls.Add(this.ucLabelEditUnfrozenReason);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 393);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(723, 132);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // ucButtonSaveLot
            // 
            this.ucButtonSaveLot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButtonSaveLot.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonSaveLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonSaveLot.BackgroundImage")));
            this.ucButtonSaveLot.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonSaveLot.Caption = "保存批退范围";
            this.ucButtonSaveLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonSaveLot.Location = new System.Drawing.Point(76, 98);
            this.ucButtonSaveLot.Name = "ucButtonSaveLot";
            this.ucButtonSaveLot.Size = new System.Drawing.Size(88, 22);
            this.ucButtonSaveLot.TabIndex = 24;
            this.ucButtonSaveLot.Click += new System.EventHandler(this.ucButtonSaveLot_Click);
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(455, 98);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 23;
            // 
            // ucButtonSave
            // 
            this.ucButtonSave.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonSave.BackgroundImage")));
            this.ucButtonSave.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonSave.Caption = "拆批";
            this.ucButtonSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonSave.Location = new System.Drawing.Point(253, 98);
            this.ucButtonSave.Name = "ucButtonSave";
            this.ucButtonSave.Size = new System.Drawing.Size(88, 22);
            this.ucButtonSave.TabIndex = 21;
            this.ucButtonSave.Click += new System.EventHandler(this.ucButtonSave_Click);
            // 
            // ucLabelEditUnfrozenReason
            // 
            this.ucLabelEditUnfrozenReason.AllowEditOnlyChecked = true;
            this.ucLabelEditUnfrozenReason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditUnfrozenReason.AutoSelectAll = false;
            this.ucLabelEditUnfrozenReason.AutoUpper = true;
            this.ucLabelEditUnfrozenReason.Caption = "取消隔离     原因";
            this.ucLabelEditUnfrozenReason.Checked = false;
            this.ucLabelEditUnfrozenReason.EditType = UserControl.EditTypes.String;
            this.ucLabelEditUnfrozenReason.Location = new System.Drawing.Point(12, 20);
            this.ucLabelEditUnfrozenReason.MaxLength = 100;
            this.ucLabelEditUnfrozenReason.Multiline = true;
            this.ucLabelEditUnfrozenReason.Name = "ucLabelEditUnfrozenReason";
            this.ucLabelEditUnfrozenReason.PasswordChar = '\0';
            this.ucLabelEditUnfrozenReason.ReadOnly = false;
            this.ucLabelEditUnfrozenReason.ShowCheckBox = true;
            this.ucLabelEditUnfrozenReason.Size = new System.Drawing.Size(531, 57);
            this.ucLabelEditUnfrozenReason.TabIndex = 4;
            this.ucLabelEditUnfrozenReason.TabNext = true;
            this.ucLabelEditUnfrozenReason.Value = "";
            this.ucLabelEditUnfrozenReason.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditUnfrozenReason.XAlign = 143;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rBRCard);
            this.groupBox3.Controls.Add(this.radioButtonCartonCode);
            this.groupBox3.Controls.Add(this.checkBoxFrozen);
            this.groupBox3.Controls.Add(this.ucLabelEditInput);
            this.groupBox3.Controls.Add(this.ucLabEditLotQty);
            this.groupBox3.Controls.Add(this.ucLabEditSelectedCount);
            this.groupBox3.Controls.Add(this.ucLabelEditNewLotNo);
            this.groupBox3.Controls.Add(this.ultraGridRCardList);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 107);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(723, 286);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // rBRCard
            // 
            this.rBRCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rBRCard.Location = new System.Drawing.Point(12, 223);
            this.rBRCard.Name = "rBRCard";
            this.rBRCard.Size = new System.Drawing.Size(104, 24);
            this.rBRCard.TabIndex = 165;
            this.rBRCard.Text = "产品序列号";
            this.rBRCard.Click += new System.EventHandler(this.radioButtonRCard_Click);
            // 
            // radioButtonCartonCode
            // 
            this.radioButtonCartonCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonCartonCode.Checked = true;
            this.radioButtonCartonCode.Location = new System.Drawing.Point(131, 223);
            this.radioButtonCartonCode.Name = "radioButtonCartonCode";
            this.radioButtonCartonCode.Size = new System.Drawing.Size(104, 24);
            this.radioButtonCartonCode.TabIndex = 164;
            this.radioButtonCartonCode.TabStop = true;
            this.radioButtonCartonCode.Text = "箱号";
            this.radioButtonCartonCode.Click += new System.EventHandler(this.radioButtonCartonCode_Click);
            // 
            // checkBoxFrozen
            // 
            this.checkBoxFrozen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxFrozen.AutoSize = true;
            this.checkBoxFrozen.Location = new System.Drawing.Point(467, 256);
            this.checkBoxFrozen.Name = "checkBoxFrozen";
            this.checkBoxFrozen.Size = new System.Drawing.Size(60, 16);
            this.checkBoxFrozen.TabIndex = 163;
            this.checkBoxFrozen.Text = "已隔离";
            this.checkBoxFrozen.UseVisualStyleBackColor = true;
            this.checkBoxFrozen.CheckedChanged += new System.EventHandler(this.checkBoxFrozen_CheckedChanged);
            // 
            // ucLabelEditInput
            // 
            this.ucLabelEditInput.AllowEditOnlyChecked = true;
            this.ucLabelEditInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditInput.AutoSelectAll = false;
            this.ucLabelEditInput.AutoUpper = true;
            this.ucLabelEditInput.Caption = "输入框";
            this.ucLabelEditInput.Checked = false;
            this.ucLabelEditInput.EditType = UserControl.EditTypes.String;
            this.ucLabelEditInput.Location = new System.Drawing.Point(12, 253);
            this.ucLabelEditInput.MaxLength = 40;
            this.ucLabelEditInput.Multiline = false;
            this.ucLabelEditInput.Name = "ucLabelEditInput";
            this.ucLabelEditInput.PasswordChar = '\0';
            this.ucLabelEditInput.ReadOnly = false;
            this.ucLabelEditInput.ShowCheckBox = false;
            this.ucLabelEditInput.Size = new System.Drawing.Size(449, 24);
            this.ucLabelEditInput.TabIndex = 3;
            this.ucLabelEditInput.TabNext = true;
            this.ucLabelEditInput.Value = "";
            this.ucLabelEditInput.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditInput.XAlign = 61;
            this.ucLabelEditInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditInput_TxtboxKeyPress);
            // 
            // ucLabEditLotQty
            // 
            this.ucLabEditLotQty.AllowEditOnlyChecked = true;
            this.ucLabEditLotQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLabEditLotQty.AutoSelectAll = false;
            this.ucLabEditLotQty.AutoUpper = true;
            this.ucLabEditLotQty.Caption = "批数量";
            this.ucLabEditLotQty.Checked = false;
            this.ucLabEditLotQty.EditType = UserControl.EditTypes.String;
            this.ucLabEditLotQty.Location = new System.Drawing.Point(606, 214);
            this.ucLabEditLotQty.MaxLength = 40;
            this.ucLabEditLotQty.Multiline = false;
            this.ucLabEditLotQty.Name = "ucLabEditLotQty";
            this.ucLabEditLotQty.PasswordChar = '\0';
            this.ucLabEditLotQty.ReadOnly = true;
            this.ucLabEditLotQty.ShowCheckBox = false;
            this.ucLabEditLotQty.Size = new System.Drawing.Size(99, 24);
            this.ucLabEditLotQty.TabIndex = 159;
            this.ucLabEditLotQty.TabNext = true;
            this.ucLabEditLotQty.Value = "";
            this.ucLabEditLotQty.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabEditLotQty.XAlign = 655;
            // 
            // ucLabEditSelectedCount
            // 
            this.ucLabEditSelectedCount.AllowEditOnlyChecked = true;
            this.ucLabEditSelectedCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLabEditSelectedCount.AutoSelectAll = false;
            this.ucLabEditSelectedCount.AutoUpper = true;
            this.ucLabEditSelectedCount.Caption = "已选数量";
            this.ucLabEditSelectedCount.Checked = false;
            this.ucLabEditSelectedCount.EditType = UserControl.EditTypes.Integer;
            this.ucLabEditSelectedCount.Location = new System.Drawing.Point(489, 214);
            this.ucLabEditSelectedCount.MaxLength = 40;
            this.ucLabEditSelectedCount.Multiline = false;
            this.ucLabEditSelectedCount.Name = "ucLabEditSelectedCount";
            this.ucLabEditSelectedCount.PasswordChar = '\0';
            this.ucLabEditSelectedCount.ReadOnly = true;
            this.ucLabEditSelectedCount.ShowCheckBox = false;
            this.ucLabEditSelectedCount.Size = new System.Drawing.Size(111, 24);
            this.ucLabEditSelectedCount.TabIndex = 160;
            this.ucLabEditSelectedCount.TabNext = true;
            this.ucLabEditSelectedCount.Value = "";
            this.ucLabEditSelectedCount.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabEditSelectedCount.XAlign = 550;
            // 
            // ucLabelEditNewLotNo
            // 
            this.ucLabelEditNewLotNo.AllowEditOnlyChecked = true;
            this.ucLabelEditNewLotNo.AutoSelectAll = false;
            this.ucLabelEditNewLotNo.AutoUpper = true;
            this.ucLabelEditNewLotNo.Caption = "新批号";
            this.ucLabelEditNewLotNo.Checked = false;
            this.ucLabelEditNewLotNo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditNewLotNo.Location = new System.Drawing.Point(12, 19);
            this.ucLabelEditNewLotNo.MaxLength = 40;
            this.ucLabelEditNewLotNo.Multiline = false;
            this.ucLabelEditNewLotNo.Name = "ucLabelEditNewLotNo";
            this.ucLabelEditNewLotNo.PasswordChar = '\0';
            this.ucLabelEditNewLotNo.ReadOnly = true;
            this.ucLabelEditNewLotNo.ShowCheckBox = false;
            this.ucLabelEditNewLotNo.Size = new System.Drawing.Size(449, 24);
            this.ucLabelEditNewLotNo.TabIndex = 1;
            this.ucLabelEditNewLotNo.TabNext = false;
            this.ucLabelEditNewLotNo.Value = "";
            this.ucLabelEditNewLotNo.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditNewLotNo.XAlign = 61;
            // 
            // ultraGridRCardList
            // 
            this.ultraGridRCardList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGridRCardList.Location = new System.Drawing.Point(12, 51);
            this.ultraGridRCardList.Name = "ultraGridRCardList";
            this.ultraGridRCardList.Size = new System.Drawing.Size(693, 160);
            this.ultraGridRCardList.TabIndex = 2;
            this.ultraGridRCardList.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;
            this.ultraGridRCardList.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridRCardList_InitializeLayout);
            this.ultraGridRCardList.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridRCardList_CellChange);
            this.ultraGridRCardList.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.ultraGridRCardList_BeforeRowFilterDropDownPopulate);
            // 
            // FOQCOffLot
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(723, 525);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FOQCOffLot";
            this.Text = "FQC - 拆批";
            this.Load += new System.EventHandler(this.FOQCOffLot_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridRCardList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCButton ucButtonQuery;
        private UserControl.UCLabelEdit ucLabelEditOldLotNo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private UserControl.UCLabelEdit ucLabelEditUnfrozenReason;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridRCardList;
        private UserControl.UCLabelEdit ucLabelEditNewLotNo;
        private UserControl.UCLabelEdit ucLabEditLotQty;
        public UserControl.UCLabelEdit ucLabEditSelectedCount;
        private System.Windows.Forms.CheckBox checkBoxFrozen;
        private UserControl.UCLabelEdit ucLabelEditInput;
        private System.Windows.Forms.RadioButton rBRCard;
        private System.Windows.Forms.RadioButton radioButtonCartonCode;
        private UserControl.UCButton ucButtonSave;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCLabelEdit ucLabelEditItemCode;
        private UserControl.UCButton ucButtonGetLot;
        private UserControl.UCLabelEdit ucLabelEditRcard;
        private System.Windows.Forms.Label labelItemDescription;
        private UserControl.UCButton ucButtonSaveLot;
    }
}