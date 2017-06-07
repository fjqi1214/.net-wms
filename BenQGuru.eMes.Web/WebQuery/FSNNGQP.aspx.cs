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
	/// FSNNGQP 的摘要说明。
	/// </summary>
	public partial class FSNNGQP : BaseQPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		protected ExcelExporter excelExporter = null;
		protected WebQueryHelper _helper = null;
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

			FormatHelper.SetSNRangeValue(txtStartSnQuery,txtEndSnQuery);

			if(!this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();
				this.txtFrmDateFrom.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );
				this.txtFrmDateTo.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );
			}
		}

		private void _initialWebGrid()
		{
			this._gridHelper.AddColumn("MOCode",				"工单",null);
			this._gridHelper.AddColumn("ItemCode",			"产品代码",null);
			this._gridHelper.AddColumn("RunningCard",			"产品序列号",null);
			this._gridHelper.AddColumn("CollectionOperationCode",			"采集工位",null);
			this._gridHelper.AddColumn("CollectionStepSequenceCode",			"采集线别",null);
			this._gridHelper.AddColumn("CollectionResourceCode",				"采集资源",null);
			this._gridHelper.AddColumn( "EmployeeNo",			"员工工号",	null);
			this._gridHelper.AddColumn( "CollectionDate",		"采集日期",	null);
			this._gridHelper.AddColumn( "CollectionTime",		"采集时间",	null);	
			this._gridHelper.AddColumn("TSStatus",				"当前状态",null);
			this._gridHelper.AddLinkColumn("NGDetails","不良明细",null);

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
			manager.Add( new LengthCheck(this.lblMOIDQuery,this.txtConditionMo.TextBox,System.Int32.MaxValue,true) );

			if( !manager.Check() )
			{
				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
				return false;
			}	
			return true;
		}

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{	
			FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
			( e as WebQueryEventArgs ).GridDataSource = 
				facadeFactory.CreateQuerySNNGFacade().QuerySNNG(
				FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionStepSequence.Text).ToUpper(),
				FormatHelper.CleanString(this.txtStartSnQuery.Text),
				FormatHelper.CleanString(this.txtEndSnQuery.Text),
				FormatHelper.CleanString(this.txtResource.Text).ToUpper(),
				FormatHelper.CleanString(this.txtFrmDateFrom.Text).ToUpper(),
				FormatHelper.CleanString(this.txtFrmDateTo.Text).ToUpper(),
				( e as WebQueryEventArgs ).StartRow,
				( e as WebQueryEventArgs ).EndRow);

			( e as WebQueryEventArgs ).RowCount = 
				facadeFactory.CreateQuerySNNGFacade().QuerySNNGCount(
				FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionStepSequence.Text).ToUpper(),
				FormatHelper.CleanString(this.txtStartSnQuery.Text),
				FormatHelper.CleanString(this.txtEndSnQuery.Text),
				FormatHelper.CleanString(this.txtResource.Text).ToUpper(),
				FormatHelper.CleanString(this.txtFrmDateFrom.Text).ToUpper(),
				FormatHelper.CleanString(this.txtFrmDateTo.Text).ToUpper());
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
			{
				BenQGuru.eMES.Domain.TS.TS obj = ( e as DomainObjectToGridRowEventArgs ).DomainObject as BenQGuru.eMES.Domain.TS.TS;
				( e as DomainObjectToGridRowEventArgs ).GridRow = 
					new UltraGridRow( new object[]{
													  obj.MOCode,
													  obj.ItemCode,
													  obj.RunningCard,
													  obj.FromOPCode,													  
													  obj.FromStepSequenceCode,
													  obj.FromResourceCode,
													  obj.MaintainUser,
													  FormatHelper.ToDateString(obj.MaintainDate),
													  FormatHelper.ToTimeString(obj.MaintainTime),
													  this.languageComponent1.GetString(obj.TSStatus),
													  ""
												  }
					);
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
			{
				BenQGuru.eMES.Domain.TS.TS obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as BenQGuru.eMES.Domain.TS.TS;
				( e as DomainObjectToExportRowEventArgs ).ExportRow = 
					new string[]{
									obj.MOCode,
									obj.ItemCode,
									obj.RunningCard,
									obj.FromOPCode,													  
									obj.FromStepSequenceCode,
									obj.FromResourceCode,
									obj.MaintainUser,
									FormatHelper.ToDateString(obj.MaintainDate),
									FormatHelper.ToTimeString(obj.MaintainTime),
									this.languageComponent1.GetString(obj.TSStatus)
				};
			}
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			( e as ExportHeadEventArgs ).Heads = 
				new string[]{
								"MOCode",
								"ItemCode",
								"RunningCard",
								"CollectionOperationCode",
								"CollectionStepSequenceCode",
								"CollectionResourceCode",
								"EmployeeNo",
								"CollectionDate",
								"CollectionTime",
								"TSStatus"
							};
		}		

		private void _helper_GridCellClick(object sender, EventArgs e)
		{
			if( (e as GridCellClickEventArgs).Cell.Column.Key.ToUpper() == "NGDetails".ToUpper() )
			{
				this.Response.Redirect( 
					this.MakeRedirectUrl(
					"FSNNGListQP.aspx",
					new string[]{
									"RunningCard"
								},
					new string[]{
									(e as GridCellClickEventArgs).Cell.Row.Cells.FromKey("RunningCard").Text
								})
					);
			}
		}			
	}
}
