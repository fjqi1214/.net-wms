namespace BenQGuru.eMES.Client
{
    partial class FEQPEFficiencyQuery
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FEQPEFficiencyQuery));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonOpCodeQuery = new System.Windows.Forms.Button();
            this.ucLabelEditResCode = new UserControl.UCLabelEdit();
            this.drpEQPID = new UserControl.UCLabelCombox();
            this.drpSSCode = new UserControl.UCLabelCombox();
            this.btnQuery = new UserControl.UCButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ultraGridScrutiny = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridScrutiny)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonOpCodeQuery);
            this.groupBox1.Controls.Add(this.ucLabelEditResCode);
            this.groupBox1.Controls.Add(this.drpEQPID);
            this.groupBox1.Controls.Add(this.drpSSCode);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(773, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // buttonOpCodeQuery
            // 
            this.buttonOpCodeQuery.Location = new System.Drawing.Point(604, 16);
            this.buttonOpCodeQuery.Name = "buttonOpCodeQuery";
            this.buttonOpCodeQuery.Size = new System.Drawing.Size(26, 21);
            this.buttonOpCodeQuery.TabIndex = 216;
            this.buttonOpCodeQuery.Text = "...";
            this.buttonOpCodeQuery.UseVisualStyleBackColor = true;
            this.buttonOpCodeQuery.Click += new System.EventHandler(this.buttonOpCodeQuery_Click);
            // 
            // ucLabelEditResCode
            // 
            this.ucLabelEditResCode.AllowEditOnlyChecked = true;
            this.ucLabelEditResCode.AutoUpper = true;
            this.ucLabelEditResCode.Caption = "资源";
            this.ucLabelEditResCode.Checked = false;
            this.ucLabelEditResCode.EditType = UserControl.EditTypes.Number;
            this.ucLabelEditResCode.Location = new System.Drawing.Point(427, 14);
            this.ucLabelEditResCode.MaxLength = 200;
            this.ucLabelEditResCode.Multiline = false;
            this.ucLabelEditResCode.Name = "ucLabelEditResCode";
            this.ucLabelEditResCode.PasswordChar = '\0';
            this.ucLabelEditResCode.ReadOnly = true;
            this.ucLabelEditResCode.ShowCheckBox = false;
            this.ucLabelEditResCode.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditResCode.TabIndex = 215;
            this.ucLabelEditResCode.TabNext = false;
            this.ucLabelEditResCode.Value = "";
            this.ucLabelEditResCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditResCode.XAlign = 464;
            // 
            // drpEQPID
            // 
            this.drpEQPID.AllowEditOnlyChecked = true;
            this.drpEQPID.Caption = "设备编码";
            this.drpEQPID.Checked = false;
            this.drpEQPID.Location = new System.Drawing.Point(6, 16);
            this.drpEQPID.Name = "drpEQPID";
            this.drpEQPID.SelectedIndex = -1;
            this.drpEQPID.ShowCheckBox = false;
            this.drpEQPID.Size = new System.Drawing.Size(194, 20);
            this.drpEQPID.TabIndex = 214;
            this.drpEQPID.WidthType = UserControl.WidthTypes.Normal;
            this.drpEQPID.XAlign = 67;
            // 
            // drpSSCode
            // 
            this.drpSSCode.AllowEditOnlyChecked = true;
            this.drpSSCode.Caption = "产线";
            this.drpSSCode.Checked = false;
            this.drpSSCode.Location = new System.Drawing.Point(225, 16);
            this.drpSSCode.Name = "drpSSCode";
            this.drpSSCode.SelectedIndex = -1;
            this.drpSSCode.ShowCheckBox = false;
            this.drpSSCode.Size = new System.Drawing.Size(170, 20);
            this.drpSSCode.TabIndex = 213;
            this.drpSSCode.WidthType = UserControl.WidthTypes.Normal;
            this.drpSSCode.XAlign = 262;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuery.BackColor = System.Drawing.SystemColors.Control;
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.ButtonType = UserControl.ButtonTypes.None;
            this.btnQuery.Caption = "查询";
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.Location = new System.Drawing.Point(662, 14);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(88, 22);
            this.btnQuery.TabIndex = 208;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ultraGridScrutiny);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(773, 320);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // ultraGridScrutiny
            // 
            this.ultraGridScrutiny.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridScrutiny.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridScrutiny.Location = new System.Drawing.Point(3, 17);
            this.ultraGridScrutiny.Name = "ultraGridScrutiny";
            this.ultraGridScrutiny.Size = new System.Drawing.Size(767, 300);
            this.ultraGridScrutiny.TabIndex = 1;
            this.ultraGridScrutiny.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridScrutiny_InitializeLayout);
            // 
            // FEQPEFficiencyQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(773, 370);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FEQPEFficiencyQuery";
            this.Text = "设备效率分析";
            this.Load += new System.EventHandler(this.FOldScrutiny_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridScrutiny)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridScrutiny;
        private UserControl.UCButton btnQuery;
        private System.Windows.Forms.Timer timer1;
        private UserControl.UCLabelCombox drpSSCode;
        private UserControl.UCLabelCombox drpEQPID;
        private UserControl.UCLabelEdit ucLabelEditResCode;
        private System.Windows.Forms.Button buttonOpCodeQuery;
    }
}