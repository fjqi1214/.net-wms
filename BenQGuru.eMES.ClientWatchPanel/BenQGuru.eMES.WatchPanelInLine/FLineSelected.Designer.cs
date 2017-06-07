namespace BenQGuru.eMES.ClientWatchPanel
{
    partial class FLineSelected
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLineSelected));
            this.ucLabelComboxBigLineLevel = new UserControl.UCLabelCombox();
            this.btnOK = new UserControl.UCButton();
            this.uBtnCancel = new UserControl.UCButton();
            this.SuspendLayout();
            // 
            // ucLabelComboxBigLineLevel
            // 
            this.ucLabelComboxBigLineLevel.AllowEditOnlyChecked = true;
            this.ucLabelComboxBigLineLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLabelComboxBigLineLevel.Caption = "大线";
            this.ucLabelComboxBigLineLevel.Checked = false;
            this.ucLabelComboxBigLineLevel.Location = new System.Drawing.Point(43, 23);
            this.ucLabelComboxBigLineLevel.Name = "ucLabelComboxBigLineLevel";
            this.ucLabelComboxBigLineLevel.SelectedIndex = -1;
            this.ucLabelComboxBigLineLevel.ShowCheckBox = false;
            this.ucLabelComboxBigLineLevel.Size = new System.Drawing.Size(172, 20);
            this.ucLabelComboxBigLineLevel.TabIndex = 2;
            this.ucLabelComboxBigLineLevel.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxBigLineLevel.XAlign = 82;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.ButtonType = UserControl.ButtonTypes.None;
            this.btnOK.Caption = "确定";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Location = new System.Drawing.Point(31, 61);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 22);
            this.btnOK.TabIndex = 211;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // uBtnCancel
            // 
            this.uBtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uBtnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.uBtnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uBtnCancel.BackgroundImage")));
            this.uBtnCancel.ButtonType = UserControl.ButtonTypes.Exit;
            this.uBtnCancel.Caption = "取消";
            this.uBtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnCancel.Location = new System.Drawing.Point(152, 61);
            this.uBtnCancel.Name = "uBtnCancel";
            this.uBtnCancel.Size = new System.Drawing.Size(88, 22);
            this.uBtnCancel.TabIndex = 212;
            this.uBtnCancel.Click += new System.EventHandler(this.uBtnCancel_Click);
            // 
            // FLineSelected
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(297, 100);
            this.Controls.Add(this.uBtnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ucLabelComboxBigLineLevel);
            this.MaximizeBox = false;
            this.Name = "FLineSelected";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "大线选择";
            this.Load += new System.EventHandler(this.FLineSelected_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCLabelCombox ucLabelComboxBigLineLevel;
        private UserControl.UCButton btnOK;
        private UserControl.UCButton uBtnCancel;

    }
}