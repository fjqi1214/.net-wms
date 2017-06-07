namespace BenQGuru.eMES.Client
{
    partial class FSMTScrapQty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSMTScrapQty));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PartNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MOCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rcard");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RelationQty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Edit");
            this.txtRCardQuery = new UserControl.UCLabelEdit();
            this.btnQuery = new UserControl.UCButton();
            this.txtMoCodeQuery = new UserControl.UCLabelEdit();
            this.endDate = new UserControl.UCDatetTime();
            this.startDate = new UserControl.UCDatetTime();
            this.txtPartNumber = new UserControl.UCLabelEdit();
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.txtRCardEdit = new UserControl.UCLabelEdit();
            this.ucButtonCancel = new UserControl.UCButton();
            this.ucBtnSave = new UserControl.UCButton();
            this.ucBtnNew = new UserControl.UCButton();
            this.txtRelationQtyEdit = new UserControl.UCLabelEdit();
            this.txtMoCodeEdit = new UserControl.UCLabelEdit();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraGroupBox5 = new Infragistics.Win.Misc.UltraGroupBox();
            this.gridList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox5)).BeginInit();
            this.ultraGroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRCardQuery
            // 
            this.txtRCardQuery.AllowEditOnlyChecked = true;
            this.txtRCardQuery.AutoSelectAll = false;
            this.txtRCardQuery.AutoUpper = true;
            this.txtRCardQuery.Caption = "产品序列号";
            this.txtRCardQuery.Checked = false;
            this.txtRCardQuery.EditType = UserControl.EditTypes.String;
            this.txtRCardQuery.Location = new System.Drawing.Point(241, 12);
            this.txtRCardQuery.MaxLength = 40;
            this.txtRCardQuery.Multiline = false;
            this.txtRCardQuery.Name = "txtRCardQuery";
            this.txtRCardQuery.PasswordChar = '\0';
            this.txtRCardQuery.ReadOnly = false;
            this.txtRCardQuery.ShowCheckBox = false;
            this.txtRCardQuery.Size = new System.Drawing.Size(206, 24);
            this.txtRCardQuery.TabIndex = 3;
            this.txtRCardQuery.TabNext = false;
            this.txtRCardQuery.Value = "";
            this.txtRCardQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtRCardQuery.XAlign = 314;
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.SystemColors.Control;
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.ButtonType = UserControl.ButtonTypes.None;
            this.btnQuery.Caption = "查询";
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.Location = new System.Drawing.Point(581, 42);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(88, 22);
            this.btnQuery.TabIndex = 11;
            this.btnQuery.TabStop = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtMoCodeQuery
            // 
            this.txtMoCodeQuery.AllowEditOnlyChecked = true;
            this.txtMoCodeQuery.AutoSelectAll = false;
            this.txtMoCodeQuery.AutoUpper = true;
            this.txtMoCodeQuery.Caption = "工单    ";
            this.txtMoCodeQuery.Checked = false;
            this.txtMoCodeQuery.EditType = UserControl.EditTypes.String;
            this.txtMoCodeQuery.Location = new System.Drawing.Point(20, 12);
            this.txtMoCodeQuery.MaxLength = 40;
            this.txtMoCodeQuery.Multiline = false;
            this.txtMoCodeQuery.Name = "txtMoCodeQuery";
            this.txtMoCodeQuery.PasswordChar = '\0';
            this.txtMoCodeQuery.ReadOnly = false;
            this.txtMoCodeQuery.ShowCheckBox = false;
            this.txtMoCodeQuery.Size = new System.Drawing.Size(194, 24);
            this.txtMoCodeQuery.TabIndex = 2;
            this.txtMoCodeQuery.TabNext = true;
            this.txtMoCodeQuery.Value = "";
            this.txtMoCodeQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtMoCodeQuery.XAlign = 81;
            // 
            // endDate
            // 
            this.endDate.Caption = "结束日期  ";
            this.endDate.Location = new System.Drawing.Point(241, 42);
            this.endDate.Name = "endDate";
            this.endDate.ShowType = UserControl.DateTimeTypes.Date;
            this.endDate.Size = new System.Drawing.Size(167, 21);
            this.endDate.TabIndex = 5;
            this.endDate.Value = new System.DateTime(2010, 10, 19, 0, 0, 0, 0);
            this.endDate.XAlign = 314;
            // 
            // startDate
            // 
            this.startDate.Caption = "开始日期";
            this.startDate.Location = new System.Drawing.Point(20, 42);
            this.startDate.Name = "startDate";
            this.startDate.ShowType = UserControl.DateTimeTypes.Date;
            this.startDate.Size = new System.Drawing.Size(153, 21);
            this.startDate.TabIndex = 4;
            this.startDate.Value = new System.DateTime(2010, 10, 19, 0, 0, 0, 0);
            this.startDate.XAlign = 81;
            // 
            // txtPartNumber
            // 
            this.txtPartNumber.AllowEditOnlyChecked = true;
            this.txtPartNumber.AutoSelectAll = false;
            this.txtPartNumber.AutoUpper = true;
            this.txtPartNumber.Caption = "料号    ";
            this.txtPartNumber.Checked = false;
            this.txtPartNumber.EditType = UserControl.EditTypes.String;
            this.txtPartNumber.Location = new System.Drawing.Point(475, 12);
            this.txtPartNumber.MaxLength = 40;
            this.txtPartNumber.Multiline = false;
            this.txtPartNumber.Name = "txtPartNumber";
            this.txtPartNumber.PasswordChar = '\0';
            this.txtPartNumber.ReadOnly = false;
            this.txtPartNumber.ShowCheckBox = false;
            this.txtPartNumber.Size = new System.Drawing.Size(194, 24);
            this.txtPartNumber.TabIndex = 1;
            this.txtPartNumber.TabNext = true;
            this.txtPartNumber.Value = "";
            this.txtPartNumber.WidthType = UserControl.WidthTypes.Normal;
            this.txtPartNumber.XAlign = 536;
            // 
            // txtRCardEdit
            // 
            this.txtRCardEdit.AllowEditOnlyChecked = true;
            this.txtRCardEdit.AutoSelectAll = false;
            this.txtRCardEdit.AutoUpper = true;
            this.txtRCardEdit.Caption = "产品序列号";
            this.txtRCardEdit.Checked = false;
            this.txtRCardEdit.EditType = UserControl.EditTypes.String;
            this.txtRCardEdit.Location = new System.Drawing.Point(241, 16);
            this.txtRCardEdit.MaxLength = 40;
            this.txtRCardEdit.Multiline = false;
            this.txtRCardEdit.Name = "txtRCardEdit";
            this.txtRCardEdit.PasswordChar = '\0';
            this.txtRCardEdit.ReadOnly = false;
            this.txtRCardEdit.ShowCheckBox = false;
            this.txtRCardEdit.Size = new System.Drawing.Size(206, 24);
            this.txtRCardEdit.TabIndex = 7;
            this.txtRCardEdit.TabNext = false;
            this.txtRCardEdit.Value = "";
            this.txtRCardEdit.WidthType = UserControl.WidthTypes.Normal;
            this.txtRCardEdit.XAlign = 314;
            this.txtRCardEdit.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRCardEdit_TxtboxKeyPress);
            // 
            // ucButtonCancel
            // 
            this.ucButtonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonCancel.BackgroundImage")));
            this.ucButtonCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.ucButtonCancel.Caption = "取消";
            this.ucButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonCancel.Location = new System.Drawing.Point(495, 46);
            this.ucButtonCancel.Name = "ucButtonCancel";
            this.ucButtonCancel.Size = new System.Drawing.Size(88, 22);
            this.ucButtonCancel.TabIndex = 14;
            this.ucButtonCancel.Click += new System.EventHandler(this.ucButtonCancel_Click);
            // 
            // ucBtnSave
            // 
            this.ucBtnSave.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnSave.BackgroundImage")));
            this.ucBtnSave.ButtonType = UserControl.ButtonTypes.Save;
            this.ucBtnSave.Caption = "保存";
            this.ucBtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnSave.Location = new System.Drawing.Point(333, 46);
            this.ucBtnSave.Name = "ucBtnSave";
            this.ucBtnSave.Size = new System.Drawing.Size(88, 22);
            this.ucBtnSave.TabIndex = 12;
            this.ucBtnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ucBtnNew
            // 
            this.ucBtnNew.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnNew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnNew.BackgroundImage")));
            this.ucBtnNew.ButtonType = UserControl.ButtonTypes.Add;
            this.ucBtnNew.Caption = "新增";
            this.ucBtnNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnNew.Location = new System.Drawing.Point(171, 46);
            this.ucBtnNew.Name = "ucBtnNew";
            this.ucBtnNew.Size = new System.Drawing.Size(88, 22);
            this.ucBtnNew.TabIndex = 13;
            this.ucBtnNew.Click += new System.EventHandler(this.ucBtnNew_Click);
            // 
            // txtRelationQtyEdit
            // 
            this.txtRelationQtyEdit.AllowEditOnlyChecked = true;
            this.txtRelationQtyEdit.AutoSelectAll = false;
            this.txtRelationQtyEdit.AutoUpper = true;
            this.txtRelationQtyEdit.Caption = "拼板比例";
            this.txtRelationQtyEdit.Checked = false;
            this.txtRelationQtyEdit.EditType = UserControl.EditTypes.Integer;
            this.txtRelationQtyEdit.Location = new System.Drawing.Point(475, 16);
            this.txtRelationQtyEdit.MaxLength = 40;
            this.txtRelationQtyEdit.Multiline = false;
            this.txtRelationQtyEdit.Name = "txtRelationQtyEdit";
            this.txtRelationQtyEdit.PasswordChar = '\0';
            this.txtRelationQtyEdit.ReadOnly = false;
            this.txtRelationQtyEdit.ShowCheckBox = false;
            this.txtRelationQtyEdit.Size = new System.Drawing.Size(194, 24);
            this.txtRelationQtyEdit.TabIndex = 8;
            this.txtRelationQtyEdit.TabNext = false;
            this.txtRelationQtyEdit.Value = "";
            this.txtRelationQtyEdit.WidthType = UserControl.WidthTypes.Normal;
            this.txtRelationQtyEdit.XAlign = 536;
            this.txtRelationQtyEdit.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRelationQtyEdit__TxtboxKeyPress);
            // 
            // txtMoCodeEdit
            // 
            this.txtMoCodeEdit.AllowEditOnlyChecked = true;
            this.txtMoCodeEdit.AutoSelectAll = false;
            this.txtMoCodeEdit.AutoUpper = true;
            this.txtMoCodeEdit.Caption = "工单    ";
            this.txtMoCodeEdit.Checked = false;
            this.txtMoCodeEdit.EditType = UserControl.EditTypes.String;
            this.txtMoCodeEdit.Location = new System.Drawing.Point(20, 16);
            this.txtMoCodeEdit.MaxLength = 40;
            this.txtMoCodeEdit.Multiline = false;
            this.txtMoCodeEdit.Name = "txtMoCodeEdit";
            this.txtMoCodeEdit.PasswordChar = '\0';
            this.txtMoCodeEdit.ReadOnly = false;
            this.txtMoCodeEdit.ShowCheckBox = false;
            this.txtMoCodeEdit.Size = new System.Drawing.Size(194, 24);
            this.txtMoCodeEdit.TabIndex = 6;
            this.txtMoCodeEdit.TabNext = true;
            this.txtMoCodeEdit.Value = "";
            this.txtMoCodeEdit.WidthType = UserControl.WidthTypes.Normal;
            this.txtMoCodeEdit.XAlign = 81;
            this.txtMoCodeEdit.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMoCodeEdit_TxtboxKeyPress);
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Controls.Add(this.ultraGroupBox5);
            this.ultraGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(755, 437);
            this.ultraGroupBox2.TabIndex = 10;
            // 
            // ultraGroupBox5
            // 
            this.ultraGroupBox5.Controls.Add(this.gridList);
            this.ultraGroupBox5.Controls.Add(this.groupBox2);
            this.ultraGroupBox5.Controls.Add(this.groupBox1);
            this.ultraGroupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox5.Location = new System.Drawing.Point(3, 0);
            this.ultraGroupBox5.Name = "ultraGroupBox5";
            this.ultraGroupBox5.Size = new System.Drawing.Size(749, 434);
            this.ultraGroupBox5.TabIndex = 6;
            // 
            // gridList
            // 
            this.gridList.Cursor = System.Windows.Forms.Cursors.Hand;
            ultraGridColumn1.Header.Caption = "料号";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn2.Header.Caption = "工单";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn3.Header.Caption = "产品序列号";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn4.Header.Caption = "拼板比例";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn5.Header.Caption = "状态";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn6.Header.Caption = "维护时间";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 135;
            ultraGridColumn7.Header.Caption = "编辑";
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            ultraGridColumn7.Width = 60;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7});
            this.gridList.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.gridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridList.Location = new System.Drawing.Point(3, 82);
            this.gridList.Name = "gridList";
            this.gridList.Size = new System.Drawing.Size(743, 231);
            this.gridList.TabIndex = 10;
            this.gridList.ClickCellButton += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.gridList_ClickCellButton);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRCardEdit);
            this.groupBox2.Controls.Add(this.ucButtonCancel);
            this.groupBox2.Controls.Add(this.ucBtnSave);
            this.groupBox2.Controls.Add(this.ucBtnNew);
            this.groupBox2.Controls.Add(this.txtRelationQtyEdit);
            this.groupBox2.Controls.Add(this.txtMoCodeEdit);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 313);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(743, 118);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMoCodeQuery);
            this.groupBox1.Controls.Add(this.txtRCardQuery);
            this.groupBox1.Controls.Add(this.txtPartNumber);
            this.groupBox1.Controls.Add(this.startDate);
            this.groupBox1.Controls.Add(this.endDate);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(743, 82);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // FSMTScrapQty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 437);
            this.Controls.Add(this.ultraGroupBox2);
            this.Name = "FSMTScrapQty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SMT打X板比例采集";
            this.Load += new System.EventHandler(this.FKeyPartFrontLoad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox5)).EndInit();
            this.ultraGroupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCLabelEdit txtPartNumber;
        private UserControl.UCDatetTime endDate;
        private UserControl.UCDatetTime startDate;
        private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
        private UserControl.UCLabelEdit txtMoCodeQuery;
        private UserControl.UCButton btnQuery;
        private UserControl.UCLabelEdit txtRelationQtyEdit;
        private UserControl.UCLabelEdit txtMoCodeEdit;
        private UserControl.UCButton ucButtonCancel;
        private UserControl.UCButton ucBtnSave;
        private UserControl.UCButton ucBtnNew;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox5;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridList;
        private UserControl.UCLabelEdit txtRCardQuery;
        private UserControl.UCLabelEdit txtRCardEdit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}