namespace BenQGuru.eMES.Client
{
    partial class FSendMetrial
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
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem13 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem14 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem15 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem16 = new Infragistics.Win.ValueListItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSendMetrial));
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.edtBufferDate = new UserControl.UCLabelEdit();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.opsCheckFIFO = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.edtMetrialLotNo = new UserControl.UCLabelEdit();
            this.edtIQCNo = new UserControl.UCLabelEdit();
            this.edtWorkSeat = new UserControl.UCLabelEdit();
            this.opsLoadMetrial = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.edtMoPlanQty = new UserControl.UCLabelEdit();
            this.edtitemDesc = new UserControl.UCLabelEdit();
            this.edtMoCode = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucLabelComboxPrinter = new UserControl.UCLabelCombox();
            this.edtPrint = new UserControl.UCLabelEdit();
            this.btnPrint = new UserControl.UCButton();
            this.btnSendMetrial = new UserControl.UCButton();
            this.btnClear = new UserControl.UCButton();
            this.ucDateTimeEnd = new UserControl.UCDatetTime();
            this.edtHeadText = new UserControl.UCLabelEdit();
            this.ucDateTimeStart = new UserControl.UCDatetTime();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ultraGridMetrialDetial = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.opsetCollectObject = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.radioButtonSaleImm = new System.Windows.Forms.RadioButton();
            this.radioButtonSaleDelay = new System.Windows.Forms.RadioButton();
            this.ucLabelComboxStorageIn = new UserControl.UCLabelCombox();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opsCheckFIFO)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opsLoadMetrial)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMetrialDetial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opsetCollectObject)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.edtBufferDate);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.edtMoPlanQty);
            this.groupBox1.Controls.Add(this.edtitemDesc);
            this.groupBox1.Controls.Add(this.edtMoCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(868, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(784, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "天";
            // 
            // edtBufferDate
            // 
            this.edtBufferDate.AllowEditOnlyChecked = true;
            this.edtBufferDate.Caption = "缓冲期限";
            this.edtBufferDate.Checked = false;
            this.edtBufferDate.EditType = UserControl.EditTypes.Integer;
            this.edtBufferDate.Location = new System.Drawing.Point(667, 65);
            this.edtBufferDate.MaxLength = 40;
            this.edtBufferDate.Multiline = false;
            this.edtBufferDate.Name = "edtBufferDate";
            this.edtBufferDate.PasswordChar = '\0';
            this.edtBufferDate.ReadOnly = false;
            this.edtBufferDate.ShowCheckBox = false;
            this.edtBufferDate.Size = new System.Drawing.Size(111, 24);
            this.edtBufferDate.TabIndex = 22;
            this.edtBufferDate.TabNext = false;
            this.edtBufferDate.Value = "0";
            this.edtBufferDate.WidthType = UserControl.WidthTypes.Tiny;
            this.edtBufferDate.XAlign = 728;
            this.edtBufferDate.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtBufferDate_TxtboxKeyPress);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox5.Controls.Add(this.opsCheckFIFO);
            this.groupBox5.Location = new System.Drawing.Point(521, 42);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(140, 76);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            // 
            // opsCheckFIFO
            // 
            appearance16.FontData.BoldAsString = "False";
            this.opsCheckFIFO.Appearance = appearance16;
            this.opsCheckFIFO.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.opsCheckFIFO.FlatMode = true;
            this.opsCheckFIFO.ItemAppearance = appearance17;
            valueListItem13.DataValue = "0";
            valueListItem13.DisplayText = "普通FIFO检查";
            valueListItem14.DataValue = "1";
            valueListItem14.DisplayText = "按供应商FIFO检查";
            this.opsCheckFIFO.Items.Add(valueListItem13);
            this.opsCheckFIFO.Items.Add(valueListItem14);
            this.opsCheckFIFO.ItemSpacingVertical = 10;
            this.opsCheckFIFO.Location = new System.Drawing.Point(6, 17);
            this.opsCheckFIFO.Name = "opsCheckFIFO";
            this.opsCheckFIFO.Size = new System.Drawing.Size(128, 45);
            this.opsCheckFIFO.TabIndex = 3;
            this.opsCheckFIFO.ValueChanged += new System.EventHandler(this.opsCheckFIFO_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.edtMetrialLotNo);
            this.groupBox4.Controls.Add(this.edtIQCNo);
            this.groupBox4.Controls.Add(this.edtWorkSeat);
            this.groupBox4.Controls.Add(this.opsLoadMetrial);
            this.groupBox4.Location = new System.Drawing.Point(12, 42);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(494, 76);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            // 
            // edtMetrialLotNo
            // 
            this.edtMetrialLotNo.AllowEditOnlyChecked = true;
            this.edtMetrialLotNo.Caption = "";
            this.edtMetrialLotNo.Checked = false;
            this.edtMetrialLotNo.EditType = UserControl.EditTypes.String;
            this.edtMetrialLotNo.Location = new System.Drawing.Point(74, 46);
            this.edtMetrialLotNo.MaxLength = 40;
            this.edtMetrialLotNo.Multiline = false;
            this.edtMetrialLotNo.Name = "edtMetrialLotNo";
            this.edtMetrialLotNo.PasswordChar = '\0';
            this.edtMetrialLotNo.ReadOnly = false;
            this.edtMetrialLotNo.ShowCheckBox = false;
            this.edtMetrialLotNo.Size = new System.Drawing.Size(408, 24);
            this.edtMetrialLotNo.TabIndex = 22;
            this.edtMetrialLotNo.TabNext = false;
            this.edtMetrialLotNo.Value = "";
            this.edtMetrialLotNo.WidthType = UserControl.WidthTypes.TooLong;
            this.edtMetrialLotNo.XAlign = 82;
            this.edtMetrialLotNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtMetrialLotNo_TxtboxKeyPress);
            // 
            // edtIQCNo
            // 
            this.edtIQCNo.AllowEditOnlyChecked = true;
            this.edtIQCNo.Caption = "IQC单号";
            this.edtIQCNo.Checked = false;
            this.edtIQCNo.EditType = UserControl.EditTypes.String;
            this.edtIQCNo.Location = new System.Drawing.Point(227, 16);
            this.edtIQCNo.MaxLength = 40;
            this.edtIQCNo.Multiline = false;
            this.edtIQCNo.Name = "edtIQCNo";
            this.edtIQCNo.PasswordChar = '\0';
            this.edtIQCNo.ReadOnly = false;
            this.edtIQCNo.ShowCheckBox = false;
            this.edtIQCNo.Size = new System.Drawing.Size(255, 24);
            this.edtIQCNo.TabIndex = 20;
            this.edtIQCNo.TabNext = false;
            this.edtIQCNo.Value = "";
            this.edtIQCNo.WidthType = UserControl.WidthTypes.Long;
            this.edtIQCNo.XAlign = 282;
            this.edtIQCNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtIQCNo_TxtboxKeyPress);
            // 
            // edtWorkSeat
            // 
            this.edtWorkSeat.AllowEditOnlyChecked = true;
            this.edtWorkSeat.Caption = "";
            this.edtWorkSeat.Checked = false;
            this.edtWorkSeat.EditType = UserControl.EditTypes.String;
            this.edtWorkSeat.Location = new System.Drawing.Point(74, 16);
            this.edtWorkSeat.MaxLength = 40;
            this.edtWorkSeat.Multiline = false;
            this.edtWorkSeat.Name = "edtWorkSeat";
            this.edtWorkSeat.PasswordChar = '\0';
            this.edtWorkSeat.ReadOnly = false;
            this.edtWorkSeat.ShowCheckBox = false;
            this.edtWorkSeat.Size = new System.Drawing.Size(108, 24);
            this.edtWorkSeat.TabIndex = 19;
            this.edtWorkSeat.TabNext = false;
            this.edtWorkSeat.Value = "";
            this.edtWorkSeat.WidthType = UserControl.WidthTypes.Small;
            this.edtWorkSeat.XAlign = 82;
            this.edtWorkSeat.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtWorkSeat_TxtboxKeyPress);
            // 
            // opsLoadMetrial
            // 
            appearance18.FontData.BoldAsString = "False";
            this.opsLoadMetrial.Appearance = appearance18;
            this.opsLoadMetrial.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.opsLoadMetrial.FlatMode = true;
            this.opsLoadMetrial.ItemAppearance = appearance19;
            valueListItem15.DataValue = "0";
            valueListItem15.DisplayText = "工位";
            valueListItem16.DataValue = "1";
            valueListItem16.DisplayText = "物料批号";
            this.opsLoadMetrial.Items.Add(valueListItem15);
            this.opsLoadMetrial.Items.Add(valueListItem16);
            this.opsLoadMetrial.ItemSpacingVertical = 10;
            this.opsLoadMetrial.Location = new System.Drawing.Point(6, 15);
            this.opsLoadMetrial.Name = "opsLoadMetrial";
            this.opsLoadMetrial.Size = new System.Drawing.Size(72, 50);
            this.opsLoadMetrial.TabIndex = 2;
            this.opsLoadMetrial.ValueChanged += new System.EventHandler(this.opsLoadMetrial_ValueChanged);
            // 
            // edtMoPlanQty
            // 
            this.edtMoPlanQty.AllowEditOnlyChecked = true;
            this.edtMoPlanQty.Caption = "发料套数";
            this.edtMoPlanQty.Checked = false;
            this.edtMoPlanQty.EditType = UserControl.EditTypes.Integer;
            this.edtMoPlanQty.Location = new System.Drawing.Point(667, 12);
            this.edtMoPlanQty.MaxLength = 8;
            this.edtMoPlanQty.Multiline = false;
            this.edtMoPlanQty.Name = "edtMoPlanQty";
            this.edtMoPlanQty.PasswordChar = '\0';
            this.edtMoPlanQty.ReadOnly = false;
            this.edtMoPlanQty.ShowCheckBox = false;
            this.edtMoPlanQty.Size = new System.Drawing.Size(111, 24);
            this.edtMoPlanQty.TabIndex = 19;
            this.edtMoPlanQty.TabNext = false;
            this.edtMoPlanQty.Value = "";
            this.edtMoPlanQty.WidthType = UserControl.WidthTypes.Tiny;
            this.edtMoPlanQty.XAlign = 728;
            this.edtMoPlanQty.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtMoPlanQty_TxtboxKeyPress);
            // 
            // edtitemDesc
            // 
            this.edtitemDesc.AllowEditOnlyChecked = true;
            this.edtitemDesc.Caption = "";
            this.edtitemDesc.Checked = false;
            this.edtitemDesc.EditType = UserControl.EditTypes.String;
            this.edtitemDesc.Enabled = false;
            this.edtitemDesc.Location = new System.Drawing.Point(222, 11);
            this.edtitemDesc.MaxLength = 40;
            this.edtitemDesc.Multiline = false;
            this.edtitemDesc.Name = "edtitemDesc";
            this.edtitemDesc.PasswordChar = '\0';
            this.edtitemDesc.ReadOnly = false;
            this.edtitemDesc.ShowCheckBox = false;
            this.edtitemDesc.Size = new System.Drawing.Size(408, 24);
            this.edtitemDesc.TabIndex = 18;
            this.edtitemDesc.TabNext = false;
            this.edtitemDesc.Value = "";
            this.edtitemDesc.WidthType = UserControl.WidthTypes.TooLong;
            this.edtitemDesc.XAlign = 230;
            // 
            // edtMoCode
            // 
            this.edtMoCode.AllowEditOnlyChecked = true;
            this.edtMoCode.Caption = "工单号码";
            this.edtMoCode.Checked = false;
            this.edtMoCode.EditType = UserControl.EditTypes.String;
            this.edtMoCode.Location = new System.Drawing.Point(12, 12);
            this.edtMoCode.MaxLength = 40;
            this.edtMoCode.Multiline = false;
            this.edtMoCode.Name = "edtMoCode";
            this.edtMoCode.PasswordChar = '\0';
            this.edtMoCode.ReadOnly = false;
            this.edtMoCode.ShowCheckBox = false;
            this.edtMoCode.Size = new System.Drawing.Size(194, 24);
            this.edtMoCode.TabIndex = 17;
            this.edtMoCode.TabNext = false;
            this.edtMoCode.Value = "";
            this.edtMoCode.WidthType = UserControl.WidthTypes.Normal;
            this.edtMoCode.XAlign = 73;
            this.edtMoCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtMoCode_TxtboxKeyPress);
            this.edtMoCode.InnerTextChanged += new System.EventHandler(this.edtMoCode_InnerTextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucLabelComboxStorageIn);
            this.groupBox2.Controls.Add(this.radioButtonSaleDelay);
            this.groupBox2.Controls.Add(this.radioButtonSaleImm);
            this.groupBox2.Controls.Add(this.ucLabelComboxPrinter);
            this.groupBox2.Controls.Add(this.edtPrint);
            this.groupBox2.Controls.Add(this.btnPrint);
            this.groupBox2.Controls.Add(this.btnSendMetrial);
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.ucDateTimeEnd);
            this.groupBox2.Controls.Add(this.edtHeadText);
            this.groupBox2.Controls.Add(this.ucDateTimeStart);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 453);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(868, 105);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // ucLabelComboxPrinter
            // 
            this.ucLabelComboxPrinter.AllowEditOnlyChecked = true;
            this.ucLabelComboxPrinter.Caption = "打印机";
            this.ucLabelComboxPrinter.Checked = false;
            this.ucLabelComboxPrinter.Location = new System.Drawing.Point(381, 16);
            this.ucLabelComboxPrinter.Name = "ucLabelComboxPrinter";
            this.ucLabelComboxPrinter.SelectedIndex = -1;
            this.ucLabelComboxPrinter.ShowCheckBox = false;
            this.ucLabelComboxPrinter.Size = new System.Drawing.Size(249, 24);
            this.ucLabelComboxPrinter.TabIndex = 26;
            this.ucLabelComboxPrinter.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelComboxPrinter.XAlign = 430;
            // 
            // edtPrint
            // 
            this.edtPrint.AllowEditOnlyChecked = true;
            this.edtPrint.Caption = "打印数量";
            this.edtPrint.Checked = false;
            this.edtPrint.EditType = UserControl.EditTypes.Integer;
            this.edtPrint.Location = new System.Drawing.Point(372, 71);
            this.edtPrint.MaxLength = 40;
            this.edtPrint.Multiline = false;
            this.edtPrint.Name = "edtPrint";
            this.edtPrint.PasswordChar = '\0';
            this.edtPrint.ReadOnly = false;
            this.edtPrint.ShowCheckBox = false;
            this.edtPrint.Size = new System.Drawing.Size(111, 24);
            this.edtPrint.TabIndex = 25;
            this.edtPrint.TabNext = false;
            this.edtPrint.Value = "1";
            this.edtPrint.WidthType = UserControl.WidthTypes.Tiny;
            this.edtPrint.XAlign = 433;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.ButtonType = UserControl.ButtonTypes.None;
            this.btnPrint.Caption = "打印";
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.Location = new System.Drawing.Point(539, 70);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(88, 22);
            this.btnPrint.TabIndex = 24;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSendMetrial
            // 
            this.btnSendMetrial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSendMetrial.BackColor = System.Drawing.SystemColors.Control;
            this.btnSendMetrial.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSendMetrial.BackgroundImage")));
            this.btnSendMetrial.ButtonType = UserControl.ButtonTypes.None;
            this.btnSendMetrial.Caption = "发料";
            this.btnSendMetrial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendMetrial.Location = new System.Drawing.Point(251, 70);
            this.btnSendMetrial.Name = "btnSendMetrial";
            this.btnSendMetrial.Size = new System.Drawing.Size(88, 22);
            this.btnSendMetrial.TabIndex = 23;
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
            this.btnClear.Location = new System.Drawing.Point(109, 70);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 22);
            this.btnClear.TabIndex = 22;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ucDateTimeEnd
            // 
            this.ucDateTimeEnd.Caption = "凭证时间";
            this.ucDateTimeEnd.Location = new System.Drawing.Point(12, 43);
            this.ucDateTimeEnd.Name = "ucDateTimeEnd";
            this.ucDateTimeEnd.ShowType = UserControl.DateTimeTypes.Date;
            this.ucDateTimeEnd.Size = new System.Drawing.Size(149, 21);
            this.ucDateTimeEnd.TabIndex = 20;
            this.ucDateTimeEnd.Value = new System.DateTime(2009, 2, 27, 0, 0, 0, 0);
            this.ucDateTimeEnd.XAlign = 73;
            // 
            // edtHeadText
            // 
            this.edtHeadText.AllowEditOnlyChecked = true;
            this.edtHeadText.Caption = "抬头文本";
            this.edtHeadText.Checked = false;
            this.edtHeadText.EditType = UserControl.EditTypes.String;
            this.edtHeadText.Location = new System.Drawing.Point(170, 43);
            this.edtHeadText.MaxLength = 1000;
            this.edtHeadText.Multiline = false;
            this.edtHeadText.Name = "edtHeadText";
            this.edtHeadText.PasswordChar = '\0';
            this.edtHeadText.ReadOnly = false;
            this.edtHeadText.ShowCheckBox = false;
            this.edtHeadText.Size = new System.Drawing.Size(194, 24);
            this.edtHeadText.TabIndex = 21;
            this.edtHeadText.TabNext = false;
            this.edtHeadText.Value = "";
            this.edtHeadText.WidthType = UserControl.WidthTypes.Normal;
            this.edtHeadText.XAlign = 231;
            // 
            // ucDateTimeStart
            // 
            this.ucDateTimeStart.Caption = "记账时间";
            this.ucDateTimeStart.Location = new System.Drawing.Point(12, 16);
            this.ucDateTimeStart.Name = "ucDateTimeStart";
            this.ucDateTimeStart.ShowType = UserControl.DateTimeTypes.Date;
            this.ucDateTimeStart.Size = new System.Drawing.Size(149, 21);
            this.ucDateTimeStart.TabIndex = 19;
            this.ucDateTimeStart.Value = new System.DateTime(2009, 2, 27, 0, 0, 0, 0);
            this.ucDateTimeStart.XAlign = 73;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.ultraGridMetrialDetial);
            this.groupBox3.Location = new System.Drawing.Point(0, 132);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(868, 298);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // ultraGridMetrialDetial
            // 
            this.ultraGridMetrialDetial.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ultraGridMetrialDetial.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridMetrialDetial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMetrialDetial.Location = new System.Drawing.Point(3, 17);
            this.ultraGridMetrialDetial.Name = "ultraGridMetrialDetial";
            this.ultraGridMetrialDetial.Size = new System.Drawing.Size(862, 278);
            this.ultraGridMetrialDetial.TabIndex = 0;
            this.ultraGridMetrialDetial.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMetrialDetial_InitializeLayout);
            // 
            // opsetCollectObject
            // 
            this.opsetCollectObject.ItemAppearance = appearance20;
            this.opsetCollectObject.Location = new System.Drawing.Point(0, 0);
            this.opsetCollectObject.Name = "opsetCollectObject";
            this.opsetCollectObject.Size = new System.Drawing.Size(96, 32);
            this.opsetCollectObject.TabIndex = 0;
            // 
            // chkAll
            // 
            this.chkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(12, 437);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(48, 16);
            this.chkAll.TabIndex = 172;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // radioButtonSaleImm
            // 
            this.radioButtonSaleImm.AutoSize = true;
            this.radioButtonSaleImm.Location = new System.Drawing.Point(381, 43);
            this.radioButtonSaleImm.Name = "radioButtonSaleImm";
            this.radioButtonSaleImm.Size = new System.Drawing.Size(47, 16);
            this.radioButtonSaleImm.TabIndex = 27;
            this.radioButtonSaleImm.Text = "即售";
            this.radioButtonSaleImm.UseVisualStyleBackColor = true;
            // 
            // radioButtonSaleDelay
            // 
            this.radioButtonSaleDelay.AutoSize = true;
            this.radioButtonSaleDelay.Checked = true;
            this.radioButtonSaleDelay.Location = new System.Drawing.Point(447, 43);
            this.radioButtonSaleDelay.Name = "radioButtonSaleDelay";
            this.radioButtonSaleDelay.Size = new System.Drawing.Size(59, 16);
            this.radioButtonSaleDelay.TabIndex = 28;
            this.radioButtonSaleDelay.TabStop = true;
            this.radioButtonSaleDelay.Text = "非即售";
            this.radioButtonSaleDelay.UseVisualStyleBackColor = true;
            // 
            // ucLabelComboxStorageIn
            // 
            this.ucLabelComboxStorageIn.AllowEditOnlyChecked = true;
            this.ucLabelComboxStorageIn.Caption = "收货地点";
            this.ucLabelComboxStorageIn.Checked = false;
            this.ucLabelComboxStorageIn.Location = new System.Drawing.Point(170, 16);
            this.ucLabelComboxStorageIn.Name = "ucLabelComboxStorageIn";
            this.ucLabelComboxStorageIn.SelectedIndex = -1;
            this.ucLabelComboxStorageIn.ShowCheckBox = false;
            this.ucLabelComboxStorageIn.Size = new System.Drawing.Size(194, 23);
            this.ucLabelComboxStorageIn.TabIndex = 29;
            this.ucLabelComboxStorageIn.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxStorageIn.XAlign = 231;
            // 
            // FSendMetrial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(868, 558);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FSendMetrial";
            this.Text = "发料";
            this.Load += new System.EventHandler(this.FSendMetrial_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.opsCheckFIFO)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.opsLoadMetrial)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMetrialDetial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opsetCollectObject)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private UserControl.UCLabelEdit edtMoPlanQty;
        private UserControl.UCLabelEdit edtitemDesc;
        private UserControl.UCLabelEdit edtMoCode;
        private System.Windows.Forms.GroupBox groupBox4;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet opsLoadMetrial;
        private Infragistics.Win.UltraWinEditors.UltraOptionSet opsetCollectObject;
        private UserControl.UCLabelEdit edtIQCNo;
        private UserControl.UCLabelEdit edtWorkSeat;
        private UserControl.UCLabelEdit edtMetrialLotNo;
        private System.Windows.Forms.GroupBox groupBox5;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet opsCheckFIFO;
        private UserControl.UCLabelEdit edtBufferDate;
        private System.Windows.Forms.Label label1;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMetrialDetial;
        private UserControl.UCDatetTime ucDateTimeStart;
        private UserControl.UCDatetTime ucDateTimeEnd;
        private UserControl.UCLabelEdit edtHeadText;
        private UserControl.UCButton btnClear;
        private UserControl.UCLabelEdit edtPrint;
        private UserControl.UCButton btnPrint;
        private UserControl.UCButton btnSendMetrial;
        private UserControl.UCLabelCombox ucLabelComboxPrinter;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.RadioButton radioButtonSaleImm;
        private System.Windows.Forms.RadioButton radioButtonSaleDelay;
        private UserControl.UCLabelCombox ucLabelComboxStorageIn;
    }
}