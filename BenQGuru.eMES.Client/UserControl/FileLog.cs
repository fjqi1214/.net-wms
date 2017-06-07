using System;
using System.IO;

namespace UserControl
{
	/// <summary>
	/// FileLog 的摘要说明。
	/// </summary>
	public class FileLog
	{
		public FileLog()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public static string FileName="MessageLog.txt";
		public static void  FileLogOut(string fileName,string logStr)
		{
			StreamWriter fs = null;
			try
			{
				if(!File.Exists(fileName))
				{
					File.Create(fileName);
				}

				fs = File.AppendText(fileName);
				fs.WriteLine(logStr);	
			}
			catch
			{
			}
			finally
			{
				if(fs != null)
				{
					fs.Flush();
					fs.Close();
				}
			}
		}

		public static string VersionFileName="Version.txt";
		public static string  GetLocalCSVersion(string fileName)
		{
			string str = String.Empty;

			if(File.Exists(fileName))
			{
				FileStream fs = File.OpenRead(fileName);
				byte[] vr = new byte[fs.Length];
				fs.Read(vr,0,vr.Length);

				fs.Close();
				str = System.Text.Encoding.Default.GetString(vr);
			}

			return str.Trim();
		}

	}
}
