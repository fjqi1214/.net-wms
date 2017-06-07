using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Domain.ReportView;

namespace BenQGuru.eMES.ReportView
{
    public class ReportDesignView
    {
        public string ReportID = "";
        public string UploadFileName = "";

        public Dictionary<string, RptViewDataSourceColumn> DataSourceColumns = new Dictionary<string, RptViewDataSourceColumn>();
        public RptViewDesignMain DesignMain = null;
        public RptViewGridColumn[] GridColumns = null;
        public RptViewGridGroup[] GridGroups = null;
        public RptViewGridGroupTotal[] GridGroupTotals = null;
        public RptViewGridFilter[] GridFilters = null;
        public RptViewGridDataFormat[] GridDataFormats = null;
        public RptViewDataFormat[] DataFormats = null;
        public RptViewChartMain[] ChartMains = null;
        public RptViewChartSeries[] ChartSeries = null;
        public RptViewChartCategory[] ChartCategories = null;
        public RptViewChartData[] ChartDatas = null;
        public RptViewDataFormat[] ChartDataFormats = null;
        public RptViewFileParameter[] FileParameters = null;
        public RptViewReportSecurity[] ReportSecurity = null;
        public RptViewReportStyle DefinedReportStyle = null;
        public RptViewExtendText[] ExtendText = null;
        public RptViewFilterUI[] FiltersUI = null;

        /// <summary>
        /// 查询某个分组的其他字段统计方式列表
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public Dictionary<string, RptViewGridGroupTotal> GetSubTotalByGroup(RptViewGridGroup group)
        {
            Dictionary<string, RptViewGridGroupTotal> subTotal = new Dictionary<string, RptViewGridGroupTotal>();
            for (int i = 0; this.GridGroupTotals != null && i < this.GridGroupTotals.Length; i++)
            {
                if (this.GridGroupTotals[i].GroupSequence == group.GroupSequence)
                {
                    subTotal.Add(this.GridGroupTotals[i].ColumnName, this.GridGroupTotals[i]);
                }
            }
            return subTotal;
        }
    }
}
