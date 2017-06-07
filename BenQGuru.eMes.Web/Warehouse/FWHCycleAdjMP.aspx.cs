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
	/// FWHCycleAdjMP 的摘要说明。
	/// </summary>
	public partial class FWHCycleAdjMP : BaseMPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
	
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
				this.cmdAdjust.Disabled = true;
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
			this.gridHelper.AddColumn( "FactoryCode", "工厂代码",	null);
			//this.gridHelper.AddColumn( "SegmentCode", "工段代码",	null);
			this.gridHelper.AddColumn( "WarehouseCode", "仓库名称",	null);
			this.gridHelper.AddColumn( "WarehouseStatusLabel", "仓库状态",	null);
			this.gridHelper.AddColumn( "WarehouseItemCode", "物料代码",	null);
			this.gridHelper.AddColumn( "WarehouseItemName", "物料名称",	null);
			this.gridHelper.AddColumn( "WarehouseItemQty1", "离散数量",	null);
			this.gridHelper.AddColumn( "WarehouseItemLineQty", "产线虚拆数量",	null);
			this.gridHelper.AddColumn( "WarehouseItemQty", "帐面数",	null);
			this.gridHelper.AddColumn( "WarehouseItemActualQty", "盘点数",	null);
			this.gridHelper.AddColumn( "WarehouseCycleDiff", "差异数",	null);
			this.gridHelper.AddColumn( "WarehouseCycleCode", "盘点序号",	null);

			this.gridHelper.AddDefaultColumn( false, false );
			this.gridWebGrid.Columns.FromKey("WarehouseCycleCode").Hidden = true;
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			string factoryCode, warehouseCode, itemCode,qty,lineqty, openQty, actualQty,cycleCode, diff;
			WarehouseCycleCountDetail item = (WarehouseCycleCountDetail)obj;
			factoryCode = item.FactoryCode;
			//segmentCode = item.SegmentCode;
			warehouseCode = item.WarehouseCode;
			itemCode = item.ItemCode;
			qty     = item.Qty.ToString();
			lineqty = item.LineQty.ToString();
			openQty = item.Warehouse2LineQty.ToString();
			actualQty = item.PhysicalQty.ToString();
			cycleCode = item.CycleCountCode;
			diff = (item.PhysicalQty - item.Warehouse2LineQty).ToString();
			if (this.ViewState["ADJUST_SUCCESS"] != null && this.ViewState["ADJUST_SUCCESS"].ToString() == "yes")
			{
				openQty = actualQty;
				diff = "0";
			}

			Infragistics.WebUI.UltraWebGrid.UltraGridRow row = 
				new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
				new object[]{
								factoryCode,
								//segmentCode,
								warehouseCode,
								GetWarehouseStatusLabel(item),
								itemCode,
								GetItemName(itemCode),
								Math.Round(Convert.ToDecimal(qty), 2),
								Math.Round(Convert.ToDecimal(lineqty), 2),
								Math.Round(Convert.ToDecimal(openQty), 2),
								Math.Round(Convert.ToDecimal(actualQty), 2),
								Math.Round(Convert.ToDecimal(diff), 2),
								cycleCode
								});
			return row;
		}

		private string whstatus_cycle = null;
		private string whstatus_normal = null;
		private string GetWarehouseStatusLabel(WarehouseCycleCountDetail item)
		{
			if (this.whstatus_normal == null)
			{
				this.whstatus_normal = this.languageComponent1.GetString(Warehouse.WarehouseStatus_Normal);
				this.whstatus_cycle = this.languageComponent1.GetString(Warehouse.WarehouseStatus_Cycle);
			}
			if (item.AdjustUser == this.GetUserCode())
				return this.whstatus_normal;
			else
				return this.whstatus_cycle;
		}

		private Hashtable htItems = new Hashtable();
		private string GetItemName(string itemCode)
		{
			try
			{
				//TODO ForSimone 此处替换半角逗号为全角逗号,为导出到csv准备
				return this.ReplaceComma(htItems[itemCode].ToString());
			}
			catch
			{
				return string.Empty;
			}
		}

		//替换半角逗号为全角逗号
		private string ReplaceComma(string str)
		{
			return str.Replace(",","，");
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object[] objs = this._facade.QueryWarehouseCycleDetailInAdjustCheck( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpWarehouseCodeQuery.Text)), /* this.drpSegmentCodeQuery.SelectedValue, */this.drpFactoryCodeQuery.SelectedValue, this.ViewState["ADJUST_SUCCESS"],
				inclusive, exclusive );
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
			return this._facade.QueryWarehouseCycleDetailInAdjustCheckCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpWarehouseCodeQuery.Text)), /*this.drpSegmentCodeQuery.SelectedValue,*/ this.drpFactoryCodeQuery.SelectedValue, this.ViewState["ADJUST_SUCCESS"]);
		}

		#endregion

		#region Button
		
		protected override void cmdQuery_Click(object sender, EventArgs e)
		{
			base.cmdQuery_Click (sender, e);

			this.txtFactoryCode.Value = this.drpFactoryCodeQuery.SelectedValue;
			//this.txtSegmentCode.Value = this.drpSegmentCodeQuery.SelectedValue;
			this.txtWarehouseCode.Value = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpWarehouseCodeQuery.Text));

			this.cmdAdjust.Disabled = false;
		}

		protected void cmdComfirm_ServerClick(object sender, EventArgs e)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			if (TransactionType.TRANSACTION_MAPPING == null)
			{
				try
				{
					string strPath = this.MapPath("TransTypeMoStock.xml");
					System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
					doc.Load(strPath);
					System.Xml.XmlElement eleDoc = doc.DocumentElement;
					TransactionType.TRANSACTION_MAPPING = new Hashtable();
					System.Xml.XmlNode elemap = eleDoc.SelectSingleNode("//TransTypeMapping");
					if (elemap != null)
					{
						for (int i = 0; i < elemap.ChildNodes.Count; i++)
						{
							if (elemap.ChildNodes[i].NodeType == System.Xml.XmlNodeType.Element)
							{
								TransactionType.TRANSACTION_MAPPING.Add(elemap.ChildNodes[i].Name, elemap.ChildNodes[i].Attributes["Code"].Value);
							}
						}
					}
				}
				catch
				{
					TransactionType.TRANSACTION_MAPPING = null;
				}
			}

			bool bresult = this._facade.AdjustWarehouseCycleCount(this.txtWarehouseCode.Value, /*this.txtSegmentCode.Value,*/ this.txtFactoryCode.Value, this.GetUserCode());
			if (bresult == true)
			{
				this.ViewState["ADJUST_SUCCESS"] = "yes";
				this.cmdQuery_Click(null, null);
				this.cmdAdjust.Disabled = true;
			}
		}
		#endregion

		#region 数据初始化

		private void InitDropDownList()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			BenQGuru.eMES.Common.MutiLanguage.LanguageWord lword  = languageComponent1.GetLanguage("listItemAll");
			DropDownListBuilder builder = new DropDownListBuilder(this.drpFactoryCodeQuery);
			builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetAllFactory);
			builder.Build("FactoryCode","FactoryCode");
			this.drpFactoryCodeQuery.Items.Insert(0, new ListItem("", ""));

			BenQGuru.eMES.BaseSetting.BaseModelFacade bmFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(base.DataProvider);
