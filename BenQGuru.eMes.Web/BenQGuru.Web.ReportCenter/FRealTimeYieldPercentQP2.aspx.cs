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
	/// FRealTimeYieldPercentQP 的摘要说明。
	/// </summary>
	public partial class FRealTimeYieldPercentQP2 : BaseRQPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		
		protected GridHelperForRPT _gridHelper = null;
		protected System.Timers.Timer timerRefresh;
		protected System.Web.UI.WebControls.Label Label1;
		public BenQGuru.eMES.Web.UserControl.eMESDate eMESDate1;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{		
			this._gridHelper = new GridHelperForRPT(this.gridWebGrid);	

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

					this._initialWebGrid();

					this.RefreshController1.Interval = ConfigSection.Current.DomainSetting.Interval;

					this.txtTargetPercent.Attributes.Add("onblur","Format()");
				}

				factory_load();
			}

			if( this.V_StartRefresh )
			{
				this._doQuery();
			}
		}


		private void _initialWebGrid()
		{
			this.gridWebGrid.Columns.Clear();
			this.gridWebGrid.Rows.Clear();

			this._gridHelper.GridHelper.AddColumn("StepSequenceCode","生产线",null);

						
			if( this.txtStepSequence.Text != "" )
			{
				string stepSeqs = this.txtStepSequence.Text.Trim().ToUpper();
				string[] stepSeqList = stepSeqs.Split(new char[]{',',';'});	
				if( stepSeqList != null )
				{
					foreach(string stepSeq in stepSeqList)
					{
						this._gridHelper.GridHelper.AddColumn(stepSeq,stepSeq,null);
					}
				}
			}
			else
			{
				BaseModelFacade facade = new FacadeFactory(base.DataProvider).CreateBaseModelFacade();					
				object[] stepSeqs = facade.QueryStepSequence("",this.drpSegmentQuery.SelectedValue,0,System.Int32.MaxValue);
				if( stepSeqs != null && stepSeqs.Length >0)
				{
					for(int i=0;i<stepSeqs.Length;i++)
					{
						this._gridHelper.GridHelper.AddColumn(((StepSequence)stepSeqs[i]).StepSequenceCode,((StepSequence)stepSeqs[i]).StepSequenceCode,null);
					}
				}
			}

			string[] rows = new string[]{"AllGoodQuantity","Quantity","AllGoodYieldPercent"};
			object[] objs = new object[this.gridWebGrid.Columns.Count];
			UltraGridRow row = null;
			foreach(string cell in rows)
			{
				row = new UltraGridRow(objs);
				string text = this.languageComponent1.GetString(cell);
				if( text == "" )
				{
					text = cell;
				}
				row.Cells[0].Key = cell;
				row.Cells[0].Text = text;
				this.gridWebGrid.Rows.Add( row );
			}

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
			this.timerRefresh.Enabled = true;
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
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRealTimeYieldPercent(
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

			if( source != null )
			{
				foreach(RealTimeYieldPercent real in source)
				{		
					try
					{
						foreach(UltraGridRow row in this.gridWebGrid.Rows)
						{
							if( row.Cells[0].Key.ToUpper() == "AllGoodQuantity".ToUpper() )
							{
								row.Cells.FromKey(real.StepSequenceCode).Text = real.AllGoodQuantity.ToString();
							}
							if( row.Cells[0].Key.ToUpper() == "Quantity".ToUpper() )
							{
								row.Cells.FromKey(real.StepSequenceCode).Text = real.Quantity.ToString();
							}
							if( row.Cells[0].Key.ToUpper() == "AllGoodYieldPercent".ToUpper() )
							{
								row.Cells.FromKey(real.StepSequenceCode).Text = (real.AllGoodYieldPercent >= 0 ? System.Decimal.Round(real.AllGoodYieldPercent*100,2).ToString() + "%" : "-1" );						
								row.Cells.FromKey(real.StepSequenceCode).Key = (real.AllGoodYieldPercent >= 0 ? System.Decimal.Round(real.AllGoodYieldPercent*100,2).ToString() : "-1" );
							}
						}
					}
					catch
					{
					}
				}
			}
		}

		private void _processDataDourceToChart(object[] source)
		{	
			this.OWCChartSpace1.ClearCharts();
			if( source != null )
			{
				string seriesName = "First";
				string[] categories = new string[this.gridWebGrid.Columns.Count-1];
				object[] values = new object[categories.Length];
				for(int i = 1;i<this.gridWebGrid.Columns.Count;i++)
				{
					categories[i-1] = (this.gridWebGrid.Columns[i] as UltraGridColumn).Key;				
					object percent = 0;
					try
					{
						percent = System.Decimal.Parse(this.gridWebGrid.Rows[2].Cells[i].Key.ToString());
					}
					catch
					{
						percent = 0;
					}
					values[i-1] = percent;
				}

				//target percent
				object[] targets = new object[values.Length];
				for(int j = 0;j < values.Length;j++)
				{
					targets[j] = this.V_TargetPercent;
				}

				this.OWCChartSpace1.AddChart("", categories, targets,OWCChartType.Line,true,this.V_TargetPercent );	
				this.OWCChartSpace1.AddChart(seriesName, categories, values );	
			
				this.OWCChartSpace1.Display = true;
			}
		}
	

		private bool _checkRequireFields()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(this.lblSegment,this.drpSegmentQuery,40,true) );
			manager.Add( new LengthCheck(this.lblShip,this.drpShiftQuery,40,true) );
			manager.Add( new DateCheck(this.lblDate,this.eMESDate1.Text,true) );
			manager.Add( new DecimalCheck(this.lblTargetPercent,this.txtTargetPercent,0,100,true) );
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

		private void _processSegment()
		{
			object[] obj = new FacadeFactory(base.DataProvider).CreateQueryFacade1().QuerySegmentRealTimeYieldPercent(
				this.V_SegmentCode,
				this.V_ModelCode,
				this.V_ItemCode,
				this.V_MoCode,
				this.V_ShiftCode,
				FormatHelper.TODateInt(this.V_Date));

			if( obj != null )
			{
				RealTimeYieldPercent segYield = obj[0] as RealTimeYieldPercent;
				this.txtSegmentAllGoodQuantity.Text = segYield.AllGoodQuantity.ToString();
				this.txtSegmentQuantity.Text = segYield.Quantity.ToString();
				this.txtSegmentNotYieldPercent.Text = 
					System.Decimal.Round( System.Decimal.Parse(segYield.AllGoodYieldPercent.ToString())*100,2).ToString() + "%";
			}
			else
			{
				this.txtSegmentAllGoodQuantity.Text = "";
				this.txtSegmentQuantity.Text = "";
				this.txtSegmentNotYieldPercent.Text = "";
			}
		}

		private void _doQuery()
		{			
			if( this._checkRequireFields() )
			{
				object[] source = this._loadDataSource();
				
				this._initialWebGrid();

				this._processDataDourceToGrid( source );

				this._processDataDourceToChart( source );

				//segment yield
				this._processSegment();

				this.V_StartRefresh = this.chbRefreshAuto.Checked;
			}
			else
			{
//				this.RefreshController1.Stop();
				this.V_StartRefresh = false;
			}				
		}



		#region ViewState
		private Decimal V_TargetPercent
		{
			get
			{				
				try
				{
					return System.Decimal.Parse(this.txtTargetPercent.Text);
				}
				catch
				{
					return 100;
				}
			}
			set
			{
				this.ViewState["V_TargetPercent"] = value;
			}
		}

		private string V_SegmentCode
		{
			get
			{		
				return this.drpSegmentQuery.SelectedValue;
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
//			if( this.V_StartRefresh )
//			{
//				this.Response.AddHeader("Refresh","2");
//			}
		}

		protected void chkRefreshAuto_CheckedChanged(object sender, System.EventArgs e)
		{
			this._doQuery();
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
	}
}