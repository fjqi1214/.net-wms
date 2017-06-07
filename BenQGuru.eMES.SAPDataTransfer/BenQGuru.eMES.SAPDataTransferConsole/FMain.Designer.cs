namespace BenQGuru.eMES.SAPDataTransferConsole
{
    partial class FMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonExit = new System.Windows.Forms.ToolStripButton();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.listBoxServiceList = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertyGridArguments = new System.Windows.Forms.PropertyGrid();
            this.toolStripButtonCleanOldLog = new System.Windows.Forms.ToolStripButton();
            this.serviceInfoPanelMain = new BenQGuru.eMES.SAPDataTransferConsole.ServiceInfoPanel();
            this.toolStripMain.SuspendLayout();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonFind,
            this.toolStripButtonRun,
            this.toolStripButtonCleanOldLog,
            this.toolStripSeparator1,
            this.toolStripButtonExit});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(797, 25);
            this.toolStripMain.TabIndex = 2;
            // 
            // toolStripButtonFind
            // 
            this.toolStripButtonFind.Image = global::BenQGuru.eMES.SAPDataTransferConsole.Properties.Resources.Find;
            this.toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFind.Name = "toolStripButtonFind";
            this.toolStripButtonFind.Size = new System.Drawing.Size(75, 22);
            this.toolStripButtonFind.Text = "服务列表";
            this.toolStripButtonFind.Click += new System.EventHandler(this.toolStripButtonFind_Click);
            // 
            // toolStripButtonRun
            // 
            this.toolStripButtonRun.Image = global::BenQGuru.eMES.SAPDataTransferConsole.Properties.Resources.Generate;
            this.toolStripButtonRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRun.Name = "toolStripButtonRun";
            this.toolStripButtonRun.Size = new System.Drawing.Size(51, 22);
            this.toolStripButtonRun.Text = "执行";
            this.toolStripButtonRun.Click += new System.EventHandler(this.toolStripButtonRun_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonExit
            // 
            this.toolStripButtonExit.Image = global::BenQGuru.eMES.SAPDataTransferConsole.Properties.Resources.Exit;
            this.toolStripButtonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExit.Name = "toolStripButtonExit";
            this.toolStripButtonExit.Size = new System.Drawing.Size(51, 22);
            this.toolStripButtonExit.Text = "退出";
            this.toolStripButtonExit.Click += new System.EventHandler(this.toolStripButtonExit_Click);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 25);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.listBoxServiceList);
            this.splitContainerMain.Panel1MinSize = 200;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainerMain.Size = new System.Drawing.Size(797, 497);
            this.splitContainerMain.SplitterDistance = 228;
            this.splitContainerMain.SplitterWidth = 6;
            this.splitContainerMain.TabIndex = 3;
            // 
            // listBoxServiceList
            // 
            this.listBoxServiceList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxServiceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxServiceList.FormattingEnabled = true;
            this.listBoxServiceList.ItemHeight = 15;
            this.listBoxServiceList.Location = new System.Drawing.Point(0, 0);
            this.listBoxServiceList.Name = "listBoxServiceList";
            this.listBoxServiceList.Size = new System.Drawing.Size(228, 497);
            this.listBoxServiceList.TabIndex = 0;
            this.listBoxServiceList.SelectedIndexChanged += new System.EventHandler(this.listBoxServiceList_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.serviceInfoPanelMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.propertyGridArguments);
            this.splitContainer1.Size = new System.Drawing.Size(563, 497);
            this.splitContainer1.SplitterDistance = 142;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // propertyGridArguments
            // 
            this.propertyGridArguments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridArguments.Location = new System.Drawing.Point(0, 0);
            this.propertyGridArguments.Name = "propertyGridArguments";
            this.propertyGridArguments.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGridArguments.Size = new System.Drawing.Size(563, 349);
            this.propertyGridArguments.TabIndex = 0;
            // 
            // toolStripButtonCleanOldLog
            // 
            this.toolStripButtonCleanOldLog.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCleanOldLog.Image")));
            this.toolStripButtonCleanOldLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCleanOldLog.Name = "toolStripButtonCleanOldLog";
            this.toolStripButtonCleanOldLog.Size = new System.Drawing.Size(87, 22);
            this.toolStripButtonCleanOldLog.Text = "清除旧日志";
            this.toolStripButtonCleanOldLog.Click += new System.EventHandler(this.toolStripButtonCleanOldLog_Click);
            // 
            // serviceInfoPanelMain
            // 
            this.serviceInfoPanelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serviceInfoPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serviceInfoPanelMain.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serviceInfoPanelMain.Location = new System.Drawing.Point(0, 0);
            this.serviceInfoPanelMain.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.serviceInfoPanelMain.Name = "serviceInfoPanelMain";
            this.serviceInfoPanelMain.Size = new System.Drawing.Size(563, 142);
            this.serviceInfoPanelMain.TabIndex = 0;
            // 
            // FMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(797, 522);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.toolStripMain);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BenQGuru eMES Service Console";
            this.Load += new System.EventHandler(this.FMain_Load);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ListBox listBoxServiceList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ServiceInfoPanel serviceInfoPanelMain;
        private System.Windows.Forms.PropertyGrid propertyGridArguments;
        private System.Windows.Forms.ToolStripButton toolStripButtonFind;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonExit;
        private System.Windows.Forms.ToolStripButton toolStripButtonRun;
        private System.Windows.Forms.ToolStripButton toolStripButtonCleanOldLog;


    }
}

