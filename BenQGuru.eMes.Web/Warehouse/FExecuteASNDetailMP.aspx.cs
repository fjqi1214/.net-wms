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

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FExecuteASNDetailMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private WarehouseFacade _WarehouseFacade = null;
        SystemSettingFacade _SystemSettingFacade = null;
        private UserFacade _UserFacade = null;
        bool isVendor = false;//判断当前用户是否为供应商

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
            if (_UserFacade == null)
            {
                _UserFacade = new UserFacade(this.DataProvider);
            }
            isVendor = _UserFacade.IsVendor(this.GetUserCode());
            InitHander();
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);


                txtStorageInASNQuery.Text = GetAsnNo();
                this.InitWebGrid();
                this.cmdQuery_Click(null, null);
                this.RequestData();
                if (string.IsNullOrEmpty(Request.QueryString["Page"]) && string.IsNullOrEmpty(Request.QueryString["Parent"]))
                    cmdReturn.Visible = false;
            }
        }


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


        private string GetAsnNo()
        {
            return Request.QueryString["ASN"];
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
            this.gridHelper.AddColumn("BigCartonNO", "大箱号", null);
            this.gridHelper.AddColumn("SmallCartonNO", "小箱号", null);
            this.gridHelper.AddColumn("CartonNO", "箱号编码", null);
            this.gridHelper.AddColumn("DQLotNO", "鼎桥批次号", null);
            this.gridHelper.AddColumn("ASNStatus", "状态", null);
            this.gridHelper.AddColumn("DQMaterialNo", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("DQMaterialNoDesc", "鼎桥物料编码描述", null);
            this.gridHelper.AddColumn("VendorMCODE", "供应商物料编码", null);
            this.gridHelper.AddColumn("VendorMCODEDesc", "供应商物料编码描述", null);
            this.gridHelper.AddColumn("ASNQTY", "来料数量", null);
            this.gridHelper.AddColumn("ReceiveQTY", "已接收数量", null);
            this.gridHelper.AddColumn("ImportQTY", "已入库数量", null);
            this.gridHelper.AddColumn("MUOM", "单位", null);
            this.gridHelper.AddColumn("ProDate", "生产日期", null);
            this.gridHelper.AddColumn("VendorLotNo", "供应商批次", null);
            this.gridHelper.AddColumn("MControlType", "物料管控类型", null);
            this.gridHelper.AddColumn("FreeCheckMcode", "免检物料", null);

            this.gridHelper.AddDataColumn("ASNCreateTime", "入库指令创建时间", 20);
            this.gridHelper.AddDataColumn("ReformCount", "现场整改数量", 20);
            this.gridHelper.AddDataColumn("ReturnCount", "退换货数量", 20);
            this.gridHelper.AddDataColumn("RejectCount", "初检拒收数量", 20);


            this.gridHelper.AddColumn("CartonMemo", "箱单备注", null);
            this.gridHelper.AddColumn("stline", "line", null);
            this.gridHelper.AddColumn("stno", "stno", null);
            this.gridHelper.AddLinkColumn("SN", "SN", null);
            this.gridWebGrid.Columns.FromKey("stline").Hidden = true;
            this.gridWebGrid.Columns.FromKey("stno").Hidden = true;

            this.gridHelper.AddDefaultColumn(false, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            WarehouseFacade _facade = new WarehouseFacade(base.DataProvider);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            DataRow row = this.DtSource.NewRow();
            Asn asn = (Asn)_facade.GetAsn(((AsndetailEX)obj).Stno);
            row["BigCartonNO"] = ((AsndetailEX)obj).Cartonbigseq;
            row["SmallCartonNO"] = ((AsndetailEX)obj).Cartonseq;
            row["CartonNO"] = ((AsndetailEX)obj).Cartonno;
            row["DQLotNO"] = ((AsndetailEX)obj).Lotno;

            if (((AsndetailEX)obj).InitreceiveStatus == "Reject")
                row["ASNStatus"] = this.GetStatusName(((AsndetailEX)obj).InitreceiveStatus);
            else
                row["ASNStatus"] = this.GetStatusName(((AsndetailEX)obj).Status);
            row["DQMaterialNo"] = ((AsndetailEX)obj).DqmCode;
            row["DQMaterialNoDesc"] = ((AsndetailEX)obj).MDesc;
            row["VendorMCODE"] = asn.StType == "UB" ? ((AsndetailEX)obj).CustmCode : ((AsndetailEX)obj).VEndormCode;
            row["VendorMCODEDesc"] = ((Asndetail)obj).VEndormCodeDesc;
            row["ASNQTY"] = ((AsndetailEX)obj).Qty.ToString();
            row["ReceiveQTY"] = ((AsndetailEX)obj).ReceiveQty.ToString();
            row["ImportQTY"] = ((AsndetailEX)obj).ActQty.ToString();
            row["MUOM"] = ((AsndetailEX)obj).Unit;
            row["ProDate"] = FormatHelper.ToDateString(((AsndetailEX)obj).Production_Date);
            row["VendorLotNo"] = ((AsndetailEX)obj).Supplier_lotno;
            row["MControlType"] = this.languageComponent1.GetString(((AsndetailEX)obj).MControlType);



        

          
            if (asn != null)
            {
                string createTime = asn.CDate.ToString() + " " + FormatHelper.ToTimeString(asn.CTime);
                row["ASNCreateTime"] = createTime;

            }

            BenQGuru.eMES.IQC.IQCFacade iqcFacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            row["ReformCount"] = iqcFacade.ReformQtyTotalWithStNoLine(((AsndetailEX)obj).Stno, ((AsndetailEX)obj).Stline);
            row["ReturnCount"] = iqcFacade.ReturnQtyTotalWithStNoLine(((AsndetailEX)obj).Stno, ((AsndetailEX)obj).Stline);
            string status = ((AsndetailEX)obj).Status;

            if (status == ASNHeadStatus.Release || status == ASNHeadStatus.WaitReceive || status == ASNHeadStatus.Receive)
            {
                row["RejectCount"] = 0;

            }
            else
            {

                row["RejectCount"] = ((AsndetailEX)obj).Qty - ((AsndetailEX)obj).ReceiveQty;
            }



            row["CartonMemo"] = ((AsndetailEX)obj).Remark1;
            row["stline"] = ((AsndetailEX)obj).Stline.ToString();

            string flag = _WarehouseFacade.GetShipToStock(((AsndetailEX)obj).MCode, dbDateTime.DBDate);
            row["FreeCheckMcode"] = flag;
            return row;

        }

        protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {
            if (commandName == "SN")
            {
                string stno = txtStorageInASNQuery.Text;
                string stLine = row.Items.FindItemByKey("stline").Text.Trim();
                string page = Request.QueryString["Page"];
                string parentPage = string.Empty;
                if (!string.IsNullOrEmpty(page))
                {
                    parentPage = page;
                }

                Response.Redirect(this.MakeRedirectUrl("FExecuteASNDetailSN.aspx", new string[] { "stno", "stline", "Page", "Parent" }, new string[] { stno, stLine, "FExecuteASNDetailMP.aspx", parentPage }));
            }
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            return this._WarehouseFacade.QueryASNDetailBystno(
              FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageInASNQuery.Text)),
              inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            return this._WarehouseFacade.ASNDetailBystnoCount(
              FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageInASNQuery.Text))

              );
        }

        #endregion

        #region Button
        #region 返回
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {

            string parent = Request.QueryString["Parent"];
            string page = Request.QueryString["Page"];

            string directPage = string.Empty;
            if (!string.IsNullOrEmpty(page))
                directPage = page;
            else if (!string.IsNullOrEmpty(parent))
                directPage = parent;
            else
                throw new Exception("缺少重定向的界面信息！");
            Response.Redirect(this.MakeRedirectUrl(directPage,
                                     new string[] { "StNo" },
                                     new string[] { txtStorageInASNQuery.Text.Trim().ToUpper()
                                        
                                    }));


        }
        #endregion
        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
               ((AsndetailEX)obj).Cartonbigseq,
               ((AsndetailEX)obj).Cartonseq,
               ((AsndetailEX)obj).Cartonno,
               ((AsndetailEX)obj).Lotno,
               ((AsndetailEX)obj).InitreceiveStatus,
               ((AsndetailEX)obj).DqmCode,
                ((AsndetailEX)obj).MDesc,
                ((Asndetail)obj).VEndormCode,
                ((Asndetail)obj).VEndormCodeDesc,
                ((AsndetailEX)obj).Qty.ToString(),
                ((AsndetailEX)obj).ReceiveQty.ToString(),
                ((AsndetailEX)obj).ActQty.ToString(),
                ((AsndetailEX)obj).Unit,
                FormatHelper.ToDateString(((AsndetailEX)obj).Production_Date),
              ((AsndetailEX)obj).Supplier_lotno,
              ((AsndetailEX)obj).MControlType,
              ((AsndetailEX)obj).Remark1
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[]
                {
                        "BigCartonNO",                    
                        "SmallCartonNO",            
                        "CartonNO",                 
                        "DQLotNO",                  
                        "ASNStatus",                
                        "DQMaterialNo",             
                        "DQMaterialNoDesc",         
                        "VendorMCODE",              
                        "VendorMCODEDesc",          
                        "ASNQTY",                   
                        "ReceiveQTY",               
                        "ImportQTY",                
                        "MUOM",                     
                        "ProDate",                  
                        "VendorLotNo",              
                        "MControlType",             
                        "CartonMemo"              
                };
        }

        #endregion

    }
}
