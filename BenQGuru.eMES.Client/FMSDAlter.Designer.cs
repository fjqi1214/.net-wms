namespace BenQGuru.eMES.Client
{
    partial class FMSDAlter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMSDAlter));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ultraGridMSDAlert = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkAutoRefresh = new System.Windows.Forms.CheckBox();
            this.labelHours1 = new System.Windows.Forms.Label();
            this.txtAlterTime = new UserControl.UCLabelEdit();
            this.labelOvertimeMore = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.red = new System.Windows.Forms.Label();
            this.lblColorDesc = new System.Windows.Forms.Label();
            this.btnSearch = new UserControl.UCButton();
            this.labelHours = new System.Windows.Forms.Label();
            this.txtAlertFloorLife = new UserControl.UCLabelEdit();
            this.labelMinute = new System.Windows.Forms.Label();
            this.txtRefreshRateV = new UserControl.UCLabelEdit();
            this.labelLeftDryingTime = new System.Windows.Forms.Label();
            this.labelOverFloorLife = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMSDAlert)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(925, 560);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ultraGridMSDAlert);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 133);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(919, 424);
            this.panel3.TabIndex = 31;
            // 
            // ultraGridMSDAlert
            // 
            this.ultraGridMSDAlert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ultraGridMSDAlert.DisplayLayout.RowConnectorColor = System.Drawing.Color.Gainsboro;
            this.ultraGridMSDAlert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMSDAlert.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridMSDAlert.Location = new System.Drawing.Point(0, 0);
            this.ultraGridMSDAlert.Name = "ultraGridMSDAlert";
            this.ultraGridMSDAlert.Size = new System.Drawing.Size(919, 424);
            this.ultraGridMSDAlert.TabIndex = 30;
            this.ultraGridMSDAlert.TabStop = false;
            this.ultraGridMSDAlert.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMSDAlert_InitializeLayout);
            this.ultraGridMSDAlert.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ultraGridMSDAlert_InitializeRow);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkAutoRefresh);
            this.panel2.Controls.Add(this.labelHours1);
            this.panel2.Controls.Add(this.txtAlterTime);
            this.panel2.Controls.Add(this.labelOvertimeMore);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.red);
            this.panel2.Controls.Add(this.lblColorDesc);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.labelHours);
            this.panel2.Controls.Add(this.txtAlertFloorLife);
            this.panel2.Controls.Add(this.labelMinute);
            this.panel2.Controls.Add(this.txtRefreshRateV);
            this.panel2.Controls.Add(this.labelLeftDryingTime);
            this.panel2.Controls.Add(this.labelOverFloorLife);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(919, 116);
            this.panel2.TabIndex = 30;
            // 
            // chkAutoRefresh
            // 
            this.chkAutoRefresh.AutoSize = true;
            this.chkAutoRefresh.Location = new System.Drawing.Point(650, 9);
            this.chkAutoRefresh.Name = "chkAutoRefresh";
            this.chkAutoRefresh.Size = new System.Drawing.Size(126, 16);
            this.chkAutoRefresh.TabIndex = 56;
            this.chkAutoRefresh.Text = "自动刷新 刷新频率";
            this.chkAutoRefresh.UseVisualStyleBackColor = true;
            this.chkAutoRefresh.CheckedChanged += new System.EventHandler(this.chkAutoRefresh_CheckedChanged);
            // 
            // labelHours1
            // 
            this.labelHours1.Location = new System.Drawing.Point(316, 77);
            this.labelHours1.Name = "labelHours1";
            this.labelHours1.Size = new System.Drawing.Size(40, 22);
            this.labelHours1.TabIndex = 55;
            this.labelHours1.Text = "小时";
            this.labelHours1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAlterTime
            // 
            this.txtAlterTime.AllowEditOnlyChecked = true;
            this.txtAlterTime.AutoSelectAll = false;
            this.txtAlterTime.AutoUpper = true;
            this.txtAlterTime.Caption = "";
            this.txtAlterTime.Checked = false;
            this.txtAlterTime.EditType = UserControl.EditTypes.Number;
            this.txtAlterTime.Location = new System.Drawing.Point(255, 77);
            this.txtAlterTime.MaxLength = 10;
            this.txtAlterTime.Multiline = false;
            this.txtAlterTime.Name = "txtAlterTime";
            this.txtAlterTime.PasswordChar = '\0';
            this.txtAlterTime.ReadOnly = false;
            this.txtAlterTime.ShowCheckBox = false;
            this.txtAlterTime.Size = new System.Drawing.Size(58, 24);
            this.txtAlterTime.TabIndex = 54;
            this.txtAlterTime.TabNext = true;
            this.txtAlterTime.TabStop = false;
            this.txtAlterTime.Value = "2";
            this.txtAlterTime.WidthType = UserControl.WidthTypes.Tiny;
            this.txtAlterTime.XAlign = 263;
            // 
            // labelOvertimeMore
            // 
            this.labelOvertimeMore.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOvertimeMore.ForeColor = System.Drawing.Color.Black;
            this.labelOvertimeMore.Location = new System.Drawing.Point(145, 8);
            this.labelOvertimeMore.Name = "labelOvertimeMore";
            this.labelOvertimeMore.Size = new System.Drawing.Size(343, 22);
            this.labelOvertimeMore.TabIndex = 46;
            this.labelOvertimeMore.Text = "超时（包括剩余车间寿命与暴露时间）";
            this.labelOvertimeMore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Orange;
            this.label2.Location = new System.Drawing.Point(86, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 22);
            this.label2.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(85, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 22);
            this.label1.TabIndex = 44;
            // 
            // red
            // 
            this.red.BackColor = System.Drawing.Color.Red;
            this.red.Location = new System.Drawing.Point(85, 9);
            this.red.Name = "red";
            this.red.Size = new System.Drawing.Size(54, 22);
            this.red.TabIndex = 43;
            // 
            // lblColorDesc
            // 
            this.lblColorDesc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblColorDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblColorDesc.Location = new System.Drawing.Point(9, 13);
            this.lblColorDesc.Name = "lblColorDesc";
            this.lblColorDesc.Size = new System.Drawing.Size(72, 22);
            this.lblColorDesc.TabIndex = 42;
            this.lblColorDesc.Text = "颜色说明";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.ButtonType = UserControl.ButtonTypes.Query;
            this.btnSearch.Caption = "查询";
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Location = new System.Drawing.Point(723, 79);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(88, 22);
            this.btnSearch.TabIndex = 41;
            this.btnSearch.TabStop = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labelHours
            // 
            this.labelHours.Location = new System.Drawing.Point(319, 41);
            this.labelHours.Name = "labelHours";
            this.labelHours.Size = new System.Drawing.Size(34, 22);
            this.labelHours.TabIndex = 52;
            this.labelHours.Text = "小时";
            this.labelHours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAlertFloorLife
            // 
            this.txtAlertFloorLife.AllowEditOnlyChecked = true;
            this.txtAlertFloorLife.AutoSelectAll = false;
            this.txtAlertFloorLife.AutoUpper = true;
            this.txtAlertFloorLife.Caption = "";
            this.txtAlertFloorLife.Checked = false;
            this.txtAlertFloorLife.EditType = UserControl.EditTypes.Number;
            this.txtAlertFloorLife.Location = new System.Drawing.Point(255, 41);
            this.txtAlertFloorLife.MaxLength = 10;
            this.txtAlertFloorLife.Multiline = false;
            this.txtAlertFloorLife.Name = "txtAlertFloorLife";
            this.txtAlertFloorLife.PasswordChar = '\0';
            this.txtAlertFloorLife.ReadOnly = false;
            this.txtAlertFloorLife.ShowCheckBox = false;
            this.txtAlertFloorLife.Size = new System.Drawing.Size(58, 24);
            this.txtAlertFloorLife.TabIndex = 51;
            this.txtAlertFloorLife.TabNext = true;
            this.txtAlertFloorLife.TabStop = false;
            this.txtAlertFloorLife.Value = "1";
            this.txtAlertFloorLife.WidthType = UserControl.WidthTypes.Tiny;
            this.txtAlertFloorLife.XAlign = 263;
            // 
            // labelMinute
            // 
            this.labelMinute.Location = new System.Drawing.Point(840, 8);
            this.labelMinute.Name = "labelMinute";
            this.labelMinute.Size = new System.Drawing.Size(40, 22);
            this.labelMinute.TabIndex = 50;
            this.labelMinute.Text = "分钟";
            this.labelMinute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRefreshRateV
            // 
            this.txtRefreshRateV.AllowEditOnlyChecked = true;
            this.txtRefreshRateV.AutoSelectAll = false;
            this.txtRefreshRateV.AutoUpper = true;
            this.txtRefreshRateV.Caption = "";
            this.txtRefreshRateV.Checked = false;
            this.txtRefreshRateV.EditType = UserControl.EditTypes.Integer;
            this.txtRefreshRateV.Location = new System.Drawing.Point(776, 7);
            this.txtRefreshRateV.MaxLength = 22;
            this.txtRefreshRateV.Multiline = false;
            this.txtRefreshRateV.Name = "txtRefreshRateV";
            this.txtRefreshRateV.PasswordChar = '\0';
            this.txtRefreshRateV.ReadOnly = false;
            this.txtRefreshRateV.ShowCheckBox = false;
            this.txtRefreshRateV.Size = new System.Drawing.Size(58, 24);
            this.txtRefreshRateV.TabIndex = 37;
            this.txtRefreshRateV.TabNext = true;
            this.txtRefreshRateV.TabStop = false;
            this.txtRefreshRateV.Value = "5";
            this.txtRefreshRateV.WidthType = UserControl.WidthTypes.Tiny;
            this.txtRefreshRateV.XAlign = 784;
            this.txtRefreshRateV.InnerTextChanged += new System.EventHandler(this.txtRefreshRate_InnerTextChanged);
            // 
            // labelLeftDryingTime
            // 
            this.labelLeftDryingTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelLeftDryingTime.ForeColor = System.Drawing.Color.Black;
            this.labelLeftDryingTime.Location = new System.Drawing.Point(144, 79);
            this.labelLeftDryingTime.Name = "labelLeftDryingTime";
            this.labelLeftDryingTime.Size = new System.Drawing.Size(204, 22);
            this.labelLeftDryingTime.TabIndex = 49;
            this.labelLeftDryingTime.Text = "剩余暴露时间";
            this.labelLeftDryingTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelOverFloorLife
            // 
            this.labelOverFloorLife.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOverFloorLife.ForeColor = System.Drawing.Color.Black;
            this.labelOverFloorLife.Location = new System.Drawing.Point(144, 43);
            this.labelOverFloorLife.Name = "labelOverFloorLife";
            this.labelOverFloorLife.Size = new System.Drawing.Size(125, 22);
            this.labelOverFloorLife.TabIndex = 47;
            this.labelOverFloorLife.Text = "剩余车间寿命小于";
            this.labelOverFloorLife.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FMSDAlter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 560);
            this.Controls.Add(this.groupBox2);
            this.Name = "FMSDAlter";
            this.Text = "湿敏元件预警";
            this.Load += new System.EventHandler(this.FMSDALERT_Load);
            this.groupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMSDAlert)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMSDAlert;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelOvertimeMore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label red;
        private System.Windows.Forms.Label lblColorDesc;
        private UserControl.UCButton btnSearch;
        private System.Windows.Forms.Label labelHours;
        private UserControl.UCLabelEdit txtAlertFloorLife;
        private System.Windows.Forms.Label labelMinute;
        private UserControl.UCLabelEdit txtRefreshRateV;
        private System.Windows.Forms.Label labelLeftDryingTime;
        private System.Windows.Forms.Label labelOverFloorLife;
        private System.Windows.Forms.Label labelHours1;
        private UserControl.UCLabelEdit txtAlterTime;
        private System.Windows.Forms.CheckBox chkAutoRefresh;
    }
}