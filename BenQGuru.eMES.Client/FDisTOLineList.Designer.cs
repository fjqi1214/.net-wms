namespace BenQGuru.eMES.Client
{
    partial class FDisTOLineList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FDisTOLineList));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSSCode = new UserControl.UCLabelEdit();
            this.txtSegCode = new UserControl.UCLabelEdit();
            this.txtMCodeQuery = new UserControl.UCLabelEdit();
            this.txtMoQuery = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBack = new UserControl.UCButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSSCode);
            this.groupBox1.Controls.Add(this.txtSegCode);
            this.groupBox1.Controls.Add(this.txtMCodeQuery);
            this.groupBox1.Controls.Add(this.txtMoQuery);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(544, 73);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtSSCode
            // 
            this.txtSSCode.AllowEditOnlyChecked = true;
            this.txtSSCode.AutoSelectAll = false;
            this.txtSSCode.AutoUpper = true;
            this.txtSSCode.Caption = "产线代码";
            this.txtSSCode.Checked = false;
            this.txtSSCode.EditType = UserControl.EditTypes.String;
            this.txtSSCode.Location = new System.Drawing.Point(227, 42);
            this.txtSSCode.MaxLength = 40;
            this.txtSSCode.Multiline = false;
            this.txtSSCode.Name = "txtSSCode";
            this.txtSSCode.PasswordChar = '\0';
            this.txtSSCode.ReadOnly = false;
            this.txtSSCode.ShowCheckBox = false;
            this.txtSSCode.Size = new System.Drawing.Size(194, 24);
            this.txtSSCode.TabIndex = 5;
            this.txtSSCode.TabNext = true;
            this.txtSSCode.Value = "";
            this.txtSSCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtSSCode.XAlign = 288;
            // 
            // txtSegCode
            // 
            this.txtSegCode.AllowEditOnlyChecked = true;
            this.txtSegCode.AutoSelectAll = false;
            this.txtSegCode.AutoUpper = true;
            this.txtSegCode.Caption = "车间代码";
            this.txtSegCode.Checked = false;
            this.txtSegCode.EditType = UserControl.EditTypes.String;
            this.txtSegCode.Location = new System.Drawing.Point(12, 42);
            this.txtSegCode.MaxLength = 40;
            this.txtSegCode.Multiline = false;
            this.txtSegCode.Name = "txtSegCode";
            this.txtSegCode.PasswordChar = '\0';
            this.txtSegCode.ReadOnly = false;
            this.txtSegCode.ShowCheckBox = false;
            this.txtSegCode.Size = new System.Drawing.Size(194, 24);
            this.txtSegCode.TabIndex = 4;
            this.txtSegCode.TabNext = true;
            this.txtSegCode.Value = "";
            this.txtSegCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtSegCode.XAlign = 73;
            // 
            // txtMCodeQuery
            // 
            this.txtMCodeQuery.AllowEditOnlyChecked = true;
            this.txtMCodeQuery.AutoSelectAll = false;
            this.txtMCodeQuery.AutoUpper = true;
            this.txtMCodeQuery.Caption = "物料代码";
            this.txtMCodeQuery.Checked = false;
            this.txtMCodeQuery.EditType = UserControl.EditTypes.String;
            this.txtMCodeQuery.Location = new System.Drawing.Point(227, 12);
            this.txtMCodeQuery.MaxLength = 40;
            this.txtMCodeQuery.Multiline = false;
            this.txtMCodeQuery.Name = "txtMCodeQuery";
            this.txtMCodeQuery.PasswordChar = '\0';
            this.txtMCodeQuery.ReadOnly = false;
            this.txtMCodeQuery.ShowCheckBox = false;
            this.txtMCodeQuery.Size = new System.Drawing.Size(194, 24);
            this.txtMCodeQuery.TabIndex = 3;
            this.txtMCodeQuery.TabNext = true;
            this.txtMCodeQuery.Value = "";
            this.txtMCodeQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtMCodeQuery.XAlign = 288;
            // 
            // txtMoQuery
            // 
            this.txtMoQuery.AllowEditOnlyChecked = true;
            this.txtMoQuery.AutoSelectAll = false;
            this.txtMoQuery.AutoUpper = true;
            this.txtMoQuery.Caption = "工单代码";
            this.txtMoQuery.Checked = false;
            this.txtMoQuery.EditType = UserControl.EditTypes.String;
            this.txtMoQuery.Location = new System.Drawing.Point(12, 12);
            this.txtMoQuery.MaxLength = 40;
            this.txtMoQuery.Multiline = false;
            this.txtMoQuery.Name = "txtMoQuery";
            this.txtMoQuery.PasswordChar = '\0';
            this.txtMoQuery.ReadOnly = false;
            this.txtMoQuery.ShowCheckBox = false;
            this.txtMoQuery.Size = new System.Drawing.Size(194, 24);
            this.txtMoQuery.TabIndex = 2;
            this.txtMoQuery.TabNext = true;
            this.txtMoQuery.Value = "";
            this.txtMoQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtMoQuery.XAlign = 73;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBack);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 338);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(544, 44);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.Control;
            this.btnBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBack.BackgroundImage")));
            this.btnBack.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnBack.Caption = "返回";
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.Location = new System.Drawing.Point(221, 14);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(88, 22);
            this.btnBack.TabIndex = 0;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ultraGridMain);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 73);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(544, 265);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            // 
            // ultraGridMain
            // 
            this.ultraGridMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMain.Location = new System.Drawing.Point(3, 17);
            this.ultraGridMain.Name = "ultraGridMain";
            this.ultraGridMain.Size = new System.Drawing.Size(538, 245);
            this.ultraGridMain.TabIndex = 1;
            this.ultraGridMain.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMain_InitializeLayout);
            this.ultraGridMain.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ultraGridMain_InitializeRow);
            // 
            // FDisTOLineList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 382);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FDisTOLineList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "配料记录查询";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCLabelEdit txtMCodeQuery;
        private UserControl.UCLabelEdit txtMoQuery;
        private UserControl.UCLabelEdit txtSSCode;
        private UserControl.UCLabelEdit txtSegCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControl.UCButton btnBack;
        private System.Windows.Forms.GroupBox groupBox3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
    }
}