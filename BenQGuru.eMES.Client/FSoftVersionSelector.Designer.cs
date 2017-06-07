namespace BenQGuru.eMES.Client
{
    partial class FSoftVersionSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSoftVersionSelector));
            this.ucLabelEditVersion = new UserControl.UCLabelEdit();
            this.ucButtonQuery = new UserControl.UCButton();
            this.ultraGridSoftVersion = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ucButtonOK = new UserControl.UCButton();
            this.ucButtonExit = new UserControl.UCButton();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridSoftVersion)).BeginInit();
            this.SuspendLayout();
            // 
            // ucLabelEditVersion
            // 
            this.ucLabelEditVersion.AllowEditOnlyChecked = true;
            this.ucLabelEditVersion.Caption = "软件版本";
            this.ucLabelEditVersion.Checked = false;
            this.ucLabelEditVersion.EditType = UserControl.EditTypes.String;
            this.ucLabelEditVersion.Location = new System.Drawing.Point(15, 12);
            this.ucLabelEditVersion.MaxLength = 40;
            this.ucLabelEditVersion.Multiline = false;
            this.ucLabelEditVersion.Name = "ucLabelEditVersion";
            this.ucLabelEditVersion.PasswordChar = '\0';
            this.ucLabelEditVersion.ReadOnly = false;
            this.ucLabelEditVersion.ShowCheckBox = false;
            this.ucLabelEditVersion.Size = new System.Drawing.Size(261, 24);
            this.ucLabelEditVersion.TabIndex = 0;
            this.ucLabelEditVersion.TabNext = true;
            this.ucLabelEditVersion.Value = "";
            this.ucLabelEditVersion.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditVersion.XAlign = 76;
            this.ucLabelEditVersion.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditVersion_TxtboxKeyPress);
            // 
            // ucButtonQuery
            // 
            this.ucButtonQuery.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonQuery.BackgroundImage")));
            this.ucButtonQuery.ButtonType = UserControl.ButtonTypes.Query;
            this.ucButtonQuery.Caption = "查询";
            this.ucButtonQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonQuery.Location = new System.Drawing.Point(301, 12);
            this.ucButtonQuery.Name = "ucButtonQuery";
            this.ucButtonQuery.Size = new System.Drawing.Size(88, 22);
            this.ucButtonQuery.TabIndex = 1;
            this.ucButtonQuery.Click += new System.EventHandler(this.ucButtonQuery_Click);
            // 
            // ultraGridSoftVersion
            // 
            this.ultraGridSoftVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGridSoftVersion.Location = new System.Drawing.Point(15, 42);
            this.ultraGridSoftVersion.Name = "ultraGridSoftVersion";
            this.ultraGridSoftVersion.Size = new System.Drawing.Size(503, 324);
            this.ultraGridSoftVersion.TabIndex = 2;
            this.ultraGridSoftVersion.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridSoftVersion_InitializeLayout);
            this.ultraGridSoftVersion.DoubleClick += new System.EventHandler(this.ultraGridSoftVersion_DoubleClick);
            // 
            // ucButtonOK
            // 
            this.ucButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButtonOK.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonOK.BackgroundImage")));
            this.ucButtonOK.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonOK.Caption = "确定";
            this.ucButtonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonOK.Location = new System.Drawing.Point(150, 372);
            this.ucButtonOK.Name = "ucButtonOK";
            this.ucButtonOK.Size = new System.Drawing.Size(88, 22);
            this.ucButtonOK.TabIndex = 3;
            this.ucButtonOK.Click += new System.EventHandler(this.ucButtonOK_Click);
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "取消";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(301, 372);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 4;
            // 
            // FSoftVersionSelector
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(530, 406);
            this.ControlBox = false;
            this.Controls.Add(this.ucButtonExit);
            this.Controls.Add(this.ucButtonOK);
            this.Controls.Add(this.ultraGridSoftVersion);
            this.Controls.Add(this.ucButtonQuery);
            this.Controls.Add(this.ucLabelEditVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FSoftVersionSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "软件版本选择";
            this.Load += new System.EventHandler(this.FSoftVersionSelector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridSoftVersion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCLabelEdit ucLabelEditVersion;
        private UserControl.UCButton ucButtonQuery;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridSoftVersion;
        private UserControl.UCButton ucButtonOK;
        private UserControl.UCButton ucButtonExit;
    }
}