namespace BenQGuru.eMES.Client
{
    partial class FOQCLotQA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FOQCLotQA));
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucLabelEditStatus = new UserControl.UCLabelEdit();
            this.ucLabelEditCartonNo = new UserControl.UCLabelEdit();
            this.ucButtonNewLot = new UserControl.UCButton();
            this.ucLabelEditMoCodeQuery = new UserControl.UCLabelEdit();
            this.ucLabelEditLotNo = new UserControl.UCLabelEdit();
            this.ucLabelEditItemCodeQuery = new UserControl.UCLabelEdit();
            this.ucLabelEditRCard = new UserControl.UCLabelEdit();
            this.ucButtonGetLot = new UserControl.UCButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ucLabelEditCapacity = new UserControl.UCLabelEdit();
            this.ucLabelEditInputCarton = new UserControl.UCLabelEdit();
            this.checkBoxSelectAll = new System.Windows.Forms.CheckBox();
            this.ucBtnDelete = new UserControl.UCButton();
            this.ucButtonLot = new UserControl.UCButton();
            this.ucButtonExit = new UserControl.UCButton();
            this.ultraGridLot2CardList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridLot2CardList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucLabelEditStatus);
            this.panel1.Controls.Add(this.ucLabelEditCartonNo);
            this.panel1.Controls.Add(this.ucButtonNewLot);
            this.panel1.Controls.Add(this.ucLabelEditMoCodeQuery);
            this.panel1.Controls.Add(this.ucLabelEditLotNo);
            this.panel1.Controls.Add(this.ucLabelEditItemCodeQuery);
            this.panel1.Controls.Add(this.ucLabelEditRCard);
            this.panel1.Controls.Add(this.ucButtonGetLot);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(842, 165);
            this.panel1.TabIndex = 27;
            // 
            // ucLabelEditStatus
            // 
            this.ucLabelEditStatus.AllowEditOnlyChecked = true;
            this.ucLabelEditStatus.AutoSelectAll = false;
            this.ucLabelEditStatus.AutoUpper = true;
            this.ucLabelEditStatus.Caption = "状态";
            this.ucLabelEditStatus.Checked = false;
            this.ucLabelEditStatus.EditType = UserControl.EditTypes.String;
            this.ucLabelEditStatus.Location = new System.Drawing.Point(471, 120);
            this.ucLabelEditStatus.MaxLength = 20;
            this.ucLabelEditStatus.Multiline = false;
            this.ucLabelEditStatus.Name = "ucLabelEditStatus";
            this.ucLabelEditStatus.PasswordChar = '\0';
            this.ucLabelEditStatus.ReadOnly = true;
            this.ucLabelEditStatus.ShowCheckBox = false;
            this.ucLabelEditStatus.Size = new System.Drawing.Size(137, 24);
            this.ucLabelEditStatus.TabIndex = 27;
            this.ucLabelEditStatus.TabNext = true;
            this.ucLabelEditStatus.Value = "";
            this.ucLabelEditStatus.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditStatus.XAlign = 508;
            // 
            // ucLabelEditCartonNo
            // 
            this.ucLabelEditCartonNo.AllowEditOnlyChecked = true;
            this.ucLabelEditCartonNo.AutoSelectAll = false;
            this.ucLabelEditCartonNo.AutoUpper = true;
            this.ucLabelEditCartonNo.Caption = "箱号";
            this.ucLabelEditCartonNo.Checked = false;
            this.ucLabelEditCartonNo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditCartonNo.Location = new System.Drawing.Point(43, 54);
            this.ucLabelEditCartonNo.MaxLength = 40;
            this.ucLabelEditCartonNo.Multiline = false;
            this.ucLabelEditCartonNo.Name = "ucLabelEditCartonNo";
            this.ucLabelEditCartonNo.PasswordChar = '\0';
            this.ucLabelEditCartonNo.ReadOnly = false;
            this.ucLabelEditCartonNo.ShowCheckBox = false;
            this.ucLabelEditCartonNo.Size = new System.Drawing.Size(437, 24);
            this.ucLabelEditCartonNo.TabIndex = 26;
            this.ucLabelEditCartonNo.TabNext = true;
            this.ucLabelEditCartonNo.Value = "";
            this.ucLabelEditCartonNo.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditCartonNo.XAlign = 80;
            this.ucLabelEditCartonNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditCartonNo_TxtboxKeyPress);
            // 
            // ucButtonNewLot
            // 
            this.ucButtonNewLot.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonNewLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonNewLot.BackgroundImage")));
            this.ucButtonNewLot.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonNewLot.Caption = "新批号";
            this.ucButtonNewLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonNewLot.Location = new System.Drawing.Point(520, 25);
            this.ucButtonNewLot.Name = "ucButtonNewLot";
            this.ucButtonNewLot.Size = new System.Drawing.Size(88, 22);
            this.ucButtonNewLot.TabIndex = 15;
            this.ucButtonNewLot.Click += new System.EventHandler(this.ucButtonNewLot_Click);
            // 
            // ucLabelEditMoCodeQuery
            // 
            this.ucLabelEditMoCodeQuery.AllowEditOnlyChecked = true;
            this.ucLabelEditMoCodeQuery.AutoSelectAll = false;
            this.ucLabelEditMoCodeQuery.AutoUpper = true;
            this.ucLabelEditMoCodeQuery.Caption = "工单代码";
            this.ucLabelEditMoCodeQuery.Checked = false;
            this.ucLabelEditMoCodeQuery.EditType = UserControl.EditTypes.String;
            this.ucLabelEditMoCodeQuery.Location = new System.Drawing.Point(19, 120);
            this.ucLabelEditMoCodeQuery.MaxLength = 20;
            this.ucLabelEditMoCodeQuery.Multiline = false;
            this.ucLabelEditMoCodeQuery.Name = "ucLabelEditMoCodeQuery";
            this.ucLabelEditMoCodeQuery.PasswordChar = '\0';
            this.ucLabelEditMoCodeQuery.ReadOnly = true;
            this.ucLabelEditMoCodeQuery.ShowCheckBox = false;
            this.ucLabelEditMoCodeQuery.Size = new System.Drawing.Size(194, 24);
            this.ucLabelEditMoCodeQuery.TabIndex = 25;
            this.ucLabelEditMoCodeQuery.TabNext = true;
            this.ucLabelEditMoCodeQuery.Value = "";
            this.ucLabelEditMoCodeQuery.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditMoCodeQuery.XAlign = 80;
            // 
            // ucLabelEditLotNo
            // 
            this.ucLabelEditLotNo.AllowEditOnlyChecked = true;
            this.ucLabelEditLotNo.AutoSelectAll = false;
            this.ucLabelEditLotNo.AutoUpper = true;
            this.ucLabelEditLotNo.Caption = "批号";
            this.ucLabelEditLotNo.Checked = false;
            this.ucLabelEditLotNo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditLotNo.Location = new System.Drawing.Point(43, 24);
            this.ucLabelEditLotNo.MaxLength = 40;
            this.ucLabelEditLotNo.Multiline = false;
            this.ucLabelEditLotNo.Name = "ucLabelEditLotNo";
            this.ucLabelEditLotNo.PasswordChar = '\0';
            this.ucLabelEditLotNo.ReadOnly = false;
            this.ucLabelEditLotNo.ShowCheckBox = false;
            this.ucLabelEditLotNo.Size = new System.Drawing.Size(437, 24);
            this.ucLabelEditLotNo.TabIndex = 10;
            this.ucLabelEditLotNo.TabNext = true;
            this.ucLabelEditLotNo.Value = "";
            this.ucLabelEditLotNo.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditLotNo.XAlign = 80;
            this.ucLabelEditLotNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditLotNo_TxtboxKeyPress);
            // 
            // ucLabelEditItemCodeQuery
            // 
            this.ucLabelEditItemCodeQuery.AllowEditOnlyChecked = true;
            this.ucLabelEditItemCodeQuery.AutoSelectAll = false;
            this.ucLabelEditItemCodeQuery.AutoUpper = true;
            this.ucLabelEditItemCodeQuery.Caption = "产品料号";
            this.ucLabelEditItemCodeQuery.Checked = false;
            this.ucLabelEditItemCodeQuery.EditType = UserControl.EditTypes.String;
            this.ucLabelEditItemCodeQuery.Location = new System.Drawing.Point(243, 120);
            this.ucLabelEditItemCodeQuery.MaxLength = 20;
            this.ucLabelEditItemCodeQuery.Multiline = false;
            this.ucLabelEditItemCodeQuery.Name = "ucLabelEditItemCodeQuery";
            this.ucLabelEditItemCodeQuery.PasswordChar = '\0';
            this.ucLabelEditItemCodeQuery.ReadOnly = true;
            this.ucLabelEditItemCodeQuery.ShowCheckBox = false;
            this.ucLabelEditItemCodeQuery.Size = new System.Drawing.Size(194, 24);
            this.ucLabelEditItemCodeQuery.TabIndex = 24;
            this.ucLabelEditItemCodeQuery.TabNext = true;
            this.ucLabelEditItemCodeQuery.Value = "";
            this.ucLabelEditItemCodeQuery.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditItemCodeQuery.XAlign = 304;
            // 
            // ucLabelEditRCard
            // 
            this.ucLabelEditRCard.AllowEditOnlyChecked = true;
            this.ucLabelEditRCard.AutoSelectAll = false;
            this.ucLabelEditRCard.AutoUpper = true;
            this.ucLabelEditRCard.Caption = "产品序列号";
            this.ucLabelEditRCard.Checked = false;
            this.ucLabelEditRCard.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRCard.Location = new System.Drawing.Point(7, 85);
            this.ucLabelEditRCard.MaxLength = 40;
            this.ucLabelEditRCard.Multiline = false;
            this.ucLabelEditRCard.Name = "ucLabelEditRCard";
            this.ucLabelEditRCard.PasswordChar = '\0';
            this.ucLabelEditRCard.ReadOnly = false;
            this.ucLabelEditRCard.ShowCheckBox = false;
            this.ucLabelEditRCard.Size = new System.Drawing.Size(473, 24);
            this.ucLabelEditRCard.TabIndex = 11;
            this.ucLabelEditRCard.TabNext = true;
            this.ucLabelEditRCard.Value = "";
            this.ucLabelEditRCard.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditRCard.XAlign = 80;
            this.ucLabelEditRCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditRCard_TxtboxKeyPress);
            // 
            // ucButtonGetLot
            // 
            this.ucButtonGetLot.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonGetLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonGetLot.BackgroundImage")));
            this.ucButtonGetLot.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonGetLot.Caption = "获取批";
            this.ucButtonGetLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonGetLot.Location = new System.Drawing.Point(520, 87);
            this.ucButtonGetLot.Name = "ucButtonGetLot";
            this.ucButtonGetLot.Size = new System.Drawing.Size(88, 22);
            this.ucButtonGetLot.TabIndex = 14;
            this.ucButtonGetLot.Click += new System.EventHandler(this.ucButtonGetLot_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ucLabelEditCapacity);
            this.panel3.Controls.Add(this.ucLabelEditInputCarton);
            this.panel3.Controls.Add(this.checkBoxSelectAll);
            this.panel3.Controls.Add(this.ucBtnDelete);
            this.panel3.Controls.Add(this.ucButtonLot);
            this.panel3.Controls.Add(this.ucButtonExit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 362);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(842, 134);
            this.panel3.TabIndex = 29;
            // 
            // ucLabelEditCapacity
            // 
            this.ucLabelEditCapacity.AllowEditOnlyChecked = true;
            this.ucLabelEditCapacity.AutoSelectAll = false;
            this.ucLabelEditCapacity.AutoUpper = true;
            this.ucLabelEditCapacity.Caption = "批量";
            this.ucLabelEditCapacity.Checked = false;
            this.ucLabelEditCapacity.EditType = UserControl.EditTypes.String;
            this.ucLabelEditCapacity.Location = new System.Drawing.Point(243, 18);
            this.ucLabelEditCapacity.MaxLength = 40;
            this.ucLabelEditCapacity.Multiline = false;
            this.ucLabelEditCapacity.Name = "ucLabelEditCapacity";
            this.ucLabelEditCapacity.PasswordChar = '\0';
            this.ucLabelEditCapacity.ReadOnly = true;
            this.ucLabelEditCapacity.ShowCheckBox = false;
            this.ucLabelEditCapacity.Size = new System.Drawing.Size(237, 24);
            this.ucLabelEditCapacity.TabIndex = 21;
            this.ucLabelEditCapacity.TabNext = true;
            this.ucLabelEditCapacity.Value = "";
            this.ucLabelEditCapacity.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditCapacity.XAlign = 280;
            // 
            // ucLabelEditInputCarton
            // 
            this.ucLabelEditInputCarton.AllowEditOnlyChecked = true;
            this.ucLabelEditInputCarton.AutoSelectAll = false;
            this.ucLabelEditInputCarton.AutoUpper = true;
            this.ucLabelEditInputCarton.Caption = "输入箱号";
            this.ucLabelEditInputCarton.Checked = false;
            this.ucLabelEditInputCarton.EditType = UserControl.EditTypes.String;
            this.ucLabelEditInputCarton.Location = new System.Drawing.Point(19, 52);
            this.ucLabelEditInputCarton.MaxLength = 40;
            this.ucLabelEditInputCarton.Multiline = false;
            this.ucLabelEditInputCarton.Name = "ucLabelEditInputCarton";
            this.ucLabelEditInputCarton.PasswordChar = '\0';
            this.ucLabelEditInputCarton.ReadOnly = false;
            this.ucLabelEditInputCarton.ShowCheckBox = false;
            this.ucLabelEditInputCarton.Size = new System.Drawing.Size(461, 24);
            this.ucLabelEditInputCarton.TabIndex = 20;
            this.ucLabelEditInputCarton.TabNext = true;
            this.ucLabelEditInputCarton.Value = "";
            this.ucLabelEditInputCarton.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditInputCarton.XAlign = 80;
            this.ucLabelEditInputCarton.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditInput_TxtboxKeyPress);
            // 
            // checkBoxSelectAll
            // 
            this.checkBoxSelectAll.AutoSize = true;
            this.checkBoxSelectAll.Location = new System.Drawing.Point(33, 18);
            this.checkBoxSelectAll.Name = "checkBoxSelectAll";
            this.checkBoxSelectAll.Size = new System.Drawing.Size(48, 16);
            this.checkBoxSelectAll.TabIndex = 22;
            this.checkBoxSelectAll.Text = "全选";
            this.checkBoxSelectAll.UseVisualStyleBackColor = true;
            this.checkBoxSelectAll.CheckedChanged += new System.EventHandler(this.checkBoxSelectAll_CheckedChanged);
            // 
            // ucBtnDelete
            // 
            this.ucBtnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnDelete.BackgroundImage")));
            this.ucBtnDelete.ButtonType = UserControl.ButtonTypes.Delete;
            this.ucBtnDelete.Caption = "删除";
            this.ucBtnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnDelete.Location = new System.Drawing.Point(104, 92);
            this.ucBtnDelete.Name = "ucBtnDelete";
            this.ucBtnDelete.Size = new System.Drawing.Size(88, 22);
            this.ucBtnDelete.TabIndex = 19;
            this.ucBtnDelete.Click += new System.EventHandler(this.ucBtnDelete_Click);
            // 
            // ucButtonLot
            // 
            this.ucButtonLot.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonLot.BackgroundImage")));
            this.ucButtonLot.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonLot.Caption = "送检";
            this.ucButtonLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonLot.Location = new System.Drawing.Point(245, 92);
            this.ucButtonLot.Name = "ucButtonLot";
            this.ucButtonLot.Size = new System.Drawing.Size(88, 22);
            this.ucButtonLot.TabIndex = 18;
            this.ucButtonLot.Click += new System.EventHandler(this.ucButtonLot_Click);
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(394, 92);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 17;
            this.ucButtonExit.Click += new System.EventHandler(this.ucButtonExit_Click);
            // 
            // ultraGridLot2CardList
            // 
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGridLot2CardList.DisplayLayout.Appearance = appearance4;
            this.ultraGridLot2CardList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridLot2CardList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance1.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridLot2CardList.DisplayLayout.GroupByBox.Appearance = appearance1;
            appearance2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridLot2CardList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance2;
            this.ultraGridLot2CardList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackColor2 = System.Drawing.SystemColors.Control;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridLot2CardList.DisplayLayout.GroupByBox.PromptAppearance = appearance3;
            this.ultraGridLot2CardList.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGridLot2CardList.DisplayLayout.MaxRowScrollRegions = 1;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGridLot2CardList.DisplayLayout.Override.ActiveCellAppearance = appearance12;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGridLot2CardList.DisplayLayout.Override.ActiveRowAppearance = appearance7;
            this.ultraGridLot2CardList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGridLot2CardList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGridLot2CardList.DisplayLayout.Override.CardAreaAppearance = appearance6;
            appearance5.BorderColor = System.Drawing.Color.Silver;
            appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGridLot2CardList.DisplayLayout.Override.CellAppearance = appearance5;
            this.ultraGridLot2CardList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGridLot2CardList.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridLot2CardList.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance11.TextHAlignAsString = "Left";
            this.ultraGridLot2CardList.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.ultraGridLot2CardList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGridLot2CardList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            this.ultraGridLot2CardList.DisplayLayout.Override.RowAppearance = appearance10;
            this.ultraGridLot2CardList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGridLot2CardList.DisplayLayout.Override.TemplateAddRowAppearance = appearance8;
            this.ultraGridLot2CardList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGridLot2CardList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGridLot2CardList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridLot2CardList.Location = new System.Drawing.Point(0, 165);
            this.ultraGridLot2CardList.Name = "ultraGridLot2CardList";
            this.ultraGridLot2CardList.Size = new System.Drawing.Size(842, 197);
            this.ultraGridLot2CardList.TabIndex = 30;
            this.ultraGridLot2CardList.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMain_InitializeLayout);
            this.ultraGridLot2CardList.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridRCardList_CellChange);
            // 
            // FOQCLotQA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 496);
            this.Controls.Add(this.ultraGridLot2CardList);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "FOQCLotQA";
            this.Text = "OQC送检批维护";
            this.Load += new System.EventHandler(this.FOQCLotQA_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridLot2CardList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UserControl.UCLabelEdit ucLabelEditCartonNo;
        private UserControl.UCButton ucButtonNewLot;
        private UserControl.UCLabelEdit ucLabelEditMoCodeQuery;
        private UserControl.UCLabelEdit ucLabelEditLotNo;
        private UserControl.UCLabelEdit ucLabelEditItemCodeQuery;
        private UserControl.UCLabelEdit ucLabelEditRCard;
        private UserControl.UCButton ucButtonGetLot;
        private System.Windows.Forms.Panel panel3;
        private UserControl.UCLabelEdit ucLabelEditCapacity;
        private UserControl.UCLabelEdit ucLabelEditInputCarton;
        private System.Windows.Forms.CheckBox checkBoxSelectAll;
        private UserControl.UCButton ucBtnDelete;
        private UserControl.UCButton ucButtonLot;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCLabelEdit ucLabelEditStatus;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridLot2CardList;
    }
}