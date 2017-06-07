using System;
using UserControl;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Package;
using BenQGuru.eMES.Domain.Package;

namespace BenQGuru.eMES.Common.DCT.Action
{
    public class ActionCompareAppAndProductCode : BaseDCTAction
    {
        public ActionCompareAppAndProductCode()
        {
            this.InitMessage = (new ActionHelper()).GetActionDesc(this);
            this.OutMesssage = new Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO");
            this.LastPrompMesssage = new Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO");
        }

        public string strCarton = string.Empty;  //Carton
        public bool checkCompare = true;
        public bool checkCycle = true;

        public override Messages PreAction(object act)
        {
            Messages msg = new Messages();
            if (checkCycle)
            {
                strCarton = act.ToString().ToUpper();

                if (strCarton == string.Empty)
                {
                    msg.Add(new UserControl.Message(MessageType.Normal, "$CS_CARTON_ISNULL"));
                    ProcessBeforeReturn(this.Status, msg);
                    return msg;
                }

                BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider domainProvider = null;

                if ((act as IDCTClient).DBConnection != null)
                {
                    domainProvider = (act as IDCTClient).DBConnection as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
                }
                else
                {
                    domainProvider = Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider()
                        as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
                    (act as IDCTClient).DBConnection = domainProvider;
                }

                object obj = (new Package.PackageFacade(domainProvider)).GetExistCARTONINFO(strCarton);

                if (obj == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_CARTON_NOT_EXIST"));
                    ProcessBeforeReturn(this.Status, msg);
                    return msg;
                }
                else
                {
                    checkCycle = false;
                    base.PreAction(act);
                    msg.Add(new UserControl.Message(MessageType.Normal, "$CS_CARTON_NO [" + strCarton + "]"));
                    msg.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_WRAPPER"));
                    return msg;
                }
            }
            else
            {
                base.PreAction(act);
                return msg;
            }
        }


