#region system
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
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FModelRouteEP 的摘要说明。
	/// </summary>
	public partial class FModelRouteEP : BasePage
	{

		private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		//private BaseModelFacade _baseModelFacade = FacadeFactory.CreateBaseModelFacade();
		private ModelFacade _modelFacade =null;// new  FacadeFactory(base.DataProvider).CreateModelFacade();
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
	
	

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
			this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
			this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";
			// 
			// excelExporter
			// 
			this.excelExporter.LanguageComponent = null;
			this.excelExporter.Page = this;

		}
		#endregion

		#region page events
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InitHanders();
			if (!IsPostBack)
			{	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				// 初始化界面UI
				this.InitUI();
				this.Initparameters();
				this.InitWebGrid();
				this.InitButton();
				this.RequestData();
				this.InitViewPanel();
				//this.pagerSizeSelector.Readonly = true;
			}
		}

		private void Initparameters()
		{
			if(this.Request.Params["modelcode"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["modelcode"] = this.Request.Params["modelcode"];
			}
		}

		public string ModelCode
		{
			get
			{
				return (string) this.ViewState["modelcode"];
			}
		}

		private void InitViewPanel()
		{
			this.txtModelCodeQuery.Text = ModelCode;
		}

		protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect(this.MakeRedirectUrl("FModelRouteSP.aspx",new string[] {"modelcode"},new string[] {FormatHelper.CleanString(this.txtModelCodeQuery.Text.Trim())}));
		}

		protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
		{
			if(_modelFacade==null){_modelFacade = new  FacadeFactory(base.DataProvider).CreateModelFacade();}
			ArrayList array = this.gridHelper.GetCheckedRows();
			if(array.Count>0)
			{
				ArrayList model2Routes = new ArrayList( array.Count );
			
				
				foreach (UltraGridRow row in array)
				{
					model2Routes.Add((Model2Route)this.GetEditObject(row) );
				}
				this._modelFacade.DeleteModelRoute((Model2Route[])model2Routes.ToArray(typeof(Model2Route)));
			}
			this.RequestData();
		}

		protected void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( this.chbSelectAll.Checked )
			{
				this.gridHelper.CheckAllRows( CheckStatus.Checked );
			}
			else
			{
				this.gridHelper.CheckAllRows( CheckStatus.Unchecked );
			}
		}
	
	
		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if(e.Cell.Column.Key == "modelop")
			{
				Response.Redirect(this.MakeRedirectUrl("FModelRouteOperationSP.aspx",new string[]{"modelcode","routecode"},new string[] {e.Cell.Row.Cells[1].Text,e.Cell.Row.Cells[2].Text} ));
			}
		
		}
		
		protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
		{
			this.excelExporter.Export();
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect(this.MakeRedirectUrl("FModelMP.aspx"));
		}
		
		#endregion

		#region private method
		private void RequestData()
		{
			this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
			this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
			this.pagerToolBar.RowCount = GetRowCount();
			this.pagerToolBar.InitPager();
		}
		private void InitHanders()
		{
			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

			this.buttonHelper = new ButtonHelper(this);

			this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

			this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
		}

		private string[] FormatExportRecord( object obj )
		{
			return new string[]{ ((Route)obj).RouteCode.ToString(),
								   ((Route)obj).RouteDescription.ToString(),
								   ((Route)obj).MaintainUser.ToString(),
								   FormatHelper.ToDateString(((Route)obj).MaintainDate)};
		}

		private string[] GetColumnHeaderText()
		{
			return new string[] {	"RouteCode",
									"RouteDescription",
				"MaintainUser","MaintainDate"
									};
		}

		private object[] LoadDataSource()
		{
			return this.LoadDataSource( 1, int.MaxValue );
		}

		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
		}
		public void InitButton()
		{	
			this.buttonHelper.AddDeleteConfirm();
		}

		private void InitWebGrid()
		{
			this.gridHelper.AddColumn( "RouteCode", "生产途程代码",	null);
			this.gridHelper.AddColumn( "RouteDescription", "生产途程描述",	null);
			this.gridHelper.AddColumn( "EffectiveDate", "生效日期",	null);
			this.gridHelper.AddColumn( "InvalidDate", "失效日期",	null);
			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);

			this.gridWebGrid.Columns.FromKey("EffectiveDate").Hidden = true;
			this.gridWebGrid.Columns.FromKey("InvalidDate").Hidden = true;

			this.gridHelper.AddDefaultColumn( true, false );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								((Route)obj).RouteCode.ToString(),
								((Route)obj).RouteDescription.ToString(),
								FormatHelper.ToDateString(((Route)obj).EffectiveDate),
								FormatHelper.ToDateString(((Route)obj).InvalidDate),
								((Route)obj).MaintainUser.ToString(),
								FormatHelper.ToDateString(((Route)obj).MaintainDate)
			});
		}

		private object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_modelFacade==null){_modelFacade = new  FacadeFactory(base.DataProvider).CreateModelFacade();}
			object obj = this._modelFacade.GetModel2Route(row.Cells[1].Text,FormatHelper.CleanString(this.txtModelCodeQuery.Text), GlobalVariables.CurrentOrganizations.First().OrganizationID);
			
			if (obj != null)
			{
				return (Model2Route)obj;
			}

			return null;
		}
		
		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_modelFacade==null){_modelFacade = new  FacadeFactory(base.DataProvider).CreateModelFacade();}
			return this._modelFacade.GetSelectedRoutesByModelCode(
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(ModelCode)),
				inclusive, exclusive );
		}
		private int GetRowCount()
		{
			if(_modelFacade==null){_modelFacade = new  FacadeFactory(base.DataProvider).CreateModelFacade();}
			return this._modelFacade.GetSelectedRouteCountByModelCode(FormatHelper.PKCapitalFormat( FormatHelper.CleanString(ModelCode)));
		}

		
		#endregion

	
		
	}
}
