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
	public partial class FQueryItemDtlSP : BaseMPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
	
		private ButtonHelper buttonHelper = null;
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

				if (this.GetRequestParam("itemcode") == "" || this.GetRequestParam("transtype") == "" ||
					(this.GetRequestParam("factoryfrom") == "" && this.GetRequestParam("factoryto") == "") ||
					//(this.GetRequestParam("segmentfrom") == "" && this.GetRequestParam("segmentto") == "") ||
					(this.GetRequestParam("warehousecodefrom") == "" && this.GetRequestParam("warehousecodeto") == ""))
				{
					throw new Exception("");
				}
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
			this.gridHelper.AddColumn( "TicketNo", "单据号",	null);
			this.gridHelper.AddColumn( "TicketQty", "数量",	null);
			this.gridHelper.AddColumn( "TicketUser", "制单人",	null);
			this.gridHelper.AddColumn( "TransactionUser", "交易人",	null);
			this.gridHelper.AddColumn( "TicketDate", "制单日期",	null);
			this.gridHelper.AddColumn( "TransactionDate", "交易日期",	null);

			this.gridHelper.AddDefaultColumn( false, false );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			DisplayTicketData();
		}

		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			WarehouseTicketDetail item = (WarehouseTicketDetail)obj;
			WarehouseTicket tkt = (WarehouseTicket)httkt[item.TicketNo];
			Infragistics.WebUI.UltraWebGrid.UltraGridRow row = 
				new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
				new object[]{
								GetTicketNoDisplay(item),
								Math.Round(item.ActualQty, 2),
								tkt.TicketUser,
								tkt.TransactionUser,
								FormatHelper.ToDateString(tkt.TicketDate),
								FormatHelper.ToDateString(tkt.TransactionDate)
							});
			item = null;
			return row;
		}
		private string GetTicketNoDisplay(WarehouseTicketDetail item)
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
			if (strHideList.IndexOf("," + this.GetRequestParam("transtype") + ",") < 0 || item.TicketNo.Substring(0, 1) != "-")
			{
				strTicketNo = item.TicketNo;
			}
			return strTicketNo;
		}


		private Hashtable httkt = new Hashtable();
		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object[] objstkt = null;
			object[] objs = this._facade.GetWarehouseTicketQueryItemDetail( 
				this.txtItemCodeQuery.Text, this.GetRequestParam("transtype"), this.GetRequestParam("mocode"), 
				this.txtFactoryFromQuery.Text, /*this.txtSegmentFromQuery.Text,*/ this.txtWarehouseFromQuery.Text,
				this.txtFactoryToQuery.Text, /*this.txtSegmentToQuery.Text,*/ this.txtWarehouseToQuery.Text,
				inclusive, exclusive, ref objstkt );
			if (objstkt != null)
			{
				for (int i = 0; i < objstkt.Length; i++)
				{
					httkt.Add(((WarehouseTicket)objstkt[i]).TicketNo, objstkt[i]);
				}
			}
			return objs;
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.GetWarehouseTicketQueryItemDetailCount( 
				this.txtItemCodeQuery.Text, this.GetRequestParam("transtype"), this.GetRequestParam("mocode"), 
				this.txtFactoryFromQuery.Text, /*this.txtSegmentFromQuery.Text,*/ this.txtWarehouseFromQuery.Text,
				this.txtFactoryToQuery.Text, /*this.txtSegmentToQuery.Text,*/ this.txtWarehouseToQuery.Text);
		}

		private void DisplayTicketData()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this.txtItemCodeQuery.Text = this.GetRequestParam("itemcode");
			this.txtFactoryFromQuery.Text = this.GetRequestParam("factoryfrom");
			//this.txtSegmentFromQuery.Text = this.GetRequestParam("segmentfrom");
			this.txtWarehouseFromQuery.Text = this.GetRequestParam("warehousecodefrom");
			this.txtFactoryToQuery.Text = this.GetRequestParam("factoryto");
			//this.txtSegmentToQuery.Text = this.GetRequestParam("segmentto");
			this.txtWarehouseToQuery.Text = this.GetRequestParam("warehousecodeto");
			
			object obj = this._facade.GetTransactionType(this.GetRequestParam("transtype"));
			this.txtTransNameQuery.Text = ((TransactionType)obj).TransactionTypeName;
			obj = this._facade.GetWarehouseItem(this.GetRequestParam("itemcode"));
			this.txtItemNameQuery.Text = ((WarehouseItem)obj).ItemName;

			this.cmdQuery_Click(null, null);
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("../Warehouse/FQueryItemMP.aspx"));
		}
		#endregion


		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			WarehouseTicketDetail item = (WarehouseTicketDetail)obj;
			WarehouseTicket tkt = (WarehouseTicket)httkt[item.TicketNo];
			string[] strArr = 
				new string[]{	item.TicketNo,
								item.ActualQty.ToString(),
								tkt.TicketUser,
								tkt.TransactionUser,
								FormatHelper.ToDateString(tkt.TicketDate),
								FormatHelper.ToDateString(tkt.TransactionDate)
							};
			item = null;
			return strArr;
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"TicketNo",
									"TicketQty",
									"TicketUser",
									"TransactionUser",
									"TicketDate",
									"TransactionDate"
								};
		}
		#endregion


	
	}
}
