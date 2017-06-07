namespace BenQGuru.eMES.PDAClient
{
    partial class FExecuteASN// : System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FExecuteASN));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ultraGridDetail = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.cboASNSTNo = new UserControl.UCLabelCombox();
            this.cboASNStatus = new UserControl.UCLabelCombox();
            this.btnInitial = new UserControl.UCButton();
            this.btnInitialCheck = new UserControl.UCButton();
            this.btnApplyIQC = new UserControl.UCButton();
            this.ucMessage = new UserControl.UCMessage();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboASNStatus);
            this.groupBox1.Controls.Add(this.cboASNSTNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 76);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucMessage);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 231);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(294, 37);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ultraGridDetail);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 76);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(294, 121);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            // 
            // ultraGridDetail
            // 
            this.ultraGridDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            appearance1.BorderColor = System.Drawing.Color.White;
            appearance1.ForeColor = System.Drawing.Color.White;
            appearance1.ForeColorDisabled = System.Drawing.Color.White;
            this.ultraGridDetail.DisplayLayout.AddNewBox.Appearance = appearance1;
            this.ultraGridDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridDetail.Location = new System.Drawing.Point(3, 17);
            this.ultraGridDetail.Name = "ultraGridDetail";
            this.ultraGridDetail.Size = new System.Drawing.Size(288, 101);
            this.ultraGridDetail.TabIndex = 1;
            this.ultraGridDetail.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridDetail_InitializeLayout);
            // 
            // cboASNSTNo
            // 
            this.cboASNSTNo.AllowEditOnlyChecked = true;
            this.cboASNSTNo.Caption = "入库指令号";
            this.cboASNSTNo.Checked = false;
            this.cboASNSTNo.Location = new System.Drawing.Point(16, 16);
            this.cboASNSTNo.Name = "cboASNSTNo";
            this.cboASNSTNo.SelectedIndex = -1;
            this.cboASNSTNo.ShowCheckBox = false;
            this.cboASNSTNo.Size = new System.Drawing.Size(208, 24);
            this.cboASNSTNo.TabIndex = 24;
            this.cboASNSTNo.WidthType = UserControl.WidthTypes.Normal;
            this.cboASNSTNo.XAlign = 91;
            // 
            // cboASNStatus
            // 
            this.cboASNStatus.AllowEditOnlyChecked = true;
            this.cboASNStatus.Caption = "状态";
            this.cboASNStatus.Checked = false;
            this.cboASNStatus.Location = new System.Drawing.Point(52, 47);
            this.cboASNStatus.Name = "cboASNStatus";
            this.cboASNStatus.SelectedIndex = -1;
            this.cboASNStatus.ShowCheckBox = false;
            this.cboASNStatus.Size = new System.Drawing.Size(172, 24);
            this.cboASNStatus.TabIndex = 25;
            this.cboASNStatus.WidthType = UserControl.WidthTypes.Normal;
            this.cboASNStatus.XAlign = 91;
            // 
            // btnInitial
            // 
            this.btnInitial.BackColor = System.Drawing.SystemColors.Control;
            this.btnInitial.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInitial.BackgroundImage")));
            this.btnInitial.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnInitial.Caption = "取消下发";
            this.btnInitial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInitial.Location = new System.Drawing.Point(7, 203);
            this.btnInitial.Name = "btnInitial";
            this.btnInitial.Size = new System.Drawing.Size(88, 22);
            this.btnInitial.TabIndex = 5;
            // 
            // btnInitialCheck
            // 
            this.btnInitialCheck.BackColor = System.Drawing.SystemColors.Control;
            this.btnInitialCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInitialCheck.BackgroundImage")));
            this.btnInitialCheck.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnInitialCheck.Caption = "初检";
            this.btnInitialCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInitialCheck.Location = new System.Drawing.Point(103, 203);
            this.btnInitialCheck.Name = "btnInitialCheck";
            this.btnInitialCheck.Size = new System.Drawing.Size(88, 22);
            this.btnInitialCheck.TabIndex = 6;
            // 
            // btnApplyIQC
            // 
            this.btnApplyIQC.BackColor = System.Drawing.SystemColors.Control;
            this.btnApplyIQC.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApplyIQC.BackgroundImage")));
            this.btnApplyIQC.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnApplyIQC.Caption = "申请IQC";
            this.btnApplyIQC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApplyIQC.Location = new System.Drawing.Point(199, 203);
            this.btnApplyIQC.Name = "btnApplyIQC";
            this.btnApplyIQC.Size = new System.Drawing.Size(88, 22);
            this.btnApplyIQC.TabIndex = 7;
            // 
            // ucMessage
            // 
            this.ucMessage.AutoScroll = true;
            this.ucMessage.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessage.ButtonVisible = false;
            this.ucMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessage.Location = new System.Drawing.Point(3, 17);
            this.ucMessage.Name = "ucMessage";
            this.ucMessage.Size = new System.Drawing.Size(288, 17);
            this.ucMessage.TabIndex = 176;
            // 
            // FExecuteASN
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(294, 268);
            this.Controls.Add(this.btnApplyIQC);
            this.Controls.Add(this.btnInitialCheck);
            this.Controls.Add(this.btnInitial);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FExecuteASN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "仓库执行入库指令";
            this.Load += new System.EventHandler(this.FLotCollectionCarton_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridDetail;
        private UserControl.UCLabelCombox cboASNStatus;
        private UserControl.UCLabelCombox cboASNSTNo;
        private UserControl.UCButton btnInitial;
        private UserControl.UCButton btnInitialCheck;
        private UserControl.UCButton btnApplyIQC;
        private UserControl.UCMessage ucMessage;


    }
}