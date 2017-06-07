using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Package;
using BenQGuru.eMES.OQC;
using UserControl;
using Infragistics.Win.UltraWinGrid;

namespace BenQGuru.eMES.Client
{
    public partial class FStackAdjustment : BaseForm
    {
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private DataTable m_dtInfo;

        private object[] m_CompanyList;

        private const string packPallet = "0";
        private const string packRcard = "1";
        private const string packStack = "2";

        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        private string m_SelectedStorage;
        /// <summary>
        /// 存放用户选择后的库位
        /// </summary>
        public string SelectedStorage
        {
            get { return m_SelectedStorage; }
            set { m_SelectedStorage = value; }
        }

        private string m_SelectedStack;
        /// <summary>
        /// 存放用户选择后的垛位
        /// </summary>
        public string SelectedStack
        {
            get { return m_SelectedStack; }
            set { m_SelectedStack = value; }
        }

        public FStackAdjustment()
        {
            InitializeComponent();
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

                m_CompanyList = objList;
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

        private void FStackAdjustment_Load(object sender, EventArgs e)
        {
            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(this.gridInfo);

            this.InitializeStorageCode();
            this.InitializeCompany();

            this.InitialDataTable();
            this.txtRecordNum.Value = "0";
            this.txtRecordNum.InnerTextBox.TextAlign = HorizontalAlignment.Right;

            this.packObject.Value = packPallet;
            //this.InitPageLanguage();
        }

        private void InitialDataTable()
        {
            this.m_dtInfo = new DataTable();
            this.m_dtInfo.Columns.Add("ostackcode");
            this.m_dtInfo.Columns.Add("opalletcode");
            this.m_dtInfo.Columns.Add("tstackcode");
            this.m_dtInfo.Columns.Add("tpalletcode");
            this.m_dtInfo.Columns.Add("rcard");            
            this.m_dtInfo.Columns.Add("company");
            this.m_dtInfo.Columns.Add("itemcode");
            this.m_dtInfo.Columns.Add("itemdesc");

            this.gridInfo.DataSource = m_dtInfo;
        }

        private void btnGetStack_Click(object sender, EventArgs e)
        {
            if (this.ucLabelComboxINVType.SelectedItemText.Trim().Length == 0)
            {
                ApplicationRun.GetInfoForm().Add(
                   new UserControl.Message(MessageType.Error, "$CS_STORAGE_NOT_INPUT"));
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

            //this.ucLabelEditStock.Value = e.CustomObject["stackcode"].ToString();
        }

        /// <summary>
        /// Check 从垛位使用状况的页面带过来的垛位和库位
        /// </summary>
        /// <returns>true/false</returns>
        private bool CheckSelecetedStackAndStorage(string selectedStorage, string selectedStack, string originalStorage)
        {
            if (originalStorage.Trim().Length == 0)
            {
                //请输入库位
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_STORAGE_NOT_INPUT"));
                return false;
            }

            if (!selectedStorage.Equals(originalStorage))
            {
                InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
                int stack2RCardCount = inventoryFacade.GetStack2RCardCount(selectedStack, originalStorage);

                if (stack2RCardCount > 0)
                {
                    //垛位和库别不对应
                    ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_STACK_STORAGE_NOT_SAME"));
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

        private void gridInfo_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            //e.Layout.Override.AllowColSizing = AllowColSizing.None;
            e.Layout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;

            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            e.Layout.Bands[0].Columns["ostackcode"].Header.Caption = "原垛位";
            e.Layout.Bands[0].Columns["ostackcode"].Width = 100;
            e.Layout.Bands[0].Columns["ostackcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["opalletcode"].Header.Caption = "原栈板";
            e.Layout.Bands[0].Columns["opalletcode"].Width = 150;
            e.Layout.Bands[0].Columns["opalletcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["tstackcode"].Header.Caption = "目标垛位";
            e.Layout.Bands[0].Columns["tstackcode"].Width = 100;
            e.Layout.Bands[0].Columns["tstackcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["tpalletcode"].Header.Caption = "目标栈板";
            e.Layout.Bands[0].Columns["tpalletcode"].Width = 150;
            e.Layout.Bands[0].Columns["tpalletcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["rcard"].Header.Caption = "序列号";
            e.Layout.Bands[0].Columns["rcard"].Width = 150;
            e.Layout.Bands[0].Columns["rcard"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            
            e.Layout.Bands[0].Columns["company"].Header.Caption = "公司别";
            e.Layout.Bands[0].Columns["company"].Width = 100;
            e.Layout.Bands[0].Columns["company"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["itemcode"].Header.Caption = "产品代码";
            e.Layout.Bands[0].Columns["itemcode"].Width = 100;
            e.Layout.Bands[0].Columns["itemcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["itemdesc"].Header.Caption = "产品描述";
            e.Layout.Bands[0].Columns["itemdesc"].Width = 150;
            e.Layout.Bands[0].Columns["itemdesc"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            //this.InitGridLanguage(gridInfo);
        }

        private void opsetPackObject_ValueChanged(object sender, EventArgs e)
        {
            if (packObject.Value == packPallet)
            {
                this.ControlEnabled(true);
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Normal, "$CS_Please_Input_PALLET"));
                this.txtInput.TextFocus(true, true);
            }

            if (packObject.Value == packRcard)
            {
                this.ControlEnabled(true);
                if (this.rdoUseOPallet.Checked)
                {
                    packObject.Value = packPallet;
                    return;
                }

                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"));
                this.txtInput.TextFocus(true, true);
            }

            if (packObject.Value == packStack)
            {
                this.ControlEnabled(false);

                ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_STACK"));
                this.txtInput.TextFocus(true, true);
            }
        }

        private void rdoUseOPallet_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoUseOPallet.Checked)
            {
                this.txtUseNPallet.ReadOnly = true;
                this.txtUseNPallet.Value = "";
                this.txtUseTPallet.ReadOnly = true;
                this.txtUseTPallet.Value = "";

                this.packObject.Value = packPallet;
            }

        }

        private void rdoUseTPallet_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoUseTPallet.Checked)
            {
                this.txtUseNPallet.ReadOnly = true;
                this.txtUseNPallet.Value = "";
                this.txtUseTPallet.ReadOnly = false;
                this.txtUseTPallet.Value = "";
                this.txtUseTPallet.TextFocus(false, true);
            }
        }

        private void rdoUseNPallet_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoUseNPallet.Checked)
            {
                this.txtUseNPallet.ReadOnly = false;
                this.txtUseNPallet.Value = "";
                this.txtUseTPallet.ReadOnly = true;
                this.txtUseTPallet.Value = "";
                this.txtUseNPallet.TextFocus(false, true);
            }
        }

        private bool CheckUI()
        {
            if (this.ucLabelComboxINVType.SelectedItemText.Trim().Length == 0)
            {
                //请输入库位
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_STORAGE_NOT_INPUT"));
                this.ucLabelComboxINVType.Focus();
                return false;
            }

            if (this.ucLabelEditStock.Value.Trim().Length == 0)
            {
                //请输入垛位
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_PLEASE_INPUT_STACK"));
                this.ucLabelEditStock.TextFocus(false, true);
                return false;
            }

            if (!this.CheckSelecetedStackAndStorage(this.SelectedStorage, this.SelectedStack, Convert.ToString(this.ucLabelComboxINVType.SelectedItemValue)))
            {
                this.ucLabelEditStock.TextFocus(false, true);
                return false;
            }

            if (this.rdoUseTPallet.Checked && this.packObject.Value != packStack)
            {
                if (this.txtUseTPallet.Value.Trim().Length == 0)
                {
                    //请输入目标垛位栈板号
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PLEASE_INPUT_TAR_PALLET_CODE"));
                    this.txtUseTPallet.TextFocus(false, true);
                    return false;
                }
            }

            if (rdoUseNPallet.Checked && this.packObject.Value != packStack)
            {
                if (this.txtUseNPallet.Value.Trim().Length == 0)
                {
                    //请输入新栈板号
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PLEASE_INPUT_NEW_PALLET_CODE"));
                    this.txtUseNPallet.TextFocus(false, true);
                    return false;
                }
            }

            return true;
        }

        private void txtInput_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.txtInput.Value.Trim().Length == 0)
                {
                    this.txtInput.TextFocus(true, true);
                    return;
                }

                ////Check UI
                //
                if (!this.CheckUI())
                {
                    return;
                }


                switch (this.packObject.CheckedItem.DataValue.ToString())
                {
                    case packPallet:
                        ////按照栈板号输入
                        //                      
                        if (!this.InputPallet(this.txtInput.Value.Trim()))
                        {
                            this.txtInput.TextFocus(true, true);
                            return;
                        }
                        break;
                    case packRcard:
                        ////按照序列号输入
                        //
                        if (!this.InputRcard(this.txtInput.Value.Trim()))
                        {
                            this.txtInput.TextFocus(true, true);
                            return;
                        }
                        break;
                    case packStack:
                        if (!this.InputStack(this.txtInput.Value.Trim()))
                        {
                            this.txtInput.TextFocus(true, true);
                            return;
                        }
                        break;
                    default:
                        break;
                }

                //添加成功
                ApplicationRun.GetInfoForm().Add(
                new UserControl.Message(MessageType.Success, "$CS_Add_Success"));

                if (packObject.Value == packPallet)
                {
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Normal, "$CS_Please_Input_PALLET"));
                }

                if (packObject.Value == packRcard)
                {
                    if (this.rdoUseOPallet.Checked)
                    {
                        packObject.Value = packPallet;
                        this.txtInput.TextFocus(true, true);
                        return;
                    }

                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"));
                }

                this.txtInput.TextFocus(true, true);
            }
        }

