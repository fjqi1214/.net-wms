namespace BenQGuru.eMES.WatchPanelDisToLine
{
    partial class DisToLineGridShow
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridMaterial = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.hearMessageControl = new BenQGuru.eMES.WatchPanelDisToLine.HearMessageControl();
            this.exceptionMessageControl = new BenQGuru.eMES.WatchPanelDisToLine.ExceptionMessageControl();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMaterial)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dataGridMaterial
            // 
            this.dataGridMaterial.AllowUserToAddRows = false;
            this.dataGridMaterial.AllowUserToDeleteRows = false;
            this.dataGridMaterial.AllowUserToResizeRows = false;
            this.dataGridMaterial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridMaterial.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGridMaterial.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridMaterial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridMaterial.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridMaterial.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridMaterial.Location = new System.Drawing.Point(0, 104);
            this.dataGridMaterial.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridMaterial.MultiSelect = false;
            this.dataGridMaterial.Name = "dataGridMaterial";
            this.dataGridMaterial.ReadOnly = true;
            this.dataGridMaterial.RowHeadersVisible = false;
            this.dataGridMaterial.RowTemplate.Height = 23;
            this.dataGridMaterial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridMaterial.Size = new System.Drawing.Size(808, 449);
            this.dataGridMaterial.TabIndex = 5;
            this.dataGridMaterial.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridMaterial_CellFormatting);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.hearMessageControl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.exceptionMessageControl, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridMaterial, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.30741F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.30741F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.24593F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(808, 553);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // hearMessageControl
            // 
            this.hearMessageControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(47)))), ((int)(((byte)(42)))));
            this.hearMessageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hearMessageControl.Location = new System.Drawing.Point(0, 0);
            this.hearMessageControl.Margin = new System.Windows.Forms.Padding(0);
            this.hearMessageControl.Name = "hearMessageControl";
            this.hearMessageControl.Size = new System.Drawing.Size(808, 52);
            this.hearMessageControl.TabIndex = 0;
            // 
            // exceptionMessageControl
            // 
            this.exceptionMessageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exceptionMessageControl.ExceptionMessage = "";
            this.exceptionMessageControl.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exceptionMessageControl.ForeColor = System.Drawing.Color.Red;
            this.exceptionMessageControl.Location = new System.Drawing.Point(3, 55);
            this.exceptionMessageControl.MessageColor = System.Drawing.Color.Red;
            this.exceptionMessageControl.Name = "exceptionMessageControl";
            this.exceptionMessageControl.Size = new System.Drawing.Size(802, 46);
            this.exceptionMessageControl.TabIndex = 1;
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // DisToLineGridShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DisToLineGridShow";
            this.Size = new System.Drawing.Size(808, 553);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMaterial)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private HearMessageControl hearMessageControl;
        private System.Windows.Forms.DataGridView dataGridMaterial;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ExceptionMessageControl exceptionMessageControl;
        private System.Windows.Forms.Timer timer2;
    }
}
