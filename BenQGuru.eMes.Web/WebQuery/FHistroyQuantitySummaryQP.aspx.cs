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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FHistroyQuantitySummaryQP 的摘要说明。
	/// </summary>
	public partial class FHistroyQuantitySummaryQP : BaseQPage
	{
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
			
								
			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				builder1.Build();
				builder2.Build();
				builder3.Build();
				builder4.Build();

				this.dateStartDateQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
				this.dateEndDateQuery.Text = this.dateStartDateQuery.Text;

				this.OWCChartSpace1.Display = false;

				this.rblSummaryTarget.Attributes.Add("onclick","judgeSummaryTarget()" ) ;
			}
			
			RadioButtonListBuilder.FormatListControlStyle( this.rblTimingType,50 );
			RadioButtonListBuilder.FormatListControlStyle( this.rblSummaryTarget,50 );
			RadioButtonListBuilder.FormatListControlStyle( this.rblVisibleStyle,50 );
			RadioButtonListBuilder.FormatListControlStyle( this.rblChartType,50 );
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
								   "ShiftDay",
								   "ShiftCode",
								   "TimePeriodCode",
								   "Quantity",		
								   "Week",
								   "Month"
							   };
		}

		private string[] getOWCSchema2()
		{
			string[] rows		= TimingType.ParserAttributeTimingType( this.rblTimingType.SelectedValue );
			string[] columns	= SummaryTarget.ParserAttributeSummaryTarget2( this.rblSummaryTarget.SelectedValue );

			ArrayList schemaList = new ArrayList();
			foreach(string row in rows)
			{
				schemaList.Add(row);
			}
			foreach(string column in columns)
			{
				schemaList.Add(column);
			}
			schemaList.Add("Quantity");
			schemaList.Add("InputQty");

			return (string[])schemaList.ToArray(typeof(string));
		}

		private void _doQuery()
		{
			if( this._checkRequireFields() )
			{
				this.OWCPivotTable1.ClearFieldSet();

				object[] dateSource = this._loadDataSource();

				//统一使用透视表处理数据
				this.OWCPivotTable1.SetDataSource( 
					dateSource, 
					this.getOWCSchema2());


				//visible style : pivot or chart
				if( this.rblVisibleStyle.SelectedValue.ToUpper() == VisibleStyle.Pivot.ToUpper() )
				{			
					string[] rows = TimingType.ParserAttributeTimingType( this.rblTimingType.SelectedValue );
					if( rows != null )
					{
						foreach(string row in rows)
						{
							this.OWCPivotTable1.AddRowFieldSet( row,false);
						}
					}
					//categories
					string[] columns = SummaryTarget.ParserAttributeSummaryTarget2( this.rblSummaryTarget.SelectedValue );
					if( columns != null )
					{
						foreach(string column in columns)
						{
							this.OWCPivotTable1.AddColumnFieldSet(column,false);
						}
					}
					//values
					//投入数
					this.OWCPivotTable1.AddTotalField(
						this.languageComponent1.GetString( "InputQty" ),
						"InputQty",
						PivotTotalFunctionType.Sum);

					//产出数(产量)
					this.OWCPivotTable1.AddTotalField(
						this.languageComponent1.GetString( "Quantity1" ),
						"Quantity",
						PivotTotalFunctionType.Sum);
					this.OWCChartSpace1.Display = false;
					this.OWCPivotTable1.Display = true;
				}
				if( this.rblVisibleStyle.SelectedValue.ToUpper() == VisibleStyle.Chart.ToUpper() )
				{

					string[] rows = TimingType.ParserAttributeTimingType2( this.rblTimingType.SelectedValue );
					if( rows != null )
					{
						foreach(string row in rows)
						{
							this.OWCPivotTable1.AddRowFieldSet(row,true);
						}
					}

					//categories
//					string[] columns = SummaryTarget.ParserAttributeSummaryTarget2( this.rblSummaryTarget.SelectedValue );
//					if( columns != null )
//					{
//						foreach(string column in columns)
//						{
//							this.OWCPivotTable1.AddColumnFieldSet(column,false);
//						}
//					}
					this.OWCPivotTable1.AddColumnFieldSet(
						SummaryTarget.ParserAttributeSummaryTarget( this.rblSummaryTarget.SelectedValue ),
						true);

					//values
					//投入数
					this.OWCPivotTable1.AddTotalField(
						this.languageComponent1.GetString( "InputQty" ),
						"InputQty",
						PivotTotalFunctionType.Sum);

					//产出数(产量)
					this.OWCPivotTable1.AddTotalField(
						this.languageComponent1.GetString( "Quantity1" ),
						"Quantity",
						PivotTotalFunctionType.Sum);
					this.OWCChartSpace1.Display = false;
					this.OWCPivotTable1.Display = true;
					this.OWCChartSpace1.DataSource = this.OWCPivotTable1.PivotTableName;
				
					//chart display : histogram or line
					if( this.rblChartType.SelectedValue.ToUpper() == ChartStyle.Histogram.ToUpper())
					{
						this.OWCChartSpace1.ChartType = OWCChartType.ColumnClustered;
					}
					if( this.rblChartType.SelectedValue.ToUpper() == ChartStyle.Line.ToUpper() )
					{
						//this.OWCChartSpace1.ChartType = OWCChartType.Line;
						this.OWCChartSpace1.ChartType = OWCChartType.LineMarkers;
					}

					this.OWCPivotTable1.Display = false;
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
			System.Collections.Specialized.NameValueCollection iNVC = new System.Collections.Specialized.NameValueCollection(3);
			iNVC.Add("iModelCode",FormatHelper.CleanString(this.txtModelQuery.Text));	//产品别
			iNVC.Add("iItemCode",FormatHelper.CleanString(this.txtItemQuery.Text));		//产品
			iNVC.Add("iMoCode",FormatHelper.CleanString(this.txtMoQuery.Text));			//工单

			object[] returnObjs = 
				new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryHistoryQuantitySummary(
				FormatHelper.CleanString(this.txtCondition.Text),
				FormatHelper.CleanString(this.txtCondition.Text),
				FormatHelper.CleanString(this.txtCondition.Text),
				FormatHelper.CleanString(this.txtCondition.Text),
				FormatHelper.CleanString(this.txtCondition.Text),
				FormatHelper.CleanString(this.txtCondition.Text),
				FormatHelper.CleanString(this.txtCondition.Text),
				iNVC,
				FormatHelper.TODateInt(this.dateStartDateQuery.Text),
				FormatHelper.TODateInt(this.dateEndDateQuery.Text),
				this.rblTimingType.SelectedValue,
				this.V_SummaryTarget,
				this.rblVisibleStyle.SelectedValue,
				this.rblChartType.SelectedValue);

			if(returnObjs != null)
				foreach(HistroyQuantitySummary hyp in returnObjs)
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
		
	}
}
