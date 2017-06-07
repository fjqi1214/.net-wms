using System;
using System.Collections;
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

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FHistroyYieldPercentSummaryQP 的摘要说明。
	/// </summary>
	public partial class FHistroyYieldPercentSummaryQP2 : BaseRQPage
	{
		public BenQGuru.eMES.Web.UserControl.eMESDate dateStartDateQuery;
		public BenQGuru.eMES.Web.UserControl.eMESDate dateEndDateQuery;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.OWCPivotTable1.LanguageComponent = this.languageComponent1;

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
			
								
			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				builder1.Build();
				builder2.Build();
				builder3.Build();
				builder4.Build();
				builder5.Build();
				/*
				// Added by Icyer 2007/01/17	仅显示不良率
				for (int i = rblYieldCatalog.Items.Count - 1; i >= 0; i--)
				{
					if (rblYieldCatalog.Items[i].Value != YieldCatalog.NotYield)
					{
						rblYieldCatalog.Items.RemoveAt(i);
					}
				}
				// Added end
				*/

				this.dateStartDateQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
				this.dateEndDateQuery.Text = this.dateStartDateQuery.Text;

				this.OWCChartSpace1.Display = false;

				this.rblSummaryTarget.Attributes.Add("onclick","judgePPM()" ) ;
				this.rblYieldCatalog.Attributes.Add("onclick","judgePPM()" ) ;
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
			this.components = new System.ComponentModel.Container();
			this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";

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

		private void _doQuery()
		{
			if( this._checkRequireFields() )
			{
				this.OWCPivotTable1.ClearFieldSet();

				object[] dateSource = this._loadDataSource();
				
				//统一使用透视表处理数据
				this.OWCPivotTable1.SetDataSource( 
					dateSource, this.getOWCSchema2());
			
				//visible style : pivot or chart
				if( this.rblVisibleStyle.SelectedValue.ToUpper() == VisibleStyle.Pivot.ToUpper() )
				{		
					//categories
					string[] rows = TimingType.ParserAttributeTimingType( this.rblTimingType.SelectedValue );
					if( rows != null )
					{
						foreach(string row in rows)
						{
							this.OWCPivotTable1.AddRowFieldSet(row,false);
						}
					}

//					this.OWCPivotTable1.AddColumnFieldSet(
//						SummaryTarget.ParserAttributeSummaryTarget( this.V_SummaryTarget ),
//						true);
					string[] columns = SummaryTarget.ParserAttributeSummaryTarget2( this.rblSummaryTarget.SelectedValue ,this.rblYieldCatalog.SelectedValue);
					if( columns != null )
					{
						foreach(string column in columns)
						{
							this.OWCPivotTable1.AddColumnFieldSet(column,false);
						}
					}

					//values
					string[] fields = YieldCatalog.ParserTotalFields(this.V_SummaryTarget,this.rblYieldCatalog.SelectedValue);
					if( fields != null )
					{
						foreach(string field in fields)
						{
							if( field.ToUpper() == "YieldPercent".ToUpper() )
							{
								this.OWCPivotTable1.AddTotalField(
									this.languageComponent1.GetString(field),
									field,
									PivotTotalFunctionType.Average,NumberFormat.Percent);

							}
							else if( field.ToUpper() == "PPM".ToUpper() )
							{
								this.OWCPivotTable1.AddCalculatedTotalField(
									"PPM",
									"PPM",
									"round( ["+this.languageComponent1.GetString("NotYieldNum")+"]/["+this.languageComponent1.GetString("TotalLocation")+"]*1000000 , 0 )",
									PivotTotalFunctionType.Calculated);
							}
							else if( field.ToUpper() == "AllGoodYieldPercent".ToUpper() )
							{
								this.OWCPivotTable1.AddCalculatedTotalField(
									this.languageComponent1.GetString(field),
									this.languageComponent1.GetString(field),
									"["+this.languageComponent1.GetString("AllGoodQuantity")+"]/["+this.languageComponent1.GetString("Quantity")+"]",
									PivotTotalFunctionType.Calculated,
									NumberFormat.Percent);
							}
							else if( field.ToUpper() == "NotYieldPercent".ToUpper() )
							{
								if( string.Compare( this.V_SummaryTarget,SummaryTarget.Operation,true)==0 
									|| string.Compare( this.V_SummaryTarget,SummaryTarget.Resource,true)==0 )
								{
									this.OWCPivotTable1.AddCalculatedTotalField(
										this.languageComponent1.GetString(field),
										this.languageComponent1.GetString(field),
										"["+this.languageComponent1.GetString("NGTimes")+"]/["+this.languageComponent1.GetString("AllTimes")+"]",
										PivotTotalFunctionType.Calculated,
										NumberFormat.Percent);
								}
								else
								{
									this.OWCPivotTable1.AddCalculatedTotalField(
										this.languageComponent1.GetString(field),
										this.languageComponent1.GetString(field),
										"["+this.languageComponent1.GetString("NGTimes")+"]/["+this.languageComponent1.GetString("InputQty")+"]",
										PivotTotalFunctionType.Calculated,
										NumberFormat.Percent);
								}
							}
							else
							{
								this.OWCPivotTable1.AddTotalField(
									this.languageComponent1.GetString(field),
									field,
									PivotTotalFunctionType.Sum);
							}						
						}
					}				

					this.OWCPivotTable1.Display =  true ;
					this.OWCChartSpace1.Display = false;
				}
				if( this.rblVisibleStyle.SelectedValue.ToUpper() == VisibleStyle.Chart.ToUpper() )
				{
					//categories
					string[] rows = TimingType.ParserAttributeTimingType2( this.rblTimingType.SelectedValue );
					if( rows != null )
					{
						foreach(string row in rows)
						{
							this.OWCPivotTable1.AddRowFieldSet(row,true);
						}
					}

					this.OWCPivotTable1.AddColumnFieldSet(
						SummaryTarget.ParserAttributeSummaryTarget( this.V_SummaryTarget ),
						true);

					//values
					this.OWCPivotTable1.RemoveAllTotalField();

					string[] fields = YieldCatalog.ParserTotalFields(this.V_SummaryTarget,this.rblYieldCatalog.SelectedValue);
					if( fields != null )
					{
						foreach(string field in fields)
						{
							if( field.ToUpper() == "YieldPercent".ToUpper() )
							{
								this.OWCPivotTable1.AddTotalField(
									this.languageComponent1.GetString(field),
									field,
									PivotTotalFunctionType.Average,NumberFormat.Percent);

							}
							else if( field.ToUpper() == "PPM".ToUpper() )
							{
								this.OWCPivotTable1.AddCalculatedTotalField(
									"PPM",
									"PPM",
									"round( ["+this.languageComponent1.GetString("NotYieldNum")+"]/["+this.languageComponent1.GetString("TotalLocation")+"]*1000000 , 0 )",
									PivotTotalFunctionType.Calculated);
							}
							else if( field.ToUpper() == "AllGoodYieldPercent".ToUpper() )
							{
								this.OWCPivotTable1.AddCalculatedTotalField(
									this.languageComponent1.GetString(field),
									this.languageComponent1.GetString(field),
									"["+this.languageComponent1.GetString("AllGoodQuantity")+"]/["+this.languageComponent1.GetString("Quantity")+"]",
									PivotTotalFunctionType.Calculated,
									NumberFormat.Percent);
							}
							else if( field.ToUpper() == "NotYieldPercent".ToUpper() )
							{
								if( string.Compare( this.V_SummaryTarget,SummaryTarget.Operation,true)==0 
									|| string.Compare( this.V_SummaryTarget,SummaryTarget.Resource,true)==0 )
								{
									this.OWCPivotTable1.AddCalculatedTotalField(
										this.languageComponent1.GetString(field),
										this.languageComponent1.GetString(field),
										"["+this.languageComponent1.GetString("NGTimes")+"]/["+this.languageComponent1.GetString("AllTimes")+"]",
										PivotTotalFunctionType.Calculated,
										NumberFormat.Percent);
								}
								else
								{
									this.OWCPivotTable1.AddCalculatedTotalField(
										this.languageComponent1.GetString(field),
										this.languageComponent1.GetString(field),
										"["+this.languageComponent1.GetString("NGTimes")+"]/["+this.languageComponent1.GetString("InputQty")+"]",
										PivotTotalFunctionType.Calculated,
										NumberFormat.Percent);
								}
							}
							else
							{
								/*	Removed by Icyer 2007/01/17		在后面添加
								this.OWCPivotTable1.AddTotalField(
									this.languageComponent1.GetString(field),
									field,
									PivotTotalFunctionType.Sum);
								*/
							}						
						}
						// Added by Icyer 2007/01/17	将统计栏位提后
						foreach(string field in fields)
						{
							if( field.ToUpper() == "YieldPercent".ToUpper() ||
								field.ToUpper() == "PPM".ToUpper() ||
								field.ToUpper() == "AllGoodYieldPercent".ToUpper() ||
								field.ToUpper() == "NotYieldPercent".ToUpper())
							{ }
							else
							{
								this.OWCPivotTable1.AddTotalField(
									this.languageComponent1.GetString(field),
									field,
									PivotTotalFunctionType.Sum);
							}
						}
						// Added end
					}

					this.OWCChartSpace1.DataSource = this.OWCPivotTable1.PivotTableName;					
				
					//chart display : histogram or line
					if( this.rblChartType.SelectedValue.ToUpper() == ChartStyle.Histogram.ToUpper() )
					{
						this.OWCChartSpace1.ChartType = OWCChartType.ColumnClustered;
					}
					if( this.rblChartType.SelectedValue.ToUpper() == ChartStyle.Line.ToUpper() )
					{
						//this.OWCChartSpace1.ChartType = OWCChartType.Line;
						this.OWCChartSpace1.ChartType = OWCChartType.LineMarkers;
					}
					this.OWCPivotTable1.Display =  false ;
					this.OWCChartSpace1.Display = true;
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
			this.OWCPivotTable1.ExportExcel( false );
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
