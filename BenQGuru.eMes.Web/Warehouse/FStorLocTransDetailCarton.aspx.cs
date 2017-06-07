using System;
using System.Data;

using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using System.Collections;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FStorLocTransDetailCarton : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private WarehouseFacade _WarehouseFacade = null;

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

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.txtTransNoQuery.Text = Request.QueryString["TRANSNO"];
                this.txtTransNoQuery.Enabled = false;
                txtPageEdit.Text = Request.QueryString["Page"];

                InitHander();
                InitWebGrid();
                this.cmdQuery_Click(null, null);
                this.RequestData();
            }
        }

        #region 默认查询
        private void RequestData()
        {

            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }
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

        #endregion
        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("TransNo", "转储单号", null);
            this.gridHelper.AddColumn("FLocationCode", "源货位", null);
            this.gridHelper.AddColumn("FCartonNo", "源箱号", null);
            this.gridHelper.AddColumn("TLocationCode", "目标货位", null);
            this.gridHelper.AddColumn("TCartonNo", "目标箱号", null);
            this.gridHelper.AddColumn("Quantity_num", "数量", null);
            this.gridHelper.AddLinkColumn("SNDetail", "SN详情", null);

            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();

            row["TransNo"] = ((StorloctransDetailCarton)obj).Transno;
            row["FLocationCode"] = ((StorloctransDetailCarton)obj).FromlocationCode;
            row["FCartonNo"] = ((StorloctransDetailCarton)obj).Fromcartonno;
            row["TLocationCode"] = ((StorloctransDetailCarton)obj).LocationCode;
            row["TCartonNo"] = ((StorloctransDetailCarton)obj).Cartonno;
            row["Quantity_num"] = ((StorloctransDetailCarton)obj).Qty.ToString("G0");

            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }

            return this._WarehouseFacade.QueryStorloctransDetailC(
           FormatHelper.CleanString(this.txtTransNoQuery.Text),
           Request.QueryString["DQMCODE"],
           inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            return this._WarehouseFacade.QueryStorloctransDetailCCount(
                  FormatHelper.CleanString(this.txtTransNoQuery.Text), Request.QueryString["DQMCODE"]
                  );
        }

        protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {
            if (commandName == "SNDetail")
            {
                string transNo = row.Items.FindItemByKey("TransNo").Text.Trim();
                string FcartonNo = row.Items.FindItemByKey("FCartonNo").Text.Trim();
                string TcartonNo = row.Items.FindItemByKey("TCartonNo").Text;
                Response.Redirect(this.MakeRedirectUrl("FStorLocTransDetailSN.aspx", new string[] { "TRANSNO", "FCARTON", "TCARTON", "DQMCODE", "Page", "FPage" }, new string[] { transNo, FcartonNo, TcartonNo,Request.QueryString["DQMCODE"] 
             ,"FStorLocTransDetailCarton.aspx" , txtPageEdit.Text }));
            }
        }

        protected override void Grid_ClickCell(GridRecord row, string command)
        {
            base.Grid_ClickCell(row, command);
            if (command == "SNDetail")
            {
                string transNo = row.Items.FindItemByKey("TransNo").Text.Trim();
                string FcartonNo = row.Items.FindItemByKey("FCartonNo").Text.Trim();
                string TcartonNo = row.Items.FindItemByKey("TCartonNo").Text;
                Response.Redirect(this.MakeRedirectUrl("FStorLocTransDetailSN.aspx", new string[] { "TRANSNO", "FCARTON", "TCARTON", "DQMCODE", "Page", "FPage" }, new string[] { transNo, FcartonNo, TcartonNo,Request.QueryString["DQMCODE"] 
                 ,"FStorLocTransDetailCarton.aspx" , txtPageEdit.Text }));
            }
        }

        #endregion

        #region Button

        //返回
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPageEdit.Text))
            {
                Response.Redirect(this.MakeRedirectUrl("FStorLocTransDetailMP.aspx", new string[] { "TRANSNO" },
                                                       new string[] { this.txtTransNoQuery.Text }));
            }
            else
            {
                Response.Redirect(this.MakeRedirectUrl(txtPageEdit.Text));

            }
        }

        #endregion

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {

            WarehouseFacade _WarehouseFacade = new WarehouseFacade(base.DataProvider);


            InventoryFacade _InventoryFacade = new InventoryFacade(this.DataProvider);


            object objStorloctrans = _WarehouseFacade.GetStorloctrans(txtTransNoQuery.Text);
            Storloctrans storloctrans = objStorloctrans as Storloctrans;
            if (storloctrans == null)
            {
                WebInfoPublish.Publish(this, txtTransNoQuery.Text + "转储单不存在！", this.languageComponent1);
                return;
            }

            if (storloctrans.Status == "Close")
            {

                WebInfoPublish.Publish(this, "转储单已关闭不能删除！", this.languageComponent1);
                return;
            }
            ArrayList array = this.gridHelper.GetCheckedRows();

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;

            foreach (GridRecord row in array)
            {
                string transno = storloctrans.Transno;
                string fromCartonno = row.Items.FindItemByKey("FCartonNo").Text;
                string cartonno = row.Items.FindItemByKey("TCartonNo").Text;
                StorloctransDetailCarton storCartonno = (StorloctransDetailCarton)_WarehouseFacade.GetStorloctransdetailcarton(transno, fromCartonno, cartonno);

                if (storCartonno != null)
                {
                    _WarehouseFacade.DeleteStorloctransdetailcarton(storCartonno);

                    StorageDetail stor = (StorageDetail)_WarehouseFacade.GetStorageDetail(fromCartonno);
                    if (stor != null)
                    {

                        stor.FreezeQty = stor.FreezeQty - (int)storCartonno.Qty;
                        stor.AvailableQty = stor.AvailableQty + (int)storCartonno.Qty;
                        _WarehouseFacade.UpdateStorageDetail(stor);
                    }

                    object[] objs = _WarehouseFacade.GetStorageDetailSnbyCartonNoBlock(fromCartonno);
                    if (objs != null && objs.Length > 0)
                    {
                        foreach (StorageDetailSN storageDetailSN in objs)
                        {

                            storageDetailSN.PickBlock = "N";
                            storageDetailSN.MaintainUser = GetUserCode();
                            storageDetailSN.MaintainDate = mDate;
                            storageDetailSN.MaintainTime = mTime;
                            _InventoryFacade.UpdateStorageDetailSN(storageDetailSN);
                        }
                    }

                    StorloctransDetailSN[] storSns = _WarehouseFacade.GetStorloctransDetailSNs(transno, fromCartonno);
                    foreach (StorloctransDetailSN sn in storSns)
                    {
                        _WarehouseFacade.DeleteStorloctransdetailsn(sn);
                    }

                    if (storCartonno.Qty != 0)
                    {
                        StorloctransDetail cartonnoDetail = (StorloctransDetail)_WarehouseFacade.GetStorloctransdetail(transno, storCartonno.MCode);
                        if (cartonnoDetail != null)
                        {
                            cartonnoDetail.Status = "Pick";
                            cartonnoDetail.MaintainUser = GetUserCode();
                            cartonnoDetail.MaintainDate = mDate;
                            cartonnoDetail.MaintainTime = mTime;
                            _WarehouseFacade.UpdateStorloctransdetail(cartonnoDetail);
                        }

                    }
                }



            }

        }


        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
                                ((StorloctransDetailCarton)obj).Transno,
                                ((StorloctransDetailCarton)obj).FromlocationCode,
                                 ((StorloctransDetailCarton)obj).Fromcartonno,
                                ((StorloctransDetailCarton)obj).LocationCode,
                                ((StorloctransDetailCarton)obj).Cartonno,
                                ((StorloctransDetailCarton)obj).Qty.ToString("G0")
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[]
                {
                                    "TransNo",
                                    "FLocationCode",
                                    "FCartonNo",
                                    "TLocationCode",
                                    "TCartonNo" ,
                                    "Quantity_num"
                                   
                };
        }

        #endregion
    }
}
