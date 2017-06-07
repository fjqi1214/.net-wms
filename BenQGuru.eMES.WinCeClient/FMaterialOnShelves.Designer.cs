namespace BenQGuru.eMES.WinCeClient
{
    partial class FMaterialOnShelves
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.lbllocationCode = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCartonNo = new System.Windows.Forms.Label();
            this.lblDQMcodeEdite = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDQMcode = new System.Windows.Forms.Label();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLocationCode = new System.Windows.Forms.TextBox();
            this.txtCartonNo = new System.Windows.Forms.TextBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnOnShelves = new System.Windows.Forms.Button();
            this.lblPlanQTY = new System.Windows.Forms.Label();
            this.lblActQTY = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbllocationCode
            // 
            this.lbllocationCode.Location = new System.Drawing.Point(3, 9);
            this.lbllocationCode.Name = "lbllocationCode";
            this.lbllocationCode.Size = new System.Drawing.Size(58, 20);
            this.lbllocationCode.Text = "货位号";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(135, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "label2";
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(3, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "消息提示：";
            // 
            // lblCartonNo
            // 
            this.lblCartonNo.Location = new System.Drawing.Point(3, 32);
            this.lblCartonNo.Name = "lblCartonNo";
            this.lblCartonNo.Size = new System.Drawing.Size(39, 20);
            this.lblCartonNo.Text = "箱号";
            // 
            // lblDQMcodeEdite
            // 
            this.lblDQMcodeEdite.Location = new System.Drawing.Point(123, 159);
            this.lblDQMcodeEdite.Name = "lblDQMcodeEdite";
            this.lblDQMcodeEdite.Size = new System.Drawing.Size(100, 20);
            this.lblDQMcodeEdite.Text = "label7";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(109, 235);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.Text = "label8";
            // 
            // lblDQMcode
            // 
            this.lblDQMcode.Location = new System.Drawing.Point(0, 159);
            this.lblDQMcode.Name = "lblDQMcode";
            this.lblDQMcode.Size = new System.Drawing.Size(114, 20);
            this.lblDQMcode.Text = "华为物料编码：";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(3, 61);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(232, 95);
            this.dataGrid1.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblActQTY);
            this.panel1.Controls.Add(this.lblPlanQTY);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 182);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 50);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.Text = "应上架：";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(107, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.Text = "已上架：";
            // 
            // txtLocationCode
            // 
            this.txtLocationCode.Location = new System.Drawing.Point(67, 3);
            this.txtLocationCode.Name = "txtLocationCode";
            this.txtLocationCode.Size = new System.Drawing.Size(156, 23);
            this.txtLocationCode.TabIndex = 19;
            // 
            // txtCartonNo
            // 
            this.txtCartonNo.Location = new System.Drawing.Point(67, 32);
            this.txtCartonNo.Name = "txtCartonNo";
            this.txtCartonNo.Size = new System.Drawing.Size(156, 23);
            this.txtCartonNo.TabIndex = 20;
            this.txtCartonNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCartonNo_KeyPress);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(13, 265);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(72, 20);
            this.btnReturn.TabIndex = 21;
            this.btnReturn.Text = "返 回";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnOnShelves
            // 
            this.btnOnShelves.Location = new System.Drawing.Point(123, 265);
            this.btnOnShelves.Name = "btnOnShelves";
            this.btnOnShelves.Size = new System.Drawing.Size(72, 20);
            this.btnOnShelves.TabIndex = 22;
            this.btnOnShelves.Text = "上 架";
            this.btnOnShelves.Click += new System.EventHandler(this.btnOnShelves_Click);
            // 
            // lblPlanQTY
            // 
            this.lblPlanQTY.Location = new System.Drawing.Point(56, 17);
            this.lblPlanQTY.Name = "lblPlanQTY";
            this.lblPlanQTY.Size = new System.Drawing.Size(73, 20);
            this.lblPlanQTY.Text = "0";
            // 
            // lblActQTY
            // 
            this.lblActQTY.Location = new System.Drawing.Point(165, 17);
            this.lblActQTY.Name = "lblActQTY";
            this.lblActQTY.Size = new System.Drawing.Size(73, 20);
            this.lblActQTY.Text = "0";
            // 
            // FMaterialOnShelves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.btnOnShelves);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.txtCartonNo);
            this.Controls.Add(this.txtLocationCode);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.lblDQMcode);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblDQMcodeEdite);
            this.Controls.Add(this.lblCartonNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbllocationCode);
            this.Menu = this.mainMenu1;
            this.Name = "FMaterialOnShelves";
            this.Text = "物料上架";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbllocationCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCartonNo;
        private System.Windows.Forms.Label lblDQMcodeEdite;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDQMcode;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLocationCode;
        private System.Windows.Forms.TextBox txtCartonNo;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnOnShelves;
        private System.Windows.Forms.Label lblActQTY;
        private System.Windows.Forms.Label lblPlanQTY;
    }
}