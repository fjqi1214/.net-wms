namespace BenQGuru.eMES.Client
{
    partial class FOnOffPostByUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FOnOffPostByUser));
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucButtonCancel = new UserControl.UCButton();
            this.ucButtonConfirm = new UserControl.UCButton();
            this.listBoxUserCodeList = new System.Windows.Forms.ListBox();
            this.ucLabelEditUserCodeInput = new UserControl.UCLabelEdit();
            this.ultraOptionSetOnOff = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetOnOff)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox1.Controls.Add(this.ucButtonCancel);
            this.groupBox1.Controls.Add(this.ucButtonConfirm);
            this.groupBox1.Controls.Add(this.listBoxUserCodeList);
            this.groupBox1.Controls.Add(this.ucLabelEditUserCodeInput);
            this.groupBox1.Controls.Add(this.ultraOptionSetOnOff);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(437, 354);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            // 
            // ucButtonCancel
            // 
            this.ucButtonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonCancel.BackgroundImage")));
            this.ucButtonCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.ucButtonCancel.Caption = "取消";
            this.ucButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonCancel.Location = new System.Drawing.Point(161, 256);
            this.ucButtonCancel.Name = "ucButtonCancel";
            this.ucButtonCancel.Size = new System.Drawing.Size(88, 22);
            this.ucButtonCancel.TabIndex = 4;
            this.ucButtonCancel.Click += new System.EventHandler(this.ucButtonCancel_Click);
            // 
            // ucButtonConfirm
            // 
            this.ucButtonConfirm.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonConfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonConfirm.BackgroundImage")));
            this.ucButtonConfirm.ButtonType = UserControl.ButtonTypes.Confirm;
            this.ucButtonConfirm.Caption = "确认";
            this.ucButtonConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonConfirm.Location = new System.Drawing.Point(12, 256);
            this.ucButtonConfirm.Name = "ucButtonConfirm";
            this.ucButtonConfirm.Size = new System.Drawing.Size(88, 22);
            this.ucButtonConfirm.TabIndex = 3;
            this.ucButtonConfirm.Click += new System.EventHandler(this.ucButtonConfirm_Click);
            // 
            // listBoxUserCodeList
            // 
            this.listBoxUserCodeList.FormattingEnabled = true;
            this.listBoxUserCodeList.ItemHeight = 12;
            this.listBoxUserCodeList.Location = new System.Drawing.Point(12, 77);
            this.listBoxUserCodeList.Name = "listBoxUserCodeList";
            this.listBoxUserCodeList.Size = new System.Drawing.Size(237, 160);
            this.listBoxUserCodeList.TabIndex = 2;
            // 
            // ucLabelEditUserCodeInput
            // 
            this.ucLabelEditUserCodeInput.AllowEditOnlyChecked = true;
            this.ucLabelEditUserCodeInput.AutoSelectAll = false;
            this.ucLabelEditUserCodeInput.AutoUpper = true;
            this.ucLabelEditUserCodeInput.Caption = "人员";
            this.ucLabelEditUserCodeInput.Checked = false;
            this.ucLabelEditUserCodeInput.EditType = UserControl.EditTypes.String;
            this.ucLabelEditUserCodeInput.Location = new System.Drawing.Point(12, 47);
            this.ucLabelEditUserCodeInput.MaxLength = 40;
            this.ucLabelEditUserCodeInput.Multiline = false;
            this.ucLabelEditUserCodeInput.Name = "ucLabelEditUserCodeInput";
            this.ucLabelEditUserCodeInput.PasswordChar = '\0';
            this.ucLabelEditUserCodeInput.ReadOnly = false;
            this.ucLabelEditUserCodeInput.ShowCheckBox = false;
            this.ucLabelEditUserCodeInput.Size = new System.Drawing.Size(237, 24);
            this.ucLabelEditUserCodeInput.TabIndex = 1;
            this.ucLabelEditUserCodeInput.TabNext = true;
            this.ucLabelEditUserCodeInput.Value = "";
            this.ucLabelEditUserCodeInput.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditUserCodeInput.XAlign = 49;
            this.ucLabelEditUserCodeInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditUserCodeInput_TxtboxKeyPress);
            // 
            // ultraOptionSetOnOff
            // 
            appearance3.FontData.BoldAsString = "False";
            this.ultraOptionSetOnOff.Appearance = appearance3;
            this.ultraOptionSetOnOff.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.ultraOptionSetOnOff.CausesValidation = false;
            this.ultraOptionSetOnOff.CheckedIndex = 0;
            valueListItem3.DataValue = "On";
            valueListItem3.DisplayText = "上岗";
            valueListItem4.DataValue = "Off";
            valueListItem4.DisplayText = "离岗";
            this.ultraOptionSetOnOff.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem3,
            valueListItem4});
            this.ultraOptionSetOnOff.Location = new System.Drawing.Point(12, 21);
            this.ultraOptionSetOnOff.Name = "ultraOptionSetOnOff";
            this.ultraOptionSetOnOff.Size = new System.Drawing.Size(237, 20);
            this.ultraOptionSetOnOff.TabIndex = 0;
            this.ultraOptionSetOnOff.Text = "上岗";
            this.ultraOptionSetOnOff.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // FOnOffPostByUser
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(437, 354);
            this.Controls.Add(this.groupBox1);
            this.Name = "FOnOffPostByUser";
            this.Text = "人员上岗离岗";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FOnOffPostByUser_Load);
            this.Activated += new System.EventHandler(this.FOnOffPostByUser_Activated);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetOnOff)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCButton ucButtonCancel;
        private UserControl.UCButton ucButtonConfirm;
        private System.Windows.Forms.ListBox listBoxUserCodeList;
        private UserControl.UCLabelEdit ucLabelEditUserCodeInput;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetOnOff;

    }
}