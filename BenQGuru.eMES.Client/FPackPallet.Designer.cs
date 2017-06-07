namespace BenQGuru.eMES.Client
{
    partial class FPackPallet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPackPallet));
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
            this.FPc = new System.Windows.Forms.Panel();
            this.txtLength = new UserControl.UCLabelEdit();
            this.chbPalletLength = new System.Windows.Forms.CheckBox();
            this.labelItemDesc = new System.Windows.Forms.Label();
            this.lblItemCodeDescV = new System.Windows.Forms.Label();
            this.txtSscode = new UserControl.UCLabelEdit();
            this.btnExchangePallet = new UserControl.UCButton();
            this.btnPrint = new UserControl.UCButton();
            this.chkPrint = new System.Windows.Forms.CheckBox();
            this.txtCapacity = new UserControl.UCLabelEdit();
            this.txtItemCode = new UserControl.UCLabelEdit();
            this.txtPalletNO = new UserControl.UCLabelEdit();
            this.ultraGridPalletForItemList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.objectCartonRCard = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.txtPalletCollect = new UserControl.UCLabelEdit();
            this.txtCollected = new UserControl.UCLabelEdit();
            this.uBtnExit = new UserControl.UCButton();
            this.txtSN = new UserControl.UCLabelEdit();
            this.ucMessage1 = new UserControl.UCMessage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboBusinessType = new UserControl.UCLabelCombox();
            this.ucLabelEditstackMessage = new UserControl.UCLabelEdit();
            this.btnGetStack = new System.Windows.Forms.Button();
            this.ucLabelEditStock = new UserControl.UCLabelEdit();
            this.ucLabelComboxCompany = new UserControl.UCLabelCombox();
            this.ucLabelComboxINVType = new UserControl.UCLabelCombox();
            this.checkBoxInINV = new System.Windows.Forms.CheckBox();
            this.checkRcard = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ultraGridPallet = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.FPc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridPalletForItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectCartonRCard)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridPallet)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // FPc
            // 
            this.FPc.BackColor = System.Drawing.Color.Gainsboro;
            this.FPc.Controls.Add(this.txtLength);
            this.FPc.Controls.Add(this.chbPalletLength);
            this.FPc.Controls.Add(this.labelItemDesc);
            this.FPc.Controls.Add(this.lblItemCodeDescV);
            this.FPc.Controls.Add(this.txtSscode);
            this.FPc.Controls.Add(this.btnExchangePallet);
            this.FPc.Controls.Add(this.btnPrint);
            this.FPc.Controls.Add(this.chkPrint);
            this.FPc.Controls.Add(this.txtCapacity);
            this.FPc.Controls.Add(this.txtItemCode);
            this.FPc.Controls.Add(this.txtPalletNO);
            this.FPc.Dock = System.Windows.Forms.DockStyle.Top;
            this.FPc.Location = new System.Drawing.Point(0, 0);
            this.FPc.Name = "FPc";
            this.FPc.Size = new System.Drawing.Size(798, 94);
            this.FPc.TabIndex = 0;
            // 
            // txtLength
            // 
            this.txtLength.AllowEditOnlyChecked = true;
            this.txtLength.AutoSelectAll = false;
            this.txtLength.AutoUpper = true;
            this.txtLength.Caption = "";
            this.txtLength.Checked = false;
            this.txtLength.EditType = UserControl.EditTypes.Integer;
            this.txtLength.Location = new System.Drawing.Point(371, 12);
            this.txtLength.MaxLength = 40;
            this.txtLength.Multiline = false;
            this.txtLength.Name = "txtLength";
            this.txtLength.PasswordChar = '\0';
            this.txtLength.ReadOnly = false;
            this.txtLength.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtLength.ShowCheckBox = false;
            this.txtLength.Size = new System.Drawing.Size(58, 24);
            this.txtLength.TabIndex = 235;
            this.txtLength.TabNext = true;
            this.txtLength.Value = "21";
            this.txtLength.WidthType = UserControl.WidthTypes.Tiny;
            this.txtLength.XAlign = 379;
            // 
            // chbPalletLength
            // 
            this.chbPalletLength.Checked = true;
            this.chbPalletLength.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbPalletLength.Location = new System.Drawing.Point(272, 12);
            this.chbPalletLength.Name = "chbPalletLength";
            this.chbPalletLength.Size = new System.Drawing.Size(96, 20);
            this.chbPalletLength.TabIndex = 234;
            this.chbPalletLength.Text = "栈板号长度";
            // 
            // labelItemDesc
            // 
            this.labelItemDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemDesc.Location = new System.Drawing.Point(3, 65);
            this.labelItemDesc.Name = "labelItemDesc";
            this.labelItemDesc.Size = new System.Drawing.Size(61, 22);
            this.labelItemDesc.TabIndex = 213;
            this.labelItemDesc.Text = "产品描述";
            this.labelItemDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblItemCodeDescV
            // 
            this.lblItemCodeDescV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCodeDescV.Location = new System.Drawing.Point(63, 62);
            this.lblItemCodeDescV.Name = "lblItemCodeDescV";
            this.lblItemCodeDescV.Size = new System.Drawing.Size(470, 31);
            this.lblItemCodeDescV.TabIndex = 212;
            this.lblItemCodeDescV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSscode
            // 
            this.txtSscode.AllowEditOnlyChecked = true;
            this.txtSscode.AutoSelectAll = false;
            this.txtSscode.AutoUpper = true;
            this.txtSscode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtSscode.Caption = "生产线";
            this.txtSscode.Checked = false;
            this.txtSscode.EditType = UserControl.EditTypes.String;
            this.txtSscode.Enabled = false;
            this.txtSscode.Location = new System.Drawing.Point(284, 40);
            this.txtSscode.MaxLength = 40;
            this.txtSscode.Multiline = false;
            this.txtSscode.Name = "txtSscode";
            this.txtSscode.PasswordChar = '\0';
            this.txtSscode.ReadOnly = false;
            this.txtSscode.ShowCheckBox = false;
            this.txtSscode.Size = new System.Drawing.Size(249, 22);
            this.txtSscode.TabIndex = 211;
            this.txtSscode.TabNext = true;
            this.txtSscode.Value = "";
            this.txtSscode.WidthType = UserControl.WidthTypes.Long;
            this.txtSscode.XAlign = 333;
            // 
            // btnExchangePallet
            // 
            this.btnExchangePallet.BackColor = System.Drawing.SystemColors.Control;
            this.btnExchangePallet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExchangePallet.BackgroundImage")));
            this.btnExchangePallet.ButtonType = UserControl.ButtonTypes.None;
            this.btnExchangePallet.Caption = "换栈板";
            this.btnExchangePallet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExchangePallet.Location = new System.Drawing.Point(622, 42);
            this.btnExchangePallet.Name = "btnExchangePallet";
            this.btnExchangePallet.Size = new System.Drawing.Size(88, 22);
            this.btnExchangePallet.TabIndex = 210;
            this.btnExchangePallet.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.ButtonType = UserControl.ButtonTypes.None;
            this.btnPrint.Caption = "打印";
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.Location = new System.Drawing.Point(622, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(88, 22);
            this.btnPrint.TabIndex = 209;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // chkPrint
            // 
            this.chkPrint.Location = new System.Drawing.Point(534, 12);
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.Size = new System.Drawing.Size(74, 22);
            this.chkPrint.TabIndex = 208;
            this.chkPrint.Text = "自动打印";
            this.chkPrint.Visible = false;
            // 
            // txtCapacity
            // 
            this.txtCapacity.AllowEditOnlyChecked = true;
            this.txtCapacity.AutoSelectAll = false;
            this.txtCapacity.AutoUpper = true;
            this.txtCapacity.BackColor = System.Drawing.Color.Gainsboro;
            this.txtCapacity.Caption = "栈板容量";
            this.txtCapacity.Checked = false;
            this.txtCapacity.EditType = UserControl.EditTypes.Integer;
            this.txtCapacity.Location = new System.Drawing.Point(409, 12);
            this.txtCapacity.MaxLength = 8;
            this.txtCapacity.Multiline = false;
            this.txtCapacity.Name = "txtCapacity";
            this.txtCapacity.PasswordChar = '\0';
            this.txtCapacity.ReadOnly = false;
            this.txtCapacity.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCapacity.ShowCheckBox = false;
            this.txtCapacity.Size = new System.Drawing.Size(111, 22);
            this.txtCapacity.TabIndex = 207;
            this.txtCapacity.TabNext = true;
            this.txtCapacity.Value = "";
            this.txtCapacity.Visible = false;
            this.txtCapacity.WidthType = UserControl.WidthTypes.Tiny;
            this.txtCapacity.XAlign = 470;
            // 
            // txtItemCode
            // 
            this.txtItemCode.AllowEditOnlyChecked = true;
            this.txtItemCode.AutoSelectAll = false;
            this.txtItemCode.AutoUpper = true;
            this.txtItemCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtItemCode.Caption = "产品";
            this.txtItemCode.Checked = false;
            this.txtItemCode.EditType = UserControl.EditTypes.String;
            this.txtItemCode.Enabled = false;
            this.txtItemCode.Location = new System.Drawing.Point(24, 40);
            this.txtItemCode.MaxLength = 40;
            this.txtItemCode.Multiline = false;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.PasswordChar = '\0';
            this.txtItemCode.ReadOnly = false;
            this.txtItemCode.ShowCheckBox = false;
            this.txtItemCode.Size = new System.Drawing.Size(237, 22);
            this.txtItemCode.TabIndex = 204;
            this.txtItemCode.TabNext = true;
            this.txtItemCode.TabStop = false;
            this.txtItemCode.Value = "";
            this.txtItemCode.WidthType = UserControl.WidthTypes.Long;
            this.txtItemCode.XAlign = 61;
            // 
            // txtPalletNO
            // 
            this.txtPalletNO.AllowEditOnlyChecked = true;
            this.txtPalletNO.AutoSelectAll = false;
            this.txtPalletNO.AutoUpper = true;
            this.txtPalletNO.BackColor = System.Drawing.Color.Gainsboro;
            this.txtPalletNO.Caption = "栈板号";
            this.txtPalletNO.Checked = false;
            this.txtPalletNO.EditType = UserControl.EditTypes.String;
            this.txtPalletNO.Location = new System.Drawing.Point(12, 12);
            this.txtPalletNO.MaxLength = 40;
            this.txtPalletNO.Multiline = false;
            this.txtPalletNO.Name = "txtPalletNO";
            this.txtPalletNO.PasswordChar = '\0';
            this.txtPalletNO.ReadOnly = false;
            this.txtPalletNO.ShowCheckBox = false;
            this.txtPalletNO.Size = new System.Drawing.Size(249, 22);
            this.txtPalletNO.TabIndex = 2;
            this.txtPalletNO.TabNext = true;
            this.txtPalletNO.Value = "";
            this.txtPalletNO.WidthType = UserControl.WidthTypes.Long;
            this.txtPalletNO.XAlign = 61;
            this.txtPalletNO.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPalletNO_TxtboxKeyPress);
            // 
            // ultraGridPalletForItemList
            // 
            this.ultraGridPalletForItemList.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridPalletForItemList.Location = new System.Drawing.Point(0, 94);
            this.ultraGridPalletForItemList.Name = "ultraGridPalletForItemList";
            this.ultraGridPalletForItemList.Size = new System.Drawing.Size(533, 180);
            this.ultraGridPalletForItemList.TabIndex = 1;
            this.ultraGridPalletForItemList.Text = "栈板中产品列表";
            this.ultraGridPalletForItemList.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMain_InitializeLayout);
            // 
            // objectCartonRCard
            // 
            this.objectCartonRCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            appearance3.FontData.BoldAsString = "False";
            this.objectCartonRCard.Appearance = appearance3;
            this.objectCartonRCard.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            valueListItem3.DataValue = "0";
            valueListItem3.DisplayText = "箱号";
            valueListItem4.DataValue = "1";
            valueListItem4.DisplayText = "产品序列号";
            this.objectCartonRCard.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem3,
            valueListItem4});
            this.objectCartonRCard.Location = new System.Drawing.Point(15, 21);
            this.objectCartonRCard.Name = "objectCartonRCard";
            this.objectCartonRCard.Size = new System.Drawing.Size(151, 16);
            this.objectCartonRCard.TabIndex = 203;
            this.objectCartonRCard.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.objectCartonRCard.ValueChanged += new System.EventHandler(this.opsetPackObject_ValueChanged);
            // 
            // txtPalletCollect
            // 
            this.txtPalletCollect.AllowEditOnlyChecked = true;
            this.txtPalletCollect.AutoSelectAll = false;
            this.txtPalletCollect.AutoUpper = true;
            this.txtPalletCollect.BackColor = System.Drawing.Color.Gainsboro;
            this.txtPalletCollect.Caption = "栈板已采集数量";
            this.txtPalletCollect.Checked = false;
            this.txtPalletCollect.EditType = UserControl.EditTypes.Integer;
            this.txtPalletCollect.Enabled = false;
            this.txtPalletCollect.Location = new System.Drawing.Point(15, 286);
            this.txtPalletCollect.MaxLength = 40;
            this.txtPalletCollect.Multiline = false;
            this.txtPalletCollect.Name = "txtPalletCollect";
            this.txtPalletCollect.PasswordChar = '\0';
            this.txtPalletCollect.ReadOnly = true;
            this.txtPalletCollect.ShowCheckBox = false;
            this.txtPalletCollect.Size = new System.Drawing.Size(197, 22);
            this.txtPalletCollect.TabIndex = 202;
            this.txtPalletCollect.TabNext = true;
            this.txtPalletCollect.TabStop = false;
            this.txtPalletCollect.Value = "";
            this.txtPalletCollect.WidthType = UserControl.WidthTypes.Small;
            this.txtPalletCollect.XAlign = 112;
            // 
            // txtCollected
            // 
            this.txtCollected.AllowEditOnlyChecked = true;
            this.txtCollected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCollected.AutoSelectAll = false;
            this.txtCollected.AutoUpper = true;
            this.txtCollected.BackColor = System.Drawing.Color.Transparent;
            this.txtCollected.Caption = "已采集数量";
            this.txtCollected.Checked = false;
            this.txtCollected.EditType = UserControl.EditTypes.Integer;
            this.txtCollected.Enabled = false;
            this.txtCollected.Location = new System.Drawing.Point(301, 43);
            this.txtCollected.MaxLength = 40;
            this.txtCollected.Multiline = false;
            this.txtCollected.Name = "txtCollected";
            this.txtCollected.PasswordChar = '\0';
            this.txtCollected.ReadOnly = true;
            this.txtCollected.ShowCheckBox = false;
            this.txtCollected.Size = new System.Drawing.Size(173, 22);
            this.txtCollected.TabIndex = 201;
            this.txtCollected.TabNext = true;
            this.txtCollected.TabStop = false;
            this.txtCollected.Value = "";
            this.txtCollected.WidthType = UserControl.WidthTypes.Small;
            this.txtCollected.XAlign = 374;
            // 
            // uBtnExit
            // 
            this.uBtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.uBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uBtnExit.BackgroundImage")));
            this.uBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.uBtnExit.Caption = "退出";
            this.uBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnExit.Location = new System.Drawing.Point(510, 43);
            this.uBtnExit.Name = "uBtnExit";
            this.uBtnExit.Size = new System.Drawing.Size(88, 22);
            this.uBtnExit.TabIndex = 177;
            // 
            // txtSN
            // 
            this.txtSN.AllowEditOnlyChecked = true;
            this.txtSN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSN.AutoSelectAll = false;
            this.txtSN.AutoUpper = true;
            this.txtSN.BackColor = System.Drawing.Color.Transparent;
            this.txtSN.Caption = "SN输入框";
            this.txtSN.Checked = false;
            this.txtSN.EditType = UserControl.EditTypes.String;
            this.txtSN.Location = new System.Drawing.Point(15, 43);
            this.txtSN.MaxLength = 40;
            this.txtSN.Multiline = false;
            this.txtSN.Name = "txtSN";
            this.txtSN.PasswordChar = '\0';
            this.txtSN.ReadOnly = false;
            this.txtSN.ShowCheckBox = false;
            this.txtSN.Size = new System.Drawing.Size(261, 22);
            this.txtSN.TabIndex = 176;
            this.txtSN.TabNext = false;
            this.txtSN.Value = "";
            this.txtSN.WidthType = UserControl.WidthTypes.Long;
            this.txtSN.XAlign = 76;
            this.txtSN.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRCard_TxtboxKeyPress);
            // 
            // ucMessage1
            // 
            this.ucMessage1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ucMessage1.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessage1.ButtonVisible = false;
            this.ucMessage1.Location = new System.Drawing.Point(0, 313);
            this.ucMessage1.Name = "ucMessage1";
            this.ucMessage1.Size = new System.Drawing.Size(365, 185);
            this.ucMessage1.TabIndex = 175;
            this.ucMessage1.WorkingErrorAdded += new UserControl.WorkingErrorAddedEventHandler(this.ucMessage1_WorkingErrorAdded);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboBusinessType);
            this.groupBox1.Controls.Add(this.ucLabelEditstackMessage);
            this.groupBox1.Controls.Add(this.btnGetStack);
            this.groupBox1.Controls.Add(this.ucLabelEditStock);
            this.groupBox1.Controls.Add(this.ucLabelComboxCompany);
            this.groupBox1.Controls.Add(this.ucLabelComboxINVType);
            this.groupBox1.Controls.Add(this.checkBoxInINV);
            this.groupBox1.Location = new System.Drawing.Point(539, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 214);
            this.groupBox1.TabIndex = 204;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // cboBusinessType
            // 
            this.cboBusinessType.AllowEditOnlyChecked = true;
            this.cboBusinessType.Caption = "业务类型";
            this.cboBusinessType.Checked = false;
            this.cboBusinessType.Location = new System.Drawing.Point(18, 161);
            this.cboBusinessType.Name = "cboBusinessType";
            this.cboBusinessType.SelectedIndex = -1;
            this.cboBusinessType.ShowCheckBox = false;
            this.cboBusinessType.Size = new System.Drawing.Size(194, 22);
            this.cboBusinessType.TabIndex = 207;
            this.cboBusinessType.WidthType = UserControl.WidthTypes.Normal;
            this.cboBusinessType.XAlign = 79;
            // 
            // ucLabelEditstackMessage
            // 
            this.ucLabelEditstackMessage.AllowEditOnlyChecked = true;
            this.ucLabelEditstackMessage.AutoSelectAll = false;
            this.ucLabelEditstackMessage.AutoUpper = true;
            this.ucLabelEditstackMessage.Caption = "垛位状况(已使用/容量)";
            this.ucLabelEditstackMessage.Checked = false;
            this.ucLabelEditstackMessage.EditType = UserControl.EditTypes.String;
            this.ucLabelEditstackMessage.Enabled = false;
            this.ucLabelEditstackMessage.Location = new System.Drawing.Point(23, 132);
            this.ucLabelEditstackMessage.MaxLength = 40;
            this.ucLabelEditstackMessage.Multiline = false;
            this.ucLabelEditstackMessage.Name = "ucLabelEditstackMessage";
            this.ucLabelEditstackMessage.PasswordChar = '\0';
            this.ucLabelEditstackMessage.ReadOnly = false;
            this.ucLabelEditstackMessage.ShowCheckBox = false;
            this.ucLabelEditstackMessage.Size = new System.Drawing.Size(189, 20);
            this.ucLabelEditstackMessage.TabIndex = 205;
            this.ucLabelEditstackMessage.TabNext = true;
            this.ucLabelEditstackMessage.Value = "";
            this.ucLabelEditstackMessage.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditstackMessage.XAlign = 162;
            // 
            // btnGetStack
            // 
            this.btnGetStack.Location = new System.Drawing.Point(216, 76);
            this.btnGetStack.Name = "btnGetStack";
            this.btnGetStack.Size = new System.Drawing.Size(34, 23);
            this.btnGetStack.TabIndex = 5;
            this.btnGetStack.Text = "...";
            this.btnGetStack.UseVisualStyleBackColor = true;
            this.btnGetStack.Click += new System.EventHandler(this.btnGetStack_Click);
            // 
            // ucLabelEditStock
            // 
            this.ucLabelEditStock.AllowEditOnlyChecked = true;
            this.ucLabelEditStock.AutoSelectAll = false;
            this.ucLabelEditStock.AutoUpper = true;
            this.ucLabelEditStock.Caption = "垛位";
            this.ucLabelEditStock.Checked = false;
            this.ucLabelEditStock.EditType = UserControl.EditTypes.String;
            this.ucLabelEditStock.Location = new System.Drawing.Point(42, 76);
            this.ucLabelEditStock.MaxLength = 40;
            this.ucLabelEditStock.Multiline = false;
            this.ucLabelEditStock.Name = "ucLabelEditStock";
            this.ucLabelEditStock.PasswordChar = '\0';
            this.ucLabelEditStock.ReadOnly = true;
            this.ucLabelEditStock.ShowCheckBox = false;
            this.ucLabelEditStock.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditStock.TabIndex = 4;
            this.ucLabelEditStock.TabNext = true;
            this.ucLabelEditStock.Value = "";
            this.ucLabelEditStock.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditStock.XAlign = 79;
            // 
            // ucLabelComboxCompany
            // 
            this.ucLabelComboxCompany.AllowEditOnlyChecked = true;
            this.ucLabelComboxCompany.Caption = "公司";
            this.ucLabelComboxCompany.Checked = false;
            this.ucLabelComboxCompany.Location = new System.Drawing.Point(42, 106);
            this.ucLabelComboxCompany.Name = "ucLabelComboxCompany";
            this.ucLabelComboxCompany.SelectedIndex = -1;
            this.ucLabelComboxCompany.ShowCheckBox = false;
            this.ucLabelComboxCompany.Size = new System.Drawing.Size(170, 20);
            this.ucLabelComboxCompany.TabIndex = 3;
            this.ucLabelComboxCompany.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxCompany.XAlign = 79;
            // 
            // ucLabelComboxINVType
            // 
            this.ucLabelComboxINVType.AllowEditOnlyChecked = true;
            this.ucLabelComboxINVType.Caption = "库别";
            this.ucLabelComboxINVType.Checked = false;
            this.ucLabelComboxINVType.Location = new System.Drawing.Point(42, 50);
            this.ucLabelComboxINVType.Name = "ucLabelComboxINVType";
            this.ucLabelComboxINVType.SelectedIndex = -1;
            this.ucLabelComboxINVType.ShowCheckBox = false;
            this.ucLabelComboxINVType.Size = new System.Drawing.Size(170, 20);
            this.ucLabelComboxINVType.TabIndex = 2;
            this.ucLabelComboxINVType.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxINVType.XAlign = 79;
            // 
            // checkBoxInINV
            // 
            this.checkBoxInINV.AutoSize = true;
            this.checkBoxInINV.Location = new System.Drawing.Point(6, 20);
            this.checkBoxInINV.Name = "checkBoxInINV";
            this.checkBoxInINV.Size = new System.Drawing.Size(48, 16);
            this.checkBoxInINV.TabIndex = 0;
            this.checkBoxInINV.Text = "入库";
            this.checkBoxInINV.UseVisualStyleBackColor = true;
            this.checkBoxInINV.CheckedChanged += new System.EventHandler(this.checkBoxInINV_CheckedChanged);
            // 
            // checkRcard
            // 
            this.checkRcard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkRcard.Location = new System.Drawing.Point(172, 17);
            this.checkRcard.Name = "checkRcard";
            this.checkRcard.Size = new System.Drawing.Size(89, 20);
            this.checkRcard.TabIndex = 227;
            this.checkRcard.Text = "序号比对";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ultraGridPallet);
            this.groupBox2.Location = new System.Drawing.Point(371, 307);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(424, 192);
            this.groupBox2.TabIndex = 228;
            this.groupBox2.TabStop = false;
            // 
            // ultraGridPallet
            // 
            this.ultraGridPallet.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridPallet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridPallet.Location = new System.Drawing.Point(3, 17);
            this.ultraGridPallet.Name = "ultraGridPallet";
            this.ultraGridPallet.Size = new System.Drawing.Size(418, 172);
            this.ultraGridPallet.TabIndex = 0;
            this.ultraGridPallet.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridPallet_InitializeLayout);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSN);
            this.groupBox3.Controls.Add(this.uBtnExit);
            this.groupBox3.Controls.Add(this.txtCollected);
            this.groupBox3.Controls.Add(this.checkRcard);
            this.groupBox3.Controls.Add(this.objectCartonRCard);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 506);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(798, 75);
            this.groupBox3.TabIndex = 229;
            this.groupBox3.TabStop = false;
            // 
            // FPackPallet
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(798, 581);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.ucMessage1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtPalletCollect);
            this.Controls.Add(this.ultraGridPalletForItemList);
            this.Controls.Add(this.FPc);
            this.Name = "FPackPallet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pallet包装采集";
            this.Load += new System.EventHandler(this.FPackPallet_Load);
            this.FPc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridPalletForItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectCartonRCard)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridPallet)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel FPc;
        private UserControl.UCLabelEdit txtPalletNO;
        private UserControl.UCLabelEdit txtItemCode;
        private UserControl.UCLabelEdit txtCapacity;
        private System.Windows.Forms.CheckBox chkPrint;
        private UserControl.UCButton btnExchangePallet;
        private UserControl.UCButton btnPrint;
        private UserControl.UCLabelEdit txtSscode;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridPalletForItemList;
        private UserControl.UCMessage ucMessage1;
        private UserControl.UCLabelEdit txtSN;
        private UserControl.UCButton uBtnExit;
        private UserControl.UCLabelEdit txtCollected;
        private UserControl.UCLabelEdit txtPalletCollect;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet objectCartonRCard;
        private System.Windows.Forms.Label lblItemCodeDescV;
        private System.Windows.Forms.Label labelItemDesc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxInINV;
        private UserControl.UCLabelCombox ucLabelComboxINVType;
        private System.Windows.Forms.Button btnGetStack;
        private UserControl.UCLabelEdit ucLabelEditStock;
        private UserControl.UCLabelCombox ucLabelComboxCompany;
        private UserControl.UCLabelEdit ucLabelEditstackMessage;
        private System.Windows.Forms.CheckBox checkRcard;
        private System.Windows.Forms.GroupBox groupBox2;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridPallet;
        private UserControl.UCLabelCombox cboBusinessType;
        private UserControl.UCLabelEdit txtLength;
        private System.Windows.Forms.CheckBox chbPalletLength;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}