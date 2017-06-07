namespace BenQGuru.eMES.Client
{
    partial class FMaterialTransfer
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
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMaterialTransfer));
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
            this.groupBoxTop = new System.Windows.Forms.GroupBox();
            this.btnGetStorageCode = new System.Windows.Forms.Button();
            this.txtstack = new System.Windows.Forms.TextBox();
            this.checkBoxNoFinish = new System.Windows.Forms.CheckBox();
            this.ucLabelEditMemo = new UserControl.UCLabelEdit();
            this.ucLabelEditToStorageID = new UserControl.UCLabelEdit();
            this.ucLabelEditFromStorageID = new UserControl.UCLabelEdit();
            this.ucLabelEditRectype = new UserControl.UCLabelEdit();
            this.ucLabelEditTransferNO = new UserControl.UCLabelEdit();
            this.groupBoxMiddle = new System.Windows.Forms.GroupBox();
            this.ultraGridHead = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBoxBottom = new System.Windows.Forms.GroupBox();
            this.opsetPackObject = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.ucButtonPrint = new UserControl.UCButton();
            this.ucLabelComboxPrintTemplete = new UserControl.UCLabelCombox();
            this.ucLabelComboxPrintList = new UserControl.UCLabelCombox();
            this.ucLabelEditPrintCount = new UserControl.UCLabelEdit();
            this.edtBufferDate = new UserControl.UCLabelEdit();
            this.opsCheckFIFO = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.ucLabelComboxType = new UserControl.UCLabelCombox();
            this.ucLERunningCard = new UserControl.UCLabelEdit();
            this.lblDay = new System.Windows.Forms.Label();
            this.ucButtonSend = new UserControl.UCButton();
            this.ucButtonExit = new UserControl.UCButton();
            this.groupBoxTop.SuspendLayout();
            this.groupBoxMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridHead)).BeginInit();
            this.groupBoxBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opsetPackObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opsCheckFIFO)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxTop
            // 
            this.groupBoxTop.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBoxTop.Controls.Add(this.btnGetStorageCode);
            this.groupBoxTop.Controls.Add(this.txtstack);
            this.groupBoxTop.Controls.Add(this.checkBoxNoFinish);
            this.groupBoxTop.Controls.Add(this.ucLabelEditMemo);
            this.groupBoxTop.Controls.Add(this.ucLabelEditToStorageID);
            this.groupBoxTop.Controls.Add(this.ucLabelEditFromStorageID);
            this.groupBoxTop.Controls.Add(this.ucLabelEditRectype);
            this.groupBoxTop.Controls.Add(this.ucLabelEditTransferNO);
            this.groupBoxTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxTop.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTop.Name = "groupBoxTop";
            this.groupBoxTop.Size = new System.Drawing.Size(850, 73);
            this.groupBoxTop.TabIndex = 17;
            this.groupBoxTop.TabStop = false;
            // 
            // btnGetStorageCode
            // 
            this.btnGetStorageCode.Enabled = false;
            this.btnGetStorageCode.Location = new System.Drawing.Point(420, 41);
            this.btnGetStorageCode.Name = "btnGetStorageCode";
            this.btnGetStorageCode.Size = new System.Drawing.Size(24, 22);
            this.btnGetStorageCode.TabIndex = 218;
            this.btnGetStorageCode.Text = "...";
            this.btnGetStorageCode.UseVisualStyleBackColor = true;
            this.btnGetStorageCode.Click += new System.EventHandler(this.btnGetStorageCode_Click);
            // 
            // txtstack
            // 
            this.txtstack.Location = new System.Drawing.Point(760, 10);
            this.txtstack.Name = "txtstack";
            this.txtstack.Size = new System.Drawing.Size(78, 21);
            this.txtstack.TabIndex = 216;
            this.txtstack.Visible = false;
            // 
            // checkBoxNoFinish
            // 
            this.checkBoxNoFinish.AutoSize = true;
            this.checkBoxNoFinish.Location = new System.Drawing.Point(462, 45);
            this.checkBoxNoFinish.Name = "checkBoxNoFinish";
            this.checkBoxNoFinish.Size = new System.Drawing.Size(60, 16);
            this.checkBoxNoFinish.TabIndex = 23;
            this.checkBoxNoFinish.Text = "未发完";
            this.checkBoxNoFinish.UseVisualStyleBackColor = true;
            this.checkBoxNoFinish.CheckedChanged += new System.EventHandler(this.checkBoxNoFinish_CheckedChanged);
            // 
            // ucLabelEditMemo
            // 
            this.ucLabelEditMemo.AllowEditOnlyChecked = true;
            this.ucLabelEditMemo.AutoSelectAll = false;
            this.ucLabelEditMemo.AutoUpper = true;
            this.ucLabelEditMemo.Caption = "备注";
            this.ucLabelEditMemo.Checked = false;
            this.ucLabelEditMemo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditMemo.Location = new System.Drawing.Point(519, 10);
            this.ucLabelEditMemo.MaxLength = 1000;
            this.ucLabelEditMemo.Multiline = true;
            this.ucLabelEditMemo.Name = "ucLabelEditMemo";
            this.ucLabelEditMemo.PasswordChar = '\0';
            this.ucLabelEditMemo.ReadOnly = false;
            this.ucLabelEditMemo.ShowCheckBox = false;
            this.ucLabelEditMemo.Size = new System.Drawing.Size(237, 60);
            this.ucLabelEditMemo.TabIndex = 21;
            this.ucLabelEditMemo.TabNext = true;
            this.ucLabelEditMemo.Value = "";
            this.ucLabelEditMemo.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditMemo.XAlign = 556;
            // 
            // ucLabelEditToStorageID
            // 
            this.ucLabelEditToStorageID.AllowEditOnlyChecked = true;
            this.ucLabelEditToStorageID.AutoSelectAll = false;
            this.ucLabelEditToStorageID.AutoUpper = true;
            this.ucLabelEditToStorageID.Caption = "目的库别";
            this.ucLabelEditToStorageID.Checked = false;
            this.ucLabelEditToStorageID.EditType = UserControl.EditTypes.String;
            this.ucLabelEditToStorageID.Location = new System.Drawing.Point(217, 42);
            this.ucLabelEditToStorageID.MaxLength = 40;
            this.ucLabelEditToStorageID.Multiline = false;
            this.ucLabelEditToStorageID.Name = "ucLabelEditToStorageID";
            this.ucLabelEditToStorageID.PasswordChar = '\0';
            this.ucLabelEditToStorageID.ReadOnly = false;
            this.ucLabelEditToStorageID.ShowCheckBox = false;
            this.ucLabelEditToStorageID.Size = new System.Drawing.Size(194, 22);
            this.ucLabelEditToStorageID.TabIndex = 20;
            this.ucLabelEditToStorageID.TabNext = true;
            this.ucLabelEditToStorageID.Value = "";
            this.ucLabelEditToStorageID.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditToStorageID.XAlign = 278;
            // 
            // ucLabelEditFromStorageID
            // 
            this.ucLabelEditFromStorageID.AllowEditOnlyChecked = true;
            this.ucLabelEditFromStorageID.AutoSelectAll = false;
            this.ucLabelEditFromStorageID.AutoUpper = true;
            this.ucLabelEditFromStorageID.Caption = "源库别";
            this.ucLabelEditFromStorageID.Checked = false;
            this.ucLabelEditFromStorageID.EditType = UserControl.EditTypes.String;
            this.ucLabelEditFromStorageID.Location = new System.Drawing.Point(12, 42);
            this.ucLabelEditFromStorageID.MaxLength = 40;
            this.ucLabelEditFromStorageID.Multiline = false;
            this.ucLabelEditFromStorageID.Name = "ucLabelEditFromStorageID";
            this.ucLabelEditFromStorageID.PasswordChar = '\0';
            this.ucLabelEditFromStorageID.ReadOnly = false;
            this.ucLabelEditFromStorageID.ShowCheckBox = false;
            this.ucLabelEditFromStorageID.Size = new System.Drawing.Size(182, 22);
            this.ucLabelEditFromStorageID.TabIndex = 19;
            this.ucLabelEditFromStorageID.TabNext = true;
            this.ucLabelEditFromStorageID.Value = "";
            this.ucLabelEditFromStorageID.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditFromStorageID.XAlign = 61;
            // 
            // ucLabelEditRectype
            // 
            this.ucLabelEditRectype.AllowEditOnlyChecked = true;
            this.ucLabelEditRectype.AutoSelectAll = false;
            this.ucLabelEditRectype.AutoUpper = true;
            this.ucLabelEditRectype.Caption = "单据类型";
            this.ucLabelEditRectype.Checked = false;
            this.ucLabelEditRectype.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRectype.Location = new System.Drawing.Point(217, 15);
            this.ucLabelEditRectype.MaxLength = 40;
            this.ucLabelEditRectype.Multiline = false;
            this.ucLabelEditRectype.Name = "ucLabelEditRectype";
            this.ucLabelEditRectype.PasswordChar = '\0';
            this.ucLabelEditRectype.ReadOnly = false;
            this.ucLabelEditRectype.ShowCheckBox = false;
            this.ucLabelEditRectype.Size = new System.Drawing.Size(194, 22);
            this.ucLabelEditRectype.TabIndex = 18;
            this.ucLabelEditRectype.TabNext = true;
            this.ucLabelEditRectype.Value = "";
            this.ucLabelEditRectype.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditRectype.XAlign = 278;
            // 
            // ucLabelEditTransferNO
            // 
            this.ucLabelEditTransferNO.AllowEditOnlyChecked = true;
            this.ucLabelEditTransferNO.AutoSelectAll = false;
            this.ucLabelEditTransferNO.AutoUpper = true;
            this.ucLabelEditTransferNO.Caption = "单号";
            this.ucLabelEditTransferNO.Checked = false;
            this.ucLabelEditTransferNO.EditType = UserControl.EditTypes.String;
            this.ucLabelEditTransferNO.Location = new System.Drawing.Point(24, 15);
            this.ucLabelEditTransferNO.MaxLength = 40;
            this.ucLabelEditTransferNO.Multiline = false;
            this.ucLabelEditTransferNO.Name = "ucLabelEditTransferNO";
            this.ucLabelEditTransferNO.PasswordChar = '\0';
            this.ucLabelEditTransferNO.ReadOnly = false;
            this.ucLabelEditTransferNO.ShowCheckBox = false;
            this.ucLabelEditTransferNO.Size = new System.Drawing.Size(170, 22);
            this.ucLabelEditTransferNO.TabIndex = 17;
            this.ucLabelEditTransferNO.TabNext = true;
            this.ucLabelEditTransferNO.Value = "";
            this.ucLabelEditTransferNO.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditTransferNO.XAlign = 61;
            this.ucLabelEditTransferNO.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditTransferNO_TxtboxKeyPress);
            // 
            // groupBoxMiddle
            // 
            this.groupBoxMiddle.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBoxMiddle.Controls.Add(this.ultraGridHead);
            this.groupBoxMiddle.Controls.Add(this.groupBoxBottom);
            this.groupBoxMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMiddle.Location = new System.Drawing.Point(0, 73);
            this.groupBoxMiddle.Name = "groupBoxMiddle";
            this.groupBoxMiddle.Size = new System.Drawing.Size(850, 444);
            this.groupBoxMiddle.TabIndex = 18;
            this.groupBoxMiddle.TabStop = false;
            // 
            // ultraGridHead
            // 
            this.ultraGridHead.Cursor = System.Windows.Forms.Cursors.Hand;
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGridHead.DisplayLayout.Appearance = appearance4;
            this.ultraGridHead.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridHead.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance1.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridHead.DisplayLayout.GroupByBox.Appearance = appearance1;
            appearance2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridHead.DisplayLayout.GroupByBox.BandLabelAppearance = appearance2;
            this.ultraGridHead.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridHead.DisplayLayout.GroupByBox.Hidden = true;
            appearance14.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance14.BackColor2 = System.Drawing.SystemColors.Control;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance14.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridHead.DisplayLayout.GroupByBox.PromptAppearance = appearance14;
            this.ultraGridHead.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGridHead.DisplayLayout.MaxRowScrollRegions = 1;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGridHead.DisplayLayout.Override.ActiveCellAppearance = appearance12;
            appearance13.BackColor = System.Drawing.SystemColors.Highlight;
            appearance13.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGridHead.DisplayLayout.Override.ActiveRowAppearance = appearance13;
            this.ultraGridHead.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGridHead.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGridHead.DisplayLayout.Override.CardAreaAppearance = appearance6;
            appearance5.BorderColor = System.Drawing.Color.Silver;
            appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGridHead.DisplayLayout.Override.CellAppearance = appearance5;
            this.ultraGridHead.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGridHead.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridHead.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance11.TextHAlignAsString = "Left";
            this.ultraGridHead.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.ultraGridHead.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGridHead.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            this.ultraGridHead.DisplayLayout.Override.RowAppearance = appearance10;
            this.ultraGridHead.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGridHead.DisplayLayout.Override.TemplateAddRowAppearance = appearance8;
            this.ultraGridHead.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGridHead.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGridHead.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraGridHead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridHead.Location = new System.Drawing.Point(3, 17);
            this.ultraGridHead.Name = "ultraGridHead";
            this.ultraGridHead.Size = new System.Drawing.Size(844, 309);
            this.ultraGridHead.TabIndex = 7;
            this.ultraGridHead.AfterCellUpdate += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridHead_AfterCellUpdate);
            this.ultraGridHead.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.ultraGridHead_ClickCell);
            this.ultraGridHead.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ultraGridHead_InitializeRow);
            this.ultraGridHead.ClickCellButton += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridHead_ClickCellButton);
            this.ultraGridHead.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridHead_InitializeLayout);
            // 
            // groupBoxBottom
            // 
            this.groupBoxBottom.Controls.Add(this.opsetPackObject);
            this.groupBoxBottom.Controls.Add(this.ucButtonPrint);
            this.groupBoxBottom.Controls.Add(this.ucLabelComboxPrintTemplete);
            this.groupBoxBottom.Controls.Add(this.ucLabelComboxPrintList);
            this.groupBoxBottom.Controls.Add(this.ucLabelEditPrintCount);
            this.groupBoxBottom.Controls.Add(this.edtBufferDate);
            this.groupBoxBottom.Controls.Add(this.opsCheckFIFO);
            this.groupBoxBottom.Controls.Add(this.ucLabelComboxType);
            this.groupBoxBottom.Controls.Add(this.ucLERunningCard);
            this.groupBoxBottom.Controls.Add(this.lblDay);
            this.groupBoxBottom.Controls.Add(this.ucButtonSend);
            this.groupBoxBottom.Controls.Add(this.ucButtonExit);
            this.groupBoxBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxBottom.Location = new System.Drawing.Point(3, 326);
            this.groupBoxBottom.Name = "groupBoxBottom";
            this.groupBoxBottom.Size = new System.Drawing.Size(844, 115);
            this.groupBoxBottom.TabIndex = 6;
            this.groupBoxBottom.TabStop = false;
            // 
            // opsetPackObject
            // 
            this.opsetPackObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            appearance3.FontData.BoldAsString = "False";
            this.opsetPackObject.Appearance = appearance3;
            this.opsetPackObject.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            valueListItem3.DataValue = "0";
            valueListItem3.DisplayText = "产品序号";
            valueListItem4.DataValue = "1";
            valueListItem4.DisplayText = "Pallet";
            this.opsetPackObject.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem3,
            valueListItem4});
            this.opsetPackObject.Location = new System.Drawing.Point(18, 12);
            this.opsetPackObject.Name = "opsetPackObject";
            this.opsetPackObject.Size = new System.Drawing.Size(151, 16);
            this.opsetPackObject.TabIndex = 205;
            this.opsetPackObject.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.opsetPackObject.Visible = false;
            this.opsetPackObject.ValueChanged += new System.EventHandler(this.opsetPackObject_ValueChanged);
            // 
            // ucButtonPrint
            // 
            this.ucButtonPrint.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonPrint.BackgroundImage")));
            this.ucButtonPrint.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonPrint.Caption = "打印";
            this.ucButtonPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonPrint.Location = new System.Drawing.Point(177, 87);
            this.ucButtonPrint.Name = "ucButtonPrint";
            this.ucButtonPrint.Size = new System.Drawing.Size(88, 22);
            this.ucButtonPrint.TabIndex = 57;
            this.ucButtonPrint.Click += new System.EventHandler(this.ucButtonPrint_Click);
            // 
            // ucLabelComboxPrintTemplete
            // 
            this.ucLabelComboxPrintTemplete.AllowEditOnlyChecked = true;
            this.ucLabelComboxPrintTemplete.Caption = "打印模板";
            this.ucLabelComboxPrintTemplete.Checked = false;
            this.ucLabelComboxPrintTemplete.Location = new System.Drawing.Point(297, 59);
            this.ucLabelComboxPrintTemplete.Name = "ucLabelComboxPrintTemplete";
            this.ucLabelComboxPrintTemplete.SelectedIndex = -1;
            this.ucLabelComboxPrintTemplete.ShowCheckBox = false;
            this.ucLabelComboxPrintTemplete.Size = new System.Drawing.Size(194, 22);
            this.ucLabelComboxPrintTemplete.TabIndex = 55;
            this.ucLabelComboxPrintTemplete.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxPrintTemplete.XAlign = 358;
            // 
            // ucLabelComboxPrintList
            // 
            this.ucLabelComboxPrintList.AllowEditOnlyChecked = true;
            this.ucLabelComboxPrintList.Caption = "打印机列表";
            this.ucLabelComboxPrintList.Checked = false;
            this.ucLabelComboxPrintList.Location = new System.Drawing.Point(3, 59);
            this.ucLabelComboxPrintList.Name = "ucLabelComboxPrintList";
            this.ucLabelComboxPrintList.SelectedIndex = -1;
            this.ucLabelComboxPrintList.ShowCheckBox = false;
            this.ucLabelComboxPrintList.Size = new System.Drawing.Size(273, 22);
            this.ucLabelComboxPrintList.TabIndex = 56;
            this.ucLabelComboxPrintList.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelComboxPrintList.XAlign = 76;
            // 
            // ucLabelEditPrintCount
            // 
            this.ucLabelEditPrintCount.AllowEditOnlyChecked = true;
            this.ucLabelEditPrintCount.AutoSelectAll = false;
            this.ucLabelEditPrintCount.AutoUpper = true;
            this.ucLabelEditPrintCount.Caption = "打印数量";
            this.ucLabelEditPrintCount.Checked = false;
            this.ucLabelEditPrintCount.EditType = UserControl.EditTypes.Integer;
            this.ucLabelEditPrintCount.Location = new System.Drawing.Point(625, 54);
            this.ucLabelEditPrintCount.MaxLength = 8;
            this.ucLabelEditPrintCount.Multiline = false;
            this.ucLabelEditPrintCount.Name = "ucLabelEditPrintCount";
            this.ucLabelEditPrintCount.PasswordChar = '\0';
            this.ucLabelEditPrintCount.ReadOnly = false;
            this.ucLabelEditPrintCount.ShowCheckBox = false;
            this.ucLabelEditPrintCount.Size = new System.Drawing.Size(111, 22);
            this.ucLabelEditPrintCount.TabIndex = 54;
            this.ucLabelEditPrintCount.TabNext = false;
            this.ucLabelEditPrintCount.Tag = "";
            this.ucLabelEditPrintCount.Value = "1";
            this.ucLabelEditPrintCount.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditPrintCount.XAlign = 686;
            // 
            // edtBufferDate
            // 
            this.edtBufferDate.AllowEditOnlyChecked = true;
            this.edtBufferDate.AutoSelectAll = false;
            this.edtBufferDate.AutoUpper = true;
            this.edtBufferDate.Caption = "缓冲期限";
            this.edtBufferDate.Checked = false;
            this.edtBufferDate.EditType = UserControl.EditTypes.Integer;
            this.edtBufferDate.Location = new System.Drawing.Point(625, 28);
            this.edtBufferDate.MaxLength = 40;
            this.edtBufferDate.Multiline = false;
            this.edtBufferDate.Name = "edtBufferDate";
            this.edtBufferDate.PasswordChar = '\0';
            this.edtBufferDate.ReadOnly = false;
            this.edtBufferDate.ShowCheckBox = false;
            this.edtBufferDate.Size = new System.Drawing.Size(111, 20);
            this.edtBufferDate.TabIndex = 24;
            this.edtBufferDate.TabNext = false;
            this.edtBufferDate.Value = "1";
            this.edtBufferDate.WidthType = UserControl.WidthTypes.Tiny;
            this.edtBufferDate.XAlign = 686;
            // 
            // opsCheckFIFO
            // 
            appearance7.FontData.BoldAsString = "False";
            this.opsCheckFIFO.Appearance = appearance7;
            this.opsCheckFIFO.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            valueListItem7.DataValue = "0";
            valueListItem7.DisplayText = "普通FIFO检查";
            valueListItem8.DataValue = "1";
            valueListItem8.DisplayText = "按供应商FIFO检查";
            this.opsCheckFIFO.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem7,
            valueListItem8});
            this.opsCheckFIFO.ItemSpacingVertical = 10;
            this.opsCheckFIFO.Location = new System.Drawing.Point(499, 28);
            this.opsCheckFIFO.Name = "opsCheckFIFO";
            this.opsCheckFIFO.Size = new System.Drawing.Size(120, 53);
            this.opsCheckFIFO.TabIndex = 23;
            this.opsCheckFIFO.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // ucLabelComboxType
            // 
            this.ucLabelComboxType.AllowEditOnlyChecked = true;
            this.ucLabelComboxType.Caption = "业务类型";
            this.ucLabelComboxType.Checked = false;
            this.ucLabelComboxType.Location = new System.Drawing.Point(297, 31);
            this.ucLabelComboxType.Name = "ucLabelComboxType";
            this.ucLabelComboxType.SelectedIndex = -1;
            this.ucLabelComboxType.ShowCheckBox = false;
            this.ucLabelComboxType.Size = new System.Drawing.Size(194, 22);
            this.ucLabelComboxType.TabIndex = 22;
            this.ucLabelComboxType.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxType.XAlign = 358;
            this.ucLabelComboxType.SelectedIndexChanged += new System.EventHandler(this.ucLabelComboxType_SelectedIndexChanged);
            // 
            // ucLERunningCard
            // 
            this.ucLERunningCard.AllowEditOnlyChecked = true;
            this.ucLERunningCard.AutoSelectAll = false;
            this.ucLERunningCard.AutoUpper = true;
            this.ucLERunningCard.Caption = "输入框";
            this.ucLERunningCard.Checked = false;
            this.ucLERunningCard.EditType = UserControl.EditTypes.String;
            this.ucLERunningCard.Location = new System.Drawing.Point(27, 31);
            this.ucLERunningCard.MaxLength = 40;
            this.ucLERunningCard.Multiline = false;
            this.ucLERunningCard.Name = "ucLERunningCard";
            this.ucLERunningCard.PasswordChar = '\0';
            this.ucLERunningCard.ReadOnly = false;
            this.ucLERunningCard.ShowCheckBox = false;
            this.ucLERunningCard.Size = new System.Drawing.Size(249, 22);
            this.ucLERunningCard.TabIndex = 21;
            this.ucLERunningCard.TabNext = true;
            this.ucLERunningCard.Value = "";
            this.ucLERunningCard.WidthType = UserControl.WidthTypes.Long;
            this.ucLERunningCard.XAlign = 76;
            this.ucLERunningCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditInput_TxtboxKeyPress);
            // 
            // lblDay
            // 
            this.lblDay.AutoSize = true;
            this.lblDay.Location = new System.Drawing.Point(742, 31);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(17, 12);
            this.lblDay.TabIndex = 51;
            this.lblDay.Text = "天";
            // 
            // ucButtonSend
            // 
            this.ucButtonSend.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonSend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonSend.BackgroundImage")));
            this.ucButtonSend.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonSend.Caption = "发料";
            this.ucButtonSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonSend.Location = new System.Drawing.Point(383, 87);
            this.ucButtonSend.Name = "ucButtonSend";
            this.ucButtonSend.Size = new System.Drawing.Size(88, 22);
            this.ucButtonSend.TabIndex = 19;
            this.ucButtonSend.TabStop = false;
            this.ucButtonSend.Click += new System.EventHandler(this.ucButtonSend_Click);
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(603, 87);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 20;
            this.ucButtonExit.TabStop = false;
            this.ucButtonExit.Click += new System.EventHandler(this.ucButtonExit_Click);
            // 
            // FMaterialTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 517);
            this.Controls.Add(this.groupBoxMiddle);
            this.Controls.Add(this.groupBoxTop);
            this.Name = "FMaterialTransfer";
            this.Text = "物料转移";
            this.Load += new System.EventHandler(this.FMaterialTransfer_Load);
            this.groupBoxTop.ResumeLayout(false);
            this.groupBoxTop.PerformLayout();
            this.groupBoxMiddle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridHead)).EndInit();
            this.groupBoxBottom.ResumeLayout(false);
            this.groupBoxBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opsetPackObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opsCheckFIFO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxTop;
        private UserControl.UCLabelEdit ucLabelEditMemo;
        private UserControl.UCLabelEdit ucLabelEditToStorageID;
        private UserControl.UCLabelEdit ucLabelEditFromStorageID;
        private UserControl.UCLabelEdit ucLabelEditRectype;
        private UserControl.UCLabelEdit ucLabelEditTransferNO;
        private System.Windows.Forms.CheckBox checkBoxNoFinish;
        private System.Windows.Forms.GroupBox groupBoxMiddle;
        private System.Windows.Forms.GroupBox groupBoxBottom;
        private UserControl.UCButton ucButtonSend;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCLabelCombox ucLabelComboxType;
        private UserControl.UCLabelEdit ucLERunningCard;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridHead;
        private UserControl.UCLabelEdit edtBufferDate;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet opsCheckFIFO;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.TextBox txtstack;
        private System.Windows.Forms.Button btnGetStorageCode;
        public UserControl.UCButton ucButtonPrint;
        public UserControl.UCLabelCombox ucLabelComboxPrintTemplete;
        public UserControl.UCLabelCombox ucLabelComboxPrintList;
        private UserControl.UCLabelEdit ucLabelEditPrintCount;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet opsetPackObject;
    }
}