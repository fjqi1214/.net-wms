using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Infragistics.Win.UltraWinGrid;
using BenQGuru.eMES.Domain.Equipment;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.IQC;
using UserControl;
using BenQGuru.eMES.SMT;

namespace BenQGuru.eMES.Client
{
    public partial class FMaterialReceive : BaseForm
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

        public FMaterialReceive()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
            InitializeUltraGrid();
        }

        private void FMaterialReceive_Load(object sender, EventArgs e)
        {

            InitializedrpTypeCode();
            InitializedrpBusiness();
            InitialzedrpControlType();
            InitialzedrpStatus();
            this.ucBegDate.Value = DateTime.Now.Date;
            this.ucEndDate.Value = DateTime.Now.Date;
            this.txtReceiveCode.Focus();

            //this.InitPageLanguage();
            //this.InitGridLanguage(ultraGridScrutiny);

        }

        private int serial;

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
            InitUltraGridUI(this.ultraGridScrutiny);

            _DataTableLoadedPart.Columns.Add("Check", typeof(bool));
            _DataTableLoadedPart.Columns.Add("ReceiptNo", typeof(string));
            _DataTableLoadedPart.Columns.Add("StLine", typeof(int));
            _DataTableLoadedPart.Columns.Add("ReceiptLine", typeof(int));
            _DataTableLoadedPart.Columns.Add("Attribute_C", typeof(string));
            _DataTableLoadedPart.Columns.Add("CheckStatus_C", typeof(string));
            _DataTableLoadedPart.Columns.Add("Status", typeof(string));
            _DataTableLoadedPart.Columns.Add("ItemCode", typeof(string));
            _DataTableLoadedPart.Columns.Add("McontrolType_C", typeof(string));
            _DataTableLoadedPart.Columns.Add("Mdesc", typeof(string));
            _DataTableLoadedPart.Columns.Add("Unit", typeof(string));
            _DataTableLoadedPart.Columns.Add("VendorCode", typeof(string));
            _DataTableLoadedPart.Columns.Add("VendorName", typeof(string));
            _DataTableLoadedPart.Columns.Add("Rohs", typeof(string));
            _DataTableLoadedPart.Columns.Add("RecriveQty", typeof(decimal));
            _DataTableLoadedPart.Columns.Add("QualifyQty", typeof(decimal));
            _DataTableLoadedPart.Columns.Add("ActQty", typeof(decimal));
            _DataTableLoadedPart.Columns.Add("RecStatus", typeof(string));
            _DataTableLoadedPart.Columns.Add("Mstorage", typeof(string));
            _DataTableLoadedPart.Columns.Add("Mstack", typeof(string));
            _DataTableLoadedPart.Columns.Add("Memo", typeof(string));
            _DataTableLoadedPart.Columns.Add("Maintenance", typeof(string));
            _DataTableLoadedPart.Columns.Add("IQCStatus", typeof(string));
            _DataTableLoadedPart.Columns.Add("McontrolType", typeof(string));
            _DataTableLoadedPart.Columns.Add("Attribute", typeof(string));
            _DataTableLoadedPart.Columns.Add("CheckStatus", typeof(string));

            _DataTableLoadedPart.Columns["Check"].ReadOnly = false;
            _DataTableLoadedPart.Columns["ReceiptNo"].ReadOnly = true;
            _DataTableLoadedPart.Columns["StLine"].ReadOnly = true;
            _DataTableLoadedPart.Columns["ReceiptLine"].ReadOnly = true;
            _DataTableLoadedPart.Columns["Attribute"].ReadOnly = true;
            _DataTableLoadedPart.Columns["CheckStatus"].ReadOnly = true;
            _DataTableLoadedPart.Columns["Status"].ReadOnly = true;
            _DataTableLoadedPart.Columns["ItemCode"].ReadOnly = true;
            _DataTableLoadedPart.Columns["McontrolType_C"].ReadOnly = true;
            _DataTableLoadedPart.Columns["Mdesc"].ReadOnly = true;
            _DataTableLoadedPart.Columns["Unit"].ReadOnly = false;
            _DataTableLoadedPart.Columns["VendorCode"].ReadOnly = true;
            _DataTableLoadedPart.Columns["VendorName"].ReadOnly = true;
            _DataTableLoadedPart.Columns["Rohs"].ReadOnly = true;
            _DataTableLoadedPart.Columns["RecriveQty"].ReadOnly = true;
            _DataTableLoadedPart.Columns["QualifyQty"].ReadOnly = true;
            //_DataTableLoadedPart.Columns["ActQty"].ReadOnly = false;
            _DataTableLoadedPart.Columns["RecStatus"].ReadOnly = true;
            _DataTableLoadedPart.Columns["Mstorage"].ReadOnly = false;
            _DataTableLoadedPart.Columns["Mstack"].ReadOnly = false;
            _DataTableLoadedPart.Columns["Memo"].ReadOnly = false;
            _DataTableLoadedPart.Columns["Maintenance"].ReadOnly = true;
            _DataTableLoadedPart.Columns["IQCStatus"].ReadOnly = true;

            this.ultraGridScrutiny.DataSource = this._DataTableLoadedPart;

            _DataTableLoadedPart.Clear();
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Check"].Width = 15;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Check"].Header.CheckBoxVisibility = Infragistics.Win.UltraWinGrid.HeaderCheckBoxVisibility.WhenUsingCheckEditor;

            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["ReceiptNo"].Width = 80;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["StLine"].Width = 70;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["ReceiptLine"].Width = 66;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Attribute"].Width = 80;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["CheckStatus"].Width = 80;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Status"].Width = 70;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["ItemCode"].Width = 70;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["McontrolType_C"].Width = 85;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Mdesc"].Width = 80;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Unit"].Width = 60;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["VendorCode"].Width = 75;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["VendorName"].Width = 80;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Rohs"].Width = 40;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["RecriveQty"].Width = 60;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["QualifyQty"].Width = 60;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["ActQty"].Width = 60;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["RecStatus"].Hidden = true;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Mstorage"].Width = 80;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Mstorage"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.EditButton;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Mstack"].Width = 80;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Mstack"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.EditButton;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Memo"].Width = 80;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Maintenance"].Width = 80;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Maintenance"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["IQCStatus"].Width = 80;

            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Check"].CellActivation = Activation.AllowEdit;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["ReceiptNo"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["StLine"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["ReceiptLine"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Attribute_C"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["CheckStatus_C"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["ItemCode"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["MControlType_C"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Mdesc"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["VendorCode"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["VendorName"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Rohs"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["RecriveQty"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["QualifyQty"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["ActQty"].CellActivation = Activation.AllowEdit;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Mstorage"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Mstack"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Memo"].CellActivation = Activation.AllowEdit;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Maintenance"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["IQCStatus"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["IQCStatus"].Hidden = true;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["McontrolType"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["McontrolType"].Hidden = true;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Attribute"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Attribute"].Hidden = true;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["CheckStatus"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["CheckStatus"].Hidden = true;

        }

        private void ultraGridScrutiny_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            _UltraWinGridHelper1 = new UltraWinGridHelper(this.ultraGridScrutiny);

            _UltraWinGridHelper1.AddCheckColumn("Check", "");
            _UltraWinGridHelper1.AddReadOnlyColumn("ReceiptNo", "入库单号");
            _UltraWinGridHelper1.AddReadOnlyColumn("StLine", "行号");
            _UltraWinGridHelper1.AddReadOnlyColumn("ReceiptLine", "入库单行");
            _UltraWinGridHelper1.AddReadOnlyColumn("Attribute_C", "单据类型");
            _UltraWinGridHelper1.AddReadOnlyColumn("CheckStatus_C", "检验结果");
            _UltraWinGridHelper1.AddReadOnlyColumn("Status", "当前状态");
            _UltraWinGridHelper1.AddReadOnlyColumn("ItemCode", "物料代码");
            _UltraWinGridHelper1.AddReadOnlyColumn("McontrolType_C", "物料管控类型");
            _UltraWinGridHelper1.AddReadOnlyColumn("Mdesc", "物料描述");
            _UltraWinGridHelper1.AddReadOnlyColumn("Unit", "单位");
            _UltraWinGridHelper1.AddReadOnlyColumn("VendorCode", "供应商代码");
            _UltraWinGridHelper1.AddReadOnlyColumn("VendorName", "供应商名称");
            _UltraWinGridHelper1.AddReadOnlyColumn("Rohs", "ROHS");
            _UltraWinGridHelper1.AddReadOnlyColumn("RecriveQty", "收货数量");
            _UltraWinGridHelper1.AddReadOnlyColumn("QualifyQty", "合格数量");
            _UltraWinGridHelper1.AddReadOnlyColumn("ActQty", "入库数量");
            _UltraWinGridHelper1.AddReadOnlyColumn("RecStatus", "入库单状态");
            _UltraWinGridHelper1.AddReadOnlyColumn("Mstorage", "库别");
            _UltraWinGridHelper1.AddReadOnlyColumn("Mstack", "库位");
            _UltraWinGridHelper1.AddReadOnlyColumn("Memo", "备注");
            _UltraWinGridHelper1.AddReadOnlyColumn("Maintenance", "追溯信息维护");
            _UltraWinGridHelper1.AddReadOnlyColumn("IQCStatus", "IQC检验状态");
            _UltraWinGridHelper1.AddReadOnlyColumn("McontrolType", "物料管控类型");
            _UltraWinGridHelper1.AddReadOnlyColumn("Attribute", "单据类型");
            _UltraWinGridHelper1.AddReadOnlyColumn("CheckStatus", "检验结果");
        }

        #endregion

        #region 页面事件

        private void btnQuery_Click(object sender, EventArgs e)
        {
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            string receiptno = FormatString(this.txtReceiveCode.Value);
            string vendorcode = FormatString(this.txtBussCode.Value);
            string rectype = FormatString(this.drpTypeCode.SelectedItemValue);
            int begdate = FormatHelper.TODateInt(ucBegDate.Value);
            int enddate = FormatHelper.TODateInt(ucEndDate.Value);
            string mcode = FormatString(this.uclMaterialCode.Value);
            string mcontroltype = FormatString(this.drpControlType.SelectedItemValue);
            string status = FormatString(this.drpStatus.SelectedItemValue);
            //bool check = false;

            if (enddate < begdate)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Alert_ReceiveDate_Compare"));
                ucBegDate.Focus();
                return;
            }
            //if (chkIsReceive.Checked)
            //{
            //    check = true;
            //}

            object[] invReceipt = inventoryFacade.QueryMaterialINVReceipt(receiptno, vendorcode, rectype, begdate, enddate, mcode, mcontroltype, status);

            _DataTableLoadedPart.Clear();

            if (invReceipt == null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_No_Data_To_Display"));
                return;
            }

            for (int i = 0; i < invReceipt.Length; i++)
            {
                DBDateTime now = FormatHelper.GetNowDBDateTime(this.DataProvider);
                Domain.IQC.INVReceipt invR = invReceipt[i] as Domain.IQC.INVReceipt;

                #region 当前入库单状态
                string currentStatus = string.Empty;
                if (invR.Recstatus.Equals("WaitCheck") && (!invR.Iqcstatus.Equals("WaitCheck") && !invR.Qualifyqty.Equals(0)))
                {
                    currentStatus = MutiLanguages.ParserString("$CS_" + InvReceiptDetailStatus.InvReceiptDetailStatus_AllowStorage);
                }
                else if (invR.Recstatus.Equals("WaitCheck") && (invR.Iqcstatus.Equals("WaitCheck") || invR.Iqcstatus.Equals("UNQualified")))
                {
                    currentStatus = MutiLanguages.ParserString("$CS_" + InvReceiptDetailStatus.InvReceiptDetailStatus_WaitCheck);
                }
                else if (invR.Recstatus.Equals("Close"))
                {
                    currentStatus = MutiLanguages.ParserString("$CS_" + InvReceiptDetailStatus.InvReceiptDetailStatus_StorageOK);
                }
                #endregion

                _DataTableLoadedPart.Rows.Add(new object[] {
                                                            false,
                                                            invR.Receiptno,
                                                            invR.Receiptline,
                                                            invR.Receiptline,
                                                            MutiLanguages.ParserString(invR.Rectype),
                                                            MutiLanguages.ParserString(invR.Iqcstatus),
                                                            currentStatus,
                                                            invR.Itemcode,
                                                            MutiLanguages.ParserString(invR.McontrolType),
                                                            invR.Mdesc,
                                                            invR.Unit,
                                                            invR.VendorCode,
                                                            invR.VendorName,
                                                            invR.Rohs,
                                                            invR.Qualifyqty,
                                                            invR.Qualifyqty,
                                                            invR.Actqty,
                                                            invR.Recstatus,
                                                            invR.Mstorage,
                                                            invR.Mstack,
                                                            invR.Memo,
                                                            "",
                                                            invR.Iqcstatus,
                                                            invR.McontrolType,
                                                            invR.Attribute,
                                                            invR.CheckStatus
                                                             });
            }
        }

        private void btnGetBussCode_Click(object sender, EventArgs e)
        {
            FVendorCodeQuery fBigSSCodeQuery = new FVendorCodeQuery();
            fBigSSCodeQuery.Owner = this;
            fBigSSCodeQuery.StartPosition = FormStartPosition.CenterScreen;
            fBigSSCodeQuery.BigSSCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(BigSSCodeSelector_BigSSCodeSelectedEvent);
            fBigSSCodeQuery.ShowDialog();
            fBigSSCodeQuery = null;

            this.txtBussCode.TextFocus(false, true);
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            this.btnReceive.Focus();
            if (!HaveSelectedGrid())
            {
                return;
            }

            if (!GridCheckd())
            {
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            this.DataProvider.BeginTransaction();
            try
            {
                DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                WarehouseFacade warehouseFacade = new WarehouseFacade(this.DataProvider);
                IQCFacade _IQCFacade = new IQCFacade(this.DataProvider);
                for (int i = 0; i < ultraGridScrutiny.Rows.Count; i++)
                {
                    if (ultraGridScrutiny.Rows[i].Cells[0].Value.ToString().ToLower() == "true")
                    {
                        ItemFacade itemfacade = new ItemFacade(this.DataProvider);
                        string mocode = ultraGridScrutiny.Rows[i].Cells["ItemCode"].Value.ToString();
                        string transno = ultraGridScrutiny.Rows[i].Cells["ReceiptNo"].Value.ToString();
                        string transline = ultraGridScrutiny.Rows[i].Cells["ReceiptLine"].Value.ToString();
                        string mstorge = ultraGridScrutiny.Rows[i].Cells["Mstorage"].Value.ToString();
                        string mstack = ultraGridScrutiny.Rows[i].Cells["Mstack"].Value.ToString();
                        string actqty = ultraGridScrutiny.Rows[i].Cells["ActQty"].Value.ToString();
                        int index = actqty.IndexOf(".");
                        if (index > 0)
                        {
                            actqty = actqty.Substring(0, index);
                        }
                        string recriveqty = ultraGridScrutiny.Rows[i].Cells["RecriveQty"].Value.ToString();
                        index = recriveqty.IndexOf(".");
                        if (index > 0)
                        {
                            recriveqty = recriveqty.Substring(0, index);
                        }
                        string memo = ultraGridScrutiny.Rows[i].Cells["Memo"].Value.ToString();
                        string bussinesscode = this.drpBusiness.SelectedItemValue.ToString();
                        //int orgid = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                        int orgid = ((InvReceipt)_IQCFacade.GetInvReceipt(transno)).Orgid;
                        object[] itemlot = inventoryFacade.QueryItemLot(mocode, transno, int.Parse(transline), orgid);

                        //如果是批次管控料，新增或更新：TBLStorageLotInfo（根据MCODE，TransNO，TransLine查出所有的LotNO） 
                        //如果是批次管控料，记录TBLITEMTrans，TBLITEMTransLot
                        if (ultraGridScrutiny.Rows[i].Cells["McontrolType"].Value.ToString().ToUpper() == "ITEM_CONTROL_LOT")
                        {
                            this.AddItemTrans(transno, transline, mocode, mstorge, mstack, decimal.Parse(actqty), memo, bussinesscode, orgid);
                            if (itemlot != null)
                            {
                                foreach (ItemLot obj in itemlot)
                                {
                                    //ItemFacade itemfacade = new ItemFacade(this.DataProvider);
                                    this.AddOrUpdateStorageLotInfo(obj.Lotno, mstorge, mstack, mocode, obj.Lotqty);
                                    this.AddItemTransLot(obj.Lotno, mocode, obj.Lotqty, memo);

                                    #region
                                    //  jack 2012-03-13    add start 
                                    //  如果是批次料，同时还是SMT料，那么保存到TBLREEL料卷表里。
                                    Domain.MOModel.Material material = (Domain.MOModel.Material)itemfacade.GetMaterial(mocode, orgid);
                                    if (material.IsSMT.Trim().ToUpper() == MaterialIsSMT.MaterialIsSMT_Y.ToString().Trim().ToUpper())
                                    {
                                        SMTFacade smtfacade = new SMTFacade(this.DataProvider);
                                        BenQGuru.eMES.Domain.SMT.Reel reel = smtfacade.CreateNewReel();

                                        object objCheck = smtfacade.GetReel(obj.Lotno.Trim());

                                        if (objCheck == null)
                                        {
                                            reel.ReelNo = obj.Lotno;    // System.Guid.NewGuid().ToString();      //主键
                                            reel.Qty = obj.Lotqty;      // Convert.ToInt32(actqty);
                                            reel.PartNo = mocode;
                                            reel.LotNo = obj.Lotno;
                                            reel.DateCode = Convert.ToString(obj.Datecode);
                                            reel.MaintainUser = ApplicationService.Current().UserCode;
                                            reel.MaintainDate = dBDateTime.DBDate;
                                            reel.MaintainTime = dBDateTime.DBTime;
                                            reel.EAttribute1 = "";
                                            reel.UsedQty = 0;
                                            reel.UsedFlag = "";       //  1
                                            reel.MOCode = " ";
                                            reel.StepSequenceCode = " ";
                                            reel.IsSpecial = "0";     //  0
                                            reel.Memo = " ";
                                            reel.CheckDiffQty = 0;
                                            smtfacade.AddReel(reel);
                                        }
                                    }
                                    //      jack 2012-03-13    add end  
                                    #endregion
                                }
                                this.AddOrUpdateStorageInfo(mocode, decimal.Parse(actqty), mstorge, mstack);
                            }


                        }
                        //如果是单件管控料，更新：TBLITEMLotDetail（storageID, StackCODE, SerialStatus= STORAGE）条件（LotNO，MCODE）
                        //如果是单件管控料，记录TBLITEMTrans，TBLITEMTransLot，TBLITEMTransLotDetail
                        if (ultraGridScrutiny.Rows[i].Cells["McontrolType"].Value.ToString().ToUpper() == "ITEM_CONTROL_KEYPARTS")
                        {
                            this.AddItemTrans(transno, transline, mocode, mstorge, mstack, decimal.Parse(recriveqty), memo, bussinesscode, orgid);
                            if (itemlot != null)
                            {
                                foreach (ItemLot obj in itemlot)
                                {
                                    this.AddOrUpdateStorageLotInfo(obj.Lotno, mstorge, mstack, mocode, obj.Lotqty);
                                    this.AddItemTransLot(obj.Lotno, mocode, obj.Lotqty, memo);
                                    object[] itemlotdetails = inventoryFacade.GetITEMLOTDETAILByLotno(obj.Lotno, mocode);
                                    if (itemlotdetails != null && itemlotdetails.Length != 0)
                                    {
                                        foreach (object itemObj in itemlotdetails)
                                        {
                                            ItemLotDetail itemlotdetail = (ItemLotDetail)itemObj;
                                            itemlotdetail.Storageid = mstorge;
                                            itemlotdetail.Stackcode = mstack;
                                            itemlotdetail.Serialstatus = "STORAGE";
                                            inventoryFacade.UpdateItemLotDetail(itemlotdetail);
                                            string serialno = itemlotdetail.Serialno.ToString();
                                            this.AddItemTransLotDetail(obj.Lotno, mocode, serialno);
                                        }

                                    }
                                }
                                this.AddOrUpdateStorageInfo(mocode, decimal.Parse(actqty), mstorge, mstack);
                            }
                        }
                        //如果是非批次管控料和非单件管控料，只记录TBLITEMTrans
                        if (ultraGridScrutiny.Rows[i].Cells["McontrolType"].Value.ToString().ToUpper() != "ITEM_CONTROL_KEYPARTS" && ultraGridScrutiny.Rows[i].Cells["McontrolType"].Value.ToString().ToUpper() != "ITEM_CONTROL_LOT")
                        {
                            this.AddItemTrans(transno, transline, mocode, mstorge, mstack, decimal.Parse(actqty), memo, bussinesscode, orgid);
                            this.AddOrUpdateStorageInfo(mocode, decimal.Parse(actqty), mstorge, mstack);
                        }

                        //Status=Close
                        this.CloseStatus(transno, int.Parse(transline));
                    }
                }
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_SUCCESS"));
                this.btnQuery_Click(sender, e);
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_FALL " + ex.Message));
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.btnClose.Focus();

            DataRow[] dataRow = _DataTableLoadedPart.Select("Check = 'true'");
            string msg = null;
            foreach (DataRow row in dataRow)
            {
                msg += " " + MutiLanguages.ParserString("InvReceiptNO") + ":" + row["ReceiptNo"].ToString() + MutiLanguages.ParserString("$CS_Receipt_LineNo") + ":" + row["ReceiptLine"].ToString();
            }
            if (MessageBox.Show(MutiLanguages.ParserMessage("$ConformClose") + msg, MutiLanguages.ParserMessage("$ShowMessage"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                foreach (DataRow row in dataRow)
                {
                    string stno = row["ReceiptNo"].ToString();
                    string stline = row["ReceiptLine"].ToString();
                    this.CloseStatus(stno, int.Parse(stline));
                }
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_SUCCESS"));
            }

            //bool isShow = false;
            //for (int i = 0; i < ultraGridScrutiny.Rows.Count; i++)
            //{
            //    if (ultraGridScrutiny.Rows[i].Cells[0].Value.ToString().ToLower() == "true")
            //    {
            //        if (!isShow)
            //        {             
            //            if (MessageBox.Show("确认关闭？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            //            {
            //                isShow = true;
            //                string stno = ultraGridScrutiny.Rows[i].Cells["ReceiptNo"].Value.ToString();
            //                string stline = ultraGridScrutiny.Rows[i].Cells["ReceiptLine"].Value.ToString();
            //                this.CloseStatus(stno, int.Parse(stline));
            //                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_SUCCESS"));
            //            }
            //        }
            //        else
            //        {
            //            string stno = ultraGridScrutiny.Rows[i].Cells["ReceiptNo"].Value.ToString();
            //            string stline = ultraGridScrutiny.Rows[i].Cells["ReceiptLine"].Value.ToString();
            //            this.CloseStatus(stno, int.Parse(stline));
            //            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_SUCCESS"));
            //        }
            //    }
            //}
            this.btnQuery_Click(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ultraGridScrutiny_ClickCellButton(object sender, CellEventArgs e)
        {
            //如果不是可入库单据点击无效
            UltraGridRow gridRow = e.Cell.Row;
            if (gridRow.Cells["RecStatus"].Value.Equals("WaitCheck") && (!gridRow.Cells["IQCStatus"].Value.Equals("WaitCheck") && !gridRow.Cells["QualifyQty"].Value.ToString().Equals("0")))
            {
                if (e.Cell.Column.Key.ToUpper() == "Maintenance".ToUpper())
                {
                    //Andy完成;
                    string repNO = e.Cell.Row.Cells["ReceiptNo"].Text.Trim();
                    string lineNO = e.Cell.Row.Cells["StLine"].Text.Trim();

                    FKeyPart fKeyPart = new FKeyPart(repNO, lineNO);
                    fKeyPart.ShowDialog();
                    //更新入库数量
                    if (e.Cell.Row.Cells["McontrolType"].Text.Trim().ToUpper() == "ITEM_CONTROL_KEYPARTS" || e.Cell.Row.Cells["McontrolType"].Text.Trim().ToUpper() == "ITEM_CONTROL_LOT")
                    {
                        e.Cell.Row.Cells["ActQty"].Value = FKeyPart.lotCount.ToString();
                    }
                }
                if (e.Cell.Column.Key.ToUpper() == "Mstorage".ToUpper())
                {
                    this.txtstroage.Text = "";
                    FStorageCodeQuery fBigSSCodeQuery = new FStorageCodeQuery();
                    fBigSSCodeQuery.Owner = this;
                    fBigSSCodeQuery.StartPosition = FormStartPosition.CenterScreen;
                    fBigSSCodeQuery.BigSSCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(StorageCodeSelector_StorageCodeSelectedEvent);
                    fBigSSCodeQuery.ShowDialog();
                    fBigSSCodeQuery = null;
                    if (this.txtstroage.Text.Trim().Length != 0)
                    {
                        if (this.txtstroage.Text == "$&Empty")
                        {
                            e.Cell.Value = "";
                            return;
                        }
                        e.Cell.Value = this.txtstroage.Text;

                        e.Cell.Row.Cells["Mstack"].Value = "";
                    }

                }
                if (e.Cell.Column.Key.ToUpper() == "Mstack".ToUpper())
                {
                    this.txtstack.Text = "";
                    FStackCode fStackCodeQuery = new FStackCode(e.Cell.Row.Cells["Mstorage"].Value.ToString());
                    fStackCodeQuery.Owner = this;
                    fStackCodeQuery.StartPosition = FormStartPosition.CenterScreen;
                    fStackCodeQuery.StackCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(StackCodeSelector_StackCodeSelectedEvent);
                    fStackCodeQuery.ShowDialog();
                    fStackCodeQuery = null;
                    if (this.txtstack.Text.Trim().Length != 0)
                    {
                        if (this.txtstack.Text == "$&Empty")
                        {
                            e.Cell.Value = "";
                            return;
                        }
                        e.Cell.Value = this.txtstack.Text;
                    }
                }
            }
            else if (gridRow.Cells["RecStatus"].Value.Equals("Close"))
            {
                if (e.Cell.Column.Key.ToUpper() == "Maintenance".ToUpper())
                {
                    string repNO = e.Cell.Row.Cells["ReceiptNo"].Text.Trim();
                    string lineNO = e.Cell.Row.Cells["StLine"].Text.Trim();

                    FKeyPart fKeyPart = new FKeyPart(repNO, lineNO);
                    fKeyPart.ShowDialog();                    
                }
            }
            
        }

        private void ultraGridScrutiny_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            UltraGridRow gridRow = e.Row;
            if (gridRow.Cells["RecStatus"].Value.Equals("WaitCheck") && (!gridRow.Cells["IQCStatus"].Value.Equals("WaitCheck") && !gridRow.Cells["QualifyQty"].Value.ToString().Equals("0")))
            {
                string value = e.Row.Cells["McontrolType"].Value.ToString().ToLower();//19
                if (e.Row != null && (value == "item_control_keyparts" || value == "item_control_lot"))
                {
                    e.Row.Cells["ActQty"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                }
                else
                {
                    e.Row.Cells["ActQty"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    e.Row.Cells["ActQty"].Appearance.BackColor = Color.LawnGreen;
                }

                e.Row.Cells["Mstorage"].Appearance.BackColor = Color.LawnGreen;
                e.Row.Cells["Mstorage"].Appearance.BackColor = Color.LawnGreen;
                e.Row.Cells["Mstack"].Appearance.BackColor = Color.LawnGreen;
                e.Row.Cells["Memo"].Appearance.BackColor = Color.LawnGreen;
                e.Row.Cells["Maintenance"].Appearance.BackColor = Color.LawnGreen;

                //ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Mstorage"].Header.Appearance.BackColor = Color.LawnGreen;
                //ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Mstack"].Header.Appearance.BackColor = Color.LawnGreen;
                //ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Memo"].Header.Appearance.BackColor = Color.LawnGreen;
                //ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Maintenance"].Header.Appearance.BackColor = Color.LawnGreen;
            }

        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ultraGridScrutiny.Rows.Count; i++)
            {
                ultraGridScrutiny.Rows[i].Cells["Check"].Value = this.chkAll.Checked;
            }
        }

        #endregion

        #region 自定义事件

        private void BigSSCodeSelector_BigSSCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.txtBussCode.Value = e.CustomObject;
        }

        private void StorageCodeSelector_StorageCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.txtstroage.Text = e.CustomObject;
        }

        private void StackCodeSelector_StackCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.txtstack.Text = e.CustomObject;
        }

        #endregion

        #region 自定义方法

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

        //检查是否选择数据
        private bool HaveSelectedGrid()
        {
            if (ultraGridScrutiny.Rows.Count <= 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_GRID_NO_RECORD"));
                return false;
            }

            for (int i = 0; i < ultraGridScrutiny.Rows.Count; i++)
            {
                if (ultraGridScrutiny.Rows[i].Cells[0].Value.ToString().ToLower() == "true")
                {
                    return true;
                }
            }

            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Please_Take_OneDate"));
            return false;
        }

        private bool GridCheckd()
        {
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            for (int i = 0; i < ultraGridScrutiny.Rows.Count; i++)
            {
                if (ultraGridScrutiny.Rows[i].Cells[0].Value.ToString().ToLower() == "true")
                {
                    //入库单行已关闭不可再入库
                    if (ultraGridScrutiny.Rows[i].Cells["RecStatus"].Value.ToString().ToUpper() == "CLOSE")
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Recstatus_Is_Close"));
                        return false;
                    }

                    //物料的检验结果必须为合格：TBLINVReceiptDetail.IQCStatus=Qualified
                    if (ultraGridScrutiny.Rows[i].Cells["IQCStatus"].Value.ToString().ToUpper() != "QUALIFIED")
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_IQCStatus_Not_Qualified"));
                        return false;
                    }
                    //如果入库的库位已经有数据，检查库位是否允许多种物料混放（tblstack. ISONEITEM）[检查物料代码是否一致]
                    if (ultraGridScrutiny.Rows[i].Cells["Mstack"].Value.ToString() != "")
                    {
                        InventoryFacade inventoryfacade = new InventoryFacade(this.DataProvider);
                        string mstorge = ultraGridScrutiny.Rows[i].Cells["Mstorage"].Value.ToString();
                        string mocode = ultraGridScrutiny.Rows[i].Cells["ItemCode"].Value.ToString();
                        string mstack = ultraGridScrutiny.Rows[i].Cells["Mstack"].Value.ToString();
                        SStack sstack = (SStack)inventoryfacade.GetSStack(ultraGridScrutiny.Rows[i].Cells["Mstack"].Value.ToString());
                        if (sstack != null)
                        {
                            if (sstack.IsOneItem == "Y")
                            {
                                bool exists = false;
                                ItemFacade itemfacade = new ItemFacade(this.DataProvider);
                                object[] storagelotinfo = inventoryFacade.QueryStorageInfo(mstorge, mstack);
                                //object storagelotinfo = itemfacade.GetStorageInfo(mstorge,mocode, mstack);
                                if (storagelotinfo != null)
                                {
                                    for (int n = 0; n < storagelotinfo.Length; n++)
                                    {
                                        if (((StorageInfo)storagelotinfo[n]).Mcode.ToString() != mocode)
                                        {
                                            exists = true;
                                        }
                                    }
                                    if (exists)
                                    {
                                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_IQC_Not_OneItem"));
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                    //检查入库数量不能为零
                    if (ultraGridScrutiny.Rows[i].Cells["ActQty"].Value.ToString() == "0")
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ActQty_Must_Exist"));
                        return false;
                    }
                    //检查库别和库位必须输入
                    if (string.IsNullOrEmpty(ultraGridScrutiny.Rows[i].Cells["Mstorage"].Value.ToString()) || string.IsNullOrEmpty(ultraGridScrutiny.Rows[i].Cells["Mstack"].Value.ToString()))
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Storage_Must_Exist"));
                        return false;
                    }
                    //检查业务类型必须输入
                    if (this.drpBusiness.SelectedItemValue == null || this.drpBusiness.SelectedItemValue.ToString().Equals(""))
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_BUSINESS_CODE_INPUT"));
                        return false;
                    }
                    //如果入库数量<>收货数量，提示用户
                    if (Convert.ToDecimal(ultraGridScrutiny.Rows[i].Cells["ActQty"].Value) != Convert.ToDecimal(ultraGridScrutiny.Rows[i].Cells["RecriveQty"].Value))
                    {
                        string msgInfo = UserControl.MutiLanguages.ParserMessage("$CS_ActQty_Must_Equal_RecriveQty") + "," + MutiLanguages.ParserString("$CS_Is_Continued");
                        frmDialog dialog = new frmDialog();
                        dialog.Text = this.Text;
                        dialog.DialogMessage = msgInfo;

                        if (DialogResult.OK != dialog.ShowDialog(this))
                        {
                            return false;
                        }
                        //ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ActQty_Must_Equal_RecriveQty"));
                        //return false;
                    }
                    //如果物料是批管控料或单件管控料：（tblmaterial. MCONTROLTYPE）
                    //检查TBLITEMLot. TransNO=@入库单号and TransLine=@入库单行的数量sum(TBLITEMLot.LOTQTY)是否等于入库数量，不等于则提示：请维护该物料的批号或单件号。
                    if (ultraGridScrutiny.Rows[i].Cells["McontrolType"].Value.ToString().ToUpper() == "ITEM_CONTROL_KEYPARTS" || ultraGridScrutiny.Rows[i].Cells["McontrolType"].Value.ToString().ToUpper() == "ITEM_CONTROL_LOT")
                    {
                        ItemFacade itemfacade = new ItemFacade(this.DataProvider);
                        string transno = ultraGridScrutiny.Rows[i].Cells["ReceiptNo"].Value.ToString();
                        string transline = ultraGridScrutiny.Rows[i].Cells["ReceiptLine"].Value.ToString();
                        string actqty = ultraGridScrutiny.Rows[i].Cells["QualifyQty"].Value.ToString();
                        ItemLot itemlot = (ItemLot)inventoryFacade.QuerySumLotQty(transno, int.Parse(transline));
                        if (itemlot != null)
                        {
                            if (itemlot.Lotqty != Convert.ToDecimal(actqty))
                            {
                                string msgInfo = UserControl.MutiLanguages.ParserMessage("$CS_ActQty_Must_Equal_QualifyQty") + "," + MutiLanguages.ParserString("$CS_Is_Continued");
                                frmDialog dialog = new frmDialog();
                                dialog.Text = this.Text;
                                dialog.DialogMessage = msgInfo;

                                if (DialogResult.OK != dialog.ShowDialog(this))
                                {
                                    return false;
                                }
                                //ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Please_Maintenance_TransNO_TransLine"));
                                //return false;
                            }
                        }
                    }
                    //如果是湿敏元器件弹出提示框
                    //if (!string.Equals(ultraGridScrutiny.Rows[i].Cells["MHumidityLevel"].Value.ToString(), "0"))
                    //{
                    //    MessageBox.Show(this, "物料为湿敏元器件，请注意！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}

                }
            }

            return true;
        }

        private void AddItemTrans(string transno, string transline, string mocode, string mstorge, string mstack, decimal recriveqty, string memo, string bussinesscode, int orgid)
        {
            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            ItemFacade itemfacade = new ItemFacade(DataProvider);
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            BenQGuru.eMES.Domain.MOModel.Material material = (BenQGuru.eMES.Domain.MOModel.Material)itemfacade.GetMaterial(mocode, orgid);
            if (material != null)
            {
                ItemTrans itemtrans = new ItemTrans();
                //serial = inventoryFacade.Maxserial();
                //itemtrans.Serial = serial;
                itemtrans.Transno = transno;
                itemtrans.Transline = int.Parse(transline);
                itemtrans.Itemcode = mocode;
                //itemtrans.Frmstorageid = material.Mstorage;
                //itemtrans.Frmstackcode = material.Mstack;
                itemtrans.Tostorageid = mstorge;
                itemtrans.Tostackcode = mstack;
                itemtrans.Transqty = recriveqty;
                itemtrans.Memo = memo;
                itemtrans.Transtype = "receive";
                itemtrans.Businesscode = bussinesscode;
                itemtrans.Orgid = orgid;
                itemtrans.Muser = ApplicationService.Current().UserCode;
                itemtrans.Mdate = dBDateTime.DBDate;
                itemtrans.Mtime = dBDateTime.DBTime;
                inventoryFacade.AddItemTrans(itemtrans);

                serial = inventoryFacade.Maxserial(transno);

            }

        }

        private void AddOrUpdateStorageInfo(string mocode, decimal lotqty, string storageID, string stackCode)
        {
            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            //更新源库存TBLStorageInfo
            StorageInfo storageInfo = (StorageInfo)inventoryFacade.GetStorageInfo(storageID, mocode, stackCode);
            if (storageInfo != null)
            {
                storageInfo.Storageqty = storageInfo.Storageqty + lotqty;
                storageInfo.Muser = ApplicationService.Current().UserCode;
                storageInfo.Mdate = dBDateTime.DBDate;
                storageInfo.Mtime = dBDateTime.DBTime;
                inventoryFacade.UpdateStorageInfo(storageInfo);
            }
            else
            {
                storageInfo = new StorageInfo();
                storageInfo.Mcode = mocode;
                storageInfo.Mdate = dBDateTime.DBDate;
                storageInfo.Mtime = dBDateTime.DBTime;
                storageInfo.Muser = ApplicationService.Current().UserCode;
                storageInfo.Stackcode = stackCode;
                storageInfo.Storageid = storageID;
                storageInfo.Storageqty = lotqty;
                storageInfo.OrganizationID = ApplicationService.Current().LoginInfo.Resource.OrganizationID;
                inventoryFacade.AddStorageInfo(storageInfo);
            }
        }

        private void AddOrUpdateStorageLotInfo(string lotNo, string mstorage, string mstack, string mocode, decimal actqty)
        {
            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            StorageLotInfo storagelotinfo = (StorageLotInfo)inventoryFacade.GetStorageLotInfo(lotNo, mstorage, mstack, mocode);
            if (storagelotinfo != null)
            {
                storagelotinfo.Lotqty = actqty;
                storagelotinfo.Receivedate = dBDateTime.DBDate;
                storagelotinfo.Muser = ApplicationService.Current().UserCode;
                storagelotinfo.Mdate = dBDateTime.DBDate;
                storagelotinfo.Mtime = dBDateTime.DBTime;
                inventoryFacade.UpdateStorageLotInfo(storagelotinfo);
            }
            else
            {
                StorageLotInfo newstoragelotinfo = new StorageLotInfo();
                newstoragelotinfo.Lotno = lotNo;
                newstoragelotinfo.Storageid = mstorage;
                newstoragelotinfo.Stackcode = mstack;
                newstoragelotinfo.Mcode = mocode;
                newstoragelotinfo.Lotqty = actqty;
                newstoragelotinfo.Receivedate = dBDateTime.DBDate;
                newstoragelotinfo.Muser = ApplicationService.Current().UserCode;
                newstoragelotinfo.Mdate = dBDateTime.DBDate;
                newstoragelotinfo.Mtime = dBDateTime.DBTime;
                inventoryFacade.AddStorageLotInfo(newstoragelotinfo);
            }
        }

        //private int GetSerial(string transno, string transline, string mocode, string mstorge, string mstack, int recriveqty, string memo, string bussinesscode, int orgid)
        //{
        //    DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
        //    ItemFacade itemfacade = new ItemFacade(this.DataProvider);
        //    BenQGuru.eMES.Domain.MOModel.Material material = (BenQGuru.eMES.Domain.MOModel.Material)itemfacade.GetMaterial(mocode, orgid);
        //    if (material != null)
        //    {
        //        ItemTrans itemtrans = new ItemTrans();
        //        itemtrans.Transno = transno;
        //        itemtrans.Transline = transline;
        //        itemtrans.Itemcode = mocode;
        //        itemtrans.Frmstorageid = material.Mstorage;
        //        itemtrans.Frmstackcode = material.Mstack;
        //        itemtrans.Tostorageid = mstorge;
        //        itemtrans.Tostackcode = mstack;
        //        itemtrans.Transqty = recriveqty;
        //        itemtrans.Memo = memo;
        //        itemtrans.Transtype = "receive";
        //        itemtrans.Businesscode = bussinesscode;
        //        itemtrans.Orgid = orgid;
        //        itemtrans.Muser = ApplicationService.Current().UserCode;
        //        itemtrans.Mdate = dBDateTime.DBDate;
        //        itemtrans.Mtime = dBDateTime.DBTime;
        //        return itemfacade.GetItemTransserial(itemtrans);
        //    }
        //    return 0;
        //}

        private void AddItemTransLot(string lotno, string itemcode, decimal transqty, string memo)
        {
            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            ItemTransLot itemtranslot = new ItemTransLot();
            itemtranslot.Tblitemtrans_serial = serial;
            itemtranslot.Lotno = lotno;
            itemtranslot.Itemcode = itemcode;
            itemtranslot.Transqty = transqty;
            itemtranslot.Memo = memo;
            itemtranslot.Muser = ApplicationService.Current().UserCode;
            itemtranslot.Mdate = dBDateTime.DBDate;
            itemtranslot.Mtime = dBDateTime.DBTime;
            inventoryFacade.AddItemTransLot(itemtranslot);
        }

        private void AddItemTransLotDetail(string lotno, string itemcode, string serialno)
        {
            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            ItemTransLotDetail itemtranslotdetail = new ItemTransLotDetail();
            itemtranslotdetail.Tblitemtrans_serial = serial;
            itemtranslotdetail.Lotno = lotno;
            itemtranslotdetail.Itemcode = itemcode;
            itemtranslotdetail.Serialno = serialno;
            itemtranslotdetail.Muser = ApplicationService.Current().UserCode;
            itemtranslotdetail.Mdate = dBDateTime.DBDate;
            itemtranslotdetail.Mtime = dBDateTime.DBTime;
            inventoryFacade.AddItemTransLotDetail(itemtranslotdetail);
        }

        private void CloseStatus(string stno, int stline)
        {
            //修改TBLINVReceiptDetail.RECSTATUS为Close。（STNO，STLINE）
            IQCFacade iqcfacade = new IQCFacade(this.DataProvider);

            InvReceiptDetail receiptdetail = (InvReceiptDetail)iqcfacade.GetInvReceiptDetail(stno, stline);

            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            //行项目Close
            if (receiptdetail != null)
            {
                receiptdetail.IsInStorage = "Y";
                receiptdetail.Recstatus = "Close";
                receiptdetail.Muser = ApplicationService.Current().UserCode;
                receiptdetail.Mdate = dBDateTime.DBDate;
                receiptdetail.Mtime = dBDateTime.DBTime;
                iqcfacade.UpdateInvReceiptDetail(receiptdetail);
            }

            //如果行项目都Close了，则更新TBLINVReceipt. RECSTATUS=Close
            if (iqcfacade.CheckAllInvReceiptDetailIsClose(stno))
            {
                //关闭TBLINVReceipt
                InvReceipt invreceipt = (InvReceipt)iqcfacade.GetInvReceipt(stno);
                if (invreceipt != null)
                {
                    invreceipt.IsAllInStorage = "Y";
                    invreceipt.Recstatus = "Close";
                    invreceipt.Muser = ApplicationService.Current().UserCode;
                    invreceipt.Mdate = dBDateTime.DBDate;
                    invreceipt.Mtime = dBDateTime.DBTime;
                    iqcfacade.UpdateInvReceipt(invreceipt);
                }
            }
        }

        private void InitializedrpTypeCode()
        {
            drpTypeCode.Clear();
            drpTypeCode.AddItem("", "");
            drpTypeCode.AddItem(MutiLanguages.ParserString(INVReceiptType.INVReceiptType_P), INVReceiptType.INVReceiptType_P);
            drpTypeCode.AddItem(MutiLanguages.ParserString(INVReceiptType.INVReceiptType_WX), INVReceiptType.INVReceiptType_WX);
            drpTypeCode.AddItem(MutiLanguages.ParserString(INVReceiptType.INVReceiptType_O), INVReceiptType.INVReceiptType_O);
        }

        private void InitializedrpBusiness()
        {
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            object[] objs = inventoryFacade.GetInvBusiness(string.Empty, BussinessType.type_in);
            if (objs != null)
            {
                this.drpBusiness.Clear();
                drpBusiness.AddItem("", "");
                foreach (InvBusiness obj in objs)
                {
                    this.drpBusiness.AddItem(obj.BusinessDescription, obj.BusinessCode);
                }
            }
        }

        private void InitialzedrpControlType()
        {
            drpControlType.Clear();
            OPBOMFacade _opBOMFacade = new OPBOMFacade();

            string[] controls = _opBOMFacade.GetItemControlTypes();
            drpControlType.AddItem("", "");
            for (int i = 0; i < controls.Length; i++)
            {
                drpControlType.AddItem(MutiLanguages.ParserString(controls[i]), controls[i]);
            }
        }

        private void InitialzedrpStatus()
        {
            drpStatus.Clear();

            drpStatus.AddItem("", "");
            drpStatus.AddItem(MutiLanguages.ParserString("$CS_" + InvReceiptDetailStatus.InvReceiptDetailStatus_WaitCheck), InvReceiptDetailStatus.InvReceiptDetailStatus_WaitCheck);
            drpStatus.AddItem(MutiLanguages.ParserString("$CS_" + InvReceiptDetailStatus.InvReceiptDetailStatus_AllowStorage), InvReceiptDetailStatus.InvReceiptDetailStatus_AllowStorage);
            drpStatus.AddItem(MutiLanguages.ParserString("$CS_" + InvReceiptDetailStatus.InvReceiptDetailStatus_StorageOK), InvReceiptDetailStatus.InvReceiptDetailStatus_StorageOK);
            drpStatus.SelectedIndex = 2;
        }

        private void btnMaterialCode_Click(object sender, EventArgs e)
        {
            FMCodeQuery fMCodeQuery = new FMCodeQuery();
            fMCodeQuery.Owner = this;
            fMCodeQuery.StartPosition = FormStartPosition.CenterScreen;
            fMCodeQuery.MCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(MCodeSelector_MCodeSelectedEvent);
            fMCodeQuery.ShowDialog();
            fMCodeQuery = null;
        }

        private void MCodeSelector_MCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.uclMaterialCode.Value = e.CustomObject;
        }

        #endregion

    }
}
