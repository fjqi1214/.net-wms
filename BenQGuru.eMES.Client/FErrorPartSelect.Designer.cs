namespace BenQGuru.eMES.Client
{
    partial class FErrorPartSelect
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaterialCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaterialDesc");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FErrorPartSelect));
            this.ultraGridMaterial = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucButtonCancel = new UserControl.UCButton();
            this.ucButtonConfirm = new UserControl.UCButton();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMaterial)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraGridMaterial
            // 
            this.ultraGridMaterial.Cursor = System.Windows.Forms.Cursors.Default;
            ultraGridColumn1.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn1.Header.Caption = "料号";
            ultraGridColumn1.Width = 180;
            ultraGridColumn2.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn2.Header.Caption = "描述";
            ultraGridColumn2.Width = 270;
            ultraGridBand1.Columns.Add(ultraGridColumn1);
            ultraGridBand1.Columns.Add(ultraGridColumn2);
            this.ultraGridMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGridMaterial.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMaterial.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridMaterial.Location = new System.Drawing.Point(0, 0);
            this.ultraGridMaterial.Name = "ultraGridMaterial";
            this.ultraGridMaterial.Size = new System.Drawing.Size(495, 227);
            this.ultraGridMaterial.TabIndex = 11;
            this.ultraGridMaterial.DoubleClick += new System.EventHandler(this.ultraGridMaterial_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucButtonCancel);
            this.groupBox1.Controls.Add(this.ucButtonConfirm);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 227);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 46);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // ucButtonCancel
            // 
            this.ucButtonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ucButtonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonCancel.BackgroundImage")));
            this.ucButtonCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.ucButtonCancel.Caption = "取消";
            this.ucButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonCancel.Location = new System.Drawing.Point(274, 12);
            this.ucButtonCancel.Name = "ucButtonCancel";
            this.ucButtonCancel.Size = new System.Drawing.Size(88, 22);
            this.ucButtonCancel.TabIndex = 7;
            this.ucButtonCancel.Click += new System.EventHandler(this.ucButtonCancel_Click);
            // 
            // ucButtonConfirm
            // 
            this.ucButtonConfirm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ucButtonConfirm.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonConfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonConfirm.BackgroundImage")));
            this.ucButtonConfirm.ButtonType = UserControl.ButtonTypes.Confirm;
            this.ucButtonConfirm.Caption = "确认";
            this.ucButtonConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonConfirm.Location = new System.Drawing.Point(120, 12);
            this.ucButtonConfirm.Name = "ucButtonConfirm";
            this.ucButtonConfirm.Size = new System.Drawing.Size(88, 22);
            this.ucButtonConfirm.TabIndex = 6;
            this.ucButtonConfirm.Click += new System.EventHandler(this.ucButtonConfirm_Click);
            // 
            // FErrorPartSelect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(495, 273);
            this.ControlBox = false;
            this.Controls.Add(this.ultraGridMaterial);
            this.Controls.Add(this.groupBox1);
            this.Name = "FErrorPartSelect";
            this.Text = "选择不良零件";
            this.Load += new System.EventHandler(this.FMaterialCodeSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMaterial)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMaterial;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCButton ucButtonConfirm;
        private UserControl.UCButton ucButtonCancel;
    }
}