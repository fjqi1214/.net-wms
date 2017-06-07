namespace BenQGuru.eMES.ClientWatchPanel
{
	partial class FacProductMessageControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FacProductMessageControl));
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.hearMessageControl = new BenQGuru.eMES.ClientWatchPanel.HearMessageControl();
            this.panelDown = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tChartNGRate = new Steema.TeeChart.TChart();
            this.NGRatePie = new Steema.TeeChart.Styles.Pie();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tChartLotPass = new Steema.TeeChart.TChart();
            this.LotbarJoin = new Steema.TeeChart.Styles.BarJoin();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.lblFinshCaption = new System.Windows.Forms.Label();
            this.lblFinshItemQty = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblSemimanuCaption = new System.Windows.Forms.Label();
            this.lblSemimanuFactureQty = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.lblSSCodeListOutProduct = new System.Windows.Forms.Label();
            this.lblSSCodeListInProduct = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ultraGridProduct = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panelHeader.SuspendLayout();
            this.panelDown.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panelCenter.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.panelHeader.Controls.Add(this.hearMessageControl);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(987, 79);
            this.panelHeader.TabIndex = 0;
            // 
            // hearMessageControl
            // 
            this.hearMessageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hearMessageControl.Location = new System.Drawing.Point(0, 0);
            this.hearMessageControl.Name = "hearMessageControl";
            this.hearMessageControl.Size = new System.Drawing.Size(987, 79);
            this.hearMessageControl.TabIndex = 0;
            this.hearMessageControl.Load += new System.EventHandler(this.hearMessageControl_Load);
            // 
            // panelDown
            // 
            this.panelDown.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.panelDown.Controls.Add(this.panel8);
            this.panelDown.Controls.Add(this.panel7);
            this.panelDown.Location = new System.Drawing.Point(0, 466);
            this.panelDown.Name = "panelDown";
            this.panelDown.Size = new System.Drawing.Size(984, 159);
            this.panelDown.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.panel8.Controls.Add(this.tChartNGRate);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.panel8.Location = new System.Drawing.Point(619, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(365, 159);
            this.panel8.TabIndex = 0;
            // 
            // tChartNGRate
            // 
            // 
            // 
            // 
            this.tChartNGRate.Aspect.Chart3DPercent = 0;
            this.tChartNGRate.Aspect.Elevation = 315;
            this.tChartNGRate.Aspect.Orthogonal = false;
            this.tChartNGRate.Aspect.Perspective = 0;
            this.tChartNGRate.Aspect.Rotation = 360;
            this.tChartNGRate.Aspect.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.tChartNGRate.Aspect.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.tChartNGRate.Aspect.View3D = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.Bottom.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.Bottom.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Axes.Bottom.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.Bottom.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Axes.Bottom.Title.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.Depth.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.Depth.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Axes.Depth.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.Depth.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Axes.Depth.Title.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.DepthTop.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.DepthTop.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Axes.DepthTop.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.DepthTop.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Axes.DepthTop.Title.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.Left.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.Left.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Axes.Left.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.Left.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Axes.Left.Title.Shadow.Visible = false;
            this.tChartNGRate.Axes.Left.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.Right.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.Right.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Axes.Right.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.Right.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Axes.Right.Title.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.Top.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.Top.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Axes.Top.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Axes.Top.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Axes.Top.Title.Shadow.Visible = false;
            this.tChartNGRate.BackColor = System.Drawing.Color.Transparent;
            this.tChartNGRate.Cursor = System.Windows.Forms.Cursors.Default;
            this.tChartNGRate.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Footer.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Footer.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Header.Font.Bold = true;
            // 
            // 
            // 
            this.tChartNGRate.Header.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tChartNGRate.Header.Font.Shadow.Visible = false;
            this.tChartNGRate.Header.Font.Size = 15;
            this.tChartNGRate.Header.Lines = new string[] {
        "前五大不良原因"};
            // 
            // 
            // 
            this.tChartNGRate.Header.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Legend.Font.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Legend.Title.Font.Bold = true;
            // 
            // 
            // 
            this.tChartNGRate.Legend.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Legend.Title.Pen.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Legend.Title.Shadow.Visible = false;
            this.tChartNGRate.Legend.Visible = false;
            this.tChartNGRate.Location = new System.Drawing.Point(0, 0);
            this.tChartNGRate.Name = "tChartNGRate";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Panel.Bevel.ColorOne = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.tChartNGRate.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.Lowered;
            // 
            // 
            // 
            this.tChartNGRate.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.tChartNGRate.Panel.Brush.Visible = false;
            this.tChartNGRate.Panel.MarginBottom = 3;
            this.tChartNGRate.Panel.MarginLeft = 2;
            this.tChartNGRate.Panel.MarginRight = 2;
            this.tChartNGRate.Panel.MarginTop = 3;
            // 
            // 
            // 
            this.tChartNGRate.Panel.Shadow.Visible = false;
            this.tChartNGRate.Series.Add(this.NGRatePie);
            this.tChartNGRate.Size = new System.Drawing.Size(365, 159);
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.SubFooter.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.SubFooter.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.SubHeader.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.SubHeader.Shadow.Visible = false;
            this.tChartNGRate.TabIndex = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartNGRate.Walls.Back.AutoHide = false;
            // 
            // 
            // 
            this.tChartNGRate.Walls.Back.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Walls.Bottom.AutoHide = false;
            // 
            // 
            // 
            this.tChartNGRate.Walls.Bottom.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Walls.Left.AutoHide = false;
            // 
            // 
            // 
            this.tChartNGRate.Walls.Left.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            // 
            // 
            // 
            this.tChartNGRate.Walls.Left.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Walls.Right.AutoHide = false;
            // 
            // 
            // 
            this.tChartNGRate.Walls.Right.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartNGRate.Zoom.Animated = true;
            this.tChartNGRate.Zoom.AnimatedSteps = 7;
            this.tChartNGRate.Zoom.MinPixels = 30;
            // 
            // NGRatePie
            // 
            // 
            // 
            // 
            this.NGRatePie.Brush.Color = System.Drawing.Color.Red;
            this.NGRatePie.Circled = true;
            this.NGRatePie.LabelMember = "Labels";
            this.NGRatePie.Labels = ((Steema.TeeChart.Styles.StringList)(resources.GetObject("NGRatePie.Labels")));
            // 
            // 
            // 
            // 
            // 
            // 
            this.NGRatePie.Marks.Arrow.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.NGRatePie.Marks.Arrow.Width = 2;
            this.NGRatePie.Marks.ArrowLength = -12;
            // 
            // 
            // 
            this.NGRatePie.Marks.Callout.Arrow = this.NGRatePie.Marks.Arrow;
            this.NGRatePie.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.NGRatePie.Marks.Callout.ArrowHeadSize = 2;
            // 
            // 
            // 
            this.NGRatePie.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.NGRatePie.Marks.Callout.Distance = 12;
            this.NGRatePie.Marks.Callout.Draw3D = false;
            this.NGRatePie.Marks.Callout.Length = -12;
            this.NGRatePie.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            this.NGRatePie.Marks.Font.Bold = true;
            // 
            // 
            // 
            this.NGRatePie.Marks.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.NGRatePie.Marks.Font.Shadow.Visible = false;
            this.NGRatePie.Marks.Font.Size = 9;
            // 
            // 
            // 
            // 
            // 
            // 
            this.NGRatePie.Marks.Symbol.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.NGRatePie.Marks.Transparent = true;
            // 
            // 
            // 
            this.NGRatePie.Shadow.Height = 20;
            this.NGRatePie.Shadow.Width = 20;
            this.NGRatePie.Title = "NGRatePie";
            // 
            // 
            // 
            this.NGRatePie.XValues.DataMember = "Angle";
            this.NGRatePie.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.NGRatePie.YValues.DataMember = "Pie";
            this.NGRatePie.YValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.panel7.Controls.Add(this.tChartLotPass);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(619, 159);
            this.panel7.TabIndex = 0;
            // 
            // tChartLotPass
            // 
            // 
            // 
            // 
            this.tChartLotPass.Aspect.Chart3DPercent = 0;
            this.tChartLotPass.Aspect.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.tChartLotPass.Aspect.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.tChartLotPass.Aspect.View3D = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.Bottom.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            // 
            // 
            // 
            this.tChartLotPass.Axes.Bottom.Grid.Centered = true;
            this.tChartLotPass.Axes.Bottom.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.tChartLotPass.Axes.Bottom.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.Bottom.Labels.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tChartLotPass.Axes.Bottom.Labels.Font.Shadow.Visible = false;
            this.tChartLotPass.Axes.Bottom.Labels.Font.Size = 9;
            // 
            // 
            // 
            this.tChartLotPass.Axes.Bottom.Labels.Shadow.Visible = false;
            this.tChartLotPass.Axes.Bottom.MinorTickCount = 4;
            // 
            // 
            // 
            this.tChartLotPass.Axes.Bottom.MinorTicks.Length = 0;
            // 
            // 
            // 
            this.tChartLotPass.Axes.Bottom.TicksInner.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.Bottom.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Axes.Bottom.Title.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.Depth.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.Depth.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Axes.Depth.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.Depth.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Axes.Depth.Title.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.DepthTop.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.DepthTop.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Axes.DepthTop.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.DepthTop.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Axes.DepthTop.Title.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.Left.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.Left.Labels.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tChartLotPass.Axes.Left.Labels.Font.Shadow.Visible = false;
            this.tChartLotPass.Axes.Left.Labels.Font.Size = 9;
            this.tChartLotPass.Axes.Left.Labels.Separation = 9;
            // 
            // 
            // 
            this.tChartLotPass.Axes.Left.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.Left.Title.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tChartLotPass.Axes.Left.Title.Font.Shadow.Visible = false;
            this.tChartLotPass.Axes.Left.Title.Font.Size = 9;
            // 
            // 
            // 
            this.tChartLotPass.Axes.Left.Title.Shadow.Visible = false;
            this.tChartLotPass.Axes.Left.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.Right.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.Right.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Axes.Right.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.Right.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Axes.Right.Title.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.Top.Grid.ZPosition = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.Top.Labels.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Axes.Top.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Axes.Top.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Axes.Top.Title.Shadow.Visible = false;
            this.tChartLotPass.BackColor = System.Drawing.Color.Transparent;
            this.tChartLotPass.Cursor = System.Windows.Forms.Cursors.Default;
            this.tChartLotPass.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Footer.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Footer.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Header.Font.Bold = true;
            // 
            // 
            // 
            this.tChartLotPass.Header.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tChartLotPass.Header.Font.Shadow.Visible = false;
            this.tChartLotPass.Header.Font.Size = 15;
            this.tChartLotPass.Header.Lines = new string[] {
        "批合格率"};
            // 
            // 
            // 
            this.tChartLotPass.Header.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Legend.Font.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Legend.Title.Font.Bold = true;
            // 
            // 
            // 
            this.tChartLotPass.Legend.Title.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Legend.Title.Pen.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Legend.Title.Shadow.Visible = false;
            this.tChartLotPass.Legend.Visible = false;
            this.tChartLotPass.Location = new System.Drawing.Point(0, 0);
            this.tChartLotPass.Name = "tChartLotPass";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.Lowered;
            // 
            // 
            // 
            this.tChartLotPass.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartLotPass.Panel.Shadow.Visible = false;
            this.tChartLotPass.Series.Add(this.LotbarJoin);
            this.tChartLotPass.Size = new System.Drawing.Size(619, 159);
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.SubFooter.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.SubFooter.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.SubHeader.Font.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.SubHeader.Shadow.Visible = false;
            this.tChartLotPass.TabIndex = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartLotPass.Walls.Back.AutoHide = false;
            // 
            // 
            // 
            this.tChartLotPass.Walls.Back.Shadow.Visible = false;
            this.tChartLotPass.Walls.Back.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Walls.Bottom.AutoHide = false;
            // 
            // 
            // 
            this.tChartLotPass.Walls.Bottom.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            // 
            // 
            // 
            this.tChartLotPass.Walls.Bottom.Shadow.Visible = false;
            this.tChartLotPass.Walls.Bottom.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Walls.Left.AutoHide = false;
            // 
            // 
            // 
            this.tChartLotPass.Walls.Left.Shadow.Visible = false;
            this.tChartLotPass.Walls.Left.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Walls.Right.AutoHide = false;
            // 
            // 
            // 
            this.tChartLotPass.Walls.Right.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartLotPass.Zoom.AnimatedSteps = 7;
            // 
            // LotbarJoin
            // 
            this.LotbarJoin.BarStyle = Steema.TeeChart.Styles.BarStyles.Cylinder;
            // 
            // 
            // 
            this.LotbarJoin.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(188)))), ((int)(((byte)(216)))), ((int)(((byte)(53)))));
            // 
            // 
            // 
            this.LotbarJoin.Gradient.EndColor = System.Drawing.Color.Red;
            // 
            // 
            // 
            // 
            // 
            // 
            this.LotbarJoin.Marks.Arrow.Visible = false;
            // 
            // 
            // 
            this.LotbarJoin.Marks.Callout.Arrow = this.LotbarJoin.Marks.Arrow;
            this.LotbarJoin.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.LotbarJoin.Marks.Callout.ArrowHeadSize = 7;
            // 
            // 
            // 
            this.LotbarJoin.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.LotbarJoin.Marks.Callout.Distance = -17;
            this.LotbarJoin.Marks.Callout.Draw3D = false;
            this.LotbarJoin.Marks.Callout.Length = 20;
            this.LotbarJoin.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            this.LotbarJoin.Marks.Font.Bold = true;
            // 
            // 
            // 
            this.LotbarJoin.Marks.Font.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            // 
            // 
            // 
            this.LotbarJoin.Marks.Font.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            // 
            // 
            // 
            this.LotbarJoin.Marks.Font.Shadow.Brush.Gradient.Transparency = 1;
            this.LotbarJoin.Marks.Font.Shadow.Visible = false;
            this.LotbarJoin.Marks.Font.Size = 9;
            // 
            // 
            // 
            this.LotbarJoin.Marks.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.LotbarJoin.Marks.Symbol.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(232)))), ((int)(((byte)(53)))));
            this.LotbarJoin.Marks.Transparent = true;
            // 
            // 
            // 
            this.LotbarJoin.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(153)))), ((int)(((byte)(139)))), ((int)(((byte)(32)))));
            this.LotbarJoin.Title = "LotbarJoin";
            // 
            // 
            // 
            this.LotbarJoin.XValues.DataMember = "X";
            this.LotbarJoin.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.LotbarJoin.YValues.DataMember = "Bar";
            // 
            // panelCenter
            // 
            this.panelCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.panelCenter.Controls.Add(this.panel2);
            this.panelCenter.Controls.Add(this.panel1);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCenter.Location = new System.Drawing.Point(0, 79);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(987, 381);
            this.panelCenter.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(715, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(272, 381);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel10);
            this.panel4.Controls.Add(this.lblFinshItemQty);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(272, 381);
            this.panel4.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.panel10.Controls.Add(this.lblFinshCaption);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(272, 31);
            this.panel10.TabIndex = 2;
            // 
            // lblFinshCaption
            // 
            this.lblFinshCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(86)))), ((int)(((byte)(96)))));
            this.lblFinshCaption.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinshCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFinshCaption.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblFinshCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinshCaption.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblFinshCaption.Location = new System.Drawing.Point(0, 0);
            this.lblFinshCaption.Name = "lblFinshCaption";
            this.lblFinshCaption.Size = new System.Drawing.Size(272, 31);
            this.lblFinshCaption.TabIndex = 0;
            this.lblFinshCaption.Text = "成 品 产 量";
            this.lblFinshCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFinshItemQty
            // 
            this.lblFinshItemQty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFinshItemQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.lblFinshItemQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinshItemQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinshItemQty.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblFinshItemQty.Location = new System.Drawing.Point(0, 34);
            this.lblFinshItemQty.Name = "lblFinshItemQty";
            this.lblFinshItemQty.Size = new System.Drawing.Size(269, 174);
            this.lblFinshItemQty.TabIndex = 1;
            this.lblFinshItemQty.Text = "0";
            this.lblFinshItemQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.lblSemimanuCaption);
            this.panel6.Controls.Add(this.lblSemimanuFactureQty);
            this.panel6.Location = new System.Drawing.Point(0, 208);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(272, 173);
            this.panel6.TabIndex = 1;
            // 
            // lblSemimanuCaption
            // 
            this.lblSemimanuCaption.AutoEllipsis = true;
            this.lblSemimanuCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(86)))), ((int)(((byte)(96)))));
            this.lblSemimanuCaption.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSemimanuCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSemimanuCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSemimanuCaption.ForeColor = System.Drawing.SystemColors.Window;
            this.lblSemimanuCaption.Location = new System.Drawing.Point(0, 0);
            this.lblSemimanuCaption.Name = "lblSemimanuCaption";
            this.lblSemimanuCaption.Size = new System.Drawing.Size(268, 30);
            this.lblSemimanuCaption.TabIndex = 2;
            this.lblSemimanuCaption.Text = "半 成 品 产 量";
            this.lblSemimanuCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSemimanuFactureQty
            // 
            this.lblSemimanuFactureQty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSemimanuFactureQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.lblSemimanuFactureQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSemimanuFactureQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSemimanuFactureQty.ForeColor = System.Drawing.Color.Yellow;
            this.lblSemimanuFactureQty.Location = new System.Drawing.Point(-2, 30);
            this.lblSemimanuFactureQty.Name = "lblSemimanuFactureQty";
            this.lblSemimanuFactureQty.Size = new System.Drawing.Size(267, 143);
            this.lblSemimanuFactureQty.TabIndex = 1;
            this.lblSemimanuFactureQty.Text = "0";
            this.lblSemimanuFactureQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(715, 381);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.panel9);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Location = new System.Drawing.Point(0, 209);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(712, 172);
            this.panel5.TabIndex = 1;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.lblSSCodeListOutProduct);
            this.panel9.Controls.Add(this.lblSSCodeListInProduct);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(79, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(633, 172);
            this.panel9.TabIndex = 5;
            // 
            // lblSSCodeListOutProduct
            // 
            this.lblSSCodeListOutProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.lblSSCodeListOutProduct.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSSCodeListOutProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSSCodeListOutProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSSCodeListOutProduct.ForeColor = System.Drawing.Color.Red;
            this.lblSSCodeListOutProduct.Location = new System.Drawing.Point(0, 89);
            this.lblSSCodeListOutProduct.Name = "lblSSCodeListOutProduct";
            this.lblSSCodeListOutProduct.Size = new System.Drawing.Size(633, 83);
            this.lblSSCodeListOutProduct.TabIndex = 4;
            // 
            // lblSSCodeListInProduct
            // 
            this.lblSSCodeListInProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.lblSSCodeListInProduct.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSSCodeListInProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSSCodeListInProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSSCodeListInProduct.ForeColor = System.Drawing.Color.Lime;
            this.lblSSCodeListInProduct.Location = new System.Drawing.Point(0, 0);
            this.lblSSCodeListInProduct.Name = "lblSSCodeListInProduct";
            this.lblSSCodeListInProduct.Size = new System.Drawing.Size(633, 89);
            this.lblSSCodeListInProduct.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(85)))), ((int)(((byte)(96)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 172);
            this.label2.TabIndex = 0;
            this.label2.Text = "      在产       概况";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ultraGridProduct);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(715, 207);
            this.panel3.TabIndex = 0;
            // 
            // ultraGridProduct
            // 
            this.ultraGridProduct.Cursor = System.Windows.Forms.Cursors.Default;
            appearance31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            appearance31.BorderColor = System.Drawing.Color.White;
            this.ultraGridProduct.DisplayLayout.Appearance = appearance31;
            this.ultraGridProduct.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
            appearance32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(86)))), ((int)(((byte)(96)))));
            appearance32.BorderColor = System.Drawing.Color.White;
            appearance32.FontData.BoldAsString = "True";
            appearance32.FontData.SizeInPoints = 16F;
            appearance32.ForeColor = System.Drawing.Color.White;
            this.ultraGridProduct.DisplayLayout.CaptionAppearance = appearance32;
            appearance33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            appearance33.BorderColor = System.Drawing.Color.White;
            appearance33.FontData.BoldAsString = "True";
            appearance33.FontData.Name = "Arial";
            appearance33.FontData.SizeInPoints = 16F;
            appearance33.ForeColor = System.Drawing.Color.Black;
            this.ultraGridProduct.DisplayLayout.Override.CellAppearance = appearance33;
            appearance34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(86)))), ((int)(((byte)(96)))));
            appearance34.BorderColor = System.Drawing.Color.White;
            this.ultraGridProduct.DisplayLayout.Override.FixedHeaderAppearance = appearance34;
            appearance35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(86)))), ((int)(((byte)(96)))));
            appearance35.BorderColor = System.Drawing.Color.White;
            appearance35.FontData.BoldAsString = "True";
            appearance35.FontData.SizeInPoints = 12F;
            appearance35.ForeColor = System.Drawing.Color.White;
            this.ultraGridProduct.DisplayLayout.Override.HeaderAppearance = appearance35;
            this.ultraGridProduct.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridProduct.Location = new System.Drawing.Point(0, 0);
            this.ultraGridProduct.Name = "ultraGridProduct";
            this.ultraGridProduct.Size = new System.Drawing.Size(715, 207);
            this.ultraGridProduct.SupportThemes = false;
            this.ultraGridProduct.TabIndex = 0;
            this.ultraGridProduct.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridProduct_InitializeLayout);
            // 
            // FacProductMessageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelDown);
            this.Controls.Add(this.panelHeader);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(46)))), ((int)(((byte)(42)))));
            this.Name = "FacProductMessageControl";
            this.Size = new System.Drawing.Size(987, 625);
            this.panelHeader.ResumeLayout(false);
            this.panelDown.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panelCenter.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridProduct)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelDown;
        private System.Windows.Forms.Panel panelCenter;
        private HearMessageControl hearMessageControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridProduct;
        private System.Windows.Forms.Label lblFinshCaption;
        private System.Windows.Forms.Label lblFinshItemQty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lblSemimanuFactureQty;
        private Steema.TeeChart.TChart tChartLotPass;
        private Steema.TeeChart.Styles.BarJoin LotbarJoin;
        private Steema.TeeChart.TChart tChartNGRate;
        private Steema.TeeChart.Styles.Pie NGRatePie;
        private System.Windows.Forms.Label lblSSCodeListInProduct;
        private System.Windows.Forms.Label lblSSCodeListOutProduct;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label lblSemimanuCaption;
	}
}
