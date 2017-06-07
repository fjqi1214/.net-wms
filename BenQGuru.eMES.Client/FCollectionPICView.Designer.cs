namespace BenQGuru.eMES.Client
{
    partial class FCollectionPICView
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRcard = new System.Windows.Forms.TextBox();
            this.checkBoxRcard = new System.Windows.Forms.CheckBox();
            this.txtMoCode = new System.Windows.Forms.TextBox();
            this.checkBoxMo = new System.Windows.Forms.CheckBox();
            this.txtItemDesc = new System.Windows.Forms.TextBox();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.lblItemDesc = new System.Windows.Forms.Label();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listEsopPicsView = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbEsopIMG = new System.Windows.Forms.PictureBox();
            this.txtPicDesc = new System.Windows.Forms.TextBox();
            this.lblPicDescription = new System.Windows.Forms.Label();
            this.txtPicTitle = new System.Windows.Forms.TextBox();
            this.lblPicTitle = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEsopIMG)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox1.Controls.Add(this.txtRcard);
            this.groupBox1.Controls.Add(this.checkBoxRcard);
            this.groupBox1.Controls.Add(this.txtMoCode);
            this.groupBox1.Controls.Add(this.checkBoxMo);
            this.groupBox1.Controls.Add(this.txtItemDesc);
            this.groupBox1.Controls.Add(this.txtItemCode);
            this.groupBox1.Controls.Add(this.lblItemDesc);
            this.groupBox1.Controls.Add(this.lblItemCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(963, 78);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "呈现条件";
            // 
            // txtRcard
            // 
            this.txtRcard.Location = new System.Drawing.Point(433, 16);
            this.txtRcard.Name = "txtRcard";
            this.txtRcard.Size = new System.Drawing.Size(132, 21);
            this.txtRcard.TabIndex = 222;
            this.txtRcard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMoCode_KeyDown);
            // 
            // checkBoxRcard
            // 
            this.checkBoxRcard.AutoSize = true;
            this.checkBoxRcard.Location = new System.Drawing.Point(339, 18);
            this.checkBoxRcard.Name = "checkBoxRcard";
            this.checkBoxRcard.Size = new System.Drawing.Size(84, 16);
            this.checkBoxRcard.TabIndex = 221;
            this.checkBoxRcard.Text = "产品序列号";
            this.checkBoxRcard.UseVisualStyleBackColor = true;
            this.checkBoxRcard.CheckedChanged += new System.EventHandler(this.checkBoxRcard_CheckedChanged);
            // 
            // txtMoCode
            // 
            this.txtMoCode.Location = new System.Drawing.Point(132, 16);
            this.txtMoCode.Name = "txtMoCode";
            this.txtMoCode.Size = new System.Drawing.Size(133, 21);
            this.txtMoCode.TabIndex = 222;
            this.txtMoCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMoCode_KeyDown);
            // 
            // checkBoxMo
            // 
            this.checkBoxMo.AutoSize = true;
            this.checkBoxMo.Location = new System.Drawing.Point(52, 18);
            this.checkBoxMo.Name = "checkBoxMo";
            this.checkBoxMo.Size = new System.Drawing.Size(72, 16);
            this.checkBoxMo.TabIndex = 221;
            this.checkBoxMo.Text = "工单代码";
            this.checkBoxMo.UseVisualStyleBackColor = true;
            this.checkBoxMo.CheckedChanged += new System.EventHandler(this.checkBoxMo_CheckedChanged);
            // 
            // txtItemDesc
            // 
            this.txtItemDesc.Location = new System.Drawing.Point(433, 49);
            this.txtItemDesc.Name = "txtItemDesc";
            this.txtItemDesc.ReadOnly = true;
            this.txtItemDesc.Size = new System.Drawing.Size(132, 21);
            this.txtItemDesc.TabIndex = 220;
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(133, 49);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.ReadOnly = true;
            this.txtItemCode.Size = new System.Drawing.Size(132, 21);
            this.txtItemCode.TabIndex = 220;
            // 
            // lblItemDesc
            // 
            this.lblItemDesc.AutoSize = true;
            this.lblItemDesc.Location = new System.Drawing.Point(365, 52);
            this.lblItemDesc.Name = "lblItemDesc";
            this.lblItemDesc.Size = new System.Drawing.Size(53, 12);
            this.lblItemDesc.TabIndex = 219;
            this.lblItemDesc.Text = "产品描述";
            // 
            // lblItemCode
            // 
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Location = new System.Drawing.Point(71, 52);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(53, 12);
            this.lblItemCode.TabIndex = 219;
            this.lblItemCode.Text = "产品代码";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox3.Controls.Add(this.listEsopPicsView);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 78);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(963, 135);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // listEsopPicsView
            // 
            this.listEsopPicsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listEsopPicsView.Location = new System.Drawing.Point(3, 17);
            this.listEsopPicsView.Name = "listEsopPicsView";
            this.listEsopPicsView.Size = new System.Drawing.Size(957, 115);
            this.listEsopPicsView.TabIndex = 0;
            this.listEsopPicsView.UseCompatibleStateImageBehavior = false;
            this.listEsopPicsView.SelectedIndexChanged += new System.EventHandler(this.listEsopPicsView_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(60, 60);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.txtPicDesc);
            this.groupBox2.Controls.Add(this.lblPicDescription);
            this.groupBox2.Controls.Add(this.txtPicTitle);
            this.groupBox2.Controls.Add(this.lblPicTitle);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(963, 504);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图片信息";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pbEsopIMG);
            this.panel1.Location = new System.Drawing.Point(73, 112);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(819, 371);
            this.panel1.TabIndex = 221;
            // 
            // pbEsopIMG
            // 
            this.pbEsopIMG.Location = new System.Drawing.Point(0, 0);
            this.pbEsopIMG.Name = "pbEsopIMG";
            this.pbEsopIMG.Size = new System.Drawing.Size(819, 371);
            this.pbEsopIMG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbEsopIMG.TabIndex = 1;
            this.pbEsopIMG.TabStop = false;
            // 
            // txtPicDesc
            // 
            this.txtPicDesc.Location = new System.Drawing.Point(132, 47);
            this.txtPicDesc.Multiline = true;
            this.txtPicDesc.Name = "txtPicDesc";
            this.txtPicDesc.ReadOnly = true;
            this.txtPicDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPicDesc.Size = new System.Drawing.Size(433, 59);
            this.txtPicDesc.TabIndex = 220;
            // 
            // lblPicDescription
            // 
            this.lblPicDescription.AutoSize = true;
            this.lblPicDescription.Location = new System.Drawing.Point(71, 50);
            this.lblPicDescription.Name = "lblPicDescription";
            this.lblPicDescription.Size = new System.Drawing.Size(53, 12);
            this.lblPicDescription.TabIndex = 219;
            this.lblPicDescription.Text = "图片说明";
            // 
            // txtPicTitle
            // 
            this.txtPicTitle.Location = new System.Drawing.Point(133, 14);
            this.txtPicTitle.Name = "txtPicTitle";
            this.txtPicTitle.ReadOnly = true;
            this.txtPicTitle.Size = new System.Drawing.Size(132, 21);
            this.txtPicTitle.TabIndex = 220;
            // 
            // lblPicTitle
            // 
            this.lblPicTitle.AutoSize = true;
            this.lblPicTitle.Location = new System.Drawing.Point(71, 17);
            this.lblPicTitle.Name = "lblPicTitle";
            this.lblPicTitle.Size = new System.Drawing.Size(53, 12);
            this.lblPicTitle.TabIndex = 219;
            this.lblPicTitle.Text = "图片概述";
            // 
            // FCollectionPICView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(963, 717);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "FCollectionPICView";
            this.Text = "操作指导说明书采集呈现";
            this.Load += new System.EventHandler(this.FCollectionPICView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbEsopIMG)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtItemDesc;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label lblItemDesc;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.ListView listEsopPicsView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbEsopIMG;
        private System.Windows.Forms.TextBox txtPicDesc;
        private System.Windows.Forms.Label lblPicDescription;
        private System.Windows.Forms.TextBox txtPicTitle;
        private System.Windows.Forms.Label lblPicTitle;
        private System.Windows.Forms.TextBox txtRcard;
        private System.Windows.Forms.CheckBox checkBoxRcard;
        private System.Windows.Forms.TextBox txtMoCode;
        private System.Windows.Forms.CheckBox checkBoxMo;
    }
}