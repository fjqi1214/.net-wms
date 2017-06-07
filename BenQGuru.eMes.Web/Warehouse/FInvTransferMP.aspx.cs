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
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FInvTransferMP : BaseMPageMinus
    {

        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        //private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
        WarehouseFacade _TransferFacade = null;
        private MOModel.MOFacade _MOFacade = null;
        BaseModelFacade baseFacade = null;
        InventoryFacade _InventoryFacade = null;
        Hashtable recTypeHT = new Hashtable();

        #region Web Init
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

        #region  Events

        protected void Page_Load(object sender, EventArgs e)
        {
            baseFacade = new BaseModelFacade(this.DataProvider);
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

                this.InitParameters();

                // 初始化控件
                this.DrpRECType_Load();
                this.drpTransferStatus_Load();
                this.DrpRECTypeEdit_Load();

                //this.cmdSave.Disabled = true;
                //this.cmdAdd.Disabled = false;
                //this.txtTransferNoEdit.Enabled = true;

                if (this.txtTransferNO.Text == string.Empty)
                {
                    this.dateCreateDateFromQuery.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") ;
                    this.dateCreateDateToQuery.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }

                ViewState["operate"] = "Add";
                RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Query);



            }
        }

        // add by andy xin 2010-12-2
        public ArrayList GetCheckedRows()
        {
            ArrayList array = new ArrayList();

            foreach (Infragistics.WebUI.UltraWebGrid.UltraGridRow row in this.gridWebGrid.Rows)
            {
                if (row.Cells.FromKey("Check").ToString() == "true")
                {
                    array.Add(row);
                }
            }

            return array;
        }

        private string CreateNewTransNo()
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            if (_TransferFacade == null)
            {
                _TransferFacade = new WarehouseFacade(this.DataProvider);
            }
            string maxserial = _TransferFacade.GetMaxSerial("MES");

            //如果已是最大值就返回为空
            if (maxserial == "9999999")
            {
                return "";
            }

            SERIALBOOK serialbook = new SERIALBOOK();

            if (maxserial == "")
            {
                serialbook.SNPrefix = "MES";
                serialbook.MaxSerial = "1";
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = this.GetUserCode();
                _TransferFacade.AddSerialBook(serialbook);
                return "MES" + string.Format("{0:0000000}", int.Parse(serialbook.MaxSerial));
            }
            else
            {
                serialbook.SNPrefix = "MES";
                serialbook.MaxSerial = (int.Parse(maxserial) + 1).ToString();
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = this.GetUserCode();
                _TransferFacade.UpdateSerialBook(serialbook);
                return "MES" + string.Format("{0:0000000}", int.Parse(serialbook.MaxSerial));
            }
        }

        protected void cmdMerge_ServerClick(object sender, EventArgs e)
        {
            ArrayList array = this.GetCheckedRows();
            //A：检查Grid中至少选择2笔以上记录
            //marked by seven  2010-12-22
            //if (array.Count < 2)
            //{
            //    WebInfoPublish.PublishInfo(this, "$Error_Row_Selected_More_Two", this.languageComponent1);
            //    return;
            //}
            //B：检查选择的入库单类型，源库别，收货库别，组织必须一致。
            Hashtable htRecType = new Hashtable();
            Hashtable htFromStorageNo = new Hashtable();
            Hashtable htToStorageNo = new Hashtable();
            Hashtable htOrgID = new Hashtable();

            foreach (Infragistics.WebUI.UltraWebGrid.UltraGridRow row in array)
            {
                string RecType = row.Cells.FromKey("RECType").ToString();
                string FromStorageNo = row.Cells.FromKey("FromStorageID").ToString();
                string ToStorageNo = row.Cells.FromKey("ToStorageID").ToString();
                string OrgID = row.Cells.FromKey("Check").ToString();

                if (!htRecType.ContainsKey(RecType))
                {
                    htRecType.Add(RecType, "");
                }

                if (!htFromStorageNo.ContainsKey(FromStorageNo))
                {
                    htFromStorageNo.Add(FromStorageNo, "");
                }

                if (!htToStorageNo.ContainsKey(ToStorageNo))
                {
                    htToStorageNo.Add(ToStorageNo, "");
                }

                if (!htOrgID.ContainsKey(OrgID))
                {
                    htOrgID.Add(OrgID, "");
                }
            }

            if (htRecType.Count != 1)
            {
                WebInfoPublish.PublishInfo(this, "$Error_RecType_Different", this.languageComponent1);
                return;
            }

            if (htFromStorageNo.Count != 1)
            {
                WebInfoPublish.PublishInfo(this, "$Error_FromStorageNo_Different", this.languageComponent1);
                return;
            }

            if (htToStorageNo.Count != 1)
            {
                WebInfoPublish.PublishInfo(this, "$Error_ToStorageNo_Different", this.languageComponent1);
                return;
            }
            if (htOrgID.Count != 1)
            {
                WebInfoPublish.PublishInfo(this, "$Error_OrgID_Different", this.languageComponent1);
                return;
            }
            if (_TransferFacade == null)
            {
                _TransferFacade = new WarehouseFacade(this.DataProvider);
            }

            try
            {
                this.DataProvider.BeginTransaction();
                //将选择的几个移转单的Detail信息按料号Sum（PLANQTY）重新计算数量后放在该新生成的移转单下。

                Hashtable detailHT = new Hashtable();
                decimal totalQty = 0;
                foreach (Infragistics.WebUI.UltraWebGrid.UltraGridRow row in array)
                {
                    string TransferNO = row.Cells.FromKey("TransferNO").ToString();
                    decimal qty = _TransferFacade.QueryTransferQty(TransferNO);
                    totalQty += qty;

                    object[] objs = _TransferFacade.GetInvTransferDetailByTransferNO(TransferNO);

                    if (objs != null)
                    {

                        foreach (InvTransferDetail tranferDetail in objs)
                        {
                            if (!detailHT.ContainsKey(tranferDetail.ItemCode))
                            {
                                detailHT.Add(tranferDetail.ItemCode, tranferDetail.Planqty);
                            }
                            else
                            {
                                detailHT[tranferDetail.ItemCode] = decimal.Parse(detailHT[tranferDetail.ItemCode].ToString()) + tranferDetail.Planqty;
                            }
                        }
                    }
                }

                //C：按规则 MESXXXXXXX（MES + 7位数字）生成新的移转单号（可以利用TblSerialBook表）
                string newTransNo = this.CreateNewTransNo();

                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                InvTransfer invTransfer = new InvTransfer();
                invTransfer.TransferNO = newTransNo;
                invTransfer.TransferStatus = RecordStatus.RecordStatus_NEW;
                invTransfer.CreateDate = dbDateTime.DBDate;
                invTransfer.CreateTime = dbDateTime.DBTime;
                invTransfer.CreateUser = this.GetUserCode();
                invTransfer.FromStorageID = (array[0] as Infragistics.WebUI.UltraWebGrid.UltraGridRow).Cells.FromKey("FromStorageID").Value.ToString();
                invTransfer.ToStorageID = (array[0] as Infragistics.WebUI.UltraWebGrid.UltraGridRow).Cells.FromKey("ToStorageID").Value.ToString();
                invTransfer.Rectype = (array[0] as Infragistics.WebUI.UltraWebGrid.UltraGridRow).Cells.FromKey("RECType").Value.ToString();
                invTransfer.Memo = "";
                invTransfer.Mdate = dbDateTime.DBDate;
                invTransfer.Mtime = dbDateTime.DBTime;
                invTransfer.Muser = this.GetUserCode();
                invTransfer.OrgID = int.Parse((array[0] as Infragistics.WebUI.UltraWebGrid.UltraGridRow).Cells.FromKey("OrganizationID").Value.ToString());
                _TransferFacade.AddInvTransfer(invTransfer);

                int i = 1;
                foreach (DictionaryEntry de in detailHT)
                {
                    //ORDERNO，ORDERLINE，MEMO，MOCODE，CustomerCode，CUSTOMERNAME都为空，ACTQTY=0
                    InvTransferDetail detail = new InvTransferDetail();
                    detail.TransferNO = newTransNo;
                    detail.TransferLine = i++;
                    detail.OrderNO = "";
                    detail.OrderLine = 0;
                    detail.Memo = "";
                    detail.MOCode = "";
                    detail.CustomerCode = "";
                    detail.CustomerName = "";
                    detail.Actqty = 0;
                    detail.Muser = this.GetUserCode();
                    detail.Mdate = dbDateTime.DBDate;
                    detail.Mtime = dbDateTime.DBTime;
                    detail.TransferDate = dbDateTime.DBDate;
                    detail.TransferTime = dbDateTime.DBTime;
                    detail.TransferUser = this.GetUserCode();
                    detail.TransferStatus = RecordStatus.RecordStatus_NEW;
                    detail.ItemCode = de.Key.ToString();
                    detail.Planqty = decimal.Parse(de.Value.ToString());

                    _TransferFacade.AddInvTransferDetail(detail);
                }

                //D：将关联关系插入table：TBLInvTransferMerge
                foreach (Infragistics.WebUI.UltraWebGrid.UltraGridRow row in array)
                {
                    InvTransferMerge merge = new InvTransferMerge();
                    merge.Transferno = newTransNo;
                    //merge.Frmtransferno = (array[0] as Infragistics.WebUI.UltraWebGrid.UltraGridRow).Cells.FromKey("FromStorageID").ToString();
                    //modify by li 2011.1.19
                    merge.Frmtransferno = row.Cells.FromKey("TransferNO").ToString();
                    merge.Muser = this.GetUserCode();
                    merge.Mdate = dbDateTime.DBDate;
                    merge.Mtime = dbDateTime.DBTime;

                    _TransferFacade.AddInvTransferMerge(merge);
                }

                this.DataProvider.CommitTransaction();

                cmdQuery_ServerClick(null, null);

                WebInfoPublish.PublishInfo(this, string.Format("$Success_Merge_Transfer {0}", newTransNo), this.languageComponent1);

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
            }


        }

        protected void cmdSave_ServerClick(object sender, EventArgs e)
        {
            ViewState["operate"] = "Edit";
            if (this.ValidateInput())
            {
                if (_TransferFacade == null)
                {
                    _TransferFacade = new WarehouseFacade(this.DataProvider);
                }

                object obj = this.GetEditObject();

                if (obj != null)
                {
                    if (((InvTransfer)obj).FromStorageID == ((InvTransfer)obj).ToStorageID)
                    {
                        WebInfoPublish.Publish(this, "$Error_TransferStorageSame", this.languageComponent1);
                        return;
                    }
                    if (((InvTransfer)obj).TransferStatus != RecordStatus.RecordStatus_NEW)
                    {
                        WebInfoPublish.Publish(this, "$Error_OnlyNEWCanEdit", this.languageComponent1);
                        return;
                    }
                    if (String.IsNullOrEmpty(((InvTransfer)obj).OrgID.ToString()))
                    {
                        InventoryFacade inventoryFacade = new InventoryFacade(base.DataProvider);
                        Storage storage = (Storage)inventoryFacade.GetStorageByStorageCode(((InvTransfer)obj).FromStorageID);
                        if (storage != null)
                        {
                            ((InvTransfer)obj).OrgID = storage.OrgID;
                        }
                    }

                    _TransferFacade.UpdateInvTransfer((InvTransfer)obj);
                }
            }

            //this.cmdSave.Disabled = true;
            //this.cmdAdd.Disabled = false;
            this.txtTransferNoEdit.Enabled = true;

            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
        }

        protected void cmdAdd_ServerClick(object sender, EventArgs e)
        {

            ViewState["operate"] = "Add";
            if (this.ValidateInput())
            {
                if (_TransferFacade == null)
                {
                    _TransferFacade = new WarehouseFacade(this.DataProvider);
                }

                object obj = this.GetEditObject();

                if (obj != null)
                {
                    if (((InvTransfer)obj).FromStorageID == ((InvTransfer)obj).ToStorageID)
                    {
                        WebInfoPublish.Publish(this, "$Error_TransferStorageSame", this.languageComponent1);
                        return;
                    }
                    //add前检验唯一性
                    object hasData = _TransferFacade.GetInvTransfer(((InvTransfer)obj).TransferNO);
                    if (hasData != null)
                    {
                        WebInfoPublish.Publish(this, "$Error_TransferHasData", this.languageComponent1);
                        return;
                    }

                    ((InvTransfer)obj).TransferStatus = RecordStatus.RecordStatus_NEW;
                    ((InvTransfer)obj).CreateDate = ((InvTransfer)obj).Mdate;
                    ((InvTransfer)obj).CreateTime = ((InvTransfer)obj).Mtime;
                    ((InvTransfer)obj).CreateUser = ((InvTransfer)obj).Muser;

                    //根据源库别从表Tblstorage中取得OrgID
                    InventoryFacade inventoryFacade = new InventoryFacade(base.DataProvider);
                    Storage storage = (Storage)inventoryFacade.GetStorageByStorageCode(((InvTransfer)obj).FromStorageID);
                    if (storage != null)
                    {
                        ((InvTransfer)obj).OrgID = storage.OrgID;
                    }

                    _TransferFacade.AddInvTransfer((InvTransfer)obj);

                    this.txtTransferNO.Text = ((InvTransfer)obj).TransferNO;
                }
            }
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
        }


        protected void cmdCancel_ServerClick(object sender, EventArgs e)
        {
            //this.cmdSave.Disabled = true;
            //this.cmdAdd.Disabled = false;
            this.txtTransferNoEdit.Enabled = true;
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }

        protected void cmdDelete_ServerClick(object sender, EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            ArrayList transferList = new ArrayList(array.Count);
            ArrayList transferDetailList = new ArrayList(array.Count);

            if (array.Count > 0)
            {
                foreach (GridRecord row in array)
                {
                    object obj = this.GetEditObject(row);
                    if (obj != null)
                    {
                        if (((InvTransfer)obj).TransferStatus == RecordStatus.RecordStatus_CLOSE || ((InvTransfer)obj).TransferStatus == RecordStatus.RecordStatus_USING)
                        {
                            WebInfoPublish.Publish(this, "$Error_TransferCannotDelete", this.languageComponent1);
                            return;
                        }

                        object[] detailobjs = this._TransferFacade.GetInvTransferDetailByTransferNO(((InvTransfer)obj).TransferNO);
                        if (detailobjs != null)
                        {
                            foreach (object item in detailobjs)
                            {
                                transferDetailList.Add((InvTransferDetail)item);
                            }
                        }

                    }
                    transferList.Add((InvTransfer)obj);
                }
            }

            this._TransferFacade.DeleteInvTransferDetail(((InvTransferDetail[])transferDetailList.ToArray(typeof(InvTransferDetail))));
            this._TransferFacade.DeleteInvTransfer(((InvTransfer[])transferList.ToArray(typeof(InvTransfer))));

            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
        }

        protected void cmdQuery_ServerClick(object sender, EventArgs e)
        {
            if (ValidateQueryInput())
            {
                RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
            }
        }

        protected void cmdProduct_ServerClick(object sender, EventArgs e)
        {
            //C：按规则 MESXXXXXXX（MES + 7位数字）生成新的移转单号（可以利用TblSerialBook表）
            string newTransNo = this.CreateNewTransNo();

            if (!string.IsNullOrEmpty(newTransNo))
            {
                this.txtTransferNoEdit.Text = newTransNo;
            }
            else
            {
                WebInfoPublish.Publish(this, "$Error_TransNo_Ctreate", this.languageComponent1);
            }
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblTransferNoEdit, this.txtTransferNoEdit, 40, true));
            manager.Add(new LengthCheck(this.lblFromStorageIDEdit, this.txtFromStorageIDQueryEdit, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }
            _InventoryFacade = new InventoryFacade(this.DataProvider);

            object obj = _InventoryFacade.GetStorage(
                    (GlobalVariables.CurrentOrganizations.First()).OrganizationID,
                    FormatHelper.CleanString(this.txtFromStorageIDQueryEdit.Text, 40));
            if (obj == null)
            {

                WebInfoPublish.Publish(this, "$BS_FromStorage_IS_NOT_Exist", languageComponent1);
                return false;
            }
            if (FormatHelper.CleanString(this.txtToStorageIDQueryEdit.Text, 40) != "")
            {
                obj = _InventoryFacade.GetStorage((GlobalVariables.CurrentOrganizations.First()).OrganizationID,
                    FormatHelper.CleanString(this.txtToStorageIDQueryEdit.Text, 40));
                if (obj == null)
                {

                    WebInfoPublish.Publish(this, "$BS_ToStorage_IS_NOT_Exist", languageComponent1);
                    return false;
                }
            }

            return true;
        }

        private bool ValidateQueryInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new DateCheck(this.lblCreateDateFromQuery, this.dateCreateDateFromQuery.Text, false));
            manager.Add(new DateCheck(this.lblCreateDateToQuery, this.dateCreateDateToQuery.Text, false));

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
                    if (((InvTransfer)obj).TransferStatus != RecordStatus.RecordStatus_NEW)
                    {
                        WebInfoPublish.Publish(this, "$Error_OnlyNEWCanEdit", this.languageComponent1);
                        return;
                    }

                    this.cmdSave.Disabled = false;
                    this.cmdAdd.Disabled = true;
                    this.txtTransferNoEdit.Enabled = false;
                    this.SetEditObject(obj);

                }
            }

            if (commandName == "TransferDetail")
            {
                Response.Redirect(this.MakeRedirectUrl("FInvTransferDetailMP.aspx", new string[] { "TransferNO", "RECType", "TransferStatus" }, new string[] { row.Items.FindItemByKey("TransferNO").Value.ToString(), row.Items.FindItemByKey("RECType").Value.ToString(), row.Items.FindItemByKey("RECTypeDesc").Value.ToString() }));
            }
        }




        #endregion

        #region For Page Load

        private void InitParameters()
        {
            if (this.Request.Params["TransferNO"] == null)
            {
                this.ViewState["TransferNO"] = string.Empty;
            }
            else
            {
                this.ViewState["TransferNO"] = this.Request.Params["TransferNO"];
            }

            this.txtTransferNO.Text = this.ViewState["TransferNO"].ToString();
        }

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);

        }

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("TransferNO", "单号", null);
            this.gridHelper.AddColumn("RECType", "单据类型", null);
            this.gridHelper.AddColumn("RECTypeDesc", "单据类型", null);
            this.gridHelper.AddColumn("TransferStatus", "单据状态", null);
            this.gridHelper.AddColumn("FromStorageID", "源库别", null);
            this.gridHelper.AddColumn("ToStorageID", "目的库别", null);
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddColumn("CreateUser", "创建人", null);
            this.gridHelper.AddColumn("CreateDate", "创建日期", null);
            //this.gridHelper.AddColumn("OrganizationDesc", "组织名称", null);
            this.gridHelper.AddColumn("OrganizationID", "组织代码", null);
            this.gridHelper.AddColumn("OrganizationDesc", "组织名称", null);
            this.gridHelper.AddLinkColumn("TransferDetail", "详细", null);

            this.gridWebGrid.Columns.FromKey("OrganizationID").Hidden = true;
            this.gridWebGrid.Columns.FromKey("RECType").Hidden = true;
            // 2005-04-06
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        public void InitButtonHelp()
        {
            //this.buttonHelper.AddDeleteConfirm();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

        }

        protected void DrpRECType_Load()
        {
            this.DrpRECType.Items.Clear();
            this.DrpRECType.Items.Add(new ListItem("", ""));
            this.DrpRECType.Items.Add(new ListItem(this.languageComponent1.GetString(RecordType.RecordType_MO), RecordType.RecordType_MO));
            this.DrpRECType.Items.Add(new ListItem(this.languageComponent1.GetString(RecordType.RecordType_DB), RecordType.RecordType_DB));
            this.DrpRECType.Items.Add(new ListItem(this.languageComponent1.GetString(RecordType.RecordType_WX), RecordType.RecordType_WX));
            this.DrpRECType.Items.Add(new ListItem(this.languageComponent1.GetString(RecordType.RecordType_SO), RecordType.RecordType_SO));
            this.DrpRECType.Items.Add(new ListItem(this.languageComponent1.GetString(RecordType.RecordType_OT), RecordType.RecordType_OT));
            this.DrpRECType.Items.Add(new ListItem(this.languageComponent1.GetString(RecordType.RecordType_QT), RecordType.RecordType_QT));

        }

        protected void drpTransferStatus_Load()
        {
            this.drpTransferStatus.Items.Clear();
            this.drpTransferStatus.Items.Add(new ListItem("", ""));
            this.drpTransferStatus.Items.Add(new ListItem(this.languageComponent1.GetString(RecordStatus.RecordStatus_NEW), RecordStatus.RecordStatus_NEW));
            this.drpTransferStatus.Items.Add(new ListItem(this.languageComponent1.GetString(RecordStatus.RecordStatus_USING), RecordStatus.RecordStatus_USING));
            this.drpTransferStatus.Items.Add(new ListItem(this.languageComponent1.GetString(RecordStatus.RecordStatus_CLOSE), RecordStatus.RecordStatus_CLOSE));
        }

        protected void DrpRECTypeEdit_Load()
        {
            this.DrpRECTypeEdit.Items.Clear();
            this.DrpRECTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(RecordType.RecordType_MO), RecordType.RecordType_MO));
            this.DrpRECTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(RecordType.RecordType_DB), RecordType.RecordType_DB));
            this.DrpRECTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(RecordType.RecordType_WX), RecordType.RecordType_WX));
            this.DrpRECTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(RecordType.RecordType_SO), RecordType.RecordType_SO));
            this.DrpRECTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(RecordType.RecordType_OT), RecordType.RecordType_OT));
            this.DrpRECTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(RecordType.RecordType_QT), RecordType.RecordType_QT));
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
            if (_TransferFacade == null) { _TransferFacade = new WarehouseFacade(this.DataProvider); }

            return this._TransferFacade.QueryTransfer(
                FormatHelper.CleanString(this.txtTransferNO.Text.Trim().ToUpper()),
                FormatHelper.CleanString(this.txtItemIDQuery.Text.Trim().ToUpper()),
                FormatHelper.CleanString(this.DrpRECType.SelectedValue.Trim()),
                FormatHelper.CleanString(this.drpTransferStatus.SelectedValue.Trim()),
                FormatHelper.TODateInt(this.dateCreateDateFromQuery.Text),
                FormatHelper.TODateInt(this.dateCreateDateToQuery.Text),
                FormatHelper.CleanString(this.txtFromStorageIDQuery.Text.Trim().ToUpper()),
                FormatHelper.CleanString(this.txtToStorageIDQuery.Text.Trim().ToUpper()),
                inclusive, exclusive);
        }

        private int GetRowCount()
        {
            if (_TransferFacade == null) { _TransferFacade = new WarehouseFacade(this.DataProvider); }


            return this._TransferFacade.QueryTransferCount(
                FormatHelper.CleanString(this.txtTransferNO.Text.Trim().ToUpper()),
                FormatHelper.CleanString(this.txtItemIDQuery.Text.Trim().ToUpper()),
                FormatHelper.CleanString(this.DrpRECType.SelectedValue.Trim()),
                FormatHelper.CleanString(this.drpTransferStatus.SelectedValue.Trim()),
                FormatHelper.TODateInt(this.dateCreateDateFromQuery.Text),
                FormatHelper.TODateInt(this.dateCreateDateToQuery.Text),
                FormatHelper.CleanString(this.txtFromStorageIDQuery.Text.Trim().ToUpper()),
                FormatHelper.CleanString(this.txtToStorageIDQuery.Text.Trim().ToUpper()));
        }

        #endregion

        #region For Grid And Edit

        protected DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["TransferNO"] = ((InvTransfer)obj).TransferNO.ToString();
            row["RECType"] = ((InvTransfer)obj).Rectype.ToString();
            row["RECTypeDesc"] = this.languageComponent1.GetString(((InvTransfer)obj).Rectype.ToString());
            row["TransferStatus"] = this.languageComponent1.GetString(((InvTransfer)obj).TransferStatus.ToString());
            row["FromStorageID"] = ((InvTransfer)obj).FromStorageID.ToString();
            row["ToStorageID"] = ((InvTransfer)obj).ToStorageID.ToString();
            row["Memo"] = ((InvTransfer)obj).Memo.ToString();
            row["CreateUser"] = ((InvTransfer)obj).CreateUser.ToString();
            row["CreateDate"] = FormatHelper.ToDateString(((InvTransfer)obj).CreateDate);
            //FormatHelper.GetFieldWithDesc("tblInvTransfer";"ORGID";"tblorg";"ORGDESC");
            row["OrganizationID"] = ((InvTransfer)obj).OrgID.ToString();
            row["OrganizationDesc"] = baseFacade.GetOrgDesc(((InvTransfer)obj).OrgID);
            row["TransferDetail"] = "";
            return row;
        }

        private void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtTransferNoEdit.Text = string.Empty;
                this.DrpRECTypeEdit.SelectedIndex = 0;
                this.txtFromStorageIDQueryEdit.Text = string.Empty;
                this.txtToStorageIDQueryEdit.Text = string.Empty;
                this.txtRemark.Text = string.Empty;


            }
            else
            {
                this.txtTransferNoEdit.Text = ((InvTransfer)obj).TransferNO.ToString().Trim();
                this.DrpRECTypeEdit.Text = ((InvTransfer)obj).Rectype.ToString();
                this.txtFromStorageIDQueryEdit.Text = ((InvTransfer)obj).FromStorageID.ToString();
                this.txtToStorageIDQueryEdit.Text = ((InvTransfer)obj).ToStorageID.ToString();
                this.txtRemark.Text = ((InvTransfer)obj).Memo.ToString();

            }
        }

        protected object GetEditObject()
        {
            if (_TransferFacade == null)
            {
                _TransferFacade = new WarehouseFacade(this.DataProvider);
            }
            DBDateTime DBDateTimeNow = FormatHelper.GetNowDBDateTime(this.DataProvider);
            InvTransfer transfer = new InvTransfer();
            if (ViewState["operate"].ToString() == "Edit")
            {
                transfer = (InvTransfer)this._TransferFacade.GetInvTransfer(FormatHelper.CleanString(this.txtTransferNoEdit.Text));
                if (transfer == null)
                {
                    return null;
                }
            }
            transfer.TransferNO = FormatHelper.CleanString(this.txtTransferNoEdit.Text.Trim().ToUpper(), 40);
            transfer.Rectype = FormatHelper.CleanString(this.DrpRECTypeEdit.SelectedValue);
            transfer.FromStorageID = FormatHelper.CleanString(this.txtFromStorageIDQueryEdit.Text.Trim().ToUpper(), 40);
            transfer.ToStorageID = FormatHelper.CleanString(this.txtToStorageIDQueryEdit.Text.Trim().ToUpper(), 40);
            transfer.Memo = FormatHelper.CleanString(this.txtRemark.Text, 2000);

            transfer.Mdate = DBDateTimeNow.DBDate;
            transfer.Mtime = DBDateTimeNow.DBTime;
            transfer.Muser = this.GetUserCode();

            return transfer;
        }

        private object GetEditObject(GridRecord row)
        {
            if (_TransferFacade == null)
            {
                _TransferFacade = new WarehouseFacade(this.DataProvider);
            }
            object obj = _TransferFacade.GetInvTransfer(row.Items.FindItemByKey("TransferNO").Text.Trim().ToUpper());

            if (obj != null)
            {
                return (InvTransfer)obj;
            }
            return null;
        }

        #endregion

        #region For Export To Excel

        protected void cmdGridExport_ServerClick(object sender, EventArgs e)
        {
            if (baseFacade == null)
            {
                baseFacade = new BaseModelFacade(this.DataProvider);
            }
            this.excelExporter.Export();
        }

        private string[] FormatExportRecord(object obj)
        {
            return new string[]{
                                ((InvTransfer)obj).TransferNO.ToString(),
                                this.languageComponent1.GetString(((InvTransfer)obj).Rectype.ToString()),
			                    this.languageComponent1.GetString(((InvTransfer)obj).TransferStatus.ToString()),
                    			((InvTransfer)obj).FromStorageID.ToString(),
                    			((InvTransfer)obj).ToStorageID.ToString(),
                                //((InvTransfer)obj).OrgID.ToString(),
                                baseFacade.GetOrgDesc(((InvTransfer)obj).OrgID),
                                ((InvTransfer)obj).Memo.ToString(),
                    			((InvTransfer)obj).CreateUser.ToString(),
                                ((InvTransfer)obj).CreateDate.ToString(),

                                
            };
        }


        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "TransferNO",
                "RECType",
                "TransferStatus",
                "FromStorageID",
                "ToStorageID",
                "OrgDesc",
                "Memo",
                "CreateUser",
                "CreateDate",
                
            };
        }

        #endregion
    }
}



