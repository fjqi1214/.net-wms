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

using Infragistics.WebUI.UltraWebGrid ;

using BenQGuru.eMES.Common ;
using BenQGuru.eMES.Web.Helper ;
using BenQGuru.eMES.Web.UserControl ;

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FSingleMOSP 的摘要说明。
	/// </summary>
	public partial class FDefaultItemRouteSP : BasePage
	{
		protected GridHelper gridHelper = null ;

		// 分隔符
		protected const string DATA_SPLITER = "," ;
		protected bool writerOutted = false ;

		protected string ItemCode = null;
		protected System.Web.UI.WebControls.CheckBox chb;
//		protected System.Web.UI.WebControls.Label lblItemCodeQuery;
//		protected System.Web.UI.WebControls.TextBox txtItemCodeQuery;
		protected System.Web.UI.WebControls.Label lblMOCodeQuery;
		protected System.Web.UI.WebControls.TextBox txtMOCodeQuery;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		private BenQGuru.eMES.BaseSetting.BaseModelFacade facade ;
//= FacadeFactory.CreateSPFacade() ;
		BenQGuru.eMES.MOModel.MOFacade moFac ;


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

		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.gridHelper = new GridHelper(this.gridItem2Route);

			//Laws Lu,2006/09/26 获取产品
			if(Request.QueryString["ItemCode"] != null)
			{
				ItemCode = Request.QueryString["ItemCode"].Trim();
				txtItemCodeQuery.Text = ItemCode;
			}
			moFac = new FacadeFactory(base.DataProvider).CreateMOFacade() ;
			Domain.MOModel.DefaultItem2Route dItem2Route = null;

			object objOld = moFac.GetDefaultItem2Route(ItemCode);

			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate( this.GetGridRow ) ;
			this.gridHelper.GetRowCountHandle = new GetRowCountDelegate( this.GetRowCount ) ;
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource) ;

			if(! this.IsPostBack )
			{
				this.InitWebGrid() ;

				this.InitPageLanguage(this.languageComponent1, false);

				this.cmdQuery_ServerClick( null, null);
			}

			if(objOld != null )
			{
				dItem2Route = objOld as Domain.MOModel.DefaultItem2Route;
			}
			if(dItem2Route != null)
			{
				lblDefaultRoute.Text = languageComponent1.GetString("$Current_Default_Route ") + dItem2Route.RouteCode;
			}

			if(!this.Page.IsStartupScriptRegistered("SelectableTextBox_Startup_js"))
			{
				string scriptString = string.Format("<script>var STB_Virtual_Path = \"{0}\";</script><script src='{0}SelectQuery/selectableTextBox.js'></script>",this.VirtualHostRoot ) ;
                
				this.Page.RegisterStartupScript("SelectableTextBox_Startup_js", scriptString);
			}

		}

		#endregion

		#region WebGrid
		protected virtual void InitWebGrid()
		{
			this.gridHelper.AddColumn( "RouteCode", "途程代码",	null);
			this.gridHelper.AddColumn( "DESC", "描述",	null);
			this.gridHelper.AddDefaultColumn(true,false) ;
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			//this.gridItem2Route.DisplayLayout.ClientSideEvents.CellClickHandler = "RowSelect";
		}

		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{ 
								"",
								((BenQGuru.eMES.Domain.MOModel.Item2Route)obj).RouteCode ,
								((BenQGuru.eMES.Domain.MOModel.Item2Route)obj).EAttribute1 
							}

				);
		}

		protected object[] LoadSelectedDataSource(int inclusive, int exclusive)
		{
			return new object[]{};
		}

		protected object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateBaseModelFacade() ;}

			return this.facade.QueryItem2Route( 
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(ItemCode) ),
				inclusive, exclusive ) ;
		}


		protected int GetRowCount()
		{
			if(facade==null){facade = (new FacadeFactory(base.DataProvider)).CreateBaseModelFacade() ;}

			return this.facade.GetItem2RouteCount( 
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(ItemCode))) ;
		}
        
		#endregion
		

		protected void cmdSave_ServerClick(object sender, System.EventArgs e)
		{
			BenQGuru.eMES.MOModel.MOFacade fac =  new FacadeFactory(base.DataProvider).CreateMOFacade();
			
			Domain.MOModel.DefaultItem2Route dItem2Code = new Domain.MOModel.DefaultItem2Route();
			dItem2Code.ItemCode = ItemCode;

			
			foreach(Infragistics.WebUI.UltraWebGrid.UltraGridRow row in gridItem2Route.Rows)
			{
				if(row.Selected == true)
				{
					dItem2Code.RouteCode = row.Cells[1].Text.Trim().ToUpper();
					dItem2Code.EAttribute1 = row.Cells[2].Text.Trim().ToUpper();
					break;
				}
			}
			if(dItem2Code.RouteCode != null && dItem2Code.RouteCode != String.Empty )
			{
				object objOld = fac.GetDefaultItem2Route(ItemCode);
				Domain.MOModel.DefaultItem2Route old  = null;
				if(objOld != null)
				{
					old = objOld as  Domain.MOModel.DefaultItem2Route;
				}
				DataProvider.BeginTransaction();
				try
				{
					if(old != null)
					{
						fac.DeleteDefaultItem2Route(old);
					}
					dItem2Code.MDate = FormatHelper.TODateInt(DateTime.Now);
					dItem2Code.MTime = FormatHelper.TOTimeInt(DateTime.Now);
					//dItem2Code.m

					fac.AddDefaultItem2Route(dItem2Code);

					DataProvider.CommitTransaction();

					Response.Redirect(this.MakeRedirectUrl("FDefaultItemRouteSP.aspx",new string[]{"ItemCOde"},new string[]{ItemCode}));
				}
				catch(Exception ex)
				{
					Log.Error(ex.Message);
					DataProvider.RollbackTransaction();
				}
				finally
				{
					((Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				}
			}
		}

		private void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			LoadDataSource(PageGridBunding.Page,int.MaxValue);
			this.gridHelper.GridBind(PageGridBunding.Page,int.MaxValue);
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect("FItemRouteMP.aspx");
		}

		
	}
}
