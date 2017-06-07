namespace BenQGuru.eMES.WinCeClient
{
    partial class FLocStorTransMP
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rdoAllCarton = new System.Windows.Forms.RadioButton();
            this.rdoSplitCarton = new System.Windows.Forms.RadioButton();
            this.txtNumEdit = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtSNEdit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.txtOriginalCartonEdit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtLocationNoEdit = new System.Windows.Forms.TextBox();
            this.btnView = new System.Windows.Forms.Button();
            this.txtTLocationCodeEdit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTLocationCartonEdit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 20);
            this.label1.Text = "货位移动单号";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "数量";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(21, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "SN";
            // 
            // rdoAllCarton
            // 
            this.rdoAllCarton.Location = new System.Drawing.Point(21, 71);
            this.rdoAllCarton.Name = "rdoAllCarton";
            this.rdoAllCarton.Size = new System.Drawing.Size(100, 20);
            this.rdoAllCarton.TabIndex = 1;
            this.rdoAllCarton.Text = "整箱";
            this.rdoAllCarton.CheckedChanged += new System.EventHandler(this.rdoAllCarton_CheckedChanged);
            // 
            // rdoSplitCarton
            // 
            this.rdoSplitCarton.Location = new System.Drawing.Point(127, 71);
            this.rdoSplitCarton.Name = "rdoSplitCarton";
            this.rdoSplitCarton.Size = new System.Drawing.Size(100, 20);
            this.rdoSplitCarton.TabIndex = 2;
            this.rdoSplitCarton.Text = "拆箱";
            this.rdoSplitCarton.CheckedChanged += new System.EventHandler(this.rdoSplitCarton_CheckedChanged);
            // 
            // txtNumEdit
            // 
            this.txtNumEdit.Location = new System.Drawing.Point(49, 121);
            this.txtNumEdit.Name = "txtNumEdit";
            this.txtNumEdit.Size = new System.Drawing.Size(99, 23);
            this.txtNumEdit.TabIndex = 4;
            this.txtNumEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumEdit_KeyPress);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(164, 237);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(72, 20);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtSNEdit
            // 
            this.txtSNEdit.Location = new System.Drawing.Point(49, 151);
            this.txtSNEdit.Name = "txtSNEdit";
            this.txtSNEdit.Size = new System.Drawing.Size(186, 23);
            this.txtSNEdit.TabIndex = 5;
            this.txtSNEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSNEdit_KeyPress);
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(1, 257);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.Text = "提示信息：";
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(73, 257);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(157, 20);
            this.lblMessage.Text = "XXX";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(165, 275);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(72, 20);
            this.btnBack.TabIndex = 72;
            this.btnBack.Text = "返回";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtOriginalCartonEdit
            // 
            this.txtOriginalCartonEdit.Location = new System.Drawing.Point(60, 94);
            this.txtOriginalCartonEdit.Name = "txtOriginalCartonEdit";
            this.txtOriginalCartonEdit.Size = new System.Drawing.Size(173, 23);
            this.txtOriginalCartonEdit.TabIndex = 74;
            this.txtOriginalCartonEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOriginalCartonEdit_KeyPress);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.Text = "原箱号";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(21, 39);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(54, 20);
            this.btnCreate.TabIndex = 77;
            this.btnCreate.Text = "创建";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtLocationNoEdit
            // 
            this.txtLocationNoEdit.Location = new System.Drawing.Point(106, 16);
            this.txtLocationNoEdit.Name = "txtLocationNoEdit";
            this.txtLocationNoEdit.Size = new System.Drawing.Size(129, 23);
            this.txtLocationNoEdit.TabIndex = 76;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(80, 275);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(72, 20);
            this.btnView.TabIndex = 78;
            this.btnView.Text = "查看";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtTLocationCodeEdit
            // 
            this.txtTLocationCodeEdit.Location = new System.Drawing.Point(96, 179);
            this.txtTLocationCodeEdit.Name = "txtTLocationCodeEdit";
            this.txtTLocationCodeEdit.Size = new System.Drawing.Size(139, 23);
            this.txtTLocationCodeEdit.TabIndex = 80;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(21, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 20);
            this.label7.Text = "目标货位";
            // 
            // txtTLocationCartonEdit
            // 
            this.txtTLocationCartonEdit.Location = new System.Drawing.Point(96, 206);
            this.txtTLocationCartonEdit.Name = "txtTLocationCartonEdit";
            this.txtTLocationCartonEdit.Size = new System.Drawing.Size(139, 23);
            this.txtTLocationCartonEdit.TabIndex = 83;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(21, 209);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.Text = "目标箱号";
            // 
            // FLocStorTransMP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.Controls.Add(this.txtTLocationCartonEdit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtTLocationCodeEdit);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtLocationNoEdit);
            this.Controls.Add(this.txtOriginalCartonEdit);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSNEdit);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtNumEdit);
            this.Controls.Add(this.rdoSplitCarton);
            this.Controls.Add(this.rdoAllCarton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "FLocStorTransMP";
            this.Size = new System.Drawing.Size(240, 320);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdoAllCarton;
        private System.Windows.Forms.RadioButton rdoSplitCarton;
        private System.Windows.Forms.TextBox txtNumEdit;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtSNEdit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtOriginalCartonEdit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtLocationNoEdit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.TextBox txtTLocationCodeEdit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTLocationCartonEdit;
        private System.Windows.Forms.Label label8;
    }
}
