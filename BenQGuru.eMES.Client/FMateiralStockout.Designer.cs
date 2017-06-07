namespace BenQGuru.eMES.Client
{
    partial class FMateiralStockout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMateiralStockout));
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
            this.groupBoxQuery = new System.Windows.Forms.GroupBox();
            this.ucBtnQuery = new UserControl.UCButton();
            this.ucLabelEditVendor = new UserControl.UCLabelEdit();
            this.ucDateStockInTo = new UserControl.UCDatetTime();
            this.ucDateStockInFrom = new UserControl.UCDatetTime();
            this.ucLEItemCode = new UserControl.UCLabelEdit();
            this.ucLabelEditDoc = new UserControl.UCLabelEdit();
            this.ucLabelComboxStorageOut = new UserControl.UCLabelCombox();
            this.ucBatch = new UserControl.UCLabelEdit();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.ucLabelEditToItem = new UserControl.UCLabelEdit();
            this.btnSendMetrial = new UserControl.UCButton();
            this.btnClear = new UserControl.UCButton();
            this.label1 = new System.Windows.Forms.Label();
            this.edtBufferDate = new UserControl.UCLabelEdit();
            this.opsCheckFIFO = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.groupBoxOption = new System.Windows.Forms.GroupBox();
            this.radioButtonSaleDelay = new System.Windows.Forms.RadioButton();
            this.radioButtonSaleImm = new System.Windows.Forms.RadioButton();
            this.ucDateVoucher = new UserControl.UCDatetTime();
            this.ucDateAccount = new UserControl.UCDatetTime();
            this.ucLabelComboxStorageIn = new UserControl.UCLabelCombox();
            this.ucLabelEditMemo = new UserControl.UCLabelEdit();
            this.ucLabelEditIssueNo = new UserControl.UCLabelEdit();
            this.ucLabelComboxBusinessCode = new UserControl.UCLabelCombox();
            this.groupBoxGrid = new System.Windows.Forms.GroupBox();
            this.ultraGridMetrialDetial = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.groupBoxQuery.SuspendLayout();
            this.groupBoxAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opsCheckFIFO)).BeginInit();
            this.groupBoxOption.SuspendLayout();
            this.groupBoxGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMetrialDetial)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxQuery
            // 
            this.groupBoxQuery.Controls.Add(this.ucBtnQuery);
            this.groupBoxQuery.Controls.Add(this.ucLabelEditVendor);
            this.groupBoxQuery.Controls.Add(this.ucDateStockInTo);
            this.groupBoxQuery.Controls.Add(this.ucDateStockInFrom);
            this.groupBoxQuery.Controls.Add(this.ucLEItemCode);
            this.groupBoxQuery.Controls.Add(this.ucLabelEditDoc);
            this.groupBoxQuery.Controls.Add(this.ucLabelComboxStorageOut);
            this.groupBoxQuery.Controls.Add(this.ucBatch);
            this.groupBoxQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxQuery.Location = new System.Drawing.Point(0, 0);
            this.groupBoxQuery.Name = "groupBoxQuery";
            this.groupBoxQuery.Size = new System.Drawing.Size(826, 99);
            this.groupBoxQuery.TabIndex = 0;
            this.groupBoxQuery.TabStop = false;
            // 
            // ucBtnQuery
            // 
            this.ucBtnQuery.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnQuery.BackgroundImage")));
            this.ucBtnQuery.ButtonType = UserControl.ButtonTypes.Query;
            this.ucBtnQuery.Caption = "查询";
            this.ucBtnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnQuery.Location = new System.Drawing.Point(491, 71);
            this.ucBtnQuery.Name = "ucBtnQuery";
            this.ucBtnQuery.Size = new System.Drawing.Size(88, 22);
            this.ucBtnQuery.TabIndex = 8;
            this.ucBtnQuery.Click += new System.EventHandler(this.ucBtnQuery_Click);
            // 
            // ucLabelEditVendor
            // 
            this.ucLabelEditVendor.AllowEditOnlyChecked = true;
            this.ucLabelEditVendor.Caption = "供应商";
            this.ucLabelEditVendor.Checked = false;
            this.ucLabelEditVendor.EditType = UserControl.EditTypes.String;
            this.ucLabelEditVendor.Location = new System.Drawing.Point(289, 72);
            this.ucLabelEditVendor.MaxLength = 40;
            this.ucLabelEditVendor.Multiline = false;
            this.ucLabelEditVendor.Name = "ucLabelEditVendor";
            this.ucLabelEditVendor.PasswordChar = '\0';
            this.ucLabelEditVendor.ReadOnly = false;
            this.ucLabelEditVendor.ShowCheckBox = false;
            this.ucLabelEditVendor.Size = new System.Drawing.Size(182, 24);
            this.ucLabelEditVendor.TabIndex = 7;
            this.ucLabelEditVendor.TabNext = true;
            this.ucLabelEditVendor.Value = "";
            this.ucLabelEditVendor.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditVendor.XAlign = 338;
            // 
            // ucDateStockInTo
            // 
            this.ucDateStockInTo.Caption = "到";
            this.ucDateStockInTo.Location = new System.Drawing.Point(160, 72);
            this.ucDateStockInTo.Name = "ucDateStockInTo";
            this.ucDateStockInTo.ShowType = UserControl.DateTimeTypes.Date;
            this.ucDateStockInTo.Size = new System.Drawing.Size(113, 21);
            this.ucDateStockInTo.TabIndex = 6;
            this.ucDateStockInTo.Value = new System.DateTime(2009, 2, 27, 0, 0, 0, 0);
            this.ucDateStockInTo.XAlign = 185;
            // 
            // ucDateStockInFrom
            // 
            this.ucDateStockInFrom.Caption = "收料日期";
            this.ucDateStockInFrom.Location = new System.Drawing.Point(12, 72);
            this.ucDateStockInFrom.Name = "ucDateStockInFrom";
            this.ucDateStockInFrom.ShowType = UserControl.DateTimeTypes.Date;
            this.ucDateStockInFrom.Size = new System.Drawing.Size(149, 21);
            this.ucDateStockInFrom.TabIndex = 5;
            this.ucDateStockInFrom.Value = new System.DateTime(2009, 2, 27, 0, 0, 0, 0);
            this.ucDateStockInFrom.XAlign = 73;
            // 
            // ucLEItemCode
            // 
            this.ucLEItemCode.AllowEditOnlyChecked = true;
            this.ucLEItemCode.Caption = "料号";
            this.ucLEItemCode.Checked = false;
            this.ucLEItemCode.EditType = UserControl.EditTypes.String;
            this.ucLEItemCode.Location = new System.Drawing.Point(301, 42);
            this.ucLEItemCode.MaxLength = 40;
            this.ucLEItemCode.Multiline = false;
            this.ucLEItemCode.Name = "ucLEItemCode";
            this.ucLEItemCode.PasswordChar = '\0';
            this.ucLEItemCode.ReadOnly = false;
            this.ucLEItemCode.ShowCheckBox = false;
            this.ucLEItemCode.Size = new System.Drawing.Size(170, 24);
            this.ucLEItemCode.TabIndex = 4;
            this.ucLEItemCode.TabNext = true;
            this.ucLEItemCode.Value = "";
            this.ucLEItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemCode.XAlign = 338;
            // 
            // ucLabelEditDoc
            // 
            this.ucLabelEditDoc.AllowEditOnlyChecked = true;
            this.ucLabelEditDoc.Caption = "单据号";
            this.ucLabelEditDoc.Checked = false;
            this.ucLabelEditDoc.EditType = UserControl.EditTypes.String;
            this.ucLabelEditDoc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucLabelEditDoc.Location = new System.Drawing.Point(24, 42);
            this.ucLabelEditDoc.MaxLength = 40;
            this.ucLabelEditDoc.Multiline = false;
            this.ucLabelEditDoc.Name = "ucLabelEditDoc";
            this.ucLabelEditDoc.PasswordChar = '\0';
            this.ucLabelEditDoc.ReadOnly = false;
            this.ucLabelEditDoc.ShowCheckBox = false;
            this.ucLabelEditDoc.Size = new System.Drawing.Size(249, 24);
            this.ucLabelEditDoc.TabIndex = 3;
            this.ucLabelEditDoc.TabNext = true;
            this.ucLabelEditDoc.Value = "";
            this.ucLabelEditDoc.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditDoc.XAlign = 73;
            // 
            // ucLabelComboxStorageOut
            // 
            this.ucLabelComboxStorageOut.AllowEditOnlyChecked = true;
            this.ucLabelComboxStorageOut.Caption = "库别";
            this.ucLabelComboxStorageOut.Checked = false;
            this.ucLabelComboxStorageOut.Location = new System.Drawing.Point(301, 12);
            this.ucLabelComboxStorageOut.Name = "ucLabelComboxStorageOut";
            this.ucLabelComboxStorageOut.SelectedIndex = -1;
            this.ucLabelComboxStorageOut.ShowCheckBox = false;
            this.ucLabelComboxStorageOut.Size = new System.Drawing.Size(170, 23);
            this.ucLabelComboxStorageOut.TabIndex = 2;
            this.ucLabelComboxStorageOut.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxStorageOut.XAlign = 338;
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
            // groupBoxAction
            // 
            this.groupBoxAction.Controls.Add(this.chkAll);
            this.groupBoxAction.Controls.Add(this.ucLabelEditToItem);
            this.groupBoxAction.Controls.Add(this.btnSendMetrial);
            this.groupBoxAction.Controls.Add(this.btnClear);
            this.groupBoxAction.Controls.Add(this.label1);
            this.groupBoxAction.Controls.Add(this.edtBufferDate);
            this.groupBoxAction.Controls.Add(this.opsCheckFIFO);
            this.groupBoxAction.Controls.Add(this.groupBoxOption);
            this.groupBoxAction.Controls.Add(this.ucLabelComboxStorageIn);
            this.groupBoxAction.Controls.Add(this.ucLabelEditMemo);
            this.groupBoxAction.Controls.Add(this.ucLabelEditIssueNo);
            this.groupBoxAction.Controls.Add(this.ucLabelComboxBusinessCode);
            this.groupBoxAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxAction.Location = new System.Drawing.Point(0, 404);
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.Size = new System.Drawing.Size(826, 136);
            this.groupBoxAction.TabIndex = 1;
            this.groupBoxAction.TabStop = false;
            // 
            // ucLabelEditToItem
            // 
            this.ucLabelEditToItem.AllowEditOnlyChecked = true;
            this.ucLabelEditToItem.Caption = "转换料号";
            this.ucLabelEditToItem.Checked = false;
            this.ucLabelEditToItem.EditType = UserControl.EditTypes.String;
            this.ucLabelEditToItem.Location = new System.Drawing.Point(193, 99);
            this.ucLabelEditToItem.MaxLength = 40;
            this.ucLabelEditToItem.Multiline = false;
            this.ucLabelEditToItem.Name = "ucLabelEditToItem";
            this.ucLabelEditToItem.PasswordChar = '\0';
            this.ucLabelEditToItem.ReadOnly = false;
            this.ucLabelEditToItem.ShowCheckBox = true;
            this.ucLabelEditToItem.Size = new System.Drawing.Size(210, 24);
            this.ucLabelEditToItem.TabIndex = 12;
            this.ucLabelEditToItem.TabNext = true;
            this.ucLabelEditToItem.Value = "";
            this.ucLabelEditToItem.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditToItem.XAlign = 270;
            // 
            // btnSendMetrial
            // 
            this.btnSendMetrial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSendMetrial.BackColor = System.Drawing.SystemColors.Control;
            this.btnSendMetrial.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSendMetrial.BackgroundImage")));
            this.btnSendMetrial.ButtonType = UserControl.ButtonTypes.None;
            this.btnSendMetrial.Caption = "发料";
            this.btnSendMetrial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendMetrial.Location = new System.Drawing.Point(726, 28);
            this.btnSendMetrial.Name = "btnSendMetrial";
            this.btnSendMetrial.Size = new System.Drawing.Size(88, 22);
            this.btnSendMetrial.TabIndex = 15;
            this.btnSendMetrial.Click += new System.EventHandler(this.btnSendMetrial_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.BackColor = System.Drawing.SystemColors.Control;
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.ButtonType = UserControl.ButtonTypes.None;
            this.btnClear.Caption = "清空";
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Location = new System.Drawing.Point(726, 56);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 22);
            this.btnClear.TabIndex = 16;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(527, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 51;
            this.label1.Text = "天";
            // 
            // edtBufferDate
            // 
            this.edtBufferDate.AllowEditOnlyChecked = true;
            this.edtBufferDate.Caption = "缓冲期限";
            this.edtBufferDate.Checked = false;
            this.edtBufferDate.EditType = UserControl.EditTypes.Integer;
            this.edtBufferDate.Location = new System.Drawing.Point(409, 71);
            this.edtBufferDate.MaxLength = 40;
            this.edtBufferDate.Multiline = false;
            this.edtBufferDate.Name = "edtBufferDate";
            this.edtBufferDate.PasswordChar = '\0';
            this.edtBufferDate.ReadOnly = false;
            this.edtBufferDate.ShowCheckBox = false;
            this.edtBufferDate.Size = new System.Drawing.Size(111, 24);
            this.edtBufferDate.TabIndex = 14;
            this.edtBufferDate.TabNext = false;
            this.edtBufferDate.Value = "1";
            this.edtBufferDate.WidthType = UserControl.WidthTypes.Tiny;
            this.edtBufferDate.XAlign = 470;
            // 
            // opsCheckFIFO
            // 
            appearance7.FontData.BoldAsString = "False";
            this.opsCheckFIFO.Appearance = appearance7;
            this.opsCheckFIFO.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.opsCheckFIFO.FlatMode = true;
            this.opsCheckFIFO.ItemAppearance = appearance8;
            valueListItem7.DataValue = "0";
            valueListItem7.DisplayText = "普通FIFO检查";
            valueListItem8.DataValue = "1";
            valueListItem8.DisplayText = "按供应商FIFO检查";
            this.opsCheckFIFO.Items.Add(valueListItem7);
            this.opsCheckFIFO.Items.Add(valueListItem8);
            this.opsCheckFIFO.ItemSpacingVertical = 10;
            this.opsCheckFIFO.Location = new System.Drawing.Point(409, 15);
            this.opsCheckFIFO.Name = "opsCheckFIFO";
            this.opsCheckFIFO.Size = new System.Drawing.Size(120, 50);
            this.opsCheckFIFO.TabIndex = 13;
            // 
            // groupBoxOption
            // 
            this.groupBoxOption.Controls.Add(this.radioButtonSaleDelay);
            this.groupBoxOption.Controls.Add(this.radioButtonSaleImm);
            this.groupBoxOption.Controls.Add(this.ucDateVoucher);
            this.groupBoxOption.Controls.Add(this.ucDateAccount);
            this.groupBoxOption.Location = new System.Drawing.Point(6, 42);
            this.groupBoxOption.Name = "groupBoxOption";
            this.groupBoxOption.Size = new System.Drawing.Size(181, 88);
            this.groupBoxOption.TabIndex = 49;
            this.groupBoxOption.TabStop = false;
            // 
            // radioButtonSaleDelay
            // 
            this.radioButtonSaleDelay.AutoSize = true;
            this.radioButtonSaleDelay.Checked = true;
            this.radioButtonSaleDelay.Location = new System.Drawing.Point(62, 65);
            this.radioButtonSaleDelay.Name = "radioButtonSaleDelay";
            this.radioButtonSaleDelay.Size = new System.Drawing.Size(59, 16);
            this.radioButtonSaleDelay.TabIndex = 19;
            this.radioButtonSaleDelay.TabStop = true;
            this.radioButtonSaleDelay.Text = "非即售";
            this.radioButtonSaleDelay.UseVisualStyleBackColor = true;
            // 
            // radioButtonSaleImm
            // 
            this.radioButtonSaleImm.AutoSize = true;
            this.radioButtonSaleImm.Location = new System.Drawing.Point(9, 65);
            this.radioButtonSaleImm.Name = "radioButtonSaleImm";
            this.radioButtonSaleImm.Size = new System.Drawing.Size(47, 16);
            this.radioButtonSaleImm.TabIndex = 18;
            this.radioButtonSaleImm.Text = "即售";
            this.radioButtonSaleImm.UseVisualStyleBackColor = true;
            // 
            // ucDateVoucher
            // 
            this.ucDateVoucher.Caption = "凭证日期";
            this.ucDateVoucher.Location = new System.Drawing.Point(9, 38);
            this.ucDateVoucher.Name = "ucDateVoucher";
            this.ucDateVoucher.ShowType = UserControl.DateTimeTypes.Date;
            this.ucDateVoucher.Size = new System.Drawing.Size(149, 21);
            this.ucDateVoucher.TabIndex = 17;
            this.ucDateVoucher.Value = new System.DateTime(2009, 2, 27, 0, 0, 0, 0);
            this.ucDateVoucher.XAlign = 70;
            // 
            // ucDateAccount
            // 
            this.ucDateAccount.Caption = "记账日期";
            this.ucDateAccount.Location = new System.Drawing.Point(9, 11);
            this.ucDateAccount.Name = "ucDateAccount";
            this.ucDateAccount.ShowType = UserControl.DateTimeTypes.Date;
            this.ucDateAccount.Size = new System.Drawing.Size(149, 21);
            this.ucDateAccount.TabIndex = 16;
            this.ucDateAccount.Value = new System.DateTime(2009, 2, 27, 0, 0, 0, 0);
            this.ucDateAccount.XAlign = 70;
            // 
            // ucLabelComboxStorageIn
            // 
            this.ucLabelComboxStorageIn.AllowEditOnlyChecked = true;
            this.ucLabelComboxStorageIn.Caption = "入库库别";
            this.ucLabelComboxStorageIn.Checked = false;
            this.ucLabelComboxStorageIn.Location = new System.Drawing.Point(209, 45);
            this.ucLabelComboxStorageIn.Name = "ucLabelComboxStorageIn";
            this.ucLabelComboxStorageIn.SelectedIndex = -1;
            this.ucLabelComboxStorageIn.ShowCheckBox = false;
            this.ucLabelComboxStorageIn.Size = new System.Drawing.Size(194, 23);
            this.ucLabelComboxStorageIn.TabIndex = 10;
            this.ucLabelComboxStorageIn.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxStorageIn.XAlign = 270;
            // 
            // ucLabelEditMemo
            // 
            this.ucLabelEditMemo.AllowEditOnlyChecked = true;
            this.ucLabelEditMemo.AutoScroll = true;
            this.ucLabelEditMemo.Caption = "备注";
            this.ucLabelEditMemo.Checked = false;
            this.ucLabelEditMemo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditMemo.Location = new System.Drawing.Point(550, 15);
            this.ucLabelEditMemo.MaxLength = 1000;
            this.ucLabelEditMemo.Multiline = true;
            this.ucLabelEditMemo.Name = "ucLabelEditMemo";
            this.ucLabelEditMemo.PasswordChar = '\0';
            this.ucLabelEditMemo.ReadOnly = false;
            this.ucLabelEditMemo.ShowCheckBox = false;
            this.ucLabelEditMemo.Size = new System.Drawing.Size(170, 105);
            this.ucLabelEditMemo.TabIndex = 20;
            this.ucLabelEditMemo.TabNext = true;
            this.ucLabelEditMemo.Value = "";
            this.ucLabelEditMemo.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditMemo.XAlign = 587;
            // 
            // ucLabelEditIssueNo
            // 
            this.ucLabelEditIssueNo.AllowEditOnlyChecked = true;
            this.ucLabelEditIssueNo.Caption = "发料单号";
            this.ucLabelEditIssueNo.Checked = false;
            this.ucLabelEditIssueNo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditIssueNo.Location = new System.Drawing.Point(209, 74);
            this.ucLabelEditIssueNo.MaxLength = 40;
            this.ucLabelEditIssueNo.Multiline = false;
            this.ucLabelEditIssueNo.Name = "ucLabelEditIssueNo";
            this.ucLabelEditIssueNo.PasswordChar = '\0';
            this.ucLabelEditIssueNo.ReadOnly = false;
            this.ucLabelEditIssueNo.ShowCheckBox = false;
            this.ucLabelEditIssueNo.Size = new System.Drawing.Size(194, 24);
            this.ucLabelEditIssueNo.TabIndex = 11;
            this.ucLabelEditIssueNo.TabNext = true;
            this.ucLabelEditIssueNo.Value = "";
            this.ucLabelEditIssueNo.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditIssueNo.XAlign = 270;
            // 
            // ucLabelComboxBusinessCode
            // 
            this.ucLabelComboxBusinessCode.AllowEditOnlyChecked = true;
            this.ucLabelComboxBusinessCode.Caption = "业务代码";
            this.ucLabelComboxBusinessCode.Checked = false;
            this.ucLabelComboxBusinessCode.Location = new System.Drawing.Point(209, 16);
            this.ucLabelComboxBusinessCode.Name = "ucLabelComboxBusinessCode";
            this.ucLabelComboxBusinessCode.SelectedIndex = -1;
            this.ucLabelComboxBusinessCode.ShowCheckBox = false;
            this.ucLabelComboxBusinessCode.Size = new System.Drawing.Size(194, 23);
            this.ucLabelComboxBusinessCode.TabIndex = 9;
            this.ucLabelComboxBusinessCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxBusinessCode.XAlign = 270;
            this.ucLabelComboxBusinessCode.SelectedIndexChanged += new System.EventHandler(this.ucLabelComboxBusinessCode_SelectedIndexChanged);
            // 
            // groupBoxGrid
            // 
            this.groupBoxGrid.Controls.Add(this.ultraGridMetrialDetial);
            this.groupBoxGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxGrid.Location = new System.Drawing.Point(0, 99);
            this.groupBoxGrid.Name = "groupBoxGrid";
            this.groupBoxGrid.Size = new System.Drawing.Size(826, 305);
            this.groupBoxGrid.TabIndex = 2;
            this.groupBoxGrid.TabStop = false;
            // 
            // ultraGridMetrialDetial
            // 
            this.ultraGridMetrialDetial.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridMetrialDetial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMetrialDetial.Location = new System.Drawing.Point(3, 17);
            this.ultraGridMetrialDetial.Name = "ultraGridMetrialDetial";
            this.ultraGridMetrialDetial.Size = new System.Drawing.Size(820, 285);
            this.ultraGridMetrialDetial.TabIndex = 0;
            this.ultraGridMetrialDetial.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMetrialDetial_InitializeLayout);
            this.ultraGridMetrialDetial.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridMetrialDetial_CellChange);
            // 
            // chkAll
            // 
            this.chkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(12, 20);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(48, 16);
            this.chkAll.TabIndex = 173;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // FMateiralStockout
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(826, 540);
            this.Controls.Add(this.groupBoxGrid);
            this.Controls.Add(this.groupBoxAction);
            this.Controls.Add(this.groupBoxQuery);
            this.Name = "FMateiralStockout";
            this.Text = "其他业务发料";
            this.Load += new System.EventHandler(this.FMateiralStockout_Load);
            this.groupBoxQuery.ResumeLayout(false);
            this.groupBoxAction.ResumeLayout(false);
            this.groupBoxAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opsCheckFIFO)).EndInit();
            this.groupBoxOption.ResumeLayout(false);
            this.groupBoxOption.PerformLayout();
            this.groupBoxGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMetrialDetial)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxQuery;
        private UserControl.UCLabelEdit ucBatch;
        private UserControl.UCLabelCombox ucLabelComboxStorageOut;
        private UserControl.UCLabelEdit ucLabelEditDoc;
        private UserControl.UCLabelEdit ucLEItemCode;
        private UserControl.UCDatetTime ucDateStockInTo;
        private UserControl.UCDatetTime ucDateStockInFrom;
        private UserControl.UCLabelEdit ucLabelEditVendor;
        private UserControl.UCButton ucBtnQuery;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private System.Windows.Forms.GroupBox groupBoxGrid;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMetrialDetial;
        private UserControl.UCLabelCombox ucLabelComboxBusinessCode;
        private UserControl.UCLabelEdit ucLabelEditIssueNo;
        private UserControl.UCLabelEdit ucLabelEditMemo;
        private UserControl.UCLabelCombox ucLabelComboxStorageIn;
        private System.Windows.Forms.GroupBox groupBoxOption;
        private System.Windows.Forms.RadioButton radioButtonSaleDelay;
        private System.Windows.Forms.RadioButton radioButtonSaleImm;
        private UserControl.UCDatetTime ucDateVoucher;
        private UserControl.UCDatetTime ucDateAccount;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet opsCheckFIFO;
        private System.Windows.Forms.Label label1;
        private UserControl.UCLabelEdit edtBufferDate;
        private UserControl.UCButton btnSendMetrial;
        private UserControl.UCButton btnClear;
        private UserControl.UCLabelEdit ucLabelEditToItem;
        private System.Windows.Forms.CheckBox chkAll;
    }
}