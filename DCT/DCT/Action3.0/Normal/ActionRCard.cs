using UserControl;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Common.DCT.Action
{
    /// <author>Laws Lu</author>
    /// <since>2006/04/14</since>
    /// <version>1.0.0</version>
    public class ActionRCard : BaseDCTAction
    {
        public ActionRCard()
        {
            this.OutMesssage
                = new Message(MessageType.Normal, "$DCT_Please_Input_SN_OR_Directive");

        }

        public bool NeedInputActionCommand = false;	// 登录资源后需要输入采集指令	// Added by Icyer 2007/01/04
        public override Messages PreAction(object act)
        {
            base.PreAction(act);

            Messages msg = new Messages();
            msg.Add(this.OutMesssage);

            return msg;
        }

        public override Messages Action(object act)
        {
            base.Action(act);

            Messages msg = new Messages();

            if (act == null)
            {
                return msg;
            }

            DataCollect.Action.ActionEventArgs args;
            if (ObjectState == null)
            {
                args = new BenQGuru.eMES.DataCollect.Action.ActionEventArgs();
            }
            else
            {
                args = ObjectState as DataCollect.Action.ActionEventArgs;
            }

            string data = act.ToString().ToUpper().Trim();

            //msg = CheckData(data);
            if (msg.IsSuccess())
            {
                #region Key logic,Push next action and fill it with data
                args.RunningCard = data;

                ObjectState = args;

                Web.Helper.ActionType acttype = new Web.Helper.ActionType();
                acttype.Items.Add(BaseDCTDriver.NEXTOP);

                ActionRCard acRcard = new ActionRCard();

                if ((act as IDCTClient).CachedAction != null && !acttype.Items.Contains(args.RunningCard))//如果存在缓存的Action则利用缓存的Action
                {

                    BaseDCTAction action = (act as IDCTClient).CachedAction;

                    if (!(action is ActionCollectNG)
                        && !(action is ActionCollectSMTNG)
                        && !(action is ActionCollectOutlineNG)
                        && !(action is ActionCollectKeyparts)
                        && !(action is ActionBatchWithKeypaerts)
                        && !(action is ActionCollectMix))
                    {
                        action.ObjectState = ObjectState;
                    }

                    this.NextAction = action;
                }
                else if ((act as IDCTClient).CachedAction == null && !acttype.Items.Contains(args.RunningCard))
                {

                    ActionFactory acFac = new ActionFactory();

                    acFac.ObjectState = ObjectState;
                    this.NextAction = acFac;
                }
                else if (acttype.Items.Contains(args.RunningCard))
                {

                    //ActionRCard acRcard = new ActionRCard();

                    //acRcard.ObjectState = ObjectState;
                    this.NextAction = acRcard;

                }

                bool bIsAction = true;
                if (acttype.Items.Contains(args.RunningCard))
                {
                    switch (args.RunningCard)
                    {
                        //归属工单
                        case ActionType.DataCollectAction_GoMO:
                            ActionGotoMO actionGotoMO = new ActionGotoMO();
                            //actionGotoMO.ObjectState = acRcrd.ObjectState;
                            acRcard.NextAction = actionGotoMO;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_Please_Input_MOCode");

                            (act as IDCTClient).CachedAction = actionGotoMO;
                            break;
                        //采集GOOD, added by jessie lee
                        case ActionType.DataCollectAction_GOOD:
                            ActionCollectGood actionCollectGood = new ActionCollectGood();
                            //actionCollectGood.ObjectState = ObjectState;
                            acRcard.NextAction = actionCollectGood;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$DCT_GOOD_Please_Input_GOOD_SN");

                            // Added by Icyer 2007/08/22
                            // 做良品采集时做归属工单
                            //changed by hiro 2008/08/27
                            //if ((act as IDCTClient).CachedAction is Action.ActionGotoMO)
                            //{
                            //    if (((Action.ActionGotoMO)(act as IDCTClient).CachedAction).moCode != string.Empty)
                            //    {
                            //        actionCollectGood.NeedGoMO = true;
                            //        actionCollectGood.NeedGoMOCode = ((Action.ActionGotoMO)(act as IDCTClient).CachedAction).moCode;
                            //    }
                            //}
                            // Added end

                            (act as IDCTClient).CachedAction = actionCollectGood;
                            break;
                        //采集NG, added by jessie lee, 10/05/2006
                        case ActionType.DataCollectAction_NG:
                            ActionCollectNG actionCollectNG = new ActionCollectNG();
                            //actionCollectNG.ObjectState = acRcard.ObjectState;
                            acRcard.NextAction = actionCollectNG;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$DCT_NG_Please_Input_NG_SN");

                            (act as IDCTClient).CachedAction = actionCollectNG;
                            break;
                        //采集SMTNG, added by jessie lee, 10/05/2006
                        case ActionType.DataCollectAction_SMTNG:
                            ActionCollectSMTNG actionCollectSMTNG = new ActionCollectSMTNG();
                            //actionCollectSMTNG.ObjectState = acRcard.ObjectState;
                            acRcard.NextAction = actionCollectSMTNG;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$DCT_NG_Please_Input_NG_SN");

                            (act as IDCTClient).CachedAction = actionCollectSMTNG;
                            break;
                        //采集OutlineGOOD, added by jessie lee, 2006/8/21
                        case ActionType.DataCollectAction_OutLineGood:
                            ActionCollectOutlineGood actionCollectOutlineGood = new ActionCollectOutlineGood();
                            //actionCollectGood.ObjectState = ObjectState;
                            acRcard.NextAction = actionCollectOutlineGood;

                            (act as IDCTClient).CachedAction = actionCollectOutlineGood;
                            break;
                        //采集OutlineNG, added by jessie lee, 2006/8/21
                        case ActionType.DataCollectAction_OutLineNG:
                            ActionCollectOutlineNG actionCollectOutlineNG = new ActionCollectOutlineNG();
                            //actionCollectNG.ObjectState = acRcard.ObjectState;
                            acRcard.NextAction = actionCollectOutlineNG;

                            (act as IDCTClient).CachedAction = actionCollectOutlineNG;
                            break;
                        //Keyparts上料, added by jessie lee, 10/05/2006
                        case ActionType.DataCollectAction_CollectKeyParts:
                            ActionCollectKeyparts actionCollectKeyparts = new ActionCollectKeyparts();
                            //actionCollectKeyparts.ObjectState = acRcard.ObjectState;
                            acRcard.NextAction = actionCollectKeyparts;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard");

                            // 如果上个采集是集成上料，则做KeyPart时同时做集成上料
                            if ((act as IDCTClient).CachedAction is Action.ActionCollectINNO)
                            {
                                if (((Action.ActionCollectINNO)(act as IDCTClient).CachedAction).INNOCode != string.Empty)
                                {
                                    actionCollectKeyparts.NeedCollectINNO = true;
                                    actionCollectKeyparts.INNOCode = ((Action.ActionCollectINNO)(act as IDCTClient).CachedAction).INNOCode;
                                }
                            }
                            else if ((act as IDCTClient).CachedAction is Action.ActionCollectKeyparts)
                            {
                                actionCollectKeyparts.NeedCollectINNO = ((Action.ActionCollectKeyparts)(act as IDCTClient).CachedAction).NeedCollectINNO;
                                actionCollectKeyparts.INNOCode = ((Action.ActionCollectKeyparts)(act as IDCTClient).CachedAction).INNOCode;
                            }
                            // Added end
                            (act as IDCTClient).CachedAction = actionCollectKeyparts;
                            break;
                        case ActionType.DataCollectAction_Split:
                            ActionCollectIDMerge actionCollectIDMerge = new ActionCollectIDMerge();
                            acRcard.NextAction = actionCollectIDMerge;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_Please_Input_SN_For_Merge");

                            (act as IDCTClient).CachedAction = actionCollectIDMerge;
                            break;
                        case ActionType.DataCollectAction_CollectINNO:
                            ActionCollectINNO actionCollectINNO = new ActionCollectINNO();
                            acRcard.NextAction = actionCollectINNO;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_CMPleaseInputINNOinEdt");

                            (act as IDCTClient).CachedAction = actionCollectINNO;
                            break;
                        case BaseDCTDriver.NEXTOP:
                            ActionNextOP actionNextOP = new ActionNextOP();
                            acRcard.NextAction = actionNextOP;
                            (act as IDCTClient).CachedAction = actionNextOP;
                            break;
                        case ActionType.DataCollectAction_CompareAppendix:
                            ActionCompareApp actionCartonCompare = new ActionCompareApp();
                            acRcard.NextAction = actionCartonCompare;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO");
                            (act as IDCTClient).CachedAction = actionCartonCompare;
                            break;
                        case ActionType.DataCollectAction_Mix:
                            ActionCollectMix actionCollectMix = new ActionCollectMix();
                            acRcard.NextAction = actionCollectMix;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
                            break;
                        case ActionType.DataCollectAction_KBatch:
                            ActionBatchWithKeypaerts actionBatchWithKeypaerts = new ActionBatchWithKeypaerts();
                            acRcard.NextAction = actionBatchWithKeypaerts;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
                            break;
                        case ActionType.DataCollectAction_CompareProductCode:
                            ActionCompareProductCode actionCompareProductCode = new ActionCompareProductCode();
                            acRcard.NextAction = actionCompareProductCode;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO");
                            (act as IDCTClient).CachedAction = actionCompareProductCode;
                            break;
                        case ActionType.DataCollectAction_CompareTwo:
                            ActionCompareAppAndProductCode actionCompareAppAndProductCode = new ActionCompareAppAndProductCode();
                            acRcard.NextAction = actionCompareAppAndProductCode;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO");
                            (act as IDCTClient).CachedAction = actionCompareAppAndProductCode;
                            break;
                        case ActionType.DataCollectAction_AutoNG:
                            ActionAutoNG actionAutoNG = new ActionAutoNG();
                            acRcard.NextAction = actionAutoNG;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$DCT_NG_Please_Input_NG_SN");
                            (act as IDCTClient).CachedAction = actionAutoNG;
                            break;
                        case ActionType.DataCollectAction_FGPacking:
                            ActionFGPacking actionFGPacking = new ActionFGPacking();
                            acRcard.NextAction = actionFGPacking;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
                            (act as IDCTClient).CachedAction = actionFGPacking;
                            break;
                        case ActionType.DataCollectAction_MACID:
                            ActionCollectMACID actionCollectMACID = new ActionCollectMACID();
                            acRcard.NextAction = actionCollectMACID;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
                            (act as IDCTClient).CachedAction = actionCollectMACID;
                            break;
                        case ActionType.DataCollectAction_MACALL:
                            ActionCollectMACALL actionCollectMACALL = new ActionCollectMACALL();
                            acRcard.NextAction = actionCollectMACALL;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
                            (act as IDCTClient).CachedAction = actionCollectMACALL;
                            break;
                        case ActionType.DataCollectAction_ONPost:
                            ActionCollectONPost actionCollectONPost = new ActionCollectONPost();
                            acRcard.NextAction = actionCollectONPost;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_Please_Input_UserCode");
                            (act as IDCTClient).CachedAction = actionCollectONPost;
                            break;
                        case ActionType.DataCollectAction_OffPost:
                            ActionCollectOffPost actionCollectOffPost = new ActionCollectOffPost();
                            acRcard.NextAction = actionCollectOffPost;
                            acRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_Please_Input_UserCode");
                            (act as IDCTClient).CachedAction = actionCollectOffPost;
                            break;
                        default:
                            bIsAction = false;
                            break;
                    }
                }
                else
                {
                    bIsAction = false;
                }
                if (NeedInputActionCommand == true)
                {
                    if (bIsAction == false)
                    {
                        msg.Add(new Message(MessageType.Error, "$CS_SystemError_CheckIDNotSupportAction"));
                        ActionRCard actRcard = new ActionRCard();
                        actRcard.OutMesssage = new Message(MessageType.Normal, "$DCT_PLEASE_ACTION");
                        actRcard.NeedInputActionCommand = true;
                        this.NextAction = actRcard;
                    }
                    else
                    {
                        NeedInputActionCommand = false;
                    }
                }
                #endregion



                msg.Add(new Message(MessageType.Data, "", new object[] { args }));
            }

            return msg;

        }


        public override Messages AftAction(object act)
        {
            base.AftAction(act);

            return null;
        }

        #region Check Data
        public Messages CheckData(string data)
        {
            Messages msg = new Messages();
            //			if ( data == string.Empty )
            //			{
            //				msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_User_Code_Empty"));
            //			}

            return msg;
        }
        #endregion

    }
}
