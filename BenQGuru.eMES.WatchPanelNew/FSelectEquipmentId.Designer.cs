namespace BenQGuru.eMES.WatchPanelNew
{
    partial class FSelectEquipmentId
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSelectEquipmentId));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewEQPID = new System.Windows.Forms.DataGridView();
            this.TxtEQPID = new UserControl.UCLabelEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonOK = new UserControl.UCButton();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEQPID)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewEQPID);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(573, 300);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // dataGridViewEQPID
            // 
            this.dataGridViewEQPID.AllowUserToAddRows = false;
            this.dataGridViewEQPID.AllowUserToDeleteRows = false;
            this.dataGridViewEQPID.AllowUserToResizeRows = false;
            this.dataGridViewEQPID.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEQPID.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewEQPID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewEQPID.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewEQPID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEQPID.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewEQPID.Name = "dataGridViewEQPID";
            this.dataGridViewEQPID.RowHeadersVisible = false;
            this.dataGridViewEQPID.Size = new System.Drawing.Size(567, 281);
            this.dataGridViewEQPID.TabIndex = 0;
            this.dataGridViewEQPID.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEQPID_CellContentClick);
            // 
            // TxtEQPID
            // 
            this.TxtEQPID.AllowEditOnlyChecked = true;
            this.TxtEQPID.Caption = "设备ID";
            this.TxtEQPID.Checked = false;
            this.TxtEQPID.EditType = UserControl.EditTypes.String;
            this.TxtEQPID.Location = new System.Drawing.Point(5, 16);
            this.TxtEQPID.MaxLength = 40;
            this.TxtEQPID.Multiline = false;
            this.TxtEQPID.Name = "TxtEQPID";
            this.TxtEQPID.PasswordChar = '\0';
            this.TxtEQPID.ReadOnly = false;
            this.TxtEQPID.ShowCheckBox = false;
            this.TxtEQPID.Size = new System.Drawing.Size(249, 24);
            this.TxtEQPID.TabIndex = 3;
            this.TxtEQPID.TabNext = true;
            this.TxtEQPID.Value = "";
            this.TxtEQPID.WidthType = UserControl.WidthTypes.Long;
            this.TxtEQPID.XAlign = 54;
            this.TxtEQPID.InnerTextChanged += new System.EventHandler(this.TxtEQPID_InnerTextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtEQPID);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(573, 50);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
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
            this.ucButtonExit.Location = new System.Drawing.Point(276, 364);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 24);
            this.ucButtonExit.TabIndex = 12;
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
            this.ucButtonOK.Location = new System.Drawing.Point(117, 364);
            this.ucButtonOK.Name = "ucButtonOK";
            this.ucButtonOK.Size = new System.Drawing.Size(88, 24);
            this.ucButtonOK.TabIndex = 11;
            this.ucButtonOK.Click += new System.EventHandler(this.ucButtonOK_Click);
            // 
            // FSelectEquipmentId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 391);
            this.Controls.Add(this.ucButtonExit);
            this.Controls.Add(this.ucButtonOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "FSelectEquipmentId";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择设备";
            this.Load += new System.EventHandler(this.FSelectEquipmentId_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEQPID)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCButton ucButtonExit;
        private UserControl.UCButton ucButtonOK;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControl.UCLabelEdit TxtEQPID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridViewEQPID;
    }
}