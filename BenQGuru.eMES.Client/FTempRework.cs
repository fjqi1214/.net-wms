using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Rework;
using BenQGuru.eMES.Domain.Rework;
using Infragistics.Win.UltraWinGrid;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.DataCollect;
using Infragistics.Win;
using UserControl;

namespace BenQGuru.eMES.Client
{
    public partial class FTempRework : Form
    {
        DataTable dtReworkLot = new DataTable();
        DataTable dtReworkRcard = new DataTable();
        string clickLotNo = string.Empty;

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FTempRework()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
            this.ultraGridLotList.UpdateMode = UpdateMode.OnCellChange;
            this.ultraGridLotList.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            this.ultraGridLotList.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridLotList.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridLotList.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridLotList.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridLotList.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridLotList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridLotList.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridLotList.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridLotList.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");

            this.ultraGridReworkRcard.UpdateMode = UpdateMode.OnCellChange;
            this.ultraGridReworkRcard.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            this.ultraGridReworkRcard.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridReworkRcard.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridReworkRcard.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridReworkRcard.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridReworkRcard.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridReworkRcard.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridReworkRcard.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridReworkRcard.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridReworkRcard.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void ultraGridLotList_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["LotNo"].Header.Caption = "批号";
            if (this.rdoNotConfirmed.Checked)
            {
                e.Layout.Bands[0].Columns["ConfirmedOrNot"].Header.Caption = "待确认/总数";
            }
            else
            {
                e.Layout.Bands[0].Columns["ConfirmedOrNot"].Header.Caption = "已确认/总数";
            }

            e.Layout.Bands[0].Columns["ItemCode"].Header.Caption = "产品";
            e.Layout.Bands[0].Columns["MaterialDescription"].Header.Caption = "产品描述";
            e.Layout.Bands[0].Columns["MaintainUser"].Header.Caption = "用户";
            e.Layout.Bands[0].Columns["MaintainDate"].Header.Caption = "日期";
            e.Layout.Bands[0].Columns["MaintainTime"].Header.Caption = "时间";

