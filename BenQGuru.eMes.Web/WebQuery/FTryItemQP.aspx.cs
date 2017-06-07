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
using BenQGuru.eMES.Domain.DataCollect;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FTryItemQP 的摘要说明。
	/// </summary>
	public partial class FTryItemQP : BaseQPage
	{

		protected ExcelExporter excelExporter = null;
		protected WebQueryHelper _helper = null;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected GridHelper _gridHelper = null;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this._gridHelper = new GridHelper(this.gridWebGrid);

			this._helper = new WebQueryHelper( this.cmdQuery,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1 );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);				
			this._helper.GridCellClick +=new EventHandler(_helper_GridCellClick);

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();
			}
		}

		private void _initialWebGrid()
		{
			this._gridHelper.AddColumn("TryItemCode",				"试流物料号",null);
			this._gridHelper.AddColumn("INNO",			"集成上料号",null);
			this._gridHelper.AddColumn("RunningCard",			"产品序列号",null);
			this._gridHelper.AddColumn("MoCode",			"工单单号",null);
			this._gridHelper.AddColumn("ItemCode",			"产品代码",null);
			this._gridHelper.AddColumn("CollectionOperationCode",			"采集工位",null);
			this._gridHelper.AddColumn("CollectionStepSequenceCode",			"采集线别",null);
			this._gridHelper.AddColumn("CollectionResourceCode",				"采集资源",null);			
			this._gridHelper.AddColumn( "EmployeeNo",			"员工工号",	null);
			this._gridHelper.AddColumn( "CollectionDate",		"采集日期",	null);
			this._gridHelper.AddColumn( "CollectionTime",		"采集时间",	null);

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
			PageCheckManager manager = new PageCheckManager();
			manager.Add( new LengthCheck(this.lblTryItemQuery,this.txtTryitemQuery,System.Int32.MaxValue,true) );

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
				( e as WebQueryEventArgs ).GridDataSource = 
					facadeFactory.CreateQueryTryItemFacade().QueryTryItem(				
					FormatHelper.CleanString(this.txtTryitemQuery.Text),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
					( e as WebQueryEventArgs ).StartRow,
					( e as WebQueryEventArgs ).EndRow);

				( e as WebQueryEventArgs ).RowCount = 
					facadeFactory.CreateQueryTryItemFacade().QueryTryItemCount(				
					FormatHelper.CleanString(this.txtTryitemQuery.Text),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper());
			}
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
			{
				QDOTryItem obj = ( e as DomainObjectToGridRowEventArgs ).DomainObject as QDOTryItem;
				( e as DomainObjectToGridRowEventArgs ).GridRow = 
					new UltraGridRow( new object[]{
													  obj.TryItemCode,
													  obj.MCARD,
													  obj.RunningCard,
													  obj.MOCode,
													  obj.ItemCode,
													  obj.OPCode,
													  obj.StepSequenceCode,
													  obj.ResourceCode,
													  obj.MaintainUser,
													  FormatHelper.ToDateString(obj.MaintainDate),
													  FormatHelper.ToTimeString(obj.MaintainTime)
												  }
					);
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
			{
				QDOTryItem obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as QDOTryItem;
				( e as DomainObjectToExportRowEventArgs ).ExportRow = 
					new string[]{
									obj.TryItemCode,
									obj.MCARD,
									obj.RunningCard,
									obj.MOCode,
									obj.ItemCode,													  
									obj.OPCode,
									obj.StepSequenceCode,
									obj.ResourceCode,
									obj.MaintainUser,
									FormatHelper.ToDateString(obj.MaintainDate),
									FormatHelper.ToTimeString(obj.MaintainTime)
								};
			}
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			( e as ExportHeadEventArgs ).Heads = 
				new string[]{
								"TryItemCode",
								"MCARD",
								"RunningCard",
								"MoCode",
								"ItemCode",
								"CollectionOperationCode",
								"CollectionStepSequenceCode",
								"CollectionResourceCode",
								"EmployeeNo",
								"CollectionDate",
								"CollectionTime"
							};
		}		

		private void _helper_GridCellClick(object sender, EventArgs e)
		{
			if( (e as GridCellClickEventArgs).Cell.Column.Key.ToUpper() == "Details".ToUpper() )
			{
				this.Response.Redirect( 
					this.MakeRedirectUrl(
					"FSMTNGListQP.aspx",
					new string[]{
									"Details"
								},
					new string[]{
									(e as GridCellClickEventArgs).Cell.Row.Cells.FromKey("RunningCard").Text
								})
					);
			}
		}			
	}
}
