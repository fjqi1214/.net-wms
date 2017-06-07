namespace BenQGuru.eMES.Client
{
    partial class FCollectionOQC_New
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.GroupBox lotInfo;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCollectionOQC_New));
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.SplitContainer splitContainer1;
            System.Windows.Forms.Label labelSelectedNGCode;
            this.ucLabelEditCartonCode = new UserControl.UCLabelEdit();
            this.ucLabelEditSampleNgSize = new UserControl.UCLabelEdit();
            this.ucLabelEditSampleGoodSize = new UserControl.UCLabelEdit();
            this.ucLabelEditSampleSize = new UserControl.UCLabelEdit();
            this.labelItemDescription = new System.Windows.Forms.Label();
            this.ucLabelEditItemCode = new UserControl.UCLabelEdit();
            this.ucButtonPass = new UserControl.UCButton();
            this.ucButtonGetLot = new UserControl.UCButton();
            this.ucButtonRefresh = new UserControl.UCButton();
            this.ucLabelEditSizeAndCapacity = new UserControl.UCLabelEdit();
            this.ucLabelEditRCard = new UserControl.UCLabelEdit();
            this.ucLabelEditLotNo = new UserControl.UCLabelEdit();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonCancel = new UserControl.UCButton();
            this.ucButtonConfirmSamp = new UserControl.UCButton();
            this.ucLabelEditMemo = new UserControl.UCLabelEdit();
            this.ucLabelEditInput = new UserControl.UCLabelEdit();
            this.checkBoxSelectAll = new System.Windows.Forms.CheckBox();
            this.ultraGridCheckList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.listBoxSelectedErrorList = new System.Windows.Forms.ListBox();
            this.ucLabelEditErrorCode = new UserControl.UCLabelEdit();
            this.ultraGridErrorList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.timerLotSize = new System.Windows.Forms.Timer(this.components);
            lotInfo = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            labelSelectedNGCode = new System.Windows.Forms.Label();
            lotInfo.SuspendLayout();
            groupBox2.SuspendLayout();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridCheckList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridErrorList)).BeginInit();
            this.SuspendLayout();
            // 
            // lotInfo
            // 
            lotInfo.Controls.Add(this.ucLabelEditCartonCode);
            lotInfo.Controls.Add(this.ucLabelEditSampleNgSize);
            lotInfo.Controls.Add(this.ucLabelEditSampleGoodSize);
            lotInfo.Controls.Add(this.ucLabelEditSampleSize);
            lotInfo.Controls.Add(this.labelItemDescription);
            lotInfo.Controls.Add(this.ucLabelEditItemCode);
            lotInfo.Controls.Add(this.ucButtonPass);
            lotInfo.Controls.Add(this.ucButtonGetLot);
            lotInfo.Controls.Add(this.ucButtonRefresh);
            lotInfo.Controls.Add(this.ucLabelEditSizeAndCapacity);
            lotInfo.Controls.Add(this.ucLabelEditRCard);
            lotInfo.Controls.Add(this.ucLabelEditLotNo);
            lotInfo.Dock = System.Windows.Forms.DockStyle.Top;
            lotInfo.Location = new System.Drawing.Point(0, 0);
            lotInfo.Name = "lotInfo";
            lotInfo.Size = new System.Drawing.Size(1022, 149);
            lotInfo.TabIndex = 0;
            lotInfo.TabStop = false;
            lotInfo.Text = "批信息";
            // 
            // ucLabelEditCartonCode
            // 
            this.ucLabelEditCartonCode.AllowEditOnlyChecked = true;
            this.ucLabelEditCartonCode.AutoSelectAll = false;
            this.ucLabelEditCartonCode.AutoUpper = true;
            this.ucLabelEditCartonCode.Caption = "箱号";
            this.ucLabelEditCartonCode.Checked = false;
            this.ucLabelEditCartonCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditCartonCode.Location = new System.Drawing.Point(48, 50);
            this.ucLabelEditCartonCode.MaxLength = 40;
            this.ucLabelEditCartonCode.Multiline = false;
            this.ucLabelEditCartonCode.Name = "ucLabelEditCartonCode";
            this.ucLabelEditCartonCode.PasswordChar = '\0';
            this.ucLabelEditCartonCode.ReadOnly = false;
            this.ucLabelEditCartonCode.ShowCheckBox = false;
            this.ucLabelEditCartonCode.Size = new System.Drawing.Size(437, 25);
            this.ucLabelEditCartonCode.TabIndex = 193;
            this.ucLabelEditCartonCode.TabNext = true;
            this.ucLabelEditCartonCode.Value = "";
            this.ucLabelEditCartonCode.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditCartonCode.XAlign = 85;
            this.ucLabelEditCartonCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditCartonCode_TxtboxKeyPress);
            // 
            // ucLabelEditSampleNgSize
            // 
            this.ucLabelEditSampleNgSize.AllowEditOnlyChecked = true;
            this.ucLabelEditSampleNgSize.AutoSelectAll = false;
            this.ucLabelEditSampleNgSize.AutoUpper = true;
            this.ucLabelEditSampleNgSize.Caption = "不合格样本";
            this.ucLabelEditSampleNgSize.Checked = false;
            this.ucLabelEditSampleNgSize.EditType = UserControl.EditTypes.String;
            this.ucLabelEditSampleNgSize.Enabled = false;
            this.ucLabelEditSampleNgSize.Location = new System.Drawing.Point(677, 111);
            this.ucLabelEditSampleNgSize.MaxLength = 40;
            this.ucLabelEditSampleNgSize.Multiline = false;
            this.ucLabelEditSampleNgSize.Name = "ucLabelEditSampleNgSize";
            this.ucLabelEditSampleNgSize.PasswordChar = '\0';
            this.ucLabelEditSampleNgSize.ReadOnly = false;
            this.ucLabelEditSampleNgSize.ShowCheckBox = false;
            this.ucLabelEditSampleNgSize.Size = new System.Drawing.Size(123, 24);
            this.ucLabelEditSampleNgSize.TabIndex = 10;
            this.ucLabelEditSampleNgSize.TabNext = true;
            this.ucLabelEditSampleNgSize.Value = "";
            this.ucLabelEditSampleNgSize.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditSampleNgSize.XAlign = 750;
            // 
            // ucLabelEditSampleGoodSize
            // 
            this.ucLabelEditSampleGoodSize.AllowEditOnlyChecked = true;
            this.ucLabelEditSampleGoodSize.AutoSelectAll = false;
            this.ucLabelEditSampleGoodSize.AutoUpper = true;
            this.ucLabelEditSampleGoodSize.Caption = "合格样本";
            this.ucLabelEditSampleGoodSize.Checked = false;
            this.ucLabelEditSampleGoodSize.EditType = UserControl.EditTypes.String;
            this.ucLabelEditSampleGoodSize.Enabled = false;
            this.ucLabelEditSampleGoodSize.Location = new System.Drawing.Point(689, 80);
            this.ucLabelEditSampleGoodSize.MaxLength = 40;
            this.ucLabelEditSampleGoodSize.Multiline = false;
            this.ucLabelEditSampleGoodSize.Name = "ucLabelEditSampleGoodSize";
            this.ucLabelEditSampleGoodSize.PasswordChar = '\0';
            this.ucLabelEditSampleGoodSize.ReadOnly = false;
            this.ucLabelEditSampleGoodSize.ShowCheckBox = false;
            this.ucLabelEditSampleGoodSize.Size = new System.Drawing.Size(111, 24);
            this.ucLabelEditSampleGoodSize.TabIndex = 9;
            this.ucLabelEditSampleGoodSize.TabNext = true;
            this.ucLabelEditSampleGoodSize.Value = "";
            this.ucLabelEditSampleGoodSize.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditSampleGoodSize.XAlign = 750;
            // 
            // ucLabelEditSampleSize
            // 
            this.ucLabelEditSampleSize.AllowEditOnlyChecked = true;
            this.ucLabelEditSampleSize.AutoSelectAll = false;
            this.ucLabelEditSampleSize.AutoUpper = true;
            this.ucLabelEditSampleSize.Caption = "实际样本";
            this.ucLabelEditSampleSize.Checked = false;
            this.ucLabelEditSampleSize.EditType = UserControl.EditTypes.String;
            this.ucLabelEditSampleSize.Enabled = false;
            this.ucLabelEditSampleSize.Location = new System.Drawing.Point(689, 49);
            this.ucLabelEditSampleSize.MaxLength = 40;
            this.ucLabelEditSampleSize.Multiline = false;
            this.ucLabelEditSampleSize.Name = "ucLabelEditSampleSize";
            this.ucLabelEditSampleSize.PasswordChar = '\0';
            this.ucLabelEditSampleSize.ReadOnly = false;
            this.ucLabelEditSampleSize.ShowCheckBox = false;
            this.ucLabelEditSampleSize.Size = new System.Drawing.Size(111, 24);
            this.ucLabelEditSampleSize.TabIndex = 8;
            this.ucLabelEditSampleSize.TabNext = true;
            this.ucLabelEditSampleSize.Value = "";
            this.ucLabelEditSampleSize.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditSampleSize.XAlign = 750;
            // 
            // labelItemDescription
            // 
            this.labelItemDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemDescription.Location = new System.Drawing.Point(291, 117);
            this.labelItemDescription.Name = "labelItemDescription";
            this.labelItemDescription.Size = new System.Drawing.Size(719, 28);
            this.labelItemDescription.TabIndex = 7;
            this.labelItemDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucLabelEditItemCode
            // 
            this.ucLabelEditItemCode.AllowEditOnlyChecked = true;
            this.ucLabelEditItemCode.AutoSelectAll = false;
            this.ucLabelEditItemCode.AutoUpper = true;
            this.ucLabelEditItemCode.Caption = "产品";
            this.ucLabelEditItemCode.Checked = false;
            this.ucLabelEditItemCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditItemCode.Location = new System.Drawing.Point(48, 115);
            this.ucLabelEditItemCode.MaxLength = 40;
            this.ucLabelEditItemCode.Multiline = false;
            this.ucLabelEditItemCode.Name = "ucLabelEditItemCode";
            this.ucLabelEditItemCode.PasswordChar = '\0';
            this.ucLabelEditItemCode.ReadOnly = true;
            this.ucLabelEditItemCode.ShowCheckBox = false;
            this.ucLabelEditItemCode.Size = new System.Drawing.Size(237, 24);
            this.ucLabelEditItemCode.TabIndex = 6;
            this.ucLabelEditItemCode.TabNext = true;
            this.ucLabelEditItemCode.Value = "";
            this.ucLabelEditItemCode.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditItemCode.XAlign = 85;
            // 
            // ucButtonPass
            // 
            this.ucButtonPass.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonPass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonPass.BackgroundImage")));
            this.ucButtonPass.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonPass.Caption = "批判过";
            this.ucButtonPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonPass.Location = new System.Drawing.Point(585, 84);
            this.ucButtonPass.Name = "ucButtonPass";
            this.ucButtonPass.Size = new System.Drawing.Size(88, 22);
            this.ucButtonPass.TabIndex = 5;
            this.ucButtonPass.Click += new System.EventHandler(this.ucButtonPass_Click);
            // 
            // ucButtonGetLot
            // 
            this.ucButtonGetLot.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonGetLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonGetLot.BackgroundImage")));
            this.ucButtonGetLot.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonGetLot.Caption = "获取批";
            this.ucButtonGetLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonGetLot.Location = new System.Drawing.Point(491, 84);
            this.ucButtonGetLot.Name = "ucButtonGetLot";
            this.ucButtonGetLot.Size = new System.Drawing.Size(88, 22);
            this.ucButtonGetLot.TabIndex = 4;
            this.ucButtonGetLot.Click += new System.EventHandler(this.ucButtonGetLot_Click);
            // 
            // ucButtonRefresh
            // 
            this.ucButtonRefresh.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonRefresh.BackgroundImage")));
            this.ucButtonRefresh.ButtonType = UserControl.ButtonTypes.Refresh;
            this.ucButtonRefresh.Caption = "刷新";
            this.ucButtonRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonRefresh.Location = new System.Drawing.Point(491, 17);
            this.ucButtonRefresh.Name = "ucButtonRefresh";
            this.ucButtonRefresh.Size = new System.Drawing.Size(88, 22);
            this.ucButtonRefresh.TabIndex = 3;
            this.ucButtonRefresh.Click += new System.EventHandler(this.ucButtonRefresh_Click);
            // 
            // ucLabelEditSizeAndCapacity
            // 
            this.ucLabelEditSizeAndCapacity.AllowEditOnlyChecked = true;
            this.ucLabelEditSizeAndCapacity.AutoSelectAll = false;
            this.ucLabelEditSizeAndCapacity.AutoUpper = true;
            this.ucLabelEditSizeAndCapacity.Caption = "实际批量";
            this.ucLabelEditSizeAndCapacity.Checked = false;
            this.ucLabelEditSizeAndCapacity.EditType = UserControl.EditTypes.String;
            this.ucLabelEditSizeAndCapacity.Enabled = false;
            this.ucLabelEditSizeAndCapacity.Location = new System.Drawing.Point(689, 18);
            this.ucLabelEditSizeAndCapacity.MaxLength = 40;
            this.ucLabelEditSizeAndCapacity.Multiline = false;
            this.ucLabelEditSizeAndCapacity.Name = "ucLabelEditSizeAndCapacity";
            this.ucLabelEditSizeAndCapacity.PasswordChar = '\0';
            this.ucLabelEditSizeAndCapacity.ReadOnly = false;
            this.ucLabelEditSizeAndCapacity.ShowCheckBox = false;
            this.ucLabelEditSizeAndCapacity.Size = new System.Drawing.Size(111, 24);
            this.ucLabelEditSizeAndCapacity.TabIndex = 2;
            this.ucLabelEditSizeAndCapacity.TabNext = true;
            this.ucLabelEditSizeAndCapacity.Value = "";
            this.ucLabelEditSizeAndCapacity.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditSizeAndCapacity.XAlign = 750;
            // 
            // ucLabelEditRCard
            // 
            this.ucLabelEditRCard.AllowEditOnlyChecked = true;
            this.ucLabelEditRCard.AutoSelectAll = false;
            this.ucLabelEditRCard.AutoUpper = true;
            this.ucLabelEditRCard.Caption = "产品序列号";
            this.ucLabelEditRCard.Checked = false;
            this.ucLabelEditRCard.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRCard.Location = new System.Drawing.Point(12, 83);
            this.ucLabelEditRCard.MaxLength = 40;
            this.ucLabelEditRCard.Multiline = false;
            this.ucLabelEditRCard.Name = "ucLabelEditRCard";
            this.ucLabelEditRCard.PasswordChar = '\0';
            this.ucLabelEditRCard.ReadOnly = false;
            this.ucLabelEditRCard.ShowCheckBox = false;
            this.ucLabelEditRCard.Size = new System.Drawing.Size(473, 24);
            this.ucLabelEditRCard.TabIndex = 1;
            this.ucLabelEditRCard.TabNext = false;
            this.ucLabelEditRCard.Value = "";
            this.ucLabelEditRCard.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditRCard.XAlign = 85;
            this.ucLabelEditRCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditRCard_TxtboxKeyPress);
            // 
            // ucLabelEditLotNo
            // 
            this.ucLabelEditLotNo.AllowEditOnlyChecked = true;
            this.ucLabelEditLotNo.AutoSelectAll = false;
            this.ucLabelEditLotNo.AutoUpper = true;
            this.ucLabelEditLotNo.Caption = "批号";
            this.ucLabelEditLotNo.Checked = false;
            this.ucLabelEditLotNo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditLotNo.Location = new System.Drawing.Point(48, 18);
            this.ucLabelEditLotNo.MaxLength = 40;
            this.ucLabelEditLotNo.Multiline = false;
            this.ucLabelEditLotNo.Name = "ucLabelEditLotNo";
            this.ucLabelEditLotNo.PasswordChar = '\0';
            this.ucLabelEditLotNo.ReadOnly = false;
            this.ucLabelEditLotNo.ShowCheckBox = false;
            this.ucLabelEditLotNo.Size = new System.Drawing.Size(437, 24);
            this.ucLabelEditLotNo.TabIndex = 0;
            this.ucLabelEditLotNo.TabNext = false;
            this.ucLabelEditLotNo.Value = "";
            this.ucLabelEditLotNo.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditLotNo.XAlign = 85;
            this.ucLabelEditLotNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditLotNo_TxtboxKeyPress);
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.ucButtonExit);
            groupBox2.Controls.Add(this.ucButtonCancel);
            groupBox2.Controls.Add(this.ucButtonConfirmSamp);
            groupBox2.Controls.Add(this.ucLabelEditMemo);
            groupBox2.Controls.Add(this.ucLabelEditInput);
            groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            groupBox2.Location = new System.Drawing.Point(0, 545);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(1022, 78);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(667, 48);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 4;
            // 
            // ucButtonCancel
            // 
            this.ucButtonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonCancel.BackgroundImage")));
            this.ucButtonCancel.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonCancel.Caption = "取消";
            this.ucButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonCancel.Location = new System.Drawing.Point(573, 48);
            this.ucButtonCancel.Name = "ucButtonCancel";
            this.ucButtonCancel.Size = new System.Drawing.Size(88, 22);
            this.ucButtonCancel.TabIndex = 3;
            this.ucButtonCancel.Click += new System.EventHandler(this.ucButtonCancel_Click);
            // 
            // ucButtonConfirmSamp
            // 
            this.ucButtonConfirmSamp.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonConfirmSamp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonConfirmSamp.BackgroundImage")));
            this.ucButtonConfirmSamp.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonConfirmSamp.Caption = "样本确认";
            this.ucButtonConfirmSamp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonConfirmSamp.Location = new System.Drawing.Point(479, 48);
            this.ucButtonConfirmSamp.Name = "ucButtonConfirmSamp";
            this.ucButtonConfirmSamp.Size = new System.Drawing.Size(88, 22);
            this.ucButtonConfirmSamp.TabIndex = 2;
            this.ucButtonConfirmSamp.Click += new System.EventHandler(this.ucButtonConfirm_Click);
            // 
            // ucLabelEditMemo
            // 
            this.ucLabelEditMemo.AllowEditOnlyChecked = true;
            this.ucLabelEditMemo.AutoSelectAll = false;
            this.ucLabelEditMemo.AutoUpper = true;
            this.ucLabelEditMemo.Caption = "备注";
            this.ucLabelEditMemo.Checked = false;
            this.ucLabelEditMemo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditMemo.Location = new System.Drawing.Point(36, 48);
            this.ucLabelEditMemo.MaxLength = 40;
            this.ucLabelEditMemo.Multiline = false;
            this.ucLabelEditMemo.Name = "ucLabelEditMemo";
            this.ucLabelEditMemo.PasswordChar = '\0';
            this.ucLabelEditMemo.ReadOnly = false;
            this.ucLabelEditMemo.ShowCheckBox = false;
            this.ucLabelEditMemo.Size = new System.Drawing.Size(437, 24);
            this.ucLabelEditMemo.TabIndex = 1;
            this.ucLabelEditMemo.TabNext = false;
            this.ucLabelEditMemo.Value = "";
            this.ucLabelEditMemo.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditMemo.XAlign = 73;
            // 
            // ucLabelEditInput
            // 
            this.ucLabelEditInput.AllowEditOnlyChecked = true;
            this.ucLabelEditInput.AutoSelectAll = false;
            this.ucLabelEditInput.AutoUpper = true;
            this.ucLabelEditInput.Caption = "输入框";
            this.ucLabelEditInput.Checked = false;
            this.ucLabelEditInput.EditType = UserControl.EditTypes.String;
            this.ucLabelEditInput.Location = new System.Drawing.Point(24, 20);
            this.ucLabelEditInput.MaxLength = 40;
            this.ucLabelEditInput.Multiline = false;
            this.ucLabelEditInput.Name = "ucLabelEditInput";
            this.ucLabelEditInput.PasswordChar = '\0';
            this.ucLabelEditInput.ReadOnly = false;
            this.ucLabelEditInput.ShowCheckBox = false;
            this.ucLabelEditInput.Size = new System.Drawing.Size(449, 24);
            this.ucLabelEditInput.TabIndex = 0;
            this.ucLabelEditInput.TabNext = false;
            this.ucLabelEditInput.Value = "";
            this.ucLabelEditInput.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditInput.XAlign = 73;
            this.ucLabelEditInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditInput_TxtboxKeyPress);
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 149);
            splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(this.checkBoxSelectAll);
            splitContainer1.Panel1.Controls.Add(this.ultraGridCheckList);
            splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(labelSelectedNGCode);
            splitContainer1.Panel2.Controls.Add(this.listBoxSelectedErrorList);
            splitContainer1.Panel2.Controls.Add(this.ucLabelEditErrorCode);
            splitContainer1.Panel2.Controls.Add(this.ultraGridErrorList);
            splitContainer1.Panel2MinSize = 100;
            splitContainer1.Size = new System.Drawing.Size(1022, 396);
            splitContainer1.SplitterDistance = 567;
            splitContainer1.TabIndex = 2;
            // 
            // checkBoxSelectAll
            // 
            this.checkBoxSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxSelectAll.AutoSize = true;
            this.checkBoxSelectAll.Location = new System.Drawing.Point(24, 372);
            this.checkBoxSelectAll.Name = "checkBoxSelectAll";
            this.checkBoxSelectAll.Size = new System.Drawing.Size(48, 16);
            this.checkBoxSelectAll.TabIndex = 1;
            this.checkBoxSelectAll.Text = "全选";
            this.checkBoxSelectAll.UseVisualStyleBackColor = true;
            this.checkBoxSelectAll.CheckedChanged += new System.EventHandler(this.checkBoxSelectAll_CheckedChanged);
            // 
            // ultraGridCheckList
            // 
            this.ultraGridCheckList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGridCheckList.Location = new System.Drawing.Point(3, 3);
            this.ultraGridCheckList.Name = "ultraGridCheckList";
            this.ultraGridCheckList.Size = new System.Drawing.Size(561, 361);
            this.ultraGridCheckList.TabIndex = 0;
            this.ultraGridCheckList.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;
            this.ultraGridCheckList.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridCheckList_InitializeLayout);
            this.ultraGridCheckList.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridCheckList_CellChange);
            this.ultraGridCheckList.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.ultraGridCheckList_BeforeRowFilterDropDownPopulate);
            // 
            // labelSelectedNGCode
            // 
            labelSelectedNGCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            labelSelectedNGCode.AutoSize = true;
            labelSelectedNGCode.Location = new System.Drawing.Point(15, 261);
            labelSelectedNGCode.Name = "labelSelectedNGCode";
            labelSelectedNGCode.Size = new System.Drawing.Size(101, 12);
            labelSelectedNGCode.TabIndex = 3;
            labelSelectedNGCode.Text = "已选择的不良代码";
            // 
            // listBoxSelectedErrorList
            // 
            this.listBoxSelectedErrorList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSelectedErrorList.FormattingEnabled = true;
            this.listBoxSelectedErrorList.ItemHeight = 12;
            this.listBoxSelectedErrorList.Location = new System.Drawing.Point(3, 279);
            this.listBoxSelectedErrorList.Name = "listBoxSelectedErrorList";
            this.listBoxSelectedErrorList.Size = new System.Drawing.Size(445, 100);
            this.listBoxSelectedErrorList.TabIndex = 2;
            this.listBoxSelectedErrorList.DoubleClick += new System.EventHandler(this.listBoxSelectedErrorList_DoubleClick);
            // 
            // ucLabelEditErrorCode
            // 
            this.ucLabelEditErrorCode.AllowEditOnlyChecked = true;
            this.ucLabelEditErrorCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditErrorCode.AutoSelectAll = false;
            this.ucLabelEditErrorCode.AutoUpper = true;
            this.ucLabelEditErrorCode.Caption = "不良代码";
            this.ucLabelEditErrorCode.Checked = false;
            this.ucLabelEditErrorCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditErrorCode.Location = new System.Drawing.Point(15, 225);
            this.ucLabelEditErrorCode.MaxLength = 40;
            this.ucLabelEditErrorCode.Multiline = false;
            this.ucLabelEditErrorCode.Name = "ucLabelEditErrorCode";
            this.ucLabelEditErrorCode.PasswordChar = '\0';
            this.ucLabelEditErrorCode.ReadOnly = false;
            this.ucLabelEditErrorCode.ShowCheckBox = false;
            this.ucLabelEditErrorCode.Size = new System.Drawing.Size(261, 24);
            this.ucLabelEditErrorCode.TabIndex = 1;
            this.ucLabelEditErrorCode.TabNext = false;
            this.ucLabelEditErrorCode.Value = "";
            this.ucLabelEditErrorCode.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditErrorCode.XAlign = 76;
            this.ucLabelEditErrorCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditErrorCode_TxtboxKeyPress);
            // 
            // ultraGridErrorList
            // 
            this.ultraGridErrorList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGridErrorList.Location = new System.Drawing.Point(3, 3);
            this.ultraGridErrorList.Name = "ultraGridErrorList";
            this.ultraGridErrorList.Size = new System.Drawing.Size(445, 216);
            this.ultraGridErrorList.TabIndex = 0;
            this.ultraGridErrorList.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;
            this.ultraGridErrorList.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridErrorList_InitializeLayout);
            this.ultraGridErrorList.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridErrorList_CellChange);
            // 
            // timerLotSize
            // 
            this.timerLotSize.Enabled = true;
            this.timerLotSize.Interval = 60000;
            this.timerLotSize.Tick += new System.EventHandler(this.timerLotSize_Tick);
            // 
            // FCollectionOQC_New
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1022, 623);
            this.Controls.Add(splitContainer1);
            this.Controls.Add(groupBox2);
            this.Controls.Add(lotInfo);
            this.Name = "FCollectionOQC_New";
            this.Text = "OQC数据采集";
            this.Load += new System.EventHandler(this.FCollectionOQC_New_Load);
            lotInfo.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridCheckList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridErrorList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCLabelEdit ucLabelEditLotNo;
        private UserControl.UCLabelEdit ucLabelEditRCard;
        private UserControl.UCLabelEdit ucLabelEditSizeAndCapacity;
        private UserControl.UCButton ucButtonRefresh;
        private UserControl.UCButton ucButtonGetLot;
        private UserControl.UCButton ucButtonPass;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridCheckList;
        private System.Windows.Forms.CheckBox checkBoxSelectAll;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridErrorList;
        private UserControl.UCLabelEdit ucLabelEditErrorCode;
        private UserControl.UCLabelEdit ucLabelEditInput;
        private UserControl.UCLabelEdit ucLabelEditMemo;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCButton ucButtonCancel;
        private UserControl.UCButton ucButtonConfirmSamp;
        private System.Windows.Forms.Timer timerLotSize;
        private UserControl.UCLabelEdit ucLabelEditItemCode;
        private System.Windows.Forms.Label labelItemDescription;
        private System.Windows.Forms.ListBox listBoxSelectedErrorList;
        private UserControl.UCLabelEdit ucLabelEditSampleNgSize;
        private UserControl.UCLabelEdit ucLabelEditSampleGoodSize;
        private UserControl.UCLabelEdit ucLabelEditSampleSize;
        private UserControl.UCLabelEdit ucLabelEditCartonCode;

    }
}