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

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FTSPerformanceDetails 的摘要说明。
	/// </summary>
	public class FTSPerformanceDetails : BaseQPage
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblModelQuery;
		protected System.Web.UI.WebControls.TextBox txtModelQuery;
		protected System.Web.UI.WebControls.Label lblItemQuery;
		protected System.Web.UI.WebControls.TextBox txtItemQuery;
		protected System.Web.UI.WebControls.Label lblMoQuery;
		protected System.Web.UI.WebControls.TextBox txtMoQuery;
		protected System.Web.UI.WebControls.Label lblSnQuery;
		protected System.Web.UI.WebControls.TextBox txtSnQuery;
		protected System.Web.UI.WebControls.Label lblTSStateQuery;
		protected System.Web.UI.WebControls.TextBox txtTsStateQuery;
		protected System.Web.UI.WebControls.Label lblRepaireOperationQuery;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid gridWebGrid;
		protected BenQGuru.eMES.Web.Helper.PagerSizeSelector pagerSizeSelector;
		protected BenQGuru.eMES.Web.Helper.PagerToolBar pagerToolBar;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdGridExport;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdReturn;

		protected ExcelExporter excelExporter = null;
		protected WebQueryHelper _helper = null;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.TextBox txtRepaireResourceQuery;
		protected GridHelper _gridHelper = null;

		#region ViewState
		#endregion
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			this._gridHelper = new GridHelper(this.gridWebGrid);

			this._initialWebGrid();

			this._helper = new WebQueryHelper( null,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1 );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);				

			if( !this.IsPostBack )
			{
				this._helper.Query(sender);
			}
		}

		private void _initialWebGrid()
		{
			this._gridHelper.AddColumn("ErrorCodeGroup",	"不良代码组",null);
			this._gridHelper.AddColumn("ErrorCode",			"不良代码",null);
			this._gridHelper.AddColumn("ErrorCause",			"不良原因",null);
			this._gridHelper.AddColumn("Duty",			"责任别",null);		
			this._gridHelper.AddColumn("Solution",			"解决方案",null);
			this._gridHelper.AddColumn( "ErrorLocation",		"不良位置",	null);
			this._gridHelper.AddColumn( "ErrorParts",		"不良元件-〉更换元件",	null);
			this._gridHelper.AddColumn( "Memo",		"补充说明",	null);
			this._gridHelper.AddColumn( "TsOperator",		"维修工",	null);
			this._gridHelper.AddColumn( "MaintainDate",		"维修日期",	null);
			this._gridHelper.AddColumn( "MaintainTime",		"维修时间",	null);
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private bool _checkRequireFields()
		{			
			PageCheckManager manager = new PageCheckManager();
			manager.Add( new LengthCheck(this.lblModelQuery,this.txtModelQuery,System.Int32.MaxValue,true) );
			manager.Add( new LengthCheck(this.lblItemQuery,this.txtItemQuery,System.Int32.MaxValue,true) );
			manager.Add( new LengthCheck(this.lblMoQuery,this.txtMoQuery,System.Int32.MaxValue,true) );
			manager.Add( new LengthCheck(this.lblSnQuery,this.txtSnQuery,System.Int32.MaxValue,true) );
			manager.Add( new LengthCheck(this.lblTSStateQuery,this.txtTsStateQuery,System.Int32.MaxValue,true) );
			manager.Add( new LengthCheck(this.lblRepaireOperationQuery,this.txtRepaireOperationQuery,System.Int32.MaxValue,true) );

			if( !manager.Check() )
			{
				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
				return true;
			}	
			return true;
		}

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{	
			if( this._checkRequireFields() )
			{
				( e as WebQueryEventArgs ).GridDataSource = 
					FacadeFactory.CreateQueryTSRecordFacade().QueryTSRecord(
					FormatHelper.CleanString(this.txtConditionModel.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
					FormatHelper.CleanString(this.txtStartSnQuery.Text).ToUpper(),
					FormatHelper.CleanString(this.txtEndSnQuery.Text).ToUpper(),
					FormatHelper.TODateInt(this.dateStartDateQuery.Text),
					FormatHelper.TODateInt(this.dateEndDateQuery.Text),
					TSStateListBuilder.GetCheckedTSStateList(this.chkTSStateList),
					"",
					( e as WebQueryEventArgs ).StartRow,
					( e as WebQueryEventArgs ).EndRow);

				( e as WebQueryEventArgs ).RowCount = 
					FacadeFactory.CreateQueryTSRecordFacade().QueryTSRecordCount(
					FormatHelper.CleanString(this.txtConditionModel.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
					FormatHelper.CleanString(this.txtStartSnQuery.Text).ToUpper(),
					FormatHelper.CleanString(this.txtEndSnQuery.Text).ToUpper(),
					FormatHelper.TODateInt(this.dateStartDateQuery.Text),
					FormatHelper.TODateInt(this.dateEndDateQuery.Text),
					TSStateListBuilder.GetCheckedTSStateList(this.chkTSStateList),
					"");
			}
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
			{
				QDOTSDetails1 obj = ( e as DomainObjectToGridRowEventArgs ).DomainObject as QDOTSDetails1;
				( e as DomainObjectToGridRowEventArgs ).GridRow = 
					new UltraGridRow( new object[]{
													  obj.ErrorCodeGroup,
													  obj.ErrorCode,
													  obj.ErrorCause,
													  obj.Duty,
													  obj.Solution,
													  obj.ErrorLocation,
													  obj.ErrorParts + "-->" + obj.ChangedParts,
													  obj.Memo,
													  obj.TsOperator,
													  FormatHelper.ToDateString(obj.TsDate),
													  FormatHelper.ToTimeString(obj.TsTime)
												  }
					);
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
			{
				QDOTSDetails1 obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as QDOTSDetails1;
				( e as DomainObjectToExportRowEventArgs ).ExportRow = 
					new string[]{
									obj.ErrorCodeGroup,
									obj.ErrorCode,
									obj.ErrorCause,
									obj.Duty,
									obj.Solution,
									obj.ErrorLocation,
									obj.ErrorParts + "-->" + obj.ChangedParts,
									obj.Memo,
									obj.TsOperator,
									FormatHelper.ToDateString(obj.TsDate),
									FormatHelper.ToTimeString(obj.TsTime)
								};
			}
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			( e as ExportHeadEventArgs ).Heads = 
				new string[]{
								"ErrorCodeGroup",
								"ErrorCode",
								"ErrorCause",
								"Duty",
								"Solution",
								"ErrorLocation",
								"ErrorParts",
								"Memo",
								"TsOperator",
								"MaintainDate",
								"MaintainTime"
							};
		}

		private void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect("FTSPerformanceListQP.aspx");
		}		
	}
}
