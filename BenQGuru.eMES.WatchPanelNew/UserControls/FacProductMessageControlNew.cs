using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;


namespace BenQGuru.eMES.WatchPanelNew
{
    public partial class FacProductMessageControlNew : System.Windows.Forms.UserControl
    {
        #region 变量
        private DataTable Pei_DataTable = new DataTable();
        public object[] TodayQtyDataSource
        {
            get;
            set;
        }

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
        //五大不良原因DataSource
        public object[] PeiDataSource
        {
            get;
            set;
        }

        public object InputQutputQty
        {
            get;
            set;
        }

        public string OpValue
        {
            get;
            set;
        }

        public string YearTargetValue
        {
            get;
            set;
        }

        public bool IsWorkShopPanel
        {
            get;
            set;
        }

        private string dimension;
        private string sscode;
        private string workShopCode;

        public string Sscode
        {
            get { return sscode; }
            set { sscode = value; }
        }

        public string WorkShopCode
        {
            get { return workShopCode; }
            set { workShopCode = value; }
        }

        public string Dimension
        {
            get { return dimension; }
            set { dimension = value; }
        }
        #endregion

        #region 事件
        public FacProductMessageControlNew()
        {
            InitializeComponent();
        }
        #endregion

        #region 自定义函数

        public void InitControlsValue()
        {
            if (!string.IsNullOrEmpty(sscode) && IsWorkShopPanel == false)
            {
                //产线看板
                this.hearMessageControl.SetTitle = string.Format("产线{0}电子看板\n Production Line Overview", sscode);
            }
            else
            {
                //车间看板
                this.hearMessageControl.SetTitle = string.Format("车间{0}电子看板\n WorkShop Overview", workShopCode);
            }
            //  调整两个图表的位置
            Rectangle ScreenArea = System.Windows.Forms.Screen.GetBounds(this);
            int heigth = (int)(ScreenArea.Height - this.hearMessageControl.Height) / 2;
            this.panelLeftTop.Height = heigth;
            this.panelLeftBottom.Height = ScreenArea.Height - this.hearMessageControl.Height - heigth;

            //设置投入/产出柱状图
            try
            {
                SetTodayValue();
            }
            catch (Exception)
            {
                throw new Exception("SetTodayValue");
            }

            //设置直通率
            try
            {
                SetFirstPassYield();
            }
            catch (Exception)
            {
                throw new Exception("SetFirstPassYield");
            }

            try
            {
                SetInputOutputQty();

                SetPeiChartValue();
            }
            catch (Exception)
            {

                throw new Exception("Other");
            }
            //设置当天投入产出量

            if (OpValue != "0/")
            {
                this.labelOpNumber.Text = OpValue;
            }
            this.panelRight.Visible = true;
        }

        private void SetTodayValue()
        {
            this.chartToday.ChartAreas[0].AxisX.Interval = 1;
            this.chartToday.ChartAreas[0].AxisX.IntervalOffset = 1;
            this.chartToday.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;

            this.chartToday.Series[0].IsValueShownAsLabel = true;
            this.chartToday.Series[0].IsVisibleInLegend = true;
            this.chartToday.Series[0].Name = "投入";

            this.chartToday.Series[1].IsValueShownAsLabel = true;
            this.chartToday.Series[1].IsVisibleInLegend = true;
            this.chartToday.Series[1].Name = "产出";

            if (IsWorkShopPanel)
            {
                this.chartToday.Titles[0].Text = string.Format("产线{0}投入/产出柱状图\\nInput /Output Histogram", sscode);
            }

            if (TodayQtyDataSource != null)
            {
                this.chartToday.Series[0].Points.Clear();
                this.chartToday.Series[1].Points.Clear();

                foreach (NewReportDomainObject obj in TodayQtyDataSource)
                {
                    if (dimension.Equals(TimeDimension.Week))
                    {
                        obj.PeriodCode = this.ToDateString(Convert.ToInt32(obj.PeriodCode), "/");
                    }
                    if (dimension.Equals(TimeDimension.Month))
                    {
                        obj.PeriodCode = string.Format(@"第{0}周", obj.PeriodCode);
                    }
                    this.chartToday.Series[0].Points.AddXY(obj.PeriodCode, obj.Input);
                    this.chartToday.Series[1].Points.AddXY(obj.PeriodCode, obj.Output);
                }
            }
            this.chartToday.AlignDataPointsByAxisLabel();
        }

