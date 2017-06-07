using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Globalization;

namespace BenQGuru.eMES.Web.Helper
{
    /// <summary>
    /// OWCChartSpace 的摘要说明。
    /// </summary>
    [DefaultProperty("Text"),
        ToolboxData("<{0}:OWCChartSpace runat=server></{0}:OWCChartSpace>")]
    public class OWCChartSpace : System.Web.UI.WebControls.WebControl, INamingContainer
    {
        public event EventHandler Click = null;
        public string ChartCombinationType = "owccombinationtype_normal";		//多图组合的绘图方式,默认是正常

        private System.Web.UI.HtmlControls.HtmlInputButton cmdHidden = new System.Web.UI.HtmlControls.HtmlInputButton();
        private System.Web.UI.HtmlControls.HtmlInputHidden hdnSeries = new System.Web.UI.HtmlControls.HtmlInputHidden();
        private System.Web.UI.HtmlControls.HtmlInputHidden hdnCategory = new System.Web.UI.HtmlControls.HtmlInputHidden();
        private System.Web.UI.HtmlControls.HtmlInputHidden hdnValue = new System.Web.UI.HtmlControls.HtmlInputHidden();

        protected override void CreateChildControls()
        {
            this.Controls.Add(this.cmdHidden);
            this.Controls.Add(this.hdnCategory);
            this.Controls.Add(this.hdnSeries);
            this.Controls.Add(this.hdnValue);
        }

        /// <summary> 
        /// 将此控件呈现给指定的输出参数。
        /// </summary>
        /// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter output)
        {
            output.Write(string.Format(this._chartSpaceHtml, this.ChartSpaceName,
                string.Format("{0} {1}", this.Width == Unit.Empty ? "" : "width='" + this.Width.Value + "'",
                                         this.Height == Unit.Empty ? "" : "height='" + this.Height.Value + "'"),
                this.Display ? "block" : "none", this.virtualHostRoot, this.downloadFileURI));

            output.Write(SpellMainScript());
            output.Write(SpellEventScript());

            output.Write(string.Format("<script language=vbscript>Load_{0}()</script>\n", this.ChartSpaceName));

            base.Render(output);
        }

        #region 初始化
        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();

            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.cmdHidden.ServerClick += new EventHandler(cmdHidden_ServerClick);

            this.cmdHidden.ID = "cmdHidden";
            this.cmdHidden.Style["width"] = "0px";
            this.cmdHidden.Style["height"] = "0px";

            this.hdnCategory.ID = "hdnCategory";
            this.hdnSeries.ID = "hdnSeries";
            this.hdnValue.ID = "hdnValue";
        }
        #endregion

        #region 公共方法
        public void AddChart(string seriesName, string[] categories, object[] values)
        {
            this.AddChart(seriesName, categories, values, OWCChartType.ColumnClustered);
        }

        public void AddChart(bool isPercent, string seriesName, string[] categories, object[] values)
        {
            this._seriesNameList.Add(seriesName);
            this.SeriesList.Add(seriesName, new Series(seriesName, categories, values, OWCChartType.ColumnClustered, false, isPercent ? "0.00%" : string.Empty));
        }

        public void AddChart(string seriesName, string[] categories, object[] values, string chartType)
        {
            this._seriesNameList.Add(seriesName);
            this.SeriesList.Add(seriesName, new Series(seriesName, categories, values, chartType));
        }

        public void AddChart(bool isPercent, string seriesName, string[] categories, object[] values, string chartType)
        {
            this._seriesNameList.Add(seriesName);
            this.SeriesList.Add(seriesName, new Series(seriesName, categories, values, chartType, false, isPercent ? "0.00%" : string.Empty));
        }

        public void AddChart(string seriesName, string[] categories, object[] values, string chartType, bool positionRight)
        {
            if (positionRight)
            {
                this._seriesNameList.Add(seriesName);
                this.SeriesList.Add(seriesName, new Series(seriesName, categories, values, chartType, positionRight, string.Empty));
            }
            else
            {
                this._seriesNameList.Add(seriesName);
                this.SeriesList.Add(seriesName, new Series(seriesName, categories, values, chartType, positionRight, string.Empty));
            }
        }

