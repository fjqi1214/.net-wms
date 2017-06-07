using System;

namespace WindowsApplication1
{
	/// <summary>
	/// Class1 的摘要说明。
	/// </summary>
	public class Class1
	{
		public Class1()
		{
		}

		private System.Threading.Timer timer = null;
		public void CycleRun()
		{
			timer = new System.Threading.Timer(new System.Threading.TimerCallback(RunState), null, 0, 100);
		}
		private void RunState(object obj)
		{
			int clientId = 0;
			short iSubCmd = -1;
			short iCmdType = -1;
			short iCmdLen = 512;
			byte[] data = new byte[iCmdLen];
			short ret = API2.AB_Tag_RcvMsg(ref clientId, ref iSubCmd, ref iCmdType, ref data[0], ref iCmdLen);
			if (ret > 0)
			{
				Console.WriteLine(ret);
			}
		}
	}
}
