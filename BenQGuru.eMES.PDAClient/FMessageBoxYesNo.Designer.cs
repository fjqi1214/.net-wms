namespace BenQGuru.eMES.PDAClient
{
    partial class FMessageBoxYesNo
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
            this.ultraButtonYes = new Infragistics.Win.Misc.UltraButton();
            this.label1 = new System.Windows.Forms.Label();
            this.ultraButtonNo = new Infragistics.Win.Misc.UltraButton();
            this.SuspendLayout();
            // 
            // ultraButtonYes
            // 
            this.ultraButtonYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ultraButtonYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraButtonYes.Location = new System.Drawing.Point(76, 50);
            this.ultraButtonYes.Name = "ultraButtonYes";
            this.ultraButtonYes.Size = new System.Drawing.Size(60, 22);
            this.ultraButtonYes.TabIndex = 15;
            this.ultraButtonYes.Text = "是";
            this.ultraButtonYes.Click += new System.EventHandler(this.ultraButtonYes_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "消息框";
            // 
            // ultraButtonNo
            // 
            this.ultraButtonNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ultraButtonNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraButtonNo.Location = new System.Drawing.Point(171, 50);
            this.ultraButtonNo.Name = "ultraButtonNo";
            this.ultraButtonNo.Size = new System.Drawing.Size(60, 22);
            this.ultraButtonNo.TabIndex = 1;
            this.ultraButtonNo.Text = "否";
            this.ultraButtonNo.Click += new System.EventHandler(this.ultraButtonNo_Click);
            // 
            // FMessageBoxYesNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 74);
            this.Controls.Add(this.ultraButtonNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ultraButtonYes);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FMessageBoxYesNo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "消息框";
            this.Load += new System.EventHandler(this.FMessageBoxYesNo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.Misc.UltraButton ultraButtonYes;
        private System.Windows.Forms.Label label1;
        private Infragistics.Win.Misc.UltraButton ultraButtonNo;

    }
}