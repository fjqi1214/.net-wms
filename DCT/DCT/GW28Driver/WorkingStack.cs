using System;
using System.Collections;

using UserControl;
using BenQGuru.eMES.Common.DCT.Action;
using BenQGuru.eMES.Common.DCT.Core;

namespace  BenQGuru.eMES.Common.DCT.ATop.GW28
{
	/// <summary>
	/// WorkStack 的摘要说明。
	/// </summary>
	public class GW28WorkingStack:BaseActionStack
	{
		//private ArrayList alWorkingAction = new ArrayList();
		public GW28WorkingStack()
		{
			
		}

//		public FlowDirect CurrentDirect = FlowDirect.WaitingOutPut;
//
//		public void Add(BaseDCTAction action)
//		{
//			if(alWorkingAction.Count < 1)
//			{
//				this.alWorkingAction.Add(action);
//			}
//		}
//
		public override void Login()
		{
            this.WorkingActionList.Clear();
			ActionUser	action = new ActionUser();
            this.WorkingActionList.Add(action);
		}
//
		public override void ResetAction()
		{
            this.WorkingActionList.Clear();

            this.WorkingActionList.Add(new ActionRCard());
		}
//
//		public BaseDCTAction GetNextAction()
//		{
//			BaseDCTAction ba = null;
//			if(alWorkingAction.Count > 0)
//			{
//				ba =  alWorkingAction[0] as BaseDCTAction;
//				if(ba.NextAction != null)
//				{
//					alWorkingAction.Clear();
//					alWorkingAction.Add(ba.NextAction);
//
//					ba = null;
//
//					ba = alWorkingAction[0] as BaseDCTAction;
//				}
//			}
//			return ba;
//		}

		public override void SendMessage(object sender,Messages msgs,string command)
		{
			Message msg = new Message();
			for(int i = 0 ;i < msgs.Count();i ++)
			{
				if(msgs.Objects(i).Type != MessageType.Data)
				{
					msg = msgs.Objects(i);
				}
			}
			GW28Client client = sender as GW28Client;

			if(msg.Body != String.Empty)
			{
				string send_msg = MutiLanguages.ParserMessage(msg.Body);
				if(send_msg.IndexOf("$") < 0)
				{
                    if (msg.Type == MessageType.Success)
					{
						//Laws Lu,2006/06/20	if Succes than no suffix
						client.SendMessage(send_msg);
					}

					if(msg.Type == MessageType.Error)
					{
						//Laws Lu,2006/06/20	if error than add suffix char !
						client.SendMessage(send_msg + " !");
					}

					if(msg.Type == MessageType.Normal)
					{
						//Laws Lu,2006/06/20	if normal than add suffix char :
						client.SendMessage(send_msg + " :");
					}
				}

			}
			if(msg.Exception != null)
			{
				string send_msg = MutiLanguages.ParserMessage(msg.Exception.Message);
				if(send_msg.IndexOf("$") < 0)
				{
					//Laws Lu,2006/06/20	if error than add suffix char !
					client.SendMessage(send_msg + " !");
				}

			}

			if(msg != null)
			{
				switch(msg.Type)
				{
                    case MessageType.Success:
					{
						break;
					}
					case MessageType.Error:
					{

						//Laws Lu,2006/06/15 shoren the interval beep time
						//Laws Lu,2006/06/20	beep twice
						client.SendCommand(DCTCommand.SpeakerOff);
						System.Threading.Thread.Sleep(100);
						client.SendCommand(DCTCommand.SpeakerOn);
						System.Threading.Thread.Sleep(50);
						client.SendCommand(DCTCommand.SpeakerOff);
						System.Threading.Thread.Sleep(100);
						client.SendCommand(DCTCommand.SpeakerOn);
						System.Threading.Thread.Sleep(50);
						client.SendCommand(DCTCommand.SpeakerOff);

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

	}
}
