namespace BenQGuru.eMES.Client
{
    partial class FInInvByProduce
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FInInvByProduce));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
            this.inInvInfo = new System.Windows.Forms.GroupBox();
            this.cboBusinessType = new UserControl.UCLabelCombox();
            this.ucLabelEditstackMessage = new UserControl.UCLabelEdit();
            this.btnGetStack = new System.Windows.Forms.Button();
            this.ucLabelEditStock = new UserControl.UCLabelEdit();
            this.ucLabelComboxCompany = new UserControl.UCLabelCombox();
            this.ucLabelComboxINVType = new UserControl.UCLabelCombox();
            this.btnInINV = new UserControl.UCButton();
            this.btnClear = new UserControl.UCButton();
            this.inputSN = new System.Windows.Forms.GroupBox();
            this.ultraOptionSetAddDelete = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.ucLabelEditInput = new UserControl.UCLabelEdit();
            this.ucLabelEditQty = new UserControl.UCLabelEdit();
            this.opsetPackObject = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.grdRcard = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.inInvInfo.SuspendLayout();
            this.inputSN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetAddDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opsetPackObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRcard)).BeginInit();
            this.SuspendLayout();
            // 
            // inInvInfo
            // 
            this.inInvInfo.Controls.Add(this.cboBusinessType);
            this.inInvInfo.Controls.Add(this.ucLabelEditstackMessage);
            this.inInvInfo.Controls.Add(this.btnGetStack);
            this.inInvInfo.Controls.Add(this.ucLabelEditStock);
            this.inInvInfo.Controls.Add(this.ucLabelComboxCompany);
            this.inInvInfo.Controls.Add(this.ucLabelComboxINVType);
            this.inInvInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.inInvInfo.Location = new System.Drawing.Point(0, 0);
            this.inInvInfo.Name = "inInvInfo";
            this.inInvInfo.Size = new System.Drawing.Size(792, 86);
            this.inInvInfo.TabIndex = 0;
            this.inInvInfo.TabStop = false;
            this.inInvInfo.Text = "入库讯息";
            // 
            // cboBusinessType
            // 
            this.cboBusinessType.AllowEditOnlyChecked = true;
            this.cboBusinessType.Caption = "业务类型";
            this.cboBusinessType.Checked = false;
            this.cboBusinessType.Location = new System.Drawing.Point(262, 46);
            this.cboBusinessType.Name = "cboBusinessType";
            this.cboBusinessType.SelectedIndex = -1;
            this.cboBusinessType.ShowCheckBox = false;
            this.cboBusinessType.Size = new System.Drawing.Size(194, 20);
            this.cboBusinessType.TabIndex = 206;
            this.cboBusinessType.WidthType = UserControl.WidthTypes.Normal;
            this.cboBusinessType.XAlign = 323;
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
            this.ucLabelEditstackMessage.Location = new System.Drawing.Point(526, 20);
            this.ucLabelEditstackMessage.MaxLength = 40;
            this.ucLabelEditstackMessage.Multiline = false;
            this.ucLabelEditstackMessage.Name = "ucLabelEditstackMessage";
            this.ucLabelEditstackMessage.PasswordChar = '\0';
            this.ucLabelEditstackMessage.ReadOnly = false;
            this.ucLabelEditstackMessage.ShowCheckBox = false;
            this.ucLabelEditstackMessage.Size = new System.Drawing.Size(189, 20);
            this.ucLabelEditstackMessage.TabIndex = 15;
            this.ucLabelEditstackMessage.TabNext = true;
            this.ucLabelEditstackMessage.Value = "";
            this.ucLabelEditstackMessage.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditstackMessage.XAlign = 665;
            // 
            // btnGetStack
            // 
            this.btnGetStack.Location = new System.Drawing.Point(461, 17);
            this.btnGetStack.Name = "btnGetStack";
            this.btnGetStack.Size = new System.Drawing.Size(34, 23);
            this.btnGetStack.TabIndex = 3;
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
            this.ucLabelEditStock.Location = new System.Drawing.Point(285, 19);
            this.ucLabelEditStock.MaxLength = 40;
            this.ucLabelEditStock.Multiline = false;
            this.ucLabelEditStock.Name = "ucLabelEditStock";
            this.ucLabelEditStock.PasswordChar = '\0';
            this.ucLabelEditStock.ReadOnly = true;
            this.ucLabelEditStock.ShowCheckBox = false;
            this.ucLabelEditStock.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditStock.TabIndex = 2;
            this.ucLabelEditStock.TabNext = true;
            this.ucLabelEditStock.Value = "";
            this.ucLabelEditStock.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditStock.XAlign = 322;
            // 
            // ucLabelComboxCompany
            // 
            this.ucLabelComboxCompany.AllowEditOnlyChecked = true;
            this.ucLabelComboxCompany.Caption = "公司";
            this.ucLabelComboxCompany.Checked = false;
            this.ucLabelComboxCompany.Location = new System.Drawing.Point(53, 46);
            this.ucLabelComboxCompany.Name = "ucLabelComboxCompany";
            this.ucLabelComboxCompany.SelectedIndex = -1;
            this.ucLabelComboxCompany.ShowCheckBox = false;
            this.ucLabelComboxCompany.Size = new System.Drawing.Size(170, 20);
            this.ucLabelComboxCompany.TabIndex = 4;
            this.ucLabelComboxCompany.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxCompany.XAlign = 90;
            // 
            // ucLabelComboxINVType
            // 
            this.ucLabelComboxINVType.AllowEditOnlyChecked = true;
            this.ucLabelComboxINVType.Caption = "库别";
            this.ucLabelComboxINVType.Checked = false;
            this.ucLabelComboxINVType.Location = new System.Drawing.Point(53, 20);
            this.ucLabelComboxINVType.Name = "ucLabelComboxINVType";
            this.ucLabelComboxINVType.SelectedIndex = -1;
            this.ucLabelComboxINVType.ShowCheckBox = false;
            this.ucLabelComboxINVType.Size = new System.Drawing.Size(170, 20);
            this.ucLabelComboxINVType.TabIndex = 1;
            this.ucLabelComboxINVType.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxINVType.XAlign = 90;
            // 
            // btnInINV
            // 
            this.btnInINV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnInINV.BackColor = System.Drawing.SystemColors.Control;
            this.btnInINV.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInINV.BackgroundImage")));
            this.btnInINV.ButtonType = UserControl.ButtonTypes.None;
            this.btnInINV.Caption = "入库";
            this.btnInINV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInINV.Location = new System.Drawing.Point(416, 528);
            this.btnInINV.Name = "btnInINV";
            this.btnInINV.Size = new System.Drawing.Size(88, 22);
            this.btnInINV.TabIndex = 9;
            this.btnInINV.Click += new System.EventHandler(this.btnInINV_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.BackColor = System.Drawing.SystemColors.Control;
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.ButtonType = UserControl.ButtonTypes.None;
            this.btnClear.Caption = "清空";
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Location = new System.Drawing.Point(416, 387);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 22);
            this.btnClear.TabIndex = 8;
            this.btnClear.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // inputSN
            // 
            this.inputSN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.inputSN.Controls.Add(this.btnClear);
            this.inputSN.Controls.Add(this.ultraOptionSetAddDelete);
            this.inputSN.Controls.Add(this.ucLabelEditInput);
            this.inputSN.Controls.Add(this.ucLabelEditQty);
            this.inputSN.Controls.Add(this.opsetPackObject);
            this.inputSN.Controls.Add(this.grdRcard);
            this.inputSN.Location = new System.Drawing.Point(0, 86);
            this.inputSN.Name = "inputSN";
            this.inputSN.Size = new System.Drawing.Size(792, 427);
            this.inputSN.TabIndex = 1;
            this.inputSN.TabStop = false;
            this.inputSN.Text = "序列号输入";
            // 
            // ultraOptionSetAddDelete
            // 
            this.ultraOptionSetAddDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            appearance1.FontData.BoldAsString = "False";
            this.ultraOptionSetAddDelete.Appearance = appearance1;
            this.ultraOptionSetAddDelete.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            valueListItem1.DataValue = "0";
            valueListItem1.DisplayText = "新增";
            valueListItem2.DataValue = "2";
            valueListItem2.DisplayText = "移除";
            this.ultraOptionSetAddDelete.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2});
            this.ultraOptionSetAddDelete.Location = new System.Drawing.Point(296, 391);
            this.ultraOptionSetAddDelete.Name = "ultraOptionSetAddDelete";
            this.ultraOptionSetAddDelete.Size = new System.Drawing.Size(114, 16);
            this.ultraOptionSetAddDelete.TabIndex = 7;
            this.ultraOptionSetAddDelete.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.ultraOptionSetAddDelete.Click += new System.EventHandler(this.ultraOptionSet1_Click);
            this.ultraOptionSetAddDelete.ValueChanged += new System.EventHandler(this.ultraOptionSetAddDelete_ValueChanged);
            // 
            // ucLabelEditInput
            // 
            this.ucLabelEditInput.AllowEditOnlyChecked = true;
            this.ucLabelEditInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditInput.AutoSelectAll = false;
            this.ucLabelEditInput.AutoUpper = true;
            this.ucLabelEditInput.Caption = "输入";
            this.ucLabelEditInput.Checked = false;
            this.ucLabelEditInput.EditType = UserControl.EditTypes.String;
            this.ucLabelEditInput.Location = new System.Drawing.Point(53, 387);
            this.ucLabelEditInput.MaxLength = 40;
            this.ucLabelEditInput.Multiline = false;
            this.ucLabelEditInput.Name = "ucLabelEditInput";
            this.ucLabelEditInput.PasswordChar = '\0';
            this.ucLabelEditInput.ReadOnly = false;
            this.ucLabelEditInput.ShowCheckBox = false;
            this.ucLabelEditInput.Size = new System.Drawing.Size(237, 24);
            this.ucLabelEditInput.TabIndex = 6;
            this.ucLabelEditInput.TabNext = true;
            this.ucLabelEditInput.Value = "";
            this.ucLabelEditInput.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditInput.XAlign = 90;
            this.ucLabelEditInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditInput_TxtboxKeyPress);
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
            this.ucLabelEditQty.Location = new System.Drawing.Point(699, 313);
            this.ucLabelEditQty.MaxLength = 40;
            this.ucLabelEditQty.Multiline = false;
            this.ucLabelEditQty.Name = "ucLabelEditQty";
            this.ucLabelEditQty.PasswordChar = '\0';
            this.ucLabelEditQty.ReadOnly = true;
            this.ucLabelEditQty.ShowCheckBox = false;
            this.ucLabelEditQty.Size = new System.Drawing.Size(87, 24);
            this.ucLabelEditQty.TabIndex = 205;
            this.ucLabelEditQty.TabNext = true;
            this.ucLabelEditQty.Value = "";
            this.ucLabelEditQty.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditQty.XAlign = 736;
            // 
            // opsetPackObject
            // 
            this.opsetPackObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            appearance3.FontData.BoldAsString = "False";
            this.opsetPackObject.Appearance = appearance3;
            this.opsetPackObject.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            valueListItem3.DataValue = "0";
            valueListItem3.DisplayText = "栈板";
            valueListItem4.DataValue = "2";
            valueListItem4.DisplayText = "箱号";
            valueListItem5.DataValue = "1";
            valueListItem5.DisplayText = "产品序列号";
            this.opsetPackObject.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem3,
            valueListItem4,
            valueListItem5});
            this.opsetPackObject.Location = new System.Drawing.Point(53, 350);
            this.opsetPackObject.Name = "opsetPackObject";
            this.opsetPackObject.Size = new System.Drawing.Size(219, 16);
            this.opsetPackObject.TabIndex = 5;
            this.opsetPackObject.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.opsetPackObject.Click += new System.EventHandler(this.opsetPackObject_Click);
            this.opsetPackObject.ValueChanged += new System.EventHandler(this.opsetPackObject_ValueChanged);
            // 
            // grdRcard
            // 
            this.grdRcard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRcard.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdRcard.Location = new System.Drawing.Point(3, 17);
            this.grdRcard.Name = "grdRcard";
            this.grdRcard.Size = new System.Drawing.Size(793, 288);
            this.grdRcard.TabIndex = 10;
            this.grdRcard.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.grdRcard_InitializeLayout);
            // 
            // FInInvByProduce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.btnInINV);
            this.Controls.Add(this.inputSN);
            this.Controls.Add(this.inInvInfo);
            this.Name = "FInInvByProduce";
            this.Text = "生产性入库";
            this.Load += new System.EventHandler(this.FInInvByProduce_Load);
            this.inInvInfo.ResumeLayout(false);
            this.inputSN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetAddDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opsetPackObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRcard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox inInvInfo;
        private System.Windows.Forms.GroupBox inputSN;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdRcard;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet opsetPackObject;
        private System.Windows.Forms.Button btnGetStack;
        private UserControl.UCLabelEdit ucLabelEditStock;
        private UserControl.UCLabelCombox ucLabelComboxCompany;
        private UserControl.UCLabelCombox ucLabelComboxINVType;
        private UserControl.UCButton btnClear;
        private UserControl.UCButton btnInINV;
        private UserControl.UCLabelEdit ucLabelEditInput;
        private UserControl.UCLabelEdit ucLabelEditQty;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetAddDelete;
        private UserControl.UCLabelEdit ucLabelEditstackMessage;
        private UserControl.UCLabelCombox cboBusinessType;
    }
}