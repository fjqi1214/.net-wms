using UserControl;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;

using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.ATop.GW28
{
	/// <author>Laws Lu</author>
	/// <since>2006/04/14</since>
	/// <version>1.0.0</version>
	public class ActionPassword:BaseDCTAction	
	{
		public ActionPassword()
		{
            base.OutMesssage 
				= 	new Message(MessageType.Normal,"$DCT_PLEASE_INPUT_PASSWORD");

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
			

			DataCollect.Action.ActionEventArgs args = null;
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
				//关键逻辑,将ActionEventArgs对象传递到下一个Action

				args.Passwod = data;
				
				object[] objUserGroup = null;
				User user = 
					new Security.SecurityFacade(Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider())
					.LoginCheck(args.UserCode, args.Passwod,out objUserGroup);
				
				args.UserGroup = objUserGroup;
				ObjectState = args;

				// 用户名不存在
				if ( user == null )
				{
					msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_User_Not_Exist"));
				}

				msg.Add(new Message(MessageType.Data,"",new object[]{args}));
			}

			if(msg.IsSuccess())
			{
				base.Action (act);

				ActionResource acRes = new ActionResource();

				//acRes.LastAction = this;

				acRes.ObjectState = ObjectState;
                NextAction = acRes;
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
			if ( data == string.Empty )
			{
				msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_Password_Empty"));
			}

			return msg;
		}
		#endregion
	}
}
