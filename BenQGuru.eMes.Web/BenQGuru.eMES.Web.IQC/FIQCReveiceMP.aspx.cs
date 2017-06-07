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

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.SAPDataTransfer;
using BenQGuru.eMES.SAPDataTransferInterface;
using BenQGuru.eMES.Web.Helper;

using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.Web.IQC
{
    public partial class FIQCReveiceMP : BasePage
    {

        private System.ComponentModel.IContainer components;
        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        private ExcelExporter excelExporter;

        private IQCFacade _IQCFacade;

        #region Facade
        private InventoryFacade m_InvFacade = null;
        public InventoryFacade InvFacade
        {
            get
            {
                if (m_InvFacade == null)
                {
                    m_InvFacade = new InventoryFacade(base.DataProvider);
                }
                return m_InvFacade;
            }
        }
        #endregion

        #region Init

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

            // languageComponent1
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

            // excelExporter
            this.excelExporter.FileExtension = "xls";
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            this.excelExporter.RowSplit = "\r\n";
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.InitOnPostBack();

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                // 初始化界面UI
                this.InitUI();
                this.InitButtonHelp();
                this.SetEditObject(null);
                this.InitWebGrid();

                // 初始化控件
                this.drpROHSQuery_Load();
                this.drpShipToStockQuery_Load();
                this.datAccountDateEdit.Text = DateTime.Now.ToString("yyyy-MM-dd"); ;
                this.datVoucherDateEdit.Text = DateTime.Now.ToString("yyyy-MM-dd"); ;
            }

            _IQCFacade = new IQCFacadeFactory(base.DataProvider).CreateIQCFacade();
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected void cmdIQCReceive_ServerClick(object sender, System.EventArgs e)
        {

            MaterialReceive materialReceive = (MaterialReceive)this.GetEditObject();

            if (materialReceive != null)
            {
                string returnMessage = string.Empty;
                if (IQCReceive(materialReceive, ref returnMessage))
                {
                    this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                    this.RequestData();
                    this.buttonHelper.PageActionStatusHandle(PageActionType.Save);

                    WebInfoPublish.PublishInfo(this, "$Message_IQCReceiveOK", languageComponent1);
                }
                else
                {
                    WebInfoPublish.Publish(this, "$Message_IQCReceiveFailed" + " : " + returnMessage, languageComponent1);
                }
            }
        }

        protected void cmdIQCReceiveBatch_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList items = new ArrayList();
                ArrayList itemsFailed = new ArrayList();

                foreach (UltraGridRow row in array)
                {
                    MaterialReceive materialReceive = (MaterialReceive)this.GetEditObject(row);

                    //检查入库库别(页面上输入的是tblstorage.sapstorage)
                    string sapStorage = materialReceive.StorageID.Trim().ToUpper();
                    object[] storageList = this.InvFacade.QueryStorageByOrgId(materialReceive.OrganizationID, sapStorage);
                    if (storageList == null)
                    {
                        //WebInfoPublish.Publish(this, "$INVALID_STORGE" + " " + "$PageControl_IQCNo" + ":" +  materialReceive.IQCNo , languageComponent1);
                        WebInfoPublish.Publish(this, string.Format("$INVALID_STORGE    $PageControl_IQCNo:{0} $STLINE:{1}", materialReceive.IQCNo, materialReceive.STLine), languageComponent1);
                        return;
                    }

                    string returnMessage = string.Empty;
                    if (IQCReceive(materialReceive, ref returnMessage))
                    {
                        items.Add(materialReceive);
                    }
                    else
                    {
                        itemsFailed.Add(materialReceive);
                        materialReceive.ReceiveMemo = languageComponent1.GetString(returnMessage);
                    }
                }


                this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Query);

                string message = string.Empty;

                if (items != null && items.Count > 0)
                {
                    message += "$Message_IQCReceiveOK" + " : \n";
                    foreach (MaterialReceive item in items)
                    {
                        message += item.IQCNo + "," + item.STLine + "\n";
                    }
                }

                if (itemsFailed != null && itemsFailed.Count > 0)
                {
                    if (message.Trim().Length > 0)
                    {
                        message += "\n";
                    }
                    message += "$Message_IQCReceiveFailed" + " : \n";
                    foreach (MaterialReceive item in itemsFailed)
                    {
                        message += item.IQCNo + "," + item.STLine + ": " + item.ReceiveMemo + "\n";
                    }
                }

                if (message.Trim().Length > 0)
                {
                    WebInfoPublish.PublishInfo(this, message, languageComponent1);
                }
            }
        }

        private bool IQCReceive(MaterialReceive materialReceive, ref string returnMessage)
        {
            bool returnValue = false;

            if (materialReceive != null)
            {
                bool checkPassed = true;
                if (this._IQCFacade.IsFromASN(materialReceive.STNo))
                {
                    ASN asn = (ASN)this._IQCFacade.GetASN(materialReceive.STNo);
                    //asn单为Release不能接受
                    if (asn == null || (asn.STStatus != IQCStatus.IQCStatus_WaitCheck && asn.STStatus != IQCStatus.IQCStatus_Close))
                    {
                        checkPassed = false;
                    }
                }

                if (checkPassed)
                {
                    materialReceive.Flag = FlagStatus.FlagStatus_MES;
                    materialReceive.MaintainUser = this.GetUserCode();

                    returnValue = this._IQCFacade.IQCReceiveMaterial((MaterialReceive)materialReceive);
                }  
                else
                {
                    returnMessage = "$Message_ASNIsNotWaitCheckOrCancel";
                }
            }

            if (returnValue)
            {
                returnMessage = string.Empty;
            }
            return returnValue;
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }

        private void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.chbSelectAll.Checked)
            {
                this.gridHelper.CheckAllRows(CheckStatus.Checked);
            }
            else
            {
                this.gridHelper.CheckAllRows(CheckStatus.Unchecked);
            }
        }

        private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
        {
            if (e.Cell.Column.Key == "Edit")
            {
                object obj = this.GetEditObject(e.Cell.Row);

                if (obj != null)
                {
                    this.SetEditObject(obj);

                    this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        private void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Update)
            {
                this.cmdIQCReceive.Disabled = false;
                this.cmdIQCReceiveBatch.Disabled = true;

                this.txtIQCNoEdit.ReadOnly = true;
                this.txtIQCLineEdit.ReadOnly = true;
                this.txtPurchaseOrderNoEdit.ReadOnly = true;
                this.txtPurchaseOrderLineEdit.ReadOnly = true;
            }
            if (pageAction == PageActionType.Save)
            {
                this.cmdIQCReceive.Disabled = true;
                this.cmdIQCReceiveBatch.Disabled = false;

                this.txtIQCNoEdit.ReadOnly = false;
                this.txtIQCLineEdit.ReadOnly = false;
                this.txtPurchaseOrderNoEdit.ReadOnly = false;
                this.txtPurchaseOrderLineEdit.ReadOnly = false;
            }
            if (pageAction == PageActionType.Cancel)
            {
                this.cmdIQCReceive.Disabled = true;
                this.cmdIQCReceiveBatch.Disabled = false;

                this.txtIQCNoEdit.ReadOnly = false;
                this.txtIQCLineEdit.ReadOnly = false;
                this.txtPurchaseOrderNoEdit.ReadOnly = false;
                this.txtPurchaseOrderLineEdit.ReadOnly = false;
            }
            if (pageAction == PageActionType.Add)
            {
                this.cmdIQCReceive.Disabled = true;
                this.cmdIQCReceiveBatch.Disabled = false;

                this.txtIQCNoEdit.ReadOnly = false;
                this.txtIQCLineEdit.ReadOnly = false;
                this.txtPurchaseOrderNoEdit.ReadOnly = false;
                this.txtPurchaseOrderLineEdit.ReadOnly = false;
            }
        }

        private bool ValidateInput()
        {
            string checkMessage = string.Empty;

            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblStorageIDEdit, txtStorageIDEdit, 4, true));
            manager.Add(new NumberCheck(lblRealReceiveQtyEdit, txtRealReceiveQtyEdit, 1, 9999999999999, true));
            manager.Add(new DateCheck(lblAccountDateEdit, datAccountDateEdit.Text, true));
            manager.Add(new DateCheck(lblVoucherDateEdit, datVoucherDateEdit.Text, true));

            if (!manager.Check())
            {
                checkMessage += manager.CheckMessage;
            }

            if (checkMessage.Trim().Length > 0)
            {
                WebInfoPublish.Publish(this, checkMessage, languageComponent1);
                return false;
            }

            //实际接收数量不能大于预计送货数量
            IQCDetail iqcDetail = (IQCDetail)_IQCFacade.GetIQCDetail(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtIQCNoEdit.Text)), int.Parse(txtIQCLineEdit.Text));
            if (iqcDetail != null && long.Parse(txtRealReceiveQtyEdit.Text) > GetReceiveQty((IQCDetail)iqcDetail))
            {
                WebInfoPublish.Publish(this, "$Error_RealReceiveQtyCannotBiggerThanReceiveQty", languageComponent1);
                return false;
            }

            //检查入库库别(页面上输入的是tblstorage.sapstorage)
            string sapStorage = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtStorageIDEdit.Text));
            object[] storageList = this.InvFacade.QueryStorageByOrgId(iqcDetail.OrganizationID, sapStorage);
            if (storageList==null)
            {
                WebInfoPublish.Publish(this, "$INVALID_STORGE", languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region For Page_Load

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this.gridHelper = new GridHelper(this.gridWebGrid);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        public void InitButtonHelp()
        {
            this.buttonHelper.AddDeleteConfirm();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

            //this.buttonHelper.AddButtonConfirm(cmdIQCReceiveBatch, languageComponent1.GetString("IQCReceiveBatchConfirm"));
        }

        private void drpROHSQuery_Load()
        {
            drpROHSQuery.Items.Add(new ListItem("", ""));
            drpROHSQuery.Items.Add(new ListItem("Y", "Y"));
            drpROHSQuery.Items.Add(new ListItem("N", "N"));
        }

        private void drpShipToStockQuery_Load()
        {
            drpShipToStockQuery.Items.Add(new ListItem("", ""));
            drpShipToStockQuery.Items.Add(new ListItem("Y", "Y"));
            drpShipToStockQuery.Items.Add(new ListItem("N", "N"));
        }

        #endregion

        #region For Query Data

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(int.MinValue, int.MaxValue);
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            return this._IQCFacade.QueryIQCDetailForReceive(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtIQCNoQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNPOQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVendorCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpROHSQuery.SelectedValue)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpShipToStockQuery.SelectedValue)),
                FormatHelper.TODateInt(this.datAppDateFromQuery.Text),
                FormatHelper.TODateInt(this.datAppDateToQuery.Text),
                inclusive,
                exclusive
                );
        }

        private int GetRowCount()
        {
            return this._IQCFacade.QueryIQCDetailForReceiveCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtIQCNoQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNPOQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVendorCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpROHSQuery.SelectedValue)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpShipToStockQuery.SelectedValue)),
                FormatHelper.TODateInt(this.datAppDateFromQuery.Text),
                FormatHelper.TODateInt(this.datAppDateToQuery.Text)
                );
        }

        #endregion

        #region For Grid And Edit

        private void InitWebGrid()
        {
            this.gridHelper.AddColumn("IQCNo", "送检单", null, 150);
            this.gridHelper.AddColumn("OrderNo", "订单号", null);
            this.gridHelper.AddColumn("OrderLine", "定单行", null);
            this.gridHelper.AddColumn("VendorCode", "供应商代码", null);
            this.gridHelper.AddColumn("VendorName", "供应商名称", null);
            this.gridHelper.AddColumn("ROHS", "ROHS", null, 50);
            this.gridHelper.AddColumn("ShipToStock", "免检", null, 50);
            this.gridHelper.AddColumn("InventoryUser", "保管员", null);
            this.gridHelper.AddColumn("IQCHeadAttribute", "送检单类型", null);
            this.gridHelper.AddColumn("Inspector", "检验员", null);
            this.gridHelper.AddColumn("InspectDate", "检验日期", null);
            this.gridHelper.AddColumn("ASNLine", "ASN行", null);

            this.gridHelper.AddColumn("MaterialCode", "物料代码", null);
            this.gridHelper.AddColumn("MaterialDesc", "物料描述", null);
            this.gridHelper.AddColumn("IQCDetailReceiveType", "接收方式", null);
            this.gridHelper.AddColumn("IQCDetailAttribute", "行类型", null);
            this.gridHelper.AddColumn("Unit", "单位", null);
            this.gridHelper.AddColumn("StorageID", "入库库别", null);
            this.gridHelper.AddColumn("IQCDetailReceiveQty", "收货数量", null);
            this.gridHelper.AddColumn("IQCDetailCheckStatus", "检验结果", null);
            this.gridHelper.AddColumn("MemoEx", "不合格描述", null);

            this.gridHelper.AddDefaultColumn(true, true);

            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{
                    "false",
                    ((IQCDetailForReceive)obj).IQCNo,
                    ((IQCDetailForReceive)obj).OrderNo,
                    ((IQCDetailForReceive)obj).OrderLine.ToString(),
                    ((IQCDetailForReceive)obj).VendorCode,
                    ((IQCDetailForReceive)obj).VendorName,
                    ((IQCDetailForReceive)obj).ROHS,
                    ((IQCDetailForReceive)obj).ShipToStock,
                    ((IQCDetailForReceive)obj).InventoryUser + ((IQCDetailForReceive)obj).InventoryUserName,
                    this.languageComponent1.GetString(((IQCDetailForReceive)obj).IQCHeadAttribute),
                    ((IQCDetailForReceive)obj).Inspector + ((IQCDetailForReceive)obj).InspectorUserName,
                    FormatHelper.ToDateString(((IQCDetailForReceive)obj).InspectDate),
                    ((IQCDetailForReceive)obj).STLine.ToString(),

                    ((IQCDetailForReceive)obj).ItemCode,
                    ((IQCDetailForReceive)obj).MaterialDescription,
                    this.languageComponent1.GetString(((IQCDetailForReceive)obj).Type),
                    this.languageComponent1.GetString(((IQCDetailForReceive)obj).Attribute),
                    ((IQCDetailForReceive)obj).Unit,
                    ((IQCDetailForReceive)obj).StorageID,
                    GetReceiveQty((IQCDetailForReceive)obj).ToString("0.00"),
                    ((IQCDetailForReceive)obj).CheckStatus,
                    ((IQCDetailForReceive)obj).MemoEx,
                    ""
                });
        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                MaterialReceive materialReceive = this._IQCFacade.CreateNewMaterialReceive();

                materialReceive.IQCNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtIQCNoEdit.Text));
                materialReceive.STLine = int.Parse(txtIQCLineEdit.Text);
                materialReceive.OrderNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtPurchaseOrderNoEdit.Text));
                materialReceive.OrderLine = int.Parse(txtPurchaseOrderLineEdit.Text);

                materialReceive.StorageID = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtStorageIDEdit.Text));
                materialReceive.RealReceiveQty = int.Parse(txtRealReceiveQtyEdit.Text);
                materialReceive.ReceiveMemo = txtReceiveMemoEdit.Text;

                materialReceive.AccountDate = FormatHelper.TODateInt(datAccountDateEdit.Text);
                materialReceive.VoucherDate = FormatHelper.TODateInt(datVoucherDateEdit.Text);

                IQCDetail iqcDetail = (IQCDetail)this._IQCFacade.GetIQCDetail(materialReceive.IQCNo, materialReceive.STLine);
                if (iqcDetail != null)
                {
                    materialReceive.STNo = iqcDetail.STNo;
                    materialReceive.OrganizationID = iqcDetail.OrganizationID;
                    materialReceive.ItemCode = iqcDetail.ItemCode;
                    materialReceive.Unit = iqcDetail.Unit;
                }

                return materialReceive;
            }
            else
            {
                return null;
            }
        }

        private object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {
            object obj = this._IQCFacade.GetMaterialReceive(row.Cells.FromKey("IQCNo").Text, int.Parse(row.Cells.FromKey("ASNLine").Text));

            if (obj == null)
            {
                obj = this._IQCFacade.GetIQCDetail(row.Cells.FromKey("IQCNo").Text, int.Parse(row.Cells.FromKey("ASNLine").Text));
                if (obj != null)
                {
                    MaterialReceive materialReceive = this._IQCFacade.CreateNewMaterialReceive();

                    materialReceive.IQCNo = ((IQCDetail)obj).IQCNo;
                    materialReceive.STLine = ((IQCDetail)obj).STLine;
                    materialReceive.OrderNo = ((IQCDetail)obj).OrderNo;
                    materialReceive.OrderLine = ((IQCDetail)obj).OrderLine;

                    materialReceive.StorageID = ((IQCDetail)obj).StorageID;
                    materialReceive.RealReceiveQty = int.Parse(GetReceiveQty(((IQCDetail)obj).IQCNo, ((IQCDetail)obj).STLine).ToString("0"));
                    materialReceive.ReceiveMemo = string.Empty;

                    materialReceive.AccountDate = FormatHelper.TODateInt(DateTime.Now);
                    materialReceive.VoucherDate = FormatHelper.TODateInt(DateTime.Now);

                    IQCDetail iqcDetail = (IQCDetail)this._IQCFacade.GetIQCDetail(materialReceive.IQCNo, materialReceive.STLine);
                    if (iqcDetail != null)
                    {
                        materialReceive.STNo = iqcDetail.STNo;
                        materialReceive.OrganizationID = iqcDetail.OrganizationID;
                        materialReceive.ItemCode = iqcDetail.ItemCode;
                        materialReceive.Unit = iqcDetail.Unit;
                    }

                    obj = materialReceive;
                }
            }

            if (obj != null)
            {
                return (MaterialReceive)obj;
            }

            return null;
        }

        private void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtIQCNoEdit.Text = string.Empty;
                this.txtIQCLineEdit.Text = string.Empty;
                this.txtPurchaseOrderNoEdit.Text = string.Empty;
                this.txtPurchaseOrderLineEdit.Text = string.Empty;

                this.txtStorageIDEdit.Text = string.Empty;
                this.txtRealReceiveQtyEdit.Text = string.Empty;
                this.txtReceiveMemoEdit.Text = string.Empty;

                this.datAccountDateEdit.Text = DateTime.Now.ToString("yyyy-MM-dd"); 
                this.datVoucherDateEdit.Text = DateTime.Now.ToString("yyyy-MM-dd"); 

                return;
            }

            this.txtIQCNoEdit.Text = ((MaterialReceive)obj).IQCNo;
            this.txtIQCLineEdit.Text = ((MaterialReceive)obj).STLine.ToString();
            this.txtPurchaseOrderNoEdit.Text = ((MaterialReceive)obj).OrderNo;
            this.txtPurchaseOrderLineEdit.Text = ((MaterialReceive)obj).OrderLine.ToString();

            this.txtStorageIDEdit.Text = ((MaterialReceive)obj).StorageID;
            this.txtRealReceiveQtyEdit.Text = ((MaterialReceive)obj).RealReceiveQty.ToString("0");
            this.txtReceiveMemoEdit.Text = ((MaterialReceive)obj).ReceiveMemo;

            this.datAccountDateEdit.Text = FormatHelper.ToDateString(((MaterialReceive)obj).AccountDate);
            this.datVoucherDateEdit.Text = FormatHelper.ToDateString(((MaterialReceive)obj).VoucherDate);


        }

        private decimal GetReceiveQty(IQCDetail iqcDetail)
        {
            decimal returnValue = 0;

            if (iqcDetail.CheckStatus == IQCCheckStatus.IQCCheckStatus_Qualified)
            {
                //合格时，接收数量为采购数量
                returnValue = iqcDetail.ReceiveQty;
            }
            else if (iqcDetail.CheckStatus == IQCCheckStatus.IQCCheckStatus_UnQualified)
            {
                if (iqcDetail.ConcessionStatus == "Y")
                {
                    //让步接收时，，接收数量为让步接收数量
                    returnValue = iqcDetail.ConcessionQty;
                }
            }

            return returnValue;
        }

        private decimal GetReceiveQty(string iqcNo, int stLine)
        {
            decimal returnValue = 0;

            IQCDetail iqcDetail = (IQCDetail)_IQCFacade.GetIQCDetail(iqcNo, stLine);

            if (iqcDetail != null)
            {
                returnValue = GetReceiveQty(iqcDetail);
            }

            return returnValue;
        }

        #endregion

        #region For Export To Excel

        private string[] FormatExportRecord(object obj)
        {
            return new string[]{
                ((IQCDetailForReceive)obj).IQCNo,
                ((IQCDetailForReceive)obj).OrderNo,
                ((IQCDetailForReceive)obj).OrderLine.ToString(),
                ((IQCDetailForReceive)obj).VendorCode,
                ((IQCDetailForReceive)obj).VendorName,
                ((IQCDetailForReceive)obj).ROHS,
                ((IQCDetailForReceive)obj).ShipToStock,
                ((IQCDetailForReceive)obj).InventoryUser + ((IQCDetailForReceive)obj).InventoryUserName,
                this.languageComponent1.GetString(((IQCDetailForReceive)obj).IQCHeadAttribute),
                ((IQCDetailForReceive)obj).Inspector + ((IQCDetailForReceive)obj).InspectorUserName,
                FormatHelper.ToDateString(((IQCDetailForReceive)obj).InspectDate),
                ((IQCDetailForReceive)obj).STLine.ToString(),

                ((IQCDetailForReceive)obj).ItemCode,
                ((IQCDetailForReceive)obj).MaterialDescription,
                this.languageComponent1.GetString(((IQCDetailForReceive)obj).Type),
                this.languageComponent1.GetString(((IQCDetailForReceive)obj).Attribute),
                ((IQCDetailForReceive)obj).Unit,
                ((IQCDetailForReceive)obj).StorageID,
                GetReceiveQty((IQCDetailForReceive)obj).ToString("0.00"),
                ((IQCDetailForReceive)obj).CheckStatus,
                ((IQCDetailForReceive)obj).MemoEx
            };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "IQCNo",
                "OrderNo",
                "OrderLine",
                "VendorCode",
                "VendorName",
                "ROHS",
                "ShipToStock",
                "InventoryUser",
                "IQCHeadAttribute",
                "Inspector",
                "InspectDate",
                "ASNLine",

                "MaterialCode",
                "MaterialDesc",
                "IQCDetailReceiveType",
                "IQCDetailAttribute",
                "Unit",
                "StorageID",
                "IQCDetailReceiveQty",
                "IQCDetailCheckStatus",
                "MemoEx"
            };
        }

        #endregion
    }
}
