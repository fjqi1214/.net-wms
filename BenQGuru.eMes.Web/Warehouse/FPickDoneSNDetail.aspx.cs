using System;
using System.Data;
using System.Configuration;
using System.Collections;
using BenQGuru.eMES.Common;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FPickDoneSNDetail : BaseMPageNew
    {

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        private static string PickNo = string.Empty;
        private static string PickLine = string.Empty;

        private WarehouseFacade facade = null;
        private InventoryFacade _Invenfacade = null;
        private InventoryFacade _InventoryFacade = null;
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



        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHander();
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                PickNo = this.GetRequestParam("PickNo");
                PickLine = this.GetRequestParam("PickLine");
                this.txtPickNoQuery.Text = PickNo;
                this.txtPickNoQuery.Enabled = false;
                this.InitPageLanguage(this.languageComponent1, false);
            }
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }

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
            this.gridHelper.AddColumn("DQMaterialNO", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("CusMCode", "客户物料编码", null);
            this.gridHelper.AddColumn("BoxNo", "箱号", null);
            this.gridHelper.AddColumn("SN", "SN", null);
            this.gridHelper.AddColumn("MUser", "拣料人", null);
            this.gridHelper.AddColumn("MDate", "拣料日期", null);
            this.gridHelper.AddColumn("MTime", "拣料时间", null);
            this.gridHelper.AddColumn("PickNo", "拣料任务令", null);
            this.gridHelper.AddColumn("PickLine", "项目号", null);

            this.gridWebGrid.Columns.FromKey("PickLine").Hidden = true;
            this.gridWebGrid.Columns.FromKey("PickNo").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            this.gridHelper.RequestData();
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["DQMaterialNO"] = ((PickDetailMaterialSNEx)obj).DQMCODE;
            row["CusMCode"] = ((PickDetailMaterialSNEx)obj).CusMCode;
            row["BoxNo"] = ((PickDetailMaterialSNEx)obj).Cartonno;
            row["SN"] = ((PickDetailMaterialSNEx)obj).Sn;
            row["MUser"] = ((PickDetailMaterialSNEx)obj).MaintainUser;
            row["MDate"] = FormatHelper.ToDateString(((PickDetailMaterialSNEx)obj).MaintainDate);
            row["MTime"] = FormatHelper.ToTimeString(((PickDetailMaterialSNEx)obj).MaintainTime);
            row["PickNo"] = ((PickDetailMaterialSNEx)obj).Pickno;
            row["PickLine"] = ((PickDetailMaterialSNEx)obj).Pickline;

            return row;
        }
        #region
        protected override object GetEditObject(GridRecord row)
        {

            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(this.DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            object obj = _WarehouseFacade.GetPickdetailmaterialsn(row.Items.FindItemByKey("PickNo").Text, row.Items.FindItemByKey("SN").Text);
            if (obj != null)
            {
                return obj;
            }

            return null;
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(this.DataProvider);
            }
            Pickdetailmaterialsn[] pickdetailmaterialList = ((Pickdetailmaterialsn[])domainObjects.ToArray(typeof(Pickdetailmaterialsn)));
            try
            {
                this.DataProvider.BeginTransaction();
                foreach (Pickdetailmaterialsn pickdetailm in pickdetailmaterialList)
                {
                    #region delete
                    //请先删除箱单信息
                    string sn = pickdetailm.Sn;
                    object[] list = _WarehouseFacade.GetCartoninvdetailsnBySn(sn);
                    if (list != null)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "请先删除箱单信息", this.languageComponent1);
                        return;
                    }
                    #region delete 检查
                    //1、只有行明细是拣料中和拣料完成状态，且拣货任务令头状态是拣料状态或制作箱单状态时才可以删除。 
                    PickDetail pickDetail = _InventoryFacade.GetPickDetail(pickdetailm.Pickno, pickdetailm.Pickline) as PickDetail;
                    Pick pickHead = _InventoryFacade.GetPick(pickdetailm.Pickno) as Pick;
                    if (pickDetail == null || pickHead == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        return;
                    }
                    if (!(pickDetail.Status == PickDetail_STATUS.Status_Pick ||
                        pickDetail.Status == PickDetail_STATUS.Status_WaitPick ||
                        pickDetail.Status == PickDetail_STATUS.Status_ClosePick || pickDetail.Status == PickDetail_STATUS.Status_Cancel))
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "行明细是拣料中和拣料完成,取消状态，才可以删除", this.languageComponent1);
                        return;
                    }
                    if (!(pickHead.Status == PickHeadStatus.PickHeadStatus_Pick ||
                             pickDetail.Status == PickDetail_STATUS.Status_WaitPick
                        || pickHead.Status == PickHeadStatus.PickHeadStatus_MakePackingList || pickHead.Status == PickHeadStatus.PickHeadStatus_Cancel))
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "拣货任务令头状态是拣料状态或制作箱单,取消状态时，才可以删除", this.languageComponent1);
                        return;
                    }
                    //delete pickdetailmsn
                    _WarehouseFacade.DeletePickdetailmaterialsn(pickdetailm);
                    // tblPickdetailmaterial
                    object[] snlist = _WarehouseFacade.QueryPickdetailmaterialsnByCartonno(pickdetailm.Pickno, pickdetailm.Cartonno);
                    Pickdetailmaterial pickdetailmaterial = _WarehouseFacade.GetPickdetailmaterial(pickdetailm.Pickno, pickdetailm.Cartonno)
                             as Pickdetailmaterial;
                    if (pickdetailmaterial != null)
                    {
                        pickdetailmaterial.Qty -= 1;
                        _WarehouseFacade.UpdatePickdetailmaterial(pickdetailmaterial);
                        if (snlist == null)
                        {
                            _WarehouseFacade.DeletePickdetailmaterial(pickdetailmaterial);
                        }
                    }

                    //3、所有已拣行明细删除后，拣货任务令状态变更为待拣料。
                    #endregion

                    StorageDetail storageDetail = _WarehouseFacade.GetStorageDetail(pickdetailm.Cartonno) as StorageDetail;
                    if (storageDetail != null)
                    {
                        storageDetail.FreezeQty -= 1;
                        storageDetail.AvailableQty += 1;
                        _WarehouseFacade.UpdateStorageDetail(storageDetail);
                    }
                    StorageDetailSN storageDetailsn =
                       _InventoryFacade.GetStorageDetailSN(pickdetailm.Sn) as StorageDetailSN;
                    if (storageDetailsn != null)
                    {
                        storageDetailsn.PickBlock = "N";
                        _InventoryFacade.UpdateStorageDetailSN(storageDetailsn);
                    }


                    int count = _WarehouseFacade.GetPickdetailmaterialCount(pickdetailm.Pickno);
                    pickDetail.SQTY -= 1;
                    if (pickDetail.Status != "Cancel")
                        pickDetail.Status = PickDetail_STATUS.Status_Pick;
                    if (count == 0)
                    {
                        //pickHead.Status = PickHeadStatus.PickHeadStatus_WaitPick;
                        _WarehouseFacade.UpdatePick(pickHead);
                        //pickDetail.Status = PickDetail_STATUS.Status_WaitPick;
                    }
                    _WarehouseFacade.UpdatePickdetail(pickDetail);
                    #endregion
                }
                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "删除成功", this.languageComponent1);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }



        }
        #endregion

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            return this.facade.QueryPickDetailMaterialSN(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(PickNo)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(PickLine)),
                   FormatHelper.CleanString(txtCartonNoQurey.Text),
                        FormatHelper.CleanString(txtSNQuery.Text),

                inclusive,
                exclusive

               );
        }
        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            return this.facade.QueryPickDetailMaterialSNCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(PickNo)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(PickLine)),
                   FormatHelper.CleanString(txtCartonNoQurey.Text),
                   FormatHelper.CleanString(txtSNQuery.Text)
            );
        }

        #endregion

        #region Button

        protected void cmdReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FPickDoneMP.aspx",
                                   new string[] { "PickNo" },
                                   new string[] { PickNo }));
        }

        #endregion

        #region Object <--> Page



        #endregion

        #region For Export To Excel

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{

                    ((PickDetailMaterialSNEx)obj).DQMCODE,
                    ((PickDetailMaterialSNEx)obj).CusMCode,
                    ((PickDetailMaterialSNEx)obj).Cartonno,
                    ((PickDetailMaterialSNEx)obj).Sn,
                    ((PickDetailMaterialSNEx)obj).MaintainUser,
                    FormatHelper.ToDateString(((PickDetailMaterialSNEx)obj).MaintainDate),
                    FormatHelper.ToTimeString(((PickDetailMaterialSNEx)obj).MaintainTime)// ,
                    //((PickDetailMaterialSNEx)obj).Pickno,
                    //((PickDetailMaterialSNEx)obj).Pickline

                                    
            };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
                    "DQMaterialNO",
                    "CusMCode", 
                    "BoxNo", 
                    "SN", 
                    "MUser",
                    "MDate",
                    "MTime" //,
                    //"PickNo",
                    //"PickLine"
           
            };
        }

        #endregion


    }
}
