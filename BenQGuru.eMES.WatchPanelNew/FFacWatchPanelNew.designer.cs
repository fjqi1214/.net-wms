namespace BenQGuru.eMES.WatchPanelNew
{
    partial class FFacWatchPanelNew
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
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.AutoTimer = new System.Windows.Forms.Timer(this.components);
            this.WatchTimer = new System.Windows.Forms.Timer(this.components);
            this.ShiftDayTimer = new System.Windows.Forms.Timer(this.components);
            this.WorkShopTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.AutoSize = true;
            this.mainLayout.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 1;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainLayout.Size = new System.Drawing.Size(796, 462);
            this.mainLayout.TabIndex = 1;
            this.mainLayout.Enter += new System.EventHandler(this.mainLayout_Enter);
            // 
            // AutoTimer
            // 
            this.AutoTimer.Tick += new System.EventHandler(this.AutoTimer_Tick);
            // 
            // WatchTimer
            // 
            this.WatchTimer.Tick += new System.EventHandler(this.WatchTimer_Tick);
            // 
            // ShiftDayTimer
            // 
            this.ShiftDayTimer.Tick += new System.EventHandler(this.ShiftDayTimer_Tick);
            // 
            // WorkShopTimer
            // 
            this.WorkShopTimer.Tick += new System.EventHandler(this.WorkShopTimer_Tick);
            // 
            // FFacWatchPanelNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(796, 462);
            this.Controls.Add(this.mainLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FFacWatchPanelNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FFacWatchPanelNew_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FFacWatchPanelNew_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.Timer AutoTimer;
        private System.Windows.Forms.Timer WatchTimer;
        private System.Windows.Forms.Timer ShiftDayTimer;
        private System.Windows.Forms.Timer WorkShopTimer;
    }
}