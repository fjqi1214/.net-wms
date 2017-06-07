namespace BenQGuru.eMES.Client
{
    partial class FTempRework
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
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.Label label1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTempRework));
            this.ultraGridLotList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.rdoConfirmed = new System.Windows.Forms.RadioButton();
            this.rdoNotConfirmed = new System.Windows.Forms.RadioButton();
            this.labelCurrentLot = new System.Windows.Forms.Label();
            this.ucButtonExit = new UserControl.UCButton();
            this.txtTotalNumber = new UserControl.UCLabelEdit();
            this.txtNotSelectnNumber = new UserControl.UCLabelEdit();
            this.txtInput = new UserControl.UCLabelEdit();
            this.rdoBoxNo = new System.Windows.Forms.RadioButton();
            this.rdoRcard = new System.Windows.Forms.RadioButton();
            this.ultraGridReworkRcard = new Infragistics.Win.UltraWinGrid.UltraGrid();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridLotList)).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridReworkRcard)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.ultraGridLotList);
            groupBox1.Controls.Add(this.rdoConfirmed);
            groupBox1.Controls.Add(this.rdoNotConfirmed);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            groupBox1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(790, 251);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "批号列表";
            // 
            // ultraGridLotList
            // 
            this.ultraGridLotList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGridLotList.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridLotList.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.ultraGridLotList.Location = new System.Drawing.Point(3, 44);
            this.ultraGridLotList.Name = "ultraGridLotList";
            this.ultraGridLotList.Size = new System.Drawing.Size(784, 204);
            this.ultraGridLotList.TabIndex = 2;
            this.ultraGridLotList.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridLotList_InitializeLayout);
            this.ultraGridLotList.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.ultraGridLotList_AfterSelectChange);
            // 
            // rdoConfirmed
            // 
            this.rdoConfirmed.AutoSize = true;
            this.rdoConfirmed.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoConfirmed.Location = new System.Drawing.Point(84, 22);
            this.rdoConfirmed.Name = "rdoConfirmed";
            this.rdoConfirmed.Size = new System.Drawing.Size(59, 16);
            this.rdoConfirmed.TabIndex = 1;
            this.rdoConfirmed.TabStop = true;
            this.rdoConfirmed.Text = "已确认";
            this.rdoConfirmed.UseVisualStyleBackColor = true;
            this.rdoConfirmed.Click += new System.EventHandler(this.rdoConfirmed_Click);
            // 
            // rdoNotConfirmed
            // 
            this.rdoNotConfirmed.AutoSize = true;
            this.rdoNotConfirmed.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoNotConfirmed.Location = new System.Drawing.Point(15, 22);
            this.rdoNotConfirmed.Name = "rdoNotConfirmed";
            this.rdoNotConfirmed.Size = new System.Drawing.Size(59, 16);
            this.rdoNotConfirmed.TabIndex = 0;
            this.rdoNotConfirmed.TabStop = true;
            this.rdoNotConfirmed.Text = "未确认";
            this.rdoNotConfirmed.UseVisualStyleBackColor = true;
            this.rdoNotConfirmed.Click += new System.EventHandler(this.rdoNotConfirmed_Click);
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.labelCurrentLot);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(this.ucButtonExit);
            groupBox2.Controls.Add(this.txtTotalNumber);
            groupBox2.Controls.Add(this.txtNotSelectnNumber);
            groupBox2.Controls.Add(this.txtInput);
            groupBox2.Controls.Add(this.rdoBoxNo);
            groupBox2.Controls.Add(this.rdoRcard);
            groupBox2.Controls.Add(this.ultraGridReworkRcard);
            groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox2.Location = new System.Drawing.Point(0, 251);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(790, 423);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            // 
            // labelCurrentLot
            // 
            this.labelCurrentLot.AutoSize = true;
            this.labelCurrentLot.Location = new System.Drawing.Point(110, 17);
            this.labelCurrentLot.Name = "labelCurrentLot";
            this.labelCurrentLot.Size = new System.Drawing.Size(0, 12);
            this.labelCurrentLot.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 17);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(89, 12);
            label1.TabIndex = 7;
            label1.Text = "当前选择批号：";
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(696, 387);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 6;
            // 
            // txtTotalNumber
            // 
            this.txtTotalNumber.AllowEditOnlyChecked = true;
            this.txtTotalNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalNumber.Caption = "总数量";
            this.txtTotalNumber.Checked = false;
            this.txtTotalNumber.EditType = UserControl.EditTypes.String;
            this.txtTotalNumber.Location = new System.Drawing.Point(635, 358);
            this.txtTotalNumber.MaxLength = 40;
            this.txtTotalNumber.Multiline = false;
            this.txtTotalNumber.Name = "txtTotalNumber";
            this.txtTotalNumber.PasswordChar = '\0';
            this.txtTotalNumber.ReadOnly = true;
            this.txtTotalNumber.ShowCheckBox = false;
            this.txtTotalNumber.Size = new System.Drawing.Size(149, 24);
            this.txtTotalNumber.TabIndex = 5;
            this.txtTotalNumber.TabNext = true;
            this.txtTotalNumber.Value = "";
            this.txtTotalNumber.WidthType = UserControl.WidthTypes.Small;
            this.txtTotalNumber.XAlign = 684;
            // 
            // txtNotSelectnNumber
            // 
            this.txtNotSelectnNumber.AllowEditOnlyChecked = true;
            this.txtNotSelectnNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotSelectnNumber.Caption = "未选数量";
            this.txtNotSelectnNumber.Checked = false;
            this.txtNotSelectnNumber.EditType = UserControl.EditTypes.String;
            this.txtNotSelectnNumber.Location = new System.Drawing.Point(456, 358);
            this.txtNotSelectnNumber.MaxLength = 40;
            this.txtNotSelectnNumber.Multiline = false;
            this.txtNotSelectnNumber.Name = "txtNotSelectnNumber";
            this.txtNotSelectnNumber.PasswordChar = '\0';
            this.txtNotSelectnNumber.ReadOnly = true;
            this.txtNotSelectnNumber.ShowCheckBox = false;
            this.txtNotSelectnNumber.Size = new System.Drawing.Size(161, 24);
            this.txtNotSelectnNumber.TabIndex = 4;
            this.txtNotSelectnNumber.TabNext = true;
            this.txtNotSelectnNumber.Value = "";
            this.txtNotSelectnNumber.WidthType = UserControl.WidthTypes.Small;
            this.txtNotSelectnNumber.XAlign = 517;
            // 
            // txtInput
            // 
            this.txtInput.AllowEditOnlyChecked = true;
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInput.Caption = "输入框";
            this.txtInput.Checked = false;
            this.txtInput.EditType = UserControl.EditTypes.String;
            this.txtInput.Location = new System.Drawing.Point(15, 387);
            this.txtInput.MaxLength = 40;
            this.txtInput.Multiline = false;
            this.txtInput.Name = "txtInput";
            this.txtInput.PasswordChar = '\0';
            this.txtInput.ReadOnly = false;
            this.txtInput.ShowCheckBox = false;
            this.txtInput.Size = new System.Drawing.Size(449, 24);
            this.txtInput.TabIndex = 3;
            this.txtInput.TabNext = true;
            this.txtInput.Value = "";
            this.txtInput.WidthType = UserControl.WidthTypes.TooLong;
            this.txtInput.XAlign = 64;
            this.txtInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_TxtboxKeyPress);
            // 
            // rdoBoxNo
            // 
            this.rdoBoxNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoBoxNo.AutoSize = true;
            this.rdoBoxNo.Location = new System.Drawing.Point(104, 361);
            this.rdoBoxNo.Name = "rdoBoxNo";
            this.rdoBoxNo.Size = new System.Drawing.Size(47, 16);
            this.rdoBoxNo.TabIndex = 2;
            this.rdoBoxNo.TabStop = true;
            this.rdoBoxNo.Text = "箱号";
            this.rdoBoxNo.UseVisualStyleBackColor = true;
            // 
            // rdoRcard
            // 
            this.rdoRcard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoRcard.AutoSize = true;
            this.rdoRcard.Location = new System.Drawing.Point(15, 361);
            this.rdoRcard.Name = "rdoRcard";
            this.rdoRcard.Size = new System.Drawing.Size(83, 16);
            this.rdoRcard.TabIndex = 1;
            this.rdoRcard.TabStop = true;
            this.rdoRcard.Text = "产品序列号";
            this.rdoRcard.UseVisualStyleBackColor = true;
            // 
            // ultraGridReworkRcard
            // 
            this.ultraGridReworkRcard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGridReworkRcard.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridReworkRcard.Location = new System.Drawing.Point(3, 38);
            this.ultraGridReworkRcard.Name = "ultraGridReworkRcard";
            this.ultraGridReworkRcard.Size = new System.Drawing.Size(784, 315);
            this.ultraGridReworkRcard.TabIndex = 0;
            this.ultraGridReworkRcard.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridReworkRcard_InitializeLayout);
            // 
            // FTempRework
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(790, 674);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.Name = "FTempRework";
            this.Text = "返工范围确认";
            this.Load += new System.EventHandler(this.FTempRework_Load);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridLotList)).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridReworkRcard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridLotList;
        private System.Windows.Forms.RadioButton rdoConfirmed;
        private System.Windows.Forms.RadioButton rdoNotConfirmed;
        private UserControl.UCLabelEdit txtTotalNumber;
        private UserControl.UCLabelEdit txtNotSelectnNumber;
        private UserControl.UCLabelEdit txtInput;
        private System.Windows.Forms.RadioButton rdoBoxNo;
        private System.Windows.Forms.RadioButton rdoRcard;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridReworkRcard;
        private UserControl.UCButton ucButtonExit;
        private System.Windows.Forms.Label labelCurrentLot;
    }
}