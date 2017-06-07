using System;
using BenQGuru.eMES.Common.DCT.ATop.GW28;

namespace DCTSimulateServer
{
	/// <summary>
	/// Simulate 的摘要说明。
	/// </summary>
	class Simulate
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		static void Main(string[] args)
		{
			for(int i = 0;i < 100;i ++)
			{
				System.Threading.ThreadStart ts = new System.Threading.ThreadStart(RunThread);
				System.Threading.Thread cc = new System.Threading.Thread(ts);

				cc.Start();
			}
		}

		public static void RunThread()
		{
			GW28DCTDriver driver = new GW28DCTDriver();
			driver.DCTListen(null);
		}

	}
}
