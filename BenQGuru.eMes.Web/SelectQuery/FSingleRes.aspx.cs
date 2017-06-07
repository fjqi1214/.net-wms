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
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.SelectQuery
{
    public partial class FSingleRes : BaseSingleSelectorPageNew
    {
        private BenQGuru.eMES.SelectQuery.SPFacade facade;//= FacadeFactory.CreateSPFacade() ;
        private string ssCode = "";


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
            ssCode = "";
            if (Request.QueryString["ssCode"] != null)
            {
                ssCode = Request.QueryString["ssCode"];
                
            }
        }

        #endregion

        #region WebGrid
       
        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((BenQGuru.eMES.Domain.BaseSetting.Resource)obj).ResourceCode;
            row["Selector_UnSelectedDesc"] = ((BenQGuru.eMES.Domain.BaseSetting.Resource)obj).ResourceDescription;
            return row;
        }

   
        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            //if (ssCode == null || ssCode.Length == 0)
            //{
            //    return this.facade.QueryUnSelectedResource(string.Empty, this.txtResourceCodeQuery.Text, new string[0], inclusive, exclusive);
            //}
            return this.facade.QueryUnSelectedResource(ssCode, this.txtResourceCodeQuery.Text, new string[0], inclusive, exclusive);
        }


        protected override int GetUnSelectedRowCount()
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            //if (ssCode == null || ssCode.Length == 0)
            //{
            //    return this.facade.QueryUnSelectedResourceCount(string.Empty, this.txtResourceCodeQuery.Text, new string[0]);
            //}
            return this.facade.QueryUnSelectedResourceCount(ssCode, this.txtResourceCodeQuery.Text, new string[0]);
        }


        #endregion
    }
}
