namespace BenQGuru.eMES.Client
{
    partial class FChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FChangePassword));
            this.groupPwd = new System.Windows.Forms.GroupBox();
            this.ucNewPwdConfirm = new UserControl.UCLabelEdit();
            this.ucNewPwd = new UserControl.UCLabelEdit();
            this.ucOldPwd = new UserControl.UCLabelEdit();
            this.ucCancel = new UserControl.UCButton();
            this.ucConfirm = new UserControl.UCButton();
            this.groupPwd.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPwd
            // 
            this.groupPwd.Controls.Add(this.ucNewPwdConfirm);
            this.groupPwd.Controls.Add(this.ucNewPwd);
            this.groupPwd.Controls.Add(this.ucOldPwd);
            this.groupPwd.Location = new System.Drawing.Point(45, 23);
            this.groupPwd.Name = "groupPwd";
            this.groupPwd.Size = new System.Drawing.Size(253, 120);
            this.groupPwd.TabIndex = 5;
            this.groupPwd.TabStop = false;
            this.groupPwd.Text = "更改密码";
            // 
            // ucNewPwdConfirm
            // 
            this.ucNewPwdConfirm.AllowEditOnlyChecked = true;
            this.ucNewPwdConfirm.Caption = "确认新密码";
            this.ucNewPwdConfirm.Checked = false;
            this.ucNewPwdConfirm.EditType = UserControl.EditTypes.String;
            this.ucNewPwdConfirm.Location = new System.Drawing.Point(12, 80);
            this.ucNewPwdConfirm.MaxLength = 40;
            this.ucNewPwdConfirm.Multiline = false;
            this.ucNewPwdConfirm.Name = "ucNewPwdConfirm";
            this.ucNewPwdConfirm.PasswordChar = '*';
            this.ucNewPwdConfirm.ReadOnly = false;
            this.ucNewPwdConfirm.ShowCheckBox = false;
            this.ucNewPwdConfirm.Size = new System.Drawing.Size(206, 24);
            this.ucNewPwdConfirm.TabIndex = 6;
            this.ucNewPwdConfirm.TabNext = true;
            this.ucNewPwdConfirm.Value = "";
            this.ucNewPwdConfirm.WidthType = UserControl.WidthTypes.Normal;
            this.ucNewPwdConfirm.XAlign = 85;
            // 
            // ucNewPwd
            // 
            this.ucNewPwd.AllowEditOnlyChecked = true;
            this.ucNewPwd.Caption = "新密码";
            this.ucNewPwd.Checked = false;
            this.ucNewPwd.EditType = UserControl.EditTypes.String;
            this.ucNewPwd.Location = new System.Drawing.Point(36, 50);
            this.ucNewPwd.MaxLength = 40;
            this.ucNewPwd.Multiline = false;
            this.ucNewPwd.Name = "ucNewPwd";
            this.ucNewPwd.PasswordChar = '*';
            this.ucNewPwd.ReadOnly = false;
            this.ucNewPwd.ShowCheckBox = false;
            this.ucNewPwd.Size = new System.Drawing.Size(182, 24);
            this.ucNewPwd.TabIndex = 5;
            this.ucNewPwd.TabNext = true;
            this.ucNewPwd.Value = "";
            this.ucNewPwd.WidthType = UserControl.WidthTypes.Normal;
            this.ucNewPwd.XAlign = 85;
            // 
            // ucOldPwd
            // 
            this.ucOldPwd.AllowEditOnlyChecked = true;
            this.ucOldPwd.Caption = "旧密码";
            this.ucOldPwd.Checked = false;
            this.ucOldPwd.EditType = UserControl.EditTypes.String;
            this.ucOldPwd.Location = new System.Drawing.Point(36, 20);
            this.ucOldPwd.MaxLength = 40;
            this.ucOldPwd.Multiline = false;
            this.ucOldPwd.Name = "ucOldPwd";
            this.ucOldPwd.PasswordChar = '*';
            this.ucOldPwd.ReadOnly = false;
            this.ucOldPwd.ShowCheckBox = false;
            this.ucOldPwd.Size = new System.Drawing.Size(182, 24);
            this.ucOldPwd.TabIndex = 4;
            this.ucOldPwd.TabNext = true;
            this.ucOldPwd.Value = "";
            this.ucOldPwd.WidthType = UserControl.WidthTypes.Normal;
            this.ucOldPwd.XAlign = 85;
            // 
            // ucCancel
            // 
            this.ucCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucCancel.BackgroundImage")));
            this.ucCancel.ButtonType = UserControl.ButtonTypes.None;
            this.ucCancel.Caption = "退出";
            this.ucCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucCancel.Location = new System.Drawing.Point(183, 176);
            this.ucCancel.Name = "ucCancel";
            this.ucCancel.Size = new System.Drawing.Size(88, 22);
            this.ucCancel.TabIndex = 4;
            this.ucCancel.Click += new System.EventHandler(this.ucCancel_Click);
            // 
            // ucConfirm
            // 
            this.ucConfirm.BackColor = System.Drawing.SystemColors.Control;
            this.ucConfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucConfirm.BackgroundImage")));
            this.ucConfirm.ButtonType = UserControl.ButtonTypes.None;
            this.ucConfirm.Caption = "保存";
            this.ucConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucConfirm.Location = new System.Drawing.Point(66, 176);
            this.ucConfirm.Name = "ucConfirm";
            this.ucConfirm.Size = new System.Drawing.Size(88, 22);
            this.ucConfirm.TabIndex = 0;
            this.ucConfirm.Click += new System.EventHandler(this.ucConfirm_Click);
            // 
            // FChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 234);
            this.Controls.Add(this.groupPwd);
            this.Controls.Add(this.ucCancel);
            this.Controls.Add(this.ucConfirm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FChangePassword";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更改密码";
            this.groupPwd.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCButton ucConfirm;
        private UserControl.UCButton ucCancel;
        private System.Windows.Forms.GroupBox groupPwd;
        private UserControl.UCLabelEdit ucNewPwdConfirm;
        private UserControl.UCLabelEdit ucNewPwd;
        private UserControl.UCLabelEdit ucOldPwd;
    }
}