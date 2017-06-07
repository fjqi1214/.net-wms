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
using BenQGuru.eMES.Domain.DeviceInterface;
using BenQGuru.eMES.Domain.DataLink;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FPTQuery 的摘要说明。
	/// </summary>
	public partial class FPTQuery  : BaseQPage
	{

		protected ExcelExporter excelExporter = null;
		protected WebQueryHelper _helper = null;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected GridHelper _gridHelper = null;
		protected System.Web.UI.WebControls.Label lblStartSNQuery;
		protected System.Web.UI.WebControls.Label lblEndSNQuery;
		protected System.Web.UI.WebControls.Label lblSoftwareNameQuery;
		protected System.Web.UI.WebControls.TextBox txtSoftwareNameQuery;
		protected System.Web.UI.WebControls.Label lblSoftwareVersionQuery;
		protected System.Web.UI.WebControls.Label lblStepSequenceConditionQuery;
		protected BenQGuru.eMES.Web.SelectQuery.SelectableTextBox txtStepSequence;
		protected System.Web.UI.WebControls.Label lblSS;
		protected System.Web.UI.WebControls.DropDownList drpSSQuery;
		protected System.Web.UI.WebControls.Label lblRes;
		protected System.Web.UI.WebControls.TextBox txtResQuery;
		protected System.Web.UI.WebControls.TextBox txtSoftwareVersionQuery;
		protected BenQGuru.eMES.Web.UserControl.eMESDate txtBeginDate;
		protected BenQGuru.eMES.Web.UserControl.eMESDate txtEndDate;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this._gridHelper = new GridHelper(this.gridWebGrid);
			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();

				//Karron Qiu 设置查询日期默认为当天，当数据库中记录日期之后可以取消以下注释
				//txtBeginDate.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );
				//txtEndDate.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );
			}

			this._helper = new WebQueryHelper( this.cmdQuery,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1 );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);				

			FormatHelper.SetSNRangeValue(txtStartSnQuery,txtEndSnQuery);
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

		private void _initialWebGrid()
		{
			//产品序列号、产品代码、工单代码、产线、资源、测试标准值,测试最大值,测试最小值、测试结果、测试人员,测试日期，测试时间
			this._gridHelper.AddColumn( "SN",			"产品序列号",null);
			this._gridHelper.AddColumn( "ItemCode",		"产品代码",null);
			this._gridHelper.AddColumn( "MOCode",		"工单代码",null);
			this._gridHelper.AddColumn( "SSCode",		"产线代码",null);
			this._gridHelper.AddColumn( "ResCode",		"资源代码",null);
			
			this._gridHelper.AddColumn( "TestStandardValue",	"实际测试值",null);
			this._gridHelper.AddColumn( "TestMaxValue",	"最大标准值",null);
			this._gridHelper.AddColumn( "TestMinValue",	"最小标准值",null);
			this._gridHelper.AddColumn( "TestResult",	"测试结果",null);

			this._gridHelper.AddColumn( "TestMan",		"测试人员",null);
			this._gridHelper.AddColumn( "TestDate",		"测试日期",	null);
			this._gridHelper.AddColumn( "TestTime",		"测试时间",	null);

			//多语言
			this._gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{
			PageCheckManager manager = new PageCheckManager();
			// Added by Icyer 2006/08/16
			if (this.txtItemQuery.Text.Trim() == string.Empty)
			{
				throw new Exception("$Error_ItemCode_NotCompare");
			}
			// Added end
		
			this.QueryEvent(sender,e);
		}

		#region 查询事件

		private void QueryEvent(object sender, EventArgs e)
		{
			int BeginDate = FormatHelper.TODateInt(this.txtBeginDate.Text);
			int EndDate = FormatHelper.TODateInt(this.txtEndDate.Text);

			FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
			( e as WebQueryEventArgs ).GridDataSource = 
				facadeFactory.CreateQueryFacade3().QueryPT(
				FormatHelper.CleanString(this.txtItemQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtMoQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtStartSnQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtEndSnQuery.Text).ToUpper(),
				BeginDate,
				EndDate,
				( e as WebQueryEventArgs ).StartRow,
				( e as WebQueryEventArgs ).EndRow);

			( e as WebQueryEventArgs ).RowCount =
				facadeFactory.CreateQueryFacade3().QueryPTCount(
				FormatHelper.CleanString(this.txtItemQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtMoQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtStartSnQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtEndSnQuery.Text).ToUpper(),
				BeginDate,
				EndDate
				);
		}

		#endregion


		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
			{
				//产品序列号、产品代码、工单代码、产线、资源、测试标准值,测试最大值,测试最小值、测试结果、测试人员,测试日期，测试时间
				PreTestValue obj = ( e as DomainObjectToGridRowEventArgs ).DomainObject as PreTestValue;
				( e as DomainObjectToGridRowEventArgs ).GridRow = 
					new UltraGridRow( new object[]{
													  obj.RCard,
													  obj.ItemCode,
													  obj.MOCode,
													  obj.SSCode,
													  obj.ResCode,
													  obj.Value.ToString(),
													  obj.MaxValue.ToString(),
													  obj.MinValue.ToString(),
													  obj.TestResult,
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
				PreTestValue obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as PreTestValue;
				( e as DomainObjectToExportRowEventArgs ).ExportRow = 
					new string[]{
									obj.RCard,
									obj.ItemCode,
									obj.MOCode,
									obj.SSCode,
									obj.ResCode,
									obj.Value.ToString(),
									obj.MaxValue.ToString(),
									obj.MinValue.ToString(),
									obj.TestResult,
									obj.MaintainUser,
									FormatHelper.ToDateString(obj.MaintainDate),
									FormatHelper.ToTimeString(obj.MaintainTime)
								};
			}

		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			//产品序列号、产品代码、工单代码、产线、资源、测试标准值,测试最大值,测试最小值、测试结果、测试人员,测试日期，测试时间
			( e as ExportHeadEventArgs ).Heads = 
				new string[]{
								"产品序列号",
								"产品代码",
								"工单代码",
								"产线代码",
								"资源代码",
								"实际测试值",
								"最大标准值",
								"最小标准值",
								"测试结果",
								"测试人员",
								"测试日期",
								"测试时间",
			};
				
			
		}
	}
}
