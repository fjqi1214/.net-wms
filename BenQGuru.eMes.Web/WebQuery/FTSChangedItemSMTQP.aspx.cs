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
using BenQGuru.eMES.Domain.TS;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FTSChangedItemSMTQP 的摘要说明。
	/// </summary>
	public partial class FTSChangedItemSMTQP : BaseQPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		protected ExcelExporter excelExporter = null;
		protected WebQueryHelper _helper = null;
		protected GridHelper _gridHelper = null;
	
		#region ViewState
		private int SourceResourceDate
		{
			get
			{
				if( this.ViewState["SourceResourceDate"] != null )
				{
					try
					{
						return System.Int32.Parse(this.ViewState["SourceResourceDate"].ToString());
					}
					catch
					{
						return 0;
					}
				}
				else
				{
					return 0;
				}
			}
			set
			{
				this.ViewState["SourceResourceDate"] = value;
			}
		}

		private int SourceResourceTime
		{
			get
			{
				if( this.ViewState["SourceResourceTime"] != null )
				{
					try
					{
						return System.Int32.Parse(this.ViewState["SourceResourceTime"].ToString());
					}
					catch
					{
						return 0;
					}
				}
				else
				{
					return 0;
				}
			}
			set
			{
				this.ViewState["SourceResourceTime"] = value;
			}
		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this._initialParamter();			

			this._gridHelper = new GridHelper(this.gridWebGrid);

			this._helper = new WebQueryHelper( null,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1 );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);				

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();
				this._helper.Query(sender);
			}
		}

		private void _initialParamter()
		{
			this.txtModelQuery.Text = this.GetRequestParam("ModelCode");
			this.txtItemQuery.Text = this.GetRequestParam("ItemCode");
			this.txtMoQuery.Text = this.GetRequestParam("MoCode");
			this.txtSnQuery.Text = this.GetRequestParam("RunningCard");
			this.txtTsStateQuery.Text = this.GetRequestParam("TSState");
			this.txtRepaireResourceQuery.Text = this.GetRequestParam("TSResourceCode");

			if( this.GetRequestParam("TSDate") != null )
			{
				string tsDate = this.GetRequestParam("TSDate");

				try
				{					
					this.SourceResourceDate = FormatHelper.TODateInt(tsDate);
				}
				catch
				{
					this.SourceResourceDate = 0;
				}

				if( this.GetRequestParam("TSTime") != null )
				{
					string tsTime = this.GetRequestParam("TSTime");

					try
					{
						this.SourceResourceTime =  FormatHelper.TOTimeInt(tsTime);
					}
					catch
					{
						this.SourceResourceTime = 0;
					}
				}
			}
		}

		private void _initialWebGrid()
		{
			this._gridHelper.AddColumn( "MItemCode1", "新物料料号",	null);
			this._gridHelper.AddColumn( "SItemCode1", "原物料料号",	null);
			this._gridHelper.AddColumn( "LotNO2", "批号",	null);
			this._gridHelper.AddColumn("DateCode",			"生产日期",null);		
			this._gridHelper.AddColumn( "Location",		"零件位置",	null);
			this._gridHelper.AddColumn( "MEMO",		"补充说明",	null);

			//多语言
			this._gridHelper.ApplyLanguage( this.languageComponent1 );
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
			return true;
		}

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{	
			if( this._checkRequireFields() )
			{
				FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
				( e as WebQueryEventArgs ).GridDataSource = 
					facadeFactory.CreateQueryTSChangedPartsFacade().QueryTSChangedPartsSMT(						
					"",
					"",
					FormatHelper.CleanString(this.txtMoQuery.Text).ToUpper(),
					FormatHelper.CleanString(this.txtSnQuery.Text).ToUpper(),
					"",
					FormatHelper.CleanString(this.txtRepaireResourceQuery.Text).ToUpper(),
					"",
					( e as WebQueryEventArgs ).StartRow,
					( e as WebQueryEventArgs ).EndRow);

				( e as WebQueryEventArgs ).RowCount = 
					facadeFactory.CreateQueryTSChangedPartsFacade().QueryTSChangedPartsSMTCount(						
					"",
					"",
					FormatHelper.CleanString(this.txtMoQuery.Text).ToUpper(),
					FormatHelper.CleanString(this.txtSnQuery.Text).ToUpper(),
					"",
					FormatHelper.CleanString(this.txtRepaireResourceQuery.Text).ToUpper(),
					"");
			}
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
			{
				TSSMTItem obj = ( e as DomainObjectToGridRowEventArgs ).DomainObject as TSSMTItem;
				( e as DomainObjectToGridRowEventArgs ).GridRow = 
					new UltraGridRow( new object[]{
													  obj.MItemCode,
													  obj.SourceItemCode ,
													  obj.LotNO,
													  obj.DateCode,													  
													  obj.Location,
													  obj.MEMO
												  }
					);
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
			{
				TSSMTItem obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as TSSMTItem;
				( e as DomainObjectToExportRowEventArgs ).ExportRow = 
					new string[]{
									obj.SourceItemCode,
									obj.SourceItemCode ,
									obj.LotNO,
									obj.DateCode,													  
									obj.Location,
									obj.MEMO
								};
			}
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			( e as ExportHeadEventArgs ).Heads = 
				new string[]{
								"MItemCode1",
								"SItemCode1",
								"LotNO2",
								"DateCode",
								"Location",
								"MEMO"
							};
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			ArrayList keys = new ArrayList();
			ArrayList values = new ArrayList();

			for(int i =0;i<this.Request.QueryString.AllKeys.Length;i++)
			{
				if( this.Request.QueryString.AllKeys.GetValue(i).ToString().StartsWith("12_") )
				{
					keys.Add( this.Request.QueryString.AllKeys.GetValue(i).ToString() );
					values.Add( this.Request.QueryString[this.Request.QueryString.AllKeys.GetValue(i).ToString()] );
				}
			}

			this.Response.Redirect(
				this.MakeRedirectUrl(
				this.GetRequestParam("BackUrl"),(string[])keys.ToArray(typeof(string)),(string[])values.ToArray(typeof(string))));
		}		
	}
}

