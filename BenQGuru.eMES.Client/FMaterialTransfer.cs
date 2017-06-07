using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UserControl;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.CodeSoftPrint;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;

using BenQGuru.eMES.Package;

namespace BenQGuru.eMES.Client
{
    public partial class FMaterialTransfer : BaseForm
    {
        #region 变量
        private string m_TransferNO = string.Empty;
        private string m_FromStorageID = string.Empty;
        private string m_ToStorageID = string.Empty;
        private int orgID = -1;
        public PrintTemplate[] _PrintTemplateList = null;
        private string _DataDescFileName = "Label.dsc";

        //add by li 2011.1.26
        private const string packRcard = "0";
        private const string packPallet = "1";

        private DataSet m_SampleList = null;
        private DataTable m_CheckInvTransferDetail = null;
        private DataTable m_CheckITEMTrans = null;
        private DataTable m_CheckKPartTrans = null;
        private WarehouseFacade _WarehouseFacade = null;
        private InventoryFacade _InventoryFacade = null;
        private ItemFacade _ItemFacade = null;

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }
        #endregion

        public FMaterialTransfer()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            this.ultraGridHead.DisplayLayout.Appearance.BackColor = System.Drawing.Color.Gainsboro; ;
            this.ultraGridHead.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridHead.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridHead.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridHead.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridHead.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridHead.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridHead.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridHead.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridHead.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void FMaterialTransfer_Load(object sender, EventArgs e)
        {
            _WarehouseFacade = new WarehouseFacade(this.DataProvider);
            _InventoryFacade = new InventoryFacade(this.DataProvider);
            _ItemFacade = new ItemFacade(this.DataProvider);

            InitializeSampleListGrid();
            this.opsCheckFIFO.Value = NeedVendor.NeedVendor_N;
            this.BindBusinessCode();
            LoadPrinter();
            LoadTemplateList();
            this.ucLabelEditMemo.Enabled = false;
            this.ucLabelEditRectype.Enabled = false;
            this.ucLabelEditToStorageID.Enabled = false;
            this.ucLabelEditFromStorageID.Enabled = false;

            //add by li 2011.1.26 
            this.opsetPackObject.Value = packRcard;

            //this.InitGridLanguage(ultraGridHead);
            //this.InitPageLanguage();

        }

        private void ultraGridHead_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            //e.Layout.MaxBandDepth = 1;
            // 自动判断是否显示前面的+、-号
            e.Layout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 冻结列
            e.Layout.UseFixedHeaders = true;
            e.Layout.Override.FixedHeaderAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedHeaderAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedCellAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedCellAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "CheckInvTransferDetail";
            e.Layout.Bands[1].ScrollTipField = "CheckITEMTrans";
            e.Layout.Bands[2].ScrollTipField = "CheckKPartTrans";

            //设置列宽和列名称
            //e.Layout.Bands[0].Columns["TransferNO"].Hidden = true;
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["TransferLine"].Header.Caption = "行号";
            e.Layout.Bands[0].Columns["MCode"].Header.Caption = "物料代码";
            e.Layout.Bands[0].Columns["MDesc"].Header.Caption = "物料描述";
            e.Layout.Bands[0].Columns["McontrolType"].Hidden = true;
            e.Layout.Bands[0].Columns["McontrolTypeName"].Header.Caption = "物料管控类型";
            e.Layout.Bands[0].Columns["FrmStorageQty"].Header.Caption = "源库存数量";
            e.Layout.Bands[0].Columns["ToStorageQty"].Header.Caption = "目的库存数量";
            e.Layout.Bands[0].Columns["PlanQty"].Header.Caption = "计划数量";
            e.Layout.Bands[0].Columns["AlreadyACTQty"].Header.Caption = "已发数量";
            e.Layout.Bands[0].Columns["Memo"].Header.Caption = "备注";
            e.Layout.Bands[0].Columns["MOCode"].Header.Caption = "工单号";
            e.Layout.Bands[0].Columns["ToStorageCode"].Header.Caption = "目的库别";
            e.Layout.Bands[0].Columns["ToStackCode"].Header.Caption = "目的库位";
            e.Layout.Bands[0].Columns["TransQty"].Header.Caption = "发料数量";
            e.Layout.Bands[0].Columns["Type"].Hidden = true;

