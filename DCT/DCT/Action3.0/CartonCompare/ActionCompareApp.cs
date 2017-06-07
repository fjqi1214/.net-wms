using System;
using UserControl;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;

namespace BenQGuru.eMES.Common.DCT.Action
{
    public class ActionCartonCompare : BaseDCTAction
    {
        public ActionCartonCompare()
		{
            base.output_message
                = new Message(MessageType.Normal, "$DCT_Please_Input_SN_OR_Directive");
		}

        public string strCarton = string.Empty;  //Carton

        public override Messages PreAction(object act)
        {
            strCarton = act.ToString().ToUpper();

            if (strCarton == string.Empty)
            {
                Messages msgMo = new Messages();
                msgMo.Add(new UserControl.Message(MessageType.Normal, "$CS_WRAPPER_ISNULL"));
                msgMo.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_WRAPPER"));
                return msgMo;
            }
           
            base.PreAction(act);

            Messages msg = new Messages();
            msg.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_WRAPPER"));
            msg.Add(new UserControl.Message(MessageType.Normal, "$CS_CARTON_NO [" + strCarton + "]"));

            return msg;
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
                
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_Product_CompareItemFailed"));
                msg.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"));

                base.Action(act);
                this.Status = ActionStatus.PrepareData;
                strCarton = string.Empty;           
                

                return msg;
            }
            else
            {
                object objSimulationRcrd = objSimulation[0];
                if (((Simulation)objSimulationRcrd).RunningCard.ToString().ToUpper() != act.ToString().ToUpper())
                {
                   
                    msg.Add(new UserControl.Message(MessageType.Succes, "$CS_Product_CompareItemFailed"));
                    msg.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"));

                    base.Action(act);
                    this.Status = ActionStatus.PrepareData;
                    strCarton = string.Empty; 
  
                    return msg;
                }
                else
                {
                    msg.Add(new UserControl.Message(MessageType.Succes, "$CS_Add_Success"));
                    msg.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"));

                  
                }
            }

            base.Action(act);
            this.Status = ActionStatus.PrepareData;
            strCarton = string.Empty; 

            return msg;
        }


    }
}
