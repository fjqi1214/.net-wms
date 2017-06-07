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
	/// FUserSP 的摘要说明。
	/// </summary>
	public partial class FUserMailSP : BaseSelectorPageNew
	{
	
		private BenQGuru.eMES.SelectQuery.SPFacade _facade = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
			}
		}

		private BenQGuru.eMES.SelectQuery.SPFacade facade
		{
			get
			{
				if(_facade == null)
					_facade = new FacadeFactory(base.DataProvider).CreateSPFacade();

				return _facade;
			}
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
            row["Selector_SelectedCode"] = ((BenQGuru.eMES.Domain.BaseSetting.User)obj).UserCode;
            row["UserName"] = ((BenQGuru.eMES.Domain.BaseSetting.User)obj).UserName;
            row["EMail"] = ((BenQGuru.eMES.Domain.BaseSetting.User)obj).UserEmail;
            return row;
		}

		protected override DataRow GetUnSelectedGridRow(object obj)
		{
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((BenQGuru.eMES.Domain.BaseSetting.User)obj).UserCode;
            row["UserName"] = ((BenQGuru.eMES.Domain.BaseSetting.User)obj).UserName;
            row["EMail"] = ((BenQGuru.eMES.Domain.BaseSetting.User)obj).UserEmail;
            return row;
		}

		protected override object[] LoadSelectedDataSource(int inclusive, int exclusive)
		{
			return this.facade.QuerySelectedUserMail( this.GetSelectedCodes() ) ;
		}

		protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
		{
            return this.facade.QueryUnSelectedUserMail(this.txtDepartmentQuery.Text, this.txtUserCodeQuery.Text, this.txtUserNameQuery.Text, this.GetSelectedCodes(), inclusive, exclusive);
		}


		protected override int GetUnSelectedRowCount()
		{
            return this.facade.QueryUnSelectedUserMailCount(this.txtDepartmentQuery.Text, this.txtUserCodeQuery.Text, this.txtUserNameQuery.Text, this.GetSelectedCodes());
		}

        protected override void InitWebGrid()
        {
            base.InitWebGrid2();
            this.gridSelectedHelper.AddColumn("Selector_SelectedCode", "已选择的项目", null);
            this.gridSelectedHelper.AddColumn("UserName", "用户姓名", null);
            this.gridSelectedHelper.AddColumn("EMail", "电子邮件", null);            
            this.gridSelectedHelper.AddDefaultColumn(true, false);
            this.gridSelectedHelper.ApplyLanguage(this.languageComponent1);
            base.InitWebGrid();
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedCode", "未选择的项目", null);
            this.gridUnSelectedHelper.AddColumn("UserName", "用户姓名", null);
            this.gridUnSelectedHelper.AddColumn("EMail", "电子邮件", null);            
            this.gridUnSelectedHelper.AddDefaultColumn(true, false);
            this.gridUnSelectedHelper.ApplyLanguage(this.languageComponent1);

           // this.gridSelectedHelper.Grid.DisplayLayout.ClientSideEvents.CellClickHandler = "RowSelect";
           // this.gridUnSelectedHelper.Grid.DisplayLayout.ClientSideEvents.CellClickHandler = "RowSelect";
        }

		#endregion

	}
}
