namespace BenQGuru.eMES.Client
{
    partial class FSoftVersionUpgrade_SEMI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSoftVersionUpgrade_SEMI));
            this.ucLabelSoftVersion = new UserControl.UCLabelEdit();
            this.ucBtnQuery = new UserControl.UCButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucLabelEditCount = new UserControl.UCLabelEdit();
            this.ultraGridUpgradeRecord = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ucLabelRcard = new UserControl.UCLabelEdit();
            this.ucButton2 = new UserControl.UCButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridUpgradeRecord)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucLabelSoftVersion
            // 
            this.ucLabelSoftVersion.AllowEditOnlyChecked = true;
            this.ucLabelSoftVersion.Caption = "软件版本";
            this.ucLabelSoftVersion.Checked = false;
            this.ucLabelSoftVersion.EditType = UserControl.EditTypes.String;
            this.ucLabelSoftVersion.Location = new System.Drawing.Point(18, 20);
            this.ucLabelSoftVersion.MaxLength = 40;
            this.ucLabelSoftVersion.Multiline = false;
            this.ucLabelSoftVersion.Name = "ucLabelSoftVersion";
            this.ucLabelSoftVersion.PasswordChar = '\0';
            this.ucLabelSoftVersion.ReadOnly = true;
            this.ucLabelSoftVersion.ShowCheckBox = false;
            this.ucLabelSoftVersion.Size = new System.Drawing.Size(261, 24);
            this.ucLabelSoftVersion.TabIndex = 0;
            this.ucLabelSoftVersion.TabNext = true;
            this.ucLabelSoftVersion.Value = "";
            this.ucLabelSoftVersion.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelSoftVersion.XAlign = 79;
            // 
            // ucBtnQuery
            // 
            this.ucBtnQuery.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnQuery.BackgroundImage")));
            this.ucBtnQuery.ButtonType = UserControl.ButtonTypes.Query;
            this.ucBtnQuery.Caption = "查询";
            this.ucBtnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnQuery.Location = new System.Drawing.Point(323, 20);
            this.ucBtnQuery.Name = "ucBtnQuery";
            this.ucBtnQuery.Size = new System.Drawing.Size(88, 22);
            this.ucBtnQuery.TabIndex = 1;
            this.ucBtnQuery.Click += new System.EventHandler(this.ucBtnQuery_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucLabelSoftVersion);
            this.groupBox1.Controls.Add(this.ucBtnQuery);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(758, 53);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择软件版本";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucLabelEditCount);
            this.groupBox2.Controls.Add(this.ultraGridUpgradeRecord);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(758, 350);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "升级记录";
            // 
            // ucLabelEditCount
            // 
            this.ucLabelEditCount.AllowEditOnlyChecked = true;
            this.ucLabelEditCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLabelEditCount.Caption = "已升级数量";
            this.ucLabelEditCount.Checked = false;
            this.ucLabelEditCount.EditType = UserControl.EditTypes.String;
            this.ucLabelEditCount.Location = new System.Drawing.Point(623, 320);
            this.ucLabelEditCount.MaxLength = 40;
            this.ucLabelEditCount.Multiline = false;
            this.ucLabelEditCount.Name = "ucLabelEditCount";
            this.ucLabelEditCount.PasswordChar = '\0';
            this.ucLabelEditCount.ReadOnly = true;
            this.ucLabelEditCount.ShowCheckBox = false;
            this.ucLabelEditCount.Size = new System.Drawing.Size(123, 24);
            this.ucLabelEditCount.TabIndex = 1;
            this.ucLabelEditCount.TabNext = true;
            this.ucLabelEditCount.Value = "";
            this.ucLabelEditCount.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditCount.XAlign = 696;
            // 
            // ultraGridUpgradeRecord
            // 
            this.ultraGridUpgradeRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGridUpgradeRecord.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridUpgradeRecord.Location = new System.Drawing.Point(6, 20);
            this.ultraGridUpgradeRecord.Name = "ultraGridUpgradeRecord";
            this.ultraGridUpgradeRecord.Size = new System.Drawing.Size(746, 285);
            this.ultraGridUpgradeRecord.TabIndex = 0;
            this.ultraGridUpgradeRecord.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridUpgradeRecord_InitializeLayout);
            // 
            // ucLabelRcard
            // 
            this.ucLabelRcard.AllowEditOnlyChecked = true;
            this.ucLabelRcard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelRcard.Caption = "产品序列号";
            this.ucLabelRcard.Checked = false;
            this.ucLabelRcard.EditType = UserControl.EditTypes.String;
            this.ucLabelRcard.Location = new System.Drawing.Point(18, 19);
            this.ucLabelRcard.MaxLength = 40;
            this.ucLabelRcard.Multiline = false;
            this.ucLabelRcard.Name = "ucLabelRcard";
            this.ucLabelRcard.PasswordChar = '\0';
            this.ucLabelRcard.ReadOnly = false;
            this.ucLabelRcard.ShowCheckBox = false;
            this.ucLabelRcard.Size = new System.Drawing.Size(473, 27);
            this.ucLabelRcard.TabIndex = 4;
            this.ucLabelRcard.TabNext = true;
            this.ucLabelRcard.Value = "";
            this.ucLabelRcard.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelRcard.XAlign = 91;
            this.ucLabelRcard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelRcard_TxtboxKeyPress);
            // 
            // ucButton2
            // 
            this.ucButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucButton2.BackColor = System.Drawing.SystemColors.Control;
            this.ucButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton2.BackgroundImage")));
            this.ucButton2.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButton2.Caption = "退出";
            this.ucButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButton2.Location = new System.Drawing.Point(658, 19);
            this.ucButton2.Name = "ucButton2";
            this.ucButton2.Size = new System.Drawing.Size(88, 22);
            this.ucButton2.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ucLabelRcard);
            this.groupBox3.Controls.Add(this.ucButton2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 403);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(758, 52);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // FSoftVersionUpgrade_SEMI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 455);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "FSoftVersionUpgrade_SEMI";
            this.Text = "半成品软件升级";
            this.Load += new System.EventHandler(this.FSoftVersionUpgrade_SEMI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridUpgradeRecord)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCLabelEdit ucLabelSoftVersion;
        private UserControl.UCButton ucBtnQuery;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridUpgradeRecord;
        private UserControl.UCLabelEdit ucLabelRcard;
        private UserControl.UCButton ucButton2;
        private System.Windows.Forms.GroupBox groupBox3;
        private UserControl.UCLabelEdit ucLabelEditCount;
    }
}