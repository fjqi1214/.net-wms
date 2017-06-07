namespace BenQGuru.eMES.Client
{
    partial class FReceiveMaterial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FReceiveMaterial));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnQuery = new UserControl.UCButton();
            this.ucPlanDate = new UserControl.UCDatetTime();
            this.txtMoCode = new UserControl.UCLabelEdit();
            this.btnGetBigSSCode = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSave = new UserControl.UCButton();
            this.edtReceiveQty = new UserControl.UCLabelEdit();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ultraGridMaterial = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.txtBigSSCode = new UserControl.UCLabelEdit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMaterial)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBigSSCode);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.ucPlanDate);
            this.groupBox1.Controls.Add(this.txtMoCode);
            this.groupBox1.Controls.Add(this.btnGetBigSSCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(851, 73);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuery.BackColor = System.Drawing.SystemColors.Control;
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.ButtonType = UserControl.ButtonTypes.None;
            this.btnQuery.Caption = "查询";
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.Location = new System.Drawing.Point(696, 21);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(88, 24);
            this.btnQuery.TabIndex = 207;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // ucPlanDate
            // 
            this.ucPlanDate.Caption = "配送日期";
            this.ucPlanDate.Location = new System.Drawing.Point(22, 22);
            this.ucPlanDate.Name = "ucPlanDate";
            this.ucPlanDate.ShowType = UserControl.DateTimeTypes.Date;
            this.ucPlanDate.Size = new System.Drawing.Size(149, 23);
            this.ucPlanDate.TabIndex = 0;
            this.ucPlanDate.Value = new System.DateTime(2009, 3, 24, 0, 0, 0, 0);
            this.ucPlanDate.XAlign = 85;
            // 
            // txtMoCode
            // 
            this.txtMoCode.AllowEditOnlyChecked = true;
            this.txtMoCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtMoCode.Caption = "工单";
            this.txtMoCode.Checked = false;
            this.txtMoCode.EditType = UserControl.EditTypes.String;
            this.txtMoCode.Location = new System.Drawing.Point(432, 21);
            this.txtMoCode.MaxLength = 40;
            this.txtMoCode.Multiline = false;
            this.txtMoCode.Name = "txtMoCode";
            this.txtMoCode.PasswordChar = '\0';
            this.txtMoCode.ReadOnly = false;
            this.txtMoCode.ShowCheckBox = false;
            this.txtMoCode.Size = new System.Drawing.Size(237, 24);
            this.txtMoCode.TabIndex = 205;
            this.txtMoCode.TabNext = true;
            this.txtMoCode.TabStop = false;
            this.txtMoCode.Value = "";
            this.txtMoCode.WidthType = UserControl.WidthTypes.Long;
            this.txtMoCode.XAlign = 469;
            // 
            // btnGetBigSSCode
            // 
            this.btnGetBigSSCode.Location = new System.Drawing.Point(371, 21);
            this.btnGetBigSSCode.Name = "btnGetBigSSCode";
            this.btnGetBigSSCode.Size = new System.Drawing.Size(34, 24);
            this.btnGetBigSSCode.TabIndex = 7;
            this.btnGetBigSSCode.Text = "...";
            this.btnGetBigSSCode.UseVisualStyleBackColor = true;
            this.btnGetBigSSCode.Click += new System.EventHandler(this.btnGetBigSSCode_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.edtReceiveQty);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 553);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(851, 72);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.ButtonType = UserControl.ButtonTypes.None;
            this.btnSave.Caption = "保存";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(381, 33);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 24);
            this.btnSave.TabIndex = 208;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // edtReceiveQty
            // 
            this.edtReceiveQty.AllowEditOnlyChecked = true;
            this.edtReceiveQty.Caption = "接受数量";
            this.edtReceiveQty.Checked = false;
            this.edtReceiveQty.EditType = UserControl.EditTypes.Integer;
            this.edtReceiveQty.Location = new System.Drawing.Point(24, 33);
            this.edtReceiveQty.MaxLength = 8;
            this.edtReceiveQty.Multiline = false;
            this.edtReceiveQty.Name = "edtReceiveQty";
            this.edtReceiveQty.PasswordChar = '\0';
            this.edtReceiveQty.ReadOnly = false;
            this.edtReceiveQty.ShowCheckBox = false;
            this.edtReceiveQty.Size = new System.Drawing.Size(161, 26);
            this.edtReceiveQty.TabIndex = 20;
            this.edtReceiveQty.TabNext = false;
            this.edtReceiveQty.Value = "";
            this.edtReceiveQty.Visible = false;
            this.edtReceiveQty.WidthType = UserControl.WidthTypes.Small;
            this.edtReceiveQty.XAlign = 85;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ultraGridMaterial);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 73);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(851, 480);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // ultraGridMaterial
            // 
            this.ultraGridMaterial.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMaterial.Location = new System.Drawing.Point(3, 16);
            this.ultraGridMaterial.Name = "ultraGridMaterial";
            this.ultraGridMaterial.Size = new System.Drawing.Size(845, 461);
            this.ultraGridMaterial.TabIndex = 0;
            this.ultraGridMaterial.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMaterial_InitializeLayout);
            // 
            // txtBigSSCode
            // 
            this.txtBigSSCode.AllowEditOnlyChecked = true;
            this.txtBigSSCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBigSSCode.Caption = "大线";
            this.txtBigSSCode.Checked = false;
            this.txtBigSSCode.EditType = UserControl.EditTypes.String;
            this.txtBigSSCode.Location = new System.Drawing.Point(189, 21);
            this.txtBigSSCode.MaxLength = 40;
            this.txtBigSSCode.Multiline = false;
            this.txtBigSSCode.Name = "txtBigSSCode";
            this.txtBigSSCode.PasswordChar = '\0';
            this.txtBigSSCode.ReadOnly = false;
            this.txtBigSSCode.ShowCheckBox = false;
            this.txtBigSSCode.Size = new System.Drawing.Size(170, 24);
            this.txtBigSSCode.TabIndex = 208;
            this.txtBigSSCode.TabNext = true;
            this.txtBigSSCode.TabStop = false;
            this.txtBigSSCode.Value = "";
            this.txtBigSSCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtBigSSCode.XAlign = 226;
            // 
            // FReceiveMaterial
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(851, 625);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FReceiveMaterial";
            this.Text = "产线收料";
            this.Load += new System.EventHandler(this.FReceiveMaterial_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMaterial)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnGetBigSSCode;
        private UserControl.UCLabelEdit txtMoCode;
        private UserControl.UCDatetTime ucPlanDate;
        private UserControl.UCButton btnQuery;
        private UserControl.UCButton btnSave;
        private UserControl.UCLabelEdit edtReceiveQty;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMaterial;
        private UserControl.UCLabelEdit txtBigSSCode;
    }
}