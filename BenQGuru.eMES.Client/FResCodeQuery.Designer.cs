namespace BenQGuru.eMES.Client
{
    partial class FResCodeQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FResCodeQuery));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtResDESC = new UserControl.UCLabelEdit();
            this.txtResCodeQuery = new UserControl.UCLabelEdit();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ultraGridResCode = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonOK = new UserControl.UCButton();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridResCode)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtResDESC);
            this.groupBox1.Controls.Add(this.txtResCodeQuery);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(563, 76);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // txtResDESC
            // 
            this.txtResDESC.AllowEditOnlyChecked = true;
            this.txtResDESC.AutoSelectAll = false;
            this.txtResDESC.AutoUpper = true;
            this.txtResDESC.Caption = "资源描述";
            this.txtResDESC.Checked = false;
            this.txtResDESC.EditType = UserControl.EditTypes.String;
            this.txtResDESC.Location = new System.Drawing.Point(277, 20);
            this.txtResDESC.MaxLength = 40;
            this.txtResDESC.Multiline = false;
            this.txtResDESC.Name = "txtResDESC";
            this.txtResDESC.PasswordChar = '\0';
            this.txtResDESC.ReadOnly = false;
            this.txtResDESC.ShowCheckBox = false;
            this.txtResDESC.Size = new System.Drawing.Size(194, 24);
            this.txtResDESC.TabIndex = 3;
            this.txtResDESC.TabNext = true;
            this.txtResDESC.Value = "";
            this.txtResDESC.WidthType = UserControl.WidthTypes.Normal;
            this.txtResDESC.XAlign = 338;
            this.txtResDESC.InnerTextChanged += new System.EventHandler(this.txtResDESC_InnerTextChanged);
            // 
            // txtResCodeQuery
            // 
            this.txtResCodeQuery.AllowEditOnlyChecked = true;
            this.txtResCodeQuery.AutoSelectAll = false;
            this.txtResCodeQuery.AutoUpper = true;
            this.txtResCodeQuery.Caption = "资源代码";
            this.txtResCodeQuery.Checked = false;
            this.txtResCodeQuery.EditType = UserControl.EditTypes.String;
            this.txtResCodeQuery.Location = new System.Drawing.Point(36, 20);
            this.txtResCodeQuery.MaxLength = 40;
            this.txtResCodeQuery.Multiline = false;
            this.txtResCodeQuery.Name = "txtResCodeQuery";
            this.txtResCodeQuery.PasswordChar = '\0';
            this.txtResCodeQuery.ReadOnly = false;
            this.txtResCodeQuery.ShowCheckBox = false;
            this.txtResCodeQuery.Size = new System.Drawing.Size(194, 24);
            this.txtResCodeQuery.TabIndex = 2;
            this.txtResCodeQuery.TabNext = true;
            this.txtResCodeQuery.Value = "";
            this.txtResCodeQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtResCodeQuery.XAlign = 97;
            this.txtResCodeQuery.InnerTextChanged += new System.EventHandler(this.txtResCode_InnerTextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ultraGridResCode);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 76);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(563, 323);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            // 
            // ultraGridResCode
            // 
            this.ultraGridResCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ultraGridResCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridResCode.Location = new System.Drawing.Point(3, 17);
            this.ultraGridResCode.Name = "ultraGridResCode";
            this.ultraGridResCode.Size = new System.Drawing.Size(557, 303);
            this.ultraGridResCode.TabIndex = 0;
            this.ultraGridResCode.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridResCode_InitializeLayout);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucButtonExit);
            this.groupBox2.Controls.Add(this.ucButtonOK);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 399);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(563, 58);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "取消";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(314, 18);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 10;
            // 
            // ucButtonOK
            // 
            this.ucButtonOK.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonOK.BackgroundImage")));
            this.ucButtonOK.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonOK.Caption = "确定";
            this.ucButtonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonOK.Location = new System.Drawing.Point(155, 18);
            this.ucButtonOK.Name = "ucButtonOK";
            this.ucButtonOK.Size = new System.Drawing.Size(88, 22);
            this.ucButtonOK.TabIndex = 9;
            this.ucButtonOK.Click += new System.EventHandler(this.ucButtonOK_Click);
            // 
            // FResCodeQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 457);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FResCodeQuery";
            this.Text = "资源查询";
            this.Load += new System.EventHandler(this.FResCodeQuery_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridResCode)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCLabelEdit txtResCodeQuery;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControl.UCLabelEdit txtResDESC;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridResCode;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCButton ucButtonOK;
    }
}