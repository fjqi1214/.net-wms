using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.Web.Helper;
using ControlLibrary.Web.Language;

namespace BenQGuru.Web.ReportCenter
{
    public class ReportChartHelper
    {
        public ReportChartHelper(OWCPivotTable userOWCPivotTable, OWCChartSpace userOWCChart, LanguageComponent languageComponent)
        {
            _UserOWCPivotTable = userOWCPivotTable;
            _UserOWCChart = userOWCChart;
            _LanguageComponent = languageComponent;
        }

        #region  Ù–‘

        private LanguageComponent _LanguageComponent = null;
        private OWCPivotTable _UserOWCPivotTable = null;
        private OWCChartSpace _UserOWCChart = null;
        private string _UserOWCChartType = NewReportDisplayType.HistogramChart;
        private object[] _DataSource = new object[0];
        private string[] _RowPropertyList = new string[0];
        private string[] _ColumnPropertyList = new string[0];
        private string[] _DataPropertyList = new string[0];
        private string[] _DataPropertyFormatList = new string[0];
        private bool _IsPercent = false;

        public LanguageComponent LanguageComponent
        {
            get { return _LanguageComponent; }
            set { _LanguageComponent = value; }
        }

        public OWCPivotTable UserOWCPivotTable
        {
            get { return _UserOWCPivotTable; }
            set { _UserOWCPivotTable = value; }
        }

        public OWCChartSpace UserOWCChart
        {
            get { return _UserOWCChart; }
            set { _UserOWCChart = value; }
        }

        public string UserOWCChartType
        {
            get { return _UserOWCChartType; }
            set { _UserOWCChartType = value; }
        }

        public object[] DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }

        public string[] RowPropertyList
        {
            get { return _RowPropertyList; }
            set { _RowPropertyList = value; }
        }

        public string[] ColumnPropertyList
        {
            get { return _ColumnPropertyList; }
            set { _ColumnPropertyList = value; }
        }

        public string[] DataPropertyList
        {
            get { return _DataPropertyList; }
            set { _DataPropertyList = value; }
        }

        public string[] DataPropertyFormatList
        {
            get { return _DataPropertyFormatList; }
            set { _DataPropertyFormatList = value; }
        }

        public bool IsPercent
        {
            get { return _IsPercent; }
            set { _IsPercent = value; }
        }

        #endregion

        private string[] GetOWCSchema()
        {
            ArrayList schemaList = new ArrayList();

            foreach (string property in _RowPropertyList)
            {
                schemaList.Add(property);
            }

            foreach (string property in _ColumnPropertyList)
            {
                schemaList.Add(property);
            }

            foreach (string property in _DataPropertyList)
            {
                schemaList.Add(property);
            }

            return (string[])schemaList.ToArray(typeof(string));
        }

        public void LoadOWCChart()
        {
            _UserOWCPivotTable.SetDataSource(_DataSource, GetOWCSchema());
            _UserOWCPivotTable.ClearFieldSet();

            foreach (string property in _RowPropertyList)
            {
                _UserOWCPivotTable.AddRowFieldSet(property, true);
            }

            foreach (string property in _ColumnPropertyList)
            {
                _UserOWCPivotTable.AddColumnFieldSet(property, true);
            }

            for (int i = 0; i < _DataPropertyList.Length; i++)
            {
                string property = _DataPropertyList[i];

                string format = string.Empty;
                if (i < _DataPropertyFormatList.Length)
                {
                    format = _DataPropertyFormatList[i];
                }

                if (_IsPercent)
                {
                    _UserOWCPivotTable.AddTotalField(_LanguageComponent.GetString(property), property, PivotTotalFunctionType.Sum, NumberFormat.Percent);
                }
                else if (format.Trim().Length > 0)
                {
                    _UserOWCPivotTable.AddTotalField(_LanguageComponent.GetString(property), property, PivotTotalFunctionType.Sum, format);
                }
                else
                {
                    _UserOWCPivotTable.AddTotalField(_LanguageComponent.GetString(property), property, PivotTotalFunctionType.Sum);
                }
            }

            _UserOWCChart.DataSource = _UserOWCPivotTable.PivotTableName;

            if (_UserOWCChartType == NewReportDisplayType.LineChart)
            {
                _UserOWCChart.ChartType = OWCChartType.LineMarkers;
            }
            else if (_UserOWCChartType == NewReportDisplayType.HistogramChart)
            {
                _UserOWCChart.ChartType = OWCChartType.ColumnClustered;
            }
            else if (_UserOWCChartType == NewReportDisplayType.PieChart)
            {
                _UserOWCChart.ChartType = OWCChartType.Pie;
            }
        }
    }
}
