namespace BenQGuru.eMES.Client
{
    partial class FCollectErrorCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCollectErrorCode));
            this.panelInput = new System.Windows.Forms.Panel();
            this.txtResource = new UserControl.UCLabelEdit();
            this.btnExit = new UserControl.UCButton();
            this.btnSave = new UserControl.UCButton();
            this.panelList = new System.Windows.Forms.Panel();
            this.ucErrorCodeSelect21 = new UserControl.UCErrorCodeSelect2();
            this.panelInput.SuspendLayout();
            this.panelList.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelInput
            // 
            this.panelInput.Controls.Add(this.txtResource);
            this.panelInput.Controls.Add(this.btnExit);
            this.panelInput.Controls.Add(this.btnSave);
            this.panelInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelInput.Location = new System.Drawing.Point(0, 282);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(514, 55);
            this.panelInput.TabIndex = 3;
            // 
            // txtResource
            // 
            this.txtResource.AllowEditOnlyChecked = true;
            this.txtResource.Caption = "对应资源";
            this.txtResource.Checked = false;
            this.txtResource.EditType = UserControl.EditTypes.String;
            this.txtResource.Location = new System.Drawing.Point(12, 19);
            this.txtResource.MaxLength = 40;
            this.txtResource.Multiline = false;
            this.txtResource.Name = "txtResource";
            this.txtResource.PasswordChar = '\0';
            this.txtResource.ReadOnly = false;
            this.txtResource.ShowCheckBox = false;
            this.txtResource.Size = new System.Drawing.Size(194, 24);
            this.txtResource.TabIndex = 7;
            this.txtResource.TabNext = false;
            this.txtResource.Value = "";
            this.txtResource.WidthType = UserControl.WidthTypes.Normal;
            this.txtResource.XAlign = 73;
            this.txtResource.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtResource_TxtboxKeyPress);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.ButtonType = UserControl.ButtonTypes.None;
            this.btnExit.Caption = "退出(F9)";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(366, 21);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 6;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.ButtonType = UserControl.ButtonTypes.None;
            this.btnSave.Caption = "保存(F2)";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(240, 21);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 22);
            this.btnSave.TabIndex = 5;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelList
            // 
            this.panelList.Controls.Add(this.ucErrorCodeSelect21);
            this.panelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelList.Location = new System.Drawing.Point(0, 0);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(514, 282);
            this.panelList.TabIndex = 2;
            // 
            // ucErrorCodeSelect21
            // 
            this.ucErrorCodeSelect21.BackColor = System.Drawing.Color.Gainsboro;
            this.ucErrorCodeSelect21.Location = new System.Drawing.Point(0, 0);
            this.ucErrorCodeSelect21.Name = "ucErrorCodeSelect21";
            this.ucErrorCodeSelect21.Size = new System.Drawing.Size(512, 280);
            this.ucErrorCodeSelect21.TabIndex = 1;
            // 
            // FCollectErrorCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 337);
            this.Controls.Add(this.panelList);
            this.Controls.Add(this.panelInput);
            this.KeyPreview = true;
            this.Name = "FCollectErrorCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "不良采集";
            this.Load += new System.EventHandler(this.FCollectErrorCode_Load);
            this.Activated += new System.EventHandler(this.FCollectErrorCode_Activated);
            this.panelInput.ResumeLayout(false);
            this.panelList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.Panel panelList;
        private UserControl.UCButton btnSave;
        private UserControl.UCButton btnExit;
        public UserControl.UCLabelEdit txtResource;
        public UserControl.UCErrorCodeSelect2 ucErrorCodeSelect21;
    }
}