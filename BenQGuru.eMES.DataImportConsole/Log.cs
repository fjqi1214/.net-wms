using System;
using System.IO;
using System.Reflection;

namespace BenQGuru.eMES.DataImportConsole
{
	/// <summary>
	/// Log 的摘要说明。
	/// </summary>
	public class Log
	{
		private string filePath=string.Empty;
		public Log( string filename )
		{
			string DirectoryName = @"ERPImportLog\";																		
			string domainPath =Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),DirectoryName);	
			if(!Directory.Exists(domainPath))
			{
				Directory.CreateDirectory(domainPath);	
			}					
			filePath = string.Format("{0}{1}-log.txt",domainPath,filename);	
		}

		public string FilePath
		{
			get
			{
				return filePath;
			}
		}


		//写消息到文件中
		private void Write(string msg)
		{
			StreamWriter objWrite = null;
			try
			{
				System.Console.WriteLine(msg);

				objWrite=File.AppendText(filePath);
				objWrite.WriteLine(msg);
			}
			catch(System.Exception)
			{

			}
			finally
			{
				if(objWrite != null)
				{
					objWrite.Close();
				}
			}
		}

		public void Info(string msg)
		{
			Write(msg);
		}
	}
}
