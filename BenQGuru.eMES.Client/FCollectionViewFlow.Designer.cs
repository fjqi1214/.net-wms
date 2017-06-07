namespace BenQGuru.eMES.Client
{
    partial class FCollectionViewFlow
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Table1", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ITEMCODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RCARD");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MOCODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ACTIONRESULT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OPCODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ACTION");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ROUTECODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SEGCODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SSCODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RESCODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MDATE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MTIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MUSER");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCollectionViewFlow));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.drownListMoCode = new UserControl.UCLabelCombox();
            this.drownListRouteCode = new UserControl.UCLabelCombox();
            this.txtRcard = new System.Windows.Forms.TextBox();
            this.lblRcard = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelDrawFlow = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gridSimulation = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnRefresh = new UserControl.UCButton();
            this.btnExit = new UserControl.UCButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblColorDesc = new System.Windows.Forms.Label();
            this.lblNoExcute = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblNGItem = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblGoodItem = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSimulation)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.drownListMoCode);
            this.groupBox1.Controls.Add(this.drownListRouteCode);
            this.groupBox1.Controls.Add(this.txtRcard);
            this.groupBox1.Controls.Add(this.lblRcard);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(991, 40);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // drownListMoCode
            // 
            this.drownListMoCode.AllowEditOnlyChecked = true;
            this.drownListMoCode.Caption = "工单代码";
            this.drownListMoCode.Checked = false;
            this.drownListMoCode.Location = new System.Drawing.Point(308, 11);
            this.drownListMoCode.Name = "drownListMoCode";
            this.drownListMoCode.SelectedIndex = -1;
            this.drownListMoCode.ShowCheckBox = false;
            this.drownListMoCode.Size = new System.Drawing.Size(194, 20);
            this.drownListMoCode.TabIndex = 223;
            this.drownListMoCode.WidthType = UserControl.WidthTypes.Normal;
            this.drownListMoCode.XAlign = 369;
            this.drownListMoCode.SelectedIndexChanged += new System.EventHandler(this.drownListMoCode_SelectedIndexChanged);
            // 
            // drownListRouteCode
            // 
            this.drownListRouteCode.AllowEditOnlyChecked = true;
            this.drownListRouteCode.Caption = "途程代码";
            this.drownListRouteCode.Checked = false;
            this.drownListRouteCode.Location = new System.Drawing.Point(596, 10);
            this.drownListRouteCode.Name = "drownListRouteCode";
            this.drownListRouteCode.SelectedIndex = -1;
            this.drownListRouteCode.ShowCheckBox = false;
            this.drownListRouteCode.Size = new System.Drawing.Size(194, 20);
            this.drownListRouteCode.TabIndex = 223;
            this.drownListRouteCode.WidthType = UserControl.WidthTypes.Normal;
            this.drownListRouteCode.XAlign = 657;
            this.drownListRouteCode.SelectedIndexChanged += new System.EventHandler(this.drownListRouteCode_SelectedIndexChanged);
            // 
            // txtRcard
            // 
            this.txtRcard.Location = new System.Drawing.Point(97, 12);
            this.txtRcard.Name = "txtRcard";
            this.txtRcard.Size = new System.Drawing.Size(132, 21);
            this.txtRcard.TabIndex = 220;
            this.txtRcard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRcard_KeyDown);
            // 
            // lblRcard
            // 
            this.lblRcard.AutoSize = true;
            this.lblRcard.Location = new System.Drawing.Point(24, 15);
            this.lblRcard.Name = "lblRcard";
            this.lblRcard.Size = new System.Drawing.Size(65, 12);
            this.lblRcard.TabIndex = 221;
            this.lblRcard.Text = "产品序列号";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.panelDrawFlow);
            this.groupBox2.Location = new System.Drawing.Point(0, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(991, 294);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // panelDrawFlow
            // 
            this.panelDrawFlow.AutoScroll = true;
            this.panelDrawFlow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelDrawFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDrawFlow.Location = new System.Drawing.Point(3, 17);
            this.panelDrawFlow.Name = "panelDrawFlow";
            this.panelDrawFlow.Size = new System.Drawing.Size(985, 274);
            this.panelDrawFlow.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.gridSimulation);
            this.groupBox3.Location = new System.Drawing.Point(0, 378);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(991, 230);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // gridSimulation
            // 
            this.gridSimulation.Cursor = System.Windows.Forms.Cursors.Hand;
            ultraGridColumn1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn1.Header.Caption = "产品代码";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn2.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn2.Header.Caption = "序列号";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn3.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn3.Header.Caption = "工单";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn4.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn4.Header.Caption = "产品状态";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn5.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn5.Header.Caption = "所在工序";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn6.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn6.Header.Caption = "工序结果";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn7.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn7.Header.Caption = "生产途程";
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn8.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn8.Header.Caption = "工段";
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn9.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn9.Header.Caption = "生产线";
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn10.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn10.Header.Caption = "资源";
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn11.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn11.Header.Caption = "日期";
            ultraGridColumn11.Header.VisiblePosition = 10;
            ultraGridColumn12.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn12.Header.Caption = "时间";
            ultraGridColumn12.Header.VisiblePosition = 11;
            ultraGridColumn13.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn13.Header.Caption = "操作人员";
            ultraGridColumn13.Header.VisiblePosition = 12;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13});
            this.gridSimulation.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.gridSimulation.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.gridSimulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSimulation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridSimulation.Location = new System.Drawing.Point(3, 17);
            this.gridSimulation.Name = "gridSimulation";
            this.gridSimulation.Size = new System.Drawing.Size(985, 210);
            this.gridSimulation.TabIndex = 15;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.BackColor = System.Drawing.SystemColors.Control;
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.BackgroundImage")));
            this.btnRefresh.ButtonType = UserControl.ButtonTypes.Save;
            this.btnRefresh.Caption = "刷新";
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Location = new System.Drawing.Point(326, 624);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(88, 22);
            this.btnRefresh.TabIndex = 11;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(454, 624);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 12;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.lblColorDesc);
            this.groupBox4.Controls.Add(this.lblNoExcute);
            this.groupBox4.Controls.Add(this.pictureBox3);
            this.groupBox4.Controls.Add(this.pictureBox2);
            this.groupBox4.Controls.Add(this.lblNGItem);
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Controls.Add(this.lblGoodItem);
            this.groupBox4.Location = new System.Drawing.Point(0, 336);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 1);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.groupBox4.Size = new System.Drawing.Size(991, 42);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            // 
            // lblColorDesc
            // 
            this.lblColorDesc.AutoSize = true;
            this.lblColorDesc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.lblColorDesc.Location = new System.Drawing.Point(13, 15);
            this.lblColorDesc.Name = "lblColorDesc";
            this.lblColorDesc.Size = new System.Drawing.Size(70, 12);
            this.lblColorDesc.TabIndex = 25;
            this.lblColorDesc.Text = "颜色说明：";
            // 
            // lblNoExcute
            // 
            this.lblNoExcute.AutoSize = true;
            this.lblNoExcute.Font = new System.Drawing.Font("宋体", 9F);
            this.lblNoExcute.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNoExcute.Location = new System.Drawing.Point(561, 15);
            this.lblNoExcute.Name = "lblNoExcute";
            this.lblNoExcute.Size = new System.Drawing.Size(41, 12);
            this.lblNoExcute.TabIndex = 24;
            this.lblNoExcute.Text = "未执行";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(500, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(55, 18);
            this.pictureBox3.TabIndex = 22;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Red;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(302, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(55, 18);
            this.pictureBox2.TabIndex = 23;
            this.pictureBox2.TabStop = false;
            // 
            // lblNGItem
            // 
            this.lblNGItem.AutoSize = true;
            this.lblNGItem.Font = new System.Drawing.Font("宋体", 9F);
            this.lblNGItem.ForeColor = System.Drawing.Color.Black;
            this.lblNGItem.Location = new System.Drawing.Point(363, 15);
            this.lblNGItem.Name = "lblNGItem";
            this.lblNGItem.Size = new System.Drawing.Size(101, 12);
            this.lblNGItem.TabIndex = 21;
            this.lblNGItem.Text = "已执行且为不良品";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Green;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(109, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 18);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // lblGoodItem
            // 
            this.lblGoodItem.AutoSize = true;
            this.lblGoodItem.Font = new System.Drawing.Font("宋体", 9F);
            this.lblGoodItem.ForeColor = System.Drawing.Color.Black;
            this.lblGoodItem.Location = new System.Drawing.Point(170, 15);
            this.lblGoodItem.Name = "lblGoodItem";
            this.lblGoodItem.Size = new System.Drawing.Size(89, 12);
            this.lblGoodItem.TabIndex = 19;
            this.lblGoodItem.Text = "已执行且为良品";
            // 
            // FCollectionViewFlow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(991, 657);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FCollectionViewFlow";
            this.Text = "流程图查看";
            this.Load += new System.EventHandler(this.FCollectionViewFlow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSimulation)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRcard;
        private System.Windows.Forms.Label lblRcard;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private UserControl.UCButton btnRefresh;
        private UserControl.UCButton btnExit;
        private System.Windows.Forms.Panel panelDrawFlow;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridSimulation;
        private UserControl.UCLabelCombox drownListMoCode;
        private UserControl.UCLabelCombox drownListRouteCode;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblNoExcute;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblNGItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblGoodItem;
        private System.Windows.Forms.Label lblColorDesc;
    }
}