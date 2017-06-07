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
using System.Collections.Generic;
using Infragistics.Web.UI.GridControls;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FStockCheckDetails : BenQGuru.eMES.Web.Helper.BaseMPageNew
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
            string CheckNo = Request.QueryString["CheckNo"];
            string StorageCode = Request.QueryString["StorageCode"];
            txtStorageCodeQuery.Text = StorageCode;
            txtStockCheckCodeQuery.Text = CheckNo;
            if (!this.IsPostBack)
            {
                // 初始化页面语言

                this.InitPageLanguage(this.languageComponent1, false);
                InitHander();
                this.InitWebGrid();

                this.cmdQuery_Click(null, null);
                this.RequestData();
            }



            cmdStorageCheckClose.Value = "结束盘点";



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

            this.gridHelper.AddColumn("DQMCODE", "鼎桥物料编码", null);


            this.gridHelper.AddColumn("STORAGEQTY", "库存数量", null);
            this.gridHelper.AddColumn("CheckQty", "盘点数量", null);
            this.gridHelper.AddColumn("Diff", "差异数量", null);
            this.gridHelper.AddColumn("DiffDesc", "差异原因", false);
            this.gridHelper.AddDataColumn("OldDiffDesc", "差异原因", true);
            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            BenQGuru.eMES.Domain.Warehouse.StockCheckDetail s = (BenQGuru.eMES.Domain.Warehouse.StockCheckDetail)obj;
            row["StorageCode"] = s.StorageCode;
            row["DQMCODE"] = s.DQMCODE;
            row["STORAGEQTY"] = s.STORAGEQTY;
            row["CheckQty"] = s.CheckQty;
            row["Diff"] = s.CheckQty - s.STORAGEQTY;
            row["DiffDesc"] = s.DIFFDESC;
            row["OldDiffDesc"] = s.DIFFDESC;
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            return _WarehouseFacade.GetStockCheckDetails(txtStockCheckCodeQuery.Text, txtStorageCodeQuery.Text, txtDQMCODEQuery.Text, inclusive, exclusive);

        }

        protected void cmdLotSave_ServerClick(object sender, EventArgs e)
        {
            try
            {

                WarehouseFacade _WarehouseFacade = new WarehouseFacade(base.DataProvider);
                BenQGuru.eMES.Domain.Warehouse.StockCheck ssss = (BenQGuru.eMES.Domain.Warehouse.StockCheck)_WarehouseFacade.GetStockCheck(txtStockCheckCodeQuery.Text);
                if (ssss == null)
                {
                    WebInfoPublish.Publish(this, "只有盘点单不存在！", this.languageComponent1); return;
                }
                this.DataProvider.BeginTransaction();

                for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                {

                    string storageCode = this.gridWebGrid.Rows[i].Items.FindItemByKey("StorageCode").Value.ToString();
                    string dqmcode = this.gridWebGrid.Rows[i].Items.FindItemByKey("DQMCODE").Value.ToString();
                    string newDiffDesc = this.gridWebGrid.Rows[i].Items.FindItemByKey("DiffDesc").Value.ToString();

                    string oldDiffDesc = this.gridWebGrid.Rows[i].Items.FindItemByKey("OldDiffDesc").Value.ToString();
                    if (newDiffDesc != oldDiffDesc)
                        _WarehouseFacade.UpdateStockCheckDetail(txtStockCheckCodeQuery.Text, dqmcode, storageCode, newDiffDesc);
                }

                this.DataProvider.CommitTransaction();


                WebInfoPublish.Publish(this, "保存完成！", this.languageComponent1);


            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }

        }
        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            return _WarehouseFacade.GetStockCheckDetailsCount(txtStockCheckCodeQuery.Text, txtStorageCodeQuery.Text, txtDQMCODEQuery.Text);


        }


        protected void cmdStorageCheckClose_ServerClick(object sender, EventArgs e)
        {
            if (!HasAuthority())
            {
                WebInfoPublish.Publish(this, "没有权限操作！", this.languageComponent1); return;
            }

            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            BenQGuru.eMES.Domain.Warehouse.StockCheck s = (BenQGuru.eMES.Domain.Warehouse.StockCheck)_WarehouseFacade.GetStockCheck(txtStockCheckCodeQuery.Text);
            s.STATUS = "Close";
            _WarehouseFacade.UpdateStockCheck(s);
            WebInfoPublish.Publish(this, "盘点关闭成功！", this.languageComponent1); return;


        }

        private bool HasAuthority()
        {
            _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            UserGroup2User[] usergroupList = _WarehouseFacade.GetUserFromUserGroup("DQWL");
            string curUserCode = GetUserCode();
            foreach (UserGroup2User userCode in usergroupList)
            {
                if (userCode.USERCODE == curUserCode)
                    return true;
            }
            return false;
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
            BenQGuru.eMES.Domain.Warehouse.StockCheckDetail s = (BenQGuru.eMES.Domain.Warehouse.StockCheckDetail)obj;



            return new string[]{
                                      s.StorageCode,
                s.DQMCODE,
               s.STORAGEQTY.ToString(),
                s.CheckQty.ToString(),
                 (s.CheckQty - s.STORAGEQTY).ToString()
                             
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	 
                "StorageCode",     
                      "DQMCODE",   
                      "STORAGEQTY",
                      "CheckQty",  
                      "Diff"    
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
