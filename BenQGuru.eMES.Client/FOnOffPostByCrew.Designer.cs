namespace BenQGuru.eMES.Client
{
    partial class FOnOffPostByCrew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FOnOffPostByCrew));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucButtonCancelPause = new UserControl.UCButton();
            this.ucButtonPause = new UserControl.UCButton();
            this.ucButtonGoOffPost = new UserControl.UCButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucLabelEditCrewCode = new UserControl.UCLabelEdit();
            this.ucLabelEditShiftCode = new UserControl.UCLabelEdit();
            this.ucLabelEditBigSSCode = new UserControl.UCLabelEdit();
            this.ucButtonRefresh = new UserControl.UCButton();
            this.ucLabelEditDate = new UserControl.UCLabelEdit();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ultraGridOnPost = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridOnPost)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucButtonCancelPause);
            this.groupBox1.Controls.Add(this.ucButtonPause);
            this.groupBox1.Controls.Add(this.ucButtonGoOffPost);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 470);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(818, 53);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            // 
            // ucButtonCancelPause
            // 
            this.ucButtonCancelPause.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonCancelPause.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonCancelPause.BackgroundImage")));
            this.ucButtonCancelPause.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonCancelPause.Caption = "取消暂停";
            this.ucButtonCancelPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonCancelPause.Location = new System.Drawing.Point(589, 19);
            this.ucButtonCancelPause.Name = "ucButtonCancelPause";
            this.ucButtonCancelPause.Size = new System.Drawing.Size(88, 22);
            this.ucButtonCancelPause.TabIndex = 8;
            this.ucButtonCancelPause.Click += new System.EventHandler(this.ucButtonCancelPause_Click);
            // 
            // ucButtonPause
            // 
            this.ucButtonPause.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonPause.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonPause.BackgroundImage")));
            this.ucButtonPause.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonPause.Caption = "暂停";
            this.ucButtonPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonPause.Location = new System.Drawing.Point(359, 19);
            this.ucButtonPause.Name = "ucButtonPause";
            this.ucButtonPause.Size = new System.Drawing.Size(88, 22);
            this.ucButtonPause.TabIndex = 7;
            this.ucButtonPause.Click += new System.EventHandler(this.ucButtonPause_Click);
            // 
            // ucButtonGoOffPost
            // 
            this.ucButtonGoOffPost.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonGoOffPost.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonGoOffPost.BackgroundImage")));
            this.ucButtonGoOffPost.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonGoOffPost.Caption = "离岗";
            this.ucButtonGoOffPost.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonGoOffPost.Location = new System.Drawing.Point(124, 19);
            this.ucButtonGoOffPost.Name = "ucButtonGoOffPost";
            this.ucButtonGoOffPost.Size = new System.Drawing.Size(88, 22);
            this.ucButtonGoOffPost.TabIndex = 6;
            this.ucButtonGoOffPost.Click += new System.EventHandler(this.ucButtonGoOffPost_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox2.Controls.Add(this.ucLabelEditCrewCode);
            this.groupBox2.Controls.Add(this.ucLabelEditShiftCode);
            this.groupBox2.Controls.Add(this.ucLabelEditBigSSCode);
            this.groupBox2.Controls.Add(this.ucButtonRefresh);
            this.groupBox2.Controls.Add(this.ucLabelEditDate);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(818, 91);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            // 
            // ucLabelEditCrewCode
            // 
            this.ucLabelEditCrewCode.AllowEditOnlyChecked = true;
            this.ucLabelEditCrewCode.Caption = "班组";
            this.ucLabelEditCrewCode.Checked = false;
            this.ucLabelEditCrewCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditCrewCode.Enabled = false;
            this.ucLabelEditCrewCode.Location = new System.Drawing.Point(610, 19);
            this.ucLabelEditCrewCode.MaxLength = 40;
            this.ucLabelEditCrewCode.Multiline = false;
            this.ucLabelEditCrewCode.Name = "ucLabelEditCrewCode";
            this.ucLabelEditCrewCode.PasswordChar = '\0';
            this.ucLabelEditCrewCode.ReadOnly = false;
            this.ucLabelEditCrewCode.ShowCheckBox = false;
            this.ucLabelEditCrewCode.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditCrewCode.TabIndex = 2;
            this.ucLabelEditCrewCode.TabNext = true;
            this.ucLabelEditCrewCode.Value = "";
            this.ucLabelEditCrewCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditCrewCode.XAlign = 647;
            // 
            // ucLabelEditShiftCode
            // 
            this.ucLabelEditShiftCode.AllowEditOnlyChecked = true;
            this.ucLabelEditShiftCode.Caption = "班次";
            this.ucLabelEditShiftCode.Checked = false;
            this.ucLabelEditShiftCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditShiftCode.Enabled = false;
            this.ucLabelEditShiftCode.Location = new System.Drawing.Point(323, 19);
            this.ucLabelEditShiftCode.MaxLength = 40;
            this.ucLabelEditShiftCode.Multiline = false;
            this.ucLabelEditShiftCode.Name = "ucLabelEditShiftCode";
            this.ucLabelEditShiftCode.PasswordChar = '\0';
            this.ucLabelEditShiftCode.ReadOnly = false;
            this.ucLabelEditShiftCode.ShowCheckBox = false;
            this.ucLabelEditShiftCode.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditShiftCode.TabIndex = 1;
            this.ucLabelEditShiftCode.TabNext = true;
            this.ucLabelEditShiftCode.Value = "";
            this.ucLabelEditShiftCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditShiftCode.XAlign = 360;
            // 
            // ucLabelEditBigSSCode
            // 
            this.ucLabelEditBigSSCode.AllowEditOnlyChecked = true;
            this.ucLabelEditBigSSCode.Caption = "大线";
            this.ucLabelEditBigSSCode.Checked = false;
            this.ucLabelEditBigSSCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditBigSSCode.Enabled = false;
            this.ucLabelEditBigSSCode.Location = new System.Drawing.Point(27, 49);
            this.ucLabelEditBigSSCode.MaxLength = 40;
            this.ucLabelEditBigSSCode.Multiline = false;
            this.ucLabelEditBigSSCode.Name = "ucLabelEditBigSSCode";
            this.ucLabelEditBigSSCode.PasswordChar = '\0';
            this.ucLabelEditBigSSCode.ReadOnly = false;
            this.ucLabelEditBigSSCode.ShowCheckBox = false;
            this.ucLabelEditBigSSCode.Size = new System.Drawing.Size(237, 24);
            this.ucLabelEditBigSSCode.TabIndex = 3;
            this.ucLabelEditBigSSCode.TabNext = true;
            this.ucLabelEditBigSSCode.Value = "";
            this.ucLabelEditBigSSCode.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditBigSSCode.XAlign = 64;
            // 
            // ucButtonRefresh
            // 
            this.ucButtonRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ucButtonRefresh.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonRefresh.BackgroundImage")));
            this.ucButtonRefresh.ButtonType = UserControl.ButtonTypes.Refresh;
            this.ucButtonRefresh.Caption = "刷新";
            this.ucButtonRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonRefresh.Location = new System.Drawing.Point(667, 51);
            this.ucButtonRefresh.Name = "ucButtonRefresh";
            this.ucButtonRefresh.Size = new System.Drawing.Size(88, 22);
            this.ucButtonRefresh.TabIndex = 4;
            this.ucButtonRefresh.Click += new System.EventHandler(this.ucButtonRefresh_Click);
            // 
            // ucLabelEditDate
            // 
            this.ucLabelEditDate.AllowEditOnlyChecked = true;
            this.ucLabelEditDate.Caption = "日期";
            this.ucLabelEditDate.Checked = false;
            this.ucLabelEditDate.EditType = UserControl.EditTypes.String;
            this.ucLabelEditDate.Enabled = false;
            this.ucLabelEditDate.Location = new System.Drawing.Point(27, 19);
            this.ucLabelEditDate.MaxLength = 40;
            this.ucLabelEditDate.Multiline = false;
            this.ucLabelEditDate.Name = "ucLabelEditDate";
            this.ucLabelEditDate.PasswordChar = '\0';
            this.ucLabelEditDate.ReadOnly = false;
            this.ucLabelEditDate.ShowCheckBox = false;
            this.ucLabelEditDate.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditDate.TabIndex = 0;
            this.ucLabelEditDate.TabNext = true;
            this.ucLabelEditDate.Value = "";
            this.ucLabelEditDate.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditDate.XAlign = 64;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ultraGridOnPost);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 91);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(818, 379);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            // 
            // ultraGridOnPost
            // 
            this.ultraGridOnPost.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridOnPost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridOnPost.Location = new System.Drawing.Point(3, 16);
            this.ultraGridOnPost.Name = "ultraGridOnPost";
            this.ultraGridOnPost.Size = new System.Drawing.Size(812, 360);
            this.ultraGridOnPost.TabIndex = 5;
            this.ultraGridOnPost.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridOnPost_InitializeLayout);
            // 
            // FOnOffPostByCrew
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(818, 523);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FOnOffPostByCrew";
            this.Text = "班组上岗离岗";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FOnOffPostByCrew_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridOnPost)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControl.UCLabelEdit ucLabelEditCrewCode;
        private UserControl.UCLabelEdit ucLabelEditShiftCode;
        private UserControl.UCLabelEdit ucLabelEditBigSSCode;
        private UserControl.UCButton ucButtonRefresh;
        private UserControl.UCLabelEdit ucLabelEditDate;
        private UserControl.UCButton ucButtonCancelPause;
        private UserControl.UCButton ucButtonPause;
        private UserControl.UCButton ucButtonGoOffPost;
        private System.Windows.Forms.GroupBox groupBox3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridOnPost;
    }
}