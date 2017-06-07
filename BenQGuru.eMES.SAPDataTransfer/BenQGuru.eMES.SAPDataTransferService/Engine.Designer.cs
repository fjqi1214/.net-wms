namespace BenQGuru.eMES.SAPDataTransferService
{
    partial class Engine
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
            if (disposing)
            {
                if (m_Watcher != null)
                {
                    m_Watcher.EnableRaisingEvents = false;
                    m_Watcher.Dispose();
                }
                if (serviceTimers != null)
                {
                    foreach (ServiceTimer timer in serviceTimers)
                    {
                        timer.Dispose();
                    }
                }
                if (scheduledServiceTimers != null)
                {
                    foreach (ScheduledServiceTimer timer in scheduledServiceTimers)
                    {
                        timer.Dispose();
                    }
                }
                if (components != null)
                {
                    components.Dispose();
                }                
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
            components = new System.ComponentModel.Container();
            this.ServiceName = "BenQGuru.eMES.SAPDataTransfer";
        }

        #endregion
    }
}
