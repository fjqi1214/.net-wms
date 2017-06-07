﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.PDAClient.Service;
using UserControl;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Package;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.Domain.LotPackage;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.LotDataCollect;
using BenQGuru.eMES.Domain.LotDataCollect;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.OQC;

namespace BenQGuru.eMES.PDAClient
{
    public partial class FLotCollectionCarton : FormBase
    {
        private int m_FlowControl = 1;
        private int m_CartonCapacity = 0;
        private bool m_CartonIsFull = false;
        private string m_FunctionName = "FLotCollectionCarton";
        private DataCollectFacade m_DataCollectFacade;
        private PackageFacade m_PackageFacade;
        private ItemFacade m_ItemFacade;
        private ItemLotFacade m_ItemLotFacade;
        private CartonCollection m_CartonCollection;
        private Carton2Lot[] m_Carton2Lot;
        private DataTable m_DataTable = new DataTable();

        private IDomainDataProvider m_DomainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return m_DomainDataProvider;
            }
        }

        public FLotCollectionCarton()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
            this.SetUltraGrid();
            this.m_FlowControl = 1;
            //this.SetInputMessageByFlowControl();
        }

        private void FLotCollectionCarton_Load(object sender, EventArgs e)
        {
            //this._FunctionName = this.Text;
            //ucMessage.AddEx(this._FunctionName, "", new UserControl.Message("$CS_Please_Input_CartonNo"), false);
            //this.ucLabelCartonNo.TextFocus(false, true);
            m_DataCollectFacade = new DataCollectFacade(this.DataProvider);
            m_PackageFacade = new PackageFacade(this.DataProvider);
            m_ItemFacade = new ItemFacade(this.DataProvider);
            m_ItemLotFacade = new ItemLotFacade(this.DataProvider);
            this.InitPageLanguage();
        }

        private void SetUltraGrid()
        {
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
        }

        private void SetInputMessageByFlowControl()
        {
            if (m_FlowControl == 1)        //请输入Carton箱号
            {
                //ShowMessage(">>$CS_Please_Input_CartonNo");
                this.ucLabelCartonNo.TextFocus(false, true);
            }
            else if (m_FlowControl == 2)   //请输入产品批次条码
            {
                //ShowMessage(">>$CS_Please_Input_LotCode");
                this.ucLabelLotCodeForCarton.TextFocus(false, true);
            }
            else if (m_FlowControl == 3)   //请再输入产品批次条码
            {
                //ShowMessage(">>$CS_Please_Input_LotCode_Again");
                this.ucLabelLotCodeForCarton.TextFocus(false, true);
            }
        }

        private void ucLabelCartonNo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                {
                    //ucMessage.AddWithoutEnter("<<");
                    //ucMessage.AddBoldText(ucLabelCartonNo.Value.Trim());
                    if (this.ucLabelCartonNo.Value.Trim() == "")
                    {
                        m_FlowControl = 1;
                        this.SetInputMessageByFlowControl();
                        return;
                    }

                    string cartonNo = FormatHelper.CleanString(ucLabelCartonNo.Value.Trim().ToUpper());
                    if (!CheckCarton(cartonNo))
                    {
                        this.GetData(cartonNo);
                        this.SetUCLabelValue(m_CartonCollection);
                        this.InitializeGrid(m_Carton2Lot);
                        this.ucLabelCartonNo.TextFocus(false, true);
                        return;
                    }
                    this.GetData(cartonNo);
                    this.SetUCLabelValue(this.m_CartonCollection);
                    this.InitializeGrid(this.m_Carton2Lot);
                    this.m_FlowControl = 2;
                    //this.SetInputMessageByFlowControl();
                    this.ucLabelLotCodeForCarton.TextFocus(false, true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                this.m_FlowControl = 1;
                this.SetInputMessageByFlowControl();
            }
        }

        //检查箱号及是否已满
        private bool CheckCarton(string cartonNo)
        {
            #region Carton Length Check
            //if (chkCartonLen.Checked && chkCartonLen.Value.Trim().Length > 0)
            //{
            //    if (cartonNo.Length != Convert.ToInt32(chkCartonLen.Value.Replace("-", "")))
            //    {
            //        ucMessage.AddEx(this.m_FunctionName, "包装箱号: " + this.ucLabelCartonNo.Value, new UserControl.Message(MessageType.Error, "$CS_CARTON_NO_LEN_CHECK_FAIL"), false);
            //        this.m_FlowControl = 1;
            //        this.SetInputMessageByFlowControl();
            //        return false;
            //    }
            //}
            #endregion

            #region Carton First Char Check
            //if (chkCartonFChar.Checked && chkCartonFChar.Value.Trim().Length > 0)
            //{
            //    if (cartonNo.IndexOf(chkCartonFChar.Value.Trim()) != 0)
            //    {
            //        ucMessage.AddEx(this.m_FunctionName, "包装箱号: " + this.ucLabelCartonNo.Value, new UserControl.Message(MessageType.Error, "$CS_CARTON_NO_FCHAR_CHECK_FAIL"), false);
            //        this.m_FlowControl = 1;
            //        this.SetInputMessageByFlowControl();
            //        return false;
            //    }
            //}
            #endregion

            #region 判断是否箱中已满
            
            CARTONINFO objCartonInfo = m_PackageFacade.GetCARTONINFO(cartonNo) as CARTONINFO;
            if (objCartonInfo != null)
            {
                if (objCartonInfo.CAPACITY == objCartonInfo.COLLECTED)
                {
                    ShowMessage("$CARTON_ALREADY_FILL_OUT");
                    this.m_FlowControl = 1;
                    this.SetInputMessageByFlowControl();
                    return false;
                }
            }
            #endregion

            #region 判断箱是否已经送检
            if (objCartonInfo != null)
            {
                //OQCFacade _OQCFacade = new OQCFacade(this.DataProvider); 
                //object obj = _OQCFacade.GetLot2CartonByCartonNo(cartonNo);
                //if (obj != null)
                //{
                //    ucMessage.AddEx(this._FunctionName, "包装箱号: " + this.ucLabelCartonNo.Value, new UserControl.Message(MessageType.Error, "$CARTON_ALREADY_OQC"), false);
                //    this.m_FlowControl = 1;
                //    this.SetInputMessageByFlowControl();
                //    return false;
                //}
            }
            #endregion

            return true;

        }

        private void ucLabelLotCodeForCarton_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (CartonPack())
                {
                    this.GetData(FormatHelper.CleanString(ucLabelCartonNo.Value.Trim().ToUpper()));
                    this.SetUCLabelValue(this.m_CartonCollection);
                    this.InitializeGrid(this.m_Carton2Lot);
                    if (this.m_CartonCollection.CAPACITY == decimal.Parse(this.ucLabelCartonCollected.Value))
                    {
                        this.ucLabelCartonNo.TextFocus(false, true);
                    }
                    //else
                    //{
                    //    this.ucLabelLotCodeForCarton.TextFocus(false, true);
                    //}
                }
            }
        }

        private bool CartonPack()
        {
            if (this.ucLabelLotCodeForCarton.Value.Trim() == "")
            {
                this.SetInputMessageByFlowControl();
                return false;
            }
            string lotCode = FormatHelper.CleanString(this.ucLabelLotCodeForCarton.Value.Trim().ToUpper());

            if (this.ucLabelCartonNo.Value.Trim() == "")
            {
                m_FlowControl = 1;
                this.SetInputMessageByFlowControl();
                return false;
            }

            #region LotCode Length Check
            //if (chkLotCodeLen.Checked && chkLotCodeLen.Value.Trim().Length > 0)
            //{
            //    if (lotCode.Length != Convert.ToInt32(chkLotCodeLen.Value.Replace("-", "")))
            //    {
            //        ucMessage.AddEx(this.m_FunctionName, "批次条码: " + this.ucLabelLotCodeForCarton.Value, new UserControl.Message(MessageType.Error, "$CS_LOT_NO_LEN_CHECK_FAIL"), false);
            //        this.m_FlowControl = 3;
            //        this.SetInputMessageByFlowControl();
            //        return false;
            //    }
            //}
            #endregion

            #region LotCode First Char Check
            //if (chkLotCodeFChar.Checked && chkLotCodeFChar.Value.Trim().Length > 0)
            //{
            //    if (lotCode.IndexOf(chkLotCodeFChar.Value.Trim()) != 0)
            //    {
            //        ucMessage.AddEx(this.m_FunctionName, "批次条码: " + this.ucLabelLotCodeForCarton.Value, new UserControl.Message(MessageType.Error, "$CS_LOT_NO_FCHAR_CHECK_FAIL"), false);
            //        this.m_FlowControl = 3;
            //        this.SetInputMessageByFlowControl();
            //        return false;
            //    }
            //}
            #endregion

            //ucMessage.AddWithoutEnter("<<");
            //ucMessage.AddBoldText(ucLabelLotCodeForCarton.Value.Trim());

            if (!CheckLot(lotCode))
            {
                this.m_FlowControl = 3;
                this.SetInputMessageByFlowControl();
                return false;
            }

            LotSimulationReport objSimulation = (m_DataCollectFacade.QueryLotSimulationReport(lotCode))[0] as LotSimulationReport;
            Item item = m_ItemFacade.GetItem(objSimulation.ItemCode.ToUpper().Trim(), GlobalVariables.CurrentOrganizations.First().OrganizationID) as Item;
            string newMOCode = objSimulation.MOCode.Trim().ToUpper();

            string cartonNo = FormatHelper.CleanString(ucLabelCartonNo.Value.Trim().ToUpper());
            //判断序列号是否已经装过箱，应该装在之前的箱中
            //OQCFacade _OQCFacade = new OQCFacade(this.DataProvider);
            //object lot2Card = _OQCFacade.GetOQCLot2Card(sourceCard.Trim().ToUpper(), newMOCode, "", "");
            //if(lot2Card != null)
            //{
            //    if ((!String.IsNullOrEmpty(((OQCLot2Card)lot2Card).EAttribute1)) && cartonNo != ((OQCLot2Card)lot2Card).EAttribute1.ToString())
            //    {
            //        ucMessage.AddEx(this._FunctionName, " ", new UserControl.Message(MessageType.Error, "$RCard:" + sourceCard + "$ShouedPackInCarton:" + ((OQCLot2Card)lot2Card).EAttribute1.ToString()), false);
            //        this.m_FlowControl = 3;
            //        this.SetInputMessageByFlowControl();
            //        return false;
            //    }
            //}

            #region check箱            
            if (!CheckCarton(cartonNo))
            {
                return false;
            }

            //check产品序列号是否重复包装
            decimal cartonQty = m_PackageFacade.SumCartonQty(lotCode.Trim().ToUpper());
            if (cartonQty >= objSimulation.LotQty)
            {
                ShowMessage("$CS_LOT_HAS_PACKED");
                this.m_FlowControl = 3;
                this.SetInputMessageByFlowControl();
                return false;
            }

            //check一个箱子只能放一个类型的产品,并且只能放一个工单的产品
            object[] objects = m_PackageFacade.GetCarton2LotByCartonNO(cartonNo);
            if (objects != null)
            {
                string oldMOCode = ((Carton2Lot)objects[0]).MOCode.Trim().ToUpper();
                if (newMOCode != oldMOCode)
                {
                    ShowMessage("$OneCarton_OneMoCode");
                    this.m_FlowControl = 3;
                    this.SetInputMessageByFlowControl();
                    return false;
                }
                object obj = m_PackageFacade.GetItemCodeByMOCode(oldMOCode);
                if (obj != null)
                {
                    if (item.ItemCode != ((CartonCollection)obj).ItemCode)
                    {
                        ShowMessage("$OneCarton_OneProduct");
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

        //检查批次信息，是否已生产完工，是否存在的产品代码
        private bool CheckLot(string lotCode)
        {            
            // Get Simulation Info
            object[] objSimulations = m_DataCollectFacade.QueryLotSimulationReport(lotCode);
            if (objSimulations == null)
            {
                ShowMessage("$LotNoProductInfo");
                return false;
            }

            LotSimulationReport objSimulation = objSimulations[0] as LotSimulationReport;
            if (!objSimulation.IsComplete.Equals("1"))//未完工
            {
                ShowMessage("$LotIsUnfinished");
                return false;
            }
            if (!objSimulation.LotStatus.Equals(LotStatusForMO2LotLink.LOTSTATUS_USE))//使用的批才可包装
            {
                ShowMessage("$LotIsInvalid");
                return false;
            }

            // Get Item Info
            object item = m_ItemFacade.GetItem(objSimulation.ItemCode.ToUpper().Trim(), GlobalVariables.CurrentOrganizations.First().OrganizationID);
            if (item == null)
            {
                ShowMessage("$Error_ItemCode_NotExist $Domain_Item=" + objSimulation.ItemCode);
                return false;
            }
            m_CartonCapacity = (item as Item).ItemCartonQty;

            return true;
        }

        //包装操作
        private void AfterToSave()
        {
            // 包装动作
            string lotCode = FormatHelper.CleanString(this.ucLabelLotCodeForCarton.Value.Trim().ToUpper());
            if (lotCode.Length > 0)
            {
                DoAction(lotCode);
            }
        }

        //包装处理
        private void DoAction(string lotCode)
        {
            bool isSuccess = false;
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            DataProvider.BeginTransaction();
            try
            {
                isSuccess = AddSingleIDIntoCartonAndTry(lotCode);
                if (!isSuccess)
                {
                    this.DataProvider.RollbackTransaction();
                    InitMessage(lotCode, false);
                    this.SetInputMessageByFlowControl();
                    if (m_FlowControl == 1)
                    {
                        this.ucLabelCartonNo.TextFocus(false, true);
                    }
                    else
                    {
                        this.ucLabelLotCodeForCarton.TextFocus(false, true);
                    }
                }
                else
                {
                    this.DataProvider.CommitTransaction();
                    InitMessage(lotCode, true);
                    //this.SetInputMessageByFlowControl();
                    if (m_CartonIsFull)
                    {
                        this.ucLabelCartonNo.TextFocus(false, true);
                    }
                    else
                    {
                        this.ucLabelLotCodeForCarton.TextFocus(false, true);
                    }
                }
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                //ApplicationRun.GetInfoForm().AddEx(ex.Message);
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                //this.ucMessage.AddEx(this.m_FunctionName, "" + this.ucLabelLotCodeForCarton.Value, messages, false);
                //if (!isSuccess)
                //{
                //    this.ucLabelLotCodeForCarton.TextFocus(false, true);
                //}
            }

        }

        //批次装箱
        private bool AddSingleIDIntoCartonAndTry(string lotCode)
        {
            string cartonNo = FormatHelper.CleanString(this.ucLabelCartonNo.Value);
            m_FlowControl = 2;
            //Messages messages = new Messages();
            LotSimulationReport objSimulation = (m_DataCollectFacade.QueryLotSimulationReport(lotCode))[0] as LotSimulationReport;
            //if (messages.IsSuccess())
            //{
                if (cartonNo != string.Empty)
                {
                    object objCarton = m_PackageFacade.GetCARTONINFO(cartonNo);

                    if (objCarton != null)
                    {
                        CARTONINFO carton = objCarton as CARTONINFO;
                        if (carton.CAPACITY == carton.COLLECTED + objSimulation.LotQty)
                        {
                            ShowMessage("$CARTON_ALREADY_FULL_PlEASE_CHANGE");
                            m_CartonIsFull = true;
                            m_FlowControl = 1;
                        }
                        else
                        {
                            m_CartonIsFull = false;
                        }
                        if (carton.CAPACITY < carton.COLLECTED + objSimulation.LotQty)//是否可容纳该批
                        {
                            ShowMessage("$CARTON_CAPACITY_NOT_ENOUGH");
                            m_FlowControl = 3;
                            return false;
                        }
                        if (carton.CAPACITY <= carton.COLLECTED)
                        {
                            ShowMessage("$CARTON_ALREADY_FILL_OUT");
                            m_FlowControl = 1;
                            return false;
                        }
                        else
                        {
                            m_PackageFacade.UpdateCollected((carton as CARTONINFO).CARTONNO, objSimulation.LotQty);
                        }
                    }
                    else if (cartonNo != String.Empty)
                    {
                        object objExistCTN = m_PackageFacade.GetExistCARTONINFO(cartonNo);

                        if (objExistCTN != null)
                        {
                            ShowMessage("$CARTON_ALREADY_FULL_PlEASE_CHANGE");
                            m_FlowControl = 1;
                            return false;
                        }
                        else
                        {
                            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                            CARTONINFO carton = new CARTONINFO();
                            carton.CAPACITY = m_CartonCapacity;
                            carton.COLLECTED = objSimulation.LotQty;                            
                            carton.PKCARTONID = System.Guid.NewGuid().ToString().ToUpper();
                            carton.CARTONNO = cartonNo;
                            carton.MUSER = ApplicationService.Current().UserCode;
                            carton.EATTRIBUTE1 = "";
                            carton.MDATE = dbDateTime.DBDate;
                            carton.MTIME = dbDateTime.DBTime;

                            if (carton.CAPACITY < carton.COLLECTED)//是否可容纳该批
                            {
                                ShowMessage("$CARTON_CAPACITY_NOT_ENOUGH");
                                m_FlowControl = 3;
                                return false;
                            }
                            if (carton.CAPACITY == 0)
                            {
                                ShowMessage("$CS_PLEASE_MAINTEIN_ITEMCARTON $CS_Param_Lot =" + lotCode);
                                m_FlowControl = 3;
                                return false;
                            }
                            else
                            {
                                if (carton.CAPACITY == carton.COLLECTED)
                                {
                                    ShowMessage("$CARTON_ALREADY_FULL_PlEASE_CHANGE");
                                    m_CartonIsFull = true;
                                    m_FlowControl = 1;
                                }
                                else
                                {
                                    m_CartonIsFull = false;
                                }
                                m_PackageFacade.AddCARTONINFO(carton);
                            }
                        }
                    }

                }
            //}
            //if (messages.IsSuccess())
            //{
                m_PackageFacade.AddCarton2Lot(cartonNo, lotCode, objSimulation.LotQty, ApplicationService.Current().UserCode, objSimulation.MOCode);
                //记log
                m_PackageFacade.AddCarton2LotLog(cartonNo, lotCode, objSimulation.LotQty, ApplicationService.Current().UserCode);
            //}

            return true;
        }

        private void InitMessage(string lotCode, bool result)
        {
            if (result)
            {
                //ShowMessage(lotCode + " $CS_Add_Success");
                this.ucLabelLotCodeForCarton.Value = "";
                //ApplicationRun.GetQtyForm().RefreshQty();
            }
            else
            {
                //ShowMessage(lotCode + " $CS_Add_Fail");
                //this.m_FlowControl = 3;
                //this.SetInputMessageByFlowControl();
            }

        }

        //获得箱的相关信息
        public void GetData(string cartonNo)
        {
            CARTONINFO objCartonInfo = m_PackageFacade.GetCARTONINFO(cartonNo) as CARTONINFO;
            if (objCartonInfo != null)
            {
                this.m_CartonCollection = new CartonCollection();
                this.m_CartonCollection.CAPACITY = objCartonInfo.CAPACITY;
                this.m_CartonCollection.COLLECTED = objCartonInfo.COLLECTED;

                object[] objs = m_PackageFacade.GetCarton2LotByCartonNO(cartonNo);
                if (objs != null)
                {
                    this.m_Carton2Lot = new Carton2Lot[objs.Length];
                    for (int i = 0; i < objs.Length; i++)
                    {
                        this.m_Carton2Lot[i] = (Carton2Lot)objs[i];
                    }
                }
                if (this.m_Carton2Lot != null)
                {
                    this.m_CartonCollection.MOCode = this.m_Carton2Lot[0].MOCode;
                    object obj = m_PackageFacade.GetItemCodeByMOCode(this.m_Carton2Lot[0].MOCode);
                    if (obj != null)
                    {
                        this.m_CartonCollection.ItemCode = ((CartonCollection)obj).ItemCode;
                        this.m_CartonCollection.ItemDescription = ((CartonCollection)obj).ItemDescription;
                        Item2LotCheck item2LotCheck = (Item2LotCheck)m_ItemLotFacade.GetItem2LotCheck(((CartonCollection)obj).ItemCode);
                        if (item2LotCheck != null)
                        {
                            //if (item2LotCheck.SNLength > 0)
                            //{
                            //    chkLotCodeLen.Checked = true;
                            //    chkLotCodeLen.Value = item2LotCheck.SNLength.ToString();
                            //}

                            //if (!string.IsNullOrEmpty(item2LotCheck.SNPrefix))
                            //{
                            //    chkLotCodeFChar.Checked = true;
                            //    chkLotCodeFChar.Value = item2LotCheck.SNPrefix;
                            //}
                        }
                    }
                }
            }
        }

        //设置需要显示的信息
        public void SetUCLabelValue(CartonCollection cartonCollection)
        {
            if (cartonCollection == null)
            {
                this.ucLabelItemCode.Value = string.Empty;
                //this.ucLabelItemName.Value = string.Empty;
                this.ucLabelMOCode.Value = string.Empty;
                //this.ucLabelCartonCapacity.Value = string.Empty;
                this.ucLabelCartonCollected.Value = string.Empty;

            }
            else
            {
                this.ucLabelItemCode.Value = cartonCollection.ItemCode + "-" + cartonCollection.ItemDescription;
                //this.ucLabelItemName.Value = cartonCollection.ItemDescription;
                this.ucLabelMOCode.Value = cartonCollection.MOCode;
                //this.ucLabelCartonCapacity.Value = cartonCollection.CAPACITY.ToString();
                this.ucLabelCartonCollected.Value = cartonCollection.COLLECTED.ToString();

            }
            this.m_CartonCollection = null;
        }

        //初始化Grid
        private void ultraGridDetail_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraWinGridHelper ultraWinGridHelper = new UltraWinGridHelper(this.ultraGridDetail);
            //ultraWinGridHelper.AddCheckColumn("checkbox", "*");
            ultraWinGridHelper.AddCommonColumn("lotCode", "产品批次条码");
            ultraWinGridHelper.AddCommonColumn("lotQty", "批次数量");
            e.Layout.Bands[0].Columns["lotCode"].Width = 140;
            e.Layout.Bands[0].Columns["lotQty"].Width = 140;
            this.InitGridLanguage(ultraGridDetail);
        }

        //设置当前Grid的数据源
        private void InitializeGrid(object[] objs)
        {
            //清空grid
            this.m_DataTable.Rows.Clear();
            this.ultraGridDetail.DataSource = null;

            if (objs != null)
            {
                m_DataTable.Columns.Clear();
                m_DataTable.Columns.Add("lotCode", typeof(string)).ReadOnly = true;
                m_DataTable.Columns.Add("lotQty", typeof(string)).ReadOnly = true;
                for (int i = 0; i < objs.Length; i++)
                {
                    m_DataTable.Rows.Add(new object[] { ((Carton2Lot)objs[i]).LotCode, ((Carton2Lot)objs[i]).CartonQty });
                    m_DataTable.AcceptChanges();
                }
                this.ultraGridDetail.DataSource = m_DataTable;
            }
            this.m_Carton2Lot = null;
        }
    }
}
