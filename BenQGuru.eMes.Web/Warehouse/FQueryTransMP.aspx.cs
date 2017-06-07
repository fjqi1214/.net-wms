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
	/// FQueryTransMP 的摘要说明。
	/// </summary>
	public partial class FQueryTransMP : BaseMPage
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
			this.gridHelper.AddColumn( "WarehouseItemCode", "物料代码",	null);
			this.gridHelper.AddColumn( "WarehouseItemName", "物料名称",	null);
			this.gridHelper.AddColumn( "TransTicketNo", "单据号",	null);
			this.gridHelper.AddColumn( "TransactionType", "单据名称",	null);
			this.gridHelper.AddColumn( "TransactionAmount", "交易数量",	null);
			this.gridHelper.AddColumn( "TransactionAmountLeft", "库存结余",	null);
			this.gridHelper.AddColumn( "TransactionUser", "交易用户",	null);
			this.gridHelper.AddColumn( "TransactionDate", "交易日期",	null);

			this.gridHelper.AddDefaultColumn( false, false );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			WarehouseTicketDetail item = (WarehouseTicketDetail)obj;
			WarehouseTicket tkt = (WarehouseTicket)httkt[item.TicketNo];
			string strFactoryCode, strSegCode, strWarehouseCode;
			string strQty, strActualQty;
			if (tkt.FactoryCode == this.drpFactoryCodeQuery.SelectedValue && /* tkt.SegmentCode == this.drpSegmentCodeQuery.SelectedValue && */ tkt.WarehouseCode == FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpWarehouseCodeQuery.Text)))
			{
				strFactoryCode = tkt.FactoryCode;
				//strSegCode = tkt.SegmentCode;
				strWarehouseCode = tkt.WarehouseCode;
				strQty = (-1 * item.ActualQty).ToString();
				strActualQty = item.FromWarehouseQty.ToString();
			}
			else
			{
				strFactoryCode = tkt.TOFactoryCode;
				//strSegCode = tkt.TOSegmentCode;
				strWarehouseCode = tkt.TOWarehouseCode;
				strQty = item.ActualQty.ToString();
				strActualQty = item.ToWarehouseQty.ToString();
			}
			Infragistics.WebUI.UltraWebGrid.UltraGridRow row = 
				new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
				new object[]{
								strFactoryCode,
								//strSegCode,
								strWarehouseCode,
								item.ItemCode,
								item.ItemName,
								GetTicketNoDisplay(tkt),
								httranstype[tkt.TransactionTypeCode],
								Math.Round(Convert.ToDecimal(strQty), 2),
								Math.Round(Convert.ToDecimal(strActualQty), 2),
								tkt.TransactionUser,
								FormatHelper.ToDateString(tkt.TransactionDate)
							});
			item = null;
			tkt = null;
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
		private string GetTicketNoDisplay(WarehouseTicket tkt)
		{
			string strTicketNo = "";
			string strHideList = ",";
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
							if (elemap.ChildNodes[i].Attributes["HideTicketNo"] != null && elemap.ChildNodes[i].Attributes["HideTicketNo"].Value == "Yes")
							{
								strHideList += elemap.ChildNodes[i].Attributes["Code"].Value + ",";
							}
						}
					}
				}
			}
			catch
			{
				strHideList = "";
			}
			if (strHideList.IndexOf("," + tkt.TransactionTypeCode + ",") < 0 || tkt.TicketNo.Substring(0, 1) != "-")
			{
				strTicketNo = string.Format("<a href='FQueryTransDtlSP.aspx?ticketno={0}&returnurl={1}' style='color:#2f2f2f'>{0}</a>", tkt.TicketNo,"FQueryTransMP.aspx");
			}
			return strTicketNo;
		}


		private Hashtable httkt;
		private Hashtable httranstype;
		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object[] objstkt = null;
			object[] objs = this._facade.GetWarehouseTicketInQuery(
				string.Empty, string.Empty, FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)), FormatHelper.TODateInt(this.txtTransDateFromQuery.Text), FormatHelper.TODateInt(this.txtTransDateToQuery.Text),  this.drpFactoryCodeQuery.SelectedValue, /*this.drpSegmentCodeQuery.SelectedValue,*/ FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpWarehouseCodeQuery.Text)),
				inclusive, exclusive,
				ref objstkt);
			
			httkt = new Hashtable();
			if (objstkt != null)
			{
				for (int i = 0; i < objstkt.Length; i++)
				{
					httkt.Add(((WarehouseTicket)objstkt[i]).TicketNo, objstkt[i]);
				}
			}
			httranstype = new Hashtable();
			if (objs != null && objs.Length > 0)
			{
				object[] objstrans = this._facade.GetAllTransactionType();
				for (int i = 0; i < objstrans.Length; i++)
				{
					TransactionType type = (TransactionType)objstrans[i];
					httranstype.Add(type.TransactionTypeCode, type.TransactionTypeName);
				}
			}
			return objs;
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.GetWarehouseTicketInQueryCount(
				string.Empty, string.Empty, FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)), FormatHelper.TODateInt(this.txtTransDateFromQuery.Text), FormatHelper.TODateInt(this.txtTransDateToQuery.Text),  this.drpFactoryCodeQuery.SelectedValue, /*this.drpSegmentCodeQuery.SelectedValue, */ FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpWarehouseCodeQuery.Text)));
		}

		#endregion

		#region 数据初始化

		private void InitDropDownList()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			DropDownListBuilder builder = new DropDownListBuilder(this.drpFactoryCodeQuery);
			builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetAllFactory);
			builder.Build("FactoryCode","FactoryCode");
			this.drpFactoryCodeQuery.Items.Insert(0, new ListItem("", ""));

			BenQGuru.eMES.BaseSetting.BaseModelFacade bmFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(	base.DataProvider);
