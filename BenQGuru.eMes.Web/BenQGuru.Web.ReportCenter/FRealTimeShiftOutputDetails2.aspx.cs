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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;


namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FRealTimeShiftOutputDetails 的摘要说明。
	/// </summary>
	public partial class FRealTimeShiftOutputDetails2  : BaseRQPage
	{
		protected BenQGuru.eMES.Web.Helper.RefreshController RefreshController1;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdQuery;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		public BenQGuru.eMES.Web.UserControl.eMESDate eMESDate1;

		private GridHelperForRPT _gridHelper = null;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent2;
		private WebQueryHelper _helper = null;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this._gridHelper = new GridHelperForRPT( this.gridWebGrid );
			this._helper = new WebQueryHelper(null,null,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1 );
			//this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			//this._helper.LoadGridDataSource +=new EventHandler(_processDataDourceToGrid);
			//this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this.pagerSizeSelector.Readonly = true;
			this.eMESDate1.Enable = "false";

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.eMESDate1.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
				
				this._initialQueryCondtion();

				this._initialWebGrid();

				this._processDataDourceToGrid();
				//this._helper.Query( sender );
			}
		}

		private void _initialQueryCondtion()
		{
						this.txtSegmentQuery.Text = this.GetRequestParam("segmentcode");	//工段
						this.eMESDate1.Text = this.GetRequestParam("shiftday");				//日期
						this.txtShiftCodeQuery.Text = this.GetRequestParam("shiftcode");	//班次
						this.txtModelCodeQuery.Text = this.GetRequestParam("modelcode");	//产品别
						this.txtItemCodeQuery.Text = this.GetRequestParam("itemcode");		//产品
						this.txtMoCodeQuery.Text = this.GetRequestParam("mocode");			//工单

			#region Test

//			this.txtSegmentQuery.Text = "SMT160";		//工段
//			this.eMESDate1.Text = "2005/10/20";			//日期
//			this.txtShiftCodeQuery.Text = "2S_SMT_D2";	//班次
//			this.txtModelCodeQuery.Text = "";			//产品别
//			this.txtItemCodeQuery.Text = "";			//产品
//			this.txtMoCodeQuery.Text = "";				//工单

			#endregion
		}

		private void _initialWebGrid()
		{
			this.gridWebGrid.Columns.Clear();

			this._gridHelper.GridHelper.AddColumn("StepSequenceCode","生产线",null);
			string selected = "";
			if( this.txtShiftCodeQuery.Text == "" )
			{
				this._gridHelper.GridHelper.AddColumn("TPCode","时段代码",null);
			}
			else
			{	
				object[] tps = new FacadeFactory(base.DataProvider).CreateShfitModelFacade().QueryTimePeriod("",this.txtShiftCodeQuery.Text,0,System.Int32.MaxValue);
				if( tps != null )
				{
					int now = FormatHelper.TOTimeInt(System.DateTime.Now);
					foreach(TimePeriod tp in tps)
					{
						string key = tp.TimePeriodCode;

						this.gridWebGrid.Bands[0].Columns.Add(key);
						this.gridWebGrid.Bands[0].Columns.FromKey(key).HeaderText						= FormatHelper.ToTimeString(tp.TimePeriodBeginTime) + "~" + FormatHelper.ToTimeString(tp.TimePeriodEndTime);
						this.gridWebGrid.Bands[0].Columns.FromKey(key).Width							= new System.Web.UI.WebControls.Unit(150);
						this.gridWebGrid.Bands[0].Columns.FromKey(key).CellButtonStyle.BackColor		= System.Drawing.Color.Transparent;
						this.gridWebGrid.Bands[0].Columns.FromKey(key).CellButtonStyle.BorderStyle		= System.Web.UI.WebControls.BorderStyle.None;
						this.gridWebGrid.Bands[0].Columns.FromKey(key).CellButtonStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;				
						this.gridWebGrid.Bands[0].Columns.FromKey(key).CellButtonStyle.CustomRules		="BACKGROUND-POSITION: center center;Background-repeat:no-repeat";
						this.gridWebGrid.Bands[0].Columns.FromKey(key).CellButtonStyle.BackgroundImage	= "";
						this.gridWebGrid.Bands[0].Columns.FromKey(key).CellStyle.CustomRules		="BACKGROUND-POSITION: center center;Background-repeat:no-repeat";

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
			this._gridHelper.GridHelper.AddColumn("Summary","汇总",null);

			GridItemStyle blueBack = new GridItemStyle(true);
			blueBack.BackColor = Color.SkyBlue;
			if( this.gridWebGrid.Columns.FromKey( selected ) != null )
			{
                this.gridWebGrid.Columns.FromKey(selected).HeaderStyle.BackColor = Color.SkyBlue;
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
			this.languageComponent2 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";
			// 
			// languageComponent2
			// 
			this.languageComponent2.Language = "CHS";
			this.languageComponent2.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
			this.languageComponent2.RuntimePage = null;
			this.languageComponent2.RuntimeUserControl = null;
			this.languageComponent2.UserControlName = "";

		}
		#endregion

		private object[] _loadDataSource()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRealTimeOutputQuantity(
				this.txtSegmentQuery.Text,
				this.txtModelCodeQuery.Text,
				this.txtItemCodeQuery.Text,
				this.txtMoCodeQuery.Text,
				this.txtShiftCodeQuery.Text,
				FormatHelper.TODateInt(this.eMESDate1.Text));
		}


		private void _processDataDourceToGrid()//(object sender, EventArgs e)
		{
			this._initialWebGrid();

			this.gridWebGrid.Rows.Clear();

			object[] source = this._loadDataSource();
			if( source != null )
			{				
				foreach(RealTimeOutputQuantity real in source)
				{
					bool isAdded = false;

					UltraGridRow gridRow = null;
					foreach(UltraGridRow row in this.gridWebGrid.Rows)
					{
						if( row.Cells[0].Text.ToUpper() == real.StepSequenceCode.ToUpper() )
						{
							gridRow = row;
							isAdded = true;
							break;
						}
					}
					if( !isAdded || gridRow == null)
					{
						object[] objs = new object[this.gridWebGrid.Columns.Count];
						gridRow = new UltraGridRow( objs );
					}
					this.gridWebGrid.Rows.Add( gridRow );
					gridRow.Cells.FromKey("StepSequenceCode").Text = real.StepSequenceCode;

					foreach(UltraGridColumn col in this.gridWebGrid.Columns)
					{
						if( col.Key.ToUpper() == real.TimePeriodCode.ToUpper() )
						{
							gridRow.Cells.FromKey(col.Key).Text = real.Quantity.ToString();
						
							decimal summary = 0;
							try
							{
								summary = System.Decimal.Parse(
									gridRow.Cells.FromKey("Summary").Text);
							}
							catch
							{
								summary = 0;
							}
							gridRow.Cells.FromKey("Summary").Text = (summary + real.Quantity).ToString();
							break;
						}
					}				
				}
				UltraGridRow summaryRow = new UltraGridRow();
				this.gridWebGrid.Rows.Add( summaryRow );
				summaryRow.Cells.FromKey("StepSequenceCode").Text = this.languageComponent1.GetString("Summary");
				for(int i=1;i<this.gridWebGrid.Columns.Count;i++)
				{
					decimal summary = 0;
					for(int j=0;j<this.gridWebGrid.Rows.Count-1;j++)
					{
						try
						{
							summary += System.Decimal.Parse( this.gridWebGrid.Rows[j].Cells[i].Text);
						}
						catch
						{
							summary += 0;
						}
					}
					summaryRow.Cells[i].Text = summary.ToString();
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
	}
}
