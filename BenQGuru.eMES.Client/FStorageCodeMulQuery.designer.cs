namespace BenQGuru.eMES.Client
{
    partial class FStorageCodeMulQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FStorageCodeMulQuery));
            this.ucButtonOK = new UserControl.UCButton();
            this.TxtStorageCode = new UserControl.UCLabelEdit();
            this.ucButtonExit = new UserControl.UCButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ultraGridStorageCode = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtStorageName = new UserControl.UCLabelEdit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridStorageCode)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucButtonOK
            // 
            this.ucButtonOK.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonOK.BackgroundImage")));
            this.ucButtonOK.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonOK.Caption = "确定";
            this.ucButtonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonOK.Location = new System.Drawing.Point(115, 22);
            this.ucButtonOK.Name = "ucButtonOK";
            this.ucButtonOK.Size = new System.Drawing.Size(88, 24);
            this.ucButtonOK.TabIndex = 7;
            this.ucButtonOK.Click += new System.EventHandler(this.ucButtonOK_Click);
            // 
            // TxtStorageCode
            // 
            this.TxtStorageCode.AllowEditOnlyChecked = true;
            this.TxtStorageCode.Caption = "库别代码";
            this.TxtStorageCode.Checked = false;
            this.TxtStorageCode.EditType = UserControl.EditTypes.String;
            this.TxtStorageCode.Location = new System.Drawing.Point(18, 22);
            this.TxtStorageCode.MaxLength = 40;
            this.TxtStorageCode.Multiline = false;
            this.TxtStorageCode.Name = "TxtStorageCode";
            this.TxtStorageCode.PasswordChar = '\0';
            this.TxtStorageCode.ReadOnly = false;
            this.TxtStorageCode.ShowCheckBox = false;
            this.TxtStorageCode.Size = new System.Drawing.Size(194, 26);
            this.TxtStorageCode.TabIndex = 2;
            this.TxtStorageCode.TabNext = true;
            this.TxtStorageCode.Value = "";
            this.TxtStorageCode.WidthType = UserControl.WidthTypes.Normal;
            this.TxtStorageCode.XAlign = 79;
            this.TxtStorageCode.InnerTextChanged += new System.EventHandler(this.TxtStorageCode_InnerTextChanged);
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "取消";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(274, 22);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 24);
            this.ucButtonExit.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ultraGridStorageCode);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 59);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(483, 251);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            // 
            // ultraGridStorageCode
            // 
            this.ultraGridStorageCode.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridStorageCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridStorageCode.Location = new System.Drawing.Point(3, 16);
            this.ultraGridStorageCode.Name = "ultraGridStorageCode";
            this.ultraGridStorageCode.Size = new System.Drawing.Size(477, 232);
            this.ultraGridStorageCode.TabIndex = 0;
            this.ultraGridStorageCode.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridStorageCode_InitializeLayout);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucButtonExit);
            this.groupBox2.Controls.Add(this.ucButtonOK);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 310);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 63);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtStorageName);
            this.groupBox1.Controls.Add(this.TxtStorageCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 59);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // TxtStorageName
            // 
            this.TxtStorageName.AllowEditOnlyChecked = true;
            this.TxtStorageName.Caption = "库别名称";
            this.TxtStorageName.Checked = false;
            this.TxtStorageName.EditType = UserControl.EditTypes.String;
            this.TxtStorageName.Location = new System.Drawing.Point(250, 22);
            this.TxtStorageName.MaxLength = 40;
            this.TxtStorageName.Multiline = false;
            this.TxtStorageName.Name = "TxtStorageName";
            this.TxtStorageName.PasswordChar = '\0';
            this.TxtStorageName.ReadOnly = false;
            this.TxtStorageName.ShowCheckBox = false;
            this.TxtStorageName.Size = new System.Drawing.Size(194, 26);
            this.TxtStorageName.TabIndex = 3;
            this.TxtStorageName.TabNext = true;
            this.TxtStorageName.Value = "";
            this.TxtStorageName.WidthType = UserControl.WidthTypes.Normal;
            this.TxtStorageName.XAlign = 311;
            this.TxtStorageName.InnerTextChanged += new System.EventHandler(this.TxtStorageName_InnerTextChanged);
            // 
            // FStorageCodeMulQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 373);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FStorageCodeMulQuery";
            this.Text = "库别查询";
            this.Load += new System.EventHandler(this.FStorageCodeMulQuery_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridStorageCode)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCButton ucButtonOK;
        private UserControl.UCLabelEdit TxtStorageCode;
        private UserControl.UCButton ucButtonExit;
        private System.Windows.Forms.GroupBox groupBox3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridStorageCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCLabelEdit TxtStorageName;

    }
}