//			builder = new DropDownListBuilder(this.drpSegmentCodeQuery);
//			builder.HandleGetObjectList = new GetObjectListDelegate(bmFacade.GetAllSegment);
//			builder.Build("SegmentCode","SegmentCode");
//			this.drpSegmentCodeQuery.Items.Insert(0, new ListItem("", ""));

			/*
			builder = new DropDownListBuilder(this.drpWarehouseCodeQuery);
			builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetAllDistinctWarehouse);
			builder.Build("WarehouseCode","WarehouseCode");
			this.drpWarehouseCodeQuery.Items.Insert(0, new ListItem(lword.ControlText, ""));
			*/

			bmFacade = null;
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
			WarehouseTicketDetail item = (WarehouseTicketDetail)obj;
			WarehouseTicket tkt = (WarehouseTicket)httkt[item.TicketNo];
			string strFactoryCode, strSegCode, strWarehouseCode;
			string strQty, strActualQty;
			if (tkt.FactoryCode != string.Empty)
			{
				strFactoryCode = tkt.FactoryCode;
				//strSegCode = tkt.SegmentCode;
				strWarehouseCode = tkt.WarehouseCode;
				strQty = (-1 * item.ActualQty).ToString();
				strActualQty = item.FromWarehouseQty.ToString();
			}
			else
			{
				strFactoryCode = tkt.TOFactoryCode;
				//strSegCode = tkt.TOSegmentCode;
				strWarehouseCode = tkt.TOWarehouseCode;
				strQty = item.ActualQty.ToString();
				strActualQty = item.ToWarehouseQty.ToString();
			}
			string[] strArr = 
				new string[]{	strFactoryCode,
								//strSegCode,
								strWarehouseCode,
								item.ItemCode,
								item.ItemName,
								GetTicketNoDisplay(tkt),
								httranstype[tkt.TransactionTypeCode].ToString(),
								strQty,
								strActualQty,
								tkt.TransactionUser,
								FormatHelper.ToDateString(tkt.TransactionDate)
							};
			item = null;
			tkt = null;
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
									"TicketNo",
									"TransactionTypeName",
									"TicketQty",
									"TransactionAmountLeft",
									"TransactionUser",
									"TransactionDate"
								};
		}
		#endregion


	
	}
}