        public void AddChart(string seriesName, string[] categories, object[] values, string chartType, bool extended, object extendedValue)
        {
            if (!extended)
            {
                this.AddChart(seriesName, categories, values, chartType);
            }
            else
            {
                this._seriesNameList.Insert(0, seriesName);

                string[] exCategories = new string[categories.Length + 2];
                object[] exValues = new object[categories.Length + 2];

                for (int i = 0; i < categories.Length; i++)
                {
                    exCategories[i + 1] = categories[i];
                    exValues[i + 1] = values[i];
                }

                exCategories[0] = " ";
                exValues[0] = extendedValue;

                exCategories[categories.Length + 1] = "  ";
                exValues[categories.Length + 1] = extendedValue;

                this.SeriesList.Add(seriesName, new Series(seriesName, exCategories, exValues, chartType));
            }
        }

        public void ClearCharts()
        {
            this._seriesNameList.Clear();
            this.SeriesList.Clear();
        }
        #endregion

        #region 公共属性
        public string ChartSpaceName
        {
            get
            {
                return this.ID + "_ChartSpace";
            }
        }

        public string ChartType
        {
            get
            {
                if (this.ViewState["$ChartType"] == null)
                {
                    this.ViewState["$ChartType"] = OWCChartType.ColumnClustered;
                }

                return this.ViewState["$ChartType"].ToString();
            }
            set
            {
                this.ViewState["$ChartType"] = value;
            }
        }

        public string AxesLeftNumberFormat
        {
            get
            {
                if (this.ViewState["$AxesLeftNumberFormat"] == null)
                {
                    this.ViewState["$AxesLeftNumberFormat"] = string.Empty;
                }

                return this.ViewState["$AxesLeftNumberFormat"].ToString();
            }
            set
            {
                this.ViewState["$AxesLeftNumberFormat"] = value;
            }
        }

        public string DataSource
        {
            get
            {
                if (this.ViewState["$DataSource"] == null)
                {
                    this.ViewState["$DataSource"] = string.Empty;
                }

                return this.ViewState["$DataSource"].ToString();
            }
            set
            {
                this.ViewState["$DataSource"] = value;
            }
        }

        public bool Display
        {
            get
            {
                if (this.ViewState["$Display"] == null)
                {
                    this.ViewState["$Display"] = true;
                }

                return (bool)this.ViewState["$Display"];
            }
            set
            {
                this.ViewState["$Display"] = value;
            }
        }

        public Hashtable SeriesList
        {
            get
            {
                if (this.ViewState["$SeriesList"] == null)
                {
                    this.ViewState["$SeriesList"] = new Hashtable();
                }

                return (Hashtable)this.ViewState["$SeriesList"];
            }
            set
            {
                this.ViewState["$SeriesList"] = value;
            }
        }
        //设置左Y轴坐标最小单元刻度,默认是0, 表示不设置最小单元刻度
        public double ChartLeftMajorUnit
        {
            get
            {
                if (this.ViewState["$ChartLeftMajorUnit"] == null)
                {
                    return 0;
                }

                return (double)this.ViewState["$ChartLeftMajorUnit"];
            }
            set
            {
                this.ViewState["$ChartLeftMajorUnit"] = value;
            }
        }

        //设置左Y轴坐标最大刻度,默认为0表示不设置最大刻度，将会由OWC默认最大刻度
        public double ChartLeftMaximum
        {
            get
            {
                if (this.ViewState["$ChartLeftMaximum"] == null)
                {
                    return 0;
                }

                return (double)this.ViewState["$ChartLeftMaximum"];
            }
            set
            {
                this.ViewState["$ChartLeftMaximum"] = value;
            }
        }
        #endregion

        #region 私有变量

        private string virtualHostRoot
        {
            get
            {
                return string.Format("{0}{1}"
                    , this.Page.Request.Url.Segments[0]
                    , this.Page.Request.Url.Segments[1]);
            }
        }

        //自动下载的owc的路径
        private string downloadFileURI
        {
            get
            {
                return string.Format("/download/owc10.exe");
            }
        }

