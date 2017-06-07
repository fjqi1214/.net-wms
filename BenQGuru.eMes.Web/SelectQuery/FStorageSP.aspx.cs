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
using BenQGuru.eMES.Web.Helper ;
using BenQGuru.eMES.Web.UserControl ;

namespace BenQGuru.eMES.Web.SelectQuery
{
	/// <summary>
	/// Selector 的摘要说明。
	/// </summary>
	public partial class FStorageSP : BaseSelectorPageNew
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

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            base.InitWebGrid2();
            this.gridSelectedHelper.AddColumn("Selector_SelectedCode", "已选择的项目", null);
            this.gridSelectedHelper.AddColumn("Selector_SelectedDesc", "描述", null);
            this.gridSelectedHelper.AddDefaultColumn(true, false);
            this.gridSelectedHelper.ApplyLanguage(this.languageComponent1);

            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedCode", "未选择的项目", null);
            this.gridUnSelectedHelper.AddColumn("Selector_UnSelectedDesc", "描述", null);
            this.gridUnSelectedHelper.AddDefaultColumn(true, false);
            this.gridUnSelectedHelper.ApplyLanguage(this.languageComponent1);

            //this.gridSelectedHelper.Grid.DisplayLayout.ClientSideEvents.CellClickHandler = "RowSelect";
            //this.gridUnSelectedHelper.Grid.DisplayLayout.ClientSideEvents.CellClickHandler = "RowSelect";

            this.gridSelected.Columns.FromKey("Selector_SelectedDesc").Hidden = true;
            this.gridUnSelected.Columns.FromKey("Selector_UnSelectedDesc").Hidden = true;
        }

        protected override DataRow GetSelectedGridRow(object obj)
        {
            DataRow row = DtSourceSelected.NewRow();
            row["Selector_SelectedCode"] = ((BenQGuru.eMES.Domain.Warehouse.Storage)obj).StorageCode;
            row["Selector_SelectedDesc"] = ((BenQGuru.eMES.Domain.Warehouse.Storage)obj).StorageCode;
            return row;
        }

        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((BenQGuru.eMES.Domain.Warehouse.Storage)obj).StorageCode;
            row["Selector_UnSelectedDesc"] = ((BenQGuru.eMES.Domain.Warehouse.Storage)obj).StorageCode;
            return row;
        }

        protected override object[] LoadSelectedDataSource(int inclusive, int exclusive)
        {
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateSPFacade() ;}
            return this.facade.QuerySelectedStorage(this.GetSelectedCodes());
        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateSPFacade() ;}
            return this.facade.QueryUnSelectedStorage(this.txtStorageQuery.Text, this.GetSelectedCodes(), inclusive, exclusive);
        }


        protected override int GetUnSelectedRowCount()
        {
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateSPFacade() ;}
            return this.facade.QueryUnSelectedStorageCount(this.txtStorageQuery.Text, this.GetSelectedCodes());
        }
        
        #endregion



	}
}
