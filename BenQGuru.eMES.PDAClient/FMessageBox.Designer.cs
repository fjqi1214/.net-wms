namespace BenQGuru.eMES.PDAClient
{
    partial class FMessageBox
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
            this.ultraButtonOut = new Infragistics.Win.Misc.UltraButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ultraButtonOut
            // 
            this.ultraButtonOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ultraButtonOut.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraButtonOut.Location = new System.Drawing.Point(115, 71);
            this.ultraButtonOut.Name = "ultraButtonOut";
            this.ultraButtonOut.Size = new System.Drawing.Size(60, 22);
            this.ultraButtonOut.TabIndex = 15;
            this.ultraButtonOut.Text = "确定";
            this.ultraButtonOut.Click += new System.EventHandler(this.ultraButtonOut_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Arial", 10F);
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(266, 51);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "";
            // 
            // FMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 99);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.ultraButtonOut);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Name = "FMessageBox";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "消息框";
            this.Load += new System.EventHandler(this.FMessageBox_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraButton ultraButtonOut;
        private System.Windows.Forms.RichTextBox richTextBox1;

    }
}