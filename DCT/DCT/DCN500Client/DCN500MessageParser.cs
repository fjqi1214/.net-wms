using System;
using System.Collections;

using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.ATop.DCN500
{
	/// <summary>
	/// Class1 的摘要说明。
	/// </summary>
	public class DCN500MessageParser
	{
		public DCN500MessageParser(){}

		public byte[] WrapCommand(byte[] srcCom)
		{
			return srcCom;
		}
		//Wrap Message
		public byte[] WrapMessage(byte[] srcMsg)
		{
			byte[] btReturn = null;
			ArrayList al = new ArrayList();

			//al.AddRange(MessagePreSuffix);

			al.Add(srcMsg.Length + 10);
			al.AddRange(MessagePreSuffix);

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

			for(int i = 7;i < btMsg.Length ; i++)
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

		public byte[] MessagePreSuffix = new byte[]{0,64,0,0,0,97,0};
	}
}
