namespace BenQGuru.eMES.Client
{
    partial class FChangeCompany
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
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FChangeCompany));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboCompany = new UserControl.UCLabelCombox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtRecordNum = new UserControl.UCLabelEdit();
            this.gridInfo = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtInput = new UserControl.UCLabelEdit();
            this.opsetPackObject = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.btnCancel = new UserControl.UCButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInfo)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opsetPackObject)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboCompany);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(792, 54);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cboCompany
            // 
            this.cboCompany.AllowEditOnlyChecked = true;
            this.cboCompany.Caption = "目标公司";
            this.cboCompany.Checked = false;
            this.cboCompany.Location = new System.Drawing.Point(26, 20);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.SelectedIndex = -1;
            this.cboCompany.ShowCheckBox = false;
            this.cboCompany.Size = new System.Drawing.Size(194, 20);
            this.cboCompany.TabIndex = 0;
            this.cboCompany.WidthType = UserControl.WidthTypes.Normal;
            this.cboCompany.XAlign = 87;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtRecordNum);
            this.groupBox2.Controls.Add(this.gridInfo);
            this.groupBox2.Location = new System.Drawing.Point(0, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(792, 306);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtRecordNum
            // 
            this.txtRecordNum.AllowEditOnlyChecked = true;
            this.txtRecordNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecordNum.Caption = "数量";
            this.txtRecordNum.Checked = false;
            this.txtRecordNum.EditType = UserControl.EditTypes.String;
            this.txtRecordNum.Location = new System.Drawing.Point(699, 276);
            this.txtRecordNum.MaxLength = 40;
            this.txtRecordNum.Multiline = false;
            this.txtRecordNum.Name = "txtRecordNum";
            this.txtRecordNum.PasswordChar = '\0';
            this.txtRecordNum.ReadOnly = true;
            this.txtRecordNum.ShowCheckBox = false;
            this.txtRecordNum.Size = new System.Drawing.Size(87, 24);
            this.txtRecordNum.TabIndex = 4;
            this.txtRecordNum.TabNext = true;
            this.txtRecordNum.Value = "";
            this.txtRecordNum.WidthType = UserControl.WidthTypes.Tiny;
            this.txtRecordNum.XAlign = 736;
            // 
            // gridInfo
            // 
            this.gridInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridInfo.Location = new System.Drawing.Point(3, 17);
            this.gridInfo.Name = "gridInfo";
            this.gridInfo.Size = new System.Drawing.Size(786, 253);
            this.gridInfo.TabIndex = 4;
            this.gridInfo.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridInfo_InitializeLayout);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtInput);
            this.groupBox3.Controls.Add(this.opsetPackObject);
            this.groupBox3.Location = new System.Drawing.Point(0, 366);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(792, 118);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "序列号输入";
            // 
            // txtInput
            // 
            this.txtInput.AllowEditOnlyChecked = true;
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInput.Caption = "输入";
            this.txtInput.Checked = false;
            this.txtInput.EditType = UserControl.EditTypes.String;
            this.txtInput.Location = new System.Drawing.Point(41, 78);
            this.txtInput.MaxLength = 40;
            this.txtInput.Multiline = false;
            this.txtInput.Name = "txtInput";
            this.txtInput.PasswordChar = '\0';
            this.txtInput.ReadOnly = false;
            this.txtInput.ShowCheckBox = false;
            this.txtInput.Size = new System.Drawing.Size(170, 24);
            this.txtInput.TabIndex = 2;
            this.txtInput.TabNext = true;
            this.txtInput.Value = "";
            this.txtInput.WidthType = UserControl.WidthTypes.Normal;
            this.txtInput.XAlign = 78;
            this.txtInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_TxtboxKeyPress);
            // 
            // opsetPackObject
            // 
            this.opsetPackObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            appearance1.FontData.BoldAsString = "False";
            this.opsetPackObject.Appearance = appearance1;
            this.opsetPackObject.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.opsetPackObject.FlatMode = true;
            this.opsetPackObject.ItemAppearance = appearance2;
            valueListItem1.DataValue = "0";
            valueListItem1.DisplayText = "垛位";
            valueListItem2.DataValue = "1";
            valueListItem2.DisplayText = "栈板";
            valueListItem3.DataValue = "2";
            valueListItem3.DisplayText = "序列号";
            this.opsetPackObject.Items.Add(valueListItem1);
            this.opsetPackObject.Items.Add(valueListItem2);
            this.opsetPackObject.Items.Add(valueListItem3);
            this.opsetPackObject.Location = new System.Drawing.Point(41, 49);
            this.opsetPackObject.Name = "opsetPackObject";
            this.opsetPackObject.Size = new System.Drawing.Size(219, 16);
            this.opsetPackObject.TabIndex = 1;
            this.opsetPackObject.ValueChanged += new System.EventHandler(this.opsetPackObject_ValueChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.btnCancel.Caption = "取消";
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(364, 512);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 22);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FChangeCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FChangeCompany";
            this.Text = "变更产品归属公司";
            this.Load += new System.EventHandler(this.FChangeCompany_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridInfo)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.opsetPackObject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCLabelCombox cboCompany;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private UserControl.UCButton btnCancel;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridInfo;
        private UserControl.UCLabelEdit txtRecordNum;
        private UserControl.UCLabelEdit txtInput;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet opsetPackObject;
    }
}