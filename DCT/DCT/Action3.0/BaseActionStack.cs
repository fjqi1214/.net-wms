using System;
using System.Collections;

using UserControl;
using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.Action
{
	/// <summary>
	/// BaseActionStack 的摘要说明。
	/// </summary>
	public class BaseActionStack
	{
		protected ArrayList alWorkingAction = new ArrayList();
		public BaseActionStack()
		{
			
		}

		public FlowDirect CurrentDirect = FlowDirect.WaitingOutPut;

		public virtual void Add(BaseDCTAction action)
		{
			if(alWorkingAction.Count < 1)
			{
				this.alWorkingAction.Add(action);
			}
		}

		public virtual void Login()
		{
			alWorkingAction.Clear();
			ActionUser	action = new ActionUser();
			alWorkingAction.Add(action);
		}

		public virtual void ResetAction()
		{
			alWorkingAction.Clear();
			
			alWorkingAction.Add(new ActionRCard());
		}

		public virtual BaseDCTAction GetNextAction()
		{
			BaseDCTAction ba = null;
			if(alWorkingAction.Count > 0)
			{
				ba =  alWorkingAction[0] as BaseDCTAction;
				if(ba.NextAction != null)
				{
					alWorkingAction.Clear();
					alWorkingAction.Add(ba.NextAction);

					ba = null;

					ba = alWorkingAction[0] as BaseDCTAction;
				}
			}
			return ba;
		}

		public virtual void SendMessage(object sender,Messages msgs)
		{
			Message msg = new Message();
			for(int i = 0 ;i < msgs.Count();i ++)
			{
				if(msgs.Objects(i).Type != MessageType.Data)
				{
					msg = msgs.Objects(i);
				}
			}
			IDCTClient client = sender as IDCTClient;

			if(msg.Body != String.Empty)
			{
				string send_msg = MutiLanguages.ParserMessage(msg.Body);
				if(send_msg.IndexOf("$") < 0)
				{
					client.SendMessage(send_msg);
				}

			}
			if(msg.Exception != null)
			{
				string send_msg = MutiLanguages.ParserMessage(msg.Exception.Message);
				if(send_msg.IndexOf("$") < 0)
				{
					client.SendMessage(send_msg);
				}
			}
			if(msg != null)
			{
				switch(msg.Type)
				{
					case MessageType.Succes:
					{
						break;
					}
					case MessageType.Error:
					{

						System.Threading.Thread.Sleep(500);
						break;
					}
					default:
					{
						break;
					}
				}
			}
		}

		public int CurrentIndex = 0;
	}
}
