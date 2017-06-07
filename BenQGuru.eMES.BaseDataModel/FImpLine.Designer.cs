namespace BenQGuru.eMES.BaseDataModel
{
    partial class FImpLine
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FImpLine));
            this.btnGenData = new UserControl.UCButton();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnGenData);
            this.panelBottom.Location = new System.Drawing.Point(0, 466);
            this.panelBottom.Size = new System.Drawing.Size(676, 64);
            this.panelBottom.Controls.SetChildIndex(this.btnGenData, 0);
            // 
            // panelMain
            // 
            this.panelMain.Size = new System.Drawing.Size(676, 450);
            // 
            // btnGenData
            // 
            this.btnGenData.BackColor = System.Drawing.SystemColors.Control;
            this.btnGenData.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGenData.BackgroundImage")));
            this.btnGenData.ButtonType = UserControl.ButtonTypes.None;
            this.btnGenData.Caption = "生成数据";
            this.btnGenData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenData.Location = new System.Drawing.Point(552, 20);
            this.btnGenData.Name = "btnGenData";
            this.btnGenData.Size = new System.Drawing.Size(88, 22);
            this.btnGenData.TabIndex = 12;
            this.btnGenData.Click += new System.EventHandler(this.btnGenData_Click);
            // 
            // FImpLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 530);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FImpLine";
            this.Text = "FImpLine";
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.UCButton btnGenData;
    }
}