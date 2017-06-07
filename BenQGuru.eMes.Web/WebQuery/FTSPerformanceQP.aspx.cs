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
using BenQGuru.eMES.Web.UserControl;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FTSPerformanceQP 的摘要说明。
	/// </summary>
	public partial class FTSPerformanceQP : BaseQPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		public BenQGuru.eMES.Web.UserControl.UCNumericUpDown upDown;
		protected ExcelExporter excelExporter = null;
		protected WebQueryHelperNew _helper = null;
		//protected GridHelper gridHelper = null;	
	
		protected void Page_Load(object sender, System.EventArgs e)
		{			
			this.gridHelper = new GridHelperNew(this.gridWebGrid,DtSource);

			this._helper = new WebQueryHelperNew( this.cmdQuery,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1,DtSource );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);				
            //this._helper.GridCellClick +=new EventHandler(_helper_GridCellClick);

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

                if (this.Request.Params["Width"] != null)
                {
                    ViewState["Width"] = this.Request.Params["Width"];
                }

                if (this.Request.Params["Height"] != null)
                {
                    ViewState["Height"] = this.Request.Params["Height"];
                }
                this.UCColumnChartProcess1.Visible = false;
			}

			RadioButtonListBuilder.FormatListControlStyle( this.rblSummaryTargetQuery,50 );
		}

		private void _initialWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddColumn("TsOperator",				"维修工",null);
            this.gridHelper.AddColumn("TsOperatorHidden", "维修工", null);
			this.gridHelper.AddColumn("TsQuantity",			"维修数量",null);
			this.gridHelper.AddLinkColumn("List",	"详细信息",null);

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
            this.gridWebGrid.Columns.FromKey("TsOperatorHidden").Hidden = true;
            //this.gridWebGrid.Bands[0].Columns[3].Width = 60;
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
					( e as WebQueryEventArgsNew ).StartRow,
					( e as WebQueryEventArgsNew ).EndRow);

				( e as WebQueryEventArgsNew ).GridDataSource = dataSource;

				( e as WebQueryEventArgsNew ).RowCount = 
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
            //this.OWCChartSpace1.ClearCharts();

            if (dataSource != null)
            {
                //string seriesName = "First";
                string[] categories = new string[dataSource.Length];
                object[] values = new object[dataSource.Length];

                for (int i = 0; i < dataSource.Length; i++)
                {
                    categories[i] = (dataSource[i] as QDOTSPerformance).TsOperator;
                    values[i] = (dataSource[i] as QDOTSPerformance).TsQuantity;
                }

                //this.OWCChartSpace1.AddChart(false, seriesName, categories, values );	

                //this.OWCChartSpace1.Display = true;

                //设置首页报表的大小
                if (ViewState["Width"] != null)
                {
                    UCColumnChartProcess1.Width = int.Parse(ViewState["Width"].ToString());
                }

                if (ViewState["Height"] != null)
                {
                    UCColumnChartProcess1.Height = int.Parse(ViewState["Height"].ToString());
                }
                //end

                DataTable dataTableColumn = new DataTable();
                dataTableColumn.Columns.Add("Names", typeof(System.String));
                dataTableColumn.Columns.Add("First", typeof(System.Int32));
                for (int i = 0; i < values.Length; i++)
                {
                    dataTableColumn.Rows.Add(new object[] { categories[i].ToString(), values[i] });
                }
                this.UCColumnChartProcess1.DataType = true;
                this.UCColumnChartProcess1.YLabelFormatString = "<DATA_VALUE:0.##>";
                this.UCColumnChartProcess1.ChartTextFormatString = "<DATA_VALUE:0.##>";
                this.UCColumnChartProcess1.ColumnDataSource = dataTableColumn;
                this.UCColumnChartProcess1.DataBindTable();
                this.UCColumnChartProcess1.Visible = true;
            }
            else
            {
                this.UCColumnChartProcess1.Visible = false;
            }
		}


		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgsNew ).DomainObject != null )
			{
				QDOTSPerformance obj = ( e as DomainObjectToGridRowEventArgsNew ).DomainObject as QDOTSPerformance;
                DataRow row = DtSource.NewRow();
                row["TsOperator"] = obj.TsOperator;
                row["TsOperatorHidden"] = obj.TsOperatorHidden;
                row["TsQuantity"] = obj.TsQuantity;
                row["List"] = "";
                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgsNew ).DomainObject != null )
			{
				QDOTSPerformance obj = ( e as DomainObjectToExportRowEventArgsNew ).DomainObject as QDOTSPerformance;
				( e as DomainObjectToExportRowEventArgsNew ).ExportRow = 
					new string[]{
									obj.TsOperator.ToString(),
									obj.TsQuantity.ToString()
								};
			}
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			( e as ExportHeadEventArgsNew ).Heads = 
				new string[]{
								"TsOperator",
								"TsQuantity"
							};
		}

        protected override void Grid_ClickCell(GridRecord row, string command)
        {
            if (command == "List")
			{
				this.Response.Redirect( 
					this.MakeRedirectUrl(
					"FTSPerformanceListQP.aspx",
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
									row.Items.FindItemByKey("TsOperatorHidden").Text,
									this.dateStartDateQuery.Text,
									this.dateEndDateQuery.Text	
								})
					);
			}
		}
	}
}
