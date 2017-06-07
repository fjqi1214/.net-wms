using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.IQC
{
    public partial class FCreateIQCMP : BaseMPageMinus
    {

        private System.ComponentModel.IContainer components;
        //private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        //private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        private ExcelExporter excelExporter;

        private IQCFacade _IQCFacade;

        #region Init

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

            // languageComponent1
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";

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

            _IQCFacade = new IQCFacadeFactory(base.DataProvider).CreateIQCFacade();

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
                this.drpIQCStatusQuery_Load();
                this.drpROHSQuery_Load();
                this.drpShipToStockQuery_Load();
                this.drpIQCHeadAttributeEdit_Load();

                //首次进入界面的查询
                string iqcNo = string.Empty;
                if (Request.Params["iqcno"] == null)
                {
                    this.drpIQCStatusQuery.SelectedValue = "New";
                }
                else
                {
                    iqcNo = Request.Params["iqcno"].ToString().Trim();
                }
                if (iqcNo.Length > 0)
                {
                    this.txtIQCNoQuery.Text = iqcNo;
                }

                string asnPO = string.Empty;
                if (Request.Params["asnpo"] != null)
                {
                    asnPO = Request.Params["asnpo"].ToString().Trim();
                }
                if (asnPO.Length > 0)
                {
                    this.txtASNPOQuery.Text = asnPO;
                    this.drpIQCStatusQuery.SelectedValue = "New";
                }

                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
            }

            //添加一些JS事件
            //if (this.txtPOMaterialEdit.Attributes["po"] == null)
            //{
            //    this.txtPOMaterialEdit.Attributes["po"] = "0";
            //}
            if (this.cmdCreateIQCFromASN.Attributes["onclick"] == null)
            {
                this.cmdCreateIQCFromASN.Attributes["onclick"] = "this.style.cursor='wait';";
            }
            //if (this.cmdCreateIQCFromPO.Attributes["onclick"] == null)
            //{
            //    this.cmdCreateIQCFromPO.Attributes["onclick"] = "this.style.cursor='wait';";
            //}            
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected void cmdCreateIQCFromASN_ServerClick(object sender, System.EventArgs e)
        {
            if (!ValidateInputForCreateIQCByASN())
            {
                return;
            }

            string asnNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNEdit.Text));

            if (_IQCFacade.CreateIQC("PO", asnNo, string.Empty, this.GetUserCode()))
            {
                this.txtASNEdit.Text = string.Empty;

                this.txtIQCNoQuery.Text = string.Empty;
                this.txtASNPOQuery.Text = asnNo;
                this.txtVendorCodeQuery.Text = string.Empty;
                this.drpIQCStatusQuery.SelectedValue = "New";
                this.drpROHSQuery.SelectedIndex = 0;
                this.drpShipToStockQuery.SelectedIndex = 0;
                this.datAppDateFromQuery.Text = string.Empty;
                this.datAppDateToQuery.Text = string.Empty;

                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
            }
            else
            {
                WebInfoPublish.PublishInfo(this, "$Error_InvalidASNToCreateIQC", languageComponent1);
            }
        }

        //protected void cmdCreateIQCFromPO_ServerClick(object sender, System.EventArgs e)
        //{
        //    if (!ValidateInputForCreateIQCByPO())
        //    {
        //        return;
        //    }

        //    string poNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPOEdit.Text));
        //    string materialList = txtPOMaterialEdit.Text;

        //    if (_IQCFacade.CreateIQCFromSRM("PO", poNo, materialList, this.GetUserCode()))
        //    {
        //        this.txtPOEdit.Text = string.Empty;
        //        this.txtPOMaterialEdit.Text = string.Empty;

        //        this.txtIQCNoQuery.Text = string.Empty;
        //        this.txtASNPOQuery.Text = "PO" + poNo;
        //        this.txtVendorCodeQuery.Text = string.Empty;
        //        this.drpIQCStatusQuery.SelectedValue = "New";
        //        this.drpROHSQuery.SelectedIndex = 0;
        //        this.drpShipToStockQuery.SelectedIndex = 0;
        //        this.datAppDateFromQuery.Text = string.Empty;
        //        this.datAppDateToQuery.Text = string.Empty;

        //        this.RequestData();
        //        this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        //    }
        //    else
        //    {
        //        WebInfoPublish.PublishInfo(this, "$Error_InvalidPOToCreateIQC", languageComponent1);
        //    }
        //}

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList items = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    IQCHead iqcHead = (IQCHead)this.GetEditObject(row);
                    if (iqcHead != null)
                    {
                        if (this._IQCFacade.IsFromASN(iqcHead.STNo))
                        {
                            WebInfoPublish.Publish(this, "$ONLY_PO_CAN_DELETE $IQCNo:" + iqcHead.IQCNo, this.languageComponent1);
                            this.RequestData();
                            return;
                        }

                        if (iqcHead.Status != IQCStatus.IQCStatus_New)
                        {
                            WebInfoPublish.Publish(this, "$ONLY_NewIQC_CAN_DELETE $IQCNo:" + iqcHead.IQCNo, this.languageComponent1);
                            this.RequestData();
                            return;
                        }

                        items.Add((IQCHead)iqcHead);
                    }
                }

                this._IQCFacade.DeleteIQCHeadAll((IQCHead[])items.ToArray(typeof(IQCHead)));

                this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
            }
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            object iqcHead = this.GetEditObject();

            if (iqcHead != null)
            {
                this._IQCFacade.UpdateIQCHead((IQCHead)iqcHead);

                this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
            }
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }

        protected void cmdSendCheck_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList items = new ArrayList();
                ArrayList itemsFailed = new ArrayList();

                foreach (GridRecord row in array)
                {
                    object iqcHead = this.GetEditObject(row);
                    if (iqcHead != null)
                    {
                        if (((IQCHead)iqcHead).Status == IQCStatus.IQCStatus_New)
                        {
                            items.Add((IQCHead)iqcHead);
                        }
                        else
                        {
                            itemsFailed.Add((IQCHead)iqcHead);
                        }
                    }
                }

                _IQCFacade.SendCheckIQCHead((IQCHead[])items.ToArray(typeof(IQCHead)), this.GetUserCode());                

                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Query);

                string message = string.Empty;

                if (items != null && items.Count > 0)
                {
                    message += "$Message_SendCheckOK" + " : \n";
                    foreach (IQCHead iqcHead in items)
                    {
                        message += iqcHead.IQCNo + "\n";
                    }
                }

                if (itemsFailed != null && itemsFailed.Count > 0)
                {
                    if (message.Trim().Length > 0)
                    {
                        message += "\n";
                    }
                    message += "$Message_SendCheckFailed" + " : \n";
                    foreach (IQCHead iqcHead in itemsFailed)
                    {
                        message += iqcHead.IQCNo + "\n";
                    }
                }

                if (message.Trim().Length > 0)
                {
                    WebInfoPublish.PublishInfo(this, message, languageComponent1);
                }
            }
        }

        protected void cmdBack_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList items = new ArrayList();

                foreach (GridRecord row in array)
                {
                    object iqcHead = this.GetEditObject(row);
                    if (iqcHead != null)
                    {
                        object[] iqcDetail = _IQCFacade.QueryIQCDetailByIQCNO(((IQCHead)iqcHead).IQCNo);

                        bool hasChecked = false;
                        foreach (object item in iqcDetail)
                        {
                            if (((IQCDetail)item).STDStatus == IQCStatus.IQCStatus_New)
                            {
                                WebInfoPublish.Publish(this, "$HAS_NOT_SEND_CHENK $IQCNo:" + ((IQCHead)iqcHead).IQCNo, this.languageComponent1);
                                this.RequestData();
                                return;
                            }
                            if (((IQCDetail)item).STDStatus == IQCStatus.IQCStatus_Close)
                            {
                                WebInfoPublish.Publish(this, "$HAS_CLOSED_CAN_NOT_GOBACK $IQCNo:" + ((IQCHead)iqcHead).IQCNo, this.languageComponent1);
                                this.RequestData();
                                return;
                            } 
                            if (((IQCDetail)item).STDStatus == IQCStatus.IQCStatus_Cancel)
                            {
                                WebInfoPublish.Publish(this, "$HAS_CANCEL_CAN_NOT_GOBACK $IQCNo:" + ((IQCHead)iqcHead).IQCNo, this.languageComponent1);
                                this.RequestData();
                                return;
                            }

                            if (((IQCDetail)item).CheckStatus != IQCCheckStatus.IQCCheckStatus_WaitCheck)
                            {
                                hasChecked = true;
                            }
                        }
                        if (hasChecked)
                        {
                            WebInfoPublish.Publish(this, "$HAS_CHECKED_CAN_NOT_GOBACK $IQCNo:" + ((IQCHead)iqcHead).IQCNo, this.languageComponent1);
                            this.RequestData();
                            return;
                        }

                        items.Add((IQCHead)iqcHead);
                    }
                }

                string message = string.Empty;
                message += "$Message_BackSuccess  $IQCNo" + " : \n";

                this.DataProvider.BeginTransaction();
                try
                {
                    foreach (IQCHead iqcHead in items)
                    {
                        object objASN = _IQCFacade.GetASN(iqcHead.STNo);
                        ((ASN)objASN).STStatus = "NEW";
                        _IQCFacade.UpdateASN((ASN)objASN);

                        object objIQCHead = _IQCFacade.GetIQCHead(iqcHead.IQCNo);
                        ((IQCHead)objIQCHead).Status = IQCStatus.IQCStatus_New;
                        _IQCFacade.UpdateIQCHead((IQCHead)objIQCHead);

                        object[] objIQCDetail = _IQCFacade.QueryIQCDetailByIQCNO(iqcHead.IQCNo);
                        foreach (object obj in objIQCDetail)
                        {
                            ((IQCDetail)obj).STDStatus = IQCStatus.IQCStatus_New;
                            ((IQCDetail)obj).CheckStatus = " ";
                            _IQCFacade.UpdateIQCDetail((IQCDetail)obj);
                        }

                        object objINVReceipt = _IQCFacade.GetInvReceipt(iqcHead.STNo);
                        ((InvReceipt)objINVReceipt).Recstatus = "NEW";
                        _IQCFacade.UpdateInvReceipt((InvReceipt)objINVReceipt);

                        object[] objINVReceiptDetail = _IQCFacade.GetInvReceiptDetailForUpdate(iqcHead.STNo);
                        foreach (object obj in objINVReceiptDetail)
                        {
                            ((InvReceiptDetail)obj).Recstatus = "NEW";
                            ((InvReceiptDetail)obj).Iqcstatus = "NEW";
                            _IQCFacade.UpdateInvReceiptDetail((InvReceiptDetail)obj);
                        }

                        message += iqcHead.IQCNo + "\n";
                    }
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                }
                this.DataProvider.CommitTransaction();

                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Query);

                if (message.Trim().Length > 0)
                {
                    WebInfoPublish.PublishInfo(this, message, languageComponent1);
                }

            }
        }

        protected void cmdIQCPrint_ServerClick(object sender, System.EventArgs e)
        {
            //获取用户选择的行和IQCHead
            ArrayList array = this.gridHelper.GetCheckedRows();

            List<IQCHeadWithVendor> items = new List<IQCHeadWithVendor>();
            foreach (GridRecord row in array)
            {
                object iqcHead = _IQCFacade.GetIQCHeadWithVendor(row.Items.FindItemByKey("IQCNo").Text);
                if (iqcHead != null && ((IQCHeadWithVendor)iqcHead).Status != IQCStatus.IQCStatus_New && ((IQCHeadWithVendor)iqcHead).Status != IQCStatus.IQCStatus_Cancel)
                {
                    items.Add((IQCHeadWithVendor)iqcHead);
                }
            }

            if (items == null || items.Count <= 0)
            {
                WebInfoPublish.PublishInfo(this, "$Message_NoIQCToPrint", languageComponent1);
            }
            else
            {
                DownloadIQCXlsFile(items);
            }
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }
        
        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "Edit")
            {
                object obj = this.GetEditObject(row);

                if (obj != null)
                {
                    this.SetEditObject(obj);

                    if (((IQCHead)obj).Status == IQCStatus.IQCStatus_New)
                    {
                        this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                    }
                    else
                    {
                        buttonHelper_AfterPageStatusChangeHandle(PageActionType.Cancel);
                    }
                }
            }

            else if (commandName == "Detail")
            {
                Response.Redirect(this.MakeRedirectUrl("FIQCDetailMP.aspx", new string[] { "iqcno" }, new string[] { row.Items.FindItemByKey("IQCNo").Text.Trim() }));
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
                this.txtIQCNoEdit.ReadOnly = true;
                this.drpIQCHeadAttributeEdit.Enabled = true;

                this.cmdSendCheck.Disabled = true;
                this.cmdIQCPrint.Disabled = true;
            }
            if (pageAction == PageActionType.Query)
            {
                this.txtIQCNoEdit.ReadOnly = true;
                this.drpIQCHeadAttributeEdit.Enabled = false;

                this.cmdSendCheck.Disabled = false;
                this.cmdIQCPrint.Disabled = false;
            }
            if (pageAction == PageActionType.Save)
            {
                this.txtIQCNoEdit.ReadOnly = true;
                this.drpIQCHeadAttributeEdit.Enabled = false;

                this.cmdSendCheck.Disabled = false;
                this.cmdIQCPrint.Disabled = false;
            }
            if (pageAction == PageActionType.Cancel)
            {
                this.txtIQCNoEdit.ReadOnly = true;
                this.drpIQCHeadAttributeEdit.Enabled = false;

                this.cmdSendCheck.Disabled = false;
                this.cmdIQCPrint.Disabled = false;
            }
            if (pageAction == PageActionType.Add)
            {
                this.txtIQCNoEdit.ReadOnly = true;
                this.drpIQCHeadAttributeEdit.Enabled = false;

                this.cmdSendCheck.Disabled = false;
                this.cmdIQCPrint.Disabled = false;
            }
        }

        private bool ValidateInput()
        {
            return true;
        }

        private bool ValidateInputForCreateIQCByASN()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblReceiptNOEdit, txtASNEdit, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.PublishInfo(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            ASN asn = (ASN)_IQCFacade.GetASN(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNEdit.Text)));

            if (asn != null)
            {
                WebInfoPublish.PublishInfo(this, "$Error_ReceiptNOHasBeenDealed", languageComponent1);
                return false;
            }

            return true;
        }

        //private bool ValidateInputForCreateIQCByPO()
        //{
        //    PageCheckManager manager = new PageCheckManager();
        //    manager.Add(new LengthCheck(lblPOEdit, txtPOEdit, 40, true));
        //    manager.Add(new LengthCheck(lblPOMaterialEdit, txtPOMaterialEdit, 4000, true));

        //    if (!manager.Check())
        //    {
        //        WebInfoPublish.PublishInfo(this, manager.CheckMessage, languageComponent1);
        //        return false;
        //    }

        //    return true;
        //}

        #endregion

        #region For Page_Load

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        private void InitButtonHelp()
        {
            this.buttonHelper.AddDeleteConfirm();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

            this.buttonHelper.AddButtonConfirm(cmdSendCheck, languageComponent1.GetString("SendCheckConfirm"));
        }

        private void drpIQCStatusQuery_Load()
        {
            drpIQCStatusQuery.Items.Add(new ListItem("", ""));
            drpIQCStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString(IQCStatus.IQCStatus_New), IQCStatus.IQCStatus_New));
            drpIQCStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString(IQCStatus.IQCStatus_WaitCheck), IQCStatus.IQCStatus_WaitCheck));
            drpIQCStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString(IQCStatus.IQCStatus_Close), IQCStatus.IQCStatus_Close));
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

        private void drpIQCHeadAttributeEdit_Load()
        {
            drpIQCHeadAttributeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(IQCHeadAttribute.IQCHeadAttribute_Normal), IQCHeadAttribute.IQCHeadAttribute_Normal));
            drpIQCHeadAttributeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(IQCHeadAttribute.IQCHeadAttribute_RePO), IQCHeadAttribute.IQCHeadAttribute_RePO));
            drpIQCHeadAttributeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(IQCHeadAttribute.IQCHeadAttribute_Present), IQCHeadAttribute.IQCHeadAttribute_Present));
            drpIQCHeadAttributeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(IQCHeadAttribute.IQCHeadAttribute_BranchReturn), IQCHeadAttribute.IQCHeadAttribute_BranchReturn));
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
            return this._IQCFacade.QueryIQCHeadWithVendor(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtIQCNoQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNPOQuery.Text)),
                this.drpIQCStatusQuery.SelectedValue,
                this.drpROHSQuery.SelectedValue,
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVendorCodeQuery.Text)),
                FormatHelper.TODateInt(this.datAppDateFromQuery.Text),
                FormatHelper.TODateInt(this.datAppDateToQuery.Text),
                this.drpShipToStockQuery.SelectedValue,
                string.Empty,
                string.Empty,
                inclusive,
                exclusive
                );
        }

        private int GetRowCount()
        {
            return this._IQCFacade.QueryIQCHeadWithVendorCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtIQCNoQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNPOQuery.Text)),
                this.drpIQCStatusQuery.SelectedValue,
                this.drpROHSQuery.SelectedValue,
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVendorCodeQuery.Text)),
                FormatHelper.TODateInt(this.datAppDateFromQuery.Text),
                FormatHelper.TODateInt(this.datAppDateToQuery.Text),
                this.drpShipToStockQuery.SelectedValue,
                string.Empty,
                string.Empty
                );
        }

        #endregion

        #region For Grid And Edit

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("IQCNo", "送检单",150);
            this.gridHelper.AddLinkColumn("Detail", "详细", null);
            this.gridHelper.AddColumn("VendorCode", "供应商代码", null);
            this.gridHelper.AddColumn("VendorName", "供应商名称", null);
            this.gridHelper.AddColumn("ROHS", "ROHS", 50);
            this.gridHelper.AddColumn("ShipToStock", "免检", 50);
            this.gridHelper.AddColumn("InventoryUser", "保管员", null);
            this.gridHelper.AddColumn("IQCStatus", "状态", null);
            this.gridHelper.AddColumn("Applicant", "送检员", null);
            this.gridHelper.AddColumn("AppDate", "送检日期", null);
            this.gridHelper.AddColumn("AppTime", "送检时间", null);
            this.gridHelper.AddColumn("IQCHeadAttribute", "类型", 50);

            this.gridHelper.AddDefaultColumn(true, true);

            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected DataRow GetGridRow(object obj)
        {
            DataRow row = DtSource.NewRow();
            row["IQCNo"]=((IQCHeadWithVendor)obj).IQCNo;
            row["Detail"]=        "";
            row["VendorCode"]=        ((IQCHeadWithVendor)obj).VendorCode;
            row["VendorName"]=        ((IQCHeadWithVendor)obj).VendorName;
            row["ROHS"]=        ((IQCHeadWithVendor)obj).ROHS;
            row["ShipToStock"]=        ((IQCHeadWithVendor)obj).STS;
            row["InventoryUser"]=        ((IQCHeadWithVendor)obj).GetDisplayText("InventoryUser"); 
            row["IQCStatus"]=        this.languageComponent1.GetString(((IQCHeadWithVendor)obj).Status);
            row["Applicant"]=        ((IQCHeadWithVendor)obj).Applicant + ((IQCHeadWithVendor)obj).ApplicantUserName;
            row["AppDate"]=        FormatHelper.ToDateString(((IQCHeadWithVendor)obj).AppDate);
            row["AppTime"]=        FormatHelper.ToTimeString(((IQCHeadWithVendor)obj).AppTime);
            row["IQCHeadAttribute"]=        this.languageComponent1.GetString(((IQCHeadWithVendor)obj).Attribute);
            return row;
        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                IQCHead iqcHead = (IQCHead)this._IQCFacade.GetIQCHead(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtIQCNoEdit.Text)));
                iqcHead.Attribute = this.drpIQCHeadAttributeEdit.SelectedValue;
                return iqcHead;
            }
            else
            {
                return null;
            }
        }

        private object GetEditObject(GridRecord row)
        {
            object obj = this._IQCFacade.GetIQCHead(row.Items.FindItemByKey("IQCNo").Value.ToString());

            if (obj != null)
            {
                return (IQCHead)obj;
            }

            return null;
        }

        private void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtIQCNoEdit.Text = string.Empty;
                this.drpIQCHeadAttributeEdit.SelectedIndex = 0;

                return;
            }

            this.txtIQCNoEdit.Text = ((IQCHead)obj).IQCNo;

            try
            {
                this.drpIQCHeadAttributeEdit.SelectedValue = ((IQCHead)obj).Attribute;
            }
            catch
            {
                this.drpIQCHeadAttributeEdit.SelectedIndex = 0;
            }
        }

        #endregion

        #region For Export To Excel

        private string[] FormatExportRecord(object obj)
        {
            return new string[]{
                ((IQCHeadWithVendor)obj).IQCNo,
                ((IQCHeadWithVendor)obj).VendorCode,
                ((IQCHeadWithVendor)obj).VendorName,
                ((IQCHeadWithVendor)obj).ROHS,
                ((IQCHeadWithVendor)obj).STS,
                ((IQCHeadWithVendor)obj).GetDisplayText("InventoryUser"),
                this.languageComponent1.GetString(((IQCHeadWithVendor)obj).Status),
                ((IQCHeadWithVendor)obj).Applicant + ((IQCHeadWithVendor)obj).ApplicantUserName,
                FormatHelper.ToDateString(((IQCHeadWithVendor)obj).AppDate),
                FormatHelper.ToTimeString(((IQCHeadWithVendor)obj).AppTime),
                this.languageComponent1.GetString(((IQCHeadWithVendor)obj).Attribute)
            };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "IQCNo",
                "VendorCode",	
                "VendorName",
                "ROHS",
                "ShipToStock",
                "InventoryUser",
                "IQCStatus",
                "Applicant",
                "AppDate",
                "AppTime",
                "IQCHeadAttribute"
            };
        }

        #endregion

        #region For XML-Excel download

        private string GetTemplateFilePath()
        {
            string returnValue = this.Request.PhysicalApplicationPath + @"download\IQCTemplate.xml";

            if (!File.Exists(returnValue))
            {
                returnValue = string.Empty;
            }

            return returnValue;
        }

        private string GetOutputFilePath(out string fileName)
        {
            string returnValue = string.Empty;

            string file = string.Format("{0}_{1}_{2}", "IQC", FormatHelper.TODateInt(System.DateTime.Now).ToString(), FormatHelper.TOTimeInt(System.DateTime.Now).ToString());
            string path = this.Request.PhysicalApplicationPath + @"upload\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = string.Format(@"{0}{1}{2}", path, file, ".xls");
            while (File.Exists(path))
            {
                file = string.Format("{0}_{1}", file, "0");
                path = string.Format(@"{0}{1}{2}", path, file, ".xls");
            }

            returnValue = path;
            fileName = file;

            return returnValue;
        }

        private XmlNode GetChildNode(XmlNode root, string childNodeName, int seq)
        {
            XmlNode returnValue = null;

            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Name == childNodeName)
                {
                    seq--;
                    if (seq == 0)
                    {
                        returnValue = node;
                        break;
                    }
                }
            }

            return returnValue;
        }

        private void DownloadIQCXlsFile(List<IQCHeadWithVendor> headList)
        {
            if (headList.Count <= 0)
            {
                return;
            }

            UserFacade userFacade = new UserFacade(this.DataProvider);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            //获取下载模板及Workbook节点和Worksheet节点
            string templateFilePath = GetTemplateFilePath();
            if (templateFilePath.Trim().Length <= 0)
            {
                WebInfoPublish.PublishInfo(this, "文件[IQCTemplate.xml]不存在", languageComponent1);
                return;
            }
            XmlDocument template = new XmlDocument();
            template.Load(templateFilePath);

            XmlNode workbook = null;
            XmlNode worksheet = null;
            workbook = GetChildNode(template, "Workbook", 1);
            if (workbook == null)
            {
                WebInfoPublish.PublishInfo(this, "文件[IQCTemplate.xml]内容不正确", languageComponent1);
                return;
            }
            worksheet = GetChildNode(workbook, "Worksheet", 1);
            if (worksheet == null)
            {
                WebInfoPublish.PublishInfo(this, "文件[IQCTemplate.xml]内容不正确", languageComponent1);
                return;
            }

            //处理每个IQCHead为一个Worksheet
            foreach (IQCHeadWithVendor iqcHead in headList)
            {
                XmlNode newWorksheet = worksheet.Clone();
                StringBuilder newInnerXml = new StringBuilder(newWorksheet.InnerXml);

                //表头数据和表尾数据
                User appUser = (User)userFacade.GetUser(iqcHead.Applicant);
                User invUser = (User)userFacade.GetUser(iqcHead.InventoryUser);

                newInnerXml.Replace("$$IQCNo$$", CommonHelper.ConvertXML(iqcHead.IQCNo));
                newInnerXml.Replace("$$ASN$$", CommonHelper.ConvertXML(iqcHead.STNo));
                newInnerXml.Replace("$$PrintDate$$", CommonHelper.ConvertXML(FormatHelper.ToDateString(dbDateTime.DBDate)));
                newInnerXml.Replace("$$PrintTime$$", CommonHelper.ConvertXML(FormatHelper.ToTimeString(dbDateTime.DBTime)));
                newInnerXml.Replace("$$IQCStatus$$", CommonHelper.ConvertXML(this.languageComponent1.GetString(iqcHead.Status)));
                newInnerXml.Replace("$$VendorCode$$", CommonHelper.ConvertXML(iqcHead.VendorCode));
                newInnerXml.Replace("$$VendorName$$", CommonHelper.ConvertXML(iqcHead.VendorName));

                newInnerXml.Replace("$$Storage$$", CommonHelper.ConvertXML(iqcHead.StorageCode + "-" + iqcHead.StorageName));

                newInnerXml.Replace("$$AppUserName$$", appUser == null ? CommonHelper.ConvertXML(iqcHead.Applicant) : CommonHelper.ConvertXML(appUser.UserName));
                newInnerXml.Replace("$$AppDateTime$$", CommonHelper.ConvertXML(FormatHelper.ToDateString(iqcHead.AppDate) + " " + FormatHelper.ToTimeString(iqcHead.AppTime)));
                newInnerXml.Replace("$$InventoryUserName$$", invUser == null ? CommonHelper.ConvertXML(iqcHead.InventoryUser) : CommonHelper.ConvertXML(invUser.UserName));

                newInnerXml.Replace("$$IQCHeadAttribute$$", CommonHelper.ConvertXML(this.languageComponent1.GetString(iqcHead.Attribute)));
                newInnerXml.Replace("$$ShipToStock$$", iqcHead.STS == "Y" ? CommonHelper.ConvertXML(this.languageComponent1.GetString("ShipToStock")) : string.Empty);
                newInnerXml.Replace("$$ROHS$$", iqcHead.ROHS == "Y" ? "ROHS" : string.Empty);

                newWorksheet.InnerXml = newInnerXml.ToString();

                //获取行数据
                object[] iqcDetailList = _IQCFacade.QueryIQCDetailWithMaterial(iqcHead.IQCNo,string.Empty,IQCStatus.IQCStatus_Cancel ,int.MinValue, int.MaxValue);
                if (iqcDetailList == null)
                {
                    iqcDetailList = new object[0];
                }

                //获取行模板
                List<XmlNode> rowTemplateList = new List<XmlNode>();
                XmlNode firstTable = GetChildNode(newWorksheet, "Table", 1);
                foreach (XmlNode node in firstTable.ChildNodes)
                {
                    if (node.Name == "Row")
                    {
                        rowTemplateList.Add(node);
                    }
                }

                int ticketCount = 0;
                for (int i = 0; i < iqcDetailList.Length; i++)
                {
                    if (i % 10 == 0)
                    {
                        foreach (XmlNode node in rowTemplateList)
                        {
                            XmlNode newNode = node.CloneNode(true);

                            if (newNode.Attributes.GetNamedItem("detailLineNo") != null)
                            {
                                int detailLineNo = int.Parse(node.Attributes.GetNamedItem("detailLineNo").Value);
                                IQCDetailWithMaterial detial = null;
                                if (ticketCount * 10 + detailLineNo < iqcDetailList.Length)
                                {
                                    detial = (IQCDetailWithMaterial)iqcDetailList[ticketCount * 10 + detailLineNo];
                                }

                                StringBuilder lineInnerXml = new StringBuilder(newNode.InnerXml);

                                lineInnerXml.Replace("$$IQCLine$$", detial == null ? string.Empty : CommonHelper.ConvertXML(detial.STLine.ToString()));
                                lineInnerXml.Replace("$$MaterialCode$$", detial == null ? string.Empty : CommonHelper.ConvertXML(detial.ItemCode));
                                lineInnerXml.Replace("$$MaterialDesc$$", detial == null ? string.Empty : CommonHelper.ConvertXML(detial.MaterialDescription));                                
                                lineInnerXml.Replace("$$Unit$$", detial == null ? string.Empty : CommonHelper.ConvertXML(detial.Unit));
                                lineInnerXml.Replace("$$ReceiveQty$$", detial == null ? string.Empty : CommonHelper.ConvertXML(detial.ReceiveQty.ToString("0.00")));
                                lineInnerXml.Replace("$$OrderNo$$", detial == null ? string.Empty : CommonHelper.ConvertXML(detial.OrderNo));
                                lineInnerXml.Replace("$$OrderLine$$", detial == null ? string.Empty : CommonHelper.ConvertXML(detial.OrderLine.ToString()));
                                lineInnerXml.Replace("$$IQCDetailAttribute$$", detial == null ? string.Empty : CommonHelper.ConvertXML(this.languageComponent1.GetString(detial.Attribute)));

                                lineInnerXml.Replace("$$Memo$$", detial == null ? string.Empty : CommonHelper.ConvertXML(detial.InvreceiptDetailMemo.ToString()));
                                lineInnerXml.Replace("$$VenderLotNo$$", detial == null ? string.Empty : CommonHelper.ConvertXML(detial.VenderLotNo.ToString()));

                                newNode.InnerXml = lineInnerXml.ToString();
                            }

                            firstTable.AppendChild(newNode);
                        }

                        ticketCount++;
                    }
                }

                firstTable.Attributes.GetNamedItem("ss:ExpandedRowCount").Value = Convert.ToString(ticketCount * rowTemplateList.Count);

                foreach (XmlNode node in rowTemplateList)
                {
                    firstTable.RemoveChild(node);
                }

                newWorksheet.Attributes.GetNamedItem("ss:Name").Value = iqcHead.IQCNo.Trim();
                workbook.AppendChild(newWorksheet);
            }

            //保存新生成的xls
            workbook.RemoveChild(worksheet);
            string filename = string.Empty;
            template.Save(GetOutputFilePath(out filename));

            //下载xls文件
            this.DownloadFile(filename);
        }

        #endregion
    }
}
