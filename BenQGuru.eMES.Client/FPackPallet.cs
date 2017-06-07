using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


#region Project
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.DataCollect;
using UserControl;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Package;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
#endregion


namespace BenQGuru.eMES.Client
{
    public partial class FPackPallet : BaseForm
    {
        #region 变量和属性
        private BenQGuru.eMES.BaseSetting.BaseModelFacade _face = null;

        private PackageFacade _palletFacade = null;

        private const string packCarton = "0";
        private const string packRcard = "1";
        private DataCollectFacade _DataFace = null;
        private Pallet _newPallet = null;
        private string _FunctionName = string.Empty;
        private string m_InputCarton = string.Empty;
        private int _FlowControl = 0;
        private string _FirstInput = string.Empty;

        private string sourceRCard = string.Empty;//产品的原始序列号

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        private DataTable mainGridList = new DataTable();
        private DataTable palletGridList = new DataTable();

        private string m_SelectedStorage;
        // 存放用户选择后的库位
        public string SelectedStorage
        {
            get { return m_SelectedStorage; }
            set { m_SelectedStorage = value; }
        }

        private string m_SelectedStack;

        // 存放用户选择后的垛位
        public string SelectedStack
        {
            get { return m_SelectedStack; }
            set { m_SelectedStack = value; }
        }
        #endregion

        #region 事件
        public FPackPallet()
        {
            InitializeComponent();
            objectCartonRCard.Value = packCarton;
            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(ultraGridPalletForItemList);
            UserControl.UIStyleBuilder.GridUI(ultraGridPallet);
        }

        private void FPackPallet_Load(object sender, EventArgs e)
        {
            this._FunctionName = this.Text;

            this.checkBoxInINV.Checked = false;
            checkBoxInINV_CheckedChanged(sender, e);

            InitializeMainGrid();
            InitializePalletGrid();
            InitializeComboBox();
            InitialBusinessType();

            if (_face == null)
            {
                _face = new BenQGuru.eMES.BaseSetting.BaseModelFacade(DataProvider);
            }
            object obj = this._face.GetResource(ApplicationService.Current().LoginInfo.Resource.ResourceCode);
            this.txtCapacity.Value = "9999";
            this.txtCollected.Value = "0";
            this.txtSscode.Value = ((Resource)obj).StepSequenceCode;

            //this.SetInvCheckBox(((Resource)obj).StepSequenceCode);

            UpdatePalletGrid();
            //this.InitPageLanguage();
        }

        private void InitialBusinessType()
        {
            InventoryFacade objFacade = new InventoryFacade(this.DataProvider);
            object[] invBusinessList = objFacade.GetInvBusiness(BussinessReason.type_produce, BussinessType.type_in);

            this.cboBusinessType.Clear();
            if (invBusinessList != null)
            {
                foreach (InvBusiness obj in invBusinessList)
                {
                    this.cboBusinessType.AddItem(obj.BusinessDescription, obj.BusinessCode);
                }
            }
        }

        private void ultraGridMain_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraWinGridHelper ultraWinGridHelper = new UltraWinGridHelper(this.ultraGridPalletForItemList);
            ultraWinGridHelper.AddCommonColumn("mocode", "工单");
            ultraWinGridHelper.AddCommonColumn("OQCLotNo", "送检批号");
            ultraWinGridHelper.AddCommonColumn("runningcard", "产品序列号");
            ultraWinGridHelper.AddCommonColumn("cartoncode", "箱号");

            e.Layout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            //this.InitGridLanguage(ultraGridPalletForItemList);
        }

        private void ultraGridPallet_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraWinGridHelper ultraWinGridHelper = new UltraWinGridHelper(this.ultraGridPallet);
            ultraWinGridHelper.AddCommonColumn("PalletCode", "栈板号");
            ultraWinGridHelper.AddCommonColumn("Capity", "已装数量");
            ultraWinGridHelper.AddCommonColumn("LotNO", "批号");
            ultraWinGridHelper.AddCommonColumn("ItemCode", "产品");
            e.Layout.Bands[0].Columns["PalletCode"].Width = 120;
            e.Layout.Bands[0].Columns["Capity"].Width = 80;
            e.Layout.Bands[0].Columns["LotNO"].Width = 200;

