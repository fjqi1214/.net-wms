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
    public partial class FSingleRouteSP : BaseSingleSelectorPageNew
    {
        private BenQGuru.eMES.SelectQuery.SPFacade facade;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // ³õÊ¼»¯Ò³ÃæÓïÑÔ
                this.InitPageLanguage(this.languageComponent1, false);
            }
        }

        #region WebGrid

        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((BenQGuru.eMES.Domain.BaseSetting.Route)obj).RouteCode;
            row["Selector_UnSelectedDesc"] = ((BenQGuru.eMES.Domain.BaseSetting.Route)obj).RouteDescription;
            return row;
        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }

            object[] objs = this.facade.QueryUnSelectedRoute(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRouteCodeQuery.Text)),
                new string[0],
                inclusive, exclusive);
            ArrayList list = new ArrayList();
            if (objs != null && objs.Length > 0)
                list.AddRange(objs);
            //Domain.BaseSetting.Route route = new BenQGuru.eMES.Domain.BaseSetting.Route();
            //route.RouteCode = "TS";
            //route.RouteDescription = "TS";
            //list.Add(route);
            objs = new object[list.Count];
            list.CopyTo(objs);
            return objs;
        }


        protected override int GetUnSelectedRowCount()
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }

            return this.facade.QueryUnSelectedRouteCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRouteCodeQuery.Text)),
                new string[0]) + 1;
        }

        #endregion
    }
}