        public override Messages Action(object act)
        {
            Messages msg = new Messages();
            BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider domainProvider = null;


            if (act == null)
            {
                return msg;
            }

            if ((act as IDCTClient).DBConnection != null)
            {
                domainProvider = (act as IDCTClient).DBConnection as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
            }
            else
            {
                domainProvider = Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider()
                    as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
                (act as IDCTClient).DBConnection = domainProvider;
            }

            DataCollectFacade db = new DataCollectFacade(domainProvider);
            object[] objSimulation = db.GetSimulationFromCarton(strCarton);


            if (checkCompare)
            {
                if (objSimulation == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_COMPAREAPPENDIX_NOTSUCCESS"));
                    msg.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_WRAPPER"));                 
                    this.Status = ActionStatus.PrepareData;
                    return msg;

                }
                else
                {
                    object objSimulationRcrd = objSimulation[0];
                    //if (((Simulation)objSimulationRcrd).ItemCode.ToString().ToUpper() != act.ToString().ToUpper())
                    if (((Simulation)objSimulationRcrd).RunningCard.ToUpper().IndexOf(act.ToString().ToUpper()) != 0)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_COMPAREAPPENDIX_NOTSUCCESS"));
                        msg.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_WRAPPER"));                        
                        this.Status = ActionStatus.PrepareData;
                        return msg;
                    }
                    else
                    {
                        msg.Add(new UserControl.Message(MessageType.Success, "$CS_COMPAREAPPENDIX_SUCCESS"));
                        msg.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_COMMODITY"));
                        DBDateTime dbDateTime;
                        dbDateTime = FormatHelper.GetNowDBDateTime(domainProvider);
                        PackageFacade _Packfacade = new PackageFacade(domainProvider);
                        PACKINGCHK newPackingCheck = _Packfacade.CreateNewPACKINGCHK();
                        newPackingCheck.Rcard = ((Simulation)objSimulationRcrd).RunningCard.ToString().ToUpper();
                        newPackingCheck.CheckAccessory = FormatHelper.TRUE_STRING;
                        newPackingCheck.CheckProductCode = FormatHelper.FALSE_STRING;
                        newPackingCheck.MUSER = (act as IDCTClient).LoginedUser;
                        newPackingCheck.MDATE = dbDateTime.DBDate;
                        newPackingCheck.MTIME = dbDateTime.DBTime;
                        newPackingCheck.EATTRIBUTE1 = " ";
                        object objGet = _Packfacade.GetPACKINGCHK(((Simulation)objSimulationRcrd).RunningCard.ToString().ToUpper());
                        if (objGet != null)
                        {
                            newPackingCheck.CheckProductCode = ((PACKINGCHK)objGet).CheckProductCode.Trim();
                            newPackingCheck.EATTRIBUTE1 = ((PACKINGCHK)objGet).EATTRIBUTE1;
                            _Packfacade.DeletePACKINGCHK((PACKINGCHK)objGet);
                            _Packfacade.AddPACKINGCHK(newPackingCheck);
                        }
                        else
                        {
                            _Packfacade.AddPACKINGCHK(newPackingCheck);
                        }
                        this.Status = ActionStatus.PrepareData;
                        checkCompare = false;
                        return msg;
                    }
                }

            }
            else
            {
                if (objSimulation == null)
                {                   
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_COMPARECOMMODITY_ISWRONG"));
                    msg.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_COMMODITY"));                    
                    this.Status = ActionStatus.PrepareData;
                    return msg;

                }
                else
                {
                    object objSimulationRcrd = objSimulation[0];
                    ItemFacade itemfacade = new ItemFacade(domainProvider);
                    object objitem = itemfacade.GetItem(((Simulation)objSimulation[0]).ItemCode.Trim().ToUpper(), GlobalVariables.CurrentOrganizations.First().OrganizationID);
                    if (objitem == null)
                    {                      
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_COMPARECOMMODITY_ISWRONG"));
                        msg.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_COMMODITY"));
                        this.Status = ActionStatus.PrepareData;
                        return msg;

                    }
                    else
                    {
                        if (((Item)objitem).ItemProductCode.ToUpper().Trim() == act.ToString().ToUpper())
                        {
                            base.Action(act);                          
                            msg.Add(new UserControl.Message(MessageType.Success, "$CS_COMPARECOMMODITY_ISSUCCESS"));
                            DBDateTime dbDateTime;
                            dbDateTime = FormatHelper.GetNowDBDateTime(domainProvider);
                            PackageFacade _Packfacade = new PackageFacade(domainProvider);
                            PACKINGCHK newPackingCheck = _Packfacade.CreateNewPACKINGCHK();
                            newPackingCheck.Rcard = ((Simulation)objSimulationRcrd).RunningCard.ToString().ToUpper();
                            newPackingCheck.CheckAccessory = FormatHelper.TRUE_STRING;
                            newPackingCheck.CheckProductCode = FormatHelper.TRUE_STRING;
                            newPackingCheck.MUSER = (act as IDCTClient).LoginedUser;
                            newPackingCheck.MDATE = dbDateTime.DBDate;
                            newPackingCheck.MTIME = dbDateTime.DBTime;
                            newPackingCheck.EATTRIBUTE1 = " ";
                            object objGet = _Packfacade.GetPACKINGCHK(((Simulation)objSimulationRcrd).RunningCard.ToString().ToUpper());
                            if (objGet != null)
                            {                                
                                newPackingCheck.EATTRIBUTE1 = ((PACKINGCHK)objGet).EATTRIBUTE1;
                                _Packfacade.DeletePACKINGCHK((PACKINGCHK)objGet);
                                _Packfacade.AddPACKINGCHK(newPackingCheck);
                            }
                            else
                            {
                                _Packfacade.AddPACKINGCHK(newPackingCheck);
                            }
                            this.ObjectState = null;
                            //this.Status = ActionStatus.PrepareData;
                            strCarton = string.Empty;
                            checkCompare = true;
                            checkCycle = true;
                            return msg;
                        }
                        else
                        {                           
                            msg.Add(new UserControl.Message(MessageType.Error, "$CS_COMPARECOMMODITY_ISWRONG"));
                            msg.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_COMMODITY"));                           
                            this.Status = ActionStatus.PrepareData;
                            return msg;

                        }
                    }
                }
            }
        }
    }
}
