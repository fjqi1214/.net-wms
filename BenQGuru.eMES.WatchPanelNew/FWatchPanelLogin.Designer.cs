namespace BenQGuru.eMES.WatchPanelNew
{
    partial class FWatchPanelLogin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FWatchPanelLogin));
            this.groupBoxWorkShop = new Infragistics.Win.Misc.UltraGroupBox();
            this.ucWorkshop = new UserControl.UCLabelCombox();
            this.rdoWorkShop = new System.Windows.Forms.RadioButton();
            this.groupBoxSSPanel = new Infragistics.Win.Misc.UltraGroupBox();
            this.ucWorkShopBySSCode = new UserControl.UCLabelCombox();
            this.ucProductLine = new UserControl.UCLabelCombox();
            this.rdoSSPanel = new System.Windows.Forms.RadioButton();
            this.btRun = new System.Windows.Forms.Button();
            this.btConfig = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.groupBoxWorkShop)).BeginInit();
            this.groupBoxWorkShop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBoxSSPanel)).BeginInit();
            this.groupBoxSSPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxWorkShop
            // 
            this.groupBoxWorkShop.Controls.Add(this.ucWorkshop);
            this.groupBoxWorkShop.Controls.Add(this.rdoWorkShop);
            this.groupBoxWorkShop.Location = new System.Drawing.Point(1, 12);
            this.groupBoxWorkShop.Name = "groupBoxWorkShop";
            this.groupBoxWorkShop.Size = new System.Drawing.Size(478, 58);
            this.groupBoxWorkShop.TabIndex = 0;
            this.groupBoxWorkShop.Text = "车间看板";
            // 
            // ucWorkshop
            // 
            this.ucWorkshop.AllowEditOnlyChecked = true;
            this.ucWorkshop.Caption = "车间";
            this.ucWorkshop.Checked = false;
            this.ucWorkshop.Location = new System.Drawing.Point(104, 22);
            this.ucWorkshop.Name = "ucWorkshop";
            this.ucWorkshop.SelectedIndex = -1;
            this.ucWorkshop.ShowCheckBox = false;
            this.ucWorkshop.Size = new System.Drawing.Size(237, 20);
            this.ucWorkshop.TabIndex = 1;
            this.ucWorkshop.WidthType = UserControl.WidthTypes.Long;
            this.ucWorkshop.XAlign = 141;
            this.ucWorkshop.Load += new System.EventHandler(this.ucWorkshop_Load);
            // 
            // rdoWorkShop
            // 
            this.rdoWorkShop.AutoSize = true;
            this.rdoWorkShop.Checked = true;
            this.rdoWorkShop.Location = new System.Drawing.Point(12, 25);
            this.rdoWorkShop.Name = "rdoWorkShop";
            this.rdoWorkShop.Size = new System.Drawing.Size(71, 16);
            this.rdoWorkShop.TabIndex = 0;
            this.rdoWorkShop.TabStop = true;
            this.rdoWorkShop.Text = "车间看板";
            this.rdoWorkShop.UseVisualStyleBackColor = true;
            this.rdoWorkShop.Click += new System.EventHandler(this.rdoWorkShop_Click);
            // 
            // groupBoxSSPanel
            // 
            this.groupBoxSSPanel.Controls.Add(this.ucWorkShopBySSCode);
            this.groupBoxSSPanel.Controls.Add(this.ucProductLine);
            this.groupBoxSSPanel.Controls.Add(this.rdoSSPanel);
            this.groupBoxSSPanel.Location = new System.Drawing.Point(1, 75);
            this.groupBoxSSPanel.Name = "groupBoxSSPanel";
            this.groupBoxSSPanel.Size = new System.Drawing.Size(478, 90);
            this.groupBoxSSPanel.TabIndex = 1;
            this.groupBoxSSPanel.Text = "产线看板";
            // 
            // ucWorkShopBySSCode
            // 
            this.ucWorkShopBySSCode.AllowEditOnlyChecked = true;
            this.ucWorkShopBySSCode.Caption = "车间";
            this.ucWorkShopBySSCode.Checked = false;
            this.ucWorkShopBySSCode.Location = new System.Drawing.Point(104, 24);
            this.ucWorkShopBySSCode.Name = "ucWorkShopBySSCode";
            this.ucWorkShopBySSCode.SelectedIndex = -1;
            this.ucWorkShopBySSCode.ShowCheckBox = false;
            this.ucWorkShopBySSCode.Size = new System.Drawing.Size(237, 20);
            this.ucWorkShopBySSCode.TabIndex = 3;
            this.ucWorkShopBySSCode.WidthType = UserControl.WidthTypes.Long;
            this.ucWorkShopBySSCode.XAlign = 141;
            this.ucWorkShopBySSCode.SelectedIndexChanged += new System.EventHandler(this.ucWorkShopBySSCode_SelectedIndexChanged);
            this.ucWorkShopBySSCode.Load += new System.EventHandler(this.ucWorkShopBySSCode_Load);
            // 
            // ucProductLine
            // 
            this.ucProductLine.AllowEditOnlyChecked = true;
            this.ucProductLine.Caption = "产线";
            this.ucProductLine.Checked = false;
            this.ucProductLine.Location = new System.Drawing.Point(104, 55);
            this.ucProductLine.Name = "ucProductLine";
            this.ucProductLine.SelectedIndex = -1;
            this.ucProductLine.ShowCheckBox = false;
            this.ucProductLine.Size = new System.Drawing.Size(237, 20);
            this.ucProductLine.TabIndex = 2;
            this.ucProductLine.WidthType = UserControl.WidthTypes.Long;
            this.ucProductLine.XAlign = 141;
            this.ucProductLine.Load += new System.EventHandler(this.ucProductLine_Load);
            // 
            // rdoSSPanel
            // 
            this.rdoSSPanel.AutoSize = true;
            this.rdoSSPanel.Location = new System.Drawing.Point(11, 27);
            this.rdoSSPanel.Name = "rdoSSPanel";
            this.rdoSSPanel.Size = new System.Drawing.Size(71, 16);
            this.rdoSSPanel.TabIndex = 0;
            this.rdoSSPanel.TabStop = true;
            this.rdoSSPanel.Text = "产线看板";
            this.rdoSSPanel.UseVisualStyleBackColor = true;
            this.rdoSSPanel.Click += new System.EventHandler(this.rdoSSPanel_Click);
            // 
            // btRun
            // 
            this.btRun.Location = new System.Drawing.Point(141, 174);
            this.btRun.Name = "btRun";
            this.btRun.Size = new System.Drawing.Size(75, 23);
            this.btRun.TabIndex = 4;
            this.btRun.Text = "运行";
            this.btRun.UseVisualStyleBackColor = true;
            this.btRun.Click += new System.EventHandler(this.btRun_Click);
            // 
            // btConfig
            // 
            this.btConfig.Location = new System.Drawing.Point(268, 174);
            this.btConfig.Name = "btConfig";
            this.btConfig.Size = new System.Drawing.Size(75, 23);
            this.btConfig.TabIndex = 5;
            this.btConfig.Text = "配置";
            this.btConfig.UseVisualStyleBackColor = true;
            this.btConfig.Click += new System.EventHandler(this.btConfig_Click);
            // 
            // FWatchPanelLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 206);
            this.Controls.Add(this.btConfig);
            this.Controls.Add(this.btRun);
            this.Controls.Add(this.groupBoxSSPanel);
            this.Controls.Add(this.groupBoxWorkShop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FWatchPanelLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "电子看板首页";
            this.Load += new System.EventHandler(this.FWatchPanelLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBoxWorkShop)).EndInit();
            this.groupBoxWorkShop.ResumeLayout(false);
            this.groupBoxWorkShop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBoxSSPanel)).EndInit();
            this.groupBoxSSPanel.ResumeLayout(false);
            this.groupBoxSSPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox groupBoxWorkShop;
        private Infragistics.Win.Misc.UltraGroupBox groupBoxSSPanel;
        private System.Windows.Forms.Button btRun;
        private System.Windows.Forms.Button btConfig;
        private System.Windows.Forms.RadioButton rdoWorkShop;
        private System.Windows.Forms.RadioButton rdoSSPanel;
        private UserControl.UCLabelCombox ucWorkshop;
        private UserControl.UCLabelCombox ucWorkShopBySSCode;
        private UserControl.UCLabelCombox ucProductLine;
    }
}

