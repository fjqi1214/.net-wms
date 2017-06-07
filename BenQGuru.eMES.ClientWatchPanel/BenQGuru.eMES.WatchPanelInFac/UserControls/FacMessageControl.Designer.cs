namespace BenQGuru.eMES.ClientWatchPanel
{
    partial class FacMessageControl
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
            this.richTextMessage = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextMessage
            // 
            this.richTextMessage.BackColor = System.Drawing.Color.Black;
            this.richTextMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextMessage.Location = new System.Drawing.Point(0, 0);
            this.richTextMessage.Name = "richTextMessage";
            this.richTextMessage.ReadOnly = true;
            this.richTextMessage.Size = new System.Drawing.Size(629, 447);
            this.richTextMessage.TabIndex = 0;
            this.richTextMessage.Text = "";
            // 
            // FacMessageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextMessage);
            this.Name = "FacMessageControl";
            this.Size = new System.Drawing.Size(629, 447);
            this.Load += new System.EventHandler(this.FacMessageControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextMessage;
    }
}
