using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;

namespace BenQGuru.eMES.SAPData
{
	/// <summary>
	/// SAPImportLoger 导入日志
	/// </summary>
	public class SAPImportLoger
	{
		private string fileName=string.Empty;	//文件名
		public SAPImportLoger()
		{
			this.CreateDefaultLogFile();
			this.Write(@"SAP 数据导入到 MES Log 文件");
		}

		//创建日志文件(每实例化一次都会创建一个新的log文件)
		//文件命名规则 : DateTime.Now.ToString();
		private void CreateDefaultLogFile()
		{
			fileName = DateTime.Now.ToString("yyMMdd-HHmmss");
			//File.Create(fileName);
		}

		//写消息到文件中
		public void Write(string message)
		{
			if(fileName == string.Empty)return;

			string DirectoryName = @"SAPImportLog\";																		//log所在文件夹Name
			string domainPath =Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),DirectoryName);	//当前应用程序路径
			domainPath = domainPath.Replace(@"bin\",string.Empty);						//设置写log路径不在bin目录下，避免BS自动重启。
			Directory.CreateDirectory(domainPath);										//如果路径不存在,则创建
			string filePath = string.Format("{0}{1}-log.txt",domainPath,fileName);		//log文件路径

			StreamWriter objWrite=File.AppendText(filePath);
			objWrite.WriteLine(message);
			objWrite.Close();
		
		}

	}
}