            e.Layout.Bands[0].Columns["LotNo"].Width = 200;
            e.Layout.Bands[0].Columns["ConfirmedOrNot"].Width = 100;
            e.Layout.Bands[0].Columns["ItemCode"].Width = 100;
            e.Layout.Bands[0].Columns["MaterialDescription"].Width = 200;
            e.Layout.Bands[0].Columns["MaintainUser"].Width = 60;
            e.Layout.Bands[0].Columns["MaintainDate"].Width = 60;
            e.Layout.Bands[0].Columns["MaintainTime"].Width = 60;


            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["LotNo"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["ConfirmedOrNot"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["ItemCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MaterialDescription"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MaintainUser"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MaintainDate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MaintainTime"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["LotNo"].SortIndicator = SortIndicator.Ascending;
        }

        private void InitializeUltraGrid()
        {
            dtReworkLot.Columns.Clear();
            dtReworkRcard.Columns.Clear();

            dtReworkLot.Columns.Add("LotNo", typeof(string));
            dtReworkLot.Columns.Add("ConfirmedOrNot", typeof(string));
            dtReworkLot.Columns.Add("ItemCode", typeof(string));
            dtReworkLot.Columns.Add("MaterialDescription", typeof(string));
            dtReworkLot.Columns.Add("MaintainUser", typeof(string));
            dtReworkLot.Columns.Add("MaintainDate", typeof(int));
            dtReworkLot.Columns.Add("MaintainTime", typeof(int));

            dtReworkRcard.Columns.Add("PalletCode", typeof(string));
            dtReworkRcard.Columns.Add("Rcard", typeof(string));
            dtReworkRcard.Columns.Add("Status", typeof(string));

            dtReworkLot.AcceptChanges();
            dtReworkRcard.AcceptChanges();

            this.ultraGridLotList.DataSource = dtReworkLot;
            this.ultraGridReworkRcard.DataSource = dtReworkRcard;

        }

        private void FTempRework_Load(object sender, EventArgs e)
        {
            rdoNotConfirmed.Checked = true;

            InitializeUltraGrid();

            this.InitUIControl();

            this.InitReworkLotNo();

            this.SelectDefaultRow();
        }

        private void SelectDefaultRow()
        {
            if (this.ultraGridLotList.Rows.Count > 0)
            {
                this.ultraGridLotList.Rows[0].Activate();
                this.ultraGridLotList.Rows[0].Selected = true;
                this.ultraGridLotList_AfterSelectChange(this.ultraGridLotList, new AfterSelectChangeEventArgs(typeof(Infragistics.Win.UltraWinGrid.UltraGridCell)));
            }
        }

        private void InitUIControl()
        {
            this.dtReworkLot.Rows.Clear();
            this.dtReworkRcard.Rows.Clear();

            this.clickLotNo = "";
            this.txtInput.Value = "";

            this.txtNotSelectnNumber.Value = "0";
            this.txtTotalNumber.Value = "0";

            rdoBoxNo.Checked = true;

            this.dtReworkLot.AcceptChanges();
            this.dtReworkRcard.AcceptChanges();
        }

        private void InitReworkLotNo()
        {
            string status = this.rdoNotConfirmed.Checked ? ReworkFacade.ReworkLot_Status_NEW : ReworkFacade.ReworkLot_Status_DEAL;

            ReworkFacade reworkFacade = new ReworkFacade(this.DataProvider);
            object[] tempReworkLotNo = reworkFacade.GetTempReworkLotNo(status);

            if (tempReworkLotNo == null)
            {
                return;
            }

            foreach (TempRework tempRework in tempReworkLotNo)
            {
                if (status == ReworkFacade.ReworkLot_Status_DEAL)
                {
                    dtReworkLot.Rows.Add(new object[] { tempRework.LotNO, tempRework.TotalCount.ToString() + "/" + tempRework.TotalCount.ToString(), 
                        tempRework.ItemCode, tempRework.MaterialDescription, 
                        tempRework.MaintainUser, tempRework.MaintainDate, tempRework.MaintainTime });
                }
                else
                {
                    dtReworkLot.Rows.Add(new object[] { tempRework.LotNO, tempRework.UnConfirmedCount.ToString() + "/" + tempRework.TotalCount.ToString(), 
                        tempRework.ItemCode, tempRework.MaterialDescription, 
                        tempRework.MaintainUser, tempRework.MaintainDate, tempRework.MaintainTime });
                }
            }

            this.dtReworkLot.AcceptChanges();
        }

        private void InitReworkRcard()
        {
            dtReworkRcard.Rows.Clear();
            this.txtTotalNumber.Value = "0";
            this.txtNotSelectnNumber.Value = "0";
            this.txtInput.Value = "";

            int totalCount = 0;
            int unconfirmCount = 0;
            string status = "";

            ReworkFacade reworkFacade = new ReworkFacade(this.DataProvider);
            object[] tempReworkRcard = reworkFacade.QueryReworkRcard(clickLotNo);
            if (tempReworkRcard != null && tempReworkRcard.Length > 0)
            {
                foreach (ReworkRcard reworkRcard in tempReworkRcard)
                {
                    if (string.Compare(reworkRcard.Status, ReworkFacade.ReworkLot_Status_NEW, true) == 0)
                    {
                        unconfirmCount++;
                        status = "待确认";
                    }
                    else
                    {
                        status = "已确认";
                    }
                    dtReworkRcard.Rows.Add(new object[] { reworkRcard.PalletCode, reworkRcard.RCard, status });
                    totalCount++;                    
                }
            }

            this.dtReworkRcard.AcceptChanges();

            this.txtNotSelectnNumber.Value = unconfirmCount.ToString();
            this.txtTotalNumber.Value = totalCount.ToString();
        }

        private void ultraGridReworkRcard_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["PalletCode"].Header.Caption = "栈板";
            e.Layout.Bands[0].Columns["Rcard"].Header.Caption = "产品序列号";
            e.Layout.Bands[0].Columns["Status"].Header.Caption = "状态";

            e.Layout.Bands[0].Columns["PalletCode"].Width = 200;
            e.Layout.Bands[0].Columns["Rcard"].Width = 200;
            e.Layout.Bands[0].Columns["Status"].Width = 100;

            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["PalletCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["Rcard"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["Status"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["Rcard"].SortIndicator = SortIndicator.Ascending;
        }

        private void ultraGridLotList_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (e.Type != typeof(Infragistics.Win.UltraWinGrid.UltraGridCell))
            {
                return;
            }

            UltraGridRow selectedRow = this.ultraGridLotList.ActiveRow;
            this.clickLotNo = FormatHelper.PKCapitalFormat(selectedRow.Cells["LotNo"].Value.ToString());
            this.labelCurrentLot.Text = this.clickLotNo;

            InitReworkRcard();
        }

        private void txtInput_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.clickLotNo == string.Empty)
                {
                    return;
                }
                string inputValue = FormatHelper.CleanString(FormatHelper.PKCapitalFormat(this.txtInput.Value));

