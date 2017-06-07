namespace BenQGuru.eMES.Client
{
    partial class FCheckRoute
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucLabelEditRcard = new UserControl.UCLabelEdit();
            this.ucLabelEditOp = new UserControl.UCLabelEdit();
            this.ucLabelEditRout = new UserControl.UCLabelEdit();
            this.ucLabelEditName = new UserControl.UCLabelEdit();
            this.ucLabelEditItemCode = new UserControl.UCLabelEdit();
            this.ucMessageInfo = new UserControl.UCMessage();
            this.ultraGridHead = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.DisplayMessage = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridHead)).BeginInit();
            this.DisplayMessage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucLabelEditRcard);
            this.groupBox1.Controls.Add(this.ucLabelEditOp);
            this.groupBox1.Controls.Add(this.ucLabelEditRout);
            this.groupBox1.Controls.Add(this.ucLabelEditName);
            this.groupBox1.Controls.Add(this.ucLabelEditItemCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(807, 109);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // ucLabelEditRcard
            // 
            this.ucLabelEditRcard.AllowEditOnlyChecked = true;
            this.ucLabelEditRcard.AutoSelectAll = false;
            this.ucLabelEditRcard.AutoUpper = true;
            this.ucLabelEditRcard.Caption = "产品序列号";
            this.ucLabelEditRcard.Checked = false;
            this.ucLabelEditRcard.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRcard.Location = new System.Drawing.Point(6, 15);
            this.ucLabelEditRcard.MaxLength = 40;
            this.ucLabelEditRcard.Multiline = false;
            this.ucLabelEditRcard.Name = "ucLabelEditRcard";
            this.ucLabelEditRcard.PasswordChar = '\0';
            this.ucLabelEditRcard.ReadOnly = false;
            this.ucLabelEditRcard.ShowCheckBox = false;
            this.ucLabelEditRcard.Size = new System.Drawing.Size(273, 25);
            this.ucLabelEditRcard.TabIndex = 1;
            this.ucLabelEditRcard.TabNext = true;
            this.ucLabelEditRcard.Value = "";
            this.ucLabelEditRcard.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditRcard.XAlign = 79;
            this.ucLabelEditRcard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditRcard_TxtboxKeyPress);
            // 
            // ucLabelEditOp
            // 
            this.ucLabelEditOp.AllowEditOnlyChecked = true;
            this.ucLabelEditOp.AutoSelectAll = false;
            this.ucLabelEditOp.AutoUpper = true;
            this.ucLabelEditOp.Caption = "当前工序";
            this.ucLabelEditOp.Checked = false;
            this.ucLabelEditOp.EditType = UserControl.EditTypes.String;
            this.ucLabelEditOp.Location = new System.Drawing.Point(319, 71);
            this.ucLabelEditOp.MaxLength = 40;
            this.ucLabelEditOp.Multiline = false;
            this.ucLabelEditOp.Name = "ucLabelEditOp";
            this.ucLabelEditOp.PasswordChar = '\0';
            this.ucLabelEditOp.ReadOnly = true;
            this.ucLabelEditOp.ShowCheckBox = false;
            this.ucLabelEditOp.Size = new System.Drawing.Size(261, 25);
            this.ucLabelEditOp.TabIndex = 5;
            this.ucLabelEditOp.TabNext = true;
            this.ucLabelEditOp.Value = "";
            this.ucLabelEditOp.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditOp.XAlign = 380;
            // 
            // ucLabelEditRout
            // 
            this.ucLabelEditRout.AllowEditOnlyChecked = true;
            this.ucLabelEditRout.AutoSelectAll = false;
            this.ucLabelEditRout.AutoUpper = true;
            this.ucLabelEditRout.Caption = "途程";
            this.ucLabelEditRout.Checked = false;
            this.ucLabelEditRout.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRout.Location = new System.Drawing.Point(42, 71);
            this.ucLabelEditRout.MaxLength = 40;
            this.ucLabelEditRout.Multiline = false;
            this.ucLabelEditRout.Name = "ucLabelEditRout";
            this.ucLabelEditRout.PasswordChar = '\0';
            this.ucLabelEditRout.ReadOnly = true;
            this.ucLabelEditRout.ShowCheckBox = false;
            this.ucLabelEditRout.Size = new System.Drawing.Size(237, 25);
            this.ucLabelEditRout.TabIndex = 4;
            this.ucLabelEditRout.TabNext = true;
            this.ucLabelEditRout.Value = "";
            this.ucLabelEditRout.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditRout.XAlign = 79;
            // 
            // ucLabelEditName
            // 
            this.ucLabelEditName.AllowEditOnlyChecked = true;
            this.ucLabelEditName.AutoSelectAll = false;
            this.ucLabelEditName.AutoUpper = true;
            this.ucLabelEditName.Caption = "产品名称";
            this.ucLabelEditName.Checked = false;
            this.ucLabelEditName.EditType = UserControl.EditTypes.String;
            this.ucLabelEditName.Location = new System.Drawing.Point(319, 43);
            this.ucLabelEditName.MaxLength = 40;
            this.ucLabelEditName.Multiline = false;
            this.ucLabelEditName.Name = "ucLabelEditName";
            this.ucLabelEditName.PasswordChar = '\0';
            this.ucLabelEditName.ReadOnly = true;
            this.ucLabelEditName.ShowCheckBox = false;
            this.ucLabelEditName.Size = new System.Drawing.Size(261, 25);
            this.ucLabelEditName.TabIndex = 3;
            this.ucLabelEditName.TabNext = true;
            this.ucLabelEditName.Value = "";
            this.ucLabelEditName.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditName.XAlign = 380;
            // 
            // ucLabelEditItemCode
            // 
            this.ucLabelEditItemCode.AllowEditOnlyChecked = true;
            this.ucLabelEditItemCode.AutoSelectAll = false;
            this.ucLabelEditItemCode.AutoUpper = true;
            this.ucLabelEditItemCode.Caption = "产品代码";
            this.ucLabelEditItemCode.Checked = false;
            this.ucLabelEditItemCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditItemCode.Location = new System.Drawing.Point(18, 43);
            this.ucLabelEditItemCode.MaxLength = 40;
            this.ucLabelEditItemCode.Multiline = false;
            this.ucLabelEditItemCode.Name = "ucLabelEditItemCode";
            this.ucLabelEditItemCode.PasswordChar = '\0';
            this.ucLabelEditItemCode.ReadOnly = true;
            this.ucLabelEditItemCode.ShowCheckBox = false;
            this.ucLabelEditItemCode.Size = new System.Drawing.Size(261, 25);
            this.ucLabelEditItemCode.TabIndex = 2;
            this.ucLabelEditItemCode.TabNext = true;
            this.ucLabelEditItemCode.Value = "";
            this.ucLabelEditItemCode.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditItemCode.XAlign = 79;
            // 
            // ucMessageInfo
            // 
            this.ucMessageInfo.AccessibleDescription = "";
            this.ucMessageInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessageInfo.ButtonVisible = false;
            this.ucMessageInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessageInfo.Location = new System.Drawing.Point(3, 17);
            this.ucMessageInfo.Name = "ucMessageInfo";
            this.ucMessageInfo.Size = new System.Drawing.Size(801, 222);
            this.ucMessageInfo.TabIndex = 0;
            this.ucMessageInfo.Tag = "";
            // 
            // ultraGridHead
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGridHead.DisplayLayout.Appearance = appearance1;
            this.ultraGridHead.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridHead.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridHead.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridHead.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.ultraGridHead.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridHead.DisplayLayout.GroupByBox.Hidden = true;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridHead.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.ultraGridHead.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGridHead.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGridHead.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGridHead.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.ultraGridHead.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGridHead.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGridHead.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGridHead.DisplayLayout.Override.CellAppearance = appearance8;
            this.ultraGridHead.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGridHead.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridHead.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.ultraGridHead.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.ultraGridHead.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGridHead.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.ultraGridHead.DisplayLayout.Override.RowAppearance = appearance11;
            this.ultraGridHead.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGridHead.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.ultraGridHead.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGridHead.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGridHead.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraGridHead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridHead.Location = new System.Drawing.Point(3, 17);
            this.ultraGridHead.Name = "ultraGridHead";
            this.ultraGridHead.Size = new System.Drawing.Size(801, 73);
            this.ultraGridHead.TabIndex = 0;
            this.ultraGridHead.Text = "ultraGrid1";
            this.ultraGridHead.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ultraGridHead_InitializeRow);
            this.ultraGridHead.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridHead_InitializeLayout);
            // 
            // DisplayMessage
            // 
            this.DisplayMessage.Controls.Add(this.ucMessageInfo);
            this.DisplayMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DisplayMessage.Location = new System.Drawing.Point(0, 202);
            this.DisplayMessage.Name = "DisplayMessage";
            this.DisplayMessage.Size = new System.Drawing.Size(807, 242);
            this.DisplayMessage.TabIndex = 1;
            this.DisplayMessage.TabStop = false;
            this.DisplayMessage.Text = "采集信息提示：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ultraGridHead);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(807, 93);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // FCheckRoute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 444);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.DisplayMessage);
            this.Controls.Add(this.groupBox1);
            this.Name = "FCheckRoute";
            this.Text = "途程检查";
            this.Load += new System.EventHandler(this.FCheckRoute_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridHead)).EndInit();
            this.DisplayMessage.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCMessage ucMessageInfo;
        private UserControl.UCLabelEdit ucLabelEditItemCode;
        private UserControl.UCLabelEdit ucLabelEditOp;
        private UserControl.UCLabelEdit ucLabelEditRout;
        private UserControl.UCLabelEdit ucLabelEditName;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridHead;
        private UserControl.UCLabelEdit ucLabelEditRcard;
        private System.Windows.Forms.GroupBox DisplayMessage;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}