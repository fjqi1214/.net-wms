namespace BenQGuru.eMES.Client
{
    partial class FBigSSCodeQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FBigSSCodeQuery));
            this.TxtBigSSCode = new UserControl.UCLabelEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.TxtBigSSCode.Caption = "大线";
            this.TxtBigSSCode.Checked = false;
            this.TxtBigSSCode.EditType = UserControl.EditTypes.String;
            this.TxtBigSSCode.Location = new System.Drawing.Point(18, 22);
            this.TxtBigSSCode.MaxLength = 40;
            this.TxtBigSSCode.Multiline = false;
            this.TxtBigSSCode.Name = "TxtBigSSCode";
            this.TxtBigSSCode.PasswordChar = '\0';
            this.TxtBigSSCode.ReadOnly = false;
            this.TxtBigSSCode.ShowCheckBox = false;
            this.TxtBigSSCode.Size = new System.Drawing.Size(237, 26);
            this.TxtBigSSCode.TabIndex = 2;
            this.TxtBigSSCode.TabNext = true;
            this.TxtBigSSCode.Value = "";
            this.TxtBigSSCode.WidthType = UserControl.WidthTypes.Long;
            this.TxtBigSSCode.XAlign = 55;
            this.TxtBigSSCode.InnerTextChanged += new System.EventHandler(this.TxtBigSSCode_InnerTextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtBigSSCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 59);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucButtonExit);
            this.groupBox2.Controls.Add(this.ucButtonOK);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 313);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 63);
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
            this.ucButtonExit.Location = new System.Drawing.Point(274, 22);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 24);
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
            this.ucButtonOK.Location = new System.Drawing.Point(115, 22);
            this.ucButtonOK.Name = "ucButtonOK";
            this.ucButtonOK.Size = new System.Drawing.Size(88, 24);
            this.ucButtonOK.TabIndex = 7;
            this.ucButtonOK.Click += new System.EventHandler(this.ucButtonOK_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ultraGridBigSSCode);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 59);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(483, 254);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // ultraGridBigSSCode
            // 
            this.ultraGridBigSSCode.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridBigSSCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridBigSSCode.Location = new System.Drawing.Point(3, 16);
            this.ultraGridBigSSCode.Name = "ultraGridBigSSCode";
            this.ultraGridBigSSCode.Size = new System.Drawing.Size(477, 235);
            this.ultraGridBigSSCode.TabIndex = 0;
            this.ultraGridBigSSCode.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridBigSSCode_InitializeLayout);
            // 
            // FBigSSCodeQuery
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(483, 376);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FBigSSCodeQuery";
            this.Text = "大线查询";
            this.Load += new System.EventHandler(this.FBigSSCodeQuery_Load);
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
    }
}