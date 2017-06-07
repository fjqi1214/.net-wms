using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting; 

namespace BenQGuru.eMES.WatchPanelNew
{
    public partial class TChartControl : System.Windows.Forms.UserControl
    {
        private Hashtable objectList = new Hashtable();
        public object[] WeekPassQtyDataSource
        {
            get;
            set;
        }

        public object[] WeekQtyDataSource
        {
            get;
            set;
        }

        public string YearTargetValue
        {
            get;
            set;
        }

        public TChartControl()
        {
            InitializeComponent();
        }

        public void SetHeader(string hearName)
        {
            this.datachart.Titles.Add (hearName);
        }       
        public void SetDataChartValue(object[] WeekPassQtyDataSource, object[] WeekQtyDataSource,string YearTargetValue)       
        {
            this.datachart.ChartAreas[0].AxisX.Interval = 1;
            this.datachart.ChartAreas[0].AxisX.IntervalOffset = 1;
            this.datachart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
            this.datachart.Series.Clear();
            #region 一周内车间产量图例

            if (WeekQtyDataSource != null)
            {
                string oldSSCode = string.Empty;
                Series sr = null;
                foreach (NewReportDomainObject obj in WeekQtyDataSource)
                {
                    if (oldSSCode != obj.StepSequenceDescription)
                    {
                        sr = new Series();
                        sr.Points.AddXY(obj.ShiftDay, obj.Output);
                        sr.IsValueShownAsLabel = true;
                        sr.IsVisibleInLegend = true;
                        sr.YAxisType = AxisType.Secondary;
                        sr.XAxisType = AxisType.Primary;
                        sr.LabelForeColor = Color.WhiteSmoke;
                        sr.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                        sr.Name = obj.StepSequenceDescription;
                        sr.ChartType = SeriesChartType.Column;
                        sr.SmartLabelStyle.Enabled = false;
                        this.datachart.Series.Add(sr);
                        oldSSCode = obj.StepSequenceDescription;
                    }
                    else
                    {
                        sr.Points.AddXY(obj.ShiftDay, obj.Output);
                    }
                }
            }

            #endregion

            #region 一周内车间直通率图例

            if (WeekPassQtyDataSource != null)
            {
                string oldSSCode = string.Empty;
                Series sr = null;
                foreach (NewReportDomainObject obj in WeekPassQtyDataSource)
                {
                    if (oldSSCode != obj.StepSequenceDescription + " ")
                    {
                        sr = new Series();
                        sr.Points.AddXY(obj.ShiftDay, Math.Round(obj.PassRcardRate, 4));
                        sr.IsValueShownAsLabel = true;
                        sr.IsVisibleInLegend = true;
                        sr.YAxisType = AxisType.Primary;
                        sr.XAxisType = AxisType.Primary;
                        sr.BorderWidth = 3;
                        sr.LabelFormat = "{P2}";
                        sr.LabelForeColor = Color.Lime;
                        sr.MarkerColor = Color.Lime;
                        sr.MarkerStyle = MarkerStyle.Circle;
                        sr.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                        sr.Name = obj.StepSequenceDescription + " ";
                        sr.ChartType = SeriesChartType.Line;
                        sr.SmartLabelStyle.Enabled = false;
                        this.datachart.Series.Add(sr);
                        oldSSCode = sr.Name;
                    }
                    else
                    {
                        sr.Points.AddXY(obj.ShiftDay, Math.Round(obj.PassRcardRate, 4));
                    }
                }
            }

            #endregion

            #region 直通率年度指标
            if (YearTargetValue != string.Empty)
            {
                StripLine sl1 = new StripLine();
                sl1.BackColor = System.Drawing.Color.Red;
                sl1.IntervalOffset = Double.Parse(YearTargetValue.ToString());
                sl1.StripWidth = 0.001;
                sl1.Text = "直通率年度指标" + string.Format("{0:0%}", Double.Parse(YearTargetValue));
                sl1.TextAlignment = StringAlignment.Near;
                sl1.Font = new Font("新宋体", 10, FontStyle.Bold);
                datachart.ChartAreas[0].AxisY.StripLines.Add(sl1);
            }
            #endregion
            this.datachart.AlignDataPointsByAxisLabel(PointSortOrder.Ascending);
            this.datachart.Refresh();
        }        
       
        public bool IsShowTChart
        {
            set { this.datachart.Visible = value; }
        }
    }
}