        private string _chartSpaceHtml = @"
		<div style='display: {2}'>
			<OBJECT id='{0}' {1} width='960' height='540' classid=clsid:0002E556-0000-0000-C000-000000000046 CODEBASE='{3}{4}' VIEWASTEXT>
				<PARAM NAME='XMLData' VALUE='<xml xmlns:x='urn:schemas-microsoft-com:office:excel'>&#13;&#10; <x:ChartSpace>&#13;&#10;  <x:OWCVersion>10.0.0.5605         </x:OWCVersion>&#13;&#10;  <x:Width>15240</x:Width>&#13;&#10;  <x:Height>10160</x:Height>&#13;&#10;  <x:Palette>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#000000</x:Entry>&#13;&#10;   <x:Entry>#8080FF</x:Entry>&#13;&#10;   <x:Entry>#802060</x:Entry>&#13;&#10;   <x:Entry>#FFFFA0</x:Entry>&#13;&#10;   <x:Entry>#A0E0E0</x:Entry>&#13;&#10;   <x:Entry>#600080</x:Entry>&#13;&#10;   <x:Entry>#FF8080</x:Entry>&#13;&#10;   <x:Entry>#008080</x:Entry>&#13;&#10;   <x:Entry>#C0C0FF</x:Entry>&#13;&#10;   <x:Entry>#000080</x:Entry>&#13;&#10;   <x:Entry>#FF00FF</x:Entry>&#13;&#10;   <x:Entry>#80FFFF</x:Entry>&#13;&#10;   <x:Entry>#0080FF</x:Entry>&#13;&#10;   <x:Entry>#FF8080</x:Entry>&#13;&#10;   <x:Entry>#C0FF80</x:Entry>&#13;&#10;   <x:Entry>#FFC0FF</x:Entry>&#13;&#10;   <x:Entry>#FF80FF</x:Entry>&#13;&#10;  </x:Palette>&#13;&#10;  <x:DefaultFont>宋体</x:DefaultFont>&#13;&#10; </x:ChartSpace>&#13;&#10;</xml>'>
				<PARAM NAME='ScreenUpdating' VALUE='-1'>
				<PARAM NAME='EnableEvents' VALUE='-1'>
			</OBJECT>
		<div>
";

        private string _loadSubScript = @"
		Dim chConstants
		Dim chtNewChart

		Set chConstants = Document.Forms(0).{0}.Constants
";

        private string _script = @"
<script language=vbscript>			
	Sub Load_{0}()
{1}
	End Sub
</script>
";

        private string _addSeriesScript = @"
<script language=vbscript>	
	Sub AddSeries{0}(chtNewChart, chConstants)

		Dim asSeriesNames(1)
		Dim asCategories({1})
		Dim aiValues({2})

		asSeriesNames(0) = ""{3}""

{4}

{5}
		' Bind the chart to the arrays.
		set s2 = chtNewChart.SeriesCollection.Add
		{6}
		s2.SetData chConstants.chDimSeriesNames, chConstants.chDataLiteral, asSeriesNames
		s2.SetData chConstants.chDimCategories, chConstants.chDataLiteral, asCategories																																											
		s2.SetData chConstants.chDimValues, chConstants.chDataLiteral, aiValues

		Dim dataLables 
		If (s2.DataLabelsCollection.Count > 0) Then 
				Set dataLables = s2.DataLabelsCollection(0) 
		Else 
				Set dataLables = s2.DataLabelsCollection.Add() 
		End If 
		dataLables.HasValue = true 
		dataLables.NumberFormat = ""{8}"" 

		chtNewChart.HasLegend=true	
		chtNewChart.Legend.Position=chConstants.chLegendPositionRight 
		{7}

	End Sub
</script>
";

        /// <summary>
        /// Y轴在右边
        /// </summary>
        private string _addRightSeriesScript = @"
<script language=vbscript>	
	Sub AddSeries{0}(chtNewChart, chConstants)

		Dim asSeriesNames(1)
		Dim asCategories({1})
		Dim aiValues({2})

		asSeriesNames(0) = ""{3}""

{4}

{5}
		' Bind the chart to the arrays.
		set sc = chtNewChart.SeriesCollection.Add
		{6}
		chtNewChart.SeriesCollection({0}).SetData chConstants.chDimSeriesNames, chConstants.chDataLiteral, asSeriesNames
		chtNewChart.SeriesCollection({0}).SetData chConstants.chDimCategories, chConstants.chDataLiteral, asCategories																																											
		chtNewChart.SeriesCollection({0}).SetData chConstants.chDimValues, chConstants.chDataLiteral, aiValues

		Dim dataLables 
		If (sc.DataLabelsCollection.Count > 0) Then 
				Set dataLables = sc.DataLabelsCollection(0) 
		Else 
				Set dataLables = sc.DataLabelsCollection.Add() 
		End If 
		dataLables.HasValue = true 
		dataLables.NumberFormat = ""{8}"" 

