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
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FTSLocECodeQP 的摘要说明。
	/// </summary>
	public partial class FTSLocECodeQP2 : BaseRQPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		public BenQGuru.eMES.Web.UserControl.eMESDate dateStartDateQuery;
		public BenQGuru.eMES.Web.UserControl.eMESDate dateEndDateQuery;

		protected WebQueryHelper _helper = null;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected BenQGuru.eMES.Web.SelectQuery.SelectableTextBox Selectabletextbox1;

		GridHelperForRPT _gridHelper = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.OWCPivotTable1.LanguageComponent = this.languageComponent1;
			
			this.txtItemCodeQuery.Target = this.MakeRedirectUrl("FItemSP.aspx");
			this.txtMoCodeQuery.Target = this.MakeRedirectUrl("FMOSP.aspx");

			if( !this.IsPostBack )
			{ 		
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.dateStartDateQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
				this.dateEndDateQuery.Text = this.dateStartDateQuery.Text;

				RadioButtonListBuilder builder3 = new RadioButtonListBuilder(
					new VisibleStyle(),this.rblVisibleStyle,this.languageComponent1);			
				builder3.Build();
				OWCChartSpace1.Display = false;
			}
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

		protected void cmdQuery_ServerClick(object sender, EventArgs e)
		{
			if (_checkRequireFields() == false)
				return;
			_helper_LoadGridDataSource(sender, e);
		}

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{
			if(!_checkRequireFields())return;

			this.ViewState["ItemCode"] = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text));
			this.ViewState["MoCode"] = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoCodeQuery.Text));
	
			this.OWCChartSpace1.ClearCharts();

			FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
			object[] dataSource = facadeFactory.CreateQueryTSInfoFacade().QueryTSLocECode(
				this.ViewState["ItemCode"].ToString(),
				this.ViewState["MoCode"].ToString(),
				FormatHelper.TODateInt(this.dateStartDateQuery.Text),
				FormatHelper.TODateInt(this.dateEndDateQuery.Text),
				1,
				int.MaxValue);

			//chart
			this.OWCPivotTable1.ClearFieldSet();
			string[] schema = new string[]{"ErrorLocation", "ErrorCauseDesc", "Quantity"};
			this.OWCPivotTable1.SetDataSource( 
				dataSource, schema);
			string[] rows = new string[]{"ErrorLocation"};
			foreach(string row in rows)
			{
				this.OWCPivotTable1.AddRowFieldSet(row,false);
			}
			string[] columns = new string[]{"ErrorCauseDesc"};
			foreach(string column in columns)
			{
				this.OWCPivotTable1.AddColumnFieldSet(column,false);
			}
			string field = "Quantity";
			this.OWCPivotTable1.AddTotalField(
				this.languageComponent1.GetString(field),
				field,
				PivotTotalFunctionType.Sum);
			if( this.rblVisibleStyle.SelectedValue.ToUpper() == VisibleStyle.Pivot.ToUpper() )
			{
				this.OWCPivotTable1.Display =  true ;
				this.OWCChartSpace1.Display = false;
			}
			if( this.rblVisibleStyle.SelectedValue.ToUpper() == VisibleStyle.Chart.ToUpper() )
			{
				this.OWCChartSpace1.DataSource = this.OWCPivotTable1.PivotTableName;					
				
				this.OWCChartSpace1.ChartType = OWCChartType.ColumnStacked;
				this.OWCPivotTable1.Display =  false ;
				this.OWCChartSpace1.Display = true;
			}
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


