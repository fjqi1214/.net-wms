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
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Web.UserControl;

namespace BenQGuru.eMES.Web.SelectQuery
{
    /// <summary>
    /// FSingleItemSP 的摘要说明。
    /// </summary>
    public partial class FSingleWWpoInvNoSP : BaseSingleSelectorPageNew
    {

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
            if (Request.QueryString["invCode"] != null)
            {
                txtYLInvNoQuery.Text = Request.QueryString["invCode"];
            }
        }

        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedCode", "鼎桥物料编码", null);
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedPOLine", "SAP单据行号", null);
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedSubLine", "SubLine", null);
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedMCode", "物料代码", null);
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedDQMCode", "鼎桥物料编码", null);
            this.gridUnSelectedHelper.AddColumn("Selector_UnSelectedMdes", "物料描述", null);
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedQty", "数量", null);
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedUnit", "单位", null);
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedHWMCode", "华为物料", null);
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedCPDQMCode", "成品物料编码", null);
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedCPMDesc", "成品物料描述", null);
            this.gridUnSelectedHelper.AddDefaultColumn(true, false);
            //this.gridUnSelectedHelper.ApplyLanguage(this.languageComponent1);
            this.gridUnSelected.Columns["Selector_UnselectedCode"].Hidden = true;
            this.gridUnSelected.Columns["Selector_UnSelectedDesc"].Hidden = true;
        }


        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((MesWWPOExc)obj).DQMCode + "," + ((MesWWPOExc)obj).POLine.ToString();
            row["Selector_UnselectedPOLine"] = ((MesWWPOExc)obj).POLine;
            row["Selector_UnselectedSubLine"] = ((MesWWPOExc)obj).SubLine;
            row["Selector_UnselectedMCode"] = ((MesWWPOExc)obj).MCode;
            row["Selector_UnselectedDQMCode"] = ((MesWWPOExc)obj).DQMCode;
            row["Selector_UnSelectedMdes"] = ((MesWWPOExc)obj).MChLongDesc;
            row["Selector_UnselectedQty"] = ((MesWWPOExc)obj).Qty;
            row["Selector_UnselectedUnit"] = ((MesWWPOExc)obj).Unit;
            row["Selector_UnselectedHWMCode"] = ((MesWWPOExc)obj).HWMCode;
            row["Selector_UnselectedCPDQMCode"] = ((MesWWPOExc)obj).CPDQMCode;
            row["Selector_UnselectedCPMDesc"] = ((MesWWPOExc)obj).CPMDesc;
            return row;
        }


        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QueryUnwWpoInvNo(
            FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtYLInvNoQuery.Text)),
            FormatHelper.CleanString(this.txtDQMoCodeQuery.Text),
            FormatHelper.CleanString(this.txtMaterialDescQuery.Text),
           new string[0],
            inclusive,
            exclusive);
        }


        protected override int GetUnSelectedRowCount()
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QueryUNWWpoInvNoCount(
        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtYLInvNoQuery.Text)),
       FormatHelper.CleanString(this.txtDQMoCodeQuery.Text),
       FormatHelper.CleanString(this.txtMaterialDescQuery.Text),
 
        new string[0]);

        }


        #endregion

    }
}
