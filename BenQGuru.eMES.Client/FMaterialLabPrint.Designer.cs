namespace BenQGuru.eMES.Client
{
    partial class FMaterialLabPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMaterialLabPrint));
            this.grpQuery = new System.Windows.Forms.GroupBox();
            this.ucVendor = new UserControl.UCLabelEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.ucEndDate = new UserControl.UCDatetTime();
            this.ucIQCNO = new UserControl.UCLabelEdit();
            this.ucBatch = new UserControl.UCLabelEdit();
            this.ucStartDate = new UserControl.UCDatetTime();
            this.ucBtnQuery = new UserControl.UCButton();
            this.ucLEItemCodeQuery = new UserControl.UCLabelEdit();
            this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucButton1 = new UserControl.UCButton();
            this.ucButtonPrint = new UserControl.UCButton();
            this.ucLabelComboxPrinter = new UserControl.UCLabelCombox();
            this.txtPrintNum = new UserControl.UCLabelEdit();
            this.ucSum = new UserControl.UCLabelEdit();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ultraGridMetrialDetial = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ucLabelComboxStorage = new UserControl.UCLabelCombox();
            this.grpQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMetrialDetial)).BeginInit();
            this.SuspendLayout();
            // 
            // grpQuery
            // 
            this.grpQuery.Controls.Add(this.ucLabelComboxStorage);
            this.grpQuery.Controls.Add(this.ucVendor);
            this.grpQuery.Controls.Add(this.label2);
            this.grpQuery.Controls.Add(this.ucEndDate);
            this.grpQuery.Controls.Add(this.ucIQCNO);
            this.grpQuery.Controls.Add(this.ucBatch);
            this.grpQuery.Controls.Add(this.ucStartDate);
            this.grpQuery.Controls.Add(this.ucBtnQuery);
            this.grpQuery.Controls.Add(this.ucLEItemCodeQuery);
            this.grpQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpQuery.Location = new System.Drawing.Point(0, 0);
            this.grpQuery.Name = "grpQuery";
            this.grpQuery.Size = new System.Drawing.Size(943, 127);
            this.grpQuery.TabIndex = 1;
            this.grpQuery.TabStop = false;
            // 
            // ucVendor
            // 
            this.ucVendor.AllowEditOnlyChecked = true;
            this.ucVendor.Caption = "供应商";
            this.ucVendor.Checked = false;
            this.ucVendor.EditType = UserControl.EditTypes.String;
            this.ucVendor.Location = new System.Drawing.Point(353, 71);
            this.ucVendor.MaxLength = 40;
            this.ucVendor.Multiline = false;
            this.ucVendor.Name = "ucVendor";
            this.ucVendor.PasswordChar = '\0';
            this.ucVendor.ReadOnly = false;
            this.ucVendor.ShowCheckBox = false;
            this.ucVendor.Size = new System.Drawing.Size(249, 24);
            this.ucVendor.TabIndex = 7;
            this.ucVendor.TabNext = true;
            this.ucVendor.Value = "";
            this.ucVendor.WidthType = UserControl.WidthTypes.Long;
            this.ucVendor.XAlign = 402;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "到";
            // 
            // ucEndDate
            // 
            this.ucEndDate.Caption = "";
            this.ucEndDate.Location = new System.Drawing.Point(177, 71);
            this.ucEndDate.Name = "ucEndDate";
            this.ucEndDate.ShowType = UserControl.DateTimeTypes.Date;
            this.ucEndDate.Size = new System.Drawing.Size(96, 21);
            this.ucEndDate.TabIndex = 6;
            this.ucEndDate.Value = new System.DateTime(2005, 12, 14, 0, 0, 0, 0);
            this.ucEndDate.XAlign = 185;
            // 
            // ucIQCNO
            // 
            this.ucIQCNO.AllowEditOnlyChecked = true;
            this.ucIQCNO.Caption = "单据号";
            this.ucIQCNO.Checked = false;
            this.ucIQCNO.EditType = UserControl.EditTypes.String;
            this.ucIQCNO.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucIQCNO.Location = new System.Drawing.Point(24, 42);
            this.ucIQCNO.MaxLength = 40;
            this.ucIQCNO.Multiline = false;
            this.ucIQCNO.Name = "ucIQCNO";
            this.ucIQCNO.PasswordChar = '\0';
            this.ucIQCNO.ReadOnly = false;
            this.ucIQCNO.ShowCheckBox = false;
            this.ucIQCNO.Size = new System.Drawing.Size(249, 24);
            this.ucIQCNO.TabIndex = 3;
            this.ucIQCNO.TabNext = true;
            this.ucIQCNO.Value = "";
            this.ucIQCNO.WidthType = UserControl.WidthTypes.Long;
            this.ucIQCNO.XAlign = 73;
            // 
            // ucBatch
            // 
            this.ucBatch.AllowEditOnlyChecked = true;
            this.ucBatch.Caption = "物料批号";
            this.ucBatch.Checked = false;
            this.ucBatch.EditType = UserControl.EditTypes.String;
            this.ucBatch.Location = new System.Drawing.Point(12, 12);
            this.ucBatch.MaxLength = 40;
            this.ucBatch.Multiline = false;
            this.ucBatch.Name = "ucBatch";
            this.ucBatch.PasswordChar = '\0';
            this.ucBatch.ReadOnly = false;
            this.ucBatch.ShowCheckBox = false;
            this.ucBatch.Size = new System.Drawing.Size(261, 24);
            this.ucBatch.TabIndex = 1;
            this.ucBatch.TabNext = true;
            this.ucBatch.Value = "";
            this.ucBatch.WidthType = UserControl.WidthTypes.Long;
            this.ucBatch.XAlign = 73;
            // 
            // ucStartDate
            // 
            this.ucStartDate.Caption = "收料日期";
            this.ucStartDate.Location = new System.Drawing.Point(13, 72);
            this.ucStartDate.Name = "ucStartDate";
            this.ucStartDate.ShowType = UserControl.DateTimeTypes.Date;
            this.ucStartDate.Size = new System.Drawing.Size(149, 21);
            this.ucStartDate.TabIndex = 5;
            this.ucStartDate.Value = new System.DateTime(2005, 12, 14, 0, 0, 0, 0);
            this.ucStartDate.XAlign = 74;
            // 
            // ucBtnQuery
            // 
            this.ucBtnQuery.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnQuery.BackgroundImage")));
            this.ucBtnQuery.ButtonType = UserControl.ButtonTypes.Query;
            this.ucBtnQuery.Caption = "查询";
            this.ucBtnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnQuery.Location = new System.Drawing.Point(683, 56);
            this.ucBtnQuery.Name = "ucBtnQuery";
            this.ucBtnQuery.Size = new System.Drawing.Size(88, 22);
            this.ucBtnQuery.TabIndex = 8;
            this.ucBtnQuery.Click += new System.EventHandler(this.ucBtnQuery_Click);
            // 
            // ucLEItemCodeQuery
            // 
            this.ucLEItemCodeQuery.AllowEditOnlyChecked = true;
            this.ucLEItemCodeQuery.Caption = "料号";
            this.ucLEItemCodeQuery.Checked = false;
            this.ucLEItemCodeQuery.EditType = UserControl.EditTypes.String;
            this.ucLEItemCodeQuery.Location = new System.Drawing.Point(365, 42);
            this.ucLEItemCodeQuery.MaxLength = 40;
            this.ucLEItemCodeQuery.Multiline = false;
            this.ucLEItemCodeQuery.Name = "ucLEItemCodeQuery";
            this.ucLEItemCodeQuery.PasswordChar = '\0';
            this.ucLEItemCodeQuery.ReadOnly = false;
            this.ucLEItemCodeQuery.ShowCheckBox = false;
            this.ucLEItemCodeQuery.Size = new System.Drawing.Size(237, 24);
            this.ucLEItemCodeQuery.TabIndex = 4;
            this.ucLEItemCodeQuery.TabNext = true;
            this.ucLEItemCodeQuery.Value = "";
            this.ucLEItemCodeQuery.WidthType = UserControl.WidthTypes.Long;
            this.ucLEItemCodeQuery.XAlign = 402;
            // 
            // ultraGridMain
            // 
            this.ultraGridMain.Location = new System.Drawing.Point(0, 0);
            this.ultraGridMain.Name = "ultraGridMain";
            this.ultraGridMain.Size = new System.Drawing.Size(384, 80);
            this.ultraGridMain.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.ucSum);
            this.groupBox1.Controls.Add(this.chkAll);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 349);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(943, 149);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucButton1);
            this.groupBox2.Controls.Add(this.ucButtonPrint);
            this.groupBox2.Controls.Add(this.ucLabelComboxPrinter);
            this.groupBox2.Controls.Add(this.txtPrintNum);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(937, 109);
            this.groupBox2.TabIndex = 176;
            this.groupBox2.TabStop = false;
            // 
            // ucButton1
            // 
            this.ucButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButton1.BackColor = System.Drawing.SystemColors.Control;
            this.ucButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton1.BackgroundImage")));
            this.ucButton1.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButton1.Caption = "退出";
            this.ucButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButton1.Location = new System.Drawing.Point(450, 72);
            this.ucButton1.Name = "ucButton1";
            this.ucButton1.Size = new System.Drawing.Size(88, 22);
            this.ucButton1.TabIndex = 13;
            // 
            // ucButtonPrint
            // 
            this.ucButtonPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButtonPrint.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonPrint.BackgroundImage")));
            this.ucButtonPrint.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonPrint.Caption = "打印";
            this.ucButtonPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonPrint.Location = new System.Drawing.Point(249, 72);
            this.ucButtonPrint.Name = "ucButtonPrint";
            this.ucButtonPrint.Size = new System.Drawing.Size(88, 22);
            this.ucButtonPrint.TabIndex = 12;
            this.ucButtonPrint.Click += new System.EventHandler(this.ucButtonPrint_Click);
            // 
            // ucLabelComboxPrinter
            // 
            this.ucLabelComboxPrinter.AllowEditOnlyChecked = true;
            this.ucLabelComboxPrinter.Caption = "打印机";
            this.ucLabelComboxPrinter.Checked = false;
            this.ucLabelComboxPrinter.Location = new System.Drawing.Point(198, 23);
            this.ucLabelComboxPrinter.Name = "ucLabelComboxPrinter";
            this.ucLabelComboxPrinter.SelectedIndex = -1;
            this.ucLabelComboxPrinter.ShowCheckBox = false;
            this.ucLabelComboxPrinter.Size = new System.Drawing.Size(449, 24);
            this.ucLabelComboxPrinter.TabIndex = 11;
            this.ucLabelComboxPrinter.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelComboxPrinter.XAlign = 247;
            // 
            // txtPrintNum
            // 
            this.txtPrintNum.AllowEditOnlyChecked = true;
            this.txtPrintNum.Caption = "打印数量";
            this.txtPrintNum.Checked = false;
            this.txtPrintNum.EditType = UserControl.EditTypes.Integer;
            this.txtPrintNum.Location = new System.Drawing.Point(23, 23);
            this.txtPrintNum.MaxLength = 40;
            this.txtPrintNum.Multiline = false;
            this.txtPrintNum.Name = "txtPrintNum";
            this.txtPrintNum.PasswordChar = '\0';
            this.txtPrintNum.ReadOnly = false;
            this.txtPrintNum.ShowCheckBox = false;
            this.txtPrintNum.Size = new System.Drawing.Size(111, 24);
            this.txtPrintNum.TabIndex = 10;
            this.txtPrintNum.TabNext = false;
            this.txtPrintNum.Value = "1";
            this.txtPrintNum.WidthType = UserControl.WidthTypes.Tiny;
            this.txtPrintNum.XAlign = 84;
            // 
            // ucSum
            // 
            this.ucSum.AllowEditOnlyChecked = true;
            this.ucSum.Caption = "总数量";
            this.ucSum.Checked = false;
            this.ucSum.EditType = UserControl.EditTypes.Integer;
            this.ucSum.Location = new System.Drawing.Point(600, 12);
            this.ucSum.MaxLength = 40;
            this.ucSum.Multiline = false;
            this.ucSum.Name = "ucSum";
            this.ucSum.PasswordChar = '\0';
            this.ucSum.ReadOnly = false;
            this.ucSum.ShowCheckBox = false;
            this.ucSum.Size = new System.Drawing.Size(149, 24);
            this.ucSum.TabIndex = 9;
            this.ucSum.TabNext = false;
            this.ucSum.Value = "0";
            this.ucSum.Visible = false;
            this.ucSum.WidthType = UserControl.WidthTypes.Small;
            this.ucSum.XAlign = 649;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(6, 12);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(48, 16);
            this.chkAll.TabIndex = 50;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ultraGridMetrialDetial);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 127);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(943, 222);
            this.panel1.TabIndex = 8;
            // 
            // ultraGridMetrialDetial
            // 
            this.ultraGridMetrialDetial.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ultraGridMetrialDetial.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridMetrialDetial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMetrialDetial.Location = new System.Drawing.Point(0, 0);
            this.ultraGridMetrialDetial.Name = "ultraGridMetrialDetial";
            this.ultraGridMetrialDetial.Size = new System.Drawing.Size(943, 222);
            this.ultraGridMetrialDetial.TabIndex = 0;
            this.ultraGridMetrialDetial.ClickCellButton += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridMetrialDetial_ClickCellButton);
            this.ultraGridMetrialDetial.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMetrialDetial_InitializeLayout);
            this.ultraGridMetrialDetial.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridMetrialDetial_CellChange);
            // 
            // ucLabelComboxStorage
            // 
            this.ucLabelComboxStorage.AllowEditOnlyChecked = true;
            this.ucLabelComboxStorage.Caption = "库别";
            this.ucLabelComboxStorage.Checked = false;
            this.ucLabelComboxStorage.Location = new System.Drawing.Point(365, 13);
            this.ucLabelComboxStorage.Name = "ucLabelComboxStorage";
            this.ucLabelComboxStorage.SelectedIndex = -1;
            this.ucLabelComboxStorage.ShowCheckBox = false;
            this.ucLabelComboxStorage.Size = new System.Drawing.Size(237, 23);
            this.ucLabelComboxStorage.TabIndex = 2;
            this.ucLabelComboxStorage.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelComboxStorage.XAlign = 402;
            // 
            // FMaterialLabPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(943, 498);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpQuery);
            this.Name = "FMaterialLabPrint";
            this.Text = "物料标签打印";
            this.Load += new System.EventHandler(this.FMaterialLabPrint_Load);
            this.grpQuery.ResumeLayout(false);
            this.grpQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMetrialDetial)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpQuery;
        private UserControl.UCLabelEdit ucIQCNO;
        private UserControl.UCLabelEdit ucBatch;
        private UserControl.UCDatetTime ucStartDate;
        private UserControl.UCButton ucBtnQuery;
        private UserControl.UCLabelEdit ucLEItemCodeQuery;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
        private System.Windows.Forms.Label label2;
        private UserControl.UCDatetTime ucEndDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCLabelEdit ucVendor;
        private System.Windows.Forms.CheckBox chkAll;
        private UserControl.UCLabelEdit ucSum;
        private System.Windows.Forms.Panel panel1;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMetrialDetial;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControl.UCLabelEdit txtPrintNum;
        private UserControl.UCLabelCombox ucLabelComboxPrinter;
        private UserControl.UCButton ucButtonPrint;
        private UserControl.UCButton ucButton1;
        private UserControl.UCLabelCombox ucLabelComboxStorage;
    }
}