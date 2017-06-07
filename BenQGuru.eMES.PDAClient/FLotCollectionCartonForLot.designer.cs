namespace BenQGuru.eMES.PDAClient
{
    partial class FLotCollectionCartonForLot// : System.Windows.Forms.Form
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucLabelCartonCollecting = new UserControl.UCLabelEdit();
            this.ucLabelCartonCapacity = new UserControl.UCLabelEdit();
            this.ucLabelItemCode = new UserControl.UCLabelEdit();
            this.ucLabelLotCode = new UserControl.UCLabelEdit();
            this.ucLabelMOCode = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucLabelCartonNoForLot = new UserControl.UCLabelEdit();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ultraGridDetail = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucLabelCartonCollecting);
            this.groupBox1.Controls.Add(this.ucLabelCartonCapacity);
            this.groupBox1.Controls.Add(this.ucLabelItemCode);
            this.groupBox1.Controls.Add(this.ucLabelLotCode);
            this.groupBox1.Controls.Add(this.ucLabelMOCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 110);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // ucLabelCartonCollecting
            // 
            this.ucLabelCartonCollecting.AllowEditOnlyChecked = true;
            this.ucLabelCartonCollecting.AutoSelectAll = false;
            this.ucLabelCartonCollecting.AutoUpper = true;
            this.ucLabelCartonCollecting.Caption = "批次待装数量";
            this.ucLabelCartonCollecting.Checked = false;
            this.ucLabelCartonCollecting.EditType = UserControl.EditTypes.String;
            this.ucLabelCartonCollecting.Location = new System.Drawing.Point(3, 83);
            this.ucLabelCartonCollecting.MaxLength = 40;
            this.ucLabelCartonCollecting.Multiline = false;
            this.ucLabelCartonCollecting.Name = "ucLabelCartonCollecting";
            this.ucLabelCartonCollecting.PasswordChar = '\0';
            this.ucLabelCartonCollecting.ReadOnly = true;
            this.ucLabelCartonCollecting.ShowCheckBox = false;
            this.ucLabelCartonCollecting.Size = new System.Drawing.Size(285, 24);
            this.ucLabelCartonCollecting.TabIndex = 12;
            this.ucLabelCartonCollecting.TabNext = true;
            this.ucLabelCartonCollecting.Value = "";
            this.ucLabelCartonCollecting.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelCartonCollecting.XAlign = 88;
            // 
            // ucLabelCartonCapacity
            // 
            this.ucLabelCartonCapacity.AllowEditOnlyChecked = true;
            this.ucLabelCartonCapacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelCartonCapacity.AutoSelectAll = false;
            this.ucLabelCartonCapacity.AutoUpper = true;
            this.ucLabelCartonCapacity.Caption = "装箱容量";
            this.ucLabelCartonCapacity.Checked = false;
            this.ucLabelCartonCapacity.EditType = UserControl.EditTypes.String;
            this.ucLabelCartonCapacity.Location = new System.Drawing.Point(45, 83);
            this.ucLabelCartonCapacity.MaxLength = 40;
            this.ucLabelCartonCapacity.Multiline = false;
            this.ucLabelCartonCapacity.Name = "ucLabelCartonCapacity";
            this.ucLabelCartonCapacity.PasswordChar = '\0';
            this.ucLabelCartonCapacity.ReadOnly = true;
            this.ucLabelCartonCapacity.ShowCheckBox = false;
            this.ucLabelCartonCapacity.Size = new System.Drawing.Size(161, 24);
            this.ucLabelCartonCapacity.TabIndex = 11;
            this.ucLabelCartonCapacity.TabNext = true;
            this.ucLabelCartonCapacity.Value = "";
            this.ucLabelCartonCapacity.Visible = false;
            this.ucLabelCartonCapacity.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelCartonCapacity.XAlign = 106;
            // 
            // ucLabelItemCode
            // 
            this.ucLabelItemCode.AllowEditOnlyChecked = true;
            this.ucLabelItemCode.AutoSelectAll = false;
            this.ucLabelItemCode.AutoUpper = true;
            this.ucLabelItemCode.Caption = "产品代码";
            this.ucLabelItemCode.Checked = false;
            this.ucLabelItemCode.EditType = UserControl.EditTypes.String;
            this.ucLabelItemCode.Location = new System.Drawing.Point(27, 35);
            this.ucLabelItemCode.MaxLength = 40;
            this.ucLabelItemCode.Multiline = false;
            this.ucLabelItemCode.Name = "ucLabelItemCode";
            this.ucLabelItemCode.PasswordChar = '\0';
            this.ucLabelItemCode.ReadOnly = true;
            this.ucLabelItemCode.ShowCheckBox = false;
            this.ucLabelItemCode.Size = new System.Drawing.Size(261, 24);
            this.ucLabelItemCode.TabIndex = 1;
            this.ucLabelItemCode.TabNext = true;
            this.ucLabelItemCode.Value = "";
            this.ucLabelItemCode.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelItemCode.XAlign = 88;
            // 
            // ucLabelLotCode
            // 
            this.ucLabelLotCode.AllowEditOnlyChecked = true;
            this.ucLabelLotCode.AutoSelectAll = true;
            this.ucLabelLotCode.AutoUpper = true;
            this.ucLabelLotCode.Caption = "批次条码";
            this.ucLabelLotCode.Checked = false;
            this.ucLabelLotCode.EditType = UserControl.EditTypes.String;
            this.ucLabelLotCode.Location = new System.Drawing.Point(27, 11);
            this.ucLabelLotCode.MaxLength = 40;
            this.ucLabelLotCode.Multiline = false;
            this.ucLabelLotCode.Name = "ucLabelLotCode";
            this.ucLabelLotCode.PasswordChar = '\0';
            this.ucLabelLotCode.ReadOnly = false;
            this.ucLabelLotCode.ShowCheckBox = false;
            this.ucLabelLotCode.Size = new System.Drawing.Size(261, 24);
            this.ucLabelLotCode.TabIndex = 0;
            this.ucLabelLotCode.TabNext = true;
            this.ucLabelLotCode.Value = "";
            this.ucLabelLotCode.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelLotCode.XAlign = 88;
            this.ucLabelLotCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelLotCode_TxtboxKeyPress);
            // 
            // ucLabelMOCode
            // 
            this.ucLabelMOCode.AllowEditOnlyChecked = true;
            this.ucLabelMOCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelMOCode.AutoSelectAll = false;
            this.ucLabelMOCode.AutoUpper = true;
            this.ucLabelMOCode.Caption = "工单代码";
            this.ucLabelMOCode.Checked = false;
            this.ucLabelMOCode.EditType = UserControl.EditTypes.String;
            this.ucLabelMOCode.Location = new System.Drawing.Point(27, 59);
            this.ucLabelMOCode.MaxLength = 40;
            this.ucLabelMOCode.Multiline = false;
            this.ucLabelMOCode.Name = "ucLabelMOCode";
            this.ucLabelMOCode.PasswordChar = '\0';
            this.ucLabelMOCode.ReadOnly = true;
            this.ucLabelMOCode.ShowCheckBox = false;
            this.ucLabelMOCode.Size = new System.Drawing.Size(261, 24);
            this.ucLabelMOCode.TabIndex = 2;
            this.ucLabelMOCode.TabNext = true;
            this.ucLabelMOCode.Value = "";
            this.ucLabelMOCode.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelMOCode.XAlign = 88;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucLabelCartonNoForLot);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 224);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(294, 44);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // ucLabelCartonNoForLot
            // 
            this.ucLabelCartonNoForLot.AllowEditOnlyChecked = true;
            this.ucLabelCartonNoForLot.AutoSelectAll = true;
            this.ucLabelCartonNoForLot.AutoUpper = true;
            this.ucLabelCartonNoForLot.Caption = "箱号";
            this.ucLabelCartonNoForLot.Checked = false;
            this.ucLabelCartonNoForLot.EditType = UserControl.EditTypes.String;
            this.ucLabelCartonNoForLot.Location = new System.Drawing.Point(49, 14);
            this.ucLabelCartonNoForLot.MaxLength = 40;
            this.ucLabelCartonNoForLot.Multiline = false;
            this.ucLabelCartonNoForLot.Name = "ucLabelCartonNoForLot";
            this.ucLabelCartonNoForLot.PasswordChar = '\0';
            this.ucLabelCartonNoForLot.ReadOnly = false;
            this.ucLabelCartonNoForLot.ShowCheckBox = false;
            this.ucLabelCartonNoForLot.Size = new System.Drawing.Size(237, 24);
            this.ucLabelCartonNoForLot.TabIndex = 4;
            this.ucLabelCartonNoForLot.TabNext = true;
            this.ucLabelCartonNoForLot.Value = "";
            this.ucLabelCartonNoForLot.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelCartonNoForLot.XAlign = 86;
            this.ucLabelCartonNoForLot.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelCartonNoForLot_TxtboxKeyPress);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ultraGridDetail);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 110);
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
            this.ultraGridDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ultraGridDetail.Location = new System.Drawing.Point(3, 0);
            this.ultraGridDetail.Name = "ultraGridDetail";
            this.ultraGridDetail.Size = new System.Drawing.Size(288, 118);
            this.ultraGridDetail.TabIndex = 1;
            this.ultraGridDetail.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridDetail_InitializeLayout);
            // 
            // FLotCollectionCartonForLot
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(294, 268);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FLotCollectionCartonForLot";
            this.Text = "Carton包装批次采集";
            this.Load += new System.EventHandler(this.FLotCollectionCartonForLot_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCLabelEdit ucLabelItemCode;
        private UserControl.UCLabelEdit ucLabelMOCode;
        private UserControl.UCLabelEdit ucLabelCartonCapacity;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private UserControl.UCLabelEdit ucLabelCartonCollecting;
        private UserControl.UCLabelEdit ucLabelLotCode;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridDetail;
        private UserControl.UCLabelEdit ucLabelCartonNoForLot;


    }
}