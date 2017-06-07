using System;
using UserControl;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.Package;

namespace BenQGuru.eMES.Common.DCT.Action
{
    public class ActionFGPacking : BaseDCTAction
    {
        public ActionFGPacking()
        {
            this.InitMessage = (new ActionHelper()).GetActionDesc(this);
            this.OutMesssage = new Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
            this.LastPrompMesssage = new Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
        }

        public int m_FlowControl = 0;
        public string strCarton = string.Empty;
        public string strRcard = string.Empty;  //rcard
        public bool checkCycle = true;
        object _item;
        object _objSimulation;
        DataCollectFacade _face;

        public override Messages PreAction(object act)
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

            _face = new DataCollectFacade(domainProvider);
            ItemFacade itemFacade = new ItemFacade(domainProvider);

            if (checkCycle)
            {
                strRcard = act.ToString().Trim().ToUpper();

                if (strRcard == string.Empty)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_IDisNull"));
                    ProcessBeforeReturn(this.Status, msg);
                    return msg;
                }

                _objSimulation = _face.GetSimulation(strRcard);
                if (_objSimulation == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$NoProductInfo"));
                    ProcessBeforeReturn(this.Status, msg);
                    return msg;
                }

                _item = itemFacade.GetItem((_objSimulation as Simulation).ItemCode.ToUpper().Trim(), GlobalVariables.CurrentOrganizations.First().OrganizationID);
                if (_item == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$Error_ItemCode_NotExist $Domain_Item=" + (_objSimulation as Simulation).ItemCode));
                    ProcessBeforeReturn(this.Status, msg);
                    return msg;
                }

                checkCycle = false;
                base.PreAction(act);

                msg.Add(new UserControl.Message(MessageType.Normal, "$CS_Param_ID [" + strRcard + "]"));
                msg.Add(new UserControl.Message(MessageType.Success, "$CS_PLEASE_INPUT_CARTONNO"));
                m_FlowControl++;
                return msg;
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


            if (m_FlowControl == 1)
            {


                string itemCode = ((Item)_item).ItemCode.ToString();
                strCarton = act.ToString().Trim().ToUpper();

                if (((Item)_item).NeedCheckCarton == FormatHelper.TRUE_STRING)
                {
                    if (strRcard.StartsWith(itemCode, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (string.Compare(strRcard, strCarton, true) == 0)
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$Error_CartonNotMatchTheRule"));
                            msg.Add(new UserControl.Message(MessageType.Success, "$CS_PLEASE_INPUT_CARTONNO"));

                            this.Status = ActionStatus.PrepareData;                     
                            return msg;
                        }
                        else
                        {
                            if (string.Compare(strRcard.Replace(" ", ""), strCarton.Replace(" ", ""), true) != 0)
                            {

                                msg.Add(new UserControl.Message(MessageType.Error, "$Error_CartonCheckFailed"));
                                msg.Add(new UserControl.Message(MessageType.Success, "$CS_PLEASE_INPUT_CARTONNO"));
                                this.Status = ActionStatus.PrepareData;                            
                                return msg;
                            }
                            else
                            {
                                msg.Add(new UserControl.Message(MessageType.Success, "$CARTON_COMPARE_ISSUCCESS"));

                            }
                        }
                    }
                    else
                    {
                        if (string.Compare(strRcard, strCarton, true) == 0)
                        {
                            msg.Add(new UserControl.Message(MessageType.Success, "$CARTON_COMPARE_ISSUCCESS"));
                        }
                        else
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$Error_CartonCheckFailed"));
                            msg.Add(new UserControl.Message(MessageType.Success, "$CS_PLEASE_INPUT_CARTONNO"));
                            this.Status = ActionStatus.PrepareData;                          
                            return msg;
                        }
                    }
                }

