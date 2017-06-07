namespace BenQGuru.eMES.BaseDataModel
{
    partial class FAutoTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FAutoTemplate));
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.lblStart = new System.Windows.Forms.Label();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.lblEnd = new System.Windows.Forms.Label();
            this.txtSeqLength = new System.Windows.Forms.TextBox();
            this.lblSeqLength = new System.Windows.Forms.Label();
            this.lblGenData = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnGenerate = new UserControl.UCButton();
            this.btnOK = new UserControl.UCButton();
            this.btnCancel = new UserControl.UCButton();
            this.SuspendLayout();
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(40, 21);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(53, 12);
            this.lblPrefix.TabIndex = 0;
            this.lblPrefix.Text = "数据前缀";
            // 
            // txtPrefix
            // 
            this.txtPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrefix.Location = new System.Drawing.Point(99, 12);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(128, 21);
            this.txtPrefix.TabIndex = 1;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(40, 49);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(53, 12);
            this.lblStart.TabIndex = 2;
            this.lblStart.Text = "序列起始";
            // 
            // txtStart
            // 
            this.txtStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStart.Location = new System.Drawing.Point(99, 40);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(128, 21);
            this.txtStart.TabIndex = 3;
            // 
            // txtEnd
            // 
            this.txtEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEnd.Location = new System.Drawing.Point(99, 67);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(128, 21);
            this.txtEnd.TabIndex = 5;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(40, 76);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(53, 12);
            this.lblEnd.TabIndex = 4;
            this.lblEnd.Text = "序列结束";
            // 
            // txtSeqLength
            // 
            this.txtSeqLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSeqLength.Location = new System.Drawing.Point(99, 94);
            this.txtSeqLength.Name = "txtSeqLength";
            this.txtSeqLength.Size = new System.Drawing.Size(128, 21);
            this.txtSeqLength.TabIndex = 7;
            // 
            // lblSeqLength
            // 
            this.lblSeqLength.AutoSize = true;
            this.lblSeqLength.Location = new System.Drawing.Point(40, 103);
            this.lblSeqLength.Name = "lblSeqLength";
            this.lblSeqLength.Size = new System.Drawing.Size(53, 12);
            this.lblSeqLength.TabIndex = 6;
            this.lblSeqLength.Text = "序列长度";
            // 
            // lblGenData
            // 
            this.lblGenData.AutoSize = true;
            this.lblGenData.Location = new System.Drawing.Point(253, 21);
            this.lblGenData.Name = "lblGenData";
            this.lblGenData.Size = new System.Drawing.Size(53, 12);
            this.lblGenData.TabIndex = 8;
            this.lblGenData.Text = "生成结果";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(255, 39);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(184, 76);
            this.txtResult.TabIndex = 9;
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.SystemColors.Control;
            this.btnGenerate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGenerate.BackgroundImage")));
            this.btnGenerate.ButtonType = UserControl.ButtonTypes.None;
            this.btnGenerate.Caption = "模拟数据";
            this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerate.Location = new System.Drawing.Point(73, 135);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(88, 22);
            this.btnGenerate.TabIndex = 10;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.ButtonType = UserControl.ButtonTypes.None;
            this.btnOK.Caption = "确定";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Location = new System.Drawing.Point(195, 135);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 22);
            this.btnOK.TabIndex = 11;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.ButtonType = UserControl.ButtonTypes.None;
            this.btnCancel.Caption = "取消";
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(318, 135);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 22);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FAutoTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 169);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.lblGenData);
            this.Controls.Add(this.txtSeqLength);
            this.Controls.Add(this.lblSeqLength);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.txtPrefix);
            this.Controls.Add(this.lblPrefix);
            this.Name = "FAutoTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据格式";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.TextBox txtSeqLength;
        private System.Windows.Forms.Label lblSeqLength;
        private System.Windows.Forms.Label lblGenData;
        private System.Windows.Forms.TextBox txtResult;
        private UserControl.UCButton btnGenerate;
        private UserControl.UCButton btnOK;
        private UserControl.UCButton btnCancel;
    }
}