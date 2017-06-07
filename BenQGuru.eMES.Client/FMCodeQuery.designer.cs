namespace BenQGuru.eMES.Client
{
    partial class FMCodeQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMCodeQuery));
            this.TxtMCode = new UserControl.UCLabelEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMDesc = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonOK = new UserControl.UCButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ultraGridMaterial = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMaterial)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtMCode
            // 
            this.TxtMCode.AllowEditOnlyChecked = true;
            this.TxtMCode.AutoUpper = true;
            this.TxtMCode.Caption = "物料代码";
            this.TxtMCode.Checked = false;
            this.TxtMCode.EditType = UserControl.EditTypes.String;
            this.TxtMCode.Location = new System.Drawing.Point(12, 20);
            this.TxtMCode.MaxLength = 40;
            this.TxtMCode.Multiline = false;
            this.TxtMCode.Name = "TxtMCode";
            this.TxtMCode.PasswordChar = '\0';
            this.TxtMCode.ReadOnly = false;
            this.TxtMCode.ShowCheckBox = false;
            this.TxtMCode.Size = new System.Drawing.Size(194, 24);
            this.TxtMCode.TabIndex = 2;
            this.TxtMCode.TabNext = true;
            this.TxtMCode.Value = "";
            this.TxtMCode.WidthType = UserControl.WidthTypes.Normal;
            this.TxtMCode.XAlign = 73;
            this.TxtMCode.InnerTextChanged += new System.EventHandler(this.TxtBigSSCode_InnerTextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMDesc);
            this.groupBox1.Controls.Add(this.TxtMCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 54);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // txtMDesc
            // 
            this.txtMDesc.AllowEditOnlyChecked = true;
            this.txtMDesc.AutoUpper = true;
            this.txtMDesc.Caption = "物料描述";
            this.txtMDesc.Checked = false;
            this.txtMDesc.EditType = UserControl.EditTypes.String;
            this.txtMDesc.Location = new System.Drawing.Point(244, 20);
            this.txtMDesc.MaxLength = 40;
            this.txtMDesc.Multiline = false;
            this.txtMDesc.Name = "txtMDesc";
            this.txtMDesc.PasswordChar = '\0';
            this.txtMDesc.ReadOnly = false;
            this.txtMDesc.ShowCheckBox = false;
            this.txtMDesc.Size = new System.Drawing.Size(194, 24);
            this.txtMDesc.TabIndex = 3;
            this.txtMDesc.TabNext = true;
            this.txtMDesc.Value = "";
            this.txtMDesc.WidthType = UserControl.WidthTypes.Normal;
            this.txtMDesc.XAlign = 305;
            this.txtMDesc.InnerTextChanged += new System.EventHandler(this.TxtVendorName_InnerTextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucButtonExit);
            this.groupBox2.Controls.Add(this.ucButtonOK);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 286);
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
            this.groupBox3.Controls.Add(this.ultraGridMaterial);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 54);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(483, 232);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // ultraGridMaterial
            // 
            this.ultraGridMaterial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ultraGridMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMaterial.Location = new System.Drawing.Point(3, 17);
            this.ultraGridMaterial.Name = "ultraGridMaterial";
            this.ultraGridMaterial.Size = new System.Drawing.Size(477, 212);
            this.ultraGridMaterial.TabIndex = 0;
            this.ultraGridMaterial.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridBigSSCode_InitializeLayout);
            this.ultraGridMaterial.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridMaterial_CellChange);
            // 
            // FMCodeQuery
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(483, 344);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FMCodeQuery";
            this.Text = "物料代码";
            this.Load += new System.EventHandler(this.FMCodeQuery_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMaterial)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCLabelEdit TxtMCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMaterial;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCButton ucButtonOK;
        private UserControl.UCLabelEdit txtMDesc;
    }
}