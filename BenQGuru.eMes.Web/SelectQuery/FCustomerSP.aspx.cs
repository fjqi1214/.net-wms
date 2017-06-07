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

namespace BenQGuru.eMES.Web.SelectQuery
{
	/// <summary>
	/// FCustomerSP 的摘要说明。
	/// </summary>
	public partial class FCustomerSP : BaseSelectorPageNew
	{
	
		private BenQGuru.eMES.SelectQuery.SPFacade facade ;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
			}
			// 在此处放置用户代码以初始化页面
			//cmdQuery_ServerClick(null,null);
		}

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

		#region WebGrid
		protected override DataRow GetSelectedGridRow(object obj)
		{
            DataRow row = DtSourceSelected.NewRow();
            row["Selector_SelectedCode"] = ((BenQGuru.eMES.Domain.RMA.CusItemCodeCheckList)obj).CustomerCode;
            row["Selector_SelectedDesc"] = "";
            return row;
		}

		protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((BenQGuru.eMES.Domain.RMA.CusItemCodeCheckList)obj).CustomerCode;
            row["Selector_UnSelectedDesc"] = "";
            return row;
		}

		protected override object[] LoadSelectedDataSource(int inclusive, int exclusive)
		{
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateSPFacade() ;}
			return this.facade.QuerySelectedCusItemCodeCheckList( this.GetSelectedCodes() ) ;
		}

		protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
		{
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateSPFacade() ;}
			return this.facade.QueryUnCusItemCodeCheckList( this.txtCustomerQuery.Text, this.GetSelectedCodes(),inclusive,exclusive ) ;
		}


		protected override int GetUnSelectedRowCount()
		{
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateSPFacade() ;}
			return this.facade.QueryUnCusItemCodeCheckListCount( this.txtCustomerQuery.Text ,this.GetSelectedCodes() ) ;
		}

        
		#endregion

		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
		
		}
	}
}
