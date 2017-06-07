using UserControl;
using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.Action
{
	/// <author>Laws Lu</author>
	/// <since>2006/04/14</since>
	/// <version>1.0.0</version>
	public class ActionUser:BaseDCTAction	
	{
		public ActionUser()
		{
            this.NeedAuthorized = false;
			this.OutMesssage 
				= 	new Message(MessageType.Normal,"$DCT_PLEASE_LOGON");			
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
			Messages msg = new Messages();

			if(act.ToString() == null)
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

				#region Key logic,Push next action and fill it with data
				args.UserCode = data;

				ActionPassword acPwd = new ActionPassword();

				//acPwd.LastAction = this;

				ObjectState = args;

				acPwd.ObjectState = ObjectState;
				this.NextAction = acPwd;
				#endregion
				

				msg.Add(new Message(MessageType.Data,"",new object[]{args}));

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
				msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_User_Code_Empty"));
			}

			return msg;
		}
		#endregion

	}
}
