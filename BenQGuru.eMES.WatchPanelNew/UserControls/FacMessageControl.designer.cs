namespace BenQGuru.eMES.WatchPanelNew
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
            this.pictureBoxBACK = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBACK)).BeginInit();
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
            this.richTextMessage.Size = new System.Drawing.Size(629, 413);
            this.richTextMessage.TabIndex = 0;
            this.richTextMessage.Text = "";
            // 
            // pictureBoxBACK
            // 
            this.pictureBoxBACK.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxBACK.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBACK.Name = "pictureBoxBACK";
            this.pictureBoxBACK.Size = new System.Drawing.Size(629, 413);
            this.pictureBoxBACK.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBACK.TabIndex = 1;
            this.pictureBoxBACK.TabStop = false;
            this.pictureBoxBACK.WaitOnLoad = true;
            // 
            // FacMessageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextMessage);
            this.Controls.Add(this.pictureBoxBACK);
            this.Name = "FacMessageControl";
            this.Size = new System.Drawing.Size(629, 413);
            this.Load += new System.EventHandler(this.FacMessageControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBACK)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextMessage;
        private System.Windows.Forms.PictureBox pictureBoxBACK;
    }
}
