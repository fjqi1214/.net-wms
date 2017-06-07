namespace BenQGuru.eMES.Client
{
    partial class FSoftVersionUpgrade_FG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSoftVersionUpgrade_FG));
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.GroupBox groupBox3;
            System.Windows.Forms.GroupBox groupBox4;
            this.ucButtonQuery = new UserControl.UCButton();
            this.ucLabelEditSoftVersion = new UserControl.UCLabelEdit();
            this.ultraGridMaterialList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.labelItemDesc = new System.Windows.Forms.Label();
            this.ucLabelEditRCardFG = new UserControl.UCLabelEdit();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucLabelEditRCard = new UserControl.UCLabelEdit();
            this.ultraGridUpgradeLog = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ucLabelEditCount = new UserControl.UCLabelEdit();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMaterialList)).BeginInit();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridUpgradeLog)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.ucButtonQuery);
            groupBox1.Controls.Add(this.ucLabelEditSoftVersion);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(760, 57);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "选择软件版本";
            // 
            // ucButtonQuery
            // 
            this.ucButtonQuery.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonQuery.BackgroundImage")));
            this.ucButtonQuery.ButtonType = UserControl.ButtonTypes.Query;
            this.ucButtonQuery.Caption = "查询";
            this.ucButtonQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonQuery.Location = new System.Drawing.Point(309, 20);
            this.ucButtonQuery.Name = "ucButtonQuery";
            this.ucButtonQuery.Size = new System.Drawing.Size(88, 22);
            this.ucButtonQuery.TabIndex = 1;
            this.ucButtonQuery.Click += new System.EventHandler(this.ucButtonQuery_Click);
            // 
            // ucLabelEditSoftVersion
            // 
            this.ucLabelEditSoftVersion.AllowEditOnlyChecked = true;
            this.ucLabelEditSoftVersion.Caption = "软件版本";
            this.ucLabelEditSoftVersion.Checked = false;
            this.ucLabelEditSoftVersion.EditType = UserControl.EditTypes.String;
            this.ucLabelEditSoftVersion.Location = new System.Drawing.Point(24, 20);
            this.ucLabelEditSoftVersion.MaxLength = 40;
            this.ucLabelEditSoftVersion.Multiline = false;
            this.ucLabelEditSoftVersion.Name = "ucLabelEditSoftVersion";
            this.ucLabelEditSoftVersion.PasswordChar = '\0';
            this.ucLabelEditSoftVersion.ReadOnly = true;
            this.ucLabelEditSoftVersion.ShowCheckBox = false;
            this.ucLabelEditSoftVersion.Size = new System.Drawing.Size(261, 24);
            this.ucLabelEditSoftVersion.TabIndex = 0;
            this.ucLabelEditSoftVersion.TabNext = true;
            this.ucLabelEditSoftVersion.Value = "";
            this.ucLabelEditSoftVersion.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditSoftVersion.XAlign = 85;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.ultraGridMaterialList);
            groupBox2.Controls.Add(this.labelItemDesc);
            groupBox2.Controls.Add(this.ucLabelEditRCardFG);
            groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            groupBox2.Location = new System.Drawing.Point(0, 57);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(760, 262);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "选择要升级的半成品";
            // 
            // ultraGridMaterialList
            // 
            this.ultraGridMaterialList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGridMaterialList.Location = new System.Drawing.Point(13, 48);
            this.ultraGridMaterialList.Name = "ultraGridMaterialList";
            this.ultraGridMaterialList.Size = new System.Drawing.Size(735, 208);
            this.ultraGridMaterialList.TabIndex = 2;
            this.ultraGridMaterialList.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMaterialList_InitializeLayout);
            this.ultraGridMaterialList.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridMaterialList_CellChange);
            // 
            // labelItemDesc
            // 
            this.labelItemDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemDesc.Location = new System.Drawing.Point(292, 17);
            this.labelItemDesc.Name = "labelItemDesc";
            this.labelItemDesc.Size = new System.Drawing.Size(456, 28);
            this.labelItemDesc.TabIndex = 1;
            this.labelItemDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucLabelEditRCardFG
            // 
            this.ucLabelEditRCardFG.AllowEditOnlyChecked = true;
            this.ucLabelEditRCardFG.Caption = "整机序列号";
            this.ucLabelEditRCardFG.Checked = false;
            this.ucLabelEditRCardFG.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRCardFG.Location = new System.Drawing.Point(12, 20);
            this.ucLabelEditRCardFG.MaxLength = 40;
            this.ucLabelEditRCardFG.Multiline = false;
            this.ucLabelEditRCardFG.Name = "ucLabelEditRCardFG";
            this.ucLabelEditRCardFG.PasswordChar = '\0';
            this.ucLabelEditRCardFG.ReadOnly = false;
            this.ucLabelEditRCardFG.ShowCheckBox = false;
            this.ucLabelEditRCardFG.Size = new System.Drawing.Size(273, 24);
            this.ucLabelEditRCardFG.TabIndex = 0;
            this.ucLabelEditRCardFG.TabNext = true;
            this.ucLabelEditRCardFG.Value = "";
            this.ucLabelEditRCardFG.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditRCardFG.XAlign = 85;
            this.ucLabelEditRCardFG.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditRCardFG_TxtboxKeyPress);
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(this.ucButtonExit);
            groupBox3.Controls.Add(this.ucLabelEditRCard);
            groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            groupBox3.Location = new System.Drawing.Point(0, 566);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(760, 57);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(660, 20);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 1;
            // 
            // ucLabelEditRCard
            // 
            this.ucLabelEditRCard.AllowEditOnlyChecked = true;
            this.ucLabelEditRCard.Caption = "产品序列号";
            this.ucLabelEditRCard.Checked = false;
            this.ucLabelEditRCard.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRCard.Location = new System.Drawing.Point(24, 21);
            this.ucLabelEditRCard.MaxLength = 40;
            this.ucLabelEditRCard.Multiline = false;
            this.ucLabelEditRCard.Name = "ucLabelEditRCard";
            this.ucLabelEditRCard.PasswordChar = '\0';
            this.ucLabelEditRCard.ReadOnly = false;
            this.ucLabelEditRCard.ShowCheckBox = false;
            this.ucLabelEditRCard.Size = new System.Drawing.Size(473, 24);
            this.ucLabelEditRCard.TabIndex = 0;
            this.ucLabelEditRCard.TabNext = true;
            this.ucLabelEditRCard.Value = "";
            this.ucLabelEditRCard.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditRCard.XAlign = 97;
            this.ucLabelEditRCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditRCard_TxtboxKeyPress);
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(this.ultraGridUpgradeLog);
            groupBox4.Controls.Add(this.ucLabelEditCount);
            groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox4.Location = new System.Drawing.Point(0, 319);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(760, 247);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "升级记录";
            // 
            // ultraGridUpgradeLog
            // 
            this.ultraGridUpgradeLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGridUpgradeLog.Location = new System.Drawing.Point(12, 20);
            this.ultraGridUpgradeLog.Name = "ultraGridUpgradeLog";
            this.ultraGridUpgradeLog.Size = new System.Drawing.Size(736, 191);
            this.ultraGridUpgradeLog.TabIndex = 1;
            this.ultraGridUpgradeLog.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridUpgradeLog_InitializeLayout);
            // 
            // ucLabelEditCount
            // 
            this.ucLabelEditCount.AllowEditOnlyChecked = true;
            this.ucLabelEditCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLabelEditCount.Caption = "已升级数量";
            this.ucLabelEditCount.Checked = false;
            this.ucLabelEditCount.EditType = UserControl.EditTypes.String;
            this.ucLabelEditCount.Location = new System.Drawing.Point(625, 217);
            this.ucLabelEditCount.MaxLength = 40;
            this.ucLabelEditCount.Multiline = false;
            this.ucLabelEditCount.Name = "ucLabelEditCount";
            this.ucLabelEditCount.PasswordChar = '\0';
            this.ucLabelEditCount.ReadOnly = true;
            this.ucLabelEditCount.ShowCheckBox = false;
            this.ucLabelEditCount.Size = new System.Drawing.Size(123, 24);
            this.ucLabelEditCount.TabIndex = 0;
            this.ucLabelEditCount.TabNext = true;
            this.ucLabelEditCount.Value = "";
            this.ucLabelEditCount.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditCount.XAlign = 698;
            // 
            // FSoftVersionUpgrade_FG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(760, 623);
            this.Controls.Add(groupBox4);
            this.Controls.Add(groupBox3);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.Name = "FSoftVersionUpgrade_FG";
            this.Text = "整机软件版本升级";
            this.Load += new System.EventHandler(this.FSoftVersionUpgrade_FG_Load);
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMaterialList)).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridUpgradeLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCLabelEdit ucLabelEditSoftVersion;
        private UserControl.UCButton ucButtonQuery;
        private UserControl.UCLabelEdit ucLabelEditRCard;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCLabelEdit ucLabelEditRCardFG;
        private System.Windows.Forms.Label labelItemDesc;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMaterialList;
        private UserControl.UCLabelEdit ucLabelEditCount;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridUpgradeLog;
    }
}