namespace UserControl
{
    partial class UCErrorCodeSelectNew
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ultraGridErrorList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ucLabelEditErrorCode = new UserControl.UCLabelEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridErrorList)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraGridErrorList
            // 
            this.ultraGridErrorList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGridErrorList.Location = new System.Drawing.Point(3, 34);
            this.ultraGridErrorList.Name = "ultraGridErrorList";
            this.ultraGridErrorList.Size = new System.Drawing.Size(350, 226);
            this.ultraGridErrorList.TabIndex = 3;
            this.ultraGridErrorList.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;
            this.ultraGridErrorList.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridErrorList_InitializeLayout);
            // 
            // ucLabelEditErrorCode
            // 
            this.ucLabelEditErrorCode.AllowEditOnlyChecked = true;
            this.ucLabelEditErrorCode.Caption = "不良代码";
            this.ucLabelEditErrorCode.Checked = false;
            this.ucLabelEditErrorCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditErrorCode.Location = new System.Drawing.Point(3, 5);
            this.ucLabelEditErrorCode.MaxLength = 40;
            this.ucLabelEditErrorCode.Multiline = false;
            this.ucLabelEditErrorCode.Name = "ucLabelEditErrorCode";
            this.ucLabelEditErrorCode.PasswordChar = '\0';
            this.ucLabelEditErrorCode.ReadOnly = false;
            this.ucLabelEditErrorCode.ShowCheckBox = false;
            this.ucLabelEditErrorCode.Size = new System.Drawing.Size(194, 24);
            this.ucLabelEditErrorCode.TabIndex = 4;
            this.ucLabelEditErrorCode.TabNext = false;
            this.ucLabelEditErrorCode.Value = "";
            this.ucLabelEditErrorCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditErrorCode.XAlign = 64;
            this.ucLabelEditErrorCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditErrorCode_TxtboxKeyPress);
            // 
            // UCErrorCodeSelectNew
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.ucLabelEditErrorCode);
            this.Controls.Add(this.ultraGridErrorList);
            this.Name = "UCErrorCodeSelectNew";
            this.Size = new System.Drawing.Size(356, 263);
            this.Load += new System.EventHandler(this.UCErrorCodeSelectNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridErrorList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public UCLabelEdit ucLabelEditErrorCode;
        public Infragistics.Win.UltraWinGrid.UltraGrid ultraGridErrorList;
    }
}
