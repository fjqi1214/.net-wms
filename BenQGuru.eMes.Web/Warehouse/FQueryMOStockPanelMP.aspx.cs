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
	/// FQueryMOStockPanelMP 的摘要说明。
	/// </summary>
	public partial class FQueryMOStockPanelMP : BaseMPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
	
		private ButtonHelper buttonHelper = null;
		private WarehouseFacade _facade ;//= new WarehouseFacade();
		private BenQGuru.eMES.MOModel.MOFacade moFacade;
		private BenQGuru.eMES.MOModel.SBOMFacade sbomFacade;


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
			this.cmdAdd.Visible = false;
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
			this.gridHelper.AddColumn( "MOCode", "工单号",	null);
			this.gridHelper.AddColumn( "WarehouseItemCode", "物料代码",	null);
			this.gridHelper.AddColumn( "WarehouseItemName", "物料名称",	null);
			this.gridHelper.AddColumn( "MOQty", "工单数量",	null);
			this.gridHelper.AddColumn( "MOMaterialQty", "需求数量",	null);
			this.gridHelper.AddColumn( "ReceiptQty", "发放数",	null);
			this.gridHelper.AddColumn( "MOMaterialDiffQty", "差异数量",	null);
			this.gridHelper.AddColumn( "IssueQty", "耗用数",	null);
			this.gridHelper.AddColumn( "ScrapQty", "报废数",	null);
			this.gridHelper.AddColumn( "ReturnQty", "良品退料数",	null);
			this.gridHelper.AddColumn( "ReturnScrapQty", "不良品退料数",	null);
			this.gridHelper.AddColumn( "RemainQty", "剩余数",	null);
			this.gridHelper.AddColumn( "MOAdviseQty", "建议发放数",	null);

			this.gridHelper.AddDefaultColumn( false, true );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		
		private Hashtable htMOList = null;
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			MOStock stock = (MOStock)obj;
			string dNGRateManual, dNGRateFromItem, dWearOffRateTotal;


			string dMOWasteRate = "0%";		//工单损耗率
			string dMOScrapRate = "0%";;	//工单报废率

			#region 工单损耗率,工单报废率 不显示不计算

