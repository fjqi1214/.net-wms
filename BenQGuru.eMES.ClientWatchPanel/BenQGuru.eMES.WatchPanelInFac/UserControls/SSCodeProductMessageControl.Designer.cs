namespace BenQGuru.eMES.ClientWatchPanel
{
    partial class SSCodeProductMessageControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SSCodeProductMessageControl));
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DataChart = new Steema.TeeChart.TChart();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ultraProdcutDataGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.normalMessageControl = new BenQGuru.eMES.ClientWatchPanel.NormalMessageControl();
            this.exceptionMessageControl = new BenQGuru.eMES.ClientWatchPanel.ExceptionMessageControl();
            this.axis1 = new Steema.TeeChart.Axis(this.components);
            this.OutPutJoin = new Steema.TeeChart.Styles.BarJoin();
            this.RateLine = new Steema.TeeChart.Styles.FastLine();
            this.hearMessageControl = new BenQGuru.eMES.ClientWatchPanel.HearMessageControl();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraProdcutDataGrid)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.hearMessageControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 63);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.DataChart);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 339);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1100, 361);
            this.panel2.TabIndex = 1;
            // 
            // DataChart
            // 
            // 
            // 
            // 
            this.DataChart.Aspect.Chart3DPercent = 0;
            this.DataChart.Aspect.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.DataChart.Aspect.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.DataChart.Aspect.View3D = false;
            this.DataChart.Aspect.Zoom = 112;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Bottom.AxisPen.Visible = false;
            // 
            // 
            // 
            this.DataChart.Axes.Bottom.Grid.ZPosition = 0;
            // 
            // 
            // 
            this.DataChart.Axes.Bottom.Labels.Angle = 90;
            // 
            // 
            // 
            this.DataChart.Axes.Bottom.Labels.Font.Bold = true;
            // 
            // 
            // 
            this.DataChart.Axes.Bottom.Labels.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Bottom.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.DataChart.Axes.Bottom.Labels.Font.Shadow.Visible = false;
            this.DataChart.Axes.Bottom.Labels.Font.Size = 9;
            // 
            // 
            // 
            this.DataChart.Axes.Bottom.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Bottom.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.Axes.Bottom.Title.Shadow.Visible = false;
            this.DataChart.Axes.Custom.Add(this.axis1);
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Depth.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Depth.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.Axes.Depth.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Depth.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.Axes.Depth.Title.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.DepthTop.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.DepthTop.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.Axes.DepthTop.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.DepthTop.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.Axes.DepthTop.Title.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Left.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DataChart.Axes.Left.AxisPen.Visible = false;
            // 
            // 
            // 
            this.DataChart.Axes.Left.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Left.Labels.Font.Bold = true;
            // 
            // 
            // 
            this.DataChart.Axes.Left.Labels.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.DataChart.Axes.Left.Labels.Font.Shadow.Visible = false;
            this.DataChart.Axes.Left.Labels.Font.Shadow.Width = 0;
            this.DataChart.Axes.Left.Labels.Font.Size = 9;
            // 
            // 
            // 
            this.DataChart.Axes.Left.Labels.Shadow.Visible = false;
            this.DataChart.Axes.Left.MinorTickCount = 0;
            this.DataChart.Axes.Left.StartPosition = 34;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Left.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.Axes.Left.Title.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Right.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Right.Labels.Font.Bold = true;
            // 
            // 
            // 
            this.DataChart.Axes.Right.Labels.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.DataChart.Axes.Right.Labels.Font.Shadow.Visible = false;
            this.DataChart.Axes.Right.Labels.Font.Size = 9;
            // 
            // 
            // 
            this.DataChart.Axes.Right.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Right.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.Axes.Right.Title.Shadow.Visible = false;
            this.DataChart.Axes.Right.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Top.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Top.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.Axes.Top.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Top.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.Axes.Top.Title.Shadow.Visible = false;
            this.DataChart.BackColor = System.Drawing.Color.Transparent;
            this.DataChart.Cursor = System.Windows.Forms.Cursors.Default;
            this.DataChart.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Footer.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.Footer.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Header.Font.Shadow.Visible = false;
            this.DataChart.Header.Lines = new string[] {
        "TeeChart"};
            // 
            // 
            // 
            this.DataChart.Header.Shadow.Visible = false;
            this.DataChart.Header.Visible = false;
            // 
            // 
            // 
            this.DataChart.Legend.Alignment = Steema.TeeChart.LegendAlignments.Bottom;
            this.DataChart.Legend.CheckBoxes = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Legend.Font.Shadow.Visible = false;
            this.DataChart.Legend.FontSeriesColor = true;
            this.DataChart.Legend.Inverted = true;
            this.DataChart.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Palette;
            this.DataChart.Legend.TextStyle = Steema.TeeChart.LegendTextStyles.RightValue;
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Legend.Title.Font.Bold = true;
            // 
            // 
            // 
            this.DataChart.Legend.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.Legend.Title.Pen.Visible = false;
            // 
            // 
            // 
            this.DataChart.Legend.Title.Shadow.Visible = false;
            this.DataChart.Legend.Visible = false;
            this.DataChart.Location = new System.Drawing.Point(0, 0);
            this.DataChart.Name = "DataChart";
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(69)))), ((int)(((byte)(86)))), ((int)(((byte)(96)))));
            // 
            // 
            // 
            this.DataChart.Panel.Shadow.Visible = false;
            this.DataChart.Series.Add(this.OutPutJoin);
            this.DataChart.Series.Add(this.RateLine);
            this.DataChart.Size = new System.Drawing.Size(1100, 361);
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.SubFooter.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.SubFooter.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.SubHeader.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.SubHeader.Shadow.Visible = false;
            this.DataChart.TabIndex = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Walls.Back.AutoHide = false;
            // 
            // 
            // 
            this.DataChart.Walls.Back.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.Walls.Bottom.AutoHide = false;
            // 
            // 
            // 
            this.DataChart.Walls.Bottom.Shadow.Visible = false;
            this.DataChart.Walls.Bottom.Visible = false;
            // 
            // 
            // 
            this.DataChart.Walls.Left.AutoHide = false;
            // 
            // 
            // 
            this.DataChart.Walls.Left.Shadow.Visible = false;
            this.DataChart.Walls.Left.Visible = false;
            // 
            // 
            // 
            this.DataChart.Walls.Right.AutoHide = false;
            // 
            // 
            // 
            this.DataChart.Walls.Right.Shadow.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.ultraProdcutDataGrid);
            this.panel3.Location = new System.Drawing.Point(0, 201);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1100, 141);
            this.panel3.TabIndex = 2;
            // 
            // ultraProdcutDataGrid
            // 
            this.ultraProdcutDataGrid.Cursor = System.Windows.Forms.Cursors.Default;
            appearance11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(162)))), ((int)(((byte)(163)))));
            appearance11.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(162)))), ((int)(((byte)(163)))));
            appearance11.BorderColor = System.Drawing.Color.White;
            appearance11.FontData.SizeInPoints = 15F;
            appearance11.ForeColor = System.Drawing.Color.Black;
            this.ultraProdcutDataGrid.DisplayLayout.Appearance = appearance11;
            appearance12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(86)))), ((int)(((byte)(96)))));
            appearance12.BorderColor = System.Drawing.Color.White;
            this.ultraProdcutDataGrid.DisplayLayout.CaptionAppearance = appearance12;
            this.ultraProdcutDataGrid.DisplayLayout.MaxRowScrollRegions = 12;
            appearance13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(162)))), ((int)(((byte)(163)))));
            appearance13.FontData.BoldAsString = "True";
            appearance13.FontData.Name = "Arial";
            appearance13.FontData.SizeInPoints = 12F;
            appearance13.ForeColor = System.Drawing.Color.Black;
            this.ultraProdcutDataGrid.DisplayLayout.Override.ActiveCellAppearance = appearance13;
            appearance14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(162)))), ((int)(((byte)(163)))));
            appearance14.BorderColor = System.Drawing.Color.White;
            this.ultraProdcutDataGrid.DisplayLayout.Override.ActiveRowAppearance = appearance14;
            appearance15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(162)))), ((int)(((byte)(163)))));
            appearance15.BorderColor = System.Drawing.Color.White;
            appearance15.FontData.BoldAsString = "True";
            appearance15.FontData.Name = "Arial";
            appearance15.FontData.SizeInPoints = 12F;
            appearance15.ForeColor = System.Drawing.Color.Black;
            this.ultraProdcutDataGrid.DisplayLayout.Override.CellAppearance = appearance15;
            appearance16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(162)))), ((int)(((byte)(163)))));
            appearance16.BorderColor = System.Drawing.Color.White;
            this.ultraProdcutDataGrid.DisplayLayout.Override.FixedCellAppearance = appearance16;
            appearance17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(162)))), ((int)(((byte)(163)))));
            appearance17.BackColor2 = System.Drawing.Color.White;
            appearance17.BorderColor = System.Drawing.Color.White;
            this.ultraProdcutDataGrid.DisplayLayout.Override.FixedHeaderAppearance = appearance17;
            appearance18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(86)))), ((int)(((byte)(96)))));
            appearance18.BorderColor = System.Drawing.Color.White;
            appearance18.FontData.BoldAsString = "True";
            appearance18.FontData.Name = "Arial";
            appearance18.FontData.SizeInPoints = 16F;
            appearance18.ForeColor = System.Drawing.Color.White;
            this.ultraProdcutDataGrid.DisplayLayout.Override.HeaderAppearance = appearance18;
            this.ultraProdcutDataGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(162)))), ((int)(((byte)(163)))));
            this.ultraProdcutDataGrid.DisplayLayout.Override.SelectedCellAppearance = appearance19;
            appearance20.BackColor = System.Drawing.Color.White;
            appearance20.BackColor2 = System.Drawing.Color.White;
            appearance20.BorderColor = System.Drawing.Color.White;
            this.ultraProdcutDataGrid.DisplayLayout.Override.SelectedRowAppearance = appearance20;
            this.ultraProdcutDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraProdcutDataGrid.Location = new System.Drawing.Point(0, 0);
            this.ultraProdcutDataGrid.Name = "ultraProdcutDataGrid";
            this.ultraProdcutDataGrid.Size = new System.Drawing.Size(1100, 141);
            this.ultraProdcutDataGrid.SupportThemes = false;
            this.ultraProdcutDataGrid.TabIndex = 0;
            this.ultraProdcutDataGrid.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraProdcutDataGrid_InitializeLayout_1);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 63);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1100, 139);
            this.panel4.TabIndex = 3;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.normalMessageControl);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(616, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(484, 139);
            this.panel6.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.exceptionMessageControl);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(616, 139);
            this.panel5.TabIndex = 0;
            // 
            // normalMessageControl
            // 
            this.normalMessageControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.normalMessageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.normalMessageControl.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.normalMessageControl.Location = new System.Drawing.Point(0, 0);
            this.normalMessageControl.Name = "normalMessageControl";
            this.normalMessageControl.Size = new System.Drawing.Size(484, 139);
            this.normalMessageControl.TabIndex = 0;
            // 
            // exceptionMessageControl
            // 
            this.exceptionMessageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exceptionMessageControl.ExpectionMessage = "";
            this.exceptionMessageControl.Location = new System.Drawing.Point(0, 0);
            this.exceptionMessageControl.Name = "exceptionMessageControl";
            this.exceptionMessageControl.Size = new System.Drawing.Size(616, 139);
            this.exceptionMessageControl.TabIndex = 0;
            // 
            // axis1
            // 
            // 
            // 
            // 
            this.axis1.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.axis1.AxisPen.Visible = false;
            this.axis1.EndPosition = 30;
            // 
            // 
            // 
            this.axis1.Grid.ZPosition = 0;
            this.axis1.Horizontal = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.axis1.Labels.Font.Bold = true;
            // 
            // 
            // 
            this.axis1.Labels.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.axis1.Labels.Font.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.axis1.Labels.Font.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.axis1.Labels.Font.Brush.Gradient.UseMiddle = true;
            this.axis1.Labels.Font.Brush.Gradient.Visible = true;
            this.axis1.Labels.Font.Brush.Solid = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.axis1.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            // 
            // 
            // 
            this.axis1.Labels.Font.Shadow.Brush.Gradient.Transparency = 4;
            this.axis1.Labels.Font.Shadow.Visible = false;
            this.axis1.Labels.Font.Size = 9;
            // 
            // 
            // 
            this.axis1.Labels.Shadow.Visible = false;
            this.axis1.LogarithmicBase = 0;
            this.axis1.MinorTickCount = 0;
            // 
            // 
            // 
            this.axis1.MinorTicks.Length = 0;
            this.axis1.OtherSide = true;
            this.axis1.StartPosition = 11;
            this.axis1.TickOnLabelsOnly = false;
            // 
            // 
            // 
            this.axis1.Ticks.Length = 6;
            // 
            // 
            // 
            // 
            // 
            // 
            this.axis1.Title.Font.Bold = true;
            // 
            // 
            // 
            this.axis1.Title.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.axis1.Title.Font.Shadow.Visible = false;
            this.axis1.Title.Font.Size = 9;
            // 
            // 
            // 
            this.axis1.Title.Shadow.Visible = false;
            this.axis1.ZPosition = 0;
            // 
            // OutPutJoin
            // 
            // 
            // 
            // 
            this.OutPutJoin.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(188)))), ((int)(((byte)(216)))), ((int)(((byte)(53)))));
            this.OutPutJoin.Dark3D = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.OutPutJoin.Marks.Arrow.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(188)))), ((int)(((byte)(216)))), ((int)(((byte)(53)))));
            this.OutPutJoin.Marks.Arrow.Visible = false;
            this.OutPutJoin.Marks.Arrow.Width = 0;
            this.OutPutJoin.Marks.ArrowLength = 25;
            // 
            // 
            // 
            this.OutPutJoin.Marks.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(224)))));
            // 
            // 
            // 
            this.OutPutJoin.Marks.Callout.Arrow = this.OutPutJoin.Marks.Arrow;
            this.OutPutJoin.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.OutPutJoin.Marks.Callout.ArrowHeadSize = 7;
            // 
            // 
            // 
            this.OutPutJoin.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.OutPutJoin.Marks.Callout.Distance = -24;
            this.OutPutJoin.Marks.Callout.Draw3D = false;
            this.OutPutJoin.Marks.Callout.Length = 25;
            this.OutPutJoin.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            this.OutPutJoin.Marks.Font.Bold = true;
            // 
            // 
            // 
            this.OutPutJoin.Marks.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.OutPutJoin.Marks.Font.Shadow.Visible = false;
            this.OutPutJoin.Marks.Font.Size = 12;
            this.OutPutJoin.Marks.Transparent = true;
            // 
            // 
            // 
            this.OutPutJoin.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(0)))));
            this.OutPutJoin.Title = "OutPutJoin";
            // 
            // 
            //// 
            //this.OutPutJoin.XValues.DataMember = "X";
            //this.OutPutJoin.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            //// 
            //// 
            //// 
            //this.OutPutJoin.YValues.DataMember = "Bar";
            // 
            // RateLine
            // 
            this.RateLine.CustomVertAxis = this.axis1;
            this.RateLine.Depth = 0;
            this.RateLine.LabelMember = "Labels";
            // 
            // 
            // 
            this.RateLine.LinePen.Color = System.Drawing.Color.Red;
            this.RateLine.LinePen.Width = 2;
            // 
            // 
            // 
            this.RateLine.Marks.ArrowLength = 4;
            // 
            // 
            // 
            this.RateLine.Marks.Callout.Arrow = this.RateLine.Marks.Arrow;
            this.RateLine.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.RateLine.Marks.Callout.ArrowHeadSize = 10;
            // 
            // 
            // 
            this.RateLine.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.RateLine.Marks.Callout.Distance = 0;
            this.RateLine.Marks.Callout.Draw3D = false;
            this.RateLine.Marks.Callout.Length = 4;
            this.RateLine.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            this.RateLine.Marks.Font.Bold = true;
            // 
            // 
            // 
            this.RateLine.Marks.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            // 
            // 
            // 
            this.RateLine.Marks.Font.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.RateLine.Marks.Font.Shadow.Visible = false;
            this.RateLine.Marks.Font.Size = 12;
            // 
            // 
            // 
            // 
            // 
            // 
            this.RateLine.Marks.Symbol.Brush.Color = System.Drawing.Color.Green;
            this.RateLine.Marks.Transparent = true;
            this.RateLine.Marks.Visible = true;
            this.RateLine.Title = "RateLine";
            this.RateLine.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Custom;
            // 
            // 
            //// 
            //this.RateLine.XValues.DataMember = "X";
            //this.RateLine.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            //// 
            //// 
            //// 
            //this.RateLine.YValues.DataMember = "Y";
            // 
            // hearMessageControl
            // 
            this.hearMessageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hearMessageControl.Location = new System.Drawing.Point(0, 0);
            this.hearMessageControl.Name = "hearMessageControl";
            this.hearMessageControl.Size = new System.Drawing.Size(1100, 63);
            this.hearMessageControl.TabIndex = 0;
            // 
            // SSCodeProductMessageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SSCodeProductMessageControl";
            this.Size = new System.Drawing.Size(1100, 700);
            this.Load += new System.EventHandler(this.SSCodeProductMessageControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraProdcutDataGrid)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private HearMessageControl hearMessageControl;
        private Steema.TeeChart.TChart DataChart;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraProdcutDataGrid;
        private System.Windows.Forms.Panel panel6;
        private NormalMessageControl normalMessageControl;
        private System.Windows.Forms.Panel panel5;
        private ExceptionMessageControl exceptionMessageControl;
        private Steema.TeeChart.Styles.BarJoin OutPutJoin;
        private Steema.TeeChart.Axis axis1;
        private Steema.TeeChart.Styles.FastLine RateLine;
    }
}
