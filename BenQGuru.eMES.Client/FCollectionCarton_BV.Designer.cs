namespace BenQGuru.eMES.Client
{
    partial class FCollectionCarton_BV
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
            this.ucLabelCartonCapacity = new UserControl.UCLabelEdit();
            this.ucLabelItemDesc = new UserControl.UCLabelEdit();
            this.ucLabelItemCode = new UserControl.UCLabelEdit();
            this.ucLabelMOCode = new UserControl.UCLabelEdit();
            this.chkCartonFChar = new UserControl.UCLabelEdit();
            this.chkCartonLen = new UserControl.UCLabelEdit();
            this.ucLabelCartonNo = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkCardFChar = new UserControl.UCLabelEdit();
            this.chkCardLen = new UserControl.UCLabelEdit();
            this.ucLabelRCardForCarton = new UserControl.UCLabelEdit();
            this.collectInfo = new System.Windows.Forms.GroupBox();
            this.ucMessage = new UserControl.UCMessage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ultraGridDetail = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.collectInfo.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucLabelCartonCollected);
            this.groupBox1.Controls.Add(this.ucLabelCartonCapacity);
            this.groupBox1.Controls.Add(this.ucLabelItemDesc);
            this.groupBox1.Controls.Add(this.ucLabelItemCode);
            this.groupBox1.Controls.Add(this.ucLabelMOCode);
            this.groupBox1.Controls.Add(this.chkCartonFChar);
            this.groupBox1.Controls.Add(this.chkCartonLen);
            this.groupBox1.Controls.Add(this.ucLabelCartonNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(886, 118);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // ucLabelCartonCollected
            // 
            this.ucLabelCartonCollected.AllowEditOnlyChecked = true;
            this.ucLabelCartonCollected.AutoSelectAll = false;
            this.ucLabelCartonCollected.AutoUpper = true;
            this.ucLabelCartonCollected.Caption = "Carton已装数量";
            this.ucLabelCartonCollected.Checked = false;
            this.ucLabelCartonCollected.EditType = UserControl.EditTypes.String;
            this.ucLabelCartonCollected.Location = new System.Drawing.Point(594, 78);
            this.ucLabelCartonCollected.MaxLength = 40;
            this.ucLabelCartonCollected.Multiline = false;
            this.ucLabelCartonCollected.Name = "ucLabelCartonCollected";
            this.ucLabelCartonCollected.PasswordChar = '\0';
            this.ucLabelCartonCollected.ReadOnly = true;
            this.ucLabelCartonCollected.ShowCheckBox = false;
            this.ucLabelCartonCollected.Size = new System.Drawing.Size(197, 24);
            this.ucLabelCartonCollected.TabIndex = 12;
            this.ucLabelCartonCollected.TabNext = true;
            this.ucLabelCartonCollected.Value = "";
            this.ucLabelCartonCollected.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelCartonCollected.XAlign = 691;
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
            // ucLabelItemDesc
            // 
            this.ucLabelItemDesc.AllowEditOnlyChecked = true;
            this.ucLabelItemDesc.AutoSelectAll = false;
            this.ucLabelItemDesc.AutoUpper = true;
            this.ucLabelItemDesc.Caption = "产品名称";
            this.ucLabelItemDesc.Checked = false;
            this.ucLabelItemDesc.EditType = UserControl.EditTypes.String;
            this.ucLabelItemDesc.Location = new System.Drawing.Point(330, 50);
            this.ucLabelItemDesc.MaxLength = 40;
            this.ucLabelItemDesc.Multiline = false;
            this.ucLabelItemDesc.Name = "ucLabelItemDesc";
            this.ucLabelItemDesc.PasswordChar = '\0';
            this.ucLabelItemDesc.ReadOnly = true;
            this.ucLabelItemDesc.ShowCheckBox = false;
            this.ucLabelItemDesc.Size = new System.Drawing.Size(461, 24);
            this.ucLabelItemDesc.TabIndex = 10;
            this.ucLabelItemDesc.TabNext = true;
            this.ucLabelItemDesc.Value = "";
            this.ucLabelItemDesc.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelItemDesc.XAlign = 391;
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
            // chkCartonFChar
            // 
            this.chkCartonFChar.AllowEditOnlyChecked = true;
            this.chkCartonFChar.AutoSelectAll = false;
            this.chkCartonFChar.AutoUpper = true;
            this.chkCartonFChar.Caption = "箱号首字符串";
            this.chkCartonFChar.Checked = false;
            this.chkCartonFChar.EditType = UserControl.EditTypes.String;
            this.chkCartonFChar.Location = new System.Drawing.Point(590, 18);
            this.chkCartonFChar.MaxLength = 40;
            this.chkCartonFChar.Multiline = false;
            this.chkCartonFChar.Name = "chkCartonFChar";
            this.chkCartonFChar.PasswordChar = '\0';
            this.chkCartonFChar.ReadOnly = false;
            this.chkCartonFChar.ShowCheckBox = true;
            this.chkCartonFChar.Size = new System.Drawing.Size(201, 24);
            this.chkCartonFChar.TabIndex = 2;
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
            this.chkCartonLen.Location = new System.Drawing.Point(315, 18);
            this.chkCartonLen.MaxLength = 5;
            this.chkCartonLen.Multiline = false;
            this.chkCartonLen.Name = "chkCartonLen";
            this.chkCartonLen.PasswordChar = '\0';
            this.chkCartonLen.ReadOnly = false;
            this.chkCartonLen.ShowCheckBox = true;
            this.chkCartonLen.Size = new System.Drawing.Size(177, 24);
            this.chkCartonLen.TabIndex = 1;
            this.chkCartonLen.TabNext = true;
            this.chkCartonLen.Value = "";
            this.chkCartonLen.WidthType = UserControl.WidthTypes.Small;
            this.chkCartonLen.XAlign = 392;
            // 
            // ucLabelCartonNo
            // 
            this.ucLabelCartonNo.AllowEditOnlyChecked = true;
            this.ucLabelCartonNo.AutoSelectAll = false;
            this.ucLabelCartonNo.AutoUpper = true;
            this.ucLabelCartonNo.Caption = "箱号";
            this.ucLabelCartonNo.Checked = false;
            this.ucLabelCartonNo.EditType = UserControl.EditTypes.String;
            this.ucLabelCartonNo.Location = new System.Drawing.Point(45, 18);
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
            this.ucLabelCartonNo.XAlign = 82;
            this.ucLabelCartonNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelCartonNo_TxtboxKeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkCardFChar);
            this.groupBox2.Controls.Add(this.chkCardLen);
            this.groupBox2.Controls.Add(this.ucLabelRCardForCarton);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 431);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(886, 92);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // chkCardFChar
            // 
            this.chkCardFChar.AllowEditOnlyChecked = true;
            this.chkCardFChar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkCardFChar.AutoSelectAll = false;
            this.chkCardFChar.AutoUpper = true;
            this.chkCardFChar.Caption = "序列号首字符串";
            this.chkCardFChar.Checked = false;
            this.chkCardFChar.EditType = UserControl.EditTypes.String;
            this.chkCardFChar.Location = new System.Drawing.Point(578, 35);
            this.chkCardFChar.MaxLength = 40;
            this.chkCardFChar.Multiline = false;
            this.chkCardFChar.Name = "chkCardFChar";
            this.chkCardFChar.PasswordChar = '\0';
            this.chkCardFChar.ReadOnly = false;
            this.chkCardFChar.ShowCheckBox = true;
            this.chkCardFChar.Size = new System.Drawing.Size(213, 24);
            this.chkCardFChar.TabIndex = 6;
            this.chkCardFChar.TabNext = true;
            this.chkCardFChar.Value = "";
            this.chkCardFChar.WidthType = UserControl.WidthTypes.Small;
            this.chkCardFChar.XAlign = 691;
            // 
            // chkCardLen
            // 
            this.chkCardLen.AllowEditOnlyChecked = true;
            this.chkCardLen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkCardLen.AutoSelectAll = false;
            this.chkCardLen.AutoUpper = true;
            this.chkCardLen.Caption = "序列号长度";
            this.chkCardLen.Checked = false;
            this.chkCardLen.EditType = UserControl.EditTypes.Integer;
            this.chkCardLen.Location = new System.Drawing.Point(303, 35);
            this.chkCardLen.MaxLength = 5;
            this.chkCardLen.Multiline = false;
            this.chkCardLen.Name = "chkCardLen";
            this.chkCardLen.PasswordChar = '\0';
            this.chkCardLen.ReadOnly = false;
            this.chkCardLen.ShowCheckBox = true;
            this.chkCardLen.Size = new System.Drawing.Size(189, 24);
            this.chkCardLen.TabIndex = 5;
            this.chkCardLen.TabNext = true;
            this.chkCardLen.Value = "";
            this.chkCardLen.WidthType = UserControl.WidthTypes.Small;
            this.chkCardLen.XAlign = 392;
            // 
            // ucLabelRCardForCarton
            // 
            this.ucLabelRCardForCarton.AllowEditOnlyChecked = true;
            this.ucLabelRCardForCarton.AutoSelectAll = false;
            this.ucLabelRCardForCarton.AutoUpper = true;
            this.ucLabelRCardForCarton.Caption = "产品序列号";
            this.ucLabelRCardForCarton.Checked = false;
            this.ucLabelRCardForCarton.EditType = UserControl.EditTypes.String;
            this.ucLabelRCardForCarton.Location = new System.Drawing.Point(9, 35);
            this.ucLabelRCardForCarton.MaxLength = 40;
            this.ucLabelRCardForCarton.Multiline = false;
            this.ucLabelRCardForCarton.Name = "ucLabelRCardForCarton";
            this.ucLabelRCardForCarton.PasswordChar = '\0';
            this.ucLabelRCardForCarton.ReadOnly = false;
            this.ucLabelRCardForCarton.ShowCheckBox = false;
            this.ucLabelRCardForCarton.Size = new System.Drawing.Size(273, 24);
            this.ucLabelRCardForCarton.TabIndex = 4;
            this.ucLabelRCardForCarton.TabNext = true;
            this.ucLabelRCardForCarton.Value = "";
            this.ucLabelRCardForCarton.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelRCardForCarton.XAlign = 82;
            this.ucLabelRCardForCarton.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelRCardForCarton_TxtboxKeyPress);
            // 
            // collectInfo
            // 
            this.collectInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.collectInfo.Controls.Add(this.ucMessage);
            this.collectInfo.Location = new System.Drawing.Point(0, 337);
            this.collectInfo.Name = "collectInfo";
            this.collectInfo.Size = new System.Drawing.Size(886, 94);
            this.collectInfo.TabIndex = 3;
            this.collectInfo.TabStop = false;
            this.collectInfo.Text = "采集信息提示：";
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
            // FCollectionCarton_BV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 523);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.collectInfo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FCollectionCarton_BV";
            this.Text = "Carton包装采集";
            this.Load += new System.EventHandler(this.FCollectionCarton_BV_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.collectInfo.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCLabelEdit ucLabelItemCode;
        private UserControl.UCLabelEdit ucLabelMOCode;
        private UserControl.UCLabelEdit chkCartonFChar;
        private UserControl.UCLabelEdit chkCartonLen;
        private UserControl.UCLabelEdit ucLabelCartonNo;
        private UserControl.UCLabelEdit ucLabelItemDesc;
        private UserControl.UCLabelEdit ucLabelCartonCapacity;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox collectInfo;
        private System.Windows.Forms.GroupBox groupBox4;
        private UserControl.UCLabelEdit ucLabelCartonCollected;
        private UserControl.UCLabelEdit chkCardFChar;
        private UserControl.UCLabelEdit chkCardLen;
        private UserControl.UCLabelEdit ucLabelRCardForCarton;
        private UserControl.UCMessage ucMessage;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridDetail;


    }
}