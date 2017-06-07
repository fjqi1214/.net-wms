namespace BenQGuru.eMES.Client
{
    partial class FVendorCodeQuery 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FVendorCodeQuery));
            this.TxtVendorCode = new UserControl.UCLabelEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtVendorName = new UserControl.UCLabelEdit();
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
            // TxtVendorCode
            // 
            this.TxtVendorCode.AllowEditOnlyChecked = true;
            this.TxtVendorCode.AutoSelectAll = false;
            this.TxtVendorCode.AutoUpper = true;
            this.TxtVendorCode.Caption = "供应商代码";
            this.TxtVendorCode.Checked = false;
            this.TxtVendorCode.EditType = UserControl.EditTypes.String;
            this.TxtVendorCode.Location = new System.Drawing.Point(6, 20);
            this.TxtVendorCode.MaxLength = 40;
            this.TxtVendorCode.Multiline = false;
            this.TxtVendorCode.Name = "TxtVendorCode";
            this.TxtVendorCode.PasswordChar = '\0';
            this.TxtVendorCode.ReadOnly = false;
            this.TxtVendorCode.ShowCheckBox = false;
            this.TxtVendorCode.Size = new System.Drawing.Size(206, 24);
            this.TxtVendorCode.TabIndex = 2;
            this.TxtVendorCode.TabNext = true;
            this.TxtVendorCode.Value = "";
            this.TxtVendorCode.WidthType = UserControl.WidthTypes.Normal;
            this.TxtVendorCode.XAlign = 79;
            this.TxtVendorCode.InnerTextChanged += new System.EventHandler(this.TxtBigSSCode_InnerTextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtVendorName);
            this.groupBox1.Controls.Add(this.TxtVendorCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 54);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // txtVendorName
            // 
            this.txtVendorName.AllowEditOnlyChecked = true;
            this.txtVendorName.AutoSelectAll = false;
            this.txtVendorName.AutoUpper = true;
            this.txtVendorName.Caption = "供应商名称";
            this.txtVendorName.Checked = false;
            this.txtVendorName.EditType = UserControl.EditTypes.String;
            this.txtVendorName.Location = new System.Drawing.Point(253, 20);
            this.txtVendorName.MaxLength = 40;
            this.txtVendorName.Multiline = false;
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.PasswordChar = '\0';
            this.txtVendorName.ReadOnly = false;
            this.txtVendorName.ShowCheckBox = false;
            this.txtVendorName.Size = new System.Drawing.Size(206, 24);
            this.txtVendorName.TabIndex = 3;
            this.txtVendorName.TabNext = true;
            this.txtVendorName.Value = "";
            this.txtVendorName.WidthType = UserControl.WidthTypes.Normal;
            this.txtVendorName.XAlign = 326;
            this.txtVendorName.InnerTextChanged += new System.EventHandler(this.TxtVendorName_InnerTextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucButtonExit);
            this.groupBox2.Controls.Add(this.ucButtonOK);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 289);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 58);
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
            this.groupBox3.Size = new System.Drawing.Size(483, 235);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // ultraGridBigSSCode
            // 
            this.ultraGridBigSSCode.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridBigSSCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridBigSSCode.Location = new System.Drawing.Point(3, 17);
            this.ultraGridBigSSCode.Name = "ultraGridBigSSCode";
            this.ultraGridBigSSCode.Size = new System.Drawing.Size(477, 215);
            this.ultraGridBigSSCode.TabIndex = 0;
            this.ultraGridBigSSCode.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridBigSSCode_InitializeLayout);
            // 
            // FVendorCodeQuery
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(483, 347);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FVendorCodeQuery";
            this.Text = "供应商查询";
            this.Load += new System.EventHandler(this.FBigSSCodeQuery_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridBigSSCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCLabelEdit TxtVendorCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridBigSSCode;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCButton ucButtonOK;
        private UserControl.UCLabelEdit txtVendorName;
    }
}