		chtNewChart.HasLegend=true	
		chtNewChart.Legend.Position=chConstants.chLegendPositionRight 
        
		sc.UnGroup TRUE
		Set axIncomeAxis = chtNewChart.Axes.Add(sc.Scalings(chConstants.chDimValues))
		axIncomeAxis.Position = chConstants.chAxisPositionRight
		axIncomeAxis.HasMajorGridlines=false

		{7}

	End Sub
</script>
";

        //addtion 添加pareto 柏拉图,注意柏拉图的y轴坐标是百分数
        private string _addtionSeriesScript = @"
<script language=vbscript>	
	Sub AddSeries{0}(chtNewChart, chConstants)

		Dim asSeriesNames(1)
		Dim asCategories({1})
		Dim aiValues({2})

		asSeriesNames(0) = ""{3}""

{4}

{5}
		' Bind the chart to the arrays.
		set s2 = chtNewChart.SeriesCollection.Add
		{6}
		chtNewChart.SeriesCollection({0}).SetData chConstants.chDimSeriesNames, chConstants.chDataLiteral, asSeriesNames
		chtNewChart.SeriesCollection({0}).SetData chConstants.chDimCategories, chConstants.chDataLiteral, asCategories																																											
		chtNewChart.SeriesCollection({0}).SetData chConstants.chDimValues, chConstants.chDataLiteral, aiValues

		Dim dataLables 
		If (s2.DataLabelsCollection.Count > 0) Then 
				Set dataLables = s2.DataLabelsCollection(0) 
		Else 
				Set dataLables = s2.DataLabelsCollection.Add() 
		End If 
		dataLables.HasValue = true 
		dataLables.NumberFormat = ""{7}"" 

		chtNewChart.HasLegend=true	
		chtNewChart.Legend.Position=chConstants.chLegendPositionRight 
        
		s2.UnGroup TRUE
		Set axIncomeAxis = chtNewChart.Axes.Add(s2.Scalings(chConstants.chDimValues))
		axIncomeAxis.NumberFormat = ""0%""													'坐标格式化为百分数
		axIncomeAxis.Position = chConstants.chAxisPositionRight
		axIncomeAxis.HasMajorGridlines=false

		chtNewChart.Axes(chConstants.chAxisPositionRight).Scaling.Maximum = 1				'设置坐标最大值为1 （100%）
		chtNewChart.Axes(chConstants.chAxisPositionRight).Scaling.Minimum = 0				'设置坐标最小值为0

		

	End Sub
</script>
";

        private string _selectedChangeScript = @"
<script language='javascript' event='SelectionChange' for='{0}'>
	var constants = document.forms(0).{0}.Constants

	if ( document.getElementById('{0}').SelectionType == constants.chSelectionPoint )
	{{	
		document.getElementById('{1}_hdnCategory').value = {0}.Selection.GetValue(constants.chDimCategories,false)
		document.getElementById('{1}_hdnSeries').value = {0}.Selection.GetValue(constants.chDimSeriesNames,false)
		document.getElementById('{1}_hdnValue').value = {0}.Selection.GetValue(constants.chDimValues,false)
			
		document.getElementById('{1}_cmdHidden').click()
	}}
</script>
";

        private ArrayList _seriesNameList
        {
            get
            {
                if (this.ViewState["$SeriesNameList"] == null)
                {
                    this.ViewState["$SeriesNameList"] = new ArrayList();
                }

                return (ArrayList)this.ViewState["$SeriesNameList"];
            }
            set
            {
                this.ViewState["$SeriesNameList"] = value;
            }
        }
        #endregion