        private bool InputPallet(string palletCode)
        {
            ////Check 栈板是否存在
            //
            PackageFacade objFacade = new PackageFacade(this.DataProvider);
            object pallet = objFacade.GetPallet(palletCode);

            if (pallet == null)
            {
                //Message:该栈板不存在
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_PALLETNO_IS_NOT_EXIT"));
                return false;
            }


            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);

            ////使用原栈板
            //
            if (this.rdoUseOPallet.Checked)
            {

                ////Check 栈板在目标垛位中已经存在
                //
                object[] rcardToStackPallet = inventoryFacade.GetRcardToStackPallet(this.ucLabelEditStock.Value, palletCode, "");

                if (rcardToStackPallet != null)
                {
                    //Message:栈板在目标垛位中已经存在
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PALLET_IS_EXIST_IN_TAR_STACK"));
                    return false;
                }

                ////获取源栈板的垛位资料
                //
                object[] rcardToStackPalletList = inventoryFacade.GetRcardToStackPallet("", palletCode, "");

                if (rcardToStackPalletList == null)
                {
                    //Message:源栈板对应的垛位信息不存在
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PALLET_IS_NOT_EXIST_IN_ORI_STACK"));
                    return false;
                }

                if (inventoryFacade.CheckStackIsOnlyAllowOneItem(ucLabelEditStock.Value.ToString()) && 
                    CheckStackItemError(this.ucLabelEditStock.Value.Trim(), this.ucLabelComboxINVType.SelectedItemValue.ToString(),
                                    ((RcardToStackPallet)rcardToStackPalletList[0]).ItemCode))
                {
                    //Message:目标垛位的物料和当前物料不一致
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_STACK_ITEM_DIFF"));
                    return false;
                }

                ////根据源栈板的Rcard更新StackToRcard
                //
                inventoryFacade.UpdatePalletStackByWholePallet(rcardToStackPalletList,
                                                                this.ucLabelComboxINVType.SelectedItemValue.ToString(),
                                                                this.ucLabelEditStock.Value.Trim(),
                                                                ApplicationService.Current().LoginInfo.UserCode);

                //Load Grid
                this.LoadGrid(rcardToStackPalletList, this.ucLabelEditStock.Value.Trim(), palletCode);


            }

