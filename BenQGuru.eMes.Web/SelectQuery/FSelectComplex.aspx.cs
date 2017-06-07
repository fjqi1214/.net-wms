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
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Web.UserControl;
using BenQGuru.eMES.ReportView;

namespace BenQGuru.eMES.Web.SelectQuery
{
    public partial class FSelectComplex : BaseSelectorPageNew
    {
        string dataSource = string.Empty;
        string dataCode = string.Empty;
        string dataDesc = string.Empty;
        string dataType = string.Empty;

        private BenQGuru.eMES.SelectQuery.SPFacade facade;//= FacadeFactory.CreateSPFacade() ;


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

            dataSource = Request.QueryString["datasource"];
            dataType = Request.QueryString["datatype"];
            if (dataType.ToUpper() != "STATIC")
            {
                BenQGuru.eMES.ReportView.ReportViewFacade rFacade = new BenQGuru.eMES.ReportView.ReportViewFacade(this.DataProvider);
                object[] objs = rFacade.QueryDataSourceById(dataSource);
                if(objs!=null && objs.Length>0)
                {
                    dataSource = ((BenQGuru.eMES.Domain.ReportView.RptViewDataSource)objs[0]).SQL;
                }
            }
            
            dataCode = Request.QueryString["datacode"];
            dataDesc = Request.QueryString["datadesc"];          
        }

        #endregion

        #region WebGrid
        protected override DataRow GetSelectedGridRow(object obj)
        {
            DataRow row = DtSourceSelected.NewRow();
            row["Selector_SelectedCode"] = ((BenQGuru.eMES.Domain.ReportView.SelectQuery)obj).Code;
            row["Selector_SelectedDesc"] = ((BenQGuru.eMES.Domain.ReportView.SelectQuery)obj).CodeDesc;
            return row;
        }

        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((BenQGuru.eMES.Domain.ReportView.SelectQuery)obj).Code;
            row["Selector_UnSelectedDesc"] = ((BenQGuru.eMES.Domain.ReportView.SelectQuery)obj).CodeDesc;
            return row;
        }

        protected override object[] LoadSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QuerySelectedData(dataSource, dataType, dataCode, dataDesc, this.GetSelectedCodes());
        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QueryUnSelectedData(dataSource,dataType,dataCode,dataDesc, this.txtQueryCodeQuery.Text,this.txtQueryDescQuery.Text, this.GetSelectedCodes(), inclusive, exclusive);
         
        }


        protected override int GetUnSelectedRowCount()
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }

            return this.facade.QueryUnSelectedDataCount(dataSource, dataType,dataCode,dataDesc, this.txtQueryCodeQuery.Text, this.txtQueryDescQuery.Text, this.GetSelectedCodes());
           
        }


        #endregion
    }
}
