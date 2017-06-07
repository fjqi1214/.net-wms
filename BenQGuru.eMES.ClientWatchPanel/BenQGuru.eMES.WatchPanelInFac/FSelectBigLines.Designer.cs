namespace BenQGuru.eMES.ClientWatchPanel
{
    partial class FSelectBigLines
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSelectBigLines));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtBigLine = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ultraGridBigLines = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonOK = new UserControl.UCButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridBigLines)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtBigLine);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(573, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // TxtBigLine
            // 
            this.TxtBigLine.AllowEditOnlyChecked = true;
            this.TxtBigLine.Caption = "大线";
            this.TxtBigLine.Checked = false;
            this.TxtBigLine.EditType = UserControl.EditTypes.String;
            this.TxtBigLine.Location = new System.Drawing.Point(12, 16);
            this.TxtBigLine.MaxLength = 40;
            this.TxtBigLine.Multiline = false;
            this.TxtBigLine.Name = "TxtBigLine";
            this.TxtBigLine.PasswordChar = '\0';
            this.TxtBigLine.ReadOnly = false;
            this.TxtBigLine.ShowCheckBox = false;
            this.TxtBigLine.Size = new System.Drawing.Size(237, 24);
            this.TxtBigLine.TabIndex = 3;
            this.TxtBigLine.TabNext = true;
            this.TxtBigLine.Value = "";
            this.TxtBigLine.WidthType = UserControl.WidthTypes.Long;
            this.TxtBigLine.XAlign = 49;
            this.TxtBigLine.InnerTextChanged += new System.EventHandler(this.TxtBigLine_InnerTextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ultraGridBigLines);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(573, 300);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // ultraGridBigLines
            // 
            this.ultraGridBigLines.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridBigLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridBigLines.Location = new System.Drawing.Point(3, 16);
            this.ultraGridBigLines.Name = "ultraGridBigLines";
            this.ultraGridBigLines.Size = new System.Drawing.Size(567, 281);
            this.ultraGridBigLines.TabIndex = 0;
            this.ultraGridBigLines.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridBigLines_InitializeLayout);
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ucButtonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "取消";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(276, 361);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 24);
            this.ucButtonExit.TabIndex = 8;
            this.ucButtonExit.Click += new System.EventHandler(this.ucButtonExit_Click);
            // 
            // ucButtonOK
            // 
            this.ucButtonOK.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ucButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButtonOK.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonOK.BackgroundImage")));
            this.ucButtonOK.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonOK.Caption = "确定";
            this.ucButtonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonOK.Location = new System.Drawing.Point(117, 361);
            this.ucButtonOK.Name = "ucButtonOK";
            this.ucButtonOK.Size = new System.Drawing.Size(88, 24);
            this.ucButtonOK.TabIndex = 7;
            this.ucButtonOK.Click += new System.EventHandler(this.ucButtonOK_Click);
            // 
            // FSelectBigLines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 391);
            this.Controls.Add(this.ucButtonExit);
            this.Controls.Add(this.ucButtonOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "FSelectBigLines";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择大线";
            this.Load += new System.EventHandler(this.FSelectBigLines_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridBigLines)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridBigLines;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCButton ucButtonOK;
        private UserControl.UCLabelEdit TxtBigLine;
    }
}