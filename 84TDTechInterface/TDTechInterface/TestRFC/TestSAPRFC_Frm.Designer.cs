namespace TestSAPRFC
{
    partial class TestSAPRFC_Frm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDN = new System.Windows.Forms.Button();
            this.btnRS = new System.Windows.Forms.Button();
            this.btnUB = new System.Windows.Forms.Button();
            this.btnKCMX = new System.Windows.Forms.Button();
            this.btnPO = new System.Windows.Forms.Button();
            this.btnMaterial = new System.Windows.Forms.Button();
            this.btnStorage = new System.Windows.Forms.Button();
            this.btnVendor = new System.Windows.Forms.Button();
            this.txtKUNNR = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnEnter = new System.Windows.Forms.Button();
            this.txtSAPROUTER = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSYSNR = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtASHOST = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCLIENT = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPASSWD = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUSER = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNAME = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rMsg = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDN);
            this.panel1.Controls.Add(this.btnRS);
            this.panel1.Controls.Add(this.btnUB);
            this.panel1.Controls.Add(this.btnKCMX);
            this.panel1.Controls.Add(this.btnPO);
            this.panel1.Controls.Add(this.btnMaterial);
            this.panel1.Controls.Add(this.btnStorage);
            this.panel1.Controls.Add(this.btnVendor);
            this.panel1.Controls.Add(this.txtKUNNR);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnEnter);
            this.panel1.Controls.Add(this.txtSAPROUTER);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtSYSNR);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtASHOST);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtCLIENT);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtPASSWD);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtUSER);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtNAME);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 151);
            this.panel1.TabIndex = 0;
            // 
            // btnDN
            // 
            this.btnDN.Location = new System.Drawing.Point(390, 86);
            this.btnDN.Name = "btnDN";
            this.btnDN.Size = new System.Drawing.Size(64, 23);
            this.btnDN.TabIndex = 25;
            this.btnDN.Text = "发货批";
            this.btnDN.UseVisualStyleBackColor = true;
            this.btnDN.Click += new System.EventHandler(this.btnDN_Click);
            // 
            // btnRS
            // 
            this.btnRS.Location = new System.Drawing.Point(166, 122);
            this.btnRS.Name = "btnRS";
            this.btnRS.Size = new System.Drawing.Size(85, 23);
            this.btnRS.TabIndex = 23;
            this.btnRS.Text = "预留单(RS)";
            this.btnRS.UseVisualStyleBackColor = true;
            this.btnRS.Click += new System.EventHandler(this.btnRS_Click);
            // 
            // btnUB
            // 
            this.btnUB.Location = new System.Drawing.Point(75, 122);
            this.btnUB.Name = "btnUB";
            this.btnUB.Size = new System.Drawing.Size(85, 23);
            this.btnUB.TabIndex = 22;
            this.btnUB.Text = "库存调拨(UB)";
            this.btnUB.UseVisualStyleBackColor = true;
            this.btnUB.Click += new System.EventHandler(this.btnUB_Click);
            // 
            // btnKCMX
            // 
            this.btnKCMX.Location = new System.Drawing.Point(257, 122);
            this.btnKCMX.Name = "btnKCMX";
            this.btnKCMX.Size = new System.Drawing.Size(64, 23);
            this.btnKCMX.TabIndex = 21;
            this.btnKCMX.Text = "库存明细";
            this.btnKCMX.UseVisualStyleBackColor = true;
            this.btnKCMX.Click += new System.EventHandler(this.btnKCMX_Click);
            // 
            // btnPO
            // 
            this.btnPO.Location = new System.Drawing.Point(14, 122);
            this.btnPO.Name = "btnPO";
            this.btnPO.Size = new System.Drawing.Size(55, 23);
            this.btnPO.TabIndex = 20;
            this.btnPO.Text = "PO";
            this.btnPO.UseVisualStyleBackColor = true;
            this.btnPO.Click += new System.EventHandler(this.btnPO_Click);
            // 
            // btnMaterial
            // 
            this.btnMaterial.Location = new System.Drawing.Point(277, 86);
            this.btnMaterial.Name = "btnMaterial";
            this.btnMaterial.Size = new System.Drawing.Size(55, 23);
            this.btnMaterial.TabIndex = 19;
            this.btnMaterial.Text = "物料";
            this.btnMaterial.UseVisualStyleBackColor = true;
            this.btnMaterial.Click += new System.EventHandler(this.btnMaterial_Click);
            // 
            // btnStorage
            // 
            this.btnStorage.Location = new System.Drawing.Point(216, 86);
            this.btnStorage.Name = "btnStorage";
            this.btnStorage.Size = new System.Drawing.Size(55, 23);
            this.btnStorage.TabIndex = 18;
            this.btnStorage.Text = "库位";
            this.btnStorage.UseVisualStyleBackColor = true;
            this.btnStorage.Click += new System.EventHandler(this.btnStorage_Click);
            // 
            // btnVendor
            // 
            this.btnVendor.Location = new System.Drawing.Point(155, 86);
            this.btnVendor.Name = "btnVendor";
            this.btnVendor.Size = new System.Drawing.Size(55, 23);
            this.btnVendor.TabIndex = 17;
            this.btnVendor.Text = "供应商";
            this.btnVendor.UseVisualStyleBackColor = true;
            this.btnVendor.Click += new System.EventHandler(this.btnVendor_Click);
            // 
            // txtKUNNR
            // 
            this.txtKUNNR.Location = new System.Drawing.Point(49, 88);
            this.txtKUNNR.Name = "txtKUNNR";
            this.txtKUNNR.Size = new System.Drawing.Size(100, 21);
            this.txtKUNNR.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "KUNNR";
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(338, 86);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(46, 23);
            this.btnEnter.TabIndex = 14;
            this.btnEnter.Text = "客户";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // txtSAPROUTER
            // 
            this.txtSAPROUTER.Location = new System.Drawing.Point(48, 61);
            this.txtSAPROUTER.Name = "txtSAPROUTER";
            this.txtSAPROUTER.Size = new System.Drawing.Size(257, 21);
            this.txtSAPROUTER.TabIndex = 13;
            this.txtSAPROUTER.Text = "/H/192.168.129.249/S/3299/H/192.168.129.249";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "SAPROUTER";
            // 
            // txtSYSNR
            // 
            this.txtSYSNR.Location = new System.Drawing.Point(363, 34);
            this.txtSYSNR.Name = "txtSYSNR";
            this.txtSYSNR.Size = new System.Drawing.Size(100, 21);
            this.txtSYSNR.TabIndex = 11;
            this.txtSYSNR.Text = "00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(326, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "SYSNR";
            // 
            // txtASHOST
            // 
            this.txtASHOST.Location = new System.Drawing.Point(206, 34);
            this.txtASHOST.Name = "txtASHOST";
            this.txtASHOST.Size = new System.Drawing.Size(100, 21);
            this.txtASHOST.TabIndex = 9;
            this.txtASHOST.Text = "192.168.129.249";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(169, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "ASHOST";
            // 
            // txtCLIENT
            // 
            this.txtCLIENT.Location = new System.Drawing.Point(49, 34);
            this.txtCLIENT.Name = "txtCLIENT";
            this.txtCLIENT.Size = new System.Drawing.Size(100, 21);
            this.txtCLIENT.TabIndex = 7;
            this.txtCLIENT.Text = "601";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "CLIENT";
            // 
            // txtPASSWD
            // 
            this.txtPASSWD.Location = new System.Drawing.Point(362, 6);
            this.txtPASSWD.Name = "txtPASSWD";
            this.txtPASSWD.Size = new System.Drawing.Size(100, 21);
            this.txtPASSWD.TabIndex = 5;
            this.txtPASSWD.Text = "start567";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "PASSWD";
            // 
            // txtUSER
            // 
            this.txtUSER.Location = new System.Drawing.Point(205, 6);
            this.txtUSER.Name = "txtUSER";
            this.txtUSER.Size = new System.Drawing.Size(100, 21);
            this.txtUSER.TabIndex = 3;
            this.txtUSER.Text = "MESUSER";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "USER";
            // 
            // txtNAME
            // 
            this.txtNAME.Location = new System.Drawing.Point(48, 6);
            this.txtNAME.Name = "txtNAME";
            this.txtNAME.Size = new System.Drawing.Size(100, 21);
            this.txtNAME.TabIndex = 1;
            this.txtNAME.Text = "Q97";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "NAME";
            // 
            // rMsg
            // 
            this.rMsg.Dock = System.Windows.Forms.DockStyle.Top;
            this.rMsg.Location = new System.Drawing.Point(0, 151);
            this.rMsg.Name = "rMsg";
            this.rMsg.Size = new System.Drawing.Size(483, 191);
            this.rMsg.TabIndex = 1;
            this.rMsg.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 342);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 23;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(483, 0);
            this.dataGridView1.TabIndex = 2;
            // 
            // TestSAPRFC_Frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 331);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.rMsg);
            this.Controls.Add(this.panel1);
            this.Name = "TestSAPRFC_Frm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestSAPRFC";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rMsg;
        private System.Windows.Forms.TextBox txtNAME;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSAPROUTER;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSYSNR;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtASHOST;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCLIENT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPASSWD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUSER;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.TextBox txtKUNNR;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnVendor;
        private System.Windows.Forms.Button btnStorage;
        private System.Windows.Forms.Button btnMaterial;
        private System.Windows.Forms.Button btnPO;
        private System.Windows.Forms.Button btnKCMX;
        private System.Windows.Forms.Button btnUB;
        private System.Windows.Forms.Button btnRS;
        private System.Windows.Forms.Button btnDN;
    }
}

