namespace BenQGuru.eMES.ClientWatchPanel
{
    partial class FLIneWatchPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLIneWatchPanel));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.userControlExceptionMessage = new BenQGuru.eMES.ClientWatchPanel.ExceptionMessageControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.userControlNormalMessage = new BenQGuru.eMES.ClientWatchPanel.NormalMessageControl();
            this.ultraProdcutDataGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.timerException = new System.Windows.Forms.Timer(this.components);
            this.timerThreeArea = new System.Windows.Forms.Timer(this.components);
            this.DataChart = new Steema.TeeChart.TChart();
            this.axis1 = new Steema.TeeChart.Axis(this.components);
            this.OutPutJoin = new Steema.TeeChart.Styles.BarJoin();
            this.RateLine = new Steema.TeeChart.Styles.FastLine();
            this.userControlHearderMessqge = new BenQGuru.eMES.ClientWatchPanel.HearMessageControl();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraProdcutDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1280, 148);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.userControlExceptionMessage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(1);
            this.panel2.Size = new System.Drawing.Size(874, 148);
            this.panel2.TabIndex = 2;
            // 
            // userControlExceptionMessage
            // 
            this.userControlExceptionMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlExceptionMessage.ExpectionMessage = "";
            this.userControlExceptionMessage.Location = new System.Drawing.Point(1, 1);
            this.userControlExceptionMessage.Name = "userControlExceptionMessage";
            this.userControlExceptionMessage.Size = new System.Drawing.Size(872, 146);
            this.userControlExceptionMessage.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.userControlNormalMessage);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(874, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(1);
            this.panel3.Size = new System.Drawing.Size(406, 148);
            this.panel3.TabIndex = 1;
            // 
            // userControlNormalMessage
            // 
            this.userControlNormalMessage.BackColor = System.Drawing.Color.Black;
            this.userControlNormalMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlNormalMessage.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userControlNormalMessage.Location = new System.Drawing.Point(1, 1);
            this.userControlNormalMessage.Name = "userControlNormalMessage";
            this.userControlNormalMessage.Size = new System.Drawing.Size(404, 146);
            this.userControlNormalMessage.TabIndex = 0;
            // 
            // ultraProdcutDataGrid
            // 
            this.ultraProdcutDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraProdcutDataGrid.Cursor = System.Windows.Forms.Cursors.Default;
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(162)))), ((int)(((byte)(163)))));
            appearance1.BorderColor = System.Drawing.Color.White;
            appearance1.ForeColor = System.Drawing.Color.White;
            this.ultraProdcutDataGrid.DisplayLayout.Appearance = appearance1;
            this.ultraProdcutDataGrid.DisplayLayout.MaxRowScrollRegions = 12;
            appearance2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(162)))), ((int)(((byte)(163)))));
            this.ultraProdcutDataGrid.DisplayLayout.Override.ActiveCellAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(162)))), ((int)(((byte)(163)))));
            appearance3.BorderColor = System.Drawing.Color.White;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.ItalicAsString = "False";
            appearance3.FontData.Name = "Arial";
            appearance3.ForeColor = System.Drawing.Color.Black;
            this.ultraProdcutDataGrid.DisplayLayout.Override.ActiveRowAppearance = appearance3;
            appearance4.BorderColor = System.Drawing.Color.Transparent;
            this.ultraProdcutDataGrid.DisplayLayout.Override.AddRowAppearance = appearance4;
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(163)))), ((int)(((byte)(162)))));
            appearance5.BorderColor = System.Drawing.Color.White;
            appearance5.FontData.SizeInPoints = 12F;
            appearance5.ForeColor = System.Drawing.Color.Black;
            this.ultraProdcutDataGrid.DisplayLayout.Override.CellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(162)))), ((int)(((byte)(163)))));
            appearance6.BorderColor = System.Drawing.Color.White;
            this.ultraProdcutDataGrid.DisplayLayout.Override.FixedCellAppearance = appearance6;
            appearance7.BackColor = System.Drawing.Color.White;
            appearance7.BackColor2 = System.Drawing.Color.White;
            appearance7.BorderColor = System.Drawing.Color.White;
            this.ultraProdcutDataGrid.DisplayLayout.Override.FixedHeaderAppearance = appearance7;
            appearance8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(86)))), ((int)(((byte)(96)))));
            appearance8.BackColor2 = System.Drawing.Color.White;
            appearance8.BorderColor = System.Drawing.Color.White;
            appearance8.FontData.BoldAsString = "True";
            appearance8.FontData.Name = "Arial";
            appearance8.FontData.SizeInPoints = 16F;
            this.ultraProdcutDataGrid.DisplayLayout.Override.HeaderAppearance = appearance8;
            appearance9.BorderColor = System.Drawing.Color.White;
            appearance9.FontData.BoldAsString = "True";
            appearance9.FontData.ItalicAsString = "False";
            appearance9.FontData.Name = "Arial";
            appearance9.FontData.SizeInPoints = 12F;
            appearance9.ForeColor = System.Drawing.Color.White;
            this.ultraProdcutDataGrid.DisplayLayout.Override.RowAppearance = appearance9;
            this.ultraProdcutDataGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(162)))), ((int)(((byte)(163)))));
            this.ultraProdcutDataGrid.DisplayLayout.Override.SelectedCellAppearance = appearance10;
            appearance11.BackColor = System.Drawing.Color.White;
            appearance11.BackColor2 = System.Drawing.Color.White;
            appearance11.BorderColor = System.Drawing.Color.White;
            this.ultraProdcutDataGrid.DisplayLayout.Override.SelectedRowAppearance = appearance11;
            this.ultraProdcutDataGrid.Location = new System.Drawing.Point(0, 220);
            this.ultraProdcutDataGrid.Name = "ultraProdcutDataGrid";
            this.ultraProdcutDataGrid.Size = new System.Drawing.Size(1280, 237);
            this.ultraProdcutDataGrid.SupportThemes = false;
            this.ultraProdcutDataGrid.TabIndex = 5;
            this.ultraProdcutDataGrid.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraProdcutDataGrid_InitializeLayout);
            // 
            // timerException
            // 
            this.timerException.Enabled = true;
            this.timerException.Interval = 20000;
            this.timerException.Tick += new System.EventHandler(this.timerException_Tick);
            // 
            // timerThreeArea
            // 
            this.timerThreeArea.Enabled = true;
            this.timerThreeArea.Interval = 20000;
            this.timerThreeArea.Tick += new System.EventHandler(this.timerThreeArea_Tick);
            // 
            // DataChart
            // 
            // 
            // 
            // 
            this.DataChart.Aspect.Chart3DPercent = 0;
            this.DataChart.Aspect.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.DataChart.Aspect.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.DataChart.Aspect.VertOffset = 1;
            this.DataChart.Aspect.View3D = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Axes.Bottom.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DataChart.Axes.Bottom.AxisPen.Visible = false;
            this.DataChart.Axes.Bottom.AxisPen.Width = 0;
            // 
            // 
            // 
            this.DataChart.Axes.Bottom.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            this.DataChart.Axes.Bottom.Grid.Width = 0;
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
            this.DataChart.Axes.Bottom.Labels.Font.Shadow.Visible = false;
            this.DataChart.Axes.Bottom.Labels.Font.Size = 9;
            // 
            // 
            // 
            this.DataChart.Axes.Bottom.Labels.Shadow.Visible = false;
            this.DataChart.Axes.Bottom.MinorTickCount = 0;
            // 
            // 
            // 
            this.DataChart.Axes.Bottom.MinorTicks.Length = 0;
            // 
            // 
            // 
            this.DataChart.Axes.Bottom.Ticks.Length = 0;
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
            this.DataChart.Axes.Depth.AxisPen.Width = 0;
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
            this.DataChart.Axes.DepthTop.AxisPen.Width = 0;
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
            this.DataChart.Axes.Left.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DataChart.Axes.Left.AxisPen.Visible = false;
            this.DataChart.Axes.Left.AxisPen.Width = 1;
            // 
            // 
            // 
            this.DataChart.Axes.Left.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
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
            // 
            // 
            // 
            this.DataChart.Axes.Left.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.DataChart.Axes.Left.Labels.Font.Shadow.Height = 0;
            this.DataChart.Axes.Left.Labels.Font.Shadow.Visible = false;
            this.DataChart.Axes.Left.Labels.Font.Shadow.Width = 0;
            this.DataChart.Axes.Left.Labels.Font.Size = 9;
            // 
            // 
            // 
            this.DataChart.Axes.Left.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.Axes.Left.MinorTicks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DataChart.Axes.Left.MinorTicks.Length = 0;
            this.DataChart.Axes.Left.StartPosition = 33;
            // 
            // 
            // 
            this.DataChart.Axes.Left.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DataChart.Axes.Left.Ticks.Length = 2;
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
            this.DataChart.Axes.Right.AxisPen.Width = 1;
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
            // 
            // 
            // 
            this.DataChart.Axes.Right.Labels.Font.Shadow.Visible = false;
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
            this.DataChart.Axes.Top.AxisPen.Width = 1;
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
            this.DataChart.Axes.Top.Visible = false;
            this.DataChart.BackColor = System.Drawing.Color.Transparent;
            this.DataChart.Cursor = System.Windows.Forms.Cursors.Default;
            this.DataChart.Dock = System.Windows.Forms.DockStyle.Bottom;
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
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Legend.Font.Shadow.Visible = false;
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
            this.DataChart.Location = new System.Drawing.Point(0, 453);
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
            this.DataChart.Panel.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.DataChart.Panel.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.DataChart.Panel.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.DataChart.Panel.Gradient.UseMiddle = true;
            this.DataChart.Panel.Brush.ImageMode = Steema.TeeChart.Drawing.ImageMode.Normal;
            this.DataChart.Panel.ImageMode = Steema.TeeChart.Drawing.ImageMode.Normal;
            // 
            // 
            // 
            this.DataChart.Panel.Pen.Width = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            this.DataChart.Panel.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.DataChart.Panel.Shadow.Height = 0;
            this.DataChart.Panel.Shadow.Visible = false;
            this.DataChart.Series.Add(this.OutPutJoin);
            this.DataChart.Series.Add(this.RateLine);
            this.DataChart.Size = new System.Drawing.Size(1280, 379);
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
            this.DataChart.TabIndex = 8;
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
            this.DataChart.Walls.Back.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(69)))), ((int)(((byte)(86)))), ((int)(((byte)(96)))));
            // 
            // 
            // 
            this.DataChart.Walls.Back.Shadow.Visible = false;
            this.DataChart.Walls.Back.Transparent = false;
            // 
            // 
            // 
            this.DataChart.Walls.Bottom.AutoHide = false;
            // 
            // 
            // 
            this.DataChart.Walls.Bottom.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DataChart.Walls.Bottom.Brush.ForegroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            // 
            // 
            // 
            this.DataChart.Walls.Bottom.Gradient.Direction = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.DataChart.Walls.Bottom.Brush.Solid = false;
            this.DataChart.Walls.Bottom.Brush.Style = System.Drawing.Drawing2D.HatchStyle.DashedDownwardDiagonal;
            // 
            // 
            // 
            this.DataChart.Walls.Bottom.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
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
            this.DataChart.Walls.Left.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.DataChart.Walls.Left.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.DataChart.Walls.Left.Shadow.Visible = false;
            // 
            // 
            // 
            this.DataChart.Walls.Right.AutoHide = false;
            // 
            // 
            // 
            this.DataChart.Walls.Right.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.DataChart.Walls.Right.Shadow.Visible = false;
            this.DataChart.Walls.Right.Visible = true;
            // 
            // axis1
            // 
            // 
            // 
            // 
            this.axis1.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.axis1.AxisPen.Visible = false;
            this.axis1.AxisPen.Width = 1;
            this.axis1.EndPosition = 29;
            // 
            // 
            // 
            this.axis1.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
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
            this.axis1.Labels.Font.Shadow.Visible = false;
            this.axis1.Labels.Font.Size = 9;
            // 
            // 
            // 
            this.axis1.Labels.Shadow.Visible = false;
            this.axis1.MinorTickCount = 2;
            // 
            // 
            // 
            this.axis1.MinorTicks.Length = 0;
            this.axis1.OtherSide = true;
            this.axis1.StartPosition = 8;
            // 
            // 
            // 
            this.axis1.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.axis1.Title.Font.Brush.Color = System.Drawing.Color.Yellow;
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
            // OutPutJoin
            // 
            this.OutPutJoin.BarStyle = Steema.TeeChart.Styles.BarStyles.Cylinder;
            this.OutPutJoin.BarWidthPercent = 40;
            // 
            // 
            // 
            this.OutPutJoin.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(188)))), ((int)(((byte)(216)))), ((int)(((byte)(53)))));
            // 
            // 
            // 
            this.OutPutJoin.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(188)))), ((int)(((byte)(216)))), ((int)(((byte)(53)))));
            this.OutPutJoin.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(188)))), ((int)(((byte)(216)))), ((int)(((byte)(53)))));
            this.OutPutJoin.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(188)))), ((int)(((byte)(216)))), ((int)(((byte)(53)))));
            this.OutPutJoin.Gradient.UseMiddle = true;
            this.OutPutJoin.Gradient.Visible = true;
            this.OutPutJoin.Brush.Solid = false;
            this.OutPutJoin.LabelMember = "Labels";
            // 
            // 
            // 
            // 
            // 
            // 
            this.OutPutJoin.Marks.Arrow.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.OutPutJoin.Marks.Arrow.Width = 4;
            this.OutPutJoin.Marks.ArrowLength = 0;
            // 
            // 
            // 
            this.OutPutJoin.Marks.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(224)))));
            // 
            // 
            // 
            this.OutPutJoin.Marks.Callout.Arrow = this.OutPutJoin.Marks.Arrow;
            this.OutPutJoin.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.OutPutJoin.Marks.Callout.ArrowHeadSize = 0;
            // 
            // 
            // 
            this.OutPutJoin.Marks.Callout.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.OutPutJoin.Marks.Callout.Distance = 3;
            this.OutPutJoin.Marks.Callout.Draw3D = false;
            this.OutPutJoin.Marks.Callout.Length = 0;
            // 
            // 
            // 
            this.OutPutJoin.Marks.Callout.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.OutPutJoin.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Circle;
            this.OutPutJoin.Marks.Callout.Visible = true;
            // 
            // 
            // 
            this.OutPutJoin.Marks.Font.Bold = true;
            // 
            // 
            // 
            this.OutPutJoin.Marks.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.OutPutJoin.Marks.Font.Shadow.Visible = false;
            this.OutPutJoin.Marks.Font.Size = 12;
            // 
            // 
            // 
            this.OutPutJoin.Marks.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.OutPutJoin.Marks.Symbol.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.OutPutJoin.Marks.Transparent = true;
            // 
            // 
            // 
            this.OutPutJoin.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(113)))), ((int)(((byte)(130)))), ((int)(((byte)(32)))));
            this.OutPutJoin.Pen.DashCap = System.Drawing.Drawing2D.DashCap.Round;
            this.OutPutJoin.Pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            this.OutPutJoin.Pen.Style = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            this.OutPutJoin.Pen.Visible = false;
            this.OutPutJoin.SideMargins = false;
            this.OutPutJoin.Title = "OutPutJoin";
            // 
            // 
            // 
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
            // 
            // 
            // 
            this.RateLine.LinePen.Color = System.Drawing.Color.Red;
            this.RateLine.LinePen.Width = 2;
            // 
            // 
            // 
            this.RateLine.Marks.ArrowLength = 3;
            // 
            // 
            // 
            this.RateLine.Marks.Callout.Arrow = this.RateLine.Marks.Arrow;
            this.RateLine.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.RateLine.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.RateLine.Marks.Callout.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.RateLine.Marks.Callout.Distance = 0;
            this.RateLine.Marks.Callout.Draw3D = false;
            this.RateLine.Marks.Callout.Length = 3;
            // 
            // 
            // 
            this.RateLine.Marks.Callout.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.RateLine.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Circle;
            this.RateLine.Marks.Callout.Visible = true;
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
            this.RateLine.Marks.Font.Shadow.Visible = false;
            this.RateLine.Marks.Font.Size = 12;
            // 
            // 
            // 
            // 
            // 
            // 
            this.RateLine.Marks.Symbol.Brush.Color = System.Drawing.Color.Red;
            this.RateLine.Marks.Transparent = true;
            this.RateLine.Marks.Visible = true;
            this.RateLine.Title = "RateLine";
            this.RateLine.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Custom;
            // 
            // 
            // 
            //this.RateLine.XValues.DataMember = "X";
            //this.RateLine.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            //// 
            //// 
            //// 
            //this.RateLine.YValues.DataMember = "Y";
            // 
            // userControlHearderMessqge
            // 
            this.userControlHearderMessqge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(47)))), ((int)(((byte)(42)))));
            this.userControlHearderMessqge.Dock = System.Windows.Forms.DockStyle.Top;
            this.userControlHearderMessqge.Location = new System.Drawing.Point(0, 0);
            this.userControlHearderMessqge.Name = "userControlHearderMessqge";
            this.userControlHearderMessqge.Size = new System.Drawing.Size(1280, 72);
            this.userControlHearderMessqge.TabIndex = 1;
            // 
            // FLIneWatchPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1280, 832);
            this.Controls.Add(this.DataChart);
            this.Controls.Add(this.ultraProdcutDataGrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.userControlHearderMessqge);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FLIneWatchPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "产线看板";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FLIneWatchPanel_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FLIneWatchPanel_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraProdcutDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private NormalMessageControl userControlNormalMessage;
        private HearMessageControl userControlHearderMessqge;
        private ExceptionMessageControl userControlExceptionMessage;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraProdcutDataGrid;
        private System.Windows.Forms.Timer timerException;
        private System.Windows.Forms.Timer timerThreeArea;
        private Steema.TeeChart.TChart DataChart;
        private Steema.TeeChart.Styles.BarJoin OutPutJoin;
        private Steema.TeeChart.Styles.FastLine RateLine;
        private Steema.TeeChart.Axis axis1;


    }
}

