namespace BenQGuru.eMES.BaseDataModel
{
    partial class FSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSetting));
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new UserControl.UCButton();
            this.btnSave = new UserControl.UCButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoErrorCancel = new System.Windows.Forms.RadioButton();
            this.rdoErrorIgnore = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoRepeatCancel = new System.Windows.Forms.RadioButton();
            this.rdoRepeatIgnore = new System.Windows.Forms.RadioButton();
            this.rdoRepeatUpdate = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoMappingByIndex = new System.Windows.Forms.RadioButton();
            this.rdoMappingByName = new System.Windows.Forms.RadioButton();
            this.panelBottom.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnSave);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 323);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(446, 69);
            this.panelBottom.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.ButtonType = UserControl.ButtonTypes.None;
            this.btnCancel.Caption = "取消";
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(273, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 22);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.ButtonType = UserControl.ButtonTypes.None;
            this.btnSave.Caption = "保存";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(110, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 22);
            this.btnSave.TabIndex = 5;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupBox3);
            this.panelMain.Controls.Add(this.groupBox2);
            this.panelMain.Controls.Add(this.groupBox1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(446, 323);
            this.panelMain.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoErrorCancel);
            this.groupBox2.Controls.Add(this.rdoErrorIgnore);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(446, 80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "导入数据出错时";
            // 
            // rdoErrorCancel
            // 
            this.rdoErrorCancel.AutoSize = true;
            this.rdoErrorCancel.Location = new System.Drawing.Point(164, 35);
            this.rdoErrorCancel.Name = "rdoErrorCancel";
            this.rdoErrorCancel.Size = new System.Drawing.Size(71, 16);
            this.rdoErrorCancel.TabIndex = 1;
            this.rdoErrorCancel.Text = "终止导入";
            this.rdoErrorCancel.UseVisualStyleBackColor = true;
            // 
            // rdoErrorIgnore
            // 
            this.rdoErrorIgnore.AutoSize = true;
            this.rdoErrorIgnore.Checked = true;
            this.rdoErrorIgnore.Location = new System.Drawing.Point(26, 35);
            this.rdoErrorIgnore.Name = "rdoErrorIgnore";
            this.rdoErrorIgnore.Size = new System.Drawing.Size(83, 16);
            this.rdoErrorIgnore.TabIndex = 0;
            this.rdoErrorIgnore.TabStop = true;
            this.rdoErrorIgnore.Text = "跳过出错行";
            this.rdoErrorIgnore.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoRepeatCancel);
            this.groupBox1.Controls.Add(this.rdoRepeatIgnore);
            this.groupBox1.Controls.Add(this.rdoRepeatUpdate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "重复数据处理";
            // 
            // rdoRepeatCancel
            // 
            this.rdoRepeatCancel.AutoSize = true;
            this.rdoRepeatCancel.Location = new System.Drawing.Point(283, 35);
            this.rdoRepeatCancel.Name = "rdoRepeatCancel";
            this.rdoRepeatCancel.Size = new System.Drawing.Size(71, 16);
            this.rdoRepeatCancel.TabIndex = 2;
            this.rdoRepeatCancel.Text = "终止导入";
            this.rdoRepeatCancel.UseVisualStyleBackColor = true;
            // 
            // rdoRepeatIgnore
            // 
            this.rdoRepeatIgnore.AutoSize = true;
            this.rdoRepeatIgnore.Location = new System.Drawing.Point(164, 35);
            this.rdoRepeatIgnore.Name = "rdoRepeatIgnore";
            this.rdoRepeatIgnore.Size = new System.Drawing.Size(71, 16);
            this.rdoRepeatIgnore.TabIndex = 1;
            this.rdoRepeatIgnore.Text = "跳过此行";
            this.rdoRepeatIgnore.UseVisualStyleBackColor = true;
            // 
            // rdoRepeatUpdate
            // 
            this.rdoRepeatUpdate.AutoSize = true;
            this.rdoRepeatUpdate.Checked = true;
            this.rdoRepeatUpdate.Location = new System.Drawing.Point(26, 35);
            this.rdoRepeatUpdate.Name = "rdoRepeatUpdate";
            this.rdoRepeatUpdate.Size = new System.Drawing.Size(95, 16);
            this.rdoRepeatUpdate.TabIndex = 0;
            this.rdoRepeatUpdate.TabStop = true;
            this.rdoRepeatUpdate.Text = "更新原有数据";
            this.rdoRepeatUpdate.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoMappingByIndex);
            this.groupBox3.Controls.Add(this.rdoMappingByName);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 160);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(446, 80);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "栏位匹配模式";
            // 
            // rdoMappingByIndex
            // 
            this.rdoMappingByIndex.AutoSize = true;
            this.rdoMappingByIndex.Location = new System.Drawing.Point(164, 35);
            this.rdoMappingByIndex.Name = "rdoMappingByIndex";
            this.rdoMappingByIndex.Size = new System.Drawing.Size(47, 16);
            this.rdoMappingByIndex.TabIndex = 1;
            this.rdoMappingByIndex.Text = "顺序";
            this.rdoMappingByIndex.UseVisualStyleBackColor = true;
            // 
            // rdoMappingByName
            // 
            this.rdoMappingByName.AutoSize = true;
            this.rdoMappingByName.Checked = true;
            this.rdoMappingByName.Location = new System.Drawing.Point(26, 35);
            this.rdoMappingByName.Name = "rdoMappingByName";
            this.rdoMappingByName.Size = new System.Drawing.Size(47, 16);
            this.rdoMappingByName.TabIndex = 0;
            this.rdoMappingByName.TabStop = true;
            this.rdoMappingByName.Text = "名称";
            this.rdoMappingByName.UseVisualStyleBackColor = true;
            // 
            // FSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 392);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelBottom);
            this.Name = "FSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.FSetting_Load);
            this.panelBottom.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoRepeatCancel;
        private System.Windows.Forms.RadioButton rdoRepeatIgnore;
        private System.Windows.Forms.RadioButton rdoRepeatUpdate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoErrorCancel;
        private System.Windows.Forms.RadioButton rdoErrorIgnore;
        private UserControl.UCButton btnSave;
        private UserControl.UCButton btnCancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoMappingByIndex;
        private System.Windows.Forms.RadioButton rdoMappingByName;
    }
}