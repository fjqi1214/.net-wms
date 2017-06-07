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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting ;

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FMO2RouteSP 的摘要说明。
	/// </summary>
	public partial class FMO2RouteSP : BasePage
	{

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private GridHelper gridHelper = null;
        private BenQGuru.eMES.MOModel.MOFacade _facade ;//= new MOFacade();

		#region const
		public const string MOROUTE_ERRORCODE = "ERRORCODE";
		public const string MOROUTE_ERRORCAUSE = "ERRORCAUSE";
		public const string MOROUTE_NORMAL = "NORMAL";
		#endregion
        
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

		#region form events
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

			this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

			if (!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				InitWebGrid();
				this.InitUI();	

				this.drpRouteSourceQuery.Items.Add( MOROUTE_ERRORCODE );
				this.drpRouteSourceQuery.Items.Add( MOROUTE_ERRORCAUSE );
				this.drpRouteSourceQuery.Items.Add( MOROUTE_NORMAL );
				
			}

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

		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			this.RequestData();
		}
		protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
		{
			if(_facade ==null)_facade = new MOFacade(base.DataProvider);
			ArrayList array = this.gridHelper.GetCheckedRows();
			if( array.Count > 0 )
			{
				foreach(Infragistics.WebUI.UltraWebGrid.UltraGridRow row in array)
				{
					MO2Route mo2Route = new MO2Route();
					mo2Route.RouteCode = row.Cells[1].Text ;
					mo2Route.IsMainRoute = row.Cells[2].Text;
					mo2Route.RouteType = row.Cells[3].Text ;
					mo2Route.MOCode = this.GetMOCode() ;
					mo2Route.MaintainUser = this.GetUserCode();
					// TODO:需要重做
					mo2Route.OPBOMCode = "~";
					mo2Route.OPBOMVersion = "~" ;


					try
					{
						this._facade.DeleteMORoute( mo2Route );
					}
					catch
					{
					}
				}

				this.RequestData();
			}
		}

		protected void cmdSelect_ServerClick(object sender, System.EventArgs e)
		{
			if(_facade ==null)_facade = new MOFacade(base.DataProvider);
			ArrayList array = this.gridHelper.GetCheckedRows();
			if( array.Count > 0 )
			{
				foreach(Infragistics.WebUI.UltraWebGrid.UltraGridRow row in array)
				{
					MO2Route mo2Route = new MO2Route();
					mo2Route.RouteCode = row.Cells[1].Text ;
					mo2Route.IsMainRoute = row.Cells[2].Text;
					mo2Route.RouteType = row.Cells[3].Text ;
					mo2Route.MOCode = this.GetMOCode() ;
					mo2Route.MaintainUser = this.GetUserCode();
					// TODO:需要重做
					mo2Route.OPBOMCode = "~";
					mo2Route.OPBOMVersion = "~" ;


					try
					{
						this._facade.AddMORoute( mo2Route );
					}
					catch
					{
					}
				}

				this.RequestData();
			}
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect(this.MakeRedirectUrl("FMOMP.aspx"));
		}

		#endregion

		#region private method
		private void RequestData()
		{
			//this.gridHelper.GridBind(PageGridBunding.Page);

			this.pagerToolBar.RowCount = GetRowCount();
			this.pagerToolBar.InitPager();
		}

		private int GetRowCount()
		{
		 if(_facade ==null)_facade = new MOFacade(base.DataProvider);
			// TODO:这里要重做
			int count = 0 ;
			switch( this.drpRouteSourceQuery.SelectedValue )
			{
				case MOROUTE_ERRORCAUSE:
					count = this._facade.QueryTSErrorCauseRouteCountByMO( this.GetMOCode() , this.txtRouteCodeQuery.Text);
					break;

				case MOROUTE_ERRORCODE:
					count = this._facade.QueryTSErrorCodeRouteCountByMO( this.GetMOCode() , this.txtRouteCodeQuery.Text);
					break;
                
				case MOROUTE_NORMAL:
					count = this._facade.QueryNormalRouteCountByMO( this.GetMOCode() , this.txtRouteCodeQuery.Text);
					break;
			}

			return count ;
		}

       
		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade ==null)_facade = new MOFacade(base.DataProvider);
			object[] objs = null ;
			switch( this.drpRouteSourceQuery.SelectedValue )
			{
				case MOROUTE_ERRORCAUSE:
					objs = this._facade.QueryTSErrorCauseRouteByMO( this.GetMOCode() , this.txtRouteCodeQuery.Text);
					break;

				case MOROUTE_ERRORCODE:
					objs = this._facade.QueryTSErrorCodeRouteByMO( this.GetMOCode() , this.txtRouteCodeQuery.Text);
					break;
                
				case MOROUTE_NORMAL:
					objs = this._facade.QueryNormalRouteByMO( this.GetMOCode() , this.txtRouteCodeQuery.Text);
					break;
			}

			return objs ;

		}


		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			if(_facade ==null)_facade = new MOFacade(base.DataProvider);
			string isMainRoute ;
			if( this.drpRouteSourceQuery.SelectedValue == MOROUTE_NORMAL )
			{
				isMainRoute = "1" ;
			}
			else
			{
				isMainRoute = "0" ;
			}

			string isUsedRoute = "false";

			if( this._facade.QueryMORoutes( this.GetMOCode() , ((Route)obj).RouteCode ) != null )
			{
				isUsedRoute = "true" ;
			}

			Infragistics.WebUI.UltraWebGrid.UltraGridRow row =
				new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]
							{
								isUsedRoute,
								((Route)obj).RouteCode,
								isMainRoute,
								((Route)obj).RouteType ,
								((Route)obj).RouteDescription
							}
				);

			return row ;
		}

		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
		}

		private void InitWebGrid()
		{
			this.gridHelper.AddColumn( "RouteCode", "途程代码",	null);
			this.gridHelper.AddColumn("RouteIsMainRoute","是否主途程",null) ;
			this.gridHelper.AddColumn("RouteType","途程类型",null) ;
			this.gridHelper.AddColumn( "RouteDesc", "途程描述",	null);

			this.gridHelper.AddDefaultColumn( true, false );
			//this.gridHelper.ApplyDefaultStyle();

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private string GetMOCode()
		{
			return Request.QueryString["MOCode"] ;
		}

     
		#endregion

	}
}
