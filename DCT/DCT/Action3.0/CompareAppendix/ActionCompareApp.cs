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
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Common.DCT.Action
{
    public class ActionCompareApp : BaseDCTAction
    {
        public ActionCompareApp()
        {
            this.InitMessage = (new ActionHelper()).GetActionDesc(this);
            this.OutMesssage = new Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO");
            this.LastPrompMesssage = new Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO");
        }

        private string strCarton = string.Empty;  //Carton

        public override Messages PreAction(object act)
        {
            strCarton = act.ToString().ToUpper();
            Messages msg = new Messages();
            if (strCarton == string.Empty)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_CARTON_ISNULL"));
                msg.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"));
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
                base.PreAction(act);
                msg.Add(new UserControl.Message(MessageType.Success, "$CS_CARTON_NO [" + strCarton + "]"));
                this.LastPrompMesssage = new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_WRAPPER");
                msg.Add(this.LastPrompMesssage);
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

            if (objSimulation == null)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_COMPAREAPPENDIX_NOTSUCCESS"));
                ProcessBeforeReturn(this.Status, msg);               
                return msg;
            }
            else
            {
                object objSimulationRcrd = objSimulation[0];
                //if (((Simulation)objSimulationRcrd).ItemCode.ToString().ToUpper() != act.ToString().ToUpper())
                if (((Simulation)objSimulationRcrd).RunningCard.ToUpper().IndexOf(act.ToString().ToUpper()) != 0)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_COMPAREAPPENDIX_NOTSUCCESS"));
                    ProcessBeforeReturn(this.Status, msg);                    
                    return msg;
                }
                else
                {
                    msg.Add(new UserControl.Message(MessageType.Success, "$CS_COMPAREAPPENDIX_SUCCESS"));
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
                        newPackingCheck.EATTRIBUTE1 =((PACKINGCHK)objGet).EATTRIBUTE1;
                        _Packfacade.DeletePACKINGCHK((PACKINGCHK)objGet);
                        _Packfacade.AddPACKINGCHK(newPackingCheck);
                    }
                    else
                    {
                        _Packfacade.AddPACKINGCHK(newPackingCheck);
                    }
                    base.Action(act); 
                    this.ObjectState = null;                    
                    strCarton = string.Empty;
                    return msg;
                }
            }
          
        }

    }
}
