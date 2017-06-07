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
	public partial class FQueryItemMP : BaseMPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
	
		private ButtonHelper buttonHelper = null;
		public BenQGuru.eMES.Web.UserControl.eMESDate txtTransDateFromQuery;
		public BenQGuru.eMES.Web.UserControl.eMESDate txtTransDateToQuery;

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
			this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

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
				this.txtTransDateToQuery.Text = DateTime.Today.ToString("yyyy-MM-dd");
				this.drpFactoryFromQuery.Attributes["onchange"] = "ChangeToDropDownList('drpFactoryFromQuery', 'drpFactoryToQuery');";
				//this.drpSegmentFromQuery.Attributes["onchange"] = "ChangeToDropDownList('drpSegmentFromQuery', 'drpSegmentToQuery');";
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
			this.gridHelper.AddColumn( "WarehouseItemCode", "物料代码",	null);
			this.gridHelper.AddColumn( "WarehouseItemName", "物料名称",	null);
			this.gridHelper.AddColumn( "TransactionTypeName", "单据名称",	null);
			this.gridHelper.AddColumn( "MOCode", "工单号",	null);
			this.gridHelper.AddColumn( "TransactionAmount", "数量",	null);
			this.gridHelper.AddColumn( "TransactionTypeCode", "单据类型",	null);
			this.gridHelper.AddColumn( "FactoryCode", "来源工厂",	null);
			//this.gridHelper.AddColumn( "SegmentCode", "来源工段",	null);
			this.gridHelper.AddColumn( "WarehouseCode", "来源仓库",	null);
			this.gridHelper.AddColumn( "TOFactoryCode", "目标工厂",	null);
			//this.gridHelper.AddColumn( "TOSegmentCode", "目标工段",	null);
			this.gridHelper.AddColumn( "TOWarehouseCode", "目标仓库",	null);
			this.gridHelper.AddLinkColumn( "TransactionDetail", "明细", null);

			this.gridWebGrid.Columns.FromKey("TransactionTypeCode").Hidden = true;
			this.gridWebGrid.Columns.FromKey("FactoryCode").Hidden = true;
			//this.gridWebGrid.Columns.FromKey("SegmentCode").Hidden = true;
			this.gridWebGrid.Columns.FromKey("WarehouseCode").Hidden = true;
			this.gridWebGrid.Columns.FromKey("TOFactoryCode").Hidden = true;
			//this.gridWebGrid.Columns.FromKey("TOSegmentCode").Hidden = true;
			this.gridWebGrid.Columns.FromKey("TOWarehouseCode").Hidden = true;
			this.gridHelper.AddDefaultColumn( false, false );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			WarehouseTicketQueryItem item = (WarehouseTicketQueryItem)obj;
			Infragistics.WebUI.UltraWebGrid.UltraGridRow row = 
				new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
				new object[]{
								item.ItemCode,
								htItems[item.ItemCode].ToString(),
								htTransType[item.TransactionTypeCode].ToString(),
								item.MOCode,
								Math.Round(item.Qty, 2).ToString(),
								item.TransactionTypeCode,
								item.FactoryCode,
								//item.SegmentCode,
								item.WarehouseCode,
								item.TOFactoryCode,
								//item.TOSegmentCode,
								item.TOWarehouseCode,
								""
							});
			item = null;
			return row;
		}

		private Hashtable htItems = new Hashtable();
		private Hashtable htTransType = new Hashtable();
		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object[] objs = this._facade.GetWarehouseTicketInQueryItem(
				string.Empty, this.txtTransTypeQuery.Value, FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)), 
				FormatHelper.TODateInt(this.txtTransDateFromQuery.Text), FormatHelper.TODateInt(this.txtTransDateToQuery.Text), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)),
				this.drpFactoryFromQuery.SelectedValue, /*this.drpSegmentFromQuery.SelectedValue,*/ FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpWarehouseFromQuery.Text)),
				this.drpFactoryToQuery.SelectedValue, /*this.drpSegmentToQuery.SelectedValue,*/ FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpWarehouseToQuery.Text)),
				inclusive, exclusive);
			
			if (objs != null)
			{
				htItems = new Hashtable();
				object[] objsitem = this._facade.GetAllWarehouseItem();
				if (objsitem != null)
				{
					for (int i = 0; i < objsitem.Length; i++)
					{
						WarehouseItem item = (WarehouseItem)objsitem[i];
						htItems.Add(item.ItemCode, item.ItemName);
						item = null;
					}
				}
				objsitem = null;

				htTransType = new Hashtable();
				object[] objstype = this._facade.GetAllTransactionType();
				if (objstype != null)
				{
					for (int i = 0; i < objstype.Length; i++)
					{
						TransactionType type = (TransactionType)objstype[i];
						htTransType.Add(type.TransactionTypeCode, type.TransactionTypeName);
					}
				}
			}

			return objs;
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.GetWarehouseTicketInQueryItemCount(
				string.Empty, this.txtTransTypeQuery.Value, FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)), 
				FormatHelper.TODateInt(this.txtTransDateFromQuery.Text), FormatHelper.TODateInt(this.txtTransDateToQuery.Text), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)),
				this.drpFactoryFromQuery.SelectedValue, /*this.drpSegmentFromQuery.SelectedValue,*/ FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpWarehouseFromQuery.Text)),
				this.drpFactoryToQuery.SelectedValue, /*this.drpSegmentToQuery.SelectedValue,*/ FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpWarehouseToQuery.Text)));
		}

		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if( this.gridHelper.IsClickColumn( "TransactionDetail",e ) )
			{
				this.Response.Redirect( this.MakeRedirectUrl("./FQueryItemDtlSP.aspx", 
					new string[]{"itemcode","transtype","mocode","factoryfrom","warehousecodefrom","factoryto","warehousecodeto"}, 
					new string[]{	e.Cell.Row.Cells.FromKey("WarehouseItemCode").Text,
									e.Cell.Row.Cells.FromKey("TransactionTypeCode").Text,
									e.Cell.Row.Cells.FromKey("MOCode").Text,
									e.Cell.Row.Cells.FromKey("FactoryCode").Text,
									e.Cell.Row.Cells.FromKey("WarehouseCode").Text,
									e.Cell.Row.Cells.FromKey("TOFactoryCode").Text,
									e.Cell.Row.Cells.FromKey("TOWarehouseCode").Text}));
			}
		}

		#endregion

		#region 数据初始化

		private void InitDropDownList()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			//来源工厂
			DropDownListBuilder builder = new DropDownListBuilder(this.drpFactoryFromQuery);
			builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetAllFactory);
			builder.Build("FactoryCode","FactoryCode");
			this.drpFactoryFromQuery.Items.Insert(0, new ListItem("", ""));
			//目标工厂
			builder = new DropDownListBuilder(this.drpFactoryToQuery);
			builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetAllFactory);
			builder.Build("FactoryCode","FactoryCode");
			this.drpFactoryToQuery.Items.Insert(0, new ListItem("", ""));

			//来源工段
			BenQGuru.eMES.BaseSetting.BaseModelFacade bmFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(base.DataProvider);
