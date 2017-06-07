namespace BenQGuru.eMES.ClientWatchPanel
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtPerDay = new System.Windows.Forms.RadioButton();
            this.rbtPerTime = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.rbtOneLine = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.btSelected = new System.Windows.Forms.Button();
            this.txtBigLineList = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.txtWatchrRefrsh = new UserControl.UCLabelEdit();
            this.txtAutoRefrsh = new UserControl.UCLabelEdit();
            this.lblProductCon = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkFinishedProduct = new System.Windows.Forms.CheckBox();
            this.chkSemimanuFacture = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.groupBox4.Controls.Add(this.panel2);
            this.groupBox4.Controls.Add(this.lblProductCon);
            this.groupBox4.Controls.Add(this.panel1);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.rbtOneLine);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.btSelected);
            this.groupBox4.Controls.Add(this.txtBigLineList);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 159);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(555, 148);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "条件设定";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbtPerDay);
            this.panel1.Controls.Add(this.rbtPerTime);
            this.panel1.Location = new System.Drawing.Point(80, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 35);
            this.panel1.TabIndex = 8;
            // 
            // rbtPerDay
            // 
            this.rbtPerDay.AutoSize = true;
            this.rbtPerDay.Location = new System.Drawing.Point(150, 9);
            this.rbtPerDay.Name = "rbtPerDay";
            this.rbtPerDay.Size = new System.Drawing.Size(73, 17);
            this.rbtPerDay.TabIndex = 1;
            this.rbtPerDay.TabStop = true;
            this.rbtPerDay.Text = "按月分天";
            this.rbtPerDay.UseVisualStyleBackColor = true;
            // 
            // rbtPerTime
            // 
            this.rbtPerTime.AutoSize = true;
            this.rbtPerTime.Checked = true;
            this.rbtPerTime.Location = new System.Drawing.Point(48, 9);
            this.rbtPerTime.Name = "rbtPerTime";
            this.rbtPerTime.Size = new System.Drawing.Size(85, 17);
            this.rbtPerTime.TabIndex = 0;
            this.rbtPerTime.TabStop = true;
            this.rbtPerTime.Text = "按天分时段";
            this.rbtPerTime.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "时间维度";
            // 
            // rbtOneLine
            // 
            this.rbtOneLine.AutoSize = true;
            this.rbtOneLine.Checked = true;
            this.rbtOneLine.Location = new System.Drawing.Point(128, 48);
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
            this.label7.Location = new System.Drawing.Point(21, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "每屏展现产线数";
            // 
            // btSelected
            // 
            this.btSelected.Location = new System.Drawing.Point(345, 18);
            this.btSelected.Name = "btSelected";
            this.btSelected.Size = new System.Drawing.Size(65, 23);
            this.btSelected.TabIndex = 2;
            this.btSelected.Text = "选择";
            this.btSelected.UseVisualStyleBackColor = true;
            this.btSelected.Click += new System.EventHandler(this.btSelected_Click);
            // 
            // txtBigLineList
            // 
            this.txtBigLineList.Location = new System.Drawing.Point(92, 20);
            this.txtBigLineList.Name = "txtBigLineList";
            this.txtBigLineList.Size = new System.Drawing.Size(247, 20);
            this.txtBigLineList.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "包含的产线";
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(92, 313);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 4;
            this.btSave.Text = "保存";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(264, 313);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 5;
            this.btClose.Text = "关闭";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // txtWatchrRefrsh
            // 
            this.txtWatchrRefrsh.AllowEditOnlyChecked = true;
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
            // lblProductCon
            // 
            this.lblProductCon.AutoSize = true;
            this.lblProductCon.Location = new System.Drawing.Point(21, 119);
            this.lblProductCon.Name = "lblProductCon";
            this.lblProductCon.Size = new System.Drawing.Size(55, 13);
            this.lblProductCon.TabIndex = 9;
            this.lblProductCon.Text = "成品类别";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkSemimanuFacture);
            this.panel2.Controls.Add(this.chkFinishedProduct);
            this.panel2.Location = new System.Drawing.Point(80, 111);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(330, 31);
            this.panel2.TabIndex = 10;
            // 
            // chkFinishedProduct
            // 
            this.chkFinishedProduct.AutoSize = true;
            this.chkFinishedProduct.Checked = true;
            this.chkFinishedProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFinishedProduct.Location = new System.Drawing.Point(48, 9);
            this.chkFinishedProduct.Name = "chkFinishedProduct";
            this.chkFinishedProduct.Size = new System.Drawing.Size(50, 17);
            this.chkFinishedProduct.TabIndex = 11;
            this.chkFinishedProduct.Text = "成品";
            this.chkFinishedProduct.UseVisualStyleBackColor = true;
            // 
            // chkSemimanuFacture
            // 
            this.chkSemimanuFacture.AutoSize = true;
            this.chkSemimanuFacture.Checked = true;
            this.chkSemimanuFacture.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSemimanuFacture.Location = new System.Drawing.Point(150, 9);
            this.chkSemimanuFacture.Name = "chkSemimanuFacture";
            this.chkSemimanuFacture.Size = new System.Drawing.Size(62, 17);
            this.chkSemimanuFacture.TabIndex = 11;
            this.chkSemimanuFacture.Text = "半成品";
            this.chkSemimanuFacture.UseVisualStyleBackColor = true;
            // 
            // FCongifDetail
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(555, 341);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.Button btSelected;
        private System.Windows.Forms.TextBox txtBigLineList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbtOneLine;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtPerDay;
        private System.Windows.Forms.RadioButton rbtPerTime;
        private UserControl.UCLabelEdit txtAutoRefrsh;
        private UserControl.UCLabelEdit txtWatchrRefrsh;
        private System.Windows.Forms.CheckBox chkFacWatchPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblProductCon;
        private System.Windows.Forms.CheckBox chkSemimanuFacture;
        private System.Windows.Forms.CheckBox chkFinishedProduct;

    }
}