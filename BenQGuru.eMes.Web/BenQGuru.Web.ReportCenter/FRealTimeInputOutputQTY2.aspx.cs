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
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Common.Config;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FRealTimeInputOutputQTY 的摘要说明。
	/// </summary>
	public partial class FRealTimeInputOutputQTY2 : BaseRQPage
	{
		protected System.Web.UI.WebControls.Label lblStartDateQuery;
		protected System.Web.UI.WebControls.Label lblEndDateQuery;
		public BenQGuru.eMES.Web.UserControl.eMESDate eMESDate1;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Timers.Timer timerRefresh;
		protected System.Web.UI.WebControls.Label lblFactory;
		protected System.Web.UI.WebControls.DropDownList drpFactory;

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

		private void _initialWebGrid()
		{
			this.gridWebGrid.Columns.Clear();

			this._gridHelper.GridHelper.AddColumn("ModelCode","产品别",null);
			this._gridHelper.GridHelper.AddColumn("ItemCode","产品",null);
			this._gridHelper.GridHelper.AddColumn("MOCode","工单",null);
			this._gridHelper.GridHelper.AddColumn("MOPlanqty","工单计划数",null);
			this._gridHelper.GridHelper.AddColumn("MOShiftInputqty","工单投入数(当班)",null);
			this._gridHelper.GridHelper.AddColumn("MOInputqty1","工单投入数(累计)",null);
			this._gridHelper.GridHelper.AddColumn("MOShiftOutputqty","工单产出数(当班)",null);
			this._gridHelper.GridHelper.AddColumn("MOOutputqty","工单产出数(累计)",null);
			this._gridHelper.GridHelper.AddColumn("MOScrapqty1","工单拆解数(累计)",null);
			this._gridHelper.GridHelper.AddColumn("MOOffQty","工单脱离数(累计)",null);

			//多语言
			this._gridHelper.GridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private object[] _loadDataSource()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRealTimeInputOutputQuantity(
				this.V_SegmentCode,
				this.V_MoCode,
				this.V_ShiftCode,
				FormatHelper.TODateInt(this.V_Date));
		}

		private Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								((RealTimeInputOutputQuantity)obj).ModelCode.ToString(),
								((RealTimeInputOutputQuantity)obj).ItemCode.ToString(),
								((RealTimeInputOutputQuantity)obj).MOCode.ToString(),
								((RealTimeInputOutputQuantity)obj).MOPlanqty.ToString(),
								WebQueryHelper.GetLinkHtml(this.languageComponent1,((RealTimeInputOutputQuantity)obj).MOShiftInputqty.ToString(),this.GetUrlParams(obj)),
								//((RealTimeInputOutputQuantity)obj).MOShiftInputqty.ToString(),
								((RealTimeInputOutputQuantity)obj).MOInputqty.ToString(),
								WebQueryHelper.GetLinkHtml(this.languageComponent1,((RealTimeInputOutputQuantity)obj).MOShiftOutputqty.ToString(),this.GetUrlParams(obj)),
								//((RealTimeInputOutputQuantity)obj).MOShiftOutputqty.ToString(),
								((RealTimeInputOutputQuantity)obj).MOOutputqty.ToString(),
								((RealTimeInputOutputQuantity)obj).MOScrapqty.ToString(),
								((RealTimeInputOutputQuantity)obj).MOOffQty.ToString()
							});
		}



		private void _processDataDourceToGrid(object[] source)
		{
			this.gridWebGrid.Rows.Clear();

			if( source != null )
			{				
				foreach(RealTimeInputOutputQuantity real in source)
				{
					this.gridWebGrid.Rows.Add( this.GetGridRow(real) );
				}
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

				this._processDataDourceToGrid( this._loadDataSource() );
			}
			else
			{
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
				return string.Empty;
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
				return string.Empty;
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
				return string.Empty;
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
						string script = 
							@"<script language='jscript' src='../Skin/js/selectAll.js'></script>
							<script language='javascript'>";

						script += string.Format(
							@"window.showModalDialog('./{0}','',showDialog(7));",
							this.MakeRedirectUrl(
							"FRealTimeQuantityDetails.asp",
							new string[]{
											"segmentcode",
											"shiftday",
											"shiftcode",
											"tpcode",
											"tpcodedetail",
											"stepsequencecode",
											"modelcode",
											"Itemcode",
											"mocode"
										},
							new string[]{
											this.drpSegmentQuery.SelectedValue,
											this.eMESDate1.Text,
											this.drpShiftQuery.SelectedValue,
											e.Cell.Column.Key,
											e.Cell.Column.HeaderText,
											e.Cell.Row.Cells[0].Text,
											e.Cell.Row.Cells.FromKey("ModelCode").Text,
											e.Cell.Row.Cells.FromKey("ItemCode").Text,
											e.Cell.Row.Cells.FromKey("MOCode").Text
										}));

						script += @"</script>";

						this.RegisterClientScriptBlock("details",script);
					}
				}
			}
			
		}

		protected void chkRefreshAuto_CheckedChanged(object sender, System.EventArgs e)
		{
			this._doQuery();
		}

		private void gridWebGrid_Click(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			if( e.Cell != null )
			{
				if( e.Cell.Column.Index > 0 && 
					e.Cell.Column.Index < this.gridWebGrid.Columns.Count-1 )
				{
					if(e.Cell.Text!=null && e.Cell.Text.Trim()!=string.Empty)
					{
						if(e.Cell.Key == "MOShiftInputqty")
						{
							//工单投入数(当班)

							if( !this.IsStartupScriptRegistered("inputdetails") )
							{
								string script = 
									@"<script language='jscript' src='../Skin/js/selectAll.js'></script>
														<script language='javascript'>";
							
								script += string.Format(
									@"window.showModalDialog('./{0}','',showDialog(7));",
									this.MakeRedirectUrl(
									"FRealTimeShiftInputDetails2.aspx",
									new string[]{
													"segmentcode",
													"shiftday",
													"shiftcode",
													"modelcode",
													"Itemcode",
													"mocode"
												},
									new string[]{
													this.drpSegmentQuery.SelectedValue,
													this.eMESDate1.Text,
													this.drpShiftQuery.SelectedValue,
													e.Cell.Row.Cells.FromKey("ModelCode").Text,
													e.Cell.Row.Cells.FromKey("ItemCode").Text,
													e.Cell.Row.Cells.FromKey("MOCode").Text
												}));
							
								script += @"</script>";
							
								this.RegisterClientScriptBlock("details",script);
							}
						
						}
						else if(e.Cell.Key == "MOShiftOutputqty")
						{
							//工单产出数(当班)

							if( !this.IsStartupScriptRegistered("outputdetails") )
							{
								string script = 
									@"<script language='jscript' src='../Skin/js/selectAll.js'></script>
														<script language='javascript'>";
							
								script += string.Format(
									@"window.showModalDialog('./{0}','',showDialog(7));",
									this.MakeRedirectUrl(
									"FRealTimeShiftOutputDetails2.aspx",
									new string[]{
													"segmentcode",
													"shiftday",
													"shiftcode",
													"modelcode",
													"Itemcode",
													"mocode"
												},
									new string[]{
													this.drpSegmentQuery.SelectedValue,
													this.eMESDate1.Text,
													this.drpShiftQuery.SelectedValue,
													e.Cell.Row.Cells[0].Text,
													string.Empty,
													this.txtMoQuery.Text
												}));
							
								script += @"</script>";
							
								this.RegisterClientScriptBlock("details",script);
							}
						}
						#region

						//						if( !this.IsStartupScriptRegistered("details") )
						//						{
						//							string script = 
						//								@"<script language='jscript' src='../Skin/js/selectAll.js'></script>
						//							<script language='javascript'>";
						//
						//							script += string.Format(
						//								@"window.showModalDialog('./{0}','',showDialog(7));",
						//								this.MakeRedirectUrl(
						//								"FRealTimeQuantityDetails.asp",
						//								new string[]{
						//												"segmentcode",
						//												"shiftday",
						//												"shiftcode",
						//												"tpcode",
						//												"tpcodedetail",
						//												"stepsequencecode",
						//												"modelcode",
						//												"Itemcode",
						//												"mocode"
						//											},
						//								new string[]{
						//												this.drpSegmentQuery.SelectedValue,
						//												this.eMESDate1.Text,
						//												this.drpShiftQuery.SelectedValue,
						//												e.Cell.Column.Key,
						//												e.Cell.Column.HeaderText,
						//												e.Cell.Row.Cells[0].Text,
						//												string.Empty,
						//												string.Empty,
						//												this.txtMoQuery.Text
						//											}));
						//
						//							script += @"</script>";
						//
						//							this.RegisterClientScriptBlock("details",script);
						//						}
						#endregion
					}
				}
			}
		}
		private string GetUrlParams(object obj)
		{
			RealTimeInputOutputQuantity rObj = (RealTimeInputOutputQuantity)obj;
			string returnStr = string.Format("{0}?post='true'&segmentcode={1}&shiftday={2}&shiftcode={3}&modelcode={4}&Itemcode={5}&mocode={6}",
													"FRealTimeQuantitySummaryQP2.aspx",
													this.drpSegmentQuery.SelectedValue,
													this.eMESDate1.Text,
													this.drpShiftQuery.SelectedValue,
													rObj.ModelCode,
													rObj.ItemCode,
													rObj.MOCode);
			return returnStr;
		}
	}
}