        #region 私有方法
        private string SpellMainScript()
        {
            StringBuilder totalBuilder = new StringBuilder("");
            StringBuilder builder = new StringBuilder("");

            if (this._loadSubScript != string.Empty)
            {
                builder.Append(string.Format(_loadSubScript, this.ChartSpaceName, this.ChartType));

                builder.Append(this.SpellAddChartScript());

                builder.Append(this.SpellAttributesScript());
            }
            //以前的方法
            //			for( int i=0; i< this._seriesNameList.Count; i++ )
            //			{
            //				totalBuilder.Append( this.SpellAddSeriesScript((Series)this.SeriesList[this._seriesNameList[i].ToString()], i) );
            //			}

            if (this.ChartCombinationType == "owccombinationtype_normal")
            {
                for (int i = 0; i < this._seriesNameList.Count; i++)
                {
                    if (((Series)this.SeriesList[this._seriesNameList[i].ToString()]).PositionRight)
                    {
                        totalBuilder.Append(this.SpellAddSeriesScript((Series)this.SeriesList[this._seriesNameList[i].ToString()], i, true));
                    }
                    else
                    {
                        totalBuilder.Append(this.SpellAddSeriesScript((Series)this.SeriesList[this._seriesNameList[i].ToString()], i));
                    }
                }
            }
            else if (this.ChartCombinationType == "owccombinationtype_pareto")
            {
                // 注意: 目前只适用于柏拉图 modify by Simone xu 2005/10/13
                for (int i = 0; i < this._seriesNameList.Count; i++)
                {
                    if (i == 0)
                    {
                        totalBuilder.Append(this.SpellAddSeriesScript((Series)this.SeriesList[this._seriesNameList[i].ToString()], i));
                    }
                    else
                    {
                        //如果添加了多个Chart.(目前只有柏拉图) ,执行拼柏拉图的Script
                        totalBuilder.Append(this.SpellAddtionSeriesScript((Series)this.SeriesList[this._seriesNameList[i].ToString()], i));
                    }
                }
            }



            totalBuilder.Append(string.Format(this._script, this.ChartSpaceName, builder.ToString()));

            return totalBuilder.ToString();
        }

        private string SpellEventScript()
        {
            if (this.Click == null)
            {
                return "";
            }

            return string.Format(_selectedChangeScript, this.ChartSpaceName, this.ID);
        }

        private string SpellAddChartScript()
        {
            StringBuilder builder = new StringBuilder("\n");

            if (this._seriesNameList.Count > 0)
            {
                builder.Append(string.Format("		Document.Forms(0).{0}.Clear\n", this.ChartSpaceName));
                builder.Append(string.Format("		Set chtNewChart = Document.Forms(0).{0}.Charts.Add\n", this.ChartSpaceName));
                builder.Append(string.Format("		chtNewChart.Type = chConstants.{0}\n\n", this.ChartType));

                if (AxesLeftNumberFormat.Trim().Length > 0)
                {
                    builder.Append(string.Format("		chtNewChart.Axes(chConstants.chAxisPositionLeft).NumberFormat = \"{0}\"\n\n", this.AxesLeftNumberFormat));
                }

                for (int i = 0; i < _seriesNameList.Count; i++)
                {
                    builder.Append(string.Format("\n		AddSeries{0} chtNewChart, chConstants\n", i.ToString()));
                }
            }

            return builder.ToString();
        }

