namespace BenQGuru.eMES.Client
{
    partial class FLotCollectionCartonForLot
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
            this.ucLabelItemName = new UserControl.UCLabelEdit();
            this.chkLotCodeFChar = new UserControl.UCLabelEdit();
            this.ucLabelItemCode = new UserControl.UCLabelEdit();
            this.chkLotCodeLen = new UserControl.UCLabelEdit();
            this.ucLabelLotCode = new UserControl.UCLabelEdit();
            this.ucLabelMOCode = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkCartonFChar = new UserControl.UCLabelEdit();
            this.chkCartonLen = new UserControl.UCLabelEdit();
            this.ucLabelCartonNoForLot = new UserControl.UCLabelEdit();
            this.DisplayMessage = new System.Windows.Forms.GroupBox();
            this.ucMessage = new UserControl.UCMessage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ultraGridDetail = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.DisplayMessage.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucLabelCartonCollecting);
            this.groupBox1.Controls.Add(this.ucLabelCartonCapacity);
            this.groupBox1.Controls.Add(this.ucLabelItemName);
            this.groupBox1.Controls.Add(this.chkLotCodeFChar);
            this.groupBox1.Controls.Add(this.ucLabelItemCode);
            this.groupBox1.Controls.Add(this.chkLotCodeLen);
            this.groupBox1.Controls.Add(this.ucLabelLotCode);
            this.groupBox1.Controls.Add(this.ucLabelMOCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(886, 118);
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
            this.ucLabelCartonCollecting.Location = new System.Drawing.Point(606, 78);
            this.ucLabelCartonCollecting.MaxLength = 40;
            this.ucLabelCartonCollecting.Multiline = false;
            this.ucLabelCartonCollecting.Name = "ucLabelCartonCollecting";
            this.ucLabelCartonCollecting.PasswordChar = '\0';
            this.ucLabelCartonCollecting.ReadOnly = true;
            this.ucLabelCartonCollecting.ShowCheckBox = false;
            this.ucLabelCartonCollecting.Size = new System.Drawing.Size(185, 24);
            this.ucLabelCartonCollecting.TabIndex = 12;
            this.ucLabelCartonCollecting.TabNext = true;
            this.ucLabelCartonCollecting.Value = "";
            this.ucLabelCartonCollecting.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelCartonCollecting.XAlign = 691;
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
            this.ucLabelCartonCapacity.Location = new System.Drawing.Point(330, 78);
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
            this.ucLabelCartonCapacity.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelCartonCapacity.XAlign = 391;
            // 
            // ucLabelItemName
            // 
            this.ucLabelItemName.AllowEditOnlyChecked = true;
            this.ucLabelItemName.AutoSelectAll = false;
            this.ucLabelItemName.AutoUpper = true;
            this.ucLabelItemName.Caption = "产品名称";
            this.ucLabelItemName.Checked = false;
            this.ucLabelItemName.EditType = UserControl.EditTypes.String;
            this.ucLabelItemName.Location = new System.Drawing.Point(330, 50);
            this.ucLabelItemName.MaxLength = 40;
            this.ucLabelItemName.Multiline = false;
            this.ucLabelItemName.Name = "ucLabelItemName";
            this.ucLabelItemName.PasswordChar = '\0';
            this.ucLabelItemName.ReadOnly = true;
            this.ucLabelItemName.ShowCheckBox = false;
            this.ucLabelItemName.Size = new System.Drawing.Size(461, 24);
            this.ucLabelItemName.TabIndex = 10;
            this.ucLabelItemName.TabNext = true;
            this.ucLabelItemName.Value = "";
            this.ucLabelItemName.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelItemName.XAlign = 391;
            // 
            // chkLotCodeFChar
            // 
            this.chkLotCodeFChar.AllowEditOnlyChecked = true;
            this.chkLotCodeFChar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkLotCodeFChar.AutoSelectAll = false;
            this.chkLotCodeFChar.AutoUpper = true;
            this.chkLotCodeFChar.Caption = "批次首字符串";
            this.chkLotCodeFChar.Checked = false;
            this.chkLotCodeFChar.EditType = UserControl.EditTypes.String;
            this.chkLotCodeFChar.Location = new System.Drawing.Point(590, 21);
            this.chkLotCodeFChar.MaxLength = 40;
            this.chkLotCodeFChar.Multiline = false;
            this.chkLotCodeFChar.Name = "chkLotCodeFChar";
            this.chkLotCodeFChar.PasswordChar = '\0';
            this.chkLotCodeFChar.ReadOnly = false;
            this.chkLotCodeFChar.ShowCheckBox = true;
            this.chkLotCodeFChar.Size = new System.Drawing.Size(201, 24);
            this.chkLotCodeFChar.TabIndex = 2;
            this.chkLotCodeFChar.TabNext = true;
            this.chkLotCodeFChar.Value = "";
            this.chkLotCodeFChar.WidthType = UserControl.WidthTypes.Small;
            this.chkLotCodeFChar.XAlign = 691;
            // 
            // ucLabelItemCode
            // 
            this.ucLabelItemCode.AllowEditOnlyChecked = true;
            this.ucLabelItemCode.AutoSelectAll = false;
            this.ucLabelItemCode.AutoUpper = true;
            this.ucLabelItemCode.Caption = "产品代码";
            this.ucLabelItemCode.Checked = false;
            this.ucLabelItemCode.EditType = UserControl.EditTypes.String;
            this.ucLabelItemCode.Location = new System.Drawing.Point(21, 50);
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
            this.ucLabelItemCode.XAlign = 82;
            // 
            // chkLotCodeLen
            // 
            this.chkLotCodeLen.AllowEditOnlyChecked = true;
            this.chkLotCodeLen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkLotCodeLen.AutoSelectAll = false;
            this.chkLotCodeLen.AutoUpper = true;
            this.chkLotCodeLen.Caption = "批次长度";
            this.chkLotCodeLen.Checked = false;
            this.chkLotCodeLen.EditType = UserControl.EditTypes.Integer;
            this.chkLotCodeLen.Location = new System.Drawing.Point(315, 21);
            this.chkLotCodeLen.MaxLength = 5;
            this.chkLotCodeLen.Multiline = false;
            this.chkLotCodeLen.Name = "chkLotCodeLen";
            this.chkLotCodeLen.PasswordChar = '\0';
            this.chkLotCodeLen.ReadOnly = false;
            this.chkLotCodeLen.ShowCheckBox = true;
            this.chkLotCodeLen.Size = new System.Drawing.Size(177, 24);
            this.chkLotCodeLen.TabIndex = 1;
            this.chkLotCodeLen.TabNext = true;
            this.chkLotCodeLen.Value = "";
            this.chkLotCodeLen.WidthType = UserControl.WidthTypes.Small;
            this.chkLotCodeLen.XAlign = 392;
            // 
            // ucLabelLotCode
            // 
            this.ucLabelLotCode.AllowEditOnlyChecked = true;
            this.ucLabelLotCode.AutoSelectAll = false;
            this.ucLabelLotCode.AutoUpper = true;
            this.ucLabelLotCode.Caption = "批次条码";
            this.ucLabelLotCode.Checked = false;
            this.ucLabelLotCode.EditType = UserControl.EditTypes.String;
            this.ucLabelLotCode.Location = new System.Drawing.Point(21, 21);
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
            this.ucLabelLotCode.XAlign = 82;
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
            this.ucLabelMOCode.Location = new System.Drawing.Point(21, 78);
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
            this.ucLabelMOCode.XAlign = 82;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkCartonFChar);
            this.groupBox2.Controls.Add(this.chkCartonLen);
            this.groupBox2.Controls.Add(this.ucLabelCartonNoForLot);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 431);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(886, 92);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // chkCartonFChar
            // 
            this.chkCartonFChar.AllowEditOnlyChecked = true;
            this.chkCartonFChar.AutoSelectAll = false;
            this.chkCartonFChar.AutoUpper = true;
            this.chkCartonFChar.Caption = "箱号首字符串";
            this.chkCartonFChar.Checked = false;
            this.chkCartonFChar.EditType = UserControl.EditTypes.String;
            this.chkCartonFChar.Location = new System.Drawing.Point(590, 36);
            this.chkCartonFChar.MaxLength = 40;
            this.chkCartonFChar.Multiline = false;
            this.chkCartonFChar.Name = "chkCartonFChar";
            this.chkCartonFChar.PasswordChar = '\0';
            this.chkCartonFChar.ReadOnly = false;
            this.chkCartonFChar.ShowCheckBox = true;
            this.chkCartonFChar.Size = new System.Drawing.Size(201, 24);
            this.chkCartonFChar.TabIndex = 6;
            this.chkCartonFChar.TabNext = true;
            this.chkCartonFChar.Value = "";
            this.chkCartonFChar.WidthType = UserControl.WidthTypes.Small;
            this.chkCartonFChar.XAlign = 691;
            // 
            // chkCartonLen
            // 
            this.chkCartonLen.AllowEditOnlyChecked = true;
            this.chkCartonLen.AutoSelectAll = false;
            this.chkCartonLen.AutoUpper = true;
            this.chkCartonLen.Caption = "箱号长度";
            this.chkCartonLen.Checked = false;
            this.chkCartonLen.EditType = UserControl.EditTypes.Integer;
            this.chkCartonLen.Location = new System.Drawing.Point(315, 36);
            this.chkCartonLen.MaxLength = 5;
            this.chkCartonLen.Multiline = false;
            this.chkCartonLen.Name = "chkCartonLen";
            this.chkCartonLen.PasswordChar = '\0';
            this.chkCartonLen.ReadOnly = false;
            this.chkCartonLen.ShowCheckBox = true;
            this.chkCartonLen.Size = new System.Drawing.Size(177, 24);
            this.chkCartonLen.TabIndex = 5;
            this.chkCartonLen.TabNext = true;
            this.chkCartonLen.Value = "";
            this.chkCartonLen.WidthType = UserControl.WidthTypes.Small;
            this.chkCartonLen.XAlign = 392;
            // 
            // ucLabelCartonNoForLot
            // 
            this.ucLabelCartonNoForLot.AllowEditOnlyChecked = true;
            this.ucLabelCartonNoForLot.AutoSelectAll = false;
            this.ucLabelCartonNoForLot.AutoUpper = true;
            this.ucLabelCartonNoForLot.Caption = "箱号";
            this.ucLabelCartonNoForLot.Checked = false;
            this.ucLabelCartonNoForLot.EditType = UserControl.EditTypes.String;
            this.ucLabelCartonNoForLot.Location = new System.Drawing.Point(45, 36);
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
            this.ucLabelCartonNoForLot.XAlign = 82;
            this.ucLabelCartonNoForLot.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelCartonNoForLot_TxtboxKeyPress);
            // 
            // DisplayMessage
            // 
            this.DisplayMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DisplayMessage.Controls.Add(this.ucMessage);
            this.DisplayMessage.Location = new System.Drawing.Point(0, 337);
            this.DisplayMessage.Name = "DisplayMessage";
            this.DisplayMessage.Size = new System.Drawing.Size(886, 94);
            this.DisplayMessage.TabIndex = 3;
            this.DisplayMessage.TabStop = false;
            this.DisplayMessage.Text = "采集信息提示：";
            // 
            // ucMessage
            // 
            this.ucMessage.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessage.ButtonVisible = false;
            this.ucMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessage.Location = new System.Drawing.Point(3, 17);
            this.ucMessage.Name = "ucMessage";
            this.ucMessage.Size = new System.Drawing.Size(880, 74);
            this.ucMessage.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ultraGridDetail);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 118);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(886, 213);
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
            this.ultraGridDetail.Location = new System.Drawing.Point(3, 5);
            this.ultraGridDetail.Name = "ultraGridDetail";
            this.ultraGridDetail.Size = new System.Drawing.Size(880, 205);
            this.ultraGridDetail.TabIndex = 1;
            this.ultraGridDetail.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridDetail_InitializeLayout);
            // 
            // FLotCollectionCartonForLot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 523);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.DisplayMessage);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FLotCollectionCartonForLot";
            this.Text = "Carton包装批次采集";
            this.Load += new System.EventHandler(this.FLotCollectionCartonForLot_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.DisplayMessage.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCLabelEdit ucLabelItemCode;
        private UserControl.UCLabelEdit ucLabelMOCode;
        private UserControl.UCLabelEdit ucLabelItemName;
        private UserControl.UCLabelEdit ucLabelCartonCapacity;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox DisplayMessage;
        private System.Windows.Forms.GroupBox groupBox4;
        private UserControl.UCLabelEdit ucLabelCartonCollecting;
        private UserControl.UCLabelEdit chkLotCodeFChar;
        private UserControl.UCLabelEdit chkLotCodeLen;
        private UserControl.UCLabelEdit ucLabelLotCode;
        private UserControl.UCMessage ucMessage;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridDetail;
        private UserControl.UCLabelEdit chkCartonFChar;
        private UserControl.UCLabelEdit chkCartonLen;
        private UserControl.UCLabelEdit ucLabelCartonNoForLot;


    }
}