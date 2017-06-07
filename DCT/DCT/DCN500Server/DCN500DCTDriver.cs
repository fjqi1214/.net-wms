using System;

using BenQGuru.eMES.Common.DCT.Action;
using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.ATop.DCN500
{
	public class DCN500DCTDriver :BaseDCTDriver,IDCTDriver
	{
		//		private DCN500Client client;
		//
		//		public event EventCommandHandler AfterLogout;
		//		
		new public BaseActionStack stack = new DCN500WorkingStack();

		private readonly byte[] fixedMsg  = new byte[]{08,64,17}; 
		private readonly byte[] fixedMsg2  = new byte[]{14,64,55}; 
		private readonly byte[] fixedMsg3  = new byte[]{64,55};

		
		#region IDCTDriver ≥…‘±

		public override IDCTClient DCTClient
		{
			get
			{
				return client;
			}
			set
			{
				client = value as DCN500Client;
			}
		}
		
		#endregion

		protected override void Clear()
		{
			if(client != null)
			{
				(client as DCN500Client).SendCommand(DCTCommand.ClearText);
				stack.ResetAction();
			}
		}
		

		//Recieve Data from client
		public override void CycleRequest()
		{
			if(stack.CurrentDirect == FlowDirect.WaitingInput)
			{
				string strRec = null;
				
				strRec = (client as DCN500Client).RecieveData();

				//strRec = (client as DCN500Client).RecievedData;

				BaseDCTAction action = null;
				if(strRec != null)
				{
					//strRec = strRec.Replace(System.Text.Encoding.ASCII.GetString(fixedMsg),"").Trim();
					//strRec = strRec.Replace(System.Text.Encoding.ASCII.GetString(fixedMsg2),"").Trim();
					//strRec = strRec.Replace(System.Text.Encoding.ASCII.GetString(fixedMsg3),"").Trim();
					if(//strRec.IndexOf(System.Text.Encoding.ASCII.GetString(fixedMsg)) < 0 && 
						strRec != String.Empty)
					{
						try
						{
							DealSuperCommand(strRec);

							action = stack.GetNextAction();

							stack.SendMessage(client,action.Do(client),"");
						}
						catch(Exception ex)
						{
							UserControl.Messages msg = new UserControl.Messages();
							msg.Add(new UserControl.Message(ex));
							stack.SendMessage(client,msg,"");
							if(action != null)
							{
								lock(stack)
								{
									//stack.CurrentDirect = action.FlowDirect;
								}
							}
						}
					}
				}
			}
		}
		//public override 
		//Send Message to client
		public override void CycleResponse()
		{
			if(stack.CurrentDirect == FlowDirect.WaitingOutput)
			{
				BaseDCTAction action = stack.GetNextAction();

				try
				{
					stack.SendMessage(client,action.Do(client),"");
				
					lock(stack)
					{
						//stack.CurrentDirect = action.FlowDirect;
					}
				}
				catch(Exception ex)
				{
					UserControl.Messages msg = new UserControl.Messages();
					msg.Add(new UserControl.Message(ex));
					stack.SendMessage(client,msg,"");
				}
			}
		}

		//logon mes 
		protected override void Login()
		{
			BaseDCTAction action = stack.GetNextAction();

            if (stack.CurrentDirect == FlowDirect.WaitingOutput && action == null)
			{	
				action = new ActionUser();
				stack.Add(action);

			
				try
				{
				
					stack.SendMessage(client,action.Do(client),"");
				
					//stack.CurrentDirect = action.FlowDirect;
				}
				catch(Exception ex)
				{
					UserControl.Messages msg = new UserControl.Messages();
					msg.Add(new UserControl.Message(ex));
					stack.SendMessage(client,msg,"");
				}
			}
		}

		public override void DCTListen(object obj)
		{
			try
			{
				client.Open();
			}
			catch{}

			while((client as DCN500Client).ClientStatus == DCN500ClientStatus.Connecting)
			{
				try
				{			
					client.Open();

					if(!client.Authorized)
					{
						Login();
					}

					CycleResponse();

					CycleRequest();

				}
				catch(Exception ex)
				{
					Log.Error(ex.Message);
				}
			}
			
		}

		public override bool DealSuperCommand(string command)
		{
			if(command == LOGIN)
			{
				stack.Login();
			}
			else if(command == CANCEL)
			{
				client.CachedAction = null;
                base.DealSuperCommand(command);
			}
			else
			{
                base.DealSuperCommand(command);
			}

            return true;
		}

	}
}
