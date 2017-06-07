using System;
using System.Collections;

using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.ATop.GW28
{
	/// <author>Laws Lu</author>
	/// <since>2006/04/12</since>
	/// <version>1.0.0</version>
	public class GW28MessageParser: IMessageParse 
	{
		public GW28MessageParser(){}

		public byte[] WrapCommand(byte[] srcCom)
		{
			return srcCom;
		}
		//Wrap Message
		public byte[] WrapMessage(byte[] srcMsg)
		{
			byte[] btReturn = null;
			ArrayList al = new ArrayList();
			al.AddRange(srcMsg);

			al.AddRange(MessageSuffix);

			btReturn = new byte[al.Count];
			for(int i = 0;i < btReturn.Length ; i++)
			{
				btReturn[i] = Convert.ToByte(Convert.ToInt32(al[i]));
			}

			al = null;
			srcMsg = null;

			return btReturn;
		}

		public byte[] RetrieveData(byte[] btMsg)
		{
			byte[] btReturn = null;
			ArrayList al = new ArrayList();

			//btReturn = new byte[al.Count];
			for(int i = 0;i < btMsg.Length ; i++)
			{
				if(btMsg[i] != (byte)13 && (int)btMsg[i] != 0)
				{
					al.Add(btMsg[i]);
				}
			}

			btReturn = new byte[al.Count];

			for(int i = 0 ; i < al.Count ; i ++ )
			{
				btReturn[i] = Convert.ToByte(Convert.ToInt32(al[i]));
			}

			al = null;
			btMsg = null;

			return btReturn;
		}

		public byte[] MessageSuffix = System.Text.Encoding.Default.GetBytes("\r\n");
	}
}
