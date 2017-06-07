namespace BenQGuru.eMES.Client
{
    partial class FCompAppendix
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCompAppendix));
            this.ucMessageInfo = new UserControl.UCMessage();
            this.edtInputCarton = new UserControl.UCLabelEdit();
            this.editBarcode = new UserControl.UCLabelEdit();
            this.btnExit = new UserControl.UCButton();
            this.txtCollected = new UserControl.UCLabelEdit();
            this.checkBoxCom = new System.Windows.Forms.CheckBox();
            this.checkBoxApp = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ucMessageInfo
            // 
            this.ucMessageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMessageInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessageInfo.ButtonVisible = false;
            this.ucMessageInfo.Location = new System.Drawing.Point(0, 0);
            this.ucMessageInfo.Name = "ucMessageInfo";
            this.ucMessageInfo.Size = new System.Drawing.Size(618, 253);
            this.ucMessageInfo.TabIndex = 0;
            this.ucMessageInfo.WorkingErrorAdded += new UserControl.WorkingErrorAddedEventHandler(this.ucMessageInfo_WorkingErrorAdded);
            // 
            // edtInputCarton
            // 
            this.edtInputCarton.AllowEditOnlyChecked = false;
            this.edtInputCarton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.edtInputCarton.Caption = "Carton号";
            this.edtInputCarton.Checked = false;
            this.edtInputCarton.EditType = UserControl.EditTypes.String;
            this.edtInputCarton.Location = new System.Drawing.Point(16, 269);
            this.edtInputCarton.MaxLength = 40;
            this.edtInputCarton.Multiline = false;
            this.edtInputCarton.Name = "edtInputCarton";
            this.edtInputCarton.PasswordChar = '\0';
            this.edtInputCarton.ReadOnly = false;
            this.edtInputCarton.ShowCheckBox = false;
            this.edtInputCarton.Size = new System.Drawing.Size(261, 24);
            this.edtInputCarton.TabIndex = 0;
            this.edtInputCarton.TabNext = false;
            this.edtInputCarton.Value = "";
            this.edtInputCarton.WidthType = UserControl.WidthTypes.Long;
            this.edtInputCarton.XAlign = 77;
            this.edtInputCarton.Load += new System.EventHandler(this.edtInputCarton_Load);
            this.edtInputCarton.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtInputCarton_TxtboxKeyPress);
            // 
            // editBarcode
            // 
            this.editBarcode.AllowEditOnlyChecked = false;
            this.editBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editBarcode.Caption = "输入";
            this.editBarcode.Checked = false;
            this.editBarcode.EditType = UserControl.EditTypes.String;
            this.editBarcode.Location = new System.Drawing.Point(40, 299);
            this.editBarcode.MaxLength = 40;
            this.editBarcode.Multiline = false;
            this.editBarcode.Name = "editBarcode";
            this.editBarcode.PasswordChar = '\0';
            this.editBarcode.ReadOnly = false;
            this.editBarcode.ShowCheckBox = false;
            this.editBarcode.Size = new System.Drawing.Size(237, 24);
            this.editBarcode.TabIndex = 1;
            this.editBarcode.TabNext = false;
            this.editBarcode.Value = "";
            this.editBarcode.WidthType = UserControl.WidthTypes.Long;
            this.editBarcode.XAlign = 77;
            this.editBarcode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editBarcode_TxtboxKeyPress);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(524, 269);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 3;
            // 
            // txtCollected
            // 
            this.txtCollected.AllowEditOnlyChecked = true;
            this.txtCollected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCollected.BackColor = System.Drawing.SystemColors.Control;
            this.txtCollected.Caption = "已对比数量";
            this.txtCollected.Checked = false;
            this.txtCollected.EditType = UserControl.EditTypes.Integer;
            this.txtCollected.Enabled = false;
            this.txtCollected.Location = new System.Drawing.Point(292, 269);
            this.txtCollected.MaxLength = 40;
            this.txtCollected.Multiline = false;
            this.txtCollected.Name = "txtCollected";
            this.txtCollected.PasswordChar = '\0';
            this.txtCollected.ReadOnly = true;
            this.txtCollected.ShowCheckBox = false;
            this.txtCollected.Size = new System.Drawing.Size(173, 22);
            this.txtCollected.TabIndex = 2;
            this.txtCollected.TabNext = true;
            this.txtCollected.TabStop = false;
            this.txtCollected.Value = "";
            this.txtCollected.WidthType = UserControl.WidthTypes.Small;
            this.txtCollected.XAlign = 365;
            // 
            // checkBoxCom
            // 
            this.checkBoxCom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxCom.Location = new System.Drawing.Point(292, 299);
            this.checkBoxCom.Name = "checkBoxCom";
            this.checkBoxCom.Size = new System.Drawing.Size(91, 24);
            this.checkBoxCom.TabIndex = 10;
            this.checkBoxCom.Text = "检查商品码";
            // 
            // checkBoxApp
            // 
            this.checkBoxApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxApp.Checked = true;
            this.checkBoxApp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxApp.Location = new System.Drawing.Point(389, 299);
            this.checkBoxApp.Name = "checkBoxApp";
            this.checkBoxApp.Size = new System.Drawing.Size(91, 24);
            this.checkBoxApp.TabIndex = 11;
            this.checkBoxApp.Text = "附件袋检查";
            this.checkBoxApp.Visible = false;
            // 
            // FCompAppendix
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(616, 326);
            this.Controls.Add(this.checkBoxApp);
            this.Controls.Add(this.checkBoxCom);
            this.Controls.Add(this.txtCollected);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.editBarcode);
            this.Controls.Add(this.edtInputCarton);
            this.Controls.Add(this.ucMessageInfo);
            this.Name = "FCompAppendix";
            this.Text = "附件袋比对";
            this.Load += new System.EventHandler(this.FCompAppendix_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCMessage ucMessageInfo;
        public UserControl.UCLabelEdit edtInputCarton;
        public UserControl.UCLabelEdit editBarcode;
        private UserControl.UCButton btnExit;
        private UserControl.UCLabelEdit txtCollected;
        private System.Windows.Forms.CheckBox checkBoxCom;
        private System.Windows.Forms.CheckBox checkBoxApp;
    }
}