using UserControl;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;

using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.ATop.GW28
{
	/// <author>Laws Lu</author>
	/// <since>2006/04/14</since>
	/// <version>1.0.0</version>
	public class ActionResource:BaseDCTAction	
	{
		public ActionResource()
		{
            base.OutMesssage 
				= 	new Message(MessageType.Normal,"$DCT_PLEASE_INPUT_RESOURCE");

			//base.FlowDirect = FlowDirect.WaitingOutPut;
		}

		public override Messages PreAction(object act)
		{
			base.PreAction (act);

			Messages msg = new Messages();
            msg.Add(this.OutMesssage);

			//base.FlowDirect = FlowDirect.WaitingInput;
			
			return  msg;
		}
		
		public override Messages Action(object act)
		{
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

			msg = CheckData(data);

			if(msg.IsSuccess())
			{
				base.Action (act);
				//关键逻辑,将ActionEventArgs对象传递到下一个Action

				args.ResourceCode = data;

				//ActionPassword acPwd = new ActionPassword();

				//			acPwd.ObjectState = ObjectState;
				//			next_action = acPwd;

				object[] objUserGroup = args.UserGroup;
				bool bIsAdmin = false;
				if(objUserGroup != null)
				{
					foreach(object o in objUserGroup)
					{
						if(((UserGroup)o).UserGroupType == "ADMIN")
						{
							bIsAdmin = true;
							break;
						}
					}
				}

				if (!bIsAdmin)
				{
					if ( !(new Security.SecurityFacade(Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider()))
						.CheckResourceRight(args.UserCode, args.ResourceCode))
					{
						msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_No_Resource_Right"));
					}
				}

				
				ObjectState = args;

				msg.Add(new Message(MessageType.Data,"",new object[]{args}));
			}

			if(msg.IsSuccess())
			{
				msg.Add(new UserControl.Message(UserControl.MessageType.Success,"$DCT_WELCOME"));
				
				if(act is IDCTClient)
				{
					IDCTClient client = act as IDCTClient;
					client.Authorized = true;
					client.LoginedUser = args.UserCode;
					client.LoginedPassword = args.Passwod;
					client.ResourceCode  = args.ResourceCode;
				}

				ActionRCard actRcard = new ActionRCard();
				
				//actRcard.LastAction = this;
                NextAction = actRcard;
			}

			return msg;
				
		}

		public override Messages AftAction(object act)
		{
			base.AftAction (act);

			Messages msg = new Messages();


			return null;
		}

		#region Check Data
		public Messages CheckData(string data)
		{
			Messages msg = new Messages();
			if ( data == string.Empty )
			{
				msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_Resource_Empty"));
			}
			else
			{

				object obj = new BaseModelFacade(Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider()).GetResource(data);

				if ( obj == null )
				{
					msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_Resource_Not_Exist"));
				}
			}

			return msg;
		}
		#endregion

	}
}
