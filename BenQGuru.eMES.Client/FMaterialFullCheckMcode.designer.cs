namespace BenQGuru.eMES.Client
{
    partial class FMaterialFullCheckMcode
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMaterialFullCheckMcode));
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
            this.btnGetModelCode = new System.Windows.Forms.Button();
            this.ucLabelEditModelCode = new UserControl.UCLabelEdit();
            this.btnExport = new UserControl.UCButton();
            this.btnQuery = new UserControl.UCButton();
            this.btnGetStorageCode = new System.Windows.Forms.Button();
            this.ucLabelEditStorageCode = new UserControl.UCLabelEdit();
            this.btnGetItemCode = new System.Windows.Forms.Button();
            this.ucLabelEditMaterialCode = new UserControl.UCLabelEdit();
            this.ultraGridHead = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridHead)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGetModelCode);
            this.groupBox1.Controls.Add(this.ucLabelEditModelCode);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.btnGetStorageCode);
            this.groupBox1.Controls.Add(this.ucLabelEditStorageCode);
            this.groupBox1.Controls.Add(this.btnGetItemCode);
            this.groupBox1.Controls.Add(this.ucLabelEditMaterialCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(793, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnGetModelCode
            // 
            this.btnGetModelCode.Location = new System.Drawing.Point(731, 16);
            this.btnGetModelCode.Name = "btnGetModelCode";
            this.btnGetModelCode.Size = new System.Drawing.Size(30, 23);
            this.btnGetModelCode.TabIndex = 229;
            this.btnGetModelCode.Text = "...";
            this.btnGetModelCode.UseVisualStyleBackColor = true;
            this.btnGetModelCode.Click += new System.EventHandler(this.btnGetModelCode_Click);
            // 
            // ucLabelEditModelCode
            // 
            this.ucLabelEditModelCode.AllowEditOnlyChecked = true;
            this.ucLabelEditModelCode.AutoSelectAll = false;
            this.ucLabelEditModelCode.AutoUpper = true;
            this.ucLabelEditModelCode.Caption = "产品别";
            this.ucLabelEditModelCode.Checked = false;
            this.ucLabelEditModelCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditModelCode.Location = new System.Drawing.Point(542, 16);
            this.ucLabelEditModelCode.MaxLength = 2000;
            this.ucLabelEditModelCode.Multiline = false;
            this.ucLabelEditModelCode.Name = "ucLabelEditModelCode";
            this.ucLabelEditModelCode.PasswordChar = '\0';
            this.ucLabelEditModelCode.ReadOnly = false;
            this.ucLabelEditModelCode.ShowCheckBox = false;
            this.ucLabelEditModelCode.Size = new System.Drawing.Size(182, 24);
            this.ucLabelEditModelCode.TabIndex = 228;
            this.ucLabelEditModelCode.TabNext = true;
            this.ucLabelEditModelCode.Value = "";
            this.ucLabelEditModelCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditModelCode.XAlign = 591;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.SystemColors.Control;
            this.btnExport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExport.BackgroundImage")));
            this.btnExport.ButtonType = UserControl.ButtonTypes.None;
            this.btnExport.Caption = "导出";
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.Location = new System.Drawing.Point(691, 46);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(88, 22);
            this.btnExport.TabIndex = 227;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.SystemColors.Control;
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.ButtonType = UserControl.ButtonTypes.None;
            this.btnQuery.Caption = "查询";
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.Location = new System.Drawing.Point(588, 46);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(88, 22);
            this.btnQuery.TabIndex = 225;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnGetStorageCode
            // 
            this.btnGetStorageCode.Location = new System.Drawing.Point(465, 16);
            this.btnGetStorageCode.Name = "btnGetStorageCode";
            this.btnGetStorageCode.Size = new System.Drawing.Size(30, 23);
            this.btnGetStorageCode.TabIndex = 222;
            this.btnGetStorageCode.Text = "...";
            this.btnGetStorageCode.UseVisualStyleBackColor = true;
            this.btnGetStorageCode.Click += new System.EventHandler(this.btnGetStorageCode_Click);
            // 
            // ucLabelEditStorageCode
            // 
            this.ucLabelEditStorageCode.AllowEditOnlyChecked = true;
            this.ucLabelEditStorageCode.AutoSelectAll = false;
            this.ucLabelEditStorageCode.AutoUpper = true;
            this.ucLabelEditStorageCode.Caption = "库别";
            this.ucLabelEditStorageCode.Checked = false;
            this.ucLabelEditStorageCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditStorageCode.Location = new System.Drawing.Point(287, 16);
            this.ucLabelEditStorageCode.MaxLength = 2000;
            this.ucLabelEditStorageCode.Multiline = false;
            this.ucLabelEditStorageCode.Name = "ucLabelEditStorageCode";
            this.ucLabelEditStorageCode.PasswordChar = '\0';
            this.ucLabelEditStorageCode.ReadOnly = false;
            this.ucLabelEditStorageCode.ShowCheckBox = false;
            this.ucLabelEditStorageCode.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditStorageCode.TabIndex = 221;
            this.ucLabelEditStorageCode.TabNext = true;
            this.ucLabelEditStorageCode.Value = "";
            this.ucLabelEditStorageCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditStorageCode.XAlign = 324;
            // 
            // btnGetItemCode
            // 
            this.btnGetItemCode.Location = new System.Drawing.Point(210, 16);
            this.btnGetItemCode.Name = "btnGetItemCode";
            this.btnGetItemCode.Size = new System.Drawing.Size(30, 23);
            this.btnGetItemCode.TabIndex = 220;
            this.btnGetItemCode.Text = "...";
            this.btnGetItemCode.UseVisualStyleBackColor = true;
            this.btnGetItemCode.Click += new System.EventHandler(this.btnGetItemCode_Click);
            // 
            // ucLabelEditMaterialCode
            // 
            this.ucLabelEditMaterialCode.AllowEditOnlyChecked = true;
            this.ucLabelEditMaterialCode.AutoSelectAll = false;
            this.ucLabelEditMaterialCode.AutoUpper = true;
            this.ucLabelEditMaterialCode.Caption = "物料代码";
            this.ucLabelEditMaterialCode.Checked = false;
            this.ucLabelEditMaterialCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditMaterialCode.Location = new System.Drawing.Point(8, 16);
            this.ucLabelEditMaterialCode.MaxLength = 2000;
            this.ucLabelEditMaterialCode.Multiline = false;
            this.ucLabelEditMaterialCode.Name = "ucLabelEditMaterialCode";
            this.ucLabelEditMaterialCode.PasswordChar = '\0';
            this.ucLabelEditMaterialCode.ReadOnly = false;
            this.ucLabelEditMaterialCode.ShowCheckBox = false;
            this.ucLabelEditMaterialCode.Size = new System.Drawing.Size(194, 24);
            this.ucLabelEditMaterialCode.TabIndex = 219;
            this.ucLabelEditMaterialCode.TabNext = true;
            this.ucLabelEditMaterialCode.Value = "";
            this.ucLabelEditMaterialCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditMaterialCode.XAlign = 69;
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
            this.ultraGridHead.Location = new System.Drawing.Point(0, 74);
            this.ultraGridHead.Name = "ultraGridHead";
            this.ultraGridHead.Size = new System.Drawing.Size(793, 277);
            this.ultraGridHead.TabIndex = 0;
            this.ultraGridHead.Text = "ultraGridHead";
            this.ultraGridHead.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridHead_InitializeLayout);
            this.ultraGridHead.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ultraGridHead_InitializeRow);
            // 
            // FMaterialFullCheckMcode
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(793, 351);
            this.Controls.Add(this.ultraGridHead);
            this.Controls.Add(this.groupBox1);
            this.Name = "FMaterialFullCheckMcode";
            this.Text = "物料齐套检查-物料";
            this.Load += new System.EventHandler(this.FMaterialFullCheckMcode_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridHead)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGetStorageCode;
        private UserControl.UCLabelEdit ucLabelEditStorageCode;
        private System.Windows.Forms.Button btnGetItemCode;
        private UserControl.UCLabelEdit ucLabelEditMaterialCode;
        private UserControl.UCButton btnQuery;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridHead;
        private UserControl.UCButton btnExport;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.Button btnGetModelCode;
        private UserControl.UCLabelEdit ucLabelEditModelCode;
    }
}