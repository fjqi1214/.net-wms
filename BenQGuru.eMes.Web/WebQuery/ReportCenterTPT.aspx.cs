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
	/// ReportCenterTPT 的摘要说明。
	/// </summary>
	public partial class ReportCenterTPT : BaseRQPage
	{

		protected GridHelperForRPT _gridHelper = null;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		protected int today = FormatHelper.TODateInt(System.DateTime.Now);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this._gridHelper = new GridHelperForRPT(this.gridTPT);

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
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";

		}
		#endregion

		private void _initialGrid()
		{
			this.gridTPT.Columns.Clear();

			this._gridHelper.GridHelper. AddColumn("Mo_MOCode","工单代码",null);
			this._gridHelper.GridHelper.AddColumn("Mo_ItemCode","产品",null);
			this._gridHelper.GridHelper.AddColumn("Mo_StartDate","投产日期",null);
			this._gridHelper.GridHelper.AddColumn("Mo_PlanEndDate","预计完成日期",null);
			this._gridHelper.GridHelper.AddColumn("Mo_EndDate","关单日期",null);
			this._gridHelper.GridHelper.AddColumn("Mo_DateNum","生产天数",null);
			this._gridHelper.GridHelper.AddColumn("Mo_OverDateNum","超计划天数",null);
			this._gridHelper.GridHelper.AddColumn("Mo_Estate","状态",null);

			//多语言
			this._gridHelper.GridHelper.ApplyLanguage( this.languageComponent1 );

			string selected = "";
			GridItemStyle blueBack = new GridItemStyle(true);
			blueBack.BackColor = Color.SkyBlue;
			if( this.gridTPT.Columns.FromKey( selected ) != null )
			{
                this.gridTPT.Columns.FromKey(selected).HeaderStyle.BackColor = Color.SkyBlue;
			}						
		}

		private object[] _loadDataSource()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRPTCenterTPT(today);
		}

		private void _processDataDourceToGrid(object[] source)
		{
			this._initialGrid();

			this.gridTPT.Rows.Clear();

			if( source != null )
			{
				foreach(RPTCenterTPT real in source)
				{
					UltraGridRow gridRow = null;
					object[] objs = new object[this.gridTPT.Columns.Count];
					gridRow = new UltraGridRow( objs );
					this.gridTPT.Rows.Add( gridRow );
					gridRow.Cells.FromKey("Mo_MOCode").Text = real.Mo_MOCode;
					gridRow.Cells.FromKey("Mo_ItemCode").Text = real.Mo_ItemCode;
					gridRow.Cells.FromKey("Mo_StartDate").Text = real.Mo_StartDate.ToString("####/##/##");
					gridRow.Cells.FromKey("Mo_PlanEndDate").Text = real.Mo_PlanEndDate.ToString("####/##/##");
					if( real.Mo_Estate == "强制完工" )
					{
						gridRow.Cells.FromKey("Mo_EndDate").Text = real.Mo_EndDate.ToString("####/##/##");
					}
					else
					{
						gridRow.Cells.FromKey("Mo_EndDate").Text = "";
					}
					gridRow.Cells.FromKey("Mo_DateNum").Text = real.Mo_DateNum.ToString();
					gridRow.Cells.FromKey("Mo_OverDateNum").Text = real.Mo_OverDateNum.ToString();
					gridRow.Cells.FromKey("Mo_Estate").Text = real.Mo_Estate;
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
				for(int col=1;col < this.gridTPT.Columns.Count-1;col++)
				{			
					for(int row=0;row<this.gridTPT.Rows.Count-1;row++)
					{
//						this.gridTPT.Rows[row].Cells[col].Style = style;					
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
