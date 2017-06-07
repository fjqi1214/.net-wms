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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.Domain.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.SMT
{
	/// <summary>
	/// FReelMP 的摘要说明。
	/// </summary>
	public partial class FSMTMOLineQtyQP : BaseMPageNew
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdAdd;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdSave;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdCancel;
		
		private BenQGuru.eMES.SMT.SMTFacade _facade ;//= new SMTFacadeFactory(base.DataProvider).Create();
	
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
			//this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
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

		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
			}
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}
		#endregion

		#region WebGrid
		protected override void InitWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddColumn( "MOCode", "工单代码",	null);
			this.gridHelper.AddColumn( "ItemCode", "产品代码",	null);
            this.gridHelper.AddColumn("PlanQty", "计划产量", HorizontalAlign.Right);
            this.gridHelper.AddColumn("PlanManHour", "计划工时", HorizontalAlign.Right);
            this.gridHelper.AddColumn("CurrentQty", "目标产量", HorizontalAlign.Right);
            this.gridHelper.AddColumn("ActualManHour", "已用工时", HorizontalAlign.Right);
            this.gridHelper.AddColumn("ActualQty", "实际产量", HorizontalAlign.Right);
            this.gridHelper.AddColumn("DifferenceQty", "差异数量", HorizontalAlign.Right);
            this.gridHelper.AddColumn("MOComPassRate", "工单达成率", HorizontalAlign.Right);
			this.gridHelper.AddLinkColumn( "TimePeriodQtyDetail", "时段明细", null);
            //this.gridWebGrid.Columns.FromKey("PlanQty").Format = "#,#";
            //this.gridWebGrid.Columns.FromKey("PlanQty").CellStyle.HorizontalAlign = HorizontalAlign.Right;
            //this.gridWebGrid.Columns.FromKey("PlanManHour").Format = "#,#";
            //this.gridWebGrid.Columns.FromKey("PlanManHour").CellStyle.HorizontalAlign = HorizontalAlign.Right;
            //this.gridWebGrid.Columns.FromKey("CurrentQty").Format = "#,#";
            //this.gridWebGrid.Columns.FromKey("CurrentQty").CellStyle.HorizontalAlign = HorizontalAlign.Right;
            //this.gridWebGrid.Columns.FromKey("ActualManHour").Format = "#,#";
            //this.gridWebGrid.Columns.FromKey("ActualManHour").CellStyle.HorizontalAlign = HorizontalAlign.Right;
            //this.gridWebGrid.Columns.FromKey("ActualQty").Format = "#,#";
            //this.gridWebGrid.Columns.FromKey("ActualQty").CellStyle.HorizontalAlign = HorizontalAlign.Right;
            //this.gridWebGrid.Columns.FromKey("DifferenceQty").Format = "#,#";

			this.gridHelper.AddDefaultColumn( false, false );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override DataRow GetGridRow(object obj)
		{
			SMTRptLineQtyMO qty = (SMTRptLineQtyMO)obj;
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{
            //                    qty.MOCode,
            //                    qty.ProductCode,
            //                    qty.PlanQty,
            //                    qty.PlanManHour,
            //                    qty.CurrentQty,
            //                    qty.ActualManHour,
            //                    qty.ActualQty,
            //                    qty.DifferenceQty,
            //                    Math.Round(qty.MOComPassRate * 100, 2).ToString() + " %",
            //                    ""
            //                });
            DataRow row = this.DtSource.NewRow();
            row["MOCode"] = qty.MOCode;
            row["ItemCode"] = qty.ProductCode;
            row["PlanQty"] = String.Format("{0:#,#}",qty.PlanQty);
            row["PlanManHour"] = String.Format("{0:#,#}",qty.PlanManHour);
            row["CurrentQty"] = String.Format("{0:#,#}",qty.CurrentQty);
            row["ActualManHour"] = String.Format("{0:#,#}",qty.ActualManHour);
            row["ActualQty"] = String.Format("{0:#,#}",qty.ActualQty);
            row["DifferenceQty"] = String.Format("{0:#,#}",qty.DifferenceQty);
            row["MOComPassRate"] = Math.Round(qty.MOComPassRate * 100, 2).ToString() + " %";
            return row;
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QueryRptMOActualQty( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSCodeQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)));
		}


		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QueryRptMOActualQtyCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSCodeQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text )));
		}

        protected override void Grid_ClickCell(GridRecord row, string commandName)
		{
            if (commandName == "TimePeriodQtyDetail")
			{
				string strUrl = this.MakeRedirectUrl("FSMTMOLineQtyTPQP.aspx", 
					new string[]{"mocode"},
					new string[]{row.Items.FindItemByKey("MOCode").Text.ToString().Trim()}
					);
				this.Response.Redirect(strUrl);
			}
		}

		#endregion

		#region Button
		protected override void AddDomainObject(object domainObject)
		{		
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
		}

		protected override void UpdateDomainObject(object domainObject)
		{
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			
		}
		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			return null;
		}


		protected override object GetEditObject(GridRecord row)
		{	
			return null;
		}

		protected override void SetEditObject(object obj)
		{
			
		}

		
		protected override bool ValidateInput()
		{
            return true ;
		}

		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			SMTRptLineQtyMO qty = (SMTRptLineQtyMO)obj;
			return new string[]{ qty.MOCode,
								   qty.ProductCode,
								   qty.PlanQty.ToString(),
								   qty.PlanManHour.ToString(),
								   qty.CurrentQty.ToString(),
								   qty.ActualManHour.ToString(),
								   qty.ActualQty.ToString(),
								   qty.DifferenceQty.ToString(),
								   Math.Round(qty.MOComPassRate * 100, 2).ToString() + " %"
							   };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	"MOCode",
									"ItemCode",
									"PlanQty",
									"PlanManHour",	
									"CurrentQty",
									"ActualManHour",
									"ActualQty",
									"DifferenceQty",
									"MOComPassRate" 
								};
		}
		#endregion
	}
}
