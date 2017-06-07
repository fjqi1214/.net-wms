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
using System.Collections.Generic;
using Infragistics.Web.UI.GridControls;
using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FSendCaseReceiptDetails : BaseMPageNew
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
            }
            InitHander();
            string pickNO = Request.QueryString["PICKNO"];
            string CARINVNO = Request.QueryString["CARINVNO"];
            txtPickNoQuery.Text = pickNO;
            txtCARINVNOQuery.Text = CARINVNO;
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
            this.gridHelper.AddColumn("CARTONNO", "包装箱号", null);
            this.gridHelper.AddColumn("GFHWITEMCODE", "华为物料编码", null);
            this.gridHelper.AddColumn("HWCODEQTY", "华为编码数量", null);
            this.gridHelper.AddColumn("CUSITEMDESC", "客户物料描述", null);

            this.gridHelper.AddColumn("DQMCODE", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("MDESC", "物料描述", null);
            this.gridHelper.AddColumn("CUSITEMSPEC", "华为型号", null);
            this.gridHelper.AddColumn("QTY", "数量", null);
            this.gridHelper.AddColumn("UNIT", "单位", null);
            this.gridHelper.AddColumn("B1", "B1", null);
            this.gridHelper.AddColumn("VENDERITEMCODE", "供应商物料编码", null);
            this.gridHelper.AddColumn("PACKINGWAYNO", "包装箱方式取数", null);



            this.gridHelper.AddLinkColumn("LinkToCartonImport", "详细信息", null);
            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            BenQGuru.eMES.Material.SendCaseDetails s = (BenQGuru.eMES.Material.SendCaseDetails)obj;
            row["CARTONNO"] = s.CARTONNO;
            row["GFHWITEMCODE"] = s.GFHWITEMCODE;
            row["HWCODEQTY"] = s.HWCODEQTY;
            row["CUSITEMDESC"] = s.CUSITEMDESC;
            row["DQMCODE"] = s.DQMCODE;
            row["MDESC"] = s.MDESC;
            row["CUSITEMSPEC"] = s.CUSITEMDESC;
            row["QTY"] = s.QTY;
            row["UNIT"] = s.UNIT;
            row["B1"] = "";
            row["VENDERITEMCODE"] = s.VENDERITEMCODE;
            row["PACKINGWAYNO"] = s.PACKINGWAYNO;

            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            return this._WarehouseFacade.QueryPackageReceiptsDetails(txtCARINVNOQuery.Text, txtPickNoQuery.Text, txtCARTONNOQuery.Text, txtDQMCODEQuery.Text, txtGFHWITEMCODEQuery.Text, inclusive, exclusive);

        }

        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            return this._WarehouseFacade.QueryPackageReceiptsDetailsCount(txtCARINVNOQuery.Text, txtPickNoQuery.Text, txtCARTONNOQuery.Text, txtDQMCODEQuery.Text, txtGFHWITEMCODEQuery.Text);
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
        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {

            string k = row.Items.FindItemByKey("PICKNO").Text.Trim();
            if (commandName == "LinkToCartonImport")
            {
                Response.Redirect(this.MakeRedirectUrl("FSendCaseReceiptDetails.aspx",
                                    new string[] { "PICKNO", "CARINVNO" },
                                    new string[] { txtPickNoQuery.Text, txtCARINVNOQuery.Text }));
            }
        }

        protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {
          
            if (commandName == "LinkToCartonImport")
            {
                Response.Redirect(this.MakeRedirectUrl("FSendCaseReceiptDetailsSN.aspx",
                                    new string[] { "PICKNO", "CARINVNO" },
                                    new string[] { txtPickNoQuery.Text, txtCARINVNOQuery.Text }));
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


        #region 返回
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FSendCaseReceipt.aspx"));
        }
        #endregion
        protected void drpStorageInTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
