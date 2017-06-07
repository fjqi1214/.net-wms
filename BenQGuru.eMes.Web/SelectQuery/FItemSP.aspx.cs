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
using System.Runtime.Remoting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Report;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.SelectQuery;
using BenQGuru.eMES.Web.UserControl;


namespace BenQGuru.eMES.Web.SelectQuery
{
    /// <summary>
    /// Selector 的摘要说明。
    /// </summary>
    public partial class FItemSP : BaseSelectorPageNew
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
            }
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid2();
            this.gridSelectedHelper.AddColumn("Selector_SelectedCode", "已选择的项目", null);
            this.gridSelectedHelper.AddColumn("ModelCode", "产品别代码", null);
            this.gridSelectedHelper.AddColumn("ItemName", "产品名称", null);
            this.gridSelectedHelper.AddColumn("ItemDesc", "产品描述", null);
            this.gridSelectedHelper.AddDefaultColumn(true, false);
            this.gridSelectedHelper.ApplyLanguage(this.languageComponent1);

            base.InitWebGrid();
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedCode", "未选择的项目", null);
            this.gridUnSelectedHelper.AddColumn("ModelCode", "产品别代码", null);
            this.gridUnSelectedHelper.AddColumn("ItemName", "产品名称", null);
            this.gridUnSelectedHelper.AddColumn("ItemDesc", "产品描述", null);
            this.gridUnSelectedHelper.AddDefaultColumn(true, false);
            this.gridUnSelectedHelper.ApplyLanguage(this.languageComponent1);

            this.gridSelected.Columns["Selector_SelectedDesc"].Hidden = true;
            this.gridUnSelected.Columns["Selector_UnSelectedDesc"].Hidden = true;
        }

        protected override DataRow GetSelectedGridRow(object obj)
        {
            DataRow row = DtSourceSelected.NewRow();
            row["Selector_SelectedCode"] = ((BenQGuru.eMES.Domain.MOModel.ItemForQuery)obj).ItemCode;
            row["ModelCode"] = ((BenQGuru.eMES.Domain.MOModel.ItemForQuery)obj).ModelCode;
            row["ItemName"] = ((BenQGuru.eMES.Domain.MOModel.ItemForQuery)obj).ItemName;
            row["ItemDesc"] = ((BenQGuru.eMES.Domain.MOModel.ItemForQuery)obj).ItemDescription;
            return row;

        }

        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((BenQGuru.eMES.Domain.MOModel.ItemForQuery)obj).ItemCode;
            row["ModelCode"] = ((BenQGuru.eMES.Domain.MOModel.ItemForQuery)obj).ModelCode;
            row["ItemName"] = ((BenQGuru.eMES.Domain.MOModel.ItemForQuery)obj).ItemName;
            row["ItemDesc"] = ((BenQGuru.eMES.Domain.MOModel.ItemForQuery)obj).ItemDescription;
            return row;
        }

        protected override object[] LoadSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QuerySelectedItem(this.GetSelectedCodes());
            //string strugc = checkuser();
            //if (strugc != "")
            //{
            //    object[] obj = this.DataProvider.CustomQuery(typeof(ItemForQuery), new SQLCondition(string.Format(
            //            "select {0} from tblitem,tblmodel2item where 1=1 and tblitem.itemcode = tblmodel2item.itemcode " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode in ({1}) and itemcode in (select itemcode from usergroup2item where usergroupcode = '{2}') order by itemcode",
            //        DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemForQuery)),
            //        ("'" + string.Join("','", this.GetSelectedCodes()) + "'").ToUpper(),
            //        strugc.ToUpper()
            //            )));
            //    return obj;
            //}
        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            //string strugc = checkuser();
            //if (strugc != "")
            //{
            //    //object[] obj = this.DataProvider.CustomQuery(typeof(ItemForQuery), new SQLCondition(string.Format(
            //    //    "select {0} from tblitem,tblmodel2item where 1=1 and tblitem.itemcode = tblmodel2item.itemcode " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode like '{2}%' and itemname like '%{3}%' and itemdesc like '%{4}%' and itemcode in (select itemcode from usergroup2item where usergroupcode = '{1}') ",
            //    //    DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemForQuery)),
            //    //    strugc.ToUpper(),
            //    //    this.txtItemCodeQuery.Text.ToUpper(),
            //    //    this.txtItemNameQuery.Text,
            //    //    this.txtItemDescQuery.Text
            //    //    )));
            //    object[] obj = this.facade.QueryUnSelectedItem(this.txtItemCodeQuery.Text, this.txtModelCodeQuery.Text, this.txtItemNameQuery.Text, this.txtItemDescQuery.Text, strugc);
            //    return obj;

            //}
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QueryUnSelectedItem(this.txtModelCodeQuery.Text, this.txtItemCodeQuery.Text, this.txtItemNameQuery.Text, this.txtItemDescQuery.Text, this.GetSelectedCodes(), inclusive, exclusive);
        }


        protected override int GetUnSelectedRowCount()
        {

            //string strugc = checkuser();
            //if (strugc != "")
            //{
            //    return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from USERGROUP2ITEM where USERGROUPCODE ='{0}'", strugc)));
            //}
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QueryUnSelectedItemCount(this.txtModelCodeQuery.Text, this.txtItemCodeQuery.Text, this.txtItemNameQuery.Text, this.txtItemDescQuery.Text, this.GetSelectedCodes());
        }

        //protected string checkuser()
        //{
        //    object[] obj = this.DataProvider.CustomQuery(typeof(UserGroup), new SQLCondition(string.Format("select * from tblusergroup where usergrouptype='客户用户组' and usergroupcode in (select usergroupcode from tblusergroup2user  where usercode='{0}')", this.GetUserCode().ToString())));
        //    if (obj == null)
        //        return "";

        //    UserGroup ug = (UserGroup)obj[0];
        //    return ug.UserGroupCode.ToString();
        //}
        #endregion





    }
}
