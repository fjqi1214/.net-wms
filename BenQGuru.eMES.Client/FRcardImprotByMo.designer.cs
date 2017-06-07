namespace BenQGuru.eMES.Client
{
    partial class FRcardImprotByMo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRCardQuery));
            this.TxtMoCode = new UserControl.UCLabelEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucButtonQuery = new UserControl.UCButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxSelectedAll = new System.Windows.Forms.CheckBox();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonOK = new UserControl.UCButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ultraGridRCard = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridRCard)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtMoCode
            // 
            this.TxtMoCode.AllowEditOnlyChecked = true;
            this.TxtMoCode.AutoUpper = true;
            this.TxtMoCode.Caption = "工单号";
            this.TxtMoCode.Checked = false;
            this.TxtMoCode.EditType = UserControl.EditTypes.String;
            this.TxtMoCode.Location = new System.Drawing.Point(18, 20);
            this.TxtMoCode.MaxLength = 40;
            this.TxtMoCode.Multiline = false;
            this.TxtMoCode.Name = "TxtMoCode";
            this.TxtMoCode.PasswordChar = '\0';
            this.TxtMoCode.ReadOnly = false;
            this.TxtMoCode.ShowCheckBox = false;
            this.TxtMoCode.Size = new System.Drawing.Size(194, 24);
            this.TxtMoCode.TabIndex = 2;
            this.TxtMoCode.TabNext = true;
            this.TxtMoCode.Value = "";
            this.TxtMoCode.WidthType = UserControl.WidthTypes.Normal;
            this.TxtMoCode.XAlign = 79;
            this.TxtMoCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMoCode_TxtboxKeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucButtonQuery);
            this.groupBox1.Controls.Add(this.TxtMoCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 54);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // ucButtonQuery
            // 
            this.ucButtonQuery.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonQuery.BackgroundImage")));
            this.ucButtonQuery.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonQuery.Caption = "查询";
            this.ucButtonQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonQuery.Location = new System.Drawing.Point(337, 20);
            this.ucButtonQuery.Name = "ucButtonQuery";
            this.ucButtonQuery.Size = new System.Drawing.Size(88, 22);
            this.ucButtonQuery.TabIndex = 8;
            this.ucButtonQuery.Click += new System.EventHandler(this.ucButtonQuery_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxSelectedAll);
            this.groupBox2.Controls.Add(this.ucButtonExit);
            this.groupBox2.Controls.Add(this.ucButtonOK);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 289);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 58);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // checkBoxSelectedAll
            // 
            this.checkBoxSelectedAll.AutoSize = true;
            this.checkBoxSelectedAll.Checked = true;
            this.checkBoxSelectedAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSelectedAll.Location = new System.Drawing.Point(12, 20);
            this.checkBoxSelectedAll.Name = "checkBoxSelectedAll";
            this.checkBoxSelectedAll.Size = new System.Drawing.Size(48, 16);
            this.checkBoxSelectedAll.TabIndex = 9;
            this.checkBoxSelectedAll.Text = "全选";
            this.checkBoxSelectedAll.UseVisualStyleBackColor = true;
            this.checkBoxSelectedAll.CheckedChanged += new System.EventHandler(this.checkBoxSelectedAll_CheckedChanged);
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(274, 20);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 8;
            this.ucButtonExit.Click += new System.EventHandler(this.ucButtonExit_Click);
            // 
            // ucButtonOK
            // 
            this.ucButtonOK.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonOK.BackgroundImage")));
            this.ucButtonOK.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonOK.Caption = "导入";
            this.ucButtonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonOK.Location = new System.Drawing.Point(115, 20);
            this.ucButtonOK.Name = "ucButtonOK";
            this.ucButtonOK.Size = new System.Drawing.Size(88, 22);
            this.ucButtonOK.TabIndex = 7;
            this.ucButtonOK.Click += new System.EventHandler(this.ucButtonOK_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ultraGridRCard);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 54);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(483, 235);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // ultraGridRCard
            // 
            this.ultraGridRCard.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridRCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridRCard.Location = new System.Drawing.Point(3, 17);
            this.ultraGridRCard.Name = "ultraGridRCard";
            this.ultraGridRCard.Size = new System.Drawing.Size(477, 215);
            this.ultraGridRCard.TabIndex = 0;
            this.ultraGridRCard.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridRCard_InitializeLayout);
            // 
            // FRCardQuery
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(483, 347);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FRCardQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "序列号查询";
            this.Load += new System.EventHandler(this.FRCardQuery_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridRCard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCLabelEdit TxtMoCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridRCard;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCButton ucButtonOK;
        private UserControl.UCButton ucButtonQuery;
        private System.Windows.Forms.CheckBox checkBoxSelectedAll;
    }
}