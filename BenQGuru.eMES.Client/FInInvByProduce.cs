using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Package;
using BenQGuru.eMES.Domain.Package;
using Infragistics.Win.UltraWinGrid;
using UserControl;

namespace BenQGuru.eMES.Client
{
    public partial class FInInvByProduce : BaseForm
    {
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private DataTable m_dtRcard = null;
        private const string packPallet = "0";
        private const string packCarton = "2";
        private const string packRcard = "1";

        private const string packAdd = "0";
        private const string packDelete = "2";
        
        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FInInvByProduce()
        {
            InitializeComponent();
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

        private void btnInINV_Click(object sender, EventArgs e)
        {
            if (this.grdRcard.Rows.Count==0)
	        {
                //表格中没有数据
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_GRID_NO_RECORD"));
                return;
	        }

            InventoryFacade objFacade = new InventoryFacade(this.DataProvider);

            object objectStacktoRcard = objFacade.GetStackCodeFromStacktoRcard(this.ucLabelEditStock.Value.Trim(), this.ucLabelComboxINVType.SelectedItemValue.ToString().Trim());

            if (objectStacktoRcard!=null)
            {
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_STACK_STORAGE_NOT_SAME"));
                return;
            }

            List<string> checkedPallet = new List<string>();
            for (int i = 0; i < this.grdRcard.Rows.Count; i++)
            {
                switch (this.grdRcard.Rows[i].Cells["inputtype"].Text)
                {
                    case packPallet:
                        if (!checkedPallet.Contains(this.grdRcard.Rows[i].Cells["palletcode"].Text))
                        {
                            if (this.CheckPalletInput(this.grdRcard.Rows[i].Cells["palletcode"].Text, false))
                            {
                                checkedPallet.Add(this.grdRcard.Rows[i].Cells["palletcode"].Text);
                            }
                            else
                            {
                                return;
                            }
                        }
                        break;
                    case packCarton:
                        if (!this.CheckCartonInput(this.grdRcard.Rows[i].Cells["cartoncode"].Text, false))
                        {
                            return;
                        }
                        break;
                    case packRcard:
                        if (!this.CheckRcardInput(this.grdRcard.Rows[i].Cells["rcard"].Text, false))
                        {
                            return;
                        }
                        break;
                }
            }

            ////Save info            
            objFacade.SaveInInventory(this.ucLabelComboxINVType.SelectedItemValue.ToString(),
                                      this.ucLabelEditStock.Value.Trim(),
                                      this.ucLabelComboxCompany.SelectedItemValue.ToString(),
                                      ApplicationService.Current().LoginInfo.UserCode,
                                      this.cboBusinessType.SelectedItemValue.ToString(),
                                      this.m_dtRcard);

            
            //表格中没有数据
            m_dtRcard.Clear();
            this.ucLabelEditQty.Value = "0";

            //更新剁位状态
            if (this.ucLabelEditStock.Value.Trim().Length > 0)
            {
                object objectStackMessage = objFacade.GetStackMessage(this.ucLabelEditStock.Value.Trim().ToUpper());
                if (objectStackMessage != null)
                {
                    this.ucLabelEditstackMessage.Value = ((StackMessage)objectStackMessage).StackQtyMessage;
                }
            }

            ApplicationRun.GetInfoForm().Add(
                new UserControl.Message(MessageType.Success, "$CS_IN_INV_SUCCESS"));


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.m_dtRcard != null)
            {
                this.m_dtRcard.Clear();
            }
            this.ucLabelEditQty.Value = "0";
            this.ucLabelEditInput.TextFocus(true, true);
        }

        private void opsetPackObject_Click(object sender, EventArgs e)
        {

        }

        private void ultraOptionSet1_Click(object sender, EventArgs e)
        {

        }

        private void FInInvByProduce_Load(object sender, EventArgs e)
        {
            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(this.grdRcard);

            this.opsetPackObject.Value = packPallet;
            this.ultraOptionSetAddDelete.Value = packAdd;
            this.ucLabelEditQty.Value = "0";
            this.ucLabelEditQty.InnerTextBox.TextAlign = HorizontalAlignment.Right;

            InitialBusinessType();
            this.InitializeComboBox();
            this.InitialDataTable();

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
            else
            {
                //Message:没有生产性入库记录
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_INV_BYPRODUCE_NO_RECORD"));
            }
            
        }

