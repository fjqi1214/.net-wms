namespace BenQGuru.eMES.Client
{
    partial class FMaterialMSD
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
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMaterialMSD));
            this.panel1 = new System.Windows.Forms.Panel();
            this.currentOperation = new System.Windows.Forms.GroupBox();
            this.ultraOptionSetOperation = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblPrompt = new System.Windows.Forms.Label();
            this.txtMtime = new System.Windows.Forms.TextBox();
            this.lblMtime = new System.Windows.Forms.Label();
            this.txtMDate = new System.Windows.Forms.TextBox();
            this.lblMDate = new System.Windows.Forms.Label();
            this.txtMUser = new System.Windows.Forms.TextBox();
            this.lblMUser = new System.Windows.Forms.Label();
            this.txtOverFloorlife = new System.Windows.Forms.TextBox();
            this.lblOverFloorlife1 = new System.Windows.Forms.Label();
            this.lblOverFloorlife = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblMDesc = new System.Windows.Forms.Label();
            this.txtMCode = new System.Windows.Forms.TextBox();
            this.txtMDesc = new System.Windows.Forms.TextBox();
            this.txtMName = new System.Windows.Forms.TextBox();
            this.lblMName = new System.Windows.Forms.Label();
            this.lblMCode = new System.Windows.Forms.Label();
            this.txtMLot = new UserControl.UCLabelEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnConfirm = new UserControl.UCButton();
            this.panel1.SuspendLayout();
            this.currentOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetOperation)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.currentOperation);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(819, 64);
            this.panel1.TabIndex = 23;
            // 
            // currentOperation
            // 
            this.currentOperation.Controls.Add(this.ultraOptionSetOperation);
            this.currentOperation.Dock = System.Windows.Forms.DockStyle.Top;
            this.currentOperation.Location = new System.Drawing.Point(0, 0);
            this.currentOperation.Name = "currentOperation";
            this.currentOperation.Size = new System.Drawing.Size(819, 56);
            this.currentOperation.TabIndex = 53;
            this.currentOperation.TabStop = false;
            this.currentOperation.Text = "当前操作";
            // 
            // ultraOptionSetOperation
            // 
            this.ultraOptionSetOperation.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.ultraOptionSetOperation.CheckedIndex = 0;
            valueListItem3.DataValue = "MSD_PACKAGE";
            valueListItem3.DisplayText = "封装";
            valueListItem5.DataValue = "MSD_OPENED";
            valueListItem5.DisplayText = "拆封";
            valueListItem4.DataValue = "MSD_ALLUSED";
            valueListItem4.DisplayText = "全部使用";
            valueListItem1.DataValue = "MSD_DRYING";
            valueListItem1.DisplayText = "干燥箱干燥";
            valueListItem2.DataValue = "MSD_BAKING";
            valueListItem2.DisplayText = "烘烤箱烘烤";
            this.ultraOptionSetOperation.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem3,
            valueListItem5,
            valueListItem4,
            valueListItem1,
            valueListItem2});
            this.ultraOptionSetOperation.Location = new System.Drawing.Point(44, 27);
            this.ultraOptionSetOperation.Name = "ultraOptionSetOperation";
            this.ultraOptionSetOperation.Size = new System.Drawing.Size(396, 23);
            this.ultraOptionSetOperation.TabIndex = 100;
            this.ultraOptionSetOperation.Text = "封装";
            this.ultraOptionSetOperation.ValueChanged += new System.EventHandler(this.ultraOptionSetOperation_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblPrompt);
            this.panel3.Controls.Add(this.txtMtime);
            this.panel3.Controls.Add(this.lblMtime);
            this.panel3.Controls.Add(this.txtMDate);
            this.panel3.Controls.Add(this.lblMDate);
            this.panel3.Controls.Add(this.txtMUser);
            this.panel3.Controls.Add(this.lblMUser);
            this.panel3.Controls.Add(this.txtOverFloorlife);
            this.panel3.Controls.Add(this.lblOverFloorlife1);
            this.panel3.Controls.Add(this.lblOverFloorlife);
            this.panel3.Controls.Add(this.txtStatus);
            this.panel3.Controls.Add(this.lblStatus);
            this.panel3.Controls.Add(this.lblMDesc);
            this.panel3.Controls.Add(this.txtMCode);
            this.panel3.Controls.Add(this.txtMDesc);
            this.panel3.Controls.Add(this.txtMName);
            this.panel3.Controls.Add(this.lblMName);
            this.panel3.Controls.Add(this.lblMCode);
            this.panel3.Controls.Add(this.txtMLot);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 64);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(819, 436);
            this.panel3.TabIndex = 25;
            // 
            // lblPrompt
            // 
            this.lblPrompt.ForeColor = System.Drawing.Color.Red;
            this.lblPrompt.Location = new System.Drawing.Point(305, 22);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(306, 21);
            this.lblPrompt.TabIndex = 76;
            this.lblPrompt.Text = "提示：请输完物料批号后回车！";
            this.lblPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMtime
            // 
            this.txtMtime.Location = new System.Drawing.Point(98, 276);
            this.txtMtime.Multiline = true;
            this.txtMtime.Name = "txtMtime";
            this.txtMtime.ReadOnly = true;
            this.txtMtime.Size = new System.Drawing.Size(200, 22);
            this.txtMtime.TabIndex = 75;
            // 
            // lblMtime
            // 
            this.lblMtime.Location = new System.Drawing.Point(10, 276);
            this.lblMtime.Name = "lblMtime";
            this.lblMtime.Size = new System.Drawing.Size(80, 21);
            this.lblMtime.TabIndex = 74;
            this.lblMtime.Text = "最后操作时间";
            this.lblMtime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMDate
            // 
            this.txtMDate.Location = new System.Drawing.Point(98, 245);
            this.txtMDate.Multiline = true;
            this.txtMDate.Name = "txtMDate";
            this.txtMDate.ReadOnly = true;
            this.txtMDate.Size = new System.Drawing.Size(200, 22);
            this.txtMDate.TabIndex = 73;
            // 
            // lblMDate
            // 
            this.lblMDate.Location = new System.Drawing.Point(10, 245);
            this.lblMDate.Name = "lblMDate";
            this.lblMDate.Size = new System.Drawing.Size(80, 21);
            this.lblMDate.TabIndex = 72;
            this.lblMDate.Text = "最后操作日期";
            this.lblMDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMUser
            // 
            this.txtMUser.Location = new System.Drawing.Point(98, 213);
            this.txtMUser.Multiline = true;
            this.txtMUser.Name = "txtMUser";
            this.txtMUser.ReadOnly = true;
            this.txtMUser.Size = new System.Drawing.Size(200, 22);
            this.txtMUser.TabIndex = 71;
            // 
            // lblMUser
            // 
            this.lblMUser.Location = new System.Drawing.Point(10, 213);
            this.lblMUser.Name = "lblMUser";
            this.lblMUser.Size = new System.Drawing.Size(80, 21);
            this.lblMUser.TabIndex = 70;
            this.lblMUser.Text = "操作人";
            this.lblMUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOverFloorlife
            // 
            this.txtOverFloorlife.Location = new System.Drawing.Point(98, 182);
            this.txtOverFloorlife.Multiline = true;
            this.txtOverFloorlife.Name = "txtOverFloorlife";
            this.txtOverFloorlife.ReadOnly = true;
            this.txtOverFloorlife.Size = new System.Drawing.Size(200, 22);
            this.txtOverFloorlife.TabIndex = 69;
            // 
            // lblOverFloorlife1
            // 
            this.lblOverFloorlife1.AutoSize = true;
            this.lblOverFloorlife1.Location = new System.Drawing.Point(304, 185);
            this.lblOverFloorlife1.Name = "lblOverFloorlife1";
            this.lblOverFloorlife1.Size = new System.Drawing.Size(29, 12);
            this.lblOverFloorlife1.TabIndex = 68;
            this.lblOverFloorlife1.Text = "小时";
            // 
            // lblOverFloorlife
            // 
            this.lblOverFloorlife.Location = new System.Drawing.Point(10, 182);
            this.lblOverFloorlife.Name = "lblOverFloorlife";
            this.lblOverFloorlife.Size = new System.Drawing.Size(80, 21);
            this.lblOverFloorlife.TabIndex = 67;
            this.lblOverFloorlife.Text = "剩余车间寿命";
            this.lblOverFloorlife.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(98, 150);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(201, 22);
            this.txtStatus.TabIndex = 66;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(10, 150);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(80, 21);
            this.lblStatus.TabIndex = 65;
            this.lblStatus.Text = "MSD状态";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMDesc
            // 
            this.lblMDesc.Location = new System.Drawing.Point(10, 119);
            this.lblMDesc.Name = "lblMDesc";
            this.lblMDesc.Size = new System.Drawing.Size(80, 21);
            this.lblMDesc.TabIndex = 64;
            this.lblMDesc.Text = "物料描述";
            this.lblMDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMCode
            // 
            this.txtMCode.Location = new System.Drawing.Point(98, 56);
            this.txtMCode.Multiline = true;
            this.txtMCode.Name = "txtMCode";
            this.txtMCode.ReadOnly = true;
            this.txtMCode.Size = new System.Drawing.Size(201, 22);
            this.txtMCode.TabIndex = 63;
            // 
            // txtMDesc
            // 
            this.txtMDesc.Location = new System.Drawing.Point(98, 119);
            this.txtMDesc.Multiline = true;
            this.txtMDesc.Name = "txtMDesc";
            this.txtMDesc.ReadOnly = true;
            this.txtMDesc.Size = new System.Drawing.Size(201, 22);
            this.txtMDesc.TabIndex = 62;
            // 
            // txtMName
            // 
            this.txtMName.Location = new System.Drawing.Point(98, 88);
            this.txtMName.Multiline = true;
            this.txtMName.Name = "txtMName";
            this.txtMName.ReadOnly = true;
            this.txtMName.Size = new System.Drawing.Size(201, 22);
            this.txtMName.TabIndex = 61;
            // 
            // lblMName
            // 
            this.lblMName.Location = new System.Drawing.Point(10, 88);
            this.lblMName.Name = "lblMName";
            this.lblMName.Size = new System.Drawing.Size(80, 21);
            this.lblMName.TabIndex = 60;
            this.lblMName.Text = "物料名称";
            this.lblMName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMCode
            // 
            this.lblMCode.Location = new System.Drawing.Point(10, 56);
            this.lblMCode.Name = "lblMCode";
            this.lblMCode.Size = new System.Drawing.Size(80, 21);
            this.lblMCode.TabIndex = 59;
            this.lblMCode.Text = "物料代码";
            this.lblMCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMLot
            // 
            this.txtMLot.AllowEditOnlyChecked = true;
            this.txtMLot.AutoSelectAll = false;
            this.txtMLot.AutoUpper = true;
            this.txtMLot.Caption = "物料批号";
            this.txtMLot.Checked = false;
            this.txtMLot.EditType = UserControl.EditTypes.String;
            this.txtMLot.Location = new System.Drawing.Point(38, 22);
            this.txtMLot.MaxLength = 40;
            this.txtMLot.Multiline = false;
            this.txtMLot.Name = "txtMLot";
            this.txtMLot.PasswordChar = '\0';
            this.txtMLot.ReadOnly = false;
            this.txtMLot.ShowCheckBox = false;
            this.txtMLot.Size = new System.Drawing.Size(261, 24);
            this.txtMLot.TabIndex = 1;
            this.txtMLot.TabNext = true;
            this.txtMLot.Value = "";
            this.txtMLot.WidthType = UserControl.WidthTypes.Long;
            this.txtMLot.XAlign = 99;
            this.txtMLot.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMLot_TxtboxKeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnConfirm);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 401);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(819, 99);
            this.panel2.TabIndex = 24;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.SystemColors.Control;
            this.btnConfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConfirm.BackgroundImage")));
            this.btnConfirm.ButtonType = UserControl.ButtonTypes.Confirm;
            this.btnConfirm.Caption = "确认";
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.Location = new System.Drawing.Point(245, 47);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(88, 22);
            this.btnConfirm.TabIndex = 22;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // FMaterialMSD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 500);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "FMaterialMSD";
            this.Text = "湿敏元件处理";
            this.Load += new System.EventHandler(this.FMATERIALMSD_Load);
            this.panel1.ResumeLayout(false);
            this.currentOperation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetOperation)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private UserControl.UCButton btnConfirm;
        private System.Windows.Forms.Panel panel2;
        private UserControl.UCLabelEdit txtMLot;
        private System.Windows.Forms.GroupBox currentOperation;
        private Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetOperation;
        private System.Windows.Forms.TextBox txtMName;
        private System.Windows.Forms.Label lblMName;
        private System.Windows.Forms.Label lblMCode;
        private System.Windows.Forms.Label lblOverFloorlife1;
        private System.Windows.Forms.Label lblOverFloorlife;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblMDesc;
        private System.Windows.Forms.TextBox txtMCode;
        private System.Windows.Forms.TextBox txtMDesc;
        private System.Windows.Forms.TextBox txtMtime;
        private System.Windows.Forms.Label lblMtime;
        private System.Windows.Forms.TextBox txtMDate;
        private System.Windows.Forms.Label lblMDate;
        private System.Windows.Forms.TextBox txtMUser;
        private System.Windows.Forms.Label lblMUser;
        private System.Windows.Forms.TextBox txtOverFloorlife;
        private System.Windows.Forms.Label lblPrompt;
    }
}