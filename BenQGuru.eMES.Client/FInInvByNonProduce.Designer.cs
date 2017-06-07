namespace BenQGuru.eMES.Client
{
    partial class FInInvByNonProduce
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
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FInInvByNonProduce));
            this.receiptsInfo = new System.Windows.Forms.GroupBox();
            this.txtRelationDoc = new UserControl.UCLabelEdit();
            this.cmdItemSelect = new System.Windows.Forms.Button();
            this.txtItemDescV = new UserControl.UCLabelEdit();
            this.txtMemo = new UserControl.UCLabelEdit();
            this.txtPlanQty = new UserControl.UCLabelEdit();
            this.txtDeliverUser = new UserControl.UCLabelEdit();
            this.cboBusinssCode = new UserControl.UCLabelCombox();
            this.txtItemCode = new UserControl.UCLabelEdit();
            this.txtTransCode = new UserControl.UCLabelEdit();
            this.inInvInfo = new System.Windows.Forms.GroupBox();
            this.ucLabelEditstackMessage = new UserControl.UCLabelEdit();
            this.btnGetStack = new System.Windows.Forms.Button();
            this.ucLabelEditStock = new UserControl.UCLabelEdit();
            this.ucLabelComboxCompany = new UserControl.UCLabelCombox();
            this.ucLabelComboxINVType = new UserControl.UCLabelCombox();
            this.inputSN = new System.Windows.Forms.GroupBox();
            this.txtLength = new UserControl.UCLabelEdit();
            this.chbRcardLength = new System.Windows.Forms.CheckBox();
            this.checkItemCode = new System.Windows.Forms.CheckBox();
            this.ultraOptionSetAddDelete = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.cmdClear = new UserControl.UCButton();
            this.txtCartonOrRCard = new UserControl.UCLabelEdit();
            this.txtPalletCode = new UserControl.UCLabelEdit();
            this.ucLabelEditQty = new UserControl.UCLabelEdit();
            this.gridRcard = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.cmdInINV = new UserControl.UCButton();
            this.cmdCancel = new UserControl.UCButton();
            this.receiptsInfo.SuspendLayout();
            this.inInvInfo.SuspendLayout();
            this.inputSN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetAddDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRcard)).BeginInit();
            this.SuspendLayout();
            // 
            // receiptsInfo
            // 
            this.receiptsInfo.Controls.Add(this.txtRelationDoc);
            this.receiptsInfo.Controls.Add(this.cmdItemSelect);
            this.receiptsInfo.Controls.Add(this.txtItemDescV);
            this.receiptsInfo.Controls.Add(this.txtMemo);
            this.receiptsInfo.Controls.Add(this.txtPlanQty);
            this.receiptsInfo.Controls.Add(this.txtDeliverUser);
            this.receiptsInfo.Controls.Add(this.cboBusinssCode);
            this.receiptsInfo.Controls.Add(this.txtItemCode);
            this.receiptsInfo.Controls.Add(this.txtTransCode);
            this.receiptsInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.receiptsInfo.Location = new System.Drawing.Point(0, 0);
            this.receiptsInfo.Name = "receiptsInfo";
            this.receiptsInfo.Size = new System.Drawing.Size(792, 137);
            this.receiptsInfo.TabIndex = 0;
            this.receiptsInfo.TabStop = false;
            this.receiptsInfo.Text = "单据信息";
            // 
            // txtRelationDoc
            // 
            this.txtRelationDoc.AllowEditOnlyChecked = true;
            this.txtRelationDoc.AutoSelectAll = false;
            this.txtRelationDoc.AutoUpper = true;
            this.txtRelationDoc.Caption = "关联单据";
            this.txtRelationDoc.Checked = false;
            this.txtRelationDoc.EditType = UserControl.EditTypes.String;
            this.txtRelationDoc.Location = new System.Drawing.Point(33, 108);
            this.txtRelationDoc.MaxLength = 40;
            this.txtRelationDoc.Multiline = false;
            this.txtRelationDoc.Name = "txtRelationDoc";
            this.txtRelationDoc.PasswordChar = '\0';
            this.txtRelationDoc.ReadOnly = false;
            this.txtRelationDoc.ShowCheckBox = false;
            this.txtRelationDoc.Size = new System.Drawing.Size(194, 26);
            this.txtRelationDoc.TabIndex = 8;
            this.txtRelationDoc.TabNext = true;
            this.txtRelationDoc.Value = "";
            this.txtRelationDoc.WidthType = UserControl.WidthTypes.Normal;
            this.txtRelationDoc.XAlign = 94;
            // 
            // cmdItemSelect
            // 
            this.cmdItemSelect.Location = new System.Drawing.Point(233, 51);
            this.cmdItemSelect.Name = "cmdItemSelect";
            this.cmdItemSelect.Size = new System.Drawing.Size(34, 25);
            this.cmdItemSelect.TabIndex = 4;
            this.cmdItemSelect.Text = "...";
            this.cmdItemSelect.UseVisualStyleBackColor = true;
            this.cmdItemSelect.Click += new System.EventHandler(this.cmdItemSelect_Click);
            // 
            // txtItemDescV
            // 
            this.txtItemDescV.AllowEditOnlyChecked = true;
            this.txtItemDescV.AutoSelectAll = false;
            this.txtItemDescV.AutoUpper = true;
            this.txtItemDescV.Caption = "";
            this.txtItemDescV.Checked = false;
            this.txtItemDescV.EditType = UserControl.EditTypes.String;
            this.txtItemDescV.Location = new System.Drawing.Point(335, 50);
            this.txtItemDescV.MaxLength = 100;
            this.txtItemDescV.Multiline = false;
            this.txtItemDescV.Name = "txtItemDescV";
            this.txtItemDescV.PasswordChar = '\0';
            this.txtItemDescV.ReadOnly = true;
            this.txtItemDescV.ShowCheckBox = false;
            this.txtItemDescV.Size = new System.Drawing.Size(408, 26);
            this.txtItemDescV.TabIndex = 5;
            this.txtItemDescV.TabNext = true;
            this.txtItemDescV.Value = "";
            this.txtItemDescV.WidthType = UserControl.WidthTypes.TooLong;
            this.txtItemDescV.XAlign = 343;
            // 
            // txtMemo
            // 
            this.txtMemo.AllowEditOnlyChecked = true;
            this.txtMemo.AutoSelectAll = false;
            this.txtMemo.AutoUpper = true;
            this.txtMemo.Caption = "备注";
            this.txtMemo.Checked = false;
            this.txtMemo.EditType = UserControl.EditTypes.String;
            this.txtMemo.Location = new System.Drawing.Point(306, 80);
            this.txtMemo.MaxLength = 100;
            this.txtMemo.Multiline = false;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.PasswordChar = '\0';
            this.txtMemo.ReadOnly = false;
            this.txtMemo.ShowCheckBox = false;
            this.txtMemo.Size = new System.Drawing.Size(437, 26);
            this.txtMemo.TabIndex = 7;
            this.txtMemo.TabNext = true;
            this.txtMemo.Value = "";
            this.txtMemo.WidthType = UserControl.WidthTypes.TooLong;
            this.txtMemo.XAlign = 343;
            // 
            // txtPlanQty
            // 
            this.txtPlanQty.AllowEditOnlyChecked = true;
            this.txtPlanQty.AutoSelectAll = false;
            this.txtPlanQty.AutoUpper = true;
            this.txtPlanQty.Caption = "计划入库数量";
            this.txtPlanQty.Checked = false;
            this.txtPlanQty.EditType = UserControl.EditTypes.Integer;
            this.txtPlanQty.Location = new System.Drawing.Point(9, 80);
            this.txtPlanQty.MaxLength = 40;
            this.txtPlanQty.Multiline = false;
            this.txtPlanQty.Name = "txtPlanQty";
            this.txtPlanQty.PasswordChar = '\0';
            this.txtPlanQty.ReadOnly = false;
            this.txtPlanQty.ShowCheckBox = false;
            this.txtPlanQty.Size = new System.Drawing.Size(218, 26);
            this.txtPlanQty.TabIndex = 6;
            this.txtPlanQty.TabNext = true;
            this.txtPlanQty.Value = "";
            this.txtPlanQty.WidthType = UserControl.WidthTypes.Normal;
            this.txtPlanQty.XAlign = 94;
            // 
            // txtDeliverUser
            // 
            this.txtDeliverUser.AllowEditOnlyChecked = true;
            this.txtDeliverUser.AutoSelectAll = false;
            this.txtDeliverUser.AutoUpper = true;
            this.txtDeliverUser.Caption = "送货人员";
            this.txtDeliverUser.Checked = false;
            this.txtDeliverUser.EditType = UserControl.EditTypes.String;
            this.txtDeliverUser.Location = new System.Drawing.Point(549, 22);
            this.txtDeliverUser.MaxLength = 40;
            this.txtDeliverUser.Multiline = false;
            this.txtDeliverUser.Name = "txtDeliverUser";
            this.txtDeliverUser.PasswordChar = '\0';
            this.txtDeliverUser.ReadOnly = false;
            this.txtDeliverUser.ShowCheckBox = false;
            this.txtDeliverUser.Size = new System.Drawing.Size(194, 26);
            this.txtDeliverUser.TabIndex = 2;
            this.txtDeliverUser.TabNext = true;
            this.txtDeliverUser.Value = "";
            this.txtDeliverUser.WidthType = UserControl.WidthTypes.Normal;
            this.txtDeliverUser.XAlign = 610;
            // 
            // cboBusinssCode
            // 
            this.cboBusinssCode.AllowEditOnlyChecked = true;
            this.cboBusinssCode.Caption = "业务类型";
            this.cboBusinssCode.Checked = false;
            this.cboBusinssCode.Location = new System.Drawing.Point(282, 22);
            this.cboBusinssCode.Name = "cboBusinssCode";
            this.cboBusinssCode.SelectedIndex = -1;
            this.cboBusinssCode.ShowCheckBox = false;
            this.cboBusinssCode.Size = new System.Drawing.Size(194, 22);
            this.cboBusinssCode.TabIndex = 1;
            this.cboBusinssCode.WidthType = UserControl.WidthTypes.Normal;
            this.cboBusinssCode.XAlign = 343;
            // 
            // txtItemCode
            // 
            this.txtItemCode.AllowEditOnlyChecked = true;
            this.txtItemCode.AutoSelectAll = false;
            this.txtItemCode.AutoUpper = true;
            this.txtItemCode.Caption = "产品代码";
            this.txtItemCode.Checked = false;
            this.txtItemCode.EditType = UserControl.EditTypes.String;
            this.txtItemCode.Location = new System.Drawing.Point(33, 51);
            this.txtItemCode.MaxLength = 40;
            this.txtItemCode.Multiline = false;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.PasswordChar = '\0';
            this.txtItemCode.ReadOnly = false;
            this.txtItemCode.ShowCheckBox = false;
            this.txtItemCode.Size = new System.Drawing.Size(194, 26);
            this.txtItemCode.TabIndex = 3;
            this.txtItemCode.TabNext = true;
            this.txtItemCode.Value = "";
            this.txtItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtItemCode.XAlign = 94;
            this.txtItemCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtItemCode_TxtboxKeyPress);
            // 
            // txtTransCode
            // 
            this.txtTransCode.AllowEditOnlyChecked = true;
            this.txtTransCode.AutoSelectAll = false;
            this.txtTransCode.AutoUpper = true;
            this.txtTransCode.Caption = "单据号码";
            this.txtTransCode.Checked = false;
            this.txtTransCode.EditType = UserControl.EditTypes.String;
            this.txtTransCode.Location = new System.Drawing.Point(33, 22);
            this.txtTransCode.MaxLength = 40;
            this.txtTransCode.Multiline = false;
            this.txtTransCode.Name = "txtTransCode";
            this.txtTransCode.PasswordChar = '\0';
            this.txtTransCode.ReadOnly = false;
            this.txtTransCode.ShowCheckBox = false;
            this.txtTransCode.Size = new System.Drawing.Size(194, 26);
            this.txtTransCode.TabIndex = 0;
            this.txtTransCode.TabNext = true;
            this.txtTransCode.Value = "";
            this.txtTransCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtTransCode.XAlign = 94;
            // 
            // inInvInfo
            // 
            this.inInvInfo.Controls.Add(this.ucLabelEditstackMessage);
            this.inInvInfo.Controls.Add(this.btnGetStack);
            this.inInvInfo.Controls.Add(this.ucLabelEditStock);
            this.inInvInfo.Controls.Add(this.ucLabelComboxCompany);
            this.inInvInfo.Controls.Add(this.ucLabelComboxINVType);
            this.inInvInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.inInvInfo.Location = new System.Drawing.Point(0, 137);
            this.inInvInfo.Name = "inInvInfo";
            this.inInvInfo.Size = new System.Drawing.Size(792, 102);
            this.inInvInfo.TabIndex = 1;
            this.inInvInfo.TabStop = false;
            this.inInvInfo.Text = "入库信息";
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
            this.ucLabelEditstackMessage.Location = new System.Drawing.Point(306, 57);
            this.ucLabelEditstackMessage.MaxLength = 40;
            this.ucLabelEditstackMessage.Multiline = false;
            this.ucLabelEditstackMessage.Name = "ucLabelEditstackMessage";
            this.ucLabelEditstackMessage.PasswordChar = '\0';
            this.ucLabelEditstackMessage.ReadOnly = false;
            this.ucLabelEditstackMessage.ShowCheckBox = false;
            this.ucLabelEditstackMessage.Size = new System.Drawing.Size(189, 22);
            this.ucLabelEditstackMessage.TabIndex = 14;
            this.ucLabelEditstackMessage.TabNext = true;
            this.ucLabelEditstackMessage.Value = "";
            this.ucLabelEditstackMessage.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditstackMessage.XAlign = 445;
            // 
            // btnGetStack
            // 
            this.btnGetStack.Location = new System.Drawing.Point(482, 29);
            this.btnGetStack.Name = "btnGetStack";
            this.btnGetStack.Size = new System.Drawing.Size(34, 25);
            this.btnGetStack.TabIndex = 12;
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
            this.ucLabelEditStock.Location = new System.Drawing.Point(306, 29);
            this.ucLabelEditStock.MaxLength = 40;
            this.ucLabelEditStock.Multiline = false;
            this.ucLabelEditStock.Name = "ucLabelEditStock";
            this.ucLabelEditStock.PasswordChar = '\0';
            this.ucLabelEditStock.ReadOnly = true;
            this.ucLabelEditStock.ShowCheckBox = false;
            this.ucLabelEditStock.Size = new System.Drawing.Size(170, 26);
            this.ucLabelEditStock.TabIndex = 11;
            this.ucLabelEditStock.TabNext = true;
            this.ucLabelEditStock.Value = "";
            this.ucLabelEditStock.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditStock.XAlign = 343;
            // 
            // ucLabelComboxCompany
            // 
            this.ucLabelComboxCompany.AllowEditOnlyChecked = true;
            this.ucLabelComboxCompany.Caption = "公司";
            this.ucLabelComboxCompany.Checked = false;
            this.ucLabelComboxCompany.Location = new System.Drawing.Point(57, 57);
            this.ucLabelComboxCompany.Name = "ucLabelComboxCompany";
            this.ucLabelComboxCompany.SelectedIndex = -1;
            this.ucLabelComboxCompany.ShowCheckBox = false;
            this.ucLabelComboxCompany.Size = new System.Drawing.Size(170, 22);
            this.ucLabelComboxCompany.TabIndex = 13;
            this.ucLabelComboxCompany.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxCompany.XAlign = 94;
            // 
            // ucLabelComboxINVType
            // 
            this.ucLabelComboxINVType.AllowEditOnlyChecked = true;
            this.ucLabelComboxINVType.Caption = "库别";
            this.ucLabelComboxINVType.Checked = false;
            this.ucLabelComboxINVType.Location = new System.Drawing.Point(57, 29);
            this.ucLabelComboxINVType.Name = "ucLabelComboxINVType";
            this.ucLabelComboxINVType.SelectedIndex = -1;
            this.ucLabelComboxINVType.ShowCheckBox = false;
            this.ucLabelComboxINVType.Size = new System.Drawing.Size(170, 22);
            this.ucLabelComboxINVType.TabIndex = 10;
            this.ucLabelComboxINVType.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxINVType.XAlign = 94;
            // 
            // inputSN
            // 
            this.inputSN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.inputSN.Controls.Add(this.txtLength);
            this.inputSN.Controls.Add(this.chbRcardLength);
            this.inputSN.Controls.Add(this.checkItemCode);
            this.inputSN.Controls.Add(this.ultraOptionSetAddDelete);
            this.inputSN.Controls.Add(this.cmdClear);
            this.inputSN.Controls.Add(this.txtCartonOrRCard);
            this.inputSN.Controls.Add(this.txtPalletCode);
            this.inputSN.Controls.Add(this.ucLabelEditQty);
            this.inputSN.Controls.Add(this.gridRcard);
            this.inputSN.Location = new System.Drawing.Point(0, 249);
            this.inputSN.Name = "inputSN";
            this.inputSN.Size = new System.Drawing.Size(792, 315);
            this.inputSN.TabIndex = 2;
            this.inputSN.TabStop = false;
            this.inputSN.Text = "序列号输入";
            // 
            // txtLength
            // 
            this.txtLength.AllowEditOnlyChecked = true;
            this.txtLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLength.AutoSelectAll = false;
            this.txtLength.AutoUpper = true;
            this.txtLength.Caption = "";
            this.txtLength.Checked = false;
            this.txtLength.EditType = UserControl.EditTypes.Integer;
            this.txtLength.Location = new System.Drawing.Point(319, 276);
            this.txtLength.MaxLength = 40;
            this.txtLength.Multiline = false;
            this.txtLength.Name = "txtLength";
            this.txtLength.PasswordChar = '\0';
            this.txtLength.ReadOnly = false;
            this.txtLength.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtLength.ShowCheckBox = false;
            this.txtLength.Size = new System.Drawing.Size(58, 26);
            this.txtLength.TabIndex = 233;
            this.txtLength.TabNext = true;
            this.txtLength.Value = "23";
            this.txtLength.WidthType = UserControl.WidthTypes.Tiny;
            this.txtLength.XAlign = 327;
            // 
            // chbRcardLength
            // 
            this.chbRcardLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbRcardLength.Checked = true;
            this.chbRcardLength.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbRcardLength.Location = new System.Drawing.Point(247, 280);
            this.chbRcardLength.Name = "chbRcardLength";
            this.chbRcardLength.Size = new System.Drawing.Size(75, 20);
            this.chbRcardLength.TabIndex = 231;
            this.chbRcardLength.Text = "序号长度";
            // 
            // checkItemCode
            // 
            this.checkItemCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkItemCode.Checked = true;
            this.checkItemCode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkItemCode.Location = new System.Drawing.Point(397, 280);
            this.checkItemCode.Name = "checkItemCode";
            this.checkItemCode.Size = new System.Drawing.Size(104, 20);
            this.checkItemCode.TabIndex = 230;
            this.checkItemCode.Text = "是否检查料号";
            // 
            // ultraOptionSetAddDelete
            // 
            this.ultraOptionSetAddDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            appearance3.FontData.BoldAsString = "False";
            this.ultraOptionSetAddDelete.Appearance = appearance3;
            this.ultraOptionSetAddDelete.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            valueListItem3.DataValue = "0";
            valueListItem3.DisplayText = "新增";
            valueListItem4.DataValue = "2";
            valueListItem4.DisplayText = "移除";
            this.ultraOptionSetAddDelete.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem3,
            valueListItem4});
            this.ultraOptionSetAddDelete.Location = new System.Drawing.Point(512, 282);
            this.ultraOptionSetAddDelete.Name = "ultraOptionSetAddDelete";
            this.ultraOptionSetAddDelete.Size = new System.Drawing.Size(114, 17);
            this.ultraOptionSetAddDelete.TabIndex = 16;
            this.ultraOptionSetAddDelete.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.ultraOptionSetAddDelete.ValueChanged += new System.EventHandler(this.ultraOptionSetAddDelete_ValueChanged);
            // 
            // cmdClear
            // 
            this.cmdClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdClear.BackColor = System.Drawing.SystemColors.Control;
            this.cmdClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdClear.BackgroundImage")));
            this.cmdClear.ButtonType = UserControl.ButtonTypes.None;
            this.cmdClear.Caption = "清空";
            this.cmdClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdClear.Location = new System.Drawing.Point(636, 278);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(88, 24);
            this.cmdClear.TabIndex = 17;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // txtCartonOrRCard
            // 
            this.txtCartonOrRCard.AllowEditOnlyChecked = true;
            this.txtCartonOrRCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCartonOrRCard.AutoSelectAll = false;
            this.txtCartonOrRCard.AutoUpper = true;
            this.txtCartonOrRCard.Caption = "箱号/产品序列号";
            this.txtCartonOrRCard.Checked = true;
            this.txtCartonOrRCard.EditType = UserControl.EditTypes.String;
            this.txtCartonOrRCard.Location = new System.Drawing.Point(5, 276);
            this.txtCartonOrRCard.MaxLength = 40;
            this.txtCartonOrRCard.Multiline = false;
            this.txtCartonOrRCard.Name = "txtCartonOrRCard";
            this.txtCartonOrRCard.PasswordChar = '\0';
            this.txtCartonOrRCard.ReadOnly = false;
            this.txtCartonOrRCard.ShowCheckBox = false;
            this.txtCartonOrRCard.Size = new System.Drawing.Size(236, 26);
            this.txtCartonOrRCard.TabIndex = 15;
            this.txtCartonOrRCard.TabNext = true;
            this.txtCartonOrRCard.Value = "";
            this.txtCartonOrRCard.WidthType = UserControl.WidthTypes.Normal;
            this.txtCartonOrRCard.XAlign = 108;
            this.txtCartonOrRCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRCard_TxtboxKeyPress);
            // 
            // txtPalletCode
            // 
            this.txtPalletCode.AllowEditOnlyChecked = true;
            this.txtPalletCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPalletCode.AutoSelectAll = false;
            this.txtPalletCode.AutoUpper = true;
            this.txtPalletCode.Caption = "归属栈板";
            this.txtPalletCode.Checked = false;
            this.txtPalletCode.EditType = UserControl.EditTypes.String;
            this.txtPalletCode.Location = new System.Drawing.Point(31, 244);
            this.txtPalletCode.MaxLength = 40;
            this.txtPalletCode.Multiline = false;
            this.txtPalletCode.Name = "txtPalletCode";
            this.txtPalletCode.PasswordChar = '\0';
            this.txtPalletCode.ReadOnly = false;
            this.txtPalletCode.ShowCheckBox = true;
            this.txtPalletCode.Size = new System.Drawing.Size(210, 26);
            this.txtPalletCode.TabIndex = 14;
            this.txtPalletCode.TabNext = true;
            this.txtPalletCode.Value = "";
            this.txtPalletCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtPalletCode.XAlign = 108;
            // 
            // ucLabelEditQty
            // 
            this.ucLabelEditQty.AllowEditOnlyChecked = true;
            this.ucLabelEditQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLabelEditQty.AutoSelectAll = false;
            this.ucLabelEditQty.AutoUpper = true;
            this.ucLabelEditQty.Caption = "数量";
            this.ucLabelEditQty.Checked = false;
            this.ucLabelEditQty.EditType = UserControl.EditTypes.String;
            this.ucLabelEditQty.Location = new System.Drawing.Point(693, 236);
            this.ucLabelEditQty.MaxLength = 40;
            this.ucLabelEditQty.Multiline = false;
            this.ucLabelEditQty.Name = "ucLabelEditQty";
            this.ucLabelEditQty.PasswordChar = '\0';
            this.ucLabelEditQty.ReadOnly = true;
            this.ucLabelEditQty.ShowCheckBox = false;
            this.ucLabelEditQty.Size = new System.Drawing.Size(87, 26);
            this.ucLabelEditQty.TabIndex = 206;
            this.ucLabelEditQty.TabNext = true;
            this.ucLabelEditQty.Value = "";
            this.ucLabelEditQty.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditQty.XAlign = 730;
            // 
            // gridRcard
            // 
            this.gridRcard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRcard.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridRcard.Location = new System.Drawing.Point(3, 18);
            this.gridRcard.Name = "gridRcard";
            this.gridRcard.Size = new System.Drawing.Size(783, 211);
            this.gridRcard.TabIndex = 20;
            this.gridRcard.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridRcard_InitializeLayout);
            // 
            // cmdInINV
            // 
            this.cmdInINV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdInINV.BackColor = System.Drawing.SystemColors.Control;
            this.cmdInINV.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdInINV.BackgroundImage")));
            this.cmdInINV.ButtonType = UserControl.ButtonTypes.None;
            this.cmdInINV.Caption = "入库";
            this.cmdInINV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdInINV.Location = new System.Drawing.Point(263, 573);
            this.cmdInINV.Name = "cmdInINV";
            this.cmdInINV.Size = new System.Drawing.Size(88, 24);
            this.cmdInINV.TabIndex = 18;
            this.cmdInINV.Click += new System.EventHandler(this.cmdInINV_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCancel.BackColor = System.Drawing.SystemColors.Control;
            this.cmdCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdCancel.BackgroundImage")));
            this.cmdCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.cmdCancel.Caption = "取消";
            this.cmdCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdCancel.Location = new System.Drawing.Point(369, 573);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(88, 24);
            this.cmdCancel.TabIndex = 19;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // FInInvByNonProduce
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(792, 613);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdInINV);
            this.Controls.Add(this.inputSN);
            this.Controls.Add(this.inInvInfo);
            this.Controls.Add(this.receiptsInfo);
            this.Name = "FInInvByNonProduce";
            this.Text = "非生产性入库";
            this.Load += new System.EventHandler(this.FInInvByNonProduce_Load);
            this.receiptsInfo.ResumeLayout(false);
            this.inInvInfo.ResumeLayout(false);
            this.inputSN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetAddDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRcard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox receiptsInfo;
        private System.Windows.Forms.GroupBox inInvInfo;
        private System.Windows.Forms.GroupBox inputSN;
        private UserControl.UCButton cmdInINV;
        private UserControl.UCButton cmdClear;
        private UserControl.UCLabelEdit txtDeliverUser;
        private UserControl.UCLabelCombox cboBusinssCode;
        private UserControl.UCLabelEdit txtItemCode;
        private UserControl.UCLabelEdit txtTransCode;
        private System.Windows.Forms.Button cmdItemSelect;
        private UserControl.UCLabelEdit txtItemDescV;
        private UserControl.UCLabelEdit txtMemo;
        private UserControl.UCLabelEdit txtPlanQty;
        private System.Windows.Forms.Button btnGetStack;
        private UserControl.UCLabelEdit ucLabelEditStock;
        private UserControl.UCLabelCombox ucLabelComboxCompany;
        private UserControl.UCLabelCombox ucLabelComboxINVType;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridRcard;
        private UserControl.UCLabelEdit txtCartonOrRCard;
        private UserControl.UCLabelEdit txtPalletCode;
        private UserControl.UCLabelEdit ucLabelEditQty;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetAddDelete;
        private UserControl.UCButton cmdCancel;
        private UserControl.UCLabelEdit txtRelationDoc;
        private UserControl.UCLabelEdit ucLabelEditstackMessage;
        private System.Windows.Forms.CheckBox checkItemCode;
        private System.Windows.Forms.CheckBox chbRcardLength;
        private UserControl.UCLabelEdit txtLength;
    }
}