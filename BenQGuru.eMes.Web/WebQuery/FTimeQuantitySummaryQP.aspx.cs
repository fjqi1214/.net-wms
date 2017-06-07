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
	/// FTimeQuantitySummaryQP 的摘要说明。
	/// </summary>
	public partial class FTimeQuantitySummaryQP : BaseQPage
	{
		protected BenQGuru.eMES.Web.UserControl.eMESTime dateStartTimeQuery;
		
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Timers.Timer timerRefresh;
		protected BenQGuru.eMES.Web.Helper.RefreshController RefreshController1;

		protected GridHelper _gridHelper = null;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{		
			this._gridHelper = new GridHelper(this.gridWebGrid);
			this.gridWebGrid.DisplayLayout.CellClickActionDefault = CellClickAction.NotSet;

			if( !this.IsPostBack )
			{
				this.dateStartDateQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
//				this.dateEndDateQuery.Text = this.dateStartDateQuery.Text;
				this.dateStartTimeQuery.Text = FormatHelper.ToTimeString(FormatHelper.TOTimeInt(System.DateTime.Now)) ;
//				this.dateEndTimeQuery.Text = FormatHelper.ToTimeString(235959);

				this._initialWebGrid();			
			}	
			
			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				//如果接受到其它页面的参数直接执行查询
				if(this.GetRequestParam("post") != null && this.GetRequestParam("post") != string.Empty)	
				{
					//日期
					this.dateStartDateQuery.Text = this.GetRequestParam("shiftday");
					this.dateStartTimeQuery.Text = this.GetRequestParam("shiftday");
					this._doQuery();
				}
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

		private void _initialWebGrid()
		{
			this.gridWebGrid.Columns.Clear();

			this._gridHelper.AddColumn("StepSequenceCode","生产线",null,100);
			this._gridHelper.AddColumn("ItemCode","产品代码",null,100);
			this._gridHelper.AddColumn("OutputQuantity","产出数量",null,100);

			//多语言
			this._gridHelper.ApplyLanguage( this.languageComponent1 );

			string selected = "";
			GridItemStyle blueBack = new GridItemStyle(true);
			blueBack.BackColor = Color.SkyBlue;
			if( this.gridWebGrid.Columns.FromKey( selected ) != null )
			{
                this.gridWebGrid.Columns.FromKey(selected).HeaderStyle.BackColor = Color.SkyBlue;
			}						
		}

		private object[] _loadDataSource()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryTimeQuantity(
				this.V_StepSequenceCode,
				FormatHelper.TODateInt(this.V_StartDate),
				FormatHelper.TOTimeInt(this.V_StartTime));
		}


		private void _processDataDourceToGrid(object[] source)
		{
			this._initialWebGrid();

			this.gridWebGrid.Rows.Clear();

			if( source != null )
			{
				foreach(RealTimeQuantity real in source)
				{
					UltraGridRow gridRow = null;
					object[] objs = new object[this.gridWebGrid.Columns.Count];
					gridRow = new UltraGridRow( objs );
					this.gridWebGrid.Rows.Add( gridRow );
					gridRow.Cells.FromKey("StepSequenceCode").Text = real.StepSequenceCode;
					gridRow.Cells.FromKey("ItemCode").Text = real.ItemCode;
					gridRow.Cells.FromKey("OutputQuantity").Text = real.OutputQuantity.ToString();
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

//			manager.Add(new DateRangeCheck(this.lblStartDateQuery,this.dateStartDateQuery.Text,this.lblEndDateQuery,this.dateEndDateQuery.Text,true));
			
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
			this._initialWebGrid();
			this._processDataDourceToGrid( this._loadDataSource() );
		}

		#region ViewState

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

		private string V_StartDate
		{
			get
			{	
				return this.dateStartDateQuery.Text;
			}
			set
			{
				this.ViewState["V_StartDate"] = value;
			}
		}

		private string V_StartTime
		{
			get
			{	
				return this.dateStartTimeQuery.Text;
			}
			set
			{
				this.ViewState["V_StartTime"] = value;
			}
		}

//		private string V_EndDate
//		{
//			get
//			{	
//				return this.dateEndDateQuery.Text;
//			}
//			set
//			{
//				this.ViewState["V_EndDate"] = value;
//			}
//		}
//
//		private string V_EndTime
//		{
//			get
//			{	
//				return this.dateEndTimeQuery.Text;
//			}
//			set
//			{
//				this.ViewState["V_EndTime"] = value;
//			}
//		}
		#endregion

		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{	
			this._doQuery();
		}

		private void timerRefresh_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
		}

//		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
//		{
//			if( e.Cell.Column.Index > 0 && 
//				e.Cell.Column.Index < this.gridWebGrid.Columns.Count-1 &&
//				e.Cell.Row.Index != this.gridWebGrid.Rows.Count - 1 )
//			{
//				
//				if(e.Cell.Text!=null && e.Cell.Text.Trim()!=string.Empty)
//				{
//					if( !this.IsStartupScriptRegistered("details") )
//					{
//						bool needMidOutput = FormatHelper.CleanString(this.txtItemQuery.Text).Length == 0?true:false;
//						string script = 
//							@"<script language='jscript' src='../Skin/js/selectAll.js'></script>
//							<script language='javascript'>";
//
//						script += string.Format(
//							@"window.showModalDialog('./{0}','',showDialog(7));",
//							this.MakeRedirectUrl(
//							"FRealTimeQuantityDetails.aspx",
//							new string[]{
//											"segmentcode",
//											"shiftday",
//											"shiftcode",
//											"tpcode",
//											"tpcodedetail",
//											"stepsequencecode",
//											"modelcode",
//											"Itemcode",
//											"mocode",
//											"IncludeMidOutput"
//										},
//							new string[]{
//											this.drpSegmentQuery.SelectedValue,
//											this.eMESDate1.Text,
//											this.drpShiftQuery.SelectedValue,
//											e.Cell.Column.Key,
//											e.Cell.Column.HeaderText.Replace("<br>","~"),
//											e.Cell.Row.Cells.FromKey("StepSequenceCode").Text,
//											this.txtModelQuery.Text,
//											//this.txtItemQuery.Text,
//											e.Cell.Row.Cells.FromKey("ItemCode").Text,
//											this.txtMoQuery.Text,
//											needMidOutput.ToString()	
//										}));
//
//						script += @"</script>";
//
//						this.RegisterClientScriptBlock("details",script);
//					}
//				}
//			}
//			
//		}

		private void chkRefreshAuto_CheckedChanged(object sender, System.EventArgs e)
		{
			this._doQuery();
		}

//		private void gridWebGrid_Click(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
//		{
//			if( e.Cell != null )
//			{
//				if(e.Cell.Row.Cells.FromKey("InputOutputName").Text == string.Empty)return;
//				if(e.Cell.Key == "InputOutputName")return;
//				if(e.Cell.Key == "ItemCode")return; 
//				if( e.Cell.Column.Index > 0 && 
//					e.Cell.Column.Index < this.gridWebGrid.Columns.Count-1 &&
//					e.Cell.Row.Index != this.gridWebGrid.Rows.Count - 1 )
//				{
//					if(e.Cell.Text!=null && e.Cell.Text.Trim()!=string.Empty)
//					{
//						if( !this.IsStartupScriptRegistered("details") )
//						{
//							bool needMidOutput = FormatHelper.CleanString(this.txtItemQuery.Text).Length == 0?true:false;
//							string script = 
//								@"<script language='jscript' src='../Skin/js/selectAll.js'></script>
//							<script language='javascript'>";
//
//							script += string.Format(
//								@"window.showModalDialog('./{0}','',showDialog(7));",
//								this.MakeRedirectUrl(
//								"FRealTimeQuantityDetails.aspx",
//								new string[]{
//												"segmentcode",
//												"shiftday",
//												"shiftcode",
//												"tpcode",
//												"tpcodedetail",
//												"stepsequencecode",
//												"modelcode",
//												"Itemcode",
//												"mocode",
//												"IncludeMidOutput"
//											},
//								new string[]{
//												this.drpSegmentQuery.SelectedValue,
//												this.eMESDate1.Text,
//												this.drpShiftQuery.SelectedValue,
//												e.Cell.Column.Key,
//												e.Cell.Column.HeaderText.Replace("<br>","~"),
//												e.Cell.Row.Cells.FromKey("StepSequenceCode").Text,
//												this.txtModelQuery.Text,
//												//this.txtItemQuery.Text,
//												e.Cell.Row.Cells.FromKey("ItemCode").Text,
//												this.txtMoQuery.Text,
//												needMidOutput.ToString(),
//												
//							}));
//
//							script += @"</script>";
//
//							this.RegisterClientScriptBlock("details",script);
//						}
//					}
//				}
//			}
//		}

		protected void cmdGridExport2_ServerClick(object sender, EventArgs e)
		{
			this.GridExport(this.gridWebGrid);
		}
	}
}
