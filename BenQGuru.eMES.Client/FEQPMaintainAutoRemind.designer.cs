namespace BenQGuru.eMES.Client
{
    partial class FEQPMaintainAutoRemind
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FEQPMaintainAutoRemind));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnQuery = new UserControl.UCButton();
            this.label4 = new System.Windows.Forms.Label();
            this.labelDay = new System.Windows.Forms.Label();
            this.ucLToMaintainDate = new UserControl.UCLabelEdit();
            this.drpEQPID = new UserControl.UCLabelCombox();
            this.ucLabelEditMaintainITEM = new UserControl.UCLabelEdit();
            this.labelOvertime = new System.Windows.Forms.Label();
            this.edtTimer = new UserControl.UCLabelEdit();
            this.labelMinute = new System.Windows.Forms.Label();
            this.chkIsvalid = new System.Windows.Forms.CheckBox();
            this.drpMaintainTYPE = new UserControl.UCLabelCombox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ultraGridMaintenance = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMaintenance)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.labelDay);
            this.groupBox1.Controls.Add(this.ucLToMaintainDate);
            this.groupBox1.Controls.Add(this.drpEQPID);
            this.groupBox1.Controls.Add(this.ucLabelEditMaintainITEM);
            this.groupBox1.Controls.Add(this.labelOvertime);
            this.groupBox1.Controls.Add(this.edtTimer);
            this.groupBox1.Controls.Add(this.labelMinute);
            this.groupBox1.Controls.Add(this.chkIsvalid);
            this.groupBox1.Controls.Add(this.drpMaintainTYPE);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(838, 76);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(7, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 18);
            this.label2.TabIndex = 219;
            this.label2.Text = "                      ";
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.SystemColors.Control;
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.ButtonType = UserControl.ButtonTypes.None;
            this.btnQuery.Caption = "查询";
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.Location = new System.Drawing.Point(725, 43);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(88, 22);
            this.btnQuery.TabIndex = 218;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(7, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 18);
            this.label4.TabIndex = 217;
            this.label4.Text = "                      ";
            // 
            // labelDay
            // 
            this.labelDay.AutoSize = true;
            this.labelDay.Location = new System.Drawing.Point(219, 49);
            this.labelDay.Name = "labelDay";
            this.labelDay.Size = new System.Drawing.Size(17, 12);
            this.labelDay.TabIndex = 215;
            this.labelDay.Text = "天";
            // 
            // ucLToMaintainDate
            // 
            this.ucLToMaintainDate.AllowEditOnlyChecked = true;
            this.ucLToMaintainDate.AutoSelectAll = false;
            this.ucLToMaintainDate.AutoUpper = true;
            this.ucLToMaintainDate.Caption = "离保养日期";
            this.ucLToMaintainDate.Checked = false;
            this.ucLToMaintainDate.EditType = UserControl.EditTypes.Integer;
            this.ucLToMaintainDate.Location = new System.Drawing.Point(87, 43);
            this.ucLToMaintainDate.MaxLength = 8;
            this.ucLToMaintainDate.Multiline = false;
            this.ucLToMaintainDate.Name = "ucLToMaintainDate";
            this.ucLToMaintainDate.PasswordChar = '\0';
            this.ucLToMaintainDate.ReadOnly = false;
            this.ucLToMaintainDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ucLToMaintainDate.ShowCheckBox = false;
            this.ucLToMaintainDate.Size = new System.Drawing.Size(123, 24);
            this.ucLToMaintainDate.TabIndex = 214;
            this.ucLToMaintainDate.TabNext = false;
            this.ucLToMaintainDate.Value = "5";
            this.ucLToMaintainDate.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLToMaintainDate.XAlign = 160;
            // 
            // drpEQPID
            // 
            this.drpEQPID.AllowEditOnlyChecked = true;
            this.drpEQPID.Caption = "设备编码";
            this.drpEQPID.Checked = false;
            this.drpEQPID.Location = new System.Drawing.Point(266, 13);
            this.drpEQPID.Name = "drpEQPID";
            this.drpEQPID.SelectedIndex = -1;
            this.drpEQPID.ShowCheckBox = false;
            this.drpEQPID.Size = new System.Drawing.Size(194, 20);
            this.drpEQPID.TabIndex = 213;
            this.drpEQPID.WidthType = UserControl.WidthTypes.Normal;
            this.drpEQPID.XAlign = 327;
            // 
            // ucLabelEditMaintainITEM
            // 
            this.ucLabelEditMaintainITEM.AllowEditOnlyChecked = true;
            this.ucLabelEditMaintainITEM.AutoSelectAll = false;
            this.ucLabelEditMaintainITEM.AutoUpper = true;
            this.ucLabelEditMaintainITEM.Caption = "保养内容";
            this.ucLabelEditMaintainITEM.Checked = false;
            this.ucLabelEditMaintainITEM.EditType = UserControl.EditTypes.String;
            this.ucLabelEditMaintainITEM.Location = new System.Drawing.Point(265, 43);
            this.ucLabelEditMaintainITEM.MaxLength = 8;
            this.ucLabelEditMaintainITEM.Multiline = false;
            this.ucLabelEditMaintainITEM.Name = "ucLabelEditMaintainITEM";
            this.ucLabelEditMaintainITEM.PasswordChar = '\0';
            this.ucLabelEditMaintainITEM.ReadOnly = false;
            this.ucLabelEditMaintainITEM.ShowCheckBox = false;
            this.ucLabelEditMaintainITEM.Size = new System.Drawing.Size(194, 24);
            this.ucLabelEditMaintainITEM.TabIndex = 212;
            this.ucLabelEditMaintainITEM.TabNext = false;
            this.ucLabelEditMaintainITEM.Value = "";
            this.ucLabelEditMaintainITEM.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditMaintainITEM.XAlign = 326;
            // 
            // labelOvertime
            // 
            this.labelOvertime.AutoSize = true;
            this.labelOvertime.Location = new System.Drawing.Point(88, 17);
            this.labelOvertime.Name = "labelOvertime";
            this.labelOvertime.Size = new System.Drawing.Size(29, 12);
            this.labelOvertime.TabIndex = 211;
            this.labelOvertime.Text = "超时";
            // 
            // edtTimer
            // 
            this.edtTimer.AllowEditOnlyChecked = true;
            this.edtTimer.AutoSelectAll = false;
            this.edtTimer.AutoUpper = true;
            this.edtTimer.Caption = "频率";
            this.edtTimer.Checked = false;
            this.edtTimer.EditType = UserControl.EditTypes.Integer;
            this.edtTimer.Location = new System.Drawing.Point(587, 43);
            this.edtTimer.MaxLength = 4;
            this.edtTimer.Multiline = false;
            this.edtTimer.Name = "edtTimer";
            this.edtTimer.PasswordChar = '\0';
            this.edtTimer.ReadOnly = false;
            this.edtTimer.ShowCheckBox = false;
            this.edtTimer.Size = new System.Drawing.Size(87, 24);
            this.edtTimer.TabIndex = 212;
            this.edtTimer.TabNext = false;
            this.edtTimer.Value = "";
            this.edtTimer.WidthType = UserControl.WidthTypes.Tiny;
            this.edtTimer.XAlign = 624;
            this.edtTimer.InnerTextChanged += new System.EventHandler(this.edtTimer_InnerTextChanged);
            // 
            // labelMinute
            // 
            this.labelMinute.AutoSize = true;
            this.labelMinute.Location = new System.Drawing.Point(680, 49);
            this.labelMinute.Name = "labelMinute";
            this.labelMinute.Size = new System.Drawing.Size(17, 12);
            this.labelMinute.TabIndex = 211;
            this.labelMinute.Text = "分";
            this.labelMinute.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkIsvalid
            // 
            this.chkIsvalid.Location = new System.Drawing.Point(504, 45);
            this.chkIsvalid.Name = "chkIsvalid";
            this.chkIsvalid.Size = new System.Drawing.Size(84, 20);
            this.chkIsvalid.TabIndex = 209;
            this.chkIsvalid.Text = "自动刷新";
            this.chkIsvalid.CheckedChanged += new System.EventHandler(this.chkIsvalid_CheckedChanged);
            // 
            // drpMaintainTYPE
            // 
            this.drpMaintainTYPE.AllowEditOnlyChecked = true;
            this.drpMaintainTYPE.Caption = "保养类型";
            this.drpMaintainTYPE.Checked = false;
            this.drpMaintainTYPE.Location = new System.Drawing.Point(506, 13);
            this.drpMaintainTYPE.Name = "drpMaintainTYPE";
            this.drpMaintainTYPE.SelectedIndex = -1;
            this.drpMaintainTYPE.ShowCheckBox = false;
            this.drpMaintainTYPE.Size = new System.Drawing.Size(194, 20);
            this.drpMaintainTYPE.TabIndex = 25;
            this.drpMaintainTYPE.WidthType = UserControl.WidthTypes.Normal;
            this.drpMaintainTYPE.XAlign = 567;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ultraGridMaintenance);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(838, 392);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // ultraGridMaintenance
            // 
            this.ultraGridMaintenance.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridMaintenance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMaintenance.Location = new System.Drawing.Point(3, 17);
            this.ultraGridMaintenance.Name = "ultraGridMaintenance";
            this.ultraGridMaintenance.Size = new System.Drawing.Size(832, 372);
            this.ultraGridMaintenance.TabIndex = 1;
            this.ultraGridMaintenance.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridScrutiny_InitializeLayout);
            this.ultraGridMaintenance.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ultraGridScrutiny_InitializeRow);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FEQPMaintainAutoRemind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(838, 468);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FEQPMaintainAutoRemind";
            this.Text = "设备保养自动提醒";
            this.Load += new System.EventHandler(this.FOldScrutiny_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMaintenance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMaintenance;
        private UserControl.UCLabelCombox drpMaintainTYPE;
        private System.Windows.Forms.CheckBox chkIsvalid;
        private System.Windows.Forms.Label labelMinute;
        private UserControl.UCLabelEdit edtTimer;
        private System.Windows.Forms.Timer timer1;
        private UserControl.UCLabelCombox drpEQPID;
        private System.Windows.Forms.Label labelOvertime;
        private System.Windows.Forms.Label labelDay;
        private UserControl.UCLabelEdit ucLToMaintainDate;
        private UserControl.UCLabelEdit ucLabelEditMaintainITEM;
        private System.Windows.Forms.Label label4;
        private UserControl.UCButton btnQuery;
        private System.Windows.Forms.Label label2;
    }
}