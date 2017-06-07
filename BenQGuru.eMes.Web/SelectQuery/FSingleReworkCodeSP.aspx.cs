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
using BenQGuru.eMES.Domain.Rework;

namespace BenQGuru.eMES.Web.SelectQuery
{
    public partial class FSingleReworkCodeSP : BaseSingleSelectorPageNew
    {
        private BenQGuru.eMES.SelectQuery.SPFacade facade;
        private string ReworkCode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
            }
        }

        protected override void InitWebGrid()
        {
            base.InitWebGrid();

            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedCode", "未选择的项目", null);
            this.gridUnSelectedHelper.AddColumn("ReworkSheetStatus", "状态", null);
            this.gridUnSelectedHelper.AddColumn("ReworkType", "返工类型", null);
            this.gridUnSelectedHelper.AddDefaultColumn(true, false);
            this.gridUnSelectedHelper.ApplyLanguage(this.languageComponent1);
            this.gridUnSelected.Columns.FromKey("Selector_UnselectedDesc").Hidden = true;
        }

        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((ReworkSheet)obj).ReworkCode.ToString();
            row["ReworkSheetStatus"] = this.languageComponent1.GetString(((ReworkSheet)obj).Status);
            row["ReworkType"] = this.languageComponent1.GetString(((ReworkSheet)obj).ReworkType);
            return row;
        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }

            return facade.QueryUnSelectedReworkCode(FormatHelper.PKCapitalFormat(this.txtReworkSheetCode.Text.Trim()),
                new string[0], inclusive, exclusive);
        }

        protected override int GetUnSelectedRowCount()
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return facade.QueryUnSelectedReworkCodeCount(FormatHelper.PKCapitalFormat(this.txtReworkSheetCode.Text.Trim()),
                new string[0]);
        }
    }
}
