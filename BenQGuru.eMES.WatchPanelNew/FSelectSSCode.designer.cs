namespace BenQGuru.eMES.WatchPanelNew
{
    partial class FSelectSSCode
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSelectSSCode));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtSSCode = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewSSCode = new System.Windows.Forms.DataGridView();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonOK = new UserControl.UCButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSSCode)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtSSCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(573, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // TxtSSCode
            // 
            this.TxtSSCode.AllowEditOnlyChecked = true;
            this.TxtSSCode.Caption = "产线";
            this.TxtSSCode.Checked = false;
            this.TxtSSCode.EditType = UserControl.EditTypes.String;
            this.TxtSSCode.Location = new System.Drawing.Point(12, 16);
            this.TxtSSCode.MaxLength = 40;
            this.TxtSSCode.Multiline = false;
            this.TxtSSCode.Name = "TxtSSCode";
            this.TxtSSCode.PasswordChar = '\0';
            this.TxtSSCode.ReadOnly = false;
            this.TxtSSCode.ShowCheckBox = false;
            this.TxtSSCode.Size = new System.Drawing.Size(237, 24);
            this.TxtSSCode.TabIndex = 3;
            this.TxtSSCode.TabNext = true;
            this.TxtSSCode.Value = "";
            this.TxtSSCode.WidthType = UserControl.WidthTypes.Long;
            this.TxtSSCode.XAlign = 49;
            this.TxtSSCode.InnerTextChanged += new System.EventHandler(this.TxtSSCode_InnerTextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewSSCode);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(573, 300);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dataGridViewSSCode
            // 
            this.dataGridViewSSCode.AllowUserToAddRows = false;
            this.dataGridViewSSCode.AllowUserToDeleteRows = false;
            this.dataGridViewSSCode.AllowUserToResizeRows = false;
            this.dataGridViewSSCode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSSCode.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewSSCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSSCode.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSSCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSSCode.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewSSCode.Name = "dataGridViewSSCode";
            this.dataGridViewSSCode.RowHeadersVisible = false;
            this.dataGridViewSSCode.Size = new System.Drawing.Size(567, 281);
            this.dataGridViewSSCode.TabIndex = 0;
            this.dataGridViewSSCode.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSSCode_CellContentClick);
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
            // FSelectSSCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 391);
            this.Controls.Add(this.ucButtonExit);
            this.Controls.Add(this.ucButtonOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "FSelectSSCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择产线";
            this.Load += new System.EventHandler(this.FSelectBigLines_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSSCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCButton ucButtonOK;
        private UserControl.UCLabelEdit TxtSSCode;
        private System.Windows.Forms.DataGridView dataGridViewSSCode;
    }
}