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
using System.IO;
using System.Text;

using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Common.Config;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FRealTimeQuantitySummaryQP 的摘要说明。
	/// </summary>
	public partial class FRealTimeQuantitySummaryQP2 : BaseRQPage
	{
		protected System.Web.UI.WebControls.Label lblStartDateQuery;
		protected System.Web.UI.WebControls.Label lblEndDateQuery;
		public BenQGuru.eMES.Web.UserControl.eMESDate eMESDate1;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Timers.Timer timerRefresh;
		/// <summary>
		/// 生产线
		/// </summary>
		protected BenQGuru.eMES.Web.SelectQuery.SelectableTextBox4SS txtStepSequence;

		protected  string inputColText ="投入数";		//初始
		protected  string outputColText ="产出数";      //下发

		protected GridHelperForRPT _gridHelper = null;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{		
			this._gridHelper = new GridHelperForRPT(this.gridWebGrid);
			this.gridWebGrid.DisplayLayout.CellClickActionDefault = CellClickAction.NotSet;

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.eMESDate1.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));				
				//
				BaseModelFacade facade = new FacadeFactory(base.DataProvider).CreateBaseModelFacade();					
				object[] segments = facade.QuerySegment("",0,System.Int32.MaxValue);
				if( segments != null )
				{
					this.drpSegmentQuery.Items.Clear();

					foreach(Segment seg in segments)
					{
						this.drpSegmentQuery.Items.Add( seg.SegmentCode );
					}

					this.drpSegmentQuery.Items.Insert(0, "" );					

					this.RefreshController1.Interval = ConfigSection.Current.DomainSetting.Interval;
				}

				this._initialWebGrid();			
			}	
			
			if( !this.IsPostBack )
			{

				//如果接受到其它页面的参数直接执行查询
				if(this.GetRequestParam("post") != null && this.GetRequestParam("post") != string.Empty)	
				{
					this.eMESDate1.Text = this.GetRequestParam("shiftday");						//日期
					this.drpSegmentQuery.SelectedValue =  this.GetRequestParam("segmentcode");	//工段
					this.drpSegmentQuery_SelectedIndexChanged(null,null);						//加载班次选项
					this.drpShiftQuery.SelectedValue =  this.GetRequestParam("shiftcode");		//班次
					this.txtModelQuery.Text = this.GetRequestParam("modelcode");				//产品别
					this.txtItemQuery.Text = this.GetRequestParam("itemcode");					//产品
					this.txtMoQuery.Text = this.GetRequestParam("mocode");						//工单
					this._doQuery();
				}
			}	
			if( this.V_StartRefresh )
			{
				this._doQuery();
			}
		}


		private void _initialWebGrid()
		{
			this.gridWebGrid.Columns.Clear();

			this._gridHelper.GridHelper.AddColumn("StepSequenceCode","生产线",null,100);
			this._gridHelper.GridHelper.AddColumn("ItemCode","产品代码",null,100);
			
			string selected = "";
			if( this.drpShiftQuery.SelectedValue == "" )
			{
				this._gridHelper.GridHelper.AddColumn("TPCode","时段代码",null);
			}
			else
			{	
				object[] tps = new FacadeFactory(base.DataProvider).CreateShfitModelFacade().GetTimePeriodByShiftCode(this.drpShiftQuery.SelectedValue);
				if( tps != null )
				{
					this._gridHelper.GridHelper.AddColumn("InputOutputName","",null,100);
					this.gridWebGrid.Bands[0].Columns.FromKey("StepSequenceCode").MergeCells = true;
					int now = FormatHelper.TOTimeInt(System.DateTime.Now);

					int count = tps.Length;
					//foreach(TimePeriod tp in tps)
					for( int i=0; i<count; i++ )
					{
						TimePeriod tp = tps[i] as TimePeriod;
						string key = tp.TimePeriodCode;

						this._gridHelper.GridHelper.AddColumn(key,FormatHelper.ToShortTimeString(tp.TimePeriodBeginTime) + "<br>" + FormatHelper.ToShortTimeString(tp.TimePeriodEndTime),null);

						if( tp.TimePeriodBeginTime <= now &&
							tp.TimePeriodEndTime >= now )
						{
							selected = tp.TimePeriodCode;
						}						
					}
				}
				else
				{
					this._gridHelper.GridHelper.AddColumn("TPCode","时段代码",null);
				}					
			}
			this._gridHelper.GridHelper.AddColumn("Summary","汇总",null,100);

			GridItemStyle blueBack = new GridItemStyle(true);
			blueBack.BackColor = Color.SkyBlue;
			if( this.gridWebGrid.Columns.FromKey( selected ) != null )
			{
                this.gridWebGrid.Columns.FromKey(selected).HeaderStyle.BackColor = Color.SkyBlue;
			}						
			this.gridWebGrid.Columns.FromKey("Summary").Hidden = false;

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
			this.timerRefresh = new System.Timers.Timer();
			((System.ComponentModel.ISupportInitialize)(this.timerRefresh)).BeginInit();
			this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
			this.gridWebGrid.Click += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.gridWebGrid_Click);
			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
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

		protected void drpSegmentQuery_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.drpShiftQuery.Items.Clear();

			this.txtStepSequence.Segment = this.drpSegmentQuery.SelectedValue;

			if( this.drpSegmentQuery.SelectedValue != "" )
			{
				BaseModelFacade facade = new FacadeFactory(base.DataProvider).CreateBaseModelFacade();					
				Segment segment = facade.GetSegment( this.drpSegmentQuery.SelectedValue ) as Segment;
				if( segment != null )
				{
                    object[] shifts = new FacadeFactory(base.DataProvider).CreateShfitModelFacade().QueryShiftBySegment("", segment.SegmentCode, 0, System.Int32.MaxValue);
					if( shifts != null )
					{
						string selected = "";
						int now = FormatHelper.TODateInt(System.DateTime.Now);

						foreach(Shift shift in shifts)
						{
							if( shift.ShiftBeginTime <= now &&
								shift.ShiftEndTime >= now )
							{
								selected = shift.ShiftCode;
							}
							this.drpShiftQuery.Items.Add( shift.ShiftCode );
						}
						try
						{
							this.drpShiftQuery.SelectedValue = selected;
						}
						catch
						{
							this.drpShiftQuery.SelectedIndex = 0;
						}
					}
				}
			}			
		}


		private object[] _loadDataSource()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRealTimeQuantity(
				this.V_SegmentCode,
				this.V_StepSequenceCode,
				this.V_ModelCode,
				this.V_ItemCode,
				this.V_MoCode,
				this.drpFactory.SelectedValue,
				this.V_ShiftCode,
				FormatHelper.TODateInt(this.V_Date));
		}


		private void _processDataDourceToGrid(object[] source)
		{
			this._initialWebGrid();

			this.gridWebGrid.Rows.Clear();

			if( source != null )
			{				
				
				foreach(RealTimeQuantity real in source)
				{
					bool isAdded = false;
					UltraGridRow gridRow = null;
					UltraGridRow gridRow2 = null;
					foreach(UltraGridRow row in this.gridWebGrid.Rows)
					{
						if( string.Compare( row.Cells.FromKey("StepSequenceCode").Text,real.StepSequenceCode,true)==0 
							&& string.Compare( row.Cells.FromKey("ItemCode").Text,real.ItemCode,true )==0
							&& string.Compare(row.Cells.FromKey("InputOutputName").Text,this.inputColText,true)==0 )
						{
							gridRow = row;
							isAdded = true;
						}

						if( string.Compare( row.Cells.FromKey("StepSequenceCode").Text,real.StepSequenceCode,true)==0 
							&& string.Compare( row.Cells.FromKey("ItemCode").Text,real.ItemCode,true )==0
							&& string.Compare(row.Cells.FromKey("InputOutputName").Text,this.outputColText,true)==0 )
						{
							gridRow2 = row;
							isAdded = true;
						}

						if( gridRow!=null && gridRow2!=null )break;
					}
					if( !isAdded || (gridRow == null && gridRow2==null) )
					{
						//投入行
						object[] objs = new object[this.gridWebGrid.Columns.Count];
						gridRow = new UltraGridRow( objs );
					
						this.gridWebGrid.Rows.Add( gridRow );
						gridRow.Cells.FromKey("StepSequenceCode").Text = real.StepSequenceCode;
						gridRow.Cells.FromKey("ItemCode").Text = real.ItemCode;
						gridRow.Cells.FromKey("InputOutputName").Text = this.inputColText;

						//产出行
						object[] objs2 = new object[this.gridWebGrid.Columns.Count];		
						gridRow2 = new UltraGridRow( objs );								
						this.gridWebGrid.Rows.Add( gridRow2 );								
						gridRow2.Cells.FromKey("StepSequenceCode").Text = real.StepSequenceCode;
						gridRow2.Cells.FromKey("ItemCode").Text = real.ItemCode;
						gridRow2.Cells.FromKey("InputOutputName").Text = this.outputColText;
						
						/* 合并产品代码 */
						gridRow.Cells.FromKey("ItemCode").RowSpan = 2;

					}
					if(gridRow == null)//投入行
					{
						
						object[] objs = new object[this.gridWebGrid.Columns.Count];
						gridRow = new UltraGridRow( objs );
					
						this.gridWebGrid.Rows.Add( gridRow );
						gridRow.Cells.FromKey("StepSequenceCode").Text = real.StepSequenceCode;
						gridRow.Cells.FromKey("ItemCode").Text = real.ItemCode;
						gridRow.Cells.FromKey("InputOutputName").Text = this.inputColText;
					}
					if(gridRow2 ==null)//产出行
					{
						
						object[] objs2 = new object[this.gridWebGrid.Columns.Count];		
						gridRow2 = new UltraGridRow( objs2 );								
						this.gridWebGrid.Rows.Add( gridRow2 );								
						gridRow2.Cells.FromKey("StepSequenceCode").Text = real.StepSequenceCode;
						gridRow2.Cells.FromKey("ItemCode").Text = real.ItemCode;
						gridRow2.Cells.FromKey("InputOutputName").Text = this.outputColText;
					}

					#region 投入数Row
					gridRow.Cells.FromKey(real.TimePeriodCode).Text = real.InputQuantity.ToString();
				
					decimal summaryIn = 0;
					try
					{

						summaryIn = System.Decimal.Parse(
							gridRow.Cells.FromKey("Summary").Text);
					}
					catch
					{
						summaryIn = 0;
					}
					gridRow.Cells.FromKey("Summary").Text = (summaryIn + real.InputQuantity).ToString();
					#endregion

					#region 产出数Row
					gridRow2.Cells.FromKey(real.TimePeriodCode).Text = real.OutputQuantity.ToString();//"产出数";
			
					decimal summaryOut = 0;
					try
					{
						if(gridRow2.Cells.FromKey("Summary").Text!=null && gridRow2.Cells.FromKey("Summary").Text != string.Empty && gridRow2.Cells.FromKey("Summary").Text != "null")
							summaryOut = System.Decimal.Parse(
								gridRow2.Cells.FromKey("Summary").Text);
					}
					catch
					{
						summaryOut = 0;
					}
					gridRow2.Cells.FromKey("Summary").Text = (summaryOut + real.OutputQuantity).ToString();
				
					#endregion
					
				}

				#region 投入数汇总

				UltraGridRow summaryRow = new UltraGridRow();
				this.gridWebGrid.Rows.Add( summaryRow );
				
				for(int i=1;i<this.gridWebGrid.Columns.Count;i++)
				{
					decimal summary = 0;
					for(int j=0;j<this.gridWebGrid.Rows.Count-1;j++)
					{
						try
						{
							if(this.gridWebGrid.Rows[j].Cells.FromKey("InputOutputName").Text == this.inputColText) //汇总投入数行
								if(this.gridWebGrid.Rows[j].Cells[i].Text!=null && this.gridWebGrid.Rows[j].Cells[i].Text != string.Empty && this.gridWebGrid.Rows[j].Cells[i].Text!="null")
								{
									summary += System.Decimal.Parse( this.gridWebGrid.Rows[j].Cells[i].Text);
								}
						}
						catch
						{
							summary += 0;
						}
					}
					summaryRow.Cells[i].Text = summary.ToString();
				}
				summaryRow.Cells.FromKey("StepSequenceCode").Text = "投入数汇总";//this.languageComponent1.GetString("InputSummary");// 
				summaryRow.Cells.FromKey("InputOutputName").Text = string.Empty;

				#endregion

				#region 产出数汇总

				UltraGridRow summaryRow2 = new UltraGridRow();
				this.gridWebGrid.Rows.Add( summaryRow2 );
				
				for(int i=1;i<this.gridWebGrid.Columns.Count;i++)
				{
					decimal summary = 0;
					for(int j=0;j<this.gridWebGrid.Rows.Count-1;j++)
					{
						try
						{
							if(this.gridWebGrid.Rows[j].Cells.FromKey("InputOutputName").Text == this.outputColText)//汇总产出数行
								if(this.gridWebGrid.Rows[j].Cells[i].Text!=null && this.gridWebGrid.Rows[j].Cells[i].Text != string.Empty && this.gridWebGrid.Rows[j].Cells[i].Text!="null")
								{
									summary += System.Decimal.Parse( this.gridWebGrid.Rows[j].Cells[i].Text);
								}
						}
						catch
						{
							summary += 0;
						}
					}
					summaryRow2.Cells[i].Text = summary.ToString();
				}
				summaryRow2.Cells.FromKey("StepSequenceCode").Text = "产出数汇总";//this.languageComponent1.GetString("OutputSummary");//
				summaryRow2.Cells.FromKey("InputOutputName").Text = string.Empty;

				#endregion
			}

			this._processGridStyle();
		}

		private void _processGridStyle()
		{
			try
			{
				GridItemStyle style = new GridItemStyle(true);
				style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
				for(int col=1;col < this.gridWebGrid.Columns.Count-1;col++)
				{			
					for(int row=0;row<this.gridWebGrid.Rows.Count-1;row++)
					{
                        this.gridWebGrid.Rows[row].Cells[col].Style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
					}
				}
			}
			catch
			{
			}
		}


		private bool _checkRequireFields()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(this.lblSegment,this.drpSegmentQuery,40,true) );
			manager.Add( new LengthCheck(this.lblShip,this.drpShiftQuery,40,true) );
			manager.Add( new DateCheck(this.lblDate,this.eMESDate1.Text,true) );
			manager.Add( new DateRangeCheck(this.lblDate,this.eMESDate1.Text,this.lblToday,FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now)),0,ConfigSection.Current.DomainSetting.MaxDateRange,true));
			
			if( !manager.Check() )
			{
				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);

				return false;
			}
			else
			{
				return true;
			}
		}


		private void _doQuery()
		{
			if( this._checkRequireFields() )
			{
				this.V_StartRefresh = this.chbRefreshAuto.Checked;
//				if( this.chkRefreshAuto.Checked )
//				{
//					this.RefreshController1.Start();
//				}

				this._initialWebGrid();

				this._processDataDourceToGrid( this._loadDataSource() );
			}
			else
			{
//				this.RefreshController1.Stop();
				this.chbRefreshAuto.Checked = false;
				this.V_StartRefresh = false;
			}
		}


		#region ViewState
		private string V_SegmentCode
		{
			get
			{		
				return this.drpSegmentQuery.SelectedValue;
//				if( this.ViewState["V_SegmentCode"] != null )
//				{
//					return this.ViewState["V_SegmentCode"].ToString();
//				}
//				else
//				{
//					return "";
//				}
			}
			set
			{
				this.ViewState["V_SegmentCode"] = value;
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

		private string V_StepSequenceCode
		{
			get
			{	
				return this.txtStepSequence.Text;
//				if( this.ViewState["V_StepSequenceCode"] != null )
//				{
//					return this.ViewState["V_StepSequenceCode"].ToString();
//				}
//				else
//				{
//					return "";
//				}
			}
			set
			{
				this.ViewState["V_StepSequenceCode"] = value;
			}
		}

		private string V_ShiftCode
		{
			get
			{	
				return this.drpShiftQuery.SelectedValue;
//				if( this.ViewState["V_ShiftCode"] != null )
//				{
//					return this.ViewState["V_ShiftCode"].ToString();
//				}
//				else
//				{
//					return "";
//				}
			}
			set
			{
				this.ViewState["V_ShiftCode"] = value;
			}
		}

		private string V_Date
		{
			get
			{	
				return this.eMESDate1.Text;
//				if( this.ViewState["V_Date"] != null )
//				{
//					return this.ViewState["V_Date"].ToString();
//				}
//				else
//				{
//					return "";
//				}
			}
			set
			{
				this.ViewState["V_Date"] = value;
			}
		}

		private string V_ModelCode
		{
			get
			{	
				return this.txtModelQuery.Text;
//				if( this.ViewState["V_ModelCode"] != null )
//				{
//					return this.ViewState["V_ModelCode"].ToString();
//				}
//				else
//				{
//					return "";
//				}
			}
			set
			{
				this.ViewState["V_ModelCode"] = value;
			}
		}

		private string V_ItemCode
		{
			get
			{		
				return this.txtItemQuery.Text;
//				if( this.ViewState["V_ItemCode"] != null )
//				{
//					return this.ViewState["V_ItemCode"].ToString();
//				}
//				else
//				{
//					return "";
//				}
			}
			set
			{
				this.ViewState["V_ItemCode"] = value;
			}
		}

		private string V_MoCode
		{
			get
			{	
				return this.txtMoQuery.Text;
//				if( this.ViewState["V_MoCode"] != null )
//				{
//					return this.ViewState["V_MoCode"].ToString();
//				}
//				else
//				{
//					return "";
//				}
			}
			set
			{
				this.ViewState["V_MoCode"] = value;
			}
		}
		#endregion

		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{	
			this._doQuery();
		}


		private void timerRefresh_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
		}

		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if( e.Cell.Column.Index > 0 && 
				e.Cell.Column.Index < this.gridWebGrid.Columns.Count-1 &&
				e.Cell.Row.Index != this.gridWebGrid.Rows.Count - 1 )
			{
				
				if(e.Cell.Text!=null && e.Cell.Text.Trim()!=string.Empty)
				{
					if( !this.IsStartupScriptRegistered("details") )
					{
						bool needMidOutput = FormatHelper.CleanString(this.txtItemQuery.Text).Length == 0?true:false;
						string script = 
							@"<script language='jscript' src='../Skin/js/selectAll.js'></script>
							<script language='javascript'>";

						script += string.Format(
							@"window.showModalDialog('./{0}','',showDialog(7));",
							this.MakeRedirectUrl(
							"FRealTimeQuantityDetails.aspx",
							new string[]{
											"segmentcode",
											"shiftday",
											"shiftcode",
											"tpcode",
											"tpcodedetail",
											"stepsequencecode",
											"modelcode",
											"Itemcode",
											"mocode",
											"IncludeMidOutput"
										},
							new string[]{
											this.drpSegmentQuery.SelectedValue,
											this.eMESDate1.Text,
											this.drpShiftQuery.SelectedValue,
											e.Cell.Column.Key,
											e.Cell.Column.HeaderText.Replace("<br>","~"),
											e.Cell.Row.Cells.FromKey("StepSequenceCode").Text,
											this.txtModelQuery.Text,
											//this.txtItemQuery.Text,
											e.Cell.Row.Cells.FromKey("ItemCode").Text,
											this.txtMoQuery.Text,
											needMidOutput.ToString()	
										}));

						script += @"</script>";

						this.RegisterClientScriptBlock("details",script);
					}
				}
			}
			
		}

		protected void chkRefreshAuto_CheckedChanged(object sender, System.EventArgs e)
		{
//			this.V_StartRefresh = this.chkRefreshAuto.Checked;	
			this._doQuery();
		}

		private void gridWebGrid_Click(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			if( e.Cell != null )
			{
				if(e.Cell.Row.Cells.FromKey("InputOutputName").Text == string.Empty)return;
				if(e.Cell.Key == "InputOutputName")return;
				if(e.Cell.Key == "ItemCode")return; 
				if( e.Cell.Column.Index > 0 && 
					e.Cell.Column.Index < this.gridWebGrid.Columns.Count-1 &&
					e.Cell.Row.Index != this.gridWebGrid.Rows.Count - 1 )
				{
					if(e.Cell.Text!=null && e.Cell.Text.Trim()!=string.Empty)
					{
						if( !this.IsStartupScriptRegistered("details") )
						{
							bool needMidOutput = FormatHelper.CleanString(this.txtItemQuery.Text).Length == 0?true:false;
							string script = 
								@"<script language='jscript' src='../Skin/js/selectAll.js'></script>
							<script language='javascript'>";

							script += string.Format(
								@"window.showModalDialog('./{0}','',showDialog(7));",
								this.MakeRedirectUrl(
								"FRealTimeQuantityDetails2.aspx",
								new string[]{
												"segmentcode",
												"shiftday",
												"shiftcode",
												"tpcode",
												"tpcodedetail",
												"stepsequencecode",
												"modelcode",
												"Itemcode",
												"mocode",
												"IncludeMidOutput"
											},
								new string[]{
												this.drpSegmentQuery.SelectedValue,
												this.eMESDate1.Text,
												this.drpShiftQuery.SelectedValue,
												e.Cell.Column.Key,
												e.Cell.Column.HeaderText.Replace("<br>","~"),
												e.Cell.Row.Cells.FromKey("StepSequenceCode").Text,
												this.txtModelQuery.Text,
												//this.txtItemQuery.Text,
												e.Cell.Row.Cells.FromKey("ItemCode").Text,
												this.txtMoQuery.Text,
												needMidOutput.ToString(),
												
											}));

							script += @"</script>";

							this.RegisterClientScriptBlock("details",script);
						}
					}
				}
			}
		}

		private void factory_load()
		{
			if(!Page.IsPostBack)
			{
				WarehouseFacade whFacade = new WarehouseFacade(base.DataProvider);
				object[]  factorys  = whFacade.GetAllFactory();
				if( factorys != null )
				{
					foreach( BenQGuru.eMES.Domain.Warehouse.Factory _factory in factorys)
					{
						this.drpFactory.Items.Add( _factory.FactoryCode ) ;
					}
					new DropDownListBuilder( this.drpFactory ).AddAllItem( this.languageComponent1 ) ;
				}
			}
		}

		protected void cmdGridExport2_ServerClick(object sender, EventArgs e)
		{
			this.GridExport(this.gridWebGrid);
		}
	}
}
