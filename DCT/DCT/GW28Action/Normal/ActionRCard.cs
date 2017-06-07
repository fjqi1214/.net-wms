using UserControl;
using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.ATop.GW28
{
	/// <author>Laws Lu</author>
	/// <since>2006/04/14</since>
	/// <version>1.0.0</version>
	public class ActionRCard:BaseDCTAction	
	{
		public ActionRCard()
		{
			base.OutMesssage 
				= 	new Message(MessageType.Normal,"$CS_Please_Input_RunningCard");
			
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
			
			//msg = CheckData(data);
			if(msg.IsSuccess())
			{
				#region Key logic,Push next action and fill it with data
				args.RunningCard = data;

				ObjectState = args;

				ActionFactory acFac = new ActionFactory();

				acFac.ObjectState = ObjectState;
                NextAction = acFac;
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
//			if ( data == string.Empty )
//			{
//				msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_User_Code_Empty"));
//			}

			return msg;
		}
		#endregion

	}
}