            ////使用目标垛位栈板
            //
            if (this.rdoUseTPallet.Checked)
            {
                if (palletCode.Equals(this.txtUseTPallet.Value.Trim()))
                {
                    //Message:源栈板和目标栈板相同
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PALLETNO_SAME_AS_TARPALLET"));
                    return false;
                }

                ////check目标垛位栈板的,产品,公司别,产品档次,是否和序列号的栈板一致
                //
                //获取源栈板信息
                object[] objOriStackToRcardList = inventoryFacade.GetStackToRcardInfoByPallet(palletCode);
                object[] objTarStackToRcardList = inventoryFacade.GetStackToRcardInfoByPallet(this.txtUseTPallet.Value.Trim());

                if (objOriStackToRcardList == null)
                {
                    //Message:源栈板对应的垛位信息不存在
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PALLET_IS_NOT_EXIST_IN_ORI_STACK"));
                    return false;

                }

                if (inventoryFacade.CheckStackIsOnlyAllowOneItem(ucLabelEditStock.Value.ToString()) && 
                    CheckStackItemError(this.ucLabelEditStock.Value.Trim(), this.ucLabelComboxINVType.SelectedItemValue.ToString(),
                                    ((StackToRcard)objOriStackToRcardList[0]).ItemCode))
                {
                    //Message:目标垛位的物料和当前物料不一致
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_STACK_ITEM_DIFF"));
                    return false;
                }

                string strErrorMessage = string.Empty;
                StackToRcard objOri = (StackToRcard)objOriStackToRcardList[0];
                StackToRcard objTar = (StackToRcard)objTarStackToRcardList[0];
                if (objOri.ItemCode != objTar.ItemCode)
                {
                    strErrorMessage = strErrorMessage + "itemcode";
                    ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_ITEM_NOT_SAME_IN_PALLET_TWO $CS_TARGET=" + objTar.ItemCode + " $CS_ORIGINAL=" + objOri.ItemCode));
                }

                if (objOri.Company != objTar.Company)
                {
                    strErrorMessage = strErrorMessage + "companycode";
                    ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_COMPANY_NOT_SAME_IN_PALLET_TWO $CS_TARGET=" + objTar.Company + " $CS_ORIGINAL=" + objOri.Company));
                }                

                if (strErrorMessage != string.Empty)
                {
                    return false;
                }

                ////获取源栈板的垛位资料
                //
                object[] rcardToStackPallet = inventoryFacade.GetRcardToStackPallet("", palletCode, "");

                ////把原栈板更新为目标栈板
                //
                inventoryFacade.UpdateOriPalletStackToTargetPallet(rcardToStackPallet,
                                                                    this.txtUseTPallet.Value.Trim(),
                                                                    this.ucLabelComboxINVType.SelectedItemValue.ToString(),
                                                                    this.ucLabelEditStock.Value.Trim(),
                                                                    ApplicationService.Current().LoginInfo.UserCode);

                //Load Grid
                this.LoadGrid(rcardToStackPallet, this.ucLabelEditStock.Value.Trim(), this.txtUseTPallet.Value.Trim());
            }