        //直通率
        private void SetFirstPassYield()
        {
            this.FirstPassYield.ChartAreas[0].AxisX.Interval = 1;
            this.FirstPassYield.ChartAreas[0].AxisX.IntervalOffset = 1;
            this.FirstPassYield.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;

            if (IsWorkShopPanel)
            {
                this.FirstPassYield.Titles[0].Text = string.Format("产线{0}直通率\\nFirst Pass Yield(FPY)", sscode);
            }

            if (WeekQtyDataSource != null)
            {
                this.FirstPassYield.Series[0].Points.Clear();
                this.FirstPassYield.Series[0].IsValueShownAsLabel = true;
                this.FirstPassYield.Series[0].Name = string.Format(@"产线{0}", sscode);

                foreach (NewReportDomainObject obj in WeekQtyDataSource)
                {
                    if (dimension.Equals(TimeDimension.Week))
                    {
                        obj.PeriodCode = this.ToDateString(Convert.ToInt32(obj.PeriodCode), "/");
                    }
                    if (dimension.Equals(TimeDimension.Month))
                    {
                        obj.PeriodCode = string.Format(@"第{0}周", obj.PeriodCode);
                    }
                    this.FirstPassYield.Series[0].Points.AddXY(obj.PeriodCode, obj.PassRcardRate);
                }
            }

            this.FirstPassYield.AlignDataPointsByAxisLabel(PointSortOrder.Ascending);
        }

        //前五大不良代码饼状图
        private void SetPeiChartValue()
        {
            this.chartRateNG.ChartAreas[0].AxisX.Interval = 1;
            this.chartRateNG.ChartAreas[0].AxisX.IntervalOffset = 1;
            this.chartRateNG.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;

            this.Pei_DataTable = new DataTable();
            Pei_DataTable.Columns.Add("ErroeCodeDesc", typeof(string));
            Pei_DataTable.Columns.Add("ErrorCodeRate", typeof(double));
            Pei_DataTable.AcceptChanges();

            decimal rate = 0;
            if (PeiDataSource != null)
            {
                foreach (QDOTSInfo obj in PeiDataSource)
                {
                    DataRow newRow = this.Pei_DataTable.NewRow();

                    newRow["ErroeCodeDesc"] = obj.ErrorCodeDesc;
                    newRow["ErrorCodeRate"] = Math.Round(obj.Percent, 4);
                    rate += Math.Round(obj.Percent, 4);
                    Pei_DataTable.Rows.Add(newRow);
                }

                DataRow Row = this.Pei_DataTable.NewRow();
                Row["ErroeCodeDesc"] = "其他原因";
                Row["ErrorCodeRate"] = Math.Round(1 - rate, 4);
                Pei_DataTable.Rows.Add(Row);

                Pei_DataTable.AcceptChanges();
                this.chartRateNG.Series[0].IsValueShownAsLabel = true;
                this.chartRateNG.Series[0].IsVisibleInLegend = true;
                this.chartRateNG.Series[0].YValueMembers = Pei_DataTable.Columns["ErrorCodeRate"].ToString();
                this.chartRateNG.Series[0].XValueMember = Pei_DataTable.Columns["ErroeCodeDesc"].ToString();
            }
            chartRateNG.DataSource = Pei_DataTable;

        }

        private void SetInputOutputQty()
        {
            if (InputQutputQty != null)
            {
                this.labelTHTValue.Text = (InputQutputQty as NewReportDomainObject).Output.ToString();
                this.labelSMTValue.Text = (InputQutputQty as NewReportDomainObject).Input.ToString();
            }
            else
            {
                this.labelTHTValue.Text = "0";
                this.labelSMTValue.Text = "0";
            }
        }

        private string ToDateString(int date, string dateSplitChar)
        {
            if (date == 0)
            {
                return string.Empty;
            }

            string dateString = date.ToString().PadLeft(8, '0');

            return string.Format("{0}{1}{2}{3}{4}"
                                , dateString.Substring(0, 4)
                                , dateSplitChar
                                , dateString.Substring(4, 2)
                                , dateSplitChar
                                , dateString.Substring(6, 2));

        }

        #endregion
    }
}
