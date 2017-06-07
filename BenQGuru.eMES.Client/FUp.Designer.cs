namespace BenQGuru.eMES.Client
{
    partial class FUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FUp));
            this.groupBoxRCard = new System.Windows.Forms.GroupBox();
            this.ucLabelEditRcardQuery = new UserControl.UCLabelEdit();
            this.radioButtonRCard = new System.Windows.Forms.RadioButton();
            this.groupBoxEvent = new System.Windows.Forms.GroupBox();
            this.ultraGridEventList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ucLabelEditEventQuery = new UserControl.UCLabelEdit();
            this.radioButtonEvent = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonClear = new UserControl.UCButton();
            this.ucButtonOK = new UserControl.UCButton();
            this.ucLabelEditUpReason = new UserControl.UCLabelEdit();
            this.ucLabelEditRCardEdit = new UserControl.UCLabelEdit();
            this.ucLabelEditTotal = new UserControl.UCLabelEdit();
            this.ucLabelEditSelected = new UserControl.UCLabelEdit();
            this.checkBoxSelectAll = new System.Windows.Forms.CheckBox();
            this.ultraGridRCardList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBoxRCard.SuspendLayout();
            this.groupBoxEvent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridEventList)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridRCardList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxRCard
            // 
            this.groupBoxRCard.Controls.Add(this.ucLabelEditRcardQuery);
            this.groupBoxRCard.Controls.Add(this.radioButtonRCard);
            this.groupBoxRCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxRCard.Location = new System.Drawing.Point(0, 0);
            this.groupBoxRCard.Name = "groupBoxRCard";
            this.groupBoxRCard.Size = new System.Drawing.Size(778, 48);
            this.groupBoxRCard.TabIndex = 1;
            this.groupBoxRCard.TabStop = false;
            // 
            // ucLabelEditRcardQuery
            // 
            this.ucLabelEditRcardQuery.AllowEditOnlyChecked = true;
            this.ucLabelEditRcardQuery.Caption = "序列号";
            this.ucLabelEditRcardQuery.Checked = false;
            this.ucLabelEditRcardQuery.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRcardQuery.Location = new System.Drawing.Point(33, 18);
            this.ucLabelEditRcardQuery.MaxLength = 40;
            this.ucLabelEditRcardQuery.Multiline = false;
            this.ucLabelEditRcardQuery.Name = "ucLabelEditRcardQuery";
            this.ucLabelEditRcardQuery.PasswordChar = '\0';
            this.ucLabelEditRcardQuery.ReadOnly = false;
            this.ucLabelEditRcardQuery.ShowCheckBox = false;
            this.ucLabelEditRcardQuery.Size = new System.Drawing.Size(449, 24);
            this.ucLabelEditRcardQuery.TabIndex = 1;
            this.ucLabelEditRcardQuery.TabNext = true;
            this.ucLabelEditRcardQuery.Value = "";
            this.ucLabelEditRcardQuery.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditRcardQuery.XAlign = 82;
            this.ucLabelEditRcardQuery.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditRcardQuery_TxtboxKeyPress);
            // 
            // radioButtonRCard
            // 
            this.radioButtonRCard.AutoSize = true;
            this.radioButtonRCard.Location = new System.Drawing.Point(12, 0);
            this.radioButtonRCard.Name = "radioButtonRCard";
            this.radioButtonRCard.Size = new System.Drawing.Size(71, 16);
            this.radioButtonRCard.TabIndex = 0;
            this.radioButtonRCard.Text = "按序列号";
            this.radioButtonRCard.UseVisualStyleBackColor = true;
            this.radioButtonRCard.CheckedChanged += new System.EventHandler(this.radioButtonRCard_CheckedChanged);
            // 
            // groupBoxEvent
            // 
            this.groupBoxEvent.Controls.Add(this.ultraGridEventList);
            this.groupBoxEvent.Controls.Add(this.ucLabelEditEventQuery);
            this.groupBoxEvent.Controls.Add(this.radioButtonEvent);
            this.groupBoxEvent.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxEvent.Location = new System.Drawing.Point(0, 48);
            this.groupBoxEvent.Name = "groupBoxEvent";
            this.groupBoxEvent.Size = new System.Drawing.Size(778, 222);
            this.groupBoxEvent.TabIndex = 0;
            this.groupBoxEvent.TabStop = false;
            // 
            // ultraGridEventList
            // 
            this.ultraGridEventList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGridEventList.Location = new System.Drawing.Point(12, 52);
            this.ultraGridEventList.Name = "ultraGridEventList";
            this.ultraGridEventList.Size = new System.Drawing.Size(754, 164);
            this.ultraGridEventList.TabIndex = 2;
            this.ultraGridEventList.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridEventList_InitializeLayout);
            this.ultraGridEventList.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.ultraGridEventList_AfterSelectChange);
            // 
            // ucLabelEditEventQuery
            // 
            this.ucLabelEditEventQuery.AllowEditOnlyChecked = true;
            this.ucLabelEditEventQuery.Caption = "事件号";
            this.ucLabelEditEventQuery.Checked = false;
            this.ucLabelEditEventQuery.EditType = UserControl.EditTypes.String;
            this.ucLabelEditEventQuery.Location = new System.Drawing.Point(33, 22);
            this.ucLabelEditEventQuery.MaxLength = 40;
            this.ucLabelEditEventQuery.Multiline = false;
            this.ucLabelEditEventQuery.Name = "ucLabelEditEventQuery";
            this.ucLabelEditEventQuery.PasswordChar = '\0';
            this.ucLabelEditEventQuery.ReadOnly = false;
            this.ucLabelEditEventQuery.ShowCheckBox = false;
            this.ucLabelEditEventQuery.Size = new System.Drawing.Size(449, 24);
            this.ucLabelEditEventQuery.TabIndex = 1;
            this.ucLabelEditEventQuery.TabNext = true;
            this.ucLabelEditEventQuery.Value = "";
            this.ucLabelEditEventQuery.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditEventQuery.XAlign = 82;
            this.ucLabelEditEventQuery.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditEventQuery_TxtboxKeyPress);
            // 
            // radioButtonEvent
            // 
            this.radioButtonEvent.AutoSize = true;
            this.radioButtonEvent.Checked = true;
            this.radioButtonEvent.Location = new System.Drawing.Point(12, 0);
            this.radioButtonEvent.Name = "radioButtonEvent";
            this.radioButtonEvent.Size = new System.Drawing.Size(59, 16);
            this.radioButtonEvent.TabIndex = 1;
            this.radioButtonEvent.TabStop = true;
            this.radioButtonEvent.Text = "按事件";
            this.radioButtonEvent.UseVisualStyleBackColor = true;
            this.radioButtonEvent.CheckedChanged += new System.EventHandler(this.radioButtonEvent_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ucButtonExit);
            this.groupBox3.Controls.Add(this.ucButtonClear);
            this.groupBox3.Controls.Add(this.ucButtonOK);
            this.groupBox3.Controls.Add(this.ucLabelEditUpReason);
            this.groupBox3.Controls.Add(this.ucLabelEditRCardEdit);
            this.groupBox3.Controls.Add(this.ucLabelEditTotal);
            this.groupBox3.Controls.Add(this.ucLabelEditSelected);
            this.groupBox3.Controls.Add(this.checkBoxSelectAll);
            this.groupBox3.Controls.Add(this.ultraGridRCardList);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 270);
            this.groupBox3.Name = "GroupBoxDownExcute";
            this.groupBox3.Size = new System.Drawing.Size(778, 298);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "下地机处理";
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(678, 264);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 8;
            // 
            // ucButtonClear
            // 
            this.ucButtonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucButtonClear.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonClear.BackgroundImage")));
            this.ucButtonClear.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonClear.Caption = "清空";
            this.ucButtonClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonClear.Location = new System.Drawing.Point(562, 264);
            this.ucButtonClear.Name = "btnClear";
            this.ucButtonClear.Size = new System.Drawing.Size(88, 22);
            this.ucButtonClear.TabIndex = 7;
            this.ucButtonClear.Click += new System.EventHandler(this.ucButtonClear_Click);
            // 
            // ucButtonOK
            // 
            this.ucButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucButtonOK.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonOK.BackgroundImage")));
            this.ucButtonOK.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonOK.Caption = "确定";
            this.ucButtonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonOK.Location = new System.Drawing.Point(562, 231);
            this.ucButtonOK.Name = "ucButtonOK";
            this.ucButtonOK.Size = new System.Drawing.Size(88, 22);
            this.ucButtonOK.TabIndex = 6;
            this.ucButtonOK.Click += new System.EventHandler(this.ucButtonOK_Click);
            // 
            // ucLabelEditUpReason
            // 
            this.ucLabelEditUpReason.AllowEditOnlyChecked = true;
            this.ucLabelEditUpReason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditUpReason.Caption = "取消下地原因";
            this.ucLabelEditUpReason.Checked = false;
            this.ucLabelEditUpReason.EditType = UserControl.EditTypes.String;
            this.ucLabelEditUpReason.Location = new System.Drawing.Point(20, 231);
            this.ucLabelEditUpReason.MaxLength = 100;
            this.ucLabelEditUpReason.Multiline = true;
            this.ucLabelEditUpReason.Name = "ucLabelEditUpReason";
            this.ucLabelEditUpReason.PasswordChar = '\0';
            this.ucLabelEditUpReason.ReadOnly = false;
            this.ucLabelEditUpReason.ShowCheckBox = false;
            this.ucLabelEditUpReason.Size = new System.Drawing.Size(485, 61);
            this.ucLabelEditUpReason.TabIndex = 5;
            this.ucLabelEditUpReason.TabNext = true;
            this.ucLabelEditUpReason.Value = "";
            this.ucLabelEditUpReason.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditUpReason.XAlign = 105;
            // 
            // ucLabelEditRCardEdit
            // 
            this.ucLabelEditRCardEdit.AllowEditOnlyChecked = true;
            this.ucLabelEditRCardEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditRCardEdit.Caption = "序列号";
            this.ucLabelEditRCardEdit.Checked = false;
            this.ucLabelEditRCardEdit.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRCardEdit.Location = new System.Drawing.Point(56, 201);
            this.ucLabelEditRCardEdit.MaxLength = 40;
            this.ucLabelEditRCardEdit.Multiline = false;
            this.ucLabelEditRCardEdit.Name = "ucLabelEditRCardEdit";
            this.ucLabelEditRCardEdit.PasswordChar = '\0';
            this.ucLabelEditRCardEdit.ReadOnly = false;
            this.ucLabelEditRCardEdit.ShowCheckBox = false;
            this.ucLabelEditRCardEdit.Size = new System.Drawing.Size(449, 24);
            this.ucLabelEditRCardEdit.TabIndex = 4;
            this.ucLabelEditRCardEdit.TabNext = true;
            this.ucLabelEditRCardEdit.Value = "";
            this.ucLabelEditRCardEdit.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditRCardEdit.XAlign = 105;
            this.ucLabelEditRCardEdit.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditRCardEdit_TxtboxKeyPress);
            // 
            // ucLabelEditTotal
            // 
            this.ucLabelEditTotal.AllowEditOnlyChecked = true;
            this.ucLabelEditTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLabelEditTotal.Caption = "总数";
            this.ucLabelEditTotal.Checked = false;
            this.ucLabelEditTotal.EditType = UserControl.EditTypes.String;
            this.ucLabelEditTotal.Location = new System.Drawing.Point(679, 175);
            this.ucLabelEditTotal.MaxLength = 40;
            this.ucLabelEditTotal.Multiline = false;
            this.ucLabelEditTotal.Name = "ucLabelEditTotal";
            this.ucLabelEditTotal.PasswordChar = '\0';
            this.ucLabelEditTotal.ReadOnly = true;
            this.ucLabelEditTotal.ShowCheckBox = false;
            this.ucLabelEditTotal.Size = new System.Drawing.Size(87, 24);
            this.ucLabelEditTotal.TabIndex = 3;
            this.ucLabelEditTotal.TabNext = true;
            this.ucLabelEditTotal.Value = "";
            this.ucLabelEditTotal.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditTotal.XAlign = 716;
            // 
            // ucLabelEditSelected
            // 
            this.ucLabelEditSelected.AllowEditOnlyChecked = true;
            this.ucLabelEditSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLabelEditSelected.Caption = "已选数量";
            this.ucLabelEditSelected.Checked = false;
            this.ucLabelEditSelected.EditType = UserControl.EditTypes.String;
            this.ucLabelEditSelected.Location = new System.Drawing.Point(562, 175);
            this.ucLabelEditSelected.MaxLength = 40;
            this.ucLabelEditSelected.Multiline = false;
            this.ucLabelEditSelected.Name = "ucLabelEditSelected";
            this.ucLabelEditSelected.PasswordChar = '\0';
            this.ucLabelEditSelected.ReadOnly = true;
            this.ucLabelEditSelected.ShowCheckBox = false;
            this.ucLabelEditSelected.Size = new System.Drawing.Size(111, 24);
            this.ucLabelEditSelected.TabIndex = 2;
            this.ucLabelEditSelected.TabNext = true;
            this.ucLabelEditSelected.Value = "";
            this.ucLabelEditSelected.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditSelected.XAlign = 623;
            // 
            // checkBoxSelectAll
            // 
            this.checkBoxSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxSelectAll.AutoSize = true;
            this.checkBoxSelectAll.Location = new System.Drawing.Point(12, 179);
            this.checkBoxSelectAll.Name = "checkBoxSelectAll";
            this.checkBoxSelectAll.Size = new System.Drawing.Size(49, 16);
            this.checkBoxSelectAll.TabIndex = 1;
            this.checkBoxSelectAll.Text = "全选";
            this.checkBoxSelectAll.UseVisualStyleBackColor = true;
            this.checkBoxSelectAll.CheckedChanged += new System.EventHandler(this.checkBoxSelectAll_CheckedChanged);
            // 
            // ultraGridRCardList
            // 
            this.ultraGridRCardList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGridRCardList.Location = new System.Drawing.Point(12, 20);
            this.ultraGridRCardList.Name = "ultraGridRCardList";
            this.ultraGridRCardList.Size = new System.Drawing.Size(754, 153);
            this.ultraGridRCardList.TabIndex = 0;
            this.ultraGridRCardList.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridRCardList_InitializeLayout);
            this.ultraGridRCardList.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridRCardList_CellChange);
            // 
            // FUp
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(778, 568);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBoxEvent);
            this.Controls.Add(this.groupBoxRCard);
            this.Name = "FUp";
            this.Text = "取消下地";
            this.Load += new System.EventHandler(this.FUp_Load);
            this.groupBoxRCard.ResumeLayout(false);
            this.groupBoxRCard.PerformLayout();
            this.groupBoxEvent.ResumeLayout(false);
            this.groupBoxEvent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridEventList)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridRCardList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxEvent;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonRCard;
        private System.Windows.Forms.GroupBox groupBoxRCard;
        private System.Windows.Forms.RadioButton radioButtonEvent;
        private UserControl.UCLabelEdit ucLabelEditRcardQuery;
        private UserControl.UCLabelEdit ucLabelEditEventQuery;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridEventList;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridRCardList;
        private System.Windows.Forms.CheckBox checkBoxSelectAll;
        private UserControl.UCLabelEdit ucLabelEditTotal;
        private UserControl.UCLabelEdit ucLabelEditSelected;
        private UserControl.UCLabelEdit ucLabelEditRCardEdit;
        private UserControl.UCLabelEdit ucLabelEditUpReason;
        private UserControl.UCButton ucButtonClear;
        private UserControl.UCButton ucButtonOK;
        private UserControl.UCButton ucButtonExit;
    }
}