        private void grdRcard_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            //e.Layout.Override.AllowColSizing = AllowColSizing.None;
            e.Layout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            e.Layout.Bands[0].Columns["palletcode"].Header.Caption = "栈板号";
            e.Layout.Bands[0].Columns["palletcode"].Width = 200;
            e.Layout.Bands[0].Columns["palletcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["cartoncode"].Header.Caption = "箱号";
            e.Layout.Bands[0].Columns["cartoncode"].Width = 100;
            e.Layout.Bands[0].Columns["cartoncode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["rcard"].Header.Caption = "序列号";
            e.Layout.Bands[0].Columns["rcard"].Width = 200;
            e.Layout.Bands[0].Columns["rcard"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["mocode"].Header.Caption = "工单";
            e.Layout.Bands[0].Columns["mocode"].Width = 100;
            e.Layout.Bands[0].Columns["mocode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["itemcode"].Header.Caption = "产品";
            e.Layout.Bands[0].Columns["itemcode"].Width = 100;
            e.Layout.Bands[0].Columns["itemcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["itemdesc"].Header.Caption = "产品描述";
            e.Layout.Bands[0].Columns["itemdesc"].Width = 300;
            e.Layout.Bands[0].Columns["itemdesc"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["inputtype"].Header.Caption = "";
            e.Layout.Bands[0].Columns["inputtype"].Hidden = true;

            e.Layout.Bands[0].Columns["sscode"].Hidden = true;

            //this.InitGridLanguage(grdRcard);
        }

        private void InitialDataTable()
        {
            this.m_dtRcard = new DataTable();
            this.m_dtRcard.Columns.Add("palletcode");
            this.m_dtRcard.Columns.Add("cartoncode");
            this.m_dtRcard.Columns.Add("rcard");
            this.m_dtRcard.Columns.Add("mocode");
            this.m_dtRcard.Columns.Add("itemcode");
            this.m_dtRcard.Columns.Add("itemdesc");
            this.m_dtRcard.Columns.Add("inputtype");
            this.m_dtRcard.Columns.Add("sscode");

            this.grdRcard.DataSource = m_dtRcard;
        }

        /// <summary>
        /// 检查入库讯息是否存在
        /// </summary>
        /// <returns>true/false</returns>
        private bool CheckInStorageInfo()
        {
            if (this.ucLabelComboxINVType.SelectedItemText.Trim().Length == 0 ||
                this.ucLabelComboxCompany.SelectedItemText.Trim().Length == 0 ||
                this.ucLabelEditStock.Value.Trim().Length == 0 ||
                this.cboBusinessType.SelectedItemText.Trim().Length==0
                )
            {
                //Message:入库讯息设定不全
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_IN_STORAGE_INFO_ERROR"));
                return false;
            }
            
            return true;
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

        private void ucLabelEditInput_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.ucLabelEditInput.Value.Trim().Length == 0)
                {
                    this.ucLabelEditInput.TextFocus(true, true);
                    return;
                }

                if (!CheckInStorageInfo())
                {
                    return;
                }


                ////删除Grid
                //
                if (this.ultraOptionSetAddDelete.CheckedItem.DataValue.ToString().Equals(packDelete))
                {
                    if (this.grdRcard.Rows.Count == 0)
                    {
                        //表格中没有数据
                        ApplicationRun.GetInfoForm().Add(
                            new UserControl.Message(MessageType.Error, "$CS_GRID_NO_RECORD"));
                        this.ucLabelEditInput.TextFocus(true, true);
                        return;
                    }

                    if (RemoveGridRecord() > 0)
                    {
                        ////删除成功
                        //
                        ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Success, "$CS_Delete_Success"));
                        this.ucLabelEditInput.TextFocus(true, true);
                    }
                    else
                    {
                        ////删除成功
                        //
                        ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_NOT_EXIST_RECORD_DELETE"));
                        this.ucLabelEditInput.TextFocus(true, true);
                    }
                    
                    
                    return;
                }

                switch (this.opsetPackObject.CheckedItem.DataValue.ToString())
                {
                    case packPallet:
                        ////输入栈板
                        //
                        if (!CheckPalletInput(this.ucLabelEditInput.Value.Trim(),true))
                        {
                            this.ucLabelEditInput.TextFocus(true, true);
                            return;
                        }
                        break;
                    case packCarton:
                        ////输入箱号
                        //
                        if (!CheckCartonInput(this.ucLabelEditInput.Value.Trim(), true))
                        {
                            this.ucLabelEditInput.TextFocus(true, true);
                            return;
                        }
                        break;
                    case packRcard:
                        ////输入序列号
                        //
                        if (!CheckRcardInput(this.ucLabelEditInput.Value.Trim(), true))
                        {
                            this.ucLabelEditInput.TextFocus(true, true);
                            return;
                        }
                        break;
                    default:
                        break;
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
                }

                //添加成功
                ApplicationRun.GetInfoForm().Add(
                new UserControl.Message(MessageType.Success, "$CS_Add_Success"));


                if (opsetPackObject.Value == packCarton)
                {
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"));
                }

                if (opsetPackObject.Value == packPallet)
                {
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Normal, "$CS_Please_Input_PALLET"));
                }

                if (opsetPackObject.Value == packRcard)
                {
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"));
                }

                this.ucLabelEditInput.TextFocus(true, true);

            }
        }

