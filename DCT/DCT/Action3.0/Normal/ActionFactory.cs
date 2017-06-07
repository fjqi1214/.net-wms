using UserControl;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Common.DCT.Action
{
	/// <author>Laws Lu</author>
	/// <since>2006/04/14</since>
	/// <version>1.0.0</version>
	public class ActionFactory:BaseDCTAction,IFactoryAction	
	{
		private BaseDCTAction currentAction = null;
		public ActionFactory()
		{

			this.OutMesssage 
				= 	new Message(MessageType.Normal,"$DCT_PLEASE_ACTION");
			
		}

		public override Messages PreAction(object act)
		{
			base.PreAction (act);

			Messages msg = new Messages();
			msg.Add(this.OutMesssage);
			
			return  msg;
		}
		
		public override Messages Action(object act)
		{
			base.Action (act);
			
			Messages msg = new Messages();

			if(act == null)
			{
				return msg;
			}
			
			DataCollect.Action.ActionEventArgs args;
			if(ObjectState == null)
			{
				args = new BenQGuru.eMES.DataCollect.Action.ActionEventArgs();
			}
			else
			{
				args = ObjectState as DataCollect.Action.ActionEventArgs;
			}

			string data = act.ToString().ToUpper().Trim();
			/*
			msg = CheckData(data);
			if(msg.IsSuccess())
			{
			#region Key logic,Push next action and fill it with data
				ObjectState = args;

				ActionPassword acPwd = new ActionPassword();

				acPwd.LastAction = this;

				acPwd.ObjectState = ObjectState;
				this.NextAction = acPwd;
			#endregion

				args.UserCode = data;

				msg.Add(new Message(MessageType.Data,"",new object[]{args}));
			}
			*/	
			args.UserCode = (act as IDCTClient).LoginedUser;
			args.ResourceCode = (act as IDCTClient).ResourceCode;
			switch(data)
			{
				//归属工单
				case ActionType.DataCollectAction_GoMO:
					ActionGotoMO actionGotoMO = new ActionGotoMO();
					actionGotoMO.ObjectState = args;
					this.NextAction = actionGotoMO;

					(act as IDCTClient).CachedAction = actionGotoMO;
					//currentAction = actionGotoMO;
					break;
				//采集GOOD, added by jessie lee
				case ActionType.DataCollectAction_GOOD:
					ActionCollectGood actionCollectGood = new ActionCollectGood();
					actionCollectGood.ObjectState = args;
					this.NextAction = actionCollectGood;

					(act as IDCTClient).CachedAction = actionCollectGood;
					break;
				//采集NG, added by jessie lee, 10/05/2006
				case ActionType.DataCollectAction_NG:
					ActionCollectNG actionCollectNG = new ActionCollectNG();
					actionCollectNG.ObjectState = args;
					this.NextAction = actionCollectNG;

					(act as IDCTClient).CachedAction = actionCollectNG;
					break;
                //采集SMTNG, added by jessie lee, 10/05/2006
                case ActionType.DataCollectAction_SMTNG:
                    ActionCollectSMTNG actionCollectSMTNG = new ActionCollectSMTNG();
                    actionCollectSMTNG.ObjectState = args;
                    this.NextAction = actionCollectSMTNG;

                    (act as IDCTClient).CachedAction = actionCollectSMTNG;
                    break;
				//采集OUTLINEGOOD, added by jessie lee, 2006/8/18
				case ActionType.DataCollectAction_OutLineGood:
					ActionCollectOutlineGood actionCollectOutlineGood = new ActionCollectOutlineGood();
					actionCollectOutlineGood.ObjectState = args;
					this.NextAction = actionCollectOutlineGood;

					(act as IDCTClient).CachedAction = actionCollectOutlineGood;
					break;
				//采集OUTLINENG, added by jessie lee, 2006/8/18
				case ActionType.DataCollectAction_OutLineNG:
					ActionCollectOutlineNG actionCollectOutlineNG = new ActionCollectOutlineNG();
					actionCollectOutlineNG.ObjectState = args;
					this.NextAction = actionCollectOutlineNG;

					(act as IDCTClient).CachedAction = actionCollectOutlineNG;
					break;
				//Keyparts上料, added by jessie lee, 10/05/2006
				case ActionType.DataCollectAction_CollectKeyParts:
					ActionCollectKeyparts actionCollectKeyparts = new ActionCollectKeyparts();
					actionCollectKeyparts.ObjectState = args;
					this.NextAction = actionCollectKeyparts;

					(act as IDCTClient).CachedAction = actionCollectKeyparts;
					break;
				case BaseDCTDriver.NEXTOP :
					ActionNextOP actionNextOP = new ActionNextOP();
					this.NextAction = actionNextOP;
					(act as IDCTClient).CachedAction = actionNextOP;
					break;
                case ActionType.DataCollectAction_CompareAppendix:
                    ActionCompareApp actionCartonCompare = new ActionCompareApp();
                    this.NextAction = actionCartonCompare;
                    
                    (act as IDCTClient).CachedAction = actionCartonCompare;
                    break;
                case ActionType.DataCollectAction_Mix:
                    ActionCollectMix actionCollectMix = new ActionCollectMix();
                    this.NextAction = actionCollectMix;
                    
                    break;
                case ActionType.DataCollectAction_KBatch:
                    ActionBatchWithKeypaerts actionBatchWithKeypaerts = new ActionBatchWithKeypaerts();
                    this.NextAction = actionBatchWithKeypaerts;
                    
                    break;
                case ActionType.DataCollectAction_CompareProductCode:
                    ActionCompareProductCode actionCompareProductCode = new ActionCompareProductCode();
                    this.NextAction = actionCompareProductCode;
                   
                    (act as IDCTClient).CachedAction = actionCompareProductCode;
                    break;
                case ActionType.DataCollectAction_CompareTwo:
                    ActionCompareAppAndProductCode actionCompareAppAndProductCode = new ActionCompareAppAndProductCode();
                    this.NextAction = actionCompareAppAndProductCode;
                    
                    (act as IDCTClient).CachedAction = actionCompareAppAndProductCode;
                    break;
                case ActionType.DataCollectAction_AutoNG:
                    ActionAutoNG actionAutoNG = new ActionAutoNG();
                    this.NextAction = actionAutoNG;
                    
                    (act as IDCTClient).CachedAction = actionAutoNG;
                    break;
                case ActionType.DataCollectAction_FGPacking:
                    ActionFGPacking actionFGPacking = new ActionFGPacking();
                    this.NextAction = actionFGPacking;

                    (act as IDCTClient).CachedAction = actionFGPacking;
                    break;
                case ActionType.DataCollectAction_ONPost:
                    ActionCollectONPost actionCollectONPost = new ActionCollectONPost();
                    this.NextAction = actionCollectONPost;

                    (act as IDCTClient).CachedAction = actionCollectONPost;
                    break;
                case ActionType.DataCollectAction_OffPost:
                    ActionCollectOffPost actionCollectOffPost = new  ActionCollectOffPost();
                    this.NextAction = actionCollectOffPost;

                    (act as IDCTClient).CachedAction = actionCollectOffPost;
                    break;
				default:
					ActionRCard actionRCard = new ActionRCard();
					//actionRCard.ObjectState = args;
					this.NextAction = actionRCard;
					break;
			}
			return msg;
		}


		public override Messages AftAction(object act)
		{
			base.AftAction (act);

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

		#region INormalAction 成员

		public BaseDCTAction CachedAction
		{
			get
			{
				// TODO:  添加 ActionFactory.CachedAction getter 实现
				return currentAction;
			}
			set
			{
				currentAction = value;
			}
		}

		#endregion
	}
}