                if (inputValue == string.Empty)
                {
                    return;
                }

                string rcardList = "";

                if (rdoBoxNo.Checked)
                {
                    DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
                    object[] sims = dcf.GetSimulationFromCarton(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(inputValue)));
                    if (sims == null || sims.Length == 0)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CartonNotUsed"));
                        this.txtInput.TextFocus(false, true);
                        return;
                    }

                    foreach (Simulation sim in sims)
                    {
                        rcardList += sim.RunningCard + ",";
                    }
                    if (rcardList.Length > 0)
                    {
                        rcardList = rcardList.Substring(0, rcardList.Length - 1);
                    }
                }
                else
                {
                    //获取产品的最原始的序列号
                    DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                    string sourceRCard = dataCollectFacade.GetSourceCard(inputValue.Trim().ToUpper(), string.Empty);

                    rcardList = sourceRCard;
                }

                ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                ((SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                DataProvider.BeginTransaction();
                try
                {
                    ReworkFacade reworkFacade = new ReworkFacade(this.DataProvider);
                    ReworkRcard reworkRcard;
                    object obj;
                    for (int i = 0; i < rcardList.Split(',').Length; i++)
                    {
                        obj = reworkFacade.GetReworkRcard(clickLotNo, rcardList.Split(',')[i]);
                        if (obj == null)
                        {
                            // Not exist
                            throw new Exception("$CS_SN_NOT_EXIST_LOT");
                        }

                        reworkRcard = obj as ReworkRcard;

                        if (string.Compare(reworkRcard.Status, ReworkFacade.ReworkLot_Status_DEAL, true) == 0)
                        {
                            // Duplicate
                            throw new Exception("$CS_RcardHasBeenConfirmed");
                        }

                        reworkRcard.Status = ReworkFacade.ReworkLot_Status_DEAL;
                        reworkFacade.UpdateReworkRcard(reworkRcard);
                    }

                    bool needReloadLot = false;
                    int needConfirmCount = reworkFacade.QueryReworkRcardNotConfirmCount(clickLotNo);
                    if (needConfirmCount == 0)
                    {
                        object objReworkLotNo = reworkFacade.GetReworkLotNo(clickLotNo);
                        ReworkLotNo reworkLotNo = objReworkLotNo as ReworkLotNo;
                        reworkLotNo.Status = ReworkFacade.ReworkLot_Status_DEAL;
                        reworkFacade.UpdateReworkLotNo(reworkLotNo);

                        needReloadLot = true;
                    }

                    DataProvider.CommitTransaction();

                    if (needReloadLot)
                    {
                        this.InitUIControl();
                        InitReworkLotNo();

                        this.SelectDefaultRow();
                    }
                    else
                    {
                        this.InitReworkRcard();
                        // Update Qty
                        foreach (DataRow row in this.dtReworkLot.Rows)
                        {
                            if (string.Compare(row["LotNo"].ToString(), clickLotNo, true) == 0)
                            {
                                row["ConfirmedOrNot"] = needConfirmCount.ToString() + "/" + row["ConfirmedOrNot"].ToString().Split('/')[1];
                                break;
                            }
                        }
                        this.dtReworkLot.AcceptChanges();
                        this.txtInput.TextFocus(false, true);
                    }
                }
                catch (Exception ex)
                {
                    DataProvider.RollbackTransaction();
                    ApplicationRun.GetInfoForm().Add(ex.Message);
                    this.txtInput.TextFocus(false, true);
                }
                finally
                {
                    ((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
                }
            }
        }

        private void rdoNotConfirmed_Click(object sender, EventArgs e)
        {
            this.InitUIControl();

            this.ultraGridLotList.DisplayLayout.Bands[0].Columns["ConfirmedOrNot"].Header.Caption = "待确认/总数";
            this.InitReworkLotNo();

            this.SelectDefaultRow();

            this.txtInput.Enabled = true;
            this.rdoRcard.Enabled = true;
            this.rdoBoxNo.Enabled = true;
        }

        private void rdoConfirmed_Click(object sender, EventArgs e)
        {
            this.InitUIControl();

            this.ultraGridLotList.DisplayLayout.Bands[0].Columns["ConfirmedOrNot"].Header.Caption = "已确认/总数";
            this.InitReworkLotNo();

            this.SelectDefaultRow();

            this.txtInput.Enabled = false;
            this.rdoRcard.Enabled = false;
            this.rdoBoxNo.Enabled = false;
        }
    }
}