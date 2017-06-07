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
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Common;

using BenQGuru.eMES.Web.UserControl;


namespace BenQGuru.eMES.Web.SelectQuery
{
    /// <summary>
    /// Selector 的摘要说明。
    /// </summary>
    public partial class FASNSP : BaseSelectorPageNew
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
            base.InitWebGrid();
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedASNCode", "ASN号", null);
            this.gridUnSelectedHelper.AddColumn("VendorCode", "供应商代码", null);
            this.gridUnSelectedHelper.AddColumn("StorageInCode", "入库库位", null);
            this.gridUnSelectedHelper.AddDefaultColumn(true, false);
            this.gridUnSelectedHelper.ApplyLanguage(this.languageComponent1);
            this.gridUnSelected.Columns["Selector_UnselectedCode"].Hidden = true;
            this.gridUnSelected.Columns["Selector_UnSelectedDesc"].Hidden = true;

            this.gridSelectedHelper.AddColumn("Selector_selectedASNCode", "ASN号", null);
            this.gridSelectedHelper.AddColumn("VendorCode", "供应商代码", null);
            this.gridSelectedHelper.AddColumn("StorageInCode", "入库库位", null);
            this.gridSelectedHelper.AddDefaultColumn(true, false);
            this.gridSelectedHelper.ApplyLanguage(this.languageComponent1);
            this.gridSelected.Columns["Selector_SelectedCode"].Hidden = true;
            this.gridSelected.Columns["Selector_SelectedDesc"].Hidden = true;

        }

        protected override DataRow GetSelectedGridRow(object obj)
        {
            DataRow row = DtSourceSelected.NewRow();
            row["Selector_SelectedCode"] = ((BenQGuru.eMES.Domain.Warehouse.ASN)obj).StNo;
            row["Selector_selectedASNCode"] = ((BenQGuru.eMES.Domain.Warehouse.ASN)obj).StNo;
            row["VendorCode"] = ((BenQGuru.eMES.Domain.Warehouse.ASN)obj).VendorCode;
            row["StorageInCode"] = ((BenQGuru.eMES.Domain.Warehouse.ASN)obj).StorageCode;

            //row["Selector_SelectedCode"] = ((SStack)obj).StackCode;
            //row["Selector_SelectedDesc"] = ((SStack)obj).StackDesc;
            return row;
        }

        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((BenQGuru.eMES.Domain.Warehouse.ASN)obj).StNo;
            row["Selector_UnselectedASNCode"] = ((BenQGuru.eMES.Domain.Warehouse.ASN)obj).StNo;
            row["VendorCode"] = ((BenQGuru.eMES.Domain.Warehouse.ASN)obj).VendorCode;
            row["StorageInCode"] = ((BenQGuru.eMES.Domain.Warehouse.ASN)obj).StorageCode;

            //row["Selector_UnselectedCode"] = ((SStack)obj).StackCode;
            //row["Selector_UnSelectedDesc"] = ((SStack)obj).StackDesc;
            return row;
        }

        protected override object[] LoadSelectedDataSource(int inclusive, int exclusive)
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QuerySelectedASN(this.GetSelectedCodes());
        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            return this.facade.QueryUnSelectedASN(this.txtASNCodeQuery.Text,
            this.txtVendorCodeQuery.Text, this.txtStorageCodeQuery.Text, this.GetSelectedCodes(), inclusive, exclusive);

            //if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            //return this.facade.QueryUnSelectedStack(this.txtStackCodeQuery.Text, this.GetSelectedCodes(), inclusive, exclusive);
        }


        protected override int GetUnSelectedRowCount()
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateSPFacade(); }
            return this.facade.QueryUnSelectedASNCount(this.txtASNCodeQuery.Text, this.txtVendorCodeQuery.Text,
         this.txtStorageCodeQuery.Text, this.GetSelectedCodes());

            //return this.facade.QueryUnSelectedStackCount(this.txtStackCodeQuery.Text, this.GetSelectedCodes());
        }
        #endregion





    }
}
