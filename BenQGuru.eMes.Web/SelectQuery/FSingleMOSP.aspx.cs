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
	/// FSingleMOSP 的摘要说明。
	/// </summary>
	public partial class FSingleMOSP : BaseSingleSelectorPageNew
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

			if ( this.GetRequestParam("frompage") == "FOnWipQP" )
			{	
				this.txtItemCodeQuery.Text = this.GetRequestParam("itemcode");
				this.txtItemCodeQuery.ReadOnly = true;

				this.lblModelCodeQuery.Visible = false;
				this.txtModelCodeQuery.Visible = false;
			}
		}

		#endregion

		#region WebGrid
		protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((BenQGuru.eMES.Domain.MOModel.MO)obj).MOCode;
            row["Selector_UnSelectedDesc"] = ((BenQGuru.eMES.Domain.MOModel.MO)obj).MODescription;
            return row;
		}

		protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
		{
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateSPFacade() ;}
			if ( this.GetRequestParam("frompage") == "FOnWipQP" )
			{
				return this.facade.QueryUnSelectedMOByItemCode( 
					FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtItemCodeQuery.Text) ),
					FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtMOCodeQuery.Text) ), 
					MOManufactureStatus.MOSTATUS_OPEN + "," + MOManufactureStatus.MOSTATUS_PENDING,						
					new string[0],
					inclusive, exclusive ) ;
			}

			return this.facade.QueryUnSelectedMO( 
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtModelCodeQuery.Text) ),
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtItemCodeQuery.Text) ),
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtMOCodeQuery.Text) ), 
				new string[0],
				inclusive, exclusive ) ;
		}


		protected override int GetUnSelectedRowCount()
		{
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateSPFacade() ;}
			if ( this.GetRequestParam("frompage") == "FOnWipQP" )
			{
				return this.facade.QueryUnSelectedMOByItemCodeCount( 
					FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtItemCodeQuery.Text) ),
					FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtMOCodeQuery.Text) ), 
					MOManufactureStatus.MOSTATUS_OPEN + "," + MOManufactureStatus.MOSTATUS_PENDING,						
					new string[0] ) ;
			}

			return this.facade.QueryUnSelectedMOCount( 
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtModelCodeQuery.Text) ),
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtItemCodeQuery.Text) ),
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtMOCodeQuery.Text) ), 
				new string[0] ) ;
		}
        
		#endregion
		
	}
}
