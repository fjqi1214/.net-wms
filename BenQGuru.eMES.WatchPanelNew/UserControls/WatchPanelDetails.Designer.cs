namespace BenQGuru.eMES.WatchPanelNew
{
    partial class WatchPanelDetails
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelGridData = new System.Windows.Forms.Panel();
            this.dataGridViewProdcut = new System.Windows.Forms.DataGridView();
            this.panelHead = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.GridViewTimer = new System.Windows.Forms.Timer(this.components);
            this.hearMessageControl1 = new BenQGuru.eMES.WatchPanelNew.HearMessageControl();
            this.panelGridData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProdcut)).BeginInit();
            this.panelHead.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGridData
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panelGridData, 2);
            this.panelGridData.Controls.Add(this.dataGridViewProdcut);
            this.panelGridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridData.Location = new System.Drawing.Point(3, 3);
            this.panelGridData.Name = "panelGridData";
            this.panelGridData.Size = new System.Drawing.Size(981, 488);
            this.panelGridData.TabIndex = 1;
            // 
            // dataGridViewProdcut
            // 
            this.dataGridViewProdcut.AllowUserToAddRows = false;
            this.dataGridViewProdcut.AllowUserToDeleteRows = false;
            this.dataGridViewProdcut.AllowUserToResizeRows = false;
            this.dataGridViewProdcut.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewProdcut.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProdcut.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewProdcut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewProdcut.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewProdcut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewProdcut.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridViewProdcut.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewProdcut.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewProdcut.Name = "dataGridViewProdcut";
            this.dataGridViewProdcut.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProdcut.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewProdcut.RowHeadersVisible = false;
            this.dataGridViewProdcut.RowTemplate.Height = 23;
            this.dataGridViewProdcut.Size = new System.Drawing.Size(981, 488);
            this.dataGridViewProdcut.TabIndex = 1;
            // 
            // panelHead
            // 
            this.panelHead.Controls.Add(this.hearMessageControl1);
            this.panelHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHead.Location = new System.Drawing.Point(0, 0);
            this.panelHead.Margin = new System.Windows.Forms.Padding(0);
            this.panelHead.Name = "panelHead";
            this.panelHead.Size = new System.Drawing.Size(987, 83);
            this.panelHead.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.91F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.09F));
            this.tableLayoutPanel1.Controls.Add(this.panelGridData, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 83);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 494F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(987, 494);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // GridViewTimer
            // 
            this.GridViewTimer.Tick += new System.EventHandler(this.GridViewTimer_Tick);
            // 
            // hearMessageControl1
            // 
            this.hearMessageControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hearMessageControl1.Location = new System.Drawing.Point(0, 0);
            this.hearMessageControl1.Margin = new System.Windows.Forms.Padding(0);
            this.hearMessageControl1.Name = "hearMessageControl1";
            this.hearMessageControl1.Size = new System.Drawing.Size(987, 83);
            this.hearMessageControl1.TabIndex = 0;
            // 
            // WatchPanelDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelHead);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "WatchPanelDetails";
            this.Size = new System.Drawing.Size(987, 577);
            this.Load += new System.EventHandler(this.WatchPanelDetails_Load);
            this.panelGridData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProdcut)).EndInit();
            this.panelHead.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGridData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelHead;
        private HearMessageControl hearMessageControl1;
        private System.Windows.Forms.DataGridView dataGridViewProdcut;
        private System.Windows.Forms.Timer GridViewTimer;





    }
}
