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
	/// FFactory 的摘要说明。
	/// </summary>
	public partial class FWHCountDetail : BaseMPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
	
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdDelete;
		private BenQGuru.eMES.Material.WarehouseFacade _facade ;


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
			if(!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				Initparameters();
				InitViewPanel();
			}
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}

		private void InitViewPanel()
		{
			this.txtFacCode.Text = this.FactoryCode ;
			this.txtWHCode.Text = this.WarehouseCode ;
			this.txtWHItemCode.Text = this.WarehouseItemCode ;

			if( _facade == null )
			{
				_facade = new WarehouseFacade(base.DataProvider);
			}

			this.txtQty.Text = _facade.GetLastScatterQty( this.FactoryCode, this.WarehouseCode, this.WarehouseItemCode );
			this.txtOpenQty.Text = _facade.GetOpenQty( this.FactoryCode, this.WarehouseCode, this.WarehouseItemCode );
		}
		#endregion

		#region WebGrid
		protected override void InitWebGrid()
		{
			this.gridHelper.AddColumn( "MOCode", "工单代码",	null);
			this.gridHelper.AddColumn( "MOFQty", "工单发料数量",	null);
			this.gridHelper.AddColumn( "CollectQty", "上料数量",	null);
			this.gridHelper.AddColumn( "DropQty", "下料数量",	null);
			this.gridHelper.AddColumn( "BackQty", "退料数量",	null);

			this.gridHelper.AddColumn( "YWHQtyIn", "移库入库数量",	null);
			this.gridHelper.AddColumn( "YWHQtyOut", "移库出库数量",	null);
			
			this.gridHelper.AddColumn( "TsQtyOn", "维修换上数量",	null);
			this.gridHelper.AddColumn( "TsQtyDown", "维修换下数量",	null);
			this.gridHelper.AddColumn( "Mo_OffMoQty", "脱离工单数量",	null);
			this.gridHelper.AddColumn( "WarehouseQty", "离散数量",	null);
			this.gridHelper.AddColumn( "Lineqty", "在制品虚拆数量",	null);
			this.gridHelper.AddColumn( "BillOpenQty", "帐面数",	null);

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridHelper.RequestData();
		}

		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			WarehouseCountDetail whCountDt = obj as WarehouseCountDetail;
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
				new object[]{   whCountDt.MOCode, 
								whCountDt.MOFQty.ToString("##.##"),
								whCountDt.CollectQty.ToString("##.##"),
								whCountDt.DropQty.ToString("##.##"),
								whCountDt.BackQty.ToString("##.##"),
								whCountDt.YWHQtyIn.ToString("##.##"),
								whCountDt.YWHQtyOut.ToString("##.##"),
								whCountDt.TsQtyOn.ToString("##.##"),
								whCountDt.TsQtyDown.ToString("##.##"),
								whCountDt.OffMoQty.ToString("##.##"),
								whCountDt.WarehouseQty.ToString("##.##"),
								whCountDt.Lineqty.ToString("##.##"),
								whCountDt.BillOpenQty.ToString("##.##")} );
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if( _facade == null )
			{
				_facade = new WarehouseFacade(base.DataProvider);
			}
			return _facade.QueryWarehouseCountDetail( this.FactoryCode, this.WarehouseCode, this.WarehouseItemCode, inclusive, exclusive );
		}


		protected override int GetRowCount()
		{
			if( _facade == null )
			{
				_facade = new WarehouseFacade(base.DataProvider);
			}
			return _facade.QueryWarehouseCountDetailCount( this.FactoryCode, this.WarehouseCode, this.WarehouseItemCode );
		}

		#endregion

		#region Button

		private void cmdExportCycle_ServerClick(object sender, EventArgs e)
		{
			this.cmdExport_Click(sender, e);
		}
		
		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			WarehouseCountDetail whCountDt = obj as WarehouseCountDetail;
			return new string[]{   whCountDt.MOCode, 
								whCountDt.MOFQty.ToString("##.##"),
								whCountDt.CollectQty.ToString("##.##"),
								whCountDt.DropQty.ToString("##.##"),
								whCountDt.BackQty.ToString("##.##"),
								whCountDt.YWHQtyIn.ToString("##.##"),
								whCountDt.YWHQtyOut.ToString("##.##"),
								whCountDt.TsQtyOn.ToString("##.##"),
								whCountDt.TsQtyDown.ToString("##.##"),
								whCountDt.OffMoQty.ToString("##.##"),
								whCountDt.WarehouseQty.ToString("##.##"),
								whCountDt.Lineqty.ToString("##.##"),
								whCountDt.BillOpenQty.ToString("##.##")} ;
		}

		protected override string[] GetColumnHeaderText()
		{
			string[] strArr =  new string[]{
												"MOCode",
												"MOFQty",
												"CollectQty",
												"DropQty",
												"BackQty",
												"YWHQtyIn",
												"YWHQtyOut",
												"TsQtyOn",
												"TsQtyDown",
												"Mo_OffMoQty",
											    "WarehouseQty",
											    "Lineqty",
											    "BillOpenQty"};
			return strArr;
		}
		#endregion

		#region property
		private void Initparameters()
		{
			if(this.Request.Params["FactoryCode"] == null 
				|| this.Request.Params["WarehouseCode"] == null
				|| this.Request.Params["WarehouseItemCode"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["FactoryCode"] = this.Request.Params["FactoryCode"];
				this.ViewState["WarehouseCode"] = this.Request.Params["WarehouseCode"];
				this.ViewState["WarehouseItemCode"] = this.Request.Params["WarehouseItemCode"];
			}
		}


	
		public string FactoryCode
		{
			get
			{
				return (string) this.ViewState["FactoryCode"];
			}
		}

		public string WarehouseCode
		{
			get
			{
				return (string) this.ViewState["WarehouseCode"];
			}
		}
		public string WarehouseItemCode
		{
			get
			{
				return (string) this.ViewState["WarehouseItemCode"];
			}
		}
		#endregion

	}
}