//			builder = new DropDownListBuilder(this.drpSegmentCodeQuery);
//			builder.HandleGetObjectList = new GetObjectListDelegate(bmFacade.GetAllSegment);
//			builder.Build("SegmentCode","SegmentCode");
//			this.drpSegmentCodeQuery.Items.Insert(0, new ListItem("", ""));

			bmFacade = null;
			lword = null;
			builder = null;
		}

		//选择来源工厂、工段，查询所有仓库
		/*
		private void FillWarehouseFrom(object sender, System.EventArgs e)
		{
			this.drpWarehouseCodeQuery.Items.Clear();
			if (this.drpFactoryCodeQuery.SelectedValue == string.Empty)
			{
				this.drpSegmentCodeQuery.SelectedValue = string.Empty;
			}
			else
				this.FillWarehouse(this.drpFactoryCodeQuery, this.drpSegmentCodeQuery, this.drpWarehouseCodeQuery);
		}
		private void FillWarehouse(DropDownList drpFactory, DropDownList drpSeg, DropDownList drp)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			string factoryCode = drpFactory.SelectedValue;
			string segCode = drpSeg.SelectedValue;
			object[] objs = this._facade.GetWarehouseByFactorySeg(segCode, factoryCode, true);
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
			string factoryCode, warehouseCode, itemCode, openQty, actualQty,cycleCode;
			WarehouseCycleCountDetail item = (WarehouseCycleCountDetail)obj;
			factoryCode = item.FactoryCode;
			//segmentCode = item.SegmentCode;
			warehouseCode = item.WarehouseCode;
			itemCode = item.ItemCode;
			openQty = item.Qty.ToString();
			actualQty = item.PhysicalQty.ToString();
			cycleCode = item.CycleCountCode;
			if (this.ViewState["ADJUST_SUCCESS"] != null && this.ViewState["ADJUST_SUCCESS"].ToString() == "yes")
			{
				openQty = actualQty;
			}
			string[] strArr = 
				new string[]{	factoryCode,
								//segmentCode,
								warehouseCode,
								itemCode,
								GetItemName(itemCode),
								openQty,
								actualQty,
								(item.PhysicalQty - item.Qty ).ToString()
							};
			return strArr;
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"FactoryCode",
									//"SegmentCode",
									"WarehouseCode",
									"WarehouseItemCode",
									"WarehouseItemName",
									"ItemOpenQty",
									"ActualQty",
									"DiffQty"
								};
		}
		#endregion
	}
}
