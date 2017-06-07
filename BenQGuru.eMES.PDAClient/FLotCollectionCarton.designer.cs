namespace BenQGuru.eMES.PDAClient
{
    partial class FLotCollectionCarton// : System.Windows.Forms.Form
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
            this.ucLabelCartonCollected = new UserControl.UCLabelEdit();
            this.ucLabelItemCode = new UserControl.UCLabelEdit();
            this.ucLabelMOCode = new UserControl.UCLabelEdit();
            this.ucLabelCartonNo = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucLabelLotCodeForCarton = new UserControl.UCLabelEdit();
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
            this.groupBox1.Controls.Add(this.ucLabelCartonCollected);
            this.groupBox1.Controls.Add(this.ucLabelItemCode);
            this.groupBox1.Controls.Add(this.ucLabelMOCode);
            this.groupBox1.Controls.Add(this.ucLabelCartonNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 115);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // ucLabelCartonCollected
            // 
            this.ucLabelCartonCollected.AllowEditOnlyChecked = true;
            this.ucLabelCartonCollected.AutoSelectAll = false;
            this.ucLabelCartonCollected.AutoUpper = true;
            this.ucLabelCartonCollected.Caption = "箱已装数量";
            this.ucLabelCartonCollected.Checked = false;
            this.ucLabelCartonCollected.EditType = UserControl.EditTypes.String;
            this.ucLabelCartonCollected.Location = new System.Drawing.Point(5, 88);
            this.ucLabelCartonCollected.MaxLength = 40;
            this.ucLabelCartonCollected.Multiline = false;
            this.ucLabelCartonCollected.Name = "ucLabelCartonCollected";
            this.ucLabelCartonCollected.PasswordChar = '\0';
            this.ucLabelCartonCollected.ReadOnly = true;
            this.ucLabelCartonCollected.ShowCheckBox = false;
            this.ucLabelCartonCollected.Size = new System.Drawing.Size(273, 24);
            this.ucLabelCartonCollected.TabIndex = 12;
            this.ucLabelCartonCollected.TabNext = true;
            this.ucLabelCartonCollected.Value = "";
            this.ucLabelCartonCollected.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelCartonCollected.XAlign = 78;
            // 
            // ucLabelItemCode
            // 
            this.ucLabelItemCode.AllowEditOnlyChecked = true;
            this.ucLabelItemCode.AutoSelectAll = false;
            this.ucLabelItemCode.AutoUpper = true;
            this.ucLabelItemCode.Caption = "产品代码";
            this.ucLabelItemCode.Checked = false;
            this.ucLabelItemCode.EditType = UserControl.EditTypes.String;
            this.ucLabelItemCode.Location = new System.Drawing.Point(17, 38);
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
            this.ucLabelItemCode.XAlign = 78;
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
            this.ucLabelMOCode.Location = new System.Drawing.Point(17, 63);
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
            this.ucLabelMOCode.XAlign = 78;
            // 
            // ucLabelCartonNo
            // 
            this.ucLabelCartonNo.AllowEditOnlyChecked = true;
            this.ucLabelCartonNo.AutoSelectAll = true;
            this.ucLabelCartonNo.AutoUpper = true;
            this.ucLabelCartonNo.Caption = "箱号";
            this.ucLabelCartonNo.Checked = false;
            this.ucLabelCartonNo.EditType = UserControl.EditTypes.String;
            this.ucLabelCartonNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucLabelCartonNo.Location = new System.Drawing.Point(41, 13);
            this.ucLabelCartonNo.MaxLength = 40;
            this.ucLabelCartonNo.Multiline = false;
            this.ucLabelCartonNo.Name = "ucLabelCartonNo";
            this.ucLabelCartonNo.PasswordChar = '\0';
            this.ucLabelCartonNo.ReadOnly = false;
            this.ucLabelCartonNo.ShowCheckBox = false;
            this.ucLabelCartonNo.Size = new System.Drawing.Size(237, 24);
            this.ucLabelCartonNo.TabIndex = 0;
            this.ucLabelCartonNo.TabNext = true;
            this.ucLabelCartonNo.Value = "";
            this.ucLabelCartonNo.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelCartonNo.XAlign = 78;
            this.ucLabelCartonNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelCartonNo_TxtboxKeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucLabelLotCodeForCarton);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 229);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(294, 39);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // ucLabelLotCodeForCarton
            // 
            this.ucLabelLotCodeForCarton.AllowEditOnlyChecked = true;
            this.ucLabelLotCodeForCarton.AutoSelectAll = true;
            this.ucLabelLotCodeForCarton.AutoUpper = true;
            this.ucLabelLotCodeForCarton.Caption = "批次条码";
            this.ucLabelLotCodeForCarton.Checked = false;
            this.ucLabelLotCodeForCarton.EditType = UserControl.EditTypes.String;
            this.ucLabelLotCodeForCarton.Location = new System.Drawing.Point(16, 12);
            this.ucLabelLotCodeForCarton.MaxLength = 40;
            this.ucLabelLotCodeForCarton.Multiline = false;
            this.ucLabelLotCodeForCarton.Name = "ucLabelLotCodeForCarton";
            this.ucLabelLotCodeForCarton.PasswordChar = '\0';
            this.ucLabelLotCodeForCarton.ReadOnly = false;
            this.ucLabelLotCodeForCarton.ShowCheckBox = false;
            this.ucLabelLotCodeForCarton.Size = new System.Drawing.Size(261, 24);
            this.ucLabelLotCodeForCarton.TabIndex = 4;
            this.ucLabelLotCodeForCarton.TabNext = true;
            this.ucLabelLotCodeForCarton.Value = "";
            this.ucLabelLotCodeForCarton.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelLotCodeForCarton.XAlign = 77;
            this.ucLabelLotCodeForCarton.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelLotCodeForCarton_TxtboxKeyPress);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ultraGridDetail);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 115);
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
            // FLotCollectionCarton
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(294, 268);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FLotCollectionCarton";
            this.Text = "Carton包装批次采集";
            this.Load += new System.EventHandler(this.FLotCollectionCarton_Load);
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
        private UserControl.UCLabelEdit ucLabelCartonNo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private UserControl.UCLabelEdit ucLabelCartonCollected;
        private UserControl.UCLabelEdit ucLabelLotCodeForCarton;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridDetail;


    }
}