namespace BenQGuru.eMES.ClientWatchPanel
{
    partial class TChartControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TChartControl));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tChartData = new Steema.TeeChart.TChart();
            this.axis1 = new Steema.TeeChart.Axis(this.components);
            this.FinishedDataBar = new Steema.TeeChart.Styles.Bar();
            this.SemimanuDataBar = new Steema.TeeChart.Styles.Bar();
            this.FinishedRateLine = new Steema.TeeChart.Styles.FastLine();
            this.SemimanuRateLine = new Steema.TeeChart.Styles.FastLine();
            this.FinishedProductDateLine = new Steema.TeeChart.Styles.FastLine();
            this.SemimanuProductDateLine = new Steema.TeeChart.Styles.FastLine();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.SystemColors.ControlText;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.tChartData, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(625, 447);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // tChartData
            // 
            // 
            // 
            // 
            this.tChartData.Aspect.Chart3DPercent = 10;
            this.tChartData.Aspect.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.tChartData.Aspect.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.tChartData.Aspect.View3D = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Bottom.Grid.ZPosition = 0;
            // 
            // 
            // 
            this.tChartData.Axes.Bottom.Labels.Angle = 270;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Bottom.Labels.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tChartData.Axes.Bottom.Labels.Font.Shadow.Visible = false;
            this.tChartData.Axes.Bottom.Labels.Separation = 17;
            // 
            // 
            // 
            this.tChartData.Axes.Bottom.Labels.Shadow.Visible = false;
            this.tChartData.Axes.Bottom.MinorTickCount = 10;
            // 
            // 
            // 
            this.tChartData.Axes.Bottom.MinorTicks.Length = 0;
            this.tChartData.Axes.Bottom.TickOnLabelsOnly = false;
            // 
            // 
            // 
            this.tChartData.Axes.Bottom.Ticks.Length = 8;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Bottom.Title.Font.Shadow.Visible = false;
            this.tChartData.Axes.Bottom.Title.Lines = new string[] {
        ""};
            // 
            // 
            // 
            this.tChartData.Axes.Bottom.Title.Shadow.Visible = false;
            this.tChartData.Axes.Custom.Add(this.axis1);
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Depth.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Depth.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.Axes.Depth.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Depth.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.Axes.Depth.Title.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.DepthTop.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.DepthTop.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.Axes.DepthTop.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.Axes.DepthTop.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.DepthTop.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.Axes.DepthTop.Title.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Left.AxisPen.Width = 0;
            // 
            // 
            // 
            this.tChartData.Axes.Left.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Left.Labels.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Left.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tChartData.Axes.Left.Labels.Font.Shadow.Visible = false;
            this.tChartData.Axes.Left.Labels.Separation = 2;
            // 
            // 
            // 
            this.tChartData.Axes.Left.Labels.Shadow.Visible = false;
            this.tChartData.Axes.Left.MinorTickCount = 0;
            // 
            // 
            // 
            this.tChartData.Axes.Left.MinorTicks.Length = 0;
            this.tChartData.Axes.Left.StartPosition = 37;
            // 
            // 
            // 
            this.tChartData.Axes.Left.Ticks.Length = 3;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Left.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.Axes.Left.Title.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Right.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Right.Labels.Font.Bold = true;
            // 
            // 
            // 
            this.tChartData.Axes.Right.Labels.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tChartData.Axes.Right.Labels.Font.Shadow.Visible = false;
            this.tChartData.Axes.Right.Labels.Font.Size = 9;
            // 
            // 
            // 
            this.tChartData.Axes.Right.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Right.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.Axes.Right.Title.Shadow.Visible = false;
            this.tChartData.Axes.Right.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Top.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Top.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.Axes.Top.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Axes.Top.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.Axes.Top.Title.Shadow.Visible = false;
            this.tChartData.Axes.Top.Visible = false;
            this.tChartData.BackColor = System.Drawing.Color.Transparent;
            this.tChartData.Cursor = System.Windows.Forms.Cursors.Default;
            this.tChartData.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Footer.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.Footer.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Header.Font.Bold = true;
            // 
            // 
            // 
            this.tChartData.Header.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tChartData.Header.Font.Shadow.Visible = false;
            this.tChartData.Header.Font.Size = 15;
            this.tChartData.Header.Lines = new string[] {
        "车间质量/产量趋势图"};
            // 
            // 
            // 
            this.tChartData.Header.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.Legend.Alignment = Steema.TeeChart.LegendAlignments.Bottom;
            // 
            // 
            // 
            this.tChartData.Legend.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartData.Legend.Gradient.Direction = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            // 
            // 
            // 
            this.tChartData.Legend.DividingLines.Width = 9;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Legend.Font.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tChartData.Legend.Font.Shadow.Visible = false;
            this.tChartData.Legend.Font.Size = 12;
            this.tChartData.Legend.Inverted = true;
            this.tChartData.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Series;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.tChartData.Legend.Shadow.Brush.Gradient.Transparency = 1;
            this.tChartData.Legend.Shadow.Height = 0;
            this.tChartData.Legend.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.Legend.Symbol.DefaultPen = false;
            // 
            // 
            // 
            this.tChartData.Legend.Symbol.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.tChartData.Legend.Symbol.Squared = true;
            this.tChartData.Legend.Symbol.Width = 50;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Legend.Title.Gradient.Direction = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            // 
            // 
            // 
            this.tChartData.Legend.Title.Font.Bold = true;
            // 
            // 
            // 
            this.tChartData.Legend.Title.Font.Shadow.Visible = false;
            this.tChartData.Legend.Title.Lines = new string[] {
        ""};
            // 
            // 
            // 
            this.tChartData.Legend.Title.Pen.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Legend.Title.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            // 
            // 
            // 
            this.tChartData.Legend.Title.Shadow.Brush.Gradient.Transparency = 3;
            this.tChartData.Legend.Title.Shadow.Height = 4;
            this.tChartData.Legend.Title.Shadow.Visible = false;
            this.tChartData.Legend.Title.Shadow.Width = 2;
            this.tChartData.Legend.VertSpacing = 12;
            this.tChartData.Location = new System.Drawing.Point(3, 3);
            this.tChartData.Name = "tChartData";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChartData.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(48)))), ((int)(((byte)(47)))), ((int)(((byte)(42)))));
            // 
            // 
            // 
            this.tChartData.Panel.Gradient.Direction = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tChartData.Panel.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.tChartData.Panel.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.tChartData.Panel.Gradient.SigmaFocus = 1F;
            this.tChartData.Panel.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.tChartData.Panel.Gradient.Transparency = 2;
            this.tChartData.Panel.Gradient.UseMiddle = true;
            this.tChartData.Panel.MarginRight = 4;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Panel.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tChartData.Panel.Shadow.Height = 0;
            this.tChartData.Panel.Shadow.Visible = false;
            this.tChartData.Panel.Shadow.Width = 0;
            // 
            // 
            // 
            this.tChartData.Panning.Allow = Steema.TeeChart.ScrollModes.Vertical;
            this.tChartData.Series.Add(this.FinishedDataBar);
            this.tChartData.Series.Add(this.SemimanuDataBar);
            this.tChartData.Series.Add(this.FinishedRateLine);
            this.tChartData.Series.Add(this.SemimanuRateLine);
            this.tChartData.Series.Add(this.FinishedProductDateLine);
            this.tChartData.Series.Add(this.SemimanuProductDateLine);
            this.tChartData.Size = new System.Drawing.Size(619, 441);
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.SubFooter.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.SubFooter.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.SubHeader.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.SubHeader.Shadow.Visible = false;
            this.tChartData.TabIndex = 1;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Walls.Back.AutoHide = false;
            // 
            // 
            // 
            this.tChartData.Walls.Back.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.Walls.Bottom.AutoHide = false;
            // 
            // 
            // 
            this.tChartData.Walls.Bottom.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.Walls.Left.AutoHide = true;
            // 
            // 
            // 
            this.tChartData.Walls.Left.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartData.Walls.Right.AutoHide = false;
            // 
            // 
            // 
            this.tChartData.Walls.Right.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartData.Zoom.Brush.Gradient.Visible = true;
            this.tChartData.Zoom.Brush.Solid = false;
            // 
            // axis1
            // 
            // 
            // 
            // 
            this.axis1.AxisPen.Visible = false;
            this.axis1.EndPosition = 29;
            // 
            // 
            // 
            this.axis1.Grid.ZPosition = 0;
            this.axis1.Horizontal = false;
            // 
            // 
            // 
            this.axis1.Labels.CustomSize = 1;
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
            this.axis1.Labels.Font.Shadow.Visible = false;
            this.axis1.Labels.Font.Size = 9;
            this.axis1.Labels.Separation = 46;
            // 
            // 
            // 
            this.axis1.Labels.Shadow.Visible = false;
            this.axis1.MinorTickCount = 0;
            // 
            // 
            // 
            this.axis1.MinorTicks.Length = 0;
            this.axis1.OtherSide = true;
            this.axis1.StartPosition = 5;
            // 
            // 
            // 
            this.axis1.Ticks.Length = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.axis1.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.axis1.Title.Shadow.Visible = false;
            this.axis1.ZPosition = 0;
            // 
            // FinishedDataBar
            // 
            this.FinishedDataBar.BarStyle = Steema.TeeChart.Styles.BarStyles.Cylinder;
            // 
            // 
            // 
            this.FinishedDataBar.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.FinishedDataBar.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.FinishedDataBar.Brush.ImageMode = Steema.TeeChart.Drawing.ImageMode.Normal;
            this.FinishedDataBar.Depth = 0;
            this.FinishedDataBar.LabelMember = "Labels";
            // 
            // 
            // 
            // 
            // 
            // 
            this.FinishedDataBar.Marks.Callout.Arrow = this.FinishedDataBar.Marks.Arrow;
            this.FinishedDataBar.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.FinishedDataBar.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.FinishedDataBar.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.FinishedDataBar.Marks.Callout.Distance = 0;
            this.FinishedDataBar.Marks.Callout.Draw3D = false;
            this.FinishedDataBar.Marks.Callout.Length = 20;
            this.FinishedDataBar.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            // 
            // 
            // 
            this.FinishedDataBar.Marks.Font.Shadow.Visible = false;
            this.FinishedDataBar.Marks.Visible = false;
            this.FinishedDataBar.MultiBar = Steema.TeeChart.Styles.MultiBars.Stacked;
            this.FinishedDataBar.Origin = 10;
            // 
            // 
            // 
            this.FinishedDataBar.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(0)))));
            this.FinishedDataBar.SideMargins = false;
            this.FinishedDataBar.StackGroup = 1;
            this.FinishedDataBar.Title = "FinishedDataBar";
            // 
            // 
            //// 
            //this.FinishedDataBar.XValues.DataMember = "X";
            //this.FinishedDataBar.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            //// 
            //// 
            //// 
            //this.FinishedDataBar.YValues.DataMember = "Bar";
            //// 
            // SemimanuDataBar
            // 
            this.SemimanuDataBar.BarStyle = Steema.TeeChart.Styles.BarStyles.Cylinder;
            // 
            // 
            // 
            this.SemimanuDataBar.Brush.Color = System.Drawing.Color.Yellow;
            // 
            // 
            // 
            this.SemimanuDataBar.Gradient.EndColor = System.Drawing.Color.Yellow;
            this.SemimanuDataBar.Gradient.Visible = true;
            this.SemimanuDataBar.Depth = 0;
            this.SemimanuDataBar.LabelMember = "Labels";
            // 
            // 
            // 
            // 
            // 
            // 
            this.SemimanuDataBar.Marks.Callout.Arrow = this.SemimanuDataBar.Marks.Arrow;
            this.SemimanuDataBar.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.SemimanuDataBar.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.SemimanuDataBar.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.SemimanuDataBar.Marks.Callout.Distance = 0;
            this.SemimanuDataBar.Marks.Callout.Draw3D = false;
            this.SemimanuDataBar.Marks.Callout.Length = 20;
            this.SemimanuDataBar.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            // 
            // 
            // 
            this.SemimanuDataBar.Marks.Font.Shadow.Visible = false;
            this.SemimanuDataBar.Marks.Visible = false;
            this.SemimanuDataBar.MultiBar = Steema.TeeChart.Styles.MultiBars.Stacked;
            // 
            // 
            // 
            this.SemimanuDataBar.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(0)))));
            this.SemimanuDataBar.SideMargins = false;
            this.SemimanuDataBar.StackGroup = 2;
            this.SemimanuDataBar.Title = "SemimanuDataBar";
            // 
            // 
            // 
            //this.SemimanuDataBar.XValues.DataMember = "X";
            //this.SemimanuDataBar.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            //// 
            //// 
            //// 
            //this.SemimanuDataBar.YValues.DataMember = "Bar";
            // 
            // FinishedRateLine
            // 
            this.FinishedRateLine.CustomVertAxis = this.axis1;
            // 
            // 
            // 
            this.FinishedRateLine.LinePen.Color = System.Drawing.Color.Red;
            this.FinishedRateLine.LinePen.Width = 2;
            // 
            // 
            // 
            this.FinishedRateLine.Marks.ArrowLength = 6;
            // 
            // 
            // 
            this.FinishedRateLine.Marks.Callout.Arrow = this.FinishedRateLine.Marks.Arrow;
            this.FinishedRateLine.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.FinishedRateLine.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.FinishedRateLine.Marks.Callout.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.FinishedRateLine.Marks.Callout.Distance = 0;
            this.FinishedRateLine.Marks.Callout.Draw3D = false;
            this.FinishedRateLine.Marks.Callout.HorizSize = 3;
            this.FinishedRateLine.Marks.Callout.Length = 6;
            // 
            // 
            // 
            this.FinishedRateLine.Marks.Callout.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.FinishedRateLine.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Circle;
            this.FinishedRateLine.Marks.Callout.VertSize = 3;
            this.FinishedRateLine.Marks.Callout.Visible = true;
            // 
            // 
            // 
            this.FinishedRateLine.Marks.Font.Bold = true;
            // 
            // 
            // 
            this.FinishedRateLine.Marks.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.FinishedRateLine.Marks.Font.Shadow.Visible = false;
            this.FinishedRateLine.Marks.Font.Size = 11;
            // 
            // 
            // 
            // 
            // 
            // 
            this.FinishedRateLine.Marks.Symbol.Brush.Color = System.Drawing.Color.Red;
            this.FinishedRateLine.Marks.Transparent = true;
            this.FinishedRateLine.Marks.Visible = true;
            this.FinishedRateLine.Title = "半成品";
            this.FinishedRateLine.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Custom;
            // 
            // 
            // 
            //this.FinishedRateLine.XValues.DataMember = "X";
            //this.FinishedRateLine.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            //// 
            //// 
            //// 
            //this.FinishedRateLine.YValues.DataMember = "Y";
            // 
            // SemimanuRateLine
            // 
            this.SemimanuRateLine.CustomVertAxis = this.axis1;
            // 
            // 
            // 
            this.SemimanuRateLine.LinePen.Color = System.Drawing.Color.Green;
            this.SemimanuRateLine.LinePen.Width = 2;
            // 
            // 
            // 
            this.SemimanuRateLine.Marks.ArrowLength = 7;
            // 
            // 
            // 
            this.SemimanuRateLine.Marks.Callout.Arrow = this.SemimanuRateLine.Marks.Arrow;
            this.SemimanuRateLine.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.SemimanuRateLine.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.SemimanuRateLine.Marks.Callout.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.SemimanuRateLine.Marks.Callout.Distance = 0;
            this.SemimanuRateLine.Marks.Callout.Draw3D = false;
            this.SemimanuRateLine.Marks.Callout.HorizSize = 3;
            this.SemimanuRateLine.Marks.Callout.Length = 7;
            // 
            // 
            // 
            this.SemimanuRateLine.Marks.Callout.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.SemimanuRateLine.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Circle;
            this.SemimanuRateLine.Marks.Callout.VertSize = 3;
            this.SemimanuRateLine.Marks.Callout.Visible = true;
            // 
            // 
            // 
            this.SemimanuRateLine.Marks.Font.Bold = true;
            // 
            // 
            // 
            this.SemimanuRateLine.Marks.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.SemimanuRateLine.Marks.Font.Shadow.Visible = false;
            this.SemimanuRateLine.Marks.Font.Size = 11;
            this.SemimanuRateLine.Marks.Transparent = true;
            this.SemimanuRateLine.Marks.Visible = true;
            this.SemimanuRateLine.Title = "成品";
            this.SemimanuRateLine.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Custom;
            // 
            // 
            // 
            //this.SemimanuRateLine.XValues.DataMember = "X";
            //this.SemimanuRateLine.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            //// 
            //// 
            //// 
            //this.SemimanuRateLine.YValues.DataMember = "Y";
            //// 
            // FinishedProductDateLine
            // 
            // 
            // 
            // 
            this.FinishedProductDateLine.LinePen.Color = System.Drawing.Color.Blue;
            this.FinishedProductDateLine.LinePen.Width = 2;
            // 
            // 
            // 
            this.FinishedProductDateLine.Marks.ArrowLength = 1;
            // 
            // 
            // 
            this.FinishedProductDateLine.Marks.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.FinishedProductDateLine.Marks.Callout.Arrow = this.FinishedProductDateLine.Marks.Arrow;
            this.FinishedProductDateLine.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.FinishedProductDateLine.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.FinishedProductDateLine.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.FinishedProductDateLine.Marks.Callout.Distance = 0;
            this.FinishedProductDateLine.Marks.Callout.Draw3D = false;
            this.FinishedProductDateLine.Marks.Callout.Length = 1;
            this.FinishedProductDateLine.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            // 
            // 
            // 
            this.FinishedProductDateLine.Marks.Font.Shadow.Visible = false;
            this.FinishedProductDateLine.Marks.Font.Size = 9;
            // 
            // 
            // 
            // 
            // 
            // 
            this.FinishedProductDateLine.Marks.Symbol.Brush.Color = System.Drawing.Color.Blue;
            this.FinishedProductDateLine.Title = "半成品";
            // 
            // 
            //// 
            //this.FinishedProductDateLine.XValues.DataMember = "X";
            //this.FinishedProductDateLine.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            //// 
            //// 
            //// 
            //this.FinishedProductDateLine.YValues.DataMember = "Y";
            // 
            // SemimanuProductDateLine
            // 
            // 
            // 
            // 
            this.SemimanuProductDateLine.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SemimanuProductDateLine.LinePen.Width = 2;
            // 
            // 
            // 
            // 
            // 
            // 
            this.SemimanuProductDateLine.Marks.Callout.Arrow = this.SemimanuProductDateLine.Marks.Arrow;
            this.SemimanuProductDateLine.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.SemimanuProductDateLine.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.SemimanuProductDateLine.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.SemimanuProductDateLine.Marks.Callout.Distance = 0;
            this.SemimanuProductDateLine.Marks.Callout.Draw3D = false;
            this.SemimanuProductDateLine.Marks.Callout.Length = 10;
            this.SemimanuProductDateLine.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            // 
            // 
            // 
            this.SemimanuProductDateLine.Marks.Font.Shadow.Visible = false;
            this.SemimanuProductDateLine.Title = "成品";
            // 
            // 
            //// 
            //this.SemimanuProductDateLine.XValues.DataMember = "X";
            //this.SemimanuProductDateLine.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            //// 
            //// 
            //// 
            //this.SemimanuProductDateLine.YValues.DataMember = "Y";
            // 
            // TChartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "TChartControl";
            this.Size = new System.Drawing.Size(625, 447);
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private Steema.TeeChart.TChart tChartData;
        private Steema.TeeChart.Axis axis1;
        private Steema.TeeChart.Styles.Bar SemimanuDataBar;
        private Steema.TeeChart.Styles.Bar FinishedDataBar;
        private Steema.TeeChart.Styles.FastLine FinishedRateLine;
        private Steema.TeeChart.Styles.FastLine SemimanuRateLine;
        private Steema.TeeChart.Styles.FastLine FinishedProductDateLine;
        private Steema.TeeChart.Styles.FastLine SemimanuProductDateLine;


    }
}
