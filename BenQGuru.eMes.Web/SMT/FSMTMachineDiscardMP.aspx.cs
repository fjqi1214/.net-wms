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
	/// FSMTMachineDiscardMP 的摘要说明。
	/// </summary>
	public partial class FSMTMachineDiscardMP : BaseMPage
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
			this.gridHelper.AddColumn( "PickupCount", "Pickup Count",	null);
			this.gridHelper.AddColumn( "RejectParts", "Reject Parts",	null);
			this.gridHelper.AddColumn( "NoPickup", "No Pickup",	null);
			this.gridHelper.AddColumn( "SMTErrorParts", "Error Parts",	null);
			this.gridHelper.AddColumn( "DislodgedParts", "Dislodged Parts",	null);
			this.gridHelper.AddColumn( "MaintainUser", "维护人员",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);
			this.gridWebGrid.Columns.FromKey("PickupCount").Format = "#,#";
			this.gridWebGrid.Columns.FromKey("PickupCount").CellStyle.HorizontalAlign = HorizontalAlign.Right;
			this.gridWebGrid.Columns.FromKey("RejectParts").Format = "#,#";
			this.gridWebGrid.Columns.FromKey("RejectParts").CellStyle.HorizontalAlign = HorizontalAlign.Right;
			this.gridWebGrid.Columns.FromKey("NoPickup").Format = "#,#";
			this.gridWebGrid.Columns.FromKey("NoPickup").CellStyle.HorizontalAlign = HorizontalAlign.Right;
			this.gridWebGrid.Columns.FromKey("SMTErrorParts").Format = "#,#";
			this.gridWebGrid.Columns.FromKey("SMTErrorParts").CellStyle.HorizontalAlign = HorizontalAlign.Right;
			this.gridWebGrid.Columns.FromKey("DislodgedParts").Format = "#,#";
			this.gridWebGrid.Columns.FromKey("DislodgedParts").CellStyle.HorizontalAlign = HorizontalAlign.Right;

			this.gridHelper.AddDefaultColumn( false, false );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			SMTMachineDiscard item = (SMTMachineDiscard)obj;
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								item.MOCode,
								item.StepSequenceCode,
								item.MaterialCode,
								item.MachineStationCode,
								item.PickupCount,
								item.RejectParts,
								item.NoPickup,
								item.ErrorParts,
								item.DislodgedParts,
								item.MaintainUser,
								FormatHelper.ToDateString(item.MaintainDate),
								FormatHelper.ToTimeString(item.MaintainTime)
							});
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QuerySMTMachineDiscard( 
				this.txtMOCodeQuery.Text.Trim().ToUpper(),
				this.txtSSCodeQuery.Text.Trim().ToUpper(),
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QuerySMTMachineDiscardCount( 
				this.txtMOCodeQuery.Text.Trim().ToUpper(),
				this.txtSSCodeQuery.Text.Trim().ToUpper()
				);
		}

		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			SMTMachineDiscard item = (SMTMachineDiscard)obj;
			return new string[]{
								   item.MOCode,
								   item.StepSequenceCode,
								   item.MaterialCode,
								   item.MachineStationCode,
								   item.PickupCount.ToString(),
								   item.RejectParts.ToString(),
								   item.NoPickup.ToString(),
								   item.ErrorParts.ToString(),
								   item.DislodgedParts.ToString(),
								   item.MaintainUser,
								   FormatHelper.ToDateString(item.MaintainDate),
								   FormatHelper.ToTimeString(item.MaintainTime)
							};
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"MOCode",
									"sscode",
									"MaterialCode",
									"MachineStationCdoe",
									"Pickup Count",
									"Reject Parts",
									"No Pickup",
									"Error Parts",
									"Dislodged Parts",
									"MaintainUser",
									"MaintainDate",
									"MaintainTime"
								};
		}
		#endregion

		protected void cmdImport_ServerClick(object sender, EventArgs e)
		{
			this.Page.Response.Redirect("FSMTMachineDiscardImpSP.aspx");
		}
	}
}
