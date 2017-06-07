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
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using System.Collections.Generic;
using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FSpecStorageInfo : BaseMPageNew
    {
        private BenQGuru.eMES.Material.WarehouseFacade _WarehouseFacade = null;
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

                this.InitPageLanguage(this.languageComponent1, false);
                InitHander();
                string stno = Request.QueryString["stno"];
                string stline = Request.QueryString["stline"];
                //txtSTNOQuery.Text = stno;
                //txtSTLINEQuery.Text = stline;

                this.InitWebGrid();
                this.cmdQuery_Click(null, null);
                this.RequestData();
            }


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

            this.gridHelper.AddColumn("MCODE", "物料号", null);
            this.gridHelper.AddColumn("DQMCODE", "鼎桥物料号", null);
            this.gridHelper.AddColumn("STORAGECODE", "库位", null);
            this.gridHelper.AddColumn("LOCATIONCODE", "货位代码", null);
            this.gridHelper.AddColumn("STORAGEQTY", "库存数量", null);
            this.gridHelper.AddColumn("CUSER", "创建人", null);
            this.gridHelper.AddColumn("CDATE", "创建日期", null);
            this.gridHelper.AddColumn("CTIME", "创建时间", null);
            this.gridHelper.AddColumn("MUSER", "维护人", null);
            this.gridHelper.AddColumn("MDATE", "维护日期", null);
            this.gridHelper.AddColumn("MTIME", "维护时间", null);
            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            SPECSTORAGEINFO s = (SPECSTORAGEINFO)obj;
            row["MCODE"] = s.MCODE;
            row["DQMCODE"] = s.DQMCODE;
            row["STORAGECODE"] = s.STORAGECODE;
            row["LOCATIONCODE"] = s.LOCATIONCODE;
            row["STORAGEQTY"] = s.STORAGEQTY;
            row["CUSER"] = s.CUSER;
            row["CDATE"] = s.CDATE;
            row["CTIME"] = s.CTIME;
            row["MUSER"] = s.MUSER;
            row["MDATE"] = s.MDATE;
            row["MTIME"] = s.MTIME;
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            return this._WarehouseFacade.QuerySPECSTORAGEINFO(txtDQMCODEQuery.Text, txtStorageCodeQuery.Text, inclusive, exclusive);
        }


        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FExecuteASNDetailMP.aspx"));
        }
        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            return this._WarehouseFacade.QuerySPECSTORAGEINFOCOUNT(txtDQMCODEQuery.Text, txtStorageCodeQuery.Text);
        }

        #endregion

        #region Button
       

        protected override void cmdSave_Click(object sender, EventArgs e)
        {
        }
        #endregion

        #region Export

        //protected override string[] FormatExportRecord(object obj)
        //{

        //    BenQGuru.eMES.Domain.Warehouse.PickInfo p = (BenQGuru.eMES.Domain.Warehouse.PickInfo)obj;






        //    return new string[]{
        //                         p.CARINVNO,
        //                        p.PICKNO,
        //                   this.dd.ContainsKey(p.STATUS) ? this.dd[p.STATUS] : "",
        //                         p.INVNO,

        //                         p.ORDERNO,
        //                        p.StorageCode,
        //                         p.ReceiverUser,
        //                          p.ReceiverAddr,
        //                       FormatHelper.ToDateString(p.Plan_Date),
                        
        //                       p.PLANGIDATE,
        //                       p.GFCONTRACTNO,
        //                       p.GFFLAG,
        //                        p.OANO,
        //                        FormatHelper.ToDateString(p.Packing_List_Date),
        //                       FormatHelper.ToTimeString(p.Packing_List_Time),
        //                        FormatHelper.ToDateString(p.Shipping_Mark_Date),
        //                        FormatHelper.ToTimeString(p.Shipping_Mark_Time),
        //                       p.GROSS_WEIGHT.ToString(),
        //                      p.VOLUME
                             
        //                       };
        //}

        //protected override string[] GetColumnHeaderText()
        //{
        //    return new string[] {	"CARINVNO",
        //                            "PICKNO",
        //                            "STATUS",
        //                            "INVNO",

        //                            "ORDERNO",
        //                            "StorageCode",
        //                            "ReceiverUser",	
        //                            "ReceiverAddr",
        //                            "PlanDate",
        //                            "PLANGIDATE",
        //                            "GFCONTRACTNO",
        //                            "GFFLAG",
        //                            "OANO",
        //                            "PackingListDate",
        //                            "PackingListTime",
        //                            "ShippingMarkDate",	
        //                            "ShippingMarkTime",
        //                            "GROSSWEIGHT",
        //                            "VOLUME",
        //                          };


        //}

        #endregion



        protected void drpStorageInTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}



