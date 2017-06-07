using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Client.Service;
using UserControl;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Package;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.OQC;

namespace BenQGuru.eMES.Client
{
    public partial class FCollectionCarton_BV : BaseForm
    {
        private int m_FlowControl = 1;
        private string _FunctionName = "FCollectCarton_BV";
        DataCollectFacade _face;
        CartonCollection cartonCollection;
        
        Carton2RCARD[] carton2RCARD;
        private DataTable dt = new DataTable();

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FCollectionCarton_BV()
        {
            InitializeComponent();

            UserControl.UIStyleBuilder.FormUI(this);
            this.ultraGridDetail.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            this.ultraGridDetail.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridDetail.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridDetail.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridDetail.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridDetail.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridDetail.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridDetail.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridDetail.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridDetail.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
            this.m_FlowControl = 1;
            this.SetInputMessageByFlowControl();
        }

        private void FCollectionCarton_BV_Load(object sender, EventArgs e)
        {
            //this._FunctionName = this.Text;
            //ucMessage.AddEx(this._FunctionName, "", new UserControl.Message("$CS_Please_Input_CartonNo"), false);
            //this.ucLabelCartonNo.TextFocus(false, true);
            //_face = new DataCollectFacade(this.DataProvider);
            InitializeDataTable();
            //this.InitializeGrid(carton2RCARD);
        }

        private void SetInputMessageByFlowControl()
        {
            Messages msg = new Messages();
            if (m_FlowControl == 1)        //请输入Carton箱号
            {
                ucMessage.AddEx(this._FunctionName, "", new UserControl.Message(">>$CS_Please_Input_CartonNo"), false);
                this.ucLabelCartonNo.TextFocus(false, true);
            }
            else if (m_FlowControl == 2)   //请输入产品序列号
            {
                ucMessage.AddEx(this._FunctionName, "", new UserControl.Message(">>$CS_Please_Input_RunningCard"), false);
                this.ucLabelRCardForCarton.TextFocus(false, true);
            }
            else if (m_FlowControl == 3)   //请再输入产品序列号
            {
                ucMessage.AddEx(this._FunctionName, "", new UserControl.Message(">>$CS_Please_Input_RunningCard_Again"), false);
                this.ucLabelRCardForCarton.TextFocus(false, true);
            }
        }

        private void ucLabelCartonNo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (e.KeyChar == '\r')
                {
                    ucMessage.AddWithoutEnter("<<");
                    ucMessage.AddBoldText(ucLabelCartonNo.Value.Trim());
                    if (this.ucLabelCartonNo.Value.Trim() == "")
                    {
                        m_FlowControl = 1;
                        this.SetInputMessageByFlowControl();
                        return;
                    }

                    string cartonNo = FormatHelper.CleanString(ucLabelCartonNo.Value.Trim().ToUpper());
                    if (!checkCarton(cartonNo))
                    {
                        this.GetData(cartonNo);
                        this.SetUCLabelValue(cartonCollection);
                        this.InitializeGrid(carton2RCARD);
                        this.ucLabelCartonNo.TextFocus(false, true);
                        return;
                    }
                    this.GetData(cartonNo);
                    this.SetUCLabelValue(this.cartonCollection);
                    this.InitializeGrid(this.carton2RCARD);
                    this.m_FlowControl = 2;
                    this.SetInputMessageByFlowControl();
                    this.ucLabelRCardForCarton.TextFocus(false, true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ApplicationRun.GetInfoForm().AddEx(ex.Message);
                this.m_FlowControl = 1;
                this.SetInputMessageByFlowControl();
            }
        }

