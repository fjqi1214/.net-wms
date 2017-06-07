namespace BenQGuru.eMES.Client
{
    partial class FAuthentication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FAuthentication));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new UserControl.UCButton();
            this.btnOK = new UserControl.UCButton();
            this.txtpwd = new UserControl.UCLabelEdit();
            this.txtusercode = new UserControl.UCLabelEdit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox1.Controls.Add(this.lblInfo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 170);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(99, 96);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 12);
            this.lblInfo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(243, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.ButtonType = UserControl.ButtonTypes.None;
            this.btnCancel.Caption = "取消";
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(172, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 22);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.TabStop = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.ButtonType = UserControl.ButtonTypes.None;
            this.btnOK.Caption = "确定";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Location = new System.Drawing.Point(43, 120);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 22);
            this.btnOK.TabIndex = 26;
            this.btnOK.TabStop = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtpwd
            // 
            this.txtpwd.AllowEditOnlyChecked = true;
            this.txtpwd.AutoSelectAll = false;
            this.txtpwd.AutoUpper = true;
            this.txtpwd.BackColor = System.Drawing.Color.Gainsboro;
            this.txtpwd.Caption = "密码";
            this.txtpwd.Checked = false;
            this.txtpwd.EditType = UserControl.EditTypes.String;
            this.txtpwd.Location = new System.Drawing.Point(66, 65);
            this.txtpwd.MaxLength = 40;
            this.txtpwd.Multiline = false;
            this.txtpwd.Name = "txtpwd";
            this.txtpwd.PasswordChar = '*';
            this.txtpwd.ReadOnly = false;
            this.txtpwd.ShowCheckBox = false;
            this.txtpwd.Size = new System.Drawing.Size(170, 24);
            this.txtpwd.TabIndex = 11;
            this.txtpwd.TabNext = false;
            this.txtpwd.Value = "";
            this.txtpwd.WidthType = UserControl.WidthTypes.Normal;
            this.txtpwd.XAlign = 103;
            this.txtpwd.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtpwd_TxtboxKeyPress);
            // 
            // txtusercode
            // 
            this.txtusercode.AllowEditOnlyChecked = true;
            this.txtusercode.AutoSelectAll = false;
            this.txtusercode.AutoUpper = true;
            this.txtusercode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtusercode.Caption = "用户名";
            this.txtusercode.Checked = false;
            this.txtusercode.EditType = UserControl.EditTypes.String;
            this.txtusercode.Location = new System.Drawing.Point(54, 21);
            this.txtusercode.MaxLength = 40;
            this.txtusercode.Multiline = false;
            this.txtusercode.Name = "txtusercode";
            this.txtusercode.PasswordChar = '\0';
            this.txtusercode.ReadOnly = false;
            this.txtusercode.ShowCheckBox = false;
            this.txtusercode.Size = new System.Drawing.Size(182, 24);
            this.txtusercode.TabIndex = 10;
            this.txtusercode.TabNext = false;
            this.txtusercode.Value = "";
            this.txtusercode.WidthType = UserControl.WidthTypes.Normal;
            this.txtusercode.XAlign = 103;
            this.txtusercode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtusercode_TxtboxKeyPress);
            // 
            // FAuthentication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 170);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtpwd);
            this.Controls.Add(this.txtusercode);
            this.Controls.Add(this.groupBox1);
            this.Name = "FAuthentication";
            this.Text = "用户验证";
            this.Load += new System.EventHandler(this.FAuthentication_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCLabelEdit txtusercode;
        private UserControl.UCLabelEdit txtpwd;
        private UserControl.UCButton btnOK;
        private UserControl.UCButton btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label label1;
    }
}