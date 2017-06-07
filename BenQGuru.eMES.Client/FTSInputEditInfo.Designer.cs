namespace BenQGuru.eMES.Client
{
    partial class FTSInputEditInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTSInputEditInfo));
            this.ucErrorCodeDesc = new UserControl.UCLabelEdit();
            this.ucErrorGroupDesc = new UserControl.UCLabelEdit();
            this.chklistErrorGroup = new System.Windows.Forms.CheckedListBox();
            this.listErrCauseGroup = new System.Windows.Forms.ListBox();
            this.listLCErrorCause = new System.Windows.Forms.ListBox();
            this.listLCDuty = new System.Windows.Forms.ListBox();
            this.listLCSolution = new System.Windows.Forms.ListBox();
            this.ucBtnSave = new UserControl.UCButton();
            this.ucBtnExit = new UserControl.UCButton();
            this.labelErrorComponent = new System.Windows.Forms.Label();
            this.labelErrorCauseGroup = new System.Windows.Forms.Label();
            this.labelErrorCause = new System.Windows.Forms.Label();
            this.labelDuty = new System.Windows.Forms.Label();
            this.labelSolution = new System.Windows.Forms.Label();
            this.labelInAdvance = new System.Windows.Forms.Label();
            this.txtLESolutionMemo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ucErrorCodeDesc
            // 
            this.ucErrorCodeDesc.AllowEditOnlyChecked = true;
            this.ucErrorCodeDesc.AutoSelectAll = false;
            this.ucErrorCodeDesc.AutoUpper = true;
            this.ucErrorCodeDesc.Caption = "不良代码描述";
            this.ucErrorCodeDesc.Checked = false;
            this.ucErrorCodeDesc.EditType = UserControl.EditTypes.String;
            this.ucErrorCodeDesc.Enabled = false;
            this.ucErrorCodeDesc.Location = new System.Drawing.Point(248, 12);
            this.ucErrorCodeDesc.MaxLength = 40;
            this.ucErrorCodeDesc.Multiline = false;
            this.ucErrorCodeDesc.Name = "ucErrorCodeDesc";
            this.ucErrorCodeDesc.PasswordChar = '\0';
            this.ucErrorCodeDesc.ReadOnly = false;
            this.ucErrorCodeDesc.ShowCheckBox = false;
            this.ucErrorCodeDesc.Size = new System.Drawing.Size(218, 24);
            this.ucErrorCodeDesc.TabIndex = 1;
            this.ucErrorCodeDesc.TabNext = true;
            this.ucErrorCodeDesc.Value = "";
            this.ucErrorCodeDesc.WidthType = UserControl.WidthTypes.Normal;
            this.ucErrorCodeDesc.XAlign = 333;
            // 
            // ucErrorGroupDesc
            // 
            this.ucErrorGroupDesc.AllowEditOnlyChecked = true;
            this.ucErrorGroupDesc.AutoSelectAll = false;
            this.ucErrorGroupDesc.AutoUpper = true;
            this.ucErrorGroupDesc.Caption = "不良代码组描述";
            this.ucErrorGroupDesc.Checked = false;
            this.ucErrorGroupDesc.EditType = UserControl.EditTypes.String;
            this.ucErrorGroupDesc.Enabled = false;
            this.ucErrorGroupDesc.Location = new System.Drawing.Point(12, 12);
            this.ucErrorGroupDesc.MaxLength = 40;
            this.ucErrorGroupDesc.Multiline = false;
            this.ucErrorGroupDesc.Name = "ucErrorGroupDesc";
            this.ucErrorGroupDesc.PasswordChar = '\0';
            this.ucErrorGroupDesc.ReadOnly = false;
            this.ucErrorGroupDesc.ShowCheckBox = false;
            this.ucErrorGroupDesc.Size = new System.Drawing.Size(230, 24);
            this.ucErrorGroupDesc.TabIndex = 2;
            this.ucErrorGroupDesc.TabNext = true;
            this.ucErrorGroupDesc.Value = "";
            this.ucErrorGroupDesc.WidthType = UserControl.WidthTypes.Normal;
            this.ucErrorGroupDesc.XAlign = 109;
            // 
            // chklistErrorGroup
            // 
            this.chklistErrorGroup.CheckOnClick = true;
            this.chklistErrorGroup.FormattingEnabled = true;
            this.chklistErrorGroup.Location = new System.Drawing.Point(10, 60);
            this.chklistErrorGroup.Name = "chklistErrorGroup";
            this.chklistErrorGroup.Size = new System.Drawing.Size(148, 196);
            this.chklistErrorGroup.TabIndex = 3;
            this.chklistErrorGroup.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chklistErrorGroup_ItemCheck);
            // 
            // listErrCauseGroup
            // 
            this.listErrCauseGroup.FormattingEnabled = true;
            this.listErrCauseGroup.ItemHeight = 12;
            this.listErrCauseGroup.Location = new System.Drawing.Point(164, 60);
            this.listErrCauseGroup.Name = "listErrCauseGroup";
            this.listErrCauseGroup.Size = new System.Drawing.Size(148, 196);
            this.listErrCauseGroup.TabIndex = 4;
            this.listErrCauseGroup.SelectedIndexChanged += new System.EventHandler(this.listErrCauseGroup_SelectedIndexChanged);
            // 
            // listLCErrorCause
            // 
            this.listLCErrorCause.FormattingEnabled = true;
            this.listLCErrorCause.ItemHeight = 12;
            this.listLCErrorCause.Location = new System.Drawing.Point(318, 60);
            this.listLCErrorCause.Name = "listLCErrorCause";
            this.listLCErrorCause.Size = new System.Drawing.Size(148, 196);
            this.listLCErrorCause.TabIndex = 5;
            // 
            // listLCDuty
            // 
            this.listLCDuty.FormattingEnabled = true;
            this.listLCDuty.ItemHeight = 12;
            this.listLCDuty.Location = new System.Drawing.Point(8, 279);
            this.listLCDuty.Name = "listLCDuty";
            this.listLCDuty.Size = new System.Drawing.Size(148, 196);
            this.listLCDuty.TabIndex = 6;
            // 
            // listLCSolution
            // 
            this.listLCSolution.FormattingEnabled = true;
            this.listLCSolution.ItemHeight = 12;
            this.listLCSolution.Location = new System.Drawing.Point(162, 279);
            this.listLCSolution.Name = "listLCSolution";
            this.listLCSolution.Size = new System.Drawing.Size(148, 196);
            this.listLCSolution.TabIndex = 7;
            // 
            // ucBtnSave
            // 
            this.ucBtnSave.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnSave.BackgroundImage")));
            this.ucBtnSave.ButtonType = UserControl.ButtonTypes.Confirm;
            this.ucBtnSave.Caption = "确认";
            this.ucBtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnSave.Location = new System.Drawing.Point(139, 484);
            this.ucBtnSave.Name = "ucBtnSave";
            this.ucBtnSave.Size = new System.Drawing.Size(88, 22);
            this.ucBtnSave.TabIndex = 9;
            this.ucBtnSave.Click += new System.EventHandler(this.ucBtnSave_Click);
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(248, 484);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 10;
            // 
            // labelErrorComponent
            // 
            this.labelErrorComponent.AutoSize = true;
            this.labelErrorComponent.Location = new System.Drawing.Point(8, 39);
            this.labelErrorComponent.Name = "labelErrorComponent";
            this.labelErrorComponent.Size = new System.Drawing.Size(53, 12);
            this.labelErrorComponent.TabIndex = 11;
            this.labelErrorComponent.Text = "不良组件";
            // 
            // labelErrorCauseGroup
            // 
            this.labelErrorCauseGroup.AutoSize = true;
            this.labelErrorCauseGroup.Location = new System.Drawing.Point(162, 39);
            this.labelErrorCauseGroup.Name = "labelErrorCauseGroup";
            this.labelErrorCauseGroup.Size = new System.Drawing.Size(65, 12);
            this.labelErrorCauseGroup.TabIndex = 12;
            this.labelErrorCauseGroup.Text = "不良原因组";
            // 
            // labelErrorCause
            // 
            this.labelErrorCause.AutoSize = true;
            this.labelErrorCause.Location = new System.Drawing.Point(316, 39);
            this.labelErrorCause.Name = "labelErrorCause";
            this.labelErrorCause.Size = new System.Drawing.Size(53, 12);
            this.labelErrorCause.TabIndex = 13;
            this.labelErrorCause.Text = "不良原因";
            // 
            // labelDuty
            // 
            this.labelDuty.AutoSize = true;
            this.labelDuty.Location = new System.Drawing.Point(6, 259);
            this.labelDuty.Name = "labelDuty";
            this.labelDuty.Size = new System.Drawing.Size(41, 12);
            this.labelDuty.TabIndex = 14;
            this.labelDuty.Text = "责任别";
            // 
            // labelSolution
            // 
            this.labelSolution.AutoSize = true;
            this.labelSolution.Location = new System.Drawing.Point(162, 259);
            this.labelSolution.Name = "labelSolution";
            this.labelSolution.Size = new System.Drawing.Size(53, 12);
            this.labelSolution.TabIndex = 15;
            this.labelSolution.Text = "解决方案";
            // 
            // labelInAdvance
            // 
            this.labelInAdvance.AutoSize = true;
            this.labelInAdvance.Location = new System.Drawing.Point(316, 259);
            this.labelInAdvance.Name = "labelInAdvance";
            this.labelInAdvance.Size = new System.Drawing.Size(53, 12);
            this.labelInAdvance.TabIndex = 16;
            this.labelInAdvance.Text = "预防措施";
            // 
            // txtLESolutionMemo
            // 
            this.txtLESolutionMemo.Location = new System.Drawing.Point(318, 279);
            this.txtLESolutionMemo.MaxLength = 100;
            this.txtLESolutionMemo.Multiline = true;
            this.txtLESolutionMemo.Name = "txtLESolutionMemo";
            this.txtLESolutionMemo.Size = new System.Drawing.Size(148, 196);
            this.txtLESolutionMemo.TabIndex = 17;
            // 
            // FTSInputEditInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 518);
            this.Controls.Add(this.txtLESolutionMemo);
            this.Controls.Add(this.labelInAdvance);
            this.Controls.Add(this.labelSolution);
            this.Controls.Add(this.labelDuty);
            this.Controls.Add(this.labelErrorCause);
            this.Controls.Add(this.labelErrorCauseGroup);
            this.Controls.Add(this.labelErrorComponent);
            this.Controls.Add(this.ucBtnExit);
            this.Controls.Add(this.ucBtnSave);
            this.Controls.Add(this.listLCSolution);
            this.Controls.Add(this.listLCDuty);
            this.Controls.Add(this.listLCErrorCause);
            this.Controls.Add(this.listErrCauseGroup);
            this.Controls.Add(this.chklistErrorGroup);
            this.Controls.Add(this.ucErrorGroupDesc);
            this.Controls.Add(this.ucErrorCodeDesc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FTSInputEditInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "维修信息维护";
            this.Load += new System.EventHandler(this.FTSInputEditInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControl.UCLabelEdit ucErrorCodeDesc;
        private UserControl.UCLabelEdit ucErrorGroupDesc;
        private System.Windows.Forms.CheckedListBox chklistErrorGroup;
        private System.Windows.Forms.ListBox listErrCauseGroup;
        private System.Windows.Forms.ListBox listLCErrorCause;
        private System.Windows.Forms.ListBox listLCDuty;
        private System.Windows.Forms.ListBox listLCSolution;
        private UserControl.UCButton ucBtnSave;
        private UserControl.UCButton ucBtnExit;
        private System.Windows.Forms.Label labelErrorComponent;
        private System.Windows.Forms.Label labelErrorCauseGroup;
        private System.Windows.Forms.Label labelErrorCause;
        private System.Windows.Forms.Label labelDuty;
        private System.Windows.Forms.Label labelSolution;
        private System.Windows.Forms.Label labelInAdvance;
        private System.Windows.Forms.TextBox txtLESolutionMemo;
    }
}