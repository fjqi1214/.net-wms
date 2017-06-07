using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Infragistics.Win;

using UserControl;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Package;
using BenQGuru.eMES.Domain.Package;

namespace BenQGuru.eMES.Client
{
    public partial class FCompAppendix : Form
    {
        public FCompAppendix()
        {
            InitializeComponent();
            this.edtInputCarton.TextFocus(false, true);
            this.txtCollected.Value = "0";
        }

        private string m_rcard = string.Empty;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private bool checkCommodity = true;
        private string _FunctionName = string.Empty;

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }




        private void edtInputCarton_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string strCarton = this.edtInputCarton.Value.ToString().Trim();
                if (strCarton == string.Empty)
                {
                    this.edtInputCarton.TextFocus(false, true);
                }
                else
                {
                    ucMessageInfo.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_WRAPPER"), false);
                    this.editBarcode.TextFocus(false, true);
                }

            }
        }

        private void editBarcode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == '\r')
            {
                string strBarcode = this.editBarcode.Value.ToString().Trim().ToUpper();
                if (strBarcode == string.Empty)
                {
                    this.editBarcode.TextFocus(false, true);
                    return;
                }
                if (this.edtInputCarton.Value.ToString().Trim() == string.Empty)
                {
                    ucMessageInfo.AddEx(this._FunctionName, this.edtInputCarton.Caption + ": " + this.edtInputCarton.Value, new UserControl.Message(MessageType.Error, "$CS_PLEASE_INPUT_CARTONNO"), true);
                    this.edtInputCarton.TextFocus(false, true);
                    return;
                }

                if (checkCommodity)
                {
                    DataCollectFacade _face = new DataCollectFacade(DataProvider);
                    object[] objSimulation = _face.GetSimulationFromCarton(this.edtInputCarton.Value.ToString().Trim());
                    if (objSimulation != null)
                    {
                        for (int i = 0; i < objSimulation.Length; i++)
                        {
                            //if (((Simulation)objSimulation[0]).ItemCode.Trim().ToUpper() == strBarcode)
                            if (((Simulation)objSimulation[i]).RunningCard.ToUpper().IndexOf(strBarcode) == 0)
                            {
                                m_rcard = ((Simulation)objSimulation[i]).RunningCard.Trim().ToUpper();
                                this.savePackingCheck(false, false);
                                this.txtCollected.Value = Convert.ToString(int.Parse(this.txtCollected.Value) + 1);
                            }
                            else
                            {
                                ucMessageInfo.AddEx(this._FunctionName, this.edtInputCarton.Caption + ": " + this.edtInputCarton.Value, new UserControl.Message(MessageType.Error, "$CS_Appdenix_Iswrong"), true);
                                ucMessageInfo.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_WRAPPER"), false);
                                this.editBarcode.TextFocus(true, true);
                                break;
                            }

                            if (i == objSimulation.Length - 1)
                            {
                                ucMessageInfo.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Success, "$CS_COMPAREAPPENDIX_SUCCESS:" + strBarcode), false);
                                if (this.checkBoxCom.Checked)
                                {
                                    ucMessageInfo.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_COMMODITY"), false);
                                    this.editBarcode.TextFocus(true, true);
                                    checkCommodity = false;
                                }
                                else
                                {
                                    this.editBarcode.Value = string.Empty;
                                    this.edtInputCarton.TextFocus(true, true);
                                }
                            }
                        }

                    }
                    else
                    {
                        ucMessageInfo.AddEx(this._FunctionName, this.edtInputCarton.Caption + ": " + this.edtInputCarton.Value, new UserControl.Message(MessageType.Error, "$CS_Appdenix_Iswrong"), true);
                        ucMessageInfo.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"), false);
                        this.edtInputCarton.Value = string.Empty;
                        this.editBarcode.Value = string.Empty;
                        this.edtInputCarton.TextFocus(false, true);
                    }
                }
                else
                {
                    if (this.checkBoxCom.Checked)
                    {
                        DataCollectFacade _face = new DataCollectFacade(DataProvider);
                        object[] objSimulation = _face.GetSimulationFromCarton(this.edtInputCarton.Value.ToString().Trim());
                        if (objSimulation == null)
                        {
                            ucMessageInfo.AddEx(this._FunctionName, this.edtInputCarton.Caption + ": " + this.edtInputCarton.Value, new UserControl.Message(MessageType.Error, "$CS_COMPARECOMMODITY_ISWRONG"), true);
                            ucMessageInfo.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_COMMODITY"), false);
                            this.editBarcode.TextFocus(true, true);
                        }
                        else
                        {
                            ItemFacade itemfacade = new ItemFacade(this.DataProvider);
                            for (int i = 0; i < objSimulation.Length; i++)
                            {
                                object objitem = itemfacade.GetItem(((Simulation)objSimulation[i]).ItemCode.Trim().ToUpper(), GlobalVariables.CurrentOrganizations.First().OrganizationID);
                                if (objitem == null)
                                {
                                    ucMessageInfo.AddEx(this._FunctionName, this.edtInputCarton.Caption + ": " + this.edtInputCarton.Value, new UserControl.Message(MessageType.Error, "$CS_COMPARECOMMODITY_ISWRONG"), true);
                                    ucMessageInfo.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_COMMODITY"), false);
                                    this.editBarcode.TextFocus(true, true);
                                    break;
                                }
                                else
                                {
                                    if (((Item)objitem).ItemProductCode.Trim() == this.editBarcode.Value.Trim().ToUpper())
                                    {
                                        
                                        checkCommodity = true;
                                        m_rcard = ((Simulation)objSimulation[i]).RunningCard.Trim().ToUpper();//added by Gawain.Gu,2011-01-30
                                        this.savePackingCheck(true, true);
                                    }
                                    else
                                    {
                                        ucMessageInfo.AddEx(this._FunctionName, this.edtInputCarton.Caption + ": " + this.edtInputCarton.Value, new UserControl.Message(MessageType.Error, "$CS_COMPARECOMMODITY_ISWRONG"), true);
                                        ucMessageInfo.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_COMMODITY"), false);
                                        this.editBarcode.TextFocus(true, true);
                                        break;
                                    }

                                    if (i == objSimulation.Length-1)
                                    {
                                        ucMessageInfo.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Success, "$CS_COMPARECOMMODITY_ISSUCCESS:" + this.editBarcode.Value.ToString().Trim()), false);
                                        m_rcard = string.Empty;
                                        this.editBarcode.Value = string.Empty;
                                        this.edtInputCarton.TextFocus(true, true);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        DataCollectFacade _face = new DataCollectFacade(DataProvider);
                        object[] objSimulation = _face.GetSimulationFromCarton(this.edtInputCarton.Value.ToString().Trim());
                        if (objSimulation != null)
                        {
                            //if (((Simulation)objSimulation[0]).ItemCode.Trim().ToUpper() == strBarcode)
                            if (((Simulation)objSimulation[0]).RunningCard.IndexOf(strBarcode) == 0)
                            {
                                ucMessageInfo.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Success, "$CS_COMPAREAPPENDIX_SUCCESS:" + strBarcode), false);
                                this.editBarcode.Value = string.Empty;
                                this.edtInputCarton.TextFocus(true, true);
                            }
                            else
                            {
                                ucMessageInfo.AddEx(this._FunctionName, this.edtInputCarton.Caption + ": " + this.edtInputCarton.Value, new UserControl.Message(MessageType.Error, "$CS_Appdenix_Iswrong"), true);
                                ucMessageInfo.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_WRAPPER"), false);
                                this.editBarcode.TextFocus(true, true);
                            }

                        }
                        else
                        {
                            ucMessageInfo.AddEx(this._FunctionName, this.edtInputCarton.Caption + ": " + this.edtInputCarton.Value, new UserControl.Message(MessageType.Error, "$CS_Appdenix_Iswrong"), true);
                            ucMessageInfo.AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"), false);
                            this.edtInputCarton.Value = string.Empty;
                            this.editBarcode.Value = string.Empty;
                            this.edtInputCarton.TextFocus(false, true);
                        }

                    }
                }
            }
        }

        private void edtInputCarton_Load(object sender, EventArgs e)
        {
            this.edtInputCarton.TextFocus(false, true);
        }

        private void savePackingCheck(bool IsCheckProduct, bool IsUpdaterProdct)
        {
            //add by hiro 08/09/27
            PackageFacade _Packfacade = new PackageFacade(DataProvider);
            PACKINGCHK newPackingCheck = _Packfacade.CreateNewPACKINGCHK();
            newPackingCheck.Rcard = m_rcard;
            DBDateTime dbDateTime;
            dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            newPackingCheck.CheckAccessory = FormatHelper.TRUE_STRING;
            if (IsCheckProduct)
                newPackingCheck.CheckProductCode = FormatHelper.TRUE_STRING;
            else
                newPackingCheck.CheckProductCode = FormatHelper.FALSE_STRING;
            newPackingCheck.MUSER = ApplicationService.Current().UserCode;
            newPackingCheck.MDATE = dbDateTime.DBDate;
            newPackingCheck.MTIME = dbDateTime.DBTime;
            newPackingCheck.EATTRIBUTE1 = " ";
            object objGet = _Packfacade.GetPACKINGCHK(m_rcard);
            if (objGet != null)
            {
                if (((PACKINGCHK)objGet).EATTRIBUTE1.ToString().Trim() != string.Empty)
                    newPackingCheck.EATTRIBUTE1 = ((PACKINGCHK)objGet).EATTRIBUTE1.ToString();
                if (!IsUpdaterProdct)
                    newPackingCheck.CheckProductCode = ((PACKINGCHK)objGet).CheckProductCode.ToString();
                _Packfacade.DeletePACKINGCHK((PACKINGCHK)objGet);
                _Packfacade.AddPACKINGCHK(newPackingCheck);
            }
            else
            {
                _Packfacade.AddPACKINGCHK(newPackingCheck);
            }
            //end by hiro
        }

        private void FCompAppendix_Load(object sender, EventArgs e)
        {
            this._FunctionName = this.Text;
        }

        private void ucMessageInfo_WorkingErrorAdded(object sender, WorkingErrorAddedEventArgs e)
        {
            CSHelper.ucMessageWorkingErrorAdded(e, this.DataProvider);
        }

    }
}