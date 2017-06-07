using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using System.Collections;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Rework;
using BenQGuru.eMES.Domain.Rework;

namespace BenQGuru.eMES.Client
{
    public partial class FOQCOffLot : BaseForm
    {
        private DataSet m_ResultList = null;
        private DataTable m_PalletList = null;
        private DataTable m_RCardList = null;

        private ProductInfo product = null;
        private OQCLot m_OldLot = null;
        private OQCFacade m_OQCFacade;
        private ReworkFacade m_reworkFacade;

        public OQCFacade oqcFacade
        {
            get
            {
                if (this.m_OQCFacade == null)
                {
                    this.m_OQCFacade = new OQCFacade(this.DataProvider);
                }
                return m_OQCFacade;
            }
        }

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FOQCOffLot()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
            this.ultraGridRCardList.UpdateMode = UpdateMode.OnCellChange;
            this.ultraGridRCardList.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            this.ultraGridRCardList.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridRCardList.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridRCardList.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridRCardList.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridRCardList.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridRCardList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridRCardList.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridRCardList.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridRCardList.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void InitializeRCardListGrid()
        {
            this.m_ResultList = new DataSet();
            this.m_PalletList = new DataTable("PalletList");
            this.m_RCardList = new DataTable("RCardList");

            this.m_PalletList.Columns.Add("Checked", typeof(string));
            this.m_PalletList.Columns.Add("PalletCode", typeof(string));
            this.m_PalletList.Columns.Add("MaintainDate", typeof(int));
            this.m_PalletList.Columns.Add("MaintainTime", typeof(int));

            this.m_RCardList.Columns.Add("Checked", typeof(string));
            this.m_RCardList.Columns.Add("PalletCode", typeof(string));
            this.m_RCardList.Columns.Add("RCardCode", typeof(string));
            this.m_RCardList.Columns.Add("CartonCode", typeof(string));
            this.m_RCardList.Columns.Add("IsSample", typeof(string));
            this.m_RCardList.Columns.Add("Result", typeof(string));
            this.m_RCardList.Columns.Add("MaintainDate", typeof(int));
            this.m_RCardList.Columns.Add("MaintainTime", typeof(int));

            this.m_ResultList.Tables.Add(this.m_PalletList);
            this.m_ResultList.Tables.Add(this.m_RCardList);

            this.m_ResultList.Relations.Add(new DataRelation("PalletAndRCard",
                                                this.m_ResultList.Tables["PalletList"].Columns["PalletCode"],
                                                this.m_ResultList.Tables["RCardList"].Columns["PalletCode"]));
            this.m_ResultList.AcceptChanges();
            this.ultraGridRCardList.DataSource = this.m_ResultList;
        }

