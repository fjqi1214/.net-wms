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
using BenQGuru.eMES.Common ;
using BenQGuru.eMES.Web.Helper ;
using BenQGuru.eMES.Web.UserControl ;
using BenQGuru.eMES.SelectQuery;

namespace BenQGuru.eMES.Web.SelectQuery
{
	/// <summary>
	/// Selector 的摘要说明。
	/// </summary>
	public partial class FCommonMultiSP : BaseSelectorPageNew
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

        protected override DataRow GetSelectedGridRow(object obj)
        {
            DataRow row = DtSourceSelected.NewRow();
            row["Selector_SelectedCode"] = _SelectQueryInfo.DomainObjectType.GetField(_SelectQueryInfo.CodeFieldName).GetValue(obj).ToString();
            row["Selector_SelectedDesc"] = _SelectQueryInfo.DomainObjectType.GetField(_SelectQueryInfo.DescFieldName).GetValue(obj).ToString();
            return row;
        }

        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = _SelectQueryInfo.DomainObjectType.GetField(_SelectQueryInfo.CodeFieldName).GetValue(obj).ToString();
            row["Selector_UnSelectedDesc"] = _SelectQueryInfo.DomainObjectType.GetField(_SelectQueryInfo.DescFieldName).GetValue(obj).ToString();
            return row;
        }

        protected override object[] LoadSelectedDataSource(int inclusive, int exclusive)
        {
            if (_Facade == null)
            {
                _Facade = new FacadeFactory(base.DataProvider).CreateSPFacade();
            }
            return this._Facade.QuerySelectedObject(_SelectQueryInfo, this.GetSelectedCodes());
        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            if (_Facade == null)
            {
                _Facade = new FacadeFactory(base.DataProvider).CreateSPFacade();
            }
            return this._Facade.QueryUnselectedObject(_SelectQueryInfo, this.txtCodeQuery.Text, this.GetSelectedCodes(), inclusive, exclusive);
        }


        protected override int GetUnSelectedRowCount()
        {
            if (_Facade == null)
            {
                _Facade = new FacadeFactory(base.DataProvider).CreateSPFacade();
            }
            return this._Facade.QueryUnselectedObjectCount(_SelectQueryInfo, this.txtCodeQuery.Text, this.GetSelectedCodes());
        }

        #endregion



	}
}
