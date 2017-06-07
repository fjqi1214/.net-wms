namespace BenQGuru.eMES.Client
{
    partial class FModelQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FModelQuery));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ultraGridModelCode = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonOK = new UserControl.UCButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtItemModelCode = new UserControl.UCLabelEdit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridModelCode)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ultraGridModelCode);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 54);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(483, 232);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            // 
            // ultraGridModelCode
            // 
            this.ultraGridModelCode.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridModelCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridModelCode.Location = new System.Drawing.Point(3, 17);
            this.ultraGridModelCode.Name = "ultraGridModelCode";
            this.ultraGridModelCode.Size = new System.Drawing.Size(477, 212);
            this.ultraGridModelCode.TabIndex = 0;
            this.ultraGridModelCode.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridModelCode_InitializeLayout);
            this.ultraGridModelCode.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridModelCode_CellChange);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucButtonExit);
            this.groupBox2.Controls.Add(this.ucButtonOK);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 286);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 58);
            this.groupBox2.TabIndex = 15;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtItemModelCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 54);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // txtItemModelCode
            // 
            this.txtItemModelCode.AllowEditOnlyChecked = true;
            this.txtItemModelCode.AutoSelectAll = false;
            this.txtItemModelCode.AutoUpper = true;
            this.txtItemModelCode.Caption = "产品别";
            this.txtItemModelCode.Checked = false;
            this.txtItemModelCode.EditType = UserControl.EditTypes.String;
            this.txtItemModelCode.Location = new System.Drawing.Point(12, 18);
            this.txtItemModelCode.MaxLength = 40;
            this.txtItemModelCode.Multiline = false;
            this.txtItemModelCode.Name = "txtItemModelCode";
            this.txtItemModelCode.PasswordChar = '\0';
            this.txtItemModelCode.ReadOnly = false;
            this.txtItemModelCode.ShowCheckBox = false;
            this.txtItemModelCode.Size = new System.Drawing.Size(182, 24);
            this.txtItemModelCode.TabIndex = 2;
            this.txtItemModelCode.TabNext = true;
            this.txtItemModelCode.Value = "";
            this.txtItemModelCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtItemModelCode.XAlign = 61;
            this.txtItemModelCode.InnerTextChanged += new System.EventHandler(this.TxtModelCode_InnerTextChanged);
            // 
            // FModelQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 344);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FModelQuery";
            this.Text = "产品别查询";
            this.Load += new System.EventHandler(this.FModelQuery_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridModelCode)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridModelCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCButton ucButtonOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCLabelEdit txtItemModelCode;

    }
}