        /// <summary>
        /// 判断Grid中是否已经存在不相同的ItemCode
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        private bool CheckExistItem(string itemCode)
        {
            for (int i = 0; i < this.grdRcard.Rows.Count; i++)
            {
                if (!itemCode.Equals(this.grdRcard.Rows[i].Cells["itemcode"].Text.Trim()))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckDuplicateInput(string inputValue,string inputType)
        {
            for (int i = 0; i < this.grdRcard.Rows.Count; i++)
            {
                switch (inputType)
                {
                    case packPallet:
                        if (inputValue.Equals(this.grdRcard.Rows[i].Cells["rcard"].Text.Trim()))
                        {
                            return true;
                        }
                        break;
                    case packCarton:
                        if (inputValue.Equals(this.grdRcard.Rows[i].Cells["cartoncode"].Text.Trim()))
                        {
                            return true;
                        }
                        break;
                    case packRcard:
                        if (inputValue.Equals(this.grdRcard.Rows[i].Cells["rcard"].Text.Trim()))
                        {
                            return true;
                        }
                        break;
                }
            }
            return false;
        }


        /// <summary>
        /// Remove record in grid when input barcode
        /// </summary>
        private int RemoveGridRecord()
        {
            int intNum = 0;
            for (int i = this.grdRcard.Rows.Count-1 ; i >= 0; i--)
            {
                switch (this.opsetPackObject.CheckedItem.DataValue.ToString())
                {
                    case packPallet:
                        if (this.grdRcard.Rows[i].Cells["palletcode"].Value.ToString().Equals(this.ucLabelEditInput.Value.Trim()))
                        {
                            this.grdRcard.Rows[i].Delete(false);
                            this.ucLabelEditQty.Value = Convert.ToString(Convert.ToInt32(this.ucLabelEditQty.Value) - 1);
                            intNum = intNum + 1;
                        }
                        break;
                    case packCarton:
                        if (this.grdRcard.Rows[i].Cells["cartoncode"].Value.ToString().Equals(this.ucLabelEditInput.Value.Trim()))
                        {
                            this.grdRcard.Rows[i].Delete(false);
                            this.ucLabelEditQty.Value = Convert.ToString(Convert.ToInt32(this.ucLabelEditQty.Value) - 1);
                            intNum = intNum + 1;
                        }
                        break;
                    case packRcard:
                        if (this.grdRcard.Rows[i].Cells["rcard"].Value.ToString().Equals(this.ucLabelEditInput.Value.Trim()))
                        {
                            this.grdRcard.Rows[i].Delete(false);
                            this.ucLabelEditQty.Value = Convert.ToString(Convert.ToInt32(this.ucLabelEditQty.Value) - 1);
                            intNum = intNum + 1;
                        }
                        break;
                    default:
                        break;
                }
            }

            return intNum;
        }
 
        private bool CheckPalletInput(string palletCode,bool needAddToGrid)
        {

            //if (!this.CheckSelecetedStackAndStorage(this.SelectedStorage, this.SelectedStack, Convert.ToString(this.ucLabelComboxINVType.SelectedItemValue)))
            //{
            //    return false;
            //}
            if (Convert.ToString(this.ucLabelComboxINVType.SelectedItemValue).Trim().Length == 0)
            {
                //请输入库位
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_STORAGE_NOT_INPUT"));
                return false;
            }

            if (!this.SelectedStorage.Equals(Convert.ToString(this.ucLabelComboxINVType.SelectedItemValue)))
            {
                InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
                int stack2RCardCount = inventoryFacade.GetStack2RCardCount(this.SelectedStack, Convert.ToString(this.ucLabelComboxINVType.SelectedItemValue));

                if (stack2RCardCount > 0)
                {
                    //垛位和库别不对应
                    ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_STACK_STORAGE_NOT_SAME"));
                    return false;
                }
            }

            PackageFacade objPFFacade = new PackageFacade(this.DataProvider);
            //判断栈板号是否存在
            object[] objPallet2RCardS = objPFFacade.GetPallet2RCardListByPallet(palletCode);
            if (objPallet2RCardS == null)
            {
                //该栈板号不存在
                ApplicationRun.GetInfoForm().Add(
                new UserControl.Message(MessageType.Error, "$CS_PALLETNO_IS_NOT_EXIT"));
                return false;
                
            }

            foreach (Pallet2RCard pallet2RCard in objPallet2RCardS)
            {
                string strRcard = pallet2RCard.RCard;

                InventoryFacade objInvFacade = new InventoryFacade(this.DataProvider);
                DataTable dtInfo = objInvFacade.GetSimulationReportInfo(strRcard, packRcard);

                if (dtInfo == null)
                {
                    //该序列号不存在
                    ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_RCARD_IS_NOT_EXIT"));
                    return false;
                }

                if (!CheckRule(strRcard))
                {
                    return false;
                }

                //foreach (DataRow dr in dtInfo.Rows)
                DataRow dr = dtInfo.Rows[0];//{
                if (!this.CheckStackAndRcardInfo(strRcard, this.ucLabelEditStock.Value.Trim(), dr["itemcode"].ToString(), palletCode,this.ucLabelComboxCompany.SelectedItemValue.ToString(),this.ucLabelComboxINVType.SelectedItemValue.ToString()))
                {
                    return false;
                }
                else
                {
                    if (needAddToGrid)
                    {
                        if (CheckDuplicateInput(dr["rcard"].ToString(), packRcard))
                        {
                            //重复输入
                            ApplicationRun.GetInfoForm().Add(
                            new UserControl.Message(MessageType.Error, "$CS_IDRepeatCollect $CS_Param_RunSeq=" + dr["rcard"].ToString()));
                            continue;
                        }

                        if (objInvFacade.CheckStackIsOnlyAllowOneItem(this.ucLabelEditStock.Value.Trim()) && CheckExistItem(dr["itemcode"].ToString()))
                        {
                            ApplicationRun.GetInfoForm().Add(
                            new UserControl.Message(MessageType.Error, "$CS_ONLY_ONE_ITEM_IN_STACK"));
                            return false;
                        }


                        DataRow drNew = this.m_dtRcard.NewRow();
                        drNew["palletcode"] = palletCode;
                        drNew["cartoncode"] = dr["cartoncode"];
                        drNew["rcard"] = dr["rcard"];
                        drNew["mocode"] = dr["mocode"];
                        drNew["itemcode"] = dr["itemcode"];
                        drNew["itemdesc"] = dr["itemdesc"];
                        drNew["inputtype"] = packPallet;
                        drNew["sscode"] = dr["sscode"];

                        this.m_dtRcard.Rows.Add(drNew);

                        this.ucLabelEditQty.Value = Convert.ToString(Convert.ToInt32(this.ucLabelEditQty.Value) + 1);
                    }
                }
                //}
            }
            
            return true;
        }

        private bool CheckCartonInput(string cartonCode,bool needAddToGrid)
        {            
            if (!this.CheckSelecetedStackAndStorage(this.SelectedStorage, this.SelectedStack, Convert.ToString(this.ucLabelComboxINVType.SelectedItemValue)))
            {
                return false;
            }

            InventoryFacade objInvFacade = new InventoryFacade(this.DataProvider);
            DataTable dtInfo = objInvFacade.GetSimulationReportInfo(cartonCode,packCarton);

            if (dtInfo.Rows.Count == 0)
            {
                //该箱号不存在
                ApplicationRun.GetInfoForm().Add(
                new UserControl.Message(MessageType.Error, "$CS_CARTON_IS_NOT_EXIT"));
                return false;
            }

            if (!CheckRule(cartonCode))
            {
                return false;
            }

            foreach (DataRow dr in dtInfo.Rows)
            {
                PackageFacade pf = new PackageFacade(this.DataProvider);
                object objPalletRcrad = pf.GetPallet2RCardByRCard(dr["rcard"].ToString());
                string palletCode = "";
                if (objPalletRcrad != null)
                {
                    palletCode = ((Pallet2RCard)objPalletRcrad).PalletCode;

                    //箱号对应的序列号存在栈板，请以栈板号来进行入库
                    //ApplicationRun.GetInfoForm().Add(
                    //new UserControl.Message(MessageType.Error, "$CS_CARTON_RCARD_EXIST_PALLET=" + palletCode));
                    //return false;
                }

                if (!this.CheckStackAndRcardInfo(dr["rcard"].ToString(), this.ucLabelEditStock.Value.Trim(), dr["itemcode"].ToString(), palletCode, this.ucLabelComboxCompany.SelectedItemValue.ToString(), this.ucLabelComboxINVType.SelectedItemValue.ToString()))
                {
                    return false;
                }
                else
                {
                    if (needAddToGrid)
                    {
                        if (CheckDuplicateInput(dr["rcard"].ToString(), packRcard))
                        {
                            //重复输入
                            ApplicationRun.GetInfoForm().Add(
                            new UserControl.Message(MessageType.Error, "$CS_IDRepeatCollect $CS_Param_RunSeq=" + dr["rcard"].ToString()));
                            continue;
                        }
                        
                        
                        if (objInvFacade.CheckStackIsOnlyAllowOneItem(this.ucLabelEditStock.Value.Trim()) && CheckExistItem(dr["itemcode"].ToString()))
                        {
                            ApplicationRun.GetInfoForm().Add(
                            new UserControl.Message(MessageType.Error, "$CS_ONLY_ONE_ITEM_IN_STACK"));
                            return false;
                        }
                        
                        
                        DataRow drNew = this.m_dtRcard.NewRow();
                        drNew["palletcode"] = palletCode;
                        drNew["cartoncode"] = dr["cartoncode"];
                        drNew["rcard"] = dr["rcard"];
                        drNew["mocode"] = dr["mocode"];
                        drNew["itemcode"] = dr["itemcode"];
                        drNew["itemdesc"] = dr["itemdesc"];
                        drNew["inputtype"] = packCarton;
                        drNew["sscode"] = dr["sscode"];
                        this.m_dtRcard.Rows.Add(drNew);
                        this.ucLabelEditQty.Value = Convert.ToString(Convert.ToInt32(this.ucLabelEditQty.Value) + 1);
                    }
                }
            }

            return true;
        }

        private bool CheckRcardInput(string rcard, bool needAddToGrid)
        {            
            if (!this.CheckSelecetedStackAndStorage(this.SelectedStorage, this.SelectedStack, Convert.ToString(this.ucLabelComboxINVType.SelectedItemValue)))
            {
                return false;
            }

            InventoryFacade objInvFacade = new InventoryFacade(this.DataProvider);
            DataTable dtInfo = objInvFacade.GetSimulationReportInfo(rcard,packRcard);

            if (dtInfo.Rows.Count == 0)
            {
                //该序列号不存在
                ApplicationRun.GetInfoForm().Add(
                new UserControl.Message(MessageType.Error, "$CS_RCARD_IS_NOT_EXIT"));
                return false;
            }

            if (!CheckRule(rcard))
            {
                return false;
            }

            if (!CheckRcardIsInTheSameStack(rcard,this.ucLabelEditStock.Value))
            {                
                return false;
            }

            PackageFacade pf = new PackageFacade(this.DataProvider);
            object objPalletRcrad = pf.GetPallet2RCardByRCard(rcard);
            string palletCode = "";
            if (objPalletRcrad != null)
            {
                palletCode = ((Pallet2RCard)objPalletRcrad).PalletCode;

                //序列号存在栈板，请以栈板号来进行入库
                //ApplicationRun.GetInfoForm().Add(
                //new UserControl.Message(MessageType.Error, "$CS_RCARD_EXIST_PALLET=" + palletCode));
                //return false;
            }


            //foreach (DataRow dr in dtInfo.Rows)
            //{
            DataRow dr = dtInfo.Rows[0];
            if (!this.CheckStackAndRcardInfo(dr["rcard"].ToString(), this.ucLabelEditStock.Value.Trim(), dr["itemcode"].ToString(), palletCode, this.ucLabelComboxCompany.SelectedItemValue.ToString(), this.ucLabelComboxINVType.SelectedItemValue.ToString()))
            {
                return false;
            }
            else
            {
                if (needAddToGrid)
                {
                    if (CheckDuplicateInput(rcard, packRcard))
                    {
                        //重复输入
                        ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_IDRepeatCollect $CS_Param_RunSeq=" + dr["rcard"].ToString()));
                        return false;
                    }
                    
                    
                    if (objInvFacade.CheckStackIsOnlyAllowOneItem(this.ucLabelEditStock.Value.Trim()) && CheckExistItem(dr["itemcode"].ToString()))
                    {
                        ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_ONLY_ONE_ITEM_IN_STACK"));
                        return false;
                    }
                    
                    DataRow drNew = this.m_dtRcard.NewRow();
                    drNew["palletcode"] = palletCode;
                    drNew["cartoncode"] = dr["cartoncode"];
                    drNew["rcard"] = dr["rcard"];
                    drNew["mocode"] = dr["mocode"];
                    drNew["itemcode"] = dr["itemcode"];
                    drNew["itemdesc"] = dr["itemdesc"];
                    drNew["inputtype"] = packRcard;
                    drNew["sscode"] = dr["sscode"];
                    this.m_dtRcard.Rows.Add(drNew);
                    this.ucLabelEditQty.Value = Convert.ToString(Convert.ToInt32(this.ucLabelEditQty.Value) + 1);
                }
            }
            //}

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
                    if (!invFacade.CheckRcardIsFinished(inputNo))
                    {
                        //Message:产品序列号未完工
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RCARD_IS_NOT_FINISHED $CS_Param_RunSeq=" + inputNo));
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 成品入库检查
        /// </summary>
        /// <returns>true/false</returns>
        private bool CheckStackAndRcardInfo(string rcard, string stackCode, string itemCode, string palletCode, string companyCode,string storageCode)
        {

            ////Check从垛位使用状况的页面带过来的垛位和库位
            //if (!this.CheckSelecetedStackAndStorage(this.SelectedStorage, this.SelectedStack, Convert.ToString(this.ucLabelComboxINVType.SelectedItemValue)))
            //{
            //    return false;
            //}
            //else
            //{
                //Check序列号对应的料号是否和垛位的不一样
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            object[] objStackToRcards = inventoryFacade.GetAnyStack2RCardByStack(stackCode);

            if (objStackToRcards != null)
            {
                if (inventoryFacade.CheckStackIsOnlyAllowOneItem(stackCode) && !((StackToRcard)objStackToRcards[0]).ItemCode.Equals(itemCode))
                {
                    //垛位的料号和当前产品的料号不一至
                    //ucMessage1.AddEx(this._FunctionName, this.opsetPackObject.CheckedItem.DisplayText + ": " + this.txtRCard.Value + ";料号:" + this.txtItemCode.Value, new UserControl.Message(MessageType.Error, "$CS_STACK_AND_PRODUCT_ITEM_NOT_SAME"), true);
                    ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_STACK_AND_PRODUCT_ITEM_NOT_SAME $ITEM_CODE=" + itemCode));
                    return false;
                }
            }

            //Check 序列号是否已经入过库
            object objStarckToRcard = inventoryFacade.GetStackToRcard(rcard);

            if (objStarckToRcard != null)
            {
                //序列号重复入库
                //ucMessage1.AddEx(this._FunctionName, "序列号" + ": " + this.txtRCard.Value, new UserControl.Message(MessageType.Error, "$CS_SERIAL_EXIST"), true);
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_SERIAL_EXIST $SERIAL_NO=" + rcard));
                return false;
            }
            //}


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

                    //if (obj.ItemGrade != itemGrade)
                    //{
                    //    //ApplicationRun.GetInfoForm().Add(
                    //    //new UserControl.Message(MessageType.Error, "$CS_RCARD_EXIST_IN_PALLET" + ((Pallet2RCard)objPallet2RCard).PalletCode));
                    //    //return false;
                    //    strErrorMessage = strErrorMessage + "itemgrade";
                    //    ApplicationRun.GetInfoForm().Add(
                    //    new UserControl.Message(MessageType.Error, "$CS_ITEMGRADE_NOT_SAME_IN_PALLET=" + obj.ItemGrade));
                    //}

                    if (strErrorMessage != string.Empty)
                    {
                        return false;
                    }
                }
                else
                {
                    if (!inventoryFacade.CheckStackCapacity(storageCode,stackCode))
                    {
                        ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_STACK_CAPACITY_FULL"));
                        return false;
                    }
                }
            }

            return true;
        }

        //检查此Rcard所属栈板下Rcard在同一垛位下
        private bool CheckRcardIsInTheSameStack(string rcard, string stackCode)
        {
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            PackageFacade packageFacade = new PackageFacade(this.DataProvider);
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);

            Pallet2RCard pallet2RCard = (Pallet2RCard)packageFacade.GetPallet2RCardByRCard(rcard);

            if (pallet2RCard == null)
            {
                return true;
            }

            object[] pallet2RCardList = packageFacade.GetPallet2RCardListByPallet(pallet2RCard.PalletCode);
            if (pallet2RCardList == null)
            {
                return true;
            }

            for (int i = 0; i < pallet2RCardList.Length; i++)
            {
                string cartonCode = string.Empty;
                SimulationReport simulationReport = (SimulationReport)dataCollectFacade.GetLastSimulationReport(((Pallet2RCard)pallet2RCardList[i]).RCard);
                if (simulationReport != null)
                {
                    cartonCode = simulationReport.CartonCode;
                }

                object[] stack2RcardList = inventoryFacade.QueryStacktoRcardByRcardAndCarton(((Pallet2RCard)pallet2RCardList[i]).RCard, cartonCode);
                if (stack2RcardList != null && !((StackToRcard)stack2RcardList[0]).StackCode.Equals(stackCode))
                {
                    ApplicationRun.GetInfoForm().Add(
                new UserControl.Message(MessageType.Error, "$CS_RCARD_IS_NOT_INSAME_STACK:" + ((StackToRcard)stack2RcardList[0]).StackCode));
                    return false;
                }
            }

            return true;
        }

        private void opsetPackObject_ValueChanged(object sender, EventArgs e)
        {
            if (opsetPackObject.Value == packCarton)
            {
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"));
                this.ucLabelEditInput.TextFocus(false, true);
            }

            if (opsetPackObject.Value == packPallet)
            {
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Normal, "$CS_Please_Input_PALLET"));
                this.ucLabelEditInput.TextFocus(false, true);
            }

            if (opsetPackObject.Value == packRcard)
            {
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"));
                this.ucLabelEditInput.TextFocus(false, true);
            }
        }

        private void ultraOptionSetAddDelete_ValueChanged(object sender, EventArgs e)
        {
            this.ucLabelEditInput.TextFocus(false, true);
        }
    }
}