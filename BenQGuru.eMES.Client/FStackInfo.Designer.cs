namespace BenQGuru.eMES.Client
{
    partial class FStackInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FStackInfo));
            Infragistics.Win.UltraWinDataSource.UltraDataBand ultraDataBand1 = new Infragistics.Win.UltraWinDataSource.UltraDataBand("Band1");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn1 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("ItemCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("ItemDesc");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn3 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("PalletCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn4 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Qty");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn5 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("StackCode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn6 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Capacity");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn7 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("PalleteNum");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn8 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Remain");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn9 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Percent");
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBack = new UserControl.UCButton();
            this.btnOK = new UserControl.UCButton();
            this.StoreroomInfo = new System.Windows.Forms.GroupBox();
            this.gridStorage = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.StackInfo = new System.Windows.Forms.GroupBox();
            this.txtStackCode = new UserControl.UCLabelEdit();
            this.gridStack = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.ultraDataSource2 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.groupBox1.SuspendLayout();
            this.StoreroomInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStorage)).BeginInit();
            this.StackInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBack);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 391);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(792, 53);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.Control;
            this.btnBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBack.BackgroundImage")));
            this.btnBack.ButtonType = UserControl.ButtonTypes.None;
            this.btnBack.Caption = "返回";
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.Location = new System.Drawing.Point(352, 16);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(88, 24);
            this.btnBack.TabIndex = 10;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.ButtonType = UserControl.ButtonTypes.None;
            this.btnOK.Caption = "确定";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Location = new System.Drawing.Point(229, 16);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 24);
            this.btnOK.TabIndex = 2;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // StoreroomInfo
            // 
            this.StoreroomInfo.Controls.Add(this.gridStorage);
            this.StoreroomInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.StoreroomInfo.Location = new System.Drawing.Point(0, 0);
            this.StoreroomInfo.Name = "StoreroomInfo";
            this.StoreroomInfo.Size = new System.Drawing.Size(254, 391);
            this.StoreroomInfo.TabIndex = 5;
            this.StoreroomInfo.TabStop = false;
            this.StoreroomInfo.Text = "库房信息";
            // 
            // gridStorage
            // 
            this.gridStorage.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridStorage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridStorage.Location = new System.Drawing.Point(3, 17);
            this.gridStorage.Name = "gridStorage";
            this.gridStorage.Size = new System.Drawing.Size(248, 371);
            this.gridStorage.TabIndex = 4;
            this.gridStorage.Click += new System.EventHandler(this.gridStorage_Click);
            this.gridStorage.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridStorage_InitializeLayout);
            // 
            // StackInfo
            // 
            this.StackInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StackInfo.Controls.Add(this.txtStackCode);
            this.StackInfo.Controls.Add(this.gridStack);
            this.StackInfo.Location = new System.Drawing.Point(257, 0);
            this.StackInfo.Name = "StackInfo";
            this.StackInfo.Size = new System.Drawing.Size(535, 391);
            this.StackInfo.TabIndex = 6;
            this.StackInfo.TabStop = false;
            this.StackInfo.Text = "垛位信息";
            // 
            // txtStackCode
            // 
            this.txtStackCode.AllowEditOnlyChecked = true;
            this.txtStackCode.AutoSelectAll = false;
            this.txtStackCode.AutoUpper = true;
            this.txtStackCode.Caption = "垛位";
            this.txtStackCode.Checked = false;
            this.txtStackCode.EditType = UserControl.EditTypes.String;
            this.txtStackCode.Location = new System.Drawing.Point(49, 22);
            this.txtStackCode.MaxLength = 40;
            this.txtStackCode.Multiline = false;
            this.txtStackCode.Name = "txtStackCode";
            this.txtStackCode.PasswordChar = '\0';
            this.txtStackCode.ReadOnly = false;
            this.txtStackCode.ShowCheckBox = false;
            this.txtStackCode.Size = new System.Drawing.Size(237, 26);
            this.txtStackCode.TabIndex = 1;
            this.txtStackCode.TabNext = true;
            this.txtStackCode.Value = "";
            this.txtStackCode.WidthType = UserControl.WidthTypes.Long;
            this.txtStackCode.XAlign = 86;
            this.txtStackCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStackCode_TxtboxKeyPress);
            this.txtStackCode.InnerTextChanged += new System.EventHandler(this.txtStackCode_InnerTextChanged);
            // 
            // gridStack
            // 
            this.gridStack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridStack.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridStack.Location = new System.Drawing.Point(3, 54);
            this.gridStack.Name = "gridStack";
            this.gridStack.Size = new System.Drawing.Size(529, 334);
            this.gridStack.TabIndex = 3;
            this.gridStack.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridStack_InitializeLayout);
            this.gridStack.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.gridStack_CellChange);
            // 
            // ultraDataSource2
            // 
            ultraDataBand1.Columns.AddRange(new object[] {
            ultraDataColumn1,
            ultraDataColumn2,
            ultraDataColumn3,
            ultraDataColumn4});
            this.ultraDataSource2.Band.ChildBands.AddRange(new object[] {
            ultraDataBand1});
            this.ultraDataSource2.Band.Columns.AddRange(new object[] {
            ultraDataColumn5,
            ultraDataColumn6,
            ultraDataColumn7,
            ultraDataColumn8,
            ultraDataColumn9});
            this.ultraDataSource2.Band.Key = "Band0";
            // 
            // FStackInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(792, 444);
            this.Controls.Add(this.StackInfo);
            this.Controls.Add(this.StoreroomInfo);
            this.Controls.Add(this.groupBox1);
            this.Name = "FStackInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "垛位使用情况";
            this.Load += new System.EventHandler(this.FStackInfo_Load);
            this.Activated += new System.EventHandler(this.FStackInfo_Activated);
            this.groupBox1.ResumeLayout(false);
            this.StoreroomInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridStorage)).EndInit();
            this.StackInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridStack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox StoreroomInfo;
        private System.Windows.Forms.GroupBox StackInfo;
        private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
        private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource2;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridStorage;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridStack;
        private UserControl.UCButton btnBack;
        private UserControl.UCButton btnOK;
        private UserControl.UCLabelEdit txtStackCode;

    }
}