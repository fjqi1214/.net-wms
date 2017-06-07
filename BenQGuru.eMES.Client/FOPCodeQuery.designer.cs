namespace BenQGuru.eMES.Client
{
    partial class FOPCodeQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FOPCodeQuery));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOPDESC = new UserControl.UCLabelEdit();
            this.txtOPCodeQuery = new UserControl.UCLabelEdit();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ultraGridOPCode = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonOK = new UserControl.UCButton();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridOPCode)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtOPDESC);
            this.groupBox1.Controls.Add(this.txtOPCodeQuery);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(563, 76);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // txtOPDESC
            // 
            this.txtOPDESC.AllowEditOnlyChecked = true;
            this.txtOPDESC.AutoSelectAll = false;
            this.txtOPDESC.AutoUpper = true;
            this.txtOPDESC.Caption = "工序描述";
            this.txtOPDESC.Checked = false;
            this.txtOPDESC.EditType = UserControl.EditTypes.String;
            this.txtOPDESC.Location = new System.Drawing.Point(277, 20);
            this.txtOPDESC.MaxLength = 40;
            this.txtOPDESC.Multiline = false;
            this.txtOPDESC.Name = "txtOPDESC";
            this.txtOPDESC.PasswordChar = '\0';
            this.txtOPDESC.ReadOnly = false;
            this.txtOPDESC.ShowCheckBox = false;
            this.txtOPDESC.Size = new System.Drawing.Size(194, 24);
            this.txtOPDESC.TabIndex = 3;
            this.txtOPDESC.TabNext = true;
            this.txtOPDESC.Value = "";
            this.txtOPDESC.WidthType = UserControl.WidthTypes.Normal;
            this.txtOPDESC.XAlign = 338;
            this.txtOPDESC.InnerTextChanged += new System.EventHandler(this.txtOPDESC_InnerTextChanged);
            // 
            // txtOPCodeQuery
            // 
            this.txtOPCodeQuery.AllowEditOnlyChecked = true;
            this.txtOPCodeQuery.AutoSelectAll = false;
            this.txtOPCodeQuery.AutoUpper = true;
            this.txtOPCodeQuery.Caption = "工序代码";
            this.txtOPCodeQuery.Checked = false;
            this.txtOPCodeQuery.EditType = UserControl.EditTypes.String;
            this.txtOPCodeQuery.Location = new System.Drawing.Point(36, 20);
            this.txtOPCodeQuery.MaxLength = 40;
            this.txtOPCodeQuery.Multiline = false;
            this.txtOPCodeQuery.Name = "txtOPCodeQuery";
            this.txtOPCodeQuery.PasswordChar = '\0';
            this.txtOPCodeQuery.ReadOnly = false;
            this.txtOPCodeQuery.ShowCheckBox = false;
            this.txtOPCodeQuery.Size = new System.Drawing.Size(194, 24);
            this.txtOPCodeQuery.TabIndex = 2;
            this.txtOPCodeQuery.TabNext = true;
            this.txtOPCodeQuery.Value = "";
            this.txtOPCodeQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtOPCodeQuery.XAlign = 97;
            this.txtOPCodeQuery.InnerTextChanged += new System.EventHandler(this.txtOPCode_InnerTextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ultraGridOPCode);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 76);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(563, 323);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            // 
            // ultraGridOPCode
            // 
            this.ultraGridOPCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ultraGridOPCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridOPCode.Location = new System.Drawing.Point(3, 17);
            this.ultraGridOPCode.Name = "ultraGridOPCode";
            this.ultraGridOPCode.Size = new System.Drawing.Size(557, 303);
            this.ultraGridOPCode.TabIndex = 0;
            this.ultraGridOPCode.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridOPCode_InitializeLayout);
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
            // FOPCodeQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 457);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FOPCodeQuery";
            this.Text = "工序查询";
            this.Load += new System.EventHandler(this.FOPCodeQuery_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridOPCode)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCLabelEdit txtOPCodeQuery;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControl.UCLabelEdit txtOPDESC;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridOPCode;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCButton ucButtonOK;
    }
}