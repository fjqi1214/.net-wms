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
using BenQGuru.eMES.Domain.DataLink;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FFTQuery 的摘要说明。
	/// </summary>
	public partial class FFTQuery : BaseQPage
	{
		protected BenQGuru.eMES.Web.UserControl.eMESDate txtDateFrom;
		protected BenQGuru.eMES.Web.UserControl.eMESDate txDateTo;


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
		protected System.Web.UI.WebControls.DropDownList drpSSQuery;
		protected System.Web.UI.WebControls.TextBox txtSoftwareVersionQuery;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 初始化页面语言
			this.InitPageLanguage(this.languageComponent1, false);

			this._gridHelper = new GridHelper(this.gridWebGrid);

			this._helper = new WebQueryHelper( this.cmdQuery,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1 );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);				
			this._helper.GridCellClick +=new EventHandler(_helper_GridCellClick);

			FormatHelper.SetSNRangeValue(txtStartSnQuery,txtEndSnQuery);

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();
			}
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
			//产线  资源  产品  产品序列号 制具  测试结果  测试日期  测试时间  测试人员  测试明细数据（用户点击进入明细资料界面）
			this._gridHelper.AddColumn( "SN",			"产品序列号",null);
			this._gridHelper.AddColumn( "TestSeq",		"测试序号",null);

			this._gridHelper.AddColumn( "SSCode",		"产线代码",null);
			this._gridHelper.AddColumn( "ResCode",		"资源代码",null);
			this._gridHelper.AddColumn( "ItemCode",		"产品代码",null);
			this._gridHelper.AddColumn( "MachineTool",	"制具",null);
			this._gridHelper.AddColumn( "TestResult",	"测试结果",null);

			this._gridHelper.AddColumn( "TestMan",		"测试人员",null);
			this._gridHelper.AddColumn( "TestDate",		"测试日期",	null);
			this._gridHelper.AddColumn( "TestTime",		"测试时间",	null);

			this._gridHelper.AddLinkColumn("TestDetails","测试明细数据",null);

			//多语言
			this._gridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridWebGrid.Columns.FromKey("TestSeq").Hidden = true;
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
		
			if((sender is System.Web.UI.HtmlControls.HtmlInputButton ) && ((System.Web.UI.HtmlControls.HtmlInputControl)(((System.Web.UI.HtmlControls.HtmlInputButton)(sender)))).Name == "cmdQuery")
			{
				this.QueryEvent(sender,e);
			}
			else
			{
				ExprotEvent(sender,e);
			}
		}

		//导出按钮事件
		private void ExprotEvent(object sender, EventArgs e)
		{
			if(chbTestDetail.Checked)
			{
				FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
				( e as WebQueryEventArgs ).GridDataSource = 
					facadeFactory.CreateDataLinkFacade().QueryFTDetail(
					FormatHelper.CleanString(this.txtStepSequence.Text).ToUpper(),
					FormatHelper.CleanString(this.txtItemQuery.Text).ToUpper(),
					FormatHelper.CleanString(this.txtMoQuery.Text).ToUpper(),
					FormatHelper.CleanString(this.txtResQuery.Text).ToUpper(),
					FormatHelper.CleanString(this.txtStartSnQuery.Text).ToUpper(),
					FormatHelper.CleanString(this.txtEndSnQuery.Text).ToUpper(),
					( e as WebQueryEventArgs ).StartRow,
					( e as WebQueryEventArgs ).EndRow);
			}
			else
			{
				this.QueryEvent(sender,e);
			}
		}

		#region 查询事件

		private void QueryEvent(object sender, EventArgs e)
		{

			FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
			( e as WebQueryEventArgs ).GridDataSource = 
				facadeFactory.CreateDataLinkFacade().QueryFT(
				FormatHelper.TODateInt(this.txtDateFrom.Text),
				FormatHelper.TODateInt(this.txDateTo.Text),
				FormatHelper.CleanString(this.txtStepSequence.Text).ToUpper(),
				FormatHelper.CleanString(this.txtItemQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtMoQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtResQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtStartSnQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtEndSnQuery.Text).ToUpper(),
				( e as WebQueryEventArgs ).StartRow,
				( e as WebQueryEventArgs ).EndRow);

			( e as WebQueryEventArgs ).RowCount =
			facadeFactory.CreateDataLinkFacade().QueryFTCount(
				FormatHelper.TODateInt(this.txtDateFrom.Text),
				FormatHelper.TODateInt(this.txDateTo.Text),
				FormatHelper.CleanString(this.txtStepSequence.Text).ToUpper(),
				FormatHelper.CleanString(this.txtItemQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtMoQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtResQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtStartSnQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtEndSnQuery.Text).ToUpper());
		}

		#endregion


		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
			{
				//产线  资源  产品  产品序列号 制具  测试结果  测试日期  测试时间  测试人员  测试明细数据（用户点击进入明细资料界面）
				FT obj = ( e as DomainObjectToGridRowEventArgs ).DomainObject as FT;
				( e as DomainObjectToGridRowEventArgs ).GridRow = 
					new UltraGridRow( new object[]{
													  obj.RCard,
													  obj.TestSeq.ToString(),
													  obj.LineCode,
													  obj.Rescode,
													  obj.Itemcode,
													  obj.Machinetool,
													  obj.TestResult,
													  obj.MaintainUser,
													  FormatHelper.ToDateString(obj.MaintainDate),
													  FormatHelper.ToTimeString(obj.MaintainTime),
													  ""
												  }
					);
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{

			if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
			{
				if(chbTestDetail.Checked)
				{
					FTDetail obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as FTDetail;
					( e as DomainObjectToExportRowEventArgs ).ExportRow = 
						new string[]{
										obj.RCard,
										obj.TGroup.ToString(),
										obj.Freq.ToString(),
										obj.AC1.ToString(),
										obj.AC2.ToString(),
										obj.AC3.ToString(),
										obj.AC4.ToString(),
										obj.AC5.ToString(),

										obj.AC6.ToString(),
										obj.AC7.ToString(),
										obj.AC8.ToString(),
										obj.AC9.ToString(),
										obj.AC10.ToString(),

										obj.AC11.ToString(),
										obj.AC12.ToString(),
										obj.AC13.ToString(),
										obj.AC14.ToString(),
										obj.AC15.ToString(),

										obj.AC16.ToString(),
										obj.AC17.ToString(),
										obj.AC18.ToString(),
										obj.AC19.ToString(),
										obj.AC20.ToString()
									};
				}
				else
				{
					FT obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as FT;
					( e as DomainObjectToExportRowEventArgs ).ExportRow = 
						new string[]{
										obj.RCard,
										obj.TestSeq.ToString(),
										obj.LineCode,
										obj.Rescode,
										obj.Itemcode,
										obj.Machinetool,
										obj.TestResult,
										obj.MaintainUser,
										FormatHelper.ToDateString(obj.MaintainDate),
										FormatHelper.ToTimeString(obj.MaintainTime)
									};
				}
			}

		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			if(chbTestDetail.Checked)
			{
				( e as ExportHeadEventArgs ).Heads = 
					new string[]{
									"产品序列号",
									"测试组",
									"Freq",
									"AC1",
									"AC2",
									"AC3",
									"AC4",
									"AC5",

									"AC6",
									"AC7",
									"AC8",
									"AC9",
									"AC10",

									"AC11",
									"AC12",
									"AC13",
									"AC14",
									"AC15",

									"AC16",
									"AC17",
									"AC18",
									"AC19",
									"AC20"
								};
			}
			else
			{
				//产线  资源  产品  产品序列号 制具  测试结果  测试日期  测试时间  测试人员  
				( e as ExportHeadEventArgs ).Heads = 
					new string[]{
									"产品序列号",
									"测试序号",
									"产线",
									"资源",
									"产品",
									"制具",
									"测试结果",
									"测试人员",
									"测试日期",
									"测试时间",
				};
				
			}
		}
		private void _helper_GridCellClick(object sender, EventArgs e)
		{
			if( (e as GridCellClickEventArgs).Cell.Column.Key.ToUpper() == "TestDetails".ToUpper() )
			{
				this.Response.Redirect( 
					this.MakeRedirectUrl(
					"FFTDetailQuery.aspx",
					new string[]{
									"ItemCode",
									"Rcard",
									"TestSeq",
									"BackUrl"
								},
					new string[]{
									(e as GridCellClickEventArgs).Cell.Row.Cells.FromKey("ItemCode").Text,
									(e as GridCellClickEventArgs).Cell.Row.Cells.FromKey("SN").Text,
									(e as GridCellClickEventArgs).Cell.Row.Cells.FromKey("TestSeq").Text,
									"FFTQuery.aspx"
								})
					);
			}
		}

	
	
	}
}
