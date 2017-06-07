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
	/// ReportCenterYield 的摘要说明。
	/// </summary>
	public partial class ReportCenterYield : BaseRQPage
	{

		protected GridHelperForRPT _gridHelper = null;

		protected int today = FormatHelper.TODateInt(System.DateTime.Now);
		protected string opCode = "";
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this._gridHelper = new GridHelperForRPT(this.gridYield);

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
			this.gridYield.Columns.Clear();

			this._gridHelper.GridHelper.AddColumn("StepSequenceCode","产线",null);
			this._gridHelper.GridHelper.AddColumn("ItemCode","产品",null);
			this._gridHelper.GridHelper.AddColumn("ItemName","产品名称 ",null);
			this._gridHelper.GridHelper.AddColumn("ResCode","资源",null);
			this._gridHelper.GridHelper.AddColumn("DayPercent","良率",null);

			//多语言
			this._gridHelper.GridHelper.ApplyLanguage( this.languageComponent1 );

			string selected = "";
			GridItemStyle blueBack = new GridItemStyle(true);
			blueBack.BackColor = Color.SkyBlue;
			if( this.gridYield.Columns.FromKey( selected ) != null )
			{
                this.gridYield.Columns.FromKey(selected).HeaderStyle.BackColor = Color.SkyBlue;
			}
		}

		private object[] _loadDataSource()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRPTCenterYield(today,opCode);
		}

		private void _processDataDourceToGrid(object[] source)
		{
			this._initialGrid();

			this.gridYield.Rows.Clear();

			if( source != null )
			{
				foreach(RPTCenterDayYield real in source)
				{
					UltraGridRow gridRow = null;
					object[] objs = new object[this.gridYield.Columns.Count];
					gridRow = new UltraGridRow( objs );
					this.gridYield.Rows.Add( gridRow );
					gridRow.Cells.FromKey("StepSequenceCode").Text = real.StepSequenceCode;
					gridRow.Cells.FromKey("ItemCode").Text = real.ItemCode;
					gridRow.Cells.FromKey("ItemName").Text = real.ItemName;
					gridRow.Cells.FromKey("ResCode").Text = real.ResCode;
					if(real.DayPercent == 0)
					{
						gridRow.Cells.FromKey("DayPercent").Text = "0%";
					}
					else
					{
						gridRow.Cells.FromKey("DayPercent").Text = real.DayPercent.ToString("##.##%");
					}
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
				for(int col=1;col < this.gridYield.Columns.Count-1;col++)
				{			
					for(int row=0;row<this.gridYield.Rows.Count-1;row++)
					{
                        this.gridYield.Rows[row].Cells[col].Style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;			
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
	}
}