                object obj = (new Package.PackageFacade(domainProvider)).GetCARTONINFO(act.ToString().Trim().ToUpper());
                if (obj != null && ((CARTONINFO)obj).CAPACITY.ToString() == ((CARTONINFO)obj).COLLECTED.ToString())
                {                  
                    msg.Add(new UserControl.Message(MessageType.Error, "$CARTON_ALREADY_FILL_OUT"));
                    msg.Add(new UserControl.Message(MessageType.Success, "$CS_PLEASE_INPUT_CARTONNO"));
                    this.Status = ActionStatus.PrepareData;  
                    return msg;
                }
                else
                {
                    m_FlowControl++;                   
                    msg.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_WRAPPER"));                    
                    this.Status = ActionStatus.PrepareData;                    
                    return msg;
                }
            }
            else
            {
                //if (((Simulation)_objSimulation).ItemCode.ToUpper().Trim() == act.ToString().ToUpper())
                if (((Simulation)_objSimulation).RunningCard.ToUpper().IndexOf(act.ToString().ToUpper()) == 0)
                {

                    ActionOnLineHelper actionOnLineHelper = new ActionOnLineHelper(domainProvider);
                    BenQGuru.eMES.DataCollect.Action.ActionFactory actionFactory = new BenQGuru.eMES.DataCollect.Action.ActionFactory(domainProvider);
                    IAction actionCartonPack = actionFactory.CreateAction(ActionType.DataCollectAction_Carton);
                    //((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)domainProvider).PersistBroker.OpenConnection();
                    msg = actionOnLineHelper.GetIDInfo(strRcard);
                    string cartonno = FormatHelper.CleanString(strCarton);
                    IDCTClient client = act as IDCTClient;

                    //add by hiro 08/09/28
                    DBDateTime dbDateTime;
                    dbDateTime = FormatHelper.GetNowDBDateTime(domainProvider);
                    PackageFacade _Packfacade = new PackageFacade(domainProvider);
                    PACKINGCHK newPackingCheck = _Packfacade.CreateNewPACKINGCHK();
                    newPackingCheck.Rcard = ((Simulation)_objSimulation).RunningCard.ToString().ToUpper();
                    newPackingCheck.CheckAccessory = FormatHelper.TRUE_STRING;
                    newPackingCheck.CheckProductCode = FormatHelper.FALSE_STRING;
                    newPackingCheck.MUSER = (act as IDCTClient).LoginedUser;
                    newPackingCheck.MDATE = dbDateTime.DBDate;
                    newPackingCheck.MTIME = dbDateTime.DBTime;
                    newPackingCheck.EATTRIBUTE1 = " ";
                    object objGet = _Packfacade.GetPACKINGCHK(((Simulation)_objSimulation).RunningCard.ToString().ToUpper());
                    if (objGet != null)
                    {                       
                        _Packfacade.DeletePACKINGCHK((PACKINGCHK)objGet);
                        _Packfacade.AddPACKINGCHK(newPackingCheck);
                    }
                    else
                    {
                        _Packfacade.AddPACKINGCHK(newPackingCheck);
                    }
                    //end by hiro

                    Messages msgTwo = new Messages();
                    domainProvider.BeginTransaction();
                    try
                    {
                        if (msg.IsSuccess())
                        {
                            ProductInfo product = (ProductInfo)msg.GetData().Values[0];
                            if (product.LastSimulation == null)
                            {
                                base.Action(act);                                
                                msgTwo.Add(new UserControl.Message(new Exception("$Error_LastSimulation_IsNull!")));                             
                                this.ObjectState = null;                               
                                return msgTwo;
                            }

                            CartonPackEventArgs cartonPackEventArgs = new CartonPackEventArgs(ActionType.DataCollectAction_Carton,
                                strRcard, client.LoginedUser,
                                client.ResourceCode,
                                "",
                                cartonno,
                                product);

                            msgTwo.AddMessages(actionCartonPack.Execute(cartonPackEventArgs));
                        }
                        if (msgTwo.IsSuccess())
                        {
                            domainProvider.CommitTransaction();
                        }
                        else
                        {
                            domainProvider.RollbackTransaction();                       
                            msgTwo.Add(new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"));
                            this.Status = ActionStatus.PrepareData;
                            this.FlowDirect = FlowDirect.WaitingInput;
                            this.ObjectState = null;
                            this.clearValue();
                            return msgTwo;
                        }
                    }
                    catch (Exception ex)
                    {
                        domainProvider.RollbackTransaction();                
                        msg.Add(new UserControl.Message(ex));
                        msg.Add(new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"));
                        this.Status = ActionStatus.PrepareData;
                        this.FlowDirect = FlowDirect.WaitingInput;
                        this.ObjectState = null;                        
                        this.clearValue();
                        return msg;
                    }
                    finally
                    {
                        ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)domainProvider).PersistBroker.CloseConnection();
                    }

                    base.Action(act);
                    msg.Add(new UserControl.Message(MessageType.Success, "$CS_COMPAREAPPENDIX_SUCCESS"));
                    msg.Add(new UserControl.Message(MessageType.Success, "$CS_RCARD_CARTON_SUCCESS [" + strCarton + "]"));                  
                    this.ObjectState = null;                   
                    this.clearValue();
                    return msg;
                }
                else
                {                    
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_COMPAREAPPENDIX_NOTSUCCESS"));
                    msg.Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_WRAPPER"));
                    this.Status = ActionStatus.PrepareData;  
                    return msg;
                }

            }
        }


        public void clearValue()
        {
            m_FlowControl = 0;
            strCarton = string.Empty;
            strRcard = string.Empty;
            checkCycle = true;
            _item = null;
            _objSimulation = null;
        }
    }
}
