using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.SelectQuery;

namespace BenQGuru.eMES.Web.SelectQuery
{
    public partial class FCommonSingleSP : BaseSingleSelectorPageNew
    {
        private BenQGuru.eMES.SelectQuery.SPFacade _Facade;
        private SelectQueryInfo _SelectQueryInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.InitPageLanguage(this.languageComponent1, false);
            }

            _SelectQueryInfo = new SelectQueryInfo(this.Request.Params["Type"], this.Request.Params["Code"], this.Request.Params["Desc"]);
        }

        #region WebGrid

        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = _SelectQueryInfo.DomainObjectType.GetField(_SelectQueryInfo.CodeFieldName).GetValue(obj).ToString();
            row["Selector_UnSelectedDesc"] = _SelectQueryInfo.DomainObjectType.GetField(_SelectQueryInfo.DescFieldName).GetValue(obj).ToString();
            return row;
        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            if (_Facade == null)
            {
                _Facade = new FacadeFactory(base.DataProvider).CreateSPFacade();
            }
            return this._Facade.QueryUnselectedObject(_SelectQueryInfo, this.txtCodeQuery.Text, new string[0], inclusive, exclusive);
        }


        protected override int GetUnSelectedRowCount()
        {
            if (_Facade == null)
            {
                _Facade = new FacadeFactory(base.DataProvider).CreateSPFacade();
            }
            return this._Facade.QueryUnselectedObjectCount(_SelectQueryInfo, this.txtCodeQuery.Text, new string[0]);
        }

        #endregion
    }
}
