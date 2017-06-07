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
	public partial class FQueryTransDtlSP : BaseMPage
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
			this.PreRender += new System.EventHandler(this.FQueryTransDtlSP_PreRender);

		}
		#endregion

		
		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				if( (this.GetRequestParam("ticketno") == null || this.GetRequestParam("ticketno") == "")
					&& (this.GetRequestParam("returnurl") == null || this.GetRequestParam("returnurl") == "") )
				{
					throw new Exception("");
				}
				//this.cmdReturn.Attributes["onClick"] = "javascript:history.go(-1);return false;";
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
			this.gridHelper.AddColumn( "TicketSequence", "序号",	null);
			this.gridHelper.AddColumn( "WarehouseItemCode", "物料代码",	null);
			this.gridHelper.AddColumn( "WarehouseItemName", "物料名称",	null);
			this.gridHelper.AddColumn( "MOCode", "工单号",	null);
			this.gridHelper.AddColumn( "TicketQty", "单据数量",	null);
			this.gridHelper.AddColumn( "ActualQty", "实际数量",	null);

			this.gridHelper.AddDefaultColumn( false, false );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			DisplayTicketData();
		}

		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			WarehouseTicketDetail item = (WarehouseTicketDetail)obj;
			Infragistics.WebUI.UltraWebGrid.UltraGridRow row = 
				new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
				new object[]{
								item.Sequence,
								item.ItemCode,
								item.ItemName,
								item.MOCode,
								Math.Round(item.Qty, 2),
								Math.Round(item.ActualQty, 2)
							});
			item = null;
			return row;
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object[] objs = this._facade.QueryWarehouseTicketDetail( 
				string.Empty,  this.GetRequestParam("ticketno"),
				inclusive, exclusive );
			return objs;
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.QueryWarehouseTicketDetailCount(string.Empty, this.GetRequestParam("ticketno"));
		}

		private void DisplayTicketData()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			string strTicketNo = this.GetRequestParam("ticketno");
			object obj = this._facade.GetWarehouseTicket(strTicketNo);
			if (obj == null)
				return;
			WarehouseTicket tkt = (WarehouseTicket)obj;
			this.txtTicketNoQuery.Text = tkt.TicketNo;
			this.txtTicketUserQuery.Text = tkt.TicketUser;
			this.txtTicketDateQuery.Text = FormatHelper.ToDateString(tkt.TicketDate);
			this.txtFactoryFromQuery.Text = tkt.FactoryCode;
			//this.txtSegmentFromQuery.Text = tkt.SegmentCode;
			this.txtWarehouseFromQuery.Text = tkt.WarehouseCode;
			this.txtFactoryToQuery.Text = tkt.TOFactoryCode;
			//this.txtSegmentToQuery.Text = tkt.TOSegmentCode;
			this.txtWarehouseToQuery.Text = tkt.TOWarehouseCode;
			
			obj = this._facade.GetTransactionType(tkt.TransactionTypeCode);
			this.txtTransTypeQuery.Text = ((TransactionType)obj).TransactionTypeName;
			obj = null;
			tkt = null;

			this.cmdQuery_Click(null, null);
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl(this.GetRequestParam("returnurl"),new string[]{"ticketno"},new string[]{this.GetRequestParam("ticketno")}));
		}
		#endregion


		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			WarehouseTicketDetail item = (WarehouseTicketDetail)obj;
			string[] strArr = 
				new string[]{	item.Sequence.ToString(),
								item.ItemCode,
								item.ItemName,
								item.Qty.ToString(),
								item.ActualQty.ToString()
							};
			item = null;
			return strArr;
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"WarehouseItemSequence",
									"WarehouseItemCode",
									"WarehouseItemName",
									"TicketQty",
									"ActualQty"
								};
		}
		#endregion

		protected void FQueryTransDtlSP_PreRender(object sender, EventArgs e)
		{
			this.gridHelper.Grid.DblClick -= new ClickEventHandler(Grid_DblClick);
		}
	}
}
