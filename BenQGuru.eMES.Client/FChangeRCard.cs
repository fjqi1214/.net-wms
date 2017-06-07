using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.Client
{
    public partial class FChangeRCard : BaseForm
    {
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        string _FunctionName = string.Empty;

        public FChangeRCard()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
        }

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            //获取产品原始的序列号
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceOldRCard = dataCollectFacade.GetSourceCard(this.ucLabelEditOldRCard.Value.Trim().ToUpper(), string.Empty);
            string sourceNewRCard = dataCollectFacade.GetSourceCard(this.ucLabelEditNewRCard.Value.Trim().ToUpper(), string.Empty);

            //check OldRcard Is Empty
            if (string.IsNullOrEmpty(this.ucLabelEditOldRCard.Value.Trim()))
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditOldRCard.Value + ":" + this.ucLabelEditOldRCard.Value,
                                                    new UserControl.Message(MessageType.Error, "$CS_OLDRCARD_ISNULL"), true);
                this.ucLabelEditOldRCard.TextFocus(true, true);
                return;
            }

            //Check OldRcard Is Exist in ProductInfo
            Object ObjectSimulationOld = dataCollectFacade.GetLastSimulationReport(FormatHelper.CleanString(sourceOldRCard.Trim().ToUpper()));
            if (ObjectSimulationOld == null)
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditOldRCard.Value + ":" + this.ucLabelEditOldRCard.Value,
                                                    new UserControl.Message(MessageType.Error, "$CS_OLDRCARD_IS_NOT_EXIT"), true);
                this.ucLabelEditOldRCard.TextFocus(false, true);
                return;
            }

            //Check NewRcard Is Empty
            if (string.IsNullOrEmpty(this.ucLabelEditNewRCard.Value.Trim()))
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditNewRCard.Value + ":" + this.ucLabelEditNewRCard.Value,
                                                    new UserControl.Message(MessageType.Error, "$CS_newIDisNull"), true);
                this.ucLabelEditNewRCard.TextFocus(false, true);
                return;
            }

            //Compare NewRcard and OldRcard Is  Same
            if (this.ucLabelEditNewRCard.Value.Trim() == this.ucLabelEditOldRCard.Value.Trim())
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditNewRCard.Value + ":" + this.ucLabelEditNewRCard.Value,
                                                    new UserControl.Message(MessageType.Error, "$CS_SAMERCARD"), true);
                this.ucLabelEditNewRCard.TextFocus(false, true);
                return;
            }

            //Check OldRcard Is Not Exist in ProductInfo
            Object ObjectSimulationNew = dataCollectFacade.GetLastSimulationReport(FormatHelper.CleanString(sourceNewRCard.Trim().ToUpper()));
            if (ObjectSimulationNew != null)
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditNewRCard.Value + ":" + this.ucLabelEditNewRCard.Value,
                                                    new UserControl.Message(MessageType.Error, "$CS_NEWRCARD_IS_PRODUCTINFO"), true);
                this.ucLabelEditNewRCard.TextFocus(false, true);
                return;
            }

            //Check ChangeReason is Empty
            if (string.IsNullOrEmpty(this.ucLabelEditReason.Value.Trim()))
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditReason.Value + ":" + this.ucLabelEditReason.Value,
                                                    new UserControl.Message(MessageType.Error, "$CS_INPUT_CHANGEREASON"), true);
                this.ucLabelEditReason.TextFocus(false, true);
                return;
            }

            BaseSetting.BaseModelFacade baseModelFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);

            string OldRcard = FormatHelper.CleanString(this.ucLabelEditOldRCard.Value.Trim().ToUpper());
            string NewRcard = FormatHelper.CleanString(this.ucLabelEditNewRCard.Value.Trim().ToUpper());
            string OutPutResoult = string.Empty;

            ProcedureCondition procedureCondition = new ProcedureCondition("CHANGERCARD",
                                                                            new ProcedureParameter[] {
                                                                                new ProcedureParameter("i_FromRCard",typeof(string),40,DirectionType.Input,OldRcard),
                                                                                new ProcedureParameter("i_ToRCard",typeof(string),40,DirectionType.Input,NewRcard),
                                                                                new ProcedureParameter("o_Result",typeof(string),40,DirectionType.Output,OutPutResoult)});

            this.DataProvider.BeginTransaction();
            try
            {
                baseModelFacade.DoRCardChange(ref procedureCondition);

                if (procedureCondition.Parameters[2].Value.ToString().Length == 2)
                {
                    DBDateTime DBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    RCardChange NewRCardChange = baseModelFacade.CreateNewRCardChange();

                    NewRCardChange.RCardFrom = OldRcard;
                    NewRCardChange.RCardTo = NewRcard;
                    NewRCardChange.Reason = FormatHelper.CleanString(this.ucLabelEditReason.Value.Trim());
                    NewRCardChange.MaintainUser = ApplicationService.Current().UserCode;
                    NewRCardChange.MaintainDate = DBDateTime.DBDate;
                    NewRCardChange.MaintainTime = DBDateTime.DBTime;
                    NewRCardChange.EAttribute1 = string.Empty;

                    baseModelFacade.AddRCardChange(NewRCardChange);

                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, procedureCondition.Parameters[2].Value.ToString(),
                                        new UserControl.Message(MessageType.Success, "$CS_RCARDCHANGE_SUCCESS"), true);
                    this.ucLabelEditNewRCard.Value = string.Empty;
                    this.ucLabelEditReason.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(ex), true);
            }
            finally
            {
                this.DataProvider.CommitTransaction();
            }

            if (procedureCondition.Parameters[2].Value.ToString().Length > 2)
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, procedureCondition.Parameters[2].Value.ToString(),
                                                        new UserControl.Message(MessageType.Error, "$CS_RCARDCHANGE_WRONG"), true);
            }
            this.ucLabelEditOldRCard.TextFocus(false, true);
        }

        private void FChangeRCard_Load(object sender, EventArgs e)
        {
            this._FunctionName = this.Text;
            //this.InitPageLanguage();
        }
    }
}