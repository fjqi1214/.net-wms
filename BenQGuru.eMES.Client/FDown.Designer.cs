namespace BenQGuru.eMES.Client
{
	partial class FDown
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FDown));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucLabelEditDown = new UserControl.UCLabelEdit();
            this.ucLabelEditDownCode = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucButtonExit = new UserControl.UCButton();
            this.btnClear = new UserControl.UCButton();
            this.ucLabelEditRcard = new UserControl.UCLabelEdit();
            this.ucLblEditNumber = new UserControl.UCLabelEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ucLabelEditDown);
            this.groupBox1.Controls.Add(this.ucLabelEditDownCode);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(779, 209);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "事件";
            // 
            // ucLabelEditDown
            // 
            this.ucLabelEditDown.AllowEditOnlyChecked = true;
            this.ucLabelEditDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditDown.AutoSelectAll = false;
            this.ucLabelEditDown.AutoUpper = true;
            this.ucLabelEditDown.Caption = "下地原因";
            this.ucLabelEditDown.Checked = false;
            this.ucLabelEditDown.EditType = UserControl.EditTypes.String;
            this.ucLabelEditDown.Location = new System.Drawing.Point(24, 50);
            this.ucLabelEditDown.MaxLength = 100;
            this.ucLabelEditDown.Multiline = true;
            this.ucLabelEditDown.Name = "ucLabelEditDown";
            this.ucLabelEditDown.PasswordChar = '\0';
            this.ucLabelEditDown.ReadOnly = false;
            this.ucLabelEditDown.ShowCheckBox = false;
            this.ucLabelEditDown.Size = new System.Drawing.Size(461, 153);
            this.ucLabelEditDown.TabIndex = 25;
            this.ucLabelEditDown.TabNext = true;
            this.ucLabelEditDown.Value = "";
            this.ucLabelEditDown.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditDown.XAlign = 85;
            this.ucLabelEditDown.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditMemo_TxtboxKeyPress);
            // 
            // ucLabelEditDownCode
            // 
            this.ucLabelEditDownCode.AllowEditOnlyChecked = true;
            this.ucLabelEditDownCode.AutoSelectAll = false;
            this.ucLabelEditDownCode.AutoUpper = true;
            this.ucLabelEditDownCode.Caption = "事件号";
            this.ucLabelEditDownCode.Checked = false;
            this.ucLabelEditDownCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditDownCode.Enabled = false;
            this.ucLabelEditDownCode.Location = new System.Drawing.Point(36, 20);
            this.ucLabelEditDownCode.MaxLength = 40;
            this.ucLabelEditDownCode.Multiline = false;
            this.ucLabelEditDownCode.Name = "ucLabelEditDownCode";
            this.ucLabelEditDownCode.PasswordChar = '\0';
            this.ucLabelEditDownCode.ReadOnly = true;
            this.ucLabelEditDownCode.ShowCheckBox = false;
            this.ucLabelEditDownCode.Size = new System.Drawing.Size(449, 24);
            this.ucLabelEditDownCode.TabIndex = 24;
            this.ucLabelEditDownCode.TabNext = true;
            this.ucLabelEditDownCode.Value = "";
            this.ucLabelEditDownCode.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditDownCode.XAlign = 85;
            this.ucLabelEditDownCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditDownCode_TxtboxKeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ucButtonExit);
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.ucLabelEditRcard);
            this.groupBox2.Controls.Add(this.ucLblEditNumber);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Location = new System.Drawing.Point(0, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(779, 383);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "下地录入";
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(413, 345);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 189;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.BackColor = System.Drawing.SystemColors.Control;
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.ButtonType = UserControl.ButtonTypes.Confirm;
            this.btnClear.Caption = "清空";
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Location = new System.Drawing.Point(278, 345);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 22);
            this.btnClear.TabIndex = 188;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ucLabelEditRcard
            // 
            this.ucLabelEditRcard.AllowEditOnlyChecked = true;
            this.ucLabelEditRcard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditRcard.AutoSelectAll = false;
            this.ucLabelEditRcard.AutoUpper = true;
            this.ucLabelEditRcard.Caption = "序列号";
            this.ucLabelEditRcard.Checked = false;
            this.ucLabelEditRcard.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRcard.Location = new System.Drawing.Point(36, 304);
            this.ucLabelEditRcard.MaxLength = 40;
            this.ucLabelEditRcard.Multiline = false;
            this.ucLabelEditRcard.Name = "ucLabelEditRcard";
            this.ucLabelEditRcard.PasswordChar = '\0';
            this.ucLabelEditRcard.ReadOnly = false;
            this.ucLabelEditRcard.ShowCheckBox = false;
            this.ucLabelEditRcard.Size = new System.Drawing.Size(449, 24);
            this.ucLabelEditRcard.TabIndex = 23;
            this.ucLabelEditRcard.TabNext = true;
            this.ucLabelEditRcard.Value = "";
            this.ucLabelEditRcard.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditRcard.XAlign = 85;
            this.ucLabelEditRcard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditRcard_TxtboxKeyPress);
            // 
            // ucLblEditNumber
            // 
            this.ucLblEditNumber.AllowEditOnlyChecked = true;
            this.ucLblEditNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLblEditNumber.AutoSelectAll = false;
            this.ucLblEditNumber.AutoUpper = true;
            this.ucLblEditNumber.Caption = "数量";
            this.ucLblEditNumber.Checked = false;
            this.ucLblEditNumber.EditType = UserControl.EditTypes.Number;
            this.ucLblEditNumber.Enabled = false;
            this.ucLblEditNumber.Location = new System.Drawing.Point(680, 304);
            this.ucLblEditNumber.MaxLength = 40;
            this.ucLblEditNumber.Multiline = false;
            this.ucLblEditNumber.Name = "ucLblEditNumber";
            this.ucLblEditNumber.PasswordChar = '\0';
            this.ucLblEditNumber.ReadOnly = true;
            this.ucLblEditNumber.ShowCheckBox = false;
            this.ucLblEditNumber.Size = new System.Drawing.Size(87, 24);
            this.ucLblEditNumber.TabIndex = 21;
            this.ucLblEditNumber.TabNext = true;
            this.ucLblEditNumber.Value = "";
            this.ucLblEditNumber.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLblEditNumber.XAlign = 717;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.ultraGridMain);
            this.panel1.Location = new System.Drawing.Point(0, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(767, 278);
            this.panel1.TabIndex = 0;
            // 
            // ultraGridMain
            // 
            this.ultraGridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGridMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridMain.Location = new System.Drawing.Point(12, 0);
            this.ultraGridMain.Name = "ultraGridMain";
            this.ultraGridMain.Size = new System.Drawing.Size(752, 278);
            this.ultraGridMain.TabIndex = 0;
            this.ultraGridMain.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMain_InitializeLayout);
            // 
            // FDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(785, 599);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FDown";
            this.Text = "下地";
            this.Load += new System.EventHandler(this.FDown_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControl.UCLabelEdit ucLabelEditDownCode;
        private UserControl.UCLabelEdit ucLabelEditDown;
        private System.Windows.Forms.Panel panel1;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
        private UserControl.UCLabelEdit ucLblEditNumber;
        private UserControl.UCLabelEdit ucLabelEditRcard;
        private UserControl.UCButton btnClear;
        private UserControl.UCButton ucButtonExit;
	}
}