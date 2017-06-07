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

using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Web.UserControl;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FTSPerformanceQP 的摘要说明。
	/// </summary>
	public partial class FTSPerformanceQP2 : BaseRQPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		public BenQGuru.eMES.Web.UserControl.eMESDate dateStartDateQuery;
		public BenQGuru.eMES.Web.UserControl.eMESDate dateEndDateQuery;
		public BenQGuru.eMES.Web.UserControl.UCNumericUpDown upDown;
		protected ExcelExporter excelExporter = null;
		protected WebQueryHelper _helper = null;
		protected GridHelperForRPT _gridHelper = null;	
	
		protected void Page_Load(object sender, System.EventArgs e)
		{			
			this._gridHelper = new GridHelperForRPT(this.gridWebGrid);

			this._helper = new WebQueryHelper( this.cmdQuery,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1 );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);				
			this._helper.GridCellClick +=new EventHandler(_helper_GridCellClick);

			RadioButtonListBuilder builder = new RadioButtonListBuilder(
				new TSPerformanceSummaryTarget(),this.rblSummaryTargetQuery,this.languageComponent1);

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();

				builder.Build();
				this.upDown.Value = 3;

				this.dateStartDateQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
				this.dateEndDateQuery.Text = this.dateStartDateQuery.Text;
			}

			RadioButtonListBuilder.FormatListControlStyle( this.rblSummaryTargetQuery,50 );
		}

		private void _initialWebGrid()
		{
			this._gridHelper.GridHelper.AddColumn("TsOperator",				"维修工",null);
			this._gridHelper.GridHelper.AddColumn("TsQuantity",			"维修数量",null);
			this._gridHelper.GridHelper.AddLinkColumn("List",	"详细信息",null);

			//多语言
			this._gridHelper.GridHelper.ApplyLanguage( this.languageComponent1 );
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
			manager.Add(new DateRangeCheck(this.lblRepairStartDateQuery,this.dateStartDateQuery.Text,this.lblEDateQuery,this.dateEndDateQuery.Text,true));

			if( !manager.Check() )
			{
				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
				return false;
			}	
			return true;
		}


		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{	
			if( this._checkRequireFields() )
			{
				FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
				object[] dataSource = 
					facadeFactory.CreateQueryTSPerformanceFacade().QueryTSPerformance(
					FormatHelper.CleanString(this.txtConditionModel.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionResource.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionOperator.Text).ToUpper(),
					this.rblSummaryTargetQuery.SelectedValue,
					this.upDown.Value,
					FormatHelper.TODateInt(this.dateStartDateQuery.Text),
					FormatHelper.TODateInt(this.dateEndDateQuery.Text),
					( e as WebQueryEventArgs ).StartRow,
					( e as WebQueryEventArgs ).EndRow);

				( e as WebQueryEventArgs ).GridDataSource = dataSource;

				( e as WebQueryEventArgs ).RowCount = 
					facadeFactory.CreateQueryTSPerformanceFacade().QueryTSPerformanceCount(
					FormatHelper.CleanString(this.txtConditionModel.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionResource.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionOperator.Text).ToUpper(),
					this.rblSummaryTargetQuery.SelectedValue,
					FormatHelper.TODateInt(this.dateStartDateQuery.Text),
					FormatHelper.TODateInt(this.dateEndDateQuery.Text),this.upDown.Value);

				this._processOWC( dataSource );
			}
		}

		private void _processOWC(object[] dataSource)
		{
			this.OWCChartSpace1.ClearCharts();

			if( dataSource != null )
			{
				string seriesName = "First";
				string[] categories = new string[ dataSource.Length ];
				object[] values = new object[ dataSource.Length ];

				for(int i = 0;i<dataSource.Length;i++)
				{
					categories[i] = (dataSource[i] as QDOTSPerformance).TsOperator;
					values[i] = (dataSource[i] as QDOTSPerformance).TsQuantity;
				}

				this.OWCChartSpace1.AddChart(seriesName, categories, values );	
			
				this.OWCChartSpace1.Display = true;
			}
		}


		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
			{
				QDOTSPerformance obj = ( e as DomainObjectToGridRowEventArgs ).DomainObject as QDOTSPerformance;
				( e as DomainObjectToGridRowEventArgs ).GridRow = 
					new UltraGridRow( new object[]{
													  obj.TsOperator,
													  obj.TsQuantity,
													  ""
												  }
					);
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
			{
				QDOTSPerformance obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as QDOTSPerformance;
				( e as DomainObjectToExportRowEventArgs ).ExportRow = 
					new string[]{
									obj.TsOperator.ToString(),
									obj.TsQuantity.ToString()
								};
			}
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			( e as ExportHeadEventArgs ).Heads = 
				new string[]{
								"TsOperator",
								"TsQuantity"
							};
		}		

		private void _helper_GridCellClick(object sender, EventArgs e)
		{			
			if( (e as GridCellClickEventArgs).Cell.Column.Key.ToUpper() == "List".ToUpper() )
			{
				this.Response.Redirect( 
					this.MakeRedirectUrl(
					"FTSPerformanceListQP2.aspx",
					new string[]{
									"12_ModelCode",
									"12_ItemCode",
									"12_MoCode",
									"12_TSResourceCode",
									"12_TSOperator",									
									"12_StartDate",
									"12_EndDate"
								},
					new string[]{
									FormatHelper.CleanString(this.txtConditionModel.Text),	
									FormatHelper.CleanString(this.txtConditionItem.Text),	
									FormatHelper.CleanString(this.txtConditionMo.Text),	
									FormatHelper.CleanString(this.txtConditionResource.Text),									
									(e as GridCellClickEventArgs).Cell.Row.Cells.FromKey("TsOperator").Text,
									this.dateStartDateQuery.Text,
									this.dateEndDateQuery.Text	
								})
					);
			}
		}
	}
}
