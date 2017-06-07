using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using UserControl;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.Client
{
    public partial class FFrozen : BaseForm
    {
        
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        private OQCFacade _OQCFacade = null;
        public FFrozen()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            if (this.ucLabelEditLotNo.Value.Trim() == string.Empty)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Please_Input_Lot_No"));
                this.ucLabelEditLotNo.TextFocus(true, true);
                return;
            }

            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(this.DataProvider);
            }

            int frozenRCardCount = _OQCFacade.QueryFrozenRCardCount(this.ucLabelEditLotNo.Value.ToString().Trim());

            if (frozenRCardCount > 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Message_LotIsFrozen"));
                return;
            }


            if (this.ucLESeparateMemo.Value.Trim() == string.Empty)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_CMPleaseInputFROZEN"));
                this.ucLESeparateMemo.TextFocus(true, true);
                return;
            } 
            

            OQCLot lot = (OQCLot)this.GetEditObject(this.ucLabelEditLotNo.Value.ToString().Trim());

            if (lot != null)
            {
                try
                {
                    ((SQLDomainDataProvider)this.DataProvider).PersistBroker.AutoCloseConnection = false;
                    ((SQLDomainDataProvider)this.DataProvider).PersistBroker.OpenConnection();
                    this.DataProvider.BeginTransaction();

                    _OQCFacade.FreezeLot(lot,this.ucLESeparateMemo.Value, this.chkBoxForbid.Checked,ApplicationService.Current().UserCode);

                    this.DataProvider.CommitTransaction();

                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_Save_Success"));

                    this.ucLabelEditLotNo.Value = "";
                    this.ucLabelEditItemCode.Value = "";
                    this.ucLESeparateMemo.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLSizeAndCapacityMore.Value = "";
                    this.ucLabelEditRcard.TextFocus(false, true);

                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    throw ex;
                }
                finally
                {
                    ((SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
                    ((SQLDomainDataProvider)this.DataProvider).PersistBroker.AutoCloseConnection = true;
                }

            }
        }


        private object GetEditObject(string lotno)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(this.DataProvider);
            }

            object obj = this._OQCFacade.GetOQCLot(lotno, OQCFacade.Lot_Sequence_Default);

            if (obj != null)
            {
                return (OQCLot)obj;
            }

            return null;
        }

        //获取批号, 实际批量/标准批量与备注值
        private void GetLotNo()
        {
            DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
            string rcard = this.ucLabelEditRcard.Value.ToUpper().Trim();

            //根据当前序列号获取产品的原始序列号
            string sourceRCard = dcf.GetSourceCard(rcard, string.Empty);

            if (rcard != String.Empty)
            {
                object obj = dcf.GetSimulation(sourceRCard);
                if (obj != null)
                {
                    Simulation sim = obj as Simulation;

                    string oqcLotNo = sim.LOTNO;
                    ucLabelEditLotNo.Value = oqcLotNo;
                    LabOQCLotKeyPress();
                }
                else
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$NoProductInfo"));
                    this.ucLabelEditRcard.Value = "";
                    this.ucLabelEditItemCode.Value = "";
                    this.ucLESeparateMemo.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLSizeAndCapacityMore.Value = "";
                    this.ucLabelEditLotNo.TextFocus(false, true);
                }
            }
            else
            {
                ucLabelEditRcard.TextFocus(false, true);
            }
        }

        //按回车键获取
        private void ucLabelEditRcard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                GetLotNo();
            }
        }

        private void ucButtonGetLot_Click(object sender, EventArgs e)
        {
            GetLotNo();
        }

        private void ucLabelEditLotNo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.LabOQCLotKeyPress();
            }
        }

        public void LabOQCLotKeyPress()
        {
            Messages msg = RequesData();
            ApplicationRun.GetInfoForm().Add(msg);

            if (msg.IsSuccess())
            {
                this.ucLESeparateMemo.TextFocus(false, true);
            }
            else
            {
                this.ucLabelEditLotNo.TextFocus(false, true);
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

                this.ucLSizeAndCapacityMore.Value = "";
                this.ucLabelEditItemCode.Value = "";
                this.labelItemDescription.Text = "";
                this.ucLESeparateMemo.Value = "";
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
                    this.ucLSizeAndCapacityMore.Value = "";
                    this.ucLabelEditItemCode.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLESeparateMemo.Value = "";
                    return msg;
                }

                //判断批是否存在tbllot2card
                object[] objs = oqcFacade.GetOQCLot2CardByLotNoAndSeq(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
                if (objs == null || objs.Length == 0)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$Error_LotNoRCard"));
                    this.ucLSizeAndCapacityMore.Value = "";
                    this.ucLabelEditItemCode.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLESeparateMemo.Value = "";
                    return msg;
                }

                //判断有没有通过第一站
                ProductInfo productionInfo = (ProductInfo)actionOnLineHelper.GetIDInfo(((OQCLot2Card)objs[0]).RunningCard).GetData().Values[0];
                if (productionInfo.LastSimulation == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$NoSimulationInfo"));
                    this.ucLSizeAndCapacityMore.Value = "";
                    this.ucLabelEditItemCode.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLESeparateMemo.Value = "";
                    return msg;
                }

                OQCLot lot = obj as OQCLot;

                string itemCode = (objs[0] as OQCLot2Card).ItemCode;
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);

                object item = itemFacade.GetItem(itemCode, lot.OrganizationID);
                if (item == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$Error_ItemCode_NotExist $ItemCode=" + itemCode));
                    this.ucLSizeAndCapacityMore.Value = "";
                    this.ucLabelEditItemCode.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLESeparateMemo.Value = "";
                    return msg;
                }

                this.ucLSizeAndCapacityMore.Value = lot.LotSize.ToString() + "/" + lot.LotCapacity.ToString();
                this.ucLabelEditItemCode.Value = (item as Item).ItemCode;
                this.labelItemDescription.Text = (item as Item).ItemDescription;
                this.ucLESeparateMemo.Value = lot.FrozenReason;
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

        private void FFrozen_Load(object sender, EventArgs e)
        {
            //this.InitPageLanguage();
        }
       
    }
}