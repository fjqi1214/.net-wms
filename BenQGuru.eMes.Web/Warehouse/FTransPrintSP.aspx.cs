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
	/// FShiftMP 的摘要说明。
	/// </summary>
	public partial class FTransPrintSP : BasePage
	{

		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		
		private BenQGuru.eMES.Material.WarehouseFacade _facade ;//= new BenQGuru.eMES.Material.WarehouseFacade();

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
			this.gridWebGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.gridWebGrid_ItemDataBound);

		}
		#endregion
			
		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				if (this.GetRequestParam("ticketno") == "")
				{
					throw new Exception("");
				}
				this.InitWebGrid();
			}
		}

		private ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}
		#endregion

		#region WebGrid
		private void InitWebGrid()
		{
			/*
			this.gridHelper.AddColumn( "WarehouseItemCode", "物料编号",	null);
			this.gridHelper.AddColumn( "WarehouseItemName", "物料名称",	null);
			this.gridHelper.AddColumn( "TransMOCode", "工单",	null);
			this.gridHelper.AddColumn( "ActualQty", "实际数量",	null);

			this.gridHelper.AddDefaultColumn( false, false );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
			*/

			/*
			for (int i = 0; i < 10; i++)
			{
				this.gridWebGrid.Columns.Add(new BoundColumn());
			}
			this.gridWebGrid.Columns[0].HeaderText = "No.";
			this.gridWebGrid.Columns[0].ItemStyle.Width = 20;
			this.gridWebGrid.Columns[1].HeaderText = "物料编号";
			this.gridWebGrid.Columns[1].ItemStyle.Width = 80;
			this.gridWebGrid.Columns[2].HeaderText = "物料名称";
			this.gridWebGrid.Columns[2].ItemStyle.Width = 150;
			this.gridWebGrid.Columns[3].HeaderText = "工单";
			this.gridWebGrid.Columns[3].ItemStyle.Width = 60;
			this.gridWebGrid.Columns[4].HeaderText = "尚欠数";
			this.gridWebGrid.Columns[4].ItemStyle.Width = 40;
			this.gridWebGrid.Columns[5].HeaderText = "1";
			this.gridWebGrid.Columns[5].ItemStyle.Width = 40;
			this.gridWebGrid.Columns[6].HeaderText = "2";
			this.gridWebGrid.Columns[6].ItemStyle.Width = 40;
			this.gridWebGrid.Columns[7].HeaderText = "3";
			this.gridWebGrid.Columns[7].ItemStyle.Width = 40;
			this.gridWebGrid.Columns[8].HeaderText = "良品退";
			this.gridWebGrid.Columns[8].ItemStyle.Width = 60;
			this.gridWebGrid.Columns[9].HeaderText = "不良品退";
			this.gridWebGrid.Columns[9].ItemStyle.Width = 60;
			*/
			this.DoDataInit();
		}
		
		private Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			WarehouseTicketDetail item = (WarehouseTicketDetail)obj;
			Infragistics.WebUI.UltraWebGrid.UltraGridRow row = new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								item.Sequence,
								item.ItemCode,
								item.ItemName,
								item.MOCode,
								Math.Round(item.Qty, 2).ToString(),
								""
								});
			item = null;
			return row;
		}


		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.QueryWarehouseTicketDetail3( 
				string.Empty,
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.GetRequestParam("ticketno"))),
				inclusive, exclusive );
		}

		private int GetRowCount()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.QueryWarehouseTicketDetailCount( 
				string.Empty,
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.GetRequestParam("ticketno"))));
		}

		private object[] objsDataSource;
		private void DoDataInit()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object obj = this._facade.GetWarehouseTicket(this.GetRequestParam("ticketno"));
			if (obj == null)
			{
				throw new Exception("");
			}
			txtPrintTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			WarehouseTicket ticket = (WarehouseTicket)obj;
			lblTicketNo.Text = "NO: " + ticket.TicketNo;
			txtFactoryFrom.Text = ticket.FactoryCode;
			//txtSegmentFrom.Text = ticket.SegmentCode;
			txtWarehouseFrom.Text = ticket.WarehouseCode;
			txtFactoryTo.Text = ticket.TOFactoryCode;
			//txtSegmentTo.Text = ticket.TOSegmentCode;
			txtWarehouseTo.Text = ticket.TOWarehouseCode;

			txtCreateUser.Text = this.GetUserCode();

			object obj1 = this._facade.GetTransactionType(ticket.TransactionTypeCode);
			string strType = ((TransactionType)obj1).TransactionTypeName;
			obj1 = null;
			ticket = null;
			lblSubTitle.Text = strType;

			//this.cmdQuery_Click(null, null);
			objsDataSource = this.LoadDataSource(0, int.MaxValue);
			this.gridWebGrid.AllowCustomPaging = true;
			this.gridWebGrid.VirtualItemCount = this.GetRowCount();
			this.gridWebGrid.DataSource = objsDataSource;
			this.gridWebGrid.DataBind();
		}

		private void gridWebGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex >= 0)
			{
				WarehouseTicketDetail item = (WarehouseTicketDetail)objsDataSource[e.Item.ItemIndex];
				e.Item.Cells[0].Text = item.Sequence.ToString();
				e.Item.Cells[1].Text = item.ItemCode;
				e.Item.Cells[2].Text = item.ItemName;
				e.Item.Cells[3].Text = item.MOCode;
				e.Item.Cells[4].Text = item.Qty.ToString();
				//e.Item.Cells[5].Text = item.ActualQty.ToString();
				//e.Item.Cells[6].Text = (item.Qty - item.ActualQty).ToString();
				//e.Item.Cells[6].Text = e.Item.Cells[4].Text.TrimEnd('0');
				//e.Item.Cells[6].Text = e.Item.Cells[4].Text.TrimEnd('.');
			}
		}
		#endregion

	}
}