        private bool checkCarton(string cartonNo)
        {
            #region Carton Length Check
            if (chkCartonLen.Checked && chkCartonLen.Value.Trim().Length > 0)
            {
                if (cartonNo.Length != Convert.ToInt32(chkCartonLen.Value.Replace("-", "")))
                {
                    ucMessage.AddEx(this._FunctionName, "$CS_CARTON_NO: " + this.ucLabelCartonNo.Value, new UserControl.Message(MessageType.Error, "$CS_CARTON_NO_LEN_CHECK_FAIL"), false);
                    this.m_FlowControl = 1;
                    this.SetInputMessageByFlowControl();
                    return false;
                }
            }
            #endregion

            #region Carton First Char Check
            if (chkCartonFChar.Checked && chkCartonFChar.Value.Trim().Length > 0)
            {
                if (cartonNo.IndexOf(chkCartonFChar.Value.Trim()) != 0)
                {
                    ucMessage.AddEx(this._FunctionName, "$CS_CARTON_NO: " + this.ucLabelCartonNo.Value, new UserControl.Message(MessageType.Error, "$CS_CARTON_NO_FCHAR_CHECK_FAIL"), false);
                    this.m_FlowControl = 1;
                    this.SetInputMessageByFlowControl();
                    return false;
                }
            }

            #endregion

            #region 判断是否箱中已满
            PackageFacade pf = new PackageFacade(DataProvider);
            CARTONINFO objCartonInfo = pf.GetCARTONINFO(cartonNo) as CARTONINFO;
            if (objCartonInfo != null)
            {
                if (objCartonInfo.CAPACITY == objCartonInfo.COLLECTED)
                {
                    ucMessage.AddEx(this._FunctionName, "$CS_CARTON_NO: " + this.ucLabelCartonNo.Value, new UserControl.Message(MessageType.Error, "$CARTON_ALREADY_FILL_OUT"), false);
                    this.m_FlowControl = 1;
                    this.SetInputMessageByFlowControl();
                    return false;
                }
            }
            #endregion


            return true;

        }

