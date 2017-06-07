using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.Web.SelectQuery
{
    public partial class FInvNoSP : BaseSelectorPageNew
    {
        private BenQGuru.eMES.SelectQuery.SPFacade facade;

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
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
            }
        }

        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            this.gridSelectedHelper.AddColumn("Selector_SelectedInvNo", "预留单号", null);
            this.gridSelectedHelper.AddColumn("Selector_SelectedContact", "联系人", null);
            this.gridSelectedHelper.AddColumn("Selector_SelectedStorage", "库位", null);
            this.gridSelectedHelper.AddDefaultColumn(true, false);
            this.gridSelectedHelper.ApplyLanguage(this.languageComponent1);

            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedInvNo", "预留单号", null);
            this.gridUnSelectedHelper.AddColumn("Selector_UnSelectedContact", "联系人", null);
            this.gridUnSelectedHelper.AddColumn("Selector_UnSelectedStorage", "库位", null);
            this.gridUnSelectedHelper.AddDefaultColumn(true, false);
            this.gridUnSelectedHelper.ApplyLanguage(this.languageComponent1);

           
          
        }
        protected override DataRow GetSelectedGridRow(object obj)
        {
            DataRow row = DtSourceSelected.NewRow();
            row["Selector_SelectedInvNo"] = ((InvoicesDetail)obj).InvNo;
            row["Selector_SelectedContact"] = ((InvoicesDetail)obj).ReceiverUser;
            row["Selector_SelectedStorage"] = ((InvoicesDetail)obj).FromStorageCode;
            return row;
        }

        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedInvNo"] = ((InvoicesDetail)obj).InvNo;
            row["Selector_UnSelectedContact"] = ((InvoicesDetail)obj).ReceiverUser;
            row["Selector_UnSelectedStorage"] = ((InvoicesDetail)obj).FromStorageCode;
            return row;
        }

        protected override object[] LoadSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) 
            { 
                facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); 
            }

            return this.facade.QuerySelectedInvNo(this.GetSelectedCodes());

        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) 
            { 
                facade = new FacadeFactory(base.DataProvider).CreateSPFacade();
            }

            return this.facade.QueryUNSelectInvNo(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtYLInvNoQuery.Text)),
                FormatHelper.CleanString(this.txtContactQuery.Text),
                FormatHelper.CleanString(this.txtStorageQuery.Text), 
                this.GetSelectedCodes(), 
                inclusive, 
                exclusive);
        }


        protected override int GetUnSelectedRowCount()
        {
            if (facade == null) 
            { 
                facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); 
            }

            return this.facade.QueryUNSelectInvNoCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtYLInvNoQuery.Text)),
                FormatHelper.CleanString(this.txtContactQuery.Text),
                FormatHelper.CleanString(this.txtStorageQuery.Text),
                this.GetSelectedCodes());
        }

        #endregion    
    
    }
}
