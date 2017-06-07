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

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// ReportCenterLine 的摘要说明。
	/// </summary>
	public partial class ReportCenterLine : BaseRQPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		protected GridHelperForRPT _gridHelper = null;

		protected int today = FormatHelper.TODateInt(System.DateTime.Now);
		protected string segmentCode = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this._gridHelper = new GridHelperForRPT(this.gridLine);
			this.gridLine.DisplayLayout.CellClickActionDefault = CellClickAction.NotSet;
			segmentCode = this.GetRequestParam("SegmentCode");

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialGrid();
				this._processDataDourceToGrid( this._loadDataSource() );
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
			this.gridLine.Click += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.gridLine_Click);
			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";

		}
		#endregion

		private void _initialGrid()
		{
			this.gridLine.Columns.Clear();

			this._gridHelper.GridHelper.AddColumn("StepSequenceCode","产线",null);
			this._gridHelper.GridHelper.AddColumn("DayQuantity","本日产量",null);
			this._gridHelper.GridHelper.AddColumn("WeekQuantity","本周累计",null);
			this._gridHelper.GridHelper.AddColumn("MonthQuantity","本月累计",null);

			//多语言
			this._gridHelper.GridHelper.ApplyLanguage( this.languageComponent1 );

			string selected = "";
			GridItemStyle blueBack = new GridItemStyle(true);
			blueBack.BackColor = Color.SkyBlue;
			if( this.gridLine.Columns.FromKey( selected ) != null )
			{
                this.gridLine.Columns.FromKey(selected).HeaderStyle.BackColor = Color.SkyBlue;
			}

			this.gridLine.Columns[1].CellStyle.Font.Underline = true;
			this.gridLine.Columns[1].CellStyle.ForeColor = Color.Blue;
			this.gridLine.Columns[2].CellStyle.Font.Underline = true;
			this.gridLine.Columns[2].CellStyle.ForeColor = Color.Blue;
			this.gridLine.Columns[3].CellStyle.Font.Underline = true;
			this.gridLine.Columns[3].CellStyle.ForeColor = Color.Blue;
		}

		private object[] _loadDataSource()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRPTCenterLine(today,segmentCode);
		}

		private void _processDataDourceToGrid(object[] source)
		{
			this._initialGrid();

			this.gridLine.Rows.Clear();

			if( source != null )
			{
				foreach(RPTCenterLine real in source)
				{
					UltraGridRow gridRow = null;
					object[] objs = new object[this.gridLine.Columns.Count];
					gridRow = new UltraGridRow( objs );
					this.gridLine.Rows.Add( gridRow );
					gridRow.Cells.FromKey("StepSequenceCode").Text = real.StepSequenceCode;
					gridRow.Cells.FromKey("DayQuantity").Text = real.DayQuantity.ToString();
					gridRow.Cells.FromKey("WeekQuantity").Text = real.WeekQuantity.ToString();
					gridRow.Cells.FromKey("MonthQuantity").Text = real.MonthQuantity.ToString();
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
				for(int col=1;col < this.gridLine.Columns.Count;col++)
				{			
					for(int row=0;row<this.gridLine.Rows.Count;row++)
					{
                        this.gridLine.Rows[row].Cells[col].Style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;			
					}
				}
			}
			catch
			{
			}
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect("ReportCenter.aspx");
		}

		private void gridLine_Click(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			if( e.Cell.Column.Key.ToUpper() == "DayQuantity".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"FReportCenterMocode.aspx",
					new string[]{"SegmentCode","StepSequenceCode"},
					new string[]{segmentCode,e.Cell.Row.Cells[0].Text})
					);
			}
			else if( e.Cell.Column.Key.ToUpper() == "WeekQuantity".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"FReportCenterWeekMocode.aspx",
					new string[]{"SegmentCode","StepSequenceCode"},
					new string[]{segmentCode,e.Cell.Row.Cells[0].Text})
					);
			}
			else if( e.Cell.Column.Key.ToUpper() == "MonthQuantity".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"FReportCenterMonthMocode.aspx",
					new string[]{"SegmentCode","StepSequenceCode"},
					new string[]{segmentCode,e.Cell.Row.Cells[0].Text})
					);
			}
		}
	}
}