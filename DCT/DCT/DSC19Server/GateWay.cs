using System;

using System.Text;
using System.IO;

namespace  BenQGuru.eMES.Common.DCT.ATop.DSC19
{
	/// <summary>
	/// Class1 的摘要说明。
	/// </summary>
	class GateWayServer:IDisposable	
	{

		public static StreamWriter sw = null;
		public static StringBuilder logInfo = new StringBuilder();

		~GateWayServer()
		{
			Dispose();
		}
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			StreamWriter sw = File.CreateText(DateTime.Now.ToString("yyyyMMdd") + ".log");
			string tmpstr = String.Empty;

			tmpstr = System.Reflection.Assembly.GetExecutingAssembly().Location ;
			tmpstr = "Current working area : " + tmpstr;
			logInfo.Append(tmpstr + "\r\n");

			Console.WriteLine(tmpstr) ;

			tmpstr = "Server start time : " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
			logInfo.Append(tmpstr + "\r\n");

			Console.WriteLine(tmpstr) ;
			//Initial server with configuration file
			InitialServer();
			
		}
		
		public static void InitialServer()
		{
			int	   i, ret ;
			string tmpStr ;

			ret = API2.AB_API_Open() ;
			if (ret < 0)
			{
				tmpStr = "Error: API File Not Found";
				logInfo.Append(tmpStr + "\r\n");
				Console.WriteLine() ;
			}
			else
			{
				ret = AB_LoadConf() ;
				if (ret > 0)
				{
					int iResult;

					tmpStr = "Load gateway from config file";
					logInfo.Append(tmpStr + "\r\n");
					Console.WriteLine(tmpStr) ;

					iResult = API2.AB_GW_Open(0) ;

					if (iResult < 0)
					{
						tmpStr =  "Error: Gateway not found";
						logInfo.Append(tmpStr + "\r\n");
						Console.WriteLine(tmpStr) ;
					}
					else
					{
					
					}
				}
			}
			while(true)
			{
				int		tag_addr ;
				short	msg_type, subcmd, data_cnt ;
				byte [] data = new byte [512] ;
				byte	ch ;

				tag_addr = 0 ;
				subcmd = -1 ;
				msg_type = -1 ;
				data_cnt = 512 ;

				API2.AB_GW_Cnt();
				int iResult = API2.AB_Tag_RcvMsg(ref tag_addr, ref subcmd, ref msg_type, ref data[0], ref data_cnt) ;

				if(iResult > 0)
				{
					Console.WriteLine(System.Text.Encoding.Default.GetString(data));
				}

				System.Threading.Thread.Sleep(100);
			}
			Console.Read();
		}

		public static void DisposeServer()
		{
			int iResturn;
			iResturn = API2.AB_GW_Close( 0) ;
			iResturn = API2.AB_API_Close() ;
			if (iResturn < 0)
			{
				string tmpstr = "Error: Close Gateway failure,you must shutdown it by press CTL + ALT + DEL ";
				logInfo.Append(tmpstr + "\r\n");
				Console.WriteLine(tmpstr);
			}

		}

		public static short GwCnt ;
		public static API2.GwConfType[]	GwConf = new API2.GwConfType[100] ;
		//Load configuration file
		public static short AB_LoadConf()
		{
			string	tmpstr ;
			short	ret ;
			short	i, j ;
			int		id, port ;
			byte [] ip = new byte[20] ;

			GwCnt = API2.AB_GW_Cnt() ;
			if (GwCnt == 0) return( 0) ;

			for (i=0; i<GwCnt; i++)
			{
				ret = API2.AB_GW_Conf( i, out id, out ip[0], out port) ;
				if (ret >= 0)
				{
					GwConf[i].gw_id = (short)id ;
					GwConf[i].port = (short)port ;

					for (j=0; j<20; j++)
						if (ip[j] == 0) break ;

					tmpstr = System.Text.Encoding.ASCII.GetString( ip, 0, j) ;
					GwConf[ i].ip = tmpstr ;
				}
				else
				{
					GwConf[ i].gw_id = 0 ;
					GwConf[ i].port = 0 ;
					GwConf[ i].ip = "" ;
				}
			}
			return( GwCnt) ;
		}

		#region IDisposable 成员

		public void Dispose()
		{
			//Dispose server
			DisposeServer();
			
			string tmpstr = "Server Exit";
			logInfo.Append(tmpstr + "\r\n");
			Console.WriteLine(tmpstr);

			sw.Write(logInfo.ToString());

			sw.Close();
		}

		#endregion
	}
}