        private void ucLabelRCardForCarton_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (CartonPack())
                {
                    this.GetData(FormatHelper.CleanString(ucLabelCartonNo.Value.Trim().ToUpper()));
                    this.SetUCLabelValue(this.cartonCollection);
                    this.InitializeGrid(this.carton2RCARD);
                    if (this.ucLabelCartonCapacity.Value == this.ucLabelCartonCollected.Value)
                    {
                        this.ucLabelCartonNo.TextFocus(false, true);
                    }
                    else
                    {
                        this.ucLabelRCardForCarton.TextFocus(false, true);
                    }
                }
            }
        }

        private bool CartonPack()
        {
            if (this.ucLabelRCardForCarton.Value.Trim() == "")
            {
                this.SetInputMessageByFlowControl();
                return false;
            }
            string rcard = FormatHelper.CleanString(this.ucLabelRCardForCarton.Value.Trim().ToUpper());
            //转换成起始序列号
            if (_face == null) _face = new DataCollectFacade(this.DataProvider);
            string sourceCard = _face.GetSourceCard(rcard, string.Empty);
            //end

            if (this.ucLabelCartonNo.Value.Trim() == "")
            {
                m_FlowControl = 1;
                this.SetInputMessageByFlowControl();
                return false;
            }

            #region RCard Length Check
            if (chkCardLen.Checked && chkCardLen.Value.Trim().Length > 0)
            {
                if (rcard.Length != Convert.ToInt32(chkCardLen.Value.Replace("-", "")))
                {
                    ucMessage.AddEx(this._FunctionName, "$CS_RCARD: " + this.ucLabelRCardForCarton.Value, new UserControl.Message(MessageType.Error, "$CS_CARD_NO_LEN_CHECK_FAIL"), false);
                    this.m_FlowControl = 3;
                    this.SetInputMessageByFlowControl();
                    return false;
                }
            }
            #endregion
            #region RCard First Char Check
            if (chkCardFChar.Checked && chkCardFChar.Value.Trim().Length > 0)
            {
                if (rcard.IndexOf(chkCardFChar.Value.Trim()) != 0)
                {
                    ucMessage.AddEx(this._FunctionName, "$CS_RCARD: " + this.ucLabelRCardForCarton.Value, new UserControl.Message(MessageType.Error, "$CS_CARD_NO_FCHAR_CHECK_FAIL"), false);
                    this.m_FlowControl = 3;
                    this.SetInputMessageByFlowControl();
                    return false;
                }
            }
            #endregion

            ucMessage.AddWithoutEnter("<<");
            ucMessage.AddBoldText(ucLabelRCardForCarton.Value.Trim());

            if (!checkRcard(rcard, sourceCard))
            {
                this.m_FlowControl = 3;
                this.SetInputMessageByFlowControl();
                return false;
            }

            Simulation objSimulation = _face.GetSimulation(sourceCard.Trim().ToUpper()) as Simulation;
            string lastAction = objSimulation.LastAction.Trim();

            ItemFacade itemFacade = new ItemFacade(DataProvider);
            Item item = itemFacade.GetItem((objSimulation as Simulation).ItemCode.ToUpper().Trim(), GlobalVariables.CurrentOrganizations.First().OrganizationID) as Item;
            string newMOCode = objSimulation.MOCode.Trim().ToUpper();

            PackageFacade pf = new PackageFacade(DataProvider);
            //check产品序列号是否重复包装
            //object rcardobj = pf.GetCarton2RCARD("", sourceCard.Trim().ToUpper());
            object rcardobj = pf.GetCarton2RcardByRcard(sourceCard.Trim().ToUpper());
            if (rcardobj != null)
            {
                ucMessage.AddEx(this._FunctionName, "$CS_CARTON_NO: " + ((Carton2RCARD)rcardobj).CartonCode, new UserControl.Message(MessageType.Error, "$CS_CARD_HAS_PACKED"), false);
                this.m_FlowControl = 3;
                this.SetInputMessageByFlowControl();
                return false;
            }

            string cartonNo = FormatHelper.CleanString(ucLabelCartonNo.Value.Trim().ToUpper());

            #region OQC检查
            //modified by lisa@2012-8-29
            //1,序列号是维修回流过来的，即属于未判定的LOT并且不属于任何小箱，应该装在之前的箱中。
            OQCFacade _OQCFacade = new OQCFacade(this.DataProvider);
            object lot2Card = _OQCFacade.GetOQCLot2Card(sourceCard.Trim().ToUpper(), newMOCode, "", "");
            Boolean blnCheckCarton = true;
            if (lot2Card != null)
            {
                OQCLot oqcLot = _OQCFacade.GetOQCLot((lot2Card as OQCLot2Card).LOTNO, OQCFacade.Lot_Sequence_Default) as OQCLot;
                if (!(oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Reject || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Rejecting ||
                            oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Pass || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Passing))
                {
                    if ((!String.IsNullOrEmpty(((OQCLot2Card)lot2Card).EAttribute1)) && cartonNo != ((OQCLot2Card)lot2Card).EAttribute1.ToString())
                    {
                        ucMessage.AddEx(this._FunctionName, " ", new UserControl.Message(MessageType.Error, "$RCard:" + sourceCard + "$ShouedPackInCarton:" + ((OQCLot2Card)lot2Card).EAttribute1.ToString()), false);
                        this.m_FlowControl = 3;
                        this.SetInputMessageByFlowControl();
                        return false;
                    }

                    if ((!String.IsNullOrEmpty(((OQCLot2Card)lot2Card).EAttribute1)) && cartonNo == ((OQCLot2Card)lot2Card).EAttribute1.ToString())
                    {
                        blnCheckCarton = false;
                    }

                }
            }

            //2,产品序列号是新的，或者是返工回来的,则不能放入已归属了Lot的小箱
            if (blnCheckCarton)
            {
                object objLot2Carton = _OQCFacade.GetLot2CartonByCartonNo(cartonNo);
                if (objLot2Carton != null)
                {
                    ucMessage.AddEx(this._FunctionName, "$CS_CARTON_NO: " + this.ucLabelCartonNo.Value, new UserControl.Message(MessageType.Error, "$CARTON_ALREADY_OQC"), false);
                    this.m_FlowControl = 1;
                    this.SetInputMessageByFlowControl();
                    return false;
                }
            }
            #endregion


            #region check箱            
            if (!checkCarton(cartonNo))
            {
                return false;
            }
            

            //check一个箱子只能放一个类型的产品,并且只能放一个工单的产品            
            object[] objects = pf.GetCarton2RCARDByCartonNO(cartonNo);
            if (objects != null)
            {
                string oldMOCode = ((Carton2RCARD)objects[0]).MOCode.Trim().ToUpper();
                if (newMOCode != oldMOCode)
                {
                    ucMessage.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$OneCarton_OneMoCode"), true);
                    this.m_FlowControl = 3;
                    this.SetInputMessageByFlowControl();
                    return false;
                }
                object obj = pf.GetItemCodeByMOCode(oldMOCode);
                if (obj != null)
                {
                    if (item.ItemCode != ((CartonCollection)obj).ItemCode)
                    {
                        ucMessage.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$OneCarton_OneProduct"), true);
                        this.m_FlowControl = 3;
                        this.SetInputMessageByFlowControl();
                        return false;
                    }
                }
            }
            #endregion
            // 包装动作
            this.AfterToSave();
            return true;
        }

        private bool checkRcard(string rcard, string sourceCard)
        {
            ItemFacade itemFacade = new ItemFacade(DataProvider);
            // Get Simulation Info
            Simulation objSimulation = _face.GetLastSimulation(sourceCard) as Simulation;
            if (objSimulation == null)
            {
                ucMessage.AddEx(this._FunctionName, "$CS_RCARD: " + rcard, new UserControl.Message(MessageType.Error, "$CardNoProductInfo"), true);
                return false;
            }

            // Get Item Info
            object item = itemFacade.GetItem(objSimulation.ItemCode.ToUpper().Trim(), GlobalVariables.CurrentOrganizations.First().OrganizationID);
            if (item == null)
            {
                ucMessage.AddEx(this._FunctionName, "$CS_RCARD: " + rcard, new UserControl.Message(MessageType.Error, "$Error_ItemCode_NotExist $Domain_Item=" + (objSimulation as Simulation).ItemCode), true);
                return false;
            }

            return true;
        }

        private void AfterToSave()
        {
            // 包装动作
            string rcard = FormatHelper.CleanString(this.ucLabelRCardForCarton.Value.Trim().ToUpper());
            //转换成起始序列号
            string sourceCard = _face.GetSourceCard(rcard, string.Empty);
            //end
            if (sourceCard.Length > 0)
            {
                DoAction(rcard, sourceCard);
            }
        }

        private void DoAction(string rcard, string sourceCard)
        {
            Messages messages = new Messages();

            ActionOnLineHelper actionOnLineHelper = new ActionOnLineHelper(this.DataProvider);
            ActionFactory actionFactory = new ActionFactory(this.DataProvider);
            IAction actionCartonPack = actionFactory.CreateAction(ActionType.DataCollectAction_Carton);

            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            DataProvider.BeginTransaction();
            try
            {
                messages.AddMessages(AddSingleIDIntoCartonAndTry(sourceCard, messages, actionCartonPack, actionOnLineHelper));
                if (!messages.IsSuccess())
                {
                    this.DataProvider.RollbackTransaction();
                    InitMessage("", false, 0, rcard, false);
                }
                else
                {
                    this.DataProvider.CommitTransaction();
                    InitMessage("", false, 0, rcard, true);
                }
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ApplicationRun.GetInfoForm().AddEx(ex.Message);
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "" + this.ucLabelRCardForCarton.Value, messages, false);
                if (!messages.IsSuccess())
                {
                    this.ucLabelRCardForCarton.TextFocus(false, true);
                }
            }
            //if (messages.IsSuccess())
            //{
            //    RefreshCartonNumber();
            //}

        }

        //对单个的ID进行对立
        private Messages AddSingleIDIntoCartonAndTry(string rcard, Messages messages, IAction action, ActionOnLineHelper actionOnLineHelper)
        {
            //转换成起始序列号
            string sourceCard = _face.GetSourceCard(rcard.Trim().ToUpper(), string.Empty);
            //end

            Messages newMessages = actionOnLineHelper.GetIDInfo(sourceCard);
            string cartonNo = FormatHelper.CleanString(this.ucLabelCartonNo.Value);

            ProductInfo product = (ProductInfo)newMessages.GetData().Values[0];
            if (product.LastSimulation == null)
            {
                newMessages.Add(new UserControl.Message(new Exception("$Error_LastSimulation_IsNull!")));
                return newMessages;
            }
            if (newMessages.IsSuccess())
            {

                CartonPackEventArgs cartonPackEventArgs = new CartonPackEventArgs(ActionType.DataCollectAction_Carton,
                    sourceCard, ApplicationService.Current().UserCode,
                    ApplicationService.Current().ResourceCode,
                    FormatHelper.CleanString(""),
                    cartonNo,
                    product);

                newMessages.AddMessages(action.Execute(cartonPackEventArgs));
            }
            if (newMessages.IsSuccess())
            {
                PackageFacade packageFacade = new PackageFacade(this.DataProvider);
                packageFacade.addCarton2RCARD(cartonNo, sourceCard, ApplicationService.Current().UserCode, product.LastSimulation.MOCode);
                //记log
                packageFacade.addCarton2RCARDLog(cartonNo, sourceCard, ApplicationService.Current().UserCode);
            }

            return newMessages;
        }

        private void InitMessage(string lotNo, bool isCheckMaxSize, decimal OQCMaxSize, string runningCard, bool result)
        {
            if (result)
            {
                this.ucMessage.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Success, runningCard + " $CS_Add_Success"), false);
                this.ucLabelRCardForCarton.Value = "";
                ApplicationRun.GetQtyForm().RefreshQty();
            }
            else
            {
                this.ucMessage.AddEx(this._FunctionName, "$CS_RCARD: " + runningCard, new UserControl.Message(MessageType.Error, runningCard + " $CS_Add_Fail"), true);
                //Add By Vivian.Sun 20090903 for hangjia version
                this.m_FlowControl = 3;
                this.SetInputMessageByFlowControl();
                //End Add
            }

        }

        //private void RefreshCartonNumber()
        //{
        //    string cartonNo = FormatHelper.CleanString(this.ucLabelCartonNo.Value.Trim().ToUpper());
        //    CARTONINFO objCartonInfo = (new Package.PackageFacade(DataProvider)).GetCARTONINFO(cartonNo) as CARTONINFO;
        //    //Refresh CartonCapacity and Lot Capacity
        //    if (objCartonInfo != null)
        //    {
        //        this.ucLabelCartonCollected.Value = objCartonInfo.COLLECTED.ToString();
        //    }
        //    else
        //    {
        //        this.ucLabelCartonCollected.Value = "";
        //    }

        //    //Check 
        //    if (objCartonInfo != null && objCartonInfo.CAPACITY == objCartonInfo.COLLECTED)
        //    {
        //        ucMessage.AddEx(this._FunctionName, "包装箱号: " + this.ucLabelCartonNo.Value, new UserControl.Message(MessageType.Normal, "$CARTON_ALREADY_FULL_PlEASE_CHANGE"), false);
        //        this.m_FlowControl = 1;
        //        this.SetInputMessageByFlowControl();
        //        return;
        //    }

        //    this.m_FlowControl = 3;
        //    this.SetInputMessageByFlowControl();
        //    //End Add
        //}

        public void GetData(string cartonNo)
        {
            PackageFacade pf = new PackageFacade(DataProvider);
            CARTONINFO objCartonInfo = pf.GetCARTONINFO(cartonNo) as CARTONINFO;
            if (objCartonInfo != null)
            {
                this.cartonCollection = new CartonCollection();
                this.cartonCollection.CAPACITY = objCartonInfo.CAPACITY;
                this.cartonCollection.COLLECTED = objCartonInfo.COLLECTED;

                object[] objs = pf.GetCarton2RCARDByCartonNO(cartonNo);
                if (objs != null)
                {
                    this.carton2RCARD = new Carton2RCARD[objs.Length];
                    for (int i = 0; i < objs.Length; i++)
                    {
                        this.carton2RCARD[i] = (Carton2RCARD)objs[i];
                    }
                }
                if (this.carton2RCARD != null)
                {
                    this.cartonCollection.MOCode = this.carton2RCARD[0].MOCode;
                    object obj = pf.GetItemCodeByMOCode(this.carton2RCARD[0].MOCode);
                    if (obj != null)
                    {
                        this.cartonCollection.ItemCode = ((CartonCollection)obj).ItemCode;
                        this.cartonCollection.ItemDescription = ((CartonCollection)obj).ItemDescription;
                        ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                        Item2SNCheck item2SNCheck = (Item2SNCheck)itemFacade.GetItem2SNCheck(((CartonCollection)obj).ItemCode, ItemCheckType.ItemCheckType_SERIAL);
                        if (item2SNCheck != null)
                        {
                            if (item2SNCheck.SNLength > 0)
                            {
                                chkCardLen.Checked = true;
                                chkCardLen.Value = item2SNCheck.SNLength.ToString();
                            }

                            if (!string.IsNullOrEmpty(item2SNCheck.SNPrefix))
                            {
                                chkCardFChar.Checked = true;
                                chkCardFChar.Value = item2SNCheck.SNPrefix;
                            }
                        }
                    }
                }
            }
        }

        public void SetUCLabelValue(CartonCollection cartonCollection)
        {
            if (cartonCollection == null)
            {
                this.ucLabelItemCode.Value = string.Empty;
                this.ucLabelItemDesc.Value = string.Empty;
                this.ucLabelMOCode.Value = string.Empty;
                this.ucLabelCartonCapacity.Value = string.Empty;
                this.ucLabelCartonCollected.Value = string.Empty;

            }
            else
            {
                this.ucLabelItemCode.Value = cartonCollection.ItemCode;
                this.ucLabelItemDesc.Value = cartonCollection.ItemDescription;
                this.ucLabelMOCode.Value = cartonCollection.MOCode;
                this.ucLabelCartonCapacity.Value = cartonCollection.CAPACITY.ToString();
                this.ucLabelCartonCollected.Value = cartonCollection.COLLECTED.ToString();

            }
            this.cartonCollection = null;
        }

        private void ultraGridDetail_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraWinGridHelper ultraWinGridHelper = new UltraWinGridHelper(this.ultraGridDetail);
            //ultraWinGridHelper.AddCheckColumn("checkbox", "*");
            //ultraWinGridHelper.AddCommonColumn();
            e.Layout.Bands[0].Columns["runningcard"].Header.Caption = "产品序列号";
            e.Layout.Bands[0].Columns["runningcard"].Width = 200;
            //this.InitGridLanguage(ultraGridDetail);
        }

        private void InitializeDataTable()
        {
            dt.Columns.Add("runningcard");

            //dtSource.Columns.Add("opDescription");
            ultraGridDetail.DataSource = dt;
        }


        private void InitializeGrid(object[] objs)
        {
            //清空grid
            this.dt.Rows.Clear();
            this.ultraGridDetail.DataSource = null;

            if (objs != null)
            {
                dt.Columns.Clear();
                dt.Columns.Add("runningcard", typeof(string)).ReadOnly = true;
                for (int i = 0; i < objs.Length; i++)
                {
                    dt.Rows.Add(new object[] { ((Carton2RCARD)objs[i]).Rcard });

                    dt.AcceptChanges();
                }
                this.ultraGridDetail.DataSource = dt;
                this.InitGridLanguage(ultraGridDetail);
            }
            
            this.carton2RCARD = null;
        }

    }
}
