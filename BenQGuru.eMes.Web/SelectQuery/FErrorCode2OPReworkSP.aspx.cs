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
using BenQGuru.eMES.SelectQuery;
using System.Collections.Generic;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.SelectQuery
{
    /// <summary>
    /// FErrorCodeSP 的摘要说明。
    /// </summary>
    public partial class FErrorCode2OPReworkSP : BaseSelectorPageNew
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
            this.Setpostback();
        }

        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid2();
            this.gridSelectedHelper.AddColumn("Selector_SelectedCode", "不良代码", null);
            this.gridSelectedHelper.AddColumn("SelectedECDesc", "不良代码描述", null);
            this.gridSelectedHelper.AddColumn("SelectedECGCode", "不良代码组", null);
            this.gridSelectedHelper.AddColumn("SelectedECGDesc", "不良代码组描述", null);
            this.gridSelectedHelper.AddDefaultColumn(true, false);
            this.gridSelectedHelper.ApplyLanguage(this.languageComponent1);

            base.InitWebGrid();
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedCode", "不良代码", null);
            this.gridUnSelectedHelper.AddColumn("UnSelectedECDesc", "不良代码描述", null);
            this.gridUnSelectedHelper.AddColumn("UnSelectedECGCode", "不良代码组", null);
            this.gridUnSelectedHelper.AddColumn("UnSelectedECGDesc", "不良代码组描述", null);
            this.gridUnSelectedHelper.AddDefaultColumn(true, false);
            this.gridUnSelectedHelper.ApplyLanguage(this.languageComponent1);

            this.gridSelected.Columns["Selector_SelectedDesc"].Hidden = true;
            this.gridUnSelected.Columns["Selector_UnSelectedDesc"].Hidden = true;
        }

        protected override DataRow GetSelectedGridRow(object obj)
        {
            ErrorGroup2CodeSelect item = (ErrorGroup2CodeSelect)obj;
            DataRow row = DtSourceSelected.NewRow();
            row["Selector_SelectedCode"] = item.ErrorCode;
            row["SelectedECDesc"] = item.ErrorDescription;
            row["SelectedECGCode"] = item.ErrorCodeGroup;
            row["SelectedECGDesc"] = item.ErrorCodeGroupDescription;
            return row;
        }

        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            ErrorGroup2CodeSelect item = (ErrorGroup2CodeSelect)obj;
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = item.ErrorCode;
            row["UnSelectedECDesc"] = item.ErrorDescription;
            row["UnSelectedECGCode"] = item.ErrorCodeGroup;
            row["UnSelectedECGDesc"] = item.ErrorCodeGroupDescription;
            return row;
        }

        protected override object[] LoadSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new FacadeFactory(base.DataProvider).CreateSPFacade();
            }
            return this.facade.QuerySelectedErrorCode2OPRework(this.GetSelectedCodes());
        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new FacadeFactory(base.DataProvider).CreateSPFacade();
            }
            return this.facade.QueryUnSelectedErrorCode2OPRework(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtErrorCodeQuery.Text)), FormatHelper.CleanString(this.txtErrorCodeDescQuery.Text), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtErrorCodeGroupQuery.Text)), FormatHelper.CleanString(this.txtErrorCodeGroupDescQuery.Text), this.GetSelectedCodes(), inclusive, exclusive);
        }


        protected override int GetUnSelectedRowCount()
        {
            if (facade == null)
            {
                facade = new FacadeFactory(base.DataProvider).CreateSPFacade();
            }
            return this.facade.QueryUnSelectedErrorCode2OPReworkCount(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtErrorCodeQuery.Text)), FormatHelper.CleanString(this.txtErrorCodeDescQuery.Text), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtErrorCodeGroupQuery.Text)), FormatHelper.CleanString(this.txtErrorCodeGroupDescQuery.Text), this.GetSelectedCodes());
        }

        protected override void AddNewRow(ArrayList rows)
        {
            foreach (GridRecord row in rows)
            {
                DataRow newrow = DtSourceSelected.NewRow();
                newrow["GUID"] = row.Items.FindItemByKey("GUID").Value;
                newrow["Selector_SelectedCode"] = row.Items.FindItemByKey("Selector_UnselectedCode").Value;
                newrow["SelectedECDesc"] = row.Items.FindItemByKey("UnSelectedECDesc").Value;
                newrow["SelectedECGCode"] = row.Items.FindItemByKey("UnSelectedECGCode").Value;
                newrow["SelectedECGDesc"] = row.Items.FindItemByKey("UnSelectedECGDesc").Value;
                this.DtSourceSelected.Rows.Add(newrow);
            }
            this.gridSelectedHelper.Grid.DataSource = this.DtSourceSelected;
            this.gridSelectedHelper.Grid.DataBind();
        }

        #endregion

        private void Setpostback()
        {
            if (this.pagePostBackCount.Value == string.Empty)
            {
                this.pagePostBackCount.Value = "1";
            }
            else
            {
                int count = int.Parse(this.pagePostBackCount.Value) + 1;
                this.pagePostBackCount.Value = count.ToString();
            }
        }
    }
}
