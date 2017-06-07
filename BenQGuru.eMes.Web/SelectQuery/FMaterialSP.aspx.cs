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
    public partial class FMaterialSP : BaseSelectorPageNew
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
            base.InitWebGrid2();
            this.gridSelectedHelper.AddColumn("Selector_SelectedCode", "已选择的项目", null);
            this.gridSelectedHelper.AddColumn("MaterialName", "物料名称", null);
            this.gridSelectedHelper.AddColumn("MaterialDes", "物料描述", null);
            this.gridSelectedHelper.AddDefaultColumn(true, false);
            this.gridSelectedHelper.ApplyLanguage(this.languageComponent1);

            base.InitWebGrid();
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedCode", "未选择的项目", null);
            this.gridUnSelectedHelper.AddColumn("MaterialName", "物料名称", null);
            this.gridUnSelectedHelper.AddColumn("MaterialDes", "物料描述", null);
            this.gridUnSelectedHelper.AddDefaultColumn(true, false);
            this.gridUnSelectedHelper.ApplyLanguage(this.languageComponent1);

            this.gridSelected.Columns["Selector_SelectedDesc"].Hidden = true;
            this.gridUnSelected.Columns["Selector_UnSelectedDesc"].Hidden = true;
        }


        protected override DataRow GetSelectedGridRow(object obj)
        {
            DataRow row = DtSourceSelected.NewRow();
            row["Selector_SelectedCode"] = ((BenQGuru.eMES.Domain.MOModel.Material)obj).MCode ;
            row["MaterialName"] =  ((BenQGuru.eMES.Domain.MOModel.Material)obj).MchshortDesc;
            row["MaterialDes"] = ((BenQGuru.eMES.Domain.MOModel.Material)obj).MenshortDesc;
            return row;
        }

        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((BenQGuru.eMES.Domain.MOModel.Material)obj).MCode;
            row["MaterialName"] = ((BenQGuru.eMES.Domain.MOModel.Material)obj).MchshortDesc;
            row["MaterialDes"] = ((BenQGuru.eMES.Domain.MOModel.Material)obj).MenshortDesc;
            return row;
        }

        protected override object[] LoadSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new FacadeFactory(base.DataProvider).CreateSPFacade();
            }

            return this.facade.QuerySelectedMaterial(this.GetSelectedCodes());

        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new FacadeFactory(base.DataProvider).CreateSPFacade();
            }

            return this.facade.QueryUnSelectedMaterial(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialCodeQuery.Text)),
                FormatHelper.CleanString(this.txtMaterialNameQuery.Text),
                FormatHelper.CleanString(this.txtMaterialDescQuery.Text),
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

            return this.facade.QueryUnSelectedMaterialCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialCodeQuery.Text)),
                FormatHelper.CleanString(this.txtMaterialNameQuery.Text),
                FormatHelper.CleanString(this.txtMaterialDescQuery.Text),
                this.GetSelectedCodes());
        }

        #endregion

    }
}
