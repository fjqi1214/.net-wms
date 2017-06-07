namespace BenQGuru.eMES.Client
{
    partial class FTryLotNo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTryLotNo));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ultraGridTry = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonOK = new UserControl.UCButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtTryCode = new UserControl.UCLabelEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridTry)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.ultraGridTry);
            this.panel1.Location = new System.Drawing.Point(0, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(530, 299);
            this.panel1.TabIndex = 0;
            // 
            // ultraGridTry
            // 
            this.ultraGridTry.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridTry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridTry.Location = new System.Drawing.Point(0, 0);
            this.ultraGridTry.Name = "ultraGridTry";
            this.ultraGridTry.Size = new System.Drawing.Size(530, 299);
            this.ultraGridTry.TabIndex = 0;
            this.ultraGridTry.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridTry_InitializeLayout);
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "取消";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(301, 372);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 6;           
            // 
            // ucButtonOK
            // 
            this.ucButtonOK.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonOK.BackgroundImage")));
            this.ucButtonOK.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonOK.Caption = "确定";
            this.ucButtonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonOK.Location = new System.Drawing.Point(142, 372);
            this.ucButtonOK.Name = "ucButtonOK";
            this.ucButtonOK.Size = new System.Drawing.Size(88, 22);
            this.ucButtonOK.TabIndex = 5;
            this.ucButtonOK.Click += new System.EventHandler(this.ucButtonOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtTryCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 54);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // TxtTryCode
            // 
            this.TxtTryCode.AllowEditOnlyChecked = true;
            this.TxtTryCode.Caption = "试流单";
            this.TxtTryCode.Checked = false;
            this.TxtTryCode.EditType = UserControl.EditTypes.String;
            this.TxtTryCode.Location = new System.Drawing.Point(6, 20);
            this.TxtTryCode.MaxLength = 40;
            this.TxtTryCode.Multiline = false;
            this.TxtTryCode.Name = "TxtTryCode";
            this.TxtTryCode.PasswordChar = '\0';
            this.TxtTryCode.ReadOnly = false;
            this.TxtTryCode.ShowCheckBox = false;
            this.TxtTryCode.Size = new System.Drawing.Size(249, 24);
            this.TxtTryCode.TabIndex = 2;
            this.TxtTryCode.TabNext = true;
            this.TxtTryCode.Value = "";
            this.TxtTryCode.WidthType = UserControl.WidthTypes.Long;
            this.TxtTryCode.XAlign = 55;
            this.TxtTryCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTryCode_TxtboxKeyPress);
            this.TxtTryCode.InnerTextChanged += new System.EventHandler(this.TxtTryCode_InnerTextChanged);
            // 
            // FTryLotNo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(530, 406);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ucButtonExit);
            this.Controls.Add(this.ucButtonOK);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FTryLotNo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "试流单选择";
            this.Load += new System.EventHandler(this.FTryLotNo_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridTry)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCButton ucButtonOK;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridTry;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCLabelEdit TxtTryCode;
    }
}