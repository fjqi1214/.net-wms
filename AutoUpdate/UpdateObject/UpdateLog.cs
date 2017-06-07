using System;

namespace Tools
{
	/// <summary>
	/// UpdateLog 的摘要说明。
	/// </summary>
	public class UpdateLog
	{
		public UpdateLog()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

//		private string pkID;
		/// <summary>
		/// get PKID
		/// </summary>
		public string PKID
		{
			get
			{
				return System.Guid.NewGuid().ToString();
			}
		}

		private string fileName;
		/// <summary>
		/// get or set file name
		/// </summary>
		public string FileName
		{
			get
			{
				return fileName;
			}
			set
			{
				fileName = value;
			}
		}

		private string version;
		/// <summary>
		/// get or set current version
		/// </summary>
		public string Version
		{
			set
			{
				version = value;
			}
			get
			{
				return version;
			}
		}

		private string machineName;
		/// <summary>
		/// get or set current machine name
		/// </summary>
		public string MachineName
		{
			get
			{
				System.Net.IPHostEntry entry = System.Net.Dns.Resolve("127.0.0.1");
				
				machineName = entry.HostName;
				return machineName;
			}
//			set
//			{
//				machineName = value;
//			}
		}
		
		private string machineIP;
		/// <summary>
		/// get or set current machine ip
		/// </summary>
		public string MachineIP
		{
			get
			{
				// System.Net.Dns.GetHostByName
				System.Net.IPHostEntry entry = System.Net.Dns.Resolve(System.Environment.MachineName);
				System.Collections.ArrayList al = new System.Collections.ArrayList();
				al.AddRange(entry.AddressList[1].GetAddressBytes());

				string[] ips = new string[al.Count];
				for(int i = 0;i < al.Count; i ++)
				{
					ips[i] = al[i].ToString();
				}
				
				machineIP = String.Join(".",ips);

				return machineIP;
			}
//			set
//			{
//				machineIP = value;
//			}
		}

		private string updateTime;
		/// <summary>
		/// get or set current update time
		/// </summary>
		public string UpdateTime
		{
			get
			{
				return updateTime;
			}
			set
			{
				updateTime = value;
			}
		}

		private string updateResult;
		/// <summary>
		/// get or set current update time
		/// </summary>
		public string Result
		{
			get
			{
				return updateResult;
			}
			set
			{
				updateResult = value;
			}
		}
	}
}
