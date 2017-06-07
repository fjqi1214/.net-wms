namespace BenQGuru.eMES.Client
{
    partial class FItemCodeQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FItemCodeQuery));
            this.TxtItemCode = new UserControl.UCLabelEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtItemDesc = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonOK = new UserControl.UCButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ultraGridItem = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridItem)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtItemCode
            // 
            this.TxtItemCode.AllowEditOnlyChecked = true;
            this.TxtItemCode.Caption = "产品代码";
            this.TxtItemCode.Checked = false;
            this.TxtItemCode.EditType = UserControl.EditTypes.String;
            this.TxtItemCode.Location = new System.Drawing.Point(18, 22);
            this.TxtItemCode.MaxLength = 40;
            this.TxtItemCode.Multiline = false;
            this.TxtItemCode.Name = "TxtItemCode";
            this.TxtItemCode.PasswordChar = '\0';
            this.TxtItemCode.ReadOnly = false;
            this.TxtItemCode.ShowCheckBox = false;
            this.TxtItemCode.Size = new System.Drawing.Size(194, 26);
            this.TxtItemCode.TabIndex = 2;
            this.TxtItemCode.TabNext = true;
            this.TxtItemCode.Value = "";
            this.TxtItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.TxtItemCode.XAlign = 79;
            this.TxtItemCode.InnerTextChanged += new System.EventHandler(this.TxtBigSSCode_InnerTextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtItemDesc);
            this.groupBox1.Controls.Add(this.TxtItemCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 59);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // txtItemDesc
            // 
            this.txtItemDesc.AllowEditOnlyChecked = true;
            this.txtItemDesc.Caption = "产品描述";
            this.txtItemDesc.Checked = false;
            this.txtItemDesc.EditType = UserControl.EditTypes.String;
            this.txtItemDesc.Location = new System.Drawing.Point(265, 22);
            this.txtItemDesc.MaxLength = 40;
            this.txtItemDesc.Multiline = false;
            this.txtItemDesc.Name = "txtItemDesc";
            this.txtItemDesc.PasswordChar = '\0';
            this.txtItemDesc.ReadOnly = false;
            this.txtItemDesc.ShowCheckBox = false;
            this.txtItemDesc.Size = new System.Drawing.Size(194, 26);
            this.txtItemDesc.TabIndex = 3;
            this.txtItemDesc.TabNext = true;
            this.txtItemDesc.Value = "";
            this.txtItemDesc.WidthType = UserControl.WidthTypes.Normal;
            this.txtItemDesc.XAlign = 326;
            this.txtItemDesc.InnerTextChanged += new System.EventHandler(this.TxtVendorName_InnerTextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucButtonExit);
            this.groupBox2.Controls.Add(this.ucButtonOK);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 310);
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
            this.groupBox3.Controls.Add(this.ultraGridItem);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 59);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(483, 251);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // ultraGridItem
            // 
            this.ultraGridItem.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridItem.Location = new System.Drawing.Point(3, 16);
            this.ultraGridItem.Name = "ultraGridItem";
            this.ultraGridItem.Size = new System.Drawing.Size(477, 232);
            this.ultraGridItem.TabIndex = 0;
            this.ultraGridItem.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridBigSSCode_InitializeLayout);
            // 
            // FItemCodeQuery
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(483, 373);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FItemCodeQuery";
            this.Text = "产品代码";
            this.Load += new System.EventHandler(this.FMCodeQuery_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCLabelEdit TxtItemCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridItem;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCButton ucButtonOK;
        private UserControl.UCLabelEdit txtItemDesc;
    }
}