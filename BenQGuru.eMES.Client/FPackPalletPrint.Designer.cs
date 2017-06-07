namespace BenQGuru.eMES.Client
{
    partial class FPackPalletPrint
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

        private System.Windows.Forms.Panel panelReportViewer;
        private System.Windows.Forms.Panel panelButton;
        private UserControl.UCButton btnExit;
        private Microsoft.Reporting.WinForms.ReportViewer ReportViewerPallet;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPackPalletPrint));
            this.panelReportViewer = new System.Windows.Forms.Panel();
            this.btnPrint = new UserControl.UCButton();
            this.btnExit = new UserControl.UCButton();
            this.panelButton = new System.Windows.Forms.Panel();
            this.ReportViewerPallet = new Microsoft.Reporting.WinForms.ReportViewer();
            this.timerAutoPrint = new System.Windows.Forms.Timer(this.components);
            this.panelReportViewer.SuspendLayout();
            this.panelButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelReportViewer
            // 
            this.panelReportViewer.Controls.Add(this.btnPrint);
            this.panelReportViewer.Controls.Add(this.btnExit);
            this.panelReportViewer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelReportViewer.Location = new System.Drawing.Point(0, 425);
            this.panelReportViewer.Name = "panelReportViewer";
            this.panelReportViewer.Size = new System.Drawing.Size(820, 57);
            this.panelReportViewer.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.ButtonType = UserControl.ButtonTypes.None;
            this.btnPrint.Caption = "´òÓ¡";
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.Location = new System.Drawing.Point(250, 16);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(88, 24);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.ButtonType = UserControl.ButtonTypes.None;
            this.btnExit.Caption = "ÍË³ö";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(501, 16);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 24);
            this.btnExit.TabIndex = 2;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panelButton
            // 
            this.panelButton.Controls.Add(this.ReportViewerPallet);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButton.Location = new System.Drawing.Point(0, 0);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(820, 425);
            this.panelButton.TabIndex = 1;
            // 
            // ReportViewerPallet
            // 
            this.ReportViewerPallet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportViewerPallet.Location = new System.Drawing.Point(0, 0);
            this.ReportViewerPallet.Name = "ReportViewerPallet";
            this.ReportViewerPallet.ShowToolBar = false;
            this.ReportViewerPallet.Size = new System.Drawing.Size(820, 425);
            this.ReportViewerPallet.TabIndex = 0;
            // 
            // timerAutoPrint
            // 
            this.timerAutoPrint.Interval = 300;
            this.timerAutoPrint.Tick += new System.EventHandler(this.timerAutoPrint_Tick);
            // 
            // FPackPalletPrint
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(820, 482);
            this.Controls.Add(this.panelButton);
            this.Controls.Add(this.panelReportViewer);
            this.Name = "FPackPalletPrint";
            this.Text = "Pallet´òÓ¡";
            this.Load += new System.EventHandler(this.FPackPalletPrint_Load);
            this.panelReportViewer.ResumeLayout(false);
            this.panelButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCButton btnPrint;
        private System.Windows.Forms.Timer timerAutoPrint;
    }
}