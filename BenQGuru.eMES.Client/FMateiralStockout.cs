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
    public partial class FMateiralStockout : Form
    {
        #region  变量
        private UltraWinGridHelper _UltraWinGridHelper1 = null;
        private IDomainDataProvider m_DataProvider = null;
        private DataTable _DataTableLoadedPart = new DataTable();
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

        public FMateiralStockout()
        {
            InitializeComponent();
        }

        private void FMateiralStockout_Load(object sender, EventArgs e)
        {
            this.InitOptionArea();
            this.opsCheckFIFO.Value = "0";
            this.InitializeUltraGrid();
            this.InitDateTime();
            this.BindStockInStorage();
            this.BindStockOutStorage();
            this.BindBusinessCode();
        }

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
            InitUltraGridUI(this.ultraGridMetrialDetial);

            _DataTableLoadedPart.Columns.Add("Check", typeof(bool));
            _DataTableLoadedPart.Columns.Add("MetrialLot", typeof(string));
            _DataTableLoadedPart.Columns.Add("IQCNO", typeof(string));
            _DataTableLoadedPart.Columns.Add("IQCLine", typeof(int));
            _DataTableLoadedPart.Columns.Add("VendorCode", typeof(string));
            _DataTableLoadedPart.Columns.Add("VendorDesc", typeof(string));
            _DataTableLoadedPart.Columns.Add("ItemCode", typeof(string));
            _DataTableLoadedPart.Columns.Add("ItemCodeDesc", typeof(string));
            _DataTableLoadedPart.Columns.Add("ReceiveDate", typeof(int));
            _DataTableLoadedPart.Columns.Add("LotQty", typeof(int));
            _DataTableLoadedPart.Columns.Add("SendQty", typeof(int));
            _DataTableLoadedPart.Columns.Add("IQCFactory", typeof(int));
            _DataTableLoadedPart.Columns.Add("SorageID", typeof(string));
            _DataTableLoadedPart.Columns.Add("Unit", typeof(string));

            _DataTableLoadedPart.Columns["Check"].ReadOnly = false;
            _DataTableLoadedPart.Columns["MetrialLot"].ReadOnly = true;
            _DataTableLoadedPart.Columns["IQCNO"].ReadOnly = true;
            _DataTableLoadedPart.Columns["IQCLine"].ReadOnly = true;
            _DataTableLoadedPart.Columns["VendorCode"].ReadOnly = true;
            _DataTableLoadedPart.Columns["VendorDesc"].ReadOnly = true;
            _DataTableLoadedPart.Columns["ItemCode"].ReadOnly = true;
            _DataTableLoadedPart.Columns["ItemCodeDesc"].ReadOnly = true;
            _DataTableLoadedPart.Columns["ReceiveDate"].ReadOnly = true;
            _DataTableLoadedPart.Columns["LotQty"].ReadOnly = true;
            _DataTableLoadedPart.Columns["SendQty"].ReadOnly = false;
            _DataTableLoadedPart.Columns["IQCFactory"].ReadOnly = true;
            _DataTableLoadedPart.Columns["SorageID"].ReadOnly = true;
            _DataTableLoadedPart.Columns["Unit"].ReadOnly = true;

            this.ultraGridMetrialDetial.DataSource = this._DataTableLoadedPart;

            _DataTableLoadedPart.Clear();

            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["Check"].Width = 18;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["MetrialLot"].Width = 165;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["IQCNO"].Width = 120;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["IQCLine"].Width = 45;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["VendorCode"].Width = 70;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["VendorDesc"].Width = 70;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["ItemCode"].Width = 60;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["ItemCodeDesc"].Width = 60;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["ReceiveDate"].Width = 60;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["LotQty"].Width = 60;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["SendQty"].Width = 60;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["IQCFactory"].Width = 60;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["SorageID"].Width = 60;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["Unit"].Width = 50;

            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["Check"].CellActivation = Activation.AllowEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["MetrialLot"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["IQCNO"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["IQCLine"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["VendorCode"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["VendorDesc"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["ItemCode"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["ItemCodeDesc"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["ReceiveDate"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["LotQty"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["SendQty"].CellActivation = Activation.AllowEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["IQCFactory"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["SorageID"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.NoEdit;
        }

        private void ultraGridMetrialDetial_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            _UltraWinGridHelper1 = new UltraWinGridHelper(this.ultraGridMetrialDetial);

            _UltraWinGridHelper1.AddCheckColumn("Check", "");
            _UltraWinGridHelper1.AddReadOnlyColumn("MetrialLot", "物料批号");
            _UltraWinGridHelper1.AddReadOnlyColumn("IQCNO", "单据号");
            _UltraWinGridHelper1.AddReadOnlyColumn("IQCLine", "行号");
            _UltraWinGridHelper1.AddReadOnlyColumn("VendorCode", "供应商");
            _UltraWinGridHelper1.AddReadOnlyColumn("VendorDesc", "供应商描述");
            _UltraWinGridHelper1.AddReadOnlyColumn("ItemCode", "物料代码");
            _UltraWinGridHelper1.AddReadOnlyColumn("ItemCodeDesc", "物料描述");
            _UltraWinGridHelper1.AddReadOnlyColumn("ReceiveDate", "收料日期");
            _UltraWinGridHelper1.AddReadOnlyColumn("LotQty", "在库数量");
            _UltraWinGridHelper1.AddReadOnlyColumn("SendQty", "发料数量");
            _UltraWinGridHelper1.AddReadOnlyColumn("IQCFactory", "订单工厂");
            _UltraWinGridHelper1.AddReadOnlyColumn("SorageID", "发货地点");
            _UltraWinGridHelper1.AddReadOnlyColumn("Unit", "单位");
        }

        #endregion

        private void InitDateTime()
        {
            this.ucDateStockInFrom.Value = DateTime.Today;
            this.ucDateStockInTo.Value = DateTime.Today;
            this.ucDateAccount.Value = DateTime.Today;
            this.ucDateVoucher.Value = DateTime.Today;
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
                ucLabelComboxStorageOut.AddItem("", "");
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
            object[] objs = this.WHFacade.QueryMaterialBusiness(BussinessType.type_out);

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            _DataTableLoadedPart.Clear();
            this.InitDateTime();
            this.opsCheckFIFO.Value = "0";
            ucBatch.Value = "";
            ucLabelComboxStorageOut.SelectedIndex = -1;
            ucLabelEditDoc.Value = "";
            ucLEItemCode.Value = "";
            ucLabelEditVendor.Value = "";
            ucLabelComboxBusinessCode.SelectedIndex = -1;
            ucLabelEditIssueNo.Value = "";
            ucLabelEditMemo.Value = "";
            ucLabelComboxStorageIn.SelectedIndex = -1;
            radioButtonSaleDelay.Checked = true;
            ucLabelEditToItem.Value = "";
            ucLabelEditToItem.Checked = false;
            edtBufferDate.Value = "1";
        }

        private void ucBtnQuery_Click(object sender, EventArgs e)
        {
            _DataTableLoadedPart.Clear();

            string materialLotNo = FormatString(this.ucBatch.Value).ToUpper();
            string storage = FormatString(this.ucLabelComboxStorageOut.SelectedItemValue).ToUpper();
            string iqcNo = FormatString(this.ucLabelEditDoc.Value).ToUpper();
            string item = FormatString(this.ucLEItemCode.Value).ToUpper();
            int stockInDateFrom = FormatHelper.TODateInt(ucDateStockInFrom.Value);
            int stockInDateTo = FormatHelper.TODateInt(ucDateStockInTo.Value);
            string vendor = FormatString(this.ucLabelEditVendor.Value).ToUpper();

            if (stockInDateTo < stockInDateFrom)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Alert_ReceiveDate_Compare"));
                ucDateStockInFrom.Focus();
                return;
            }

            object[] MaterialLotDetial = this.InvFacade.QueryMaterialIssue(materialLotNo, storage, iqcNo, item, stockInDateFrom, stockInDateTo, vendor);
            if (MaterialLotDetial == null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Not_Find_Metrial"));
                this.ucBatch.TextFocus(false, true);
                return;
            }
            for (int i = 0; i < MaterialLotDetial.Length; i++)
            {
                Domain.Material.MaterialLotWithItemDesc materialLotWithItemDesc = (Domain.Material.MaterialLotWithItemDesc)MaterialLotDetial[i];

                _DataTableLoadedPart.Rows.Add(new object[]{
                    false,
                    materialLotWithItemDesc.MaterialLotNo,
                    materialLotWithItemDesc.IQCNo,
                    materialLotWithItemDesc.STLine,
                    materialLotWithItemDesc.VendorCode,
                    materialLotWithItemDesc.VendorDesc,
                    materialLotWithItemDesc.ItemCode,
                    materialLotWithItemDesc.ItemDesc,
                    materialLotWithItemDesc.CreateDate,
                    materialLotWithItemDesc.LotQty,
                    0,
                    materialLotWithItemDesc.OrganizationID,
                    materialLotWithItemDesc.StorageID,
                    materialLotWithItemDesc.Unit});
            }
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

        private bool CheckFiFo(List<string> lotNoList, string itemCode, string vendorCode, int createDate,string storageId)
        {
            //检查FIFO
            DateTime dateTime = Convert.ToDateTime(FormatHelper.ToDateString(createDate, "-")).AddDays(-Convert.ToInt32(this.edtBufferDate.Value.Trim()));
            int bufferDate = FormatHelper.TODateInt(dateTime.Date);

            string lotNoString = string.Empty;
            for (int i = 0; i < lotNoList.Count; i++)
            {
                lotNoString += "'" + lotNoList[i].ToString() + "',"; ;
            }

            if (lotNoString.Length > 0)
            {
                lotNoString = lotNoString.Substring(0, lotNoString.Length - 1);
            }

            if (this.opsCheckFIFO.Value.ToString() == "0")
            {
                vendorCode = string.Empty;
            }

            object[] queryObjects = this.InvFacade.QueryMaterialLot(itemCode, vendorCode, bufferDate, lotNoString, storageId);
            if (queryObjects != null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ItemCode:" + itemCode + "$CS_Have_Ever_MaterialLot:" + ((Domain.Material.MaterialLot)queryObjects[0]).MaterialLotNo));
                return false;
            }

            return true;
        }

        protected bool ValidateInput()
        {
            if (!this.CheckGridChecked())
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Please_Take_OneMaterial"));
                return false;
            }

            if (!this.CheckBufferDate())
            {
                this.edtBufferDate.TextFocus(false, true);
                return false;
            }

            if (!CheckGridSendNumber())
            {
                return false;
            }

            string businesscode = FormatString(ucLabelComboxBusinessCode.SelectedItemValue);
            string stockInStorage = FormatString(ucLabelComboxStorageIn.SelectedItemValue);
            if (businesscode.Length == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_MATERIAL_BUSINESS_CODE_INPUT"));
                ucLabelComboxBusinessCode.Focus();
                return false;
            }

            //入库库别必须选择
            if (stockInStorage.Length == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_STOCKIN_STORAGE_SELECT"));
                ucLabelComboxStorageIn.Focus();
                return false;
            }

            //料号有效性,OrgId取materialBusiness中的OrgId
            string uiToItem = FormatString(ucLabelEditToItem.Value).Trim().ToUpper();
            if (uiToItem.Trim().Length>0)
            {
                object itemEntity = this.ItemNoFacade.GetMaterial(uiToItem, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                if (itemEntity == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$ITEMNOTINORGID"));
                    this.ucLabelEditToItem.TextFocus(false, true);
                    return false;
                }
            }

            return true;
        }

        private void btnSendMetrial_Click(object sender, EventArgs e)
        {
            //UI Check
            ultraGridMetrialDetial.Rows.Band.SortedColumns.Add("ReceiveDate", false);

            if (!ValidateInput())
            {
                return;
            }

            string stockInStorage = FormatString(ucLabelComboxStorageIn.SelectedItemValue);
            string uiToItem = FormatString(ucLabelEditToItem.Value).Trim().ToUpper();

            string moCode = FormatString(this.ucLabelEditIssueNo.Value);
            if (moCode == "")
            {
                moCode = " ";
            }

            //获取业务类型
            string businesscode = FormatString(ucLabelComboxBusinessCode.SelectedItemValue);
            MaterialBusiness materialBusiness = (MaterialBusiness)this.WHFacade.GetMaterialBusiness(businesscode);
            
            //判断入库库别是否被管理
            //tblsysparam.PARAMCODE维护的是需要管理的库别对应tblstorage.STORAGECODE
            //对应的tblsysparamgroup.PARAMGROUPCODE="DESTSTORAGEMANAGE"
            SystemSettingFacade systemFacade = new SystemSettingFacade(this.DataProvider);
            object para = systemFacade.GetParameter(stockInStorage, "DESTSTORAGEMANAGE");
            bool isManageToStorage = false;
            if (para != null)
            {
                isManageToStorage = true;
            }

            List<string> lotNoList = new List<string>();
            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            bool saveData = true;

            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i < ultraGridMetrialDetial.Rows.Count; i++)
                {
                    int lotSendNumber = ultraGridMetrialDetial.Rows[i].Cells["SendQty"].Value.ToString().Trim() == string.Empty ? 0 : Convert.ToInt32(ultraGridMetrialDetial.Rows[i].Cells["SendQty"].Value.ToString().Trim());
                    if (ultraGridMetrialDetial.Rows[i].Cells["Check"].Value.ToString().ToLower() == "true" && lotSendNumber > 0)
                    {
                        MaterialLot materialLot = (MaterialLot)this.InvFacade.GetMaterialLot(ultraGridMetrialDetial.Rows[i].Cells["MetrialLot"].Value.ToString().Trim().ToUpper());
                        //只有当目标库房没有被管理时，才需要做FIFO检查，就如同工单发料的库房没有被管理，在发料的时候是需要FIFO检查的
                        if (materialLot != null && materialLot.FIFOFlag == "Y" && isManageToStorage==false)
                        {
                            if (!CheckFiFo(lotNoList, ultraGridMetrialDetial.Rows[i].Cells["ItemCode"].Value.ToString(),
                                ultraGridMetrialDetial.Rows[i].Cells["VendorCode"].Value.ToString(),
                                Convert.ToInt32(ultraGridMetrialDetial.Rows[i].Cells["ReceiveDate"].Value), materialLot.StorageID.Trim().ToUpper()))
                            {
                                this.DataProvider.RollbackTransaction();
                                saveData = false;
                                break;
                            }
                        }

                        if (materialLot != null)
                        {
                            int orgIdStockOut = 0;
                            int orgIdStockIn = 0;
                            string fromSAPStorageID = " ";
                            string toSAPStorageID = " ";
                            string materialLotNoStockIn = " ";
                            object storageStockIn = this.InvFacade.GetStorageByStorageCode(FormatString(ucLabelComboxStorageIn.SelectedItemValue));
                            if (storageStockIn != null)
                            {
                                orgIdStockIn = ((Storage)storageStockIn).OrgID;
                                toSAPStorageID = ((Storage)storageStockIn).SAPStorage;
                            }

                            object storageStockOut = this.InvFacade.GetStorageByStorageCode(materialLot.StorageID);
                            if (storageStockOut != null)
                            {
                                orgIdStockOut = ((Storage)storageStockOut).OrgID;
                                fromSAPStorageID = ((Storage)storageStockOut).SAPStorage;
                            }

                            string toItemCode = uiToItem.Trim().Length == 0 ? materialLot.ItemCode : uiToItem.Trim().ToUpper();
                            //判断是否超发
                            int sendQty=Convert.ToInt32(ultraGridMetrialDetial.Rows[i].Cells["SendQty"].Value);
                            if ((materialLot.LotQty - sendQty) < 0)
                            {
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SendMaterialLot_Not_Enough"));
                                this.DataProvider.RollbackTransaction();
                                saveData = false;
                                break;
                            }

                            if (isManageToStorage==true)
                            {
                                //当目标库管理时
                                if (materialLot.LotQty == sendQty && materialLot.ItemCode == toItemCode)
                                {
                                    materialLotNoStockIn = materialLot.MaterialLotNo;
                                    //当发料数量等于库存数量，直接修改TBLMaterialLot中的库别和ToItemCode
                                    MaterialLot materialLotStockIn = this.InvFacade.CreateNewMaterialLot();
                                    materialLotStockIn.MaterialLotNo = materialLot.MaterialLotNo;
                                    materialLotStockIn.IQCNo = materialLot.IQCNo;
                                    materialLotStockIn.STLine = materialLot.STLine;
                                    materialLotStockIn.ItemCode = materialLot.ItemCode;
                                    materialLotStockIn.VendorCode = materialLot.VendorCode;
                                    materialLotStockIn.OrganizationID = orgIdStockIn;
                                    materialLotStockIn.StorageID = stockInStorage;
                                    materialLotStockIn.Unit = materialLot.Unit;
                                    //Modified By Nettie Chen 2009/09/24
                                    //materialLotStockIn.CreateDate = dBDateTime.DBDate;
                                    materialLotStockIn.CreateDate = materialLot.CreateDate;
                                    //End Modified
                                    materialLotStockIn.LotInQty = materialLot.LotInQty;
                                    materialLotStockIn.LotQty = materialLot.LotQty;
                                    materialLotStockIn.FIFOFlag = materialLot.FIFOFlag;
                                    materialLotStockIn.MaintainUser = ApplicationService.Current().UserCode;
                                    materialLotStockIn.MaintainDate = dBDateTime.DBDate;
                                    materialLotStockIn.MaintainTime = dBDateTime.DBTime;

                                    this.InvFacade.UpdateMaterialLot(materialLotStockIn);
                                }
                                else
                                {
                                    //当发料数量不等于库存数量,或Item有变化时，产生新的Lot
                                    string dateCode = this.InvFacade.GetMaterialLotDateCode(materialLot.CreateDate);
                                    string runningNumber = this.InvFacade.GetNewMaterialLotRunningNumber(materialLot.VendorCode, toItemCode, materialLot.CreateDate);
                                    materialLotNoStockIn = materialLot.VendorCode + "-" + toItemCode + "-" + dateCode + "-" + runningNumber;

                                    //修改出库的TBLMaterialLot
                                    materialLot.LotQty = materialLot.LotQty - sendQty;
                                    materialLot.MaintainDate = dBDateTime.DBDate;
                                    materialLot.MaintainTime = dBDateTime.DBTime;
                                    this.InvFacade.UpdateMaterialLot(materialLot);

                                    //新增入库的TBLMaterialLot
                                    MaterialLot materialLotStockIn = this.InvFacade.CreateNewMaterialLot();
                                    materialLotStockIn.MaterialLotNo = materialLotNoStockIn;
                                    materialLotStockIn.IQCNo = materialLot.IQCNo;
                                    materialLotStockIn.STLine = materialLot.STLine;
                                    materialLotStockIn.ItemCode = toItemCode;
                                    materialLotStockIn.VendorCode = materialLot.VendorCode;
                                    materialLotStockIn.OrganizationID = orgIdStockIn;
                                    materialLotStockIn.StorageID = stockInStorage;
                                    materialLotStockIn.Unit = materialLot.Unit;
                                    materialLotStockIn.CreateDate = materialLot.CreateDate;
                                    materialLotStockIn.LotInQty = sendQty;
                                    materialLotStockIn.LotQty = sendQty;
                                    materialLotStockIn.FIFOFlag = materialLot.FIFOFlag;
                                    materialLotStockIn.MaintainUser = ApplicationService.Current().UserCode;
                                    materialLotStockIn.MaintainDate = dBDateTime.DBDate;
                                    materialLotStockIn.MaintainTime = dBDateTime.DBTime;
                                    this.InvFacade.AddMaterialLot(materialLotStockIn);
                                }

                                //入库的TBLMaterialTrans
                                MaterialTrans materialTransStockIn = this.InvFacade.CreateNewMaterialTrans();
                                materialTransStockIn.Serial = 0;
                                materialTransStockIn.FRMaterialLot = materialLot.MaterialLotNo;
                                materialTransStockIn.FRMITEMCODE = materialLot.ItemCode;
                                materialTransStockIn.FRMStorageID = materialLot.StorageID;
                                materialTransStockIn.TOMaterialLot = materialLotNoStockIn;
                                materialTransStockIn.TOITEMCODE = toItemCode;
                                materialTransStockIn.TOStorageID = stockInStorage;
                                materialTransStockIn.TransQTY = sendQty;
                                materialTransStockIn.Memo = FormatString(this.ucLabelEditMemo.Value);
                                materialTransStockIn.UNIT = materialLot.Unit;
                                materialTransStockIn.VendorCode = materialLot.VendorCode;
                                materialTransStockIn.IssueType = IssueType.IssueType_Receive;
                                materialTransStockIn.TRANSACTIONCODE = moCode;
                                materialTransStockIn.BusinessCode = materialBusiness.BusinessCode;
                                materialTransStockIn.OrganizationID = orgIdStockIn;
                                materialTransStockIn.MaintainUser = ApplicationService.Current().UserCode;
                                materialTransStockIn.MaintainDate = dBDateTime.DBDate;
                                materialTransStockIn.MaintainTime = dBDateTime.DBTime;
                                this.InvFacade.AddMaterialTrans(materialTransStockIn);

                            }
                            else
                            {
                                //当目标库没有管理时,只更新出库的TBLMaterialLot.lotqty  
                                materialLotNoStockIn = materialLot.MaterialLotNo;

                                materialLot.LotQty = materialLot.LotQty - sendQty;
                                materialLot.MaintainUser = ApplicationService.Current().UserCode;
                                materialLot.MaintainDate = dBDateTime.DBDate;
                                materialLot.MaintainTime = dBDateTime.DBTime;

                                this.InvFacade.UpdateMaterialLot(materialLot);
                            }

                            //出库的TBLMaterialTrans
                            MaterialTrans materialTransStockOut = this.InvFacade.CreateNewMaterialTrans();
                            materialTransStockOut.Serial = 0;
                            materialTransStockOut.FRMaterialLot = materialLot.MaterialLotNo;
                            materialTransStockOut.FRMITEMCODE = materialLot.ItemCode;
                            materialTransStockOut.FRMStorageID = materialLot.StorageID;
                            materialTransStockOut.TOMaterialLot = materialLotNoStockIn;
                            materialTransStockOut.TOITEMCODE = toItemCode;
                            materialTransStockOut.TOStorageID = stockInStorage;
                            materialTransStockOut.TransQTY = sendQty;
                            materialTransStockOut.Memo = FormatString(this.ucLabelEditMemo.Value);
                            materialTransStockOut.UNIT = materialLot.Unit;
                            materialTransStockOut.VendorCode = materialLot.VendorCode;
                            materialTransStockOut.IssueType = IssueType.IssueType_Issue;
                            materialTransStockOut.TRANSACTIONCODE = moCode;
                            materialTransStockOut.BusinessCode = materialBusiness.BusinessCode;
                            materialTransStockOut.OrganizationID = orgIdStockOut;
                            materialTransStockOut.MaintainUser = ApplicationService.Current().UserCode;
                            materialTransStockOut.MaintainDate = dBDateTime.DBDate;
                            materialTransStockOut.MaintainTime = dBDateTime.DBTime;

                            this.InvFacade.AddMaterialTrans(materialTransStockOut);

                            if (materialBusiness.SAPCODE.Trim().Length > 0)
                            {
                                SAPMaterialTrans sAPMaterialTrans = this.InvFacade.CreateNewSAPMaterialTrans();
                                sAPMaterialTrans.MaterialLotNo = materialLot.MaterialLotNo;
                                sAPMaterialTrans.PostSeq = this.InvFacade.GetSAPMaterialTransMaxSeq(materialLot.MaterialLotNo);
                                sAPMaterialTrans.OrganizationID = orgIdStockOut;
                                sAPMaterialTrans.ItemCode = materialLot.ItemCode;
                                sAPMaterialTrans.AccountDate = FormatHelper.TODateInt(ucDateAccount.Value);
                                sAPMaterialTrans.VoucherDate = FormatHelper.TODateInt(ucDateVoucher.Value);
                                sAPMaterialTrans.FRMStorageID = fromSAPStorageID;
                                sAPMaterialTrans.TOStorageID = toSAPStorageID;
                                sAPMaterialTrans.TransQTY = sendQty;
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
                                sAPMaterialTrans.MoCode = moCode;
                                sAPMaterialTrans.Flag = FlagStatus.FlagStatus_MES;
                                sAPMaterialTrans.TransactionCode = " ";
                                sAPMaterialTrans.ToItemCode = toItemCode;
                                sAPMaterialTrans.SAPCode = materialBusiness.SAPCODE;
                                sAPMaterialTrans.MaintainUser = ApplicationService.Current().UserCode;
                                sAPMaterialTrans.MaintainDate = dBDateTime.DBDate;
                                sAPMaterialTrans.MaintainTime = dBDateTime.DBTime;

                                this.InvFacade.AddSAPMaterialTrans(sAPMaterialTrans);
                            }
                            
                        }

                        lotNoList.Add(ultraGridMetrialDetial.Rows[i].Cells["MetrialLot"].Value.ToString());
                    }
                }

                if (saveData)
                {
                    this.DataProvider.CommitTransaction();

                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_SendMaterialLot_Success"));
                }
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();

                Messages msg = new Messages();
                msg.Add(new UserControl.Message(ex));
                ApplicationRun.GetInfoForm().Add(msg);
            }

            if (saveData)
            {
                //_DataTableLoadedPart.Clear();
                //ucBtnQuery_Click
                ucBtnQuery_Click(this,null);
            }
        }

        //检查Grid发货数量
        private bool CheckGridSendNumber()
        {
            for (int i = 0; i < ultraGridMetrialDetial.Rows.Count; i++)
            {
                if (ultraGridMetrialDetial.Rows[i].Cells["Check"].Value.ToString().ToLower() == "true")
                {
                    string itemCode = ultraGridMetrialDetial.Rows[i].Cells["ItemCode"].Value.ToString();
                    int lotInQty = Convert.ToInt32(ultraGridMetrialDetial.Rows[i].Cells["LotQty"].Value.ToString());
                    int lotQty = ultraGridMetrialDetial.Rows[i].Cells["SendQty"].Value.ToString().Trim() == string.Empty ? 0 : int.Parse(ultraGridMetrialDetial.Rows[i].Cells["SendQty"].Value.ToString().Trim());

                    if (lotInQty < lotQty)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LotQty_Smaller_LotInQty $CS_MaterialLot:" + ultraGridMetrialDetial.Rows[i].Cells["MetrialLot"].Value.ToString()));
                        return false;
                    }

                    if (lotQty <= 0)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LotQty_Over_Zero $CS_MaterialLot:" + ultraGridMetrialDetial.Rows[i].Cells["MetrialLot"].Value.ToString()));
                        return false;
                    }
                }
            }


            return true;
        }

        //检查是否选择数据
        private bool CheckGridChecked()
        {
            for (int i = 0; i < ultraGridMetrialDetial.Rows.Count; i++)
            {
                if (ultraGridMetrialDetial.Rows[i].Cells["Check"].Value.ToString().ToLower() == "true")
                {
                    return true;
                }
            }

            return false;
        }

        //检查缓冲日期
        private bool CheckBufferDate()
        {
            if (this.edtBufferDate.Value.Trim() == string.Empty)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_PeleaseInput_BufferDate"));
                return false;
            }

            if (Convert.ToInt32(this.edtBufferDate.Value.Trim()) < 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_BufferDate_Must_Bigger_Zero"));
                return false;
            }

            return true;
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
            ucDateVoucher.Enabled = value;
            radioButtonSaleImm.Enabled = value;
            radioButtonSaleDelay.Enabled = value;
        }

        private void InitOptionArea()
        {
            EnableControl(false);
            this.ucDateAccount.Value = DateTime.Today;
            this.ucDateVoucher.Value = DateTime.Today;
            radioButtonSaleDelay.Checked = true;
            radioButtonSaleImm.Checked = false;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ultraGridMetrialDetial.Rows.Count; i++)
            {
                ultraGridMetrialDetial.Rows[i].Cells["Check"].Value = this.chkAll.Checked;
            }
        }

        private void ultraGridMetrialDetial_CellChange(object sender, CellEventArgs e)
        {
            ultraGridMetrialDetial.UpdateData();
        }
    }
}