namespace BenQGuru.eMES.WinCeClient
{
    partial class FIQCExceptionPic
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
        
            this.SuspendLayout();
            // 
            // fUploadPicture1
            // 
         
            // 
            // FPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(239, 317);
            this.Controls.Add(this.fUploadPicture1);
            this.Menu = this.mainMenu1;
            this.Name = "FIQCExceptionPic";
            this.Text = "IQC异常图片上传";
            this.ResumeLayout(false);

        }

        #endregion

        private FUploadPicture fUploadPicture1;
    }
}