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

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Domain.SMT;

namespace BenQGuru.eMES.Web.SMT
{
	/// <summary>
	/// FFeederMP 的摘要说明。
	/// </summary>
	public partial class FSMTMOMaterialQP : BaseMPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		
		private BenQGuru.eMES.SMT.SMTFacade _facade ;//= new SMTFacadeFactory().Create();
	
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
			this.gridHelper.AddColumn( "MOCode", "工单代码",	null);
			this.gridHelper.AddColumn( "sscode", "产线代码",	null);
			this.gridHelper.AddColumn( "MaterialCode", "物料代码",	null);
			this.gridHelper.AddColumn( "MachineStationCode", "站位编号",	null);
			this.gridHelper.AddColumn( "LogicalUsedQty", "理论用量",	null);
			this.gridHelper.AddColumn( "ActualUsedQty", "实际用量",	null);
			this.gridHelper.AddColumn( "MachineDiscardRate", "设备抛料率",	null);
			this.gridHelper.AddColumn( "MachineDiscardQty", "设备抛料数量",	null);
			this.gridHelper.AddColumn( "ManualDiscardRate", "人为抛料率",	null);
			this.gridHelper.AddColumn( "ManualDiscardQty", "人为抛料数量",	null);
			this.gridWebGrid.Columns.FromKey("LogicalUsedQty").Format = "#,#";
			this.gridWebGrid.Columns.FromKey("LogicalUsedQty").CellStyle.HorizontalAlign = HorizontalAlign.Right;
			this.gridWebGrid.Columns.FromKey("ActualUsedQty").Format = "#,#";
			this.gridWebGrid.Columns.FromKey("ActualUsedQty").CellStyle.HorizontalAlign = HorizontalAlign.Right;
			this.gridWebGrid.Columns.FromKey("MachineDiscardRate").CellStyle.HorizontalAlign = HorizontalAlign.Right;
			this.gridWebGrid.Columns.FromKey("MachineDiscardQty").Format = "#,#";
			this.gridWebGrid.Columns.FromKey("MachineDiscardQty").CellStyle.HorizontalAlign = HorizontalAlign.Right;
			this.gridWebGrid.Columns.FromKey("ManualDiscardRate").CellStyle.HorizontalAlign = HorizontalAlign.Right;
			this.gridWebGrid.Columns.FromKey("ManualDiscardQty").Format = "#,#";
			this.gridWebGrid.Columns.FromKey("ManualDiscardQty").CellStyle.HorizontalAlign = HorizontalAlign.Right;

			this.gridHelper.AddDefaultColumn( false, false );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			SMTRptMOMaterial item = (SMTRptMOMaterial)obj;
			Infragistics.WebUI.UltraWebGrid.UltraGridRow row = new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								item.MOCode,
								item.StepSequenceCode,
								item.MaterialCode,
								item.MachineStationCode,
								item.LogicalUsedQty,
								item.ActualUsedQty,
								Math.Round(item.MachineDiscardRate * 100, 2).ToString() + " %",
								item.MachineDiscardQty,
								Math.Round(item.ManualDiscardRate * 100, 2).ToString() + " %",
								item.ManualDiscardQty
							});
			
			return row;
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QueryMOMaterialRpt(
				this.txtMOCodeQuery.Text.Trim().ToUpper(),
				this.txtLineCodeQuery.Text.Trim().ToUpper(),
				this.txtMaterialCodeQuery.Text.Trim().ToUpper(),
				this.txtMachineStationCodeQuery.Text.Trim().ToUpper(),
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QueryMOMaterialRptCount( 
				this.txtMOCodeQuery.Text.Trim().ToUpper(),
				this.txtLineCodeQuery.Text.Trim().ToUpper(),
				this.txtMaterialCodeQuery.Text.Trim().ToUpper(),
				this.txtMachineStationCodeQuery.Text.Trim().ToUpper());
		}

		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			SMTRptMOMaterial item = (SMTRptMOMaterial)obj;
			return new string[]{
								   item.MOCode,
								   item.StepSequenceCode,
								   item.MaterialCode,
								   item.MachineStationCode,
								   item.LogicalUsedQty.ToString(),
								   item.ActualUsedQty.ToString(),
								   Math.Round(item.MachineDiscardRate * 100, 2).ToString() + " %",
								   item.MachineDiscardQty.ToString(),
								   Math.Round(item.ManualDiscardRate * 100, 2).ToString() + " %",
								   item.ManualDiscardQty.ToString() };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"MOCode",
									"sscode",
									"MaterialCode",
									"MachineStationCode",
									"LogicalUsedQty",
									"ActualUsedQty",
									"MachineDiscardRate",
									"MachineDiscardQty",
									"ManualDiscardRate",
									"ManualDiscardQty"};
		}
		#endregion


	}
}
