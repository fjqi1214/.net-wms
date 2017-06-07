namespace BenQGuru.eMES.Client
{
    partial class FCollectionCarton_SKD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCollectionCarton_SKD));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.edtitemDesc = new UserControl.UCLabelEdit();
            this.btnQuery = new UserControl.UCButton();
            this.edtMoCode = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ultraGridDetail = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.edtCarton = new UserControl.UCLabelEdit();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkCINNO = new System.Windows.Forms.CheckBox();
            this.edtItemCode = new UserControl.UCLabelEdit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDetail)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.edtitemDesc);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.edtMoCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(810, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // edtitemDesc
            // 
            this.edtitemDesc.AllowEditOnlyChecked = true;
            this.edtitemDesc.Caption = "";
            this.edtitemDesc.Checked = false;
            this.edtitemDesc.EditType = UserControl.EditTypes.String;
            this.edtitemDesc.Location = new System.Drawing.Point(273, 20);
            this.edtitemDesc.MaxLength = 40;
            this.edtitemDesc.Multiline = false;
            this.edtitemDesc.Name = "edtitemDesc";
            this.edtitemDesc.PasswordChar = '\0';
            this.edtitemDesc.ReadOnly = false;
            this.edtitemDesc.ShowCheckBox = false;
            this.edtitemDesc.Size = new System.Drawing.Size(408, 24);
            this.edtitemDesc.TabIndex = 16;
            this.edtitemDesc.TabNext = false;
            this.edtitemDesc.Value = "";
            this.edtitemDesc.WidthType = UserControl.WidthTypes.TooLong;
            this.edtitemDesc.XAlign = 281;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.BackColor = System.Drawing.SystemColors.Control;
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.ButtonType = UserControl.ButtonTypes.None;
            this.btnQuery.Caption = "查询";
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.Location = new System.Drawing.Point(698, 20);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(88, 24);
            this.btnQuery.TabIndex = 15;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // edtMoCode
            // 
            this.edtMoCode.AllowEditOnlyChecked = true;
            this.edtMoCode.Caption = "工单号码";
            this.edtMoCode.Checked = false;
            this.edtMoCode.EditType = UserControl.EditTypes.String;
            this.edtMoCode.Location = new System.Drawing.Point(6, 20);
            this.edtMoCode.MaxLength = 40;
            this.edtMoCode.Multiline = false;
            this.edtMoCode.Name = "edtMoCode";
            this.edtMoCode.PasswordChar = '\0';
            this.edtMoCode.ReadOnly = false;
            this.edtMoCode.ShowCheckBox = false;
            this.edtMoCode.Size = new System.Drawing.Size(261, 24);
            this.edtMoCode.TabIndex = 1;
            this.edtMoCode.TabNext = false;
            this.edtMoCode.Value = "";
            this.edtMoCode.WidthType = UserControl.WidthTypes.Long;
            this.edtMoCode.XAlign = 67;
            this.edtMoCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtMoCode_TxtboxKeyPress);
            this.edtMoCode.InnerTextChanged += new System.EventHandler(this.edtMoCode_InnerTextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ultraGridDetail);
            this.groupBox2.Controls.Add(this.edtCarton);
            this.groupBox2.Location = new System.Drawing.Point(0, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(810, 426);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "包装结果";
            // 
            // ultraGridDetail
            // 
            this.ultraGridDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGridDetail.Cursor = System.Windows.Forms.Cursors.Default;
            appearance1.BorderColor = System.Drawing.Color.White;
            appearance1.ForeColor = System.Drawing.Color.White;
            appearance1.ForeColorDisabled = System.Drawing.Color.White;
            this.ultraGridDetail.DisplayLayout.AddNewBox.Appearance = appearance1;
            this.ultraGridDetail.Location = new System.Drawing.Point(6, 50);
            this.ultraGridDetail.Name = "ultraGridDetail";
            this.ultraGridDetail.Size = new System.Drawing.Size(792, 370);
            this.ultraGridDetail.TabIndex = 0;
            this.ultraGridDetail.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridDetail_InitializeLayout);
            this.ultraGridDetail.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridDetail_CellChange);
            // 
            // edtCarton
            // 
            this.edtCarton.AllowEditOnlyChecked = true;
            this.edtCarton.Caption = "包装箱号";
            this.edtCarton.Checked = false;
            this.edtCarton.EditType = UserControl.EditTypes.String;
            this.edtCarton.Location = new System.Drawing.Point(6, 20);
            this.edtCarton.MaxLength = 40;
            this.edtCarton.Multiline = false;
            this.edtCarton.Name = "edtCarton";
            this.edtCarton.PasswordChar = '\0';
            this.edtCarton.ReadOnly = false;
            this.edtCarton.ShowCheckBox = false;
            this.edtCarton.Size = new System.Drawing.Size(261, 24);
            this.edtCarton.TabIndex = 2;
            this.edtCarton.TabNext = false;
            this.edtCarton.Value = "";
            this.edtCarton.WidthType = UserControl.WidthTypes.Long;
            this.edtCarton.XAlign = 67;
            this.edtCarton.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtCarton_TxtboxKeyPress);
            this.edtCarton.InnerTextChanged += new System.EventHandler(this.edtCarton_InnerTextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkCINNO);
            this.groupBox3.Controls.Add(this.edtItemCode);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 489);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(810, 59);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // checkCINNO
            // 
            this.checkCINNO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkCINNO.Location = new System.Drawing.Point(288, 29);
            this.checkCINNO.Name = "checkCINNO";
            this.checkCINNO.Size = new System.Drawing.Size(130, 20);
            this.checkCINNO.TabIndex = 227;
            this.checkCINNO.Text = "检查上料资料";
            // 
            // edtItemCode
            // 
            this.edtItemCode.AllowEditOnlyChecked = true;
            this.edtItemCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.edtItemCode.Caption = "输入框";
            this.edtItemCode.Checked = false;
            this.edtItemCode.EditType = UserControl.EditTypes.String;
            this.edtItemCode.Location = new System.Drawing.Point(12, 25);
            this.edtItemCode.MaxLength = 40;
            this.edtItemCode.Multiline = false;
            this.edtItemCode.Name = "edtItemCode";
            this.edtItemCode.PasswordChar = '\0';
            this.edtItemCode.ReadOnly = false;
            this.edtItemCode.ShowCheckBox = false;
            this.edtItemCode.Size = new System.Drawing.Size(249, 24);
            this.edtItemCode.TabIndex = 2;
            this.edtItemCode.TabNext = false;
            this.edtItemCode.Value = "";
            this.edtItemCode.WidthType = UserControl.WidthTypes.Long;
            this.edtItemCode.XAlign = 61;
            this.edtItemCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtItemCode_TxtboxKeyPress);
            // 
            // FCollectionCarton_SKD
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(810, 548);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FCollectionCarton_SKD";
            this.Text = "SKD包装";
            this.Load += new System.EventHandler(this.FCollectionCarton_SKD_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDetail)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private UserControl.UCLabelEdit edtMoCode;
        private UserControl.UCButton btnQuery;
        private UserControl.UCLabelEdit edtItemCode;
        private System.Windows.Forms.CheckBox checkCINNO;
        private UserControl.UCLabelEdit edtCarton;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridDetail;
        private UserControl.UCLabelEdit edtitemDesc;
    }
}