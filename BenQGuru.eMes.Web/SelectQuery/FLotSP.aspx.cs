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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Web.UserControl;

namespace BenQGuru.eMES.Web.SelectQuery
{
    /// <summary>
    /// Selector 的摘要说明。
    /// </summary>
    public partial class FLotSP : BaseSingleSelectorPageNew
    {

        private BenQGuru.eMES.SelectQuery.SPFacade facade;// = new FacadeFactory(base.DataProvider).CreateSPFacade() ;


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

                this.drpOQCStateQuery.Items.Clear();

                foreach (string key in new OQCLotStatus().Items)
                {
                    this.drpOQCStateQuery.Items.Add(new ListItem(this.languageComponent1.GetString(key), key));
                }
                this.drpOQCStateQuery.Items.Insert(0, "");

                if (Request.QueryString["Status"] == null)
                {
                    this.drpOQCStateQuery.Enabled = true;
                    this.drpOQCStateQuery.SelectedIndex = 0;
                }
                else
                {
                    this.drpOQCStateQuery.Enabled = false;
                    this.drpOQCStateQuery.SelectedValue = Request.QueryString["Status"];
                }
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.gridUnSelected.Columns.FromKey("Selector_UnSelectedDesc").Hidden = true;
        }

        #endregion

        #region WebGrid
        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((BenQGuru.eMES.Domain.OQC.OQCLot)obj).LOTNO;
            row["Selector_UnSelectedDesc"] = "";
            return row;

        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QueryUnSelectedLot(this.txtLotNoQuery.Text, this.drpOQCStateQuery.SelectedValue, new string[0], inclusive, exclusive);
        }


        protected override int GetUnSelectedRowCount()
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QueryUnSelectedLotCount(this.txtLotNoQuery.Text, this.drpOQCStateQuery.SelectedValue, new string[0]);
        }


        #endregion





    }
}
