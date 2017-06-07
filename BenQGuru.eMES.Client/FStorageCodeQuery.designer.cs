namespace BenQGuru.eMES.Client
{
    partial class FStorageCodeQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FStorageCodeQuery));
            this.TxtBigSSCode = new UserControl.UCLabelEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtStorageName = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonOK = new UserControl.UCButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ultraGridBigSSCode = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridBigSSCode)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtBigSSCode
            // 
            this.TxtBigSSCode.AllowEditOnlyChecked = true;
            this.TxtBigSSCode.Caption = "库别代码";
            this.TxtBigSSCode.Checked = false;
            this.TxtBigSSCode.EditType = UserControl.EditTypes.String;
            this.TxtBigSSCode.Location = new System.Drawing.Point(18, 20);
            this.TxtBigSSCode.MaxLength = 40;
            this.TxtBigSSCode.Multiline = false;
            this.TxtBigSSCode.Name = "TxtBigSSCode";
            this.TxtBigSSCode.PasswordChar = '\0';
            this.TxtBigSSCode.ReadOnly = false;
            this.TxtBigSSCode.ShowCheckBox = false;
            this.TxtBigSSCode.Size = new System.Drawing.Size(194, 24);
            this.TxtBigSSCode.TabIndex = 2;
            this.TxtBigSSCode.TabNext = true;
            this.TxtBigSSCode.Value = "";
            this.TxtBigSSCode.WidthType = UserControl.WidthTypes.Normal;
            this.TxtBigSSCode.XAlign = 79;
            this.TxtBigSSCode.InnerTextChanged += new System.EventHandler(this.TxtBigSSCode_InnerTextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtStorageName);
            this.groupBox1.Controls.Add(this.TxtBigSSCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 54);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // TxtStorageName
            // 
            this.TxtStorageName.AllowEditOnlyChecked = true;
            this.TxtStorageName.Caption = "库别名称";
            this.TxtStorageName.Checked = false;
            this.TxtStorageName.EditType = UserControl.EditTypes.String;
            this.TxtStorageName.Location = new System.Drawing.Point(250, 20);
            this.TxtStorageName.MaxLength = 40;
            this.TxtStorageName.Multiline = false;
            this.TxtStorageName.Name = "TxtStorageName";
            this.TxtStorageName.PasswordChar = '\0';
            this.TxtStorageName.ReadOnly = false;
            this.TxtStorageName.ShowCheckBox = false;
            this.TxtStorageName.Size = new System.Drawing.Size(194, 24);
            this.TxtStorageName.TabIndex = 3;
            this.TxtStorageName.TabNext = true;
            this.TxtStorageName.Value = "";
            this.TxtStorageName.WidthType = UserControl.WidthTypes.Normal;
            this.TxtStorageName.XAlign = 311;
            this.TxtStorageName.InnerTextChanged += new System.EventHandler(this.TxtStorageName_InnerTextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucButtonExit);
            this.groupBox2.Controls.Add(this.ucButtonOK);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 286);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 61);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "取消";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(274, 20);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 8;
            this.ucButtonExit.Click += new System.EventHandler(this.ucButtonExit_Click);
            // 
            // ucButtonOK
            // 
            this.ucButtonOK.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonOK.BackgroundImage")));
            this.ucButtonOK.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonOK.Caption = "确定";
            this.ucButtonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonOK.Location = new System.Drawing.Point(115, 20);
            this.ucButtonOK.Name = "ucButtonOK";
            this.ucButtonOK.Size = new System.Drawing.Size(88, 22);
            this.ucButtonOK.TabIndex = 7;
            this.ucButtonOK.Click += new System.EventHandler(this.ucButtonOK_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ultraGridBigSSCode);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 54);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(483, 232);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // ultraGridBigSSCode
            // 
            this.ultraGridBigSSCode.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridBigSSCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridBigSSCode.Location = new System.Drawing.Point(3, 17);
            this.ultraGridBigSSCode.Name = "ultraGridBigSSCode";
            this.ultraGridBigSSCode.Size = new System.Drawing.Size(477, 212);
            this.ultraGridBigSSCode.TabIndex = 0;
            this.ultraGridBigSSCode.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridBigSSCode_InitializeLayout);
            this.ultraGridBigSSCode.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridBigSSCode_CellChange);
            // 
            // FStorageCodeQuery
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(483, 347);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FStorageCodeQuery";
            this.Text = "库别查询";
            this.Load += new System.EventHandler(this.FStorageCodeQuery_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridBigSSCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCLabelEdit TxtBigSSCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridBigSSCode;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCButton ucButtonOK;
        private UserControl.UCLabelEdit TxtStorageName;
    }
}