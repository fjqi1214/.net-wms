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
using System.Xml.Linq;
using Infragistics.Web.UI.GridControls;
using System.Collections.Generic;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FStockPortionCheckDetails : BenQGuru.eMES.Web.Helper.BaseMPageNew
    {
        private BenQGuru.eMES.Material.WarehouseFacade _WarehouseFacade = null;
        private BenQGuru.eMES.Material.InventoryFacade _InventoryFacade = null;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        BenQGuru.eMES.BaseSetting.SystemSettingFacade _SystemSettingFacade = null;
        private Dictionary<string, string> dd = new Dictionary<string, string>();

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
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion



        #region Init
        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.GetRowCountHandle = new GetRowCountDelegateNew(this.GetRowCount);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.buttonHelper = new ButtonHelper(this);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);


            #region Exporter
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            this.excelExporter.Page = this;
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
            #endregion

        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }


        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                string CheckNo = Request.QueryString["CheckNo"];
                string StorageCode = Request.QueryString["StorageCode"];
                this.InitPageLanguage(this.languageComponent1, false);
                txtStorageCodeQuery.Text = StorageCode;
                txtStockCheckCodeQuery.Text = CheckNo;
                if (_WarehouseFacade == null)
                {
                    _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
                }

                if (_InventoryFacade == null)
                {
                    _InventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
                }


                object[] objs = _WarehouseFacade.GetLocations(CheckNo, StorageCode, txtDQMCODEQuery.Text);
                this.drpLocationCodeQuery.Items.Add(new ListItem("", ""));
                int orgId = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                if (objs != null && objs.Length > 0)
                {
                    foreach (Location l in objs)
                    {

                        Location lll = (Location)_InventoryFacade.GetLocation(l.LocationCode, orgId);
                        if (lll != null)
                        {
                            this.drpLocationCodeQuery.Items.Add(new ListItem(lll.LocationName, lll.LocationCode));
                        }
                        else
                        {
                            this.drpLocationCodeQuery.Items.Add(new ListItem(l.LocationCode, l.LocationCode));
                        }
                    }
                }

            }
            InitHander();

            //txtPickNoQuery.Text = pickNO;

            this.InitWebGrid();
            this.cmdQuery_Click(null, null);
            this.RequestData();

        }

        private void RequestData()
        {

            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }




        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("StorageCode", "库位", null);
            this.gridHelper.AddColumn("LocationCode", "货位", null);
            this.gridHelper.AddColumn("DQMCODE", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("CARTONNO", "箱号", null);

            this.gridHelper.AddColumn("STORAGEQTY", "库存数量", null);
            this.gridHelper.AddColumn("CheckQty", "盘点数量", null);
            this.gridHelper.AddColumn("Diff", "差异数量", null);




            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            BenQGuru.eMES.Material.StockCheckDetailDo s = (BenQGuru.eMES.Material.StockCheckDetailDo)obj;
            row["StorageCode"] = s.StorageCode;
            row["LocationCode"] = s.LocationCode;
            row["DQMCODE"] = s.DQMCODE;
            row["CARTONNO"] = s.CARTONNO;
            row["DQMCODE"] = s.DQMCODE;
            row["STORAGEQTY"] = s.STORAGEQTY;
            row["CheckQty"] = s.Qty;
            row["Diff"] = s.Qty - s.STORAGEQTY;


            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }



            return _WarehouseFacade.GetStockPortionCheckDetails(txtStockCheckCodeQuery.Text, drpLocationCodeQuery.SelectedValue, txtStorageCodeQuery.Text, txtDQMCODEQuery.Text, inclusive, exclusive);

        }

        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            return _WarehouseFacade.GetStockPortionCheckDetailsCount(txtStockCheckCodeQuery.Text, drpLocationCodeQuery.SelectedValue, txtStorageCodeQuery.Text, txtDQMCODEQuery.Text);
            // return this._WarehouseFacade.QueryPackageReceiptsCount(
            //    FormatHelper.CleanString(this.txtCARINVNOQuery.Text),
            //FormatHelper.CleanString(this.txtPickNoQuery.Text),
            //FormatHelper.CleanString(this.drpPickTypeQuery.SelectedValue),
            //FormatHelper.CleanString(this.txtItemNameQuery.Text),
            //    FormatHelper.CleanString(this.txtOrderNoQuery.Text),
            //FormatHelper.TODateInt(this.txtCBDateQuery.Text),
            //FormatHelper.TODateInt(this.txtCEDateQuery.Text));

        }

        #endregion

        #region Button

        protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {

            if (commandName == "LinkToCartonImport")
            {
                Response.Redirect(this.MakeRedirectUrl("FSendCaseReceiptDetailsSN.aspx",
                                    new string[] { "PICKNO", "CARINVNO" }
                                   , new string[] { "" }));
            }
        }

        protected override void cmdSave_Click(object sender, EventArgs e)
        {



        }
        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            PickInfo p = (PickInfo)obj;



            return new string[]{
                                 p.CARINVNO,
                                p.PICKNO,
                           this.dd.ContainsKey(p.STATUS.ToUpper()) ? this.dd[p.STATUS.ToUpper()] : "",
                                 p.INVNO,

                                 p.ORDERNO,
                                p.StorageCode,
                                 p.ReceiverUser,
                                  p.ReceiverAddr,
                               FormatHelper.ToDateString(p.Plan_Date),
                        
                               p.PLANGIDATE,
                               p.GFCONTRACTNO,
                               p.GFFLAG,
                                p.OANO,
                                FormatHelper.ToDateString(p.Packing_List_Date),
                               FormatHelper.ToTimeString(p.Packing_List_Time),
                                FormatHelper.ToDateString(p.Shipping_Mark_Date),
                                FormatHelper.ToTimeString(p.Shipping_Mark_Time),
                               p.GROSS_WEIGHT.ToString(),
                              p.VOLUME
                             
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"CARINVNO",
                                    "PICKNO",
                                    "STATUS",
                                    "INVNO",

	                                "ORDERNO",
                                    "StorageCode",
                                    "ReceiverUser",	
                                    "ReceiverAddr",
                                    "PlanDate",
                                    "PLANGIDATE",
                                    "GFCONTRACTNO",
                                    "GFFLAG",
                                    "OANO",
                                    "PackingListDate",
                                    "PackingListTime",
                                    "ShippingMarkDate",	
                                    "ShippingMarkTime",
                                    "GROSSWEIGHT",
                                    "VOLUME",
                                  };


        }

        #endregion



        protected void drpStorageInTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {


            Response.Redirect(this.MakeRedirectUrl("FStockCheck.aspx",
                                new string[] { "CheckCode", "StorageCode" },
                                new string[] { txtStockCheckCodeQuery.Text.Trim().ToUpper(),txtStorageCodeQuery.Text.Trim()
                                        
                                    }));


        }
    }
}
