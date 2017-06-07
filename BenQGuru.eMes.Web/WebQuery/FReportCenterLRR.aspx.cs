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
using BenQGuru.eMES.Web.UserControl;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FReportCenterLRR 的摘要说明。
	/// </summary>
	public partial class FReportCenterLRR : BaseRQPage
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton Submit2;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected ExcelExporter excelExporter = null;
		protected WebQueryHelper _helper = null;
		protected GridHelperForRPT _gridHelper = null;	
	
		private string segmentCode = string.Empty;
		private string dateGroupType = string.Empty;
		private string lrrValue = string.Empty;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			InitialRequestParam();
			this._gridHelper = new GridHelperForRPT(this.gridWebGrid);

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

				this._helper.Query(this);
				// 设置不良数量栏的合并
				string strErrCode = string.Empty;
				int iRowCount = 0;
				int iRowIdx = 0;
				int iIdxErrCode = this.gridWebGrid.Columns.FromKey("ErrorCode").Index;
				int iIdxQty = this.gridWebGrid.Columns.FromKey("TotalQty").Index;
				for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
				{
					if (this.gridWebGrid.Rows[i].Cells[iIdxErrCode].Text != strErrCode)
					{
						if (iRowCount > 1)
						{
							this.gridWebGrid.Rows[iRowIdx].Cells[iIdxQty].RowSpan = iRowCount;
						}
						iRowCount = 1;
						iRowIdx = i;
						strErrCode = this.gridWebGrid.Rows[i].Cells[iIdxErrCode].Text;
					}
					else
					{
						iRowCount++;
					}
				}
				if (iRowCount > 1)
				{
					this.gridWebGrid.Rows[iRowIdx].Cells[iIdxQty].RowSpan = iRowCount;
				}
			}

		}
		private void InitialRequestParam()
		{
			segmentCode = this.GetRequestParam("segmentcode").ToUpper();
			dateGroupType = this.GetRequestParam("dategroup").ToUpper();
			lrrValue = this.GetRequestParam("lrr").ToUpper();

			txtSegmentCode.Text = segmentCode;
			if (dateGroupType.ToUpper() == "WEEK")
			{
				int iWeek = DateTime.Today.DayOfYear / 7 + 1;
				if (DateTime.Today.DayOfYear % 7 == 0)
					iWeek--;
				txtDateGroup.Text = DateTime.Today.Year.ToString() + "-" + iWeek.ToString();
			}
			else if (dateGroupType.ToUpper() == "MONTH")
			{
				DateTime dt1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
				txtDateGroup.Text = DateTime.Today.ToString("yyyy/MM");
			}
			else
			{
				txtDateGroup.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Today));
			}
			if (lrrValue != string.Empty)
				txtLRRValue.Text = Convert.ToDecimal(lrrValue).ToString("0.##%");
			else
				txtLRRValue.Text = string.Empty;
		}

		private void _initialWebGrid()
		{
			this.gridWebGrid.Columns.Clear();

			this._gridHelper.GridHelper.AddColumn("ErrorCode", "不良代码", null);
			this._gridHelper.GridHelper.AddColumn("TotalQty", "数量", null);
			this._gridHelper.GridHelper.AddColumn("ItemCode", "产品代码", null);						
			this._gridHelper.GridHelper.AddColumn("Qty", "数量", null);
			
			this.gridWebGrid.Columns.FromKey("ErrorCode").MergeCells = true;

			//多语言
			this._gridHelper.GridHelper.ApplyLanguage( this.languageComponent1 );
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
			this._initialWebGrid();
			if( this._checkRequireFields() )
			{
				int iDateFrom = 0;
				int iDateTo = 0;
				if (dateGroupType.ToUpper() == "WEEK")
				{
					DateTime dt1 = DateTime.Today.AddDays(Convert.ToInt32(DateTime.Today.DayOfWeek) * -1 + 1);
					iDateFrom = FormatHelper.TODateInt(dt1);
					iDateTo = FormatHelper.TODateInt(dt1.AddDays(6));
				}
				else if (dateGroupType.ToUpper() == "MONTH")
				{
					DateTime dt1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
					iDateFrom = FormatHelper.TODateInt(dt1);
					iDateTo = FormatHelper.TODateInt(dt1.AddMonths(1).AddDays(-1));
				}
				else
				{
					iDateFrom = iDateTo = FormatHelper.TODateInt(DateTime.Today);
				}
				FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
				object[] dataSource = 
					facadeFactory.CreateQueryFacade2().QueryOQCErrorCode(
						segmentCode,
						iDateFrom, iDateTo);

				( e as WebQueryEventArgs ).GridDataSource = dataSource;

				( e as WebQueryEventArgs ).RowCount = 
					facadeFactory.CreateQueryFacade2().QueryOQCErrorCodeCount(
					segmentCode,
					iDateFrom, iDateTo);

				this._processOWC( dataSource );
			}
		}


		private void _processOWC(object[] dataSource)
		{
			this.OWCChartSpace1.ClearCharts();

			if( dataSource != null )
			{
				string[] categories = new string[ dataSource.Length ];
				object[] ColumnClusteredValues = new object[ dataSource.Length ];	//柱状图values
				object[] ParetoValues = new object[ dataSource.Length ];		//柏拉图values

				int iTotal = 0;
				for(int i = 0;i<dataSource.Length;i++)
				{
					OQCErrorCode oqcErr = (OQCErrorCode)dataSource[i];
					iTotal += Convert.ToInt32(oqcErr.ErrorCodeCardQty);
				}
				for(int i = 0;i<dataSource.Length;i++)
				{
					OQCErrorCode oqcErr = (OQCErrorCode)dataSource[i];
					categories[i] = oqcErr.ErrorCodeDesc;
					
					ColumnClusteredValues[i] = oqcErr.ErrorCodeCardQty;
					ParetoValues[i] = this.getParetoValue(dataSource, iTotal, i);
				}

				this.OWCChartSpace1.ChartCombinationType = OWCChartCombinationType.OWCCombinationPareto;		//设置多图组合绘图方式为Pareto 柏拉图
				this.OWCChartSpace1.AddChart("数量", categories, ColumnClusteredValues );						//默认添加柱状图
				this.OWCChartSpace1.AddChart("百分比", categories, ParetoValues ,OWCChartType.LineMarkers);		//柏拉图
				
				int majorUnit = GetMahorUnit(iTotal);			//设置左Y轴最小单元刻度,避免小于5的时候出现小数单元.
				if(majorUnit > 0)
				{
					this.OWCChartSpace1.ChartLeftMajorUnit = majorUnit;
				}
				this.OWCChartSpace1.ChartLeftMaximum = iTotal;								//设置左Y轴最大刻度
				this.OWCChartSpace1.Display = true;
			}
		}

		//获取最小单元刻度
		private int GetMahorUnit(int quantity)
		{
			if(quantity < 10 ) 
				return 1;

			return 0;
		}

		private decimal getParetoValue(object[] dataSource, int total, int count)
		{
			//柏拉图的value 是累计值,根据传入的累计数,统计累计值
			decimal returnValue = 0;

			for(int i=0;i<=count;i++)
			{
				returnValue += (dataSource[i] as OQCErrorCode).ErrorCodeCardQty;
			}
			return returnValue / total;
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
			{
				OQCErrorCode obj = ( e as DomainObjectToGridRowEventArgs ).DomainObject as OQCErrorCode;

				ArrayList objList = new ArrayList();
				objList.Add(obj.ErrorCodeDesc);
				objList.Add(obj.ErrorCodeCardQty);
				objList.Add(obj.ItemCode);
				objList.Add(obj.ItemCardQty);

				( e as DomainObjectToGridRowEventArgs ).GridRow = 
					new UltraGridRow( objList.ToArray() );
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
			{
				OQCErrorCode obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as OQCErrorCode;

				ArrayList objList = new ArrayList();
				objList.Add(obj.ErrorCodeDesc);
				objList.Add(obj.ErrorCodeCardQty.ToString());
				objList.Add(obj.ItemCode);
				objList.Add(obj.ItemCardQty.ToString());

				( e as DomainObjectToExportRowEventArgs ).ExportRow = 
									(string[])objList.ToArray(typeof(string));
			}
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			ArrayList objList = new ArrayList();
			objList.Add( "ErrorCode" );
			objList.Add( "TotalQty" );
			objList.Add( "ItemCode" );
			objList.Add( "Qty" );

			( e as ExportHeadEventArgs ).Heads = (string[])objList.ToArray(typeof(string))
							;
		}	
	
		private void _helper_GridCellClick(object sender, EventArgs e)
		{			
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect("ReportCenter.aspx");
		}
	}
}
