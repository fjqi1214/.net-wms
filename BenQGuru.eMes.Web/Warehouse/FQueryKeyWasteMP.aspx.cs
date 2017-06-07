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

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using Infragistics.WebUI.UltraWebGrid;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
	/// <summary>
	/// FQueryKeyWasteMP 的摘要说明。
	/// </summary>
	public partial class FQueryKeyWasteMP : BaseMPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
	
		private ButtonHelper buttonHelper = null;
		protected System.Web.UI.WebControls.Label lblFactoryCodeQuery;
		protected System.Web.UI.WebControls.DropDownList drpFactoryCodeQuery;
		protected System.Web.UI.WebControls.Label lblWarehouseCodeQuery;
		protected System.Web.UI.WebControls.DropDownList drpWarehouseCodeQuery;
		private WarehouseFacade _facade ;//= new WarehouseFacade();


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

				InitDropDownList();
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
			this.gridHelper.AddColumn( "MOItemInputQty", "投入量",	null);
			this.gridHelper.AddColumn( "MOToTSQty", "送离线维修",	null);
			this.gridHelper.AddColumn( "MOTSGoodQty", "修好量",	null);
			this.gridHelper.AddColumn( "MOTSDoingQty", "在修量",	null);
			this.gridHelper.AddColumn( "MOScrapQty", "报废量",	null);
			this.gridHelper.AddColumn( "MOWearOffRate", "损耗率",	null);
			this.gridHelper.AddColumn( "MOScrapRate", "报废率",	null);

			this.gridHelper.AddDefaultColumn( false, false );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			decimal[] dec = (decimal[])obj;
			Infragistics.WebUI.UltraWebGrid.UltraGridRow row = new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								dec[0].ToString(),
								dec[1].ToString(),
								dec[2].ToString(),
								dec[3].ToString(),
								dec[4].ToString(),
								Math.Round(dec[5] * 100, 2).ToString() + "%",
								Math.Round(dec[6] * 100, 2).ToString() + "%"
								});
			return row;
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.GetItemKeyWaste(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)), inclusive, exclusive);
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.GetItemKeyWasteCount(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)));
		}

		#endregion

		#region 数据初始化

		private void InitDropDownList()
		{
		}
		#endregion


		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			decimal[] dec = (decimal[])obj;
			string[] strValue = new string[]{
												dec[0].ToString(),
												dec[1].ToString(),
												dec[2].ToString(),
												dec[3].ToString(),
												dec[4].ToString(),
												(dec[5] * 100).ToString() + "%",
												(dec[6] * 100).ToString() + "%"
											};
			return strValue;
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"MOItemInputQty",
									"MOToTSQty",
									"MOTSGoodQty",
									"MOTSDoingQty",
									"MOScrapQty",
									"MOWearOffRate",
									"MOScrapRate"
								};
		}
		#endregion


	
	}
}
