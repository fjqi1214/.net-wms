using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.CodeSoftPrint;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using UserControl;


using Infragistics.Win.UltraWinGrid;
using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.Client
{
    public partial class FMaterialStockIn : Form
    {
        public FMaterialStockIn()
        {
            InitializeComponent();
        }
        #region  变量
        private UltraWinGridHelper _UltraWinGridHelper1 = null;
        private IDomainDataProvider m_DataProvider = null;
        private DataTable _DataTable = new DataTable();
        private InventoryFacade m_InvFacade = null;
        private WarehouseFacade m_WHFacade = null;
        private ItemFacade m_ItemNoFacade = null;
        Messages msg = new Messages();
        #endregion

        #region 属性
        #region DataProvider
        public IDomainDataProvider DataProvider
        {
            get
            {
                if (m_DataProvider == null)
                {
                    m_DataProvider = ApplicationService.Current().DataProvider;
                }
                return m_DataProvider;
            }
        }
        #endregion

        #region InventoryFacade

        public InventoryFacade InvFacade
        {
            get
            {
                if (m_InvFacade == null)
                {
                    m_InvFacade = new InventoryFacade(this.DataProvider);
                }
                return m_InvFacade;
            }
        }
        #endregion

        #region WarehouseFacade

        public WarehouseFacade WHFacade
        {
            get
            {
                if (m_WHFacade == null)
                {
                    m_WHFacade = new WarehouseFacade(this.DataProvider);
                }
                return m_WHFacade;
            }
        }

        #endregion

        #region ItemFacade

        public ItemFacade ItemNoFacade
        {
            get
            {
                if (m_ItemNoFacade == null)
                {
                    m_ItemNoFacade = new ItemFacade(this.DataProvider);
                }
                return m_ItemNoFacade;
            }
        }

        #endregion
        #endregion

        #region 初始化Grid
        private void InitUltraGridUI(UltraGrid ultraGrid)
        {
            ultraGrid.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGrid.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGrid.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGrid.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGrid.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGrid.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGrid.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGrid.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGrid.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void InitializeUltraGrid()
        {
            InitUltraGridUI(this.ultraGridStockIn);

            _DataTable.Columns.Add("Check", typeof(bool));
            _DataTable.Columns.Add("MaterialLot", typeof(string));
            _DataTable.Columns.Add("IQCNO", typeof(string));
            _DataTable.Columns.Add("IQCLine", typeof(int));
            _DataTable.Columns.Add("VendorCode", typeof(string));
            _DataTable.Columns.Add("VendorDesc", typeof(string));//供应商描述
            _DataTable.Columns.Add("ItemCode", typeof(string));
            _DataTable.Columns.Add("ItemCodeDesc", typeof(string));
            _DataTable.Columns.Add("ReceiveDate", typeof(int));
            _DataTable.Columns.Add("StockInQty", typeof(int));
            _DataTable.Columns.Add("StockInStorage", typeof(string));
            _DataTable.Columns.Add("FifoChcek", typeof(string));
            _DataTable.Columns.Add("FrmMemo", typeof(string));

            _DataTable.Columns["Check"].ReadOnly = false;
            _DataTable.Columns["MaterialLot"].ReadOnly = true;
            _DataTable.Columns["IQCNO"].ReadOnly = true;
            _DataTable.Columns["IQCLine"].ReadOnly = true;
            _DataTable.Columns["VendorCode"].ReadOnly = true;
            _DataTable.Columns["VendorDesc"].ReadOnly = true; ;//供应商描述
            _DataTable.Columns["ItemCode"].ReadOnly = true;
            _DataTable.Columns["ItemCodeDesc"].ReadOnly = true;
            _DataTable.Columns["ReceiveDate"].ReadOnly = true;
            _DataTable.Columns["StockInQty"].ReadOnly = true;
            _DataTable.Columns["StockInStorage"].ReadOnly = true;
            _DataTable.Columns["FifoChcek"].ReadOnly = true;
            _DataTable.Columns["FrmMemo"].ReadOnly = true;

            this.ultraGridStockIn.DataSource = this._DataTable;

            _DataTable.Clear();

            ultraGridStockIn.DisplayLayout.Bands[0].Columns["Check"].Width = 16;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["MaterialLot"].Width = 128;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["IQCNO"].Width = 60;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["IQCLine"].Width = 50;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["VendorCode"].Width = 48;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["VendorDesc"].Width = 68;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["ItemCode"].Width = 60;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["ItemCodeDesc"].Width = 73;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["ReceiveDate"].Width = 59;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["StockInQty"].Width = 59;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["StockInQty"].Width = 59;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["FifoChcek"].Width = 60;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["FrmMemo"].Width = 120;

            ultraGridStockIn.DisplayLayout.Bands[0].Columns["Check"].CellActivation = Activation.AllowEdit;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["MaterialLot"].CellActivation = Activation.NoEdit;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["IQCNO"].CellActivation = Activation.NoEdit;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["IQCLine"].CellActivation = Activation.NoEdit;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["VendorCode"].CellActivation = Activation.NoEdit;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["VendorDesc"].CellActivation = Activation.NoEdit;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["ItemCode"].CellActivation = Activation.NoEdit;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["ItemCodeDesc"].CellActivation = Activation.NoEdit;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["ReceiveDate"].CellActivation = Activation.NoEdit;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["StockInQty"].CellActivation = Activation.NoEdit;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["StockInStorage"].CellActivation = Activation.NoEdit;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["FifoChcek"].CellActivation = Activation.NoEdit;
            ultraGridStockIn.DisplayLayout.Bands[0].Columns["FrmMemo"].CellActivation = Activation.NoEdit;

            ultraGridStockIn.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
        }

        private void ultraGridStockIn_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            _UltraWinGridHelper1 = new UltraWinGridHelper(this.ultraGridStockIn);

            _UltraWinGridHelper1.AddCheckColumn("Check", "");
            _UltraWinGridHelper1.AddReadOnlyColumn("MaterialLot", "物料批号");
            _UltraWinGridHelper1.AddReadOnlyColumn("IQCNO", "单据号");
            _UltraWinGridHelper1.AddReadOnlyColumn("IQCLine", "单据行号");
            _UltraWinGridHelper1.AddReadOnlyColumn("VendorCode", "供应商");
            _UltraWinGridHelper1.AddReadOnlyColumn("VendorDesc", "供应商描述");
            _UltraWinGridHelper1.AddReadOnlyColumn("ItemCode", "物料代码");
            _UltraWinGridHelper1.AddReadOnlyColumn("ItemCodeDesc", "物料描述");
            _UltraWinGridHelper1.AddReadOnlyColumn("ReceiveDate", "收料日期");
            _UltraWinGridHelper1.AddReadOnlyColumn("StockInQty", "入库数量");
            _UltraWinGridHelper1.AddReadOnlyColumn("StockInStorage", "入库库别");
            _UltraWinGridHelper1.AddCommonColumn("FifoChcek", "FIFO检查");
            _UltraWinGridHelper1.AddCommonColumn("FrmMemo", "备注");
        }
        #endregion

        private void FMaterialStockIn_Load(object sender, EventArgs e)
        {
            this.InitializeUltraGrid();
            this.InitDateTime();
            this.LoadPrinter();
            this.BindStockInStorage();
            this.BindStockOutStorage();
            this.BindBusinessCode();
            this.InitOptionArea();
        }

        private void InitDateTime()
        {
            this.ucDateStockIn.Value = DateTime.Today;
            this.ucDateAccount.Value = DateTime.Today;
            this.ucDateVoucher.Value = DateTime.Today;
        }

        protected void ShowMessage(string message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        protected void ShowMessage(Exception e)
        {
            ApplicationRun.GetInfoForm().Add(new UserControl.Message(e));
        }

        protected void ShowMessage(Messages messages)
        {
            ApplicationRun.GetInfoForm().Add(messages);
        }

        protected void ShowMessage(UserControl.Message message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        private void LoadPrinter()
        {
            this.ucLabelComboxPrinter.Clear();

            if (System.Drawing.Printing.PrinterSettings.InstalledPrinters == null ||
                System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count == 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_PleaseInstallPrinter"));
                return;
            }

            int defaultprinter = 0;
            for (int i = 0; i < System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count; i++)
            {
                this.ucLabelComboxPrinter.AddItem(System.Drawing.Printing.PrinterSettings.InstalledPrinters[i], System.Drawing.Printing.PrinterSettings.InstalledPrinters[i]);
                System.Drawing.Printing.PrinterSettings pts = new System.Drawing.Printing.PrinterSettings();
                pts.PrinterName = System.Drawing.Printing.PrinterSettings.InstalledPrinters[i];
                if (pts.IsDefaultPrinter)
                {
                    defaultprinter = i;
                }
            }
            this.ucLabelComboxPrinter.SelectedIndex = defaultprinter;
        }

        private void BindStockInStorage()
        {
            ucLabelComboxStorageIn.ComboBoxData.Items.Clear();
            object[] objs = this.InvFacade.GetAllStorage();
            if (objs != null)
            {
                foreach (object obj in objs)
                {
                    Storage storage = (Storage)obj;
                    if (storage != null)
                    {
                        ucLabelComboxStorageIn.AddItem(storage.StorageName, storage.StorageCode);
                    }
                }
            }
        }

        private void BindStockOutStorage()
        {
            ucLabelComboxStorageOut.ComboBoxData.Items.Clear();
            object[] objs = this.InvFacade.GetAllStorage();
            if (objs != null)
            {
                foreach (object obj in objs)
                {
                    Storage storage = (Storage)obj;
                    if (storage != null)
                    {
                        ucLabelComboxStorageOut.AddItem(storage.StorageName, storage.StorageCode);
                    }
                }
            }
        }

        private void BindBusinessCode()
        {
            ucLabelComboxBusinessCode.ComboBoxData.Items.Clear();
            object[] objs = this.WHFacade.QueryMaterialBusiness(BussinessType.type_in);

            if (objs != null)
            {
                foreach (object obj in objs)
                {
                    MaterialBusiness materialBusiness = (MaterialBusiness)obj;
                    if (materialBusiness != null)
                    {
                        ucLabelComboxBusinessCode.AddItem(materialBusiness.BusinessDesc, materialBusiness.BusinessCode);
                    }
                }
            }
        }

        private void ucLabelComboxBusinessCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取当前业务代码信息
            string sapCode = string.Empty;
            if (ucLabelComboxBusinessCode.SelectedItemValue != null)
            {
                string businessCode = ucLabelComboxBusinessCode.SelectedItemValue.ToString();
                object materialBusiness = this.WHFacade.GetMaterialBusiness(businessCode);

                if (materialBusiness != null)
                {
                    sapCode = ((MaterialBusiness)materialBusiness).SAPCODE;
                }
            }

            if (sapCode.Trim().Length > 0)
            {
                EnableControl(true);
            }
            else
            {
                InitOptionArea();
            }
        }

        private void EnableControl(bool value)
        {
            ucDateAccount.Enabled = value;
            ucLabelComboxStorageOut.Enabled = value;
            ucDateVoucher.Enabled = value;
            radioButtonSaleImm.Enabled = value;
            radioButtonSaleDelay.Enabled = value;
        }

        private void InitOptionArea()
        {
            EnableControl(false);
            this.ucDateAccount.Value = DateTime.Today;
            this.ucDateVoucher.Value = DateTime.Today;
            ucLabelComboxStorageOut.SelectedIndex = -1;
            radioButtonSaleDelay.Checked = true;
            radioButtonSaleImm.Checked = false;
        }

        private void bntStockIn_Click(object sender, EventArgs e)
        {
            try
            {

                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                string itemDesc = string.Empty;
                string vendorDesc = string.Empty;
                bool result = ValidateInput(out itemDesc, out vendorDesc);
                if (result == false)
                {
                    return;
                }
                string businesscode = FormatString(ucLabelComboxBusinessCode.SelectedItemValue);
                int qty = int.Parse(ucLabelEditQty.Value);

                //入库库别
                string fromSAPStorageID = string.Empty;
                string toSAPStorageID = string.Empty;
                int orgId = 0;
                object storageStockIn = this.InvFacade.GetStorageByStorageCode(FormatString(ucLabelComboxStorageIn.SelectedItemValue));
                if (storageStockIn != null)
                {
                    orgId = ((Storage)storageStockIn).OrgID;
                    toSAPStorageID = ((Storage)storageStockIn).SAPStorage;
                }

                object storageStockOut = this.InvFacade.GetStorageByStorageCode(FormatString(ucLabelComboxStorageOut.SelectedItemValue));
                if (storageStockOut != null)
                {
                    fromSAPStorageID = ((Storage)storageStockOut).SAPStorage;
                }

                //获取当前业务代码信息
                MaterialBusiness materialBusiness = (MaterialBusiness)this.WHFacade.GetMaterialBusiness(businesscode);
                string iqcno = FormatString(this.ucLabelEditDoc.Value).ToUpper();
                if (iqcno=="")
                {
                    iqcno = " ";
                }
                //Config MaterialLot
                MaterialLot materialLot = this.InvFacade.CreateNewMaterialLot();
                materialLot.IQCNo = iqcno;
                materialLot.STLine = FormatInt(this.ucLabelEditDocLine.Value);
                materialLot.ItemCode = FormatString(this.ucLEItemCode.Value);
                materialLot.VendorCode = FormatString(this.ucLabelEditVendor.Value);
                materialLot.OrganizationID = orgId;
                materialLot.StorageID = FormatString(ucLabelComboxStorageIn.SelectedItemValue);
                materialLot.Unit = FormatString(this.ucLabelEditUnit.Value);
                materialLot.CreateDate = FormatHelper.TODateInt(ucDateStockIn.Value);
                materialLot.LotInQty = qty;
                materialLot.LotQty = qty;
                materialLot.FIFOFlag = materialBusiness.ISFIFO;
                materialLot.MaintainUser = ApplicationService.Current().UserCode;
                materialLot.MaintainDate = dbDateTime.DBDate;
                materialLot.MaintainTime = dbDateTime.DBTime;

                //Set MaterialLotNo
                string dateCode = this.InvFacade.GetMaterialLotDateCode(materialLot.CreateDate);
                string runningNumber = this.InvFacade.GetNewMaterialLotRunningNumber(materialLot.VendorCode, materialLot.ItemCode, materialLot.CreateDate);
                materialLot.MaterialLotNo = materialLot.VendorCode + "-" + materialLot.ItemCode + "-" + dateCode + "-" + runningNumber;

                //Config MaterialTrans
                MaterialTrans materialTrans = this.InvFacade.CreateNewMaterialTrans();
                materialTrans.Serial = 0;
                materialTrans.FRMaterialLot = " ";
                materialTrans.FRMITEMCODE = " ";
                materialTrans.FRMStorageID = " ";
                materialTrans.TOMaterialLot = materialLot.MaterialLotNo;
                materialTrans.TOITEMCODE = materialLot.ItemCode;
                materialTrans.TOStorageID = materialLot.StorageID;
                materialTrans.TransQTY = materialLot.LotInQty;
                materialTrans.Memo = FormatString(this.ucLabelEditMemo.Value);
                materialTrans.UNIT = materialLot.Unit;
                materialTrans.VendorCode = materialLot.VendorCode;
                materialTrans.IssueType = IssueType.IssueType_Receive;
                materialTrans.TRANSACTIONCODE = materialLot.IQCNo;
                materialTrans.BusinessCode = materialBusiness.BusinessCode;
                materialTrans.OrganizationID = materialLot.OrganizationID;
                materialTrans.MaintainUser = ApplicationService.Current().UserCode;
                materialTrans.MaintainDate = dbDateTime.DBDate;
                materialTrans.MaintainTime = dbDateTime.DBTime;

                //Config Sapmaterialtrans
                //分别获取入库库别和发货库别对应的SAP库别，在TBLsAPMaterialTrans中的StorageId存放的是SAP库别
                SAPMaterialTrans sAPMaterialTrans = null;
                if (materialBusiness.SAPCODE.Trim().Length > 0)
                {
                    sAPMaterialTrans = this.InvFacade.CreateNewSAPMaterialTrans();
                    sAPMaterialTrans.MaterialLotNo = materialLot.MaterialLotNo;
                    sAPMaterialTrans.PostSeq = this.InvFacade.GetSAPMaterialTransMaxSeq(materialLot.MaterialLotNo);
                    sAPMaterialTrans.OrganizationID = materialLot.OrganizationID;
                    sAPMaterialTrans.ItemCode = materialLot.ItemCode;
                    sAPMaterialTrans.AccountDate = FormatHelper.TODateInt(ucDateAccount.Value);
                    sAPMaterialTrans.VoucherDate = FormatHelper.TODateInt(ucDateVoucher.Value);
                    if (fromSAPStorageID == "" || fromSAPStorageID == null)
                    {
                        sAPMaterialTrans.FRMStorageID = " ";
                    }
                    else
                    {
                        sAPMaterialTrans.FRMStorageID = fromSAPStorageID;
                    }
                    if (toSAPStorageID == "" || toSAPStorageID == null)
                    {
                        sAPMaterialTrans.TOStorageID = " ";
                    }
                    else
                    {
                        sAPMaterialTrans.TOStorageID = toSAPStorageID;
                    }
                    sAPMaterialTrans.TransQTY = materialLot.LotInQty;
                    sAPMaterialTrans.ReceiveMemo = FormatString(this.ucLabelEditMemo.Value);
                    sAPMaterialTrans.Unit = materialLot.Unit;
                    //当非即售时Vendor为空
                    if (radioButtonSaleDelay.Checked == true)
                    {
                        sAPMaterialTrans.VendorCode = " ";
                    }
                    else
                    {
                        sAPMaterialTrans.VendorCode = materialLot.VendorCode;
                    }
                    sAPMaterialTrans.MoCode = materialLot.IQCNo;
                    sAPMaterialTrans.Flag = FlagStatus.FlagStatus_MES;
                    sAPMaterialTrans.TransactionCode = " ";
                    sAPMaterialTrans.ToItemCode = materialLot.ItemCode;
                    if (materialBusiness.SAPCODE == null || materialBusiness.SAPCODE == "")
                    {
                        sAPMaterialTrans.SAPCode = " ";
                    }
                    else
                    {
                        sAPMaterialTrans.SAPCode = materialBusiness.SAPCODE;
                    }
                    sAPMaterialTrans.MaintainUser = ApplicationService.Current().UserCode;
                    sAPMaterialTrans.MaintainDate = dbDateTime.DBDate;
                    sAPMaterialTrans.MaintainTime = dbDateTime.DBTime;
                }

                bool saveData = this.InvFacade.MaterialStockIn(materialLot, materialBusiness, materialTrans, sAPMaterialTrans);

                if (saveData)
                {
                    //向Grid上增加记录
                    DataRow dr = this._DataTable.NewRow();
                    dr["Check"] = false;
                    dr["MaterialLot"] = materialLot.MaterialLotNo;
                    dr["IQCNO"] = materialLot.IQCNo;
                    dr["IQCLine"] = materialLot.STLine;
                    dr["VendorCode"] = materialLot.VendorCode;
                    dr["VendorDesc"] = vendorDesc;
                    dr["ItemCode"] = materialLot.ItemCode;
                    dr["ItemCodeDesc"] = itemDesc;
                    dr["ReceiveDate"] = materialLot.CreateDate;
                    dr["StockInQty"] = materialLot.LotInQty;
                    dr["StockInStorage"] = materialLot.StorageID;
                    dr["FifoChcek"] = materialBusiness.ISFIFO;
                    dr["FrmMemo"] = FormatString(this.ucLabelEditMemo.Value).Replace("\n", " ");
                    this._DataTable.Rows.Add(dr);

                    ClearInputData();

                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_StockIn_Success"));
                }
            }
            catch (Exception ex)
            {
                Messages msg = new Messages();
                msg.Add(new UserControl.Message(ex));
                ApplicationRun.GetInfoForm().Add(msg);
            }
        }

        protected bool ValidateInput(out string itemdest, out string vendordesc)
        {
            string businesscode = FormatString(ucLabelComboxBusinessCode.SelectedItemValue);
            string unit = FormatString(this.ucLabelEditUnit.Value);
            string stockInStorage = FormatString(ucLabelComboxStorageIn.SelectedItemValue);
            string item = FormatString(this.ucLEItemCode.Value);
            string vendor = FormatString(this.ucLabelEditVendor.Value);
            string stockOutStorage = FormatString(ucLabelComboxStorageOut.SelectedItemValue);
            itemdest = "";
            vendordesc = "";
            //检查业务代码
            if (businesscode.Length == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_MATERIAL_BUSINESS_CODE_INPUT"));
                ucLabelComboxBusinessCode.Focus();
                return false;
            }
            //检查单位
            if (unit.Length == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_UNIT_INPUT"));
                this.ucLabelEditUnit.TextFocus(false, true);
                return false;
            }
            //检查入库库别
            if (stockInStorage.Length == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_STOCKIN_STORAGE_SELECT"));
                ucLabelComboxStorageIn.Focus();
                return false;
            }
            //检查数量
            if (ucLabelEditQty.Value.Length == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_QTY_INPUT"));
                this.ucLabelEditQty.TextFocus(false, true);
                return false;
            }
            int qty = int.Parse(ucLabelEditQty.Value);
            if (qty <= 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$STOCKINQTY_LARGE_ZERO"));
                this.ucLabelEditQty.TextFocus(false, true);
                return false;
            }
            //检查料号
            if (item.Length == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$Error_CS_ItemCode_Empty"));
                this.ucLEItemCode.TextFocus(false, true);
                return false;
            }

            //获取当前业务代码信息
            object materialBusiness = this.WHFacade.GetMaterialBusiness(businesscode);
            string sapCode = string.Empty;
            int orgId = 0;
            if (materialBusiness != null)
            {
                sapCode = ((MaterialBusiness)materialBusiness).SAPCODE;
                orgId = ((MaterialBusiness)materialBusiness).OrgID;
            }

            //料号有效性,OrgId取materialBusiness中的OrgId
            object itemEntity = this.ItemNoFacade.GetMaterial(item, GlobalVariables.CurrentOrganizations.First().OrganizationID);
            if (itemEntity == null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$ITEMNOTINORGID"));
                this.ucLEItemCode.TextFocus(false, true);
                return false;
            }
            itemdest = ((BenQGuru.eMES.Domain.MOModel.Material)itemEntity).MaterialDescription;
            if (vendor.Length == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_VENDOR_INPUT"));
                this.ucLabelEditVendor.TextFocus(false, true);
                return false;
            }


            //Vendor有效性
            object vendorEntity = this.ItemNoFacade.GetVender(vendor);
            if (vendorEntity == null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$Vendor_isnot_exit"));
                this.ucLabelEditVendor.TextFocus(false, true);
                return false;
            }
            vendordesc = ((BenQGuru.eMES.Domain.MOModel.Vendor)vendorEntity).VendorName;
            //当业务代码有SAPCode时，发货库别必须选择
            if (sapCode.Trim().Length > 0)
            {
                if (stockOutStorage.Length == 0)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_STOCKOUT_STORAGE_SELECT"));
                    ucLabelComboxStorageOut.Focus();
                    return false;
                }
            }
            return true;
        }
        private string FormatString(object value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return FormatHelper.CleanString(value.ToString().Trim());
            }
        }

        private int FormatInt(string value)
        {
            try
            {
                return int.Parse(value);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private void ucButtonClear_Click(object sender, EventArgs e)
        {
            ClearInputData();
            _DataTable.Clear();
            ucLabelComboxBusinessCode.Focus();
        }

        private void ClearInputData()
        {
            ucLabelComboxBusinessCode.SelectedIndex = -1;
            ucLabelEditUnit.Value = "";
            ucLabelComboxStorageIn.SelectedIndex = -1;
            ucLabelEditQty.Value = "";
            ucLabelEditDoc.Value = "";
            ucLabelEditDocLine.Value = "";
            ucLEItemCode.Value = "";
            ucLabelEditVendor.Value = "";
            ucLabelComboxStorageOut.SelectedIndex = -1;
            radioButtonSaleDelay.Checked = true;
            ucLabelEditMemo.Value = "";
            txtPrintNum.Value = "1";
            chkAll.Checked = false;
            this.InitDateTime();
        }

        private void ucButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {

                string printNum = this.txtPrintNum.Value.Trim();

                if (printNum == "" || printNum == string.Empty)
                {
                    printNum = "0";
                }
                if (Convert.ToInt32(printNum) <= 0)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_Error_PrintNumber_Must_Over_Zero"));
                    return;
                }

                if (this.ucLabelComboxPrinter.ComboBoxData.Items.Count == 0)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_PleaseInstallPrinter"));
                    return;
                }

                SetPrintButtonStatus(false);

                CodeSoftPrintFacade codeSoftPrintFacade = new CodeSoftPrintFacade(this.DataProvider);

                string printer = string.Empty;
                string templatePath = string.Empty; ; //print path

                printer = this.ucLabelComboxPrinter.SelectedItemText;


                List<string> materialLot = new List<string>();

                for (int i = 0; i < ultraGridStockIn.Rows.Count; i++)
                {
                    if (ultraGridStockIn.Rows[i].Cells[0].Value.ToString().Trim().ToUpper() == "TRUE")
                    {
                        materialLot.Add(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ultraGridStockIn.Rows[i].Cells["MaterialLot"].Value.ToString())));

                    }
                }

                if (!CheckPrintCondition(printer, materialLot))
                {
                    return;
                }


                if (MessageBox.Show(UserControl.MutiLanguages.ParserMessage("$CS_YES_OR_NO_Print"), this.Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    for (int j = 0; j < Convert.ToInt16(this.txtPrintNum.Value.Trim()); j++)
                    {

                        msg = codeSoftPrintFacade.PrintMaterialLot(printer, materialLot);
                    }

                    if (msg.IsSuccess())
                    {
                        this.ShowMessage(new UserControl.Message(MessageType.Success, "$Success_Print_Label"));
                    }
                }

                this.ShowMessage(msg);

            }
            catch (Exception ex)
            {
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                return;

            }
            finally
            {
                SetPrintButtonStatus(true);
            }
        }

        private void SetPrintButtonStatus(bool enabled)
        {
            this.ucButtonPrint.Enabled = enabled;

            if (enabled)
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
            else
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            }
        }

        private bool CheckPrintCondition(string printer, List<string> materialLot)
        {


            if (materialLot == null || materialLot.Count <= 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Message_NoMaterialLotToPrint"));
                return false;
            }
            //模板
            //if (templatePath == null || templatePath.Length <= 0 || templatePath=="")
            //{
            //    this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_PrintTemplate_Empty"));
            //    return false;
            //}

            //打印机
            if (printer == null || printer.Trim().Length <= 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_Printer_Empty"));
                return false;
            }
            return true;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                for (int i = 0; i < ultraGridStockIn.Rows.Count; i++)
                {
                    ultraGridStockIn.Rows[i].Cells[0].Value = chkAll.Checked;
                }
                ultraGridStockIn.UpdateData();
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
        }

        private void ultraGridStockIn_CellChange(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Index == 0)
            {
                ultraGridStockIn.UpdateData();
            }
        }

    }
}