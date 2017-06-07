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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FOnWipOP 的摘要说明。
	/// </summary>
	public partial class FOnWipQP : BaseQPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		protected WebQueryHelperNew _helper = null;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected BenQGuru.eMES.Web.SelectQuery.SelectableTextBox Selectabletextbox1;

		//GridHelper gridHelper = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.gridHelper = new GridHelperNew(this.gridWebGrid,DtSource);

			this._helper = new WebQueryHelperNew( this.cmdQuery,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1,DtSource );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);
			//this._helper.GridCellClick +=new EventHandler(_helper_GridCellClick);
			this.txtItemCodeQuery.Target = this.MakeRedirectUrl("FItemSP.aspx");
			this.txtMoCodeQuery.Target = this.MakeRedirectUrl("FMOSP.aspx");

			if( !this.IsPostBack )
			{ 		
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();

				this.dateStartDateQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
				this.dateEndDateQuery.Text = this.dateStartDateQuery.Text;

                this.columnChart.Visible = false;
			}
		}

		private void _initialWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddColumn("OperationCode","工序",null);
			this.gridHelper.AddColumn("QuantityOnOperation","在制数量",null);
			this.gridHelper.AddLinkColumn("OnWipDistributing","在制分布",null);

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			//this.gridWebGrid.Bands[0].Columns.FromKey("OnWipDistributing").Width = new Unit(60);
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

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{
			if(!_checkRequireFields())return;

			this.ViewState["ItemCode"] = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text));
			this.ViewState["MoCode"] = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoCodeQuery.Text));
	
			//this.OWCChartSpace1.ClearCharts();

			FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
			object[] dataSource = facadeFactory.CreateQueryFacade3().QueryOnWipInfoOnOperation(
				this.ViewState["ItemCode"].ToString(),
				this.ViewState["MoCode"].ToString(),
				//this.dFactoryCode.SelectedValue,
				FormatHelper.TODateInt(this.dateStartDateQuery.Text),
				FormatHelper.TODateInt(this.dateEndDateQuery.Text),
				( e as WebQueryEventArgsNew ).StartRow,
				( e as WebQueryEventArgsNew ).EndRow);

			( e as WebQueryEventArgsNew ).GridDataSource = dataSource;

			( e as WebQueryEventArgsNew ).RowCount = 
				facadeFactory.CreateQueryFacade3().QueryOnWipInfoOnOperationCount(
				this.ViewState["ItemCode"].ToString(),
				this.ViewState["MoCode"].ToString() ,
				FormatHelper.TODateInt(this.dateStartDateQuery.Text),
				FormatHelper.TODateInt(this.dateEndDateQuery.Text)
				);            


            //update by Seven  2011-01-06
            if (dataSource != null)
            {
                this.columnChart.Visible = true;
                NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dataSource.Length];

                string propertyName = "Operation";
                NewReportDomainObject item;
                for (int i = 0; i < dataSource.Length; i++)
                {
                    item = new NewReportDomainObject();
                    item.EAttribute1 = propertyName;
                    item.PeriodCode = (dataSource[i] as OnWipInfoOnOperation).OperationCode;
                    item.TempValue = (dataSource[i] as OnWipInfoOnOperation).OnWipQuantityOnOperation.ToString();
                    dateSourceForOWC[i] = item;
                }

                this.columnChart.ChartGroupByString = "";
                this.columnChart.ChartTextFormatString = "<DATA_VALUE:0.##>";
                this.columnChart.YLabelFormatString = "<DATA_VALUE:0.##>";
                this.columnChart.DataType = true;
                this.columnChart.DataSource = dateSourceForOWC;
                this.columnChart.DataBind();
            }
            else
            {
                this.columnChart.Visible = false;
            }
            //end
		}

		/// <summary>
		/// 输入检查
		/// </summary>
		/// <returns></returns>
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

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgsNew ).DomainObject != null )
			{
				OnWipInfoOnOperation obj = ( e as DomainObjectToGridRowEventArgsNew ).DomainObject as OnWipInfoOnOperation;
                DataRow row = DtSource.NewRow();
                row["OperationCode"] = obj.OperationCode;
                row["QuantityOnOperation"] = obj.OnWipQuantityOnOperation;
                row["OnWipDistributing"] = "";
                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
					
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgsNew ).DomainObject != null )
			{
				OnWipInfoOnOperation obj = ( e as DomainObjectToExportRowEventArgsNew ).DomainObject as OnWipInfoOnOperation;
				( e as DomainObjectToExportRowEventArgsNew ).ExportRow = 
					new string[]{
									this.ViewState["ItemCode"].ToString(),
									obj.OperationCode,
									obj.OnWipQuantityOnOperation.ToString()
								};
			}
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			( e as ExportHeadEventArgsNew ).Heads = 
				new string[]{
								"ItemCode",
								"OperationCode",
								"OnWipQuantityOnOperation"
							};
		}

        protected override void Grid_ClickCell(GridRecord row, string command)
        {

            if (command == "OnWipDistributing")
			{
				this.Response.Redirect( 
					
					this.MakeRedirectUrl(
					"FOnWipResourceQP.aspx",
					new string[]{"ItemCode","MoCode","OperationCode","STARTDATE","ENDDATE"},
					new string[]{
									this.ViewState["ItemCode"].ToString(),
									this.ViewState["MoCode"].ToString(),
									row.Items.FindItemByKey("OperationCode").Text,
									this.dateStartDateQuery.Text,
									this.dateEndDateQuery.Text
								})
					);
			}
		}

		private void dFactoryCode_Load(object sender, EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				WarehouseFacade whFacade = new WarehouseFacade(base.DataProvider);
				object[]  factorys  = whFacade.GetAllFactory();
				if( factorys != null )
				{
					foreach( BenQGuru.eMES.Domain.Warehouse.Factory _factory in factorys)
					{
						this.dFactoryCode.Items.Add( _factory.FactoryCode ) ;
					}
					new DropDownListBuilder( this.dFactoryCode ).AddAllItem( this.languageComponent1 ) ;
				}
			}
		}
	}
}


