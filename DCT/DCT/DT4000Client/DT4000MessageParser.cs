using System;
using System.Collections;

using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.ATop.DT4000
{
	/// <summary>
	/// Class1 的摘要说明。
	/// </summary>
	public class DT4000MessageParser
	{
		public DT4000MessageParser(){}

		public byte[] WrapCommand(byte[] srcCom)
		{
			return srcCom;
		}

        public byte[] WrapMessage(byte[] srcMsg, int line)
        {
            byte[] btReturn = null;
            ArrayList al = new ArrayList();

            //al.AddRange(MessagePreSuffix);

            al.Add(srcMsg.Length + 12);
            al.AddRange(MessagePreSuffix);
            al.AddRange(new byte[] { 0, 0, 0, (byte)line });
            al.AddRange(srcMsg);

            al.AddRange(MessageSuffix);

            btReturn = new byte[al.Count];
            for (int i = 0; i < btReturn.Length; i++)
            {
                btReturn[i] = Convert.ToByte(Convert.ToInt32(al[i]));
            }

            al = null;
            srcMsg = null;

            return btReturn;
        }

        int _DCTEnLineLen = 30;
        public byte[] WrapMessageNew(byte[] srcMsg)
        {
            byte[] btReturn = null;
            ArrayList al = new ArrayList();

            al.AddRange(MessagePrefix);
            al.AddRange(srcMsg);
            al.AddRange(MessageSuffix);

            int count = al.Count;
            if (count >= 3 * _DCTEnLineLen)
                count--;

            btReturn = new byte[count];
            for (int i = 0; i < btReturn.Length; i++)
            {
                btReturn[i] = Convert.ToByte(Convert.ToInt32(al[i]));
            }

            if (btReturn != null)
                btReturn[0] = (byte)btReturn.Length;

            al = null;
            srcMsg = null;

            return btReturn;
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

            //获取总的数据长度
            short dataLength = BitConverter.ToInt16(btMsg, 0);
            if (dataLength > btMsg.Length)
            {
                dataLength = (short)btMsg.Length;
            }

            //获取有效数据
            for (int i = 8; i < dataLength; i++)
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

		public byte[] MessageSuffix = System.Text.Encoding.Default.GetBytes("");
        public byte[] MessagePreSuffix = new byte[] { 0, 64, 0, 0, 0, 0x52, 0 };

        public static byte[] MessageSetCursor1 = new byte[] { 0x0c, 0x00, 0x40, 0x00, 0x00, 0x00, 0x65, 0x00, 0x00, 0x01, 0x00, 0x01 };
        public static byte[] MessageSetCursor2 = new byte[] { 0x0c, 0x00, 0x40, 0x00, 0x00, 0x00, 0x65, 0x00, 0x00, 0x01, 0x00, 0x02 };
        public static byte[] MessageClearText = new byte[] { 0x25, 0x00, 0x40, 0x00, 0x00, 0x00, 0x61, 0x00, 
            0x20, 0x20, 0x20, 0x20, 0x20, 
            0x20, 0x20, 0x20, 0x20, 0x20, 
            0x20, 0x20, 0x20, 0x20, 0x20, 
            0x20, 0x20, 0x20, 0x20, 0x20, 
            0x20, 0x20, 0x20, 0x20, 0x20, 
            0x20, 0x20, 0x20, 0x20
        };
        public static byte[] MessagePrefix = new byte[] { 0x08, 0x00, 0x40, 0x00, 0x00, 0x00, 0x61, 0x00 };
	}
}
