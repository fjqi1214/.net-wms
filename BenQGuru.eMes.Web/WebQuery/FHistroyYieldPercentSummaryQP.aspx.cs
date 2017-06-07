using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Material;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FHistroyYieldPercentSummaryQP 的摘要说明。
	/// </summary>
	public partial class FHistroyYieldPercentSummaryQP : BaseQPageNew
	{
		//protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		//private System.ComponentModel.IContainer components;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{		
			RadioButtonListBuilder builder1 = new RadioButtonListBuilder(
				new TimingType(),this.rblTimingType,this.languageComponent1);

			RadioButtonListBuilder builder2 = new RadioButtonListBuilder(
				new SummaryTarget(),this.rblSummaryTarget,this.languageComponent1);			

			RadioButtonListBuilder builder3 = new RadioButtonListBuilder(
				new VisibleStyle(),this.rblVisibleStyle,this.languageComponent1);			

			RadioButtonListBuilder builder4 = new RadioButtonListBuilder(
				new ChartStyle(),this.rblChartType,this.languageComponent1);

			RadioButtonListBuilder builder5 = new RadioButtonListBuilder(
				new YieldCatalog(),this.rblYieldCatalog,this.languageComponent1);

            this.gridHelper = new GridHelperNew(gridWebGrid, DtSource);
            this.gridWebGrid.Visible = false;
            this.cmdGridExport.Visible = false;
            this.columnChart.Visible = false;
            this.lineChart.Visible = false;
					
			if( !this.IsPostBack )
			{
				// 初始化页面语言
				//this.InitPageLanguage(this.languageComponent1, false);

				builder1.Build();
				builder2.Build();
				builder3.Build();
				builder4.Build();
				builder5.Build();
				/*
				//Laws Lu,2007/01/17 不显示PPM和直通率
				rblYieldCatalog.Items.RemoveAt(1);
				rblYieldCatalog.Items.RemoveAt(1);
				*/

				this.dateStartDateQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
				this.dateEndDateQuery.Text = this.dateStartDateQuery.Text;
                			

				this.rblSummaryTarget.Attributes.Add("onclick","judgePPM()" ) ;
				this.rblYieldCatalog.Attributes.Add("onclick","judgePPM()" ) ;
                              
                if (this.Request.Params["Width"] != null)
                {
                    ViewState["Width"] = this.Request.Params["Width"];
                }

                if (this.Request.Params["Height"] != null)
                {
                    ViewState["Height"] = this.Request.Params["Height"];
                }
                                
			}

			RadioButtonListBuilder.FormatListControlStyle( this.rblChartType,50 );
			RadioButtonListBuilder.FormatListControlStyle( this.rblTimingType,50 );
			RadioButtonListBuilder.FormatListControlStyle( this.rblSummaryTarget,50 );
			RadioButtonListBuilder.FormatListControlStyle( this.rblVisibleStyle,50 );
			RadioButtonListBuilder.FormatListControlStyle( this.rblYieldCatalog,50 );
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
            //this.components = new System.ComponentModel.Container();
            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            //// 
            //// languageComponent1
            //// 
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";

		}
		#endregion

		private string V_SummaryTarget
		{
			get
			{
				try
				{
					return this.ViewState["V_SummaryTarget"].ToString();
				}
				catch
				{
					return SummaryTarget.Model;
				}
			}
			set
			{
				this.ViewState["V_SummaryTarget"] = value;
			}

		}

		private bool _checkRequireFields()
		{			
			PageCheckManager manager = new PageCheckManager();
			manager.Add( new DateRangeCheck(this.lblStartDateQuery,this.dateStartDateQuery.Text,this.lblEndDateQuery,this.dateEndDateQuery.Text,true));

			if( !manager.Check() )
			{
				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
				return true;
			}	
			return true;
		}

		//获取OWC显示的Schema
		#region 获取OWC显示的Schema
		private string[] getOWCSchema()
		{
			return new string[]{
								   "ModelCode", 
								   "ItemCode",
								   "MoCode",
								   "OperationCode",
								   "SegmentCode",
								   "StepSequenceCode",
								   "ResourceCode",
								   "TimePeriodCode",
								   "ShiftCode",
								   "ShiftDay",
								   "Week",
								   "Month",	
								   "YieldPercent",
								   "AllGoodYieldPercent",
								   "NotYieldPercent",
								   "AllGoodQuantity",
								   "InputQuantity",
								   "Quantity",
								   "NGTimes",
								   "AllTimes",
								   "NotYieldLocation",
								   "TotalLocation",
								   "PPM"
							   };
		}

		private string[] getOWCSchema2()
		{
			string[] rows		= TimingType.ParserAttributeTimingType( this.rblTimingType.SelectedValue );
			string[] columns	= SummaryTarget.ParserAttributeSummaryTarget2( this.rblSummaryTarget.SelectedValue );
			string[] fields		= YieldCatalog.ParserTotalFields(this.V_SummaryTarget,this.rblYieldCatalog.SelectedValue);

			ArrayList schemaList = new ArrayList();
			foreach(string row in rows)
			{
				schemaList.Add(row);
			}
			foreach(string column in columns)
			{
				schemaList.Add(column);
			}
			foreach(string field in fields)
			{
				schemaList.Add(field);
			}

			return (string[])schemaList.ToArray(typeof(string));
		}

		#endregion

        private NewReportDomainObject[] ToNewReportDomainObject(object[] dateSource)
        {
            NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dateSource.Length];
            NewReportDomainObject item;
            for (int i = 0; i < dateSource.Length; i++)
            {
                item = new NewReportDomainObject();                
                item.InputQty = Convert.ToInt32((dateSource[i] as HistoryYieldPercent).InputQuantity);
                item.ItemCode = (dateSource[i] as HistoryYieldPercent).ItemCode == null ? "" : (dateSource[i] as HistoryYieldPercent).ItemCode.ToString();
                item.NGTimes = Convert.ToInt32((dateSource[i] as HistoryYieldPercent).NGTimes);
                item.NotYieldPercent = (dateSource[i] as HistoryYieldPercent).NotYieldPercent;
                item.AllGoodQuantity = Convert.ToInt32((dateSource[i] as HistoryYieldPercent).AllGoodQuantity);
                item.AllGoodYieldPercent = (dateSource[i] as HistoryYieldPercent).AllGoodYieldPercent;
                item.PPM = (dateSource[i] as HistoryYieldPercent).PPM;
                item.TotalLocation = (dateSource[i] as HistoryYieldPercent).TotalLocation;
                item.NotYieldLocation = (dateSource[i] as HistoryYieldPercent).NotYieldLocation;
                item.YieldPercent = (dateSource[i] as HistoryYieldPercent).YieldPercent;
                item.AllTimes = (dateSource[i] as HistoryYieldPercent).AllTimes;
                item.NatureDate = (dateSource[i] as HistoryYieldPercent).NatureDate;
                item.MOCode = (dateSource[i] as HistoryYieldPercent).MoCode == null ? "" : (dateSource[i] as HistoryYieldPercent).MoCode.ToString();
                item.ModelCode = (dateSource[i] as HistoryYieldPercent).ModelCode == null ? "" : (dateSource[i] as HistoryYieldPercent).ModelCode.ToString();
                item.Month = (dateSource[i] as HistoryYieldPercent).Month == null ? "" : (dateSource[i] as HistoryYieldPercent).Month.ToString();                
                item.OPCode = (dateSource[i] as HistoryYieldPercent).OperationCode == null ? "" : (dateSource[i] as HistoryYieldPercent).OperationCode.ToString();
                item.Quantity = Convert.ToInt32((dateSource[i] as HistoryYieldPercent).Quantity);
                item.ResCode = (dateSource[i] as HistoryYieldPercent).ResourceCode == null ? "" : (dateSource[i] as HistoryYieldPercent).ResourceCode.ToString();
                item.SegCode = (dateSource[i] as HistoryYieldPercent).SegmentCode == null ? "" : (dateSource[i] as HistoryYieldPercent).SegmentCode.ToString();
                item.ShiftCode = (dateSource[i] as HistoryYieldPercent).ShiftCode == null ? "" : (dateSource[i] as HistoryYieldPercent).ShiftCode.ToString();
                item.ShiftDay = (dateSource[i] as HistoryYieldPercent).ShiftDay.ToString();
                item.SSCode = (dateSource[i] as HistoryYieldPercent).StepSequenceCode == null ? "" : (dateSource[i] as HistoryYieldPercent).StepSequenceCode.ToString();
                item.PeriodCode = (dateSource[i] as HistoryYieldPercent).TimePeriodCode == null ? "" : (dateSource[i] as HistoryYieldPercent).TimePeriodCode.ToString();
                item.Week = (dateSource[i] as HistoryYieldPercent).Week == null ? "" : (dateSource[i] as HistoryYieldPercent).Week.ToString();
                
                dateSourceForOWC[i] = item;
            }
            return dateSourceForOWC;
        }

		private void _doQuery()
		{
			if( this._checkRequireFields() )
			{				
				object[] dateSource = this._loadDataSource();

                if (dateSource == null || dateSource.Length <= 0)
                {
                    this.gridWebGrid.Visible = false;
                    this.cmdGridExport.Visible = false;
                    this.lineChart.Visible = false;
                    this.columnChart.Visible = false;
                                       
                    return;
                }
                NewReportDomainObject[] dateSourceForOWC = this.ToNewReportDomainObject(dateSource);
                //数据加载到Grid
                List<string> fixedColumnList = new List<string>();
                string[] fixedColumn = TimingType.ParserAttributeTimingType4(this.rblTimingType.SelectedValue);
                if (fixedColumn != null)
                {
                    foreach (string row in fixedColumn)
                    {
                        fixedColumnList.Add(row);
                    }
                }

                List<string> dim2PropertyList = new List<string>();
                string dim2Columns = SummaryTarget.ParserAttributeSummaryTarget3(this.rblSummaryTarget.SelectedValue);
                if (dim2Columns != null)
                {                    
                    dim2PropertyList.Add(dim2Columns);                    
                }


                List<ReportGridDim3Property> dim3PropertyList = new List<ReportGridDim3Property>();


                if (this.rblYieldCatalog.SelectedValue.ToUpper() == "yieldcatalog_ppm".ToUpper())
                {
                    dim3PropertyList.Add(new ReportGridDim3Property("TotalLocation", "0", "SUM", "SUM", false));
                    dim3PropertyList.Add(new ReportGridDim3Property("NotYieldLocation", "0", "SUM", "SUM", false));
                    dim3PropertyList.Add(new ReportGridDim3Property("PPM", "0.00%", "DIV({-1},{-2})", "DIV({-1},{-2})", false));
                }
                else if (this.rblYieldCatalog.SelectedValue.ToUpper() == "yieldcatalog_allgood".ToUpper())
                {
                    dim3PropertyList.Add(new ReportGridDim3Property("Quantity", "0", "SUM", "SUM", false));
                    dim3PropertyList.Add(new ReportGridDim3Property("AllGoodQuantity", "0", "SUM", "SUM", false));
                    dim3PropertyList.Add(new ReportGridDim3Property("AllGoodYieldPercent", "0.00%", "DIV({-1},{-2})", "DIV({-1},{-2})", false));
                }
                else if (this.rblYieldCatalog.SelectedValue.ToUpper() == "yieldcatalog_notyield".ToUpper())
                {
                    if (string.Compare(this.V_SummaryTarget, SummaryTarget.Operation, true) == 0
                        || string.Compare(this.V_SummaryTarget, SummaryTarget.Resource, true) == 0)
                    {
                        dim3PropertyList.Add(new ReportGridDim3Property("AllTimes", "0", "SUM", "SUM", false));
                        dim3PropertyList.Add(new ReportGridDim3Property("NGTimes", "0", "SUM", "SUM", false));
                        dim3PropertyList.Add(new ReportGridDim3Property("NotYieldPercent", "0.00%", "DIV({-1},{-2})", "DIV({-1},{-2})", false));

                    }
                    else
                    {
                        dim3PropertyList.Add(new ReportGridDim3Property("InputQty", "0", "SUM", "SUM", false));
                        dim3PropertyList.Add(new ReportGridDim3Property("NGTimes", "0", "SUM", "SUM", false));
                        dim3PropertyList.Add(new ReportGridDim3Property("NotYieldPercent", "0.00%", "DIV({-1},{-2})", "DIV({-1},{-2})", false));
                    }
                }      	
			
				//visible style : pivot or chart
				if( this.rblVisibleStyle.SelectedValue.ToUpper() == VisibleStyle.Pivot.ToUpper() )
				{		
                    ReportGridHelperNew reportGridHelper = new ReportGridHelperNew(this.DataProvider, this.languageComponent1, this.gridWebGrid,this.DtSource);

                    reportGridHelper.DataSource = dateSourceForOWC;
                    reportGridHelper.Dim1PropertyList = fixedColumnList;
                    reportGridHelper.Dim2PropertyList = dim2PropertyList;
                    reportGridHelper.Dim3PropertyList = dim3PropertyList;
                    reportGridHelper.HasDim3PropertyNameRowColumn = true;
                    reportGridHelper.ShowGrid();

                    this.gridWebGrid.Visible = true;
                    this.cmdGridExport.Visible = true;
                    this.lineChart.Visible = false;
                    this.columnChart.Visible = false;
				}
				if( this.rblVisibleStyle.SelectedValue.ToUpper() == VisibleStyle.Chart.ToUpper() )
				{	
					string[] fields = YieldCatalog.ParserTotalFields(this.V_SummaryTarget,this.rblYieldCatalog.SelectedValue);
					string propertyName = this.languageComponent1.GetString(dim3PropertyList[0].Name);
                   
                    List<string> rowPropertyList = new List<string>();
                    string[] listRows = TimingType.ParserAttributeTimingType4(this.rblTimingType.SelectedValue);
                    if (listRows != null)
                    {
                        foreach (string row in listRows)
                        {
                            rowPropertyList.Add(row);
                        }
                    }
                                                          
                    List<string> valuePropertyList = new List<string>();
                    foreach (ReportGridDim3Property property in dim3PropertyList)
                    {
                        if (!property.Hidden)
                        {
                            valuePropertyList.Add(property.Name);
                        }
                    }
                    List<string> dataPropertyList = valuePropertyList;

                    foreach (NewReportDomainObject obj in dateSourceForOWC)
                    {

                        if (fields != null)
                        {
                            foreach (string field in fields)
                            {
                                if (field.ToUpper() == "NotYieldPercent".ToUpper())
                                {
                                    obj.TempValue = obj.NotYieldPercent.ToString();
                                }
                                else if (field.ToUpper() == "PPM".ToUpper())
                                {
                                    obj.TempValue = obj.PPM.ToString();
                                }
                                else if (field.ToUpper() == "AllGoodYieldPercent".ToUpper())
                                {
                                    obj.TempValue = obj.AllGoodYieldPercent.ToString();
                                }

                            }
                        }

                        //时段、班次、天、周、月、年
                        if (this.rblTimingType.SelectedValue == TimingType.TimePeriod.ToString())
                        {
                            obj.PeriodCode = obj.PeriodCode.ToString();
                        }

                        if (this.rblTimingType.SelectedValue == TimingType.Shift.ToString())
                        {
                            obj.PeriodCode = obj.ShiftCode.ToString();
                        }

                        if (this.rblTimingType.SelectedValue == TimingType.Day.ToString())
                        {
                            obj.PeriodCode = obj.ShiftDay.ToString();
                        }

                        if (this.rblTimingType.SelectedValue == TimingType.Week.ToString())
                        {
                            obj.PeriodCode = obj.Week.ToString();
                        }

                        if (this.rblTimingType.SelectedValue == TimingType.Month.ToString())
                        {
                            obj.PeriodCode = obj.Month.ToString();
                        }
                        //end
                    }
				
					//chart display : histogram or line
					if( this.rblChartType.SelectedValue.ToUpper() == ChartStyle.Histogram.ToUpper() )
					{						
                        this.columnChart.Visible = true;
                        this.lineChart.Visible = false;

                        columnChart.ChartGroupByString = dim2Columns;
                        //设置首页报表的大小
                        if (ViewState["Width"] != null)
                        {
                            columnChart.Width = int.Parse(ViewState["Width"].ToString());
                        }

                        if (ViewState["Height"] != null)
                        {
                            columnChart.Height = int.Parse(ViewState["Height"].ToString());
                        }
                        //end
                        this.columnChart.ChartTextFormatString = "<DATA_VALUE:#0.##%>";
                        this.columnChart.YLabelFormatString = "<DATA_VALUE:#0.##%>";
                        this.columnChart.DataType = true;
                        this.columnChart.DataSource = dateSourceForOWC;
                        this.columnChart.DataBind();
					}
					if( this.rblChartType.SelectedValue.ToUpper() == ChartStyle.Line.ToUpper() )
					{						
                        this.columnChart.Visible = false;
                        this.lineChart.Visible = true;

                        lineChart.ChartGroupByString = dim2Columns;

                        //设置首页报表的大小
                        if (ViewState["Width"] != null)
                        {
                            lineChart.Width = int.Parse(ViewState["Width"].ToString());
                        }

                        if (ViewState["Height"] != null)
                        {
                            lineChart.Height = int.Parse(ViewState["Height"].ToString());
                        }
                        //end

                        this.lineChart.ChartTextFormatString = "<DATA_VALUE:#0.##%>";
                        this.lineChart.YLabelFormatString = "<DATA_VALUE:#0.##%>";
                        this.lineChart.DataType = true;
                        this.lineChart.DataSource = dateSourceForOWC;
                        this.lineChart.DataBind();
					}					
                    this.gridWebGrid.Visible = false;
                    this.cmdGridExport.Visible = false;
                    Response.Write("<script type='text/javascript'>scrollToBottom=true;</script>");
				}
			}

		}

		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			this.V_SummaryTarget = this.rblSummaryTarget.SelectedValue;

			this._doQuery();
		}

		protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
		{			
            this.GridExport(this.gridWebGrid);
		}

		private object[] _loadDataSource()
		{
			//产品ppm查询条件
			System.Collections.Specialized.NameValueCollection ppmNVC = new System.Collections.Specialized.NameValueCollection(3);
			ppmNVC.Add("ppmModelCode",FormatHelper.CleanString(this.txtModelQuery.Text));	//产品别
			ppmNVC.Add("ppmItemCode",FormatHelper.CleanString(this.txtItemQuery.Text));		//产品
			ppmNVC.Add("ppmMoCode",FormatHelper.CleanString(this.txtMoQuery.Text));			//工单

			object[] returnObjs = 
				new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryHistoryYieldPercent(
				FormatHelper.CleanString(this.txtCondition.Text),
				FormatHelper.CleanString(this.txtCondition.Text),
				FormatHelper.CleanString(this.txtCondition.Text),
				FormatHelper.CleanString(this.txtCondition.Text),
				FormatHelper.CleanString(this.txtCondition.Text),
				FormatHelper.CleanString(this.txtCondition.Text),
				FormatHelper.CleanString(this.txtCondition.Text),
				FormatHelper.TODateInt(this.dateStartDateQuery.Text),
				FormatHelper.TODateInt(this.dateEndDateQuery.Text),
				ppmNVC,
				this.rblTimingType.SelectedValue,
				this.V_SummaryTarget,
				this.rblYieldCatalog.SelectedValue,
				this.rblVisibleStyle.SelectedValue,
				this.rblChartType.SelectedValue);

			if(returnObjs != null)
				foreach(HistoryYieldPercent hyp in returnObjs)
				{
					if(hyp.Week != null)
					{
						hyp.Week = string.Format("{0}W{1}",hyp.ShiftDay.ToString().Substring(2,2),hyp.Week.PadLeft(2,'0'));
					}
					if(hyp.Month != null)
					{
						hyp.Month = string.Format("{0}M{1}",hyp.ShiftDay.ToString().Substring(2,2),hyp.Month.PadLeft(2,'0'));
					}
				}

			return returnObjs;
		}

		protected void rblSummaryTarget_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string summaryTarget = this.rblSummaryTarget.SelectedValue;
			if( summaryTarget.ToUpper() == SummaryTarget.Model.ToUpper() )
			{
				this.txtCondition.Type = "model";
			}
			if( summaryTarget.ToUpper() == SummaryTarget.Item.ToUpper() )
			{
				this.txtCondition.Type = "item";
			}
			if( summaryTarget.ToUpper() == SummaryTarget.Mo.ToUpper() )
			{
				this.txtCondition.Type = "mo";
			}
			if( summaryTarget.ToUpper() == SummaryTarget.Operation.ToUpper() )
			{
				this.txtCondition.Type = "operation";
			}
			if( summaryTarget.ToUpper() == SummaryTarget.Segment.ToUpper() )
			{
				this.txtCondition.Type = "segment";
			}
			if( summaryTarget.ToUpper() == SummaryTarget.StepSequence.ToUpper() )
			{
				this.txtCondition.Type = "stepsequence";
			}
			if( summaryTarget.ToUpper() == SummaryTarget.Resource.ToUpper() )
			{				
				this.txtCondition.Type = "resource";
			}
			this.txtCondition.Text = "";
			string lblText = this.languageComponent1.GetString( SummaryTarget.ParserAttributeSummaryTarget( summaryTarget ) );
			if( lblText != "" && lblText != null )
			{
				this.lblModelCodeQuery.Text = lblText;
			}

		}

		private void rblTimingType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this._doQuery();
		}

		protected void rblVisibleStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if( this.rblVisibleStyle.SelectedValue.ToUpper() == VisibleStyle.Pivot.ToUpper() )
			{
				this.rblChartType.Enabled = false;
			}
			else if( this.rblVisibleStyle.SelectedValue.ToUpper() == VisibleStyle.Chart.ToUpper() )
			{
				this.rblChartType.Enabled = true;
			}		
		}

		private void rblChartType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this._doQuery();
		}

		/// <summary>
		/// 执行客户端的函数
		/// </summary>
		/// <param name="FunctionName">函数名</param>
		/// <param name="FunctionParam">参数</param>
		/// <param name="Page">当前页面的引用</param>
		public  void ExecuteClientFunction(string FunctionName,string FunctionParam)
		{
			try
			{
				string _msg = string.Empty;
				if(FunctionParam != string.Empty)
					_msg = string.Format("<script language='JavaScript'>  {0}('{1}');</script>",FunctionName,FunctionParam);
				else
					_msg = string.Format("<script language='JavaScript'>  {0}();</script>",FunctionName);

				//将Key值设为随机数,防止脚本重复
				Page.RegisterStartupScript(Guid.NewGuid().ToString(),_msg);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
	}
}
