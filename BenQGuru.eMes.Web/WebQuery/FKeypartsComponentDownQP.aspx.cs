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
	/// FKeypartsComponentLoadingQP 的摘要说明。
	/// </summary>
	public partial class FKeypartsComponentDownQP : BaseQPage
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
			FormatHelper.SetSNRangeValue(txtStartKeypartsQuery,txtEndKeypartsQuery);

			if(!this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();
			}
		}

		private void _initialWebGrid()
		{
			this._gridHelper.AddColumn("MOCode",				"工单",null);
			this._gridHelper.AddColumn("ItemCode",			"产品代码",null);			
			this._gridHelper.AddColumn("SN",			"产品序列号",null);
			this._gridHelper.AddColumn("ComponentLoadingOPCode1",			"下料工位",null);
			this._gridHelper.AddColumn("ComponentLoadingStepSequenceCode1",			"下料线别",null);			
			this._gridHelper.AddColumn("ComponentLoadingResourceCode1",				"下料资源",null);
			this._gridHelper.AddColumn("Keyparts",			"关键料号",null);
			this._gridHelper.AddColumn("EmployeeNo",				"员工工号",null);
			this._gridHelper.AddColumn( "ComponentLoadingDate1",		"下料日期",	null);
			this._gridHelper.AddColumn( "ComponentLoadingTime1",		"下料时间",	null);
			this._gridHelper.AddColumn( "INNO",		"KeyPats序列号",	null);
			this._gridHelper.AddLinkColumn("KeypartsDetails","Keyparts明细",null);

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
				facadeFactory.CreateQueryComponentLoadingFacade().QueryDownKeyparts(
				FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
				FormatHelper.CleanString(this.txtStartSnQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtEndSnQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtStartKeypartsQuery.Text.ToUpper()),
				FormatHelper.CleanString(this.txtEndKeypartsQuery.Text.ToUpper()),
				( e as WebQueryEventArgs ).StartRow,
				( e as WebQueryEventArgs ).EndRow);

			( e as WebQueryEventArgs ).RowCount = 
				facadeFactory.CreateQueryComponentLoadingFacade().QueryDownKeypartsCount(
				FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
				FormatHelper.CleanString(this.txtStartSnQuery.Text.ToUpper()),
				FormatHelper.CleanString(this.txtEndSnQuery.Text.ToUpper()),
				FormatHelper.CleanString(this.txtStartKeypartsQuery.Text.ToUpper()),
				FormatHelper.CleanString(this.txtEndKeypartsQuery.Text.ToUpper()));
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
			{
				QDOKeyparts obj = ( e as DomainObjectToGridRowEventArgs ).DomainObject as QDOKeyparts;
				( e as DomainObjectToGridRowEventArgs ).GridRow = 
					new UltraGridRow( new object[]{
													  obj.MoCode,
													  obj.ItemCode,
													  obj.SN,
													  obj.OperationCode,													  
													  obj.StepSequenceCode,
													  obj.ResourceCode,
													  obj.MItemCode,
													  obj.MaintainUser,
													  FormatHelper.ToDateString(obj.MaintainDate),
													  FormatHelper.ToTimeString(obj.MaintainTime),
													  obj.INNO,
													  ""
												  }
					);
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
			{
				QDOKeyparts obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as QDOKeyparts;
				( e as DomainObjectToExportRowEventArgs ).ExportRow = 
					new string[]{
									obj.MoCode,
									obj.ItemCode,
									obj.SN,
									obj.OperationCode,													  
									obj.StepSequenceCode,
									obj.ResourceCode,
									obj.MItemCode,
									obj.MaintainUser,
									FormatHelper.ToDateString(obj.MaintainDate),
									FormatHelper.ToTimeString(obj.MaintainTime),
									obj.INNO
								};
			}
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			( e as ExportHeadEventArgs ).Heads = 
				new string[]{
								"MOCode",
								"ItemCode",
								"SN",
								"ComponentLoadingOPCode",
								"ComponentLoadingStepSequenceCode",
								"ComponentLoadingResourceCode",
								"Keyparts",
								"EmployeeNo",
								"ComponentLoadingDate",
								"ComponentLoadingTime",
								"INNO"
							};
		}		

		private void _helper_GridCellClick(object sender, EventArgs e)
		{
			if( (e as GridCellClickEventArgs).Cell.Column.Key.ToUpper() == "KeypartsDetails".ToUpper() )
			{
				this.Response.Redirect( 
					this.MakeRedirectUrl(
					"FKeypartsDetailsQP.aspx",
					new string[]{
									"INNO",
									"RETURNPAGEURL"
								},
					new string[]{
									(e as GridCellClickEventArgs).Cell.Row.Cells.FromKey("INNO").Text,
									"FKeypartsComponentDownQP.aspx"
								})
					);
			}
		}			
	}
}
