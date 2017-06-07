using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Infragistics.Win.UltraWinGrid;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.Delivery;
using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Package;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common;

using UserControl;

namespace BenQGuru.eMES.Client
{
    public partial class FOutInv : BaseForm
    {
        #region 变量

        private const string PACK_ERP = "0";
        private const string PACK_MES = "1";

        private const string FROM_FILEIMPORT = "FileImport";
        private const string FROM_SCAN = "Scan";

        private const string INPUT_PALLET = "Pallet";
        private const string INPUT_CARTON = "Carton";
        private const string INPUT_RUNNINGCARD = "RunningCard";

        private const string ERRORRUNNINGCARD = "异常序列号";

        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;

        private DataTable _DataTableDNList;         //用于“交货单号列表”Grid

        private DataSet _DataSetInfo;               //用于“待出库序列号”Grid
        private DataTable _DataTableHead;           //用于“待出库序列号”Grid中的Head部分
        private DataTable _DataTableDetail;         //用于“待出库序列号”Grid中的Detail部分

        private DataTable _DataTableErrorInfo;      //用于“异常序列号”Grid
        private DataTable _DataTableFile;           //用于后台记录已经输入成功的序列号        

        private object[] _CompanyList;
        private string _FunctionName = string.Empty;

        #endregion

        #region 属性

        private IDomainDataProvider DataProvider
        {
            get
            {
                return _DomainDataProvider;
            }
        }

        #endregion

        #region 基本

        public FOutInv()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void FOutInv_Load(object sender, EventArgs e)
        {
            this._FunctionName = this.Text;

            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(this.gridDNInfo);
            UserControl.UIStyleBuilder.GridUI(this.gridInfo);
            UserControl.UIStyleBuilder.GridUI(this.gridErrorInfo);

            this.InitializeCompany();
            this.InitialBusinessType();
            this.InitialDataSource();
            this.ultraOptionSetERPMES.Value = PACK_ERP;
            this.ultraOptionSetSource.Value = FROM_FILEIMPORT;
            this.ultraOptionSetScanType.Value = INPUT_PALLET;
            this.SetErrorRunningCardQty(0);
            this.txtLabel.TextFocus(true, true);
            this.chkPauseCancel.Checked = false;
            this.txtPauseCancelReason.ReadOnly = true;
            this.chkPauseCancel.Enabled = false;
            this.btnImport.Enabled = false;
            this.InitPageLanguage();
        }

        private void gridInfo_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.Override.CellClickAction = CellClickAction.Edit;
            //e.Layout.Override.CellClickAction = CellClickAction.CellSelect;

            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            // 自动判断是否显示前面的+、-号
            e.Layout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            e.Layout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;