            e.Layout.Bands[0].Columns["Checked"].Width = 30;
            e.Layout.Bands[0].Columns["TransferLine"].Width = 70;
            e.Layout.Bands[0].Columns["MCode"].Width = 100;
            e.Layout.Bands[0].Columns["MDesc"].Width = 100;
            e.Layout.Bands[0].Columns["McontrolType"].Width = 70;
            e.Layout.Bands[0].Columns["McontrolTypeName"].Width = 100;
            e.Layout.Bands[0].Columns["FrmStorageQty"].Width = 100;
            e.Layout.Bands[0].Columns["ToStorageQty"].Width = 100;
            e.Layout.Bands[0].Columns["PlanQty"].Width = 70;
            e.Layout.Bands[0].Columns["AlreadyACTQty"].Width = 70;
            e.Layout.Bands[0].Columns["Memo"].Width = 100;
            e.Layout.Bands[0].Columns["MOCode"].Width = 100;
            e.Layout.Bands[0].Columns["ToStorageCode"].Width = 100;
            e.Layout.Bands[0].Columns["ToStackCode"].Width = 100;
            e.Layout.Bands[0].Columns["TransQty"].Width = 70;
            e.Layout.Bands[0].Columns["Type"].Width = 20;
            // 设置栏位是否允许编辑，及栏位的显示形式

            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["TransferLine"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["MCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["MDesc"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["McontrolType"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["McontrolTypeName"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["FrmStorageQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["ToStorageQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["PlanQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["AlreadyACTQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["Memo"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["MOCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["ToStorageCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["ToStackCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["ToStackCode"].CellAppearance.BackColor = Color.LawnGreen;
            e.Layout.Bands[0].Columns["TransQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
            e.Layout.Bands[0].Columns["Type"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;

            e.Layout.Bands[0].Columns["ToStackCode"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.EditButton;
            //e.Layout.Bands[0].Columns["ToStorageCode"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
            // 允许筛选
            e.Layout.Bands[0].Columns["TransferLine"].AllowRowFiltering = DefaultableBoolean.True;
            // 允许冻结，且Checked栏位始终处于冻结状态，不可更改          
            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            //e.Layout.Bands[0].Columns["TransferLine"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;

            e.Layout.Bands[0].Columns["TransferLine"].SortIndicator = SortIndicator.Ascending;

            // CheckITEMTrans

            e.Layout.Bands[1].Columns["SERIAL"].Header.Caption = "序号";
            e.Layout.Bands[1].Columns["LOTNO"].Header.Caption = "批号ID";
            e.Layout.Bands[1].Columns["TransferLine"].Header.Caption = "行号";
            e.Layout.Bands[1].Columns["MCode"].Header.Caption = "物料代码";
            e.Layout.Bands[1].Columns["FrmStackCODE"].Header.Caption = "源库位";
            e.Layout.Bands[1].Columns["FrmStorageQty"].Header.Caption = "源库存数量";
            e.Layout.Bands[1].Columns["TransQty"].Header.Caption = "发料数量";
            e.Layout.Bands[1].Columns["Clear"].Header.Caption = "清除";
            e.Layout.Bands[1].Columns["ITEMTrans_Serial"].Header.Caption = "产品序号";
            e.Layout.Bands[1].Columns["Checked"].Header.Caption = "";

            e.Layout.Bands[1].Columns["Type"].Hidden = true;
            e.Layout.Bands[1].Columns["MCode"].Hidden = true;
            e.Layout.Bands[1].Columns["TransferLine"].Hidden = true;

            e.Layout.Bands[1].Columns["MCode"].Width = 100;
            e.Layout.Bands[1].Columns["TransferLine"].Width = 100;
            e.Layout.Bands[1].Columns["SERIAL"].Width = 70;
            e.Layout.Bands[1].Columns["LOTNO"].Width = 150;
            e.Layout.Bands[1].Columns["FrmStackCODE"].Width = 100;
            e.Layout.Bands[1].Columns["FrmStorageQty"].Width = 100;
            e.Layout.Bands[1].Columns["TransQty"].Width = 70;
            e.Layout.Bands[1].Columns["Clear"].Width = 50;
            e.Layout.Bands[1].Columns["Type"].Width = 20;
            e.Layout.Bands[1].Columns["Checked"].Width = 30;

            e.Layout.Bands[1].Columns["SERIAL"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["LOTNO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["FrmStackCODE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["FRMStorageQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["TransQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
            e.Layout.Bands[1].Columns["Clear"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["Clear"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            e.Layout.Bands[1].Columns["Type"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;

            e.Layout.Bands[1].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[1].Columns["SERIAL"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;
            e.Layout.Bands[1].Columns["SERIAL"].AllowRowFiltering = DefaultableBoolean.True;

            e.Layout.Bands[1].Columns["SERIAL"].SortIndicator = SortIndicator.Ascending;

            e.Layout.Bands[1].Columns["ITEMTrans_Serial"].Hidden = true;

            // CheckKPartTrans
            // CheckITEMTrans

            e.Layout.Bands[2].Columns["LOTNO"].Header.Caption = "批号ID";
            e.Layout.Bands[2].Columns["SERIALNO"].Header.Caption = "序列号";
            e.Layout.Bands[2].Columns["ITEMTrans_Serial"].Header.Caption = "产品序号";

            e.Layout.Bands[2].Columns["MCode"].Width = 100;
            e.Layout.Bands[2].Columns["TransferLine"].Width = 100;

            e.Layout.Bands[2].Columns["LOTNO"].Width = 150;
            e.Layout.Bands[2].Columns["SERIALNO"].Width = 200;

            e.Layout.Bands[2].Columns["MCode"].Hidden = true;
            e.Layout.Bands[2].Columns["TransferLine"].Hidden = true;
            e.Layout.Bands[2].Columns["ITEMTrans_Serial"].Hidden = true;
            e.Layout.Bands[2].Columns["LOTNO"].Hidden = true;
            e.Layout.Bands[2].Columns["FrmStackCODE"].Hidden = true;
            //e.Layout.Bands[2].Columns["SERIAL"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[2].Columns["LOTNO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[2].Columns["SERIALNO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;

            e.Layout.Bands[2].Columns["SERIALNO"].Header.Fixed = true;
            e.Layout.Bands[2].Columns["SERIALNO"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;
            e.Layout.Bands[2].Columns["SERIALNO"].AllowRowFiltering = DefaultableBoolean.True;

            //this.InitGridLanguage(this.ultraGridHead);

        }

        private void ucLabelEditTransferNO_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.ClearSampleList();
                this.GetTransferNo();
                this.checkBoxNoFinish.Checked = false;
                this.checkBoxNoFinish.Checked = true;
            }
        }

        private void ucLabelEditInput_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    bool isChecked = false;
                    DataRow[] dr = m_CheckInvTransferDetail.Select("Checked = true");
                    if (dr.Length > 0)
                    {
                        isChecked = true;
                    }
                    string inputNo = this.ucLERunningCard.Value.Trim().ToUpper();

                    #region //输入序列号
                    if (this.opsetPackObject.Value.ToString() == packRcard)
                    {
                        object[] obj = _InventoryFacade.QueryItemLotByStorage(inputNo, m_FromStorageID);
                        //用户输入的是批号
                        if (obj != null)
                        {
                            foreach (ItemLotForTrans itemLot in obj)
                            {
                                object objtemp = _ItemFacade.GetMaterial(itemLot.Mcode, orgID);
                                if (objtemp != null)
                                {
                                    if ((objtemp as Domain.MOModel.Material).MaterialControlType == "item_control_keyparts")
                                    {
                                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Lot_Keyparts"));
                                        this.ucLERunningCard.TextFocus(false, true);
                                        return;
                                    }
                                }
                                if (itemLot.Exdate < FormatHelper.TODateInt(DateTime.Now))
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Material_IS_Invalid"));
                                    this.ucLERunningCard.TextFocus(false, true);
                                    return;
                                }

                                if (itemLot.Active == "N")
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Material_Has_Taboo"));
                                    this.ucLERunningCard.TextFocus(false, true);
                                    return;
                                }

                                if (itemLot.FrmStorageQty == 0)
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Lot_StorgeQty_Is_Zero"));
                                    this.ucLERunningCard.TextFocus(false, true);
                                    return;
                                }

                                //add by kathy @20140626 卡控批次料是否已备过料
                                int minnoCount = _InventoryFacade.QueryMINNOCountByItemLot(itemLot.Mcode, inputNo);
                                if (minnoCount > 0)
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$LotNo ["+inputNo+"] $CS_Lot_In_MINNO"));
                                    this.ucLERunningCard.TextFocus(false, true);
                                    return;
                                }

                                #region//得到母行
                                int rowCode = -1;
                                if (isChecked)
                                {
                                    rowCode = this.GetParentCountInput(itemLot.Mcode);
                                }
                                else
                                {
                                    rowCode = this.GetParentCount(itemLot.Mcode);
                                }
                                if (rowCode == -1)
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ItemCode_Not_Match"));
                                    this.ucLERunningCard.TextFocus(false, true);
                                    return;
                                }
                                #endregion

                                #region//得到当前母行中转移单行号
                                int transferline = int.Parse(this.m_CheckInvTransferDetail.Rows[rowCode]["TransferLine"].ToString());

                                InvTransferDetail invTransferDetail = (InvTransferDetail)_WarehouseFacade.GetInvTransferDetail(m_TransferNO, transferline);
                                if (invTransferDetail != null)
                                {
                                    string status = invTransferDetail.TransferStatus;
                                    if (status == RecordStatus.RecordStatus_CLOSE)
                                    {
                                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Line_Has_Closed"));
                                        this.ucLERunningCard.TextFocus(false, true);
                                        return;
                                    }
                                }
                                else
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_InvTransferDetail_IS_NULL"));
                                    this.ucLERunningCard.TextFocus(false, true);
                                    return;
                                }

                                Messages msg = new Messages();
                                msg = this.CheckDataAlreadyExist(itemLot.Lotno, itemLot.Mcode, itemLot.Stackcode, transferline);
                                if (!msg.IsSuccess())
                                {
                                    ApplicationRun.GetInfoForm().Add(msg);
                                    this.ucLERunningCard.TextFocus(false, true);
                                    return;
                                }


                                decimal transQty = GetTransQty(itemLot.FrmStorageQty, rowCode, itemLot.Mcode);
                                int rowCount = GetRowCount(itemLot.Mcode, transferline);
                                DataRow rowItemLot;
                                rowItemLot = this.m_SampleList.Tables["CheckITEMTrans"].NewRow();
                                rowItemLot["ITEMTrans_Serial"] = rowCount;
                                rowItemLot["TransferLine"] = transferline;
                                rowItemLot["MCode"] = itemLot.Mcode;
                                rowItemLot["LOTNO"] = itemLot.Lotno;
                                rowItemLot["FrmStackCODE"] = itemLot.Stackcode;
                                rowItemLot["SERIAL"] = rowCount;
                                rowItemLot["FrmStorageQty"] = itemLot.FrmStorageQty;
                                rowItemLot["TransQty"] = transQty;
                                rowItemLot["Type"] = "Add";
                                rowItemLot["Checked"] = false;
                                this.m_SampleList.Tables["CheckITEMTrans"].Rows.Add(rowItemLot);
                                this.m_SampleList.Tables["CheckITEMTrans"].AcceptChanges();

                                this.m_CheckInvTransferDetail.Rows[rowCode]["TransQty"] = decimal.Parse(this.m_CheckInvTransferDetail.Rows[rowCode]["TransQty"].ToString()) + transQty;
                                this.m_CheckInvTransferDetail.AcceptChanges();
                                #endregion

                                #region//展开子行
                                foreach (UltraGridRow row in ultraGridHead.Rows)
                                {
                                    if (row.Cells["MCode"].Value.ToString() == itemLot.Mcode && row.Cells["TransferLine"].Value.ToString() == transferline.ToString())
                                    {
                                        row.ExpandAll();
                                    }
                                }
                                #endregion
                            }
                        }
                        //用户输入的是序列号
                        else
                        {
                            object[] objSerialList = _InventoryFacade.QueryItemLotDetail(inputNo, m_FromStorageID);
                            if (objSerialList != null)
                            {
                                ItemLotDetail itemLotBySerialNo = null;
                                foreach (ItemLotDetail objSerial in objSerialList)
                                {
                                    for (int i = 0; i < m_CheckInvTransferDetail.Rows.Count; i++)
                                    {
                                        if (objSerial.Mcode == this.m_CheckInvTransferDetail.Rows[i]["MCode"].ToString())
                                        {
                                            itemLotBySerialNo = objSerial;
                                        }
                                    }
                                }
                                if (itemLotBySerialNo != null)
                                {
                                    ItemLot itemLotSerial = (ItemLot)this._InventoryFacade.GetItemLot(itemLotBySerialNo.Lotno, itemLotBySerialNo.Mcode);
                                    if (itemLotSerial.Exdate < FormatHelper.TODateInt(DateTime.Now))
                                    {
                                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Material_IS_Invalid"));
                                        this.ucLERunningCard.TextFocus(false, true);
                                        return;
                                    }
                                    if (itemLotSerial.Active == "N")
                                    {
                                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Material_Has_Taboo"));
                                        this.ucLERunningCard.TextFocus(false, true);
                                        return;
                                    }

                                    #region//得到母行
                                    int rowCode = -1;
                                    if (isChecked)
                                    {
                                        rowCode = this.GetParentCountInput(itemLotSerial.Mcode);
                                    }
                                    else
                                    {
                                        rowCode = this.GetParentCount(itemLotSerial.Mcode);
                                    }
                                    if (rowCode == -1)
                                    {
                                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ItemCode_Not_Match"));
                                        this.ucLERunningCard.TextFocus(false, true);
                                        return;
                                    }
                                    #endregion

                                    //得到当前母行中转移单行号
                                    int transferline = int.Parse(this.m_CheckInvTransferDetail.Rows[rowCode]["TransferLine"].ToString());

                                    InvTransferDetail invTransferDetail = (InvTransferDetail)_WarehouseFacade.GetInvTransferDetail(m_TransferNO, transferline);
                                    if (invTransferDetail != null)
                                    {
                                        string status = invTransferDetail.TransferStatus;
                                        if (status == RecordStatus.RecordStatus_CLOSE)
                                        {
                                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Line_Has_Closed"));
                                            this.ucLERunningCard.TextFocus(false, true);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_InvTransferDetail_IS_NULL"));
                                        this.ucLERunningCard.TextFocus(false, true);
                                        return;
                                    }
                                    ItemLotDetailForTrans itemLotDetail = (ItemLotDetailForTrans)_InventoryFacade.QueryItemLotDetailByStorage(itemLotBySerialNo.Serialno, itemLotBySerialNo.Mcode);
                                    if (itemLotDetail != null)
                                    {
                                        if (itemLotDetail.FrmStorageQty == 0)
                                        {
                                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Lot_StorgeQty_Is_Zero"));
                                            this.ucLERunningCard.TextFocus(false, true);
                                            return;
                                        }


                                        if (!this.CheckKPartDataAlreadyExist(itemLotDetail.Lotno, itemLotDetail.Mcode, itemLotDetail.Stackcode, transferline, inputNo))
                                        {
                                            decimal transQty = GetTransQty(itemLotDetail.FrmStorageQty, rowCode, itemLotDetail.Mcode);

                                            if (!this.CheckLotDataAlreadyExist(itemLotDetail.Lotno, itemLotDetail.Mcode, itemLotDetail.Stackcode, transferline))
                                            {
                                                //此次操作，批次为新增
                                                DataRow rowItemLotDetail;
                                                rowItemLotDetail = this.m_SampleList.Tables["CheckITEMTrans"].NewRow();
                                                rowItemLotDetail["ITEMTrans_Serial"] = -1;
                                                rowItemLotDetail["TransferLine"] = transferline;
                                                rowItemLotDetail["MCode"] = itemLotDetail.Mcode;
                                                rowItemLotDetail["LOTNO"] = itemLotDetail.Lotno;
                                                rowItemLotDetail["SERIAL"] = GetRowCount(itemLotDetail.Mcode, transferline);
                                                rowItemLotDetail["FrmStackCODE"] = itemLotDetail.Stackcode;
                                                rowItemLotDetail["FrmStorageQty"] = itemLotDetail.FrmStorageQty;
                                                rowItemLotDetail["TransQty"] = 0;
                                                rowItemLotDetail["Type"] = "Add";
                                                rowItemLotDetail["Checked"] = false;

                                                this.m_SampleList.Tables["CheckITEMTrans"].Rows.Add(rowItemLotDetail);
                                                this.m_SampleList.Tables["CheckITEMTrans"].AcceptChanges();
                                                this.m_CheckInvTransferDetail.Rows[rowCode]["TransQty"] = decimal.Parse(this.m_CheckInvTransferDetail.Rows[rowCode]["TransQty"].ToString()) + transQty;
                                                this.m_CheckInvTransferDetail.AcceptChanges();
                                                //展开子行
                                                foreach (UltraGridRow row in ultraGridHead.Rows)
                                                {
                                                    if (row.Cells["MCode"].Value.ToString() == itemLotDetail.Mcode && row.Cells["TransferLine"].Value.ToString() == transferline.ToString())
                                                    {
                                                        row.ExpandAll();
                                                    }
                                                }
                                            }

                                            if (!this.CheckKPartDataAlreadyExist(itemLotDetail.Lotno, itemLotDetail.Mcode, itemLotDetail.Stackcode, transferline, inputNo))
                                            {
                                                //  add by andy xin 2010-12-7
                                                DataRow rowKPartDetail;
                                                rowKPartDetail = this.m_SampleList.Tables["CheckKPartTrans"].NewRow();
                                                rowKPartDetail["ITEMTrans_Serial"] = -1;
                                                rowKPartDetail["SERIALNO"] = inputNo;
                                                rowKPartDetail["TransferLine"] = transferline;
                                                rowKPartDetail["MCode"] = itemLotDetail.Mcode;
                                                rowKPartDetail["LOTNO"] = itemLotDetail.Lotno;
                                                rowKPartDetail["FrmStackCODE"] = itemLotDetail.Stackcode;
                                                this.m_SampleList.Tables["CheckKPartTrans"].Rows.Add(rowKPartDetail);

                                                this.m_SampleList.Tables["CheckKPartTrans"].AcceptChanges();
                                                //// decimal.Parse(this.m_CheckInvTransferDetail.Rows[rowCode]["TransQty"].ToString()) + transQty;

                                                DataRow[] drsLotNo = this.m_SampleList.Tables["CheckITEMTrans"].Select("ITEMTrans_Serial = -1 And TransferLine='" + transferline + "' And MCode='" + itemLotDetail.Mcode + "' And LOTNO='" + itemLotDetail.Lotno + "' And FrmStackCODE='" + itemLotDetail.Stackcode + "'");
                                                DataRow[] drsKPart = this.m_SampleList.Tables["CheckKPartTrans"].Select("ITEMTrans_Serial = -1 And TransferLine='" + transferline + "' And MCode='" + itemLotDetail.Mcode + "' And LOTNO='" + itemLotDetail.Lotno + "' And FrmStackCODE='" + itemLotDetail.Stackcode + "'");

                                                if (drsLotNo != null)
                                                {
                                                    if (drsKPart != null)
                                                    {
                                                        drsLotNo[0]["TransQty"] = drsKPart.Length;
                                                        this.m_CheckInvTransferDetail.Rows[rowCode]["TransQty"] = this.GetTransQty(itemLotDetail.Mcode, transferline);
                                                    }
                                                }

                                                this.m_SampleList.Tables["CheckITEMTrans"].AcceptChanges();
                                                this.m_CheckInvTransferDetail.AcceptChanges();
                                            }
                                        }
                                        else
                                        {
                                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, itemLotDetail.Serialno + "$ERROR_DATA_ALREADY_EXIST"));
                                            this.ucLERunningCard.TextFocus(false, true);
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ItemCode_Not_Match"));
                                    this.ucLERunningCard.TextFocus(false, true);
                                    return;
                                }
                            }
                            else
                            {
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SN_NOT_EXIST_LotNo"));
                                this.ucLERunningCard.TextFocus(false, true);
                                return;
                            }
                        }
                    }
                    #endregion
                    #region//输入栈板号
                    else if (this.opsetPackObject.Value.ToString() == packPallet)
                    {
                        PackageFacade _packageFacade = new PackageFacade(this.DataProvider);
                        object[] pallet2RCards = _packageFacade.GetPallet2RCardListByPallet(inputNo);

                        if (pallet2RCards != null)
                        {
                            foreach (BenQGuru.eMES.Domain.Package.Pallet2RCard pallet2RCard in pallet2RCards)
                            {
                                object[] objSerialList = _InventoryFacade.QueryItemLotDetail(pallet2RCard.RCard, m_FromStorageID);
                                if (objSerialList != null)
                                {
                                    ItemLotDetail itemLotBySerialNo = null;
                                    foreach (ItemLotDetail objSerial in objSerialList)
                                    {
                                        for (int i = 0; i < m_CheckInvTransferDetail.Rows.Count; i++)
                                        {
                                            if (objSerial.Mcode == this.m_CheckInvTransferDetail.Rows[i]["MCode"].ToString())
                                            {
                                                itemLotBySerialNo = objSerial;
                                            }
                                        }
                                    }
                                    if (itemLotBySerialNo != null)
                                    {
                                        ItemLot itemLotSerial = (ItemLot)this._InventoryFacade.GetItemLot(itemLotBySerialNo.Lotno, itemLotBySerialNo.Mcode);

                                        if (itemLotSerial.Exdate < FormatHelper.TODateInt(DateTime.Now))
                                        {
                                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Material_IS_Invalid"));
                                            this.ucLERunningCard.TextFocus(false, true);
                                            return;
                                        }

                                        if (itemLotSerial.Active == "N")
                                        {
                                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Material_Has_Taboo"));
                                            this.ucLERunningCard.TextFocus(false, true);
                                            continue;
                                        }
                                        //得到母行
                                        int rowCode = -1;
                                        if (isChecked)
                                        {
                                            rowCode = this.GetParentCountInput(itemLotSerial.Mcode);
                                        }
                                        else
                                        {
                                            rowCode = this.GetParentCount(itemLotSerial.Mcode);
                                        }
                                        if (rowCode == -1)
                                        {
                                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ItemCode_Not_Match"));
                                            this.ucLERunningCard.TextFocus(false, true);
                                            continue;
                                        }
                                        //得到当前母行中转移单行号
                                        int transferline = int.Parse(this.m_CheckInvTransferDetail.Rows[rowCode]["TransferLine"].ToString());

                                        InvTransferDetail invTransferDetail = (InvTransferDetail)_WarehouseFacade.GetInvTransferDetail(m_TransferNO, transferline);
                                        if (invTransferDetail != null)
                                        {
                                            string status = invTransferDetail.TransferStatus;
                                            if (status == RecordStatus.RecordStatus_CLOSE)
                                            {
                                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Line_Has_Closed"));
                                                this.ucLERunningCard.TextFocus(false, true);
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_InvTransferDetail_IS_NULL"));
                                            this.ucLERunningCard.TextFocus(false, true);
                                            continue;
                                        }
                                        ItemLotDetailForTrans itemLotDetail = (ItemLotDetailForTrans)_InventoryFacade.QueryItemLotDetailByStorage(itemLotBySerialNo.Serialno, itemLotBySerialNo.Mcode);
                                        if (itemLotDetail != null)
                                        {
                                            if (itemLotDetail.FrmStorageQty == 0)
                                            {
                                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Lot_StorgeQty_Is_Zero"));
                                                this.ucLERunningCard.TextFocus(false, true);
                                                continue;
                                            }


                                            if (!this.CheckKPartDataAlreadyExist(itemLotDetail.Lotno, itemLotDetail.Mcode, itemLotDetail.Stackcode, transferline, pallet2RCard.RCard))
                                            {
                                                decimal transQty = GetTransQty(itemLotDetail.FrmStorageQty, rowCode, itemLotDetail.Mcode);

                                                if (!this.CheckLotDataAlreadyExist(itemLotDetail.Lotno, itemLotDetail.Mcode, itemLotDetail.Stackcode, transferline))
                                                {

                                                    DataRow rowItemLotDetail;
                                                    rowItemLotDetail = this.m_SampleList.Tables["CheckITEMTrans"].NewRow();
                                                    rowItemLotDetail["ITEMTrans_Serial"] = -1;
                                                    rowItemLotDetail["TransferLine"] = transferline;
                                                    rowItemLotDetail["MCode"] = itemLotDetail.Mcode;
                                                    rowItemLotDetail["LOTNO"] = itemLotDetail.Lotno;
                                                    rowItemLotDetail["SERIAL"] = GetRowCount(itemLotDetail.Mcode, transferline);
                                                    rowItemLotDetail["FrmStackCODE"] = itemLotDetail.Stackcode;
                                                    rowItemLotDetail["FrmStorageQty"] = itemLotDetail.FrmStorageQty;
                                                    rowItemLotDetail["TransQty"] = 0;
                                                    rowItemLotDetail["Type"] = "Add";
                                                    rowItemLotDetail["Checked"] = false;

                                                    this.m_SampleList.Tables["CheckITEMTrans"].Rows.Add(rowItemLotDetail);
                                                    this.m_SampleList.Tables["CheckITEMTrans"].AcceptChanges();
                                                    this.m_CheckInvTransferDetail.Rows[rowCode]["TransQty"] = decimal.Parse(this.m_CheckInvTransferDetail.Rows[rowCode]["TransQty"].ToString()) + transQty;
                                                    this.m_CheckInvTransferDetail.AcceptChanges();
                                                    //展开子行
                                                    foreach (UltraGridRow row in ultraGridHead.Rows)
                                                    {
                                                        if (row.Cells["MCode"].Value.ToString() == itemLotDetail.Mcode && row.Cells["TransferLine"].Value.ToString() == transferline.ToString())
                                                        {
                                                            row.ExpandAll();
                                                        }
                                                    }

                                                }

                                                if (!this.CheckKPartDataAlreadyExist(itemLotDetail.Lotno, itemLotDetail.Mcode, itemLotDetail.Stackcode, transferline, pallet2RCard.RCard))
                                                {


                                                    //// add by andy xin 2010-12-7
                                                    DataRow rowKPartDetail;
                                                    rowKPartDetail = this.m_SampleList.Tables["CheckKPartTrans"].NewRow();
                                                    rowKPartDetail["ITEMTrans_Serial"] = -1;
                                                    rowKPartDetail["SERIALNO"] = pallet2RCard.RCard;
                                                    rowKPartDetail["TransferLine"] = transferline;
                                                    rowKPartDetail["MCode"] = itemLotDetail.Mcode;
                                                    rowKPartDetail["LOTNO"] = itemLotDetail.Lotno;
                                                    rowKPartDetail["FrmStackCODE"] = itemLotDetail.Stackcode;
                                                    this.m_SampleList.Tables["CheckKPartTrans"].Rows.Add(rowKPartDetail);

                                                    this.m_SampleList.Tables["CheckKPartTrans"].AcceptChanges();
                                                    //// decimal.Parse(this.m_CheckInvTransferDetail.Rows[rowCode]["TransQty"].ToString()) + transQty;

                                                    DataRow[] drsLotNo = this.m_SampleList.Tables["CheckITEMTrans"].Select("ITEMTrans_Serial = -1 And TransferLine='" + transferline + "' And ItemCode='" + itemLotDetail.Mcode + "' And LOTNO='" + itemLotDetail.Lotno + "' And FrmStackCODE='" + itemLotDetail.Stackcode + "'");
                                                    DataRow[] drsKPart = this.m_SampleList.Tables["CheckKPartTrans"].Select("ITEMTrans_Serial = -1 And TransferLine='" + transferline + "' And ItemCode='" + itemLotDetail.Mcode + "' And LOTNO='" + itemLotDetail.Lotno + "' And FrmStackCODE='" + itemLotDetail.Stackcode + "'");

                                                    if (drsLotNo != null)
                                                    {
                                                        if (drsKPart != null)
                                                        {
                                                            drsLotNo[0]["TransQty"] = drsKPart.Length;
                                                            this.m_CheckInvTransferDetail.Rows[rowCode]["TransQty"] = this.GetTransQty(itemLotDetail.Mcode, transferline);
                                                        }
                                                    }

                                                    this.m_SampleList.Tables["CheckITEMTrans"].AcceptChanges();
                                                    this.m_CheckInvTransferDetail.AcceptChanges();
                                                }
                                            }
                                            else
                                            {
                                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, itemLotDetail.Serialno + "$ERROR_DATA_ALREADY_EXIST"));
                                                continue;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ItemCode_Not_Match"));
                                        this.ucLERunningCard.TextFocus(false, true);
                                        continue;
                                    }

                                }
                                else
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SN_NOT_EXIST_LotNo"));
                                    this.ucLERunningCard.TextFocus(false, true);
                                    continue;
                                }
                            }
                        }
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));
                    this.ucLERunningCard.TextFocus(false, true);
                    return;
                }
                this.ucLERunningCard.TextFocus(true, true);
            }
        }


        private void ucButtonSend_Click(object sender, EventArgs e)
        {
            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            if (!ValidateInput())
            {
                return;
            }

            //获取业务类型
            string businesscode = FormatString(ucLabelComboxType.SelectedItemValue);
            InvBusiness invBusiness = (InvBusiness)this._InventoryFacade.GetInvBusiness(businesscode, orgID);
            if (invBusiness == null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_BusinessCode_Not_Found"));
                return;
            }
            bool saveData = true;

            try
            {
                this.DataProvider.BeginTransaction();
                //获得新增子行数据
                DataRow[] itemTransSelect = m_CheckITEMTrans.Select("Type='Add'");

                //检查输入的数据
                int transCount = 0;

                foreach (DataRow dataRowCheck in itemTransSelect)
                {

                    if (decimal.Parse(dataRowCheck["TransQty"].ToString()) <= 0)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_TransQty_Must_Bigger_Zero"));
                        this.DataProvider.RollbackTransaction();
                        saveData = false;
                        return;
                    }
                    transCount += 1;

                }
                for (int i = 0; i < m_CheckInvTransferDetail.Rows.Count; i++)
                {
                    if (decimal.Parse(m_CheckInvTransferDetail.Rows[i]["TransQty"].ToString()) > 0)
                        transCount += 1;
                }
                if (transCount == 0)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Not_Found_TransferData"));
                    this.DataProvider.RollbackTransaction();
                    saveData = false;
                    return;
                }
                #region 发料/转移处理
                foreach (UltraGridRow row in ultraGridHead.Rows)
                {
                    //非管控料处理
                    if (row.Cells["McontrolType"].Value.ToString().ToLower() == BOMItemControlType.ITEM_CONTROL_NOCONTROL)
                    {
                        if (row.Cells["Checked"].Value.ToString().ToUpper() == "TRUE")
                        {
                            decimal lotSendNumber = row.Cells["TransQty"].Value.ToString() == string.Empty ? 0 : Convert.ToDecimal(row.Cells["TransQty"].Value.ToString());
                            string itemCode = row.Cells["MCode"].Value.ToString();
                            int transLine = int.Parse(row.Cells["TransferLine"].Value.ToString());
                            string Tostackcode = row.Cells["ToStackCode"].Value.ToString();
                            int transferline = int.Parse(row.Cells["TransferLine"].Value.ToString());


                            //判断目的库别不能为空 
                            if (row.Cells["ToStorageCode"].Value.ToString().Equals(string.Empty))
                            {
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ToStorageCode_Is_Null"));
                                this.DataProvider.RollbackTransaction();
                                saveData = false;
                                break;
                            }
                            //end add

                            //判断目的库位不能为空                           
                            if (Tostackcode == string.Empty)
                            {
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ItemCode:" + itemCode + "$CS_TransLine:" + transLine + "$CS_ToStackCode_IS_Null"));
                                this.DataProvider.RollbackTransaction();
                                saveData = false;
                                break;
                            }
                            //end modify 

                            //超发检查
                            //主行中：发料数量必须<=计划数量-已发数量。发料数量必须<=源库存数量。                            
                            decimal sendCount = decimal.Parse(row.Cells["TransQty"].Value.ToString());
                            decimal planCount = decimal.Parse(row.Cells["PlanQty"].Value.ToString());
                            decimal sentCount = decimal.Parse(row.Cells["AlreadyACTQty"].Value.ToString());
                            if (sendCount > decimal.Parse(row.Cells["FrmStorageQty"].Value.ToString()))
                            {
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ItemCode:" + itemCode + "$CS_SendMaterialLot_Not_Enough"));
                                this.DataProvider.RollbackTransaction();
                                saveData = false;
                                break;
                            }
                            #region 更新
                            //更新源库存TBLStorageInfo
                            decimal totalTemp = lotSendNumber;
                            object[] fromStorageInfos = this._InventoryFacade.QueryStorageInfoByIDAndMCode(m_FromStorageID, itemCode);
                            if (fromStorageInfos != null)
                            {
                                InvTransfer invTransfer = (InvTransfer)this._WarehouseFacade.GetInvTransfer(m_TransferNO);

                                foreach (StorageInfo fromStorageInfo in fromStorageInfos)
                                {
                                    if (totalTemp >= fromStorageInfo.Storageqty)
                                    {
                                        totalTemp = totalTemp - fromStorageInfo.Storageqty;

                                        fromStorageInfo.Storageqty = 0;
                                        this._InventoryFacade.DeleteStorageInfo(fromStorageInfo);
                                    }
                                    else
                                    {
                                        fromStorageInfo.Storageqty = fromStorageInfo.Storageqty - totalTemp;
                                        fromStorageInfo.Muser = ApplicationService.Current().UserCode;
                                        fromStorageInfo.Mdate = dBDateTime.DBDate;
                                        fromStorageInfo.Mtime = dBDateTime.DBTime;
                                        this._InventoryFacade.UpdateStorageInfo(fromStorageInfo);


                                    }



                                    //新增TBLITEMTrans                            
                                    ItemTrans itemTrans = new ItemTrans();
                                    //itemTrans.Serial = serial;
                                    itemTrans.Transno = m_TransferNO;
                                    itemTrans.Transline = transferline;
                                    itemTrans.Itemcode = itemCode;
                                    itemTrans.Frmstorageid = m_FromStorageID;
                                    itemTrans.Frmstackcode = fromStorageInfo.Stackcode;
                                    itemTrans.Tostorageid = this.m_ToStorageID;
                                    itemTrans.Tostackcode = fromStorageInfo.Stackcode;
                                    if (fromStorageInfo.Storageqty != 0)
                                    {
                                        itemTrans.Transqty = totalTemp;
                                    }
                                    else
                                    {
                                        itemTrans.Transqty = fromStorageInfo.Storageqty;
                                    }
                                    itemTrans.Transtype = IssueType.IssueType_Issue;
                                    itemTrans.Memo = this.ucLabelEditMemo.Value.Trim().ToString();
                                    itemTrans.Businesscode = invBusiness.BusinessCode;
                                    itemTrans.Orgid = invTransfer.OrgID;
                                    itemTrans.Muser = ApplicationService.Current().UserCode;
                                    itemTrans.Mdate = dBDateTime.DBDate;
                                    itemTrans.Mtime = dBDateTime.DBTime;
                                    this._InventoryFacade.AddItemTrans(itemTrans);

                                }


                                //更新目的库存TBLStorageInfo
                                StorageInfo toStorageInfo = (StorageInfo)this._InventoryFacade.GetStorageInfo(m_ToStorageID, itemCode, Tostackcode);
                                if (toStorageInfo != null)
                                {
                                    toStorageInfo.Storageqty = toStorageInfo.Storageqty + lotSendNumber;
                                    toStorageInfo.Muser = ApplicationService.Current().UserCode;
                                    toStorageInfo.Mdate = dBDateTime.DBDate;
                                    toStorageInfo.Mtime = dBDateTime.DBTime;
                                    this._InventoryFacade.UpdateStorageInfo(toStorageInfo);
                                }
                                else
                                {
                                    if (m_ToStorageID != string.Empty)
                                    {
                                        toStorageInfo = new StorageInfo();
                                        toStorageInfo.Storageid = m_ToStorageID;
                                        toStorageInfo.Mcode = itemCode;
                                        toStorageInfo.Stackcode = Tostackcode;
                                        toStorageInfo.Storageqty = lotSendNumber;
                                        toStorageInfo.Muser = ApplicationService.Current().UserCode;
                                        toStorageInfo.Mdate = dBDateTime.DBDate;
                                        toStorageInfo.Mtime = dBDateTime.DBTime;
                                        this._InventoryFacade.AddStorageInfo(toStorageInfo);
                                    }
                                }



                                //更新TBLInvTransferDetail, TBLInvTransfer                
                                InvTransferDetail invTransferDetail = (InvTransferDetail)this._WarehouseFacade.GetInvTransferDetail(m_TransferNO, transferline);
                                if (sentCount + sendCount >= planCount)
                                {
                                    invTransferDetail.Actqty = sentCount + sendCount;
                                    invTransferDetail.TransferStatus = RecordStatus.RecordStatus_CLOSE;
                                    invTransferDetail.TransferUser = ApplicationService.Current().UserCode;
                                    invTransferDetail.TransferDate = dBDateTime.DBDate;
                                    invTransferDetail.TransferTime = dBDateTime.DBTime;
                                    this._WarehouseFacade.UpdateInvTransferDetail(invTransferDetail);
                                }
                                else
                                {
                                    invTransferDetail.Actqty = sentCount + sendCount;
                                    invTransferDetail.TransferStatus = RecordStatus.RecordStatus_USING;
                                    invTransferDetail.TransferUser = ApplicationService.Current().UserCode;
                                    invTransferDetail.TransferDate = dBDateTime.DBDate;
                                    invTransferDetail.TransferTime = dBDateTime.DBTime;
                                    this._WarehouseFacade.UpdateInvTransferDetail(invTransferDetail);
                                }

                                //更新tblinvtransfer.TransferStatus栏位状态                        
                                int closeCount = this._WarehouseFacade.GetInvTransferDetailCount(invTransfer.TransferNO, RecordStatus.RecordStatus_CLOSE);
                                if (closeCount == 0)
                                {
                                    invTransfer.TransferStatus = RecordStatus.RecordStatus_CLOSE;
                                    this._WarehouseFacade.UpdateInvTransfer(invTransfer);
                                }
                                //add by vivian.sun 2011-4-28 【当一个行项目tblinvtransferdetail变为RecordStatus_CLOSE时，
                                //如果不是所有的行项目都RecordStatus_CLOSE，则tblinvtransfer的状态设置为RecordStatus_USING】
                                else
                                {
                                    invTransfer.TransferStatus = RecordStatus.RecordStatus_USING;
                                    this._WarehouseFacade.UpdateInvTransfer(invTransfer);
                                }
                                //end add
                            #endregion
                            }
                        }
                    }
                    //批管控料及单件管控料处理
                    if (row.Cells["McontrolType"].Value.ToString().ToLower() == BOMItemControlType.ITEM_CONTROL_KEYPARTS || row.Cells["McontrolType"].Value.ToString().ToLower() == BOMItemControlType.ITEM_CONTROL_LOT)
                    {
                        foreach (DataRow dataRowItemTrans in itemTransSelect)
                        {
                            if ((dataRowItemTrans["Type"].ToString() == "Add") && (dataRowItemTrans["TransferLine"].ToString() == row.Cells["TransferLine"].Value.ToString())
                                && (dataRowItemTrans["MCode"].ToString() == row.Cells["MCode"].Value.ToString()))
                            {
                                decimal lotSendNumber = dataRowItemTrans["TransQty"].ToString() == string.Empty ? 0 : Convert.ToDecimal(dataRowItemTrans["TransQty"].ToString());
                                string itemCode = dataRowItemTrans["MCode"].ToString();
                                int transLine = int.Parse(dataRowItemTrans["TransferLine"].ToString());
                                string lotNo = dataRowItemTrans["LOTNO"].ToString();
                                string stackCode = dataRowItemTrans["FrmStackCODE"].ToString();

                                //检查同一批号加入不同行号中的发料数量                        
                                DataRow[] lotQty = m_CheckITEMTrans.Select(string.Format("MCode='{0}' and LOTNO='{1}' and FrmStackCODE ='{2}' and Type='Add' ", itemCode, lotNo, stackCode));
                                if (lotQty.Length > 0)
                                {
                                    decimal transSendCount = 0;
                                    foreach (DataRow dataRow in lotQty)
                                    {
                                        transSendCount += decimal.Parse(dataRow["TransQty"].ToString());
                                    }
                                    if (transSendCount > decimal.Parse(dataRowItemTrans["FrmStorageQty"].ToString()))
                                    {
                                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LotNo:" + lotNo + "$CS_SendMaterialLot_OverFlow"));
                                        this.DataProvider.RollbackTransaction();
                                        saveData = false;
                                        break;
                                    }
                                }


                                int rowCode = this.GetParentCount(itemCode, transLine);
                                if (rowCode == -1)
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Not_Found_ParentRow"));
                                    this.DataProvider.RollbackTransaction();
                                    saveData = false;
                                    break;
                                }

                                //判断目的库别不能为空
                                if (this.m_CheckInvTransferDetail.Rows[rowCode]["ToStorageCode"].ToString().Equals(string.Empty))
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ToStorageCode_Is_Null"));
                                    this.DataProvider.RollbackTransaction();
                                    saveData = false;
                                    break;
                                }

                                //判断目的库位不能为空
                                string Tostackcode = this.GetToStackCode(rowCode);
                                if (Tostackcode == string.Empty)
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ItemCode:" + itemCode + "$CS_TransLine:" + transLine + "$CS_ToStackCode_IS_Null"));
                                    this.DataProvider.RollbackTransaction();
                                    saveData = false;
                                    break;
                                }

                                //int lineCode = this.GetLineCode(rowCode);
                                int transferline = int.Parse(this.m_CheckInvTransferDetail.Rows[rowCode]["TransferLine"].ToString());
                                ItemLot itemLot = (ItemLot)this._InventoryFacade.GetItemLot(lotNo, itemCode);

                                //FIFO检查
                                if (invBusiness != null && invBusiness.ISFIFO == FIFOFlag.FIFOFlag_Y)
                                {
                                    if (!CheckFiFo(itemCode, itemLot.Vendorcode, itemLot.Datecode, itemLot.Lotno))
                                    {
                                        this.DataProvider.RollbackTransaction();
                                        saveData = false;
                                        break;
                                    }
                                }

                                //超发检查
                                //主行中：发料数量必须<=计划数量-已发数量。发料数量必须<=源库存数量。
                                //子行中：发料数量必须<=源库存数量。
                                decimal sendCount = decimal.Parse(m_CheckInvTransferDetail.Rows[rowCode]["TransQty"].ToString());
                                decimal planCount = decimal.Parse(m_CheckInvTransferDetail.Rows[rowCode]["PlanQty"].ToString());
                                decimal sentCount = decimal.Parse(m_CheckInvTransferDetail.Rows[rowCode]["AlreadyACTQty"].ToString());
                                if (sendCount > decimal.Parse(m_CheckInvTransferDetail.Rows[rowCode]["FrmStorageQty"].ToString()) ||
                                    lotSendNumber > decimal.Parse(dataRowItemTrans["FrmStorageQty"].ToString()))
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ItemCode:" + itemCode + "$CS_SendMaterialLot_Not_Enough"));
                                    this.DataProvider.RollbackTransaction();
                                    saveData = false;
                                    break;
                                }

                                #region 更新
                                //更新源库存TBLStorageInfo
                                StorageInfo storageInfo = (StorageInfo)this._InventoryFacade.GetStorageInfo(m_FromStorageID, itemCode, stackCode);
                                if (storageInfo != null)
                                {
                                    storageInfo.Storageqty = storageInfo.Storageqty - lotSendNumber;
                                    storageInfo.Muser = ApplicationService.Current().UserCode;
                                    storageInfo.Mdate = dBDateTime.DBDate;
                                    storageInfo.Mtime = dBDateTime.DBTime;
                                    this._InventoryFacade.UpdateStorageInfo(storageInfo);

                                    //当原库存数量为零时，删除此笔数据
                                    if (storageInfo.Storageqty == 0)
                                    {
                                        this._InventoryFacade.DeleteStorageInfo(storageInfo);
                                    }
                                }

                                //更新目的库存TBLStorageInfo
                                StorageInfo toStorageInfo = (StorageInfo)this._InventoryFacade.GetStorageInfo(m_ToStorageID, itemCode, Tostackcode);
                                if (toStorageInfo != null)
                                {
                                    toStorageInfo.Storageqty = toStorageInfo.Storageqty + lotSendNumber;
                                    toStorageInfo.Muser = ApplicationService.Current().UserCode;
                                    toStorageInfo.Mdate = dBDateTime.DBDate;
                                    toStorageInfo.Mtime = dBDateTime.DBTime;
                                    this._InventoryFacade.UpdateStorageInfo(toStorageInfo);
                                }
                                else
                                {
                                    if (m_ToStorageID != string.Empty)
                                    {
                                        toStorageInfo = new StorageInfo();
                                        toStorageInfo.Storageid = m_ToStorageID;
                                        toStorageInfo.Mcode = itemCode;
                                        toStorageInfo.Stackcode = Tostackcode;
                                        toStorageInfo.Storageqty = lotSendNumber;
                                        toStorageInfo.Muser = ApplicationService.Current().UserCode;
                                        toStorageInfo.Mdate = dBDateTime.DBDate;
                                        toStorageInfo.Mtime = dBDateTime.DBTime;
                                        this._InventoryFacade.AddStorageInfo(toStorageInfo);
                                    }
                                }

                                //更新源库存TBLStorageLotInfo
                                StorageLotInfo storageLotInfo = (StorageLotInfo)this._InventoryFacade.GetStorageLotInfo(lotNo, m_FromStorageID, stackCode, itemCode);
                                if (storageLotInfo != null)
                                {
                                    storageLotInfo.Lotqty = storageLotInfo.Lotqty - lotSendNumber;
                                    storageLotInfo.Muser = ApplicationService.Current().UserCode;
                                    storageLotInfo.Mdate = dBDateTime.DBDate;
                                    storageLotInfo.Mtime = dBDateTime.DBTime;
                                    this._InventoryFacade.UpdateStorageLotInfo(storageLotInfo);

                                    //当原库存数量为零时，删除此笔数据
                                    if (storageLotInfo.Lotqty == 0)
                                    {
                                        this._InventoryFacade.DeleteStorageLotInfo(storageLotInfo);
                                    }
                                }

                                //更新目的库存TBLStorageLotInfo
                                StorageLotInfo toStorageLotInfo = (StorageLotInfo)this._InventoryFacade.GetStorageLotInfo(lotNo, m_ToStorageID, Tostackcode, itemCode);
                                if (toStorageLotInfo != null)
                                {
                                    toStorageLotInfo.Lotqty = toStorageLotInfo.Lotqty + lotSendNumber;
                                    toStorageLotInfo.Muser = ApplicationService.Current().UserCode;
                                    toStorageLotInfo.Mdate = dBDateTime.DBDate;
                                    toStorageLotInfo.Mtime = dBDateTime.DBTime;
                                    this._InventoryFacade.UpdateStorageLotInfo(toStorageLotInfo);
                                }
                                else
                                {
                                    if (m_ToStorageID != string.Empty)
                                    {
                                        toStorageLotInfo = new StorageLotInfo();
                                        toStorageLotInfo.Lotno = lotNo;
                                        toStorageLotInfo.Storageid = m_ToStorageID;
                                        toStorageLotInfo.Mcode = itemCode;
                                        toStorageLotInfo.Stackcode = Tostackcode;
                                        toStorageLotInfo.Lotqty = lotSendNumber;
                                        toStorageLotInfo.Muser = ApplicationService.Current().UserCode;
                                        toStorageLotInfo.Receivedate = dBDateTime.DBDate;
                                        toStorageLotInfo.Mdate = dBDateTime.DBDate;
                                        toStorageLotInfo.Mtime = dBDateTime.DBTime;
                                        this._InventoryFacade.AddStorageLotInfo(toStorageLotInfo);
                                    }
                                }

                                //取得序号
                                //int serial = this._InventoryFacade.Maxserial();

                                //新增TBLITEMTrans

                                ItemTrans itemTrans = new ItemTrans();
                                //itemTrans.Serial = serial;
                                itemTrans.Transno = m_TransferNO;
                                itemTrans.Transline = transferline;
                                itemTrans.Itemcode = itemCode;
                                itemTrans.Frmstorageid = m_FromStorageID;
                                itemTrans.Frmstackcode = dataRowItemTrans["FrmStackCODE"].ToString();
                                itemTrans.Tostorageid = this.m_ToStorageID;
                                itemTrans.Tostackcode = Tostackcode;
                                itemTrans.Transqty = lotSendNumber;
                                itemTrans.Transtype = IssueType.IssueType_Issue;
                                itemTrans.Memo = this.ucLabelEditMemo.Value.Trim().ToString();
                                itemTrans.Businesscode = invBusiness.BusinessCode;
                                itemTrans.Orgid = itemLot.Orgid;
                                itemTrans.Muser = ApplicationService.Current().UserCode;
                                itemTrans.Mdate = dBDateTime.DBDate;
                                itemTrans.Mtime = dBDateTime.DBTime;
                                this._InventoryFacade.AddItemTrans(itemTrans);

                                int serial = _InventoryFacade.Maxserial(m_TransferNO);

                                //新增TBLITEMTransLot
                                ItemTransLot itemTransLot = new ItemTransLot();
                                itemTransLot.Tblitemtrans_serial = serial;
                                itemTransLot.Lotno = lotNo;
                                itemTransLot.Itemcode = itemCode;
                                itemTransLot.Transqty = lotSendNumber;
                                itemTransLot.Memo = this.ucLabelEditMemo.Value.Trim().ToString();
                                itemTransLot.Muser = ApplicationService.Current().UserCode;
                                itemTransLot.Mdate = dBDateTime.DBDate;
                                itemTransLot.Mtime = dBDateTime.DBTime;
                                this._InventoryFacade.AddItemTransLot(itemTransLot);

                                //新增TBLITEMTransLotDetail add by andy xin 2010-12-7
                                DataRow[] drsKPart = this.m_SampleList.Tables["CheckKPartTrans"].Select("ITEMTrans_Serial = -1 And TransferLine='" + transferline + "' And MCode='" + itemCode + "' And LOTNO='" + lotNo + "' And FrmStackCODE='" + dataRowItemTrans["FrmStackCODE"].ToString() + "'");
                                foreach (DataRow dr in drsKPart)
                                {
                                    ItemTransLotDetail itemTransLotDetail = new ItemTransLotDetail();
                                    itemTransLotDetail.Tblitemtrans_serial = serial;
                                    itemTransLotDetail.Serialno = dr["SERIALNO"].ToString();
                                    itemTransLotDetail.Lotno = lotNo;
                                    itemTransLotDetail.Itemcode = itemCode;
                                    itemTransLotDetail.Muser = ApplicationService.Current().UserCode;
                                    itemTransLotDetail.Mdate = dBDateTime.DBDate;
                                    itemTransLotDetail.Mtime = dBDateTime.DBTime;
                                    this._InventoryFacade.AddItemTransLotDetail(itemTransLotDetail);
                                }

                                //更新TblItemLotDetail add by andy xin 2010-12-8
                                foreach (DataRow dr in drsKPart)
                                {
                                    object objItemLotDetail = _InventoryFacade.GetItemLotDetail(dr["SERIALNO"].ToString(), itemCode);
                                    if (objItemLotDetail != null)
                                    {
                                        (objItemLotDetail as ItemLotDetail).Storageid = this.m_ToStorageID;
                                        (objItemLotDetail as ItemLotDetail).Stackcode = Tostackcode;
                                        _InventoryFacade.UpdateItemLotDetail((objItemLotDetail as ItemLotDetail));
                                    }
                                }


                                //更新TBLInvTransferDetail, TBLInvTransfer                
                                InvTransferDetail invTransferDetail = (InvTransferDetail)this._WarehouseFacade.GetInvTransferDetail(m_TransferNO, transferline);
                                InvTransfer invTransfer = (InvTransfer)this._WarehouseFacade.GetInvTransfer(m_TransferNO);
                                if (sentCount + sendCount >= planCount)
                                {
                                    invTransferDetail.Actqty = sentCount + sendCount;
                                    invTransferDetail.TransferStatus = RecordStatus.RecordStatus_CLOSE;
                                    invTransferDetail.TransferUser = ApplicationService.Current().UserCode;
                                    invTransferDetail.TransferDate = dBDateTime.DBDate;
                                    invTransferDetail.TransferTime = dBDateTime.DBTime;
                                    this._WarehouseFacade.UpdateInvTransferDetail(invTransferDetail);
                                }
                                else
                                {
                                    invTransferDetail.Actqty = sentCount + sendCount;
                                    invTransferDetail.TransferStatus = RecordStatus.RecordStatus_USING;
                                    invTransferDetail.TransferUser = ApplicationService.Current().UserCode;
                                    invTransferDetail.TransferDate = dBDateTime.DBDate;
                                    invTransferDetail.TransferTime = dBDateTime.DBTime;
                                    this._WarehouseFacade.UpdateInvTransferDetail(invTransferDetail);
                                }

                                //更新tblinvtransfer.TransferStatus栏位状态                        
                                int closeCount = this._WarehouseFacade.GetInvTransferDetailCount(invTransfer.TransferNO, RecordStatus.RecordStatus_CLOSE);
                                if (closeCount == 0)
                                {
                                    invTransfer.TransferStatus = RecordStatus.RecordStatus_CLOSE;
                                    this._WarehouseFacade.UpdateInvTransfer(invTransfer);
                                }
                                //add by vivian.sun 2011-4-28 【当一个行项目tblinvtransferdetail变为RecordStatus_CLOSE时，
                                //如果不是所有的行项目都RecordStatus_CLOSE，则tblinvtransfer的状态设置为RecordStatus_USING】
                                else
                                {
                                    invTransfer.TransferStatus = RecordStatus.RecordStatus_USING;
                                    this._WarehouseFacade.UpdateInvTransfer(invTransfer);
                                }
                                //end add
                                #endregion



                            }

                        }

                    }
                }
                #endregion

