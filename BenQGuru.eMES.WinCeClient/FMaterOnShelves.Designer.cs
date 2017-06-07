namespace BenQGuru.eMES.WinCeClient
{
    partial class FMaterOnShelves
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblActQTY = new System.Windows.Forms.Label();
            this.lblPlanQTY = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLocationNo = new System.Windows.Forms.TextBox();
            this.txtCartonNo = new System.Windows.Forms.TextBox();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.Check = new System.Windows.Forms.DataGridTextBoxColumn();
            this.STNO = new System.Windows.Forms.DataGridTextBoxColumn();
            this.cartonno = new System.Windows.Forms.DataGridTextBoxColumn();
            this.relocaNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.locaNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DQMCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.stline = new System.Windows.Forms.DataGridTextBoxColumn();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.lblDQMcodeEdite = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.Text = "货位号";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 20);
            this.label2.Text = "箱号";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 20);
            this.label3.Text = "华为编码：";
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(82, 316);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(100, 20);
            this.lblMessage.Text = "X";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblActQTY);
            this.panel1.Controls.Add(this.lblPlanQTY);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(0, 260);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 37);
            this.panel1.GotFocus += new System.EventHandler(this.panel1_GotFocus);
            // 
            // lblActQTY
            // 
            this.lblActQTY.Location = new System.Drawing.Point(184, 15);
            this.lblActQTY.Name = "lblActQTY";
            this.lblActQTY.Size = new System.Drawing.Size(37, 20);
            this.lblActQTY.Text = "0";
            // 
            // lblPlanQTY
            // 
            this.lblPlanQTY.Location = new System.Drawing.Point(74, 15);
            this.lblPlanQTY.Name = "lblPlanQTY";
            this.lblPlanQTY.Size = new System.Drawing.Size(25, 20);
            this.lblPlanQTY.Text = "0";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(109, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 20);
            this.label7.Text = "已上架：";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(7, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 20);
            this.label6.Text = "应上架：";
            // 
            // label10
            // 
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(7, 316);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 20);
            this.label10.Text = "消息提示：";
            // 
            // txtLocationNo
            // 
            this.txtLocationNo.Location = new System.Drawing.Point(67, 3);
            this.txtLocationNo.Name = "txtLocationNo";
            this.txtLocationNo.Size = new System.Drawing.Size(120, 23);
            this.txtLocationNo.TabIndex = 11;
            // 
            // txtCartonNo
            // 
            this.txtCartonNo.Location = new System.Drawing.Point(42, 36);
            this.txtCartonNo.Name = "txtCartonNo";
            this.txtCartonNo.Size = new System.Drawing.Size(179, 23);
            this.txtCartonNo.TabIndex = 12;
            this.txtCartonNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCartonNo_KeyPress);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(3, 65);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(229, 156);
            this.dataGrid1.TabIndex = 13;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.Check);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.STNO);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.cartonno);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.relocaNo);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.locaNo);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.DQMCode);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.stline);
            // 
            // Check
            // 
            this.Check.Format = "";
            this.Check.FormatInfo = null;
            this.Check.HeaderText = "选中";
            this.Check.Width = 30;
            // 
            // STNO
            // 
            this.STNO.Format = "";
            this.STNO.FormatInfo = null;
            this.STNO.HeaderText = "入库指令号";
            // 
            // cartonno
            // 
            this.cartonno.Format = "";
            this.cartonno.FormatInfo = null;
            this.cartonno.HeaderText = "箱号";
            // 
            // relocaNo
            // 
            this.relocaNo.Format = "";
            this.relocaNo.FormatInfo = null;
            this.relocaNo.HeaderText = "推荐货位";
            // 
            // locaNo
            // 
            this.locaNo.Format = "";
            this.locaNo.FormatInfo = null;
            this.locaNo.HeaderText = "货位";
            // 
            // DQMCode
            // 
            this.DQMCode.Format = "";
            this.DQMCode.FormatInfo = null;
            this.DQMCode.HeaderText = "鼎桥物料号";
            // 
            // stline
            // 
            this.stline.Format = "";
            this.stline.FormatInfo = null;
            this.stline.HeaderText = "行";
            this.stline.Width = 10;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(184, 227);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(48, 27);
            this.btnSubmit.TabIndex = 14;
            this.btnSubmit.Text = "提 交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(184, 309);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(48, 27);
            this.btnReturn.TabIndex = 15;
            this.btnReturn.Text = "返 回";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // lblDQMcodeEdite
            // 
            this.lblDQMcodeEdite.Location = new System.Drawing.Point(87, 227);
            this.lblDQMcodeEdite.Name = "lblDQMcodeEdite";
            this.lblDQMcodeEdite.Size = new System.Drawing.Size(100, 20);
            this.lblDQMcodeEdite.Text = "x";
            // 
            // FMaterOnShelves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.Controls.Add(this.lblDQMcodeEdite);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.txtCartonNo);
            this.Controls.Add(this.txtLocationNo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FMaterOnShelves";
            this.Size = new System.Drawing.Size(236, 346);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblActQTY;
        private System.Windows.Forms.Label lblPlanQTY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtLocationNo;
        private System.Windows.Forms.TextBox txtCartonNo;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label lblDQMcodeEdite;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn Check;
        private System.Windows.Forms.DataGridTextBoxColumn STNO;
        private System.Windows.Forms.DataGridTextBoxColumn cartonno;
        private System.Windows.Forms.DataGridTextBoxColumn relocaNo;
        private System.Windows.Forms.DataGridTextBoxColumn locaNo;
        private System.Windows.Forms.DataGridTextBoxColumn DQMCode;
        private System.Windows.Forms.DataGridTextBoxColumn stline;
    }
}