            ////使用新栈板
            //
            if (this.rdoUseNPallet.Checked)
            {
                if (palletCode.Equals(this.txtUseNPallet.Value.Trim()))
                {
                    //Message:源栈板和目标栈板相同
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PALLETNO_SAME_AS_TARPALLET"));
                    return false;
                }


                ////获取源栈板的垛位资料
                //
                object[] rcardToStackPallet = inventoryFacade.GetRcardToStackPallet("", palletCode, "");

                if (rcardToStackPallet == null)
                {
                    //Message:源栈板对应的垛位信息不存在
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PALLET_IS_NOT_EXIST_IN_ORI_STACK"));
                    return false;
                }

                if (inventoryFacade.CheckStackIsOnlyAllowOneItem(ucLabelEditStock.Value.ToString()) && CheckStackItemError(this.ucLabelEditStock.Value.Trim(), this.ucLabelComboxINVType.SelectedItemValue.ToString(),
                                    ((RcardToStackPallet)rcardToStackPallet[0]).ItemCode))
                {
                    //Message:目标垛位的物料和当前物料不一致
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_STACK_ITEM_DIFF"));
                    return false;
                }

                //检查栈板容量是否满
                if (!inventoryFacade.CheckStackCapacity(this.ucLabelComboxINVType.SelectedItemValue.ToString().Trim().ToUpper(),
                                                        this.ucLabelEditStock.Value.Trim().ToUpper()))
                {
                    ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_STACK_CAPACITY_FULL"));
                    return false;
                }


                string lotNo = " ";
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                SimulationReport simulationReport =(SimulationReport)dataCollectFacade.GetRcardFromSimulationReport(((RcardToStackPallet)rcardToStackPallet[0]).SerialNo);
                if (simulationReport!=null)
                {
                    OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
                    OQCLot2Card oqcLot2Card = (OQCLot2Card)oqcFacade.GetLastOQCLot2CardByRCard(simulationReport.RunningCard);

                    if (oqcLot2Card != null)
                    {
                        lotNo = oqcLot2Card.LOTNO;
                    } 
                }

                //把原栈板更新为新栈板
                inventoryFacade.UpdateOriPalletStackToNewPallet(rcardToStackPallet,
                                                                this.txtUseNPallet.Value.Trim(),
                                                                this.ucLabelComboxINVType.SelectedItemValue.ToString(),
                                                                this.ucLabelEditStock.Value.Trim(),
                                                                ApplicationService.Current().LoginInfo.UserCode,
                                                                ApplicationService.Current().ResourceCode,
                                                                lotNo);

                //Load Grid
                this.LoadGrid(rcardToStackPallet, this.ucLabelEditStock.Value.Trim(), this.txtUseNPallet.Value.Trim());
            }

