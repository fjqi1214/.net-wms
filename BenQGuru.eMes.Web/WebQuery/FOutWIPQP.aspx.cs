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
using BenQGuru.eMES.Domain.SMT;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FFeederMP 的摘要说明。
	/// </summary>
	public partial class FOutWIPQP : BaseMPage
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdAdd;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdDelete;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdSave;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdCancel;
	
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
		
		private BenQGuru.eMES.WebQuery.OutSourcingQueryFacade _facade = null;
		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !this.IsPostBack )
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
			this.gridHelper.Grid.Columns.Clear();
			this.gridHelper.Grid.Rows.Clear();
			
			if (this.rdbType.SelectedValue == "PCS")
			{
				this.gridHelper.AddColumn("MOCode", "工单代码");
				this.gridHelper.AddColumn("StartSN", "起始序列号");
				this.gridHelper.AddColumn("EndSN", "结束序列号");
				this.gridHelper.AddColumn("OPCode", "工序");
				this.gridHelper.AddColumn("ProductStatus", "Pass/Fail");
				this.gridHelper.AddColumn("ErrorDescription", "不良及维修记录");
				this.gridHelper.AddColumn("SSSN", "产线代码");
				this.gridHelper.AddColumn("ShiftDate", "生产时间");
				
				this.lblStartRCardQuery.Text = "起始序列号";
				this.lblEndRCardQuery.Text = "结束序列号";
				this.lblMOIDQuery.Visible = true;
				this.txtConditionMo.Visible = true;
			}
			else
			{
				this.gridHelper.AddColumn("StartSN", "Lot No.");
				this.gridHelper.AddColumn("OPCode", "工序");
				this.gridHelper.AddColumn("GoodQty", "良品数");
				this.gridHelper.AddColumn("ErrorDescription", "不良及维修记录");
				this.gridHelper.AddColumn("SSSN", "产线代码");
				this.gridHelper.AddColumn("ShiftDate", "生产时间");
				
				this.lblStartRCardQuery.Text = "起始Lot号";
				this.lblEndRCardQuery.Text = "结束Lot号";
				this.lblMOIDQuery.Visible = false;
				this.txtConditionMo.Visible = false;
			}
			this.gridHelper.AddDefaultColumn( false, false );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			BenQGuru.eMES.Domain.OutSourcing.OutWIP wip = (BenQGuru.eMES.Domain.OutSourcing.OutWIP)obj;
			object[] objs = null;
			if (this.rdbType.SelectedValue == "LOT")
			{
				objs = new object[]{
									   wip.StartSN,
									   wip.OPCode,
									   wip.Qty,
									   wip.ErrorDescription,
									   wip.StepSequenceCode,
									   wip.ShiftDate };
			}
			else
			{
				objs = new object[]{
									   wip.MOCode,
									   wip.StartSN,
									   wip.EndSN,
									   wip.OPCode,
									   wip.ProductStatus,
									   wip.ErrorDescription,
									   wip.StepSequenceCode,
									   wip.ShiftDate };
			}
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(objs);
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new BenQGuru.eMES.WebQuery.OutSourcingQueryFacade(base.DataProvider);}
			return this._facade.QueryOutSourcingWIP(
				this.rdbType.SelectedValue, this.txtConditionItem.Text,
				this.txtConditionMo.Text, this.txtStartSnQuery.Text, this.txtEndSnQuery.Text,
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new BenQGuru.eMES.WebQuery.OutSourcingQueryFacade(base.DataProvider);}
			return this._facade.QueryOutSourcingWIPCount(
				this.rdbType.SelectedValue, this.txtConditionItem.Text,
				this.txtConditionMo.Text, this.txtStartSnQuery.Text, this.txtEndSnQuery.Text);
		}

		#endregion


		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			BenQGuru.eMES.Domain.OutSourcing.OutWIP wip = (BenQGuru.eMES.Domain.OutSourcing.OutWIP)obj;
			string[] str = null;
			if (this.rdbType.SelectedValue == "LOT")
			{
				str = new string[]{
									  wip.StartSN,
									  wip.OPCode,
									  wip.Qty.ToString(),
									  wip.ErrorDescription,
									  wip.StepSequenceCode,
									  wip.ShiftDate};
			}
			else
			{
				str = new string[]{
									   wip.MOCode,
									   wip.StartSN,
									   wip.EndSN,
									   wip.OPCode,
									   wip.ProductStatus,
									  wip.ErrorDescription,
									  wip.StepSequenceCode,
									  wip.ShiftDate};
			}
			return str;
		}

		protected override string[] GetColumnHeaderText()
		{
			string[] str = null;
			if (this.rdbType.SelectedValue == "PCS")
			{
				str = new string[]{
									  "MOCode",
									  "StartSN",
									  "EndSN",
									  "OPCode",
									  "ProductStatus",
									  "ErrorDescription",
									  "sscode",
									  "ShiftDate"
								  };
			}
			else
			{
				str = new string[]{
									  "Lot No.",
									  "OPCode",
									  "GoodQty",
									  "ErrorDescription",
									  "sscode",
									  "ShiftDate"
								  };
			}
			return str;
		}
		#endregion

		protected void rdoType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.InitWebGrid();
		}
	}
}