//			decimal wasteRate = 0;
//			decimal scrapRate = 0;
//			if(stock.MOLoadingQty !=0 && (stock.MOLoadingQty + stock.TSLoadingQty) != 0 )
//			if(stock.MOStatus == BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_CLOSE)
//			{
//					wasteRate = (stock.ScrapQty + stock.TSUnCompletedQty) / stock.MOLoadingQty;
//					scrapRate = (stock.ScrapQty + stock.TSUnCompletedQty) / (stock.MOLoadingQty + stock.TSLoadingQty);
//			}
//			else
//			{
//				wasteRate = stock.ScrapQty/ stock.MOLoadingQty;
//				scrapRate = stock.ScrapQty / (stock.MOLoadingQty + stock.TSLoadingQty);
//			}
//			if(wasteRate != 0)
//			{
//				dMOWasteRate = wasteRate.ToString("##.##%");
//			}
//			if(scrapRate != 0)
//			{
//				dMOScrapRate = scrapRate.ToString("##.##%");
//			}

			#endregion

			if (stock.IssueQty != 0)
			{
				dNGRateManual = Math.Round(stock.ScrapQty / stock.IssueQty * 100, 2).ToString() + "%";
				dNGRateFromItem = Math.Round((stock.ReturnScrapQty - stock.ScrapQty) / stock.IssueQty * 100, 2).ToString() + "%";
				dWearOffRateTotal = Math.Round(stock.ReturnScrapQty / stock.IssueQty * 100, 2).ToString() + "%";
			}
			else
			{
				dNGRateManual = dNGRateFromItem = dWearOffRateTotal = "0%";
			}


			//
			if (htMOList == null)
				htMOList = new Hashtable();
			if (htMOList.Contains(stock.MOCode) == false)
			{
				if (moFacade == null) { moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider); }
				Domain.MOModel.MO mo = (Domain.MOModel.MO)moFacade.GetMO(stock.MOCode);
				if (sbomFacade == null) { sbomFacade = new BenQGuru.eMES.MOModel.SBOMFacade(this.DataProvider); }
				object[] objsBom = sbomFacade.Query(mo.ItemCode);
				object[] objValue = new object[]{mo, objsBom};
				htMOList.Add(stock.MOCode, objValue);
			}
			object[] objMOValue = (object[])htMOList[stock.MOCode];
			Domain.MOModel.MO moValue = (Domain.MOModel.MO)objMOValue[0];
			string strMOQty = Convert.ToInt32(moValue.MOPlanQty).ToString();
			object[] objMOBom = (object[])objMOValue[1];
			string strMOMaterialQty = "0";
			if (objMOBom != null)
			{
				for (int i = 0; i < objMOBom.Length; i++)
				{
					Domain.MOModel.SBOM sbom = (Domain.MOModel.SBOM)objMOBom[i];
					if (sbom.SBOMItemCode == stock.ItemCode)
					{
						strMOMaterialQty = Math.Round(moValue.MOPlanQty * sbom.SBOMItemQty, 2).ToString();
						break;
					}
				}
			}
			string strDiffMaterialQty = "0";
			try
			{
				decimal dTmp = Convert.ToDecimal(strMOMaterialQty) - stock.ReceiptQty + stock.ReturnScrapQty + stock.ReturnQty;
				strDiffMaterialQty = Math.Round(dTmp, 2).ToString();
			}
			catch {}
			string strMemoQty = this.GetMemoQty(stock.MOCode, stock.ItemCode);
			//
			Infragistics.WebUI.UltraWebGrid.UltraGridRow row = 
				new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
				new object[]{
								stock.MOCode,
								stock.ItemCode,
								GetItemName(stock.ItemCode),
								strMOQty,
								strMOMaterialQty,
								Math.Round(stock.ReceiptQty, 2).ToString(),
								strDiffMaterialQty,
								Math.Round(stock.IssueQty, 2).ToString(),
								Math.Round(stock.ScrapQty, 2).ToString(),
								Math.Round(stock.ReturnQty, 2).ToString(),
								Math.Round(stock.ReturnScrapQty, 2).ToString(),
								Math.Round(stock.ReceiptQty - stock.IssueQty - stock.ReturnQty - stock.ReturnScrapQty, 2),
								strMemoQty,
								""
							});
			stock = null;
			return row;
		}
		private Hashtable htItems = new Hashtable();
		private string GetItemName(string itemCode)
		{
			try
			{
				return htItems[itemCode].ToString();
			}
			catch
			{
				return "";
			}
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			this.txtMOCodeEdit.Text = "";
			this.txtMaterialCodeEdit.Text = "";
			this.txtMaterialNameEdit.Text = "";
			this.txtMemo.Text = "";
			
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object[] objs = this._facade.GetMOStockInQuery( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)),
				inclusive, exclusive );
			

			#region 工单损耗率，工单报废率相关数据加载 不显示,不加载

			//this._facade.GetMOByMoStock(objs);						//工单状态加载
			//this._facade.GetMOLoadingMaterialCount(objs);				//工单物料上料数量
			//this._facade.GetTSLoadingMaterialCount(objs);				//工单物料维修不良数量
			//this._facade.GetUnCompletedMaterialCount(objs);			//工单物料未修完数量

			#endregion

			if (objs != null && objs.Length > 0)
			{
				//读取所有物料
				object[] objitem = this._facade.GetAllWarehouseItem();
				if (objitem != null)
				{
					for (int i = 0; i < objitem.Length; i++)
					{
						WarehouseItem item = (WarehouseItem)objitem[i];
						htItems.Add(item.ItemCode, item.ItemName);
						item = null;
					}
				}
				objitem = null;
			}

			return objs;
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.GetMOStockInQueryCount(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)));
		}

		#endregion


		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			/*
			MOStock stock = (MOStock)obj;
			string dNGRateManual, dNGRateFromItem, dWearOffRateTotal;
			if (stock.IssueQty != 0)
			{
				dNGRateManual = Math.Round(stock.ScrapQty / stock.IssueQty * 100, 2).ToString() + "%";
				dNGRateFromItem = Math.Round((stock.ReturnScrapQty - stock.ScrapQty) / stock.IssueQty * 100, 2).ToString() + "%";
				dWearOffRateTotal = Math.Round(stock.ReturnScrapQty / stock.IssueQty * 100, 2).ToString() + "%";
			}
			else
			{
				dNGRateManual = dNGRateFromItem = dWearOffRateTotal = "0%";
			}
			string[] strArr = 
				new string[]{	stock.MOCode,
								stock.ItemCode,
								GetItemName(stock.ItemCode),
								Math.Round(stock.ReceiptQty, 2).ToString(),
								Math.Round(stock.IssueQty, 2).ToString(),
								Math.Round(stock.ScrapQty, 2).ToString(),
								Math.Round(stock.ReturnQty, 2).ToString(),
								Math.Round(stock.ReturnScrapQty, 2).ToString(),
								Math.Round(stock.ReceiptQty - stock.IssueQty - stock.ReturnQty - stock.ReturnScrapQty, 2).ToString(),
								dNGRateManual,
								dNGRateFromItem,
								dWearOffRateTotal
							};
			stock = null;
			return strArr;
			*/
			MOStock stock = (MOStock)obj;
			string dNGRateManual, dNGRateFromItem, dWearOffRateTotal;


			string dMOWasteRate = "0%";		//工单损耗率
			string dMOScrapRate = "0%";;	//工单报废率

			#region 工单损耗率,工单报废率 不显示不计算

			//			decimal wasteRate = 0;
			//			decimal scrapRate = 0;
			//			if(stock.MOLoadingQty !=0 && (stock.MOLoadingQty + stock.TSLoadingQty) != 0 )
			//			if(stock.MOStatus == BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_CLOSE)
			//			{
			//					wasteRate = (stock.ScrapQty + stock.TSUnCompletedQty) / stock.MOLoadingQty;
			//					scrapRate = (stock.ScrapQty + stock.TSUnCompletedQty) / (stock.MOLoadingQty + stock.TSLoadingQty);
			//			}
			//			else
			//			{
			//				wasteRate = stock.ScrapQty/ stock.MOLoadingQty;
			//				scrapRate = stock.ScrapQty / (stock.MOLoadingQty + stock.TSLoadingQty);
			//			}
			//			if(wasteRate != 0)
			//			{
			//				dMOWasteRate = wasteRate.ToString("##.##%");
			//			}
			//			if(scrapRate != 0)
			//			{
			//				dMOScrapRate = scrapRate.ToString("##.##%");
			//			}

			#endregion

			if (stock.IssueQty != 0)
			{
				dNGRateManual = Math.Round(stock.ScrapQty / stock.IssueQty * 100, 2).ToString() + "%";
				dNGRateFromItem = Math.Round((stock.ReturnScrapQty - stock.ScrapQty) / stock.IssueQty * 100, 2).ToString() + "%";
				dWearOffRateTotal = Math.Round(stock.ReturnScrapQty / stock.IssueQty * 100, 2).ToString() + "%";
			}
			else
			{
				dNGRateManual = dNGRateFromItem = dWearOffRateTotal = "0%";
			}


			//
			if (htMOList == null)
				htMOList = new Hashtable();
			if (htMOList.Contains(stock.MOCode) == false)
			{
				if (moFacade == null) { moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider); }
				Domain.MOModel.MO mo = (Domain.MOModel.MO)moFacade.GetMO(stock.MOCode);
				if (sbomFacade == null) { sbomFacade = new BenQGuru.eMES.MOModel.SBOMFacade(this.DataProvider); }
				object[] objsBom = sbomFacade.Query(mo.ItemCode);
				object[] objValue = new object[]{mo, objsBom};
				htMOList.Add(stock.MOCode, objValue);
			}
			object[] objMOValue = (object[])htMOList[stock.MOCode];
			Domain.MOModel.MO moValue = (Domain.MOModel.MO)objMOValue[0];
			string strMOQty = Convert.ToInt32(moValue.MOPlanQty).ToString();
			object[] objMOBom = (object[])objMOValue[1];
			string strMOMaterialQty = "0";
			if (objMOBom != null)
			{
				for (int i = 0; i < objMOBom.Length; i++)
				{
					Domain.MOModel.SBOM sbom = (Domain.MOModel.SBOM)objMOBom[i];
					if (sbom.SBOMItemCode == stock.ItemCode)
					{
						strMOMaterialQty = Math.Round(moValue.MOPlanQty * sbom.SBOMItemQty, 2).ToString();
						break;
					}
				}
			}
			string strDiffMaterialQty = "0";
			try
			{
				decimal dTmp = Convert.ToDecimal(strMOMaterialQty) - stock.ReceiptQty + stock.ReturnScrapQty + stock.ReturnQty;
				strDiffMaterialQty = Math.Round(dTmp, 2).ToString();
			}
			catch {}
			string strMemoQty = this.GetMemoQty(stock.MOCode, stock.ItemCode);
			//
			string[] row = 
				new string[]{
								stock.MOCode,
								stock.ItemCode,
								GetItemName(stock.ItemCode),
								strMOQty,
								strMOMaterialQty,
								Math.Round(stock.ReceiptQty, 2).ToString(),
								strDiffMaterialQty,
								Math.Round(stock.IssueQty, 2).ToString(),
								Math.Round(stock.ScrapQty, 2).ToString(),
								Math.Round(stock.ReturnQty, 2).ToString(),
								Math.Round(stock.ReturnScrapQty, 2).ToString(),
								Math.Round(stock.ReceiptQty - stock.IssueQty - stock.ReturnQty - stock.ReturnScrapQty, 2).ToString(),
								strMemoQty,
								""
							};
			stock = null;
			return row;
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"MOCode",
									"WarehouseItemCode",
									"WarehouseItemName",
									"MOQty",
									"MOMaterialQty",
									"ReceiptQty",
									"MOMaterialDiffQty",
									"ReceiptScrapQty",
									"IssueQty",
									"ScrapQty",
									"ReturnQty",
									"RemainQty",
									"MOAdviseQty"
								};
		}
		#endregion

		private void gridWebGrid_ClickCellButton(object sender, CellEventArgs e)
		{
			if (e.Cell.Key.ToUpper() == "EDIT")
			{
				this.txtMOCodeEdit.Text = e.Cell.Row.Cells.FromKey("MOCode").Text;
				this.txtMaterialCodeEdit.Text = e.Cell.Row.Cells.FromKey("WarehouseItemCode").Text;
				this.txtMaterialNameEdit.Text = e.Cell.Row.Cells.FromKey("WarehouseItemName").Text;
				this.txtMemo.Text = e.Cell.Row.Cells.FromKey("MOAdviseQty").Text;
				this.cmdSave.Disabled = false;
			}
		}

		private string MOStockPanelMemoQtyKey = "MOStockPanelMemoQtyKey";
		private string GetMemoQty(string moCode, string materialCode)
		{
			if (Session[MOStockPanelMemoQtyKey] != null &&
				Session[MOStockPanelMemoQtyKey] is Hashtable)
			{
				Hashtable htTmp = (Hashtable)Session[MOStockPanelMemoQtyKey];
				if (htTmp.ContainsKey(moCode + ":" + materialCode) == true)
				{
					return htTmp[moCode + ":" + materialCode].ToString();
				}
			}
			return "";
		}
		private void SetMemoQty(string moCode, string materialCode, string memoQty)
		{
			Hashtable htTmp = new Hashtable();
			if (Session[MOStockPanelMemoQtyKey] == null)
				Session.Add(MOStockPanelMemoQtyKey, htTmp);
			else
				htTmp = (Hashtable)Session[MOStockPanelMemoQtyKey];
			if (htTmp.ContainsKey(moCode + ":" + materialCode) == true)
				htTmp[moCode + ":" + materialCode] = memoQty;
			else
				htTmp.Add(moCode + ":" + materialCode, memoQty);
			Session[moCode + ":" + materialCode] = htTmp;
		}

		protected void cmdSave_ServerClick(object sender, EventArgs e)
		{
			if (this.txtMOCodeEdit.Text != string.Empty && 
				this.txtMaterialCodeEdit.Text != string.Empty)
			{
				try
				{
					decimal d = decimal.Parse(this.txtMemo.Text);
					this.SetMemoQty(this.txtMOCodeEdit.Text, this.txtMaterialCodeEdit.Text, d.ToString());
					for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
					{
						if (this.gridWebGrid.Rows[i].Cells.FromKey("MOCode").Text == this.txtMOCodeEdit.Text &&
							this.gridWebGrid.Rows[i].Cells.FromKey("WarehouseItemCode").Text == this.txtMaterialCodeEdit.Text)
						{
							this.gridWebGrid.Rows[i].Cells.FromKey("MOAdviseQty").Text = this.GetMemoQty(this.txtMOCodeEdit.Text, this.txtMaterialCodeEdit.Text);
						}
					}
				}
				catch {}
			}
		}
	}
}