        private void ultraGridRCardList_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

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
            e.Layout.Bands[0].ScrollTipField = "PalletCode";
            e.Layout.Bands[1].ScrollTipField = "RCardCode";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["PalletCode"].Header.Caption = "栈板号";
            e.Layout.Bands[0].Columns["MaintainDate"].Header.Caption = "产生日期";
            e.Layout.Bands[0].Columns["MaintainTime"].Header.Caption = "产生时间";
            e.Layout.Bands[0].Columns["Checked"].Width = 40;
            e.Layout.Bands[0].Columns["PalletCode"].Width = 300;
            e.Layout.Bands[0].Columns["MaintainDate"].Width = 100;
            e.Layout.Bands[0].Columns["MaintainTime"].Width = 100;
            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["PalletCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MaintainDate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MaintainTime"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            // 允许冻结，且Checked栏位始终处于冻结状态，不可更改
            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["PalletCode"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["PalletCode"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;

            e.Layout.Bands[0].Columns["PalletCode"].SortIndicator = SortIndicator.Ascending;

            // RCard List
            e.Layout.Bands[1].Columns["PalletCode"].Hidden = true;
            e.Layout.Bands[1].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[1].Columns["RCardCode"].Header.Caption = "序列号";
            e.Layout.Bands[1].Columns["CartonCode"].Header.Caption = "箱号";
            e.Layout.Bands[1].Columns["IsSample"].Header.Caption = "是否样本";
            e.Layout.Bands[1].Columns["Result"].Header.Caption = "检验结果";
            e.Layout.Bands[1].Columns["MaintainDate"].Header.Caption = "入批日期";
            e.Layout.Bands[1].Columns["MaintainTime"].Header.Caption = "入批时间";

            e.Layout.Bands[1].Columns["Checked"].Width = 40;
            e.Layout.Bands[1].Columns["RCardCode"].Width = 250;
            e.Layout.Bands[1].Columns["CartonCode"].Width = 250;
            e.Layout.Bands[1].Columns["IsSample"].Width = 80;
            e.Layout.Bands[1].Columns["Result"].Width = 80;
            e.Layout.Bands[1].Columns["MaintainDate"].Width = 100;
            e.Layout.Bands[1].Columns["MaintainTime"].Width = 100;

            e.Layout.Bands[1].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[1].Columns["RCardCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["CartonCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["IsSample"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["Result"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["MaintainDate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["MaintainTime"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["RCardCode"].SortIndicator = SortIndicator.Ascending;

            e.Layout.Bands[1].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[1].Columns["RCardCode"].Header.Fixed = true;
            e.Layout.Bands[1].Columns["RCardCode"].AllowRowFiltering = DefaultableBoolean.True;
            e.Layout.Bands[1].Columns["RCardCode"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;

            this.InitGridLanguage(ultraGridRCardList);
        }

        private void ultraGridRCardList_CellChange(object sender, CellEventArgs e)
        {
            this.ultraGridRCardList.UpdateData();
            if (e.Cell.Column.Key == "Checked")
            {
                if (e.Cell.Row.Band.Index == 0) //Parent
                {
                    for (int i = 0; i < e.Cell.Row.ChildBands[0].Rows.Count; i++)
                    {
                        e.Cell.Row.ChildBands[0].Rows[i].Cells["Checked"].Value = e.Cell.Value;
                    }
                }

                if (e.Cell.Row.Band.Index == 1) // Child
                {
                    if (Convert.ToBoolean(e.Cell.Value) == true)
                    {
                        e.Cell.Row.ParentRow.Cells["Checked"].Value = e.Cell.Value;
                    }
                    else
                    {
                        bool needUnCheckHeader = true;
                        for (int i = 0; i < e.Cell.Row.ParentRow.ChildBands[0].Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(e.Cell.Row.ParentRow.ChildBands[0].Rows[i].Cells["Checked"].Value) == true)
                            {
                                needUnCheckHeader = false;
                                break;
                            }
                        }
                        if (needUnCheckHeader)
                        {
                            e.Cell.Row.ParentRow.Cells["Checked"].Value = e.Cell.Value;
                        }
                    }
                }
            }
            this.ultraGridRCardList.UpdateData();

            DataView dv = this.m_ResultList.Tables["RCardList"].DefaultView;
            dv.RowFilter = "Checked='True'";
            this.ucLabEditSelectedCount.Value = dv.Count.ToString();
        }

        private void ucLabelEditOldLotNo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.LoadLotInfo();
            }
        }

        private void LoadLotInfo()
        {
            if (this.ucLabelEditOldLotNo.Value.Trim().Length == 0)
            {
                ApplicationRun.GetInfoForm().Add("$CS_FQCLOT_NOT_NULL");
                InitializeUIControl();
                this.ucLabelEditOldLotNo.TextFocus(false, true);
                return;
            }

            string lotNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelEditOldLotNo.Value));

            if (!this.CanDoOffLotEvent(lotNo))
            {
                InitializeUIControl();
                this.ucLabelEditOldLotNo.TextFocus(false, true);
                return;
            }

            Messages messages = new Messages();
            ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            ((SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            try
            {
                object[] objs = oqcFacade.GetOQCLot2CardByLotNoAndSeq(lotNo, OQCFacade.Lot_Sequence_Default);
                if (objs == null || objs.Length == 0)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_LotNoRCard"));
                    InitializeUIControl();
                    ucLabelEditOldLotNo.TextFocus(false, true);
                    return;
                }

                messages = this.GetProduct((objs[0] as OQCLot2Card).RunningCard);
                if (!messages.IsSuccess() || product == null || product.LastSimulation == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$NoProductInfo"));
                    InitializeUIControl();
                    ucLabelEditOldLotNo.TextFocus(false, true);
                    return;
                }

                object obj = oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(lotNo), OQCFacade.Lot_Sequence_Default);
                OQCLot lot = obj as OQCLot;
                string itemCode = (objs[0] as OQCLot2Card).ItemCode;
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);

                object item = itemFacade.GetItem(itemCode, lot.OrganizationID);
                if (item == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_ItemCode_NotExist $ItemCode=" + itemCode));
                    InitializeUIControl();
                    ucLabelEditOldLotNo.TextFocus(false, true);
                    return;
                }

                this.ucLabelEditItemCode.Value = (item as Item).ItemCode;
                this.labelItemDescription.Text = (item as Item).ItemDescription;

                // Load Pallet and RCard List
                messages.AddMessages(this.LoadPalletAndRCardList(lotNo));
                if (!messages.IsSuccess())
                {
                    ApplicationRun.GetInfoForm().Add(messages);
                    InitializeUIControl();
                    ucLabelEditOldLotNo.TextFocus(false, true);
                    return;
                }

                this.ucLabelEditNewLotNo.Value = this.GenerateNewLotNo().LOTNO;
                this.ucLabEditLotQty.Value = this.m_OldLot.LotSize.ToString();
                this.ucLabEditSelectedCount.Value = "0";

                // Call the Method
                messages.AddMessages(this.CheckRework());
                if (!messages.IsSuccess())
                {
                    ApplicationRun.GetInfoForm().Add(messages);
                    InitializeUIControl();
                    ucLabelEditOldLotNo.TextFocus(false, true);
                    return;
                }

                this.ucLabelEditInput.TextFocus(true, true);
            }
            catch (Exception ex)
            {
                ApplicationRun.GetInfoForm().Add(ex.Message);
                InitializeUIControl();
                ucLabelEditOldLotNo.TextFocus(false, true);
                this.product = null;
            }
            finally
            {
                ((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
            }

        }

        private Messages GetProduct(string rcard)
        {
            Messages productmessages = new Messages();
            ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
            productmessages.AddMessages(dataCollect.GetIDInfo(rcard.Trim().ToUpper()));
            if (productmessages.IsSuccess())
            {
                product = (ProductInfo)productmessages.GetData().Values[0];
            }
            dataCollect = null;
            return productmessages;
        }

        private bool CanDoOffLotEvent(string lotNo)
        {
            OQCLot lot = this.oqcFacade.GetOQCLot(lotNo, OQCFacade.Lot_Sequence_Default) as OQCLot;
            if (lot == null)
            {
                ApplicationRun.GetInfoForm().Add("$OQCLot_Not_Exist $CS_LotNo=" + lotNo);
                return false;
            }
            else
            {
                if (lot.LOTStatus != OQCLotStatus.OQCLotStatus_Initial &&
                    lot.LOTStatus != OQCLotStatus.OQCLotStatus_Examing &&
                    lot.LOTStatus != OQCLotStatus.OQCLotStatus_NoExame &&
                    lot.LOTStatus != OQCLotStatus.OQCLotStatus_SendExame)
                {
                    ApplicationRun.GetInfoForm().Add("$CS_LotHasRejectOrNotValid $CS_LotNo=" + lotNo);
                    return false;
                }
            }
            m_OldLot = lot as OQCLot;
            return true;
        }

        private OQCLot GenerateNewLotNo()
        {
            string beginLotNo = "";
            string endLotNo = "";
            string oldLotNo = product.LastSimulation.LOTNO;

            if (oldLotNo.Substring(oldLotNo.Length - 3, 1) == "_")
            {
                beginLotNo = oldLotNo;
                endLotNo = oldLotNo.Substring(0, oldLotNo.Length - 2) + "99";
            }
            else
            {
                beginLotNo = oldLotNo + "_00";
                endLotNo = oldLotNo + "_99";
            }

            string itemCode = product.LastSimulation.ItemCode;
            Item item = (new ItemFacade(this.DataProvider)).GetItem(itemCode,
                GlobalVariables.CurrentOrganizations.First().OrganizationID) as Item;

            if (item == null)
            {
                throw new Exception("$Error_ItemCode_NotExist $Domain_ItemCode=" + itemCode);
            }

            try
            {
                OQCLot maxOQCLot = this.oqcFacade.GetMaxLotForOffLot(beginLotNo,
                    endLotNo, ApplicationService.Current().UserCode, this.m_OldLot, item.OrganizationID);

                return maxOQCLot;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ultraGridRCardList_BeforeRowFilterDropDownPopulate(object sender, BeforeRowFilterDropDownPopulateEventArgs e)
        {
            // 去除默认筛选项、自定义筛选
            if (e.Column.Key == "PalletCode" && e.Column.Band.Index == 0)
            {
                e.ValueList.ValueListItems.RemoveAt(1);
                e.ValueList.ValueListItems.RemoveAt(1);
                e.ValueList.ValueListItems.RemoveAt(1);
            }
        }

        private void FOQCOffLot_Load(object sender, EventArgs e)
        {
            this.InitializeRCardListGrid();
            //this.InitPageLanguage();
        }

        private void ucButtonQuery_Click(object sender, EventArgs e)
        {
            this.LoadLotInfo();
        }

        private void ClearPalletAndRCardList()
        {
            if (this.m_ResultList == null)
            {
                return;
            }
            this.m_ResultList.Tables["RCardList"].Rows.Clear();
            this.m_ResultList.Tables["PalletList"].Rows.Clear();
            this.m_ResultList.Tables["RCardList"].AcceptChanges();
            this.m_ResultList.Tables["PalletList"].AcceptChanges();
            this.m_ResultList.AcceptChanges();
        }

        private Messages LoadPalletAndRCardList(string lotNo)
        {
            Messages msg = new Messages();
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            try
            {
                this.ClearPalletAndRCardList();

                object[] result = oqcFacade.GetDataSourceForOffLot(lotNo);
                if (result != null)
                {
                    string palletCode = "";
                    string rcard = "";
                    DataRow rowPallet, rowRCard;
                    Hashtable palletList = new Hashtable(), rcardList = new Hashtable();

                    foreach (OQCCardCartonAndPallet cardCartonAndPallet in result)
                    {
                        palletCode = cardCartonAndPallet.PalletCode;
                        rcard = cardCartonAndPallet.RunningCard;

                        if (!palletList.ContainsKey(palletCode))
                        {
                            palletList.Add(palletCode, cardCartonAndPallet);
                        }

                        if (!rcardList.ContainsKey(rcard))
                        {
                            rcardList.Add(rcard, cardCartonAndPallet);
                        }
                        else
                        {
                            OQCCardCartonAndPallet ccap = rcardList[rcard] as OQCCardCartonAndPallet;
                            if (ccap.Status == ProductStatus.GOOD && cardCartonAndPallet.Status == ProductStatus.NG)
                            {
                                rcardList.Remove(rcard);
                                rcardList.Add(rcard, cardCartonAndPallet);
                            }
                        }
                    }

                    foreach (OQCCardCartonAndPallet cardCartonAndPallet in palletList.Values)
                    {
                        rowPallet = this.m_ResultList.Tables["PalletList"].NewRow();
                        rowPallet["Checked"] = "false";
                        rowPallet["PalletCode"] = cardCartonAndPallet.PalletCode;
                        rowPallet["MaintainDate"] = cardCartonAndPallet.PalletDate;
                        rowPallet["MaintainTime"] = cardCartonAndPallet.PalletTime;

                        this.m_ResultList.Tables["PalletList"].Rows.Add(rowPallet);
                    }

                    foreach (OQCCardCartonAndPallet cardCartonAndPallet in rcardList.Values)
                    {
                        rowRCard = this.m_ResultList.Tables["RCardList"].NewRow();

                        rowRCard["Checked"] = "false";
                        rowRCard["PalletCode"] = cardCartonAndPallet.PalletCode;
                        rowRCard["RCardCode"] = cardCartonAndPallet.RunningCard;
                        rowRCard["CartonCode"] = cardCartonAndPallet.CartonCode;
                        rowRCard["IsSample"] = cardCartonAndPallet.IsSample;
                        if (string.Compare(cardCartonAndPallet.IsSample, "Y", true) == 0)
                        {
                            rowRCard["Result"] = cardCartonAndPallet.Status == ProductStatus.GOOD ? 
                                MutiLanguages.ParserString(ProductStatus.GOOD) : MutiLanguages.ParserString(ProductStatus.NG);
                        }
                        else
                        {
                            rowRCard["Result"] = "";
                        }
                        rowRCard["MaintainDate"] = cardCartonAndPallet.MaintainDate;
                        rowRCard["MaintainTime"] = cardCartonAndPallet.MaintainTime;

                        this.m_ResultList.Tables["RCardList"].Rows.Add(rowRCard);
                    }
                    this.m_ResultList.Tables["PalletList"].AcceptChanges();
                    this.m_ResultList.Tables["RCardList"].AcceptChanges();
                    this.m_ResultList.AcceptChanges();
                    this.ultraGridRCardList.DataSource = this.m_ResultList;
                }
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
            }
            return msg;
        }

        private void ucLabelEditInput_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.ucLabelEditInput.Value.Trim().Length == 0)
                {
                    this.ucLabelEditInput.TextFocus(false, true);
                    return;
                }

                if (this.m_OldLot == null || this.ucLabelEditOldLotNo.Value.Trim().Length == 0)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_FQCLOT_NOT_NULL"));
                    this.ucLabelEditInput.Value = "";
                    this.ucLabelEditOldLotNo.TextFocus(false, true);
                    return;
                }

                if (this.ultraGridRCardList.ActiveRow != null)
                {
                    this.ultraGridRCardList.ActiveRow.Selected = false;
                    this.ultraGridRCardList.ActiveRow = null;
                }

                string inputValue = this.ucLabelEditInput.Value.Trim();
                bool hasChecked = false;
                DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);

                #region For  Carton Code
                if (this.radioButtonCartonCode.Checked)   // Carton
                {
                    if (this.checkBoxFrozen.Checked)
                    {
                        object[] sims = dcf.GetSimulationFromCarton(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(inputValue)));
                        if (sims == null || sims.Length == 0)
                        {
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CartonNotUsed"));
                            this.ucLabelEditInput.TextFocus(false, true);
                            return;
                        }

                        foreach (Simulation sim in sims)
                        {
                            object[] frozenList = this.m_OQCFacade.QueryFrozen(sim.RunningCard, sim.RunningCard,
                            sim.LOTNO, sim.MOCode, sim.ItemCode, FrozenStatus.STATUS_FRONZEN, 0, 0, 0, 0, 0, int.MaxValue);

                            if (frozenList == null || frozenList.Length == 0)
                            {
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CartonRCardNotFrozen"));
                                this.ucLabelEditInput.TextFocus(false, true);
                                return;
                            }
                        }
                    }

                    bool dialogShown = false;
                    bool unCheck = true;
                    for (int i = 0; i < this.ultraGridRCardList.Rows.Count; i++)
                    {
                        if (this.ultraGridRCardList.Rows[i].HasChild(false))
                        {
                            for (int j = 0; j < this.ultraGridRCardList.Rows[i].ChildBands[0].Rows.Count; j++)
                            {
                                if (string.Compare(inputValue,
                                    this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["CartonCode"].Value.ToString(), true) == 0)
                                {
                                    hasChecked = true;

                                    if (string.Compare(this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"].Value.ToString(), "true", true) == 0)
                                    {
                                        if (dialogShown && unCheck)
                                        {
                                            this.ultraGridRCardList.Rows[i].Expanded = true;
                                            this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"].Value = "false";
                                            //this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Selected = true;
                                            this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Activate();

                                            this.ultraGridRCardList_CellChange(this.ultraGridRCardList,
                                                new CellEventArgs(this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"]));
                                        }
                                        else
                                        {
                                            dialogShown = true;
                                            frmDialog dialog = new frmDialog();
                                            dialog.Text = this.Text;
                                            dialog.DialogMessage = UserControl.MutiLanguages.ParserMessage("$Info_CancelSelected");

                                            if (DialogResult.OK == dialog.ShowDialog(this))
                                            {
                                                unCheck = true;
                                                this.ultraGridRCardList.Rows[i].Expanded = true;
                                                this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"].Value = "false";
                                                //this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Selected = true;
                                                this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Activate();

                                                this.ultraGridRCardList_CellChange(this.ultraGridRCardList,
                                                    new CellEventArgs(this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"]));
                                            }
                                            else
                                            {
                                                unCheck = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.ultraGridRCardList.Rows[i].Expanded = true;
                                        this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"].Value = "true";
                                        //this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Selected = true;
                                        this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Activate();

                                        this.ultraGridRCardList_CellChange(this.ultraGridRCardList,
                                            new CellEventArgs(this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"]));
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region For  RCard Code
                if (this.rBRCard.Checked) // RCard
                {
                    //将产品当前序列号转换为原始的序列号
                    string sourceRCard = dcf.GetSourceCard(inputValue.Trim().ToUpper(), string.Empty);

                    if (this.checkBoxFrozen.Checked)
                    {
                        object objSimReport = dcf.GetLastSimulationReport(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(sourceRCard)));
                        if (objSimReport == null)
                        {
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$NoProductInfo"));
                            this.ucLabelEditInput.TextFocus(false, true);
                            return;
                        }
                        SimulationReport sim = objSimReport as SimulationReport;

                        object[] frozenList = this.m_OQCFacade.QueryFrozen(sim.RunningCard, sim.RunningCard,
                            sim.LOTNO, sim.MOCode, sim.ItemCode, FrozenStatus.STATUS_FRONZEN, 0, 0, 0, 0, 0, int.MaxValue);

                        if (frozenList == null || frozenList.Length == 0)
                        {
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_RCardNotForzen"));
                            this.ucLabelEditInput.TextFocus(false, true);
                            return;
                        }
                    }

                    for (int i = 0; i < this.ultraGridRCardList.Rows.Count; i++)
                    {
                        if (this.ultraGridRCardList.Rows[i].HasChild(false))
                        {
                            for (int j = 0; j < this.ultraGridRCardList.Rows[i].ChildBands[0].Rows.Count; j++)
                            {
                                if (string.Compare(inputValue,
                                    this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["RCardCode"].Value.ToString(), true) == 0)
                                {
                                    hasChecked = true;

                                    if (string.Compare(this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"].Value.ToString(), "true", true) == 0)
                                    {
                                        frmDialog dialog = new frmDialog();
                                        dialog.Text = this.Text;
                                        dialog.DialogMessage = UserControl.MutiLanguages.ParserMessage("$Info_CancelSelected");

                                        if (DialogResult.OK == dialog.ShowDialog(this))
                                        {
                                            this.ultraGridRCardList.Rows[i].Expanded = true;
                                            this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"].Value = "false";
                                            //this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Selected = true;
                                            this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Activate();

                                            this.ultraGridRCardList_CellChange(this.ultraGridRCardList,
                                                new CellEventArgs(this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"]));
                                        }
                                    }
                                    else
                                    {
                                        this.ultraGridRCardList.Rows[i].Expanded = true;
                                        this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"].Value = "true";
                                        //this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Selected = true;
                                        this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Activate();

                                        this.ultraGridRCardList_CellChange(this.ultraGridRCardList,
                                            new CellEventArgs(this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"]));
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                #endregion

                this.ultraGridRCardList.UpdateData();

                if (!hasChecked)
                {
                    string message = "";
                    if (this.radioButtonCartonCode.Checked)
                    {
                        message = "$Error_CartonNotInLot";
                    }
                    if (this.rBRCard.Checked)
                    {
                        message = "$Error_RCardNotInLot";
                    }
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, message));
                }

                this.ucLabelEditInput.TextFocus(false, true);
            }
        }

        private void ucButtonSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(MutiLanguages.ParserString("$CS_Confirm_Split_Lot"), MutiLanguages.ParserString("$ShowMessage"), MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                if (ValidInput())
                {
                    ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                    ((SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();

                    DataProvider.BeginTransaction();
                    try
                    {
                        DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                        DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                        string oldLotNo = this.m_OldLot.LOTNO;

                        // 重新产生新的Lot，防止并发
                        OQCLot newLot = this.GenerateNewLotNo();

                        if (this.oqcFacade.GetOQCLot(newLot.LOTNO, newLot.LotSequence) == null)
                        {
                            // 保存新的批
                            this.oqcFacade.AddOQCLot(newLot);

                            // 保存Try Code 列表
                            if (this.m_OldLot.ProductionType == ProductionType.ProductionType_Try)
                            {
                                TryFacade tryFacade = new TryFacade(this.DataProvider);
                                object[] tryList = tryFacade.GetTry2LotList(this.m_OldLot.LOTNO);

                                if (tryList != null && tryList.Length > 0)
                                {
                                    Try2Lot newTry2Lot;
                                    foreach (Try2Lot try2lot in tryList)
                                    {
                                        newTry2Lot = tryFacade.CreateNewTry2Lot();
                                        newTry2Lot.EAttribute1 = try2lot.EAttribute1;
                                        newTry2Lot.LotNo = newLot.LOTNO;
                                        newTry2Lot.MaintainDate = currentDateTime.DBDate;
                                        newTry2Lot.MaintainTime = currentDateTime.DBTime;
                                        newTry2Lot.MaintainUser = ApplicationService.Current().UserCode;
                                        newTry2Lot.TryCode = try2lot.TryCode;

                                        tryFacade.AddTry2Lot(newTry2Lot);
                                    }
                                }
                            }
                        }

                        string newLotNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(newLot.LOTNO));
                        Messages msg = new Messages();
                        foreach (DataRow row in this.m_ResultList.Tables["RCardList"].Rows)
                        {
                            if (string.Compare(Convert.ToString(row["Checked"]), "true", true) == 0)
                            {
                                string rCard = Convert.ToString(row["RCardCode"]).Trim();

                                msg = this.GetProduct(rCard);
                                if (!msg.IsSuccess() || product == null || product.LastSimulation == null)
                                {
                                    throw new Exception("$NoProductInfo $CS_Param_ID=" + rCard);
                                }
                                string moCode = product.LastSimulation.MOCode;

                                if (this.ucLabelEditUnfrozenReason.Checked)  // Unfrozen
                                {
                                    this.oqcFacade.UnfreezeRCard(rCard, oldLotNo,
                                        OQCFacade.Lot_Sequence_Default, moCode,
                                        product.LastSimulation.ItemCode,
                                        FormatHelper.CleanString(this.ucLabelEditUnfrozenReason.Value, 100),
                                        ApplicationService.Current().UserCode, currentDateTime);
                                }

                                // Update Simulation and Simulation Report
                                dataCollectFacade.UpdateSimulationForLot(rCard, moCode, newLotNo);
                                dataCollectFacade.UpdateSimulationReportForLot(rCard, moCode, newLotNo);

                                // Update tblLot2Card
                                this.oqcFacade.UpdateOQCLot2CardLotNo(oldLotNo, rCard, OQCFacade.Lot_Sequence_Default, moCode, newLotNo);
                                // Update tblLot.LotSize
                                this.oqcFacade.UpdateOQCLotSizeForOffLot(oldLotNo, newLotNo, OQCFacade.Lot_Sequence_Default);

                                if (string.Compare(Convert.ToString(row["IsSample"]).Trim(), "Y", true) == 0)
                                {
                                    // Update tbllot2cardcheck.LotNo
                                    this.oqcFacade.UpdateOQCLot2CardForOffLot(moCode, rCard, oldLotNo, newLotNo, OQCFacade.Lot_Sequence_Default);
                                    // Update tbloqclotcard2errorcode.LotNo
                                    this.oqcFacade.UpdateOQCLotCard2ErrorCodeForOffLot(rCard, oldLotNo, newLotNo, moCode, OQCFacade.Lot_Sequence_Default);
                                    // Update tbloqccardlotcklist.lotno
                                    this.oqcFacade.UpdateOQCLotCardCheckListForOffLot(moCode, rCard, oldLotNo, newLotNo, OQCFacade.Lot_Sequence_Default);
                                }
                            }
                        }

                        // Update tbloqclotcklist for old lot (ABCZ Grade Summary)
                        this.oqcFacade.UpdateOQCLotCheckListForOffLot(oldLotNo);
                        if (this.oqcFacade.GetOQCLOTCheckList(newLotNo, OQCFacade.Lot_Sequence_Default) == null)
                        {
                            // Insert new tbloqclotcklist for new lot (ABCZ Grade Summary)
                            OQCLOTCheckList newLotCheckList = oqcFacade.CreateNewOQCLOTCheckList();
                            newLotCheckList.AGradeTimes = OQCFacade.Decimal_Default_value;
                            newLotCheckList.BGradeTimes = OQCFacade.Decimal_Default_value;
                            newLotCheckList.CGradeTimes = OQCFacade.Decimal_Default_value;
                            newLotCheckList.ZGradeTimes = OQCFacade.Decimal_Default_value;
                            newLotCheckList.EAttribute1 = "";
                            newLotCheckList.LOTNO = newLotNo;
                            newLotCheckList.LotSequence = OQCFacade.Lot_Sequence_Default;
                            newLotCheckList.MaintainDate = currentDateTime.DBDate;
                            newLotCheckList.MaintainTime = currentDateTime.DBTime;
                            newLotCheckList.MaintainUser = ApplicationService.Current().UserCode;
                            newLotCheckList.Result = OQCLotStatus.OQCLotStatus_Examing;
                            this.oqcFacade.AddOQCLOTCheckList(newLotCheckList);
                        }

                        // Update tbloqclotcklist for new lot (ABCZ Grade Summary)
                        this.oqcFacade.UpdateOQCLotCheckListForOffLot(newLotNo);

                        //delete tbltempreworlotno and tbltempreworkrcard
                        m_reworkFacade.DeleteLotAndAllRCard(this.m_OldLot.LOTNO);

                        DataProvider.CommitTransaction();

                        ApplicationRun.GetInfoForm().Add("$CS_OffLotOK $CS_NewLotNo=" + this.ucLabelEditNewLotNo.Value);
                        this.LoadLotInfo();
                    }
                    catch (Exception ex)
                    {
                        DataProvider.RollbackTransaction();
                        ApplicationRun.GetInfoForm().Add(ex.Message);
                    }
                    finally
                    {
                        ((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                        ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
                    }
                }
            }
        }

        private void InitializeUIControl()
        {
            this.InitializeRCardListGrid();

            this.ucLabelEditItemCode.Value = "";
            this.labelItemDescription.Text = "";

            this.ucLabEditLotQty.Value = "0";
            this.ucLabEditSelectedCount.Value = "0";
            this.ucLabelEditInput.Value = "";
            this.ucLabelEditNewLotNo.Value = "";
            this.ucLabelEditUnfrozenReason.Value = "";
            this.ucLabelEditUnfrozenReason.Checked = false;
        }

        private bool ValidInput()
        {
            if (this.m_ResultList == null || this.m_OldLot == null || product == null)
            {
                return false;
            }

            if (this.ucLabEditSelectedCount.Value.Trim() == "" || this.ucLabEditSelectedCount.Value.Trim() == "0")
            {
                ApplicationRun.GetInfoForm().Add("$Error_NoDataSelected");
                this.ucLabelEditInput.TextFocus(false, true);
                return false;
            }

            if (!this.CanDoOffLotEvent(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelEditOldLotNo.Value))))
            {
                return false;
            }

            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(DataProvider);
            Parameter paraSystem = (Parameter)systemSettingFacade.GetParameter("ISCHECKPALLETLOT", "CHECKPALLETLOT");
            if (paraSystem != null && paraSystem.ParameterAlias.ToUpper() == "Y")
            {
                bool checkSelectedAllPallet = true;
                for (int i = 0; i < this.ultraGridRCardList.Rows.Count; i++)
                {
                    if (this.ultraGridRCardList.Rows[i].HasChild(false)
                        && this.ultraGridRCardList.Rows[i].Cells["Checked"].Value.ToString().ToLower() == "true"
                        && this.ultraGridRCardList.Rows[i].Cells["PalletCode"].Value.ToString().Trim() != string.Empty)
                    {
                        for (int j = 0; j < this.ultraGridRCardList.Rows[i].ChildBands[0].Rows.Count; j++)
                        {
                            if (this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"].Value.ToString().ToLower() == "false")
                            {
                                checkSelectedAllPallet = false;
                                break;
                            }
                        }
                    }
                }

                if (!checkSelectedAllPallet)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_MustSelectedAllPallet"));
                    this.ucLabelEditInput.TextFocus(false, true);
                    return false;
                }
            }
            return true;
        }

        private void radioButtonRCard_Click(object sender, EventArgs e)
        {
            this.ucLabelEditInput.TextFocus(false, true);
        }

        private void radioButtonCartonCode_Click(object sender, EventArgs e)
        {
            this.ucLabelEditInput.TextFocus(false, true);
        }

        private void checkBoxFrozen_CheckedChanged(object sender, EventArgs e)
        {
            this.ucLabelEditInput.TextFocus(false, true);
        }

        private void ucButtonSaveLot_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(MutiLanguages.ParserString("$CS_Confirm_Saving_LotRange"), MutiLanguages.ParserString("$ShowMessage"), MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                if (this.ucLabelEditOldLotNo.Value.ToString().Trim() == string.Empty)
                {
                    ApplicationRun.GetInfoForm().Add("$CS_FQCLOT_NOT_NULL");
                    InitializeUIControl();
                    this.ucLabelEditOldLotNo.TextFocus(false, true);
                    return;
                }

                ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                ((SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();

                DataProvider.BeginTransaction();

                try
                {
                    if (m_reworkFacade == null)
                    {
                        m_reworkFacade = new ReworkFacade(this.DataProvider);
                    }

                    object objReworkLot = m_reworkFacade.GetReworkLotNo(this.ucLabelEditOldLotNo.Value.ToString().Trim());

                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                    ReworkRcard newReworRcard = m_reworkFacade.CreateNewReworkRcard();
                    newReworRcard.LotNO = this.ucLabelEditOldLotNo.Value.ToString().Trim();
                    newReworRcard.Status = ReworkFacade.ReworkLot_Status_NEW;
                    newReworRcard.MaintainUser = ApplicationService.Current().UserCode;
                    newReworRcard.MaintainDate = dbDateTime.DBDate;
                    newReworRcard.MaintainTime = dbDateTime.DBTime;
                    newReworRcard.EAttribute1 = "";

                    string rcards = string.Empty;
                    if (objReworkLot == null)
                    {
                        ReworkLotNo newRework = m_reworkFacade.CreateNewReworkLotNo();
                        newRework.LotNO = this.ucLabelEditOldLotNo.Value.ToString().Trim();
                        newRework.ItemCode = this.ucLabelEditItemCode.Value.ToString().Trim();
                        newRework.Status = ReworkFacade.ReworkLot_Status_NEW;
                        newRework.MaintainUser = ApplicationService.Current().UserCode;
                        newRework.MaintainDate = dbDateTime.DBDate;
                        newRework.MaintainTime = dbDateTime.DBTime;
                        newRework.EAttribute1 = "";
                        m_reworkFacade.AddReworkLotNo(newRework);

                        foreach (DataRow row in this.m_ResultList.Tables["RCardList"].Rows)
                        {
                            if (string.Compare(Convert.ToString(row["Checked"]), "true", true) == 0)
                            {
                                newReworRcard.RCard = FormatHelper.PKCapitalFormat(Convert.ToString(row["RCardCode"]).Trim());
                                newReworRcard.PalletCode = Convert.ToString(row["PalletCode"]) == string.Empty ? " " : Convert.ToString(row["PalletCode"]).Trim();
                                m_reworkFacade.AddReworkRcard(newReworRcard);
                            }
                        }
                    }
                    else
                    {
                        foreach (DataRow row in this.m_ResultList.Tables["RCardList"].Rows)
                        {
                            if (string.Compare(Convert.ToString(row["Checked"]), "true", true) == 0)
                            {
                                newReworRcard.RCard = FormatHelper.PKCapitalFormat(Convert.ToString(row["RCardCode"]).Trim());
                                newReworRcard.PalletCode = Convert.ToString(row["PalletCode"]) == string.Empty ? " " : Convert.ToString(row["PalletCode"]).Trim();
                                rcards += newReworRcard.RCard + ",";
                                object oldReworkRcard = m_reworkFacade.GetReworkRcard(FormatHelper.PKCapitalFormat(this.ucLabelEditOldLotNo.Value.ToString().Trim()), newReworRcard.RCard);
                                if (oldReworkRcard == null)
                                {
                                    m_reworkFacade.AddReworkRcard(newReworRcard);
                                }
                            }
                        }

                        int stringlength = rcards.Length;
                        if (stringlength != 0)
                        {
                            rcards = rcards.Substring(0, stringlength - 1);
                            rcards = rcards.Replace(",", "','");
                        }

                        m_reworkFacade.DeleteReworkRcard(rcards, this.ucLabelEditOldLotNo.Value.ToString().Trim());
                    }

                    //更新状态
                    object[] NowReworkRcard = m_reworkFacade.QueryReworkRcard(FormatHelper.PKCapitalFormat(this.ucLabelEditOldLotNo.Value.ToString().Trim()));
                    objReworkLot = m_reworkFacade.GetReworkLotNo(FormatHelper.PKCapitalFormat(this.ucLabelEditOldLotNo.Value.ToString().Trim()));

                    if (NowReworkRcard == null)
                    {
                        if (objReworkLot != null)
                        {
                            m_reworkFacade.DeleteReworkLotNo((ReworkLotNo)objReworkLot);
                        }
                    }
                    else
                    {
                        int New_SameNumber = 0;
                        int DEAL_SameNumber = 0;
                        for (int i = 0; i < NowReworkRcard.Length; i++)
                        {
                            if (((ReworkRcard)NowReworkRcard[i]).Status == ReworkFacade.ReworkLot_Status_NEW)
                            {
                                New_SameNumber += 1;
                            }
                            if (((ReworkRcard)NowReworkRcard[i]).Status == ReworkFacade.ReworkLot_Status_DEAL)
                            {
                                DEAL_SameNumber += 1;
                            }
                        }

                        if (DEAL_SameNumber == NowReworkRcard.Length)
                        {
                            ((ReworkLotNo)objReworkLot).Status = ReworkFacade.ReworkLot_Status_DEAL;
                        }
                        else
                        {
                            ((ReworkLotNo)objReworkLot).Status = ReworkFacade.ReworkLot_Status_NEW;
                        }
                        m_reworkFacade.UpdateReworkLotNo((ReworkLotNo)objReworkLot);
                    }

                    DataProvider.CommitTransaction();

                    ApplicationRun.GetInfoForm().Add("$CS_SAVE_LOTOFF_SUCCESS");
                }
                catch (Exception ex)
                {
                    DataProvider.RollbackTransaction();
                    ApplicationRun.GetInfoForm().Add(ex.Message);
                }
                finally
                {
                    ((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
                }
            }
        }

        private void ucButtonGetLot_Click(object sender, EventArgs e)
        {
            GetLotNo();
        }

        private void ucLabelEditRcard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                GetLotNo();
            }
        }

        //获取批号, 实际批量/标准批量与备注值
        private void GetLotNo()
        {
            DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
            string rcard = this.ucLabelEditRcard.Value.ToUpper().Trim();

            string sourceRCard = dcf.GetSourceCard(rcard, string.Empty);

            if (rcard != String.Empty)
            {
                object obj = dcf.GetLastSimulationReport(sourceRCard);
                if (obj != null)
                {
                    SimulationReport sim = obj as SimulationReport;

                    string oqcLotNo = sim.LOTNO;
                    this.ucLabelEditOldLotNo.Value = oqcLotNo;

                    this.LoadLotInfo();
                }
                else
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$NoProductInfo"));
                    this.InitializeUIControl();
                    this.ucLabelEditOldLotNo.Value = "";
                    this.ucLabelEditRcard.TextFocus(false, true);
                }
            }
            else
            {
                this.ucLabelEditRcard.TextFocus(false, true);
            }
        }

        private Messages CheckRework()
        {
            //add by hiro 2008/10/16
            Messages msg = new Messages();
            try
            {
                if (m_reworkFacade == null)
                    m_reworkFacade = new ReworkFacade(this.DataProvider);
                object objReworkLot = m_reworkFacade.GetReworkLotNo(this.ucLabelEditOldLotNo.Value.ToString().Trim());
                if (objReworkLot == null)
                {
                    return msg;
                }

                if (((ReworkLotNo)objReworkLot).Status == ReworkFacade.ReworkLot_Status_NEW)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$LOT_NOT_CONFIRM"));
                }

                if (((ReworkLotNo)objReworkLot).Status == ReworkFacade.ReworkLot_Status_DEAL)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$LOT_IS_CONFIRM"));
                }

                object[] objReworkRcard = m_reworkFacade.QueryReworkRcard(this.ucLabelEditOldLotNo.Value.ToString().Trim());
                if (objReworkRcard == null || objReworkRcard.Length == 0)
                {
                    return msg;
                }

                string rCard = string.Empty;
                for (int i = 0; i < this.ultraGridRCardList.Rows.Count; i++)
                {
                    if (this.ultraGridRCardList.Rows[i].HasChild(false))
                    {
                        for (int j = 0; j < this.ultraGridRCardList.Rows[i].ChildBands[0].Rows.Count; j++)
                        {
                            rCard = this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["RCardCode"].Value.ToString();
                            foreach (ReworkRcard rr in objReworkRcard)
                            {
                                if (string.Compare(rCard, rr.RCard, true) == 0)
                                {
                                    this.ultraGridRCardList.Rows[i].Expanded = false;
                                    this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"].Value = "true";
                                    this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Activate();
                                    this.ultraGridRCardList_CellChange(this.ultraGridRCardList,
                                        new CellEventArgs(this.ultraGridRCardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"]));
                                }
                            }
                        }
                    }
                }
                //end 
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
            }
            return msg;
        }
    }
}