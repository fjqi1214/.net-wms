namespace BenQGuru.eMES.WinCeClient
{
    partial class FCollectBasketGood
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
            this.btn_exit = new System.Windows.Forms.Button();
            this.txtmessage = new System.Windows.Forms.TextBox();
            this.txtGoodCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(82, 219);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(72, 20);
            this.btn_exit.TabIndex = 7;
            this.btn_exit.Text = "退出";
            this.btn_exit.Visible = false;
            // 
            // txtmessage
            // 
            this.txtmessage.Location = new System.Drawing.Point(5, 80);
            this.txtmessage.Multiline = true;
            this.txtmessage.Name = "txtmessage";
            this.txtmessage.Size = new System.Drawing.Size(232, 93);
            this.txtmessage.TabIndex = 6;
            // 
            // txtGoodCode
            // 
            this.txtGoodCode.Location = new System.Drawing.Point(81, 35);
            this.txtGoodCode.Name = "txtGoodCode";
            this.txtGoodCode.Size = new System.Drawing.Size(155, 23);
            this.txtGoodCode.TabIndex = 5;
            this.txtGoodCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGoodCode_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "良品条码";
            // 
            // FCollectBasketGood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.txtmessage);
            this.Controls.Add(this.txtGoodCode);
            this.Controls.Add(this.label2);
            this.Name = "FCollectBasketGood";
            this.Size = new System.Drawing.Size(240, 285);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FCollectBasketGood_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.TextBox txtmessage;
        private System.Windows.Forms.TextBox txtGoodCode;
        private System.Windows.Forms.Label label2;

    }
}
