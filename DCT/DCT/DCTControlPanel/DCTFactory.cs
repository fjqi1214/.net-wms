using System;

using BenQGuru.eMES.Common.DCT.ATop;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.Common.DCT.ATop.DSC19;
using BenQGuru.eMES.Common.DCT.ATop.GW28;
using BenQGuru.eMES.Common.DCT.ATop.DT4000;
using BenQGuru.eMES.Common.DCT.ATop.DCN500;
using BenQGuru.eMES.Common.DCT.PC;

namespace  BenQGuru.eMES.Common.DCT
{
	/// <summary>
	/// DCTFactory 的摘要说明。
	/// </summary>
	public abstract class DCTFactory
	{
		//		public DCTFactory()
		//		{
		//			//
		//			// TODO: 在此处添加构造函数逻辑
		//			//
		//		}
		public static IDCTClient CreateDCTClient(string ip,int port)
		{
			IDCTClient client = null;

			if (frmDCTServer.IsRS485 == true)
			{
				client = new DSC19Client(ip, port);
			}
			else if(port == 55962)
			{
                client = new GW28Client(ip, port);
			}
			else if(port == 4660)
			{
                client = new DT4000Client(ip, port);
			}
            else if (port == 12345)
            {
                client = new PCClient(ip, port);
            }

			return client;
		}

		public static BaseDCTDriver CreateDCTDriver(string ip,int port)
		{
			BaseDCTDriver driver = null;

			if (frmDCTServer.IsRS485 == true)
			{
				driver = new DSC19DCTDriver();
			}
			else if(port == 55962)
			{
				driver = new GW28DCTDriver();
			}
			else if(port == 4660)
			{
				driver = new DT4000DCTDriver();
			}
            else if (port == 12345)
            {
                driver = new PCDriver();
            }

			// Added by Icyer 2006/12/29 @ YHI	设置切换指令时的Action
			BenQGuru.eMES.Common.DCT.Action.ActionRCard arCard = new BenQGuru.eMES.Common.DCT.Action.ActionRCard();
			arCard.Status = ActionStatus.Working;
			driver.defaultDCTAction = arCard;
			// Added end

			return driver;
		}
	}
}
