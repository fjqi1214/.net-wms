namespace BenQGuru.eMES.Client
{
    partial class FBurn
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

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FBurn));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn0 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MoCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ItemCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RunningCard");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BurnInDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BurnInTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ForecastOutDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ForecastOutTime");
            this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
            this.panelButton = new System.Windows.Forms.Panel();
            this.btnBurnForcePass = new UserControl.UCButton();
            this.txtMem = new UserControl.UCLabelEdit();
            this.txtRunningCard = new UserControl.UCLabelEdit();
            this.rdoNG = new System.Windows.Forms.RadioButton();
            this.rdoGood = new System.Windows.Forms.RadioButton();
            this.btnExit = new UserControl.UCButton();
            this.txtGOMO = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoBurnOut = new System.Windows.Forms.RadioButton();
            this.rdoBurnIn = new System.Windows.Forms.RadioButton();
            this.bRCardLetterULE = new UserControl.UCLabelEdit();
            this.bRCardLenULE = new UserControl.UCLabelEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelItemDescription = new System.Windows.Forms.Label();
            this.txtMO = new UserControl.UCLabelEdit();
            this.txtItem = new UserControl.UCLabelEdit();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gridInfo = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panelButton.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraLabel6
            // 
            this.ultraLabel6.Location = new System.Drawing.Point(0, 0);
            this.ultraLabel6.Name = "ultraLabel6";
            this.ultraLabel6.Size = new System.Drawing.Size(100, 23);
            this.ultraLabel6.TabIndex = 0;
            // 
            // panelButton
            // 
            this.panelButton.Controls.Add(this.btnBurnForcePass);
            this.panelButton.Controls.Add(this.txtMem);
            this.panelButton.Controls.Add(this.txtRunningCard);
            this.panelButton.Controls.Add(this.rdoNG);
            this.panelButton.Controls.Add(this.rdoGood);
            this.panelButton.Controls.Add(this.btnExit);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(0, 400);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(904, 109);
            this.panelButton.TabIndex = 155;
            // 
            // btnBurnForcePass
            // 
            this.btnBurnForcePass.BackColor = System.Drawing.SystemColors.Control;
            this.btnBurnForcePass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBurnForcePass.BackgroundImage")));
            this.btnBurnForcePass.ButtonType = UserControl.ButtonTypes.Save;
            this.btnBurnForcePass.Caption = "老化强制通过";
            this.btnBurnForcePass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBurnForcePass.Enabled = false;
            this.btnBurnForcePass.Location = new System.Drawing.Point(280, 62);
            this.btnBurnForcePass.Name = "btnBurnForcePass";
            this.btnBurnForcePass.Size = new System.Drawing.Size(88, 22);
            this.btnBurnForcePass.TabIndex = 12;
            this.btnBurnForcePass.Click += new System.EventHandler(this.btnBurnForcePass_Click);
            // 
            // txtMem
            // 
            this.txtMem.AllowEditOnlyChecked = true;
            this.txtMem.AutoSelectAll = false;
            this.txtMem.AutoUpper = true;
            this.txtMem.Caption = "备注";
            this.txtMem.Checked = false;
            this.txtMem.EditType = UserControl.EditTypes.String;
            this.txtMem.Location = new System.Drawing.Point(562, 8);
            this.txtMem.MaxLength = 80;
            this.txtMem.Multiline = true;
            this.txtMem.Name = "txtMem";
            this.txtMem.PasswordChar = '\0';
            this.txtMem.ReadOnly = false;
            this.txtMem.ShowCheckBox = false;
            this.txtMem.Size = new System.Drawing.Size(237, 72);
            this.txtMem.TabIndex = 11;
            this.txtMem.TabNext = true;
            this.txtMem.Value = "";
            this.txtMem.WidthType = UserControl.WidthTypes.Long;
            this.txtMem.XAlign = 599;
            // 
            // txtRunningCard
            // 
            this.txtRunningCard.AllowEditOnlyChecked = true;
            this.txtRunningCard.AutoSelectAll = false;
            this.txtRunningCard.AutoUpper = true;
            this.txtRunningCard.Caption = "产品序列号";
            this.txtRunningCard.Checked = false;
            this.txtRunningCard.EditType = UserControl.EditTypes.String;
            this.txtRunningCard.Location = new System.Drawing.Point(265, 8);
            this.txtRunningCard.MaxLength = 40;
            this.txtRunningCard.Multiline = false;
            this.txtRunningCard.Name = "txtRunningCard";
            this.txtRunningCard.PasswordChar = '\0';
            this.txtRunningCard.ReadOnly = false;
            this.txtRunningCard.ShowCheckBox = false;
            this.txtRunningCard.Size = new System.Drawing.Size(273, 24);
            this.txtRunningCard.TabIndex = 2;
            this.txtRunningCard.TabNext = false;
            this.txtRunningCard.Value = "";
            this.txtRunningCard.WidthType = UserControl.WidthTypes.Long;
            this.txtRunningCard.XAlign = 338;
            this.txtRunningCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRunningCard_TxtboxKeyPress);
            // 
            // rdoNG
            // 
            this.rdoNG.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoNG.ForeColor = System.Drawing.Color.Red;
            this.rdoNG.Location = new System.Drawing.Point(104, 8);
            this.rdoNG.Name = "rdoNG";
            this.rdoNG.Size = new System.Drawing.Size(96, 24);
            this.rdoNG.TabIndex = 6;
            this.rdoNG.Tag = "1";
            this.rdoNG.Text = "不良品";
            this.rdoNG.CheckedChanged += new System.EventHandler(this.rdoNG_CheckedChanged);
            // 
            // rdoGood
            // 
            this.rdoGood.Checked = true;
            this.rdoGood.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoGood.ForeColor = System.Drawing.Color.Blue;
            this.rdoGood.Location = new System.Drawing.Point(9, 8);
            this.rdoGood.Name = "rdoGood";
            this.rdoGood.Size = new System.Drawing.Size(79, 24);
            this.rdoGood.TabIndex = 5;
            this.rdoGood.TabStop = true;
            this.rdoGood.Tag = "1";
            this.rdoGood.Text = "良品";
            this.rdoGood.CheckedChanged += new System.EventHandler(this.rdoGood_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(392, 62);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 5;
            // 
            // txtGOMO
            // 
            this.txtGOMO.AllowEditOnlyChecked = true;
            this.txtGOMO.AutoSelectAll = false;
            this.txtGOMO.AutoUpper = true;
            this.txtGOMO.Caption = "设定归属工单";
            this.txtGOMO.Checked = false;
            this.txtGOMO.EditType = UserControl.EditTypes.String;
            this.txtGOMO.Location = new System.Drawing.Point(8, 13);
            this.txtGOMO.MaxLength = 40;
            this.txtGOMO.Multiline = false;
            this.txtGOMO.Name = "txtGOMO";
            this.txtGOMO.PasswordChar = '\0';
            this.txtGOMO.ReadOnly = false;
            this.txtGOMO.ShowCheckBox = true;
            this.txtGOMO.Size = new System.Drawing.Size(234, 24);
            this.txtGOMO.TabIndex = 1;
            this.txtGOMO.TabNext = true;
            this.txtGOMO.Value = "";
            this.txtGOMO.WidthType = UserControl.WidthTypes.Normal;
            this.txtGOMO.XAlign = 109;
            this.txtGOMO.CheckBoxCheckedChanged += new System.EventHandler(this.txtGOMO_CheckBoxCheckedChanged);
            this.txtGOMO.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGOMO_TxtboxKeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoBurnOut);
            this.groupBox2.Controls.Add(this.rdoBurnIn);
            this.groupBox2.Controls.Add(this.bRCardLetterULE);
            this.groupBox2.Controls.Add(this.bRCardLenULE);
            this.groupBox2.Controls.Add(this.txtGOMO);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(904, 70);
            this.groupBox2.TabIndex = 157;
            this.groupBox2.TabStop = false;
            // 
            // rdoBurnOut
            // 
            this.rdoBurnOut.Font = new System.Drawing.Font("宋体", 9F);
            this.rdoBurnOut.ForeColor = System.Drawing.Color.Black;
            this.rdoBurnOut.Location = new System.Drawing.Point(107, 40);
            this.rdoBurnOut.Name = "rdoBurnOut";
            this.rdoBurnOut.Size = new System.Drawing.Size(96, 24);
            this.rdoBurnOut.TabIndex = 30;
            this.rdoBurnOut.Tag = "1";
            this.rdoBurnOut.Text = "老化出";
            this.rdoBurnOut.CheckedChanged += new System.EventHandler(this.rdoBurnOut_CheckedChanged);
            // 
            // rdoBurnIn
            // 
            this.rdoBurnIn.Checked = true;
            this.rdoBurnIn.Font = new System.Drawing.Font("宋体", 9F);
            this.rdoBurnIn.ForeColor = System.Drawing.Color.Black;
            this.rdoBurnIn.Location = new System.Drawing.Point(7, 40);
            this.rdoBurnIn.Name = "rdoBurnIn";
            this.rdoBurnIn.Size = new System.Drawing.Size(79, 24);
            this.rdoBurnIn.TabIndex = 29;
            this.rdoBurnIn.TabStop = true;
            this.rdoBurnIn.Tag = "1";
            this.rdoBurnIn.Text = "老化进";
            this.rdoBurnIn.CheckedChanged += new System.EventHandler(this.rdoBurnIn_CheckedChanged);
            // 
            // bRCardLetterULE
            // 
            this.bRCardLetterULE.AllowEditOnlyChecked = true;
            this.bRCardLetterULE.AutoSelectAll = false;
            this.bRCardLetterULE.AutoUpper = true;
            this.bRCardLetterULE.Caption = "产品序列号首字符串";
            this.bRCardLetterULE.Checked = false;
            this.bRCardLetterULE.EditType = UserControl.EditTypes.String;
            this.bRCardLetterULE.Enabled = false;
            this.bRCardLetterULE.Location = new System.Drawing.Point(520, 13);
            this.bRCardLetterULE.MaxLength = 40;
            this.bRCardLetterULE.Multiline = false;
            this.bRCardLetterULE.Name = "bRCardLetterULE";
            this.bRCardLetterULE.PasswordChar = '\0';
            this.bRCardLetterULE.ReadOnly = false;
            this.bRCardLetterULE.ShowCheckBox = true;
            this.bRCardLetterULE.Size = new System.Drawing.Size(270, 24);
            this.bRCardLetterULE.TabIndex = 28;
            this.bRCardLetterULE.TabNext = false;
            this.bRCardLetterULE.Value = "";
            this.bRCardLetterULE.WidthType = UserControl.WidthTypes.Normal;
            this.bRCardLetterULE.XAlign = 657;
            // 
            // bRCardLenULE
            // 
            this.bRCardLenULE.AllowEditOnlyChecked = true;
            this.bRCardLenULE.AutoSelectAll = false;
            this.bRCardLenULE.AutoUpper = true;
            this.bRCardLenULE.Caption = "产品序列号长度";
            this.bRCardLenULE.Checked = false;
            this.bRCardLenULE.EditType = UserControl.EditTypes.Integer;
            this.bRCardLenULE.Enabled = false;
            this.bRCardLenULE.Location = new System.Drawing.Point(259, 13);
            this.bRCardLenULE.MaxLength = 40;
            this.bRCardLenULE.Multiline = false;
            this.bRCardLenULE.Name = "bRCardLenULE";
            this.bRCardLenULE.PasswordChar = '\0';
            this.bRCardLenULE.ReadOnly = false;
            this.bRCardLenULE.ShowCheckBox = true;
            this.bRCardLenULE.Size = new System.Drawing.Size(246, 24);
            this.bRCardLenULE.TabIndex = 27;
            this.bRCardLenULE.TabNext = false;
            this.bRCardLenULE.Value = "";
            this.bRCardLenULE.WidthType = UserControl.WidthTypes.Normal;
            this.bRCardLenULE.XAlign = 372;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelItemDescription);
            this.groupBox1.Controls.Add(this.txtMO);
            this.groupBox1.Controls.Add(this.txtItem);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(904, 50);
            this.groupBox1.TabIndex = 159;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "产品信息";
            // 
            // labelItemDescription
            // 
            this.labelItemDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemDescription.Location = new System.Drawing.Point(462, 15);
            this.labelItemDescription.Name = "labelItemDescription";
            this.labelItemDescription.Size = new System.Drawing.Size(428, 31);
            this.labelItemDescription.TabIndex = 4;
            this.labelItemDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMO
            // 
            this.txtMO.AllowEditOnlyChecked = true;
            this.txtMO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMO.AutoSelectAll = false;
            this.txtMO.AutoUpper = true;
            this.txtMO.Caption = "工单";
            this.txtMO.Checked = false;
            this.txtMO.EditType = UserControl.EditTypes.String;
            this.txtMO.Location = new System.Drawing.Point(286, 21);
            this.txtMO.MaxLength = 40;
            this.txtMO.Multiline = false;
            this.txtMO.Name = "txtMO";
            this.txtMO.PasswordChar = '\0';
            this.txtMO.ReadOnly = true;
            this.txtMO.ShowCheckBox = false;
            this.txtMO.Size = new System.Drawing.Size(170, 24);
            this.txtMO.TabIndex = 3;
            this.txtMO.TabNext = true;
            this.txtMO.Value = "";
            this.txtMO.WidthType = UserControl.WidthTypes.Normal;
            this.txtMO.XAlign = 323;
            // 
            // txtItem
            // 
            this.txtItem.AllowEditOnlyChecked = true;
            this.txtItem.AutoSelectAll = false;
            this.txtItem.AutoUpper = true;
            this.txtItem.Caption = "产品";
            this.txtItem.Checked = false;
            this.txtItem.EditType = UserControl.EditTypes.String;
            this.txtItem.Location = new System.Drawing.Point(61, 21);
            this.txtItem.MaxLength = 40;
            this.txtItem.Multiline = false;
            this.txtItem.Name = "txtItem";
            this.txtItem.PasswordChar = '\0';
            this.txtItem.ReadOnly = true;
            this.txtItem.ShowCheckBox = false;
            this.txtItem.Size = new System.Drawing.Size(170, 24);
            this.txtItem.TabIndex = 0;
            this.txtItem.TabNext = true;
            this.txtItem.Value = "";
            this.txtItem.WidthType = UserControl.WidthTypes.Normal;
            this.txtItem.XAlign = 98;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gridInfo);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(0, 120);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(904, 277);
            this.groupBox3.TabIndex = 160;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "老化信息";
            // 
            // gridInfo
            // 
            this.gridInfo.Cursor = System.Windows.Forms.Cursors.Default;
            ultraGridColumn0.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn0.Header.Caption = "工单";
            ultraGridColumn0.Header.VisiblePosition = 0;
            ultraGridColumn1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn1.Header.Caption = "产品";
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridColumn2.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn2.Header.Caption = "产品序列号";
            ultraGridColumn2.Header.VisiblePosition = 2;
            ultraGridColumn3.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn3.Header.Caption = "老化开始日期";
            ultraGridColumn3.Header.VisiblePosition = 3;
            ultraGridColumn4.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn4.Header.Caption = "老化开始时间";
            ultraGridColumn4.Header.VisiblePosition = 4;
            ultraGridColumn5.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn5.Header.Caption = "预计结束日期";
            ultraGridColumn5.Header.VisiblePosition = 5;
            ultraGridColumn6.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn6.Header.Caption = "预计结束时间";
            ultraGridColumn6.Header.VisiblePosition = 6;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn0,
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6});
            this.gridInfo.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.gridInfo.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.gridInfo.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.AutoFixed;
            this.gridInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridInfo.Location = new System.Drawing.Point(3, 17);
            this.gridInfo.Name = "gridInfo";
            this.gridInfo.Size = new System.Drawing.Size(898, 257);
            this.gridInfo.TabIndex = 20;
            // 
            // FBurn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(904, 509);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelButton);
            this.KeyPreview = true;
            this.Name = "FBurn";
            this.Text = "老化采集";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.FBurn_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FBurn_FormClosed);
            this.Load += new System.EventHandler(this.FBurn_Load);
            this.panelButton.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridInfo)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private Infragistics.Win.Misc.UltraLabel ultraLabel6;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.GroupBox groupBox2;
        public UserControl.UCLabelEdit txtRunningCard;
        //private ActionOnLineHelper dataCollect = null;
        private UserControl.UCButton btnExit;
        private System.Windows.Forms.RadioButton rdoGood;
        private System.Windows.Forms.RadioButton rdoNG;
        private UserControl.UCLabelEdit txtGOMO;
        private UserControl.Messages globeMSG = new UserControl.Messages();
        private UserControl.UCLabelEdit bRCardLetterULE;
        private UserControl.UCLabelEdit bRCardLenULE;
        private UserControl.UCLabelEdit txtMem;
        private UserControl.UCButton btnBurnForcePass;
        private System.Windows.Forms.RadioButton rdoBurnOut;
        private System.Windows.Forms.RadioButton rdoBurnIn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelItemDescription;
        private UserControl.UCLabelEdit txtMO;
        private UserControl.UCLabelEdit txtItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridInfo;
    }
}