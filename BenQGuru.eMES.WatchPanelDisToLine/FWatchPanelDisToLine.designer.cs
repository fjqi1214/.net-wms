namespace BenQGuru.eMES.WatchPanelDisToLine
{
    partial class FWatchPanelDisToLine
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
            this.Autotimer = new System.Windows.Forms.Timer(this.components);
            this.MainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // Autotimer
            // 
            this.Autotimer.Tick += new System.EventHandler(this.Autotimer_Tick);
            // 
            // MainLayout
            // 
            this.MainLayout.ColumnCount = 1;
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayout.Location = new System.Drawing.Point(0, 0);
            this.MainLayout.Margin = new System.Windows.Forms.Padding(0);
            this.MainLayout.Name = "MainLayout";
            this.MainLayout.RowCount = 1;
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainLayout.Size = new System.Drawing.Size(959, 687);
            this.MainLayout.TabIndex = 0;
            // 
            // FWatchPanelDisToLine
            // 
            this.ClientSize = new System.Drawing.Size(959, 687);
            this.ControlBox = false;
            this.Controls.Add(this.MainLayout);
            this.KeyPreview = true;
            this.Name = "FWatchPanelDisToLine";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FWatchPanelMaterial_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.Timer Autotimer;
        private System.Windows.Forms.TableLayoutPanel MainLayout;


    }
}