            //if (this._DataTableHead.Rows.Count > 0)
            //{
                e.Layout.Bands[0].Columns["seq"].Header.Caption = "行项目号";
                e.Layout.Bands[0].Columns["seq"].Width = 60;
                e.Layout.Bands[0].Columns["seq"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                e.Layout.Bands[0].Columns["seq"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

                e.Layout.Bands[0].Columns["itemcode"].Header.Caption = "产品代码";
                e.Layout.Bands[0].Columns["itemcode"].Width = 80;
                e.Layout.Bands[0].Columns["itemcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                e.Layout.Bands[0].Columns["itemdesc"].Header.Caption = "产品描述";
                e.Layout.Bands[0].Columns["itemdesc"].Width = 120;
                e.Layout.Bands[0].Columns["itemdesc"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;


                e.Layout.Bands[0].Columns["storagecode"].Header.Caption = "库位";
                e.Layout.Bands[0].Columns["storagecode"].Width = 60;
                e.Layout.Bands[0].Columns["storagecode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                e.Layout.Bands[0].Columns["planqty"].Header.Caption = "计划";
                e.Layout.Bands[0].Columns["planqty"].Width = 60;
                e.Layout.Bands[0].Columns["planqty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                e.Layout.Bands[0].Columns["planqty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

                e.Layout.Bands[0].Columns["alreadysendedqty"].Header.Caption = "已出货";
                e.Layout.Bands[0].Columns["alreadysendedqty"].Width = 60;
                e.Layout.Bands[0].Columns["alreadysendedqty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                e.Layout.Bands[0].Columns["alreadysendedqty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

                e.Layout.Bands[0].Columns["sendedqty"].Header.Caption = "本次出库";
                e.Layout.Bands[0].Columns["sendedqty"].Width = 60;
                e.Layout.Bands[0].Columns["sendedqty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                e.Layout.Bands[0].Columns["sendedqty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;


                e.Layout.Bands[0].Columns["mocode"].Header.Caption = "工单号";
                e.Layout.Bands[0].Columns["mocode"].Width = 100;
                e.Layout.Bands[0].Columns["mocode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                e.Layout.Bands[0].Columns["reworkmocode"].Header.Caption = "返工工单号";
                e.Layout.Bands[0].Columns["reworkmocode"].Width = 100;
                e.Layout.Bands[0].Columns["reworkmocode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                e.Layout.Bands[0].Columns["contractno"].Header.Caption = "合约号码";
                e.Layout.Bands[0].Columns["contractno"].Width = 100;
                e.Layout.Bands[0].Columns["contractno"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                //e.Layout.Bands["Head"].Columns["lineno"].Hidden = true;
                //e.Layout.Bands[this._DataTableHead].Columns["lineno"].Hidden = true;
               

                e.Layout.Bands[0].Columns["lineno"].Hidden = true;
                e.Layout.Bands[0].Columns["dnno"].Hidden = true;
                e.Layout.Bands[0].Columns["cusorderno"].Hidden = true;
                e.Layout.Bands[0].Columns["businesscode"].Hidden = true;

            //}

            if (this._DataTableDetail.Rows.Count > 0)
            {


                e.Layout.Bands[1].Columns["stackcode"].Header.Caption = "垛位";
                e.Layout.Bands[1].Columns["stackcode"].Width = 100;
                e.Layout.Bands[1].Columns["stackcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                e.Layout.Bands[1].Columns["palletcode"].Header.Caption = "栈板号";
                e.Layout.Bands[1].Columns["palletcode"].Width = 120;
                e.Layout.Bands[1].Columns["palletcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                e.Layout.Bands[1].Columns["rcard"].Header.Caption = "序列号";
                e.Layout.Bands[1].Columns["rcard"].Width = 120;
                e.Layout.Bands[1].Columns["rcard"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                e.Layout.Bands[1].Columns["sendedqty"].Header.Caption = "待出货数量";
                e.Layout.Bands[1].Columns["sendedqty"].Width = 80;
                e.Layout.Bands[1].Columns["sendedqty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                e.Layout.Bands[1].Columns["sendedqty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

                e.Layout.Bands[1].Columns["scandate"].Header.Caption = "扫码时间";
                e.Layout.Bands[1].Columns["scandate"].Width = 130;
                e.Layout.Bands[1].Columns["scandate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                e.Layout.Bands[1].Columns["itemcode"].Hidden = true;                
                e.Layout.Bands[1].Columns["storagecode"].Hidden = true;
                e.Layout.Bands[1].Columns["company"].Hidden = true;
                e.Layout.Bands[1].Columns["cartoncode"].Hidden = true;
                e.Layout.Bands[1].Columns["mocode"].Hidden = true;
                e.Layout.Bands[1].Columns["pausecode"].Hidden = true;
                e.Layout.Bands[1].Columns["seq"].Hidden = true;
            }


            this.InitGridLanguage(gridInfo);
        }

        private void gridDNInfo_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            //e.Layout.Override.AllowColSizing = AllowColSizing.None;
            e.Layout.Override.CellClickAction = CellClickAction.Edit;
            //e.Layout.Override.CellClickAction = CellClickAction.CellSelect;

            e.Layout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            e.Layout.Bands[0].Columns["dnno"].Header.Caption = "交货单号列表";
            e.Layout.Bands[0].Columns["dnno"].Width = 100;
            e.Layout.Bands[0].Columns["dnno"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            this.InitGridLanguage(gridDNInfo);
        }

        private void gridErrorInfo_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            //e.Layout.Override.AllowColSizing = AllowColSizing.None;
            e.Layout.Override.CellClickAction = CellClickAction.Edit;
            //e.Layout.Override.CellClickAction = CellClickAction.CellSelect;

            e.Layout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            e.Layout.Bands[0].Columns["check"].Header.Caption = "";
            e.Layout.Bands[0].Columns["check"].Width = 16;
            e.Layout.Bands[0].Columns["check"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
            e.Layout.Bands[0].Columns["check"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            e.Layout.Bands[0].Columns["check"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;

            e.Layout.Bands[0].Columns["filelinenumber"].Header.Caption = "文件行号";
            e.Layout.Bands[0].Columns["filelinenumber"].Width = 60;
            e.Layout.Bands[0].Columns["filelinenumber"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["storagecode"].Header.Caption = "库位";
            e.Layout.Bands[0].Columns["storagecode"].Width = 100;
            e.Layout.Bands[0].Columns["storagecode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["stackcode"].Header.Caption = "垛位";
            e.Layout.Bands[0].Columns["stackcode"].Width = 100;
            e.Layout.Bands[0].Columns["stackcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["palletcode"].Header.Caption = "栈板号";
            e.Layout.Bands[0].Columns["palletcode"].Width = 100;
            e.Layout.Bands[0].Columns["palletcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["itemcode"].Header.Caption = "产品代码";
            e.Layout.Bands[0].Columns["itemcode"].Width = 100;
            e.Layout.Bands[0].Columns["itemcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["itemdesc"].Header.Caption = "产品描述";
            e.Layout.Bands[0].Columns["itemdesc"].Width = 150;
            e.Layout.Bands[0].Columns["itemdesc"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["reason"].Header.Caption = "异常原因";
            e.Layout.Bands[0].Columns["reason"].Width = 220;
            e.Layout.Bands[0].Columns["reason"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["rcard"].Header.Caption = "序列号";
            e.Layout.Bands[0].Columns["rcard"].Width = 100;
            e.Layout.Bands[0].Columns["rcard"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["company"].Header.Caption = "公司别";
            e.Layout.Bands[0].Columns["company"].Width = 100;
            e.Layout.Bands[0].Columns["company"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;


            e.Layout.Bands[0].Columns["scandate"].Header.Caption = "扫码时间";
            e.Layout.Bands[0].Columns["scandate"].Width = 150;
            e.Layout.Bands[0].Columns["scandate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            this.InitGridLanguage(gridErrorInfo);
        }

        private void gridDNInfo_Click(object sender, EventArgs e)
        {
            if (this.gridDNInfo.ActiveRow != null)
            {
                this.LoadUIHead(this.gridDNInfo.ActiveRow.Cells["dnno"].Value.ToString().Trim());
            }
        }

        private void ultraOptionSetERPMES_ValueChanged(object sender, EventArgs e)
        {

            btnCancel_Click(null, null);

            if (this.ultraOptionSetERPMES.CheckedItem.DataValue.ToString() == PACK_MES)
            {
                this.cboBusinessType.Enabled = false;
                this.cboBusinessType.SelectedIndex = -1;
                this.txtLabel.TextFocus(true, true);
            }

            if (this.ultraOptionSetERPMES.CheckedItem.DataValue.ToString() == PACK_ERP)
            {
                this.cboBusinessType.Enabled = true;
                this.cboBusinessType.SelectedIndex = -1;
                this.txtLabel.TextFocus(true, true);
            }
        }

        private void ultraOptionSetSource_ValueChanged(object sender, EventArgs e)
        {
            if (ultraOptionSetSource.CheckedIndex == 0)
            {
                txtFile.Enabled = true;
                ucButtonSelectFile.Enabled = true;
                btnImport.Enabled = true;

                ultraOptionSetScanType.Enabled = false;
                ucLabelEdit.Enabled = false;
            }
            else
            {
                txtFile.Enabled = false;
                ucButtonSelectFile.Enabled = false;
                //btnImport.Enabled = false;

                ultraOptionSetScanType.Enabled = true;
                ucLabelEdit.Enabled = true;
            }
        }

        private void txtTicketNo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.txtTicketNo.Value.Trim().Length == 0)
                {
                    return;
                }

                DeliveryFacade objFacade = new DeliveryFacade(this.DataProvider);

                object[] deliveryNoteList = null;

                if (this.ultraOptionSetERPMES.CheckedItem.DataValue.ToString() == PACK_MES)
                {
                    deliveryNoteList = objFacade.GetActiveDeliveryNoteHeadList(this.txtLabel.Value.Trim(), this.txtTicketNo.Value.Trim(), DNFrom.MES);
                }

                if (this.ultraOptionSetERPMES.CheckedItem.DataValue.ToString() == PACK_ERP)
                {
                    deliveryNoteList = objFacade.GetActiveDeliveryNoteHeadList(this.txtLabel.Value.Trim(), this.txtTicketNo.Value.Trim(), DNFrom.ERP);
                }


                if (deliveryNoteList == null)
                {
                    //Message:交货单不存在
                    ApplicationRun.GetInfoForm().AddEx(_FunctionName, "交货单:" + this.txtTicketNo.Value.Trim(),
                        new UserControl.Message(MessageType.Error, "$CS_DN_NOT_EXIST"), true);
                    this.txtTicketNo.TextFocus(false, true);
                    ClearUI();
                    return;
                }

                ////Load 交货单列表
                //
                this.LoadDnList(deliveryNoteList);

                this.gridDNInfo.ActiveRow = this.gridDNInfo.Rows[0];
                this.gridDNInfo.Rows[0].Selected = true;

                ////Load UI Head Info
                //
                this.LoadUIHead(this.gridDNInfo.ActiveRow.Cells["dnno"].Value.ToString().Trim());

            }
        }

        private void txtLabel_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.txtLabel.Value.Trim().Length == 0)
                {

                    return;
                }

                DeliveryFacade objFacade = new DeliveryFacade(this.DataProvider);

                object[] deliveryNoteList = null;

                if (this.ultraOptionSetERPMES.CheckedItem.DataValue.ToString() == PACK_MES)
                {
                    deliveryNoteList = objFacade.GetActiveDeliveryNoteHeadList(this.txtLabel.Value.Trim(), this.txtTicketNo.Value.Trim(), DNFrom.MES);
                }

                if (this.ultraOptionSetERPMES.CheckedItem.DataValue.ToString() == PACK_ERP)
                {
                    deliveryNoteList = objFacade.GetActiveDeliveryNoteHeadList(this.txtLabel.Value.Trim(), this.txtTicketNo.Value.Trim(), DNFrom.ERP);
                }


                if (deliveryNoteList == null)
                {
                    //Message:交货单不存在
                    ApplicationRun.GetInfoForm().AddEx(_FunctionName, "交货单:" + this.txtTicketNo.Value.Trim(),
                        new UserControl.Message(MessageType.Error, "$CS_DN_NOT_EXIST"), true);
                    this.txtLabel.TextFocus(false, true);
                    ClearUI();
                    return;
                }

                ////Load 交货单列表
                //
                this.LoadDnList(deliveryNoteList);

                this.gridDNInfo.ActiveRow = this.gridDNInfo.Rows[0];

                ////Load UI Head Info
                //
                this.LoadUIHead(this.gridDNInfo.ActiveRow.Cells["dnno"].Value.ToString().Trim());



            }
        }

        private void ucLabelEditInput_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!CheckInfoGrid())
                {
                    return;
                }

                PackageFacade packageFacade = new PackageFacade(this.DataProvider);
                InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);

                if (ultraOptionSetScanType.CheckedItem.DataValue.ToString() == INPUT_PALLET)
                {
                    if (this.ucLabelEdit.Value.Trim().Length <= 0)
                    {
                        ApplicationRun.GetInfoForm().AddEx(_FunctionName, "栈板号:" + this.ucLabelEdit.Value.Trim(), new UserControl.Message(MessageType.Error, "$CS_Please_Input_PALLET"), true);
                        this.ucLabelEdit.TextFocus(false, true);
                        return;
                    }

                    object[] pallet2RCardList = packageFacade.GetPallet2RCardListByPallet(this.ucLabelEdit.Value.Trim());

                    if (pallet2RCardList == null)
                    {
                        ApplicationRun.GetInfoForm().AddEx(_FunctionName, "栈板号:" + this.ucLabelEdit.Value.Trim(), new UserControl.Message(MessageType.Error, "$CS_Pallet2RCardNotFound"), true);
                        this.ucLabelEdit.TextFocus(false, true);
                        return;
                    }
                    else
                    {
                        foreach (Pallet2RCard pallet2RCard in pallet2RCardList)
                        {
                            StackToRcard stackToRcard = (StackToRcard)inventoryFacade.GetStackToRcard(pallet2RCard.RCard.Trim().ToUpper());

                            if (stackToRcard != null)
                            {
                                string content = GetContentFromStackToRCard(stackToRcard);
                                if (!ImportOneLine(string.Empty, content, inventoryFacade))
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
                if (ultraOptionSetScanType.CheckedItem.DataValue.ToString() == INPUT_CARTON)
                {
                    if (this.ucLabelEdit.Value.Trim().Length <= 0)
                    {
                        ApplicationRun.GetInfoForm().AddEx(_FunctionName, "箱号:" + this.ucLabelEdit.Value.Trim(), new UserControl.Message(MessageType.Error, "$CS_PLEASE_INPUT_CARTONNO"), true);
                        this.ucLabelEdit.TextFocus(false, true);
                        return;
                    }

                    object[] carton2RCardList = packageFacade.GetCarton2RCARDByCartonNO(this.ucLabelEdit.Value.Trim());

                    if (carton2RCardList == null)
                    {
                        ApplicationRun.GetInfoForm().AddEx(_FunctionName, "箱号:" + this.ucLabelEdit.Value.Trim(), new UserControl.Message(MessageType.Error, "$CS_Carton2RCardNotFound"), true);
                        this.ucLabelEdit.TextFocus(false, true);
                        return;
                    }
                    else
                    {
                        foreach (Carton2RCARD carton2RCard in carton2RCardList)
                        {
                            StackToRcard stackToRcard = (StackToRcard)inventoryFacade.GetStackToRcard(carton2RCard.Rcard.Trim().ToUpper());

                            if (stackToRcard != null)
                            {
                                string content = GetContentFromStackToRCard(stackToRcard);
                                if (!ImportOneLine(string.Empty, content, inventoryFacade))
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
                else if (ultraOptionSetScanType.CheckedItem.DataValue.ToString() == INPUT_RUNNINGCARD)
                {
                    if (this.ucLabelEdit.Value.Trim().Length <= 0)
                    {
                        ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + this.ucLabelEdit.Value.Trim(), new UserControl.Message(MessageType.Error, "$CS_PleaseInputID"), true);
                        this.ucLabelEdit.TextFocus(false, true);
                        return;
                    }

                    DataCollectFacade dataCollectFacade = new DataCollectFacade(DataProvider);
                    string sourceRcard = dataCollectFacade.GetSourceCard(this.ucLabelEdit.Value.Trim().ToUpper(), string.Empty);

                    StackToRcard stackToRcard = (StackToRcard)inventoryFacade.GetStackToRcard(sourceRcard);

                    //if (stackToRcard == null)
                    //{
                    //    SimulationReport simulationReport = dataCollectFacade.GetLastSimulationReportByCarton(sourceRcard);
                    //    if (simulationReport != null)
                    //    {
                    //        stackToRcard = (StackToRcard)inventoryFacade.GetStackToRcard(simulationReport.RunningCard);
                    //    }
                    //}

                    if (stackToRcard == null)
                    {
                        ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + this.ucLabelEdit.Value.Trim(), new UserControl.Message(MessageType.Error, "$CS_RCARD_INFO_NOT_EXIST"), true);
                        this.ucLabelEdit.TextFocus(false, true);
                        return;
                    }
                    else
                    {
                        string content = GetContentFromStackToRCard(stackToRcard);
                        ImportOneLine(string.Empty, content, inventoryFacade);
                    }
                }

                this.FillDetailToGrid();
                this.ucLabelEdit.TextFocus(true, true);
            }
        }

        private void cboBusinessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboBusinessType.SelectedItemText.Trim().Length != 0)
            {
                InventoryFacade invFacade = new InventoryFacade(this.DataProvider);

                object rule = invFacade.GetInvBusiness2Formula(this.cboBusinessType.SelectedItemValue.ToString(), OutInvRuleCheck.ProductIsPause, GlobalVariables.CurrentOrganizations.First().OrganizationID);

                if (rule == null)
                {
                    this.txtPauseCancelReason.ReadOnly = false;
                    this.txtPauseCancelReason.Value = "";
                    this.chkPauseCancel.Checked = true;
                }
                else
                {
                    this.txtPauseCancelReason.ReadOnly = true;
                    this.txtPauseCancelReason.Value = "";
                    this.chkPauseCancel.Checked = false;
                }
            }
            else
            {
                this.txtPauseCancelReason.ReadOnly = true;
                this.txtPauseCancelReason.Value = "";
                this.chkPauseCancel.Checked = false;
            }
        }

        private void chkPauseCancel_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkPauseCancel.Checked)
            {
                this.txtPauseCancelReason.Value = "";
                this.txtPauseCancelReason.ReadOnly = false;
            }
            else
            {
                this.txtPauseCancelReason.Value = "";
                this.txtPauseCancelReason.ReadOnly = true;
            }
        }

        private void checkBoxSelectAllError_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gridErrorInfo.Rows.Count; i++)
            {
                gridErrorInfo.Rows[i].Cells["check"].Value = checkBoxSelectAllError.Checked;
            }
        }

        private void ucButtonSelectFile_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (!CheckInfoGrid())
            {
                return;
            }

            ImportFromFile(this.txtFile.Value);
        }

        private void btnOutInv_Click(object sender, EventArgs e)
        {
            if (!CheckInfoGrid())
            {
                return;
            }

            if (this._DataTableDetail.Rows.Count == 0)
            {
                //Message:无出库信息
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_NO_OUT_INV_DATA"));
                this.btnOutInv.Focus();
                return;
            }

            if (this.gridErrorInfo.Rows.Count != 0)
            {
                //Message:出货时不能存在异常序列号
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_OUT_INV_DATA_ERROR"));
                this.btnOutInv.Focus();
                return;
            }

            InventoryFacade invFacade = new InventoryFacade(this.DataProvider);

            //非SAP完工不能出库
            if (!CheckNeedSAPCompelete())
            {
                return;
            }

            string strBusinessType;
            if (this.cboBusinessType.SelectedItemText.Trim().Length == 0)
            {
                strBusinessType = " ";
            }
            else
            {
                strBusinessType = this.cboBusinessType.SelectedItemValue.ToString();
            }

            string dnConfirmSAP = this.cboBusinessType.SelectedItemText.Trim().Length == 0 ? string.Empty : this.cboBusinessType.SelectedItemValue.ToString().Trim();


            invFacade.OutInventory(this.gridDNInfo.ActiveRow.Cells["dnno"].Value.ToString().Trim(), strBusinessType, FormatHelper.CleanString(this.txtMemo.Value.Trim()), ApplicationService.Current().LoginInfo.UserCode, this.txtPauseCancelReason.Value.Trim(), this._DataTableHead.Copy(), this._DataTableDetail.Copy(), dnConfirmSAP);

            this.txtMemo.Value = "";
            //this.txtFile.Value = "";

            this._DataTableDNList.Clear();
            this._DataSetInfo.Clear();
            this._DataTableHead.Clear();
            this._DataTableDetail.Clear();
            this._DataTableErrorInfo.Clear();
            this._DataTableFile.Clear();

            this.SetErrorRunningCardQty(0);

            //Message:出库成功
            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_OUT_INV_SUCCESS"));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.txtLabel.Value = "";
            this.txtLabel.TextFocus(true, true);
            this.txtTicketNo.Value = "";
            this.cboBusinessType.SelectedIndex = -1;
            this.txtLabelRelation.Value = "";
            this.txtDept.Value = "";
            this.txtShipToParty.Value = "";
            this.txtTicketMemo.Value = "";
            this.txtMemo.Value = "";
            //this.txtFile.Value = "";

            this.chkPauseCancel.Checked = false;
            this.chkPauseCancel.Enabled = false;
            this.txtPauseCancelReason.ReadOnly = true;

            this._DataTableDNList.Clear();
            this._DataSetInfo.Clear();
            this._DataTableHead.Clear();
            this._DataTableDetail.Clear();
            this._DataTableErrorInfo.Clear();
            this._DataTableFile.Clear();

            this.SetErrorRunningCardQty(0);
        }

        private void ucButtonClearError_Click(object sender, EventArgs e)
        {
            checkBoxSelectAllError.Focus();

            for (int i = gridErrorInfo.Rows.Count - 1; i >= 0; i--)
            {
                if (bool.Parse(gridErrorInfo.Rows[i].Cells["check"].Value.ToString()))
                {
                    gridErrorInfo.Rows[i].Delete(false);
                }
            }

            SetErrorRunningCardQty(gridErrorInfo.Rows.Count);
            checkBoxSelectAllError.Checked = false;
        }

        #endregion

        #region 函数

        private void InitialBusinessType()
        {
            InventoryFacade objFacade = new InventoryFacade(this.DataProvider);
            object[] invBusinessList = objFacade.GetInvBusiness("", BussinessType.type_out);

            this.cboBusinessType.Clear();
            if (invBusinessList != null)
            {
                foreach (InvBusiness obj in invBusinessList)
                {
                    this.cboBusinessType.AddItem(obj.BusinessDescription, obj.BusinessCode);
                }
            }

        }

        private void InitialDataSource()
        {
            _DataSetInfo = new DataSet();
            _DataTableHead = new DataTable();
            this._DataTableHead.Columns.Add("seq");
            this._DataTableHead.Columns.Add("itemcode");
            this._DataTableHead.Columns.Add("itemdesc");
            this._DataTableHead.Columns.Add("storagecode");
            this._DataTableHead.Columns.Add("planqty");
            this._DataTableHead.Columns.Add("alreadysendedqty");
            this._DataTableHead.Columns.Add("sendedqty");
            this._DataTableHead.Columns.Add("mocode");
            this._DataTableHead.Columns.Add("reworkmocode");
            this._DataTableHead.Columns.Add("contractno");

            this._DataTableHead.Columns.Add("lineno");
            this._DataTableHead.Columns.Add("dnno");
            this._DataTableHead.Columns.Add("cusorderno");
            this._DataTableHead.Columns.Add("businesscode");
            
            this._DataTableHead.TableName = "Head";

            _DataTableDetail = new DataTable();
            this._DataTableDetail.Columns.Add("stackcode");
            this._DataTableDetail.Columns.Add("palletcode");
            this._DataTableDetail.Columns.Add("rcard");
            this._DataTableDetail.Columns.Add("sendedqty");
            this._DataTableDetail.Columns.Add("scandate");
            this._DataTableDetail.Columns.Add("itemcode");
            this._DataTableDetail.Columns.Add("storagecode");
            this._DataTableDetail.Columns.Add("company");
            this._DataTableDetail.Columns.Add("cartoncode");
            this._DataTableDetail.Columns.Add("mocode");
            this._DataTableDetail.Columns.Add("pausecode");
            this._DataTableDetail.Columns.Add("seq");
            this._DataTableDetail.TableName = "Detail";

            this._DataSetInfo.Tables.Add(this._DataTableHead);
            this._DataSetInfo.Tables.Add(this._DataTableDetail);

            this.gridInfo.DataSource = this._DataSetInfo;

            this._DataTableDNList = new DataTable();
            this._DataTableDNList.Columns.Add("dnno");
            this.gridDNInfo.DataSource = this._DataTableDNList;

            _DataTableFile = _DataTableDetail.Clone();

            this._DataTableErrorInfo = new DataTable();
            this._DataTableErrorInfo.Columns.Add("check");
            this._DataTableErrorInfo.Columns.Add("filelinenumber");
            this._DataTableErrorInfo.Columns.Add("storagecode");
            this._DataTableErrorInfo.Columns.Add("stackcode");
            this._DataTableErrorInfo.Columns.Add("palletcode");
            this._DataTableErrorInfo.Columns.Add("itemcode");
            this._DataTableErrorInfo.Columns.Add("itemdesc");
            this._DataTableErrorInfo.Columns.Add("reason");
            this._DataTableErrorInfo.Columns.Add("rcard");
            this._DataTableErrorInfo.Columns.Add("company");
            this._DataTableErrorInfo.Columns.Add("scandate");

            this.gridErrorInfo.DataSource = this._DataTableErrorInfo;

        }

        private void ClearUI()
        {
            this.txtLabelRelation.Value = "";
            this.txtDept.Value = "";
            this.txtShipToParty.Value = "";
            this.txtTicketMemo.Value = "";

            this._DataTableDNList.Clear();
            this._DataSetInfo.Clear();
            this._DataTableHead.Clear();
            this._DataTableDetail.Clear();
            this._DataTableErrorInfo.Clear();
            this._DataTableFile.Clear();

            this.SetErrorRunningCardQty(0);
        }

        private void LoadUIHead(string dnno)
        {
            DeliveryFacade objFacade = new DeliveryFacade(this.DataProvider);
            object[] dnList = null;

            if (this.ultraOptionSetERPMES.CheckedItem.DataValue.ToString().Equals(PACK_ERP))
            {

                dnList = objFacade.GetActiveDeliveryNoteHeadList("", dnno, DNFrom.ERP);

                if (dnList == null)
                {
                    //Message:送货单不存在
                    ApplicationRun.GetInfoForm().AddEx(_FunctionName, "送货单:" + dnno,
                        new UserControl.Message(MessageType.Error, "$CS_DN_NOT_EXIST"), true);
                    this.txtLabel.TextFocus(true, true);
                    return;
                }

                LoadGridHeadInfo(dnno, DNFrom.ERP);
            }
            else if (this.ultraOptionSetERPMES.CheckedItem.DataValue.ToString().Equals(PACK_MES))
            {
                dnList = objFacade.GetActiveDeliveryNoteHeadList("", dnno, DNFrom.MES);

                if (dnList == null)
                {
                    //Message:送货单不存在
                    ApplicationRun.GetInfoForm().AddEx(_FunctionName, "送货单:" + dnno,
                        new UserControl.Message(MessageType.Error, "$CS_DN_NOT_EXIST"), true);
                    this.txtTicketNo.TextFocus(true, true);
                    return;
                }

                LoadGridHeadInfo(dnno, DNFrom.MES);
            }

            this.txtLabelRelation.Value = ((DeliveryNote)dnList[0]).RelatedDocument;
            this.txtDept.Value = ((DeliveryNote)dnList[0]).Dept;
            this.txtShipToParty.Value = ((DeliveryNote)dnList[0]).ShipTo;
            this.txtTicketMemo.Value = ((DeliveryNote)dnList[0]).Memo;
            //this.cboBusinessType.SelectedItemText = ((DeliveryNote)dnList[0]).BusinessCode;
            SetBusinessCode(((DeliveryNote)dnList[0]).BusinessCode);
        }

        private void SetBusinessCode(string businessCode)
        {
            InventoryFacade objFacade = new InventoryFacade(this.DataProvider);
            object invBusiness = objFacade.GetInvBusiness(businessCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);

            if (invBusiness != null)
            {
                cboBusinessType.SetSelectItem(businessCode);
            }
        }

        private void LoadDnList(object[] deliveryNoteList)
        {
            this._DataTableDNList.Clear();
            string beforeDNNo = "";

            foreach (DeliveryNote obj in deliveryNoteList)
            {
                if (!beforeDNNo.Equals(obj.DNCode))
                {
                    DataRow dr = this._DataTableDNList.NewRow();
                    dr["dnno"] = obj.DNCode;
                    this._DataTableDNList.Rows.Add(dr);
                    beforeDNNo = obj.DNCode;
                }
            }
        }

        private void LoadGridHeadInfo(string dnNo, string dnFrom)
        {
            object[] dnList = null;

            DeliveryFacade objFacade = new DeliveryFacade(this.DataProvider);

            if (dnFrom.Equals(DNFrom.ERP))
            {
                dnList = objFacade.GetActiveDeliveryNoteDetailList(dnNo, DNFrom.ERP);
            }
            else if (dnFrom.Equals(DNFrom.MES))
            {

                dnList = objFacade.GetActiveDeliveryNoteDetailList(dnNo, DNFrom.MES);
            }

            if (dnList == null)
            {
                //Message:送货单明细不存在
                ApplicationRun.GetInfoForm().AddEx(_FunctionName, "送货单:" + dnNo,
                    new UserControl.Message(MessageType.Error, "$CS_DN_DETAIL_NOT_EXIST"), true);
                this.txtTicketNo.TextFocus(true, true);
                return;
            }

            this._DataSetInfo.Clear();
            this._DataTableHead.Clear();
            this._DataTableDetail.Clear();
            this._DataTableErrorInfo.Clear();
            this._DataTableFile.Clear();

            this.SetErrorRunningCardQty(0);


            if (dnList != null)
            {
                int i = 1;

                ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                string itemDesc = string.Empty;

                ////Add Grid Parent data
                //
                foreach (DeliveryNote dn in dnList)
                {
                    itemDesc = string.Empty;
                    object item = itemFacade.GetMaterial(dn.ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                    if (item != null)
                    {
                        itemDesc = ((BenQGuru.eMES.Domain.MOModel.Material)item).MaterialDescription;
                    }

                    DataRow dr = this._DataTableHead.NewRow();
                    dr["seq"] = i;
                    dr["itemcode"] = dn.ItemCode;
                    dr["itemdesc"] = itemDesc;
                    dr["storagecode"] = dn.FromStorage;
                    dr["mocode"] = dn.MOCode;
                    dr["reworkmocode"] = dn.ReworkMOCode;
                    dr["contractno"] = dn.OrderNo;
                    dr["planqty"] = dn.DNQuantity;
                    dr["alreadysendedqty"] = dn.RealQuantity;
                    dr["sendedqty"] = 0;
                    dr["lineno"] = dn.DNLine;
                    dr["dnno"] = dn.DNCode;
                    dr["cusorderno"] = dn.CustomerOrderNo;
                    dr["businesscode"] = dn.BusinessCode;

                    this._DataTableHead.Rows.Add(dr);
                    i = i + 1;
                }
            }
            this.gridInfo.DataSource = this._DataSetInfo;
        }

        private void OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "打开(Open)";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            //ofd.Filter = "XML文件(*.xml)|*.xml";
            ofd.ValidateNames = true;     //文件有效性验证ValidateNames，验证用户输入是否是一个有效的Windows文件名
            ofd.CheckFileExists = true;  //验证路径有效性
            ofd.CheckPathExists = true; //验证文件有效性
            ofd.ShowDialog();

            if (ofd.FileName.Trim().Length == 0)
            {
                //this.txtFile.Value = string.Empty;
                //this.btnImport.Enabled = false;
            }
            else
            {
                this.txtFile.Value = ofd.FileName;
                this.btnImport.Enabled = true;
            }
        }

        private bool ImportOneLine(string fileLineNumber, string contentLine, InventoryFacade invFacade)
        {
            bool returnValue = false;

            string[] arryContent = contentLine.Split(',');

            if (arryContent.Length != 5)
            {
                //Message:文件格式不正确
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CS_File_Format_Error"));
                return returnValue;
            }

            //string strItemGrade = GetValue(arryContent[0]);
            string strOrderNo = GetValue(arryContent[0]);
            string strRcard = GetValue(arryContent[1]);
            string strQty = GetValue(arryContent[2]);
            string strTime = GetValue(arryContent[3]);
            string strDate = GetValue(arryContent[4]);

            if (strOrderNo.Trim().Length > 0 && !this.CheckOrderNo(strOrderNo))
            {
                //Message:订单号无法和出库单匹配
                return returnValue;
            }

            object[] rcardInfo = invFacade.GetStackPalletInfoByDNFileRcard(strRcard);
            if (rcardInfo == null)
            {
                //Message:序列号对应的相关库房信息不存在
                ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + strRcard,
                    new UserControl.Message(MessageType.Error, "$CS_RCARD_INFO_NOT_EXIST $CS_Param_RunSeq=" + strRcard), true);
                AddErrorInfo(fileLineNumber, "", "", "", "", "", strRcard, "", strDate + " " + strTime, UserControl.MutiLanguages.ParserMessage("$CS_RCARD_INFO_NOT_EXIST"));
                return returnValue;
            }

            if (strQty.Trim().Length == 0)
            {
                //Message:数量格式不对
                ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                    new UserControl.Message(MessageType.Error, "$CS_QTY_ERROR_FORMAT " + strQty), true);
                AddErrorInfo(fileLineNumber,
                             ((RcardToStackPallet)rcardInfo[0]).StorageCode,
                             ((RcardToStackPallet)rcardInfo[0]).StackCode,
                             ((RcardToStackPallet)rcardInfo[0]).PalletCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemDescription,
                             ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                             ((RcardToStackPallet)rcardInfo[0]).Company,
                             strDate + " " + strTime,
                             UserControl.MutiLanguages.ParserMessage("$CS_QTY_ERROR_FORMAT ") + strQty);

                return returnValue;
            }

            decimal decResult;

            if (!decimal.TryParse(strQty, out decResult))
            {
                //Message:数量格式不对
                ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                    new UserControl.Message(MessageType.Error, "$CS_QTY_ERROR_FORMAT " + strQty), true);
                AddErrorInfo(fileLineNumber,
                             ((RcardToStackPallet)rcardInfo[0]).StorageCode,
                             ((RcardToStackPallet)rcardInfo[0]).StackCode,
                             ((RcardToStackPallet)rcardInfo[0]).PalletCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemDescription,
                             ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                             ((RcardToStackPallet)rcardInfo[0]).Company,
                             strDate + " " + strTime,
                             UserControl.MutiLanguages.ParserMessage("$CS_QTY_ERROR_FORMAT ") + strQty);
                return returnValue;
            }



            ////检查序列号是否有重复数据
            //
            if (CheckRcardExist(strRcard))
            {
                //Message:序列号重复
                ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                    new UserControl.Message(MessageType.Error, "$CS_RCARD_DUPLICATIED $CS_Param_RunSeq=" + strRcard), true);
                AddErrorInfo(fileLineNumber,
                             ((RcardToStackPallet)rcardInfo[0]).StorageCode,
                             ((RcardToStackPallet)rcardInfo[0]).StackCode,
                             ((RcardToStackPallet)rcardInfo[0]).PalletCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemDescription,
                             ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                             ((RcardToStackPallet)rcardInfo[0]).Company,
                             strDate + " " + strTime,
                             UserControl.MutiLanguages.ParserMessage("$CS_RCARD_DUPLICATIED"));
                return returnValue;
            }

            ////检查序列号是否有重复数据
            //
            if (CheckRcardExistInGrid(strRcard))
            {
                //Message:序列号重复
                ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                    new UserControl.Message(MessageType.Error, "$CS_RCARD_DUPLICATIED_GRID $CS_Param_RunSeq=" + strRcard), true);
                AddErrorInfo(fileLineNumber,
                             ((RcardToStackPallet)rcardInfo[0]).StorageCode,
                             ((RcardToStackPallet)rcardInfo[0]).StackCode,
                             ((RcardToStackPallet)rcardInfo[0]).PalletCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemDescription,
                             ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                             ((RcardToStackPallet)rcardInfo[0]).Company,
                             strDate + " " + strTime,
                             UserControl.MutiLanguages.ParserMessage("$CS_RCARD_DUPLICATIED_GRID"));
                return returnValue;
            }



            if (!this.CheckFileInfo(((RcardToStackPallet)rcardInfo[0]).ItemCode, ((RcardToStackPallet)rcardInfo[0]).StorageCode))
            {
                //Message:产品、产品档次、库别无法和出库单匹配
                ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                    new UserControl.Message(MessageType.Error, "$CS_FILE_INFO_ERROR $CS_Param_RunSeq=" + strRcard), true);
                AddErrorInfo(fileLineNumber,
                             ((RcardToStackPallet)rcardInfo[0]).StorageCode,
                             ((RcardToStackPallet)rcardInfo[0]).StackCode,
                             ((RcardToStackPallet)rcardInfo[0]).PalletCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemDescription,
                             ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                             ((RcardToStackPallet)rcardInfo[0]).Company,
                             strDate + " " + strTime,
                             UserControl.MutiLanguages.ParserMessage("$CS_FILE_INFO_ERROR"));
                return returnValue;
            }

            int intHeadSeq = 0;
            if (!this.CheckMaxOutQty(((RcardToStackPallet)rcardInfo[0]).ItemCode, ((RcardToStackPallet)rcardInfo[0]).StorageCode, 1, ref intHeadSeq))
            {
                //Message:出库数量超出最大值
                ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                    new UserControl.Message(MessageType.Error, "$CS_OVER_OUTINV_MAX_QTY $CS_Param_RunSeq=" + strRcard), true);
                AddErrorInfo(fileLineNumber,
                             ((RcardToStackPallet)rcardInfo[0]).StorageCode,
                             ((RcardToStackPallet)rcardInfo[0]).StackCode,
                             ((RcardToStackPallet)rcardInfo[0]).PalletCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemDescription,
                             ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                             ((RcardToStackPallet)rcardInfo[0]).Company,
                             strDate + " " + strTime,
                             UserControl.MutiLanguages.ParserMessage("$CS_OVER_OUTINV_MAX_QTY"));
                return returnValue;
            }

            string pauseCode = string.Empty;

            ////检查是否需要解除停发
            //
            if (this.chkPauseCancel.Checked)
            {
                SimulationReport simulationReport = (SimulationReport)invFacade.GetSimulationReportByRcardOrCartonCode(strRcard);
                string rcard = strRcard;
                if (simulationReport != null)
                {
                    rcard = simulationReport.RunningCard;
                }

                object[] rcardIsPause = invFacade.GetRcardIsPause(rcard);

                if (rcardIsPause != null)
                {
                    if (this.txtPauseCancelReason.Value.Trim().Length == 0)
                    {
                        //Message:请输入停发原因
                        ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                            new UserControl.Message(MessageType.Error, "$CS_INPUT_CANCEL_PAUSE_REASON  $CS_Param_RunSeq=" + strRcard), true);
                        AddErrorInfo(fileLineNumber,
                             ((RcardToStackPallet)rcardInfo[0]).StorageCode,
                             ((RcardToStackPallet)rcardInfo[0]).StackCode,
                             ((RcardToStackPallet)rcardInfo[0]).PalletCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemDescription,
                             ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                             ((RcardToStackPallet)rcardInfo[0]).Company,
                             strDate + " " + strTime,
                             UserControl.MutiLanguages.ParserMessage("$CS_INPUT_CANCEL_PAUSE_REASON"));
                        return returnValue;
                    }
                    else
                    {
                        ////解除停发
                        //
                        pauseCode = ((Pause2Rcard)rcardIsPause[0]).PauseCode;
                    }
                }
            }

            ////检查设定的规则是否满足
            //
            if (!CheckRule(strRcard))
            {
                //Message:规则检查未通过
                ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                    new UserControl.Message(MessageType.Error, "$CS_CheckRuleError $CS_Param_RunSeq=" + strRcard), true);
                AddErrorInfo(fileLineNumber,
                             ((RcardToStackPallet)rcardInfo[0]).StorageCode,
                             ((RcardToStackPallet)rcardInfo[0]).StackCode,
                             ((RcardToStackPallet)rcardInfo[0]).PalletCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemCode,
                             ((RcardToStackPallet)rcardInfo[0]).ItemDescription,
                             ((RcardToStackPallet)rcardInfo[0]).SerialNo,
                             ((RcardToStackPallet)rcardInfo[0]).Company,
                             strDate + " " + strTime,
                             UserControl.MutiLanguages.ParserMessage("$CS_CheckRuleError"));
                return returnValue;
            }

            DataRow dr = this._DataTableFile.NewRow();

            dr["stackcode"] = ((RcardToStackPallet)rcardInfo[0]).StackCode;
            dr["palletcode"] = ((RcardToStackPallet)rcardInfo[0]).PalletCode;
            dr["rcard"] = ((RcardToStackPallet)rcardInfo[0]).SerialNo;
            dr["sendedqty"] = Convert.ToDecimal(strQty);
            dr["scandate"] = strDate + " " + strTime;
            dr["itemcode"] = ((RcardToStackPallet)rcardInfo[0]).ItemCode;
            dr["storagecode"] = ((RcardToStackPallet)rcardInfo[0]).StorageCode;
            dr["company"] = ((RcardToStackPallet)rcardInfo[0]).Company;
            dr["cartoncode"] = ((RcardToStackPallet)rcardInfo[0]).Cartoncode;
            dr["mocode"] = ((RcardToStackPallet)rcardInfo[0]).MOCode;
            dr["pausecode"] = pauseCode;
            dr["seq"] = intHeadSeq;

            this._DataTableFile.Rows.Add(dr);

            returnValue = true;
            return returnValue;
        }

        private void ImportFromFile(string fileName)
        {
            if (fileName.Trim().Length <= 0 || (!System.IO.File.Exists(fileName.Trim())))
            {
                //Message:文件不存在
                ApplicationRun.GetInfoForm().AddEx(_FunctionName, "文件名:" + fileName, new UserControl.Message(MessageType.Error, "$File_Not_Exist"), true);
                this.btnImport.Focus();
                return;
            }

            StreamReader sr = System.IO.File.OpenText(fileName.Trim());
            String contentLine = string.Empty;

            //第一行过滤掉，不要读
            contentLine = sr.ReadLine();

            InventoryFacade invFacade = new InventoryFacade(this.DataProvider);

            int fileLinenumber = 1;
            //读入数据
            while ((contentLine = sr.ReadLine()) != null)
            {
                fileLinenumber++;
                if (contentLine.Trim().IndexOf("(") < 0 || contentLine.Trim().IndexOf(")") < 0 || contentLine.Trim().IndexOf(",") < 0)
                {
                    //Message:文件格式不正确
                    ApplicationRun.GetInfoForm().AddEx(_FunctionName, "文件名:" + fileName, new UserControl.Message(MessageType.Error, "$Error_CS_File_Format_Error"), true);
                    this.btnImport.Focus();
                    sr.Close();
                    return;
                }

                contentLine = contentLine.Trim().Substring(1, contentLine.Trim().Length - 2);

                if (contentLine.Split(',').Length != 5)
                {
                    //Message:文件格式不正确
                    ApplicationRun.GetInfoForm().AddEx(_FunctionName, "文件名:" + fileName, new UserControl.Message(MessageType.Error, "$Error_CS_File_Format_Error"), true);
                    this.btnImport.Focus();
                    sr.Close();
                    return;
                }

                if (!ImportOneLine(Convert.ToString(fileLinenumber), contentLine, invFacade))
                {
                    continue;
                }
            }

            sr.Close();

            //Load Detail Grid
            this.FillDetailToGrid();
            //this.btnImport.Enabled = false;
            //this.txtFile.Value = string.Empty;
        }

        private void AddErrorInfo(string fileLineNumber, string storageCode, string stackCode, string palletCode, string itemCode, string itemDesc, string rcard, string company, string scanDate, string reason)
        {
            DataRow dr = this._DataTableErrorInfo.NewRow();

            dr["check"] = false;
            dr["filelinenumber"] = fileLineNumber;
            dr["storagecode"] = storageCode;
            dr["stackcode"] = stackCode;
            dr["palletcode"] = palletCode;
            dr["itemcode"] = itemCode;
            dr["itemdesc"] = itemDesc;
            dr["rcard"] = rcard;
            if (company.Trim().Length == 0)
            {
                dr["company"] = company;
            }
            else
            {
                dr["company"] = this.GetCompanyDesc(company);
            }
            
            dr["scandate"] = scanDate;
            dr["reason"] = reason;

            this._DataTableErrorInfo.Rows.Add(dr);

            this.SetErrorRunningCardQty(gridErrorInfo.Rows.Count);
        }

        private bool CheckMaxOutQty(string itemCode, string storageCode, decimal qtyInThisLine, ref Int32 seq)
        {
            decimal maxQty = 0;

            bool result = false;

            DataRow[] drHeadList = this._DataTableHead.Select("itemcode='" + itemCode + "' and storagecode = '" + storageCode + "'");

            if (drHeadList.Length != 0)
            {
                for (int i = drHeadList.Length - 1; i >= 0; i--)
                {
                    decimal cachedQty = GetFileTotalQty(Convert.ToInt32(drHeadList[i]["seq"].ToString()), itemCode, storageCode);

                    maxQty = Convert.ToDecimal(drHeadList[i]["planqty"].ToString()) - Convert.ToDecimal(drHeadList[i]["alreadysendedqty"].ToString());

                    if (maxQty < cachedQty + qtyInThisLine)
                    {
                    }
                    else
                    {
                        result = true;
                        seq = Convert.ToInt32(drHeadList[i]["seq"].ToString());
                    }
                }


            }

            return result;
        }

        private void FillDetailToGrid()
        {
            _DataTableDetail.Clear();
            foreach (DataRow dr in this._DataTableFile.Rows)
            {
                DataRow drDetail = this._DataTableDetail.NewRow();
                drDetail["stackcode"] = dr["stackcode"];
                drDetail["palletcode"] = dr["palletcode"];
                drDetail["rcard"] = dr["rcard"];
                drDetail["sendedqty"] = dr["sendedqty"];
                drDetail["scandate"] = dr["scandate"];
                drDetail["itemcode"] = dr["itemcode"];
                drDetail["storagecode"] = dr["storagecode"];
                drDetail["company"] = dr["company"];
                drDetail["cartoncode"] = dr["cartoncode"];
                drDetail["mocode"] = dr["mocode"];
                drDetail["pausecode"] = dr["pausecode"];
                drDetail["seq"] = dr["seq"];
                this._DataTableDetail.Rows.Add(drDetail);
            }
            //decimal sendedQty = 0;
            foreach (DataRow dr in this._DataTableHead.Rows)
            {
                dr["sendedqty"] = this.GetTotalQty(Convert.ToInt32(dr["seq"]), dr["itemcode"].ToString(), dr["storagecode"].ToString());
                //sendedQty = this.GetTotalQty(dr["itemcode"].ToString(), dr["itemgrade"].ToString(), dr["storagecode"].ToString());

            }

            DataColumn[] dataColumnHead = new DataColumn[3];
            dataColumnHead[0] = this._DataSetInfo.Tables["Head"].Columns["seq"];
            dataColumnHead[1] = this._DataSetInfo.Tables["Head"].Columns["itemcode"];
            //dataColumnHead[2] = this._DataSetInfo.Tables["Head"].Columns["itemgrade"];
            dataColumnHead[2] = this._DataSetInfo.Tables["Head"].Columns["storagecode"];

            DataColumn[] dataColumnDetail = new DataColumn[3];
            dataColumnDetail[0] = this._DataSetInfo.Tables["Detail"].Columns["seq"];
            dataColumnDetail[1] = this._DataSetInfo.Tables["Detail"].Columns["itemcode"];
            //dataColumnDetail[2] = this._DataSetInfo.Tables["Detail"].Columns["itemgrade"];
            dataColumnDetail[2] = this._DataSetInfo.Tables["Detail"].Columns["storagecode"];

            //if (!this._DataSetInfo.Relations.CanRemove(this._DataSetInfo.Relations["relation"]))
            //{
            this.gridInfo.DataSource = this._DataSetInfo;

            if (this._DataTableDetail.Rows.Count != 0)
            {
                if (this._DataSetInfo.Relations.IndexOf("relation") < 0)
                {
                    this._DataSetInfo.Relations.Add("relation", dataColumnHead, dataColumnDetail, false);
                }
            }

            this.gridInfo.UpdateData();
            this.gridInfo.Refresh();
            //}
        }

        private bool CheckOrderNo(string custOrderNo)
        {
            DataRow[] drHeadList = this._DataTableHead.Select("cusorderno = '" + custOrderNo + "'");

            if (drHeadList.Length == 0)
            {
                return false;
            }

            return true;
        }

        private bool CheckFileInfo(string itemCode, string storageCode)
        {
            DataRow[] drHeadList = this._DataTableHead.Select("itemcode='" + itemCode + "' and storagecode = '" + storageCode + "'");

            if (drHeadList.Length == 0)
            {
                return false;
            }            

            return true;
        }

        private decimal GetTotalQty(int seq, string itemCode, string storageCode)
        {
            decimal qty = 0;
            DataRow[] drList = this._DataTableDetail.Select("seq='" + seq + "' and  itemcode='" + itemCode + "' and storagecode = '" + storageCode + "'");

            if (drList.Length != 0)
            {
                foreach (DataRow dr in drList)
                {
                    qty = qty + 1;
                }
            }

            return qty;
        }

        private decimal GetFileTotalQty(int seq, string itemCode, string storageCode)
        {
            decimal qty = 0;
            DataRow[] drList = this._DataTableFile.Select("seq='" + seq + "' and itemcode='" + itemCode + "' and storagecode = '" + storageCode + "'");

            if (drList.Length != 0)
            {
                foreach (DataRow dr in drList)
                {
                    qty = qty + 1;
                }
            }

            return qty;
        }

        private bool CheckRcardExist(string inputNo)
        {
            if (this._DataTableFile.Select("rcard = '" + inputNo + "'").Length != 0)
            {
                return true;
            }
            else
            {
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                SimulationReport simulationReport = dataCollectFacade.GetLastSimulationReportByCarton(inputNo.Trim().ToUpper());
                if (simulationReport != null)
                {
                    if (this._DataTableFile.Select("rcard = '" + simulationReport.RunningCard + "'").Length != 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private bool CheckRcardExistInGrid(string inputNo)
        {
            if (this._DataTableDetail.Select("rcard = '" + inputNo + "'").Length != 0)
            {
                return true;
            }

            return false;
        }

        private void InitializeCompany()
        {
            SystemSettingFacade systemFacade = new SystemSettingFacade(this.DataProvider);
            object[] objList = systemFacade.GetParametersByParameterGroup("COMPANYLIST");
            if (objList != null)
            {
                //this.cboCompany.Clear();
                //foreach (Parameter para in objList)
                //{
                //    this.cboCompany.AddItem(para.ParameterDescription, para.ParameterAlias);
                //}

                _CompanyList = objList;
            }
        }

        private string GetCompanyDesc(string companyCode)
        {
            foreach (Parameter objPara in this._CompanyList)
            {
                if (objPara.ParameterAlias.Equals(companyCode))
                {
                    return objPara.ParameterDescription;
                }
            }

            return "";
        }

        private bool CheckRule(string inputNo)
        {
            if (this.cboBusinessType.SelectedItemText.Trim().Length == 0)
            {
                return true;
            }

            InventoryFacade invFacade = new InventoryFacade(this.DataProvider);

            object[] ruleList = invFacade.GetInvBusiness2Formula(this.cboBusinessType.SelectedItemValue.ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID);

            if (ruleList == null)
            {
                return true;
            }

            foreach (InvBusiness2Formula rule in ruleList)
            {
                ////检查产品必须完工
                //
                if (rule.FormulaCode.Equals(OutInvRuleCheck.ProductIsFinished))
                {
                    if (!invFacade.CheckRcardIsFinished(inputNo))
                    {
                        //Message:产品序列号未完工
                        ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + inputNo,
                            new UserControl.Message(MessageType.Error, "$CS_RCARD_IS_NOT_FINISHED $CS_Param_RunSeq=" + inputNo), true);
                        return false;
                    }
                }

                ////检查产品不能完工
                //
                if (rule.FormulaCode.Equals(OutInvRuleCheck.ProductIsNotFinished))
                {
                    if (!invFacade.CheckRcardIsNotFinished(inputNo))
                    {
                        //Message:该产品已完工
                        ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + inputNo,
                            new UserControl.Message(MessageType.Error, "$CS_ERROR_PRODUCT_ALREADY_COMPLETE $CS_Param_RunSeq=" + inputNo), true);
                        return false;
                    }
                }

                ////检查产品是否已经被隔离
                //
                if (rule.FormulaCode.Equals(OutInvRuleCheck.ProductIsFrozen))
                {
                    if (invFacade.CheckRcardIsFrozen(inputNo))
                    {
                        //Message:序列号已经被隔离
                        ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + inputNo,
                            new UserControl.Message(MessageType.Error, "$CS_RCARD_IS_FROZEN  $CS_Param_RunSeq=" + inputNo), true);
                        return false;
                    }
                }

                ////检查产品是否已经被下地
                //
                if (rule.FormulaCode.Equals(OutInvRuleCheck.ProductIsDown))
                {
                    if (invFacade.CheckRcardIsDown(inputNo))
                    {
                        //Message:序列号已经被下地
                        ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + inputNo,
                            new UserControl.Message(MessageType.Error, "$CS_RCARD_IS_DOWN  $CS_Param_RunSeq=" + inputNo), true);
                        return false;
                    }
                }

                ////检查产品是否已经被停发
                //
                if (rule.FormulaCode.Equals(OutInvRuleCheck.ProductIsPause))
                {
                    SimulationReport simulationReport = (SimulationReport)invFacade.GetSimulationReportByRcardOrCartonCode(inputNo);
                    string rcard = inputNo;
                    if (simulationReport != null)
                    {
                        rcard = simulationReport.RunningCard;
                    }

                    object[] rcardIsPause = invFacade.GetRcardIsPause(rcard);
                    if (rcardIsPause != null)
                    {
                        //Message:序列号已经被停发
                        ApplicationRun.GetInfoForm().AddEx(_FunctionName, "产品序列号:" + inputNo,
                            new UserControl.Message(MessageType.Error, "$CS_RCARD_IS_PAUSE  $CS_Param_RunSeq=" + inputNo), true);
                        return false;
                    }
                }

                //检查是否同步SAP
                if (rule.FormulaCode.Equals(OutInvRuleCheck.DNConfirmSAP))
                {
                    DeliveryFacade deliveryFacade = new DeliveryFacade(this.DataProvider);
                    object[] DNS = deliveryFacade.GetDNForClose(FormatHelper.CleanString(this.txtTicketNo.Value.Trim().ToUpper()));
                    if (DNS != null && ((DeliveryNote)DNS[0]).DNFrom == DNFrom.MES)
                    {
                        //检查是否同步SAP
                        ApplicationRun.GetInfoForm().AddEx(_FunctionName, "DN单号:" + this.txtTicketNo.Value.Trim().ToUpper(),
                           new UserControl.Message(MessageType.Error, "$CS_DN_DIMCONFIG_WRONG  $CS_DNNO=" + this.txtTicketNo.Value.Trim().ToUpper()), true);
                        return false;
                    }
                }


                //非SAP完工不能出库    
                if (rule.FormulaCode.Equals(OutInvRuleCheck.InSAPNotCompeleted))
                {
                    if (!invFacade.CheckSAPConmpelete(inputNo))
                    {
                        ApplicationRun.GetInfoForm().Add(
                                   new UserControl.Message(MessageType.Error, inputNo + "$CS_OUT_INV_DATA_Not_SAP_COMPELETE"));
                        return false;
                    }
                }

            }

            return true;
        }

        private string GetValue(string content)
        {
            if (content.Trim().Length == 0)
            {
                return "";
            }
            else if (content.Trim().IndexOf("'") < 0)
            {
                return content;
            }
            else
            {
                return content.Substring(1, content.Length - 2);
            }
        }

        private void SetErrorRunningCardQty(int qty)
        {
            ucLabelEditErrorRunningCardQty.Value = qty.ToString();
            //tabControlGrids.TabPages[1].Text = ERRORRUNNINGCARD;
            if (qty > 0)
            {
                tabControlGrids.TabPages[1].Text += "(" + qty.ToString() + ")";
            }
        }

        private string GetContentFromStackToRCard(StackToRcard stackToRcard)
        {
            string returnValue = string.Empty;
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DateTime dateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

            //returnValue += " ,";
            returnValue += " ,";
            returnValue += stackToRcard.SerialNo + ",";
            returnValue += "1,";
            returnValue += dateTime.ToString("MM-dd-yyyy") + ",";
            returnValue += dateTime.ToString("hh:mm:ss");

            return returnValue;
        }

        private bool CheckInfoGrid()
        {
            if (this.gridInfo.Rows.Count == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_GRID_NO_RECORD"));
                return false;
            }

            if (cboBusinessType.SelectedItemText.Trim().Length <= 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_BUSINESS_CODE_INPUT"));
                return false;
            }

            return true;
        }

        private bool CheckNeedSAPCompelete()
        {
            InventoryFacade invFacade = new InventoryFacade(this.DataProvider);

            if (this.cboBusinessType.SelectedItemText.Trim().Length == 0)
            {
                return true;
            }

            object[] ruleList = invFacade.GetInvBusiness2Formula(this.cboBusinessType.SelectedItemValue.ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID);

            if (ruleList == null)
            {
                return true;
            }

            string unSAPCompeleteRcards = string.Empty;

            for (int i = 0; i < this._DataTableDetail.Rows.Count; i++)
            {
                DataRow row = _DataTableDetail.Rows[i];
                string serialNo = row["rcard"].ToString();
                if (!invFacade.CheckSAPConmpelete(serialNo))
                {
                    unSAPCompeleteRcards += serialNo + ",";
                }
            }

            foreach (InvBusiness2Formula rule in ruleList)
            {
                //非SAP完工不能出库                
                if (rule.FormulaCode.Equals(OutInvRuleCheck.InSAPNotCompeleted) && !string.IsNullOrEmpty(unSAPCompeleteRcards))
                {
                    ApplicationRun.GetInfoForm().Add(
                                  new UserControl.Message(MessageType.Error, unSAPCompeleteRcards + "$CS_OUT_INV_DATA_Not_SAP_COMPELETE"));
                    return false;
                }
            }

            return true;
        }
        #endregion

        private void ucButtonExp_Click(object sender, EventArgs e)
        {
            checkBoxSelectAllError.Focus();

            bool Ischeck = false;

            if (gridErrorInfo.Rows.Count == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_GRID_NO_RECORD"));
                return;
            }

            for (int k = gridErrorInfo.Rows.Count - 1; k >= 0; k--)
            {
                if (bool.Parse(gridErrorInfo.Rows[k].Cells["check"].Value.ToString()))
                {
                    Ischeck = true;
                }
            }


            if (Ischeck == false)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_RCard_Empty"));
                return;
            }


            FileDialog fd = new SaveFileDialog();
            fd.AddExtension = true;
            //fd.
            fd.Filter = "XLS数据文件|*.xls";

            DialogResult diaResult = fd.ShowDialog();

            string fileName = fd.FileName;

            if (diaResult == DialogResult.OK && fileName != String.Empty)
            {
                try
                {
                    System.IO.FileStream fs = System.IO.File.Create(fileName);

                    string colName = String.Empty;

                    for (int j = 1; j < gridErrorInfo.DisplayLayout.Bands[0].Columns.Count; j++)
                    {
                        colName += gridErrorInfo.DisplayLayout.Bands[0].Columns[j].Header.Caption + "\t";
                    }

                    colName = colName.Substring(0, colName.Length - 1);
                    colName += "\r\n";

                    byte[] btCol = System.Text.Encoding.Default.GetBytes(colName);
                    fs.Write(btCol, 0, btCol.Length);

                    for (int i = 0; i < gridErrorInfo.Rows.Count; i++)
                    {
                        string rowData = String.Empty;

                        if (bool.Parse(gridErrorInfo.Rows[i].Cells["check"].Value.ToString()))
                        {
                            rowData = gridErrorInfo.Rows[i].Cells["filelinenumber"].Value + "\t"
                                 + gridErrorInfo.Rows[i].Cells["storagecode"].Value + "\t"
                                 + gridErrorInfo.Rows[i].Cells["stackcode"].Value + "\t"
                                 + gridErrorInfo.Rows[i].Cells["palletcode"].Value + "\t"
                                 + gridErrorInfo.Rows[i].Cells["itemcode"].Value + "\t"
                                 + gridErrorInfo.Rows[i].Cells["itemdesc"].Value + "\t"
                                 + gridErrorInfo.Rows[i].Cells["reason"].Value + "\t"
                                 + gridErrorInfo.Rows[i].Cells["rcard"].Value + "\t"
                                 + gridErrorInfo.Rows[i].Cells["company"].Value + "\t"
                                 //+ gridErrorInfo.Rows[i].Cells["itemgrade"].Value + "\t"
                                 + gridErrorInfo.Rows[i].Cells["scandate"].Value;

                            rowData = rowData + "\r\n";

                            byte[] bt = System.Text.Encoding.Default.GetBytes(rowData);
                            fs.Write(bt, 0, bt.Length);
                        }
                    }
                    fs.Flush();
                    fs.Close();

                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_EXPSUCESS"));


                }
                catch (Exception ex)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_EXCELPROCESS_ERROR"));
                    return;
                }
                finally
                {

                }
            }

        }

    }
}