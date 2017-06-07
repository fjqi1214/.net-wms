namespace BenQGuru.eMES.Client
{
    partial class FRecNoSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRecNoSelect));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInvRecNo = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdCancel = new UserControl.UCButton();
            this.cmdOK = new UserControl.UCButton();
            this.gridProduct = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtInvRecNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(522, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtInvRecNo
            // 
            this.txtInvRecNo.AllowEditOnlyChecked = true;
            this.txtInvRecNo.AutoSelectAll = false;
            this.txtInvRecNo.AutoUpper = true;
            this.txtInvRecNo.Caption = "入库单号";
            this.txtInvRecNo.Checked = false;
            this.txtInvRecNo.EditType = UserControl.EditTypes.String;
            this.txtInvRecNo.Location = new System.Drawing.Point(12, 20);
            this.txtInvRecNo.MaxLength = 40;
            this.txtInvRecNo.Multiline = false;
            this.txtInvRecNo.Name = "txtInvRecNo";
            this.txtInvRecNo.PasswordChar = '\0';
            this.txtInvRecNo.ReadOnly = false;
            this.txtInvRecNo.ShowCheckBox = false;
            this.txtInvRecNo.Size = new System.Drawing.Size(194, 24);
            this.txtInvRecNo.TabIndex = 0;
            this.txtInvRecNo.TabNext = true;
            this.txtInvRecNo.Value = "";
            this.txtInvRecNo.WidthType = UserControl.WidthTypes.Normal;
            this.txtInvRecNo.XAlign = 73;
            this.txtInvRecNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtItemCode_TxtboxKeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdCancel);
            this.groupBox2.Controls.Add(this.cmdOK);
            this.groupBox2.Controls.Add(this.gridProduct);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(522, 349);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.BackColor = System.Drawing.SystemColors.Control;
            this.cmdCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdCancel.BackgroundImage")));
            this.cmdCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.cmdCancel.Caption = "取消";
            this.cmdCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdCancel.Location = new System.Drawing.Point(268, 304);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(88, 22);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdOK.BackColor = System.Drawing.SystemColors.Control;
            this.cmdOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdOK.BackgroundImage")));
            this.cmdOK.ButtonType = UserControl.ButtonTypes.None;
            this.cmdOK.Caption = "确定";
            this.cmdOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdOK.Location = new System.Drawing.Point(122, 304);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(88, 22);
            this.cmdOK.TabIndex = 2;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // gridProduct
            // 
            this.gridProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridProduct.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridProduct.Location = new System.Drawing.Point(3, 17);
            this.gridProduct.Name = "gridProduct";
            this.gridProduct.Size = new System.Drawing.Size(513, 267);
            this.gridProduct.TabIndex = 0;
            this.gridProduct.DoubleClick += new System.EventHandler(this.gridProduct_DoubleClick);
            this.gridProduct.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridProduct_InitializeLayout);
            // 
            // FRecNoSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 399);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FRecNoSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择入库单";
            this.Load += new System.EventHandler(this.FProductSelect_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProduct)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControl.UCLabelEdit txtInvRecNo;
        private UserControl.UCButton cmdCancel;
        private UserControl.UCButton cmdOK;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridProduct;
    }
}