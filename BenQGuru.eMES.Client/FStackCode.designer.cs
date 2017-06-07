namespace BenQGuru.eMES.Client
{
    partial class FStackCode 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FStackCode));
            this.TxtStackCode = new UserControl.UCLabelEdit();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ultraGridStackCode = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ucButtonExit = new UserControl.UCButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucButtonOK = new UserControl.UCButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridStackCode)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtStackCode
            // 
            this.TxtStackCode.AllowEditOnlyChecked = true;
            this.TxtStackCode.Caption = "库位";
            this.TxtStackCode.Checked = false;
            this.TxtStackCode.EditType = UserControl.EditTypes.String;
            this.TxtStackCode.Location = new System.Drawing.Point(18, 22);
            this.TxtStackCode.MaxLength = 40;
            this.TxtStackCode.Multiline = false;
            this.TxtStackCode.Name = "TxtStackCode";
            this.TxtStackCode.PasswordChar = '\0';
            this.TxtStackCode.ReadOnly = false;
            this.TxtStackCode.ShowCheckBox = false;
            this.TxtStackCode.Size = new System.Drawing.Size(237, 26);
            this.TxtStackCode.TabIndex = 2;
            this.TxtStackCode.TabNext = true;
            this.TxtStackCode.Value = "";
            this.TxtStackCode.WidthType = UserControl.WidthTypes.Long;
            this.TxtStackCode.XAlign = 55;
            this.TxtStackCode.InnerTextChanged += new System.EventHandler(this.TxtStackCode_InnerTextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ultraGridStackCode);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 59);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(483, 254);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            // 
            // ultraGridStackCode
            // 
            this.ultraGridStackCode.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridStackCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridStackCode.Location = new System.Drawing.Point(3, 16);
            this.ultraGridStackCode.Name = "ultraGridStackCode";
            this.ultraGridStackCode.Size = new System.Drawing.Size(477, 235);
            this.ultraGridStackCode.TabIndex = 0;
            this.ultraGridStackCode.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridStackCode_InitializeLayout);
            this.ultraGridStackCode.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridStackCode_CellChange);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucButtonExit);
            this.groupBox2.Controls.Add(this.ucButtonOK);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 313);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 63);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtStackCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 59);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // FStackCode
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(483, 376);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FStackCode";
            this.Text = "库位查询";
            this.Load += new System.EventHandler(this.FStackCode_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridStackCode)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCLabelEdit TxtStackCode;
        private System.Windows.Forms.GroupBox groupBox3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridStackCode;
        private UserControl.UCButton ucButtonExit;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControl.UCButton ucButtonOK;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}