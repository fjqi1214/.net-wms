namespace BenQGuru.eMES.DBTransferTool
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.groupBoxMain = new System.Windows.Forms.GroupBox();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonQuery = new System.Windows.Forms.Button();
            this.comboBoxJobType = new System.Windows.Forms.ComboBox();
            this.textBoxDBLink = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.ultraGridJobList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBoxRefresh = new System.Windows.Forms.GroupBox();
            this.buttonManualRefresh = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.maskedTextBoxSecond = new System.Windows.Forms.MaskedTextBox();
            this.checkBoxAutoRefresh = new System.Windows.Forms.CheckBox();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.progressBarMain = new System.Windows.Forms.ProgressBar();
            this.groupBoxMain.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridJobList)).BeginInit();
            this.groupBoxRefresh.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMain
            // 
            this.groupBoxMain.Controls.Add(this.textBoxDate);
            this.groupBoxMain.Controls.Add(this.label3);
            this.groupBoxMain.Controls.Add(this.buttonQuery);
            this.groupBoxMain.Controls.Add(this.comboBoxJobType);
            this.groupBoxMain.Controls.Add(this.textBoxDBLink);
            this.groupBoxMain.Controls.Add(this.label2);
            this.groupBoxMain.Controls.Add(this.label1);
            this.groupBoxMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxMain.Location = new System.Drawing.Point(0, 0);
            this.groupBoxMain.Name = "groupBoxMain";
            this.groupBoxMain.Size = new System.Drawing.Size(792, 65);
            this.groupBoxMain.TabIndex = 0;
            this.groupBoxMain.TabStop = false;
            // 
            // textBoxDate
            // 
            this.textBoxDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDate.Location = new System.Drawing.Point(411, 24);
            this.textBoxDate.MaxLength = 8;
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.Size = new System.Drawing.Size(279, 21);
            this.textBoxDate.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(376, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "日期";
            // 
            // buttonQuery
            // 
            this.buttonQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonQuery.Location = new System.Drawing.Point(705, 24);
            this.buttonQuery.Name = "buttonQuery";
            this.buttonQuery.Size = new System.Drawing.Size(75, 23);
            this.buttonQuery.TabIndex = 2;
            this.buttonQuery.Text = "查询";
            this.buttonQuery.UseVisualStyleBackColor = true;
            this.buttonQuery.Click += new System.EventHandler(this.buttonQuery_Click);
            // 
            // comboBoxJobType
            // 
            this.comboBoxJobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxJobType.FormattingEnabled = true;
            this.comboBoxJobType.Location = new System.Drawing.Point(94, 24);
            this.comboBoxJobType.Name = "comboBoxJobType";
            this.comboBoxJobType.Size = new System.Drawing.Size(94, 20);
            this.comboBoxJobType.TabIndex = 1;
            this.comboBoxJobType.SelectedIndexChanged += new System.EventHandler(this.comboBoxJobType_SelectedIndexChanged);
            // 
            // textBoxDBLink
            // 
            this.textBoxDBLink.Location = new System.Drawing.Point(241, 24);
            this.textBoxDBLink.MaxLength = 20;
            this.textBoxDBLink.Name = "textBoxDBLink";
            this.textBoxDBLink.Size = new System.Drawing.Size(128, 21);
            this.textBoxDBLink.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(194, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "DBLink";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "任务类型";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.progressBarMain);
            this.groupBox2.Controls.Add(this.buttonClose);
            this.groupBox2.Controls.Add(this.buttonExecute);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 510);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(792, 60);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(705, 20);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "退出";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonExecute
            // 
            this.buttonExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExecute.Location = new System.Drawing.Point(615, 20);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(75, 23);
            this.buttonExecute.TabIndex = 0;
            this.buttonExecute.Text = "执行";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // ultraGridJobList
            // 
            this.ultraGridJobList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridJobList.Location = new System.Drawing.Point(0, 128);
            this.ultraGridJobList.Name = "ultraGridJobList";
            this.ultraGridJobList.Size = new System.Drawing.Size(792, 382);
            this.ultraGridJobList.TabIndex = 2;
            this.ultraGridJobList.Text = "选择一个或多个任务执行";
            this.ultraGridJobList.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridJobList_InitializeLayout);
            // 
            // groupBoxRefresh
            // 
            this.groupBoxRefresh.Controls.Add(this.buttonManualRefresh);
            this.groupBoxRefresh.Controls.Add(this.label5);
            this.groupBoxRefresh.Controls.Add(this.label4);
            this.groupBoxRefresh.Controls.Add(this.maskedTextBoxSecond);
            this.groupBoxRefresh.Controls.Add(this.checkBoxAutoRefresh);
            this.groupBoxRefresh.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxRefresh.Location = new System.Drawing.Point(0, 65);
            this.groupBoxRefresh.Name = "groupBoxRefresh";
            this.groupBoxRefresh.Size = new System.Drawing.Size(792, 63);
            this.groupBoxRefresh.TabIndex = 3;
            this.groupBoxRefresh.TabStop = false;
            this.groupBoxRefresh.Text = "结果刷新";
            // 
            // buttonManualRefresh
            // 
            this.buttonManualRefresh.Location = new System.Drawing.Point(294, 24);
            this.buttonManualRefresh.Name = "buttonManualRefresh";
            this.buttonManualRefresh.Size = new System.Drawing.Size(111, 23);
            this.buttonManualRefresh.TabIndex = 4;
            this.buttonManualRefresh.Text = "手动刷新结果";
            this.buttonManualRefresh.UseVisualStyleBackColor = true;
            this.buttonManualRefresh.Click += new System.EventHandler(this.buttonManualRefresh_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(216, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "秒刷新一次";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "每隔";
            // 
            // maskedTextBoxSecond
            // 
            this.maskedTextBoxSecond.HidePromptOnLeave = true;
            this.maskedTextBoxSecond.Location = new System.Drawing.Point(147, 25);
            this.maskedTextBoxSecond.Mask = "99";
            this.maskedTextBoxSecond.Name = "maskedTextBoxSecond";
            this.maskedTextBoxSecond.Size = new System.Drawing.Size(63, 21);
            this.maskedTextBoxSecond.SkipLiterals = false;
            this.maskedTextBoxSecond.TabIndex = 1;
            this.maskedTextBoxSecond.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // checkBoxAutoRefresh
            // 
            this.checkBoxAutoRefresh.AutoSize = true;
            this.checkBoxAutoRefresh.Location = new System.Drawing.Point(25, 28);
            this.checkBoxAutoRefresh.Name = "checkBoxAutoRefresh";
            this.checkBoxAutoRefresh.Size = new System.Drawing.Size(72, 16);
            this.checkBoxAutoRefresh.TabIndex = 0;
            this.checkBoxAutoRefresh.Text = "自动刷新";
            this.checkBoxAutoRefresh.UseVisualStyleBackColor = true;
            this.checkBoxAutoRefresh.CheckedChanged += new System.EventHandler(this.checkBoxAutoRefresh_CheckedChanged);
            // 
            // timerRefresh
            // 
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // progressBarMain
            // 
            this.progressBarMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarMain.Location = new System.Drawing.Point(12, 20);
            this.progressBarMain.Name = "progressBarMain";
            this.progressBarMain.Size = new System.Drawing.Size(597, 23);
            this.progressBarMain.TabIndex = 5;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 570);
            this.Controls.Add(this.ultraGridJobList);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxRefresh);
            this.Controls.Add(this.groupBoxMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormMain";
            this.Text = "数据库迁移工具";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.groupBoxMain.ResumeLayout(false);
            this.groupBoxMain.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridJobList)).EndInit();
            this.groupBoxRefresh.ResumeLayout(false);
            this.groupBoxRefresh.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonQuery;
        private System.Windows.Forms.ComboBox comboBoxJobType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDBLink;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.Label label3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridJobList;
        private System.Windows.Forms.GroupBox groupBoxRefresh;
        private System.Windows.Forms.CheckBox checkBoxAutoRefresh;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxSecond;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonManualRefresh;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.ProgressBar progressBarMain;

    }
}

