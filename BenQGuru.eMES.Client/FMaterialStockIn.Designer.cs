namespace BenQGuru.eMES.Client
{
    partial class FMaterialStockIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMaterialStockIn));
            this.groupBoxTop = new System.Windows.Forms.GroupBox();
            this.ucLabelEditDocLine = new UserControl.UCLabelEdit();
            this.bntStockIn = new UserControl.UCButton();
            this.groupBoxOption = new System.Windows.Forms.GroupBox();
            this.radioButtonSaleDelay = new System.Windows.Forms.RadioButton();
            this.radioButtonSaleImm = new System.Windows.Forms.RadioButton();
            this.ucDateVoucher = new UserControl.UCDatetTime();
            this.ucDateAccount = new UserControl.UCDatetTime();
            this.ucLabelComboxStorageOut = new UserControl.UCLabelCombox();
            this.ucLabelEditMemo = new UserControl.UCLabelEdit();
            this.ucLabelEditQty = new UserControl.UCLabelEdit();
            this.ucLabelEditUnit = new UserControl.UCLabelEdit();
            this.ucLabelComboxStorageIn = new UserControl.UCLabelCombox();
            this.ucLabelEditDoc = new UserControl.UCLabelEdit();
            this.ucDateStockIn = new UserControl.UCDatetTime();
            this.ucLabelEditVendor = new UserControl.UCLabelEdit();
            this.ucLEItemCode = new UserControl.UCLabelEdit();
            this.ucLabelComboxBusinessCode = new UserControl.UCLabelCombox();
            this.groupBoxBottom = new System.Windows.Forms.GroupBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.ucButtonPrint = new UserControl.UCButton();
            this.ucLabelComboxPrinter = new UserControl.UCLabelCombox();
            this.txtPrintNum = new UserControl.UCLabelEdit();
            this.ucButtonClear = new UserControl.UCButton();
            this.groupBoxMiddle = new System.Windows.Forms.GroupBox();
            this.ultraGridStockIn = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBoxTop.SuspendLayout();
            this.groupBoxOption.SuspendLayout();
            this.groupBoxBottom.SuspendLayout();
            this.groupBoxMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridStockIn)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxTop
            // 
            this.groupBoxTop.Controls.Add(this.ucLabelEditDocLine);
            this.groupBoxTop.Controls.Add(this.bntStockIn);
            this.groupBoxTop.Controls.Add(this.groupBoxOption);
            this.groupBoxTop.Controls.Add(this.ucLabelEditMemo);
            this.groupBoxTop.Controls.Add(this.ucLabelEditQty);
            this.groupBoxTop.Controls.Add(this.ucLabelEditUnit);
            this.groupBoxTop.Controls.Add(this.ucLabelComboxStorageIn);
            this.groupBoxTop.Controls.Add(this.ucLabelEditDoc);
            this.groupBoxTop.Controls.Add(this.ucDateStockIn);
            this.groupBoxTop.Controls.Add(this.ucLabelEditVendor);
            this.groupBoxTop.Controls.Add(this.ucLEItemCode);
            this.groupBoxTop.Controls.Add(this.ucLabelComboxBusinessCode);
            this.groupBoxTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxTop.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTop.Name = "groupBoxTop";
            this.groupBoxTop.Size = new System.Drawing.Size(826, 179);
            this.groupBoxTop.TabIndex = 3;
            this.groupBoxTop.TabStop = false;
            // 
            // ucLabelEditDocLine
            // 
            this.ucLabelEditDocLine.AllowEditOnlyChecked = true;
            this.ucLabelEditDocLine.Caption = "单据行号";
            this.ucLabelEditDocLine.Checked = false;
            this.ucLabelEditDocLine.EditType = UserControl.EditTypes.Integer;
            this.ucLabelEditDocLine.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucLabelEditDocLine.Location = new System.Drawing.Point(212, 49);
            this.ucLabelEditDocLine.MaxLength = 8;
            this.ucLabelEditDocLine.Multiline = false;
            this.ucLabelEditDocLine.Name = "ucLabelEditDocLine";
            this.ucLabelEditDocLine.PasswordChar = '\0';
            this.ucLabelEditDocLine.ReadOnly = false;
            this.ucLabelEditDocLine.ShowCheckBox = false;
            this.ucLabelEditDocLine.Size = new System.Drawing.Size(111, 24);
            this.ucLabelEditDocLine.TabIndex = 6;
            this.ucLabelEditDocLine.TabNext = true;
            this.ucLabelEditDocLine.Value = "";
            this.ucLabelEditDocLine.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditDocLine.XAlign = 273;
            // 
            // bntStockIn
            // 
            this.bntStockIn.BackColor = System.Drawing.SystemColors.Control;
            this.bntStockIn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bntStockIn.BackgroundImage")));
            this.bntStockIn.ButtonType = UserControl.ButtonTypes.None;
            this.bntStockIn.Caption = "入库";
            this.bntStockIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntStockIn.Location = new System.Drawing.Point(568, 145);
            this.bntStockIn.Name = "bntStockIn";
            this.bntStockIn.Size = new System.Drawing.Size(88, 22);
            this.bntStockIn.TabIndex = 16;
            this.bntStockIn.TabStop = false;
            this.bntStockIn.Click += new System.EventHandler(this.bntStockIn_Click);
            // 
            // groupBoxOption
            // 
            this.groupBoxOption.Controls.Add(this.radioButtonSaleDelay);
            this.groupBoxOption.Controls.Add(this.radioButtonSaleImm);
            this.groupBoxOption.Controls.Add(this.ucDateVoucher);
            this.groupBoxOption.Controls.Add(this.ucDateAccount);
            this.groupBoxOption.Controls.Add(this.ucLabelComboxStorageOut);
            this.groupBoxOption.Location = new System.Drawing.Point(12, 106);
            this.groupBoxOption.Name = "groupBoxOption";
            this.groupBoxOption.Size = new System.Drawing.Size(515, 67);
            this.groupBoxOption.TabIndex = 48;
            this.groupBoxOption.TabStop = false;
            // 
            // radioButtonSaleDelay
            // 
            this.radioButtonSaleDelay.AutoSize = true;
            this.radioButtonSaleDelay.Checked = true;
            this.radioButtonSaleDelay.Location = new System.Drawing.Point(253, 39);
            this.radioButtonSaleDelay.Name = "radioButtonSaleDelay";
            this.radioButtonSaleDelay.Size = new System.Drawing.Size(59, 16);
            this.radioButtonSaleDelay.TabIndex = 14;
            this.radioButtonSaleDelay.TabStop = true;
            this.radioButtonSaleDelay.Text = "非即售";
            this.radioButtonSaleDelay.UseVisualStyleBackColor = true;
            // 
            // radioButtonSaleImm
            // 
            this.radioButtonSaleImm.AutoSize = true;
            this.radioButtonSaleImm.Location = new System.Drawing.Point(200, 39);
            this.radioButtonSaleImm.Name = "radioButtonSaleImm";
            this.radioButtonSaleImm.Size = new System.Drawing.Size(47, 16);
            this.radioButtonSaleImm.TabIndex = 13;
            this.radioButtonSaleImm.Text = "即售";
            this.radioButtonSaleImm.UseVisualStyleBackColor = true;
            // 
            // ucDateVoucher
            // 
            this.ucDateVoucher.Caption = "凭证日期";
            this.ucDateVoucher.Location = new System.Drawing.Point(12, 38);
            this.ucDateVoucher.Name = "ucDateVoucher";
            this.ucDateVoucher.ShowType = UserControl.DateTimeTypes.Date;
            this.ucDateVoucher.Size = new System.Drawing.Size(149, 21);
            this.ucDateVoucher.TabIndex = 12;
            this.ucDateVoucher.Value = new System.DateTime(2009, 2, 27, 0, 0, 0, 0);
            this.ucDateVoucher.XAlign = 73;
            // 
            // ucDateAccount
            // 
            this.ucDateAccount.Caption = "记账日期";
            this.ucDateAccount.Location = new System.Drawing.Point(12, 11);
            this.ucDateAccount.Name = "ucDateAccount";
            this.ucDateAccount.ShowType = UserControl.DateTimeTypes.Date;
            this.ucDateAccount.Size = new System.Drawing.Size(149, 21);
            this.ucDateAccount.TabIndex = 10;
            this.ucDateAccount.Value = new System.DateTime(2009, 2, 27, 0, 0, 0, 0);
            this.ucDateAccount.XAlign = 73;
            // 
            // ucLabelComboxStorageOut
            // 
            this.ucLabelComboxStorageOut.AllowEditOnlyChecked = true;
            this.ucLabelComboxStorageOut.Caption = "发货库别";
            this.ucLabelComboxStorageOut.Checked = false;
            this.ucLabelComboxStorageOut.Location = new System.Drawing.Point(200, 9);
            this.ucLabelComboxStorageOut.Name = "ucLabelComboxStorageOut";
            this.ucLabelComboxStorageOut.SelectedIndex = -1;
            this.ucLabelComboxStorageOut.ShowCheckBox = false;
            this.ucLabelComboxStorageOut.Size = new System.Drawing.Size(194, 23);
            this.ucLabelComboxStorageOut.TabIndex = 11;
            this.ucLabelComboxStorageOut.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxStorageOut.XAlign = 261;
            // 
            // ucLabelEditMemo
            // 
            this.ucLabelEditMemo.AllowEditOnlyChecked = true;
            this.ucLabelEditMemo.Caption = "备注";
            this.ucLabelEditMemo.Checked = false;
            this.ucLabelEditMemo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditMemo.Location = new System.Drawing.Point(533, 50);
            this.ucLabelEditMemo.MaxLength = 1000;
            this.ucLabelEditMemo.Multiline = true;
            this.ucLabelEditMemo.Name = "ucLabelEditMemo";
            this.ucLabelEditMemo.PasswordChar = '\0';
            this.ucLabelEditMemo.ReadOnly = false;
            this.ucLabelEditMemo.ShowCheckBox = false;
            this.ucLabelEditMemo.Size = new System.Drawing.Size(237, 88);
            this.ucLabelEditMemo.TabIndex = 15;
            this.ucLabelEditMemo.TabNext = true;
            this.ucLabelEditMemo.Value = "";
            this.ucLabelEditMemo.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditMemo.XAlign = 570;
            // 
            // ucLabelEditQty
            // 
            this.ucLabelEditQty.AllowEditOnlyChecked = true;
            this.ucLabelEditQty.Caption = "数量";
            this.ucLabelEditQty.Checked = false;
            this.ucLabelEditQty.EditType = UserControl.EditTypes.Integer;
            this.ucLabelEditQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucLabelEditQty.Location = new System.Drawing.Point(533, 19);
            this.ucLabelEditQty.MaxLength = 8;
            this.ucLabelEditQty.Multiline = false;
            this.ucLabelEditQty.Name = "ucLabelEditQty";
            this.ucLabelEditQty.PasswordChar = '\0';
            this.ucLabelEditQty.ReadOnly = false;
            this.ucLabelEditQty.ShowCheckBox = false;
            this.ucLabelEditQty.Size = new System.Drawing.Size(87, 24);
            this.ucLabelEditQty.TabIndex = 4;
            this.ucLabelEditQty.TabNext = true;
            this.ucLabelEditQty.Value = "";
            this.ucLabelEditQty.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditQty.XAlign = 570;
            // 
            // ucLabelEditUnit
            // 
            this.ucLabelEditUnit.AllowEditOnlyChecked = true;
            this.ucLabelEditUnit.Caption = "单位";
            this.ucLabelEditUnit.Checked = false;
            this.ucLabelEditUnit.EditType = UserControl.EditTypes.String;
            this.ucLabelEditUnit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucLabelEditUnit.Location = new System.Drawing.Point(236, 19);
            this.ucLabelEditUnit.MaxLength = 40;
            this.ucLabelEditUnit.Multiline = false;
            this.ucLabelEditUnit.Name = "ucLabelEditUnit";
            this.ucLabelEditUnit.PasswordChar = '\0';
            this.ucLabelEditUnit.ReadOnly = false;
            this.ucLabelEditUnit.ShowCheckBox = false;
            this.ucLabelEditUnit.Size = new System.Drawing.Size(87, 24);
            this.ucLabelEditUnit.TabIndex = 2;
            this.ucLabelEditUnit.TabNext = true;
            this.ucLabelEditUnit.Value = "";
            this.ucLabelEditUnit.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditUnit.XAlign = 273;
            // 
            // ucLabelComboxStorageIn
            // 
            this.ucLabelComboxStorageIn.AllowEditOnlyChecked = true;
            this.ucLabelComboxStorageIn.Caption = "入库库别";
            this.ucLabelComboxStorageIn.Checked = false;
            this.ucLabelComboxStorageIn.Location = new System.Drawing.Point(333, 19);
            this.ucLabelComboxStorageIn.Name = "ucLabelComboxStorageIn";
            this.ucLabelComboxStorageIn.SelectedIndex = -1;
            this.ucLabelComboxStorageIn.ShowCheckBox = false;
            this.ucLabelComboxStorageIn.Size = new System.Drawing.Size(194, 23);
            this.ucLabelComboxStorageIn.TabIndex = 3;
            this.ucLabelComboxStorageIn.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxStorageIn.XAlign = 394;
            // 
            // ucLabelEditDoc
            // 
            this.ucLabelEditDoc.AllowEditOnlyChecked = true;
            this.ucLabelEditDoc.Caption = "单据号";
            this.ucLabelEditDoc.Checked = false;
            this.ucLabelEditDoc.EditType = UserControl.EditTypes.String;
            this.ucLabelEditDoc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucLabelEditDoc.Location = new System.Drawing.Point(24, 49);
            this.ucLabelEditDoc.MaxLength = 40;
            this.ucLabelEditDoc.Multiline = false;
            this.ucLabelEditDoc.Name = "ucLabelEditDoc";
            this.ucLabelEditDoc.PasswordChar = '\0';
            this.ucLabelEditDoc.ReadOnly = false;
            this.ucLabelEditDoc.ShowCheckBox = false;
            this.ucLabelEditDoc.Size = new System.Drawing.Size(182, 24);
            this.ucLabelEditDoc.TabIndex = 5;
            this.ucLabelEditDoc.TabNext = true;
            this.ucLabelEditDoc.Value = "";
            this.ucLabelEditDoc.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditDoc.XAlign = 73;
            // 
            // ucDateStockIn
            // 
            this.ucDateStockIn.Caption = "收料日期";
            this.ucDateStockIn.Location = new System.Drawing.Point(12, 79);
            this.ucDateStockIn.Name = "ucDateStockIn";
            this.ucDateStockIn.ShowType = UserControl.DateTimeTypes.Date;
            this.ucDateStockIn.Size = new System.Drawing.Size(161, 21);
            this.ucDateStockIn.TabIndex = 8;
            this.ucDateStockIn.Value = new System.DateTime(2005, 12, 14, 0, 0, 0, 0);
            this.ucDateStockIn.XAlign = 73;
            // 
            // ucLabelEditVendor
            // 
            this.ucLabelEditVendor.AllowEditOnlyChecked = true;
            this.ucLabelEditVendor.Caption = "供应商";
            this.ucLabelEditVendor.Checked = false;
            this.ucLabelEditVendor.EditType = UserControl.EditTypes.String;
            this.ucLabelEditVendor.Location = new System.Drawing.Point(226, 80);
            this.ucLabelEditVendor.MaxLength = 40;
            this.ucLabelEditVendor.Multiline = false;
            this.ucLabelEditVendor.Name = "ucLabelEditVendor";
            this.ucLabelEditVendor.PasswordChar = '\0';
            this.ucLabelEditVendor.ReadOnly = false;
            this.ucLabelEditVendor.ShowCheckBox = false;
            this.ucLabelEditVendor.Size = new System.Drawing.Size(182, 24);
            this.ucLabelEditVendor.TabIndex = 9;
            this.ucLabelEditVendor.TabNext = true;
            this.ucLabelEditVendor.Value = "";
            this.ucLabelEditVendor.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditVendor.XAlign = 275;
            // 
            // ucLEItemCode
            // 
            this.ucLEItemCode.AllowEditOnlyChecked = true;
            this.ucLEItemCode.Caption = "料号";
            this.ucLEItemCode.Checked = false;
            this.ucLEItemCode.EditType = UserControl.EditTypes.String;
            this.ucLEItemCode.Location = new System.Drawing.Point(357, 49);
            this.ucLEItemCode.MaxLength = 40;
            this.ucLEItemCode.Multiline = false;
            this.ucLEItemCode.Name = "ucLEItemCode";
            this.ucLEItemCode.PasswordChar = '\0';
            this.ucLEItemCode.ReadOnly = false;
            this.ucLEItemCode.ShowCheckBox = false;
            this.ucLEItemCode.Size = new System.Drawing.Size(170, 24);
            this.ucLEItemCode.TabIndex = 7;
            this.ucLEItemCode.TabNext = true;
            this.ucLEItemCode.Value = "";
            this.ucLEItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemCode.XAlign = 394;
            // 
            // ucLabelComboxBusinessCode
            // 
            this.ucLabelComboxBusinessCode.AllowEditOnlyChecked = true;
            this.ucLabelComboxBusinessCode.Caption = "业务代码";
            this.ucLabelComboxBusinessCode.Checked = false;
            this.ucLabelComboxBusinessCode.Location = new System.Drawing.Point(12, 19);
            this.ucLabelComboxBusinessCode.Name = "ucLabelComboxBusinessCode";
            this.ucLabelComboxBusinessCode.SelectedIndex = -1;
            this.ucLabelComboxBusinessCode.ShowCheckBox = false;
            this.ucLabelComboxBusinessCode.Size = new System.Drawing.Size(194, 23);
            this.ucLabelComboxBusinessCode.TabIndex = 1;
            this.ucLabelComboxBusinessCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxBusinessCode.XAlign = 73;
            this.ucLabelComboxBusinessCode.SelectedIndexChanged += new System.EventHandler(this.ucLabelComboxBusinessCode_SelectedIndexChanged);
            // 
            // groupBoxBottom
            // 
            this.groupBoxBottom.Controls.Add(this.chkAll);
            this.groupBoxBottom.Controls.Add(this.ucButtonPrint);
            this.groupBoxBottom.Controls.Add(this.ucLabelComboxPrinter);
            this.groupBoxBottom.Controls.Add(this.txtPrintNum);
            this.groupBoxBottom.Controls.Add(this.ucButtonClear);
            this.groupBoxBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxBottom.Location = new System.Drawing.Point(0, 470);
            this.groupBoxBottom.Name = "groupBoxBottom";
            this.groupBoxBottom.Size = new System.Drawing.Size(826, 70);
            this.groupBoxBottom.TabIndex = 5;
            this.groupBoxBottom.TabStop = false;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(12, 20);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(48, 16);
            this.chkAll.TabIndex = 51;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // ucButtonPrint
            // 
            this.ucButtonPrint.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonPrint.BackgroundImage")));
            this.ucButtonPrint.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonPrint.Caption = "打印";
            this.ucButtonPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonPrint.Location = new System.Drawing.Point(474, 20);
            this.ucButtonPrint.Name = "ucButtonPrint";
            this.ucButtonPrint.Size = new System.Drawing.Size(88, 22);
            this.ucButtonPrint.TabIndex = 19;
            this.ucButtonPrint.TabStop = false;
            this.ucButtonPrint.Click += new System.EventHandler(this.ucButtonPrint_Click);
            // 
            // ucLabelComboxPrinter
            // 
            this.ucLabelComboxPrinter.AllowEditOnlyChecked = true;
            this.ucLabelComboxPrinter.Caption = "打印机";
            this.ucLabelComboxPrinter.Checked = false;
            this.ucLabelComboxPrinter.Location = new System.Drawing.Point(183, 20);
            this.ucLabelComboxPrinter.Name = "ucLabelComboxPrinter";
            this.ucLabelComboxPrinter.SelectedIndex = -1;
            this.ucLabelComboxPrinter.ShowCheckBox = false;
            this.ucLabelComboxPrinter.Size = new System.Drawing.Size(249, 24);
            this.ucLabelComboxPrinter.TabIndex = 18;
            this.ucLabelComboxPrinter.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelComboxPrinter.XAlign = 232;
            // 
            // txtPrintNum
            // 
            this.txtPrintNum.AllowEditOnlyChecked = true;
            this.txtPrintNum.Caption = "打印数量";
            this.txtPrintNum.Checked = false;
            this.txtPrintNum.EditType = UserControl.EditTypes.Integer;
            this.txtPrintNum.Location = new System.Drawing.Point(66, 20);
            this.txtPrintNum.MaxLength = 40;
            this.txtPrintNum.Multiline = false;
            this.txtPrintNum.Name = "txtPrintNum";
            this.txtPrintNum.PasswordChar = '\0';
            this.txtPrintNum.ReadOnly = false;
            this.txtPrintNum.ShowCheckBox = false;
            this.txtPrintNum.Size = new System.Drawing.Size(111, 24);
            this.txtPrintNum.TabIndex = 17;
            this.txtPrintNum.TabNext = false;
            this.txtPrintNum.Value = "1";
            this.txtPrintNum.WidthType = UserControl.WidthTypes.Tiny;
            this.txtPrintNum.XAlign = 127;
            // 
            // ucButtonClear
            // 
            this.ucButtonClear.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonClear.BackgroundImage")));
            this.ucButtonClear.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonClear.Caption = "清除";
            this.ucButtonClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonClear.Location = new System.Drawing.Point(568, 20);
            this.ucButtonClear.Name = "ucButtonClear";
            this.ucButtonClear.Size = new System.Drawing.Size(88, 22);
            this.ucButtonClear.TabIndex = 20;
            this.ucButtonClear.TabStop = false;
            this.ucButtonClear.Click += new System.EventHandler(this.ucButtonClear_Click);
            // 
            // groupBoxMiddle
            // 
            this.groupBoxMiddle.Controls.Add(this.ultraGridStockIn);
            this.groupBoxMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMiddle.Location = new System.Drawing.Point(0, 179);
            this.groupBoxMiddle.Name = "groupBoxMiddle";
            this.groupBoxMiddle.Size = new System.Drawing.Size(826, 291);
            this.groupBoxMiddle.TabIndex = 7;
            this.groupBoxMiddle.TabStop = false;
            // 
            // ultraGridStockIn
            // 
            this.ultraGridStockIn.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridStockIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridStockIn.Location = new System.Drawing.Point(3, 17);
            this.ultraGridStockIn.Name = "ultraGridStockIn";
            this.ultraGridStockIn.Size = new System.Drawing.Size(820, 271);
            this.ultraGridStockIn.TabIndex = 7;
            this.ultraGridStockIn.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridStockIn_InitializeLayout);
            this.ultraGridStockIn.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridStockIn_CellChange);
            // 
            // FMaterialStockIn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(826, 540);
            this.Controls.Add(this.groupBoxMiddle);
            this.Controls.Add(this.groupBoxBottom);
            this.Controls.Add(this.groupBoxTop);
            this.Name = "FMaterialStockIn";
            this.Text = "物料收料入库";
            this.Load += new System.EventHandler(this.FMaterialStockIn_Load);
            this.groupBoxTop.ResumeLayout(false);
            this.groupBoxOption.ResumeLayout(false);
            this.groupBoxOption.PerformLayout();
            this.groupBoxBottom.ResumeLayout(false);
            this.groupBoxBottom.PerformLayout();
            this.groupBoxMiddle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridStockIn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxTop;
        private UserControl.UCLabelCombox ucLabelComboxBusinessCode;
        private System.Windows.Forms.GroupBox groupBoxBottom;
        private UserControl.UCLabelEdit ucLEItemCode;
        private UserControl.UCLabelEdit ucLabelEditVendor;
        private UserControl.UCDatetTime ucDateStockIn;
        private UserControl.UCLabelEdit ucLabelEditDoc;
        private UserControl.UCLabelEdit ucLabelEditUnit;
        private UserControl.UCLabelCombox ucLabelComboxStorageIn;
        private UserControl.UCLabelEdit ucLabelEditQty;
        private UserControl.UCLabelEdit ucLabelEditMemo;
        private System.Windows.Forms.GroupBox groupBoxOption;
        private UserControl.UCLabelCombox ucLabelComboxStorageOut;
        private UserControl.UCDatetTime ucDateVoucher;
        private UserControl.UCDatetTime ucDateAccount;
        private System.Windows.Forms.RadioButton radioButtonSaleDelay;
        private System.Windows.Forms.RadioButton radioButtonSaleImm;
        private UserControl.UCButton bntStockIn;
        private UserControl.UCLabelEdit ucLabelEditDocLine;
        private UserControl.UCButton ucButtonClear;
        private UserControl.UCLabelEdit txtPrintNum;
        private UserControl.UCLabelCombox ucLabelComboxPrinter;
        private UserControl.UCButton ucButtonPrint;
        private System.Windows.Forms.GroupBox groupBoxMiddle;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridStockIn;
        private System.Windows.Forms.CheckBox chkAll;

    }
}