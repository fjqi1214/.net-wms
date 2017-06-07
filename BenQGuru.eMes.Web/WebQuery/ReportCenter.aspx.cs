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
using BenQGuru.eMES.Common.Config;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// ReportCenter 的摘要说明。
	/// </summary>
	public partial class ReportCenter : BaseRQPage
	{
		protected System.Timers.Timer timerRefresh;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		protected GridHelperForRPT _gridQuantityHelper = null;
		protected GridHelperForRPT _gridYieldHelper = null;
		protected GridHelperForRPT _gridLRRHelper = null;
		protected System.Web.UI.WebControls.Label lblTSInfoChart;

		protected int today = FormatHelper.TODateInt(System.DateTime.Now);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this._gridQuantityHelper = new GridHelperForRPT(this.gridQuantity);
			this._gridYieldHelper = new GridHelperForRPT(this.gridYield);
			this._gridLRRHelper = new GridHelperForRPT(this.gridLRR);

			this.gridQuantity.DisplayLayout.CellClickActionDefault = CellClickAction.NotSet;
			this.gridYield.DisplayLayout.CellClickActionDefault = CellClickAction.NotSet;
			this.gridLRR.DisplayLayout.CellClickActionDefault = CellClickAction.NotSet;
			
			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				// 初始化页面显示
				//InitUIFromViewConfig();

				this._doQuery();

				this.RefreshController1.Interval = ConfigSection.Current.DomainSetting.Interval;
			}
			
			if( this.V_StartRefresh )
			{
				this._doQuery();
			}
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			
			InitUIFromViewConfig();
			
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
			this.timerRefresh = new System.Timers.Timer();
			((System.ComponentModel.ISupportInitialize)(this.timerRefresh)).BeginInit();
			this.gridQuantity.Click += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.gridQuantity_Click);
			this.gridYield.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridYield_ClickCellButton);
			this.gridYield.Click += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.gridYield_Click);
			this.gridLRR.Click += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.gridLRR_Click);
			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";
			// 
			// timerRefresh
			// 
			this.timerRefresh.Elapsed += new System.Timers.ElapsedEventHandler(this.timerRefresh_Elapsed);
			((System.ComponentModel.ISupportInitialize)(this.timerRefresh)).EndInit();

		}
		#endregion
		
		private void InitUIFromViewConfig()
		{
			BenQGuru.eMES.WebQuery.ReportViewConfigFacade rptFacade = new ReportViewConfigFacade(this.DataProvider);
			object[] objs = rptFacade.GetReportCenterViewByUser(this.GetUserCode());
			if (objs != null)
			{
				this.trQuantityTitle.Visible = false;
				this.trquantityData.Visible = false;
				this.trYieldTitle.Visible = false;
				this.trYieldData.Visible = false;
				this.trOQCTitle.Visible = false;
				this.trOQCData.Visible = false;
				this.tblMain.Rows.Remove(this.trQuantityTitle);
				this.tblMain.Rows.Remove(this.trquantityData);
				this.tblMain.Rows.Remove(this.trYieldTitle);
				this.tblMain.Rows.Remove(this.trYieldData);
				this.tblMain.Rows.Remove(this.trOQCTitle);
				this.tblMain.Rows.Remove(this.trOQCData);
				this.tblMain.Rows.Remove(this.trBottom);
				this.tblMain.Rows.Clear();
				for (int i = 0; i < objs.Length; i++)
				{
					ReportCenterView view = (ReportCenterView)objs[i];
					if (view.ReportCode == ReportCenterViewCode.Quantity)
					{
						this.trQuantityTitle.Visible = true;
						this.trquantityData.Visible = true;
						this.tblMain.Rows.Add(this.trQuantityTitle);
						this.tblMain.Rows.Add(this.trquantityData);
						if (view.Height > 0)
						{
							this.trquantityData.Height = Convert.ToInt32(view.Height).ToString();
						}
					}
					if (view.ReportCode == ReportCenterViewCode.YieldPercent)
					{
						this.trYieldTitle.Visible = true;
						this.trYieldData.Visible = true;
						this.tblMain.Rows.Add(this.trYieldTitle);
						this.tblMain.Rows.Add(this.trYieldData);
						if (view.Height > 0)
						{
							this.trYieldData.Height = Convert.ToInt32(view.Height).ToString();
						}
					}
					if (view.ReportCode == ReportCenterViewCode.OQC)
					{
						this.trOQCTitle.Visible = true;
						this.trOQCData.Visible = true;
						this.tblMain.Rows.Add(this.trOQCTitle);
						this.tblMain.Rows.Add(this.trOQCData);
						if (view.Height > 0)
						{
							this.trOQCData.Height = Convert.ToInt32(view.Height).ToString();
						}
					}
					if (view.ReportCode == ReportCenterViewCode.TSInfo)
					{
						this.trTSInfoChartTitle.Visible = true;
						this.trTSInfoChartData.Visible = true;
						this.tblMain.Rows.Add(this.trTSInfoChartTitle);
						this.tblMain.Rows.Add(this.trTSInfoChartData);
						this.OWCChartSpaceTSInfo.Height = Unit.Pixel(Convert.ToInt32(view.Height));
						this.OWCChartSpaceTSInfo.Width = Unit.Pixel(Convert.ToInt32(view.Width));
						this.trTSInfoChartData.Height = Convert.ToInt32(view.Height).ToString();
						this.OWCChartSpaceTSInfo.Style.Add("visibility", "hidden");
						this.OWCChartSpaceTSInfo.Style.Add("display", "none");
					}
				}
				this.tblMain.Rows.Add(this.trBottom);
			}
		}
		private void _doQuery()
		{
			this.V_StartRefresh = this.chbRefreshAuto.Checked;

			this._initialGridQuantity();
			this._initialGridYield();
			this._initialGridLRR();

			if (this.trquantityData.Visible == true)
			{
				this._processDataDourceToGridQuantity( this._loadDataSourceQuantity() );
			}
			if (this.trYieldData.Visible == true)
			{
				this._processDataDourceToGridYield( this._loadDataSourceYieldDay(),this._loadDataSourceYieldWeek(),this._loadDataSourceYieldMonth() );
			}
			if (this.trOQCData.Visible == true)
			{
				this._processDataDourceToGridLRR( this._loadDataSourceLRR() );
			}
			if (this.trTSInfoChartData.Visible == true)
			{
				this._initialChartTSInfo();
			}
		}
		
		public bool V_StartRefresh
		{
			get
			{
				if( this.Session["V_StartRefresh"] != null )
				{
					try
					{
						return System.Boolean.Parse( this.Session["V_StartRefresh"].ToString() );
					}
					catch
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			set
			{
				this.Session["V_StartRefresh"] = value.ToString();
				if( value )
				{
					this.RefreshController1.Start();
				}
				else
				{
					this.RefreshController1.Stop();
				}
			}
		}

		private void timerRefresh_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
		}

		private void _initialGridQuantity()
		{
			this.gridQuantity.Columns.Clear();

			this._gridQuantityHelper.GridHelper.AddColumn("SegmentCode","工段",null);
			this._gridQuantityHelper.GridHelper.AddColumn("DayQuantity","本日产量",null);
			this._gridQuantityHelper.GridHelper.AddColumn("WeekQuantity","本周累计",null);
			this._gridQuantityHelper.GridHelper.AddColumn("MonthQuantity","本月累计",null);

			//多语言
			this._gridQuantityHelper.GridHelper.ApplyLanguage( this.languageComponent1 );

			string selected = "";
			GridItemStyle blueBack = new GridItemStyle(true);
			blueBack.BackColor = Color.SkyBlue;
			if( this.gridQuantity.Columns.FromKey( selected ) != null )
			{
				//this.gridQuantity.Columns.FromKey(selected).HeaderStyle = blueBack;
			}

//			this.gridQuantity.Columns[0].CellStyle.Font.Bold = true;
			this.gridQuantity.Columns[1].CellStyle.Font.Underline = true;
			this.gridQuantity.Columns[1].CellStyle.ForeColor = Color.Blue;
			this.gridQuantity.Columns[2].CellStyle.Font.Underline = true;
			this.gridQuantity.Columns[2].CellStyle.ForeColor = Color.Blue;
			this.gridQuantity.Columns[3].CellStyle.Font.Underline = true;
			this.gridQuantity.Columns[3].CellStyle.ForeColor = Color.Blue;
		}
		
		private void _initialGridYield()
		{
			this.gridYield.Columns.Clear();

			this._gridYieldHelper.GridHelper.AddColumn("OPCode","工序",null);
			this._gridYieldHelper.GridHelper.AddColumn("DayPercent1","本日良率",null);
			this._gridYieldHelper.GridHelper.AddColumn("WeekPercent","本周良率",null);
			this._gridYieldHelper.GridHelper.AddColumn("MonthPercent","本月良率",null);
			this._gridYieldHelper.GridHelper.AddLinkColumn("DayYield","本日不良分布",null);
			this._gridYieldHelper.GridHelper.AddLinkColumn("WeekYield","本周不良分布",null);
			this._gridYieldHelper.GridHelper.AddLinkColumn("MonthYield","本月不良分布",null);

			//多语言
			this._gridYieldHelper.GridHelper.ApplyLanguage( this.languageComponent1 );

			string selected = "";
			GridItemStyle blueBack = new GridItemStyle(true);
			blueBack.BackColor = Color.SkyBlue;
			if( this.gridYield.Columns.FromKey( selected ) != null )
			{
				//this.gridYield.Columns.FromKey(selected).HeaderStyle = blueBack;
			}
			
//			this.gridYield.Columns[0].CellStyle.Font.Bold = true;
			this.gridYield.Columns[1].CellStyle.Font.Underline = true;
			this.gridYield.Columns[1].CellStyle.ForeColor = Color.Blue;
			this.gridYield.Columns[2].CellStyle.Font.Underline = true;
			this.gridYield.Columns[2].CellStyle.ForeColor = Color.Blue;
			this.gridYield.Columns[3].CellStyle.Font.Underline = true;
			this.gridYield.Columns[3].CellStyle.ForeColor = Color.Blue;

			this.gridYield.Columns.FromKey("DayYield").Width = Unit.Pixel(90);
			this.gridYield.Columns.FromKey("WeekYield").Width = Unit.Pixel(90);
			this.gridYield.Columns.FromKey("MonthYield").Width = Unit.Pixel(90);
		}
		
		private void _initialGridLRR()
		{
			this.gridLRR.Columns.Clear();

			this._gridLRRHelper.GridHelper.AddColumn("SegmentCode","工段",null);
			this._gridLRRHelper.GridHelper.AddColumn("DayLRR","本日LRR",null);
			this._gridLRRHelper.GridHelper.AddColumn("WeekLRR","本周LRR",null);
			this._gridLRRHelper.GridHelper.AddColumn("MonthLRR","本月LRR",null);

			//多语言
			this._gridLRRHelper.GridHelper.ApplyLanguage( this.languageComponent1 );

			string selected = "";
			GridItemStyle blueBack = new GridItemStyle(true);
			blueBack.BackColor = Color.SkyBlue;
			if( this.gridLRR.Columns.FromKey( selected ) != null )
			{
				//this.gridLRR.Columns.FromKey(selected).HeaderStyle = blueBack;
			}
			
//			this.gridLRR.Columns[0].CellStyle.Font.Bold = true;
			this.gridLRR.Columns[1].CellStyle.Font.Underline = true;
			this.gridLRR.Columns[1].CellStyle.ForeColor = Color.Blue;
			this.gridLRR.Columns[2].CellStyle.Font.Underline = true;
			this.gridLRR.Columns[2].CellStyle.ForeColor = Color.Blue;
			this.gridLRR.Columns[3].CellStyle.Font.Underline = true;
			this.gridLRR.Columns[3].CellStyle.ForeColor = Color.Blue;
		}

		#region TSInfo Chart
		private void _initialChartTSInfo()
		{
			FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
			object[] dataSource = 
				facadeFactory.CreateQueryTSInfoFacade().QueryTSInfo(
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				FormatHelper.TODateInt(DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString() + "/01"),
				FormatHelper.TODateInt(DateTime.Today.ToString("yyyy/MM/dd")),
				TSInfoSummaryTarget.ErrorCode,
				string.Empty,
				5,
				1,
				int.MaxValue);
			
			this._processTSInfoOWC(dataSource);

		}
		private void _processTSInfoOWC(object[] dataSource)
		{
			this.OWCChartSpaceTSInfo.ClearCharts();

			if( dataSource != null )
			{
				dataSource = this.AddOtherInfoTSInfo(dataSource);
				string[] categories = new string[ dataSource.Length ];
				object[] ColumnClusteredValues = new object[ dataSource.Length ];	//柱状图values
				object[] ParetoValues = new object[ dataSource.Length ];		//柏拉图values

				for(int i = 0;i<dataSource.Length;i++)
				{
					categories[i] = (dataSource[i] as QDOTSInfo).ErrorCode;
					
					ColumnClusteredValues[i] = (dataSource[i] as QDOTSInfo).Quantity;
					ParetoValues[i] = this.getParetoValue(dataSource,i);
				}

				this.OWCChartSpaceTSInfo.ChartCombinationType = OWCChartCombinationType.OWCCombinationPareto;		//设置多图组合绘图方式为Pareto 柏拉图
				this.OWCChartSpaceTSInfo.AddChart("数量", categories, ColumnClusteredValues );						//默认添加柱状图
				this.OWCChartSpaceTSInfo.AddChart("百分比", categories, ParetoValues ,OWCChartType.LineMarkers);		//柏拉图
			
				int majorUnit = GetMahorUnitTSInfo(((QDOTSInfo)dataSource[0]).AllQuantity);			//设置左Y轴最小单元刻度,避免小于5的时候出现小数单元.
				if(majorUnit > 0)
				{
					this.OWCChartSpaceTSInfo.ChartLeftMajorUnit = majorUnit;
				}
				this.OWCChartSpaceTSInfo.ChartLeftMaximum = ((QDOTSInfo)dataSource[0]).AllQuantity;								//设置左Y轴最大刻度
				this.OWCChartSpaceTSInfo.Display = true;
			}
		}
		//添加其它统计栏位
		private object[] AddOtherInfoTSInfo(object[] dataSource)
		{
			if(dataSource !=null && dataSource.Length >0)
			{
				QDOTSInfo otherQDOTSInfo = new QDOTSInfo();
				string OtherCode = "其它";
				otherQDOTSInfo.ErrorCodeGroup = OtherCode;
				otherQDOTSInfo.ErrorCode = OtherCode;
				otherQDOTSInfo.ErrorCause = OtherCode;
				otherQDOTSInfo.ErrorLocation = OtherCode;
				otherQDOTSInfo.Duty = OtherCode;
				otherQDOTSInfo.AllQuantity = ((QDOTSInfo)dataSource[0]).AllQuantity;
				otherQDOTSInfo.Quantity = otherQDOTSInfo.AllQuantity - (int)this.getMaxNum(dataSource);
				decimal percent =  ((decimal)otherQDOTSInfo.Quantity)/((decimal)otherQDOTSInfo.AllQuantity);
				otherQDOTSInfo.Percent = percent;
				
				object[] newDataSource = new object[dataSource.Length + 1];
				for(int i=0;i<dataSource.Length;i++)
				{
					newDataSource[i] = dataSource[i];
				}
				newDataSource[dataSource.Length] = otherQDOTSInfo;

				return newDataSource;
			}
			return dataSource;
		}
		//获取最小单元刻度
		private int GetMahorUnitTSInfo(int quantity)
		{
			if(quantity < 10 ) 
				return 1;

			return 0;
		}
		private decimal getParetoValue(object[] dataSource,int count)
		{
			//柏拉图的value 是累计值,根据传入的累计数,统计累计值
			decimal returnValue = 0;

			for(int i=0;i<=count;i++)
			{
				returnValue += (dataSource[i] as QDOTSInfo).Percent;
			}
			return returnValue;
		}
		private double getMaxNum(object[] dataSource)
		{
			//统计传入数据源的最大值,用来设置左边Y轴的最大值
			double returnValue = 0;

			for(int i=0;i<=dataSource.Length-1;i++)
			{
				returnValue += (dataSource[i] as QDOTSInfo).Quantity;
			}
			return returnValue;
		}
		#endregion

		private object[] _loadDataSourceQuantity()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRPTCenterQuantity(today);
		}

		private object[] _loadDataSourceYieldDay()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRPTCenterYieldDay(today);
		}
		private object[] _loadDataSourceYieldWeek()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRPTCenterYieldWeek(today);
		}
		private object[] _loadDataSourceYieldMonth()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRPTCenterYieldMonth(today);
		}

		private object[] _loadDataSourceLRR()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRPTCenterLRR(today);
		}

		private void _processDataDourceToGridQuantity(object[] source)
		{
			this._initialGridQuantity();

			this.gridQuantity.Rows.Clear();

			if( source != null )
			{
				foreach(RPTCenterQuantity real in source)
				{
					UltraGridRow gridRow = null;
					object[] objs = new object[this.gridQuantity.Columns.Count];
					gridRow = new UltraGridRow( objs );
					this.gridQuantity.Rows.Add( gridRow );
					gridRow.Cells.FromKey("SegmentCode").Text = real.SegmentCode;
					gridRow.Cells.FromKey("DayQuantity").Text = real.DayQuantity.ToString();
					gridRow.Cells.FromKey("WeekQuantity").Text = real.WeekQuantity.ToString();
					gridRow.Cells.FromKey("MonthQuantity").Text = real.MonthQuantity.ToString();
				}
			}

			this._processGridStyleQuantity();
		}

		public struct YieldDay
		{
			public string opCode;
			public int NGTimes;
			public int Eattribute2;
		}

		public struct YieldWeek
		{
			public string opCode;
			public int NGTimes;
			public int Eattribute2;
		}

		public struct YieldMonth
		{
			public string opCode;
			public int NGTimes;
			public int Eattribute2;
		}

		private void _processDataDourceToGridYield(object[] sourceDay,object[] sourceWeek,object[] sourceMonth)
		{
			this._initialGridYield();

			this.gridYield.Rows.Clear();

			ArrayList alDay = new ArrayList();
			Hashtable htDay = new Hashtable();

			ArrayList alWeek = new ArrayList();
			Hashtable htWeek = new Hashtable();

			ArrayList alMonth = new ArrayList();
			Hashtable htMonth = new Hashtable();

			//取出当月所有MO2Route
			object[] sourceMO2Route = new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryMO2RouteByMOCode(today);
			Hashtable htRoute = new Hashtable();
			if(sourceMO2Route != null)
			{
				foreach(MO2Route mo2Route in sourceMO2Route)
				{
					if (htRoute.Contains(mo2Route.MOCode) == false)
					{
						htRoute.Add(mo2Route.MOCode, mo2Route);
					}
				}
			}
			//取出当月所有OP
			//object[] sourceOP = new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryOP(today);
			Hashtable htOP = new Hashtable();
			/*
			foreach(ItemRoute2OP route2OP in sourceOP)
			{
				if (htOP.Contains(route2OP.ItemCode + ":" + route2OP.RouteCode + ":" + route2OP.OPCode) == false)
				{
					htOP.Add(route2OP.ItemCode + ":" + route2OP.RouteCode + ":" + route2OP.OPCode, route2OP);
				}
			}
			*/

			if(sourceDay != null)
			{
				foreach(RPTCenterYield real in sourceDay)
				{
					ItemFacade itemFacade = new ItemFacade(DataProvider);
					MOFacade moFacade = new MOFacade(DataProvider);

					/*
					string routeCode = "";
					if (htRoute.Contains(real.MOCode) == true)
					{
						routeCode = ((MO2Route)htRoute[real.MOCode]).RouteCode;
					}

					object objOp = null;
					if (htOP.Contains(real.ItemCode + ":" + routeCode + ":" + real.OperationCode) == true)
					{
						objOp = htOP[real.ItemCode + ":" + routeCode + ":" + real.OperationCode];
					}
					string opControl = String.Empty;
					if (objOp != null)
					{
						opControl = ((ItemRoute2OP)objOp).OPControl;
					}
					else
					{
						string strTmp = "";
					}
					*/
					string opControl = GetOPControl(htRoute, htOP, real);

					if(itemFacade.IsTestingOperation(opControl))
					{
						alDay.Add(sourceDay);

						if(htDay.Contains(real.OperationCode))
						{
							YieldDay yield = new YieldDay();

							yield.opCode = real.OperationCode ;
							yield.NGTimes = real.NGTimes ;
							yield.Eattribute2 = real.Eattribute2 ;

							YieldDay oldYield = (YieldDay)htDay[real.OperationCode];

							yield.NGTimes = oldYield.NGTimes + real.NGTimes;
							yield.Eattribute2 = oldYield.Eattribute2 + real.Eattribute2;
							htDay[real.OperationCode] = yield;
						}
						else
						{
							YieldDay yield = new YieldDay();

							yield.NGTimes = real.NGTimes ;
							yield.opCode = real.OperationCode ;
							yield.Eattribute2 = real.Eattribute2 ;

							htDay.Add(real.OperationCode,yield);
						}
					}
				}
			}
			if(sourceWeek != null)
			{
				foreach(RPTCenterYield real in sourceWeek)
				{
					ItemFacade itemFacade = new ItemFacade(DataProvider);
					MOFacade moFacade = new MOFacade(DataProvider);

					/*
					string routeCode = "";
					if (htRoute.Contains(real.MOCode) == true)
					{
						routeCode = ((MO2Route)htRoute[real.MOCode]).RouteCode;
					}

					object objOp = null;
					if (htOP.Contains(real.ItemCode + ":" + routeCode + ":" + real.OperationCode) == true)
					{
						objOp = htOP[real.ItemCode + ":" + routeCode + ":" + real.OperationCode];
					}
					string opControl = String.Empty;
					if (objOp != null)
					{
						opControl = ((ItemRoute2OP)objOp).OPControl;
					}
					else
					{
						string strTmp = "";
					}
					*/
					string opControl = GetOPControl(htRoute, htOP, real);

					if(itemFacade.IsTestingOperation(opControl))
					{
						alWeek.Add(sourceWeek);

						if(htWeek.Contains(real.OperationCode))
						{
							YieldWeek yield = new YieldWeek();

							yield.opCode = real.OperationCode ;
							yield.NGTimes = real.NGTimes ;
							yield.Eattribute2 = real.Eattribute2 ;

							YieldWeek oldYield = (YieldWeek)htWeek[real.OperationCode];

							yield.NGTimes = oldYield.NGTimes + real.NGTimes;
							yield.Eattribute2 = oldYield.Eattribute2 + real.Eattribute2;
							htWeek[real.OperationCode] = yield;
						}
						else
						{
							YieldWeek yield = new YieldWeek();

							yield.NGTimes = real.NGTimes ;
							yield.opCode = real.OperationCode ;
							yield.Eattribute2 = real.Eattribute2 ;

							htWeek.Add(real.OperationCode,yield);
						}
					}
				}
			}
			if(sourceMonth != null)
			{
				foreach(RPTCenterYield real in sourceMonth)
				{
					ItemFacade itemFacade = new ItemFacade(DataProvider);
					MOFacade moFacade = new MOFacade(DataProvider);

					/*
					string routeCode = "";
					if (htRoute.Contains(real.MOCode) == true)
					{
						routeCode = ((MO2Route)htRoute[real.MOCode]).RouteCode;
					}

					object objOp = null;
					if (htOP.Contains(real.ItemCode + ":" + routeCode + ":" + real.OperationCode) == true)
					{
						objOp = htOP[real.ItemCode + ":" + routeCode + ":" + real.OperationCode];
					}
					string opControl = String.Empty;
					if (objOp != null)
					{
						opControl = ((ItemRoute2OP)objOp).OPControl;
					}
					else
					{
						string strTmp = "";
					}
					*/
					string opControl = GetOPControl(htRoute, htOP, real);

					if(itemFacade.IsTestingOperation(opControl))
					{
						alMonth.Add(sourceMonth);

						if(htMonth.Contains(real.OperationCode))
						{
							YieldMonth yield = new YieldMonth();

							yield.opCode = real.OperationCode ;
							yield.NGTimes = real.NGTimes ;
							yield.Eattribute2 = real.Eattribute2 ;

							YieldMonth oldYield = (YieldMonth)htMonth[real.OperationCode];

							yield.NGTimes = oldYield.NGTimes + real.NGTimes;
							yield.Eattribute2 = oldYield.Eattribute2 + real.Eattribute2;
							htMonth[real.OperationCode] = yield;
						}
						else
						{
							YieldMonth yield = new YieldMonth();

							yield.NGTimes = real.NGTimes ;
							yield.opCode = real.OperationCode ;
							yield.Eattribute2 = real.Eattribute2 ;

							htMonth.Add(real.OperationCode,yield);
						}
					}
				}
			}

			foreach( DictionaryEntry deMonth in htMonth )
			{
				UltraGridRow gridRow = null;
				object[] objs = new object[this.gridYield.Columns.Count];
				gridRow = new UltraGridRow( objs );
				this.gridYield.Rows.Add( gridRow );

				YieldMonth yieldMonth = (YieldMonth)deMonth.Value;
				gridRow.Cells.FromKey("OPCode").Text = yieldMonth.opCode;
				if( yieldMonth.Eattribute2 == 0 )
				{
					gridRow.Cells.FromKey("MonthPercent").Text = "N/A";
				}
				else if( yieldMonth.NGTimes == 0 )
				{
					gridRow.Cells.FromKey("MonthPercent").Text = "100%";
				}
				else if( yieldMonth.NGTimes == yieldMonth.Eattribute2 )
				{
					gridRow.Cells.FromKey("MonthPercent").Text = "0%";
				}
				else
				{
					gridRow.Cells.FromKey("MonthPercent").Text = ( 1-Convert.ToDecimal(yieldMonth.NGTimes)/Convert.ToDecimal(yieldMonth.Eattribute2) ).ToString("##.##%");
				}
			
				gridRow.Cells.FromKey("DayPercent1").Text = "N/A";
				foreach( DictionaryEntry deDay in htDay )
				{
					YieldDay yieldDay = (YieldDay)deDay.Value;
					if( yieldDay.opCode == yieldMonth.opCode )
					{
						if( yieldDay.Eattribute2 == 0 )
						{
							gridRow.Cells.FromKey("DayPercent1").Text = "N/A";
						}
						else if( yieldDay.NGTimes == 0 )
						{
							gridRow.Cells.FromKey("DayPercent1").Text = "100%";
						}
						else if( yieldDay.NGTimes == yieldDay.Eattribute2 )
						{
							gridRow.Cells.FromKey("DayPercent1").Text = "0%";
						}
						else
						{
							gridRow.Cells.FromKey("DayPercent1").Text = ( 1-Convert.ToDecimal(yieldDay.NGTimes)/Convert.ToDecimal(yieldDay.Eattribute2) ).ToString("##.##%");
						}
					}
				}
				gridRow.Cells.FromKey("WeekPercent").Text = "N/A";
				foreach( DictionaryEntry deWeek in htWeek )
				{
					YieldWeek yieldWeek = (YieldWeek)deWeek.Value;
					if( yieldWeek.opCode == yieldMonth.opCode )
					{
						if( yieldWeek.Eattribute2 == 0 )
						{
							gridRow.Cells.FromKey("WeekPercent").Text = "N/A";
						}
						else if( yieldWeek.NGTimes == 0 )
						{
							gridRow.Cells.FromKey("WeekPercent").Text = "100%";
						}
						else if( yieldWeek.NGTimes == yieldWeek.Eattribute2 )
						{
							gridRow.Cells.FromKey("WeekPercent").Text = "0%";
						}
						else
						{
							gridRow.Cells.FromKey("WeekPercent").Text = ( 1-Convert.ToDecimal(yieldWeek.NGTimes)/Convert.ToDecimal(yieldWeek.Eattribute2) ).ToString("##.##%");
						}
					}
				}
			}

			this._processGridStyleYield();
		}
		private string GetOPControl(Hashtable htRoute, Hashtable htOP, RPTCenterYield real)
		{
			string routeCode = "";
			if (htRoute.Contains(real.MOCode) == true)
			{
				routeCode = ((MO2Route)htRoute[real.MOCode]).RouteCode;
			}

			object objOp = null;
			if (htOP.Contains(real.ItemCode + ":" + routeCode + ":" + real.OperationCode) == true)
			{
				objOp = htOP[real.ItemCode + ":" + routeCode + ":" + real.OperationCode];
			}
			else
			{
				QueryFacade1 queryFacade1 = new QueryFacade1(this.DataProvider);
				try
				{
					objOp = queryFacade1.GetItemRoute2Operation(real.ItemCode, routeCode, real.OperationCode);
					if (objOp != null)
					{
						htOP.Add(real.ItemCode + ":" + routeCode + ":" + real.OperationCode, objOp);
					}
				}
				catch {}
			}
			string opControl = String.Empty;
			if (objOp != null)
			{
				if (objOp is ItemRoute2OP)
					opControl = ((ItemRoute2OP)objOp).OPControl;
				else if (objOp is BenQGuru.eMES.Domain.BaseSetting.Operation)
					opControl = ((BenQGuru.eMES.Domain.BaseSetting.Operation)objOp).OPControl;
			}
			else
			{
				BenQGuru.eMES.BaseSetting.BaseModelFacade modelFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
				object objTmp = modelFacade.GetOperation(real.OperationCode);
				if (objTmp != null)
				{
					opControl = ((BenQGuru.eMES.Domain.BaseSetting.Operation)objTmp).OPControl;
					htOP.Add(real.ItemCode + ":" + routeCode + ":" + real.OperationCode, objTmp);
				}
			}
			return opControl;
		}

		private void _processDataDourceToGridLRR(object[] source)
		{
			this._initialGridLRR();

			this.gridLRR.Rows.Clear();

			if( source != null )
			{
				foreach(RPTCenterLRR real in source)
				{
					UltraGridRow gridRow = null;
					object[] objs = new object[this.gridLRR.Columns.Count];
					gridRow = new UltraGridRow( objs );
					this.gridLRR.Rows.Add( gridRow );
					gridRow.Cells.FromKey("SegmentCode").Text = real.SegmentCode;
					if(real.DayLRR == 0)
					{
						gridRow.Cells.FromKey("DayLRR").Text = "0%";
					}
					else
					{
						gridRow.Cells.FromKey("DayLRR").Text = real.DayLRR.ToString("##.##%");
					}
					if(real.WeekLRR == 0)
					{
						gridRow.Cells.FromKey("WeekLRR").Text = "0%";
					}
					else
					{
						gridRow.Cells.FromKey("WeekLRR").Text = real.WeekLRR.ToString("##.##%");
					}
					if(real.MonthLRR == 0)
					{
						gridRow.Cells.FromKey("MonthLRR").Text = "0%";
					}
					else
					{
						gridRow.Cells.FromKey("MonthLRR").Text = real.MonthLRR.ToString("##.##%");
					}
				}
			}

			this._processGridStyleLRR();
		}
		
		private void _processGridStyleQuantity()
		{
			try
			{
				GridItemStyle style = new GridItemStyle(true);
				style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
				for(int col=1;col < this.gridQuantity.Columns.Count;col++)
				{			
					for(int row=0;row<this.gridQuantity.Rows.Count;row++)
					{
                        this.gridQuantity.Rows[row].Cells[col].Style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
					}
				}
			}
			catch
			{
			}
		}
		
		private void _processGridStyleYield()
		{
			try
			{
				GridItemStyle style = new GridItemStyle(true);
				style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
				for(int col=1;col < this.gridYield.Columns.Count;col++)
				{			
					for(int row=0;row<this.gridYield.Rows.Count;row++)
					{
                        this.gridYield.Rows[row].Cells[col].Style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;		
					}
				}
			}
			catch
			{
			}
		}

		private void _processGridStyleLRR()
		{
			try
			{
				GridItemStyle style = new GridItemStyle(true);
				style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
				for(int col=1;col < this.gridLRR.Columns.Count;col++)
				{			
					for(int row=0;row<this.gridLRR.Rows.Count;row++)
					{
                        this.gridLRR.Rows[row].Cells[col].Style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
					}
				}
			}
			catch
			{
			}
		}

		private void gridQuantity_Click(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			if( e.Cell.Column.Key.ToUpper() == "DayQuantity".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"ReportCenterLine.aspx",
					new string[]{"SegmentCode"},
					new string[]{e.Cell.Row.Cells[0].Text})
					);
			}
			else if( e.Cell.Column.Key.ToUpper() == "WeekQuantity".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"ReportCenterWeekQuantity.aspx",
					new string[]{"SegmentCode"},
					new string[]{e.Cell.Row.Cells[0].Text})
					);
			}
			else if( e.Cell.Column.Key.ToUpper() == "MonthQuantity".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"ReportCenterMonthQuantity.aspx",
					new string[]{"SegmentCode"},
					new string[]{e.Cell.Row.Cells[0].Text})
					);
			}
		}

		private void gridYield_Click(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			if( e.Cell.Column.Key.ToUpper() == "DayPercent1".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"ReportCenterYield.aspx",
					new string[]{"OPCode"},
					new string[]{e.Cell.Row.Cells[0].Text})
					);
			}
			else if( e.Cell.Column.Key.ToUpper() == "WeekPercent".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"FReportCenterWeekPercent.aspx",
					new string[]{"OPCode"},
					new string[]{e.Cell.Row.Cells[0].Text})
					);
			}
			else if( e.Cell.Column.Key.ToUpper() == "MonthPercent".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"FReportCenterMonthPercent.aspx",
					new string[]{"OPCode"},
					new string[]{e.Cell.Row.Cells[0].Text})
					);
			}
		}	

		private void gridYield_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if( e.Cell.Column.Key.ToUpper() == "DayYield".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"ReportCenterDayYield.aspx",
					new string[]{"OPCode"},
					new string[]{e.Cell.Row.Cells[0].Text})
					);
			}
			else if( e.Cell.Column.Key.ToUpper() == "WeekYield".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"ReportCenterWeekYield.aspx",
					new string[]{"OPCode"},
					new string[]{e.Cell.Row.Cells[0].Text})
					);
			}
			else if( e.Cell.Column.Key.ToUpper() == "MonthYield".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"ReportCenterMonthYield.aspx",
					new string[]{"OPCode"},
					new string[]{e.Cell.Row.Cells[0].Text})
					);
			}
		}

		private void gridLRR_Click(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			if( e.Cell.Column.Key.ToUpper() == "DayLRR".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"FReportCenterLRR.aspx",
					new string[]{"segmentcode","dategroup","lrr"},
					new string[]{e.Cell.Row.Cells[0].Text,"DAY",
									(Convert.ToDecimal(e.Cell.Row.Cells[1].Text.Substring(0,e.Cell.Row.Cells[1].Text.Length-1))/100).ToString()})
					);
			}
			else if( e.Cell.Column.Key.ToUpper() == "WeekLRR".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"FReportCenterLRR.aspx",
					new string[]{"segmentcode","dategroup","lrr"},
					new string[]{e.Cell.Row.Cells[0].Text,"WEEK",
									(Convert.ToDecimal(e.Cell.Row.Cells[2].Text.Substring(0,e.Cell.Row.Cells[2].Text.Length-1))/100).ToString()})
					);
			}
			else if( e.Cell.Column.Key.ToUpper() == "MonthLRR".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"FReportCenterLRR.aspx",
					new string[]{"segmentcode","dategroup","lrr"},
					new string[]{e.Cell.Row.Cells[0].Text,"MONTH",
									(Convert.ToDecimal(e.Cell.Row.Cells[3].Text.Substring(0,e.Cell.Row.Cells[3].Text.Length-1))/100).ToString()})
					);
			}
		}

		protected void chkRefreshAuto_CheckedChanged(object sender, System.EventArgs e)
		{
			this._doQuery();
		}
	}
}
