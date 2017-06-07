namespace BenQGuru.eMES.Client
{
    partial class FOQCQuery
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
            System.Windows.Forms.GroupBox groupBox1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FOQCQuery));
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.SplitContainer splitContainer1;
            this.txtCartonCode = new UserControl.UCLabelEdit();
            this.labelItemDescription = new System.Windows.Forms.Label();
            this.ucLabelEditItemCode = new UserControl.UCLabelEdit();
            this.ucButtonGetLot = new UserControl.UCButton();
            this.ucLabelEditSizeAndCapacity = new UserControl.UCLabelEdit();
            this.ucLabelEditRCard = new UserControl.UCLabelEdit();
            this.ucLabelEditLotNo = new UserControl.UCLabelEdit();
            this.ucButtonExit = new UserControl.UCButton();
            this.labelSampleNGPer = new System.Windows.Forms.Label();
            this.labelPPM = new System.Windows.Forms.Label();
            this.ultraGridHeader = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ultraGridDetail = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGridBase = new Infragistics.Win.UltraWinGrid.UltraGrid();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridHeader)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridBase)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.txtCartonCode);
            groupBox1.Controls.Add(this.labelItemDescription);
            groupBox1.Controls.Add(this.ucLabelEditItemCode);
            groupBox1.Controls.Add(this.ucButtonGetLot);
            groupBox1.Controls.Add(this.ucLabelEditSizeAndCapacity);
            groupBox1.Controls.Add(this.ucLabelEditRCard);
            groupBox1.Controls.Add(this.ucLabelEditLotNo);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(744, 138);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // txtCartonCode
            // 
            this.txtCartonCode.AllowEditOnlyChecked = true;
            this.txtCartonCode.AutoSelectAll = false;
            this.txtCartonCode.AutoUpper = true;
            this.txtCartonCode.Caption = "箱号";
            this.txtCartonCode.Checked = false;
            this.txtCartonCode.EditType = UserControl.EditTypes.String;
            this.txtCartonCode.Location = new System.Drawing.Point(74, 42);
            this.txtCartonCode.MaxLength = 40;
            this.txtCartonCode.Multiline = false;
            this.txtCartonCode.Name = "txtCartonCode";
            this.txtCartonCode.PasswordChar = '\0';
            this.txtCartonCode.ReadOnly = false;
            this.txtCartonCode.ShowCheckBox = false;
            this.txtCartonCode.Size = new System.Drawing.Size(437, 25);
            this.txtCartonCode.TabIndex = 193;
            this.txtCartonCode.TabNext = true;
            this.txtCartonCode.Value = "";
            this.txtCartonCode.WidthType = UserControl.WidthTypes.TooLong;
            this.txtCartonCode.XAlign = 111;
            this.txtCartonCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCartonCode_TxtboxKeyPress);
            // 
            // labelItemDescription
            // 
            this.labelItemDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemDescription.Location = new System.Drawing.Point(317, 102);
            this.labelItemDescription.Name = "labelItemDescription";
            this.labelItemDescription.Size = new System.Drawing.Size(406, 28);
            this.labelItemDescription.TabIndex = 11;
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
            this.ucLabelEditItemCode.Location = new System.Drawing.Point(74, 105);
            this.ucLabelEditItemCode.MaxLength = 40;
            this.ucLabelEditItemCode.Multiline = false;
            this.ucLabelEditItemCode.Name = "ucLabelEditItemCode";
            this.ucLabelEditItemCode.PasswordChar = '\0';
            this.ucLabelEditItemCode.ReadOnly = true;
            this.ucLabelEditItemCode.ShowCheckBox = false;
            this.ucLabelEditItemCode.Size = new System.Drawing.Size(237, 24);
            this.ucLabelEditItemCode.TabIndex = 10;
            this.ucLabelEditItemCode.TabNext = true;
            this.ucLabelEditItemCode.Value = "";
            this.ucLabelEditItemCode.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditItemCode.XAlign = 111;
            // 
            // ucButtonGetLot
            // 
            this.ucButtonGetLot.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonGetLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonGetLot.BackgroundImage")));
            this.ucButtonGetLot.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonGetLot.Caption = "获取批";
            this.ucButtonGetLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonGetLot.Location = new System.Drawing.Point(517, 73);
            this.ucButtonGetLot.Name = "ucButtonGetLot";
            this.ucButtonGetLot.Size = new System.Drawing.Size(88, 22);
            this.ucButtonGetLot.TabIndex = 9;
            this.ucButtonGetLot.Click += new System.EventHandler(this.ucButtonGetLot_Click);
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
            this.ucLabelEditSizeAndCapacity.Location = new System.Drawing.Point(571, 12);
            this.ucLabelEditSizeAndCapacity.MaxLength = 40;
            this.ucLabelEditSizeAndCapacity.Multiline = false;
            this.ucLabelEditSizeAndCapacity.Name = "ucLabelEditSizeAndCapacity";
            this.ucLabelEditSizeAndCapacity.PasswordChar = '\0';
            this.ucLabelEditSizeAndCapacity.ReadOnly = false;
            this.ucLabelEditSizeAndCapacity.ShowCheckBox = false;
            this.ucLabelEditSizeAndCapacity.Size = new System.Drawing.Size(161, 26);
            this.ucLabelEditSizeAndCapacity.TabIndex = 7;
            this.ucLabelEditSizeAndCapacity.TabNext = true;
            this.ucLabelEditSizeAndCapacity.Value = "";
            this.ucLabelEditSizeAndCapacity.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditSizeAndCapacity.XAlign = 632;
            // 
            // ucLabelEditRCard
            // 
            this.ucLabelEditRCard.AllowEditOnlyChecked = true;
            this.ucLabelEditRCard.AutoSelectAll = false;
            this.ucLabelEditRCard.AutoUpper = true;
            this.ucLabelEditRCard.Caption = "产品序列号";
            this.ucLabelEditRCard.Checked = false;
            this.ucLabelEditRCard.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRCard.Location = new System.Drawing.Point(38, 73);
            this.ucLabelEditRCard.MaxLength = 40;
            this.ucLabelEditRCard.Multiline = false;
            this.ucLabelEditRCard.Name = "ucLabelEditRCard";
            this.ucLabelEditRCard.PasswordChar = '\0';
            this.ucLabelEditRCard.ReadOnly = false;
            this.ucLabelEditRCard.ShowCheckBox = false;
            this.ucLabelEditRCard.Size = new System.Drawing.Size(473, 26);
            this.ucLabelEditRCard.TabIndex = 6;
            this.ucLabelEditRCard.TabNext = true;
            this.ucLabelEditRCard.Value = "";
            this.ucLabelEditRCard.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditRCard.XAlign = 111;
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
            this.ucLabelEditLotNo.Location = new System.Drawing.Point(74, 12);
            this.ucLabelEditLotNo.MaxLength = 40;
            this.ucLabelEditLotNo.Multiline = false;
            this.ucLabelEditLotNo.Name = "ucLabelEditLotNo";
            this.ucLabelEditLotNo.PasswordChar = '\0';
            this.ucLabelEditLotNo.ReadOnly = false;
            this.ucLabelEditLotNo.ShowCheckBox = false;
            this.ucLabelEditLotNo.Size = new System.Drawing.Size(437, 26);
            this.ucLabelEditLotNo.TabIndex = 5;
            this.ucLabelEditLotNo.TabNext = true;
            this.ucLabelEditLotNo.Value = "";
            this.ucLabelEditLotNo.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditLotNo.XAlign = 111;
            this.ucLabelEditLotNo.Click += new System.EventHandler(this.ucButtonGetLot_Click);
            this.ucLabelEditLotNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditLotNo_TxtboxKeyPress);
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.ucButtonExit);
            groupBox2.Controls.Add(this.labelSampleNGPer);
            groupBox2.Controls.Add(this.labelPPM);
            groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            groupBox2.Location = new System.Drawing.Point(0, 530);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(744, 65);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(635, 31);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 5;
            // 
            // labelSampleNGPer
            // 
            this.labelSampleNGPer.AutoSize = true;
            this.labelSampleNGPer.ForeColor = System.Drawing.Color.Blue;
            this.labelSampleNGPer.Location = new System.Drawing.Point(38, 43);
            this.labelSampleNGPer.Name = "labelSampleNGPer";
            this.labelSampleNGPer.Size = new System.Drawing.Size(287, 12);
            this.labelSampleNGPer.TabIndex = 1;
            this.labelSampleNGPer.Text = "缺陷抽样不合格率 ： 缺陷个数 / 样本数 * 1000000";
            // 
            // labelPPM
            // 
            this.labelPPM.AutoSize = true;
            this.labelPPM.ForeColor = System.Drawing.Color.Blue;
            this.labelPPM.Location = new System.Drawing.Point(38, 20);
            this.labelPPM.Name = "labelPPM";
            this.labelPPM.Size = new System.Drawing.Size(317, 12);
            this.labelPPM.TabIndex = 0;
            this.labelPPM.Text = "抽样不合格率（PPM） ： 不良样本数 / 样本数 * 1000000";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 138);
            splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(this.ultraGridHeader);
            splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            splitContainer1.Panel2MinSize = 100;
            splitContainer1.Size = new System.Drawing.Size(744, 392);
            splitContainer1.SplitterDistance = 194;
            splitContainer1.TabIndex = 2;
            // 
            // ultraGridHeader
            // 
            this.ultraGridHeader.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridHeader.Location = new System.Drawing.Point(0, 0);
            this.ultraGridHeader.Name = "ultraGridHeader";
            this.ultraGridHeader.Size = new System.Drawing.Size(744, 194);
            this.ultraGridHeader.TabIndex = 0;
            this.ultraGridHeader.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridHeader_InitializeLayout);
            this.ultraGridHeader.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.ultraGridHeader_BeforeRowFilterDropDownPopulate);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ultraGridDetail);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ultraGridBase);
            this.splitContainer2.Size = new System.Drawing.Size(744, 194);
            this.splitContainer2.SplitterDistance = 358;
            this.splitContainer2.TabIndex = 1;
            // 
            // ultraGridDetail
            // 
            this.ultraGridDetail.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridDetail.Location = new System.Drawing.Point(0, 0);
            this.ultraGridDetail.Name = "ultraGridDetail";
            this.ultraGridDetail.Size = new System.Drawing.Size(358, 194);
            this.ultraGridDetail.TabIndex = 0;
            this.ultraGridDetail.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridDetail_InitializeLayout);
            this.ultraGridDetail.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.ultraGridDetail_BeforeRowFilterDropDownPopulate);
            // 
            // ultraGridBase
            // 
            this.ultraGridBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridBase.Location = new System.Drawing.Point(0, 0);
            this.ultraGridBase.Name = "ultraGridBase";
            this.ultraGridBase.Size = new System.Drawing.Size(382, 194);
            this.ultraGridBase.TabIndex = 0;
            this.ultraGridBase.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridBase_InitializeLayout);
            this.ultraGridBase.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.ultraGridBase_BeforeRowFilterDropDownPopulate);
            // 
            // FOQCQuery
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(744, 595);
            this.Controls.Add(splitContainer1);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.Name = "FOQCQuery";
            this.Text = "OQC -- 查询";
            this.Load += new System.EventHandler(this.FCollectionOQCQuery_Load);
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridHeader)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridBase)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCButton ucButtonGetLot;
        private UserControl.UCLabelEdit ucLabelEditSizeAndCapacity;
        private UserControl.UCLabelEdit ucLabelEditRCard;
        private UserControl.UCLabelEdit ucLabelEditLotNo;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridHeader;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridDetail;
        private System.Windows.Forms.Label labelPPM;
        private System.Windows.Forms.Label labelSampleNGPer;
        private UserControl.UCButton ucButtonExit;
        private System.Windows.Forms.Label labelItemDescription;
        private UserControl.UCLabelEdit ucLabelEditItemCode;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridBase;
        private UserControl.UCLabelEdit txtCartonCode;



    }
}