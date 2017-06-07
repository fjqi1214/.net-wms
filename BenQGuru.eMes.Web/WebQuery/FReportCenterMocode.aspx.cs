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
	/// ReportCenterMocode 的摘要说明。
	/// </summary>
	public partial class FReportCenterMocode : BaseRQPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		protected GridHelperForRPT _gridHelper = null;

		protected int today = FormatHelper.TODateInt(System.DateTime.Now);
		protected string segmentCode = "";
		protected string stepSequenceCode = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this._gridHelper = new GridHelperForRPT(this.gridMocode);
			this.gridMocode.DisplayLayout.CellClickActionDefault = CellClickAction.NotSet;
			segmentCode = this.GetRequestParam("SegmentCode");
			stepSequenceCode = this.GetRequestParam("StepSequenceCode");

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

		private void _initialGrid()
		{
			this.gridMocode.Columns.Clear();

			this._gridHelper.GridHelper.AddColumn("StepSequenceCode","产线",null);
			this._gridHelper.GridHelper.AddColumn("MoCode","工单",null);
			this._gridHelper.GridHelper.AddColumn("ItemCode","产品",null);
			this._gridHelper.GridHelper.AddColumn("ItemName","产品名称",null);
			this._gridHelper.GridHelper.AddColumn("DayQuantity","当日产量",null);
			this._gridHelper.GridHelper.AddColumn("MOPlanQTY","工单计划",null);
			this._gridHelper.GridHelper.AddColumn("MOActQTY","工单累计",null);

			//多语言
			this._gridHelper.GridHelper.ApplyLanguage( this.languageComponent1 );

			string selected = "";
			GridItemStyle blueBack = new GridItemStyle(true);
			blueBack.BackColor = Color.SkyBlue;
			if( this.gridMocode.Columns.FromKey( selected ) != null )
			{
                this.gridMocode.Columns.FromKey(selected).HeaderStyle.BackColor = Color.SkyBlue;
			}
			//melo zheng 修改于2007.1.4 去除链接
//			this.gridMocode.Columns[4].CellStyle.Font.Underline = true;
//			this.gridMocode.Columns[4].CellStyle.ForeColor = Color.Blue;
		}

		private object[] _loadDataSource()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRPTCenterMocode(today,segmentCode,stepSequenceCode);
		}

		private void _processDataDourceToGrid(object[] source)
		{
			this._initialGrid();

			this.gridMocode.Rows.Clear();

			if( source != null )
			{
				foreach(RPTCenterMocode real in source)
				{
					UltraGridRow gridRow = null;
					object[] objs = new object[this.gridMocode.Columns.Count];
					gridRow = new UltraGridRow( objs );
					this.gridMocode.Rows.Add( gridRow );
					gridRow.Cells.FromKey("StepSequenceCode").Text = real.StepSequenceCode;
					gridRow.Cells.FromKey("MoCode").Text = real.MoCode;
					gridRow.Cells.FromKey("ItemCode").Text = real.ItemCode;
					gridRow.Cells.FromKey("ItemName").Text = real.ItemName;
					gridRow.Cells.FromKey("DayQuantity").Text = real.DayQuantity.ToString();
					gridRow.Cells.FromKey("MOPlanQTY").Text = real.PlanQTY.ToString();
					gridRow.Cells.FromKey("MOActQTY").Text = real.ActQTY.ToString();
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
				for(int row=0;row<this.gridMocode.Rows.Count;row++)
				{
					//melo zheng 修改于2007.1.4 去除链接
//					this.gridMocode.Rows[row].Cells[4].Style = style;
				}
			}
			catch
			{
			}
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(
				this.MakeRedirectUrl(
				"ReportCenterLine.aspx",
				new string[]{"SegmentCode"},
				new string[]{segmentCode})
				);
		}

		private void gridMocode_Click(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			if( e.Cell.Column.Key.ToUpper() == "DayQuantity".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"FReportCenterResCode.aspx",
					new string[]{"SegmentCode","StepSequenceCode","MoCode","ItemCode"},
					new string[]{segmentCode,e.Cell.Row.Cells[0].Text,e.Cell.Row.Cells[1].Text,e.Cell.Row.Cells[2].Text})
					);
			}
		}
	}
}
