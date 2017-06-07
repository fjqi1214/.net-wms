namespace BenQGuru.eMES.Client
{
    partial class FFrozenReason
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FFrozenReason));
            this.ucBtnConfirm = new UserControl.UCButton();
            this.ucBtnCancle = new UserControl.UCButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFrozenReason = new UserControl.UCLabelEdit();
            this.SuspendLayout();
            // 
            // ucBtnConfirm
            // 
            this.ucBtnConfirm.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnConfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnConfirm.BackgroundImage")));
            this.ucBtnConfirm.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnConfirm.Caption = "确定";
            this.ucBtnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnConfirm.Location = new System.Drawing.Point(117, 252);
            this.ucBtnConfirm.Name = "ucBtnConfirm";
            this.ucBtnConfirm.Size = new System.Drawing.Size(88, 22);
            this.ucBtnConfirm.TabIndex = 1;
            this.ucBtnConfirm.Click += new System.EventHandler(this.ucBtnConfirm_Click);
            // 
            // ucBtnCancle
            // 
            this.ucBtnCancle.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnCancle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnCancle.BackgroundImage")));
            this.ucBtnCancle.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnCancle.Caption = "取消";
            this.ucBtnCancle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnCancle.Location = new System.Drawing.Point(240, 252);
            this.ucBtnCancle.Name = "ucBtnCancle";
            this.ucBtnCancle.Size = new System.Drawing.Size(88, 22);
            this.ucBtnCancle.TabIndex = 2;
            this.ucBtnCancle.Click += new System.EventHandler(this.ucBtnCancle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 9F);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "取消隔离原因";
            // 
            // txtFrozenReason
            // 
            this.txtFrozenReason.AllowEditOnlyChecked = true;
            this.txtFrozenReason.Caption = "";
            this.txtFrozenReason.Checked = false;
            this.txtFrozenReason.EditType = UserControl.EditTypes.String;
            this.txtFrozenReason.Location = new System.Drawing.Point(6, 30);
            this.txtFrozenReason.MaxLength = 100;
            this.txtFrozenReason.Multiline = true;
            this.txtFrozenReason.Name = "txtFrozenReason";
            this.txtFrozenReason.PasswordChar = '\0';
            this.txtFrozenReason.ReadOnly = false;
            this.txtFrozenReason.ShowCheckBox = false;
            this.txtFrozenReason.Size = new System.Drawing.Size(408, 216);
            this.txtFrozenReason.TabIndex = 4;
            this.txtFrozenReason.TabNext = true;
            this.txtFrozenReason.Value = "";
            this.txtFrozenReason.WidthType = UserControl.WidthTypes.TooLong;
            this.txtFrozenReason.XAlign = 14;
            // 
            // FFrozenReason
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(439, 287);
            this.Controls.Add(this.txtFrozenReason);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucBtnCancle);
            this.Controls.Add(this.ucBtnConfirm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FFrozenReason";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请输入取消隔离原因";
            this.Load += new System.EventHandler(this.FFrozenReason_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControl.UCButton ucBtnConfirm;
        private UserControl.UCButton ucBtnCancle;
        private System.Windows.Forms.Label label1;
        private UserControl.UCLabelEdit txtFrozenReason;
    }
}