            e.Layout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            //this.InitGridLanguage(ultraGridPallet);
        }

        private void chkPallet_CheckedChanged(object sender, EventArgs e)
        {
            this.lblItemCodeDescV.Text = string.Empty;
            this.txtPalletNO.Value = "";
            mainGridList.Clear();
            this.txtPalletCollect.Value = "";
            this.txtItemCode.Value = "";
            this.txtPalletNO.Enabled = true;
            this.txtPalletNO.TextFocus(true, true);
        }

        private void txtRCard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string rcard = FormatHelper.CleanString(this.txtSN.Value.Trim().ToUpper());
                if (rcard == "CHANGEPALLET")
                {
                    this.txtPalletNO.TextFocus(true, true);
                    this.lblItemCodeDescV.Text = string.Empty;
                    this.txtPalletNO.Value = "";
                    this.txtItemCode.Value = "";
                    this.mainGridList.Clear();
                    this.txtPalletCollect.Value = "";
                    this.txtPalletNO.Enabled = true;
                    this.chkPrint.Checked = false;
                    this.txtSN.Value = string.Empty;
                    return;
                }

                if (rcard == string.Empty)
                {
                    this.txtSN.TextFocus(false, true);
                }

                ucMessage1.AddWithoutEnter("<<");
                ucMessage1.AddBoldText(txtSN.Value.Trim());

                if (this.checkBoxInINV.Checked)
                {
                    if (!CheckInStorageInfo())
                    {
                        return;
                    }
                }

                if (_FlowControl == 0)
                {
                    _FirstInput = FormatHelper.CleanString(this.txtSN.Value.Trim().ToUpper());

                    switch (objectCartonRCard.CheckedItem.DataValue.ToString())
                    {
                        case packRcard:
                            {
                                //只提出信息但可以继续上栈板
                                CheckRCardFrozen(rcard);

                                if (this.checkBoxInINV.Checked && !CheckRule(rcard))
                                {
                                    return;
                                }

                                if (!this.CheckAppAndProductCode(rcard))
                                {
                                    ucMessage1.AddEx("$CS_Please_Input_RunningCard");
                                    this.txtSN.TextFocus(false, true);
                                    return;
                                }

                                if (!CheckProdcutItemAndISCom(rcard))
                                {
                                    ucMessage1.AddEx("$CS_Please_Input_RunningCard");
                                    this.txtSN.TextFocus(false, true);
                                    return;
                                }

                                //check是否下地
                                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);

                                sourceRCard = dataCollectFacade.GetSourceCard(rcard.Trim().ToUpper(), string.Empty);

                                Messages msg = new Messages();
                                msg.AddMessages(dataCollectFacade.CheckISDown(sourceRCard.ToUpper().Trim()));
                                if (!msg.IsSuccess())
                                {
                                    ucMessage1.AddEx(this._FunctionName, "", msg, false);
                                    ucMessage1.AddEx("$CS_Please_Input_RunningCard");
                                    this.txtSN.TextFocus(false, true);
                                    return;
                                }

                                InventoryFacade objFacade = new InventoryFacade(this.DataProvider);
                                object objStarckToRcard = objFacade.GetStackToRcard(sourceRCard);
                                if (objStarckToRcard != null)
                                {
                                    //序列号重复入库                            
                                    ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$CS_SERIAL_EXIST_Storge $SERIAL_NO=" + rcard), false);
                                    ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), false);
                                    txtSN.TextFocus(false, true);
                                    return;
                                }

                                if (this.checkRcard.Checked)
                                {
                                    ucMessage1.AddEx("$CS_Please_Input_RunningCard_Again");
                                    _FlowControl = 1;
                                    this.txtSN.TextFocus(false, true);
                                    return;
                                }


                                this.DoPackPalleting();
                                _FlowControl = 0;
                                _FirstInput = string.Empty;

                            }
                            break;
                        case packCarton:
                            {
                                DataCollectFacade face = new DataCollectFacade(DataProvider);
                                object[] objSimulationReport = face.QuerySimulationReportByCarton(FormatHelper.CleanString(this.txtSN.Value.ToString().Trim()));
                                if (objSimulationReport != null)
                                {
                                    if (this.checkBoxInINV.Checked && !CheckRule(this.txtSN.Value.ToString().Trim()))
                                    {
                                        this.txtSN.TextFocus(false, true);
                                        return;
                                    }

                                    for (int i = 0; i < objSimulationReport.Length; i++)
                                    {
                                        //只提出信息但可以继续上栈板
                                        CheckRCardFrozen(((SimulationReport)objSimulationReport[i]).RunningCard.Trim().ToUpper());

                                        if (!this.CheckAppAndProductCode(((SimulationReport)objSimulationReport[i]).RunningCard.Trim().ToUpper()))
                                        {
                                            ucMessage1.AddEx("$CS_PLEASE_INPUT_CARTONNO");
                                            this.txtSN.TextFocus(false, true);
                                            return;
                                        }

                                        if (!CheckProdcutItemAndISCom(((SimulationReport)objSimulationReport[i]).RunningCard.Trim().ToUpper()))
                                        {
                                            ucMessage1.AddEx("$CS_PLEASE_INPUT_CARTONNO");
                                            this.txtSN.TextFocus(false, true);
                                            return;
                                        }

                                        //check是否下地
                                        Messages msg = new Messages();
                                        msg.AddMessages(face.CheckISDown(((SimulationReport)objSimulationReport[i]).RunningCard.Trim().ToUpper()));
                                        if (!msg.IsSuccess())
                                        {
                                            ucMessage1.AddEx(this._FunctionName, "", msg, false);
                                            ucMessage1.AddEx("$CS_PLEASE_INPUT_CARTONNO");
                                            this.txtSN.TextFocus(false, true);
                                            return;
                                        }

                                        InventoryFacade objFacade = new InventoryFacade(this.DataProvider);
                                        object objStarckToRcard = objFacade.GetStackToRcard(((SimulationReport)objSimulationReport[i]).RunningCard.Trim().ToUpper(), FormatHelper.CleanString(this.txtSN.Value.ToString().Trim()));
                                        if (objStarckToRcard != null)
                                        {
                                            //序列号重复入库                            
                                            ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$CS_SERIAL_EXIST_Storge $SERIAL_NO=" + ((SimulationReport)objSimulationReport[i]).RunningCard.Trim().ToUpper()), false);
                                            ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"), false);
                                            txtSN.TextFocus(false, true);
                                            return;
                                        }
                                    }

                                    if (this.checkRcard.Checked)
                                    {
                                        ucMessage1.AddEx("$CS_Please_Input_RunningCard_Again");
                                        _FlowControl = 1;
                                        this.txtSN.TextFocus(false, true);
                                        return;
                                    }

                                    this.DoPackPalleting();
                                    _FlowControl = 0;
                                    _FirstInput = string.Empty;
                                }
                                else
                                {
                                    ucMessage1.AddEx(this._FunctionName, this.objectCartonRCard.Items[0].DisplayText + ": " + this.txtSN.Value, new UserControl.Message(MessageType.Error, "$NoProductInfo"), true);
                                    this.txtSN.TextFocus(true, true);
                                }
                            }
                            break;
                    }
                }

                if (_FlowControl == 1)
                {
                    string checkRcard = FormatHelper.CleanString(this.txtSN.Value.Trim().ToUpper());

                    checkRcard = checkRcard.Replace(" ", "");
                    switch (objectCartonRCard.CheckedItem.DataValue.ToString())
                    {
                        case packRcard:
                            string packracrd = _FirstInput.Replace(" ", "");
                            if (string.Compare(checkRcard, packracrd, true) == 0)
                            {
                                this.DoPackPalleting();
                                _FlowControl = 0;
                                _FirstInput = string.Empty;
                            }
                            else
                            {
                                ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$CS_CheckRcard_IsWrong"), false);
                                ucMessage1.AddEx("$CS_Please_Input_RunningCard_Again");
                                this.txtSN.TextFocus(false, true);
                                return;
                            }
                            break;
                        case packCarton:

                            DataCollectFacade dataCollectFacade = new DataCollectFacade(DataProvider);
                            object[] objSimulationReports = dataCollectFacade.QuerySimulationReportByCarton(FormatHelper.CleanString(_FirstInput));
                            if (objSimulationReports != null)
                            {
                                string getRcard = ((SimulationReport)objSimulationReports[0]).RunningCard.Trim().Replace(" ", "");
                                if (string.Compare(checkRcard, getRcard, true) == 0)
                                {
                                    this.DoPackPalleting();
                                    _FlowControl = 0;
                                    _FirstInput = string.Empty;
                                }
                                else
                                {
                                    ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$CS_CheckRcard_IsWrong"), false);
                                    ucMessage1.AddEx("$CS_Please_Input_RunningCard_Again");
                                    this.txtSN.TextFocus(false, true);
                                    return;
                                }
                            }
                            break;
                    }
                }
            }
        }

        private void txtPalletNO_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.txtPalletNO.Value.Trim() == "")
                {
                    this.txtSN.TextFocus(false, true);
                    return;
                }

                if (this.txtPalletNO.Value.Trim().ToUpper() == "CHANGEPALLET")
                {
                    this.txtPalletNO.TextFocus(true, true);
                    this.lblItemCodeDescV.Text = string.Empty;
                    this.txtPalletNO.Value = "";
                    this.txtItemCode.Value = "";
                    this.mainGridList.Clear();
                    this.txtPalletCollect.Value = "";
                    this.txtPalletNO.Enabled = true;
                    this.chkPrint.Checked = false;
                    return;
                }

                if (this.chbPalletLength.Checked && this.txtPalletNO.Value.Trim().Length != int.Parse(this.txtLength.Value.ToString().Trim()))
                {
                    ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$CS_PalletCode_Length_Is_Wrong"), false);
                    this.txtPalletNO.TextFocus(false, true);
                    return;
                }

                Package.PackageFacade pf = new BenQGuru.eMES.Package.PackageFacade(DataProvider);
                object obj = pf.GetPallet(FormatHelper.CleanString(this.txtPalletNO.Value.Trim().ToString()));
                if (obj != null)
                {
                    this.txtItemCode.Value = ((Domain.Package.Pallet)obj).ItemCode;
                    ItemFacade itemface = new ItemFacade(DataProvider);
                    object objItem = itemface.GetItem(this.txtItemCode.Value, GlobalVariables.CurrentOrganizations.First().OrganizationID);

                    if (objItem != null)
                    {
                        if (((Pallet)obj).RCardCount.ToString() == FormatHelper.FALSE_STRING)
                        {
                            this.lblItemCodeDescV.Text = " ";
                        }
                        else
                        {
                            this.lblItemCodeDescV.Text = ((Item)objItem).ItemDescription.ToString();
                        }
                    }
                    else
                    {
                        this.lblItemCodeDescV.Text = " ";
                    }

                    this.UpdateMainGrid();


                    this.txtPalletCollect.Value = ((Domain.Package.Pallet)obj).RCardCount.ToString().Trim();
                    if (objectCartonRCard.Value == packCarton)
                    {
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"), false);
                    }
                    if (objectCartonRCard.Value == packRcard)
                    {
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), false);
                    }
                    this.txtSN.TextFocus(false, true);
                }
                else
                {
                    if (objectCartonRCard.Value.ToString() == packCarton)
                    {
                        this.txtItemCode.Value = "";
                        this.mainGridList.Clear();
                        this.txtPalletCollect.Value = "";
                        this.lblItemCodeDescV.Text = string.Empty;
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"), false);
                    }
                    if (objectCartonRCard.Value.ToString() == packRcard)
                    {
                        this.txtItemCode.Value = "";
                        this.mainGridList.Clear();
                        this.txtPalletCollect.Value = "";
                        this.lblItemCodeDescV.Text = string.Empty;
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), false);
                    }
                    this.txtSN.TextFocus(false, true);
                }

                this.UpdatePalletGrid();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.txtPalletNO.Value.Trim() == "")
            {
                ucMessage1.AddEx(this._FunctionName, this.txtPalletNO.Caption + ": " + this.txtPalletNO.Value, new UserControl.Message(MessageType.Error, "$CS_PLEASE_INPUT_PALLETNO"), true);
            }
            else
            {
                ShowPalletPrintForm(this.txtPalletNO.Value.Trim(), false);
                this.UpdateMainGrid();
                mainGridList.Clear();
                this.lblItemCodeDescV.Text = string.Empty;
                this.txtPalletCollect.Value = "";
                this.txtItemCode.Value = "";
                this.txtPalletNO.TextFocus(false, true);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtPalletNO.TextFocus(true, true);
            this.lblItemCodeDescV.Text = string.Empty;
            this.txtPalletNO.Value = "";
            this.txtItemCode.Value = "";
            this.mainGridList.Clear();
            palletGridList.Clear();
            this.txtPalletCollect.Value = "";
            this.txtPalletNO.Enabled = true;
            this.chkPrint.Checked = false;
            _newPallet = null;
            _FlowControl = 0;
            _FirstInput = string.Empty;
        }

        private void opsetPackObject_ValueChanged(object sender, EventArgs e)
        {
            if (objectCartonRCard.CheckedItem.DataValue.ToString() == packCarton)
            {
                ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"), false);
                this.txtSN.TextFocus(false, true);
            }
            if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
            {
                ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), false);
                this.txtSN.TextFocus(false, true);
            }

            _FlowControl = 0;
            _FirstInput = string.Empty;
            _newPallet = new Pallet();
        }

        private void ucMessage1_WorkingErrorAdded(object sender, WorkingErrorAddedEventArgs e)
        {
            CSHelper.ucMessageWorkingErrorAdded(e, this.DataProvider);
        }

        private void checkBoxInINV_CheckedChanged(object sender, EventArgs e)
        {
            this.ucLabelComboxCompany.SelectedIndex = -1;
            this.ucLabelComboxINVType.SelectedIndex = -1;
            this.ucLabelEditStock.Value = "";            

            if (checkBoxInINV.Checked)
            {
                this.ucLabelComboxCompany.Enabled = true;
                this.ucLabelComboxINVType.Enabled = true;                
                this.btnGetStack.Enabled = true;
                this.cboBusinessType.Enabled = true;
            }
            else
            {
                this.ucLabelComboxCompany.Enabled = false;
                this.ucLabelComboxINVType.Enabled = false;                
                this.btnGetStack.Enabled = false;
                this.cboBusinessType.Enabled = false;
            }
        }

        private void btnGetStack_Click(object sender, EventArgs e)
        {
            if (this.ucLabelComboxINVType.SelectedItemText.Trim().Length == 0)
            {
                ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$CS_STORAGE_NOT_INPUT"), true);
                return;
            }

            FStackInfo objForm = new FStackInfo();
            objForm.StackInfoEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<System.Collections.Hashtable>>(objForm_StackInfoEvent);
            objForm.StorageCode = this.ucLabelComboxINVType.SelectedItemValue.ToString();
            objForm.StackCode = this.ucLabelEditStock.Value.ToString();
            objForm.ShowDialog();
        }

        void objForm_StackInfoEvent(object sender, ParentChildRelateEventArgs<System.Collections.Hashtable> e)
        {
            this.SelectedStack = e.CustomObject["stackcode"].ToString();
            this.SelectedStorage = e.CustomObject["storagecode"].ToString();

            //Check从垛位使用状况的页面带过来的垛位和库位
            if (this.CheckSelecetedStackAndStorage(this.SelectedStorage, this.SelectedStack, Convert.ToString(this.ucLabelComboxINVType.SelectedItemValue)))
            {
                this.ucLabelEditStock.Value = e.CustomObject["stackcode"].ToString();
            }

            InventoryFacade inventoryFacade = new InventoryFacade(DataProvider);
            //更新剁位状态
            if (this.ucLabelEditStock.Value.Trim().Length > 0)
            {
                object objectStackMessage = inventoryFacade.GetStackMessage(this.ucLabelEditStock.Value.Trim().ToUpper());
                if (objectStackMessage != null)
                {
                    this.ucLabelEditstackMessage.Value = ((StackMessage)objectStackMessage).StackQtyMessage;
                }
                else
                {
                    SStack objectStack = (SStack)inventoryFacade.GetSStack(this.ucLabelEditStock.Value.Trim().ToUpper());
                    if (objectStack != null)
                    {
                        this.ucLabelEditstackMessage.Value = "0" + "/" + objectStack.Capacity;
                    }
                    else
                    {
                        this.ucLabelEditstackMessage.Value = string.Empty;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FInInvByProduce objForm = new FInInvByProduce();
            objForm.Show();
        }

        #endregion

        #region 自定义事件

        private void SetInvCheckBox(string ssCode)
        {
            if (_face == null)
            {
                _face = new BenQGuru.eMES.BaseSetting.BaseModelFacade(DataProvider);
            }
            object obj = this._face.GetStepSequence(ssCode);

            if (obj != null)
            {
                if (((StepSequence)obj).SaveInStock.ToLower().Equals("y"))
                {
                    this.checkBoxInINV.Checked = true;
                }
                else
                {
                    this.checkBoxInINV.Checked = false;
                }
            }
            else
            {
                this.checkBoxInINV.Checked = false;
            }

            this.checkBoxInINV.Enabled = false;
        }

        private void InitializeComboBox()
        {            
            this.InitializeStorageCode();
            this.InitializeCompany();
        }

        private void InitializeCompany()
        {
            SystemSettingFacade systemFacade = new SystemSettingFacade(this.DataProvider);
            object[] objList = systemFacade.GetParametersByParameterGroup("COMPANYLIST");
            if (objList != null)
            {
                this.ucLabelComboxCompany.Clear();
                foreach (Parameter para in objList)
                {
                    this.ucLabelComboxCompany.AddItem(para.ParameterDescription, para.ParameterAlias);
                }
            }
        }

        private void InitializeStorageCode()
        {
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            object[] storageList = inventoryFacade.GetAllStorage();

            if (storageList != null)
            {
                this.ucLabelComboxINVType.Clear();
                foreach (Storage storage in storageList)
                {
                    this.ucLabelComboxINVType.AddItem(storage.StorageName, storage.StorageCode);
                }
            }
        }

        private void InitializePalletGrid()
        {
            palletGridList.Columns.Clear();
            palletGridList.Columns.Add("PalletCode", typeof(string)).ReadOnly = true;
            palletGridList.Columns.Add("Capity", typeof(string)).ReadOnly = true;
            palletGridList.Columns.Add("LotNO", typeof(string)).ReadOnly = true;
            palletGridList.Columns.Add("ItemCode", typeof(string)).ReadOnly = true;
            this.ultraGridPallet.DataSource = palletGridList;
        }

        private void InitializeMainGrid()
        {
            mainGridList.Columns.Clear();
            mainGridList.Columns.Add("mocode", typeof(string)).ReadOnly = true;
            mainGridList.Columns.Add("OQCLotNo", typeof(string)).ReadOnly = true;
            mainGridList.Columns.Add("runningcard", typeof(string)).ReadOnly = true;
            mainGridList.Columns.Add("cartoncode", typeof(string)).ReadOnly = true;
            this.ultraGridPalletForItemList.DataSource = mainGridList;
        }

        //GET SimulationReport
        private object GetLastSimulationReport(string rcard)
        {
            if (_DataFace == null)
            {
                _DataFace = new BenQGuru.eMES.DataCollect.DataCollectFacade(DataProvider);
            }
            return this._DataFace.GetLastSimulationReport(rcard);
        }

        //GET产线
        private string GetSsCode(string rcard)
        {
            if (_face == null)
            {
                _face = new BenQGuru.eMES.BaseSetting.BaseModelFacade(DataProvider);
            }
            object obj = this._face.GetResource(ApplicationService.Current().LoginInfo.Resource.ResourceCode);
            return ((Resource)obj).StepSequenceCode.ToString().Trim();

        }

        //更新PALLET
        private Pallet updatePallet(string NowRcard)
        {
            Package.PackageFacade pf = new BenQGuru.eMES.Package.PackageFacade(DataProvider);

            //Add By Bernard @ 2010-11-03
            DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
            sourceRCard = dcf.GetSourceCard(NowRcard.Trim().ToUpper(), string.Empty);
            object objSimulationReport = null;

            DBDateTime dbDateTime;
            dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            _newPallet.RCardCount = _newPallet.RCardCount + 1;
            this.txtPalletCollect.Value = _newPallet.RCardCount.ToString();

            if (_newPallet.RCardCount == 1)
            {
                _newPallet.Capacity = int.Parse(this.txtCapacity.Value);

                //Add By Bernard @ 2010-11-03
                if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
                {
                    objSimulationReport = this.GetLastSimulationReport(sourceRCard);
                }
                else
                {
                    objSimulationReport = this.GetLastSimulationReport(NowRcard);
                }

                if (objSimulationReport != null)
                {
                    _newPallet.ItemCode = ((SimulationReport)objSimulationReport).ItemCode.ToString().Trim();
                }
                _newPallet.Rescode = ApplicationService.Current().ResourceCode;

                string lotNo = string.Empty;
                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
                OQCLot2Card oqcLot2Card = new OQCLot2Card();

                 //Add By Bernard @ 2010-11-03
                if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
                {
                    oqcLot2Card = (OQCLot2Card)oqcFacade.GetLastOQCLot2CardByRCard(sourceRCard);
                }
                else
                {
                    oqcLot2Card = (OQCLot2Card)oqcFacade.GetLastOQCLot2CardByRCard(NowRcard);
                }

                if (oqcLot2Card != null)
                {
                    _newPallet.EAttribute1 = oqcLot2Card.LOTNO;
                }

                if (_newPallet.EAttribute1 == string.Empty)
                {
                    _newPallet.EAttribute1 = " ";
                }
            }
            _newPallet.MaintainDate = dbDateTime.DBDate;
            _newPallet.MaintainTime = dbDateTime.DBTime;
            _newPallet.MaintainUser = ApplicationService.Current().UserCode;
            return _newPallet;
        }

        //增加APLLET2RCARD
        private Pallet2RCard AddPalletRcard(string NowRcard)
        {
            //Add By Bernard @ 2010-11-03
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            sourceRCard = dataCollectFacade.GetSourceCard(NowRcard.Trim().ToUpper(), string.Empty);

            Package.PackageFacade pf = new BenQGuru.eMES.Package.PackageFacade(DataProvider);
            Domain.Package.Pallet2RCard newPalletRcard = new BenQGuru.eMES.Package.PackageFacade(DataProvider).CreateNewPallet2RCard();
            DBDateTime dbDateTime;
            dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            object objSimulationReport = null;
            //Add By Bernard @ 2010-11-03
            if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
            {
                objSimulationReport = this.GetLastSimulationReport(sourceRCard);
            }
            else
            {
                objSimulationReport = this.GetLastSimulationReport(NowRcard);
            }
            
            if (objSimulationReport != null)
            {
                newPalletRcard.MOCode = ((SimulationReport)objSimulationReport).MOCode.ToString().Trim();
            }
            newPalletRcard.PalletCode = this.txtPalletNO.Value.Trim().ToString();
            if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
            {
                newPalletRcard.RCard = sourceRCard;
            }
            else
            {
                newPalletRcard.RCard = NowRcard;
            }
            newPalletRcard.MaintainDate = dbDateTime.DBDate;
            newPalletRcard.MaintainTime = dbDateTime.DBTime;
            newPalletRcard.MaintainUser = ApplicationService.Current().UserCode;
            newPalletRcard.EAttribute1 = " ";
            return newPalletRcard;
        }

        //绑定MainGrid数据
        private void BindMainGrid(object[] objs)
        {
            mainGridList.Clear();
            foreach (SimulationReport sim in objs)
            {
                mainGridList.Rows.Add(new object[]{
                                                   sim.MOCode,
												   sim.LOTNO
												  ,sim.RunningCard	
												  ,sim.CartonCode
											  });
            }
            mainGridList.AcceptChanges();
        }

        //更新MainGrid查询数据
        private void UpdateMainGrid()
        {
            PackageFacade pf = new BenQGuru.eMES.Package.PackageFacade(DataProvider);
            InventoryFacade inventoryFacade = new InventoryFacade(DataProvider);
            object[] obj = pf.GetPalletDetailInfo(this.txtPalletNO.Value.ToString().Trim());
            if (obj != null)
            {
                BindMainGrid(obj);
            }
            else
            {
                mainGridList.Clear();
            }
            //更新剁位状态
            if (this.ucLabelEditStock.Value.Trim().Length > 0)
            {
                object objectStackMessage = inventoryFacade.GetStackMessage(this.ucLabelEditStock.Value.Trim().ToUpper());
                if (objectStackMessage != null)
                {
                    this.ucLabelEditstackMessage.Value = ((StackMessage)objectStackMessage).StackQtyMessage;
                }
            }
        }

        //绑定PalletGrid数据
        private void BindPalletGrid(object[] objs)
        {
            palletGridList.Clear();
            foreach (Pallet pallet in objs)
            {
                palletGridList.Rows.Add(new object[]{
                                                   pallet.PalletCode,
												   pallet.RCardCount,
                                                   pallet.EAttribute1,	
												   pallet.ItemCode
											  });
            }
            palletGridList.AcceptChanges();
        }

        private void UpdatePalletGrid()
        {
            PackageFacade packageFacade = new BenQGuru.eMES.Package.PackageFacade(DataProvider);
            object[] pallletList = packageFacade.QueryPallet(ApplicationService.Current().ResourceCode);

            if (pallletList != null)
            {
                BindPalletGrid(pallletList);
            }
            else
            {
                palletGridList.Clear();
            }
        }

        private void ShowPalletPrintForm(string palletCode, bool autoPrint)
        {
            if (_palletFacade == null)
            {
                _palletFacade = new PackageFacade(this.DataProvider);
            }

            Pallet pallet = (Pallet)_palletFacade.GetPallet(FormatHelper.CleanString(palletCode));
            if (pallet == null)
            {
                ucMessage1.AddEx(this._FunctionName, this.txtPalletNO.Caption + ": " + this.txtPalletNO.Value, new UserControl.Message(MessageType.Error, "$CS_PLEASE_INPUT_PALLETNO"), true);
            }
            else
            {
                FPackPalletPrint formPrint = new FPackPalletPrint();
                formPrint.SetData(pallet.PalletCode, autoPrint);
                formPrint.MdiParent = this.MdiParent;
                formPrint.WindowState = FormWindowState.Maximized;
                formPrint.Show();
            }
        }

        private bool IsPalletFull(string palletCode)
        {
            if (_palletFacade == null)
            {
                _palletFacade = new PackageFacade(this.DataProvider);
            }

            Pallet pallet = (Pallet)_palletFacade.GetPallet(FormatHelper.CleanString(palletCode));
            return pallet.RCardCount >= pallet.Capacity;
        }

        //检查此RCard是否被隔离
        private bool CheckRCardFrozen(string rCard)
        {
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);

            DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
            sourceRCard = dcf.GetSourceCard(rCard.Trim().ToUpper(), string.Empty);
            object[] frozen = null;

            if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
            {
                frozen = oqcFacade.QueryFrozen(sourceRCard, sourceRCard, string.Empty, string.Empty, string.Empty, FrozenStatus.STATUS_FRONZEN, -1, -1, -1, -1, int.MinValue, int.MaxValue);
            }
            else
            {
                frozen = oqcFacade.QueryFrozen(rCard, rCard, string.Empty, string.Empty, string.Empty, FrozenStatus.STATUS_FRONZEN, -1, -1, -1, -1, int.MinValue, int.MaxValue);

            }
            if (frozen != null && frozen.Length > 0)
            {
                ucMessage1.AddEx(this._FunctionName, this.objectCartonRCard.Items[1].DisplayText + ": " + rCard, new UserControl.Message(MessageType.Normal, "$Message_RCardIsFrozen $RCard = " + rCard), true);
                return false;
            }
            return true;
        }

        //检查业务类型
        private bool CheckRule(string inputNo)
        {
            if (this.cboBusinessType.SelectedItemText.Trim().Length == 0)
            {
                return true;
            }

            InventoryFacade invFacade = new InventoryFacade(this.DataProvider);

            DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
            sourceRCard = dcf.GetSourceCard(inputNo.Trim().ToUpper(), string.Empty);
            bool m_IsFinished = false;

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

                    if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
                    {
                        m_IsFinished = invFacade.CheckRcardIsFinished(sourceRCard);
                    }
                    else
                    {
                        m_IsFinished = invFacade.CheckRcardIsFinished(inputNo);
                    }
                    if (!m_IsFinished)
                    {
                        //Message:产品序列号未完工
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$CS_RCARD_IS_NOT_FINISHED $CS_Param_RunSeq=" + inputNo), true);
                        return false;
                    }
                }
            }

            return true;
        }

        // 检查是否要 比对商品码 比对附件 add by hiro 08/09/27
        private bool CheckAppAndProductCode(string rcard)
        {
            DataCollectFacade _face = new DataCollectFacade(DataProvider);

            sourceRCard = _face.GetSourceCard(rcard.Trim().ToUpper(), string.Empty);

            object objSimulationReport = null;
            if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
            {
                objSimulationReport = _face.GetLastSimulationReport(sourceRCard);
            }
            else
            {
                objSimulationReport = _face.GetLastSimulationReport(rcard);
            }
            if (objSimulationReport != null)
            {
                string strItemCode = ((SimulationReport)objSimulationReport).ItemCode.Trim();
                ItemFacade _facadeItem = new ItemFacade(DataProvider);
                object objItem = _facadeItem.GetItem(strItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                if (objItem != null)
                {
                    PackageFacade _facadePacking = new PackageFacade(DataProvider);
                    object objPackingCheck = null;
                    
                    if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
                    {
                        objPackingCheck = _facadePacking.GetPACKINGCHK(sourceRCard);
                    }
                    else
                    {
                        objPackingCheck = _facadePacking.GetPACKINGCHK(rcard);
                    }

                    if (((Item)objItem).ItemProductCode != null && ((Item)objItem).ItemProductCode.ToString().Trim() != string.Empty)
                    {
                        if (objPackingCheck == null)
                        {
                            ucMessage1.AddEx(this._FunctionName, this.objectCartonRCard.Items[1].DisplayText + ": " + rcard, new UserControl.Message(MessageType.Error, "$First_TO_CheckProcudctCode  $RCard = " + rcard), true);
                            return false;
                        }
                        else
                        {
                            if (((PACKINGCHK)objPackingCheck).CheckProductCode != FormatHelper.TRUE_STRING)
                            {
                                ucMessage1.AddEx(this._FunctionName, this.objectCartonRCard.Items[1].DisplayText + ": " + rcard, new UserControl.Message(MessageType.Error, "$First_TO_CheckProcudctCode $RCard = " + rcard), true);
                                return false;
                            }
                        }
                    }
                    if (((Item)objItem).NeedCheckAccessory == FormatHelper.TRUE_STRING)
                    {
                        if (objPackingCheck == null)
                        {
                            ucMessage1.AddEx(this._FunctionName, this.objectCartonRCard.Items[1].DisplayText + ": " + rcard, new UserControl.Message(MessageType.Error, "$First_TO_CheckAccessory  $RCard = " + rcard), true);
                            return false;
                        }
                        else
                        {
                            if (((PACKINGCHK)objPackingCheck).CheckAccessory != FormatHelper.TRUE_STRING)
                            {
                                ucMessage1.AddEx(this._FunctionName, this.objectCartonRCard.Items[1].DisplayText + ": " + rcard, new UserControl.Message(MessageType.Error, "$First_TO_CheckAccessory $RCard = " + rcard), true);
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            return true;
        }

        // 检查入库讯息是否存在
        private bool CheckInStorageInfo()
        {
            if (this.ucLabelComboxINVType.SelectedItemText.Trim().Length == 0 ||
                this.ucLabelComboxCompany.SelectedItemText.Trim().Length == 0 ||
                this.ucLabelEditStock.Value.Trim().Length == 0 ||
                this.cboBusinessType.SelectedItemText.Trim().Length == 0
                )
            {
                //Message:入库讯息设定不全
                ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$CS_IN_STORAGE_INFO_ERROR"), true);
                return false;
            }

            return true;
        }

        // Check 从垛位使用状况的页面带过来的垛位和库位
        private bool CheckSelecetedStackAndStorage(string selectedStorage, string selectedStack, string originalStorage)
        {
            if (originalStorage.Trim().Length == 0)
            {
                //请输入库位
                ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$CS_STORAGE_NOT_INPUT"), true);
                return false;
            }

            if (!selectedStorage.Equals(originalStorage))
            {
                InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
                int stack2RCardCount = inventoryFacade.GetStack2RCardCount(selectedStack, originalStorage);

                if (stack2RCardCount > 0)
                {
                    //垛位和库别不对应
                    ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error, "$CS_STACK_STORAGE_NOT_SAME"), true);
                    return false;
                }
                else
                {
                    //垛位和库别不对应,确定使用该垛位?
                    if (MessageBox.Show(UserControl.MutiLanguages.ParserMessage("$CS_STACK_STORAGE_NOT_SAME_CONFIRM"), MutiLanguages.ParserString("$ShowConfirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        // 成品入库检查
        private bool CheckStackAndRcardInfo(string rcard, string stackCode, string itemCode, string palletCode, string companyCode, string storageCode)
        {

            ////Check从垛位使用状况的页面带过来的垛位和库位
            if (!this.CheckSelecetedStackAndStorage(this.SelectedStorage, this.SelectedStack, Convert.ToString(this.ucLabelComboxINVType.SelectedItemValue)))
            {
                return false;
            }
            else
            {
                DataCollectFacade dataCollectFacade = new DataCollectFacade(DataProvider);
                sourceRCard = dataCollectFacade.GetSourceCard(rcard, string.Empty);
                object objStarckToRcard = null;

                //Check序列号对应的料号是否和垛位的不一样
                InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
                object[] objStackToRcards = inventoryFacade.GetAnyStack2RCardByStack(stackCode);

                if (objStackToRcards != null)
                {
                    if (inventoryFacade.CheckStackIsOnlyAllowOneItem(stackCode) && !((StackToRcard)objStackToRcards[0]).ItemCode.Equals(itemCode))
                    {
                        //垛位的料号和当前产品的料号不一至
                        ucMessage1.AddEx(this._FunctionName, this.objectCartonRCard.CheckedItem.DisplayText + ": " + this.txtSN.Value + ";料号:" + this.txtItemCode.Value, new UserControl.Message(MessageType.Error, "$CS_STACK_AND_PRODUCT_ITEM_NOT_SAME"), true);
                        return false;
                    }
                }

                //Check 序列号是否已经入过库
                if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
                {
                    objStarckToRcard = inventoryFacade.GetStackToRcard(sourceRCard);
                }
                else
                {
                    objStarckToRcard = inventoryFacade.GetStackToRcard(rcard);
                }

                if (objStarckToRcard != null)
                {
                    //序列号重复入库
                    ucMessage1.AddEx(this._FunctionName, "$CS_RCARD" + ": " + this.txtSN.Value, new UserControl.Message(MessageType.Error, "$CS_SERIAL_EXIST"), true);
                    return false;
                }

                //Check栈板(如果入栈板,而且栈板在系统中已经存在)的公司别,产品别和产品等级是否和当前的一致
                if (palletCode.Length > 0)
                {
                    object[] objStackToRcardList = inventoryFacade.GetStackToRcardInfoByPallet(palletCode);
                    if (objStackToRcardList != null)
                    {
                        string strErrorMessage = string.Empty;
                        StackToRcard obj = (StackToRcard)objStackToRcardList[0];
                        if (obj.ItemCode != itemCode)
                        {
                            strErrorMessage = strErrorMessage + "itemcode";
                            ApplicationRun.GetInfoForm().Add(
                            new UserControl.Message(MessageType.Error, "$CS_ITEM_NOT_SAME_IN_PALLET=" + obj.ItemCode));
                        }

                        if (obj.Company != companyCode)
                        {
                            strErrorMessage = strErrorMessage + "companycode";
                            ApplicationRun.GetInfoForm().Add(
                            new UserControl.Message(MessageType.Error, "$CS_COMPANY_NOT_SAME_IN_PALLET=" + obj.Company));
                        }
                                                
                        if (strErrorMessage != string.Empty)
                        {
                            return false;
                        }


                    }
                    else
                    {
                        if (!inventoryFacade.CheckStackCapacity(storageCode, stackCode))
                        {
                            ApplicationRun.GetInfoForm().Add(
                            new UserControl.Message(MessageType.Error, "$CS_STACK_CAPACITY_FULL"));
                            return false;
                        }
                    }
                }

            }


            return true;
        }

        // 检查一个栈板不能存在于两个垛位中
        private bool CheckPalletStack(string palletCode, string stackCode)
        {
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            object[] stackToRcardList = inventoryFacade.GetRcardToStackPallet("", palletCode, "");

            if (stackToRcardList != null)
            {
                if (!((RcardToStackPallet)stackToRcardList[0]).StackCode.Equals(stackCode))
                {
                    return false;
                }
            }

            return true;
        }


        // 检查一个栈板是否已经存在于垛位上
        private bool CheckPalletInStack(string palletCode)
        {
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            object[] stackToRcardList = inventoryFacade.GetRcardToStackPallet("", palletCode, "");

            if (stackToRcardList != null)
            {
                return true;
            }

            return false;

        }

        //检查产品序列号
        private bool CheckProdcutItemAndISCom(string rCard)
        {
            if (_DataFace == null)
            {
                _DataFace = new DataCollectFacade(this.DataProvider);
            }

            sourceRCard = _DataFace.GetSourceCard(rCard.Trim().ToUpper(), string.Empty);
            object objSimulationReport = null;

            if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
            {
                objSimulationReport = _DataFace.GetLastSimulationReport(sourceRCard);
            }
            else
            {
                objSimulationReport = _DataFace.GetLastSimulationReport(rCard);
            }
            if (objSimulationReport == null)
            {
                return true;
            }
            string itemCode = ((SimulationReport)objSimulationReport).ItemCode.Trim();
            ItemFacade _itemface = new ItemFacade(this.DataProvider);
            object objItem = _itemface.GetItem(itemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
            if (objItem == null)
            {
                return true;
            }
            //Marked By Nettie Chen 2009/09/22
            //if (((Item)objItem).ItemType == ItemType.ITEMTYPE_SEMIMANUFACTURE)
            //{
            //    if (((SimulationReport)objSimulationReport).IsComplete != FormatHelper.TRUE_STRING)
            //    {
            //        ucMessage1.AddEx(this._FunctionName, this.opsetPackObject.Items[1].DisplayText + ": " + rCard, new UserControl.Message(MessageType.Error, "$rcard_is_not_complete $RCard = " + rCard), true);
            //        return false;
            //    }
            //}
            //End Marked
            return true;
        }

        private void AddPallet2Rcard(Object objPallet, Object objPalletRcrad, string strRcard)
        {
            Package.PackageFacade pf = new BenQGuru.eMES.Package.PackageFacade(DataProvider);
            int intRcardCount = ((Domain.Package.Pallet)objPallet).RCardCount;
            int intCapacity = ((Domain.Package.Pallet)objPallet).Capacity;
            string NowRcard = string.Empty;
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);

            //GetLot 
            string lotNo = string.Empty;
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            //OQCLot2Card oqcLot2Card = (OQCLot2Card)oqcFacade.GetLastOQCLot2CardByRCard(NowRcard);
            OQCLot2Card oqcLot2Card = new OQCLot2Card();

            if (objectCartonRCard.CheckedItem.DataValue.ToString() == packCarton)
            {
                NowRcard = strRcard;
                oqcLot2Card = (OQCLot2Card)oqcFacade.GetLastOQCLot2CardByRCard(NowRcard);
            }
            if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
            {
                NowRcard = FormatHelper.CleanString(_FirstInput);
                sourceRCard = dataCollectFacade.GetSourceCard(NowRcard.Trim().ToUpper(), string.Empty);
                oqcLot2Card = (OQCLot2Card)oqcFacade.GetLastOQCLot2CardByRCard(sourceRCard);
            }


            if (oqcLot2Card != null)
            {
                lotNo = oqcLot2Card.LOTNO;
            }

            //栈板与产品无关系 pallet2rcard新增加数据
            if (objPalletRcrad == null)
            {
                //检查数量
                if (intCapacity == 9999 && intCapacity <= intRcardCount)
                {
                    ucMessage1.AddEx(this._FunctionName, this.txtPalletNO.Caption + ": " + this.txtPalletNO.Value, new UserControl.Message(MessageType.Error,
                                    "$PALLET_IS_FULL"), true);
                    if (objectCartonRCard.CheckedItem.DataValue.ToString() == packCarton)
                    {
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"), false);
                    }
                    else
                    {
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), false);
                    }

                    this.txtSN.TextFocus(true, true);
                    return;
                }

                //判断产品序列号信息
                object objSimulationReport = null;
                if (objectCartonRCard.CheckedItem.DataValue.ToString() == packCarton)
                {
                    objSimulationReport = this.GetLastSimulationReport(NowRcard);
                }
                else if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
                {
                    objSimulationReport = this.GetLastSimulationReport(sourceRCard);
                }


                if (objSimulationReport == null)
                {
                    ucMessage1.AddEx(this._FunctionName, this.objectCartonRCard.Items[1].DisplayText + ": " + NowRcard, new UserControl.Message(MessageType.Error
                                            , "$Error_ProductInfo_IS_Null_or_Productstatus_IS_wrong" + " $RCard = " + NowRcard), true);
                    if (objectCartonRCard.CheckedItem.DataValue.ToString() == packCarton)
                    {
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"), true);
                    }
                    else
                    {
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), true);
                    }

                    this.txtSN.TextFocus(true, true);
                    return;
                }

                string SScode = string.Empty;
                string resourceCode = ((SimulationReport)objSimulationReport).ResourceCode.ToString().Trim();
                BaseModelFacade _mf = new BenQGuru.eMES.BaseSetting.BaseModelFacade(DataProvider);
                object objRes = _mf.GetResource(resourceCode);
                if (objRes != null)
                {
                    SScode = ((Resource)objRes).StepSequenceCode.Trim().ToString();
                }
                string strItemCode = ((SimulationReport)objSimulationReport).ItemCode.ToString().Trim();
                _newPallet = (Pallet)objPallet;
                if (_newPallet.RCardCount == 0)
                {
                    this.txtItemCode.Value = ((SimulationReport)objSimulationReport).ItemCode.ToString().Trim();
                    _newPallet.EAttribute1 = lotNo.Trim().ToUpper();
                }

                //判断栈板料号与产品序列号料号是否一致
                if (strItemCode != this.txtItemCode.Value)
                {
                    ucMessage1.AddEx(this._FunctionName, this.objectCartonRCard.Items[1].DisplayText + ": " + _FirstInput, new UserControl.Message(MessageType.Error,
                           "$Itemcode_IsDifferent"), true);
                    ucMessage1.AddEx(this._FunctionName, this.txtPalletNO.Caption + ": " + this.txtPalletNO.Value, new UserControl.Message(MessageType.Error
                                  , "$CS_PALLETON_ItemCode =" + this.txtItemCode.Value.Trim()), true);
                    ucMessage1.AddEx(this._FunctionName, this.objectCartonRCard.Items[1].DisplayText + ": " + _FirstInput, new UserControl.Message(MessageType.Error
                                 , "$CS_RCARD_ItemCode =" + strItemCode), true);
                    if (objectCartonRCard.CheckedItem.DataValue.ToString() == packCarton)
                    {
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"), false);
                    }
                    else
                    {
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), false);
                    }
                    this.txtSN.TextFocus(true, true);
                    return;
                }

                //根据系统参数设定，判断栈板批号是否一致
                SystemSettingFacade systemSettingFacade = new SystemSettingFacade(DataProvider);
                Parameter paraSystem = (Parameter)systemSettingFacade.GetParameter("ISCHECKPALLETLOT", "CHECKPALLETLOT");
                if (paraSystem != null && paraSystem.ParameterAlias.ToUpper() == "Y"
                    && _newPallet.Capacity > 0
                    && _newPallet.EAttribute1.Trim().ToUpper() != lotNo.Trim().ToUpper())
                {
                    ucMessage1.AddEx(this._FunctionName, this.objectCartonRCard.Items[1].DisplayText + ": " + _FirstInput, new UserControl.Message(MessageType.Error,
                           "$Itemcode_INLOtNO:" + lotNo + "$Pallet_INLOtNO:" + ((Pallet)objPallet).EAttribute1 + "$Differnet"), true);
                    if (objectCartonRCard.CheckedItem.DataValue.ToString() == packCarton)
                    {
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"), false);
                    }
                    else
                    {
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), false);
                    }
                    this.txtSN.TextFocus(true, true);
                    return;
                }


                //保存pallet,pallet2racd,StackToRcard,invInTransaction等信息
                if (this.checkBoxInINV.Checked)
                {
                    if (!CheckPalletStack(this.txtPalletNO.Value.Trim(), this.ucLabelEditStock.Value.Trim()))
                    {
                        //Message:一个栈板不能存在于不同垛位中
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error,
                                              "$CS_PALLET_IN_ONLY_STACK"), false);
                        this.txtSN.TextFocus(true, true);
                        return;
                    }

                    if (!CheckStackAndRcardInfo(NowRcard, this.ucLabelEditStock.Value.Trim(), strItemCode, this.txtPalletNO.Value.Trim(), this.ucLabelComboxCompany.SelectedItemValue.ToString(), this.ucLabelComboxINVType.SelectedItemValue.ToString()))
                    {
                        this.txtSN.TextFocus(true, true);
                        return;
                    }

                    StackToRcard stackToRcard = new StackToRcard();
                    stackToRcard.StorageCode = this.ucLabelComboxINVType.SelectedItemValue.ToString().Trim();
                    stackToRcard.StackCode = this.ucLabelEditStock.Value.Trim();

                    if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
                    {
                        stackToRcard.SerialNo = sourceRCard;
                    }
                    else
                    {
                        stackToRcard.SerialNo = NowRcard;
                    }
                    stackToRcard.ItemCode = strItemCode;
                    stackToRcard.BusinessReason = BussinessReason.type_produce.ToString();
                    stackToRcard.Company = this.ucLabelComboxCompany.SelectedItemValue.ToString().Trim();
                    
                    stackToRcard.InUser = ApplicationService.Current().LoginInfo.UserCode;
                    stackToRcard.InDate = dbDateTime.DBDate;
                    stackToRcard.InTime = dbDateTime.DBTime;
                    stackToRcard.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;
                    stackToRcard.MaintainDate = dbDateTime.DBDate;
                    stackToRcard.MaintainTime = dbDateTime.DBTime;


                    ////Save In Transaction info
                    InvInTransaction invInTransaction = new InvInTransaction();
                    invInTransaction.TransCode = "";

                    if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
                    {
                        invInTransaction.Rcard = sourceRCard;
                    }
                    else
                    {
                        invInTransaction.Rcard = NowRcard;
                    }

                    if (objectCartonRCard.CheckedItem.DataValue.ToString() == packCarton)
                    {
                        if (this.txtSN.Value.Trim().Length > 0)
                        {
                            this.m_InputCarton = _FirstInput;
                        }

                        invInTransaction.CartonCode = this.m_InputCarton;

                    }
                    else
                    {
                        invInTransaction.CartonCode = ((SimulationReport)objSimulationReport).CartonCode;
                    }

                    invInTransaction.PalletCode = this.txtPalletNO.Value.Trim();
                    invInTransaction.ItemCode = strItemCode;
                    invInTransaction.MOCode = ((SimulationReport)objSimulationReport).MOCode;
                    invInTransaction.BusinessCode = this.cboBusinessType.SelectedItemValue.ToString().Trim();
                    invInTransaction.StackCode = this.ucLabelEditStock.Value.Trim();
                    invInTransaction.StorageCode = this.ucLabelComboxINVType.SelectedItemValue.ToString().Trim();
                    
                    invInTransaction.Company = this.ucLabelComboxCompany.SelectedItemValue.ToString().Trim();
                    invInTransaction.BusinessReason = BussinessReason.type_produce;
                    invInTransaction.OrgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                    invInTransaction.SSCode = SScode;
                    invInTransaction.Serial = 0;
                    invInTransaction.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;
                    invInTransaction.MaintainDate = dbDateTime.DBDate;
                    invInTransaction.MaintainTime = dbDateTime.DBTime;

                    this.DataProvider.BeginTransaction();
                    try
                    {
                        if (pf.GetPallet(this.txtPalletNO.Value.Trim().ToUpper()) == null)
                        {
                            _newPallet = new BenQGuru.eMES.Package.PackageFacade(DataProvider).CreateNewPallet(this.txtPalletNO.Value.Trim().ToString(),
                                                                                                               this.txtSscode.Value,
                                                                                                               int.Parse(this.txtCapacity.Value.ToString().Trim()),
                                                                                                               ApplicationService.Current().ResourceCode,
                                                                                                               lotNo);
                            pf.ACTIONPalletAndRacrd(this.updatePallet(NowRcard), this.AddPalletRcard(NowRcard), stackToRcard, invInTransaction);
                            //记log
                            if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
                            {
                                pf.AddPallet2RcardLog(this.txtPalletNO.Value, sourceRCard, ApplicationService.Current().UserCode);
                            }
                            else
                            {
                                pf.AddPallet2RcardLog(this.txtPalletNO.Value, NowRcard, ApplicationService.Current().UserCode);
                            }
                        }
                        else
                        {
                            pf.ACTIONPalletAndRacrd(this.updatePallet(NowRcard), this.AddPalletRcard(NowRcard), stackToRcard, invInTransaction);
                            //记log
                            if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
                            {
                                pf.AddPallet2RcardLog(this.txtPalletNO.Value, sourceRCard, ApplicationService.Current().UserCode);
                            }
                            else
                            {
                                pf.AddPallet2RcardLog(this.txtPalletNO.Value, NowRcard, ApplicationService.Current().UserCode);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(ex), true);
                    }
                    finally
                    {
                        this.DataProvider.CommitTransaction();
                    }
                }
                else
                {
                    if (this.CheckPalletInStack(this.txtPalletNO.Value.Trim()))
                    {
                        //Message:一个栈板已经存在于垛位上，请选择垛位
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Error,
                                              "$CS_PALLET_IN_STACK"), false);
                        this.txtSN.TextFocus(true, true);
                        return;
                    }

                    this.DataProvider.BeginTransaction();
                    try
                    {
                        if (pf.GetPallet(this.txtPalletNO.Value.Trim().ToUpper()) == null)
                        {
                            _newPallet = new BenQGuru.eMES.Package.PackageFacade(DataProvider).CreateNewPallet(this.txtPalletNO.Value.Trim().ToString(),
                                                                                                               this.txtSscode.Value,
                                                                                                               int.Parse(this.txtCapacity.Value.ToString().Trim()),
                                                                                                               ApplicationService.Current().ResourceCode,
                                                                                                               lotNo);
                            pf.ACTIONPalletAndRacrd(this.updatePallet(NowRcard), this.AddPalletRcard(NowRcard));
                            //记log
                            if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
                            {
                                pf.AddPallet2RcardLog(this.txtPalletNO.Value, sourceRCard, ApplicationService.Current().UserCode);
                            }
                            else
                            {
                                pf.AddPallet2RcardLog(this.txtPalletNO.Value, NowRcard, ApplicationService.Current().UserCode);
                            }
                        }
                        else
                        {
                            pf.ACTIONPalletAndRacrd(this.updatePallet(NowRcard), this.AddPalletRcard(NowRcard));
                            //记log
                            if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
                            {
                                pf.AddPallet2RcardLog(this.txtPalletNO.Value, sourceRCard, ApplicationService.Current().UserCode);
                            }
                            else
                            {
                                pf.AddPallet2RcardLog(this.txtPalletNO.Value, NowRcard, ApplicationService.Current().UserCode);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(ex), true);
                    }
                    finally
                    {
                        this.DataProvider.CommitTransaction();
                    }
                }

                this.txtCollected.Value = Convert.ToString(int.Parse(this.txtCollected.Value) + 1);
                if (objectCartonRCard.CheckedItem.DataValue.ToString() == packCarton)
                {
                    ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Success,
                                              "$CS_RCARD_PALLET_SUCCESS "), false);
                    ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"), false);
                }
                else
                {
                    ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Success,
                                               "$CS_RCARD_PALLET_SUCCESS "), false);
                    ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message("$CS_Please_Input_RunningCard"), false);
                }
                this.txtSN.TextFocus(true, true);
                if (this.chkPrint.Checked)
                {
                    if (IsPalletFull(this.txtPalletNO.Value))
                    {
                        ShowPalletPrintForm(this.txtPalletNO.Value, true);
                    }
                }
            }
            //栈板与产品存在关系，需要删除栈板
            else
            {
                string moCode = ((Pallet2RCard)objPalletRcrad).MOCode;
                if (_DataFace == null)
                {
                    _DataFace = new BenQGuru.eMES.DataCollect.DataCollectFacade(DataProvider);
                }

                object objSimulationReport = null;
                //Add By Bernard @ 2010-11-03
                if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
                {
                    objSimulationReport = this._DataFace.GetSimulationReport(moCode, sourceRCard);
                }
                else
                {
                    objSimulationReport = this._DataFace.GetSimulationReport(moCode, NowRcard);
                }
                
                string rcardSScode = string.Empty;
                string strItemCode = string.Empty;
                string resourceCode = string.Empty;
                if (objSimulationReport != null)
                {
                    resourceCode = ((SimulationReport)objSimulationReport).ResourceCode.ToString().Trim();
                    strItemCode = ((SimulationReport)objSimulationReport).ItemCode.ToString().Trim();
                }

                BaseModelFacade _mf = new BenQGuru.eMES.BaseSetting.BaseModelFacade(DataProvider);
                object objRes = _mf.GetResource(resourceCode);
                if (objRes != null)
                {
                    rcardSScode = ((Resource)objRes).StepSequenceCode.Trim().ToString();
                }
                string strScode = ((Domain.Package.Pallet)objPallet).SSCode;
                //此Rcard已经被其他栈板采集
                if (((Pallet2RCard)objPalletRcrad).PalletCode != this.txtPalletNO.Value.Trim())
                {
                    ucMessage1.AddEx(this._FunctionName, this.objectCartonRCard.Items[1].DisplayText + ": " + NowRcard, new UserControl.Message(MessageType.Error,
                                            "$CS_RCard_Is_Be_Collected : "
                                            + ((Domain.Package.Pallet2RCard)objPalletRcrad).PalletCode + " $RCard = " + NowRcard), true);
                    if (objectCartonRCard.Value == packCarton)
                    {
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"), false);
                    }
                    else
                    {
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), false);
                    }
                    if (intRcardCount == 0)
                    {
                        this.txtItemCode.Value = string.Empty;
                        this.lblItemCodeDescV.Text = string.Empty;
                    }
                    this.txtSN.TextFocus(true, true);
                    return;
                }

                //判断栈板料号与产品序列号料号是否一致
                if (strItemCode.ToString().Trim() != this.txtItemCode.Value)
                {
                    ucMessage1.AddEx(this._FunctionName, this.objectCartonRCard.Items[1].DisplayText + ": " + _FirstInput, new UserControl.Message(MessageType.Error,
                                    "$Itemcode_IsDifferent"), true);
                    ucMessage1.AddEx(this._FunctionName, this.txtPalletNO.Caption + ": " + this.txtPalletNO.Value, new UserControl.Message(MessageType.Error
                                  , "$CS_PALLETON_ItemCode =" + this.txtItemCode.Value.Trim()), true);
                    ucMessage1.AddEx(this._FunctionName, this.objectCartonRCard.Items[1].DisplayText + ": " + _FirstInput, new UserControl.Message(MessageType.Error
                                 , "$CS_RCARD_ItemCode =" + strItemCode), true);
                    if (objectCartonRCard.Value == packCarton)
                    {
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"), false);
                    }
                    else
                    {
                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), false);
                    }
                    this.txtSN.TextFocus(true, true);
                    return;
                }

                //确认删除
                if (objPalletRcrad != null && ((Domain.Package.Pallet2RCard)objPalletRcrad).PalletCode == this.txtPalletNO.Value.Trim())
                {
                    InventoryFacade objFacade = new InventoryFacade(this.DataProvider);

                    if (MessageBox.Show(MutiLanguages.ParserString("$CS_Confirm_Delete_RCard") + " " + NowRcard + " ?", MutiLanguages.ParserString("$ShowMessage"), MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        pf.DeletePallet2RCard((Domain.Package.Pallet2RCard)objPalletRcrad);
                        Domain.Package.Pallet UpdatePallet = (Domain.Package.Pallet)objPallet;
                        UpdatePallet.RCardCount = UpdatePallet.RCardCount - 1;
                        this.txtPalletCollect.Value = UpdatePallet.RCardCount.ToString();
                        if (UpdatePallet.RCardCount == 0)
                        {
                            UpdatePallet.ItemCode = " ";
                            UpdatePallet.MOCode = " ";
                            UpdatePallet.EAttribute1 = " ";
                            UpdatePallet.Rescode = " ";
                            this.txtItemCode.Value = " ";
                            this.lblItemCodeDescV.Text = " ";

                        }
                        pf.UpdatePallet(UpdatePallet);

                        //记log
                        if (objectCartonRCard.CheckedItem.DataValue.ToString() == packRcard)
                        {
                            pf.SaveRemovePallet2RcardLog(this.txtPalletNO.Value, sourceRCard, ApplicationService.Current().UserCode);
                        }
                        else
                        {
                            pf.SaveRemovePallet2RcardLog(this.txtPalletNO.Value, NowRcard, ApplicationService.Current().UserCode);
                        }

                        ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Success,
                                         "$CS_Delete_Success"), false);
                        if (objectCartonRCard.CheckedItem.DataValue.ToString() == packCarton)
                        {
                            ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"), false);
                        }
                        else
                        {
                            ucMessage1.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"), false);
                        }
                        txtSN.TextFocus(true, true);
                    }
                    else
                    {
                        txtSN.TextFocus(true, true);
                    }
                }
            }
        }
        //采集
        private void DoPackPalleting()
        {
            switch (objectCartonRCard.CheckedItem.DataValue.ToString())
            {
                case packRcard:
                    if (_FirstInput != string.Empty)
                    {
                        Package.PackageFacade pf = new BenQGuru.eMES.Package.PackageFacade(DataProvider);

                        //根据当前序列号获取产品的原始序列号
                        DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
                        sourceRCard = dcf.GetSourceCard(_FirstInput.Trim().ToUpper(), string.Empty);

                        object objPalletRcrad = pf.GetPallet2RCardByRCard(FormatHelper.CleanString(sourceRCard));
                        //未输入PALLETNO
                        if (this.txtPalletNO.Value.ToString().Trim() == "")
                        {
                            //无此Rcard栈板信息且界面上栈板号不存在
                            if (objPalletRcrad == null)
                            {
                                ucMessage1.AddEx(this._FunctionName, this.txtPalletNO.Caption + ": " + this.txtPalletNO.Value, new UserControl.Message(MessageType.Error, "$CS_PLEASE_INPUT_PALLETNO"), true);
                                this.txtPalletNO.TextFocus(true, true);
                            }
                            //有此Rcard栈板信息且界面上栈板号不存在
                            else if (objPalletRcrad != null)
                            {
                                this.txtPalletNO.Value = ((Domain.Package.Pallet2RCard)objPalletRcrad).PalletCode.ToString().Trim();
                                object objPallet = pf.GetPallet(FormatHelper.CleanString(this.txtPalletNO.Value));
                                this.txtItemCode.Value = ((Domain.Package.Pallet)objPallet).ItemCode.ToString().Trim();
                                this.txtCapacity.Value = ((Domain.Package.Pallet)objPallet).Capacity.ToString();
                                this.txtPalletCollect.Value = ((Domain.Package.Pallet)objPallet).RCardCount.ToString();

                                ItemFacade itemface = new ItemFacade(DataProvider);
                                object obj = itemface.GetItem(this.txtItemCode.Value, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                                if (obj != null)
                                {
                                    this.lblItemCodeDescV.Text = ((Item)obj).ItemDescription.ToString();
                                }
                                ucMessage1.AddEx("$CS_Please_Input_RunningCard");
                                txtSN.TextFocus(true, true);
                            }
                        }
                        //有输入PALLETNO
                        else
                        {
                            object objPallet = pf.GetPallet(FormatHelper.CleanString(this.txtPalletNO.Value));
                            if (objPallet == null)
                            {
                                if (this.txtItemCode.Value.Trim() == string.Empty)
                                {
                                    object objSimulationReport = this.GetLastSimulationReport(FormatHelper.CleanString(sourceRCard));
                                    if (objSimulationReport != null)
                                    {
                                        this.txtItemCode.Value = ((SimulationReport)objSimulationReport).ItemCode.ToString().Trim();
                                    }
                                }

                                ItemFacade itemface = new ItemFacade(DataProvider);
                                object obj = itemface.GetItem(this.txtItemCode.Value, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                                if (obj != null)
                                {
                                    this.lblItemCodeDescV.Text = ((Item)obj).ItemDescription.ToString();
                                }

                                Pallet newPallet = pf.CreateNewPallet();
                                this.AddPallet2Rcard(newPallet, objPalletRcrad, FormatHelper.CleanString(_FirstInput));

                            }
                            else
                            {
                                if (this.txtItemCode.Value.Trim() == string.Empty && ((Domain.Package.Pallet)objPallet).RCardCount.ToString() == "0")
                                {
                                    object objSimulationReport = this.GetLastSimulationReport(FormatHelper.CleanString(sourceRCard));
                                    if (objSimulationReport != null)
                                    {
                                        this.txtItemCode.Value = ((SimulationReport)objSimulationReport).ItemCode.ToString().Trim();
                                    }
                                }
                                else
                                {
                                    this.txtItemCode.Value = ((Domain.Package.Pallet)objPallet).ItemCode.ToString().Trim();
                                }
                                ItemFacade itemface = new ItemFacade(DataProvider);
                                object obj = itemface.GetItem(this.txtItemCode.Value, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                                if (obj != null)
                                {
                                    this.lblItemCodeDescV.Text = ((Item)obj).ItemDescription.ToString();
                                }
                                this.AddPallet2Rcard(objPallet, objPalletRcrad, FormatHelper.CleanString(_FirstInput));
                            }
                        }
                        this.UpdateMainGrid();
                        this.UpdatePalletGrid();
                    }
                    break;
                case packCarton:
                    if (_FirstInput != string.Empty)
                    {
                        Package.PackageFacade pf = new BenQGuru.eMES.Package.PackageFacade(DataProvider);
                        DataCollectFacade face = new DataCollectFacade(DataProvider);
                        object[] objSimulationReports = face.QuerySimulationReportByCarton(FormatHelper.CleanString(_FirstInput));
                        if (objSimulationReports != null)
                        {
                            for (int i = 0; i < objSimulationReports.Length; i++)
                            {
                                object objPalletRcrad = pf.GetPallet2RCardByRCard(((SimulationReport)objSimulationReports[i]).RunningCard.Trim().ToUpper());

                                //未输入PALLETNO
                                if (this.txtPalletNO.Value.ToString().Trim() == "")
                                {
                                    //无此Rcard栈板信息且界面上栈板号不存在
                                    if (objPalletRcrad == null)
                                    {
                                        ucMessage1.AddEx(this._FunctionName, this.txtPalletNO.Caption + ": " + this.txtPalletNO.Value, new UserControl.Message(MessageType.Error, "$CS_PleasePressEnterOnPalletNO"), true);
                                        this.txtPalletNO.TextFocus(true, true);
                                    }
                                    //有此Rcard栈板信息且界面上栈板号不存在
                                    else if (objPalletRcrad != null && i == objSimulationReports.Length - 1)
                                    {
                                        this.txtPalletNO.Value = ((Domain.Package.Pallet2RCard)objPalletRcrad).PalletCode.ToString().Trim();
                                        object objPallet = pf.GetPallet(FormatHelper.CleanString(this.txtPalletNO.Value));
                                        this.txtItemCode.Value = ((Domain.Package.Pallet)objPallet).ItemCode.ToString().Trim();
                                        this.txtCapacity.Value = ((Domain.Package.Pallet)objPallet).Capacity.ToString();
                                        this.txtPalletCollect.Value = ((Domain.Package.Pallet)objPallet).RCardCount.ToString();
                                        this.lblItemCodeDescV.Text = ((Domain.Package.Pallet)objPallet).EAttribute1.ToString();

                                        ucMessage1.AddEx("$CS_PLEASE_INPUT_CARTONNO");
                                        txtSN.TextFocus(true, true);
                                    }
                                }
                                //有输入PALLETNO
                                else
                                {
                                    object objPallet = pf.GetPallet(FormatHelper.CleanString(this.txtPalletNO.Value));
                                    if (objPallet == null)
                                    {
                                        if (this.txtItemCode.Value.Trim() == string.Empty)
                                        {
                                            object objSimulationReport = this.GetLastSimulationReport(FormatHelper.CleanString(_FirstInput));
                                            if (objSimulationReport != null)
                                            {
                                                this.txtItemCode.Value = ((SimulationReport)objSimulationReport).ItemCode.ToString().Trim();
                                            }
                                        }

                                        ItemFacade itemface = new ItemFacade(DataProvider);
                                        object obj = itemface.GetItem(this.txtItemCode.Value, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                                        if (obj != null)
                                        {
                                            this.lblItemCodeDescV.Text = ((Item)obj).ItemDescription.ToString();
                                        }
                                        //新建栈板
                                        Pallet newPallet = pf.CreateNewPallet();
                                        this.AddPallet2Rcard(newPallet, objPalletRcrad, ((SimulationReport)objSimulationReports[i]).RunningCard.ToString());
                                    }
                                    else
                                    {
                                        if (this.txtItemCode.Value.Trim() == string.Empty && ((Domain.Package.Pallet)objPallet).RCardCount.ToString() == "0")
                                        {
                                            object objSimulationReport = this.GetLastSimulationReport(FormatHelper.CleanString(_FirstInput));
                                            if (objSimulationReport != null)
                                            {
                                                this.txtItemCode.Value = ((SimulationReport)objSimulationReport).ItemCode.ToString().Trim();
                                            }
                                        }
                                        else
                                        {
                                            this.txtItemCode.Value = ((Domain.Package.Pallet)objPallet).ItemCode.ToString().Trim();
                                        }
                                        ItemFacade itemface = new ItemFacade(DataProvider);
                                        object obj = itemface.GetItem(this.txtItemCode.Value, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                                        if (obj != null)
                                        {
                                            this.lblItemCodeDescV.Text = ((Item)obj).ItemDescription.ToString();
                                        }
                                        this.AddPallet2Rcard(objPallet, objPalletRcrad, ((SimulationReport)objSimulationReports[i]).RunningCard.ToString());
                                    }
                                }
                            }
                        }
                        else
                        {
                            ucMessage1.AddEx(this._FunctionName, this.objectCartonRCard.Items[0].DisplayText + ": " + this.txtSN.Value, new UserControl.Message(MessageType.Error, "$NoProductInfo"), true);
                            this.txtSN.TextFocus(true, true);
                        }
                        this.UpdateMainGrid();
                        this.UpdatePalletGrid();
                    }
                    break;
            }
        }
        #endregion

    }
}