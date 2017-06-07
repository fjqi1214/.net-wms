#region system
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
using System.Web.UI.WebControls.WebParts;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
#endregion

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.Domain.BaseSetting;


namespace BenQGuru.eMES.Web.IQC
{
    public partial class FIQCDetailMP : BaseMPageMinus
    {
        // protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        // private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
        IQCFacade _IQCFacade = null;

        #region  Init

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
            // 
            // languageComponent1
            //
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";
            // 
            // excelExporter
            // 
            this.excelExporter.FileExtension = "xls";
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            this.excelExporter.RowSplit = "\r\n";
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            _IQCFacade = new IQCFacade(this.DataProvider);

            InitOnPostBack();

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                // 初始化界面UI
                this.InitUI();
                InitButtonHelp();
                SetEditObject(null);
                this.InitWebGrid();
                this.txtIQCNo.Enabled = false;

                this.InitParameters();
                if (this.txtIQCNo.Text.Trim() != string.Empty)
                {
                    RequestData();
                    this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
                }
            }
        }

        private void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Update)
            {
                this.cmdIQCCancel.Disabled = true;
            }
            if (pageAction == PageActionType.Query)
            {
                this.cmdIQCCancel.Disabled = false;
            }
            if (pageAction == PageActionType.Save)
            {
                this.cmdIQCCancel.Disabled = false;
            }
            if (pageAction == PageActionType.Cancel)
            {
                this.cmdIQCCancel.Disabled = false;
            }
            if (pageAction == PageActionType.Add)
            {
                this.cmdIQCCancel.Disabled = false;
            }
        }

        protected void cmdSave_ServerClick(object sender, EventArgs e)
        {
            if (this.ValidateInput())
            {
                DBDateTime DBDateTimeNow = FormatHelper.GetNowDBDateTime(this.DataProvider);
                object ObjectIQCDetail = this.GetEditObject();

                object objectIQCDetailGet = this._IQCFacade.GetIQCDetail(((IQCDetail)ObjectIQCDetail).IQCNo, ((IQCDetail)ObjectIQCDetail).STLine);

                if (objectIQCDetailGet != null)
                {
                    ((IQCDetail)objectIQCDetailGet).Attribute = ((IQCDetail)ObjectIQCDetail).Attribute;
                    ((IQCDetail)objectIQCDetailGet).PurchaseMEMO = ((IQCDetail)ObjectIQCDetail).PurchaseMEMO;
                    object objectASN = this._IQCFacade.GetASN(((IQCDetail)objectIQCDetailGet).STNo);
                    if (objectASN != null)
                    {
                        if (((ASN)objectASN).Flag.Trim() == IQCTicketType.IQCTicketType_PO)
                        {
                            ((IQCDetail)objectIQCDetailGet).ReceiveQty = Math.Round(((IQCDetail)ObjectIQCDetail).ReceiveQty, 2);
                        }
                    }


                    ((IQCDetail)objectIQCDetailGet).MaintainDate = DBDateTimeNow.DBDate;
                    ((IQCDetail)objectIQCDetailGet).MaintainTime = DBDateTimeNow.DBTime;
                    ((IQCDetail)objectIQCDetailGet).MaintainUser = this.GetUserCode();
                    _IQCFacade.UpdateIQCDetail((IQCDetail)objectIQCDetailGet);
                }

                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
            }
        }

        protected void cmdCancel_ServerClick(object sender, EventArgs e)
        {
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }

        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            //Response.Redirect(this.MakeRedirectUrl("FCreateIQCMP.aspx?iqcno=" + this.txtIQCNo.Text.Trim().ToUpper()));

            string asnPO = string.Empty;
            IQCHead iqcHead = (IQCHead)_IQCFacade.GetIQCHead(this.txtIQCNo.Text.Trim().ToUpper());
            if (iqcHead != null)
            {
                ASN asn = (ASN)_IQCFacade.GetASN(iqcHead.STNo);
                if (asn != null)
                {
                    asnPO = asn.STNo;
                }
            }

            if (asnPO.Trim().Length > 0)
            {
                Response.Redirect(this.MakeRedirectUrl("FCreateIQCMP.aspx?asnpo=" + asnPO));
            }
            else
            {
                Response.Redirect(this.MakeRedirectUrl("FCreateIQCMP.aspx"));
            }
        }

        protected void cmdIQCCancel_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList items = new ArrayList();
                ArrayList itemsFailed = new ArrayList();

                foreach (GridRecord row in array)
                {
                    IQCDetail iqcDetail = (IQCDetail)this.GetEditObject(row);
                    if (iqcDetail != null)
                    {
                        if (iqcDetail.STDStatus == IQCStatus.IQCStatus_New)
                        {
                            items.Add(iqcDetail);
                        }
                        else if (iqcDetail.STDStatus == IQCStatus.IQCStatus_WaitCheck)
                        {
                            if (iqcDetail.ConcessionStatus == "Y")
                            {
                                itemsFailed.Add((IQCDetail)iqcDetail);
                            }
                            else if (_IQCFacade.GetMaterialReceive(iqcDetail.IQCNo, iqcDetail.STLine) == null)
                            {
                                items.Add((IQCDetail)iqcDetail);
                            }
                            else
                            {
                                itemsFailed.Add((IQCDetail)iqcDetail);
                            }
                        }
                        else
                        {
                            itemsFailed.Add(iqcDetail);
                        }
                    }
                }

                _IQCFacade.CancelIQCDetail((IQCDetail[])items.ToArray(typeof(IQCDetail)), this.GetUserCode());

                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Query);

                string message = string.Empty;

                if (items != null && items.Count > 0)
                {
                    message += "$Message_IQCCancelOK" + " : \n";
                    foreach (IQCDetail item in items)
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

                    message += "$Message_IQCCancelFailed" + " : \n";
                    foreach (IQCDetail item in itemsFailed)
                    {
                        message += item.IQCNo + "," + item.STLine + "\n";
                    }
                }

                if (message.Trim().Length > 0)
                {
                    WebInfoPublish.PublishInfo(this, message, languageComponent1);
                }
            }
        }

        protected void txtPageLineNumber_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.DrpControlserverType.Items.Clear();
                this.DrpControlserverType.Items.Add(new ListItem("", ""));
                this.DrpControlserverType.Items.Add(new ListItem(this.languageComponent1.GetString(IQCDetailAttribute.IQCDetailAttribute_Normal), IQCDetailAttribute.IQCDetailAttribute_Normal));
                this.DrpControlserverType.Items.Add(new ListItem(this.languageComponent1.GetString(IQCDetailAttribute.IQCDetailAttribute_Claim), IQCDetailAttribute.IQCDetailAttribute_Claim));
                this.DrpControlserverType.Items.Add(new ListItem(this.languageComponent1.GetString(IQCDetailAttribute.IQCDetailAttribute_Try), IQCDetailAttribute.IQCDetailAttribute_Try));
                this.DrpControlserverType.Items.Add(new ListItem(this.languageComponent1.GetString(IQCDetailAttribute.IQCDetailAttribute_TS_Market), IQCDetailAttribute.IQCDetailAttribute_TS_Market));
            }
        }

        protected void cmdDelete_ServerClick(object sender, EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            ArrayList IQCDetailList = new ArrayList(array.Count);

            if (array.Count > 0)
            {
                foreach (GridRecord row in array)
                {
                    if (this._IQCFacade.IsFromASN(row.Items.FindItemByKey("ReceiptNO").Value.ToString()))
                    {
                        WebInfoPublish.Publish(this, "$ONLY_PO_CAN_DELETE $IQCNo:" + row.Items.FindItemByKey("IQCNo").Text, this.languageComponent1);
                        this.RequestData();
                        return;
                    }

                    object obj = this.GetEditObject(row);
                    IQCDetailList.Add((IQCDetail)obj);
                }
            }
            this._IQCFacade.DeleteIQCDetail(((IQCDetail[])IQCDetailList.ToArray(typeof(IQCDetail))));

            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            if (this.ViewState["iqcno"].ToString().Substring(0, 2) == IQCTicketType.IQCTicketType_PO)
            {
                manager.Add(new DecimalCheck(this.lblSendQty, this.txtSendQty, 1, int.MaxValue, false));
            }

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }
            return true;
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "Edit")
            {
                object obj = this.GetEditObject(row);

                if (obj != null)
                {
                    this.SetEditObject(obj);
                    this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
        }

        protected DataRow GetGridRow(object obj)
        {
            DataRow row = DtSource.NewRow();
            row["IQCNo"] = ((IQCDetailWithMaterial)obj).IQCNo;
            row["ReceiptNO"] = ((IQCDetailWithMaterial)obj).STNo;
            row["IQCStLine"] = ((IQCDetailWithMaterial)obj).STLine;
            row["MaterialCode"] = ((IQCDetailWithMaterial)obj).ItemCode;
            row["MaterialDesc"] = ((IQCDetailWithMaterial)obj).MaterialDescription;
            row["Unit"] = ((IQCDetailWithMaterial)obj).Unit;
            row["SendQty"] = Math.Round(((IQCDetailWithMaterial)obj).ReceiveQty, 2);
            row["IQCOrderNO"] = ((IQCDetailWithMaterial)obj).OrderNo;
            row["OrderLine"] = ((IQCDetailWithMaterial)obj).OrderLine;
            row["STDStatus"] = String.IsNullOrEmpty(this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).STDStatus)) ? (((IQCDetailWithMaterial)obj).STDStatus) : (this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).STDStatus));
            row["AttriBute"] = this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).Attribute);
            row["PurchaseMEMO"] = ((IQCDetailWithMaterial)obj).PurchaseMEMO;
            return row;
        }

        #endregion

        #region For Page_Load

        private void InitParameters()
        {
            if (this.Request.Params["iqcno"] == null)
            {
                this.ViewState["iqcno"] = string.Empty;
            }
            else
            {
                this.ViewState["iqcno"] = this.Request.Params["iqcno"];
            }

            this.txtIQCNo.Text = this.ViewState["iqcno"].ToString();
        }

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("IQCNo", "送检单号", null);
            this.gridHelper.AddColumn("ReceiptNO", "入库单号", null);
            this.gridHelper.AddColumn("IQCStLine", "行号码", null);
            this.gridHelper.AddColumn("MaterialCode", "物料代码", null);
            this.gridHelper.AddColumn("MaterialDesc", "物料描述", null);
            //this.gridHelper.AddColumn("IQCPlanDate", "排程日期", null);
            //this.gridHelper.AddColumn("IQCPlanQTY", "排程数量", null);
            this.gridHelper.AddColumn("Unit", "单位", null);
            this.gridHelper.AddColumn("SendQty", "送货数量", null);
            this.gridHelper.AddColumn("IQCOrderNO", "采购订单", null);
            this.gridHelper.AddColumn("OrderLine", "订单行", null);
            this.gridHelper.AddColumn("STDStatus", "状态", null);
            this.gridHelper.AddColumn("AttriBute", "类型", null);
            this.gridHelper.AddColumn("PurchaseMEMO", "备注", null);

            this.gridWebGrid.Columns.FromKey("IQCNo").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ReceiptNO").Hidden = true;

            // 2005-04-06
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        public void InitButtonHelp()
        {
            this.buttonHelper.AddDeleteConfirm();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

            this.buttonHelper.AddButtonConfirm(cmdIQCCancel, languageComponent1.GetString("IQCCancelConfirm"));
        }

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
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

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            return this._IQCFacade.QueryIQCDetailWithMaterial(FormatHelper.CleanString(this.txtIQCNo.Text.Trim().ToUpper()), inclusive, exclusive);
        }

        private int GetRowCount()
        {
            return this._IQCFacade.QueryIQCDetailWithMaterialCount(FormatHelper.CleanString(this.txtIQCNo.Text.Trim().ToUpper()));
        }

        #endregion

        #region For Grid And Edit

        private void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtPageLineNumber.Text = string.Empty;
                this.txtSendQty.Text = string.Empty;
                this.DrpControlserverType.SelectedIndex = 0;
                this.txtSTNO.Text = string.Empty;
                this.txtPurchaseMEMO.Text = string.Empty;
            }
            else
            {
                this.txtPageLineNumber.Text = ((IQCDetail)obj).STLine.ToString();
                this.txtSendQty.Text = Math.Round(((IQCDetail)obj).ReceiveQty, 2).ToString();
                this.DrpControlserverType.SelectedValue = ((IQCDetail)obj).Attribute.ToString();
                this.txtSTNO.Text = ((IQCDetail)obj).STNo.ToString();
                this.txtPurchaseMEMO.Text = ((IQCDetail)obj).PurchaseMEMO.ToString();

                if (((IQCDetail)obj).STDStatus == IQCStatus.IQCStatus_New)
                {
                    this.txtSendQty.Enabled = true;
                    this.txtPageLineNumber.Enabled = true;
                    this.DrpControlserverType.Enabled = true;
                    this.cmdSave.Disabled = false;
                }
                else
                {
                    this.txtSendQty.Enabled = false;
                    this.txtPageLineNumber.Enabled = false;
                    this.DrpControlserverType.Enabled = false;
                    this.cmdSave.Disabled = true;
                }

                object objectASN = this._IQCFacade.GetASN(((IQCDetail)obj).STNo.Trim());
                if (objectASN != null)
                {
                    if (((ASN)objectASN).Flag.Trim() == IQCTicketType.IQCTicketType_ASN)
                    {
                        this.txtSendQty.Enabled = false;
                        this.txtPageLineNumber.Enabled = false;
                    }

                    if (((ASN)objectASN).Flag.Trim() == IQCTicketType.IQCTicketType_PO)
                    {
                        this.txtPageLineNumber.Enabled = false;
                    }
                }
            }
        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                IQCDetail iqcDetail = this._IQCFacade.CreateNewIQCDetail();

                iqcDetail.IQCNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtIQCNo.Text));
                iqcDetail.STNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSTNO.Text));
                iqcDetail.STLine = System.Int32.Parse(FormatHelper.CleanString(this.txtPageLineNumber.Text));
                iqcDetail.Attribute = FormatHelper.CleanString(this.DrpControlserverType.SelectedValue);
                iqcDetail.ReceiveQty = Math.Round(Convert.ToDecimal(FormatHelper.CleanString(this.txtSendQty.Text)), 2);
                iqcDetail.PurchaseMEMO = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPurchaseMEMO.Text));

                return iqcDetail;
            }
            else
            {
                return null;
            }
        }

        private object GetEditObject(GridRecord row)
        {
            object obj = _IQCFacade.GetIQCDetail(row.Items.FindItemByKey("IQCNo").Value.ToString(), System.Int32.Parse(row.Items.FindItemByKey("IQCStLine").Value.ToString()));

            if (obj != null)
            {
                return (IQCDetail)obj;
            }

            return null;

        }

        #endregion

        #region For Export To Excel

        protected void cmdGridExport_ServerClick(object sender, EventArgs e)
        {
            this.excelExporter.Export();
        }

        private string[] FormatExportRecord(object obj)
        {
            return new string[]{
                ((IQCDetailWithMaterial)obj).IQCNo ,
                    ((IQCDetailWithMaterial)obj).STNo,
                    ((IQCDetailWithMaterial)obj).STLine.ToString() ,
                    ((IQCDetailWithMaterial)obj).ItemCode ,
                    ((IQCDetailWithMaterial)obj).MaterialDescription ,
                    ((IQCDetailWithMaterial)obj).Unit,
                    Convert.ToString(Math.Round(((IQCDetailWithMaterial)obj).ReceiveQty,2)),
                    ((IQCDetailWithMaterial)obj).OrderNo,
                    ((IQCDetailWithMaterial)obj).OrderLine.ToString(),
                    String.IsNullOrEmpty(this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).STDStatus))?(((IQCDetailWithMaterial)obj).STDStatus):(this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).STDStatus)),
                    this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).Attribute),
                    ((IQCDetailWithMaterial)obj).PurchaseMEMO,                                
                            };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {
                                "IQCNo",
                                "ReceiptNO",
                                "IQCStLine",
                                "MaterialCode",
                                "MaterialDesc",
                                "Unit",
                                "SendQty",
                                "IQCOrderNO",
                                "OrderLine",
                                "STDStatus",
                                "AttriBute",
                                "PurchaseMEMO"
                                };
        }

        #endregion
    }
}
