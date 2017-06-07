using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#region Project
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.DataCollect;
using UserControl;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.BaseSetting;
#endregion

namespace BenQGuru.eMES.Client
{
    public partial class FOQCLotOperate : BaseForm
    {

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FOQCLotOperate()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
        }

        private string frozenReason = string.Empty;

        //获取批号, 实际批量/标准批量与备注值
        private void GetLotNo()
        {
            DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
            string rcard = this.ucLabelEditRcard.Value.ToUpper().Trim();
            string cartonCode = ucLabelEditCartonCode.Value.ToUpper().Trim();

            string sourceRCard = dcf.GetSourceCard(rcard, string.Empty);
            if (rcard != String.Empty)
            {
                object obj = dcf.GetSimulation(sourceRCard);
                if (obj != null)
                {
                    Simulation sim = obj as Simulation;

                    string oqcLotNo = sim.LOTNO;
                    if (String.IsNullOrEmpty(oqcLotNo))
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$RCard_No_Lot"));
                        this.ucLabelEditLotNo.Value = "";
                        this.Clear();
                        this.ucLabelEditRcard.TextFocus(false, true);
                        return;
                    }

                    if (!CheckLotStatus(oqcLotNo))
                    {
                        this.ucLabelEditLotNo.Value = "";
                        this.Clear();
                        this.ucLabelEditRcard.TextFocus(false, true);
                        return;
                    }
                    LabOQCLotKeyPress();
                }
                else
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$NoProductInfo"));
                    this.ucLabelEditLotNo.Value = "";
                    this.Clear();
                    this.ucLabelEditRcard.TextFocus(false, true);
                }
            }
            //add by alex 2010.11.10
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
                        Messages messages = new Messages();
                        messages.Add(new UserControl.Message(MessageType.Error, "$CS_CARTON_NOT_EXIST_LOT"));
                        ApplicationRun.GetInfoForm().Add(messages);
                        this.ucLabelEditLotNo.Value = "";
                        this.Clear();
                        this.ucLabelEditCartonCode.TextFocus(false, true);
                        return;
                    }
                    if (!CheckLotStatus(lot2Carton.OQCLot.Trim()))
                    {
                        this.ucLabelEditLotNo.Value = "";
                        this.Clear();
                        this.ucLabelEditCartonCode.TextFocus(false, true);
                        return;
                    }
                    LabOQCLotKeyPress();
                }
                else
                {
                    Messages messages = new Messages();
                    messages.Add(new UserControl.Message(MessageType.Error, "$NoLol2CartonInfo"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    this.ucLabelEditLotNo.Value = "";
                    this.Clear();
                    this.ucLabelEditCartonCode.TextFocus(false, true);
                }
            }
            else
            {
                ucLabelEditRcard.TextFocus(false, true);
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

        //按回车键获取
        private void ucLabelEditRcard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                GetLotNo();
            }
        }

        //按"获取批"按扭获取值
        private void ucButtonGetLot_Click(object sender, EventArgs e)
        {
            GetLotNo();
        }

        public void LabOQCLotKeyPress()
        {
            Messages msg = RequesData();
            ApplicationRun.GetInfoForm().Add(msg);

            if (msg.IsSuccess())
            {
                this.ucLabelEditStatusMemo.TextFocus(false, true);
            }
            else
            {
                this.ucLabelEditLotNo.TextFocus(false, true);
            }
        }

        private void ucLabelEditLotNo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.LabOQCLotKeyPress();
            }
        }

        //实际批量/标准批量, 产品, 产品描述, 备注值
        private Messages RequesData()
        {
            Messages msg = new Messages();

            //判断该产品序列号的批号存不存在
            string oqcLotNo = FormatHelper.CleanString(this.ucLabelEditLotNo.Value);
            if (oqcLotNo.Length == 0)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_FQCLOT_NOT_NULL"));

                this.Clear();
                return msg;
            }

            //不允许自动关闭连接
            ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();

            try
            {
                OQCFacade oqcFacade = new OQCFacade(DataProvider);
                ActionOnLineHelper actionOnLineHelper = new ActionOnLineHelper(this.DataProvider);

                //判断批是否存在
                object obj = oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
                if (obj == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));
                    this.ucLabelEditSizeAndCapacity.Value = "";
                    this.ucLabelEditItemCode.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLabelEditStatusMemo.Value = "";
                    return msg;
                }

                //判断批是否存在tbllot2card
                object[] objs = oqcFacade.GetOQCLot2CardByLotNoAndSeq(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
                if (objs == null || objs.Length == 0)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$Error_LotNoRCard"));
                    this.ucLabelEditSizeAndCapacity.Value = "";
                    this.ucLabelEditItemCode.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLabelEditStatusMemo.Value = "";
                    return msg;
                }

                //判断有没有通过第一站
                ProductInfo productionInfo = (ProductInfo)actionOnLineHelper.GetIDInfo(((OQCLot2Card)objs[0]).RunningCard).GetData().Values[0];
                if (productionInfo.LastSimulation == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$NoSimulationInfo"));
                    this.ucLabelEditSizeAndCapacity.Value = "";
                    this.ucLabelEditItemCode.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLabelEditStatusMemo.Value = "";
                    return msg;
                }

                OQCLot lot = obj as OQCLot;

                string itemCode = (objs[0] as OQCLot2Card).ItemCode;
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);

                object item = itemFacade.GetItem(itemCode, lot.OrganizationID);
                if (item == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$Error_ItemCode_NotExist $ItemCode=" + itemCode));
                    this.ucLabelEditSizeAndCapacity.Value = "";
                    this.ucLabelEditItemCode.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLabelEditStatusMemo.Value = "";
                    return msg;
                }

                this.ucLabelEditSizeAndCapacity.Value = lot.LotSize.ToString();// +"/" + lot.LotCapacity.ToString();
                this.ucLabelEditItemCode.Value = (item as Item).ItemCode;
                this.labelItemDescription.Text = (item as Item).ItemDescription;
                this.ucLabelEditStatusMemo.Value = lot.Memo;
                //added by alex 2010/11/10
                int lot2CardCheckCount = oqcFacade.ExactQueryOQCLot2CardCheckCount(lot.LOTNO, OQCFacade.Lot_Sequence_Default);
                int lot2CardCheckNgCount = oqcFacade.ExactQueryOQCLot2CardCheckNgCount(lot.LOTNO, OQCFacade.Lot_Sequence_Default);
                this.ucLabelEditSampleSize.Value = lot2CardCheckCount.ToString();
                this.ucLabelEditSampleNgSize.Value = lot2CardCheckNgCount.ToString();
                this.ucLabelEditSampleGoodSize.Value = (lot2CardCheckCount - lot2CardCheckNgCount).ToString();
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
            }

            return msg;
        }

        //对批通过和批判退的处理
        private void LotOPerate(string lotStatus, bool isForce)
        {
            Messages msg = new Messages();
            bool isFrozen = false;

            string oqcLotNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelEditLotNo.Value));
            ActionFactory actionFactory = new ActionFactory(this.DataProvider);
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);

            //获取OQCLot以及逻辑检查
            object obj = null;
            msg.AddMessages(GetOQCLotToOperate(oqcFacade, oqcLotNo, out obj));
            if (!msg.IsSuccess())
            {
                ApplicationRun.GetInfoForm().Add(msg);
                this.ucLabelEditLotNo.TextFocus(false, true);
                return;
            }

            //其他检查
            if (lotStatus == OQCLotStatus.OQCLotStatus_Reject)
            {
                if (this.ucLabelEditStatusMemo.Value.Trim().Length == 0)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_PleaseInputMemo"));
                    ApplicationRun.GetInfoForm().Add(msg);
                    this.ucLabelEditStatusMemo.TextFocus(false, true);
                    return;
                }
            }
            string msgInfo = String.Empty;
            if (lotStatus == OQCLotStatus.OQCLotStatus_Pass)
            {
                if (isForce)
                {
                    msgInfo = UserControl.MutiLanguages.ParserMessage("$CS_CONFIRM_ForcePassLot");
                }
                else
                {
                    msgInfo = UserControl.MutiLanguages.ParserMessage("$CS_CONFIRM_LOT_PASS");
                }
            }
            else
            {
                if (isForce)
                {
                    msgInfo = UserControl.MutiLanguages.ParserMessage("$CS_CONFIRM_ForceRejectLot");
                }
                else
                {
                    msgInfo = UserControl.MutiLanguages.ParserMessage("$CS_CONFIRM_LOT_RJECT");
                }
            }

            frmDialog dialog = new frmDialog();
            dialog.Text = this.Text;
            dialog.DialogMessage = msgInfo;

            if (DialogResult.OK != dialog.ShowDialog(this))
            {
                return;
            }

            //add by roger xue  2008/10/27
            OQCLot objFrozen = obj as OQCLot;

            if (objFrozen.FrozenStatus == FrozenStatus.STATUS_FRONZEN)
            {
                isFrozen = true;
                FFrozenReason frozenReason = new FFrozenReason();
                string reason = this.ucLabelEditStatusMemo.Value.Trim();
                if (reason.Length == 0)
                {
                    if (lotStatus == OQCLotStatus.OQCLotStatus_Reject)
                    {
                        reason = "批判退";
                    }
                    else
                    {
                        reason = "批判过";
                    }
                }
                frozenReason.Reason = reason;
                frozenReason.Event += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(Form_Event);
                if (DialogResult.Cancel == frozenReason.ShowDialog(this))
                {
                    return;
                }
            }

            //end add

            // 输入用户名、密码确认 add by alex.hu 2010/11/19
            if (isForce)
            {
                string strMsg = UserControl.MutiLanguages.ParserString("$SMT_UnLoadAll_Confirm_UserCode");
                FDialogInput finput = new FDialogInput(strMsg);
                DialogResult dialogResult = finput.ShowDialog();
                if (dialogResult != DialogResult.OK)
                {
                    return;
                }
                string strUserCode = finput.InputText.Trim().ToUpper();
                strMsg = UserControl.MutiLanguages.ParserString("$Please_Input_Password");
                finput = new FDialogInput(strMsg);
                finput.InputPasswordChar = '*';
                dialogResult = finput.ShowDialog();
                if (dialogResult != DialogResult.OK)
                {
                    return;
                }
                string strPassword = finput.InputText.Trim().ToUpper();
                finput.Close();
                BenQGuru.eMES.Security.SecurityFacade security = new BenQGuru.eMES.Security.SecurityFacade(this.DataProvider);
                try
                {
                    object objSec = security.PasswordCheck(strUserCode, strPassword);

                    if (lotStatus == OQCLotStatus.OQCLotStatus_Pass && !security.IsBelongToAdminGroup(strUserCode) && !security.CheckAccessRight(strUserCode, "OQCFORCEPASS"))
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_No_Access_Right"));
                        return;
                    }

                    if (lotStatus == OQCLotStatus.OQCLotStatus_Reject && !security.IsBelongToAdminGroup(strUserCode) && !security.CheckAccessRight(strUserCode, "OQCFORCEREJECT"))
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_No_Access_Right"));
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Messages msgErr = new Messages();
                    msgErr.Add(new UserControl.Message(ex));
                    ApplicationRun.GetInfoForm().Add(msgErr);
                    Application.DoEvents();
                    return;
                }
            }
            //end add

            //把lotcapacity更新成lotsise一样大小,防止此时有产品在产生工序进入批而产生批判定时整个批无法全部完工的并发问题 by hiro 
            Decimal reallyLotCapacity = ((OQCLot)obj).LotCapacity;
            this.DataProvider.BeginTransaction();
            try
            {
                //lock该lot，防止同时对该lot操作产生死锁
                oqcFacade.LockOQCLotByLotNO(oqcLotNo);
                //end 

                oqcFacade.UpdateOQCLotCapacity(((OQCLot)obj).LOTNO, ((OQCLot)obj).LotSize);

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

            //缓解性能问题
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            DataProvider.BeginTransaction();
            try
            {
                //lock该lot，防止同时对该lot操作产生死锁
                oqcFacade.LockOQCLotByLotNO(oqcLotNo);

                //
                msg.AddMessages(GetOQCLotToOperate(oqcFacade, oqcLotNo, out obj));
                if (!msg.IsSuccess())
                {
                    return;
                }

                #region 业务逻辑
                ActionOnLineHelper actinOnlineHelper = new ActionOnLineHelper(this.DataProvider);

                object[] objs = (new ActionOQCHelper(this.DataProvider)).QueryCardOfLot(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
                if (objs == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));
                    return;
                }

                // Added By Hi1/Venus.Feng on 20081111 for Hisense : 根据产生批的资源获取该资源对应的大线的最后一个工序的资源(取一个)
                //string rightResource = (new BaseModelFacade(this.DataProvider)).GetRightResourceForOQCOperate(objFrozen.ResourceCode,
                //            (objs[0] as Simulation).ItemCode, (objs[0] as Simulation).RouteCode);
                //if (string.IsNullOrEmpty(rightResource))
                //{
                //    msg.Add(new UserControl.Message(MessageType.Error, "$Error_NoBigLineFQCResource"));
                //    return;
                //}
                string rightResource = ApplicationService.Current().ResourceCode;
                // End Added

                //对批通过的处理
                if (lotStatus == OQCLotStatus.OQCLotStatus_Pass)
                {
                    IAction actionPass = actionFactory.CreateAction(ActionType.DataCollectAction_OQCPass);

                    OQCPASSEventArgs actionEventArgs = new OQCPASSEventArgs(ActionType.DataCollectAction_OQCPass, ((Domain.DataCollect.Simulation)objs[0]).RunningCard, ApplicationService.Current().
                        UserCode, rightResource, oqcLotNo, null);

                    actionEventArgs.Lot = obj;
                    actionEventArgs.CardOfLot = objs;
                    actionEventArgs.IsForcePass = isForce;
                    actionEventArgs.Memo = FormatHelper.CleanString(this.ucLabelEditStatusMemo.Value.Trim(), 100);

                    //frozen operate
                    actionEventArgs.IsUnFrozen = isFrozen;
                    actionEventArgs.UnFrozenReason = this.frozenReason;

                    msg.AddMessages(actionPass.Execute(actionEventArgs));
                    if (msg.IsSuccess())
                    {
                        msg.Add(new UserControl.Message(MessageType.Success, "$CS_OQCPASSSUCCESS"));
                    }
                }

                //对批判退的处理
                if (lotStatus == OQCLotStatus.OQCLotStatus_Reject)
                {
                    IAction actionReject = actionFactory.CreateAction(ActionType.DataCollectAction_OQCReject);

                    OQCRejectEventArgs actionEventArgs = new OQCRejectEventArgs(ActionType.DataCollectAction_OQCReject, ((Domain.DataCollect.Simulation)objs[0]).RunningCard, ApplicationService.Current().
                        UserCode, ApplicationService.Current().ResourceCode, oqcLotNo, null);

                    actionEventArgs.Lot = obj;
                    actionEventArgs.CardOfLot = objs;
                    actionEventArgs.IsForceReject = isForce;
                    actionEventArgs.IsAutoGenerateReworkSheet = this.chkBoxAutoGenerate.Checked;
                    actionEventArgs.IsCreateNewLot = this.checkBoxAutoLot.Checked;
                    actionEventArgs.Memo = FormatHelper.CleanString(this.ucLabelEditStatusMemo.Value.Trim(), 100);

                    //frozen operate
                    actionEventArgs.IsUnFrozen = isFrozen;
                    actionEventArgs.UnFrozenReason = frozenReason;
                    actionEventArgs.rightResource = rightResource;

                    msg.AddMessages(actionReject.Execute(actionEventArgs));

                    if (msg.IsSuccess())
                    {
                        msg.Add(new UserControl.Message(MessageType.Success, "$CS_OQCREJECTSUCCESS"));
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                msg.Add(new UserControl.Message(ex));
            }
            finally
            {
                ApplicationRun.GetInfoForm().Add(msg);

                if (!msg.IsSuccess())
                {
                    this.DataProvider.RollbackTransaction();
                    this.DataProvider.BeginTransaction();
                }

                obj = oqcFacade.GetOQCLot(((OQCLot)obj).LOTNO, ((OQCLot)obj).LotSequence);
                if (obj != null)
                {
                    oqcFacade.UpdateOQCLotCapacity(((OQCLot)obj).LOTNO, reallyLotCapacity);
                }

                this.DataProvider.CommitTransaction();

                this.ucLabelEditLotNo.TextFocus(false, true);

                //缓解性能问题
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            }
        }

        public void Form_Event(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.frozenReason = e.CustomObject;
        }

        //批通过
        private void ucButtonLotPass_Click(object sender, EventArgs e)
        {
            LotOPerate(OQCLotStatus.OQCLotStatus_Pass, false);
        }

        //批判退
        private void ucButtonLotReject_Click(object sender, EventArgs e)
        {
            LotOPerate(OQCLotStatus.OQCLotStatus_Reject, false);
        }

        //批强制通过
        private void ucButtonLotForcePass_Click(object sender, EventArgs e)
        {
            LotOPerate(OQCLotStatus.OQCLotStatus_Pass, true);
        }

        //批强制判退
        private void ucButtonLotForceReject_Click(object sender, EventArgs e)
        {
            LotOPerate(OQCLotStatus.OQCLotStatus_Reject, true);
        }

        private void chkBoxAutoGenerate_CheckedChanged(object sender, EventArgs e)
        {
            //this.checkBoxAutoLot.Enabled = this.chkBoxAutoGenerate.Checked;
            //this.checkBoxAutoLot.Checked = this.chkBoxAutoGenerate.Checked;
        }

        private Messages GetOQCLotToOperate(OQCFacade oqcFacade, string oqcLotNo, out object obj)
        {
            Messages msg = new Messages();
            obj = null;

            oqcLotNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(oqcLotNo));

            //批号不能为空
            if (oqcLotNo.Length == 0)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_FQCLOT_NOT_NULL"));
                return msg;
            }

            //判断批是否存在
            obj = oqcFacade.GetOQCLot(oqcLotNo, OQCFacade.Lot_Sequence_Default);
            if (obj == null)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));
                return msg;
            }

            //判断该批是否已经判过
            if (((OQCLot)obj).LOTStatus == OQCLotStatus.OQCLotStatus_Pass)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_Already_Pass"));
                return msg;
            }

            //判断该批是否已经判退
            if (((OQCLot)obj).LOTStatus == OQCLotStatus.OQCLotStatus_Reject)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_Already_Reject"));
                return msg;
            }

            return msg;
        }

        private void ucLabelEditCartonCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                GetLotNo();
            }
        }

        private void Clear()
        {
            this.ucLabelEditItemCode.Value = "";
            this.ucLabelEditStatusMemo.Value = "";
            this.labelItemDescription.Text = "";
            this.ucLabelEditSizeAndCapacity.Value = "";
            this.ucLabelEditSampleSize.Value = "";
            this.ucLabelEditSampleGoodSize.Value = "";
            this.ucLabelEditSampleNgSize.Value = ""; ;
        }

        private void FOQCLotOperate_Load(object sender, EventArgs e)
        {
            //this.InitPageLanguage();
        }
    }
}