namespace BenQGuru.eMES.WatchPanelNew
{
    partial class ExceptionMessageControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtHead = new System.Windows.Forms.TextBox();
            this.txtExpection = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtHead, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtExpection, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.38461F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.61539F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(642, 132);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtHead
            // 
            this.txtHead.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(47)))), ((int)(((byte)(42)))));
            this.txtHead.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHead.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.txtHead.ForeColor = System.Drawing.Color.White;
            this.txtHead.Location = new System.Drawing.Point(1, 1);
            this.txtHead.Margin = new System.Windows.Forms.Padding(0);
            this.txtHead.Multiline = true;
            this.txtHead.Name = "txtHead";
            this.txtHead.Size = new System.Drawing.Size(640, 32);
            this.txtHead.TabIndex = 1;
            this.txtHead.Text = "异常信息(Exception information)";
            this.txtHead.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtExpection
            // 
            this.txtExpection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(47)))), ((int)(((byte)(42)))));
            this.txtExpection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExpection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExpection.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpection.ForeColor = System.Drawing.Color.White;
            this.txtExpection.Location = new System.Drawing.Point(1, 33);
            this.txtExpection.Margin = new System.Windows.Forms.Padding(0);
            this.txtExpection.Multiline = true;
            this.txtExpection.Name = "txtExpection";
            this.txtExpection.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExpection.Size = new System.Drawing.Size(640, 98);
            this.txtExpection.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ExceptionMessageControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ExceptionMessageControl";
            this.Size = new System.Drawing.Size(642, 132);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtHead;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtExpection;
        private System.Windows.Forms.Timer timer1;

    }
}