//			builder = new DropDownListBuilder(this.drpSegmentFromQuery);
//			builder.HandleGetObjectList = new GetObjectListDelegate(bmFacade.GetAllSegment);
//			builder.Build("SegmentCode","SegmentCode");
//			this.drpSegmentFromQuery.Items.Insert(0, new ListItem("", ""));
//			//目标工段
//			builder = new DropDownListBuilder(this.drpSegmentToQuery);
//			builder.HandleGetObjectList = new GetObjectListDelegate(bmFacade.GetAllSegment);
//			builder.Build("SegmentCode","SegmentCode");
//			this.drpSegmentToQuery.Items.Insert(0, new ListItem("", ""));

			bmFacade = null;
			builder = null;
		}

		//选择来源工厂、工段，查询所有仓库
		/*
		private void FillWarehouseFrom(object sender, System.EventArgs e)
		{
			this.drpWarehouseFromQuery.Items.Clear();
			if (this.drpFactoryFromQuery.SelectedValue == string.Empty)
			{
				this.drpSegmentFromQuery.SelectedValue = string.Empty;
			}
			else
			{
				this.FillWarehouse(this.drpFactoryFromQuery, this.drpSegmentFromQuery, this.drpWarehouseFromQuery);
				if (sender != null)
				{
					if (((DropDownList)sender).ID == this.drpFactoryFromQuery.ID)
						this.drpFactoryToQuery.SelectedIndex = this.drpFactoryFromQuery.SelectedIndex;
					if (((DropDownList)sender).ID == this.drpSegmentFromQuery.ID)
						this.drpSegmentToQuery.SelectedIndex = this.drpSegmentFromQuery.SelectedIndex;
					FillWarehouseTo(null, null);
				}
			}
		}
		private void FillWarehouseTo(object sender, System.EventArgs e)
		{
			this.drpWarehouseToQuery.Items.Clear();
			if (this.drpFactoryToQuery.SelectedValue == string.Empty)
			{
				this.drpSegmentToQuery.SelectedValue = string.Empty;
			}
			else
				this.FillWarehouse(this.drpFactoryToQuery, this.drpSegmentToQuery, this.drpWarehouseToQuery);
		}
		private void FillWarehouse(DropDownList drpFactory, DropDownList drpSeg, DropDownList drp)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			string factoryCode = drpFactory.SelectedValue;
			string segCode = drpSeg.SelectedValue;
			object[] objs = this._facade.GetWarehouseByFactorySeg(segCode, factoryCode);
			drp.Items.Clear();
			drp.Items.Add("");
			if (objs != null)
			{
				for (int i = 0; i < objs.Length; i++)
				{
					Warehouse wh = (Warehouse)objs[i];
					drp.Items.Add(new ListItem(wh.WarehouseCode, wh.WarehouseCode));
					wh = null;
				}
			}
			objs = null;
		}
		*/
		#endregion


		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			WarehouseTicketQueryItem item = (WarehouseTicketQueryItem)obj;
			string[] strArr = 
				new string[]{	item.ItemCode,
								htItems[item.ItemCode].ToString(),
								htTransType[item.TransactionTypeCode].ToString(),
								item.MOCode,
								item.Qty.ToString()
							};
			item = null;
			return strArr;
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"WarehouseItemCode",
									"WarehouseItemName",
									"TransactionTypeName",
									"MOCode",
									"TotalQty"
								};
		}
		#endregion
	}
}
