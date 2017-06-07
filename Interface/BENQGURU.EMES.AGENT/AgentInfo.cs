using System;

namespace BenQGuru.eMES.Agent
{
	/// <summary>
	/// AgentInfo 的摘要说明。
	/// </summary>
	public class AgentInfo
	{
		public AgentInfo()
		{
		}

		/// <summary>
		/// 是否运行
		/// </summary>
		public static string RunStatus;

		/// <summary>
		/// Agent 模块
		/// </summary>
		public static string Module;

		/// <summary>
		/// 监控目录
		/// </summary>
		public static string DirectoryPath;

		/// <summary>
		/// 文件备份目录
		/// </summary>
		public static string BackDirectoryPath
		{
			get
			{
				if(DirectoryPath != null && DirectoryPath != string.Empty)
				{
					return DirectoryPath + "_bak";
				}
				return string.Empty;
			}
		}

		/// <summary>
		/// Agent 循环执行的间隔
		/// </summary>
		public static int Interval = 1000;

		/// <summary>
		/// 文件编码的格式
		/// </summary>
		public static string FileEncoding;

		// Added by Icyer 2006/08/03
		/// <summary>
		/// 是否需要SMT上料
		/// </summary>
		public static bool SMTLoadItem;

		// Added by Laws 2007/01/09
		/// <summary>
		/// 是否允许归属工单
		/// </summary>
		public static bool AllowGoToMO;
		// Added end
	}
}
