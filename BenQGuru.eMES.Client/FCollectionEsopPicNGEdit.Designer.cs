namespace BenQGuru.eMES.Client
{
    partial class FCollectionEsopPicNGEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCollectionEsopPicNGEdit));
            this.imageListEsopPics = new System.Windows.Forms.ImageList(this.components);
            this.lblItemCode = new System.Windows.Forms.Label();
            this.lblItemDesc = new System.Windows.Forms.Label();
            this.lblRcard = new System.Windows.Forms.Label();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.txtRcard = new System.Windows.Forms.TextBox();
            this.txtItemDesc = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxDisplayMemo = new System.Windows.Forms.GroupBox();
            this.listPics = new System.Windows.Forms.ListView();
            this.groupBoxTsMemo = new System.Windows.Forms.GroupBox();
            this.listPicsNG = new System.Windows.Forms.ListView();
            this.groupBoxPicMemo = new System.Windows.Forms.GroupBox();
            this.ColorBox = new System.Windows.Forms.GroupBox();
            this.blue = new System.Windows.Forms.Button();
            this.MoreColor = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.LawnGreen = new System.Windows.Forms.Button();
            this.Red = new System.Windows.Forms.Button();
            this.Cyan = new System.Windows.Forms.Button();
            this.Yellow = new System.Windows.Forms.Button();
            this.Black = new System.Windows.Forms.Button();
            this.Magenta = new System.Windows.Forms.Button();
            this.White = new System.Windows.Forms.Button();
            this.ucBtnClose = new UserControl.UCButton();
            this.ucBtnDelete = new UserControl.UCButton();
            this.txtNGPicMemo = new System.Windows.Forms.TextBox();
            this.lblNGPicMemo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbEsopIMG = new System.Windows.Forms.PictureBox();
            this.ucBtnCancel = new UserControl.UCButton();
            this.ucBtnSave = new UserControl.UCButton();
            this.imageListPicsNG = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBoxDisplayMemo.SuspendLayout();
            this.groupBoxTsMemo.SuspendLayout();
            this.groupBoxPicMemo.SuspendLayout();
            this.ColorBox.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEsopIMG)).BeginInit();
            this.SuspendLayout();
            // 
            // imageListEsopPics
            // 
            this.imageListEsopPics.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListEsopPics.ImageSize = new System.Drawing.Size(60, 60);
            this.imageListEsopPics.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lblItemCode
            // 
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Location = new System.Drawing.Point(290, 17);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(53, 12);
            this.lblItemCode.TabIndex = 219;
            this.lblItemCode.Text = "产品代码";
            // 
            // lblItemDesc
            // 
            this.lblItemDesc.AutoSize = true;
            this.lblItemDesc.Location = new System.Drawing.Point(541, 17);
            this.lblItemDesc.Name = "lblItemDesc";
            this.lblItemDesc.Size = new System.Drawing.Size(53, 12);
            this.lblItemDesc.TabIndex = 219;
            this.lblItemDesc.Text = "产品描述";
            // 
            // lblRcard
            // 
            this.lblRcard.AutoSize = true;
            this.lblRcard.Location = new System.Drawing.Point(30, 17);
            this.lblRcard.Name = "lblRcard";
            this.lblRcard.Size = new System.Drawing.Size(65, 12);
            this.lblRcard.TabIndex = 219;
            this.lblRcard.Text = "产品序列号";
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(352, 15);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.ReadOnly = true;
            this.txtItemCode.Size = new System.Drawing.Size(132, 21);
            this.txtItemCode.TabIndex = 1;
            // 
            // txtRcard
            // 
            this.txtRcard.Location = new System.Drawing.Point(103, 15);
            this.txtRcard.Name = "txtRcard";
            this.txtRcard.ReadOnly = true;
            this.txtRcard.Size = new System.Drawing.Size(132, 21);
            this.txtRcard.TabIndex = 0;
            // 
            // txtItemDesc
            // 
            this.txtItemDesc.Location = new System.Drawing.Point(600, 14);
            this.txtItemDesc.Name = "txtItemDesc";
            this.txtItemDesc.ReadOnly = true;
            this.txtItemDesc.Size = new System.Drawing.Size(132, 21);
            this.txtItemDesc.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox1.Controls.Add(this.txtItemDesc);
            this.groupBox1.Controls.Add(this.txtRcard);
            this.groupBox1.Controls.Add(this.txtItemCode);
            this.groupBox1.Controls.Add(this.lblRcard);
            this.groupBox1.Controls.Add(this.lblItemDesc);
            this.groupBox1.Controls.Add(this.lblItemCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1028, 44);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "不良产品信息";
            // 
            // groupBoxDisplayMemo
            // 
            this.groupBoxDisplayMemo.Controls.Add(this.listPics);
            this.groupBoxDisplayMemo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxDisplayMemo.Location = new System.Drawing.Point(0, 44);
            this.groupBoxDisplayMemo.Name = "groupBoxDisplayMemo";
            this.groupBoxDisplayMemo.Size = new System.Drawing.Size(1028, 130);
            this.groupBoxDisplayMemo.TabIndex = 5;
            this.groupBoxDisplayMemo.TabStop = false;
            this.groupBoxDisplayMemo.Text = "维修说明书呈现";
            // 
            // listPics
            // 
            this.listPics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listPics.Location = new System.Drawing.Point(3, 17);
            this.listPics.Name = "listPics";
            this.listPics.Size = new System.Drawing.Size(1022, 110);
            this.listPics.TabIndex = 3;
            this.listPics.UseCompatibleStateImageBehavior = false;
            this.listPics.SelectedIndexChanged += new System.EventHandler(this.listEsopPicsView_SelectedIndexChanged);
            // 
            // groupBoxTsMemo
            // 
            this.groupBoxTsMemo.Controls.Add(this.listPicsNG);
            this.groupBoxTsMemo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxTsMemo.Location = new System.Drawing.Point(0, 174);
            this.groupBoxTsMemo.Name = "groupBoxTsMemo";
            this.groupBoxTsMemo.Size = new System.Drawing.Size(1028, 130);
            this.groupBoxTsMemo.TabIndex = 5;
            this.groupBoxTsMemo.TabStop = false;
            this.groupBoxTsMemo.Text = "维修点";
            // 
            // listPicsNG
            // 
            this.listPicsNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listPicsNG.Location = new System.Drawing.Point(3, 17);
            this.listPicsNG.Name = "listPicsNG";
            this.listPicsNG.Size = new System.Drawing.Size(1022, 110);
            this.listPicsNG.TabIndex = 4;
            this.listPicsNG.UseCompatibleStateImageBehavior = false;
            this.listPicsNG.SelectedIndexChanged += new System.EventHandler(this.listPicsNG_SelectedIndexChanged);
            // 
            // groupBoxPicMemo
            // 
            this.groupBoxPicMemo.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBoxPicMemo.Controls.Add(this.ColorBox);
            this.groupBoxPicMemo.Controls.Add(this.ucBtnClose);
            this.groupBoxPicMemo.Controls.Add(this.ucBtnDelete);
            this.groupBoxPicMemo.Controls.Add(this.txtNGPicMemo);
            this.groupBoxPicMemo.Controls.Add(this.lblNGPicMemo);
            this.groupBoxPicMemo.Controls.Add(this.panel1);
            this.groupBoxPicMemo.Controls.Add(this.ucBtnCancel);
            this.groupBoxPicMemo.Controls.Add(this.ucBtnSave);
            this.groupBoxPicMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPicMemo.Location = new System.Drawing.Point(0, 304);
            this.groupBoxPicMemo.Name = "groupBoxPicMemo";
            this.groupBoxPicMemo.Size = new System.Drawing.Size(1028, 405);
            this.groupBoxPicMemo.TabIndex = 7;
            this.groupBoxPicMemo.TabStop = false;
            this.groupBoxPicMemo.Text = "图片信息";
            // 
            // ColorBox
            // 
            this.ColorBox.Controls.Add(this.blue);
            this.ColorBox.Controls.Add(this.MoreColor);
            this.ColorBox.Controls.Add(this.btnColor);
            this.ColorBox.Controls.Add(this.LawnGreen);
            this.ColorBox.Controls.Add(this.Red);
            this.ColorBox.Controls.Add(this.Cyan);
            this.ColorBox.Controls.Add(this.Yellow);
            this.ColorBox.Controls.Add(this.Black);
            this.ColorBox.Controls.Add(this.Magenta);
            this.ColorBox.Controls.Add(this.White);
            this.ColorBox.Location = new System.Drawing.Point(637, 305);
            this.ColorBox.Name = "ColorBox";
            this.ColorBox.Size = new System.Drawing.Size(166, 49);
            this.ColorBox.TabIndex = 226;
            this.ColorBox.TabStop = false;
            // 
            // blue
            // 
            this.blue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.blue.BackColor = System.Drawing.Color.Blue;
            this.blue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.blue.Location = new System.Drawing.Point(53, 28);
            this.blue.Name = "blue";
            this.blue.Size = new System.Drawing.Size(15, 15);
            this.blue.TabIndex = 3;
            this.blue.UseVisualStyleBackColor = false;
            this.blue.Click += new System.EventHandler(this.blue_Click);
            // 
            // MoreColor
            // 
            this.MoreColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoreColor.BackColor = System.Drawing.SystemColors.HighlightText;
            this.MoreColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.MoreColor.Location = new System.Drawing.Point(104, 11);
            this.MoreColor.Name = "MoreColor";
            this.MoreColor.Size = new System.Drawing.Size(50, 31);
            this.MoreColor.TabIndex = 30;
            this.MoreColor.Text = "More>>";
            this.MoreColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MoreColor.UseVisualStyleBackColor = false;
            this.MoreColor.Click += new System.EventHandler(this.MoreColor_Click);
            // 
            // btnColor
            // 
            this.btnColor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnColor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColor.Location = new System.Drawing.Point(3, 12);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(30, 30);
            this.btnColor.TabIndex = 32;
            this.btnColor.UseVisualStyleBackColor = false;
            // 
            // LawnGreen
            // 
            this.LawnGreen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LawnGreen.BackColor = System.Drawing.Color.LawnGreen;
            this.LawnGreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LawnGreen.Location = new System.Drawing.Point(85, 12);
            this.LawnGreen.Name = "LawnGreen";
            this.LawnGreen.Size = new System.Drawing.Size(15, 15);
            this.LawnGreen.TabIndex = 29;
            this.LawnGreen.UseVisualStyleBackColor = false;
            this.LawnGreen.Click += new System.EventHandler(this.LawnGreen_Click);
            // 
            // Red
            // 
            this.Red.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Red.BackColor = System.Drawing.Color.Red;
            this.Red.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Red.Location = new System.Drawing.Point(37, 28);
            this.Red.Name = "Red";
            this.Red.Size = new System.Drawing.Size(15, 15);
            this.Red.TabIndex = 2;
            this.Red.UseVisualStyleBackColor = false;
            this.Red.Click += new System.EventHandler(this.Red_Click);
            // 
            // Cyan
            // 
            this.Cyan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Cyan.BackColor = System.Drawing.Color.Cyan;
            this.Cyan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cyan.Location = new System.Drawing.Point(69, 28);
            this.Cyan.Name = "Cyan";
            this.Cyan.Size = new System.Drawing.Size(15, 15);
            this.Cyan.TabIndex = 30;
            this.Cyan.UseVisualStyleBackColor = false;
            this.Cyan.Click += new System.EventHandler(this.Cyan_Click);
            // 
            // Yellow
            // 
            this.Yellow.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Yellow.BackColor = System.Drawing.Color.Yellow;
            this.Yellow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Yellow.Location = new System.Drawing.Point(69, 12);
            this.Yellow.Name = "Yellow";
            this.Yellow.Size = new System.Drawing.Size(15, 15);
            this.Yellow.TabIndex = 28;
            this.Yellow.UseVisualStyleBackColor = false;
            this.Yellow.Click += new System.EventHandler(this.Yellow_Click);
            // 
            // Black
            // 
            this.Black.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Black.BackColor = System.Drawing.Color.Black;
            this.Black.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Black.Location = new System.Drawing.Point(37, 12);
            this.Black.Name = "Black";
            this.Black.Size = new System.Drawing.Size(15, 15);
            this.Black.TabIndex = 0;
            this.Black.UseVisualStyleBackColor = false;
            this.Black.Click += new System.EventHandler(this.Black_Click);
            // 
            // Magenta
            // 
            this.Magenta.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Magenta.BackColor = System.Drawing.Color.Magenta;
            this.Magenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Magenta.Location = new System.Drawing.Point(85, 28);
            this.Magenta.Name = "Magenta";
            this.Magenta.Size = new System.Drawing.Size(15, 15);
            this.Magenta.TabIndex = 31;
            this.Magenta.UseVisualStyleBackColor = false;
            this.Magenta.Click += new System.EventHandler(this.Magenta_Click);
            // 
            // White
            // 
            this.White.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.White.BackColor = System.Drawing.Color.White;
            this.White.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.White.Location = new System.Drawing.Point(53, 12);
            this.White.Name = "White";
            this.White.Size = new System.Drawing.Size(15, 15);
            this.White.TabIndex = 1;
            this.White.UseVisualStyleBackColor = false;
            this.White.Click += new System.EventHandler(this.White_Click);
            // 
            // ucBtnClose
            // 
            this.ucBtnClose.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnClose.BackgroundImage")));
            this.ucBtnClose.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnClose.Caption = "关闭";
            this.ucBtnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnClose.Location = new System.Drawing.Point(635, 370);
            this.ucBtnClose.Name = "ucBtnClose";
            this.ucBtnClose.Size = new System.Drawing.Size(88, 22);
            this.ucBtnClose.TabIndex = 228;
            this.ucBtnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucBtnDelete
            // 
            this.ucBtnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnDelete.BackgroundImage")));
            this.ucBtnDelete.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnDelete.Caption = "删除";
            this.ucBtnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnDelete.Location = new System.Drawing.Point(530, 370);
            this.ucBtnDelete.Name = "ucBtnDelete";
            this.ucBtnDelete.Size = new System.Drawing.Size(88, 22);
            this.ucBtnDelete.TabIndex = 229;
            this.ucBtnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtNGPicMemo
            // 
            this.txtNGPicMemo.Location = new System.Drawing.Point(67, 309);
            this.txtNGPicMemo.MaxLength = 500;
            this.txtNGPicMemo.Multiline = true;
            this.txtNGPicMemo.Name = "txtNGPicMemo";
            this.txtNGPicMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNGPicMemo.Size = new System.Drawing.Size(553, 55);
            this.txtNGPicMemo.TabIndex = 223;
            // 
            // lblNGPicMemo
            // 
            this.lblNGPicMemo.AutoSize = true;
            this.lblNGPicMemo.Location = new System.Drawing.Point(32, 312);
            this.lblNGPicMemo.Name = "lblNGPicMemo";
            this.lblNGPicMemo.Size = new System.Drawing.Size(29, 12);
            this.lblNGPicMemo.TabIndex = 222;
            this.lblNGPicMemo.Text = "说明";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pbEsopIMG);
            this.panel1.Location = new System.Drawing.Point(32, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(961, 281);
            this.panel1.TabIndex = 221;
            // 
            // pbEsopIMG
            // 
            this.pbEsopIMG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbEsopIMG.Location = new System.Drawing.Point(0, 0);
            this.pbEsopIMG.Name = "pbEsopIMG";
            this.pbEsopIMG.Size = new System.Drawing.Size(961, 281);
            this.pbEsopIMG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbEsopIMG.TabIndex = 1;
            this.pbEsopIMG.TabStop = false;
            this.pbEsopIMG.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDemo_MouseMove);
            this.pbEsopIMG.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDemo_MouseClick);
            this.pbEsopIMG.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDemo_MouseDown);
            this.pbEsopIMG.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxDemo_Paint);
            this.pbEsopIMG.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDemo_MouseUp);
            // 
            // ucBtnCancel
            // 
            this.ucBtnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnCancel.BackgroundImage")));
            this.ucBtnCancel.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnCancel.Caption = "取消";
            this.ucBtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnCancel.Location = new System.Drawing.Point(425, 370);
            this.ucBtnCancel.Name = "ucBtnCancel";
            this.ucBtnCancel.Size = new System.Drawing.Size(88, 22);
            this.ucBtnCancel.TabIndex = 228;
            this.ucBtnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ucBtnSave
            // 
            this.ucBtnSave.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnSave.BackgroundImage")));
            this.ucBtnSave.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnSave.Caption = "保存";
            this.ucBtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnSave.Location = new System.Drawing.Point(320, 370);
            this.ucBtnSave.Name = "ucBtnSave";
            this.ucBtnSave.Size = new System.Drawing.Size(88, 22);
            this.ucBtnSave.TabIndex = 229;
            this.ucBtnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // imageListPicsNG
            // 
            this.imageListPicsNG.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListPicsNG.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListPicsNG.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FCollectionEsopPicNGEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1028, 709);
            this.Controls.Add(this.groupBoxPicMemo);
            this.Controls.Add(this.groupBoxTsMemo);
            this.Controls.Add(this.groupBoxDisplayMemo);
            this.Controls.Add(this.groupBox1);
            this.Name = "FCollectionEsopPicNGEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "操作指导说明书修改";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxDisplayMemo.ResumeLayout(false);
            this.groupBoxTsMemo.ResumeLayout(false);
            this.groupBoxPicMemo.ResumeLayout(false);
            this.groupBoxPicMemo.PerformLayout();
            this.ColorBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbEsopIMG)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageListEsopPics;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.Label lblItemDesc;
        private System.Windows.Forms.Label lblRcard;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.TextBox txtRcard;
        private System.Windows.Forms.TextBox txtItemDesc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxDisplayMemo;
        private System.Windows.Forms.ListView listPics;
        private System.Windows.Forms.GroupBox groupBoxTsMemo;
        private System.Windows.Forms.ListView listPicsNG;
        private System.Windows.Forms.GroupBox groupBoxPicMemo;
        private System.Windows.Forms.GroupBox ColorBox;
        private System.Windows.Forms.Button blue;
        private System.Windows.Forms.Button MoreColor;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Button LawnGreen;
        private System.Windows.Forms.Button Red;
        private System.Windows.Forms.Button Cyan;
        private System.Windows.Forms.Button Yellow;
        private System.Windows.Forms.Button Black;
        private System.Windows.Forms.Button Magenta;
        private System.Windows.Forms.Button White;
        private System.Windows.Forms.TextBox txtNGPicMemo;
        private System.Windows.Forms.Label lblNGPicMemo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbEsopIMG;
        private System.Windows.Forms.ImageList imageListPicsNG;
        private UserControl.UCButton ucBtnDelete;
        private UserControl.UCButton ucBtnClose;
        private UserControl.UCButton ucBtnCancel;
        private UserControl.UCButton ucBtnSave;
    }
}