        private string SpellAddSeriesScript(Series series, int index)
        {
            StringBuilder categoriesBuilder = new StringBuilder("\n");
            StringBuilder valuesBuilder = new StringBuilder("\n");

            for (int i = 0; i < series.Categories.Length; i++)
            {
                categoriesBuilder.Append(string.Format("		asCategories({0}) = \"{1}\"\n", i.ToString(), series.Categories[i]));
            }

            for (int i = 0; i < series.Values.Length; i++)
            {
                valuesBuilder.Append(string.Format("		aiValues({0}) = {1}\n", i.ToString(), series.Values[i].ToString()));
            }

            StringBuilder attrBuilder = new StringBuilder();
            if (series.ChartType.Trim().Length > 0)
            {
                attrBuilder.Append(string.Format("chtNewChart.SeriesCollection({0}).Type = chConstants.{1}\n", index.ToString(), series.ChartType));
            }

            foreach (string key in series.Attributes.Keys)
            {
                attrBuilder.Append(string.Format("		chtNewChart.SeriesCollection({0}).{1} = {2}\n", index.ToString(), series.Attributes[key]));
            }

            string chartscript = string.Empty;
            if (ChartLeftMajorUnit > 0)
            {
                //最小单元刻度
                chartscript = string.Format(@" chtNewChart.Axes(chConstants.chAxisPositionLeft).MajorUnit = {0}
											   ", ChartLeftMajorUnit);
            }
            if (ChartLeftMaximum > 0)
            {
                //最大刻度
                chartscript += string.Format(@" chtNewChart.Axes(chConstants.chAxisPositionleft).Scaling.Maximum = {0}
												", ChartLeftMaximum);

            }

            return string.Format(_addSeriesScript,
                                    index.ToString(),
                                    series.Categories.Length.ToString(),
                                    series.Values.Length.ToString(),
                                    series.Caption,
                                    categoriesBuilder.ToString(),
                                    valuesBuilder.ToString(),
                                    attrBuilder.ToString(),
                                    chartscript,
                                    series.NumberFormat);
        }


        private string SpellAddSeriesScript(Series series, int index, bool positionRight)
        {
            if (positionRight)
            {
                StringBuilder categoriesBuilder = new StringBuilder("\n");
                StringBuilder valuesBuilder = new StringBuilder("\n");

                for (int i = 0; i < series.Categories.Length; i++)
                {
                    categoriesBuilder.Append(string.Format("		asCategories({0}) = \"{1}\"\n", i.ToString(), series.Categories[i]));
                }

                for (int i = 0; i < series.Values.Length; i++)
                {
                    valuesBuilder.Append(string.Format("		aiValues({0}) = {1}\n", i.ToString(), series.Values[i].ToString()));
                }

                StringBuilder attrBuilder = new StringBuilder(string.Format("chtNewChart.SeriesCollection({0}).Type = chConstants.{1}\n", index.ToString(), series.ChartType));

                foreach (string key in series.Attributes.Keys)
                {
                    attrBuilder.Append(string.Format("		chtNewChart.SeriesCollection({0}).{1} = {2}\n", index.ToString(), series.Attributes[key]));
                }

                string chartscript = string.Empty;
                //				if(ChartLeftMajorUnit > 0 )
                //				{
                //					//最小单元刻度
                //					chartscript = string.Format(@" chtNewChart.Axes(chConstants.chAxisPositionLeft).MajorUnit = {0}
                //											   ",ChartLeftMajorUnit);
                //				}
                //				if(ChartLeftMaximum > 0)
                //				{
                //					//最大刻度
                //					chartscript += string.Format(@" chtNewChart.Axes(chConstants.chAxisPositionleft).Scaling.Maximum = {0}
                //												",ChartLeftMaximum);
                //				
                //				}

                return string.Format(_addRightSeriesScript,
                    index.ToString(),
                    series.Categories.Length.ToString(),
                    series.Values.Length.ToString(),
                    series.Caption,
                    categoriesBuilder.ToString(),
                    valuesBuilder.ToString(),
                    attrBuilder.ToString(),
                    chartscript,
                    series.NumberFormat);
            }
            else
            {
                return SpellAddSeriesScript(series, index);
            }
        }

        //addtion 拼柏拉图的 Script
        private string SpellAddtionSeriesScript(Series series, int index)
        {
            StringBuilder categoriesBuilder = new StringBuilder("\n");
            StringBuilder valuesBuilder = new StringBuilder("\n");

            for (int i = 0; i < series.Categories.Length; i++)
            {
                categoriesBuilder.Append(string.Format("		asCategories({0}) = \"{1}\"\n", i.ToString(), series.Categories[i]));
            }

            for (int i = 0; i < series.Values.Length; i++)
            {
                valuesBuilder.Append(string.Format("		aiValues({0}) = {1}\n", i.ToString(), series.Values[i].ToString()));
            }

            StringBuilder attrBuilder = new StringBuilder(string.Format("chtNewChart.SeriesCollection({0}).Type = chConstants.{1}\n", index.ToString(), series.ChartType));

            foreach (string key in series.Attributes.Keys)
            {
                attrBuilder.Append(string.Format("		chtNewChart.SeriesCollection({0}).{1} = {2}\n", index.ToString(), series.Attributes[key]));
            }

            return string.Format(_addtionSeriesScript,
                index.ToString(),
                series.Categories.Length.ToString(),
                series.Values.Length.ToString(),
                series.Caption,
                categoriesBuilder.ToString(),
                valuesBuilder.ToString(),
                attrBuilder.ToString(),
                series.NumberFormat);
        }

        public string SpellAttributesScript()
        {
            StringBuilder builder = new StringBuilder("\n");

            if (this.DataSource != string.Empty && this.DataSource != null)
            {
                builder.Append(string.Format("		Document.Forms(0).{0}.DataSource = Document.Forms(0).{1}\n", this.ChartSpaceName, this.DataSource));
                builder.Append(string.Format("		Document.Forms(0).{0}.DisplayToolbar=false\n", this.ChartSpaceName, this.ChartType));
                builder.Append(string.Format("		Document.Forms(0).{0}.Commands(6052).Execute\n", this.ChartSpaceName, this.ChartType));
                builder.Append(string.Format("		Document.Forms(0).{0}.DisplayFieldList=false\n", this.ChartSpaceName, this.ChartType));
                builder.Append(string.Format("		Document.Forms(0).{0}.Charts(0).Type = chConstants.{1}\n", this.ChartSpaceName, this.ChartType));
                if (this.ChartType == OWCChartType.LineMarkers)
                {
                    builder.Append(string.Format("		For i = 0 To Document.Forms(0).{0}.Charts(0).SeriesCollection.Count-1 \n", this.ChartSpaceName));
                    builder.Append(string.Format("		    Document.Forms(0).{0}.Charts(0).SeriesCollection(i).Marker.Size = 3 \n", this.ChartSpaceName));
                    builder.Append(string.Format("		Next \n"));
                }
                builder.Append(string.Format("		For i = 0 To Document.Forms(0).{0}.Charts(0).SeriesCollection.Count-1 \n", this.ChartSpaceName));
                builder.Append(string.Format("				Dim dataLables \n", this.ChartSpaceName));
                builder.Append(string.Format("				If (Document.Forms(0).{0}.Charts(0).SeriesCollection(i).DataLabelsCollection.Count > 0) Then \n", this.ChartSpaceName));
                builder.Append(string.Format("						Set dataLables = Document.Forms(0).{0}.Charts(0).SeriesCollection(i).DataLabelsCollection(0) \n", this.ChartSpaceName));
                builder.Append(string.Format("				Else \n", this.ChartSpaceName));
                builder.Append(string.Format("						Set dataLables = Document.Forms(0).{0}.Charts(0).SeriesCollection(i).DataLabelsCollection.Add() \n", this.ChartSpaceName));
                builder.Append(string.Format("				End If \n", this.ChartSpaceName));
                builder.Append(string.Format("				dataLables.HasValue = true \n", this.ChartSpaceName));
                builder.Append(string.Format("		Next \n"));
                builder.Append(string.Format("		Document.Forms(0).{0}.Charts(0).HasLegend = True\n\n", this.ChartSpaceName));
            }

            foreach (string key in this.Attributes.Keys)
            {
                builder.Append(string.Format("		Document.Forms(0).{0}.{1} = \"{2}\"\n",
                    this.ChartSpaceName,
                    key,
                    this.Attributes[key]));
            }

            return builder.ToString();
        }

        private void cmdHidden_ServerClick(object sender, EventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, new ChartSpaceSelectionEventArgs(this.hdnSeries.Value, this.hdnCategory.Value, this.hdnValue.Value));
            }
        }
        #endregion
    }

    #region Series
    //	[TypeConverter(typeof(SeriesConverter))]
    [Serializable]
    public class Series
    {
        public Series(string caption, string[] categories, object[] values)
            : this(caption , categories , values, OWCChartType .ColumnClustered , false, string.Empty)
        {
        }

        public Series(string caption, string[] categories, object[] values, string chartType)
            : this(caption , categories , values , chartType , false, string.Empty)
        {
        }

        public Series(string caption, string[] categories, object[] values, string chartType, bool positionRight, string numberFormat)
        {
            this.Caption = caption;
            this.ChartType = chartType;
            this.Categories = new string[categories.Length];
            this.Values = new object[values.Length];
            this.PositionRight = positionRight;
            this.NumberFormat = numberFormat;

            for (int i = 0; i < categories.Length; i++)
            {
                this.Categories[i] = categories[i];
            }

            for (int i = 0; i < values.Length; i++)
            {
                this.Values[i] = values[i];
            }
        }

        public string ChartType = OWCChartType.ColumnClustered;
        public string Caption = "";
        public string[] Categories = null;
        public object[] Values = null;
        public bool PositionRight = false;
        public Hashtable Attributes = new Hashtable();
        public string NumberFormat = "";
    }

    public class SeriesConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                string[] attrs = ((string)value).Split(new char[] { ';' });

                Series series = new Series(attrs[0], attrs[1].Split(','), attrs[2].Split(','), attrs[3]);

                if (attrs.Length > 4)
                {
                    string[] v = null;

                    foreach (string attr in attrs[4].Split(','))
                    {
                        v = attr.Split('=');
                        series.Attributes.Add(v[0], v[1]);
                    }
                }

                return series;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                StringBuilder builder = new StringBuilder();

                builder.Append(((Series)value).Caption + ";");
                builder.Append(string.Join(",", ((Series)value).Categories) + ";");

                foreach (object v in ((Series)value).Values)
                {
                    builder.Append(v.ToString() + ",");
                }

                builder.Remove(builder.Length - 1, 1);
                builder.Append(";" + ((Series)value).ChartType + ";");

                foreach (string key in ((Series)value).Attributes.Keys)
                {
                    builder.Append(string.Format("{0}={1},", key, ((FieldSet)value).Attributes[key].ToString()));
                }

                return builder.ToString(0, builder.Length - 1);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
    #endregion

    #region ChartType
    public class OWCChartType
    {
        public static string Area = "chChartTypeArea";
        public static string Area3D = "chChartTypeArea3D";
        public static string AreaOverlapped3D = "chChartTypeAreaOverlapped3D";
        public static string AreaStacked = "chChartTypeAreaStacked";
        public static string AreaStacked100 = "chChartTypeAreaStacked100";
        public static string AreaStacked1003D = "chChartTypeAreaStacked1003D";

        public static string Bar3D = "chChartTypeBar3D";
        public static string BarClustered = "chChartTypeBarClustered";
        public static string BarClustered3D = "chChartTypeBarClustered3D";
        public static string BarStacked = "chChartTypeBarStacked";
        public static string BarStacked100 = "chChartTypeBarStacked100";
        public static string BarStacked1003D = "chChartTypeBarStacked1003D";

        public static string Column3D = "chChartTypeColumn3D";
        public static string ColumnClustered = "chChartTypeColumnClustered";
        public static string ColumnClustered3D = "chChartTypeColumnClustered3D";
        public static string ColumnStacked = "chChartTypeColumnStacked";
        public static string ColumnStacked100 = "chChartTypeColumnStacked100";
        public static string ColumnStacked1003D = "chChartTypeColumnStacked1003D";
        public static string ColumnStacked3D = "chChartTypeColumnStacked3D";

        public static string Doughnut = "chChartTypeDoughnut";
        public static string DoughnutExploded = "chChartTypeDoughnutExploded";

        public static string Line = "chChartTypeLine";
        public static string Line3D = "chChartTypeLine3D";
        public static string LineMarkers = "chChartTypeLineMarkers";
        public static string LineOverlapped3D = "chChartTypeLineOverlapped3D";
        public static string LineStacked = "chChartTypeLineStacked";
        public static string LineStacked100 = "chChartTypeLineStacked100";
        public static string LineStacked1003D = "chChartTypeLineStacked1003D";
        public static string LineStacked100Markers = "chChartTypeLineStacked100Markers";
        public static string LineStacked3D = "chChartTypeLineStacked3D";
        public static string StackedMarkers = "chChartTypeLineStackedMarkers";

        public static string Pie = "chChartTypePie";
        public static string Pie3D = "chChartTypePie3D";
        public static string PieExploded = "chChartTypePieExploded";
        public static string PieExploded3D = "chChartTypePieExploded3D";
        public static string PieStacked = "chChartTypePieStacked";

        public static string RadarLine = "chChartTypeRadarLine";
        public static string RadarLineFilled = "chChartTypeRadarLineFilled";
        public static string RadarLineMarkers = "chChartTypeRadarLineMarkers";
        public static string RadarSmoothLine = "chChartTypeRadarSmoothLine";
        public static string RadarSmoothLineMarkers = "chChartTypeRadarSmoothLineMarkers";

        public static string SmoothLine = "chChartTypeSmoothLine";
        public static string SmoothLineMarkers = "chChartTypeSmoothLineMarkers";
        public static string SmoothLineStacked = "chChartTypeSmoothLineStacked";
        public static string SmoothLineStacked100 = "chChartTypeSmoothLineStacked100";
        public static string SmoothLineStacked100Markers = "chChartTypeSmoothLineStacked100Markers";
        public static string SmoothLineStackedMarkers = "chChartTypeSmoothLineStackedMarkers";
    }
    #endregion

    #region ChartSpaceSelectionEventArgs
    public class ChartSpaceSelectionEventArgs : System.EventArgs
    {
        private string _seriesName = string.Empty;
        public string Category = null;
        public string Value = null;

        public string SeriesName
        {
            get
            {
                return this._seriesName;
            }
        }

        public ChartSpaceSelectionEventArgs(string seriesName, string category, string value)
        {
            this._seriesName = seriesName;
            this.Category = category;
            this.Value = value;
        }
    }
    #endregion
}
