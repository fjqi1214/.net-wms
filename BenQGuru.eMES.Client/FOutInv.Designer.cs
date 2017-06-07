namespace BenQGuru.eMES.Client
{
    partial class FOutInv
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FOutInv));
            this.deliveryNote = new System.Windows.Forms.GroupBox();
            this.gridDNInfo = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.txtTicketMemo = new UserControl.UCLabelEdit();
            this.txtShipToParty = new UserControl.UCLabelEdit();
            this.txtTicketNo = new UserControl.UCLabelEdit();
            this.txtLabelRelation = new UserControl.UCLabelEdit();
            this.txtDept = new UserControl.UCLabelEdit();
            this.txtLabel = new UserControl.UCLabelEdit();
            this.cboBusinessType = new UserControl.UCLabelCombox();
            this.labelFormSrc = new System.Windows.Forms.Label();
            this.ultraOptionSetERPMES = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ultraOptionSetSource = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.ultraOptionSetScanType = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.ucButtonSelectFile = new UserControl.UCButton();
            this.chkPauseCancel = new System.Windows.Forms.CheckBox();
            this.txtPauseCancelReason = new UserControl.UCLabelEdit();
            this.txtFile = new UserControl.UCLabelEdit();
            this.btnOutInv = new UserControl.UCButton();
            this.btnCancel = new UserControl.UCButton();
            this.btnImport = new UserControl.UCButton();
            this.ucLabelEdit = new UserControl.UCLabelEdit();
            this.txtMemo = new UserControl.UCLabelEdit();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControlGrids = new System.Windows.Forms.TabControl();
            this.tabPageWaitOutRCard = new System.Windows.Forms.TabPage();
            this.gridInfo = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.tabPageErrorRCard = new System.Windows.Forms.TabPage();
            this.ucButtonExp = new UserControl.UCButton();
            this.ucLabelEditErrorRunningCardQty = new UserControl.UCLabelEdit();
            this.checkBoxSelectAllError = new System.Windows.Forms.CheckBox();
            this.ucButtonClearError = new UserControl.UCButton();
            this.gridErrorInfo = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.deliveryNote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDNInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetERPMES)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetScanType)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabControlGrids.SuspendLayout();
            this.tabPageWaitOutRCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInfo)).BeginInit();
            this.tabPageErrorRCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridErrorInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // deliveryNote
            // 
            this.deliveryNote.Controls.Add(this.gridDNInfo);
            this.deliveryNote.Controls.Add(this.txtTicketMemo);
            this.deliveryNote.Controls.Add(this.txtShipToParty);
            this.deliveryNote.Controls.Add(this.txtTicketNo);
            this.deliveryNote.Controls.Add(this.txtLabelRelation);
            this.deliveryNote.Controls.Add(this.txtDept);
            this.deliveryNote.Controls.Add(this.txtLabel);
            this.deliveryNote.Controls.Add(this.cboBusinessType);
            this.deliveryNote.Controls.Add(this.labelFormSrc);
            this.deliveryNote.Controls.Add(this.ultraOptionSetERPMES);
            this.deliveryNote.Dock = System.Windows.Forms.DockStyle.Top;
            this.deliveryNote.Location = new System.Drawing.Point(0, 0);
            this.deliveryNote.Name = "deliveryNote";
            this.deliveryNote.Size = new System.Drawing.Size(792, 194);
            this.deliveryNote.TabIndex = 0;
            this.deliveryNote.TabStop = false;
            this.deliveryNote.Text = "交货单";
            // 
            // gridDNInfo
            // 
            this.gridDNInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridDNInfo.Location = new System.Drawing.Point(60, 107);
            this.gridDNInfo.Name = "gridDNInfo";
            this.gridDNInfo.Size = new System.Drawing.Size(197, 81);
            this.gridDNInfo.TabIndex = 7;
            this.gridDNInfo.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridDNInfo_InitializeLayout);
            this.gridDNInfo.Click += new System.EventHandler(this.gridDNInfo_Click);
            // 
            // txtTicketMemo
            // 
            this.txtTicketMemo.AllowEditOnlyChecked = true;
            this.txtTicketMemo.AutoSelectAll = false;
            this.txtTicketMemo.AutoUpper = true;
            this.txtTicketMemo.Caption = "备注";
            this.txtTicketMemo.Checked = false;
            this.txtTicketMemo.EditType = UserControl.EditTypes.String;
            this.txtTicketMemo.Location = new System.Drawing.Point(322, 106);
            this.txtTicketMemo.MaxLength = 40;
            this.txtTicketMemo.Multiline = true;
            this.txtTicketMemo.Name = "txtTicketMemo";
            this.txtTicketMemo.PasswordChar = '\0';
            this.txtTicketMemo.ReadOnly = true;
            this.txtTicketMemo.ShowCheckBox = false;
            this.txtTicketMemo.Size = new System.Drawing.Size(437, 83);
            this.txtTicketMemo.TabIndex = 8;
            this.txtTicketMemo.TabNext = true;
            this.txtTicketMemo.Value = "";
            this.txtTicketMemo.WidthType = UserControl.WidthTypes.TooLong;
            this.txtTicketMemo.XAlign = 359;
            // 
            // txtShipToParty
            // 
            this.txtShipToParty.AllowEditOnlyChecked = true;
            this.txtShipToParty.AutoSelectAll = false;
            this.txtShipToParty.AutoUpper = true;
            this.txtShipToParty.Caption = "送达方";
            this.txtShipToParty.Checked = false;
            this.txtShipToParty.EditType = UserControl.EditTypes.String;
            this.txtShipToParty.Location = new System.Drawing.Point(577, 76);
            this.txtShipToParty.MaxLength = 40;
            this.txtShipToParty.Multiline = false;
            this.txtShipToParty.Name = "txtShipToParty";
            this.txtShipToParty.PasswordChar = '\0';
            this.txtShipToParty.ReadOnly = true;
            this.txtShipToParty.ShowCheckBox = false;
            this.txtShipToParty.Size = new System.Drawing.Size(182, 26);
            this.txtShipToParty.TabIndex = 6;
            this.txtShipToParty.TabNext = true;
            this.txtShipToParty.Value = "";
            this.txtShipToParty.WidthType = UserControl.WidthTypes.Normal;
            this.txtShipToParty.XAlign = 626;
            // 
            // txtTicketNo
            // 
            this.txtTicketNo.AllowEditOnlyChecked = true;
            this.txtTicketNo.AutoSelectAll = false;
            this.txtTicketNo.AutoUpper = true;
            this.txtTicketNo.Caption = "交货单号";
            this.txtTicketNo.Checked = false;
            this.txtTicketNo.EditType = UserControl.EditTypes.String;
            this.txtTicketNo.Location = new System.Drawing.Point(63, 76);
            this.txtTicketNo.MaxLength = 40;
            this.txtTicketNo.Multiline = false;
            this.txtTicketNo.Name = "txtTicketNo";
            this.txtTicketNo.PasswordChar = '\0';
            this.txtTicketNo.ReadOnly = false;
            this.txtTicketNo.ShowCheckBox = false;
            this.txtTicketNo.Size = new System.Drawing.Size(194, 26);
            this.txtTicketNo.TabIndex = 4;
            this.txtTicketNo.TabNext = true;
            this.txtTicketNo.Value = "";
            this.txtTicketNo.WidthType = UserControl.WidthTypes.Normal;
            this.txtTicketNo.XAlign = 124;
            this.txtTicketNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTicketNo_TxtboxKeyPress);
            // 
            // txtLabelRelation
            // 
            this.txtLabelRelation.AllowEditOnlyChecked = true;
            this.txtLabelRelation.AutoSelectAll = false;
            this.txtLabelRelation.AutoUpper = true;
            this.txtLabelRelation.Caption = "关联单据";
            this.txtLabelRelation.Checked = false;
            this.txtLabelRelation.EditType = UserControl.EditTypes.String;
            this.txtLabelRelation.Location = new System.Drawing.Point(299, 75);
            this.txtLabelRelation.MaxLength = 40;
            this.txtLabelRelation.Multiline = false;
            this.txtLabelRelation.Name = "txtLabelRelation";
            this.txtLabelRelation.PasswordChar = '\0';
            this.txtLabelRelation.ReadOnly = true;
            this.txtLabelRelation.ShowCheckBox = false;
            this.txtLabelRelation.Size = new System.Drawing.Size(194, 26);
            this.txtLabelRelation.TabIndex = 5;
            this.txtLabelRelation.TabNext = true;
            this.txtLabelRelation.Value = "";
            this.txtLabelRelation.WidthType = UserControl.WidthTypes.Normal;
            this.txtLabelRelation.XAlign = 360;
            // 
            // txtDept
            // 
            this.txtDept.AllowEditOnlyChecked = true;
            this.txtDept.AutoSelectAll = false;
            this.txtDept.AutoUpper = true;
            this.txtDept.Caption = "部门/单位";
            this.txtDept.Checked = false;
            this.txtDept.EditType = UserControl.EditTypes.String;
            this.txtDept.Location = new System.Drawing.Point(559, 47);
            this.txtDept.MaxLength = 40;
            this.txtDept.Multiline = false;
            this.txtDept.Name = "txtDept";
            this.txtDept.PasswordChar = '\0';
            this.txtDept.ReadOnly = true;
            this.txtDept.ShowCheckBox = false;
            this.txtDept.Size = new System.Drawing.Size(200, 26);
            this.txtDept.TabIndex = 3;
            this.txtDept.TabNext = true;
            this.txtDept.Value = "";
            this.txtDept.WidthType = UserControl.WidthTypes.Normal;
            this.txtDept.XAlign = 626;
            // 
            // txtLabel
            // 
            this.txtLabel.AllowEditOnlyChecked = true;
            this.txtLabel.AutoSelectAll = false;
            this.txtLabel.AutoUpper = true;
            this.txtLabel.Caption = "订单号";
            this.txtLabel.Checked = false;
            this.txtLabel.EditType = UserControl.EditTypes.String;
            this.txtLabel.Location = new System.Drawing.Point(75, 47);
            this.txtLabel.MaxLength = 40;
            this.txtLabel.Multiline = false;
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.PasswordChar = '\0';
            this.txtLabel.ReadOnly = false;
            this.txtLabel.ShowCheckBox = false;
            this.txtLabel.Size = new System.Drawing.Size(182, 26);
            this.txtLabel.TabIndex = 1;
            this.txtLabel.TabNext = true;
            this.txtLabel.Value = "";
            this.txtLabel.WidthType = UserControl.WidthTypes.Normal;
            this.txtLabel.XAlign = 124;
            this.txtLabel.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLabel_TxtboxKeyPress);
            // 
            // cboBusinessType
            // 
            this.cboBusinessType.AllowEditOnlyChecked = true;
            this.cboBusinessType.Caption = "业务类型";
            this.cboBusinessType.Checked = false;
            this.cboBusinessType.Location = new System.Drawing.Point(299, 47);
            this.cboBusinessType.Name = "cboBusinessType";
            this.cboBusinessType.SelectedIndex = -1;
            this.cboBusinessType.ShowCheckBox = false;
            this.cboBusinessType.Size = new System.Drawing.Size(194, 22);
            this.cboBusinessType.TabIndex = 2;
            this.cboBusinessType.WidthType = UserControl.WidthTypes.Normal;
            this.cboBusinessType.XAlign = 360;
            this.cboBusinessType.SelectedIndexChanged += new System.EventHandler(this.cboBusinessType_SelectedIndexChanged);
            // 
            // labelFormSrc
            // 
            this.labelFormSrc.AutoSize = true;
            this.labelFormSrc.Location = new System.Drawing.Point(61, 23);
            this.labelFormSrc.Name = "labelFormSrc";
            this.labelFormSrc.Size = new System.Drawing.Size(53, 12);
            this.labelFormSrc.TabIndex = 17;
            this.labelFormSrc.Text = "单据来源";
            // 
            // ultraOptionSetERPMES
            // 
            this.ultraOptionSetERPMES.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            appearance1.FontData.BoldAsString = "False";
            this.ultraOptionSetERPMES.Appearance = appearance1;
            this.ultraOptionSetERPMES.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            valueListItem1.DataValue = "0";
            valueListItem1.DisplayText = "ERP";
            valueListItem2.DataValue = "1";
            valueListItem2.DisplayText = "MES";
            this.ultraOptionSetERPMES.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2});
            this.ultraOptionSetERPMES.Location = new System.Drawing.Point(124, 21);
            this.ultraOptionSetERPMES.Name = "ultraOptionSetERPMES";
            this.ultraOptionSetERPMES.Size = new System.Drawing.Size(114, 17);
            this.ultraOptionSetERPMES.TabIndex = 0;
            this.ultraOptionSetERPMES.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.ultraOptionSetERPMES.ValueChanged += new System.EventHandler(this.ultraOptionSetERPMES_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ultraOptionSetSource);
            this.groupBox3.Controls.Add(this.ultraOptionSetScanType);
            this.groupBox3.Controls.Add(this.ucButtonSelectFile);
            this.groupBox3.Controls.Add(this.chkPauseCancel);
            this.groupBox3.Controls.Add(this.txtPauseCancelReason);
            this.groupBox3.Controls.Add(this.txtFile);
            this.groupBox3.Controls.Add(this.btnOutInv);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.btnImport);
            this.groupBox3.Controls.Add(this.ucLabelEdit);
            this.groupBox3.Controls.Add(this.txtMemo);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 466);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(792, 147);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // ultraOptionSetSource
            // 
            this.ultraOptionSetSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            appearance3.FontData.BoldAsString = "False";
            this.ultraOptionSetSource.Appearance = appearance3;
            this.ultraOptionSetSource.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            valueListItem3.DataValue = "FileImport";
            valueListItem3.DisplayText = "文件导入";
            valueListItem4.DataValue = "Scan";
            valueListItem4.DisplayText = "扫描";
            this.ultraOptionSetSource.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem3,
            valueListItem4});
            this.ultraOptionSetSource.ItemSpacingVertical = 18;
            this.ultraOptionSetSource.Location = new System.Drawing.Point(12, 21);
            this.ultraOptionSetSource.Name = "ultraOptionSetSource";
            this.ultraOptionSetSource.Size = new System.Drawing.Size(70, 61);
            this.ultraOptionSetSource.TabIndex = 18;
            this.ultraOptionSetSource.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.ultraOptionSetSource.ValueChanged += new System.EventHandler(this.ultraOptionSetSource_ValueChanged);
            // 
            // ultraOptionSetScanType
            // 
            this.ultraOptionSetScanType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ultraOptionSetScanType.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            valueListItem5.DataValue = "Pallet";
            valueListItem5.DisplayText = "栈板";
            valueListItem7.DataValue = "Carton";
            valueListItem7.DisplayText = "箱子";
            valueListItem6.DataValue = "RunningCard";
            valueListItem6.DisplayText = "序列号";
            this.ultraOptionSetScanType.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem5,
            valueListItem7,
            valueListItem6});
            this.ultraOptionSetScanType.ItemSpacingHorizontal = 20;
            this.ultraOptionSetScanType.Location = new System.Drawing.Point(88, 61);
            this.ultraOptionSetScanType.Name = "ultraOptionSetScanType";
            this.ultraOptionSetScanType.Size = new System.Drawing.Size(214, 17);
            this.ultraOptionSetScanType.TabIndex = 17;
            this.ultraOptionSetScanType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // ucButtonSelectFile
            // 
            this.ucButtonSelectFile.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonSelectFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonSelectFile.BackgroundImage")));
            this.ucButtonSelectFile.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonSelectFile.Caption = "选择文件";
            this.ucButtonSelectFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonSelectFile.Location = new System.Drawing.Point(534, 24);
            this.ucButtonSelectFile.Name = "ucButtonSelectFile";
            this.ucButtonSelectFile.Size = new System.Drawing.Size(88, 24);
            this.ucButtonSelectFile.TabIndex = 16;
            this.ucButtonSelectFile.Click += new System.EventHandler(this.ucButtonSelectFile_Click);
            // 
            // chkPauseCancel
            // 
            this.chkPauseCancel.AutoSize = true;
            this.chkPauseCancel.Location = new System.Drawing.Point(14, 118);
            this.chkPauseCancel.Name = "chkPauseCancel";
            this.chkPauseCancel.Size = new System.Drawing.Size(15, 14);
            this.chkPauseCancel.TabIndex = 12;
            this.chkPauseCancel.UseVisualStyleBackColor = true;
            this.chkPauseCancel.CheckedChanged += new System.EventHandler(this.chkPauseCancel_CheckedChanged);
            // 
            // txtPauseCancelReason
            // 
            this.txtPauseCancelReason.AllowEditOnlyChecked = true;
            this.txtPauseCancelReason.AutoSelectAll = false;
            this.txtPauseCancelReason.AutoUpper = true;
            this.txtPauseCancelReason.Caption = "取消停发原因";
            this.txtPauseCancelReason.Checked = false;
            this.txtPauseCancelReason.EditType = UserControl.EditTypes.String;
            this.txtPauseCancelReason.Location = new System.Drawing.Point(40, 116);
            this.txtPauseCancelReason.MaxLength = 40;
            this.txtPauseCancelReason.Multiline = false;
            this.txtPauseCancelReason.Name = "txtPauseCancelReason";
            this.txtPauseCancelReason.PasswordChar = '\0';
            this.txtPauseCancelReason.ReadOnly = false;
            this.txtPauseCancelReason.ShowCheckBox = false;
            this.txtPauseCancelReason.Size = new System.Drawing.Size(485, 26);
            this.txtPauseCancelReason.TabIndex = 13;
            this.txtPauseCancelReason.TabNext = true;
            this.txtPauseCancelReason.Value = "";
            this.txtPauseCancelReason.WidthType = UserControl.WidthTypes.TooLong;
            this.txtPauseCancelReason.XAlign = 125;
            // 
            // txtFile
            // 
            this.txtFile.AllowEditOnlyChecked = false;
            this.txtFile.AutoSelectAll = false;
            this.txtFile.AutoUpper = true;
            this.txtFile.Caption = "文件";
            this.txtFile.Checked = false;
            this.txtFile.EditType = UserControl.EditTypes.String;
            this.txtFile.Location = new System.Drawing.Point(88, 25);
            this.txtFile.MaxLength = 200;
            this.txtFile.Multiline = false;
            this.txtFile.Name = "txtFile";
            this.txtFile.PasswordChar = '\0';
            this.txtFile.ReadOnly = true;
            this.txtFile.ShowCheckBox = false;
            this.txtFile.Size = new System.Drawing.Size(437, 26);
            this.txtFile.TabIndex = 10;
            this.txtFile.TabNext = true;
            this.txtFile.Value = "";
            this.txtFile.WidthType = UserControl.WidthTypes.TooLong;
            this.txtFile.XAlign = 125;
            // 
            // btnOutInv
            // 
            this.btnOutInv.BackColor = System.Drawing.SystemColors.Control;
            this.btnOutInv.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOutInv.BackgroundImage")));
            this.btnOutInv.ButtonType = UserControl.ButtonTypes.None;
            this.btnOutInv.Caption = "出货";
            this.btnOutInv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOutInv.Location = new System.Drawing.Point(534, 116);
            this.btnOutInv.Name = "btnOutInv";
            this.btnOutInv.Size = new System.Drawing.Size(88, 24);
            this.btnOutInv.TabIndex = 14;
            this.btnOutInv.Click += new System.EventHandler(this.btnOutInv_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.btnCancel.Caption = "取消";
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(632, 116);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 24);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.SystemColors.Control;
            this.btnImport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImport.BackgroundImage")));
            this.btnImport.ButtonType = UserControl.ButtonTypes.None;
            this.btnImport.Caption = "导入";
            this.btnImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImport.Location = new System.Drawing.Point(632, 24);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(88, 24);
            this.btnImport.TabIndex = 11;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // ucLabelEdit
            // 
            this.ucLabelEdit.AllowEditOnlyChecked = true;
            this.ucLabelEdit.AutoSelectAll = false;
            this.ucLabelEdit.AutoUpper = true;
            this.ucLabelEdit.Caption = "";
            this.ucLabelEdit.Checked = false;
            this.ucLabelEdit.EditType = UserControl.EditTypes.String;
            this.ucLabelEdit.Location = new System.Drawing.Point(301, 56);
            this.ucLabelEdit.MaxLength = 200;
            this.ucLabelEdit.Multiline = false;
            this.ucLabelEdit.Name = "ucLabelEdit";
            this.ucLabelEdit.PasswordChar = '\0';
            this.ucLabelEdit.ReadOnly = false;
            this.ucLabelEdit.ShowCheckBox = false;
            this.ucLabelEdit.Size = new System.Drawing.Size(208, 26);
            this.ucLabelEdit.TabIndex = 9;
            this.ucLabelEdit.TabNext = true;
            this.ucLabelEdit.Value = "";
            this.ucLabelEdit.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEdit.XAlign = 309;
            this.ucLabelEdit.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditInput_TxtboxKeyPress);
            // 
            // txtMemo
            // 
            this.txtMemo.AllowEditOnlyChecked = true;
            this.txtMemo.AutoSelectAll = false;
            this.txtMemo.AutoUpper = true;
            this.txtMemo.Caption = "备注";
            this.txtMemo.Checked = false;
            this.txtMemo.EditType = UserControl.EditTypes.String;
            this.txtMemo.Location = new System.Drawing.Point(88, 88);
            this.txtMemo.MaxLength = 200;
            this.txtMemo.Multiline = false;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.PasswordChar = '\0';
            this.txtMemo.ReadOnly = false;
            this.txtMemo.ShowCheckBox = false;
            this.txtMemo.Size = new System.Drawing.Size(237, 26);
            this.txtMemo.TabIndex = 9;
            this.txtMemo.TabNext = true;
            this.txtMemo.Value = "";
            this.txtMemo.WidthType = UserControl.WidthTypes.Long;
            this.txtMemo.XAlign = 125;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControlGrids);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 194);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(792, 272);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // tabControlGrids
            // 
            this.tabControlGrids.Controls.Add(this.tabPageWaitOutRCard);
            this.tabControlGrids.Controls.Add(this.tabPageErrorRCard);
            this.tabControlGrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlGrids.Location = new System.Drawing.Point(3, 17);
            this.tabControlGrids.Name = "tabControlGrids";
            this.tabControlGrids.SelectedIndex = 0;
            this.tabControlGrids.Size = new System.Drawing.Size(786, 252);
            this.tabControlGrids.TabIndex = 19;
            // 
            // tabPageWaitOutRCard
            // 
            this.tabPageWaitOutRCard.Controls.Add(this.gridInfo);
            this.tabPageWaitOutRCard.Location = new System.Drawing.Point(4, 21);
            this.tabPageWaitOutRCard.Name = "tabPageWaitOutRCard";
            this.tabPageWaitOutRCard.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWaitOutRCard.Size = new System.Drawing.Size(778, 227);
            this.tabPageWaitOutRCard.TabIndex = 0;
            this.tabPageWaitOutRCard.Text = "待出库序列号";
            this.tabPageWaitOutRCard.UseVisualStyleBackColor = true;
            // 
            // gridInfo
            // 
            this.gridInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridInfo.Location = new System.Drawing.Point(3, 3);
            this.gridInfo.Name = "gridInfo";
            this.gridInfo.Size = new System.Drawing.Size(772, 221);
            this.gridInfo.TabIndex = 0;
            this.gridInfo.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridInfo_InitializeLayout);
            // 
            // tabPageErrorRCard
            // 
            this.tabPageErrorRCard.Controls.Add(this.ucButtonExp);
            this.tabPageErrorRCard.Controls.Add(this.ucLabelEditErrorRunningCardQty);
            this.tabPageErrorRCard.Controls.Add(this.checkBoxSelectAllError);
            this.tabPageErrorRCard.Controls.Add(this.ucButtonClearError);
            this.tabPageErrorRCard.Controls.Add(this.gridErrorInfo);
            this.tabPageErrorRCard.Location = new System.Drawing.Point(4, 21);
            this.tabPageErrorRCard.Name = "tabPageErrorRCard";
            this.tabPageErrorRCard.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageErrorRCard.Size = new System.Drawing.Size(778, 227);
            this.tabPageErrorRCard.TabIndex = 1;
            this.tabPageErrorRCard.Text = "异常序列号";
            this.tabPageErrorRCard.UseVisualStyleBackColor = true;
            // 
            // ucButtonExp
            // 
            this.ucButtonExp.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucButtonExp.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExp.BackgroundImage")));
            this.ucButtonExp.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonExp.Caption = "导出";
            this.ucButtonExp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExp.Location = new System.Drawing.Point(230, 197);
            this.ucButtonExp.Name = "ucButtonExp";
            this.ucButtonExp.Size = new System.Drawing.Size(88, 24);
            this.ucButtonExp.TabIndex = 20;
            this.ucButtonExp.Click += new System.EventHandler(this.ucButtonExp_Click);
            // 
            // ucLabelEditErrorRunningCardQty
            // 
            this.ucLabelEditErrorRunningCardQty.AllowEditOnlyChecked = true;
            this.ucLabelEditErrorRunningCardQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLabelEditErrorRunningCardQty.AutoSelectAll = false;
            this.ucLabelEditErrorRunningCardQty.AutoUpper = true;
            this.ucLabelEditErrorRunningCardQty.Caption = "异常序列号个数";
            this.ucLabelEditErrorRunningCardQty.Checked = false;
            this.ucLabelEditErrorRunningCardQty.EditType = UserControl.EditTypes.String;
            this.ucLabelEditErrorRunningCardQty.Location = new System.Drawing.Point(575, 196);
            this.ucLabelEditErrorRunningCardQty.MaxLength = 200;
            this.ucLabelEditErrorRunningCardQty.Multiline = false;
            this.ucLabelEditErrorRunningCardQty.Name = "ucLabelEditErrorRunningCardQty";
            this.ucLabelEditErrorRunningCardQty.PasswordChar = '\0';
            this.ucLabelEditErrorRunningCardQty.ReadOnly = true;
            this.ucLabelEditErrorRunningCardQty.ShowCheckBox = false;
            this.ucLabelEditErrorRunningCardQty.Size = new System.Drawing.Size(197, 26);
            this.ucLabelEditErrorRunningCardQty.TabIndex = 19;
            this.ucLabelEditErrorRunningCardQty.TabNext = true;
            this.ucLabelEditErrorRunningCardQty.Value = "";
            this.ucLabelEditErrorRunningCardQty.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditErrorRunningCardQty.XAlign = 672;
            // 
            // checkBoxSelectAllError
            // 
            this.checkBoxSelectAllError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxSelectAllError.AutoSize = true;
            this.checkBoxSelectAllError.Location = new System.Drawing.Point(7, 203);
            this.checkBoxSelectAllError.Name = "checkBoxSelectAllError";
            this.checkBoxSelectAllError.Size = new System.Drawing.Size(48, 16);
            this.checkBoxSelectAllError.TabIndex = 18;
            this.checkBoxSelectAllError.Text = "全选";
            this.checkBoxSelectAllError.UseVisualStyleBackColor = true;
            this.checkBoxSelectAllError.CheckedChanged += new System.EventHandler(this.checkBoxSelectAllError_CheckedChanged);
            // 
            // ucButtonClearError
            // 
            this.ucButtonClearError.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucButtonClearError.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonClearError.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonClearError.BackgroundImage")));
            this.ucButtonClearError.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonClearError.Caption = "清除";
            this.ucButtonClearError.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonClearError.Location = new System.Drawing.Point(336, 197);
            this.ucButtonClearError.Name = "ucButtonClearError";
            this.ucButtonClearError.Size = new System.Drawing.Size(88, 24);
            this.ucButtonClearError.TabIndex = 17;
            this.ucButtonClearError.Click += new System.EventHandler(this.ucButtonClearError_Click);
            // 
            // gridErrorInfo
            // 
            this.gridErrorInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridErrorInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridErrorInfo.Location = new System.Drawing.Point(3, 3);
            this.gridErrorInfo.Name = "gridErrorInfo";
            this.gridErrorInfo.Size = new System.Drawing.Size(772, 188);
            this.gridErrorInfo.TabIndex = 1;
            this.gridErrorInfo.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridErrorInfo_InitializeLayout);
            // 
            // FOutInv
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(792, 613);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.deliveryNote);
            this.Name = "FOutInv";
            this.Text = "出库";
            this.Load += new System.EventHandler(this.FOutInv_Load);
            this.deliveryNote.ResumeLayout(false);
            this.deliveryNote.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDNInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetERPMES)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetScanType)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tabControlGrids.ResumeLayout(false);
            this.tabPageWaitOutRCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridInfo)).EndInit();
            this.tabPageErrorRCard.ResumeLayout(false);
            this.tabPageErrorRCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridErrorInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox deliveryNote;
        private System.Windows.Forms.GroupBox groupBox3;
        private UserControl.UCButton btnOutInv;
        private UserControl.UCButton btnCancel;
        private UserControl.UCButton btnImport;
        private UserControl.UCLabelEdit txtMemo;
        private System.Windows.Forms.Label labelFormSrc;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetERPMES;
        private UserControl.UCLabelEdit txtLabel;
        private UserControl.UCLabelCombox cboBusinessType;
        private UserControl.UCLabelEdit txtShipToParty;
        private UserControl.UCLabelEdit txtTicketNo;
        private UserControl.UCLabelEdit txtLabelRelation;
        private UserControl.UCLabelEdit txtDept;
        private UserControl.UCLabelEdit txtTicketMemo;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridDNInfo;
        private UserControl.UCLabelEdit txtFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox groupBox2;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridInfo;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridErrorInfo;
        private UserControl.UCLabelEdit txtPauseCancelReason;
        private System.Windows.Forms.CheckBox chkPauseCancel;
        private System.Windows.Forms.TabControl tabControlGrids;
        private System.Windows.Forms.TabPage tabPageWaitOutRCard;
        private System.Windows.Forms.TabPage tabPageErrorRCard;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetSource;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetScanType;
        private UserControl.UCButton ucButtonSelectFile;
        private UserControl.UCLabelEdit ucLabelEdit;
        private UserControl.UCLabelEdit ucLabelEditErrorRunningCardQty;
        private System.Windows.Forms.CheckBox checkBoxSelectAllError;
        private UserControl.UCButton ucButtonClearError;
        private UserControl.UCButton ucButtonExp;
    }
}