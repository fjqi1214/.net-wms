using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Client.Service;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using BenQGuru.eMES.DataCollect;

namespace BenQGuru.eMES.Client
{
    public partial class FUp : BaseForm
    {
        private ProductInfo product = null;
        private DataTable m_EventList = null;
        private DataTable m_RcardList = null;

        private string m_DownCodeSelected = string.Empty;

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }


        public FUp()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(this.ultraGridEventList);
            UserControl.UIStyleBuilder.GridUI(this.ultraGridRCardList);
            this.ultraGridRCardList.UpdateMode = UpdateMode.OnCellChange;
            this.ultraGridRCardList.DisplayLayout.Override.CellClickAction = CellClickAction.Edit;
        }

        private void InitialUltraGridDataSource()
        {
            this.m_EventList = new DataTable();
            this.m_EventList.Columns.Add("EventCode", typeof(string));
            this.m_EventList.Columns.Add("DownQty", typeof(decimal));
            this.m_EventList.Columns.Add("DownReason", typeof(string));
            this.m_EventList.AcceptChanges();

            this.m_RcardList = new DataTable();
            this.m_RcardList.Columns.Add("Checked", typeof(string));
            this.m_RcardList.Columns.Add("RCard", typeof(string));
            this.m_RcardList.Columns.Add("MOCode", typeof(string));
            this.m_RcardList.Columns.Add("ItemCode", typeof(string));
            this.m_RcardList.Columns.Add("ItemDescription", typeof(string));
            this.m_RcardList.Columns.Add("DownCode", typeof(string));
            this.m_RcardList.AcceptChanges();

            this.ultraGridEventList.DataSource = this.m_EventList;
            this.ultraGridRCardList.DataSource = this.m_RcardList;
        }

        private void FUp_Load(object sender, EventArgs e)
        {
            this.radioButtonEvent.Checked = true;
            this.radioButtonEvent_CheckedChanged(this.radioButtonEvent, new EventArgs());

            this.InitialUltraGridDataSource();

            this.ucLabelEditEventQuery.TextFocus(false, true);

            //this.InitPageLanguage();
            //this.InitGridLanguage(this.ultraGridEventList);
            //this.InitGridLanguage(this.ultraGridRCardList);
        }

        private void radioButtonRCard_CheckedChanged(object sender, EventArgs e)
        {
            this.radioButtonEvent.CheckedChanged -= new EventHandler(radioButtonEvent_CheckedChanged);
            this.radioButtonEvent.Checked = false;
            this.ucLabelEditEventQuery.Value = "";
            this.ucLabelEditEventQuery.Enabled = false;
            this.InitialEventList();
            this.InitialRCardList();
            this.ultraGridEventList.Enabled = false;

            this.ucLabelEditRcardQuery.Enabled = true;
            this.ucLabelEditRcardQuery.TextFocus(true, true);

            this.radioButtonEvent.CheckedChanged += new EventHandler(radioButtonEvent_CheckedChanged);
        }

        private void InitialEventList()
        {
            if (this.m_EventList == null)
            {
                return;
            }
            this.m_EventList.Rows.Clear();
            this.m_EventList.AcceptChanges();
        }

        private void InitialRCardList()
        {
            if (this.m_RcardList == null)
            {
                return;
            }
            this.m_RcardList.Rows.Clear();
            this.m_RcardList.AcceptChanges();

            this.ucLabelEditSelected.Value = "";
            this.ucLabelEditTotal.Value = "";
            this.checkBoxSelectAll.Checked = false;
        }

        private void radioButtonEvent_CheckedChanged(object sender, EventArgs e)
        {
            this.radioButtonRCard.CheckedChanged -= new EventHandler(radioButtonRCard_CheckedChanged);

            this.radioButtonRCard.Checked = false;
            this.ucLabelEditRcardQuery.Value = "";
            this.ucLabelEditRcardQuery.Enabled = false;
            this.InitialRCardList();

            this.ucLabelEditEventQuery.Enabled = true;
            this.ultraGridEventList.Enabled = true;
            this.ucLabelEditEventQuery.TextFocus(true, true);

            this.radioButtonRCard.CheckedChanged += new EventHandler(radioButtonRCard_CheckedChanged);
        }

        private void ultraGridEventList_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "EventCode";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["EventCode"].Header.Caption = "事件号";
            e.Layout.Bands[0].Columns["DownQty"].Header.Caption = "下地数量";
            e.Layout.Bands[0].Columns["DownReason"].Header.Caption = "下地原因";
            e.Layout.Bands[0].Columns["EventCode"].Width = 250;
            e.Layout.Bands[0].Columns["DownQty"].Width = 100;
            e.Layout.Bands[0].Columns["DownReason"].Width = 300;

            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["EventCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["DownQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["DownReason"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["DownQty"].CellAppearance.TextHAlign = HAlign.Right;
        }

        private void ultraGridEventList_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (e.Type != typeof(UltraGridRow))
            {
                return;
            }

            string downCode = this.ultraGridEventList.ActiveRow.Cells["EventCode"].Value.ToString();
            this.m_DownCodeSelected = downCode;

            DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
            object[] rcardList = dcf.GetDownRCardListByDownCode(downCode);
            this.BindRCardList(rcardList);
            this.ucLabelEditRCardEdit.TextFocus(true, true);
        }

        private void ultraGridRCardList_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "RCard";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["RCard"].Header.Caption = "序列号";
            e.Layout.Bands[0].Columns["MOCode"].Header.Caption = "工单";
            e.Layout.Bands[0].Columns["ItemCode"].Header.Caption = "料号";
            e.Layout.Bands[0].Columns["ItemDescription"].Header.Caption = "产品描述";

            e.Layout.Bands[0].Columns["Checked"].Width = 40;
            e.Layout.Bands[0].Columns["RCard"].Width = 150;
            e.Layout.Bands[0].Columns["MOCode"].Width = 150;
            e.Layout.Bands[0].Columns["ItemCode"].Width = 150;
            e.Layout.Bands[0].Columns["ItemDescription"].Width = 200;

            e.Layout.Bands[0].Columns["DownCode"].Hidden = true;

            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["RCard"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MOCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["ItemCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["ItemDescription"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
        }

        private void ultraGridRCardList_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            this.ultraGridRCardList.UpdateData();

            DataView dv = this.m_RcardList.Copy().DefaultView;
            dv.RowFilter = "Checked = 'true'";
            this.ucLabelEditSelected.Value = dv.Count.ToString();
        }

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            if (this.ucLabelEditTotal.Value.Trim() == string.Empty || this.ucLabelEditTotal.Value.Trim() == "0")
            {
                return;
            }
            if (this.ucLabelEditSelected.Value.Trim() == string.Empty || this.ucLabelEditSelected.Value.Trim() == "0")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$Error_NoDataSelected"));
                this.ucLabelEditRCardEdit.TextFocus(false, true);
                return;
            }
            if (this.ucLabelEditUpReason.Value.Trim().Length == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$Error_PleaseInputUPReason"));
                this.ucLabelEditUpReason.TextFocus(false, true);
                return;
            }

            frmDialog dialog = new frmDialog();
            dialog.Owner = this;
            dialog.Text = this.Text;
            dialog.DialogMessage = UserControl.MutiLanguages.ParserMessage("$Are_You_Sure_To_Do_UP");

            if (DialogResult.OK != dialog.ShowDialog())
            {
                return;
            }

            Messages msg = new Messages();
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            DataProvider.BeginTransaction();
            try
            {
                string rcard = "";
                string downCode = "";
                Down down;

                DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
                DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                for (int i = 0; i < this.ultraGridRCardList.Rows.Count; i++)
                {
                    if (string.Compare(this.ultraGridRCardList.Rows[i].Cells["Checked"].Value.ToString(), "true", true) == 0)
                    {
                        rcard = this.ultraGridRCardList.Rows[i].Cells["RCard"].Value.ToString();
                        downCode = this.ultraGridRCardList.Rows[i].Cells["DownCode"].Value.ToString();

                        down = dcf.GetDown(downCode, rcard) as Down;
                        if (down == null)
                        {
                            throw new Exception("$Error_DownNotExist $EventCode=" + downCode + " $CS_Param_ID=" + rcard);
                        }

                        if (down.DownStatus == DownStatus.DownStatus_Up)
                        {
                            throw new Exception("$Error_AlreadyUp $EventCode=" + downCode + " $CS_Param_ID=" + rcard);
                        }

                        down.DownStatus = DownStatus.DownStatus_Up;
                        down.UPReason = FormatHelper.CleanString(this.ucLabelEditUpReason.Value.Trim(), 100);
                        down.UPDATE_ = currentDateTime.DBDate;
                        down.UPTIME = currentDateTime.DBTime;
                        down.UPBY = ApplicationService.Current().UserCode;
                        down.MaintainDate = currentDateTime.DBDate;
                        down.MaintainTime = currentDateTime.DBTime;
                        down.MaintainUser = ApplicationService.Current().UserCode;

                        dcf.UpdateDown(down);
                    }
                }
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_UPSuccess"));
                DataProvider.CommitTransaction();

                this.ReloadData();

                this.checkBoxSelectAll.Checked = false;
                this.ucLabelEditUpReason.Value = "";

                if (this.ultraGridRCardList.Rows.Count > 0)
                {
                    this.ucLabelEditRCardEdit.TextFocus(true, true);
                }
            }
            catch (Exception ex)
            {
                DataProvider.RollbackTransaction();
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            }
        }

        private void ReloadData()
        {
            this.InitialEventList();
            this.InitialRCardList();

            if (this.radioButtonEvent.Checked)
            {
                this.QueryByEvent();

                this.SelectRowByDownCode(this.m_DownCodeSelected);
            }

            if (this.radioButtonRCard.Checked)
            {
                this.QueryByRCard();
            }
        }

        private void SelectRowByDownCode(string downCode)
        {
            if (this.ultraGridEventList.Rows.Count == 0)
            {
                return;
            }
            bool selected = false;
            for (int i = 0; i < this.ultraGridEventList.Rows.Count; i++)
            {
                if (string.Compare(this.ultraGridEventList.Rows[i].Cells["EventCode"].Value.ToString(), downCode, true) == 0)
                {
                    this.ultraGridEventList.Rows[i].Activate();
                    this.ultraGridEventList.Rows[i].Selected = true;

                    selected = true;
                    break;
                }
            }

            if (!selected)
            {
                this.ultraGridEventList.Rows[0].Activate();
                this.ultraGridEventList.Rows[0].Selected = true;
            }
        }

        private void ucButtonClear_Click(object sender, EventArgs e)
        {
            this.InitialRCardList();
            this.InitialEventList();

            this.ucLabelEditUpReason.Value = "";
            this.ucLabelEditRCardEdit.Value = "";

            this.radioButtonEvent.Checked = true;
            this.radioButtonEvent_CheckedChanged(this.radioButtonEvent, new EventArgs());

            this.ucLabelEditEventQuery.TextFocus(false, true);
        }

        private void checkBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            string checkStatus = "";
            if (this.checkBoxSelectAll.Checked)
            {
                checkStatus = "true";
            }
            else
            {
                checkStatus = "false";
            }

            for (int i = 0; i < this.ultraGridRCardList.Rows.Count; i++)
            {
                this.ultraGridRCardList.Rows[i].Cells["Checked"].Value = checkStatus;
            }
            this.ultraGridRCardList.UpdateData();

            DataView dv = this.m_RcardList.Copy().DefaultView;
            dv.RowFilter = "Checked = 'true'";
            this.ucLabelEditSelected.Value = dv.Count.ToString();
        }

        private void ucLabelEditRcardQuery_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.QueryByRCard();
            }
        }

        private void QueryByRCard()
        {
            string rcard = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelEditRcardQuery.Value));
            if (string.IsNullOrEmpty(rcard))
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"));
                this.ucLabelEditRcardQuery.TextFocus(false, true);
                return;
            }

            DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
            //获取当前序列号产品对应的最原始的序列号
            string sourceRCard = dcf.GetSourceCard(rcard.Trim().ToUpper(), string.Empty);

            object[] rcardList = dcf.GetDownRCardListByCode(sourceRCard);
            if (rcardList == null || rcardList.Length == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_RCardErrorOrNotInDown"));
                this.ucLabelEditRcardQuery.TextFocus(false, true);
                return;
            }
            else
            {
                this.BindRCardList(rcardList);
                this.ucLabelEditRCardEdit.TextFocus(true, true);
            }
        }

        private void ucLabelEditEventQuery_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.InitialEventList();
                this.InitialRCardList();

                this.QueryByEvent();

                if (this.ultraGridEventList.Rows.Count != 0)
                {
                    this.ultraGridEventList.Rows[0].Activate();
                    this.ultraGridEventList.Rows[0].Selected = true;
                }
            }
        }

        private void QueryByEvent()
        {
            string eventCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelEditEventQuery.Value));
            DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);

            object[] eventList = dcf.GetDownEventList(eventCode);
            if (eventList == null || eventList.Length == 0)
            {
                this.ucLabelEditEventQuery.TextFocus(false, true);
                return;
            }

            DataRow rowNew;
            this.m_EventList.Rows.Clear();
            foreach (DownWithRCardInfo down in eventList)
            {
                if (down.DownQuantity > 0)
                {
                    rowNew = this.m_EventList.NewRow();
                    rowNew["EventCode"] = down.DownCode;
                    rowNew["DownQty"] = down.DownQuantity;
                    rowNew["DownReason"] = down.DownReason;

                    this.m_EventList.Rows.Add(rowNew);
                }
            }
            this.m_EventList.AcceptChanges();
        }

        private void BindRCardList(object[] rcardList)
        {
            DataRow rowRcard;
            this.m_RcardList.Rows.Clear();

            if (rcardList == null || rcardList.Length == 0)
            {
                return;
            }

            foreach (DownWithRCardInfo rcard in rcardList)
            {
                rowRcard = this.m_RcardList.NewRow();
                rowRcard["Checked"] = "false";
                rowRcard["RCard"] = rcard.RCard;
                rowRcard["MOCode"] = rcard.MOCode;
                rowRcard["ItemCode"] = rcard.ItemCode;
                rowRcard["ItemDescription"] = rcard.ItemDescription;
                rowRcard["DownCode"] = rcard.DownCode;

                this.m_RcardList.Rows.Add(rowRcard);
            }
            this.m_RcardList.AcceptChanges();
            this.ucLabelEditSelected.Value = "0";
            this.ucLabelEditTotal.Value = this.m_RcardList.Rows.Count.ToString();
        }

        private void ucLabelEditRCardEdit_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string rcard = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelEditRCardEdit.Value));
                DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
                //获取当前序列号产品对应的最原始的序列号
                string sourceRCard = dcf.GetSourceCard(rcard.Trim().ToUpper(), string.Empty);
                if (string.IsNullOrEmpty(rcard))
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"));
                    this.ucLabelEditRCardEdit.TextFocus(false, true);
                    return;
                }

                bool selected = this.SelectRowByRCard(sourceRCard);

                if (!selected)
                {
                    object rcardObject = (new DataCollectFacade(this.DataProvider)).GetRCardByCartonCode(sourceRCard);
                    if (rcardObject == null)
                    {
                        selected = false;
                    }
                    else
                    {
                        rcard = (rcardObject as SimulationReport).RunningCard;
                        selected = this.SelectRowByRCard(sourceRCard);
                    }
                }

                if (!selected)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_RCardErrorOrNotInDown"));
                }

                this.ucLabelEditRCardEdit.TextFocus(false, true);
            }
        }

        private bool SelectRowByRCard(string rcard)
        {
            bool selected = false;
            for (int i = 0; i < this.ultraGridRCardList.Rows.Count; i++)
            {
                if (string.Compare(this.ultraGridRCardList.Rows[i].Cells["RCard"].Value.ToString(), rcard, true) == 0)
                {
                    this.ultraGridRCardList.Rows[i].Cells["Checked"].Value = "true";
                    this.ultraGridRCardList.UpdateData();
                    this.ultraGridRCardList_CellChange(this.ultraGridRCardList, new CellEventArgs(this.ultraGridRCardList.Rows[i].Cells["Checked"]));

                    selected = true;
                    break;
                }
            }

            return selected;
        }
    }
}