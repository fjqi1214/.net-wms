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
	/// ReportCenterDayYield 的摘要说明。
	/// </summary>
	public partial class ReportCenterDayYield : BaseRQPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		protected GridHelperForRPT _gridHelper = null;

		protected int today = FormatHelper.TODateInt(System.DateTime.Now);
		protected string opCode = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this._gridHelper = new GridHelperForRPT(this.gridDayYield);
			this.gridDayYield.DisplayLayout.CellClickActionDefault = CellClickAction.NotSet;

			opCode = this.GetRequestParam("OPCode");

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
			this.gridDayYield.Click += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.gridDayYield_Click);
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
			this.gridDayYield.Columns.Clear();

			this._gridHelper.GridHelper.AddColumn("EcgCode","不良代码组",null);
			this._gridHelper.GridHelper.AddColumn("ECode","不良代码",null);
			this._gridHelper.GridHelper.AddColumn("Ecdesc","不良描述",null);
			this._gridHelper.GridHelper.AddColumn("Qty","数量",null);

			//多语言
			this._gridHelper.GridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridDayYield.Columns.FromKey("EcgCode").MergeCells = true;

			string selected = "";
			GridItemStyle blueBack = new GridItemStyle(true);
			blueBack.BackColor = Color.SkyBlue;
			if( this.gridDayYield.Columns.FromKey( selected ) != null )
			{
                this.gridDayYield.Columns.FromKey(selected).HeaderStyle.BackColor = Color.SkyBlue;
			}

			this.gridDayYield.Columns[3].CellStyle.Font.Underline = true;
			this.gridDayYield.Columns[3].CellStyle.ForeColor = Color.Blue;
		}

		private object[] _loadDataSource()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRPTCenterDayYield(today,opCode);
		}

		private void _processDataDourceToGrid(object[] source)
		{
			this._initialGrid();

			this.gridDayYield.Rows.Clear();

			if( source != null )
			{
				foreach(RPTCenterDayPercent real in source)
				{
					UltraGridRow gridRow = null;
					object[] objs = new object[this.gridDayYield.Columns.Count];
					gridRow = new UltraGridRow( objs );
					this.gridDayYield.Rows.Add( gridRow );
					gridRow.Cells.FromKey("ECode").Text = real.ECode;
					gridRow.Cells.FromKey("EcgCode").Text = real.EcgCode;
					gridRow.Cells.FromKey("Ecdesc").Text = real.Ecdesc;
					gridRow.Cells.FromKey("Qty").Text = real.Qty.ToString();
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
				for(int col=3;col < this.gridDayYield.Columns.Count;col++)
				{			
					for(int row=0;row<this.gridDayYield.Rows.Count;row++)
					{
                        this.gridDayYield.Rows[row].Cells[col].Style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
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

		private void gridDayYield_Click(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			if( e.Cell.Column.Key.ToUpper() == "Qty".ToUpper() )
			{
				this.Response.Redirect(
					this.MakeRedirectUrl(
					"FReportCenterDayProduct.aspx",
					new string[]{"OPCode","ECode"},
					new string[]{opCode,e.Cell.Row.Cells[1].Text})
					);
			}
		}
	}
}
