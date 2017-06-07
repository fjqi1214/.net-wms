namespace BenQGuru.eMES.Client
{
    partial class FCollectionEsopPicNGView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCollectionEsopPicNGView));
            this.pbEsopIMG = new System.Windows.Forms.PictureBox();
            this.groupBoxPicInfo = new System.Windows.Forms.GroupBox();
            this.ucBtnClose = new UserControl.UCButton();
            this.txtNGPicMemo = new System.Windows.Forms.TextBox();
            this.lblNGPicMemo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listPicsNG = new System.Windows.Forms.ListView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.lblRcard = new System.Windows.Forms.Label();
            this.lblItemDesc = new System.Windows.Forms.Label();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.txtRcard = new System.Windows.Forms.TextBox();
            this.groupBoxNGItemInfo = new System.Windows.Forms.GroupBox();
            this.txtItemDesc = new System.Windows.Forms.TextBox();
            this.imageListPicsNG = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbEsopIMG)).BeginInit();
            this.groupBoxPicInfo.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBoxNGItemInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbEsopIMG
            // 
            this.pbEsopIMG.Location = new System.Drawing.Point(0, 0);
            this.pbEsopIMG.Name = "pbEsopIMG";
            this.pbEsopIMG.Size = new System.Drawing.Size(950, 330);
            this.pbEsopIMG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbEsopIMG.TabIndex = 1;
            this.pbEsopIMG.TabStop = false;
            // 
            // groupBoxPicInfo
            // 
            this.groupBoxPicInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBoxPicInfo.Controls.Add(this.ucBtnClose);
            this.groupBoxPicInfo.Controls.Add(this.txtNGPicMemo);
            this.groupBoxPicInfo.Controls.Add(this.lblNGPicMemo);
            this.groupBoxPicInfo.Controls.Add(this.panel1);
            this.groupBoxPicInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPicInfo.Location = new System.Drawing.Point(0, 174);
            this.groupBoxPicInfo.Name = "groupBoxPicInfo";
            this.groupBoxPicInfo.Size = new System.Drawing.Size(1014, 468);
            this.groupBoxPicInfo.TabIndex = 11;
            this.groupBoxPicInfo.TabStop = false;
            this.groupBoxPicInfo.Text = "图片信息";
            // 
            // ucBtnClose
            // 
            this.ucBtnClose.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnClose.BackgroundImage")));
            this.ucBtnClose.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnClose.Caption = "关闭";
            this.ucBtnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnClose.Location = new System.Drawing.Point(450, 434);
            this.ucBtnClose.Name = "ucBtnClose";
            this.ucBtnClose.Size = new System.Drawing.Size(88, 22);
            this.ucBtnClose.TabIndex = 226;
            this.ucBtnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtNGPicMemo
            // 
            this.txtNGPicMemo.Location = new System.Drawing.Point(67, 363);
            this.txtNGPicMemo.Multiline = true;
            this.txtNGPicMemo.Name = "txtNGPicMemo";
            this.txtNGPicMemo.ReadOnly = true;
            this.txtNGPicMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNGPicMemo.Size = new System.Drawing.Size(699, 54);
            this.txtNGPicMemo.TabIndex = 223;
            // 
            // lblNGPicMemo
            // 
            this.lblNGPicMemo.AutoSize = true;
            this.lblNGPicMemo.Location = new System.Drawing.Point(30, 366);
            this.lblNGPicMemo.Name = "lblNGPicMemo";
            this.lblNGPicMemo.Size = new System.Drawing.Size(29, 12);
            this.lblNGPicMemo.TabIndex = 222;
            this.lblNGPicMemo.Text = "说明";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pbEsopIMG);
            this.panel1.Location = new System.Drawing.Point(32, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(951, 330);
            this.panel1.TabIndex = 221;
            // 
            // listPicsNG
            // 
            this.listPicsNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listPicsNG.Location = new System.Drawing.Point(3, 17);
            this.listPicsNG.Name = "listPicsNG";
            this.listPicsNG.Size = new System.Drawing.Size(1008, 110);
            this.listPicsNG.TabIndex = 4;
            this.listPicsNG.UseCompatibleStateImageBehavior = false;
            this.listPicsNG.SelectedIndexChanged += new System.EventHandler(this.listPicsNG_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox4.Controls.Add(this.listPicsNG);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 44);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1014, 130);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "不良位置";
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(352, 18);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.ReadOnly = true;
            this.txtItemCode.Size = new System.Drawing.Size(132, 21);
            this.txtItemCode.TabIndex = 1;
            // 
            // lblRcard
            // 
            this.lblRcard.AutoSize = true;
            this.lblRcard.Location = new System.Drawing.Point(30, 20);
            this.lblRcard.Name = "lblRcard";
            this.lblRcard.Size = new System.Drawing.Size(65, 12);
            this.lblRcard.TabIndex = 219;
            this.lblRcard.Text = "产品序列号";
            // 
            // lblItemDesc
            // 
            this.lblItemDesc.AutoSize = true;
            this.lblItemDesc.Location = new System.Drawing.Point(539, 20);
            this.lblItemDesc.Name = "lblItemDesc";
            this.lblItemDesc.Size = new System.Drawing.Size(53, 12);
            this.lblItemDesc.TabIndex = 219;
            this.lblItemDesc.Text = "产品描述";
            // 
            // lblItemCode
            // 
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Location = new System.Drawing.Point(290, 20);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(53, 12);
            this.lblItemCode.TabIndex = 219;
            this.lblItemCode.Text = "产品代码";
            // 
            // txtRcard
            // 
            this.txtRcard.Location = new System.Drawing.Point(103, 18);
            this.txtRcard.Name = "txtRcard";
            this.txtRcard.ReadOnly = true;
            this.txtRcard.Size = new System.Drawing.Size(132, 21);
            this.txtRcard.TabIndex = 0;
            // 
            // groupBoxNGItemInfo
            // 
            this.groupBoxNGItemInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBoxNGItemInfo.Controls.Add(this.txtItemDesc);
            this.groupBoxNGItemInfo.Controls.Add(this.txtRcard);
            this.groupBoxNGItemInfo.Controls.Add(this.txtItemCode);
            this.groupBoxNGItemInfo.Controls.Add(this.lblRcard);
            this.groupBoxNGItemInfo.Controls.Add(this.lblItemDesc);
            this.groupBoxNGItemInfo.Controls.Add(this.lblItemCode);
            this.groupBoxNGItemInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxNGItemInfo.Location = new System.Drawing.Point(0, 0);
            this.groupBoxNGItemInfo.Name = "groupBoxNGItemInfo";
            this.groupBoxNGItemInfo.Size = new System.Drawing.Size(1014, 44);
            this.groupBoxNGItemInfo.TabIndex = 8;
            this.groupBoxNGItemInfo.TabStop = false;
            this.groupBoxNGItemInfo.Text = "不良产品信息";
            // 
            // txtItemDesc
            // 
            this.txtItemDesc.Location = new System.Drawing.Point(600, 18);
            this.txtItemDesc.Name = "txtItemDesc";
            this.txtItemDesc.ReadOnly = true;
            this.txtItemDesc.Size = new System.Drawing.Size(132, 21);
            this.txtItemDesc.TabIndex = 2;
            // 
            // imageListPicsNG
            // 
            this.imageListPicsNG.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListPicsNG.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListPicsNG.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FCollectionEsopPicNGView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 642);
            this.Controls.Add(this.groupBoxPicInfo);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBoxNGItemInfo);
            this.Name = "FCollectionEsopPicNGView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "不良位置显示";
            ((System.ComponentModel.ISupportInitialize)(this.pbEsopIMG)).EndInit();
            this.groupBoxPicInfo.ResumeLayout(false);
            this.groupBoxPicInfo.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBoxNGItemInfo.ResumeLayout(false);
            this.groupBoxNGItemInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbEsopIMG;
        private System.Windows.Forms.GroupBox groupBoxPicInfo;
        private System.Windows.Forms.TextBox txtNGPicMemo;
        private System.Windows.Forms.Label lblNGPicMemo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listPicsNG;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label lblRcard;
        private System.Windows.Forms.Label lblItemDesc;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.TextBox txtRcard;
        private System.Windows.Forms.GroupBox groupBoxNGItemInfo;
        private System.Windows.Forms.TextBox txtItemDesc;
        private System.Windows.Forms.ImageList imageListPicsNG;
        private UserControl.UCButton ucBtnClose;
    }
}