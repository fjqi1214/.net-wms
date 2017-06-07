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
using BenQGuru.eMES.Common ;
using BenQGuru.eMES.Web.Helper ;
using BenQGuru.eMES.Web.UserControl ;

using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.Web.SelectQuery
{
	/// <summary>
	/// FWHItemSP 的摘要说明。
	/// </summary>
	public partial class FWHItemSP : BaseSelectorPageNew
	{

		private BenQGuru.eMES.SelectQuery.SPFacade facade ;//= FacadeFactory.CreateSPFacade() ;


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

		}
		#endregion

		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
			}
		}

		#endregion

		#region WebGrid
		protected override DataRow GetSelectedGridRow(object obj)
		{
			WarehouseItem item = (WarehouseItem)obj;
            DataRow row = DtSourceSelected.NewRow();
            row["Selector_SelectedCode"] = item.ItemCode;
            row["Selector_SelectedDesc"] = item.ItemName;
            return row;
		}

		protected override DataRow GetUnSelectedGridRow(object obj)
		{
			WarehouseItem item = (WarehouseItem)obj;
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = item.ItemCode;
            row["Selector_UnSelectedDesc"] = item.ItemName;
            return row;
		}

		protected override object[] LoadSelectedDataSource(int inclusive, int exclusive)
		{
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateSPFacade() ;}
			return this.facade.QuerySelectedWarehouseItem( this.GetSelectedCodes() ) ;
		}

		protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
		{
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateSPFacade() ;}
			return this.facade.QueryUnSelectedWarehouseItem(string.Empty, this.txtItemCodeQuery.Text ,this.txtItemNameQuery.Text, this.GetSelectedCodes(),inclusive,exclusive ) ;
		}


		protected override int GetUnSelectedRowCount()
		{
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateSPFacade() ;}
			return this.facade.QueryUnSelectedWarehouseItemCount(string.Empty, this.txtItemCodeQuery.Text ,this.txtItemNameQuery.Text , this.GetSelectedCodes() ) ;
		}

        
		#endregion





	}
}
