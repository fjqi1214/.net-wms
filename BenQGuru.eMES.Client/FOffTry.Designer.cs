namespace BenQGuru.eMES.Client
{
    partial class FOffTry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FOffTry));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOffTry = new UserControl.UCButton();
            this.txtCollected = new UserControl.UCLabelEdit();
            this.BtnExit = new UserControl.UCButton();
            this.txtInPutSN = new UserControl.UCLabelEdit();
            this.opsetOffTry = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ultraGridTry = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opsetOffTry)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridTry)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOffTry);
            this.groupBox1.Controls.Add(this.txtCollected);
            this.groupBox1.Controls.Add(this.BtnExit);
            this.groupBox1.Controls.Add(this.txtInPutSN);
            this.groupBox1.Controls.Add(this.opsetOffTry);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 471);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(666, 101);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnOffTry
            // 
            this.btnOffTry.BackColor = System.Drawing.SystemColors.Control;
            this.btnOffTry.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOffTry.BackgroundImage")));
            this.btnOffTry.ButtonType = UserControl.ButtonTypes.None;
            this.btnOffTry.Caption = "脱离";
            this.btnOffTry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOffTry.Location = new System.Drawing.Point(442, 56);
            this.btnOffTry.Name = "btnOffTry";
            this.btnOffTry.Size = new System.Drawing.Size(88, 22);
            this.btnOffTry.TabIndex = 210;
            this.btnOffTry.Click += new System.EventHandler(this.btnOffTry_Click);
            // 
            // txtCollected
            // 
            this.txtCollected.AllowEditOnlyChecked = true;
            this.txtCollected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCollected.AutoSelectAll = false;
            this.txtCollected.AutoUpper = true;
            this.txtCollected.BackColor = System.Drawing.Color.Transparent;
            this.txtCollected.Caption = "已采集数量";
            this.txtCollected.Checked = false;
            this.txtCollected.EditType = UserControl.EditTypes.Integer;
            this.txtCollected.Enabled = false;
            this.txtCollected.Location = new System.Drawing.Point(298, 56);
            this.txtCollected.MaxLength = 40;
            this.txtCollected.Multiline = false;
            this.txtCollected.Name = "txtCollected";
            this.txtCollected.PasswordChar = '\0';
            this.txtCollected.ReadOnly = true;
            this.txtCollected.ShowCheckBox = false;
            this.txtCollected.Size = new System.Drawing.Size(123, 22);
            this.txtCollected.TabIndex = 207;
            this.txtCollected.TabNext = true;
            this.txtCollected.TabStop = false;
            this.txtCollected.Value = "";
            this.txtCollected.WidthType = UserControl.WidthTypes.Tiny;
            this.txtCollected.XAlign = 371;
            // 
            // BtnExit
            // 
            this.BtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.BtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnExit.BackgroundImage")));
            this.BtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.BtnExit.Caption = "退出";
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.Location = new System.Drawing.Point(549, 56);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(88, 22);
            this.BtnExit.TabIndex = 206;
            this.BtnExit.Click += new System.EventHandler(this.uBtnExit_Click);
            // 
            // txtInPutSN
            // 
            this.txtInPutSN.AllowEditOnlyChecked = true;
            this.txtInPutSN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInPutSN.AutoSelectAll = false;
            this.txtInPutSN.AutoUpper = true;
            this.txtInPutSN.BackColor = System.Drawing.Color.Transparent;
            this.txtInPutSN.Caption = "SN输入框";
            this.txtInPutSN.Checked = false;
            this.txtInPutSN.EditType = UserControl.EditTypes.String;
            this.txtInPutSN.Location = new System.Drawing.Point(12, 56);
            this.txtInPutSN.MaxLength = 40;
            this.txtInPutSN.Multiline = false;
            this.txtInPutSN.Name = "txtInPutSN";
            this.txtInPutSN.PasswordChar = '\0';
            this.txtInPutSN.ReadOnly = false;
            this.txtInPutSN.ShowCheckBox = false;
            this.txtInPutSN.Size = new System.Drawing.Size(261, 22);
            this.txtInPutSN.TabIndex = 1;
            this.txtInPutSN.TabNext = false;
            this.txtInPutSN.Value = "";
            this.txtInPutSN.WidthType = UserControl.WidthTypes.Long;
            this.txtInPutSN.XAlign = 73;
            this.txtInPutSN.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRCard_TxtboxKeyPress);
            // 
            // opsetOffTry
            // 
            this.opsetOffTry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            appearance1.FontData.BoldAsString = "False";
            this.opsetOffTry.Appearance = appearance1;
            this.opsetOffTry.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            valueListItem1.DataValue = "0";
            valueListItem1.DisplayText = "箱号";
            valueListItem2.DataValue = "1";
            valueListItem2.DisplayText = "产品序列号";
            this.opsetOffTry.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2});
            this.opsetOffTry.Location = new System.Drawing.Point(12, 23);
            this.opsetOffTry.Name = "opsetOffTry";
            this.opsetOffTry.Size = new System.Drawing.Size(129, 16);
            this.opsetOffTry.TabIndex = 204;
            this.opsetOffTry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.opsetOffTry.ValueChanged += new System.EventHandler(this.opsetOffTry_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.ultraGridTry);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(642, 460);
            this.panel1.TabIndex = 1;
            // 
            // ultraGridTry
            // 
            this.ultraGridTry.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridTry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridTry.Location = new System.Drawing.Point(0, 0);
            this.ultraGridTry.Name = "ultraGridTry";
            this.ultraGridTry.Size = new System.Drawing.Size(642, 460);
            this.ultraGridTry.TabIndex = 0;
            this.ultraGridTry.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridTry_InitializeLayout);
            // 
            // FOffTry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 572);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FOffTry";
            this.Text = "FOffTry";
            this.Load += new System.EventHandler(this.FOffTry_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.opsetOffTry)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridTry)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridTry;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet opsetOffTry;
        private UserControl.UCLabelEdit txtCollected;
        private UserControl.UCButton BtnExit;
        private UserControl.UCLabelEdit txtInPutSN;
        private UserControl.UCButton btnOffTry;
    }
}