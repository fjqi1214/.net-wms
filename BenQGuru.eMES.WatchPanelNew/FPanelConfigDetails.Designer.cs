namespace BenQGuru.eMES.WatchPanelNew
{
    partial class FPanelConfigDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPanelConfigDetails));
            this.groupBoxBaseInfo = new Infragistics.Win.Misc.UltraGroupBox();
            this.ucWorkShopCode = new UserControl.UCLabelEdit();
            this.ucSSCode = new UserControl.UCLabelEdit();
            this.groupBoxPanelInfo = new Infragistics.Win.Misc.UltraGroupBox();
            this.choPanelDetails = new System.Windows.Forms.CheckBox();
            this.choSynthesized = new System.Windows.Forms.CheckBox();
            this.ButtonSet = new Infragistics.Win.Misc.UltraButton();
            this.choStandBy = new System.Windows.Forms.CheckBox();
            this.groupBoxRefresh = new Infragistics.Win.Misc.UltraGroupBox();
            this.lblUnitSeconds = new System.Windows.Forms.Label();
            this.ucScreenRefresh = new UserControl.UCLabelEdit();
            this.lblUnit = new System.Windows.Forms.Label();
            this.ucAutoRefresh = new UserControl.UCLabelEdit();
            this.groupPanelDetails = new Infragistics.Win.Misc.UltraGroupBox();
            this.lblUnits = new System.Windows.Forms.Label();
            this.ucPageScrolling = new UserControl.UCLabelEdit();
            this.lblPCS = new System.Windows.Forms.Label();
            this.ucMaxLineCount = new UserControl.UCLabelEdit();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.rdoMonth = new System.Windows.Forms.RadioButton();
            this.rdoWeek = new System.Windows.Forms.RadioButton();
            this.rdoDay = new System.Windows.Forms.RadioButton();
            this.ButtonSave = new Infragistics.Win.Misc.UltraButton();
            this.ButtonCancel = new Infragistics.Win.Misc.UltraButton();
            this.ButtonClosed = new Infragistics.Win.Misc.UltraButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupBoxBaseInfo)).BeginInit();
            this.groupBoxBaseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBoxPanelInfo)).BeginInit();
            this.groupBoxPanelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBoxRefresh)).BeginInit();
            this.groupBoxRefresh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupPanelDetails)).BeginInit();
            this.groupPanelDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxBaseInfo
            // 
            this.groupBoxBaseInfo.Controls.Add(this.ucWorkShopCode);
            this.groupBoxBaseInfo.Controls.Add(this.ucSSCode);
            this.groupBoxBaseInfo.Location = new System.Drawing.Point(9, 8);
            this.groupBoxBaseInfo.Name = "groupBoxBaseInfo";
            this.groupBoxBaseInfo.Size = new System.Drawing.Size(557, 55);
            this.groupBoxBaseInfo.TabIndex = 1;
            this.groupBoxBaseInfo.Text = "基础信息";
            // 
            // ucWorkShopCode
            // 
            this.ucWorkShopCode.AllowEditOnlyChecked = true;
            this.ucWorkShopCode.AutoSelectAll = false;
            this.ucWorkShopCode.AutoUpper = true;
            this.ucWorkShopCode.Caption = "当前车间";
            this.ucWorkShopCode.Checked = false;
            this.ucWorkShopCode.EditType = UserControl.EditTypes.String;
            this.ucWorkShopCode.Enabled = false;
            this.ucWorkShopCode.Location = new System.Drawing.Point(39, 22);
            this.ucWorkShopCode.MaxLength = 40;
            this.ucWorkShopCode.Multiline = false;
            this.ucWorkShopCode.Name = "ucWorkShopCode";
            this.ucWorkShopCode.PasswordChar = '\0';
            this.ucWorkShopCode.ReadOnly = false;
            this.ucWorkShopCode.ShowCheckBox = false;
            this.ucWorkShopCode.Size = new System.Drawing.Size(194, 24);
            this.ucWorkShopCode.TabIndex = 1;
            this.ucWorkShopCode.TabNext = true;
            this.ucWorkShopCode.Value = "";
            this.ucWorkShopCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucWorkShopCode.XAlign = 100;
            // 
            // ucSSCode
            // 
            this.ucSSCode.AllowEditOnlyChecked = true;
            this.ucSSCode.AutoSelectAll = false;
            this.ucSSCode.AutoUpper = true;
            this.ucSSCode.Caption = "当前产线";
            this.ucSSCode.Checked = false;
            this.ucSSCode.EditType = UserControl.EditTypes.String;
            this.ucSSCode.Enabled = false;
            this.ucSSCode.Location = new System.Drawing.Point(305, 22);
            this.ucSSCode.MaxLength = 40;
            this.ucSSCode.Multiline = false;
            this.ucSSCode.Name = "ucSSCode";
            this.ucSSCode.PasswordChar = '\0';
            this.ucSSCode.ReadOnly = false;
            this.ucSSCode.ShowCheckBox = false;
            this.ucSSCode.Size = new System.Drawing.Size(194, 24);
            this.ucSSCode.TabIndex = 0;
            this.ucSSCode.TabNext = true;
            this.ucSSCode.Value = "";
            this.ucSSCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucSSCode.XAlign = 366;
            // 
            // groupBoxPanelInfo
            // 
            this.groupBoxPanelInfo.Controls.Add(this.choPanelDetails);
            this.groupBoxPanelInfo.Controls.Add(this.choSynthesized);
            this.groupBoxPanelInfo.Controls.Add(this.ButtonSet);
            this.groupBoxPanelInfo.Controls.Add(this.choStandBy);
            this.groupBoxPanelInfo.Location = new System.Drawing.Point(9, 69);
            this.groupBoxPanelInfo.Name = "groupBoxPanelInfo";
            this.groupBoxPanelInfo.Size = new System.Drawing.Size(557, 55);
            this.groupBoxPanelInfo.TabIndex = 1;
            this.groupBoxPanelInfo.Text = "看板类型";
            // 
            // choPanelDetails
            // 
            this.choPanelDetails.AutoSize = true;
            this.choPanelDetails.Location = new System.Drawing.Point(427, 26);
            this.choPanelDetails.Name = "choPanelDetails";
            this.choPanelDetails.Size = new System.Drawing.Size(72, 16);
            this.choPanelDetails.TabIndex = 3;
            this.choPanelDetails.Text = "看板明细";
            this.choPanelDetails.UseVisualStyleBackColor = true;
            this.choPanelDetails.CheckedChanged += new System.EventHandler(this.choPanelDetails_CheckedChanged);
            this.choPanelDetails.Click += new System.EventHandler(this.choPanelDetails_Click);
            // 
            // choSynthesized
            // 
            this.choSynthesized.AutoSize = true;
            this.choSynthesized.Location = new System.Drawing.Point(287, 26);
            this.choSynthesized.Name = "choSynthesized";
            this.choSynthesized.Size = new System.Drawing.Size(72, 16);
            this.choSynthesized.TabIndex = 2;
            this.choSynthesized.Text = "综合看板";
            this.choSynthesized.UseVisualStyleBackColor = true;
            this.choSynthesized.Click += new System.EventHandler(this.choSynthesized_Click);
            // 
            // ButtonSet
            // 
            this.ButtonSet.Location = new System.Drawing.Point(144, 22);
            this.ButtonSet.Name = "ButtonSet";
            this.ButtonSet.Size = new System.Drawing.Size(76, 24);
            this.ButtonSet.TabIndex = 1;
            this.ButtonSet.Text = "设定";
            this.ButtonSet.Click += new System.EventHandler(this.ButtonSet_Click);
            // 
            // choStandBy
            // 
            this.choStandBy.AutoSize = true;
            this.choStandBy.Location = new System.Drawing.Point(39, 26);
            this.choStandBy.Name = "choStandBy";
            this.choStandBy.Size = new System.Drawing.Size(72, 16);
            this.choStandBy.TabIndex = 0;
            this.choStandBy.Text = "待机内容";
            this.choStandBy.UseVisualStyleBackColor = true;
            this.choStandBy.Click += new System.EventHandler(this.choStandBy_Click);
            // 
            // groupBoxRefresh
            // 
            this.groupBoxRefresh.Controls.Add(this.lblUnitSeconds);
            this.groupBoxRefresh.Controls.Add(this.ucScreenRefresh);
            this.groupBoxRefresh.Controls.Add(this.lblUnit);
            this.groupBoxRefresh.Controls.Add(this.ucAutoRefresh);
            this.groupBoxRefresh.Location = new System.Drawing.Point(9, 130);
            this.groupBoxRefresh.Name = "groupBoxRefresh";
            this.groupBoxRefresh.Size = new System.Drawing.Size(557, 55);
            this.groupBoxRefresh.TabIndex = 0;
            this.groupBoxRefresh.Text = "刷新频率";
            // 
            // lblUnitSeconds
            // 
            this.lblUnitSeconds.AutoSize = true;
            this.lblUnitSeconds.Location = new System.Drawing.Point(496, 27);
            this.lblUnitSeconds.Name = "lblUnitSeconds";
            this.lblUnitSeconds.Size = new System.Drawing.Size(17, 12);
            this.lblUnitSeconds.TabIndex = 3;
            this.lblUnitSeconds.Text = "秒";
            // 
            // ucScreenRefresh
            // 
            this.ucScreenRefresh.AllowEditOnlyChecked = true;
            this.ucScreenRefresh.AutoSelectAll = false;
            this.ucScreenRefresh.AutoUpper = true;
            this.ucScreenRefresh.Caption = "切换画面频率";
            this.ucScreenRefresh.Checked = false;
            this.ucScreenRefresh.EditType = UserControl.EditTypes.Integer;
            this.ucScreenRefresh.Location = new System.Drawing.Point(305, 22);
            this.ucScreenRefresh.MaxLength = 10;
            this.ucScreenRefresh.Multiline = false;
            this.ucScreenRefresh.Name = "ucScreenRefresh";
            this.ucScreenRefresh.PasswordChar = '\0';
            this.ucScreenRefresh.ReadOnly = false;
            this.ucScreenRefresh.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ucScreenRefresh.ShowCheckBox = false;
            this.ucScreenRefresh.Size = new System.Drawing.Size(185, 24);
            this.ucScreenRefresh.TabIndex = 2;
            this.ucScreenRefresh.TabNext = true;
            this.ucScreenRefresh.Value = "";
            this.ucScreenRefresh.WidthType = UserControl.WidthTypes.Small;
            this.ucScreenRefresh.XAlign = 390;
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(229, 27);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(17, 12);
            this.lblUnit.TabIndex = 1;
            this.lblUnit.Text = "秒";
            // 
            // ucAutoRefresh
            // 
            this.ucAutoRefresh.AllowEditOnlyChecked = true;
            this.ucAutoRefresh.AutoSelectAll = false;
            this.ucAutoRefresh.AutoUpper = true;
            this.ucAutoRefresh.Caption = "自动刷新频率";
            this.ucAutoRefresh.Checked = false;
            this.ucAutoRefresh.EditType = UserControl.EditTypes.Integer;
            this.ucAutoRefresh.Location = new System.Drawing.Point(38, 22);
            this.ucAutoRefresh.MaxLength = 10;
            this.ucAutoRefresh.Multiline = false;
            this.ucAutoRefresh.Name = "ucAutoRefresh";
            this.ucAutoRefresh.PasswordChar = '\0';
            this.ucAutoRefresh.ReadOnly = false;
            this.ucAutoRefresh.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ucAutoRefresh.ShowCheckBox = false;
            this.ucAutoRefresh.Size = new System.Drawing.Size(185, 24);
            this.ucAutoRefresh.TabIndex = 1;
            this.ucAutoRefresh.TabNext = true;
            this.ucAutoRefresh.Value = "";
            this.ucAutoRefresh.WidthType = UserControl.WidthTypes.Small;
            this.ucAutoRefresh.XAlign = 123;
            // 
            // groupPanelDetails
            // 
            this.groupPanelDetails.Controls.Add(this.lblUnits);
            this.groupPanelDetails.Controls.Add(this.ucPageScrolling);
            this.groupPanelDetails.Controls.Add(this.lblPCS);
            this.groupPanelDetails.Controls.Add(this.ucMaxLineCount);
            this.groupPanelDetails.Location = new System.Drawing.Point(9, 191);
            this.groupPanelDetails.Name = "groupPanelDetails";
            this.groupPanelDetails.Size = new System.Drawing.Size(557, 55);
            this.groupPanelDetails.TabIndex = 3;
            this.groupPanelDetails.Text = "看板明细页面设置";
            // 
            // lblUnits
            // 
            this.lblUnits.AutoSize = true;
            this.lblUnits.Location = new System.Drawing.Point(496, 29);
            this.lblUnits.Name = "lblUnits";
            this.lblUnits.Size = new System.Drawing.Size(17, 12);
            this.lblUnits.TabIndex = 4;
            this.lblUnits.Text = "秒";
            // 
            // ucPageScrolling
            // 
            this.ucPageScrolling.AllowEditOnlyChecked = true;
            this.ucPageScrolling.AutoSelectAll = false;
            this.ucPageScrolling.AutoUpper = true;
            this.ucPageScrolling.Caption = "页面滚动频率";
            this.ucPageScrolling.Checked = false;
            this.ucPageScrolling.EditType = UserControl.EditTypes.Integer;
            this.ucPageScrolling.Location = new System.Drawing.Point(305, 23);
            this.ucPageScrolling.MaxLength = 10;
            this.ucPageScrolling.Multiline = false;
            this.ucPageScrolling.Name = "ucPageScrolling";
            this.ucPageScrolling.PasswordChar = '\0';
            this.ucPageScrolling.ReadOnly = false;
            this.ucPageScrolling.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ucPageScrolling.ShowCheckBox = false;
            this.ucPageScrolling.Size = new System.Drawing.Size(185, 24);
            this.ucPageScrolling.TabIndex = 3;
            this.ucPageScrolling.TabNext = true;
            this.ucPageScrolling.Value = "";
            this.ucPageScrolling.WidthType = UserControl.WidthTypes.Small;
            this.ucPageScrolling.XAlign = 390;
            // 
            // lblPCS
            // 
            this.lblPCS.AutoSize = true;
            this.lblPCS.Location = new System.Drawing.Point(229, 28);
            this.lblPCS.Name = "lblPCS";
            this.lblPCS.Size = new System.Drawing.Size(29, 12);
            this.lblPCS.TabIndex = 2;
            this.lblPCS.Text = "5~50";
            // 
            // ucMaxLineCount
            // 
            this.ucMaxLineCount.AllowEditOnlyChecked = true;
            this.ucMaxLineCount.AutoSelectAll = false;
            this.ucMaxLineCount.AutoUpper = true;
            this.ucMaxLineCount.Caption = "每页显示行数";
            this.ucMaxLineCount.Checked = false;
            this.ucMaxLineCount.EditType = UserControl.EditTypes.Integer;
            this.ucMaxLineCount.Location = new System.Drawing.Point(38, 23);
            this.ucMaxLineCount.MaxLength = 10;
            this.ucMaxLineCount.Multiline = false;
            this.ucMaxLineCount.Name = "ucMaxLineCount";
            this.ucMaxLineCount.PasswordChar = '\0';
            this.ucMaxLineCount.ReadOnly = false;
            this.ucMaxLineCount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ucMaxLineCount.ShowCheckBox = false;
            this.ucMaxLineCount.Size = new System.Drawing.Size(185, 24);
            this.ucMaxLineCount.TabIndex = 0;
            this.ucMaxLineCount.TabNext = true;
            this.ucMaxLineCount.Value = "";
            this.ucMaxLineCount.WidthType = UserControl.WidthTypes.Small;
            this.ucMaxLineCount.XAlign = 123;
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.rdoMonth);
            this.ultraGroupBox1.Controls.Add(this.rdoWeek);
            this.ultraGroupBox1.Controls.Add(this.rdoDay);
            this.ultraGroupBox1.Location = new System.Drawing.Point(9, 252);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(557, 55);
            this.ultraGroupBox1.TabIndex = 4;
            this.ultraGroupBox1.Text = "时间维度";
            // 
            // rdoMonth
            // 
            this.rdoMonth.AutoSize = true;
            this.rdoMonth.Location = new System.Drawing.Point(376, 27);
            this.rdoMonth.Name = "rdoMonth";
            this.rdoMonth.Size = new System.Drawing.Size(83, 16);
            this.rdoMonth.TabIndex = 2;
            this.rdoMonth.Text = "按月分时段";
            this.rdoMonth.UseVisualStyleBackColor = true;
            // 
            // rdoWeek
            // 
            this.rdoWeek.AutoSize = true;
            this.rdoWeek.Location = new System.Drawing.Point(207, 27);
            this.rdoWeek.Name = "rdoWeek";
            this.rdoWeek.Size = new System.Drawing.Size(83, 16);
            this.rdoWeek.TabIndex = 1;
            this.rdoWeek.Text = "按周分时段";
            this.rdoWeek.UseVisualStyleBackColor = true;
            // 
            // rdoDay
            // 
            this.rdoDay.AutoSize = true;
            this.rdoDay.Checked = true;
            this.rdoDay.Location = new System.Drawing.Point(38, 27);
            this.rdoDay.Name = "rdoDay";
            this.rdoDay.Size = new System.Drawing.Size(83, 16);
            this.rdoDay.TabIndex = 0;
            this.rdoDay.TabStop = true;
            this.rdoDay.Text = "按天分时段";
            this.rdoDay.UseVisualStyleBackColor = true;
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(118, 320);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(76, 24);
            this.ButtonSave.TabIndex = 5;
            this.ButtonSave.Text = "保存";
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(248, 320);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(76, 24);
            this.ButtonCancel.TabIndex = 6;
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonClosed
            // 
            this.ButtonClosed.Location = new System.Drawing.Point(378, 320);
            this.ButtonClosed.Name = "ButtonClosed";
            this.ButtonClosed.Size = new System.Drawing.Size(76, 24);
            this.ButtonClosed.TabIndex = 7;
            this.ButtonClosed.Text = "关闭";
            this.ButtonClosed.Click += new System.EventHandler(this.ButtonClosed_Click);
            // 
            // FPanelConfigDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 356);
            this.Controls.Add(this.ButtonClosed);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ultraGroupBox1);
            this.Controls.Add(this.groupPanelDetails);
            this.Controls.Add(this.groupBoxRefresh);
            this.Controls.Add(this.groupBoxPanelInfo);
            this.Controls.Add(this.groupBoxBaseInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FPanelConfigDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "电子看板配置项维护";
            this.Load += new System.EventHandler(this.FPanelConfigDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBoxBaseInfo)).EndInit();
            this.groupBoxBaseInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBoxPanelInfo)).EndInit();
            this.groupBoxPanelInfo.ResumeLayout(false);
            this.groupBoxPanelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBoxRefresh)).EndInit();
            this.groupBoxRefresh.ResumeLayout(false);
            this.groupBoxRefresh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupPanelDetails)).EndInit();
            this.groupPanelDetails.ResumeLayout(false);
            this.groupPanelDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox groupBoxBaseInfo;
        private UserControl.UCLabelEdit ucWorkShopCode;
        private UserControl.UCLabelEdit ucSSCode;
        private Infragistics.Win.Misc.UltraGroupBox groupBoxPanelInfo;
        private System.Windows.Forms.CheckBox choStandBy;
        private Infragistics.Win.Misc.UltraButton ButtonSet;
        private System.Windows.Forms.CheckBox choPanelDetails;
        private System.Windows.Forms.CheckBox choSynthesized;
        private Infragistics.Win.Misc.UltraGroupBox groupBoxRefresh;
        private System.Windows.Forms.Label lblUnit;
        private UserControl.UCLabelEdit ucAutoRefresh;
        private System.Windows.Forms.Label lblUnitSeconds;
        private UserControl.UCLabelEdit ucScreenRefresh;
        private Infragistics.Win.Misc.UltraGroupBox groupPanelDetails;
        private UserControl.UCLabelEdit ucMaxLineCount;
        private System.Windows.Forms.Label lblPCS;
        private UserControl.UCLabelEdit ucPageScrolling;
        private System.Windows.Forms.Label lblUnits;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraButton ButtonSave;
        private Infragistics.Win.Misc.UltraButton ButtonCancel;
        private System.Windows.Forms.RadioButton rdoDay;
        private System.Windows.Forms.RadioButton rdoMonth;
        private System.Windows.Forms.RadioButton rdoWeek;
        private Infragistics.Win.Misc.UltraButton ButtonClosed;
    }
}