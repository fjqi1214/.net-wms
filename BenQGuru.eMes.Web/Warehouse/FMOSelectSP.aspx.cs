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
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
	/// <summary>
	/// FMOSelectSP 的摘要说明。
	/// </summary>
	public partial class FMOSelectSP : BaseMPage
	{
		protected System.Web.UI.WebControls.Label lblShiftTitle;

		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		
		private BenQGuru.eMES.Material.WarehouseFacade _facade ;//= new BenQGuru.eMES.MOModel.MOFacade(base.DataProvider);

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
			this.cmdSave.Disabled = false;
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
			this.gridHelper.AddColumn( "ItemCode", "产品代码",	null);
			this.gridHelper.AddColumn( "ItemName", "产品名称",	null);

			this.gridHelper.AddDefaultColumn( true, false );
			this.gridHelper.CheckAllBox.Visible = false;
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			BenQGuru.eMES.Domain.MOModel.MO item = (BenQGuru.eMES.Domain.MOModel.MO)obj;
			Infragistics.WebUI.UltraWebGrid.UltraGridRow row = new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								item.MOCode,
								item.ItemCode,
								item.EAttribute1
			});
			item = null;
			return row;
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade == null){_facade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);}
			return this._facade.GetAvailableMO(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtMOCodeQuery.Text)), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtItemCodeQuery.Text)), inclusive, exclusive);
		}

		protected override int GetRowCount()
		{
			if(_facade == null){_facade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);}
			return this._facade.GetAvailableMOCount(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtMOCodeQuery.Text)), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtItemCodeQuery.Text)));
		}
		#endregion

		#region Button
		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			this.cmdSave.Disabled = false;
		}
		protected override void cmdSave_Click(object sender, EventArgs e)
		{
			ArrayList array = this.gridHelper.GetCheckedRows();
			if ( array.Count > 0 )
			{
				UltraGridRow row = (UltraGridRow)array[0];
				this.txtReturnCode.Value = row.Cells[1].Text;
				Page.RegisterStartupScript("return value", @"<script language='javascript'>ReturnValue();</script>");
			}
		}

		#endregion

	}
}
