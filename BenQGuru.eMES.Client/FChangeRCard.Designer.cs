namespace BenQGuru.eMES.Client
{
    partial class FChangeRCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FChangeRCard));
            this.ucLabelEditOldRCard = new UserControl.UCLabelEdit();
            this.ucLabelEditNewRCard = new UserControl.UCLabelEdit();
            this.ucLabelEditReason = new UserControl.UCLabelEdit();
            this.ucButtonOK = new UserControl.UCButton();
            this.ucButtonExit = new UserControl.UCButton();
            this.SuspendLayout();
            // 
            // ucLabelEditOldRCard
            // 
            this.ucLabelEditOldRCard.AllowEditOnlyChecked = true;
            this.ucLabelEditOldRCard.Caption = "老序列号";
            this.ucLabelEditOldRCard.Checked = false;
            this.ucLabelEditOldRCard.EditType = UserControl.EditTypes.String;
            this.ucLabelEditOldRCard.Location = new System.Drawing.Point(12, 12);
            this.ucLabelEditOldRCard.MaxLength = 40;
            this.ucLabelEditOldRCard.Multiline = false;
            this.ucLabelEditOldRCard.Name = "ucLabelEditOldRCard";
            this.ucLabelEditOldRCard.PasswordChar = '\0';
            this.ucLabelEditOldRCard.ReadOnly = false;
            this.ucLabelEditOldRCard.ShowCheckBox = false;
            this.ucLabelEditOldRCard.Size = new System.Drawing.Size(461, 24);
            this.ucLabelEditOldRCard.TabIndex = 0;
            this.ucLabelEditOldRCard.TabNext = true;
            this.ucLabelEditOldRCard.Value = "";
            this.ucLabelEditOldRCard.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditOldRCard.XAlign = 73;
            // 
            // ucLabelEditNewRCard
            // 
            this.ucLabelEditNewRCard.AllowEditOnlyChecked = true;
            this.ucLabelEditNewRCard.Caption = "新序列号";
            this.ucLabelEditNewRCard.Checked = false;
            this.ucLabelEditNewRCard.EditType = UserControl.EditTypes.String;
            this.ucLabelEditNewRCard.Location = new System.Drawing.Point(12, 42);
            this.ucLabelEditNewRCard.MaxLength = 40;
            this.ucLabelEditNewRCard.Multiline = false;
            this.ucLabelEditNewRCard.Name = "ucLabelEditNewRCard";
            this.ucLabelEditNewRCard.PasswordChar = '\0';
            this.ucLabelEditNewRCard.ReadOnly = false;
            this.ucLabelEditNewRCard.ShowCheckBox = false;
            this.ucLabelEditNewRCard.Size = new System.Drawing.Size(461, 24);
            this.ucLabelEditNewRCard.TabIndex = 1;
            this.ucLabelEditNewRCard.TabNext = true;
            this.ucLabelEditNewRCard.Value = "";
            this.ucLabelEditNewRCard.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditNewRCard.XAlign = 73;
            // 
            // ucLabelEditReason
            // 
            this.ucLabelEditReason.AllowEditOnlyChecked = true;
            this.ucLabelEditReason.Caption = "替换原因";
            this.ucLabelEditReason.Checked = false;
            this.ucLabelEditReason.EditType = UserControl.EditTypes.String;
            this.ucLabelEditReason.Location = new System.Drawing.Point(12, 72);
            this.ucLabelEditReason.MaxLength = 200;
            this.ucLabelEditReason.Multiline = true;
            this.ucLabelEditReason.Name = "ucLabelEditReason";
            this.ucLabelEditReason.PasswordChar = '\0';
            this.ucLabelEditReason.ReadOnly = false;
            this.ucLabelEditReason.ShowCheckBox = false;
            this.ucLabelEditReason.Size = new System.Drawing.Size(461, 113);
            this.ucLabelEditReason.TabIndex = 2;
            this.ucLabelEditReason.TabNext = true;
            this.ucLabelEditReason.Value = "";
            this.ucLabelEditReason.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditReason.XAlign = 73;
            // 
            // ucButtonOK
            // 
            this.ucButtonOK.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonOK.BackgroundImage")));
            this.ucButtonOK.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonOK.Caption = "确定";
            this.ucButtonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonOK.Location = new System.Drawing.Point(170, 228);
            this.ucButtonOK.Name = "ucButtonOK";
            this.ucButtonOK.Size = new System.Drawing.Size(88, 22);
            this.ucButtonOK.TabIndex = 3;
            this.ucButtonOK.Click += new System.EventHandler(this.ucButtonOK_Click);
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(294, 228);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 4;
            // 
            // FChangeRCard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(497, 262);
            this.Controls.Add(this.ucButtonExit);
            this.Controls.Add(this.ucButtonOK);
            this.Controls.Add(this.ucLabelEditReason);
            this.Controls.Add(this.ucLabelEditNewRCard);
            this.Controls.Add(this.ucLabelEditOldRCard);
            this.Name = "FChangeRCard";
            this.Text = "替换序列号";
            this.Load += new System.EventHandler(this.FChangeRCard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCLabelEdit ucLabelEditOldRCard;
        private UserControl.UCLabelEdit ucLabelEditNewRCard;
        private UserControl.UCLabelEdit ucLabelEditReason;
        private UserControl.UCButton ucButtonOK;
        private UserControl.UCButton ucButtonExit;
    }
}