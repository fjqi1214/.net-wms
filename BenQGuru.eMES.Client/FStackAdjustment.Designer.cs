namespace BenQGuru.eMES.Client
{
    partial class FStackAdjustment
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FStackAdjustment));
            this.toStackInfo = new System.Windows.Forms.GroupBox();
            this.btnGetStack = new System.Windows.Forms.Button();
            this.ucLabelEditStock = new UserControl.UCLabelEdit();
            this.ucLabelComboxINVType = new UserControl.UCLabelCombox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtUseNPallet = new UserControl.UCLabelEdit();
            this.rdoUseNPallet = new System.Windows.Forms.RadioButton();
            this.txtUseTPallet = new UserControl.UCLabelEdit();
            this.rdoUseTPallet = new System.Windows.Forms.RadioButton();
            this.rdoUseOPallet = new System.Windows.Forms.RadioButton();
            this.inputText = new System.Windows.Forms.GroupBox();
            this.txtInput = new UserControl.UCLabelEdit();
            this.packObject = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.txtRecordNum = new UserControl.UCLabelEdit();
            this.gridInfo = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnCancel = new UserControl.UCButton();
            this.toStackInfo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.inputText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // toStackInfo
            // 
            this.toStackInfo.Controls.Add(this.btnGetStack);
            this.toStackInfo.Controls.Add(this.ucLabelEditStock);
            this.toStackInfo.Controls.Add(this.ucLabelComboxINVType);
            this.toStackInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.toStackInfo.Location = new System.Drawing.Point(0, 0);
            this.toStackInfo.Name = "toStackInfo";
            this.toStackInfo.Size = new System.Drawing.Size(792, 53);
            this.toStackInfo.TabIndex = 0;
            this.toStackInfo.TabStop = false;
            this.toStackInfo.Text = "目标库位信息";
            // 
            // btnGetStack
            // 
            this.btnGetStack.Location = new System.Drawing.Point(467, 21);
            this.btnGetStack.Name = "btnGetStack";
            this.btnGetStack.Size = new System.Drawing.Size(34, 23);
            this.btnGetStack.TabIndex = 6;
            this.btnGetStack.Text = "...";
            this.btnGetStack.UseVisualStyleBackColor = true;
            this.btnGetStack.Click += new System.EventHandler(this.btnGetStack_Click);
            // 
            // ucLabelEditStock
            // 
            this.ucLabelEditStock.AllowEditOnlyChecked = true;
            this.ucLabelEditStock.AutoSelectAll = false;
            this.ucLabelEditStock.AutoUpper = true;
            this.ucLabelEditStock.Caption = "垛位";
            this.ucLabelEditStock.Checked = false;
            this.ucLabelEditStock.EditType = UserControl.EditTypes.String;
            this.ucLabelEditStock.Location = new System.Drawing.Point(282, 20);
            this.ucLabelEditStock.MaxLength = 40;
            this.ucLabelEditStock.Multiline = false;
            this.ucLabelEditStock.Name = "ucLabelEditStock";
            this.ucLabelEditStock.PasswordChar = '\0';
            this.ucLabelEditStock.ReadOnly = true;
            this.ucLabelEditStock.ShowCheckBox = false;
            this.ucLabelEditStock.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditStock.TabIndex = 5;
            this.ucLabelEditStock.TabNext = true;
            this.ucLabelEditStock.Value = "";
            this.ucLabelEditStock.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditStock.XAlign = 319;
            // 
            // ucLabelComboxINVType
            // 
            this.ucLabelComboxINVType.AllowEditOnlyChecked = true;
            this.ucLabelComboxINVType.Caption = "库别";
            this.ucLabelComboxINVType.Checked = false;
            this.ucLabelComboxINVType.Location = new System.Drawing.Point(64, 20);
            this.ucLabelComboxINVType.Name = "ucLabelComboxINVType";
            this.ucLabelComboxINVType.SelectedIndex = -1;
            this.ucLabelComboxINVType.ShowCheckBox = false;
            this.ucLabelComboxINVType.Size = new System.Drawing.Size(170, 20);
            this.ucLabelComboxINVType.TabIndex = 4;
            this.ucLabelComboxINVType.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxINVType.XAlign = 101;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtUseNPallet);
            this.groupBox2.Controls.Add(this.rdoUseNPallet);
            this.groupBox2.Controls.Add(this.txtUseTPallet);
            this.groupBox2.Controls.Add(this.rdoUseTPallet);
            this.groupBox2.Controls.Add(this.rdoUseOPallet);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(792, 114);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtUseNPallet
            // 
            this.txtUseNPallet.AllowEditOnlyChecked = true;
            this.txtUseNPallet.AutoSelectAll = false;
            this.txtUseNPallet.AutoUpper = true;
            this.txtUseNPallet.Caption = "";
            this.txtUseNPallet.Checked = false;
            this.txtUseNPallet.EditType = UserControl.EditTypes.String;
            this.txtUseNPallet.Location = new System.Drawing.Point(185, 84);
            this.txtUseNPallet.MaxLength = 40;
            this.txtUseNPallet.Multiline = false;
            this.txtUseNPallet.Name = "txtUseNPallet";
            this.txtUseNPallet.PasswordChar = '\0';
            this.txtUseNPallet.ReadOnly = false;
            this.txtUseNPallet.ShowCheckBox = false;
            this.txtUseNPallet.Size = new System.Drawing.Size(141, 24);
            this.txtUseNPallet.TabIndex = 14;
            this.txtUseNPallet.TabNext = true;
            this.txtUseNPallet.Value = "";
            this.txtUseNPallet.WidthType = UserControl.WidthTypes.Normal;
            this.txtUseNPallet.XAlign = 193;
            this.txtUseNPallet.Leave += new System.EventHandler(this.txtUseNPallet_Leave);
            // 
            // rdoUseNPallet
            // 
            this.rdoUseNPallet.AutoSize = true;
            this.rdoUseNPallet.Location = new System.Drawing.Point(64, 84);
            this.rdoUseNPallet.Name = "rdoUseNPallet";
            this.rdoUseNPallet.Size = new System.Drawing.Size(83, 16);
            this.rdoUseNPallet.TabIndex = 13;
            this.rdoUseNPallet.TabStop = true;
            this.rdoUseNPallet.Text = "使用新栈板";
            this.rdoUseNPallet.UseVisualStyleBackColor = true;
            this.rdoUseNPallet.CheckedChanged += new System.EventHandler(this.rdoUseNPallet_CheckedChanged);
            // 
            // txtUseTPallet
            // 
            this.txtUseTPallet.AllowEditOnlyChecked = true;
            this.txtUseTPallet.AutoSelectAll = false;
            this.txtUseTPallet.AutoUpper = true;
            this.txtUseTPallet.Caption = "";
            this.txtUseTPallet.Checked = false;
            this.txtUseTPallet.EditType = UserControl.EditTypes.String;
            this.txtUseTPallet.Location = new System.Drawing.Point(185, 54);
            this.txtUseTPallet.MaxLength = 40;
            this.txtUseTPallet.Multiline = false;
            this.txtUseTPallet.Name = "txtUseTPallet";
            this.txtUseTPallet.PasswordChar = '\0';
            this.txtUseTPallet.ReadOnly = false;
            this.txtUseTPallet.ShowCheckBox = false;
            this.txtUseTPallet.Size = new System.Drawing.Size(141, 24);
            this.txtUseTPallet.TabIndex = 12;
            this.txtUseTPallet.TabNext = true;
            this.txtUseTPallet.Value = "";
            this.txtUseTPallet.WidthType = UserControl.WidthTypes.Normal;
            this.txtUseTPallet.XAlign = 193;
            this.txtUseTPallet.Leave += new System.EventHandler(this.txtUseTPallet_Leave);
            // 
            // rdoUseTPallet
            // 
            this.rdoUseTPallet.AutoSize = true;
            this.rdoUseTPallet.Location = new System.Drawing.Point(64, 54);
            this.rdoUseTPallet.Name = "rdoUseTPallet";
            this.rdoUseTPallet.Size = new System.Drawing.Size(119, 16);
            this.rdoUseTPallet.TabIndex = 11;
            this.rdoUseTPallet.TabStop = true;
            this.rdoUseTPallet.Text = "使用目标垛位栈板";
            this.rdoUseTPallet.UseVisualStyleBackColor = true;
            this.rdoUseTPallet.CheckedChanged += new System.EventHandler(this.rdoUseTPallet_CheckedChanged);
            // 
            // rdoUseOPallet
            // 
            this.rdoUseOPallet.AutoSize = true;
            this.rdoUseOPallet.Location = new System.Drawing.Point(64, 20);
            this.rdoUseOPallet.Name = "rdoUseOPallet";
            this.rdoUseOPallet.Size = new System.Drawing.Size(83, 16);
            this.rdoUseOPallet.TabIndex = 10;
            this.rdoUseOPallet.TabStop = true;
            this.rdoUseOPallet.Text = "使用原栈板";
            this.rdoUseOPallet.UseVisualStyleBackColor = true;
            this.rdoUseOPallet.CheckedChanged += new System.EventHandler(this.rdoUseOPallet_CheckedChanged);
            // 
            // inputText
            // 
            this.inputText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.inputText.Controls.Add(this.txtInput);
            this.inputText.Controls.Add(this.packObject);
            this.inputText.Controls.Add(this.txtRecordNum);
            this.inputText.Controls.Add(this.gridInfo);
            this.inputText.Location = new System.Drawing.Point(0, 167);
            this.inputText.Name = "inputText";
            this.inputText.Size = new System.Drawing.Size(792, 347);
            this.inputText.TabIndex = 2;
            this.inputText.TabStop = false;
            this.inputText.Text = "输入文本";
            // 
            // txtInput
            // 
            this.txtInput.AllowEditOnlyChecked = true;
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInput.AutoSelectAll = false;
            this.txtInput.AutoUpper = true;
            this.txtInput.Caption = "输入";
            this.txtInput.Checked = false;
            this.txtInput.EditType = UserControl.EditTypes.String;
            this.txtInput.Location = new System.Drawing.Point(44, 315);
            this.txtInput.MaxLength = 40;
            this.txtInput.Multiline = false;
            this.txtInput.Name = "txtInput";
            this.txtInput.PasswordChar = '\0';
            this.txtInput.ReadOnly = false;
            this.txtInput.ShowCheckBox = false;
            this.txtInput.Size = new System.Drawing.Size(237, 24);
            this.txtInput.TabIndex = 9;
            this.txtInput.TabNext = true;
            this.txtInput.Value = "";
            this.txtInput.WidthType = UserControl.WidthTypes.Long;
            this.txtInput.XAlign = 81;
            this.txtInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_TxtboxKeyPress);
            // 
            // packObject
            // 
            this.packObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            appearance1.FontData.BoldAsString = "False";
            this.packObject.Appearance = appearance1;
            this.packObject.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            valueListItem1.DataValue = "0";
            valueListItem1.DisplayText = "栈板";
            valueListItem2.DataValue = "1";
            valueListItem2.DisplayText = "序列号";
            valueListItem3.DataValue = "2";
            valueListItem3.DisplayText = "垛位";
            this.packObject.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2,
            valueListItem3});
            this.packObject.Location = new System.Drawing.Point(44, 284);
            this.packObject.Name = "packObject";
            this.packObject.Size = new System.Drawing.Size(219, 16);
            this.packObject.TabIndex = 8;
            this.packObject.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.packObject.ValueChanged += new System.EventHandler(this.opsetPackObject_ValueChanged);
            // 
            // txtRecordNum
            // 
            this.txtRecordNum.AllowEditOnlyChecked = true;
            this.txtRecordNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecordNum.AutoSelectAll = false;
            this.txtRecordNum.AutoUpper = true;
            this.txtRecordNum.Caption = "数量";
            this.txtRecordNum.Checked = false;
            this.txtRecordNum.EditType = UserControl.EditTypes.String;
            this.txtRecordNum.Location = new System.Drawing.Point(699, 250);
            this.txtRecordNum.MaxLength = 40;
            this.txtRecordNum.Multiline = false;
            this.txtRecordNum.Name = "txtRecordNum";
            this.txtRecordNum.PasswordChar = '\0';
            this.txtRecordNum.ReadOnly = true;
            this.txtRecordNum.ShowCheckBox = false;
            this.txtRecordNum.Size = new System.Drawing.Size(87, 24);
            this.txtRecordNum.TabIndex = 5;
            this.txtRecordNum.TabNext = true;
            this.txtRecordNum.Value = "";
            this.txtRecordNum.WidthType = UserControl.WidthTypes.Tiny;
            this.txtRecordNum.XAlign = 736;
            // 
            // gridInfo
            // 
            this.gridInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridInfo.Location = new System.Drawing.Point(3, 17);
            this.gridInfo.Name = "gridInfo";
            this.gridInfo.Size = new System.Drawing.Size(786, 227);
            this.gridInfo.TabIndex = 0;
            this.gridInfo.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridInfo_InitializeLayout);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.btnCancel.Caption = "取消";
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(364, 524);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FStackAdjustment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.inputText);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toStackInfo);
            this.Name = "FStackAdjustment";
            this.Text = "栈板调整";
            this.Load += new System.EventHandler(this.FStackAdjustment_Load);
            this.toStackInfo.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.inputText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.packObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox toStackInfo;
        private System.Windows.Forms.Button btnGetStack;
        private UserControl.UCLabelEdit ucLabelEditStock;
        private UserControl.UCLabelCombox ucLabelComboxINVType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox inputText;
        private UserControl.UCButton btnCancel;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridInfo;
        private UserControl.UCLabelEdit txtRecordNum;
        private UserControl.UCLabelEdit txtInput;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet packObject;
        private UserControl.UCLabelEdit txtUseNPallet;
        private System.Windows.Forms.RadioButton rdoUseNPallet;
        private UserControl.UCLabelEdit txtUseTPallet;
        private System.Windows.Forms.RadioButton rdoUseTPallet;
        private System.Windows.Forms.RadioButton rdoUseOPallet;
    }
}