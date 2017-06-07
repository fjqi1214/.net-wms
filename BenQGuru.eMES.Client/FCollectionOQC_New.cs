using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

using UserControl;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Domain.TSModel;
using System.Collections;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.Client
{
    public partial class FCollectionOQC_New : BaseForm
    {
        private string m_LotNo = string.Empty;
        private DataSet m_CheckList = null;
        private DataTable m_CheckGroup = null;
        private DataTable m_CheckItem = null;

        private DataSet m_ErrorList = null;
        private DataTable m_ErrorGroup = null;
        private DataTable m_ErrorCode = null;

        private Domain.BaseSetting.Resource m_Resource;
        private decimal m_MaxCheckSequence = 1;
        private bool m_HasNG = false;
        private string _FunctionName = string.Empty;
        private string m_UnfrozenReason = string.Empty;

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FCollectionOQC_New()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
            this.ultraGridCheckList.UpdateMode = UpdateMode.OnCellChange;
            this.ultraGridCheckList.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            this.ultraGridCheckList.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridCheckList.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridCheckList.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridCheckList.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridCheckList.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridCheckList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridCheckList.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridCheckList.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridCheckList.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");

            this.ultraGridErrorList.UpdateMode = UpdateMode.OnCellChange;
            this.ultraGridErrorList.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            this.ultraGridErrorList.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridErrorList.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridErrorList.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridErrorList.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridErrorList.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridErrorList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridErrorList.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridErrorList.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridErrorList.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void FCollectionOQC_New_Load(object sender, EventArgs e)
        {
            this._FunctionName = this.Text;

            this.PopularValueList();
            this.InitializeCheckListGrid();
            this.InitializeErrorListGrid();
            //this.InitPageLanguage();
        }

        private void ucLabelEditRCard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.GetLotNo();
            }
        }

        private void ucLabelEditLotNo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                // Get CheckGroup Info
                Messages msg = this.LoadLotInfo();
                if (!msg.IsSuccess())
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditLotNo.Caption + ": " + this.ucLabelEditLotNo.Value, msg, true);
                    this.m_LotNo = string.Empty;
                    this.ucLabelEditItemCode.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLabelEditSizeAndCapacity.Value = "";
                    //added by alex 2010/11/10
                    this.ucLabelEditSampleSize.Value = "";
                    this.ucLabelEditSampleGoodSize.Value = "";
                    this.ucLabelEditSampleNgSize.Value = "";
                }
                else
                {
                    this.m_LotNo = this.ucLabelEditLotNo.Value.Trim().ToUpper();
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), false);
                    this.ucLabelEditInput.TextFocus(true, true);
                }
            }

        }

        private void timerLotSize_Tick(object sender, EventArgs e)
        {
            this.RefreshLotSize();
        }

        private void ucButtonGetLot_Click(object sender, EventArgs e)
        {
            this.GetLotNo();
        }

        private void ucButtonPass_Click(object sender, EventArgs e)
        {
            if (this.ValidInputForLotPass())
            {
                // Check LotNo
                string oqcLotNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelEditLotNo.Value));
                if (oqcLotNo.Length == 0)
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditLotNo.Caption + ": " + this.ucLabelEditLotNo.Value, new UserControl.Message(MessageType.Error, "$CS_FQCLOT_NOT_NULL"), true);
                    ucLabelEditLotNo.TextFocus(false, true);
                    return;
                }

                Messages msg = new Messages();

                OQCFacade oqcFacade = new OQCFacade(DataProvider);
                //判断批是否存在
                object obj = oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
                if (obj == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));
                    ApplicationRun.GetInfoForm().Add(msg);
                    this.ucLabelEditLotNo.TextFocus(false, true);
                    return;
                }
                //判断该批是否已经结束
                if (((OQCLot)obj).LOTStatus == OQCLotStatus.OQCLotStatus_Pass)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_Already_Pass"));
                    ApplicationRun.GetInfoForm().Add(msg);
                    this.ucLabelEditLotNo.TextFocus(false, true);
                    return;
                }

                if (((OQCLot)obj).LOTStatus == OQCLotStatus.OQCLotStatus_Reject)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_Already_Reject"));
                    ApplicationRun.GetInfoForm().Add(msg);
                    this.ucLabelEditLotNo.TextFocus(false, true);
                    return;
                }

                string msgInfo = String.Empty;
                msgInfo = UserControl.MutiLanguages.ParserMessage("$CS_CONFIRM_LOT_PASS " + "$CS_CURRENT_LOT_SIZE :" + ((OQCLot)obj).LotSize);

                frmDialog dialog = new frmDialog();
                dialog.Text = this.Text;
                dialog.DialogMessage = msgInfo;

                if (DialogResult.OK != dialog.ShowDialog(this))
                {
                    ucLabelEditLotNo.TextFocus(false, true);
                    return;
                }

                // Check Frozen status
                bool isFrozen = false;
                OQCLot objFrozen = obj as OQCLot;

                if (objFrozen.FrozenStatus == FrozenStatus.STATUS_FRONZEN)
                {
                    isFrozen = true;
                    FFrozenReason frozenReason = new FFrozenReason();
                    string reason = this.ucLabelEditMemo.Value.Trim();
                    if (reason.Length == 0)
                    {
                        reason = "批判过";
                    }
                    frozenReason.Reason = reason;
                    frozenReason.Event += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(frozenReason_Event);
                    if (DialogResult.Cancel == frozenReason.ShowDialog(this))
                    {
                        ucLabelEditLotNo.TextFocus(false, true);
                        return;
                    }
                }


                //把lotcapacity更新成lotsise一样大小,防止此时有产品在产生工序进入批而产生批判定时整个批无法全部完工的并发问题 by hiro 
                Decimal reallyLotCapacity = ((OQCLot)obj).LotCapacity;
                this.DataProvider.BeginTransaction();
                try
                {
                    //lock该lot，防止同时对该lot操作产生死锁
                    oqcFacade.LockOQCLotByLotNO(oqcLotNo);
                    //end 

                    DBDateTime DBDateTimeNow = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    ((OQCLot)obj).LotCapacity = ((OQCLot)obj).LotSize;
                    ((OQCLot)obj).MaintainDate = DBDateTimeNow.DBDate;
                    ((OQCLot)obj).MaintainTime = DBDateTimeNow.DBTime;
                    oqcFacade.UpdateOQCLot((OQCLot)obj);

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    msg.Add(new UserControl.Message(ex));
                    ApplicationRun.GetInfoForm().Add(msg);
                }

                if (!msg.IsSuccess())
                {
                    return;
                }
                //end add

                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                DataProvider.BeginTransaction();

                try
                {
                    //lock该lot，防止同时对该lot操作产生死锁
                    oqcFacade.LockOQCLotByLotNO(oqcLotNo);
                    //end 

                    object[] objs = (new ActionOQCHelper(this.DataProvider)).QueryCardOfLot(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
                    if (objs == null)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditLotNo.Caption + ": " + this.ucLabelEditLotNo.Value, msg, true);
                        return;
                    }

                    ActionFactory actionFactory = new ActionFactory(this.DataProvider);
                    ActionOnLineHelper actinOnlineHelper = new ActionOnLineHelper(this.DataProvider);
                    IAction actionPass = actionFactory.CreateAction(ActionType.DataCollectAction_OQCPass);

                    // Added By Hi1/Venus.Feng on 20081111 for Hisense : 根据产生批的资源获取该资源对应的大线的最后一个工序的资源(取一个)
                    //string rightResource = (new BaseModelFacade(this.DataProvider)).GetRightResourceForOQCOperate(objFrozen.ResourceCode,
                    //            (objs[0] as Simulation).ItemCode, (objs[0] as Simulation).RouteCode);
                    //if (string.IsNullOrEmpty(rightResource))
                    //{
                    //    msg.Add(new UserControl.Message(MessageType.Error, "$Error_NoBigLineFQCResource"));
                    //    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditLotNo.Caption + ": " + this.ucLabelEditLotNo.Value, msg, true);
                    //    return;
                    //}
                    string rightResource = ApplicationService.Current().ResourceCode;
                    // End Added

                    OQCPASSEventArgs actionEventArgs = new OQCPASSEventArgs(ActionType.DataCollectAction_OQCPass, ((Domain.DataCollect.Simulation)objs[0]).RunningCard,
                        ApplicationService.Current().UserCode, rightResource, oqcLotNo, null);

                    actionEventArgs.Lot = obj;
                    actionEventArgs.CardOfLot = objs;
                    actionEventArgs.IsForcePass = false;

                    // UnFrozen
                    actionEventArgs.IsUnFrozen = isFrozen;
                    actionEventArgs.UnFrozenReason = this.m_UnfrozenReason;

                    msg.AddMessages(actionPass.Execute(actionEventArgs));
                    if (msg.IsSuccess())
                    {
                        msg.Add(new UserControl.Message(MessageType.Success, "$CS_OQCPASSSUCCESS"));
                    }
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    msg.Add(new UserControl.Message(ex));
                }
                finally
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditLotNo.Caption + ": " + this.ucLabelEditLotNo.Value, msg, true);
                    if (!msg.IsSuccess())
                    {
                        this.DataProvider.RollbackTransaction();
                        DBDateTime DBDateTimeNow = FormatHelper.GetNowDBDateTime(this.DataProvider);
                        //modify by alex.hu 修正当报错后，lot状态被改为pass
                        object objlot = oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
                        if (objlot != null)
                        {
                            ((OQCLot)objlot).LotCapacity = reallyLotCapacity;
                            ((OQCLot)objlot).MaintainDate = DBDateTimeNow.DBDate;
                            ((OQCLot)objlot).MaintainTime = DBDateTimeNow.DBTime;
                            oqcFacade.UpdateOQCLot((OQCLot)objlot);
                        }
                    }
                    else
                    {
                        this.DataProvider.CommitTransaction();
                    }
                    ucLabelEditLotNo.TextFocus(false, true);
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
                }
            }
        }

        void frozenReason_Event(object sender, ParentChildRelateEventArgs<string> e)
        {
            m_UnfrozenReason = e.CustomObject;
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

            for (int i = 0; i < this.ultraGridCheckList.Rows.Count; i++)
            {
                this.ultraGridCheckList.Rows[i].Cells["Checked"].Value = checkStatus;
                if (this.ultraGridCheckList.Rows[i].HasChild(false))
                {
                    for (int j = 0; j < this.ultraGridCheckList.Rows[i].ChildBands[0].Rows.Count; j++)
                    {
                        this.ultraGridCheckList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"].Value = checkStatus;
                    }
                }
            }
            this.ultraGridCheckList.UpdateData();
        }

        private void ucLabelEditErrorCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.ucLabelEditErrorCode.Value.Trim().Length == 0)
                {
                    this.ucLabelEditErrorCode.TextFocus(false, true);
                    return;
                }

                if (this.ultraGridErrorList.ActiveRow != null)
                {
                    this.ultraGridErrorList.ActiveRow.Selected = false;
                    this.ultraGridErrorList.ActiveRow = null;
                }
                bool hasChecked = false;
                for (int i = 0; i < this.ultraGridErrorList.Rows.Count; i++)
                {
                    if (this.ultraGridErrorList.Rows[i].HasChild(false))
                    {
                        for (int j = 0; j < this.ultraGridErrorList.Rows[i].ChildBands[0].Rows.Count; j++)
                        {
                            if (string.Compare(this.ultraGridErrorList.Rows[i].ChildBands[0].Rows[j].Cells["ErrorCodeCode"].Value.ToString(),
                                this.ucLabelEditErrorCode.Value.Trim(), true) == 0)
                            {
                                this.ultraGridErrorList.Rows[i].Expanded = true;
                                this.ultraGridErrorList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"].Value = "true";
                                this.ultraGridErrorList.Rows[i].ChildBands[0].Rows[j].Selected = true;
                                this.ultraGridErrorList.Rows[i].ChildBands[0].Rows[j].Activate();
                                hasChecked = true;
                                this.ultraGridErrorList_CellChange(this.ultraGridErrorList, new CellEventArgs(this.ultraGridErrorList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"]));
                            }
                        }
                    }
                }
                this.ultraGridErrorList.UpdateData();

                if (hasChecked == false)
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditErrorCode.Caption + ": " + this.ucLabelEditErrorCode.Value, new UserControl.Message(MessageType.Error, "$ErrorCode_Not_Exist"), false);
                }
                this.ucLabelEditErrorCode.TextFocus(false, true);
            }
        }

        private void ucLabelEditInput_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Messages msg = new Messages();
                msg = this.LoadLotInfo();

                if (!msg.IsSuccess())
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditLotNo.Caption + ": " + this.ucLabelEditLotNo.Value, msg, true);
                    this.m_LotNo = string.Empty;
                    this.ucLabelEditItemCode.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLabelEditSizeAndCapacity.Value = "";
                    this.ucLabelEditSampleSize.Value = "";
                    this.ucLabelEditSampleGoodSize.Value = "";
                    this.ucLabelEditSampleNgSize.Value = "";
                }
                else
                {
                    this.m_LotNo = this.ucLabelEditLotNo.Value.Trim().ToUpper();
                }


                #region UI  Input  Check
                // Check RCard
                string runningID = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelEditInput.Value));
                if (runningID.Length == 0)
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "产品序列号: " + this.ucLabelEditInput.Value, new UserControl.Message(MessageType.Error, "$CS_Please_Input_RunningCard"), true);
                    ucLabelEditInput.TextFocus(false, true);
                    return;
                }

                // Check LotNo
                string oqcLotNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelEditLotNo.Value));
                if (oqcLotNo.Length == 0)
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditLotNo.Caption + ": " + this.ucLabelEditLotNo.Value, new UserControl.Message(MessageType.Error, "$CS_FQCLOT_NOT_NULL"), true);
                    ucLabelEditInput.Value = "";
                    ucLabelEditLotNo.TextFocus(false, true);
                    return;
                }
                #endregion

                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
                ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                ((SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();

                try
                {
                    #region Lot And RCard Check
                    //判断批是否存在
                    object obj = oqcFacade.GetOQCLot(oqcLotNo, OQCFacade.Lot_Sequence_Default);
                    if (obj == null)
                    {
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditLotNo.Caption + ": " + this.ucLabelEditLotNo.Value, new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"), true);
                        ucLabelEditInput.Value = "";
                        ucLabelEditLotNo.TextFocus(false, true);
                        return;
                    }

                    //判断批是否已经维护抽样计划
                    if (((OQCLot)obj).LOTStatus == OQCLotStatus.OQCLotStatus_Initial)
                    {
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditLotNo.Caption + ": " + this.ucLabelEditLotNo.Value, new UserControl.Message(MessageType.Error, "$Error_OQCLotNO_Cannot_Initial"), true);
                        ucLabelEditInput.Value = "";
                        ucLabelEditLotNo.TextFocus(false, true);
                        return;
                    }

                    //判断该批是否已经结束
                    if ((((OQCLot)obj).LOTStatus == OQCLotStatus.OQCLotStatus_Pass) ||
                        (((OQCLot)obj).LOTStatus == OQCLotStatus.OQCLotStatus_Reject))
                    {
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditLotNo.Caption + ": " + this.ucLabelEditLotNo.Value, new UserControl.Message(MessageType.Error, "$Error_OQCLotNO_HasComplete"), true);
                        ucLabelEditInput.Value = "";
                        ucLabelEditLotNo.TextFocus(false, true);
                        return;
                    }

                    DataCollect.DataCollectFacade dcf = new BenQGuru.eMES.DataCollect.DataCollectFacade(DataProvider);
                    //根据产品的当前序列号获取原始序列号
                    string sourceRCard = dcf.GetSourceCard(runningID.Trim().ToUpper(), string.Empty);

                    //判断ID是否有生产信息
                    ActionOnLineHelper actinOnlineHelper = new ActionOnLineHelper(this.DataProvider);
                    ProductInfo productInfo = (ProductInfo)actinOnlineHelper.GetIDInfo(sourceRCard).GetData().Values[0];
                    if (productInfo.LastSimulation == null)
                    {
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "产品序列号: " + this.ucLabelEditInput.Value, new UserControl.Message(MessageType.Error, "$NoSimulationInfo $CS_Param_ID =" + runningID), true);
                        ucLabelEditInput.TextFocus(false, true);
                        return;
                    }

                    //判断ID是否在批中
                    obj = oqcFacade.GetOQCLot2Card(sourceRCard, productInfo.LastSimulation.MOCode, oqcLotNo, OQCFacade.Lot_Sequence_Default);
                    if (obj == null)
                    {
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "产品序列号: " + this.ucLabelEditInput.Value, new UserControl.Message(MessageType.Error, "$CS_SN_NOT_EXIST_LOT $CS_Param_ID =" + runningID), true);
                        ucLabelEditInput.TextFocus(false, true);
                        return;
                    }
                    #endregion

                    //获取检验类型和检验项
                    //modify 改用产品获取检验类型 by alex.hu 
                    //msg.AddMessages(this.LoadCheckList(oqcLotNo, ApplicationService.Current().ResourceCode));
                    msg.AddMessages(this.LoadCheckList(oqcLotNo,((OQCLot2Card)obj).ItemCode));
                    if (!msg.IsSuccess())
                    {
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "产品序列号: " + this.ucLabelEditInput.Value, msg, true);
                        ucLabelEditInput.TextFocus(false, true);
                        return;
                    }

                    //获取不良原因组和不良原因
                    msg.AddMessages(this.LoadErrorList(productInfo.LastSimulation.ItemCode));
                    if (!msg.IsSuccess())
                    {
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "产品序列号: " + this.ucLabelEditInput.Value, msg, true);
                        ucLabelEditInput.TextFocus(false, true);
                        return;
                    }

                    //获取最大的Check Sequence
                    m_MaxCheckSequence = oqcFacade.GetNextCheckSequence(sourceRCard);

                    //控制UI控件的状态
                    this.SetUIControlStatus(false);
                    this.ucLabelEditErrorCode.TextFocus(true, true);
                }
                catch (Exception ex)
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "产品序列号: " + this.ucLabelEditInput.Value, new UserControl.Message(ex), true);
                    ucLabelEditLotNo.TextFocus(false, true);
                }
                finally
                {
                    ((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
                }
            }
        }

        private void ucButtonConfirm_Click(object sender, EventArgs e)
        {
            if (this.ValidInput())
            {
                Messages msg = new Messages();

                string oqcLotNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelEditLotNo.Value));
                string runningCard = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelEditInput.Value));
                ActionOnLineHelper actionOnLineHelper = new ActionOnLineHelper(this.DataProvider);
                ActionFactory actionFactory = new ActionFactory(this.DataProvider);
                OQCFacade oqcFacade = new OQCFacade(DataProvider);

                DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
                string sourceRCard = dcf.GetSourceCard(runningCard, string.Empty);

                if (m_Resource == null)
                {
                    BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
                    m_Resource = (Domain.BaseSetting.Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);
                }

                #region Product Info
                // Get ProductInfo
                ProductInfo product = null;
                msg = actionOnLineHelper.GetIDInfo(sourceRCard);
                if (msg.IsSuccess())
                {
                    product = (ProductInfo)msg.GetData().Values[0];
                    if (product.LastSimulation == null)
                    {
                        msg.Add(new UserControl.Message(new Exception("$Error_LastSimulation_IsNull")));
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "产品序列号: " + this.ucLabelEditInput.Value, msg, true);
                        this.ucLabelEditInput.TextFocus(false, true);
                        return;
                    }
                }
                #endregion

                //获取选中的CheckGroup和CheckItem
                //这里获取CheckGroupList，其实后面不会进行Update动作的，因为在抽样计划那边已经保存过了，
                //这边没有信息需要更新的
                //获取只是为了沿用之前的函数，不再做更改，以免今后需要调整
                this.m_HasNG = false;
                object[] checkGroupList = this.GetSelectedCheckGroupList();
                object[] checkItemList = this.GetSelectedCheckItemList(product);

                if (checkItemList.Length == 0)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$Error_PleaseSelectCheckItem"));
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", msg, false);
                    //this.ucLabelEditInput.TextFocus(false, true);
                    this.ultraGridCheckList.Focus();
                    return;
                }

                //获取选中的ErrorGroup和ErrorCode
                object[] errorList = this.GetSelectedErrorCodeList();

                if (m_HasNG)
                {
                    if (errorList.Length == 0)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_Please_Select_ErrorCode"));
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", msg, false);
                        //this.ucLabelEditInput.TextFocus(false, true);
                        this.ultraGridErrorList.Focus();
                        return;
                    }
                }
                else
                {
                    if (errorList.Length > 0)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_NOT_EXIST_NG_CHECKLIST"));
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditLotNo.Caption + ": " + this.ucLabelEditLotNo.Value, msg, true);
                        //this.ucLabelEditInput.TextFocus(false, true);
                        this.ultraGridErrorList.Focus();
                        return;
                    }
                }

                ActionCheckStatus actionCheckStatus = new ActionCheckStatus();
                actionCheckStatus.ProductInfo = product;
                actionCheckStatus.ProductInfo.Resource = m_Resource;
                actionCheckStatus.ActionList = new ArrayList();

                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                DataProvider.BeginTransaction();
                try
                {
                    // NG
                    if (m_HasNG)
                    {
                        OQCLot obj =(OQCLot) oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(oqcLotNo.Trim().ToUpper()), OQCFacade.Lot_Sequence_Default);
                        if (obj == null)
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));
                            ApplicationRun.GetInfoForm().Add(msg);
                            this.ucLabelEditLotNo.TextFocus(false, true);
                            return;
                        }

                        ActionOnLineHelper actinOnlineHelper = new ActionOnLineHelper(this.DataProvider);

                        object[] objs = (new ActionOQCHelper(this.DataProvider)).QueryCardOfLot(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
                        if (objs == null)
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));
                            return;
                        }

                        // Added By Hi1/Venus.Feng on 20081111 for Hisense : 根据产生批的资源获取该资源对应的大线的最后一个工序的资源(取一个)
                        //string rightResource = (new BaseModelFacade(this.DataProvider)).GetRightResourceForOQCOperate(obj.ResourceCode,
                        //            (objs[0] as Simulation).ItemCode, (objs[0] as Simulation).RouteCode);
                        //if (string.IsNullOrEmpty(rightResource))
                        //{
                        //    msg.Add(new UserControl.Message(MessageType.Error, "$Error_NoBigLineFQCResource"));
                        //    return;
                        //}
                        string rightResource = ApplicationService.Current().ResourceCode;
                        // End Added

                        IAction actionOQCNG = actionFactory.CreateAction(ActionType.DataCollectAction_OQCNG);
                        OQCNGEventArgs oqcNGEventArgs = new OQCNGEventArgs(ActionType.DataCollectAction_OQCNG, sourceRCard,
                            ApplicationService.Current().UserCode, m_Resource.ResourceCode, oqcLotNo, checkItemList,
                            checkGroupList, errorList, product);

                        oqcNGEventArgs.IsDataLink = BenQGuru.eMES.Web.Helper.FormatHelper.FALSE_STRING;
                        oqcNGEventArgs.Memo = FormatHelper.CleanString(this.ucLabelEditMemo.Value.Trim(), 40);
                        oqcNGEventArgs.CheckSequence = this.m_MaxCheckSequence;
                        oqcNGEventArgs.rightResource = rightResource;

                        msg.AddMessages(((IActionWithStatus)actionOQCNG).Execute(oqcNGEventArgs, actionCheckStatus));
                    }
                    else // Good
                    {
                        IAction actionOQCGood = actionFactory.CreateAction(ActionType.DataCollectAction_OQCGood);
                        OQCGoodEventArgs oqcGoodEventArgs = new OQCGoodEventArgs(ActionType.DataCollectAction_OQCGood, sourceRCard,
                            ApplicationService.Current().UserCode, m_Resource.ResourceCode, oqcLotNo, checkItemList,
                            checkGroupList, product);

                        oqcGoodEventArgs.IsDataLink = BenQGuru.eMES.Web.Helper.FormatHelper.FALSE_STRING;
                        oqcGoodEventArgs.Memo = FormatHelper.CleanString(this.ucLabelEditMemo.Value.Trim(), 40);
                        oqcGoodEventArgs.CheckSequence = this.m_MaxCheckSequence;
                      

                        msg.AddMessages(((IActionWithStatus)actionOQCGood).Execute(oqcGoodEventArgs, actionCheckStatus));
                    }
                }
                catch (Exception ex)
                {
                    DataProvider.RollbackTransaction();
                    msg.Add(new UserControl.Message(ex));
                }
                finally
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditLotNo.Caption + ": " + this.ucLabelEditLotNo.Value, msg, true);
                    if (!msg.IsSuccess())
                    {
                        DataProvider.RollbackTransaction();
                    }
                    else
                    {
                        DataProvider.CommitTransaction();

                        //控制UI控件的状态
                        this.ClearCheckList();
                        this.ClearErrorList();
                        this.SetUIControlStatus(true);
                        this.ucLabelEditInput.TextFocus(true, true);
                        //added by alex 刷新样本数量
                        this.RefreshLotSize();
                    }
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
                }
            }
        }

        private void ucButtonRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshLotSize();
        }

        private void ucButtonCancel_Click(object sender, EventArgs e)
        {
            this.ClearCheckList();
            this.ClearErrorList();
            this.SetUIControlStatus(true);
            this.ucLabelEditInput.TextFocus(true, true);
        }

        private void ultraGridCheckList_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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
            e.Layout.Bands[0].ScrollTipField = "CheckGroupCode";
            e.Layout.Bands[1].ScrollTipField = "CheckItemCode";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["CheckGroupCode"].Header.Caption = "检验类型";
            e.Layout.Bands[0].Columns["CheckedCount"].Header.Caption = "已检验数";
            e.Layout.Bands[0].Columns["NeedCheckCount"].Header.Caption = "应检验数";
            e.Layout.Bands[0].Columns["Checked"].Width = 40;
            e.Layout.Bands[0].Columns["CheckGroupCode"].Width = 160;
            e.Layout.Bands[0].Columns["CheckedCount"].Width = 100;
            e.Layout.Bands[0].Columns["NeedCheckCount"].Width = 100;
            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["CheckGroupCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["CheckedCount"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["NeedCheckCount"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            // 允许筛选
            e.Layout.Bands[0].Columns["CheckGroupCode"].AllowRowFiltering = DefaultableBoolean.True;
            // 允许冻结，且Checked栏位始终处于冻结状态，不可更改
            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["CheckGroupCode"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["CheckGroupCode"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;

            e.Layout.Bands[0].Columns["CheckGroupCode"].SortIndicator = SortIndicator.Ascending;

            // CheckItem
            e.Layout.Bands[1].Columns["CheckGroupCode"].Hidden = true;
            e.Layout.Bands[1].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[1].Columns["CheckItemCode"].Header.Caption = "检验项目";
            e.Layout.Bands[1].Columns["Result"].Header.Caption = "不通过";
            e.Layout.Bands[1].Columns["Grade"].Header.Caption = "缺陷等级";
            e.Layout.Bands[1].Columns["Memo"].Header.Caption = "备注";
            e.Layout.Bands[1].Columns["Checked"].Width = 40;
            e.Layout.Bands[1].Columns["CheckItemCode"].Width = 160;
            e.Layout.Bands[1].Columns["Result"].Width = 60;
            e.Layout.Bands[1].Columns["Grade"].Width = 60;
            e.Layout.Bands[1].Columns["Memo"].Width = 150;

            e.Layout.Bands[1].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[1].Columns["CheckItemCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["Result"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[1].Columns["Grade"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
            e.Layout.Bands[1].Columns["Grade"].ValueList = this.ultraGridCheckList.DisplayLayout.ValueLists["Grades"];

            e.Layout.Bands[1].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[1].Columns["CheckItemCode"].Header.Fixed = true;
            e.Layout.Bands[1].Columns["CheckItemCode"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;

            e.Layout.Bands[1].Columns["CheckItemCode"].SortIndicator = SortIndicator.Ascending;
            //this.InitGridLanguage(ultraGridCheckList);
        }

        private void ultraGridCheckList_BeforeRowFilterDropDownPopulate(object sender, BeforeRowFilterDropDownPopulateEventArgs e)
        {
            // 去除默认筛选项、自定义筛选
            if (e.Column.Key == "CheckGroupCode" && e.Column.Band.Index == 0)
            {
                e.ValueList.ValueListItems.RemoveAt(1);
                e.ValueList.ValueListItems.RemoveAt(1);
                e.ValueList.ValueListItems.RemoveAt(1);
            }
        }

        private void ultraGridCheckList_CellChange(object sender, CellEventArgs e)
        {
            this.ultraGridCheckList.UpdateData();
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

            if (e.Cell.Column.Key == "Result")
            {
                if (Convert.ToBoolean(e.Cell.Value) == true)
                {
                    e.Cell.Row.Cells["Checked"].Value = "true";
                    e.Cell.Row.ParentRow.Cells["Checked"].Value = "true";
                }
            }
            this.ultraGridCheckList.UpdateData();
        }

        private void ultraGridErrorList_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "ErrorGroupCode";
            e.Layout.Bands[1].ScrollTipField = "ErrorCodeCode";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["ErrorGroupCode"].Header.Caption = "不良代码组";
            e.Layout.Bands[0].Columns["ErrorGroupDescription"].Header.Caption = "不良代码组描述";
            e.Layout.Bands[0].Columns["ErrorGroupCode"].Width = 100;
            e.Layout.Bands[0].Columns["ErrorGroupDescription"].Width = 200;

            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["ErrorGroupCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["ErrorGroupDescription"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["ErrorGroupCode"].SortIndicator = SortIndicator.Ascending;

            // ErrorCode
            e.Layout.Bands[1].Columns["ErrorGroupCode"].Hidden = true;
            e.Layout.Bands[1].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[1].Columns["ErrorCodeCode"].Header.Caption = "不良代码";
            e.Layout.Bands[1].Columns["ErrorCodeDescription"].Header.Caption = "不良代码描述";
            e.Layout.Bands[1].Columns["Checked"].Width = 40;
            e.Layout.Bands[1].Columns["ErrorCodeCode"].Width = 100;
            e.Layout.Bands[1].Columns["ErrorCodeDescription"].Width = 200;

            e.Layout.Bands[1].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[1].Columns["ErrorCodeCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["ErrorCodeDescription"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["ErrorCodeCode"].SortIndicator = SortIndicator.Ascending;
            //this.InitGridLanguage(ultraGridErrorList);
        }

        private void ultraGridErrorList_CellChange(object sender, CellEventArgs e)
        {
            this.ultraGridErrorList.UpdateData();
            if (e.Cell.Column.Key == "Checked")
            {
                string errorCodeSelected = e.Cell.Row.Cells["ErrorCodeCode"].Value.ToString();
                string errorCodeGroupSelected = e.Cell.Row.Cells["ErrorGroupCode"].Value.ToString();
                string selectedValue = errorCodeGroupSelected + ":" + errorCodeSelected;

                if (Convert.ToBoolean(e.Cell.Value) == true)
                {
                    // Add errorcode to listbox
                    if (!this.listBoxSelectedErrorList.Items.Contains(selectedValue))
                    {
                        this.listBoxSelectedErrorList.Items.Add(selectedValue);
                    }
                }
                else
                {
                    // Remove errorcode from listbox
                    if (this.listBoxSelectedErrorList.Items.Contains(selectedValue))
                    {
                        this.listBoxSelectedErrorList.Items.Remove(selectedValue);
                    }
                }

                this.listBoxSelectedErrorList.Refresh();
            }
        }

        private void GetLotNo()
        {
            DataCollect.DataCollectFacade dcf = new BenQGuru.eMES.DataCollect.DataCollectFacade(DataProvider);
            string rCard = this.ucLabelEditRCard.Value.Trim().ToUpper();
            string cartonCode = ucLabelEditCartonCode.Value.ToUpper().Trim();

            //根据产品的当前序列号获取原始序列号
            string sourceRCard = dcf.GetSourceCard(rCard.Trim().ToUpper(), string.Empty);

            if (rCard.Length > 0)
            {
                object objSimulation = dcf.GetSimulation(sourceRCard);
                if (objSimulation != null)
                {
                    Domain.DataCollect.Simulation simulation = objSimulation as Domain.DataCollect.Simulation;
                    if (String.IsNullOrEmpty(simulation.LOTNO.Trim()))
                    {
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, " ", new UserControl.Message(MessageType.Error, "$RCard_No_Lot"), true);
                        this.ucLabelEditLotNo.Value = "";
                        this.ucLabelEditItemCode.Value = "";
                        this.labelItemDescription.Text = "";
                        this.ucLabelEditSizeAndCapacity.Value = "";
                        this.ucLabelEditSampleSize.Value = "";
                        this.ucLabelEditSampleGoodSize.Value = "";
                        this.ucLabelEditSampleNgSize.Value = "";
                        this.ucLabelEditRCard.TextFocus(false, true);
                        return;
                    }
                    if (!CheckLotStatus(simulation.LOTNO.Trim()))
                    {
                        this.ucLabelEditLotNo.Value = "";
                        this.ucLabelEditItemCode.Value = "";
                        this.labelItemDescription.Text = "";
                        this.ucLabelEditSizeAndCapacity.Value = "";
                        this.ucLabelEditSampleSize.Value = "";
                        this.ucLabelEditSampleGoodSize.Value = "";
                        this.ucLabelEditSampleNgSize.Value = "";
                        this.ucLabelEditRCard.TextFocus(false, true);
                        return;
                    }

                    Messages msg = this.LoadLotInfo();
                    if (!msg.IsSuccess())
                    {
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditLotNo.Caption + ": " + this.ucLabelEditLotNo.Value, msg, true);
                        this.ucLabelEditLotNo.Value = "";
                        this.ucLabelEditItemCode.Value = "";
                        this.labelItemDescription.Text = "";
                        this.ucLabelEditSizeAndCapacity.Value = "";
                        this.ucLabelEditSampleSize.Value = "";
                        this.ucLabelEditSampleGoodSize.Value = "";
                        this.ucLabelEditSampleNgSize.Value = "";
                    }
                    else
                    {
                        this.m_LotNo = this.ucLabelEditLotNo.Value.Trim().ToUpper();
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), false);
                        this.ucLabelEditInput.TextFocus(true, true);
                    }
                }
                else
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "产品序列号: " + this.ucLabelEditInput.Value, new UserControl.Message(MessageType.Error, "$NoSimulationInfo"), true);
                    this.ucLabelEditLotNo.Value = "";
                    this.ucLabelEditItemCode.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLabelEditSizeAndCapacity.Value = "";
                    this.ucLabelEditSampleSize.Value = "";
                    this.ucLabelEditSampleGoodSize.Value = "";
                    this.ucLabelEditSampleNgSize.Value = "";
                    this.ucLabelEditRCard.TextFocus(false, true);
                }
            }
            //add by alex 2010.11.8
            else if (cartonCode != String.Empty)
            {
                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
                object obj = oqcFacade.GetLot2CartonByCartonNo(cartonCode);
                if (obj != null)
                {
                    Lot2Carton lot2Carton = obj as Lot2Carton;

                    string lotno = lot2Carton.OQCLot;
                    if (lotno == string.Empty)
                    {                 
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$CS_CARTON_NOT_EXIST_LOT"), false);
                        this.ucLabelEditLotNo.Value = "";
                        this.ucLabelEditItemCode.Value = "";
                        this.labelItemDescription.Text = "";
                        this.ucLabelEditSizeAndCapacity.Value = "";
                        this.ucLabelEditSampleSize.Value = "";
                        this.ucLabelEditSampleGoodSize.Value = "";
                        this.ucLabelEditSampleNgSize.Value = "";
                        this.ucLabelEditCartonCode.TextFocus(false, true);
                    }
                    if (!CheckLotStatus(lot2Carton.OQCLot.Trim()))
                    {
                        this.ucLabelEditLotNo.Value = "";
                        this.ucLabelEditItemCode.Value = "";
                        this.labelItemDescription.Text = "";
                        this.ucLabelEditSizeAndCapacity.Value = "";
                        this.ucLabelEditSampleSize.Value = "";
                        this.ucLabelEditSampleGoodSize.Value = "";
                        this.ucLabelEditSampleNgSize.Value = "";
                        this.ucLabelEditCartonCode.TextFocus(false, true);
                        return;
                    }
                    Messages msg = this.LoadLotInfo();
                    if (!msg.IsSuccess())
                    {
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditLotNo.Caption + ": " + this.ucLabelEditLotNo.Value, msg, true);
                        this.ucLabelEditLotNo.Value = "";
                        this.ucLabelEditItemCode.Value = "";
                        this.labelItemDescription.Text = "";
                        this.ucLabelEditSizeAndCapacity.Value = "";
                        this.ucLabelEditSampleSize.Value = "";
                        this.ucLabelEditSampleGoodSize.Value = "";
                        this.ucLabelEditSampleNgSize.Value = "";
                    }
                    else
                    {
                        this.m_LotNo = this.ucLabelEditLotNo.Value.Trim().ToUpper();
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), false);
                        this.ucLabelEditInput.TextFocus(true, true);
                    }
                }
                else
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$NoLol2CartonInfo"), false);
                    this.ucLabelEditLotNo.Value = "";
                    this.ucLabelEditItemCode.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLabelEditSizeAndCapacity.Value = "";
                    this.ucLabelEditSampleSize.Value = "";
                    this.ucLabelEditSampleGoodSize.Value = "";
                    this.ucLabelEditSampleNgSize.Value = "";
                    this.ucLabelEditCartonCode.TextFocus(false, true);
                }
            }
            else
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$CS_Please_Input_RunningCard"), false);
                this.ucLabelEditLotNo.Value = "";
                this.ucLabelEditItemCode.Value = "";
                this.labelItemDescription.Text = "";
                this.ucLabelEditSizeAndCapacity.Value = "";
                this.ucLabelEditSampleSize.Value = "";
                this.ucLabelEditSampleGoodSize.Value = "";
                this.ucLabelEditSampleNgSize.Value = "";
                this.ucLabelEditRCard.TextFocus(false, true);
            }
        }

        private bool CheckLotStatus(string lotno)
        {
            OQCFacade oqcFacade = new OQCFacade(DataProvider);
            OQCLot oqcLot = oqcFacade.GetOQCLot(lotno, OQCFacade.Lot_Sequence_Default) as OQCLot;
            //判断批状态，为以下4种状态的继续
            if (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Initial || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Examing ||
                 oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_NoExame || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_SendExame)
            {
                this.ucLabelEditLotNo.Value = lotno;

            }
            else if (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Reject || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Rejecting)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Lot_Has_Reject"));
                return false;
            }
            else if (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Pass || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Passing)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Lot_Has_Pass"));
                return false;
            }
            return true;
        }

        private Messages LoadLotInfo()
        {
            Messages msg = new Messages();

            string lotNo = this.ucLabelEditLotNo.Value.Trim().ToUpper();
            if (lotNo.Length > 0)
            {
                try
                {
                    OQCFacade oqcFacade = new OQCFacade(DataProvider);
                    object obj = oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(lotNo), OQCFacade.Lot_Sequence_Default);
                    if (obj == null)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));
                        this.ucLabelEditLotNo.TextFocus(false, true);
                        return msg;
                    }

                    object[] objs = oqcFacade.GetOQCLot2CardByLotNoAndSeq(FormatHelper.PKCapitalFormat(lotNo), OQCFacade.Lot_Sequence_Default);
                    if (objs == null || objs.Length == 0)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$Error_LotNoRCard"));
                        ucLabelEditLotNo.TextFocus(false, true);
                        return msg;
                    }

                    OQCLot lot = obj as OQCLot;
                    string itemCode = (objs[0] as OQCLot2Card).ItemCode;
                    ItemFacade itemFacade = new ItemFacade(this.DataProvider);

                    object item = itemFacade.GetItem(itemCode, lot.OrganizationID);
                    if (item == null)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$Error_ItemCode_NotExist $ItemCode=" + itemCode));
                        ucLabelEditLotNo.TextFocus(false, true);
                        return msg;
                    }

                    this.ucLabelEditItemCode.Value = (item as Item).ItemCode;
                    this.labelItemDescription.Text = (item as Item).ItemDescription;
                    this.ucLabelEditSizeAndCapacity.Value = lot.LotSize.ToString();// +"/" + lot.LotCapacity.ToString();
                    //added by alex 2010/11/10
                    int lot2CardCheckCount = oqcFacade.ExactQueryOQCLot2CardCheckCount(lotNo, OQCFacade.Lot_Sequence_Default);
                    int lot2CardCheckNgCount = oqcFacade.ExactQueryOQCLot2CardCheckNgCount(lotNo, OQCFacade.Lot_Sequence_Default);
                    this.ucLabelEditSampleSize.Value = lot2CardCheckCount.ToString();
                    this.ucLabelEditSampleNgSize.Value = lot2CardCheckNgCount.ToString();
                    this.ucLabelEditSampleGoodSize.Value = (lot2CardCheckCount - lot2CardCheckNgCount).ToString();
                }
                catch (Exception ex)
                {
                    msg.Add(new UserControl.Message(ex));
                    ucLabelEditLotNo.TextFocus(false, true);
                }
            }
            else
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_FQCLOT_NOT_NULL"));
                this.ucLabelEditLotNo.TextFocus(false, true);
            }
            return msg;
        }

        private void RefreshLotSize()
        {
            if (this.m_LotNo == string.Empty)
            {
                this.ucLabelEditSizeAndCapacity.Value = "";
                //added by alex 2010/11/10
                this.ucLabelEditSampleSize.Value = "";
                this.ucLabelEditSampleGoodSize.Value = "";
                this.ucLabelEditSampleNgSize.Value = "";
            }
            else
            {
                OQCFacade oqcFacade = new OQCFacade(DataProvider);
                object obj = oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(m_LotNo), OQCFacade.Lot_Sequence_Default);               
                if (obj == null)
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditLotNo.Caption + ": " + this.ucLabelEditLotNo.Value, new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"), true);
                }
                else
                {
                    OQCLot lot = obj as OQCLot;                  

                    this.ucLabelEditSizeAndCapacity.Value = lot.LotSize.ToString();// +"/" + lot.LotCapacity.ToString();
                    //added by alex 2010/11/10
                    int lot2CardCheckCount = oqcFacade.ExactQueryOQCLot2CardCheckCount(m_LotNo, OQCFacade.Lot_Sequence_Default);
                    int lot2CardCheckNgCount = oqcFacade.ExactQueryOQCLot2CardCheckNgCount(m_LotNo, OQCFacade.Lot_Sequence_Default);
                    this.ucLabelEditSampleSize.Value = lot2CardCheckCount.ToString();
                    this.ucLabelEditSampleNgSize.Value = lot2CardCheckNgCount.ToString();
                    this.ucLabelEditSampleGoodSize.Value = (lot2CardCheckCount - lot2CardCheckNgCount).ToString();
                }
            }
        }

        private void SetUIControlStatus(bool status)
        {
            this.ucLabelEditRCard.Enabled = status;
            this.ucLabelEditLotNo.Enabled = status;
            this.ucLabelEditInput.Enabled = status;
            this.ucButtonGetLot.Enabled = status;
            this.ucButtonPass.Enabled = status;
        }

        private void InitializeUI()
        {

        }

        private void InitializeCheckListGrid()
        {
            this.m_CheckList = new DataSet();
            this.m_CheckGroup = new DataTable("CheckGroup");
            this.m_CheckItem = new DataTable("CheckItem");

            this.m_CheckGroup.Columns.Add("Checked", typeof(string));
            this.m_CheckGroup.Columns.Add("CheckGroupCode", typeof(string));
            this.m_CheckGroup.Columns.Add("CheckedCount", typeof(decimal));
            this.m_CheckGroup.Columns.Add("NeedCheckCount", typeof(decimal));

            this.m_CheckItem.Columns.Add("Checked", typeof(string));
            this.m_CheckItem.Columns.Add("CheckItemCode", typeof(string));
            this.m_CheckItem.Columns.Add("Result", typeof(string));
            this.m_CheckItem.Columns.Add("Grade", typeof(string));
            this.m_CheckItem.Columns.Add("Memo", typeof(string)).MaxLength = 100;
            this.m_CheckItem.Columns.Add("CheckGroupCode", typeof(string));

            this.m_CheckList.Tables.Add(this.m_CheckGroup);
            this.m_CheckList.Tables.Add(this.m_CheckItem);

            this.m_CheckList.Relations.Add(new DataRelation("CheckGroupAndCheckItem",
                                                this.m_CheckList.Tables["CheckGroup"].Columns["CheckGroupCode"],
                                                this.m_CheckList.Tables["CheckItem"].Columns["CheckGroupCode"]));
            this.m_CheckList.AcceptChanges();
            this.ultraGridCheckList.DataSource = this.m_CheckList;
        }

        private void InitializeErrorListGrid()
        {
            this.m_ErrorList = new DataSet();
            this.m_ErrorGroup = new DataTable("ErrorGroup");
            this.m_ErrorCode = new DataTable("ErrorCode");

            this.m_ErrorGroup.Columns.Add("ErrorGroupCode", typeof(string));
            this.m_ErrorGroup.Columns.Add("ErrorGroupDescription", typeof(string));

            this.m_ErrorCode.Columns.Add("Checked", typeof(string));
            this.m_ErrorCode.Columns.Add("ErrorGroupCode", typeof(string));
            this.m_ErrorCode.Columns.Add("ErrorCodeCode", typeof(string));
            this.m_ErrorCode.Columns.Add("ErrorCodeDescription", typeof(string));

            this.m_ErrorList.Tables.Add(this.m_ErrorGroup);
            this.m_ErrorList.Tables.Add(this.m_ErrorCode);

            this.m_ErrorList.Relations.Add(new DataRelation("ErrorGroupAndErrorCode",
                                                this.m_ErrorList.Tables["ErrorGroup"].Columns["ErrorGroupCode"],
                                                this.m_ErrorList.Tables["ErrorCode"].Columns["ErrorGroupCode"]));
            this.m_ErrorList.AcceptChanges();
            this.ultraGridErrorList.DataSource = this.m_ErrorList;
        }

        private void PopularValueList()
        {
            if (this.ultraGridCheckList.DisplayLayout.ValueLists.Contains("Grades"))
            {
                return;
            }

            ValueList vl = this.ultraGridCheckList.DisplayLayout.ValueLists.Add("Grades");
            vl.ValueListItems.Add("", "");
            vl.ValueListItems.Add(OQCFacade.OQC_ZGrade, "Z");
            vl.ValueListItems.Add(OQCFacade.OQC_AGrade, "A");
            vl.ValueListItems.Add(OQCFacade.OQC_BGrade, "B");
            vl.ValueListItems.Add(OQCFacade.OQC_CGrade, "C");
        }

        private Messages LoadCheckList(string lotNo, string resourceCode)
        {
            Messages msg = new Messages();
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            try
            {
                this.ClearCheckList();

                object[] checkGroups = oqcFacade.GetCheckGroupList4OQCCollect(lotNo, resourceCode);
                if (checkGroups != null)
                {
                    string checkGroupList = "";
                    DataRow rowGroup;
                    foreach (OQCLot2CheckGroup lot2CheckGroup in checkGroups)
                    {
                        checkGroupList += lot2CheckGroup.CheckGroup + ",";

                        rowGroup = this.m_CheckList.Tables["CheckGroup"].NewRow();
                        rowGroup["Checked"] = "false";
                        rowGroup["CheckGroupCode"] = lot2CheckGroup.CheckGroup;
                        rowGroup["CheckedCount"] = lot2CheckGroup.CheckedCount;
                        rowGroup["NeedCheckCount"] = lot2CheckGroup.NeedCheckCount;
                        this.m_CheckList.Tables["CheckGroup"].Rows.Add(rowGroup);
                    }
                    if (checkGroupList.Length > 0)
                    {
                        checkGroupList = checkGroupList.Substring(0, checkGroupList.Length - 1);
                        checkGroupList = checkGroupList.Replace(",", "','");
                    }

                    if (checkGroupList.Length > 0)
                    {
                        object[] checkItems = oqcFacade.GetOQCCheckListByCheckGroup(checkGroupList);
                        if (checkItems != null)
                        {
                            DataRow rowItem;
                            foreach (OQCCheckListQuery checkList in checkItems)
                            {
                                rowItem = this.m_CheckList.Tables["CheckItem"].NewRow();
                                rowItem["Checked"] = "false";
                                rowItem["CheckGroupCode"] = checkList.CheckGroupCode;
                                rowItem["CheckItemCode"] = checkList.CheckItemCode;
                                rowItem["Result"] = "false";
                                rowItem["Grade"] = "";
                                rowItem["Memo"] = "";
                                this.m_CheckList.Tables["CheckItem"].Rows.Add(rowItem);
                            }
                        }
                    }
                    this.m_CheckList.Tables["CheckGroup"].AcceptChanges();
                    this.m_CheckList.Tables["CheckItem"].AcceptChanges();
                    this.m_CheckList.AcceptChanges();
                    this.ultraGridCheckList.DataSource = this.m_CheckList;
                }
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
            }
            return msg;
        }

        private Messages LoadErrorList(string itemCode)
        {
            Messages msg = new Messages();

            TSModelFacade tsFacade = new TSModelFacade(this.DataProvider);
            try
            {
                this.ClearErrorList();

                object[] errorCodeGroups = tsFacade.GetErrorCodeGroupByItemCode(itemCode);
                if (errorCodeGroups != null)
                {
                    string errorGroupList = "";
                    DataRow newRow;
                    foreach (ErrorCodeGroupA errorGroup in errorCodeGroups)
                    {
                        errorGroupList += errorGroup.ErrorCodeGroup + ",";

                        newRow = this.m_ErrorList.Tables["ErrorGroup"].NewRow();
                        newRow["ErrorGroupCode"] = errorGroup.ErrorCodeGroup;
                        newRow["ErrorGroupDescription"] = errorGroup.ErrorCodeGroupDescription;
                        this.m_ErrorList.Tables["ErrorGroup"].Rows.Add(newRow);
                    }
                    if (errorGroupList.Length > 0)
                    {
                        errorGroupList = errorGroupList.Substring(0, errorGroupList.Length - 1);
                    }

                    // Get ErrorCode List By ErrorGroupList
                    if (errorGroupList.Length > 0)
                    {
                        object[] errorCodes = tsFacade.GetErrorCodeByErrorGroupList(errorGroupList);
                        if (errorCodes != null)
                        {
                            DataRow row;
                            foreach (ErrorGrou2ErrorCode4OQC eg2ec in errorCodes)
                            {
                                row = this.m_ErrorList.Tables["ErrorCode"].NewRow();
                                row["Checked"] = "false";
                                row["ErrorCodeCode"] = eg2ec.ErrorCode;
                                row["ErrorCodeDescription"] = eg2ec.ErrorCodeDescription;
                                row["ErrorGroupCode"] = eg2ec.ErrorCodeGroup;
                                this.m_ErrorList.Tables["ErrorCode"].Rows.Add(row);
                            }
                        }
                    }

                    this.m_ErrorList.Tables["ErrorGroup"].AcceptChanges();
                    this.m_ErrorList.Tables["ErrorCode"].AcceptChanges();
                    this.m_ErrorList.AcceptChanges();
                    this.ultraGridErrorList.DataSource = this.m_ErrorList;
                }
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
            }

            return msg;
        }

        private void ClearCheckList()
        {
            if (this.m_CheckList == null)
            {
                return;
            }
            this.m_CheckList.Tables["CheckItem"].Rows.Clear();
            this.m_CheckList.Tables["CheckGroup"].Rows.Clear();

            this.m_CheckList.Tables["CheckItem"].AcceptChanges();
            this.m_CheckList.Tables["CheckGroup"].AcceptChanges();
            this.m_CheckList.AcceptChanges();

            this.checkBoxSelectAll.Checked = false;
        }

        private void ClearErrorList()
        {
            if (this.m_ErrorList == null)
            {
                return;
            }
            this.m_ErrorList.Tables["ErrorCode"].Rows.Clear();
            this.m_ErrorList.Tables["ErrorGroup"].Rows.Clear();

            this.m_ErrorList.Tables["ErrorCode"].AcceptChanges();
            this.m_ErrorList.Tables["ErrorGroup"].AcceptChanges();
            this.m_ErrorList.AcceptChanges();

            this.listBoxSelectedErrorList.Items.Clear();
            this.listBoxSelectedErrorList.Refresh();
        }

        private bool ValidInputForLotPass()
        {


            return true;
        }

        private bool ValidInput()
        {
            if (this.ucLabelEditInput.Value.Trim().Length == 0)
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$CS_Please_Input_RunningCard"), false);
                this.ucLabelEditInput.TextFocus(false, true);
                return false;
            }

            for (int i = 0; i < this.ultraGridCheckList.Rows.Count; i++)
            {
                if (this.ultraGridCheckList.Rows[i].HasChild(false))
                {
                    for (int j = 0; j < this.ultraGridCheckList.Rows[i].ChildBands[0].Rows.Count; j++)
                    {
                        if (string.Compare(Convert.ToString(this.ultraGridCheckList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"].Value), "true", true) == 0)
                        {
                            if (string.Compare(Convert.ToString(this.ultraGridCheckList.Rows[i].ChildBands[0].Rows[j].Cells["Result"].Value), "true", true) == 0
                                && Convert.ToString(this.ultraGridCheckList.Rows[i].ChildBands[0].Rows[j].Cells["Grade"].Value) == "")
                            {
                                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$Error_MustChooseGrade"), false);
                                if (this.ultraGridCheckList.ActiveRow != null)
                                {
                                    this.ultraGridCheckList.ActiveRow.Selected = false;
                                    this.ultraGridCheckList.ActiveRow = null;
                                }
                                this.ultraGridCheckList.Rows[i].Expanded = true;
                                this.ultraGridCheckList.Rows[i].ChildBands[0].Rows[j].Selected = true;
                                this.ultraGridCheckList.Rows[i].ChildBands[0].Rows[j].Activate();
                                return false;
                            }

                            if (System.Text.Encoding.Default.GetByteCount(this.ultraGridCheckList.Rows[i].ChildBands[0].Rows[j].Cells["Memo"].Value.ToString()) > 100)
                            {
                                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditMemo.Caption + ": " + this.ucLabelEditMemo.Value, new UserControl.Message(MessageType.Error, "$Error_MemoMaxLength"), false);
                                if (this.ultraGridCheckList.ActiveRow != null)
                                {
                                    this.ultraGridCheckList.ActiveRow.Selected = false;
                                    this.ultraGridCheckList.ActiveRow = null;
                                }
                                this.ultraGridCheckList.Rows[i].Expanded = true;
                                this.ultraGridCheckList.Rows[i].ChildBands[0].Rows[j].Selected = true;
                                this.ultraGridCheckList.Rows[i].ChildBands[0].Rows[j].Activate();
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private object[] GetSelectedCheckGroupList()
        {
            ArrayList checkGroupList = new ArrayList();
            OQCLot2CheckGroup lot2CheckGroup;

            foreach (DataRow row in this.m_CheckList.Tables["CheckGroup"].Rows)
            {
                if (string.Compare(Convert.ToString(row["Checked"]), "true", true) == 0)
                {
                    lot2CheckGroup = new OQCLot2CheckGroup();
                    lot2CheckGroup.CheckedCount = Convert.ToInt32(row["CheckedCount"]);
                    lot2CheckGroup.CheckGroup = Convert.ToString(row["CheckGroupCode"]);
                    lot2CheckGroup.LOTNO = this.m_LotNo;
                    lot2CheckGroup.LotSequence = OQCFacade.Lot_Sequence_Default;
                    lot2CheckGroup.NeedCheckCount = Convert.ToInt32(row["NeedCheckCount"]);
                    checkGroupList.Add(lot2CheckGroup);
                }
            }
            return checkGroupList.ToArray();
        }

        private object[] GetSelectedCheckItemList(ProductInfo product)
        {
            ArrayList checkItemList = new ArrayList();
            OQCLOTCardCheckList lotCardCheckList;

            foreach (DataRow row in this.m_CheckList.Tables["CheckItem"].Rows)
            {
                if (string.Compare(Convert.ToString(row["Checked"]), "true", true) == 0)
                {
                    lotCardCheckList = new OQCLOTCardCheckList();
                    lotCardCheckList.CheckGroup = Convert.ToString(row["CheckGroupCode"]);
                    lotCardCheckList.CheckItemCode = Convert.ToString(row["CheckItemCode"]);
                    lotCardCheckList.CheckSequence = this.m_MaxCheckSequence;
                    lotCardCheckList.EAttribute1 = "";
                    lotCardCheckList.Grade = Convert.ToString(row["Grade"]);
                    lotCardCheckList.ItemCode = product.LastSimulation.ItemCode;
                    lotCardCheckList.LOTNO = this.m_LotNo;
                    lotCardCheckList.LotSequence = OQCFacade.Lot_Sequence_Default;
                    lotCardCheckList.MaintainUser = ApplicationService.Current().UserCode;
                    lotCardCheckList.MEMO = FormatHelper.CleanString(Convert.ToString(row["Memo"]), 100);
                    lotCardCheckList.MOCode = product.LastSimulation.MOCode;
                    lotCardCheckList.ModelCode = product.LastSimulation.ModelCode;
                    lotCardCheckList.Result = (string.Compare(Convert.ToString(row["Result"]), "true", true) == 0) ? "NG" : "GOOD";
                    lotCardCheckList.RunningCard = product.LastSimulation.RunningCard;
                    lotCardCheckList.RunningCardSequence = product.LastSimulation.RunningCardSequence;

                    if (lotCardCheckList.Result == "GOOD")
                    {
                        lotCardCheckList.Grade = "";
                    }
                    else
                    {
                        m_HasNG = true;
                    }

                    checkItemList.Add(lotCardCheckList);
                }
            }

            return checkItemList.ToArray();
        }

        private object[] GetSelectedErrorCodeList()
        {
            ArrayList errorCodeList = new ArrayList();
            ErrorCodeGroup2ErrorCode ecg2ec;

            foreach (DataRow row in this.m_ErrorList.Tables["ErrorCode"].Rows)
            {
                if (string.Compare(Convert.ToString(row["Checked"]), "true", true) == 0)
                {
                    ecg2ec = new ErrorCodeGroup2ErrorCode();
                    ecg2ec.ErrorCodeGroup = Convert.ToString(row["ErrorGroupCode"]);
                    ecg2ec.ErrorCode = Convert.ToString(row["ErrorCodeCode"]);
                    errorCodeList.Add(ecg2ec);
                }
            }

            return errorCodeList.ToArray();
        }

        private void listBoxSelectedErrorList_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxSelectedErrorList.SelectedIndex >= 0)
            {
                if (this.ultraGridErrorList.ActiveRow != null)
                {
                    this.ultraGridErrorList.ActiveRow.Selected = false;
                    this.ultraGridErrorList.ActiveRow = null;
                }

                string selectValue = this.listBoxSelectedErrorList.SelectedItem.ToString();
                string errorCodeGroup = selectValue.Split(':')[0];
                string errorCode = selectValue.Split(':')[1];

                for (int i = 0; i < this.ultraGridErrorList.Rows.Count; i++)
                {
                    if (string.Compare(this.ultraGridErrorList.Rows[i].Cells["ErrorGroupCode"].Value.ToString(), errorCodeGroup, true) == 0
                        && this.ultraGridErrorList.Rows[i].HasChild(false))
                    {
                        for (int j = 0; j < this.ultraGridErrorList.Rows[i].ChildBands[0].Rows.Count; j++)
                        {
                            if (string.Compare(this.ultraGridErrorList.Rows[i].ChildBands[0].Rows[j].Cells["ErrorCodeCode"].Value.ToString(),
                                errorCode, true) == 0)
                            {
                                this.ultraGridErrorList.Rows[i].Expanded = true;
                                this.ultraGridErrorList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"].Value = "false";
                                this.ultraGridErrorList.Rows[i].ChildBands[0].Rows[j].Selected = true;
                                this.ultraGridErrorList.Rows[i].ChildBands[0].Rows[j].Activate();
                            }
                        }
                    }
                }

                this.listBoxSelectedErrorList.Items.Remove(selectValue);

                this.ultraGridErrorList.UpdateData();
                this.listBoxSelectedErrorList.Refresh();
            }
        }

        private void ucLabelEditCartonCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.GetLotNo();
            }
        }
    }
}