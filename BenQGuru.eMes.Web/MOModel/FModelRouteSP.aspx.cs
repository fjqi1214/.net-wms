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
	/// FModelRouteSP 的摘要说明。
	/// </summary>
	public partial class FModelRouteSP : BasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		private ModelFacade _modelFacade ;//= FacadeFactory.CreateModelFacade();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InitHanders();
			if (!IsPostBack)
			{	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				// 初始化界面UI
				this.InitUI();
				InitParameters();
				this.InitWebGrid();
			}
		}


		private void InitParameters()
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
				return (string)this.ViewState["modelcode"];
			}
		}


	

		private void InitHanders()
		{
			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

			this.buttonHelper = new ButtonHelper(this);

			this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

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

		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
		}

		private object[] LoadDataSource()
		{
			return this.LoadDataSource( 1, int.MaxValue );
		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			return this._modelFacade.GetUnSelectedRoutesByModelCode(
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(ModelCode)), FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtRouteCodeQuery.Text)),
				inclusive, exclusive );
		}
		private int GetRowCount()
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			return this._modelFacade.GetUnSelectedRouteCountsByModelCode(FormatHelper.PKCapitalFormat( FormatHelper.CleanString(ModelCode)),FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtRouteCodeQuery.Text)));
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

		private void RequestData()
		{
			this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
			this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
			this.pagerToolBar.RowCount = GetRowCount();
			this.pagerToolBar.InitPager();
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
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";

		}
		#endregion

		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			this.RequestData();
		}

		protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			ArrayList array = this.gridHelper.GetCheckedRows();
			Model2Route[] model2Routes = new Model2Route[array.Count];
			
			for(int i=0;i<array.Count;i++)
			{
				model2Routes[i] = (Model2Route) this.GetEditObject((UltraGridRow)array[i]);
			}

			this._modelFacade.AddModelRoute(model2Routes);

			this.RequestData();
		}


		private object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			Model2Route model2Route = this._modelFacade.CreateNewModel2Route();
			model2Route.ModelCode = ModelCode;
			model2Route.RouteCode = row.Cells[1].Text;
			model2Route.MaintainUser = this.GetUserCode();
            model2Route.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
			return model2Route;
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect(this.MakeRedirectUrl("FModelRouteEP.aspx",new string[]{"modelcode"},new string[] {ModelCode}));
		}
	}
}
