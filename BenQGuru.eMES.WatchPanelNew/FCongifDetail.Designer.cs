namespace BenQGuru.eMES.WatchPanelNew
{
    partial class FCongifDetail
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtConfigName = new System.Windows.Forms.TextBox();
            this.lblConfigName = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkFacWatchPanel = new System.Windows.Forms.CheckBox();
            this.btInit = new System.Windows.Forms.Button();
            this.chkBigLineMessage = new System.Windows.Forms.CheckBox();
            this.chkFacOutPutAndRate = new System.Windows.Forms.CheckBox();
            this.chkFacMessage = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtWatchrRefrsh = new UserControl.UCLabelEdit();
            this.txtAutoRefrsh = new UserControl.UCLabelEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkShowRightArea = new System.Windows.Forms.CheckBox();
            this.btSelectEQPID = new System.Windows.Forms.Button();
            this.txtEQPID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtOneLine = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.btSelectSSCode = new System.Windows.Forms.Button();
            this.txtSSCodeList = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ucLabelEditScreens = new UserControl.UCLabelEdit();
            this.btSelectSSCodeForFactory = new System.Windows.Forms.Button();
            this.txtSSCodeListForFactory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtConfigName);
            this.groupBox1.Controls.Add(this.lblConfigName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(555, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtConfigName
            // 
            this.txtConfigName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfigName.Location = new System.Drawing.Point(116, 20);
            this.txtConfigName.Name = "txtConfigName";
            this.txtConfigName.Size = new System.Drawing.Size(416, 20);
            this.txtConfigName.TabIndex = 1;
            // 
            // lblConfigName
            // 
            this.lblConfigName.AutoSize = true;
            this.lblConfigName.Location = new System.Drawing.Point(21, 25);
            this.lblConfigName.Name = "lblConfigName";
            this.lblConfigName.Size = new System.Drawing.Size(91, 13);
            this.lblConfigName.TabIndex = 0;
            this.lblConfigName.Text = "当前配置项名称";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkFacWatchPanel);
            this.groupBox2.Controls.Add(this.btInit);
            this.groupBox2.Controls.Add(this.chkBigLineMessage);
            this.groupBox2.Controls.Add(this.chkFacOutPutAndRate);
            this.groupBox2.Controls.Add(this.chkFacMessage);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(555, 50);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "看板内容";
            // 
            // chkFacWatchPanel
            // 
            this.chkFacWatchPanel.AutoSize = true;
            this.chkFacWatchPanel.Checked = true;
            this.chkFacWatchPanel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFacWatchPanel.Location = new System.Drawing.Point(184, 21);
            this.chkFacWatchPanel.Name = "chkFacWatchPanel";
            this.chkFacWatchPanel.Size = new System.Drawing.Size(122, 17);
            this.chkFacWatchPanel.TabIndex = 4;
            this.chkFacWatchPanel.Text = "车间概况电子看板";
            this.chkFacWatchPanel.UseVisualStyleBackColor = true;
            // 
            // btInit
            // 
            this.btInit.Location = new System.Drawing.Point(103, 17);
            this.btInit.Name = "btInit";
            this.btInit.Size = new System.Drawing.Size(64, 23);
            this.btInit.TabIndex = 3;
            this.btInit.Text = "设定内容";
            this.btInit.UseVisualStyleBackColor = true;
            this.btInit.Click += new System.EventHandler(this.btInit_Click);
            // 
            // chkBigLineMessage
            // 
            this.chkBigLineMessage.AutoSize = true;
            this.chkBigLineMessage.Checked = true;
            this.chkBigLineMessage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBigLineMessage.Location = new System.Drawing.Point(458, 23);
            this.chkBigLineMessage.Name = "chkBigLineMessage";
            this.chkBigLineMessage.Size = new System.Drawing.Size(74, 17);
            this.chkBigLineMessage.TabIndex = 2;
            this.chkBigLineMessage.Text = "产线情况";
            this.chkBigLineMessage.UseVisualStyleBackColor = true;
            // 
            // chkFacOutPutAndRate
            // 
            this.chkFacOutPutAndRate.AutoSize = true;
            this.chkFacOutPutAndRate.Checked = true;
            this.chkFacOutPutAndRate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFacOutPutAndRate.Location = new System.Drawing.Point(318, 22);
            this.chkFacOutPutAndRate.Name = "chkFacOutPutAndRate";
            this.chkFacOutPutAndRate.Size = new System.Drawing.Size(122, 17);
            this.chkFacOutPutAndRate.TabIndex = 1;
            this.chkFacOutPutAndRate.Text = "车间产量及直通率";
            this.chkFacOutPutAndRate.UseVisualStyleBackColor = true;
            // 
            // chkFacMessage
            // 
            this.chkFacMessage.AutoSize = true;
            this.chkFacMessage.Checked = true;
            this.chkFacMessage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFacMessage.Location = new System.Drawing.Point(23, 23);
            this.chkFacMessage.Name = "chkFacMessage";
            this.chkFacMessage.Size = new System.Drawing.Size(74, 17);
            this.chkFacMessage.TabIndex = 0;
            this.chkFacMessage.Text = "车间概况";
            this.chkFacMessage.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtWatchrRefrsh);
            this.groupBox3.Controls.Add(this.txtAutoRefrsh);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 105);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(555, 54);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "刷新频率";
            // 
            // txtWatchrRefrsh
            // 
            this.txtWatchrRefrsh.AllowEditOnlyChecked = true;
            this.txtWatchrRefrsh.AutoUpper = true;
            this.txtWatchrRefrsh.Caption = "屏幕切换频率";
            this.txtWatchrRefrsh.Checked = false;
            this.txtWatchrRefrsh.EditType = UserControl.EditTypes.Number;
            this.txtWatchrRefrsh.Location = new System.Drawing.Point(240, 19);
            this.txtWatchrRefrsh.MaxLength = 40;
            this.txtWatchrRefrsh.Multiline = false;
            this.txtWatchrRefrsh.Name = "txtWatchrRefrsh";
            this.txtWatchrRefrsh.PasswordChar = '\0';
            this.txtWatchrRefrsh.ReadOnly = false;
            this.txtWatchrRefrsh.ShowCheckBox = false;
            this.txtWatchrRefrsh.Size = new System.Drawing.Size(135, 24);
            this.txtWatchrRefrsh.TabIndex = 6;
            this.txtWatchrRefrsh.TabNext = true;
            this.txtWatchrRefrsh.Value = "";
            this.txtWatchrRefrsh.WidthType = UserControl.WidthTypes.Tiny;
            this.txtWatchrRefrsh.XAlign = 325;
            // 
            // txtAutoRefrsh
            // 
            this.txtAutoRefrsh.AllowEditOnlyChecked = true;
            this.txtAutoRefrsh.AutoUpper = true;
            this.txtAutoRefrsh.Caption = "自动刷新频率";
            this.txtAutoRefrsh.Checked = false;
            this.txtAutoRefrsh.EditType = UserControl.EditTypes.Number;
            this.txtAutoRefrsh.Location = new System.Drawing.Point(24, 19);
            this.txtAutoRefrsh.MaxLength = 40;
            this.txtAutoRefrsh.Multiline = false;
            this.txtAutoRefrsh.Name = "txtAutoRefrsh";
            this.txtAutoRefrsh.PasswordChar = '\0';
            this.txtAutoRefrsh.ReadOnly = false;
            this.txtAutoRefrsh.ShowCheckBox = false;
            this.txtAutoRefrsh.Size = new System.Drawing.Size(135, 24);
            this.txtAutoRefrsh.TabIndex = 4;
            this.txtAutoRefrsh.TabNext = true;
            this.txtAutoRefrsh.Value = "";
            this.txtAutoRefrsh.WidthType = UserControl.WidthTypes.Tiny;
            this.txtAutoRefrsh.XAlign = 109;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(381, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "分钟";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(164, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "分钟";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btSelectSSCodeForFactory);
            this.groupBox4.Controls.Add(this.txtSSCodeListForFactory);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.chkShowRightArea);
            this.groupBox4.Controls.Add(this.btSelectEQPID);
            this.groupBox4.Controls.Add(this.txtEQPID);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.rbtOneLine);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.btSelectSSCode);
            this.groupBox4.Controls.Add(this.txtSSCodeList);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 159);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(555, 166);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "条件设定";
            // 
            // chkShowRightArea
            // 
            this.chkShowRightArea.AutoSize = true;
            this.chkShowRightArea.Checked = true;
            this.chkShowRightArea.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowRightArea.Location = new System.Drawing.Point(24, 139);
            this.chkShowRightArea.Name = "chkShowRightArea";
            this.chkShowRightArea.Size = new System.Drawing.Size(98, 17);
            this.chkShowRightArea.TabIndex = 14;
            this.chkShowRightArea.Text = "显示右边区域";
            this.chkShowRightArea.UseVisualStyleBackColor = true;
            // 
            // btSelectEQPID
            // 
            this.btSelectEQPID.Location = new System.Drawing.Point(368, 77);
            this.btSelectEQPID.Name = "btSelectEQPID";
            this.btSelectEQPID.Size = new System.Drawing.Size(65, 23);
            this.btSelectEQPID.TabIndex = 13;
            this.btSelectEQPID.Text = "选择";
            this.btSelectEQPID.UseVisualStyleBackColor = true;
            this.btSelectEQPID.Click += new System.EventHandler(this.btSelectEQPID_Click);
            // 
            // txtEQPID
            // 
            this.txtEQPID.Location = new System.Drawing.Point(115, 79);
            this.txtEQPID.Name = "txtEQPID";
            this.txtEQPID.Size = new System.Drawing.Size(247, 20);
            this.txtEQPID.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "设备ID";
            // 
            // rbtOneLine
            // 
            this.rbtOneLine.AutoSize = true;
            this.rbtOneLine.Checked = true;
            this.rbtOneLine.Location = new System.Drawing.Point(128, 113);
            this.rbtOneLine.Name = "rbtOneLine";
            this.rbtOneLine.Size = new System.Drawing.Size(31, 17);
            this.rbtOneLine.TabIndex = 4;
            this.rbtOneLine.TabStop = true;
            this.rbtOneLine.Text = "1";
            this.rbtOneLine.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "每屏展现产线数";
            // 
            // btSelectSSCode
            // 
            this.btSelectSSCode.Location = new System.Drawing.Point(368, 18);
            this.btSelectSSCode.Name = "btSelectSSCode";
            this.btSelectSSCode.Size = new System.Drawing.Size(65, 23);
            this.btSelectSSCode.TabIndex = 2;
            this.btSelectSSCode.Text = "选择";
            this.btSelectSSCode.UseVisualStyleBackColor = true;
            this.btSelectSSCode.Click += new System.EventHandler(this.btSelectSSCode_Click);
            // 
            // txtSSCodeList
            // 
            this.txtSSCodeList.Location = new System.Drawing.Point(115, 20);
            this.txtSSCodeList.Name = "txtSSCodeList";
            this.txtSSCodeList.Size = new System.Drawing.Size(247, 20);
            this.txtSSCodeList.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "包含的产线";
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(154, 377);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 4;
            this.btSave.Text = "保存";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(326, 377);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 5;
            this.btClose.Text = "关闭";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ucLabelEditScreens);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 325);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(555, 49);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            // 
            // ucLabelEditScreens
            // 
            this.ucLabelEditScreens.AllowEditOnlyChecked = true;
            this.ucLabelEditScreens.AutoUpper = true;
            this.ucLabelEditScreens.Caption = "选择显示屏幕";
            this.ucLabelEditScreens.Checked = false;
            this.ucLabelEditScreens.EditType = UserControl.EditTypes.Number;
            this.ucLabelEditScreens.Location = new System.Drawing.Point(22, 20);
            this.ucLabelEditScreens.MaxLength = 40;
            this.ucLabelEditScreens.Multiline = false;
            this.ucLabelEditScreens.Name = "ucLabelEditScreens";
            this.ucLabelEditScreens.PasswordChar = '\0';
            this.ucLabelEditScreens.ReadOnly = false;
            this.ucLabelEditScreens.ShowCheckBox = false;
            this.ucLabelEditScreens.Size = new System.Drawing.Size(135, 24);
            this.ucLabelEditScreens.TabIndex = 5;
            this.ucLabelEditScreens.TabNext = true;
            this.ucLabelEditScreens.Value = "";
            this.ucLabelEditScreens.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditScreens.XAlign = 107;
            // 
            // btSelectSSCodeForFactory
            // 
            this.btSelectSSCodeForFactory.Location = new System.Drawing.Point(368, 47);
            this.btSelectSSCodeForFactory.Name = "btSelectSSCodeForFactory";
            this.btSelectSSCodeForFactory.Size = new System.Drawing.Size(65, 23);
            this.btSelectSSCodeForFactory.TabIndex = 17;
            this.btSelectSSCodeForFactory.Text = "选择";
            this.btSelectSSCodeForFactory.UseVisualStyleBackColor = true;
            this.btSelectSSCodeForFactory.Click += new System.EventHandler(this.btSelectSSCodeForFactory_Click);
            // 
            // txtSSCodeListForFactory
            // 
            this.txtSSCodeListForFactory.Location = new System.Drawing.Point(115, 49);
            this.txtSSCodeListForFactory.Name = "txtSSCodeListForFactory";
            this.txtSSCodeListForFactory.Size = new System.Drawing.Size(247, 20);
            this.txtSSCodeListForFactory.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "车间包含的产线";
            // 
            // FCongifDetail
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(555, 415);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FCongifDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "电子看板配置项维护";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtConfigName;
        private System.Windows.Forms.Label lblConfigName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkBigLineMessage;
        private System.Windows.Forms.CheckBox chkFacOutPutAndRate;
        private System.Windows.Forms.CheckBox chkFacMessage;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btInit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btSelectSSCode;
        private System.Windows.Forms.TextBox txtSSCodeList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbtOneLine;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btClose;
        private UserControl.UCLabelEdit txtAutoRefrsh;
        private UserControl.UCLabelEdit txtWatchrRefrsh;
        private System.Windows.Forms.CheckBox chkFacWatchPanel;
        private System.Windows.Forms.GroupBox groupBox5;
        private UserControl.UCLabelEdit ucLabelEditScreens;
        private System.Windows.Forms.Button btSelectEQPID;
        private System.Windows.Forms.TextBox txtEQPID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkShowRightArea;
        private System.Windows.Forms.Button btSelectSSCodeForFactory;
        private System.Windows.Forms.TextBox txtSSCodeListForFactory;
        private System.Windows.Forms.Label label2;

    }
}