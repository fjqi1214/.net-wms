namespace BenQGuru.eMES.Client
{
    partial class FDocView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FDocView));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGetOplistQuery = new System.Windows.Forms.Button();
            this.txtOplistQuery = new UserControl.UCLabelEdit();
            this.txtMemoQuery = new UserControl.UCLabelEdit();
            this.drpValidStatusQuery = new UserControl.UCLabelCombox();
            this.drpCheckedStatusQuery = new UserControl.UCLabelCombox();
            this.txtKeywordQuery = new UserControl.UCLabelEdit();
            this.txtDocnumQuery = new UserControl.UCLabelEdit();
            this.txtMcodelistQuery = new UserControl.UCLabelEdit();
            this.txtDocnameQuery = new UserControl.UCLabelEdit();
            this.btnGetMcodelist = new System.Windows.Forms.Button();
            this.btnQuery = new UserControl.UCButton();
            this.drpDoctypeQuery = new UserControl.UCLabelCombox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnExit = new UserControl.UCButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ultraGridScrutiny = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridScrutiny)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGetOplistQuery);
            this.groupBox1.Controls.Add(this.txtOplistQuery);
            this.groupBox1.Controls.Add(this.txtMemoQuery);
            this.groupBox1.Controls.Add(this.drpValidStatusQuery);
            this.groupBox1.Controls.Add(this.drpCheckedStatusQuery);
            this.groupBox1.Controls.Add(this.txtKeywordQuery);
            this.groupBox1.Controls.Add(this.txtDocnumQuery);
            this.groupBox1.Controls.Add(this.txtMcodelistQuery);
            this.groupBox1.Controls.Add(this.txtDocnameQuery);
            this.groupBox1.Controls.Add(this.btnGetMcodelist);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.drpDoctypeQuery);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(873, 112);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            // 
            // btnGetOplistQuery
            // 
            this.btnGetOplistQuery.Location = new System.Drawing.Point(723, 49);
            this.btnGetOplistQuery.Name = "btnGetOplistQuery";
            this.btnGetOplistQuery.Size = new System.Drawing.Size(34, 22);
            this.btnGetOplistQuery.TabIndex = 225;
            this.btnGetOplistQuery.Text = "...";
            this.btnGetOplistQuery.UseVisualStyleBackColor = true;
            this.btnGetOplistQuery.Click += new System.EventHandler(this.btnGetOplistQuery_Click);
            // 
            // txtOplistQuery
            // 
            this.txtOplistQuery.AllowEditOnlyChecked = true;
            this.txtOplistQuery.AutoUpper = true;
            this.txtOplistQuery.Caption = "工序";
            this.txtOplistQuery.Checked = false;
            this.txtOplistQuery.EditType = UserControl.EditTypes.String;
            this.txtOplistQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtOplistQuery.Location = new System.Drawing.Point(537, 49);
            this.txtOplistQuery.MaxLength = 40;
            this.txtOplistQuery.Multiline = false;
            this.txtOplistQuery.Name = "txtOplistQuery";
            this.txtOplistQuery.PasswordChar = '\0';
            this.txtOplistQuery.ReadOnly = false;
            this.txtOplistQuery.ShowCheckBox = false;
            this.txtOplistQuery.Size = new System.Drawing.Size(170, 22);
            this.txtOplistQuery.TabIndex = 224;
            this.txtOplistQuery.TabNext = true;
            this.txtOplistQuery.Value = "";
            this.txtOplistQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtOplistQuery.XAlign = 574;
            // 
            // txtMemoQuery
            // 
            this.txtMemoQuery.AllowEditOnlyChecked = true;
            this.txtMemoQuery.AutoUpper = true;
            this.txtMemoQuery.Caption = "备注";
            this.txtMemoQuery.Checked = false;
            this.txtMemoQuery.EditType = UserControl.EditTypes.String;
            this.txtMemoQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtMemoQuery.Location = new System.Drawing.Point(288, 49);
            this.txtMemoQuery.MaxLength = 40;
            this.txtMemoQuery.Multiline = false;
            this.txtMemoQuery.Name = "txtMemoQuery";
            this.txtMemoQuery.PasswordChar = '\0';
            this.txtMemoQuery.ReadOnly = false;
            this.txtMemoQuery.ShowCheckBox = false;
            this.txtMemoQuery.Size = new System.Drawing.Size(170, 22);
            this.txtMemoQuery.TabIndex = 223;
            this.txtMemoQuery.TabNext = true;
            this.txtMemoQuery.Value = "";
            this.txtMemoQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtMemoQuery.XAlign = 325;
            // 
            // drpValidStatusQuery
            // 
            this.drpValidStatusQuery.AllowEditOnlyChecked = false;
            this.drpValidStatusQuery.Caption = "有效状态";
            this.drpValidStatusQuery.Checked = false;
            this.drpValidStatusQuery.Location = new System.Drawing.Point(513, 77);
            this.drpValidStatusQuery.Name = "drpValidStatusQuery";
            this.drpValidStatusQuery.SelectedIndex = -1;
            this.drpValidStatusQuery.ShowCheckBox = false;
            this.drpValidStatusQuery.Size = new System.Drawing.Size(194, 20);
            this.drpValidStatusQuery.TabIndex = 222;
            this.drpValidStatusQuery.WidthType = UserControl.WidthTypes.Normal;
            this.drpValidStatusQuery.XAlign = 574;
            // 
            // drpCheckedStatusQuery
            // 
            this.drpCheckedStatusQuery.AllowEditOnlyChecked = false;
            this.drpCheckedStatusQuery.Caption = "审核状态";
            this.drpCheckedStatusQuery.Checked = false;
            this.drpCheckedStatusQuery.Location = new System.Drawing.Point(264, 77);
            this.drpCheckedStatusQuery.Name = "drpCheckedStatusQuery";
            this.drpCheckedStatusQuery.SelectedIndex = -1;
            this.drpCheckedStatusQuery.ShowCheckBox = false;
            this.drpCheckedStatusQuery.Size = new System.Drawing.Size(194, 20);
            this.drpCheckedStatusQuery.TabIndex = 221;
            this.drpCheckedStatusQuery.WidthType = UserControl.WidthTypes.Normal;
            this.drpCheckedStatusQuery.XAlign = 325;
            // 
            // txtKeywordQuery
            // 
            this.txtKeywordQuery.AllowEditOnlyChecked = true;
            this.txtKeywordQuery.AutoUpper = true;
            this.txtKeywordQuery.Caption = "关键字";
            this.txtKeywordQuery.Checked = false;
            this.txtKeywordQuery.EditType = UserControl.EditTypes.String;
            this.txtKeywordQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtKeywordQuery.Location = new System.Drawing.Point(34, 49);
            this.txtKeywordQuery.MaxLength = 40;
            this.txtKeywordQuery.Multiline = false;
            this.txtKeywordQuery.Name = "txtKeywordQuery";
            this.txtKeywordQuery.PasswordChar = '\0';
            this.txtKeywordQuery.ReadOnly = false;
            this.txtKeywordQuery.ShowCheckBox = false;
            this.txtKeywordQuery.Size = new System.Drawing.Size(182, 22);
            this.txtKeywordQuery.TabIndex = 220;
            this.txtKeywordQuery.TabNext = true;
            this.txtKeywordQuery.Value = "";
            this.txtKeywordQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtKeywordQuery.XAlign = 83;
            // 
            // txtDocnumQuery
            // 
            this.txtDocnumQuery.AllowEditOnlyChecked = true;
            this.txtDocnumQuery.AutoUpper = true;
            this.txtDocnumQuery.Caption = "文件编号";
            this.txtDocnumQuery.Checked = false;
            this.txtDocnumQuery.EditType = UserControl.EditTypes.String;
            this.txtDocnumQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDocnumQuery.Location = new System.Drawing.Point(264, 20);
            this.txtDocnumQuery.MaxLength = 40;
            this.txtDocnumQuery.Multiline = false;
            this.txtDocnumQuery.Name = "txtDocnumQuery";
            this.txtDocnumQuery.PasswordChar = '\0';
            this.txtDocnumQuery.ReadOnly = false;
            this.txtDocnumQuery.ShowCheckBox = false;
            this.txtDocnumQuery.Size = new System.Drawing.Size(194, 22);
            this.txtDocnumQuery.TabIndex = 219;
            this.txtDocnumQuery.TabNext = true;
            this.txtDocnumQuery.Value = "";
            this.txtDocnumQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtDocnumQuery.XAlign = 325;
            // 
            // txtMcodelistQuery
            // 
            this.txtMcodelistQuery.AllowEditOnlyChecked = true;
            this.txtMcodelistQuery.AutoUpper = true;
            this.txtMcodelistQuery.Caption = "产品代码";
            this.txtMcodelistQuery.Checked = false;
            this.txtMcodelistQuery.EditType = UserControl.EditTypes.String;
            this.txtMcodelistQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtMcodelistQuery.Location = new System.Drawing.Point(513, 20);
            this.txtMcodelistQuery.MaxLength = 40;
            this.txtMcodelistQuery.Multiline = false;
            this.txtMcodelistQuery.Name = "txtMcodelistQuery";
            this.txtMcodelistQuery.PasswordChar = '\0';
            this.txtMcodelistQuery.ReadOnly = false;
            this.txtMcodelistQuery.ShowCheckBox = false;
            this.txtMcodelistQuery.Size = new System.Drawing.Size(194, 22);
            this.txtMcodelistQuery.TabIndex = 218;
            this.txtMcodelistQuery.TabNext = true;
            this.txtMcodelistQuery.Value = "";
            this.txtMcodelistQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtMcodelistQuery.XAlign = 574;
            // 
            // txtDocnameQuery
            // 
            this.txtDocnameQuery.AllowEditOnlyChecked = true;
            this.txtDocnameQuery.AutoUpper = true;
            this.txtDocnameQuery.Caption = "文件名称";
            this.txtDocnameQuery.Checked = false;
            this.txtDocnameQuery.EditType = UserControl.EditTypes.String;
            this.txtDocnameQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDocnameQuery.Location = new System.Drawing.Point(22, 20);
            this.txtDocnameQuery.MaxLength = 40;
            this.txtDocnameQuery.Multiline = false;
            this.txtDocnameQuery.Name = "txtDocnameQuery";
            this.txtDocnameQuery.PasswordChar = '\0';
            this.txtDocnameQuery.ReadOnly = false;
            this.txtDocnameQuery.ShowCheckBox = false;
            this.txtDocnameQuery.Size = new System.Drawing.Size(194, 22);
            this.txtDocnameQuery.TabIndex = 0;
            this.txtDocnameQuery.TabNext = true;
            this.txtDocnameQuery.Value = "";
            this.txtDocnameQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtDocnameQuery.XAlign = 83;
            // 
            // btnGetMcodelist
            // 
            this.btnGetMcodelist.Location = new System.Drawing.Point(723, 20);
            this.btnGetMcodelist.Name = "btnGetMcodelist";
            this.btnGetMcodelist.Size = new System.Drawing.Size(34, 22);
            this.btnGetMcodelist.TabIndex = 211;
            this.btnGetMcodelist.Text = "...";
            this.btnGetMcodelist.UseVisualStyleBackColor = true;
            this.btnGetMcodelist.Click += new System.EventHandler(this.btnGetMcodelist_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuery.BackColor = System.Drawing.SystemColors.Control;
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.ButtonType = UserControl.ButtonTypes.None;
            this.btnQuery.Caption = "查询";
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.Location = new System.Drawing.Point(723, 75);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(88, 22);
            this.btnQuery.TabIndex = 208;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // drpDoctypeQuery
            // 
            this.drpDoctypeQuery.AllowEditOnlyChecked = false;
            this.drpDoctypeQuery.Caption = "文档类型";
            this.drpDoctypeQuery.Checked = false;
            this.drpDoctypeQuery.Location = new System.Drawing.Point(22, 77);
            this.drpDoctypeQuery.Name = "drpDoctypeQuery";
            this.drpDoctypeQuery.SelectedIndex = -1;
            this.drpDoctypeQuery.ShowCheckBox = false;
            this.drpDoctypeQuery.Size = new System.Drawing.Size(194, 20);
            this.drpDoctypeQuery.TabIndex = 25;
            this.drpDoctypeQuery.WidthType = UserControl.WidthTypes.Normal;
            this.drpDoctypeQuery.XAlign = 83;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnExit);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 462);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(873, 89);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(392, 55);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 31;
            this.btnExit.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ultraGridScrutiny);
            this.groupBox2.Location = new System.Drawing.Point(2, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(871, 384);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            // 
            // ultraGridScrutiny
            // 
            this.ultraGridScrutiny.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridScrutiny.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridScrutiny.Location = new System.Drawing.Point(3, 17);
            this.ultraGridScrutiny.Name = "ultraGridScrutiny";
            this.ultraGridScrutiny.Size = new System.Drawing.Size(865, 364);
            this.ultraGridScrutiny.TabIndex = 1;
            this.ultraGridScrutiny.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.ultraGridScrutiny_ClickCell);
            this.ultraGridScrutiny.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridScrutiny_InitializeLayout);
            // 
            // FDocView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 551);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "FDocView";
            this.Text = "文档查阅";
            this.Load += new System.EventHandler(this.FDocView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridScrutiny)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCLabelEdit txtMcodelistQuery;
        private UserControl.UCLabelEdit txtDocnameQuery;
        private System.Windows.Forms.Button btnGetMcodelist;
        private UserControl.UCButton btnQuery;
        private UserControl.UCLabelCombox drpDoctypeQuery;
        private System.Windows.Forms.GroupBox groupBox3;
        private UserControl.UCButton btnExit;
        private System.Windows.Forms.Button btnGetOplistQuery;
        private UserControl.UCLabelEdit txtOplistQuery;
        private UserControl.UCLabelEdit txtMemoQuery;
        private UserControl.UCLabelCombox drpValidStatusQuery;
        private UserControl.UCLabelCombox drpCheckedStatusQuery;
        private UserControl.UCLabelEdit txtKeywordQuery;
        private UserControl.UCLabelEdit txtDocnumQuery;
        private System.Windows.Forms.GroupBox groupBox2;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridScrutiny;

    }
}