            return true;
        }

        private bool InputRcard(string rcard)
        {

            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            DataTable dtInfo = inventoryFacade.GetSimulationReportInfo(rcard, packRcard);

            if (dtInfo == null)
            {
                //该序列号不存在
                ApplicationRun.GetInfoForm().Add(
                new UserControl.Message(MessageType.Error, "$CS_RCARD_IS_NOT_EXIT"));
                return false;
            }

            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRcard = dataCollectFacade.GetSourceCard(rcard.Trim().ToUpper(), string.Empty);

            ////使用目标垛位栈板
            //
            if (this.rdoUseTPallet.Checked)
            {
                ////check目标垛位栈板的,产品,公司别,产品档次,是否和序列号的栈板一致
                //
                //获取源栈板信息
                object[] objOriRcardTOStackPalletList = inventoryFacade.GetRcardToStackPallet("", "", sourceRcard);
                object[] objTarStackToRcardList = inventoryFacade.GetStackToRcardInfoByPallet(this.txtUseTPallet.Value.Trim());

                if (objOriRcardTOStackPalletList == null)
                {
                    //Message:产品序列号不存在垛位中
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_RCARD_IS_NOT_EXIST_IN_ORI_STACK"));
                    return false;

                }

                ////CHeck 序列号对应的栈板和目标栈板是否相同
                //
                if (((RcardToStackPallet)objOriRcardTOStackPalletList[0]).PalletCode.Equals(this.txtUseTPallet.Value.Trim()))
                {
                    //Message:源栈板和目标栈板相同
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PALLETNO_SAME_AS_TARPALLET"));
                    return false;
                }

                if (inventoryFacade.CheckStackIsOnlyAllowOneItem(ucLabelEditStock.Value.ToString()) && CheckStackItemError(this.ucLabelEditStock.Value.Trim(), this.ucLabelComboxINVType.SelectedItemValue.ToString(),
                                    ((RcardToStackPallet)objOriRcardTOStackPalletList[0]).ItemCode))
                {
                    //Message:目标垛位的物料和当前物料不一致
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_STACK_ITEM_DIFF"));
                    return false;
                }

                string strErrorMessage = string.Empty;
                RcardToStackPallet objOri = (RcardToStackPallet)objOriRcardTOStackPalletList[0];
                StackToRcard objTar = (StackToRcard)objTarStackToRcardList[0];
                if (objOri.ItemCode != objTar.ItemCode)
                {
                    strErrorMessage = strErrorMessage + "itemcode";
                    ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_ITEM_NOT_SAME_IN_PALLET_TWO $CS_TARGET=" + objTar.ItemCode + " $CS_ORIGINAL=" + objOri.ItemCode));
                }

                if (objOri.Company != objTar.Company)
                {
                    strErrorMessage = strErrorMessage + "companycode";
                    ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_COMPANY_NOT_SAME_IN_PALLET_TWO $CS_TARGET=" + objTar.Company + " $CS_ORIGINAL=" + objOri.Company));
                }                

                if (strErrorMessage != string.Empty)
                {
                    return false;
                }

                ////更新该产品序列号的源栈板为目标栈板
                //
                inventoryFacade.UpdateOriPalletStackToTargetPalletByRcard(objOri,
                                                                            this.txtUseTPallet.Value.Trim(),
                                                                            this.ucLabelComboxINVType.SelectedItemValue.ToString(),
                                                                            this.ucLabelEditStock.Value.Trim(),
                                                                             ApplicationService.Current().LoginInfo.UserCode);
                //Load Grid
                this.LoadGrid(objOriRcardTOStackPalletList, this.ucLabelEditStock.Value.Trim(), this.txtUseTPallet.Value.Trim());
            }

            ////使用新栈板
            //
            if (this.rdoUseNPallet.Checked)
            {
                ////获取源序列号对应的垛位资料
                //
                object[] rcardToStackPallet = inventoryFacade.GetRcardToStackPallet("", "", sourceRcard);

                if (rcardToStackPallet == null)
                {
                    //Message:产品序列号不存在垛位中
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_RCARD_IS_NOT_EXIST_IN_ORI_STACK"));
                    return false;
                }

                ////CHeck 序列号对应的栈板和目标栈板是否相同
                //
                if (((RcardToStackPallet)rcardToStackPallet[0]).PalletCode.Equals(this.txtUseNPallet.Value.Trim()))
                {
                    //Message:序列号对应的栈板和目标栈板是否相同
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PALLETNO_IN_RCARD_SAME_AS_TARPALLET"));
                    return false;
                }

                //获取源栈板信息
                object[] objOriRcardTOStackPalletList = inventoryFacade.GetRcardToStackPallet("", "", sourceRcard);

                if (objOriRcardTOStackPalletList == null)
                {
                    //Message:产品序列号不存在垛位中
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_RCARD_IS_NOT_EXIST_IN_ORI_STACK"));
                    return false;

                }

                if (inventoryFacade.CheckStackIsOnlyAllowOneItem(ucLabelEditStock.Value.ToString()) && 
                    CheckStackItemError(this.ucLabelEditStock.Value.Trim(), this.ucLabelComboxINVType.SelectedItemValue.ToString(),
                                    ((RcardToStackPallet)objOriRcardTOStackPalletList[0]).ItemCode))
                {
                    //Message:目标垛位的物料和当前物料不一致
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_STACK_ITEM_DIFF"));
                    return false;
                }

                //检查栈板容量是否满
                if (!inventoryFacade.CheckStackCapacity(this.ucLabelComboxINVType.SelectedItemValue.ToString().Trim().ToUpper(),
                                                        this.ucLabelEditStock.Value.Trim().ToUpper()))
                {
                    ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_STACK_CAPACITY_FULL"));
                    return false;
                }

                
                string lotNo = " ";
                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
                OQCLot2Card oqcLot2Card =(OQCLot2Card) oqcFacade.GetLastOQCLot2CardByRCard(sourceRcard);

                if (oqcLot2Card!=null)
                {
                    lotNo = oqcLot2Card.LOTNO;
                }

                ////更新该产品序列号的源栈板为新栈板
                //
                inventoryFacade.UpdateOriPalletStackToNewPalletByRcard(rcardToStackPallet[0],
                                                                        this.txtUseNPallet.Value.Trim(),
                                                                        this.ucLabelComboxINVType.SelectedItemValue.ToString(),
                                                                        this.ucLabelEditStock.Value.Trim(),
                                                                        ApplicationService.Current().LoginInfo.UserCode,
                                                                        ApplicationService.Current().ResourceCode,
                                                                        lotNo);

                //Load Grid
                this.LoadGrid(rcardToStackPallet, this.ucLabelEditStock.Value.Trim(), this.txtUseNPallet.Value.Trim());
            }

            return true;
        }


        private bool InputStack(string stackCode)
        {
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);

            SStack stackObject = (SStack)inventoryFacade.GetSStack(stackCode.Trim().ToUpper());

            if (stackObject == null)
            {
                ApplicationRun.GetInfoForm().Add(
                 new UserControl.Message(MessageType.Error, "$CS_Stack_Is_Not_Exist"));
                return false;
            }

            object[] rcardToStackPallet = inventoryFacade.GetRcardToStackPallet(stackCode.Trim().ToUpper(), string.Empty, string.Empty);

            //转移垛位物料信息
            object[] rcardToStackObjects = inventoryFacade.GetStackToRcardByStack(stackCode.Trim().ToUpper(), string.Empty);

            if (rcardToStackObjects == null)
            {
                //Message:转移垛位没有物料
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_STACK_Not_Have_Item"));
                return false;
            }

            if (inventoryFacade.CheckStackIsOnlyAllowOneItem(ucLabelEditStock.Value.ToString()) && 
                CheckStackItemError(this.ucLabelEditStock.Value.Trim(), this.ucLabelComboxINVType.SelectedItemValue.ToString(),
                                   ((RcardToStackPallet)rcardToStackPallet[0]).ItemCode))
            {
                //Message:目标垛位的物料和当前物料不一致
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_STACK_ITEM_DIFF"));
                return false;
            }

            //检查垛位容量是否满
            if (!inventoryFacade.CheckStackCapacity(this.ucLabelComboxINVType.SelectedItemValue.ToString().Trim().ToUpper(),
                                                    this.ucLabelEditStock.Value.Trim().ToUpper(), stackCode.Trim().ToUpper()))
            {
                ApplicationRun.GetInfoForm().Add(
                new UserControl.Message(MessageType.Error, "$CS_STACK_CAPACITY_FULL"));
                return false;
            }

            //更新垛位
            inventoryFacade.UpdateStackToRcard(this.ucLabelComboxINVType.SelectedItemValue.ToString().Trim().ToUpper(),
                                                    this.ucLabelEditStock.Value.Trim().ToUpper(),
                                                    ((StackToRcard)rcardToStackObjects[0]).StorageCode,
                                                    stackCode.Trim().ToUpper(), ApplicationService.Current().UserCode);


            //Load Grid
            this.LoadGrid(rcardToStackPallet, this.ucLabelEditStock.Value.Trim(), this.txtUseNPallet.Value.Trim());

            return true;
        }



        private bool CheckStackItemError(string stackCode, string storageCode,string itemCode)
        {
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            object[] stackToRcard = inventoryFacade.GetStackToRcardByStack(stackCode, storageCode);

            if (stackToRcard != null)
            {
                if (!((StackToRcard)stackToRcard[0]).ItemCode.Equals(itemCode))
                {
                    return true;
                }
            }

            return false;

        }

        private void LoadGrid(object[] rcardToStackPalletList, string tStackCode,string tPalletCode)
        {
            foreach (RcardToStackPallet obj in rcardToStackPalletList)
            {               
                DataRow dr = this.m_dtInfo.NewRow();
                dr["ostackcode"] = obj.StackCode;
                dr["opalletcode"] = obj.PalletCode;
                dr["tstackcode"] = tStackCode;
                if (packObject.Value == packStack)
                {
                    dr["tpalletcode"] = obj.PalletCode;
                }
                else
                {
                    dr["tpalletcode"] = tPalletCode;
                }                
               
                dr["rcard"] = obj.SerialNo;                
                dr["company"] = this.GetCompanyDesc(obj.Company);
                dr["itemcode"] = obj.ItemCode;
                dr["itemdesc"] = obj.ItemDescription;

                this.m_dtInfo.Rows.Add(dr);

                this.txtRecordNum.Value = Convert.ToString(Convert.ToInt32(this.txtRecordNum.Value) + 1);
            }
        }

        private string GetCompanyDesc(string companyCode)
        {
            foreach (Parameter objPara in this.m_CompanyList)
            {
                if (objPara.ParameterAlias.Equals(companyCode))
                {
                    return objPara.ParameterDescription;
                }
            }

            return "";
        }

        private void txtUseTPallet_Leave(object sender, EventArgs e)
        {
            if (this.txtUseTPallet.Value.Trim().Length != 0)
            {
                PackageFacade objFacade = new PackageFacade(this.DataProvider);
                object pallet = objFacade.GetPallet(this.txtUseTPallet.Value);

                if (pallet == null)
                {
                    //该栈板不存在
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PALLETNO_IS_NOT_EXIT"));
                    txtUseTPallet.TextFocus(true, true);
                }

                InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
                object[] stackToRcardS = inventoryFacade.GetRcardToStackPallet(this.ucLabelEditStock.Value.Trim(), this.txtUseTPallet.Value, "");

                if (stackToRcardS == null)
                {
                    //该栈板不在目标垛位中 栈板号
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PALLETNO_NOT_IN_TRA_STACK=" + this.txtUseTPallet.Value));
                    txtUseTPallet.TextFocus(true, true);
                }

            }
        }

        private void txtUseNPallet_Leave(object sender, EventArgs e)
        {
            if (this.txtUseNPallet.Value.Trim().Length != 0)
            {
                PackageFacade objFacade = new PackageFacade(this.DataProvider);
                object pallet = objFacade.GetPallet(this.txtUseNPallet.Value);

                if (pallet != null)
                {
                    //该栈板已存在
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PALLETNO_IS_EXIT"));
                    txtUseNPallet.TextFocus(true, true);
                }

                InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
                object[] stackToRcardS = inventoryFacade.GetRcardToStackPallet(this.ucLabelEditStock.Value.Trim(), this.txtUseNPallet.Value, "");

                if (stackToRcardS != null)
                {
                    //该栈板已经在目标垛位中 垛位=
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PALLETNO_IN_TRA_STACK=" + ((RcardToStackPallet)stackToRcardS[0]).StackCode));
                    txtUseNPallet.TextFocus(true, true);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ucLabelComboxINVType.SelectedIndex = -1;
            this.ucLabelEditStock.Value = "";
            this.SelectedStack = "";
            this.SelectedStorage = "";
            this.txtUseNPallet.Value = "";
            this.txtUseTPallet.Value = "";
            this.txtRecordNum.Value = "0";
            this.m_dtInfo.Clear();
            this.txtInput.Value = "";
            this.rdoUseOPallet.Checked = true;
            this.ucLabelComboxINVType.Focus();
        }

        private void ControlEnabled(bool isEnabled)
        {
            this.rdoUseOPallet.Enabled = isEnabled;
            this.rdoUseTPallet.Enabled = isEnabled;
            this.rdoUseNPallet.Enabled = isEnabled;
            this.txtUseTPallet.Enabled = isEnabled;
            this.txtUseNPallet.Enabled = isEnabled;
        }
    }
}