                if (saveData)
                {
                    this.DataProvider.CommitTransaction();
                    if (m_ToStorageID != string.Empty)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_MaterialTransfer_Success"));
                    }
                    else
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_SendMaterialLot_Success"));
                    }
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
                this.GetTransferNo();
                this.checkBoxNoFinish.Checked = false;
                this.checkBoxNoFinish.Checked = true;
            }
        }

        private void ultraGridHead_ClickCellButton(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Key.ToUpper() == "Clear".ToUpper())
            {
                string itemCode = string.Empty;
                itemCode = e.Cell.Row.Cells["MCode"].Value.ToString();
                int transLine = int.Parse(e.Cell.Row.Cells["TransferLine"].Value.ToString());
                //得到母行
                int rowCode = this.GetParentCount(itemCode, transLine);
                if (rowCode == -1)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Not_Found_ParentRow"));
                    return;
                }
                this.m_CheckInvTransferDetail.Rows[rowCode]["TransQty"] = decimal.Parse(this.m_CheckInvTransferDetail.Rows[rowCode]["TransQty"].ToString()) - decimal.Parse(e.Cell.Row.Cells["TransQty"].Value.ToString());
                this.m_CheckInvTransferDetail.AcceptChanges();
                //得到当前要删除的行
                int currentRows = 0;
                for (int i = 0; i < this.m_CheckITEMTrans.Rows.Count; i++)
                {
                    if (m_CheckITEMTrans.Rows[i]["MCode"].ToString() == e.Cell.Row.Cells["MCode"].Value.ToString() && m_CheckITEMTrans.Rows[i]["LOTNO"].ToString() == e.Cell.Row.Cells["LOTNO"].Value.ToString() &&
                        m_CheckITEMTrans.Rows[i]["TransferLine"].ToString() == transLine.ToString() && m_CheckITEMTrans.Rows[i]["FrmStackCODE"].ToString() == e.Cell.Row.Cells["FrmStackCODE"].Value.ToString() &&
                        e.Cell.Row.Cells["Type"].Value.ToString() == "Add")
                    {
                        currentRows = i;
                    }
                }

                this.m_CheckITEMTrans.Rows[currentRows].Delete();
                this.m_SampleList.AcceptChanges();
            }
            if (e.Cell.Column.Key.ToUpper() == "ToStackCode".ToUpper())
            {
                FStackCode fStackCodeQuery = new FStackCode(this.m_ToStorageID);
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

        private void ucButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ultraGridHead_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            //管控料不能维护发料数量
            if (e.Row.HasParent() && (e.Row.ParentRow.Cells.Exists("McontrolType")))
            {
                if (e.Row.HasParent() && (e.Row.ParentRow.Cells["McontrolType"].Value.ToString().ToLower() == BOMItemControlType.ITEM_CONTROL_KEYPARTS))
                {
                    e.Row.ParentRow.Cells["TransQty"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    if (e.Row.Cells.Exists("TransQty"))
                    {
                        e.Row.Cells["TransQty"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    }

                }
                if (e.Row.HasParent() && (e.Row.ParentRow.Cells["McontrolType"].Value.ToString().ToLower() == BOMItemControlType.ITEM_CONTROL_LOT))
                {
                    e.Row.ParentRow.Cells["TransQty"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                }
                if (e.Row.HasParent() && (e.Row.ParentRow.Cells["McontrolType"].Value.ToString().ToLower() == BOMItemControlType.ITEM_CONTROL_NOCONTROL))
                {
                    e.Row.ParentRow.Cells["TransQty"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    e.Row.ParentRow.Cells["TransQty"].Appearance.BackColor = Color.LawnGreen;
                }
                if (!e.Row.HasParent() && (e.Row.Cells["McontrolType"].Value.ToString().ToLower() == BOMItemControlType.ITEM_CONTROL_KEYPARTS || e.Row.Cells["McontrolType"].Value.ToString().ToLower() == BOMItemControlType.ITEM_CONTROL_LOT))
                {
                    e.Row.Cells["TransQty"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                }
                if (!e.Row.HasParent() && (e.Row.Cells["McontrolType"].Value.ToString().ToLower() == BOMItemControlType.ITEM_CONTROL_NOCONTROL))
                {
                    e.Row.Cells["TransQty"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;


                    e.Row.Cells["TransQty"].Appearance.BackColor = Color.LawnGreen;
                }
            }

            //设置新增数据的背景颜色
            if (e.Row.Cells.Exists("Type"))
            {
                if (e.Row.Cells["Type"].Value.ToString() == "Add")
                {
                    //e.Row.Appearance.BackColor = Color.LightGreen;
                    e.Row.Cells["TransQty"].Appearance.BackColor = Color.LawnGreen;
                    e.Row.Cells["Clear"].Appearance.BackColor = Color.LawnGreen;
                }
            }

            //原有数据不能删除
            if (e.Row.HasParent() && (e.Row.Cells.Exists("Type")))
            {

                if (e.Row.HasParent() && e.Row.Cells["Type"].Value.ToString() == "Select")
                {
                    e.Row.Cells["Clear"].Activation = Activation.Disabled;
                }
            }
            //已发物料数量不能修改
            if (e.Row.HasParent() && (e.Row.Cells.Exists("Type")))
            {

                if (e.Row.HasParent() && e.Row.Cells["Type"].Value.ToString() == "Select")
                {
                    e.Row.Cells["TransQty"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                }
            }
        }

        //勾选未完成时转换显示内容
        private void checkBoxNoFinish_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxNoFinish.Checked)
            {
                for (int i = 0; i < ultraGridHead.Rows.Count; i++)
                {
                    if (decimal.Parse(this.ultraGridHead.Rows[i].Cells["PlanQty"].Value.ToString()) <= decimal.Parse(this.ultraGridHead.Rows[i].Cells["AlreadyACTQty"].Value.ToString()))
                    {
                        this.ultraGridHead.Rows[i].Hidden = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < ultraGridHead.Rows.Count; i++)
                {
                    if (decimal.Parse(this.ultraGridHead.Rows[i].Cells["PlanQty"].Value.ToString()) <= decimal.Parse(this.ultraGridHead.Rows[i].Cells["AlreadyACTQty"].Value.ToString()))
                    {
                        this.ultraGridHead.Rows[i].Hidden = false;
                    }
                }
            }
        }

        //选择的业务类型决定了先进先出（FIFO）控件的可用与否
        private void ucLabelComboxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string businesscode = FormatString(ucLabelComboxType.SelectedItemValue);
            InvBusiness invBusiness = (InvBusiness)this._InventoryFacade.GetInvBusiness(businesscode, orgID);
            if (invBusiness != null)
            {
                if (invBusiness.ISFIFO == FIFOFlag.FIFOFlag_Y)
                {
                    this.opsCheckFIFO.Enabled = true;
                    this.edtBufferDate.Enabled = true;
                }
                else
                {
                    this.opsCheckFIFO.Enabled = false;
                    this.edtBufferDate.Enabled = false;
                }
            }
            else
            {
                this.opsCheckFIFO.Enabled = false;
                this.edtBufferDate.Enabled = false;
            }

        }

        private void ultraGridHead_ClickCell(object sender, ClickCellEventArgs e)
        {
            this.ultraGridHead.UpdateData();
        }

        private void ultraGridHead_AfterCellUpdate(object sender, CellEventArgs e)
        {
            if (e.Cell.Row.HasParent() && (e.Cell.Row.Cells["Type"].Value.ToString() == "Add"))
            {
                decimal qty = 0;
                for (int i = 0; i < m_CheckITEMTrans.Rows.Count; i++)
                {
                    if (m_CheckITEMTrans.Rows[i]["Type"].ToString() == "Add" && m_CheckITEMTrans.Rows[i]["MCode"].ToString() == e.Cell.Row.Cells["MCode"].Value.ToString() &&
                        m_CheckITEMTrans.Rows[i]["TransferLine"].ToString() == e.Cell.Row.Cells["TransferLine"].Value.ToString())
                    {
                        qty += decimal.Parse(m_CheckITEMTrans.Rows[i]["TransQty"].ToString());
                    }
                }
                e.Cell.Row.ParentRow.Cells["TransQty"].Value = qty;
            }

        }

        private void btnGetStorageCode_Click(object sender, EventArgs e)
        {
            string oldToStorage = this.ucLabelEditToStorageID.Value;
            FStorageCodeQuery objForm = new FStorageCodeQuery();
            objForm.Owner = this;
            objForm.StartPosition = FormStartPosition.CenterScreen;
            objForm.BigSSCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(StorageCodeSelector_StorageCodeSelectedEvent);
            objForm.ShowDialog();
            if (!this.ucLabelEditToStorageID.Value.Equals(oldToStorage))
            {
                if (!this.ucLabelEditToStorageID.Value.Equals(m_ToStorageID))
                {
                    m_ToStorageID = this.ucLabelEditToStorageID.Value;
                    this.changeStackCode();
                }
                if (m_ToStorageID == "")
                {
                    this.ucLabelEditToStorageID.Value = "";
                }
                else
                {
                    this.ucLabelEditToStorageID.Value = m_ToStorageID + " - " + GetStorageName(orgID, m_ToStorageID);
                }
            }
            this.ClearValue();
        }

        private void ucButtonPrint_Click(object sender, EventArgs e)
        {
            if (!CheckISSelectRow())
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_NO_ROW_SELECTED"));
                return;
            }

            //获取打印模板的路径
            if (this.ucLabelComboxPrintTemplete.SelectedIndex < 0)
            {
                this.ShowMessage("$Error_NO_TempLeteSelect");
                return;
            }
            string filePath = ((PrintTemplate)this.ucLabelComboxPrintTemplete.SelectedItemValue).TemplatePath.ToString();

            if (!filePath.ToUpper().Contains(".LAB"))
            {
                this.ShowMessage("$Error_LAB_File_Select!");
                return;
            }
            printRcardList();
        }


        #region method
        private void GetTransferNo()
        {

            object invTransfer = _WarehouseFacade.GetInvTransfer(this.ucLabelEditTransferNO.Value.Trim().ToUpper());
            if (invTransfer != null)
            {
                this.m_FromStorageID = ((InvTransfer)invTransfer).FromStorageID.ToString();
                if (!string.Equals(((InvTransfer)invTransfer).ToStorageID.ToString(), string.Empty))
                {
                    this.m_ToStorageID = ((InvTransfer)invTransfer).ToStorageID.ToString();
                }
                else
                {
                    this.m_ToStorageID = "";
                }
                this.orgID = ((InvTransfer)invTransfer).OrgID;
                if (m_FromStorageID == string.Empty)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_FromStorageID_IS_Null"));
                    this.ucLabelEditTransferNO.TextFocus(false, true);
                    this.ucLabelEditFromStorageID.TextFocus(true, false);
                    this.ucLabelEditToStorageID.TextFocus(true, false);
                    this.ucLabelEditRectype.TextFocus(true, false);
                    this.ucLabelEditMemo.TextFocus(true, false);
                    return;
                }
                object objStorage = _InventoryFacade.GetStorage(orgID, m_FromStorageID);
                if (objStorage == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_FromStorageID_Invalid"));
                    this.ucLabelEditTransferNO.TextFocus(false, true);
                    this.ucLabelEditFromStorageID.TextFocus(true, false);
                    this.ucLabelEditToStorageID.TextFocus(true, false);
                    this.ucLabelEditRectype.TextFocus(true, false);
                    this.ucLabelEditMemo.TextFocus(true, false);
                    return;
                }

                this.m_TransferNO = ((InvTransfer)invTransfer).TransferNO.ToString();

                this.ucLabelEditRectype.Value = MutiLanguages.ParserString(((InvTransfer)invTransfer).Rectype.ToString());

                this.ucLabelEditFromStorageID.Value = m_FromStorageID + " - " + GetStorageName(orgID, m_FromStorageID);
                if (m_ToStorageID.Trim() != string.Empty)
                {
                    this.ucLabelEditToStorageID.Value = m_ToStorageID + " - " + GetStorageName(orgID, m_ToStorageID);
                }
                else
                {
                    this.ucLabelEditToStorageID.Value = "";
                }
                this.ucLabelEditMemo.Value = ((InvTransfer)invTransfer).Memo.ToString();
                Messages msg = new Messages();
                msg = this.LoadSampleList(m_TransferNO, m_FromStorageID, ((InvTransfer)invTransfer).ToStorageID);
                if (!msg.IsSuccess())
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, msg.ToString()));
                    this.ucLabelEditTransferNO.TextFocus(false, true);
                    this.ucLabelEditFromStorageID.TextFocus(true, false);
                    this.ucLabelEditToStorageID.TextFocus(true, false);
                    this.ucLabelEditRectype.TextFocus(true, false);
                    this.ucLabelEditMemo.TextFocus(true, false);
                    return;
                }
                this.changeStackCode();

            }
            else
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SN_NOT_EXIST_TransferNO"));
                this.ucLabelEditTransferNO.TextFocus(false, true);
                this.ucLabelEditFromStorageID.TextFocus(true, false);
                this.ucLabelEditToStorageID.TextFocus(true, false);
                this.ucLabelEditRectype.TextFocus(true, false);
                this.ucLabelEditMemo.TextFocus(true, false);
                return;
            }
            this.ClearValue();

        }

        private void InitializeSampleListGrid()
        {
            this.m_SampleList = new DataSet();
            this.m_CheckInvTransferDetail = new DataTable("CheckInvTransferDetail");//第一层 单号下的物料信息
            this.m_CheckITEMTrans = new DataTable("CheckITEMTrans"); //第二层 物料下的批次信息
            this.m_CheckKPartTrans = new DataTable("CheckKPartTrans");//第三层 批下面的KPart信息

            //this.m_CheckInvTransferDetail.Columns.Add("TransferNO", typeof(string));
            this.m_CheckInvTransferDetail.Columns.Add("Checked", typeof(string));
            this.m_CheckInvTransferDetail.Columns.Add("TransferLine", typeof(string));
            this.m_CheckInvTransferDetail.Columns.Add("MCode", typeof(string));
            this.m_CheckInvTransferDetail.Columns.Add("MDesc", typeof(string));
            this.m_CheckInvTransferDetail.Columns.Add("McontrolType", typeof(string));
            this.m_CheckInvTransferDetail.Columns.Add("McontrolTypeName", typeof(string));
            this.m_CheckInvTransferDetail.Columns.Add("FrmStorageQty", typeof(double));
            this.m_CheckInvTransferDetail.Columns.Add("ToStorageQty", typeof(double));
            this.m_CheckInvTransferDetail.Columns.Add("PlanQty", typeof(double));
            this.m_CheckInvTransferDetail.Columns.Add("AlreadyACTQty", typeof(double));
            this.m_CheckInvTransferDetail.Columns.Add("TransQty", typeof(double));
            this.m_CheckInvTransferDetail.Columns.Add("Memo", typeof(string));
            this.m_CheckInvTransferDetail.Columns.Add("MOCode", typeof(string));
            this.m_CheckInvTransferDetail.Columns.Add("ToStorageCode", typeof(string));
            this.m_CheckInvTransferDetail.Columns.Add("ToStackCode", typeof(string));
            this.m_CheckInvTransferDetail.Columns.Add("Type", typeof(string));

            this.m_CheckITEMTrans.Columns.Add("Checked", typeof(string));
            this.m_CheckITEMTrans.Columns.Add("ITEMTrans_Serial", typeof(int));
            this.m_CheckITEMTrans.Columns.Add("TransferLine", typeof(string));
            this.m_CheckITEMTrans.Columns.Add("MCode", typeof(string));
            this.m_CheckITEMTrans.Columns.Add("SERIAL", typeof(string));
            this.m_CheckITEMTrans.Columns.Add("LOTNO", typeof(string));
            this.m_CheckITEMTrans.Columns.Add("FrmStackCODE", typeof(string));
            this.m_CheckITEMTrans.Columns.Add("FrmStorageQty", typeof(double));
            this.m_CheckITEMTrans.Columns.Add("TransQty", typeof(double));
            this.m_CheckITEMTrans.Columns.Add("Clear", typeof(string));
            this.m_CheckITEMTrans.Columns.Add("Type", typeof(string));

            // add by andy xin 2010-12-3
            this.m_CheckKPartTrans.Columns.Add("ITEMTrans_Serial", typeof(int));
            this.m_CheckKPartTrans.Columns.Add("FrmStackCODE", typeof(string));
            this.m_CheckKPartTrans.Columns.Add("TransferLine", typeof(string));
            this.m_CheckKPartTrans.Columns.Add("MCode", typeof(string));
            this.m_CheckKPartTrans.Columns.Add("LOTNO", typeof(string));
            this.m_CheckKPartTrans.Columns.Add("SERIALNO", typeof(string));

            this.m_SampleList.Tables.Add(this.m_CheckInvTransferDetail);
            this.m_SampleList.Tables.Add(this.m_CheckITEMTrans);
            this.m_SampleList.Tables.Add(this.m_CheckKPartTrans);
            DataColumn[] parentCols = new DataColumn[] { this.m_SampleList.Tables["CheckInvTransferDetail"].Columns["MCode"], this.m_SampleList.Tables["CheckInvTransferDetail"].Columns["TransferLine"] };
            DataColumn[] childCols = new DataColumn[] { this.m_SampleList.Tables["CheckITEMTrans"].Columns["MCode"], this.m_SampleList.Tables["CheckITEMTrans"].Columns["TransferLine"] };

            DataColumn[] parentCols2 = new DataColumn[] { this.m_SampleList.Tables["CheckITEMTrans"].Columns["ITEMTrans_Serial"], this.m_SampleList.Tables["CheckITEMTrans"].Columns["LOTNO"], this.m_SampleList.Tables["CheckITEMTrans"].Columns["MCode"], this.m_SampleList.Tables["CheckITEMTrans"].Columns["TransferLine"], this.m_SampleList.Tables["CheckITEMTrans"].Columns["FrmStackCODE"] };
            DataColumn[] childCols2 = new DataColumn[] { this.m_SampleList.Tables["CheckKPartTrans"].Columns["ITEMTrans_Serial"], this.m_SampleList.Tables["CheckKPartTrans"].Columns["LOTNO"], this.m_SampleList.Tables["CheckKPartTrans"].Columns["MCode"], this.m_SampleList.Tables["CheckKPartTrans"].Columns["TransferLine"], this.m_SampleList.Tables["CheckKPartTrans"].Columns["FrmStackCODE"] };

            //第一层和第二层关联  行号+物料代码
            this.m_SampleList.Relations.Add(new DataRelation("SampleGroupAll", parentCols, childCols));
            //第二层和第三层关联  批号Serial+批号+物料代码+行号
            this.m_SampleList.Relations.Add(new DataRelation("SampleGroupAll2", parentCols2, childCols2)); ;


            this.m_SampleList.AcceptChanges();


            this.ultraGridHead.DataSource = this.m_SampleList;
        }

        private void ClearSampleList()
        {
            if (this.m_SampleList == null)
            {
                return;
            }

            this.m_SampleList.Tables["CheckKPartTrans"].Rows.Clear();
            this.m_SampleList.Tables["CheckITEMTrans"].Rows.Clear();
            this.m_SampleList.Tables["CheckInvTransferDetail"].Rows.Clear();

            this.m_SampleList.Tables["CheckKPartTrans"].AcceptChanges();
            this.m_SampleList.Tables["CheckITEMTrans"].AcceptChanges();
            this.m_SampleList.Tables["CheckInvTransferDetail"].AcceptChanges();
            this.m_SampleList.AcceptChanges();
        }

        private Messages LoadSampleList(string transferNO, string fromStorageID, string toStorageID)
        {
            Messages msg = new Messages();

            try
            {
                decimal qty = 0;
                this.ClearSampleList();
                object[] transferQueryList = _WarehouseFacade.GetInvTransferDetailForQuery(fromStorageID, toStorageID, m_TransferNO);

                if (transferQueryList != null)
                {
                    foreach (InvTransferDetailForQuey transferQuery in transferQueryList)
                    {
                        DataRow rowTransferNO;

                        rowTransferNO = this.m_SampleList.Tables["CheckInvTransferDetail"].NewRow();
                        rowTransferNO["Checked"] = false;
                        rowTransferNO["TransferLine"] = transferQuery.TransferLine;
                        rowTransferNO["MCode"] = transferQuery.ItemCode;
                        rowTransferNO["MDesc"] = transferQuery.MaterialDescription;
                        rowTransferNO["McontrolType"] = transferQuery.MaterialControlType;
                        rowTransferNO["McontrolTypeName"] = MutiLanguages.ParserString(transferQuery.MaterialControlType);
                        //edit by kathy @20140626 查询批次料库存数量-已备料数量
                        rowTransferNO["FrmStorageQty"] = (transferQuery.MaterialControlType.ToUpper() == MControlType.ITEM_CONTROL_LOT ? transferQuery.FrmLotQty : transferQuery.FrmStorageQty);
                        rowTransferNO["ToStorageQty"] = (transferQuery.MaterialControlType.ToUpper() == MControlType.ITEM_CONTROL_LOT ? transferQuery.ToLotQty : transferQuery.ToStorageQty);
                        rowTransferNO["PlanQty"] = transferQuery.Planqty;
                        rowTransferNO["AlreadyACTQty"] = transferQuery.Actqty;
                        rowTransferNO["Memo"] = transferQuery.Memo;
                        rowTransferNO["MOCode"] = transferQuery.MOCode;
                        rowTransferNO["ToStorageCode"] = transferQuery.ToStorageID;
                        rowTransferNO["ToStackCode"] = transferQuery.StackCODE;

                        if (transferQuery.MaterialControlType.ToLower() == BOMItemControlType.ITEM_CONTROL_NOCONTROL)
                        {
                            rowTransferNO["TransQty"] = transferQuery.Planqty;
                        }
                        else
                        {
                            rowTransferNO["TransQty"] = qty;
                        }

                        rowTransferNO["Type"] = "Select";



                        this.m_SampleList.Tables["CheckInvTransferDetail"].Rows.Add(rowTransferNO);

                        if (rowTransferNO["McontrolType"].ToString().ToLower() == BOMItemControlType.ITEM_CONTROL_KEYPARTS || rowTransferNO["McontrolType"].ToString().ToLower() == BOMItemControlType.ITEM_CONTROL_LOT)
                        {
                            object[] itemTranslist = this._InventoryFacade.GetItemTransForInvTransfer(m_TransferNO, transferQuery.ItemCode, transferQuery.TransferLine);
                            if (itemTranslist != null)
                            {
                                foreach (ItemTransLotForTrans itemTrans in itemTranslist)
                                {
                                    DataRow rowItemTrans;
                                    rowItemTrans = this.m_SampleList.Tables["CheckITEMTrans"].NewRow();
                                    rowItemTrans["Checked"] = false;
                                    rowItemTrans["ITEMTrans_Serial"] = itemTrans.Serial;
                                    rowItemTrans["TransferLine"] = itemTrans.Transline;
                                    rowItemTrans["MCode"] = itemTrans.Itemcode;
                                    rowItemTrans["LOTNO"] = itemTrans.Lotno;
                                    rowItemTrans["FrmStackCODE"] = itemTrans.Stackcode;
                                    rowItemTrans["SERIAL"] = GetRowCount(itemTrans.Itemcode, itemTrans.Transline);
                                    rowItemTrans["FrmStorageQty"] = itemTrans.FrmStorageQty;
                                    rowItemTrans["TransQty"] = itemTrans.Transqty;
                                    rowItemTrans["Type"] = "Select";

                                    this.m_SampleList.Tables["CheckITEMTrans"].Rows.Add(rowItemTrans);

                                    // add by andy xin 2010-12-3
                                    object[] checkItems = _InventoryFacade.QueryItemTransLotDetai(itemTrans.Serial.ToString(), itemTrans.Lotno);
                                    if (checkItems != null)
                                    {
                                        DataRow rowItem;
                                        foreach (ItemTransLotDetail checkList in checkItems)
                                        {
                                            rowItem = this.m_SampleList.Tables["CheckKPartTrans"].NewRow();
                                            rowItem["ITEMTrans_Serial"] = checkList.Tblitemtrans_serial;
                                            rowItem["TransferLine"] = itemTrans.Transline;
                                            rowItem["MCode"] = itemTrans.Itemcode;
                                            rowItem["LotNO"] = checkList.Lotno;
                                            rowItem["SERIALNO"] = checkList.Serialno;
                                            rowItem["FrmStackCODE"] = itemTrans.Stackcode;

                                            this.m_SampleList.Tables["CheckKPartTrans"].Rows.Add(rowItem);
                                        }
                                    }

                                    if (checkItems != null)
                                    {
                                        this.m_SampleList.Tables["CheckKPartTrans"].AcceptChanges();
                                    }
                                }
                            }
                            if (itemTranslist != null)
                                this.m_SampleList.Tables["CheckITEMTrans"].AcceptChanges();
                        }

                        if (transferQueryList != null)
                            this.m_SampleList.Tables["CheckInvTransferDetail"].AcceptChanges();

                    }
                }

                this.m_SampleList.AcceptChanges();
                this.ultraGridHead.DataSource = this.m_SampleList;
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
            }
            return msg;
        }


        private bool CheckFiFo(string itemCode, string vendorCode, int createDate, string lotNo)
        {
            //检查FIFO
            DateTime dateTime = Convert.ToDateTime(FormatHelper.ToDateString(createDate, "-")).AddDays(-Convert.ToInt32(this.edtBufferDate.Value.Trim()));
            int bufferDate = FormatHelper.TODateInt(dateTime.Date);

            if (this.opsCheckFIFO.Value.ToString() == NeedVendor.NeedVendor_N)
            {
                vendorCode = string.Empty;
            }
            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            int currentDate = dBDateTime.DBDate;
            object[] queryObjects = this._InventoryFacade.QueryItemLotForTrans(m_FromStorageID, itemCode, vendorCode, bufferDate, currentDate, lotNo);
            if (queryObjects != null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ItemCode:" + itemCode + "$CS_Have_Ever_MaterialLot:" + ((ItemLot)queryObjects[0]).Lotno));
                return false;
            }

            return true;
        }

        //初始化业务类型下拉框
        private void BindBusinessCode()
        {
            ucLabelComboxType.ComboBoxData.Items.Clear();
            int orgid = ApplicationService.Current().LoginInfo.Resource.OrganizationID;
            object[] objs = this._InventoryFacade.QueryInvBusiness(BussinessType.type_out, orgid);

            if (objs != null)
            {
                foreach (object obj in objs)
                {
                    InvBusiness invBusiness = (InvBusiness)obj;
                    if (invBusiness != null)
                    {
                        ucLabelComboxType.AddItem(invBusiness.BusinessDescription, invBusiness.BusinessCode);
                    }
                }
            }
        }

        private void StackCodeSelector_StackCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.txtstack.Text = e.CustomObject;
        }

        private void StorageCodeSelector_StorageCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.ucLabelEditToStorageID.Value = e.CustomObject;
        }

        //得到Grid中子行的行数
        private int GetRowCount(string itemCode, int transferLine)
        {
            int count = 1;
            for (int i = 0; i < this.m_CheckITEMTrans.Rows.Count; i++)
            {
                if (itemCode == this.m_CheckITEMTrans.Rows[i]["MCode"].ToString() && transferLine.ToString() == this.m_CheckITEMTrans.Rows[i]["TransferLine"].ToString())
                {
                    count += 1;
                }
            }
            return count;
        }

        //得到Grid中与子行中的料号相同母行的行数
        private int GetParentCount(string itemCode, int transLine)
        {
            int returnValue = -1;
            for (int i = 0; i < this.m_CheckInvTransferDetail.Rows.Count; i++)
            {
                if (m_CheckInvTransferDetail.Rows[i]["MCode"].ToString() == itemCode && m_CheckInvTransferDetail.Rows[i]["TransferLine"].ToString() == transLine.ToString())
                {
                    returnValue = i;
                }
            }
            return returnValue;
        }

        private int GetParentCount(string itemCode)
        {
            int returnValue = -1;
            for (int i = 0; i < this.m_CheckInvTransferDetail.Rows.Count; i++)
            {
                if (itemCode == this.m_CheckInvTransferDetail.Rows[i]["MCode"].ToString() && (decimal.Parse(this.m_CheckInvTransferDetail.Rows[i]["PlanQty"].ToString()) > decimal.Parse(this.m_CheckInvTransferDetail.Rows[i]["AlreadyACTQty"].ToString())))
                {
                    returnValue = i;
                    break;
                }

            }
            return returnValue;
        }

        private int GetParentCountInput(string itemCode)
        {
            int returnValue = -1;
            for (int i = 0; i < this.m_CheckInvTransferDetail.Rows.Count; i++)
            {
                if (m_CheckInvTransferDetail.Rows[i]["Checked"].ToString() == "True" && m_CheckInvTransferDetail.Rows[i]["MCode"].ToString() == itemCode)
                {
                    returnValue = i;
                }
            }
            return returnValue;
        }
        //得到Grid中母行中的目的库位
        private string GetToStackCode(int rowCode)
        {
            string toStackCode = this.m_CheckInvTransferDetail.Rows[rowCode]["ToStackCode"].ToString();
            if (toStackCode == string.Empty)
            {
                toStackCode = "";
            }
            return toStackCode;
        }

        //检查输入有效性
        private bool ValidateInput()
        {
            if (!CheckBufferDate())
            {
                this.edtBufferDate.TextFocus(false, true);
                return false;
            }

            if (this.ucLabelComboxType.SelectedItemValue == null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_PeleaseSelect_BusinessType"));
                return false;
            }
            return true;
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

        //检查数据刷入的唯一性
        private Messages CheckDataAlreadyExist(string lotNO, string mCode, string stackCode, int transferLine)
        {
            Messages msg = new Messages();
            for (int i = 0; i < m_CheckITEMTrans.Rows.Count; i++)
            {
                if (m_CheckITEMTrans.Rows[i]["Type"].ToString() == "Add")
                {
                    if (m_CheckITEMTrans.Rows[i]["LOTNO"].ToString() == lotNO && m_CheckITEMTrans.Rows[i]["MCode"].ToString() == mCode && m_CheckITEMTrans.Rows[i]["FrmStackCODE"].ToString() == stackCode &&
                        m_CheckITEMTrans.Rows[i]["TransferLine"].ToString() == transferLine.ToString())
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_Lot:" + lotNO + " $CS_StackCode:" + stackCode + " $ERROR_DATA_ALREADY_EXIST"));
                    }
                }
            }
            return msg;
        }

        //检查数据刷入的唯一性
        private bool CheckLotDataAlreadyExist(string lotNO, string mCode, string stackCode, int transferLine)
        {
            //Messages msg = new Messages();
            for (int i = 0; i < m_CheckITEMTrans.Rows.Count; i++)
            {
                if (m_CheckITEMTrans.Rows[i]["Type"].ToString() == "Add")
                {
                    if (m_CheckITEMTrans.Rows[i]["LOTNO"].ToString() == lotNO && m_CheckITEMTrans.Rows[i]["MCode"].ToString() == mCode && m_CheckITEMTrans.Rows[i]["FrmStackCODE"].ToString() == stackCode &&
                        m_CheckITEMTrans.Rows[i]["TransferLine"].ToString() == transferLine.ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //检查数据刷入的唯一性
        private bool CheckKPartDataAlreadyExist(string lotNO, string mCode, string stackCode, int transferLine, string kPary)
        {
            //Messages msg = new Messages();
            for (int i = 0; i < m_CheckKPartTrans.Rows.Count; i++)
            {

                if (m_CheckKPartTrans.Rows[i]["LOTNO"].ToString() == lotNO && m_CheckKPartTrans.Rows[i]["MCode"].ToString() == mCode && m_CheckKPartTrans.Rows[i]["FrmStackCODE"].ToString() == stackCode &&
                        m_CheckKPartTrans.Rows[i]["TransferLine"].ToString() == transferLine.ToString() && m_CheckKPartTrans.Rows[i]["SERIALNO"].ToString() == kPary)
                {
                    return true;
                }

            }
            return false;
        }

        private void ClearValue()
        {
            this.ucLERunningCard.TextFocus(true, false);
            this.btnGetStorageCode.Enabled = true;
        }

        //自动分配子行发料数量
        private decimal GetTransQty(decimal fromStorgeQty, int rowCode, string itemCode)
        {
            decimal newQty = fromStorgeQty;
            decimal oldQty = 0;
            decimal parentRowPlanQty = decimal.Parse(this.m_CheckInvTransferDetail.Rows[rowCode]["PlanQty"].ToString());
            decimal parentRowActQty = decimal.Parse(this.m_CheckInvTransferDetail.Rows[rowCode]["AlreadyACTQty"].ToString());
            for (int i = 0; i < m_CheckITEMTrans.Rows.Count; i++)
            {
                if (itemCode == m_CheckITEMTrans.Rows[i]["MCode"].ToString())
                {
                    oldQty += decimal.Parse(this.m_CheckITEMTrans.Rows[i]["TransQty"].ToString());
                }
            }
            if (fromStorgeQty >= parentRowPlanQty - parentRowActQty)
            {
                newQty = parentRowPlanQty - parentRowActQty;
            }
            else
            {
                if (oldQty > 0)
                {
                    if (fromStorgeQty > parentRowPlanQty - parentRowActQty)
                    {
                        newQty = parentRowPlanQty - parentRowActQty;
                    }
                }
            }
            return newQty;
        }

        //自动分配子行发料数量
        private int GetLotTransQty(string lotNO, string mCode, string stackCode, int transferLine)
        {
            int count = 0;
            for (int i = 0; i < m_CheckITEMTrans.Rows.Count; i++)
            {
                if (m_CheckITEMTrans.Rows[i]["Type"].ToString() == "Add")
                {
                    if (m_CheckITEMTrans.Rows[i]["LOTNO"].ToString() == lotNO && m_CheckITEMTrans.Rows[i]["MCode"].ToString() == mCode && m_CheckITEMTrans.Rows[i]["FrmStackCODE"].ToString() == stackCode &&
                        m_CheckITEMTrans.Rows[i]["TransferLine"].ToString() == transferLine.ToString())
                    {
                        count++;
                    }
                }
            }
            return count + 1;
        }

        //子行发料数量
        private decimal GetTransQty(string mCode, int transferLine)
        {
            int count = 0;
            for (int i = 0; i < m_CheckITEMTrans.Rows.Count; i++)
            {
                if (m_CheckITEMTrans.Rows[i]["Type"].ToString() == "Add" && m_CheckITEMTrans.Rows[i]["MCode"].ToString() == mCode &&
                        m_CheckITEMTrans.Rows[i]["TransferLine"].ToString() == transferLine.ToString())
                {
                    count += int.Parse(m_CheckITEMTrans.Rows[i]["TransQty"].ToString());
                }
            }

            return count;
        }

        private string GetStorageName(int orgid, string storageCode)
        {
            Storage storage = this._InventoryFacade.GetStorage(orgid, storageCode) as Storage;
            if (storage != null)
            {
                return storage.StorageName;
            }
            return "";
        }

        //初始赋值库位为当前目的库别的第一个库位
        private void changeStackCode()
        {
            object[] objStack = _InventoryFacade.GetStack(m_ToStorageID);
            if (objStack == null)
            {
                for (int i = 0; i < m_CheckInvTransferDetail.Rows.Count; i++)
                {
                    string itemCode = m_CheckInvTransferDetail.Rows[i]["MCode"].ToString();
                    string toStorageQty = _InventoryFacade.GetStorageQty(m_ToStorageID, itemCode).ToString();

                    m_CheckInvTransferDetail.Rows[i]["ToStorageCode"] = m_ToStorageID;
                    m_CheckInvTransferDetail.Rows[i]["ToStackCode"] = string.Empty;
                    m_CheckInvTransferDetail.Rows[i]["ToStorageQty"] = toStorageQty;
                    m_CheckInvTransferDetail.AcceptChanges();
                }
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Storage_NotFound_StackCode"));
                return;
            }
            if (!string.IsNullOrEmpty(((SStack)objStack[0]).StackCode))
            {
                for (int i = 0; i < m_CheckInvTransferDetail.Rows.Count; i++)
                {
                    string itemCode = m_CheckInvTransferDetail.Rows[i]["MCode"].ToString();
                    //string toStorageQty = _InventoryFacade.GetStorageQty(m_ToStorageID, itemCode).ToString("N0");

                    m_CheckInvTransferDetail.Rows[i]["ToStorageCode"] = m_ToStorageID;
                    m_CheckInvTransferDetail.Rows[i]["ToStackCode"] = ((SStack)objStack[0]).StackCode;
                    m_CheckInvTransferDetail.Rows[i]["ToStorageQty"] = _InventoryFacade.GetStorageQty(m_ToStorageID, itemCode);
                    m_CheckInvTransferDetail.AcceptChanges();
                }
            }
        }


        #endregion

        #region 打印

        //打印机列表
        private void LoadPrinter()
        {
            this.ucLabelComboxPrintList.Clear();

            if (System.Drawing.Printing.PrinterSettings.InstalledPrinters == null ||
                System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count == 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_PleaseInstallPrinter"));

                return;
            }

            int defaultprinter = 0;
            for (int i = 0; i < System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count; i++)
            {
                this.ucLabelComboxPrintList.AddItem(System.Drawing.Printing.PrinterSettings.InstalledPrinters[i], System.Drawing.Printing.PrinterSettings.InstalledPrinters[i]);
                System.Drawing.Printing.PrinterSettings pts = new System.Drawing.Printing.PrinterSettings();
                pts.PrinterName = System.Drawing.Printing.PrinterSettings.InstalledPrinters[i];
                if (pts.IsDefaultPrinter)
                {
                    defaultprinter = i;
                }
            }
            this.ucLabelComboxPrintList.SelectedIndex = defaultprinter;
        }

        //打印模板
        private void LoadTemplateList()
        {

            this.ucLabelComboxPrintTemplete.Clear();

            object[] objs = this.LoadTemplateListDataSource();
            if (objs == null)
            {
                this.ShowMessage("$CS_No_Data_To_Display");
                return;
            }

            _PrintTemplateList = new PrintTemplate[objs.Length];

            for (int i = 0; i < objs.Length; i++)
            {
                _PrintTemplateList[i] = (PrintTemplate)objs[i];

                ucLabelComboxPrintTemplete.AddItem(_PrintTemplateList[i].TemplateName, _PrintTemplateList[i]);

            }
        }

        //打印模板数据源
        private object[] LoadTemplateListDataSource()
        {
            try
            {
                PrintTemplateFacade printTemplateFacade = new PrintTemplateFacade(this.DataProvider);
                return printTemplateFacade.QueryPrintTemplate(string.Empty, string.Empty, int.MinValue, int.MaxValue);
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }

            return null;
        }

        //打印前检查数据
        private bool ValidateInput(string printer, PrintTemplate printTemplate)
        {
            ////序列号

            if (this.ucLabelEditPrintCount.Value.Trim() == "")
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_Print_Count_Empty"));
                return false;
            }

            //模板
            if (printTemplate == null)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_PrintTemplate_Empty"));
                return false;
            }

            //打印机

            if (printer == null || printer.Trim().Length <= 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_Printer_Empty"));
                return false;
            }

            return true;
        }

        //条码打印方法
        private void printRcardList()
        {
            try
            {
                //Check Printers
                if (this.ucLabelComboxPrintList.ComboBoxData.Items.Count == 0)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_PleaseInstallPrinter"));
                    return;
                }

                SetPrintButtonStatus(false);

                PrintTemplateFacade printTemplateFacade = new PrintTemplateFacade(this.DataProvider);
                MOFacade moFacade = new MOFacade(this.DataProvider);

                string printer = this.ucLabelComboxPrintList.SelectedItemText;

                PrintTemplate printTemplate = (PrintTemplate)this.ucLabelComboxPrintTemplete.SelectedItemValue;
                printTemplate = (PrintTemplate)printTemplateFacade.GetPrintTemplate(printTemplate.TemplateName);

                if (!System.IO.Path.IsPathRooted(printTemplate.TemplatePath))
                {
                    string ExePath = Application.StartupPath;
                    string SimplyPath = printTemplate.TemplatePath;
                    int PathIndex = SimplyPath.IndexOf("\\");
                    SimplyPath = SimplyPath.Substring(PathIndex);
                    printTemplate.TemplatePath = ExePath + SimplyPath;
                }
                List<ItemTransLotForTrans> itemLotList = GetEditItemLotObject();

                if (itemLotList.Count == 0)
                {
                    return;
                }

                if (!ValidateInput(printer, printTemplate))
                {
                    return;
                }

                for (int i = 0; i < int.Parse(this.ucLabelEditPrintCount.Value.Trim()); i++)
                {
                    Messages msg = this.Print(printer, printTemplate.TemplatePath, itemLotList);

                    this.ShowMessage(msg);
                }
            }
            finally
            {
                SetPrintButtonStatus(true);
            }
        }

        //打印
        public UserControl.Messages Print(string printer, string templatePath, List<ItemTransLotForTrans> itemLotList)
        {
            UserControl.Messages messages = new UserControl.Messages();
            CodeSoftFacade _CodeSoftFacade = new CodeSoftFacade();
            bool _IsBatchPrint = true;
            CodeSoftPrintFacade _CodeSoftPrintFacade = new CodeSoftPrintFacade(this.DataProvider);
            try
            {
                try
                {
                    _CodeSoftPrintFacade.PrePrint();
                    _CodeSoftFacade.OpenTemplate(printer, templatePath);
                }
                catch (System.Exception ex)
                {
                    messages.Add(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                    return messages;
                }

                //批量打印前生成文本文件

                string strBatchDataFile = string.Empty;
                if (_IsBatchPrint)
                {
                    strBatchDataFile = _CodeSoftPrintFacade.CreateFile();
                }

                for (int i = 0; i < itemLotList.Count; i++)
                {
                    ItemTransLotForTrans itemlot = (ItemTransLotForTrans)itemLotList[i];

                    string machineType = string.Empty;
                    string materialName = string.Empty;

                    object objMaterial = this._ItemFacade.GetMaterial(itemlot.Itemcode, orgID);

                    if (objMaterial != null)
                    {
                        machineType = (objMaterial as Domain.MOModel.Material).MaterialMachineType;
                        materialName = (objMaterial as Domain.MOModel.Material).MaterialName;
                    }

                    LabelPrintVars labelPrintVars = new LabelPrintVars();

                    string[] vars = new string[0];

                    if (messages.IsSuccess())
                    {
                        try
                        {
                            ItemLot lot = _InventoryFacade.GetItemLot(itemlot.Lotno, itemlot.Itemcode) as ItemLot;
                            //要传给Codesoft的数组，字段顺序不能修改
                            vars = _CodeSoftPrintFacade.GetPrintVars(itemlot.Lotno, itemlot.Itemcode, materialName, machineType, itemlot.Transqty.ToString(), lot.Venderlotno);

                            //批量打印前的写文件

                            if (_IsBatchPrint)
                            {
                                string[] printVars = _CodeSoftPrintFacade.ProcessVars(vars, labelPrintVars);
                                _CodeSoftPrintFacade.WriteFile(strBatchDataFile, printVars);
                            }
                            //直接打印
                            else
                            {
                                _CodeSoftFacade.LabelPrintVars = labelPrintVars;
                                _CodeSoftFacade.Print(vars);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            messages.Add(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                            return messages;
                        }
                    }
                }
                //批量打印
                if (_IsBatchPrint)
                {
                    try
                    {
                        _CodeSoftFacade.Print(strBatchDataFile, _CodeSoftPrintFacade.GetDataDescPath(_DataDescFileName));
                    }
                    catch (System.Exception ex)
                    {
                        messages.Add(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                        return messages;
                    }
                }

                messages.Add(new UserControl.Message(UserControl.MessageType.Success, "$Success_Print_Label"));
            }
            finally
            {
                _CodeSoftFacade.ReleaseCom();

            }
            return messages;
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

        protected List<ItemTransLotForTrans> GetEditItemLotObject()
        {

            List<ItemTransLotForTrans> list = new List<ItemTransLotForTrans>();
            ItemTransLotForTrans tran = null;
            foreach (DataRow dr in m_CheckITEMTrans.Rows)
            {
                if (dr["Checked"].ToString() == "True")
                {
                    tran = new ItemTransLotForTrans();
                    tran.Serial = int.Parse(dr["ITEMTrans_Serial"].ToString());
                    tran.Transline = int.Parse(dr["TransferLine"].ToString());
                    tran.Itemcode = dr["MCode"].ToString();
                    tran.Lotno = dr["LOTNO"].ToString();
                    tran.Stackcode = dr["FrmStackCODE"].ToString();
                    tran.Serial = int.Parse(dr["SERIAL"].ToString());
                    tran.FrmStorageQty = decimal.Parse(dr["FrmStorageQty"].ToString());
                    tran.Transqty = decimal.Parse(dr["TransQty"].ToString());

                    list.Add(tran);
                }

            }
            return list;
        }

        //判断是否选择了要打印的数据
        private bool CheckISSelectRow()
        {
            bool flag = false;
            foreach (DataRow dr in m_CheckITEMTrans.Rows)
            {
                if (dr["Checked"].ToString() == "True")
                {
                    flag = true;
                }
            }
            return flag;
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

        #endregion

        private void opsetPackObject_ValueChanged(object sender, EventArgs e)
        {
            this.ucLERunningCard.TextFocus(false, true);
        }



    }
}
