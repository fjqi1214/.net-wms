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
    public partial class FSendMetrial : Form
    {
        #region  变量

        private UltraWinGridHelper _UltraWinGridHelper1 = null;
        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;
        private DataTable _DataTableLoadedPart = new DataTable();


        #endregion

        #region 属性

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _DomainDataProvider;
            }
        }

        #endregion

        public FSendMetrial()
        {
            InitializeComponent();
        }

        private void FSendMetrial_Load(object sender, EventArgs e)
        {
            InitializeUltraGrid();
            this.LoadPrinter();
            this.BindStockInStorage();
            this.opsLoadMetrial.Value = "0";
            this.opsCheckFIFO.Value = "0";
            this.InitDateTime();
            this.chkAll.Checked = true;
            ApplicationRun.GetInfoForm().AddEx("$CS_CMPleaseInputMO");
        }

        private void BindStockInStorage()
        {
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            ucLabelComboxStorageIn.ComboBoxData.Items.Clear();
            //产线对应的库房都是没有在参数表中管理的
            //参考SQL:select PARAMCODE as storage from tblsysparam where PARAMGROUPCODE='DESTSTORAGEMANAGE'
            object[] objs = inventoryFacade.GetAllNoManageStorage();
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

        private void InitDateTime()
        {
            this.ucDateTimeEnd.Value = DateTime.Today;
            this.ucDateTimeStart.Value = DateTime.Today;
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

        #region 页面事件

        private void edtMoCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string moCode = FormatHelper.CleanString(this.edtMoCode.Value.Trim().ToUpper());
                if (moCode == string.Empty)
                {
                    this.edtMoCode.TextFocus(true, true);
                    return;
                }

                MOFacade moFacade = new MOFacade(this.DataProvider);
                Domain.MOModel.MO mo = (Domain.MOModel.MO)moFacade.GetMO(moCode);

                if (mo == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist $CS_Param_MOCode:" + moCode));
                    this.edtMoCode.TextFocus(false, true);
                    return;
                }

                this.edtitemDesc.Value = mo.MaterialDescription.Trim();
                this.edtMoPlanQty.Value = Convert.ToString(Convert.ToInt32(mo.MOPlanQty - mo.MOActualQty));
                this.edtMoPlanQty.TextFocus(false, true);
            }

        }

        private void edtMoPlanQty_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!this.CheckSendMetrialNumber())
                {
                    this.edtMoPlanQty.TextFocus(false, true);
                    return;
                }

                if (opsLoadMetrial.Value == "0")
                {
                    this.edtWorkSeat.TextFocus(false, true);
                    return;
                }

                if (this.opsLoadMetrial.Value == "1")
                {
                    this.edtMetrialLotNo.TextFocus(false, true);
                    return;
                }
            }
        }

        private void edtWorkSeat_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.edtWorkSeat.Value.Trim() == string.Empty)
                {
                    this.edtWorkSeat.TextFocus(false, true);
                    return;
                }

                this.edtIQCNo.TextFocus(false, true);
            }
        }

        private void edtIQCNo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                _DataTableLoadedPart.Clear();
                if (this.edtMoCode.Value.Trim() == string.Empty)
                {
                    ApplicationRun.GetInfoForm().AddEx("$CS_CMPleaseInputMO");
                    this.edtMoCode.TextFocus(false, true);
                    return;
                }

                if (this.edtWorkSeat.Value.Trim() == string.Empty)
                {
                    ApplicationRun.GetInfoForm().AddEx("$Please_Input_WorkSeat");
                    this.edtWorkSeat.TextFocus(false, true);
                    return;
                }

                if (this.edtIQCNo.Value.Trim() == string.Empty)
                {
                    this.edtIQCNo.TextFocus(false, true);
                    return;
                }

                InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
                object[] MaterialLotDetial = inventoryFacade.QueryMaterialLotAndItemDesc(FormatHelper.CleanString(this.edtMoCode.Value.Trim().ToUpper()),
                                                                                         FormatHelper.CleanString(this.edtWorkSeat.Value.Trim().ToUpper()),
                                                                                         FormatHelper.CleanString(this.edtIQCNo.Value.Trim().ToUpper()));
                if (MaterialLotDetial == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Not_Find_Metrial"));
                    this.edtIQCNo.TextFocus(false, true);
                    return;
                }

                if (!this.CheckSendMetrialNumber())
                {
                    this.edtMoPlanQty.TextFocus(false, true);
                    return;
                }

                this.LoadGrid(MaterialLotDetial);

            }
        }

        private void edtMetrialLotNo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.edtMoCode.Value.Trim() == string.Empty)
                {
                    ApplicationRun.GetInfoForm().AddEx("$CS_CMPleaseInputMO");
                    this.edtMoCode.TextFocus(false, true);
                    return;
                }

                if (!this.CheckSendMetrialNumber())
                {
                    this.edtMoPlanQty.TextFocus(false, true);
                    return;
                }

                if (this.edtMetrialLotNo.Value.Trim() == string.Empty)
                {
                    ApplicationRun.GetInfoForm().AddEx("$Please_Input_MaterialLot");
                    this.edtMetrialLotNo.TextFocus(false, true);
                    return;
                }

                InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
                object materialLot = inventoryFacade.GetMaterialLot(this.edtMetrialLotNo.Value.Trim().ToUpper());
                if (materialLot==null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Not_Find_Metrial"));
                    this.edtMetrialLotNo.TextFocus(false, true);
                    return;
                }

                MaterialLotWithItemDesc materialLotWithItemDesc = (MaterialLotWithItemDesc)inventoryFacade.GetMaterialLotAndItemDesc(FormatHelper.CleanString(this.edtMoCode.Value.Trim().ToUpper()), this.edtMetrialLotNo.Value.Trim().ToUpper());

                if (materialLotWithItemDesc == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$MaterialLot_NOMAP_MO"));
                    this.edtMetrialLotNo.TextFocus(false, true);
                    return;
                }

                if (materialLotWithItemDesc.LotQty == 0)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MetrialLot_Is_Empty"));
                    this.edtMetrialLotNo.TextFocus(false, true);
                    return;
                }

                //检查物料是否存在Sbom
                SBOMFacade sboMFacade = new SBOMFacade(this.DataProvider);
                object[] sBOMList = sboMFacade.QuerySBOMByMoCode(this.edtMoCode.Value.Trim().ToUpper());

                if (sBOMList==null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SBOMInMO_IsNot_Exist"));
                    this.edtMetrialLotNo.TextFocus(false, true);
                    return;
                }

                bool materailIsInSBOM = false;
                for (int i = 0; i < sBOMList.Length; i++)
                {
                    if (((SBOM)sBOMList[i]).SBOMItemCode.Trim().ToUpper()==materialLotWithItemDesc.ItemCode.Trim().ToUpper())
                    {
                        materailIsInSBOM = true;
                        break;
                    }
                }

                if (!materailIsInSBOM)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Material_Not_In_SBOM"));
                    this.edtMetrialLotNo.TextFocus(false, true);
                    return;
                }
                //end

                //检查是否需要再加载到Grid上
                bool dateHaveExist = false;

                for (int i = 0; i < ultraGridMetrialDetial.Rows.Count; i++)
                {
                    if (ultraGridMetrialDetial.Rows[i].Cells["MetrialLot"].Value.ToString().ToUpper() == materialLotWithItemDesc.MaterialLotNo.ToUpper())
                    {
                        dateHaveExist = true;
                        break;
                    }
                }

                if (!dateHaveExist)
                {
                    this.LoadGrid(new object[] { materialLotWithItemDesc });
                }
                //end

                this.edtMetrialLotNo.TextFocus(false, true);
            }

        }

        private void edtBufferDate_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!this.CheckBufferDate())
                {
                    this.edtBufferDate.TextFocus(false, true);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _DataTableLoadedPart.Clear();
            if (this.opsLoadMetrial.Value == "0")
            {
                this.edtIQCNo.TextFocus(false, true);
                return;
            }

            if (this.opsLoadMetrial.Value == "1")
            {
                this.edtMetrialLotNo.TextFocus(false, true);
                return;
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

        private void btnSendMetrial_Click(object sender, EventArgs e)
        {
            ultraGridMetrialDetial.Rows.Band.SortedColumns.Add("ReceiveDate", false);

            this.edtHeadText.TextFocus(false, true);
            if (this.edtMoCode.Value.Trim() == string.Empty)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_CMPleaseInputMO"));
                this.edtMoCode.TextFocus(false, true);
                return;
            }
            //ucLabelComboxStorageIn
            if (FormatString(ucLabelComboxStorageIn.SelectedItemValue).Trim() == string.Empty)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_CMPleaseInput_Receive_Place"));
                ucLabelComboxStorageIn.Focus();
                return;
            }

            if (!this.CheckSendMetrialNumber())
            {
                this.edtMoPlanQty.TextFocus(false, true);
                return;
            }

            if (!this.CheckGridChecked())
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Please_Take_OneMaterial"));
                return;
            }

            if (!this.CheckBufferDate())
            {
                this.edtBufferDate.TextFocus(false, true);
                return;
            }

            if (!CheckGridSendNumber())
            {
                return;
            }

            //检查收获地点没有被管理，因为如果是被管理的库别，就是库存转移了。工单发料目的是将料发到产线上
            //因此收获地点一定是没有被管理的
            //tblsysparam.PARAMCODE维护的是需要管理的库别对应tblstorage.STORAGECODE
            //对应的tblsysparamgroup.PARAMGROUPCODE="DESTSTORAGEMANAGE"
            string receiveStorage = FormatString(ucLabelComboxStorageIn.SelectedItemValue).ToUpper();
            SystemSettingFacade systemFacade = new SystemSettingFacade(this.DataProvider);
            object para = systemFacade.GetParameter(receiveStorage, "DESTSTORAGEMANAGE");
            bool isManageToStorage = false;
            if (para != null)
            {
                isManageToStorage = true;
            }
            if (isManageToStorage==true)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$INVALID_STORGE"));
                return;
            }

            List<string> lotNoList = new List<string>();
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            bool saveData = true;


            try
            {
                this.DataProvider.BeginTransaction();

                for (int i = 0; i < ultraGridMetrialDetial.Rows.Count; i++)
                {
                    string stockOutStorage = ultraGridMetrialDetial.Rows[i].Cells["SorageID"].Value.ToString().Trim().ToUpper();
                    string stockInStorage = FormatString(ucLabelComboxStorageIn.SelectedItemValue).ToUpper();

                    int lotSendNumber = ultraGridMetrialDetial.Rows[i].Cells["SendQty"].Value.ToString().Trim() == string.Empty ? 0 : Convert.ToInt32(ultraGridMetrialDetial.Rows[i].Cells["SendQty"].Value.ToString().Trim());
                    if (ultraGridMetrialDetial.Rows[i].Cells["Check"].Value.ToString().ToLower() == "true" && lotSendNumber > 0)
                    {

                        MaterialLot materialLot = (MaterialLot)inventoryFacade.GetMaterialLot(ultraGridMetrialDetial.Rows[i].Cells["MetrialLot"].Value.ToString().Trim().ToUpper());
                        if (materialLot != null && materialLot.FIFOFlag == "Y")
                        {
                            if (!CheckFiFo(lotNoList, ultraGridMetrialDetial.Rows[i].Cells["ItemCode"].Value.ToString(),
                                ultraGridMetrialDetial.Rows[i].Cells["VendorCode"].Value.ToString(),
                                Convert.ToInt32(ultraGridMetrialDetial.Rows[i].Cells["ReceiveDate"].Value), stockOutStorage))
                            {
                                this.DataProvider.RollbackTransaction();
                                saveData = false;
                                break;
                            }
                        }

                        //更新TBLMaterialLot.lotqty                       

                        if (materialLot != null)
                        {
                            if (materialLot.LotQty - Convert.ToInt32(ultraGridMetrialDetial.Rows[i].Cells["SendQty"].Value) < 0)
                            {
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SendMaterialLot_Not_Enough"));
                                this.DataProvider.RollbackTransaction();
                                saveData = false;
                                break;
                            }
                            materialLot.LotQty = materialLot.LotQty - Convert.ToInt32(ultraGridMetrialDetial.Rows[i].Cells["SendQty"].Value);
                            materialLot.MaintainDate = dBDateTime.DBDate;
                            materialLot.MaintainTime = dBDateTime.DBTime;

                            inventoryFacade.UpdateMaterialLot(materialLot);
                        }

                        //tblmateriallot.STORAGEID <=> tblstorage.STORAGECODE Ex:2001-0002
                        //tblsapmaterialtrans.FRMSTORAGEID <=> tblstorage.SAPSTORAGE  Ex:0002
                        //tblsapmaterialtrans.TOSTORAGEID  <=> tblstorage.SAPSTORAGE   Ex:0002
                        
                        string stockOutSAPStorage = " ";
                        string stockInSAPStorage = " ";

                        object storageStockIn = inventoryFacade.GetStorageByStorageCode(stockInStorage);
                        if (storageStockIn != null)
                        {
                            stockInSAPStorage = ((Storage)storageStockIn).SAPStorage;
                        }

                        object storageStockOut = inventoryFacade.GetStorageByStorageCode(stockOutStorage);
                        if (storageStockOut != null)
                        {
                            stockOutSAPStorage = ((Storage)storageStockOut).SAPStorage;
                        }

                        //插入出库TBLMaterialTrans
                        MaterialTrans materialTransStockOut = inventoryFacade.CreateNewMaterialTrans();
                        materialTransStockOut.Serial = 0;
                        materialTransStockOut.FRMaterialLot  = ultraGridMetrialDetial.Rows[i].Cells["MetrialLot"].Value.ToString().Trim().ToUpper();
                        materialTransStockOut.FRMITEMCODE = ultraGridMetrialDetial.Rows[i].Cells["ItemCode"].Value.ToString().Trim().ToUpper();
                        materialTransStockOut.FRMStorageID = stockOutStorage;
                        materialTransStockOut.TOMaterialLot = ultraGridMetrialDetial.Rows[i].Cells["MetrialLot"].Value.ToString().Trim().ToUpper();
                        materialTransStockOut.TOITEMCODE = ultraGridMetrialDetial.Rows[i].Cells["ItemCode"].Value.ToString().Trim().ToUpper();
                        materialTransStockOut.TOStorageID = stockInStorage;
                        materialTransStockOut.TransQTY = Convert.ToInt32(ultraGridMetrialDetial.Rows[i].Cells["SendQty"].Value);
                        materialTransStockOut.Memo = FormatHelper.CleanString(this.edtHeadText.Value.Trim()); 
                        materialTransStockOut.UNIT = ultraGridMetrialDetial.Rows[i].Cells["Unit"].Value.ToString().Trim();
                        materialTransStockOut.VendorCode = ultraGridMetrialDetial.Rows[i].Cells["VendorCode"].Value.ToString().Trim().ToUpper();
                        materialTransStockOut.IssueType = IssueType.IssueType_Issue;
                        materialTransStockOut.TRANSACTIONCODE = FormatHelper.CleanString(this.edtMoCode.Value.Trim()).ToUpper();  
                        materialTransStockOut.BusinessCode = "MO";
                        materialTransStockOut.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                        materialTransStockOut.MaintainUser = ApplicationService.Current().UserCode;
                        materialTransStockOut.MaintainDate = dBDateTime.DBDate;
                        materialTransStockOut.MaintainTime = dBDateTime.DBTime;

                        inventoryFacade.AddMaterialTrans(materialTransStockOut);

                        //插入TBLSAPMaterialTrans
                        Domain.Material.SAPMaterialTrans sapMaterialTrans = new BenQGuru.eMES.Domain.Material.SAPMaterialTrans();

                        sapMaterialTrans.MaterialLotNo = ultraGridMetrialDetial.Rows[i].Cells["MetrialLot"].Value.ToString().Trim().ToUpper();
                        sapMaterialTrans.PostSeq = inventoryFacade.GetSAPMaterialTransMaxSeq(sapMaterialTrans.MaterialLotNo);
                        sapMaterialTrans.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                        sapMaterialTrans.ItemCode = ultraGridMetrialDetial.Rows[i].Cells["ItemCode"].Value.ToString().Trim().ToUpper();
                        sapMaterialTrans.AccountDate = FormatHelper.TODateInt(this.ucDateTimeStart.Value);
                        sapMaterialTrans.VoucherDate = FormatHelper.TODateInt(this.ucDateTimeEnd.Value);
                        sapMaterialTrans.FRMStorageID = stockOutSAPStorage;
                        sapMaterialTrans.TOStorageID = stockInSAPStorage;
                        sapMaterialTrans.TransQTY = Convert.ToInt32(ultraGridMetrialDetial.Rows[i].Cells["SendQty"].Value);
                        sapMaterialTrans.ReceiveMemo = FormatHelper.CleanString(this.edtHeadText.Value.Trim());
                        sapMaterialTrans.Unit = ultraGridMetrialDetial.Rows[i].Cells["Unit"].Value.ToString().Trim();
                        //当非即售时Vendor为空
                        if (radioButtonSaleDelay.Checked == true)
                        {
                            sapMaterialTrans.VendorCode = " ";
                        }
                        else
                        {
                            sapMaterialTrans.VendorCode = ultraGridMetrialDetial.Rows[i].Cells["VendorCode"].Value.ToString().Trim().ToUpper();
                        }
                        sapMaterialTrans.MoCode = FormatHelper.CleanString(this.edtMoCode.Value.Trim().ToUpper());
                        sapMaterialTrans.Flag = "MES";
                        sapMaterialTrans.TransactionCode = " ";
                        sapMaterialTrans.SAPCode = "411";
                        sapMaterialTrans.ToItemCode = ultraGridMetrialDetial.Rows[i].Cells["ItemCode"].Value.ToString().Trim().ToUpper();
                        sapMaterialTrans.MaintainUser = ApplicationService.Current().UserCode;
                        sapMaterialTrans.MaintainDate = dBDateTime.DBDate;
                        sapMaterialTrans.MaintainTime = dBDateTime.DBTime;

                        inventoryFacade.AddSAPMaterialTrans(sapMaterialTrans);

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
                _DataTableLoadedPart.Clear();
            }


        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string printNum = this.edtPrint.Value.Trim();

                if (this.ucLabelComboxPrinter.ComboBoxData.Items.Count == 0)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_PleaseInstallPrinter"));
                    return;
                }

                if (printNum == string.Empty)
                {
                    this.edtPrint.TextFocus(false, true);
                    return;
                }

                if (Convert.ToInt32(printNum) <= 0)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Error_PrintNumber_Must_Over_Zero"));
                    return;
                }

                SetPrintButtonStatus(false);

                CodeSoftPrintFacade codeSoftPrintFacade = new CodeSoftPrintFacade(this.DataProvider);

                string printer = this.ucLabelComboxPrinter.SelectedItemValue.ToString();

                List<string> materialLotList = new List<string>();

                for (int i = 0; i < ultraGridMetrialDetial.Rows.Count; i++)
                {
                    if (ultraGridMetrialDetial.Rows[i].Cells["Check"].Value.ToString().ToLower() == "true")
                    {
                        materialLotList.Add(ultraGridMetrialDetial.Rows[i].Cells["MetrialLot"].Value.ToString());
                    }
                }

                if (!CheckPrintCondition(printer, materialLotList))
                    return;

                if (MessageBox.Show(UserControl.MutiLanguages.ParserMessage("$CS_YES_OR_NO_Print"), this.Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Messages msg = new Messages();
                    for (int j = 0; j < Convert.ToInt32(printNum); j++)
                    {
                        msg = codeSoftPrintFacade.PrintMaterialLot(printer, materialLotList);
                    }

                    if (msg.IsSuccess())
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$Success_Print_Label"));
                    }
                    else
                    {
                        ApplicationRun.GetInfoForm().Add(msg);
                    }
                }

            }
            catch (Exception ex)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                return;
            }
            finally
            {
                SetPrintButtonStatus(true);
            }
        }

        private void edtMoCode_InnerTextChanged(object sender, EventArgs e)
        {
            this.ClearValues();
        }

        private void opsLoadMetrial_ValueChanged(object sender, EventArgs e)
        {
            if (this.opsLoadMetrial.Value == "0")
            {
                this.edtWorkSeat.Value = string.Empty;
                this.edtIQCNo.Value = string.Empty;
                this.edtMetrialLotNo.Value = string.Empty;

                this.edtWorkSeat.Enabled = true;
                this.edtIQCNo.Enabled = true;
                this.edtMetrialLotNo.Enabled = false;

                ApplicationRun.GetInfoForm().AddEx("$Please_Input_WorkSeat");
                this.edtWorkSeat.TextFocus(false, true);
            }

            if (this.opsLoadMetrial.Value == "1")
            {
                this.edtWorkSeat.Value = string.Empty;
                this.edtIQCNo.Value = string.Empty;
                this.edtMetrialLotNo.Value = string.Empty;

                this.edtWorkSeat.Enabled = false;
                this.edtIQCNo.Enabled = false;
                this.edtMetrialLotNo.Enabled = true;

                ApplicationRun.GetInfoForm().AddEx("$Please_Input_MaterialLot");
                this.edtMetrialLotNo.TextFocus(true, true);
            }

        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ultraGridMetrialDetial.Rows.Count; i++)
            {
                ultraGridMetrialDetial.Rows[i].Cells["Check"].Value = this.chkAll.Checked;
            }
        }

        private void opsCheckFIFO_ValueChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region 自定义方法


        //打印检查
        private bool CheckPrintCondition(string printer, List<string> materialLot)
        {
            //打印物料不能为空 
            if (materialLot == null || materialLot.Count <= 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_PrintTemplate_Empty"));
                return false;
            }

            //打印机
            if (printer == null || printer.Trim().Length <= 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_Printer_Empty"));
                return false;
            }

            return true;
        }

        //打印时状态变化
        private void SetPrintButtonStatus(bool enabled)
        {
            this.btnPrint.Enabled = enabled;

            if (enabled)
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
            else
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            }
        }

        //请空页面控件数据
        private void ClearValues()
        {
            this.edtitemDesc.Value = string.Empty;
            this.edtMoPlanQty.Value = string.Empty;
            this.edtWorkSeat.Value = string.Empty;
            this.edtMetrialLotNo.Value = string.Empty;
            this.edtIQCNo.Value = string.Empty;
            this.edtHeadText.Value = string.Empty;
            ucLabelComboxStorageIn.SelectedIndex = -1;
            this.edtBufferDate.Value = "0";
            this.edtPrint.Value = "1";
            _DataTableLoadedPart.Clear();
            this.InitDateTime();
        }

        //检查发货套数
        private bool CheckSendMetrialNumber()
        {
            string inputNumber = FormatHelper.CleanString(this.edtMoPlanQty.Value.Trim().ToUpper());

            if (inputNumber == string.Empty)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Send_MetrialQty_Must_Exist"));
                return false;
            }

            if (int.Parse(inputNumber) <= 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Send_MetrialQty_Must_Bigger_Zero"));
                return false;
            }

            return true;
        }

        private void LoadGrid(object[] dataObjects)
        {
            if (this.opsLoadMetrial.Value == "0")
            {
                _DataTableLoadedPart.Clear();
            }

            int needBatchNumber = Convert.ToInt32(this.edtMoPlanQty.Value.Trim());
            int needNumber = 0;
            int[] lotNeedNumberList = new int[dataObjects.Length];

            //计算每行发料数量
            for (int i = 0; i < dataObjects.Length; i++)
            {
                int lotSendNumber = 0;
                MaterialLotWithItemDesc materialLotWithItemDesc = (MaterialLotWithItemDesc)dataObjects[i];

                string lastItemCode = string.Empty;
                if (i > 0)
                {
                    MaterialLotWithItemDesc materialLotWithItemDescLast = (MaterialLotWithItemDesc)dataObjects[i - 1];
                    lastItemCode = materialLotWithItemDescLast.ItemCode;
                }
                if (lastItemCode != materialLotWithItemDesc.ItemCode)
                {
                    SBOMFacade SBOMFacade = new SBOMFacade(this.DataProvider);
                    decimal sBomItemQty = SBOMFacade.GetSbomItemQtyWithMo(materialLotWithItemDesc.ItemCode, FormatHelper.CleanString(this.edtMoCode.Value.Trim().ToUpper()));
                    needNumber = Convert.ToInt32(Math.Ceiling(needBatchNumber * sBomItemQty));
                }

                lotSendNumber = Math.Min(materialLotWithItemDesc.LotQty, needNumber);
                lotNeedNumberList[i] = lotSendNumber;
                needNumber -= lotSendNumber;
            }


            for (int i = 0; i < dataObjects.Length; i++)
            {
                Domain.Material.MaterialLotWithItemDesc materialLotWithItemDesc = (Domain.Material.MaterialLotWithItemDesc)dataObjects[i];

                _DataTableLoadedPart.Rows.Add(new object[]{
                    true,
                    materialLotWithItemDesc.MaterialLotNo,
                    materialLotWithItemDesc.IQCNo,
                    materialLotWithItemDesc.STLine,
                    materialLotWithItemDesc.VendorCode,
                    materialLotWithItemDesc.VendorDesc,
                    materialLotWithItemDesc.ItemCode,
                    materialLotWithItemDesc.ItemDesc,
                    materialLotWithItemDesc.CreateDate,
                    materialLotWithItemDesc.LotQty,
                    lotNeedNumberList[i],
                    materialLotWithItemDesc.OrganizationID,
                    materialLotWithItemDesc.StorageID,
                    materialLotWithItemDesc.Unit});
            }            
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

        //检查FIFO
        private bool CheckFiFo(List<string> lotNoList, string itemCode, string vendorCode, int createDate,string storageId)
        {
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
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

            object[] queryObjects = inventoryFacade.QueryMaterialLot(itemCode, vendorCode, bufferDate, lotNoString, storageId);
            if (queryObjects != null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ItemCode:" + itemCode + "$CS_Have_Ever_MaterialLot:" + ((Domain.Material.MaterialLot)queryObjects[0]).MaterialLotNo));
                return false;
            }

            return true;
        }

        //检查Grid发货数量
        private bool CheckGridSendNumber()
        {
            Dictionary<string, int> sendQtyByItem = new Dictionary<string, int>();

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

                    if (!sendQtyByItem.ContainsKey(itemCode))
                    {
                        sendQtyByItem.Add(itemCode, 0);
                    }
                    sendQtyByItem[itemCode] += lotQty;
                }
            }

            SBOMFacade SBOMFacade = new SBOMFacade(this.DataProvider);

            foreach (KeyValuePair<string, int> par in sendQtyByItem)
            {
                decimal SbomItemQty = SBOMFacade.GetSbomItemQtyWithMo(par.Key, FormatHelper.CleanString(this.edtMoCode.Value.Trim().ToUpper()));
                int planQty = Convert.ToInt32(Math.Ceiling(SbomItemQty * Convert.ToInt32(this.edtMoPlanQty.Value.Trim())));

                if (par.Value > planQty)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_OneItem_SendNumber:" + par.Value + "$CS_Not_Over_PlanQty:" + planQty + " $CS_ItemCode:" + par.Key));
                    return false;
                }
            }

            return true;
        }

        //初始化打印选择项
        private void LoadPrinter()
        {
            this.ucLabelComboxPrinter.Clear();

            // Check Printers
            if (System.Drawing.Printing.PrinterSettings.InstalledPrinters == null ||
                System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_PleaseInstallPrinter"));
                return;
            }
            // End Added

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

        #endregion


    }
}