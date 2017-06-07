using System;
using UserControl;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.Package;

namespace BenQGuru.eMES.Common.DCT.Action
{
    public class ActionCompareProductCode : BaseDCTAction
    {
        public ActionCompareProductCode()
        {
            this.InitMessage = (new ActionHelper()).GetActionDesc(this);
            this.OutMesssage = new Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO");
            this.LastPrompMesssage = new Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO");
        }

        private string _CartonCode = string.Empty;  //Carton

        public override Messages PreAction(object act)
        {
            
            _CartonCode = act.ToString().ToUpper();            
            Messages msg = new Messages();
            if (_CartonCode == string.Empty)
            {               
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_CARTON_ISNULL"));
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

            object obj = (new Package.PackageFacade(domainProvider)).GetExistCARTONINFO(_CartonCode);

            if (obj == null)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_CARTON_NOT_EXIST"));
                ProcessBeforeReturn(this.Status, msg);
                return msg;
            }
            else
            {
                base.PreAction(act);
                msg.Add(new UserControl.Message(MessageType.Normal, "$CS_CARTON_NO [" + _CartonCode + "]"));
                msg.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_COMMODITY"));
                ProcessBeforeReturn(this.Status, msg);
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
            object[] objSimulation = db.GetSimulationFromCarton(_CartonCode);

            if (objSimulation == null)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_COMPARECOMMODITY_ISWRONG "));
                ProcessBeforeReturn(this.Status, msg);
                return msg;
            }
            else
            {
                object objSimulationRcrd = objSimulation[0];

                ItemFacade itemfacade = new ItemFacade(domainProvider);
                object objitem = itemfacade.GetItem(((Simulation)objSimulation[0]).ItemCode.Trim().ToUpper(), GlobalVariables.CurrentOrganizations.First().OrganizationID);
                if (objitem == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_COMPARECOMMODITY_ISWRONG "));
                    ProcessBeforeReturn(this.Status, msg);
                    return msg;
                }
                else
                {
                    if (((Item)objitem).ItemProductCode.ToUpper().Trim() == act.ToString().ToUpper())
                    {
                        //add by hiro 08/10/06
                        DBDateTime dbDateTime;
                        dbDateTime = FormatHelper.GetNowDBDateTime(domainProvider);
                        PackageFacade _Packfacade = new PackageFacade(domainProvider);
                        PACKINGCHK newPackingCheck = _Packfacade.CreateNewPACKINGCHK();
                        newPackingCheck.Rcard = ((Simulation)objSimulationRcrd).RunningCard.ToString().ToUpper();
                        newPackingCheck.CheckAccessory = FormatHelper.FALSE_STRING;
                        newPackingCheck.CheckProductCode = FormatHelper.TRUE_STRING;
                        newPackingCheck.MUSER = (act as IDCTClient).LoginedUser;
                        newPackingCheck.MDATE = dbDateTime.DBDate;
                        newPackingCheck.MTIME = dbDateTime.DBTime;
                        newPackingCheck.EATTRIBUTE1 = " ";
                        object objGet = _Packfacade.GetPACKINGCHK(((Simulation)objSimulationRcrd).RunningCard.ToString().ToUpper());
                        if (objGet != null)
                        {
                            newPackingCheck.CheckAccessory = ((PACKINGCHK)objGet).CheckAccessory.Trim();
                            newPackingCheck.EATTRIBUTE1 = ((PACKINGCHK)objGet).EATTRIBUTE1;
                            _Packfacade.DeletePACKINGCHK((PACKINGCHK)objGet);
                            _Packfacade.AddPACKINGCHK(newPackingCheck);
                        }
                        else
                        {
                            _Packfacade.AddPACKINGCHK(newPackingCheck);
                        }
                        //end by hiro
                        base.Action(act);
                        msg.Add(new UserControl.Message(MessageType.Success, "$CS_COMPARECOMMODITY_ISSUCCESS "));
                        _CartonCode = string.Empty;
                        return msg;
                    }
                    else
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_COMPARECOMMODITY_ISWRONG "));
                        ProcessBeforeReturn(this.Status, msg);
                        return msg;
                    }
                }
            }
        }

    
    }
}
