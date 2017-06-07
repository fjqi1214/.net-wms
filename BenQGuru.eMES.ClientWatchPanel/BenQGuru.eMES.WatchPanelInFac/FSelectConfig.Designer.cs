namespace BenQGuru.eMES.ClientWatchPanel
{
    partial class FSelectConfig
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btDelete = new System.Windows.Forms.Button();
            this.DrpConfigList = new UserControl.UCLabelCombox();
            this.btAdd = new System.Windows.Forms.Button();
            this.btChange = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btDelete);
            this.groupBox1.Controls.Add(this.DrpConfigList);
            this.groupBox1.Controls.Add(this.btAdd);
            this.groupBox1.Controls.Add(this.btChange);
            this.groupBox1.Controls.Add(this.btOK);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 139);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " ";
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(297, 78);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(75, 25);
            this.btDelete.TabIndex = 7;
            this.btDelete.Text = "删除";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // DrpConfigList
            // 
            this.DrpConfigList.AllowEditOnlyChecked = true;
            this.DrpConfigList.Caption = "配置项列表";
            this.DrpConfigList.Checked = false;
            this.DrpConfigList.Location = new System.Drawing.Point(31, 31);
            this.DrpConfigList.Name = "DrpConfigList";
            this.DrpConfigList.SelectedIndex = -1;
            this.DrpConfigList.ShowCheckBox = false;
            this.DrpConfigList.Size = new System.Drawing.Size(275, 25);
            this.DrpConfigList.TabIndex = 6;
            this.DrpConfigList.WidthType = UserControl.WidthTypes.Long;
            this.DrpConfigList.XAlign = 106;
            this.DrpConfigList.SelectBox_DropDown += new System.EventHandler(this.DrpConfigList_SelectBox_DropDown);
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(203, 78);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 25);
            this.btAdd.TabIndex = 4;
            this.btAdd.Text = "新增";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btChange
            // 
            this.btChange.Location = new System.Drawing.Point(109, 78);
            this.btChange.Name = "btChange";
            this.btChange.Size = new System.Drawing.Size(75, 25);
            this.btChange.TabIndex = 3;
            this.btChange.Text = "修改";
            this.btChange.UseVisualStyleBackColor = true;
            this.btChange.Click += new System.EventHandler(this.btChange_Click);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(15, 78);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 25);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "确认";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // FSelectConfig
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 139);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FSelectConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择配置项";
            this.Load += new System.EventHandler(this.FSelectConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btChange;
        private System.Windows.Forms.Button btOK;
        private UserControl.UCLabelCombox DrpConfigList;
        private System.Windows.Forms.Button btDelete;
    }
}

