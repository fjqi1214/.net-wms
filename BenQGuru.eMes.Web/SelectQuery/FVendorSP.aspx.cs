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

namespace BenQGuru.eMES.Web.SelectQuery
{
    public partial class FVendorSP : BaseSelectorPageNew
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
        protected override DataRow GetSelectedGridRow(object obj)
        {
            DataRow row = DtSourceSelected.NewRow();
            row["Selector_SelectedCode"] = ((Vendor)obj).VendorCode;
            row["Selector_SelectedDesc"] = ((Vendor)obj).VendorName;
            return row;
        }

        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((Vendor)obj).VendorCode;
            row["Selector_UnSelectedDesc"] = ((Vendor)obj).VendorName;
            return row;
        }

        protected override object[] LoadSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) 
            { 
                facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); 
            }

            return this.facade.QuerySelectedVendorCode(this.GetSelectedCodes());

        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) 
            { 
                facade = new FacadeFactory(base.DataProvider).CreateSPFacade();
            }

            return this.facade.QueryUNSelectVendorCode(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVendorCode.Text)), 
                FormatHelper.CleanString(this.txtVendorDescription.Text), 
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

            return this.facade.QueryUNSelectVendorCodeCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVendorCode.Text)), 
                FormatHelper.CleanString(this.txtVendorDescription.Text), 
                this.GetSelectedCodes());
        }

        #endregion    
    
    }
}
